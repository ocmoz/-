﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using WebUtility.Components;
using FM2E.Model.Maintain;
using FM2E.BLL.Maintain;
using System.Collections;
using FM2E.BLL.Basic;
using FM2E.Model.System;

public partial class Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionMaintain_InteriorMyMalfunctionSheets : System.Web.UI.Page
{
    private readonly MalfunctionHandle malfunctionBll = new MalfunctionHandle();

    /// <summary>
    /// 查询条件
    /// </summary>
    private MalfunctionSearchInfo SearchTerm
    {
        get
        {
            if (ViewState["SearchTerm"] == null)
            {
                MalfunctionSearchInfo term = new MalfunctionSearchInfo();
                term.MaintainDept = UserData.CurrentUserData.DepartmentID;
                return term;
            }
            else return (MalfunctionSearchInfo)ViewState["SearchTerm"];
        }
        set
        {
            ViewState["SearchTerm"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            PermissionControl();
        }
    }
    private void InitialPage()
    {
        try
        {
            LoginUserInfo loginUser = UserData.CurrentUserData;

            ddlDepartment.Items.Clear();
            ddlDepartment.Items.Add(new ListItem("不限", "0"));
            ddlDepartment.Items.AddRange(ListItemHelper.GetDepartmentListItems(loginUser.CompanyID));

            ListItem[] statusItems = EnumHelper.GetListItems(typeof(MalfunctionHandleStatus), (int)MalfunctionHandleStatus.Unknown);

            ddlStatus.Items.Clear();
            ddlStatus.Items.Add(new ListItem("不限", ((int)MalfunctionHandleStatus.Unknown).ToString()));
            ddlStatus.Items.AddRange(statusItems);

            //过滤条件
            ddlFilterDepartment.Items.Clear();
            ddlFilterDepartment.Items.Add(new ListItem("不限", "0"));
            ddlFilterDepartment.Items.AddRange(ListItemHelper.GetDepartmentListItems(loginUser.CompanyID));

            ListItem[] statusItems1 = EnumHelper.GetListItems(typeof(MalfunctionHandleStatus), (int)MalfunctionHandleStatus.Unknown);

            ddlFilterStatus.Items.Clear();
            ddlFilterStatus.Items.Add(new ListItem("不限", ((int)MalfunctionHandleStatus.Unknown).ToString()));
            ddlFilterStatus.Items.AddRange(statusItems1);

            ListItem[] rankTypes = EnumHelper.GetListItems(typeof(MalfunctionRank), (int)MalfunctionRank.Unknown);

            ddlFilterRank.Items.Clear();
            ddlFilterRank.Items.Add(new ListItem("不限", ((int)MalfunctionRank.Unknown).ToString()));
            ddlFilterRank.Items.AddRange(rankTypes);

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 权限控制
    /// </summary>
    private void PermissionControl()
    {
        if (SystemPermission.CheckPermission(PopedomType.Edit))
        {
            //有编辑权限
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
        }
        //else GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
    }
    /// <summary>
    /// 填充页面数据
    /// </summary>
    private void FillData()
    {
        try
        {
            MalfunctionSearchInfo term = SearchTerm;
            //查询
            int recordCount = 0;
            IList list = malfunctionBll.GetMalfunctionList(term, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out recordCount);
            GridView1.DataSource = list;
            GridView1.DataBind();
            AspNetPager1.RecordCount = recordCount;
            TabContainer1.ActiveTabIndex = 0;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取故障处理单列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
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
    /// <summary>
    /// GridView  行命令
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long id = Convert.ToInt64(gvRow.Attributes["SheetID"]);

        if (e.CommandName == "record")
        {
            //查看
            Response.Redirect("MalfunctionHandle.aspx?cmd=edit&from=2&&id=" + id);
        }

        if (e.CommandName == "view")
        {
            //查看
            Response.Redirect("../MalfunctionReport/ViewMalfunctionSheet.aspx?cmd=view&id=" + id);
        }
    }
    /// <summary>
    /// GridView数据绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#efefef';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            MalfunctionHandleInfo item = (MalfunctionHandleInfo)e.Row.DataItem;
            e.Row.Attributes["SheetID"] = item.SheetID.ToString();
            if (!SystemPermission.CheckPermission(PopedomType.Edit))
            {
                e.Row.FindControl("ImageButton1").Visible = false;
            }

            //********** Modified by Xue    For V 3.1.1    2011-10-17 *********************************************************************************
            if (item.Status == MalfunctionHandleStatus.Canceled)
            {
                e.Row.FindControl("ImageButton1").Visible = false;
            }
            //********** Modification Finished 2011-10-17 **********************************************************************************************
        }
    }
    /// <summary>
    /// 获取查询条件
    /// </summary>
    /// <returns></returns>
    private void GetSearchTerm()
    {
        MalfunctionSearchInfo term = new MalfunctionSearchInfo();

        if (!string.IsNullOrEmpty(tbSheetNO.Text.Trim()))
            term.SheetNO = Common.inSQL(tbSheetNO.Text.Trim());

        if (ddlDepartment.SelectedValue != "0" && ddlDepartment.SelectedValue != "")
            term.DepartmentID = Convert.ToInt64(ddlDepartment.SelectedValue);

        if (!string.IsNullOrEmpty(tbEquipmentNO.Text.Trim()))
            term.EquipmentNO = Common.inSQL(tbEquipmentNO.Text.Trim());

        if (!string.IsNullOrEmpty(tbEquipmentName.Text.Trim()))
            term.EquipmentName = Common.inSQL(tbEquipmentName.Text.Trim());

        if (!string.IsNullOrEmpty(tbReporter.Text.Trim()))
            term.Reporter = Common.inSQL(tbReporter.Text.Trim());

        if (!string.IsNullOrEmpty(tbReportTimeFrom.Text.Trim()))
            term.ReportDateFrom = Convert.ToDateTime(tbReportTimeFrom.Text.Trim());

        if (!string.IsNullOrEmpty(tbReportTimeTo.Text.Trim()))
            term.ReportDateTo = Convert.ToDateTime(tbReportTimeTo.Text.Trim());

        if (ddlStatus.SelectedValue != "0")
            term.Status = Convert.ToInt32(ddlStatus.SelectedValue);

        //term.Receiver = Common.Get_UserName;

        SearchTerm = term;
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        GetSearchTerm();
        FillData();
    }


    /// <summary>
    /// 列表项筛选
    /// </summary>
    private void Filter()
    {
        MalfunctionSearchInfo term = new MalfunctionSearchInfo();

        term.DepartmentID = Convert.ToInt64(ddlFilterDepartment.SelectedValue);
        //term.RecorderName = Common.inSQL(tbFilterRecorder.Text.Trim());
        term.Receiver = Common.inSQL(tbReceiver.Text.Trim());
        term.MalfunctionRank = Convert.ToInt32(ddlFilterRank.SelectedValue);
        term.Status = Convert.ToInt32(ddlFilterStatus.SelectedValue);

        SearchTerm = term;
    }
    /// <summary>
    /// 列表条件筛选
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btFilter_Click(object sender, EventArgs e)
    {
        Filter();
        AspNetPager1.CurrentPageIndex = 1;
        FillData();
    }
}
