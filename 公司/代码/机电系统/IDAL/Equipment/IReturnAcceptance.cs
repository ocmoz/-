using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Equipment;

namespace FM2E.IDAL.Equipment
{
    public interface IReturnAcceptance
    {
        /// <summary>
        /// 获取某一项的设备归还验收记录
        /// </summary>
        /// <param name="returnID"></param>
        /// <returns></returns>
        ReturnAcceptanceInfo GetAcceptanceInfo(long returnID);
        /// <summary>
        /// 获取某张借调申请单的验收记录
        /// </summary>
        /// <param name="borrowApplyID"></param>
        /// <returns></returns>
        IList GetAcceptanceList(long borrowApplyID);
        /// <summary>
        /// 获取验收记录（支持分页）
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList GetAcceptanceList(QueryParam term, out int recordCount);

        /// <summary>
        /// 获取某个设备借调验收的历史明细
        /// </summary>
        /// <param name="equipmentNO"></param>
        /// <returns></returns>
        IList GetBorrowAcceptanceHistory(string equipmentNO);

        /// <summary>
        /// 设备归还验收
        /// </summary>
        /// <param name="acceptanceRecords"></param>
        void AddAcceptanceRecord(IList acceptanceRecords);
        /// <summary>
        /// 删除某张借调申请单的某个设备的验收记录
        /// </summary>
        /// <param name="borrowApplyID"></param>
        /// <param name="equipmentNO"></param>
        void DeleteAcceptanceRecord(long borrowApplyID, string equipmentNO);
        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam GenerateSearchTerm(ReturnAcceptanceSearchInfo term);
    }
}
