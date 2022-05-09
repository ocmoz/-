using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

using WebUtility;
using WebUtility.Components;
using WebUtility.WebControls;

using FM2E.BLL.System;
using FM2E.Model.System;
using FM2E.Model.Exceptions;
using System.Text;
using System.Xml;

public partial class Module_FM2E_SystemManager_ModuleManager_ModuleManager : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private string id = null;

    [Serializable]
    private class PageInfo
    {
        private string pageName;
        public string PageName
        {
            get { return pageName; }
            set { pageName = value; }
        }

        public PageInfo(string pageName)
        {
            this.pageName = pageName;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        id = (string)Common.sink("id", MethodType.Get, 50, 0, DataType.Str);
        if (id == null || id.Trim() == string.Empty)
            id = Guid.Empty.ToString("N");

        SystemPermission.CheckCommandPermission(cmd);

        TextBox4.Attributes.Add("ReadOnly", "ReadOnly");

        if (!IsPostBack)
        {
            //校验是否有权限执行此cmd
            SystemPermission.CheckCommandPermission(cmd);

            BuildTree();
            FillData();
            ButtonBind();
            PermissionControl();
        }
    }
    private void PermissionControl()
    {
        if (SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[7].Visible = true;
        else GridView1.Columns[7].Visible = false;

        if (SystemPermission.CheckPermission(PopedomType.Edit))
        {
            TabContainer1.Tabs[2].Visible = true;
            Button4.Visible = true;
        }
        else 
        { 
            TabContainer1.Tabs[2].Visible = false;
            Button4.Visible = false;
        }
    }

    private void BuildTree()
    {
        string virtualPath = "~/Module/FM2E";
        string startPath = Server.MapPath(virtualPath);

        DirectoryInfo dirInfo = new DirectoryInfo(startPath);
        DirectoryInfo[] dirs = dirInfo.GetDirectories();
        foreach (DirectoryInfo dir in dirs)
        {
            if (dir.Name == ".svn" || dir.Name == "_svn") continue;
            TreeNode node = new TreeNode(dir.Name, virtualPath + "/" + dir.Name);
            node.PopulateOnDemand = true;
            node.SelectAction = TreeNodeSelectAction.Expand;
            node.Expanded = false;
            pageList.Nodes.Add(node);
        }
        FileInfo[] files = dirInfo.GetFiles();
        foreach (FileInfo file in files)
        {
            if (file.Extension != ".aspx" && file.Extension != ".asp" && file.Extension != ".html" && file.Extension != ".htm")
                continue;
            TreeNode node = new TreeNode(file.Name, virtualPath + "/" + file.Name);
            node.Expanded = false;
            node.SelectAction = TreeNodeSelectAction.Select;
            pageList.Nodes.Add(node);
        }

    }

    protected void PopulateNode(Object sender, TreeNodeEventArgs e)
    {
        string path = e.Node.Value;
        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath(path));
        DirectoryInfo[] dirs = dirInfo.GetDirectories();
        foreach (DirectoryInfo dir in dirs)
        {
            if (dir.Name == ".svn") continue;
            TreeNode node = new TreeNode(dir.Name, path + "/" + dir.Name);
            node.PopulateOnDemand = true;
            node.Expanded = false;
            node.SelectAction = TreeNodeSelectAction.Expand;
            e.Node.ChildNodes.Add(node);
        }
        FileInfo[] files = dirInfo.GetFiles();
        foreach (FileInfo file in files)
        {
           if (file.Extension != ".aspx" && file.Extension != ".asp" && file.Extension != ".html" && file.Extension != ".htm" && file.Extension != ".chm")
                continue;
            TreeNode node = new TreeNode(file.Name, path + "/" + file.Name);
            node.Expanded = false;
            node.SelectAction = TreeNodeSelectAction.Select;
            e.Node.ChildNodes.Add(node);
        }
    }

    protected void Select_Change(Object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(pageList.SelectedNode.Value))
        {
            int len = pageList.SelectedNode.Value.Length;

            PopupControlExtender1.Commit(pageList.SelectedNode.Value.Substring(2, len - 2));
        }
        else
        {
            PopupControlExtender1.Cancel();
        }
    }


    private void FillData()
    {
        // string aaa= FileUpload1.TemplateSourceDirectory;
        Module bll = new Module();
        ModuleInfo item = bll.GetModule(id);
        ViewState["ModuleInfo"] = item;

        if (cmd == "add")
        {
            //添加时，需要以id为父模块，然后再添加子模块
            //添加有两种情况，一是添加最上层的模块，二是添加第二层以下的模块
            //添加最上层模块时，传进来的参数id=000000....
            //添加第二层以下的模块时，传进来的参数id为父模块
            Label1.Text = Guid.NewGuid().ToString("N");
            ParentID.Value = id;
            if (item != null)
                Label3.Text = item.Name;
            else Label3.Text = "无";
        }
        else if (cmd == "edit" || cmd == "view")
        {
            //修改和查看时，需要先查询出模块的信息
            //修改和查看时，传进来的参数id为本模块id
            if (item != null)
            {
                Label1.Text = item.ModuleID;
                Label2.Text = item.Name;
                TextBox2.Text = item.Name;

                if (item.ParentID == Guid.Empty.ToString("N"))
                    Label3.Text = "无";
                else
                {
                    ModuleInfo tmp = bll.GetModule(item.ParentID);
                    Label3.Text = tmp.Name;
                }
                ParentID.Value = item.ParentID;

                Label4.Text = (item.Directory == null || item.Directory.Trim() == string.Empty) ? "无" : item.Directory;
                TextBox4.Text = Label4.Text;
                Label5.Text = item.IsSystem ? "是" : "否";
                DropDownList1.SelectedValue = item.IsSystem ? "1" : "0";
                Label6.Text = item.IsClose ? "是" : "否";
                DropDownList2.SelectedValue = item.IsClose ? "1" : "0";
            }
        }

        //显示出子模块列表
        IList subModules = bll.GetSubModules(id, true);
        GridView1.DataSource = subModules;
        GridView1.DataBind();

        ReorderList1.DataSource = subModules;
        ReorderList1.DataBind();

        if (subModules == null || subModules.Count == 0)
        {
            TabContainer1.Tabs[1].Visible = false;
            TabContainer1.Tabs[2].Visible = false;
        }
        else
        {
            TabContainer1.Tabs[1].Visible = true;
            TabContainer1.Tabs[2].Visible = true;
        }

        List<string> order = new List<string>();
        for (int i = 0; i < subModules.Count; i++)
        {
            order.Add(((ModuleInfo)subModules[i]).ModuleID);
        }
        Session["order"] = order;

        if (cmd == "add")
        {
            HideDisplay();
        }
        if (cmd == "edit")
        {
            HideDisplay();
        }
        if (cmd == "view")
        {
            if (!(subModules == null || subModules.Count == 0))
                TabContainer1.ActiveTabIndex = 1;
            HideEdit();
        }
    }

    private void HideEdit()
    {
        Label2.Visible = true;
        Label4.Visible = true;
        Label5.Visible = true;
        Label6.Visible = true;
        TextBox2.Visible = false;
        TextBox4.Visible = false;
        btClearUrl.Visible = false;
        DropDownList1.Visible = false;
        DropDownList2.Visible = false;
        PostButton.Visible = false;
        lbStar.Visible = false;
    }

    private void HideDisplay()
    {
        Label2.Visible = false;
        Label4.Visible = false;
        Label5.Visible = false;
        Label6.Visible = false;
        TextBox2.Visible = true;
        TextBox4.Visible = true;
        btClearUrl.Visible = true;
        DropDownList1.Visible = true;
        DropDownList2.Visible = true;
        PostButton.Visible = true;
        lbStar.Visible = true;
    }

    private void ButtonBind()
    {
        ModuleInfo module = (ModuleInfo)ViewState["ModuleInfo"];
        if (module != null && module.ParentID != Guid.Empty.ToString("N"))
        {
            HeadMenuButtonItem itemBack = new HeadMenuButtonItem();
            itemBack.ButtonName = "返回上一级模块";
            itemBack.ButtonIcon = "back.gif";
            itemBack.ButtonPopedom = PopedomType.List;
            itemBack.ButtonUrl = string.Format("?cmd=view&id={0}", module.ParentID);
            HeadMenuWebControls1.ButtonList.Add(itemBack);
        }
        if (cmd == "add")
        {

        }
        else if (cmd == "edit")
        {
            //添加新增与删除按钮
            string moduleName = Label2.Text.Trim();
            HeadMenuButtonItem itemAdd = new HeadMenuButtonItem();
            itemAdd.ButtonName = "新增模块";
            itemAdd.ButtonIcon = "new.gif";
            itemAdd.ButtonPopedom = PopedomType.New;
            itemAdd.ButtonUrl = "?cmd=add&id=" + id;
            HeadMenuWebControls1.ButtonList.Add(itemAdd);

            HeadMenuButtonItem itemDel = new HeadMenuButtonItem();
            itemDel.ButtonName = "删除";
            itemDel.ButtonIcon = "delete.gif";
            itemDel.ButtonPopedom = PopedomType.Delete;
            itemDel.ButtonUrl = string.Format("DelData('?cmd=delete&id={0}')", id);
            itemDel.ButtonUrlType = UrlType.JavaScript;
            HeadMenuWebControls1.ButtonList.Add(itemDel);
        }
        else if (cmd == "view")
        {
            //添加新增与修改按钮
            //view 有两种情况，一是查看最上层的模块，二是查看第二层以下的模块
            //对于查看最上层模块的情况，传进来的参数id为本模块id，parentName为"无"
            string moduleName = Label2.Text.Trim();
            HeadMenuButtonItem itemAdd = new HeadMenuButtonItem();
            itemAdd.ButtonName = "新增模块";
            itemAdd.ButtonIcon = "new.gif";
            itemAdd.ButtonPopedom = PopedomType.New;
            itemAdd.ButtonUrl = "?cmd=add&id=" + id;
            HeadMenuWebControls1.ButtonList.Add(itemAdd);

            HeadMenuButtonItem itemEdit = new HeadMenuButtonItem();
            itemEdit.ButtonName = "修改";
            itemEdit.ButtonIcon = "edit.gif";
            itemEdit.ButtonPopedom = PopedomType.Edit;
            itemEdit.ButtonUrl = "?cmd=edit&id=" + id;
            HeadMenuWebControls1.ButtonList.Add(itemEdit);
        }
        else if (cmd == "delete")
        {
            if (module != null && module.IsSystem == true)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "不能删除系统模块", Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }

            bool bSuccess = false;
            try
            {
                Module bll = new Module();
                bll.DeleteModule(id);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除模块失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                string url = null;
                if (module.ParentID != Guid.Empty.ToString("N"))
                    url = "ModuleManager.aspx?cmd=view&id=" + module.ParentID;
                else url = "Modulelist.aspx";

                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除模块(ID:" + id + ")成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl(url), UrlType.Href, "");
            }
        }
    }
    /// <summary>
    /// 清空用户输入的URL
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btClearUrl_Click(object sender, EventArgs e)
    {
        TextBox4.Text = "";
    }
    /// <summary>
    /// 添加/修改模块
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        //获取用户输入
        ModuleInfo module = new ModuleInfo();
        try
        {
            module.ModuleID = Label1.Text.Trim();
            module.Name = TextBox2.Text.Trim();
            if (module.Name == string.Empty)
                throw new WebException("模块名称不能为空");

            module.ParentID = ParentID.Value;

            module.Directory = TextBox4.Text.Trim();
            if (module.Directory == "无")
                module.Directory = "";

            if (DropDownList1.SelectedValue == "0")
                module.IsSystem = false;
            else module.IsSystem = true;

            if (DropDownList2.SelectedValue == "0")
                module.IsClose = false;
            else module.IsClose = true;
        }
        catch (WebException ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加模块失败",ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        bool bSuccess = false;
        Module bll = new Module();
        if (cmd == "add")
        {
            try
            {
                module.OrderLevel = GridView1.Rows.Count + 1;
                bll.AddModule(module);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加模块失败",ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                string url = null;
                if (module.ParentID != Guid.Empty.ToString("N"))
                    url = "ModuleManager.aspx?cmd=view&id=" + module.ParentID;
                else url = "Modulelist.aspx";

                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加模块成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl(url), UrlType.Href, "");
            }
        }
        if (cmd == "edit")
        {
            try
            {
                module.ChildCount = GridView1.Rows.Count;
                bll.UpdateModule(module);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改模块信息失败",ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改模块信息成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ModuleManager.aspx?cmd=view&id=" + id), UrlType.Href, "");
            }
        }
    }
    protected void ReorderList1_ItemReorder(object sender, AjaxControlToolkit.ReorderListItemReorderEventArgs e)
    {
        List<string> order = (List<string>)Session["order"];
        if (order == null)
            return;

        string tmp = order[e.OldIndex];
        order.RemoveAt(e.OldIndex);
        order.Insert(e.NewIndex, tmp);

        Session["order"] = order;
    }
    /// <summary>
    /// 重排模块顺序
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;
        try
        {
            List<string> order = (List<string>)Session["order"];
            Module module = new Module();
            module.OrderModules(order.ToArray());
            bSuccess = true;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "模块分类排序失败" , ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        if (bSuccess)
        {
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "模块分类排序成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ModuleManager.aspx?cmd=" + cmd + "&id=" + id), UrlType.Href, "");
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        string id = gvRow.Attributes["ModuleID"];

        if (e.CommandName == "view")
        {
            Response.Redirect("ModuleManager.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "del")
        {
            ModuleInfo module = (ModuleInfo)ViewState["ModuleInfo"];

            if (gvRow.Attributes["IsSystem"].ToLower() == "true")
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "不能删除系统模块", Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }

            bool bSuccess = false;
            try
            {
                Module bll = new Module();
                bll.DeleteModule(id);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除模块失败",ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                string url = null;
                if (module.ModuleID != Guid.Empty.ToString("N"))
                    url = "ModuleManager.aspx?cmd=view&id=" + module.ModuleID;
                else url = "Modulelist.aspx";
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除模块成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl(url), UrlType.Href, "");
            }
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

            ModuleInfo item = (ModuleInfo)e.Row.DataItem;
            e.Row.Attributes["ModuleID"] = item.ModuleID;
            e.Row.Attributes["IsSystem"] = item.IsSystem.ToString();
        }
    }

