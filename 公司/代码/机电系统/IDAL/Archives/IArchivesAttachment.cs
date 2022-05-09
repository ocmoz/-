using System;
using System.Data;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Archives;
namespace FM2E.IDAL.Archives
{
    /// <summary>
    /// 接口层IArchivesAttachment 的摘要说明。
    /// </summary>
    public interface IArchivesAttachment
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        void InsertArchivesAttachment(ArchivesAttachmentInfo model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        void UpdateArchivesAttachment(ArchivesAttachmentInfo model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        void DeleteArchivesAttachment(long ArchivesAttachmentID);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ArchivesAttachmentInfo GetArchivesAttachment(long ArchivesAttachmentID);
        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        IList GetAllArchivesAttachment();
        /// <summary>
        /// 获取查询实体
        /// </summary>
        QueryParam GenerateSearchTerm(ArchivesAttachmentInfo model);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        IList GetList(QueryParam term, out int recordCount);
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(long ArchivesAttachmentID);
        #endregion  成员方法
    }
}
