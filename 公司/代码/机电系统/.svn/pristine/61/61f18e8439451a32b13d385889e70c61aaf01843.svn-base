using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Reflection;

using FM2E.IDAL.Maintain;

namespace FM2E.DALFactory
{
    public sealed class MaintainAccess
    {
    // Look up the DAL implementation we should be using
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        private MaintainAccess() { }

        /// <summary>
        /// 获取维护接口
        /// </summary>
        /// <returns></returns>
        public static IMaintain CreateMaintain()
        {
            return (IMaintain)InstanceCache.CreateInstance(path, ".Maintain.Maintain");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IMaintainPlanConfig CreateMaintainPlanConfig()
        {
            return (IMaintainPlanConfig)InstanceCache.CreateInstance(path, ".Maintain.MaintainPlanConfig");
        }
        public static IMaintainPlanRecord CreateMaintainPlanRecord()
        {
            return (IMaintainPlanRecord)InstanceCache.CreateInstance(path, ".Maintain.MaintainPlanRecord");
        }

        /// <summary>
        /// 日常巡查
        /// </summary>
        /// <returns></returns>
        public static IDailyPatrolDetail CreateDailyPatrolDetail()
        {
            return (IDailyPatrolDetail)InstanceCache.CreateInstance(path, ".Maintain.DailyPatrolDetail");
        }
        public static IDailyPatrolPlan CreateDailyPatrolPlan()
        {
            return (IDailyPatrolPlan)InstanceCache.CreateInstance(path, ".Maintain.DailyPatrolPlan");
        }
        public static IDailyPatrolRecord CreateDailyPatrolRecord()
        {
            return (IDailyPatrolRecord)InstanceCache.CreateInstance(path, ".Maintain.DailyPatrolRecord");
        }
        public static IDailyPatrolConfig CreateDailyPatrolConfig()
        {
            return (IDailyPatrolConfig)InstanceCache.CreateInstance(path, ".Maintain.DailyPatrolConfig");
        }
        /// <summary>
        /// 例行保养
        /// </summary>
        /// <returns></returns>
        public static IRoutineMaintainDetail CreateRoutineMaintainDetail()
        {
            return (IRoutineMaintainDetail)InstanceCache.CreateInstance(path, ".Maintain.RoutineMaintainDetail");
        }
        public static IRoutineMaintainPlan CreateRoutineMaintainPlan()
        {
            return (IRoutineMaintainPlan)InstanceCache.CreateInstance(path, ".Maintain.RoutineMaintainPlan");
        }
        public static IRoutineMaintainRecord CreateRoutineMaintainRecord()
        {
            return (IRoutineMaintainRecord)InstanceCache.CreateInstance(path, ".Maintain.RoutineMaintainRecord");
        }
        public static IRoutineMaintainConfig CreateRoutineMaintainConfig()
        {
            return (IRoutineMaintainConfig)InstanceCache.CreateInstance(path, ".Maintain.RoutineMaintainConfig");
        }
        /// <summary>
        /// 例行检测
        /// </summary>
        /// <returns></returns>
        public static IRoutineInspectDetail CreateRoutineInspectDetail()
        {
            return (IRoutineInspectDetail)InstanceCache.CreateInstance(path, ".Maintain.RoutineInspectDetail");
        }
        public static IRoutineInspectPlan CreateRoutineInspectPlan()
        {
            return (IRoutineInspectPlan)InstanceCache.CreateInstance(path, ".Maintain.RoutineInspectPlan");
        }
        public static IRoutineInspectRecord CreateRoutineInspectRecord()
        {
            return (IRoutineInspectRecord)InstanceCache.CreateInstance(path, ".Maintain.RoutineInspectRecord");
        }
        public static IMalfunctionClassify CreateMalfunctionClassify()
        {
            return (IMalfunctionClassify)InstanceCache.CreateInstance(path, ".Maintain.MalfunctionClassify");
        }
        public static IMalfunctionHandle CreateMalfunctionHandle()
        {
            return (IMalfunctionHandle)InstanceCache.CreateInstance(path, ".Maintain.MalfunctionHandle");
        }


        public static IEquipmentCost CreateEquipmentCost()
        {
            return (IEquipmentCost)InstanceCache.CreateInstance(path, ".Maintain.EquipmentCost");
        }
    }
}
