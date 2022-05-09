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
using FM2E.BLL.Budget;
using FM2E.Model.Budget;

public partial class Module_FM2E_BudgetManager_AnnualBudgetManager_ShowChart : System.Web.UI.Page
{
    long id = (long)Common.sink("YearID", MethodType.Get, 255, 0, DataType.Long);
    string workflownameparam = (string)Common.sink("workflowname", MethodType.Get, 50, 0, DataType.Str);

    protected void Page_Load(object sender, EventArgs e)
    {
        
        WorkFlowChart newViewer = Page.LoadControl("~/Module/FM2E/WorkflowManager/WorkflowEditor/WorkFlowChart.ascx") as WorkFlowChart;
        newViewer.id = id;
        newViewer.workflowname = workflownameparam;
        if (id != 0)
        {
            AnnualBudget bll = new AnnualBudget();
            BudgetYearInfo budgetyearinfo = bll.GetBudgetYear(id);
            newViewer.workflowstate = budgetyearinfo.WorkFlowStateName;
        }
        ShowChartContent.Controls.Add(newViewer);

    }
}
