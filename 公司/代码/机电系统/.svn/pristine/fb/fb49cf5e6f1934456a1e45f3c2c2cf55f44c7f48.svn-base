using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.BudgetManagement;
using FM2E.Model.Utils;
using System.Collections;
using FM2E.Model.BudgetManagement;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;
using System.Data;
using System.Data.SqlTypes;
using FM2E.Model.Exceptions;
using FM2E.SQLServerDAL.Basic;
using FM2E.Model.Basic;

namespace FM2E.SQLServerDAL.BudgetManagement
{
    public class AnnualBudget : IAnnualBudget
    {

        private const string BudgetYearTableName = " FM2E_AnnualBudgetView ";

        private const string BudgetYearDetailTableName = " FM2E_AnnualBudgetDetail ";

        private const string BudgetYearReturnFields = " * ";

        private const string BudgetYearDetailRetuanFields = " * ";

        private const string BudgetYearOrderBy = " order by Year desc ";

        private const string BudgetYearDetailOrderBy = " order by BudgetYearDetailID ";

        private const string BudgetYearWhere = " where 1=1 ";

        private const string BudgetYearDetailWhere = " where 1=1 ";

        private const string BudgetPerMonthTotalTableName = " FM2E_BudgetMonthlyView ";

        private const string BudgetPerMonthTableName = " FM2E_BudgetPerMonth ";

        private const string BudgetPerMonthTotalReturnFields = " * ";

        private const string BudgetPerMonthReturnFields = " * ";

        private const string BudgetPerMonthTotalOrderBy = " order by TotalID desc ";

        private const string BudgetPerMonthOrderBy = " order by BudgetPermonthID ";

        private const string BudgetPerMonthTotalWhere = " where 1=1 ";

        private const string BudgetDetailTableName = " FM2E_BudgetDetail ";

        private const string BudgetDetailViewTableName = " FM2E_BudgetDetailView ";

        private const string BudgetDetailReturnFields = " * ";

        private const string BudgetDetailOrderBy = " order by DetailID desc ";

        private const string SELECT_BUDGETYEAR = " select " + BudgetYearReturnFields + " from " + BudgetYearTableName + BudgetYearWhere + " and BudgetYearID = '{0}' " + BudgetYearOrderBy;

        private const string SELECT_BUDGETMONTHTOTAL = " select " + BudgetPerMonthTotalReturnFields + " from " + BudgetPerMonthTotalTableName + BudgetPerMonthTotalWhere + " and TotalID = '{0}' " + BudgetPerMonthTotalOrderBy;


