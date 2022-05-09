using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FM2E.Model.Maintain;
using FM2E.Model.Utils;
using System.Data.Common;
using System.Data.SqlClient;

namespace FM2E.IDAL.Maintain
{
    /// <summary>
    /// 故障处理类接口
    /// </summary>
    public interface IMalfunctionHandle
    {

        /// <summary>
        /// 获取所有的故障处理单
        /// </summary>
        /// <returns></returns>
        IList GetAllMalfunctionSheets();
        /// <summary>
        /// 获取某张故障处理单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MalfunctionHandleInfo GetMalfunctionSheet(long id);
        MalfunctionHandleInfo GetMalfunctionSheet(DbTransaction trans, long id);
        /// <summary>
        /// 根据查询条件获取故障处理单
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList GetMalfunctionSheets(QueryParam term,out int recordCount);


        //  [5/21/2013 Tvk]
        /// <summary>
        /// 根据延迟审批查询条件获取故障处理单
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        //IList GetDelayApproveMalfunctionSheets(QueryParam term, out int recordCount);
        //  [5/21/2013 Tvk]
        /// <summary>
        /// 根据查询条件获取故障处理单(不包括已撤消的故障单)，不支持分页
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList GetMalfunctionSheets(MalfunctionSearchInfo term);
        /// <summary>
        /// 获取某张故障处理单的故障设备
        /// </summary>
        /// <param name="sheetID"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        IList GetFaultyEquipments(long sheetID,DbTransaction trans);
        /// <summary>
        /// 获取所有故障设备
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList GetFaultyEquipments(MalfunctionSearchInfo term);
        /// <summary>
        /// 获取所有故障设备
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList GetMaintainedEquipments(MalfunctionSearchInfo term);
        /// <summary>
        /// 获取设备维修记录
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList GetEquipmentMaintainRecords(QueryParam term, out int recordCount);
        IList GetTransferRecord(QueryParam term, out int recordCount);
        
        /// <summary>
        /// 添加故障处理单
        /// </summary>
        /// <param name="model"></param>
        long AddMalfunctionSheet(MalfunctionHandleInfo model, DbTransaction trans);
        /// <summary>
        /// 更新故障处理单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="updateSubTable">是否更新子表</param>
        void UpdateMalfunctionSheet(MalfunctionHandleInfo model,bool updateSubTable, DbTransaction trans);
        /// <summary>
        /// 删除故障处理单
        /// </summary>
        /// <param name="id"></param>
        void DelMalfunctionSheet(long id,DbTransaction trans);


        /// <summary>
        /// 把DelegateUserName的值赋给NextUserName
        /// </summary>
        /// <param name="sheetid"></param>
        void UpdateWorkflowInstanceNextUserName(long id);

        /// <summary>
        /// 添加维修信息并更新故障处理单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="item"></param>
        void AddMaintainRecord(MalfunctionHandleInfo model, MalfuncitonMaintainInfo item, DbTransaction trans);
        /// <summary>
        /// 获取某张故障单的处理历史
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList GetMaintainHistory(long id);
        /// <summary>
        /// 获取某张故障处理单的修改历史
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList GetModifyHistory(long id);
        /// <summary>
        /// 添加故障单修改记录
        /// </summary>
        /// <param name="model"></param>
        void AddModifyRecord(MalfunctionModifyRecordInfo model, DbTransaction trans);


        /// <summary>
        /// 生成故障处理单查询条件上报时间排序
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam GenerateSearchTermByReportTime(MalfunctionSearchInfo term);

        /// <summary>
        /// 生成故障处理单查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam GenerateSearchTerm(MalfunctionSearchInfo term);
        //  [5/21/2013 Tvk]
        /// <summary>
        /// 生成故障处理单查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam GenerateDelayApproveSearchTerm(MalfunctionSearchInfo term);
        //  [5/21/2013 Tvk]
        //********** Modified by Xue    For ShenGaoSu    2011-11-28 ********************************************************************************
        /// <summary>
        /// 生成待审批的设备维修记录查询条件
        /// </summary>
        /// <param name="term">查询条件</param>
        /// <param name="userName">审批人用户名</param>
        /// <returns></returns>
        QueryParam GenerateApprovalSearchTerm(MalfunctionSearchInfo term,string userName);
        //********** Modification Finished 2011-11-28 **********************************************************************************************


        QueryParam GenerateSearchTerm2(MalfunctionSearchInfo term);

        /// <summary>
        /// 生成设备维修记录查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam GenerateSearchTerm(EquipmentMaintainRecordSearchInfo term);
        /// <summary>
        /// 生成设备流转记录查询条件
        /// </summary>
        QueryParam GenerateSearchTerm1(EquipmentMaintainRecordSearchInfo term);
        //********** Modified by Xue    For ShenGaoSu    2011-11-28 ********************************************************************************
        /// <summary>
        /// 生成待审批的设备维修记录查询条件
        /// </summary>
        /// <param name="term">查询条件</param>
        /// <param name="userName">审批人用户名</param>
        /// <returns></returns>
        QueryParam GenerateApprovalSearchTerm(EquipmentMaintainRecordSearchInfo term,string userName);
        //********** Modification Finished 2011-11-28 **********************************************************************************************

        /// <summary>
        /// 根据统计条件进行统计
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList GetMalfunctionStatisticData(MalfunctionStatisticTerm term);

        /// <summary>
        /// 根据条件获取故障设备总数列表
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList GetMaintainedEquipmentCount(MalfunctionSearchInfo term);

        /// <summary>
        /// 根据条件获取未修复故障设备总数列表
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList GetRepairedEquipmentCount(MalfunctionSearchInfo term);

        /// <summary>
        /// 根据条件获取未修复故障设备总数列表
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList GetWait4RepairedEquipmentCount(MalfunctionSearchInfo term);

        /// <summary>
        /// 根据条件获取设备总数列表
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList GetAllEquipmentCount(MalfunctionSearchInfo term);



        /// <summary>
        /// 根据ID删除故障登记纪录
        /// </summary>
        /// <param name="maintainID"></param>
        /// <param name="trans"></param>
        void DelMaintainedEquipmentByMaintainID(long maintainID, DbTransaction trans);


        //void UpdateWorkInstance(long p);
    }
}
