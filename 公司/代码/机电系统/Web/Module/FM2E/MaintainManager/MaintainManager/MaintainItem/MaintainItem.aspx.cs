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

public partial class Module_FM2E_MaintainManager_MaintainManager_MaintainItem_MaintainItem : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private readonly Maintain maintainBll = new Maintain();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            PermissionControl();
            //********** Modification Finished 2011-09-09 **********************************************************************************************
        }
    }

    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
    private void PermissionControl()
    {
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = SystemPermission.CheckButtonPermission(PopedomType.New);
        GridView_PlanList.Columns[GridView_PlanList.Columns.Count - 1].Visible = SystemPermission.CheckButtonPermission(PopedomType.Delete);
        GridView_PlanList.Columns[GridView_PlanList.Columns.Count - 2].Visible = SystemPermission.CheckButtonPermission(PopedomType.Edit);
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        //try
        //{
        //    ViewState["isSearch"] = false;
        //}
        //catch (Exception ex)
        //{
        //    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        //}
        //系统
        DDLSystem.Items.Clear();
        DDLSystem.Items.AddRange(ListItemHelper.GetSystemListItemsWithBlank());
        DDLSystem.SelectedIndex = 0;

        //子系统
        DDLSubsystem.Items.Clear();
        DDLSubsystem.Items.AddRange(ListItemHelper.GetSubSystemListItemsWithBlank(DDLSystem.SelectedValue));

        //类型
        DropDownList_Type.Items.Clear();
        DropDownList_Type.Items.AddRange(EnumHelper.GetListItems(typeof(MaintainType)));

        //周期
        DropDownList_Period.Items.Clear();
        DropDownList_Period.Items.AddRange(EnumHelper.GetListItems(typeof(MaintainIntervalUnit)));
    }
    /// <summary>
    /// 初始化GridView等数据
    /// </summary>
    private void FillData()
    {
        try
        {
            MaintainItemSearchInfo info = CurrentSearchInfo;
            int recordCount = 0;
            IList list = maintainBll.SearchMaintainItem(info, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out recordCount);
            GridView_PlanList.DataSource = list;
            GridView_PlanList.DataBind();
            AspNetPager1.RecordCount = recordCount;

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }

    //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
    //    long id = Convert.ToInt64(gvRow.Attributes["ID"]);
    //    if (e.CommandName == "view")
    //    {
    //        Response.Redirect("ViewDailyPatrolConfig.aspx?cmd=view&id=" + id);
    //    }
    //    else if (e.CommandName == "del")
    //    {
    //        try
    //        {
    //            MaintainPlanConfig bll = new MaintainPlanConfig();
    //            bll.DelMaintainPlanConfig(id);
    //            FillData();
    //        }
    //        catch (Exception ex)
    //        {
    //            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
    //        }
    //    }
    //}
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            //MaintainPlanConfigInfo item = (MaintainPlanConfigInfo)e.Row.DataItem;
            //e.Row.Attributes["ID"] = item.ItemID.ToString();
        }

    }
    ///// <summary>
    ///// 查询事件
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        TabContainer1.ActiveTabIndex = 0;
    //        ViewState["isSearch"] = true;
    //        FillData();
    //    }
    //    catch (Exception ex)
    //    {
    //        EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询计划失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
    //    }
    //}

    protected void DDLSystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        //子系统
        DDLSubsystem.Items.Clear();
        DDLSubsystem.Items.AddRange(ListItemHelper.GetSubSystemListItemsWithBlank(DDLSystem.SelectedValue));

        CurrentSearchInfo = GetSearchInfo();
        Filter();
    }
    protected void DDLSubsystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        CurrentSearchInfo = GetSearchInfo();
        Filter();
    }
    protected void DropDownList_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        CurrentSearchInfo = GetSearchInfo();
        Filter();
    }
    protected void DropDownList_Period_SelectedIndexChanged(object sender, EventArgs e)
    {
        CurrentSearchInfo = GetSearchInfo();
        Filter();
    }

    private void Filter()
    {
        AspNetPager1.CurrentPageIndex = 1;
        FillData();
    }

    private MaintainItemSearchInfo GetSearchInfo()
    {
        MaintainItemSearchInfo info = new MaintainItemSearchInfo();
        info.SystemID = DDLSystem.SelectedValue;
        info.SubSystemID = Convert.ToInt64(DDLSubsystem.SelectedValue);
        info.MaintainType = (MaintainType)Convert.ToInt32(DropDownList_Type.SelectedValue);
        info.PeriodUnit = (MaintainIntervalUnit)Convert.ToInt32(DropDownList_Period.SelectedValue);
        info.Object = Common.inSQL(TextBox_ObjectSearch.Text.Trim());
        return info;
    }

    private MaintainItemSearchInfo CurrentSearchInfo
    {
        get
        {
            MaintainItemSearchInfo info = (MaintainItemSearchInfo)ViewState["SearchInfo"];
            if (info == null)
            {
                if (info == null)
                {
                    info = new MaintainItemSearchInfo();
                    info.SystemID = DDLSystem.SelectedValue;
                    info.SubSystemID = Convert.ToInt64(DDLSubsystem.SelectedValue);
                    info.MaintainType = (MaintainType)Convert.ToInt32(DropDownList_Type.SelectedValue);
                    info.PeriodUnit = (MaintainIntervalUnit)Convert.ToInt32(DropDownList_Period.SelectedValue);
                }
                
            }
            return info;
        }
        set { ViewState["SearchInfo"] = value; }
    }

    protected void GridView_PlanList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        long id = long.Parse(e.CommandArgument.ToString());
        //编辑
        switch (e.CommandName)
        {
            case "EditCMD":
                {
                    ////把相关的信息获取出来，显示在编辑区域中
                    //MaintainItemInfo p = maintainBll.GetMaintainItem(id);
                    //try
                    //{
                    //    //DropDownList_EditSystem.SelectedValue = p.SystemID;
                    //    CascadingDropDown1.SelectedValue = p.SystemID;
                    //}
                    //catch { }
                    //try
                    //{
                    //    //DropDownList_EditSubSystem.SelectedValue = p.SubSystemID.ToString();
                    //    CascadingDropDown2.SelectedValue = p.SubSystemID.ToString();
                    //}
                    //catch { }
                    //try
                    //{
                    //    DDLPeriodUnit.SelectedValue = ((int)p.PeriodUnit).ToString();
                    //}
                    //catch { }
                    //try { DropDownList_EditType.SelectedValue = ((int)p.MaintainType).ToString(); }
                    //catch { }

                    //TBPlanPeriod.Text = p.Period.ToString();
                    //TBPlanObject.Text = p.Object;
                    //TextArea_Content.Value = p.Content;
                    //TextArea_Standard.Value = p.Standard;
                    //Hidden_EditID.Value = id.ToString();
                    //CurrentAction = EditAction;
                    //btnSubmit.Text = "更新";
                    //Button_Cancel.Visible = true;
                    ////隐藏删除和编辑列
                    //GridView_PlanList.Columns[GridView_PlanList.Columns.Count - 2].Visible = false;
                    //GridView_PlanList.Columns[GridView_PlanList.Columns.Count - 1].Visible = false;

                    ////UpdatePanel_Edit.Update();
                    ////UpdatePanel1.Update();
                    Response.Redirect("EditMaintainItem.aspx?cmd=edit&id=" + id);
                    break;
                }
                //删除
            case "DeleteCMD":
                {
                    maintainBll.DeleteMaintainItem(id);
                    UpdateList();
                    break;
                }
            default: break;
        }
    }
    /// <summary>
    /// 更新列表
    /// </summary>
    private void UpdateList()
    {
        //更新列表
        CurrentSearchInfo = GetSearchInfo();
        Filter();
        //UpdatePanel1.Update();
    }

    
    protected void TextBox_ObjectSearch_TextChanged(object sender, EventArgs e)
    {
        CurrentSearchInfo = GetSearchInfo();
        Filter();
    }
}
