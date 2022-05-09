using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using WebUtility;

using WebUtility.WebControls;
using WebUtility.Components;


using FM2E.Model.System;
using FM2E.Model.SpecialProject;
using FM2E.Model.Exceptions;

using FM2E.BLL.System;
using FM2E.BLL.SpecialProject;
using System.Collections;

public partial class Module_FM2E_SpecialProject_QueryProject : System.Web.UI.Page
{

    
    SpecialProject specialProjectBll = new SpecialProject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
            FillData();
        }
    }

    private void InitPage()
    {
        //绑定公司到下拉列表
        Company companyBll = new Company();
        IList<CompanyInfo> companyList = companyBll.GetAllCompany();

        DropDownList_Company.Items.Clear();
        DropDownList_Company.Items.Add(new ListItem("请选择公司", ""));
        foreach (CompanyInfo item in companyList)
        {
            DropDownList_Company.Items.Add(new ListItem(item.CompanyName, item.CompanyID));
        }
        
        try
        {
            DropDownList_Company.SelectedValue = UserData.CurrentUserData.CompanyID;
        }
        catch
        {
            EventMessage.EventWriteLog(WebUtility.Components.Msg_Type.Warn,"在公司下拉列表中找不到用户所在公司，用户名："+UserData.CurrentUserData.UserName+" 用户公司："+UserData.CurrentUserData.CompanyID);
        }
    }

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        FillData();
    }

    /// <summary>
    /// 换页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        //if (ScriptManager1.IsInAsyncPostBack && !ScriptManager1.IsNavigating)
        //{
        //    ScriptManager1.AddHistoryPoint("Index", AspNetPager1.CurrentPageIndex.ToString());
        //}
        FillData();
    }



    /// <summary>
    /// 填充列表
    /// </summary>
    private void FillData()
    {
        try
        {
            int pageIndex = AspNetPager1.CurrentPageIndex;
            int listCount = 0;
            Array array = Enum.GetValues(typeof(SpecialProjectStatus));
            SpecialProjectStatus[] ar = new SpecialProjectStatus[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                ar[i] = (SpecialProjectStatus)array.GetValue(i);
            }

            SpecialProjectSearchInfo info = new SpecialProjectSearchInfo();
            info.StatusArray = ar;

            info.CompanyID = DropDownList_Company.SelectedValue;

            info.BidCompany = TextBox_BidCompany.Text.Trim();
            try { info.BudgetLower = decimal.Parse(TextBox_BudgetLower.Text.Trim()); }
            catch { }
            try { info.BudgetUpper = decimal.Parse(TextBox_BudgetUpper.Text.Trim()); }
            catch { }
            info.DesignCompany = TextBox_DesignCompany.Text.Trim();
            info.ProjectName = TextBox_ProjectName.Text.Trim();
            try { info.TimeLower = DateTime.Parse(TextBox_TimeLower.Text.Trim()); }
            catch { }
            try { info.TimeUpper = DateTime.Parse(TextBox_TimeUpper.Text.Trim()); }
            catch { }


            IList list = specialProjectBll.SearchSpecialProject(info, pageIndex, AspNetPager1.PageSize, out listCount);
            AspNetPager1.RecordCount = listCount;

            gridview_ProjectList.DataSource = list;
            gridview_ProjectList.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "读取信息列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }


    /// <summary>
    /// 点击查找按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Query_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        TabContainer1.ActiveTabIndex = 0;
        FillData();
    }
}
