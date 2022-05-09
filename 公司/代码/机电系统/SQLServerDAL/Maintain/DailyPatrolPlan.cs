using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using FM2E.IDAL.Maintain;
using FM2E.Model.Maintain;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Utils;
using System.Collections;
using FM2E.Model.Exceptions;

namespace FM2E.SQLServerDAL.Maintain
{
    public class DailyPatrolPlan : IDailyPatrolPlan
    {
        public QueryParam GenerateSearchTerm(DailyPatrolPlanInfo item, string[] WFStates)
        {
            string sqlSearch = "where 1=1";
            if (item.PlanID != 0)
                sqlSearch += " and a.PlanID ='" + item.PlanID + "'";

            if (item.CompanyID != "")
                sqlSearch += " and a.CompanyID ='" + item.CompanyID + "'";

            if (item.PlanName != "")
                sqlSearch += " and a.PlanName like '%" + item.PlanName + "%'";

            if (item.Planner != "")
                sqlSearch += " and a.Planner ='" + item.Planner + "'";

            if (item.PlannerName != "")
                sqlSearch += " and b.PersonName like '%" + item.PlannerName + "%'";

            if (item.Approvaler != null && item.Approvaler != "")
                sqlSearch += " and a.Approvaler ='" + item.Approvaler + "'";

            if (item.Status > 0)
                sqlSearch += " and a.Status ='" + (int)item.Status + "'";

            if (WFStates != null && WFStates.Length > 0)
            {
                sqlSearch += "and h.TableName='FM2E_DailyPatrolPlan' and (";
                bool first = true;
                foreach (string wfstate in WFStates)
                {
                    if (first)
                    {
                        sqlSearch += "CurrentStateName='" + wfstate + "'";
                        first = false;
                    }
                    else
                        sqlSearch += "or CurrentStateName='" + wfstate + "'";
                }
                sqlSearch += ")";
            }
            else
            {
                sqlSearch = "where 1=0";
            }
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_DailyPatrolPlan a left join FM2E_User b on a.Planner=b.UserName left join FM2E_User c on a.Approvaler=c.UserName left join FM2E_Company g on a.CompanyID = g.CompanyID left join FM2E_WorkflowInstance h on a.PlanID=h.DataID left join FM2E_Department s on s.DepartmentID=a.DepartmentID";
            searchTerm.ReturnFields = "a.*,b.PersonName as PlannerName,c.PersonName as ApprovalerName,g.CompanyName as CompanyName,s.Name as DepartmentName";
            searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by PlanDate desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        public QueryParam GenerateSearchTerm(DailyPatrolPlanInfo item)
        {
            string sqlSearch = "where 1=1";
            if (item.PlanID != 0)
                sqlSearch += " and a.PlanID ='" + item.PlanID + "'";

            if (item.CompanyID != "")
                sqlSearch += " and a.CompanyID ='" + item.CompanyID + "'";

            if (item.PlanName != "")
                sqlSearch += " and a.PlanName like '%" + item.PlanName + "%'";

            if (item.Planner != "")
                sqlSearch += " and a.Planner ='" + item.Planner + "'";

            if (item.PlannerName != "")
                sqlSearch += " and b.PersonName like '%" + item.PlannerName + "%'";

            if (item.Approvaler != null && item.Approvaler != "")
                sqlSearch += " and a.Approvaler ='" + item.Approvaler + "'";

            if (item.Status > 0)
                sqlSearch += " and a.Status ='" + (int)item.Status + "'";

            if (item.StatusArray != null && item.StatusArray.Count > 0)
            {
                for (int i = 0; i < item.StatusArray.Count; i++)
                {
                    if (i == 0)
                    {
                        sqlSearch += " and ( ";
                        sqlSearch += " " + " a.Status=" + (int)item.StatusArray[i] + " ";
                    }
                    else
                    {
                        sqlSearch += " or " + " a.Status=" + (int)item.StatusArray[i] + " ";
                    }
                    if (i == item.StatusArray.Count - 1)
                    {
                        sqlSearch += " ) ";
                    }
                }
            }

            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_DailyPatrolPlan a left join FM2E_User b on a.Planner=b.UserName left join FM2E_User c on a.Approvaler=c.UserName left join FM2E_Company g on a.CompanyID = g.CompanyID left join FM2E_Department s on s.DepartmentID=a.DepartmentID ";
            searchTerm.ReturnFields = "a.*,b.PersonName as PlannerName,c.PersonName as ApprovalerName,g.CompanyName as CompanyName,s.Name as DepartmentName";
            searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by PlanDate desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            if (searchTerm.Where == "")
            {
                searchTerm.TableName = "FM2E_DailyPatrolPlan a left join FM2E_User b on a.Planner=b.UserName left join FM2E_User c on a.Approvaler=c.UserName left join FM2E_Company g on a.CompanyID = g.CompanyID left join FM2E_Department s on s.DepartmentID=a.DepartmentID";
                searchTerm.ReturnFields = "a.*,b.PersonName as PlannerName,c.PersonName as ApprovalerName,g.CompanyName as CompanyName,s.Name as DepartmentName";
                searchTerm.PageSize = 10;
                searchTerm.OrderBy = "order by PlanDate desc";
                searchTerm.Where = "where a.Status > 1";
            }
            return SQLHelper.GetObjectList(this.GetData, searchTerm, out recordCount);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long InsertDailyPatrolPlan(DailyPatrolPlanInfo model)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            long thisID = 0;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先插入申请信息
                thisID = insertPlan(trans, model);

                //插入申请明细信息
                if (model.PlanDetailList != null)
                {
                    foreach (DailyPatrolDetailInfo item in model.PlanDetailList)
                    {
                        item.PlanID = thisID;
                        InsertDailyPatrolDetail(trans, item);
                    }
                }
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
            return thisID;
        }
        private long insertPlan(SqlTransaction trans, DailyPatrolPlanInfo model)
        {
            long id = 1;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_DailyPatrolPlan(");
            strSql.Append("ApprovalOpinion,ApprovalRemark,Approvaler,ApprovalDate,Status,UpdateTime,PlanYear,CompanyID,PlanName,IsTemporary,Planner,PlanDate,StartDate,CompleteDate,DepartmentID)");
            strSql.Append(" values (");
            strSql.Append("@ApprovalOpinion,@ApprovalRemark,@Approvaler,@ApprovalDate,@Status,@UpdateTime,@PlanYear,@CompanyID,@PlanName,@IsTemporary,@Planner,@PlanDate,@StartDate,@CompleteDate,@DepartmentID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ApprovalOpinion", SqlDbType.NVarChar,50),
					new SqlParameter("@ApprovalRemark", SqlDbType.NVarChar,100),
					new SqlParameter("@Approvaler", SqlDbType.VarChar,20),
					new SqlParameter("@ApprovalDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@PlanYear", SqlDbType.Int,4),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@PlanName", SqlDbType.NVarChar,20),
					new SqlParameter("@IsTemporary", SqlDbType.Bit,1),
					new SqlParameter("@Planner", SqlDbType.VarChar,20),
					new SqlParameter("@PlanDate", SqlDbType.DateTime),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@CompleteDate", SqlDbType.DateTime),
                    new SqlParameter("@DepartmentID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.ApprovalOpinion;
            parameters[1].Value = model.ApprovalRemark;
            parameters[2].Value = model.Approvaler;
            parameters[3].Value = model.ApprovalDate;
            parameters[4].Value = (int)model.Status;
            parameters[5].Value = model.UpdateTime;
            parameters[6].Value = model.PlanYear;
            parameters[7].Value = model.CompanyID;
            parameters[8].Value = model.PlanName;
            parameters[9].Value = model.IsTemporary;
            parameters[10].Value = model.Planner;
            parameters[11].Value = model.PlanDate;
            parameters[12].Value = model.StartDate;
            parameters[13].Value = model.CompleteDate;
            parameters[14].Value = model.DepartmentID;
            SqlDataReader rdr = null;
            try
            {
                using (rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        if (!Convert.IsDBNull(rdr[0]))
                            id = Convert.ToInt64(rdr[0]);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("添加日常巡查计划信息失败", e);
            }
            finally
            {
                rdr.Close();
            }
            return id;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        private void InsertDailyPatrolDetail(SqlTransaction trans, DailyPatrolDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_DailyPatrolDetail(");
            strSql.Append("PlanID,PatrolPeriod,PeriodUnit,System,Subsystem,PatrolObject,PatrolContent,CheckStandard)");
            strSql.Append(" values (");
            strSql.Append("@PlanID,@PatrolPeriod,@PeriodUnit,@System,@Subsystem,@PatrolObject,@PatrolContent,@CheckStandard)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PlanID", SqlDbType.BigInt,8),
					new SqlParameter("@PatrolPeriod", SqlDbType.Int,4),
					new SqlParameter("@PeriodUnit", SqlDbType.TinyInt,1),
					new SqlParameter("@System", SqlDbType.VarChar,1),
					new SqlParameter("@Subsystem", SqlDbType.BigInt,8),
					new SqlParameter("@PatrolObject", SqlDbType.NVarChar,50),
					new SqlParameter("@PatrolContent", SqlDbType.NVarChar,200),
					new SqlParameter("@CheckStandard", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.PlanID;
            parameters[1].Value = model.PatrolPeriod;
            parameters[2].Value = (int)model.PeriodUnit;
            parameters[3].Value = model.System;
            parameters[4].Value = model.Subsystem;
            parameters[5].Value = model.PatrolObject;
            parameters[6].Value = model.PatrolContent;
            parameters[7].Value = model.CheckStandard;

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateDailyPatrolPlan(DailyPatrolPlanInfo model)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先更新申请信息
                updatePlan(trans, model);

                //先删除原来的明细，后添加新的明细
                StringBuilder delSql = new StringBuilder();
                delSql.AppendFormat("delete FM2E_DailyPatrolDetail");
                delSql.Append(" where PlanID=@PlanID ");
                SqlParameter[] delPara = {
					new SqlParameter("@PlanID", SqlDbType.BigInt)};
                delPara[0].Value = model.PlanID;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, delSql.ToString(), delPara);

                //插入申请明细信息
                if (model.PlanDetailList != null)
                {
                    foreach (DailyPatrolDetailInfo item in model.PlanDetailList)
                    {
                        item.PlanID = model.PlanID;
                        InsertDailyPatrolDetail(trans, item);
                    }
                }
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
        /// <summary>
        /// 更新计划信息
        /// </summary>
        private void updatePlan(SqlTransaction trans, DailyPatrolPlanInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_DailyPatrolPlan set ");
            strSql.Append("ApprovalOpinion=@ApprovalOpinion,");
            strSql.Append("ApprovalRemark=@ApprovalRemark,");
            strSql.Append("Approvaler=@Approvaler,");
            strSql.Append("ApprovalDate=@ApprovalDate,");
            strSql.Append("Status=@Status,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("PlanYear=@PlanYear,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("PlanName=@PlanName,");
            strSql.Append("IsTemporary=@IsTemporary,");
            strSql.Append("Planner=@Planner,");
            strSql.Append("PlanDate=@PlanDate,");
            strSql.Append("StartDate=@StartDate,");
            strSql.Append("CompleteDate=@CompleteDate,");
            strSql.Append("DepartmentID=@DepartmentID");
            strSql.Append(" where PlanID=@PlanID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PlanID", SqlDbType.BigInt,8),
					new SqlParameter("@ApprovalOpinion", SqlDbType.NVarChar,50),
					new SqlParameter("@ApprovalRemark", SqlDbType.NVarChar,100),
					new SqlParameter("@Approvaler", SqlDbType.VarChar,20),
					new SqlParameter("@ApprovalDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@PlanYear", SqlDbType.Int,4),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@PlanName", SqlDbType.NVarChar,20),
					new SqlParameter("@IsTemporary", SqlDbType.Bit,1),
					new SqlParameter("@Planner", SqlDbType.VarChar,20),
					new SqlParameter("@PlanDate", SqlDbType.DateTime),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@CompleteDate", SqlDbType.DateTime),
                    new SqlParameter("@DepartmentID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.PlanID;
            parameters[1].Value = model.ApprovalOpinion;
            parameters[2].Value = model.ApprovalRemark;
            parameters[3].Value = model.Approvaler;
            parameters[4].Value = model.ApprovalDate;
            parameters[5].Value = (int)model.Status;
            parameters[6].Value = model.UpdateTime;
            parameters[7].Value = model.PlanYear;
            parameters[8].Value = model.CompanyID;
            parameters[9].Value = model.PlanName;
            parameters[10].Value = model.IsTemporary;
            parameters[11].Value = model.Planner;
            parameters[12].Value = model.PlanDate;
            parameters[13].Value = model.StartDate;
            parameters[14].Value = model.CompleteDate;
            parameters[15].Value = model.DepartmentID;
            try
            {
                int result = SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("更新日常巡查计划信息失败", e);
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DelDailyPatrolPlan(long PlanID)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //删除申请明细信息
                DelDailyPatrolDetail(trans, PlanID);

                //删除申请信息
                DelPlan(trans, PlanID);

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
        /// <summary>
        /// 删除申请信息
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="ID"></param>
        private void DelPlan(SqlTransaction trans, long PlanID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_DailyPatrolPlan ");
                strSql.Append(" where PlanID=@PlanID ");
                SqlParameter[] parameters = {
					new SqlParameter("@PlanID", SqlDbType.BigInt)};
                parameters[0].Value = PlanID;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除日常巡查计划信息失败", e);
            }
        }
        /// <summary>
        /// 删除所有申请明细
        /// </summary>
        private void DelDailyPatrolDetail(SqlTransaction trans, long PlanID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_DailyPatrolDetail ");
                strSql.Append(" where PlanID=@PlanID ");
                SqlParameter[] parameters = {
					new SqlParameter("@PlanID", SqlDbType.BigInt)};
                parameters[0].Value = PlanID;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除日常巡查计划明细信息失败", e);
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DailyPatrolPlanInfo GetDailyPatrolPlan(long PlanID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 a.*,b.PersonName as PlannerName,c.PersonName as ApprovalerName,g.CompanyName as CompanyName,s.Name as DepartmentName from FM2E_DailyPatrolPlan  a left join FM2E_User b on a.Planner=b.UserName left join FM2E_User c on a.Approvaler=c.UserName left join FM2E_Department s on s.DepartmentID=a.DepartmentID left join FM2E_Company g on a.CompanyID = g.CompanyID");
            strSql.Append(" where a.PlanID=@PlanID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PlanID", SqlDbType.BigInt)};
            parameters[0].Value = PlanID;
            DailyPatrolPlanInfo model = null;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        model = this.GetData(rd);
                    }
                }
                if (model == null) return null;

                //获取计划明细列表
                StringBuilder strDetailSql = new StringBuilder();
                strDetailSql.Append("select a.*,b.SystemName as SystemName,c.SubSystemName as SubSystemName from FM2E_DailyPatrolDetail a left join FM2E_System b on a.System = b.SystemID left join FM2E_SubSystem c on a.Subsystem = c.SubSystemID ");
                strDetailSql.Append(" where a.PlanID=@PlanID order by ItemID asc");

                ArrayList list = new ArrayList();
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strDetailSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        DailyPatrolDetailInfo item = GetDetailData(rd);
                        list.Add(item);
                    }
                }
                model.PlanDetailList = list;
            }
            catch (Exception e)
            {
                throw new DALException("获取日常巡查计划信息失败", e);
            }
            return model;
        }

        private DailyPatrolPlanInfo GetData(IDataReader rd)
        {
            DailyPatrolPlanInfo item = new DailyPatrolPlanInfo();

            if (!Convert.IsDBNull(rd["PlanID"]))
                item.PlanID = Convert.ToInt64(rd["PlanID"]);

            if (!Convert.IsDBNull(rd["ApprovalOpinion"]))
                item.ApprovalOpinion = Convert.ToString(rd["ApprovalOpinion"]);

            if (!Convert.IsDBNull(rd["ApprovalRemark"]))
                item.ApprovalRemark = Convert.ToString(rd["ApprovalRemark"]);

            if (!Convert.IsDBNull(rd["ApprovalDate"]))
                item.ApprovalDate = Convert.ToDateTime(rd["ApprovalDate"]);

            if (!Convert.IsDBNull(rd["Approvaler"]))
                item.Approvaler = Convert.ToString(rd["Approvaler"]);

            if (!Convert.IsDBNull(rd["Status"]))
                item.Status = (DailyPatrolPlanStatus)(Convert.ToInt32(rd["Status"]));

            if (!Convert.IsDBNull(rd["UpdateTime"]))
                item.UpdateTime = Convert.ToDateTime(rd["UpdateTime"]);

            if (!Convert.IsDBNull(rd["PlanYear"]))
                item.PlanYear = Convert.ToInt32(rd["PlanYear"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["PlanName"]))
                item.PlanName = Convert.ToString(rd["PlanName"]);

            if (!Convert.IsDBNull(rd["PlanDate"]))
                item.PlanDate = Convert.ToDateTime(rd["PlanDate"]);

            if (!Convert.IsDBNull(rd["Planner"]))
                item.Planner = Convert.ToString(rd["Planner"]);

            if (!Convert.IsDBNull(rd["IsTemporary"]))
                item.IsTemporary = Convert.ToBoolean(rd["IsTemporary"]);

            if (!Convert.IsDBNull(rd["StartDate"]))
                item.StartDate = Convert.ToDateTime(rd["StartDate"]);

            if (!Convert.IsDBNull(rd["CompleteDate"]))
                item.CompleteDate = Convert.ToDateTime(rd["CompleteDate"]);

            if (!Convert.IsDBNull(rd["PlannerName"]))
                item.PlannerName = Convert.ToString(rd["PlannerName"]);

            if (!Convert.IsDBNull(rd["ApprovalerName"]))
                item.ApprovalerName = Convert.ToString(rd["ApprovalerName"]);

            if (!Convert.IsDBNull(rd["CompanyName"]))
                item.CompanyName = Convert.ToString(rd["CompanyName"]);

            if (!Convert.IsDBNull(rd["DepartmentID"]))
                item.DepartmentID = Convert.ToInt64(rd["DepartmentID"]);

            if (!Convert.IsDBNull(rd["DepartmentName"]))
                item.DepartmentName = Convert.ToString(rd["DepartmentName"]);

            return item;

        }
        private DailyPatrolDetailInfo GetDetailData(IDataReader rd)
        {
            DailyPatrolDetailInfo item = new DailyPatrolDetailInfo();

            if (!Convert.IsDBNull(rd["PlanID"]))
                item.PlanID = Convert.ToInt64(rd["PlanID"]);

            if (!Convert.IsDBNull(rd["ItemID"]))
                item.ItemID = Convert.ToInt64(rd["ItemID"]);

            if (!Convert.IsDBNull(rd["PatrolPeriod"]))
                item.PatrolPeriod = Convert.ToInt32(rd["PatrolPeriod"]);

            if (!Convert.IsDBNull(rd["PeriodUnit"]))
                item.PeriodUnit = (DailyPatrolPeriodUnit)Convert.ToInt32(rd["PeriodUnit"]);

            if (!Convert.IsDBNull(rd["System"]))
                item.System = Convert.ToString(rd["System"]);

            if (!Convert.IsDBNull(rd["Subsystem"]))
                item.Subsystem = Convert.ToInt64(rd["Subsystem"]);

            if (!Convert.IsDBNull(rd["SystemName"]))
                item.SystemName = Convert.ToString(rd["SystemName"]);

            if (!Convert.IsDBNull(rd["SubsystemName"]))
                item.SubsystemName = Convert.ToString(rd["SubsystemName"]);

            if (!Convert.IsDBNull(rd["PatrolObject"]))
                item.PatrolObject = Convert.ToString(rd["PatrolObject"]);

            if (!Convert.IsDBNull(rd["PatrolContent"]))
                item.PatrolContent = Convert.ToString(rd["PatrolContent"]);

            if (!Convert.IsDBNull(rd["CheckStandard"]))
                item.CheckStandard = Convert.ToString(rd["CheckStandard"]);

            return item;

        }
    }
}
