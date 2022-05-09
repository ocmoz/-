using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using FM2E.BLL.Basic;
using FM2E.Model.Maintain;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.WorkflowLayer;
using FM2E.BLL.Maintain;
using WebUtility.Components;
using WebUtility;

public partial class Module_FM2E_MaintainManager_MaintainStatistic_MaintainStatistic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
        }
    }
    /// <summary>
    /// 初始化页面
    /// </summary>

    private void InitialPage()
    {
        TabContainer1.Tabs[1].Visible = false;
        TabContainer1.Tabs[2].Visible = false;
        TabContainer1.Tabs[3].Visible = false;
    }
    /// <summary>
    /// 查询按钮的响应
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FillData1();
            Filldata2();
            FillData3();
            TabContainer1.Tabs[1].Visible = true;
            TabContainer1.Tabs[2].Visible = true;
            TabContainer1.Tabs[3].Visible = true;
            TabContainer1.ActiveTabIndex = 1;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        MaintainPlanRecordInfo recordItem = (MaintainPlanRecordInfo)e.Item.DataItem;
        MaintainPlanConfig bll = new MaintainPlanConfig();
        MaintainPlanConfigInfo info = bll.GetMaintainPlanConfig(recordItem.ItemID);
        Label lb = (Label)e.Item.FindControl("lbPatrolSystem");
        lb.Text = info.SystemName;
        lb = (Label)e.Item.FindControl("lbPatrolSubsystem");
        lb.Text = info.SubsystemName;
        lb = (Label)e.Item.FindControl("lbPatrolPlanObject");
        lb.Text = info.PlanObject;
        lb = (Label)e.Item.FindControl("lbPatrolPlanPeriod");
        lb.Text = info.PlanPeriodString;
        lb = (Label)e.Item.FindControl("lbPatrolPlanContent");
        lb.Text = info.PlanContent;
        lb = (Label)e.Item.FindControl("lbPatrolCheckStandard");
        lb.Text = info.CheckStandard;
    }

    protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        MaintainPlanRecordInfo recordItem = (MaintainPlanRecordInfo)e.Item.DataItem;
        MaintainPlanConfig bll = new MaintainPlanConfig();
        MaintainPlanConfigInfo info = bll.GetMaintainPlanConfig(recordItem.ItemID);
        Label lb = (Label)e.Item.FindControl("lbMaintainSystem");
        lb.Text = info.SystemName;
        lb = (Label)e.Item.FindControl("lbMaintainSubsystem");
        lb.Text = info.SubsystemName;
        lb = (Label)e.Item.FindControl("lbMaintainPlanObject");
        lb.Text = info.PlanObject;
        lb = (Label)e.Item.FindControl("lbMaintainPlanPeriod");
        lb.Text = info.PlanPeriodString;
        lb = (Label)e.Item.FindControl("lbMaintainPlanContent");
        lb.Text = info.PlanContent;
        lb = (Label)e.Item.FindControl("lbMaintainCheckStandard");
        lb.Text = info.CheckStandard;
    }

    protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        MaintainPlanRecordInfo recordItem = (MaintainPlanRecordInfo)e.Item.DataItem;
        MaintainPlanConfig bll = new MaintainPlanConfig();
        MaintainPlanConfigInfo info = bll.GetMaintainPlanConfig(recordItem.ItemID);
        Label lb = (Label)e.Item.FindControl("lbInspectSystem");
        lb.Text = info.SystemName;
        lb = (Label)e.Item.FindControl("lbInspectSubsystem");
        lb.Text = info.SubsystemName;
        lb = (Label)e.Item.FindControl("lbInspectPlanObject");
        lb.Text = info.PlanObject;
        lb = (Label)e.Item.FindControl("lbInspectPlanPeriod");
        lb.Text = info.PlanPeriodString;
        lb = (Label)e.Item.FindControl("lbInspectPlanContent");
        lb.Text = info.PlanContent;
        lb = (Label)e.Item.FindControl("lbInspectCheckStandard");
        lb.Text = info.CheckStandard;
    }
    private void FillData1()
    {
        try
        {
            MaintainPlanRecord bll = new MaintainPlanRecord();
            MaintainPlanRecordInfo info = new MaintainPlanRecordInfo();
            info.VerifiedResult = MaintainPlanVerifiedResult.Verified;
            Company companybll = new Company();
            if (UserData.CurrentUserData.IsParentCompany) 
                info.CompanyID = "";
            else
                info.CompanyID = UserData.CurrentUserData.CompanyID;
            int listCount = 0;
            info.PlanType = MaintainPlanType.DailyPatrol;
            QueryParam searchTerm = bll.GenerateSearchTerm(info);
            searchTerm.PageIndex = AspNetPager1.CurrentPageIndex;
            IList list = bll.GetList(searchTerm, out listCount);

            AspNetPager1.RecordCount = listCount;
            Repeater1.DataSource = list;
            Repeater1.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    private void Filldata2()
    {
        try
        {
            MaintainPlanRecord bll = new MaintainPlanRecord();
            MaintainPlanRecordInfo info = new MaintainPlanRecordInfo();
            info.VerifiedResult = MaintainPlanVerifiedResult.Verified;
            Company companybll = new Company();
            if (UserData.CurrentUserData.IsParentCompany)
                info.CompanyID = "";
            else
                info.CompanyID = UserData.CurrentUserData.CompanyID;
            int listCount = 0;
            info.PlanType = MaintainPlanType.RoutineMaintain;
            QueryParam searchTerm = bll.GenerateSearchTerm(info);
            searchTerm.PageIndex = AspNetPager2.CurrentPageIndex;
            IList list = bll.GetList(searchTerm, out listCount);

            AspNetPager2.RecordCount = listCount;
            Repeater2.DataSource = list;
            Repeater2.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    private void FillData3()
    {
        try
        {
            MaintainPlanRecord bll = new MaintainPlanRecord();
            MaintainPlanRecordInfo info = new MaintainPlanRecordInfo();
            info.VerifiedResult = MaintainPlanVerifiedResult.Verified;
            Company companybll = new Company();
            if (UserData.CurrentUserData.IsParentCompany)
                info.CompanyID = "";
            else
                info.CompanyID = UserData.CurrentUserData.CompanyID;
            int listCount = 0;
            info.PlanType = MaintainPlanType.RoutineInspect;
            QueryParam searchTerm = bll.GenerateSearchTerm(info);
            searchTerm.PageIndex = AspNetPager3.CurrentPageIndex;
            IList list = bll.GetList(searchTerm, out listCount);

            AspNetPager3.RecordCount = listCount;
            Repeater3.DataSource = list;
            Repeater3.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData1();
    }
    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Filldata2();
    }
    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager3_PageChanged(object sender, EventArgs e)
    {
        FillData3();
    }
}
