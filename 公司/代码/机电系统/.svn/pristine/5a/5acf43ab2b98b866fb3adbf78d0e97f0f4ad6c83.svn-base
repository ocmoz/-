using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FM2E.Model.Utils;
namespace FM2E.Model.System
{
    public class LoginUserInfo
    {
        private string _username;
        private string _companyid;
        private string _companyName;
        private long _departmentid;
        private string _departmentName;
        private string _personName;
        private UserType _usertype;
        private UserStatus _status;
        private bool _isparentcompany;
        private long _positionID;
        private string _positionName;
        private IList _roles;
        private string _im;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyID
        {
            get { return _companyid; }
            set { _companyid = value; }
        }
        /// <summary>
        /// 公司名
        /// </summary>
        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }
        /// <summary>
        /// 部门ID
        /// </summary>
        public long DepartmentID
        {
            get { return _departmentid; }
            set { _departmentid = value; }
        }
        /// <summary>
        /// 部门名
        /// </summary>
        public string DepartmentName
        {
            get { return _departmentName; }
            set { _departmentName = value; }
        }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string PersonName
        {
            get { return _personName; }
            set { _personName = value; }
        }
        /// <summary>
        /// 用户类型 
        /// </summary>
        public UserType UserType
        {
            get { return _usertype; }
            set { _usertype = value; }
        }
        /// <summary>
        /// 用户状态
        /// </summary>
        public UserStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// 用户角色列表
        /// </summary>
        public IList Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }
        /// <summary>
        /// 是否总公司
        /// </summary>
        public bool IsParentCompany
        {
            get { return _isparentcompany; }
            set { _isparentcompany = value; }
        }
        /// <summary>
        /// 职位ID
        /// </summary>
        public long PositionID
        {
            get { return _positionID; }
            set { _positionID = value; }
        }
        /// <summary>
        /// 职位
        /// </summary>
        public string PositionName
        {
            get { return _positionName; }
            set { _positionName = value; }
        }
        /// <summary>
        /// IM(深高速改为系统ID)
        /// </summary>
        public string IM
        {
            get { return _im; }
            set { _im = value; }
        }
        /// <summary>
        /// 是否超级用户
        /// </summary>
        public bool IsAdministrator
        {
            get { return _usertype == UserType.Administrator ? true : false; }
        }
    }
}
