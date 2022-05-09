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

public partial class Module_FM2E_DeviceManager_DeviceInfo_WareConsumableEquipmentManager_InWarehouse : System.Web.UI.Page
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

    private void FillData()
    {
        try
        {
            WareHouseConsumableEquipmentInfo item;
            ConsumableEquipment bll = new ConsumableEquipment();
            item = bll.GetExpendasExpendable(id);
            TB_IWName.Text = item.Name;  //产品名称
            TB_IWName.Enabled = false;
            TB_IWModel.Text = item.Model.ToString();  //型号
            TB_IWModel.Enabled = false;
            TB_IWCount.Text = "1";  //数量默认为1
            TB_IWUnit.Text = item.Unit;  //单位
            TB_IWUnit.Enabled = false;
            TB_INDate.Text = DateTime.Now.ToString("yyyy-MM-dd");  //入库时间
            TB_IWPrice.Text = item.Price.ToString("0.##");  //单价
            TB_IWPrice.Enabled = false;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    protected void Button_SaveInWarehouse_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;

        try
        {

            InWarehouseInfo item = new InWarehouseInfo();
            InEquipmentsInfo info = new InEquipmentsInfo();
            info.WarehouseID = CurrentWarehouse.WareHouseID;
            info.IsAsset = false;
            info.EquipmentNO = "";
            info.InTime = Convert.ToDateTime(TB_INDate.Text.Trim());
            info.Name = TB_IWName.Text.Trim();
            info.Model = TB_IWModel.Text.Trim();
            info.Unit = TB_IWUnit.Text.Trim();
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
            item.SubmitTime = Convert.ToDateTime(TB_INDate.Text.Trim());
            //item.ApplicantID = TB_Applicant.Text.Trim();
            item.ApplicantID = UserData.CurrentUserData.UserName;
            item.ApplicantName = UserData.CurrentUserData.PersonName;
            item.OperatorName = UserData.CurrentUserData.PersonName;
            item.OperatorID = Common.Get_UserName;
            item.Remark = TB_IWRemark.Text.Trim();
            item.IsDeleted = false;
            item.InWarehouseList = null;
            item.SheetName = SheetNOGenerator.GetSheetNO(UserData.CurrentUserData.CompanyID, SheetType.INWAREHOUSESHEET);
            InWarehouse inbll = new InWarehouse();
            item.DepartmentID = 0;// Convert.ToInt64(DDL_Department.SelectedValue);
            inbll.InsertInWarehouseExpendable(item, info);

            //入库记录
            ExpendableInOutRecordInfo record = new ExpendableInOutRecordInfo();
            record.InOutTime = Convert.ToDateTime(TB_INDate.Text.Trim());
            record.CompanyID = DDL_Department.SelectedValue;
            if (TB_IWCount.Text.Trim() != "")
                record.Amount = Convert.ToDecimal(TB_IWCount.Text.Trim());
            else
                record.Amount = 1;
            record.Model = TB_IWModel.Text.Trim();
            record.Name = TB_IWName.Text.Trim();
            if (TB_IWPrice.Text.Trim() != "")
                record.Price = Convert.ToDecimal(TB_IWPrice.Text.Trim());
            else
                record.Price = 0;
            record.Receiver = UserData.CurrentUserData.UserName;
            record.ReceiverName = UserData.CurrentUserData.PersonName;
            record.Remark = TB_IWRemark.Text.Trim();
            record.Type = ExpendableInOutRecordType.In;
            record.Unit = TB_IWUnit.Text.Trim();
            record.WarehouseID = CurrentWarehouse.WareHouseID;
            record.WarehouseKeeper = Common.Get_UserName;
            record.WarehouseKeeperName = UserData.CurrentUserData.PersonName;

            //入库，更新入库数量
            ConsumableEquipment bll = new ConsumableEquipment();
            bll.ExpendableInWarehouse(UserData.CurrentUserData.CompanyID, record);

            bSuccess = true;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", ex.Message, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        if (bSuccess == true)
            EventMessage.MessageBox(Msg_Type.Info, "入库成功","仓库设备易耗品入库成功",Icon_Type.OK, true, Common.GetHomeBaseUrl("EquipmentExpendable.aspx"), UrlType.Href, "");
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


}
