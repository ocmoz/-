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

public partial class Module_FM2E_ArchivesManager_ArchivesDestroyApply_ArchivesDestroyApply_ArchivesDestroyApply : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);

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
        try
        {
            ViewState["isSearch"] = false;
            DDLStatus.Items.Clear();
            DDLStatus.Items.Add(new ListItem("请选择申请状态", "0"));
            DDLStatus.Items.Add(new ListItem("草稿", "1"));
            DDLStatus.Items.Add(new ListItem("等待审批", "2"));
            DDLStatus.Items.Add(new ListItem("审批通过", "3"));
            DDLStatus.Items.Add(new ListItem("审批不通过", "4"));
            DDLStatus.Items.Add(new ListItem("申请过期", "5"));
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
            ArchivesDestroyApply bll = new ArchivesDestroyApply();
            int listCount = 0;

            QueryParam searchTerm = new QueryParam();
            if (!search)
            {
                ArchivesDestroyApplyInfo item = new ArchivesDestroyApplyInfo();
                item.ID = 0;
                item.Applicant = Common.Get_UserName;
                item.ApplicantName = "";
                item.ApplyStatus = 0;
                searchTerm = bll.GenerateSearchTerm(item);
            }
            else
            {
                ArchivesDestroyApplyInfo item = new ArchivesDestroyApplyInfo();
                item.ID = 0;
                item.Applicant = Common.Get_UserName;
                item.ApplicantName = Common.inSQL(TextBox2.Text.Trim());
                item.ApplyStatus = (ArchivesDestroyApplyStatus)Convert.ToInt32(DDLStatus.SelectedValue);
                searchTerm = bll.GenerateSearchTerm(item);
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
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long id = Convert.ToInt64(gvRow.Attributes["ID"]);
        if (e.CommandName == "view")
        {
            string url = "ViewArchivesDestroyApply.aspx?cmd=view&id=" + id;
            //Response.Redirect(url);
            System.Web.UI.ScriptManager.RegisterStartupScript(this, Page.GetType(), "ajaxjs", "window.open('" + url + "','_blank');", true);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                ArchivesDestroyApply bll = new ArchivesDestroyApply();
                bll.DelArchivesDestroyApply(id);
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
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

}
