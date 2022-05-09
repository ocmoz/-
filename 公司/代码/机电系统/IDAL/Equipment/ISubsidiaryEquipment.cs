using System;
using System.Data;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Equipment;
namespace FM2E.IDAL.Equipment
{
    /// <summary>
    /// 接口层ISubsidiaryEquipment 的摘要说明。
    /// </summary>
    public interface ISubsidiaryEquipment
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        void InsertSubsidiaryEquipment(SubsidiaryEquipmentInfo model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        void UpdateSubsidiaryEquipment(SubsidiaryEquipmentInfo model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        void DeleteSubsidiaryEquipment(long SubsidiaryEquipmentID);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        SubsidiaryEquipmentInfo GetSubsidiaryEquipment(long SubsidiaryEquipmentID);
        /// <summary>
        /// 根据条形码得到一个对象实体
        /// </summary>
        SubsidiaryEquipmentInfo GetSubsidiaryEquipmentByNO(string SubsidiaryEquipmentNO);
        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        IList GetAllSubsidiaryEquipment();
        /// <summary>
        /// 获取查询实体
        /// </summary>
        QueryParam GenerateSearchTerm(SubsidiaryEquipmentInfo model);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        IList GetList(QueryParam term, out int recordCount);
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(long SubsidiaryEquipmentID);
        #endregion  成员方法
    }
}
