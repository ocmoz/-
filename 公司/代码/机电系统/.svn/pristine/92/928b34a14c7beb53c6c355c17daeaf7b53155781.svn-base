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
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;

public partial class Module_FM2E_SystemManager_UserManager_UserList : System.Web.UI.Page
{
    private readonly User userBll = new User();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            PermissionControl();
        }
        ViewState["HaveAccessed"] = true;
    }
    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        try
        {
            ListItem[] userTypeItems = EnumHelper.GetListItems(typeof(UserType), (int)UserType.Unknown);
            ListItem[] userStatusItems = EnumHelper.GetListItems(typeof(UserStatus), (int)UserStatus.Unknown);

            ddlUserType.Items.Clear();
            ddlUserType.Items.Add(new ListItem("不限", ((int)UserType.Unknown).ToString()));
            ddlUserType.Items.AddRange(userTypeItems);

            ddlUserStatus.Items.Clear();
            ddlUserStatus.Items.Add(new ListItem("不限", ((int)UserStatus.Unknown).ToString()));
            ddlUserStatus.Items.AddRange(userStatusItems);

            //公司
            ddlCompany.Items.Clear();
            ddlCompany.Items.AddRange(ListItemHelper.GetCompanyListItemsWithBlank());
            //ddlCompany.SelectedValue = UserData.CurrentUserData.CompanyID;

            //部门
            ddlDepartment.Items.Clear();
            ddlDepartment.Items.AddRange(ListItemHelper.GetDepartmentListItemsWithBlank());

            //LoginUserInfo loginUser = UserData.CurrentUserData;
            //if (loginUser.IsParentCompany)
            //{
            //    ViewState["IsShow"] = true;
            //    cddCompany.SelectedValue = "";
            //}
            //else
            //{
            //    ViewState["IsShow"] = false;
            //    cddCompany.SelectedValue = loginUser.CompanyID;
            //    lbCompany.Text = loginUser.CompanyName;
            //}
        }
        catch (Exception ex)
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "初始化页面失败：" + ex.Message);
        }
    }
    private void PermissionControl()
    {
        if (SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[5].Visible = true;
        else GridView1.Columns[5].Visible = false;

        //只有超级用户才有权限删除用户
        if (UserData.CurrentUserData.IsAdministrator)
            GridView1.Columns[5].Visible = true;
        else GridView1.Columns[5].Visible = false;

    }
    private void FillData()
    {
        ////查询
        int recordCount = 0;
        IList list = userBll.GetList(GetSearchTerm(), AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out recordCount);
        GridView1.DataSource = list;
        GridView1.DataBind();
        AspNetPager1.RecordCount = recordCount;
        TabContainer1.ActiveTabIndex = 0;
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        string userName = gvRow.Attributes["UserName"];
        //string userType = gvRow.Attributes["UserType"];
        string photoUrl = gvRow.Attributes["PhotoUrl"];
        if (e.CommandName == "view")
        {
            //查看
            Response.Redirect("ViewUser.aspx?cmd=view&name=" + userName);
        }
        else if (e.CommandName == "del")
        {
            if (userName == Common.Get_UserName)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作无效", "用户无法删除自己!", Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            //普通用户无法删除用户
            if (!UserData.CurrentUserData.IsAdministrator)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作无效", "普通用户无法删除用户资料!", Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }

            bool bSuccess = false;
            try
            {
                userBll.DeleteUser(userName);
                bSuccess = true;
                if (!string.IsNullOrEmpty(photoUrl))
                    FileUpLoadCommon.DeleteFile(photoUrl);

                //从缓存中移除用户以及权限
                UserData.RemoveUserCache(userName);
                //从在线列表中移除
                SystemPermission.UserOnlineList.RemoveUserName(userName);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除用户失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除用户成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("UserList.aspx"), UrlType.Href, "");
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

            UserInfo item = (UserInfo)e.Row.DataItem;
            e.Row.Attributes["UserName"] = item.UserName;
            e.Row.Attributes["PhotoUrl"] = item.PhotoUrl;
            //e.Row.Attributes["UserType"] = item.UserType.ToString();
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        FillData();
        AspNetPager1.CurrentPageIndex = 1;
    }

    private UserSearchInfo GetSearchTerm()
    {
        //生成查询条件
        UserSearchInfo item = new UserSearchInfo();

        if (tbUserName.Text.Trim() != string.Empty)
        {
            item.UserName = Common.inSQL(tbUserName.Text.Trim());
        }
        if (tbPersonName.Text.Trim() != string.Empty)
        {
            item.PersonName = Common.inSQL(tbPersonName.Text.Trim());
        }

        
        item.UserType = (UserType)Convert.ToInt32(ddlUserType.SelectedValue);

        item.Status = (UserStatus)Convert.ToInt32(ddlUserStatus.SelectedValue);

        item.CompanyID = ddlCompany.SelectedValue;

        if (!string.IsNullOrEmpty(ddlDepartment.SelectedValue.Trim()))
        {
            item.DepartmentID = Convert.ToInt64(ddlDepartment.SelectedValue);
        }

        return item;
    }
}
