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
    public class Section:ISection
    {
        public QueryParam GenerateSearchTerm(SectionInfo item)
        {
            string sqlSearch = "where 1=1";
            if (item.SectionID != "")
                sqlSearch += " and a.SectionID like '%" + item.SectionID + "%'";
            if (item.CompanyID != "")
                sqlSearch += " and a.CompanyID ='" + item.CompanyID + "'";
            if (item.SectionName != "")
                sqlSearch += " and SectionName like '%" + item.SectionName + "%'";

            sqlSearch += " and a.IsDeleted = 0";
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_Section a left join FM2E_Company b on a.CompanyID=b.CompanyID";
            searchTerm.ReturnFields = "a.SectionID,a.CompanyID,a.SectionName,a.Length,a.OpenTime,a.Remark,a.PictureUrl,a.IsDeleted,b.CompanyName";
            //searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by CompanyName asc,SectionID desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            if (searchTerm.Where == "")
            {
                searchTerm.TableName = "FM2E_Section a left join FM2E_Company b on a.CompanyID=b.CompanyID";
                searchTerm.ReturnFields = "a.SectionID,a.CompanyID,a.SectionName,a.Length,a.OpenTime,a.Remark,a.PictureUrl,a.IsDeleted,b.CompanyName";
                //searchTerm.PageSize = 10;
                searchTerm.OrderBy = "order by CompanyName asc,SectionID desc";
                searchTerm.Where = "where a.[IsDeleted]=0";
            }
            return SQLHelper.GetObjectList(this.GetData, searchTerm, out recordCount);
        }
        private SectionInfo GetData(IDataReader rd)
        {
            SectionInfo item = new SectionInfo();

            if (!Convert.IsDBNull(rd["SectionID"]))
                item.SectionID = Convert.ToString(rd["SectionID"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["CompanyName"]))
                item.CompanyName = Convert.ToString(rd["CompanyName"]);

            if (!Convert.IsDBNull(rd["SectionName"]))
                item.SectionName = Convert.ToString(rd["SectionName"]);

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

        public IList<SectionInfo> GetAllSection()
        {
            List<SectionInfo> list = new List<SectionInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.SectionID,a.CompanyID,a.SectionName,a.Length,a.OpenTime,a.Remark,a.PictureUrl,a.IsDeleted,b.CompanyName ");
            strSql.Append(" FROM FM2E_Section a left join FM2E_Company b on a.CompanyID=b.CompanyID where a.IsDeleted=0 order by CompanyName,SectionID desc");
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

        public IList<SectionInfo> GetAllSectionByCompany(string companyid)
        {
            List<SectionInfo> list = new List<SectionInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.SectionID,a.CompanyID,a.SectionName,a.Length,a.OpenTime,a.Remark,a.PictureUrl,a.IsDeleted FROM FM2E_Section a");
            strSql.Append(" where a.IsDeleted=0 and CompanyID=@CompanyID ");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyID", SqlDbType.VarChar,50)};
            parameters[0].Value = companyid;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        
                        SectionInfo item = new SectionInfo();

                        if (!Convert.IsDBNull(rd["SectionID"]))
                            item.SectionID = Convert.ToString(rd["SectionID"]);

                        if (!Convert.IsDBNull(rd["CompanyID"]))
                            item.CompanyID = Convert.ToString(rd["CompanyID"]);

                        if (!Convert.IsDBNull(rd["SectionName"]))
                            item.SectionName = Convert.ToString(rd["SectionName"]);

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
                        list.Add(item);
                    }
                }
            }
            catch
            {
                throw;
            }
            return list;
        }

        public SectionInfo GetSection(string Sectionid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 a.SectionID,a.CompanyID,a.SectionName,a.Length,a.OpenTime,a.Remark,a.PictureUrl,a.IsDeleted,b.CompanyName FROM FM2E_Section a left join FM2E_Company b on a.CompanyID=b.CompanyID");
            strSql.Append(" where a.IsDeleted=0 and SectionID=@SectionID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SectionID", SqlDbType.VarChar,50)};
            parameters[0].Value = Sectionid;
            SectionInfo item = new SectionInfo();
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

        public void DelSection(string SectionID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_Section set IsDeleted=1");
                strSql.Append(" where SectionID=@SectionID ");
                SqlParameter[] parameters = {
					new SqlParameter("@SectionID", SqlDbType.VarChar,50)};
                parameters[0].Value = SectionID;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch
            {
                throw;
            }
        }

        public IList<SectionInfo> Search(SectionInfo whi)
        {
            string cmd = GenerateSearchSQL(whi);
            List<SectionInfo> list = new List<SectionInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                    {
                        SectionInfo item = new SectionInfo();

                        if (!Convert.IsDBNull(rd["SectionID"]))
                            item.SectionID = Convert.ToString(rd["SectionID"]);

                        if (!Convert.IsDBNull(rd["CompanyID"]))
                            item.CompanyID = Convert.ToString(rd["CompanyID"]);

                        if (!Convert.IsDBNull(rd["CompanyName"]))
                            item.CompanyName = Convert.ToString(rd["CompanyName"]);

                        if (!Convert.IsDBNull(rd["SectionName"]))
                            item.SectionName = Convert.ToString(rd["SectionName"]);

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

                        list.Add(item);
                    }
                }
            }
            catch
            {
                throw;
            }
            return list;
        }

        private string GenerateSearchSQL(SectionInfo item)
        {
            string sqlSearch = "select a.SectionID,a.CompanyID,a.SectionName,a.Length,a.OpenTime,a.Remark,a.PictureUrl,a.IsDeleted,b.CompanyName FROM FM2E_Section a left join FM2E_Company b on a.CompanyID=b.CompanyID where 1=1";

            if (item.SectionID != "")
                sqlSearch += " and a.SectionID like '%" + item.SectionID + "%'";
            if (item.CompanyName!= "")
                sqlSearch += " and b.CompanyName like '%" + item.CompanyName + "%'";
            if (item.SectionName != "")
                sqlSearch += " and SectionName like '%" + item.SectionName + "%'";

            sqlSearch += " and a.IsDeleted = 0 order by CompanyName,SectionID desc";
            return sqlSearch;
        }

        public void InsertSection(SectionInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_Section(");
            strSql.Append("SectionID,CompanyID,SectionName,Length,OpenTime,Remark,PictureUrl,IsDeleted)");
            strSql.Append(" values (");
            strSql.Append("@SectionID,@CompanyID,@SectionName,@Length,@OpenTime,@Remark,@PictureUrl,@IsDeleted)");
            SqlParameter[] parameters = {
					new SqlParameter("@SectionID", SqlDbType.VarChar,2),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@SectionName", SqlDbType.NVarChar,20),
					new SqlParameter("@Length", SqlDbType.Decimal,5),
					new SqlParameter("@OpenTime", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1000),
					new SqlParameter("@PictureUrl", SqlDbType.VarChar,80),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1)};
            parameters[0].Value = model.SectionID;
            parameters[1].Value = model.CompanyID;
            parameters[2].Value = model.SectionName;
            parameters[3].Value = model.Length;
            parameters[4].Value = model.OpenTime;
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
                        throw new DuplicateException("插入重复的路段编号", ex);
                    else
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void UpdateSection(SectionInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_Section set ");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("SectionName=@SectionName,");
            strSql.Append("Length=@Length,");
            strSql.Append("OpenTime=@OpenTime,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("PictureUrl=@PictureUrl,");
            strSql.Append("IsDeleted=@IsDeleted");
            strSql.Append(" where SectionID=@SectionID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SectionID", SqlDbType.VarChar,2),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@SectionName", SqlDbType.NVarChar,20),
					new SqlParameter("@Length", SqlDbType.Decimal,5),
					new SqlParameter("@OpenTime", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1000),
					new SqlParameter("@PictureUrl", SqlDbType.VarChar,80),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1)};
            parameters[0].Value = model.SectionID;
            parameters[1].Value = model.CompanyID;
            parameters[2].Value = model.SectionName;
            parameters[3].Value = model.Length;
            parameters[4].Value = model.OpenTime;
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
