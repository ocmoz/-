using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Equipment
{
    public interface IInEquipments
    {
        //IList<EquipmentInfo> GetAllEquipment();
        InEquipmentsInfo GetInEquipments(long ID, long ItemID);
        void InsertInEquipments(InEquipmentsInfo model);
        void UpdateInEquipments(InEquipmentsInfo model);
        void DelInEquipments(long ID, long ItemID);
        IList GetList(QueryParam searchTerm, out int recordCount);
        IList GetListForOut(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(InEquipmentsInfo item);
        QueryParam GenerateSearchTermForOut(InEquipmentsInfo item);
    }
}
