﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using FM2E.Model.Utils;
using System.IO;

namespace FM2E.Model.Maintain
{
    /// <summary>
    /// 故障处理单状态
    /// </summary>
    public enum MalfunctionHandleStatus
    {
        //********** Modified by Xue    For ShenGaoSu    2011-11-28 ********************************************************************************
        //[EnumDescription("未知状态")]
        //Unknown = 0,
        //[EnumDescription("等待维修队受理")]
        //Waiting4Accept = 2,
        //[EnumDescription("维修队已受理")]
        //Accepted = 4,
        //[EnumDescription("等待维修资金审批")]
        //Waiting4MoneyApproval = 8,
        //[EnumDescription("维修资金审批通过")]
        //MoneyApprovalPassed = 16,
        //[EnumDescription("维修资金审批不通过")]
        //MoneyApprovalFailed = 32,
        //[EnumDescription("功能性修复")]
        //FunctionalityRestore = 64,
        //[EnumDescription("故障完全修复")]
        //Fixed = 128,
        //[EnumDescription("已结束")]
        //Finished = 256,
        //[EnumDescription("已撤单")]
        //Canceled=512,
        //[EnumDescription("未修复")]
        //NotRepaired=126,
        //[EnumDescription("已修复")]
        //Repaired = 384

        [EnumDescription("未知状态")]
        Unknown = 0,
        [EnumDescription("等待维修队受理")]
        Waiting4Accept = 2,
        [EnumDescription("等待维护员确认时间")]
        Accepted = 4,
        [EnumDescription("延迟审批")]
        Delay = 6,
        [EnumDescription("等待维护队登记")]
        Waiting4Mark = 10,
        [EnumDescription("返回修改")]
        ReturnModify = 8,
        [EnumDescription("等待故障验收")]
        Waiting4Check = 16,

        [EnumDescription("审批流程")]
        Waiting4MoneyApproval = 32,
        //[EnumDescription("返回修改")]
        //ReturnModify = 32,
        //[EnumDescription("审批不通过")]
        //MoneyApprovalFailed = 64,

        [EnumDescription("功能性修复")]
        FunctionalityRestore = 64,
        [EnumDescription("故障完全修复")]
        Fixed = 128,
        [EnumDescription("已结束")]
        Finished = 256,
        [EnumDescription("等待撤单审批")]
        Wait4Canceled = 512,
        [EnumDescription("等待值班站长验收")]
        AnotherCheck = 1024,
        [EnumDescription("等待自维工程师确定")]
        InteriorEngineerCheck = 2048,
        [EnumDescription("等待自维工程师验收")]
        Wait4EngineerCheck = 4096,
        [EnumDescription("等待维护员验收")]
        Waiting4ReporterCheck = 8192,
        [EnumDescription("已撤单")]
        Canceled = 16384,
        [EnumDescription("未修复")]
        NotRepaired = 6750,  //62
        [EnumDescription("已修复")]
        Repaired = 9632


        //********** Modification Finished 2011-11-28 **********************************************************************************************
    }

    /// <summary>
    /// 评分等级
    /// </summary>
    public enum Grade
    {
        [EnumDescription("未知等级")]
        Unknown = 0,
        [EnumDescription("优")]
        Excellent = 2,
        [EnumDescription("良")]
        Good = 4,
        [EnumDescription("中")]
        Medium = 8,
        [EnumDescription("差")]
        Bad = 16
    }
    /// <summary>
    /// 故障原因
    /// </summary>

    public enum MalfunctionReason
    {
        [EnumDescription("未知原因")]
        Unknown = 0,
        [EnumDescription("常规故障")]
        CommonMalfunction = 1,
        [EnumDescription("雷击")]
        Lightning = 2,
        [EnumDescription("被盗破坏")]
        Stolen = 4,
        [EnumDescription("交通事故")]
        TrafficAccident = 8,
        [EnumDescription("其他")]          //3.8
        Others = 16                         //3.8
    }
    /// <summary>
    /// 计量与否
    /// </summary>

    public enum MalfunctionMeasure
    {
        [EnumDescription("未知")]
        Unknown = 0,
        [EnumDescription("需要计量")]
        measure = 1,
        [EnumDescription("不需要计量")]
        dismeasure = 2
                          
    }
    /// <summary>
    /// 计量与否
    /// </summary>

