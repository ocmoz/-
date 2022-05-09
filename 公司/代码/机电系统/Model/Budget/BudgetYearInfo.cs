using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Budget
{
    public class BudgetYearInfo
    {
        #region Model
        private long _budgetyearid;
        private string _attachment;
        private DateTime _updatetime;
        private decimal _budgetapply;
        private decimal _budgetapprove;
        private string _companyid;
        private int _year;
        private string _maker;
        private DateTime _maketime;
        private short _status;
        private string _approvaler;
        private bool _result;
        private string _remark;
        private IList _DetailList;
        private string _title;
        private string _workflowusername;


        public string WorkFlowUserName
        {
            get { return _workflowusername; }
            set { _workflowusername = value; }
        }

        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }

        public string StatusShow
        {
            get
            {
                switch (_status)
                {
                    case 1: return "草稿";
                    case 2: return "待审批";
                    case 3: return "审批结束";
                    default: return "";
                }
            }
        }

        public IList DetailList
        {
            set { _DetailList = value; }
            get { return _DetailList; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long BudgetYearID
        {
            set { _budgetyearid = value; }
            get { return _budgetyearid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
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
        public decimal BudgetApply
        {
            set { _budgetapply = value; }
            get { return _budgetapply; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal BudgetApprove
        {
            set { _budgetapprove = value; }
            get { return _budgetapprove; }
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
        public int Year
        {
            set { _year = value; }
            get { return _year; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Maker
        {
            set { _maker = value; }
            get { return _maker; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime MakeTime
        {
            set { _maketime = value; }
            get { return _maketime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public short Status
        {
            set { _status = value; }
            get { return _status; }
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
        public bool Result
        {
            set { _result = value; }
            get { return _result; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
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
        #endregion Model
    }
}
