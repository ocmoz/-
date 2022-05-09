using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 采购单明细验收结果
    /// </summary>
    public enum PurchaseOrderDetailAcceptanceResult
    {
        /// <summary>
        /// 未验收
        /// </summary>
        NOTCHECK = 1,
        /// <summary>
        /// 全部不通过
        /// </summary>
        NONE = 2,
        /// <summary>
        /// 部分通过
        /// </summary>
        PART = 3,
        /// <summary>
        /// 全部通过
        /// </summary>
        ALL = 4

        
    }

    /// <summary>
    /// 采购单明细状态，包括等待指派、等待采购、采购中、采购完成(不需要理会采购结果，例如失败、部分失败、全部成功，都是采购完成)
    /// </summary>
    public enum PurchaseOrderDetailStatus
    {
        /// <summary>
        /// 等待指派
        /// </summary>
        WAITING4DELIVERY = 1,
        /// <summary>
        /// 等待采购
        /// </summary>
        WAITING4PURCHASE = 2,
        /// <summary>
        /// 采购中
        /// </summary>
        PURCHASING =3,
        /// <summary>
        /// 采购完成
        /// </summary>
        PURCHASINGFINISH =4,
        /// <summary>
        /// 验收完成 
        /// </summary>
        CHECKFINISH =5,
        /// <summary>
        /// 入库完成，即结束
        /// </summary>
        INWAREHOUSEFINISH =6
    }

    /// <summary>
    /// 明细项类型，包括消耗品、设备。消耗品不需要贴条形码，只需要增加数量；设备为可数，需要贴条形码
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// 消耗品
        /// </summary>
        EXPENDABLE = 1,

        /// <summary>
        /// 设备
        /// </summary>
        DEVICE = 2
    }

    /// <summary>
    /// 采购单明细实体
    /// </summary>
    public class PurchaseOrderDetailInfo
    {
        public PurchaseOrderDetailInfo()
        {
        }
        #region Model
        private long _id;
        private decimal _adjustcount;
        private decimal _finalcount;
        private string _unit;
        private decimal _price;
        private decimal _adjustprice;
        private decimal _finalprice;
        private string _remark;
        private string _purchaseremark;
        //private decimal _actualcount;
        //private decimal _actualprice;
        private string _checker_wk;
        private string _purchaseorderid;
        private string _checker_technician;
        private PurchaseOrderDetailAcceptanceResult _acceptanceresult;
        private string _acceptanceremark;
        private short _suborderindex;
        private string _companyid;
        private short _itemid;
        private string _productname;
        private string _model;
        private bool _isasset;
        private decimal _plancount;
        //private decimal _acceptedcount;
        private PurchaseOrderDetailStatus _status = PurchaseOrderDetailStatus.WAITING4DELIVERY;
        private DateTime _purchasetime;
        private string _warehouseid;
        private DateTime _acceptedtime;
        private ItemType _type;
        private string _producer;
        private string _supplier;
        private bool _divide;

        //private IList _basebarcode = new List<PurchaseBarcodeInfo>();
        //private IList _childrenbarcode = new List<PurchaseBarcodeInfo>();
        /// <summary>
        /// 采购单在数据库中的序列号
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 采购数量领导建议值
        /// </summary>
        public decimal AdjustCount
        {
            set { _adjustcount = value; }
            get { return _adjustcount; }
        }

        /// <summary>
        /// 采购小计领导建议值
        /// </summary>
        public decimal AdjustAmount
        {
            get { return _adjustcount*_adjustprice; }
        }

        /// <summary>
        /// 采购数量最终审批值
        /// </summary>
        public decimal FinalCount
        {
            set { _finalcount = value; }
            get { return _finalcount; }
        }

        /// <summary>
        /// 采购小计最终审批值
        /// </summary>
        public decimal FinalAmount
        {
            get { return _finalcount * _finalprice; }
        }

        /// <summary>
        /// 需要采购的物品的单位
        /// </summary>
        public string Unit
        {
            set { _unit = value; }
            get { return _unit == null ? "" : _unit; }
        }
        /// <summary>
        /// 申请采购的单价
        /// </summary>
        public decimal Price
        {
            set { _price = value; }
            get { return _price; }
        }

        private decimal _beforeadjustprice;
        /// <summary>
        /// 审批过程中领导修改之前的价格，不在数据库保存，而在审批的时候读取出来再赋值
        /// </summary>
        public decimal BeforeAdjustPrice
        {
            set { _beforeadjustprice = value; }
            get { return _beforeadjustprice; }
        }

        private decimal _beforeadjustcount;
        /// <summary>
        /// 审批过程中领导修改之前的数量，不在数据库保存，而在审批的时候读取出来再赋值
        /// </summary>
        public decimal BeforeAdjustCount
        {
            set { _beforeadjustcount = value; }
            get { return _beforeadjustcount; }
        }

        /// <summary>
        /// 领导建议的单价
        /// </summary>
        public decimal AdjustPrice
        {
            set { _adjustprice = value; }
            get { return _adjustprice; }
        }
        /// <summary>
        /// 最终审批的单价
        /// </summary>
        public decimal FinalPrice
        {
            set { _finalprice = value; }
            get { return _finalprice; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark == null ? "" : _remark; }
        }

        /// <summary>
        /// 采购者采购结果的备注
        /// </summary>
        public string PurchaseRemark
        {
            set { _purchaseremark = value; }
            get { return _purchaseremark == null ? "" : _purchaseremark; }
        }

        /// <summary>
        /// 实际采购数量
        /// </summary>
        public decimal ActualCount
        {
            //set { _actualcount = value; }
            get {

                decimal amount = 0;
                if (PurchaseRecordList != null)
                {
                    foreach (PurchaseRecordInfo p in PurchaseRecordList)
                    {
                        amount += p.PurchaseCount;
                    }
                }
                return amount;
            }
        }
        /// <summary>
        /// 实际采购单价(平均)
        /// </summary>
        public decimal ActualPrice
        {
            //set { _actualprice = value; }
            //get { return _actualprice; }
            get
            {

                if (ActualCount != 0)
                {
                    return ActualAmount / ActualCount;
                }
                else
                    return 0;
            }
        }

        /// <summary>
        /// 实绩采购金额
        /// </summary>
        public decimal ActualAmount
        {
            //get { return _actualprice * _actualcount; }
            get
            {

                decimal amount = 0;
                if (PurchaseRecordList != null)
                {
                    foreach (PurchaseRecordInfo p in PurchaseRecordList)
                    {
                        amount += p.PurchaseAmount;
                    }
                }
                return amount;
            }
        }

        /// <summary>
        /// 仓库验收人ID
        /// </summary>
        public string Checker_WK
        {
            set { _checker_wk = value; }
            get { return _checker_wk == null ? "" : _checker_wk; }
        }
        /// <summary>
        /// 采购单编号
        /// </summary>
        public string PurchaseOrderID
        {
            set { _purchaseorderid = value; }
            get { return _purchaseorderid; }
        }
        /// <summary>
        /// 技术验收人ID
        /// </summary>
        public string Checker_Technician
        {
            set { _checker_technician = value; }
            get { return _checker_technician == null ? "" : _checker_technician; }
        }

        private string _purchaser;

        /// <summary>
        /// 采购人ID
        /// </summary>
        public string Purchaser
        {
            set { _purchaser = value; }
            get { return _purchaser == null ? "" : _purchaser; }
        }

        /// <summary>
        /// 验收结果，0未验收，1全部不通过，2部分通过，3全部通过
        /// </summary>
        public PurchaseOrderDetailAcceptanceResult AcceptanceResult
        {
            set { _acceptanceresult = value; }
            get 
            {
                return _acceptanceresult; 
            }
        }

        /// <summary>
        /// 验收通过数量
        /// </summary>
        public decimal AcceptedCount
        {
            //set { _acceptedcount = value; }
            get {
                decimal amount = 0;
                if (PurchaseRecordList != null)
                {
                    foreach (PurchaseRecordInfo p in PurchaseRecordList)
                    {
                        amount += p.AcceptanceCount;
                    }
                }
                return amount;
               // return _acceptedcount; 
            }
        }
        /// <summary>
        /// 验收备注
        /// </summary>
        public string AcceptanceRemark
        {
            set { _acceptanceremark = value; }
            get { return _acceptanceremark == null ? "" : _acceptanceremark; }
        }
        /// <summary>
        /// 子采购单序号，从1开始
        /// </summary>
        public short SubOrderIndex
        {
            set { _suborderindex = value; }
            get { return _suborderindex; }
        }
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 采购物品在子采购单中的项目序号
        /// </summary>
        public short ItemID
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname == null ? "" : _productname; }
        }

        /// <summary>
        /// 链接字符串使用的产品名称编码
        /// </summary>
        public string ProductNameEncoded
        {
            get
            {
                return Microsoft.JScript.GlobalObject.escape(ProductName);
            }
        }

        /// <summary>
        /// 链接字符串使用的单位编码
        /// </summary>
        public string UnitEncoded
        {
            get
            {
                return Microsoft.JScript.GlobalObject.escape(Unit);
            }
        }

        /// <summary>
        /// 审批的时候查询产品的价格脚本
        /// </summary>
        public string ProductQueryPriceScript
        {
            get{
                return
                    "javascript:showPopWin('查询指导价','QueryPrice.aspx?name=" + ProductNameEncoded
                    + "&model=" + ModelEncoded
                    + "&price=" + AdjustPrice.ToString("0.##")
                    + "&itemid=" + ItemID
                    + "&count=" + AdjustCount.ToString("0.##")
                    + "&unit=" + UnitEncoded
                    + "&adjust={0}"
                    + "', 900, 435, addtolist,true,true);";
            }
        }

        /// <summary>
        /// 审批的时候查询产品库存的脚本
        /// </summary>
        public string ProductQueryStorageScript
        {
            get
            {
                return
                    "javascript:showPopWin('查询库存量','QueryStorage.aspx?name=" + ProductNameEncoded
                    + "&model=" + ModelEncoded
                    + "&price=" + AdjustPrice.ToString("0.##")
                    + "&itemid=" + ItemID
                    + "&count=" + AdjustCount.ToString("0.##")
                    + "&unit=" + UnitEncoded
                    + "&adjust={0}"
                    + "', 900, 435, addtolist,true,true);";
            }
        }

        /// <summary>
        /// 产品型号
        /// </summary>
        public string Model
        {
            set { _model = value; }
            get { return _model == null ? "" : _model; }
        }

        /// <summary>
        /// 链接字符串使用的产品规格型号
        /// </summary>
        public string ModelEncoded
        {
            get
            {
                return Microsoft.JScript.GlobalObject.escape(Model);
            }
        }

        /// <summary>
        /// 消耗品/固定资产，true固定资产，false消耗品
        /// </summary>
        public bool IsAsset
        {
            set { _isasset = value; }
            get { return _isasset; }
        }
        /// <summary>
        /// 计划购入数量 
        /// </summary>
        public decimal PlanCount
        {
            set { _plancount = value; }
            get { return _plancount; }
        }
        #endregion Model

        /// <summary>
        /// 申请采购时候的金额
        /// </summary>
        public decimal PlanAmount
        {
            get { return _price * _plancount; }
        }
        /// <summary>
        /// 采购单明细状态
        /// </summary>
        public PurchaseOrderDetailStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        /// <summary>
        /// 采购时间
        /// </summary>
        public DateTime PurchaseTime
        {
            get { return _purchasetime; }
            set { _purchasetime = value; }
        }

        /// <summary>
        /// 状态显示字符串
        /// </summary>
        public string StatusString
        {
            get
            {
                string status = "";
                switch (_status)
                {

                    case PurchaseOrderDetailStatus.CHECKFINISH:
                        status = "完成验收";
                        break;
                    case PurchaseOrderDetailStatus.PURCHASING:
                        status = "采购中";
                        break;
                    case PurchaseOrderDetailStatus.PURCHASINGFINISH:
                        status = "采购完成";
                        break;
                    case PurchaseOrderDetailStatus.WAITING4DELIVERY:
                        status = "等待指派";
                        break;
                    case PurchaseOrderDetailStatus.WAITING4PURCHASE:
                        status = "等待采购";
                        break;
                    case PurchaseOrderDetailStatus.INWAREHOUSEFINISH:
                        status = "入库完毕";
                        break;
                    default:
                        status = "未知状态";
                        break;
                }
                return status;
            }
        }

        private string _purchasername;
        /// <summary>
        /// 采购者名称
        /// </summary>
        public string PurchaserName
        {
            get
            {
                if (Purchaser == null || Purchaser == "")
                {
                    return "未选定";
                }
                else
                {
                    if (_purchasername == null || _purchasername == "")
                        return _purchaser;
                    else
                    return _purchasername;
                }
            }
            set
            {
                _purchasername = value;
            }
        }
        
        /// <summary>
        /// 验收仓库ID
        /// </summary>
        public string WareHouseID
        {
            get
            {
                return _warehouseid;
            }
            set
            {
                _warehouseid = value;
            }
        }

        private string _warehousename;
        /// <summary>
        /// 验收仓库名称
        /// </summary>
        public string WareHouseName
        {
            get
            {
                return _warehousename;
            }
            set
            {
                _warehousename = value;
            }
        }

        /// <summary>
        /// 验收时间
        /// </summary>
        public DateTime AcceptedTime
        {
            get
            {
                return _acceptedtime;
            }
            set
            {
                _acceptedtime = value;
            }
        }
        /// <summary>
        /// 设备种类，消耗品还是设备
        /// </summary>
        public ItemType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
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
        /// 生产商
        /// </summary>
        public string Producer
        {
            get
            {
                return _producer;
            }
            set
            {
                _producer = value;
            }
        }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier
        {
            get
            {
                return _supplier;
            }
            set
            {
                _supplier = value;
            }
        }
        /// <summary>
        /// 是否可以拆分
        /// </summary>
        public bool Divide
        {
            get
            {
                return _divide;
            }
            set
            {
                _divide = value;
            }
        }

        private IList _purchaserecordlist = new List<PurchaseRecordInfo>();

        /// <summary>
        /// 分批购买记录列表
        /// </summary>
        public IList PurchaseRecordList
        {
            get
            {
                return _purchaserecordlist;
            }
            set
            {
                _purchaserecordlist = value;
            }
        }

        /// <summary>
        /// 以整体入库的列表
        /// </summary>
        //public IList BaseBarcodeList//在读取赋值给barcodelist的时候set
        //{
        //    get
        //    {
        //        return _basebarcode;
        //    }
        //    //set
        //    //{
        //    //    _basebarcode = value;
        //    //}
        //}

        ///// <summary>
        ///// 所有拆分后的设备，未分层次
        ///// </summary>
        //public List<string> ChildrenBarcodeList
        //{
        //    get
        //    {
        //        return _childrenbarcode;
        //    }
        //    set
        //    {
        //        _childrenbarcode = value;
        //    }
        //}

        //private IList _barcodelist;

        ///// <summary>
        ///// 所有入库设备，未分层次
        ///// </summary>
        //public IList BarcodeList
        //{
        //    get
        //    {
        //        return _barcodelist;
        //    }
        //    set
        //    {
        //        _barcodelist = value;
        //        _basebarcode.Clear();
        //        foreach (PurchaseBarcodeInfo item in value)
        //        {
        //            char key = item.Barcode.Substring(item.Barcode.Length - 2, 1)[0];
        //            if(key=='0')
        //                _basebarcode.Add(item);
        //        }
        //    }
        //}



        ///// <summary>
        ///// 所有设备条码，分层次，以“,”分割
        ///// </summary>
        //public List<BarcodeStringInfo> BarcodeStrList
        //{
        //    get
        //    {
        //        List<BarcodeStringInfo> list = new List<BarcodeStringInfo>();
        //        Hashtable hs = new Hashtable();
        //        foreach (PurchaseBarcodeInfo item in _barcodelist)//_barcodelist已经排序完毕的
        //        {
        //            char key = item.Barcode.Substring(item.Barcode.Length-2,1)[0];
        //            if (hs.Contains(key))
        //            {
        //                BarcodeStringInfo str = (BarcodeStringInfo)hs[key];
        //                str.BarcodeRecordString += "," + item.Barcode;
        //                hs[key] = str;
        //            }
        //            else
        //            {
        //                BarcodeStringInfo str = new BarcodeStringInfo();
        //                str.Name = item.ProductName;
        //                str.Model = item.Model;
        //                str.BarcodeRecordString = item.Barcode;
        //                hs.Add(key, str);
        //            }
        //        }
        //        //把hashtable转换为list
        //        foreach (BarcodeStringInfo itemhs in hs.Values)
        //        {
        //            BarcodeStringInfo item = new BarcodeStringInfo();
        //            item.Name = itemhs.Name;
        //            item.Model = itemhs.Model;
        //            item.BarcodeRecordString = itemhs.BarcodeRecordString;
        //            list.Add(item);
        //        }
        //        return list;
        //    }
        //}
        /// <summary>
        /// 判断是否可以改变采购员
        /// </summary>
        public bool CanChangePurchaser
        {
            get
            {
                return _status == PurchaseOrderDetailStatus.WAITING4PURCHASE || _status == PurchaseOrderDetailStatus.WAITING4DELIVERY;
            }
        }

        private string _systemid ;
        /// <summary>
        /// 系统划分ID
        /// </summary>
        public string SystemID
        {
            get
            {
                return _systemid;
            }
            set
            {
                _systemid = value;
            }
        }

        private string _systemname = "";
        /// <summary>
        /// 系统划分名称
        /// </summary>
        public string SystemName
        {
            get
            {
                return _systemname;
            }
            set
            {
                _systemname = value;
            }

        }
    }

    /// <summary>
    /// 用于显示条形码的列表
    /// </summary>
    public class BarcodeStringInfo
    {
        public BarcodeStringInfo() { }

        private short _itemid;
        private long _purchaserecordid;
        private string _name;
        private string _model;
        private string _str;

        public short ItemID
        {
            get { return _itemid; }
            set { _itemid = value; }
        }

        public long PurchaseRecordID
        {
            get { return _purchaserecordid; }
            set { _purchaserecordid = value; }
        }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 产品型号
        /// </summary>
        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        /// <summary>
        /// 条码列表
        /// </summary>
        public string BarcodeRecordString
        {
            get
            {
                return _str;
            }
            set
            {
                _str = value;
            }
        }
    }
}
