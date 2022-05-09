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
using FM2E.BLL.BudgetManagement;
using WebUtility.Components;
using FM2E.Model.BudgetManagement;
using System.Collections.Generic;
using FM2E.Model.Exceptions;
using CrystalDecisions.CrystalReports.Engine;
using FM2E.WorkflowLayer;
using FM2E.Model.Basic;
using FM2E.BLL.Basic;

public partial class Module_FM2E_BudgetManagement_ImplementationOfTheBudget_ImplementationOfTheBudget : System.Web.UI.Page
{
    ReportDocument reportdocument = new ReportDocument();
    ReportDocument reportdocument2 = new ReportDocument();
    string companyid = UserData.CurrentUserData.CompanyID;

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
            QuarterlyBudget bll = new QuarterlyBudget();
            QuarterlyBudgetTotalInfo budgetpermonthtotalinfo = new QuarterlyBudgetTotalInfo();
            //budgetpermonthtotalinfo.Status = Convert.ToInt16(3);
            //budgetpermonthtotalinfo.CompanyID = companyid;

            budgetpermonthtotalinfo.WorkFlowStatus.Add(BudgetMonthlyWorkflow.ApprovalSuccessState);
            QueryParam qp = bll.GenerateSearchTerm(budgetpermonthtotalinfo);
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;
            int recordCount = 0;
            IList list = bll.GetQuarterlyBudgetTotalList(qp, out recordCount, null);
            GridView1.DataSource = list;
            GridView1.DataBind();

