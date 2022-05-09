using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Equipment;

namespace FM2E.IDAL.Equipment
{
    /// <summary>
    /// 设备借出登记接口
    /// </summary>
    public interface IBorrowRecord
    {
        /// <summary>
        /// 获取某个借调申请的借出设备登记明细
        /// </summary>
        /// <param name="borrowApplyID"></param>
        /// <returns></returns>
        IList GetBorrowRecordList(long borrowApplyID);
        /// <summary>
        /// 获取借出设备登记明细(支持分页)
        /// </summary>
        /// <param name="borrowApplyID"></param>
        /// <returns></returns>
        IList GetBorrowRecordList(QueryParam term,out int recordCount);
        /// <summary>
        /// 获取某个设备的借调历史明细
        /// </summary>
        /// <param name="equipmentNO"></param>
        /// <returns></returns>
        IList GetBorrowRecordHistory(string equipmentNO);
        /// <summary>
        /// 获取某个未归还设备的借出信息
        /// </summary>
        /// <param name="equipmentNO"></param>
        /// <returns></returns>
        BorrowRecordInfo GetEquipmentNotReturned(string equipmentNO);
        /// <summary>
        /// 获取某个设备的借出记录
        /// </summary>
        /// <param name="borrowApplyID"></param>
        /// <param name="equipmentNO"></param>
        /// <returns></returns>
        BorrowRecordInfo GetBorrowRecord(long borrowApplyID, string equipmentNO);
        /// <summary>
        /// 设备借出登记
        /// </summary>
        /// <param name="borrowRecords"></param>
        void AddBorrowRecord(IList borrowRecords);
        /// <summary>
        /// 删除某张借调申请单的设备借出记录
        /// </summary>
        /// <param name="borrowApplyID"></param>
        void DeleteBorrowRecord(long borrowApplyID);
        /// <summary>
        /// 删除某张借调申请单的某个设备的借出记录
        /// </summary>
        /// <param name="borrowApplyID"></param>
        /// <param name="equipmentNO"></param>
        void DeleteBorrowRecord(long borrowApplyID, string equipmentNO);
        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam GenerateSearchTerm(BorrowRecordSearchInfo term);
    }
}
