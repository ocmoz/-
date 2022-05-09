using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Maintain
{
    public enum RoutineInspectVerifiedResult
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
        CompletedAsPlanned = 3
    }
    public class RoutineInspectRecordInfo
    {
        #region Model
        private long _recordid;
        private RoutineInspectVerifiedResult _verifiedresult;
        private string _verifyby;
        private string _verifybyname;
        private string _verifyremark;
        private DateTime _Inspectdate;
        private string _equipmentno;
        private long _planid;
        private long _itemid;
        private string _Inspectobject;
        private string _Inspectcontent;
        private string _Inspectresult;
        private string _Inspectmanid;
        private string _Inspectmanname;
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
        public RoutineInspectVerifiedResult VerifiedResult
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
                    case RoutineInspectVerifiedResult.NotImplemented: str = "未执行"; break;
                    case RoutineInspectVerifiedResult.CompletedAsPlanned: str = "按计划执行"; break;
                    case RoutineInspectVerifiedResult.NotCompleted: str = "未按计划执行"; break;
                    case RoutineInspectVerifiedResult.NotVerified: str = "未审核"; break;
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
        public DateTime InspectDate
        {
            set { _Inspectdate = value; }
            get { return _Inspectdate; }
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
        public string InspectObject
        {
            set { _Inspectobject = value; }
            get { return _Inspectobject; }
        }
        /// <summary>
        /// 显示于列表之中
        /// </summary>
        public string InspectObjectString
        {
            get
            {
                if (_Inspectobject.Length > 10)
                    return _Inspectobject.Substring(0, 10) + "...";
                else
                    return _Inspectobject;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InspectContent
        {
            set { _Inspectcontent = value; }
            get { return _Inspectcontent; }
        }
        /// <summary>
        /// 显示于列表之中
        /// </summary>
        public string InspectContentString
        {
            get
            {
                if (_Inspectcontent.Length > 10)
                    return _Inspectcontent.Substring(0, 10) + "...";
                else
                    return _Inspectcontent;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InspectResult
        {
            set { _Inspectresult = value; }
            get { return _Inspectresult; }
        }
        /// <summary>
        /// 显示于列表之中
        /// </summary>
        public string InspectResultString
        {
            get
            {
                if (_Inspectresult.Length > 10)
                    return _Inspectresult.Substring(0, 10) + "...";
                else
                    return _Inspectresult;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InspectmanID
        {
            set { _Inspectmanid = value; }
            get { return _Inspectmanid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InspectmanName
        {
            set { _Inspectmanname = value; }
            get { return _Inspectmanname; }
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
        private string _equipmentnamemodel;
        public string EquipmentNameModel
        {
            set { _equipmentnamemodel = value; }
            get { return _equipmentnamemodel; }
        }
    }
}
