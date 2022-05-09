using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using WebUtility.Components;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;
using FM2E.Model.Archives;
using FM2E.BLL.Archives;

public partial class Module_FM2E_ArchivesManager_ArchivesManage_Archives : System.Web.UI.Page
{
    private readonly Archives bll = new Archives();
    //加载页面
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            PermissionControl();

            AddTree(1, (TreeNode)null);
            if (TreeView1.Nodes.Count == 0)
            {
                TreeNode node = new TreeNode();
                node.Value = "0";
                node.Text = "还没有档案";
                TreeView1.Nodes.Add(node);
                TreeView1.Nodes[0].Select();//没有父节点时，选中
            }
            TreeView1.ShowLines = true;

            BuildArchivesTypeTree();
        }
    }
    //初始化页面
    private void InitialPage()
    {
        try
        {
        }
        catch (Exception ex)
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "初始化页面失败：" + ex.Message);
        }
    }
    //权限控制
    private void PermissionControl()
    {
        //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = SystemPermission.CheckPermission(PopedomType.New);
        GridView1.Columns[GridView1.Columns.Count - 1].Visible = SystemPermission.CheckButtonPermission(PopedomType.Delete);
        //********** Modification Finished 2011-09-09 **********************************************************************************************
    }
    //填充数据
    private void FillData()
    {
        try
        {
            int listCount = 0;
            QueryParam searchTerm = (QueryParam)ViewState["SearchTerm"];
            if (searchTerm == null)
            {
                searchTerm = new QueryParam(1, 10);
                searchTerm.Where = "";
            }
            searchTerm.PageSize = AspNetPager1.PageSize;
            searchTerm.PageIndex = AspNetPager1.CurrentPageIndex;
            IList list = bll.GetList(searchTerm, out listCount);
            AspNetPager1.RecordCount = listCount;
            GridView1.DataSource = list;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    //行命令
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long id = Convert.ToInt64(gvRow.Attributes["ArchivesID"]);
        if (e.CommandName == "view")
        {
            Response.Redirect("ViewArchives.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                bll.DeleteArchives(id);
                FillData();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }
    //行数据绑定
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果 
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");
            //设置悬浮鼠标指针形状为"小手"
            e.Row.Attributes["style"] = "Cursor:hand";
            ArchivesInfo item = (ArchivesInfo)e.Row.DataItem;
            e.Row.Attributes["ArchivesID"] = item.ArchivesID.ToString();
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }
    /// <summary>
    /// 查询按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        ArchivesInfo item = new ArchivesInfo();
        item.ArchivesName = Common.inSQL(tbArchivesName.Text.Trim());
        if (tbArchivesTypeName.Text.Trim() != "")
        {
            item.ArchivesTypeName = tbArchivesTypeName.Text.Trim();
        }
        item.InvolvedSystem = Common.inSQL(tbInvolvedSystem.Text.Trim());
        item.InvolvedEquipment = Common.inSQL(tbInvolvedEquipment.Text.Trim());
        item.Description = Common.inSQL(tbDescription.Text.Trim());
        item.Remark = Common.inSQL(tbRemark.Text.Trim());
        QueryParam searchTerm = bll.GenerateSearchTerm(item);
        TabContainer1.ActiveTabIndex = 0;
        ViewState["SearchTerm"] = searchTerm;
        FillData();
        //查询后清空查询条件
        tbArchivesName.Text = tbArchivesTypeName.Text = tbInvolvedSystem.Text = tbInvolvedEquipment.Text = tbDescription.Text = tbRemark.Text = "";
    }

    /// <summary>
    /// 生成树形结构
    /// </summary>
    /// <param name="ParentID"></param>
    /// <param name="pNode"></param>
    public void AddTree(long ParentID, TreeNode pNode)
    {
        ArchivesType bll = new ArchivesType();
        ArchivesTypeInfo archivestypeinfo = new ArchivesTypeInfo();
        archivestypeinfo.ParentID = ParentID;
        IList<ArchivesTypeInfo> nodelist = bll.Search(archivestypeinfo);
        List<ArchivesTypeInfo> subnodes = new List<ArchivesTypeInfo>();
        foreach (ArchivesTypeInfo node in nodelist)
        {
            if ((node.ParentID == ParentID))
                subnodes.Add(node);
        }

        //循环递归
        foreach (ArchivesTypeInfo node in subnodes)
        {
            //声明节点
            TreeNode Node = new TreeNode();
            //绑定超级链接
            //开始递归
            if (pNode == null)
            {
                //添加根节点
                Node.Text = node.ArchivesTypeName;
                Node.Value = node.ArchivesTypeID.ToString();
                TreeView1.Nodes.Add(Node);
                Node.Expanded = true; //节点状态展开
                AddTree(node.ArchivesTypeID, Node);    //再次递归
            }
            else
            {
                //添加当前节点的子节点
                Node.Text = node.ArchivesTypeName;
                Node.Value = node.ArchivesTypeID.ToString();
                //TreeView1.Nodes.Add(Node);
                pNode.ChildNodes.Add(Node);
                Node.Expanded = true; //节点状态展开
                AddTree(node.ArchivesTypeID, Node);     //再次递归
            }
        }
    }

    /// <summary>
    /// 树点击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        tbArchivesTypeName.Text = this.TreeView1.SelectedNode.Text;

        PopupControlExtender1.Commit(tbArchivesTypeName.Text);
    }


    /// <summary>
    /// 建立树形结构
    /// </summary>
    /// <param name="ParentID"></param>
    /// <param name="pNode"></param>
    protected void BuildArchivesTypeTree()
    {
        ArchivesType bll = new ArchivesType();
        try
        {
            TreeNode root = null;

            IList archivesTypeList = bll.GetChildArchivesType(1);
            root = new TreeNode("档案列表", "0");

            ArchivesTypeTree.Nodes.Add(root);

            foreach (ArchivesTypeInfo item in archivesTypeList)
            {
                TreeNode node = new TreeNode(item.ArchivesTypeName, item.ArchivesTypeID.ToString());
                node.Expanded = false;
                if (item.ChildCount > 0)
                {
                    //非叶子结点
                    node.Expanded = false;
                    node.PopulateOnDemand = true;
                    node.SelectAction = TreeNodeSelectAction.SelectExpand;

                }
                root.ChildNodes.Add(node);
            }
            root.Select();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "加载档案类型树失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 选择树结点
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ArchivesTypeTree_SelectedNodeChanged(object sender, EventArgs e)
    {
        Filter();
    }

    /// <summary>
    /// 展开树结点
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ArchivesTypeTree_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        ArchivesType bll = new ArchivesType();
        try
        {
            long id = Convert.ToInt64(e.Node.Value);
            IList archivesTypeList = bll.GetChildArchivesType(id);
            foreach (ArchivesTypeInfo item in archivesTypeList)
            {
                TreeNode node = new TreeNode(item.ArchivesTypeName, item.ArchivesTypeID.ToString());
                node.Expanded = false;
                if (item.ChildCount > 0)
                {
                    //非叶子结点
                    node.PopulateOnDemand = true;
                    node.SelectAction = TreeNodeSelectAction.SelectExpand;
                }
                e.Node.ChildNodes.Add(node);
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取下一级地址结点失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 筛选条件
    /// </summary>
    private void Filter()
    {
        ArchivesInfo item = new ArchivesInfo();
        item.ArchivesName = Common.inSQL(tbArchivesName.Text.Trim());
        //if (tbArchivesTypeName.Text.Trim() != "")
        //{
        //    item.ArchivesTypeName = tbArchivesTypeName.Text.Trim();
        //}
        item.ArchivesTypeID = Convert.ToInt64(ArchivesTypeTree.SelectedValue);
        item.InvolvedSystem = Common.inSQL(tbInvolvedSystem.Text.Trim());
        item.InvolvedEquipment = Common.inSQL(tbInvolvedEquipment.Text.Trim());
        item.Description = Common.inSQL(tbDescription.Text.Trim());
        item.Remark = Common.inSQL(tbRemark.Text.Trim());
        QueryParam searchTerm = bll.GenerateSearchTerm(item);
        TabContainer1.ActiveTabIndex = 0;
        ViewState["SearchTerm"] = searchTerm;
        FillData();
    }
}
