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
using WebUtility.WebControls;
using WebUtility.Components;
using FM2E.BLL.Budget;
using FM2E.Model.Budget;
using FM2E.Model.Utils;
using System.Collections.Generic;
using FM2E.Model.Exceptions;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.WorkflowLayer;

public partial class Module_FM2E_BudgetManager_MonthlyBudgetManager_ViewMonthlyBudget : System.Web.UI.Page
{
    string companyid = UserData.CurrentUserData.CompanyID;
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 255, 0, DataType.Long);
    bool inittreeyet = true;
    protected long YearID;

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            Company companybll = new Company();
            CompanyInfo companyinfo = new CompanyInfo();
            //companyinfo.IsParentCompany = false;
            IList companylist = (List<CompanyInfo>)companybll.Search(companyinfo);
            companycount.Value = companylist.Count.ToString();
            ButtonBind();

            if (YearID == 0)
            {
                AnnualBudget annualbll = new AnnualBudget();
                BudgetPerMonthTotalInfo budgetyearinfo = ((BudgetPerMonthTotalInfo)annualbll.GetBudgetPerMonthTotal(id));
                if (budgetyearinfo != null)
                    YearID = budgetyearinfo.BudgetYearID;
            }
            inittreeyet = false;
            AddTree(0, (TreeNode)null);
            TreeView1.ShowLines = true;
            BudgetApplTotalydiv.InnerHtml = "";
            TotalExpenditurediv.InnerHtml = "";
            NonPaymentdiv.InnerHtml = "";
            BudgetPermonthdiv.InnerHtml = "";
            Totaldiv.InnerHtml = "";
            SurplusExpenditurediv.InnerHtml = "";
            GetMonthlySessionFromDatabase();
            staticsfunction(TreeView1.Nodes);
            staticstotal();
            AddTextBox(TreeView1.Nodes);
            inittreeyet = true;


            FillData();


        }
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
    private void FillData()
    {
        try
        {
            AnnualBudget bll = new AnnualBudget();
            BudgetDetailInfo budgetdetailinfo = new BudgetDetailInfo();
            budgetdetailinfo.TotalID = id;
            IList list = (List<BudgetDetailInfo>)bll.Search(budgetdetailinfo);
            GridView1.DataSource = convertfromreallist(list);
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取开支明细列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
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



    private void ButtonBind()
    {
        HeadMenuButtonItem button1 = HeadMenuWebControls1.ButtonList[1];
        button1.ButtonUrl = "MakeMonthlyBudget.aspx?cmd=edit&id=" + id;

        HeadMenuButtonItem button2 = HeadMenuWebControls1.ButtonList[2];
        button2.ButtonUrlType = UrlType.JavaScript;
        button2.ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');", id);

        HeadMenuButtonItem button3 = HeadMenuWebControls1.ButtonList[3];
        button3.ButtonUrl = "ShowChart.aspx?MonthlyID=" + id +"&workflowname=" + BudgetMonthlyWorkflow.WorkflowName;


        if (cmd == "delete")
        {
            bool bSuccess = false; 
            AnnualBudget bll = new AnnualBudget();
            if(((BudgetPerMonthTotalInfo)bll.GetBudgetPerMonthTotal(id)).Status >= 2)
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "月算已经提交不能删除", new WebException("月算已经提交不能删除"), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            try
            {

                bll.DelBudgetPerMonthTotal(id);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess == true)
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除本月预算所有信息成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("MonthlyBudget.aspx"), UrlType.Href, "");

        }

    }

    public void AddTree(long ParentID, TreeNode pNode)
    {
        SubjectPerYear subjectperyearinfo = new SubjectPerYear();
        subjectperyearinfo.ParentID = ParentID;
        //AnnualBudget annualbll = new AnnualBudget();
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
                    BudgetApply = Session[nodetemp.Value + "BudgetApply"].ToString();
                if (Session[nodetemp.Value + "TotalExpenditure"] != null)
                    TotalExpenditure = Session[nodetemp.Value + "TotalExpenditure"].ToString();
                if (Session[nodetemp.Value + "NonPayment"] != null)
                    NonPayment = Session[nodetemp.Value + "NonPayment"].ToString();
                if (Session[nodetemp.Value + "BudgetPermonth"] != null)
                    BudgetPermonth = Session[nodetemp.Value + "BudgetPermonth"].ToString();
                if (Session[nodetemp.Value + "Total"] != null)
                    Total = Session[nodetemp.Value + "Total"].ToString();
                if (Session[nodetemp.Value + "SurplusExpenditure"] != null)
                    SurplusExpenditure = Session[nodetemp.Value + "SurplusExpenditure"].ToString();
                if (nodetemp.ChildNodes.Count > 0)
                {
                    BudgetApplTotalydiv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  size='14' id='" + nodetemp.Value + "BudgetApply' style='display:" + showornot + "' runat='server' value='" + BudgetApply + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    TotalExpenditurediv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  size='14' id='" + nodetemp.Value + "TotalExpenditure' style='display:" + showornot + "' runat='server' value='" + TotalExpenditure + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    NonPaymentdiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "NonPayment' style='display:" + showornot + "' runat='server' value='" + NonPayment + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    BudgetPermonthdiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "BudgetPermonth' style='display:" + showornot + "' runat='server' value='" + BudgetPermonth + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    Totaldiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "Total' style='display:" + showornot + "' runat='server' value='" + Total + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    SurplusExpenditurediv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  size='14' id='" + nodetemp.Value + "SurplusExpenditure' style='display:" + showornot + "' runat='server' value='" + SurplusExpenditure + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                }
                else
                {
                    BudgetApplTotalydiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "BudgetApply' style='display:" + showornot + "' runat='server' value='" + BudgetApply + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    TotalExpenditurediv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "TotalExpenditure' style='display:" + showornot + "' runat='server' value='" + TotalExpenditure + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    NonPaymentdiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "NonPayment' style='display:" + showornot + "' runat='server' value='" + NonPayment + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    BudgetPermonthdiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "BudgetPermonth' style='display:" + showornot + "' runat='server' value='" + BudgetPermonth + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    Totaldiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "Total' style='display:" + showornot + "' runat='server' value='" + Total + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    SurplusExpenditurediv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  size='14' id='" + nodetemp.Value + "SurplusExpenditure' style='display:" + showornot + "' runat='server' value='" + SurplusExpenditure + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                }
            }
            else
            {
                if (Session[nodetemp.Value + "BudgetApply"] != null)
                    BudgetApply = Session[nodetemp.Value + "BudgetApply"].ToString();
                if (Session[nodetemp.Value + "TotalExpenditure"] != null)
                    TotalExpenditure = Session[nodetemp.Value + "TotalExpenditure"].ToString();
                if (Session[nodetemp.Value + "NonPayment"] != null)
                    NonPayment = Session[nodetemp.Value + "NonPayment"].ToString();
                if (Session[nodetemp.Value + "BudgetPermonth"] != null)
                    BudgetPermonth = Session[nodetemp.Value + "BudgetPermonth"].ToString();
                if (Session[nodetemp.Value + "Total"] != null)
                    Total = Session[nodetemp.Value + "Total"].ToString();
                if (Session[nodetemp.Value + "SurplusExpenditure"] != null)
                    SurplusExpenditure = Session[nodetemp.Value + "SurplusExpenditure"].ToString();
                if (nodetemp.ChildNodes.Count > 0)
                {
                    BudgetApplTotalydiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "BudgetApply' style='display:block' runat='server' value='" + BudgetApply + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    TotalExpenditurediv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "TotalExpenditure' style='display:block' runat='server' value='" + TotalExpenditure + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    NonPaymentdiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "NonPayment' style='display:block' runat='server' value='" + NonPayment + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    BudgetPermonthdiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "BudgetPermonth' style='display:block' runat='server' value='" + BudgetPermonth + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    Totaldiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "Total' style='display:block' runat='server' value='" + Total + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                    SurplusExpenditurediv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  size='14' id='" + nodetemp.Value + "SurplusExpenditure' style='display:block' runat='server' value='" + SurplusExpenditure + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                }
                else
                {
                    BudgetApplTotalydiv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  size='14' id='" + nodetemp.Value + "BudgetApply' style='display:block' runat='server' value='" + BudgetApply + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    TotalExpenditurediv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  size='14' id='" + nodetemp.Value + "TotalExpenditure' style='display:block' runat='server' value='" + TotalExpenditure + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    NonPaymentdiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "NonPayment' style='display:block' runat='server' value='" + NonPayment + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    BudgetPermonthdiv.InnerHtml += "<input type='text' size='14' title='" + nodetemp.Text + "'  id='" + nodetemp.Value + "BudgetPermonth' style='display:block' runat='server' value='" + BudgetPermonth + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    Totaldiv.InnerHtml += "<input type='text' size='14'  title='" + nodetemp.Text + "' id='" + nodetemp.Value + "Total' style='display:block' runat='server' value='" + Total + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                    SurplusExpenditurediv.InnerHtml += "<input type='text' title='" + nodetemp.Text + "'  size='14' id='" + nodetemp.Value + "SurplusExpenditure' style='display:block' runat='server' value='" + SurplusExpenditure + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly'/>";
                }
            }
            AddTextBox(nodetemp.ChildNodes);
        }

    }

    public void TreeView1_OnTreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {
        //AddSession();
        BudgetApplTotalydiv.InnerHtml = "";
        TotalExpenditurediv.InnerHtml = "";
        NonPaymentdiv.InnerHtml = "";
        BudgetPermonthdiv.InnerHtml = "";
        Totaldiv.InnerHtml = "";
        SurplusExpenditurediv.InnerHtml = "";
        AddTextBox(TreeView1.Nodes);
    }
    /// <summary>
    /// 树展开事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void TreeView1_OnTreeNodeCollapsed(object sender, TreeNodeEventArgs e)
    {
        //AddSession();
        if(inittreeyet)
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

    public void TreeView1_OnSelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            IList list = new List<BudgetDetailInfo>();
            GetBudgetDetailByNodeSelect(TreeView1.SelectedNode, list);
            GridView1.DataSource = convertfromreallist(list);
            GridView1.DataBind();
            string str = TreeView1.SelectedNode.ToolTip;
            TreeView1.SelectedNode.Selected = false;
            ScriptManager.RegisterClientScriptBlock(TreeView1, this.GetType(), "click", "setslectrowcolor('" + str + "');", true); 
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取开支明细列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

    }
    /// <summary>
    /// 获取单击树时所对应节点的所有开支明细
    /// </summary>
    private void GetBudgetDetailByNodeSelect(TreeNode node, IList list)
    {
        if (node.ChildNodes.Count > 0)
        {
            foreach (TreeNode subnode in node.ChildNodes)
            {
                GetBudgetDetailByNodeSelect(subnode, list);
            }
        }
        else
        {
            AnnualBudget bll = new AnnualBudget();
            BudgetDetailInfo budgetdetailinfo = new BudgetDetailInfo();
            budgetdetailinfo.TotalID = id;
            budgetdetailinfo.SubID = Convert.ToInt64(node.Value);
            IList listtemp = (List<BudgetDetailInfo>)bll.Search(budgetdetailinfo);
            foreach (BudgetDetailInfo item in listtemp)
            {
                list.Add(item);
            }
        }
    }

    /// <summary>
    /// 从数据库中获取预算金额到Session中
    /// </summary>
    private void GetMonthlySessionFromDatabase()
    {
        AnnualBudget bll = new AnnualBudget();

        BudgetPerMonthTotalInfo budgetpermonthtotalinfo = bll.GetBudgetPerMonthTotal(id);
        if (budgetpermonthtotalinfo == null)
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "你所查看的月度预算已不存在", new WebException("你所查看的月度预算已不存在"), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        Year.Value = budgetpermonthtotalinfo.Year.ToString();
        Month.Value = budgetpermonthtotalinfo.Month.ToString();
        INPTitle.Value = budgetpermonthtotalinfo.Title;

        BudgetPermonthInfo budgetpermonthinfo = new BudgetPermonthInfo();
        budgetpermonthinfo.TotalID = id;

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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";
            BudgetDetailInfo dv = (BudgetDetailInfo)e.Row.DataItem;
            e.Row.Attributes["DetailID"] = dv.DetailID.ToString();
        }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "view")
        {
            string DetailID = gvRow.Attributes["DetailID"];
            Response.Redirect("ViewDetailBudget.aspx?cmd=view&id=" + DetailID);
        }
        if (e.CommandName == "del")
        {
            try
            {
                long id = Convert.ToInt64(gvRow.Attributes["DetailID"]);
                AnnualBudget bll = new AnnualBudget();
                bll.DelBudgetDetail(id);
                FillData();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }

    }

    protected void showallexpenditure_click(object sender, EventArgs e)
    {
        FillData();
    }
}
