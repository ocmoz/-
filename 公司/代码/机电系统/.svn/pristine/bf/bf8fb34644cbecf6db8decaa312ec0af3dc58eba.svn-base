using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using System.Collections;
using FM2E.Model.Utils;
using System.Data.Common;

namespace FM2E.IDAL.Equipment
{
    /// <summary>
    /// 接口层IConsumableEquipment 的摘要说明。
    /// </summary>
    public interface IConsumableEquipment
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        void InsertConsumableEquipment(ConsumableEquipmentInfo model);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        long InsertConsumableEquipmentTrans(ConsumableEquipmentInfo model, DbTransaction trans);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        void UpdateConsumableEquipmentTrans(ConsumableEquipmentInfo model, DbTransaction trans);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        void UpdateConsumableEquipment(ConsumableEquipmentInfo model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        void DeleteConsumableEquipment(long ConsumableEquipmentID);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ConsumableEquipmentInfo GetConsumableEquipment(long ConsumableEquipmentID);
        /// <summary>
        /// 根据条形码得到一个对象实体
        /// </summary>
        ConsumableEquipmentInfo GetConsumableEquipmentByNO(string ConsumableEquipmentNO);
        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        IList GetAllConsumableEquipment();
        /// <summary>
        /// 获取查询实体
        /// </summary>
        QueryParam GenerateSearchTerm(ConsumableEquipmentInfo model);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        IList GetList(QueryParam term, out int recordCount);
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(long ConsumableEquipmentID);
        /// <summary>
        /// 查询易耗品列表
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        IList<ConsumableEquipmentInfo> Search(ConsumableEquipmentInfo item);
        /// <summary>
        /// 获取当前查询条件下的设备总量
        /// </summary>
        /// <param name="term"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        int GetCurrentDeviceCount(QueryParam term, string companyid);
        #endregion  成员方法


        #region 修改 By Tianmu
        /// <summary>
        /// 用于出库
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="companyid"></param>
        /// <param name="warehouseid"></param>
        /// <param name="productname"></param>
        /// <param name="model"></param>
        /// <param name="unit"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        decimal AddExpendasExpendable(DbTransaction trans, string companyid, string warehouseid, string productname, string model, string unit, decimal price, decimal count);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        void DeleteExpendasExpendable(long ConsumableEquipmentID);
        WareHouseConsumableEquipmentInfo GetExpendasExpendable(long ConsumableEquipmentID);
        ConsumableEquipmentDetailInfo GetEquipmentDetailByWarehouseID(string warehouseID, DbTransaction trans);
        void UpdateExpendasExpendable(WareHouseConsumableEquipmentInfo model, DbTransaction trans);
        long InsertExpendasExpendable(WareHouseConsumableEquipmentInfo model, DbTransaction trans);
        QueryParam GenerateExpendasSearchTerm(WareHouseConsumableEquipmentInfo item);
        IList GetExpendasListByWarehouseID(QueryParam searchTerm, out int recordCount, string wareHouseID);
        IList GetExpendasList(QueryParam searchTerm, out int recordCount);
        /// <summary>
        /// 当前的设备易耗品总数
        /// </summary>
        /// <param name="term"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        int GetCurrentExpendasDeviceCount(QueryParam term, string companyid);
        /// <summary>
        /// 获取导出仓库设备易耗品信息列表
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        IList GetExportList(QueryParam searchTerm);
        #endregion
    }
}
