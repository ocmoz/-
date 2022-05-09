using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.SpecialProject
{
    /// <summary>
    /// 专项工程搜索参数实体
    /// </summary>
    public class SpecialProjectSearchInfo
    {
        public SpecialProjectSearchInfo()
        {
        }

        private string _companyid = "";

        private string _projectname ="";

        private decimal _budgetlower = decimal.MinValue;
        private decimal _budgetupper = decimal.MaxValue;

        private DateTime _timelower = DateTime.MinValue;
        private DateTime _timeupper = DateTime.MaxValue;

        private string _designcompany = "";

        private string _bidcompany = "";

        private SpecialProjectStatus[] _statusarray;
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyID
        {
            get { return _companyid; }
            set { _companyid = value; }
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get { return _projectname; }
            set { _projectname = value; }
        }
        /// <summary>
        /// 最小预算
        /// </summary>
        public decimal BudgetLower
        {
            get { return _budgetlower; }
            set { _budgetlower = value; }
        }
        /// <summary>
        /// 最大预算
        /// </summary>
        public decimal BudgetUpper
        {
            get { return _budgetupper; }
            set { _budgetupper = value; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime TimeLower
        {
            get { return _timelower; }
            set { _timelower = value; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime TimeUpper
        {
            get { return _timeupper; }
            set { _timeupper = value; }
        }
        /// <summary>
        /// 设计单位
        /// </summary>
        public string DesignCompany
        {
            get { return _designcompany; }
            set { _designcompany = value; }
        }
        /// <summary>
        /// 施工单位
        /// </summary>
        public string BidCompany
        {
            get { return _bidcompany; }
            set { _bidcompany = value; }
        }

        /// <summary>
        /// 状态数组
        /// </summary>
        public SpecialProjectStatus[] StatusArray
        {
            get { return _statusarray; }
            set { _statusarray = value; }
        }
    }
}
