using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using FM2E.Model.System;
using FM2E.Model.Utils;
using FM2E.IDAL.System;
using FM2E.Model.Exceptions;
using FM2E.SQLServerDAL.Utils;
using System.Collections;



namespace FM2E.SQLServerDAL.System
{
    public class Role : IRole
    {
        private RoleInfo GetData(IDataReader rd)
        {
            RoleInfo item = new RoleInfo();

            if (!Convert.IsDBNull(rd["RoleID"]))
                item.RoleID = Convert.ToInt64(rd["RoleID"]);

            if (!Convert.IsDBNull(rd["RoleName"]))
                item.RoleName = Convert.ToString(rd["RoleName"]);

            if (!Convert.IsDBNull(rd["Description"]))
                item.Description = Convert.ToString(rd["Description"]);

            return item;
        }
        public RoleInfo GetRole(long id)
        {
            RoleInfo item=null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select top 1 RoleID,RoleName,Description from FM2E_Role ");
                strSql.Append(" where RoleID=@RoleID ");
                SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                        item = GetData(rd);
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取角色资料失败", e);
            }
            return item;

        }
        public IList GetAllRole()
        {
            ArrayList list = new ArrayList();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select RoleID,RoleName,Description ");
                strSql.Append(" FROM FM2E_Role ");

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        RoleInfo item = GetData(rd);
                        list.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取角色列表失败", e);
            }
            return list;
        }
        public void AddRole(RoleInfo model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FM2E_Role(");
                strSql.Append("RoleName,Description)");
                strSql.Append(" values (");
                strSql.Append("@RoleName,@Description)");
                SqlParameter[] parameters = {
					new SqlParameter("@RoleName", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,255)};
                parameters[0].Value = model.RoleName;
                parameters[1].Value = model.Description;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("添加角色失败", e);
            }

        }
        public void UpdateRole(RoleInfo model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_Role set ");
                strSql.Append("RoleName=@RoleName,");
                strSql.Append("Description=@Description");
                strSql.Append(" where RoleID=@RoleID ");
                SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.BigInt,8),
					new SqlParameter("@RoleName", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,255)};
                parameters[0].Value = model.RoleID;
                parameters[1].Value = model.RoleName;
                parameters[2].Value = model.Description;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("更新角色资料失败", e);
            }
        }
        public void DeleteRole(long id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_Role ");
                strSql.Append(" where RoleID=@RoleID ");
                SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除角色失败", e);
            }
        }

        public IList GetRoleByPage(int pageIndex, int pageSize, out int recordCount)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = "FM2E_Role";
                qp.ReturnFields = "*";
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.OrderBy = "Order by RoleID desc";

                return SQLHelper.GetObjectList(this.GetData, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取角色分页失败", e);
            }

        }
    }
}
