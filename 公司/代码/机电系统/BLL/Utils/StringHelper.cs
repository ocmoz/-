using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FM2E.BLL.Utils
{
    public class StringHelper
    {
        /// <summary>
        /// 获取文本的字节表示，采用ASCII编码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string text)
        {
            return Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// 把文本写到流中
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="text"></param>
        public static void WriteToStream(Stream stream,string text)
        {
            stream.Write(GetBytes(text), 0, text.Length);
        }
        /// <summary>
        /// 把字节数组写到流中
        /// </summary>
        /// <param name="array"></param>
        public static void WriteToStream(Stream stream,byte[] array)
        {
            stream.Write(array, 0, array.Length);
        }
    }
}
