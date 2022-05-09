using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using WebUtility.Components;
using System.Configuration;

namespace WebUtility
{
    public class SystemPermission : IHttpModule
    {
        /// <summary>
        /// 在线用户缓存
        /// </summary>
        public static CacheOnline<string, OnlineUser<string>> UserOnlineList = null;

        /// <summary>
        /// 应用启动时间
        /// </summary>
        public static DateTime AppStartTime;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="app"></param>
        public void Init(HttpApplication app)
        {
            app.Error += new EventHandler(AppError);
            app.AuthenticateRequest += new EventHandler(AppAuthMethod);
            app.AcquireRequestState += new EventHandler(AppAuth);
            AppStartTime = DateTime.Now;
        }

        /// <summary>
        /// 设置方法属性权限检测数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppAuthMethod(object sender, EventArgs e)
        {
            //检测方法权限设置
            HttpApplication App = (HttpApplication)sender;
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = App.Context.Request.Cookies[cookieName];

            if (null == authCookie)
            {
                return;
            }

            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch (Exception ex)
            {
                FileLogWriter.Instance.WriteError(ex.ToString());
                return;
            }

            if (null == authTicket)
            {
                return;
            }

            // 建立 Identity 物件
            FormsIdentity id = new FormsIdentity(authTicket);

            App.Context.User = new PermissionPrincipal(id);
        }

