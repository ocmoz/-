using System;
using System. Collections. Generic;
using System. Text;

namespace FM2E. Model. Workflow
{
    public class UserDelegateInfo
    {
        public String ChiefUserName
        {
            get;
            set;
        }
        public String DelegateUserName
        {
            get;
            set;
        }
        public string DelegateUserPersonName
        {
            get;
            set;
        }
        public long WorkflowRoleID
        {
            get;
            set;
        }
        public DateTime DelegateStartTime
        {
            get;
            set;
        }
        public DateTime DelegateEndTime
        {
            get;
            set;
        }
    }
}
