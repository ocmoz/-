using System;
using System.Data;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Equipment;
namespace FM2E.IDAL.Equipment
{
    /// <summary>
    /// 接口层IConsumableEquipmentDetail 的摘要说明。
    /// </summary>
    public interface IConsumableEquipmentDetail
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        void InsertConsumableEquipmentDetail(ConsumableEquipmentDetailInfo model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        void UpdateConsumableEquipmentDetail(ConsumableEquipmentDetailInfo model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        void DeleteConsumableEquipmentDetail(long ID);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ConsumableEquipmentDetailInfo GetConsumableEquipmentDetail(long ID);
        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        IList GetAllConsumableEquipmentDetail();
        /// <summary>
        /// 获取查询实体
        /// </summary>
        QueryParam GenerateSearchTerm(ConsumableEquipmentDetailInfo model);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        IList GetList(QueryParam term, out int recordCount);
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(long ID);
        #endregion  成员方法
    }
}
