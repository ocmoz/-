using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Basic;
using FM2E.IDAL.Basic;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;
using System.Data.SqlTypes;

namespace FM2E.SQLServerDAL.Basic
{
    public class TollGate:ITollGate
    {
        public QueryParam GenerateSearchTerm(TollGateInfo item)
        {
            string sqlSearch = "where 1=1";
            if (item.TollGateID != "")
                sqlSearch += " and TollGateID like '%" + item.TollGateID + "%'";
            if (item.TollGateName != "")
                sqlSearch += " and TollGateName like '%" + item.TollGateName + "%'";
            if (item.CompanyName != "")
                sqlSearch += " and b.CompanyName like '%" + item.CompanyName + "%'";
            if (item.SectionName != "")
                sqlSearch += " and c.SectionName like '%" + item.SectionName + "%'";
            if (item.CompanyID != null && item.CompanyID != string.Empty)
                sqlSearch += " and a.CompanyID = '" + item.CompanyID + "' ";

            sqlSearch += " and a.IsDeleted = 0";
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_TollGate a left join FM2E_Company b on a.CompanyID = b.CompanyID left join FM2E_Section c on a.SectionID = c.SectionID";
            searchTerm.ReturnFields = "TollGateID,a.CompanyID,b.CompanyName,a.SectionID,c.SectionName,TollGateName,a.OpenTime,a.Remark,a.PictureUrl,a.IsDeleted";
            //searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by CompanyName asc,SectionName asc,TollGateID desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            if (searchTerm.Where == "")
            {
                searchTerm.TableName = "FM2E_TollGate a left join FM2E_Company b on a.CompanyID = b.CompanyID left join FM2E_Section c on a.SectionID = c.SectionID";
                searchTerm.ReturnFields = "TollGateID,a.CompanyID,b.CompanyName,a.SectionID,c.SectionName,TollGateName,a.OpenTime,a.Remark,a.PictureUrl,a.IsDeleted";
                //searchTerm.PageSize = 10;
                searchTerm.OrderBy = "order by CompanyName asc,SectionName asc,TollGateID desc";
                searchTerm.Where = "where a.[IsDeleted]=0";
            }
            return SQLHelper.GetObjectList(this.GetData, searchTerm, out recordCount);
        }
        private TollGateInfo GetData(IDataReader rd)
        {
            TollGateInfo item = new TollGateInfo();

            if (!Convert.IsDBNull(rd["TollGateID"]))
                item.TollGateID = Convert.ToString(rd["TollGateID"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["CompanyName"]))
                item.CompanyName = Convert.ToString(rd["CompanyName"]);

            if (!Convert.IsDBNull(rd["SectionID"]))
                item.SectionID = Convert.ToString(rd["SectionID"]);

            if (!Convert.IsDBNull(rd["SectionName"]))
                item.SectionName = Convert.ToString(rd["SectionName"]);

            if (!Convert.IsDBNull(rd["TollGateName"]))
                item.TollGateName = Convert.ToString(rd["TollGateName"]);

            if (!Convert.IsDBNull(rd["OpenTime"]))
                item.OpenTime = Convert.ToDateTime(rd["OpenTime"]);
            if (item.OpenTime == SqlDateTime.MinValue)
                item.OpenTime = DateTime.MinValue;

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["PictureUrl"]))
                item.PictureUrl = Convert.ToString(rd["PictureUrl"]);

