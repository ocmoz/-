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
using WebUtility.Components;
using FM2E.Model.Utils;
using FM2E.BLL.Budget;
using FM2E.Model.Budget;
using FM2E.WorkflowLayer;

public partial class Module_FM2E_BudgetManager_AnnualBudgetApprovalManager_AnnualBudget : System.Web.UI.Page
{
    string companyid = UserData.CurrentUserData.CompanyID;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.RemoveAll();
            FillData();
            
        }

    }

    private void FillData()
    {
        try
        {
            AnnualBudget bll = new AnnualBudget();
            BudgetYearInfo budgetyearinfo = new BudgetYearInfo();
            //budgetyearinfo.Status = Convert.ToInt16(2);
            budgetyearinfo.WorkFlowStatus.Add(BudgetYearWorkflow.Wait4FinanceApprovalState);
            budgetyearinfo.WorkFlowStatus.Add(BudgetYearWorkflow.Wait4LeaderApprovalState);
            budgetyearinfo.WorkFlowStatus.Add(BudgetYearWorkflow.Wait4CompanyApprovalState);
            //budgetyearinfo.CompanyID = companyid;
            budgetyearinfo.WorkFlowUserName = Common.Get_UserName;
            QueryParam qp = bll.GenerateSearchTerm(budgetyearinfo);
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;
            
            int recordCount = 0;
            IList list = bll.GetBudgetYearList(qp, out recordCount, null);
            GridView1.DataSource = list;
            GridView1.DataBind();

            AspNetPager1.RecordCount = recordCount;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取年度预算列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
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
            BudgetYearInfo dv = (BudgetYearInfo)e.Row.DataItem;
            e.Row.Attributes["BudgetYearID"] = dv.BudgetYearID.ToString();
        }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "view")
        {
            string BudgetYearID = gvRow.Attributes["BudgetYearID"];
            Response.Redirect("ViewAnnualBudget.aspx?cmd=view&id=" + BudgetYearID);
        }
        if (e.CommandName == "approval")
        {
            string BudgetYearID = gvRow.Attributes["BudgetYearID"];
            Response.Redirect("MakeAnnualBudget.aspx?cmd=edit&id=" + BudgetYearID);
        }
        if (e.CommandName == "del")
        {
            try
            {
                long id = Convert.ToInt64(gvRow.Attributes["BudgetYearID"]);
                AnnualBudget bll = new AnnualBudget();
                bll.DelBudgetYear(id);
                FillData();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        if (e.CommandName == "back")
        {
            try
            {
                string BudgetYearID = gvRow.Attributes["BudgetYearID"];
                AnnualBudget bll = new AnnualBudget();
                BudgetYearInfo info = bll.GetBudgetYear(Convert.ToInt64(BudgetYearID));
                info.Status = 1;
                bll.UpdateBudgetYear(info);
                FillData();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "退回申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
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
}
