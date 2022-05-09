using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{

    /// <summary>
    /// 采购申请单修改方式
    /// </summary>
    public enum PurchaseOrderModifyType
    {
        /// <summary>
        /// 保存
        /// </summary>
        SAVE = 1,
        /// <summary>
        /// 提交
        /// </summary>
        SUBMIT = 2
    }

    /// <summary>
    /// 采购申请单修改记录实体
    /// </summary>
    public class PurchaseOrderModifyInfo
    {


        public PurchaseOrderModifyInfo()
        { }
        #region Model
        private long _id;
        private long _ordersn;
        private string _companyid;
        private string _purchaseorderid;
        private int _suborderindex;
        private string _modifier;
        private string _modifiername;
        private PurchaseOrderModifyType _modifytype;
        private string _savecontent;
        private DateTime _modifytime;
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
        /// 修改人ID
        /// </summary>
        public string Modifier
        {
            set { _modifier = value; }
            get { return _modifier; }
        }
        /// <summary>
        /// 修改人姓名
        /// </summary>
        public string ModifierName
        {
            set { _modifiername = value; }
            get { return _modifiername; }
        }
        /// <summary>
        /// 修改方式，1保存、2保存并提交
        /// </summary>
        public PurchaseOrderModifyType ModifyType
        {
            set { _modifytype = value; }
            get { return _modifytype; }
        }

        /// <summary>
        /// 修改方式字符串
        /// </summary>
        public string ModifyTypeString
        {
            get
            {
                string str = "未知操作";
                switch (_modifytype)
                {
                    case PurchaseOrderModifyType.SAVE:
                        str = "保存";
                        break;
                    case PurchaseOrderModifyType.SUBMIT:
                        str = "提交";
                        break;
                    default:
                        break;
                }
                return str;
            }
        }

        /// <summary>
        /// 修改后保存的内容
        /// </summary>
        public string Content
        {
            set { _savecontent = value; }
            get { return _savecontent; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime
        {
            set { _modifytime = value; }
            get { return _modifytime; }
        }



        #endregion Model
    }
}
