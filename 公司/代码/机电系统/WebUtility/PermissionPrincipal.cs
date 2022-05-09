using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Security.Principal;
using WebUtility.Components;

namespace WebUtility
{
    /// <summary>
    /// 方法属性权限检测类
    /// </summary>
    public class PermissionPrincipal : IPrincipal
    {
        private IIdentity _identity;
        private string[] _roles;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_IID"></param>
        public PermissionPrincipal(IIdentity _IID)
        {
            _identity = _IID;
            _roles = new string[1] { "check" };
        }

        /// <summary>
        /// 检测角色资料
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            return Check_PopedomTypeAttaible();
        }
        /// <summary>
        /// 用户标识
        /// </summary>
        public IIdentity Identity
        {
            get
            {
                return _identity;
            }
        }


        private bool Check_PopedomTypeAttaible()
        {
            StackTrace stack = new StackTrace();

            foreach (StackFrame sframe in stack.GetFrames())
            {
                foreach (PopedomTypeAttaible var in sframe.GetMethod().GetCustomAttributes(typeof(PopedomTypeAttaible), true))
                {
                    SystemPermission.CheckPermissionWithHint(var.PType);
                }
            }
            return true;
        }
    }

    /// <summary>
    /// 权限方法属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class PopedomTypeAttaible : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="PT"></param>
        public PopedomTypeAttaible(PopedomType PT)
        {
            _PType = PT;
        }

        /// <summary>
        /// 权限类型
        /// </summary>
        private PopedomType _PType;
        /// <summary>
        /// 权限类型
        /// </summary>
        public PopedomType PType
        {
            get
            {
                return _PType;
            }
        }

    }
}