            if (!Convert.IsDBNull(rd["IsDeleted"]))
                item.IsDeleted = Convert.ToBoolean(rd["IsDeleted"]);
            return item;
        }
        public IList<TollGateInfo> GetAllTollGate()
        {
            List<TollGateInfo> list = new List<TollGateInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TollGateID,a.CompanyID,b.CompanyName,a.SectionID,c.SectionName,TollGateName,a.OpenTime,a.Remark,a.PictureUrl,a.IsDeleted from FM2E_TollGate a left join FM2E_Company b on a.CompanyID = b.CompanyID left join FM2E_Section c on a.SectionID = c.SectionID where a.IsDeleted = 0 order by CompanyName,SectionName,TollGateID desc");
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        list.Add(this.GetData(rd));
                    }
                }
            }
            catch
            {
                throw;
            }
            return list;
        }

        public IList GetAllTollGateByCompany(string CompanyID)
        {
            ArrayList list = new ArrayList();
            string strSql=string.Format("select TollGateID,a.CompanyID,b.CompanyName,a.SectionID,c.SectionName,TollGateName,a.OpenTime,a.Remark,a.PictureUrl,a.IsDeleted from FM2E_TollGate a left join FM2E_Company b on a.CompanyID = b.CompanyID left join FM2E_Section c on a.SectionID = c.SectionID where a.IsDeleted = 0 and a.CompanyID='{0}' order by CompanyName,SectionName,TollGateID desc",CompanyID);
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql, null))
                {
                    while (rd.Read())
                    {
                        list.Add(this.GetData(rd));
                    }
                }
            }
            catch
            {
                throw;
            }
            return list;
        }

        public TollGateInfo GetTollGate(string TollGateid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 TollGateID,a.CompanyID,b.CompanyName,a.SectionID,c.SectionName,TollGateName,a.OpenTime,a.Remark,a.PictureUrl,a.IsDeleted from FM2E_TollGate a left join FM2E_Company b on a.CompanyID = b.CompanyID left join FM2E_Section c on a.SectionID = c.SectionID");
            strSql.Append(" where a.IsDeleted=0 and TollGateID=@TollGateID ");
            SqlParameter[] parameters = {
					new SqlParameter("@TollGateID", SqlDbType.VarChar,50)};
            parameters[0].Value = TollGateid;
            TollGateInfo item = new TollGateInfo();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        item = this.GetData(rd);
                    }
                }
            }
            catch
            {
                throw;
            }
            return item;
        }

        public void DelTollGate(string TollGateID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_TollGate set IsDeleted=1");
                strSql.Append(" where TollGateID=@TollGateID ");
                SqlParameter[] parameters = {
					new SqlParameter("@TollGateID", SqlDbType.VarChar,50)};
                parameters[0].Value = TollGateID;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch
            {
                throw;
            }
        }

        public IList<TollGateInfo> Search(TollGateInfo whi)
        {
            string cmd = GenerateSearchSQL(whi);
            List<TollGateInfo> list = new List<TollGateInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                    {
                        list.Add(this.GetData(rd));
                    }
                }
            }
            catch
            {
                throw;
            }
            return list;
        }

        private string GenerateSearchSQL(TollGateInfo item)
        {
            string sqlSearch = "select TollGateID,a.CompanyID,b.CompanyName,a.SectionID,c.SectionName,TollGateName,a.OpenTime,a.Remark,a.PictureUrl,a.IsDeleted from FM2E_TollGate a left join FM2E_Company b on a.CompanyID = b.CompanyID left join FM2E_Section c on a.SectionID = c.SectionID where 1=1";

            if (item.TollGateID != "")
                sqlSearch += " and TollGateID like '%" + item.CompanyID + "%'";
            if (item.TollGateName != "")
                sqlSearch += " and TollGateName like '%" + item.TollGateName + "%'";
            if (item.CompanyName != "")
                sqlSearch += " and b.CompanyName like '%" + item.CompanyName + "%'";
            if (item.SectionName != "")
                sqlSearch += " and c.SectionName like '%" + item.SectionName + "%'";

            sqlSearch += " and a.IsDeleted=0  order by CompanyName,SectionName,TollGateID desc";
            return sqlSearch;
        }

        public void InsertTollGate(TollGateInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_TollGate(");
            strSql.Append("TollGateID,CompanyID,SectionID,TollGateName,OpenTime,Remark,PictureUrl,IsDeleted)");
            strSql.Append(" values (");
            strSql.Append("@TollGateID,@CompanyID,@SectionID,@TollGateName,@OpenTime,@Remark,@PictureUrl,@IsDeleted)");
            SqlParameter[] parameters = {
					new SqlParameter("@TollGateID", SqlDbType.VarChar,2),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@SectionID", SqlDbType.VarChar,2),
					new SqlParameter("@TollGateName", SqlDbType.NVarChar,20),
					new SqlParameter("@OpenTime", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1000),
					new SqlParameter("@PictureUrl", SqlDbType.VarChar,80),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1)};
            parameters[0].Value = model.TollGateID;
            parameters[1].Value = model.CompanyID;
            parameters[2].Value = model.SectionID;
            parameters[3].Value = model.TollGateName;
            parameters[4].Value = model.OpenTime == DateTime.MinValue ? SqlDateTime.MinValue : model.OpenTime;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.PictureUrl;
            parameters[7].Value = model.IsDeleted;
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("违反了 PRIMARY KEY 约束"))
                        throw new DuplicateException("插入重复的收费站编号", ex);
                    else
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void UpdateTollGate(TollGateInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_TollGate set ");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("SectionID=@SectionID,");
            strSql.Append("TollGateName=@TollGateName,");
            strSql.Append("OpenTime=@OpenTime,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("PictureUrl=@PictureUrl,");
            strSql.Append("IsDeleted=@IsDeleted");
            strSql.Append(" where TollGateID=@TollGateID ");
            SqlParameter[] parameters = {
					new SqlParameter("@TollGateID", SqlDbType.VarChar,2),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@SectionID", SqlDbType.VarChar,2),
					new SqlParameter("@TollGateName", SqlDbType.NVarChar,20),
					new SqlParameter("@OpenTime", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1000),
					new SqlParameter("@PictureUrl", SqlDbType.VarChar,80),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1)};
            parameters[0].Value = model.TollGateID;
            parameters[1].Value = model.CompanyID;
            parameters[2].Value = model.SectionID;
            parameters[3].Value = model.TollGateName;
            parameters[4].Value = model.OpenTime == DateTime.MinValue ? SqlDateTime.MinValue : model.OpenTime;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.PictureUrl;
            parameters[7].Value = model.IsDeleted;
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                    if (result == 0)
                        throw new Exception("没有更新任何数据项");
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}
