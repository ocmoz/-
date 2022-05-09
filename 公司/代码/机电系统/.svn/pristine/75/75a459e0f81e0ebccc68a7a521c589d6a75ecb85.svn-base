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
public class CompanyService : System.Web.Services.WebService
{
    public CompanyService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    //[WebMethod]
    //public AjaxControlToolkit.CascadingDropDownNameValue[] GetDropDownContents(string knownCategoryValues, string category)
    //{
    //    // Get a dictionary of known category/value pairs
    //    StringDictionary knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);

    //    // Perform a simple query against the data document
    //    return AjaxControlToolkit.CascadingDropDown.QuerySimpleCascadingDropDownDocument(Document, Hierarchy, knownCategoryValuesDictionary, category);
    //}
    [WebMethod]
    public CascadingDropDownNameValue[] GetCompany(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> CompanyList = new List<CascadingDropDownNameValue>();

        Company company = new FM2E.BLL.Basic.Company();
        IList<CompanyInfo> list = company.GetAllCompany();
        foreach (CompanyInfo item in list)
        {
            CompanyList.Add(new CascadingDropDownNameValue(item.CompanyName, item.CompanyID));
        }
        return CompanyList.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetSection(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> SectionList = new List<CascadingDropDownNameValue>();
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        string companyid = kv["company"];

        Section Section = new FM2E.BLL.Basic.Section();
        IList<SectionInfo> list = Section.GetAllSectionByCompany(companyid);
        foreach (SectionInfo item in list)
        {
            SectionList.Add(new CascadingDropDownNameValue(item.SectionName, item.SectionID));
        }
        return SectionList.ToArray();
    }
}

