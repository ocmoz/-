using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using FM2E.IDAL.Message;

namespace FM2E.DALFactory
{
    /// <summary>
    /// This class is implemented following the Abstract Factory pattern to create the DAL implementation
    /// specified from the configuration file
    /// </summary>
    public sealed class MessageAccess
    {

        // Look up the DAL implementation we should be using
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        private MessageAccess() { }

       
        public static IMessage CreateMessage()
        {
            return (IMessage)InstanceCache.CreateInstance(path, ".Message.Message");
        }

    }
}
