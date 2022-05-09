using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;
using FM2E.BLL.Basic;
using FM2E.BLL.System;
using FM2E.BLL.Utils;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using System.Collections.Generic;
using FM2E.WorkflowLayer;

public partial class Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_OutWarehouse : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 255, 0, DataType.Long);
    /// <summary>
    /// 校验用户名
    /// </summary>
    User userBll = new User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
            FillData();
        }
    }

    private void InitPage()
    {
        //公司;
        DDL_OutDepartment.Items.Clear();
        //DDL_OutDepartment.Items.AddRange(ListItemHelper.GetDepartmentListItemsWithBlank(UserData.CurrentUserData.CompanyID));
        DDL_OutDepartment.Items.AddRange(ListItemHelper.GetCompanyListItemsWithBlank());
        try
        {
            DDL_OutDepartment.SelectedValue = UserData.CurrentUserData.CompanyID;
        }
        catch { }

        Warehouse bllstaff = new Warehouse();
        WarehouseInfo warehouse = bllstaff.GetWarehouseByUserName(UserData.CurrentUserData.UserName);
    }

    private void FillData()
    {
        try
        { 
            Expendable bll = new Expendable();
            if (cmd == "add")
            {
                ExpendableInfo item;
             
                item = bll.GetExpendable(id);
                TB_OWName.Text = item.Name;  //产品名称;
                TB_OWName.Enabled = false;
                TB_OWModel.Text = item.Model.ToString();  //型号;
                TB_OWModel.Enabled = false;
                TB_OWCount.Text = "1";  //数量默认为1
                TB_OWUnit.Text = item.Unit;  //单位
                TB_OWUnit.Enabled = false;
                TB_OWCategory.Text = item.CategoryName;  //种类;
                TB_OWCategory.Enabled = false;
                TB_OWCategoryID.Text = item.CategoryID.ToString();
                TB_OWPrice.Text = item.Price.ToString("0.##");  //单价;
                TB_OWPrice.Enabled = false;

                OutWarehouseText.Text = "_____________";
                Reset1.Style.Add("display", "");
                Reset2.Style.Add("display", "none");
            }
            else if (cmd == "edit")
            {
                OutWarehouseInfo info = bll.GetOutWarehouse(id);
                DDL_OutDepartment.SelectedValue = info.CompanyName;
                OutWarehouseText.Text = info.SheetName;
                TB_OWRemark.Text = info.Remark;
                TB_OWName.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.OutWarehouseList[0])).Name;
                TB_OWName.Enabled = false;
                TB_OWModel.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.OutWarehouseList[0])).Model;
                TB_OWModel.Enabled = false;
                TB_OWCount.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.OutWarehouseList[0])).Count.ToString();
                TB_OWUnit.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.OutWarehouseList[0])).Unit;
                TB_OWUnit.Enabled = false;
                TB_OWCategory.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.OutWarehouseList[0])).ExpendableType;
                TB_OWCategoryID.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.OutWarehouseList[0])).ExpendableTypeID.ToString();
                TB_OWCategory.Enabled = false;
                TB_OWPrice.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.OutWarehouseList[0])).ExpendablePrice.ToString();
                Reset1.Style.Add("display", "none");
                Reset2.Style.Add("display", " ");
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    
    protected void Button_SaveOutWarehouse_Click(object sender, EventArgs e)
    {
        string title = "";
        string URL = "";

        //出库申请记录
        InEquipmentsInfo record = new InEquipmentsInfo();
        OutWarehouseInfo item = null;
        Expendable bll = new Expendable();
        if (cmd == "edit")
        {
            item = new OutWarehouseInfo();
        }
        else if (cmd == "add")
        {
            item = new OutWarehouseInfo();
        }
        try
        {
            record.WarehouseID = CurrentWarehouse.WareHouseID;
            record.IsAsset = false;
            record.EquipmentNO = "";
            record.Name = TB_OWName.Text.Trim();
            record.Model = TB_OWModel.Text.Trim();
            record.Unit = TB_OWUnit.Text.Trim();
            record.ExpendableTypeID = Convert.ToInt64(TB_OWCategoryID.Text.Trim());
            record.ExpendableType = TB_OWCategory.Text.Trim();
            if (TB_OWPrice.Text.Trim() != "")
                record.ExpendablePrice = Convert.ToDecimal(TB_OWPrice.Text.Trim());
            else
                record.ExpendablePrice = 0;
            if (TB_OWCount.Text.Trim() != "")
                record.Count = Convert.ToDecimal(TB_OWCount.Text.Trim());
            else
                record.Count = 1;

            item.CompanyID = DDL_OutDepartment.SelectedValue; // UserData.CurrentUserData.CompanyID;
            item.WarehouseID = CurrentWarehouse.WareHouseID;
            item.WarehouseAddressID = CurrentWarehouse.AddressID;
            item.WarehouseDetailLocation = "";
            item.SubmitTime = DateTime.Now;
            item.ApplicantID = UserData.CurrentUserData.UserName;
            item.ApplicantName = UserData.CurrentUserData.PersonName;
            item.OperatorName = UserData.CurrentUserData.PersonName;
            item.OperatorID = Common.Get_UserName;
            item.Remark = TB_OWRemark.Text.Trim();
            item.IsDeleted = false;
            item.OutWarehouseList = null;
            item.SheetName = SheetNOGenerator.GetSheetNO(UserData.CurrentUserData.CompanyID, SheetType.INWAREHOUSESHEET);
            item.DepartmentID = DDL_OutDepartment.SelectedIndex;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交申请失败：获取参数失败", ex, Icon_Type.Error, true,
                "window.history.go(-1)", UrlType.JavaScript, "");
        }


        if (cmd == "add")
        {
            try
            {
                item.ID = bll.ExpendableOutRequest(item,record);
                URL = "../DeviceManager/DeviceInfo/ExpendableInfo/OutWarehouseApply.aspx";
                title = "你有新的易耗品出库申请" + item.SheetName + "待审批";
                WorkflowApplication.CreateWorkflowAndSendingPendingOrder1<SGS_OutWarehouseEventService>(item.ID, title, SGS_OutWarehouseWorkflow.WorkflowName, SGS_OutWarehouseWorkflow.AppSubmitedEvent, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, item.CompanyID);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "出库申请失败", "易耗品出库申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        else if (cmd == "edit")
        {
            try
            {
                URL = "../DeviceManager/DeviceInfo/ExpendableInfo/OutWarehouseApply.aspx";
                title = "你有新的易耗品出库申请" + item.SheetName + "待审批";
                WorkflowApplication.SetStateMachineAndSendingPendingOrderAndNextUserMachine<SGS_OutWarehouseEventService>(id, title, URL, SGS_OutWarehouseWorkflow.WorkflowName, "SubmitReturnModify", SGS_OutWarehouseWorkflow.TableName, Common.Get_UserName, UserData.CurrentUserData.PersonName, 0, null);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "出库申请失败", "保存修改失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }

       EventMessage.MessageBox(Msg_Type.Info, "操作成功", string.Format("提交易耗品出库申请单成功,故障处理单号为：{0}", item.SheetName), Icon_Type.OK, true, Common.GetHomeBaseUrl("OutWarehouse.aspx"), UrlType.Href, "");
    }

    private WarehouseInfo CurrentWarehouse
    {
        get
        {
            WarehouseInfo warehouse = (WarehouseInfo)ViewState["CurrentWarehouse"];
            if (warehouse == null)
            {
                warehouse = new Warehouse().GetWarehouseByUserName(UserData.CurrentUserData.UserName);
            }
            return warehouse;
        }
        set
        {
            ViewState["CurrentWarehouse"] = value;
        }
    }
}
