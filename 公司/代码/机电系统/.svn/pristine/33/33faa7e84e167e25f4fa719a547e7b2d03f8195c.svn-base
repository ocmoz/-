using System;
using System.Collections;
using System.Text;

using FM2E.Model.System;

namespace FM2E.IDAL.System
{
    public interface IRolePermission
    {
        IList GetRolePermissions(long roleID);
        IList GetRolePermissions(string userName);
        RolePermissionInfo GetRolePermissions(long roleID, string moduleID);
        void UpdateRolePermissions(IList permissions);
        void DeleteRolePermissions(long roleID);
    }
}
