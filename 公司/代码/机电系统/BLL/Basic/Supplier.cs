using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Basic;
using FM2E.Model.Basic;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.BLL.Basic
{
    public class Supplier
    {
        public IList<SupplierInfo> GetAllSupplier()
        {
            ISupplier dal = FM2E.DALFactory.BasicAccess.CreateSupplier();
            return dal.GetAllSupplier();
        }
        public void InsertSupplier(SupplierInfo item)
        {
            ISupplier dal = FM2E.DALFactory.BasicAccess.CreateSupplier();
            dal.InsertSupplier(item);
        }
        public void UpdateSupplier(SupplierInfo item)
        {
            ISupplier dal = FM2E.DALFactory.BasicAccess.CreateSupplier();
            dal.UpdateSupplier(item);
        }
        public SupplierInfo GetSupplier(long id)
        {
            ISupplier dal = FM2E.DALFactory.BasicAccess.CreateSupplier();
            return dal.GetSupplier(id);
        }
        public void DelSupplier(long id)
        {
            ISupplier dal = FM2E.DALFactory.BasicAccess.CreateSupplier();
            dal.DelSupplier(id);
        }
        public IList<SupplierInfo> Search(SupplierInfo item)
        {
            ISupplier dal = FM2E.DALFactory.BasicAccess.CreateSupplier();
            return dal.Search(item);
        }
        public QueryParam GenerateSearchTerm(SupplierInfo item)
        {
            ISupplier dal = FM2E.DALFactory.BasicAccess.CreateSupplier();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            ISupplier dal = FM2E.DALFactory.BasicAccess.CreateSupplier();
            return dal.GetList(searchTerm, out recordCount);
        }
    }
}
