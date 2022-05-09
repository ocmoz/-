using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using FM2E.BLL.Equipment;
using WebUtility.Components;
using FM2E.Model.Equipment;
using FM2E.BLL.Basic;
using FM2E.BLL.System;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_WareHouseCheck_WareHouseCheck_ViewWareHouseCheckForm : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 20, 0, DataType.Str);
    private string id = (string)Common.sink("id", MethodType.Get, 20, 0, DataType.Str);
    private readonly WareHouseCheck wareHouseCheck = new WareHouseCheck();
    private readonly Company company = new Company();
    private readonly User user = new User();
    private readonly Warehouse warehouse = new Warehouse();
    private WareHouseFormStatus status = WareHouseFormStatus.Unknown;

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            FillData();
            ButtonBind();
        }
    }
    /// <summary>
    /// 菜单绑定
    /// </summary>
    private void ButtonBind()
    {
        HeadMenuWebControls1.ButtonList[0].ButtonUrl = "EditCheckForm.aspx?cmd=edit&formNO=" + id;
        HeadMenuWebControls1.ButtonList[0].ButtonUrlType = UrlType.Href;

        HeadMenuWebControls1.ButtonList[1].ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');", id);
        HeadMenuWebControls1.ButtonList[1].ButtonUrlType = UrlType.JavaScript;

        if (status != WareHouseFormStatus.Draft)
        {
            //只有草稿才能编辑删除
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
        }

    }
    /// <summary>
    /// 填充页面数据
    /// </summary>
    private void FillData()
    {
        if (cmd == "view"||cmd=="viewArchives")
        {
            try
            {
                WareHouseCheckInfo item = wareHouseCheck.GetWareHouseCheck(id);
                if (item == null) return;

                lbCompanyName.Text = company.GetCompany(item.CompanyID).CompanyName;
                lbWareHouse.Text = warehouse.GetWarehouse(item.WareHouseID).Name;
                lbCheckDate.Text = item.CheckDate.ToString("yyyy-MM-dd");
                lbFormNO.Text = item.FormNO;
                cblMaterialType.SelectedValue = ((int)item.MaterialType).ToString();
                cblMaterialType.Enabled = false;
                lbChecker.Text = string.Format("{0}（{1}）", user.GetUser(item.Checker).PersonName, item.Checker);
                lbSpotCheck.Text = item.SpotCheck;
                lbStockCount.Text = item.StockCount;
                cblQuantitySituation.SelectedValue = ((int)item.QuantitySituation).ToString();
                cblQuantitySituation.Enabled = false;
                cblQuality.SelectedValue = ((int)item.QualitySituation).ToString();
                cblQuality.Enabled = false;
                cblRegSituation.SelectedValue = ((int)item.RegSituation).ToString();
                cblRegSituation.Enabled = false;
                lbExceptionSituation.Text = item.ExceptionSituation;

                //表单状态
                status = item.Status;

                //检查后处理意见
                lbQuantityFeeBack.Text = item.QuantityFeeBack;
                if (!string.IsNullOrEmpty(item.QuantityConfirmer))
                    lbQuantityConfirmer.Text = user.GetUser(item.QuantityConfirmer).PersonName;
                else lbQuantityConfirmer.Text = "";

                lbQualityFeeBack.Text = item.QualityFeeBack;
                if (!string.IsNullOrEmpty(item.QualityConfirmer))
                    lbQualityConfirmer.Text = user.GetUser(item.QualityConfirmer).PersonName;
                else lbQualityConfirmer.Text = "";

                lbRegFeeBack.Text = item.RegFeeBack;
                if (!string.IsNullOrEmpty(item.RegConfirmer))
                    lbRegConfirmer.Text = user.GetUser(item.RegConfirmer).PersonName;
                else lbRegConfirmer.Text = "";

                lbOtherFeeBack.Text = item.OtherFeeBack;

                if (!string.IsNullOrEmpty(item.OtherConfirmer))
                    lbOtherConfirmer.Text = user.GetUser(item.OtherConfirmer).PersonName;
                else lbOtherConfirmer.Text = "";

                if (DateTime.Compare(item.OtherConfirmTime, DateTime.MinValue) != 0)
                    lbOtherConfirmTime.Text = item.OtherConfirmTime.ToString("yyyy年MM月dd日");
                else lbOtherConfirmTime.Text = "   年   月   日";

                if (!string.IsNullOrEmpty(item.ResultConfirmer))
                    lbResultConfirmer.Text = user.GetUser(item.ResultConfirmer).PersonName;
                else lbResultConfirmer.Text = "";

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取仓库检查表内容失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        else if (cmd == "delete")
        {
            try
            {
                wareHouseCheck.DeleteWareHouseCheck(id);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除仓库检查表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除仓库检查表成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("WareHouseCheck.aspx"), UrlType.Href, "");
        }
    }
}
