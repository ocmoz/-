using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Maintain
{
    public enum RoutineMaintainVerifiedResult
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
    public class RoutineMaintainRecordInfo
    {
        #region Model
        private long _recordid;
        private RoutineMaintainVerifiedResult _verifiedresult;
        private string _verifyby;
        private string _verifybyname;
        private string _verifyremark;
        private DateTime _Maintaindate;
        private string _equipmentno;
        private long _planid;
        private long _itemid;
        private string _Maintainobject;
        private string _Maintaincontent;
        private string _Maintainresult;
        private string _Maintainmanid;
        private string _Maintainmanname;
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
        public RoutineMaintainVerifiedResult VerifiedResult
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
                    case RoutineMaintainVerifiedResult.NotImplemented: str = "未执行"; break;
                    case RoutineMaintainVerifiedResult.CompletedAsPlanned: str = "按计划执行"; break;
                    case RoutineMaintainVerifiedResult.NotCompleted: str = "未按计划执行"; break;
                    case RoutineMaintainVerifiedResult.NotVerified: str = "未审核"; break;
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
        public DateTime MaintainDate
        {
            set { _Maintaindate = value; }
            get { return _Maintaindate; }
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
        public string MaintainObject
        {
            set { _Maintainobject = value; }
            get { return _Maintainobject; }
        }
        /// <summary>
        /// 显示于列表之中
        /// </summary>
        public string MaintainObjectString
        {
            get
            {
                if (_Maintainobject.Length > 10)
                    return _Maintainobject.Substring(0, 10) + "...";
                else
                    return _Maintainobject;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MaintainContent
        {
            set { _Maintaincontent = value; }
            get { return _Maintaincontent; }
        }
        /// <summary>
        /// 显示于列表之中
        /// </summary>
        public string MaintainContentString
        {
            get
            {
                if (_Maintaincontent.Length > 10)
                    return _Maintaincontent.Substring(0, 10) + "...";
                else
                    return _Maintaincontent;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MaintainResult
        {
            set { _Maintainresult = value; }
            get { return _Maintainresult; }
        }
        /// <summary>
        /// 显示于列表之中
        /// </summary>
        public string MaintainResultString
        {
            get
            {
                if (_Maintainresult.Length > 10)
                    return _Maintainresult.Substring(0, 10) + "...";
                else
                    return _Maintainresult;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MaintainmanID
        {
            set { _Maintainmanid = value; }
            get { return _Maintainmanid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MaintainmanName
        {
            set { _Maintainmanname = value; }
            get { return _Maintainmanname; }
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
