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
using FM2E.BLL.Budget;
using FM2E.Model.Budget;
using WebUtility.WebControls;
using WebUtility.Components;
using System.Collections.Generic;
using FM2E.Model.Utils;

public partial class Module_FM2E_BudgetManager_SubjectRelationManager_ViewSubjectRelation : System.Web.UI.Page
{
    string companyid = UserData.CurrentUserData.CompanyID;
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 255, 0, DataType.Long);

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            ButtonBind();
            BindData();
            AddTree(0, (TreeNode)null);
            OperNodeByID(id.ToString(), TreeView1.Nodes, ref TreeView1);
            TreeView1.ShowLines = true;
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

    private void ButtonBind()
    {
        HeadMenuButtonItem button1 = HeadMenuWebControls1.ButtonList[1];
        button1.ButtonUrl += "&id=" + id;
        if (cmd == "view")
        {
            //删除
            HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[3];
            button.ButtonUrlType = UrlType.JavaScript;
            button.ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');", id);
            //修改
            button = HeadMenuWebControls1.ButtonList[2];
            button.ButtonUrlType = UrlType.Href;
            button.ButtonUrl = string.Format("EditSubjectRelation.aspx?cmd=edit&id={0}", id);
        }
        else if (cmd == "delete")
        {
            //执行删除操作
            bool bSuccess = false;
            try
            {
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
                /*TreeView1.Nodes.Clear();
                AddTree(0, (TreeNode)null);
                DepartmentInfo departmentinfo2 = new DepartmentInfo();
                departmentinfo2.CompanyID = companyid;
                IList<DepartmentInfo> list = new Department().Search(departmentinfo2);
                AspNetPager1.RecordCount = list.Count;
                GridView1.DataSource = list;
                GridView1.DataBind();*/

                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除科目失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess == true)
            {
                //EventMessage.MessageBox(1, "操作成功", "删除记录ID:(" + id + ")成功！", Icon_Type.OK, Common.GetHomeBaseUrl("DeviceInfo.aspx"));
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除科目:成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("SubjectRelation.aspx"), UrlType.Href, "");
            }
        }
    }
    /// <summary>
    /// 列表行事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                long idtemp = Convert.ToInt64(gvRow.Attributes["SubID"]);
                SubjectRelation bll = new SubjectRelation();
                SubjectRelationInfos subjectrelationinfo = bll.GetSubjectRelation(idtemp);
                deleteSubjectRelation(idtemp);
                SubjectRelationInfos searchinfo = new SubjectRelationInfos();
                searchinfo.ParentID = subjectrelationinfo.ParentID;
                if (bll.Search(searchinfo).Count == 0)
                {
                    SubjectRelationInfos parentsubject = bll.GetSubjectRelation(subjectrelationinfo.ParentID);
                    parentsubject.IsLeaf = 1;
                    bll.UpdateSubjectRelation(parentsubject);
                }
                //Response.Redirect("ViewDeviceType.aspx?cmd=view&id=" + id);
                TreeView1.Nodes.Clear();
                AddTree(0, (TreeNode)null);

                SubjectRelationInfos item2 = new SubjectRelationInfos();
                item2.ParentID = id;
                IList<SubjectRelationInfos> list = bll.Search(item2);
                GridView1.DataSource = list;
                GridView1.DataBind();



                //CategorysearchInfo item2 = new CategorysearchInfo();
                //item2.ParentID = id;
                //IList<CategoryInfo> list = bll.Search(item2);
                //GridView1.DataSource = list;
                //GridView1.DataBind();
                
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }

    private void BindData()
    {
        try
        {
            SubjectRelation subjectrelation = new SubjectRelation();
            SubjectRelationInfos item = subjectrelation.GetSubjectRelation(id);

            Session["SubjectRelationInfos" + id] = item;
            if (item.SubID != 0)
                SubID.Value = item.SubID.ToString();
            Name.Text = item.Name;
            ParentName.Text = item.ParentName;
            if (item.ParentName != null && item.ParentName != string.Empty)
                ParentName.NavigateUrl = "ViewSubjectRelation.aspx?cmd=view&id=" + item.ParentID;
            if (item.IsLeaf != 0)
                IsLeafshow.Text = (item.IsLeaf == Convert.ToInt16(1)) ? "是" : "否";
            IsLeaf.Value = item.IsLeaf.ToString(); ;

            SubjectRelationInfos item2 = new SubjectRelationInfos();
            item2.ParentID = item.SubID;
            IList<SubjectRelationInfos> list = subjectrelation.Search(item2);
            GridView1.DataSource = list;
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
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

    public void AddTree(long ParentID, TreeNode pNode)
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

    public void OperNodeByID(string nodeID, TreeNodeCollection tnc, ref   TreeView tv)
    {
        foreach (TreeNode node in tnc)
        {
            if (node.Value == nodeID)
            {
                tv.FindNode(node.ValuePath).Selected = true;
                ExpandAllParentNode(tv.FindNode(node.ValuePath));
            }
            if (node.ChildNodes.Count != 0)
                OperNodeByID(nodeID, node.ChildNodes, ref   tv);
        }
    }

    private void ExpandAllParentNode(TreeNode node)
    {
        if (node.Parent != null)
        {
            node.Parent.Expanded = true;
            ExpandAllParentNode(node.Parent);
        }
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        id = Convert.ToInt64(TreeView1.SelectedNode.Value);
        BindData();

    }


}
