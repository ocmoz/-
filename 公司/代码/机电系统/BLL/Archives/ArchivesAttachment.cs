using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using FM2E.Model.Archives;
using FM2E.Model.Utils;
using FM2E.DALFactory;
using FM2E.IDAL.Archives;

namespace FM2E.BLL.Archives
{
    /// <summary>
    /// 业务逻辑类ArchivesAttachment 的摘要说明。
    /// </summary>
    public class ArchivesAttachment
    {
        private readonly IArchivesAttachment dal = ArchivesAccess.CreateArchivesAttachment();
        public ArchivesAttachment()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void InsertArchivesAttachment(ArchivesAttachmentInfo model)
        {
            dal.InsertArchivesAttachment(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateArchivesAttachment(ArchivesAttachmentInfo model)
        {
            dal.UpdateArchivesAttachment(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteArchivesAttachment(long ArchivesAttachmentID)
        {

            dal.DeleteArchivesAttachment(ArchivesAttachmentID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ArchivesAttachmentInfo GetArchivesAttachment(long ArchivesAttachmentID)
        {

            return dal.GetArchivesAttachment(ArchivesAttachmentID);
        }

        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        public IList GetAllArchivesAttachment()
        {
            return dal.GetAllArchivesAttachment();
        }

        /// <summary>
        /// 获取查询实体
        /// </summary>
        public QueryParam GenerateSearchTerm(ArchivesAttachmentInfo model)
        {
            return dal.GenerateSearchTerm(model);
        }

        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        public IList GetList(QueryParam term, out int recordCount)
        {
            return dal.GetList(term, out recordCount);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ArchivesAttachmentID)
        {
            return dal.Exists(ArchivesAttachmentID);
        }

        #endregion  成员方法
    }
}

