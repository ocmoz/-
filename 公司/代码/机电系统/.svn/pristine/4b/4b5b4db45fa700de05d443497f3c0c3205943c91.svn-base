using System;
using System.Collections.Generic;
using System.Text;

using FM2E.IDAL.System;
using FM2E.Model.System;
using System.Collections;

namespace FM2E.BLL.System
{
    public class RolePermission
    {

        public IList GetRolePermissions(long roleID)
        {
            IRolePermission dal = FM2E.DALFactory.SystemAccess.CreateRolePermission();
            return dal.GetRolePermissions(roleID);
        }
        public IList GetRolePermissions(string userName)
        {
            IRolePermission dal = FM2E.DALFactory.SystemAccess.CreateRolePermission();
            return dal.GetRolePermissions(userName);
        }
        public RolePermissionInfo GetRolePermissions(long roleID, string moduleID)
        {
            IRolePermission dal = FM2E.DALFactory.SystemAccess.CreateRolePermission();
            return dal.GetRolePermissions(roleID,moduleID);
        }
        public void UpdateRolePermissions(IList permissions)
        {
            IRolePermission dal = FM2E.DALFactory.SystemAccess.CreateRolePermission();
            dal.UpdateRolePermissions(permissions);
        }
        public void DeleteRolePermissions(long roleID)
        {
            IRolePermission dal = FM2E.DALFactory.SystemAccess.CreateRolePermission();
            dal.DeleteRolePermissions(roleID);
        }
    }
}