#region 页面配置相关的操作
    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory">当前目录的虚拟路径</param>
    private void LoadPageSet(string directory)
    {
        ArrayList pageList = new ArrayList();
        try
        {
            if (directory != string.Empty)
            {
                string startPath = Server.MapPath("~/" + directory);

                DirectoryInfo dirInfo = new DirectoryInfo(startPath);
                FileInfo[] files = dirInfo.GetFiles();

                foreach (FileInfo file in files)
                {
                    if (file.Extension != ".aspx" && file.Extension != ".asp" && file.Extension != ".html" && file.Extension != ".htm")
                        continue;

                    pageList.Add(new PageInfo(file.Name));
                }
            }
        }
        catch (Exception e)
        {
            pageList.Clear();
            EventMessage.EventWriteLog(Msg_Type.Error, e.Message);
        }
        if (pageList.Count > 0)
        {
            Button5.Visible = true;
            warnMsg.Visible = false;
        }
        else
        {
            Button5.Visible = false;
            warnMsg.Visible = true;
        }

        ViewState["pages"] = pageList;
        PageRepeater.DataSource = pageList;
        PageRepeater.DataBind();
    }

    protected void PageRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        PageInfo pageInfo = (PageInfo)e.Item.DataItem;   //页面名称

        Permission permission = (Permission)Session["Permission"];
        int permissionValue = 0;
        if (permission != null)
            permissionValue = SystemPermission.Get_PermissionValue(permission.ItemList, string.Format(",{0},",pageInfo.PageName));

        int tempStep = 1;
        string selectTxt = "";
        for (int i = 0; i < 8; i++)
        {
            tempStep = tempStep + tempStep;

            Literal li = (Literal)e.Item.FindControl(string.Format("Lab{0}_Txt", tempStep));
            if (li != null)
            {
                if (permissionValue == 0)
                {
                    selectTxt = "";
                }
                else if ((permissionValue & tempStep) == tempStep)
                {
                    selectTxt = "checked";
                }
                else
                {
                    selectTxt = "";
                }

                li.Text = string.Format("<input type=checkbox id='Page_{0}' name='Page_{0}' value={1}  {2}>", pageInfo.PageName.Replace(".", "+"), tempStep, selectTxt);
            }
        }
    }
    /// <summary>
    /// 弹出页面配置对话框
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            if (TextBox2.Text.Trim() == string.Empty)
                throw new WebException("请先输入模块名称");

            string directory = TextBox4.Text.Trim();
            directory = directory.Substring(0, directory.LastIndexOf("/") + 1);
            Permission permission = LoadXmlConfig(directory);
            Session["Permission"] = permission;
            LoadPageSet(directory);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", ex.Message, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        Button3_ModalPopupExtender.Show();
    }
    /// <summary>
    /// 生成页面配置文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            List<PermissionItem> permissionItems = GetPermissionTypeSelect();

            string directory = TextBox4.Text.Trim();
            directory = directory.Substring(0, directory.LastIndexOf("/") + 1);
            GenerateXMLConfig(directory, permissionItems);
            Session.Remove("Permission");
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "生成页面配置文件失败",ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        Button3_ModalPopupExtender.Hide();
    }

    /// <summary>
    /// 加载xml配置文件
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    private Permission LoadXmlConfig(string directory)
    {
        Permission permission = null;
        if (directory != string.Empty)
        {
            string startPath = Server.MapPath("~/" + directory);

            string filePath = startPath + "Web.config";

            permission = PermissionConfigFile.LoadXmlConfigFile(filePath);

        }
        return permission;
    }
    /// <summary>
    /// 生成模块相应的xml配置文件
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="items"></param>
    private void GenerateXMLConfig(string directory, List<PermissionItem> items)
    {
        if (directory != string.Empty)
        {
            string startPath = Server.MapPath("~/" + directory);

            string filePath = startPath + "/Web.config";
            Permission permission = new Permission();
            permission.ModuleID = Label1.Text.Trim();
            permission.ModuleName = TextBox2.Text.Trim();
            permission.ItemList = items;

            PermissionConfigFile.SaveXmlConfigFile(filePath, permission);
        }
    }
    /// <summary>
    /// 获取用户输入的页面配置
    /// </summary>
    /// <returns></returns>
    private List<PermissionItem> GetPermissionTypeSelect()
    {
        List<PermissionItem> list = new List<PermissionItem>();

        //暂时hard code了
        PermissionItem listItem = new PermissionItem();
        listItem.Item_Name = "列表/查看";
        listItem.Item_Value = 2;
        listItem.Item_FileList = "";
        list.Add(listItem);

        PermissionItem addItem = new PermissionItem();
        addItem.Item_Name = "新增";
        addItem.Item_Value = 4;
        addItem.Item_FileList = "";
        list.Add(addItem);

        PermissionItem editItem = new PermissionItem();
        editItem.Item_Name = "修改";
        editItem.Item_Value = 8;
        editItem.Item_FileList = "";
        list.Add(editItem);

        PermissionItem delItem = new PermissionItem();
        delItem.Item_Name = "删除";
        delItem.Item_Value = 16;
        delItem.Item_FileList = "";
        list.Add(delItem);

        PermissionItem printItem = new PermissionItem();
        printItem.Item_Name = "打印";
        printItem.Item_Value = 32;
        printItem.Item_FileList = "";
        list.Add(printItem);

        PermissionItem approvalItem = new PermissionItem();
        approvalItem.Item_Name = "审批";
        approvalItem.Item_Value = 64;
        approvalItem.Item_FileList = "";
        list.Add(approvalItem);

        PermissionItem aItem = new PermissionItem();
        aItem.Item_Name = "权限A";
        aItem.Item_Value = 128;
        aItem.Item_FileList = "";
        list.Add(aItem);

        PermissionItem bItem = new PermissionItem();
        bItem.Item_Name = "权限B";
        bItem.Item_Value = 256;
        bItem.Item_FileList = "";
        list.Add(bItem);

        string[] arrayInt;
        int permissionValue = 0;

        ArrayList lst = (ArrayList)ViewState["pages"];
        foreach (PageInfo item in lst)
        {
            string target = Request.Form["Page_" + item.PageName.Replace(".", "+")];
            if (target == null || target.Trim() == string.Empty)
            {
                permissionValue = 0;
            }
            else
            {
                arrayInt = target.Split(',');
                for (int i = 0; i < arrayInt.Length; i++)
                {
                    permissionValue = Convert.ToInt32(arrayInt[i]);
                    switch (permissionValue)
                    {
                        case 2:
                            listItem.Item_FileList += item.PageName + ",";
                            break;
                        case 4:
                            addItem.Item_FileList += item.PageName + ",";
                            break;
                        case 8:
                            editItem.Item_FileList += item.PageName + ",";
                            break;
                        case 16:
                            delItem.Item_FileList += item.PageName + ",";
                            break;
                        case 32:
                            printItem.Item_FileList += item.PageName + ",";
                            break;
                        case 64:
                            approvalItem.Item_FileList += item.PageName + ",";
                            break;
                        case 128:
                            aItem.Item_FileList += item.PageName + ",";
                            break;
                        case 256:
                            bItem.Item_FileList += item.PageName + ",";
                            break;
                        default:
                            listItem.Item_FileList += item.PageName + ",";
                            break;
                    }
                }
            }
        }
        foreach (PermissionItem item in list)
        {
            if (item.Item_FileList != string.Empty)
                item.Item_FileList = "," + item.Item_FileList;
        }
        return list;
    }
#endregion
}
