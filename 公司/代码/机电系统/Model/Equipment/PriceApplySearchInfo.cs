using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 价格管理申请审批查找信息实体类
    /// </summary>
    public class PriceApplySearchInfo
    {
        private string _companyid;
        private long _applyformid;
        private string _productname;
        private string _model;
        private string _applicant;
        private string _approvaler;
        private DateTime _applytimelower = DateTime.MinValue;
        private DateTime _applytimeupper = DateTime.MaxValue;
        private DateTime _approvaltimelower = DateTime.MinValue;
        private DateTime _approvaltimeupper = DateTime.MaxValue;
        private List<PriceApplyStatus> _status = new List<PriceApplyStatus>();
        private List<string> _wfstatus = new List<string>();

        /// <summary>
        /// 申请单ID
        /// </summary>
        public long ApplyFormID
        {
            get { return _applyformid; }
            set { _applyformid = value; }
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
        /// 产品名称、模糊匹配
        /// </summary>
        public string ProductName
        {
            get { return _productname; }
            set { _productname = value; }
        }

        /// <summary>
        /// 产品型号，模糊匹配
        /// </summary>
        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        /// <summary>
        /// 申请人，完全匹配
        /// </summary>
        public string Applicant
        {
            get { return _applicant; }
            set { _applicant = value; }
        }

        /// <summary>
        /// 审批人，完全匹配
        /// </summary>
        public string Approvaler
        {
            get { return _approvaler; }
            set { _approvaler = value; }
        }

        /// <summary>
        /// 申请时间下界
        /// </summary>
        public DateTime ApplyTimeLower
        {
            get { return _applytimelower; }
            set { _applytimelower = value; }
        }

        /// <summary>
        /// 申请时间上界
        /// </summary>
        public DateTime ApplyTimeUpper
        {
            get { return _applytimeupper; }
            set { _applytimeupper = value; }
        }

        /// <summary>
        /// 审批时间下界
        /// </summary>
        public DateTime ApprovalTimeLower
        {
            get { return _approvaltimelower; }
            set { _approvaltimelower = value; }
        }

        /// <summary>
        /// 审批时间上界
        /// </summary>
        public DateTime ApprovalTimeUpper
        {
            get { return _approvaltimeupper; }
            set { _approvaltimeupper = value; }
        }

        /// <summary>
        /// 表单状态（不是工作流）
        /// </summary>
        public List<PriceApplyStatus> StatusList
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// 工作流状态列表，中间是关系
        /// </summary>
        public List<string> WFStatusList
        {
            get { return _wfstatus; }
            set { _wfstatus = value; }
        }

    }
}
