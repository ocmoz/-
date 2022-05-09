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


public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_CheckInWarehouse_CheckPurchaseOrderHistory : System.Web.UI.Page
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

    string sessionName = "Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_CheckInWarehouse_CheckPurchaseOrderHistory";


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
        }


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
        Label_OrderID.Text = order.PurchaseOrderID + "-" + order.SubOrderIndex;

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

        }
        else
        {
            tr.BgColor = "Transparent";
        }

    }

    protected void GridView_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        PurchaseRecordInfo item = e.Row.DataItem as PurchaseRecordInfo;
        if (item.WarehouseID == Hidden_WarehouseID.Value)
        {
            e.Row.BackColor = System.Drawing.Color.LightYellow;
        }
        else
        {
            e.Row.BackColor = System.Drawing.Color.Transparent;
        }
    }




}
