using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM2E.BLL.Equipment;
using WebUtility;
using WebUtility.Components;
using FM2E.Model.Equipment;
using System.Collections;

public partial class Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_BorrowRecord_ViewApplyRecord : System.Web.UI.Page
{
    private long id = (long)Common.sink("id", MethodType.Get, 20, 0, DataType.Long);
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
            BorrowApplyInfo item = secondment.GetBorrowApply(id);
            if (item == null)
                return;

            lbSheetName.Text = item.SheetName;
            lbLendCompany.Text = item.CompanyName;
            lbBorrowCompany.Text = item.BorrowCompanyName;
            lbApplicant.Text = item.ApplicantName;
            lbStatus.Text = item.StatusString;
            lbSubmitTime.Text = item.SubmitTime.ToShortDateString();

            if (item.DetailList != null)
            {

                GridView1.DataSource = item.DetailList;
                GridView1.DataBind();
            }

            //读取借到的设备列表
            IList list = secondment.GetBorrowRecordList(id);
            if (list != null)
            {
                GridView2.DataSource = list;
                GridView2.DataBind();
            }

            //读取归还验收历史明细
            IList returnHistory = secondment.GetAcceptanceList(id);
            if (returnHistory != null)
            {
                GridView3.DataSource = returnHistory;
                GridView3.DataBind();
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取借调申请单信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
}
