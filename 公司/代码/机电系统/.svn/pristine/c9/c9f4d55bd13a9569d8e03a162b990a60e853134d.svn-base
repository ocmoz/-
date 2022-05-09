using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Maintain
{
    public interface IDailyPatrolRecord
    {
        DailyPatrolRecordInfo GetDailyPatrolRecord(long RecordID);
        void InsertDailyPatrolRecord(DailyPatrolRecordInfo model);
        void UpdateDailyPatrolRecord(DailyPatrolRecordInfo model);
        void DelDailyPatrolRecord(long RecordID);
        IList GetList(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(DailyPatrolRecordInfo item);
        IList GetAllRecord(long itemID);
        IList GetAllEquipmentByRecordID(long RecordID);
        string GetTheLastRecord(DailyPatrolRecordInfo info);

        QueryParam GenerateSearchTerm1(string system, long subsystem, string EquipmentNO);
        IList GetList1(QueryParam searchTerm, out int recordCount);
        long GetAddressIDByRecordID(long RecordID);
    }
}