    public enum ScrapReason
    {
        [EnumDescription("未知")]
        Unknown = 0,
        [EnumDescription("设备的修复成本过高（使用3年以上的设备修理费超过设备原值的30%）")]
        Reason1 = 1,
        [EnumDescription("设备使用不到3年，但设备修理费超过设备原值的50%")]
        Reason2 = 2,
        [EnumDescription("易耗件")]
        Reason3 = 4,
        [EnumDescription("技术落后、耗能过高或工作效率很低无修复价值，存在因雷击而造成的明显烧焦痕迹")]
        Reason4 = 8,
        [EnumDescription("不符合国家规范标准需要强制淘汰")]
        Reason5 = 16,
        [EnumDescription("其他原因")]
        Reason6 = 32
                          
                          
    }
    /// <summary>
    /// 故障处理单实体类
    /// </summary>
    [Serializable]
    public class MalfunctionHandleInfo
    {
        #region model
        private long _sheetid;
        private string _recorder="";
        private string _recordername;
        private long _maintaindept;
        private MalfunctionRank _malfunctionrank;
        private int _responsetime;
        private TimeUnits _responseunit;
        private int _actualresponsetime;
        private int _funrestoretime;
        private TimeUnits _funrestoreunit;
        private int _actualfunrestoretime;
        private int _repairtime;
        private TimeUnits _repairunit;
        private int _actualrepairtime;
        private string _receiver = "";
        private string _sheetno = "";
        private DateTime _receivedate;
        private Grade _effect;
        private Grade _technicevaluate;
        private Grade _attitude;
        private Grade _rationality;
        private string _feeback = "";
        private DateTime _updatetime;
        private string _companyid = "";
        private long _departmentid;
        private string _departmentname;
        private string _reporter = "";
        private DateTime _reportdate;
        private DateTime _reportdate2;
        private long _addressid;
        private string _addressdetail = "";
        private string _malfunctiondescription = "";
        private long _recordDept;
        private string _recordDeptName;
        private MalfunctionReason _systemID;
        private MalfunctionHandleStatus _status;
        private string _maintainDeptName;
        private string _cancelReason="";
        private string _canceler="";
        private string _investigator="";
        private bool _isResponseInTime;
        private bool _isFunRestoreInTime;
        private bool _isRepairInTime;
        private IList _faultyEquipments;
        private bool _isDelivered;
        private string _noequipment;
        private Boolean _isintime;
        private string _editor;
        private string _editreason;
        private Boolean _stationcheck;
        private bool? _delayapply;
        private DateTime _cancelApplyTime;
        private string _cancelApproveName;
        private string _cancelApproveRemark;
        private DateTime _cancelApproveTime;
        private string _cancelApproveResult;
        private DateTime _delayApplyTime;
        private int _firstConsultTime;
        private TimeUnits _firstConsultUnit;
        private DateTime _firstApproveTime;
        private string _firstDelayRemark;
        private int _firstDelayApprove;
        private string _firstApproveName;
        private int _finalConsultTime;
        private TimeUnits _finalConsultUnit;
        private string _finalDelayRemark;
        private DateTime _finalAprroveTime;
        private int _finalDelayApprove;
        private string _finalApproveName;
        private string _timeConfirmer;
        private int _delayForTime;
        private TimeUnits _delayForUnit;
        private string _currentStateName;
        private string _statusDescription;
        private string _nextUserName;
        private string _nextApproverPersonName;

        private string _attachment;
        #endregion

        /// <summary>
        ///当前审批状态
        /// </summary>
        public string CurrentStateName
        {
            set { _currentStateName = value; }
            get { return _currentStateName; }
        }
        /// <summary>
        ///当前审批状态描述
        /// </summary>
        public string StatusDescription
        {
            set { _statusDescription = value; }
            get { return _statusDescription; }
        }
        /// <summary>
        ///下一审批用户
        /// </summary>
        public string NextUserName
        {
            set { _nextUserName = value; }
            get { return _nextUserName; }
        }
        /// <summary>
        ///下一审批用户名字
        /// </summary>
        public string NextApproverPersonName
        {
            set { _nextApproverPersonName = value; }
            get { return _nextApproverPersonName; }
        }
        /// <summary>
        /// 故障原因
        /// </summary>
        public MalfunctionReason SystemID
        {
            set { _systemID = value; }
            get { return _systemID; }
        }

