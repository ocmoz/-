using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Basic
{
    public class SubEquipmentSystemInfo
    {
        #region Model
        private int _subsystemid;
        private string _subsystemname;
        private string _parentsystemid;
        private string _remark;
        private bool _isdeleted;
        /// <summary>
        /// 
        /// </summary>
        public int SubSystemID
        {
            set { _subsystemid = value; }
            get { return _subsystemid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SubSystemName
        {
            set { _subsystemname = value; }
            get { return _subsystemname; }
        }
        public string ParentSystemID
        {
            set { _parentsystemid = value; }
            get { return _parentsystemid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        public bool IsDeleted
        {
            set { _isdeleted = value; }
            get { return _isdeleted; }
        }
        #endregion Model
    }
}
