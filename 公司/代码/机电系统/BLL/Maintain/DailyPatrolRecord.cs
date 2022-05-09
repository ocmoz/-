using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using FM2E.IDAL.Maintain;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Maintain
{
    public class DailyPatrolRecord
    {
        public DailyPatrolRecordInfo GetDailyPatrolRecord(long RecordID)
        {
            IDailyPatrolRecord dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolRecord();
            return dal.GetDailyPatrolRecord(RecordID);
        }
        public void InsertDailyPatrolRecord(DailyPatrolRecordInfo model)
        {
            IDailyPatrolRecord dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolRecord();
            dal.InsertDailyPatrolRecord(model);
        }
        public void UpdateDailyPatrolRecord(DailyPatrolRecordInfo model)
        {
            IDailyPatrolRecord dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolRecord();
            dal.UpdateDailyPatrolRecord(model);
        }
        public void DelDailyPatrolRecord(long RecordID)
        {
            IDailyPatrolRecord dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolRecord();
            dal.DelDailyPatrolRecord(RecordID);
        }
        public QueryParam GenerateSearchTerm(DailyPatrolRecordInfo item)
        {
            IDailyPatrolRecord dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolRecord();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IDailyPatrolRecord dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolRecord();
            return dal.GetList(searchTerm, out recordCount);
        }
        public IList GetAllRecord(long itemID)
        {
            IDailyPatrolRecord dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolRecord();
            return dal.GetAllRecord(itemID);
        }
        public IList GetAllEquipmentByRecordID(long RecordID)
        {
            IDailyPatrolRecord dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolRecord();
            return dal.GetAllEquipmentByRecordID(RecordID);
        }
        public string GetTheLastRecord(DailyPatrolRecordInfo info)
        {
            IDailyPatrolRecord dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolRecord();
            return dal.GetTheLastRecord(info);
        }
        public QueryParam GenerateSearchTerm1(string system, long subsystem,string EquipmentNO)
        {
            IDailyPatrolRecord dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolRecord();
            return dal.GenerateSearchTerm1(system,subsystem,EquipmentNO);
        }
        public IList GetList1(QueryParam searchTerm, out int recordCount)
        {
            IDailyPatrolRecord dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolRecord();
            return dal.GetList1(searchTerm, out recordCount);
        }
        public long GetAddressIDByRecordID(long RecordID)
        {
            IDailyPatrolRecord dal = FM2E.DALFactory.MaintainAccess.CreateDailyPatrolRecord();
            return dal.GetAddressIDByRecordID(RecordID);
        }
    }
}