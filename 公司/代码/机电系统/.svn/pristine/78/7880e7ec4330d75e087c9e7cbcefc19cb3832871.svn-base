using System;
using System.Data;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Archives;

namespace FM2E.IDAL.Archives
{
    /// <summary>
    /// 接口层IArchives 的摘要说明。
    /// </summary>
    public interface IArchives
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        void InsertArchives(ArchivesInfo model);
        /// <summary>
        /// 增加一条详细数据
        /// </summary>
        long InsertArchivesDetails(ArchivesInfo model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        void UpdateArchives(ArchivesInfo model);
        /// <summary>
        /// 更新一条详细数据
        /// </summary>
        void UpdateArchivesDetails(ArchivesInfo model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        void DeleteArchives(long ArchivesID);
        /// <summary>
        /// 删除一条详细数据
        /// </summary>
        void DeleteArchivesDetails(long ArchivesID);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ArchivesInfo GetArchives(long ArchivesID);
        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        IList GetAllArchives();
        /// <summary>
        /// 获取查询实体
        /// </summary>
        QueryParam GenerateSearchTerm(ArchivesInfo model);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        IList GetList(QueryParam term, out int recordCount);
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(long ArchivesID);
        #endregion  成员方法
    }
}
