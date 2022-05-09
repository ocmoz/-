using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 采购单状态
    /// </summary>
    public enum PurchaseOrderStatus
    {
        /// <summary>
        /// 草稿状态
        /// </summary>
        DRAFT = 1,
        /// <summary>
        /// 等待审批
        /// </summary>
        WAITING4APPROVAL = 2,
        /// <summary>
        /// 审批中
        /// </summary>
        APPROVALING = 3,
        /// <summary>
        /// 审批不通过，返回修改
        /// </summary>
        APPROVALANDRETURN = 4,
        /// <summary>
        /// 审批不通过
        /// </summary>
        APPROVALFAILED = 5,
        /// <summary>
        /// 返回修改再审批
        /// </summary>
        REAPPROVALING = 6,
        /// <summary>
        /// 等待采购，即审批通过
        /// </summary>
        WAITING4PURCHASE = 7,
        /// <summary>
        /// 采购中
        /// </summary>
        PURCHASING = 8,
        /// <summary>
        /// 采购完成
        /// </summary>
        PURCHASINGFINISH = 9,
        /// <summary>
        /// 验收中
        /// </summary>
        ACCEPTING = 10,
        /// <summary>
        /// 验收完毕
        /// </summary>
        ACCEPTINGFINISH= 11,
        /// <summary>
        /// 入库中
        /// </summary>
        INWAREHOUSEING = 12,
        /// <summary>
        /// 入库完毕
        /// </summary>
        INWAREHOUSEFINISH = 13,
        /// <summary>
        /// 采购计划结束
        /// </summary>
        TERMINATED = 14
    }

    /// <summary>
    /// 采购单分发状态
    /// </summary>
    public enum PurchaseOrderDeliveryStatus
    {
        /// <summary>
        /// 未分发
        /// </summary>
        NONE = 1,
        /// <summary>
        /// 部分分发
        /// </summary>
        PART = 2,
        /// <summary>
        /// 全部分发
        /// </summary>
        ALL = 3
    }

    /// <summary>
    /// 采购单采购状态
    /// </summary>
    public enum PurchasingStatus
    {
        /// <summary>
        /// 未有进入采购状态的
        /// </summary>
        ALLWAITING = 1,
        /// <summary>
        /// 部分正在采购的
        /// </summary>
        PURCHASING = 2,
        /// <summary>
        /// 完成全部采购的
        /// </summary>
        FINISH = 3
    }


    /// <summary>
    /// 采购单实体
    /// </summary>
    public class PurchaseOrderInfo
    {
        public PurchaseOrderInfo()
        {
        }
        #region Model
        private long _id;
        private int _nextorderindex;
        private DateTime _updatetime = DateTime.MinValue;
        private string _purchaseorderid = "";
        private string _purchaseordername = "";
        private string _companyid = "";
        private string _companyname = "";
       
        private decimal _plantotalamount;
        private string _applicant = "";
        private string _applicantname = "";
        private string _approvaling = "";
        private string _approvalers = "";
        private DateTime _submittime = DateTime.MinValue;
        private PurchaseOrderStatus _status;
        private string _remark = "";
        private short _suborderindex;
        private PurchaseOrderDeliveryStatus _deliverystatus = PurchaseOrderDeliveryStatus.NONE;
        private PurchasingStatus _purchasingstatus = PurchasingStatus.ALLWAITING;


        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            get { return _companyname; }
            set { _companyname = value; }
        }

        /// <summary>
        /// 申请者真实姓名
        /// </summary>
        public string ApplicantName
        {
            get { return _applicantname; }
            set { _applicantname = value; }
        }
