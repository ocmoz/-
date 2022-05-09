using System;
using System.Collections.Generic;
using System.Workflow .Activities;

namespace FM2E.WorkflowLayer
{
    public class InsuranceEventService : IInsuranceEventService, IBasicEventService
    {
        public void RaiseEvent( String eventName, Guid instanceID)
        {
            EventHandler<ExternalDataEventArgs> handler = _eventList [eventName ];
            if(handler != null)
                handler(null, new ExternalDataEventArgs(instanceID));
        }

        Dictionary<String, EventHandler<ExternalDataEventArgs>> _eventList = new Dictionary<string, EventHandler<ExternalDataEventArgs>>();

        public event EventHandler<ExternalDataEventArgs> AppSubmited
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("AppSubmited", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> Repair
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("Repair", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> RepairApproved
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("RepairApproved", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> RepairRejected
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("RepairRejected", value);
            }
            remove
            {
            }
        }
       
    }
}
