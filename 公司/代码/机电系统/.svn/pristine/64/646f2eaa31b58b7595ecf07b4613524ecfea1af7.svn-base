using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.Model.System
{
    /// <summary>
    /// 性别类型
    /// </summary>
    public enum Sex
    {
        [EnumDescription("未知性别")]
        Unknown=0,
        [EnumDescription("男")]
        Male=1,
        [EnumDescription("女")]
        Female=2
    }
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        [EnumDescription("未知用户类型")]
        Unknown = 0,
        [EnumDescription("超级用户")]
        Administrator=1,
        [EnumDescription("普通用户")]
        CommonUser=2
    }
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatus
    {
        [EnumDescription("未知用户状态")]
        Unknown = 0,
        [EnumDescription("正常")]
        Normal=1,
        [EnumDescription("停用")]
        NonUse=2
    }
    /// <summary>
    /// 用户实体类
    /// </summary>
    [Serializable]
    public class UserInfo
    {
        #region Model
        private string _username;
        private long _departmentid;
        private long _positionid;
        private Sex _sex;
        private DateTime _birthday;
        private string _photourl;
        private string _officephone;
        private string _mobilephone;
        private string _homephone;
        private string _fax;
        private string _address;
        private string _password;
        private string _email;
        private string _im;
        private string _responsibility;
        private DateTime _updatetime;
        private string _positionname;
        private string _companyname;
        private string _departmentname;
        private UserType _usertype;
        private UserStatus _status;
        private DateTime _lastlogintime;
        private string _staffno;
        private string _companyid;
        private string _personname;
        private string _idcard;
        private bool _isparentcompany;
        private IList _roles;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 部门编号
        /// </summary>
        public long DepartmentID
        {
            set { _departmentid = value; }
            get { return _departmentid; }
        }
        /// <summary>
        /// 职位编号
        /// </summary>
        public long PositionID
        {
            set { _positionid = value; }
            get { return _positionid; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public Sex Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        /// <summary>
        /// 照片URL
        /// </summary>
        public string PhotoUrl
        {
            set { _photourl = value; }
            get { return _photourl; }
        }
        /// <summary>
        /// 办公室电话
        /// </summary>
        public string OfficePhone
        {
            set { _officephone = value; }
            get { return _officephone; }
        }
        /// <summary>
        /// 移动电话
        /// </summary>
        public string MobilePhone
        {
            set { _mobilephone = value; }
            get { return _mobilephone; }
        }
        /// <summary>
        /// 家庭电话
        /// </summary>
        public string HomePhone
        {
            set { _homephone = value; }
            get { return _homephone; }
        }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax
        {
            set { _fax = value; }
            get { return _fax; }
        }
        /// <summary>
        /// 家庭住址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 即时通信号码
        /// </summary>
        public string IM
        {
            set { _im = value; }
            get { return _im; }
        }
        /// <summary>
        /// 主要职责描述
        /// </summary>
        public string Responsibility
        {
            set { _responsibility = value; }
            get { return _responsibility; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName
        {
            set { _positionname = value; }
            get { return _positionname; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName
        {
            set { _departmentname = value; }
            get { return _departmentname; }
        }
        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UserType
        {
            set { _usertype = value; }
            get { return _usertype; }
        }
        /// <summary>
        /// 用户状态
        /// </summary>
        public UserStatus Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 上一次登陆时间
        /// </summary>
        public DateTime LastLoginTime
        {
            set { _lastlogintime = value; }
            get { return _lastlogintime; }
        }
        /// <summary>
        /// 员工工号
        /// </summary>
        public string StaffNO
        {
            set { _staffno = value; }
            get { return _staffno; }
        }
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string PersonName
        {
            set { _personname = value; }
            get { return _personname; }
        }
        /// <summary>
        ///身份证号码 
        /// </summary>
        public string IDCard
        {
            set { _idcard = value; }
            get { return _idcard; }
        }
        /// <summary>
        /// 是否总公司
        /// </summary>
        public  bool IsParentCompany
        {
            get { return _isparentcompany; }
            set { _isparentcompany = value; }
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
        /// 是否超级用户
        /// </summary>
        public bool IsAdministrator
        {
            get { return _usertype == UserType.Administrator ? true : false; }
        }
        #endregion Model

    }
}
