using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM2E.Model.System;
using FM2E.BLL.System;
using WebUtility;
using WebUtility.Components;
using System.Collections;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Exceptions;

public partial class Module_FM2E_SystemManager_UserManager_EditUser : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private string name = (string)Common.sink("name", MethodType.Get, 20, 0, DataType.Str);
    private readonly Position positionBll = new Position();
    private readonly Role roleBll = new Role();
    private readonly User userBll = new User();
    private const string UPLOADFOLDER = "UserPicture/";
    private const string PHOTOURL_VIEWSTATE = "PhotoUrl";
    private const string OLDPASSWORD_VIEWSTATE = "OldPassword";
    private readonly EquipmentSystem eqsysBll = new EquipmentSystem();
    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);

        if (!IsPostBack)
        {
            InitialPage();

            FillData();
            ButtonBind();
        }
    }
    /// <summary>
    /// 页面初始化
    /// </summary>
    private void InitialPage()
    {
        LoginUserInfo userInfo = UserData.CurrentUserData;

        if (!userInfo.IsAdministrator)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作无效", string.Format("用户{0}无法添加/修改用户", userInfo.UserName), Icon_Type.Error, true, Common.GetHomeBaseUrl("UserList.aspx"), UrlType.Href, "");
        }

        try
        {
            //cddCompany.SelectedValue = userInfo.CompanyID;
            IList<PositionInfo> positionList = positionBll.GetAllPosition();
            ddlPosition.Items.Clear();
            ddlPosition.Items.Add(new ListItem("请选择职位", "0"));
            foreach (PositionInfo item in positionList)
            {
                ddlPosition.Items.Add(new ListItem(item.PositionName, item.PositionID.ToString()));
            }
            //加载用户类型列表以及用户状态列表
            ListItem[] userTypeItems = EnumHelper.GetListItemsEx(typeof(UserType), (int)UserType.CommonUser, (int)UserType.Unknown);
            ListItem[] userStatusItems = EnumHelper.GetListItemsEx(typeof(UserStatus), (int)UserStatus.Normal, (int)UserStatus.Unknown);
            ListItem[] sexItems = EnumHelper.GetListItemsEx(typeof(Sex), (int)Sex.Male, (int)Sex.Unknown);

            ddlUserType.Items.Clear();
            ddlUserType.Items.AddRange(userTypeItems);

            ddlUserStatus.Items.Clear();
            ddlUserStatus.Items.AddRange(userStatusItems);

            rblSex.Items.Clear();
            rblSex.Items.AddRange(sexItems);

            //公司
            ddlCompany.Items.Clear();
            ddlCompany.Items.AddRange(ListItemHelper.GetCompanyListItemsWithBlank());
            ddlCompany.SelectedValue = UserData.CurrentUserData.CompanyID;

            //部门
            ddlDepartment.Items.Clear();
            ddlDepartment.Items.AddRange(ListItemHelper.GetDepartmentListItemsWithBlank());

            //是否系统工程师
            //ddlusersystemid.Items.Clear();
            //ddlusersystemid.Items.Add(new ListItem("否", "0"));
            //ddlusersystemid.Items.AddRange(ListItemHelper.GetSystemListItems());

            //cddCompany.SelectedValue = userInfo.CompanyID;
            //if (userInfo.IsParentCompany)
            //{
            //    ViewState["IsShow"] = true;
            //}
            //else
            //{
            //    ViewState["IsShow"] = false;
            //    lbCompany.Text = userInfo.CompanyName;
            //}
            //if (string.IsNullOrEmpty(userInfo.CompanyID))
            //{
            //    ViewState["IsShow"] = true;
            //}

            //加载角色列表
            IList roleList = roleBll.GetAllRole();

            mblRoles.FirstListBox.DataSource = roleList;
            mblRoles.DataBind();
            //加载系统列表
            IList sysList = eqsysBll.GetAllSystem();
            sysNames.FirstListBox.DataSource = sysList;
            sysNames.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    private void FillData()
    {
        imgPhotoThumb.Visible = false;
        btModifyPic.Visible = false;
        if (cmd == "edit")
        {
            FileUpload1.Visible = false;
            imgPhotoThumb.Visible = true ;
            btModifyPic.Visible = true ;
            try
            {
                
                UserInfo item = userBll.GetUser(name);

                if (item.UserName == Common.Get_UserName && !UserData.CurrentUserData.IsAdministrator)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作无效", "普通用户无法修改自己的资料!", Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                }
                //普通用户无法删除超级用户
                if (item.IsAdministrator && !UserData.CurrentUserData.IsAdministrator)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作无效", "普通用户无法修改超级用户资料!", Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                }

                //填充页面
                tbUserName.Text = item.UserName;
                tbPersonName.Text = item.PersonName;
                tbPassword.Attributes["value"] = item.Password;
                tbConfirmPassword.Attributes["value"] = item.Password;
                //Session[Common.Get_UserName + "Password"] = item.Password;
                ViewState[OLDPASSWORD_VIEWSTATE] = item.Password;

                ddlCompany.SelectedValue = item.CompanyID;
                ddlDepartment.SelectedValue = item.DepartmentID.ToString();
                ddlPosition.SelectedValue = item.PositionID.ToString();
                rblSex.SelectedValue = ((int)item.Sex).ToString();
                ddlUserType.SelectedValue = ((int)item.UserType).ToString();
                ddlUserStatus.SelectedValue = ((int)item.Status).ToString();
                tbStaffNO.Text = item.StaffNO;
                tbIDCard.Text = item.IDCard;
                if (DateTime.Compare(item.Birthday, DateTime.MinValue) != 0)
                    tbBirthday.Text = item.Birthday.ToString("yyyy-MM-dd");
                else tbBirthday.Text = "";
                tbOfficePhone.Text = item.OfficePhone;
                tbMobilePhone.Text = item.MobilePhone;
                tbHomePhone.Text = item.HomePhone;
                tbFax.Text = item.Fax;
                tbAddress.Text = item.Address;
                tbEmail.Text = item.Email;
                //tbIM.Text = item.IM;

                if (item.IM != "" && item.IM != null && item.IM!="0")
                {
                    if (item.IM.Contains("@"))
                    {
                        string[] containSystemID = item.IM.Split('@');
                        for (int i = 0; i < containSystemID.Count(); i++)
                        {
                            ListItem lSys = new ListItem();
                            lSys = sysNames.FirstListBox.Items.FindByValue(containSystemID[i]);
                            if (lSys != null)
                            {
                                sysNames.FirstListBox.Items.Remove(lSys);
                                sysNames.SecondListBox.Items.Add(lSys);
                            }
                        }
                    }
                    else
                    {
                        ListItem lSys = new ListItem();
                        lSys = sysNames.FirstListBox.Items.FindByValue(item.IM);
                        sysNames.FirstListBox.Items.Remove(lSys);
                        sysNames.SecondListBox.Items.Add(lSys);
                    }
                   
                }
                //ddlusersystemid.SelectedValue = item.IM;
                if (item.IM.Contains("@"))
                {
                    string[] containSystemID = item.IM.Split('@');
                    for (int i = 0; i < containSystemID.Count(); i++)
                    {
                        ListItem lSys = new ListItem();
                        lSys = sysNames.FirstListBox.Items.FindByValue(containSystemID[i]);
                        if (lSys!=null)
                        {
                            sysNames.FirstListBox.Items.Remove(lSys);
                            sysNames.SecondListBox.Items.Add(lSys);
                        }
                    }
                }
                tbResposibility.Text = item.Responsibility;
                ViewState[PHOTOURL_VIEWSTATE] = item.PhotoUrl;
                if (!string.IsNullOrEmpty(item.PhotoUrl))
                {
                    imgPhotoThumb.ImageUrl = item.PhotoUrl;
                    imgPhoto.ImageUrl = item.PhotoUrl;
                }
                else
                {
                    imgPhotoThumb.ImageUrl = "~/images/nopicture.gif";
                    imgPhoto.ImageUrl = "~/images/nopicture.gif";
                }

                if (item != null && item.Roles != null && item.Roles.Count != 0)
                {
                    foreach (object var in item.Roles)
                    {
                        UserRoleInfo role = (UserRoleInfo)var;

                        ListItem li = new ListItem();
                        li = mblRoles.FirstListBox.Items.FindByValue(role.RoleID.ToString());
                        if (li != null)
                        {
                            mblRoles.FirstListBox.Items.Remove(li);
                            mblRoles.SecondListBox.Items.Add(li);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "加载用户信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }

    }

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        imgPhotoThumb.Visible = true;
        FileUpload1.Visible = false;
        ButtonCancel.Visible = false;
        btModifyPic.Visible = true;

        //Session["NeedUpdatePhoto"] = false;
    }

    protected void btModifyPic_Click(object sender, EventArgs e)
    {
        imgPhotoThumb.Visible = false;
        FileUpload1.Visible = true;
        ButtonCancel.Visible = true;
        btModifyPic.Visible = false;
        //Session["NeedUpdatePhoto"] = true;

    }

    private void ButtonBind()
    {
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：用户信息添加";
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：用户信息修改";
        }
    }


    protected void btSave_Click(object sender, EventArgs e)
    {
        if (tbPassword.Text.Trim() != tbConfirmPassword.Text.Trim())
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加用户失败：密码与确认密码不相符，请检查输入", Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");

        UserInfo userInfo = new UserInfo();
        userInfo.UserName = tbUserName.Text.Trim().ToLower();
        string oldPassword = ViewState[OLDPASSWORD_VIEWSTATE] != null ? ViewState[OLDPASSWORD_VIEWSTATE].ToString() : "";
        string newPassword = tbPassword.Text.Trim();
        if (oldPassword == "" || oldPassword != newPassword)
            userInfo.Password = Common.md5(tbPassword.Text.Trim(), 32);
        else userInfo.Password = oldPassword;

        userInfo.Address = tbAddress.Text.Trim();
        if(tbBirthday.Text.Trim()!="")
            userInfo.Birthday = Convert.ToDateTime(tbBirthday.Text.Trim());
        userInfo.CompanyID = ddlCompany.SelectedValue;
        if (!string.IsNullOrEmpty(ddlDepartment.SelectedValue.Trim()))
            userInfo.DepartmentID = Convert.ToInt64(ddlDepartment.SelectedValue);
        else userInfo.DepartmentID = 0;

        userInfo.Email = tbEmail.Text.Trim() ;
        userInfo.Fax = tbFax.Text.Trim();
        userInfo.HomePhone = tbHomePhone.Text.Trim();
        userInfo.IDCard = tbIDCard.Text.Trim();
        //userInfo.IM = tbIM.Text.Trim();

        //userInfo.IM = ddlusersystemid.SelectedValue;

        userInfo.MobilePhone = tbMobilePhone.Text.Trim();
        userInfo.OfficePhone = tbOfficePhone.Text.Trim();
        userInfo.PersonName = tbPersonName.Text.Trim();
        if(!string.IsNullOrEmpty(ddlPosition.SelectedValue.Trim()))
            userInfo.PositionID = Convert.ToInt64(ddlPosition.SelectedValue);
        userInfo.Responsibility = tbResposibility.Text.Trim();
        userInfo.Sex = (Sex)Convert.ToInt32(rblSex.SelectedValue);
        userInfo.StaffNO = tbStaffNO.Text.Trim();
        userInfo.UserType = (UserType)Convert.ToInt32(ddlUserType.SelectedValue);
        userInfo.Status = (UserStatus)Convert.ToInt32(ddlUserStatus.SelectedValue);

        userInfo.PhotoUrl = (string)ViewState[PHOTOURL_VIEWSTATE];
        //对图片是否上传更新的选择处理
        FileUpLoadCommon fuc = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + UPLOADFOLDER, false);
        if (FileUpload1.HasFile)
        {
            if (fuc.SaveFile(FileUpload1.PostedFile,true,false))
            {
                if (!string.IsNullOrEmpty(userInfo.PhotoUrl))
                    FileUpLoadCommon.DeleteFile(userInfo.PhotoUrl);
                userInfo.PhotoUrl = SystemConfig.Instance.UploadPath + UPLOADFOLDER + fuc.NewFileName;
            }
            else
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "上传照片失败，原因："+fuc.ErrorMsg, new WebException(fuc.ErrorMsg), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                return;
            }
        }
        if (userInfo.PhotoUrl == null)
            userInfo.PhotoUrl = "";

        ArrayList list = new ArrayList();
        foreach (ListItem var in mblRoles.SecondListBox.Items)
        {
            UserRoleInfo item = new UserRoleInfo();
            item.RoleID = Convert.ToInt32(var.Value);
            item.UserName = name;
            list.Add(item);
        }

        string sysList = "";
        if (sysNames.SecondListBox.Items.Count !=0)
        {
            foreach (ListItem var in sysNames.SecondListBox.Items)
            {
                sysList += var.Value.ToString() + "@";
            }
            int jj = sysList.LastIndexOf("@");
            sysList = sysList.Remove(jj, 1);
            
        }
        userInfo.IM = sysList;
        userInfo.Roles = list;
        userInfo.UpdateTime = DateTime.Now;

        if (cmd == "add")
        {
            bool bSuccess = false;
            try
            {
                userBll.AddUser(userInfo);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.Message.IndexOf("违反了 PRIMARY KEY 约束") >= 0)
                        EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加用户失败，添加的用户名已存在", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                }
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加用户失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加用户成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("UserList.aspx"), UrlType.Href, "");
            }
        }
        else if (cmd == "edit")
        {
            bool bSuccess = false;
            try
            {
                userBll.UpdateUser(userInfo);
                bSuccess = true;

                UserData.RemoveUserCache(userInfo.UserName);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改用户失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改用户成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("UserList.aspx"), UrlType.Href, "");
            }
        }
    }
}
