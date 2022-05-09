using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    public class PriceApplyDetailInfo
    {
        #region Model
        private long _applyformid;
        private string _reason;
        private int _result;
        private string _feeback;
        private string _companyid;
        private string _productname;
        private string _model;
        private DateTime _starttime;
        private decimal _oldupperprice;
        private decimal _oldlowerprice;
        private decimal _newupperprice;
        private decimal _newlowerprice;
        private short _deleteold;
        private string _status;
        private Guid _instanceId;
        private string _unit;
        /// <summary>
        /// 记录单位
        /// </summary>
        public string Unit
        {
            set { _unit = value; }
            get { return _unit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ApplyFormID
        {
            set { _applyformid = value; }
            get { return _applyformid; }
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
        /// 
        /// </summary>
        public int Result
        {
            set { _result = value; }
            get { return _result; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FeeBack
        {
            set { _feeback = value; }
            get { return _feeback; }
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
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
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
        public DateTime StartTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal OldUpperPrice
        {
            set { _oldupperprice = value; }
            get { return _oldupperprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal OldLowerPrice
        {
            set { _oldlowerprice = value; }
            get { return _oldlowerprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal NewUpperPrice
        {
            set { _newupperprice = value; }
            get { return _newupperprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal NewLowerPrice
        {
            set { _newlowerprice = value; }
            get { return _newlowerprice; }
        }
        public string ResultString
        {
            get
            {
                switch (_result)
                {
                    case 0: return "未审批";
                    case 1: return "不通过";
                    case 2: return "通过";
                    default: return "未审批";
                }
            }
        }
        public short DeleteOld
        {
            set { _deleteold = value; }
            get { return _deleteold; }
        }
        public string DeleteOldOrNot
        {
            get
            {
                switch (_deleteold)
                {
                    case 0: return "修改";
                    case 1: return "删除";
                    case 2: return "添加";
                    default: return "出错";
                }
            }
        }
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        public Guid instanceId
        {
            set { _instanceId = value; }
            get { return _instanceId; }
        }
        #endregion Model
    }
}
