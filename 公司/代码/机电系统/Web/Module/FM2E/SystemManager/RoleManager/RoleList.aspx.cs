using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FM2E.BLL.System;
using FM2E.Model.System;
using WebUtility;
using WebUtility.Components;
using System.Collections;

public partial class Module_FM2E_SystemManager_RoleManager_RoleList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
            PermissionControl();
        }
    }
    private void PermissionControl()
    {
        if (SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[4].Visible = true;
        else GridView1.Columns[4].Visible = false;
    }
    private void FillData()
    {
        try
        {
            Role bll = new Role();
            int recordCount = 0;

            IList list = bll.GetRoleByPage(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out recordCount);

            GridView1.DataSource = list;
            GridView1.DataBind();

            AspNetPager1.RecordCount = recordCount;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取角色列表失败",ex, Icon_Type.Error, true  , "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long id = Convert.ToInt64(gvRow.Attributes["RoleID"]);

        if (e.CommandName == "view")
        {
            Response.Redirect("RoleManage.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "del")
        {
            bool bSuccess = false;
            try
            {
                Role bll = new Role();
                bll.DeleteRole(id);
                bSuccess = true;

                //从缓存中清除关于此角色的权限资料
                UserData.RemoveRoleUserPermissionCache(id);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除角色失败", ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除角色成功！", Icon_Type.OK, true , Common.GetHomeBaseUrl("RoleList.aspx"), UrlType.Href, "");
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

            RoleInfo item = (RoleInfo)e.Row.DataItem;
            e.Row.Attributes["RoleID"] = item.RoleID.ToString();
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }

}
