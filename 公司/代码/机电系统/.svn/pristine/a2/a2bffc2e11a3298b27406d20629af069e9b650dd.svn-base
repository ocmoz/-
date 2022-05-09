using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FM2E.Model.Maintain;
using FM2E.IDAL.Maintain;
using FM2E.Model.Utils;

namespace FM2E.BLL.Maintain
{
    public class MalfunctionClassify
    {
        /// <summary>
        /// 获取所有的故障分类信息
        /// </summary>
        /// <returns></returns>
        public IList GetClassifyList()
        {
            IMalfunctionClassify dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionClassify();
            return dal.GetClassifyList();
        }
        /// <summary>
        /// 获取用户列表（支持分页）
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetClassifyList(MalfunctionClassifySearchInfo term, int currentPageIndex, int pageSize, out int recordCount)
        {
            IMalfunctionClassify dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionClassify();
            QueryParam p = dal.GenerateSearchTerm(term);
            p.PageIndex = currentPageIndex;
            p.PageSize = pageSize;
            return dal.GetClassifyList(p, out recordCount);
        }
        /// <summary>
        /// 获取某项的故障分类信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MalfunctionClassifyInfo GetClassify(long id)
        {
            IMalfunctionClassify dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionClassify();
            return dal.GetClassify(id);
        }
        /// <summary>
        /// 添加故障分类信息
        /// </summary>
        /// <param name="model"></param>
        public void AddClassify(MalfunctionClassifyInfo model)
        {
            IMalfunctionClassify dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionClassify();
            dal.AddClassify(model);
        }
        /// <summary>
        /// 修改故障分类信息
        /// </summary>
        /// <param name="model"></param>
        public void UpdateClassify(MalfunctionClassifyInfo model)
        {
            IMalfunctionClassify dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionClassify();
            dal.UpdateClassify(model);
        }
        /// <summary>
        /// 删除故障分类信息
        /// </summary>
        /// <param name="id"></param>
        public void DeleteClassify(long id)
        {
            IMalfunctionClassify dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionClassify();
            dal.DeleteClassify(id);
        }
        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public QueryParam GenerateSearchTerm(MalfunctionClassifySearchInfo term)
        {
            IMalfunctionClassify dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionClassify();
            return dal.GenerateSearchTerm(term);
        }
    }
}
