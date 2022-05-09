using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Reflection;

using FM2E.IDAL.Equipment;

namespace FM2E.DALFactory
{
    public sealed class EquipmentAccess
    {
    // Look up the DAL implementation we should be using
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        private EquipmentAccess() { }

        public static IEquipment CreateEquipment()
        {
            return (IEquipment)InstanceCache.CreateInstance(path, ".Equipment.Equipment");
        }

        
        public static IOutWarehouseApply CreateOutWarehouseApply()
        {
            return (IOutWarehouseApply)InstanceCache.CreateInstance(path, ".Equipment.OutWarehouseApply");
        }
        public static IOutWarehouseDetail CreateOutWarehouseDetail()
        {
            return (IOutWarehouseDetail)InstanceCache.CreateInstance(path, ".Equipment.OutWarehouseDetail");
        }
        public static IOutEquipments CreateOutEquipments()
        {
            return (IOutEquipments)InstanceCache.CreateInstance(path, ".Equipment.OutEquipments");
        }
        public static IInEquipments CreateInEquipments()
        {
            return (IInEquipments)InstanceCache.CreateInstance(path, ".Equipment.InEquipments");
        }
        public static IInWarehouse CreateInWarehouse()
        {
            return (IInWarehouse)InstanceCache.CreateInstance(path, ".Equipment.InWarehouse");
        }
        public static IExpendable CreateExpendable()
        {
            return (IExpendable)InstanceCache.CreateInstance(path, ".Equipment.Expendable");
        }

        public static IWareHouseCheck CreateWareHouseCheck()
        {
            return (IWareHouseCheck)InstanceCache.CreateInstance(path, ".Equipment.WareHouseCheck");
        }

        public static IBorrowApply CreateBorrowApply()
        {
            return (IBorrowApply)InstanceCache.CreateInstance(path, ".Equipment.BorrowApply");
        }

        public static IBorrowRecord CreateBorrowRecord()
        {
            return (IBorrowRecord)InstanceCache.CreateInstance(path, ".Equipment.BorrowRecord");
        }

        public static IReturnAcceptance CreateReturnAcceptance()
        {
            return (IReturnAcceptance)InstanceCache.CreateInstance(path, ".Equipment.ReturnAcceptance");
        }

        public static IPriceManager CreatePriceManager()
        {
            return (IPriceManager)InstanceCache.CreateInstance(path, ".Equipment.PriceManager");
        }

        public static IWarehouseInventory CreateWarehouseInventory()
        {
            return (IWarehouseInventory)InstanceCache.CreateInstance(path, ".Equipment.WarehouseInventory");
        }

        public static IPurchase CreatePurchase()
        {
            return (IPurchase)InstanceCache.CreateInstance(path, ".Equipment.Purchase");
        }
        
        public static IScrapApply CreateScrapApply()
        {
            return (IScrapApply)InstanceCache.CreateInstance(path, ".Equipment.ScrapApply");
        }

        public static IScrapRecord CreateScrapRecord()
        {
            return (IScrapRecord)InstanceCache.CreateInstance(path, ".Equipment.ScrapRecord");
        }

        public static ICheckAcceptance CreateCheckAcceptance()
        {
            return (ICheckAcceptance)InstanceCache.CreateInstance(path, ".Equipment.CheckAcceptance");
        }

        public static IExpendableInOut CreateExpendableInOut()
        {
            return (IExpendableInOut)InstanceCache.CreateInstance(path, ".Equipment.ExpendableInOut");
        }
        /// <summary>
        /// 创建SubsidiaryEquipment数据层接口
        /// </summary>
        public static ISubsidiaryEquipment CreateSubsidiaryEquipment()
        {
            return (ISubsidiaryEquipment)InstanceCache.CreateInstance(path, ".Equipment.SubsidiaryEquipment");
        }
        /// <summary>
        /// 创建ConsumableEquipment数据层接口
        /// </summary>
        public static IConsumableEquipment CreateConsumableEquipment()
        {
            return (IConsumableEquipment)InstanceCache.CreateInstance(path, ".Equipment.ConsumableEquipment");
        }
        /// <summary>
        /// 创建ConsumableEquipmentDetail数据层接口
        /// </summary>
        public static IConsumableEquipmentDetail CreateConsumableEquipmentDetail()
        {
            return (IConsumableEquipmentDetail)InstanceCache.CreateInstance(path, ".Equipment.ConsumableEquipmentDetail");
        }

    }
}
