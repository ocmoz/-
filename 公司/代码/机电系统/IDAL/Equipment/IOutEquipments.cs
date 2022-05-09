using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Equipment
{
    public interface IOutEquipments
    {
        //IList<EquipmentInfo> GetAllEquipment();
        OutEquipmentsInfo GetOutEquipments(long ItemID);
        void InsertOutEquipments(OutWarehouseApplyInfo info, ArrayList OutEquipmentList);
        void UpdateOutEquipments(OutEquipmentsInfo model,OutEquipmentsInfo oldModel);
        void DelOutEquipments(long ItemID);
        IList GetList(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(OutEquipmentsInfo item);
        decimal getCountOfItem(long ApplyItemID);
    }
}
