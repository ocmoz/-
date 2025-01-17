﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Contract
{
    public class ContractInformationInfo
    {
        #region Model
        private long _Id;
        private string _ContractNo;
        private string _ContractName;
        private decimal _ContractAmount;
        private decimal _SettlementAmount;
        private string _ContractedUnits;
        private int _Period;
        private decimal _Retentions;
        private string _ContractPeople;
        private string _ContractTheWay;

        private decimal _Prepaid;
        private int _InterimPaymentId;
        private decimal _CompletedPayment;
        private decimal _HandOverpay;

        private string _Attachment;
        
        /// <summary>
        /// 合同ID
        /// </summary>
        public long Id
        {
            set { _Id = value; }
            get { return _Id; }
        }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo
        {
            set { _ContractNo = value; }
            get { return _ContractNo; }
        }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContractName
        {
            set { _ContractName = value; }
            get { return _ContractName; }
        }
        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal ContractAmount
        {
            set { _ContractAmount = value; }
            get { return _ContractAmount; }
        }
        /// <summary>
        /// 结算金额
        /// </summary>
        public decimal SettlementAmount
        {
            set { _SettlementAmount = value; }
            get { return _SettlementAmount; }
        }
        /// <summary>
        /// 签约单位
        /// </summary>
        public string ContractedUnits
        {
            set { _ContractedUnits = value; }
            get { return _ContractedUnits; }
        }
        /// <summary>
        /// 期数
        /// </summary>
        public int Period
        {
            set { _Period = value; }
            get { return _Period; }
        }
        /// <summary>
        /// 质保金
        /// </summary>
        public decimal Retentions
        {
            set { _Retentions = value; }
            get { return _Retentions; }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContractPeople
        {
            set { _ContractPeople = value; }
            get { return _ContractPeople; }
        }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string ContractTheWay
        {
            set { _ContractTheWay = value; }
            get { return _ContractTheWay; }
        }

        /// <summary>
        /// 预付
        /// </summary>
        public decimal Prepaid
        {
            set { _Prepaid = value; }
            get { return _Prepaid; }
        }
        /// <summary>
        /// 期中支付ID
        /// </summary>
        public int InterimPaymentId
        {
            set { _InterimPaymentId = value; }
            get { return _InterimPaymentId; }
        }
        /// <summary>
        /// 竣工支付
        /// </summary>
        public decimal CompletedPayment
        {
            set { _CompletedPayment = value; }
            get { return _CompletedPayment; }
        }
        /// <summary>
        /// 交工支付
        /// </summary>
        public decimal HandOverpay
        {
            set { _HandOverpay = value; }
            get { return _HandOverpay; }
        }

        public string Attachment
        {
            set { _Attachment = value; }
            get { return _Attachment; }
        }

        
      
        #endregion Model
    }
}
