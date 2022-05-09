using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using WebUtility.Components;
using WebUtility.WebControls;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;
using FM2E.Model.Archives;
using FM2E.BLL.Archives;

public partial class Module_FM2E_ArchivesManager_ArchivesTypeManage_EditArchivesType : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 20, 0, DataType.Long);
    private readonly ArchivesType bll = new ArchivesType();
    //加载页面
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            ButtonBind();
            AddTree(0, (TreeNode)null);
            if (TreeView1.Nodes.Count == 0)
            {
                TreeNode node = new TreeNode();
                node.Value = "0";
                node.Text = "还没有档案";
                TreeView1.Nodes.Add(node);
                TreeView1.Nodes[0].Select();//没有父节点时，选中
            }
            TreeView1.ShowLines = true;
        }
    }
    // 页面初始化
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
    //填充数据
    private void FillData()
    {
        tbParentName.ReadOnly = true;
        HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[0];
        button.ButtonUrl = String.Format("ViewArchivesType.aspx?cmd=view&id={0}", id);
        if (cmd == "edit")
        {
            try
            {
                ArchivesTypeInfo item = new ArchivesTypeInfo();
                if (Session["ArchivesTypeInfo" + id] != null)
                {
                    item = (ArchivesTypeInfo)Session["ArchivesTypeInfo" + id];
                }
                else
                {
                    item = bll.GetArchivesType(id);
                }
                
                //填充页面
                tbArchivesTypeName.Text = item.ArchivesTypeName;
                tbDescription.Text = item.Description;
                tbChildCount.Text = item.ChildCount.ToString();
                tbRemark.Text = item.Remark;

                ViewState["Name"] = item.ArchivesTypeName;
                if (item.ParentID != 0)
                {
                    tbParentName.Text = item.ParentName.ToString();
                    ViewState["olderparentarchivestypeid"] = item.ParentID.ToString();
                }
                ViewState["level"] = item.Level.ToString();

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "加载信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        else if (cmd == "add")
        {
            try
            {
                if (id > 0)
                {
                    ArchivesTypeInfo archivestypeinfo = bll.GetArchivesType(id);
                    tbParentName.Text = archivestypeinfo.ArchivesTypeName;
                    ViewState["parentarchviestypeidtemp"] = archivestypeinfo.ArchivesTypeID;
                    ViewState["level"] = Convert.ToString(archivestypeinfo.Level + 1);
                }
                tbChildCount.Text = "0";
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }
    //绑定按钮
    private void ButtonBind()
    {
        HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[0];
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：信息添加";
        }
        if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：信息修改";
        }
        button.ButtonUrl = "ArchivesType.aspx";
    }
    //确定按钮事件
    protected void btSave_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;
        ArchivesTypeInfo item = new ArchivesTypeInfo();
        try
        {
            //获取填入数据
            item.ArchivesTypeID = id;
            item.ArchivesTypeName = tbArchivesTypeName.Text.Trim();
            item.Description = tbDescription.Text.Trim();

            if (tbParentName.Text.Trim() != "")
            {
                if (ViewState["parentarchviestypeidtemp"] != null)
                {
                    if (!string.IsNullOrEmpty(ViewState["parentarchviestypeidtemp"].ToString()))
                    {
                        item.ParentID = Convert.ToInt64(ViewState["parentarchviestypeidtemp"].ToString());
                    }
                }
                else
                {
                    item.ParentID = Convert.ToInt64(ViewState["olderparentarchivestypeid"].ToString());
                    item.ParentName = tbParentName.Text.Trim();
                }
            }
            if ((ViewState["level"] != null) && (ViewState["level"].ToString() != ""))
            {
                item.Level = Convert.ToByte(ViewState["level"].ToString());
            }
            else
            {
                item.Level = 1;
            }

            item.ChildCount = Convert.ToInt32(tbChildCount.Text.Trim());
            item.Remark = tbRemark.Text.Trim();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入数据的格式不正确", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }

        if (cmd == "add")  //增加预处理
        {
            ArchivesTypeInfo archivestypeinfo = new ArchivesTypeInfo();
            archivestypeinfo.ArchivesTypeName = item.ArchivesTypeName;
            IList<ArchivesTypeInfo> list = bll.Search(archivestypeinfo);
            if (list.Count != 0)
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "重复插入名称相同的档案类型", new WebException("重复插入名称相同的档案类型"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        else if (cmd == "edit")
        {
            bool overwrite = false;
            ArchivesTypeInfo archivestypeinfo = new ArchivesTypeInfo();
            archivestypeinfo.ArchivesTypeName = item.ArchivesTypeName;
            IList<ArchivesTypeInfo> list = bll.Search(archivestypeinfo);
            if (list.Count != 0 && list[0].ArchivesTypeName != ViewState["Name"].ToString())
                overwrite = true;
            if (overwrite)
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "已存在名称相同的档案类型", new WebException("已存在名称相同的档案类型"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }

        if (cmd == "add")
        {
            bll.InsertArchivesType(item);
            if ((ViewState["parentarchviestypeidtemp"] != null) && (ViewState["parentarchviestypeidtemp"].ToString() != ""))
            {
                ArchivesTypeInfo parentarchivestype = bll.GetArchivesType(Convert.ToInt64(ViewState["parentarchviestypeidtemp"].ToString()));
                if (parentarchivestype != null)
                {
                    parentarchivestype.ChildCount += 1;
                    bll.UpdateArchivesType(parentarchivestype);
                }
            }
            bSuccess = true;
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加档案类型成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ArchivesType.aspx"), UrlType.Href, "");
            }

        }
        else if (cmd == "edit")
        {
            bll.UpdateArchivesType(item);
            if ((ViewState["olderparentarchivestypeid"] != null) && (ViewState["olderparentarchivestypeid"].ToString() != ""))
            {
                ArchivesTypeInfo oldparentarchivestype = bll.GetArchivesType(Convert.ToInt64(ViewState["olderparentarchivestypeid"].ToString()));
                if (oldparentarchivestype != null)
                {
                    if (oldparentarchivestype.ChildCount > 0)
                        oldparentarchivestype.ChildCount -= 1;
                    bll.UpdateArchivesType(oldparentarchivestype);
                }
            }
            if ((ViewState["parentarchviestypeidtemp"] != null) && (ViewState["parentarchviestypeidtemp"].ToString() != ""))
            {
                ArchivesTypeInfo parentarchivestype = bll.GetArchivesType(Convert.ToInt64(ViewState["parentarchviestypeidtemp"].ToString()));
                if (parentarchivestype != null)
                {
                    parentarchivestype.ChildCount += 1;
                    bll.UpdateArchivesType(parentarchivestype);
                }
            }
            bSuccess = true;

            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作成功", "修改档案类型成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ArchivesType.aspx"), UrlType.Href, "");
            }
        }
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
        IList<ArchivesTypeInfo> nodelist = bll.Search(archivestypeinfo);
        List<ArchivesTypeInfo> subnodes = new List<ArchivesTypeInfo>();
        foreach (ArchivesTypeInfo node in nodelist)
        {
            if (cmd == "edit" && id == node.ArchivesTypeID)
                continue;
            if ((node.ParentID == ParentID))
                subnodes.Add(node);
        }

        //循环递归
        foreach (ArchivesTypeInfo node in subnodes)
        {
            if (cmd == "edit" && id == node.ArchivesTypeID)
                continue;
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
        if (cmd == "edit")//编辑的时候无法选择自己作为父节点
        {
            if (id == long.Parse(TreeView1.SelectedNode.Value))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "alertselect", "alert('不能选择自身作为父节点');", true);
                return;
            }

        }
        tbParentName.Text = this.TreeView1.SelectedNode.Text;
        ViewState["parentarchviestypeidtemp"] = this.TreeView1.SelectedNode.Value;
        ViewState["level"] = Convert.ToString(bll.GetArchivesType(Convert.ToInt64(ViewState["parentarchviestypeidtemp"].ToString())).Level + 1);

        PopupControlExtender1.Commit(tbParentName.Text);
    }
}
