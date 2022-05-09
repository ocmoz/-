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

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_CheckInWarehouse_CheckCAForm : System.Web.UI.Page
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


    string sessionName = "Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_CheckInWarehouse_CheckCAForm";


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

        Label_WarehouseKeeper.Text = Common.Get_UserName;

        //单位
        DropDownList_Unit.DataSource = Constants.GetUnits();
        DropDownList_Unit.DataBind();

        DropDownList_Unit.Items.Insert(0, new ListItem("--请选择单位--", ""));
        EquipmentSystem eqmtsysBll = new EquipmentSystem();
        //系统划分
        DropDownList_System.Items.AddRange(eqmtsysBll.GenerateListItemCollectionWithBlank());

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
        if (item.Status == PurchaseRecordStatus.WAIT4CHECK)
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



    /// <summary>
    /// 完成一行的时候，进行保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        short itemID = short.Parse(Hidden_EditItemID.Value);


        CheckAcceptanceInfo form = (CheckAcceptanceInfo)Session[sessionName];
        if (form == null)
        {
            form = checkaccptanceBll.GetCheckAcceptanceInfoAllDetail(id);
            Session[sessionName] = form;
        }
        CheckAcceptanceDetailInfo targetItem = null;
        foreach (CheckAcceptanceDetailInfo item in form.DetailList)
        {
            if (item.ItemID == itemID)
            {
                targetItem = item;
                break;
            }
        }
        if (targetItem == null)
        {
            EventMessage.MessageBox(Msg_Type.Error, "验收失败", "验收失败，数据丢失，请重试", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return;
        }
        //需要校验采购员签名和技术验收员签名
        string purchaser = targetItem.Purchaser;
        string purchaserpsw = TextBox_PurchaserPassword.Text;
        string technician = TextBox_TechnicianID.Text.Trim();
        string technicianpsw = TextBox_TechnicianPassword.Text;
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
        targetItem.Checker_Technician = technician;
        targetItem.Checker_Warehouse = Common.Get_UserName;

        targetItem.Status = PurchaseRecordStatus.WAIT4INWAREHOUSE;
        targetItem.AcceptanceCount = decimal.Parse(TextBox_AcceptedCount.Text.Trim());
        targetItem.AcceptanceRemark = TextBox_Remark.Text.Trim();


        if (Math.Abs(targetItem.AcceptanceCount - targetItem.PurchaseCount) < 0.01M)//近似相等
        {
            targetItem.AcceptanceResult = PurchaseOrderDetailAcceptanceResult.ALL;//全部通过
        }
        else
            if (targetItem.AcceptanceCount < targetItem.PurchaseCount && targetItem.AcceptanceCount > 0)
                targetItem.AcceptanceResult = PurchaseOrderDetailAcceptanceResult.PART;//部分通过
            else
                if (targetItem.AcceptanceCount == 0)
                    targetItem.AcceptanceResult = PurchaseOrderDetailAcceptanceResult.NONE;//全部不通过

        targetItem.AcceptedTime = DateTime.Now;


        try
        {
            checkaccptanceBll.UpdateCheckAcceptanceDetail(targetItem);

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
            EventMessage.MessageBox(Msg_Type.Error, "保存采购验收记录失败", "保存采购验收记录失败，请重试", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            return;
        }


        try
        {
            checkaccptanceBll.SaveCheckAcceptanceFormWithoutDetail(form);
        }
        catch { }
        

        Session[sessionName] = form;

        FillData();
    }


}

