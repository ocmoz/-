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

public partial class Module_FM2E_ArchivesManager_ArchivesManage_EditArchives : System.Web.UI.Page
{
    /// <summary>
    /// 该页在新增的时候使用SESSION的名称
    /// </summary>
    string sessionName = "Module_FM2E_ArchivesManager_ArchivesManage_Archives";
    /// <summary>
    /// 该页在编辑的时候使用的Session名称
    /// </summary>
    string sessionName4Edit = "Module_FM2E_ArchivesManager_ArchivesManage_ArchivesEdit";
    /// <summary>
    /// 上传文件路径，相对于~/public文件夹
    /// </summary>
    private const string UPLOADFOLDER = "ArchivesAttachmentFile/";

    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 20, 0, DataType.Long);
    private readonly Archives bll = new Archives();
    //加载页面
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            ButtonBind();
            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            PermissionControl();
            //********** Modification Finished 2011-09-09 **********************************************************************************************

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
        }
    }

    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
    private void PermissionControl()
    {
        bool bEdit = SystemPermission.CheckButtonPermission(PopedomType.Edit);
        gridview_ItemList.Columns[gridview_ItemList.Columns.Count - 1].Visible = SystemPermission.CheckButtonPermission(PopedomType.Delete);
        gridview_ItemList.Columns[gridview_ItemList.Columns.Count - 2].Visible = bEdit;
        Button_Select.Visible = bEdit;
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************

    // 页面初始化
    private void InitialPage()
    {
        try
        {
            if (cmd == "add")
            {
                Session[sessionName] = null;//清空该页面使用的SESSION
            }
            else if (cmd == "edit")
            {
                Session[sessionName] = null;//清空该页面使用的SESSION
                Archives arbll = new Archives();
                ArchivesInfo archives = arbll.GetArchives(id);
                Session[sessionName4Edit] = archives;
            }

        }
        catch (Exception ex)
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "初始化页面失败：" + ex.Message);
        }
    }
    //填充数据
    private void FillData()
    {
        IList list = null;
        if (cmd == "add")
        {
            list = (IList)Session[sessionName];
        }
        else if (cmd == "edit")
        {
            if (Session[sessionName] == null)
            {
                try
                {
                    ArchivesInfo item = bll.GetArchives(id);
                    //填充页面
                    tbArchivesName.Text = item.ArchivesName;
                    tbArchivesTypeName.Text = item.ArchivesTypeName.Trim();
                    tbArchivesTypeID.Text = (item.ArchivesTypeID).ToString();
                    tbInvolvedSystem.Text = item.InvolvedSystem;
                    tbInvolvedEquipment.Text = item.InvolvedEquipment;
                    tbDescription.Text = item.Description;
                    tbRemark.Text = item.Remark;

                    list = item.AttachmentList;
                    Session[sessionName] = list;
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "加载信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                }
            }
            else
            {
                list = (IList)Session[sessionName];
            }
        }
        if (list == null)//如果列表为空，填充回去
        {
            list = new List<ArchivesAttachmentInfo>();
            if (cmd == "add")
            {
                Session[sessionName] = list;
            }
            else if(cmd == "edit")
            {
                ArchivesInfo archives = (ArchivesInfo)Session[sessionName4Edit];
                archives.AttachmentList = list;
                Session[sessionName4Edit] = archives;
            }
        }

        gridview_ItemList.DataSource = list;
        gridview_ItemList.DataBind();
    }
    /// <summary>
    /// 按钮绑定
    /// </summary>
    private void ButtonBind()
    {
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：信息添加";
        }
        if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：信息修改";
        }
    }
    /// <summary>
    /// 确定按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSave_Click(object sender, EventArgs e)
    {
        ArchivesInfo item = new ArchivesInfo();
        //获取填入数据
        item.ArchivesID = id;
        item.ArchivesName = tbArchivesName.Text.Trim();
        item.ArchivesTypeID = Convert.ToInt64(tbArchivesTypeID.Text.Trim());
        item.InvolvedSystem = tbInvolvedSystem.Text.Trim();
        item.InvolvedEquipment = tbInvolvedEquipment.Text.Trim();
        item.Description = tbDescription.Text.Trim();
        item.Remark = tbRemark.Text.Trim();
        item.AttachmentList = (IList)Session[sessionName];

        //增加
        if (cmd == "add")
        {
            bool bSuccess = false;
            try
            {
                bll.InsertArchivesDetails(item);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Archives.aspx"), UrlType.Href, "");
            }
        }
        //修改
        else if (cmd == "edit")
        {
            bool bSuccess = false;
            try
            {
                bll.UpdateArchivesDetails(item);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Archives.aspx"), UrlType.Href, "");
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
        tbArchivesTypeID.Text = this.TreeView1.SelectedValue;

        PopupControlExtender1.Commit(tbArchivesTypeName.Text);
        PopupControlExtender2.Commit(tbArchivesTypeID.Text);
    }


    /// <summary>
    /// 删除行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridview_ItemList_RowDeleted(object sender, GridViewDeleteEventArgs e)
    {
        int itemID = (int)gridview_ItemList.DataKeys[e.RowIndex].Values["ItemID"];

        IList list = null;
        if (cmd =="add")  //新增
            list = (IList)Session[sessionName];
        else if(cmd == "edit")
        {
            ArchivesInfo archives = (ArchivesInfo)Session[sessionName4Edit];
            if (archives == null)
            {
                EventMessage.MessageBox(Msg_Type.Error, "获取档案信息失败", "该档案信息可能已经被删除，请刷新后重试",
                Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
                return;
            }
            list = archives.AttachmentList;
        }

        if (list == null || list.Count == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "删除失败", "附件项:" + itemID + "已经不存在，请刷新",
               Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }

        list.RemoveAt(itemID - 1);
        //重新更新ITEMID
        int id = 1;
        foreach (ArchivesAttachmentInfo item in list)
        {
            item.ItemID = id;
            id++;
        }
        Session[sessionName] = list;
        FillData();
    }

    /// <summary>
    /// 数据绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridview_ItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    /// <summary>
    /// 后台添加新项
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_AddItem_Click(object sender, EventArgs e)
    {
        string selectedString = Hidden_SelectedItem.Value;
        string[] array = selectedString.Split('|');//分割，分别是档案描述、档案保存路径、备注

        if (array.Length != 4)
        {
            EventMessage.MessageBox(Msg_Type.Error, "选取附件资料失败", "档案描述、档案保存路径、备注中有项缺失，请重试",
                Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }

        string description = array[0];
        string savepath = array[1];
        string archivesattachmentname = array[2];
        string remark = array[3];
        //获取当前工作的LIST
        IList list = null;
        if (cmd == "add")
            list = (IList)Session[sessionName];
        else if(cmd == "edit")
        {
            ArchivesInfo item = (ArchivesInfo)Session[sessionName4Edit];
            if (item == null)
            {
                EventMessage.MessageBox(Msg_Type.Error, "获取档案详细信息失败", "该档案详细信息可能已经被删除，请刷新后重试",
                Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
                return;
            }
            list = item.AttachmentList;
        }
        if (list == null)//如果列表为空，填充回去
        {
            list = new List<ArchivesAttachmentInfo>();
            if (cmd == "add")
            {
                Session[sessionName] = list;
            }
            else if(cmd == "edit")
            {
                ArchivesInfo item = (ArchivesInfo)Session[sessionName4Edit];
                item.AttachmentList = list;
                Session[sessionName4Edit] = item;
            }
        }

        ArchivesAttachmentInfo attachitem = new ArchivesAttachmentInfo();

        attachitem.ItemID = list.Count + 1;//下一序号
        attachitem.SavePath = savepath;
        attachitem.ArchivesAttachmentName = archivesattachmentname;
        attachitem.Description = description;
        attachitem.Remark = remark;
        list.Add(attachitem);
        Session[sessionName] = list;

        FillData();
    }


    /// <summary>
    /// 编辑一行的时候，进行保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        int itemID = int.Parse(Hidden_EditItemID.Value);
        IList list = null;
        if (cmd == "add")
            list = (IList)Session[sessionName];
        else if(cmd == "edit")
        {
            ArchivesInfo archives = (ArchivesInfo)Session[sessionName4Edit];
            if (archives == null)
            {
                EventMessage.MessageBox(Msg_Type.Error, "获取档案详细信息失败", "该档案信息可能已经被删除，请刷新后重试",
                Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
                return;
            }
            list = archives.AttachmentList;
        }
        if (list == null)//如果列表为空，填充回去
        {
            list = new List<ArchivesAttachmentInfo>();
            if (cmd == "add")
            {
                Session[sessionName] = list;
            }
            else if (cmd == "edit")
            {
                ArchivesInfo archives = (ArchivesInfo)Session[sessionName4Edit];
                archives.AttachmentList = list;
                Session[sessionName4Edit] = archives;
            }
        }
        ArchivesAttachmentInfo target = null;
        foreach (ArchivesAttachmentInfo item in list)
        {
            if (item.ItemID == itemID)
                target = item;
        }

        string archivesattachmentname = tbAttachmentName.Text.Trim();
        string description = tbAttachmentDescription.Text.Trim();
        string savepath = tbAttachmentSavePath.Text.Trim();
        string remark = tbAttachmentRemark.Text.Trim();
        if (target != null)
        {
            target.Description = description;
            target.SavePath = savepath;
            target.ArchivesAttachmentName = archivesattachmentname;
            target.Remark = remark;
        }
        Session[sessionName] = list;
        FillData();
    }

}
