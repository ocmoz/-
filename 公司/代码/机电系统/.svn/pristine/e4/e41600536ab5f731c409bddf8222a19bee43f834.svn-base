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
    /// 业务逻辑类Archives 的摘要说明。
    /// </summary>
    public class Archives
    {
        private readonly IArchives dal = ArchivesAccess.CreateArchives();
        public Archives()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void InsertArchives(ArchivesInfo model)
        {
            dal.InsertArchives(model);
        }

        /// <summary>
        /// 增加一条详细数据
        /// </summary>
        public long InsertArchivesDetails(ArchivesInfo model)
        {
            return dal.InsertArchivesDetails(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateArchives(ArchivesInfo model)
        {
            dal.UpdateArchives(model);
        }

        /// <summary>
        /// 更新一条详细数据
        /// </summary>
        public void UpdateArchivesDetails(ArchivesInfo model)
        {
            dal.UpdateArchivesDetails(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteArchives(long ArchivesID)
        {

            dal.DeleteArchives(ArchivesID);
        }

        /// <summary>
        /// 删除一条详细数据
        /// </summary>
        public void DeleteArchivesDetails(long ArchivesID)
        {
            dal.DeleteArchivesDetails(ArchivesID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ArchivesInfo GetArchives(long ArchivesID)
        {

            return dal.GetArchives(ArchivesID);
        }

        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        public IList GetAllArchives()
        {
            return dal.GetAllArchives();
        }

        /// <summary>
        /// 获取查询实体
        /// </summary>
        public QueryParam GenerateSearchTerm(ArchivesInfo model)
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
        public bool Exists(long ArchivesID)
        {
            return dal.Exists(ArchivesID);
        }

        #endregion  成员方法
    }
}


