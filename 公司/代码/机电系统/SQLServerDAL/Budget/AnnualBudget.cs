﻿using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Budget;
using FM2E.Model.Utils;
using System.Collections;
using FM2E.Model.Budget;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;
using System.Data;
using System.Data.SqlTypes;
using FM2E.Model.Exceptions;
using FM2E.SQLServerDAL.Basic;
using FM2E.Model.Basic;

namespace FM2E.SQLServerDAL.Budget
{
    public class AnnualBudget : IAnnualBudget
    {

        private const string BudgetYearTableName = " FM2E_BudgetYearView ";

        private const string BudgetYearDetailTableName = " FM2E_BudgetYearDetail ";

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
            strSql.Append("SELECT MAX(BudgetYearID) AS MaxID FROM FM2E_BudgetYear WHERE (Status = 3)");
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
                strSql.Append("insert into FM2E_BudgetYearDetail(");
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
            strSql.Append("insert into FM2E_BudgetYear(");
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
                    strSql.Append("update FM2E_BudgetYearDetail set ");
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
            strSql.Append("update FM2E_BudgetYear set ");
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
                strSql.Append("delete FM2E_BudgetYearDetail ");
                strSql.Append(" where BudgetYearDetailID=@BudgetYearDetailID ");
                SqlParameter[] parameters = {
					new SqlParameter("@BudgetYearDetailID", SqlDbType.BigInt)};
                foreach (BudgetYearDetailInfo model in list)
                {
                    parameters[0].Value = model.BudgetYearDetailID;
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                }
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("delete FM2E_BudgetYear ");
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        public QueryParam GenerateSearchTerm(BudgetPerMonthTotalInfo item)
        {

            QueryParam qp = new QueryParam();
            qp.TableName = BudgetPerMonthTotalTableName;
            qp.ReturnFields = BudgetPerMonthTotalReturnFields;
            qp.OrderBy = BudgetPerMonthTotalOrderBy;
            qp.Where = BudgetPerMonthTotalWhere;
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

        public IList GetBudgetPerMonthTotalList(QueryParam term, out int recordCount, string companyid)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = BudgetPerMonthTotalTableName;
                    term.ReturnFields = BudgetPerMonthTotalReturnFields;
                    term.OrderBy = BudgetPerMonthTotalOrderBy;
                    term.Where = BudgetPerMonthTotalWhere;
                }
                if (companyid != null && companyid != string.Empty)
                    term.Where += " and CompanyID = '" + companyid + "' ";
                return SQLHelper.GetObjectList(this.GetBudgetPerMonthTotalData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException(" 获取月度预算分页失败", e);
            }
        }

        private BudgetPerMonthTotalInfo GetBudgetPerMonthTotalData(IDataReader dr)
        {
            BudgetPerMonthTotalInfo item = new BudgetPerMonthTotalInfo();
            if (!Convert.IsDBNull(dr["TotalID"]))
                item.TotalID = Convert.ToInt64(dr["TotalID"]);
            if (!Convert.IsDBNull(dr["Year"]))
                item.Year = Convert.ToInt32(dr["Year"]);
            if (!Convert.IsDBNull(dr["Month"]))
                item.Month = Convert.ToInt32(dr["Month"]);
            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);
            if (!Convert.IsDBNull(dr["ViceEngineerReview"]))
                item.ViceEngineerReview = Convert.ToString(dr["ViceEngineerReview"]);
            if (!Convert.IsDBNull(dr["ViceManagerReview"]))
                item.ViceManagerReview = Convert.ToString(dr["ViceManagerReview"]);
            if (!Convert.IsDBNull(dr["ManagerReview"]))
                item.ManagerReview = Convert.ToString(dr["ManagerReview"]);
            if (!Convert.IsDBNull(dr["FinanceReview"]))
                item.FinanceReview = Convert.ToString(dr["FinanceReview"]);
            if (!Convert.IsDBNull(dr["Result"]))
                item.Result = Convert.ToBoolean(dr["Result"]);
            if (!Convert.IsDBNull(dr["TotalExpenditure"]))
                item.TotalExpenditure = Convert.ToDecimal(dr["TotalExpenditure"]);
            if (!Convert.IsDBNull(dr["BudgetPermonth"]))
                item.BudgetPermonth = Convert.ToDecimal(dr["BudgetPermonth"]);
            if (!Convert.IsDBNull(dr["SurplusExpenditure"]))
                item.SurplusExpenditure = Convert.ToDecimal(dr["SurplusExpenditure"]);
            if (!Convert.IsDBNull(dr["NonPayment"]))
                item.NonPayment = Convert.ToDecimal(dr["NonPayment"]);
            if (!Convert.IsDBNull(dr["Total"]))
                item.Total = Convert.ToDecimal(dr["Total"]);
            if (!Convert.IsDBNull(dr["MakeTime"]))
                item.MakeTime = Convert.ToDateTime(dr["MakeTime"]);
            if (!Convert.IsDBNull(dr["Expenditure"]))
                item.Expenditure = Convert.ToDecimal(dr["Expenditure"]);
            if (!Convert.IsDBNull(dr["Allocation"]))
                item.Allocation = Convert.ToDecimal(dr["Allocation"]);
            if (!Convert.IsDBNull(dr["Deviation"]))
                item.Deviation = Convert.ToDecimal(dr["Deviation"]);
            if (!Convert.IsDBNull(dr["BudgetApply"]))
                item.BudgetApply = Convert.ToDecimal(dr["BudgetApply"]);
            if (!Convert.IsDBNull(dr["Status"]))
                item.Status = Convert.ToInt16(dr["Status"]);
            if (!Convert.IsDBNull(dr["BudgetYearID"]))
                item.BudgetYearID = Convert.ToInt64(dr["BudgetYearID"]);
            if (!Convert.IsDBNull(dr["Approvaler"]))
                item.Approvaler = Convert.ToString(dr["Approvaler"]);

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
            if (!Convert.IsDBNull(dr["Title"]))
                item.Title = Convert.ToString(dr["Title"]);

