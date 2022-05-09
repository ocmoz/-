using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using FM2E.IDAL.Archives;
using FM2E.Model.Archives;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Utils;
using System.Collections;
using FM2E.Model.Exceptions;

namespace FM2E.SQLServerDAL.Archives
{
    /// <summary>
    /// 档案借阅申请数据库访问
    /// </summary>
    public class ArchivesBorrowApply : IArchivesBorrowApply
    {
        /// <summary>
        /// 生成查询申请单所用的对象
        /// </summary>
        /// <param name="item">查询参数</param>
        /// <returns>查询所用的对象</returns>
        public QueryParam GenerateSearchTerm(ArchivesBorrowApplyInfo item)
        {
            string sqlSearch = "where 1=1";
            if (item.ID != 0)
                sqlSearch += " and a.ID ='" + item.ID + "'";

            if (item.Applicant != "")
                sqlSearch += " and a.Applicant ='" + item.Applicant + "'";

            if (item.Approvaler!=null&&item.Approvaler != "")
                sqlSearch += " and a.Approvaler ='" + item.Approvaler + "'";

            if (item.ApplicantName != "")
                sqlSearch += " and b.PersonName like '%" + item.ApplicantName + "%'";

            if (item.ApplyStatus > 0)
                sqlSearch += " and a.ApplyStatus ='" + (int)item.ApplyStatus + "'";

            if (item.StatusArray != null && item.StatusArray.Count > 0)
            {
                for (int i = 0; i < item.StatusArray.Count; i++)
                {
                    if (i == 0)
                    {
                        sqlSearch += " and ( ";
                        sqlSearch += " " + " a.ApplyStatus=" + (int)item.StatusArray[i] + " ";
                    }
                    else
                    {
                        sqlSearch += " or " + " a.ApplyStatus=" + (int)item.StatusArray[i] + " ";
                    }
                    if (i == item.StatusArray.Count - 1)
                    {
                        sqlSearch += " ) ";
                    }
                }
            }
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_ArchivesBorrowApply a left join FM2E_User b on a.Applicant=b.UserName left join FM2E_User c on a.Approvaler=c.UserName left join FM2E_Department g on a.ApplicantDept = g.DepartmentID";
            searchTerm.ReturnFields = "a.*,b.PersonName as ApplicantName,c.PersonName as ApprovalerName,g.Name as ApplicantDeptName";
            //searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by ApplyDate desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        /// <summary>
        /// 生成查询申请单所用的对象，包含工作流状态
        /// </summary>
        /// <param name="item">查询参数</param>
        /// <param name="WFStates">工作流状态数据</param>
        /// <returns>查询所用的对象</returns>
        public QueryParam GenerateSearchTerm(ArchivesBorrowApplyInfo item, string[] WFStates)
        {
            string sqlSearch = "where 1=1";
            if (item.ID != 0)
                sqlSearch += " and a.ID ='" + item.ID + "'";

            if (item.Applicant != "")
                sqlSearch += " and a.Applicant ='" + item.Applicant + "'";

            if (item.Approvaler != null && item.Approvaler != "")
                sqlSearch += " and a.Approvaler ='" + item.Approvaler + "'";

            if (item.ApplicantName != "")
                sqlSearch += " and b.PersonName like '%" + item.ApplicantName + "%'";

            if (item.ApplyStatus > 0)
                sqlSearch += " and a.ApplyStatus ='" + (int)item.ApplyStatus + "'";

            if (WFStates != null && WFStates.Length > 0)
            {
                sqlSearch += "and h.TableName='FM2E_ArchivesBorrowApply' and (";
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
            searchTerm.TableName = "FM2E_ArchivesBorrowApply a left join FM2E_User b on a.Applicant=b.UserName left join FM2E_User c on a.Approvaler=c.UserName left join FM2E_Department g on a.ApplicantDept = g.DepartmentID left join FM2E_WorkflowInstance h on a.ID=h.DataID";
            searchTerm.ReturnFields = "a.*,b.PersonName as ApplicantName,c.PersonName as ApprovalerName,g.Name as ApplicantDeptName";
           // searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by ApplyDate desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        /// <summary>
        /// 根据查询对象进行查询
        /// </summary>
        /// <param name="searchTerm">查询对象</param>
        /// <param name="recordCount">查询结果数量</param>
        /// <returns>查询结果列表</returns>
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            if (searchTerm.Where == "")
            {
                searchTerm.TableName = "FM2E_ArchivesBorrowApply a left join FM2E_User b on a.Applicant=b.UserName left join FM2E_User c on a.Approvaler=c.UserName left join FM2E_Department g on a.ApplicantDept = g.DepartmentID";
                searchTerm.ReturnFields = "a.*,b.PersonName as ApplicantName,c.PersonName as ApprovalerName,g.Name as ApplicantDeptName";
                //searchTerm.PageSize = 10;
                searchTerm.OrderBy = "order by ApplyDate desc";
                searchTerm.Where = "where a.ApplyStatus > 1";
            }
            return SQLHelper.GetObjectList(this.GetData, searchTerm, out recordCount);
        }
        /// <summary>
        /// 增加一条借阅申请数据
        /// </summary>
        /// <param name="model">借阅申请单信息</param>
        /// <returns>新增申请单流水号</returns>
        public long InsertArchivesBorrowApply(ArchivesBorrowApplyInfo model)
        {
            long thisID = 0;
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先插入申请信息
                thisID = insertApply(trans, model);

                //插入申请明细信息
                if (model.ApplyDetailList != null)
                {
                    foreach (ArchivesBorrowApplyDetailInfo item in model.ApplyDetailList)
                    {
                        item.ID = thisID;
                        InsertArchivesBorrowApplyDetail(trans, item);
                    }
                }
                //事务提交
                trans.Commit();
                return thisID;
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
                //return thisID;
            }
            
        }
        private long insertApply(SqlTransaction trans, ArchivesBorrowApplyInfo model)
        {
            long id = 1;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_ArchivesBorrowApply(");
            strSql.Append("Approvaler,ApprovalDate,ApplyStatus,SheetNo,ApplyDate,Applicant,ApplicantDept,BorrowReason,BorrowTime,Remark,ApprovalOpinion)");
            strSql.Append(" values (");
            strSql.Append("@Approvaler,@ApprovalDate,@ApplyStatus,@SheetNo,@ApplyDate,@Applicant,@ApplicantDept,@BorrowReason,@BorrowTime,@Remark,@ApprovalOpinion)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Approvaler", SqlDbType.VarChar,20),
					new SqlParameter("@ApprovalDate", SqlDbType.DateTime),
					new SqlParameter("@ApplyStatus", SqlDbType.TinyInt,1),
					new SqlParameter("@SheetNo", SqlDbType.VarChar,20),
					new SqlParameter("@ApplyDate", SqlDbType.DateTime),
					new SqlParameter("@Applicant", SqlDbType.VarChar,20),
					new SqlParameter("@ApplicantDept", SqlDbType.BigInt,8),
					new SqlParameter("@BorrowReason", SqlDbType.NVarChar,100),
					new SqlParameter("@BorrowTime", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@ApprovalOpinion", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.Approvaler;
            parameters[1].Value = model.ApprovalDate;
            parameters[2].Value = (int)model.ApplyStatus;
            parameters[3].Value = model.SheetNo;
            parameters[4].Value = model.ApplyDate;
            parameters[5].Value = model.Applicant;
            parameters[6].Value = model.ApplicantDept;
            parameters[7].Value = model.BorrowReason;
            parameters[8].Value = model.BorrowTime;
            parameters[9].Value = model.Remark;
            parameters[10].Value = model.ApprovalOpinion;
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
                throw new DALException("添加档案借阅申请信息失败", e);
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
        private void InsertArchivesBorrowApplyDetail(SqlTransaction trans, ArchivesBorrowApplyDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_ArchivesBorrowApplyDetail(");
            strSql.Append("ID,Module,ArchivesType,ArchivesName,ArchivesID)");
            strSql.Append(" values (");
            strSql.Append("@ID,@Module,@ArchivesType,@ArchivesName,@ArchivesID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@Module", SqlDbType.NVarChar,30),
					new SqlParameter("@ArchivesType", SqlDbType.VarChar,50),
					new SqlParameter("@ArchivesName", SqlDbType.NVarChar,100),
					new SqlParameter("@ArchivesID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Module;
            parameters[2].Value = model.ArchivesType;
            parameters[3].Value = model.ArchivesName;
            parameters[4].Value = model.ArchivesID;

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        }
       /// <summary>
        /// 更新一条数据
       /// </summary>
       /// <param name="model">需要更新的借阅单信息</param>
        public void UpdateArchivesBorrowApply(ArchivesBorrowApplyInfo model)
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
                updateApply(trans, model);

                //先删除原来的明细，后添加新的明细
                StringBuilder delSql = new StringBuilder();
                delSql.AppendFormat("delete FM2E_ArchivesBorrowApplyDetail");
                delSql.Append(" where ID=@ID ");
                SqlParameter[] delPara = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                delPara[0].Value = model.ID;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, delSql.ToString(), delPara);

                //插入申请明细信息
                if (model.ApplyDetailList != null)
                {
                    foreach (ArchivesBorrowApplyDetailInfo item in model.ApplyDetailList)
                    {
                        item.ID = model.ID;
                        InsertArchivesBorrowApplyDetail(trans, item);
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
        /// 更新申请信息
        /// </summary>
        private void updateApply(SqlTransaction trans, ArchivesBorrowApplyInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_ArchivesBorrowApply set ");
            strSql.Append("Approvaler=@Approvaler,");
            strSql.Append("ApprovalDate=@ApprovalDate,");
            strSql.Append("ApplyStatus=@ApplyStatus,");
            strSql.Append("SheetNo=@SheetNo,");
            strSql.Append("ApplyDate=@ApplyDate,");
            strSql.Append("Applicant=@Applicant,");
            strSql.Append("ApplicantDept=@ApplicantDept,");
            strSql.Append("BorrowReason=@BorrowReason,");
            strSql.Append("BorrowTime=@BorrowTime,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("ApprovalOpinion=@ApprovalOpinion");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@Approvaler", SqlDbType.VarChar,20),
					new SqlParameter("@ApprovalDate", SqlDbType.DateTime),
					new SqlParameter("@ApplyStatus", SqlDbType.TinyInt,1),
					new SqlParameter("@SheetNo", SqlDbType.VarChar,20),
					new SqlParameter("@ApplyDate", SqlDbType.DateTime),
					new SqlParameter("@Applicant", SqlDbType.VarChar,20),
					new SqlParameter("@ApplicantDept", SqlDbType.BigInt,8),
					new SqlParameter("@BorrowReason", SqlDbType.NVarChar,100),
					new SqlParameter("@BorrowTime", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@ApprovalOpinion", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Approvaler;
            parameters[2].Value = model.ApprovalDate;
            parameters[3].Value = (int)model.ApplyStatus;
            parameters[4].Value = model.SheetNo;
            parameters[5].Value = model.ApplyDate;
            parameters[6].Value = model.Applicant;
            parameters[7].Value = model.ApplicantDept;
            parameters[8].Value = model.BorrowReason;
            parameters[9].Value = model.BorrowTime;
            parameters[10].Value = model.Remark;
            parameters[11].Value = model.ApprovalOpinion;
            try
            {
                int result = SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("更新档案借阅申请信息失败", e);
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="ID">申请单流水号</param>
        public void DelArchivesBorrowApply(long ID)
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
                DelArchivesBorrowApplyDetail(trans, ID);

                //删除申请信息
                DelApply(trans, ID);

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
        private void DelApply(SqlTransaction trans, long ID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_ArchivesBorrowApply ");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                parameters[0].Value = ID;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除档案借阅申请信息失败", e);
            }
        }
        /// <summary>
        /// 删除所有申请明细
        /// </summary>
        private void DelArchivesBorrowApplyDetail(SqlTransaction trans, long ID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_ArchivesBorrowApplyDetail ");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                parameters[0].Value = ID;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除档案借阅申请明细信息失败", e);
            }
        }
        /// <summary>
        /// 获取档案借阅申请单信息
        /// </summary>
        /// <param name="ID">申请单流水号</param>
        /// <returns>借阅申请单信息</returns>
        public ArchivesBorrowApplyInfo GetArchivesBorrowApply(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 a.*,b.PersonName as ApplicantName,c.PersonName as ApprovalerName,g.Name as ApplicantDeptName from FM2E_ArchivesBorrowApply  a left join FM2E_User b on a.Applicant=b.UserName left join FM2E_User c on a.Approvaler=c.UserName left join FM2E_Department g on a.ApplicantDept = g.DepartmentID");
            strSql.Append(" where a.ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = ID;
            ArchivesBorrowApplyInfo model = null;
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

                //获取申请明细列表
                StringBuilder strDetailSql = new StringBuilder();
                strDetailSql.Append("select ItemID,ID,Module,ArchivesType,ArchivesName,ArchivesID from FM2E_ArchivesBorrowApplyDetail ");
                strDetailSql.Append(" where ID=@ID order by ItemID asc");

                ArrayList list = new ArrayList();
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strDetailSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        ArchivesBorrowApplyDetailInfo item = GetDetailData(rd);
                        list.Add(item);
                    }
                }
                model.ApplyDetailList = list;
            }
            catch (Exception e)
            {
                throw new DALException("获取档案借阅申请信息失败", e);
            }
            return model;
        }
        /// <summary>
        /// user是否已经借阅
        /// </summary>
        /// <param name="archivesType">档案类型</param>
        /// <param name="id">档案流水号</param>
        /// <param name="user">用户账号</param>
        /// <returns>检查是否已经借阅</returns>
        public bool isBorrowedDetail(string archivesType, long id, string user)
        {
            string sql = string.Format("select count(*) from FM2E_ArchivesBorrowApplyDetail a left join FM2E_ArchivesBorrowApply b on a.ID=b.ID where a.ArchivesType='{0}' and a.ArchivesID='{1}' and b.ApplyStatus='3' and b.BorrowTime>='{2}' and b.Applicant='{3}'", archivesType, id.ToString(), DateTime.Now.ToString(),user);
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
                {
                    if (rd.Read())
                    {
                        if (!Convert.IsDBNull(rd[0]))
                        {
                            if (Convert.ToInt32(rd[0]) > 0)
                                return true;
                            else return false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取档案借阅信息失败", e);
            }
            return false;
        }

        private ArchivesBorrowApplyInfo GetData(IDataReader rd)
        {
            ArchivesBorrowApplyInfo item = new ArchivesBorrowApplyInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["ApprovalOpinion"]))
                item.ApprovalOpinion = Convert.ToString(rd["ApprovalOpinion"]);

            if (!Convert.IsDBNull(rd["ApprovalDate"]))
                item.ApprovalDate = Convert.ToDateTime(rd["ApprovalDate"]);

            if (!Convert.IsDBNull(rd["Approvaler"]))
                item.Approvaler = Convert.ToString(rd["Approvaler"]);

            if (!Convert.IsDBNull(rd["ApprovalerName"]))
                item.ApprovalerName = Convert.ToString(rd["ApprovalerName"]);

            if (!Convert.IsDBNull(rd["ApplyStatus"]))
                item.ApplyStatus = (ArchivesBorrowApplyStatus)(Convert.ToInt32(rd["ApplyStatus"]));

            if (!Convert.IsDBNull(rd["SheetNo"]))
                item.SheetNo = Convert.ToString(rd["SheetNo"]);

            if (!Convert.IsDBNull(rd["Applicant"]))
                item.Applicant = Convert.ToString(rd["Applicant"]);

            if (!Convert.IsDBNull(rd["ApplyDate"]))
                item.ApplyDate = Convert.ToDateTime(rd["ApplyDate"]);

            if (!Convert.IsDBNull(rd["ApplicantName"]))
                item.ApplicantName = Convert.ToString(rd["ApplicantName"]);

            if (!Convert.IsDBNull(rd["ApplicantDept"]))
                item.ApplicantDept = Convert.ToInt64(rd["ApplicantDept"]);

            if (!Convert.IsDBNull(rd["ApplicantDeptName"]))
                item.ApplicantDeptName = Convert.ToString(rd["ApplicantDeptName"]);

            if (!Convert.IsDBNull(rd["BorrowTime"]))
                item.BorrowTime = Convert.ToDateTime(rd["BorrowTime"]);

            if (!Convert.IsDBNull(rd["BorrowReason"]))
                item.BorrowReason = Convert.ToString(rd["BorrowReason"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            return item;

        }
        private ArchivesBorrowApplyDetailInfo GetDetailData(IDataReader rd)
        {
            ArchivesBorrowApplyDetailInfo item = new ArchivesBorrowApplyDetailInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["ItemID"]))
                item.ItemID = Convert.ToInt64(rd["ItemID"]);

            if (!Convert.IsDBNull(rd["Module"]))
                item.Module = Convert.ToString(rd["Module"]);

            if (!Convert.IsDBNull(rd["ArchivesID"]))
                item.ArchivesID = Convert.ToInt64(rd["ArchivesID"]);

            if (!Convert.IsDBNull(rd["ArchivesType"]))
                item.ArchivesType = Convert.ToString(rd["ArchivesType"]);

            if (!Convert.IsDBNull(rd["ArchivesName"]))
                item.ArchivesName = Convert.ToString(rd["ArchivesName"]);

            return item;

        }
        /// <summary>
        /// 借阅申请详情明细查询查询对象生成
        /// </summary>
        /// <param name="item">明细查询参数</param>
        /// <param name="Applicant">申请人账号</param>
        /// <returns>查询对象</returns>
        public QueryParam GenerateDetailSearchTerm(ArchivesBorrowApplyDetailInfo item, string Applicant)
        {
            string sqlSearch = "where 1=1 and b.ApplyStatus='3'";
            if (item.ID != 0)
                sqlSearch += " and a.ID ='" + item.ID + "'";
            if (Applicant != null)
                sqlSearch += " and b.Applicant='" + Applicant + "'";
            sqlSearch += " and b.BorrowTime>='" + DateTime.Now.ToShortDateString() + " 00:00" + "'";

            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_ArchivesBorrowApplyDetail a left join FM2E_ArchivesBorrowApply b on a.ID=b.ID ";
            searchTerm.ReturnFields = "a.*";
            //searchTerm.PageSize = 10;
            searchTerm.OrderBy = "";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        /// <summary>
        /// 查询借阅明细
        /// </summary>
        /// <param name="searchTerm">查询对象</param>
        /// <param name="recordCount">查询结果总数</param>
        /// <returns>查询结果列表</returns>
        public IList GetDetailList(QueryParam searchTerm, out int recordCount)
        {
            if (searchTerm.Where == "")
            {
                searchTerm.TableName = "FM2E_ArchivesBorrowApplyDetail a left join FM2E_ArchivesBorrowApply b on a.ID=b.ID ";
                searchTerm.ReturnFields = "a.*";
                //searchTerm.PageSize = 10;
                searchTerm.OrderBy = "";
                searchTerm.Where = "";
            }
            return SQLHelper.GetObjectList(this.GetDetailData, searchTerm, out recordCount);
        }

    }
}
