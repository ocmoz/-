using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

using FM2E.BLL.Basic;
using FM2E.BLL.System;
using FM2E.BLL.Equipment;

using FM2E.WorkflowLayer;
using FM2E.BLL.Utils;
public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseCenter_DeliveryPurchaseOrder : System.Web.UI.Page
{
    /// <summary>
    /// 采购管理业务逻辑处理类对象
    /// </summary>
    Purchase purchaseBll = new Purchase();

    string sessionName = "Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseCenter_DeliveryPurchaseOrder";
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
        FillData();
        //获取采购员列表,

        IList list = purchaseBll.GetPurchaserList(UserData.CurrentUserData.CompanyID);
        PurchaserInfo nullPurchaser = new PurchaserInfo();
        nullPurchaser.UserID = "";
        nullPurchaser.PurchaserName = "未选定";
        nullPurchaser.Remark= "取消选定";
        list.Insert(0, nullPurchaser);
        RadioButtonList_Purchasers.DataSource = list;
        RadioButtonList_Purchasers.DataBind();

        RadioButtonList_Purchasers.SelectedIndex = 0;
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

        Label_UpdateTime.Text = order.UpdateTime == DateTime.MinValue ? "" : order.UpdateTime.ToString("yyyy-MM-dd HH:mm");

        Label_SubmitTime.Text = order.SubmitTime == DateTime.MinValue ? "" : order.SubmitTime.ToString("yyyy-MM-dd HH:mm");
       // Label_Applicant.Text = order.Applicant;

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


    }


    /// <summary>
    /// 数据绑定事件，主要处理footer合计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private decimal totalFinalAmount = 0;//每次postback都会自动初始化
    private decimal totalActualAmount = 0;
    protected void gridview_ItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PurchaseOrderDetailInfo item = e.Row.DataItem as PurchaseOrderDetailInfo;
            totalFinalAmount += item.FinalAmount;
            totalActualAmount += item.ActualAmount;
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
            Label LabelFinalTotal = e.Row.FindControl("Label_FinalTotalAmount") as Label;
            Label LabelActualTotal = e.Row.FindControl("Label_ActualTotalAmount") as Label;
            if (LabelFinalTotal != null)
            {
                LabelFinalTotal.Text += totalFinalAmount.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }
            if (LabelActualTotal != null)
            {
                LabelActualTotal.Text += totalActualAmount.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }

        }

    }


    /// <summary>
    /// 为radiobutton绑定javascript事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioButtonList_Purchasers_DataBound(object sender, EventArgs e)
    {
        foreach (ListItem li in RadioButtonList_Purchasers.Items)
        {
            li.Attributes.Add("onclick", string.Format("javascript:setSelectedValue('{0}','{1}');", li.Value, li.Text));
            
        }
        
    }

    /// <summary>
    /// 保存采购者信息 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        short itemid = short.Parse(Hidden_ItemID.Value);
        string purchaserid = RadioButtonList_Purchasers.SelectedValue; //Hidden_EditPurchaserID.Value;
        string purchasername = RadioButtonList_Purchasers.SelectedItem.Text;// Label_SelectedPurchaser.Text;

        PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName];
        if (order == null)
        {
            order = purchaseBll.GetPurchaseOrderByID(id);
            Session[sessionName] = order;
        }
        int deliveryCount = 0;
        foreach (PurchaseOrderDetailInfo item in order.DetailList)
        {
            if (item.ItemID == itemid)
            {
                item.Purchaser = purchaserid;
                item.PurchaserName = purchasername;
                if (purchaserid == null || purchaserid == "")
                    item.Status = PurchaseOrderDetailStatus.WAITING4DELIVERY;
                else
                    item.Status = PurchaseOrderDetailStatus.WAITING4PURCHASE;
                bool success = false;
                try
                {
                    purchaseBll.UpdatePurchaseOrderDetail(item);

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

                    success = true;
                }
                catch
                {
                }
                if (success)
                {
                    long thisID = order.ID;
                    string title = "采购申请申请" + order.PurchaseOrderID + "-" + order.SubOrderIndex + " " + order.PurchaseOrderName + "材料申购单" + "被指派为您的采购项";
                    string URL = "../DeviceManager/SpareEquipmentManager/Purchase/ExecutePurchasing/Purchase.aspx?id=" + thisID + "&cmd=history";
                    try
                    {
                        WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, UserData.CurrentUserData.CompanyID, purchaserid);
                    }
                    catch (Exception ex)
                    {
                        EventMessage.EventWriteLog(Msg_Type.Error, "工作流发送待办事务错误" + ex.Message);
                    }
                }
            }
            //判断指派状态
            if (item.Purchaser != null && item.Purchaser != "")
            {
                deliveryCount += 1;//指派过的，加1
            }
        }
        PurchaseOrderDeliveryStatus originalDeliveryStatus =  order.DeliveryStatus;//保留原来的状态

        if (deliveryCount == 0)
        {
            order.DeliveryStatus = PurchaseOrderDeliveryStatus.NONE;
        }
        else
            if (deliveryCount < order.DetailList.Count)
                order.DeliveryStatus = PurchaseOrderDeliveryStatus.PART;
            else
                if (deliveryCount == order.DetailList.Count)
                    order.DeliveryStatus = PurchaseOrderDeliveryStatus.ALL;
        if (originalDeliveryStatus != order.DeliveryStatus)
        {
            purchaseBll.UpdatePurchaseOrderNoDetail(order);//如果状态改变了
        }
        
        Session[sessionName] = order;
        FillData();
    }
}
