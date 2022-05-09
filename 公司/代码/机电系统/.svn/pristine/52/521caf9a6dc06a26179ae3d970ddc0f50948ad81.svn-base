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
using FM2E.Model.Archives;
using FM2E.BLL.Archives;
using FM2E.Model.Workflow;

public partial class Module_FM2E_ArchivesManager_ArchivesDestroyApply_ArchivesDestroyApproval_ArchivesDestroyApplyApproval : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    private IList detailList = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
        }
    }
    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        try
        {
            DropDownList1.Items.Clear();
            List<WorkflowEventInfo> list = WorkflowHelper.GetEventInfoList(ArchivesDestroyWorkflow.WorkflowName, ArchivesDestroyWorkflow.WaitManagerApproveState);
            DropDownList1.DataTextField = "Description";
            DropDownList1.DataValueField = "Name";
            DropDownList1.DataSource = list;
            DropDownList1.DataBind();

            ArchivesDestroyApply bll = new ArchivesDestroyApply();
            ArchivesDestroyApplyInfo item = bll.GetArchivesDestroyApply(id);
            Label1.Text = item.SheetNo;
            Label2.Text = item.ApplyDate.ToString();
            Label3.Text = item.ApplicantName;
            Label4.Text = item.ApplicantDeptName;
            Label5.Text = item.DestroyReason;
            Label7.Text = item.Remark;

            Label9.Text = item.StatusString;
            detailList = item.ApplyDetailList;
            FillData();
            AspNetPager1.CurrentPageIndex = 1;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败",ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 初始化GridView等数据
    /// </summary>
    private void FillData()
    {
        try
        {
            if (detailList != null)
            {
                ArrayList list = (ArrayList)detailList;
                int min = (AspNetPager1.CurrentPageIndex - 1) * 10;
                int max = (AspNetPager1.CurrentPageIndex * 10) > list.Count ? list.Count : AspNetPager1.CurrentPageIndex * 10;
                max = max - 1;
                ArrayList thisList = list.GetRange(min, max - min + 1);
                AspNetPager1.RecordCount = list.Count;
                GridView1.DataSource = thisList;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 审批按钮的响应
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;

        ArchivesDestroyApply bll = new ArchivesDestroyApply();
        ArchivesDestroyApplyInfo item = bll.GetArchivesDestroyApply(id);
        item.Approvaler = Common.Get_UserName;
        item.ApprovalDate = DateTime.Now;
        item.ApprovalOpinion = TextArea1.Value.Trim();
        item.ApplyStatus = DropDownList1.SelectedValue == ArchivesDestroyWorkflow.ManagerApprovedEvent ? ArchivesDestroyApplyStatus.ApprovalPassed : ArchivesDestroyApplyStatus.ApprovalFailed;
        try
        {
            //此处应用工作流
            WorkflowInstanceInfo info = WorkflowHelper.GetWorkflowInstanceInfo(ArchivesDestroyWorkflow.TableName, item.ID);
            WorkflowHelper.SetStateMachine<ArchivesDestroyEventService>(info.InstanceID, DropDownList1.SelectedValue);
            bll.UpdateArchivesDestroyApply(item);
            bSuccess = true;

            //**********Modified by Xue 2011-6-27****************************************************************************************************
            FM2E.BLL.PendingOrder.PendingOrder pobll = new FM2E.BLL.PendingOrder.PendingOrder();
            string lastURL = Request.Url.AbsolutePath + "?" + Request.QueryString.ToString();
            if (lastURL.Contains("/Web/Module/FM2E"))
            {
                lastURL = lastURL.Replace("/Web/Module/FM2E", "..");
            }
            if (lastURL.Contains("/Module/FM2E"))
            {
                lastURL = lastURL.Replace("/Module/FM2E", "..");
            }
            pobll.MarkReadByURL(lastURL);
            //**********Modification Finished 2011-6-27**********************************************************************************************

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败","审批失败" ,ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        if (bSuccess)
        {
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "审批成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ArchivesDestroyApply.aspx"), UrlType.Href, "");
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
}
