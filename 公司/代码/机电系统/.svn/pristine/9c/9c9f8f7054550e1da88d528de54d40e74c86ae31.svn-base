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

public partial class Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_ReturnEquipment_ViewAcceptanceReocrd : System.Web.UI.Page
{
    private long id = (long)Common.sink("id", MethodType.Get, 30, 0, DataType.Long);
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 20, 0, DataType.Str);
    private readonly Secondment secondment = new Secondment();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
        }
    }
    private void FillData()
    {
        try
        {
            ReturnAcceptanceInfo item = secondment.GetAcceptanceInfo(id);
            lbEquipmentNO.Text = item.EquipmentNO;
            lbEquipmentName.Text = item.EquipmentName;
            lbModel.Text = item.Model;
            lbReturnCompany.Text = item.ReturnCompanyName;
            lbReturner.Text = item.ReturnerName;
            BorrowApplyInfo applyInfo = secondment.GetBorrowApply(item.BorrowApplyID);

            ltSheet.Text = string.Format("<a href='../BorrowRecord/ViewApplyRecord.aspx?id={0}' style='color: #FF0000'>{1}</a>", applyInfo.BorrowApplyID, applyInfo.SheetName);
            lbChecker.Text = item.CheckerName;
            lbReturnDate.Text = item.ReturnDate.ToString("yyyy-MM-dd HH:mm");
            lbResult.Text = item.Result ? "验收通过" : "验收不通过";
            lbFeeBack.Text = item.FeeBack;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取归还设备验收信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
}
