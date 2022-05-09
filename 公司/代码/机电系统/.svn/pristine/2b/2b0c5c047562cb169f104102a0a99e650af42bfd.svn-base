using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Examine
{
    /// <summary>
    /// 考核明细实体类
    /// </summary>
    public class ExamineDetailInfo
    {
        #region Model
        private long _id;
        private string _remark;
        private string _deductreason;
        private float _deduct;
        private DateTime _examinedate;
        private string _confirmer;
        private ExamineConfirmResult _confirmresult;
        private string _confirmremark;
        private DateTime _confirmtime;
        private DateTime _updatetime;
        private string _examinername;
        private long _examsheetid;
        private string _confirmername;
        private string _examiner;
        private string _itemname;
        private long _parentitem;
        private float _score;
        private float _threshold;
        private string _content;
        private string _standard;
        /// <summary>
        /// 考核明细ID
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 考核项备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 扣分原因
        /// </summary>
        public string DeductReason
        {
            set { _deductreason = value; }
            get { return _deductreason; }
        }
        /// <summary>
        /// 扣除的分数
        /// </summary>
        public float Deduct
        {
            set { _deduct = value; }
            get { return _deduct; }
        }
        /// <summary>
        /// 考核时间
        /// </summary>
        public DateTime ExamineDate
        {
            set { _examinedate = value; }
            get { return _examinedate; }
        }
        /// <summary>
        /// 确认人
        /// </summary>
        public string Confirmer
        {
            set { _confirmer = value; }
            get { return _confirmer; }
        }
        /// <summary>
        /// 确认结果
        /// </summary>
        public ExamineConfirmResult ConfirmResult
        {
            set { _confirmresult = value; }
            get { return _confirmresult; }
        }
        /// <summary>
        /// 确认备注
        /// </summary>
        public string ConfirmRemark
        {
            set { _confirmremark = value; }
            get { return _confirmremark; }
        }
        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime ConfirmTime
        {
            set { _confirmtime = value; }
            get { return _confirmtime; }
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
        /// 考核人姓名
        /// </summary>
        public string ExaminerName
        {
            set { _examinername = value; }
            get { return _examinername; }
        }
        /// <summary>
        /// 考核表ID
        /// </summary>
        public long ExamSheetID
        {
            set { _examsheetid = value; }
            get { return _examsheetid; }
        }
        /// <summary>
        /// 确认人姓名
        /// </summary>
        public string ConfirmerName
        {
            set { _confirmername = value; }
            get { return _confirmername; }
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
        /// 考核项名称
        /// </summary>
        public string ItemName
        {
            set { _itemname = value; }
            get { return _itemname; }
        }
        /// <summary>
        /// 父考核项
        /// </summary>
        public long ParentItem
        {
            set { _parentitem = value; }
            get { return _parentitem; }
        }
        /// <summary>
        /// 此考核项的总分
        /// </summary>
        public float Score
        {
            set { _score = value; }
            get { return _score; }
        }
        /// <summary>
        /// 阈值，如果扣分后的总分低于此值，则分数取0
        /// </summary>
        public float Threshold
        {
            set { _threshold = value; }
            get { return _threshold; }
        }
        /// <summary>
        /// 考核内容
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 评分标准
        /// </summary>
        public string Standard
        {
            set { _standard = value; }
            get { return _standard; }
        }
        /// <summary>
        /// 子考核项数
        /// </summary>
        public int ChildCount { get; set; }
        /// <summary>
        /// 考核项层次
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 子考核项ID
        /// </summary>
        public long ExamItemID { get; set; }
        /// <summary>
        /// 是否能在此考核项下添加子考核项
        /// </summary>
        public bool CanAddChild { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public string NO { get; set; }
        /// <summary>
        /// 考核得分
        /// </summary>
        public float ExamScore { get; set; }
        #endregion Model
    }
}
