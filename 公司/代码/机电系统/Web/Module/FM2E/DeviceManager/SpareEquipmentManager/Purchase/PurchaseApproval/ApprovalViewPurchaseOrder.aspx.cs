using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;

using FM2E.Model.Basic;
using FM2E.Model.System;
using FM2E.Model.Equipment;
using FM2E.Model.Exceptions;
using FM2E.BLL.Message;

using FM2E.BLL.Basic;
using FM2E.BLL.System;
using FM2E.BLL.Equipment;
using FM2E.WorkflowLayer;
using FM2E.BLL.Utils;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseApproval_ApprovalViewPurchaseOrder : System.Web.UI.Page
{
    /// <summary>
    /// 采购管理业务逻辑处理类对象
    /// </summary>
    Purchase purchaseBll = new Purchase();
    /// <summary>
    /// 需要查看的采购单ID
    /// </summary>
    protected long id = (long)Common.sink("id", MethodType.Get, 0, 0, DataType.Long);
    /// <summary>
    /// 使用SESSION的名称
    /// </summary>
    string sessionName = "Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseApproval_ApprovalViewPurchaseOrder";

    /// <summary>
    /// 是否可以被调整
    /// </summary>
    protected string CanAdjust = "";

    /// <summary>
    /// 加载页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        Session[sessionName] = null;
        //审批的下拉列表
        //DropDownList_ApprovalResult.Items.Clear();
        //Array array = Enum.GetValues(typeof(PurchaseOrderApprovalResult));
        //foreach (PurchaseOrderApprovalResult item in array)
        //{
        //    switch (item)
        //    {
        //        case PurchaseOrderApprovalResult.NOTPASS:
        //            DropDownList_ApprovalResult.Items.Add(new ListItem("不通过", ((int)item).ToString()));
        //            break;
        //        case PurchaseOrderApprovalResult.PASS:
        //            DropDownList_ApprovalResult.Items.Add(new ListItem("通过", ((int)item).ToString()));
        //            break;
        //        case PurchaseOrderApprovalResult.RETURNANDMODIFY:
        //            DropDownList_ApprovalResult.Items.Add(new ListItem("返回修改", ((int)item).ToString()));
        //            break;
        //        default:
        //            break;
        //    }
        //}
        PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName];
        if (order == null)
        {
            order = purchaseBll.GetPurchaseOrderByID(id);
            Session[sessionName] = order;
        }

        //工作流控件
        WorkFlowUserSelectControl1.EventIDField = "Name";
        WorkFlowUserSelectControl1.EventNameField = "Description";
        WorkFlowUserSelectControl1.WorkFlowState = order.WorkFlowStateName;
        WorkFlowUserSelectControl1.WorkFlowName = PurchaseWorkflow.WorkflowName;
        WorkFlowUserSelectControl1.EventListDataSource = WorkflowHelper.GetEventInfoList(PurchaseWorkflow.WorkflowName, order.WorkFlowStateName);
        WorkFlowUserSelectControl1.EventListDataBind();
        WorkFlowUserSelectControl1.ShowCompanySelect = true;
        WorkFlowUserSelectControl1.SelectedCompanyID = UserData.CurrentUserData.CompanyID;
        WorkFlowUserSelectControl1.SelectedDepartmentID = UserData.CurrentUserData.DepartmentID;
        WorkFlowUserSelectControl1.AddShowSelectDivValue(PurchaseWorkflow.LeaderApprovalEvent);

        FillData();
    }


    private bool canEdit = false;

    /// <summary>
    /// 往对应的控件填入数据
    /// </summary>
    private void FillData()
    {

        PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName];
        if (order == null)
        {
            order = purchaseBll.GetPurchaseOrderByID(id);
            Session[sessionName] = order;
        }
        //公司ID
        CompanyInfo company = new Company().GetCompany(order.CompanyID);
        if (company != null)
            Label_CompanyName.Text = company.CompanyName;

        Label_OrderName.Text = order.PurchaseOrderName;
        Label_OrderID.Text = order.PurchaseOrderID + "-" + order.SubOrderIndex;
        Label_Remark.Text = order.Remark;
       // Label_Applicant.Text = order.Applicant;
        Label_Status.Text = order.WorkFlowStateDescription;
        Label_ApplicantName.Text = order.ApplicantName;
        Label_SubmitTime.Text = order.SubmitTime == DateTime.MinValue ? "" : order.SubmitTime.ToString("yyyy-MM-dd HH:mm");
        if (order.Status == PurchaseOrderStatus.APPROVALING || order.Status == PurchaseOrderStatus.REAPPROVALING)
        {
            Label_Approvaling.Text = "(" + ((order.Approvaling==null||order.Approvaling=="")?"等待上一级审批":order.Approvaling) + ")";
            Label_Approvaling.Visible = true;
            Button_GetApproval.Visible = false;
        }
        else
        {
            Label_Approvaling.Visible = false;
            if (order.Status == PurchaseOrderStatus.WAITING4APPROVAL)//等待审批的话，可以获取审批
            {
                Button_GetApproval.Visible = true;
            }
        }
        //如果是自己正在审批的话，可以放弃审批
        if (order.Approvaling == Common.Get_UserName)
        {
            if(order.Status== PurchaseOrderStatus.APPROVALING)//只有从来没有被返回的表单，才能被放弃审批
                Button_ReleaseApproval.Visible = true;
            else
                Button_ReleaseApproval.Visible = false;
            Table_Approval.Visible = true;//当前审批者是自己，则可以显示审批表格
        }
        else//不是自己在审批的，统一不显示
        {
            Button_ReleaseApproval.Visible = false;
            Table_Approval.Visible = false;
        }

        Button_ReleaseApproval.Visible = false;
        Button_GetApproval.Visible = false;
        Table_Approval.Visible = true;

        CanAdjust = "true";// order.CanBeAdjust(Common.Get_UserName).ToString();

        

        IList list = order.DetailList;
        if (list == null)
        {
            list = new List<PurchaseOrderDetailInfo>();
        }
        gridview_ItemList.DataSource = list;
        gridview_ItemList.DataBind();

        gridview_ApprovalRecord.DataSource = order.ApprovalList;
        gridview_ApprovalRecord.DataBind();

        gridview_ModifyRecord.DataSource = order.ModifyRecordSubmitList;
        gridview_ModifyRecord.DataBind();

        gridview_RelatedOrders.DataSource = order.RelatedOrders;
        gridview_RelatedOrders.DataBind();

        canEdit = order.CanEdit;

        
    }


    /// <summary>
    /// 数据绑定事件，主要处理footer合计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private decimal totalAmount = 0;//每次postback都会自动初始化
    private decimal totalAdjustAmount = 0;
    protected void gridview_ItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PurchaseOrderDetailInfo item = e.Row.DataItem as PurchaseOrderDetailInfo;
            totalAmount += item.PlanAmount;
            totalAdjustAmount += item.AdjustAmount;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "合计";
            e.Row.Cells[0].ColumnSpan = 6;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            for (int i = 1; i <= 5; i++)
            {
                e.Row.Cells[i].Visible = false;
            }
            Label LabelTotal = e.Row.FindControl("Label_TotalAmount") as Label;
            Label LabelTotalAdjust = e.Row.FindControl("Label_AdjustTotalAmount") as Label;
            if (LabelTotal != null)
            {
                LabelTotal.Text = totalAmount.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
                
            }
            if (LabelTotalAdjust != null)
            {
                LabelTotalAdjust.Text += totalAdjustAmount.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }

        }

    }

    /// <summary>
    /// 获取审批权限
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_GetApproval_Click(object sender, EventArgs e)
    {
        PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName];
        if (order == null)
            order = purchaseBll.GetPurchaseOrderByID(id);
        if (order == null)//判断当前是否有其他人已经提起审批
        {
            
            EventMessage.MessageBox(Msg_Type.Error, "进入审批失败", "申购单已经被删除，请刷新后重试",
                Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }

        if (order.Status != PurchaseOrderStatus.WAITING4APPROVAL)
        {
            EventMessage.MessageBox(Msg_Type.Error, "进入审批失败", "申购单已经进入"+order.Approvaling+"的审批流程，请刷新后重试",
                Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }
        //自己挂起要审批
        order.Approvaling = Common.Get_UserName;
        order.Status = PurchaseOrderStatus.APPROVALING;
        purchaseBll.UpdatePurchaseOrderNoDetail(order);
        Session[sessionName] = order;//重新获取的一个对象
        FillData();

        Table_Approval.Visible = true;
    }
    /// <summary>
    /// 放弃审批
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_ReleaseApproval_Click(object sender, EventArgs e)
    {
        PurchaseOrderInfo order = purchaseBll.GetPurchaseOrderByID(id);
        //放弃审批
        order.Approvaling = null;
        order.Status = PurchaseOrderStatus.WAITING4APPROVAL;
        purchaseBll.UpdatePurchaseOrderNoDetail(order);

        Session[sessionName] = order;//重新获取的一个对象

        FillData();

        Table_Approval.Visible = false;
        TextBox_ApprovalRemark.Text = "";
        //DropDownList_ApprovalResult.SelectedIndex = 0;
        Span_Adjust.InnerHtml = "";
    }

    /// <summary>
    /// 审批项
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_ApprovalItem_Click(object sender, EventArgs e)
    {
        string returnVal = Hidden_ApprovalItem.Value;
        string[] vals = returnVal.Split('|');
        short itemID = short.Parse(vals[0]);
        decimal count = decimal.Parse(vals[1]);
        decimal price = decimal.Parse(vals[2]);

        PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName];
        if (order == null)
            order = purchaseBll.GetPurchaseOrderByID(id);
        string adjust = "";
        int adjustIndex = 1;
        foreach (PurchaseOrderDetailInfo item in order.DetailList)
        {
            if (item.ItemID == itemID)
            {
                item.AdjustCount = count;
                item.AdjustPrice = price;
            }
            //生成调整意见
            if (item.AdjustPrice != item.BeforeAdjustPrice|| item.AdjustCount!=item.BeforeAdjustCount)
            {
                if (adjustIndex != 1)
                    adjust += "<br/>";
                adjust += adjustIndex.ToString() + "： (" +item.ItemID+") "+ item.ProductName + "[品牌：" + item.Model + "]" + "数量：" + item.BeforeAdjustCount.ToString("#,0.##") +
                    "-->" + item.AdjustCount.ToString("#,0.##") + "  单价：" + item.BeforeAdjustPrice.ToString("#,0.##") + "-->" + item.AdjustPrice.ToString("#,0.##");
                adjustIndex++;
            }
        }
        Span_Adjust.InnerHtml = adjust;
        Session[sessionName] = order;
        FillData();
    }

    /// <summary>
    /// 执行审批操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_DoApproval_Click(object sender, EventArgs e)
    {
        PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName];

        //if (order.Approvaling.ToLower() != Common.Get_UserName.ToLower())
        //{
        //    EventMessage.MessageBox(Msg_Type.Error, "审批失败", "您已经审批过该单，或者无权审批，请重试。",
        //        Icon_Type.Error, true, Common.GetHomeBaseUrl("ApprovalViewPurchaseOrder.aspx?id="+id), UrlType.Href, "");
        //    return;
        //}

        if (!WorkFlowUserSelectControl1.ProperlySelected)
        {
            EventMessage.MessageBox(Msg_Type.Error, "审批失败", "未能选择正确的审批结果",
                Icon_Type.Alert, false, "history.go(-1);", UrlType.JavaScript, "");
            return;
        }



        if (order == null)
            order = purchaseBll.GetPurchaseOrderByID(id);
        PurchaseOrderApprovalInfo record = new PurchaseOrderApprovalInfo();
        record.OrderSn = order.ID;
        record.CompanyID = order.CompanyID;
        record.ApprovalDate = DateTime.Now;
        record.Approvaler = Common.Get_UserName;
        record.FeeBack = TextBox_ApprovalRemark.Text.Trim();
        record.PurchaseOrderID = order.PurchaseOrderID;

        string v = "";
        string evt = WorkFlowUserSelectControl1.SelectedEvent;
        if (evt == PurchaseWorkflow.DoApprovalFailedEvent)
        {
            v = "3";
        }
        if (evt == PurchaseWorkflow.ApprovalReturnModifyEvent)
        {
            v = "2";
        }
        if (evt == PurchaseWorkflow.ApprovalPassEvent || evt==PurchaseWorkflow.LeaderApprovalEvent)
        {
            v = "1";
        }

        record.Result = (PurchaseOrderApprovalResult)Enum.Parse(typeof(PurchaseOrderApprovalResult),v);
        
        record.ApprovalLog = Span_Adjust.InnerHtml;
        record.SubOrderIndex = order.SubOrderIndex;
        try
        {
            purchaseBll.DoApproval(order, record);

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

            Guid guid = new Guid(order.WorkFlowInstanceID);
            WorkflowHelper.SetStateMachine<PurchaseEventService>(guid, WorkFlowUserSelectControl1.SelectedEvent);
            if(evt==PurchaseWorkflow.ApprovalReturnModifyEvent)
                WorkflowHelper.UpdateNextUserID(guid,order.Applicant,null);
            else
                WorkflowHelper.UpdateNextUserID(guid,WorkFlowUserSelectControl1.NextUserName,WorkFlowUserSelectControl1.DelegateUserName);
        }
        catch(Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "审批失败", "发生未知错误，请重试。",ex,
               Icon_Type.Error, true, Common.GetHomeBaseUrl("ApprovalViewPurchaseOrder.aspx?id=" + id), UrlType.Href, "");
            return;
        }

        if (evt == PurchaseWorkflow.LeaderApprovalEvent)
        {
            long thisID = order.ID;
            string title = "采购申请：" + order.PurchaseOrderID + "-" + order.SubOrderIndex + " " + order.PurchaseOrderName + "材料申购单" + WorkFlowUserSelectControl1.SelectedEventName;
            string URL = "../DeviceManager/SpareEquipmentManager/Purchase/PurchaseCenter/DeliveryPurchaseOrder.aspx?id=" + thisID + "&cmd=history";
            try
            {
                WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName,
                    UserData.CurrentUserData.PersonName, URL, 0, null, WorkFlowUserSelectControl1.NextUserName,WorkFlowUserSelectControl1.DelegateUserName
                    );
            }
            catch (Exception ex)
            {
                EventMessage.EventWriteLog(Msg_Type.Error, "工作流发送待办事务错误" + ex.Message);
            }
        }
        if (evt == PurchaseWorkflow.ApprovalReturnModifyEvent)
        {
            long thisID = order.ID;
            string title = "采购申请：" + order.PurchaseOrderID + "-" + order.SubOrderIndex + " " + order.PurchaseOrderName + "材料申购单" + WorkFlowUserSelectControl1.SelectedEventName;
            string URL = "../DeviceManager/SpareEquipmentManager/Purchase/PurchaseApply/PurchaseApply.aspx?id=" + thisID + "&cmd=history";
            try
            {
                WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 1, null, order.Applicant);
            }
            catch (Exception ex)
            {
                EventMessage.EventWriteLog(Msg_Type.Error, "工作流发送待办事务错误" + ex.Message);
            }
        }
        if (evt == PurchaseWorkflow.DoApprovalFailedEvent)
        {
            long thisID = order.ID;
            string title = "采购申请：" + order.PurchaseOrderID + "-" + order.SubOrderIndex + " " + order.PurchaseOrderName + "材料申购单" + WorkFlowUserSelectControl1.SelectedEventName;
            string URL = "../DeviceManager/SpareEquipmentManager/Purchase/PurchaseApply/PurchaseApply.aspx?id=" + thisID + "&cmd=history";
            try
            {
                FM2E.Model.Message.MessageInfo msg = new FM2E.Model.Message.MessageInfo();
                msg.Title = title;
                msg.Message = title + "\n"+"可以从已完成的申购单中找回该申购单";
                msg.Type = 0;
                msg.SendWay = 0;
                msg.SendFrom = Common.Get_UserName;
                msg.SenderPersonName = UserData.CurrentUserData.PersonName;
                msg.ReceiverAddress = order.Applicant;
                new Message().SendMessage(msg);
            }
            catch (Exception ex)
            {
                EventMessage.EventWriteLog(Msg_Type.Error, "工作流发送待办事务错误" + ex.Message);
            }
        }

        order.ApprovalList.Insert(0, record);
        Session[sessionName] = order;
        FillData();
        Response.Redirect("PurchaseApproval.aspx");
    }
}
