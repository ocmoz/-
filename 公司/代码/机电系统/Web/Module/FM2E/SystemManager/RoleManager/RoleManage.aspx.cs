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

public partial class Module_FM2E_SystemManager_RoleManager_RoleManage : System.Web.UI.Page
{
    protected string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);

        if (!IsPostBack)
        {
            FillData();
            ButtonBind();
        }
    }

    private void FillData()
    {
        Role bll = new Role();

        if (cmd == "view" || cmd == "edit")
        {
            RoleInfo roleInfo = bll.GetRole(id);

            Label1.Text = roleInfo.RoleID.ToString();
            TextBox1.Text = Label2.Text = roleInfo.RoleName.ToString();
            TextBox2.Text = Label3.Text = roleInfo.Description.ToString();

        }
        else if (cmd == "delete")
        {
            bool bSuccess = false;
            try
            {
                bll.DeleteRole(id);
                bSuccess = true;

                //从缓存中清除关于此角色的权限资料
                UserData.RemoveRoleUserPermissionCache(id);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除角色失败" ,ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info , "操作成功", "删除角色成功！", Icon_Type.OK, true , Common.GetHomeBaseUrl("RoleList.aspx"), UrlType.Href, "");
            }
        }

    }

    private void ButtonBind()
    {
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：添加角色";
            TabContainer1.Tabs[0].HeaderText = "添加角色";
            roleIDTr.Visible = false;
            HideDisplay();
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：修改角色";
            TabContainer1.Tabs[0].HeaderText = "修改角色";

            //添加返回及删除按钮
            HeadMenuButtonItem backItem = new HeadMenuButtonItem();
            backItem.ButtonName = "返回";
            backItem.ButtonIcon = "back.gif";
            backItem.ButtonUrl = "window.history.go(-1);";
            backItem.ButtonUrlType = UrlType.JavaScript;
            backItem.ButtonPopedom = PopedomType.List;
            HeadMenuWebControls1.ButtonList.Add(backItem);

            HeadMenuButtonItem delItem = new HeadMenuButtonItem();
            delItem.ButtonName = "删除";
            delItem.ButtonIcon = "delete.gif";
            delItem.ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');", id);
            delItem.ButtonUrlType = UrlType.JavaScript;
            delItem.ButtonPopedom = PopedomType.Delete;
            HeadMenuWebControls1.ButtonList.Add(delItem);


            roleIDTr.Visible = true;
            HideDisplay();
        }
        else if (cmd == "view")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：查看角色";
            TabContainer1.Tabs[0].HeaderText = "查看角色";

            //添加修改角色资料及配置角色权限按钮
            HeadMenuButtonItem modifyItem = new HeadMenuButtonItem();
            modifyItem.ButtonName = "修改角色资料";
            modifyItem.ButtonIcon = "edit.gif";
            modifyItem.ButtonUrl = string.Format("?cmd=edit&id={0}", id);
            modifyItem.ButtonUrlType = UrlType.Href;
            modifyItem.ButtonPopedom = PopedomType.Edit;
            HeadMenuWebControls1.ButtonList.Add(modifyItem);

            HeadMenuButtonItem setItem = new HeadMenuButtonItem();
            setItem.ButtonName = "配置角色权限";
            setItem.ButtonIcon = "set.gif";
            setItem.ButtonUrl = string.Format("RolePermission.aspx?cmd=view&id={0}&roleName={1}", id,Server.HtmlEncode(Label2.Text.Trim()));
            setItem.ButtonUrlType = UrlType.Href;
            setItem.ButtonPopedom = PopedomType.Edit;
            HeadMenuWebControls1.ButtonList.Add(setItem);

            //HeadMenuButtonItem setWFItem = new HeadMenuButtonItem();
            //setWFItem.ButtonName = "配置工作流角色";
            //setWFItem.ButtonIcon = "set.gif";
            //setWFItem.ButtonUrl = string.Format("WorkflowRoleManage.aspx?cmd=view&id={0}&roleName={1}", id, Label2.Text.Trim());
            //setWFItem.ButtonUrlType = UrlType.Href;
            //setWFItem.ButtonPopedom = PopedomType.Edit;
            //HeadMenuWebControls1.ButtonList.Add(setWFItem);

            roleIDTr.Visible = true;
            HideEdit();
        }
    }

    private void HideEdit()
    {
        Label2.Visible = true;
        Label3.Visible = true;
        TextBox1.Visible = false;
        TextBox2.Visible = false;
        PostButton.Visible = false;
    }

    private void HideDisplay()
    {
        Label2.Visible = false;
        Label3.Visible = false;
        TextBox1.Visible = true;
        TextBox2.Visible = true;
        PostButton.Visible = true;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        RoleInfo roleInfo = new RoleInfo();
        try
        {
            roleInfo.RoleName = TextBox1.Text.Trim();
            if (roleInfo.RoleName == string.Empty)
                throw new WebException("角色名称不能为空");

            roleInfo.Description = TextBox2.Text.Trim();
        }
        catch (WebException ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加角色资料失败",ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
        }

        Role bll = new Role();
        if (cmd == "add")
        {
            bool bSuccess = false;
            try
            {
                bll.AddRole(roleInfo);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加角色资料失败",ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加角色资料成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("RoleList.aspx"), UrlType.Href, "");
            }
        }
        else if (cmd == "edit")
        {
            bool bSuccess = false;
            try
            {
                roleInfo.RoleID = Convert.ToInt64(Label1.Text.Trim());
                bll.UpdateRole(roleInfo);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改角色资料失败",ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改角色资料成功！", Icon_Type.OK, true , Common.GetHomeBaseUrl("RoleList.aspx"), UrlType.Href, "");
            }
        }
    }
}