        public string Editor
        {
            set { _editor = value; }
            get { return _editor; }
        }

        public string Editreason
        {
            set { _editreason = value; }
            get { return _editreason; }
        }


        public Boolean IsInTime
        {
            set { _isintime = value; }
            get { return _isintime; }
        }
        /// <summary>
        /// 非设备类故障
        /// </summary>
        public string NoEquipment
        {
            set { _noequipment = value; }
            get { return _noequipment; }
        }

        /// <summary>
        /// 故障处理单流水号
        /// </summary>
        public long SheetID
        {
            set { _sheetid = value; }
            get { return _sheetid; }
        }
        /// <summary>
        /// 故障记录人用户名
        /// </summary>
        public string Recorder
        {
            set { _recorder = value; }
            get { return _recorder; }
        }
        /// <summary>
        /// 故障记录人名
        /// </summary>
        public string RecorderName
        {
            set { _recordername = value; }
            get { return _recordername; }
        }
        /// <summary>
        /// 维修队
        /// </summary>
        public long MaintainDept
        {
            set { _maintaindept = value; }
            get { return _maintaindept; }
        }
        /// <summary>
        /// 故障等级
        /// </summary>
        public MalfunctionRank MalfunctionRank
        {
            set { _malfunctionrank = value; }
            get { return _malfunctionrank; }
        }
        /// <summary>
        /// 响应时间
        /// </summary>
        public int ResponseTime
        {
            set { _responsetime = value; }
            get { return _responsetime; }
        }
        /// <summary>
        /// 响应时间单位
        /// </summary>
        public TimeUnits ResponseUnit
        {
            set { _responseunit = value; }
            get { return _responseunit; }
        }
        /// <summary>
        /// 响应时间，以分钟为单位
        /// </summary>
        public int ResponseTimeInMinutes
        {
            get
            {
                int result = 0;
                switch (_responseunit)
                {
                    case TimeUnits.Day:
                        result = _responsetime * 24 * 60;
                        break;
                    case TimeUnits.Hour:
                        result = _responsetime * 60;
                        break;
                    case TimeUnits.Minute:
                        result = _responsetime;
                        break;
                }
                return result;
            }
        }
        /// <summary>
        /// 实际响应时间（以分钟为单位）
        /// </summary>
        public int ActualResponseTime
        {
            set { _actualresponsetime = value; }
            get { return _actualresponsetime; }
        }
        /// <summary>
        /// 实际响应时间字符串
        /// </summary>
        public string ActualResponseTimeString
        {
            get
            {
                string str = "";

                int days = _actualresponsetime / (24 * 60);
                int remainder = _actualresponsetime % (24 * 60);
                int hours = remainder / 60;
                int minutes = remainder % 60;

                if (days != 0)
                {
                    str += days + "天";
                }
                if (hours != 0)
                {
                    str += hours + "小时";
                }
                else
                {
                    if (days == 0 && minutes == 0)
                        str += "0分钟";
                }
                if (minutes != 0)
                    str += minutes + "分钟";
                return str;
            }
        }
        /// <summary>
        /// 功能性恢复时间
        /// </summary>
        public int FunRestoreTime
        {
            set { _funrestoretime = value; }
            get { return _funrestoretime; }
        }
        /// <summary>
        /// 功能性恢复时间单位
        /// </summary>
        public TimeUnits FunRestoreUnit
        {
            set { _funrestoreunit = value; }
            get { return _funrestoreunit; }
        }
        /// <summary>
        /// 功能性恢复时间单位，以分钟为单位
        /// </summary>
        public int FunRestoreTimeInMinutes
        {
            get
            {
                int result = 0;
                switch (_funrestoreunit)
                {
                    case TimeUnits.Day:
                        result = _funrestoretime * 24 * 60;
                        break;
                    case TimeUnits.Hour:
                        result = _funrestoretime * 60;
                        break;
                    case TimeUnits.Minute:
                        result = _funrestoretime;
                        break;
                }
                return result;
            }
        }
        /// <summary>
        /// 实际功能性恢复时间（以分钟为单位）
        /// </summary>
        public int ActualFunRestoreTime
        {
            set { _actualfunrestoretime = value; }
            get { return _actualfunrestoretime; }
        }
        /// <summary>
        /// 实际功能性恢复时间字符串
        /// </summary>
        public string ActualFunRestoreTimeString
        {
            get
            {
                string str = "";

                int days = _actualfunrestoretime / (24 * 60);
                int remainder = _actualfunrestoretime % (24 * 60);
                int hours = remainder / 60;
                int minutes = remainder % 60;

                if (days != 0)
                {
                    str += days + "天";
                }
                if (hours != 0)
                {
                    str += hours + "小时";
                }
                else
                {
                    if (days == 0 && minutes == 0)
                        str += "0分钟";
                }
                if(minutes!=0)
                    str += minutes + "分钟";
                return str;
            }
        }
        /// <summary>
        /// 修复时间
        /// </summary>
        public int RepairTime
        {
            set { _repairtime = value; }
            get { return _repairtime; }
        }
        /// <summary>
        /// 修复时间单位
        /// </summary>
        public TimeUnits RepairUnit
        {
            set { _repairunit = value; }
            get { return _repairunit; }
        }
        /// <summary>
        /// 修复时间单位，以分钟为单位
        /// </summary>
        public int RepairTimeInMinutes
        {
            get
            {
                int result = 0;
                switch (_repairunit)
                {
                    case TimeUnits.Day:
                        result = _repairtime * 24 * 60;
                        break;
                    case TimeUnits.Hour:
                        result = _repairtime * 60;
                        break;
                    case TimeUnits.Minute:
                        result = _repairtime;
                        break;
                }
                return result;
            }
        }
        /// <summary>
        /// 实际修复时间（以分钟为单位）
        /// </summary>
        public int ActualRepairTime
        {
            set { _actualrepairtime = value; }
            get { return _actualrepairtime; }
        }
        /// <summary>
        /// 实际响应时间字符串
        /// </summary>
        public string ActualRepairTimeString
        {
            get
            {
                string str = "";

                int days = _actualrepairtime / (24 * 60);
                int remainder = _actualrepairtime % (24 * 60);
                int hours = remainder / 60;
                int minutes = remainder % 60;

                if (days != 0)
                {
                    str += days + "天";
                }
                if (hours != 0)
                {
                    str += hours + "小时";
                }
                else
                {
                    if (days == 0 && minutes == 0)
                        str += "0分钟";
                }
                if (minutes != 0)
                    str += minutes + "分钟";
                return str;
            }
        }
        /// <summary>
        /// 故障受理人
        /// </summary>
        public string Receiver
        {
            set { _receiver = value; }
            get { return _receiver; }
        }
        /// <summary>
        /// 故障处理单编号
        /// </summary>
        public string SheetNO
        {
            set { _sheetno = value; }
            get { return _sheetno; }
        }
        /// <summary>
        /// 故障受理时间
        /// </summary>
        public DateTime ReceiveDate
        {
            set { _receivedate = value; }
            get { return _receivedate; }
        }
        /// <summary>
        /// 处理效果
        /// </summary>
        public Grade Effect
        {
            set { _effect = value; }
            get { return _effect; }
        }
        /// <summary>
        /// 技术评价
        /// </summary>
        public Grade TechnicEvaluate
        {
            set { _technicevaluate = value; }
            get { return _technicevaluate; }
        }
        /// <summary>
        /// 工作态度 
        /// </summary>
        public Grade Attitude
        {
            set { _attitude = value; }
            get { return _attitude; }
        }
        /// <summary>
        /// 处理故障的合理性
        /// </summary>
        public Grade Rationality
        {
            set { _rationality = value; }
            get { return _rationality; }
        }
        /// <summary>
        /// 使用部门意见
        /// </summary>
        public string Feeback
        {
            set { _feeback = value; }
            get { return _feeback; }
        }
        /// <summary>
        /// 最近更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 故障公司ID
        /// </summary>
        public string CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 故障部门ID
        /// </summary>
        public long DepartmentID
        {
            set { _departmentid = value; }
            get { return _departmentid; }
        }
        /// <summary>
        /// 故障部门名称
        /// </summary>
        public string DepartmentName
        {
            set { _departmentname = value; }
            get { return _departmentname; }
        }
        /// <summary>
        /// 故障上报人
        /// </summary>
        public string Reporter
        {
            set { _reporter = value; }
            get { return _reporter; }
        }
        /// <summary>
        /// 故障上报时间
        /// </summary>
        public DateTime ReportDate
        {
            set { _reportdate = value; }
            get { return _reportdate; }
        }
        /// <summary>
        /// 故障上报时间2
        /// </summary>
        public DateTime ReportDate2
        {
            set { _reportdate2 = value; }
            get { return _reportdate2; }
        }
        /// <summary>
        /// 故障设备所在地址ID
        /// </summary>
        public long AddressID
        {
            set { _addressid = value; }
            get { return _addressid; }
        }
        /// <summary>
        /// 故障设备所在地址名称
        /// </summary>
        public string AddressName
        {
            get;
            set;
        }
        /// <summary>
        /// 详细地址描述(已变成上报故障设备条形码@系统ID)
        /// </summary>
        public string AddressDetail
        {
            set { _addressdetail = value; }
            get { return _addressdetail; }
        }
        /// <summary>
        /// 故障现象描述
        /// </summary>
        public string MalfunctionDescription
        {
            set { _malfunctiondescription = value; }
            get { return _malfunctiondescription; }
        }
        /// <summary>
        /// 故障记录部门ID
        /// </summary>
        public long RecordDept
        {
            set { _recordDept = value; }
            get { return _recordDept; }
        }
        /// <summary>
        /// 故障记录部门名称
        /// </summary>
        public string RecordDeptName
        {
            get { return _recordDeptName; }
            set { _recordDeptName = value; }
        }
       
