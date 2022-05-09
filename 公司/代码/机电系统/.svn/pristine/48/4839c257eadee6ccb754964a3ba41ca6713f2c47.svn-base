using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using FM2E.Model.Utils;

namespace FM2E.SQLServerDAL.Utils
{
    /// <summary>
    /// The SQLHelper class is intended to encapsulate high performance, 
    /// scalable best practices for common uses of SqlClient.
    /// </summary>
    public abstract class SQLHelper
    {

        //Database connection strings
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
        public static readonly string ConnectionStringRSMS = ConfigurationManager.ConnectionStrings["RSMS"].ConnectionString;

        // Hashtable to store cached parameters
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        public delegate object PopulateDelegate(IDataReader dr);

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) using an existing SQL Transaction 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">an existing sql transaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">查询语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>存在返回true，否则返回false</returns>
        public static bool Exists(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            using (SqlDataReader rd = SQLHelper.ExecuteReader(connectionString, cmdType, cmdText, commandParameters))
            {
                if (rd.Read())
                {
                    return true;
                }
                else return false;
            }
        }

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="connectionString">连接</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">查询语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>存在返回true，否则返回false</returns>
        public static bool Exists(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            using (SqlDataReader rd = SQLHelper.ExecuteReader(connection, cmdType, cmdText, commandParameters))
            {
                if (rd.Read())
                {
                    return true;
                }
                else return false;
            }
        }


        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="connectionString">事务</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">查询语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>存在返回true，否则返回false</returns>
        public static bool Exists(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            using (SqlDataReader rd = SQLHelper.ExecuteReader(trans, cmdType, cmdText, commandParameters))
            {
                if (rd.Read())
                {
                    return true;
                }
                else return false;
            }
        }
        /// <summary>
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader r = ExecuteReader(connection, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.Default);
            cmd.Parameters.Clear();
            return rdr;
        }


        /// <summary>
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader r = ExecuteReader(trans, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">a valid transaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.Default);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        public static object ExecuteScalar(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache</param>
        /// <param name="cmdParms">an array of SqlParamters to be cached</param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve cached parameters
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters</param>
        /// <returns>Cached SqlParamters array</returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// Prepare a command for execution
        /// </summary>
        /// <param name="cmd">SqlCommand object</param>
        /// <param name="conn">SqlConnection object</param>
        /// <param name="trans">SqlTransaction object</param>
        /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
        /// <param name="cmdText">Command text, e.g. Select * from Products</param>
        /// <param name="cmdParms">SqlParameters to use in the command</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// 公共查询数据函数Sql存储过程版
        /// </summary>
        /// <param name="pd">委托对象</param>
        /// <param name="pp">查询字符串</param>
        /// <returns>返回记录集ArrayList</returns>
        public static IList GetObjectList(PopulateDelegate pd, QueryParam pp,out int recordCount)
        {
            ArrayList lst = new ArrayList();
            recordCount = 0;
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("FM2E_QueryPager", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // 设置参数
                    cmd.Parameters.Add("@TableName", SqlDbType.NVarChar, 1000).Value = pp.TableName;
                    cmd.Parameters.Add("@ReturnFields", SqlDbType.NVarChar, 1000).Value = pp.ReturnFields;
                    cmd.Parameters.Add("@Where", SqlDbType.NVarChar, 1000).Value = pp.Where;
                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pp.PageIndex;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pp.PageSize;
                    cmd.Parameters.Add("@Order", SqlDbType.NVarChar, 200).Value = pp.OrderBy;
                    // 执行
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lst.Add(pd(dr));
                        }
                        // 取记录总数 及页数
                        if (dr.NextResult())
                        {
                            if (dr.Read())
                            {
                                recordCount = Convert.ToInt32(dr["RecordCount"]);
                            }
                        }

                    }
                }
            }
            return lst;
        }


        /// <summary>
        /// 公共查询数据函数Sql存储过程版，含有DISTINCT关键字的，pp的ReturnFields字段中不用加distinct标记
        /// </summary>
        /// <param name="pd">委托对象</param>
        /// <param name="pp">查询字符串</param>
        /// <returns>返回记录集ArrayList</returns>
        public static IList GetObjectListWithDistinct(PopulateDelegate pd, QueryParam pp, out int recordCount)
        {
            ArrayList lst = new ArrayList();
            recordCount = 0;
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("FM2E_QueryPagerWithDistinct", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // 设置参数
                    cmd.Parameters.Add("@TableName", SqlDbType.NVarChar, 1000).Value = pp.TableName;
                    cmd.Parameters.Add("@ReturnFields", SqlDbType.NVarChar, 1000).Value = pp.ReturnFields;
                    cmd.Parameters.Add("@Where", SqlDbType.NVarChar, 1000).Value = pp.Where;
                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pp.PageIndex;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pp.PageSize;
                    cmd.Parameters.Add("@Order", SqlDbType.NVarChar, 200).Value = pp.OrderBy;
                    // 执行
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lst.Add(pd(dr));
                        }
                        // 取记录总数 及页数
                        if (dr.NextResult())
                        {
                            if (dr.Read())
                            {
                                recordCount = Convert.ToInt32(dr["RecordCount"]);
                            }
                        }

                    }
                }
            }
            return lst;
        }


        /// <summary>
        /// 公共查询数据函数Sql存储过程版，带GroupBy，其中的QueryParam里面的GroupBy为包含如Count(COL),Sum(COL)等函数，
        /// 此函数的增加，为了分页正确By zjf 2009-1-11
        /// </summary>
        /// <param name="pd">委托对象</param>
        /// <param name="pp">查询字符串</param>
        /// <returns>返回记录集ArrayList</returns>
        public static IList GetObjectListWithGroupBy(PopulateDelegate pd, QueryParam pp, out int recordCount)
        {
            ArrayList lst = new ArrayList();
            recordCount = 0;
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("FM2E_QueryPagerWithGroupBy", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // 设置参数
                    cmd.Parameters.Add("@TableName", SqlDbType.NVarChar, 1000).Value = pp.TableName;
                    cmd.Parameters.Add("@ReturnFields", SqlDbType.NVarChar, 1000).Value = pp.ReturnFields;
                    cmd.Parameters.Add("@Where", SqlDbType.NVarChar, 1000).Value = pp.Where;
                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pp.PageIndex;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pp.PageSize;
                    cmd.Parameters.Add("@GroupKey", SqlDbType.NVarChar, 200).Value = pp.GroupKey;
                    cmd.Parameters.Add("@Order", SqlDbType.NVarChar, 200).Value = pp.OrderBy;
                    // 执行
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lst.Add(pd(dr));
                        }
                        // 取记录总数 及页数
                        if (dr.NextResult())
                        {
                            if (dr.Read())
                            {
                                recordCount = Convert.ToInt32(dr["RecordCount"]);
                            }
                        }

                    }
                }
            }
            return lst;
        }

        #region 修改 By tianmu
        /// <summary>
        /// 公共查询数据函数Sql存储过程版
        /// </summary>
        /// <returns>返回记录表</returns>
        public static DataTable GetObjectList(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            ArrayList lst = new ArrayList();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);

            try
            {
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }
            catch (Exception e)
            {

            }

            return ds.Tables[0];
        }
        #endregion
    }
}
