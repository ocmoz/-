using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Basic;
using FM2E.Model.Basic;
using System.Collections;
using FM2E.Model.Utils;


namespace FM2E.BLL.Basic
{
    public class Contractor
    {
        public IList<ContractorInfo> GetAllContractor()
        {
            IContractor dal = FM2E.DALFactory.BasicAccess.CreateContractor();
            return dal.GetAllContractor();
        }
        public void InsertContractor(ContractorInfo item)
        {
            IContractor dal = FM2E.DALFactory.BasicAccess.CreateContractor();
            dal.InsertContractor(item);
        }
        public void UpdateContractor(ContractorInfo item)
        {
            IContractor dal = FM2E.DALFactory.BasicAccess.CreateContractor();
            dal.UpdateContractor(item);
        }
        public ContractorInfo GetContractor(long id)
        {
            IContractor dal = FM2E.DALFactory.BasicAccess.CreateContractor();
            return dal.GetContractor(id);
        }
        public void DelContractor(long id)
        {
            IContractor dal = FM2E.DALFactory.BasicAccess.CreateContractor();
            dal.DelContractor(id);
        }
        public IList<ContractorInfo> Search(ContractorInfo item)
        {
            IContractor dal = FM2E.DALFactory.BasicAccess.CreateContractor();
            return dal.Search(item);
        }
        public QueryParam GenerateSearchTerm(ContractorInfo item)
        {
            IContractor dal = FM2E.DALFactory.BasicAccess.CreateContractor();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IContractor dal = FM2E.DALFactory.BasicAccess.CreateContractor();
            return dal.GetList(searchTerm, out recordCount);
        }
    }
}
