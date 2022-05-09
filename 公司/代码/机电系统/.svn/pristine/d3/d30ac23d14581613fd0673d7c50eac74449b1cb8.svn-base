using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FM2E.Model.Maintain;
using FM2E.IDAL.Maintain;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Maintain
{
    public class RoutineMaintainConfig
    {
        public RoutineMaintainConfigInfo GetRoutineMaintainConfig(long ItemID)
        {
            IRoutineMaintainConfig dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainConfig();
            return dal.GetRoutineMaintainConfig(ItemID);
        }
        public void InsertRoutineMaintainConfig(RoutineMaintainConfigInfo model)
        {
            IRoutineMaintainConfig dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainConfig();
            dal.InsertRoutineMaintainConfig(model);
        }
        public void UpdateRoutineMaintainConfig(RoutineMaintainConfigInfo model)
        {
            IRoutineMaintainConfig dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainConfig();
            dal.UpdateRoutineMaintainConfig(model);
        }
        public void DelRoutineMaintainConfig(long ItemID)
        {
            IRoutineMaintainConfig dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainConfig();
            dal.DelRoutineMaintainConfig(ItemID);
        }
        public QueryParam GenerateSearchTerm(RoutineMaintainConfigInfo item)
        {
            IRoutineMaintainConfig dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainConfig();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IRoutineMaintainConfig dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainConfig();
            return dal.GetList(searchTerm, out recordCount);
        }
        public IList GetAllList(RoutineMaintainConfigInfo model)
        {
            IRoutineMaintainConfig dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainConfig();
            return dal.GetAllList(model);
        }
        public IList GetAllEquipmentByItemID(long itemID)
        {
            IRoutineMaintainConfig dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainConfig();
            return dal.GetAllEquipmentByItemID(itemID);
        }
        public void UpdateEquipments(RoutineMaintainConfigInfo model)
        {
            IRoutineMaintainConfig dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainConfig();
            dal.UpdateEquipments(model);
        }
        public IList GetRoutineMaintainConfigByEquipmentNO(string EquipmentNO)
        {
            IRoutineMaintainConfig dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainConfig();
            return dal.GetRoutineMaintainConfigByEquipmentNO(EquipmentNO);
        }
        public void DelRoutineMaintainPlanEquipment(string EquipmentNO, long ItemID)
        {
            IRoutineMaintainConfig dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainConfig();
            dal.DelRoutineMaintainPlanEquipment(EquipmentNO, ItemID);
        }
        public void InsertRoutineMaintainPlanEquipment(string EquipmentNO, long ItemID)
        {
            IRoutineMaintainConfig dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainConfig();
            dal.InsertRoutineMaintainPlanEquipment(EquipmentNO, ItemID);
        }
        public QueryParam GenerateSearchTermForEquipmentList(RoutineMaintainConfigInfo item)
        {
            IRoutineMaintainConfig dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainConfig();
            return dal.GenerateSearchTermForEquipmentList(item);
        }
        public IList GetListForEquipmentList(QueryParam searchTerm, out int recordCount)
        {
            IRoutineMaintainConfig dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainConfig();
            return dal.GetListForEquipmentList(searchTerm, out recordCount);
        }

        public QueryParam GenerateSearchTermForEquipmentAddressList(RoutineMaintainConfigInfo item, string addresscode)
        {
            IRoutineMaintainConfig dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainConfig();
            return dal.GenerateSearchTermForEquipmentAddressList(item, addresscode);
        }

    }
}
