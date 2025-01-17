﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;
using FM2E.BLL.PendingOrder;
using FM2E.Model.PendingOrder;
using System.Collections;
using FM2E.Model.Plan;
using FM2E.BLL.Schedule;

public partial class Module_FM2E_Plan_MonthFundsUsePlanList : System.Web.UI.Page
{
    Schedule bll = new Schedule();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {         
            FillData();
        }
    }

    private void FillData()
    {
        int recordCount = 0;
        IList list = bll.GetPlanList(PlanTime.Value, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out recordCount);
        GridView1.DataSource = list;
        GridView1.DataBind();
        AspNetPager1.RecordCount = recordCount;
        PlanTime.Value = "";
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
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "view")
        {
            Response.Redirect("MonthFundsUsePlan.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "viewUsePlan")
        {
            Response.Redirect("Schedule.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "viewUseActual")
        {
            Response.Redirect("ScheduleActual.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "viewIncomePlan")
        {
            Response.Redirect("ScheduleIncome.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "viewIncomeActual")
        {
            Response.Redirect("ScheduleIncomeActual.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "sp")
        {
            Response.Redirect("MonthFundsUsePlan.aspx?cmd=sp&id=" + id);
        }
        else if (e.CommandName == "del")
        {
            bll.DelPlan(id);
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        PlanInfo item = new PlanInfo();        
        if (PlanTime.Value.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "v1", "alert('请输入计划日期！')", true);
            return;
        }

        item.Year = Convert.ToDateTime(PlanTime.Value).Year.ToString();
        item.Month = Convert.ToDateTime(PlanTime.Value).Month.ToString();
        if (bll.GetPlan(item) == null)
        {
            item.Department = UserData.CurrentUserData.DepartmentName;
            item.Producer = UserData.CurrentUserData.PersonName;
            item.ProducerTime = DateTime.Now;
            bll.AddPlan(item);
            FillData();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "v1", "alert('该计划已经存在！')", true);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (PlanTime.Value.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "v1", "alert('请输入计划日期！')", true);
            return;
        }
        FillData();
    }
}
