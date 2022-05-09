using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Maintain
{
    /// <summary>
    /// 维修人员列表
    /// </summary>
    [Serializable]
    public class MalfunctionMaintainStaffInfo
    {
        #region Model
        private long _id;
        private long _maintainid;
        private string _maintenancestaff;
        /// <summary>
        /// 维修人员列表ID
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 故障维修明细记录表ID
        /// </summary>
        public long MaintainID
        {
            set { _maintainid = value; }
            get { return _maintainid; }
        }
        /// <summary>
        /// 维修人员名称
        /// </summary>
        public string MaintenanceStaff
        {
            set { _maintenancestaff = value; }
            get { return _maintenancestaff; }
        }

        #endregion Model
    }
}
