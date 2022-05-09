using System;
using System.Collections.Generic;
using System.Workflow .Activities;

namespace FM2E.WorkflowLayer
{
    public class MalFunctionEventService : IMalFunctionEventService, IBasicEventService
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

        public event EventHandler<ExternalDataEventArgs> SupervisorAccepted
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("SupervisorAccepted", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> SupervisorRejected
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("SupervisorRejected", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> ElectDeptAccepted
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("ElectDeptAccepted", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> ApprovalAccepted
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("ApprovalAccepted", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> ApprovalFailedAndReturn
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("ApprovalFailedAndReturn", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> ApprovalFailedAndBreak
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("ApprovalFailedAndBreak", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> Maintain
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("Maintain", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> Accepted
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("Accepted", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> AcceptanceFailedAndContinue
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("AcceptanceFailedAndContinue", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> AcceptanceFailedAndBreak
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("AcceptanceFailedAndBreak", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> Confirmed
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("Confirmed", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> ConfirmFailedAndContinue
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("ConfirmFailedAndContinue", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> ConfirmFailedAndBreak
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("ConfirmFailedAndBreak", value);
            }
            remove
            {
            }
        }

    }
}
