using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Maintain
{
    public interface IRoutineMaintainRecord
    {
        RoutineMaintainRecordInfo GetRoutineMaintainRecord(long RecordID);
        void InsertRoutineMaintainRecord(RoutineMaintainRecordInfo model);
        void UpdateRoutineMaintainRecord(RoutineMaintainRecordInfo model);
        void DelRoutineMaintainRecord(long RecordID);
        IList GetList(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(RoutineMaintainRecordInfo item);
        IList GetAllRecord(long itemID);
        string GetTheLastRecord(RoutineMaintainRecordInfo info);
        QueryParam GenerateSearchTerm1(string system, long subsystem, string EquipmentNO);
        IList GetList1(QueryParam searchTerm, out int recordCount);
    }
}
