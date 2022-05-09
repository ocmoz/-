using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Maintain
{
    public enum DailyPatrolPlanStatus
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
        /// 审批通过（正式执行）
        /// </summary>
        ApprovalPassed = 3,
        /// <summary>
        /// 审批不通过（计划中止）
        /// </summary>
        ApprovalFailed = 4,
        /// <summary>
        /// 执行完毕（计划终止）
        /// </summary>
        PlanComplete = 5

    }
    public class DailyPatrolPlanInfo
    {
        /// <summary>
        /// 日常巡查计划状态
        /// </summary>

        #region Model
        private long _planid;
        private string _approvalopinion;
        private string _approvalremark;
        private string _approvaler;
        private string _approvalername;
        private DateTime _approvaldate;
        private DailyPatrolPlanStatus _status;
        private DateTime _updatetime;
        private int _planyear;
        private string _companyid;
        private string _companyname;
        private string _planname;
        private bool _istemporary;
        private string _planner;
        private string _plannername;
        private DateTime _plandate;
        private DateTime _startdate;
        private DateTime _completedate;
        private IList _plandetaillist;
        private long _departmentid;
        private string _departmentname;
        /// <summary>
        /// 
        /// </summary>
        public long DepartmentID
        {
            set { _departmentid = value; }
            get { return _departmentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DepartmentName
        {
            set { _departmentname = value; }
            get { return _departmentname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long PlanID
        {
            set { _planid = value; }
            get { return _planid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ApprovalOpinion
        {
            set { _approvalopinion = value; }
            get { return _approvalopinion; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ApprovalRemark
        {
            set { _approvalremark = value; }
            get { return _approvalremark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Approvaler
        {
            set { _approvaler = value; }
            get { return _approvaler; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ApprovalerName
        {
            set { _approvalername = value; }
            get { return _approvalername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ApprovalDate
        {
            set { _approvaldate = value; }
            get { return _approvaldate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DailyPatrolPlanStatus Status
        {
            set { _status = value; }
            get { return _status; }
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
        public int PlanYear
        {
            set { _planyear = value; }
            get { return _planyear; }
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
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PlanName
        {
            set { _planname = value; }
            get { return _planname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsTemporary
        {
            set { _istemporary = value; }
            get { return _istemporary; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Planner
        {
            set { _planner = value; }
            get { return _planner; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PlannerName
        {
            set { _plannername = value; }
            get { return _plannername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime PlanDate
        {
            set { _plandate = value; }
            get { return _plandate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CompleteDate
        {
            set { _completedate = value; }
            get { return _completedate; }
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
        /// 计划类型
        /// </summary>
        public string PlanType
        {
            get { return _istemporary ? "临时计划" : "年度计划"; }
        }
        public IList PlanDetailList
        {
            set
            {
                _plandetaillist = value;
            }
            get { return _plandetaillist; }
        }
        /// <summary>
        /// 当前计划的状态文字描述
        /// </summary>
        public string StatusString
        {
            get
            {
                string statusString = "";
                switch (_status)
                {
                    case DailyPatrolPlanStatus.Draft:
                        statusString = "草稿";
                        break;
                    case DailyPatrolPlanStatus.Waiting4ApprovalResult:
                        statusString = "等待审批结果";
                        break;
                    case DailyPatrolPlanStatus.ApprovalPassed:
                        statusString = "正式执行";
                        break;
                    case DailyPatrolPlanStatus.ApprovalFailed:
                        statusString = "审批不通过";
                        break;
                    case DailyPatrolPlanStatus.PlanComplete:
                        statusString = "执行完毕";
                        break;
                    default:
                        statusString = "未知状态";
                        break;
                }
                return statusString;
            }
        }
        public string ApprovalTime1
        {
            get
            {
                return _approvaldate == _plandate ? "" : _approvaldate.ToString();
            }
        }
    }
}
