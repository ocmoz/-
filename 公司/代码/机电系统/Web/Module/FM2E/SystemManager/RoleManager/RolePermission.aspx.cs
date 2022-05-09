using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;
using FM2E.BLL.System;
using FM2E.Model.System;
using FM2E.Model.Exceptions;
using System.Collections;

public partial class Module_FM2E_SystemManager_RoleManager_RolePermission : System.Web.UI.Page
{
    protected  string cmd = "view";
    private string roleName = (string)Common.sink("roleName", MethodType.Get, 100, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);

    public string ModuleName = "FM2E";

    protected void Page_Load(object sender, EventArgs e)
    {
        roleName = Server.HtmlDecode(roleName);
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            FillData();
        }
        ButtonBind();
    }

    private void ButtonBind()
    {
        HeadMenuWebControls1.ButtonList[0].ButtonUrl = string.Format("RoleManage.aspx?cmd=view&id={0}", id);
        HeadMenuWebControls1.ButtonList[0].ButtonUrlType = UrlType.Href;
        HeadMenuWebControls1.ButtonList[0].ButtonPopedom = PopedomType.List;

       // HeadMenuWebControls1.ButtonList.RemoveAt(1);

        string mode = Mode.Value.Trim();
        cmd = mode;
        //if (mode == "view")
        //{
            HeadMenuButtonItem modifyItem = new HeadMenuButtonItem();
            modifyItem.ButtonName = "修改角色权限";
            modifyItem.ButtonIcon = "edit.gif";
            modifyItem.ButtonUrl = "ChangeToEditMode();";
            modifyItem.ButtonUrlType = UrlType.JavaScript;
            modifyItem.ButtonPopedom = PopedomType.Edit;

            HeadMenuWebControls1.ButtonList.Add(modifyItem);
        //}
        //else if (mode == "edit")
        //{
        //    HeadMenuButtonItem backItem = new HeadMenuButtonItem();
        //    backItem.ButtonName = "返回查看模式";
        //    backItem.ButtonIcon = "back.gif";
        //    backItem.ButtonUrl = "ChangeToViewMode();";
        //    backItem.ButtonUrlType = UrlType.JavaScript;
        //    HeadMenuWebControls1.ButtonList.Add(backItem);

        //    //HeadMenuButtonItem modifyItem = new HeadMenuButtonItem();
        //    //modifyItem.ButtonName = "修改角色权限";
        //    //modifyItem.ButtonIcon = "edit.gif";
        //    //modifyItem.ButtonUrl = "ChangeToEditMode();";
        //    //modifyItem.ButtonUrlType = UrlType.JavaScript;

        //    //HeadMenuWebControls1.ButtonList.Add(modifyItem);
        //}

        BindData(moduleTree.SelectedNode.Value);
    }

    private void FillData()
    {
        Label1.Text = roleName;

        Module bll = new Module();
        IList moduleList = bll.GetSubModules(Guid.Empty.ToString("N"), false);

        //build module tree
        TreeNode root = new TreeNode("FM2E", Guid.Empty.ToString("N"));
        moduleTree.Nodes.Add(root);

        foreach (object item in moduleList)
        {
            ModuleInfo moduleInfo = (ModuleInfo)item;
            if (moduleInfo.ChildCount <= 0)
                continue;
            else
            {
                //非叶子结点
                TreeNode node = new TreeNode(moduleInfo.Name, moduleInfo.ModuleID);
                node.Expanded = false;
                node.PopulateOnDemand = true;
                node.SelectAction = TreeNodeSelectAction.SelectExpand;
                root.ChildNodes.Add(node);
            }

        }

         root.Select();

    }

    protected void moduleTree_SelectedNodeChanged(object sender, EventArgs e)
    {
        ModuleName = moduleTree.SelectedNode.Text;

        BindData(moduleTree.SelectedNode.Value);
    }

    private void BindData(string moduleID)
    {
        Module bll = new Module();
        IList list = bll.GetSubModules(moduleID, false);

        ArrayList moduleIDs = new ArrayList();
        foreach (object item in list)
        {
            moduleIDs.Add(((ModuleInfo)item).ModuleID);
        }
        ViewState["moduleIDs"] = moduleIDs;

        ModuleSub.DataSource = list;
        ModuleSub.DataBind();
    }

    protected void moduleTree_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        Module bll = new Module();
        IList moduleList = bll.GetSubModules(e.Node.Value, false);
        foreach (object item in moduleList)
        {
            ModuleInfo moduleInfo = (ModuleInfo)item;
            if (moduleInfo.ChildCount <= 0)
                continue;
            else
            {
                //非叶子结点
                TreeNode node = new TreeNode(moduleInfo.Name, moduleInfo.ModuleID);
                node.Expanded = false;
                node.PopulateOnDemand = true;
                node.SelectAction = TreeNodeSelectAction.SelectExpand;
                e.Node.ChildNodes.Add(node);
            }
        }
    }

    #region 表格数据绑定

    protected void Module_Sub_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ModuleInfo subModule = (ModuleInfo)e.Item.DataItem;   //子模块

        string rightString = string.Format("<img src='{0}'>", ResolveClientUrl("~/images/right.gif"));
        string wrongString = string.Format("<img src='{0}'>", ResolveClientUrl("~/images/wrong.gif"));
        string dispString = "";
        string selectTxt = "";
        int tempStep = 1;

        RolePermission bll = new RolePermission();
        RolePermissionInfo permission = bll.GetRolePermissions(id, subModule.ModuleID);

        string mode = Mode.Value.Trim();

        for (int i = 0; i < 8; i++)
        {
            tempStep = tempStep + tempStep;

            Literal li = (Literal)e.Item.FindControl(string.Format("Lab{0}_Txt", tempStep));
            if (li != null)
            {
                if (permission == null)
                {
                    dispString = wrongString;
                    selectTxt = "";
                }
                else if ((permission.Permission & tempStep) == tempStep)
                {
                    dispString = rightString;
                    selectTxt = "checked";
                }
                else
                {
                    dispString = wrongString;
                    selectTxt = "";
                }
                if (mode == "edit")
                {
                    dispString = string.Format("<input type=checkbox id='PageCode{0}' name='PageCode{0}' value={1}  {2}>", subModule.ModuleID, tempStep, selectTxt);
                }
                li.Text = dispString;
            }
        }
    }

    #endregion

    private IList GetPermissionSelect()
    {
        ArrayList list = new ArrayList();

        string[] arrayInt;
        int permissionValue = 0;

        IList subModules = (ArrayList)ViewState["moduleIDs"];
        foreach (object item in subModules)
        {
            permissionValue = 0;

            RolePermissionInfo permission = new RolePermissionInfo();
            permission.RoleID = id;
            permission.ModuleID = item.ToString();
            string target = Request.Form["PageCode" + item];
            if (target==null||target.Trim() == string.Empty)
            {
                permission.Permission = 0;
            }
            else
            {
                arrayInt = target.Split(',');
                for (int i = 0; i < arrayInt.Length; i++)
                {
                    permissionValue = permissionValue + Convert.ToInt32(arrayInt[i]);
                }
                permission.Permission = permissionValue;
            }
            list.Add(permission);
        }
        
        return list;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;
        try
        {
            IList permissionList = GetPermissionSelect();
            RolePermission bll = new RolePermission();
            bll.UpdateRolePermissions(permissionList);
            bSuccess = true;

            UserData.RemoveRoleUserPermissionCache(id);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "更新角色权限失败",ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
        }
        if (bSuccess)
        {
            //重新绑定数据
            Mode.Value = "view";
            ButtonBind();
            ModuleName = moduleTree.SelectedNode.Text;

            BindData(moduleTree.SelectedNode.Value);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
}