        /// <summary>
        /// 处理认证成功事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppAuth(object sender, EventArgs e)
        {
            try
            {
                //初始化在线用户列表
                if (UserOnlineList == null)
                    UserOnlineList = new CacheOnline<string, OnlineUser<string>>(SystemConfig.Instance.OnlineTime);

                //判断
                if (Common.GetScriptNameExt.ToLower() == "aspx" && Common.Get_UserName != null)
                {
                    //判断在线用户

                    if (UserOnlineList.CheckKeyOnline(Common.GetSessionID))
                    {
                        UserOnlineList.Access(Common.GetSessionID, Common.GetScriptUrl);
                    }
                    else
                    {
                        if (SystemConfig.Instance.OnlineTime != 0)
                        {
                            SystemLogin.UserOut();
                            MessageBox MBx = new MessageBox();
                            MBx.M_Type = Msg_Type.Error;
                            MBx.M_Title = "没有登陆!";
                            MBx.M_IconType = Icon_Type.Error;
                            MBx.M_Body = "您已经被系统强制退出！";
                            MBx.M_ButtonList.Add(new sys_NavigationUrl("返回", "window.top.location.href='Login.aspx'", "", UrlType.JavaScript, true));
                            EventMessage.MessageBox(MBx);
                        }
                    }

                    //检测权限
                    if (!Check_Permission)
                    {
                        MessageBox MBx = new MessageBox();
                        MBx.M_Type = Msg_Type.Error;
                        MBx.M_Title = "权限出错";
                        MBx.M_IconType = Icon_Type.Error;
                        MBx.M_Body = "无权访问当前页面！";
                        MBx.M_ButtonList.Add(new sys_NavigationUrl("返回", "history.back();", "", UrlType.JavaScript, true));
                        EventMessage.MessageBox(MBx);
                    }
                }
            }
            catch (Exception ex)
            {
                EventMessage.EventWriteLog(Msg_Type.Error, "执行SystemPermission的事件时出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 处理出错日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppError(object sender, EventArgs e)
        {

            HttpApplication ap = sender as HttpApplication;
            Exception ex = ap.Server.GetLastError();
            if (ex is HttpException)
            {
                HttpException hx = (HttpException)ex;
                if (hx.GetHttpCode() == 404)
                {
                    string page = ap.Request.PhysicalPath;
                    FileLogWriter.Instance.WriteError(string.Format("文件不存在:{0}", ap.Request.Url.AbsoluteUri));
                    return;
                }
            }
            if (ex != null)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                FileLogWriter.Instance.WriteError(ex.Source + " thrown " + ex.GetType().ToString() + "\r\n" + ex.Message + ex.StackTrace);
                if (SystemConfig.Instance.DisplayError)
                {
                    if (Common.GetScriptName.ToLower().IndexOf("Messages.aspx") < 0)
                    {
                        ap.Response.Redirect("~/Messages.aspx?CMD=AppError");
                    }
                }
            }
        }

        /// <summary>
        /// 检测是否有权限访问当前页面,true代表有权限访问,false代表没有权限访问
        /// </summary>
        private bool Check_Permission
        {
            get
            {
                Permission Pis = Get_Permission;   //如果没有配置文件，则默认为允许访问
                if (Pis == null)
                    return true;

                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    string userName = Common.Get_UserName;
                    if (UserData.GetLoginUserInfo(userName).IsAdministrator) //如为超级用户
                        return true;

                    //获取当前页面的权限配置项,如果配置文件存在，但不存在当前页面的配置项，则不允许访问
                    int permissionValue = Get_PermissionValue(Pis.ItemList,Get_Script_Name);

                    if (permissionValue == 0)
                        return true;

                    return UserData.CheckModuleID(userName, Pis.ModuleID, permissionValue);
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 获取当前访问网页文件名格式: ,文件名,
        /// </summary>
        public static string Get_Script_Name
        {
            get
            {
                string Script_Name = Common.GetScriptName;
                Script_Name = Script_Name.Substring(Script_Name.LastIndexOf("/") + 1);
                return string.Format(",{0},", Script_Name);
            }
        }

        /// <summary>
        /// 获取当前目录下权限配置集合
        /// </summary>
        public static Permission Get_Permission
        {
            get
            {
                return (Permission)ConfigurationManager.GetSection("Permission");
            }
        }

        /// <summary>
        /// 获取当前面页所属的权限类型
        /// </summary>
        /// <param name="List">权限列表</param>
        /// <returns>权限值</returns>
        public static int Get_PermissionValue(List<PermissionItem> List,string currentPage)
        {
            int permissionValue = 0;
            foreach (PermissionItem var in List)
            {
                if (var.Item_FileList.IndexOf(currentPage.ToLower()) >= 0)
                {
                    permissionValue += var.Item_Value;
                }
            }
            return permissionValue;
        }

        /// <summary>
        /// 检测权限
        /// </summary>
        /// <param name="PT"></param>
        /// <returns>true-能访问，false-不能访问</returns>
        public static bool CheckButtonPermission(PopedomType pt)
        {
            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            //要通过权限约束，必须在模块进行页面配置，以生成web.config文件。
            bool pageCheck = false;
            bool permCheck = true;
            string userName = Common.Get_UserName;
            if (UserData.GetLoginUserInfo(userName).IsAdministrator) //如为超级用户
                pageCheck = true;
            Permission Pis = Get_Permission;
            if (Pis == null)
                pageCheck = true;
            else
            {
                int permissionValue = Get_PermissionValue(Pis.ItemList, Get_Script_Name);
                if (permissionValue == 0)
                    pageCheck = true;
                if ((permissionValue & (int)pt) != 0)
                    pageCheck = true;
                permCheck = UserData.CheckModuleID(Common.Get_UserName, Pis.ModuleID, (int)pt);
            }
            return pageCheck & permCheck;
            //********** Modification Finished 2011-09-09 **********************************************************************************************
        }
        /// <summary>
        /// 检测权限
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>true-有权限，false-无权限</returns>
        public static bool CheckPermission(PopedomType pt)
        {
            return CheckButtonPermission(pt);
        }
        /// <summary>
        /// 检测权限(出提示框)
        /// </summary>
        /// <param name="PT"></param>
        public static void CheckPermissionWithHint(PopedomType pt)
        {
            if (!CheckButtonPermission(pt))
            {
                EventMessage.MessageBox(Msg_Type.Error, "禁止访问", "无权限访问当前操作!", Icon_Type.Error, "history.back();", UrlType.JavaScript);
            }
        }

        /// <summary>
        /// 根据cmd检测权限
        /// </summary>
        /// <param name="CMD">CMD值</param>
        public static void CheckCommandPermission(string cmd)
        {
            switch (cmd.ToLower())
            {
                case "view":
                case "list":
                    CheckPermissionWithHint(PopedomType.List);
                    break;
                case "add":
                case "new":
                    CheckPermissionWithHint(PopedomType.New);
                    break;
                case "edit":
                    CheckPermissionWithHint(PopedomType.Edit);
                    break;
                case "print":
                    CheckPermissionWithHint(PopedomType.Print);
                    break;
                case "approval":
                    CheckPermissionWithHint(PopedomType.Approval);
                    break;
                case "del":
                case "delete":
                    CheckPermissionWithHint(PopedomType.Delete);
                    break;
                case "permissiona":
                    CheckPermissionWithHint(PopedomType.PermissionA);
                    break;
                case "permissionb":
                    CheckPermissionWithHint(PopedomType.PermissionB);
                    break;
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {

        }
    }
}
