using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Basic;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Basic
{
    public interface IDepartment
    {
        IList<DepartmentInfo> GetAllDepartment();
        DepartmentInfo GetDepartment(long id);

        long InsertDepartment(DepartmentInfo item);
        void UpdateDepartment(DepartmentInfo item);
        void DelDepartment(long id);
        IList<DepartmentInfo> Search(DepartmentInfo item);
        void DelDepartment(IList<DepartmentInfo> departments);
        QueryParam GenerateSearchTerm(DepartmentInfo item);
        IList GetList(QueryParam term, out int recordCount,string companyid);

        //long GenerateID();

        IList GetList(QueryParam term, out int recordCount, int level);
    }
}
