using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using FM2E.IDAL.System;

namespace FM2E.DALFactory
{
    /// <summary>
    /// This class is implemented following the Abstract Factory pattern to create the DAL implementation
    /// specified from the configuration file
    /// </summary>
    public sealed class SystemAccess
    {

        // Look up the DAL implementation we should be using
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        private SystemAccess() { }

        public static IModule CreateModule()
        {
            return (IModule)InstanceCache.CreateInstance(path, ".System.Module");
        }

        public static IRole CreateRole()
        {
            return (IRole)InstanceCache.CreateInstance(path, ".System.Role");
        }

        public static IRolePermission CreateRolePermission()
        {
            return (IRolePermission)InstanceCache.CreateInstance(path, ".System.RolePermission");
        }

        public static IUser  CreateUser()
        {
            return (IUser)InstanceCache.CreateInstance(path, ".System.User");
        }

        public static IRoleForWorkflow CreateRoleForWorkflow()
        {
            return (IRoleForWorkflow)InstanceCache.CreateInstance(path, ".System.RoleForWorkflow");
        }


    }
}
