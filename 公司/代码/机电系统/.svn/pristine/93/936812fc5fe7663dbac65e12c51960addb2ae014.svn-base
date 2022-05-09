using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FM2E.Model.Schedule;
using System.Data;
using System.Collections;
using WebUtility;
using FM2E.BLL.Schedule;
using FM2E.Model.Plan;
using WebUtility.WebControls;
using WebUtility.Components;

public partial class Module_FM2E_Plan_Schedule : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    Schedule bll = new Schedule();

    public List<ScheduleInfo> ScheduleItems
    {
        get
        {
            if (Session["ScheduleItems"] == null)
            {
                return new List<ScheduleInfo>();
            }
            return (List<ScheduleInfo>)Session["ScheduleItems"];
        }
        set
        {
            Session["ScheduleItems"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["ScheduleItems"] = null;
            PlanInfo planItem = new PlanInfo();
            planItem.Id = Convert.ToInt32(id);
            planItem = bll.GetPlan(planItem);
            lbDepartment.Text = planItem.Department;
            lbPlanTime.Text = planItem.Year + "年" + planItem.Month + "月";
            ScheduleInfo scheduleItems = new ScheduleInfo();
            List<ScheduleInfo> list = bll.GetSchedule(planItem.Id);
            ScheduleItems = list;
            FillData();
            if (cmd == "edit")
            {
                isview(false);
            }
            else if (cmd == "view")
            {
                isview(true);
                HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[0];
                button.ButtonUrlType = UrlType.Href;
                button.ButtonVisible = true;
                button.ButtonUrl = string.Format("Schedule.aspx?cmd=edit&id={0}", id);

                HeadMenuButtonItem button1 = HeadMenuWebControls1.ButtonList[1];
                button1.ButtonVisible = false;
            }
        }
    }

    public void isview(bool isview)
    {
        Tr4.Visible = !isview;
        Button1.Visible = !isview;

        for (int i = 0; i < r_UsePlan.Items.Count; i++)
        {
            ((TextBox)(r_UsePlan.Items[i].FindControl("r_tbPlanName"))).Visible = !isview;
            ((TextBox)(r_UsePlan.Items[i].FindControl("r_tbcontent"))).Visible = !isview;
            ((TextBox)(r_UsePlan.Items[i].FindControl("r_tbContractNo"))).Visible = !isview;
            ((TextBox)(r_UsePlan.Items[i].FindControl("r_tbAmount"))).Visible = !isview;
            ((TextBox)(r_UsePlan.Items[i].FindControl("r_tbExpectPaymentTime"))).Visible = !isview;
            ((TextBox)(r_UsePlan.Items[i].FindControl("r_tbRemark"))).Visible = !isview;
            ((ImageButton)(r_UsePlan.Items[i].FindControl("ibDelEqItems"))).Visible = !isview;

            ((Label)(r_UsePlan.Items[i].FindControl("r_lbPlanName"))).Visible = isview;
            ((Label)(r_UsePlan.Items[i].FindControl("r_lbcontent"))).Visible = isview;
            ((Label)(r_UsePlan.Items[i].FindControl("r_lbContractNo"))).Visible = isview;
            ((Label)(r_UsePlan.Items[i].FindControl("r_lbAmount"))).Visible = isview;
            ((Label)(r_UsePlan.Items[i].FindControl("r_lbExpectPaymentTime"))).Visible = isview;
            ((Label)(r_UsePlan.Items[i].FindControl("r_lbRemark"))).Visible = isview;        
        }
    }

    private void FillData()
    {
        r_UsePlan.DataSource = ScheduleItems;
        r_UsePlan.DataBind();
    }
    protected void rpUsePlanItems_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int index = int.Parse(e.CommandArgument.ToString());
        if (e.CommandName == "del")
        {
            //删除
            List<ScheduleInfo> list = ScheduleItems;
            if (list == null || list.Count == 0)
                return;
            list.RemoveAt(index);
            FillData();
        }
    }
    protected void btAddUsePlanItems_Click(object sender, EventArgs e)
    {
        if (tbPlanName.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "v1", "alert('请输入项目名称！')", true);
            return;
        }      
          if (tbAmount.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "v2", "alert('请输入项目金额！')", true);
            return;
        }
          if (tbExpectPaymentTime.Value.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "v2", "alert('请输入支付时间！')", true);
            return;
        }

           ScheduleInfo item = new ScheduleInfo();
           List<ScheduleInfo> list = ScheduleItems;
        try
        {
            item.ProjectName = tbPlanName.Text.Trim();
            item.Content = tbcontent.Text.Trim();
            item.ContractNo = tbContractNo.Text.Trim();
            item.Amount = Convert.ToDecimal(tbAmount.Text.Trim());
            item.PaymentTime =Convert.ToDateTime(tbExpectPaymentTime.Value) ;
            item.Remark = tbRemark.Text.Trim();
            list.Add(item);
            ScheduleItems = list;
            FillData();
            isview(false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "v3", "alert('错误，输入的格式不正确！(" + ex.Message + ")')", true);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ScheduleInfo item = new ScheduleInfo();
        try
        {
            for (int i = 0; i < r_UsePlan.Items.Count; i++)
            {
                ScheduleInfo itemlist = new ScheduleInfo();
                itemlist.ProjectName = ((TextBox)(r_UsePlan.Items[i].FindControl("r_tbPlanName"))).Text;
                itemlist.Content = ((TextBox)(r_UsePlan.Items[i].FindControl("r_tbcontent"))).Text;
                itemlist.ContractNo = ((TextBox)(r_UsePlan.Items[i].FindControl("r_tbContractNo"))).Text;
                itemlist.Amount = Convert.ToDecimal(((TextBox)(r_UsePlan.Items[i].FindControl("r_tbAmount"))).Text);
                itemlist.PaymentTime = Convert.ToDateTime(((TextBox)(r_UsePlan.Items[i].FindControl("r_tbExpectPaymentTime"))).Text);
                itemlist.Remark = ((TextBox)(r_UsePlan.Items[i].FindControl("r_tbRemark"))).Text;
                item.Schedulelist.Add(itemlist);
            }
            bll.AddAndUpdateSchedule(id, item);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改添加失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        EventMessage.MessageBox(Msg_Type.Error, "操作成功", "修改添加成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Schedule.aspx?cmd=view&id="+id), UrlType.Href, "");
    }  
}
