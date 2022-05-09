using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Reflection;

using Cobainsoft.Windows.Forms;
using FM2E.IDAL.Utils;
using System.Collections;
using FM2E.IDAL.Equipment;
using System.Net.Sockets;
using System.Net;
using FM2E.Model.Exceptions;
using FM2E.BLL.Utils;

namespace FM2E.BLL.BarCode
{
    public class BarCode
    {
        private const int dataSize = 1024;
        //private byte[] sendBytes = new byte[dataSize];
        private byte[] recvBytes = new byte[dataSize];
        private Socket clientSocket = null;

        public BarCode()
        {
        }

        public static Image GenerateBarCodeImage(string code, BarcodeType type, string copyright, int width, int height)
        {
            // 使用条码控件生成条码图片并保存在内存中
            BarcodeControl barcode = new BarcodeControl();
            barcode.BarcodeType = type;
            barcode.CopyRight = copyright; // 空字符串就会不显示标题
            barcode.Data = code;

            MemoryStream stream = new MemoryStream();
            barcode.MakeImage(ImageFormat.Jpeg, width, height, true, false, null, stream);
            Image img = Image.FromStream(stream);
            return img;

        }
        /// <summary>
        /// 生成设备条形码，拆分编号为0
        /// </summary>
        /// <param name="companyID">公司编号</param>
        /// <param name="purchaseDate">购买年月</param>
        /// <param name="isComponent">是否零配件</param>
        /// <returns></returns>
        public static string GenerateBarCode(string companyID, DateTime purchaseDate, bool isComponent)
        {
            string equipmentNO = "";

            IEquipmentIDAssign idAssignDal = FM2E.DALFactory.UtilsAccess.CreateEquipmentIDAssign();
            equipmentNO = companyID;
            equipmentNO += purchaseDate.ToString("yyMM");
            equipmentNO += GetEquipmentIDString(idAssignDal.GetEquipmentID(companyID, purchaseDate)) + "0";
            equipmentNO += (isComponent ? "1" : "0");
            return equipmentNO;
        }
        /// <summary>
        /// 生成设备条形码，拆分编号为0~splitCount
        /// </summary>
        /// <param name="companyID">公司编号</param>
        /// <param name="purchaseDate">购买年月</param>
        /// <param name="splitCount">需要拆分出来的数量</param>
        /// <returns></returns>
        public static string[] GenerateBarCode(string companyID, DateTime purchaseDate, int splitCount, bool isComponent)
        {
            string[] equipmentNOs = new string[splitCount + 1];
            IEquipmentIDAssign idAssignDal = FM2E.DALFactory.UtilsAccess.CreateEquipmentIDAssign();
            int equipmentID = idAssignDal.GetEquipmentID(companyID, purchaseDate);
            if (equipmentID == -1)
                return null;

            for (int i = 0; i <= splitCount; i++)
            {
                equipmentNOs[i] = companyID;
                equipmentNOs[i] += purchaseDate.ToString("yyMM");
                equipmentNOs[i] += GetEquipmentIDString(equipmentID);
                equipmentNOs[i] += IntToLetter(i);
                equipmentNOs[i] += (!isComponent && i == 0 ? "0" : "1");
            }
            return equipmentNOs;
        }
        /// <summary>
        /// 生成折分出来的设备的条形码
        /// </summary>
        /// <param name="parentEquipmentNO"></param>
        /// <returns></returns>
        public static string GenerateBarCode(string parentEquipmentNO)
        {
            string equipmentNO = "";

            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            equipmentNO = parentEquipmentNO.Substring(0, parentEquipmentNO.Length - 2);
            int splitNO = dal.GetNextSplitNO(equipmentNO + "0_");
            equipmentNO += IntToLetter(splitNO);
            equipmentNO += parentEquipmentNO[parentEquipmentNO.Length - 1];
            return equipmentNO;
        }
        /// <summary>
        /// 生成barCodeCount个条形码
        /// </summary>
        /// <param name="companyID">公司编号</param>
        /// <param name="purchaseDate">购买年月</param>
        /// <param name="isComponent">是否零配件</param>
        /// <param name="barCodeCount">需要生成的条形码数量</param>
        /// <returns></returns>
        public static string[] GenerateBarCode(string companyID, DateTime purchaseDate, bool isComponent, int barCodeCount)
        {
            string[] equipmentNOs = new string[barCodeCount];
            IEquipmentIDAssign idAssignDal = FM2E.DALFactory.UtilsAccess.CreateEquipmentIDAssign();
            int equipmentID = idAssignDal.GetEquipmentID(companyID, purchaseDate, barCodeCount);
            if (equipmentID == -1)
                return null;

            for (int i = 0; i < barCodeCount; i++)
            {
                equipmentNOs[i] = companyID;
                equipmentNOs[i] += purchaseDate.ToString("yyMM");
                equipmentNOs[i] += GetEquipmentIDString(equipmentID + i);
                equipmentNOs[i] += "0";
                equipmentNOs[i] += (!isComponent?"0" : "1");
            }
            return equipmentNOs;
        }


        /// <summary>
        /// 格式化设备ID
        /// </summary>
        /// <param name="equipmentID"></param>
        /// <returns></returns>
        private static string GetEquipmentIDString(int equipmentID)
        {
            string tmp = equipmentID.ToString("0000");
            return tmp.Substring(tmp.Length - 4);
        }

        private static string IntToLetter(int num)
        {
            string result = "";
            if (num < 10)
                result = num.ToString();
            else
            {
                string startLetter = "A";
                int tmp = Encoding.ASCII.GetBytes(startLetter)[0];
                tmp = tmp + (num - 10);
                result += (char)tmp;
            }

            return result;
        }

