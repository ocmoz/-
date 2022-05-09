using System;
using System. Collections.Generic;
using System. Text;
using System.Collections;

namespace FM2E.Model.Schedule
{
    public class ScheduleInfo
    {
        private long _planId;
        private string _projectName;
        private string _content;
        private string _contractNo;
        private DateTime _paymentTime;
        private decimal _amount;
        private string _remark;
        private decimal _sumAmount;

        public long PlanId
        {
            get { return _planId; }
            set { _planId = value; }
        }
        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
        }
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        public string ContractNo
        {
            get { return _contractNo; }
            set { _contractNo = value; }
        }
        public DateTime PaymentTime
        {
            get { return _paymentTime; }
            set { _paymentTime = value; }
        }
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        public decimal SumAmount
        {
            get { return _sumAmount; }
            set { _sumAmount = value; }
        }

        private IList _schedulelist = new List<ScheduleInfo>();
        public IList Schedulelist
        {
            get { return _schedulelist; }
            set { _schedulelist = value; }
        }
    }
}
