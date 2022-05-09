using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 设备借调申请明细实体类
    /// </summary>
    public class BorrowApplyDetailInfo
    {
        private long _borrowapplyid;
        private long _itemid;
        private string _equipmentname;
        private string _model;
        private int _count;
        private string _unit;
        private DateTime _returndate;
        private string _reason;
        /// <summary>
        /// 
        /// </summary>
        public long BorrowApplyID
        {
            set { _borrowapplyid = value; }
            get { return _borrowapplyid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ItemID
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EquipmentName
        {
            set { _equipmentname = value; }
            get { return _equipmentname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Model
        {
            set { _model = value; }
            get { return _model; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            set { _count = value; }
            get { return _count; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Unit
        {
            set { _unit = value; }
            get { return _unit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ReturnDate
        {
            set { _returndate = value; }
            get { return _returndate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Reason
        {
            set { _reason = value; }
            get { return _reason; }
        }

        /// <summary>
        /// 使用地点ID
        /// </summary>
        public long AddressID { get; set; }

        /// <summary>
        /// 使用地点名称
        /// </summary>
        public string AddressName { get; set; }

        /// <summary>
        /// 使用地点编码
        /// </summary>
        public string AddressCode { get; set; }

        /// <summary>
        /// 使用地点详细情况
        /// </summary>
        public string DetailLocation { get; set; }
    }
}
