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

public partial class Module_FM2E_ArchivesManager_ArchivesTypeManage_ArchivesType : System.Web.UI.Page
{
    private readonly ArchivesType bll = new ArchivesType();
    //加载页面
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();

            //调用递归函数，完成树形结构的生成
            AddTree(0, (TreeNode)null);
            TreeView1.ShowLines = true;

            PermissionControl();
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
        long id = Convert.ToInt64(gvRow.Attributes["ArchivesTypeID"]);
        if (e.CommandName == "view")
        {
            Response.Redirect("ViewArchivesType.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                DeleteArchivesType(id);
                TreeView1.Nodes.Clear();
                AddTree(0, (TreeNode)null);
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
            ArchivesTypeInfo item = (ArchivesTypeInfo)e.Row.DataItem;
            e.Row.Attributes["ArchivesTypeID"] = item.ArchivesTypeID.ToString();
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
    }
    /// <summary>
    /// 查询按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        ArchivesTypeInfo item = new ArchivesTypeInfo();
        item.ArchivesTypeName = Common.inSQL(tbArchivesTypeName.Text.Trim());
        item.Description = Common.inSQL(tbDescription.Text.Trim());
        if (tbParentID.Text.Trim() != "")
        {
            item.ParentID = Convert.ToInt64(tbParentID.Text.Trim());
        }
        else
        {
            item.ParentID = 0;
        }
        //item.Level = Convert.ToInt32(tbLevel.Text.Trim());
        //item.ChildCount = Convert.ToInt32(tbChildCount.Text.Trim());
        item.Remark = Common.inSQL(tbRemark.Text.Trim());
        QueryParam searchTerm = bll.GenerateSearchTerm(item);
        TabContainer1.ActiveTabIndex = 0;
        ViewState["SearchTerm"] = searchTerm;
        FillData();
    }
    /// <summary>
    /// 生成树形结构
    /// </summary>
    /// <param name="ParentID"></param>
    /// <param name="pNode"></param>
    public void AddTree(long ParentID, TreeNode pNode)
    {
        ArchivesTypeInfo archivestypeinfo = new ArchivesTypeInfo();
        archivestypeinfo.ParentID = ParentID;
        IList<ArchivesTypeInfo> nodelist = new ArchivesType().Search(archivestypeinfo);
        List<ArchivesTypeInfo> subnodes = new List<ArchivesTypeInfo>();
        foreach (ArchivesTypeInfo node in nodelist)
        {
            if (node.ParentID == ParentID)
                subnodes.Add(node);
        }

        //循环递归
        foreach (ArchivesTypeInfo node in subnodes)
        {
            //声明节点
            TreeNode Node = new TreeNode();
            //绑定超级链接
            Node.NavigateUrl = "ViewArchivesType.aspx?cmd=view&id=" + node.ArchivesTypeID;
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
    /// 点击树节点事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
    }
    /// <summary>
    /// 删除当前节点及下属节点
    /// </summary>
    /// <param name="id"></param>
    private void DeleteArchivesType(long id)
    {
        ArchivesTypeInfo archivestypeinfo = bll.GetArchivesType(id);
        ArchivesTypeInfo childarchivestypeinfo = new ArchivesTypeInfo();
        if (archivestypeinfo.ChildCount > 0)  //若有子节点
        {
            childarchivestypeinfo.ParentID = archivestypeinfo.ArchivesTypeID;
            IList<ArchivesTypeInfo> archivestypelist = bll.Search(childarchivestypeinfo);
            foreach (ArchivesTypeInfo archivestypetemp in archivestypelist)
            {
                DeleteArchivesType(archivestypetemp.ArchivesTypeID);  //删除子节点及下属节点
            }
            bll.DeleteArchivesType(archivestypeinfo.ArchivesTypeID);  //删除当前节点
        }
        else  //若无子节点
        {
            bll.DeleteArchivesType(archivestypeinfo.ArchivesTypeID);  //删除当前节点
        }
    }
}
