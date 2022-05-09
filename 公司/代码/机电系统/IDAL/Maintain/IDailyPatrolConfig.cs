using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Maintain
{
    public interface IDailyPatrolConfig
    {
        DailyPatrolConfigInfo GetDailyPatrolConfig(long PlanID);
        void InsertDailyPatrolConfig(DailyPatrolConfigInfo model);
        void UpdateDailyPatrolConfig(DailyPatrolConfigInfo model);
        void DelDailyPatrolConfig(long PlanID);
        IList GetList(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(DailyPatrolConfigInfo item);
        IList GetAllList(DailyPatrolConfigInfo model);
        IList GetAllEquipmentByItemID(long itemID);
        void UpdateEquipments(DailyPatrolConfigInfo model);
        IList GetDailyPatrolConfigByEquipmentNO(string EquipmentNO);
        void DelDailyPatrolPlanEquipment(string EquipmentNO, long ItemID);
        void InsertDailyPatrolPlanEquipment(string EquipmentNO, long ItemID);
        QueryParam GenerateSearchTermForEquipmentList(DailyPatrolConfigInfo item);
        IList GetListForEquipmentList(QueryParam searchTerm, out int recordCount);

        QueryParam GenerateSearchTermForEquipmentAddressList(DailyPatrolConfigInfo item,string addresscode);
        IList GetAllEquipmentByItemIDandAddessCode(long itemID, string AddressCode);
    }
}
