using System;
using System.Collections.Generic;
using System.Workflow .Activities;

namespace FM2E.WorkflowLayer
{
    public class BudgetYearEventService : IBudgetYearEventService, IBasicEventService
    {
        #region 以下由Workflow Generator 2.0自动生成，不推荐改动
        public void RaiseEvent( String eventName, Guid instanceID)
        {
            EventHandler<ExternalDataEventArgs> handler = _eventList [eventName ];
            if(handler != null)
                handler(null, new ExternalDataEventArgs(instanceID));
        }

        Dictionary<String, EventHandler<ExternalDataEventArgs>> _eventList = new Dictionary<string, EventHandler<ExternalDataEventArgs>>();

        public event EventHandler<ExternalDataEventArgs> SaveNew
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("SaveNew", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> SubmitNew
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("SubmitNew", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> SaveDraft
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("SaveDraft", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> SubmitDraft
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("SubmitDraft", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> FinanceReturntoDraft
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("FinanceReturntoDraft", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> SubmitFinanceApproval
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("SubmitFinanceApproval", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> LeaderReturntoDraft
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("LeaderReturntoDraft", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> LeaderReturntoFinance
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("LeaderReturntoFinance", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> SubmitLeaderApproval
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("SubmitLeaderApproval", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> CompanyReturntoDraft
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("CompanyReturntoDraft", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> CompanyReturntoLeader
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("CompanyReturntoLeader", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> SubmitCompanySuccess
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("SubmitCompanySuccess", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> ToArchinveSuccess
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("ToArchinveSuccess", value);
            }
            remove
            {
            }
        }
        #endregion
    }
}
