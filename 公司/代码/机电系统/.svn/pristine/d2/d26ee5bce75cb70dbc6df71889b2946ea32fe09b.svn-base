using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using FM2E.BLL.Basic;
using FM2E.BLL.Maintain;
using FM2E.BLL.System;
using FM2E.Model.Basic;
using FM2E.Model.Maintain;
using FM2E.Model.Utils;
using FM2E.WorkflowLayer;
using WebUtility;
using WebUtility.Components;
using WebUtility.WebControls;

public partial class Module_FM2E_MaintainManager_RoutineInspectManager_RoutineInspectConfig_EditRoutineInspectConfig : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonBind();
            FillData();
        }
    }

    private void ButtonBind()
    {
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：例行检测计划信息添加";
            TabPanel1.HeaderText = "添加例行检测计划";
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：例行检测计划信息修改";
            TabPanel1.HeaderText = "修改例行检测计划信息";
        }
    }
    private void FillData()
    {
        DDLPeriodUnit.Items.Add(new ListItem("天", "1"));
        DDLPeriodUnit.Items.Add(new ListItem("周", "2"));
        DDLPeriodUnit.Items.Add(new ListItem("月", "3"));
        DDLPeriodUnit.Items.Add(new ListItem("季度", "4"));
        DDLPeriodUnit.Items.Add(new ListItem("年", "5"));
        if (cmd == "edit")
        {
            try
            {
                MaintainPlanConfig bll = new MaintainPlanConfig();
                MaintainPlanConfigInfo item = bll.GetMaintainPlanConfig(id);
                CascadingDropDown1.SelectedValue = item.System;
                CascadingDropDown2.SelectedValue = item.Subsystem.ToString();
                TBPlanObject.Text = item.PlanObject;
                TBPlanPeriod.Text = item.PlanPeriod.ToString();
                DDLPeriodUnit.SelectedValue = ((int)item.PeriodUnit).ToString();
                TextArea2.Value = item.PlanContent;
                TextArea3.Value = item.CheckStandard;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;
        MaintainPlanConfig bll = new MaintainPlanConfig();
        if (cmd == "add" || cmd == "edit")
        {
            MaintainPlanConfigInfo item = new MaintainPlanConfigInfo();
            item.System = DDLSystem.SelectedValue;
            item.Subsystem = Convert.ToInt64(DDLSubsystem.SelectedValue);
            item.PlanObject = TBPlanObject.Text.Trim();
            item.PlanPeriod = Convert.ToInt32(TBPlanPeriod.Text.Trim());
            item.PeriodUnit = (MaintainPlanPeriodUnit)Convert.ToInt32(DDLPeriodUnit.SelectedValue);
            item.PlanContent = TextArea2.Value.Trim();
            item.CheckStandard = TextArea3.Value.Trim();
            item.CompanyID = UserData.CurrentUserData.CompanyID;
            item.PlanType = MaintainPlanType.RoutineInspect;

            if (cmd == "add")
            {
                try
                {

                    bll.InsertMaintainPlanConfig(item);
                    bSuccess = true;
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加例行检测计划失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                }

                if (bSuccess)
                {
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加例行检测计划成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("RoutineInspectConfig.aspx"), UrlType.Href, "");
                }
            }
            else if (cmd == "edit")
            {
                try
                {
                    item.ItemID = id;
                    bll.UpdateMaintainPlanConfig(item);
                    bSuccess = true;
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改例行检测计划信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                }

                if (bSuccess)
                {
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改例行检测计划成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("RoutineInspectConfig.aspx"), UrlType.Href, "");
                }
            }
        }
    }
    protected void btnEquipment_Click(object sender, EventArgs e)
    {
        Response.Redirect("SelectDevice.aspx?cmd=select&id=");
    }
}
