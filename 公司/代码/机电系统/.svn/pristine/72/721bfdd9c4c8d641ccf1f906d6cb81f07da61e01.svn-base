using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Basic;
using FM2E.Model.Utils;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 设备状态
    /// </summary>
    public enum EquipmentStatus
    {
        //[EnumDescription("未知状态")]
        //Unknown=0,
        //[EnumDescription("正常")]
        //Normal=1,
        //[EnumDescription("故障待修")]
        //Failure=2,
        //[EnumDescription("已报废")]
        //Scrapped=3,
        //[EnumDescription("遗失")]
        //Lossed=4,
        //[EnumDescription("已借出")]
        //Lent=5,
        //[EnumDescription("无法修复")]
        //BeyondRepair=6,
        //[EnumDescription("功能性恢复")]
        //FunctionalityRestore=7,
        //[EnumDescription("测试数据")]
        //TestData = 8


        [EnumDescription("未知状态")]
        Unknown = 0,
        [EnumDescription("在用")]
        Normal = 1,
        [EnumDescription("待修")]
        Failure = 2,
        [EnumDescription("报废")]
        Scrapped = 3,
        [EnumDescription("送修")]
        Delivered = 4,
        [EnumDescription("停用")]
        StopUse = 5,
        [EnumDescription("备用")]
        Standby = 6,
        [EnumDescription("功能性恢复")]
        FunctionalityRestore = 7,
        [EnumDescription("无法修复")]
        BeyondRepair = 8

    }
    [Serializable]
    public class EquipmentInfo
    {
        #region Model
        private long _equipmentid;
        private string _serialnum;
        private string _photourl;
        private string _model;
        private string _specification;
        private EquipmentStatus _status;
        private long _supplierid;
        private long _producerid;
        private string _purchaser;
        private string _responsibility;
        private string _checker;
        private string _equipmentno;
        private DateTime _purchasedate;
        private DateTime _examdate;
        private DateTime _openingdate;
        private DateTime _filedate;
        private DateTime _warrantydate;
        private long _servicelife;
        private decimal _price;
        private long _categoryid;
        private long _depreciationmethod;
        private long _depreciablelife;
        private string _name;
        private decimal _residualrate;
        private long _malongenancetimes;
        private string _remark;
        private bool _iscancel;
        private DateTime _updatetime;
        private string _companyid;
        private string _sectionid;
        private string _locationtag;
        private string _locationid;
        private string _systemid;
        private string _purchaseorderid;
        private string responsibilityname;
        private string purchasername;
        private string checkername;
        private string categoryname;
        private string _producer;
        private string _supplier;
        private string _detaillocation = "";
        private int _count;
        private string _unit;
        private AddressType _addresstype;
        private int _warming;
        /// <summary>
        /// 地址信息ID
        /// </summary>
        public long AddressID { get; set; }

        private string _addresscode;

        /// <summary>
        /// 地址信息编码
        /// </summary>
        public string AddressCode
        {
            get { return _addresscode; }
            set { _addresscode = value; }
        }
        /// <summary>
        /// warming,该做缓存状态
        /// </summary>
        public int Warming
        {
            get { return _warming; }
            set { _warming = value; }
        }

        private string _addressname;
        /// <summary>
        /// 地址信息名称
        /// </summary>
        public string AddressName
        {
            get { return _addressname; }
            set { _addressname = value; }
        }
        /// <summary>
        /// 资产编号
        /// </summary>
        public string AssertNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 位置详细信息
        /// </summary>
        public string DetailLocation
        {
            set { _detaillocation = value; }
            get { return _detaillocation; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// 
        public string CategoryName
        {
            set { categoryname = value; }
            get { return categoryname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PurchaserName
        {
            set { purchasername = value; }
            get { return purchasername; }
        }

        public string CheckerName
        {
            set { checkername = value; }
            get { return checkername; }
        }

        public long EquipmentID
        {
            set { _equipmentid = value; }
            get { return _equipmentid; }
        }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string SerialNum
        {
            set { _serialnum = value; }
            get { return _serialnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PhotoUrl
        {
            set { _photourl = value; }
            get { return _photourl; }
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
        /// 原规格，现改为品牌
        /// </summary>
        public string Specification
        {
            set { _specification = value; }
            get { return _specification; }
        }
        /// <summary>
        /// 
        /// </summary>
        public EquipmentStatus Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long SupplierID
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ProducerID
        {
            set { _producerid = value; }
            get { return _producerid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Purchaser
        {
            set { _purchaser = value; }
            get { return _purchaser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Responsibility
        {
            set { _responsibility = value; }
            get { return _responsibility; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Checker
        {
            set { _checker = value; }
            get { return _checker; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EquipmentNO
        {
            set { _equipmentno = value; }
            get { return _equipmentno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime PurchaseDate
        {
            set { _purchasedate = value; }
            get { return _purchasedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ExamDate
        {
            set { _examdate = value; }
            get { return _examdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime OpeningDate
        {
            set { _openingdate = value; }
            get { return _openingdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime FileDate
        {
            set { _filedate = value; }
            get { return _filedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime WarrantyDate
        {
            set { _warrantydate = value; }
            get { return _warrantydate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ServiceLife
        {
            set { _servicelife = value; }
            get { return _servicelife; }
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
        public long DepreciationMethod
        {
            set { _depreciationmethod = value; }
            get { return _depreciationmethod; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long DepreciableLife
        {
            set { _depreciablelife = value; }
            get { return _depreciablelife; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal ResidualRate
        {
            set { _residualrate = value; }
            get { return _residualrate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long MaintenanceTimes
        {
            set { _malongenancetimes = value; }
            get { return _malongenancetimes; }
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
        public bool IsCancel
        {
            set { _iscancel = value; }
            get { return _iscancel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
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
        public string SectionID
        {
            set { _sectionid = value; }
            get { return _sectionid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string LocationTag
        {
            set { _locationtag = value; }
            get { return _locationtag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LocationID
        {
            set { _locationid = value; }
            get { return _locationid; }
        }
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
        public string PurchaseOrderID
        {
            set { _purchaseorderid = value; }
            get { return _purchaseorderid; }
        }
        public string ResponsibilityName
        {
            set { responsibilityname = value; }
            get { return responsibilityname; }
        }

        /// <summary>
        /// 生产商名称
        /// </summary>
        public string ProducerName
        {
            get { return _producer; }
            set { _producer = value; }
        }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName
        {
            get { return _supplier; }
            set { _supplier = value; }
        }
        /// <summary>
        /// 设备数量
        /// </summary>
        public int Count
        {
            set { _count = value; }
            get { return _count; }
        }
        /// <summary>
        /// 设备单位
        /// </summary>
        public string Unit
        {
            set { _unit = value; }
            get { return _unit; }
        }
        /// <summary>
        /// 地址类型
        /// </summary>
        public AddressType AddressType
        {
            set { _addresstype = value; }
            get { return _addresstype; }
        }

        #endregion Model
        
    }
}
