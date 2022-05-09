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
///CompanyDeptService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
[System.Web.Script.Services.ScriptService]
public class CompanyDeptService : System.Web.Services.WebService
{

    public CompanyDeptService()
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
    public CascadingDropDownNameValue[] GetDepartment(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> DepartmentList = new List<CascadingDropDownNameValue>();
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        string companyid = kv["company"];

        if (companyid == "0")
            return null;

        string pcompanyid = Constants.GetParentCompanyID();
        Department Department = new FM2E.BLL.Basic.Department();
        DepartmentInfo dinfo = new DepartmentInfo();
        //dinfo.CompanyID = companyid;

        dinfo.Level = 1;//先获取第一重节点
        IList<DepartmentInfo> list = Department.Search(dinfo);
        Stack<DepartmentInfo> q = new Stack<DepartmentInfo>();
        foreach (DepartmentInfo item in list)
        {
            if(companyid==pcompanyid || pcompanyid==item.CompanyID || companyid == item.CompanyID)
                q.Push(item);
            
            while (q.Count > 0)
            {
                DepartmentInfo department = q.Pop();
                string text = department.Name;
                if (department.Level > 1)
                {
                    string str = "";
                    for (int i = 1; i < department.Level; i++)
                    {
                        str += "   ";
                    }
                    text = str + text;
                }
                DepartmentList.Add(new CascadingDropDownNameValue(text, department.DepartmentID.ToString()));

                DepartmentInfo childrendinfo = new DepartmentInfo();
                childrendinfo.ParentID = department.DepartmentID;
                IList<DepartmentInfo> childlist = Department.Search(childrendinfo);
                foreach (DepartmentInfo child in childlist)
                {
                    if (companyid == pcompanyid || pcompanyid == child.CompanyID || companyid == child.CompanyID)
                    q.Push(child);
                }
            }
        }
        return DepartmentList.ToArray();
    }

    //------
    [WebMethod]
    public CascadingDropDownNameValue[] GetDepartmentByCompany(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> DepartmentList = new List<CascadingDropDownNameValue>();
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        string companyid = kv["company"];

        if (companyid == "0")
            return null;

        string pcompanyid = Constants.GetParentCompanyID();
        Department Department = new FM2E.BLL.Basic.Department();
        DepartmentInfo dinfo = new DepartmentInfo();
        dinfo.CompanyID = companyid;

        dinfo.Level = 1;//先获取第一重节点
        IList<DepartmentInfo> list = Department.Search(dinfo);
        Stack<DepartmentInfo> q = new Stack<DepartmentInfo>();
        foreach (DepartmentInfo item in list)
        {
            if (companyid == pcompanyid || pcompanyid == item.CompanyID || companyid == item.CompanyID)
                q.Push(item);

            while (q.Count > 0)
            {
                DepartmentInfo department = q.Pop();
                string text = department.Name;
                if (department.Level > 1)
                {
                    string str = "";
                    for (int i = 1; i < department.Level; i++)
                    {
                        str += "   ";
                    }
                    text = str + text;
                }
                DepartmentList.Add(new CascadingDropDownNameValue(text, department.DepartmentID.ToString()));

                DepartmentInfo childrendinfo = new DepartmentInfo();
                childrendinfo.ParentID = department.DepartmentID;
                IList<DepartmentInfo> childlist = Department.Search(childrendinfo);
                foreach (DepartmentInfo child in childlist)
                {
                    if (companyid == pcompanyid || pcompanyid == child.CompanyID || companyid == child.CompanyID)
                        q.Push(child);
                }
            }
        }
        return DepartmentList.ToArray();
    }


    //**********Modified by Xue    For V 3.1.2     2011-10-20****************************************************************************************************
    
    //********** Modified by Xue    For ShenGaoSu    2011-11-28 ********************************************************************************
    [WebMethod]
    public CascadingDropDownNameValue[] GetMaintainDeptTypes(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> reList = new List<CascadingDropDownNameValue>();
        reList.Add(new CascadingDropDownNameValue("自维单位", "1"));
        reList.Add(new CascadingDropDownNameValue("外维单位", "2"));
        return reList.ToArray();
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetMaintainDepts(string knownCategoryValues, string category)
    {
        
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        string deptType = kv["DeptType"];
        int intDeptType = Convert.ToInt32(deptType);

        System.Web.UI.WebControls.ListItem[] MaintainTeams = ListItemHelper.GetAllMaintainTeamsByDeptType(WebUtility.UserData.CurrentUserData.CompanyID,intDeptType);
        List<CascadingDropDownNameValue> reList = new List<CascadingDropDownNameValue>();
        foreach (System.Web.UI.WebControls.ListItem item in MaintainTeams)
        {
            reList.Add(new CascadingDropDownNameValue(item.Text, item.Value));
        }
        return reList.ToArray();
        
        
    }
    //********** Modification Finished 2011-11-28 **********************************************************************************************

    [WebMethod]
    public CascadingDropDownNameValue[] GetApprovelers(string knownCategoryValues, string category)
    {
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        string deptId = kv["MaintainDeptID"];
        long longDeptId = Convert.ToInt32(deptId);

        
        FM2E.BLL.System.User userBLL = new FM2E.BLL.System.User();
        IList userInfoList = userBLL.GetUsersByDepartmentId(longDeptId);
        //IList<FM2E.Model.System.UserInfo> 
        List<CascadingDropDownNameValue> userList = new List<CascadingDropDownNameValue>();
        foreach (FM2E.Model.System.UserInfo item in userInfoList)
        {
            userList.Add(new CascadingDropDownNameValue(item.PersonName, item.UserName));
        }

        return userList.ToArray();
    }

    //**********Modification Finished 2011-6-27**********************************************************************************************

}

