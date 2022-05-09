using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Xml;
using WebUtility.Components;
using WebUtility;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;

/// <summary>
///Constant 的摘要说明
/// </summary>
public class Constants
{
    /// <summary>
    /// 打印机配置文件存放路径
    /// </summary>
    public static string PRINTER_CONFIG_FILE = HttpContext.Current.Server.MapPath("~/Xml_Config/PrinterConfig.xml");
    public static string BARCODE_SESSION_STRING = "BarCodesNeedToPrint";

    public Constants()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 日期格式年月日
    /// </summary>
    public const string DateFormatString = "yyyy-MM-dd";
    /// <summary>
    /// 时间格式，年月日 时分
    /// </summary>
    public const string TimeFormatString = "yyyy-MM-dd HH:mm";

    public class EquipmentUnit
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    /// <summary>
    /// 获取所有单位
    /// </summary>
    /// <returns></returns>
    public static IList GetUnits()
    {
        ArrayList list = new ArrayList();
        try
        {
            string systemXmlPath = HttpContext.Current.Server.MapPath("~/Xml_Config/UnitConfig.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(systemXmlPath);

            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Unit");

            foreach (XmlNode item in nodeList)
            {
                if (item.InnerText.Trim() != string.Empty)
                {
                    list.Add(item.InnerText.Trim());
                    //EquipmentUnit eu = new EquipmentUnit();
                    //eu.Value = item.InnerText.Trim();
                    //eu.Name = item.Attributes["pinyin"].ToString() + eu.Value;
                    //list.Add(eu);
                }
            }

        }
        catch (Exception e)
        {
            list.Clear();
            list.Add("获取单位列表出错");
            EventMessage.EventWriteLog(Msg_Type.Error, "读取UnitConfig.xml失败：" + e.Message);
        }
        return list;
    }
    /// <summary>
    ///  获取所有条形码打印机
    /// </summary>
    /// <returns></returns>
    public static IList GetBarCodePrinters()
    {
        ArrayList list = new ArrayList();
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(PRINTER_CONFIG_FILE);

            XmlNodeList printers = xmlDoc.GetElementsByTagName("Printer");
            if (printers == null||printers.Count==0)
                throw new Exception("找不到条形码打印机的配置数据");

            foreach (XmlNode item in printers)
            {
                string printerName = item.Attributes["name"].Value;
                if (printerName != "")
                    list.Add(printerName);
            }
        }
        catch (Exception e)
        {
            list.Clear();
            list.Add("获取打印机列表出错");
            EventMessage.EventWriteLog(Msg_Type.Error, "读取PrinterConfig.xml失败：" + e.Message);
        }
        return list;
    }

    /// <summary>
    /// 获取总公司的ID
    /// </summary>
    /// <returns></returns>
    public static string GetParentCompanyID()
    {
        string pid = (string)HttpContext.Current.Cache["ParentCompanyID"];
        if (string.IsNullOrEmpty(pid))
        {
            Company bll = new Company();
            IList<CompanyInfo> list = bll.GetAllCompany();
            foreach (CompanyInfo c in list)
            {
                if (c.IsParentCompany.HasValue && c.IsParentCompany.Value)
                {
                    pid = c.CompanyID;
                    HttpContext.Current.Cache["ParentCompanyID"] = pid;
                }
            }
        }
        return pid;
    }
}