            AspNetPager1.RecordCount = recordCount;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取季度预算列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
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
            QuarterlyBudgetTotalInfo dv = (QuarterlyBudgetTotalInfo)e.Row.DataItem;
            e.Row.Attributes["TotalID"] = dv.TotalID.ToString();
        }

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "approval")
        {
            string TotalID = gvRow.Attributes["TotalID"];
            Response.Redirect("MakeImplementationOfTheBudget.aspx?cmd=edit&id=" + TotalID);
        }
        if (e.CommandName == "del")
        {
            try
            {
                long id = Convert.ToInt64(gvRow.Attributes["TotalID"]);
                QuarterlyBudget bll = new QuarterlyBudget();
                bll.DelQuarterlyBudgetTotal(id);
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
        QuarterlyBudget bll = new QuarterlyBudget();
        SubjectRelationInfos subjectrelationinfo = new SubjectRelationInfos();
        subjectrelationinfo.IsLeaf = 1;
        IList subjectrelationinfolist = (List<SubjectRelationInfos>)subjectrelationbll.Search(subjectrelationinfo);
        decimal[] staticslist = new decimal[subjectrelationinfolist.Count];

        int year = Convert.ToInt32(BeginYear.Value);
        int month = Convert.ToInt32(BeginMonth.SelectedValue);

        while ((year < Convert.ToInt32(EndYear.Value)) || (year == Convert.ToInt32(EndYear.Value) && month <= Convert.ToInt32(EndMonth.SelectedValue)))
        {
            for (int i = 0; i < subjectrelationinfolist.Count; i++)
            {
                QuarterlyBudgetDetailInfo searchbudgetdetail = new QuarterlyBudgetDetailInfo();
                searchbudgetdetail.Year = year;
                searchbudgetdetail.Quarter = month;
                searchbudgetdetail.SubID = ((SubjectRelationInfos)subjectrelationinfolist[i]).SubID;

                IList Budgetdetaillist = (List<QuarterlyBudgetDetailInfo>)bll.Search(searchbudgetdetail);
                foreach (QuarterlyBudgetDetailInfo computeitem in Budgetdetaillist)
                {
                    staticslist[i] += computeitem.RealExpenditure;
                }
            }


            if (month == 4)
            {
                year++;
                month = 1;
            }
            else
                month++;
        }
        DataSet dataset = new DataSet();
        DataTable datatable = new DataTable("FM2E_QuarterlyBudgetDetail");
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
        DataTable datatable = new DataTable("FM2E_QuarterlyBudgetDetail");
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

            if (month == 4)
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
        QuarterlyBudget quarterlyBudgetbll = new QuarterlyBudget();
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
            QuarterlyBudgetDetailInfo searchBudgetdetailinfo = new QuarterlyBudgetDetailInfo();
            searchBudgetdetailinfo.Year = year;
            searchBudgetdetailinfo.Quarter = month;
            searchBudgetdetailinfo.SubID = SubID;
            IList detaillist = (List<QuarterlyBudgetDetailInfo>)quarterlyBudgetbll.Search(searchBudgetdetailinfo);
            foreach (QuarterlyBudgetDetailInfo item in detaillist)
            {
                RealBudget += item.Expenditure;
            }

        }
        return RealBudget;

    }

    protected void Sure_Click4(object sender, EventArgs e)
    {
        QuarterlyBudgetDetailInfo item = new QuarterlyBudgetDetailInfo();
        item.Title = Title4.Value;
        item.Supplier = Supplier5.Value;
        item.StartYear = StartYear4.Value != "" ? Convert.ToInt32(StartYear4.Value) : 0;
        item.EndYear = EndYear4.Value != "" ? Convert.ToInt32(EndYear4.Value) : 0;
        item.StartMonth = Convert.ToInt32(StartMonth4.SelectedValue);
        item.EndMonth = Convert.ToInt32(EndMonth4.SelectedValue);

        QuarterlyBudget bll = new QuarterlyBudget();
        IList list = bll.Statistics1(item);
        StaticsBudgetDetail.DataSource = list;
        StaticsBudgetDetail.DataBind();

        CompanyInfo companyinfo = new CompanyInfo();
        Company companybll = new Company();

        IList totallist = new List<QuarterlyBudgetDetailInfo>();
        QuarterlyBudgetDetailInfo total = new QuarterlyBudgetDetailInfo();
        total.ExpenditureName = "合计";
        total.Totallist = (List<CompanyInfo>)companybll.Search(companyinfo);

        foreach (QuarterlyBudgetDetailInfo subitem in list)
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
        IList list = new List<QuarterlyBudgetDetailInfo>();
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
            for (int j = ((QuarterlyBudgetDetailInfo)list[i]).CompanyList.Count - 1; j >= 0; j--)
            {
                if (j == 0)
                {
                    ((CompanyInfo)((QuarterlyBudgetDetailInfo)list[i]).CompanyList[j]).BudgetYear = budgetyear;
                    ((CompanyInfo)((QuarterlyBudgetDetailInfo)list[i]).CompanyList[j]).HavePaid = havepaid;
                    ((CompanyInfo)((QuarterlyBudgetDetailInfo)list[i]).CompanyList[j]).LeftMoney = budgetyear - havepaid;
                    if (((QuarterlyBudgetDetailInfo)list[i]).StartYear != -1)
                    {
                        ((CompanyInfo)((QuarterlyBudgetDetailInfo)list[((QuarterlyBudgetDetailInfo)list[i]).StartYear]).CompanyList[j]).BudgetYear += budgetyear;
                        ((CompanyInfo)((QuarterlyBudgetDetailInfo)list[((QuarterlyBudgetDetailInfo)list[i]).StartYear]).CompanyList[j]).HavePaid += havepaid;
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
                    ((CompanyInfo)((QuarterlyBudgetDetailInfo)list[i]).CompanyList[j]).LeftMoney = ((CompanyInfo)((QuarterlyBudgetDetailInfo)list[i]).CompanyList[j]).BudgetYear - ((CompanyInfo)((QuarterlyBudgetDetailInfo)list[i]).CompanyList[j]).HavePaid;
                    if (((QuarterlyBudgetDetailInfo)list[i]).StartYear != -1)
                    {
                        ((CompanyInfo)((QuarterlyBudgetDetailInfo)list[((QuarterlyBudgetDetailInfo)list[i]).StartYear]).CompanyList[j]).BudgetYear += ((CompanyInfo)((QuarterlyBudgetDetailInfo)list[i]).CompanyList[j]).BudgetYear;
                        ((CompanyInfo)((QuarterlyBudgetDetailInfo)list[((QuarterlyBudgetDetailInfo)list[i]).StartYear]).CompanyList[j]).HavePaid += ((CompanyInfo)((QuarterlyBudgetDetailInfo)list[i]).CompanyList[j]).HavePaid;
                    }
                    else
                    {
                        ((CompanyInfo)CompanyList[j]).BudgetYear += ((CompanyInfo)((QuarterlyBudgetDetailInfo)list[i]).CompanyList[j]).BudgetYear;
                        ((CompanyInfo)CompanyList[j]).HavePaid += ((CompanyInfo)((QuarterlyBudgetDetailInfo)list[i]).CompanyList[j]).HavePaid;
                        ((CompanyInfo)CompanyList[j]).LeftMoney += ((CompanyInfo)((QuarterlyBudgetDetailInfo)list[i]).CompanyList[j]).BudgetYear - ((CompanyInfo)((QuarterlyBudgetDetailInfo)list[i]).CompanyList[j]).HavePaid;
                    }
                    budgetyear += ((CompanyInfo)((QuarterlyBudgetDetailInfo)list[i]).CompanyList[j]).BudgetYear;
                    havepaid += ((CompanyInfo)((QuarterlyBudgetDetailInfo)list[i]).CompanyList[j]).HavePaid;
                }
            }
        }
        QuarterlyBudgetDetailInfo detailitem = new QuarterlyBudgetDetailInfo();
        detailitem.SubName = "合计";
        detailitem.CompanyList = CompanyList;
        list.Add(detailitem);

        Repeater1.DataSource = list;
        Repeater1.DataBind();

        IList firstheadlist = new List<QuarterlyBudgetDetailInfo>();
        IList Secondheadlist = new List<QuarterlyBudgetDetailInfo>();

        QuarterlyBudgetDetailInfo firstheaditem = new QuarterlyBudgetDetailInfo();
        firstheaditem.SubName = "";
        firstheaditem.CompanyList = CompanyList;
        firstheadlist.Add(firstheaditem);

        QuarterlyBudgetDetailInfo secondheaditem = new QuarterlyBudgetDetailInfo();
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

    private void AddTree(IList al, long ParentID, int index, int Level, IList list, long YearID, int Year, int Month, string Title)
    {
        SubjectPerYear subjectperyearinfo = new SubjectPerYear();
        subjectperyearinfo.ParentID = ParentID;
        //AnnualBudget annualbll = new AnnualBudget();
        subjectperyearinfo.Year = YearID;
        SubjectRelation bll = new SubjectRelation();
        Company companybll = new Company();
        AnnualBudget annualbudgetbll = new AnnualBudget();
        QuarterlyBudget quarterlyBudgetbll = new QuarterlyBudget();
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
            QuarterlyBudgetDetailInfo item = new QuarterlyBudgetDetailInfo();
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

                    QuarterlyBudgetDetailInfo budgetdetailinfo = new QuarterlyBudgetDetailInfo();
                    budgetdetailinfo.Year = Year;
                    budgetdetailinfo.Quarter = Month;
                    budgetdetailinfo.Title = Title;
                    budgetdetailinfo.SubID = node.SubID;
                    budgetdetailinfo.SubName = node.Name;
                    budgetdetailinfo.CompanyID = companyitem.CompanyID;
                    budgetdetailinfo.CompanyName = companyitem.CompanyName;
                    IList budgetdetaillist = quarterlyBudgetbll.Summary1(budgetdetailinfo);
                    if (budgetdetaillist != null && budgetdetaillist.Count != 0)
                        companyitem.HavePaid = ((QuarterlyBudgetDetailInfo)budgetdetaillist[0]).RealExpenditure;

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
                    if (Level > ((QuarterlyBudgetDetailInfo)list[list.Count - 1]).EndMonth)
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
}
