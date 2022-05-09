using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using FM2E.SQLServerDAL.Utils;
using FM2E.IDAL.System;
using FM2E.Model.System;
using FM2E.Model.Exceptions;
using FM2E.Model.Utils;

namespace FM2E.SQLServerDAL.System
{
    public class Module : IModule
    {
        private const string SELECT_SUBMODULES = "select * from FM2E_Module where [ParentID]='{0}' order by [OrderLevel]";
        private const string SELECT_SUBMODULESWITHOUTCLOSED = "select * from FM2E_Module where [ParentID]='{0}' and [IsClose]=0 order by [OrderLevel]";
        private const string UPDATE_MODULEORDER = " update FM2E_Module set [OrderLevel]={0} where [ModuleID]='{1}'";
        private const string SELECT_MODULE = "select * from FM2E_Module where [ModuleID]='{0}'";

        private ModuleInfo GetData(IDataReader dr)
        {
            ModuleInfo item = new ModuleInfo();
            if (!Convert.IsDBNull(dr["ModuleID"]))
                item.ModuleID = Convert.ToString(dr["ModuleID"]);

            if (!Convert.IsDBNull(dr["ParentID"]))
                item.ParentID = Convert.ToString(dr["ParentID"]);

            if (!Convert.IsDBNull(dr["Name"]))
                item.Name = Convert.ToString(dr["Name"]);

            if (!Convert.IsDBNull(dr["Directory"]))
                item.Directory = Convert.ToString(dr["Directory"]);

            if (!Convert.IsDBNull(dr["ChildCount"]))
                item.ChildCount = Convert.ToInt32(dr["ChildCount"]);

            if (!Convert.IsDBNull(dr["OrderLevel"]))
                item.OrderLevel = Convert.ToInt32(dr["OrderLevel"]);

            if (!Convert.IsDBNull(dr["IsSystem"]))
                item.IsSystem = Convert.ToBoolean(dr["IsSystem"]);

            if (!Convert.IsDBNull(dr["IsClose"]))
                item.IsClose = Convert.ToBoolean(dr["IsClose"]);

            return item;
        }

