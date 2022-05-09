
using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using WebUtility.Components;

namespace WebUtility
{
    /// <summary>
    /// 日志消息类
    /// </summary>
    public class EventMessage
    {

        /// <summary>
        /// 信息提示类
        /// </summary>
        /// <param name="M_Type">类型</param>
        /// <param name="M_Title">标题</param>
        /// <param name="M_Body">内容</param>
        /// <param name="M_IconType">Icon类型</param>
        /// <param name="Url">Url</param>
        public static void MessageBox(Msg_Type M_Type, string M_Title, string M_Body, Icon_Type M_IconType,string Url)
        {
            MessageBox(M_Type, M_Title, M_Body, M_IconType, Url,UrlType.Href);
        }

        /// <summary>
        /// 信息提示类
        /// </summary>
        /// <param name="M_Type">类型</param>
        /// <param name="M_Title">标题</param>
        /// <param name="M_Body">内容</param>
        /// <param name="M_IconType">Icon类型</param>
        /// <param name="Url">Url</param>
        /// <param name="ReturnScript">执行Script脚本字符串(需加<script></script>)</param>
        public static void MessageBox(Msg_Type M_Type, string M_Title, string M_Body, Icon_Type M_IconType, string Url, string ReturnScript)
        {
            MessageBox(M_Type, M_Title, M_Body, M_IconType, true, Url, UrlType.Href, ReturnScript);
        }

        /// <summary>
        /// 信息提示类
        /// </summary>
        /// <param name="M_Type">类型</param>
        /// <param name="M_Title">标题</param>
        /// <param name="M_Body">内容</param>
        /// <param name="M_IconType">Icon类型</param>
        /// <param name="Url">Url</param>
        /// <param name="M_UrlType">按钮链接类型</param>
        public static void MessageBox(Msg_Type M_Type, string M_Title, string M_Body, Icon_Type M_IconType, string Url, UrlType M_UrlType)
        {
            MessageBox(M_Type, M_Title, M_Body, M_IconType, true, Url, M_UrlType,"");
        }

        /// <summary>
        /// 信息提示类
        /// </summary>
        /// <param name="M_Type">类型</param>
        /// <param name="M_Title">标题</param>
        /// <param name="M_Body">内容</param>
        /// <param name="M_IconType">icon类型</param>
        /// <param name="M_WriteToLog">是否写入log</param>
        /// <param name="Url">链接地址</param>
        /// <param name="M_UrlType">链接类型</param>
        /// <param name="ReturnScript">执行Script脚本字符串(需加<script></script>)</param>
        public static void MessageBox(Msg_Type M_Type, string M_Title, string M_Body, Icon_Type M_IconType, bool M_WriteToLog, string Url, UrlType M_UrlType, string ReturnScript)
        {
            List<sys_NavigationUrl> M_ButtonList = new List<sys_NavigationUrl>();
            M_ButtonList.Add(new sys_NavigationUrl("确定", Url, "", M_UrlType, true));
            MessageBox(M_Type, M_Title, M_Body, null, M_IconType, M_WriteToLog, M_ButtonList, ReturnScript);
        }

        /// <summary>
        /// 信息提示类
        /// </summary>
        /// <param name="M_Type">类型</param>
        /// <param name="M_Title">标题</param>
        /// <param name="M_Body">内容</param>
        /// <param name="M_Exception">导致错误发生的异常</param>
        /// <param name="M_IconType">icon类型</param>
        /// <param name="M_WriteToLog">是否写入log</param>
        /// <param name="Url">链接地址</param>
        /// <param name="M_UrlType">链接类型</param>
        /// <param name="ReturnScript">执行Script脚本字符串(需加<script></script>)</param>
        public static void MessageBox(Msg_Type M_Type, string M_Title, string M_Body,Exception M_Exception, Icon_Type M_IconType, bool M_WriteToLog, string Url, UrlType M_UrlType, string ReturnScript)
        {
            List<sys_NavigationUrl> M_ButtonList = new List<sys_NavigationUrl>();
            M_ButtonList.Add(new sys_NavigationUrl("确定", Url, "", M_UrlType, true));
            MessageBox(M_Type, M_Title, M_Body, M_Exception, M_IconType, M_WriteToLog, M_ButtonList, ReturnScript);
        }

