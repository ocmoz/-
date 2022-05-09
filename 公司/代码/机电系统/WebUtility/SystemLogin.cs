using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

using FM2E.BLL.System;
using FM2E.Model.System;
using WebUtility.Components;
using System.Web;
using System.Configuration;

namespace WebUtility
{
    /// <summary>
    /// 用户登陆类
    /// </summary>
    public class SystemLogin
    {
        /// <summary>
        /// 校验系统是否符合使用要求，即正式版、试用版、试用到期等
        /// Version使用了DES加密
        /// </summary>
        /// <returns></returns>
        public static bool CheckSysActivation()
        {
            //使用简单的移位加密，5位
            string versionInfo = ConfigurationManager.AppSettings["Version"];
            if (versionInfo.Length <= 2)
                return false;

            string content = versionInfo.Substring(1, versionInfo.Length-2);
            char[] array = content.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (char)(array[i] + 5);
            }

            string str = new string(array);
            EventMessage.EventWriteLog(Msg_Type.Fatal, "Version:" + str);
            if (str == "true")
                return true;
            else
            {
                try
                {
                    //如果时间过期
                    if (DateTime.Parse(str).CompareTo(DateTime.Now) <= 0)
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///登陆验证
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public bool CheckLogin(string userName, string password)
        {
            bool bSuccess = false;

            User bll = new User();
            LoginUserInfo userInfo = bll.UserLogin(userName, password);
            if (userInfo != null)
            {
                bSuccess = true;
                CheckOnline(userName,userInfo.PersonName, password, Common.GetSessionID);
                SignIn(userInfo);
            }
            return bSuccess && CheckSysActivation();
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        public static void UserOut()
        {
            RemoveOnlineUser(Common.GetSessionID);
            //删除一些用户Cache
            UserData.RemoveUserPermissionCache(Common.Get_UserName);
            UserData.RemoveUserCache(Common.Get_UserName);

            System.Web.Security.FormsAuthentication.SignOut();
        }
        /// <summary>
        /// 检测在线用户表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="sessionID">用户SessionID</param>
        private void CheckOnline(string userName,string personName, string password, string sessionID)
        {
            if (isUserOnline(userName))
            {
                //用户已在线
                HttpContext.Current.Session["CheckCode"] = Common.RndNum(4);
                OnlineUser<string> ou = GetOnlineMember(userName);
                MessageBox mb = new MessageBox();
                mb.M_Title = "重复登陆";
                mb.M_IconType = Icon_Type.Alert;

                if (Common.GetSessionID != ou.U_Guid)
                {

                    mb.M_Body = string.Format("您的用户名({0})已经于({1}),从({2})IP登陆在本系统.在线时间:{3}分.", userName, ou.U_StartTime, ou.U_LastIP, (ou.U_OnlineSeconds / 60).ToString("N2"));
                    mb.M_ButtonList.Add(new sys_NavigationUrl("返回", "Login.aspx", "", UrlType.Href, true));
                    mb.M_ButtonList.Add(new sys_NavigationUrl("强制下线", string.Format("Login.aspx?CMD=OutOnline&OPCode={0}&U_LoginName={1}&U_Password={2}&SessionID={3}", HttpContext.Current.Session["CheckCode"].ToString(), userName, password, ou.U_Guid), "", UrlType.Href, false));
                    EventMessage.MessageBox(mb);
                }
            }
            else
            {
                RemoveOnlineUser(sessionID);    //同一机器同一时间只能有一个用户登陆
                AddOnlineUser(userName,personName, sessionID);
            }
        }

        #region "用户登陆出错次数检测"
        private static CacheOnline<string, LoginErrorUser> loginErrorList = new CacheOnline<string, LoginErrorUser>(SystemConfig.Instance.LoginErrorDisableMinute);

        /// <summary>
        /// 检测用户是否可以登陆
        /// </summary>
        /// <param name="userKey">用户UserKey</param>
        /// <returns>true可以登陆,false不可登陆</returns>
        public static bool CheckDisableLoginUser(string userKey)
        {
            bool rBool = false;
            LoginErrorUser LUser = loginErrorList.GetValue(userKey);
            if (LUser == null)
            {
                //如果Cache中没有此用户信息则添加
                LUser = new LoginErrorUser();
                LUser.U_Guid = userKey;
                LUser.U_Type = true;
                LUser.ErrorCount = 1;
                LUser.U_Name = userKey;
                loginErrorList.InsertUser(userKey, LUser);
                rBool = true;
            }
            else
            {
                if (LUser.ErrorCount < SystemConfig.Instance.LoginErrorMaxNum)
                {
                    LUser.ErrorCount++;
                    return true;
                }
                else
                {
                    loginErrorList.Access(userKey);
                }
            }
            return rBool;
        }

        /// <summary>
        /// 移除登陆列表中用户
        /// </summary>
        /// <param name="userKey">用户key</param>
        /// <returns>true成功,false失败</returns>
        public static bool RemoveErrorLoginUser(string userKey)
        {
            bool rBool = false;
            loginErrorList.Remove(userKey);
            return rBool;
        }
        #endregion

        public void SignIn(LoginUserInfo userInfo)
        {
            System.Web.Security.FormsAuthenticationTicket tk 
                = new System.Web.Security.FormsAuthenticationTicket(1,
                    userInfo.UserName, 
                    DateTime.Now, 
                    DateTime.Now.AddMonths(1),
                    true, 
                    "",
                    System.Web.Security.FormsAuthentication.FormsCookiePath);

            string key = System.Web.Security.FormsAuthentication.Encrypt(tk);
            HttpCookie ck = new HttpCookie(System.Web.Security.FormsAuthentication.FormsCookieName, key);
            //ck.Domain = System.Web.Security.FormsAuthentication.CookieDomain;  // 这句话在部署网站后有用，此为关系到同一个域名下面的多个站点是否能共享Cookie
            HttpContext.Current.Response.Cookies.Add(ck);

            //HttpContext.Current.Session["UserInfo"] = userInfo;
        }

        /// <summary>
        /// 检测用户名是否在线
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private static bool isUserOnline(string userName)
        {
            return SystemPermission.UserOnlineList.CheckMemberOnline(userName);
        }
        /// <summary>
        /// 获得在线用户列表用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private static OnlineUser<string> GetOnlineMember(string userName)
        {
            return SystemPermission.UserOnlineList.GetValue(userName);
        }
        /// <summary>
        /// 添加在线新用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="sessionID"></param>
        public static void AddOnlineUser(string userName,string personName, string sessionID)
        {
            OnlineUser<string> us = new OnlineUser<string>();
            us.U_Guid = sessionID;
            us.U_Name = userName;
            us.U_Type = true;
            us.U_PersonName = personName;
            us.U_LastUrl = Common.GetScriptUrl;
            SystemPermission.UserOnlineList.InsertUser(us.U_Guid, us);
        }

        /// <summary>
        /// 移除在线用户
        /// </summary>
        /// <param name="sessionID">用户sessionid</param>
        public static void RemoveOnlineUser(string sessionID)
        {
            SystemPermission.UserOnlineList.Remove(sessionID);
        }

    }
}
