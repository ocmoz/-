using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using AjaxControlToolkit;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;

/// <summary>
///CompanyService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
[System.Web.Script.Services.ScriptService]
public class ArchivesService : System.Web.Services.WebService
{
    public ArchivesService()
    {
        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetModule(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> ModuleList = new List<CascadingDropDownNameValue>();
        XmlDocument _ConfigXml = new XmlDocument();
        _ConfigXml.Load(HttpContext.Current.Server.MapPath("~") + "/Module/FM2E/ArchivesManager/ArchivesConfig.xml");
        XmlNodeList nodelist = _ConfigXml.SelectSingleNode("ArchivesConfig").ChildNodes;
        foreach (XmlNode node in nodelist)
        {
            ModuleList.Add(new CascadingDropDownNameValue(node.Attributes["ModuleName"].Value, node.Name));
        }
        return ModuleList.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetSubmodule(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> SubmoduleList = new List<CascadingDropDownNameValue>();
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        string Module = kv["Module"];

        XmlDocument _ConfigXml = new XmlDocument();
        _ConfigXml.Load(HttpContext.Current.Server.MapPath("~") + "/Module/FM2E/ArchivesManager/ArchivesConfig.xml");

        XmlNode node = _ConfigXml.SelectSingleNode("ArchivesConfig/" + Module);
        foreach (XmlNode subnode in node.ChildNodes)
        {
            SubmoduleList.Add(new CascadingDropDownNameValue(subnode.Attributes["ModuleName"].Value, subnode.Name));
        }
        return SubmoduleList.ToArray();
    }
}

