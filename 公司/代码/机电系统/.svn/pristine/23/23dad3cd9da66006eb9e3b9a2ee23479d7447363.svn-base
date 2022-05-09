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
using FM2E.BLL.Archives;
using FM2E.Model.Archives;
using FM2E.BLL.System;
using FM2E.WorkflowLayer;

public partial class Module_FM2E_ArchivesManager_ArchivesDestroyApply_ArchivesDestroyApproval_ArchivesDestroyApply : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            FillData2();
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
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 初始化GridView1等数据
    /// </summary>
    private void FillData()
    {
        try
        {
            bool search = Convert.ToBoolean(ViewState["isSearch"]);
            ArchivesDestroyApply bll = new ArchivesDestroyApply();
            int listCount = 0;

            List<string> states = WorkflowHelper.GetCorrelativeStateNameList(ArchivesDestroyWorkflow.WorkflowName, Common.Get_UserName);

            if (states.Count == 0)//该用户没有任何权限
                return;

            QueryParam searchTerm = new QueryParam();
            if (!search)
            {
                ArchivesDestroyApplyInfo item = new ArchivesDestroyApplyInfo();
                item.ID = 0;
                item.Applicant = "";
                item.ApplicantName = "";
                item.ApplyStatus = ArchivesDestroyApplyStatus.Waiting4ApprovalResult;
                searchTerm = bll.GenerateSearchTerm(item, states.ToArray());
            }
            else
            {
                ArchivesDestroyApplyInfo item = new ArchivesDestroyApplyInfo();
                item.ID = 0;
                item.Applicant = "";
                item.ApplicantName = Common.inSQL(TextBox2.Text.Trim());
                item.ApplyStatus = ArchivesDestroyApplyStatus.Waiting4ApprovalResult;
                searchTerm = bll.GenerateSearchTerm(item, states.ToArray());
            }
            searchTerm.PageIndex = AspNetPager1.CurrentPageIndex;
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
    /// 初始化GridView2等数据
    /// </summary>
    private void FillData2()
    {
        try
        {
            bool search = Convert.ToBoolean(ViewState["isSearch"]);
            ArchivesDestroyApply bll = new ArchivesDestroyApply();
            int listCount = 0;

            QueryParam searchTerm = new QueryParam();
            if (!search)
            {
                ArchivesDestroyApplyInfo item = new ArchivesDestroyApplyInfo();
                item.ID = 0;
                item.Applicant = "";
                item.Approvaler = Common.Get_UserName;
                item.ApplicantName = "";
                item.ApplyStatus = 0;
                searchTerm = bll.GenerateSearchTerm(item);
            }
            else
            {
                ArchivesDestroyApplyInfo item = new ArchivesDestroyApplyInfo();
                item.ID = 0;
                item.Applicant = "";
                item.Approvaler = Common.Get_UserName;
                item.ApplicantName = Common.inSQL(TextBox2.Text.Trim());
                item.ApplyStatus = 0;
                searchTerm = bll.GenerateSearchTerm(item);
            }
            searchTerm.PageIndex = AspNetPager2.CurrentPageIndex;
            IList list = bll.GetList(searchTerm, out listCount);
            AspNetPager2.RecordCount = listCount;
            GridView2.DataSource = list;
            GridView2.DataBind();
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
        TabContainer1.ActiveTabIndex = 0;
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        FillData2();
        TabContainer1.ActiveTabIndex = 1;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long id = Convert.ToInt64(gvRow.Attributes["ID"]);
        if (e.CommandName == "approval")
        {
            string url = "ArchivesDestroyApplyApproval.aspx?cmd=approval&id=" + id;
            //Response.Redirect(url);
            System.Web.UI.ScriptManager.RegisterStartupScript(this, Page.GetType(), "ajaxjs", "window.open('" + url + "','_blank');", true);
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

            ArchivesDestroyApplyInfo item = (ArchivesDestroyApplyInfo)e.Row.DataItem;
            e.Row.Attributes["ID"] = item.ID.ToString();
        }

    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView2.Rows[Convert.ToInt32(e.CommandArgument)];
        long id = Convert.ToInt64(gvRow.Attributes["ID"]);
        if (e.CommandName == "view")
        {

            string url = "../ArchivesDestroyApply/ViewArchivesDestroyApply.aspx?cmd=viewArchives&id=" + id;
            //Response.Redirect(url);
            System.Web.UI.ScriptManager.RegisterStartupScript(this, Page.GetType(), "ajaxjs", "window.open('" + url + "','_blank');", true);
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            ArchivesDestroyApplyInfo item = (ArchivesDestroyApplyInfo)e.Row.DataItem;
            e.Row.Attributes["ID"] = item.ID.ToString();
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
            FillData2();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

}
