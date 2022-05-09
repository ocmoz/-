using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using FM2E.IDAL.PendingOrder;

namespace FM2E.DALFactory
{
    /// <summary>
    /// This class is implemented following the Abstract Factory pattern to create the DAL implementation
    /// specified from the configuration file
    /// </summary>
    public sealed class PendingOrderAccess
    {

        // Look up the DAL implementation we should be using
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        private PendingOrderAccess() { }

       
        public static IPendingOrder CreatePendingOrder()
        {
            return (IPendingOrder)InstanceCache.CreateInstance(path, ".PendingOrder.PendingOrder");
        }

    }
}