        public IList GetSubModules(string id,bool includeClosed)
        {
            string cmd=null;
            if (includeClosed)
                cmd = string.Format(SELECT_SUBMODULES, id);
            else cmd = string.Format(SELECT_SUBMODULESWITHOUTCLOSED, id);
            List<ModuleInfo> list = new List<ModuleInfo>();

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                    {
                        ModuleInfo item = GetData(rd);

                        list.Add(item);
                    }
                }
            }
            catch(Exception e)
            {
                throw new DALException("获取子模块失败", e);
            }
            return list;
        }

        public ModuleInfo GetModule(string moduleID)
        {
            ModuleInfo module = null;

            try
            {
                string cmd = string.Format(SELECT_MODULE, moduleID);
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                    {
                        module = GetData(rd);

                        break;
                    }
                }
            }
            catch(Exception e)
            {
                throw new DALException("获取模块信息失败",e);
            }

            return module;
        }

        public void AddModule(ModuleInfo model)
        {

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into FM2E_Module(");
                    strSql.Append("ModuleID,ParentID,Name,Directory,ChildCount,OrderLevel,IsSystem,IsClose)");
                    strSql.Append(" values (");
                    strSql.Append("@ModuleID,@ParentID,@Name,@Directory,@ChildCount,@OrderLevel,@IsSystem,@IsClose)");
                    SqlParameter[] parameters = {
					new SqlParameter("@ModuleID", SqlDbType.Char,32),
					new SqlParameter("@ParentID", SqlDbType.Char,32),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@Directory", SqlDbType.NVarChar,255),
					new SqlParameter("@ChildCount", SqlDbType.Int,4),
					new SqlParameter("@OrderLevel", SqlDbType.Int,4),
					new SqlParameter("@IsSystem", SqlDbType.Bit,1),
					new SqlParameter("@IsClose", SqlDbType.Bit,1)};
                    parameters[0].Value = model.ModuleID;
                    parameters[1].Value = model.ParentID;
                    parameters[2].Value = model.Name;
                    parameters[3].Value = model.Directory;
                    parameters[4].Value = model.ChildCount;
                    parameters[5].Value = model.OrderLevel;
                    parameters[6].Value = model.IsSystem;
                    parameters[7].Value = model.IsClose;
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                    string cmd = "update FM2E_Module set ChildCount = ChildCount + 1 where ModuleID ='" + model.ParentID + "'";
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, cmd, null);

                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    throw new DALException("添加模块失败", e);
                }
            }
        }
        public void OrderModules(string[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new DALException("非法参数");

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string cmd = null;
                    for (int i = 0; i < ids.Length; i++)
                    {
                        cmd = string.Format(UPDATE_MODULEORDER, i + 1,ids[i]);
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, cmd, null);
                    }
                    trans.Commit();
                }
                catch(Exception e)
                {
                    trans.Rollback();
                    throw new DALException("模块排序失败",e);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void UpdateModule(ModuleInfo model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_Module set ");
                strSql.Append("ParentID=@ParentID,");
                strSql.Append("Name=@Name,");
                strSql.Append("Directory=@Directory,");
                strSql.Append("ChildCount=@ChildCount,");
                strSql.Append("IsSystem=@IsSystem,");
                strSql.Append("IsClose=@IsClose");
                strSql.Append(" where ModuleID=@ModuleID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ModuleID", SqlDbType.Char,32),
					new SqlParameter("@ParentID", SqlDbType.Char,32),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@Directory", SqlDbType.NVarChar,255),
					new SqlParameter("@ChildCount", SqlDbType.Int,4),
					new SqlParameter("@IsSystem", SqlDbType.Bit,1),
					new SqlParameter("@IsClose", SqlDbType.Bit,1)};
                parameters[0].Value = model.ModuleID;
                parameters[1].Value = model.ParentID;
                parameters[2].Value = model.Name;
                parameters[3].Value = model.Directory;
                parameters[4].Value = model.ChildCount;
                parameters[5].Value = model.IsSystem;
                parameters[6].Value = model.IsClose;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch(Exception e)
            {
                throw new DALException("更新模块信息失败", e);
            }
        }

        /// <summary>
        /// 需要递归删除所有子节点
        /// </summary>
        /// <param name="moduleID"></param>
        public void DeleteModule(string moduleID)
        {
            string cmd = "delete FM2E_Module where ModuleID='{0}'";
            string updateChild = "update FM2E_Module set ChildCount=ChildCount-1 where ModuleID='{0}'";

            ModuleInfo item = GetModule(moduleID);

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    Queue<string> queue = new Queue<string>();
                    queue.Enqueue(moduleID);

                    if (item != null)
                    {
                        //更新父结点的ChildCount
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, string.Format(updateChild, item.ParentID), null);
                    }
                    string command = null;
                    while(queue.Count>0)
                    {
                        string id = queue.Dequeue();
                        command = string.Format(cmd, id);
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, command, null);

                        using (SqlDataReader rd = SQLHelper.ExecuteReader(trans, CommandType.Text, string.Format(SELECT_SUBMODULES, id), null))
                        {
                            while (rd.Read())
                            {
                                string tmp = null;
                                if (!Convert.IsDBNull(rd["ModuleID"]))
                                {
                                    tmp = Convert.ToString(rd["ModuleID"]);
                                    queue.Enqueue(tmp);
                                }
                            }
                        }
                    }
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    throw new DALException("删除模块信息失败", e);
                }
            }
        }

        /// <summary>
        /// 返回父id为id，且某用户有权访问的模块列表
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList GetUserModules(string userName, string id)
        {
            ArrayList list = new ArrayList();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT DISTINCT FM2E_Module.ModuleID as ModuleID, FM2E_Module.ParentID as ParentID, FM2E_Module.Name as Name, FM2E_Module.Directory as Directory, FM2E_Module.ChildCount as ChildCount, FM2E_Module.OrderLevel as OrderLevel, FM2E_Module.IsSystem as IsSystem, FM2E_Module.IsClose as IsClose ");
                strSql.Append(" FROM FM2E_UserRole INNER JOIN FM2E_RolePermission ON FM2E_UserRole.RoleID = FM2E_RolePermission.RoleID INNER JOIN FM2E_Module ON FM2E_RolePermission.ModuleID = FM2E_Module.ModuleID ");
                strSql.Append(" where FM2E_Module.ParentID=@ParentID and FM2E_UserRole.UserName=@UserName and FM2E_Module.IsClose=0 order by OrderLevel");

                SqlParameter[] parameters = {
					new SqlParameter("@ParentID", SqlDbType.Char,32),
                    new SqlParameter("@UserName", SqlDbType.VarChar,20)};
                parameters[0].Value = id;
                parameters[1].Value = userName;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        ModuleInfo item = GetData(rd);
                        list.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取用户能够访问的模块列表失败", e);
            }
            return list;
        }
    }
}
