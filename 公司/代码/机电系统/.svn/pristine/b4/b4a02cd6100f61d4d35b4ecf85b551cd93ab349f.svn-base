using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Archives;
using FM2E.IDAL.Archives;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Archives
{
    /// <summary>
    ///  档案销毁业务逻辑处理类
    /// </summary>
    public class ArchivesDestroyApply
    {
        public ArchivesDestroyApplyInfo GetArchivesDestroyApply(long ID)
        {
            IArchivesDestroyApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesDestroyApply();
            return dal.GetArchivesDestroyApply(ID);
        }
        public long InsertArchivesDestroyApply(ArchivesDestroyApplyInfo model)
        {
            IArchivesDestroyApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesDestroyApply();
            return dal.InsertArchivesDestroyApply(model);
        }
        public void UpdateArchivesDestroyApply(ArchivesDestroyApplyInfo model)
        {
            IArchivesDestroyApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesDestroyApply();
            dal.UpdateArchivesDestroyApply(model);
        }
        public void DelArchivesDestroyApply(long ID)
        {
            IArchivesDestroyApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesDestroyApply();
            dal.DelArchivesDestroyApply(ID);
        }
        public QueryParam GenerateSearchTerm(ArchivesDestroyApplyInfo item)
        {
            IArchivesDestroyApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesDestroyApply();
            return dal.GenerateSearchTerm(item);
        }
        public QueryParam GenerateSearchTerm(ArchivesDestroyApplyInfo item, string[] WFStates)
        {
            IArchivesDestroyApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesDestroyApply();
            return dal.GenerateSearchTerm(item, WFStates);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IArchivesDestroyApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesDestroyApply();
            return dal.GetList(searchTerm, out recordCount);
        }
        public bool isDestroyedDetail(string archivesType, long id,string applicant)
        {
            IArchivesDestroyApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesDestroyApply();
            return dal.isDestroyedDetail(archivesType, id,applicant);
        }
        public void SetDestroyApplyDetailDestroyed(long ItemID)
        {
            IArchivesDestroyApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesDestroyApply();
            dal.SetDestroyApplyDetailDestroyed(ItemID);
        }
        public void SetDestroyApplyDestroyed(long ID)
        {
            IArchivesDestroyApply dal = FM2E.DALFactory.ArchivesAccess.CreateArchivesDestroyApply();
            dal.SetDestroyApplyDestroyed(ID);
        }
    }
}
