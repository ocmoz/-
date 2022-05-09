using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Basic;
using FM2E.IDAL.Basic;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Basic
{
    public class Channal
    {
        public IList<ChannalInfo> GetAllChannal()
        {
            IChannal dal = FM2E.DALFactory.BasicAccess.CreateChannal();
            return dal.GetAllChannal();
        }
        public ChannalInfo GetChannal(string Channalid)
        {
            IChannal dal = FM2E.DALFactory.BasicAccess.CreateChannal();
            return dal.GetChannal(Channalid);
        }
        public void DelChannal(string Channalid)
        {
            IChannal dal = FM2E.DALFactory.BasicAccess.CreateChannal();
            dal.DelChannal(Channalid);
        }
        public IList<ChannalInfo> SearchChannal(ChannalInfo item)
        {
            IChannal dal = FM2E.DALFactory.BasicAccess.CreateChannal();
            return dal.Search(item);
        }
        public void InsertChannal(ChannalInfo item)
        {
            IChannal dal = FM2E.DALFactory.BasicAccess.CreateChannal();
            dal.InsertChannal(item);
        }
        public void UpdateChannal(ChannalInfo item)
        {
            IChannal dal = FM2E.DALFactory.BasicAccess.CreateChannal();
            dal.UpdateChannal(item);
        }
        public QueryParam GenerateSearchTerm(ChannalInfo item)
        {
            IChannal dal = FM2E.DALFactory.BasicAccess.CreateChannal();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IChannal dal = FM2E.DALFactory.BasicAccess.CreateChannal();
            return dal.GetList(searchTerm, out recordCount);
        }
        public IList GetAllChannalByCompany(string CompanyID)
        {
            IChannal dal = FM2E.DALFactory.BasicAccess.CreateChannal();
            return dal.GetAllChannalByCompany(CompanyID);
        }
    }
}
