using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 易耗品出入库记录，类型
    /// </summary>
    public enum ExpendableInOutRecordType
    {
        /// <summary>
        /// 入库
        /// </summary>
        [EnumDescription("入库")]
        In = 1,
        /// <summary>
        /// 出库
        /// </summary>
        [EnumDescription("出库")]
        Out = 2
    }
    /// <summary>
    /// 易耗品出入库审批，类型
    /// </summary>
    public enum ExpendableApprovalType
    {
        /// <summary>
        /// 暂无
        /// </summary>
        [EnumDescription("暂无")]
        NoYet = 0,
        /// <summary>
        /// 同意
        /// </summary>
        [EnumDescription("同意")]
        AGREE = 1,
        /// <summary>
        /// 不同意
        /// </summary>
        [EnumDescription("不同意")]
        NOAGREE = 2,
        /// <summary>
        /// 其它
        /// </summary>
        [EnumDescription("其它")]
        OTHER = 3
    }
    /// <summary>
    /// 易耗品出入库记录
    /// </summary>
    public class ExpendableInOutRecordInfo
    {
        public ExpendableInOutRecordInfo()
		{}
		#region Model
		private long _id;
        private string _companyid;
		private string _model;
		private decimal _amount;
        private string _typestr;
		private string _unit;
		private decimal _price;
		private long _categoryid;
		private string _remark;
        private ExpendableInOutRecordType _type;
		private string _warehouseid;
		private string _warehousekeeper;
		private string _warehousekeepername;
		private string _receiver;
		private string _receivername;
		private DateTime _inouttime;
		private string _name;
        private string _companyname;
        private string _warehousename;
        private string _amountstr;
        private long _sheetid;

        public string Typestr
        {
            set { _typestr = value; }
            get { return _typestr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long SheetID
        {
            set { _sheetid = value; }
            get { return _sheetid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Amountstr
        {
            set { _amountstr = value; }
            get { return _amountstr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Warehousename
        {
            set { _warehousename = value; }
            get { return _warehousename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Companyname
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
		/// <summary>
		/// 
		/// </summary>
        public long ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Model
		{
			set{ _model=value;}
			get{return _model;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Amount
		{
			set{ _amount=value;
            this._amountstr = Convert.ToInt32(value).ToString();
            }
			get{return _amount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Unit
		{
			set{ _unit=value;}
			get{return _unit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
        public long CategoryID
		{
			set{ _categoryid=value;}
			get{return _categoryid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
        public ExpendableInOutRecordType Type
		{
            set
            {
                _type = value;
                switch (value)
                {
                    case ExpendableInOutRecordType.In:
                        this._typestr = "入库";
                        break;
                    case ExpendableInOutRecordType.Out:
                        this._typestr = "出库";
                        break;
                }
            }
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WarehouseID
		{
			set{ _warehouseid=value;}
			get{return _warehouseid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WarehouseKeeper
		{
			set{ _warehousekeeper=value;}
			get{return _warehousekeeper;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WarehouseKeeperName
		{
			set{ _warehousekeepername=value;}
			get{return _warehousekeepername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Receiver
		{
			set{ _receiver=value;}
			get{return _receiver;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReceiverName
		{
			set{ _receivername=value;}
			get{return _receivername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime InOutTime
		{
			set{ _inouttime=value;}
			get{return _inouttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }

		#endregion Model
    }

    /// <summary>
    /// 易耗品出入库记录
    /// </summary>
    public class ExpendableInOutRecordSearchInfo
    {
        public ExpendableInOutRecordSearchInfo()
        { }
        #region Model
        private long _id;
        private string _model;
        private decimal _amount;
        private string _unit;
        private decimal _price;
        private long _categoryid;
        private string _remark;
        private ExpendableInOutRecordType _type;
        private string _warehouseid;
        private string _warehousekeeper;
        private string _warehousekeepername;
        private string _receiver;
        private string _receivername;
        private DateTime _inouttimelower;
        private DateTime _inouttimeupper;
        private string _name;
        private string _companyid;
        /// <summary>
        /// 
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
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
        public decimal Amount
        {
            set { _amount = value; }
            get { return _amount; }
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
        public decimal Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long CategoryID
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public ExpendableInOutRecordType Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WarehouseID
        {
            set { _warehouseid = value; }
            get { return _warehouseid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WarehouseKeeper
        {
            set { _warehousekeeper = value; }
            get { return _warehousekeeper; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WarehouseKeeperName
        {
            set { _warehousekeepername = value; }
            get { return _warehousekeepername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Receiver
        {
            set { _receiver = value; }
            get { return _receiver; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReceiverName
        {
            set { _receivername = value; }
            get { return _receivername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime InOutTimeLower
        {
            set { _inouttimelower = value; }
            get { return _inouttimelower; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime InOutTimeUpper
        {
            set { _inouttimeupper = value; }
            get { return _inouttimeupper; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        public string CompanyID
        {
            get { return _companyid; }
            set { _companyid = value; }
        }
        #endregion Model
    }
}
