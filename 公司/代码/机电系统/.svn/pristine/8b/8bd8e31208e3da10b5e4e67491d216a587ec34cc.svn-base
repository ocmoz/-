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

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_ExecutePurchasing_SendWarehouse : System.Web.UI.Page
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
    /// 价格管理业务逻辑处理类对象
    /// </summary>
    PriceManager priceBll = new PriceManager();

    /// <summary>
    /// 员工业务逻辑处理类对象
    /// </summary>
    Warehouse staffBll = new Warehouse();

    string sessionName = "Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_ExecutePurchasing_SendWarehouse";
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
        CompanyInfo company = new Company().GetCompany(form.CompanyID);
        if (company != null)
            Label_CompanyName.Text = company.CompanyName;

        TextBox_SheetName.Text = form.SheetName;
        Label_SheetID.Text = form.SheetID == "" ? "____________________" : form.SheetID;

        Label_Status.Text = form.StatusString;


        Label_UpdateTime.Text = form.UpdateTime==DateTime.MinValue? "": form.UpdateTime.ToString("yyyy-MM-dd HH:mm");

        Label_SubmitTime.Text = form.SubmitTime == DateTime.MinValue ? "" : form.SubmitTime.ToString("yyyy-MM-dd HH:mm");
        Label_Applicant.Text = form.Applicant;

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
            form.CompanyID = UserData.CurrentUserData.CompanyID;
            Session[sessionName] = form;
        }

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
            target.CompanyID = form.CompanyID;
            target.SheetID = form.SheetID;
            form.DetailList.Add(target);
        }
        string name = TextBox_SelectedProductName.Text.Trim();
        string model = TextBox_SelectedProductModel.Text.Trim();
        decimal unitprice = decimal.Parse(TextBox_UnitPrice.Text.Trim());
        string unit = DropDownList_Unit.SelectedValue;
        decimal count = decimal.Parse(TextBox_Count.Text.Trim());
        string remark = TextBox_Remark.Text.Trim();
        string producer = TextBox_Producer.Text.Trim();
        string supplier = TextBox_Supplier.Text.Trim();
        string warehouseid = DropDownList_Warehouse.SelectedValue;
        string warehousename = DropDownList_Warehouse.SelectedItem.Text;
        string sheetname = TextBox_SheetName.Text.Trim();
        string systemid = DropDownList_System.SelectedValue;
        string systemname = DropDownList_System.SelectedItem.Text;
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
            target.Purchaser = Common.Get_UserName;
            target.PurchaserConfirm = true;
            target.PurchaserConfirmTime = DateTime.Now;
            target.PurchaseTime = DateTime.Now;
            target.Status = PurchaseRecordStatus.WAIT4CHECK;
            target.Supplier = supplier;
            target.WarehouseID = warehouseid;
            target.WarehouseName = warehousename;
            target.SystemID = systemid;
            target.SystemName = systemname;
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
            form.CompanyID = UserData.CurrentUserData.CompanyID;
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
            form.SheetID = SheetNOGenerator.GetSheetNO(form.CompanyID, SheetType.EQUIPMENT_CHECKACCEPTANCEFORM);
            form.SubmitTime = DateTime.Now;
            form.Applicant = Common.Get_UserName;
            checkacceptanceBll.SaveCheckAcceptanceFormWithDetail(form);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "提交失败", "报验单提交失败，请刷新后重试", ex,
            Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }

        //发送消息
        foreach (CheckAcceptanceDetailInfo detail in form.DetailList)
        {
            //获取所有的仓管员，发送消息
            IList wks = staffBll.GetAllWarehouseUserByWarehouseID(detail.WarehouseID);//仓管员
            string[] wksarray = new string[wks.Count];
            for (int i = 0; i < wks.Count; i++)
            {
                wksarray[i] = (wks[i] as UserInfo).UserName;
            }

            long thisID = form.ID;
            string title = "报验单验收请求：" +form.SheetID+ form.SheetName + " 需要验收";
            string URL = "../DeviceManager/SpareEquipmentManager/Purchase/CheckInWarehouse/CheckCAForm.aspx?id=" + thisID + "&cmd=history";
            try
            {
                WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, UserData.CurrentUserData.CompanyID, wksarray);
            }
            catch (Exception ex)
            {
                EventMessage.EventWriteLog(Msg_Type.Error, "工作流发送待办事务错误" + ex.Message);
            }
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
           Icon_Type.OK, false, Common.GetHomeBaseUrl("ExecutePurchasing.aspx"), UrlType.Href, "");




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
