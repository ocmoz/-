using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using FM2E.Model.Utils;

namespace FM2E.Model.Archives
{
    /// <summary>
    /// 实体类ArchivesInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class ArchivesInfo
    {
        public ArchivesInfo()
        { }
        #region Model
        private long _archivesid;
        private string _archivesname;
        private long _archivestypeid;
        private string _archivestypename;
        private string _involvedsystem;
        private string _involvedequipment;
        private string _description;
        private string _remark;
        private IList _attachmentlist = new List<ArchivesAttachmentInfo>();

        /// <summary>
        /// 档案ID
        /// </summary>
        public long ArchivesID
        {
            set { _archivesid = value; }
            get { return _archivesid; }
        }
        /// <summary>
        /// 档案名字
        /// </summary>
        public string ArchivesName
        {
            set { _archivesname = value; }
            get { return _archivesname; }
        }
        /// <summary>
        /// 档案类型ID
        /// </summary>
        public long ArchivesTypeID
        {
            set { _archivestypeid = value; }
            get { return _archivestypeid; }
        }
        /// <summary>
        /// 档案类型名称
        /// </summary>
        public string ArchivesTypeName
        {
            set { _archivestypename = value; }
            get { return _archivestypename; }
        }
        /// <summary>
        /// 所涉及系统
        /// </summary>
        public string InvolvedSystem
        {
            set { _involvedsystem = value; }
            get { return _involvedsystem; }
        }
        /// <summary>
        /// 所涉及设备
        /// </summary>
        public string InvolvedEquipment
        {
            set { _involvedequipment = value; }
            get { return _involvedequipment; }
        }
        /// <summary>
        /// 档案描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 附件列表
        /// </summary>
        public IList AttachmentList
        {
            set { _attachmentlist = value; }
            get { return _attachmentlist; }
        }
        #endregion Model

    }
}

