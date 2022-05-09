using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using FM2E.IDAL.Insurance;
using FM2E.Model.Equipment;
using FM2E.Model.Insurance;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Utils;
using System.Collections;
using FM2E.Model.Exceptions;
using System.Data.SqlTypes;
using FM2E.Model.Basic;
using FM2E.SQLServerDAL.Basic;

namespace FM2E.SQLServerDAL.Insurance
{
    public class Insurance : IInsurance
    {

        private const string QUERY_ALL_INSURANCE_INFO = "select * from FM2E_InsuranceInfo order by id";

        private const string QUERY_INSURANCE_INFO_BY_ID = "select * from FM2E_InsuranceInfo where id = ";

        private const string INSERT_INSURANCE_INFO =
            "Insert into FM2E_InsuranceInfo ([InsuranceNo],[InsureTarget],[StartDate],[EndDate],[InsuranceType]) values(@InsuranceNo,@InsureTarget,@StartDate,@EndDate,@InsuranceType)";

        private const string UPDATE_INSURANCE_INFO =
            "Update FM2E_InsuranceInfo set [InsuranceNo]=@InsuranceNo,[InsureTarget]=@InsureTarget,[StartDate]=@StartDate,[EndDate]=@EndDate,[InsuranceType]=@InsuranceType where [Id] = @Id";

        private const string DELETE_INSURANCE_INFO = "delete from FM2E_InsuranceInfo where [ID]='{0}'";
        
         public void InsertInsurance(InsuranceInfo item)
         {
             SqlParameter[] param = GetInsertParam();
             param[0].Value = item.InsuranceNo;
             param[1].Value = item.InsureTarget;
             param[2].Value = item.StartDate;
             param[3].Value = item.EndDate;
             param[4].Value = item.InsuranceType;

             using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
             {
                 conn.Open();
                 try
                 {
                     int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, INSERT_INSURANCE_INFO, param);
                     if (result == 0)
                         throw new Exception("没有添加任何数据项");
                 }
                 catch (Exception e)
                 {
                     throw e;// DALException("插入设备信息失败", e);
                 }
                 finally
                 {
                     conn.Close();
                 }
             }
         }  

  


       

