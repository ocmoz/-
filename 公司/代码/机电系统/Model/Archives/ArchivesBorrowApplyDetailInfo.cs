using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Archives
{
    /// <summary>
    /// 档案借阅申请明细信息实体类
    /// </summary>
    public class ArchivesBorrowApplyDetailInfo
    {
        #region Model
        private long _itemid;
        private long _id;
        private string _module;
        private string _archivestype;
        private string _archivesname;
        private long _archivesid;
        /// <summary>
        /// 申请单明细流水号
        /// </summary>
        public long ItemID
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// 申请单流水号
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 档案模块
        /// </summary>
        public string Module
        {
            set { _module = value; }
            get { return _module; }
        }
        /// <summary>
        /// 档案类型
        /// </summary>
        public string ArchivesType
        {
            set { _archivestype = value; }
            get { return _archivestype; }
        }
        /// <summary>
        /// 档案名称
        /// </summary>
        public string ArchivesName
        {
            set { _archivesname = value; }
            get { return _archivesname; }
        }
        /// <summary>
        /// 档案流水号
        /// </summary>
        public long ArchivesID
        {
            set { _archivesid = value; }
            get { return _archivesid; }
        }
        #endregion Model
    }
}
