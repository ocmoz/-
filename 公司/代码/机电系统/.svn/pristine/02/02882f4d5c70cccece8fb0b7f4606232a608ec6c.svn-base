using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Archives
{
    public enum ArchivesBorrowApplyStatus
    {
        /// <summary>
        /// 未知状态
        /// </summary>
        UnKnownStatus,
        /// <summary>
        /// 草稿
        /// </summary>
        Draft = 1,
        /// <summary>
        /// 等待审批结果
        /// </summary>
        Waiting4ApprovalResult = 2,
        /// <summary>
        /// 审批通过
        /// </summary>
        ApprovalPassed = 3,
        /// <summary>
        /// 审批不通过
        /// </summary>
        ApprovalFailed = 4,
        /// <summary>
        /// 过期
        /// </summary>
        ApplyOutOfDate = 5

    }
    /// <summary>
    /// 档案借阅申请单信息实体类
    /// </summary>
    public class ArchivesBorrowApplyInfo
    {
        #region Model
        private long _id;
        private string _approvaler;
        private string _approvalername;
        private DateTime _approvaldate;
        private ArchivesBorrowApplyStatus _applystatus;
        private string _sheetno;
        private string _applicant;
        private DateTime _applydate;
        private string _applicantname;
        private long _applicantdept;
        private string _applicantdeptname;
        private string _borrowreason;
        private DateTime _borrowtime;
        private string _remark;
        private string _approvalopinion;
        private IList _applydetaillist;
        /// <summary>
        /// 申请单流水号
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 审批人账号
        /// </summary>
        public string Approvaler
        {
            set { _approvaler = value; }
            get { return _approvaler; }
        }
        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string ApprovalerName
        {
            set { _approvalername = value; }
            get { return _approvalername; }
        }
        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime ApprovalDate
        {
            set { _approvaldate = value; }
            get { return _approvaldate; }
        }
        /// <summary>
        /// 申请单状态
        /// </summary>
        public ArchivesBorrowApplyStatus ApplyStatus
        {
            set { _applystatus = value; }
            get { return _applystatus; }
        }
        /// <summary>
        /// 申请单逻辑单号
        /// </summary>
        public string SheetNo
        {
            set { _sheetno = value; }
            get { return _sheetno; }
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
        /// 申请日期
        /// </summary>
        public DateTime ApplyDate
        {
            set { _applydate = value; }
            get { return _applydate; }
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
        /// 申请人部门ID
        /// </summary>
        public long ApplicantDept
        {
            set { _applicantdept = value; }
            get { return _applicantdept; }
        }
        /// <summary>
        /// 申请人部门名称
        /// </summary>
        public string ApplicantDeptName
        {
            set { _applicantdeptname = value; }
            get { return _applicantdeptname; }
        }
        /// <summary>
        /// 申请理由
        /// </summary>
        public string BorrowReason
        {
            set { _borrowreason = value; }
            get { return _borrowreason; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime BorrowTime
        {
            set { _borrowtime = value; }
            get { return _borrowtime; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string ApprovalOpinion
        {
            set { _approvalopinion = value; }
            get { return _approvalopinion; }
        }
        #endregion Model
        private ArrayList _statusarray;
        /// <summary>
        /// 状态数组
        /// </summary>
        public ArrayList StatusArray
        {
            get { return _statusarray; }
            set { _statusarray = value; }
        }
        /// <summary>
        /// 申请详情列表
        /// </summary>
        public IList ApplyDetailList
        {
            set
            {
                _applydetaillist = value;
            }
            get { return _applydetaillist; }
        }
        /// <summary>
        /// 当前申请的状态文字描述
        /// </summary>
        public string StatusString
        {
            get
            {
                string statusString = "";
                switch (_applystatus)
                {
                    case ArchivesBorrowApplyStatus.Draft:
                        statusString = "草稿";
                        break;
                    case ArchivesBorrowApplyStatus.Waiting4ApprovalResult:
                        statusString = "等待审批结果";
                        break;
                    case ArchivesBorrowApplyStatus.ApprovalPassed:
                        statusString = "审批通过";
                        break;
                    case ArchivesBorrowApplyStatus.ApprovalFailed:
                        statusString = "审批不通过";
                        break;
                    case ArchivesBorrowApplyStatus.ApplyOutOfDate:
                        statusString = "过期";
                        break;
                    default:
                        statusString = "未知状态";
                        break;
                }
                return statusString;
            }
        }
        /// <summary>
        /// 申请时间字符串表示
        /// </summary>
        public string BorrowTimeString
        {
            get
            {
                TimeSpan span = _borrowtime.Subtract(_applydate);
                return span.Days.ToString() + "天";
            }
        }
        /// <summary>
        /// 审批时间字符串表示
        /// </summary>
        public string ApprovalTime1
        {
            get
            {
                return _approvaldate == _applydate ? "" : _approvaldate.ToString();
            }
        }
    }
}
