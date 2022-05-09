using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Basic;
using FM2E.Model.Basic;
using System.Collections;
using FM2E.Model.Utils;


namespace FM2E.BLL.Basic
{
    public class Producer
    {
        public IList<ProducerInfo> GetAllProducer()
        {
            IProducer dal = FM2E.DALFactory.BasicAccess.CreateProducer();
            return dal.GetAllProducer();
        }
        public void InsertProducer(ProducerInfo item)
        {
            IProducer dal = FM2E.DALFactory.BasicAccess.CreateProducer();
            dal.InsertProducer(item);
        }
        public void UpdateProducer(ProducerInfo item)
        {
            IProducer dal = FM2E.DALFactory.BasicAccess.CreateProducer();
            dal.UpdateProducer(item);
        }
        public ProducerInfo GetProducer(long id)
        {
            IProducer dal = FM2E.DALFactory.BasicAccess.CreateProducer();
            return dal.GetProducer(id);
        }
        public void DelProducer(long id)
        {
            IProducer dal = FM2E.DALFactory.BasicAccess.CreateProducer();
            dal.DelProducer(id);
        }
        public IList<ProducerInfo> Search(ProducerInfo item)
        {
            IProducer dal = FM2E.DALFactory.BasicAccess.CreateProducer();
            return dal.Search(item);
        }
        public QueryParam GenerateSearchTerm(ProducerInfo item)
        {
            IProducer dal = FM2E.DALFactory.BasicAccess.CreateProducer();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IProducer dal = FM2E.DALFactory.BasicAccess.CreateProducer();
            return dal.GetList(searchTerm, out recordCount);
        }
    }
}
