using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using FM2E.IDAL.Maintain;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Maintain
{
    public class MaintainPlanRecord
    {
        public void InsertMaintainPlanRecord(MaintainPlanRecordInfo model)
        {
            IMaintainPlanRecord dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanRecord();
            dal.InsertMaintainPlanRecord(model);
        }

        public void UpdateMaintainPlanRecord(MaintainPlanRecordInfo model)
        {
            IMaintainPlanRecord dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanRecord();
            dal.UpdateMaintainPlanRecord(model);
        }

        public void DelMaintainPlanRecord(long RecordID)
        {
            IMaintainPlanRecord dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanRecord();
            dal.DelMaintainPlanRecord(RecordID);
        }

        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IMaintainPlanRecord dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanRecord();
            return dal.GetList(searchTerm, out recordCount);
        }

        public IList GetAllRecord(long itemID)
        {
            IMaintainPlanRecord dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanRecord();
            return dal.GetAllRecord(itemID);
        }

        public IList GetAllEquipmentByRecordID(long RecordID)
        {
            IMaintainPlanRecord dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanRecord();
            return dal.GetAllEquipmentByRecordID(RecordID);
        }

        public IList GetList1(QueryParam searchTerm, out int recordCount)
        {
            IMaintainPlanRecord dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanRecord();
            return dal.GetList1(searchTerm, out recordCount);
        }

        public QueryParam GenerateSearchTerm(MaintainPlanRecordInfo item)
        {
            IMaintainPlanRecord dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanRecord();
            return dal.GenerateSearchTerm(item);
        }

        public QueryParam GenerateSearchTerm1(string system, long subsystem, string EquipmentNO ,MaintainPlanType PlanType)
        {
            IMaintainPlanRecord dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanRecord();
            return dal.GenerateSearchTerm1(system, subsystem, EquipmentNO ,PlanType);
        }

        public MaintainPlanRecordInfo GetMaintainPlanRecord(long RecordID)
        {
            IMaintainPlanRecord dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanRecord();
            return dal.GetMaintainPlanRecord(RecordID);
        }

        public string GetTheLastRecord(MaintainPlanRecordInfo info)
        {
            IMaintainPlanRecord dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanRecord();
            return dal.GetTheLastRecord(info);
        }

        public long GetAddressIDByRecordID(long RecordID)
        {
            IMaintainPlanRecord dal = FM2E.DALFactory.MaintainAccess.CreateMaintainPlanRecord();
            return dal.GetAddressIDByRecordID(RecordID);
        }
    }
}
