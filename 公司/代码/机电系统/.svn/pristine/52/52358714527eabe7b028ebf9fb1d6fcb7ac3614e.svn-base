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
using FM2E.Model.Utils;
using FM2E.BLL.Budget;
using WebUtility.Components;
using FM2E.Model.Budget;
using System.Collections.Generic;
using FM2E.Model.Exceptions;
using CrystalDecisions.CrystalReports.Engine;
using FM2E.WorkflowLayer;
using FM2E.Model.Basic;
using FM2E.BLL.Basic;
using System.IO;

public partial class Module_FM2E_BudgetManager_BudgetStaticsManager_MonthlyBudget : System.Web.UI.Page
{
    ReportDocument reportdocument = new ReportDocument();
    ReportDocument reportdocument2 = new ReportDocument();
    string companyid = UserData.CurrentUserData.CompanyID;
    bool inittreeyet = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.RemoveAll();
            FillData();
            AddTree2(0, (TreeNode)null);
            TreeView2.ShowLines = true;
        }

    }

    private void Page_Unload(object sender, EventArgs e)
    {
        reportdocument.Dispose();
        reportdocument2.Dispose();
    }

    private void FillData()
    {
        try
        {
            AnnualBudget bll = new AnnualBudget();
            BudgetPerMonthTotalInfo budgetpermonthtotalinfo = new BudgetPerMonthTotalInfo();
            //budgetpermonthtotalinfo.Status = Convert.ToInt16(3);
            //budgetpermonthtotalinfo.CompanyID = companyid;

            //budgetpermonthtotalinfo.WorkFlowStatus.Add(BudgetMonthlyWorkflow.DraftState);
            QueryParam qp = bll.GenerateSearchTerm(budgetpermonthtotalinfo);
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;
            int recordCount = 0;
            IList list = bll.GetBudgetPerMonthTotalList(qp, out recordCount, null);
            GridView1.DataSource = list;
            GridView1.DataBind();

            AspNetPager1.RecordCount = recordCount;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取月度预算列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
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
            BudgetPerMonthTotalInfo dv = (BudgetPerMonthTotalInfo)e.Row.DataItem;
            e.Row.Attributes["TotalID"] = dv.TotalID.ToString();
        }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "approval")
        {
            string TotalID = gvRow.Attributes["TotalID"];
            Response.Redirect("MakeMonthlyBudget.aspx?cmd=edit&id=" + TotalID);
        }
        if (e.CommandName == "del")
        {
            try
            {
                long id = Convert.ToInt64(gvRow.Attributes["TotalID"]);
                AnnualBudget bll = new AnnualBudget();
                bll.DelBudgetPerMonthTotal(id);
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
        if (ViewState["dataset"] != null)
        {
            reportdocument.Load(Server.MapPath("~") + "/report/BudgetReport/CrystalReport.rpt");
            reportdocument.SetDataSource(ViewState["dataset"]);
            CrystalReportViewer1.ReportSource = reportdocument;
            CrystalReportViewer1.DataBind();
        }
        if (ViewState["dataset2"] != null)
        {
            reportdocument2.Load(Server.MapPath("~") + "/report/BudgetReport/CrystalReport2.rpt");
            reportdocument2.SetDataSource(ViewState["dataset2"]);
            CrystalReportViewer2.ReportSource = reportdocument2;
            CrystalReportViewer2.DataBind();
        }
    }
    /// <summary>
    /// 费用统计事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Sure_Click(object sender, EventArgs e)
    {
        if ((Convert.ToInt32(BeginYear.Value) > Convert.ToInt32(EndYear.Value)) || ((Convert.ToInt32(BeginYear.Value) == Convert.ToInt32(EndYear.Value)) && (Convert.ToInt32(BeginMonth.SelectedValue) > Convert.ToInt32(EndMonth.SelectedValue))))
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "起始日期不能大于截止日期", new WebException("起始日期不能大于截止日期"), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        SubjectRelation subjectrelationbll = new SubjectRelation();
        AnnualBudget annualbudgetbll = new AnnualBudget();
        SubjectPerYear subjectrelationinfo = new SubjectPerYear();
        subjectrelationinfo.IsLeaf = 1;
        IList subjectrelationinfolist = (List<SubjectRelationInfos>)subjectrelationbll.SearchName(subjectrelationinfo);
        decimal[] staticslist = new decimal[subjectrelationinfolist.Count];

        int year = Convert.ToInt32(BeginYear.Value);
        int month = Convert.ToInt32(BeginMonth.SelectedValue);

        while ((year < Convert.ToInt32(EndYear.Value)) || (year == Convert.ToInt32(EndYear.Value) && month <= Convert.ToInt32(EndMonth.SelectedValue)))
        {
            for (int i = 0; i < subjectrelationinfolist.Count; i++)
            {
                BudgetDetailInfo searchbudgetdetail = new BudgetDetailInfo();
                searchbudgetdetail.Year = year;
                searchbudgetdetail.Month = month;
                searchbudgetdetail.SubName = ((SubjectRelationInfos)subjectrelationinfolist[i]).Name;

                IList Budgetdetaillist = (List<BudgetDetailInfo>)annualbudgetbll.Search(searchbudgetdetail);
                foreach (BudgetDetailInfo computeitem in Budgetdetaillist)
                {
                    staticslist[i] += computeitem.RealExpenditure;
                }
            }


            if (month == 12)
            {
                year++;
                month = 1;
            }
            else
                month++;
        }
        DataSet dataset = new DataSet();
        DataTable datatable = new DataTable("FM2E_BudgetDetail");
        dataset.Tables.Add(datatable);
        DataColumn column;

        column = new DataColumn();
        column.ColumnName = "SubName";
        column.DataType = Type.GetType("System.String");
        column.Unique = false;
        datatable.Columns.Add(column);

        column = new DataColumn();
        column.ColumnName = "RealExpenditure";
        column.DataType = Type.GetType("System.Decimal");
        column.Unique = false;
        datatable.Columns.Add(column);
        DataRow datarow;

        for (int i = 0; i < staticslist.Length; i++)
        {
            if (staticslist[i] != decimal.Zero)
            {
                datarow = datatable.NewRow();
                datarow["SubName"] = ((SubjectRelationInfos)subjectrelationinfolist[i]).Name;
                datarow["RealExpenditure"] = staticslist[i];
                datatable.Rows.Add(datarow);
            }
        }
        reportdocument.Load(Server.MapPath("~") + "/report/BudgetReport/CrystalReport.rpt");
        reportdocument.SetDataSource(dataset);
        ViewState["dataset"] = dataset;
        CrystalReportViewer1.ReportSource = reportdocument;
        CrystalReportViewer1.DataBind();

        if (ViewState["dataset2"] != null)
        {
            reportdocument2.Load(Server.MapPath("~") + "/report/BudgetReport/CrystalReport2.rpt");
            reportdocument2.SetDataSource(ViewState["dataset2"]);
            CrystalReportViewer2.ReportSource = reportdocument2;
            CrystalReportViewer2.DataBind();
        }


    }

    /// <summary>
    /// 初始化种类弹出树
    /// </summary>
    /// <param name="ParentID"></param>
    /// <param name="pNode"></param>
    public void AddTree2(long ParentID, TreeNode pNode)
    {
        SubjectRelationInfos subjectrelationinfo = new SubjectRelationInfos();
        subjectrelationinfo.ParentID = ParentID;
        SubjectRelation bll = new SubjectRelation();
        QueryParam qp = bll.GenerateSearchTerm(subjectrelationinfo);
        int recordcount = 0;
        IList nodelist = bll.GetList(qp, out recordcount, companyid);
        List<SubjectRelationInfos> subnodes = new List<SubjectRelationInfos>();
        foreach (SubjectRelationInfos node in nodelist)
        {
            if (node.ParentID == ParentID)
                subnodes.Add(node);
        }

        //循环递归
        foreach (SubjectRelationInfos node in subnodes)
        {
            //声明节点
            TreeNode Node = new TreeNode();
            //绑定超级链接
            //Node.NavigateUrl = "ViewSubjectRelation.aspx?cmd=view&id=" + node.SubID;
            if (pNode == null)
            {
                Node.Text = node.Name;
                Node.Value = node.SubID.ToString();
                TreeView2.Nodes.Add(Node);
                Node.Expanded = false;


                //HtmlInputText inputtext = new HtmlInputText();
                //TextBox tb = new TextBox();
                //tb.ID = "int" + Node.Value;
                //inputtext.Attributes["style"] = "display:block";
                //inputrow.Controls.Add(tb);
                //inputrow.InnerHtml += "<input type='text' id='int" + Node.Value + "' style='display:block' runat='server' enableviewstate='true' />";
                AddTree2(node.SubID, Node);

            }
            else
            {
                Node.Text = node.Name;
                Node.Value = node.SubID.ToString();
                pNode.ChildNodes.Add(Node);
                Node.Expanded = false;

                //HtmlInputText inputtext = new HtmlInputText();
                //inputtext.ID = "int" + Node.Value;
                //inputtext.Attributes["style"] = "display:none";
                //inputrow.Controls.Add(inputtext);
                //inputrow.InnerHtml += "<input type='text' id='int" + Node.Value + "' style='display:none' runat='server' enableviewstate='true' />";
                AddTree2(node.SubID, Node);
            }
        }
    }

    protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
    {
        SubIDNametb.Text = this.TreeView2.SelectedNode.Text;
        SubIDtb.Text = this.TreeView2.SelectedValue;
        PopupControlExtender1.Commit(SubIDNametb.Text);
        PopupControlExtender2.Commit(SubIDtb.Text);

        if (ViewState["dataset"] != null)
        {
            reportdocument.Load(Server.MapPath("~") + "/report/BudgetReport/CrystalReport.rpt");
            reportdocument.SetDataSource(ViewState["dataset"]);
            CrystalReportViewer1.ReportSource = reportdocument;
            CrystalReportViewer1.DataBind();
        }
        if (ViewState["dataset2"] != null)
        {
            reportdocument2.Load(Server.MapPath("~") + "/report/BudgetReport/CrystalReport2.rpt");
            reportdocument2.SetDataSource(ViewState["dataset2"]);
            CrystalReportViewer2.ReportSource = reportdocument2;
            CrystalReportViewer2.DataBind();
        }
    }
    /// <summary>
    /// 费用跟踪事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Sure2_Click(object sender, EventArgs e)
    {
        if ((Convert.ToInt32(BeginYear2.Value) > Convert.ToInt32(EndYear2.Value)) || ((Convert.ToInt32(BeginYear2.Value) == Convert.ToInt32(EndYear2.Value)) && (Convert.ToInt32(BeginMonth2.SelectedValue) > Convert.ToInt32(EndMonth2.SelectedValue))))
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "起始日期不能大于截止日期", new WebException("起始日期不能大于截止日期"), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        //IList list = new List<decimal>();
        int year = Convert.ToInt32(BeginYear2.Value);
        int month = Convert.ToInt32(BeginMonth2.SelectedValue);

        DataSet dataset = new DataSet();
        DataTable datatable = new DataTable("FM2E_BudgetDetail");
        dataset.Tables.Add(datatable);
        DataColumn column;

        column = new DataColumn();
        column.ColumnName = "SubName";
        column.DataType = Type.GetType("System.String");
        column.Unique = false;
        datatable.Columns.Add(column);

        column = new DataColumn();
        column.ColumnName = "RealExpenditure";
        column.DataType = Type.GetType("System.Decimal");
        column.Unique = false;
        datatable.Columns.Add(column);
        DataRow datarow;

        while ((year < Convert.ToInt32(EndYear2.Value)) || (year == Convert.ToInt32(EndYear2.Value) && month <= Convert.ToInt32(EndMonth2.SelectedValue)))
        {
            //list.Add(GetAllSubRealBudget(year,month,Convert.ToInt64(SubIDtb.Text)));

            datarow = datatable.NewRow();
            datarow["SubName"] = year + "-" + month;
            datarow["RealExpenditure"] = GetAllSubRealBudget(year, month, Convert.ToInt64(SubIDtb.Text));
            datatable.Rows.Add(datarow);

            if (month == 12)
            {
                year++;
                month = 1;
            }
            else
                month++;
        }



        reportdocument2.Load(Server.MapPath("~") + "/report/BudgetReport/CrystalReport2.rpt");
        reportdocument2.SetDataSource(dataset);
        ViewState["dataset2"] = dataset;
        CrystalReportViewer2.ReportSource = reportdocument2;
        CrystalReportViewer2.DataBind();
        if (ViewState["dataset"] != null)
        {
            reportdocument.Load(Server.MapPath("~") + "/report/BudgetReport/CrystalReport.rpt");
            reportdocument.SetDataSource(ViewState["dataset"]);
            CrystalReportViewer1.ReportSource = reportdocument;
            CrystalReportViewer1.DataBind();
        }


    }
    /// <summary>
    /// 获取该种类下所有指定月份的实际开支
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="list"></param>
    /// <param name="SubID"></param>
    /// <returns></returns>
    private decimal GetAllSubRealBudget(int year, int month, long SubID)
    {
        SubjectRelation bll = new SubjectRelation();
        AnnualBudget budgetbll = new AnnualBudget();
        decimal RealBudget = 0;

        if (((SubjectRelationInfos)bll.GetSubjectRelation(SubID)).IsLeaf != 1)
        {
            SubjectRelationInfos searchinfo = new SubjectRelationInfos();
            searchinfo.ParentID = SubID;
            IList subjectlist = (List<SubjectRelationInfos>)bll.Search(searchinfo);
            foreach (SubjectRelationInfos item in subjectlist)
            {
                RealBudget += GetAllSubRealBudget(year, month, item.SubID);
            }
        }
        else
        {
            BudgetDetailInfo searchBudgetdetailinfo = new BudgetDetailInfo();
            searchBudgetdetailinfo.Year = year;
            searchBudgetdetailinfo.Month = month;
            searchBudgetdetailinfo.SubID = SubID;
            IList detaillist = (List<BudgetDetailInfo>)budgetbll.Search(searchBudgetdetailinfo);
            foreach (BudgetDetailInfo item in detaillist)
            {
                RealBudget += item.RealExpenditure;
            }

        }
        return RealBudget;

    }

    protected void Sure_Click4(object sender, EventArgs e)
    {
        BudgetDetailInfo item = new BudgetDetailInfo();
        item.Title = Title4.Value;
        item.Supplier = Supplier5.Value;
        item.StartYear = StartYear4.Value != "" ? Convert.ToInt32(StartYear4.Value) : 0;
        item.EndYear = EndYear4.Value != "" ? Convert.ToInt32(EndYear4.Value) : 0;
        item.StartMonth = Convert.ToInt32(StartMonth4.SelectedValue);
        item.EndMonth = Convert.ToInt32(EndMonth4.SelectedValue);

        AnnualBudget bll = new AnnualBudget();
        IList list = bll.Statistics1(item);
        StaticsBudgetDetail.DataSource = list;
        StaticsBudgetDetail.DataBind();

        CompanyInfo companyinfo = new CompanyInfo();
        Company companybll = new Company();

        IList totallist = new List<BudgetDetailInfo>();
        BudgetDetailInfo total = new BudgetDetailInfo();
        total.ExpenditureName = "合计";
        total.Totallist = (List<CompanyInfo>)companybll.Search(companyinfo);

        foreach (BudgetDetailInfo subitem in list)
        {
            int i = 0;
            foreach (CompanyInfo totalitem in subitem.Totallist)
            {
                ((CompanyInfo)total.Totallist[i]).CompanyExpenditure += totalitem.CompanyExpenditure;
                i++;
            }
            total.RealExpenditure += subitem.RealExpenditure;
        }
        totallist.Add(total);

        TotalRepeater.DataSource = totallist;
        TotalRepeater.DataBind();

        HeadRepeater.DataSource = totallist;
        HeadRepeater.DataBind();

        ViewState["totallist"] = totallist;
        ViewState["BudgetDetaillist"] = list;

        if (ViewState["dataset"] != null)
        {
            reportdocument.Load(Server.MapPath("~") + "/report/BudgetReport/CrystalReport.rpt");
            reportdocument.SetDataSource(ViewState["dataset"]);
            CrystalReportViewer1.ReportSource = reportdocument;
            CrystalReportViewer1.DataBind();
        }
        if (ViewState["dataset2"] != null)
        {
            reportdocument2.Load(Server.MapPath("~") + "/report/BudgetReport/CrystalReport2.rpt");
            reportdocument2.SetDataSource(ViewState["dataset2"]);
            CrystalReportViewer2.ReportSource = reportdocument2;
            CrystalReportViewer2.DataBind();
        }

    }

    protected void Sure_Click5(object sender, EventArgs e)
    {
        IList list = new List<BudgetDetailInfo>();
        int yearparam = Year5.Value != "" ? Convert.ToInt32(Year5.Value) : 0;
        int monthparam = Convert.ToInt32(Month5.SelectedValue);
        string title = Title5.Value;
        AnnualBudget bll = new AnnualBudget();
        BudgetYearInfo yearinfo = new BudgetYearInfo();
        yearinfo.Title = title;
        yearinfo.Year = yearparam;
        List<BudgetYearInfo> sublist = (List<BudgetYearInfo>)bll.Search(yearinfo);
        List<int> al = new List<int>();
        if (sublist != null && sublist.Count != 0)
            AddTree(al, 0, -1, 0, list, sublist[0].BudgetYearID, yearparam, monthparam, title);
        Company companybll = new Company();
        IList CompanyList = (List<CompanyInfo>)companybll.GetAllCompany();
        CompanyInfo companyinfo = new CompanyInfo();
        companyinfo.CompanyName = "四家公司汇总";
        companyinfo.CompanyID = "al";
        CompanyList.Insert(0, companyinfo);

        for (int i = list.Count - 1; i >= 0; i--)
        {
            decimal budgetyear = 0;
            decimal havepaid = 0;
            for (int j = ((BudgetDetailInfo)list[i]).CompanyList.Count - 1; j >= 0; j--)
            {
                if (j == 0)
                {
                    ((CompanyInfo)((BudgetDetailInfo)list[i]).CompanyList[j]).BudgetYear = budgetyear;
                    ((CompanyInfo)((BudgetDetailInfo)list[i]).CompanyList[j]).HavePaid = havepaid;
                    ((CompanyInfo)((BudgetDetailInfo)list[i]).CompanyList[j]).LeftMoney = budgetyear - havepaid;
                    if (((BudgetDetailInfo)list[i]).StartYear != -1)
                    {
                        ((CompanyInfo)((BudgetDetailInfo)list[((BudgetDetailInfo)list[i]).StartYear]).CompanyList[j]).BudgetYear += budgetyear;
                        ((CompanyInfo)((BudgetDetailInfo)list[((BudgetDetailInfo)list[i]).StartYear]).CompanyList[j]).HavePaid += havepaid;
                    }
                    else
                    {
                        ((CompanyInfo)CompanyList[j]).BudgetYear += budgetyear;
                        ((CompanyInfo)CompanyList[j]).HavePaid += havepaid;
                        ((CompanyInfo)CompanyList[j]).LeftMoney += budgetyear - havepaid;
                    }
                }
                else
                {
                    ((CompanyInfo)((BudgetDetailInfo)list[i]).CompanyList[j]).LeftMoney = ((CompanyInfo)((BudgetDetailInfo)list[i]).CompanyList[j]).BudgetYear - ((CompanyInfo)((BudgetDetailInfo)list[i]).CompanyList[j]).HavePaid;
                    if (((BudgetDetailInfo)list[i]).StartYear != -1)
                    {
                        ((CompanyInfo)((BudgetDetailInfo)list[((BudgetDetailInfo)list[i]).StartYear]).CompanyList[j]).BudgetYear += ((CompanyInfo)((BudgetDetailInfo)list[i]).CompanyList[j]).BudgetYear;
                        ((CompanyInfo)((BudgetDetailInfo)list[((BudgetDetailInfo)list[i]).StartYear]).CompanyList[j]).HavePaid += ((CompanyInfo)((BudgetDetailInfo)list[i]).CompanyList[j]).HavePaid;
                    }
                    else
                    {
                        ((CompanyInfo)CompanyList[j]).BudgetYear += ((CompanyInfo)((BudgetDetailInfo)list[i]).CompanyList[j]).BudgetYear;
                        ((CompanyInfo)CompanyList[j]).HavePaid += ((CompanyInfo)((BudgetDetailInfo)list[i]).CompanyList[j]).HavePaid;
                        ((CompanyInfo)CompanyList[j]).LeftMoney += ((CompanyInfo)((BudgetDetailInfo)list[i]).CompanyList[j]).BudgetYear - ((CompanyInfo)((BudgetDetailInfo)list[i]).CompanyList[j]).HavePaid;
                    }
                    budgetyear += ((CompanyInfo)((BudgetDetailInfo)list[i]).CompanyList[j]).BudgetYear;
                    havepaid += ((CompanyInfo)((BudgetDetailInfo)list[i]).CompanyList[j]).HavePaid;
                }
            }
        }
        BudgetDetailInfo detailitem = new BudgetDetailInfo();
        detailitem.SubName = "合计";
        detailitem.CompanyList = CompanyList;
        list.Add(detailitem);

        Repeater1.DataSource = list;
        Repeater1.DataBind();

        IList firstheadlist = new List<BudgetDetailInfo>();
        IList Secondheadlist = new List<BudgetDetailInfo>();

        BudgetDetailInfo firstheaditem = new BudgetDetailInfo();
        firstheaditem.SubName = "";
        firstheaditem.CompanyList = CompanyList;
        firstheadlist.Add(firstheaditem);

        BudgetDetailInfo secondheaditem = new BudgetDetailInfo();
        secondheaditem.SubName = "费用项目";
        secondheaditem.CompanyList = CompanyList;
        Secondheadlist.Add(secondheaditem);

        FirstHeadRepeater.DataSource = firstheadlist;
        FirstHeadRepeater.DataBind();

        SecondHeadRepeater.DataSource = Secondheadlist;
        SecondHeadRepeater.DataBind();


        if (ViewState["dataset"] != null)
        {
            reportdocument.Load(Server.MapPath("~") + "/report/BudgetReport/CrystalReport.rpt");
            reportdocument.SetDataSource(ViewState["dataset"]);
            CrystalReportViewer1.ReportSource = reportdocument;
            CrystalReportViewer1.DataBind();
        }
        if (ViewState["dataset2"] != null)
        {
            reportdocument2.Load(Server.MapPath("~") + "/report/BudgetReport/CrystalReport2.rpt");
            reportdocument2.SetDataSource(ViewState["dataset2"]);
            CrystalReportViewer2.ReportSource = reportdocument2;
            CrystalReportViewer2.DataBind();
        }
    }

    protected void Sure_Click6(object sender, EventArgs e)
    {
        TreeView1.Nodes.Clear();
        Company companybll = new Company();
        CompanyInfo companyinfo = new CompanyInfo();
        //companyinfo.IsParentCompany = false;
        IList companylist = (List<CompanyInfo>)companybll.Search(companyinfo);
        companycount.Value = companylist.Count.ToString();

        //if (ViewState["YearID"] == null)
        //{
            AnnualBudget annualbll = new AnnualBudget();
            BudgetYearInfo item = new BudgetYearInfo();
            item.Year = Convert.ToInt32(this.Year6.Value);
            List<BudgetYearInfo> budgetyearinfolist = (List<BudgetYearInfo>)annualbll.Search(item);
            if (budgetyearinfolist != null && budgetyearinfolist.Count > 0)
            {
                BudgetYearInfo budgetyearinfo = budgetyearinfolist[0];//一年一个预算
                ViewState["YearID"] = budgetyearinfo.BudgetYearID;
            }
            else
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "改年年度预算还没制作", new WebException("改年年度预算还没制作"), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        //}

        inittreeyet = false;
        AddTree(0, (TreeNode)null);
        TreeView1.ShowLines = true;
        //BudgetApplTotalydiv.InnerHtml = "";
        TotalExpenditurediv.InnerHtml = "";
        //NonPaymentdiv.InnerHtml = "";
        //BudgetPermonthdiv.InnerHtml = "";
        //Totaldiv.InnerHtml = "";
        //SurplusExpenditurediv.InnerHtml = "";
        PercentDiv.InnerHtml = "";
        GetMonthlySessionFromDatabase();
        staticsfunction(TreeView1.Nodes);
        staticstotal();
        ComputePercent(TreeView1.Nodes);
        Session["TotalPercent"] = 100;
        AddTextBox(TreeView1.Nodes);
        inittreeyet = true;

        showtable.Visible = true;

    }



    private void AddTextBox(TreeNodeCollection nodes)
    {
        foreach (TreeNode nodetemp in nodes)
        {
            //string BudgetApply = "";
            string TotalExpenditure = "";
            String Percent = "";
            //string NonPayment = "";
            //string BudgetPermonth = "";
            //string Total = "";
            //string SurplusExpenditure = "";
            if (nodetemp.Parent != null)
            {
                string showornot = (nodetemp.Parent.Expanded == true) ? "block" : "none";
                if (showornot == "none")
                    nodetemp.Expanded = false;
                //if (Session[nodetemp.Value + "BudgetApply"] != null)
                //    BudgetApply = Session[nodetemp.Value + "BudgetApply"].ToString();
                if (Session[nodetemp.Value + "TotalExpenditure"] != null)
                    TotalExpenditure = Session[nodetemp.Value + "TotalExpenditure"].ToString();
                if (Session[nodetemp.Value + "Percent"] != null)
                    Percent = Session[nodetemp.Value + "Percent"].ToString();

                //if (Session[nodetemp.Value + "NonPayment"] != null)
                //    NonPayment = Session[nodetemp.Value + "NonPayment"].ToString();
                //if (Session[nodetemp.Value + "BudgetPermonth"] != null)
                //    BudgetPermonth = Session[nodetemp.Value + "BudgetPermonth"].ToString();
                //if (Session[nodetemp.Value + "Total"] != null)
                //    Total = Session[nodetemp.Value + "Total"].ToString();
                //if (Session[nodetemp.Value + "SurplusExpenditure"] != null)
                //    SurplusExpenditure = Session[nodetemp.Value + "SurplusExpenditure"].ToString();
                if (nodetemp.ChildNodes.Count > 0)
                {
                    //BudgetApplTotalydiv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  size='14' id='" + nodetemp.Value + "BudgetApply' style='display:" + showornot + "' runat='server' value='" + BudgetApply + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    TotalExpenditurediv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  size='14' id='" + nodetemp.Value + "TotalExpenditure' style='display:" + showornot + "' runat='server' value='" + TotalExpenditure + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    PercentDiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "Percent' style='display:" + showornot + "' runat='server' value='" + Percent + "%' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    //BudgetPermonthdiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "BudgetPermonth' style='display:" + showornot + "' runat='server' value='" + BudgetPermonth + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    //Totaldiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "Total' style='display:" + showornot + "' runat='server' value='" + Total + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    //SurplusExpenditurediv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  size='14' id='" + nodetemp.Value + "SurplusExpenditure' style='display:" + showornot + "' runat='server' value='" + SurplusExpenditure + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                }
                else
                {
                    //BudgetApplTotalydiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "BudgetApply' style='display:" + showornot + "' runat='server' value='" + BudgetApply + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    TotalExpenditurediv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "TotalExpenditure' style='display:" + showornot + "' runat='server' value='" + TotalExpenditure + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    PercentDiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "Percent' style='display:" + showornot + "' runat='server' value='" + Percent + "%' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    //BudgetPermonthdiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "BudgetPermonth' style='display:" + showornot + "' runat='server' value='" + BudgetPermonth + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    //Totaldiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "Total' style='display:" + showornot + "' runat='server' value='" + Total + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    //SurplusExpenditurediv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  size='14' id='" + nodetemp.Value + "SurplusExpenditure' style='display:" + showornot + "' runat='server' value='" + SurplusExpenditure + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                }
            }
            else
            {
                //if (Session[nodetemp.Value + "BudgetApply"] != null)
                //    BudgetApply = Session[nodetemp.Value + "BudgetApply"].ToString();
                if (Session[nodetemp.Value + "TotalExpenditure"] != null)
                    TotalExpenditure = Session[nodetemp.Value + "TotalExpenditure"].ToString();
                if (Session[nodetemp.Value + "Percent"] != null)
                    Percent = Session[nodetemp.Value + "Percent"].ToString();
                //if (Session[nodetemp.Value + "NonPayment"] != null)
                //    NonPayment = Session[nodetemp.Value + "NonPayment"].ToString();
                //if (Session[nodetemp.Value + "BudgetPermonth"] != null)
                //    BudgetPermonth = Session[nodetemp.Value + "BudgetPermonth"].ToString();
                //if (Session[nodetemp.Value + "Total"] != null)
                //    Total = Session[nodetemp.Value + "Total"].ToString();
                //if (Session[nodetemp.Value + "SurplusExpenditure"] != null)
                //    SurplusExpenditure = Session[nodetemp.Value + "SurplusExpenditure"].ToString();
                if (nodetemp.ChildNodes.Count > 0)
                {
                    //BudgetApplTotalydiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "BudgetApply' style='display:block' runat='server' value='" + BudgetApply + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    TotalExpenditurediv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "TotalExpenditure' style='display:block' runat='server' value='" + TotalExpenditure + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    PercentDiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "Percent' style='display:block' runat='server' value='" + Percent + "%'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    //BudgetPermonthdiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "BudgetPermonth' style='display:block' runat='server' value='" + BudgetPermonth + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    //Totaldiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "Total' style='display:block' runat='server' value='" + Total + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    //SurplusExpenditurediv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  size='14' id='" + nodetemp.Value + "SurplusExpenditure' style='display:block' runat='server' value='" + SurplusExpenditure + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                }
                else
                {
                    //BudgetApplTotalydiv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  size='14' id='" + nodetemp.Value + "BudgetApply' style='display:block' runat='server' value='" + BudgetApply + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    TotalExpenditurediv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  size='14' id='" + nodetemp.Value + "TotalExpenditure' style='display:block' runat='server' value='" + TotalExpenditure + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    PercentDiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "Percent' style='display:block' runat='server' value='" + Percent + "%'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    //BudgetPermonthdiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "BudgetPermonth' style='display:block' runat='server' value='" + BudgetPermonth + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    //Totaldiv.InnerHtml += "<input type='text' size='14'  title='" + nodetemp.Text + "' id='" + nodetemp.Value + "Total' style='display:block' runat='server' value='" + Total + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    //SurplusExpenditurediv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  size='14' id='" + nodetemp.Value + "SurplusExpenditure' style='display:block' runat='server' value='" + SurplusExpenditure + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                }
            }
            AddTextBox(nodetemp.ChildNodes);
        }

    }

    private void staticstotal()
    {
        //decimal BudgetApply = 0;
        decimal TotalExpenditure = 0;
        //decimal NonPayment = 0;
        //decimal BudgetPermonth = 0;
        //decimal Total = 0;
        //decimal SurplusExpenditure = 0;
        foreach (TreeNode nodetemp in TreeView1.Nodes)
        {
            //if (Session[nodetemp.Value + "BudgetApply"] != null)
            //{
            //    BudgetApply += Convert.ToDecimal(Session[nodetemp.Value + "BudgetApply"]);
            //}
            if (Session[nodetemp.Value + "TotalExpenditure"] != null)
            {
                TotalExpenditure += Convert.ToDecimal(Session[nodetemp.Value + "TotalExpenditure"]);
            }
            //if (Session[nodetemp.Value + "NonPayment"] != null)
            //{
            //    NonPayment += Convert.ToDecimal(Session[nodetemp.Value + "NonPayment"]);
            //}
            //if (Session[nodetemp.Value + "BudgetPermonth"] != null)
            //{
            //    BudgetPermonth += Convert.ToDecimal(Session[nodetemp.Value + "BudgetPermonth"]);
            //}
            //if (Session[nodetemp.Value + "Total"] != null)
            //{
            //    Total += Convert.ToDecimal(Session[nodetemp.Value + "Total"]);
            //}
            //if (Session[nodetemp.Value + "SurplusExpenditure"] != null)
            //{
            //    SurplusExpenditure += Convert.ToDecimal(Session[nodetemp.Value + "SurplusExpenditure"]);
            //}
        }
        //Session["TotalBudgetApply"] = BudgetApply;
        Session["TotalTotalExpenditure"] = TotalExpenditure;
        //Session["TotalNonPayment"] = NonPayment;
        //Session["TotalBudgetPermonth"] = BudgetPermonth;
        //Session["TotalTotal"] = Total;
        //Session["TotalSurplusExpenditure"] = SurplusExpenditure;
    }


    /// <summary>
    /// 用于统计的函数
    /// </summary>
    /// <param name="nodes">树节点</param>
    private void staticsfunction(TreeNodeCollection nodes)
    {
        foreach (TreeNode nodetemp in nodes)
        {
            staticsfunction(nodetemp.ChildNodes);
            if (nodetemp.ChildNodes.Count > 0)
            {
                decimal TotalExpenditure = 0;
                foreach (TreeNode subnodetemp in nodetemp.ChildNodes)
                {

                    if (Session[subnodetemp.Value + "TotalExpenditure"] != null)
                    {
                        TotalExpenditure += Convert.ToDecimal(Session[subnodetemp.Value + "TotalExpenditure"]);
                    }

                }
                Session[nodetemp.Value + "TotalExpenditure"] = TotalExpenditure;


            }
            else
            {
                decimal TotalExpenditure = 0;

                if (Session[nodetemp.Value + "TotalExpenditure"] != null)
                {
                    TotalExpenditure = Convert.ToDecimal(Session[nodetemp.Value + "TotalExpenditure"]);
                }
                else
                    Session[nodetemp.Value + "TotalExpenditure"] = decimal.Zero;

            }
        }
    }

    private void ComputePercent(TreeNodeCollection nodes)
    {
        foreach (TreeNode nodetemp in nodes)
        {
            ComputePercent(nodetemp.ChildNodes);
            decimal percent = 0;
            if (Session[nodetemp.Value + "TotalExpenditure"] != null)
            {
                percent = Convert.ToDecimal(Session[nodetemp.Value + "TotalExpenditure"]) / Convert.ToDecimal(Session["TotalTotalExpenditure"]);
                percent = Math.Round(percent * 100, 2);
                Session[nodetemp.Value + "Percent"] = percent;
            }
            else
                Session[nodetemp.Value + "Percent"] = decimal.Zero;
        }
    }

    /// <summary>
    /// 从数据库中获取预算金额到Session中
    /// </summary>
    private void GetMonthlySessionFromDatabase()
    {
        AnnualBudget bll = new AnnualBudget();
        BudgetPerMonthTotalInfo budgetpermonthtotalinfo = new BudgetPerMonthTotalInfo();
        budgetpermonthtotalinfo.BudgetYearID = Convert.ToInt64(ViewState["YearID"]);

        List<BudgetPerMonthTotalInfo> totallist = (List<BudgetPerMonthTotalInfo>)bll.Search(budgetpermonthtotalinfo);
        BudgetPerMonthTotalInfo lastmonth = new BudgetPerMonthTotalInfo();

        if (totallist != null && totallist.Count > 0)
            lastmonth = totallist[0];
        else
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "改年月度预算还没制作", new WebException("改年月度预算还没制作"), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");

        Year.Value = lastmonth.Year.ToString();
        //Month.Value = lastmonth.Month.ToString();
        INPTitle.Value = lastmonth.Title;

        BudgetPermonthInfo budgetpermonthinfo = new BudgetPermonthInfo();
        ViewState["id"] = lastmonth.TotalID;
        budgetpermonthinfo.TotalID = lastmonth.TotalID;

        IList<BudgetPermonthInfo> list = bll.Search(budgetpermonthinfo);

        foreach (BudgetPermonthInfo item in list)
        {
            //Session[item.SubID.ToString() + "BudgetApply"] = item.BudgetApply;
            Session[item.SubID.ToString() + "TotalExpenditure"] = item.TotalExpenditure;
            //Session[item.SubID.ToString() + "NonPayment"] = item.NonPayment;
            //Session[item.SubID.ToString() + "BudgetPermonth"] = item.BudgetPermonth;
            //Session[item.SubID.ToString() + "Total"] = item.Total;
            //Session[item.SubID.ToString() + "SurplusExpenditure"] = item.SurplusExpenditure;

        }

        try
        {

            BudgetDetailInfo budgetdetailinfo = new BudgetDetailInfo();
            budgetdetailinfo.TotalID = Convert.ToInt32(ViewState["id"]);
            IList detaillist = (List<BudgetDetailInfo>)bll.Search(budgetdetailinfo);



            foreach (BudgetDetailInfo detailinfoitem in detaillist)
            {
                decimal temp;
                if (Session[detailinfoitem.SubID.ToString() + "TotalExpenditure"] != null)
                    temp = Convert.ToDecimal(Session[detailinfoitem.SubID.ToString() + "TotalExpenditure"]);
                else
                    temp = 0;
                temp += detailinfoitem.RealExpenditure;
                Session[detailinfoitem.SubID.ToString() + "TotalExpenditure"] = temp;
            }
            //GridView2.DataSource = convertfromreallist(detaillist);
            //GridView2.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取开支明细列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    //protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    GridViewRow gvRow = GridView2.Rows[Convert.ToInt32(e.CommandArgument)];
    //    if (e.CommandName == "view")
    //    {
    //        string DetailID = gvRow.Attributes["DetailID"];
    //        Response.Redirect("ViewDetailBudget.aspx?cmd=view&id=" + DetailID);
    //    }
    //    //if (e.CommandName == "del")
    //    //{
    //    //    try
    //    //    {
    //    //        long id = Convert.ToInt64(gvRow.Attributes["DetailID"]);
    //    //        AnnualBudget bll = new AnnualBudget();
    //    //        bll.DelBudgetDetail(id);
    //    //        FillData();
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
    //    //    }
    //    //}

    //}

    //protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        //鼠标移动到每项时颜色交替效果    
    //        e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
    //        e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

    //        //设置悬浮鼠标指针形状为"小手"    
    //        e.Row.Attributes["style"] = "Cursor:hand";
    //        BudgetDetailInfo dv = (BudgetDetailInfo)e.Row.DataItem;
    //        e.Row.Attributes["DetailID"] = dv.DetailID.ToString();
    //    }

    //}

    public void TreeView1_OnSelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            //IList list = new List<BudgetDetailInfo>();
            //GetBudgetDetailByNodeSelect(TreeView1.SelectedNode, list);
            //GridView2.DataSource = convertfromreallist(list);
            //GridView2.DataBind();
            string str = TreeView1.SelectedNode.ToolTip;
            TreeView1.SelectedNode.Selected = false;
            ScriptManager.RegisterClientScriptBlock(TreeView1, this.GetType(), "click", "setslectrowcolor('" + str + "');", true);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取开支明细列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

    }

    ///// <summary>
    ///// 获取单击树时所对应节点的所有开支明细
    ///// </summary>
    //private void GetBudgetDetailByNodeSelect(TreeNode node, IList list)
    //{
    //    if (node.ChildNodes.Count > 0)
    //    {
    //        foreach (TreeNode subnode in node.ChildNodes)
    //        {
    //            GetBudgetDetailByNodeSelect(subnode, list);
    //        }
    //    }
    //    else
    //    {
    //        AnnualBudget bll = new AnnualBudget();
    //        BudgetDetailInfo budgetdetailinfo = new BudgetDetailInfo();
    //        budgetdetailinfo.Year = Convert.ToInt32(Year.Value);
    //        budgetdetailinfo.SubID = Convert.ToInt64(node.Value);
    //        IList listtemp = (List<BudgetDetailInfo>)bll.Search(budgetdetailinfo);
    //        foreach (BudgetDetailInfo item in listtemp)
    //        {
    //            list.Add(item);
    //        }
    //    }
    //}

    /// <summary>
    /// 树展开事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void TreeView1_OnTreeNodeCollapsed(object sender, TreeNodeEventArgs e)
    {
        //AddSession();
        if (inittreeyet)
        {
            //BudgetApplTotalydiv.InnerHtml = "";
            TotalExpenditurediv.InnerHtml = "";
            //NonPaymentdiv.InnerHtml = "";
            //BudgetPermonthdiv.InnerHtml = "";
            //Totaldiv.InnerHtml = "";
            //SurplusExpenditurediv.InnerHtml = "";
            PercentDiv.InnerHtml = "";
            AddTextBox(TreeView1.Nodes);
        }
    }

    public void TreeView1_OnTreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {
        //AddSession();
        //BudgetApplTotalydiv.InnerHtml = "";
        TotalExpenditurediv.InnerHtml = "";
        //NonPaymentdiv.InnerHtml = "";
        //BudgetPermonthdiv.InnerHtml = "";
        //Totaldiv.InnerHtml = "";
        //SurplusExpenditurediv.InnerHtml = "";
        PercentDiv.InnerHtml = "";
        AddTextBox(TreeView1.Nodes);
    }

    //private IList convertfromreallist(IList list)
    //{
    //    IList listtemp = new List<BudgetDetailInfo>();
    //    int count = 0;
    //    decimal expenditure = 0;
    //    decimal budgetapproval = 0;
    //    int comcount = Convert.ToInt32(companycount.Value);
    //    foreach (BudgetDetailInfo budgetdetailinfo in (List<BudgetDetailInfo>)list)
    //    {
    //        if (count % comcount == 0)
    //        {
    //            expenditure = budgetdetailinfo.Expenditure;
    //            budgetapproval = budgetdetailinfo.BudgetApprove;
    //            listtemp.Add(budgetdetailinfo);
    //            ((BudgetDetailInfo)listtemp[count / comcount]).ExpenditureStr = budgetdetailinfo.CompanyName + ":" + budgetdetailinfo.Expenditure + " ";
    //            ((BudgetDetailInfo)listtemp[count / comcount]).BudgetApproveStr = budgetdetailinfo.CompanyName + ":" + budgetdetailinfo.BudgetApprove + " ";

    //        }
    //        else
    //        {
    //            expenditure += budgetdetailinfo.Expenditure;
    //            budgetapproval += budgetdetailinfo.BudgetApprove;
    //            ((BudgetDetailInfo)listtemp[count / comcount]).ExpenditureStr += budgetdetailinfo.CompanyName + ":" + budgetdetailinfo.Expenditure + " ";
    //            ((BudgetDetailInfo)listtemp[count / comcount]).BudgetApproveStr += budgetdetailinfo.CompanyName + ":" + budgetdetailinfo.BudgetApprove + " ";
    //            if (count % comcount == 3)
    //            {
    //                ((BudgetDetailInfo)listtemp[count / comcount]).ExpenditureStr += "小计" + ":" + expenditure;
    //                ((BudgetDetailInfo)listtemp[count / comcount]).BudgetApproveStr += "小计" + ":" + budgetapproval;
    //            }

    //        }
    //        count++;
    //    }
    //    return listtemp;
    //}

    //private void FillBudgetDetail()
    //{
    //    try
    //    {
    //        AnnualBudget bll = new AnnualBudget();
    //        BudgetDetailInfo budgetdetailinfo = new BudgetDetailInfo();
    //        budgetdetailinfo.Year = Convert.ToInt32(this.Year.Value);
    //        IList list = (List<BudgetDetailInfo>)bll.Search(budgetdetailinfo);
    //        GridView2.DataSource = convertfromreallist(list);
    //        GridView2.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取开支明细列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
    //    }
    //}

    //protected void showallexpenditure_click(object sender, EventArgs e)
    //{
    //    FillBudgetDetail();
    //}


    public void AddTree(long ParentID, TreeNode pNode)
    {
        SubjectPerYear subjectperyearinfo = new SubjectPerYear();
        subjectperyearinfo.ParentID = ParentID;
        //AnnualBudget annualbll = new AnnualBudget();
        subjectperyearinfo.Year = Convert.ToInt64(ViewState["YearID"]);
        SubjectRelation bll = new SubjectRelation();
        QueryParam qp = bll.GenerateSearchTermByYear(subjectperyearinfo);
        int recordcount = 0;
        IList nodelist = bll.GetListByYear(qp, out recordcount, companyid);
        List<SubjectRelationInfos> subnodes = new List<SubjectRelationInfos>();
        foreach (SubjectRelationInfos node in nodelist)
        {
            if (node.ParentID == ParentID)
                subnodes.Add(node);
        }

        //循环递归
        foreach (SubjectRelationInfos node in subnodes)
        {
            //声明节点
            TreeNode Node = new TreeNode();
            //绑定超级链接
            //Node.NavigateUrl = "ViewSubjectRelation.aspx?cmd=view&id=" + node.SubID;
            if (pNode == null)
            {
                Node.Text = node.Name;
                Node.Value = node.SubID.ToString();
                TreeView1.Nodes.Add(Node);
                Node.Expanded = true;
                Node.ToolTip = node.Name;

                //HtmlInputText inputtext = new HtmlInputText();
                //TextBox tb = new TextBox();
                //tb.ID = "int" + Node.Value;
                //inputtext.Attributes["style"] = "display:block";
                //inputrow.Controls.Add(tb);
                //inputrow.InnerHtml += "<input type='text' id='int" + Node.Value + "' style='display:block' runat='server' enableviewstate='true' />";
                AddTree(node.SubID, Node);

            }
            else
            {
                Node.Text = node.Name;
                Node.Value = node.SubID.ToString();
                pNode.ChildNodes.Add(Node);
                Node.Expanded = false;
                Node.ToolTip = node.Name;

                //HtmlInputText inputtext = new HtmlInputText();
                //inputtext.ID = "int" + Node.Value;
                //inputtext.Attributes["style"] = "display:none";
                //inputrow.Controls.Add(inputtext);
                //inputrow.InnerHtml += "<input type='text' id='int" + Node.Value + "' style='display:none' runat='server' enableviewstate='true' />";
                AddTree(node.SubID, Node);
            }
        }
    }

    private void AddTree(IList al, long ParentID, int index, int Level, IList list, long YearID, int Year, int Month, string Title)
    {
        SubjectPerYear subjectperyearinfo = new SubjectPerYear();
        subjectperyearinfo.ParentID = ParentID;
        //AnnualBudget annualbll = new AnnualBudget();
        subjectperyearinfo.Year = YearID;
        SubjectRelation bll = new SubjectRelation();
        Company companybll = new Company();
        AnnualBudget annualbudgetbll = new AnnualBudget();
        QueryParam qp = bll.GenerateSearchTermByYear(subjectperyearinfo);
        int recordcount = 0;
        IList nodelist = bll.GetListByYear(qp, out recordcount, companyid);
        IList subnodes = new List<SubjectRelationInfos>();
        foreach (SubjectRelationInfos node in nodelist)
        {
            if (node.ParentID == ParentID)
                subnodes.Add(node);
        }

        foreach (SubjectRelationInfos node in subnodes)
        {
            BudgetDetailInfo item = new BudgetDetailInfo();
            item.SubID = node.SubID;
            item.SubName = node.Name;
            item.CompanyList = (List<CompanyInfo>)companybll.GetAllCompany();

            if (node.IsLeaf == 1)
            {
                foreach (CompanyInfo companyitem in item.CompanyList)
                {
                    BudgetYearDetailInfo budgetyeardetailinfo = new BudgetYearDetailInfo();
                    budgetyeardetailinfo.CompanyID = companyitem.CompanyID;
                    budgetyeardetailinfo.SubID = node.SubID;
                    budgetyeardetailinfo.BudgetYearID = YearID;
                    IList budgetyearlist = (List<BudgetYearDetailInfo>)annualbudgetbll.Search(budgetyeardetailinfo);
                    if (budgetyearlist != null && budgetyearlist.Count != 0)
                        companyitem.BudgetYear = ((BudgetYearDetailInfo)budgetyearlist[0]).BudgetApprove;

                    BudgetDetailInfo budgetdetailinfo = new BudgetDetailInfo();
                    budgetdetailinfo.Year = Year;
                    budgetdetailinfo.Month = Month;
                    budgetdetailinfo.Title = Title;
                    budgetdetailinfo.SubID = node.SubID;
                    budgetdetailinfo.SubName = node.Name;
                    budgetdetailinfo.CompanyID = companyitem.CompanyID;
                    budgetdetailinfo.CompanyName = companyitem.CompanyName;
                    IList budgetdetaillist = annualbudgetbll.Summary1(budgetdetailinfo);
                    if (budgetdetaillist != null && budgetdetaillist.Count != 0)
                        companyitem.HavePaid = ((BudgetDetailInfo)budgetdetaillist[0]).RealExpenditure;

                }
            }

            CompanyInfo companyinfo = new CompanyInfo();
            companyinfo.CompanyName = "四家公司汇总";
            companyinfo.CompanyID = "al";
            item.CompanyList.Insert(0, companyinfo);
            item.StartYear = index;//父节点标记
            item.EndMonth = Level;//节点层次
            //int levelstr = 0;
            //while (levelstr < Level)
            //{
            //    item.SubName = "　" + item.SubName;
            //    levelstr++;
            //}
            if (list.Count == 0)
            {
                al.Add(1);
                item.SubName = al[Level] + "　" + item.SubName;
            }
            else
            {
                if (Level + 1 > al.Count)
                    al.Add(1);
                else
                {
                    if (Level > ((BudgetDetailInfo)list[list.Count - 1]).EndMonth)
                        al[Level] = 1;
                    else
                        al[Level] = Convert.ToInt32(al[Level]) + 1;
                }
                string indexstr = "";
                for (int i = 0; i < Level + 1; i++)
                {
                    if (i == 0)
                        indexstr += al[i];
                    else
                        indexstr += "." + al[i];

                }
                item.SubName = indexstr + "　" + item.SubName;
            }
            list.Add(item);
            AddTree(al, node.SubID, list.Count - 1, Level + 1, list, YearID, Year, Month, Title);

        }


    }

    //导出
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string filepath = Server.MapPath("~/public") + "/" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".csv"; ;
        FileStream fs = File.Create(filepath);
        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);

        if (ViewState["totallist"] != null && ViewState["BudgetDetaillist"] != null)
        {
            List<BudgetDetailInfo> totallist = (List<BudgetDetailInfo>)ViewState["totallist"];

            foreach (BudgetDetailInfo budgetdetailinfo in totallist)
            {
                sw.Write("类别,");
                sw.Write("项目,");
                sw.Write("收款方,");
                sw.Write("经办人,");
                foreach (CompanyInfo companyinfo in budgetdetailinfo.Totallist)
                {
                    sw.Write(companyinfo.CompanyName + ",");
                }
                sw.Write(budgetdetailinfo.ExpenditureName + ",");
                sw.Write("\r\n");
            }

            List<BudgetDetailInfo> list = (List<BudgetDetailInfo>)ViewState["BudgetDetaillist"];

            int i = 1;
            foreach (BudgetDetailInfo info2 in list)
            {
                sw.Write(i + "、" + info2.SubName + ",\r\n");
                int i2 = 1;
                foreach (BudgetDetailInfo info3 in info2.BudgetDetailList)
                {
                    if (info3.RealExpenditure != 0)
                    {
                        sw.Write(",");
                        sw.Write(i2 + "、" + info3.ExpenditureName + ",");
                        sw.Write(info3.Supplier + ",");
                        sw.Write(info3.Manager + ",");
                        foreach (BudgetDetailInfo info4 in info3.CompanyList)
                        {
                            sw.Write(info4.RealExpenditure + ",");
                        }
                        sw.Write(info3.RealExpenditure + ",\r\n");
                        i2++;
                    }
                }
                if (info2.RealExpenditure != 0)
                {
                    sw.Write("小计,");
                    sw.Write(",");
                    sw.Write(",");
                    sw.Write(",");
                    foreach (CompanyInfo companyinfo in info2.Totallist)
                    {
                        sw.Write(companyinfo.CompanyExpenditure + ",");
                    }
                    sw.Write(info2.RealExpenditure + ",\r\n");
                }
                i++;
            }


            foreach (BudgetDetailInfo budgetdetailinfo in totallist)
            {
                sw.Write("总计,");
                sw.Write(",");
                sw.Write(",");
                sw.Write(",");
                foreach (CompanyInfo companyinfo in budgetdetailinfo.Totallist)
                {
                    sw.Write(companyinfo.CompanyExpenditure + ",");
                }
                sw.Write(budgetdetailinfo.RealExpenditure + ",");
                sw.Write("\r\n");
            }
        }


        sw.Flush();
        sw.Close();
        fs.Close();
        Response.ClearContent();
        Response.ClearHeaders();

        Response.ContentType = "application/vnd.ms-excel";

        Response.AddHeader("Content-Disposition", "inline;filename=" + HttpUtility.UrlEncode("预算详细开支导出.csv"));
        Response.WriteFile(filepath);//FileName为Excel文件所在地址

        Response.Flush();

        Response.Close();
        File.Delete(filepath);
        Response.End();
    }

    private void fillht(NoSortHashTable ht, BudgetDetailInfo item)
    {
        if (ht.Contains(item.SubName))
        {
            NoSortHashTable h2 = (NoSortHashTable)ht[item.SubName];
            fillh2(h2, item);
        }
        else
        {
            NoSortHashTable h2 = new NoSortHashTable();
            fillh2(h2, item);
            ht.Add(item.SubName, h2);
        }

        if (Session[item.SubName] != null)
        {
            Decimal total = Convert.ToDecimal(Session[item.SubName]);
            total += item.RealExpenditure;
            Session[item.SubName] = total;


            Company companybll = new Company();
            List<CompanyInfo> CompanyList = (List<CompanyInfo>)companybll.GetAllCompany();
            foreach (CompanyInfo companyinfo in CompanyList)
            {
                if (companyinfo.CompanyName.Substring(0, 2).Equals(item.CompanyName))
                {
                    Decimal num = Convert.ToDecimal(Session[item.SubName + "," + companyinfo.CompanyName.Substring(0, 2)]);
                    num += item.RealExpenditure;
                    Session[item.SubName + "," + companyinfo.CompanyName.Substring(0, 2)] = num;
                    break;
                }
            }
        }
        else
        {
            Session[item.SubName] = item.RealExpenditure;

            Company companybll = new Company();
            List<CompanyInfo> CompanyList = (List<CompanyInfo>)companybll.GetAllCompany();
            foreach (CompanyInfo companyinfo in CompanyList)
            {
                if (companyinfo.CompanyName.Substring(0, 2).Equals(item.CompanyName))
                {
                    Session[item.SubName + "," + companyinfo.CompanyName.Substring(0, 2)] = item.RealExpenditure;

                }
                else
                {
                    Session[item.SubName + "," + companyinfo.CompanyName.Substring(0, 2)] = 0;
                }
            }
        }

    }

    private void fillh2(NoSortHashTable h2, BudgetDetailInfo item)
    {
        if (h2.Contains(item.SubName+","+item.ExpenditureName))
        {
            NoSortHashTable h3 = (NoSortHashTable)h2[item.SubName + "," + item.ExpenditureName];
            fillh3(h3, item);
        }
        else
        {
            NoSortHashTable h3 = new NoSortHashTable();
            fillh3(h3, item);
            h2.Add(item.SubName + "," + item.ExpenditureName, h3);
        }


        if (Session[item.SubName + "," + item.ExpenditureName] != null)
        {
            Decimal total = Convert.ToDecimal(Session[item.SubName + "," + item.ExpenditureName]);
            total += item.RealExpenditure;
            Session[item.SubName + "," + item.ExpenditureName] = total;


            Company companybll = new Company();
            List<CompanyInfo> CompanyList = (List<CompanyInfo>)companybll.GetAllCompany();
            foreach (CompanyInfo companyinfo in CompanyList)
            {
                if (companyinfo.CompanyName.Substring(0, 2).Equals(item.CompanyName))
                {
                    Decimal num = Convert.ToDecimal(Session[item.SubName + "," + item.ExpenditureName + "," + companyinfo.CompanyName.Substring(0, 2)]);
                    num += item.RealExpenditure;
                    Session[item.SubName + "," + item.ExpenditureName + "," + companyinfo.CompanyName.Substring(0, 2)] = num;
                    break;
                }
            }
        }
        else
        {
            Session[item.SubName + "," + item.ExpenditureName] = item.RealExpenditure;

            Company companybll = new Company();
            List<CompanyInfo> CompanyList = (List<CompanyInfo>)companybll.GetAllCompany();
            foreach (CompanyInfo companyinfo in CompanyList)
            {
                if (companyinfo.CompanyName.Substring(0, 2).Equals(item.CompanyName))
                {
                    Session[item.SubName + "," + item.ExpenditureName + "," + companyinfo.CompanyName.Substring(0, 2)] = item.RealExpenditure;

                }
                else
                {
                    Session[item.SubName + "," + item.ExpenditureName + "," + companyinfo.CompanyName.Substring(0, 2)] = 0;
                }
            }
        }


    }

    private void fillh3(NoSortHashTable h3, BudgetDetailInfo item)
    {
        String key = item.Year + "年" + item.Month + "月";
        if (h3.Contains(key))
        {
            List<BudgetDetailInfo> list = (List<BudgetDetailInfo>)h3[key];

            BudgetDetailInfo olditem = (BudgetDetailInfo)list[0];

            //if (item.Manager != null && !item.Manager.Equals(olditem.Manager))
            //{
            //    olditem.Manager = olditem.Manager +" " + item.Manager;
            //}

            //if (item.Supplier != null && !item.Supplier.Equals(olditem.Supplier))
            //{
            //    olditem.Supplier = olditem.Supplier + " " + item.Supplier;
            //}

            foreach (CompanyInfo companyinfo in olditem.CompanyList)
            {
                if (companyinfo.CompanyName.Substring(0, 2).Equals(item.CompanyName))
                {
                    companyinfo.CompanyExpenditure += item.RealExpenditure;
                    break;
                }
            }
            olditem.BudgetApprove += item.RealExpenditure;
        }
        else
        {

            List<BudgetDetailInfo> list = new List<BudgetDetailInfo>();
            Company companybll = new Company();
            List<CompanyInfo> CompanyList = (List<CompanyInfo>)companybll.GetAllCompany();
            foreach (CompanyInfo companyinfo in CompanyList)
            {
                if (companyinfo.CompanyName.Substring(0, 2).Equals(item.CompanyName))
                {
                    companyinfo.CompanyExpenditure += item.RealExpenditure;
                    break;
                }
            }

            item.CompanyList = CompanyList;
            item.BudgetApprove = item.RealExpenditure;
            list.Add(item);
            h3.Add(key, list);
        }


    }


    protected void Sure_Clickf(object sender, EventArgs e)
    {
        //reporthead.Visible = true;
        AnnualBudget bll = new AnnualBudget();

        BudgetDetailInfo budgetdetailinfo = new BudgetDetailInfo();
        try
        {
            budgetdetailinfo.StartYear = Convert.ToInt32(syearf.Value);
            budgetdetailinfo.StartMonth = Convert.ToInt32(smonthf.SelectedValue);

            budgetdetailinfo.EndYear = Convert.ToInt32(eyearf.Value);
            budgetdetailinfo.EndMonth = Convert.ToInt32(emonthf.SelectedValue);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入数据格式有误", new WebException("输入数据格式有误"), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        List<BudgetDetailInfo> budgetdetaillist = (List<BudgetDetailInfo>)bll.Search(budgetdetailinfo);

        NoSortHashTable ht = new NoSortHashTable();


        foreach (BudgetDetailInfo item in budgetdetaillist)
        {
            item.CompanyName = item.CompanyName.Substring(0, 2);
            item.Manager = item.Manager.Trim();
            item.Supplier = item.Supplier.Trim();
            fillht(ht, item);
        }


        detailreportf.DataSource = ht;
        detailreportf.DataBind();

        Company companybll = new Company();
        List<CompanyInfo> CompanyList = (List<CompanyInfo>)companybll.GetAllCompany();
        foreach (CompanyInfo companyinfo in CompanyList)
        {
            companyinfo.CompanyName = companyinfo.CompanyName.Substring(0, 2);
        }

        RepeaterHead3.DataSource = CompanyList;
        RepeaterHead3.DataBind();

        Session.RemoveAll();

    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        
        DictionaryEntry row = (DictionaryEntry)e.Item.DataItem;//当前行的数据  
        Company companybll = new Company();
        List<CompanyInfo> CompanyList = (List<CompanyInfo>)companybll.GetAllCompany();
        int i = 1;
        foreach (CompanyInfo companyinfo in CompanyList)
        {
            Label lbl = (Label)e.Item.FindControl("subcom"+i++);

            lbl.Text = Session[row.Key + "," + companyinfo.CompanyName.Substring(0, 2)].ToString();
           
        }

        Label total = (Label)e.Item.FindControl("suball");

        total.Text = Session[row.Key.ToString()].ToString();

   
    }

    protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DictionaryEntry row = (DictionaryEntry)e.Item.DataItem;//当前行的数据  
        Company companybll = new Company();
        List<CompanyInfo> CompanyList = (List<CompanyInfo>)companybll.GetAllCompany();
        int i = 1;
        foreach (CompanyInfo companyinfo in CompanyList)
        {
            Label lbl = (Label)e.Item.FindControl("excom" + i++);

            lbl.Text = Session[row.Key + "," + companyinfo.CompanyName.Substring(0, 2)].ToString();

        }

        Label total = (Label)e.Item.FindControl("exall");

        total.Text = Session[row.Key.ToString()].ToString();

        Label name = (Label)e.Item.FindControl("exname");

        name.Text = row.Key.ToString().Substring(row.Key.ToString().IndexOf(","));
    }

}
