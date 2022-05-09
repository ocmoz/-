using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Basic;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Basic
{
    public interface IContractor
    {
        IList<ContractorInfo> GetAllContractor();
        void InsertContractor(ContractorInfo item);
        void UpdateContractor(ContractorInfo item);
        ContractorInfo GetContractor(long id);
        void DelContractor(long id);
        IList<ContractorInfo> Search(ContractorInfo item);
        QueryParam GenerateSearchTerm(ContractorInfo item);
        IList GetList(QueryParam searchTerm, out int recordCount);
    }
}
