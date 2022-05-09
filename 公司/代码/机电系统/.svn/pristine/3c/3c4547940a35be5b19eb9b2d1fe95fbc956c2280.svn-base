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

public partial class Module_FM2E_MaintainManager_RoutineInspectManager_RoutineInspectView_RoutineInspectConfig : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            //FillData();
        }
    }
    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        try
        {
            ViewState["isSearch"] = false;
            DateTime dt1 = DateTime.Now.Date;
            DateTime dt2 = new DateTime(dt1.Year, dt1.Month, 1);
            TBMinTime.Text = dt2.ToShortDateString();
            TBMaxTime.Text = DateTime.Now.ToShortDateString();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 初始化GridView等数据
    /// </summary>
    private void FillData()
    {
        try
        {
            bool search = Convert.ToBoolean(ViewState["isSearch"]);
            MaintainPlanConfig bll = new MaintainPlanConfig();
            MaintainPlanConfigInfo item = new MaintainPlanConfigInfo();
            if (search)
            {
                item.System = DDLSystem.SelectedValue;
                if (!DDLSubsystem.SelectedValue.Equals(string.Empty))
                    item.Subsystem = Convert.ToInt64(DDLSubsystem.SelectedValue);
                item.PlanObject = DDLPlanObject.SelectedItem.Text;
            }
            item.PlanType = MaintainPlanType.RoutineInspect;
            QueryParam searchTerm = bll.GenerateSearchTerm(item);
            searchTerm.PageIndex = AspNetPager1.CurrentPageIndex;
            searchTerm.PageSize = AspNetPager1.PageSize;
            int listCount = 0;
            IList list = bll.GetList(searchTerm, out listCount);
            AspNetPager1.RecordCount = listCount;
            GridView1.DataSource = list;
            GridView1.DataBind();
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
        TabContainer1.ActiveTabIndex = 1;
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long id = Convert.ToInt64(gvRow.Attributes["ID"]);
        if (e.CommandName == "view")
        {
            Response.Redirect("RoutineInspectTrack.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                MaintainPlanConfig bll = new MaintainPlanConfig();
                bll.DelMaintainPlanConfig(id);
                FillData();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            MaintainPlanConfigInfo item = (MaintainPlanConfigInfo)e.Row.DataItem;
            e.Row.Attributes["ID"] = item.ItemID.ToString();
            string click = "javascript:showPopWin('查看跟踪对比信息','RoutineInspectTrack.aspx?itemID=" + item.ItemID.ToString() + "&minDate=" + TBMinTime.Text.Trim() + "&maxDate=" + TBMaxTime.Text.Trim() + "', 900, 450, addtolist,true,true);return false;";
            ImageButton cb = (ImageButton)e.Row.FindControl("ImageButton1");
            if (cb != null)
                cb.Attributes.Add("OnClick", click);
        }

    }
    /// <summary>
    /// 查询事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            TabContainer1.ActiveTabIndex = 0;
            ViewState["isSearch"] = true;
            FillData();
            TabContainer1.ActiveTabIndex = 1;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询计划失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
}
