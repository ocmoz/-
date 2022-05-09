using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using FM2E.IDAL.Equipment;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Exceptions;
using FM2E.Model.Utils;
using FM2E.Model.Equipment;
namespace FM2E.SQLServerDAL.Equipment
{
    /// <summary>
    /// 数据访问类ConsumableEquipmentDetail。
    /// </summary>
    public class ConsumableEquipmentDetail : IConsumableEquipmentDetail
    {
        public ConsumableEquipmentDetail()
        { }
        #region  成员方法

        /// <summary>
        /// 获取一个实体
        /// </summary>
        private ConsumableEquipmentDetailInfo GetData(IDataReader rd)
        {
            ConsumableEquipmentDetailInfo item = new ConsumableEquipmentDetailInfo();
            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["ConsumableEquipmentID"]))
                item.ConsumableEquipmentID = Convert.ToInt64(rd["ConsumableEquipmentID"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["DetailLocation"]))
                item.DetailLocation = Convert.ToString(rd["DetailLocation"]);

            if (!Convert.IsDBNull(rd["AddressID"]))
                item.AddressID = Convert.ToInt64(rd["AddressID"]);

            if (!Convert.IsDBNull(rd["Count"]))
                item.Count = Convert.ToInt32(rd["Count"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            return item;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void InsertConsumableEquipmentDetail(ConsumableEquipmentDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_ConsumableEquipmentDetail(");
            strSql.Append("ID,ConsumableEquipmentID,CompanyID,DetailLocation,AddressID,Count,Remark)");
            strSql.Append(" values (");
            strSql.Append("@ID,@ConsumableEquipmentID,@CompanyID,@DetailLocation,@AddressID,@Count,@Remark)");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@ConsumableEquipmentID", SqlDbType.BigInt,8),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@DetailLocation", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressID", SqlDbType.BigInt,8),
					new SqlParameter("@Count", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.ConsumableEquipmentID;
            parameters[2].Value = model.CompanyID;
            parameters[3].Value = model.DetailLocation;
            parameters[4].Value = model.AddressID;
            parameters[5].Value = model.Count;
            parameters[6].Value = model.Remark;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                    if (result == 0)
                        throw new Exception("没有增加任何数据项");
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateConsumableEquipmentDetail(FM2E.Model.Equipment.ConsumableEquipmentDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_ConsumableEquipmentDetail set ");
            strSql.Append("ConsumableEquipmentID=@ConsumableEquipmentID,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("DetailLocation=@DetailLocation,");
            strSql.Append("AddressID=@AddressID,");
            strSql.Append("Count=@Count,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@ConsumableEquipmentID", SqlDbType.BigInt,8),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@DetailLocation", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressID", SqlDbType.BigInt,8),
					new SqlParameter("@Count", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.ConsumableEquipmentID;
            parameters[2].Value = model.CompanyID;
            parameters[3].Value = model.DetailLocation;
            parameters[4].Value = model.AddressID;
            parameters[5].Value = model.Count;
            parameters[6].Value = model.Remark;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                    if (result == 0)
                        throw new Exception("没有更新任何数据项");
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteConsumableEquipmentDetail(long ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from FM2E_ConsumableEquipmentDetail ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = ID;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                    if (result == 0)
                        throw new Exception("没有删除任何数据项");
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ConsumableEquipmentDetailInfo GetConsumableEquipmentDetail(long ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ConsumableEquipmentID,CompanyID,DetailLocation,AddressID,Count,Remark ");
            strSql.Append(" FROM FM2E_ConsumableEquipmentDetail ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = ID;

            ConsumableEquipmentDetailInfo item = new ConsumableEquipmentDetailInfo();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        item = this.GetData(rd);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取ConsumableEquipmentDetailInfo实体信息失败", e);
            }
            return item;
        }

        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        public IList GetAllConsumableEquipmentDetail()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ConsumableEquipmentID,CompanyID,DetailLocation,AddressID,Count,Remark ");
            strSql.Append(" FROM FM2E_ConsumableEquipmentDetail ");
            ArrayList list = new ArrayList();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        list.Add(GetData(rd));
                    }
                }
            }
            catch (Exception e)
            {
                list.Clear();
                throw new DALException("获取ConsumableEquipmentDetailInfo列表信息失败", e);
            }
            return list;
        }

        /// <summary>
        /// 获取查询实体
        /// </summary>
        public QueryParam GenerateSearchTerm(ConsumableEquipmentDetailInfo model)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = "FM2E_ConsumableEquipmentDetail";
            qp.ReturnFields = "ID,ConsumableEquipmentID,CompanyID,DetailLocation,AddressID,Count,Remark";
            qp.OrderBy = "";
            string sqlSearch = "where 1=1 ";

            if (model.ID != 0)
            {
                sqlSearch += " and ID =" + model.ID;
            }

            if (model.ConsumableEquipmentID != 0)
            {
                sqlSearch += " and ConsumableEquipmentID =" + model.ConsumableEquipmentID;
            }

            if (model.CompanyID != null && model.CompanyID.Trim() != string.Empty)
            {
                sqlSearch += " and CompanyID like '%" + model.CompanyID.Trim() + "%'";
            }

            if (model.DetailLocation != null && model.DetailLocation.Trim() != string.Empty)
            {
                sqlSearch += " and DetailLocation like '%" + model.DetailLocation.Trim() + "%'";
            }

            if (model.AddressID != 0)
            {
                sqlSearch += " and AddressID =" + model.AddressID;
            }

            if (model.Count != 0)
            {
                sqlSearch += " and Count =" + model.Count;
            }

            if (model.Remark != null && model.Remark.Trim() != string.Empty)
            {
                sqlSearch += " and Remark like '%" + model.Remark.Trim() + "%'";
            }

            qp.Where = sqlSearch;
            return qp;
        }

        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        public IList GetList(QueryParam term, out int recordCount)
        {
            if (term.Where == "")
            {
                term.TableName = "FM2E_ConsumableEquipmentDetail";
                term.ReturnFields = "ID,ConsumableEquipmentID,CompanyID,DetailLocation,AddressID,Count,Remark";
                term.OrderBy = "";
                term.Where = "where 1=1";
            }
            try
            {
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取ConsumableEquipmentDetailInfo列表分页失败", e);
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from FM2E_ConsumableEquipmentDetail");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = ID;

            return SQLHelper.Exists(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        #endregion  成员方法
    }
}