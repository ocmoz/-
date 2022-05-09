using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Basic
{
    public class EquipmentSystemInfo
    {
        #region Model
        private string _systemid;
        private string _systemname;
        private string _remark;
        private bool _isdeleted;
        private IList _subsystem;
        /// <summary>
        /// 
        /// </summary>
        public string SystemID
        {
            set { _systemid = value; }
            get { return _systemid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SystemName
        {
            set { _systemname = value; }
            get { return _systemname; }
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
        public IList SubSystem
        {
            set { _subsystem = value; }
            get { return _subsystem; }
        }
        #endregion Model
    }
}