        public IList GetAllInsurance()
        {
            IList list = new List<InsuranceInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, QUERY_ALL_INSURANCE_INFO, null))
                {
                    while (rd.Read())
                        list.Add(GetInsuranceInfo(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取所有设备信息时失败", e);
            }
            return list;
        }

       

        public void UpdateInsurance(InsuranceInfo item)
        {
            SqlParameter[] param = GetUpdateParam();

            param[0].Value = item.InsuranceNo;
            param[1].Value = item.InsureTarget;
            param[2].Value = item.StartDate;
            param[3].Value = item.EndDate;
            param[4].Value = item.InsuranceType;
            param[5].Value = item.InsuranceId;
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, UPDATE_INSURANCE_INFO, param);
                    if (result == 0)
                        throw new Exception("没有更新任何数据项");
                }
                catch (Exception e)
                {
                    throw e;// DALException("插入设备信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void DelInsurance(string id)
        {
            string cmd = string.Format(DELETE_INSURANCE_INFO, id);
            try
            {
                int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, cmd, null);
                if (rows == 0)
                    throw new Exception("没有删除任何数据项！");
            }
            catch (Exception e)
            {
                throw new DALException("删除保单信息失败", e);
            }
        }

        public InsuranceInfo GetInsuranceInfo(long id)
        {
            InsuranceInfo insuranceInfo = null;
            try
            {
                string queryStr = QUERY_INSURANCE_INFO_BY_ID + id;
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, queryStr, null))
                {
                    if (rd.Read())
                        insuranceInfo=GetInsuranceInfo(rd);
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取保单信息失败", e);
            }
            return insuranceInfo;
        }

        public InsuranceInfo GetInsuranceInfo(string insuranceNo)
        {
            return null;
        }
        /// <summary>
        /// 查询考核表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetInsurances(QueryParam term, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetInsuranceInfo, term, out recordCount);
            }
            catch (Exception ex)
            {
                throw new DALException("获取考核表分页失败", ex);
            }
        }


        public QueryParam GetSearchTerm(InsuranceSearchInfo term)
        {
            QueryParam qp = new QueryParam();
            qp.ReturnFields = "*";
            qp.TableName = "FM2E_InsuranceInfo";
            qp.Where = GetSqlWhere(term);
            qp.OrderBy = "order by Id desc";

            return qp;
        }


        private string GetSqlWhere(InsuranceSearchInfo term)
        {
            string sqlSearch = "where 1=1 ";

            if (!string.IsNullOrEmpty(term.InsuranceNo))
                sqlSearch += " and InsuranceNo like '%" + term.InsuranceNo + "%'";

            if (!string.IsNullOrEmpty(term.InsureTarget))
                sqlSearch += " and InsureTarget like '%" + term.InsureTarget + "%'";

            if (term.InsuranceType != 0)
                sqlSearch += " and InsuranceType=" + (int)term.InsuranceType;
            if (DateTime.Compare(term.StartDate, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(term.StartDate, sqlMinDate) < 0)
                    term.StartDate = sqlMinDate;

                sqlSearch += " and StartDate<='" + term.StartDate.ToString("yyyy-M-d") + "'";
            }
            if (DateTime.Compare(term.EndDate, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(term.EndDate, sqlMinDate) < 0)
                    term.EndDate = sqlMinDate;

                sqlSearch += " and EndDate>='" + term.EndDate.ToString("yyyy-M-d") + "'";
            }
           
            return sqlSearch;
        }

        /// <summary>
        /// 获取Insert InsuranceInfo得SQLPararment
        /// </summary>
        /// <returns></returns>
        private static SqlParameter[] GetInsertParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(INSERT_INSURANCE_INFO);
            if (param == null)
            {
                param = new SqlParameter[]{
                    new SqlParameter("@InsuranceNo",SqlDbType.VarChar,30),
                    new SqlParameter("@InsureTarget",SqlDbType.VarChar,50),
                    new SqlParameter("@StartDate",SqlDbType.DateTime),
                    new SqlParameter("@EndDate",SqlDbType.DateTime),
                    new SqlParameter("@InsuranceType",SqlDbType.Int,2) 
                };
                SQLHelper.CacheParameters(INSERT_INSURANCE_INFO, param);
            }
            return param;
        }

        private static SqlParameter[] GetUpdateParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(UPDATE_INSURANCE_INFO);
            if (param == null)
            {
                param = new SqlParameter[]{
                    new SqlParameter("@InsuranceNo",SqlDbType.VarChar,30),
                    new SqlParameter("@InsureTarget",SqlDbType.VarChar,50),
                    new SqlParameter("@StartDate",SqlDbType.DateTime),
                    new SqlParameter("@EndDate",SqlDbType.DateTime),
                    new SqlParameter("@InsuranceType",SqlDbType.Int,2),
                    new SqlParameter("@Id",SqlDbType.BigInt,32) 
                };
                SQLHelper.CacheParameters(UPDATE_INSURANCE_INFO, param);
            }
            return param;
        }


        private InsuranceInfo GetInsuranceInfo(IDataReader dr)
        {
            InsuranceInfo item = new InsuranceInfo();
            if (!Convert.IsDBNull(dr["Id"]))
            {
                item.InsuranceId = Convert.ToInt64(dr["Id"]);
            }
            if (!Convert.IsDBNull(dr["InsuranceNo"]))
            {
                item.InsuranceNo = Convert.ToString(dr["InsuranceNO"]);
            }
            if (!Convert.IsDBNull(dr["InsureTarget"]))
            {
                item.InsureTarget = Convert.ToString(dr["InsureTarget"]);
            }
            if (!Convert.IsDBNull(dr["StartDate"]))
            {
                item.StartDate = Convert.ToDateTime(dr["StartDate"]);
            }
            if (!Convert.IsDBNull(dr["EndDate"]))
            {
                item.EndDate = Convert.ToDateTime(dr["EndDate"]);
            }
            if (!Convert.IsDBNull(dr["InsuranceType"]))
            {
                item.InsuranceType = (InsuranceType)Convert.ToInt32(dr["InsuranceType"]);
            }
            return item;
        }
   
    }
}
