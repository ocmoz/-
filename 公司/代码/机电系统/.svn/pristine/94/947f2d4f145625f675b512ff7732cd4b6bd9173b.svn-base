using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using FM2E.DALFactory;
using FM2E.IDAL.Equipment;
namespace FM2E.BLL.Equipment
{
    /// <summary>
    /// 业务逻辑类SubsidiaryEquipment 的摘要说明。
    /// </summary>
    public class SubsidiaryEquipment
    {
        private readonly ISubsidiaryEquipment dal = EquipmentAccess.CreateSubsidiaryEquipment();
        public SubsidiaryEquipment()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void InsertSubsidiaryEquipment(SubsidiaryEquipmentInfo model)
        {
            dal.InsertSubsidiaryEquipment(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateSubsidiaryEquipment(SubsidiaryEquipmentInfo model)
        {
            dal.UpdateSubsidiaryEquipment(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteSubsidiaryEquipment(long SubsidiaryEquipmentID)
        {

            dal.DeleteSubsidiaryEquipment(SubsidiaryEquipmentID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SubsidiaryEquipmentInfo GetSubsidiaryEquipment(long SubsidiaryEquipmentID)
        {

            return dal.GetSubsidiaryEquipment(SubsidiaryEquipmentID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SubsidiaryEquipmentInfo GetSubsidiaryEquipmentByNO(string SubsidiaryEquipmentNO)
        {

            return dal.GetSubsidiaryEquipmentByNO(SubsidiaryEquipmentNO);
        }

        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        public IList GetAllSubsidiaryEquipment()
        {
            return dal.GetAllSubsidiaryEquipment();
        }

        /// <summary>
        /// 获取查询实体
        /// </summary>
        public QueryParam GenerateSearchTerm(SubsidiaryEquipmentInfo model)
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
        public bool Exists(long SubsidiaryEquipmentID)
        {
            return dal.Exists(SubsidiaryEquipmentID);
        }

        #endregion  成员方法
    }
}

