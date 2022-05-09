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
    /// 业务逻辑类ArchivesType 的摘要说明。
    /// </summary>
    public class ArchivesType
    {
        private readonly IArchivesType dal = ArchivesAccess.CreateArchivesType();
        public ArchivesType()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void InsertArchivesType(ArchivesTypeInfo model)
        {
            dal.InsertArchivesType(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateArchivesType(ArchivesTypeInfo model)
        {
            dal.UpdateArchivesType(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteArchivesType(long ArchivesTypeID)
        {

            dal.DeleteArchivesType(ArchivesTypeID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ArchivesTypeInfo GetArchivesType(long ArchivesTypeID)
        {

            return dal.GetArchivesType(ArchivesTypeID);
        }

        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        public IList GetAllArchivesType()
        {
            return dal.GetAllArchivesType();
        }

        /// <summary>
        /// 获取查询实体
        /// </summary>
        public QueryParam GenerateSearchTerm(ArchivesTypeInfo model)
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
        /// 获取查找实体
        /// </summary>
        public IList<ArchivesTypeInfo> Search(ArchivesTypeInfo item)
        {
            return dal.Search(item);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ArchivesTypeID)
        {
            return dal.Exists(ArchivesTypeID);
        }

        /// <summary>
        /// 获取某个类型下的所有子地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList GetChildArchivesType(long id)
        {
            return dal.GetChildArchivesType(id);
        }

        #endregion  成员方法
    }
}

