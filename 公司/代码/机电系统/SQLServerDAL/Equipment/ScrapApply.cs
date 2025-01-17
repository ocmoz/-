﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using FM2E.SQLServerDAL.Utils;
using FM2E.IDAL.Equipment;
using FM2E.Model.Equipment;
using FM2E.Model.Exceptions;
using FM2E.Model.Utils;

using System.Data.SqlTypes;

namespace FM2E.SQLServerDAL.Equipment
{
    public class ScrapApply : IScrapApply
    {
        private ScrapApplyInfo GetData(IDataReader rd)
        {
            ScrapApplyInfo item = new ScrapApplyInfo();
            if (!Convert.IsDBNull(rd["ScrapID"]))
                item.ScrapID = Convert.ToInt64(rd["ScrapID"]);
            if (!Convert.IsDBNull(rd["SheetName"]))
                item.SheetName = Convert.ToString(rd["SheetName"]);
            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);
            if (!Convert.IsDBNull(rd["CompanyName"]))
                item.CompanyName = Convert.ToString(rd["CompanyName"]);
            if (!Convert.IsDBNull(rd["DepID"]))
                item.DepID = Convert.ToInt64(rd["DepID"]);
            if (!Convert.IsDBNull(rd["DepName"]))
                item.DepName = Convert.ToString(rd["DepName"]);
            if (!Convert.IsDBNull(rd["Applicant"]))
                item.Applicant = Convert.ToString(rd["Applicant"]);
            if (!Convert.IsDBNull(rd["ApplicantName"]))
                item.ApplicantName = Convert.ToString(rd["ApplicantName"]);
            if (!Convert.IsDBNull(rd["ApplyDate"]))
                item.ApplyDate = Convert.ToDateTime(rd["ApplyDate"]);
            if (!Convert.IsDBNull(rd["Status"]))
                item.Status = (ScrapStatus)Convert.ToInt32(rd["Status"]);
            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);
            if (!Convert.IsDBNull(rd["Attachment"]))
                item.Attachment = Convert.ToString(rd["Attachment"]);

