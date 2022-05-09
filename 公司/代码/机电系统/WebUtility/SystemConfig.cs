using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;
using System.Reflection;
using WebUtility.Components;

namespace WebUtility
{
    public class SystemConfig
    {
        private readonly static SystemConfig instance = new SystemConfig();

        private SystemConfig()
        {
          
            try
            {
                string systemXmlPath = HttpContext.Current.Server.MapPath("~/Xml_Config/SystemConfig.xml");
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(systemXmlPath);

                XmlElement element = null;

                foreach (PropertyInfo pi in this.GetType().GetProperties(BindingFlags.NonPublic|~BindingFlags.Static))
                {
                    if (pi.Name == "Instance")
                        continue;

                    element = xmlDoc.GetElementById(pi.Name);
                    pi.SetValue(this, Convert.ChangeType(element.InnerText.Trim(), pi.PropertyType), null);
                }
            }
            catch (Exception e)
            {
                EventMessage.EventWriteLog(Msg_Type.Error, "读取SystemConfig.xml失败：" + e.Message);
            }
        }

        public static SystemConfig Instance
        {
            get
            {
                return instance;
            }
        }

        #region 字段
        private string systemName = "高速公路机电设备维护系统";
        private string version = "V1.0";
        private int imageWidth = 180;
        private int imageHeight = 120;
        private string uploadPath = "~Public/";
        private int uploadSizeKb = 1024;
        private string uploadFileExt = "zip,rar,doc,jpg,png,gif,bmp,swf";
        private int onlineTime = 30;
        private int loginErrorDisableMinute = 30;
        private int loginErrorMaxNum = 5;
        private int pageSize = 10;
        private bool displayError = false;
        private int refreshInterval = 60000;
        #endregion

        #region 属性
        public string SystemName
        {
            get { return systemName; }
            set { systemName = value; }
        }
        public string Version
        {
            get { return version; }
            set { version = value; }
        }
        public int ImageWidth
        {
            get { return imageWidth; }
            set { imageWidth = value; }
        }
        public int ImageHeight
        {
            get { return imageHeight; }
            set { imageHeight = value; }
        }
        public string UploadPath
        {
            get { return uploadPath; }
            set { uploadPath = value; }
        }
        public int UploadSizeKb
        {
            get { return uploadSizeKb; }
            set { uploadSizeKb = value; }
        }
        public string UploadFileExt
        {
            get { return uploadFileExt; }
            set { uploadFileExt = value; }
        }
        public int OnlineTime
        {
            get { return onlineTime; }
            set { onlineTime = value; }
        }
        public int LoginErrorDisableMinute
        {
            get { return loginErrorDisableMinute; }
            set { loginErrorDisableMinute = value; }
        }
        public int LoginErrorMaxNum
        {
            get { return loginErrorMaxNum; }
            set { loginErrorMaxNum = value; }
        }
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }
        public bool DisplayError
        {
            get { return displayError; }
            set { displayError = value; }
        }
        /// <summary>
        /// 页面刷新周期(单位毫秒)
        /// </summary>
        public int RefreshInterval
        {
            set { refreshInterval = value; }
            get { return refreshInterval; }
        }
        #endregion
    }
}
