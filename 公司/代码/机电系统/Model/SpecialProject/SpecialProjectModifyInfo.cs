using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.SpecialProject
{
    /// <summary>
    /// 专项工程变更表状态
    /// </summary>
    public enum SpecialProjectModifyStatus
    {
        /// <summary>
        /// 草稿
        /// </summary>
        DRAFT = 1,

        /// <summary>
        /// 审批中
        /// </summary>
        APPROVALING = 2,

        /// <summary>
        /// 生效
        /// </summary>
        OK = 3,

        /// <summary>
        /// 取消
        /// </summary>
        CANCEL = 4
    }

    /// <summary>
    /// 专项工程变更审批结果
    /// </summary>
    public enum SpecialProjectModifyApprovalResult
    {

        /// <summary>
        /// 通过
        /// </summary>
        SUCCESS = 1,
        /// <summary>
        /// 不通过
        /// </summary>
        FAILED = 2
        
    }

    /// <summary>
    /// 专项工程变更记录实体类
    /// </summary>
    public class SpecialProjectModifyInfo
    {
        public SpecialProjectModifyInfo()
		{}
		#region Model
		private long _projectid;
        private string _ownerapprovaler = "";
        private SpecialProjectModifyApprovalResult _ownerresult;
		private string _ownerfeeback ="";
        private DateTime _ownerapprovaldate = DateTime.MinValue;
        private string _contractapprovaler = "";
        private SpecialProjectModifyApprovalResult _contractresult;
        private string _contractfeeback = "";
        private DateTime _contractapprovaldate = DateTime.MinValue;
        private string _leaderapprovaler = "";
        private SpecialProjectModifyApprovalResult _leaderresult;
        private long _modifyid;
        private string _leaderfeeback = "";
        private DateTime _leaderapprovaldate = DateTime.MinValue;
        private DateTime _applytime = DateTime.MinValue;
        private decimal _budgetchange;
		private decimal _budgetincdesc;
		private int _delaydays;
        private string _changecontent = "";
        private string _contentattechment = "";
        private string _remark = "";
        private SpecialProjectModifyStatus _status;
		/// <summary>
		/// 专项工程ID
		/// </summary>
        public long ProjectID
		{
			set{ _projectid=value;}
			get{return _projectid;}
		}
		/// <summary>
		/// 业主审批人
		/// </summary>
		public string OwnerApprovaler
		{
			set{ _ownerapprovaler=value;}
			get{return _ownerapprovaler;}
		}
		/// <summary>
		/// 业主审批结果
		/// </summary>
		public SpecialProjectModifyApprovalResult OwnerResult
		{
			set{ _ownerresult=value;}
			get{return _ownerresult;}
		}

        /// <summary>
        ///  业主审批结果字符串
        /// </summary>
        public string OwnerResultString
        {
            get { return GetApprovalResultString(_ownerresult); }
        }


		/// <summary>
		/// 业主审批反馈意见
		/// </summary>
		public string OwnerFeeBack
		{
			set{ _ownerfeeback=value;}
			get{return _ownerfeeback;}
		}
		/// <summary>
		/// 业主审批时间
		/// </summary>
		public DateTime OwnerApprovalDate
		{
			set{ _ownerapprovaldate=value;}
			get{return _ownerapprovaldate;}
		}
		/// <summary>
		/// 承包商审批人
		/// </summary>
		public string ContractApprovaler
		{
			set{ _contractapprovaler=value;}
			get{return _contractapprovaler;}
		}
		/// <summary>
		/// 承包商审批结果
		/// </summary>
		public SpecialProjectModifyApprovalResult ContractResult
		{
			set{ _contractresult=value;}
			get{return _contractresult;}
		}

        /// <summary>
        /// 承包商审批结果字符串
        /// </summary>
        public string ContractResultString
        {
            get { return GetApprovalResultString(_contractresult); }
        }

		/// <summary>
		/// 承包商审批反馈意见
		/// </summary>
		public string ContractFeeBack
		{
			set{ _contractfeeback=value;}
			get{return _contractfeeback;}
		}
		/// <summary>
		/// 承包商审批时间
		/// </summary>
		public DateTime ContractApprovalDate
		{
			set{ _contractapprovaldate=value;}
			get{return _contractapprovaldate;}
		}
		/// <summary>
		/// 领导审批人
		/// </summary>
		public string LeaderApprovaler
		{
			set{ _leaderapprovaler=value;}
			get{return _leaderapprovaler;}
		}
		/// <summary>
		/// 领导审批结果
		/// </summary>
        public SpecialProjectModifyApprovalResult LeaderResult
		{
			set{ _leaderresult=value;}
			get{return _leaderresult;}
		}


        /// <summary>
        /// 领导审批结果字符串
        /// </summary>
        public string LeaderResultString
        {
            get { return GetApprovalResultString(_leaderresult); }
        }

        /// <summary>
        /// 把状态值转换为字符串显示
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private string GetApprovalResultString(SpecialProjectModifyApprovalResult result)
        {
            string str = "";
            switch (result)
            {
                case SpecialProjectModifyApprovalResult.FAILED:
                    str = "不通过";
                    break;
                case SpecialProjectModifyApprovalResult.SUCCESS:
                    str = "通过";
                    break;
                default:
                    str = "未审批";
                    break;
            }
            return str;
        }

		/// <summary>
		/// 修改项序号
		/// </summary>
        public long ModifyID
		{
			set{ _modifyid =value;}
            get { return _modifyid; }
		}
		/// <summary>
		/// 领导审批意见反馈
		/// </summary>
		public string LeaderFeeBack
		{
			set{ _leaderfeeback=value;}
			get{return _leaderfeeback;}
		}
		/// <summary>
		/// 领导审批时间
		/// </summary>
		public DateTime LeaderApprovalDate
		{
			set{ _leaderapprovaldate=value;}
			get{return _leaderapprovaldate;}
		}
		/// <summary>
		/// 申请时间
		/// </summary>
		public DateTime ApplyTime
		{
			set{ _applytime=value;}
			get{return _applytime;}
		}
		/// <summary>
		/// 预算变更额（变更为）
		/// </summary>
		public decimal BudgetChange
		{
			set{ _budgetchange=value;}
			get{return _budgetchange;}
		}
		/// <summary>
		/// 预算增减额度
		/// </summary>
		public decimal BudgetIncDesc
		{
			set{ _budgetincdesc=value;}
			get{return _budgetincdesc;}
		}
		/// <summary>
		/// 工程延迟天数
		/// </summary>
		public int DelayDays
		{
			set{ _delaydays=value;}
			get{return _delaydays;}
		}
		/// <summary>
		/// 变更内容描述
		/// </summary>
		public string ChangeContent
		{
			set{ _changecontent=value;}
			get{return _changecontent;}
		}
		/// <summary>
		/// 变更内容附件
		/// </summary>
		public string ContentAttechment
		{
			set{ _contentattechment=value;}
			get{return _contentattechment;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}


        /// <summary>
        /// 变更表状态
        /// </summary>
        public SpecialProjectModifyStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// 变更表状态字符串
        /// </summary>
        public string StatusString
        {
            get
            {
                string str = "";
                switch (_status)
                {
                    case SpecialProjectModifyStatus.DRAFT:
                        str = "草稿";
                        break;
                    case SpecialProjectModifyStatus.CANCEL:
                        str = "取消";
                        break;
                    case SpecialProjectModifyStatus.APPROVALING:
                        str = "审批中";
                        break;
                    case SpecialProjectModifyStatus.OK:
                        str = "生效";
                        break;
                    default:
                        str = "未知";
                        break;
                }
                return str;
            }
        }

        IList _detaillist = new List<SpecialProjectModifyDeviceInfo>();
        /// <summary>
        /// 变更设备明细项列表
        /// </summary>
        public IList DetailList
        {
            get { return _detaillist; }
            set { _detaillist = value; }
        }

        /// <summary>
        /// 从明细中计算出总的变化金额
        /// </summary>
        public decimal TotalAmountFromDetail
        {
            get
            {
                decimal total = 0;
                foreach (SpecialProjectModifyDeviceInfo item in _detaillist)
                {
                    if (item.IsAdd)
                        total += item.Amount;
                    else
                        total -= item.Amount;
                }
                return total;
            }
        }
		#endregion Model
    }
}
