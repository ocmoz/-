using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.SpecialProject
{
    /// <summary>
    /// 专项工程合同支付项实体类
    /// </summary>
    public class SpecialProjectContractPayInfo
    {
        public SpecialProjectContractPayInfo()
		{}
		#region Model
        private long _projectid;
        private long _itemid;
        private string _itemname = "";
		private long _planitemid;
        private string _planitemname = "";
		private int _daysafter;
		private decimal _amount;
        private string _method = "";
		private decimal _paid;
        private string _payee = "";
        private string _remark = "";
		/// <summary>
		/// 专项工程ID
		/// </summary>
		public long ProjectID
		{
			set{ _projectid=value;}
			get{return _projectid;}
		}
		/// <summary>
		/// 合同支付项序号
		/// </summary>
        public long ItemID
		{
			set{ _itemid=value;}
			get{return _itemid;}
		}

        /// <summary>
        /// 合同支付项名称
        /// </summary>
        public string ItemName
        {
            set { _itemname = value; }
            get { return _itemname; }
        }

		/// <summary>
		/// 对应的工作计划项序号
		/// </summary>
		public long PlanItemID
		{
			set{ _planitemid=value;}
			get{return _planitemid;}
		}

        /// <summary>
        /// 对应工作项名称
        /// </summary>
        public string PlanItemName
        {
            set { _planitemname = value; }
            get { return _planitemname; }
        }
		/// <summary>
		/// 支付在工作完成后的天数
		/// </summary>
		public int DaysAfter
		{
			set{ _daysafter=value;}
			get{return _daysafter;}
		}
		/// <summary>
		/// 支付金额
		/// </summary>
		public decimal Amount
		{
			set{ _amount=value;}
			get{return _amount;}
		}
		/// <summary>
		/// 支付方式
		/// </summary>
		public string Method
		{
			set{ _method=value;}
			get{return _method;}
		}
		/// <summary>
		/// 已支付金额
		/// </summary>
		public decimal Paid
		{
			set{ _paid=value;}
			get{return _paid;}
		}
		/// <summary>
		/// 支付人
		/// </summary>
		public string Payee
		{
			set{ _payee=value;}
			get{return _payee;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model
    }
}
