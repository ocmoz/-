using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WebUtility;
using WebUtility.Components;

using FM2E.Model.Exceptions;
using FM2E.Model.System;
using FM2E.BLL.System;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TextBox1.Focus();

        string CMD = (string)Common.sink("CMD", MethodType.Get, 255, 0, DataType.Str);
        if (CMD == "OutOnline")
        {
            string U_LoginName = (string)Common.sink("U_LoginName", MethodType.Get, 20, 1, DataType.Str);
            string U_Password = (string)Common.sink("U_Password", MethodType.Get, 32, 32, DataType.Str);
            string OPCode = (string)Common.sink("OPCode", MethodType.Get, 4, 4, DataType.Str);

            MessageBox MBx = new MessageBox();
            MBx.M_Type = Msg_Type.Error;
            MBx.M_Title = "强制下线！";
            MBx.M_IconType = Icon_Type.Error;
            MBx.M_Body = "强制下线失败.验证码无效，请确认您输入的验证码有效！";


            if (Session["CheckCode"] == null || OPCode != Session["CheckCode"].ToString())
            {
                MBx.M_ButtonList.Add(new sys_NavigationUrl("返回", "login.aspx", "点击按钮重新输入验证码！", UrlType.Href, true));
            }
            else
            {

                User bll = new User();
                LoginUserInfo userInfo = bll.UserLogin(U_LoginName, U_Password);

                if (userInfo != null)
                {
                    string sessionid = (string)Common.sink("SessionID", MethodType.Get, 24, 0, DataType.Str);
                    SystemLogin.RemoveOnlineUser(sessionid);
                    SystemLogin.AddOnlineUser(U_LoginName, userInfo.PersonName, Common.GetSessionID);
                    EventMessage.EventWriteLog(Msg_Type.Info, string.Format("欢迎您{0}，成功登入。您的IP为：{1}！", U_LoginName, Common.GetIPAddress()));
                    //
                    //return;
                    new SystemLogin().SignIn(userInfo);
                    Response.Redirect("Default.aspx");
                    //MBx.M_Type = Msg_Type.Info;
                    //MBx.M_IconType = Icon_Type.OK;
                    //MBx.M_Body = string.Format("强制帐号{0}下线成功.请重新登陆！", U_LoginName);
                    //MBx.M_ButtonList.Add(new sys_NavigationUrl("返回", "Default.aspx", "", UrlType.Href, true));
                }
                else
                {
                    MBx.M_Body = "强制下线失败.用户名/密码无效！";
                    MBx.M_ButtonList.Add(new sys_NavigationUrl("返回", "login.aspx", "", UrlType.Href, true));
                }

            }
            Session["CheckCode"] = Common.RndNum(4);
            EventMessage.MessageBox(MBx);
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        MessageBox mb = new MessageBox();
        mb.M_Type = Msg_Type.Error;
        mb.M_Title = "登陆出错！";
        mb.M_IconType = Icon_Type.Error;
        mb.M_Body = "验证码无效，请确认您输入的验证码有效！";


        string userKey = TextBox1.Text.Trim() + Common.GetIPAddress().Replace(".", "");
        string userName = TextBox1.Text.Trim();
        string password = TextBox2.Text.Trim();
        try
        {
            if (userName == string.Empty)
            {
                mb.M_Body = "用户名不能为空";
                mb.M_ButtonList.Add(new sys_NavigationUrl("返回", "Login.aspx", "返回登陆页面", UrlType.Href, true));
            }
            else if (password == string.Empty)
            {
                mb.M_Body = "密码不能为空";
                mb.M_ButtonList.Add(new sys_NavigationUrl("返回", "Login.aspx", "返回登陆页面", UrlType.Href, true));
            }

            //else if (TextBox3.Text.Trim() == string.Empty)
            //{
            //    mb.M_Body = "验证码不能为空";
            //    mb.M_ButtonList.Add(new sys_NavigationUrl("返回", "Login.aspx", "返回登陆页面", UrlType.Href, true));
            //}

            //else if (TextBox3.Text.Trim() != (string)Session["CheckCode"])
            //{
            //    mb.M_ButtonList.Add(new sys_NavigationUrl("返回", "Login.aspx", "点击按钮重新输入验证码！", UrlType.Href, true));
            //}
            //else if (!SystemLogin.CheckDisableLoginUser(userKey))
            //{
            //    mb.M_Body = string.Format("此用户:{0},IP:{1}登陆出错次数超过系统允许,已经禁止登陆.请联系管理员！", userName, Common.GetIPAddress());
            //    mb.M_ButtonList.Add(new sys_NavigationUrl("返回", "Login.aspx", "点击按钮返回！", UrlType.Href, true));
            //}
            else if (new SystemLogin().CheckLogin(userName, Common.md5(password, 32)))
            {
                //成功登陆后，需要清除出错次数检测列表中的信息
                SystemLogin.RemoveErrorLoginUser(userKey);

                //mb.M_IconType = Icon_Type.OK;
                //mb.M_Title = "登陆成功！";
                //mb.M_Body = string.Format("欢迎您{0}，成功登入。您的IP为：{1}！", userName, Common.GetIPAddress());
                //mb.M_Type = Msg_Type.Info;
                //mb.M_ButtonList.Add(new sys_NavigationUrl("确定", "Default.aspx", "点击按钮登陆！", UrlType.Href, true));
                EventMessage.EventWriteLog(Msg_Type.Info, string.Format("欢迎您{0}，成功登入。您的IP为：{1}！", userName, Common.GetIPAddress()));
                Response.Redirect("Default.aspx");
            }
            else
            {
                mb.M_Body = string.Format("用户名/密码({0}/{1})错误！", userName, password);
                mb.M_ButtonList.Add(new sys_NavigationUrl("返回", "login.aspx", "点击按钮重新输入！", UrlType.Href, true));

            }

            Session["CheckCode"] = Common.RndNum(4);
        }
        catch (Exception ex)
        {
            mb.M_Body = "登录失败：" + ex.Message;
            mb.M_WriteToLog = true;
            mb.M_ButtonList.Add(new sys_NavigationUrl("返回", "Login.aspx", "返回登陆页面", UrlType.Href, true));
        }
        EventMessage.MessageBox(mb);
    }
}
