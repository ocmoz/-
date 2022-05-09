using System;
using System.Collections.Generic;
using System.Text;

using FM2E.IDAL.Plan;
using System.Data.SqlClient;
using FM2E.Model.Schedule;
using System.Data.SqlTypes;
using FM2E.SQLServerDAL.Utils;
using System.Data;
using FM2E.Model.Exceptions;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Plan;

namespace FM2E.SQLServerDAL.Plan
{
    public class PlanInformation : IPlanInformation
    {
        #region 添加计划
        public void AddPlan(PlanInfo item)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FundsUsePlan(");
            strSql.Append("[year],[month],Department,Producer,ProducerTime)");
            strSql.Append(" values (");
            strSql.Append("@year,@month,@Department,@Producer,@ProducerTime)");
            strSql.Append(";select @@IDENTITY");

            SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@year",SqlDbType.NVarChar,50),
                    new SqlParameter("@month",SqlDbType.NVarChar,50),
                    new SqlParameter("@Department",SqlDbType.NVarChar,50),
                    new SqlParameter("@Producer",SqlDbType.NVarChar,50),
                    new SqlParameter("@ProducerTime",SqlDbType.DateTime)                  
            };
                param[0].Value = item.Year;
                param[1].Value = item.Month;
                param[2].Value = item.Department;
                param[3].Value = item.Producer;
                param[4].Value = item.ProducerTime;              

                using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
                {
                    conn.Open();
                    try
                    {
                        int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), param);
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
        #endregion

        #region 修改计划
        public void UpdatePlan(PlanInfo item)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FundsUsePlan set");
            strSql.Append("[year]=@year,[month]=@month,UseReasonsDifferences=@UseReasonsDifferences,IncomeReasonsDifferences=@IncomeReasonsDifferences,DepartmentManager=@DepartmentManager,DepartmentManagerRemark=@DepartmentManagerRemark,DepartmentManagerTime=@DepartmentManagerTime");
            strSql.Append(" where id=@id");

            SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@year",SqlDbType.NVarChar,50),
                    new SqlParameter("@month",SqlDbType.NVarChar,50),
                    new SqlParameter("@UseReasonsDifferences",SqlDbType.NVarChar,50),
                    new SqlParameter("@IncomeReasonsDifferences",SqlDbType.NVarChar,50),
                    new SqlParameter("@DepartmentManager",SqlDbType.NVarChar,50),
                    new SqlParameter("@DepartmentManagerRemark",SqlDbType.NVarChar,50),
                    new SqlParameter("@DepartmentManagerTime",SqlDbType.DateTime) ,
                     new SqlParameter("@Id",SqlDbType.BigInt,8)
            };
            param[0].Value = item.Year;
            param[1].Value = item.Month;
            param[2].Value = string.IsNullOrEmpty(item.UseReasonsDifferences) ? SqlString.Null : item.UseReasonsDifferences; ;
            param[3].Value = string.IsNullOrEmpty(item.IncomeReasonsDifferences) ? SqlString.Null : item.IncomeReasonsDifferences; 
            param[4].Value = string.IsNullOrEmpty(item.DepartmentManager) ? SqlString.Null : item.DepartmentManager;
            param[5].Value = string.IsNullOrEmpty(item.DepartmentManagerRemark) ? SqlString.Null : item.DepartmentManagerRemark; 
            param[6].Value = item.DepartmentManagerTime; 
            param[7].Value = item.Id;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), param);
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
        #endregion


        #region 获取计划列表
        public IList GetPlanList(QueryParam term, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetPlanListInfo, term, out recordCount);
            }
            catch (Exception ex)
            {
                throw new DALException("获取计划表分页失败", ex);
            }
        }
        #endregion

        #region 获取计划详情
        public PlanInfo GetPlan(PlanInfo item)
        {
            PlanInfo scheduleInfo = null;
            try
            {
                string queryStr = "select * from FM2E_PlanView where 1=1";
                if (item.Id != null&&item.Id!=0)
                    queryStr += " and id=" + item.Id;
                if (item.Year != null)
                    queryStr += " and [Year]='" + item.Year + "'";
                if (item.Month != null)
                    queryStr += " and [Month]='" + item.Month + "'";
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, queryStr, null))
                {
                    if (rd.Read())
                        scheduleInfo = GetPlanListInfo(rd);
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取计划信息失败", e);
            }
            return scheduleInfo;
        }
        #endregion

        private PlanInfo GetPlanListInfo(IDataReader dr)
        {
            PlanInfo item = new PlanInfo();
            if (!Convert.IsDBNull(dr["ID"]))
            {
                item.Id = Convert.ToInt32(dr["ID"]);
            }
            if (!Convert.IsDBNull(dr["Year"]))
            {
                item.Year = Convert.ToString(dr["Year"]);
            }
            if (!Convert.IsDBNull(dr["Month"]))
            {
                item.Month = Convert.ToString(dr["Month"]);
            }
            if (!Convert.IsDBNull(dr["Department"]))
            {
                item.Department = Convert.ToString(dr["Department"]);
            }
            if (!Convert.IsDBNull(dr["Producer"]))
            {
                item.Producer = Convert.ToString(dr["Producer"]);
            }
            if (!Convert.IsDBNull(dr["ProducerTime"]))
            {
                item.ProducerTime = Convert.ToDateTime(dr["ProducerTime"]);
            }
          
            if (!Convert.IsDBNull(dr["UseReasonsDifferences"]))
            {
                item.UseReasonsDifferences = Convert.ToString(dr["UseReasonsDifferences"]);
            }
            if (!Convert.IsDBNull(dr["IncomeReasonsDifferences"]))
            {
                item.IncomeReasonsDifferences = Convert.ToString(dr["IncomeReasonsDifferences"]);
            }
            if (!Convert.IsDBNull(dr["DepartmentManagerRemark"]))
            {
                item.DepartmentManagerRemark = Convert.ToString(dr["DepartmentManagerRemark"]);
            }
            if (!Convert.IsDBNull(dr["DepartmentManager"]))
            {
                item.DepartmentManager = Convert.ToString(dr["DepartmentManager"]);
            }

            if (!Convert.IsDBNull(dr["DepartmentManagerTime"]))
            {
                item.DepartmentManagerTime = Convert.ToDateTime(dr["DepartmentManagerTime"]);
            }
            if (!Convert.IsDBNull(dr["accountingRemark"]))
            {
                item.AccountingRemark = Convert.ToString(dr["accountingRemark"]);
            }
            if (!Convert.IsDBNull(dr["accounting"]))
            {
                item.Accounting = Convert.ToString(dr["accounting"]);
            }
            if (!Convert.IsDBNull(dr["accountingTime"]))
            {
                item.AccountingTime = Convert.ToDateTime(dr["accountingTime"]);
            }
            if (!Convert.IsDBNull(dr["financialRemark"]))
            {
                item.FinancialRemark = Convert.ToString(dr["financialRemark"]);
            }
            if (!Convert.IsDBNull(dr["financial"]))
            {
                item.Financial = Convert.ToString(dr["financial"]);
            }
            if (!Convert.IsDBNull(dr["financialTime"]))
            {
                item.FinancialTime = Convert.ToDateTime(dr["financialTime"]);
            }
              if (!Convert.IsDBNull(dr["operatingRemark"]))
            {
                item.OperatingRemark = Convert.ToString(dr["operatingRemark"]);
            }
            if (!Convert.IsDBNull(dr["operating"]))
            {
                item.Operating = Convert.ToString(dr["operating"]);
            }
            if (!Convert.IsDBNull(dr["operatingTime"]))
            {
                item.OperatingTime = Convert.ToDateTime(dr["operatingTime"]);
            }
            if (!Convert.IsDBNull(dr["Remark"]))
            {
                item.Remark = Convert.ToString(dr["Remark"]);
            }

            if (!Convert.IsDBNull(dr["MonthUse"]))
            {
                item.MonthUse = Convert.ToDecimal(dr["MonthUse"]);
            }
            if (!Convert.IsDBNull(dr["MonthIncome"]))
            {
                item.MonthIncome = Convert.ToDecimal(dr["MonthIncome"]);
            }      

            return item;
        }

        #region 删除计划
        public void DelPlan(int id)
        {

            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                DelPlan(trans, id);

                //事务提交
                trans.Commit();
            }
            catch (SqlException sqlex)
            {
                //回滚
                trans.Rollback();
                throw sqlex;
            }
            catch (Exception ex)
            {
                //回滚
                trans.Rollback();
                throw ex;
            }
            finally
            {
                //关闭连接
                if (trans != null)
                {
                    trans.Dispose();
                    trans = null;
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
        }

        public void DelPlan(SqlTransaction trans ,int id)
        { 
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FundsUsePlan where id=@ID; ");
                strSql.Append("delete FM2E_Schedule where Planid=@ID; ");
                strSql.Append("delete FM2E_ScheduleActual where Planid=@ID; ");
                strSql.Append("delete FM2E_ScheduleIncome where Planid=@ID; ");
                strSql.Append("delete FM2E_ScheduleIncomeActual where Planid=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                parameters[0].Value = id;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                
            }
            catch (Exception e)
            {
                throw new DALException("删除计划失败", e);
            }
        }
        #endregion

        #region 获取上月计划金额
        public PlanInfo GetLastPlan(PlanInfo item)
        {
            PlanInfo scheduleInfo = null;
            try
            {
                string queryStr = "select b.LastMonthUse,c.LastMonthIncome from dbo.FundsUsePlan a LEFT OUTER JOIN (select planid, sum(amount) LastMonthUse from FM2E_Schedule group by planid )b on a.Id=b.PlanId LEFT OUTER JOIN (select planid, sum(amount) LastMonthIncome from FM2E_ScheduleIncome group by planid )c on a.Id=c.PlanId where 1=1 ";
              
                if (item.Year != null)
                    queryStr += " and a.[Year]='" + item.Year + "'";
                if (item.Month != null)
                    queryStr += " and a.[Month]='" + item.Month + "'";
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, queryStr, null))
                {
                    if (rd.Read())
                        scheduleInfo = GetLastPlanInfo(rd);
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取计划信息失败", e);
            }
            return scheduleInfo;
        }

        private PlanInfo GetLastPlanInfo(IDataReader dr)
        {
            PlanInfo item = new PlanInfo();
            if (!Convert.IsDBNull(dr["LastMonthUse"]))
            {
                item.LastMonthUse = Convert.ToDecimal(dr["LastMonthUse"]);
            }
            if (!Convert.IsDBNull(dr["LastMonthIncome"]))
            {
                item.LastMonthIncome = Convert.ToDecimal(dr["LastMonthIncome"]);
            }
            return item;
        }
        #endregion

        #region 获取资金使用计划明细表
        public List<ScheduleInfo> GetSchedule(int id)
        {
            List<ScheduleInfo> list = new List<ScheduleInfo>();
            try
            {
                string queryStr = "select * from FM2E_Schedule where PlanId=" + id;
             
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, queryStr, null))
                {
                    while(rd.Read())
                        list.Add(GetScheduleInfo(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取计划信息失败", e);
            }
            return list;
        }
        #endregion

        #region 获取资金实际使用明细表
        public List<ScheduleInfo> GetScheduleActual(int id)
        {
            List<ScheduleInfo> list = new List<ScheduleInfo>();
            try
            {
                string queryStr = "select * from FM2E_ScheduleActual where PlanId=" + id;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, queryStr, null))
                {
                    while(rd.Read())
                        list.Add(GetScheduleInfo(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取计划信息失败", e);
            }
            return list;
        }

        #endregion

        #region 获取资金使用计划明细表分组
        public List<ScheduleInfo> GetScheduleGroupBy(int id)
        {
            List<ScheduleInfo> list = new List<ScheduleInfo>();
            try
            {
                string queryStr = "select Projectname,SUM(amount) SumAmount from FM2E_Schedule where PlanId=" + id;
                queryStr += " group by projectName";
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, queryStr, null))
                {
                    while (rd.Read())
                        list.Add(GetScheduleInfoGroupBy(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取计划信息失败", e);
            }
            return list;
        }

        #endregion

        #region 获取资金收入计划明细表
        public List<ScheduleInfo> GetScheduleIncome(int id)
        {
            List<ScheduleInfo> list = new List<ScheduleInfo>();
            try
            {
                string queryStr = "select * from FM2E_ScheduleIncome where PlanId=" + id;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, queryStr, null))
                {
                    while (rd.Read())
                        list.Add(GetScheduleInfo(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取计划信息失败", e);
            }
            return list;
        }
        #endregion

        #region 获取资金实际收入明细表
        public List<ScheduleInfo> GetScheduleIncomeActual(int id)
        {
            List<ScheduleInfo> list = new List<ScheduleInfo>();
            try
            {
                string queryStr = "select * from FM2E_ScheduleIncomeActual where PlanId=" + id;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, queryStr, null))
                {
                    while (rd.Read())
                        list.Add(GetScheduleInfo(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取计划信息失败", e);
            }
            return list;
        }

        #endregion

        #region 获取资金使用计划明细表分组
        public List<ScheduleInfo> GetScheduleIncomeGroupBy(int id)
        {
            List<ScheduleInfo> list = new List<ScheduleInfo>();
            try
            {
                string queryStr = "select Projectname,SUM(amount) SumAmount from FM2E_ScheduleIncome where PlanId=" + id;
                queryStr += " group by projectName";
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, queryStr, null))
                {
                    while (rd.Read())
                        list.Add(GetScheduleInfoGroupBy(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取计划信息失败", e);
            }
            return list;
        }

        #endregion

        #region 修改添加用款计划明细
        public void DelSchedule(long id)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                DelSchedule(trans, id);

                //事务提交
                trans.Commit();
            }
            catch (SqlException sqlex)
            {
                //回滚
                trans.Rollback();
                throw sqlex;
            }
            catch (Exception ex)
            {
                //回滚
                trans.Rollback();
                throw ex;
            }
            finally
            {
                //关闭连接
                if (trans != null)
                {
                    trans.Dispose();
                    trans = null;
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
        }
        public void DelSchedule(SqlTransaction trans, long id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_Schedule where Planid=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                parameters[0].Value = id;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除失败", e);
            }
        }

        public void AddSchedule(ScheduleInfo item)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_Schedule(");
            strSql.Append("PlanId,ProjectName,Content,ContractNo,Amount,PaymentTime,Remark)");
            strSql.Append(" values (");
            strSql.Append("@PlanId,@ProjectName,@Content,@ContractNo,@Amount,@PaymentTime,@Remark)");
            strSql.Append(";select @@IDENTITY");

            SqlParameter[] param = new SqlParameter[]{                  
                    new SqlParameter("@PlanId",SqlDbType.BigInt,8),
                    new SqlParameter("@ProjectName",SqlDbType.NVarChar,50),
                    new SqlParameter("@Content",SqlDbType.NVarChar,50), 
                    new SqlParameter("@ContractNo",SqlDbType.NVarChar,50),
                    new SqlParameter("@Amount",SqlDbType.Decimal),
                    new SqlParameter("@PaymentTime",SqlDbType.DateTime),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,1000)
            };

            param[0].Value = item.PlanId;
            param[1].Value = item.ProjectName;
            param[2].Value = string.IsNullOrEmpty(item.Content) ? SqlString.Null : item.Content;
            param[3].Value = string.IsNullOrEmpty(item.ContractNo) ? SqlString.Null : item.ContractNo;
            param[4].Value = item.Amount;
            param[5].Value = item.PaymentTime;
            param[6].Value = string.IsNullOrEmpty(item.Remark) ? SqlString.Null : item.Remark;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), param);
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
        #endregion

        #region 添加修改实际用款明细
        public void DelScheduleActual(long id)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                DelScheduleActual(trans, id);

                //事务提交
                trans.Commit();
            }
            catch (SqlException sqlex)
            {
                //回滚
                trans.Rollback();
                throw sqlex;
            }
            catch (Exception ex)
            {
                //回滚
                trans.Rollback();
                throw ex;
            }
            finally
            {
                //关闭连接
                if (trans != null)
                {
                    trans.Dispose();
                    trans = null;
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
        }
        public void DelScheduleActual(SqlTransaction trans, long id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_ScheduleActual where Planid=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                parameters[0].Value = id;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除失败", e);
            }
        }

        public void AddScheduleActual(ScheduleInfo item)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_ScheduleActual(");
            strSql.Append("PlanId,ProjectName,Content,ContractNo,Amount,PaymentTime,Remark)");
            strSql.Append(" values (");
            strSql.Append("@PlanId,@ProjectName,@Content,@ContractNo,@Amount,@PaymentTime,@Remark)");
            strSql.Append(";select @@IDENTITY");

            SqlParameter[] param = new SqlParameter[]{                  
                    new SqlParameter("@PlanId",SqlDbType.BigInt,8),
                    new SqlParameter("@ProjectName",SqlDbType.NVarChar,50),
                    new SqlParameter("@Content",SqlDbType.NVarChar,50), 
                    new SqlParameter("@ContractNo",SqlDbType.NVarChar,50),
                    new SqlParameter("@Amount",SqlDbType.Decimal),
                    new SqlParameter("@PaymentTime",SqlDbType.DateTime),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,1000)
            };

            param[0].Value = item.PlanId;
            param[1].Value = item.ProjectName;
            param[2].Value = string.IsNullOrEmpty(item.Content) ? SqlString.Null : item.Content;
            param[3].Value = string.IsNullOrEmpty(item.ContractNo) ? SqlString.Null : item.ContractNo;
            param[4].Value = item.Amount;
            param[5].Value = item.PaymentTime;
            param[6].Value = string.IsNullOrEmpty(item.Remark) ? SqlString.Null : item.Remark;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), param);
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
        #endregion

        #region 添加修改收入计划明细
        public void DelScheduleIncome(long id)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                DelScheduleIncome(trans, id);

                //事务提交
                trans.Commit();
            }
            catch (SqlException sqlex)
            {
                //回滚
                trans.Rollback();
                throw sqlex;
            }
            catch (Exception ex)
            {
                //回滚
                trans.Rollback();
                throw ex;
            }
            finally
            {
                //关闭连接
                if (trans != null)
                {
                    trans.Dispose();
                    trans = null;
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
        }
        public void DelScheduleIncome(SqlTransaction trans, long id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_ScheduleIncome where planid=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                parameters[0].Value = id;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除失败", e);
            }
        }

        public void AddScheduleIncome(ScheduleInfo item)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_ScheduleIncome(");
            strSql.Append("PlanId,ProjectName,Content,ContractNo,Amount,PaymentTime,Remark)");
            strSql.Append(" values (");
            strSql.Append("@PlanId,@ProjectName,@Content,@ContractNo,@Amount,@PaymentTime,@Remark)");
            strSql.Append(";select @@IDENTITY");

            SqlParameter[] param = new SqlParameter[]{                  
                    new SqlParameter("@PlanId",SqlDbType.BigInt,8),
                    new SqlParameter("@ProjectName",SqlDbType.NVarChar,50),
                    new SqlParameter("@Content",SqlDbType.NVarChar,50), 
                    new SqlParameter("@ContractNo",SqlDbType.NVarChar,50),
                    new SqlParameter("@Amount",SqlDbType.Decimal),
                    new SqlParameter("@PaymentTime",SqlDbType.DateTime),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,1000)
            };

            param[0].Value = item.PlanId;
            param[1].Value = item.ProjectName;
            param[2].Value = string.IsNullOrEmpty(item.Content) ? SqlString.Null : item.Content;
            param[3].Value = string.IsNullOrEmpty(item.ContractNo) ? SqlString.Null : item.ContractNo;
            param[4].Value = item.Amount;
            param[5].Value = item.PaymentTime;
            param[6].Value = string.IsNullOrEmpty(item.Remark) ? SqlString.Null : item.Remark;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), param);
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
        #endregion

        #region 添加修改实际收入明细
        public void DelScheduleIncomeActual(long id)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                DelScheduleIncomeActual(trans, id);

                //事务提交
                trans.Commit();
            }
            catch (SqlException sqlex)
            {
                //回滚
                trans.Rollback();
                throw sqlex;
            }
            catch (Exception ex)
            {
                //回滚
                trans.Rollback();
                throw ex;
            }
            finally
            {
                //关闭连接
                if (trans != null)
                {
                    trans.Dispose();
                    trans = null;
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
        }
        public void DelScheduleIncomeActual(SqlTransaction trans, long id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_ScheduleIncomeActual where Planid=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                parameters[0].Value = id;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除失败", e);
            }
        }

        public void AddScheduleIncomeActual(ScheduleInfo item)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_ScheduleIncomeActual(");
            strSql.Append("PlanId,ProjectName,Content,ContractNo,Amount,PaymentTime,Remark)");
            strSql.Append(" values (");
            strSql.Append("@PlanId,@ProjectName,@Content,@ContractNo,@Amount,@PaymentTime,@Remark)");
            strSql.Append(";select @@IDENTITY");

            SqlParameter[] param = new SqlParameter[]{                  
                    new SqlParameter("@PlanId",SqlDbType.BigInt,8),
                    new SqlParameter("@ProjectName",SqlDbType.NVarChar,50),
                    new SqlParameter("@Content",SqlDbType.NVarChar,50), 
                    new SqlParameter("@ContractNo",SqlDbType.NVarChar,50),
                    new SqlParameter("@Amount",SqlDbType.Decimal),
                    new SqlParameter("@PaymentTime",SqlDbType.DateTime),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,1000)
            };

            param[0].Value = item.PlanId;
            param[1].Value = item.ProjectName;
            param[2].Value = string.IsNullOrEmpty(item.Content) ? SqlString.Null : item.Content;
            param[3].Value = string.IsNullOrEmpty(item.ContractNo) ? SqlString.Null : item.ContractNo;
            param[4].Value = item.Amount;
            param[5].Value = item.PaymentTime;
            param[6].Value = string.IsNullOrEmpty(item.Remark) ? SqlString.Null : item.Remark;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), param);
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
        #endregion

        private ScheduleInfo GetScheduleInfo(IDataReader dr)
        {
            ScheduleInfo item = new ScheduleInfo();
            if (!Convert.IsDBNull(dr["ProjectName"]))
            {
                item.ProjectName = Convert.ToString(dr["ProjectName"]);
            }
            if (!Convert.IsDBNull(dr["Content"]))
            {
                item.Content = Convert.ToString(dr["Content"]);
            }
            if (!Convert.IsDBNull(dr["ContractNo"]))
            {
                item.ContractNo = Convert.ToString(dr["ContractNo"]);
            }
            if (!Convert.IsDBNull(dr["Amount"]))
            {
                item.Amount = Convert.ToDecimal(dr["Amount"]);
            }
            if (!Convert.IsDBNull(dr["PaymentTime"]))
            {
                item.PaymentTime = Convert.ToDateTime(dr["PaymentTime"]);
            }
            if (!Convert.IsDBNull(dr["Remark"]))
            {
                item.Remark = Convert.ToString(dr["Remark"]);
            }
            return item;
        }

        private ScheduleInfo GetScheduleInfoGroupBy(IDataReader dr)
        {
            ScheduleInfo item = new ScheduleInfo();
            if (!Convert.IsDBNull(dr["ProjectName"]))
            {
                item.ProjectName = Convert.ToString(dr["ProjectName"]);
            }
            if (!Convert.IsDBNull(dr["Sumamount"]))
            {
                item.SumAmount = Convert.ToDecimal(dr["Sumamount"]);
            }          
            return item;
        }

        #region 获取统计
        public List<PlanInfo> GetStatistics(DateTime dt1, DateTime dt2)
        {
            List<PlanInfo> list = new List<PlanInfo>();
            try
            {
                while (dt1 <= dt2)
                {
                    DateTime dt = dt1.AddMonths(-1);

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select sumSchedule,sumScheduleActual,sumScheduleIncome,sumScheduleIncomeActual from dbo.FundsUsePlan a ");
                    strSql.Append(" LEFT OUTER JOIN (select planid, sum(amount) sumSchedule from FM2E_Schedule group by planid)b on a.Id=b.PlanId ");
                    strSql.Append(" LEFT OUTER JOIN (select planid, sum(amount) sumScheduleActual from FM2E_ScheduleActual group by planid)c on a.Id=c.PlanId ");
                    strSql.Append(" LEFT OUTER JOIN (select planid, sum(amount) sumScheduleIncome from FM2E_ScheduleIncome group by planid)d on a.Id=d.PlanId ");
                    strSql.Append(" LEFT OUTER JOIN (select planid, sum(amount) sumScheduleIncomeActual from FM2E_ScheduleIncomeActual group by planid)e on a.Id=e.PlanId ");
                    strSql.Append(" where a.[year]=@year and a.[month]=@month");

                    SqlParameter[] param = new SqlParameter[]{                  
                    new SqlParameter("@year",SqlDbType.NVarChar,50),
                    new SqlParameter("@month",SqlDbType.NVarChar,50)
                    };

                    param[0].Value = dt.Year;
                    param[1].Value = dt.Month;



                    using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), param))
                    {
                        while (rd.Read())
                        {
                            PlanInfo item = new PlanInfo();

                            item.Year = Convert.ToString(dt1.Year);
                            item.Month = Convert.ToString(dt1.Month);

                            if (!Convert.IsDBNull(rd["sumSchedule"]))
                            {
                                item.SumSchedule = Convert.ToDecimal(rd["sumSchedule"]);
                            }
                            if (!Convert.IsDBNull(rd["sumScheduleActual"]))
                            {
                                item.SumScheduleActual = Convert.ToDecimal(rd["sumScheduleActual"]);
                            }
                            item.SumScheduleDifferent = item.SumSchedule - item.SumScheduleActual;
                            if (item.SumScheduleActual!=0)
                                item.SumScheduleDifferentRatio = item.SumScheduleDifferent / item.SumScheduleActual;

                            if (!Convert.IsDBNull(rd["sumScheduleIncome"]))
                            {
                                item.SumScheduleIncome = Convert.ToDecimal(rd["sumScheduleIncome"]);
                            }
                            if (!Convert.IsDBNull(rd["sumScheduleIncomeActual"]))
                            {
                                item.SumScheduleIncomeActual = Convert.ToDecimal(rd["sumScheduleIncomeActual"]);
                            }
                            item.SumScheduleIncomeDifferent = item.SumScheduleIncome - item.SumScheduleIncomeActual;
                            if (item.SumScheduleIncomeActual!=0)
                                item.SumScheduleIncomeDifferentRatio = item.SumScheduleIncomeDifferent / item.SumScheduleIncomeActual;

                            list.Add(item);
                        }
                    }
                   dt1= dt1.AddMonths(1);
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取计划信息失败", e);
            }
            return list;
        }
        private ScheduleInfo GetStatisticsInfo(IDataReader dr)
        {
            ScheduleInfo item = new ScheduleInfo();
            if (!Convert.IsDBNull(dr["ProjectName"]))
            {
                item.ProjectName = Convert.ToString(dr["ProjectName"]);
            }
            if (!Convert.IsDBNull(dr["Sumamount"]))
            {
                item.SumAmount = Convert.ToDecimal(dr["Sumamount"]);
            }
            return item;
        }
        #endregion
    }
}
