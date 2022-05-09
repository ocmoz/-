using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using FM2E.IDAL.Equipment;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Equipment
{
    public class WarehouseInventory
    {
        public IList GetWarehouseInventory(int pageindex, int pagesize, string WarehouseID, string WarehouseName, DateTime MinInventoryTime, DateTime MaxInventoryTime, out int listCount)
        {
            IWarehouseInventory dal = FM2E.DALFactory.EquipmentAccess.CreateWarehouseInventory();
            return dal.GetWarehouseInventory(pageindex,pagesize, WarehouseID, WarehouseName, MinInventoryTime,MaxInventoryTime,out listCount);
        }
    }
}
