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
using FM2E.Model.Utils;
/// <summary>
///CompanyWarehouseService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
[System.Web.Script.Services.ScriptService]
public class CompanyWarehouseService : System.Web.Services.WebService
{

    public CompanyWarehouseService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

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
        CompanyList.Insert(0, new CascadingDropDownNameValue("非公司所属", "0"));
        return CompanyList.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetWarehouse(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> WarehouseList = new List<CascadingDropDownNameValue>();
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        string companyid = kv["company"];

        WarehouseInfo item = new WarehouseInfo();
        item.WareHouseID = string.Empty;
        item.Name = string.Empty;
        item.AddressName = string.Empty;
        item.AddressID = 0;
        item.AddressCode = string.Empty;
        item.CompanyID = companyid;
        item.ResName = string.Empty;
        item.Contactor = string.Empty;
        item.Phone = string.Empty;
        Warehouse bll = new Warehouse();
        int listcount = 0;
        QueryParam searchTerm = bll.GenerateSearchTerm(item);
        IList list = bll.GetList(searchTerm, out listcount);
        foreach (WarehouseInfo item1 in list)
        {
            WarehouseList.Add(new CascadingDropDownNameValue(item1.Name, item1.WareHouseID));
        }
        return WarehouseList.ToArray();
    }

}

