using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using FM2E.IDAL.Equipment;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Equipment
{
    public class OutWarehouseDetail
    {
        public OutWarehouseDetailInfo GetOutWarehouseDetail(long ID, long ItemID)
        {
            IOutWarehouseDetail dal = FM2E.DALFactory.EquipmentAccess.CreateOutWarehouseDetail();
            return dal.GetOutWarehouseDetail(ID, ItemID);
        }
        public void InsertOutWarehouseDetail(OutWarehouseDetailInfo model)
        {
            IOutWarehouseDetail dal = FM2E.DALFactory.EquipmentAccess.CreateOutWarehouseDetail();
            dal.InsertOutWarehouseDetail(model);
        }
        public void UpdateOutWarehouseDetail(OutWarehouseDetailInfo model)
        {
            IOutWarehouseDetail dal = FM2E.DALFactory.EquipmentAccess.CreateOutWarehouseDetail();
            dal.UpdateOutWarehouseDetail(model);
        }
        public void DelOutWarehouseDetail(long ID, long ItemID)
        {
            IOutWarehouseDetail dal = FM2E.DALFactory.EquipmentAccess.CreateOutWarehouseDetail();
            dal.DelOutWarehouseDetail(ID, ItemID);
        }
        public QueryParam GenerateSearchTerm(OutWarehouseDetailInfo item)
        {
            IOutWarehouseDetail dal = FM2E.DALFactory.EquipmentAccess.CreateOutWarehouseDetail();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IOutWarehouseDetail dal = FM2E.DALFactory.EquipmentAccess.CreateOutWarehouseDetail();
            return dal.GetList(searchTerm, out recordCount);
        }
    }
}
