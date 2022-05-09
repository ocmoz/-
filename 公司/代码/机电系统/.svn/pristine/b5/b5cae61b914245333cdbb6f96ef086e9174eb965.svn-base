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

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_CheckInWarehouse_CheckPurchaseOrder : System.Web.UI.Page
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
    /// 校验用户名
    /// </summary>
    User userBll = new User();
    /// <summary>
    /// 员工业务逻辑处理类对象，用于获取仓库管理员
    /// </summary>
    Warehouse staffBll = new Warehouse();
    /// <summary>
    /// 当前仓库的ID
    /// </summary>
    //string warehouseid = "";
    /// <summary>
    /// 价格管理业务逻辑处理类对象
    /// </summary>
    PriceManager priceBll = new PriceManager();

    string sessionName = "CheckPurchaseOrder";


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

        WarehouseInfo warehouse = staffBll.GetWarehouseByUserName(Common.Get_UserName);
        if (warehouse == null || warehouse.WareHouseID == null || warehouse.WareHouseID == "")//找不到仓库
        {
            EventMessage.MessageBox(Msg_Type.Error, "身份验证失败", "仓管员" + Common.Get_UserName + "身份验证失败，无法访问该页面", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return;
        }
        else
        {
            Hidden_WarehouseID.Value = warehouse.WareHouseID;
            Hidden_WarehouseName.Value = warehouse.Name;
        }

        Label_WarehouseKeeper.Text = Common.Get_UserName;
        Label_WarehouseKeeper2.Text = Common.Get_UserName;

        //单位
        DropDownList_Unit.DataSource = Constants.GetUnits();
        DropDownList_Unit.DataBind();

        DropDownList_Unit.Items.Insert(0, new ListItem("--请选择单位--", ""));

        //单位
        DropDownList_Unit2.DataSource = Constants.GetUnits();
        DropDownList_Unit2.DataBind();

        DropDownList_Unit2.Items.Insert(0, new ListItem("--请选择单位--", ""));

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
       // Label_Applicant.Text = order.Applicant;

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
    /// 数据绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Repeater_ItemList_RowDataBound(object sender, RepeaterItemEventArgs e)
    {

        PurchaseOrderDetailInfo item = e.Item.DataItem as PurchaseOrderDetailInfo;
        HtmlTableRow tr = e.Item.FindControl("tr_item") as HtmlTableRow;
        if (item.Status == PurchaseOrderDetailStatus.PURCHASING)
        {
            tr.BgColor = "LightYellow";

            e.Item.FindControl("Button_Check").Visible = true;
        }
        else
        {
            tr.BgColor = "Transparent";
            e.Item.FindControl("Button_Check").Visible = false;
        }

    }

    protected void GridView_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        PurchaseRecordInfo item = e.Row.DataItem as PurchaseRecordInfo;
        if (item.Status == PurchaseRecordStatus.WAIT4CHECK && item.WarehouseID == Hidden_WarehouseID.Value)
        {
            e.Row.BackColor = System.Drawing.Color.LightYellow;
            e.Row.FindControl("Button_Check2").Visible = true;
        }
        else
        {
            e.Row.BackColor = System.Drawing.Color.Transparent;
            e.Row.FindControl("Button_Check2").Visible = false;
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

        //需要校验采购员签名和技术验收员签名
        string purchaser = targetItem.Purchaser;
        string purchaserpsw = TextBox_PurchaserPassword.Text;
        string technician = TextBox_TechnicianID.Text.Trim();
        string technicianpsw = TextBox_TechnicianPassword.Text;
        if(!userBll.ValidatePassword(purchaser, Common.md5(purchaserpsw,32)))
        {
            EventMessage.MessageBox(Msg_Type.Error, "身份验证失败", "采购员身份验证失败，请输入正确的验证信息", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return;
        }
        
        if (!userBll.ValidatePassword(technician, Common.md5(technicianpsw, 32)))
        {
            EventMessage.MessageBox(Msg_Type.Error, "身份验证失败", "技术验收员身份验证失败，请输入正确的验证信息", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return;
        }
        //targetItem.Checker_Technician = technician;
        //targetItem.Checker_WK = Common.Get_UserName;

        //添加一条验收记录（即分批购买记录）
        PurchaseRecordInfo purchaseRecord = new PurchaseRecordInfo();
        purchaseRecord.SystemID = targetItem.SystemID;
        purchaseRecord.Checker_Technician = technician;
        purchaseRecord.Checker_Warehouse = Common.Get_UserName;
        purchaseRecord.CompanyID = order.CompanyID;
        purchaseRecord.ID = 0;
        purchaseRecord.Model = TextBox_SelectedProductModel.Text.Trim();
        purchaseRecord.OrderID = id;
        purchaseRecord.PlanDetailItemID = targetItem.ItemID;
        purchaseRecord.Producer = TextBox_Producer.Text.Trim();
        purchaseRecord.Supplier = TextBox_Supplier.Text.Trim();
        purchaseRecord.Unit = DropDownList_Unit.SelectedValue;
        purchaseRecord.WarehouseID = Hidden_WarehouseID.Value;
        purchaseRecord.ProductName = TextBox_SelectedProductName.Text.Trim();
        purchaseRecord.PurchaseCount = decimal.Parse(TextBox_ActualCount.Text.Trim());
        purchaseRecord.PurchaseOrderID = order.PurchaseOrderID;
        purchaseRecord.Purchaser = targetItem.Purchaser;
        purchaseRecord.PurchaseRemark = TextBox_PurchaseRemark.Text.Trim();
        purchaseRecord.PurchaseTime = DateTime.Now;
        purchaseRecord.PurchaseUnitPrice = decimal.Parse(TextBox_UnitPrice.Text.Trim());
        purchaseRecord.SubOrderIndex = order.SubOrderIndex;
        purchaseRecord.AcceptanceCount = decimal.Parse(TextBox_ActualCount.Text.Trim());
        purchaseRecord.AcceptanceRemark = TextBox_Remark.Text.Trim();
        purchaseRecord.WarehouseName = Hidden_WarehouseName.Value;
        purchaseRecord.Status = PurchaseRecordStatus.WAIT4INWAREHOUSE;

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
        catch(Exception ex)
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "无法添加价格历史"+ex.Message);
        }

        if (Math.Abs(purchaseRecord.AcceptanceCount - purchaseRecord.PurchaseCount) < 0.01M)//近似相等
            {
                purchaseRecord.AcceptanceResult = PurchaseOrderDetailAcceptanceResult.ALL;//全部通过
            }
            else
            if (purchaseRecord.AcceptanceCount < purchaseRecord.PurchaseCount && purchaseRecord.AcceptanceCount > 0)
                purchaseRecord.AcceptanceResult = PurchaseOrderDetailAcceptanceResult.PART;//部分通过
                else
                if (purchaseRecord.AcceptanceCount == 0)
                    purchaseRecord.AcceptanceResult = PurchaseOrderDetailAcceptanceResult.NONE;//全部不通过

        purchaseRecord.AcceptedTime = DateTime.Now;

        //purchaseBll.UpdatePurchaseOrderDetail(targetItem);

        try
        {
            purchaseRecord.ID = purchaseBll.SavePurchaseRecord(purchaseRecord);
            targetItem.PurchaseRecordList.Add(purchaseRecord);
        }
        catch(Exception ex) {
            EventMessage.MessageBox(Msg_Type.Error, "保存采购验收记录失败", "保存采购验收记录失败，请重试", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            return;
        }

        //采购确认
        long thisID = order.ID;
        string title = "采购确认：" + order.PurchaseOrderID+"-"+order.SubOrderIndex+" "+order.PurchaseOrderName + "材料申购单 需要核实采购记录";
        string URL = "../DeviceManager/SpareEquipmentManager/Purchase/ExecutePurchasing/Purchase.aspx?id=" + thisID + "&cmd=history";
        WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, UserData.CurrentUserData.CompanyID, purchaseRecord.Purchaser);


        //判断是否全部验收完毕

        bool finish = true;
        foreach (PurchaseOrderDetailInfo item in order.DetailList)
        {
            switch (item.Status)
            {
                case PurchaseOrderDetailStatus.CHECKFINISH:
                case PurchaseOrderDetailStatus.INWAREHOUSEFINISH:
                case PurchaseOrderDetailStatus.PURCHASINGFINISH:
                    foreach (PurchaseRecordInfo record in item.PurchaseRecordList)
                    {
                        switch (record.Status)
                        {
                            case PurchaseRecordStatus.NONE:
                            case PurchaseRecordStatus.PURCHASING:
                            case PurchaseRecordStatus.WAIT4CHECK:
                                finish = false;
                                break;
                        }
                    }
                    break;
                default:
                    finish = false;
                    break;
            }
        }

        
        try
        {
            if (finish)
                order.Status = PurchaseOrderStatus.ACCEPTINGFINISH;
            purchaseBll.UpdatePurchaseOrderNoDetail(order);
        }
        catch { }

        Session[sessionName] = order;

        FillData();
    }

    /// <summary>
    /// 完成一行的时候，进行保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Save2_Click(object sender, EventArgs e)
    {

        short itemID = short.Parse(Hidden_EditItemID2.Value);
        long recordid = long.Parse(Hidden_EditRecordID.Value);

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

        PurchaseRecordInfo targetRecordItem = null;
        foreach (PurchaseRecordInfo r in targetItem.PurchaseRecordList)
        {
            if (r.ID == recordid)
            {
                targetRecordItem = r;
                break;
            }

        }

        //需要校验采购员签名和技术验收员签名
        string purchaser = targetItem.Purchaser;
        string purchaserpsw = TextBox_PurchaserPassword2.Text;
        string technician = TextBox_TechnicianID2.Text.Trim();
        string technicianpsw = TextBox_TechnicianPassword2.Text;
        if (!userBll.ValidatePassword(purchaser, Common.md5(purchaserpsw, 32)))
        {
            EventMessage.MessageBox(Msg_Type.Error, "身份验证失败", "采购员身份验证失败，请输入正确的验证信息", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return;
        }

        if (!userBll.ValidatePassword(technician, Common.md5(technicianpsw, 32)))
        {
            EventMessage.MessageBox(Msg_Type.Error, "身份验证失败", "技术验收员身份验证失败，请输入正确的验证信息", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return;
        }
        //targetItem.Checker_Technician = technician;
        //targetItem.Checker_WK = Common.Get_UserName;

        //添加一条验收记录（即分批购买记录）
        targetRecordItem.SystemID = targetItem.SystemID;
        targetRecordItem.Checker_Technician = technician;
        targetRecordItem.Checker_Warehouse = Common.Get_UserName;
        targetRecordItem.WarehouseID = Hidden_WarehouseID.Value;
        targetRecordItem.WarehouseName = Hidden_WarehouseName.Value;
        targetRecordItem.AcceptanceCount = decimal.Parse(TextBox_ActualCount2.Text.Trim());
        targetRecordItem.AcceptanceRemark = TextBox_Remark2.Text.Trim();
        targetRecordItem.Status = PurchaseRecordStatus.WAIT4INWAREHOUSE;
        if (Math.Abs(targetRecordItem.AcceptanceCount - targetRecordItem.PurchaseCount) < 0.01M)//近似相等
        {
            targetRecordItem.AcceptanceResult = PurchaseOrderDetailAcceptanceResult.ALL;//全部通过
        }
        else
            if (targetRecordItem.AcceptanceCount < targetRecordItem.PurchaseCount && targetRecordItem.AcceptanceCount > 0)
                targetRecordItem.AcceptanceResult = PurchaseOrderDetailAcceptanceResult.PART;//部分通过
            else
                if (targetRecordItem.AcceptanceCount == 0)
                    targetRecordItem.AcceptanceResult = PurchaseOrderDetailAcceptanceResult.NONE;//全部不通过
        targetRecordItem.AcceptedTime = DateTime.Now;
        try
        {
            targetRecordItem.ID = purchaseBll.SavePurchaseRecord(targetRecordItem);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "保存验收记录失败", "保存验收记录失败，请重试", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            return;
        }



        //判断是否全部验收完毕

        bool finish = true;
        foreach (PurchaseOrderDetailInfo item in order.DetailList)
        {
            switch (item.Status)
            {
                case PurchaseOrderDetailStatus.CHECKFINISH:
                case PurchaseOrderDetailStatus.INWAREHOUSEFINISH:
                case PurchaseOrderDetailStatus.PURCHASINGFINISH:
                    foreach (PurchaseRecordInfo record in item.PurchaseRecordList)
                    {
                        switch (record.Status)
                        {
                            case PurchaseRecordStatus.NONE:
                            case PurchaseRecordStatus.PURCHASING:
                            case PurchaseRecordStatus.WAIT4CHECK:
                                finish = false;
                                break;
                        }
                    }
                    break;
                default:
                    finish = false;
                    break;
            }
        }

        try
        {
            if (finish)
                order.Status = PurchaseOrderStatus.ACCEPTINGFINISH;
            purchaseBll.UpdatePurchaseOrderNoDetail(order);
        }
        catch { }
        

        Session[sessionName] = order;

        FillData();
    }
}