            return item;
        }

        private ScrapApplyDetailInfo GetDetailData(IDataReader dr)
        {
            ScrapApplyDetailInfo item = new ScrapApplyDetailInfo();

            if (!Convert.IsDBNull(dr["ScrapID"]))
                item.ScrapID = Convert.ToInt64(dr["ScrapID"]);

            if (!Convert.IsDBNull(dr["EquipmentNo"]))
                item.EquipmentNo = Convert.ToString(dr["EquipmentNo"]);

            if (!Convert.IsDBNull(dr["EquipmentName"]))
                item.EquipmentName = Convert.ToString(dr["EquipmentName"]);

            if (!Convert.IsDBNull(dr["ScrapReason"]))
                item.ScrapReason = Convert.ToString(dr["ScrapReason"]);

            return item;
        }

        private ScrapApprovaFacadelInfo GetApprovalHistoryData(IDataReader dr)
        {
            ScrapApprovaFacadelInfo item = new ScrapApprovaFacadelInfo();

            if (!Convert.IsDBNull(dr["ScrapID"]))
                item.ScrapID = Convert.ToInt64(dr["ScrapID"]);

            if (!Convert.IsDBNull(dr["SheetNO"]))
                item.SheetNO = Convert.ToString(dr["SheetNO"]);
            /*
            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);

            if (!Convert.IsDBNull(dr["CompanyName"]))
                item.CompanyName = Convert.ToString(dr["CompanyName"]);

            if (!Convert.IsDBNull(dr["DepID"]))
                item.DepID = Convert.ToInt64(dr["DepID"]);

            if (!Convert.IsDBNull(dr["DepName"]))
                item.DepName = Convert.ToString(dr["DepName"]);
            */
            if (!Convert.IsDBNull(dr["Applicant"]))
                item.Applicant = Convert.ToString(dr["Applicant"]);

            if (!Convert.IsDBNull(dr["ApplicantName"]))
                item.ApplicantName = Convert.ToString(dr["ApplicantName"]);

            if (!Convert.IsDBNull(dr["ApplyDate"]))
                item.ApplyDate = Convert.ToDateTime(dr["ApplyDate"]);

            if (!Convert.IsDBNull(dr["Status"]))
                item.Status = (ScrapStatus)Convert.ToInt32(dr["Status"]);

            if (!Convert.IsDBNull(dr["ApprovalerID"]))
                item.ApprovalerID = Convert.ToString(dr["ApprovalerID"]);

            if (!Convert.IsDBNull(dr["ApprovalerName"]))
                item.ApprovalerName = Convert.ToString(dr["ApprovalerName"]);

            if (!Convert.IsDBNull(dr["ApprovalResult"]))
                item.ApprovalResult = Convert.ToInt32(dr["ApprovalResult"]);

            if (!Convert.IsDBNull(dr["ApprovalDate"]))
                item.ApprovalDate = Convert.ToDateTime(dr["ApprovalDate"]);

            return item;
        }


        #region IScrapApply 成员

        IList IScrapApply.GetScrapApplyList(QueryParam term, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取设备报废申请列表失败", e);
            }
        }

        IList IScrapApply.GetScrapApprovalHistory(QueryParam term, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetApprovalHistoryData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取报废历史记录失败", e);
            }
        }

        ScrapApplyInfo IScrapApply.GetScrapApply(long id)
        {
            ScrapApplyInfo model = null;
            SqlConnection conn = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  a.ScrapID, a.SheetName,a.CompanyID, b.CompanyName, a.DepID,e.Name as DepName, a.Applicant,d.PersonName as ApplicantName, a.Status,a.ApplyDate,a.Remark,a.Attachment ");
                strSql.Append(" from FM2E_Scrap a left JOIN FM2E_Company b ON a.CompanyID = b.CompanyID left join FM2E_User d on a.Applicant=d.UserName left join FM2E_Department e on a.DepID=e.DepartmentID");
                strSql.Append(" where a.ScrapID=@ScrapID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ScrapID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                using (SqlDataReader rd = SQLHelper.ExecuteReader(conn, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        model = GetData(rd);
                    }
                }

                if (model == null) return null;

                //获取借调设备列表
                StringBuilder strDetailSql = new StringBuilder();
                strDetailSql.Append("select ScrapID,EquipmentNO,EquipmentName,ScrapReason ");
                strDetailSql.Append(" from FM2E_ScrapEquipments");
                strDetailSql.Append(" where ScrapID=@ScrapID");

                ArrayList list = new ArrayList();
                using (SqlDataReader rd = SQLHelper.ExecuteReader(conn, CommandType.Text, strDetailSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        ScrapApplyDetailInfo item = GetDetailData(rd);
                        list.Add(item);
                    }
                }
                model.Equipments = list;

                //获取审批历史
                StringBuilder strApprovalSql = new StringBuilder();
                strApprovalSql.Append("select a.ScrapID,a.ApprovalerID,b.PersonName as ApprovalerName,a.EquipmentNO,a.EquipmentName,a.Result,a.FeeBack,a.ApprovalDate ");
                strApprovalSql.Append(" from FM2E_ScrapApproval a left join FM2E_User b on a.ApprovalerID=b.UserName ");
                strApprovalSql.Append(" where a.ScrapID=@ScrapID order by a.ApprovalDate asc");

                ArrayList approvalList = new ArrayList();
                using (SqlDataReader rd = SQLHelper.ExecuteReader(conn, CommandType.Text, strApprovalSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        ScrapApprovalInfo item = new ScrapApprovalInfo();

                        if (!Convert.IsDBNull(rd["ScrapID"]))
                            item.ScrapID = Convert.ToInt64(rd["ScrapID"]);

                        if (!Convert.IsDBNull(rd["ApprovalerID"]))
                            item.ApprovalerID = Convert.ToString(rd["ApprovalerID"]);

                        if (!Convert.IsDBNull(rd["ApprovalerName"]))
                            item.ApprovalerName = Convert.ToString(rd["ApprovalerName"]);

                        if (!Convert.IsDBNull(rd["EquipmentNO"]))
                            item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

                        if (!Convert.IsDBNull(rd["EquipmentName"]))
                            item.EquipmentName = Convert.ToString(rd["EquipmentName"]);

                        if (!Convert.IsDBNull(rd["Result"]))
                            item.Result = Convert.ToBoolean(rd["Result"]);

                        if (!Convert.IsDBNull(rd["FeeBack"]))
                            item.FeeBack = Convert.ToString(rd["FeeBack"]);

                        if (!Convert.IsDBNull(rd["ApprovalDate"]))
                            item.ApprovalDate = Convert.ToDateTime(rd["ApprovalDate"]);

                        approvalList.Add(item);
                    }
                }
                model.ApprovalList = approvalList;
            }
            catch (Exception ex)
            {
                model = null;
                throw new DALException("获取设备报废申请单失败", ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn = null;
                }
            }
            return model;
        }

        private long InsertScrapApply(SqlTransaction trans, ScrapApplyInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("insert into {0}(", "FM2E_Scrap");
            strSql.Append("SheetName,CompanyID,DepID,Applicant,Status,ApplyDate,Remark,Attachment)");
            strSql.Append(" values (");
            strSql.Append("@SheetName,@CompanyID,@DepID,@Applicant,@Status,@ApplyDate,@Remark,@Attachment)");
            strSql.Append(";select cast(@@IDENTITY as BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@SheetName", SqlDbType.NVarChar,20),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@DepID", SqlDbType.BigInt),
					new SqlParameter("@Applicant", SqlDbType.VarChar,20),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@ApplyDate", SqlDbType.DateTime),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                                        new SqlParameter("@Attachment",SqlDbType.NVarChar,100)};
            parameters[0].Value = model.SheetName;
            parameters[1].Value = model.CompanyID;
            parameters[2].Value = model.DepID;
            parameters[3].Value = model.Applicant;
            parameters[4].Value = model.Status;
            parameters[5].Value = model.ApplyDate;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.Attachment;
            long id = 1;
            id = (long)SQLHelper.ExecuteScalar(trans, CommandType.Text, strSql.ToString(), parameters);

            return id;
        }

        long IScrapApply.AddScrapApply(ScrapApplyInfo model)
        {
            long id = 0;
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先插入申请单基本信息
                id = InsertScrapApply(trans, model);

                StringBuilder strSql = new StringBuilder();
                strSql.Append("Insert Into FM2E_ScrapEquipments");
                strSql.Append("(ScrapID,EquipmentNO,EquipmentName,ScrapReason) ");
                strSql.Append(" values(@ScrapID,@EquipmentNO,@EquipmentName,@ScrapReason)");

                SqlParameter[] parameters = {
					new SqlParameter("@ScrapID", SqlDbType.BigInt,8),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
                    new SqlParameter("@EquipmentName", SqlDbType.NVarChar,20),
					new SqlParameter("@ScrapReason", SqlDbType.NVarChar,50)};

                foreach (ScrapApplyDetailInfo item in model.Equipments)
                {
                    parameters[0].Value = id;
                    parameters[1].Value = item.EquipmentNo;
                    parameters[2].Value = item.EquipmentName;
                    parameters[3].Value = item.ScrapReason;
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                }
                //再插入明细信息
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new DALException("添加设备报废申请失败", e);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn = null;
                }
            }
            return id;
        }

        void IScrapApply.UpdateScrapApply(ScrapApplyInfo model)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                UpdateScrapApply(trans, model);
                UpdateScrapApplyDetail(trans, model);

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new DALException("更新设备报废申请失败", e);
            }
        }

        private void UpdateScrapApply(SqlTransaction trans, ScrapApplyInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("update {0} set ", "FM2E_Scrap");
            strSql.Append("SheetName=@SheetName,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("DepID=@DepID,");
            strSql.Append("Applicant=@Applicant,");
            strSql.Append("Status=@Status,");
            strSql.Append("ApplyDate=@ApplyDate,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Attachment=@Attachment");
            strSql.Append(" where ScrapID=@ScrapID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ScrapID", SqlDbType.BigInt),
					new SqlParameter("@SheetName", SqlDbType.NVarChar,20),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@DepID", SqlDbType.BigInt),
					new SqlParameter("@Applicant", SqlDbType.VarChar,20),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@ApplyDate", SqlDbType.DateTime),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                                        new SqlParameter("@Attachment",SqlDbType.NVarChar,100)};
            parameters[0].Value = model.ScrapID;
            parameters[1].Value = model.SheetName;
            parameters[2].Value = model.CompanyID;
            parameters[3].Value = model.DepID;
            parameters[4].Value = model.Applicant;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.ApplyDate;
            parameters[7].Value = model.Remark;
            parameters[8].Value = string.IsNullOrEmpty(model.Attachment) ? SqlString.Null : model.Attachment;

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        }

        private void UpdateScrapApplyDetail(SqlTransaction trans, ScrapApplyInfo model)
        {
            //先删除原来的明细，后添加新的明细
            StringBuilder delSql = new StringBuilder();
            delSql.AppendFormat("delete {0} ", "FM2E_ScrapEquipments");
            delSql.Append(" where ScrapID=@ScrapID ");
            SqlParameter[] delPara = {
					new SqlParameter("@ScrapID", SqlDbType.BigInt)};
            delPara[0].Value = model.ScrapID;
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, delSql.ToString(), delPara);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Insert Into FM2E_ScrapEquipments");
            strSql.Append("(ScrapID,EquipmentNO,EquipmentName,ScrapReason) ");
            strSql.Append(" values(@ScrapID,@EquipmentNO,@EquipmentName,@ScrapReason)");

            SqlParameter[] parameters = {
					new SqlParameter("@ScrapID", SqlDbType.BigInt,8),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
                    new SqlParameter("@EquipmentName", SqlDbType.NVarChar,20),
					new SqlParameter("@ScrapReason", SqlDbType.NVarChar,50)};

            foreach (ScrapApplyDetailInfo item in model.Equipments)
            {
                parameters[0].Value = model.ScrapID;
                parameters[1].Value = item.EquipmentNo;
                parameters[2].Value = item.EquipmentName;
                parameters[3].Value = item.ScrapReason == null ? SqlString.Null : item.ScrapReason;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            }
        }

        void IScrapApply.DeleteScrapApply(long id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("delete {0} ", "FM2E_Scrap");
                strSql.Append(" where ScrapID=@ScrapID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ScrapID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除设备报废申请失败", e);
            }
        }
        void IScrapApply.ApprovalScrapApply(ScrapApprovalInfo model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("insert into {0}(", "FM2E_ScrapApproval");
                strSql.Append("ScrapID,ApprovalerID,Result,FeeBack,ApprovalDate)");
                strSql.Append(" values (");
                strSql.Append("@ScrapID,@ApprovalerID,@Result,@FeeBack,@ApprovalDate)");

                SqlParameter[] parameters = {
					new SqlParameter("@ScrapID", SqlDbType.BigInt,8),
					new SqlParameter("@ApprovalerID", SqlDbType.VarChar,20),
					new SqlParameter("@Result", SqlDbType.Bit,1),
					new SqlParameter("@FeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@ApprovalDate", SqlDbType.DateTime)};
                parameters[0].Value = model.ScrapID;
                parameters[1].Value = model.ApprovalerID;
                parameters[2].Value = model.Result;
                parameters[3].Value = model.FeeBack;
                parameters[4].Value = model.ApprovalDate;


                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("添加审批信息失败", e);
            }
        }
        void IScrapApply.ChangeStatus(long ScrapID, int status)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_Scrap set Status=@Status ");
                strSql.Append("where ScrapID=@ScrapID");
                SqlParameter[] parameters = {
					new SqlParameter("@ScrapID", SqlDbType.BigInt),
					new SqlParameter("@Status", SqlDbType.TinyInt,1)};
                parameters[0].Value = ScrapID;
                parameters[1].Value = status;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("改变申请单状态值失败", e);
            }
        }

        QueryParam IScrapApply.GenerateSearchTerm(ScrapApplySearchInfo item)
        {
            string sqlSearch = "where 1=1";
            if (!string.IsNullOrEmpty(item.CompanyID) && item.CompanyID != "0")
                sqlSearch += " and a.CompanyID ='" + item.CompanyID + "'";

            if (item.DepID != 0)
                sqlSearch += " and a.DepID ='" + item.DepID + "'";

            if (item.SheetName != null && item.SheetName.Trim() != string.Empty)
                sqlSearch += " and a.SheetName like '%" + item.SheetName + "%'";

            if (item.Status != 0)
                sqlSearch += " and a.Status =" + (int)item.Status;

            if (!string.IsNullOrEmpty(item.Applicant))
            {
                sqlSearch += " and a.Applicant='" + item.Applicant + "'";
            }

            if (!string.IsNullOrEmpty(item.ApplicantName))
            {
                sqlSearch += " and d.PersonName like '%" + item.ApplicantName + "%'";
            }

            if (DateTime.Compare(item.SubmitTimeFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(item.SubmitTimeFrom, sqlMinDate) < 0)
                    item.SubmitTimeFrom = sqlMinDate;

                sqlSearch += " and a.ApplyDate>='" + item.SubmitTimeFrom.ToShortDateString() + " 00:00:00'";
            }

            if (DateTime.Compare(item.SubmitTimeTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(item.SubmitTimeTo, sqlMaxDate) > 0)
                    item.SubmitTimeTo = sqlMaxDate;

                sqlSearch += " and a.ApplyDate<='" + item.SubmitTimeTo.ToShortDateString() + " 23:59:59'";
            }

            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_Scrap a left JOIN FM2E_Company b ON a.CompanyID = b.CompanyID LEFT JOIN FM2E_User d ON a.Applicant = d.UserName LEFT JOIN FM2E_Department c ON a.DepID=c.DepartmentID";
            searchTerm.ReturnFields = "a.ScrapID, a.SheetName,a.CompanyID, b.CompanyName,a.Applicant,d.PersonName as ApplicantName, a.Status,a.ApplyDate,a.DepID,c.Name as DepName,a.Remark,a.Attachment";
            searchTerm.OrderBy = "order by ApplyDate desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        QueryParam IScrapApply.GenerateSearchTerm(ScrapApplySearchInfo item, string[] WFStates)
        {
            string sqlSearch = "where 1=1";
            if (!string.IsNullOrEmpty(item.CompanyID) && item.CompanyID != "0")
                sqlSearch += " and a.CompanyID ='" + item.CompanyID + "'";

            if (item.DepID != 0)
                sqlSearch += " and a.DepID ='" + item.DepID + "'";

            if (item.SheetName != null && item.SheetName.Trim() != string.Empty)
                sqlSearch += " and a.SheetName like '%" + item.SheetName + "%'";

            if (item.Status != 0)
                sqlSearch += " and a.Status =" + (int)item.Status;

            if (!string.IsNullOrEmpty(item.Applicant))
            {
                sqlSearch += " and a.Applicant='" + item.Applicant + "'";
            }

            if (!string.IsNullOrEmpty(item.ApplicantName))
            {
                sqlSearch += " and d.PersonName like '%" + item.ApplicantName + "%'";
            }

            if (DateTime.Compare(item.SubmitTimeFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(item.SubmitTimeFrom, sqlMinDate) < 0)
                    item.SubmitTimeFrom = sqlMinDate;

                sqlSearch += " and a.ApplyDate>='" + item.SubmitTimeFrom.ToShortDateString() + " 00:00:00'";
            }

            if (DateTime.Compare(item.SubmitTimeTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(item.SubmitTimeTo, sqlMaxDate) > 0)
                    item.SubmitTimeTo = sqlMaxDate;

                sqlSearch += " and a.ApplyDate<='" + item.SubmitTimeTo.ToShortDateString() + " 23:59:59'";
            }
            if (WFStates != null && WFStates.Length > 0)
            {
                sqlSearch += " and h.TableName='FM2E_Scrap' and (";
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
            searchTerm.TableName = "FM2E_Scrap a left JOIN FM2E_Company b ON a.CompanyID = b.CompanyID LEFT JOIN FM2E_User d ON a.Applicant = d.UserName LEFT JOIN FM2E_Department c ON a.DepID=c.DepartmentID left join FM2E_WorkflowInstance h on a.ScrapID=h.DataID ";
            searchTerm.ReturnFields = "a.ScrapID, a.SheetName,a.CompanyID, b.CompanyName,a.Applicant,d.PersonName as ApplicantName, a.Status,a.ApplyDate,a.DepID,c.Name as DepName,a.Remark,a.Attachment";
            searchTerm.OrderBy = "order by ApplyDate desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }

        QueryParam IScrapApply.GenerateSearchTerm(ScrapApprovalSearchInfo item)
        {
            #region 生成where条件
            string sqlSearch = "where 1=1";

            if (item.SheetName != null && item.SheetName.Trim() != string.Empty)
                sqlSearch += " and b.SheetName like '%" + item.SheetName + "%'";

            if (!string.IsNullOrEmpty(item.ApplicantName))
            {
                sqlSearch += " and c.PersonName like '%" + item.ApplicantName + "%'";
            }
            if (DateTime.Compare(item.ApplyDateFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(item.ApplyDateFrom, sqlMinDate) < 0)
                    item.ApplyDateFrom = sqlMinDate;

                sqlSearch += " and b.ApplyDate>='" + item.ApplyDateFrom.ToShortDateString() + " 00:00:00'";
            }
            if (DateTime.Compare(item.ApplyDateTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(item.ApplyDateTo, sqlMaxDate) > 0)
                    item.ApplyDateTo = sqlMaxDate;

                sqlSearch += " and b.ApplyDate<='" + item.ApplyDateTo.ToShortDateString() + " 23:59:59'";
            }
            if (DateTime.Compare(item.ApprovalDateFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(item.ApprovalDateFrom, sqlMinDate) < 0)
                    item.ApprovalDateFrom = sqlMinDate;

                sqlSearch += " and a.ApprovalDate>='" + item.ApprovalDateFrom.ToShortDateString() + " 00:00:00'";
            }
            if (DateTime.Compare(item.ApprovalDateTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(item.ApprovalDateTo, sqlMaxDate) > 0)
                    item.ApprovalDateTo = sqlMaxDate;

                sqlSearch += " and a.ApprovalDate<='" + item.ApprovalDateTo.ToShortDateString() + " 23:59:59'";
            }
            if (item.ApprovalResult != 3)
            {
                sqlSearch += " and a.Result=" + item.ApprovalResult;
            }
            #endregion
            QueryParam searchTerm = new QueryParam();

            searchTerm.TableName = "FM2E_ScrapApproval a left join FM2E_Scrap b on a.ScrapID=b.ScrapID";
            searchTerm.TableName += "                   Left join FM2E_User c on c.UserName=a.ApprovalerID";
            searchTerm.TableName += "                   Left join FM2E_User d on d.UserName=b.Applicant";
            searchTerm.ReturnFields = "a.*,b.SheetName as SheetNO,b.SheetName as SheetName,c.PersonName as ApplicantName,d.PersonName as ApprovalerName,b.Applicant,b.ApplyDate,b.Status,a.Result as ApprovalResult";
            searchTerm.OrderBy = "order by ApprovalDate desc";
            searchTerm.Where = sqlSearch + " and not (a.ScrapID in (Select ScrapID From FM2E_ScrapRecord))";

            return searchTerm;
        }
        #endregion
    }
}

