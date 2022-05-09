using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.SpecialProject
{
    /// <summary>
    /// 专项工程预算项实体类
    /// </summary>
    public class SpecialProjectBudgetItemInfo
    {
        public SpecialProjectBudgetItemInfo()
        {
        }

        #region Model
        private long _projectid;
        private long _itemid;
        private bool _isrelated2direct;//是否跟直接费相关
        private string _budgetitemname;
        private decimal _amultiple;
        private decimal _amount;
        private string _remark;
        /// <summary>
        /// 专项工程ID
        /// </summary>
        public long ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 预算项序号，直接费的预算项序号最小的那个
        /// </summary>
        public long ItemID
        {
            set { _itemid = value; }
            get { return _itemid; }
        }

        /// <summary>
        /// 是否与直接费相关，如果是，则比例值有效，AMOUNT需要通过比例计算，如果否，则比例值无效，比例值通过AMOUNT计算
        /// </summary>
        public bool IsRelated2Direct
        {
            set { _isrelated2direct = value; }
            get { return _isrelated2direct; }
        }

        /// <summary>
        /// 预算项名称
        /// </summary>
        public string BudgetItemName
        {
            set { _budgetitemname = value; }
            get { return _budgetitemname; }
        }
        /// <summary>
        /// 与直接费相对应的倍数关系，直接费为1
        /// </summary>
        public decimal Amultiple
        {
            set { _amultiple = value; }
            get { return _amultiple; }
        }
        /// <summary>
        /// 费用
        /// </summary>
        public decimal Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }

        private decimal _directamount;//用于在MODEL中计算
        /// <summary>
        /// 直接费
        /// </summary>
        public decimal DirectAmount
        {
            set
            {
                _directamount = value;
            }
        }

        /// <summary>
        /// 真正的比例，小数
        /// </summary>
        public decimal TrueMultiple
        {
            get
            {
                if (_isrelated2direct)
                    return _amultiple;
                else
                {
                    if (_directamount != 0)
                    {
                        return _amount / _directamount;
                    }
                    else
                        return 0;
                }

            }
        }
        /// <summary>
        /// 真正的金额
        /// </summary>
        public decimal TrueAmount
        {
            get
            {
                if (_isrelated2direct)
                    return _directamount * _amultiple;
                else
                    return _amount;
            }
        }
        #endregion Model
    }
}
