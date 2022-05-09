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
using FM2E.BLL.System;
using System.Collections;

public partial class Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_BorrowApply_ViewBorrowApply : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 20, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 20, 0, DataType.Long);
    private readonly Secondment secondment = new Secondment();
    private BorrowApplyStatus status = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            FillData();
            ButtonBind();
        }
    }

    private void FillData()
    {
        if (cmd == "view"||cmd=="viewArchives")
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
                status = item.Status;

                if (item.DetailList != null)
                {

                    GridView1.DataSource = item.DetailList;
                    GridView1.DataBind();
                }
                if (item.ApprovalList != null)
                {
                    GridView2.DataSource = item.ApprovalList;
                    GridView2.DataBind();
                }
                //读取借到的设备列表
                IList list = secondment.GetBorrowRecordList(id);
                if (list != null)
                {
                    GridView3.DataSource = list;
                    GridView3.DataBind();
                }
                //读取归还验收历史明细
                IList returnHistory = secondment.GetAcceptanceList(id);
                if (returnHistory != null)
                {
                    GridView4.DataSource = returnHistory;
                    GridView4.DataBind();
                }
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取借调申请单信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        else if (cmd == "delete")
        {
            try
            {
                secondment.DeleteBorrowApply(id);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除借调申请单信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除设备借调申请成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("BorrowApply.aspx"), UrlType.Href, "");
        }
    }

    private void ButtonBind()
    {
        HeadMenuWebControls1.ButtonList[0].ButtonUrl = "EditBorrowApply.aspx?cmd=edit&id=" + id;
        HeadMenuWebControls1.ButtonList[0].ButtonUrlType = UrlType.Href;

        HeadMenuWebControls1.ButtonList[1].ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');", id);
        HeadMenuWebControls1.ButtonList[1].ButtonUrlType = UrlType.JavaScript;

        if (status != BorrowApplyStatus.Draft)
        {
            //只有草稿才能编辑删除
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
        }
    }
}
