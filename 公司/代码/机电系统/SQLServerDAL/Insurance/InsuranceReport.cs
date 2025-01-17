﻿using System;
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
    public class InsuranceReport : IInsuranceReport
    {

        private const string QUERY_ALL_INSURANCE_REPORT_INFO = "select * from FM2E_InsuranceReportInfo order by id";

        private const string QUERY_INSURANCE_REPORT_INFO_BY_ID = "select * from FM2E_InsuranceReportInfo where Id = ";

        private const string INSERT_INSURANCE_REPORT_INFO =
            "Insert into FM2E_InsuranceReportInfo ([ReportNo],[ReportDate],[InsuranceNo],[RiskType],[RiskTypeName],[RiskDate],[RiskContent],[RiskAttachment],[Operator],[State],ReceiptNo,Estimate,Claim,Address) " +
            "values(@ReportNo,@ReportDate,@InsuranceNo,@RiskType,@RiskTypeName,@RiskDate,@RiskContent,@RiskAttachment,@Operator,@State,@ReceiptNo,@Estimate,@Claim,@Address)";

        private const string UPDATE_INSURANCE_REPORT_INFO =
            "Update FM2E_InsuranceReportInfo set [ReportNo] =@ReportNo,[ReportDate]=@ReportDate,[InsuranceNo]=@InsuranceNo,[RiskType]=@RiskType,[RiskTypeName] = @RiskTypeName,[RiskContent]=@RiskContent,[RiskAttachment]=@RiskAttachment,[Operator]=@Operator,[RepairContent]=@RepairContent,[RepairAttachment]=@RepairAttachment,[StationManager]=@StationManager,[ReviewContent]=@ReviewContent,[InsuranceManager]=@InsuranceManager where [Id] = @Id";

        private const string DELETE_INSURANCE_REPORT_INFO = "delete from FM2E_InsuranceReportInfo where [ID]='{0}'";

        private const string REPAIR_INSURANCE_REPORT_INFO =
           "Update FM2E_InsuranceReportInfo set [RepairContent]=@RepairContent,[RepairAttachment]=@RepairAttachment,[StationManager]=@StationManager,[State]=@State where [Id] = @Id";

        private const string REVIEW_INSURANCE_REPORT_INFO = 
            "Update FM2E_InsuranceReportInfo set [ReviewContent]=@ReviewContent,[InsuranceManager]=@InsuranceManager,[State]=@State where [Id] = @Id";

        public long InsertInsuranceReport(InsuranceReportInfo item)
        {
            long id = 0;
            SqlParameter[] param = GetInsertParam();
            param[0].Value = item.ReportNo;
            param[1].Value = item.ReportDate;
            param[2].Value = item.InsuranceNo;
            param[3].Value = item.RiskType;
            param[4].Value = item.RiskTypeName;
            param[5].Value = item.RiskDate;
            param[6].Value = item.RiskContent;
            param[7].Value = string.IsNullOrEmpty(item.RiskAttachment) ? SqlString.Null : item.RiskAttachment;
            param[8].Value = item.Operator;
            param[9].Value = item.State;

            param[10].Value = item.ReceiptNo;
            param[11].Value = item.Estimate;
            param[12].Value = item.Claim;
            param[13].Value = item.Address;


            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, INSERT_INSURANCE_REPORT_INFO, param);
                    if (result == 0)
                        throw new Exception("没有添加任何数据项");
                    id=(long)result;
                }
                catch (Exception e)
                {
                    throw e;// DALException("插入设备信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
                return id;
            }
        }






        public IList GetAllInsuranceReport()
        {
            IList list = new List<InsuranceReportInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, QUERY_ALL_INSURANCE_REPORT_INFO, null))
                {
                    while (rd.Read())
                        list.Add(GetInsuranceReportInfo(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取所有设备信息时失败", e);
            }
            return list;
        }



        public void UpdateInsuranceReport(InsuranceReportInfo item)
        {
            SqlParameter[] param = GetUpdateParam();

            param[0].Value = item.ReportNo;
            param[1].Value = item.ReportDate;
            param[2].Value = item.InsuranceNo;
            param[3].Value = item.RiskType;
            param[4].Value = item.RiskTypeName;
            param[5].Value = item.RiskDate;
            param[6].Value = item.RiskContent;
            param[7].Value = item.RiskAttachment;
            param[8].Value = item.Operator;
            param[9].Value = item.RepairContent;
            param[10].Value = item.RepairAttachment;
            param[11].Value = item.StationManager;
            param[12].Value = item.ReviewContent;
            param[13].Value = item.InsuranceManager;
            param[14].Value = item.Id;
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, UPDATE_INSURANCE_REPORT_INFO, param);
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

        public void DelInsuranceReport(string id)
        {
            string cmd = string.Format(DELETE_INSURANCE_REPORT_INFO, id);
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

        public InsuranceReportInfo GetInsuranceReportInfo(long id)
        {
            InsuranceReportInfo insuranceInfo = null;
            try
            {
                string queryStr = QUERY_INSURANCE_REPORT_INFO_BY_ID + id;
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, queryStr, null))
                {
                    if (rd.Read())
                        insuranceInfo = GetInsuranceReportInfo(rd);
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取保单信息失败", e);
            }
            return insuranceInfo;
        }

        public InsuranceReportInfo GetInsuranceReportInfo(string insuranceNo)
        {
            return null;
        }
        

        public IList GetInsuranceReports(QueryParam term, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetInsuranceReportInfo, term, out recordCount);
            }
            catch (Exception ex)
            {
                throw new DALException("获取考核表分页失败", ex);
            }
        }


        public QueryParam GetSearchTerm(InsuranceReportSearchInfo term)
        {
            QueryParam qp = new QueryParam();
            qp.ReturnFields = "*";
            qp.TableName = "FM2E_InsuranceReportInfo";
            qp.Where = GetSqlWhere(term);
            qp.OrderBy = "order by Id desc";

            return qp;
        }


        private string GetSqlWhere(InsuranceReportSearchInfo term)
        {
            string sqlSearch = "where 1=1 ";

            if (!string.IsNullOrEmpty(term.InsuranceNo))
                sqlSearch += " and InsuranceNo like '%" + term.InsuranceNo + "%'";

            if (!string.IsNullOrEmpty(term.ReportNo))
                sqlSearch += " and ReportNo like '%" + term.ReportNo + "%'";

            if (term.RiskType != 0)
                sqlSearch += " and RiskType=" + (int)term.RiskType;
            if (DateTime.Compare(term.StartReportDate, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(term.StartReportDate, sqlMinDate) < 0)
                    term.StartReportDate = sqlMinDate;

                sqlSearch += " and   ReportDate>='" + term.StartReportDate.ToString("yyyy-M-d") + "'";
            }
            if (DateTime.Compare(term.EndReportDate, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(term.EndReportDate, sqlMinDate) < 0)
                    term.EndReportDate = sqlMinDate;

                sqlSearch += " and ReportDate<='" + term.EndReportDate.ToString("yyyy-M-d") + "'";
            }
            if (DateTime.Compare(term.StartRiskDate, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(term.StartRiskDate, sqlMinDate) < 0)
                    term.StartRiskDate = sqlMinDate;

                sqlSearch += " and   RiskDate>='" + term.StartRiskDate.ToString("yyyy-M-d") + "'";
            }
            if (DateTime.Compare(term.EndRiskDate, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(term.EndRiskDate, sqlMinDate) < 0)
                    term.EndRiskDate = sqlMinDate;

                sqlSearch += " and RiskDate<='" + term.EndRiskDate.ToString("yyyy-M-d") + "'";
            }
            if (term.State != 0)
                sqlSearch += " and State = " + (int)term.State;

            return sqlSearch;
        }

        /// <summary>
        /// 获取Insert InsuranceInfo得SQLPararment
        /// </summary>
        /// <returns></returns>
        private static SqlParameter[] GetInsertParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(INSERT_INSURANCE_REPORT_INFO);
            if (param == null)
            {
                param = new SqlParameter[]{
                    new SqlParameter("@ReportNo",SqlDbType.VarChar,50),
                    new SqlParameter("@ReportDate",SqlDbType.DateTime),
                    new SqlParameter("@InsuranceNo",SqlDbType.VarChar,30),
                    new SqlParameter("@RiskType",SqlDbType.Int),
                    new SqlParameter("@RiskTypeName",SqlDbType.VarChar,50),
                    new SqlParameter("@RiskDate",SqlDbType.DateTime),
                    new SqlParameter("@RiskContent",SqlDbType.VarChar,3000),
                    new SqlParameter("@RiskAttachment",SqlDbType.VarChar,100),
                    new SqlParameter("@Operator",SqlDbType.VarChar,50),
                    new SqlParameter("@State",SqlDbType.Int),
                     new SqlParameter("@ReceiptNo",SqlDbType.VarChar,50),
                      new SqlParameter("@Estimate",SqlDbType.VarChar,50),
                       new SqlParameter("@Claim",SqlDbType.VarChar,50),
                        new SqlParameter("@Address",SqlDbType.VarChar,50)
                };
                SQLHelper.CacheParameters(INSERT_INSURANCE_REPORT_INFO, param);
            }
            return param;
        }

        private static SqlParameter[] GetUpdateParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(UPDATE_INSURANCE_REPORT_INFO);
            if (param == null)
            {
                param = new SqlParameter[]{
                    new SqlParameter("@ReportNo",SqlDbType.VarChar,50),
                    new SqlParameter("@ReportDate",SqlDbType.DateTime),
                    new SqlParameter("@InsuranceNo",SqlDbType.VarChar,30),
                    new SqlParameter("@RiskType",SqlDbType.Int),
                    new SqlParameter("@RiskTypeName",SqlDbType.VarChar,50),
                    new SqlParameter("@RiskDate",SqlDbType.DateTime),
                    new SqlParameter("@RiskContent",SqlDbType.VarChar,3000),
                    new SqlParameter("@RiskAttachment",SqlDbType.VarChar,1000),
                    new SqlParameter("@Operator",SqlDbType.VarChar,50),
                    new SqlParameter("@RepairContent",SqlDbType.VarChar,1000),
                    new SqlParameter("@RepairAttachment",SqlDbType.VarChar,1000),
                    new SqlParameter("@StationManager",SqlDbType.VarChar,50),
                    new SqlParameter("@ReviewContent",SqlDbType.VarChar,1000),
                    new SqlParameter("@InsuranceManager",SqlDbType.VarChar,50),
                    new SqlParameter("@Id",SqlDbType.BigInt,32) 
                };
                SQLHelper.CacheParameters(UPDATE_INSURANCE_REPORT_INFO, param);
            }
            return param;
        }

     
        private static SqlParameter[] GetRepairParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(REPAIR_INSURANCE_REPORT_INFO);
            if (param == null)
            {
                param = new SqlParameter[]{
                    new SqlParameter("@RepairContent",SqlDbType.VarChar,1000),
                    new SqlParameter("@RepairAttachment",SqlDbType.VarChar,100),
                    new SqlParameter("@StationManager",SqlDbType.VarChar,50),
                    new SqlParameter("@State",SqlDbType.Int),
                    new SqlParameter("@Id",SqlDbType.BigInt,32) 
                };
                SQLHelper.CacheParameters(REPAIR_INSURANCE_REPORT_INFO, param);
            }
            return param;
        }

        private static SqlParameter[] GetReviewParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(REVIEW_INSURANCE_REPORT_INFO);
            if (param == null)
            {
                param = new SqlParameter[]{
                    new SqlParameter("@ReviewContent",SqlDbType.VarChar,1000),
                    new SqlParameter("@InsuranceManager",SqlDbType.VarChar,50),
                    new SqlParameter("@State",SqlDbType.Int),
                    new SqlParameter("@Id",SqlDbType.BigInt,32) 
                };
                SQLHelper.CacheParameters(REVIEW_INSURANCE_REPORT_INFO, param);
            }
            return param;
        }

        private InsuranceReportInfo GetInsuranceReportInfo(IDataReader dr)
        {
            InsuranceReportInfo item = new InsuranceReportInfo();
            if (!Convert.IsDBNull(dr["Id"]))
            {
                item.Id = Convert.ToInt64(dr["Id"]);
            }
            if (!Convert.IsDBNull(dr["ReportNo"]))
            {
                item.ReportNo = Convert.ToString(dr["ReportNo"]);
            }
            if (!Convert.IsDBNull(dr["InsuranceNo"]))
            {
                item.InsuranceNo = Convert.ToString(dr["InsuranceNo"]);
            }
            if (!Convert.IsDBNull(dr["ReportDate"]))
            {
                item.ReportDate = Convert.ToDateTime(dr["ReportDate"]);
            }
            if (!Convert.IsDBNull(dr["RiskType"]))
            {
                item.RiskType = (RiskType)Convert.ToInt32(dr["RiskType"]);
            }
            if (!Convert.IsDBNull(dr["RiskTypeName"]))
            {
                item.RiskTypeName = Convert.ToString(dr["RiskTypeName"]);
            }
            if (!Convert.IsDBNull(dr["RiskDate"]))
            {
                item.RiskDate = Convert.ToDateTime(dr["RiskDate"]);
            }
            if (!Convert.IsDBNull(dr["RiskContent"]))
            {
                item.RiskContent = Convert.ToString(dr["RiskContent"]);
            }
            if (!Convert.IsDBNull(dr["RiskAttachment"]))
            {
                item.RiskAttachment = Convert.ToString(dr["RiskAttachment"]);
            }
            if (!Convert.IsDBNull(dr["Operator"]))
            {
                item.Operator = Convert.ToString(dr["Operator"]);
            }
            if (!Convert.IsDBNull(dr["RepairContent"]))
            {
                item.RepairContent = Convert.ToString(dr["RepairContent"]);
            }
            if (!Convert.IsDBNull(dr["RepairAttachment"]))
            {
                item.RepairAttachment = Convert.ToString(dr["RepairAttachment"]);
            }
            if (!Convert.IsDBNull(dr["StationManager"]))
            {
                item.StationManager = Convert.ToString(dr["StationManager"]);
            }
            if (!Convert.IsDBNull(dr["ReviewContent"]))
            {
                item.ReviewContent = Convert.ToString(dr["ReviewContent"]);
            }
            if (!Convert.IsDBNull(dr["InsuranceManager"]))
            {
                item.InsuranceManager = Convert.ToString(dr["InsuranceManager"]);
            }
            if (!Convert.IsDBNull(dr["State"]))
            {
                item.State = (State)Convert.ToInt32(dr["State"]);
            }

               if (!Convert.IsDBNull(dr["ReceiptNo"]))
            {
                item.ReceiptNo = Convert.ToString(dr["ReceiptNo"]);
            }
            if (!Convert.IsDBNull(dr["Estimate"]))
            {
                item.Estimate = Convert.ToString(dr["Estimate"]);
            }
            if (!Convert.IsDBNull(dr["Claim"]))
            {
                item.Claim = Convert.ToString(dr["Claim"]);
            }
              if (!Convert.IsDBNull(dr["Address"]))
            {
                item.Address = Convert.ToString(dr["Address"]);
            }
           
            return item;
        }
        public void RepairRegister(InsuranceReportInfo item)
        {
            SqlParameter[] param = GetRepairParam();

           
            param[0].Value = item.RepairContent;
            param[1].Value = item.RepairAttachment;
            param[2].Value = item.StationManager;
            param[3].Value = item.State;
            param[4].Value = item.Id;
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, REPAIR_INSURANCE_REPORT_INFO, param);
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
        public void ReviewRegister(InsuranceReportInfo item)
        {
            SqlParameter[] param = GetReviewParam();


            param[0].Value = item.ReviewContent;
            param[1].Value = item.InsuranceManager; ;
            param[2].Value = item.State;
            param[3].Value = item.Id;
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, REVIEW_INSURANCE_REPORT_INFO, param);
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

    }
}
