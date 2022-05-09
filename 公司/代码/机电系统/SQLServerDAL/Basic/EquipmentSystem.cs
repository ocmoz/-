using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using FM2E.SQLServerDAL.Utils;
using FM2E.IDAL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Exceptions;
using FM2E.Model.Utils;

namespace FM2E.SQLServerDAL.Basic
{
    public class EquipmentSystem:IEquipmentSystem
    {
        #region SQL语句定义
        private const string TBALE_NAME = "FM2E_System";
        private const string SELECT_ALLSYSTEM = 
            "SELECT * FROM "
            +TBALE_NAME+
            " WHERE [IsDeleted]=0";
        private const string INSERT_SYSTEM =
            "INSERT INTO "
            + TBALE_NAME + "([SystemID],[SystemName],[Remark],[IsDeleted]) "
            + "VALUES(@SystemID,@SystemName,@Remark,0)";
        private const string UPDATE_SYSTEM=
            "UPDATE "+TBALE_NAME
            +" SET [SystemName]=@SystemName,[Remark]=@Remark"
            + " WHERE [SystemID]=@SystemID";
        private const string SELECT_SYSTEM=
            "SELECT [SystemID],[SystemName],[Remark],[IsDeleted] "
            +"FROM "+TBALE_NAME
            + " WHERE [IsDeleted]=0 AND [SystemID]='{0}'";
        private const string DEL_SYSTEM=
            "UPDATE "+TBALE_NAME
            +" SET [IsDeleted]=1"
            + " WHERE [SystemID]='{0}'";
        private const string SELECT_ALLSUBSYSTEM =
            "SELECT [SubSystemID],[SubSystemName],[ParentSystemID],[Remark],[IsDeleted] "
            + "FROM FM2E_SubSystem "
            + "WHERE [IsDeleted]=0 AND [ParentSystemID]='{0}'"
            + "Order By [SubSystemID] DESC";
        private const string SELECT_SUBSYSTEM =
            "SELECT [SubSystemID],[SubSystemName],[ParentSystemID],[Remark],[IsDeleted] "
            + "FROM FM2E_SubSystem "
            + "WHERE [IsDeleted]=0 AND [SubSystemID]='{0}' ";
        private const string UPDATE_SUBSYSTEM =
            "UPDATE FM2E_SubSystem "
            + "SET [SubSystemName]=@SubSystemName,[ParentSystemID]=@ParentSystemID,[Remark]=@Remark "
            + "WHERE [SubSystemID]=@SubSystemID";
        private const string INSERT_SUBSYSTEM =
            "INSERT INTO "
            + "FM2E_SubSystem([SubSystemName],[ParentSystemID],[Remark],[IsDeleted]) "
            + "VALUES(@SubSystemName,@ParentSystemID,@Remark,0)";
        private const string DEL_SUBSYSTEM =
            "UPDATE FM2E_SubSystem "
            + "SET [IsDeleted]=1"
            + " WHERE [SubSystemID]='{0}'";

        private const string PARAM_SYSTEMID = "@SystemID";
        private const string PARAM_SYSTEMNAME = "@SystemName";
        private const string PARAM_REMARK = "@Remark";
        #endregion 

        private EquipmentSystemInfo GetData(IDataReader rd)
        {
            EquipmentSystemInfo item = new EquipmentSystemInfo();
            if (!Convert.IsDBNull(rd["SystemID"]))
                item.SystemID = Convert.ToString(rd["SystemID"]);

            if (!Convert.IsDBNull(rd["SystemName"]))
                item.SystemName = Convert.ToString(rd["SystemName"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["IsDeleted"]))
                item.IsDeleted = Convert.ToBoolean(rd["IsDeleted"]);

            return item;
        }
        private SubEquipmentSystemInfo GetSubData(IDataReader rd)
        {
            SubEquipmentSystemInfo item = new SubEquipmentSystemInfo();
            if (!Convert.IsDBNull(rd["SubSystemID"]))
                item.SubSystemID = Convert.ToInt32(rd["SubSystemID"]);
            if (!Convert.IsDBNull(rd["ParentSystemID"]))
                item.ParentSystemID = Convert.ToString(rd["ParentSystemID"]);
            if (!Convert.IsDBNull(rd["SubSystemName"]))
                item.SubSystemName = Convert.ToString(rd["SubSystemName"]);
            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);
            if (!Convert.IsDBNull(rd["IsDeleted"]))
                item.IsDeleted = Convert.ToBoolean(rd["IsDeleted"]);

