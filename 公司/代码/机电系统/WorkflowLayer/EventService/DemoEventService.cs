using System;
using System.Collections.Generic;
using System.Workflow .Activities;

namespace FM2E.WorkflowLayer
{
    public class DemoEventService : IDemoEventService, IBasicEventService
    {
        #region 以下由Workflow Generator 2.0自动生成，不推荐改动
        public void RaiseEvent( String eventName, Guid instanceID)
        {
            EventHandler<ExternalDataEventArgs> handler = _eventList [eventName ];
            if(handler != null)
                handler(null, new ExternalDataEventArgs(instanceID));
        }

        Dictionary<String, EventHandler<ExternalDataEventArgs>> _eventList = new Dictionary<string, EventHandler<ExternalDataEventArgs>>();

        public event EventHandler<ExternalDataEventArgs> AppSubmitted
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("AppSubmitted", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> FinanceApproved1
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("FinanceApproved1", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> FinanceApproved2
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("FinanceApproved2", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> FinanceApproved3
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("FinanceApproved3", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> FinanceRejected1
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("FinanceRejected1", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> FinanceRejected2
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("FinanceRejected2", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> FinanceRejected3
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("FinanceRejected3", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> FinanceCountersigned
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("FinanceCountersigned", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> ManagerApproved
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("ManagerApproved", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> ManagerRejected
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("ManagerRejected", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> _EstimationEvent_1
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("_EstimationEvent_1", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> _EstimationEvent_2
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("_EstimationEvent_2", value);
            }
            remove
            {
            }
        }
        #endregion
    }
}
