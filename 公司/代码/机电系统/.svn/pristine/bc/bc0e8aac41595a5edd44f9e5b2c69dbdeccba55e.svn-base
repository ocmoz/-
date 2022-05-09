using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using FM2E.Model.Utils;
using FM2E.Model.Archives;

namespace FM2E.IDAL.Archives
{
    /// <summary>
    /// 接口层IArchivesType 的摘要说明。
    /// </summary>
    public interface IArchivesType
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        void InsertArchivesType(ArchivesTypeInfo model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        void UpdateArchivesType(ArchivesTypeInfo model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        void DeleteArchivesType(long ArchivesTypeID);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ArchivesTypeInfo GetArchivesType(long ArchivesTypeID);
        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        IList GetAllArchivesType();
        /// <summary>
        /// 获取查询实体
        /// </summary>
        QueryParam GenerateSearchTerm(ArchivesTypeInfo model);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        IList GetList(QueryParam term, out int recordCount);
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(long ArchivesTypeID);
        /// <summary>
        /// 获取查找实体
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        IList<ArchivesTypeInfo> Search(ArchivesTypeInfo item);
        /// <summary>
        /// 获取某个类型下的所有子地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList GetChildArchivesType(long id);
        #endregion  成员方法
    }
}
