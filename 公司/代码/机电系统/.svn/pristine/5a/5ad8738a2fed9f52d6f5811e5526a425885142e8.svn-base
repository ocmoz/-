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

public partial class Module_FM2E_DeviceManager_DeviceInfo_WareConsumableEquipmentManager_OutWarehouse : System.Web.UI.Page
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
        //公司
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
        WareHouseConsumableEquipmentInfo item;
        ConsumableEquipment bll = new ConsumableEquipment();
        item = bll.GetExpendasExpendable(id);
        TB_OWName.Text = item.Name;  //产品名称
        TB_OWName.Enabled = false;
        TB_OWModel.Text = item.Model.ToString();  //型号
        TB_OWModel.Enabled = false;
        TB_OWCount.Text = "1";  //数量默认为1
        TB_OWUnit.Text = item.Unit;  //单位
        TB_OWUnit.Enabled = false;
        TB_OWDate.Text = DateTime.Now.ToString("yyyy-MM-dd");  //入库时间
        TB_OWPrice.Text = item.Price.ToString("0.##");  //单价
        TB_OWPrice.Enabled = false;
    }

    protected void Button_SaveOutWarehouse_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;

        try
        {
            //出库记录
            ExpendableInOutRecordInfo record = new ExpendableInOutRecordInfo();
            record.InOutTime = Convert.ToDateTime(TB_OWDate.Text.Trim());
            record.CompanyID = DDL_OutDepartment.SelectedValue;
            record.Amount = decimal.Parse(TB_OWCount.Text.Trim());
            record.Model = TB_OWModel.Text.Trim();
            record.Name = TB_OWName.Text.Trim();
            record.Price = decimal.Parse(TB_OWPrice.Text.Trim());
            record.Receiver = TB_HandlerID.Text.Trim();
            record.ReceiverName = TB_HandlerName.Text.Trim();
            record.Remark = TB_OWRemark.Text.Trim();
            record.Type = ExpendableInOutRecordType.Out;
            record.Unit = TB_OWUnit.Text.Trim();
            record.WarehouseID = CurrentWarehouse.WareHouseID;
            record.WarehouseKeeper = UserData.CurrentUserData.UserName;
            record.WarehouseKeeperName = UserData.CurrentUserData.PersonName;

            //出库，顺便减去出库数量
            ConsumableEquipment bll = new ConsumableEquipment();
            bll.ExpendableOutWarehouse(UserData.CurrentUserData.CompanyID, record);

            bSuccess = true;

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", ex.Message, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        if (bSuccess == true)
            EventMessage.MessageBox(Msg_Type.Info, "出库成功", "仓库设备易耗品出库成功",Icon_Type.OK, true, Common.GetHomeBaseUrl("EquipmentExpendable.aspx"), UrlType.Href, "");
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
