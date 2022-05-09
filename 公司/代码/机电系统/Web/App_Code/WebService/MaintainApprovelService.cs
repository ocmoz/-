using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using AjaxControlToolkit;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;

/// <summary>
///MaintainApprovelService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class MaintainApprovelService : System.Web.Services.WebService
{

    public MaintainApprovelService()
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
    public CascadingDropDownNameValue[] GetMaintainDepts(string knownCategoryValues, string category)
    {
        System.Web.UI.WebControls.ListItem [] MaintainTeams = ListItemHelper.GetAllMaintainTeams(WebUtility.UserData.CurrentUserData.CompanyID);
        List<CascadingDropDownNameValue> reList = new List<CascadingDropDownNameValue>();
        foreach (System.Web.UI.WebControls.ListItem item in MaintainTeams)
        {
            reList.Add(new CascadingDropDownNameValue(item.Text,item.Value));
        }
        return reList.ToArray();
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetApprovelers(string knownCategoryValues, string category)
    {
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        string deptId = kv["MaintainDeptID"];
        long addressId = Convert.ToInt32(deptId);

        FM2E.Model.System.UserSearchInfo userSearch = new FM2E.Model.System.UserSearchInfo();
        userSearch.DepartmentID = addressId;
        FM2E.BLL.System.User userBLL = new FM2E.BLL.System.User();
        int num = 0;
        IList userInfoList = userBLL.GetList(userSearch,0,999,out num);    //PageSize
        //IList<FM2E.Model.System.UserInfo> 
        List<CascadingDropDownNameValue> userList = new List<CascadingDropDownNameValue>();
        foreach (FM2E.Model.System.UserInfo item in userInfoList)
        {
            userList.Add(new CascadingDropDownNameValue(item.PersonName, item.UserName));
        }

        return userList.ToArray();
    }

}

