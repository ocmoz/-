using System;
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

using WebUtility;
using WebUtility.Components;
using WebUtility.WebControls;

using FM2E.Model.Maintain;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.Model.Equipment;
using FM2E.WorkflowLayer;

using FM2E.BLL.Basic;
using FM2E.BLL.Maintain;
using FM2E.BLL.Equipment;

using CrystalDecisions.CrystalReports.Engine;


public partial class Module_FM2E_MaintainManager_DailyPatrolManager_DailyPatrolView_DailyPatrolTrack : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long itemid = (long)Common.sink("itemID", MethodType.Get, 50, 0, DataType.Long);
    DateTime minDate = (DateTime)Common.sink("minDate", MethodType.Get, 50, 0, DataType.Dat);
    DateTime maxDate = (DateTime)Common.sink("maxDate", MethodType.Get, 50, 0, DataType.Dat);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FilldataForGridView3();
        }
    }
    /// <summary>
    /// 初始化页面
    /// </summary>

    private void InitialPage()
    {
        try
        {
            Session.Remove("ReportList");
            TabContainer1.Tabs[1].Visible = false;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    private void FilldataForGridView3()
    {
        MaintainPlanConfig bll = new MaintainPlanConfig();
        MaintainPlanConfigInfo detail = bll.GetMaintainPlanConfig(itemid);
        Label2.Text = detail.SystemName;
        Label3.Text = detail.SubsystemName;
        Label4.Text = detail.PlanPeriodString;
        Label5.Text = detail.PlanObject;
        Label6.Text = detail.PlanContent;
        Label7.Text = detail.CheckStandard;
        MaintainPlanRecord rbll = new MaintainPlanRecord();
        IList rlist = rbll.GetAllRecord(detail.ItemID);

        DateTime NowDate = DateTime.Now.Date;
        if (minDate > NowDate)//当前时间还没达到计划开始时间
            return;
        TimeSpan span = maxDate.Subtract(minDate);
        int spanDays = span.Days;
        int recordCount = CountOfRecord(spanDays);
        ArrayList tlist = new ArrayList();
        for (int i = 0; i < recordCount; i++)
        {
            MaintainPlanTrackInfo item = new MaintainPlanTrackInfo();
            item.StartDate = minDate + getStartSpan(i);
            item.EndDate = minDate + getStartSpan(i + 1);
            item.EndDate = item.EndDate.Subtract(new TimeSpan(1, 0, 0, 0));
            item.RecordmanName = string.Empty;
            item.RecordDate = DateTime.MinValue;
            item.VerifyName = string.Empty;
            item.RecordResult = string.Empty;
            item.VerifiedResult = MaintainPlanVerifiedResult.NotImplemented;
            tlist.Add(item);
        }

        foreach (MaintainPlanRecordInfo rInfo in rlist)
        {
            foreach (MaintainPlanTrackInfo tInfo in tlist)
            {
                if (tInfo.StartDate <= rInfo.RecordDate && rInfo.RecordDate <= tInfo.EndDate)
                {
                    tInfo.VerifiedResult = rInfo.VerifiedResult;
                    tInfo.VerifyBy = rInfo.VerifyBy;
                    tInfo.VerifyName = rInfo.VerifyName;
                    tInfo.RecordDate = rInfo.RecordDate;
                    tInfo.RecordResult = rInfo.RecordResult;
                    tInfo.RecordmanName = rInfo.RecordmanName;
                    break;
                }
            }
        }

        Hashtable ht = new Hashtable();
        ht.Add(MaintainPlanVerifiedResult.NotImplemented, 0);
        ht.Add(MaintainPlanVerifiedResult.CompletedAsPlanned, 0);
        ht.Add(MaintainPlanVerifiedResult.NotCompleted, 0);
        ht.Add(MaintainPlanVerifiedResult.NotVerified, 0);
        foreach (MaintainPlanTrackInfo tInfo in tlist)
        {
            switch (tInfo.VerifiedResult)
            {
                case MaintainPlanVerifiedResult.NotImplemented: int tmp = (int)ht[MaintainPlanVerifiedResult.NotImplemented] + 1; ht.Remove(MaintainPlanVerifiedResult.NotImplemented); ht.Add(MaintainPlanVerifiedResult.NotImplemented, tmp); break;
                case MaintainPlanVerifiedResult.CompletedAsPlanned: tmp = (int)ht[MaintainPlanVerifiedResult.CompletedAsPlanned] + 1; ht.Remove(MaintainPlanVerifiedResult.CompletedAsPlanned); ht.Add(MaintainPlanVerifiedResult.CompletedAsPlanned, tmp); break;
                case MaintainPlanVerifiedResult.NotCompleted: tmp = (int)ht[MaintainPlanVerifiedResult.NotCompleted] + 1; ht.Remove(MaintainPlanVerifiedResult.NotCompleted); ht.Add(MaintainPlanVerifiedResult.NotCompleted, tmp); break;
                case MaintainPlanVerifiedResult.NotVerified: tmp = (int)ht[MaintainPlanVerifiedResult.NotVerified] + 1; ht.Remove(MaintainPlanVerifiedResult.NotVerified); ht.Add(MaintainPlanVerifiedResult.NotVerified, tmp); break;
                default: break;
            }
        }
        Session["ReportList"] = tlist;
        ViewState["ReportData"] = ht;
        int min = (AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize;
        int max = (AspNetPager1.CurrentPageIndex * AspNetPager1.PageSize) > tlist.Count ? tlist.Count : AspNetPager1.CurrentPageIndex * AspNetPager1.PageSize;
        max = max - 1;
        ArrayList thisList = tlist.GetRange(min, max - min + 1);
        AspNetPager1.RecordCount = tlist.Count;
        GridView3.DataSource = thisList;
        GridView3.DataBind();
    }
    private int CountOfRecord(int spanDays)
    {
        MaintainPlanConfig bll = new MaintainPlanConfig();
        MaintainPlanConfigInfo detail = bll.GetMaintainPlanConfig(itemid);
        double periodDays = 0;
        switch (detail.PeriodUnit)
        {
            case MaintainPlanPeriodUnit.Hour: periodDays = spanDays * 24 / detail.PlanPeriod; break;
            case MaintainPlanPeriodUnit.Day: periodDays = spanDays / detail.PlanPeriod; break;
            case MaintainPlanPeriodUnit.Week: periodDays = spanDays / 7 / detail.PlanPeriod; break;
            case MaintainPlanPeriodUnit.Month: periodDays = spanDays / 30 / detail.PlanPeriod; break;
            case MaintainPlanPeriodUnit.Season: periodDays = spanDays / 90 / detail.PlanPeriod; break;
            case MaintainPlanPeriodUnit.Year: periodDays = spanDays / 360 / detail.PlanPeriod; break;
        }
        return Convert.ToInt32(periodDays) + 1;
    }
    private TimeSpan getStartSpan(int i)
    {
        MaintainPlanConfig bll = new MaintainPlanConfig();
        MaintainPlanConfigInfo detail = bll.GetMaintainPlanConfig(itemid);

        if (detail.PeriodUnit == MaintainPlanPeriodUnit.Hour)
        {
            return new TimeSpan(detail.PlanPeriod, 0, 0);
        }

        int periodDays = 0;
        switch (detail.PeriodUnit)
        {
            case MaintainPlanPeriodUnit.Day: periodDays = detail.PlanPeriod; break;
            case MaintainPlanPeriodUnit.Week: periodDays = 7 * detail.PlanPeriod; break;
            case MaintainPlanPeriodUnit.Month: periodDays = 30 * detail.PlanPeriod; break;
            case MaintainPlanPeriodUnit.Season: periodDays = 90 * detail.PlanPeriod; break;
            case MaintainPlanPeriodUnit.Year: periodDays = 360 * detail.PlanPeriod; break;
        }
        return new TimeSpan(i * periodDays, 0, 0, 0);
    }
    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FilldataForGridView3();
    }
    protected void btnReport1_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dataset = new DataSet();
            DataTable datatable = new DataTable("ReportView");
            dataset.Tables.Add(datatable);
            DataColumn column;

            column = new DataColumn();
            column.ColumnName = "CategoryName";
            column.DataType = Type.GetType("System.String");
            column.Unique = false;
            datatable.Columns.Add(column);
            column = new DataColumn();
            column.ColumnName = "CategoryValue";
            column.DataType = Type.GetType("System.Decimal");
            column.Unique = false;
            datatable.Columns.Add(column);

            Hashtable ht = (Hashtable)ViewState["ReportData"];
            DataRow datarow;
            datarow = datatable.NewRow();
            datarow["CategoryName"] = "未执行";
            datarow["CategoryValue"] = ht[MaintainPlanVerifiedResult.NotImplemented];
            datatable.Rows.Add(datarow);
            datarow = datatable.NewRow();
            datarow["CategoryName"] = "未按计划执行";
            datarow["CategoryValue"] = ht[MaintainPlanVerifiedResult.NotCompleted];
            datatable.Rows.Add(datarow);
            datarow = datatable.NewRow();
            datarow["CategoryName"] = "按计划执行";
            datarow["CategoryValue"] = ht[MaintainPlanVerifiedResult.CompletedAsPlanned];
            datatable.Rows.Add(datarow);
            datarow = datatable.NewRow();
            datarow["CategoryName"] = "未审核";
            datarow["CategoryValue"] = ht[MaintainPlanVerifiedResult.NotVerified];
            datatable.Rows.Add(datarow);
            ReportDocument reportdocument = new ReportDocument();

            reportdocument.Load(Server.MapPath("~") + "/report/DailyPatrol/DailyPatrolGrahpReport.rpt");
            reportdocument.SetDataSource(dataset);
            CrystalReportViewer1.ReportSource = reportdocument;
            CrystalReportViewer1.DataBind();
            TabContainer1.Tabs[1].Visible = true;
            TabContainer1.ActiveTabIndex = 1;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "显示报表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    protected void btnReport2_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dataset = new DataSet();
            DataTable datatable = new DataTable("ReportView");
            dataset.Tables.Add(datatable);
            DataColumn column;

            column = new DataColumn();
            column.ColumnName = "CategoryName";
            column.DataType = Type.GetType("System.String");
            column.Unique = false;
            datatable.Columns.Add(column);
            column = new DataColumn();
            column.ColumnName = "CategoryValue";
            column.DataType = Type.GetType("System.Decimal");
            column.Unique = false;
            datatable.Columns.Add(column);

            ArrayList list = (ArrayList)Session["ReportList"];
            DataRow datarow;
            ReportDocument reportdocument = new ReportDocument();

            MaintainPlanConfig bll = new MaintainPlanConfig();
            MaintainPlanConfigInfo detail = bll.GetMaintainPlanConfig(itemid);
            switch (detail.PeriodUnit)
            {

                case MaintainPlanPeriodUnit.Year:
                    {
                        foreach (MaintainPlanTrackInfo tInfo in list)
                        {
                            datarow = datatable.NewRow();
                            datarow["CategoryName"] = tInfo.PlanDateString;
                            datarow["CategoryValue"] = (int)tInfo.VerifiedResult;
                            datatable.Rows.Add(datarow);
                        }
                        reportdocument.Load(Server.MapPath("~") + "/report/DailyPatrol/DailyPatrolListReport1.rpt");
                        break;
                    }
                case MaintainPlanPeriodUnit.Season:
                    {
                        foreach (MaintainPlanTrackInfo tInfo in list)
                        {
                            datarow = datatable.NewRow();
                            datarow["CategoryName"] = tInfo.PlanDateString;
                            datarow["CategoryValue"] = (int)tInfo.VerifiedResult;
                            datatable.Rows.Add(datarow);
                        }
                        reportdocument.Load(Server.MapPath("~") + "/report/DailyPatrol/DailyPatrolListReport2.rpt");
                        break;
                    }
                case MaintainPlanPeriodUnit.Month:
                    {
                        foreach (MaintainPlanTrackInfo tInfo in list)
                        {
                            datarow = datatable.NewRow();
                            datarow["CategoryName"] = tInfo.PlanDateString;
                            datarow["CategoryValue"] = (int)tInfo.VerifiedResult;
                            datatable.Rows.Add(datarow);
                        }
                        reportdocument.Load(Server.MapPath("~") + "/report/DailyPatrol/DailyPatrolListReport3.rpt");
                        break;
                    }
                case MaintainPlanPeriodUnit.Week:
                case MaintainPlanPeriodUnit.Day:
                    {
                        foreach (MaintainPlanTrackInfo tInfo in list)
                        {
                            datarow = datatable.NewRow();
                            datarow["CategoryName"] = tInfo.StartDate.ToShortDateString();
                            datarow["CategoryValue"] = (int)tInfo.VerifiedResult;
                            datatable.Rows.Add(datarow);
                        }
                        reportdocument.Load(Server.MapPath("~") + "/report/DailyPatrol/DailyPatrolListReport4.rpt");
                        break;
                    }
            }

            reportdocument.SetDataSource(dataset);
            CrystalReportViewer1.ReportSource = reportdocument;
            CrystalReportViewer1.DataBind();
            TabContainer1.Tabs[1].Visible = true;
            TabContainer1.ActiveTabIndex = 1;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "显示报表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
}
