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
using WebUtility;
using FM2E.Model.BugReportManager;
using FM2E.BLL.BugReport;

public partial class Module_FM2E_BugReportManager_SendBugreport_ViewBugreport : System.Web.UI.Page
{
    long id = (long)Common.sink("id", MethodType.Get, 0, 0, DataType.Long);
    protected void Page_Load(object sender, EventArgs e)
    {
        InitiaPage();
    }

    private void InitiaPage()
    {
        BugReport bll = new BugReport();
        BugReportInfo item = bll.GetBugReport(id);
        LB_Title.Text = item.Title;
        LB_Message.Text = item.Message;
        attachment.HRef = item.Attachment;
        LB_Report.Text = item.Report;

    }
}
