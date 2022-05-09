using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Equipment
{
    //public enum OutWarehouseApplyStatus
    //{
    //    /// <summary>
    //    /// 未知状态
    //    /// </summary>
    //    UnKnownStatus,
    //    /// <summary>
    //    /// 草稿
    //    /// </summary>
    //    Draft = 1,
    //    /// <summary>
    //    /// 等待审批结果
    //    /// </summary>
    //    Waiting4ApprovalResult = 2,
    //    /// <summary>
    //    /// 审批通过，等待出库
    //    /// </summary>
    //    ApprovalPassed = 3,
    //    /// <summary>
    //    /// 已出库
    //    /// </summary>
    //    Received = 4,
    //    /// <summary>
    //    /// 已拒绝
    //    /// </summary>
    //    ApprovalFailed = 5
    //}

    /// <summary>
    /// 出库申请单实体类
    /// </summary>
    [Serializable]
    public class OutWarehouseApplyInfo
    {
        #region Model
        private long _id;
        //private OutWarehouseApplyStatus _status;
        //private string _FeedBack;
        private DateTime _outtime;
        private string _receiver;
        private string _operator;
        private string _outwarehouseremark;
        private string _warehouseid;
        private DateTime _applytime;
        private string _applyremark;
        private string _companyid;
        private string _applicant;
        //private string _approvaler;
        //private bool _isdeleted;
        private string _companyname;
        private string _warehousename;

        private long _warehouseaddressid;
        private string _applicantname;
        //private string _approvalername;
        private string _receivername;
        private string _operatorname;
        //private DateTime _approvaltime;
        private string _sheetname;
        private IList _applydetaillist = new List<OutWarehouseDetailInfo>();

        private IList _approvallist = new List<OutWarehouseApprovalInfo>();
        /// <summary>
        /// 审批信息列表
        /// </summary>
        public IList ApprovalList
        {
            get { return _approvallist; }
            set { _approvallist = value; }
        }
        /// <summary>
        /// 出库申请单自增ID
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        ///// <summary>
        ///// 
        ///// </summary>
        //public OutWarehouseApplyStatus Status
        //{
        //    set { _status = value; }
        //    get { return _status; }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //public string FeedBack
        //{
        //    set { _FeedBack = value; }
        //    get { return _FeedBack; }
        //}
        /// <summary>
        /// 出库时间
        /// </summary>
        public DateTime OutTime
        {
            set { _outtime = value; }
            get { return _outtime; }
        }
        /// <summary>
        /// 接收人ID
        /// </summary>
        public string Receiver
        {
            set { _receiver = value; }
            get { return _receiver; }
        }
        /// <summary>
        /// 经办人ID
        /// </summary>
        public string Operator
        {
            set { _operator = value; }
            get { return _operator; }
        }
        /// <summary>
        /// 出库备注
        /// </summary>
        public string OutWarehouseRemark
        {
            set { _outwarehouseremark = value; }
            get { return _outwarehouseremark; }
        }
        /// <summary>
        /// 所出仓库
        /// </summary>
        public string WarehouseID
        {
            set { _warehouseid = value; }
            get { return _warehouseid; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime
        {
            set { _applytime = value; }
            get { return _applytime; }
        }
        /// <summary>
        /// 申请备注
        /// </summary>
        public string ApplyRemark
        {
            set { _applyremark = value; }
            get { return _applyremark; }
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
        /// 申请人账号
        /// </summary>
        public string Applicant
        {
            set { _applicant = value; }
            get { return _applicant; }
        }

        /// <summary>
        /// 申请人职位
        /// </summary>
        public string ApplicantPositionName { get; set; }
        /// <summary>
        /// 申请人部门名称
        /// </summary>
        public string ApplicantDepartmentName { get; set; }
        /// <summary>
        /// 申请人部门ID
        /// </summary>
        public long ApplicantDepartmentID { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public string Approvaler
        //{
        //    set { _approvaler = value; }
        //    get { return _approvaler; }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //public bool IsDeleted
        //{
        //    set { _isdeleted = value; }
        //    get { return _isdeleted; }
        //}
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WarehouseName
        {
            set { _warehousename = value; }
            get { return _warehousename; }
        }
        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplicantName
        {
            set { _applicantname = value; }
            get { return _applicantname; }
        }

        /// <summary>
        /// 仓库地址ID
        /// </summary>
        public long WarehouseAddressID
        {
            get { return _warehouseaddressid; }
            set { _warehouseaddressid = value; }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //public string ApprovalerName
        //{
        //    set { _approvalername = value; }
        //    get { return _approvalername; }
        //}
        /// <summary>
        /// 接收人姓名
        /// </summary>
        public string ReceiverName
        {
            set { _receivername = value; }
            get { return _receivername; }
        }
        /// <summary>
        /// 接收人职位
        /// </summary>
        public string ReceiverPositionName { get; set; }
        /// <summary>
        /// 接收人部门名称
        /// </summary>
        public string ReceiverDepartmentName { get; set; }
        /// <summary>
        /// 接收人部门ID
        /// </summary>
        public long ReceiverDepartmentID { get; set; }

        /// <summary>
        /// 经办人姓名
        /// </summary>
        public string OperatorName
        {
            set { _operatorname = value; }
            get { return _operatorname; }
        }
        /// <summary>
        /// 经办人职位
        /// </summary>
        public string OperatorPositionName { get; set; }
        /// <summary>
        /// 经办人部门名称
        /// </summary>
        public string OperatorDepartmentName { get; set; }
        /// <summary>
        /// 经办人部门ID
        /// </summary>
        public long OperatorDepartmentID { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public DateTime ApprovalTime
        //{
        //    set { _approvaltime = value; }
        //    get { return _approvaltime; }
        //}
        /// <summary>
        /// 出库申请单逻辑单号
        /// </summary>
        public string SheetName
        {
            set { _sheetname = value; }
            get { return _sheetname; }
        }
        /// <summary>
        /// 出库申请明细列表
        /// </summary>
        public IList ApplyDetailList
        {
            set
            {
                _applydetaillist = value;
            }
            get { return _applydetaillist; }
        }


        private string _workflowstatename;
        /// <summary>
        /// 工作流状态
        /// </summary>
        public string WorkFlowStateName
        {
            get { return _workflowstatename; }
            set { _workflowstatename = value; }
        }

        private string _workflowstatedescription;
        /// <summary>
        /// 工作流状态描述
        /// </summary>
        public string WorkFlowStateDescription
        {
            get { return _workflowstatedescription; }
            set { _workflowstatedescription = value; }
        }

        private string _workflowinstanceid;
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public string WorkFlowInstanceID
        {
            get { return _workflowinstanceid; }
            set { _workflowinstanceid = value; }
        }
        /// <summary>
        /// 下一处理用户
        /// </summary>
        public string NextUserName { get; set; }
        /// <summary>
        /// 下一处理用户姓名
        /// </summary>
        public string NextUserPersonName { get; set; }
        /// <summary>
        /// 下一处理用户职位
        /// </summary>
        public string NextUserPositionName { get; set; }
        /// <summary>
        /// 下一处理用户部门ID
        /// </summary>
        public long NextUserDepartmentID { get; set; }

        /// <summary>
        /// 下一处理用户部门名称
        /// </summary>
        public string NextUserDepartmentName { get; set; }

        /// <summary>
        /// 下一处理代理用户
        /// </summary>
        public string DelegateUserName { get; set; }
        /// <summary>
        /// 下一处理代理用户姓名
        /// </summary>
        public string DelegateUserPersonName { get; set; }
        /// <summary>
        /// 下一处理代理用户职位
        /// </summary>
        public string DelegateUserPositionName { get; set; }
        /// <summary>
        /// 下一处理代理用户部门ID
        /// </summary>
        public long DelegateUserDepartmentID { get; set; }

        /// <summary>
        /// 下一处理代理用户部门名称
        /// </summary>
        public string DelegateUserDepartmentName { get; set; }

        #endregion Model
        //private ArrayList _statusarray;



        /// <summary>
        /// 状态数组
        /// </summary>
        //public ArrayList StatusArray
        //{
        //    get { return _statusarray; }
        //    set { _statusarray = value; }
        //}
        //public string StatusName
        //{
        //    get
        //    {
        //        switch (_status)
        //        {
        //            case OutWarehouseApplyStatus.Draft: return "草稿";
        //            case OutWarehouseApplyStatus.Waiting4ApprovalResult: return "等待审批";
        //            case OutWarehouseApplyStatus.ApprovalPassed: return "等待出库";
        //            case OutWarehouseApplyStatus.Received: return "已出库";
        //            case OutWarehouseApplyStatus.ApprovalFailed: return "已拒绝";
        //            default: return "未知状态";
        //        }
        //    }
        //}
        //public string ApprovalTime1
        //{
        //    get
        //    {
        //        if (_approvaltime.Equals(_applytime))
        //            return string.Empty;
        //        else
        //            return _approvaltime.ToString();
        //    }
        //}
        //public string OutTime1
        //{
        //    get
        //    {
        //        if (_outtime.Equals(_applytime))
        //            return string.Empty;
        //        else
        //            return _outtime.ToString();
        //    }
        //}
    }
}
