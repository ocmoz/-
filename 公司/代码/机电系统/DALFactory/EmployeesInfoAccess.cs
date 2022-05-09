using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.EmployeesInfo;
using System.Configuration;
using System.Reflection;
namespace FM2E.DALFactory
{
    public sealed class EmployeesInfoAccess
    {
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        public static IEmployeesInfo CreateEquipment()
        {
            return (IEmployeesInfo)InstanceCache.CreateInstance(path, ".EmployeesInfo.EmployeesInfo");
        }
    }
}
