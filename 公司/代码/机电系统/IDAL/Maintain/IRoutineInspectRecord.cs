using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Maintain
{
    public interface IRoutineInspectRecord
    {
        RoutineInspectRecordInfo GetRoutineInspectRecord(long RecordID);
        void InsertRoutineInspectRecord(RoutineInspectRecordInfo model);
        void UpdateRoutineInspectRecord(RoutineInspectRecordInfo model);
        void DelRoutineInspectRecord(long RecordID);
        IList GetList(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(RoutineInspectRecordInfo item);
        IList GetAllRecord(long itemID);
        string GetTheLastRecord(RoutineInspectRecordInfo info);
        QueryParam GenerateSearchTerm1(string system, long subsystem, string EquipmentNO);
        IList GetList1(QueryParam searchTerm, out int recordCount);
    }
}
