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
using FM2E.Model.Utils;

public partial class Module_FM2E_MaintainManager_MalFunctionManager_MalFunctionClassify_EditClassify : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 20, 1, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);

    private readonly MalfunctionClassify classifyBll = new MalfunctionClassify();

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
    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        ListItem[] rankTypes = EnumHelper.GetListItemsEx(typeof(MalfunctionRank), (int)MalfunctionRank.Common,(int)MalfunctionRank.Unknown);
        ddlRank.Items.Clear();
        ddlRank.Items.AddRange(rankTypes);

        ListItem[] timeUnits1 = EnumHelper.GetListItemsEx(typeof(TimeUnits), (int)TimeUnits.Hour, (int)TimeUnits.Unknown, (int)TimeUnits.Month, (int)TimeUnits.Year);
        ListItem[] timeUnits2 = EnumHelper.GetListItemsEx(typeof(TimeUnits), (int)TimeUnits.Hour, (int)TimeUnits.Unknown, (int)TimeUnits.Month, (int)TimeUnits.Year);
        ListItem[] timeUnits3 = EnumHelper.GetListItemsEx(typeof(TimeUnits), (int)TimeUnits.Hour, (int)TimeUnits.Unknown, (int)TimeUnits.Month, (int)TimeUnits.Year);
        ddlResonseTime.Items.Clear();
        ddlResonseTime.Items.AddRange(timeUnits1);

        ddlFunTime.Items.Clear();
        ddlFunTime.Items.AddRange(timeUnits2);

        ddlRepairTime.Items.Clear();
        ddlRepairTime.Items.AddRange(timeUnits3);
    }
    /// <summary>
    /// 填充页面数据
    /// </summary>
    private void FillData()
    {
        try
        {
            if (cmd == "edit")
            {
                MalfunctionClassifyInfo item = classifyBll.GetClassify(id);
                cddSystem.SelectedValue = item.System;
                cddSubSystem.SelectedValue = item.SubSystem.ToString();
                tbMalfunctionObject.Text = item.MalfunctionObject;
                tbMalfunctionDescription.Text = item.MalfunctionDescription;
                ddlRank.SelectedValue = ((int)item.Rank).ToString();
                tbResponseTime.Text = item.ResponseTime.ToString();
                tbFunRestoreTime.Text = item.FunRestoreTime.ToString();
                tbRepairTime.Text = item.RepairTime.ToString();
                ddlResonseTime.SelectedValue = ((int)item.ResponseUnit).ToString();
                ddlFunTime.SelectedValue = ((int)item.FunRestoreUnit).ToString();
                ddlRepairTime.SelectedValue = ((int)item.RepairUnit).ToString();
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取故障别类数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 菜单按钮绑定
    /// </summary>
    private void ButtonBind()
    {
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "新增故障类别";
            TabContainer1.Tabs[0].HeaderText = "新增故障类别";
            Button2.Visible = true;
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "修改故障类别";
            TabContainer1.Tabs[0].HeaderText = "修改故障类别";
            Button2.Visible = false;
        }
    }
    /// <summary>
    /// 添加或修改故障类别信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!ValidateInput())
            return;

        MalfunctionClassifyInfo item = new MalfunctionClassifyInfo();
        try
        {
            item.System = ddlSystem.SelectedValue;
            item.SubSystem = Convert.ToInt64(ddlSubsystem.SelectedValue);
            item.MalfunctionObject = tbMalfunctionObject.Text;
            item.MalfunctionDescription = tbMalfunctionDescription.Text;
            item.Rank = (MalfunctionRank)Convert.ToInt32(ddlRank.SelectedValue);
            item.ResponseTime = Convert.ToInt32(tbResponseTime.Text.Trim());
            item.FunRestoreTime = Convert.ToInt32(tbFunRestoreTime.Text.Trim());
            item.RepairTime = Convert.ToInt32(tbRepairTime.Text.Trim());
            item.ResponseUnit = (TimeUnits)Convert.ToInt32(ddlResonseTime.SelectedValue);
            item.FunRestoreUnit = (TimeUnits)Convert.ToInt32(ddlFunTime.SelectedValue);
            item.RepairUnit = (TimeUnits)Convert.ToInt32(ddlRepairTime.SelectedValue);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交失败：获取参数失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        if (cmd == "add")
        {
            try
            {
                classifyBll.AddClassify(item);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加故障类别失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加故障类别成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("MalfunctionClassify.aspx"), UrlType.Href, "");
        }
        else if (cmd == "edit")
        {
            try
            {
                item.ID = id;
                classifyBll.UpdateClassify(item);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改故障类别失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改故障类别成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("MalfunctionClassify.aspx"), UrlType.Href, "");
        }
    }
        /// <summary>
    /// 添加故障类别信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (!ValidateInput())
            return;

        MalfunctionClassifyInfo item = new MalfunctionClassifyInfo();
        try
        {
            item.System = ddlSystem.SelectedValue;
            item.SubSystem = Convert.ToInt64(ddlSubsystem.SelectedValue);
            item.MalfunctionObject = tbMalfunctionObject.Text;
            item.MalfunctionDescription = tbMalfunctionDescription.Text;
            item.Rank = (MalfunctionRank)Convert.ToInt32(ddlRank.SelectedValue);
            item.ResponseTime = Convert.ToInt32(tbResponseTime.Text.Trim());
            item.FunRestoreTime = Convert.ToInt32(tbFunRestoreTime.Text.Trim());
            item.RepairTime = Convert.ToInt32(tbRepairTime.Text.Trim());
            item.ResponseUnit = (TimeUnits)Convert.ToInt32(ddlResonseTime.SelectedValue);
            item.FunRestoreUnit = (TimeUnits)Convert.ToInt32(ddlFunTime.SelectedValue);
            item.RepairUnit = (TimeUnits)Convert.ToInt32(ddlRepairTime.SelectedValue);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交失败：获取参数失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        if (cmd == "add")
        {
            try
            {
                classifyBll.AddClassify(item);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加故障类别失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            Common.MessBox("添加故障类别成功");
            tbMalfunctionDescription.Text = "";
        }
    }
    /// <summary>
    /// 校验用户输入
    /// </summary>
    /// <returns></returns>
    private bool ValidateInput()
    {
        string errorMsg = "";

        if (ddlSystem.SelectedValue == "")
            errorMsg = "请选择系统";
        else if (tbMalfunctionObject.Text.Trim() == "")
            errorMsg = "故障对象不能为空";
        else if (ddlRank.SelectedValue == "0")
            errorMsg = "请选择响应类型";
        else if (tbResponseTime.Text.Trim() == "")
            errorMsg = "响应时间不能为空";
        else if (tbFunRestoreTime.Text.Trim() == "")
            errorMsg = "功能恢复时间不能为空";
        else if (tbRepairTime.Text.Trim() == "")
            errorMsg = "修复时间不能为空";

        try
        {
            Convert.ToSingle(tbResponseTime.Text.Trim());
        }
        catch
        {
            errorMsg = "响应时间格式错误";
        }

        try
        {
            Convert.ToSingle(tbRepairTime.Text.Trim());
        }
        catch
        {
            errorMsg = "修复时间格式错误";
        }

        if (errorMsg == "")
            return true;
        else
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "错误：" + errorMsg);
            errMsg.Text ="错误："+ errorMsg;
            return false;
        }
    }
}
