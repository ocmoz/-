using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;

namespace FM2E.Model.Examine
{
    /// <summary>
    /// 考核类型
    /// </summary>
    public enum ExamineType
    {
        [EnumDescription("未知类型")]
        Unknown=0,
        [EnumDescription("日常考核")]
        DailyExamine = 2,
        [EnumDescription("季度考核")]
        SeasonExamine = 4,
    }
    /// <summary>
    /// 考核项实体类
    /// </summary>
    [Serializable]
    public class ExamineItemInfo
    {
        public ExamineItemInfo()
		{}
		#region Model
		private long _examitemid;
        private ExamineType _examinetype;
		private string _itemname;
        private long _parentitem;
		private float _score;
		private float _threshold;
		private string _content;
		private string _standard;
		/// <summary>
		/// 考核项ID
		/// </summary>
        public long ExamItemID
		{
			set{ _examitemid=value;}
			get{return _examitemid;}
		}
		/// <summary>
		/// 考核类型
		/// </summary>
        public ExamineType ExamineType
		{
			set{ _examinetype=value;}
			get{return _examinetype;}
		}
		/// <summary>
		/// 考核项名称
		/// </summary>
		public string ItemName
		{
			set{ _itemname=value;}
			get{return _itemname;}
		}
		/// <summary>
		/// 父考核项
		/// </summary>
		public long ParentItem
		{
			set{ _parentitem=value;}
			get{return _parentitem;}
		}
		/// <summary>
		/// 考核项分数
		/// </summary>
		public float Score
		{
			set{ _score=value;}
			get{return _score;}
		}
		/// <summary>
		/// 阈值，当扣分后分数低于此值时，分数直接变为0
		/// </summary>
		public float Threshold
		{
			set{ _threshold=value;}
			get{return _threshold;}
		}
		/// <summary>
		/// 考核内容
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 评分标准
		/// </summary>
		public string Standard
		{
			set{ _standard=value;}
			get{return _standard;}
		}
        /// <summary>
        /// 子结点数
        /// </summary>
        public int ChildCount { get; set; }
        /// <summary>
        /// 此考核项所处的层次
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 所有子考核项的分数总和
        /// </summary>
        public float ScoreOfChild { get; set; }
		#endregion Model
    }
}
