using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using FM2E.BLL.BugReport;
using FM2E.Model.Utils;
using FM2E.Model.BugReportManager;
using WebUtility;

public partial class Module_FM2E_BugReportManager_ViewBugreport_History : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillHaveUpdateData();
            FillNoUpdateData();
        }
    }

    private void FillHaveUpdateData()
    {
        BugReport bll = new BugReport();
        QueryParam qp = (QueryParam)ViewState["HaveUpdateData"];
        if (qp == null)
        {
            BugReportInfo item = new BugReportInfo();
            item.Status = 2;
            qp = bll.GenerateSearchTerm(item);
        }
        qp.PageIndex = AspNetPager1.CurrentPageIndex;
        qp.PageSize = AspNetPager1.PageSize;

        //查询

        int recordcount = 0;
        IList list = bll.GetBugReportList(qp, out recordcount);


        gridview_MessageList.DataSource = list;
        gridview_MessageList.DataBind();

        AspNetPager1.RecordCount = recordcount;
    }
    private void FillNoUpdateData()
    {
        BugReport bll = new BugReport();
        QueryParam qp = (QueryParam)ViewState["NoUpdateData"];
        if (qp == null)
        {
            BugReportInfo item = new BugReportInfo();
            item.Status = 1;
            qp = bll.GenerateSearchTerm(item);
        }
        qp.PageIndex = AspNetPager2.CurrentPageIndex;
        qp.PageSize = AspNetPager2.PageSize;

        //查询

        int recordcount = 0;
        IList list = bll.GetBugReportList(qp, out recordcount);


        gridview2.DataSource = list;
        gridview2.DataBind();

        AspNetPager2.RecordCount = recordcount;
    }
    #region  已修改意见列表的三个相关事件
    protected void gridview_MessageList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            BugReportInfo dv = (BugReportInfo)e.Row.DataItem;
            e.Row.Attributes["BugreportID"] = dv.BugreportID.ToString();

        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillHaveUpdateData();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = gridview_MessageList.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "view")
        {
            string id = gvRow.Attributes["BugreportID"];
            Response.Redirect("ViewBugreport.aspx?cmd=view&id=" + id);
        }
    }
    #endregion
    #region 待修改事件列表的三个相关事件
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            BugReportInfo dv = (BugReportInfo)e.Row.DataItem;
            e.Row.Attributes["BugreportID"] = dv.BugreportID.ToString();

        }
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        FillNoUpdateData();
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = gridview2.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "view")
        {
            string id = gvRow.Attributes["BugreportID"];
            Response.Redirect("SendBugreport.aspx?cmd=view&id=" + id);
        }
    }

    #endregion

    protected void btn_Sure_Click(object sender, EventArgs e)
    {
        BugReportInfo item = new BugReportInfo();
        item.Title = TB_FTitle.Text;
        item.Status = Convert.ToInt32(DDL_Status.SelectedValue);
        BugReport bll = new BugReport();

        switch (Convert.ToInt32(DDL_Status.SelectedValue))
        {
            case 1:
                {
                    ViewState["NoUpdateData"] = (QueryParam)bll.GenerateSearchTerm(item);
                    FillNoUpdateData();
                    TabContainer1.ActiveTabIndex = 0;
                    break;
                }
            case 2:
                {
                    ViewState["HaveUpdateData"] = (QueryParam)bll.GenerateSearchTerm(item);
                    FillHaveUpdateData();
                    TabContainer1.ActiveTabIndex = 1;
                    break;
                }
            default: break;
        }
    }


}
