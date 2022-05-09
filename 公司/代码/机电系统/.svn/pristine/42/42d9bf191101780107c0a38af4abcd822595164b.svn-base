using System;
using System.Collections;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Basic;
using FM2E.IDAL.Basic;
using FM2E.Model.Exceptions;
using FM2E.Model.Utils;
using System.Collections.Generic;

namespace FM2E.SQLServerDAL.Basic
{
    public class Position : IPosition
    {
        private const string SELECT_ALLPOSITION = "select * from FM2E_Position where [IsDeleted]=0";

        private const string SELECT_POSITION = "select [PositionID],[PositionName],[Remark],[IsDeleted] from FM2E_Position where [PositionID]='{0}'";

        private const string SELECT_POSITION_BY_NAME = "select [PositionID],[PositionName],[Remark],[IsDeleted] from FM2E_Position where [PositionName]='{0}'";

        private const string INSERT_POSITION = "insert into FM2E_Position([PositionName],[Remark],[IsDeleted]) "
                                            + "values(@PositionName,@Remark,0)";

        private const string UPDATE_POSITION = "update FM2E_Position "
                                            + "set [PositionName]=@PositionName,[Remark]=@Remark where [PositionID]=@PositionID";

        private const string DEL_POSITION = "update FM2E_Position set [IsDeleted]=1 where [PositionID]='{0}'";

        private const string PARAM_POSITIONID = "@PositionID";
        private const string PARAM_POSITIONNAME = "@PositionName";
        private const string PARAM_REMARK = "@Remark";
        private const string PARAM_ISDELETED = "@IsDeleted";

        private const string TABLE_NAME = "FM2E_POSITION";


        private PositionInfo GetData(IDataReader rd)
        {
            PositionInfo item = new PositionInfo();
            if (!Convert.IsDBNull(rd["PositionID"]))
                item.PositionID = Convert.ToInt64(rd["PositionID"]);

            if (!Convert.IsDBNull(rd["PositionName"]))
                item.PositionName = Convert.ToString(rd["PositionName"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["IsDeleted"]))
                item.IsDeleted = Convert.ToBoolean(rd["IsDeleted"]);

            return item;
        }
        public IList<PositionInfo> GetAllPosition()
        {
            IList<PositionInfo> list = new List<PositionInfo>();

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, SELECT_ALLPOSITION, null))
                {
                    while (rd.Read())
                    {
                        PositionInfo item = GetData(rd);

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

        public PositionInfo GetPosition(long id)
        {
            string cmd = string.Format(SELECT_POSITION, id);
            PositionInfo item = null;
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

        public void InsertPosition(PositionInfo item)
        {
            SqlParameter[]  param = new SqlParameter[]{
                    new SqlParameter(PARAM_POSITIONNAME,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_REMARK,SqlDbType.NVarChar,100),};
            param[0].Value = item.PositionName;
            param[1].Value = item.Remark;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, INSERT_POSITION, param);
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

        public void UpdatePosition(PositionInfo item)
        {
            SqlParameter[] param = GetInsertUpdateParam();
            param[0].Value = item.PositionID;
            param[1].Value = item.PositionName;
            param[2].Value = item.Remark;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, UPDATE_POSITION, param);
                    if (result == 0)
                        throw new Exception("没有更新任何数据项");
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

        private static SqlParameter[] GetInsertUpdateParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(INSERT_POSITION);
            if (param == null)
            {
                param = new SqlParameter[]{
                    new SqlParameter(PARAM_POSITIONID,SqlDbType.BigInt,8),
                    new SqlParameter(PARAM_POSITIONNAME,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_REMARK,SqlDbType.NVarChar,100),
                };
                SQLHelper.CacheParameters(INSERT_POSITION, param);
            }
            return param;
        }

        public void DelPosition(long id)
        {
            string cmd = string.Format(DEL_POSITION, id);
            try
            {
                int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, cmd, null);
                if (rows == 0)
                    throw new Exception("没有删除任何数据项！");
            }
            catch
            {
                throw;
            }
        }

        public IList GetPositionByPage(int pageIndex, int pageSize, out int recordCount)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME;
                qp.ReturnFields = "*";
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where [IsDeleted]=0";
                qp.OrderBy = "order by PositionID desc";
                return SQLHelper.GetObjectList(this.GetData, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取职位分页失败", e);
            }
        }
        /// <summary>
        /// 判断职位是否已经存在
        /// </summary>
        /// <param name="positionname"></param>
        /// <returns></returns>
        bool IPosition.IfExists(string positionname)
        {
            string cmd = string.Format(SELECT_POSITION_BY_NAME, positionname);

            return SQLHelper.Exists(SQLHelper.ConnectionString, CommandType.Text, cmd, null);
        }

    }
}
