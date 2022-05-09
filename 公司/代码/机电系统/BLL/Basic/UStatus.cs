using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Basic;
using FM2E.Model.Basic;

namespace FM2E.BLL.Basic
{
    public class UStatus
    {
        public IList<UStatusInfo> GetAllUStatus()
        {
            IUStatus dal = FM2E.DALFactory.BasicAccess.CreateUStatus();
            return dal.GetAllUStatus();
        }
    }
}
