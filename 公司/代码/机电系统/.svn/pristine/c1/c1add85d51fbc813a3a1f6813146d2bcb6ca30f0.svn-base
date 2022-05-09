using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 采购单查找信息实体类
    /// </summary>
    public class PurchaseOrderSearchInfo
    {
        public PurchaseOrderSearchInfo() { }


        private string _companyid;

        private string _ordersn;

        private string _ordername;

        private string _productname;

        private string _model;

        private decimal _amountlower = decimal.MinValue;

        private decimal _amountupper = decimal.MaxValue;

        private string _applicant;

        private PurchaseOrderStatus[] _statusarray;

        private DateTime _timelower = DateTime.MinValue;

        private DateTime _timeupper = DateTime.MaxValue;

        private List<PurchaseOrderDetailStatus> _detailstatuslist = new List<PurchaseOrderDetailStatus>();

        private List<PurchaseRecordStatus> _purchaserecordstatus = new List<PurchaseRecordStatus>();

        private string _warehouseid;

        private string _systemid;

        private List<PurchaseRecordStatus> _warehousestatus = new List<PurchaseRecordStatus>();


        private string _workflowusername;
        public string WorkFlowUserName
        {
            get { return _workflowusername; }
            set { _workflowusername = value; }
        }

        private string _nextusername;
        public string NextUserName
        {
            get { return _nextusername; }
            set { _nextusername = value; }
        }

        private string _delegateusernaem;
        public string DelegateUserName
        {
            get { return _delegateusernaem; }
            set { _delegateusernaem = value; }
        }

        private List<string> _workflowstatus = new List<string>();

        /// <summary>
        /// 工作流状态列表
        /// </summary>
        public List<string> WorkFlowStatus
        {
            get { return _workflowstatus; }
            set { _workflowstatus = value; }
        }

        /// <summary>
        /// 受仓库影响的状态
        /// </summary>
        public List<PurchaseRecordStatus> WareHouseRecordStatus
        {
            get { return _warehousestatus; }
            set { _warehousestatus = value; }
        }

       

        /// <summary>
        /// 要搜索的仓库ID
        /// </summary>
        public string WareHouseID
        {
            get { return _warehouseid; }
            set { _warehouseid = value; }
        }


        /// <summary>
        /// 要搜索的采购记录状态(不与仓库冲突，即不受仓库影响)
        /// </summary>
        public List<PurchaseRecordStatus> PurchaseRecordStatusList
        {
            get { return _purchaserecordstatus; }
            set { _purchaserecordstatus = value; }
        }

        /// <summary>
        /// 要搜索的详情的状态
        /// </summary>
        public List<PurchaseOrderDetailStatus> DetailStatusList
        {
            get { return _detailstatuslist; }
            set { _detailstatuslist = value; }
        }

        /// <summary>
        /// 公司ID，完全匹配
        /// </summary>
        public string CompanyID
        {
            get { return _companyid; }
            set { _companyid = value; }
        }

        /// <summary>
        /// 表单编号，完全匹配
        /// </summary>
        public string OrderSn
        {
            get { return _ordersn; }
            set { _ordersn = value; }
        }
        /// <summary>
        /// 表单名称，模糊匹配 
        /// </summary>
        public string OrderName
        {
            get { return _ordername; }
            set { _ordername = value; }
        }
        /// <summary>
        /// 含有产品名称，模糊匹配
        /// </summary>
        public string ProductName
        {
            get { return _productname; }
            set { _productname = value; }
        }
        /// <summary>
        /// 含有产品型号，模糊匹配
        /// </summary>
        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }
        /// <summary>
        /// 申请价格总价下限
        /// </summary>
        public decimal AmountLower
        {
            get { return _amountlower; }
            set { _amountlower = value; }
        }
        /// <summary>
        /// 申请价格总价上限
        /// </summary>
        public decimal AmountUpper
        {
            get { return _amountupper; }
            set { _amountupper = value; }
        }
        /// <summary>
        /// 状态数组
        /// </summary>
        public PurchaseOrderStatus[] StatusArray
        {
            get { return _statusarray; }
            set { _statusarray = value; }
        }
       /// <summary>
       /// 时间下限，与提交时间比较
       /// </summary>
        public DateTime TimeLower
        {
            get { return _timelower; }
            set { _timelower = value; }
        }
        /// <summary>
        /// 时间上限，与最后更新时间比较
        /// </summary>
        public DateTime TimeUpper
        {
            get { return _timeupper; }
            set { _timeupper = value; }
        }
/// <summary>
/// 提交人
/// </summary>
        public string Applicant
        {
            get { return _applicant; }
            set { _applicant = value; }
        }
        /// <summary>
        /// 系统划分ID
        /// </summary>
        public string SystemID
        {
            get { return _systemid; }
            set { _systemid = value; }
        }
    }
}
