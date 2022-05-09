using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Equipment
{
    public interface IOutWarehouseDetail
    {
        //IList<EquipmentInfo> GetAllEquipment();
        OutWarehouseDetailInfo GetOutWarehouseDetail(long ID, long ItemID);
        void InsertOutWarehouseDetail(OutWarehouseDetailInfo model);
        void UpdateOutWarehouseDetail(OutWarehouseDetailInfo model);
        void DelOutWarehouseDetail(long ID, long ItemID);
        //IList<EquipmentInfoFacade> Search(EquipmentSearchInfo item);
        IList GetList(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(OutWarehouseDetailInfo item);
    }
}
