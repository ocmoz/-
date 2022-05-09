using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using FM2E.Model.Exceptions;

namespace FM2E.BLL.BarCode
{
 /// <summary>
    /// 打印配置参数
    /// </summary>
    public class PrintParameter
    {
        private string ipAddress;
        private int port;
        private int labelWidth;
        private int pageWidth;
        private int leftMargin;
        private int labelGap;
        private string barCodeType;
        private int bcPrintPointX;
        private int bcPrintPointY;
        private int titlePointX;
        private int titlePointY;
        private int remarkPointX;
        private int remarkPointY;
        private int barCodeHeight;
        private string barCodeRatio;
        private int barMag;
        private string font;
        private int fontSize;
        private int majorVersion;
        private int minorVersion;


        /// <summary>
        /// 根据打印机名称，从配置文件中读取相应的参数
        /// </summary>
        /// <param name="printerID"></param>
        /// <param name="xmlFile">配置文件的绝对路径</param>
        public PrintParameter(string printerID,string xmlFile)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFile);

                XmlElement printer = xmlDoc.GetElementById(printerID);
                if(printer==null)
                    throw new BLLException("找不到打印机："+printerID+"的配置数据");

                XmlNode element = null;

                foreach (PropertyInfo pi in this.GetType().GetProperties(BindingFlags.NonPublic | ~BindingFlags.Static))
                {
                    element = printer.SelectSingleNode(string.Format("parameter[@name='{0}']", pi.Name));

                    pi.SetValue(this, Convert.ChangeType(element.InnerText.Trim(), pi.PropertyType), null);
                }
            }
            catch (Exception e)
            {
                throw new BLLException("读取打印机配置失败", e);
            }
        }

        /// <summary>
        /// 打印机IP地址
        /// </summary>
        public string IPAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }
        /// <summary>
        /// 打印机通讯端口
        /// </summary>
        public int Port
        {
            get { return port; }
            set { port = value; }
        }
        /// <summary>
        /// 每张标签的宽度，以dot为单位
        /// </summary>
        public int LabelWidth
        {
            get { return labelWidth; }
            set { labelWidth = value; }
        }
        /// <summary>
        /// 纸张的宽度，以dot为单位
        /// </summary>
        public int PageWidth
        {
            get { return pageWidth; }
            set { pageWidth = value; }
        }
        /// <summary>
        /// 标签与打印纸左边缘间的间隙大小，以dot为单位
        /// </summary>
        public int LeftMargin
        {
            get { return leftMargin; }
            set { leftMargin = value; }
        }
        /// <summary>
        /// 标签与标签间的间隙大小，以dot为单位
        /// </summary>
        public int LabelGap
        {
            get { return labelGap; }
            set { labelGap = value; }
        }
        /// <summary>
        /// 条形码类型
        /// </summary>
        public string BarCodeType
        {
            get { return barCodeType; }
            set { barCodeType = value; }
        }
        /// <summary>
        /// 条形打印区域原点X坐标，以dot为单位
        /// </summary>
        public int BCPrintPointX
        {
            get { return bcPrintPointX; }
            set { bcPrintPointX = value; }
        }
        /// <summary>
        /// 条形打印区域原点Y坐标，以dot为单位
        /// </summary>
        public int BCPrintPointY
        {
            get { return bcPrintPointY; }
            set { bcPrintPointY = value; }
        }
        /// <summary>
        /// 标签标题打印区域原点X坐标
        /// </summary>
        public int TitlePointX
        {
            get { return titlePointX; }
            set { titlePointX = value; }
        }
        /// <summary>
        /// 标签标题打印区域原点Y坐标
        /// </summary>
        public int TitlePointY
        {
            get { return titlePointY; }
            set { titlePointY = value; }
        }
        /// <summary>
        /// 标签文字备注打印区域原点X坐标
        /// </summary>
        public int RemarkPointX
        {
            get { return remarkPointX; }
            set { remarkPointX = value; }
        }
        /// <summary>
        /// 标签文字备注打印区域原点Y坐标
        /// </summary>
        public int RemarkPointY
        {
            get { return remarkPointY; }
            set { remarkPointY = value; }
        }
        /// <summary>
        /// 条形码区域高度，以dot为单位
        /// </summary>
        public int BarCodeHeight
        {
            get { return barCodeHeight; }
            set { barCodeHeight = value; }
        }
        /// <summary>
        /// 条形码中宽线条与窄线条的大小比例
        /// </summary>
        public string BarCodeRatio
        {
            get { return barCodeRatio; }
            set { barCodeRatio = value; }
        }
        /// <summary>
		///条形码中线条的放大倍率
        /// 
        /// </summary>
        public int BarMag
        {
            get { return barMag; }
            set { barMag = value; }
        }
        /// <summary>
        /// 文字字体
        /// </summary>
        public int FontSize
        {
            set { fontSize = value; }
            get { return fontSize; }
        }
        /// <summary>
        /// 文字字体大小
        /// </summary>
        public string Font
        {
            set { font = value; }
            get { return font; }
        }
        /// <summary>
        /// 打印控件主版本号
        /// </summary>
        public int MajorVersion
        {
            get { return majorVersion; }
            set { majorVersion = value; }
        }
        /// <summary>
        /// 打印控件次版本号
        /// </summary>
        public int MinorVersion
        {
            get { return minorVersion; }
            set { minorVersion = value; }
        }
    }
}
