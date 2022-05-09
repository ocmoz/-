using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
     public class PriceDetailInfo
    {
        #region Model
        private string _companyid;
        private string _companyname;
        private string _productname;
        private string _model;
        private DateTime _starttime;
        private decimal _upperprice;
        private decimal _lowerprice;
        private decimal _newupperprice;
        private decimal _newlowerprice;
        private string _reason;
        private bool _deleteornot; 
        private string _unit;
         /// <summary>
         /// jiavascript事件ClientClick的字符串
         /// </summary>
        public string ClientClick
        {
            get
            {
                return "javascript:showPopWin('对比历史购买价','PurchaseHistory.aspx?productname=" + ProductName + "&model=" + Model + "', 900, 380, null,true,true);return false;";
            }
        }
         /// <summary>
         /// 是否删除
         /// </summary>
        public bool DeleteOrNot
        {
            set { _deleteornot = value; }
            get { return _deleteornot; }
        }
         /// <summary>
         /// 变更原因
         /// </summary>
        public string Reason
        {
            set { _reason = value; }
            get { return _reason; }
        }
         /// <summary>
         /// 新指导价格下限
         /// </summary>
        public decimal NewLowerPrice
        {
            set { _newlowerprice = value; }
            get { return _newlowerprice; }
        }
         /// <summary>
         /// 新指导价格上限
         /// </summary>
        public decimal NewUpperPrice
        {
            set { _newupperprice = value; }
            get { return _newupperprice; }
        }
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
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
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
