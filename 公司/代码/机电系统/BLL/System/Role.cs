using System;
using System.Collections.Generic;
using System.Text;

using FM2E.IDAL.System;
using FM2E.Model.System;
using System.Collections;

namespace FM2E.BLL.System
{
    public class Role
    {
        public RoleInfo GetRole(long id)
        {
            IRole dal = FM2E.DALFactory.SystemAccess.CreateRole();
            return dal.GetRole(id);
        }
        public IList GetAllRole()
        {
            IRole dal = FM2E.DALFactory.SystemAccess.CreateRole();
            return dal.GetAllRole();
        }
        public IList GetRoleByPage(int pageIndex, int pageSize, out int recordCount)
        {
            IRole dal = FM2E.DALFactory.SystemAccess.CreateRole();
            return dal.GetRoleByPage(pageIndex, pageSize, out recordCount);
        }
        public void AddRole(RoleInfo model)
        {
            IRole dal = FM2E.DALFactory.SystemAccess.CreateRole();
            dal.AddRole(model);
        }
        public void UpdateRole(RoleInfo model)
        {
            IRole dal = FM2E.DALFactory.SystemAccess.CreateRole();
            dal.UpdateRole(model);
        }
        public void DeleteRole(long id)
        {
            IRole dal = FM2E.DALFactory.SystemAccess.CreateRole();
            dal.DeleteRole(id);
        }
    }
}
