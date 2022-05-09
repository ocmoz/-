using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections;
using System.Web.Caching;

using FM2E.BLL.System;
using FM2E.Model.System;

namespace WebUtility
{
    /// <summary>
    /// 用户数据存储类
    /// </summary>
    public class UserData
    {
        #region "用户资料"
        /// <summary>
        /// 用户资料Cache
        /// </summary>
        private static Cache _UserCache = HttpRuntime.Cache;
        /// <summary>
        /// 根据用户Name获取用户资料
        /// </summary>
        /// <param name="userName">用户Name</param>
        /// <returns></returns>
        internal static LoginUserInfo GetLoginUserInfo(string userName)
        {
            string CacheKey = string.Format("{0}-UserInfo-{1}", Common.Get_WebCacheName, userName);
            if (_UserCache[CacheKey] != null)
            {
                return (LoginUserInfo)_UserCache[CacheKey];
            }
            else
            {
                LoginUserInfo userInfo = new User().GetLoginUser(userName);
                _UserCache.Insert(CacheKey, userInfo);
                return userInfo;
            }
        }

        /// <summary>
        /// 根据用户ID移除用户资料Cache
        /// </summary>
        /// <param name="userName"></param>
        public static void RemoveUserCache(string userName)
        {
            _UserCache.Remove(string.Format("{0}-UserInfo-{1}", Common.Get_WebCacheName, userName));
            RemoveUserPermissionCache(userName);
        }

        #endregion

        #region "用户权限"
        /// <summary>
        /// 用户权限Cache
        /// </summary>
        private static Cache _UserPermissionCache = HttpRuntime.Cache;

        /// <summary>
        /// 根据用户UserName,ModuleID判断用户是否拥有当前权限
        /// </summary>
        /// <param name="userName">用户Name</param>
        /// <param name="moduleID">模块ID号</param>
        /// <returns></returns>
        public static bool CheckModuleID(string userName, string moduleID)
        {
            if (GetLoginUserInfo(userName).IsAdministrator) //判断用户是否为超级用户，超级用户拥有所有权限
                return true;

            bool bBool = false;
            Hashtable UserPermission = Get_UserPermission(userName);
            if (UserPermission.Count > 0)
            {
                string Key = moduleID;
                if (UserPermission.ContainsKey(Key))
                {
                    bBool = true;
                }
            }
            return bBool;
        }

        /// <summary>
        /// 根据用户Name,ModuleID,要检测权限数值
        /// </summary>
        /// <param name="userName">用户Name</param>
        /// <param name="moduleID">模块编号</param>
        /// <param name="permissionValue">权限值</param>
        /// <returns></returns>
        public static bool CheckModuleID(string userName, string moduleID, int permissionValue)
        {
            if (GetLoginUserInfo(userName).IsAdministrator) //判断用户是否为超级用户
                return true;

            bool bBool = false;
            Hashtable UserPermission = Get_UserPermission(userName);
            if (UserPermission.Count > 0)
            {
                string Key = moduleID;
                if (UserPermission.ContainsKey(Key))
                {
                    if ((((RolePermissionInfo)UserPermission[Key]).Permission & permissionValue) != 0)
                    {
                        //有权限访问此页面
                        bBool = true;
                    }
                }
            }
            return bBool;
        }

        /// <summary>
        /// 获取用户权限Hashtable
        /// </summary>
        /// <param name="userName">用户Name</param>
        /// <returns></returns>
        private static Hashtable Get_UserPermission(string userName)
        {
            string Key = string.Format("{1}-Permission-{0}", userName, Common.Get_WebCacheName);
            if (_UserPermissionCache[Key] != null)
            {
                return (Hashtable)_UserPermissionCache[Key];
            }
            else
            {
                Hashtable _Temp = GetRolePermissionTable(userName);
                _UserPermissionCache.Insert(Key, _Temp);
                return _Temp;
            }
        }

        /// <summary>
        /// 移除用户权限Cache
        /// </summary>
        /// <param name="userName">用户Name</param>
        public static void RemoveUserPermissionCache(string userName)
        {
            _UserPermissionCache.Remove(string.Format("{1}-Permission-{0}", userName, Common.Get_WebCacheName));
        }

        /// <summary>
        /// 移除某个角色的用户权限Cache
        /// </summary>
        /// <param name="RoleID"></param>
        public static void RemoveRoleUserPermissionCache(long RoleID)
        {
            IList list = new User().GetUsers(RoleID);
            foreach (UserRoleInfo var in list)
            {
                RemoveUserPermissionCache(var.UserName);
            }
        }

        /// <summary>
        /// 根据用户Name,获取用户模块权限列表
        /// </summary>
        /// <param name="userName">用户Name</param>
        /// <returns></returns>
        private static Hashtable GetRolePermissionTable(string userName)
        {
            Hashtable moduleList = new Hashtable();
            IList list = new RolePermission().GetRolePermissions(userName);

            foreach (RolePermissionInfo item in list)
            {
                string Key = item.ModuleID;
                if (moduleList.ContainsKey(Key))
                {
                    RolePermissionInfo permissionInfo = (RolePermissionInfo)moduleList[Key];
                    //if (permissionInfo.Permission < item.Permission)
                    //{
                    //    moduleList[Key] = item;
                    //}
                    permissionInfo.Permission |= item.Permission;
                }
                else
                {
                    moduleList.Add(Key, item);
                }
            }
            return moduleList;
        }
        #endregion

        #region "获取当前登陆用户信息"
        /// <summary>
        /// 获取当前登陆用户信息
        /// </summary>
        public static LoginUserInfo CurrentUserData
        {
            get
            {
                return GetLoginUserInfo(Common.Get_UserName);
            }
        }
        #endregion
    }
}