            return item;
        }

        public BudgetPerMonthTotalInfo GetBudgetPerMonthTotal(long id)
        {
            BudgetPerMonthTotalInfo item = null;
            string cmd = string.Format(SELECT_BUDGETMONTHTOTAL, id);

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        item = GetBudgetPerMonthTotalData(rd);
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取月度预算基本信息失败", e);
            }

            return item;
        }

        public long InsertBudgetPerMonthTotal(BudgetPerMonthTotalInfo item)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            long id = 0;
            try
            {
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                id = InsertBudgetPerMonthTotalBase(trans, item);
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FM2E_BudgetPermonth(");
                strSql.Append("Remarks,Manager,MakeTime,Expenditure,Allocation,Deviation,ReasonForDeviation,EvaluationForDeviation,Review,TotalID,BudgetYearDetailID,SubID,BudgetApply,CompanyID,Month,TotalExpenditure,BudgetPermonth,SurplusExpenditure,NonPayment,Total)");
                strSql.Append(" values (");
                strSql.Append("@Remarks,@Manager,@MakeTime,@Expenditure,@Allocation,@Deviation,@ReasonForDeviation,@EvaluationForDeviation,@Review,@TotalID,@BudgetYearDetailID,@SubID,@BudgetApply,@CompanyID,@Month,@TotalExpenditure,@BudgetPermonth,@SurplusExpenditure,@NonPayment,@Total)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@Remarks", SqlDbType.VarChar,100),
					new SqlParameter("@Manager", SqlDbType.VarChar,50),
					new SqlParameter("@MakeTime", SqlDbType.DateTime),
					new SqlParameter("@Expenditure", SqlDbType.Decimal,9),
					new SqlParameter("@Allocation", SqlDbType.Decimal,9),
					new SqlParameter("@Deviation", SqlDbType.Decimal,9),
					new SqlParameter("@ReasonForDeviation", SqlDbType.VarChar,30),
					new SqlParameter("@EvaluationForDeviation", SqlDbType.VarChar,20),
					new SqlParameter("@Review", SqlDbType.TinyInt,1),
					new SqlParameter("@TotalID", SqlDbType.BigInt,8),
					new SqlParameter("@BudgetYearDetailID", SqlDbType.BigInt,8),
					new SqlParameter("@SubID", SqlDbType.BigInt,8),
					new SqlParameter("@BudgetApply", SqlDbType.Decimal,9),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Month", SqlDbType.Int,4),
					new SqlParameter("@TotalExpenditure", SqlDbType.Decimal,9),
					new SqlParameter("@BudgetPermonth", SqlDbType.Decimal,9),
					new SqlParameter("@SurplusExpenditure", SqlDbType.Decimal,9),
					new SqlParameter("@NonPayment", SqlDbType.Decimal,9),
					new SqlParameter("@Total", SqlDbType.Decimal,9)};
                foreach (BudgetPermonthInfo model in item.DetailList)
                {
                    parameters[0].Value = model.Remarks;
                    parameters[1].Value = model.Manager;
                    parameters[2].Value = model.MakeTime;
                    parameters[3].Value = model.Expenditure;
                    parameters[4].Value = model.Allocation;
                    parameters[5].Value = model.Deviation;
                    parameters[6].Value = model.ReasonForDeviation;
                    parameters[7].Value = model.EvaluationForDeviation;
                    parameters[8].Value = model.Review;
                    parameters[9].Value = id;
                    parameters[10].Value = model.BudgetYearDetailID;
                    parameters[11].Value = model.SubID;
                    parameters[12].Value = model.BudgetApply;
                    parameters[13].Value = model.CompanyID;
                    parameters[14].Value = model.Month;
                    parameters[15].Value = model.TotalExpenditure;
                    parameters[16].Value = model.BudgetPermonth;
                    parameters[17].Value = model.SurplusExpenditure;
                    parameters[18].Value = model.NonPayment;
                    parameters[19].Value = model.Total;
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                }
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("insert into FM2E_BudgetDetail(");
                strSql2.Append("ExpenditureDetail,BudgetApprove,Year,Month,SubID,Approvaler,Attachment,TotalID,SubName,BudgetPermonthID,ExpenditureName,Expenditure,Review,Manager,Remarks,RecordDate,System,CompanyID,CompanyName)");
                strSql2.Append(" values (");
                strSql2.Append("@ExpenditureDetail,@BudgetApprove,@Year,@Month,@SubID,@Approvaler,@Attachment,@TotalID,@SubName,@BudgetPermonthID,@ExpenditureName,@Expenditure,@Review,@Manager,@Remarks,@RecordDate,@System,@CompanyID,@CompanyName)");
                strSql2.Append(";select @@IDENTITY");
                SqlParameter[] parameters2 = {
					new SqlParameter("@ExpenditureDetail", SqlDbType.VarChar,80),
					new SqlParameter("@BudgetApprove", SqlDbType.Decimal,9),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@Month", SqlDbType.Int,4),
					new SqlParameter("@SubID", SqlDbType.BigInt,8),
					new SqlParameter("@Approvaler", SqlDbType.VarChar,20),
					new SqlParameter("@Attachment", SqlDbType.VarChar,500),
					new SqlParameter("@TotalID", SqlDbType.BigInt,8),
					new SqlParameter("@SubName", SqlDbType.VarChar,30),
					new SqlParameter("@BudgetPermonthID", SqlDbType.BigInt,8),
					new SqlParameter("@ExpenditureName", SqlDbType.VarChar,30),
					new SqlParameter("@Expenditure", SqlDbType.Decimal,9),
					new SqlParameter("@Review", SqlDbType.VarChar,10),
					new SqlParameter("@Manager", SqlDbType.VarChar,10),
					new SqlParameter("@Remarks", SqlDbType.VarChar,30),
					new SqlParameter("@RecordDate", SqlDbType.DateTime),
					new SqlParameter("@System", SqlDbType.VarChar,10),
                    new SqlParameter("@CompanyID",SqlDbType.VarChar,2),
                    new SqlParameter("@CompanyName",SqlDbType.NVarChar,20)
                                             };
                foreach (BudgetDetailInfo model in item.BudgetDetailList)
                {
                    parameters2[0].Value = model.ExpenditureDetail;
                    parameters2[1].Value = model.BudgetApprove;
                    parameters2[2].Value = model.Year;
                    parameters2[3].Value = model.Month;
                    parameters2[4].Value = model.SubID;
                    parameters2[5].Value = model.Approvaler;
                    parameters2[6].Value = model.Attachment;
                    parameters2[7].Value = id;
                    parameters2[8].Value = model.SubName;
                    parameters2[9].Value = model.BudgetPermonthID;
                    parameters2[10].Value = model.ExpenditureName;
                    parameters2[11].Value = model.Expenditure;
                    parameters2[12].Value = model.Review;
                    parameters2[13].Value = model.Manager;
                    parameters2[14].Value = model.Remarks;
                    parameters2[15].Value = model.RecordDate;
                    parameters2[16].Value = model.System;
                    parameters2[17].Value = model.CompanyID;
                    parameters2[18].Value = model.CompanyName;
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);
                }
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new DALException("添加月度预算信息失败", e);
            }
            return id;
        }

        private long InsertBudgetPerMonthTotalBase(SqlTransaction trans, BudgetPerMonthTotalInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_BudgetPerMonthTotal(");
            strSql.Append("TotalExpenditure,BudgetPermonth,SurplusExpenditure,NonPayment,Total,MakeTime,Expenditure,Allocation,Deviation,BudgetApply,Year,Status,BudgetYearID,Month,CompanyID,ViceEngineerReview,ViceManagerReview,ManagerReview,FinanceReview,Result,Approvaler,Title)");
            strSql.Append(" values (");
            strSql.Append("@TotalExpenditure,@BudgetPermonth,@SurplusExpenditure,@NonPayment,@Total,@MakeTime,@Expenditure,@Allocation,@Deviation,@BudgetApply,@Year,@Status,@BudgetYearID,@Month,@CompanyID,@ViceEngineerReview,@ViceManagerReview,@ManagerReview,@FinanceReview,@Result,@Approvaler,@Title)");
            strSql.Append(";select cast(@@IDENTITY as BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@TotalExpenditure", SqlDbType.Decimal,9),
					new SqlParameter("@BudgetPermonth", SqlDbType.Decimal,9),
					new SqlParameter("@SurplusExpenditure", SqlDbType.Decimal,9),
					new SqlParameter("@NonPayment", SqlDbType.Decimal,9),
					new SqlParameter("@Total", SqlDbType.Decimal,9),
					new SqlParameter("@MakeTime", SqlDbType.DateTime),
					new SqlParameter("@Expenditure", SqlDbType.Decimal,9),
					new SqlParameter("@Allocation", SqlDbType.Decimal,9),
					new SqlParameter("@Deviation", SqlDbType.Decimal,9),
					new SqlParameter("@BudgetApply", SqlDbType.Decimal,9),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@BudgetYearID", SqlDbType.BigInt,8),
					new SqlParameter("@Month", SqlDbType.Int,4),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@ViceEngineerReview", SqlDbType.VarChar,10),
					new SqlParameter("@ViceManagerReview", SqlDbType.VarChar,10),
					new SqlParameter("@ManagerReview", SqlDbType.VarChar,10),
					new SqlParameter("@FinanceReview", SqlDbType.VarChar,30),
					new SqlParameter("@Result", SqlDbType.Bit,1),
                    new SqlParameter("@Approvaler",SqlDbType.VarChar,50),
                    new SqlParameter("@Title",SqlDbType.VarChar,50)};
            parameters[0].Value = model.TotalExpenditure;
            parameters[1].Value = model.BudgetPermonth;
            parameters[2].Value = model.SurplusExpenditure;
            parameters[3].Value = model.NonPayment;
            parameters[4].Value = model.Total;
            parameters[5].Value = model.MakeTime;
            parameters[6].Value = model.Expenditure;
            parameters[7].Value = model.Allocation;
            parameters[8].Value = model.Deviation;
            parameters[9].Value = model.BudgetApply;
            parameters[10].Value = model.Year;
            parameters[11].Value = model.Status;
            parameters[12].Value = model.BudgetYearID;
            parameters[13].Value = model.Month;
            parameters[14].Value = model.CompanyID;
            parameters[15].Value = model.ViceEngineerReview;
            parameters[16].Value = model.ViceManagerReview;
            parameters[17].Value = model.ManagerReview;
            parameters[18].Value = model.FinanceReview;
            parameters[19].Value = model.Result;
            parameters[20].Value = model.Approvaler;
            parameters[21].Value = model.Title;
            long id = 1;
            id = (long)SQLHelper.ExecuteScalar(trans, CommandType.Text, strSql.ToString(), parameters);

            return id;
        }

        public void UpdateBudgetPerMonthTotal(BudgetPerMonthTotalInfo item)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            long id = item.TotalID;
            try
            {
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();
                UpdateBudgetPermonthTotalBase(trans, item);
                if (item.DetailList != null && item.DetailList.Count != 0)
                {
                    BudgetPermonthInfo budgetpermonthinfo = new BudgetPermonthInfo();
                    budgetpermonthinfo.TotalID = id;
                    IList<BudgetPermonthInfo> list = this.Search(budgetpermonthinfo);

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update FM2E_BudgetPermonth set ");
                    strSql.Append("Remarks=@Remarks,");
                    strSql.Append("Manager=@Manager,");
                    strSql.Append("MakeTime=@MakeTime,");
                    strSql.Append("Expenditure=@Expenditure,");
                    strSql.Append("Allocation=@Allocation,");
                    strSql.Append("Deviation=@Deviation,");
                    strSql.Append("ReasonForDeviation=@ReasonForDeviation,");
                    strSql.Append("EvaluationForDeviation=@EvaluationForDeviation,");
                    strSql.Append("Review=@Review,");
                    strSql.Append("TotalID=@TotalID,");
                    strSql.Append("BudgetYearDetailID=@BudgetYearDetailID,");
                    strSql.Append("SubID=@SubID,");
                    strSql.Append("BudgetApply=@BudgetApply,");
                    strSql.Append("CompanyID=@CompanyID,");
                    strSql.Append("Month=@Month,");
                    strSql.Append("TotalExpenditure=@TotalExpenditure,");
                    strSql.Append("BudgetPermonth=@BudgetPermonth,");
                    strSql.Append("SurplusExpenditure=@SurplusExpenditure,");
                    strSql.Append("NonPayment=@NonPayment,");
                    strSql.Append("Total=@Total ");

                    strSql.Append(" where BudgetPermonthID=@BudgetPermonthID ");
                    SqlParameter[] parameters = {
					new SqlParameter("@BudgetPermonthID", SqlDbType.BigInt,8),
					new SqlParameter("@Remarks", SqlDbType.VarChar,100),
					new SqlParameter("@Manager", SqlDbType.VarChar,50),
					new SqlParameter("@MakeTime", SqlDbType.DateTime),
					new SqlParameter("@Expenditure", SqlDbType.Decimal,9),
					new SqlParameter("@Allocation", SqlDbType.Decimal,9),
					new SqlParameter("@Deviation", SqlDbType.Decimal,9),
					new SqlParameter("@ReasonForDeviation", SqlDbType.VarChar,30),
					new SqlParameter("@EvaluationForDeviation", SqlDbType.VarChar,20),
					new SqlParameter("@Review", SqlDbType.TinyInt,1),
					new SqlParameter("@TotalID", SqlDbType.BigInt,8),
					new SqlParameter("@BudgetYearDetailID", SqlDbType.BigInt,8),
					new SqlParameter("@SubID", SqlDbType.BigInt,8),
					new SqlParameter("@BudgetApply", SqlDbType.Decimal,9),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Month", SqlDbType.Int,4),
					new SqlParameter("@TotalExpenditure", SqlDbType.Decimal,9),
					new SqlParameter("@BudgetPermonth", SqlDbType.Decimal,9),
					new SqlParameter("@SurplusExpenditure", SqlDbType.Decimal,9),
					new SqlParameter("@NonPayment", SqlDbType.Decimal,9),
					new SqlParameter("@Total", SqlDbType.Decimal,9)};
                    for (int i = 0; i != list.Count; i++)
                    {
                        parameters[0].Value = ((BudgetPermonthInfo)list[i]).BudgetPermonthID;
                        parameters[1].Value = ((BudgetPermonthInfo)list[i]).Remarks;
                        parameters[2].Value = ((BudgetPermonthInfo)list[i]).Manager;
                        parameters[3].Value = ((BudgetPermonthInfo)item.DetailList[i]).MakeTime;
                        parameters[4].Value = ((BudgetPermonthInfo)list[i]).Expenditure;
                        parameters[5].Value = ((BudgetPermonthInfo)list[i]).Allocation;
                        parameters[6].Value = ((BudgetPermonthInfo)list[i]).Deviation;
                        parameters[7].Value = ((BudgetPermonthInfo)list[i]).ReasonForDeviation;
                        parameters[8].Value = ((BudgetPermonthInfo)list[i]).EvaluationForDeviation;
                        parameters[9].Value = ((BudgetPermonthInfo)list[i]).Review;
                        parameters[10].Value = ((BudgetPermonthInfo)list[i]).TotalID;
                        parameters[11].Value = ((BudgetPermonthInfo)list[i]).BudgetYearDetailID;
                        parameters[12].Value = ((BudgetPermonthInfo)list[i]).SubID;
                        parameters[13].Value = ((BudgetPermonthInfo)item.DetailList[i]).BudgetApply;
                        parameters[14].Value = ((BudgetPermonthInfo)list[i]).CompanyID;
                        parameters[15].Value = ((BudgetPermonthInfo)list[i]).Month;
                        parameters[16].Value = ((BudgetPermonthInfo)item.DetailList[i]).TotalExpenditure;
                        parameters[17].Value = ((BudgetPermonthInfo)item.DetailList[i]).BudgetPermonth;
                        parameters[18].Value = ((BudgetPermonthInfo)item.DetailList[i]).SurplusExpenditure;
                        parameters[19].Value = ((BudgetPermonthInfo)item.DetailList[i]).NonPayment;
                        parameters[20].Value = ((BudgetPermonthInfo)item.DetailList[i]).Total;

                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                    }
                }
                if (item.UpdateBudgetDetail)
                {
                    BudgetDetailInfo budgetdetailinfo = new BudgetDetailInfo();
                    budgetdetailinfo.TotalID = id;
                    foreach (BudgetDetailInfo budgetdetailmodel in (List<BudgetDetailInfo>)this.Search(budgetdetailinfo, trans))
                    {
                        DelBudgetDetail(budgetdetailmodel.DetailID, trans);
                    }
                    if (item.BudgetDetailList != null && item.BudgetDetailList.Count != 0)
                    {

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("insert into FM2E_BudgetDetail(");
                        strSql2.Append("ExpenditureDetail,BudgetApprove,Year,Month,SubID,Approvaler,Attachment,TotalID,SubName,BudgetPermonthID,ExpenditureName,Expenditure,Review,Manager,Remarks,RecordDate,System,RealExpenditure,CompanyID,CompanyName,Supplier,PayDate)");
                        strSql2.Append(" values (");
                        strSql2.Append("@ExpenditureDetail,@BudgetApprove,@Year,@Month,@SubID,@Approvaler,@Attachment,@TotalID,@SubName,@BudgetPermonthID,@ExpenditureName,@Expenditure,@Review,@Manager,@Remarks,@RecordDate,@System,@RealExpenditure,@CompanyID,@CompanyName,@Supplier,@PayDate)");
                        strSql2.Append(";select @@IDENTITY");
                        SqlParameter[] parameters2 = {
					    new SqlParameter("@ExpenditureDetail", SqlDbType.VarChar,80),
					    new SqlParameter("@BudgetApprove", SqlDbType.Decimal,9),
					    new SqlParameter("@Year", SqlDbType.Int,4),
					    new SqlParameter("@Month", SqlDbType.Int,4),
					    new SqlParameter("@SubID", SqlDbType.BigInt,8),
					    new SqlParameter("@Approvaler", SqlDbType.VarChar,20),
					    new SqlParameter("@Attachment", SqlDbType.VarChar,500),
					    new SqlParameter("@TotalID", SqlDbType.BigInt,8),
					    new SqlParameter("@SubName", SqlDbType.VarChar,30),
					    new SqlParameter("@BudgetPermonthID", SqlDbType.BigInt,8),
					    new SqlParameter("@ExpenditureName", SqlDbType.VarChar,30),
					    new SqlParameter("@Expenditure", SqlDbType.Decimal,9),
					    new SqlParameter("@Review", SqlDbType.VarChar,10),
					    new SqlParameter("@Manager", SqlDbType.VarChar,10),
					    new SqlParameter("@Remarks", SqlDbType.VarChar,30),
					    new SqlParameter("@RecordDate", SqlDbType.DateTime),
					    new SqlParameter("@System", SqlDbType.VarChar,10),
                        new SqlParameter("@RealExpenditure",SqlDbType.Decimal,9),
                        new SqlParameter("@CompanyID",SqlDbType.VarChar,2),
                        new SqlParameter("@CompanyName",SqlDbType.NVarChar,20),
                        new SqlParameter("@Supplier",SqlDbType.VarChar,100),
                        new SqlParameter("@PayDate",SqlDbType.DateTime)
                                                 };
                        foreach (BudgetDetailInfo model in item.BudgetDetailList)
                        {
                            parameters2[0].Value = model.ExpenditureDetail;
                            parameters2[1].Value = model.BudgetApprove;
                            parameters2[2].Value = model.Year;
                            parameters2[3].Value = model.Month;
                            parameters2[4].Value = model.SubID;
                            parameters2[5].Value = model.Approvaler;
                            parameters2[6].Value = model.Attachment;
                            parameters2[7].Value = id;
                            parameters2[8].Value = model.SubName;
                            parameters2[9].Value = model.BudgetPermonthID;
                            parameters2[10].Value = model.ExpenditureName;
                            parameters2[11].Value = model.Expenditure;
                            parameters2[12].Value = model.Review;
                            parameters2[13].Value = model.Manager;
                            parameters2[14].Value = model.Remarks;
                            parameters2[15].Value = model.RecordDate;
                            parameters2[16].Value = model.System;
                            parameters2[17].Value = model.RealExpenditure;
                            parameters2[18].Value = model.CompanyID;
                            parameters2[19].Value = model.CompanyName;
                            parameters2[20].Value = model.Supplier;
                            parameters2[21].Value = DateTime.Compare(model.PayDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : model.PayDate;
                            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);
                        }
                    }
                }

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new DALException("修改月度预算失败", e);
            }


        }
        private void UpdateBudgetPermonthTotalBase(SqlTransaction trans, BudgetPerMonthTotalInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_BudgetPerMonthTotal set ");
            strSql.Append("TotalExpenditure=@TotalExpenditure,");
            strSql.Append("BudgetPermonth=@BudgetPermonth,");
            strSql.Append("SurplusExpenditure=@SurplusExpenditure,");
            strSql.Append("NonPayment=@NonPayment,");
            strSql.Append("Total=@Total,");
            strSql.Append("Expenditure=@Expenditure,");
            strSql.Append("Allocation=@Allocation,");
            strSql.Append("Deviation=@Deviation,");
            strSql.Append("BudgetApply=@BudgetApply,");
            strSql.Append("Year=@Year,");
            strSql.Append("Status=@Status,");
            strSql.Append("BudgetYearID=@BudgetYearID,");
            strSql.Append("Month=@Month,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("ViceEngineerReview=@ViceEngineerReview,");
            strSql.Append("ViceManagerReview=@ViceManagerReview,");
            strSql.Append("ManagerReview=@ManagerReview,");
            strSql.Append("FinanceReview=@FinanceReview,");
            strSql.Append("Result=@Result,");
            strSql.Append("Approvaler=@Approvaler,");
            strSql.Append("Title=@Title ");
            strSql.Append(" where TotalID=@TotalID ");
            SqlParameter[] parameters = {
					new SqlParameter("@TotalID", SqlDbType.BigInt,8),
					new SqlParameter("@TotalExpenditure", SqlDbType.Decimal,9),
					new SqlParameter("@BudgetPermonth", SqlDbType.Decimal,9),
					new SqlParameter("@SurplusExpenditure", SqlDbType.Decimal,9),
					new SqlParameter("@NonPayment", SqlDbType.Decimal,9),
					new SqlParameter("@Total", SqlDbType.Decimal,9),
					new SqlParameter("@Expenditure", SqlDbType.Decimal,9),
					new SqlParameter("@Allocation", SqlDbType.Decimal,9),
					new SqlParameter("@Deviation", SqlDbType.Decimal,9),
					new SqlParameter("@BudgetApply", SqlDbType.Decimal,9),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@BudgetYearID", SqlDbType.BigInt,8),
					new SqlParameter("@Month", SqlDbType.Int,4),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@ViceEngineerReview", SqlDbType.VarChar,10),
					new SqlParameter("@ViceManagerReview", SqlDbType.VarChar,10),
					new SqlParameter("@ManagerReview", SqlDbType.VarChar,10),
					new SqlParameter("@FinanceReview", SqlDbType.VarChar,30),
					new SqlParameter("@Result", SqlDbType.Bit,1),
                    new SqlParameter("@Approvaler",SqlDbType.VarChar,50),
                    new SqlParameter("@Title",SqlDbType.VarChar,50)};
            parameters[0].Value = model.TotalID;
            parameters[1].Value = model.TotalExpenditure;
            parameters[2].Value = model.BudgetPermonth;
            parameters[3].Value = model.SurplusExpenditure;
            parameters[4].Value = model.NonPayment;
            parameters[5].Value = model.Total;
            parameters[6].Value = model.Expenditure;
            parameters[7].Value = model.Allocation;
            parameters[8].Value = model.Deviation;
            parameters[9].Value = model.BudgetApply;
            parameters[10].Value = model.Year;
            parameters[11].Value = model.Status;
            parameters[12].Value = model.BudgetYearID;
            parameters[13].Value = model.Month;
            parameters[14].Value = model.CompanyID;
            parameters[15].Value = model.ViceEngineerReview;
            parameters[16].Value = model.ViceManagerReview;
            parameters[17].Value = model.ManagerReview;
            parameters[18].Value = model.FinanceReview;
            parameters[19].Value = model.Result;
            parameters[20].Value = model.Approvaler;
            parameters[21].Value = model.Title;

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        }
        public void DelBudgetPerMonthTotal(long id)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();
                AnnualBudget bll = new AnnualBudget();
                BudgetPermonthInfo budgetpermonthinfo = new BudgetPermonthInfo();
                budgetpermonthinfo.TotalID = id;
                IList<BudgetPermonthInfo> list = bll.Search(budgetpermonthinfo);
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_BudgetPermonth ");
                strSql.Append(" where BudgetPermonthID=@BudgetPermonthID ");
                SqlParameter[] parameters = {
					new SqlParameter("@BudgetPermonthID", SqlDbType.BigInt)};

                foreach (BudgetPermonthInfo model in list)
                {
                    parameters[0].Value = model.BudgetPermonthID;
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                }

                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("delete FM2E_BudgetPerMonthTotal ");
                strSql2.Append(" where TotalID=@TotalID ");
                SqlParameter[] parameters2 = {
					new SqlParameter("@TotalID", SqlDbType.BigInt)};
                parameters2[0].Value = id;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);

                StringBuilder strSql3 = new StringBuilder();
                strSql3.Append("delete FM2E_BudgetDetail ");
                strSql3.Append(" where TotalID=@TotalID ");
                SqlParameter[] parameters3 = {
					new SqlParameter("@TotalID", SqlDbType.BigInt)};
                parameters3[0].Value = id;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql3.ToString(), parameters3);

                trans.Commit();


            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new DALException("删除月度预算失败", e);
            }
        }
        public IList<BudgetPerMonthTotalInfo> Search(BudgetPerMonthTotalInfo item)
        {
            string cmd = " select " + BudgetPerMonthTotalReturnFields + " from " + BudgetPerMonthTotalTableName + BudgetPerMonthTotalWhere;
            if (item.BudgetYearID != 0)
                cmd += " and BudgetYearID = " + item.BudgetYearID + " ";
            if (item.Status != 0)
                cmd += " and Status = " + item.Status + " ";
            if (item.CompanyID != null && item.CompanyID != string.Empty)
                cmd += " and CompanyID = '" + item.CompanyID + "' ";
            if (item.Year != 0)
                cmd += " and Year = " + item.Year + " ";
            if (item.Month != 0)
                cmd += " and Month = " + item.Month + " ";
            cmd += BudgetPerMonthTotalOrderBy;

            List<BudgetPerMonthTotalInfo> list = new List<BudgetPerMonthTotalInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        list.Add(GetBudgetPerMonthTotalData(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("搜索本公司月度预算详细明细失败", e);
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public QueryParam GenerateSearchTerm(BudgetPermonthInfo item)
        {
            QueryParam qp = new QueryParam();
            return qp;
        }

        public IList GetBudgetPermonthList(QueryParam term, out int recordCount, string companyid)
        {
            throw new DALException("发生未知错误");
            //return null;
        }

        public BudgetPermonthInfo GetBudgetPermonth(long id)
        {
            BudgetPermonthInfo item = new BudgetPermonthInfo();
            return item;
        }

        public void InsertBudgetPermonth(BudgetPermonthInfo item)
        {
            throw new DALException("发生未知错误");
        }
        public void UpdateBudgetPermonth(BudgetPermonthInfo item)
        {
            throw new DALException("发生未知错误");
        }
        public void DelBudgetPermonth(long id)
        {
            throw new DALException("发生未知错误");
        }
        public IList<BudgetPermonthInfo> Search(BudgetPermonthInfo item)
        {
            string cmd = " select " + BudgetPerMonthReturnFields + " from " + BudgetPerMonthTableName + BudgetPerMonthTotalWhere;
            if (item.TotalID != 0)
                cmd += " and TotalID = " + item.TotalID + " ";
            cmd += BudgetPerMonthOrderBy;

            List<BudgetPermonthInfo> list = new List<BudgetPermonthInfo>();

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        list.Add(GetBudgetPerMonthData(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("搜索本公司月度预算详细明细失败", e);
            }
            return list;
        }

        private BudgetPermonthInfo GetBudgetPerMonthData(IDataReader dr)
        {
            BudgetPermonthInfo item = new BudgetPermonthInfo();
            if (!Convert.IsDBNull(dr["BudgetPermonthID"]))
                item.BudgetPermonthID = Convert.ToInt64(dr["BudgetPermonthID"]);
            if (!Convert.IsDBNull(dr["BudgetYearDetailID"]))
                item.BudgetYearDetailID = Convert.ToInt64(dr["BudgetYearDetailID"]);
            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);
            if (!Convert.IsDBNull(dr["Month"]))
                item.Month = Convert.ToInt32(dr["Month"]);
            if (!Convert.IsDBNull(dr["TotalExpenditure"]))
                item.TotalExpenditure = Convert.ToDecimal(dr["TotalExpenditure"]);
            if (!Convert.IsDBNull(dr["BudgetPermonth"]))
                item.BudgetPermonth = Convert.ToDecimal(dr["BudgetPermonth"]);
            if (!Convert.IsDBNull(dr["SurplusExpenditure"]))
                item.SurplusExpenditure = Convert.ToDecimal(dr["SurplusExpenditure"]);
            if (!Convert.IsDBNull(dr["NonPayment"]))
                item.NonPayment = Convert.ToDecimal(dr["NonPayment"]);
            if (!Convert.IsDBNull(dr["Total"]))
                item.Total = Convert.ToDecimal(dr["Total"]);
            if (!Convert.IsDBNull(dr["Remarks"]))
                item.Remarks = Convert.ToString(dr["Remarks"]);
            if (!Convert.IsDBNull(dr["Manager"]))
                item.Manager = Convert.ToString(dr["Manager"]);
            if (!Convert.IsDBNull(dr["MakeTime"]))
                item.MakeTime = Convert.ToDateTime(dr["MakeTime"]);
            if (!Convert.IsDBNull(dr["Expenditure"]))
                item.Expenditure = Convert.ToDecimal(dr["Expenditure"]);
            if (!Convert.IsDBNull(dr["Allocation"]))
                item.Allocation = Convert.ToDecimal(dr["Allocation"]);
            if (!Convert.IsDBNull(dr["Deviation"]))
                item.Deviation = Convert.ToDecimal(dr["Deviation"]);
            if (!Convert.IsDBNull(dr["ReasonForDeviation"]))
                item.ReasonForDeviation = Convert.ToString(dr["ReasonForDeviation"]);
            if (!Convert.IsDBNull(dr["EvaluationForDeviation"]))
                item.EvaluationForDeviation = Convert.ToString(dr["EvaluationForDeviation"]);
            if (!Convert.IsDBNull(dr["Review"]))
                item.Review = Convert.ToInt16(dr["Review"]);
            if (!Convert.IsDBNull(dr["TotalID"]))
                item.TotalID = Convert.ToInt64(dr["TotalID"]);
            if (!Convert.IsDBNull(dr["SubID"]))
                item.SubID = Convert.ToInt64(dr["SubID"]);
            if (!Convert.IsDBNull(dr["BudgetApply"]))
                item.BudgetApply = Convert.ToDecimal(dr["BudgetApply"]);

            return item;


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public QueryParam GenerateSearchTerm(BudgetDetailInfo item)
        {
            QueryParam qp = new QueryParam();
            return qp;
        }
        public IList GetBudgetDetailList(QueryParam term, out int recordCount, string companyid)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = BudgetDetailTableName;
                    term.ReturnFields = BudgetDetailReturnFields;
                    term.OrderBy = BudgetDetailOrderBy;
                    term.Where = BudgetPerMonthTotalWhere;
                }
                if (companyid != null && companyid != string.Empty)
                    term.Where += " and CompanyID = '" + companyid + "' ";
                return SQLHelper.GetObjectList(this.GetBudgetDetailData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException(" 获取开支明细分页失败", e);
            }
        }
        public BudgetDetailInfo GetBudgetDetail(long id)
        {
            BudgetDetailInfo item = new BudgetDetailInfo();
            return item;
        }

        public void InsertBudgetDetail(BudgetDetailInfo item)
        {
        }
        public void UpdateBudgetDetail(BudgetDetailInfo item)
        {
        }
        public void DelBudgetDetail(long id)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_BudgetDetail ");
                strSql.Append(" where DetailID=@DetailID ");
                SqlParameter[] parameters = {
					new SqlParameter("@DetailID", SqlDbType.BigInt)};
                parameters[0].Value = id;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new DALException("删除开支明细失败", e);
            }
        }

        private void DelBudgetDetail(long id, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_BudgetDetail ");
            strSql.Append(" where DetailID=@DetailID ");
            SqlParameter[] parameters = {
					new SqlParameter("@DetailID", SqlDbType.BigInt)};
            parameters[0].Value = id;
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

        }
        public IList<BudgetDetailInfo> Search(BudgetDetailInfo item)
        {
            string cmd = " select " + BudgetDetailReturnFields + " from " + BudgetDetailViewTableName + BudgetPerMonthTotalWhere;
            if (item.TotalID != 0)
                cmd += " and TotalID = " + item.TotalID + " ";
            if (item.SubID != 0)
                cmd += " and SubID = " + item.SubID + " ";
            if (item.SubName != null && !item.SubName.Equals(string.Empty))
                cmd += " and SubName = '" + item.SubName + "' ";
            if (item.StartMonth != 0 && item.EndMonth != 0 && item.StartYear != 0 && item.EndYear != 0)
            {
                int starttime = item.StartYear * 12 + item.StartMonth;
                int endtime = item.EndYear * 12 + item.EndMonth;
                cmd += " and [Year] * 12 + [Month] >= " + starttime + " and [Year] * 12 + [Month] <= " + endtime;
            }
            if (item.Year != 0)
                cmd += " and Year = " + item.Year + " ";
            if (item.Month != 0)
                cmd += " and Month = " + item.Month + " ";
            if (item.SubID != 0)
                cmd += " and SubID = " + item.SubID + " ";
            if (item.Title != null && item.Title != string.Empty)
                cmd += " and Title = '" + item.Title + "' ";
            cmd += BudgetDetailOrderBy + ";";

            List<BudgetDetailInfo> list = new List<BudgetDetailInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        list.Add(GetBudgetDetailData(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("搜索本公司月度预算详细明细失败", e);
            }
            return list;
        }

        private IList<BudgetDetailInfo> Search(BudgetDetailInfo item, SqlTransaction trans)
        {
            string cmd = " select " + BudgetDetailReturnFields + " from " + BudgetDetailViewTableName + BudgetPerMonthTotalWhere;
            if (item.TotalID != 0)
                cmd += " and TotalID = " + item.TotalID + " ";
            if (item.SubID != 0)
                cmd += " and SubID = " + item.SubID + " ";
            if (item.Year != 0)
                cmd += " and Year = " + item.Year + " ";
            if (item.Month != 0)
                cmd += " and Month = " + item.Month + " ";
            if (item.SubID != 0)
                cmd += " and SubID = " + item.SubID + " ";
            if (item.Title != null && item.Title != string.Empty)
                cmd += " and Title = '" + item.Title + "' ";
            cmd += BudgetDetailOrderBy + ";";

            List<BudgetDetailInfo> list = new List<BudgetDetailInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(trans, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        list.Add(GetBudgetDetailData(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("搜索本公司月度预算详细明细失败", e);
            }
            return list;
        }

        private BudgetDetailInfo GetBudgetDetailData(IDataReader dr)
        {

            BudgetDetailInfo item = new BudgetDetailInfo();
            if (!Convert.IsDBNull(dr["DetailID"]))
                item.DetailID = Convert.ToInt64(dr["DetailID"]);
            if (!Convert.IsDBNull(dr["BudgetPermonthID"]))
                item.BudgetPermonthID = Convert.ToInt64(dr["BudgetPermonthID"]);
            if (!Convert.IsDBNull(dr["ExpenditureName"]))
                item.ExpenditureName = Convert.ToString(dr["ExpenditureName"]);
            if (!Convert.IsDBNull(dr["Expenditure"]))
                item.Expenditure = Convert.ToDecimal(dr["Expenditure"]);
            if (!Convert.IsDBNull(dr["Review"]))
                item.Review = Convert.ToString(dr["Review"]);
            if (!Convert.IsDBNull(dr["Manager"]))
                item.Manager = Convert.ToString(dr["Manager"]);
            if (!Convert.IsDBNull(dr["Remarks"]))
                item.Remarks = Convert.ToString(dr["Remarks"]);
            if (!Convert.IsDBNull(dr["RecordDate"]))
                item.RecordDate = Convert.ToDateTime(dr["RecordDate"]);
            if (!Convert.IsDBNull(dr["System"]))
                item.System = Convert.ToString(dr["System"]);
            if (!Convert.IsDBNull(dr["ExpenditureDetail"]))
                item.ExpenditureDetail = Convert.ToString(dr["ExpenditureDetail"]);
            if (!Convert.IsDBNull(dr["BudgetApprove"]))
                item.BudgetApprove = Convert.ToDecimal(dr["BudgetApprove"]);
            if (!Convert.IsDBNull(dr["Year"]))
                item.Year = Convert.ToInt32(dr["Year"]);
            if (!Convert.IsDBNull(dr["Month"]))
                item.Month = Convert.ToInt32(dr["Month"]);
            if (!Convert.IsDBNull(dr["SubID"]))
                item.SubID = Convert.ToInt64(dr["SubID"]);
            if (!Convert.IsDBNull(dr["Approvaler"]))
                item.Approvaler = Convert.ToString(dr["Approvaler"]);
            if (!Convert.IsDBNull(dr["Attachment"]))
                item.Attachment = Convert.ToString(dr["Attachment"]);
            if (!Convert.IsDBNull(dr["TotalID"]))
                item.TotalID = Convert.ToInt64(dr["TotalID"]);
            if (!Convert.IsDBNull(dr["SubName"]))
                item.SubName = Convert.ToString(dr["SubName"]);
            if (!Convert.IsDBNull(dr["RealExpenditure"]))
                item.RealExpenditure = Convert.ToDecimal(dr["RealExpenditure"]);
            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);
            if (!Convert.IsDBNull(dr["CompanyName"]))
                item.CompanyName = Convert.ToString(dr["CompanyName"]);
            if (!Convert.IsDBNull(dr["Supplier"]))
                item.Supplier = Convert.ToString(dr["Supplier"]);
            if (!Convert.IsDBNull(dr["PayDate"]))
                item.PayDate = Convert.ToDateTime(dr["PayDate"]);
            return item;
        }
        private BudgetDetailInfo GetBudgetDetailData1(IDataReader dr)
        {
            BudgetDetailInfo item = new BudgetDetailInfo();
            if (!Convert.IsDBNull(dr["SubID"]))
                item.SubID = Convert.ToInt64(dr["SubID"]);
            if (!Convert.IsDBNull(dr["SubName"]))
                item.SubName = Convert.ToString(dr["SubName"]);
            if (!Convert.IsDBNull(dr["RealExpenditure"]))
                item.RealExpenditure = Convert.ToDecimal(dr["RealExpenditure"]);

            return item;
        }
        private BudgetDetailInfo GetBudgetDetailData2(IDataReader dr)
        {
            BudgetDetailInfo item = new BudgetDetailInfo();
            if (!Convert.IsDBNull(dr["SubID"]))
                item.SubID = Convert.ToInt64(dr["SubID"]);
            if (!Convert.IsDBNull(dr["SubName"]))
                item.SubName = Convert.ToString(dr["SubName"]);
            if (!Convert.IsDBNull(dr["RealExpenditure"]))
                item.RealExpenditure = Convert.ToDecimal(dr["RealExpenditure"]);
            if (!Convert.IsDBNull(dr["Manager"]))
                item.Manager = Convert.ToString(dr["Manager"]);
            if (!Convert.IsDBNull(dr["ExpenditureName"]))
                item.ExpenditureName = Convert.ToString(dr["ExpenditureName"]);

            return item;
        }
        private BudgetDetailInfo GetBudgetDetailData3(IDataReader dr)
        {
            BudgetDetailInfo item = new BudgetDetailInfo();
            if (!Convert.IsDBNull(dr["SubID"]))
                item.SubID = Convert.ToInt64(dr["SubID"]);
            if (!Convert.IsDBNull(dr["SubName"]))
                item.SubName = Convert.ToString(dr["SubName"]);
            //if (!Convert.IsDBNull(dr["RealExpenditure"]))
            //    item.RealExpenditure = Convert.ToDecimal(dr["RealExpenditure"]);
            //if (!Convert.IsDBNull(dr["Manager"]))
            //    item.Manager = Convert.ToString(dr["Manager"]);
            if (!Convert.IsDBNull(dr["ExpenditureName"]))
                item.ExpenditureName = Convert.ToString(dr["ExpenditureName"]);
            if (!Convert.IsDBNull(dr["Supplier"]))
                item.Supplier = Convert.ToString(dr["Supplier"]);

            return item;
        }
        private BudgetDetailInfo GetBudgetDetailData4(IDataReader dr)
        {
            BudgetDetailInfo item = new BudgetDetailInfo();
            if (!Convert.IsDBNull(dr["SubID"]))
                item.SubID = Convert.ToInt64(dr["SubID"]);
            if (!Convert.IsDBNull(dr["SubName"]))
                item.SubName = Convert.ToString(dr["SubName"]);
            if (!Convert.IsDBNull(dr["RealExpenditure"]))
                item.RealExpenditure = Convert.ToDecimal(dr["RealExpenditure"]);
            //if (!Convert.IsDBNull(dr["Manager"]))
            //    item.Manager = Convert.ToString(dr["Manager"]);
            if (!Convert.IsDBNull(dr["ExpenditureName"]))
                item.ExpenditureName = Convert.ToString(dr["ExpenditureName"]);
            //if (!Convert.IsDBNull(dr["Supplier"]))
            //    item.Supplier = Convert.ToString(dr["Supplier"]);
            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);
            if (!Convert.IsDBNull(dr["CompanyName"]))
                item.CompanyName = Convert.ToString(dr["CompanyName"]);


            return item;
        }
        private BudgetDetailInfo GetBudgetDetailData5(IDataReader dr)
        {
            BudgetDetailInfo item = new BudgetDetailInfo();
            if (!Convert.IsDBNull(dr["SubID"]))
                item.SubID = Convert.ToInt64(dr["SubID"]);
            if (!Convert.IsDBNull(dr["SubName"]))
                item.SubName = Convert.ToString(dr["SubName"]);
            if (!Convert.IsDBNull(dr["RealExpenditure"]))
                item.RealExpenditure = Convert.ToDecimal(dr["RealExpenditure"]);
            //if (!Convert.IsDBNull(dr["Manager"]))
            //    item.Manager = Convert.ToString(dr["Manager"]);
            //if (!Convert.IsDBNull(dr["ExpenditureName"]))
            //    item.ExpenditureName = Convert.ToString(dr["ExpenditureName"]);
            if (!Convert.IsDBNull(dr["Supplier"]))
                item.Supplier = Convert.ToString(dr["Supplier"]);
            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);
            if (!Convert.IsDBNull(dr["CompanyName"]))
                item.CompanyName = Convert.ToString(dr["CompanyName"]);


            return item;
        }

        public IList Statistics1(BudgetDetailInfo item)
        {
            SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString);
            SqlCommand sqlcmd = null;
            IList sublist = new List<BudgetDetailInfo>();
            IList expenditurenamelist = new List<BudgetDetailInfo>();
            IList Supplierlist = new List<BudgetDetailInfo>();
            IList Companylist = new List<BudgetDetailInfo>();
            IList Totallist = new List<BudgetDetailInfo>();
            try
            {
                conn.Open();
                string cmd1 = "SELECT SubID, SubName, SUM(RealExpenditure) AS RealExpenditure FROM FM2E_BudgetDetailView where 1=1 ";
                if (item.Title != null && item.Title != string.Empty)
                    cmd1 += " and Title = '" + item.Title + "' ";
                if (item.Supplier != null && item.Supplier != string.Empty)
                    cmd1 += " and Supplier = '" + item.Supplier.Trim() + "' ";
                if (item.StartMonth != 0 && item.EndMonth != 0 && item.StartYear != 0 && item.EndYear != 0)
                {
                    int starttime = item.StartYear * 12 + item.StartMonth;
                    int endtime = item.EndYear * 12 + item.EndMonth;
                    cmd1 += " and [Year] * 12 + [Month] >= " + starttime + " and [Year] * 12 + [Month] <= " + endtime;
                }
                cmd1 += " group by SubID, SubName ";
                sqlcmd = new SqlCommand(cmd1, conn);

                using (SqlDataReader rd = sqlcmd.ExecuteReader())
                {
                    while (rd.Read())
                        sublist.Add(GetBudgetDetailData1(rd));
                }

                string cmd2 = "SELECT SubID, SubName, ExpenditureName, Manager, SUM(RealExpenditure) AS RealExpenditure FROM FM2E_BudgetDetailView where 1=1 ";
                if (item.Title != null && item.Title != string.Empty)
                    cmd2 += " and Title = '" + item.Title + "' ";
                if (item.Supplier != null && item.Supplier != string.Empty)
                    cmd2 += " and Supplier = '" + item.Supplier.Trim() + "' ";
                if (item.StartMonth != 0 && item.EndMonth != 0 && item.StartYear != 0 && item.EndYear != 0)
                {
                    int starttime = item.StartYear * 12 + item.StartMonth;
                    int endtime = item.EndYear * 12 + item.EndMonth;
                    cmd2 += " and [Year] * 12 + [Month] >= " + starttime + " and [Year] * 12 + [Month] <= " + endtime;
                }
                cmd2 += " group by SubID, SubName, ExpenditureName, Manager ";
                sqlcmd = new SqlCommand(cmd2, conn);

                using (SqlDataReader rd = sqlcmd.ExecuteReader())
                {
                    while (rd.Read())
                        expenditurenamelist.Add(GetBudgetDetailData2(rd));
                }


                string cmd3 = "SELECT DISTINCT SubID, SubName, ExpenditureName, Supplier FROM FM2E_BudgetDetailView where 1=1 ";
                if (item.Title != null && item.Title != string.Empty)
                    cmd3 += " and Title = '" + item.Title + "' ";
                if (item.Supplier != null && item.Supplier != string.Empty)
                    cmd3 += " and Supplier = '" + item.Supplier.Trim() + "' ";
                if (item.StartMonth != 0 && item.EndMonth != 0 && item.StartYear != 0 && item.EndYear != 0)
                {
                    int starttime = item.StartYear * 12 + item.StartMonth;
                    int endtime = item.EndYear * 12 + item.EndMonth;
                    cmd3 += " and [Year] * 12 + [Month] >= " + starttime + " and [Year] * 12 + [Month] <= " + endtime;
                }
                sqlcmd = new SqlCommand(cmd3, conn);

                using (SqlDataReader rd = sqlcmd.ExecuteReader())
                {
                    while (rd.Read())
                        Supplierlist.Add(GetBudgetDetailData3(rd));
                }

                string cmd4 = "SELECT SubID, SubName, ExpenditureName,CompanyID, CompanyName, SUM(RealExpenditure) AS RealExpenditure FROM FM2E_BudgetDetailView where 1=1 ";
                if (item.Title != null && item.Title != string.Empty)
                    cmd4 += " and Title = '" + item.Title + "' ";
                if (item.StartMonth != 0 && item.EndMonth != 0 && item.StartYear != 0 && item.EndYear != 0)
                {
                    int starttime = item.StartYear * 12 + item.StartMonth;
                    int endtime = item.EndYear * 12 + item.EndMonth;
                    cmd4 += " and [Year] * 12 + [Month] >= " + starttime + " and [Year] * 12 + [Month] <= " + endtime;
                }
                cmd4 += " GROUP BY SubID, SubName, ExpenditureName, CompanyID, CompanyName order by CompanyID ";
                sqlcmd = new SqlCommand(cmd4, conn);
                using (SqlDataReader rd = sqlcmd.ExecuteReader())
                {
                    while (rd.Read())
                        Companylist.Add(GetBudgetDetailData4(rd));
                }

                //string cmd5 = "select SubID, SubName, CompanyID, CompanyName, SUM(RealExpenditure) AS RealExpenditure, Supplier FROM FM2E_BudgetDetailView where 1=1 ";
                //if (item.Title != null && item.Title != string.Empty)
                //    cmd5 += " and Title = '" + item.Title + "' ";
                //if (item.StartMonth != 0 && item.EndMonth != 0 && item.StartYear != 0 && item.EndYear != 0)
                //{
                //    int starttime = item.StartYear * 12 + item.StartMonth;
                //    int endtime = item.EndYear * 12 + item.EndMonth;
                //    cmd5 += " and [Year] * 12 + [Month] >= " + starttime + " and [Year] * 12 + [Month] <= " + endtime;
                //}
                //cmd5 += " group by SubID, SubName, CompanyID, CompanyName, Supplier order by CompanyID ";
                //sqlcmd = new SqlCommand(cmd5, conn);

                //using (SqlDataReader rd = sqlcmd.ExecuteReader())
                //{
                //    while (rd.Read())
                //        Totallist.Add(GetBudgetDetailData5(rd));
                //}




                foreach (BudgetDetailInfo item1 in expenditurenamelist)
                {
                    int i = 0;

                    foreach (BudgetDetailInfo item2 in Companylist)
                    {
                        if (item1.SubID == item2.SubID && item1.SubName == item2.SubName && item1.ExpenditureName == item2.ExpenditureName)
                        {
                            if (i == 0)
                                item1.CompanyList = new List<BudgetDetailInfo>();
                            if (item.Supplier != string.Empty && item2.Supplier != item.Supplier)
                                item2.RealExpenditure = 0;
                            item1.CompanyList.Add(item2);
                            i++;
                        }
                    }
                }

                foreach (BudgetDetailInfo item1 in expenditurenamelist)
                {
                    int i = 0;
                    foreach (BudgetDetailInfo item2 in Supplierlist)
                    {
                        if (item1.SubID == item2.SubID && item1.SubName == item2.SubName && item1.ExpenditureName == item2.ExpenditureName)
                        {
                            if (i == 0)
                                item1.Supplier = "";
                            item1.Supplier += item2.Supplier + " ";
                            i++;
                        }
                    }
                }

                foreach (BudgetDetailInfo item1 in sublist)
                {
                    int i = 0;
                    foreach (BudgetDetailInfo item2 in expenditurenamelist)
                    {
                        if (item1.SubID == item2.SubID && item1.SubName == item2.SubName)
                        {
                            if (i == 0)
                                item1.BudgetDetailList = new List<BudgetDetailInfo>();

                            item1.BudgetDetailList.Add(item2);
                            i++;
                        }
                    }
                }

                //foreach (BudgetDetailInfo item1 in sublist)
                //{
                //    int i = 0;
                //    foreach (BudgetDetailInfo item2 in Totallist)
                //    {
                //        if (item1.SubID == item2.SubID && item1.SubName == item2.SubName)
                //        {
                //            if (i == 0)
                //                item1.Totallist = new List<BudgetDetailInfo>();
                //            if (item.Supplier != string.Empty && item2.Supplier != item.Supplier)
                //                item2.RealExpenditure = 0;
                //            item1.Totallist.Add(item2);
                //            i++;
                //        }
                //    }
                //}

                foreach (BudgetDetailInfo item1 in sublist)
                {
                    Company companydal = new Company();
                    IList totalist = (List<CompanyInfo>)companydal.GetAllCompany();
                    foreach (BudgetDetailInfo item2 in item1.BudgetDetailList)
                    {
                        foreach (BudgetDetailInfo item3 in item2.CompanyList)
                        {
                            foreach (CompanyInfo companyitem in totalist)
                            {
                                if (companyitem.CompanyName.Substring(0, 2).Equals(item3.CompanyName.Substring(0, 2)))
                                {
                                    companyitem.CompanyExpenditure += item3.RealExpenditure;
                                    break;
                                }
                            }
                        }
                    }
                    item1.Totallist = totalist;
                }


            }
            catch (Exception e)
            {
                throw new DALException("统计详细开支时出错", e);
            }
            finally
            {
                sqlcmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return sublist;

        }

        public IList Summary1(BudgetDetailInfo item)
        {
            SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString);
            SqlCommand sqlcmd = null;
            IList list = new List<BudgetDetailInfo>();
            try
            {
                conn.Open();
                string cmd = " select SubID, SubName, SUM(RealExpenditure) AS RealExpenditure from FM2E_BudgetDetailView where Title = '" + item.Title + "' and SubID = " + item.SubID + " and CompanyID = '" + item.CompanyID + "' and CompanyName = '" + item.CompanyName + "' and SubName = '" + item.SubName + "' and [Year] = " + item.Year + " and Month <= " + item.Month + " group by SubID, SubName ";
                sqlcmd = new SqlCommand(cmd, conn);
                using (SqlDataReader rd = sqlcmd.ExecuteReader())
                {
                    while (rd.Read())
                        list.Add(GetBudgetDetailData1(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("汇总明细开支时出错", e);
            }
            finally
            {
                sqlcmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return list;


        }

        public void SaveCurrentSubject(long Year,ref Hashtable ht)
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
                strSql.Append("insert into FM2E_SubjectPerYear(");
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
                delstr.Append("delete FM2E_SubjectPerYear where Year=@Year");
                SqlParameter delparameters = new SqlParameter("@Year", SqlDbType.BigInt, 8);
                delparameters.Value = Year;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, delstr.ToString(), delparameters);

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FM2E_SubjectPerYear(");
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
