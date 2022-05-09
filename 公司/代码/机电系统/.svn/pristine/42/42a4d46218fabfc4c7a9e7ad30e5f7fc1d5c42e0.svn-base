using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Reflection;

using FM2E.IDAL.Examine;


namespace FM2E.DALFactory
{
    public sealed class ExamineAccess
    {
        // Look up the DAL implementation we should be using
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        private ExamineAccess() { }

        public static IExamine CreateExamine()
        {
            return (IExamine)InstanceCache.CreateInstance(path, ".Examine.Examine");
        }

    }
}
