using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using AjaxControlToolkit;
using System.Collections.Generic;
using System.Collections.Specialized;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Utils;

/// <summary>
///LocationService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
[System.Web.Script.Services.ScriptService]
public class LocationService : System.Web.Services.WebService
{

    public LocationService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetCompanyInfo(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> CompanyInfoList = new List<CascadingDropDownNameValue>();
        Company bllcompany = new Company();
        IList list = (List<CompanyInfo>)bllcompany.GetAllCompany();

        foreach (CompanyInfo item in list)
        {
            CompanyInfoList.Add(new CascadingDropDownNameValue(item.CompanyName, item.CompanyID));
        }
        return CompanyInfoList.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetLocationType(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> LocationTypeList = new List<CascadingDropDownNameValue>();
        LocationTypeList.Add(new CascadingDropDownNameValue("隧道", "1"));
        LocationTypeList.Add(new CascadingDropDownNameValue("收费站", "2"));
        LocationTypeList.Add(new CascadingDropDownNameValue("桩号", "3"));
        LocationTypeList.Add(new CascadingDropDownNameValue("仓库", "4"));
        LocationTypeList.Add(new CascadingDropDownNameValue("管理中心", "5"));

        return LocationTypeList.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetLocationName(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> LocationNameList = new List<CascadingDropDownNameValue>();
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        string LocationType = kv["LocationType"];
        string companyid = kv["CompanyInfo"];
        //if (companyid == null || companyid == string.Empty)
        //    companyid = category;
        switch (LocationType)
        {
            case "1":
                {
                    Channal channal = new Channal();
                    ChannalInfo channalinfo = new ChannalInfo();
                    channalinfo.CompanyID = companyid;
                    QueryParam qp = channal.GenerateSearchTerm(channalinfo);
                    qp.PageSize = 500;
                    int rc = 0;
                    IList list = channal.GetList(qp, out rc);
                    foreach (ChannalInfo item in list)
                    {
                        LocationNameList.Add(new CascadingDropDownNameValue(item.ChannalName,item.ChannalID.ToString()));
                    }
                    break;
                }
            case "2":
                {
                    TollGate tollgate = new TollGate();
                    TollGateInfo tollgateinfo = new TollGateInfo();
                    tollgateinfo.CompanyID = companyid;
                    QueryParam qp = tollgate.GenerateSearchTerm(tollgateinfo);
                    qp.PageSize = 500;
                    int rc = 0;
                    IList list = tollgate.GetList(qp, out rc);
                    foreach (TollGateInfo item in list)
                    {
                        LocationNameList.Add(new CascadingDropDownNameValue(item.TollGateName, item.TollGateID.ToString()));
                    }
                    break;
                }
            case "3":
                {

                    break;
                }
            case "4":
                {
                    Warehouse warehouse = new Warehouse();
                    WarehouseInfo warehouseinfo = new WarehouseInfo();
                    warehouseinfo.CompanyID = companyid;
                    QueryParam qp = warehouse.GenerateSearchTerm(warehouseinfo);
                    qp.PageSize = 500;
                    int rc =0;
                    IList list = warehouse.GetList(qp,out rc);
                    foreach (WarehouseInfo item in list)
                    {
                        LocationNameList.Add(new CascadingDropDownNameValue(item.Name, item.WareHouseID.ToString()));
                    }
                    break;
                }
            //case "5":
            //    {
            //        Company bllcompany = new Company();
            //        IList list = (List<CompanyInfo>)bllcompany.GetAllCompany();

            //        foreach (CompanyInfo item in list)
            //        {
            //            LocationNameList.Add(new CascadingDropDownNameValue(item.CompanyName,item.CompanyID));
            //        }

            //        //Warehouse warehouse = new Warehouse();
            //        //WarehouseInfo warehouseinfo = new WarehouseInfo();
            //        //warehouseinfo.CompanyID = companyid;
            //        //QueryParam qp = warehouse.GenerateSearchTerm(warehouseinfo);
            //        //qp.PageSize = 500;
            //        //int rc = 0;
            //        //IList list = warehouse.GetList(qp, out rc);
            //        //foreach (WarehouseInfo item in list)
            //        //{
            //        //    LocationNameList.Add(new CascadingDropDownNameValue(item.Name, item.WareHouseID.ToString()));
            //        //}
            //        break;
            //    }
        }

        return LocationNameList.ToArray();

    }
    

}

