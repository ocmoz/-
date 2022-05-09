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
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using System.Collections.Generic;
using FM2E.Model.Exceptions;

public partial class Module_FM2E_DeviceManager_DeviceInfo_WareConsumableEquipmentManager_EquipmentExpendableRecord : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    private readonly ConsumableEquipment bll = new ConsumableEquipment();
    protected string IsShow = "block";
    protected string IsShow2 = "block";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
        }
    }

    // 页面初始化
    private void InitialPage()
    {
        try
        {
            //系统
            EquipmentSystem systemBll = new EquipmentSystem();
            IList sysList = systemBll.GetAllSystem();

            foreach (EquipmentSystemInfo sys in sysList)
            {
                DDL_System.Items.Add(new ListItem(sys.SystemName, sys.SystemID));
            }
            //仓库
            Warehouse whbll = new Warehouse();
            List<WarehouseInfo> wareHouseList = whbll.GetWarehouseListByUserName(UserData.CurrentUserData.UserName);
            foreach (WarehouseInfo whInfo in wareHouseList)
            {
                DropDownList_FilterWareHouse.Items.Add(new ListItem(whInfo.Name, whInfo.WareHouseID));
            }
            //单位
            DDL_Unit.Items.Clear();
            IList list = Constants.GetUnits();
            foreach (string unit in list)
                DDL_Unit.Items.Add(new ListItem(unit, unit));
            DDL_Unit.Items.Insert(0, new ListItem("请选择单位", ""));

            EquipmentDetailList = null;  //清空
        }
        catch (Exception ex)
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "初始化页面失败：" + ex.Message);
        }
    }
    //填充数据
    private void FillData()
    {
        if (cmd == "edit")
        {
            try
            {
                WareHouseConsumableEquipmentInfo item = bll.GetExpendasExpendable(id);
                //填充页面
                //tbConsumableEquipmentNO.Text = item.ConsumableEquipmentNO;
                tbName.Text = item.Name;
                DDL_System.SelectedValue = item.SystemID;
                tbSerialNum.Text = item.SerialNum;
                tbModel.Text = item.Model;
                tbSpecification.Text = item.Specification;
                tbAssertNumber.Text = item.AssertNumber;
                DDL_Unit.SelectedValue = item.Unit;
                tbCount.Text = (item.Count).ToString();
                tbPrice.Text = (item.Price).ToString();
                DropDownList_FilterWareHouse.SelectedValue = item.WareHouseID;
                //tbSupplierID.Text = (item.SupplierID).ToString();
                //tbProducerID.Text = (item.ProducerID).ToString();
                //tbSupplierName.Text = item.SupplierName;
                //tbProducerName.Text = item.ProducerName;
                //if (DateTime.Compare(item.FileDate, DateTime.MinValue) != 0)
                //{
                //    tbFileDate.Text = item.FileDate.ToShortDateString();
                //}
                EquipmentDetailList = item.WareHouseConsumableEquipmentDetailList;
                //tbMaintenanceTimes.Text = (item.MaintenanceTimes).ToString();
                tbRemark.Text = item.Remark;
                //if (DateTime.Compare(item.UpdateTime, DateTime.MinValue) != 0)
                //{
                //    tbUpdateTime.Text = item.UpdateTime.ToShortDateString();
                //}

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "加载信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }
    /// <summary>
    /// 仓库设备易耗品详细信息列表
    /// </summary>
    private IList EquipmentDetailList
    {
        get
        {
            if (Session["EquipmentDetailList"] == null)
                return new ArrayList();
            return (ArrayList)Session["EquipmentDetailList"];
        }
        set
        {
            Session["EquipmentDetailList"] = value;
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        WareHouseConsumableEquipmentInfo item = new WareHouseConsumableEquipmentInfo();
        //获取填入数据
        item.ConsumableEquipmentID = id;
        //生成设备配套设施的编号，与设备编号对应。
        DateTime date = Convert.ToDateTime("2011-1-1");
        item.ConsumableEquipmentNO = FM2E.BLL.BarCode.BarCode.GenerateBarCode("YH", date, false);
        item.Name = tbName.Text.Trim();
        item.SystemID = DDL_System.SelectedValue;
        item.SerialNum = tbSerialNum.Text.Trim();
        item.Model = tbModel.Text.Trim();
        item.Specification = tbSpecification.Text.Trim();
        item.AssertNumber = tbAssertNumber.Text.Trim();
        item.Unit = DDL_Unit.SelectedValue;
        item.Count = Convert.ToInt32(tbCount.Text.Trim());
        item.Price = Convert.ToDecimal(tbPrice.Text.Trim());
        //所在仓库编号WareHouseID
        item.WareHouseID = DropDownList_FilterWareHouse.SelectedValue;
        item.ProducerID = Convert.ToInt32(tbProduceID.Text.Trim());
        //供应商、生产商
        //item.SupplierID = Convert.ToInt64(tbSupplierID.Text.Trim());
        //item.ProducerID = Convert.ToInt64(tbProducerID.Text.Trim());
        //item.SupplierName = tbSupplierName.Text.Trim();
        //item.ProducerName = tbProducerName.Text.Trim();
        item.FileDate = DateTime.Now;
        //维修次数
        //item.MaintenanceTimes = Convert.ToInt32(tbMaintenanceTimes.Text.Trim());
        item.Remark = tbRemark.Text.Trim();
        item.UpdateTime = DateTime.Now;

        if (DropDownList_FilterWareHouse.SelectedValue.Trim() != "select")
        {
            WareHouseConsumableEquipmentInfo ep = new WareHouseConsumableEquipmentInfo();
            ConsumableEquipmentDetailInfo ced = new ConsumableEquipmentDetailInfo();
            ced = bll.GetEquipmentDetailByWarehouseID(DropDownList_FilterWareHouse.SelectedValue);
            ep.AddressID = ced.AddressID;
            ep.CompanyID = ced.CompanyID;
            ep.DetailLocation = ced.DetailLocation;
            ep.Count = ced.Count;
            ep.Remark = ced.Remark;
            IList equipmentparts = EquipmentDetailList;
            equipmentparts.Clear();
            equipmentparts.Add(ep);
            EquipmentDetailList = equipmentparts;
        }

        item.WareHouseConsumableEquipmentDetailList = EquipmentDetailList;
        //增加
        if (cmd == "add")
        {
            bool bSuccess = false;
            try
            {
                bll.InsertExpendasExpendable(item);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加失败" + ex.Message, ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("EquipmentExpendable.aspx"), UrlType.Href, "");
            }
        }
        //修改
        else if (cmd == "edit")
        {
            bool bSuccess = false;
            try
            {
                bll.UpdateExpendasExpendable(item);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("EquipmentExpendable.aspx"), UrlType.Href, "");
            }
        }
    }
    protected void DropDownList_FilterWareHouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_FilterWareHouse.SelectedValue.Trim() != "select")
        {
            WareHouseConsumableEquipmentInfo ep = new WareHouseConsumableEquipmentInfo();
            ConsumableEquipmentDetailInfo ced = new ConsumableEquipmentDetailInfo();
            ced = bll.GetEquipmentDetailByWarehouseID(DropDownList_FilterWareHouse.SelectedValue);
            ep.AddressID = ced.AddressID;
            ep.CompanyID = ced.CompanyID;
            ep.DetailLocation = ced.DetailLocation;
            ep.Count = ced.Count;
            ep.Remark = ced.Remark;
            IList equipmentparts = EquipmentDetailList;
            equipmentparts.Clear();
            equipmentparts.Add(ep);
            EquipmentDetailList = equipmentparts;
        }
    }
}
