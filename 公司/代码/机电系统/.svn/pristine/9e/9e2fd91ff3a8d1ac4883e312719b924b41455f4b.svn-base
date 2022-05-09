using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Equipment
{
    public interface IInWarehouse
    {
        //IList<EquipmentInfo> GetAllEquipment();
        InWarehouseInfo GetInWarehouse(long ID);
        void InsertInWarehouse(InWarehouseInfo model, ArrayList list);
        void UpdateInWarehouse(InWarehouseInfo model);
        void DelInWarehouse(long ID);
        IList GetList(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(InWarehouseInfo item);
        /// <summary>
        /// 插入带有详情的入库单
        /// </summary>
        /// <param name="model"></param>
        void InsertInWarehouseWithDetail(InWarehouseInfo model);

        long InsertInWarehouseExpendable(InWarehouseInfo model, InEquipmentsInfo item);

        long UpdateInWarehouseExpendable(InWarehouseInfo model, InEquipmentsInfo item);

        void UpdateInEquipments(InEquipmentsInfo model);

        QueryParam GenerateSearchTerm(InWarehouseInfo info, int pageindex, int pagesize);
        QueryParam GetGenerateSearchTerm(InWarehouseInfo info, int pageindex, int pagesize, string userName);

        /// <summary>
        ///易耗品入库申请单（审批列表）
        /// </summary>
        IList SearchInWarehouseApply(QueryParam qp, out int recordCount);
    }
}
