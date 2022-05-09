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
using System.Collections.Generic;
using WebUtility;
using System.Web.UI.MobileControls;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;

public partial class Module_FM2E_BudgetManager_MonthlyBudgetApprovalManager_EditDetail : System.Web.UI.Page
{
    int index = (int)Common.sink("index", MethodType.Get, 255, 0, DataType.Int);
    string companyid = UserData.CurrentUserData.CompanyID;
    long yearid = (long)Common.sink("yearid", MethodType.Get, 255, 0, DataType.Long);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Company companybll = new Company();
            CompanyInfo companyinfo = new CompanyInfo();
            //companyinfo.IsParentCompany = false;
            IList companylist = (List<CompanyInfo>)companybll.Search(companyinfo);
            companycount.Value = companylist.Count.ToString();
            AddTree2(0, (TreeNode)null);
            TreeView2.ShowLines = true;
            InitPage();
        }
    }

    private void InitPage()
    {
        BudgetDetailInfo budgetdetailinfo = ((List<BudgetDetailInfo>)Session["BudgetDetaillist"])[index];
        SubIDNametb.Text = budgetdetailinfo.SubName;
        SubIDtb.Text = budgetdetailinfo.SubID.ToString();
        ExpenditureNametb.Text = budgetdetailinfo.ExpenditureName;
        //Expendituretb.Text = budgetdetailinfo.Expenditure.ToString();
        //if(budgetdetailinfo.BudgetApprove != decimal.Zero)
        //    BudgetApprove.Text = budgetdetailinfo.BudgetApprove.ToString();
        Reviewtb.Text = budgetdetailinfo.Review;
        Remarks.Text = budgetdetailinfo.Remarks;
        ExpenditureDetailtb.Text = budgetdetailinfo.ExpenditureDetail;
        //FileUpload1tb.f = budgetdetailinfo.Attachment;
        attachment.HRef = budgetdetailinfo.Attachment;
        int comcount = Convert.ToInt32(companycount.Value);
        for (int i = index * comcount; i < index * comcount + comcount; i++)
        {
            HtmlTableCell celltitle = new HtmlTableCell();
            celltitle.InnerText = ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].CompanyName + ":";
            expenditure.Cells.Add(celltitle);

            HtmlTableCell cellcontent = new HtmlTableCell();
            HtmlInputText celllabel = new HtmlInputText();
            celllabel.Value = ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].Expenditure.ToString();
            celllabel.Size = 14;
            celllabel.Disabled = true;
            cellcontent.Controls.Add(celllabel);
            expenditure.Cells.Add(cellcontent);

            HtmlTableCell celltitle2 = new HtmlTableCell();
            celltitle2.InnerText = ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].CompanyName + ":";
        
            budgetapprove.Cells.Add(celltitle2);

            HtmlInputText inputtext = new HtmlInputText();
            inputtext.Size = 14;
            inputtext.Attributes["id"] = "inputtext" + ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].CompanyID;
            inputtext.Value = ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].BudgetApprove.ToString();
            inputtext.Attributes["onblur"] = "javascript:AddSession(this.id,this.value);";

            HtmlTableCell cellcontent2 = new HtmlTableCell();
            cellcontent2.Controls.Add(inputtext);

            budgetapprove.Cells.Add(cellcontent2);


        }

    }



    /// <summary>
    /// 初始化种类弹出树
    /// </summary>
    /// <param name="ParentID"></param>
    /// <param name="pNode"></param>
    public void AddTree2(long ParentID, TreeNode pNode)
    {
        SubjectPerYear subjectperyearinfo = new SubjectPerYear();
        subjectperyearinfo.ParentID = ParentID;
        subjectperyearinfo.Year = yearid;
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

    protected void AddDetail_Click(object sender, EventArgs e)
    {
        AddSession();
        IList list = ((List<BudgetDetailInfo>)Session["BudgetDetaillist"]);
        //BudgetDetailInfo item = ((BudgetDetailInfo)list[index]);
        ((BudgetDetailInfo)list[index]).SubName = SubIDNametb.Text;
        ((BudgetDetailInfo)list[index]).SubID = Convert.ToInt64(SubIDtb.Text);
        ((BudgetDetailInfo)list[index]).ExpenditureName = ExpenditureNametb.Text;
        ((BudgetDetailInfo)list[index]).BudgetApproveStr = string.Empty;
        int comcount = Convert.ToInt32(companycount.Value);
        decimal totalexpenditure = 0;
        for (int i = index * comcount; i < index * comcount + comcount; i++)
        {

            ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].BudgetApprove = (ViewState["inputtext" + ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].CompanyID] != null) ? Convert.ToDecimal(ViewState["inputtext" + ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].CompanyID].ToString()) : ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].BudgetApprove;
            ((BudgetDetailInfo)list[index]).BudgetApproveStr += ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].CompanyName + ":" + ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].BudgetApprove + " ";
            totalexpenditure += ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].BudgetApprove;
            ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].SubName = SubIDNametb.Text;
            ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].SubID = Convert.ToInt64(SubIDtb.Text);
            ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].ExpenditureName = ExpenditureNametb.Text;
            ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].Review = Reviewtb.Text;
            ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].Remarks = Remarks.Text;
            ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].ExpenditureDetail = ExpenditureDetailtb.Text;
            ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].Attachment = attachment.HRef;
            ((List<BudgetDetailInfo>)Session["RealBudgetDetaillist"])[i].Approvaler = UserData.CurrentUserData.PersonName;
        }
        ((BudgetDetailInfo)list[index]).BudgetApproveStr += " 小计:" + totalexpenditure;
        //((BudgetDetailInfo)list[index]).Expenditure = Convert.ToDecimal(Expendituretb.Text);
        //((BudgetDetailInfo)list[index]).BudgetApprove = Convert.ToDecimal(BudgetApprove.Text);
        ((BudgetDetailInfo)list[index]).Review = Reviewtb.Text;
        ((BudgetDetailInfo)list[index]).Remarks = Remarks.Text;
        ((BudgetDetailInfo)list[index]).ExpenditureDetail = ExpenditureDetailtb.Text;
        ((BudgetDetailInfo)list[index]).Attachment = attachment.HRef;
        ((BudgetDetailInfo)list[index]).Approvaler = UserData.CurrentUserData.PersonName;
        //list[index] = item;
        Session["BudgetDetaillist"] = list;

        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "BudgetDetailRefresh", "window.parent.hidePopWin(true);", true);
      
    }

    protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
    {
        SubIDNametb.Text = this.TreeView2.SelectedNode.Text;
        SubIDtb.Text = this.TreeView2.SelectedValue;
        PopupControlExtender1.Commit(SubIDNametb.Text);
        PopupControlExtender2.Commit(SubIDtb.Text);
    }

    private void AddSession()
    {
        string[] sessionarray = sessionvalue.Value.Split('|');
        foreach (string sessioncontent in sessionarray)
        {
            if (sessioncontent != null && sessioncontent != string.Empty)
            {
                string[] temp = sessioncontent.Split(',');
                ViewState[temp[0]] = temp[1];

            }
        }
    }


}
