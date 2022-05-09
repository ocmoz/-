using System;
using System. Collections.Generic;
using System. Text;
using System.Collections;

namespace FM2E.Model.Plan
{
    public class PlanInfo
    {
        private int _id;
        private string _year;
        private string _month;
        private string _department;
        private string _producer;
        private DateTime _producerTime;
        private string _useReasonsDifferences;
        private string _incomeReasonsDifferences;
        private string _departmentManagerRemark;
        private string _departmentManager;
        private DateTime _departmentManagerTime;
        private string _accountingRemark;
        private string _accounting;
        private DateTime _accountingTime;
        private string _financialRemark;
        private string _financial;
        private DateTime _financialTime;
        private string _operatingRemark;
        private string _operating;
        private DateTime _operatingTime;
        private string _remark;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Year
        {
            get { return _year; }
            set { _year = value; }
        }
        public string Month
        {
            get { return _month; }
            set { _month = value; }
        }
        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }
        public string Producer
        {
            get { return _producer; }
            set { _producer = value; }
        }
        public DateTime ProducerTime
        {
            get { return _producerTime; }
            set { _producerTime = value; }
        }
              
        public string UseReasonsDifferences
        {
            get { return _useReasonsDifferences; }
            set { _useReasonsDifferences = value; }
        }
        public string IncomeReasonsDifferences
        {
            get { return _incomeReasonsDifferences; }
            set { _incomeReasonsDifferences = value; }
        }
        public string DepartmentManagerRemark
        {
            get { return _departmentManagerRemark; }
            set { _departmentManagerRemark = value; }
        }
        public string DepartmentManager
        {
            get { return _departmentManager; }
            set { _departmentManager = value; }
        }
        public DateTime DepartmentManagerTime
        {
            get { return _departmentManagerTime; }
            set { _departmentManagerTime = value; }
        }
        public string AccountingRemark
        {
            get { return _accountingRemark; }
            set { _accountingRemark = value; }
        }
        public string Accounting
        {
            get { return _accounting; }
            set { _accounting = value; }
        }
        public DateTime AccountingTime
        {
            get { return _accountingTime; }
            set { _accountingTime = value; }
        }
        public string FinancialRemark
        {
            get { return _financialRemark; }
            set { _financialRemark = value; }
        }
        public string Financial
        {
            get { return _financial; }
            set { _financial = value; }
        }
        public DateTime FinancialTime
        {
            get { return _financialTime; }
            set { _financialTime = value; }
        }
        public string OperatingRemark
        {
            get { return _operatingRemark; }
            set { _operatingRemark = value; }
        }
        public string Operating
        {
            get { return _operating; }
            set { _operating = value; }
        }
        public DateTime OperatingTime
        {
            get { return _operatingTime; }
            set { _operatingTime = value; }
        }
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        private decimal _monthUse;
        public decimal MonthUse
        {
            get { return _monthUse; }
            set { _monthUse = value; }
        }
        private decimal _monthIncome;
        public decimal MonthIncome
        {
            get { return _monthIncome; }
            set { _monthIncome = value; }
        }

        private decimal _lastMonthUse;
        public decimal LastMonthUse
        {
            get { return _lastMonthUse; }
            set { _lastMonthUse = value; }
        }
        private decimal _lastMonthIncome;
        public decimal LastMonthIncome
        {
            get { return _lastMonthIncome; }
            set { _lastMonthIncome = value; }
        }
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private decimal _sumSchedule;
        public decimal SumSchedule
        {
            get { return _sumSchedule; }
            set { _sumSchedule = value; }
        }
        private decimal _sumScheduleActual;
        public decimal SumScheduleActual
        {
            get { return _sumScheduleActual; }
            set { _sumScheduleActual = value; }
        }

        private decimal _sumScheduleDifferent;
        public decimal SumScheduleDifferent
        {
            get { return _sumScheduleDifferent; }
            set { _sumScheduleDifferent = value; }
        }
        private decimal _sumScheduleDifferentRatio;
        public decimal SumScheduleDifferentRatio
        {
            get { return _sumScheduleDifferentRatio; }
            set { _sumScheduleDifferentRatio = value; }
        }
        private decimal _sumScheduleIncome;
        public decimal SumScheduleIncome
        {
            get { return _sumScheduleIncome; }
            set { _sumScheduleIncome = value; }
        }
        private decimal _sumScheduleIncomeActual;
        public decimal SumScheduleIncomeActual
        {
            get { return _sumScheduleIncomeActual; }
            set { _sumScheduleIncomeActual = value; }
        }

        private decimal _sumScheduleIncomeDifferent;
        public decimal SumScheduleIncomeDifferent
        {
            get { return _sumScheduleIncomeDifferent; }
            set { _sumScheduleIncomeDifferent = value; }
        }
        private decimal _sumScheduleIncomeDifferentRatio;
          public decimal SumScheduleIncomeDifferentRatio
        {
            get { return _sumScheduleIncomeDifferentRatio; }
            set { _sumScheduleIncomeDifferentRatio = value; }
        }
         
    }
}
