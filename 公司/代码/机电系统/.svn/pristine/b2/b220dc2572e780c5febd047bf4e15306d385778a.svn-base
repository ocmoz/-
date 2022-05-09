using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using FM2E.Model.System;
using FM2E.Model.Utils;

namespace FM2E.IDAL.System
{
    public interface IUser
    {
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        IList GetAllUser();
        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        UserInfo GetUser(string userName);
        /// <summary>
        /// 获取属于某种角色的所有用户
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        IList GetUsers(long roleID);
        /// <summary>
        /// 获取属于某一间公司的所有用户
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        IList GetUsersByCompanyID(string companyID);
        /// <summary>
        /// 更新用户名与密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        void UpdatePassword(string userName, string password);
        /// <summary>
        /// 校验密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>正确-true，否则-false</returns>
        bool ValidatePassword(string userName, string password);
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        void AddUser(UserInfo user);
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        void UpdateUser(UserInfo user);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userName"></param>
        void DeleteUser(string userName);
        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam GenerateSearchTerm(UserSearchInfo item);
        /// <summary>
        /// 根据查询条件获取用户列表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList GetList(QueryParam term,out int recordCount);
        /// <summary>
        /// 用户登录校验
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>如果用户与密码均正确，则返回用户记录，否则返回null</returns>
        LoginUserInfo UserLogin(string userName, string password);
        /// <summary>
        /// 获取用户的登陆信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        LoginUserInfo GetLoginUser(string userName);

         /// <summary>
        /// 获取工作流状态下指定部门的用户列表
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="workflowStateName"></param>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        IList GetNextWorkflowUser(String workflowName, String workflowStateName, long departmentID);


        //**********Modified by Xue    For V 3.1.2     2011-10-20****************************************************************************************************
        /// <summary>
        /// 通过部门ID获取用户
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        IList GetUsersByDepartmentId(long deptId);
        //**********Modification Finished 2011-6-27**********************************************************************************************
        

        /// <summary>
        /// 获取所属工程师的所有用户
        /// </summary>
        /// <param name="im"></param>
        /// <returns></returns>
        IList GetUsersByIM(string im);

    }
}
