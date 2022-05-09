using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using FM2E.IDAL.Equipment;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Equipment
{
    public class OutEquipments
    {
        public OutEquipmentsInfo GetOutEquipments(long ItemID)
        {
            IOutEquipments dal = FM2E.DALFactory.EquipmentAccess.CreateOutEquipments();
            return dal.GetOutEquipments(ItemID);
        }
        public void InsertOutEquipments(OutWarehouseApplyInfo info, ArrayList OutEquipmentList)
        {
            IOutEquipments dal = FM2E.DALFactory.EquipmentAccess.CreateOutEquipments();
            dal.InsertOutEquipments(info,OutEquipmentList);
        }
        public void UpdateOutEquipments(OutEquipmentsInfo model,OutEquipmentsInfo oldModel)
        {
            IOutEquipments dal = FM2E.DALFactory.EquipmentAccess.CreateOutEquipments();
            dal.UpdateOutEquipments(model,oldModel);
        }
        public void DelOutEquipments(long ItemID)
        {
            IOutEquipments dal = FM2E.DALFactory.EquipmentAccess.CreateOutEquipments();
            dal.DelOutEquipments(ItemID);
        }
        public QueryParam GenerateSearchTerm(OutEquipmentsInfo item)
        {
            IOutEquipments dal = FM2E.DALFactory.EquipmentAccess.CreateOutEquipments();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IOutEquipments dal = FM2E.DALFactory.EquipmentAccess.CreateOutEquipments();
            return dal.GetList(searchTerm, out recordCount);
        }
        public decimal getCountOfItem(long ApplyItemID)
        {
            IOutEquipments dal = FM2E.DALFactory.EquipmentAccess.CreateOutEquipments();
            return dal.getCountOfItem(ApplyItemID);
        }
    }
}
