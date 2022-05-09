using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

using FM2E.IDAL.Plan;

namespace FM2E.DALFactory
{
    public sealed class PlanAccess
    {
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        private PlanAccess() { }

        public static IPlanInformation CreateSchedule()
        {
            return (IPlanInformation)InstanceCache.CreateInstance(path, ".Plan.PlanInformation");
        }
    }
}
