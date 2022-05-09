using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.Activities;

namespace FM2E.WorkflowLayer
{
    public class SGS_InWarehouseEventService : ISGS_InWarehouseEventService, IBasicEventService
    {
        public void RaiseEvent(String eventName, Guid instanceID)
        {
            EventHandler<ExternalDataEventArgs> handler = _eventList[eventName];
            if (handler != null)
                handler(null, new ExternalDataEventArgs(instanceID));
        }

        Dictionary<String, EventHandler<ExternalDataEventArgs>> _eventList = new Dictionary<string, EventHandler<ExternalDataEventArgs>>();

        public event EventHandler<ExternalDataEventArgs> AppSubmited
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("AppSubmited", value);
            }
            remove
            {
            }
        }

        public event EventHandler<ExternalDataEventArgs> EngineerApproved
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("EngineerApproved", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> EngineerRejected
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("EngineerRejected", value);
            }
            remove
            {
            }
        }
        

        public event EventHandler<ExternalDataEventArgs> ManagerApproved
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ManagerApproved", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> RetuenEngineer
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("RetuenEngineer", value);
            }
            remove
            {
            }
        }
        
        public event EventHandler<ExternalDataEventArgs> ManagerRejected
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("ManagerRejected", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> WarehouseKeeperApproved
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("WarehouseKeeperApproved", value);
            }
            remove
            {
            }
        }
      
         public event EventHandler<ExternalDataEventArgs> SubmitReturnModify
        {
            add
            {
                if (_eventList.ContainsValue(value) == false)
                    _eventList.Add("SubmitReturnModify", value);
            }
            remove
            {
            }
        }

    }
}
