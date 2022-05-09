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

using System.IO;
using FM2E.Model.Exceptions;
using FM2E.Model.System;
using FM2E.WorkflowLayer;

public partial class Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_InWarehouse : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 255, 0, DataType.Long);

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
        //部门
        DDL_Department.Items.Clear();
        //DDL_Department.Items.AddRange(ListItemHelper.GetDepartmentListItemsWithBlank(UserData.CurrentUserData.CompanyID));
        DDL_Department.Items.AddRange(ListItemHelper.GetCompanyListItemsWithBlank());
        try
        {
            DDL_Department.SelectedValue = UserData.CurrentUserData.CompanyID;
        }
        catch { }

        Warehouse bllstaff = new Warehouse();
        WarehouseInfo warehouse = bllstaff.GetWarehouseByUserName(UserData.CurrentUserData.UserName);

    }

    InWarehouse inbll = new InWarehouse();
    private void FillData()
    {
        try
        {
            if (cmd == "add")
            {
                Expendable bll = new Expendable();
                ExpendableInfo item = bll.GetExpendable(id);

                TB_IWName.Text = item.Name; //产品名称
                TB_IWName.Enabled = false;
                TB_IWModel.Text = item.Model.ToString(); //型号
                TB_IWModel.Enabled = false;

                TB_IWUnit.Text = item.Unit; //单位
                TB_IWUnit.Enabled = false;
                TB_IWCategory.Text = item.CategoryName; //种类
                TB_IWCategory.Enabled = false;
                TB_IWCategoryID.Text = item.CategoryID.ToString();
                TB_IWPrice.Text = item.Price.ToString("0.##"); //单价
                TB_IWPrice.Enabled = false;

                TB_IWCount.Text = "1"; //数量默认为1
                InWarehouseText.Text = "_____________";

                Reset2.Style.Add("display", "none");
            }
            else if (cmd == "edit")
            {
                InWarehouseInfo info = inbll.GetInWarehouse(id);
                DDL_Department.SelectedValue = info.CompanyName;
                InWarehouseText.Text = info.SheetName;
                TB_IWRemark.Text = info.Remark;
                TB_IWName.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.InWarehouseList[0])).Name;
                TB_IWName.Enabled = false;
                TB_IWModel.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.InWarehouseList[0])).Model;
                TB_IWModel.Enabled = false;
                TB_IWCount.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.InWarehouseList[0])).Count.ToString();
                TB_IWUnit.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.InWarehouseList[0])).Unit;
                TB_IWUnit.Enabled = false;
                TB_IWCategory.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.InWarehouseList[0])).ExpendableType;
                TB_IWCategoryID.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.InWarehouseList[0])).ExpendableTypeID.ToString();
                TB_IWCategory.Enabled = false;
                TB_IWPrice.Text = ((FM2E.Model.Equipment.InEquipmentsInfo)(info.InWarehouseList[0])).ExpendablePrice.ToString();
                Reset1.Style.Add("display", "none");
                Reset2.Style.Add("display", " ");
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    protected void Button_SaveInWarehouse_Click(object sender, EventArgs e)
    {
        InWarehouse inbll = new InWarehouse();
        InWarehouseInfo item=null;
        InEquipmentsInfo info = new InEquipmentsInfo();

        string title = "";
        string URL = "";

        if (cmd == "edit")
        {
            item = inbll.GetInWarehouse(id);
        }
        else if (cmd == "add")
        {
            item = new InWarehouseInfo();
        }
        try
        {
            info.WarehouseID = CurrentWarehouse.WareHouseID;
            info.IsAsset = false;
            info.EquipmentNO = "";
            info.Name = TB_IWName.Text.Trim();
            info.Model = TB_IWModel.Text.Trim();
            info.Unit = TB_IWUnit.Text.Trim();
            info.ExpendableTypeID = Convert.ToInt64(TB_IWCategoryID.Text.Trim());
            info.ExpendableType = TB_IWCategory.Text.Trim();
            if (TB_IWPrice.Text.Trim() != "")
                info.ExpendablePrice = Convert.ToDecimal(TB_IWPrice.Text.Trim());
            else
                info.ExpendablePrice = 0;
            if (TB_IWCount.Text.Trim() != "")
                info.Count = Convert.ToDecimal(TB_IWCount.Text.Trim());
            else
                info.Count = 1;

            item.CompanyID = DDL_Department.SelectedValue; // UserData.CurrentUserData.CompanyID;
            item.WarehouseID = CurrentWarehouse.WareHouseID;
            item.WarehouseAddressID = CurrentWarehouse.AddressID;
            item.WarehouseDetailLocation = "";
            item.SubmitTime = DateTime.Now;
            item.ApplicantID = UserData.CurrentUserData.UserName;
            item.ApplicantName = UserData.CurrentUserData.PersonName;
            item.OperatorName = UserData.CurrentUserData.PersonName;
            item.OperatorID = Common.Get_UserName;
            item.Remark = TB_IWRemark.Text.Trim();
            item.IsDeleted = false;
            item.InWarehouseList = null;
            item.SheetName = SheetNOGenerator.GetSheetNO(UserData.CurrentUserData.CompanyID, SheetType.INWAREHOUSESHEET);
            item.DepartmentID = DDL_Department.SelectedIndex;


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
                item.ID = inbll.InsertInWarehouseExpendable(item, info); //插入一条申请记录,返回最后插入的ID 
                URL = "../DeviceManager/DeviceInfo/ExpendableInfo/InWarehouseApply.aspx";
                title = "你有新的易耗品入库申请" + item.SheetName + "待审批";
                WorkflowApplication.CreateWorkflowAndSendingPendingOrder1<SGS_InWarehouseEventService>(item.ID, title, SGS_InWarehouseWorkflow.WorkflowName, SGS_InWarehouseWorkflow.AppSubmitedEvent, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, item.CompanyID);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交申请失败", ex, Icon_Type.Error, true,
                     Common.GetHomeBaseUrl("InWarehouseApprovalList.aspx"), UrlType.Href, "");
            }
        }
        else if (cmd == "edit")
        {
            try
            {
                item.ID = id;
                item.ID = inbll.SavaOutWarehouseApply(item, info);

                URL = "../DeviceManager/DeviceInfo/ExpendableInfo/InWarehouseApply.aspx";
                title = "你有新的易耗品入库申请" + item.SheetName + "待审批";
                WorkflowApplication.SetStateMachineAndSendingPendingOrderAndNextUserMachine<SGS_InWarehouseEventService>(id, title, URL, SGS_InWarehouseWorkflow.WorkflowName, "SubmitReturnModify", SGS_InWarehouseWorkflow.TableName, Common.Get_UserName, UserData.CurrentUserData.PersonName, 0, null);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存修改失败", ex, Icon_Type.Error, true,
                    "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", string.Format("提交易耗品入库申请单成功,故障处理单号为：{0}", item.SheetName), Icon_Type.OK, true, Common.GetHomeBaseUrl("InWarehouse.aspx"), UrlType.Href, "");

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

    private IList InWarehouseList
    {
        get
        {
            IList list = (IList)Session[this.ToString()];
            if (list == null)
                list = new List<InEquipmentsInfo>();
            return list;
        }
        set
        {
            Session[this.ToString()] = value;
        }
    }

    private InWarehouseInfo CurrentInWarehouseInfo
    {
        get
        {
            InWarehouseInfo item = (InWarehouseInfo)Session[this.ToString()];
            if (item == null)
            {
                if (cmd == "edit")
                {
                    item = inbll.GetInWarehouse(id);
                    Session[this.ToString()] = item;
                }
                if (cmd == "add")
                {
                    item = new InWarehouseInfo();
                    Session[this.ToString()] = item;
                }
            }
            return item;
        }
        set
        {
            Session[this.ToString()] = value;
        }
    }
}