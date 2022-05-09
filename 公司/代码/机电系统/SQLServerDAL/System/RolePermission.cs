using System;
using System.Collections;
using System.Text;

using FM2E.Model.System;
using FM2E.Model.Exceptions;
using FM2E.IDAL.System;
using FM2E.SQLServerDAL.Utils;
using System.Data.SqlClient;
using System.Data;

namespace FM2E.SQLServerDAL.System
{
    public class RolePermission:IRolePermission
    {
        private RolePermissionInfo GetData(IDataReader rd)
        {
            RolePermissionInfo item = new RolePermissionInfo();

            if (!Convert.IsDBNull(rd["RoleID"]))
                item.RoleID = Convert.ToInt64(rd["RoleID"]);

            if (!Convert.IsDBNull(rd["ModuleID"]))
                item.ModuleID = Convert.ToString(rd["ModuleID"]);

            if (!Convert.IsDBNull(rd["Permission"]))
                item.Permission = Convert.ToInt32(rd["Permission"]);

            return item;
        }

        public IList GetRolePermissions(long roleID)
        {
            ArrayList list = new ArrayList();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select RoleID,ModuleID,Permission from FM2E_RolePermission  ");
                strSql.Append(" where RoleID=@RoleID ");

                SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.BigInt,8)};

                parameters[0].Value = roleID;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        RolePermissionInfo item = GetData(rd);
                        list.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取权限列表失败", e);
            }
            return list;
        }

        public IList GetRolePermissions(string userName)
        {
            ArrayList list = new ArrayList();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT FM2E_RolePermission.RoleID, FM2E_RolePermission.ModuleID, FM2E_RolePermission.Permission");
                strSql.Append(" FROM FM2E_User ");
                strSql.Append(" INNER JOIN FM2E_UserRole ON FM2E_User.UserName = FM2E_UserRole.UserName ");
                strSql.Append(" INNER JOIN FM2E_RolePermission ON FM2E_UserRole.RoleID = FM2E_RolePermission.RoleID ");
                strSql.Append(" WHERE FM2E_User.UserName=@UserName ");

                SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20)};

                parameters[0].Value = userName;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        RolePermissionInfo item = GetData(rd);
                        list.Add(item);
                    }
                }

            }
            catch (Exception e)
            {
                list.Clear();
                throw new DALException("获取用户角色列表失败", e);
            }
            return list;
        }

        public RolePermissionInfo GetRolePermissions(long roleID, string moduleID)
        {
            RolePermissionInfo item = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select top 1 RoleID,ModuleID,Permission from FM2E_RolePermission  ");
                strSql.Append(" where RoleID=@RoleID and ModuleID=@ModuleID ");

                SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.BigInt,8),
                    new SqlParameter("@ModuleID", SqlDbType.Char,32)};

                parameters[0].Value = roleID;
                parameters[1].Value = moduleID;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        item = GetData(rd);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取权限列表失败", e);
            }
            return item;
        }

        public void UpdateRolePermissions(IList permissions)
        {
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    StringBuilder updateSql = new StringBuilder();
                    updateSql.Append("update FM2E_RolePermission set ");
                    updateSql.Append("Permission=@Permission");
                    updateSql.Append(" where RoleID=@RoleID and ModuleID=@ModuleID ");

                    StringBuilder insertSql = new StringBuilder();
                    insertSql.Append("insert into FM2E_RolePermission(");
                    insertSql.Append("RoleID,ModuleID,Permission)");
                    insertSql.Append(" values (");
                    insertSql.Append("@RoleID,@ModuleID,@Permission)");

                    StringBuilder delSql = new StringBuilder();
                    delSql.Append("delete FM2E_RolePermission ");
                    delSql.Append(" where ModuleID=@ModuleID and RoleID=@RoleID ");

                    SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.BigInt,8),
					new SqlParameter("@ModuleID", SqlDbType.Char,32),
					new SqlParameter("@Permission", SqlDbType.Int,4)};


                    foreach (object item in permissions)
                    {
                        RolePermissionInfo model = (RolePermissionInfo)item;

                        parameters[0].Value = model.RoleID;
                        parameters[1].Value = model.ModuleID;
                        parameters[2].Value = model.Permission;

                        if (model.Permission == 0)
                        {
                            //删除

                            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, delSql.ToString(), parameters);
                            continue;
                        }

                        int count = SQLHelper.ExecuteNonQuery(trans, CommandType.Text, updateSql.ToString(), parameters);
                        if (count <= 0)
                        {
                            //表中没有相应的项，则插入
                            
                            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, insertSql.ToString(), parameters);
                        }
                    }

                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    throw new DALException("更新用户权限失败", e);
                }
            }
        }
        public void DeleteRolePermissions(long roleID)
        {
            try
            {
                //先删除所有的权限，再添加新权限
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_RolePermission ");
                strSql.Append(" where RoleID=@RoleID ");
                SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.BigInt)};
                parameters[0].Value = roleID;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除用户权限失败", e);
            }
        }
    }
}
