using System;
using System.Collections.Generic;
using System.Workflow .Activities;

namespace FM2E.WorkflowLayer
{
    public class SpecialProjectEventService : ISpecialProjectEventService, IBasicEventService
    {
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

        public event EventHandler<ExternalDataEventArgs> BudgetCheckPass
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("BudgetCheckPass", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> BudgetCheckFailed
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("BudgetCheckFailed", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> BudgetApprovalPass
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("BudgetApprovalPass", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> BudgetApprovalFailed
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("BudgetApprovalFailed", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> DesignInputSubmit
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("DesignInputSubmit", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> ProjectSetupSubmit
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("ProjectSetupSubmit", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> CompanyApprovalPass
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("CompanyApprovalPass", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> CompanyApprovalFailed
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("CompanyApprovalFailed", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> BiddingSubmit
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("BiddingSubmit", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> Start
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("Start", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> Finish
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("Finish", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> Pass
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("Pass", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> Complete
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("Complete", value);
            }
            remove
            {
            }
        }

    }
}
