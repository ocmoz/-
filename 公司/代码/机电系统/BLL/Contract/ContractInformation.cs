﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.IDAL.Contract;
using FM2E.Model.Contract;
using FM2E.DALFactory;

namespace FM2E.BLL.Contract
{
    public class ContractInformation
    {
        /// <summary>
        /// 插入合同基本信息
        /// </summary>
        /// <param name="insuranceInfo"></param>
        public void InsertContractInformation(ContractInformationInfo contractInformationInfo)
        {
            IContractInformation dal = ContractAccess.CreateInsurance();

            dal.InsertContractInformation(contractInformationInfo);
        }

        /// <summary>
        /// 更新一条合同基本信息
        /// </summary>
        /// <param name="item"></param>
        public void UpdateContractInformationInfo(ContractInformationInfo item)
        {
            IContractInformation dal = ContractAccess.CreateInsurance();
            dal.UpdateContractInformationInfo(item);
        }

        public ContractInformationInfo GetContractInformationInfo(long id)
        {
            IContractInformation dal = ContractAccess.CreateInsurance();
            return dal.GetContractInformationInfo(id);
        }

        /// <summary>
        /// 通过条件查询合同基本信息
        /// </summary>
        /// <param name="term"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetContractInformation(ContractInformationInfo term, int pageIndex, int pageSize, out int recordCount)
        {
            IContractInformation dal = ContractAccess.CreateInsurance();
            QueryParam qp = dal.GetSearchTerm(term);
            qp.PageIndex = pageIndex;
            qp.PageSize = pageSize;

            return dal.GetContractInformation(qp, out recordCount);
        }

        /// <summary>
        /// 通过Id删除一条合同基本信息
        /// </summary>
        /// <param name="id"></param>
        public void DelContractInformation(string id)
        {
            IContractInformation dal = ContractAccess.CreateInsurance();
            dal.DelContractInformation(id);
        }


        /// <summary>
        /// 更新支付情况
        /// </summary>
        /// <param name="item"></param>
        public void UpdatePrepaid(ContractInformationInfo item)
        {
            IContractInformation dal = ContractAccess.CreateInsurance();
            dal.UpdatePrepaid(item);
        }

        public IList GetInterimPayment(int id)
        {
            IContractInformation dal = ContractAccess.CreateInsurance();
            return dal.GetInterimPayment(id);
        }

        public void DelInterimPayment(long id)
        {
            IContractInformation dal = ContractAccess.CreateInsurance();
            dal.DelInterimPayment(id);
        }

        public ContractInterimPaymentInfo GetContractInterimPaymentInfo(long id)
        {
            IContractInformation dal = ContractAccess.CreateInsurance();
            return dal.GetContractInterimPaymentInfo(id);
        }

        public void InsertContractInterimPayment(ContractInterimPaymentInfo item)
        {
            IContractInformation dal = ContractAccess.CreateInsurance();
            dal.InsertContractInterimPayment(item);
        }

        public void UpdateContractInterimPayment(ContractInterimPaymentInfo item)
        {
            IContractInformation dal = ContractAccess.CreateInsurance();
            dal.UpdateContractInterimPayment(item);
        }

    }
}
