using System;
using System.Collections.Generic;
using System.Workflow .Activities;
using System.ComponentModel;

namespace FM2E.WorkflowLayer
{
    public class DemoWorkflow: StateMachineWorkflowActivity, IParallelEventSupport, IEnumItems
    {
        //(1) Add your SQLServerDAL object here, e.g: DailyClean _dailCleanDAL = new DailyClean();
        public Decimal GetAmount( long dataID )
        {
            return 0;//(2) Use the function in your SQLServerDAL object to return the value of Amount in your database
        }
        public int GetAppOption( long dataID )
        {
            return 0;//(3) Use the function in your SQLServerDAL object to return the value of AppOption in your database
        }

        public bool CanEnumItemDeleted( String enumName, int oldValue )
        {
            //(4) Add your own code below to estimate whether the enumeration item with oldValue can be deleted
            /*
            switch( enumName )
            {
                case "AppOption";
                    break;
            }
            */
            return true;
        }

        #region 以下由Workflow Generator 2.0自动生成，不推荐改动
        public DemoWorkflow()
            : base()
        {
        }
        public long EventFlag
        {
            get;
            set;
        }
        public Guid InstanceId
        {
            get;
            set;
        }
        public long DataID
        {
            get;
            set;
        }

        public void Event1_Invoked( object sender , ExternalDataEventArgs e )
        {
            EventFlag |= (1<<0);
        }
        public void Event2_Invoked( object sender , ExternalDataEventArgs e )
        {
            EventFlag |= (1<<1);
        }
        public void Event3_Invoked( object sender , ExternalDataEventArgs e )
        {
            EventFlag |= (1<<2);
        }
        public void Event4_Invoked( object sender , ExternalDataEventArgs e )
        {
            EventFlag |= (1<<3);
        }
        public void Event5_Invoked( object sender , ExternalDataEventArgs e )
        {
            EventFlag |= (1<<4);
        }
        public void Event6_Invoked( object sender , ExternalDataEventArgs e )
        {
            EventFlag |= (1<<5);
        }

        public void ParallelCondition1( object sender , ConditionalEventArgs e )
        {
            e. Result = ((EventFlag&7) == 7);
            if(e.Result)
                EventFlag = 0;
        }
        static public long FinanceApproved1_Mask
        {
            get
            {
                return 1;
            }
        }
        static public long FinanceApproved2_Mask
        {
            get
            {
                return 2;
            }
        }
        static public long FinanceApproved3_Mask
        {
            get
            {
                return 4;
            }
        }

        public void ParallelCondition2( object sender , ConditionalEventArgs e )
        {
            e. Result = ((EventFlag&56) == 56);
            if(e.Result)
                EventFlag = 0;
        }
        static public long FinanceRejected1_Mask
        {
            get
            {
                return 8;
            }
        }
        static public long FinanceRejected2_Mask
        {
            get
            {
                return 16;
            }
        }
        static public long FinanceRejected3_Mask
        {
            get
            {
                return 32;
            }
        }

        public Decimal Amount
        {
            get
           {
                return GetAmount( DataID );
            }
        }
        public int AppOption
        {
            get
           {
                return GetAppOption( DataID );
            }
        }

        static public String WorkflowName
        {
            get
            {
                return "DemoWorkflow";
            }
        }
        static public String TableName
        {
            get
            {
                return "Application";
            }
        }

        static public String WaitSubmitState
        {
            get
            {
                return "WaitSubmit";
            }
        }
        static public String WaitFinanceApproveState
        {
            get
            {
                return "WaitFinanceApprove";
            }
        }
        static public String WaitManagerApproveState
        {
            get
            {
                return "WaitManagerApprove";
            }
        }
        static public String AppApprovedState
        {
            get
            {
                return "AppApproved";
            }
        }
        static public String AppRejectedState
        {
            get
            {
                return "AppRejected";
            }
        }
        static public String EndState
        {
            get
            {
                return "End";
            }
        }

        static public String AppSubmittedEvent
        {
            get
            {
                return "AppSubmitted";
            }
        }
        static public String FinanceApproved1Event
        {
            get
            {
                return "FinanceApproved1";
            }
        }
        static public String FinanceApproved2Event
        {
            get
            {
                return "FinanceApproved2";
            }
        }
        static public String FinanceApproved3Event
        {
            get
            {
                return "FinanceApproved3";
            }
        }
        static public String FinanceRejected1Event
        {
            get
            {
                return "FinanceRejected1";
            }
        }
        static public String FinanceRejected2Event
        {
            get
            {
                return "FinanceRejected2";
            }
        }
        static public String FinanceRejected3Event
        {
            get
            {
                return "FinanceRejected3";
            }
        }
        static public String FinanceCountersignedEvent
        {
            get
            {
                return "FinanceCountersigned";
            }
        }
        static public String ManagerApprovedEvent
        {
            get
            {
                return "ManagerApproved";
            }
        }
        static public String ManagerRejectedEvent
        {
            get
            {
                return "ManagerRejected";
            }
        }

        static public String AmountName
        {
            get
            {
                return "Amount";
            }
        }
        static public String AppOptionName
        {
            get
            {
                return "AppOption";
            }
        }
        #endregion
    }
}
