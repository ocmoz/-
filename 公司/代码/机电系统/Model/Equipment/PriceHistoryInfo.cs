using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    public class PriceHistoryInfo
    {
        #region Model
        private string _companyid;
        private string _productname;
        private string _model;
        private DateTime _starttime;
        private DateTime _endtime;
        private decimal _upperprice;
        private decimal _lowerprice;
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
        public DateTime EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal UpperPrice
        {
            set { _upperprice = value; }
            get { return _upperprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal LowerPrice
        {
            set { _lowerprice = value; }
            get { return _lowerprice; }
        }
        #endregion Model
    }
}
