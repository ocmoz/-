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
using FM2E.Model.BudgetManagement;
using System.Collections.Generic;
using WebUtility.Components;

public partial class Module_FM2E_BudgetManagement_BudgetAccounts_SubjectRelation : System.Web.UI.Page
{
    string companyid = UserData.CurrentUserData.CompanyID;
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            FillData();
            AddTree(0, (TreeNode)null);
            TreeView1.ShowLines = true;

            PermissionControl();
        }
    }

    private void PermissionControl()
    {
        if (SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
        else GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
    }
    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }

    private void FillData()
    {
        try
        {
            QueryParam qp = (QueryParam)ViewState["SearchTerm"];
            if (qp == null)
            {
                qp = new QueryParam();
            }
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;
            SubjectRelation bll = new SubjectRelation();
            int recordCount = 0;
            IList list = bll.GetList(qp, out recordCount,null);
            GridView1.DataSource = list;
            GridView1.DataBind();
            
            AspNetPager1.RecordCount = recordCount;

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取预算科目失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
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
            SubjectRelationInfos dv = (SubjectRelationInfos)e.Row.DataItem;
            e.Row.Attributes["SubID"] = dv.SubID.ToString();
        }

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "view")
        {
            string subid = gvRow.Attributes["SubID"];
            Response.Redirect("ViewSubjectRelation.aspx?cmd=view&id=" + subid);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                int row = Convert.ToInt32(e.CommandArgument);
                long id = Convert.ToInt64(gvRow.Attributes["SubID"]);
                SubjectRelation bll = new SubjectRelation();
                SubjectRelationInfos subjectrelationinfo = bll.GetSubjectRelation(id);
                deleteSubjectRelation(id);
                SubjectRelationInfos searchinfo = new SubjectRelationInfos();
                searchinfo.ParentID = subjectrelationinfo.ParentID;
                if (bll.Search(searchinfo).Count == 0)
                {
                    SubjectRelationInfos parentsubject = bll.GetSubjectRelation(subjectrelationinfo.ParentID);
                    parentsubject.IsLeaf = 1;
                    bll.UpdateSubjectRelation(parentsubject);
                }
                TreeView1.Nodes.Clear();
                AddTree(0, (TreeNode)null);
                FillData();
                //CategoryInfo Categoryinfo2 = new CategoryInfo();
                //IList<CategoryInfo> list = new Category().Search(Categoryinfo2);
                //AspNetPager1.RecordCount = list.Count;
                //GridView1.DataSource = list;
                //GridView1.DataBind();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }

    private void deleteSubjectRelation(long id)
    {
        SubjectRelation bll = new SubjectRelation();
        SubjectRelationInfos subjectrelationinfo = bll.GetSubjectRelation(id);
        SubjectRelationInfos searchiinfo = new SubjectRelationInfos();
        if (subjectrelationinfo.IsLeaf != 1)
        {
            bll.DelSubjectRelation(subjectrelationinfo.SubID);
            searchiinfo.ParentID = subjectrelationinfo.SubID;
            IList<SubjectRelationInfos> subjectlist = bll.Search(searchiinfo);
            foreach (SubjectRelationInfos subjecttemp in subjectlist)
            {
                deleteSubjectRelation(subjecttemp.SubID);
            }
            
        }
        else
            bll.DelSubjectRelation(subjectrelationinfo.SubID);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SubjectRelationInfos item = new SubjectRelationInfos();
        if (Name.Value != string.Empty)
            item.Name = Name.Value;
        if (ParentName.Value != string.Empty)
            item.ParentName = ParentName.Value;
        if (IsLeaf.SelectedValue != "0")
            item.IsLeaf = Convert.ToInt16(IsLeaf.SelectedValue);

        SubjectRelation bll = new SubjectRelation();
        QueryParam qp = bll.GenerateSearchTerm(item);
        ViewState["SearchTerm"] = qp;
        FillData();
        TabContainer1.ActiveTabIndex = 0;
    }

    public void AddTree(long ParentID, TreeNode pNode)
    {
        SubjectRelationInfos subjectrelationinfo = new SubjectRelationInfos();
        subjectrelationinfo.ParentID = ParentID;
        SubjectRelation bll = new SubjectRelation();
        QueryParam qp = bll.GenerateSearchTerm(subjectrelationinfo);
        int recordcount = 0;
        IList nodelist = bll.GetList(qp, out recordcount,companyid);
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
            Node.NavigateUrl = "ViewSubjectRelation.aspx?cmd=view&id=" + node.SubID;
            if (pNode == null)
            {
                Node.Text = node.Name;
                Node.Value = node.SubID.ToString();
                TreeView1.Nodes.Add(Node);
                Node.Expanded = false;
                AddTree(node.SubID, Node);
            }
            else
            {
                Node.Text = node.Name;
                Node.Value = node.SubID.ToString();
                pNode.ChildNodes.Add(Node);
                Node.Expanded = false;
                AddTree(node.SubID, Node);
            }
        }
    }
}
