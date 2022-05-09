using System;
using System.Collections.Generic;
using System.Workflow .Activities;

namespace FM2E.WorkflowLayer
{
    public class OutWarehouseEventService : IOutWarehouseEventService, IBasicEventService
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
        public event EventHandler<ExternalDataEventArgs> DeleteDraft
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("DeleteDraft", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> DoApprovalFailed
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("DoApprovalFailed", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> ApprovalReturnModify
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("ApprovalReturnModify", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> LeaderApproval
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("LeaderApproval", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> ApprovalPass
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("ApprovalPass", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> SubmitReturnModify
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("SubmitReturnModify", value);
            }
            remove
            {
            }
        }
        public event EventHandler<ExternalDataEventArgs> FinishOut
        {
            add
            {
                if(_eventList.ContainsValue(value) == false)
                    _eventList.Add("FinishOut", value);
            }
            remove
            {
            }
        }
        #endregion
    }
}
