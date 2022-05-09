using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Basic;
using FM2E.Model.Basic;


namespace FM2E.BLL.Basic
{
    public class RStatus
    {
        public IList<RStatusInfo> GetAllRStatus()
        {
            IRStatus dal = FM2E.DALFactory.BasicAccess.CreateRStatus();
            return dal.GetAllRStatus();
        }
    }
}
