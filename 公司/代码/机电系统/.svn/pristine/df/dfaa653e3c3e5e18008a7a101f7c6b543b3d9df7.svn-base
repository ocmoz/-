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
    public class QuarterlyBudget : IQuarterlyBudget
    {
        private const string QuarterlyBudgetTotalTableName = " FM2E_QuarterlyBudgetView ";

        private const string QuarterlyBudgetTableName = " FM2E_QuarterlyBudget ";

        private const string QuarterlyBudgetTotalReturnFields = " * ";

        private const string QuarterlyBudgetReturnFields = " * ";

        private const string QuarterlyBudgetTotalOrderBy = " order by TotalID desc ";

        private const string QuarterlyBudgetOrderBy = " order by BudgetPermonthID ";

        private const string QuarterlyBudgetTotalWhere = " where 1=1 ";

        private const string SELECT_QUARTERLYBUDGETTOTAL = " select " + QuarterlyBudgetTotalReturnFields + " from " + QuarterlyBudgetTotalTableName + QuarterlyBudgetTotalWhere + " and TotalID = '{0}' " + QuarterlyBudgetTotalOrderBy;

        /*------------------------------------------*/
        private const string QuarterlyBudgetDetailTableName = " FM2E_QuarterlyBudgetDetail ";

        private const string QuarterlyBudgetDetailViewTableName = " FM2E_QuarterlyBudgetDetailView ";

        private const string QuarterlyBudgetDetailReturnFields = " * ";

        private const string QuarterlyBudgetDetailOrderBy = " order by DetailID desc ";

        public QueryParam GenerateSearchTerm(QuarterlyBudgetTotalInfo item)
        {

            QueryParam qp = new QueryParam();
            qp.TableName = QuarterlyBudgetTotalTableName;
            qp.ReturnFields = QuarterlyBudgetTotalReturnFields;
            qp.OrderBy = QuarterlyBudgetTotalOrderBy;
            qp.Where = QuarterlyBudgetTotalWhere;
            if (item.Status != 0)
                qp.Where += " and Status = " + item.Status + " ";
            if (item.CompanyID != null && item.CompanyID != string.Empty)
                qp.Where += " and CompanyID = '" + item.CompanyID + "' ";

            //if (item.WorkFlowStatus != null && item.WorkFlowStatus.Count > 0)
            //{
            //    for (int i = 0; i < item.WorkFlowStatus.Count; i++)
            //    {
            //        if (i == 0)
            //        {
            //            qp.Where += " and ( ";
            //            qp.Where += " " + " CurrentStateName='" + item.WorkFlowStatus[i] + "' ";
            //        }
            //        else
            //        {
            //            qp.Where += " or " + " CurrentStateName='" + item.WorkFlowStatus[i] + "' ";
            //        }
            //        if (i == item.WorkFlowStatus.Count - 1)
            //        {
            //            qp.Where += " ) ";
            //        }
            //    }
            //}
            if (!string.IsNullOrEmpty(item.WorkFlowUserName))
                qp.Where += " and NextUserName = '" + item.WorkFlowUserName + "' ";

            return qp;
        }

        public IList GetQuarterlyBudgetTotalList(QueryParam term, out int recordCount, string companyid)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = QuarterlyBudgetTotalTableName;
                    term.ReturnFields = QuarterlyBudgetTotalReturnFields;
                    term.OrderBy = QuarterlyBudgetTotalOrderBy;
                    term.Where = QuarterlyBudgetTotalWhere;
                }
                if (companyid != null && companyid != string.Empty)
                    term.Where += " and CompanyID = '" + companyid + "' ";
                return SQLHelper.GetObjectList(this.GetQuarterlyBudgetTotalData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException(" 获取季度预算分页失败", e);
            }
        }

        private QuarterlyBudgetTotalInfo GetQuarterlyBudgetTotalData(IDataReader dr)
        {
            QuarterlyBudgetTotalInfo item = new QuarterlyBudgetTotalInfo();
            if (!Convert.IsDBNull(dr["TotalID"]))
                item.TotalID = Convert.ToInt64(dr["TotalID"]);
            if (!Convert.IsDBNull(dr["Year"]))
                item.Year = Convert.ToInt32(dr["Year"]);
            if (!Convert.IsDBNull(dr["Quarter"]))
                item.Quarter = Convert.ToInt32(dr["Quarter"]);
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

        public QuarterlyBudgetTotalInfo GetQuarterlyBudgetTotal(long id)
        {
            QuarterlyBudgetTotalInfo item = null;
            string cmd = string.Format(SELECT_QUARTERLYBUDGETTOTAL, id);

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        item = GetQuarterlyBudgetTotalData(rd);
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取季度预算基本信息失败", e);
            }

            return item;
        }

        public long InsertQuarterlyBudgetTotal(QuarterlyBudgetTotalInfo item)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            long id = 0;
            try
            {
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                id = InsertQuarterlyBudgetTotalBase(trans, item);
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FM2E_QuarterlyBudget(");
                strSql.Append("Remarks,Manager,MakeTime,Expenditure,Allocation,Deviation,ReasonForDeviation,EvaluationForDeviation,Review,TotalID,BudgetYearDetailID,SubID,BudgetApply,CompanyID,Quarter,TotalExpenditure,BudgetPermonth,SurplusExpenditure,NonPayment,Total)");
                strSql.Append(" values (");
                strSql.Append("@Remarks,@Manager,@MakeTime,@Expenditure,@Allocation,@Deviation,@ReasonForDeviation,@EvaluationForDeviation,@Review,@TotalID,@BudgetYearDetailID,@SubID,@BudgetApply,@CompanyID,@Quarter,@TotalExpenditure,@BudgetPermonth,@SurplusExpenditure,@NonPayment,@Total)");
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
					new SqlParameter("@Quarter", SqlDbType.Int,4),
					new SqlParameter("@TotalExpenditure", SqlDbType.Decimal,9),
					new SqlParameter("@BudgetPermonth", SqlDbType.Decimal,9),
					new SqlParameter("@SurplusExpenditure", SqlDbType.Decimal,9),
					new SqlParameter("@NonPayment", SqlDbType.Decimal,9),
					new SqlParameter("@Total", SqlDbType.Decimal,9)};
                foreach (QuarterlyBudgetInfo model in item.DetailList)
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
                    parameters[14].Value = model.Quarter;
                    parameters[15].Value = model.TotalExpenditure;
                    parameters[16].Value = model.BudgetPermonth;
                    parameters[17].Value = model.SurplusExpenditure;
                    parameters[18].Value = model.NonPayment;
                    parameters[19].Value = model.Total;
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                }
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("insert into FM2E_QuarterlyBudgetDetail(");
                strSql2.Append("ExpenditureDetail,BudgetApprove,Year,Quarter,SubID,Approvaler,Attachment,TotalID,SubName,BudgetPermonthID,ExpenditureName,Expenditure,Review,Manager,Remarks,RecordDate,System,CompanyID,CompanyName)");
                strSql2.Append(" values (");
                strSql2.Append("@ExpenditureDetail,@BudgetApprove,@Year,@Quarter,@SubID,@Approvaler,@Attachment,@TotalID,@SubName,@BudgetPermonthID,@ExpenditureName,@Expenditure,@Review,@Manager,@Remarks,@RecordDate,@System,@CompanyID,@CompanyName)");
                strSql2.Append(";select @@IDENTITY");
                SqlParameter[] parameters2 = {
					new SqlParameter("@ExpenditureDetail", SqlDbType.VarChar,80),
					new SqlParameter("@BudgetApprove", SqlDbType.Decimal,9),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@Quarter", SqlDbType.Int,4),
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
                foreach (QuarterlyBudgetDetailInfo model in item.BudgetDetailList)
                {
                    parameters2[0].Value = model.ExpenditureDetail;
                    parameters2[1].Value = model.BudgetApprove;
                    parameters2[2].Value = model.Year;
                    parameters2[3].Value = model.Quarter;
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
                throw new DALException("添加季度预算信息失败", e);
            }
            return id;
        }

        private long InsertQuarterlyBudgetTotalBase(SqlTransaction trans, QuarterlyBudgetTotalInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_QuarterlyBudgetTotal(");
            strSql.Append("TotalExpenditure,BudgetPermonth,SurplusExpenditure,NonPayment,Total,MakeTime,Expenditure,Allocation,Deviation,BudgetApply,Year,Status,BudgetYearID,Quarter,CompanyID,ViceEngineerReview,ViceManagerReview,ManagerReview,FinanceReview,Result,Approvaler,Title)");
            strSql.Append(" values (");
            strSql.Append("@TotalExpenditure,@BudgetPermonth,@SurplusExpenditure,@NonPayment,@Total,@MakeTime,@Expenditure,@Allocation,@Deviation,@BudgetApply,@Year,@Status,@BudgetYearID,@Quarter,@CompanyID,@ViceEngineerReview,@ViceManagerReview,@ManagerReview,@FinanceReview,@Result,@Approvaler,@Title)");
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
					new SqlParameter("@Quarter", SqlDbType.Int,4),
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
            parameters[13].Value = model.Quarter;
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

        public void UpdateQuarterlyBudgetTotal(QuarterlyBudgetTotalInfo item)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            long id = item.TotalID;
            try
            {
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();
                UpdateQuarterlyBudgetTotalBase(trans, item);
                if (item.DetailList != null && item.DetailList.Count != 0)
                {
                    QuarterlyBudgetInfo budgetpermonthinfo = new QuarterlyBudgetInfo();
                    budgetpermonthinfo.TotalID = id;
                    IList<QuarterlyBudgetInfo> list = this.Search(budgetpermonthinfo);

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update FM2E_QuarterlyBudget set ");
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
                    strSql.Append("Quarter=@Quarter,");
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
					new SqlParameter("@Quarter", SqlDbType.Int,4),
					new SqlParameter("@TotalExpenditure", SqlDbType.Decimal,9),
					new SqlParameter("@BudgetPermonth", SqlDbType.Decimal,9),
					new SqlParameter("@SurplusExpenditure", SqlDbType.Decimal,9),
					new SqlParameter("@NonPayment", SqlDbType.Decimal,9),
					new SqlParameter("@Total", SqlDbType.Decimal,9)};
                    for (int i = 0; i != list.Count; i++)
                    {
                        parameters[0].Value = ((QuarterlyBudgetInfo)list[i]).BudgetPermonthID;
                        parameters[1].Value = ((QuarterlyBudgetInfo)list[i]).Remarks;
                        parameters[2].Value = ((QuarterlyBudgetInfo)list[i]).Manager;
                        parameters[3].Value = ((QuarterlyBudgetInfo)item.DetailList[i]).MakeTime;
                        parameters[4].Value = ((QuarterlyBudgetInfo)list[i]).Expenditure;
                        parameters[5].Value = ((QuarterlyBudgetInfo)list[i]).Allocation;
                        parameters[6].Value = ((QuarterlyBudgetInfo)list[i]).Deviation;
                        parameters[7].Value = ((QuarterlyBudgetInfo)list[i]).ReasonForDeviation;
                        parameters[8].Value = ((QuarterlyBudgetInfo)list[i]).EvaluationForDeviation;
                        parameters[9].Value = ((QuarterlyBudgetInfo)list[i]).Review;
                        parameters[10].Value = ((QuarterlyBudgetInfo)list[i]).TotalID;
                        parameters[11].Value = ((QuarterlyBudgetInfo)list[i]).BudgetYearDetailID;
                        parameters[12].Value = ((QuarterlyBudgetInfo)list[i]).SubID;
                        parameters[13].Value = ((QuarterlyBudgetInfo)item.DetailList[i]).BudgetApply;
                        parameters[14].Value = ((QuarterlyBudgetInfo)list[i]).CompanyID;
                        parameters[15].Value = ((QuarterlyBudgetInfo)list[i]).Quarter;
                        parameters[16].Value = ((QuarterlyBudgetInfo)item.DetailList[i]).TotalExpenditure;
                        parameters[17].Value = ((QuarterlyBudgetInfo)item.DetailList[i]).BudgetPermonth;
                        parameters[18].Value = ((QuarterlyBudgetInfo)item.DetailList[i]).SurplusExpenditure;
                        parameters[19].Value = ((QuarterlyBudgetInfo)item.DetailList[i]).NonPayment;
                        parameters[20].Value = ((QuarterlyBudgetInfo)item.DetailList[i]).Total;

                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                    }
                }
                if (item.UpdateBudgetDetail)
                {
                    QuarterlyBudgetDetailInfo budgetdetailinfo = new QuarterlyBudgetDetailInfo();
                    budgetdetailinfo.TotalID = id;
                    foreach (QuarterlyBudgetDetailInfo budgetdetailmodel in (List<QuarterlyBudgetDetailInfo>)this.Search(budgetdetailinfo, trans))
                    {
                        DelBudgetDetail(budgetdetailmodel.DetailID, trans);
                    }
                    if (item.BudgetDetailList != null && item.BudgetDetailList.Count != 0)
                    {

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("insert into FM2E_QuarterlyBudgetDetail(");
                        strSql2.Append("ExpenditureDetail,BudgetApprove,Year,Quarter,SubID,Approvaler,Attachment,TotalID,SubName,BudgetPermonthID,ExpenditureName,Expenditure,Review,Manager,Remarks,RecordDate,System,RealExpenditure,CompanyID,CompanyName,Supplier,PayDate)");
                        strSql2.Append(" values (");
                        strSql2.Append("@ExpenditureDetail,@BudgetApprove,@Year,@Quarter,@SubID,@Approvaler,@Attachment,@TotalID,@SubName,@BudgetPermonthID,@ExpenditureName,@Expenditure,@Review,@Manager,@Remarks,@RecordDate,@System,@RealExpenditure,@CompanyID,@CompanyName,@Supplier,@PayDate)");
                        strSql2.Append(";select @@IDENTITY");
                        SqlParameter[] parameters2 = {
					    new SqlParameter("@ExpenditureDetail", SqlDbType.VarChar,80),
					    new SqlParameter("@BudgetApprove", SqlDbType.Decimal,9),
					    new SqlParameter("@Year", SqlDbType.Int,4),
					    new SqlParameter("@Quarter", SqlDbType.Int,4),
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
                        foreach (QuarterlyBudgetDetailInfo model in item.BudgetDetailList)
                        {
                            parameters2[0].Value = model.ExpenditureDetail;
                            parameters2[1].Value = model.BudgetApprove;
                            parameters2[2].Value = model.Year;
                            parameters2[3].Value = model.Quarter;
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
                throw new DALException("修改季度预算失败", e);
            }


        }

        private IList<QuarterlyBudgetDetailInfo> Search(QuarterlyBudgetDetailInfo item, SqlTransaction trans)
        {
            string cmd = " select " + QuarterlyBudgetDetailReturnFields + " from " + QuarterlyBudgetDetailViewTableName + QuarterlyBudgetTotalWhere;
            if (item.TotalID != 0)
                cmd += " and TotalID = " + item.TotalID + " ";
            if (item.SubID != 0)
                cmd += " and SubID = " + item.SubID + " ";
            if (item.Year != 0)
                cmd += " and Year = " + item.Year + " ";
            if (item.Quarter != 0)
                cmd += " and Quarter = " + item.Quarter + " ";
            if (item.SubID != 0)
                cmd += " and SubID = " + item.SubID + " ";
            if (item.Title != null && item.Title != string.Empty)
                cmd += " and Title = '" + item.Title + "' ";
            cmd += QuarterlyBudgetDetailOrderBy + ";";

            List<QuarterlyBudgetDetailInfo> list = new List<QuarterlyBudgetDetailInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(trans, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        list.Add(GetQuarterlyBudgetDetailData(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("搜索本公司季度预算详细明细失败", e);
            }
            return list;
        }

        private void DelBudgetDetail(long id, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_QuarterlyBudgetDetail ");
            strSql.Append(" where DetailID=@DetailID ");
            SqlParameter[] parameters = {
					new SqlParameter("@DetailID", SqlDbType.BigInt)};
            parameters[0].Value = id;
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

        }

        private void UpdateQuarterlyBudgetTotalBase(SqlTransaction trans, QuarterlyBudgetTotalInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_QuarterlyBudgetTotal set ");
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
            strSql.Append("Quarter=@Quarter,");
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
					new SqlParameter("@Quarter", SqlDbType.Int,4),
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
            parameters[13].Value = model.Quarter;
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


        public void DelQuarterlyBudgetTotal(long id)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();
                QuarterlyBudget bll = new QuarterlyBudget();
                QuarterlyBudgetInfo budgetpermonthinfo = new QuarterlyBudgetInfo();
                budgetpermonthinfo.TotalID = id;
                IList<QuarterlyBudgetInfo> list = bll.Search(budgetpermonthinfo);
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_QuarterlyBudget ");
                strSql.Append(" where BudgetPermonthID=@BudgetPermonthID ");
                SqlParameter[] parameters = {
					new SqlParameter("@BudgetPermonthID", SqlDbType.BigInt)};

                foreach (QuarterlyBudgetInfo model in list)
                {
                    parameters[0].Value = model.BudgetPermonthID;
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                }

                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("delete FM2E_QuarterlyBudgetTotal ");
                strSql2.Append(" where TotalID=@TotalID ");
                SqlParameter[] parameters2 = {
					new SqlParameter("@TotalID", SqlDbType.BigInt)};
                parameters2[0].Value = id;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);

                StringBuilder strSql3 = new StringBuilder();
                strSql3.Append("delete FM2E_QuarterlyBudgetDetail ");
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
                throw new DALException("删除季度预算失败", e);
            }
        }

        public IList<QuarterlyBudgetInfo> Search(QuarterlyBudgetInfo item)
        {
            string cmd = " select " + QuarterlyBudgetReturnFields + " from " + QuarterlyBudgetTableName + QuarterlyBudgetTotalWhere;
            if (item.TotalID != 0)
                cmd += " and TotalID = " + item.TotalID + " ";
            cmd += QuarterlyBudgetOrderBy;

            List<QuarterlyBudgetInfo> list = new List<QuarterlyBudgetInfo>();

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        list.Add(GetQuarterlyBudgetData(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("搜索本公司季度预算详细明细失败", e);
            }
            return list;
        }

        private QuarterlyBudgetInfo GetQuarterlyBudgetData(IDataReader dr)
        {
            QuarterlyBudgetInfo item = new QuarterlyBudgetInfo();
            if (!Convert.IsDBNull(dr["BudgetPermonthID"]))
                item.BudgetPermonthID = Convert.ToInt64(dr["BudgetPermonthID"]);
            if (!Convert.IsDBNull(dr["BudgetYearDetailID"]))
                item.BudgetYearDetailID = Convert.ToInt64(dr["BudgetYearDetailID"]);
            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);
            if (!Convert.IsDBNull(dr["Quarter"]))
                item.Quarter = Convert.ToInt32(dr["Quarter"]);
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

        public IList<QuarterlyBudgetTotalInfo> Search(QuarterlyBudgetTotalInfo item)
        {
            string cmd = " select " + QuarterlyBudgetTotalReturnFields + " from " + QuarterlyBudgetTotalTableName + QuarterlyBudgetTotalWhere;
            if (item.BudgetYearID != 0)
                cmd += " and BudgetYearID = " + item.BudgetYearID + " ";
            if (item.Status != 0)
                cmd += " and Status = " + item.Status + " ";
            if (item.CompanyID != null && item.CompanyID != string.Empty)
                cmd += " and CompanyID = '" + item.CompanyID + "' ";
            if (item.Year != 0)
                cmd += " and Year = " + item.Year + " ";
            if (item.Quarter != 0)
                cmd += " and Quarter = " + item.Quarter + " ";
            cmd += QuarterlyBudgetTotalOrderBy;

            List<QuarterlyBudgetTotalInfo> list = new List<QuarterlyBudgetTotalInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        list.Add(GetQuarterlyBudgetTotalData(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("搜索本公司季度预算详细明细失败", e);
            }
            return list;
        }



        /*-------------------------------------------------------*/
        public IList<QuarterlyBudgetDetailInfo> Search(QuarterlyBudgetDetailInfo item)
        {
            string cmd = " select " + QuarterlyBudgetDetailReturnFields + " from " + QuarterlyBudgetDetailViewTableName + QuarterlyBudgetTotalWhere;
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
                cmd += " and [Year] * 12 + [Quarter] >= " + starttime + " and [Quarter] * 12 + [Month] <= " + endtime;
            }
            if (item.Year != 0)
                cmd += " and Year = " + item.Year + " ";
            if (item.Quarter != 0)
                cmd += " and Quarter = " + item.Quarter + " ";
            if (item.SubID != 0)
                cmd += " and SubID = " + item.SubID + " ";
            if (item.Title != null && item.Title != string.Empty)
                cmd += " and Title = '" + item.Title + "' ";
            cmd += QuarterlyBudgetDetailOrderBy + ";";

            List<QuarterlyBudgetDetailInfo> list = new List<QuarterlyBudgetDetailInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        list.Add(GetQuarterlyBudgetDetailData(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("搜索本公司季度预算详细明细失败", e);
            }
            return list;
        }

        private QuarterlyBudgetDetailInfo GetQuarterlyBudgetDetailData(IDataReader dr)
        {

            QuarterlyBudgetDetailInfo item = new QuarterlyBudgetDetailInfo();
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
            if (!Convert.IsDBNull(dr["Quarter"]))
                item.Quarter = Convert.ToInt32(dr["Quarter"]);
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

        public void DelQuarterlyBudgetDetail(long id)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_QuarterlyBudgetDetail ");
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

        public IList Statistics1(QuarterlyBudgetDetailInfo item)
        {
            SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString);
            SqlCommand sqlcmd = null;
            IList sublist = new List<QuarterlyBudgetDetailInfo>();
            IList expenditurenamelist = new List<QuarterlyBudgetDetailInfo>();
            IList Supplierlist = new List<QuarterlyBudgetDetailInfo>();
            IList Companylist = new List<QuarterlyBudgetDetailInfo>();
            IList Totallist = new List<QuarterlyBudgetDetailInfo>();
            try
            {
                conn.Open();
                string cmd1 = "SELECT SubID, SubName, SUM(Expenditure) AS RealExpenditure FROM FM2E_QuarterlyBudgetDetailView where 1=1 ";
                if (item.Title != null && item.Title != string.Empty)
                    cmd1 += " and Title = '" + item.Title + "' ";
                if (item.Supplier != null && item.Supplier != string.Empty)
                    cmd1 += " and Supplier = '" + item.Supplier.Trim() + "' ";
                if (item.StartMonth != 0 && item.EndMonth != 0 && item.StartYear != 0 && item.EndYear != 0)
                {
                    int starttime = item.StartYear * 4 + item.StartMonth;
                    int endtime = item.EndYear * 4 + item.EndMonth;
                    cmd1 += " and [Year] * 4 + [Quarter] >= " + starttime + " and [Year] * 4 + [Quarter] <= " + endtime;
                }
                cmd1 += " group by SubID, SubName ";
                sqlcmd = new SqlCommand(cmd1, conn);

                using (SqlDataReader rd = sqlcmd.ExecuteReader())
                {
                    while (rd.Read())
                        sublist.Add(GetQuarterlyBudgetDetailData1(rd));
                }

                string cmd2 = "SELECT SubID, SubName, ExpenditureName, Manager, SUM(Expenditure) AS RealExpenditure FROM FM2E_QuarterlyBudgetDetailView where 1=1 ";
                if (item.Title != null && item.Title != string.Empty)
                    cmd2 += " and Title = '" + item.Title + "' ";
                if (item.Supplier != null && item.Supplier != string.Empty)
                    cmd2 += " and Supplier = '" + item.Supplier.Trim() + "' ";
                if (item.StartMonth != 0 && item.EndMonth != 0 && item.StartYear != 0 && item.EndYear != 0)
                {
                    int starttime = item.StartYear * 4 + item.StartMonth;
                    int endtime = item.EndYear * 4 + item.EndMonth;
                    cmd2 += " and [Year] * 4 + [Quarter] >= " + starttime + " and [Year] * 4 + [Quarter] <= " + endtime;
                }
                cmd2 += " group by SubID, SubName, ExpenditureName, Manager ";
                sqlcmd = new SqlCommand(cmd2, conn);

                using (SqlDataReader rd = sqlcmd.ExecuteReader())
                {
                    while (rd.Read())
                        expenditurenamelist.Add(GetQuarterlyBudgetDetailData2(rd));
                }


                string cmd3 = "SELECT DISTINCT SubID, SubName, ExpenditureName, Supplier FROM FM2E_QuarterlyBudgetDetailView where 1=1 ";
                if (item.Title != null && item.Title != string.Empty)
                    cmd3 += " and Title = '" + item.Title + "' ";
                if (item.Supplier != null && item.Supplier != string.Empty)
                    cmd3 += " and Supplier = '" + item.Supplier.Trim() + "' ";
                if (item.StartMonth != 0 && item.EndMonth != 0 && item.StartYear != 0 && item.EndYear != 0)
                {
                    int starttime = item.StartYear * 4 + item.StartMonth;
                    int endtime = item.EndYear * 4 + item.EndMonth;
                    cmd3 += " and [Year] * 4 + [Quarter] >= " + starttime + " and [Year] * 4 + [Quarter] <= " + endtime;
                }
                sqlcmd = new SqlCommand(cmd3, conn);

                using (SqlDataReader rd = sqlcmd.ExecuteReader())
                {
                    while (rd.Read())
                        Supplierlist.Add(GetQuarterlyBudgetDetailData3(rd));
                }

                string cmd4 = "SELECT SubID, SubName, ExpenditureName,CompanyID, CompanyName, SUM(Expenditure) AS RealExpenditure FROM FM2E_QuarterlyBudgetDetailView where 1=1 ";
                if (item.Title != null && item.Title != string.Empty)
                    cmd4 += " and Title = '" + item.Title + "' ";
                if (item.StartMonth != 0 && item.EndMonth != 0 && item.StartYear != 0 && item.EndYear != 0)
                {
                    int starttime = item.StartYear * 4 + item.StartMonth;
                    int endtime = item.EndYear * 4 + item.EndMonth;
                    cmd4 += " and [Year] * 4 + [Quarter] >= " + starttime + " and [Year] * 4 + [Quarter] <= " + endtime;
                }
                cmd4 += " GROUP BY SubID, SubName, ExpenditureName, CompanyID, CompanyName order by CompanyID ";
                sqlcmd = new SqlCommand(cmd4, conn);
                using (SqlDataReader rd = sqlcmd.ExecuteReader())
                {
                    while (rd.Read())
                        Companylist.Add(GetQuarterlyBudgetDetailData4(rd));
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




                foreach (QuarterlyBudgetDetailInfo item1 in expenditurenamelist)
                {
                    int i = 0;

                    foreach (QuarterlyBudgetDetailInfo item2 in Companylist)
                    {
                        if (item1.SubID == item2.SubID && item1.SubName == item2.SubName && item1.ExpenditureName == item2.ExpenditureName)
                        {
                            if (i == 0)
                                item1.CompanyList = new List<QuarterlyBudgetDetailInfo>();
                            if (item.Supplier != string.Empty && item2.Supplier != item.Supplier)
                                item2.RealExpenditure = 0;
                            item1.CompanyList.Add(item2);
                            i++;
                        }
                    }
                }

                foreach (QuarterlyBudgetDetailInfo item1 in expenditurenamelist)
                {
                    int i = 0;
                    foreach (QuarterlyBudgetDetailInfo item2 in Supplierlist)
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

                foreach (QuarterlyBudgetDetailInfo item1 in sublist)
                {
                    int i = 0;
                    foreach (QuarterlyBudgetDetailInfo item2 in expenditurenamelist)
                    {
                        if (item1.SubID == item2.SubID && item1.SubName == item2.SubName)
                        {
                            if (i == 0)
                                item1.BudgetDetailList = new List<QuarterlyBudgetDetailInfo>();

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

                foreach (QuarterlyBudgetDetailInfo item1 in sublist)
                {
                    Company companydal = new Company();
                    IList totalist = (List<CompanyInfo>)companydal.GetAllCompany();
                    foreach (QuarterlyBudgetDetailInfo item2 in item1.BudgetDetailList)
                    {
                        foreach (QuarterlyBudgetDetailInfo item3 in item2.CompanyList)
                        {
                            foreach (CompanyInfo companyitem in totalist)
                            {
                                if (companyitem.CompanyName.Substring(0, 3).Equals(item3.CompanyName.Substring(0, 3)))
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

        private QuarterlyBudgetDetailInfo GetQuarterlyBudgetDetailData1(IDataReader dr)
        {
            QuarterlyBudgetDetailInfo item = new QuarterlyBudgetDetailInfo();
            if (!Convert.IsDBNull(dr["SubID"]))
                item.SubID = Convert.ToInt64(dr["SubID"]);
            if (!Convert.IsDBNull(dr["SubName"]))
                item.SubName = Convert.ToString(dr["SubName"]);
            if (!Convert.IsDBNull(dr["RealExpenditure"]))
                item.RealExpenditure = Convert.ToDecimal(dr["RealExpenditure"]);

            return item;
        }

        private QuarterlyBudgetDetailInfo GetQuarterlyBudgetDetailData2(IDataReader dr)
        {
            QuarterlyBudgetDetailInfo item = new QuarterlyBudgetDetailInfo();
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

        private QuarterlyBudgetDetailInfo GetQuarterlyBudgetDetailData3(IDataReader dr)
        {
            QuarterlyBudgetDetailInfo item = new QuarterlyBudgetDetailInfo();
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

        private QuarterlyBudgetDetailInfo GetQuarterlyBudgetDetailData4(IDataReader dr)
        {
            QuarterlyBudgetDetailInfo item = new QuarterlyBudgetDetailInfo();
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

        public IList Summary1(QuarterlyBudgetDetailInfo item)
        {
            SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString);
            SqlCommand sqlcmd = null;
            IList list = new List<QuarterlyBudgetDetailInfo>();
            try
            {
                conn.Open();
                string cmd = " select SubID, SubName, SUM(RealExpenditure) AS RealExpenditure from FM2E_QuarterlyBudgetDetailView where Title = '" + item.Title + "' and SubID = " + item.SubID + " and CompanyID = '" + item.CompanyID + "' and CompanyName = '" + item.CompanyName + "' and SubName = '" + item.SubName + "' and [Year] = " + item.Year + " and Quarter <= " + item.Quarter + " group by SubID, SubName ";
                sqlcmd = new SqlCommand(cmd, conn);
                using (SqlDataReader rd = sqlcmd.ExecuteReader())
                {
                    while (rd.Read())
                        list.Add(GetQuarterlyBudgetDetailData1(rd));
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
    }
}
