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
using FM2E.Model.Utils;
using FM2E.BLL.BugReport;
using FM2E.Model.BugReportManager;
using WebUtility;

public partial class Module_FM2E_BugReportManager_SendBugreport_History : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
        }
    }

    private void FillData()
    {
        BugReport bll = new BugReport();
        QueryParam qp = (QueryParam)ViewState["SearchTerm"];
        if (qp == null)
        {
            BugReportInfo item = new BugReportInfo();
            item.SenderID = UserData.CurrentUserData.UserName;
            qp = bll.GenerateSearchTerm(item);
        }
        qp.PageIndex = AspNetPager1.CurrentPageIndex;
        qp.PageSize = AspNetPager1.PageSize;

        //查询
        
        int recordcount = 0;
        IList list = bll.GetBugReportList(qp, out recordcount);


        Repeater_ReportList.DataSource = list;
        Repeater_ReportList.DataBind();

        AspNetPager1.RecordCount = recordcount;


    }

    //protected void gridview_MessageList_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        //鼠标移动到每项时颜色交替效果    
    //        e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
    //        e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

    //        //设置悬浮鼠标指针形状为"小手"    
    //        e.Row.Attributes["style"] = "Cursor:hand";

    //        BugReportInfo dv = (BugReportInfo)e.Row.DataItem;
    //        e.Row.Attributes["BugreportID"] = dv.BugreportID.ToString();

    //    }
    //}

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }

    //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    GridViewRow gvRow = gridview_MessageList.Rows[Convert.ToInt32(e.CommandArgument)];
    //    if (e.CommandName == "view")
    //    {
    //        string id = gvRow.Attributes["BugreportID"];
    //        Response.Redirect("ViewBugreport.aspx?cmd=view&id=" + id);
    //    }
    //}


}
