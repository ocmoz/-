using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using WebUtility.Components;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using FM2E.BLL.Basic;

public partial class Module_FM2E_DeviceManager_DeviceInfo_SubsidiaryEquipmentManager_EditSubsidiaryEquipment : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 20, 0, DataType.Long);
    private readonly SubsidiaryEquipment bll = new SubsidiaryEquipment();
    //加载页面
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            ButtonBind();
        }
    }
    // 页面初始化
    private void InitialPage()
    {
        try
        {
            ListItem[] StatusTypeItems = EnumHelper.GetListItems(typeof(EquipmentStatus), (int)EquipmentStatus.Unknown);
            ddlStatus.Items.Clear();
            ddlStatus.Items.Add(new ListItem("不限", ((int)EquipmentStatus.Unknown).ToString()));
            ddlStatus.Items.AddRange(StatusTypeItems);
            ddlStatus.SelectedValue = ((int)(EquipmentStatus.Normal)).ToString();
            //所属系统
            EquipmentSystem systemBll = new EquipmentSystem();
            DDL_System.Items.AddRange(systemBll.GenerateListItemCollectionWithBlank());
            //公司
            DDL_Company.Items.Clear();
            DDL_Company.Items.AddRange(ListItemHelper.GetCompanyListItemsWithBlank());
            DDL_Company.SelectedValue = UserData.CurrentUserData.CompanyID;
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
                SubsidiaryEquipmentInfo item = bll.GetSubsidiaryEquipment(id);
                //填充页面
                tbName.Text = item.Name;
                DDL_Company.SelectedValue = item.CompanyName;
                DDL_System.SelectedValue = item.SystemID;
                tbModel.Text = item.Model;
                tbSpecification.Text = item.Specification;
                TextBox_DetailLocation.Text = item.DetailLocation;
                Hidden_AddressID.Value = item.AddressID.ToString();
                TextBox_Address.Value = item.AddressName;
                tbAssertNumber.Text = item.AssertNumber;
                tbPrice.Text = (item.Price).ToString();
                ddlStatus.SelectedValue = ((int)item.Status).ToString();
                tbMaintenanceTimes.Text = (item.MaintenanceTimes).ToString();
                tbRemark.Text = item.Remark;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "加载信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }
    //绑定按钮
    private void ButtonBind()
    {
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：设备配套设施信息添加";
        }
        if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：设备配套设施信息信息修改";
        }
    }
    //确定按钮事件
    protected void btSave_Click(object sender, EventArgs e)
    {
        SubsidiaryEquipmentInfo item = new SubsidiaryEquipmentInfo();
        //获取填入数据
        item.SubsidiaryEquipmentID = id;
        item.Name = tbName.Text.Trim();
        item.CompanyID = DDL_Company.SelectedValue;
        item.SystemID = DDL_System.SelectedValue;
        item.Model = tbModel.Text.Trim();
        item.Specification = tbSpecification.Text.Trim();
        item.DetailLocation = TextBox_DetailLocation.Text.Trim();
        long addressid = 0;
        long.TryParse(Hidden_AddressID.Value, out addressid);
        item.AddressID = addressid;
        item.AssertNumber = tbAssertNumber.Text.Trim();
        item.Price = Convert.ToDecimal(tbPrice.Text.Trim());
        item.Status = (EquipmentStatus)Convert.ToInt32(ddlStatus.SelectedValue);
        item.MaintenanceTimes = Convert.ToInt32(tbMaintenanceTimes.Text.Trim());
        item.Remark = tbRemark.Text.Trim();
        item.IsCancel = false;
        item.UpdateTime = DateTime.Now;
        //增加
        if (cmd == "add")
        {
            bool bSuccess = false;
            try
            {
                item.FileDate = DateTime.Now;
                //生成设备配套设施的编号，与设备编号对应。
                DateTime date = Convert.ToDateTime("2011-1-1");
                item.SubsidiaryEquipmentNO = FM2E.BLL.BarCode.BarCode.GenerateBarCode("FS", date, false);
                bll.InsertSubsidiaryEquipment(item);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("SubsidiaryEquipment.aspx"), UrlType.Href, "");
            }
        }
        //修改
        else if (cmd == "edit")
        {
            bool bSuccess = false;
            try
            {
                SubsidiaryEquipmentInfo tempitem = bll.GetSubsidiaryEquipment(id);
                item.SubsidiaryEquipmentNO = tempitem.SubsidiaryEquipmentNO;
                item.CatalogID = tempitem.CatalogID;
                item.CatalogName = tempitem.CatalogName;
                item.FileDate = tempitem.FileDate;
                bll.UpdateSubsidiaryEquipment(item);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("SubsidiaryEquipment.aspx"), UrlType.Href, "");
            }
        }
    }
}
