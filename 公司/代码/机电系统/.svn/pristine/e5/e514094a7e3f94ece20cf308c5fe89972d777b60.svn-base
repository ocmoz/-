using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using FM2E.BLL.Maintain;
using WebUtility.Components;
using FM2E.Model.Maintain;

public partial class Module_FM2E_MaintainManager_MaintainManager_MaintainSheet_MaintainSheetPrint : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 0, 0, DataType.Long);
    private readonly Maintain maintainBll = new Maintain();
    private MaintainConfirmResult confirmResult = MaintainConfirmResult.NotConfirm;

    protected int CountPerRow = 5;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
        }
    }

    private void FillData()
    {
        MaintainSheetInfo item = maintainBll.GetMaintainSheet(id);
        Label_AddressName.Text = item.AddressName;
        Label_DeparmentName.Text = item.DepartmentName;
        Label_Period.Text = item.Period + " " + EnumHelper.GetDescription(item.PeriodUnit);
        Label_Remark.Text = item.Remark;
        Label_SystemName.Text = item.SystemName;
        Label_TemplateSheetName.Text = item.SheetNO + " " + item.SheetName;
        Label_TypeName.Text = EnumHelper.GetDescription(item.MaintainType);
        Label_Maintainer.Text = item.MaintainerName;
        Label_MaintainTime.Text = item.MaintainTime.ToString("yyyy-MM-dd");

        Label_Confirmer.Text = item.ConfirmerName;
        Label_ConfirmTime.Text = item.ConfirmTime == DateTime.MinValue ? "" : item.ConfirmTime.ToString("yyyy-MM-dd HH:mm");
        Label_ConfirmResult.Text = EnumHelper.GetDescription(item.ConfirmResult);
        Label_ConfirmRemark.Text = item.ConfirmRemark;

        Image_IsTemp.Visible = item.IsTemp;

        Repeater_EquipmentList.DataSource = item.Equipments;
        Repeater_EquipmentList.DataBind();

        GridView1.DataSource = item.AbnormalEquipments;
        GridView1.DataBind();

        confirmResult = item.ConfirmResult;
    }
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Delete_Click(object sender, EventArgs e)
    {
        try
        {
            maintainBll.DeleteTemplateMaintainSheet(id);
        }
        catch(Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "删除失败", "删除失败，请重新进入再尝试。"+ex.Message, ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        Response.Redirect("MaintainTemplateList.aspx");
    }

}
