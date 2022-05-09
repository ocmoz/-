using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.SpecialProject
{
    /// <summary>
    /// 专项工程状态枚举类型
    /// </summary>
    public enum SpecialProjectStatus
    {
        /// <summary>
        /// 原始状态，无
        /// </summary>
        NONE,
        /// <summary>
        /// 草稿，而已删除、修改
        /// </summary>
        DRAFT,
        /// <summary>
        /// 公司主管部门预算审查
        /// </summary>
        BUDGETCHECK,
        /// <summary>
        /// 路桥预算批复
        /// </summary>
        BUDGETAPPORVAL ,
        /// <summary>
        /// 正式立项
        /// </summary>
        PROJECTSETUP ,
        /// <summary>
        /// 公司主管部门工程立项审批
        /// </summary>
        COMPANYAPPOVAL ,
        /// <summary>
        /// 路桥公司审批
        /// </summary>
        MOTHERCOMPANYAPPROVAL ,
        /// <summary>
        /// 集团审批
        /// </summary>
        GROUPAPPROVAL,
        /// <summary>
        /// 设计
        /// </summary>
        DESIGNINPUT ,
        /// <summary>
        /// 设计审批
        /// </summary>
        DESIGNCHECK ,
        /// <summary>
        /// 价格审查
        /// </summary>
        COSTCHECK,
        /// <summary>
        /// 招标
        /// </summary>
        BIDDING ,
        /// <summary>
        /// 定标审查
        /// </summary>
        BIDCHECK ,
        /// <summary>
        /// 等待开工通知
        /// </summary>
        WAIT4START,
        /// <summary>
        /// 施工中
        /// </summary>
        WORKING ,
        /// <summary>
        /// 完工
        /// </summary>
        FINISH ,
        /// <summary>
        /// 交工
        /// </summary>
        PASS ,
        /// <summary>
        /// 竣工
        /// </summary>
        COMPLETE ,
        /// <summary>
        /// 全部完成
        /// </summary>
        ALLFINISH ,
        /// <summary>
        /// 终止
        /// </summary>
        TERMINATED
    }

    /// <summary>
    /// 专项工程事件
    /// </summary>
    public enum SpecialProjectEvents
    {
        /// <summary>
        /// 保存草稿
        /// </summary>
        SAVE_DRAFT,
        /// <summary>
        /// 预算审批提交
        /// </summary>
        SUBMIT,
        /// <summary>
        /// 审批通过
        /// </summary>
        PASS,
        /// <summary>
        /// 审批不通过
        /// </summary>
        FAILED,
        /// <summary>
        /// 开工
        /// </summary>
        START,
        /// <summary>
        /// 下一流程
        /// </summary>
        NETX,
        /// <summary>
        /// 全部完成
        /// </summary>
        ALL_FINISH,
        /// <summary>
        /// 终止
        /// </summary>
        TERMINATED
    }

    /// <summary>
    /// 专项工程实体类
    /// </summary>
    public class SpecialProjectInfo
    {

        public SpecialProjectInfo()
        {
        }

        #region Model
        private long _projectid = 0;
        private string _solution = "";
        private string _solutionfile = "";
        private SpecialProjectStatus _status;//
        private string _companyid = "";
        private string _companyname = "";
        private string _projectname = "";
        private string _budgetname = "";
        private decimal _budget = 0;
        private string _currentstatus= "";
        private string _currentstatusfile = "";
        private string _problem = "";
        private string _problemfile = "";
        private string _submitter = "";
        private DateTime _submittime = DateTime.MinValue;
        private DateTime _updatetime = DateTime.MinValue;
        private string _workflowstatus = "";
        private string _workflowstatusname = "";

        /// <summary>
        /// 工作流状态
        /// </summary>
        public string WorkFlowStatus
        {
            set { _workflowstatus = value; }
            get { return _workflowstatus; }
        }

        /// <summary>
        /// 工作流状态名称
        /// </summary>
        public string WorkFlowStatusName
        {
            set { _workflowstatusname = value; }
            get { return _workflowstatusname; }
        }

        /// <summary>
        /// 审批的下一状态
        /// </summary>
        public SpecialProjectStatus NextStatus(SpecialProjectEvents e,out string tip)
        {
            tip = "";
            SpecialProjectStatus s = _status;
            switch(_status)
            {
                #region 原始状态
                case SpecialProjectStatus.NONE://最原始状态，2个事件，保存草稿、提交审批
                    switch(e)
                    {
                        case SpecialProjectEvents.SAVE_DRAFT: //保存草稿，下一状态是草稿
                            s = SpecialProjectStatus.DRAFT;
                            break;
                        case SpecialProjectEvents.SUBMIT://提交，需要根据预算的金额进行
                            if(_budget<50)
                            {
                                s = SpecialProjectStatus.BUDGETCHECK;
                            }
                            else
                            {
                                if(_budget<=500)
                                {
                                    s = SpecialProjectStatus.BUDGETCHECK;
                                }
                                else
                                {
                                    s = SpecialProjectStatus.BUDGETCHECK;
                                }
                            }
                            break;
                        default: 
                            tip="";
                            throw new Exception("在工程"+ _projectid + _projectname +"执行不允许的操作:" + e + "@" + _status);
                            
                    }
                    break;
                #endregion

                #region 草稿状态
                case SpecialProjectStatus.DRAFT://草稿状态，2个事件，保存草稿、提交审批
                    switch (e)
                    {
                        case SpecialProjectEvents.SAVE_DRAFT: //保存草稿，下一状态是草稿
                            s = SpecialProjectStatus.DRAFT;
                            break;
                        case SpecialProjectEvents.SUBMIT://提交，需要根据预算的金额进行
                            if (_budget < 50)
                            {
                                s = SpecialProjectStatus.BUDGETCHECK;
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    s = SpecialProjectStatus.BUDGETCHECK;
                                }
                                else
                                {
                                    s = SpecialProjectStatus.BUDGETCHECK;
                                }
                            }
                            break;
                        default:
                            tip = "";
                            throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            
                    }
                    break;
#endregion

                #region 公司工程预算审查
                case SpecialProjectStatus.BUDGETCHECK://公司工程预算审查，2个时间，通过、不通过
                    switch(e)
                    {
                        case SpecialProjectEvents.PASS://根据金额跳到下一个状态
                            if(_budget<50){
                                //s = SpecialProjectStatus.BUDGETAPPORVAL;//路桥预算批复
                                s = SpecialProjectStatus.COMPANYAPPOVAL;//公司主管部门立项审批
                            }
                            else
                            {
                                if(_budget<=500)
                                {
                                    //s = SpecialProjectStatus.BUDGETAPPORVAL;
                                    s = SpecialProjectStatus.COMPANYAPPOVAL;//公司主管部门立项审批
                                }
                                else
                                {
                                    //s = SpecialProjectStatus.BUDGETAPPORVAL;
                                    s = SpecialProjectStatus.COMPANYAPPOVAL;//公司主管部门立项审批
                                }
                            }
                            break;
                        case SpecialProjectEvents.FAILED://失败则直接返回
                            s = SpecialProjectStatus.DRAFT;
                            break;
                        default :
                            tip = "";
                            throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                           
                    }
                    break;
                #endregion

                #region 路桥公司预算批复
                case SpecialProjectStatus.BUDGETAPPORVAL://路桥公司预算批复
                    switch(e)
                    {
                        case SpecialProjectEvents.PASS://根据金额跳到下一个状态
                            if(_budget<50)
                            {
                                s = SpecialProjectStatus.DESIGNINPUT;//正式立项
                            }
                            else
                            {
                                if(_budget<=500)
                                {
                                    s = SpecialProjectStatus.PROJECTSETUP;
                                }
                                else
                                {
                                    s = SpecialProjectStatus.PROJECTSETUP;
                                }
                            }
                            break;
                        case SpecialProjectEvents.FAILED://失败则直接返回
                            if(_budget<50)
                            {
                                s = SpecialProjectStatus.BUDGETCHECK;
                            }
                            else
                            {
                                if(_budget<=500)
                                {
                                    s = SpecialProjectStatus.BUDGETCHECK;
                                }
                                else
                                {
                                    s = SpecialProjectStatus.BUDGETCHECK;
                                }
                            }
                            break;
                        default:
                            throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            
                    }
                    break;
                #endregion 

                #region 立项申请
                case SpecialProjectStatus.PROJECTSETUP:
                    switch(e)
                    {
                        case SpecialProjectEvents.SUBMIT:
                            if (_budget < 50)
                            {
                                s = SpecialProjectStatus.COMPANYAPPOVAL;
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    s = SpecialProjectStatus.COMPANYAPPOVAL;
                                }
                                else
                                {
                                    s = SpecialProjectStatus.COMPANYAPPOVAL;
                                }
                            }
                            break;
                        default:
                            throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            
                    }
                    break;
                #endregion

                #region 公司专项审批
                case SpecialProjectStatus.COMPANYAPPOVAL:
                    switch (e)
                    {
                        case SpecialProjectEvents.PASS:
                            if (_budget < 50)
                            {
                                //s = SpecialProjectStatus.BIDDING;
                                s = SpecialProjectStatus.DESIGNINPUT;//设计方案
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    s = SpecialProjectStatus.DESIGNINPUT;
                                }
                                else
                                {
                                    //s = SpecialProjectStatus.MOTHERCOMPANYAPPROVAL;
                                    s = SpecialProjectStatus.DESIGNINPUT;
                                }
                            }
                            break;
                        case SpecialProjectEvents.FAILED:
                            if (_budget < 50)
                            {
                                //s = SpecialProjectStatus.PROJECTSETUP;
                                s = SpecialProjectStatus.BUDGETCHECK;//预算审查
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    //s = SpecialProjectStatus.DRAFT;
                                    s = SpecialProjectStatus.BUDGETCHECK;
                                }
                                else
                                {
                                    //s = SpecialProjectStatus.DRAFT;
                                    s = SpecialProjectStatus.BUDGETCHECK;
                                }
                            }
                            break;
                        default:
                            throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            
                    }
                    break;
                #endregion

                #region 路桥专项审批
                case SpecialProjectStatus.MOTHERCOMPANYAPPROVAL :
                    switch (e)
                    {
                        case SpecialProjectEvents.PASS:
                            if (_budget < 50)
                            {
                                throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                                }
                                else
                                {
                                    s = SpecialProjectStatus.GROUPAPPROVAL;
                                }
                            }
                            break;
                        case SpecialProjectEvents.FAILED:
                            if (_budget < 50)
                            {
                                throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                                }
                                else
                                {
                                    s = SpecialProjectStatus.COMPANYAPPOVAL;
                                }
                            }
                            break;
                        default:
                            throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            
                    }
                    break;
                #endregion

                #region 交通集团专项审批
                case SpecialProjectStatus.GROUPAPPROVAL:
                    switch (e)
                    {
                        case SpecialProjectEvents.PASS:
                            if (_budget < 50)
                            {
                                throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                                }
                                else
                                {
                                    s = SpecialProjectStatus.DESIGNINPUT;
                                }
                            }
                            break;
                        case SpecialProjectEvents.FAILED:
                            if (_budget < 50)
                            {
                                throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                                }
                                else
                                {
                                    s = SpecialProjectStatus.MOTHERCOMPANYAPPROVAL;
                                }
                            }
                            break;
                        default:
                            throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            
                    }
                    break;
                #endregion

                #region 设计
                case SpecialProjectStatus.DESIGNINPUT:
                    switch (e)
                    {
                        case SpecialProjectEvents.SUBMIT:
                            if (_budget < 50)
                            {
                                //s = SpecialProjectStatus.PROJECTSETUP;
                                s = SpecialProjectStatus.BIDDING;
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    //s = SpecialProjectStatus.DESIGNCHECK;
                                    s = SpecialProjectStatus.BIDDING;
                                }
                                else
                                {
                                    //s = SpecialProjectStatus.DESIGNCHECK;
                                    s = SpecialProjectStatus.BIDDING;
                                }
                            }
                            break;
                        
                        default:
                            throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            
                    }
                    break;
                #endregion

                #region 设计审查
                case SpecialProjectStatus.DESIGNCHECK:
                    switch (e)
                    {
                        case SpecialProjectEvents.PASS:
                            if (_budget < 50)
                            {
                                throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    s = SpecialProjectStatus.COSTCHECK;
                                }
                                else
                                {
                                    s = SpecialProjectStatus.COSTCHECK;
                                }
                            }
                            break;
                        case SpecialProjectEvents.FAILED:
                            if (_budget < 50)
                            {
                                throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    s = SpecialProjectStatus.DESIGNINPUT;
                                }
                                else
                                {
                                    s = SpecialProjectStatus.DESIGNINPUT;
                                }
                            }
                            break;
                        default:
                            throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            
                    }
                    break;
                #endregion

                #region 设计预算审查
                case SpecialProjectStatus.COSTCHECK:
                    switch (e)
                    {
                        case SpecialProjectEvents.PASS:
                            if (_budget < 50)
                            {
                                throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    s = SpecialProjectStatus.BIDDING;
                                }
                                else
                                {
                                    s = SpecialProjectStatus.BIDDING;
                                }
                            }
                            break;
                        case SpecialProjectEvents.FAILED:
                            if (_budget < 50)
                            {
                                throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    s = SpecialProjectStatus.DESIGNCHECK;
                                }
                                else
                                {
                                    s = SpecialProjectStatus.DESIGNCHECK;
                                }
                            }
                            break;

                        default:
                            throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            
                    }
                    break;
                #endregion

                #region 工程招标
                case SpecialProjectStatus.BIDDING:
                    switch (e)
                    {
                        case SpecialProjectEvents.SUBMIT:
                            if (_budget < 50)
                            {
                                s = SpecialProjectStatus.WAIT4START;
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    //s = SpecialProjectStatus.BIDCHECK;
                                    s = SpecialProjectStatus.WAIT4START;
                                }
                                else
                                {
                                    //s = SpecialProjectStatus.BIDCHECK;
                                    s = SpecialProjectStatus.WAIT4START;
                                }
                            }
                            break;
                       

                        default:
                            throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            
                    }
                    break;
                #endregion

                #region 定标审查
                case SpecialProjectStatus.BIDCHECK:
                    switch (e)
                    {
                        case SpecialProjectEvents.PASS:
                            if (_budget < 50)
                            {
                                throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    s = SpecialProjectStatus.WAIT4START;
                                }
                                else
                                {
                                    s = SpecialProjectStatus.WAIT4START;
                                }
                            }
                            break;
                        case SpecialProjectEvents.FAILED:
                            if (_budget < 50)
                            {
                                throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    s = SpecialProjectStatus.BIDDING;
                                }
                                else
                                {
                                    s = SpecialProjectStatus.BIDDING;
                                }
                            }
                            break;

                        default:
                            throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            
                    }
                    break;
                #endregion

                #region 等待开工
                case SpecialProjectStatus.WAIT4START:
                    switch (e)
                    {
                        case SpecialProjectEvents.START:
                            if (_budget < 50)
                            {
                                s = SpecialProjectStatus.WORKING;
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    s = SpecialProjectStatus.WORKING;
                                }
                                else
                                {
                                    s = SpecialProjectStatus.WORKING;
                                }
                            }
                            break;
                        
                        default:
                            throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            
                    }
                    break;
                #endregion

                #region 施工中
                case SpecialProjectStatus.WORKING:
                    switch (e)
                    {
                        case SpecialProjectEvents.NETX:
                            if (_budget < 50)
                            {
                                s = SpecialProjectStatus.FINISH;
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    s = SpecialProjectStatus.FINISH;
                                }
                                else
                                {
                                    s = SpecialProjectStatus.FINISH;
                                }
                            }
                            break;

                        default:
                            throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            
                    }
                    break;
                #endregion

                #region 完工
                case SpecialProjectStatus.FINISH:
                    switch (e)
                    {
                        case SpecialProjectEvents.NETX:
                            if (_budget < 50)
                            {
                                s = SpecialProjectStatus.PASS;
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    s = SpecialProjectStatus.PASS;
                                }
                                else
                                {
                                    s = SpecialProjectStatus.PASS;
                                }
                            }
                            break;

                        default:
                            throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                            
                    }
                    break;
                #endregion

                #region 交工
                case SpecialProjectStatus.PASS:
                    switch (e)
                    {
                        case SpecialProjectEvents.NETX:
                            if (_budget < 50)
                            {
                                s = SpecialProjectStatus.COMPLETE;
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    s = SpecialProjectStatus.COMPLETE;
                                }
                                else
                                {
                                    s = SpecialProjectStatus.COMPLETE;
                                }
                            }
                            break;

                        default:
                            throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                           
                    }
                    break;
                #endregion

                #region 竣工
                case SpecialProjectStatus.COMPLETE:
                    switch (e)
                    {
                        case SpecialProjectEvents.NETX:
                            if (_budget < 50)
                            {
                                s = SpecialProjectStatus.ALLFINISH;
                            }
                            else
                            {
                                if (_budget <= 500)
                                {
                                    s = SpecialProjectStatus.ALLFINISH;
                                }
                                else
                                {
                                    s = SpecialProjectStatus.ALLFINISH;
                                }
                            }
                            break;

                        default:
                            throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                           
                    }
                    break;
                #endregion

                default:
                    throw new Exception("在工程" + _projectid + _projectname + "执行不允许的操作:" + e + "@" + _status);
                    
            }
            string st = "";
            switch (e)
            {
                case SpecialProjectEvents.SUBMIT:
                    st = "提交成功，";
                    break;
                case SpecialProjectEvents.START:
                    st = "启动成功，";
                    break;
                case SpecialProjectEvents.SAVE_DRAFT:
                    st = "保存成功，";
                    break;
                case SpecialProjectEvents.PASS:
                    st = "审批通过，";
                    break;
                case SpecialProjectEvents.NETX:
                    st = "确认通过，";
                    break;
                case SpecialProjectEvents.FAILED:
                    st = "审批不通过，";
                    break;
                default:
                    break;

            }
            tip = st + "工程新状态为：" + GetStatusString(s);
            return s;
        }

        /// <summary>
        /// 立项提交人
        /// </summary>
        public string Submitter
        {
            set { _submitter = value; }
            get { return _submitter; }
        }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime SubmitTime
        {
            get
            {
                return _submittime;
            }
            set
            {
                _submittime = value;
            }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            get
            {
                return _updatetime;
            }
            set
            {
                _updatetime = value;
            }
        }

        /// <summary>
        /// 专项工程ID
        /// </summary>
        public long ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 解决方案描述
        /// </summary>
        public string Solution
        {
            set { _solution = value; }
            get { return _solution; }
        }
        /// <summary>
        /// 解决方案附件
        /// </summary>
        public string SolutionFile
        {
            set { _solutionfile = value; }
            get { return _solutionfile; }
        }
        /// <summary>
        /// 工程状态
        /// </summary>
        public SpecialProjectStatus Status
        {
            set { _status = value; }
            get { return _status; }
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
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            get
            {
                return _companyname;
            }
            set
            {
                _companyname = value;
            }
        }
        /// <summary>
        /// 工程名称
        /// </summary>
        public string ProjectName
        {
            set { _projectname = value; }
            get { return _projectname; }
        }
        /// <summary>
        /// 使用的预算项名称
        /// </summary>
        public string BudgetName
        {
            set { _budgetname = value; }
            get { return _budgetname; }
        }
        /// <summary>
        /// 预算金额
        /// </summary>
        public decimal Budget
        {
            set { _budget = value; }
            get { return _budget; }
        }
        /// <summary>
        /// 现状描述
        /// </summary>
        public string CurrentStatus
        {
            set { _currentstatus = value; }
            get { return _currentstatus; }
        }
        /// <summary>
        /// 现状附件
        /// </summary>
        public string CurrentStatusFile
        {
            set { _currentstatusfile = value; }
            get { return _currentstatusfile; }
        }
        /// <summary>
        /// 存在的问题描述
        /// </summary>
        public string Problem
        {
            set { _problem = value; }
            get { return _problem; }
        }
        /// <summary>
        /// 存在的问题附件
        /// </summary>
        public string ProblemFile
        {
            set { _problemfile = value; }
            get { return _problemfile; }
        }
        #endregion Model


        #region 包含的其他详情
        IList _jobitems = new List<SpecialProjectJobItemInfo>();
        /// <summary>
        /// 工程量清单
        /// </summary>
        public IList JobItems
        {
            get { return _jobitems; }
            set
            {
                _jobitems = value;
                //更改的时候，重新设置预算的直接费
                if (_budgetitems != null)
                {
                    foreach (SpecialProjectBudgetItemInfo budget in _budgetitems)
                    {
                        budget.DirectAmount = DirectBugdet;
                    }
                }
            }
        }

        IList _budgetitems = new List<SpecialProjectBudgetItemInfo>();
        /// <summary>
        /// 预算清单
        /// </summary>
        public IList BudgetItems
        {
            get { return _budgetitems; }
            set
            {
                //设置的时候顺便设置直接费金额
                _budgetitems = value;
                foreach (SpecialProjectBudgetItemInfo budget in _budgetitems)
                {
                    budget.DirectAmount = DirectBugdet;
                }
            }
        }

        IList _approvalrecords = new List<SpecialProjectApprovalInfo>();
        /// <summary>
        /// 审批记录列表
        /// </summary>
        public IList ApprovalRecords
        {
            get { return _approvalrecords; }
            set { _approvalrecords = value; }
        }

        SpecialProjectDesignInfo _design;
        /// <summary>
        /// 设计方案
        /// </summary>
        public SpecialProjectDesignInfo Design
        {
            get { return _design; }
            set { _design = value; }
        }

        SpecialProjectBidInfo _bid;
        /// <summary>
        /// 招标信息
        /// </summary>
        public SpecialProjectBidInfo Bid
        {
            get { return _bid; }
            set { _bid = value; }
        }

        IList _planitems = new List<SpecialProjectPlanInfo>();
        /// <summary>
        /// 工作计划列表
        /// </summary>
        public IList PlanItems
        {
            get { return _planitems; }
            set { _planitems = value; }
        }

        IList _deviceitems = new List<SpecialProjectDeviceInfo>();
        /// <summary>
        /// 进场设备列表
        /// </summary>
        public IList DeviceList
        {
            get { return _deviceitems; }
            set { _deviceitems = value; }
        }


        IList _prepayitems = new List<SpecialProjectPrePayInfo>();
        /// <summary>
        /// 预支付项列表
        /// </summary>
        public IList PrePayList
        {
            get { return _prepayitems; }
            set { _prepayitems = value; }
        }

        IList _payrecordlist = new List<SpecialProjectPayRecordInfo>();
        /// <summary>
        /// 进度支付项列表
        /// </summary>
        public IList MonthlyPayRecordList
        {
            get { return _payrecordlist; }
            set { _payrecordlist = value; }
        }

        IList _contractpaylist = new List<SpecialProjectContractPayInfo>();
        /// <summary>
        /// 合同支付项列表
        /// </summary>
        public IList ContractPayList
        {
            get { return _contractpaylist; }
            set { _contractpaylist = value; }
        }

        IList _checkrecordlist = new List<SpecialProjectCheckRecordInfo>();
        /// <summary>
        /// 施工进度检查列表
        /// </summary>
        public IList CheckRecordList
        {
            get { return _checkrecordlist; }
            set { _checkrecordlist = value; }
        }

        IList _modifylist = new List<SpecialProjectModifyInfo>();
        /// <summary>
        /// 工程变更列表
        /// </summary>
        public IList ModifyList
        {
            get { return _modifylist; }
            set { _modifylist = value; }
        }


        SpecialProjectCheckAcceptInfo _checkaccept;
        /// <summary>
        /// 验收信息
        /// </summary>
        public SpecialProjectCheckAcceptInfo CheckAcceptInfo
        {
            get { return _checkaccept; }
            set { _checkaccept = value; }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private static string GetStatusString(SpecialProjectStatus status)
        {
            string str = "未知状态";
            switch (status)
            {
                case SpecialProjectStatus.DRAFT:
                    str = "草稿";
                    break;
                case SpecialProjectStatus.ALLFINISH:
                    str = "全部完成";
                    break;
                case SpecialProjectStatus.BIDCHECK:
                    str = "定标审查中";
                    break;
                case SpecialProjectStatus.BIDDING:
                    str = "招标中";
                    break;
                case SpecialProjectStatus.BUDGETAPPORVAL:
                    str = "路桥预算批复中";
                    break;
                case SpecialProjectStatus.BUDGETCHECK:
                    str = "主管部门预算审查中";
                    break;
                case SpecialProjectStatus.COMPANYAPPOVAL:
                    str = "主管部门领导工程计划审查中";
                    break;
                case SpecialProjectStatus.FINISH:
                    str = "已完工";
                    break;
                case SpecialProjectStatus.COSTCHECK:
                    str = "造价站预算审查";
                    break;
                case SpecialProjectStatus.DESIGNCHECK:
                    str = "设计审查";
                    break;
                case SpecialProjectStatus.DESIGNINPUT:
                    str = "设计中";
                    break;
                case SpecialProjectStatus.COMPLETE:
                    str = "已竣工";
                    break;
                case SpecialProjectStatus.GROUPAPPROVAL:
                    str = "集团专项批复中";
                    break;
                case SpecialProjectStatus.MOTHERCOMPANYAPPROVAL:
                    str = "路桥公司审批计划中";
                    break;
                case SpecialProjectStatus.NONE:
                    str = "未知状态";
                    break;
                case SpecialProjectStatus.PASS:
                    str = "已交工";
                    break;
                case SpecialProjectStatus.PROJECTSETUP:
                    str = "正在提请立项申请";
                    break;
                case SpecialProjectStatus.TERMINATED:
                    str = "工程终止";
                    break;
                case SpecialProjectStatus.WORKING:
                    str = "施工中";
                    break;
                case SpecialProjectStatus.WAIT4START:
                    str = "等待开工";
                    break;
                default:
                    break;
            }
            return str;
        }

        /// <summary>
        /// 状态字符串
        /// </summary>
        public string StatusString
        {
            get
            {
                return GetStatusString(_status);
            }
        }

        /// <summary>
        /// 直接费，从工程量金额总数算出
        /// </summary>
        public decimal DirectBugdet
        {
            get
            {
                decimal amount = 0;
                if (_jobitems != null)
                {
                    foreach (SpecialProjectJobItemInfo job in _jobitems)
                    {
                        amount += job.Amount;
                    }
                }
                return amount;
            }
        }

        /// <summary>
        /// 总预算，从预算项中计算得出
        /// </summary>
        public decimal TotalBudget
        {
            get
            {
                decimal amount = 0;
                foreach (SpecialProjectBudgetItemInfo budget in _budgetitems)
                {
                    amount += budget.TrueAmount;
                }
                return amount;
            }
        }

        /// <summary>
        /// 总预算与直接费的倍数
        /// </summary>
        public decimal TotalDirectMultiple
        {
            get
            {
                if (DirectBugdet != 0)
                    return TotalBudget / DirectBugdet;
                else
                    return 0;
            }
        }

        /// <summary>
        /// 计划预支付总数
        /// </summary>
        public decimal TotalPlanPrePay
        {
            get
            {
                decimal total = 0;
                foreach (SpecialProjectPrePayInfo item in _prepayitems)
                {
                    total += item.Amount;
                }
                return total;
            }
        }

        /// <summary>
        /// 已经支付的预支付项总数
        /// </summary>
        public decimal TotalPaidPrePay
        {
            get
            {
                decimal total = 0;
                foreach (SpecialProjectPrePayInfo item in _prepayitems)
                {
                    total += item.Paid;
                }
                return total;
            }
        }

        /// <summary>
        /// 计划支付的合同支付项总数
        /// </summary>
        public decimal TotalPlanContractPay
        {
            get
            {
                decimal total = 0;
                foreach (SpecialProjectContractPayInfo item in _contractpaylist)
                {
                    total += item.Amount;
                }
                return total;
            }
        }

        /// <summary>
        /// 计划支付的合同支付项总数
        /// </summary>
        public decimal TotalPaidContractPay
        {
            get
            {
                decimal total = 0;
                foreach (SpecialProjectContractPayInfo item in _contractpaylist)
                {
                    total += item.Paid;
                }
                return total;
            }
        }

        /// <summary>
        /// 已经支付的进度支付项
        /// </summary>
        public decimal TotalPlanMonthlyPay
        {
            get
            {
                decimal total = 0;
                foreach (SpecialProjectPayRecordInfo item in _payrecordlist)
                {
                    total += item.Amount;
                }
                return total;
            }
        }

        /// <summary>
        /// 已经支付的进度支付项
        /// </summary>
        public decimal TotalMonthlyPay
        {
            get
            {
                decimal total = 0;
                foreach (SpecialProjectPayRecordInfo item in _payrecordlist)
                {
                    total += item.Paid;
                }
                return total;
            }
        }

        /// <summary>
        /// 总的计划支付金额
        /// </summary>
        public decimal TotalPlanPay
        {
            get { return TotalPlanContractPay + TotalPlanPrePay + TotalPlanMonthlyPay; }
        }

        /// <summary>
        /// 总的已经支付金额
        /// </summary>
        public decimal TotalPaid
        {
            get { return TotalMonthlyPay + TotalPaidContractPay + TotalPaidPrePay; }
        }

        /// <summary>
        /// 是否能够进入验收阶段，当所有工作项都100%完成才能进入验收
        /// </summary>
        public bool CanCheckAccept
        {
            get
            {
                foreach (SpecialProjectPlanInfo plan in _planitems)
                {
                    if (plan.Progress < 1)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// 生效的变更数量
        /// </summary>
        public int ModifyOKCount
        {
            get
            {
                int count = 0;
                foreach (SpecialProjectModifyInfo item in _modifylist)
                {
                    if (item.Status == SpecialProjectModifyStatus.OK)
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        /// <summary>
        /// 有变更生效后的JobItem
        /// </summary>
        public IList JobItemListAfterModify
        {
            get
            {
                List<SpecialProjectJobItemInfo> list = new List<SpecialProjectJobItemInfo>();
                Hashtable hs = new Hashtable(list.Count);
                foreach (SpecialProjectJobItemInfo job in _jobitems)
                {
                    SpecialProjectJobItemInfo newjob = new SpecialProjectJobItemInfo();
                   
                    newjob.Count = job.Count;
                    newjob.Equipment = job.Equipment;
                    newjob.ItemID = job.ItemID;
                    newjob.Model = job.Model;
                    newjob.ProjectID = job.ProjectID;
                    newjob.Remark = job.Remark;
                    newjob.Unit = job.Unit;
                    newjob.UnitPrice = job.UnitPrice;
                    list.Add(newjob);
                    hs.Add(newjob.Equipment + newjob.Model + newjob.Unit + newjob.UnitPrice.ToString("0.##"), newjob);
                }
                //每一个变更单
                foreach (SpecialProjectModifyInfo modify in _modifylist)
                {
                    if (modify.Status == SpecialProjectModifyStatus.OK)
                    {
                        foreach (SpecialProjectModifyDeviceInfo device in modify.DetailList)
                        {
                            if (hs.Contains(device.DeviceName + device.Model + device.Unit + device.UnitPrice.ToString("0.##")))
                            {
                                SpecialProjectJobItemInfo job = hs[device.DeviceName + device.Model + device.Unit + device.UnitPrice.ToString("0.##")] as SpecialProjectJobItemInfo;
                                job.Count += device.Count;
                            }
                            else
                            {
                                SpecialProjectJobItemInfo job = new SpecialProjectJobItemInfo();
                                job.Count = device.Count;
                                job.Equipment = device.DeviceName;
                                job.ItemID = 0;
                                job.Model = device.Model;
                                job.ProjectID = device.ProjectID;
                                job.Remark = device.Remark;
                                job.Unit = device.Unit;
                                job.UnitPrice = device.UnitPrice;
                                list.Add(job);
                                hs.Add(job.Equipment + job.Model + job.Unit + job.UnitPrice.ToString("0.##"), job);
                            }
                        }
                    }
                }
                return list;
            }
        }
    }
}
