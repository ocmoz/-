using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FM2E.Model.Maintain;
using FM2E.IDAL.Maintain;
using FM2E.Model.Utils;
using FM2E.IDAL.Utils;
using System.Data.Common;
using FM2E.Model.Exceptions;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using FM2E.IDAL.Equipment;
using FM2E.Model.Basic;
using FM2E.BLL.Basic;
using System.Data;

namespace FM2E.BLL.Maintain
{
    /// <summary>
    /// 故障单费用逻辑类
    /// </summary>
    public class EquipmentCost
    {
        private IEquipmentCost dal = FM2E.DALFactory.MaintainAccess.CreateEquipmentCost();


        public EquipmentCostInfor GetEquipmentCostInforBySheetID(long sheetID)
        {
            return dal.GetEquipmentCostInforBySheetID(sheetID);
        }

        public EquipmentCostInfor GetEquipmentCostInforByID(long ID)
        {
            return dal.GetEquipmentCostInforByID(ID);
        }

        public List<EquipmentCostItems> GetEquipmentCostItemsByCostID(long costID)
        {
            return dal.GetEquipmentCostItemsByCostID(costID);
        }

        public void AddEquipmentCostInfor(EquipmentCostInfor item)
        {
            dal.AddEquipmentCostInfor(item);
        }

        public void UpdateEquipmentCostInfor(EquipmentCostInfor item)
        {
            dal.UpdateEquipmentCostInfor(item);
        }

        public void DelEquipmentCostItemsByCostID(long costID)
        {
            dal.DelEquipmentCostItemsByCostID(costID);
        }

        public void AddEquipmentCostItems(EquipmentCostItems item)
        {
            dal.AddEquipmentCostItems(item);
        }
    }
}
