using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using FM2E.IDAL.Contract;

namespace FM2E.DALFactory
{
    public sealed class ContractAccess
    {
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        private ContractAccess() { }

        public static IContractInformation CreateInsurance()
        {
            return (IContractInformation)InstanceCache.CreateInstance(path,".Contract.ContractInformation");
        }
    }
}
