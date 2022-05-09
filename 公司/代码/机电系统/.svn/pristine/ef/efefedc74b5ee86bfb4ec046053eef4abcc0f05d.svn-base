using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using FM2E.IDAL.Consumables;

namespace FM2E.DALFactory
{
    public sealed class ConsumablesAccess
    {
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        public static IConsumables CreateConsumables()
        {
            return (IConsumables)InstanceCache.CreateInstance(path, ".Consumables.Consumables");
        }
    }
}
