using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Budget
{
    public class BudgetPerMonthTotalInfo
    {
        #region Model
        private long _totalid;
        private decimal _totalexpenditure;
        private decimal _budgetpermonth;
        private decimal _surplusexpenditure;
        private decimal _nonpayment;
        private decimal _total;
        private DateTime _datetime;
        private decimal _expenditure;
        private decimal _allocation;
        private decimal _deviation;
        private int _year;
        private int _month;
        private string _companyid;
        private string _viceengineerreview;
        private string _vicemanagerreview;
        private string _managerreview;
        private string _financereview;
        private bool _result;
        private decimal _BudgetApply;
        private short _status;
        private long _BudgetYearID;
        private IList _DetailList;
        private IList _BudgetDetailList;
        private bool _updatedetail;
        private bool _updatebudgetdetail;
        private string _Approvaler;
        private string _Title;

        public string Title
        {
            set { _Title = value; }
            get { return _Title; }
        }

        public string Approvaler
        {
            set { _Approvaler = value; }
            get { return _Approvaler; }
        }



        public bool UpdateDetail
        {
            set { _updatedetail = value; }
            get { return _updatedetail; }
        }

        public bool UpdateBudgetDetail
        {
            set { _updatebudgetdetail = value; }
            get { return _updatebudgetdetail; }
        }

        public IList BudgetDetailList
        {
            set { _BudgetDetailList = value; }
            get { return _BudgetDetailList; }
        }


        public string StatusShow
        {
            get
            {
                switch (_status)
                {
                    case 1: return "已填写";
                    default: return "未填写";
                }
            }
        }


        public IList DetailList
        {
            set { _DetailList = value; }
            get { return _DetailList; }
        }

        public long BudgetYearID
        {
            set { _BudgetYearID = value; }
            get { return _BudgetYearID; }
        }

        public short Status
        {
            set { _status = value; }
            get { return _status; }
        }

        public decimal BudgetApply
        {
            set { _BudgetApply = value; }
            get { return _BudgetApply; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long TotalID
        {
            set { _totalid = value; }
            get { return _totalid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal TotalExpenditure
        {
            set { _totalexpenditure = value; }
            get { return _totalexpenditure; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal BudgetPermonth
        {
            set { _budgetpermonth = value; }
            get { return _budgetpermonth; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SurplusExpenditure
        {
            set { _surplusexpenditure = value; }
            get { return _surplusexpenditure; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal NonPayment
        {
            set { _nonpayment = value; }
            get { return _nonpayment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Total
        {
            set { _total = value; }
            get { return _total; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime MakeTime
        {
            set { _datetime = value; }
            get { return _datetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Expenditure
        {
            set { _expenditure = value; }
            get { return _expenditure; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Allocation
        {
            set { _allocation = value; }
            get { return _allocation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Deviation
        {
            set { _deviation = value; }
            get { return _deviation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Year
        {
            set { _year = value; }
            get { return _year; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Month
        {
            set { _month = value; }
            get { return _month; }
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
        public string ViceEngineerReview
        {
            set { _viceengineerreview = value; }
            get { return _viceengineerreview; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ViceManagerReview
        {
            set { _vicemanagerreview = value; }
            get { return _vicemanagerreview; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ManagerReview
        {
            set { _managerreview = value; }
            get { return _managerreview; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FinanceReview
        {
            set { _financereview = value; }
            get { return _financereview; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Result
        {
            set { _result = value; }
            get { return _result; }
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

        private List<string> _workflowstatus = new List<string>();

        public List<string> WorkFlowStatus
        {
            get { return _workflowstatus; }
            set { _workflowstatus = value; }
        }

        private string _workflowusername;


        public string WorkFlowUserName
        {
            get { return _workflowusername; }
            set { _workflowusername = value; }
        }

        #endregion Model
    }
}
