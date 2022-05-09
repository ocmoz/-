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
using FM2E.Model.Exceptions;
using FM2E.BLL.BudgetManagement;
using FM2E.Model.BudgetManagement;

public partial class Module_FM2E_BudgetManagement_AnnualBudget_AnnualBudget : System.Web.UI.Page
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
            QueryParam qp = (QueryParam)ViewState["SearchTerm"];
            if (qp == null)
            {
                qp = new QueryParam();

            }
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;
            AnnualBudget bll = new AnnualBudget();
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
        if (e.CommandName == "AddMonthlyBudget")
        {
            string BudgetYearID = gvRow.Attributes["BudgetYearID"];
            Response.Redirect("../QuarterlyBudget/MakeQuarterlyBudget.aspx?cmd=add&BudgetYearID=" + BudgetYearID);
            
        }
        if (e.CommandName == "AddQuarterlyForecast")
        {
            string BudgetYearID = gvRow.Attributes["BudgetYearID"];
            Response.Redirect("../QuarterlyForecast/MakeQuarterlyForecast.aspx?cmd=add&BudgetYearID=" + BudgetYearID);

        }
        if (e.CommandName == "view")
        {
            string BudgetYearID = gvRow.Attributes["BudgetYearID"];
            Response.Redirect("ViewAnnualBudget.aspx?cmd=view&id="+BudgetYearID);
        }
        else if (e.CommandName == "del")
        {
            long id = Convert.ToInt64(gvRow.Attributes["BudgetYearID"]);
            AnnualBudget bll = new AnnualBudget();
            BudgetYearInfo budgetyearinfo = bll.GetBudgetYear(id);
            if(budgetyearinfo == null)
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "本年度预算已被删除！", new WebException("本年度预算已被删除！"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            //if (budgetyearinfo.WorkFlowStateName != "Draft")
            //    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "本年度预算处于" + budgetyearinfo.WorkFlowStateDescription + "状态，不能删除！", new WebException("本年度预算处于" + budgetyearinfo.WorkFlowStateDescription + "状态，不能删除！"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            //if (bll.GetBudgetYear(id).Status == 2)
            //    EventMessage.MessageBox(Msg_Type.Error, "操作警告", "已经提交到审批，不能删除!", new WebException("已经提交到审批，不能删除!"), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            //if (bll.GetBudgetYear(id).Status == 3)
            //    EventMessage.MessageBox(Msg_Type.Error, "操作警告", "已经审批结束，不能删除!", new WebException("已经审批，不能删除!"), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                 
            try
            {            
                bll.DelBudgetYear(id);
                FillData();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
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
