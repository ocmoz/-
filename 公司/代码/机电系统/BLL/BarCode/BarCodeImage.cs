using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using FM2E.BLL.Utils;

namespace FM2E.BLL.BarCode
{
    public class BarCodeImage
    {
        /// <summary>
        /// 把文字绘制在图片上
        /// </summary>
        /// <param name="strText">所要绘制的文字</param>
        /// <param name="font">文字所用的字体</param>
        /// <param name="fontSize">文字的字号</param>
        /// <returns>绘有文字strText的24位RGB图片</returns>
        public static Bitmap CreateBitmapFromString(string strText, string font, int fontSize)
        {
            Font crFont = new Font(font, fontSize, FontStyle.Regular);

            Bitmap bmpBG = new Bitmap(300, 300, PixelFormat.Format24bppRgb);

            Graphics gCanvas = Graphics.FromImage(bmpBG);

            SolidBrush bgBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
            gCanvas.FillRectangle(bgBrush, new Rectangle(0, 0, bmpBG.Width, bmpBG.Height));      //白色背景

            bgBrush.Dispose();

            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Near;
            SizeF crSize = gCanvas.MeasureString(strText, crFont);

            SolidBrush sBrush = new SolidBrush(Color.FromArgb(255, 0, 0, 0));

            //Draw the   string
            gCanvas.DrawString(strText,                 //string of text
                crFont,                                   //font
                sBrush,                           //Brush
                new PointF(0, 0),  //Position
                StrFormat);
            sBrush.Dispose();
            gCanvas.Dispose();
            gCanvas = null;

            RectangleF rect = new RectangleF(0, 0, crSize.Width <= bmpBG.Width ? crSize.Width : bmpBG.Width, crSize.Height <= bmpBG.Height ? crSize.Height : bmpBG.Height);
            Bitmap textImg = bmpBG.Clone(rect, PixelFormat.Format24bppRgb);
            bmpBG.Dispose();
            bmpBG = null;

            return ConvertTo8bppBitmap(textImg);
        }
        /// <summary>
        /// 获取需要打印的文字的RLL表示
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="font"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public static byte[] GetStringToPrint(string strText, string font, int fontSize)
        {
            Bitmap img = CreateBitmapFromString(strText, font, fontSize);
            return RLLEncode(img);
        }
        /// <summary>
        /// 把24位的RGB图转换为8位的索引图
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        private static Bitmap ConvertTo8bppBitmap(Bitmap img)
        {
            int w = img.Width;
            int h = img.Height;
            Bitmap bmp = new Bitmap(w, h, PixelFormat.Format8bppIndexed);
            BitmapData sourceData = img.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData destData = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

            int sourceSize = sourceData.Stride * sourceData.Height;
            byte[] sourceBuf = new byte[sourceSize];
            Marshal.Copy(sourceData.Scan0, sourceBuf, 0, sourceSize);
            img.UnlockBits(sourceData);

            int destSize = destData.Stride * destData.Height;
            byte[] destBuf = new byte[destSize];
            Marshal.Copy(destData.Scan0, destBuf, 0, destSize);

            int width = destData.Width;
            int height = destData.Height;
            int sourceIndex = 0;
            int dstIndex = 0;
            for (int y = 0; y < height; y++)
            {
                sourceIndex = y * sourceData.Stride;
                dstIndex = y * destData.Stride;

                for (int x = 0; x < width; x++)
                {
                    destBuf[dstIndex++] = (byte)sourceBuf[sourceIndex];
                    sourceIndex += 3;
                }
            }

            Marshal.Copy(destBuf, 0, destData.Scan0, destSize);
            bmp.UnlockBits(destData);

            return bmp;
        }
        /// <summary>
        /// 对8位的黑白索引位图进行RLL编码
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] RLLEncode(Bitmap img)
        {
            MemoryStream rllStream = new MemoryStream();
            int[] curRow = new int[img.Width];     //存放当前行的处理结果

            //清空数组
            Array.Clear(curRow, 0, curRow.Length);

            rllStream.WriteByte(0x40);   //添加 @
            rllStream.WriteByte(0x02);   //添加02，代表使用RLL
            rllStream.Write(ToByteArray((ushort)img.Width), 0, sizeof(ushort));    //图片宽
            rllStream.Write(ToByteArray((ushort)img.Height), 0, sizeof(ushort));   //图片高

            //读取图像数据
            BitmapData sourceData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
            int bufSize = sourceData.Stride * sourceData.Height;
            byte[] imgData = new byte[bufSize];
            Marshal.Copy(sourceData.Scan0, imgData, 0, bufSize);
            img.UnlockBits(sourceData);

            int width = img.Width;
            int height = img.Height;
            int startIndex = 0;
            int curIndex=0;
            int sameColor = 0;   //相同颜色像素的计数
            int sameRow = 1;     //相同行的计数
            byte lastPointColor = 100;    //上一点的颜色
            //逐行处理
            for (int y = 0; y < height; y++)
            {
                startIndex = y * sourceData.Stride;
                //if (IsEmpty(imgData, startIndex, width))   //如果是空行，则跳过
                //    continue;


                //比较当前行与上一行像素是否完全相同
                if (y > 0)
                {
                    if (IsSame(imgData, startIndex, (y - 1) * sourceData.Stride, width))
                    {
                        //相同
                        sameRow++;
                        continue;
                    }
                    else
                    {
                        if (sameRow > 1)
                        {
                            //不相同并且有重复行，则记录下来(用大白点规则)
                            while (sameRow > 128)
                            {
                                rllStream.WriteByte(256 - 128);
                                SaveRowToStream(rllStream, curRow, curIndex);
                                rllStream.WriteByte(256 - 128);
                                sameRow -= 128;
                            }
                            rllStream.WriteByte((byte)(256 - sameRow));
                            SaveRowToStream(rllStream, curRow, curIndex);
                            rllStream.WriteByte((byte)(256 - sameRow));
                        }
                        else
                        {
                            //不相同，并且没有重复行(没有重复行需要用小白点规则)
                            int temp = curRow[0];
                            if (temp > 127)
                            {
                                rllStream.WriteByte(127);
                                rllStream.WriteByte(0);
                                curRow[0] = temp - 127;
                            }
                            SaveRowToStream(rllStream, curRow, curIndex);
                        }
                        sameRow = 1;
                        Array.Clear(curRow, 0, curRow.Length);
                    }
                }

                sameColor = 1;
                lastPointColor = 100;
                curIndex = 0;
                for (int x = 0; x < width; x++)
                {
                    byte color = imgData[startIndex++];
                    if (x == 0 && color == 0)
                    {
                        //此行的第一个像素为黑色,则需要添加一个0
                        curRow[curIndex++] = 0;
                        lastPointColor = color;
                        sameColor = 1;
                        continue;
                    }

                    if (color == lastPointColor)    //与上一点颜色相同
                    {
                        sameColor++;
                    }
                    if (x == width - 1 || (color != lastPointColor && x != 0))    //对于最后一个点和第一个点需要特别注意
                    {
                        //与上一点颜色不相同，则记录上一点结果
                        curRow[curIndex++] = sameColor;
                        sameColor = 1;

                    }
                    //最后一个点是白点，并且与上一点颜色不同
                    if (x == width - 1 && color == 255 && color != lastPointColor)
                    {
                        curRow[curIndex++] = 1;
                        sameColor = 1;
                    }
                    if (x == width - 1 && color == 0)
                    {
                        //此行的最后一个像素为黑色，则需要记录本点并且添加一个0代表以0个白点结束
                        if (color != lastPointColor)
                            curRow[curIndex++] = 1;
                        curRow[curIndex++] = 0;
                    }
                    lastPointColor = color;    //记录本点的颜色
                }
            }
            //记录最后一行
            if (sameRow > 1)
            {
                //不相同并且有重复行，则记录下来(用大白点规则)
                while (sameRow > 128)
                {
                    rllStream.WriteByte(256 - 128);
                    SaveRowToStream(rllStream, curRow, curIndex);
                    rllStream.WriteByte(256 - 128);
                    sameRow -= 128;
                }
                rllStream.WriteByte((byte)(256 - sameRow));
                SaveRowToStream(rllStream, curRow, curIndex);
                rllStream.WriteByte((byte)(256 - sameRow));
            }
            else
            {
                //不相同，并且没有重复行(没有重复行需要用小白点规则)
                int temp = curRow[0];
                if (temp > 127)
                {
                    rllStream.WriteByte(127);
                    rllStream.WriteByte(0);
                    curRow[0] = temp - 127;
                }
                SaveRowToStream(rllStream, curRow, curIndex);
            }

            byte[] rllBytes=rllStream.ToArray();
            rllStream.Close();
            rllStream.Dispose();
            rllStream = null;

            //FileStream fs = new FileStream("D:/rllResult.txt", FileMode.Create);
            //int count = 1;
            //for (int i = 0; i < rllBytes.Length; i++)
            //{
            //    string tmp = rllBytes[i].ToString("");
            //    StringHelper.WriteToStream(fs, tmp+",");
            //    if (count % 16 == 0)
            //        StringHelper.WriteToStream(fs, "\n");

            //    count++;
            //}
            //fs.Flush();
            //fs.Close();

            return rllBytes;
        }
        /// <summary>
        /// 把行数据保存到stream中
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="rowData"></param>
        /// <param name="length"></param>
        private static void SaveRowToStream(Stream stream, int[] rowData,int length)
        {
            for (int i = 0; i < length; i++)
            {
                int temp = rowData[i];
                while (temp > 255)
                {
                    stream.WriteByte(255);
                    stream.WriteByte(0);
                    temp -= 255;
                }
                stream.WriteByte((byte)temp);
            }
        }
        /// <summary>
        /// 判断某行是否只有一个黑色像素
        /// </summary>
        /// <param name="imgData"></param>
        /// <param name="row"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private static bool IsEmpty(byte[] imgData, int startIndex, int width)
        {
            int count = 0;
            for (int i = startIndex; i < startIndex + width; i++)
                if (imgData[i] == 0)
                    count++;

            return count == 1 ? true : false;
        }

        /// <summary>
        /// 比较图片的两行是否完全相同
        /// </summary>
        /// <param name="imgData"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="endRowIndex"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private static bool IsSame(byte[] imgData, int startRowIndex, int endRowIndex, int width)
        {
            for (int i = 0; i < width; i++)
            {
                if (imgData[startRowIndex + i] != imgData[endRowIndex + i])
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 将ushort转换为BigEndian字节数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static byte[] ToByteArray(ushort value)
        {
            byte[] a = new byte[sizeof(ushort)];
            a = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(a);
            return a;
        }
    }
}
