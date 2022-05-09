using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FM2E.Model.Basic;

namespace FM2E.Model.Equipment
{
    [Serializable]
    public class EquipmentInfoFacade
    {
        #region Model
        private long _equipmentid;
        private string _serialnum;
        private string _lastaddress;
        private string _photourl;
        private string _model;
        private string _specification;
        private EquipmentStatus _status;
        private long _supplierid;
        private string _suppliername;
        private long _producerid;
        private string _producername;
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
        private bool? _iscancel;
        private DateTime _updatetime;
        private string _companyid;
        private string companyname;
        private string _sectionid;
        private string sectionname;
        private string _locationtag;
        private string _locationid;
        private string _systemid;
        private string systemname;
        private string _purchaseorderid;
        private string purchaseordername;
        private string responsibilityname;
        private string purchasername;
        private string checkername;
        private string categoryname;
        private int nextSplitNO;
        private string _unit;
        private string _detaillocation = "";
        private bool _visible;
        private string _CategoryCode;
        private decimal _DepreciationPrice;
        private int _count;
        private AddressType _addresstype;
        private int _warming;
        public decimal DepreciationPrice
        {
            get { return _DepreciationPrice; }
            set { _DepreciationPrice = value; }
        }

        public string CategoryCode
        {
            get { return _CategoryCode; }
            set { _CategoryCode = value; }
        }


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

        /// <summary>
        /// 
        /// </summary>
        public long EquipmentID
        {
            set { _equipmentid = value; }
            get { return _equipmentid; }
        }
        /// <summary>
        ///   设备类型，新件，二手件
        /// </summary>
        public string SerialNum
        {
            set { _serialnum = value; }
            get { return _serialnum; }
        }

        ///<summary>
        ///上一地址
        ///<summary>
        public string LastAddress
        {
            set { _lastaddress = value; }
            get { return _lastaddress; }
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
        /// 
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
        //public string Statusstr
        //{
        //    get
        //    {
        //        switch (_status)
        //        {
        //            case 1:
        //                {
        //                    return "正常";
                          
        //                }
        //            case 2:
        //                {
        //                    return "故障待修";
                         
        //                }
        //            case 3:
        //                {
        //                    return "报废";
                           
        //                }
        //            case 4:
        //                {
        //                    return "遗失";
                           
        //                }
        //            default: return "";

        //        }
        //    }
        //}
        /// <summary>
        /// 
        /// </summary>
        public long SupplierID
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }

        public string SupplierName
        {
            set { _suppliername = value; }
            get { return _suppliername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ProducerID
        {
            set { _producerid = value; }
            get { return _producerid; }
        }

        public string ProducerName
        {
            set { _producername = value; }
            get { return _producername; }
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

        public string CategoryName
        {
            set { categoryname = value; }
            get { return categoryname; }
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
        public bool? IsCancel
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

        public string CompanyName
        {
            set { companyname = value; }
            get { return companyname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SectionID
        {
            set { _sectionid = value; }
            get { return _sectionid; }
        }

        public string SectionName
        {
            set { sectionname = value; }
            get { return sectionname; }
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

        public string SystemName
        {
            set { systemname = value; }
            get { return systemname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PurchaseOrderID
        {
            set { _purchaseorderid = value; }
            get { return _purchaseorderid; }
        }
        public string PurchaseOrderName
        {
            set { purchaseordername = value; }
            get { return purchaseordername; }
        }
        public string ResponsibilityName
        {
            set { responsibilityname = value; }
            get { return responsibilityname; }
        }
        /// <summary>
        /// 下一分拆号
        /// </summary>
        public int NextSplitNO
        {
            set { nextSplitNO = value; }
            get { return nextSplitNO; }
        }
        /// <summary>
        /// 设备单位，通过设备种类获取
        /// </summary>
        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
        #endregion Model

        private IList _relateddevice = new List<EquipmentInfoFacade>();
        /// <summary>
        /// 相关设备
        /// </summary>
        public IList RelatedDevice
        {
            get
            {
                
                return _relateddevice;
            }
            set
            {
                List<EquipmentInfoFacade> childrenlist = new List<EquipmentInfoFacade>();
                foreach (EquipmentInfoFacade item in value)
                {
                    if (item.EquipmentNO != this.EquipmentNO)
                    {
                        if (item.EquipmentNO.Substring(item.EquipmentNO.Length - 2, 1) == "0")
                            _basicequipment = item;
                        else
                            childrenlist.Add(item);
                    }
                }
                _childrenlist = childrenlist;
                _relateddevice = value;
            }
        }

        private IList _childrenlist = new List<EquipmentInfoFacade>();
        public IList ChildrenList
        {
            get { return _childrenlist; }
            set { _childrenlist = value; }
        }

        private EquipmentInfoFacade _basicequipment;

        /// <summary>
        /// 父设备ID
        /// </summary>
        public EquipmentInfoFacade BasicEquipment
        {
            get { return _basicequipment; }
            set { _basicequipment = value; }
        }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
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
        /// 地址类型
        /// </summary>
        public AddressType AddressType
        {
            set { _addresstype = value; }
            get { return _addresstype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Warming  
        {
            set { _warming = value; }
            get { return _warming; }
        }
    }
}
