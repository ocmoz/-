using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Reflection;

using FM2E.IDAL.Archives;

namespace FM2E.DALFactory
{
    public sealed class ArchivesAccess
    {
        // Look up the DAL implementation we should be using
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        private ArchivesAccess() { }
        /// <summary>
        /// 档案借阅
        /// </summary>
        /// <returns></returns>
        public static IArchivesBorrowApply CreateArchivesBorrowApply()
        {
            return (IArchivesBorrowApply)InstanceCache.CreateInstance(path, ".Archives.ArchivesBorrowApply");
        }
        public static IArchivesDestroyApply CreateArchivesDestroyApply()
        {
            return (IArchivesDestroyApply)InstanceCache.CreateInstance(path, ".Archives.ArchivesDestroyApply");
        }
        /// <summary>
        /// 创建ArchivesType数据层接口
        /// </summary>
        public static IArchivesType CreateArchivesType()
        {
            return (IArchivesType)InstanceCache.CreateInstance(path, ".Archives.ArchivesType");
        }
        /// <summary>
        /// 创建Archives数据层接口
        /// </summary>
        public static IArchives CreateArchives()
        {
            return (IArchives)InstanceCache.CreateInstance(path, ".Archives.Archives");
        }
        /// <summary>
        /// 创建ArchivesAttachment数据层接口
        /// </summary>
        public static IArchivesAttachment CreateArchivesAttachment()
        {
            return (IArchivesAttachment)InstanceCache.CreateInstance(path, ".Archives.ArchivesAttachment");
        }


    }
}