            return item;
        }
        private static SqlParameter[] GetInsertUpdateSubParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(INSERT_SUBSYSTEM);
            if (param == null)
            {
                param = new SqlParameter[]
                {
                    new SqlParameter("@SubSystemID",SqlDbType.BigInt),
                    new SqlParameter("@SubSystemName",SqlDbType.NVarChar,20),
                    new SqlParameter("@ParentSystemID",SqlDbType.VarChar,2),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,1000),
                };
                SQLHelper.CacheParameters(INSERT_SUBSYSTEM, param);
            }
            return param;
        }
        private static SqlParameter[] GetInsertUpdateParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(INSERT_SYSTEM);
            if (param == null)
            {
                param = new SqlParameter[]
                {
                   new SqlParameter(PARAM_SYSTEMID,SqlDbType.VarChar,2),
                   new SqlParameter(PARAM_SYSTEMNAME,SqlDbType.NVarChar,20),
                   new SqlParameter(PARAM_REMARK,SqlDbType.NVarChar,100),
                };
                SQLHelper.CacheParameters(INSERT_SYSTEM, param);
            }
            return param;
        }

        public IList GetAllSystem()
        {
            ArrayList list = new ArrayList();

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, SELECT_ALLSYSTEM, null))
                {
                    while (rd.Read())
                    {
                        EquipmentSystemInfo item = GetData(rd);

                        list.Add(item);
                    }
                }
            }
            catch
            {
                throw;
            }
            return list;
        }
        public EquipmentSystemInfo GetSystem(string id)
        {
            string cmd = string.Format(SELECT_SYSTEM, id);
            EquipmentSystemInfo item = null;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                    {
                        item = GetData(rd);
                    }
                }
            }
            catch
            {
                throw;
            }
            return item;
        }
        public void InsertSystem(EquipmentSystemInfo item)
        {
            SqlParameter[] param = GetInsertUpdateParam();
            param[0].Value = item.SystemID;
            param[1].Value = item.SystemName;
            param[2].Value = item.Remark;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, INSERT_SYSTEM, param);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void UpdateSystem(EquipmentSystemInfo item)
        {
            SqlParameter[] param = GetInsertUpdateParam();
            param[0].Value = item.SystemID;
            param[1].Value = item.SystemName;
            param[2].Value = item.Remark;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, UPDATE_SYSTEM, param);
                    if (result == 0)
                    {
                        throw new Exception("没有更新任何数据项");
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void DelSystem(string id)
        {
            string cmd = string.Format(DEL_SYSTEM, id);
            try
            {
                int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, cmd, null);
                if (rows == 0)
                {
                    throw new Exception("没有删除任何数据！");
                }
            }
            catch
            {
                throw;
            }
        }
        public IList GetAllSubSystem(string id)
        {
            string cmd = string.Format(SELECT_ALLSUBSYSTEM, id);
            SubEquipmentSystemInfo item = null;
            ArrayList list=new ArrayList();
            try
            {
                using(SqlDataReader rd=SQLHelper.ExecuteReader(SQLHelper.ConnectionString,CommandType.Text,cmd,null))
                {
                    while (rd.Read())
                    {
                        item = GetSubData(rd);
                        list.Add(item);
                    }
                }
            }catch
            {
                throw;
            }
            return list;
        }
        public SubEquipmentSystemInfo GetSubSystem(string id)
        {
            int I = Convert.ToInt32(id);
            string cmd = string.Format(SELECT_SUBSYSTEM, I);
            SubEquipmentSystemInfo item = null;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while(rd.Read())
                    {
                        item=GetSubData(rd);
                    }
                }
            }
            catch
            {
                throw;
            }

            return item;
        }
        public void InsertSubSystem(SubEquipmentSystemInfo item)
        {
            SqlParameter[] param = GetInsertUpdateSubParam();
            param[0].Value = item.SubSystemID;
            param[1].Value = item.SubSystemName;
            param[2].Value = item.ParentSystemID;
            param[3].Value = item.Remark;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                     SQLHelper.ExecuteNonQuery(conn, CommandType.Text,INSERT_SUBSYSTEM, param);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void UpdateSubSystem(SubEquipmentSystemInfo item)
        {
            SqlParameter[] param = GetInsertUpdateSubParam();
            param[0].Value = item.SubSystemID;
            param[1].Value = item.SubSystemName;
            param[2].Value = item.ParentSystemID;
            param[3].Value = item.Remark;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, UPDATE_SUBSYSTEM, param);
                    if (result == 0)
                    {
                        throw new Exception("没有更新任何数据项");
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void DelSubSystem(string id)
        {
            string cmd = string.Format(DEL_SUBSYSTEM, id);
            try
            {
                int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, cmd, null);
                if (rows == 0)
                {
                    throw new Exception("没有删除任何数据！");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}