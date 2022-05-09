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

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseInWarehouse_InCAForm : System.Web.UI.Page
{
    CheckAcceptance checkaccptanceBll = new CheckAcceptance();
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

    Company companyBll = new Company();
    string sessionName = "Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseInWarehouse_InCAForm";


    /// <summary>
    /// 需要查看的报验单ID
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
        CheckAcceptanceInfo form = (CheckAcceptanceInfo)Session[sessionName];
        if (form == null)
        {
            form = checkaccptanceBll.GetCheckAcceptanceInfoAllDetail(id);
            Session[sessionName] = form;
        }
        //公司ID
        CompanyInfo company = new Company().GetCompany(form.CompanyID);
        if (company != null)
            Label_CompanyName.Text = company.CompanyName;

        Label_SheetID.Text = form.SheetID;
        Label_SheetName.Text = form.SheetName;

        Label_Status.Text = form.StatusString;


        Label_UpdateTime.Text = form.UpdateTime == DateTime.MinValue ? "" : form.UpdateTime.ToString("yyyy-MM-dd HH:mm");

        Label_SubmitTime.Text = form.UpdateTime == DateTime.MinValue ? "" : form.SubmitTime.ToString("yyyy-MM-dd HH:mm");

        Label_ApplicantName.Text = form.ApplicantName;

        Repeater_PurchaseRecordList.DataSource = form.DetailList;
        Repeater_PurchaseRecordList.DataBind();

    }


    /// <summary>
    /// 数据绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Repeater_ItemList_RowDataBound(object sender, RepeaterItemEventArgs e)
    {

        CheckAcceptanceDetailInfo item = e.Item.DataItem as CheckAcceptanceDetailInfo;
        HtmlTableRow tr = e.Item.FindControl("tr_item") as HtmlTableRow;
        if (item.WarehouseID == Hidden_WarehouseID.Value)
        {

            switch (item.Status)
            {
                case PurchaseRecordStatus.WAIT4INWAREHOUSE:
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
    /// 打印条形码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Repeater_BarcodeList_OnCommand(object sender, RepeaterCommandEventArgs e)
    {

        if (e.CommandName != null && e.CommandName != "")
        {
            CheckAcceptanceInfo form = (CheckAcceptanceInfo)Session[sessionName];
            if (form == null)
            {
                form = checkaccptanceBll.GetCheckAcceptanceInfoAllDetail(id);
                Session[sessionName] = form;
            }

            CompanyInfo cmp = companyBll.GetCompany(form.CompanyID);
            string companyname = "";
            if (cmp != null)
                companyname = cmp.CompanyName;

            string arg = e.CommandArgument.ToString();

            short itemid = short.Parse(arg);
            int index = e.Item.ItemIndex;
            CheckAcceptanceDetailInfo target = null;
            foreach (CheckAcceptanceDetailInfo item in form.DetailList)
            {
                if (item.ItemID == itemid)
                {
                    target = item;
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
                        CheckAcceptanceDetailBarcodeInfo detail = list[i] as CheckAcceptanceDetailBarcodeInfo;
                        barCodeArray[i].BarCode = detail.Barcode;
                        barCodeArray[i].CompanyName = companyname;
                        barCodeArray[i].EquipmentName = detail.ProductName;

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