        /// <summary>
        /// 表单状态
        /// </summary>
        public MalfunctionHandleStatus Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 维修队名称
        /// </summary>
        public string MaintainDeptName
        {
            set { _maintainDeptName = value; }
            get { return _maintainDeptName; }
        }
        /// <summary>
        /// 撤单人用户名
        /// </summary>
        public string Canceler
        {
            set { _canceler = value; }
            get { return _canceler; }
        }
        /// <summary>
        /// 撤单原因
        /// </summary>
        public string CancelReason
        {
            set { _cancelReason = value; }
            get { return _cancelReason; }
        }
        /// <summary>
        /// 故障处理满意度调查人用户名
        /// </summary>
        public string Investigator
        {
            set { _investigator = value; }
            get { return _investigator; }
        }
        /// <summary>
        /// 响应是否及时
        /// </summary>
        public bool IsResponseInTime
        {
            get { return _isResponseInTime; }
            set { _isResponseInTime = value; }
        }
        /// <summary>
        /// 功能恢复是否及时
        /// </summary>
        public bool IsFunRestoreInTime
        {
            get { return _isFunRestoreInTime; }
            set { _isFunRestoreInTime = value; }
        }
        /// <summary>
        /// 修复是否及时
        /// </summary>
        public bool IsRepairInTime
        {
            get { return _isRepairInTime; }
            set { _isRepairInTime = value; }
        }
        /// <summary>
        /// 故障设备列表
        /// </summary>
        public IList FaultyEquipments
        {
            set { _faultyEquipments = value; }
            get { return _faultyEquipments; }
        }
        /// <summary>
        /// 是否已打印
        /// </summary>
        public bool IsPrinted
        {
            get;
            set;
        }
        /// <summary>
        /// 是否送修
        /// </summary>
        public bool IsDelivered
        {
            get { return _isDelivered; }
            set { _isDelivered = value; }
        }
        /// </summary>
        /// 自维工程师确认
        /// </summary>
        public bool Stationcheck
        {
            get { return _stationcheck; }
            set { _stationcheck = value; }
        }
        /// <summary>
        /// 
        /// 延时申请
        /// </summary>
        public bool? IsDelayApply
        {
            get { return _delayapply; }
            set { _delayapply = value; }
        }
        /// <summary>
        ///撤单申请时间
        /// </summary>
        public DateTime CancelApplyTime
        {
            get { return _cancelApplyTime; }
            set { _cancelApplyTime = value; }
        }
        /// <summary>
        /// 撤单申请人
        /// </summary>
        public string CancelApproveName
        {
            get { return _cancelApproveName; }
            set { _cancelApproveName = value; }
        }
        /// <summary>
        /// 撤单审批
        /// </summary>
        public string CancelApproveRemark
        {
            get { return _cancelApproveRemark; }
            set { _cancelApproveRemark = value; }
        }
        /// <summary>
        /// 撤单审批时间
        /// </summary>
        public DateTime CancelApproveTime
        {
            get { return _cancelApproveTime; }
            set { _cancelApproveTime = value; }
        }
        /// <summary>
        /// 撤单审批结果
        /// </summary>
        public string CancelApproveResult
        {
            get { return _cancelApproveResult; }
            set { _cancelApproveResult = value; }
        }
        /// <summary>
        /// 延迟申请时间
        /// </summary>
        public DateTime DelayApplyTime
        {
            get { return _delayApplyTime; }
            set { _delayApplyTime = value; }
        }
        /// <summary>
        /// 延迟工程师协商时间
        /// </summary>
        public int FirstConsultTime
        {
            get { return _firstConsultTime; }
            set { _firstConsultTime = value; }
        }
        /// <summary>
        /// 延迟工程师协商单位
        /// </summary>
        public TimeUnits FirstConsultUnit
        {
            get { return _firstConsultUnit; }
            set { _firstConsultUnit = value; }
        }
        /// <summary>
        /// 延迟工程师批复时间
        /// </summary>
        public DateTime FirstApproveTime
        {
            get { return _firstApproveTime; }
            set { _firstApproveTime = value; }
        }
        /// <summary>
        /// 延迟工程师备注
        /// </summary>
        public string FirstDelayRemark
        {
            get { return _firstDelayRemark; }
            set { _firstDelayRemark = value; }
        }
        /// <summary>
        /// 延迟工程师批复结果
        /// </summary>
        public int FirstDelayApprove
        {
            get { return _firstDelayApprove; }
            set { _firstDelayApprove = value; }
        }
        /// <summary>
        /// 延迟工程师账号
        /// </summary>
        public string FirstApproveName
        {
            get { return _firstApproveName; }
            set { _firstApproveName = value; }
        }
        /// <summary>
        /// 延迟经理协商时间
        /// </summary>
        public int FinalConsultTime
        {
            get { return _finalConsultTime; }
            set { _finalConsultTime = value; }
        }
        /// <summary>
        /// 延迟经理协商时间单位
        /// </summary>
        public TimeUnits FinalConsultUnit
        {
            get { return _finalConsultUnit; }
            set { _finalConsultUnit = value; }
        }
        /// <summary>
        /// 延迟经理备注
        /// </summary>
        public string FinalDelayRemark
        {
            get { return _finalDelayRemark; }
            set { _finalDelayRemark = value; }
        }
        /// <summary>
        /// 延迟工程师审批时间
        /// </summary>
        public DateTime FinalAprroveTime
        {
            get { return _finalAprroveTime; }
            set { _finalAprroveTime = value; }
        }
        /// <summary>
        /// 延迟经理审批结果
        /// </summary>
        public int FinalDelayApprove
        {
            get { return _finalDelayApprove; }
            set { _finalDelayApprove = value; }
        }
        /// <summary>
        /// 延迟经理账号
        /// </summary>
        public string FinalApproveName
        {
            get { return _finalApproveName; }
            set { _finalApproveName = value; }
        }
        /// <summary>
        ///确认修复时间签名
        /// </summary>
        public string TimeConfirmer
        {
            get { return _timeConfirmer; }
            set { _timeConfirmer = value; }
        }
        /// <summary>
        /// 延迟申请时间
        /// </summary>
        public int DelayForTime
        {
            get { return _delayForTime; }
            set { _delayForTime = value; }
        }
        /// <summary>
        /// 延迟申请时间单位
        /// </summary>
        public TimeUnits DelayForUnit
        {
            get { return _delayForUnit ; }
            set { _delayForUnit = value; }
        }

        /// <summary>
        /// 返回一个内存副本(深层副本)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static MalfunctionHandleInfo CloneObject(MalfunctionHandleInfo obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                stream.Position = 0;
                formatter.Serialize(stream, obj);
                stream.Position = 0;
                return (MalfunctionHandleInfo)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// 附件
        /// </summary>
        public string attachment
        {
            get { return _attachment; }
            set { _attachment = value; }
        }
    }
}
