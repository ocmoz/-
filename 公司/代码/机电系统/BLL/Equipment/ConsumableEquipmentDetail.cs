using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using FM2E.DALFactory;
using FM2E.IDAL.Equipment;
using System.Data.Common;
namespace FM2E.BLL.Equipment
{
    /// <summary>
    /// 业务逻辑类ConsumableEquipmentDetail 的摘要说明。
    /// </summary>
    public class ConsumableEquipmentDetail
    {
        private readonly IConsumableEquipmentDetail dal = EquipmentAccess.CreateConsumableEquipmentDetail();
        public ConsumableEquipmentDetail()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void InsertConsumableEquipmentDetail(ConsumableEquipmentDetailInfo model)
        {
            dal.InsertConsumableEquipmentDetail(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateConsumableEquipmentDetail(ConsumableEquipmentDetailInfo model)
        {
            dal.UpdateConsumableEquipmentDetail(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteConsumableEquipmentDetail(long ID)
        {

            dal.DeleteConsumableEquipmentDetail(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ConsumableEquipmentDetailInfo GetConsumableEquipmentDetail(long ID)
        {

            return dal.GetConsumableEquipmentDetail(ID);
        }

        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        public IList GetAllConsumableEquipmentDetail()
        {
            return dal.GetAllConsumableEquipmentDetail();
        }

        /// <summary>
        /// 获取查询实体
        /// </summary>
        public QueryParam GenerateSearchTerm(ConsumableEquipmentDetailInfo model)
        {
            return dal.GenerateSearchTerm(model);
        }

        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        public IList GetList(QueryParam term, out int recordCount)
        {
            return dal.GetList(term, out recordCount);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ID)
        {
            return dal.Exists(ID);
        }

        #endregion  成员方法
    }
}

