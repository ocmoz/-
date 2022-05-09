using System.Collections.Generic;
using System.Text;
using System.Configuration;
using FM2E.IDAL.Utils;
namespace FM2E.DALFactory
{
    public sealed  class UtilsAccess
    {
         // Look up the DAL implementation we should be using
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        private UtilsAccess() { }

        public static ISheetNO CreateSheetNO()
        {
            return (ISheetNO)InstanceCache.CreateInstance(path, ".Utils.SheetNOGenerator");
        }

        public static IEquipmentIDAssign CreateEquipmentIDAssign()
        {
            return (IEquipmentIDAssign)InstanceCache.CreateInstance(path, ".Utils.EquipmentAssign");
        }

        public static ITransaction CreateTransaction()
        {
            return (ITransaction)InstanceCache.CreateInstance(path, ".Utils.Transaction");
        }
    }
}
