using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Budget
{
    public class BudgetPermonthInfo
    {
        #region Model
        private long _budgetpermonthid;
        private string _remarks;
        private string _manager;
        private DateTime _datetime;
        private decimal _expenditure;
        private decimal _allocation;
        private decimal _deviation;
        private string _reasonfordeviation;
        private string _evaluationfordeviation;
        private short _review;
        private long _totalid;
        private long _budgetperyeardetailid;
        private string _companyid;
        private int _month;
        private decimal _totalexpenditure;
        private decimal _budgetpermonth;
        private decimal _surplusexpenditure;
        private decimal _nonpayment;
        private decimal _total;
        private long _SubId;
        private decimal _BudgetApply;

        public decimal BudgetApply
        {
            set { _BudgetApply = value; }
            get { return _BudgetApply; }
        }

        public long SubID
        {
            set { _SubId = value; }
            get { return _SubId; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long BudgetPermonthID
        {
            set { _budgetpermonthid = value; }
            get { return _budgetpermonthid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Manager
        {
            set { _manager = value; }
            get { return _manager; }
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
        public string ReasonForDeviation
        {
            set { _reasonfordeviation = value; }
            get { return _reasonfordeviation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EvaluationForDeviation
        {
            set { _evaluationfordeviation = value; }
            get { return _evaluationfordeviation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public short Review
        {
            set { _review = value; }
            get { return _review; }
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
        public long BudgetYearDetailID
        {
            set { _budgetperyeardetailid = value; }
            get { return _budgetperyeardetailid; }
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
        public int Month
        {
            set { _month = value; }
            get { return _month; }
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
        #endregion Model
    }
}