        /// <summary>
        ///  信息提示
        /// </summary>
        /// <param name="M_Type">类型</param>
        /// <param name="M_Title">标题</param>
        /// <param name="M_Body">内容</param>
        /// <param name="M_Exception">导致错误发生的异常</param>
        /// <param name="M_IconType">icon类型</param>
        /// <param name="M_WriteToLog">是否写入log</param>
        /// <param name="M_ButtonList">按钮类型</param>
        /// <param name="M_ReturnScript">执行Script脚本字符串(需加<script></script>)</param>
        public static void MessageBox(Msg_Type M_Type, string M_Title, string M_Body, Exception M_Exception, Icon_Type M_IconType, bool M_WriteToLog, List<sys_NavigationUrl> M_ButtonList, string M_ReturnScript)
        {
            MessageBox mbx = new MessageBox();
            mbx.M_Body = M_Body;
            mbx.M_Type = M_Type;
            mbx.M_Exception = M_Exception;
            mbx.M_ButtonList = M_ButtonList;
            mbx.M_IconType = M_IconType;
            mbx.M_Title = M_Title;
            mbx.M_WriteToLog = M_WriteToLog;
            mbx.M_ReturnScript = M_ReturnScript;
            MessageBox(mbx);
        }

        /// <summary>
        /// 信息提示
        /// </summary>
        /// <param name="MBx">信息提示类</param>
        public static void MessageBox(MessageBox MBx)
        {
            if (MBx.M_WriteToLog)
            {
                if (MBx.M_Exception != null)
                {
                    string msg = string.Format("{0}:{1}{2} \nSTACK{3}", MBx.M_Body, MBx.M_Exception.Message, MBx.M_Exception.InnerException != null ? "(caused by " + MBx.M_Exception.InnerException.Message + ")" : "",MBx.M_Exception.StackTrace);
                    EventWriteLog(MBx.M_Type, msg);
                }
                else
                {
                    EventWriteLog(MBx.M_Type, MBx.M_Body);
                }
            }
            if (MBx.M_ButtonList.Count > 0)
            {
                System.Web.HttpContext.Current.Response.Cookies[string.Format("{0}-MessageValue",Common.Get_CookiesName)].Value = Serializable_MessageBox(Serializable_MessageBox(MBx));
                System.Web.HttpContext.Current.Response.Redirect(string.Format("~/Messages.aspx?OPID={0}",Common.RndNum(5)));
            }            
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="E_Type">日志类型</param>
        /// <param name="E_Record">日志内容</param>
        public static void EventWriteLog(Msg_Type E_Type, string E_Record)
        {
            switch (E_Type)
            {
                case Msg_Type.Info:
                    FileLogWriter.Instance.WriteInfo(E_Record);
                    break;
                case Msg_Type.Error:
                    FileLogWriter.Instance.WriteError(E_Record);
                    break;
                case Msg_Type.Fatal:
                    FileLogWriter.Instance.WriteFatal(E_Record);
                    break;
                case Msg_Type.Debug:
                    FileLogWriter.Instance.WriteDebug(E_Record);
                    break;
                case Msg_Type.Warn:
                    FileLogWriter.Instance.WriteWarn(E_Record);
                    break;
                default:
                    FileLogWriter.Instance.WriteInfo(E_Record);
                    break;
            }
        }

        /// <summary>
        /// 序列化MessageBox类
        /// </summary>
        /// <param name="MBx">MessageBox类</param>
        /// <returns>字符数组</returns>
        public static byte[] Serializable_MessageBox(MessageBox MBx)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            byte[] b;
            formatter.Serialize(ms, MBx);
            ms.Position = 0;
            b = new byte[ms.Length];
            ms.Read(b, 0, b.Length);
            ms.Close();
            return b;
        }

        /// <summary>
        /// 将字节数组转为ASCII字符
        /// </summary>
        /// <param name="MessageArray">字节数组</param>
        /// <returns></returns>
        public static string Serializable_MessageBox(byte[] MessageArray)
        {
            return Convert.ToBase64String(MessageArray);
        }

        /// <summary>
        /// 反序列化MessageBox类
        /// </summary>
        /// <param name="BytArray">字节内容</param>
        /// <returns></returns>
        public static MessageBox Deserialize_MessageBox(byte[] BytArray)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            ms.Write(BytArray, 0, BytArray.Length);
            ms.Position = 0;
            MessageBox MBx = (MessageBox)formatter.Deserialize(ms);
            return MBx;            
        }

        /// <summary>
        /// 将字符按ASCII转为字节数组
        /// </summary>
        /// <param name="Messages"></param>
        /// <returns></returns>
        public static byte[] Deserialize_MessageBox(string Messages)
        {
            return Convert.FromBase64String(Messages);
        }
    }
}
