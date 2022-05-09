using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Archives;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Archives
{
    public interface IArchivesDestroyApply
    {
        ArchivesDestroyApplyInfo GetArchivesDestroyApply(long ID);
        long InsertArchivesDestroyApply(ArchivesDestroyApplyInfo model);
        void UpdateArchivesDestroyApply(ArchivesDestroyApplyInfo model);
        void DelArchivesDestroyApply(long ID);
        IList GetList(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(ArchivesDestroyApplyInfo item);
        QueryParam GenerateSearchTerm(ArchivesDestroyApplyInfo item,string[] WFStates);
        bool isDestroyedDetail(string archivesType, long id,string Applicant);
        void SetDestroyApplyDetailDestroyed(long ItemID);
        void SetDestroyApplyDestroyed(long ID);
    }
}