        public long SelectMaxBudgetYearRecord()
        {
            SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString);
            conn.Open();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT MAX(BudgetYearID) AS MaxID FROM FM2E_AnnualBudget WHERE (Status = 3)");
            return (long)SQLHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), null);
        }


        public QueryParam GenerateSearchTerm(BudgetYearInfo item)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = BudgetYearTableName;
            qp.ReturnFields = BudgetYearReturnFields;
            qp.OrderBy = BudgetYearOrderBy;
            qp.Where = BudgetYearWhere;
            if (item.Status != 0)
                qp.Where += " and Status = " + item.Status + " ";
            if (item.CompanyID != null && item.CompanyID != string.Empty)
                qp.Where += " and CompanyID = '" + item.CompanyID + "' ";
            if (item.WorkFlowStatus != null && item.WorkFlowStatus.Count > 0)
            {
                for (int i = 0; i < item.WorkFlowStatus.Count; i++)
                {
                    if (i == 0)
                    {
                        qp.Where += " and ( ";
                        qp.Where += " " + " CurrentStateName='" + item.WorkFlowStatus[i] + "' ";
                    }
                    else
                    {
                        qp.Where += " or " + " CurrentStateName='" + item.WorkFlowStatus[i] + "' ";
                    }
                    if (i == item.WorkFlowStatus.Count - 1)
                    {
                        qp.Where += " ) ";
                    }
                }
            }
            if (!string.IsNullOrEmpty(item.WorkFlowUserName))
                qp.Where += " and NextUserName = '" + item.WorkFlowUserName + "' ";
            return qp;
        }
        public IList GetBudgetYearList(QueryParam term, out int recordCount, string companyid)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = BudgetYearTableName;
                    term.ReturnFields = BudgetYearReturnFields;
                    term.OrderBy = BudgetYearOrderBy;
                    term.Where = BudgetYearWhere;
                }
                if (companyid != null && companyid != string.Empty)
                    term.Where += " and CompanyID = '" + companyid + "' ";
                return SQLHelper.GetObjectList(this.GetBudgetYearData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException(" 获取年度预算分页失败", e);
            }
        }
        public BudgetYearInfo GetBudgetYear(long id)
        {
            BudgetYearInfo item = null;
            string cmd = string.Format(SELECT_BUDGETYEAR, id);

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        item = GetBudgetYearData(rd);
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取年度预算基本信息失败", e);
            }

            return item;
        }

        public long InsertBudgetYear(BudgetYearInfo item)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            long id = 0;
            try
            {
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                id = InsertBudgetYearBase(trans, item);

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FM2E_AnnualBudgetDetail(");
                strSql.Append("BudgetYearID,SubID,Name,DepartmentID,ParentID,BudgetApply,BudgetApprove,IsLeaf,CompanyID)");
                strSql.Append(" values (");
                strSql.Append("@BudgetYearID,@SubID,@Name,@DepartmentID,@ParentID,@BudgetApply,@BudgetApprove,@IsLeaf,@CompanyID)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@BudgetYearID", SqlDbType.BigInt,8),
					new SqlParameter("@SubID", SqlDbType.BigInt,8),
					new SqlParameter("@Name", SqlDbType.VarChar,30),
					new SqlParameter("@DepartmentID", SqlDbType.BigInt,8),
					new SqlParameter("@ParentID", SqlDbType.BigInt,8),
					new SqlParameter("@BudgetApply", SqlDbType.Decimal,9),
					new SqlParameter("@BudgetApprove", SqlDbType.Decimal,9),
					new SqlParameter("@IsLeaf", SqlDbType.Bit,1),
                    new SqlParameter("@CompanyID",SqlDbType.VarChar,2)
                                            };

                foreach (BudgetYearDetailInfo model in item.DetailList)
                {
                    parameters[0].Value = id;
                    parameters[1].Value = model.SubID;
                    parameters[2].Value = model.Name;
                    parameters[3].Value = model.DepartmentID;
                    parameters[4].Value = model.ParentID;
                    parameters[5].Value = model.BudgetApply;
                    parameters[6].Value = model.BudgetApprove;
                    parameters[7].Value = model.IsLeaf;
                    parameters[8].Value = model.CompanyID;

                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                }
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new DALException("添加年度预算信息失败", e);
            }
            return id;
        }
        /// <summary>
        /// 插入年度预算基本信息
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="model">年度预算基本信息对算</param>
        /// <returns></returns>
        private long InsertBudgetYearBase(SqlTransaction trans, BudgetYearInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_AnnualBudget(");
            strSql.Append("Attachment,UpdateTime,BudgetApply,BudgetApprove,CompanyID,Year,Maker,MakeTime,Status,Approvaler,Result,Remark,Title)");
            strSql.Append(" values (");
            strSql.Append("@Attachment,@UpdateTime,@BudgetApply,@BudgetApprove,@CompanyID,@Year,@Maker,@MakeTime,@Status,@Approvaler,@Result,@Remark,@Title)");
            strSql.Append(";select cast(@@IDENTITY as BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@Attachment", SqlDbType.VarChar,80),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@BudgetApply", SqlDbType.Decimal,9),
					new SqlParameter("@BudgetApprove", SqlDbType.Decimal,9),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@Maker", SqlDbType.VarChar,20),
					new SqlParameter("@MakeTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@Approvaler", SqlDbType.VarChar,20),
					new SqlParameter("@Result", SqlDbType.Bit,1),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
                    new SqlParameter("@Title",SqlDbType.NVarChar,50)
                                        };
            parameters[0].Value = model.Attachment;
            parameters[1].Value = DateTime.Compare(model.UpdateTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : model.UpdateTime;
            parameters[2].Value = model.BudgetApply;
            parameters[3].Value = model.BudgetApprove;
            parameters[4].Value = model.CompanyID;
            parameters[5].Value = model.Year;
            parameters[6].Value = model.Maker;
            parameters[7].Value = DateTime.Compare(model.MakeTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : model.MakeTime;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.Approvaler;
            parameters[10].Value = model.Result;
            parameters[11].Value = model.Remark;
            parameters[12].Value = model.Title;

            long id = 0;
            id = (long)SQLHelper.ExecuteScalar(trans, CommandType.Text, strSql.ToString(), parameters);

            return id;
        }
        public void UpdateBudgetYear(BudgetYearInfo item)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            long id = item.BudgetYearID;
            try
            {
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                UpdateBudgetYearBase(trans, item);
                if (item.DetailList != null && item.DetailList.Count != 0)
                {
                    BudgetYearDetailInfo budgetyeardetail = new BudgetYearDetailInfo();
                    budgetyeardetail.BudgetYearID = id;
                    IList<BudgetYearDetailInfo> list = this.Search(budgetyeardetail);

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update FM2E_AnnualBudgetDetail set ");
                    strSql.Append("BudgetYearID=@BudgetYearID,");
                    strSql.Append("SubID=@SubID,");
                    strSql.Append("Name=@Name,");
                    strSql.Append("DepartmentID=@DepartmentID,");
                    strSql.Append("ParentID=@ParentID,");
                    strSql.Append("BudgetApply=@BudgetApply,");
                    strSql.Append("BudgetApprove=@BudgetApprove,");
                    strSql.Append("IsLeaf=@IsLeaf,CompanyID=@CompanyID");
                    strSql.Append(" where BudgetYearDetailID=@BudgetYearDetailID ");
                    SqlParameter[] parameters = {
					new SqlParameter("@BudgetYearDetailID", SqlDbType.BigInt,8),
					new SqlParameter("@BudgetYearID", SqlDbType.BigInt,8),
					new SqlParameter("@SubID", SqlDbType.BigInt,8),
					new SqlParameter("@Name", SqlDbType.VarChar,30),
					new SqlParameter("@DepartmentID", SqlDbType.BigInt,8),
					new SqlParameter("@ParentID", SqlDbType.BigInt,8),
					new SqlParameter("@BudgetApply", SqlDbType.Decimal,9),
					new SqlParameter("@BudgetApprove", SqlDbType.Decimal,9),
					new SqlParameter("@IsLeaf", SqlDbType.Bit,1),
                    new SqlParameter("@CompanyID",SqlDbType.VarChar,2)
                                                };

                    for (int i = 0; i != list.Count; i++)
                    {
                        parameters[0].Value = ((BudgetYearDetailInfo)list[i]).BudgetYearDetailID;
                        parameters[1].Value = ((BudgetYearDetailInfo)list[i]).BudgetYearID;
                        parameters[2].Value = ((BudgetYearDetailInfo)list[i]).SubID;
                        parameters[3].Value = ((BudgetYearDetailInfo)list[i]).Name;
                        parameters[4].Value = ((BudgetYearDetailInfo)list[i]).DepartmentID;
                        parameters[5].Value = ((BudgetYearDetailInfo)list[i]).ParentID;
                        parameters[6].Value = ((BudgetYearDetailInfo)item.DetailList[i]).BudgetApply;
                        parameters[7].Value = ((BudgetYearDetailInfo)item.DetailList[i]).BudgetApprove;
                        parameters[8].Value = ((BudgetYearDetailInfo)list[i]).IsLeaf;
                        parameters[9].Value = ((BudgetYearDetailInfo)list[i]).CompanyID;

                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                    }
                }
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new DALException("修改年度预算失败", e);
            }
        }

        private void UpdateBudgetYearBase(SqlTransaction trans, BudgetYearInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_AnnualBudget set ");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("BudgetApply=@BudgetApply,");
            strSql.Append("BudgetApprove=@BudgetApprove,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("Year=@Year,");
            strSql.Append("Maker=@Maker,");
            //strSql.Append("MakeTime=@MakeTime,");
            strSql.Append("Status=@Status,");
            strSql.Append("Approvaler=@Approvaler,");
            strSql.Append("Result=@Result,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Title=@Title");
            strSql.Append(" where BudgetYearID=@BudgetYearID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BudgetYearID", SqlDbType.BigInt,8),
					new SqlParameter("@Attachment", SqlDbType.VarChar,80),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@BudgetApply", SqlDbType.Decimal,9),
					new SqlParameter("@BudgetApprove", SqlDbType.Decimal,9),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@Maker", SqlDbType.VarChar,20),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@Approvaler", SqlDbType.VarChar,20),
					new SqlParameter("@Result", SqlDbType.Bit,1),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
                    new SqlParameter("@Title",SqlDbType.NVarChar,50)
                                        };
            parameters[0].Value = model.BudgetYearID;
            parameters[1].Value = model.Attachment;
            parameters[2].Value = model.UpdateTime;
            parameters[3].Value = model.BudgetApply;
            parameters[4].Value = model.BudgetApprove;
            parameters[5].Value = model.CompanyID;
            parameters[6].Value = model.Year;
            parameters[7].Value = model.Maker;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.Approvaler;
            parameters[10].Value = model.Result;
            parameters[11].Value = model.Remark;
            parameters[12].Value = model.Title;

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        }

        public void DelBudgetYear(long id)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                AnnualBudget bll = new AnnualBudget();
                BudgetYearDetailInfo budgetyeardetailinfo = new BudgetYearDetailInfo();
                budgetyeardetailinfo.BudgetYearID = id;
                IList<BudgetYearDetailInfo> list = bll.Search(budgetyeardetailinfo);

                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_AnnualBudgetDetail ");
                strSql.Append(" where BudgetYearDetailID=@BudgetYearDetailID ");
                SqlParameter[] parameters = {
					new SqlParameter("@BudgetYearDetailID", SqlDbType.BigInt)};
                foreach (BudgetYearDetailInfo model in list)
                {
                    parameters[0].Value = model.BudgetYearDetailID;
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                }
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("delete FM2E_AnnualBudget ");
                strSql2.Append(" where BudgetYearID=@BudgetYearID ");
                SqlParameter[] parameters2 = {
					new SqlParameter("@BudgetYearID", SqlDbType.BigInt)};
                parameters2[0].Value = id;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new DALException("删除年度预算失败", e);
            }


        }
        public IList<BudgetYearInfo> Search(BudgetYearInfo item)
        {
            string cmd = "select " + BudgetYearReturnFields + " from " + BudgetYearTableName + BudgetYearWhere;
            if (item.CompanyID != null && item.CompanyID != string.Empty)
                cmd += " and CompanyID = '" + item.CompanyID + "' ";
            if (item.Year != 0)
                cmd += " and Year = " + item.Year + " ";
            if (item.Title != null && item.Title != string.Empty)
                cmd += " and Title = '" + item.Title + "' ";
            if (item.WorkFlowStatus != null && item.WorkFlowStatus.Count > 0)
            {
                for (int i = 0; i < item.WorkFlowStatus.Count; i++)
                {
                    if (i == 0)
                    {
                        cmd += " and ( ";
                        cmd += " " + " CurrentStateName='" + item.WorkFlowStatus[i] + "' ";
                    }
                    else
                    {
                        cmd += " or " + " CurrentStateName='" + item.WorkFlowStatus[i] + "' ";
                    }
                    if (i == item.WorkFlowStatus.Count - 1)
                    {
                        cmd += " ) ";
                    }
                }
            }
            if (!string.IsNullOrEmpty(item.WorkFlowUserName))
                cmd += " and NextUserName = '" + item.WorkFlowUserName + "' ";
            cmd += BudgetYearOrderBy;
            List<BudgetYearInfo> list = new List<BudgetYearInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        list.Add(GetBudgetYearData(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("搜索本公司年度预算失败", e);
            }

            return list;
        }

        private BudgetYearInfo GetBudgetYearData(IDataReader dr)
        {
            BudgetYearInfo item = new BudgetYearInfo();
            if (!Convert.IsDBNull(dr["BudgetYearID"]))
                item.BudgetYearID = Convert.ToInt64(dr["BudgetYearID"]);
            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);
            if (!Convert.IsDBNull(dr["Year"]))
                item.Year = Convert.ToInt32(dr["Year"]);
            if (!Convert.IsDBNull(dr["Maker"]))
                item.Maker = Convert.ToString(dr["Maker"]);
            if (!Convert.IsDBNull(dr["MakeTime"]))
                item.MakeTime = Convert.ToDateTime(dr["MakeTime"]);
            if (!Convert.IsDBNull(dr["Status"]))
                item.Status = Convert.ToInt16(dr["Status"]);
            if (!Convert.IsDBNull(dr["Approvaler"]))
                item.Approvaler = Convert.ToString(dr["Approvaler"]);
            if (!Convert.IsDBNull(dr["Result"]))
                item.Result = Convert.ToBoolean(dr["Result"]);
            if (!Convert.IsDBNull(dr["Remark"]))
                item.Remark = Convert.ToString(dr["Remark"]);
            if (!Convert.IsDBNull(dr["Attachment"]))
                item.Attachment = Convert.ToString(dr["Attachment"]);
            if (!Convert.IsDBNull(dr["UpdateTime"]))
                item.UpdateTime = Convert.ToDateTime(dr["UpdateTime"]);
            if (!Convert.IsDBNull(dr["BudgetApply"]))
                item.BudgetApply = Convert.ToDecimal(dr["BudgetApply"]);
            if (!Convert.IsDBNull(dr["BudgetApprove"]))
                item.BudgetApprove = Convert.ToDecimal(dr["BudgetApprove"]);
            if (!Convert.IsDBNull(dr["Title"]))
                item.Title = Convert.ToString(dr["Title"]);

            if (!Convert.IsDBNull(dr["NextUserName"]))
            {
                item.NextUserName = Convert.ToString(dr["NextUserName"]);
            }
            if (!Convert.IsDBNull(dr["NextUserPersonName"]))
            {
                item.NextUserPersonName = Convert.ToString(dr["NextUserPersonName"]);
            }
            if (!Convert.IsDBNull(dr["NextUserPositionName"]))
            {
                item.NextUserPositionName = Convert.ToString(dr["NextUserPositionName"]);
            }
            if (!Convert.IsDBNull(dr["NextUserDepartmentID"]))
            {
                item.NextUserDepartmentID = Convert.ToInt64(dr["NextUserDepartmentID"]);
            }
            if (!Convert.IsDBNull(dr["NextUserDepartmentName"]))
            {
                item.NextUserDepartmentName = Convert.ToString(dr["NextUserDepartmentName"]);
            }

            if (!Convert.IsDBNull(dr["DelegateUserName"]))
            {
                item.DelegateUserName = Convert.ToString(dr["DelegateUserName"]);
            }
            if (!Convert.IsDBNull(dr["DelegateUserPersonName"]))
            {
                item.DelegateUserPersonName = Convert.ToString(dr["DelegateUserPersonName"]);
            }
            if (!Convert.IsDBNull(dr["DelegateUserPositionName"]))
            {
                item.DelegateUserPositionName = Convert.ToString(dr["DelegateUserPositionName"]);
            }
            if (!Convert.IsDBNull(dr["DelegateUserDepartmentID"]))
            {
                item.DelegateUserDepartmentID = Convert.ToInt64(dr["DelegateUserDepartmentID"]);
            }
            if (!Convert.IsDBNull(dr["DelegateUserDepartmentName"]))
            {
                item.DelegateUserDepartmentName = Convert.ToString(dr["DelegateUserDepartmentName"]);
            }

            if (!Convert.IsDBNull(dr["InstanceID"]))
            {
                item.WorkFlowInstanceID = Convert.ToString(dr["InstanceID"]);
            }
            if (!Convert.IsDBNull(dr["StatusDescription"]))
            {
                item.WorkFlowStateDescription = Convert.ToString(dr["StatusDescription"]);
            }
            if (!Convert.IsDBNull(dr["CurrentStateName"]))
            {
                item.WorkFlowStateName = Convert.ToString(dr["CurrentStateName"]);
            }

            return item;

        }

        public QueryParam GenerateSearchTerm(BudgetYearDetailInfo item)
        {
            QueryParam qp = new QueryParam();
            return qp;
        }
        public IList GetBudgetYearDetailList(QueryParam term, out int recordCount, string companyid)
        {
            //IList list = null;
            recordCount = 0;
            return null;
        }
        public BudgetYearDetailInfo GetBudgetYearDetail(long id)
        {
            BudgetYearDetailInfo item = new BudgetYearDetailInfo();
            return item;
        }

        public void InsertBudgetYearDetail(BudgetYearDetailInfo item)
        {

        }
        public void UpdateBudgetYearDetail(BudgetYearDetailInfo item)
        {
        }
        public void DelBudgetYearDetail(long id)
        {
        }
        public IList<BudgetYearDetailInfo> Search(BudgetYearDetailInfo item)
        {
            string cmd = "select " + BudgetYearDetailRetuanFields + " from " + BudgetYearDetailTableName + BudgetYearDetailWhere;
            if (item.BudgetYearID != 0)
                cmd += " and BudgetYearID = " + item.BudgetYearID + " ";
            if (item.CompanyID != null && item.CompanyID != string.Empty)
                cmd += " and CompanyID = '" + item.CompanyID + "' ";
            if (item.SubID != 0)
                cmd += " and SubID = " + item.SubID + " ";

            cmd += BudgetYearDetailOrderBy;

            List<BudgetYearDetailInfo> list = new List<BudgetYearDetailInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        list.Add(GetBudgetYearDetailData(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("搜索本公司年度预算详细明细失败", e);
            }

            return list;
        }

        private BudgetYearDetailInfo GetBudgetYearDetailData(IDataReader dr)
        {
            BudgetYearDetailInfo item = new BudgetYearDetailInfo();
            if (!Convert.IsDBNull(dr["BudgetYearDetailID"]))
                item.BudgetYearDetailID = Convert.ToInt64(dr["BudgetYearDetailID"]);
            if (!Convert.IsDBNull(dr["BudgetYearID"]))
                item.BudgetYearID = Convert.ToInt64(dr["BudgetYearID"]);
            if (!Convert.IsDBNull(dr["SubID"]))
                item.SubID = Convert.ToInt64(dr["SubID"]);
            if (!Convert.IsDBNull(dr["Name"]))
                item.Name = Convert.ToString(dr["Name"]);
            if (!Convert.IsDBNull(dr["DepartmentID"]))
                item.DepartmentID = Convert.ToInt64(dr["DepartmentID"]);
            if (!Convert.IsDBNull(dr["ParentID"]))
                item.ParentID = Convert.ToInt64(dr["ParentID"]);
            if (!Convert.IsDBNull(dr["BudgetApply"]))
                item.BudgetApply = Convert.ToDecimal(dr["BudgetApply"]);
            if (!Convert.IsDBNull(dr["BudgetApprove"]))
                item.BudgetApprove = Convert.ToDecimal(dr["BudgetApprove"]);
            if (!Convert.IsDBNull(dr["IsLeaf"]))
                item.IsLeaf = Convert.ToBoolean(dr["IsLeaf"]);
            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);

            return item;
        }      

        public void SaveCurrentSubject(long Year, ref Hashtable ht)
        {
            SubjectRelation subjectdal = new SubjectRelation();
            SubjectRelationInfos info = new SubjectRelationInfos();
            IList list = (List<SubjectRelationInfos>)subjectdal.Search(info);
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FM2E_BudgetAccountsPerYear(");
                strSql.Append("Year,SubID,Name,IsLeaf,CompanyID,ParentID)");
                strSql.Append(" values (");
                strSql.Append("@Year,@SubID,@Name,@IsLeaf,@CompanyID,@ParentID)");
                SqlParameter[] parameters = {
                    new SqlParameter("@Year", SqlDbType.BigInt,8),
                    new SqlParameter("@SubID", SqlDbType.BigInt,8),
                    new SqlParameter("@Name", SqlDbType.VarChar,100),
                    new SqlParameter("@IsLeaf", SqlDbType.Bit,1),
                    new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
                    new SqlParameter("@ParentID", SqlDbType.BigInt,8)};
                foreach (SubjectRelationInfos model in list)
                {
                    //if (ht.ContainsKey(model.SubID.ToString()))
                    //{
                    parameters[0].Value = Year;
                    parameters[1].Value = model.SubID;
                    parameters[2].Value = model.Name;
                    parameters[3].Value = (model.IsLeaf == 1) ? 1 : 0;
                    parameters[4].Value = model.CompanyID;
                    parameters[5].Value = model.ParentID;
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                    //}

                }
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new DALException("保存当前预算用到的科目失败", e);
            }
        }
        public void UpdateCurrentSubject(long Year)
        {
            SubjectRelation subjectdal = new SubjectRelation();
            SubjectRelationInfos info = new SubjectRelationInfos();
            IList list = (List<SubjectRelationInfos>)subjectdal.Search(info);
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();
                StringBuilder delstr = new StringBuilder();
                delstr.Append("delete FM2E_BudgetAccountsPerYear where Year=@Year");
                SqlParameter delparameters = new SqlParameter("@Year", SqlDbType.BigInt, 8);
                delparameters.Value = Year;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, delstr.ToString(), delparameters);

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FM2E_BudgetAccountsPerYear(");
                strSql.Append("Year,SubID,Name,IsLeaf,CompanyID,ParentID)");
                strSql.Append(" values (");
                strSql.Append("@Year,@SubID,@Name,@IsLeaf,@CompanyID,@ParentID)");
                SqlParameter[] parameters = {
                    new SqlParameter("@Year", SqlDbType.BigInt,8),
                    new SqlParameter("@SubID", SqlDbType.BigInt,8),
                    new SqlParameter("@Name", SqlDbType.VarChar,100),
                    new SqlParameter("@IsLeaf", SqlDbType.Bit,1),
                    new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
                    new SqlParameter("@ParentID", SqlDbType.BigInt,8)};
                foreach (SubjectRelationInfos model in list)
                {
                    parameters[0].Value = Year;
                    parameters[1].Value = model.SubID;
                    parameters[2].Value = model.Name;
                    parameters[3].Value = (model.IsLeaf == 1) ? 1 : 0;
                    parameters[4].Value = model.CompanyID;
                    parameters[5].Value = model.ParentID;
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                }
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new DALException("更新当前预算用到的科目失败", e);
            }
        }

    }
}
