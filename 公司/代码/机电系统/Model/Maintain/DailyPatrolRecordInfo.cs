using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Maintain
{
    public enum DailyPatrolVerifiedResult
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
        Verified=4
    }
    public class DailyPatrolRecordInfo
    {
        #region Model
        private long _recordid;
        private DailyPatrolVerifiedResult _verifiedresult;
        private string _verifyby;
        private string _verifybyname;
        private string _verifyremark;
        private DateTime _patroldate;
        private long _itemid;
        private string _patrolobject;
        private string _patrolcontent;
        private string _patrolresult;
        private string _patrolmanid;
        private string _patrolmanname;
        private string _patrolremark;
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
        public DailyPatrolVerifiedResult VerifiedResult
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
                    case DailyPatrolVerifiedResult.NotImplemented: str = "未执行"; break;
                    case DailyPatrolVerifiedResult.CompletedAsPlanned: str = "按计划执行"; break;
                    case DailyPatrolVerifiedResult.NotCompleted: str = "未按计划执行"; break;
                    case DailyPatrolVerifiedResult.NotVerified: str = "未审核"; break;
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
        public DateTime PatrolDate
        {
            set { _patroldate = value; }
            get { return _patroldate; }
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
        public string PatrolObject
        {
            set { _patrolobject = value; }
            get { return _patrolobject; }
        }
        /// <summary>
        /// 显示于列表之中
        /// </summary>
        public string PatrolObjectString
        {
            get
            {
                if (_patrolobject.Length > 10)
                    return _patrolobject.Substring(0, 10) + "...";
                else
                    return _patrolobject;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PatrolContent
        {
            set { _patrolcontent = value; }
            get { return _patrolcontent; }
        }
        /// <summary>
        /// 显示于列表之中
        /// </summary>
        public string PatrolContentString
        {
            get
            {
                if (_patrolcontent.Length > 10)
                    return _patrolcontent.Substring(0, 10) + "...";
                else
                    return _patrolcontent;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PatrolResult
        {
            set { _patrolresult = value; }
            get { return _patrolresult; }
        }
        /// <summary>
        /// 显示于列表之中
        /// </summary>
        public string PatrolResultString
        {
            get
            {
                if (_patrolresult.Length > 10)
                    return _patrolresult.Substring(0, 10) + "...";
                else
                    return _patrolresult;
            }
        }
        public string PatrolRemark
        {
            set { _patrolremark = value; }
            get { return _patrolremark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PatrolmanID
        {
            set { _patrolmanid = value; }
            get { return _patrolmanid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PatrolmanName
        {
            set { _patrolmanname = value; }
            get { return _patrolmanname; }
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
    }
}
