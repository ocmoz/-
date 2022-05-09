using System;
using System. Collections.Generic;
using System. Text;

namespace FM2E.Model.MonthFundsUsePlan
{
    public class MonthFundsUsePlanInfo
    {
        private string _projectName;
        private decimal _projectMoney;

        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
        }
        public decimal ProjectMoney
        {
            get { return _projectMoney; }
            set { _projectMoney = value; }
        }
    }
}
