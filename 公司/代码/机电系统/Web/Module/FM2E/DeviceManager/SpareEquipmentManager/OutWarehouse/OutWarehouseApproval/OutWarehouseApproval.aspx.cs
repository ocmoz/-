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
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.WorkflowLayer;
using FM2E.Model.Workflow;
using FM2E.BLL.Utils;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_OutWarehouse_OutWarehouseApproval_OutWarehouseApproval : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    //private IList detailList = null;
    OutWarehouse bll = new OutWarehouse();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            //FillData();
        }
    }

    private OutWarehouseApplyInfo CurrentOutWarehouseApplyInfo
    {
        get
        {
            OutWarehouseApplyInfo item = (OutWarehouseApplyInfo)Session[this.ToString()];
            if (item == null)
            {
                if (cmd == "edit")
                {
                    item = bll.GetOutWarehouseApplyInfo(id);
                    Session[this.ToString()] = item;
                }
                if (cmd == "new")
                {
                    item = new OutWarehouseApplyInfo();
                    Session[this.ToString()] = item;
                }
            }
            return item;
        }
        set
        {
            Session[this.ToString()] = value;
        }
    }

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        try
        {
            
            OutWarehouseApplyInfo item = bll.GetOutWarehouseApplyInfo(id);

            CurrentOutWarehouseApplyInfo = item;
            Label_ApplicantName.Text = item.ApplicantName;
            Label_ApplyRemark.Text = item.ApplyRemark;
            Label_ApplyTime.Text = item.OutTime == DateTime.MinValue ? "" : item.ApplyTime.ToString("yyyy-MM-dd HH:mm");
            Label_OperatorName.Text = item.OperatorName;
            Label_OutTime.Text = item.OutTime == DateTime.MinValue ? "" : item.OutTime.ToString("yyyy-MM-dd HH:mm");
            Label_ReceiverName.Text = item.ReceiverName;
            Label_SheetName.Text = item.SheetName;
            Label_Status.Text = item.WorkFlowStateDescription;
            Label_WarehouseName.Text = item.WarehouseName;
            Label_WarehouseRemark.Text = item.OutWarehouseRemark;

            GridView_ApplyDetail.DataSource = item.ApplyDetailList;
            GridView_ApplyDetail.DataBind();

            gridview_ApprovalRecord.DataSource = item.ApprovalList;
            gridview_ApprovalRecord.DataBind();

            //工作流控件
            WorkFlowUserSelectControl1.EventIDField = "Name";
            WorkFlowUserSelectControl1.EventNameField = "Description";
            WorkFlowUserSelectControl1.WorkFlowState = item.WorkFlowStateName;
            WorkFlowUserSelectControl1.WorkFlowName = OutWarehouseWorkflow.WorkflowName;
            WorkFlowUserSelectControl1.EventListDataSource = WorkflowHelper.GetEventInfoList(OutWarehouseWorkflow.WorkflowName, item.WorkFlowStateName);
            WorkFlowUserSelectControl1.EventListDataBind();
            WorkFlowUserSelectControl1.ShowCompanySelect = false;
            WorkFlowUserSelectControl1.SelectedCompanyID = UserData.CurrentUserData.CompanyID;
            WorkFlowUserSelectControl1.SelectedDepartmentID = UserData.CurrentUserData.DepartmentID;
            WorkFlowUserSelectControl1.AddShowSelectDivValue(OutWarehouseWorkflow.SubmitNewEvent);
            WorkFlowUserSelectControl1.AddShowSelectDivValue(OutWarehouseWorkflow.SubmitDraftEvent);
            WorkFlowUserSelectControl1.AddShowSelectDivValue(OutWarehouseWorkflow.SubmitReturnModifyEvent);
            WorkFlowUserSelectControl1.AddShowSelectDivValue(OutWarehouseWorkflow.LeaderApprovalEvent);

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    ///// <summary>
    ///// 初始化GridView等数据
    ///// </summary>
    //private void FillData()
    //{
    //    try
    //    {
    //        if (detailList != null)
    //        {
    //            ArrayList list = (ArrayList)detailList;
    //            int min = (AspNetPager1.CurrentPageIndex - 1) * 10;
    //            int max = (AspNetPager1.CurrentPageIndex * 10) > list.Count ? list.Count : AspNetPager1.CurrentPageIndex * 10;
    //            max = max - 1;
    //            ArrayList thisList = list.GetRange(min, max - min + 1);
    //            AspNetPager1.RecordCount = list.Count;
    //            GridView1.DataSource = thisList;
    //            GridView1.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
    //    }
    //}
    /// <summary>
    /// 审批按钮的响应
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;

        OutWarehouseApplyInfo item = CurrentOutWarehouseApplyInfo;

        OutWarehouseApprovalInfo record = new OutWarehouseApprovalInfo();

        record.Approvaler = Common.Get_UserName;
        record.ApprovalTime = DateTime.Now;
        record.CompanyID = UserData.CurrentUserData.CompanyID;
        record.OutWarehouseApplyID = item.ID;
        record.Result = WorkFlowUserSelectControl1.SelectedEventName;
        record.FeedBack = TextArea1.Value.Trim();
        string title = "", URL = "";
        try
        {
            record.ID = bll.DoApproval(record);
            //此处应用工作流
            bll.SavaOutWarehouseApply(item);

            //**********Modified by Xue 2011-6-27****************************************************************************************************
            //FM2E.BLL.PendingOrder.PendingOrder pobll = new FM2E.BLL.PendingOrder.PendingOrder();
            //string lastURL = Request.Url.AbsolutePath + "?" + Request.QueryString.ToString();
            //if (lastURL.Contains("/Web/Module/FM2E"))
            //{
            //    lastURL = lastURL.Replace("/Web/Module/FM2E", "..");
            //}
            //if (lastURL.Contains("/Module/FM2E"))
            //{
            //    lastURL = lastURL.Replace("/Module/FM2E", "..");
            //}
            //pobll.MarkReadByURL(lastURL);
            //**********Modification Finished 2011-6-27**********************************************************************************************

            title = "你有新的设备出库申请" + item.SheetName + "待审批";
            URL = "../DeviceManager/SpareEquipmentManager/OutWarehouse/OutWarehouseApproval/OutWarehouseApply.aspx";

            Guid guid = new Guid(item.WorkFlowInstanceID);
            WorkflowHelper.SetStateMachine<OutWarehouseEventService>(guid, WorkFlowUserSelectControl1.SelectedEvent);
            WorkflowHelper.UpdateNextUserID(guid, WorkFlowUserSelectControl1.NextUserName, WorkFlowUserSelectControl1.DelegateUserName);

            //**********Modified by Xue 2011-6-27****************************************************************************************************
            if (WorkFlowUserSelectControl1.NextUserName == null || WorkFlowUserSelectControl1.NextUserName == "")
            {
                bSuccess = false;
                //EventMessage.MessageBox(Msg_Type.Info, "操作成功", "审批成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("OutWarehouseApply.aspx"), UrlType.Href, "");
            }
            else
            {
                //pobll.DeletePendingOrder(pobll.GetPendingOrderIDByURL(lastURL));

                bSuccess = true;
            }
            //---bSuccess = true;
            //**********Modification Finished 2011-6-27**********************************************************************************************
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "审批失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        if (bSuccess)
        {
            try
            {
                WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL,
                       0, null, WorkFlowUserSelectControl1.NextUserName, WorkFlowUserSelectControl1.DelegateUserName);
            }
            catch (Exception ex)
            {
                EventMessage.EventWriteLog(Msg_Type.Error, "工作流发送待办事务错误" + ex.Message);
            }

            
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", "审批成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("OutWarehouseApply.aspx"), UrlType.Href, "");
    }
    ///// <summary>
    ///// 分页事件
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    //{
    //    FillData();
    //}
    //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    //{

    //}
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";
            OutWarehouseDetailInfo item = (OutWarehouseDetailInfo)e.Row.DataItem;
            e.Row.Attributes["itemID"] = item.ItemID.ToString();
        }

    }
}
