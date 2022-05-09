using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using FM2E.BLL.Equipment;
using FM2E.BLL.Utils;
using FM2E.Model.Utils;
using WebUtility.Components;
using FM2E.Model.System;
using FM2E.Model.Equipment;
using FM2E.BLL.Basic;
using System.Collections;
using FM2E.Model.Basic;
using FM2E.BLL.System;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_WareHouseCheck_WareHouseCheck_EditCheckForm : System.Web.UI.Page
{
    public string cmd = (string)Common.sink("cmd", MethodType.Get, 10, 0, DataType.Str);
    private string formNO = (string)Common.sink("formNO", MethodType.Get, 30, 0, DataType.Str);
    private readonly WareHouseCheck warehouseCheck = new WareHouseCheck();
    private readonly Company company = new Company();
    private readonly User user = new User();

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            ButtonBind();
        }
    }
    private void InitialPage()
    {
        try
        {
            Warehouse warehouse = new Warehouse();
            IList<WarehouseInfo> list = warehouse.GetAllWarehouse();

            ddlWareHouse.Items.Clear();
            ddlWareHouse.Items.Add(new ListItem("请选择仓库", "0"));
            foreach (WarehouseInfo item in list)
            {
                ddlWareHouse.Items.Add(new ListItem(item.Name, item.WareHouseID));
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "页面初始化失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 填充页面数据
    /// </summary>
    private void FillData()
    {
        try
        {
            LoginUserInfo userInfo=UserData.CurrentUserData;
            if (cmd == "add")
            {
                lbCompanyName.Text = userInfo.CompanyName;
                tbCheckDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //生成表单编号
                lbFormNO.Text = SheetNOGenerator.GetSheetNO(userInfo.CompanyID, SheetType.WAREHOUSE_CHECKFORM);
                //检查人员
                lbChecker.Text = string.Format("{0}（{1}）", userInfo.PersonName, userInfo.UserName);
            }
            else if (cmd == "edit")
            {
                WareHouseCheckInfo item = warehouseCheck.GetWareHouseCheck(formNO);
                if (item == null) return;

                lbCompanyName.Text = company.GetCompany(item.CompanyID).CompanyName;
                ddlWareHouse.SelectedValue = item.WareHouseID;
                tbCheckDate.Text = item.CheckDate.ToString("yyyy-MM-dd");
                lbFormNO.Text = item.FormNO;
                //材料类型
                hdMaterialType.Value = ((int)item.MaterialType).ToString();
                //检查人员
                lbChecker.Text = string.Format("{0}（{1}）", userInfo.PersonName, userInfo.UserName);
                //抽查情况
                tbSpotCheck.Text = item.SpotCheck;
                //盘点情况
                tbStockCount.Text = item.StockCount;
                //数量情况
                hdQuantitySituation.Value = ((int)item.QuantitySituation).ToString();
                //质量情况
                hdQualitySituation.Value = ((int)item.QualitySituation).ToString();
                //表单登记情况
                hdRegSituation.Value = ((int)item.RegSituation).ToString();
                //有无异常情况
                tbExceptionSituation.Text = item.ExceptionSituation;
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "加载页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 菜单绑定
    /// </summary>
    private void ButtonBind()
    {
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：填写仓库检查表";
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：修改仓库检查表";
        }
    }
    /// <summary>
    /// 保存草稿
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSave_Click(object sender, EventArgs e)
    {
        ValidateInput();
          
        WareHouseCheckInfo item = new WareHouseCheckInfo();
        try
        {

            item.FormNO = lbFormNO.Text.Trim();
            item.CompanyID = UserData.CurrentUserData.CompanyID;
            item.WareHouseID = ddlWareHouse.SelectedValue;
            item.CheckDate = Convert.ToDateTime(tbCheckDate.Text.Trim());
            item.Status = WareHouseFormStatus.Draft;
            item.MaterialType = (MaterialType)Convert.ToInt32(hdMaterialType.Value.Trim());
            item.Checker = Common.Get_UserName;
            item.SpotCheck = tbSpotCheck.Text.Trim();
            item.StockCount = tbStockCount.Text.Trim();
            item.QuantitySituation = (QuantitySituation)Convert.ToInt32(hdQuantitySituation.Value.Trim());
            item.QualitySituation = (QualitySituation)Convert.ToInt32(hdQualitySituation.Value.Trim());
            item.RegSituation = (RegSituation)Convert.ToInt32(hdRegSituation.Value.Trim());
            item.ExceptionSituation = tbExceptionSituation.Text.Trim();
            item.UpdateTime = DateTime.Now;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存仓库检查表失败：获取参数失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        if (cmd == "add")
        {
            try
            {
                warehouseCheck.AddWareHouseCheck(item);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存仓库检查表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "保存仓库检查表成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("WareHouseCheck.aspx"), UrlType.Href, "");

        }
        else if (cmd == "edit")
        {
            try
            {
                warehouseCheck.UpdateWareHouseCheck(item);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改仓库检查表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改仓库检查表成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("WareHouseCheck.aspx"), UrlType.Href, "");
        }
    }
    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSubmit_Click(object sender, EventArgs e)
    {
        ValidateInput();
        WareHouseCheckInfo item = new WareHouseCheckInfo();
        try
        {

            item.FormNO = lbFormNO.Text.Trim();
            item.CompanyID = UserData.CurrentUserData.CompanyID;
            item.WareHouseID = ddlWareHouse.SelectedValue;
            item.CheckDate = Convert.ToDateTime(tbCheckDate.Text.Trim());
            item.Status = WareHouseFormStatus.Committed;
            item.MaterialType = (MaterialType)Convert.ToInt32(hdMaterialType.Value.Trim());
            item.Checker = Common.Get_UserName;
            item.SpotCheck = tbSpotCheck.Text.Trim();
            item.StockCount = tbStockCount.Text.Trim();
            item.QuantitySituation = (QuantitySituation)Convert.ToInt32(hdQuantitySituation.Value.Trim());
            item.QualitySituation = (QualitySituation)Convert.ToInt32(hdQualitySituation.Value.Trim());
            item.RegSituation = (RegSituation)Convert.ToInt32(hdRegSituation.Value.Trim());
            item.ExceptionSituation = tbExceptionSituation.Text.Trim();
            item.UpdateTime = DateTime.Now;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交仓库检查表失败：获取参数失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        if (cmd == "add")
        {
            try
            {
                warehouseCheck.AddWareHouseCheck(item);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交仓库检查表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "提交仓库检查表成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("WareHouseCheck.aspx"), UrlType.Href, "");

        }
        else if (cmd == "edit")
        {
            try
            {
                warehouseCheck.UpdateWareHouseCheck(item);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交仓库检查表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "提交仓库检查表成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("WareHouseCheck.aspx"), UrlType.Href, "");
        }
    }
    /// <summary>
    /// 用户输入校验
    /// </summary>
    private void ValidateInput()
    {
        string errorMsg = "";
        if (ddlWareHouse.SelectedIndex == 0)
        {
            errorMsg = "请选择仓库";
        }else if (tbCheckDate.Text.Trim() == "")
        {
            errorMsg = "检查日期不能为空";
        }
        else if (hdMaterialType.Value.Trim() == "")
        {
            errorMsg = "请选择材料类型";
        }
        else if (hdQuantitySituation.Value.Trim() == "")
        {
            errorMsg = "请选择数量情况";
        }
        else if (hdQualitySituation.Value.Trim() == "")
        {
            errorMsg = "请选择质量情况";
        }
        else if (hdRegSituation.Value.Trim() == "")
        {
            errorMsg = "请选择表单登记情况";
        }

        try
        {
            Convert.ToDateTime(tbCheckDate.Text.Trim());
        }
        catch
        {
            errorMsg = "检查日期格式不正确";
        }

        if (errorMsg != "")
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入有误：" + errorMsg, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
    }

}