        public void PrintBarCode(BarCodeInfo[] barCodes,PrintParameter param)
        {
            if (barCodes == null || barCodes.Length <= 0)
                return;

            MemoryStream cmdStream = new MemoryStream(100);
            StringHelper.WriteToStream(cmdStream, "INPUT ON\n");
            StringHelper.WriteToStream(cmdStream, "CLEAR\n");
            StringHelper.WriteToStream(cmdStream, "FT \"Swiss 721 BT\"\n");

            int columns = param.PageWidth / param.LabelWidth;   //每行的标签数

            int count = 0;
            foreach (BarCodeInfo item in barCodes)
            {
                int barCodeX = param.LeftMargin + count * (param.LabelWidth + param.LabelGap) + param.BCPrintPointX;
                int barCodeY = param.BCPrintPointY;

                StringHelper.WriteToStream(cmdStream, string.Format("PP {0},{1}\n", barCodeX, barCodeY));//设置条形码区域
                StringHelper.WriteToStream(cmdStream, string.Format("BT \"{0}\"\n", param.BarCodeType));//设置条形码类型
                StringHelper.WriteToStream(cmdStream, string.Format("BR {0}\n", param.BarCodeRatio));
                StringHelper.WriteToStream(cmdStream, string.Format("BH {0}\n", param.BarCodeHeight));
                StringHelper.WriteToStream(cmdStream, string.Format("BM {0}\n", param.BarMag));
                StringHelper.WriteToStream(cmdStream, string.Format("PB \"{0}\"\n", item.BarCode)); //设置条形码值

                int remarkX = param.LeftMargin + count * (param.LabelWidth + param.LabelGap) + param.RemarkPointX;
                int remarkY = param.RemarkPointY;

                StringHelper.WriteToStream(cmdStream, string.Format("PP {0},{1}\n", remarkX, remarkY));//设置文字区域
                byte[] textBytes = null;
                if (!string.IsNullOrEmpty(item.CompanyName.Trim()))
                {
                    textBytes = BarCodeImage.GetStringToPrint(item.BarCode + "(" + item.CompanyName + ")", "宋体", 18);
                }
                else textBytes = BarCodeImage.GetStringToPrint(item.BarCode, "宋体", 18);

                StringHelper.WriteToStream(cmdStream, string.Format("PRBUF {0}\n", textBytes.Length));//设置图片长度
                StringHelper.WriteToStream(cmdStream, textBytes);
                StringHelper.WriteToStream(cmdStream, "\n");

                int titleX = param.LeftMargin + count * (param.LabelWidth + param.LabelGap) + param.TitlePointX;
                int titleY = param.TitlePointY;
                if (!string.IsNullOrEmpty(item.EquipmentName.Trim()))
                {
                    StringHelper.WriteToStream(cmdStream, string.Format("PP {0},{1}\n", titleX, titleY));//设置标题区域
                    //StringHelper.WriteToStream(cmdStream, string.Format("PT \"{0}\"\n", "Title"));
                    byte[] titleBytes = BarCodeImage.GetStringToPrint(item.EquipmentName, "宋体", 18);
                    StringHelper.WriteToStream(cmdStream, string.Format("PRBUF {0}\n", titleBytes.Length));
                    StringHelper.WriteToStream(cmdStream, titleBytes);
                    StringHelper.WriteToStream(cmdStream, "\n");
                }

                count++;

                if (count % columns == 0)
                {
                    count = 0;
                    StringHelper.WriteToStream(cmdStream, "PF\nCLL\nPRINT KEY OFF\n");
                }
            }
            if (count != 0)
                StringHelper.WriteToStream(cmdStream, "PF\nCLL\nPRINT KEY OFF\n");
            //StringHelper.WriteToStream(cmdStream, "INPUT OFF\n");

            byte[] cmdBytes = cmdStream.ToArray();
            cmdStream.Close();
            cmdStream.Dispose();
            cmdStream = null;

            //建立tcp/ip连接
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress printServer = IPAddress.Parse(param.IPAddress);
            int port = param.Port;

            try
            {
                clientSocket.Connect(printServer, port);
                //sendBytes = Encoding.ASCII.GetBytes(printCmd.ToString());
                //发送打印请求
                clientSocket.BeginSend(cmdBytes, 0, cmdBytes.Length, SocketFlags.None, new AsyncCallback(SendData), clientSocket);
            }
            catch (SocketException ex)
            {
                throw new BLLException("与打印机通讯失败", ex);
            }
            catch (Exception ex)
            {
                throw new BLLException("打印过程出现错误", ex);
            }

        }
        /// <summary>
        /// 异步发送打印请求
        /// </summary>
        /// <param name="ar"></param>
        private void SendData(IAsyncResult ar)
        {
            try
            {
                Socket socket = (Socket)ar.AsyncState;

                int send = socket.EndSend(ar);
                //socket.BeginReceive(recvBytes, 0, recvBytes.Length, SocketFlags.None, new AsyncCallback(ReceiveData), socket);
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                clientSocket = null;
            }
            catch
            {
                throw;
            }
        }
        ///// <summary>
        ///// 异步接收打印机返回的信息
        ///// </summary>
        ///// <param name="ar"></param>
        //private void ReceiveData(IAsyncResult ar)
        //{
        //    try
        //    {
        //        //Socket server = (Socket)ar.AsyncState;

        //        //int receiveDataLength = server.EndReceive(ar);

        //        //string str = Encoding.ASCII.GetString(recvBytes, 0, receiveDataLength);

        //        clientSocket.Shutdown(SocketShutdown.Both);
        //        clientSocket.Close();
        //        clientSocket = null;

        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (clientSocket != null)
        //        {
        //            clientSocket.Shutdown(SocketShutdown.Both);
        //            clientSocket.Close();
        //            clientSocket = null;
        //        }
        //    }
        //}

    }
}
