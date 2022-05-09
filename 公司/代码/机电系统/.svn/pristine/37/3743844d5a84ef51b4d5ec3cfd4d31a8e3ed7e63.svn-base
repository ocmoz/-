using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using FM2E.BLL.Maintain;
using FM2E.Model.Maintain;
using WebUtility.Components;
using WebUtility.WebControls;

public partial class Module_FM2E_MaintainManager_RoutineInspectManager_RoutineInspectConfig_ViewRoutineInspectConfig : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonBind();
            BindDate();
        }
    }
    private void ButtonBind()
    {
        if (cmd == "view")
        {
            //删除
            HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[2];
            button.ButtonUrlType = UrlType.JavaScript;
            button.ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');", id);
            //修改
            button = HeadMenuWebControls1.ButtonList[1];
            button.ButtonUrlType = UrlType.Href;
            button.ButtonUrl = string.Format("EditRoutineInspectConfig.aspx?cmd=edit&id={0}", id);

            //配置设备
            button = HeadMenuWebControls1.ButtonList[0];
            button.ButtonUrlType = UrlType.Href;
            button.ButtonUrl = string.Format("SelectDevice.aspx?cmd=edit&id={0}", id);
        }
        else if (cmd == "delete")
        {
            //执行删除操作
            bool bSuccess = false;
            try
            {
                MaintainPlanConfig bll = new MaintainPlanConfig();
                bll.DelMaintainPlanConfig(id);

                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除例行检测计划失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess == true)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除例行检测计划(ID:" + id + ")成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("RoutineInspectConfig.aspx"), UrlType.Href, "");
            }
        }
    }
    protected void BindDate()
    {
        try
        {
            if (cmd == "view")
            {
                MaintainPlanConfig bll = new MaintainPlanConfig();
                MaintainPlanConfigInfo detail = bll.GetMaintainPlanConfig(id);
                Label2.Text = detail.SystemName;
                Label3.Text = detail.SubsystemName;
                Label4.Text = detail.PlanPeriodString;
                Label5.Text = detail.PlanObject;
                Label6.Text = detail.PlanContent;
                Label7.Text = detail.CheckStandard;
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
}
