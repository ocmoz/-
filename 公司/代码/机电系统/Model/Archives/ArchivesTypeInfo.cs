using System;
using System.Text;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.Model.Archives
{
    /// <summary>
    /// 实体类ArchivesTypeInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class ArchivesTypeInfo
    {
        public ArchivesTypeInfo()
        { }
        #region Model
        private long _archivestypeid;
        private string _archivestypename;
        private string _description;
        private long _parentid;
        private string _parentname;
        private long _level;
        private long _childcount;
        private string _remark;

        /// <summary>
        /// 档案类型ID
        /// </summary>
        public long ArchivesTypeID
        {
            set { _archivestypeid = value; }
            get { return _archivestypeid; }
        }
        /// <summary>
        /// 档案类型名字
        /// </summary>
        public string ArchivesTypeName
        {
            set { _archivestypename = value; }
            get { return _archivestypename; }
        }
        /// <summary>
        /// 档案类型描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 上级档案类型ID，父结点为0，则为根结点
        /// </summary>
        public long ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 上级档案类型名称
        /// </summary>
        public string ParentName
        {
            set { _parentname = value; }
            get { return _parentname; }
        }
        /// <summary>
        /// 层次
        /// </summary>
        public long Level
        {
            set { _level = value; }
            get { return _level; }
        }
        /// <summary>
        /// 子档案类型数
        /// </summary>
        public long ChildCount
        {
            set { _childcount = value; }
            get { return _childcount; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
}

