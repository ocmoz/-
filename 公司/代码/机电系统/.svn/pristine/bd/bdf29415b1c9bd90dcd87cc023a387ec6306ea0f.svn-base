using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using System.Collections;
using FM2E.Model.Utils;
using System.Data.Common;

namespace FM2E.IDAL.Maintain
{
    /// <summary>
    /// 维护类接口
    /// </summary>
    public interface IMaintain
    {
        /// <summary>
        /// 添加维护项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long AddMaintainItem(MaintainItemInfo model);
        /// <summary>
        /// 修改维护项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long UpdateMaintainItem(MaintainItemInfo model);
        /// <summary>
        /// 删除维护项
        /// </summary>
        /// <param name="id"></param>
        void DeleteMaintainItem(long id);
        /// <summary>
        /// 获取一个维护项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MaintainItemInfo GetMaintainItem(long id);
        /// <summary>
        /// 核实
        /// </summary>
        /// <param name="id"></param>
        /// <param name="result"></param>
        /// <param name="confirmer"></param>
        /// <param name="confirmTime"></param>
        /// <param name="remark"></param>
        void DoConfirm(long id, MaintainConfirmResult result, string confirmer, DateTime confirmTime, string remark);
        /// <summary>
        /// 生成维护项查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam GetSearchTerm(MaintainItemSearchInfo term);
        /// <summary>
        /// 搜索维护项（分页）
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList SearchMaintainItem(QueryParam qp, out int recordCount);
        /// <summary>
        /// 搜索维护项（不分页）
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList SearchMaintainItem(MaintainItemSearchInfo term);

        /// <summary>
        /// 添加一个维护模板表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        long AddTemplateMaintainSheet(TemplateMaintainSheetInfo model,DbTransaction trans);
        /// <summary>
        /// 修改维护模板表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        long UpdateTemplateMaintainSheet(TemplateMaintainSheetInfo model,DbTransaction trans);
        /// <summary>
        /// 删除维护模板表
        /// </summary>
        /// <param name="id"></param>
        void DeleteTemplateMaintainSheet(long id);
        /// <summary>
        /// 获取一个维护模板表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TemplateMaintainSheetInfo GetTemplateMaintainSheet(long id);
        /// <summary>
        /// 生成维护模板表查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam GetSearchTerm(TemplateMaintainSheetSearchInfo term);
        /// <summary>
        /// 查询维护模板表
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList SearchTemplateMaintainSheet(QueryParam qp, out int recordCount);

        
        /// <summary>
        /// 添加一个维护表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        long AddMaintainSheet(MaintainSheetInfo model, DbTransaction trans);
        /// <summary>
        /// 修改维护表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        long UpdateMaintainSheet(MaintainSheetInfo model, DbTransaction trans);
        /// <summary>
        /// 删除维护表
        /// </summary>
        /// <param name="id"></param>
        void DeleteMaintainSheet(long id);
        /// <summary>
        /// 获取维护表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MaintainSheetInfo GetMaintainSheet(long id);
        /// <summary>
        /// 生成维护表查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam GetSearchTerm(MaintainSheetSearchInfo term);
        /// <summary>
        /// 查询维护表
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList SearchMaintainSheet(QueryParam qp, out int recordCount);
        /// <summary>
        /// 获取设备维护记录，不分页
        /// </summary>
        /// <param name="equipmentNO">设备条形码</param>
        /// <param name="type">维护类型</param>
        /// <returns></returns>
        IList SearchDeviceMaintainRecord(string equipmentNO, MaintainType type);
        /// <summary>
        /// 生成维护设备查询条件
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        QueryParam GetSearchTerm(MaintainSheetEquipmentSearchInfo info);
         /// <summary>
        /// 获取设备维护记录，分页
        /// </summary>
        /// <param name="equipmentNO">设备条形码</param>
        /// <param name="type">维护类型</param>
        /// <returns></returns>
        IList SearchDeviceMaintainRecord(QueryParam qp, out int recordCount);
        /// <summary>
        /// 获取一个维护项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MaintainSheetInfo GetMaintainSheetByEquipmentName(long id);

    }
}
