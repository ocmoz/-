using System;
using System.Text;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.Model.Archives
{
    /// <summary>
    /// 实体类ArchivesAttachmentInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class ArchivesAttachmentInfo
    {
        public ArchivesAttachmentInfo()
        { }
        #region Model
        private long _archivesattachmentid;
        private long _archivesid;
        private string _archivesattachmentname;
        private int _itemid;
        private string _description;
        private string _savepath;
        private string _remark;

        /// <summary>
        /// 档案附件ID
        /// </summary>
        public long ArchivesAttachmentID
        {
            set { _archivesattachmentid = value; }
            get { return _archivesattachmentid; }
        }
        /// <summary>
        /// 档案ID
        /// </summary>
        public long ArchivesID
        {
            set { _archivesid = value; }
            get { return _archivesid; }
        }
        /// <summary>
        /// 附件名称
        /// </summary>
        public string ArchivesAttachmentName
        {
            set { _archivesattachmentname = value; }
            get { return _archivesattachmentname; }
        }
        /// <summary>
        /// 档案附件项ID
        /// </summary>
        public int ItemID
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// 档案附件描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 档案附件存放路径
        /// </summary>
        public string SavePath
        {
            set { _savepath = value; }
            get { return _savepath; }
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

