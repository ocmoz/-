using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using System.Collections;
using FM2E.Model.Utils;
namespace FM2E.IDAL.Maintain
{
    public interface IMaintainPlanRecord
    {
        void InsertMaintainPlanRecord(MaintainPlanRecordInfo model);
        void UpdateMaintainPlanRecord(MaintainPlanRecordInfo model);
        void DelMaintainPlanRecord(long RecordID);

        IList GetList(QueryParam searchTerm, out int recordCount);
        IList GetAllRecord(long itemID);
        IList GetAllEquipmentByRecordID(long RecordID);
        IList GetList1(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(MaintainPlanRecordInfo item);
        QueryParam GenerateSearchTerm1(string system, long subsystem, string EquipmentNO, MaintainPlanType PlanType);

        MaintainPlanRecordInfo GetMaintainPlanRecord(long RecordID);

        string GetTheLastRecord(MaintainPlanRecordInfo info);

        long GetAddressIDByRecordID(long RecordID);
    }
}
