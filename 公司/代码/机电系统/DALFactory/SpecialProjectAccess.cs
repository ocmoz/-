using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using FM2E.IDAL.SpecialProject;

namespace FM2E.DALFactory
{
    /// <summary>
    /// This class is implemented following the Abstract Factory pattern to create the DAL implementation
    /// specified from the configuration file
    /// </summary>
    public sealed class SpecialProjectAccess
    {
         // Look up the DAL implementation we should be using
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        private SpecialProjectAccess() { }

        public static ISpecialProject CreateSpecialProject()
        {
            return (ISpecialProject)InstanceCache.CreateInstance(path, ".SpecialProject.SpecialProject");
        }
    }
}
