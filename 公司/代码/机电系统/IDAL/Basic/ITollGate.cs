using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.IDAL.Basic
{
    public interface ITollGate
    {
        IList<TollGateInfo> GetAllTollGate();
        TollGateInfo GetTollGate(string TollGate);
        void DelTollGate(string TollGate);
        IList<TollGateInfo> Search(TollGateInfo item);
        void InsertTollGate(TollGateInfo item);
        void UpdateTollGate(TollGateInfo item);
        QueryParam GenerateSearchTerm(TollGateInfo item);
        IList GetList(QueryParam searchTerm, out int recordCount);
        IList GetAllTollGateByCompany(string CompanyID);
    }
}
