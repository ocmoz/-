using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FM2E.Model.Maintain;
using FM2E.Model.Utils;
using System.Data.Common;
using System.Data.SqlClient;

namespace FM2E.IDAL.Maintain
{
    /// <summary>
    /// 故障单费用类接口
    /// </summary>
    public interface IEquipmentCost
    {
        EquipmentCostInfor GetEquipmentCostInforBySheetID(long sheetID);

        EquipmentCostInfor GetEquipmentCostInforByID(long ID);

        List<EquipmentCostItems> GetEquipmentCostItemsByCostID(long costID);



        void AddEquipmentCostInfor(EquipmentCostInfor item);

        void UpdateEquipmentCostInfor(EquipmentCostInfor item);


        void DelEquipmentCostItemsByCostID(long costID);

        void AddEquipmentCostItems(EquipmentCostItems item);
    }
}
