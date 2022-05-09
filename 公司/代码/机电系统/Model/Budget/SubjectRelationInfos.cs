using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Budget
{
    public class SubjectRelationInfos
    {
        #region Model
        private long _subid;
        private long _parentid;
        private string _name;
        private short _isleaf;
        private string _parentname;
        private string _companyid;
        

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 上级科目名称
        /// </summary>
        public string ParentName
        {
            set { _parentname = value; }
            get { return _parentname; }
        }
        /// <summary>
        /// 科目编号
        /// </summary>
        public long SubID
        {
            set { _subid = value; }
            get { return _subid; }
        }
        /// <summary>
        /// 父节点编号
        /// </summary>
        public long ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 科目名字
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 是否叶子科目
        /// </summary>
        public short IsLeaf
        {
            set { _isleaf = value; }
            get { return _isleaf; }
        }
        #endregion Model
    }
}
