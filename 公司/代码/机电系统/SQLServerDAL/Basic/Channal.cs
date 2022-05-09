using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Basic;
using FM2E.IDAL.Basic;
using FM2E.Model.Utils;
using System.Collections;
using FM2E.Model.Exceptions;

namespace FM2E.SQLServerDAL.Basic
{
    public class Channal:IChannal
    {
        public QueryParam GenerateSearchTerm(ChannalInfo item)
        {
            string sqlSearch = "where 1=1";
            if (item.ChannalID != "")
                sqlSearch += " and ChannalID like '%" + item.ChannalID + "%'";
            if (item.ChannalName != "")
                sqlSearch += " and ChannalName like '%" + item.ChannalName + "%'";
            if (item.CompanyName != "")
                sqlSearch += " and b.CompanyName like '%" + item.CompanyName + "%'";
            if (item.SectionName != "")
                sqlSearch += " and c.SectionName like '%" + item.SectionName + "%'";
            if (item.CompanyID != null && item.CompanyID != string.Empty)
                sqlSearch += " and a.CompanyID = '" + item.CompanyID + "' ";

            sqlSearch += " and a.IsDeleted=0";
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_Channal a left join FM2E_Company b on a.CompanyID = b.CompanyID left join FM2E_Section c on a.SectionID = c.SectionID";
            searchTerm.ReturnFields = "ChannalID,a.CompanyID,b.CompanyName,a.SectionID,c.SectionName,ChannalName,a.Length,a.OpenTime,a.Remark,a.PictureUrl,a.IsDeleted";
            //searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by CompanyName asc,SectionName asc,ChannalID desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        public IList GetList(QueryParam searchTerm,out int recordCount)
        {
            if (searchTerm.Where=="")
            {
                searchTerm.TableName = "FM2E_Channal a left join FM2E_Company b on a.CompanyID = b.CompanyID left join FM2E_Section c on a.SectionID = c.SectionID";
                searchTerm.ReturnFields = "ChannalID,a.CompanyID,b.CompanyName,a.SectionID,c.SectionName,ChannalName,a.Length,a.OpenTime,a.Remark,a.PictureUrl,a.IsDeleted";
                //searchTerm.PageSize = 10;
                searchTerm.OrderBy = "order by CompanyName asc,SectionName asc,ChannalID desc";
                searchTerm.Where = "where a.[IsDeleted]=0";
            }
            return SQLHelper.GetObjectList(this.GetData, searchTerm, out recordCount);
        }
        private ChannalInfo GetData(IDataReader rd)
        {
            ChannalInfo item = new ChannalInfo();

            if (!Convert.IsDBNull(rd["ChannalID"]))
                item.ChannalID = Convert.ToString(rd["ChannalID"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["CompanyName"]))
                item.CompanyName = Convert.ToString(rd["CompanyName"]);

            if (!Convert.IsDBNull(rd["SectionID"]))
                item.SectionID = Convert.ToString(rd["SectionID"]);

            if (!Convert.IsDBNull(rd["SectionName"]))
                item.SectionName = Convert.ToString(rd["SectionName"]);

            if (!Convert.IsDBNull(rd["ChannalName"]))
                item.ChannalName = Convert.ToString(rd["ChannalName"]);

            if (!Convert.IsDBNull(rd["Length"]))
                item.Length = Convert.ToDecimal(rd["Length"]);

            if (!Convert.IsDBNull(rd["OpenTime"]))
                item.OpenTime = Convert.ToDateTime(rd["OpenTime"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["PictureUrl"]))
                item.PictureUrl = Convert.ToString(rd["PictureUrl"]);

            if (!Convert.IsDBNull(rd["IsDeleted"]))
                item.IsDeleted = Convert.ToBoolean(rd["IsDeleted"]);
            return item;

        }
        public IList<ChannalInfo> GetAllChannal()
        {
            List<ChannalInfo> list = new List<ChannalInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ChannalID,a.CompanyID,b.CompanyName,a.SectionID,c.SectionName,ChannalName,a.Length,a.OpenTime,a.Remark,a.PictureUrl,a.IsDeleted from FM2E_Channal a left join FM2E_Company b on a.CompanyID = b.CompanyID left join FM2E_Section c on a.SectionID = c.SectionID where a.IsDeleted=0 order by CompanyName,SectionName,ChannalID desc");
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

        public IList GetAllChannalByCompany(string CompanyID)
        {
            ArrayList list = new ArrayList();
            
            string strSql=string.Format("select ChannalID,a.CompanyID,b.CompanyName,a.SectionID,c.SectionName,ChannalName,a.Length,a.OpenTime,a.Remark,a.PictureUrl,a.IsDeleted from FM2E_Channal a left join FM2E_Company b on a.CompanyID = b.CompanyID left join FM2E_Section c on a.SectionID = c.SectionID where a.IsDeleted=0 and a.CompanyID='{0}' order by CompanyName,SectionName,ChannalID desc",CompanyID);
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

        public ChannalInfo GetChannal(string Channalid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ChannalID,a.CompanyID,b.CompanyName,a.SectionID,c.SectionName,ChannalName,a.Length,a.OpenTime,a.Remark,a.PictureUrl,a.IsDeleted from FM2E_Channal a left join FM2E_Company b on a.CompanyID = b.CompanyID left join FM2E_Section c on a.SectionID = c.SectionID");
            strSql.Append(" where a.IsDeleted=0 and ChannalID=@ChannalID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ChannalID", SqlDbType.VarChar,50)};
            parameters[0].Value = Channalid;
            ChannalInfo item = new ChannalInfo();
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

        public void DelChannal(string ChannalID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_Channal set IsDeleted=1");
                strSql.Append(" where ChannalID=@ChannalID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ChannalID", SqlDbType.VarChar,50)};
                parameters[0].Value = ChannalID;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch
            {
                throw;
            }
        }

        public IList<ChannalInfo> Search(ChannalInfo whi)
        {
            string cmd = GenerateSearchSQL(whi);
            List<ChannalInfo> list = new List<ChannalInfo>();
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

        private string GenerateSearchSQL(ChannalInfo item)
        {
            string sqlSearch = "select ChannalID,a.CompanyID,b.CompanyName,a.SectionID,c.SectionName,ChannalName,a.Length,a.OpenTime,a.Remark,a.PictureUrl,a.IsDeleted from FM2E_Channal a left join FM2E_Company b on a.CompanyID = b.CompanyID left join FM2E_Section c on a.SectionID = c.SectionID where 1=1";

            if (item.ChannalID != "")
                sqlSearch += " and ChannalID like '%" + item.ChannalID + "%'";
            if (item.ChannalName != "")
                sqlSearch += " and ChannalName like '%" + item.ChannalName + "%'";
            if (item.CompanyName != "")
                sqlSearch += " and b.CompanyName like '%" + item.CompanyName + "%'";
            if (item.SectionName != "")
                sqlSearch += " and c.SectionName like '%" + item.SectionName + "%'";

            sqlSearch += " and a.IsDeleted=0 order by CompanyName,SectionName,ChannalID desc";
            return sqlSearch;
        }

        public void InsertChannal(ChannalInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_Channal(");
            strSql.Append("ChannalID,CompanyID,SectionID,ChannalName,Length,OpenTime,Remark,PictureUrl,IsDeleted)");
            strSql.Append(" values (");
            strSql.Append("@ChannalID,@CompanyID,@SectionID,@ChannalName,@Length,@OpenTime,@Remark,@PictureUrl,@IsDeleted)");
            SqlParameter[] parameters = {
					new SqlParameter("@ChannalID", SqlDbType.VarChar,2),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@SectionID", SqlDbType.VarChar,2),
					new SqlParameter("@ChannalName", SqlDbType.NVarChar,20),
					new SqlParameter("@Length", SqlDbType.Decimal,5),
					new SqlParameter("@OpenTime", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1000),
					new SqlParameter("@PictureUrl", SqlDbType.VarChar,80),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1)};
            parameters[0].Value = model.ChannalID;
            parameters[1].Value = model.CompanyID;
            parameters[2].Value = model.SectionID;
            parameters[3].Value = model.ChannalName;
            parameters[4].Value = model.Length;
            parameters[5].Value = model.OpenTime;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.PictureUrl;
            parameters[8].Value = model.IsDeleted;
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch(Exception ex)
                {
                    if (ex.Message.Contains("违反了 PRIMARY KEY 约束"))
                        throw new DuplicateException("插入重复的隧道编号", ex);
                    else
                        throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void UpdateChannal(ChannalInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_Channal set ");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("SectionID=@SectionID,");
            strSql.Append("ChannalName=@ChannalName,");
            strSql.Append("Length=@Length,");
            strSql.Append("OpenTime=@OpenTime,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("PictureUrl=@PictureUrl,");
            strSql.Append("IsDeleted=@IsDeleted");
            strSql.Append(" where ChannalID=@ChannalID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ChannalID", SqlDbType.VarChar,2),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@SectionID", SqlDbType.VarChar,2),
					new SqlParameter("@ChannalName", SqlDbType.NVarChar,20),
					new SqlParameter("@Length", SqlDbType.Decimal,5),
					new SqlParameter("@OpenTime", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1000),
					new SqlParameter("@PictureUrl", SqlDbType.VarChar,80),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1)};
            parameters[0].Value = model.ChannalID;
            parameters[1].Value = model.CompanyID;
            parameters[2].Value = model.SectionID;
            parameters[3].Value = model.ChannalName;
            parameters[4].Value = model.Length;
            parameters[5].Value = model.OpenTime;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.PictureUrl;
            parameters[8].Value = model.IsDeleted;
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
