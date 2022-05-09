using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Contract;

namespace FM2E.IDAL.Contract
{
    public interface IContractInformation
    {
        /// <summary>
        /// 插入一条合同基本信息
        /// </summary>
        /// <param name="item"></param>
        void InsertContractInformation(ContractInformationInfo item);
        /// <summary>
        /// 更新一条合同基本信息
        /// </summary>
        /// <param name="item"></param>
        void UpdateContractInformationInfo(ContractInformationInfo item);
        /// <summary>
        /// 通过id获取一条合同基本信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ContractInformationInfo GetContractInformationInfo(long id);

        /// <summary>
        /// 通过id删除一条合同基本信息
        /// </summary>
        /// <param name="id"></param>
        void DelContractInformation(string id);

        QueryParam GetSearchTerm(ContractInformationInfo term);

        IList GetContractInformation(QueryParam term, out int recordCount);


        /// <summary>
        /// 更新支付情况
        /// </summary>
        /// <param name="item"></param>
        void UpdatePrepaid(ContractInformationInfo item);

        IList GetInterimPayment(int id);

        void DelInterimPayment(long id);

        ContractInterimPaymentInfo GetContractInterimPaymentInfo(long id);

        void InsertContractInterimPayment(ContractInterimPaymentInfo item);

        void UpdateContractInterimPayment(ContractInterimPaymentInfo item);
    }
}
