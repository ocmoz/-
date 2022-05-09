using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using FM2E.IDAL.Basic;

namespace FM2E.DALFactory
{
    /// <summary>
    /// This class is implemented following the Abstract Factory pattern to create the DAL implementation
    /// specified from the configuration file
    /// </summary>
    public sealed class BasicAccess
    {

        // Look up the DAL implementation we should be using
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        private BasicAccess() { }

        public static IDepartment CreateDepartment()
        {
            return (IDepartment)InstanceCache.CreateInstance(path, ".Basic.Department");
        }


        public static ICategory CreateEquimentCaterogy()
        {
            return (ICategory)InstanceCache.CreateInstance(path, ".Basic.Category");
        }

        public static IProducer CreateProducer()
        {
            return (IProducer)InstanceCache.CreateInstance(path, ".Basic.Producer");
        }

        public static IRStatus CreateRStatus()
        {
            return (IRStatus)InstanceCache.CreateInstance(path, ".Basic.RStatus");
        }
        public static ISupplier CreateSupplier()
        {
            return (ISupplier)InstanceCache.CreateInstance(path, ".Basic.Supplier");
        }
        public static IUStatus CreateUStatus()
        {
            return (IUStatus)InstanceCache.CreateInstance(path, ".Basic.UStatus");
        }
        public static IWarehouse CreateWarehouse()
        {
            return (IWarehouse)InstanceCache.CreateInstance(path, ".Basic.Warehouse");
        }

        public static IPosition CreatePosition()
        {
            return (IPosition)InstanceCache.CreateInstance(path, ".Basic.Position");
        }

        public static ICompany CreateCompany()
        {
            return (ICompany)InstanceCache.CreateInstance(path, ".Basic.Company");
        }

        public static ISection CreateSection()
        {
            return (ISection)InstanceCache.CreateInstance(path, ".Basic.Section");
        }

        public static ITollGate CreateTollGate()
        {
            return (ITollGate)InstanceCache.CreateInstance(path, ".Basic.TollGate");
        }
        public static IChannal CreateChannal()
        {
            return (IChannal)InstanceCache.CreateInstance(path, ".Basic.Channal");
        }
        public static IContractor CreateContractor()
        {
            return (IContractor)InstanceCache.CreateInstance(path, ".Basic.Contractor");
        }
        public static IEquipmentSystem CreateSystem()
        {
            return (IEquipmentSystem)InstanceCache.CreateInstance(path, ".Basic.EquipmentSystem");
        }

        public static IAddress CreateAddress()
        {
            return (IAddress)InstanceCache.CreateInstance(path, ".Basic.Address");
        }
    }
}
