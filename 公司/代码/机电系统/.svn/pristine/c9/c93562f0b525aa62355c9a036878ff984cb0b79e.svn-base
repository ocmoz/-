using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using WebUtility.Components;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using FM2E.BLL.Basic;
using FM2E.BLL.System;

public partial class Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_BorrowRecord_ViewBorrowRecord : System.Web.UI.Page
{
    private string equipmentNO = (string)Common.sink("equipmentNO", MethodType.Get, 30, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 30, 0, DataType.Long);
    private readonly Secondment secondment = new Secondment();
    private readonly Equipment equipment = new Equipment();
    private readonly Company company = new Company();
    private readonly User user = new User();

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
            BorrowRecordInfo recordInfo = secondment.GetBorrowRecord(id, equipmentNO);
            BorrowApplyInfo applyInfo = secondment.GetBorrowApply(recordInfo.BorrowApplyID);
            EquipmentInfoFacade equipmentInfo = equipment.GetEquipmentBYNO(equipmentNO);
            lbEquipmentNO.Text = recordInfo.EquipmentNO;
            if (equipmentInfo != null)
            {
                lbEquipmentName.Text = equipmentInfo.Name;
                lbModel.Text = equipmentInfo.Model + "/" + equipmentInfo.Specification;
                lbEquipmentName.ForeColor = System.Drawing.Color.Black;
                lbModel.ForeColor = System.Drawing.Color.Black; 
            }
            else
            {
                lbEquipmentName.Text = "找不到相关设备的信息，可能设备信息已删除";
                lbModel.Text = "找不到相关设备的信息，可能设备信息已删除";
                lbEquipmentName.ForeColor = System.Drawing.Color.Red;
                lbModel.ForeColor = System.Drawing.Color.Red;
            }
            lbBorrowCompany.Text = company.GetCompany(recordInfo.BorrowCompany).CompanyName;
            lbApplicant.Text = applyInfo.ApplicantName;
            ltSheet.Text = string.Format("<a href='ViewApplyRecord.aspx?id={0}' style='color: #FF0000'>{1}</a>", applyInfo.BorrowApplyID, applyInfo.SheetName);
            lbBorrower.Text = user.GetUser(recordInfo.Borrower).PersonName;
            lbBorrowTime.Text = recordInfo.BorrowTime.ToString("yyyy-MM-dd HH:mm:ss");
            lbReturnTime.Text = recordInfo.ReturnDate.ToString("yyyy-MM-dd");
            lbReason.Text = recordInfo.Reason;
            lbIsReturned.Text = recordInfo.IsReturned ? "是" : "否";
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取借出设备信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
}
