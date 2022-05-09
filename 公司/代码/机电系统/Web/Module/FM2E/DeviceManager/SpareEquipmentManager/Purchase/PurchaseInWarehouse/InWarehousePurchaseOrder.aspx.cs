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
using FM2E.BLL.BarCode;


public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseInWarehouse_InWarehousePurchaseOrder : System.Web.UI.Page
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

    string sessionName = "Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseInWarehouse_InWarehousePurchaseOrder";
    Company companyBll = new Company();

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
    /// 数据绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Repeater_ItemList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

            PurchaseRecordInfo item = e.Item.DataItem as PurchaseRecordInfo;
            HtmlTableRow tr = e.Item.FindControl("tr_item") as HtmlTableRow;
            if (item.WarehouseID == Hidden_WarehouseID.Value)
            {
                
                switch (item.Status)
                {
                    case  PurchaseRecordStatus.WAIT4INWAREHOUSE:
                        e.Item.FindControl("Button_InWarehouse").Visible = true;
                        tr.BgColor = "LightYellow";
                        break;
                    default:
                        e.Item.FindControl("Button_InWarehouse").Visible = false;
                        tr.BgColor = "Transparent";
                        break;

                }
                if (item.IsDividable)
                {
                    e.Item.FindControl("Button_Divide").Visible = true;
                }
                else
                {
                    e.Item.FindControl("Button_Divide").Visible = false;
                }
            }
            else
            {
                tr.BgColor = "Transparent";
                e.Item.FindControl("Button_InWarehouse").Visible = false;
                e.Item.FindControl("Button_Divide").Visible = false;
            }
            
    
    }

    /// <summary>
    /// 完成一行的时候，进行保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        //判断是否全部入库完毕
        PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName];
       
            order = purchaseBll.GetPurchaseOrderByID(id);
            Session[sessionName] = order;
        
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
                            case PurchaseRecordStatus.WAIT4INWAREHOUSE:
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
                order.Status = PurchaseOrderStatus.INWAREHOUSEFINISH;
            purchaseBll.UpdatePurchaseOrderNoDetail(order);
        }
        catch { }


        Session[sessionName] = order;

        FillData();
    }

    /// <summary>
    /// 打印条形码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Repeater_BarcodeList_OnCommand(object sender, RepeaterCommandEventArgs e)
    {

        if (e.CommandName != null && e.CommandName != "")
        {
            PurchaseOrderInfo order = (PurchaseOrderInfo)Session[sessionName];
            if (order == null)
            {
                order = purchaseBll.GetPurchaseOrderByID(id);
                Session[sessionName] = order;
            }

            CompanyInfo cmp = companyBll.GetCompany(order.CompanyID);
            string companyname = "";
            if (cmp != null)
                companyname = cmp.CompanyName;

            string arg = e.CommandArgument.ToString();
            string[] args = arg.Split('|');
            short itemid = short.Parse(args[0]);
            long recordid = long.Parse(args[1]);
            int index = e.Item.ItemIndex;
            PurchaseRecordInfo target = null;
            foreach (PurchaseOrderDetailInfo detail in order.DetailList)
            {
                if (detail.ItemID == itemid)
                {
                    foreach (PurchaseRecordInfo record in detail.PurchaseRecordList)
                    {
                        if (record.ID == recordid)
                        {
                            target = record;
                            break;
                        }
                    }
                    if (target != null)
                        break;
                }
            }
            IList list = target.GetBarcodeList(index);
            switch (e.CommandName)
            {
                case "PRINT":

                    BarCodeInfo[] barCodeArray = new BarCodeInfo[list.Count];
                    for (int i = 0; i < list.Count; i++)
                    {
                        barCodeArray[i] = new BarCodeInfo();
                        PurchaseBarcodeInfo bc
                            = list[i] as PurchaseBarcodeInfo;
                        barCodeArray[i].BarCode = bc.Barcode;
                        barCodeArray[i].CompanyName = companyname;
                        barCodeArray[i].EquipmentName = bc.ProductName;

                    }

                    Session[Constants.BARCODE_SESSION_STRING] = barCodeArray;
                    UpdatePanel p = e.Item.FindControl("UpdatePanel_Print") as UpdatePanel;
                    ScriptManager.RegisterClientScriptBlock(p, p.GetType(), "print", "showPopWin('打印条形码','../../../BarCode/BarCodePrint.aspx',800, 330, null,true,true);", true);
                    break;
                default:
                    break;
            }
        }
    }
}
