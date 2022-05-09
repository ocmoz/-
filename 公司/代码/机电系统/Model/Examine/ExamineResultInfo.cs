using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;

namespace FM2E.Model.Examine
{
    /// <summary>
    /// 考核确认结果
    /// </summary>
    public enum ExamineConfirmResult
    {
        [EnumDescription("未知结果")]
        Unknown=0,
        [EnumDescription("未确认")]
        NotConfirmed = 2,
        [EnumDescription("确认通过")]
        Passed = 4,
        [EnumDescription("确认不通过")]
        NotPassed = 8
    }

    /// <summary>
    /// 考核季度
    /// </summary>
    public enum ExamineSeason{
        [EnumDescription("未知")]
        Unknown=0,
        [EnumDescription("第一季度")]
        SeasonOne = 2,
        [EnumDescription("第二季度")]
        SeasonTwo = 4,
        [EnumDescription("第三季度")]
        SeasonThree = 8,
        [EnumDescription("第四季度")]
        SeasonFour = 16
    }
    /// <summary>
    /// 考核结果实体类
    /// </summary>
    public class ExamineResultInfo
    {
        public ExamineResultInfo()
		{}
		#region Model
		private long _id;
		private DateTime _savetime;
		private string _examineconfirmer;
        private ExamineConfirmResult _examineconfirmresult;
		private string _examineconfirmremark;
		private DateTime _examineconfirmtime;
		private string _targetconfirmer;
        private ExamineConfirmResult _targetconfirmresult;
		private string _targetconfirmremark;
		private DateTime _targetconfirmtime;
		private DateTime _updatetime;
		private string _companyid;
		private string _companyname;
		private string _examinerconfirmername;
		private string _targetconfirmername;
		private string _examinername;
		private ExamineSeason _season;
		private string _examiner;
		private long _examinetarget;
		private float _dailyexamineresult;
		private float _dailyexamineratio;
		private float _seasonexamineresult;
		private float _seasonexamineratio;
		/// <summary>
		/// 主键
		/// </summary>
		public long ID
		{
			set{ _id=value;}
			get{return _id;}
		}
        /// <summary>
        /// 考核表编号
        /// </summary>
        public string SheetNO
        {
            get;
            set;
        }
        /// <summary>
        /// 考核表名称
        /// </summary>
        public string SheetName
        {
            get;
            set;
        }
        /// <summary>
        /// 考核年度
        /// </summary>
        public int Year
        {
            get;
            set;
        }
		/// <summary>
		/// 考核结果保存时间
		/// </summary>
		public DateTime SaveTime
		{
			set{ _savetime=value;}
			get{return _savetime;}
		}
		/// <summary>
		/// 考核方确认人
		/// </summary>
		public string ExamineConfirmer
		{
			set{ _examineconfirmer=value;}
			get{return _examineconfirmer;}
		}
		/// <summary>
		/// 考核方确认结果
		/// </summary>
        public ExamineConfirmResult ExamineConfirmResult
		{
			set{ _examineconfirmresult=value;}
			get{return _examineconfirmresult;}
		}
		/// <summary>
		/// 考核方确认备注
		/// </summary>
		public string ExamineConfirmRemark
		{
			set{ _examineconfirmremark=value;}
			get{return _examineconfirmremark;}
		}
		/// <summary>
		/// 考核方确认时间
		/// </summary>
		public DateTime ExamineConfirmTime
		{
			set{ _examineconfirmtime=value;}
			get{return _examineconfirmtime;}
		}
		/// <summary>
		/// 被考核方确认人
		/// </summary>
		public string TargetConfirmer
		{
			set{ _targetconfirmer=value;}
			get{return _targetconfirmer;}
		}
		/// <summary>
		/// 被考核方确认结果
		/// </summary>
        public ExamineConfirmResult TargetConfirmResult
		{
			set{ _targetconfirmresult=value;}
			get{return _targetconfirmresult;}
		}
		/// <summary>
		/// 被考核方确认备注
		/// </summary>
		public string TargetConfirmRemark
		{
			set{ _targetconfirmremark=value;}
			get{return _targetconfirmremark;}
		}
		/// <summary>
		/// 被考核方确认时间
		/// </summary>
		public DateTime TargetConfirmTime
		{
			set{ _targetconfirmtime=value;}
			get{return _targetconfirmtime;}
		}
		/// <summary>
		/// 考核结果更新时间
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 公司ID
		/// </summary>
		public string CompanyID
		{
			set{ _companyid=value;}
			get{return _companyid;}
		}
		/// <summary>
		/// 公司名称
		/// </summary>
		public string CompanyName
		{
			set{ _companyname=value;}
			get{return _companyname;}
		}
		/// <summary>
		/// 考核方确认人姓名
		/// </summary>
		public string ExaminerConfirmerName
		{
			set{ _examinerconfirmername=value;}
			get{return _examinerconfirmername;}
		}
		/// <summary>
		/// 被考核方确认人姓名
		/// </summary>
		public string TargetConfirmerName
		{
			set{ _targetconfirmername=value;}
			get{return _targetconfirmername;}
		}
		/// <summary>
		/// 考核结果保存人姓名
		/// </summary>
		public string ExaminerName
		{
			set{ _examinername=value;}
			get{return _examinername;}
		}
		/// <summary>
		/// 考核的季度
		/// </summary>
		public ExamineSeason Season
		{
			set{ _season=value;}
			get{return _season;}
		}
		/// <summary>
		/// 考核结果保存人
		/// </summary>
		public string Examiner
		{
			set{ _examiner=value;}
			get{return _examiner;}
		}
		/// <summary>
		/// 考核对象（维护队）
		/// </summary>
		public long ExamineTarget
		{
			set{ _examinetarget=value;}
			get{return _examinetarget;}
		}
        /// <summary>
        /// 考核对象（维护队）名称
        /// </summary>
        public string ExamineTargetName { get; set; }

