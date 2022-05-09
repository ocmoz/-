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

public partial class Module_FM2E_ArchivesManager_ArchivesTypeManage_ViewArchivesType : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 20, 0, DataType.Long);
    private readonly ArchivesType bll = new ArchivesType();
    //加载页面
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
            ButtonBind();
            AddTree(0, (TreeNode)null);
            OperNodeByID(id.ToString(), TreeView1.Nodes, ref TreeView1);
            TreeView1.ShowLines = true;
            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            PermissionControl();
            //********** Modification Finished 2011-09-09 **********************************************************************************************
        }
    }

    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
    private void PermissionControl()
    {
        HeadMenuWebControls1.ButtonList[1].ButtonVisible = SystemPermission.CheckPermission(PopedomType.Delete);
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = SystemPermission.CheckPermission(PopedomType.Edit);
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************

    //填充数据
    private void FillData()
    {
        if (cmd == "view")
        {
            try
            {
                ArchivesTypeInfo item = bll.GetArchivesType(id);
                if (item == null)
                {
                    return;
                }
                lbArchivesTypeID.Text = Convert.ToString(item.ArchivesTypeID);
                lbArchivesTypeName.Text = item.ArchivesTypeName;
                lbDescription.Text = item.Description;
                if (item.ParentName !=null && item.ParentName!="")
                    lbParentName.Text = item.ParentName;
                lbLevel.Text = Convert.ToString(item.Level);
                lbChildCount.Text = Convert.ToString(item.ChildCount);
                lbRemark.Text = item.Remark;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        if (cmd == "delete")
        {
            bool bSuccess = false;
            try
            {
                ArchivesTypeInfo item = bll.GetArchivesType(id);  //获取实体
                DeleteArchivesType(id);  //删除本节点及下属节点
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除" + id + "成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ArchivesType.aspx"), UrlType.Href, "");
            }
        }
    }
    /// <summary>
    /// 绑定按钮
    /// </summary>
    private void ButtonBind()
    {
        HeadMenuWebControls1.ButtonList[0].ButtonUrl = "EditArchivesType.aspx?cmd=edit&id=" + id;
        HeadMenuWebControls1.ButtonList[0].ButtonUrlType = UrlType.Href;
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = true;
        HeadMenuWebControls1.ButtonList[1].ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');", id);
        HeadMenuWebControls1.ButtonList[1].ButtonUrlType = UrlType.JavaScript;
        HeadMenuWebControls1.ButtonList[1].ButtonVisible = true;
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
    /// 显示分层数据项
    /// </summary>
    /// <param name="nodeID"></param>
    /// <param name="tnc"></param>
    /// <param name="tv"></param>
    public void OperNodeByID(string nodeID, TreeNodeCollection tnc, ref TreeView tv)
    {
        foreach (TreeNode node in tnc)
        {
            if (node.Value == nodeID)
            {
                tv.FindNode(node.ValuePath).Selected = true;  //选择项
            }
            if (node.ChildNodes.Count != 0)
                OperNodeByID(nodeID, node.ChildNodes, ref tv);
        }
    }
    /// <summary>
    /// 树点击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        id = Convert.ToInt64(TreeView1.SelectedNode.Value);
        FillData();
        ButtonBind();
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