/// <summary>
/// 申请人职位
/// </summary>
        public string ApplicantPositionName { get; set; }
        /// <summary>
        /// 申请人部门ID
        /// </summary>
        public long ApplicantDepartmentID { get; set; }
        /// <summary>
        /// 申请人部门名称
        /// </summary>
        public string ApplicantDepartmentName { get; set; }

        /// <summary>
        /// 申请采购总金额
        /// </summary>
        public decimal PlanTotalAmount
        {
            get {
                //if (_detaillist.Count == 0)//不含详情
                //    return _plantotalamount;
                //else
                //{
                //    decimal a = 0;
                //    foreach (PurchaseOrderDetailInfo detail in _detaillist)
                //    {
                //        a += detail.PlanAmount;
                //    }
                //    return a;
                //}
                return _plantotalamount;
            }
            set
            {
                _plantotalamount = value;
            }
        }

        /// <summary>
        /// 采购单分发状态
        /// </summary>
        public PurchaseOrderDeliveryStatus DeliveryStatus
        {
            get { return _deliverystatus; }
            set { _deliverystatus = value; }

        }
        /// <summary>
        /// 采购单指派状态字符串
        /// </summary>
        public string DeliveryStatusString
        {
            get
            {
                string status = "";
                switch (_deliverystatus)
                {

                    case PurchaseOrderDeliveryStatus.ALL:
                        status = "全部已指派";
                        break;
                    case PurchaseOrderDeliveryStatus.NONE:
                        status = "未进行指派";
                        break;
                    case PurchaseOrderDeliveryStatus.PART:
                        status = "部分已指派";
                        break;
                    default:
                        status = "未知状态";
                        break;
                }
                return status;
            }
        }

        /// <summary>
        /// 采购单明细采购状态
        /// </summary>
        public PurchasingStatus PurchasingStatus
        {
            get { return _purchasingstatus; }
            set { _purchasingstatus = value; }

        }
        /// <summary>
        /// 采购单明细采购状态字符串
        /// </summary>
        public string PurchasingStatusString
        {
            get
            {
                string status = "";
                switch (_purchasingstatus)
                {
                    case PurchasingStatus.ALLWAITING:
                        status = "采购未开始";
                        break;
                    case PurchasingStatus.PURCHASING:
                        status = "采购进行中";
                        break;
                    case PurchasingStatus.FINISH:
                        status = "所有采购完成";
                        break;
                    default:
                        status = "未知状态";
                        break;
                }
                return status;
            }
        }

        /// <summary>
        /// 数据库记录ID
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 下一子采购单序号
        /// </summary>
        public int NextOrderIndex
        {
            set { _nextorderindex = value; }
            get { return _nextorderindex; }
        }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 采购单逻辑序号
        /// </summary>
        public string PurchaseOrderID
        {
            set { _purchaseorderid = value; }
            get { return _purchaseorderid; }
        }
        /// <summary>
        /// 采购单名称
        /// </summary>
        public string PurchaseOrderName
        {
            set { _purchaseordername = value; }
            get { return _purchaseordername == null ? "" : _purchaseordername; }
        }
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 申请人ID
        /// </summary>
        public string Applicant
        {
            set { _applicant = value; }
            get { return _applicant == null ? "" : _applicant; }
        }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime SubmitTime
        {
            set { _submittime = value; }
            get { return _submittime; }
        }
        /// <summary>
        /// 当前采购单状态
        /// </summary>
        public PurchaseOrderStatus Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 当前采购单状态文字描述
        /// </summary>
        public string StatusString
        {
            get
            { 
                string status="";
                switch (_status)
                {
                   
                    case PurchaseOrderStatus.ACCEPTING:
                        status = "验收中";
                        break;
                    case PurchaseOrderStatus.ACCEPTINGFINISH:
                        status = "验收完毕";
                        break;
                    case PurchaseOrderStatus.INWAREHOUSEFINISH:
                        status = "入库完毕";
                        break;
                    case PurchaseOrderStatus.INWAREHOUSEING:
                        status = "入库中";
                        break;
                    case PurchaseOrderStatus.PURCHASING:
                        status = "采购中";
                        break;
                    case PurchaseOrderStatus.PURCHASINGFINISH:
                        status = "采购完毕";
                        break;
                    case PurchaseOrderStatus.APPROVALFAILED:
                        status = "审批不通过，否决";
                        break;
                    case PurchaseOrderStatus.APPROVALING:
                        status = "审批中";
                        break;
                    case PurchaseOrderStatus.APPROVALANDRETURN:
                        status = "返回修改";
                        break;
                    case PurchaseOrderStatus.DRAFT:
                        status = "草稿";
                        break;
                    case PurchaseOrderStatus.REAPPROVALING:
                        status = "重新审批";
                        break;

                  
                    case PurchaseOrderStatus.TERMINATED:
                        status = "采购中止";
                        break;
                    case PurchaseOrderStatus.WAITING4APPROVAL:
                        status = "等待审批";
                        break;
                    case PurchaseOrderStatus.WAITING4PURCHASE:
                        status = "审批通过，等待采购";
                        break;
                    
                    default :
                        status = "未知状态";
                        break;
                }
                return status;
            }
        }

           /// <summary>
           /// 是否可以编辑，处于草稿或者返回修改的，可以修改
           /// </summary>
        public bool CanEdit
        {
            get
            {
                return _status == PurchaseOrderStatus.DRAFT || _status == PurchaseOrderStatus.APPROVALANDRETURN;
            }
        }

        /// <summary>
        /// 是否可以删除，只有处于草稿状态的，才能删除
        /// </summary>
        public bool CanDelete
        {
            get
            {
                return _status == PurchaseOrderStatus.DRAFT;
            }
        }

        /// <summary>
        /// 是否可以终止，等待审批或者返回修改的单，可以被终止
        /// </summary>
        public bool CanStop
        {
            get
            {
                return _status == PurchaseOrderStatus.APPROVALANDRETURN
                    ||
                    _status == PurchaseOrderStatus.WAITING4APPROVAL;
            }
        }


        /// <summary>
        /// 描述 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark==null?"":_remark; }
        }
        /// <summary>
        /// 子采购单序号
        /// </summary>
        public short SubOrderIndex
        {
            set { _suborderindex = value; }
            get { return _suborderindex; }
        }
        #endregion Model

        /// <summary>
        /// 当前正在审批的用户
        /// </summary>
        public string Approvaling
        {
            set { _approvaling = value; }
            get { return _approvaling==null?"":_approvaling; }
        }

        /// <summary>
        /// 审批过的用户，以"|"隔开，按照顺序存放
        /// </summary>
        public string Approvalers
        {
            set { _approvalers = value; }
            get { return _approvalers==null?"":_approvalers; }
        }

        private IList _detaillist = new List<PurchaseOrderDetailInfo>();

        /// <summary>
        /// 详情列表
        /// </summary>
        public IList DetailList
        {
            get { return _detaillist; }
            set { _detaillist = value; }
        }


        private IList _approvallist = new List<PurchaseOrderApprovalInfo>();
        /// <summary>
        /// 审批列表
        /// </summary>
        public IList ApprovalList
        {
            get { return _approvallist; }
            set { _approvallist = value; }
        }

        /// <summary>
        /// 修改后的快照
        /// </summary>
        private IList _modifyrecordlist = new List<PurchaseOrderModifyInfo>();
        /// <summary>
        /// 修改后的快照，不包含与当前
        /// </summary>
        public IList ModifyRecordList
        {
            get { return _modifyrecordlist; }
            set { _modifyrecordlist = value; }
        }

        /// <summary>
        /// 修改后的快照，只包含提交的
        /// </summary>
        public IList ModifyRecordSubmitList
        {
            get { 
                IList submit = new List<PurchaseOrderModifyInfo>();

                foreach (PurchaseOrderModifyInfo record in _modifyrecordlist)
                {
                    if (record.ModifyType == PurchaseOrderModifyType.SUBMIT)
                        submit.Add(record);
                }

                return submit;
            }
            
        }

        private PurchaseOrderModifyInfo _modifyinfo;
        /// <summary>
        /// 修改信息，只用于申请者进行保存或者提交操作
        /// </summary>
        public PurchaseOrderModifyInfo ModifyInfo
        {
            get { return _modifyinfo; }
            set { _modifyinfo = value; }
        }

        /// <summary>
        /// 判断是否能够被userid的用户调整
        /// </summary>
        /// <param name="userid">用户名</param>
        /// <returns>是否能调整</returns>
        public bool CanBeAdjust(string userid)
        {
            if (Approvaling!=null && Approvaling.Trim().ToLower()== userid.ToLower())
                return true;
            else
                return false;
        }

        /// <summary>
        /// 返回第一个审批者，如果没有，则返回""
        /// </summary>
        public string FirstApprovaler
        {
            get
            {
                string first = "";
                if (_approvalers == null || _approvalers.Trim() == "")
                    return first;
                else
                {
                    string[] array = _approvalers.Split('|');
                    if (array.Length > 0)
                        return array[0];
                }
                return first;
            }
        }

        private IList _relatedorders = new List<PurchaseOrderInfo>();
        /// <summary>
        /// 相关的采购单，即PurchaseOrderID相同的采购单
        /// </summary>
        public IList RelatedOrders
        {
            get { return _relatedorders; }
            set { _relatedorders = value; }
        }

        private string _workflowstatename;
        /// <summary>
        /// 工作流状态
        /// </summary>
        public string WorkFlowStateName
        {
            get { return _workflowstatename; }
            set { _workflowstatename = value; }
        }

        private string _workflowstatedescription;
        /// <summary>
        /// 工作流状态描述
        /// </summary>
        public string WorkFlowStateDescription
        {
            get { return _workflowstatedescription; }
            set { _workflowstatedescription = value; }
        }

        private string _workflowinstanceid;
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public string WorkFlowInstanceID
        {
            get { return _workflowinstanceid; }
            set { _workflowinstanceid = value; }
        }
        /// <summary>
        /// 下一处理用户
        /// </summary>
        public string NextUserName{get;set;}
