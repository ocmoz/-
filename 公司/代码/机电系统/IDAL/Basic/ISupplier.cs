using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Basic;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Basic
{
    public interface ISupplier
    {
        IList<SupplierInfo> GetAllSupplier();
        void InsertSupplier(SupplierInfo item);
        void UpdateSupplier(SupplierInfo item);
        SupplierInfo GetSupplier(long id);
        void DelSupplier(long id);
        IList<SupplierInfo> Search(SupplierInfo item);
        QueryParam GenerateSearchTerm(SupplierInfo item);
        IList GetList(QueryParam searchTerm, out int recordCount);
    }
}
