using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Equipment;

namespace FM2E.IDAL.Equipment
{
    /// <summary>
    /// 设备借调申请接口
    /// </summary>
    public interface IBorrowApply
    {
        /// <summary>
        /// 获取借调申请列表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList GetBorrowApplyList(QueryParam term, out int recordCount);
        /// <summary>
        /// 获取审批历史记录
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList GetBorrowApprovalHistory(QueryParam term, out int recordCount);
        /// <summary>
        /// 获取特定借调申请的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BorrowApplyInfo GetBorrowApply(long id);
        /// <summary>
        /// 添加借调申请
        /// </summary>
        /// <param name="model"></param>
        long AddBorrowApply(BorrowApplyInfo model);
        /// <summary>
        /// 更新借调申请
        /// </summary>
        /// <param name="model"></param>
        void UpdateBorrowApply(BorrowApplyInfo model);
        /// <summary>
        /// 删除相应的借调申请
        /// </summary>
        /// <param name="id"></param>
        void DeleteBorrowApply(long id);
        /// <summary>
        /// 审批借调申请
        /// </summary>
        /// <param name="model"></param>
        void ApprovalBorrowApply(BorrowApprovalInfo model);
        /// <summary>
        /// 改变申请单状态值
        /// </summary>
        /// <param name="borrowApplyID">申请单号</param>
        /// <param name="status">状态值</param>
        void ChangeStatus(long borrowApplyID, BorrowApplyStatus status);
        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam GenerateSearchTerm(BorrowApplySearchInfo item);
        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam GenerateSearchTerm(BorrowApplySearchInfo item, string[] WFStates);
        /// <summary>
        /// 生成审批查询条件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam GenerateSearchTerm(BorrowApprovalSearchInfo item);
    }
}
