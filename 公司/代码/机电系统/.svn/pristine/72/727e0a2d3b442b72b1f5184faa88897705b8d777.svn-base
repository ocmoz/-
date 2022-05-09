using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Basic;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Basic
{
    public interface IChannal
    {
        IList<ChannalInfo> GetAllChannal();
        ChannalInfo GetChannal(string Channal);
        void DelChannal(string Channal);
        IList<ChannalInfo> Search(ChannalInfo item);
        void InsertChannal(ChannalInfo item);
        void UpdateChannal(ChannalInfo item);
        QueryParam GenerateSearchTerm(ChannalInfo item);
        IList GetList(QueryParam searchTerm, out int recordCount);
        IList GetAllChannalByCompany(string CompanyID);
    }
}
