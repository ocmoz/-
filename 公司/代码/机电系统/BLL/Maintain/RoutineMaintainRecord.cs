using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using FM2E.IDAL.Maintain;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Maintain
{
    public class RoutineMaintainRecord
    {
        public RoutineMaintainRecordInfo GetRoutineMaintainRecord(long RecordID)
        {
            IRoutineMaintainRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainRecord();
            return dal.GetRoutineMaintainRecord(RecordID);
        }
        public void InsertRoutineMaintainRecord(RoutineMaintainRecordInfo model)
        {
            IRoutineMaintainRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainRecord();
            dal.InsertRoutineMaintainRecord(model);
        }
        public void UpdateRoutineMaintainRecord(RoutineMaintainRecordInfo model)
        {
            IRoutineMaintainRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainRecord();
            dal.UpdateRoutineMaintainRecord(model);
        }
        public void DelRoutineMaintainRecord(long RecordID)
        {
            IRoutineMaintainRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainRecord();
            dal.DelRoutineMaintainRecord(RecordID);
        }
        public QueryParam GenerateSearchTerm(RoutineMaintainRecordInfo item)
        {
            IRoutineMaintainRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainRecord();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IRoutineMaintainRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainRecord();
            return dal.GetList(searchTerm, out recordCount);
        }
        public IList GetAllRecord(long itemID)
        {
            IRoutineMaintainRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainRecord();
            return dal.GetAllRecord(itemID);
        }
        public string GetTheLastRecord(RoutineMaintainRecordInfo info)
        {
            IRoutineMaintainRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainRecord();
            return dal.GetTheLastRecord(info);
        }
        public QueryParam GenerateSearchTerm1(string system, long subsystem,string EquipmentNO)
        {
            IRoutineMaintainRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainRecord();
            return dal.GenerateSearchTerm1(system,subsystem,EquipmentNO);
        }
        public IList GetList1(QueryParam searchTerm, out int recordCount)
        {
            IRoutineMaintainRecord dal = FM2E.DALFactory.MaintainAccess.CreateRoutineMaintainRecord();
            return dal.GetList1(searchTerm, out recordCount);
        }
    }
}