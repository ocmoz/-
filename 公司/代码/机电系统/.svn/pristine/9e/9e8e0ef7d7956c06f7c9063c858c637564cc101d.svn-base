using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;

using FM2E.Model.Basic;
using FM2E.Model.System;
using FM2E.Model.Equipment;
using FM2E.Model.Exceptions;

using FM2E.BLL.Basic;
using FM2E.BLL.System;
using FM2E.BLL.Equipment;

using FM2E.BLL.Utils;
using FM2E.WorkflowLayer;


public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_ExecutePurchasing_Purchase : System.Web.UI.Page
{
    /// <summary>
    /// 采购管理业务逻辑处理类对象
    /// </summary>
    Purchase purchaseBll = new Purchase();
    /// <summary>
    /// 仓库业务逻辑处理类对象
    /// </summary>
    Warehouse warehouseBll = new Warehouse();

    /// <summary>
    /// 价格管理业务逻辑处理类对象
    /// </summary>
    PriceManager priceBll = new PriceManager();

    /// <summary>
    /// 员工业务逻辑处理类对象
    /// </summary>
    Warehouse staffBll = new Warehouse();

    string sessionName = "Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_ExecutePurchasing_Purchase";
    /// <summary>
    /// 需要查看的采购单ID
    /// </summary>
    protected long id = (long)Common.sink("id", MethodType.Get, 0, 0, DataType.Long);

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
        TextBox_Amount.Attributes.Add("readonly", "readonly");

      
        TextBox_Count.Attributes.Add("onchange", "javascript:onCountChange()");
        TextBox_UnitPrice.Attributes.Add("onchange", "javascript:onCountChange()");

        //读取仓库列表
        IList warehouseList = warehouseBll.GetAllWarehouseByCompany(UserData.CurrentUserData.CompanyID);
        DropDownList_Warehouse.DataSource = warehouseList;
        DropDownList_Warehouse.DataBind();
        DropDownList_Warehouse.Items.Insert(0, new ListItem("--请选择送验仓库--", ""));

        //单位
        DropDownList_Unit.DataSource = Constants.GetUnits();
        DropDownList_Unit.DataBind();

        DropDownList_Unit.Items.Insert(0, new ListItem("--请选择单位--", ""));

        FillData();
    }



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
        Label_OrderID.Text = order.PurchaseOrderID +"-" +order.SubOrderIndex;

        Label_Status.Text = order.WorkFlowStateDescription;

        Label_ApplicantName.Text = order.ApplicantName;
        Label_UpdateTime.Text = order.UpdateTime.ToString("yyyy-MM-dd HH:mm");

        Label_SubmitTime.Text = order.SubmitTime.ToString("yyyy-MM-dd HH:mm");
        //Label_Applicant.Text = order.Applicant;

        IList list = order.DetailList;
        if (list == null)
        {
            list = new List<PurchaseOrderDetailInfo>();
        }
        Repeater_ItemList.DataSource = list;
        Repeater_ItemList.DataBind();

        gridview_ApprovalRecord.DataSource = order.ApprovalList;
        gridview_ApprovalRecord.DataBind();

        gridview_ModifyRecord.DataSource = order.ModifyRecordSubmitList;
        gridview_ModifyRecord.DataBind();


    }


    /// <summary>
    /// 数据绑定事件，主要处理footer合计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //private decimal totalFinalAmount = 0;//每次postback都会自动初始化
    //private decimal totalActualAmount = 0;
    protected void Repeater_ItemList_RowDataBound(object sender,  RepeaterItemEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
            PurchaseOrderDetailInfo item = e.Item.DataItem as PurchaseOrderDetailInfo;
            //totalFinalAmount += item.FinalAmount;
            //totalActualAmount += item.ActualAmount;
            HtmlTableRow tr = e.Item.FindControl("tr_item") as HtmlTableRow;
            if (item.Purchaser == Common.Get_UserName)
            {
                tr.BgColor = "LightYellow";
                e.Item.FindControl("Label_Purchaser").Visible = false;
                switch (item.Status)
                {
                    case PurchaseOrderDetailStatus.WAITING4PURCHASE:
                        e.Item.FindControl("Button_GetPurchase").Visible = true;
                        e.Item.FindControl("Button_SendCheck").Visible = false;
                        e.Item.FindControl("Button_FinishPurchase").Visible = false;
                        e.Item.FindControl("Button_ReleasePurchase").Visible = false;
                        break;
                    case PurchaseOrderDetailStatus.PURCHASING:
                        e.Item.FindControl("Button_GetPurchase").Visible = false;
                        e.Item.FindControl("Button_SendCheck").Visible = true;
                        e.Item.FindControl("Button_FinishPurchase").Visible = true;
                        e.Item.FindControl("Button_ReleasePurchase").Visible = true;
                        break;
                    case PurchaseOrderDetailStatus.PURCHASINGFINISH:
                        e.Item.FindControl("Button_GetPurchase").Visible = false;
                        e.Item.FindControl("Button_SendCheck").Visible = false;
                        e.Item.FindControl("Button_FinishPurchase").Visible = false;
                        e.Item.FindControl("Button_ReleasePurchase").Visible = false;
                        break;
                    default:
                        e.Item.FindControl("Button_GetPurchase").Visible = false;
                        e.Item.FindControl("Button_SendCheck").Visible = false;
                        e.Item.FindControl("Button_FinishPurchase").Visible = false;
                        e.Item.FindControl("Button_ReleasePurchase").Visible = false;
                        break;

                }

            }
            else
            {
                tr.BgColor = "Transparent";
                e.Item.FindControl("Button_GetPurchase").Visible = false;
                e.Item.FindControl("Button_SendCheck").Visible = false;
                e.Item.FindControl("Button_FinishPurchase").Visible = false;
                e.Item.FindControl("Button_ReleasePurchase").Visible = false;
            }
        //}
        //else if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    e.Row.Cells[0].Text = "合计";
        //    e.Row.Cells[0].ColumnSpan = 6;
        //    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        //    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
        //    for (int i = 1; i <= 5; i++)
        //    {
        //        e.Row.Cells[i].Visible = false;
        //    }
        //    Label LabelFinalTotal = e.Row.FindControl("Label_FinalTotalAmount") as Label;
        //    Label LabelActualTotal = e.Row.FindControl("Label_ActualTotalAmount") as Label;
        //    if (LabelFinalTotal != null)
        //    {
        //        LabelFinalTotal.Text += totalFinalAmount.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
        //    }
        //    if (LabelActualTotal != null)
        //    {
        //        LabelActualTotal.Text += totalActualAmount.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
        //    }

        //}

    }

    /// <summary>
    /// 核实事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridview_PurchaseRecordList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != null && e.CommandName != "")
        {
            PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName];
            if (order == null)
            {
                order = purchaseBll.GetPurchaseOrderByID(id);
                Session[sessionName] = order;
            }
            long recordid = long.Parse(e.CommandArgument.ToString());
            PurchaseRecordInfo record = purchaseBll.GetPurchaseRecordInfo(recordid);
            switch (e.CommandName)
            {
                case "CONFIRM":
                    record.PurchaserConfirm = true;
                    record.PurchaserConfirmTime = DateTime.Now;
                    break;
                default:
                    break;
            }
            purchaseBll.SavePurchaseRecord(record);
            order = purchaseBll.GetPurchaseOrderByID(id);
            Session[sessionName] = order;
            FillData();
        }
    }

    /// <summary>
    /// 执行事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Repeater_ItemList_Command(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName != null && e.CommandName != "")
        {
            PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName];
            if (order == null)
            {
                order = purchaseBll.GetPurchaseOrderByID(id);
                Session[sessionName] = order;
            }
            short itemID = short.Parse(e.CommandArgument.ToString());
            PurchaseOrderDetailInfo targetItem = null;
            foreach (PurchaseOrderDetailInfo item in order.DetailList)
            {
                if (item.ItemID == itemID)
                {
                    targetItem = item;
                    break;
                }
            }
            switch (e.CommandName)
            {
                case "GET":
                    targetItem.Status = PurchaseOrderDetailStatus.PURCHASING;
                    break;
                case "RELEASE":
                    targetItem.Status = PurchaseOrderDetailStatus.WAITING4PURCHASE;
                    break;
                case "FINISH":
                    targetItem.Status = PurchaseOrderDetailStatus.PURCHASINGFINISH;
                    break;
                default:
                    break;
            }
            purchaseBll.UpdatePurchaseOrderDetail(targetItem);
            //int purchasingCount = 0;
            //int finishCount = 0;
            //int waitingCount = 0;
            //foreach (PurchaseOrderDetailInfo item in order.DetailList)
            //{
            //    switch (item.Status)
            //    {
            //        case PurchaseOrderDetailStatus.PURCHASING:
            //            purchasingCount++;
            //            break;
            //        case PurchaseOrderDetailStatus.PURCHASINGFINISH:
            //            finishCount++;
            //            break;
            //        case PurchaseOrderDetailStatus.WAITING4PURCHASE:
            //            waitingCount++;
            //            break;
            //        default:
            //            break;
            //    }
            //}

            //PurchasingStatus originalPurchasingStatus = order.PurchasingStatus;//保留原来的状态
            //PurchaseOrderStatus originalOrderStatus = order.Status;

            //if (waitingCount == order.DetailList.Count)//全部在等待
            //{
            //    order.PurchasingStatus = PurchasingStatus.ALLWAITING;
            //    order.Status = PurchaseOrderStatus.WAITING4PURCHASE;
            //}
            //if (finishCount == order.DetailList.Count)
            //{
            //    order.PurchasingStatus = PurchasingStatus.FINISH;
            //    order.Status = PurchaseOrderStatus.PURCHASINGFINISH;
            //}
            //if (purchasingCount > 0 && purchasingCount < order.DetailList.Count)
            //{
            //    order.PurchasingStatus = PurchasingStatus.PURCHASING;
            //    order.Status = PurchaseOrderStatus.PURCHASING;
            //}

            //if (originalPurchasingStatus != order.PurchasingStatus || originalOrderStatus != order.Status)
            //{
            //    purchaseBll.UpdatePurchaseOrderNoDetail(order);
            //}
            bool finish = true;
            //PurchaseOrderInfo order = purchaseBll.GetPurchaseOrderByID(id);
            foreach (PurchaseOrderDetailInfo detail in order.DetailList)
            {
                if (detail.Status != PurchaseOrderDetailStatus.PURCHASINGFINISH)
                {
                    finish = false;
                }
                foreach (PurchaseRecordInfo record in detail.PurchaseRecordList)
                {
                    if (record.Status != PurchaseRecordStatus.INWAREHOUSEFINISH)
                    {
                        finish = false;
                        break;
                    }
                }
                if (!finish)
                    break;
            }
            if (finish)
            {
                order.Status = PurchaseOrderStatus.INWAREHOUSEFINISH;
                try
                {
                    purchaseBll.UpdatePurchaseOrderNoDetail(order);
                    Guid guid = new Guid(order.WorkFlowInstanceID);
                    WorkflowHelper.SetStateMachine<PurchaseEventService>(guid, PurchaseWorkflow.FinishEvent);
                }
                catch (Exception ex)
                {
                    EventMessage.EventWriteLog(Msg_Type.Error, "无法更新采购单" + order.ID + "状态" + ex.Message + "\nStack:" + ex.StackTrace);
                }
            }
            Session[sessionName] = order;
            FillData();
        }
    }


    /// <summary>
    /// 完成一行的时候，进行保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        short itemID = short.Parse(Hidden_EditItemID.Value);

        PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName];
        if (order == null)
        {
            order = purchaseBll.GetPurchaseOrderByID(id);
            Session[sessionName] = order;
        }
        PurchaseOrderDetailInfo targetItem = null;
        foreach (PurchaseOrderDetailInfo item in order.DetailList)
        {
            if (item.ItemID == itemID)
            {
                targetItem = item;
                break;
            }
        }
       
        decimal unitprice = decimal.Parse(TextBox_UnitPrice.Text.Trim());
        
        decimal count = decimal.Parse(TextBox_Count.Text.Trim());
        string remark = TextBox_Remark.Text.Trim();
        string producer = TextBox_Producer.Text.Trim();
        string supplier = TextBox_Supplier.Text.Trim();

        if (targetItem != null)
        {
            PurchaseRecordInfo purchaseRecord = new PurchaseRecordInfo();
            purchaseRecord.Purchaser = Common.Get_UserName;
            purchaseRecord.CompanyID = order.CompanyID;
            purchaseRecord.ID = 0;
            purchaseRecord.Model = TextBox_SelectedProductModel.Text.Trim();
            purchaseRecord.OrderID = id;
            purchaseRecord.PlanDetailItemID = targetItem.ItemID;
            purchaseRecord.Producer = TextBox_Producer.Text.Trim();
            purchaseRecord.Supplier = TextBox_Supplier.Text.Trim();
            purchaseRecord.Unit = DropDownList_Unit.SelectedValue;
            purchaseRecord.WarehouseID = DropDownList_Warehouse.SelectedValue;
            purchaseRecord.ProductName = TextBox_SelectedProductName.Text.Trim();
            purchaseRecord.PurchaseCount = decimal.Parse(TextBox_Count.Text.Trim());
            purchaseRecord.PurchaseOrderID = order.PurchaseOrderID;
            purchaseRecord.Purchaser = targetItem.Purchaser;
            purchaseRecord.PurchaseRemark = TextBox_Remark.Text.Trim();
            purchaseRecord.PurchaseTime = DateTime.Now;
            purchaseRecord.PurchaseUnitPrice = decimal.Parse(TextBox_UnitPrice.Text.Trim());
            purchaseRecord.SubOrderIndex = order.SubOrderIndex;
            purchaseRecord.Status = PurchaseRecordStatus.WAIT4CHECK;
            purchaseRecord.PurchaserConfirm = true;
            purchaseRecord.PurchaserConfirmTime = DateTime.Now;
            purchaseRecord.WarehouseName = DropDownList_Warehouse.SelectedItem.Text;
            try
            {
                
                purchaseRecord.ID = purchaseBll.SavePurchaseRecord(purchaseRecord);
                targetItem.PurchaseRecordList.Add(purchaseRecord);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "保存采购记录失败", "保存采购记录失败，请重试", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                return;
            }

            //获取所有的仓管员，发送消息
            IList wks = staffBll.GetAllWarehouseUserByWarehouseID(purchaseRecord.WarehouseID);//仓管员
            string[] wksarray = new string[wks.Count];
            for (int i = 0; i < wks.Count; i++)
            {
                wksarray[i] = (wks[i] as UserInfo).UserName;
            }

            long thisID = order.ID;
            string title = "验收请求：" + order.PurchaseOrderID + "-" + order.SubOrderIndex + " " + order.PurchaseOrderName + "材料申购单" + "需要验收";
            string URL = "../DeviceManager/SpareEquipmentManager/Purchase/CheckInWarehouse/CheckPurchaseOrder.aspx?id=" + thisID + "&cmd=history";
            try
            {
                WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, UserData.CurrentUserData.CompanyID, wksarray);
            }
            catch (Exception ex)
            {
                EventMessage.EventWriteLog(Msg_Type.Error, "工作流发送待办事务错误" + ex.Message);
            }


            //添加价格历史

            PricePurchaseHistoryInfo priceItem = new PricePurchaseHistoryInfo();
            priceItem.CompanyID = purchaseRecord.CompanyID;
            priceItem.ActualPrice = purchaseRecord.PurchaseUnitPrice;
            priceItem.Model = purchaseRecord.Model;
            priceItem.ProductName = purchaseRecord.ProductName;
            priceItem.PurchaseTime = purchaseRecord.PurchaseTime;
            priceItem.Supplier = purchaseRecord.Supplier;
            priceItem.Unit = purchaseRecord.Unit;
            try
            {
                priceBll.AddPricePurchaseHistory(priceItem);
            }
            catch (Exception ex)
            {
                EventMessage.EventWriteLog(Msg_Type.Error, "无法添加价格历史" + ex.Message);
            }
        }
        
        ////判断是否全部完成，如果是，则更新

        //int finishCount = 0;

        //foreach (PurchaseOrderDetailInfo item in order.DetailList)
        //{
        //    switch (item.Status)
        //    {
                
        //        case PurchaseOrderDetailStatus.PURCHASINGFINISH:
        //            finishCount++;
        //            break;
               
        //        default:
        //            break;
        //    }
        //}

        //PurchasingStatus originalPurchasingStatus = order.PurchasingStatus;//保留原来的状态
        //PurchaseOrderStatus originalOrderStatus = order.Status;

        //if (finishCount == order.DetailList.Count)
        //{
        //    order.PurchasingStatus = PurchasingStatus.FINISH;
        //    order.Status = PurchaseOrderStatus.PURCHASINGFINISH;
        //}


        //if (originalPurchasingStatus != order.PurchasingStatus|| originalOrderStatus != order.Status)
        //{
        //    purchaseBll.UpdatePurchaseOrderNoDetail(order);
        //}

        Session[sessionName] = order;

        FillData();

        
    }
}