/// <summary>
/// 下一处理用户姓名
/// </summary>
        public string NextUserPersonName { get; set; }
        /// <summary>
        /// 下一处理用户职位
        /// </summary>
        public string NextUserPositionName { get; set; }
        /// <summary>
        /// 下一处理用户部门ID
        /// </summary>
        public long NextUserDepartmentID { get; set; }

        /// <summary>
        /// 下一处理用户部门名称
        /// </summary>
        public string NextUserDepartmentName { get; set; }

        /// <summary>
        /// 下一处理代理用户
        /// </summary>
        public string DelegateUserName { get; set; }
        /// <summary>
        /// 下一处理代理用户姓名
        /// </summary>
        public string DelegateUserPersonName { get; set; }
        /// <summary>
        /// 下一处理代理用户职位
        /// </summary>
        public string DelegateUserPositionName { get; set; }
        /// <summary>
        /// 下一处理代理用户部门ID
        /// </summary>
        public long DelegateUserDepartmentID { get; set; }

        /// <summary>
        /// 下一处理代理用户部门名称
        /// </summary>
        public string DelegateUserDepartmentName { get; set; }
    }

    /// <summary>
    /// 采购单排序比较--申请人
    /// </summary>
    public class PurchaseOrderComparer : IComparer
    {
        #region IComparer 成员

        int IComparer.Compare(object x, object y)
        {
            PurchaseOrderInfo a = (PurchaseOrderInfo)x;
            PurchaseOrderInfo b = (PurchaseOrderInfo)y;
            if (a.Status == PurchaseOrderStatus.DRAFT && b.Status != PurchaseOrderStatus.DRAFT)
            {
                return -1;
            }
            else
                if (a.Status != PurchaseOrderStatus.DRAFT && b.Status == PurchaseOrderStatus.DRAFT)
                {
                    return 1;
                }
                else
                    return b.UpdateTime.CompareTo(a.UpdateTime);

        }

        #endregion
    }



}
