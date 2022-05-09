using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Archives;
using FM2E.IDAL.Archives;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Archives
{
    /// <summary>
    /// 档案借阅业务逻辑处理类
    /// </summary>
    public class ArchivesBorrowApply
    {
        /// <summary>
        /// 获取档案借阅申请表
        /// </summary>
        /// <param name="ID">申请表流水号</param>
        /// <returns>档案申请表信息</returns>
        public ArchivesBorrowApplyInfo GetArchivesBorrowApply(long ID)
        {
            IArchivesBorrowApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesBorrowApply();
            return dal.GetArchivesBorrowApply(ID);
        }
        /// <summary>
        /// 添加档案借阅申请表
        /// </summary>
        /// <param name="model">新添加的申请表信息</param>
        /// <returns>新申请表流水号</returns>
        public long InsertArchivesBorrowApply(ArchivesBorrowApplyInfo model)
        {
            IArchivesBorrowApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesBorrowApply();
            return dal.InsertArchivesBorrowApply(model);
        }
        /// <summary>
        /// 更新档案借阅申请表
        /// </summary>
        /// <param name="model">需要更新的申请表信息</param>
        public void UpdateArchivesBorrowApply(ArchivesBorrowApplyInfo model)
        {
            IArchivesBorrowApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesBorrowApply();
            dal.UpdateArchivesBorrowApply(model);
        }
        /// <summary>
        /// 删除档案借阅申请表
        /// </summary>
        /// <param name="ID">申请表流水号</param>
        public void DelArchivesBorrowApply(long ID)
        {
            IArchivesBorrowApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesBorrowApply();
            dal.DelArchivesBorrowApply(ID);
        }
        /// <summary>
        /// 生成查询档案借阅申请单查询对象
        /// </summary>
        /// <param name="item">申请单查询参数</param>
        /// <returns>查询所用的查询对象</returns>
        public QueryParam GenerateSearchTerm(ArchivesBorrowApplyInfo item)
        {
            IArchivesBorrowApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesBorrowApply();
            return dal.GenerateSearchTerm(item);
        }
        /// <summary>
        /// 生成查询档案借阅申请单查询对象（含工作流状态）
        /// </summary>
        /// <param name="item">申请单查询参数</param>
        /// <param name="WFStates">工作流状态列表</param>
        /// <returns>查询所用的查询对象</returns>
        public QueryParam GenerateSearchTerm(ArchivesBorrowApplyInfo item, string[] WFStates)
        {
            IArchivesBorrowApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesBorrowApply();
            return dal.GenerateSearchTerm(item,WFStates);
        }
        /// <summary>
        /// 查询档案借阅申请单
        /// </summary>
        /// <param name="searchTerm">查询对象</param>
        /// <param name="recordCount">查询结果总数</param>
        /// <returns>查询结果列表</returns>
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IArchivesBorrowApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesBorrowApply();
            return dal.GetList(searchTerm, out recordCount);
        }
        /// <summary>
        /// 判断申请人是否已经申请该档案
        /// </summary>
        /// <param name="archivesType">档案类型</param>
        /// <param name="id">档案流水号</param>
        /// <param name="applicant">申请人账户</param>
        /// <returns>是否已经借阅</returns>
        public bool isBorrowedDetail(string archivesType, long id,string applicant)
        {
            IArchivesBorrowApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesBorrowApply();
            return dal.isBorrowedDetail(archivesType, id,applicant);
        }
        /// <summary>
        /// 生成档案借阅明细查询所用的对象
        /// </summary>
        /// <param name="item">明细查询参数</param>
        /// <param name="Applicant">申请人账户</param>
        /// <returns>查询对象</returns>
        public QueryParam GenerateDetailSearchTerm(ArchivesBorrowApplyDetailInfo item,string Applicant)
        {
            IArchivesBorrowApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesBorrowApply();
            return dal.GenerateDetailSearchTerm(item,Applicant);
        }
        /// <summary>
        /// 查询借阅明细
        /// </summary>
        /// <param name="searchTerm">查询对象</param>
        /// <param name="recordCount">查询结果总数</param>
        /// <returns>明细查询结果列表</returns>
        public IList GetDetailList(QueryParam searchTerm, out int recordCount)
        {
            IArchivesBorrowApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesBorrowApply();
            return dal.GetDetailList(searchTerm, out recordCount);
        }

    }
}
