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
using FM2E.Model.Budget;
using FM2E.BLL.Budget;
using FM2E.Model.Utils;
using WebUtility;
using System.Collections.Generic;
using WebUtility.Components;
using FM2E.Model.Exceptions;
using FM2E.Model.Basic;
using FM2E.BLL.Basic;
using WebUtility.WebControls;
using FM2E.WorkflowLayer;

public partial class Module_FM2E_BudgetManager_AnnualBudgetApprovalManager_MakeAnnualBudget : System.Web.UI.Page
{
    string companyid = UserData.CurrentUserData.CompanyID;
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 255, 0, DataType.Long);
    public int companycount = 0;
    bool inittreeyet = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            CheckEditable(id);
            Company companybll = new Company();
            CompanyInfo companyinfo = new CompanyInfo();
            
            //companyinfo.IsParentCompany = false;
            IList companylist = (List<CompanyInfo>)companybll.Search(companyinfo);
            ViewState["companylist"] = companylist;
            string workflowstate = "";
           
            inittreeyet = false;
            AddTree(0, (TreeNode)null);
            TreeView1.ShowLines = true;
            GetSessionFromDatabase(ref workflowstate);
            foreach (CompanyInfo companyitem in (List<CompanyInfo>)ViewState["companylist"])
            {
                staticsfunction(TreeView1.Nodes, companyitem.CompanyID);
            }
            staticstotal();
            staticsallcompanytotal(TreeView1.Nodes);
            AddForm();
            inittreeyet = true;

            WorkFlowUserSelectControl1.EventIDField = "Name";  //不用变
            WorkFlowUserSelectControl1.EventNameField = "Description";//不用变
            WorkFlowUserSelectControl1.WorkFlowState = workflowstate;//当前表单的状态，新增的话为初始状态，旧的话，可以通过获取的表单model获得
            WorkFlowUserSelectControl1.WorkFlowName = BudgetYearWorkflow.WorkflowName;//对应工作流的名称
            WorkFlowUserSelectControl1.EventListDataSource = WorkflowHelper.GetEventInfoList(BudgetYearWorkflow.WorkflowName, workflowstate);
            //显示可以选择的事件列表
            WorkFlowUserSelectControl1.EventListDataBind();
            WorkFlowUserSelectControl1.ShowCompanySelect = true;//是否需要选择公司
            WorkFlowUserSelectControl1.SelectedCompanyID = UserData.CurrentUserData.CompanyID;//当前用户公司，不用变
            WorkFlowUserSelectControl1.SelectedDepartmentID = UserData.CurrentUserData.DepartmentID;//当前用户部门，不用变

            /*********以下几行为需要选择具体用户的一些事件，例如提交审批********/
            WorkFlowUserSelectControl1.AddShowSelectDivValue(BudgetYearWorkflow.SubmitNewEvent);
            WorkFlowUserSelectControl1.AddShowSelectDivValue(BudgetYearWorkflow.SubmitDraftEvent);
            WorkFlowUserSelectControl1.AddShowSelectDivValue(BudgetYearWorkflow.SubmitFinanceApprovalEvent);
            WorkFlowUserSelectControl1.AddShowSelectDivValue(BudgetYearWorkflow.SubmitLeaderApprovalEvent);
            WorkFlowUserSelectControl1.AddShowSelectDivValue(BudgetYearWorkflow.LeaderReturntoFinanceEvent);
            WorkFlowUserSelectControl1.AddShowSelectDivValue(BudgetYearWorkflow.CompanyReturntoLeaderEvent);
        }
        if (((HtmlInputHidden)WorkFlowUserSelectControl1.FindControl("clickyet")).Value == "true")
        {
            AddSession();
            AddForm();
            ((HtmlInputHidden)WorkFlowUserSelectControl1.FindControl("clickyet")).Value = "false";
        }
    }

    private void CheckEditable(long id)
    {

        AnnualBudget bll = new AnnualBudget();
        BudgetYearInfo budgetyearinfo = bll.GetBudgetYear(id);
        if (budgetyearinfo != null && budgetyearinfo.WorkFlowStateName != "Wait4FinanceApproval" && budgetyearinfo.WorkFlowStateName != "Wait4LeaderApproval" && budgetyearinfo.WorkFlowStateName != "Wait4CompanyApproval")
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "本年度预算处于" + budgetyearinfo.WorkFlowStateDescription + "状态，不能再编辑！", new WebException("本年度预算处于" + budgetyearinfo.WorkFlowStateDescription + "状态，不能再编辑！"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        //if (budgetyearinfo != null && budgetyearinfo.Status == 2)
            //EventMessage.MessageBox(Msg_Type.Error, "操作失败", "本年度预算已经提交到审批，不能再编辑！", new WebException("本年度预算已经提交到审批，不能再编辑！"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        if (budgetyearinfo != null)
        {
            Year.Value = budgetyearinfo.Year.ToString();
            IPUTTitle.Value = budgetyearinfo.Title;
        }
        else Year.Value = DateTime.Now.Year.ToString();
    }

    private void AddForm()
    {
        if (TR_company.Cells.Count == 1)
        {
            //增加标题
            HtmlTableCell celltotal = new HtmlTableCell();
            HtmlGenericControl celllabel = new HtmlGenericControl();
            celllabel.InnerText = "总申报数";
            celllabel.Attributes["style"] = "position:relative; cursor:e-resize;";
            //celllabel.Attributes["onmousedown"] = "MouseDownToResize(this);";
            //celllabel.Attributes["onmousemove"] = "MouseMoveToResize(this);";
            //celllabel.Attributes["onmouseup"] = "MouseUpToResize(this);";

            celltotal.Controls.Add(celllabel);
            TR_company.Cells.Add(celltotal);
            //增加输入内容
            HtmlTableCell celltotalcontent = new HtmlTableCell();
            AddTextBox(TreeView1.Nodes, "allcompanytotal", celltotalcontent,false);

            TR_content.Cells.Add(celltotalcontent);
            //增加合计内容
            HtmlTableCell celltaticstoal = new HtmlTableCell();
            HtmlInputText inputtext = new HtmlInputText();
            inputtext.Size = 14;
            inputtext.Attributes["id"] = "allcompanytotal,TotalBudget";
            inputtext.Attributes["style"] = "display:block;background:#d0d0d0;";
            if (Session["allcompanytotal,TotalBudget"] != null)
                inputtext.Value = Session["allcompanytotal,TotalBudget"].ToString();
            inputtext.Attributes["onblur"] = "javascript:AddSession(this.id,this.value);";
            inputtext.Attributes["readonly"] = "readonly";
            celltaticstoal.Controls.Add(inputtext);

            TR_total.Cells.Add(celltaticstoal);

            //增加标题
            HtmlTableCell celltotal2 = new HtmlTableCell();
            HtmlGenericControl celllabel2 = new HtmlGenericControl();
            celllabel2.InnerText = "总审批数";
            celllabel2.Attributes["style"] = "position:relative; cursor:e-resize;";
            //celllabel2.Attributes["onmousedown"] = "MouseDownToResize(this);";
            //celllabel2.Attributes["onmousemove"] = "MouseMoveToResize(this);";
            //celllabel2.Attributes["onmouseup"] = "MouseUpToResize(this);";

            celltotal2.Controls.Add(celllabel2);
            TR_company.Cells.Add(celltotal2);
            //增加输入内容
            HtmlTableCell celltotalcontent2 = new HtmlTableCell();
            AddTextBox(TreeView1.Nodes, "approvalallcompanytotal", celltotalcontent2,false);

            TR_content.Cells.Add(celltotalcontent2);
            //增加合计内容
            HtmlTableCell celltaticstoal2 = new HtmlTableCell();
            HtmlInputText inputtext2 = new HtmlInputText();
            inputtext2.Size = 14;
            inputtext2.Attributes["id"] = "approvalallcompanytotal,TotalBudget";
            inputtext2.Attributes["style"] = "display:block;background:#d0d0d0;";
            if (Session["approvalallcompanytotal,TotalBudget"] != null)
                inputtext2.Value = Session["approvalallcompanytotal,TotalBudget"].ToString();
            inputtext2.Attributes["onblur"] = "javascript:AddSession(this.id,this.value);";
            inputtext2.Attributes["readonly"] = "readonly";
            celltaticstoal2.Controls.Add(inputtext2);

            TR_total.Cells.Add(celltaticstoal2);

            foreach (CompanyInfo companyinfo in (List<CompanyInfo>)ViewState["companylist"])
            {
                //增加标题
                HtmlTableCell cell = new HtmlTableCell();
                HtmlGenericControl cellcompanylabel = new HtmlGenericControl();
                cellcompanylabel.InnerText = companyinfo.CompanyName;
                cellcompanylabel.Attributes["style"] = "position:relative; cursor:e-resize;";
                //cellcompanylabel.Attributes["onmousedown"] = "MouseDownToResize(this);";
                //cellcompanylabel.Attributes["onmousemove"] = "MouseMoveToResize(this);";
                //cellcompanylabel.Attributes["onmouseup"] = "MouseUpToResize(this);";

                cell.Controls.Add(cellcompanylabel);
                TR_company.Cells.Add(cell);
                //增加输入内容
                HtmlTableCell cellcontent = new HtmlTableCell();
                string innerstr = string.Empty;
                AddTextBox(TreeView1.Nodes, companyinfo.CompanyID, cellcontent,false);

                TR_content.Cells.Add(cellcontent);
                //增加合计内容
                HtmlTableCell cellcompanytotal = new HtmlTableCell();
                HtmlInputText companyinputtext = new HtmlInputText();
                companyinputtext.Size = 14;
                companyinputtext.Attributes["id"] = companyinfo.CompanyID + ",TotalBudget";
                companyinputtext.Attributes["style"] = "display:block;background:#d0d0d0;";
                if (Session[companyinfo.CompanyID + ",TotalBudget"] != null)
                    companyinputtext.Value = Session[companyinfo.CompanyID + ",TotalBudget"].ToString();
                companyinputtext.Attributes["onblur"] = "javascript:AddSession(this.id,this.value);";
                companyinputtext.Attributes["readonly"] = "readonly";
                cellcompanytotal.Controls.Add(companyinputtext);
                TR_total.Cells.Add(cellcompanytotal);

                //增加标题
                HtmlTableCell cell2 = new HtmlTableCell();
                HtmlGenericControl cellcompanylabel2 = new HtmlGenericControl();
                cellcompanylabel2.InnerText = companyinfo.CompanyName + "审批数";
                cellcompanylabel2.Attributes["style"] = "position:relative; cursor:e-resize;";
                //cellcompanylabel2.Attributes["onmousedown"] = "MouseDownToResize(this);";
                //cellcompanylabel2.Attributes["onmousemove"] = "MouseMoveToResize(this);";
                //cellcompanylabel2.Attributes["onmouseup"] = "MouseUpToResize(this);";

                cell2.Controls.Add(cellcompanylabel2);
                TR_company.Cells.Add(cell2);
                //增加输入内容
                HtmlTableCell cellcontent2 = new HtmlTableCell();
                AddTextBox(TreeView1.Nodes, "approval" + companyinfo.CompanyID, cellcontent2,true);

                TR_content.Cells.Add(cellcontent2);
                //增加合计内容
                HtmlTableCell cellcompanytotal2 = new HtmlTableCell();
                HtmlInputText companyinputtext2 = new HtmlInputText();
                companyinputtext2.Size = 14;
                companyinputtext2.Attributes["id"] = "approval" + companyinfo.CompanyID + ",TotalBudget";
                companyinputtext2.Attributes["style"] = "display:block;background:#d0d0d0;";
                if (Session["approval" + companyinfo.CompanyID + ",TotalBudget"] != null)
                    companyinputtext2.Value = Session["approval" + companyinfo.CompanyID + ",TotalBudget"].ToString();
                companyinputtext2.Attributes["onblur"] = "javascript:AddSession(this.id,this.value);";
                companyinputtext2.Attributes["readonly"] = "readonly";
                cellcompanytotal2.Controls.Add(companyinputtext2);
                TR_total.Cells.Add(cellcompanytotal2);

            }
        }
        companycount = TR_company.Cells.Count;

    }


    public void AddTree(long ParentID, TreeNode pNode)
    {
        IList nodelist = null;
        if (cmd == "add")
        {
            SubjectRelationInfos subjectrelationinfo = new SubjectRelationInfos();
            subjectrelationinfo.ParentID = ParentID;
            SubjectRelation bll = new SubjectRelation();
            QueryParam qp = bll.GenerateSearchTerm(subjectrelationinfo);
            int recordcount = 0;
             nodelist = bll.GetList(qp, out recordcount, companyid);

        }
        if (cmd == "edit")
        {
            SubjectPerYear subjectperyearinfo = new SubjectPerYear();
            subjectperyearinfo.ParentID = ParentID;
            subjectperyearinfo.Year = id;
            SubjectRelation bll = new SubjectRelation();
            QueryParam qp = bll.GenerateSearchTermByYear(subjectperyearinfo);
            int recordcount = 0;
             nodelist = bll.GetListByYear(qp, out recordcount, companyid);
        }
        

        
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
    /// 动态生成输入框
    /// </summary>
    /// <param name="nodes"></param>
    private void AddTextBox(TreeNodeCollection nodes, string companyid, HtmlTableCell cell,bool editable)
    {
        foreach (TreeNode nodetemp in nodes)
        {
            string textvalue = "";
            if (nodetemp.Parent != null)
            {
                string showornot = (nodetemp.Parent.Expanded == true) ? "block" : "none";
                if (showornot == "none")
                    nodetemp.Expanded = false;
                if (Session[companyid + "," + nodetemp.Value] != null)
                    textvalue = Session[companyid + "," + nodetemp.Value].ToString();
                if (nodetemp.ChildNodes.Count > 0)
                {
                    HtmlInputText inputtext = new HtmlInputText();
                    inputtext.Size = 14;
                    inputtext.Attributes["id"] = companyid + "," + nodetemp.Value;
                    inputtext.Attributes["style"] = "background:#d0d0d0;display:" + showornot;
                    inputtext.Value = textvalue;
                    inputtext.Attributes["title"] = nodetemp.Text; 
                    inputtext.Attributes["onblur"] = "javascript:AddSession(this.id,this.value);";
                    inputtext.Attributes["readonly"] = "readonly";
                    cell.Controls.Add(inputtext);
                    //inputrowstr += "<input type='text' size='14' id='" + companyid + "," + nodetemp.Value + "' style='display:" + showornot + "' runat='server' value='" + textvalue + "' onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                }
                else
                {
                    HtmlInputText inputtext = new HtmlInputText();
                    inputtext.Size = 14;
                    inputtext.Attributes["id"] = companyid + "," + nodetemp.Value;
                    inputtext.Attributes["style"] = "display:" + showornot;
                    inputtext.Attributes["title"] = nodetemp.Text; 
                    inputtext.Value = textvalue;
                    inputtext.Attributes["onblur"] = "javascript:AddSession(this.id,this.value);";
                    if(!editable)
                        inputtext.Attributes["readonly"] = "readonly";
                    cell.Controls.Add(inputtext);
                }
                //inputrowstr += "<input type='text' size='14' id='" + companyid + "," + nodetemp.Value + "' style='display:" + showornot + "' runat='server' value='" + textvalue + "' onblur='javascript:AddSession(this.id,this.value);' />";

            }
            else
            {
                if (Session[companyid + "," + nodetemp.Value] != null)
                    textvalue = Session[companyid + "," + nodetemp.Value].ToString();
                if (nodetemp.ChildNodes.Count > 0)
                {
                    HtmlInputText inputtext = new HtmlInputText();
                    inputtext.Size = 14;
                    inputtext.Attributes["id"] = companyid + "," + nodetemp.Value;
                    inputtext.Attributes["style"] = "background:#d0d0d0;display:block";
                    inputtext.Attributes["title"] = nodetemp.Text;
                    inputtext.Value = textvalue;
                    inputtext.Attributes["onblur"] = "javascript:AddSession(this.id,this.value);";
                    inputtext.Attributes["readonly"] = "readonly";
                    cell.Controls.Add(inputtext);
                }
                //inputrowstr += "<input type='text' size='14' id='" + companyid + "," + nodetemp.Value + "' style='display:block' runat='server' value='" + textvalue + "'onblur='javascript:AddSession(this.id,this.value);' readonly='readonly' />";
                else
                {
                    HtmlInputText inputtext = new HtmlInputText();
                    inputtext.Size = 14;
                    inputtext.Attributes["id"] = companyid + "," + nodetemp.Value;
                    inputtext.Attributes["style"] = "display:block";
                    inputtext.Value = textvalue;
                    inputtext.Attributes["title"] = nodetemp.Text;
                    inputtext.Attributes["onblur"] = "javascript:AddSession(this.id,this.value);";
                    if (!editable)
                        inputtext.Attributes["readonly"] = "readonly";
                    cell.Controls.Add(inputtext);
                }
                //inputrowstr += "<input type='text' size='14' id='" + companyid + "," + nodetemp.Value + "' style='display:block' runat='server' value='" + textvalue + "'onblur='javascript:AddSession(this.id,this.value);' />";

            }
            AddTextBox(nodetemp.ChildNodes, companyid, cell,editable);
        }


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
            AddForm();
        //GetSessionFromDatabase();
        //inputrow.InnerHtml = "";
        //approvaldiv.InnerHtml = "";
        //AddTextBox(TreeView1.Nodes);
        //SubjectRelationInfos searchinfo = new SubjectRelationInfos();
        //searchinfo.ParentID = Convert.ToInt64(e.Node.Value);
        //SubjectRelation bll = new SubjectRelation();
        //IList<SubjectRelationInfos> subnodes = bll.Search(searchinfo);
        //foreach (SubjectRelationInfos subnodetemp in subnodes)
        //{
        //    inputrow.InnerHtml = inputrow.InnerHtml.Replace("id='int" + subnodetemp.SubID.ToString() + "' style='display:none'", "id='int" + subnodetemp.SubID.ToString() + "' style='display:block'");
        //}
    }
    /// <summary>
    /// 树展开事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void TreeView1_OnTreeNodeCollapsed(object sender, TreeNodeEventArgs e)
    {
        AddSession();
        AddForm();
        //GetSessionFromDatabase();
        //inputrow.InnerHtml = "";
        //approvaldiv.InnerHtml = "";
        //AddTextBox(TreeView1.Nodes);
    }
    /// <summary>
    /// 用于统计的函数
    /// </summary>
    /// <param name="nodes">树节点</param>
    private void staticsfunction(TreeNodeCollection nodes, string companyid)
    {
        foreach (TreeNode nodetemp in nodes)
        {
            staticsfunction(nodetemp.ChildNodes, companyid);
            if (nodetemp.ChildNodes.Count > 0)
            {
                decimal total = 0;
                decimal approvaltotal = 0;
                foreach (TreeNode subnodetemp in nodetemp.ChildNodes)
                {
                    if (Session[companyid + "," + subnodetemp.Value] != null)
                        total += Convert.ToDecimal(Session[companyid + "," + subnodetemp.Value]);
                    if (Session["approval" + companyid + "," + subnodetemp.Value] != null)
                        approvaltotal += Convert.ToDecimal(Session["approval" + companyid + "," + subnodetemp.Value]);
                }
                Session[companyid + "," + nodetemp.Value] = total;
                Session["approval" + companyid + "," + nodetemp.Value] = approvaltotal;
            }
        }
    }
    private void staticstotal()
    {
        decimal allcompanytotal = 0;
        decimal allcompanyapprovaltotal = 0;
        foreach (CompanyInfo companyinfo in (List<CompanyInfo>)ViewState["companylist"])
        {
            decimal total = 0;
            decimal approvaltotal = 0;
            foreach (TreeNode nodetemp in TreeView1.Nodes)
            {
                if (Session[companyinfo.CompanyID + "," + nodetemp.Value] != null)
                    total += Convert.ToDecimal(Session[companyinfo.CompanyID + "," + nodetemp.Value]);
                if (Session["approval" + companyinfo.CompanyID + "," + nodetemp.Value] != null)
                    approvaltotal += Convert.ToDecimal(Session["approval" + companyinfo.CompanyID + "," + nodetemp.Value]);
            }
            Session[companyinfo.CompanyID + "," + "TotalBudget"] = total;
            allcompanytotal += total;
            Session["approval" + companyinfo.CompanyID + "," + "TotalBudget"] = approvaltotal;
            allcompanyapprovaltotal += approvaltotal;
        }
        Session["allcompanytotal,TotalBudget"] = allcompanytotal;
        Session["approvalallcompanytotal,TotalBudget"] = allcompanyapprovaltotal;
    }
    /// <summary>
    /// 从数据库中获取预算金额到Session中
    /// </summary>
    private void GetSessionFromDatabase(ref string workflowstate)
    {
        //AnnualBudget bll = new AnnualBudget();
        //BudgetYearInfo budgetyeatinfo = bll.GetBudgetYear(id);
        //Remark.Text = budgetyeatinfo.Remark;
        //Year.Value = budgetyeatinfo.Year.ToString();
        //BudgetYearDetailInfo budgetyeardetailinfo = new BudgetYearDetailInfo();
        //budgetyeardetailinfo.BudgetYearID = id;
        //IList<BudgetYearDetailInfo> list = bll.Search(budgetyeardetailinfo);

        //foreach (BudgetYearDetailInfo item in list)
        //{
        //    Session[item.SubID.ToString()] = item.BudgetApply;
        //    Session[item.SubID.ToString() + "approvaldiv"] = item.BudgetApprove;
        //}
        AnnualBudget bll = new AnnualBudget();
        BudgetYearInfo budgetyeatinfo = bll.GetBudgetYear(id);
        Remark.Value = budgetyeatinfo.Remark;
        Year.Value = budgetyeatinfo.Year.ToString();
        IPUTTitle.Value = budgetyeatinfo.Title;
        workflowstate = budgetyeatinfo.WorkFlowStateName;
        ViewState["WorkFlowInstanceID"] = budgetyeatinfo.WorkFlowInstanceID;
        ViewState["Maker"] = budgetyeatinfo.Maker;
        BudgetYearDetailInfo budgetyeardetailinfo = new BudgetYearDetailInfo();
        budgetyeardetailinfo.BudgetYearID = id;
        IList<BudgetYearDetailInfo> list = bll.Search(budgetyeardetailinfo);

        foreach (BudgetYearDetailInfo item in list)
        {
            Session[item.CompanyID + "," + item.SubID.ToString()] = item.BudgetApply;
            Session["approval" + item.CompanyID + "," + item.SubID.ToString()] = item.BudgetApprove;
        }

    }

    private void staticsallcompanytotal(TreeNodeCollection nodes)
    {
        foreach (TreeNode nodetemp in nodes)
        {
            staticsallcompanytotal(nodetemp.ChildNodes);

            decimal allcompanytotal = 0;
            decimal allcompanyapprovaltotal = 0;
            foreach (CompanyInfo companyinfo in (List<CompanyInfo>)ViewState["companylist"])
            {
                if (Session[companyinfo.CompanyID + "," + nodetemp.Value] != null)
                    allcompanytotal += Convert.ToDecimal(Session[companyinfo.CompanyID + "," + nodetemp.Value]);
                if (Session["approval" + companyinfo.CompanyID + "," + nodetemp.Value] != null)
                    allcompanyapprovaltotal += Convert.ToDecimal(Session["approval" + companyinfo.CompanyID + "," + nodetemp.Value]);
            }
            Session["allcompanytotal," + nodetemp.Value] = allcompanytotal;
            Session["approvalallcompanytotal," + nodetemp.Value] = allcompanyapprovaltotal;

        }
    }

    //protected void SaveAsTemp_Click(object sender, EventArgs e)
    //{
    //    //if(cmd == "add")
    //    AddSession();
    //    foreach (CompanyInfo companyinfo in (List<CompanyInfo>)ViewState["companylist"])
    //    {
    //        staticsfunction(TreeView1.Nodes, companyinfo.CompanyID);
    //    }

    //    staticstotal();
    //    staticsallcompanytotal(TreeView1.Nodes);
    //    //staticsfunction(TreeView1.Nodes);
    //    //staticstotal();
    //    long newBudgetYaerid = id;
    //    if (cmd == "add")
    //    {
    //        BudgetYearInfo budgetyear = new BudgetYearInfo();
    //        try
    //        {
    //            budgetyear.Title = IPUTTitle.Value;
    //            budgetyear.CompanyID = companyid;
    //            budgetyear.Year = Convert.ToInt32(Year.Value);
    //            budgetyear.Maker = UserData.CurrentUserData.PersonName;
    //            budgetyear.MakeTime = DateTime.Now;
    //            budgetyear.Status = Convert.ToInt16(2);
    //            budgetyear.Approvaler = string.Empty;
    //            budgetyear.Result = false;
    //            budgetyear.Remark = Remark.Value;
    //            budgetyear.Attachment = string.Empty;
    //            budgetyear.UpdateTime = DateTime.Now;
    //            budgetyear.BudgetApply = (Session["allcompanytotal,TotalBudget"] != null) ? Convert.ToDecimal(Session["allcompanytotal,TotalBudget"]) : decimal.Zero;
    //            budgetyear.BudgetApprove = (Session["approvalallcompanytotal,TotalBudget"] != null) ? Convert.ToDecimal(Session["approvalallcompanytotal,TotalBudget"]) : decimal.Zero;
    //            budgetyear.DetailList = new List<BudgetYearDetailInfo>();
    //            foreach (CompanyInfo companyinfo in (List<CompanyInfo>)ViewState["companylist"])
    //            {
    //                GetDetailList(TreeView1.Nodes, budgetyear.DetailList, companyinfo.CompanyID);
    //            }
    //            GetDetailList(TreeView1.Nodes, budgetyear.DetailList, "allcompanytotal");
    //        }
    //        catch (Exception ex)
    //        {
    //            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入预算数额不符合格式要求", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
    //        }

    //        AnnualBudget bll = new AnnualBudget();
    //        BudgetYearInfo searchinfo = new BudgetYearInfo();
    //        searchinfo.CompanyID = budgetyear.CompanyID;
    //        searchinfo.Year = budgetyear.Year;

    //        if (bll.Search(searchinfo).Count > 0)
    //            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "本年度预算已经提交过了，不能再创建", new WebException("本年度预算已经提交过了，不能再创建"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
    //        try
    //        {
    //            budgetyear.Approvaler = UserData.CurrentUserData.PersonName;
    //            newBudgetYaerid = bll.InsertBudgetYear(budgetyear);
    //        }
    //        catch (Exception ex)
    //        {
    //            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交年度预算失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
    //        }
    //    }
    //    if (cmd == "edit")
    //    {
    //        BudgetYearInfo budgetyear = new BudgetYearInfo();
    //        try
    //        {
    //            budgetyear.Title = IPUTTitle.Value;
    //            budgetyear.BudgetYearID = id;
    //            budgetyear.CompanyID = companyid;
    //            budgetyear.Year = Convert.ToInt32(Year.Value);
    //            budgetyear.Maker = UserData.CurrentUserData.PersonName;
    //            budgetyear.MakeTime = DateTime.Now;
    //            budgetyear.Status = Convert.ToInt16(2);
    //            budgetyear.Approvaler = UserData.CurrentUserData.PersonName;
    //            budgetyear.Result = false;
    //            budgetyear.Remark = Remark.Value;
    //            budgetyear.Attachment = string.Empty;
    //            budgetyear.UpdateTime = DateTime.Now;
    //            budgetyear.BudgetApply = (Session["allcompanytotal,TotalBudget"] != null) ? Convert.ToDecimal(Session["allcompanytotal,TotalBudget"]) : decimal.Zero;
    //            budgetyear.BudgetApprove = (Session["approvalallcompanytotal,TotalBudget"] != null) ? Convert.ToDecimal(Session["approvalallcompanytotal,TotalBudget"]) : decimal.Zero;
    //            budgetyear.DetailList = new List<BudgetYearDetailInfo>();
    //            foreach (CompanyInfo companyinfo in (List<CompanyInfo>)ViewState["companylist"])
    //            {
    //                GetDetailList(TreeView1.Nodes, budgetyear.DetailList, companyinfo.CompanyID);
    //            }
    //            GetDetailList(TreeView1.Nodes, budgetyear.DetailList, "allcompanytotal");
    //        }
    //        catch (Exception ex)
    //        {
    //            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入预算数额不符合格式要求", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
    //        }

    //        AnnualBudget bll = new AnnualBudget();
    //        //BudgetYearInfo searchinfo = new BudgetYearInfo();
    //        //searchinfo.CompanyID = budgetyear.CompanyID;
    //        //searchinfo.Year = budgetyear.Year;
    //        //if (bll.Search(searchinfo).Count > 0)
    //        //    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "本年度预算已经提交过了，不能再编制", new WebException("本年度预算已经提交过了，不能再编制"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
    //        try
    //        {
    //            bll.UpdateBudgetYear(budgetyear);
    //        }
    //        catch (Exception ex)
    //        {
    //            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存年度预算失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
    //        }
    //    }
    //    Session.RemoveAll();
    //    if (newBudgetYaerid > 0)
    //        EventMessage.MessageBox(Msg_Type.Info, "操作成功", "已保存为草稿,下次可以继续审批", Icon_Type.OK, true, Common.GetHomeBaseUrl("AnnualBudget.aspx"), UrlType.Href, "");
    //}

    private void GetDetailList(TreeNodeCollection nodes, IList list, string companyid)
    {
        foreach (TreeNode nodetemp in nodes)
        {
            if (nodetemp.ChildNodes.Count == 0)
            {
                BudgetYearDetailInfo item = new BudgetYearDetailInfo();
                item.SubID = Convert.ToInt64(nodetemp.Value);
                item.Name = nodetemp.Text;
                item.DepartmentID = 0;
                if (nodetemp.Parent != null)
                    item.ParentID = Convert.ToInt64(nodetemp.Parent.Value);
                else
                    item.ParentID = 0;
                if (Session[companyid + "," + nodetemp.Value] != null && Session[companyid + "," + nodetemp.Value].ToString() != string.Empty)
                    item.BudgetApply = Convert.ToDecimal(Session[companyid + "," + nodetemp.Value]);
                else
                    item.BudgetApply = decimal.Zero;
                if (Session["approval" + companyid + "," + nodetemp.Value] != null && Session["approval" + companyid + "," + nodetemp.Value].ToString() != string.Empty)
                    item.BudgetApprove = Convert.ToDecimal(Session["approval" + companyid + "," + nodetemp.Value]);
                else
                    item.BudgetApprove = decimal.Zero;
                item.IsLeaf = true;
                if (companyid == "allcompanytotal")
                    item.CompanyID = "al";
                else
                    item.CompanyID = companyid;
                list.Add(item);
            }
            GetDetailList(nodetemp.ChildNodes, list, companyid);
        }
    }

    protected void Sure_Click(object sender, EventArgs e)
    {
        //if(cmd == "add")
        AddSession();
        foreach (CompanyInfo companyinfo in (List<CompanyInfo>)ViewState["companylist"])
        {
            staticsfunction(TreeView1.Nodes, companyinfo.CompanyID);
        }

        staticstotal();
        staticsallcompanytotal(TreeView1.Nodes);
        if (!WorkFlowUserSelectControl1.ProperlySelected)
        {
            EventMessage.MessageBox(Msg_Type.Warn, "无法提交申请单", "无选择下一处理用户",
              Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }
        //staticsfunction(TreeView1.Nodes);
        //staticstotal();
        long newBudgetYaerid = id;
        if (cmd == "add")
        {
            BudgetYearInfo budgetyear = new BudgetYearInfo();
            try
            {
                budgetyear.Title = IPUTTitle.Value;
                budgetyear.CompanyID = companyid;
                budgetyear.Year = Convert.ToInt32(Year.Value);
                budgetyear.Maker = UserData.CurrentUserData.PersonName;
                budgetyear.MakeTime = DateTime.Now;
                budgetyear.Status = Convert.ToInt16(3);
                budgetyear.Approvaler = string.Empty;
                budgetyear.Result = false;
                budgetyear.Remark = Remark.Value;
                budgetyear.Attachment = string.Empty;
                budgetyear.UpdateTime = DateTime.Now;
                budgetyear.BudgetApply = (Session["allcompanytotal,TotalBudget"] != null) ? Convert.ToDecimal(Session["allcompanytotal,TotalBudget"]) : decimal.Zero;
                budgetyear.BudgetApprove = (Session["approvalallcompanytotal,TotalBudget"] != null) ? Convert.ToDecimal(Session["approvalallcompanytotal,TotalBudget"]) : decimal.Zero;
                budgetyear.DetailList = new List<BudgetYearDetailInfo>();
                foreach (CompanyInfo companyinfo in (List<CompanyInfo>)ViewState["companylist"])
                {
                    GetDetailList(TreeView1.Nodes, budgetyear.DetailList, companyinfo.CompanyID);
                }
                GetDetailList(TreeView1.Nodes, budgetyear.DetailList, "allcompanytotal");
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入预算数额不符合格式要求", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }

            AnnualBudget bll = new AnnualBudget();
            BudgetYearInfo searchinfo = new BudgetYearInfo();
            searchinfo.CompanyID = budgetyear.CompanyID;
            searchinfo.Year = budgetyear.Year;
            if (bll.Search(searchinfo).Count > 0)
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "本年度预算已经提交过了，不能再编制", new WebException("本年度预算已经提交过了，不能再编制"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            try
            {
                newBudgetYaerid = bll.InsertBudgetYear(budgetyear);
                Guid guid = WorkflowHelper.CreateNewInstance(newBudgetYaerid, BudgetYearWorkflow.WorkflowName);
                WorkflowHelper.SetStateMachine<BudgetYearEventService>(guid, WorkFlowUserSelectControl1.SelectedEvent);
                WorkflowHelper.UpdateNextUserID(guid, WorkFlowUserSelectControl1.NextUserName, WorkFlowUserSelectControl1.DelegateUserName);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交年度预算失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }
        }
        if (cmd == "edit")
        {
            BudgetYearInfo budgetyear = new BudgetYearInfo();
            try
            {
                budgetyear.Title = IPUTTitle.Value;
                budgetyear.BudgetYearID = id;
                budgetyear.CompanyID = companyid;
                budgetyear.Year = Convert.ToInt32(Year.Value);
                budgetyear.Maker = ViewState["Maker"].ToString();
                budgetyear.MakeTime = DateTime.Now;
                budgetyear.Status = Convert.ToInt16(3);
                budgetyear.Approvaler = UserData.CurrentUserData.PersonName;
                budgetyear.Result = false;
                budgetyear.Remark = Remark.Value;
                budgetyear.Attachment = string.Empty;
                budgetyear.UpdateTime = DateTime.Now;
                budgetyear.BudgetApply = (Session["allcompanytotal,TotalBudget"] != null) ? Convert.ToDecimal(Session["allcompanytotal,TotalBudget"]) : decimal.Zero;
                budgetyear.BudgetApprove = (Session["approvalallcompanytotal,TotalBudget"] != null) ? Convert.ToDecimal(Session["approvalallcompanytotal,TotalBudget"]) : decimal.Zero;
                budgetyear.DetailList = new List<BudgetYearDetailInfo>();
                foreach (CompanyInfo companyinfo in (List<CompanyInfo>)ViewState["companylist"])
                {
                    GetDetailList(TreeView1.Nodes, budgetyear.DetailList, companyinfo.CompanyID);
                }
                GetDetailList(TreeView1.Nodes, budgetyear.DetailList, "allcompanytotal");
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入预算数额不符合格式要求", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }

            AnnualBudget bll = new AnnualBudget();
            //BudgetYearInfo searchinfo = new BudgetYearInfo();
            //searchinfo.CompanyID = budgetyear.CompanyID;
            //searchinfo.Year = budgetyear.Year;
            //if (bll.Search(searchinfo).Count > 0)
            //    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "本年度预算已经提交过了，不能再编制", new WebException("本年度预算已经提交过了，不能再编制"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            try
            {
                bll.UpdateBudgetYear(budgetyear);
                WorkflowHelper.SetStateMachine<BudgetYearEventService>(new Guid(ViewState["WorkFlowInstanceID"].ToString()), WorkFlowUserSelectControl1.SelectedEvent);
                WorkflowHelper.UpdateNextUserID(new Guid(ViewState["WorkFlowInstanceID"].ToString()), WorkFlowUserSelectControl1.NextUserName, WorkFlowUserSelectControl1.DelegateUserName);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交年度预算失败失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }
        }
        Session.RemoveAll();
        if (newBudgetYaerid > 0)
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "操作成功", Icon_Type.OK, true, Common.GetHomeBaseUrl("AnnualBudget.aspx"), UrlType.Href, "");
    }

    protected void Tatics_Click(object sender, EventArgs e)
    {
        //if(cmd == "add")
        AddSession();
        foreach (CompanyInfo companyinfo in (List<CompanyInfo>)ViewState["companylist"])
        {
            staticsfunction(TreeView1.Nodes, companyinfo.CompanyID);
        }

        staticstotal();
        staticsallcompanytotal(TreeView1.Nodes);
        AddForm();
        //AddForm();
        //staticsfunction(TreeView1.Nodes);
        //staticstotal();
        //inputrow.InnerHtml = "";
        //AddTextBox(TreeView1.Nodes);
    }

    private void AddSession()
    {
        string[] sessionarray = sessionvalue.Value.Split('|');
        foreach (string sessioncontent in sessionarray)
        {
            if (sessioncontent != null && sessioncontent != string.Empty)
            {
                string[] temp = sessioncontent.Split('&');
                Session[temp[0]] = temp[1];

            }
        }
    }

}
