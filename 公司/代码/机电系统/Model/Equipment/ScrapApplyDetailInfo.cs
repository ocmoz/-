using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    public class ScrapApplyDetailInfo
    {
    /// <summary>
    /// 设备借调申请明细实体类
    /// </summary>
        private long _scrapid;
        private string _equipmentno;
        private string _equipmentname;
        private string _scrapreason;
        /// <summary>
        /// 
        /// </summary>
        public long ScrapID
        {
            set { _scrapid = value; }
            get { return _scrapid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EquipmentNo
        {
            set { _equipmentno = value; }
            get { return _equipmentno; }
        }
        public string EquipmentName
        {
            set { _equipmentname = value; }
            get { return _equipmentname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ScrapReason
        {
            set { _scrapreason = value; }
            get { return _scrapreason; }
        }
    }
}
