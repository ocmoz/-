using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.IDAL.Basic
{
    public interface ISection
    {
        IList<SectionInfo> GetAllSection();
        IList<SectionInfo> GetAllSectionByCompany(string companyid);
        SectionInfo GetSection(string Sectionid);
        void DelSection(string Sectionid);
        IList<SectionInfo> Search(SectionInfo item);
        void InsertSection(SectionInfo item);
        void UpdateSection(SectionInfo item);
        QueryParam GenerateSearchTerm(SectionInfo item);
        IList GetList(QueryParam searchTerm, out int recordCount);
    }
}
