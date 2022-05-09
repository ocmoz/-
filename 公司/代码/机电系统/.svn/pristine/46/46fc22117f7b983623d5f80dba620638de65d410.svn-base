using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{

    /// <summary>
    /// 采购申请单审批记录结果
    /// </summary>
    public enum PurchaseOrderApprovalResult
    {
        /// <summary>
        /// 通过审批
        /// </summary>
        PASS = 1,
        /// <summary>
        /// 返回修改
        /// </summary>
        RETURNANDMODIFY = 2,
        /// <summary>
        /// 不通过，直接进入终止
        /// </summary>
        NOTPASS = 3
    }

    /// <summary>
    /// 采购申请单审批记录实体
    /// </summary>
    public class PurchaseOrderApprovalInfo
    {
       

        public PurchaseOrderApprovalInfo()
        { }
        #region Model
        private long _id;
        private long _ordersn;
        private string _companyid;
        private string _purchaseorderid;
        private int _suborderindex;
        private string _approvaler;
        private string _approvalername;
        private PurchaseOrderApprovalResult _result;
        private string _feeback;
        private DateTime _approvaldate;

        public string ApprovalerName
        {

            set { _approvalername = value; }
            get { return _approvalername; }
        }
        /// <summary>
        /// 数据库自增ID
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 采购申请单的数据库序号
        /// </summary>
        public long OrderSn
        {
            set { _ordersn = value; }
            get { return _ordersn; }
        }

        /// <summary>
        /// 采购申请单公司ID
        /// </summary>
        public string CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }

        /// <summary>
        /// 采购申请单逻辑序号
        /// </summary>
        public string PurchaseOrderID
        {
            set { _purchaseorderid = value; }
            get { return _purchaseorderid; }
        }
        /// <summary>
        /// 采购申请单逻辑子序号
        /// </summary>
        public int SubOrderIndex
        {
            set { _suborderindex = value; }
            get { return _suborderindex; }
        }
        /// <summary>
        /// 审批人ID
        /// </summary>
        public string Approvaler
        {
            set { _approvaler = value; }
            get { return _approvaler; }
        }
        /// <summary>
        /// 审批结果，1通过，2返回修改，3不通过
        /// </summary>
        public PurchaseOrderApprovalResult Result
        {
            set { _result = value; }
            get { return _result; }
        }

        /// <summary>
        /// 审批结果字符串
        /// </summary>
        public string ResultString
        {
            get
            {
                string str = "未知审批结果";
                switch (_result)
                {
                    case PurchaseOrderApprovalResult.RETURNANDMODIFY:
                        str = "返回修改";
                        break;
                    case PurchaseOrderApprovalResult.PASS:
                        str = "审批通过";
                        break;
                    case PurchaseOrderApprovalResult.NOTPASS:
                        str = "审批不通过";
                        break;
                    default:
                        break;
                }
                return str;
            }
        }

        /// <summary>
        /// 审批反馈信息
        /// </summary>
        public string FeeBack
        {
            set { _feeback = value; }
            get { return _feeback; }
        }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime ApprovalDate
        {
            set { _approvaldate = value; }
            get { return _approvaldate; }
        }

        private string _approvallog;
        /// <summary>
        /// 审批日志，系统自动记录，用于保存审批的时候数量的变化和价钱的变化
        /// </summary>
        public string ApprovalLog
        {
            set { _approvallog = value; }
            get { return _approvallog; }
        }

        #endregion Model
    }
}
