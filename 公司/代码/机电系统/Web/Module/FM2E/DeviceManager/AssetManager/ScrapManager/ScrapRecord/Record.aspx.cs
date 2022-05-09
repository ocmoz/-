using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using System.Collections;
using WebUtility.Components;
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;

using FM2E.Model.Utils;
using FM2E.BLL.Utils;


public partial class Module_FM2E_DeviceManager_AssetManager_ScrapManager_ScrapRecord_Record : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 10, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 20, 0, DataType.Long);
    private readonly Scrap scrapBll = new Scrap();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove(this.ToString());
            FillData();
        }
    }

    private void FillData()
    {
        try
        {
            if (cmd == "add")
            {
                ScrapApplyInfo item = scrapBll.GetScrapApply(id);
                if (item != null)
                {
                    Session[this.ToString()] = item;
                    lbSheetName.Text = item.SheetName;
                    lbApplicant.Text = item.ApplicantName;
                    lbCompany.Text = item.CompanyName;
                    lbDep.Text = item.DepName;
                    lbStatus.Text = item.StatusString;
                    lbApplyDate.Text = item.ApplyDate.ToShortDateString();
                    lbRemark.Text = item.Remark;

                    if (item.Equipments != null && item.Equipments.Count != 0)
                    {
                        ScrapApplyDetailInfo detail = (ScrapApplyDetailInfo)item.Equipments[0];
                        lbEquipment.Text = detail.EquipmentNo + "（" + detail.EquipmentName + "）";
                        lbReason.Text = detail.ScrapReason;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "加载页面数据失败" + ex.Message, ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    private void BindButton()
    {
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：报废登记";
            TabContainer1.Tabs[0].HeaderText = "报废登记";
            ApprovalPanel.Visible = true;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            ScrapApplyInfo item = (ScrapApplyInfo)Session[this.ToString()];
            ScrapRecordInfo model = new ScrapRecordInfo();
            ScrapApplyDetailInfo detail = (ScrapApplyDetailInfo)item.Equipments[0];
            model.EquipmentNO = detail.EquipmentNo;
            model.EquipmentName = detail.EquipmentName;
            model.ScrapID = item.ScrapID;
            model.ScrapReason = detail.ScrapReason;
            model.ScrapTime = DateTime.Now;
            model.DepID = item.DepID;
            
            Session.Remove(this.ToString());
            scrapBll.AddScrapRecord(model);

            scrapBll.ChangeStatus(id, (int)ScrapStatus.Finished);

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "注销资产失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", "注销资产成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ScrapRecord.aspx"), UrlType.Href, "");
    }

}
