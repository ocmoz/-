using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.Model.Examine
{
    /// <summary>
    /// 考核表状态
    /// </summary>
    public enum ExamineSheetStatus
    {
        [EnumDescription("未知状态")]
        Unknown=0,
        [EnumDescription("草稿")]
        Draft=2,
        [EnumDescription("等待考核小组确认")]
        Waiting4ExamineConfirm = 4,
        [EnumDescription("确认通过")]
        ExamineConfirmPassed = 8,
        [EnumDescription("确认不通过")]
        ExamineConfirmNotPassed = 16
    }
    /// <summary>
    /// 考核表实体类
    /// </summary>
    public class ExamineInfo
    {
        #region Model
        private long _examsheetid;
        private string _examineconfirmer;
        private ExamineConfirmResult _examineconfirmresult;
        private string _examineconfirmremark;
        private DateTime _examinerconfirmdate;
        private string _targetconfirmer;
        private ExamineConfirmResult _targetconfirmresult;
        private string _targetconfirmremark;
        private DateTime _targetconfirmdate;
        private DateTime _updatetime;
        private string _companyname;
        private string _examsheetno;
        private string _examineconfirmername;
        private string _examinername;
        private string _targetconfirmername;
        private string _examsheetname;
        private string _companyid;
        private string _examiner;
        private long _examinetarget;
        private float _score;
        private DateTime _savetime;
        private ExamineType _examinetype;
        /// <summary>
        /// 考核表主键
        /// </summary>
        public long ExamSheetID
        {
            set { _examsheetid = value; }
            get { return _examsheetid; }
        }
        /// <summary>
        /// 考核小组确认人
        /// </summary>
        public string ExamineConfirmer
        {
            set { _examineconfirmer = value; }
            get { return _examineconfirmer; }
        }
        /// <summary>
        /// 考核小组确认结果
        /// </summary>
        public ExamineConfirmResult ExamineConfirmResult
        {
            set { _examineconfirmresult = value; }
            get { return _examineconfirmresult; }
        }
        /// <summary>
        /// 考核小组确认备注
        /// </summary>
        public string ExamineConfirmRemark
        {
            set { _examineconfirmremark = value; }
            get { return _examineconfirmremark; }
        }
        /// <summary>
        /// 考核小组确认时间
        /// </summary>
        public DateTime ExaminerConfirmDate
        {
            set { _examinerconfirmdate = value; }
            get { return _examinerconfirmdate; }
        }
        /// <summary>
        /// 被考核方确认人
        /// </summary>
        public string TargetConfirmer
        {
            set { _targetconfirmer = value; }
            get { return _targetconfirmer; }
        }
        /// <summary>
        /// 被考核方确认结果
        /// </summary>
        public ExamineConfirmResult TargetConfirmResult
        {
            set { _targetconfirmresult = value; }
            get { return _targetconfirmresult; }
        }
        /// <summary>
        /// 被考核方确认备注
        /// </summary>
        public string TargetConfirmRemark
        {
            set { _targetconfirmremark = value; }
            get { return _targetconfirmremark; }
        }
        /// <summary>
        /// 被考核方确认时间
        /// </summary>
        public DateTime TargetConfirmDate
        {
            set { _targetconfirmdate = value; }
            get { return _targetconfirmdate; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 考核表编号
        /// </summary>
        public string ExamSheetNO
        {
            set { _examsheetno = value; }
            get { return _examsheetno; }
        }
        /// <summary>
        /// 考核小组确认人姓名
        /// </summary>
        public string ExamineConfirmerName
        {
            set { _examineconfirmername = value; }
            get { return _examineconfirmername; }
        }
        /// <summary>
        /// 考核人姓名（通常是填表人）
        /// </summary>
        public string ExaminerName
        {
            set { _examinername = value; }
            get { return _examinername; }
        }
        /// <summary>
        /// 被考核方确认人姓名
        /// </summary>
        public string TargetConfirmerName
        {
            set { _targetconfirmername = value; }
            get { return _targetconfirmername; }
        }
        /// <summary>
        /// 考核表名称
        /// </summary>
        public string ExamSheetName
        {
            set { _examsheetname = value; }
            get { return _examsheetname; }
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
        /// 考核人
        /// </summary>
        public string Examiner
        {
            set { _examiner = value; }
            get { return _examiner; }
        }
        /// <summary>
        /// 被考核对象
        /// </summary>
        public long ExamineTarget
        {
            set { _examinetarget = value; }
            get { return _examinetarget; }
        }
        /// <summary>
        /// 考核表总分
        /// </summary>
        public float Score
        {
            set { _score = value; }
            get { return _score; }
        }
        /// <summary>
        /// 创建此表的时间（考核时间）
        /// </summary>
        public DateTime SaveTime
        {
            set { _savetime = value; }
            get { return _savetime; }
        }
        /// <summary>
        /// 考核类型
        /// </summary>
        public ExamineType ExamineType
        {
            set { _examinetype = value; }
            get { return _examinetype; }
        }
        /// <summary>
        /// 考核表状态
        /// </summary>
        public ExamineSheetStatus Status { get; set; } 
        #endregion Model
        /// <summary>
        /// 被考核单位名称
        /// </summary>
        public string ExamineTargetName { get; set; }
        /// <summary>
        /// 考核明细项列表
        /// </summary>
        public IList ExamineItems { get; set; }
    }
    /// <summary>
    /// 考核表查询实体类
    /// </summary>
    public class ExamineSearchInfo
    {
        //private long _examsheetid;
        //private string _examineconfirmer;
        //private ExamineConfirmResult _examineconfirmresult;
        //private string _examineconfirmremark;
        //private DateTime _examinerconfirmdate;
        //private string _targetconfirmer;
        //private ExamineConfirmResult _targetconfirmresult;
        //private string _targetconfirmremark;
        //private DateTime _targetconfirmdate;
        //private DateTime _updatetime;
        //private string _companyname;
        //private string _examsheetno;
        //private string _examineconfirmername;
        //private string _examinername;
        //private string _targetconfirmername;
        //private string _examsheetname;
        //private string _companyid;
        //private string _examiner;
        //private long _examinetarget;
        //private float _score;
        //private DateTime _savetime;
        //private ExamineType _examinetype;

        /// <summary>
        /// 考核表编号
        /// </summary>
        public string ExamSheetNO { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// 被考核方
        /// </summary>
        public long ExamineTarget { get; set; }
        /// <summary>
        /// 考核人
        /// </summary>
        public string Examiner { get; set; }
        /// <summary>
        /// 考核人姓名
        /// </summary>
        public string ExaminerName { get; set; }
        /// <summary>
        /// 考核类型
        /// </summary>
        public int ExamineType { get; set; }
        /// <summary>
        /// 考核表状态
        /// </summary>
        public ExamineSheetStatus Status { get; set; } 
        /// <summary>
        /// 考核时间
        /// </summary>
        public DateTime SaveTimeFrom { get; set; }
        /// <summary>
        /// 考核时间
        /// </summary>
        public DateTime SaveTimeTo { get; set; }
    }
}
