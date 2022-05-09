using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Maintain
{
    public class DailyPatrolTrackInfo
    {
        #region Model
        private long _recordid;

        private DateTime _startdate;
        private DateTime _enddate;

        private DailyPatrolVerifiedResult _verifiedresult;
        private string _verifyby;
        private string _verifybyname;
        private string _verifyremark;
        private DateTime _patroldate;
        private string _equipmentno;
        private long _planid;
        private long _itemid;
        private string _patrolobject;
        private string _patrolcontent;
        private string _patrolresult;
        private string _patrolmanid;
        private string _patrolmanname;
        /// <summary>
        /// 
        /// </summary>
        public long RecordID
        {
            set { _recordid = value; }
            get { return _recordid; }
        }
        /// <summary>
        /// 按计划该执行的时间段开始时间
        /// </summary>
        public DateTime StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 按计划该执行的时间段结束时间
        /// </summary>
        public DateTime EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
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
        public string PatrolDateString
        {
            get {
                if(_patroldate.Equals(DateTime.MinValue))
                    return string.Empty;
                else
                    return _patroldate.ToString();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EquipmentNO
        {
            set { _equipmentno = value; }
            get { return _equipmentno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long PlanID
        {
            set { _planid = value; }
            get { return _planid; }
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
            get {
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
        public string PlanDateString
        {
            get
            {
                return _startdate.ToShortDateString() + " 至 " + _enddate.ToShortDateString();
            }
        }
    }
}
