using System;
using System.Collections;
using System.Text;

using FM2E.Model.System;

namespace FM2E.IDAL.System
{
    public interface IRole
    {
        RoleInfo GetRole(long id);
        IList GetAllRole();
        IList GetRoleByPage(int pageIndex, int pageSize, out int recordCount);
        void AddRole(RoleInfo model);
        void UpdateRole(RoleInfo model);
        void DeleteRole(long id);
    }
}
