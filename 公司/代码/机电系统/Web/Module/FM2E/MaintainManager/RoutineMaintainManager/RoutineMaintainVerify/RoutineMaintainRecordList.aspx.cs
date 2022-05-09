using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WebUtility;
using WebUtility.Components;
using WebUtility.WebControls;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.WorkflowLayer;
using FM2E.Model.Maintain;
using FM2E.BLL.Maintain;

public partial class Module_FM2E_MaintainManager_RoutineMaintainManager_RoutineMaintainVerify_RoutineMaintainRecordList : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            FillData2();
        }
    }
    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        try
        {

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 初始化Repeater等数据
    /// </summary>
    private void FillData()
    {
        try
        {
            MaintainPlanRecord bll = new MaintainPlanRecord();
            MaintainPlanRecordInfo info = new MaintainPlanRecordInfo();
            info.VerifiedResult = MaintainPlanVerifiedResult.NotVerified;
            Company companybll = new Company();
            if (UserData.CurrentUserData.IsParentCompany)
                info.CompanyID = "";
            else
                info.CompanyID = UserData.CurrentUserData.CompanyID;
            int listCount = 0;
            info.PlanType = MaintainPlanType.RoutineMaintain;
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
    private void FillData2()
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
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }
    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        FillData2();
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        MaintainPlanRecordInfo recordItem = (MaintainPlanRecordInfo)e.Item.DataItem;
        MaintainPlanConfig bll = new MaintainPlanConfig();
        MaintainPlanConfigInfo info = bll.GetMaintainPlanConfig(recordItem.ItemID);
        Label lb = (Label)e.Item.FindControl("lbSystem");
        lb.Text = info.SystemName;
        lb = (Label)e.Item.FindControl("lbSubsystem");
        lb.Text = info.SubsystemName;
        lb = (Label)e.Item.FindControl("lbRecordObject");
        lb.Text = info.PlanObject;
        lb = (Label)e.Item.FindControl("lbRecordPeriod");
        lb.Text = info.PlanPeriodString;
        lb = (Label)e.Item.FindControl("lbRecordContent");
        lb.Text = info.PlanContent;
        lb = (Label)e.Item.FindControl("lbCheckStandard");
        lb.Text = info.CheckStandard;
    }
    protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        MaintainPlanRecordInfo recordItem = (MaintainPlanRecordInfo)e.Item.DataItem;
        MaintainPlanConfig bll = new MaintainPlanConfig();
        MaintainPlanConfigInfo info = bll.GetMaintainPlanConfig(recordItem.ItemID);
        Label lb = (Label)e.Item.FindControl("lbSystem");
        lb.Text = info.SystemName;
        lb = (Label)e.Item.FindControl("lbSubsystem");
        lb.Text = info.SubsystemName;
        lb = (Label)e.Item.FindControl("lbRecordObject");
        lb.Text = info.PlanObject;
        lb = (Label)e.Item.FindControl("lbRecordPeriod");
        lb.Text = info.PlanPeriodString;
        lb = (Label)e.Item.FindControl("lbRecordContent");
        lb.Text = info.PlanContent;
        lb = (Label)e.Item.FindControl("lbCheckStandard");
        lb.Text = info.CheckStandard;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;
        try
        {
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                HtmlInputCheckBox cb = (HtmlInputCheckBox)Repeater1.Items[i].FindControl("Checkbox1");
                if (cb.Checked)
                {
                    Literal lt = (Literal)Repeater1.Items[i].FindControl("ltRecordID");
                    string recordID = lt.Text;
                    MaintainPlanRecord bll = new MaintainPlanRecord();
                    MaintainPlanRecordInfo info = bll.GetMaintainPlanRecord(Convert.ToInt64(recordID));
                    RadioButtonList bl = (RadioButtonList)Repeater1.Items[i].FindControl("RadioButtonList1");
                    TextBox tb = (TextBox)Repeater1.Items[i].FindControl("tbVerifyRemark");
                    info.VerifiedResult = bl.SelectedIndex == 0 ? MaintainPlanVerifiedResult.CompletedAsPlanned : MaintainPlanVerifiedResult.NotCompleted;
                    info.VerifyRemark = tb.Text.Trim();
                    info.VerifyBy = Common.Get_UserName;
                    bll.UpdateMaintainPlanRecord(info);
                }
            }
            bSuccess = true;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "审核失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        if (bSuccess)
        {
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "审核成功", Icon_Type.OK, true, Common.GetHomeBaseUrl("RoutineMaintainRecordList.aspx"), UrlType.Href, "");
        }
    }
}
