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
using FM2E.Model.Utils;

using FM2E.BLL.Basic;
using FM2E.BLL.System;
using FM2E.BLL.Equipment;
using FM2E.BLL.Utils;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_CheckInWarehouse_SendWarehouse : System.Web.UI.Page
{
    /// <summary>
    /// 采购管理业务逻辑处理类对象
    /// </summary>
    CheckAcceptance checkacceptanceBll = new CheckAcceptance();
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
    /// 价格管理业务逻辑处理类对象
    /// </summary>
    PriceManager priceBll = new PriceManager();

    Purchase purchaseBll = new Purchase();

    string sessionName = "Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_CheckInWarehouse_SendWarehouse";
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

        Label_WarehouseKeeper.Text = Common.Get_UserName;
        
        Warehouse Warehousebll = new Warehouse();
        IList<WarehouseInfo> Warehouselist = Warehousebll.GetWarehouseListByUserName(UserData.CurrentUserData.UserName);
        if (Warehouselist.Count == 0)   
        {
            EventMessage.MessageBox(Msg_Type.Error, "身份验证失败", "仓管员" + Common.Get_UserName + "身份验证失败，无法访问该页面", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return;
        }
        ddl_Warehouse.Items.Add(new ListItem("--请选择仓库--", ""));
        foreach (WarehouseInfo item in Warehouselist)
        {          
            ddl_Warehouse.Items.Add(new ListItem(item.Name, item.WareHouseID));
        }
        

        TextBox_Amount.Attributes.Add("readonly", "readonly");
        TextBox_Count.Attributes.Add("onchange", "javascript:onCountChange()");
        TextBox_UnitPrice.Attributes.Add("onchange", "javascript:onCountChange()");

        //读取采购人列表
        IList list = purchaseBll.GetAllPurchaserList(UserData.CurrentUserData.CompanyID);
        PurchaserInfo nullPurchaser = new PurchaserInfo();
        nullPurchaser.UserID = "";
        nullPurchaser.PurchaserName = "--";
        nullPurchaser.Remark = "--";
        list.Insert(0, nullPurchaser);
        DropDownList_Purchaser.DataSource = list;
        DropDownList_Purchaser.DataBind();

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
            form = new CheckAcceptanceInfo();
            form.CompanyID = UserData.CurrentUserData.CompanyID;
            Session[sessionName] = form;
        }
        //公司ID
        //CompanyInfo company = new Company().GetCompany(form.CompanyID);
        //if (company != null)
        //    Label_CompanyName.Text = company.CompanyName;
        CascadingDropDown1.SelectedValue = form.CompanyID;

        TextBox_SheetName.Text = form.SheetName;
        Label_SheetID.Text = form.SheetID == "" ? "____________________" : form.SheetID;

        Label_Status.Text = form.StatusString;


        Label_UpdateTime.Text = form.UpdateTime==DateTime.MinValue? "": form.UpdateTime.ToString("yyyy-MM-dd HH:mm");

        Label_SubmitTime.Text = form.SubmitTime == DateTime.MinValue ? "" : form.SubmitTime.ToString("yyyy-MM-dd HH:mm");
        Label_ApplicantName.Text = form.ApplicantName;

        IList list = form.DetailList;
        if (list == null)
        {
            list = new List<CheckAcceptanceDetailInfo>();
        }
        Repeater_PurchaseRecordList.DataSource = list;
        Repeater_PurchaseRecordList.DataBind();



    }

    /// <summary>
    /// 编辑一行的时候，进行保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        short itemID = short.Parse(Hidden_EditItemID.Value);
        CheckAcceptanceInfo form = (CheckAcceptanceInfo)Session[sessionName];
        if (form == null)
        {
            form = new CheckAcceptanceInfo();
            form.CompanyID = DDLCompany.SelectedValue;
            Session[sessionName] = form;
        }
        else
        {
            form.CompanyID = DDLCompany.SelectedValue;
            Session[sessionName] = form;
        }

        //需要校验采购员签名和技术验收员签名
        string purchaser = DropDownList_Purchaser.SelectedValue;
        string purchaserpsw = TextBox_PurchaserPassword.Text;
        string technician = TextBox_TechnicianID.Text.Trim();
        string technicianpsw = TextBox_TechnicianPassword.Text;
        if (!userBll.ValidatePassword(purchaser, Common.md5(purchaserpsw, 32)))
        {
            //EventMessage.MessageBox(Msg_Type.Error, "身份验证失败", "采购员身份验证失败，请输入正确的验证信息", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            //return;
        }

        if (!userBll.ValidatePassword(technician, Common.md5(technicianpsw, 32)))
        {
            EventMessage.MessageBox(Msg_Type.Error, "身份验证失败", "技术验收员身份验证失败，请输入正确的验证信息", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return;
        }

        string name = TextBox_SelectedProductName.Text.Trim();
        string model = TextBox_SelectedProductModel.Text.Trim();
        decimal unitprice = decimal.Parse(TextBox_UnitPrice.Text.Trim());
        string unit = DropDownList_Unit.SelectedValue;
        decimal count = decimal.Parse(TextBox_Count.Text.Trim());
        string remark = TextBox_Remark.Text.Trim();
        string producer = TextBox_Producer.Text.Trim();
        string supplier = TextBox_Supplier.Text.Trim();

        string warehouseid = "";
        string warehousename = "";
        if (ddl_Warehouse.SelectedValue == "")
        {
            EventMessage.MessageBox(Msg_Type.Error, "提交失败", "请选择仓库", Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
            return;
        }
        else
        {
             warehouseid =ddl_Warehouse.SelectedValue ;
             warehousename = ddl_Warehouse.SelectedItem.ToString();
        }
        string sheetname = TextBox_SheetName.Text.Trim();
        decimal acceptedcount = decimal.Parse(TextBox_AcceptedCount.Text.Trim());
        string acceptedremark = TextBox_RemarkAccept.Text.Trim();

        string systemid = DropDownList_System.SelectedValue;
        string systemname = DropDownList_System.SelectedItem.Text;

        CheckAcceptanceDetailInfo target = null;
        foreach (CheckAcceptanceDetailInfo item in form.DetailList)
        {
            if (item.ItemID == itemID)
                target = item;
        }
        if (target == null)
        {
            target = new CheckAcceptanceDetailInfo();
            target.ItemID = (short)(form.DetailList.Count + 1);
            target.ID = form.ID;
            target.CompanyID = DDLCompany.SelectedValue;
            target.SheetID = form.SheetID;
            form.DetailList.Add(target);
        }

        form.SheetName = sheetname;
        if (target != null)
        {
            target.Unit = unit;
            target.PurchaseUnitPrice = unitprice;
            target.PurchaseCount = count;
            target.PurchaseRemark = remark;
            target.Model = model;
            target.Producer = producer;
            target.ProductName = name;
            target.Status = PurchaseRecordStatus.WAIT4INWAREHOUSE;
            target.Supplier = supplier;
            target.WarehouseID = warehouseid;
            target.WarehouseName = warehousename;
            target.AcceptanceCount = acceptedcount;
            target.AcceptanceRemark = acceptedremark;
            target.AcceptedTime = DateTime.Now;
            target.Checker_Technician = technician;
            target.Checker_Warehouse = Common.Get_UserName;
            target.Purchaser = purchaser;
            target.PurchaserConfirm = false;
            target.PurchaseRemark = remark;
            target.PurchaseTime = DateTime.Now;
            target.SystemID = systemid;
            target.SystemName = systemname;
             if (Math.Abs(target.AcceptanceCount - target.PurchaseCount) < 0.01M)//近似相等
             {
                 target.AcceptanceResult = PurchaseOrderDetailAcceptanceResult.ALL;//全部通过
             }
             else
                 if (target.AcceptanceCount < target.PurchaseCount && target.AcceptanceCount > 0)
                     target.AcceptanceResult = PurchaseOrderDetailAcceptanceResult.PART;//部分通过
                 else
                     if (target.AcceptanceCount == 0)
                         target.AcceptanceResult = PurchaseOrderDetailAcceptanceResult.NONE;//全部不通过
        }
        Session[sessionName] = form;
        FillData();
    }


    /// <summary>
    /// 提交验收
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Submit_Click(object sender, EventArgs e)
    {
        CheckAcceptanceInfo form = (CheckAcceptanceInfo)Session[sessionName];
        if (form == null)
        {
            form = new CheckAcceptanceInfo();
            form.CompanyID = DDLCompany.SelectedValue;
            Session[sessionName] = form;
            EventMessage.MessageBox(Msg_Type.Error, "提交失败", "未添加报验单，请重试",
               Icon_Type.Error, false, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }

        if (form.DetailList.Count == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "提交失败", "未添加报验单详情，请重试",
               Icon_Type.Error, false, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }

        form.SheetName = TextBox_SheetName.Text.Trim();
        if (form.SheetName == "")
        {
            EventMessage.MessageBox(Msg_Type.Error, "提交失败", "未输入表单名称",
               Icon_Type.Error, false, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }
        
        try
        {
            form.SheetID = SheetNOGenerator.GetSheetNO(DDLCompany.SelectedValue, SheetType.EQUIPMENT_CHECKACCEPTANCEFORM);
            form.SubmitTime = DateTime.Now;
            form.Applicant = Common.Get_UserName;
            form.ID = checkacceptanceBll.SaveCheckAcceptanceFormWithDetail(form);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "提交失败", "报验单提交失败，请刷新后重试", ex,
            Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }

        foreach (CheckAcceptanceDetailInfo detail in form.DetailList)
        {
            //采购确认
            long thisID = form.ID;
            string title = "采购确认：" + form.SheetID + form.SheetName + "报验单 需要核实采购记录";
            string URL = "../DeviceManager/SpareEquipmentManager/Purchase/ExecutePurchasing/ConfirmCAForm.aspx?id=" + thisID + "&cmd=history";
            WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, UserData.CurrentUserData.CompanyID, detail.Purchaser);
        }

        //添加价格历史
        foreach (CheckAcceptanceDetailInfo detail in form.DetailList)
        {
            //添加价格历史

            PricePurchaseHistoryInfo priceItem = new PricePurchaseHistoryInfo();
            priceItem.CompanyID = detail.CompanyID;
            priceItem.ActualPrice = detail.PurchaseUnitPrice;
            priceItem.Model = detail.Model;
            priceItem.ProductName = detail.ProductName;
            priceItem.PurchaseTime = detail.PurchaseTime;
            priceItem.Supplier = detail.Supplier;
            priceItem.Unit = detail.Unit;
            try
            {
                priceBll.AddPricePurchaseHistory(priceItem);
            }
            catch (Exception ex)
            {
                EventMessage.EventWriteLog(Msg_Type.Error, "无法添加价格历史" + ex.Message);
            }
        }


        EventMessage.MessageBox(Msg_Type.Info, "提交成功", "报验单提交成功",
           Icon_Type.OK, false, Common.GetHomeBaseUrl("Check.aspx"), UrlType.Href, "");




    }

    /// <summary>
    /// 删除行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Repeater_PurchaseRecordListCommand(object sender, RepeaterCommandEventArgs e)
    {
        short itemID = short.Parse(e.CommandArgument.ToString());

        if (e.CommandName == "Delete")
        {
            CheckAcceptanceInfo form = (CheckAcceptanceInfo)Session[sessionName];

            if (form.DetailList == null || form.DetailList.Count == 0)
            {
                EventMessage.MessageBox(Msg_Type.Error, "删除失败", "报验项:" + itemID + "已经不存在，请刷新",
                   Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
                return;
            }

            form.DetailList.RemoveAt(itemID - 1);
            //重新更新ITEMID
            short id = 1;
            foreach (CheckAcceptanceDetailInfo item in form.DetailList)
            {
                item.ItemID = id;
                id++;
            }
            FillData();
        }
    }
}
