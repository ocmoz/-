using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using FM2E.IDAL.System;
using FM2E.Model.System;
using FM2E.Model.Utils;

namespace FM2E.BLL.System
{
    public class User
    {
        /// <summary>
        /// 用户登录校验
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>如果用户与密码均正确，则返回用户记录，否则返回null</returns>
        public LoginUserInfo UserLogin(string userName, string password)
        {
            IUser dal = FM2E.DALFactory.SystemAccess.CreateUser();
            return dal.UserLogin(userName, password);
        }
        /// <summary>
        /// 获取用户的登陆信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public LoginUserInfo GetLoginUser(string userName)
        {
            IUser dal = FM2E.DALFactory.SystemAccess.CreateUser();
            return dal.GetLoginUser(userName);
        }
        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public IList GetAllUser()
        {
            IUser dal = FM2E.DALFactory.SystemAccess.CreateUser();
            return dal.GetAllUser();
        }

         /// <summary>
        /// 获取工作流状态下指定部门的用户列表
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="workflowStateName"></param>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public IList GetWorkflowStateUser(String workflowName, String workflowStateName, long departmentID)
        {
            IUser dal = FM2E.DALFactory.SystemAccess.CreateUser();
            return dal.GetNextWorkflowUser(workflowName,workflowStateName,departmentID);
        }
        /// <summary>
        /// 获取属于某一间公司的所有用户
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public IList GetUsersByCompanyID(string companyID)
        {
            IUser dal = FM2E.DALFactory.SystemAccess.CreateUser();
            return dal.GetUsersByCompanyID(companyID);
        }
        /// <summary>
        /// 获取属于某个角色的用户列表
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public  IList GetUsers(long roleID)
        {
            IUser dal = FM2E.DALFactory.SystemAccess.CreateUser();
            return dal.GetUsers(roleID);
        }
        /// <summary>
        /// 获取某个特定用户的信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserInfo GetUser(string userName)
        {
            IUser dal = FM2E.DALFactory.SystemAccess.CreateUser();
            return dal.GetUser(userName);
        }
        /// <summary>
        /// 添加新用户
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(UserInfo user)
        {
            IUser dal = FM2E.DALFactory.SystemAccess.CreateUser();
            dal.AddUser(user);
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser(UserInfo user)
        {
            IUser dal = FM2E.DALFactory.SystemAccess.CreateUser();
            dal.UpdateUser(user);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userName"></param>
        public void DeleteUser(string userName)
        {
            IUser dal = FM2E.DALFactory.SystemAccess.CreateUser();
            dal.DeleteUser(userName);
        }
        /// <summary>
        /// 获取用户列表（支持分页）
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetList(UserSearchInfo term, int currentPageIndex, int pageSize, out int recordCount)
        {
            IUser dal = FM2E.DALFactory.SystemAccess.CreateUser();
            QueryParam p = dal.GenerateSearchTerm(term);
            p.PageIndex = currentPageIndex;
            p.PageSize = pageSize;
            return dal.GetList(p, out recordCount);
        }
        /// <summary>
        /// 校验用户名与密码是否相符
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>如果相符返回true,否则返回false</returns>
        public bool ValidatePassword(string userName, string password)
        {
            IUser dal = FM2E.DALFactory.SystemAccess.CreateUser();
            return dal.ValidatePassword(userName, password);
        }
        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public void UpdatePassword(string userName, string password)
        {
            IUser dal = FM2E.DALFactory.SystemAccess.CreateUser();
            dal.UpdatePassword(userName, password);
        }

        //**********Modified by Xue    For V 3.1.2     2011-10-20****************************************************************************************************
        /// <summary>
        /// 通过部门ID获取用户
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public IList GetUsersByDepartmentId(long deptId)
        {
            IUser dal = FM2E.DALFactory.SystemAccess.CreateUser();
            return dal.GetUsersByDepartmentId(deptId);
        }
        //**********Modification Finished 2011-6-27**********************************************************************************************


        /// <summary>
        /// 获取所属工程师的所有用户
        /// </summary>
        /// <param name="im"></param>
        /// <returns></returns>
        public IList GetUsersByIM(string im)
        {
            IUser dal = FM2E.DALFactory.SystemAccess.CreateUser();
            return dal.GetUsersByIM(im);
        }


    }
}
