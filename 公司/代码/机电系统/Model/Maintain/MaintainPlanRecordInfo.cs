using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Maintain
{
    public enum MaintainPlanVerifiedResult
    {
        /// <summary>
        /// 未执行
        /// </summary>
        NotImplemented = 0,
        /// <summary>
        /// 未审核
        /// </summary>
        NotVerified = 1,
        /// <summary>
        /// 未按计划执行
        /// </summary>
        NotCompleted = 2,
        /// <summary>
        /// 按计划执行
        /// </summary>
        CompletedAsPlanned = 3,
        /// <summary>
        /// 已审核
        /// </summary>
        Verified = 4
    }
    public class MaintainPlanRecordInfo
    {
        #region Model
        private long _recordid;
        private MaintainPlanVerifiedResult _verifiedresult;
        private string _verifyby;
        private string _verifybyname;
        private string _verifyremark;
        private DateTime _recorddate;
        public string SystemID { get; set; }
        public long SubSystemID { get; set; }
        public string SystemName { get; set; }
        public string SubSystemName { get; set; }
        private long _itemid;
        private string _recordobject;
        private string _recordcontent;
        private string _recordresult;
        private string _recordmanid;
        private string _recordmanname;
        private string _recordremark;
        private MaintainPlanType _plantype;
        /// <summary>
        /// 
        /// </summary>
        public long RecordID
        {
            set { _recordid = value; }
            get { return _recordid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public MaintainPlanVerifiedResult VerifiedResult
        {
            set { _verifiedresult = value; }
            get { return _verifiedresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string VerifiedResultString
        {
            get
            {
                string str = string.Empty;
                switch (_verifiedresult)
                {
                    case MaintainPlanVerifiedResult.NotImplemented: str = "未执行"; break;
                    case MaintainPlanVerifiedResult.CompletedAsPlanned: str = "按计划执行"; break;
                    case MaintainPlanVerifiedResult.NotCompleted: str = "未按计划执行"; break;
                    case MaintainPlanVerifiedResult.NotVerified: str = "未审核"; break;
                }
                return str;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string VerifyBy
        {
            set { _verifyby = value; }
            get { return _verifyby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string VerifyName
        {
            set { _verifybyname = value; }
            get { return _verifybyname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string VerifyRemark
        {
            set { _verifyremark = value; }
            get { return _verifyremark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime RecordDate
        {
            set { _recorddate = value; }
            get { return _recorddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ItemID
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RecordObject
        {
            set { _recordobject = value; }
            get { return _recordobject; }
        }
        /// <summary>
        /// 显示于列表之中
        /// </summary>
        public string RecordObjectString
        {
            get
            {
                if (_recordobject.Length > 10)
                    return _recordobject.Substring(0, 10) + "...";
                else
                    return _recordobject;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RecordContent
        {
            set { _recordcontent = value; }
            get { return _recordcontent; }
        }
        /// <summary>
        /// 显示于列表之中
        /// </summary>
        public string RecordContentString
        {
            get
            {
                if (_recordcontent.Length > 10)
                    return _recordcontent.Substring(0, 10) + "...";
                else
                    return _recordcontent;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RecordResult
        {
            set { _recordresult = value; }
            get { return _recordresult; }
        }
        /// <summary>
        /// 显示于列表之中
        /// </summary>
        public string RecordResultString
        {
            get
            {
                if (_recordresult.Length > 10)
                    return _recordresult.Substring(0, 10) + "...";
                else
                    return _recordresult;
            }
        }
        public string RecordRemark
        {
            set { _recordremark = value; }
            get { return _recordremark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RecordmanID
        {
            set { _recordmanid = value; }
            get { return _recordmanid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RecordmanName
        {
            set { _recordmanname = value; }
            get { return _recordmanname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public MaintainPlanType PlanType
        {
            set { _plantype = value; }
            get { return _plantype; }
        }

        #endregion Model

        private string _companyid;
        public string CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        private IList _equipmentList;
        public IList EquipmentList
        {
            set { _equipmentList = value; }
            get { return _equipmentList; }
        }
        private long _addressid;
        public long AddressID
        {
            set { _addressid = value; }
            get { return _addressid; }
        }
        private string _addressname;
        public string AddressName
        {
            set { _addressname = value; }
            get { return _addressname; }
        }
        private string _equipmentnamemodel;
        public string EquipmentNameModel
        {
            set { _equipmentnamemodel = value; }
            get { return _equipmentnamemodel; }
        }
        /// <summary>
        /// 计划类型字符串
        /// </summary>
        public string PlanTypeString
        {
            get
            {
                string typestring = "";
                switch (_plantype)
                {
                    case MaintainPlanType.DailyPatrol:
                        typestring = "日常巡查";
                        break;
                    case MaintainPlanType.RoutineInspect:
                        typestring = "例行检测";
                        break;
                    case MaintainPlanType.RoutineMaintain:
                        typestring = "例行保养";
                        break;
                    default:
                        typestring = "未知";
                        break;
                }
                return typestring;
            }
        }
    }
}
