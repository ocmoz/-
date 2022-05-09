using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Archives;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Archives
{
    public interface IArchivesBorrowApply
    {
        ArchivesBorrowApplyInfo GetArchivesBorrowApply(long ID);
        long InsertArchivesBorrowApply(ArchivesBorrowApplyInfo model);
        void UpdateArchivesBorrowApply(ArchivesBorrowApplyInfo model);
        void DelArchivesBorrowApply(long ID);
        IList GetList(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(ArchivesBorrowApplyInfo item);
        QueryParam GenerateSearchTerm(ArchivesBorrowApplyInfo item, string[] WFStates);
        bool isBorrowedDetail(string archivesType, long id,string Applicant);

        QueryParam GenerateDetailSearchTerm(ArchivesBorrowApplyDetailInfo item, string Applicant);
        IList GetDetailList(QueryParam searchTerm, out int recordCount);
    }
}
