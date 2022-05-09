using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FM2E.Model.Maintain;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Maintain
{
    public interface IMalfunctionClassify
    {
        /// <summary>
        /// 获取所有的故障分类信息
        /// </summary>
        /// <returns></returns>
        IList GetClassifyList();
        /// <summary>
        /// 获取故障分类列表，支持分页
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList GetClassifyList(QueryParam term, out int recordCount);
        /// <summary>
        /// 获取某项的故障分类信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MalfunctionClassifyInfo GetClassify(long id);
        /// <summary>
        /// 添加故障分类信息
        /// </summary>
        /// <param name="model"></param>
        void AddClassify(MalfunctionClassifyInfo model);
        /// <summary>
        /// 修改故障分类信息
        /// </summary>
        /// <param name="model"></param>
        void UpdateClassify(MalfunctionClassifyInfo model);
        /// <summary>
        /// 删除故障分类信息
        /// </summary>
        /// <param name="id"></param>
        void DeleteClassify(long id);
        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam GenerateSearchTerm(MalfunctionClassifySearchInfo term);
    }
}
