using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using FM2E.Model.Utils;
using FM2E.Model.Equipment;

namespace FM2E.IDAL.Equipment
{
    public interface IScrapApply
   { 
        /// <summary>
        /// 获取报废申请列表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList GetScrapApplyList(QueryParam term, out int recordCount);
        /// <summary>
        /// 获取审批历史记录
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList GetScrapApprovalHistory(QueryParam term, out int recordCount);
        /// <summary>
        /// 获取特定报废申请的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ScrapApplyInfo GetScrapApply(long id);
        /// <summary>
        /// 添加报废申请
        /// </summary>
        /// <param name="model"></param>
        long AddScrapApply(ScrapApplyInfo model);
        /// <summary>
        /// 更新报废申请
        /// </summary>
        /// <param name="model"></param>
        void UpdateScrapApply(ScrapApplyInfo model);
        /// <summary>
        /// 删除相应的报废申请
        /// </summary>
        /// <param name="id"></param>
        void DeleteScrapApply(long id);
        /// <summary>
        /// 审批报废申请
        /// </summary>
        /// <param name="model"></param>
        void ApprovalScrapApply(ScrapApprovalInfo model);
        /// <summary>
        /// 改变申请单状态值
        /// </summary>
        /// <param name="ScrapID">申请单号</param>
        /// <param name="status">状态值</param>
        void ChangeStatus(long ScrapID, int status);
        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam GenerateSearchTerm(ScrapApplySearchInfo item);
        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam GenerateSearchTerm(ScrapApplySearchInfo item, string[] WFStates);
        /// <summary>
        /// 生成审批查询条件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam GenerateSearchTerm(ScrapApprovalSearchInfo item);

    }
}
