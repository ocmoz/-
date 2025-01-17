﻿using System;
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
using FM2E.BLL.Budget;
using FM2E.Model.Budget;
using WebUtility.Components;
using WebUtility;
using FM2E.Model.Exceptions;
using FM2E.Model.Utils;
using System.Collections.Generic;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;

public partial class Module_FM2E_BudgetManager_MonthlyBudgetManager_MakeMonthlyBudget : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    string companyid = UserData.CurrentUserData.CompanyID;
    long YearID = (long)Common.sink("BudgetYearID", MethodType.Get, 255, 0, DataType.Long);
    long id = (long)Common.sink("id", MethodType.Get, 255, 0, DataType.Long);
    bool inittreeyet = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (YearID == 0)
        {
            AnnualBudget annualbll = new AnnualBudget();
            BudgetPerMonthTotalInfo budgetyearinfo = ((BudgetPerMonthTotalInfo)annualbll.GetBudgetPerMonthTotal(id));
            if (budgetyearinfo != null)
                YearID = budgetyearinfo.BudgetYearID;
        }
        if (!IsPostBack)
        {
            CheckEditable(YearID, id);

            Company companybll = new Company();
            CompanyInfo companyinfo = new CompanyInfo();
            //companyinfo.IsParentCompany = false;
            IList companylist = (List<CompanyInfo>)companybll.Search(companyinfo);
            ViewState["companylist"] = companylist;
            companycount.Value = companylist.Count.ToString();

            inittreeyet = false;
            AddTree(0, (TreeNode)null);
            BindData();
            TreeView1.ShowLines = true;
            BudgetApplTotalydiv.InnerHtml = "";
            TotalExpenditurediv.InnerHtml = "";
            NonPaymentdiv.InnerHtml = "";
            BudgetPermonthdiv.InnerHtml = "";
            Totaldiv.InnerHtml = "";
            SurplusExpenditurediv.InnerHtml = "";
            AddTextBox(TreeView1.Nodes);
            inittreeyet = true;

            //AddTree2(0, (TreeNode)null);
            //TreeView2.ShowLines = true;
        }
        InitForm();
        //if (((HtmlInputHidden)WorkFlowUserSelectControl1.FindControl("clickyet")).Value == "true")
        //{
        //    AddSession();
        //    BudgetApplTotalydiv.InnerHtml = "";
        //    TotalExpenditurediv.InnerHtml = "";
        //    NonPaymentdiv.InnerHtml = "";
        //    BudgetPermonthdiv.InnerHtml = "";
        //    Totaldiv.InnerHtml = "";
        //    SurplusExpenditurediv.InnerHtml = "";
        //    AddTextBox(TreeView1.Nodes);
        //    ((HtmlInputHidden)WorkFlowUserSelectControl1.FindControl("clickyet")).Value = "false";
        //}
    }

    private void InitForm()
    {
        expenditure.Cells.Clear();
        if (expenditure.Cells.Count == 0)
        {
            foreach (CompanyInfo companyinfo in (List<CompanyInfo>)ViewState["companylist"])
            {
                HtmlTableCell celltitle = new HtmlTableCell();
                celltitle.InnerText = companyinfo.CompanyName + "：";
                expenditure.Cells.Add(celltitle);

                HtmlInputText inputtext = new HtmlInputText();
                inputtext.Size = 14;
                inputtext.Attributes["id"] = "inputtext" + companyinfo.CompanyID;
                if (Session["inputtext" + companyinfo.CompanyID] != null)
                    inputtext.Value = Session["inputtext" + companyinfo.CompanyID].ToString(); ;
                inputtext.Attributes["onblur"] = "javascript:AddSession(this.id,this.value);";

                HtmlTableCell cellcontent = new HtmlTableCell();
                cellcontent.Controls.Add(inputtext);

                expenditure.Cells.Add(cellcontent);
            }
        }
    }

    /// <summary>
    /// 检查预算能否修改的函数
    /// </summary>
    /// <param name="id"></param>
    private void CheckEditable(long id, long MonthlyID)
    {
        string workflowstate = "";
        Session.RemoveAll();
        if (cmd == "add")
        {
            if (id == 0)
                EventMessage.MessageBox(Msg_Type.Info, "操作提示", "请先指定一个年度预算，然后才能为其制定月度预算", new WebException("请先指定一个年度预算，然后才能为其制定月度预算"), Icon_Type.Alert, true, Common.GetHomeBaseUrl("../AnnualBudgetManager/AnnualBudget.aspx"), UrlType.Href, "");

            AnnualBudget bll = new AnnualBudget();
            BudgetYearInfo budgetyearinfo = bll.GetBudgetYear(id);

            if (budgetyearinfo == null)
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "该年度预算已不存在，请先重新指定年度预算", new WebException("该年度预算已不存在，请先重新指定年度预算"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            //if (budgetyearinfo.WorkFlowStateName != "ApprovalSuccess")
            //    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "该年度预算还没审批，不能制定月度预算", new WebException("该年度预算还没审批，不能制定月度预算"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            BudgetPerMonthTotalInfo searchinfo = new BudgetPerMonthTotalInfo();
            searchinfo.BudgetYearID = id;
            int count = bll.Search(searchinfo).Count;
            if (count >= 12)
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "这一年的月度预算已全部制定完毕", new WebException("这一年的月度预算已全部制定完毕"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            else
            {

                Year.Text = budgetyearinfo.Year.ToString();
                Month.Text = Convert.ToString(count + 1);
                INPTitle.Text = budgetyearinfo.Title;
            }
            //workflowstate = WorkflowHelper.GetWorkflowBasicInfo(BudgetMonthlyWorkflow.WorkflowName).InitialStateName;
        }
        if (cmd == "edit")
        {
            AnnualBudget bll = new AnnualBudget();
            BudgetPerMonthTotalInfo budgetpermonthtotalinfo = bll.GetBudgetPerMonthTotal(MonthlyID);
            if (budgetpermonthtotalinfo == null)
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "指定的月度预算不存在！", new WebException("指定的月度预算不存在！"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            //if (budgetpermonthtotalinfo.WorkFlowStateName == "ApprovalSuccess")
            //    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "本月度预算已经验收过，不能再编辑！", new WebException("本月度预算已经验收过，不能再编辑！"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            //if (budgetpermonthtotalinfo.WorkFlowStateName != "Draft")
            //    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "本月度预算已经提交到审批，不能再编辑！", new WebException("本月度预算已经提交到审批，不能再编辑！"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            Year.Text = budgetpermonthtotalinfo.Year.ToString();
            Month.Text = budgetpermonthtotalinfo.Month.ToString();
            INPTitle.Text = budgetpermonthtotalinfo.Title;
            workflowstate = budgetpermonthtotalinfo.WorkFlowStateName;
            ViewState["WorkFlowInstanceID"] = budgetpermonthtotalinfo.WorkFlowInstanceID;
        }

        //WorkFlowUserSelectControl1.EventIDField = "Name";  //不用变
        //WorkFlowUserSelectControl1.EventNameField = "Description";//不用变
        //WorkFlowUserSelectControl1.WorkFlowState = workflowstate;//当前表单的状态，新增的话为初始状态，旧的话，可以通过获取的表单model获得
        //WorkFlowUserSelectControl1.WorkFlowName = BudgetMonthlyWorkflow.WorkflowName;//对应工作流的名称
        //WorkFlowUserSelectControl1.EventListDataSource = WorkflowHelper.GetEventInfoList(BudgetMonthlyWorkflow.WorkflowName, workflowstate);
        ////显示可以选择的事件列表
        //WorkFlowUserSelectControl1.EventListDataBind();
        //WorkFlowUserSelectControl1.ShowCompanySelect = true;//是否需要选择公司
        //WorkFlowUserSelectControl1.SelectedCompanyID = UserData.CurrentUserData.CompanyID;//当前用户公司，不用变
        //WorkFlowUserSelectControl1.SelectedDepartmentID = UserData.CurrentUserData.DepartmentID;//当前用户部门，不用变

        ///*********以下几行为需要选择具体用户的一些事件，例如提交审批********/
        //WorkFlowUserSelectControl1.AddShowSelectDivValue(BudgetMonthlyWorkflow.SubmitNewEvent);
        //WorkFlowUserSelectControl1.AddShowSelectDivValue(BudgetMonthlyWorkflow.SubmitDraftEvent);
        //WorkFlowUserSelectControl1.AddShowSelectDivValue(BudgetMonthlyWorkflow.SubmitDepartmentApprovalEvent);
        //WorkFlowUserSelectControl1.AddShowSelectDivValue(BudgetMonthlyWorkflow.SubmitFinanceApprovalEvent);
        //WorkFlowUserSelectControl1.AddShowSelectDivValue(BudgetMonthlyWorkflow.SubmitLeaderApprovalEvent);
        //WorkFlowUserSelectControl1.AddShowSelectDivValue(BudgetMonthlyWorkflow.LeaderReturntoFinanceEvent);
        //WorkFlowUserSelectControl1.AddShowSelectDivValue(BudgetMonthlyWorkflow.CompanyReturntoLeaderEvent);
        //WorkFlowUserSelectControl1.AddShowSelectDivValue(BudgetMonthlyWorkflow.FinanceReturntoDepartmentEvent);
        //((RadioButtonList)WorkFlowUserSelectControl1.FindControl("RadioButtonList_Events")).SelectedIndex = 0;


    }

    private void BindData()
    {

        if (cmd == "edit")
        {
            GetMonthlySessionFromDatabase();
            GetMonthlySessionFromDatabaseForTotalExpenditure();
            staticsfunction(TreeView1.Nodes);
            staticstotal();
        }
        if (cmd == "add")
        {
            GetAnnualSessionFromDatabase();
            staticsfunction(TreeView1.Nodes);
            staticstotal();
        }


    }

    public void AddTree(long ParentID, TreeNode pNode)
    {
        SubjectPerYear subjectperyearinfo = new SubjectPerYear();
        subjectperyearinfo.ParentID = ParentID;
        subjectperyearinfo.Year = YearID;
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
            Node.NavigateUrl = "javascript:return false;";
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
                Node.Expanded = true;
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
    /// <summary>
    /// 从数据库中获取预算金额到Session中
    /// </summary>
    private void GetAnnualSessionFromDatabase()
    {
        AnnualBudget bll = new AnnualBudget();
        SubjectRelation subjectbll = new SubjectRelation();
        BudgetYearInfo budgetyeatinfo = bll.GetBudgetYear(YearID);

        BudgetYearDetailInfo budgetyeardetailinfo = new BudgetYearDetailInfo();
        budgetyeardetailinfo.BudgetYearID = YearID;
        budgetyeardetailinfo.CompanyID = "al";
        IList<BudgetYearDetailInfo> list = bll.Search(budgetyeardetailinfo);

        foreach (BudgetYearDetailInfo item in list)
        {
            item.BudgetApprove = item.BudgetApply;
            Session[item.SubID.ToString() + "BudgetApply"] = item.BudgetApprove;
            Session[item.SubID.ToString() + "BudgetPermonth"] = item.BudgetApprove / 12;
            //int year = budgetyeatinfo.Year;
            //int month = 1;
            //if (((SubjectRelationInfos)subjectbll.GetSubjectRelation(item.SubID)).IsLeaf == 1)
            //{
            //    while (Convert.ToInt32(Month.Text) > 2 && month <= Convert.ToInt32(Month.Text) - 2)
            //    {
            //        TotalExpenditure += GetAllSubRealBudget(year, month, item.SubID);
            //        month++;
            //    }
            //    Session[item.SubID.ToString() + "TotalExpenditure"] = TotalExpenditure;
            //}
            decimal TotalExpenditure = 0;
            int year = budgetyeatinfo.Year;
            int month = 1;
            SubjectPerYear subjectperyear = new SubjectPerYear();
            subjectperyear.SubID = item.SubID;
            subjectperyear.Year = budgetyeatinfo.BudgetYearID;
            IList subjectperyearlist = (List<SubjectRelationInfos>)subjectbll.Search(subjectperyear);
            if (subjectperyearlist.Count != 0 && ((SubjectRelationInfos)subjectperyearlist[0]).IsLeaf == 1)
            {
                while (Convert.ToInt32(Month.Text) > 1 && month <= Convert.ToInt32(Month.Text) - 1)
                {
                    TotalExpenditure += GetAllSubRealBudget(year, month, item.SubID, budgetyeatinfo.Title);
                    month++;
                }
                Session[item.SubID.ToString() + "TotalExpenditure"] = TotalExpenditure;
            }

        }


        ////BudgetYearDetailInfo budgetyeardetailinfo2 = new BudgetYearDetailInfo();
        ////budgetyeardetailinfo2.BudgetYearID = YearID;
        ////budgetyeardetailinfo2.CompanyID = "al";
        //IList<BudgetYearDetailInfo> list2 = bll.Search(budgetyeardetailinfo);


        //foreach (BudgetYearDetailInfo item in list2)
        //{
            
        //}




    }

    private IList convertfromreallist(IList list)
    {
        IList listtemp = new List<BudgetDetailInfo>();
        int count = 0;
        decimal expenditure = 0;
        decimal budgetapproval = 0;
        int comcount = Convert.ToInt32(companycount.Value);
        foreach (BudgetDetailInfo budgetdetailinfo in (List<BudgetDetailInfo>)list)
        {
            if (count % comcount == 0)
            {
                expenditure = budgetdetailinfo.Expenditure;
                budgetapproval = budgetdetailinfo.BudgetApprove;
                listtemp.Add(budgetdetailinfo);
                ((BudgetDetailInfo)listtemp[count / comcount]).ExpenditureStr = budgetdetailinfo.CompanyName + ":" + budgetdetailinfo.Expenditure + " ";
                ((BudgetDetailInfo)listtemp[count / comcount]).BudgetApproveStr = budgetdetailinfo.CompanyName + ":" + budgetdetailinfo.BudgetApprove + " ";

            }
            else
            {
                expenditure += budgetdetailinfo.Expenditure;
                budgetapproval += budgetdetailinfo.BudgetApprove;
                ((BudgetDetailInfo)listtemp[count / comcount]).ExpenditureStr += budgetdetailinfo.CompanyName + ":" + budgetdetailinfo.Expenditure + " ";
                ((BudgetDetailInfo)listtemp[count / comcount]).BudgetApproveStr += budgetdetailinfo.CompanyName + ":" + budgetdetailinfo.BudgetApprove + " ";
                if (count % comcount == 3)
                {
                    ((BudgetDetailInfo)listtemp[count / comcount]).ExpenditureStr += "小计" + ":" + expenditure;
                    ((BudgetDetailInfo)listtemp[count / comcount]).BudgetApproveStr += "小计" + ":" + budgetapproval;
                }

            }
            count++;
        }
        return listtemp;
    }

    private void GetMonthlySessionFromDatabase()
    {
        AnnualBudget bll = new AnnualBudget();

        BudgetPermonthInfo budgetpermonthinfo = new BudgetPermonthInfo();
        budgetpermonthinfo.TotalID = id;

        BudgetDetailInfo budgetdetailinfo = new BudgetDetailInfo();
        budgetdetailinfo.TotalID = id;

        IList budgetdetaillist = (List<BudgetDetailInfo>)bll.Search(budgetdetailinfo);
        Session["RealBudgetDetaillist"] = budgetdetaillist;
        Session["BudgetDetaillist"] = convertfromreallist(budgetdetaillist);
        GridView1.DataSource = Session["BudgetDetaillist"];
        GridView1.DataBind();


        IList<BudgetPermonthInfo> list = bll.Search(budgetpermonthinfo);

        foreach (BudgetPermonthInfo item in list)
        {
            Session[item.SubID.ToString() + "BudgetApply"] = item.BudgetApply;
            Session[item.SubID.ToString() + "TotalExpenditure"] = item.TotalExpenditure;
            Session[item.SubID.ToString() + "NonPayment"] = item.NonPayment;
            Session[item.SubID.ToString() + "BudgetPermonth"] = item.BudgetPermonth;
            Session[item.SubID.ToString() + "Total"] = item.Total;
            Session[item.SubID.ToString() + "SurplusExpenditure"] = item.SurplusExpenditure;
        }
    }

    private void GetMonthlySessionFromDatabaseForTotalExpenditure()
    {
        AnnualBudget bll = new AnnualBudget();
        SubjectRelation subjectbll = new SubjectRelation();
        BudgetYearInfo budgetyeatinfo = bll.GetBudgetYear(YearID);

        BudgetYearDetailInfo budgetyeardetailinfo = new BudgetYearDetailInfo();
        budgetyeardetailinfo.BudgetYearID = YearID;
        budgetyeardetailinfo.CompanyID = "al";
        IList<BudgetYearDetailInfo> list = bll.Search(budgetyeardetailinfo);

        foreach (BudgetYearDetailInfo item in list)
        {
            item.BudgetApprove = item.BudgetApply;
            Session[item.SubID.ToString() + "BudgetApply"] = item.BudgetApprove;
            //Session[item.SubID.ToString() + "BudgetPermonth"] = item.BudgetApprove / 12;
            //int year = budgetyeatinfo.Year;
            //int month = 1;
            //if (((SubjectRelationInfos)subjectbll.GetSubjectRelation(item.SubID)).IsLeaf == 1)
            //{
            //    while (Convert.ToInt32(Month.Text) > 2 && month <= Convert.ToInt32(Month.Text) - 2)
            //    {
            //        TotalExpenditure += GetAllSubRealBudget(year, month, item.SubID);
            //        month++;
            //    }
            //    Session[item.SubID.ToString() + "TotalExpenditure"] = TotalExpenditure;
            //}
            decimal TotalExpenditure = 0;
            int year = budgetyeatinfo.Year;
            int month = 1;
            SubjectPerYear subjectperyear = new SubjectPerYear();
            subjectperyear.SubID = item.SubID;
            subjectperyear.Year = budgetyeatinfo.BudgetYearID;
            IList subjectperyearlist = (List<SubjectRelationInfos>)subjectbll.Search(subjectperyear);
            if (subjectperyearlist.Count != 0 && ((SubjectRelationInfos)subjectperyearlist[0]).IsLeaf == 1)
            {
                while (Convert.ToInt32(Month.Text) > 1 && month <= Convert.ToInt32(Month.Text) - 1)
                {
                    TotalExpenditure += GetAllSubRealBudget(year, month, item.SubID, budgetyeatinfo.Title);
                    month++;
                }
                Session[item.SubID.ToString() + "TotalExpenditure"] = TotalExpenditure;
            }

        }
    }
    /// <summary>
    /// 动态生成输入框
    /// </summary>
    /// <param name="nodes"></param>
    private void AddTextBox(TreeNodeCollection nodes)
    {
        foreach (TreeNode nodetemp in nodes)
        {
            string BudgetApply = "";
            string TotalExpenditure = "";
            string NonPayment = "";
            string BudgetPermonth = "";
            string Total = "";
            string SurplusExpenditure = "";
            if (nodetemp.Parent != null)
            {
                string showornot = (nodetemp.Parent.Expanded == true) ? "block" : "none";
                if (showornot == "none")
                    nodetemp.Expanded = false;
                if (Session[nodetemp.Value + "BudgetApply"] != null)
                    BudgetApply = Convert.ToDecimal(Session[nodetemp.Value + "BudgetApply"]).ToString("0.##");
                if (Session[nodetemp.Value + "TotalExpenditure"] != null)
                    TotalExpenditure = Convert.ToDecimal(Session[nodetemp.Value + "TotalExpenditure"]).ToString("0.##");
                if (Session[nodetemp.Value + "NonPayment"] != null)
                    NonPayment = Convert.ToDecimal(Session[nodetemp.Value + "NonPayment"]).ToString("0.##");
                if (Session[nodetemp.Value + "BudgetPermonth"] != null)
                    BudgetPermonth = Convert.ToDecimal(Session[nodetemp.Value + "BudgetPermonth"]).ToString("0.##");
                if (Session[nodetemp.Value + "Total"] != null)
                    Total = Convert.ToDecimal(Session[nodetemp.Value + "Total"]).ToString("0.##");
                if (Session[nodetemp.Value + "SurplusExpenditure"] != null)
                    SurplusExpenditure = Convert.ToDecimal(Session[nodetemp.Value + "SurplusExpenditure"]).ToString("0.##");
                if (nodetemp.ChildNodes.Count > 0)
                {
                    BudgetApplTotalydiv.InnerHtml += "<input title='" + nodetemp.Text + "'  type='text' id='" + nodetemp.Value + "BudgetApply' style='background:#d0d0d0;width:100px;display:" + showornot + "' runat='server' value='" + BudgetApply + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    TotalExpenditurediv.InnerHtml += "<input type='text'  title='" + nodetemp.Text + "' id='" + nodetemp.Value + "TotalExpenditure' style='background:#d0d0d0;width:100px;display:" + showornot + "' runat='server' value='" + TotalExpenditure + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    NonPaymentdiv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "NonPayment' style='background:#d0d0d0;width:100px;display:" + showornot + "' runat='server' value='" + NonPayment + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    BudgetPermonthdiv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "BudgetPermonth' style='background:#d0d0d0;width:100px;display:" + showornot + "' runat='server' value='" + BudgetPermonth + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    Totaldiv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "Total' style='background:#d0d0d0;width:100px;display:" + showornot + "' runat='server' value='" + Total + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    SurplusExpenditurediv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "SurplusExpenditure' style='background:#d0d0d0;width:100px;display:" + showornot + "' runat='server' value='" + SurplusExpenditure + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                }
                else
                {
                    BudgetApplTotalydiv.InnerHtml += "<input  title='" + nodetemp.Text + "' type='text' id='" + nodetemp.Value + "BudgetApply' style='width:100px;display:" + showornot + "' runat='server' value='" + BudgetApply + "' onblur='javascript:AddSession(this.id,this.value);' />";
                    TotalExpenditurediv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "TotalExpenditure' style='width:100px;display:" + showornot + "' runat='server' value='" + TotalExpenditure + "' onblur='javascript:AddSession(this.id,this.value);' />";
                    NonPaymentdiv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "NonPayment' style='width:100px;display:" + showornot + "' runat='server' value='" + NonPayment + "' onblur='javascript:AddSession(this.id,this.value);' />";
                    BudgetPermonthdiv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "BudgetPermonth' style='width:100px;display:" + showornot + "' runat='server' value='" + BudgetPermonth + "' onblur='javascript:AddSession(this.id,this.value);' />";
                    Totaldiv.InnerHtml += "<input type='text'  title='" + nodetemp.Text + "' id='" + nodetemp.Value + "Total' style='width:100px;display:" + showornot + "' runat='server' value='" + Total + "' onblur='javascript:AddSession(this.id,this.value);' />";
                    SurplusExpenditurediv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "SurplusExpenditure' style='width:100px;display:" + showornot + "' runat='server' value='" + SurplusExpenditure + "' onblur='javascript:AddSession(this.id,this.value);' />";
                }
                treeviewitemdiv.InnerHtml += "<input type='text' id='" + nodetemp.Text + "treeviewitem' value='" + nodetemp.Value + "' />";
            }
            else
            {
                if (Session[nodetemp.Value + "BudgetApply"] != null)
                    BudgetApply = ((Decimal)Session[nodetemp.Value + "BudgetApply"]).ToString("0.##");
                if (Session[nodetemp.Value + "TotalExpenditure"] != null)
                    TotalExpenditure = ((Decimal)Session[nodetemp.Value + "TotalExpenditure"]).ToString("0.##");
                if (Session[nodetemp.Value + "NonPayment"] != null)
                    NonPayment = ((Decimal)Session[nodetemp.Value + "NonPayment"]).ToString("0.##");
                if (Session[nodetemp.Value + "BudgetPermonth"] != null)
                    BudgetPermonth = ((Decimal)Session[nodetemp.Value + "BudgetPermonth"]).ToString("0.##");
                if (Session[nodetemp.Value + "Total"] != null)
                    Total = ((Decimal)Session[nodetemp.Value + "Total"]).ToString("0.##");
                if (Session[nodetemp.Value + "SurplusExpenditure"] != null)
                    SurplusExpenditure = ((Decimal)Session[nodetemp.Value + "SurplusExpenditure"]).ToString("0.##");
                if (nodetemp.ChildNodes.Count > 0)
                {
                    BudgetApplTotalydiv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "BudgetApply' style='background:#d0d0d0;width:100px;display:block' runat='server' value='" + BudgetApply + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    TotalExpenditurediv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "TotalExpenditure' style='background:#d0d0d0;width:100px;display:block' runat='server' value='" + TotalExpenditure + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    NonPaymentdiv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "NonPayment' style='background:#d0d0d0;width:100px;display:block' runat='server' value='" + NonPayment + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    BudgetPermonthdiv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "BudgetPermonth' style='background:#d0d0d0;width:100px;display:block' runat='server' value='" + BudgetPermonth + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    Totaldiv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "Total' style='background:#d0d0d0;width:100px;display:block' runat='server' value='" + Total + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    SurplusExpenditurediv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "SurplusExpenditure' style='background:#d0d0d0;width:100px;display:block' runat='server' value='" + SurplusExpenditure + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                }
                else
                {
                    BudgetApplTotalydiv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "BudgetApply' style='width:100px;display:block' runat='server' value='" + BudgetApply + "'onblur='javascript:AddSession(this.id,this.value);' />";
                    TotalExpenditurediv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "TotalExpenditure' style='width:100px;display:block' runat='server' value='" + TotalExpenditure + "'onblur='javascript:AddSession(this.id,this.value);' />";
                    NonPaymentdiv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "NonPayment' style='width:100px;display:block' runat='server' value='" + NonPayment + "'onblur='javascript:AddSession(this.id,this.value);' />";
                    BudgetPermonthdiv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "BudgetPermonth' style='width:100px;display:block' runat='server' value='" + BudgetPermonth + "'onblur='javascript:AddSession(this.id,this.value);' />";
                    Totaldiv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "Total' style='width:100px;display:block' runat='server' value='" + Total + "'onblur='javascript:AddSession(this.id,this.value);' />";
                    SurplusExpenditurediv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "SurplusExpenditure' style='width:100px;display:block' runat='server' value='" + SurplusExpenditure + "'onblur='javascript:AddSession(this.id,this.value);' />";
                }
                treeviewitemdiv.InnerHtml += "<input type='text' id='" + nodetemp.Text + "treeviewitem' value='" + nodetemp.Value + "' />";
            }
            AddTextBox(nodetemp.ChildNodes);
        }

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
                decimal BudgetApply = 0;
                decimal TotalExpenditure = 0;
                decimal NonPayment = 0;
                decimal BudgetPermonth = 0;
                decimal Total = 0;
                decimal SurplusExpenditure = 0;
                foreach (TreeNode subnodetemp in nodetemp.ChildNodes)
                {

                    if (Session[subnodetemp.Value + "BudgetApply"] != null)
                    {
                        BudgetApply += Convert.ToDecimal(Session[subnodetemp.Value + "BudgetApply"]);
                    }
                    if (Session[subnodetemp.Value + "TotalExpenditure"] != null)
                    {
                        TotalExpenditure += Convert.ToDecimal(Session[subnodetemp.Value + "TotalExpenditure"]);
                    }
                    if (Session[subnodetemp.Value + "NonPayment"] != null)
                    {
                        NonPayment += Convert.ToDecimal(Session[subnodetemp.Value + "NonPayment"]);
                    }
                    if (Session[subnodetemp.Value + "BudgetPermonth"] != null)
                    {
                        BudgetPermonth += Convert.ToDecimal(Session[subnodetemp.Value + "BudgetPermonth"]);
                    }
                    if (Session[subnodetemp.Value + "Total"] != null)
                    {
                        Total += Convert.ToDecimal(Session[subnodetemp.Value + "Total"]);
                    }
                    if (Session[subnodetemp.Value + "SurplusExpenditure"] != null)
                    {
                        SurplusExpenditure += Convert.ToDecimal(Session[subnodetemp.Value + "SurplusExpenditure"]);
                    }
                }
                Session[nodetemp.Value + "BudgetApply"] = BudgetApply;
                Session[nodetemp.Value + "TotalExpenditure"] = TotalExpenditure;
                Session[nodetemp.Value + "NonPayment"] = NonPayment;
                Session[nodetemp.Value + "BudgetPermonth"] = BudgetPermonth;
                Session[nodetemp.Value + "Total"] = Total;
                Session[nodetemp.Value + "SurplusExpenditure"] = SurplusExpenditure;

            }
            else
            {
                decimal BudgetApply = 0;
                decimal TotalExpenditure = 0;
                decimal NonPayment = 0;
                decimal BudgetPermonth = 0;
                decimal Total = 0;
                decimal SurplusExpenditure = 0;
                if (Session[nodetemp.Value + "BudgetApply"] != null)
                {
                    BudgetApply = Convert.ToDecimal(Session[nodetemp.Value + "BudgetApply"]);
                }
                else
                    Session[nodetemp.Value + "BudgetApply"] = decimal.Zero;
                if (Session[nodetemp.Value + "TotalExpenditure"] != null)
                {
                    TotalExpenditure = Convert.ToDecimal(Session[nodetemp.Value + "TotalExpenditure"]);
                }
                else
                    Session[nodetemp.Value + "TotalExpenditure"] = decimal.Zero;
                if (Session[nodetemp.Value + "NonPayment"] != null)
                {
                    NonPayment = Convert.ToDecimal(Session[nodetemp.Value + "NonPayment"]);
                }
                else
                    Session[nodetemp.Value + "NonPayment"] = decimal.Zero;
                if (Session[nodetemp.Value + "BudgetPermonth"] != null)
                {
                    BudgetPermonth = Convert.ToDecimal(Session[nodetemp.Value + "BudgetPermonth"]);
                }
                else
                    Session[nodetemp.Value + "BudgetPermonth"] = decimal.Zero;

                Total = TotalExpenditure + NonPayment + BudgetPermonth;
                SurplusExpenditure = BudgetApply - Total;
                Session[nodetemp.Value + "Total"] = Total;
                Session[nodetemp.Value + "SurplusExpenditure"] = SurplusExpenditure;
            }
        }
    }

    private void staticstotal()
    {
        decimal BudgetApply = 0;
        decimal TotalExpenditure = 0;
        decimal NonPayment = 0;
        decimal BudgetPermonth = 0;
        decimal Total = 0;
        decimal SurplusExpenditure = 0;
        foreach (TreeNode nodetemp in TreeView1.Nodes)
        {
            if (Session[nodetemp.Value + "BudgetApply"] != null)
            {
                BudgetApply += Convert.ToDecimal(Session[nodetemp.Value + "BudgetApply"]);
            }
            if (Session[nodetemp.Value + "TotalExpenditure"] != null)
            {
                TotalExpenditure += Convert.ToDecimal(Session[nodetemp.Value + "TotalExpenditure"]);
            }
            if (Session[nodetemp.Value + "NonPayment"] != null)
            {
                NonPayment += Convert.ToDecimal(Session[nodetemp.Value + "NonPayment"]);
            }
            if (Session[nodetemp.Value + "BudgetPermonth"] != null)
            {
                BudgetPermonth += Convert.ToDecimal(Session[nodetemp.Value + "BudgetPermonth"]);
            }
            if (Session[nodetemp.Value + "Total"] != null)
            {
                Total += Convert.ToDecimal(Session[nodetemp.Value + "Total"]);
            }
            if (Session[nodetemp.Value + "SurplusExpenditure"] != null)
            {
                SurplusExpenditure += Convert.ToDecimal(Session[nodetemp.Value + "SurplusExpenditure"]);
            }
        }
        Session["TotalBudgetApply"] = BudgetApply;
        Session["TotalTotalExpenditure"] = TotalExpenditure;
        Session["TotalNonPayment"] = NonPayment;
        Session["TotalBudgetPermonth"] = BudgetPermonth;
        Session["TotalTotal"] = Total;
        Session["TotalSurplusExpenditure"] = SurplusExpenditure;
    }
    /// <summary>
    /// 树的展开事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void TreeView1_OnTreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {
        AddSession();
        if (inittreeyet)
        {
            BudgetApplTotalydiv.InnerHtml = "";
            TotalExpenditurediv.InnerHtml = "";
            NonPaymentdiv.InnerHtml = "";
            BudgetPermonthdiv.InnerHtml = "";
            Totaldiv.InnerHtml = "";
            SurplusExpenditurediv.InnerHtml = "";
            AddTextBox(TreeView1.Nodes);
        }
    }
    /// <summary>
    /// 树展开事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void TreeView1_OnTreeNodeCollapsed(object sender, TreeNodeEventArgs e)
    {
        AddSession();
        BudgetApplTotalydiv.InnerHtml = "";
        TotalExpenditurediv.InnerHtml = "";
        NonPaymentdiv.InnerHtml = "";
        BudgetPermonthdiv.InnerHtml = "";
        Totaldiv.InnerHtml = "";
        SurplusExpenditurediv.InnerHtml = "";
        AddTextBox(TreeView1.Nodes);
    }
    /// <summary>
    /// 保存输入的信息
    /// </summary>
    private void AddSession()
    {
        string[] sessionarray = sessionvalue.Value.Split('|');
        foreach (string sessioncontent in sessionarray)
        {
            if (sessioncontent != null && sessioncontent != string.Empty)
            {
                string[] temp = sessioncontent.Split(',');
                Session[temp[0]] = temp[1];

            }
        }
    }
    /// <summary>
    /// 提交月度预算事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    protected void Sure_Click(object sender, EventArgs e)
    {
        //if (!WorkFlowUserSelectControl1.ProperlySelected)
        //{
        //    EventMessage.MessageBox(Msg_Type.Warn, "无法提交申请单", "无选择下一处理用户",
        //      Icon_Type.Error, true, Common.GetHomeBaseUrl("MakeMonthlyBudget.aspx?cmd=" + cmd + "&BudgetYearID=" + YearID + "&id=" + id), UrlType.Href, "");
        //    return;
        //}
        AddSession();
        staticsfunction(TreeView1.Nodes);
        staticstotal();
        long newMonthlyBudgetid = id;
        if (cmd == "add")
        {
            BudgetPerMonthTotalInfo budgetpermonthtotalinfo = new BudgetPerMonthTotalInfo();
            try
            {
                budgetpermonthtotalinfo.Year = Convert.ToInt32(Year.Text);
                budgetpermonthtotalinfo.Month = Convert.ToInt32(Month.Text);
                budgetpermonthtotalinfo.Title = INPTitle.Text;
                budgetpermonthtotalinfo.CompanyID = companyid;
                budgetpermonthtotalinfo.ViceEngineerReview = "";
                budgetpermonthtotalinfo.ViceManagerReview = "";
                budgetpermonthtotalinfo.ManagerReview = "";
                budgetpermonthtotalinfo.FinanceReview = "";
                budgetpermonthtotalinfo.Result = false;
                budgetpermonthtotalinfo.TotalExpenditure = (Session["TotalTotalExpenditure"] != null) ? Convert.ToDecimal(Session["TotalTotalExpenditure"]) : decimal.Zero;
                budgetpermonthtotalinfo.BudgetPermonth = (Session["TotalBudgetPermonth"] != null) ? Convert.ToDecimal(Session["TotalBudgetPermonth"]) : decimal.Zero;
                budgetpermonthtotalinfo.SurplusExpenditure = (Session["TotalSurplusExpenditure"] != null) ? Convert.ToDecimal(Session["TotalSurplusExpenditure"]) : decimal.Zero;
                budgetpermonthtotalinfo.NonPayment = (Session["TotalNonPayment"] != null) ? Convert.ToDecimal(Session["TotalNonPayment"]) : decimal.Zero;
                budgetpermonthtotalinfo.Total = (Session["TotalTotal"] != null) ? Convert.ToDecimal(Session["TotalTotal"]) : decimal.Zero;
                budgetpermonthtotalinfo.MakeTime = DateTime.Now;
                budgetpermonthtotalinfo.Expenditure = decimal.Zero;
                budgetpermonthtotalinfo.Allocation = decimal.Zero;
                budgetpermonthtotalinfo.Deviation = decimal.Zero;
                budgetpermonthtotalinfo.BudgetApply = (Session["TotalBudgetApply"] != null) ? Convert.ToDecimal(Session["TotalBudgetApply"]) : decimal.Zero;
                budgetpermonthtotalinfo.Status = Convert.ToInt16(0);
                budgetpermonthtotalinfo.BudgetYearID = YearID;
                budgetpermonthtotalinfo.DetailList = new List<BudgetPermonthInfo>();
                budgetpermonthtotalinfo.BudgetDetailList = new List<BudgetDetailInfo>();
                budgetpermonthtotalinfo.Approvaler = string.Empty;


            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入预算数额不符合格式要求", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }
            if (Session["RealBudgetDetaillist"] != null)
                foreach (BudgetDetailInfo budgetdetailinfo in (List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])
                {
                    budgetpermonthtotalinfo.BudgetDetailList.Add(budgetdetailinfo);
                }
            GetDetailList(TreeView1.Nodes, budgetpermonthtotalinfo.DetailList);
            AnnualBudget bll = new AnnualBudget();
            BudgetPerMonthTotalInfo searchinfo = new BudgetPerMonthTotalInfo();
            searchinfo.BudgetYearID = budgetpermonthtotalinfo.BudgetYearID;
            searchinfo.Month = budgetpermonthtotalinfo.Month;
            if (bll.Search(searchinfo).Count > 0)
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "本月度预算已经提交过了，不能再创建", new WebException("本月度预算已经提交过了，不能再创建"), Icon_Type.Error, true, Common.GetHomeBaseUrl("MakeMonthlyBudget.aspx?cmd=" + cmd + "&BudgetYearID=" + YearID + "&id=" + id), UrlType.Href, "");
            try
            {
                newMonthlyBudgetid = bll.InsertBudgetPerMonthTotal(budgetpermonthtotalinfo);
                //Guid guid = WorkflowHelper.CreateNewInstance(newMonthlyBudgetid, BudgetMonthlyWorkflow.WorkflowName);
                //WorkflowHelper.SetStateMachine<BudgetMonthlyEventService>(guid, WorkFlowUserSelectControl1.SelectedEvent);
                //WorkflowHelper.UpdateNextUserID(guid, WorkFlowUserSelectControl1.NextUserName, WorkFlowUserSelectControl1.DelegateUserName);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交月度预算失败", ex, Icon_Type.Error, true, Common.GetHomeBaseUrl("MakeMonthlyBudget.aspx?cmd=" + cmd + "&BudgetYearID=" + YearID + "&id=" + id), UrlType.Href, "");
            }


        }
        if (cmd == "edit")
        {
            AnnualBudget bll = new AnnualBudget();
            BudgetPerMonthTotalInfo budgetpermonthtotalinfo = new BudgetPerMonthTotalInfo();
            try
            {
                budgetpermonthtotalinfo.TotalID = id;
                budgetpermonthtotalinfo.Year = Convert.ToInt32(Year.Text);
                budgetpermonthtotalinfo.Month = Convert.ToInt32(Month.Text);
                budgetpermonthtotalinfo.Title = INPTitle.Text;
                budgetpermonthtotalinfo.CompanyID = companyid;
                budgetpermonthtotalinfo.ViceEngineerReview = "";
                budgetpermonthtotalinfo.ViceManagerReview = "";
                budgetpermonthtotalinfo.ManagerReview = "";
                budgetpermonthtotalinfo.FinanceReview = "";
                budgetpermonthtotalinfo.Result = false;
                budgetpermonthtotalinfo.TotalExpenditure = (Session["TotalTotalExpenditure"] != null) ? Convert.ToDecimal(Session["TotalTotalExpenditure"]) : decimal.Zero;
                budgetpermonthtotalinfo.BudgetPermonth = (Session["TotalBudgetPermonth"] != null) ? Convert.ToDecimal(Session["TotalBudgetPermonth"]) : decimal.Zero;
                budgetpermonthtotalinfo.SurplusExpenditure = (Session["TotalSurplusExpenditure"] != null) ? Convert.ToDecimal(Session["TotalSurplusExpenditure"]) : decimal.Zero;
                budgetpermonthtotalinfo.NonPayment = (Session["TotalNonPayment"] != null) ? Convert.ToDecimal(Session["TotalNonPayment"]) : decimal.Zero;
                budgetpermonthtotalinfo.Total = (Session["TotalTotal"] != null) ? Convert.ToDecimal(Session["TotalTotal"]) : decimal.Zero;
                budgetpermonthtotalinfo.MakeTime = DateTime.Now;
                budgetpermonthtotalinfo.Expenditure = decimal.Zero;
                budgetpermonthtotalinfo.Allocation = decimal.Zero;
                budgetpermonthtotalinfo.Deviation = decimal.Zero;
                budgetpermonthtotalinfo.BudgetApply = (Session["TotalBudgetApply"] != null) ? Convert.ToDecimal(Session["TotalBudgetApply"]) : decimal.Zero;
                budgetpermonthtotalinfo.Status = Convert.ToInt16(0);
                budgetpermonthtotalinfo.BudgetYearID = bll.GetBudgetPerMonthTotal(id).BudgetYearID;
                budgetpermonthtotalinfo.DetailList = new List<BudgetPermonthInfo>();
                budgetpermonthtotalinfo.BudgetDetailList = new List<BudgetDetailInfo>();
                budgetpermonthtotalinfo.Approvaler = string.Empty;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入预算数额不符合格式要求", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }
            if (Session["RealBudgetDetaillist"] != null)
                foreach (BudgetDetailInfo budgetdetailinfo in (List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])
                {
                    budgetpermonthtotalinfo.BudgetDetailList.Add(budgetdetailinfo);
                }
            GetDetailList(TreeView1.Nodes, budgetpermonthtotalinfo.DetailList);
            try
            {
                budgetpermonthtotalinfo.UpdateBudgetDetail = true;
                bll.UpdateBudgetPerMonthTotal(budgetpermonthtotalinfo);
                //WorkflowHelper.SetStateMachine<BudgetMonthlyEventService>(new Guid(ViewState["WorkFlowInstanceID"].ToString()), WorkFlowUserSelectControl1.SelectedEvent);
                //WorkflowHelper.UpdateNextUserID(new Guid(ViewState["WorkFlowInstanceID"].ToString()), WorkFlowUserSelectControl1.NextUserName, WorkFlowUserSelectControl1.DelegateUserName);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交月度预算失败", ex, Icon_Type.Error, true, Common.GetHomeBaseUrl("MakeMonthlyBudget.aspx?cmd=" + cmd + "&BudgetYearID=" + YearID + "&id=" + id), UrlType.Href, "");
            }
        }
        Session.RemoveAll();
        Response.Redirect("ViewMonthlyBudget.aspx?cmd=view&id=" + newMonthlyBudgetid);
        //if (newMonthlyBudgetid > 0)
        //    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "已保存为草稿", Icon_Type.OK, true, Common.GetHomeBaseUrl("MonthlyBudget.aspx"), UrlType.Href, "");

    }
    /// <summary>
    /// 保存为草稿
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void SaveAsTemp_Click(object sender, EventArgs e)
    {
        AddSession();
        staticsfunction(TreeView1.Nodes);
        staticstotal();
        long newMonthlyBudgetid = id;
        if (cmd == "add")
        {
            BudgetPerMonthTotalInfo budgetpermonthtotalinfo = new BudgetPerMonthTotalInfo();
            try
            {
                budgetpermonthtotalinfo.Year = Convert.ToInt32(Year.Text);
                budgetpermonthtotalinfo.Month = Convert.ToInt32(Month.Text);
                budgetpermonthtotalinfo.CompanyID = companyid;
                budgetpermonthtotalinfo.ViceEngineerReview = "";
                budgetpermonthtotalinfo.ViceManagerReview = "";
                budgetpermonthtotalinfo.ManagerReview = "";
                budgetpermonthtotalinfo.FinanceReview = "";
                budgetpermonthtotalinfo.Result = false;
                budgetpermonthtotalinfo.TotalExpenditure = (Session["TotalTotalExpenditure"] != null) ? Convert.ToDecimal(Session["TotalTotalExpenditure"]) : decimal.Zero;
                budgetpermonthtotalinfo.BudgetPermonth = (Session["TotalBudgetPermonth"] != null) ? Convert.ToDecimal(Session["TotalBudgetPermonth"]) : decimal.Zero;
                budgetpermonthtotalinfo.SurplusExpenditure = (Session["TotalSurplusExpenditure"] != null) ? Convert.ToDecimal(Session["TotalSurplusExpenditure"]) : decimal.Zero;
                budgetpermonthtotalinfo.NonPayment = (Session["TotalNonPayment"] != null) ? Convert.ToDecimal(Session["TotalNonPayment"]) : decimal.Zero;
                budgetpermonthtotalinfo.Total = (Session["TotalTotal"] != null) ? Convert.ToDecimal(Session["TotalTotal"]) : decimal.Zero;
                budgetpermonthtotalinfo.MakeTime = DateTime.Now;
                budgetpermonthtotalinfo.Expenditure = decimal.Zero;
                budgetpermonthtotalinfo.Allocation = decimal.Zero;
                budgetpermonthtotalinfo.Deviation = decimal.Zero;
                budgetpermonthtotalinfo.BudgetApply = (Session["TotalBudgetApply"] != null) ? Convert.ToDecimal(Session["TotalBudgetApply"]) : decimal.Zero;
                budgetpermonthtotalinfo.Status = Convert.ToInt16(1);
                budgetpermonthtotalinfo.BudgetYearID = YearID;
                budgetpermonthtotalinfo.DetailList = new List<BudgetPermonthInfo>();
                budgetpermonthtotalinfo.BudgetDetailList = new List<BudgetDetailInfo>();



            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入预算数额不符合格式要求", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }
            if (Session["RealBudgetDetaillist"] != null)
                foreach (BudgetDetailInfo budgetdetailinfo in (List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])
                {
                    budgetpermonthtotalinfo.BudgetDetailList.Add(budgetdetailinfo);
                }
            GetDetailList(TreeView1.Nodes, budgetpermonthtotalinfo.DetailList);

            AnnualBudget bll = new AnnualBudget();
            BudgetPerMonthTotalInfo searchinfo = new BudgetPerMonthTotalInfo();
            searchinfo.BudgetYearID = budgetpermonthtotalinfo.BudgetYearID;
            searchinfo.Month = budgetpermonthtotalinfo.Month;
            if (bll.Search(searchinfo).Count > 0)
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "本月度预算已经提交过了，不能再创建", new WebException("本月度预算已经提交过了，不能再创建"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            try
            {
                newMonthlyBudgetid = bll.InsertBudgetPerMonthTotal(budgetpermonthtotalinfo);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存月度预算失败", ex, Icon_Type.Error, true, Common.GetHomeBaseUrl("MakeMonthlyBudget.aspx?cmd=" + cmd + "&BudgetYearID=" + YearID + "&id=" + id), UrlType.Href, "");
            }


        }
        if (cmd == "edit")
        {
            AnnualBudget bll = new AnnualBudget();
            BudgetPerMonthTotalInfo budgetpermonthtotalinfo = new BudgetPerMonthTotalInfo();

            try
            {
                budgetpermonthtotalinfo.TotalID = id;
                budgetpermonthtotalinfo.Year = Convert.ToInt32(Year.Text);
                budgetpermonthtotalinfo.Month = Convert.ToInt32(Month.Text);
                budgetpermonthtotalinfo.CompanyID = companyid;
                budgetpermonthtotalinfo.ViceEngineerReview = "";
                budgetpermonthtotalinfo.ViceManagerReview = "";
                budgetpermonthtotalinfo.ManagerReview = "";
                budgetpermonthtotalinfo.FinanceReview = "";
                budgetpermonthtotalinfo.Result = false;
                budgetpermonthtotalinfo.TotalExpenditure = (Session["TotalTotalExpenditure"] != null) ? Convert.ToDecimal(Session["TotalTotalExpenditure"]) : decimal.Zero;
                budgetpermonthtotalinfo.BudgetPermonth = (Session["TotalBudgetPermonth"] != null) ? Convert.ToDecimal(Session["TotalBudgetPermonth"]) : decimal.Zero;
                budgetpermonthtotalinfo.SurplusExpenditure = (Session["TotalSurplusExpenditure"] != null) ? Convert.ToDecimal(Session["TotalSurplusExpenditure"]) : decimal.Zero;
                budgetpermonthtotalinfo.NonPayment = (Session["TotalNonPayment"] != null) ? Convert.ToDecimal(Session["TotalNonPayment"]) : decimal.Zero;
                budgetpermonthtotalinfo.Total = (Session["TotalTotal"] != null) ? Convert.ToDecimal(Session["TotalTotal"]) : decimal.Zero;
                budgetpermonthtotalinfo.MakeTime = DateTime.Now;
                budgetpermonthtotalinfo.Expenditure = decimal.Zero;
                budgetpermonthtotalinfo.Allocation = decimal.Zero;
                budgetpermonthtotalinfo.Deviation = decimal.Zero;
                budgetpermonthtotalinfo.BudgetApply = (Session["TotalBudgetApply"] != null) ? Convert.ToDecimal(Session["TotalBudgetApply"]) : decimal.Zero;
                budgetpermonthtotalinfo.Status = Convert.ToInt16(1);
                budgetpermonthtotalinfo.BudgetYearID = bll.GetBudgetPerMonthTotal(id).BudgetYearID;
                budgetpermonthtotalinfo.DetailList = new List<BudgetPermonthInfo>();
                budgetpermonthtotalinfo.BudgetDetailList = new List<BudgetDetailInfo>();

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入预算数额不符合格式要求", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }
            if (Session["RealBudgetDetaillist"] != null)
                foreach (BudgetDetailInfo budgetdetailinfo in (List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])
                {
                    budgetpermonthtotalinfo.BudgetDetailList.Add(budgetdetailinfo);
                }
            GetDetailList(TreeView1.Nodes, budgetpermonthtotalinfo.DetailList);
            try
            {
                budgetpermonthtotalinfo.UpdateBudgetDetail = true;
                bll.UpdateBudgetPerMonthTotal(budgetpermonthtotalinfo);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存月度预算失败", ex, Icon_Type.Error, true, Common.GetHomeBaseUrl("MakeMonthlyBudget.aspx?cmd=" + cmd + "&BudgetYearID=" + YearID + "&id=" + id), UrlType.Href, "");
            }
        }
        Session.RemoveAll();
        if (newMonthlyBudgetid > 0)
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "已保存为草稿", Icon_Type.OK, true, Common.GetHomeBaseUrl("MonthlyBudget.aspx"), UrlType.Href, "");


    }

    private void GetDetailList(TreeNodeCollection nodes, IList list)
    {
        foreach (TreeNode nodetemp in nodes)
        {
            if (nodetemp.ChildNodes.Count == 0)
            {
                BudgetPermonthInfo item = new BudgetPermonthInfo();
                item.BudgetYearDetailID = Convert.ToInt32(Year.Text);
                item.CompanyID = companyid;
                item.Month = Convert.ToInt32(Month.Text);
                item.TotalExpenditure = (Session[nodetemp.Value + "TotalExpenditure"] != null && Session[nodetemp.Value + "TotalExpenditure"].ToString() != string.Empty) ? Convert.ToDecimal(Session[nodetemp.Value + "TotalExpenditure"]) : decimal.Zero;
                item.BudgetPermonth = (Session[nodetemp.Value + "BudgetPermonth"] != null && Session[nodetemp.Value + "BudgetPermonth"].ToString() != string.Empty) ? Convert.ToDecimal(Session[nodetemp.Value + "BudgetPermonth"]) : decimal.Zero;
                item.SurplusExpenditure = (Session[nodetemp.Value + "SurplusExpenditure"] != null && Session[nodetemp.Value + "SurplusExpenditure"].ToString() != string.Empty) ? Convert.ToDecimal(Session[nodetemp.Value + "SurplusExpenditure"]) : decimal.Zero;
                item.NonPayment = (Session[nodetemp.Value + "NonPayment"] != null && Session[nodetemp.Value + "NonPayment"].ToString() != string.Empty) ? Convert.ToDecimal(Session[nodetemp.Value + "NonPayment"]) : decimal.Zero;
                item.Total = (Session[nodetemp.Value + "Total"] != null && Session[nodetemp.Value + "Total"].ToString() != string.Empty) ? Convert.ToDecimal(Session[nodetemp.Value + "Total"]) : decimal.Zero;
                item.BudgetApply = (Session[nodetemp.Value + "BudgetApply"] != null && Session[nodetemp.Value + "BudgetApply"].ToString() != string.Empty) ? Convert.ToDecimal(Session[nodetemp.Value + "BudgetApply"]) : decimal.Zero;
                item.Remarks = "";
                item.Manager = UserData.CurrentUserData.PersonName;
                item.MakeTime = DateTime.Now;
                item.Expenditure = decimal.Zero;
                item.Allocation = decimal.Zero;
                item.Deviation = decimal.Zero;
                item.ReasonForDeviation = "";
                item.EvaluationForDeviation = "";
                item.Review = Convert.ToInt16(1);
                item.SubID = Convert.ToInt64(nodetemp.Value);

                list.Add(item);

            }
            GetDetailList(nodetemp.ChildNodes, list);
        }
    }
    /// <summary>
    /// 统计输入的预算额,并计算出其他预算的项
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Tatics_Click(object sender, EventArgs e)
    {
        AddSession();
        staticsfunction(TreeView1.Nodes);
        staticstotal();
        BudgetApplTotalydiv.InnerHtml = "";
        TotalExpenditurediv.InnerHtml = "";
        NonPaymentdiv.InnerHtml = "";
        BudgetPermonthdiv.InnerHtml = "";
        Totaldiv.InnerHtml = "";
        SurplusExpenditurediv.InnerHtml = "";
        AddTextBox(TreeView1.Nodes);
    }

    //protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
    //{
    //    SubIDNametb.Text = this.TreeView2.SelectedNode.Text;
    //    SubIDtb.Text = this.TreeView2.SelectedValue;
    //    PopupControlExtender1.Commit(SubIDNametb.Text);
    //    PopupControlExtender2.Commit(SubIDtb.Text);
    //    TreeView2.SelectedNode.Selected = false;
    //}


    /// <summary>
    /// 初始化种类弹出树
    /// </summary>
    /// <param name="ParentID"></param>
    /// <param name="pNode"></param>
    //public void AddTree2(long ParentID, TreeNode pNode)
    //{
    //    SubjectPerYear subjectperyearinfo = new SubjectPerYear();
    //    subjectperyearinfo.ParentID = ParentID;
    //    subjectperyearinfo.Year = YearID;
    //    SubjectRelation bll = new SubjectRelation();
    //    QueryParam qp = bll.GenerateSearchTermByYear(subjectperyearinfo);
    //    int recordcount = 0;
    //    IList nodelist = bll.GetListByYear(qp, out recordcount, companyid);
    //    List<SubjectRelationInfos> subnodes = new List<SubjectRelationInfos>();
    //    foreach (SubjectRelationInfos node in nodelist)
    //    {
    //        if (node.ParentID == ParentID)
    //            subnodes.Add(node);
    //    }

    //    //循环递归
    //    foreach (SubjectRelationInfos node in subnodes)
    //    {
    //        //声明节点
    //        TreeNode Node = new TreeNode();
    //        //绑定超级链接
    //        //Node.NavigateUrl = "ViewSubjectRelation.aspx?cmd=view&id=" + node.SubID;
    //        if (pNode == null)
    //        {
    //            Node.Text = node.Name;
    //            Node.Value = node.SubID.ToString();
    //            TreeView2.Nodes.Add(Node);
    //            Node.Expanded = false;


    //            //HtmlInputText inputtext = new HtmlInputText();
    //            //TextBox tb = new TextBox();
    //            //tb.ID = "int" + Node.Value;
    //            //inputtext.Attributes["style"] = "display:block";
    //            //inputrow.Controls.Add(tb);
    //            //inputrow.InnerHtml += "<input type='text' id='int" + Node.Value + "' style='display:block' runat='server' enableviewstate='true' />";
    //            AddTree2(node.SubID, Node);

    //        }
    //        else
    //        {
    //            Node.Text = node.Name;
    //            Node.Value = node.SubID.ToString();
    //            pNode.ChildNodes.Add(Node);
    //            Node.Expanded = false;

    //            //HtmlInputText inputtext = new HtmlInputText();
    //            //inputtext.ID = "int" + Node.Value;
    //            //inputtext.Attributes["style"] = "display:none";
    //            //inputrow.Controls.Add(inputtext);
    //            //inputrow.InnerHtml += "<input type='text' id='int" + Node.Value + "' style='display:none' runat='server' enableviewstate='true' />";
    //            AddTree2(node.SubID, Node);
    //        }
    //    }
    //}

    /// <summary>
    /// 列表绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        if (e.CommandName == "edititem")
        {
            try
            {
                inp_index.Value = e.CommandArgument.ToString();
                BudgetDetailInfo item = ((List<BudgetDetailInfo>)Session["BudgetDetaillist"])[Convert.ToInt32(e.CommandArgument)];
                SubIDNametb.Value = item.SubName;
                SubIDtb.Value = item.SubID.ToString();
                ExpenditureNametb.Text = item.ExpenditureName;
                ExpenditureDetailtb.Text = item.ExpenditureDetail;
                Remarkstb.Text = item.Remarks;
                a_attachement.HRef = item.Attachment;
                if (a_attachement.HRef != string.Empty)
                    a_attachement.InnerText = "查看上次";
                TBManager.Text = item.Manager;
                int i = 0;
                int count = ((List<CompanyInfo>)ViewState["companylist"]).Count;
                foreach (CompanyInfo companyinfo in (List<CompanyInfo>)ViewState["companylist"])
                {
                    Session["inputtext" + companyinfo.CompanyID] = ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[count * Convert.ToInt32(e.CommandArgument) + i].Expenditure;
                    i++;
                }
                InitForm();

                canceledit.Visible = true;
                sendagain.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "settablefocus", "document.getElementById('popcontrolbutton').click();", true);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改开支明细失败", new WebException(ex.ToString()), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }
        }
        if (e.CommandName == "del")
        {
            try
            {
                IList list = (List<BudgetDetailInfo>)Session["BudgetDetaillist"];
                FileUpLoadCommon.DeleteFile(((BudgetDetailInfo)list[Convert.ToInt32(e.CommandArgument)]).Attachment);
                list.RemoveAt(Convert.ToInt32(e.CommandArgument));
                Session["BudgetDetaillist"] = list;

                IList list2 = (List<BudgetDetailInfo>)Session["RealBudgetDetaillist"];
                int count = 0;
                int comcount = Convert.ToInt32(companycount.Value);
                while (count < comcount)
                {
                    list2.RemoveAt(Convert.ToInt32(e.CommandArgument) * comcount);
                    count++;
                }
                Session["RealBudgetDetaillist"] = list2;
                GridView1.DataSource = list;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除开支明细时请避免刷新", new WebException(ex.ToString()), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }
        }

    }

    protected void CancelEdit_Click(object sender, EventArgs e)
    {
        SubIDNametb.Value = "";
        SubIDtb.Value = "";
        ExpenditureNametb.Text = "";
        ExpenditureDetailtb.Text = "";
        Remarkstb.Text = "";
        TBManager.Text = "";
        foreach (CompanyInfo companyinfo in (List<CompanyInfo>)ViewState["companylist"])
        {
            Session.Remove("inputtext" + companyinfo.CompanyID);
        }

        InitForm();

        canceledit.Visible = false;
        sendagain.Visible = false;
        this.PopupControlExtender1.Cancel();
    }


    private string UploadFile(ref bool isSuccess, ref string errorMsg)
    {
        FileUpLoadCommon fc = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + "Attachement/", false);
        isSuccess = fc.SaveFile(FileUpload1tb.PostedFile, false, false);
        if (!isSuccess)
            errorMsg = fc.ErrorMsg;
        return fc.NewFileName;
    }

    protected void AddDetail_Click(object sender, EventArgs e)
    {
        if (canceledit.Visible == false)
        {
            AddSession();
            string attachmentstr = "";
            string errorMsg = "";
            bool isSuccess = false;
            if (FileUpload1tb.FileName != null && FileUpload1tb.FileName != string.Empty)
            {
                string fileUrl = UploadFile(ref isSuccess, ref errorMsg);
                if (fileUrl != "")
                {
                    //if (a_attachement.HRef != string.Empty)
                    //    FileUpLoadCommon.DeleteFile(string.Format("{0}", a_attachement.HRef));
                    attachmentstr = SystemConfig.Instance.UploadPath + "Attachement/" + fileUrl;

                }
                else
                {
                    attachmentstr = fileUrl;
                }

            }
            else
            {
                attachmentstr = "";
                isSuccess = true;
            }
            if (!isSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "上传附件失败", new WebException(errorMsg), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                return;
            }


            foreach (CompanyInfo companyinfo in (List<CompanyInfo>)ViewState["companylist"])
            {
                BudgetDetailInfo realitem = new BudgetDetailInfo();
                realitem.SubName = SubIDNametb.Value;
                realitem.SubID = Convert.ToInt64(SubIDtb.Value);
                realitem.ExpenditureName = ExpenditureNametb.Text;
                //item.Expenditure = Convert.ToDecimal(Expendituretb.Text);
                realitem.Expenditure = (Session["inputtext" + companyinfo.CompanyID] != null && Session["inputtext" + companyinfo.CompanyID].ToString() != "") ? Convert.ToDecimal(Session["inputtext" + companyinfo.CompanyID].ToString()) : decimal.Zero;
                realitem.ExpenditureDetail = ExpenditureDetailtb.Text;
                realitem.Review = "";
                realitem.Manager = TBManager.Text;
                realitem.Remarks = Remarkstb.Text;
                realitem.RecordDate = DateTime.Now;
                realitem.System = "";
                realitem.BudgetApprove = realitem.Expenditure;
                realitem.Year = Convert.ToInt32(Year.Text);
                realitem.Month = Convert.ToInt32(Month.Text);
                realitem.Approvaler = "";
                realitem.Attachment = attachmentstr;
                realitem.CompanyID = companyinfo.CompanyID;
                realitem.CompanyName = companyinfo.CompanyName;
                if (Session["RealBudgetDetaillist"] != null)
                {
                    IList list = (List<BudgetDetailInfo>)Session["RealBudgetDetaillist"];
                    list.Add(realitem);
                    Session["RealBudgetDetaillist"] = list;
                }
                else
                {
                    IList list = new List<BudgetDetailInfo>();
                    list.Add(realitem);
                    Session["RealBudgetDetaillist"] = list;
                }

            }

            BudgetDetailInfo item = new BudgetDetailInfo();
            item.SubName = SubIDNametb.Value;
            item.SubID = Convert.ToInt64(SubIDtb.Value);
            item.ExpenditureName = ExpenditureNametb.Text;
            //item.Expenditure = Convert.ToDecimal(Expendituretb.Text);
            item.Expenditure = decimal.Zero;
            item.ExpenditureDetail = ExpenditureDetailtb.Text;
            item.Review = "";
            item.Manager = TBManager.Text;
            item.Remarks = Remarkstb.Text;
            item.RecordDate = DateTime.Now;
            item.System = "";
            item.BudgetApprove = decimal.Zero;
            item.Year = Convert.ToInt32(Year.Text);
            item.Month = Convert.ToInt32(Month.Text);
            item.Approvaler = "";
            item.Attachment = attachmentstr;
            item.ExpenditureStr = string.Empty;
            item.BudgetApproveStr = string.Empty;
            decimal tempexpenditure = 0;
            foreach (CompanyInfo companyinfo in (List<CompanyInfo>)ViewState["companylist"])
            {
                decimal expendituretemp = (Session["inputtext" + companyinfo.CompanyID] != null && Session["inputtext" + companyinfo.CompanyID].ToString() != "") ? Convert.ToDecimal(Session["inputtext" + companyinfo.CompanyID].ToString()) : decimal.Zero;
                item.ExpenditureStr += companyinfo.CompanyName + ":" + expendituretemp.ToString() + " ";
                tempexpenditure += expendituretemp;
                item.BudgetApproveStr += companyinfo.CompanyName + ":" + expendituretemp.ToString() + " ";
                Session["inputtext" + companyinfo.CompanyID] = "";
            }
            item.ExpenditureStr += "小计:" + tempexpenditure;
            item.BudgetApproveStr += "小计:" + tempexpenditure;

            if (Session["BudgetDetaillist"] != null)
            {
                IList list = (List<BudgetDetailInfo>)Session["BudgetDetaillist"];
                list.Add(item);
                GridView1.DataSource = list;
                GridView1.DataBind();
                Session["BudgetDetaillist"] = list;
            }
            else
            {
                IList list = new List<BudgetDetailInfo>();
                list.Add(item);
                GridView1.DataSource = list;
                GridView1.DataBind();
                Session["BudgetDetaillist"] = list;
            }

            SubIDNametb.Value = "";
            SubIDtb.Value = "";
            ExpenditureNametb.Text = "";
            ExpenditureDetailtb.Text = "";
            Remarkstb.Text = "";
            TBManager.Text = "";

            InitForm();
            canceledit.Visible = false;
            sendagain.Visible = false;
        }
        else
        {
            AddSession();
            string attachmentstr = "";
            string errorMsg = "";
            bool isSuccess = false;
            if (sendagain.Checked == true)
            {

                if (FileUpload1tb.FileName != null && FileUpload1tb.FileName != string.Empty)
                {
                    string fileUrl = UploadFile(ref isSuccess, ref errorMsg);
                    if (fileUrl != "")
                    {
                        if (a_attachement.HRef != string.Empty)
                            FileUpLoadCommon.DeleteFile(string.Format("{0}", a_attachement.HRef));
                        attachmentstr = SystemConfig.Instance.UploadPath + "Attachement/" + fileUrl;

                    }
                    else
                    {
                        attachmentstr = fileUrl;
                    }

                }
                else
                {
                    attachmentstr = "";
                    isSuccess = true;
                }
                if (!isSuccess)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "上传附件失败", new WebException(errorMsg), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                    return;
                }
            }
            else
            {
                attachmentstr = ((List<BudgetDetailInfo>)Session["BudgetDetaillist"])[Convert.ToInt32(inp_index.Value)].Attachment;
            }

            #region 删除原列表项
            IList oldlist = (List<BudgetDetailInfo>)Session["BudgetDetaillist"];
            oldlist.RemoveAt(Convert.ToInt32(inp_index.Value));
            Session["BudgetDetaillist"] = oldlist;

            IList oldlist2 = (List<BudgetDetailInfo>)Session["RealBudgetDetaillist"];
            int count = 0;
            int comcount = Convert.ToInt32(companycount.Value);
            while (count < comcount)
            {
                oldlist2.RemoveAt(Convert.ToInt32(inp_index.Value) * comcount);
                count++;
            }
            Session["RealBudgetDetaillist"] = oldlist2;
            #endregion

            #region 增加修改项

            foreach (CompanyInfo companyinfo in (List<CompanyInfo>)ViewState["companylist"])
            {
                BudgetDetailInfo realitem = new BudgetDetailInfo();
                realitem.SubName = SubIDNametb.Value;
                realitem.SubID = Convert.ToInt64(SubIDtb.Value);
                realitem.ExpenditureName = ExpenditureNametb.Text;
                //item.Expenditure = Convert.ToDecimal(Expendituretb.Text);
                realitem.Expenditure = (Session["inputtext" + companyinfo.CompanyID] != null && Session["inputtext" + companyinfo.CompanyID].ToString() != "") ? Convert.ToDecimal(Session["inputtext" + companyinfo.CompanyID].ToString()) : decimal.Zero;
                realitem.ExpenditureDetail = ExpenditureDetailtb.Text;
                realitem.Review = "";
                realitem.Manager = TBManager.Text;
                realitem.Remarks = Remarkstb.Text;
                realitem.RecordDate = DateTime.Now;
                realitem.System = "";
                realitem.BudgetApprove = realitem.Expenditure;
                realitem.Year = Convert.ToInt32(Year.Text);
                realitem.Month = Convert.ToInt32(Month.Text);
                realitem.Approvaler = "";
                realitem.Attachment = attachmentstr;
                realitem.CompanyID = companyinfo.CompanyID;
                realitem.CompanyName = companyinfo.CompanyName;
                if (Session["RealBudgetDetaillist"] != null)
                {
                    IList list = (List<BudgetDetailInfo>)Session["RealBudgetDetaillist"];
                    list.Add(realitem);
                    Session["RealBudgetDetaillist"] = list;
                }
                else
                {
                    IList list = new List<BudgetDetailInfo>();
                    list.Add(realitem);
                    Session["RealBudgetDetaillist"] = list;
                }

            }

            BudgetDetailInfo item = new BudgetDetailInfo();
            item.SubName = SubIDNametb.Value;
            item.SubID = Convert.ToInt64(SubIDtb.Value);
            item.ExpenditureName = ExpenditureNametb.Text;
            //item.Expenditure = Convert.ToDecimal(Expendituretb.Text);
            item.Expenditure = decimal.Zero;
            item.ExpenditureDetail = ExpenditureDetailtb.Text;
            item.Review = "";
            item.Manager = TBManager.Text;
            item.Remarks = Remarkstb.Text;
            item.RecordDate = DateTime.Now;
            item.System = "";
            item.BudgetApprove = decimal.Zero;
            item.Year = Convert.ToInt32(Year.Text);
            item.Month = Convert.ToInt32(Month.Text);
            item.Approvaler = "";
            item.Attachment = attachmentstr;
            item.ExpenditureStr = string.Empty;
            item.BudgetApproveStr = string.Empty;
            decimal tempexpenditure = 0;
            foreach (CompanyInfo companyinfo in (List<CompanyInfo>)ViewState["companylist"])
            {
                decimal expendituretemp = (Session["inputtext" + companyinfo.CompanyID] != null && Session["inputtext" + companyinfo.CompanyID].ToString() != "") ? Convert.ToDecimal(Session["inputtext" + companyinfo.CompanyID].ToString()) : decimal.Zero;
                item.ExpenditureStr += companyinfo.CompanyName + ":" + expendituretemp.ToString() + " ";
                tempexpenditure += expendituretemp;
                item.BudgetApproveStr += companyinfo.CompanyName + ":" + expendituretemp.ToString() + " ";
                Session["inputtext" + companyinfo.CompanyID] = "";
            }
            item.ExpenditureStr += "小计:" + tempexpenditure;
            item.BudgetApproveStr += "小计:" + tempexpenditure;

            if (Session["BudgetDetaillist"] != null)
            {
                IList list = (List<BudgetDetailInfo>)Session["BudgetDetaillist"];
                list.Add(item);
                GridView1.DataSource = list;
                GridView1.DataBind();
                Session["BudgetDetaillist"] = list;
            }
            else
            {
                IList list = new List<BudgetDetailInfo>();
                list.Add(item);
                GridView1.DataSource = list;
                GridView1.DataBind();
                Session["BudgetDetaillist"] = list;
            }

            SubIDNametb.Value = "";
            SubIDtb.Value = "";
            ExpenditureNametb.Text = "";
            ExpenditureDetailtb.Text = "";
            Remarkstb.Text = "";
            TBManager.Text = "";

            InitForm();
            canceledit.Visible = false;
            sendagain.Visible = false;
            #endregion

        }

    }

    private decimal GetAllSubRealBudget(int year, int month, long SubID,string Title)
    {
        //SubjectRelation bll = new SubjectRelation();
        AnnualBudget budgetbll = new AnnualBudget();
        decimal RealBudget = 0;

        //if (((SubjectRelationInfos)bll.GetSubjectRelation(SubID)).IsLeaf != 1)
        //{
        //    SubjectRelationInfos searchinfo = new SubjectRelationInfos();
        //    searchinfo.ParentID = SubID;
        //    IList subjectlist = (List<SubjectRelationInfos>)bll.Search(searchinfo);
        //    foreach (SubjectRelationInfos item in subjectlist)
        //    {
        //        RealBudget += GetAllSubRealBudget(year, month, item.SubID);
        //    }
        //}
        //else
        //{
        BudgetDetailInfo searchBudgetdetailinfo = new BudgetDetailInfo();
        searchBudgetdetailinfo.Year = year;
        searchBudgetdetailinfo.Month = month;
        searchBudgetdetailinfo.SubID = SubID;
        searchBudgetdetailinfo.Title = Title;
        IList detaillist = (List<BudgetDetailInfo>)budgetbll.Search(searchBudgetdetailinfo);
        foreach (BudgetDetailInfo item in detaillist)
        {
            RealBudget += item.RealExpenditure;
        }

        //}
        return RealBudget;

    }


}
