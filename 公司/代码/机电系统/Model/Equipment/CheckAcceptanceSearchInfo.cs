using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 采购单查找信息实体类
    /// </summary>
    public class CheckAcceptanceSearchInfo
    {
        public CheckAcceptanceSearchInfo() { }



        //private long _id;

        private string _companyid;

        private string _sheetid;

        private string _sheetname;

        private string _productname;

        private string _model;

        private string _applicant;

        private string _purchaser;

        private string _applicantname;

        private string _purchasername;

        private string _warehouseid;

        private bool? _purchaserconfirm;

        private List<CheckAcceptanceStatus> _statuslist = new List<CheckAcceptanceStatus>();

        private List<PurchaseRecordStatus> _detailstatuslist = new List<PurchaseRecordStatus>();

        private string _systemid;

        /// <summary>
        /// 系统划分ID
        /// </summary>
        public string SystemID
        {
            get { return _systemid; }
            set { _systemid = value; }
        }

        /// <summary>
        /// 是否已经是采购人核实的
        /// </summary>
        public bool? PurchaserConfirm
        {
            get { return _purchaserconfirm; }
            set { _purchaserconfirm = value; }
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
        /// 要搜索表单状态
        /// </summary>
        public List<CheckAcceptanceStatus> StatusList
        {
            get { return _statuslist; }
            set { _statuslist = value; }
        }

        /// <summary>
        /// 要搜索的详情的状态
        /// </summary>
        public List<PurchaseRecordStatus> DetailStatusList
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
        public string SheetID
        {
            get { return _sheetid; }
            set { _sheetid = value; }
        }
        /// <summary>
        /// 表单名称，模糊匹配 
        /// </summary>
        public string SheetName
        {
            get { return _sheetname; }
            set { _sheetname = value; }
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
        /// 提交人姓名，模糊匹配
        /// </summary>
        public string ApplicantName
        {
            get { return _applicantname; }
            set { _applicantname = value; }
        }
        /// <summary>
        /// 提交人ID，完全匹配
        /// </summary>
        public string ApplicantID
        {
            get { return _applicant; }
            set { _applicant = value; }
        }
        /// <summary>
        /// 采购人ID
        /// </summary>
        public string PurchaserID
        {
            get { return _purchaser; }
            set { _purchaser = value; }
        }

        /// <summary>
        /// 采购人姓名
        /// </summary>
        public string PurchaserName
        {
            get { return _purchasername; }
            set { _purchasername = value; }
        }
    }
}
