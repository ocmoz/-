using System;
using System.Collections.Generic;
using System.Workflow .Activities;

namespace FM2E.WorkflowLayer
{
    public class ArchivesBorrowEventService : IArchivesBorrowEventService, IBasicEventService
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

    }
}
