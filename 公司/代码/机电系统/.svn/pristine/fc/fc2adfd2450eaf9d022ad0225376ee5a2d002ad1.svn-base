using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FM2E.Model.MonthFundsUsePlan;
using FM2E.Model.Schedule;
using FM2E.BLL.Schedule;
using WebUtility;
using FM2E.Model.Plan;
using WebUtility.WebControls;
using WebUtility.Components;

public partial class Module_FM2E_Plan_MonthFundsUsePlan : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    Schedule bll = new Schedule();

    public PlanInfo PlanItems
    {
        get
        {
            if (Session["PlanItems"] == null)
            {
                return new PlanInfo();
            }
            return (PlanInfo)Session["PlanItems"];
        }
        set
        {
            Session["PlanItems"] = value;
        }
    }

    public void isview(bool isview)
    {
        lbPlanTime.Visible = isview;
        PlanTime.Visible = !isview;
        lb_UseDifferencesReasons.Visible = isview;
        tb_UseDifferencesReasons.Visible = !isview;
        lb_IncomeDifferencesReasons.Visible = isview;
        tb_IncomeDifferencesReasons.Visible = !isview;
        lb_Income.Visible = isview;
        Button1.Visible = !isview;
        opinion2.Visible = !isview;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["PlanItems"] = null;
            PlanInfo planItem = new PlanInfo();
            planItem.Id = Convert.ToInt32(id);
            planItem = bll.GetPlan(planItem);
            PlanItems = planItem;
            lbDepartment.Text = planItem.Department;
            lbPlanTime.Text = planItem.Year + "年" + planItem.Month + "月";
            tb_UseDifferencesReasons.Text = planItem.UseReasonsDifferences;
            tb_IncomeDifferencesReasons.Text = planItem.IncomeReasonsDifferences;

            ScheduleInfo scheduleItems = new ScheduleInfo();
            List<ScheduleInfo> list1 = bll.GetScheduleGroupBy(planItem.Id);
            List<ScheduleInfo> list2 = bll.GetScheduleIncomeGroupBy(planItem.Id);

            r_UsePlan.DataSource = list1;
            r_UsePlan.DataBind();
            r_IncomePlan.DataSource = list2;
            r_IncomePlan.DataBind();

            DateTime lastMonth = Convert.ToDateTime(lbPlanTime.Text).AddMonths(-1);
            PlanInfo lastMonthplanItem = new PlanInfo();
            lastMonthplanItem.Year = lastMonth.Year.ToString();
            lastMonthplanItem.Month = lastMonth.Month.ToString();
            lastMonthplanItem = bll.GetLastPlan(lastMonthplanItem);
            if (lastMonthplanItem != null)
            {


                lb_LastMonthUse.Text = lastMonthplanItem.LastMonthUse.ToString();
                lb_LastMonthincome.Text = lastMonthplanItem.LastMonthIncome.ToString();
                lb_Use.Text = planItem.MonthUse.ToString();
                lb_Income.Text = planItem.MonthIncome.ToString();
                if (planItem.MonthUse != 0)
                    lb_UseDifferences.Text = (lastMonthplanItem.LastMonthUse / planItem.MonthUse).ToString();
                else
                    lb_UseDifferences.Text = "0";
                if (planItem.MonthIncome != 0)
                    lb_IncomeDifferences.Text = (lastMonthplanItem.LastMonthIncome / planItem.MonthIncome).ToString();
                else
                    lb_IncomeDifferences.Text = "0";
            }

            Label1.Text = planItem.DepartmentManager;
            Label2.Text = planItem.DepartmentManagerRemark;
            Label3.Text = planItem.DepartmentManagerTime.ToString();

            decimal UseTotalAmount = 0;
            foreach (ScheduleInfo MonthFundsUsePlanInfo in list1)
            {
                UseTotalAmount += MonthFundsUsePlanInfo.SumAmount;
            }
            lbUseTotalAmount.Text = UseTotalAmount.ToString();

            decimal incomeTotalAmount = 0;
            foreach (ScheduleInfo MonthFundsUsePlanInfo in list2)
            {
                incomeTotalAmount += MonthFundsUsePlanInfo.SumAmount;
            }
            lbincomeTotalAmount.Text = incomeTotalAmount.ToString();

            if (cmd == "view")
            {
                isview(true);
                lb_UseDifferencesReasons.Text = planItem.UseReasonsDifferences;
                lb_IncomeDifferencesReasons.Text = planItem.IncomeReasonsDifferences;

                HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[0];
                button.ButtonUrlType = UrlType.Href;
                button.ButtonVisible = true;
                button.ButtonUrl = string.Format("MonthFundsUsePlan.aspx?cmd=edit&id={0}", id);

                HeadMenuButtonItem button1 = HeadMenuWebControls1.ButtonList[1];
                button1.ButtonUrlType = UrlType.JavaScript;
                button1.ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');", id);
            }
            else if (cmd == "edit")
            {
                HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[0];
                button.ButtonVisible = false;
                isview(false);
                PlanTime.Value = planItem.Year + "年" + planItem.Month + "月";
                tb_UseDifferencesReasons.Text = planItem.UseReasonsDifferences;
                tb_IncomeDifferencesReasons.Text = planItem.IncomeReasonsDifferences;
            }
            else if (cmd == "sp")
            {
                isview(true);
                opinion2.Visible = true;
                HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[0];
                button.ButtonVisible = false;
                HeadMenuButtonItem button1 = HeadMenuWebControls1.ButtonList[1];
                button1.ButtonVisible = false;
                Button1.Visible = true;
            }
            else if (cmd == "delete")
            {
                //执行删除操作
                bool bSuccess = false;
                try
                {

                    bll.DelPlan(Convert.ToInt32(id));

                    bSuccess = true;
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                }
                if (bSuccess == true)
                {
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除申请成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("MonthFundsUsePlanList.aspx"), UrlType.Href, "");
                }
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        PlanInfo item = new PlanInfo();

        item = PlanItems;
        if (cmd == "edit")
        {
            if (PlanTime.Value.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "v1", "alert('请输入计划日期！')", true);
                return;
            }
            item.Year = Convert.ToDateTime(PlanTime.Value).Year.ToString();
            item.Month = Convert.ToDateTime(PlanTime.Value).Month.ToString();
            item.UseReasonsDifferences = tb_UseDifferencesReasons.Text;
            item.IncomeReasonsDifferences = tb_IncomeDifferencesReasons.Text;
            item.DepartmentManagerRemark = tbApprovalRemark.Text;
        }
        else
        {
            item.DepartmentManager = UserData.CurrentUserData.PersonName;
            item.DepartmentManagerRemark = tbApprovalRemark.Text;
            item.DepartmentManagerTime = DateTime.Now;
        }
        try
        {
            bll.UpdatePlan(item);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", "成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("MonthFundsUsePlanList.aspx"), UrlType.Href, "");
    }
}
