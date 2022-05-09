using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Basic;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Basic
{
    public interface IProducer
    {
        IList<ProducerInfo> GetAllProducer();
        void InsertProducer(ProducerInfo item);
        void UpdateProducer(ProducerInfo item);
        ProducerInfo GetProducer(long id);
        void DelProducer(long id);
        IList<ProducerInfo> Search(ProducerInfo item);
        //IList GetSubModuleByPage(string id, int pageIndex, int pageSize, out int recordCount);
        QueryParam GenerateSearchTerm(ProducerInfo item);
        IList GetList(QueryParam searchTerm, out int recordCount);
    }
}
