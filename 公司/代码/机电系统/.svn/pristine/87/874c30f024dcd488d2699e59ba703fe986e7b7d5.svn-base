using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using FM2E.IDAL.Maintain;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Maintain
{
    public class MaintainPlanConfig
    {
        public void InsertMaintainPlanConfig(MaintainPlanConfigInfo model)
        {
            IMaintainPlanConfig dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanConfig();
            dal.InsertMaintainPlanConfig(model);
        }

        public void UpdateMaintainPlanConfig(MaintainPlanConfigInfo model)
        {
            IMaintainPlanConfig dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanConfig();
            dal.UpdateMaintainPlanConfig(model);
        }
        public void DelMaintainPlanConfig(long PlanID)
        {
            IMaintainPlanConfig dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanConfig();
            dal.DelMaintainPlanConfig(PlanID);
        }

        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IMaintainPlanConfig dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanConfig();
            return dal.GetList(searchTerm,out recordCount);
        }

        public IList GetAllList(MaintainPlanConfigInfo model)
        {
            IMaintainPlanConfig dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanConfig();
            return dal.GetAllList(model);
        }

        public IList GetAllEquipmentByItemID(long itemID)
        {
            IMaintainPlanConfig dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanConfig();
            return dal.GetAllEquipmentByItemID(itemID);
        }

        public IList GetAllEquipmentByItemIDandAddessCode(long itemID, string AddressCode)
        {
            IMaintainPlanConfig dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanConfig();
            return dal.GetAllEquipmentByItemIDandAddessCode(itemID, AddressCode);
        }
        public IList GetMaintainPlanConfigByEquipmentNO(string EquipmentNO, MaintainPlanType PlanType)
        {
            IMaintainPlanConfig dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanConfig();
            return dal.GetMaintainPlanConfigByEquipmentNO(EquipmentNO, PlanType);
        }

        public IList GetListForEquipmentList(QueryParam searchTerm, out int recordCount)
        {
            IMaintainPlanConfig dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanConfig();
            return dal.GetListForEquipmentList(searchTerm, out recordCount);
        }

        public QueryParam GenerateSearchTerm(MaintainPlanConfigInfo item)
        {
            IMaintainPlanConfig dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanConfig();
            return dal.GenerateSearchTerm(item);
        }

        public QueryParam GenerateSearchTermForEquipmentList(MaintainPlanConfigInfo item)
        {
            IMaintainPlanConfig dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanConfig();
            return dal.GenerateSearchTermForEquipmentList(item);
        }
        public QueryParam GenerateSearchTermForEquipmentAddressList(MaintainPlanConfigInfo item, string addresscode)
        {
            IMaintainPlanConfig dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanConfig();
            return dal.GenerateSearchTermForEquipmentAddressList(item, addresscode);
        }

        public MaintainPlanConfigInfo GetMaintainPlanConfig(long PlanID)
        {
            IMaintainPlanConfig dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanConfig();
            return dal.GetMaintainPlanConfig(PlanID);
        }

        public void UpdateEquipments(MaintainPlanConfigInfo model)
        {
            IMaintainPlanConfig dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanConfig();
            dal.UpdateEquipments(model);
        }

        public void DelMaintainPlanEquipment(string EquipmentNO, long ItemID)
        {
            IMaintainPlanConfig dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanConfig();
            dal.DelMaintainPlanEquipment(EquipmentNO, ItemID);
        }

        public void InsertMaintainPlanEquipment(string EquipmentNO, long ItemID)
        {
            IMaintainPlanConfig dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanConfig();
            dal.InsertMaintainPlanEquipment(EquipmentNO, ItemID);
        }

    }
}
