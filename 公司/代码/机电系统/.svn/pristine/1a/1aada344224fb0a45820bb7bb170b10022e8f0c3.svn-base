using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Maintain
{
    public class MaintainPlanTrackInfo
    {
        #region Model
        private long _recordid;

        private DateTime _startdate;
        private DateTime _enddate;

        private MaintainPlanVerifiedResult _verifiedresult;
        private string _verifyby;
        private string _verifybyname;
        private string _verifyremark;
        private DateTime _recorddate;
        private string _equipmentno;
        private long _planid;
        private long _itemid;
        private string _recordobject;
        private string _recordcontent;
        private string _recordresult;
        private string _recordmanid;
        private string _recordmanname;
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
        public string RecordDateString
        {
            get
            {
                if (_recorddate.Equals(DateTime.MinValue))
                    return string.Empty;
                else
                    return _recorddate.ToString();
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
