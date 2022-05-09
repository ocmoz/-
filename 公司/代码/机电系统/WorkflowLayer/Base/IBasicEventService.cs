using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FM2E.WorkflowLayer
{
    public interface  IBasicEventService
    {
        void RaiseEvent( String eventName, Guid instanceID );
    }
}
