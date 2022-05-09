using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using FM2E.IDAL.Maintain;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Maintain
{
    public class RoutineInspectRecord
    {
        public RoutineInspectRecordInfo GetRoutineInspectRecord(long RecordID)
        {
            IRoutineInspectRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectRecord();
            return dal.GetRoutineInspectRecord(RecordID);
        }
        public void InsertRoutineInspectRecord(RoutineInspectRecordInfo model)
        {
            IRoutineInspectRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectRecord();
            dal.InsertRoutineInspectRecord(model);
        }
        public void UpdateRoutineInspectRecord(RoutineInspectRecordInfo model)
        {
            IRoutineInspectRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectRecord();
            dal.UpdateRoutineInspectRecord(model);
        }
        public void DelRoutineInspectRecord(long RecordID)
        {
            IRoutineInspectRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectRecord();
            dal.DelRoutineInspectRecord(RecordID);
        }
        public QueryParam GenerateSearchTerm(RoutineInspectRecordInfo item)
        {
            IRoutineInspectRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectRecord();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IRoutineInspectRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectRecord();
            return dal.GetList(searchTerm, out recordCount);
        }
        public IList GetAllRecord(long itemID)
        {
            IRoutineInspectRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectRecord();
            return dal.GetAllRecord(itemID);
        }
        public string GetTheLastRecord(RoutineInspectRecordInfo info)
        {
            IRoutineInspectRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectRecord();
            return dal.GetTheLastRecord(info);
        }
        public QueryParam GenerateSearchTerm1(string system, long subsystem,string EquipmentNO)
        {
            IRoutineInspectRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectRecord();
            return dal.GenerateSearchTerm1(system,subsystem,EquipmentNO);
        }
        public IList GetList1(QueryParam searchTerm, out int recordCount)
        {
            IRoutineInspectRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineInspectRecord();
            return dal.GetList1(searchTerm, out recordCount);
        }
    }
}