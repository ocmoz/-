using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections.Generic;

using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.BLL.Maintain;
using FM2E.Model.Maintain;
using FM2E.BLL.System;
using FM2E.WorkflowLayer;

public partial class Module_FM2E_MaintainManager_MaintainManager_MaintainItem_EditMaintainItem : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    private readonly Maintain maintainBll = new Maintain();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
        }
    }

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        //类型
        DropDownList_EditType.Items.Clear();
        DropDownList_EditType.Items.AddRange(EnumHelper.GetListItems(typeof(MaintainType), (int)MaintainType.Unknown));

        //周期
        DDLPeriodUnit.Items.Clear();
        DDLPeriodUnit.Items.AddRange(EnumHelper.GetListItems(typeof(MaintainIntervalUnit), (int)MaintainIntervalUnit.Unknown));
    }
    /// <summary>
    /// 初始化GridView等数据
    /// </summary>
    private void FillData()
    {
        try
        {
            if (cmd == "edit")  //若需要编辑
            {
                //把相关的信息获取出来，显示在编辑区域中
                MaintainItemInfo item = maintainBll.GetMaintainItem(id);
                if (item == null)
                    return;
                else
                {
                    try
                    {
                        //DropDownList_EditSystem.SelectedValue = p.SystemID;
                        CascadingDropDown1.SelectedValue = item.SystemID;
                    }
                    catch { }
                    try
                    {
                        //DropDownList_EditSubSystem.SelectedValue = p.SubSystemID.ToString();
                        CascadingDropDown2.SelectedValue = item.SubSystemID.ToString();
                    }
                    catch { }
                    try
                    {
                        DDLPeriodUnit.SelectedValue = ((int)item.PeriodUnit).ToString();
                    }
                    catch { }
                    try { DropDownList_EditType.SelectedValue = ((int)item.MaintainType).ToString(); }
                    catch { }

                    TBPlanPeriod.Text = item.Period.ToString();
                    TBPlanObject.Text = item.Object;
                    TextArea_Content.Value = item.Content;
                    TextArea_Standard.Value = item.Standard;
                    Hidden_EditID.Value = id.ToString();
                    CurrentAction = EditAction;
                    btnSubmit.Text = "更新";
                }
            }
            else if (cmd == "new")
            {
                //Do Nothing
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 添加新的
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        MaintainItemInfo item = GetEditObject(); 
        item.ID = maintainBll.SaveMaintainItem(item);
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", "操作维护标准项成功", Icon_Type.OK, true, Common.GetHomeBaseUrl("MaintainItem.aspx"), UrlType.Href, "");
    }

    /// <summary>
    /// 获取当前编辑的时候返回的对象
    /// </summary>
    /// <returns></returns>
    private MaintainItemInfo GetEditObject()
    {
        MaintainItemInfo item = new MaintainItemInfo();
        item.Content = TextArea_Content.Value.Trim();
        item.MaintainType = (MaintainType)int.Parse(DropDownList_EditType.SelectedValue);
        item.Object = TBPlanObject.Text.Trim();
        item.Period = int.Parse(TBPlanPeriod.Text.Trim());
        item.PeriodUnit = (MaintainIntervalUnit)int.Parse(DDLPeriodUnit.SelectedValue);
        item.Standard = TextArea_Standard.Value.Trim();
        item.SubSystemID = long.Parse(DropDownList_EditSubSystem.SelectedValue);
        item.SystemID = DropDownList_EditSystem.SelectedValue;
        switch (CurrentAction)
        {
            case AddAction:
                {
                    item.ID = 0;
                    break;
                }
            case EditAction:
                {
                    item.ID = long.Parse(Hidden_EditID.Value);
                    break;
                }
            default: break;
        }
        return item;
    }

    private const string EditAction = "Edit";
    private const string AddAction = "Add";
    /// <summary>
    /// 当前动作
    /// </summary>
    private string CurrentAction
    {
        get { return string.IsNullOrEmpty(Hidden_CurrentAction.Value) ? AddAction : EditAction; }
        set { Hidden_CurrentAction.Value = value; }
    }

}
