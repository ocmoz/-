using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Basic;
using FM2E.IDAL.Basic;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.BLL.Basic
{
    public class TollGate
    {
        public IList<TollGateInfo> GetAllTollGate()
        {
            ITollGate dal = FM2E.DALFactory.BasicAccess.CreateTollGate();
            return dal.GetAllTollGate();
        }
        public TollGateInfo GetTollGate(string TollGateid)
        {
            ITollGate dal = FM2E.DALFactory.BasicAccess.CreateTollGate();
            return dal.GetTollGate(TollGateid);
        }
        public void DelTollGate(string TollGateid)
        {
            ITollGate dal = FM2E.DALFactory.BasicAccess.CreateTollGate();
            dal.DelTollGate(TollGateid);
        }
        public IList<TollGateInfo> SearchTollGate(TollGateInfo item)
        {
            ITollGate dal = FM2E.DALFactory.BasicAccess.CreateTollGate();
            return dal.Search(item);
        }
        public void InsertTollGate(TollGateInfo item)
        {
            ITollGate dal = FM2E.DALFactory.BasicAccess.CreateTollGate();
            dal.InsertTollGate(item);
        }
        public void UpdateTollGate(TollGateInfo item)
        {
            ITollGate dal = FM2E.DALFactory.BasicAccess.CreateTollGate();
            dal.UpdateTollGate(item);
        }
        public QueryParam GenerateSearchTerm(TollGateInfo item)
        {
            ITollGate dal = FM2E.DALFactory.BasicAccess.CreateTollGate();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            ITollGate dal = FM2E.DALFactory.BasicAccess.CreateTollGate();
            return dal.GetList(searchTerm, out recordCount);
        }
        public IList GetAllTollGateByCompany(string CompanyID)
        {
            ITollGate dal = FM2E.DALFactory.BasicAccess.CreateTollGate();
            return dal.GetAllTollGateByCompany(CompanyID);
        }
    }
}
