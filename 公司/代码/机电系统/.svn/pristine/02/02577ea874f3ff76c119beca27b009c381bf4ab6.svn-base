using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.BugReport;
using FM2E.Model.Utils;
using FM2E.Model.BugReportManager;
using System.Collections;
using FM2E.Model.Exceptions;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;
using System.Data;
using System.Data.SqlTypes;

namespace FM2E.SQLServerDAL.BugReport
{
    public class BugReport:IBugReport
    {
        public QueryParam GenerateSearchTerm(BugReportInfo item)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = "FM2E_BugReport";
            qp.ReturnFields = "*";
            qp.OrderBy = " order by BugreportID desc ";

            qp.Where = " where 1=1 ";
            if (item.SenderID != null && item.SenderID != string.Empty)
                qp.Where += " and SenderID = '" + item.SenderID + "' ";
            if (item.Status != 0)
                qp.Where += " and Status = " + item.Status + " ";
            if (item.Title != null && item.Title != string.Empty)
                qp.Where += " and Title like '%" + item.Title + "%' ";
           
            return qp;
        }
        public IList GetBugReportList(QueryParam term, out int recordCount)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = "FM2E_BugReport";
                    term.ReturnFields = "*";
                    term.OrderBy = " order by BugreportID desc ";
                    term.Where = " where 1=1 ";
                }
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取用户意见分页失败", e);
            }
        }
        public BugReportInfo GetBugReport(long BugreportID)
        {
            BugReportInfo item = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from FM2E_BugReport ");
            strSql.Append(" where BugreportID=@BugreportID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BugreportID", SqlDbType.BigInt)};
            parameters[0].Value = BugreportID;

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    rd.Read();
                    item = GetData(rd);

                }
            }
            catch (Exception ex)
            {
                throw new DALException("获取该用户意见失败", ex);
            }

            return item;
        }


        public long InsertBugReport(BugReportInfo model)
        {
            long newid = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_BugReport(");
            strSql.Append("Title,Message,Attachment,SenderID,SenderName,Status,Attachment2,Report,ReportTime,ResponseTime,ResponserID,ResponserName)");
            strSql.Append(" values (");
            strSql.Append("@Title,@Message,@Attachment,@SenderID,@SenderName,@Status,@Attachment2,@Report,@ReportTime,@ResponseTime,@ResponserID,@ResponserName)");
            strSql.Append(";select cast(@@IDENTITY as BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,50),
					new SqlParameter("@Message", SqlDbType.NVarChar,200),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,80),
					new SqlParameter("@SenderID", SqlDbType.VarChar,20),
					new SqlParameter("@SenderName", SqlDbType.NVarChar,20),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@Attachment2", SqlDbType.NVarChar,80),
					new SqlParameter("@Report", SqlDbType.NVarChar,200),
                    new SqlParameter("@ReportTime",SqlDbType.DateTime),
                    new SqlParameter("@ResponseTime",SqlDbType.DateTime),
                    new SqlParameter("@ResponserID",SqlDbType.VarChar,20),
                    new SqlParameter("@ResponserName",SqlDbType.NVarChar,10)
                    };
            parameters[0].Value = string.IsNullOrEmpty(model.Title)?SqlString.Null:model.Title;
            parameters[1].Value = string.IsNullOrEmpty(model.Message)?SqlString.Null:model.Message;
            parameters[2].Value = string.IsNullOrEmpty(model.Attachment)?SqlString.Null:model.Attachment;
            parameters[3].Value = string.IsNullOrEmpty(model.SenderID)?SqlString.Null:model.SenderID;
            parameters[4].Value = string.IsNullOrEmpty(model.SenderName)?SqlString.Null:model.SenderName;
            parameters[5].Value = model.Status;
            parameters[6].Value = string.IsNullOrEmpty(model.Attachment2)?SqlString.Null:model.Attachment2;
            parameters[7].Value = string.IsNullOrEmpty(model.Report)?SqlString.Null:model.Report;
            parameters[8].Value = model.ReportTime == DateTime.MinValue ? SqlDateTime.Null : model.ReportTime;
            parameters[9].Value = model.ReponseTime == DateTime.MinValue ? SqlDateTime.Null : model.ReponseTime;
            parameters[10].Value = string.IsNullOrEmpty(model.ResponserID)?SqlString.Null:model.ResponserID;
            parameters[11].Value = string.IsNullOrEmpty(model.ResponserName) ? SqlString.Null : model.ResponserName;
            
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    newid = (long)SQLHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch(Exception ex)
                {
                    throw new DALException("插入用户意见失败", ex);
                }
                finally
                {
                    conn.Close();
                }
            }

            return newid;
        }
        public void UpdateBugReport(BugReportInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_BugReport set ");
            strSql.Append("Title=@Title,");
            strSql.Append("Message=@Message,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("SenderID=@SenderID,");
            strSql.Append("SenderName=@SenderName,");
            strSql.Append("Status=@Status,");
            strSql.Append("Attachment2=@Attachment2,");
            strSql.Append("Report=@Report,");
            strSql.Append("ReportTime=@ReportTime,");
            strSql.Append("ResponseTime=@ResponseTime,");
            strSql.Append("ResponserID=@ResponserID,");
            strSql.Append("ResponserName=@ResponserName");
            strSql.Append(" where BugreportID=@BugreportID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BugreportID", SqlDbType.BigInt,8),
					new SqlParameter("@Title", SqlDbType.NVarChar,50),
					new SqlParameter("@Message", SqlDbType.NVarChar,200),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,80),
					new SqlParameter("@SenderID", SqlDbType.VarChar,20),
					new SqlParameter("@SenderName", SqlDbType.NVarChar,20),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@Attachment2", SqlDbType.NVarChar,80),
					new SqlParameter("@Report", SqlDbType.NVarChar,200),
                    new SqlParameter("@ReportTime",SqlDbType.DateTime),
                    new SqlParameter("@ResponseTime",SqlDbType.DateTime),
                    new SqlParameter("@ResponserID",SqlDbType.VarChar,20),
                    new SqlParameter("@ResponserName",SqlDbType.NVarChar,10)};
            int i=0;
            parameters[i++].Value = model.BugreportID;
            parameters[i++].Value = string.IsNullOrEmpty(model.Title) ? SqlString.Null : model.Title;
            parameters[i++].Value = string.IsNullOrEmpty(model.Message) ? SqlString.Null : model.Message;
            parameters[i++].Value = string.IsNullOrEmpty(model.Attachment) ? SqlString.Null : model.Attachment;
            parameters[i++].Value = string.IsNullOrEmpty(model.SenderID) ? SqlString.Null : model.SenderID;
            parameters[i++].Value = string.IsNullOrEmpty(model.SenderName) ? SqlString.Null : model.SenderName;
            parameters[i++].Value = model.Status;
            parameters[i++].Value = string.IsNullOrEmpty(model.Attachment2) ? SqlString.Null : model.Attachment2;
            parameters[i++].Value = string.IsNullOrEmpty(model.Report) ? SqlString.Null : model.Report;
            parameters[i++].Value = model.ReportTime == DateTime.MinValue ? SqlDateTime.Null : model.ReportTime;
            parameters[i++].Value = model.ReponseTime == DateTime.MinValue ? SqlDateTime.Null : model.ReponseTime;
            parameters[i++].Value = string.IsNullOrEmpty(model.ResponserID) ? SqlString.Null : model.ResponserID;
            parameters[i++].Value = string.IsNullOrEmpty(model.ResponserName) ? SqlString.Null : model.ResponserName;


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
                    throw new DALException("更新用户意见信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        public IList<BugReportInfo> Search(BugReportInfo item)
        {
            string cmd = " select * from FM2E_BugReport where 1=1 ";
            if (item.Title != null && item.Title != string.Empty)
                cmd += " and Title like '%" + item.Title + "%' ";
            if (item.Status != 0)
                cmd += " and Status = " + item.Status + " ";
            if (!string.IsNullOrEmpty(item.SenderID))
            {
                cmd += " and SenderID = '" + item.SenderID + "'";
            }

            List<BugReportInfo> list = new List<BugReportInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        list.Add(GetData(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("搜索用户意见信息失败", e);
            }
            return list;
        }

        private BugReportInfo GetData(IDataReader dr)
        {
            BugReportInfo item = new BugReportInfo();
            if(!Convert.IsDBNull(dr["BugreportID"]))
                item.BugreportID = Convert.ToInt64(dr["BugreportID"]);
            if(!Convert.IsDBNull(dr["Title"]))
                item.Title = Convert.ToString(dr["Title"]);
            if(!Convert.IsDBNull(dr["Message"]))
                item.Message = Convert.ToString(dr["Message"]);
            if(!Convert.IsDBNull(dr["Attachment"]))
                item.Attachment = Convert.ToString(dr["Attachment"]);
            if(!Convert.IsDBNull(dr["SenderID"]))
                item.SenderID = Convert.ToString(dr["SenderID"]);
            if(!Convert.IsDBNull(dr["SenderName"]))
                item.SenderName = Convert.ToString(dr["SenderName"]);
            if(!Convert.IsDBNull(dr["Status"]))
                item.Status = Convert.ToInt32(dr["Status"]);
            if(!Convert.IsDBNull(dr["Attachment2"]))
                item.Attachment2 = Convert.ToString(dr["Attachment2"]);
            if(!Convert.IsDBNull(dr["Report"]))
                item.Report = Convert.ToString(dr["Report"]);
            if (!Convert.IsDBNull(dr["ReportTime"]))
            {
                item.ReportTime = Convert.ToDateTime(dr["ReportTime"]);
            }
            if (!Convert.IsDBNull(dr["ResponseTime"]))
            {
                item.ReponseTime = Convert.ToDateTime(dr["ResponseTime"]);
            }
            if (!Convert.IsDBNull(dr["ResponserID"]))
            {
                item.ResponserID = Convert.ToString(dr["ResponserID"]);
            }
            if (!Convert.IsDBNull(dr["ResponserName"]))
            {
                item.ResponserName = Convert.ToString(dr["ResponserName"]);
            }
            return item;
        }
    }
}
