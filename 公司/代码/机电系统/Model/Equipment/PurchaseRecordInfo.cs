using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 采购详情状态
    /// </summary>
    public enum PurchaseRecordStatus
    {
        /// <summary>
        /// 无状态
        /// </summary>
        NONE,
        /// <summary>
        /// 采购中
        /// </summary>
        PURCHASING,
        /// <summary>
        ///  等待验收
        /// </summary>
        WAIT4CHECK,
        /// <summary>
        /// 等待入库
        /// </summary>
        WAIT4INWAREHOUSE,
        /// <summary>
        /// 入库完毕
        /// </summary>
        INWAREHOUSEFINISH

    }
    /// <summary>
    /// 采购记录实体类，层次对应关系申购单--申购单详情--采购记录--条码列表
    /// </summary>
    public class PurchaseRecordInfo
    {
        public PurchaseRecordInfo()
		{}
		#region Model
		private long _id;
		private decimal _purchasecount;
		private decimal _purchaseunitprice;
        private string _unit = "";
        private string _producer = "";
        private string _supplier = "";
        private string _purchaseremark = "";
		private DateTime _purchasetime;
        private string _purchaser = "";
		private bool _purchaserconfirm;
		private DateTime _purchaserconfirmtime;
        private long _orderid;
        private string _warehouseid;
        private string _checker_warehouse = "";
        private string _warehousekeepername = "";
        /// <summary>
        /// 仓管员姓名
        /// </summary>
        public string WarehouseKeeperName
        {
            get { return _warehousekeepername; }
            set { _warehousekeepername = value; }
        }

        
        private string _checker_technician = "";
        private string _technicianname = "";
        /// <summary>
        /// 技术验收员姓名
        /// </summary>
        public string TechnicianName
        {
            get { return _technicianname; }
            set { _technicianname = value; }
        }
		private decimal _acceptancecount;
        private PurchaseOrderDetailAcceptanceResult _acceptanceresult;
        private string _acceptanceremark = "";
		private DateTime _acceptedtime;
        private PurchaseRecordStatus _status;
		private bool _isdividable;
		private short _plandetailitemid;
		private string _companyid = "";
        private string _purchaseorderid = "";
        private short _suborderindex;
        private string _productname = "";
        private string _model = "";
		private ItemType _type;
        private string _systemid;
        private string _systemname = "";
        private string _warehousename = "";

        /// <summary>
        /// 系统划分ID
        /// </summary>
        public string SystemID
        {
            set { _systemid = value; }
            get { return _systemid; }
        }

        /// <summary>
        /// 系统划分名称
        /// </summary>
        public string SystemName
        {
            set { _systemname = value; }
            get { return _systemname; }
        }

        /// <summary>
        /// 验收仓库名称
        /// </summary>
        public string WarehouseName
        {
            set { _warehousename = value; }
            get { return _warehousename; }
        }
		/// <summary>
		/// 数据库自增ID
		/// </summary>
		public long  ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 本批采购数量
		/// </summary>
		public decimal PurchaseCount
		{
			set{ _purchasecount=value;}
			get{return _purchasecount;}
		}
		/// <summary>
		/// 本批采购单价
		/// </summary>
		public decimal PurchaseUnitPrice
		{
			set{ _purchaseunitprice=value;}
			get{return _purchaseunitprice;}
		}
		/// <summary>
		/// 本批采购单位
		/// </summary>
		public string Unit
		{
			set{ _unit=value;}
			get{return _unit;}
		}
		/// <summary>
		/// 本批生产商
		/// </summary>
		public string Producer
		{
			set{ _producer=value;}
			get{return _producer;}
		}
		/// <summary>
		/// 本批供应商
		/// </summary>
		public string Supplier
		{
			set{ _supplier=value;}
			get{return _supplier;}
		}
		/// <summary>
		/// 本批采购备注
		/// </summary>
		public string PurchaseRemark
		{
			set{ _purchaseremark=value;}
			get{return _purchaseremark;}
		}
		/// <summary>
		/// 本批采购时间
		/// </summary>
		public DateTime PurchaseTime
		{
			set{ _purchasetime=value;}
			get{return _purchasetime;}
		}
		/// <summary>
		/// 本批采购人账号
		/// </summary>
		public string Purchaser
		{
			set{ _purchaser=value;}
			get{return _purchaser;}
		}
        private string _purchasername;

        /// <summary>
        /// 采购人姓名
        /// </summary>
        public string PurchaserName
        {
            set { _purchasername = value; }
            get { return _purchasername; }
        }


		/// <summary>
		/// 本批采购人核实
		/// </summary>
		public bool PurchaserConfirm
		{
			set{ _purchaserconfirm=value;}
			get{return _purchaserconfirm;}
		}
		/// <summary>
		/// 本批采购人核实时间
		/// </summary>
		public DateTime PurchaserConfirmTime
		{
			set{ _purchaserconfirmtime=value;}
			get{return _purchaserconfirmtime;}
		}
		/// <summary>
		/// 对应申购单ID
		/// </summary>
		public long OrderID
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 对应验收仓库ID
		/// </summary>
		public string WarehouseID
		{
			set{ _warehouseid=value;}
			get{return _warehouseid;}
		}
		/// <summary>
		/// 仓库验收人账号
		/// </summary>
		public string Checker_Warehouse
		{
			set{ _checker_warehouse=value;}
			get{return _checker_warehouse;}
		}
		/// <summary>
		/// 技术验收人账号
		/// </summary>
		public string Checker_Technician
		{
			set{ _checker_technician=value;}
			get{return _checker_technician;}
		}
		/// <summary>
		/// 验收数量
		/// </summary>
		public decimal AcceptanceCount
		{
			set{ _acceptancecount=value;}
			get{return _acceptancecount;}
		}
		/// <summary>
		/// 验收结果
		/// </summary>
        public PurchaseOrderDetailAcceptanceResult AcceptanceResult
		{
			set{ _acceptanceresult=value;}
			get{return _acceptanceresult;}
		}
		/// <summary>
		/// 验收备注
		/// </summary>
		public string AcceptanceRemark
		{
			set{ _acceptanceremark=value;}
			get{return _acceptanceremark;}
		}
		/// <summary>
		/// 验收时间
		/// </summary>
		public DateTime AcceptedTime
		{
			set{ _acceptedtime=value;}
			get{return _acceptedtime;}
		}
		/// <summary>
		/// 采购项状态
		/// </summary>
        public PurchaseRecordStatus Status
		{
			set{ _status=value;}
			get{return _status;}
		}
        /// <summary>
        /// 状态字符串
        /// </summary>
        public string StatusString
        {
            get
            {
                string str = "";
                switch (_status)
                {
                    case PurchaseRecordStatus.INWAREHOUSEFINISH:
                        str = "入库完毕";
                        break;
                    case PurchaseRecordStatus.PURCHASING:
                        str = "采购中";
                        break;
                    case PurchaseRecordStatus.WAIT4CHECK:
                        str = "等待验收";
                        break;
                    case PurchaseRecordStatus.WAIT4INWAREHOUSE:
                        str = "等待入库";
                        break;
                    default:
                        break;
                }
                return str;
            }
        }

		/// <summary>
		/// 本批是否可拆分
		/// </summary>
		public bool IsDividable
		{
			set{ _isdividable=value;}
			get{return _isdividable;}
		}
		/// <summary>
		/// 对应申购单中明细项序号
		/// </summary>
		public short PlanDetailItemID
		{
			set{ _plandetailitemid=value;}
			get{return _plandetailitemid;}
		}
		/// <summary>
		/// 公司ID
		/// </summary>
		public string CompanyID
		{
			set{ _companyid=value;}
			get{return _companyid;}
		}
		/// <summary>
		/// 申购单编号（字符串）
		/// </summary>
		public string PurchaseOrderID
		{
			set{ _purchaseorderid=value;}
			get{return _purchaseorderid;}
		}
		/// <summary>
		/// 申购单子序号
		/// </summary>
		public short SubOrderIndex
		{
			set{ _suborderindex=value;}
			get{return _suborderindex;}
		}
		/// <summary>
		/// 产品名称
		/// </summary>
		public string ProductName
		{
			set{ _productname=value;}
			get{return _productname;}
		}
		/// <summary>
		/// 产品型号
		/// </summary>
		public string Model
		{
			set{ _model=value;}
			get{return _model;}
		}
		/// <summary>
		/// 产品类型
		/// </summary>
        public ItemType Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		#endregion Model

        private IList _basebarcode = new List<PurchaseBarcodeInfo>();
        private IList _childrenbarcode = new List<PurchaseBarcodeInfo>();
        /// <summary>
        /// 以整体入库的列表
        /// </summary>
        public IList BaseBarcodeList//在读取赋值给barcodelist的时候set
        {
            get
            {
                return _basebarcode;
            }
            set
            {
                _basebarcode = value;
            }
        }

        /// <summary>
        /// 所有拆分后的设备，未分层次
        /// </summary>
        public IList ChildrenBarcodeList
        {
            get
            {
                return _childrenbarcode;
            }
            set
            {
                _childrenbarcode = value;
            }
        }

        private IList _barcodelist;

        /// <summary>
        /// 所有入库设备，未分层次
        /// </summary>
        public IList BarcodeList
        {
            get
            {
                return _barcodelist;
            }
            set
            {
                _barcodelist = value;
                _basebarcode.Clear();
                foreach (PurchaseBarcodeInfo item in value)
                {
                    char key = item.Barcode.Substring(item.Barcode.Length - 2, 1)[0];
                    if (key == '0')
                        _basebarcode.Add(item);
                }
            }
        }



        /// <summary>
        /// 所有设备条码，分层次，以“,”分割
        /// </summary>
        public IList BarcodeStrList
        {
            get
            {
                List<BarcodeStringInfo> list = new List<BarcodeStringInfo>();
                Hashtable hs = new Hashtable();
                foreach (PurchaseBarcodeInfo item in _barcodelist)//_barcodelist已经排序完毕的
                {
                    char key = item.Barcode.Substring(item.Barcode.Length - 2, 1)[0];
                    if (hs.Contains(key))
                    {
                        BarcodeStringInfo str = (BarcodeStringInfo)hs[key];
                        str.BarcodeRecordString += "," + item.Barcode;
                        hs[key] = str;
                    }
                    else
                    {
                        BarcodeStringInfo str = new BarcodeStringInfo();
                        str.PurchaseRecordID = item.PurchaseRecordID;
                        str.ItemID = item.ItemID;
                        str.Name = item.ProductName;
                        str.Model = item.Model;
                        str.BarcodeRecordString = item.Barcode;
                        hs.Add(key, str);
                    }
                }
                //把hashtable转换为list
                foreach (BarcodeStringInfo itemhs in hs.Values)
                {
                    BarcodeStringInfo item = new BarcodeStringInfo();
                    item.PurchaseRecordID = itemhs.PurchaseRecordID;
                    item.ItemID = itemhs.ItemID;
                    item.Name = itemhs.Name;
                    item.Model = itemhs.Model;
                    item.BarcodeRecordString = itemhs.BarcodeRecordString;
                    list.Add(item);
                }
                return list;
            }
        }


        public IList GetBarcodeList(int layerindex)
        {
            List<PurchaseBarcodeInfo> list = new List<PurchaseBarcodeInfo>();
            foreach (PurchaseBarcodeInfo item in _barcodelist)//_barcodelist已经排序完毕的
            {
                char key = item.Barcode.Substring(item.Barcode.Length - 2, 1)[0];
                if (int.Parse(key.ToString()) == layerindex)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 本批采购金额
        /// </summary>
        public decimal PurchaseAmount
        {
            get { return _purchaseunitprice * _purchasecount; }
        }

        /// <summary>
        /// 状态显示字符串
        /// </summary>
        public string TypeString
        {
            get
            {
                string type = "";
                switch (_type)
                {

                    case ItemType.DEVICE:
                        type = "设备";
                        break;
                    case ItemType.EXPENDABLE:
                        type = "消耗品";
                        break;

                    default:
                        type = "";
                        break;
                }
                return type;
            }
        }

        /// <summary>
        /// 验收结果字符串
        /// </summary>
        public string AcceptanceResultString
        {
            get
            {
                string str = "";
                switch (_acceptanceresult)
                {
                    case PurchaseOrderDetailAcceptanceResult.ALL:
                        str = "全部通过";
                        break;
                    case PurchaseOrderDetailAcceptanceResult.NONE:
                        str = "全部不通过";
                        break;
                    case PurchaseOrderDetailAcceptanceResult.NOTCHECK:
                        str = "未验收";
                        break;
                    case PurchaseOrderDetailAcceptanceResult.PART:
                        str = "部分通过";
                        break;
                    default:
                        break;
                }
                return str;
            }
        }
    }

}
