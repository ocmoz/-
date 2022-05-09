using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;

namespace FM2E.Model.Basic
{
    public enum DepartmentType
    {
        [EnumDescription("普通部门")]
        Unknown=0,
        
        //********** Modified by Xue    For ShenGaoSu    2011-11-28 **********************************************************************************
        [EnumDescription("维修部门(自维)")]
        MaintainTeam=1,

        [EnumDescription("维修部门(外维)")]
        MaintainTeamOther=2
        
        //********** Modification Finished 2011-11-28 **********************************************************************************************

    }
    /// <summary>
    /// 部门实体类
    /// </summary>
    public class DepartmentInfo
    {
        #region Model
        private long _departmentid;
        private string _parentname;
        private long _level;
        private long _childrencount;
        private string _companyid;
        private string _companyname;
        private string _name;
        private string _remark;
        private string _phone;
        private string _leaderid;
        private string _staffname;
        private long _parentid;
        private DepartmentType _departmentType;
        /// <summary>
        /// 
        /// </summary>
        public long DepartmentID
        {
            set { _departmentid = value; }
            get { return _departmentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ParentName
        {
            set { _parentname = value; }
            get { return _parentname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long Level
        {
            set { _level = value; }
            get { return _level; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ChildrenCount
        {
            set { _childrencount = value; }
            get { return _childrencount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LeaderID
        {
            set { _leaderid = value; }
            get { return _leaderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StaffName
        {
            set { _staffname = value; }
            get { return _staffname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 部门类型
        /// </summary>
        public DepartmentType DepartmentType
        {
            set { _departmentType = value; }
            get { return _departmentType; }
        }
        #endregion Model
    }
}