		/// <summary>
		/// 日常考核分数
		/// </summary>
		public float DailyExamineResult
		{
			set{ _dailyexamineresult=value;}
			get{return _dailyexamineresult;}
		}
		/// <summary>
		/// 日常考核分数占的比例
		/// </summary>
		public float DailyExamineRatio
		{
			set{ _dailyexamineratio=value;}
			get{return _dailyexamineratio;}
		}
		/// <summary>
		/// 季度考核分数
		/// </summary>
		public float SeasonExamineResult
		{
			set{ _seasonexamineresult=value;}
			get{return _seasonexamineresult;}
		}
		/// <summary>
		/// 季度考核分数占的比例
		/// </summary>
		public float SeasonExamineRatio
		{
			set{ _seasonexamineratio=value;}
			get{return _seasonexamineratio;}
		}
        /// <summary>
        /// 分数
        /// </summary>
        public float Score
        {
            get {
                float result = 0;

                result = _dailyexamineratio * _dailyexamineresult + _seasonexamineratio * _seasonexamineresult;

                return result;
            }
        }
		#endregion Model
    }
    /// <summary>
    /// 考核结果查询实体类
    /// </summary>
    public class ExamineResultSearchInfo
    {
        /// <summary>
        /// 表单编号
        /// </summary>
        public string SheetNO { get; set; }
        /// <summary>
        /// 表单名称
        /// </summary>
        public string SheetName { get; set; }
        /// <summary>
        /// 考核年
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// 考核对象
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
        /// 考核季度
        /// </summary>
        public ExamineSeason Season { get; set; }
        /// <summary>
        /// 考核小组确认人
        /// </summary>
        public string ExamineConfirmer { get; set; }
        /// <summary>
        /// 考核小组确认结果
        /// </summary>
        public ExamineConfirmResult ExamineConfirmeResult { get; set; }
        /// <summary>
        /// 被考核方确认人
        /// </summary>
        public string TargetConfirmer { get; set; }
        /// <summary>
        /// 被考核方确认结果
        /// </summary>
        public ExamineConfirmResult TargetConfirmeResult { get; set; }
        /// <summary>
        /// 考核结果生成时间
        /// </summary>
        public DateTime SaveTimeFrom { get; set; }
        /// <summary>
        /// 考核结果生成时间
        /// </summary>
        public DateTime SaveTimeTo { get; set; }
    }
}
