using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using WebUtility.Components;
using FM2E.BLL.System;
using FM2E.Model.System;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;

public partial class Module_FM2E_SystemManager_UserManager_ViewUser : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private string name = (string)Common.sink("name", MethodType.Get, 20, 0, DataType.Str);
    private readonly User userBll = new User();
    private const string PHOTOURL_VIEWSTATE = "PhotoUrl";


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
        User userBll = new User();
        if (cmd == "view")
        {
            try
            {
                UserInfo userInfo = userBll.GetUser(name);
                if (userInfo == null)
                    return;

                lbUserName.Text = userInfo.UserName;
                lbPersonName.Text = userInfo.PersonName;
                lbCompany.Text = userInfo.CompanyName;
                lbDepartment.Text = userInfo.DepartmentName;
                lbPosition.Text = userInfo.PositionName;
                lbSex.Text = EnumHelper.GetDescription(userInfo.Sex);
                lbUserType.Text = EnumHelper.GetDescription(userInfo.UserType);
                lbUserStatus.Text = EnumHelper.GetDescription(userInfo.Status);


                if (userInfo.Roles != null && userInfo.Roles.Count > 0)
                {
                    foreach (object var in userInfo.Roles)
                    {
                        UserRoleInfo item = (UserRoleInfo)var;
                        ltRoles.Text += string.Format("·{0}&nbsp;&nbsp;", item.RoleName);
                    }
                }
                lbStaffNO.Text = userInfo.StaffNO;
                lbIDCard.Text = userInfo.IDCard;
                if (DateTime.Compare(userInfo.Birthday, DateTime.MinValue) != 0)
                    lbBirthday.Text = userInfo.Birthday.ToString("yyyy-MM-dd");
                lbOfficePhone.Text = userInfo.OfficePhone;
                lbMobilePhone.Text = userInfo.MobilePhone;
                lbHomePhone.Text = userInfo.HomePhone;
                lbFax.Text = userInfo.Fax;
                lbAddress.Text = userInfo.Address;
                lbEmail.Text = userInfo.Email;
                //lbIM.Text = userInfo.IM;

                //if (userInfo.IM == "0" || userInfo.IM == "")
                //{
                //    lbIM.Text = "否";
                //}
                //else
                //{
                //    lbIM.Text = (new FM2E.BLL.Basic.EquipmentSystem()).GetSystem(userInfo.IM).SystemName;
                //}
                string systemNames = "";
                if (userInfo.IM != "" && userInfo.IM != null && userInfo.IM!="0")
                {
                    if (userInfo.IM.Contains("@"))
                    {
                        string[] containSystemID = userInfo.IM.Split('@');
                        for (int i = 0; i < containSystemID.Count(); i++)
                        {
                            systemNames += (new FM2E.BLL.Basic.EquipmentSystem()).GetSystem(containSystemID[i]).SystemName+" ";
                        }
                    }
                    else
                    {
                        systemNames += (new FM2E.BLL.Basic.EquipmentSystem()).GetSystem(userInfo.IM).SystemName;
                    }
                }

                lbIM.Text = systemNames;
                lbRes.Text = userInfo.Responsibility;

                //判断图片是否存在

                if (userInfo.PhotoUrl != null && userInfo.PhotoUrl != "")
                {
                    if (System.IO.File.Exists(Server.MapPath(userInfo.PhotoUrl)))
                        imPhoto.ImageUrl = userInfo.PhotoUrl;
                    else
                        imPhoto.ImageUrl = "~/images/deletedpicture.gif";
                }
                else imPhoto.ImageUrl = "~/images/nopicture.gif";
                //ViewState[PHOTOURL_VIEWSTATE] = userInfo.PhotoUrl;
                Session[PHOTOURL_VIEWSTATE] = userInfo.PhotoUrl;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询用户信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        if (cmd == "delete")
        {
            //UserInfo userInfo = (UserInfo)Session["UserInfo" + name];

            if (name == Common.Get_UserName)
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
                userBll.DeleteUser(name);
                bSuccess = true;

                string path = (string)Session[PHOTOURL_VIEWSTATE];
                if (!string.IsNullOrEmpty(path))
                {
                    FileUpLoadCommon.DeleteFile(path);
                }
                //从缓存中移除用户以及权限
                UserData.RemoveUserCache(name);
                //从在线列表中移除
                SystemPermission.UserOnlineList.RemoveUserName(name);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除用户信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除用户" + name + "成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("UserList.aspx"), UrlType.Href, "");
            }
        }
    }

    private void ButtonBind()
    {
        HeadMenuWebControls1.ButtonList[0].ButtonUrl = "EditUser.aspx?cmd=edit&name=" + name;
        HeadMenuWebControls1.ButtonList[0].ButtonUrlType = UrlType.Href;
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = true;

        HeadMenuWebControls1.ButtonList[1].ButtonUrl = "EditUserWorkflowRole.aspx?name=" + name;
        HeadMenuWebControls1.ButtonList[1].ButtonUrlType = UrlType.Href;
        HeadMenuWebControls1.ButtonList[1].ButtonVisible = true;

        HeadMenuWebControls1.ButtonList[2].ButtonUrl = string.Format("DelData('?cmd=delete&name={0}');", name);
        HeadMenuWebControls1.ButtonList[2].ButtonUrlType = UrlType.JavaScript;
        HeadMenuWebControls1.ButtonList[2].ButtonVisible = true;

        if (name == Common.Get_UserName && !UserData.CurrentUserData.IsAdministrator)
        {
            //无法删除修改自己，非超级管理员无法添加/修改/删除用户
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
            HeadMenuWebControls1.ButtonList[2].ButtonVisible = false;
        }


    }
}
