using System;
using System.Collections.Generic;
using System.Text;

using FM2E.IDAL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Basic
{
    public class Company
    {
        public IList<CompanyInfo> GetAllCompany()
        {
            ICompany dal = FM2E.DALFactory.BasicAccess.CreateCompany();
            return dal.GetAllCompany();
        }

        public CompanyInfo GetCompany(string id)
        {
            ICompany dal = FM2E.DALFactory.BasicAccess.CreateCompany();
            return dal.GetCompany(id);
        }


        public void InsertCompany(CompanyInfo item)
        {
            ICompany dal = FM2E.DALFactory.BasicAccess.CreateCompany();
            dal.InsertCompany(item);
        }

        public void UpdateCompany(CompanyInfo item)
        {
            ICompany dal = FM2E.DALFactory.BasicAccess.CreateCompany();
            dal.UpdateCompany(item);
        }

        public void DelCompany(string id)
        {
            ICompany dal = FM2E.DALFactory.BasicAccess.CreateCompany();
            dal.DelCompany(id);
        }

        public List<CompanyInfo> Search(CompanyInfo item)
        {
            ICompany dal = FM2E.DALFactory.BasicAccess.CreateCompany();
            return dal.Search(item);
        }

        public List<CompanyInfo> SearchForBudgetManagement(string companyName)
        {
            ICompany dal = FM2E.DALFactory.BasicAccess.CreateCompany();
            return dal.Search(companyName);
        }

        public QueryParam GenerateSearchTerm(CompanyInfo item)
        {
            ICompany dal = FM2E.DALFactory.BasicAccess.CreateCompany();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam term, out int recordCount)
        {
            ICompany dal = FM2E.DALFactory.BasicAccess.CreateCompany();
            return dal.GetList(term, out recordCount);
        }

    }
}
