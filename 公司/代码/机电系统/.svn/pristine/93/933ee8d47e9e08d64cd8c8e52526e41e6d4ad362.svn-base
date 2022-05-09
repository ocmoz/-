using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Maintain
{
    public interface IRoutineMaintainConfig
    {
        RoutineMaintainConfigInfo GetRoutineMaintainConfig(long PlanID);
        void InsertRoutineMaintainConfig(RoutineMaintainConfigInfo model);
        void UpdateRoutineMaintainConfig(RoutineMaintainConfigInfo model);
        void DelRoutineMaintainConfig(long PlanID);
        IList GetList(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(RoutineMaintainConfigInfo item);
        void UpdateEquipments(RoutineMaintainConfigInfo model);
        IList GetAllList(RoutineMaintainConfigInfo model);
        IList GetAllEquipmentByItemID(long itemID);
        IList GetRoutineMaintainConfigByEquipmentNO(string EquipmentNO);
        void InsertRoutineMaintainPlanEquipment(string EquipmentNO, long ItemID);
        void DelRoutineMaintainPlanEquipment(string EquipmentNO,long ItemID);
        QueryParam GenerateSearchTermForEquipmentList(RoutineMaintainConfigInfo model);
        IList GetListForEquipmentList(QueryParam searchTerm,out int recordCount);

        QueryParam GenerateSearchTermForEquipmentAddressList(RoutineMaintainConfigInfo item,string addresscode);
    }
}
