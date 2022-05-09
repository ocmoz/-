using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.SpecialProject
{
    /// <summary>
    /// 专项工程工程量清单实体类
    /// </summary>
    public class SpecialProjectJobItemInfo
    {
        public SpecialProjectJobItemInfo()
        {
        }

        #region Model
        private long _projectid;
        private long _itemid;
        private string _equipment;
        private string _model;
        private decimal _count;
        private string _unit;
        private decimal _unitprice;
        private string _remark;
        /// <summary>
        /// 专项工程ID
        /// </summary>
        public long ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 清单序号
        /// </summary>
        public long ItemID
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// 设备
        /// </summary>
        public string Equipment
        {
            set { _equipment = value; }
            get { return _equipment; }
        }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string Model
        {
            set { _model = value; }
            get { return _model; }
        }
        /// <summary>
        /// 设备数量
        /// </summary>
        public decimal Count
        {
            set { _count = value; }
            get { return _count; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit
        {
            set { _unit = value; }
            get { return _unit; }
        }
        /// <summary>
        /// 设备单价
        /// </summary>
        public decimal UnitPrice
        {
            set { _unitprice = value; }
            get { return _unitprice; }
        }

        /// <summary>
        /// 小计金额
        /// </summary>
        public decimal Amount
        {
            get
            {
                return _unitprice * _count;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model
    }
}
