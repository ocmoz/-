﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FM2E.BLL.Insurance;
using FM2E.BLL.System;
using FM2E.Model.Insurance;
using FM2E.Model.System;
using WebUtility;
using WebUtility.Components;

public partial class Module_FM2E_InsureManager_InsureInfoManager_InsuranceReportlist : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
            PermissionControl();
        }
    }
    private void PermissionControl()
    {
        if (SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[7].Visible = true;
        else GridView1.Columns[7].Visible = false;

    }
    private void FillData()
    {
        try
        {
            Module module = new Module();
            InsuranceReport insuranceReportBll = new InsuranceReport();
            InsuranceReportSearchInfo term = GetSearchTerm();
            int recordCount = 0;
            IList list = insuranceReportBll.GetInsuranceReport(term, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out recordCount);
            GridView1.DataSource = list;
            GridView1.DataBind();
            AspNetPager1.RecordCount = recordCount;

            ddl_search_riskType.Items.Clear();
            ddl_search_riskType.Items.Add(new ListItem("不限", "0"));
            ddl_search_riskType.Items.AddRange(EnumHelper.GetListItems(typeof(RiskType)));

            ddl_search_state.Items.Clear();
            ddl_search_state.Items.Add(new ListItem("不限", "0"));
            ddl_search_state.Items.AddRange(EnumHelper.GetListItems(typeof(State)));


        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取保单信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        string id = gvRow.Attributes["Id"];

        if (e.CommandName == "view")
        {
            Response.Redirect("InsuranceReportManager.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "del")
        {


            bool bSuccess = false;
            try
            {
                Insurance insuranceBll = new Insurance();
                insuranceBll.DelInsurance(id);
                bSuccess = true;
                // GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Visible = false;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除保单失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除保单成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("InsuranceReportlist.aspx?"), UrlType.Href, "");
            }
        }
        else if (e.CommandName == "repair")
        {
            Response.Redirect("InsuranceReportManager.aspx?cmd=repair&id=" + id);
        }
        else if (e.CommandName == "review")
        {
            Response.Redirect("InsuranceReportManager.aspx?cmd=review&id=" + id);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        User userBll = new User();
        UserInfo info = userBll.GetUser(Common.Get_UserName);
        List<string> roles = new List<string>();
        foreach (UserRoleInfo item in info.Roles)
        {
            roles.Add(item.RoleName);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            InsuranceReportInfo insuranceReportInfo = (InsuranceReportInfo)e.Row.DataItem;
            e.Row.Attributes["Id"] = Convert.ToString(insuranceReportInfo.Id);
            e.Row.Cells[8].Controls[0].Visible = false;
            e.Row.Cells[9].Controls[0].Visible = false;

            if (insuranceReportInfo.State == State.New && roles.Contains("报险修复"))
            {
                e.Row.Cells[8].Controls[0].Visible = true;
            }
            else if (insuranceReportInfo.State == State.Repaired && roles.Contains("报险复核"))
            {
                e.Row.Cells[9].Controls[0].Visible = true;

            }
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSearch_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        FillData();
    }

    /// <summary>
    /// 获取查询条件
    /// </summary>
    /// <returns></returns>
    private InsuranceReportSearchInfo GetSearchTerm()
    {
        InsuranceReportSearchInfo item = new InsuranceReportSearchInfo();
        item.InsuranceNo = tb_search_insuranceNo.Text.Trim();
        item.ReportNo = tb_search_reportNo.Text.Trim();
        if (!string.IsNullOrEmpty(tb_search_startReportDate.Text.Trim()))
        {
            item.StartReportDate = Convert.ToDateTime(tb_search_startReportDate.Text.Trim());
        }
        if (!string.IsNullOrEmpty(tb_search_endReportDate.Text.Trim()))
        {
            item.EndReportDate = Convert.ToDateTime(tb_search_endReportDate.Text.Trim());
        }
        if (!string.IsNullOrEmpty(tb_search_startRiskDate.Text))
        {
            item.StartRiskDate = Convert.ToDateTime(tb_search_startRiskDate.Text.Trim());
        }
        if (!string.IsNullOrEmpty(tb_search_endRiskDate.Text))
        {
            item.EndRiskDate = Convert.ToDateTime(tb_search_endRiskDate.Text.Trim());
        }
        string riskTypeTemp = ddl_search_riskType.SelectedValue;
        if (!string.IsNullOrEmpty(riskTypeTemp.Trim()))
        {
            item.RiskType = (RiskType)Convert.ToInt32(riskTypeTemp);
        }
        string stateTemp = ddl_search_state.SelectedValue;
        if (!string.IsNullOrEmpty(stateTemp.Trim()))
        {
            item.State = (State)Convert.ToInt32(stateTemp);
        }

        return item;
    }
}
