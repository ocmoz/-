﻿using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.IDAL.Basic
{
    public interface ICompany
    {
        IList<CompanyInfo> GetAllCompany();
        CompanyInfo GetCompany(string id);

        void InsertCompany(CompanyInfo item);
        void UpdateCompany(CompanyInfo item);
        void DelCompany(string id);
        List<CompanyInfo> Search(CompanyInfo item);
        List<CompanyInfo> Search(string companyName);
        QueryParam GenerateSearchTerm(CompanyInfo item);
        IList GetList(QueryParam term, out int recordCount);
    }
}
