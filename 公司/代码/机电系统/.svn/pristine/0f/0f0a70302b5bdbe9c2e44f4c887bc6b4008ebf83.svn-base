using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using FM2E.IDAL.Maintain;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Maintain
{
    public class DailyPatrolConfig
    {
        public DailyPatrolConfigInfo GetDailyPatrolConfig(long ItemID)
        {
            IDailyPatrolConfig dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolConfig();
            return dal.GetDailyPatrolConfig(ItemID);
        }
        public void InsertDailyPatrolConfig(DailyPatrolConfigInfo model)
        {
            IDailyPatrolConfig dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolConfig();
            dal.InsertDailyPatrolConfig(model);
        }
        public void UpdateDailyPatrolConfig(DailyPatrolConfigInfo model)
        {
            IDailyPatrolConfig dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolConfig();
            dal.UpdateDailyPatrolConfig(model);
        }
        public void DelDailyPatrolConfig(long ItemID)
        {
            IDailyPatrolConfig dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolConfig();
            dal.DelDailyPatrolConfig(ItemID);
        }
        public QueryParam GenerateSearchTerm(DailyPatrolConfigInfo item)
        {
            IDailyPatrolConfig dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolConfig();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IDailyPatrolConfig dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolConfig();
            return dal.GetList(searchTerm, out recordCount);
        }
        public IList GetAllList(DailyPatrolConfigInfo model)
        {
            IDailyPatrolConfig dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolConfig();
            return dal.GetAllList(model);
        }
        public IList GetAllEquipmentByItemID(long itemID)
        {
            IDailyPatrolConfig dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolConfig();
            return dal.GetAllEquipmentByItemID(itemID);
        }
        public void UpdateEquipments(DailyPatrolConfigInfo model)
        {
            IDailyPatrolConfig dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolConfig();
            dal.UpdateEquipments(model);
        }
        public IList GetDailyPatrolConfigByEquipmentNO(string EquipmentNO)
        {
            IDailyPatrolConfig dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolConfig();
            return dal.GetDailyPatrolConfigByEquipmentNO(EquipmentNO);
        }
        public void DelDailyPatrolPlanEquipment(string EquipmentNO, long ItemID)
        {
            IDailyPatrolConfig dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolConfig();
            dal.DelDailyPatrolPlanEquipment(EquipmentNO, ItemID);
        }
        public void InsertDailyPatrolPlanEquipment(string EquipmentNO, long ItemID)
        {
            IDailyPatrolConfig dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolConfig();
            dal.InsertDailyPatrolPlanEquipment(EquipmentNO,ItemID);
        }
        public QueryParam GenerateSearchTermForEquipmentList(DailyPatrolConfigInfo item)
        {
            IDailyPatrolConfig dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolConfig();
            return dal.GenerateSearchTermForEquipmentList(item);
        }
        public IList GetListForEquipmentList(QueryParam searchTerm, out int recordCount)
        {
            IDailyPatrolConfig dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolConfig();
            return dal.GetListForEquipmentList(searchTerm, out recordCount);
        }

        public IList GetAllEquipmentByItemIDandAddessCode(long itemID, string AddressCode)
        {
            IDailyPatrolConfig dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolConfig();
            return dal.GetAllEquipmentByItemIDandAddessCode(itemID, AddressCode);
        }

        public QueryParam GenerateSearchTermForEquipmentAddressList(DailyPatrolConfigInfo item,string addresscode)
        {
            IDailyPatrolConfig dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolConfig();
            return dal.GenerateSearchTermForEquipmentAddressList(item, addresscode);
        }

    }
}