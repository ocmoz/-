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
using WebUtility;
using WebUtility.WebControls;
using FM2E.Model.Budget;
using FM2E.BLL.Budget;
using FM2E.Model.Utils;
using System.Collections.Generic;
using WebUtility.Components;
using FM2E.Model.Exceptions;
using FM2E.Model.Basic;
using FM2E.BLL.Basic;

public partial class Module_FM2E_BudgetManager_AnnualBudgetApprovalManager_ViewAnnualBudget : System.Web.UI.Page
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
            Company companybll = new Company();
            CompanyInfo companyinfo = new CompanyInfo();
            //companyinfo.IsParentCompany = false;
            IList companylist = (List<CompanyInfo>)companybll.Search(companyinfo);
            ViewState["companylist"] = companylist;

           
            inittreeyet = false;
            AddTree(0, (TreeNode)null);
            TreeView1.ShowLines = true;
            GetSessionFromDatabase();
            foreach (CompanyInfo companyitem in (List<CompanyInfo>)ViewState["companylist"])
            {
                staticsfunction(TreeView1.Nodes, companyitem.CompanyID);
            }
            staticstotal();
            staticsallcompanytotal(TreeView1.Nodes);
            AddForm();
            inittreeyet = true;
        }
      
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
            celllabel.Attributes["onmousedown"] = "MouseDownToResize(this);";
            celllabel.Attributes["onmousemove"] = "MouseMoveToResize(this);";
            celllabel.Attributes["onmouseup"] = "MouseUpToResize(this);";

            celltotal.Controls.Add(celllabel);
            TR_company.Cells.Add(celltotal);
            //增加输入内容
            HtmlTableCell celltotalcontent = new HtmlTableCell();
            AddTextBox(TreeView1.Nodes, "allcompanytotal", celltotalcontent);

            TR_content.Cells.Add(celltotalcontent);
            //增加合计内容
            HtmlTableCell celltaticstoal = new HtmlTableCell();
            HtmlInputText inputtext = new HtmlInputText();
            inputtext.Size = 14;
            inputtext.Attributes["id"] = "allcompanytotal,TotalBudget";
            inputtext.Attributes["style"] = "display:block";
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
            celllabel2.Attributes["onmousedown"] = "MouseDownToResize(this);";
            celllabel2.Attributes["onmousemove"] = "MouseMoveToResize(this);";
            celllabel2.Attributes["onmouseup"] = "MouseUpToResize(this);";

            celltotal2.Controls.Add(celllabel2);
            TR_company.Cells.Add(celltotal2);
            //增加输入内容
            HtmlTableCell celltotalcontent2 = new HtmlTableCell();
            AddTextBox(TreeView1.Nodes, "approvalallcompanytotal", celltotalcontent2);

            TR_content.Cells.Add(celltotalcontent2);
            //增加合计内容
            HtmlTableCell celltaticstoal2 = new HtmlTableCell();
            HtmlInputText inputtext2 = new HtmlInputText();
            inputtext2.Size = 14;
            inputtext2.Attributes["id"] = "approvalallcompanytotal,TotalBudget";
            inputtext2.Attributes["style"] = "display:block";
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
                cellcompanylabel.Attributes["onmousedown"] = "MouseDownToResize(this);";
                cellcompanylabel.Attributes["onmousemove"] = "MouseMoveToResize(this);";
                cellcompanylabel.Attributes["onmouseup"] = "MouseUpToResize(this);";

                cell.Controls.Add(cellcompanylabel);
                TR_company.Cells.Add(cell);
                //增加输入内容
                HtmlTableCell cellcontent = new HtmlTableCell();
                string innerstr = string.Empty;
                AddTextBox(TreeView1.Nodes, companyinfo.CompanyID, cellcontent);

                TR_content.Cells.Add(cellcontent);
                //增加合计内容
                HtmlTableCell cellcompanytotal = new HtmlTableCell();
                HtmlInputText companyinputtext = new HtmlInputText();
                companyinputtext.Size = 14;
                companyinputtext.Attributes["id"] = companyinfo.CompanyID + ",TotalBudget";
                companyinputtext.Attributes["style"] = "display:block";
                if (Session[companyinfo.CompanyID + ",TotalBudget"] != null)
                    companyinputtext.Value = Session[companyinfo.CompanyID + ",TotalBudget"].ToString();
                companyinputtext.Attributes["onblur"] = "javascript:AddSession(this.id,this.value);";
                companyinputtext.Attributes["readonly"] = "readonly";
                cellcompanytotal.Controls.Add(companyinputtext);
                TR_total.Cells.Add(cellcompanytotal);

                //增加标题
                HtmlTableCell cell2 = new HtmlTableCell();
                HtmlGenericControl cellcompanylabel2 = new HtmlGenericControl();
                cellcompanylabel2.InnerText = companyinfo.CompanyName+"审批数";
                cellcompanylabel2.Attributes["style"] = "position:relative; cursor:e-resize;";
                cellcompanylabel2.Attributes["onmousedown"] = "MouseDownToResize(this);";
                cellcompanylabel2.Attributes["onmousemove"] = "MouseMoveToResize(this);";
                cellcompanylabel2.Attributes["onmouseup"] = "MouseUpToResize(this);";

                cell2.Controls.Add(cellcompanylabel2);
                TR_company.Cells.Add(cell2);
                //增加输入内容
                HtmlTableCell cellcontent2 = new HtmlTableCell();
                AddTextBox(TreeView1.Nodes, "approval"+companyinfo.CompanyID, cellcontent2);

                TR_content.Cells.Add(cellcontent2);
                //增加合计内容
                HtmlTableCell cellcompanytotal2 = new HtmlTableCell();
                HtmlInputText companyinputtext2 = new HtmlInputText();
                companyinputtext2.Size = 14;
                companyinputtext2.Attributes["id"] = "approval"+companyinfo.CompanyID + ",TotalBudget";
                companyinputtext2.Attributes["style"] = "display:block";
                if (Session["approval"+companyinfo.CompanyID + ",TotalBudget"] != null)
                    companyinputtext2.Value = Session["approval"+companyinfo.CompanyID + ",TotalBudget"].ToString();
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
        SubjectPerYear subjectperyearinfo = new SubjectPerYear();
        subjectperyearinfo.ParentID = ParentID;
        subjectperyearinfo.Year = id;
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
    private void AddTextBox(TreeNodeCollection nodes, string companyid, HtmlTableCell cell)
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
                    inputtext.Attributes["style"] = "display:" + showornot;
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
                    inputtext.Attributes["style"] = "display:block";
                    inputtext.Value = textvalue;
                    inputtext.Attributes["title"] = nodetemp.Text;
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
                    inputtext.Attributes["readonly"] = "readonly";
                    cell.Controls.Add(inputtext);
                }
                //inputrowstr += "<input type='text' size='14' id='" + companyid + "," + nodetemp.Value + "' style='display:block' runat='server' value='" + textvalue + "'onblur='javascript:AddSession(this.id,this.value);' />";

            }
            AddTextBox(nodetemp.ChildNodes, companyid, cell);
        }


    }
    /// <summary>
    /// 树的展开事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void TreeView1_OnTreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {
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
    private void staticsfunction(TreeNodeCollection nodes,string companyid)
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
                    if (Session["approval"+companyid + "," + subnodetemp.Value] != null)
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
    private void GetSessionFromDatabase()
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

}
