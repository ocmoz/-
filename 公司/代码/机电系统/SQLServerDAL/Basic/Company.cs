﻿using System;
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
    public class Company : ICompany
    {
        private const string SELECT_ALLCOMPANY = "select * from FM2E_Company where [IsDeleted]=0 order by CompanyID ";

        private const string SELECT_COMPANY = "select [CompanyID],[CompanyName],[Address],[Contact],[Phone],[Website],[Email],[Fax],[Remark],[PictureUrl],[IsParentCompany],[IsDeleted] from FM2E_Company where [CompanyID]='{0}' and [IsDeleted] = 0 ";

        private const string INSERT_COMPANY = "insert into FM2E_Company([CompanyID],[CompanyName],[Address],[Contact],[Phone],[Website],[Email],[Fax],[Remark],[PictureUrl],[IsParentCompany],[IsDeleted]) "
                                            + "values(@CompanyID,@CompanyName,@Address,@Contact,@Phone,@Website,@Email,@Fax,@Remark,@PictureUrl,@IsParentCompany,@IsDeleted)";

        private const string UPDATE_COMPANY = "update FM2E_Company "
                                            + "set [CompanyName]=@CompanyName,[Address]=@Address,[Contact]=@Contact,[Phone]=@Phone,[Website]=@Website,[Email]=@Email,[Fax]=@Fax,[Remark]=@Remark,[PictureUrl]=@PictureUrl,[IsParentCompany]=@IsParentCompany,[IsDeleted]=@IsDeleted where [CompanyID]=@CompanyID";

        private const string DEL_COMPANY = "update FM2E_Company set [IsDeleted]=1 where [CompanyID]='{0}'";


        private const string TABLE_NAME = "FM2E_Company";

        private CompanyInfo GetData(IDataReader rd)
        {
            CompanyInfo item = new CompanyInfo();
            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["CompanyName"]))
                item.CompanyName = Convert.ToString(rd["CompanyName"]);

            if (!Convert.IsDBNull(rd["Address"]))
                item.Address = Convert.ToString(rd["Address"]);

            if (!Convert.IsDBNull(rd["Contact"]))
                item.Contact = Convert.ToString(rd["Contact"]);

            if (!Convert.IsDBNull(rd["Phone"]))
                item.Phone = Convert.ToString(rd["Phone"]);

            if (!Convert.IsDBNull(rd["Website"]))
                item.Website = Convert.ToString(rd["Website"]);

            if (!Convert.IsDBNull(rd["Email"]))
                item.Email = Convert.ToString(rd["Email"]);

            if (!Convert.IsDBNull(rd["Fax"]))
                item.Fax = Convert.ToString(rd["Fax"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["PictureUrl"]))
                item.PictureUrl = Convert.ToString(rd["PictureUrl"]);

            if (!Convert.IsDBNull(rd["IsParentCompany"]))
                item.IsParentCompany = Convert.ToBoolean(rd["IsParentCompany"]);

            if (!Convert.IsDBNull(rd["IsDeleted"]))
                item.IsDeleted = Convert.ToBoolean(rd["IsDeleted"]);

            return item;
        }

        public IList<CompanyInfo> GetAllCompany()
        {
            List<CompanyInfo> list = new List<CompanyInfo>();

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, SELECT_ALLCOMPANY, null))
                {
                    while (rd.Read())
                    {
                        CompanyInfo item = new CompanyInfo();
                        if (!Convert.IsDBNull(rd["CompanyID"]))
                            item.CompanyID = Convert.ToString(rd["CompanyID"]);

                        if (!Convert.IsDBNull(rd["CompanyName"]))
                            item.CompanyName = Convert.ToString(rd["CompanyName"]);

                        if (!Convert.IsDBNull(rd["Address"]))
                            item.Address = Convert.ToString(rd["Address"]);

                        if (!Convert.IsDBNull(rd["Contact"]))
                            item.Contact = Convert.ToString(rd["Contact"]);

                        if (!Convert.IsDBNull(rd["Phone"]))
                            item.Phone = Convert.ToString(rd["Phone"]);

                        if (!Convert.IsDBNull(rd["Website"]))
                            item.Website = Convert.ToString(rd["Website"]);

                        if (!Convert.IsDBNull(rd["Email"]))
                            item.Email = Convert.ToString(rd["Email"]);

                        if (!Convert.IsDBNull(rd["Fax"]))
                            item.Fax = Convert.ToString(rd["Fax"]);

                        if (!Convert.IsDBNull(rd["Remark"]))
                            item.Remark = Convert.ToString(rd["Remark"]);

                        if (!Convert.IsDBNull(rd["PictureUrl"]))
                            item.PictureUrl = Convert.ToString(rd["PictureUrl"]);

                        if (!Convert.IsDBNull(rd["IsParentCompany"]))
                            item.IsParentCompany = Convert.ToBoolean(rd["IsParentCompany"]);

                        if (!Convert.IsDBNull(rd["IsDeleted"]))
                            item.IsDeleted = Convert.ToBoolean(rd["IsDeleted"]);

                        list.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取所有公司信息失败",e);
            }
            return list;
        }

        public CompanyInfo GetCompany(string id)
        {
            string cmd = string.Format(SELECT_COMPANY, id);
            CompanyInfo item = null;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                    {
                        item = new CompanyInfo();

                        if (!Convert.IsDBNull(rd["CompanyID"]))
                            item.CompanyID = Convert.ToString(rd["CompanyID"]);

                        if (!Convert.IsDBNull(rd["CompanyName"]))
                            item.CompanyName = Convert.ToString(rd["CompanyName"]);

                        if (!Convert.IsDBNull(rd["Address"]))
                            item.Address = Convert.ToString(rd["Address"]);

                        if (!Convert.IsDBNull(rd["Contact"]))
                            item.Contact = Convert.ToString(rd["Contact"]);

                        if (!Convert.IsDBNull(rd["Phone"]))
                            item.Phone = Convert.ToString(rd["Phone"]);

                        if (!Convert.IsDBNull(rd["Website"]))
                            item.Website = Convert.ToString(rd["Website"]);

                        if (!Convert.IsDBNull(rd["Email"]))
                            item.Email = Convert.ToString(rd["Email"]);

                        if (!Convert.IsDBNull(rd["Fax"]))
                            item.Fax = Convert.ToString(rd["Fax"]);

                        if (!Convert.IsDBNull(rd["Remark"]))
                            item.Remark = Convert.ToString(rd["Remark"]);

                        if (!Convert.IsDBNull(rd["PictureUrl"]))
                            item.PictureUrl = Convert.ToString(rd["PictureUrl"]);

                        if (!Convert.IsDBNull(rd["IsParentCompany"]))
                            item.IsParentCompany = Convert.ToBoolean(rd["IsParentCompany"]);

                        if (!Convert.IsDBNull(rd["IsDeleted"]))
                            item.IsDeleted = Convert.ToBoolean(rd["IsDeleted"]);
                    }
                }

            }
            catch (Exception e)
            {
                throw new DALException("获取公司信息失败",e);
            }
            return item;
        }

        public void InsertCompany(CompanyInfo item)
        {
            SqlParameter[] param = GetInsertUpdateParam();
            param[0].Value = item.CompanyID;
            param[1].Value = item.PictureUrl;
            param[2].Value = item.IsParentCompany;
            param[3].Value = item.IsDeleted;
            param[4].Value = item.CompanyName;
            param[5].Value = item.Address;
            param[6].Value = item.Contact;
            param[7].Value = item.Phone;
            param[8].Value = item.Website;
            param[9].Value = item.Email;
            param[10].Value = item.Fax;
            param[11].Value = item.Remark;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, INSERT_COMPANY, param);
                }
                catch (Exception e)
                {
                    throw new DALException("添加公司失败",e);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void UpdateCompany(CompanyInfo item)
        {
            SqlParameter[] param = GetInsertUpdateParam();
            param[0].Value = item.CompanyID;
            param[1].Value = item.PictureUrl;
            param[2].Value = item.IsParentCompany;
            param[3].Value = item.IsDeleted;
            param[4].Value = item.CompanyName;
            param[5].Value = item.Address;
            param[6].Value = item.Contact;
            param[7].Value = item.Phone;
            param[8].Value = item.Website;
            param[9].Value = item.Email;
            param[10].Value = item.Fax;
            param[11].Value = item.Remark;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, UPDATE_COMPANY, param);
                    if (result == 0)
                        throw new Exception("没有更新任何数据项");
                }
                catch (Exception e)
                {
                    throw new DALException("更新公司信息失败",e);
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        private static SqlParameter[] GetInsertUpdateParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(INSERT_COMPANY);
            if (param == null)
            {
                param = new SqlParameter[]{
                    new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@PictureUrl", SqlDbType.VarChar,80),
					new SqlParameter("@IsParentCompany", SqlDbType.Bit,1),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
					new SqlParameter("@CompanyName", SqlDbType.NVarChar,20),
					new SqlParameter("@Address", SqlDbType.NVarChar,100),
					new SqlParameter("@Contact", SqlDbType.NVarChar,5),
					new SqlParameter("@Phone", SqlDbType.VarChar,20),
					new SqlParameter("@Website", SqlDbType.VarChar,50),
					new SqlParameter("@Email", SqlDbType.VarChar,30),
					new SqlParameter("@Fax", SqlDbType.VarChar,20),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1000)
                };
                SQLHelper.CacheParameters(INSERT_COMPANY, param);
            }
            return param;
        }

        public void DelCompany(string id)
        {
            string cmd = string.Format(DEL_COMPANY, id);
            try
            {
                int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, cmd, null);
                if (rows == 0)
                    throw new Exception("没有删除任何数据项！");
            }
            catch (Exception e)
            {
                throw new DALException("删除公司信息失败",e);
            }
        }

        public List<CompanyInfo> Search(CompanyInfo item)
        {
            string cmd = GenerateSearchSQL(item);
            List<CompanyInfo> list = new List<CompanyInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                    {
                        CompanyInfo Company = new CompanyInfo();

                        if (!Convert.IsDBNull(rd["CompanyID"]))
                            Company.CompanyID = Convert.ToString(rd["CompanyID"]);

                        if (!Convert.IsDBNull(rd["CompanyName"]))
                            Company.CompanyName = Convert.ToString(rd["CompanyName"]);

                        if (!Convert.IsDBNull(rd["Address"]))
                            Company.Address = Convert.ToString(rd["Address"]);

                        if (!Convert.IsDBNull(rd["Contact"]))
                            Company.Contact = Convert.ToString(rd["Contact"]);

                        if (!Convert.IsDBNull(rd["Phone"]))
                            Company.Phone = Convert.ToString(rd["Phone"]);

                        if (!Convert.IsDBNull(rd["Website"]))
                            Company.Website = Convert.ToString(rd["Website"]);

                        if (!Convert.IsDBNull(rd["Email"]))
                            Company.Email = Convert.ToString(rd["Email"]);

                        if (!Convert.IsDBNull(rd["Fax"]))
                            Company.Fax = Convert.ToString(rd["Fax"]);

                        if (!Convert.IsDBNull(rd["Remark"]))
                            Company.Remark = Convert.ToString(rd["Remark"]);

                        if (!Convert.IsDBNull(rd["PictureUrl"]))
                            Company.PictureUrl = Convert.ToString(rd["PictureUrl"]);

                        if (!Convert.IsDBNull(rd["IsParentCompany"]))
                            Company.IsParentCompany = Convert.ToBoolean(rd["IsParentCompany"]);

                        if (!Convert.IsDBNull(rd["IsDeleted"]))
                            Company.IsDeleted = Convert.ToBoolean(rd["IsDeleted"]);

                        list.Add(Company);
                    }
                }

            }
            catch(Exception e)
            {
                throw new DALException("搜索公司信息失败",e);
            }
            return list;

        }

         public List<CompanyInfo> Search(string companyName)
        {
            string cmd = "select * from FM2E_Company where [IsDeleted]=0 and CompanyName in(" + companyName + ") order by CompanyID"; ;
            List<CompanyInfo> list = new List<CompanyInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                    {
                        CompanyInfo Company = new CompanyInfo();

                        if (!Convert.IsDBNull(rd["CompanyID"]))
                            Company.CompanyID = Convert.ToString(rd["CompanyID"]);

                        if (!Convert.IsDBNull(rd["CompanyName"]))
                            Company.CompanyName = Convert.ToString(rd["CompanyName"]);

                        if (!Convert.IsDBNull(rd["Address"]))
                            Company.Address = Convert.ToString(rd["Address"]);

                        if (!Convert.IsDBNull(rd["Contact"]))
                            Company.Contact = Convert.ToString(rd["Contact"]);

                        if (!Convert.IsDBNull(rd["Phone"]))
                            Company.Phone = Convert.ToString(rd["Phone"]);

                        if (!Convert.IsDBNull(rd["Website"]))
                            Company.Website = Convert.ToString(rd["Website"]);

                        if (!Convert.IsDBNull(rd["Email"]))
                            Company.Email = Convert.ToString(rd["Email"]);

                        if (!Convert.IsDBNull(rd["Fax"]))
                            Company.Fax = Convert.ToString(rd["Fax"]);

                        if (!Convert.IsDBNull(rd["Remark"]))
                            Company.Remark = Convert.ToString(rd["Remark"]);

                        if (!Convert.IsDBNull(rd["PictureUrl"]))
                            Company.PictureUrl = Convert.ToString(rd["PictureUrl"]);

                        if (!Convert.IsDBNull(rd["IsParentCompany"]))
                            Company.IsParentCompany = Convert.ToBoolean(rd["IsParentCompany"]);

                        if (!Convert.IsDBNull(rd["IsDeleted"]))
                            Company.IsDeleted = Convert.ToBoolean(rd["IsDeleted"]);

                        list.Add(Company);
                    }
                }

            }
            catch(Exception e)
            {
                throw new DALException("搜索公司信息失败",e);
            }
            return list;

        }

        private string GenerateSearchSQL(CompanyInfo item)
        {
            string sqlSearch = "select * from FM2E_Company where [IsDeleted]=0 ";

           /* if (item.CompanyID >= 1)
            {
                sqlSearch += " and [CompanyID] =" + item.CompanyID;
            }*/
           
            if (item.CompanyName!=null&& item.CompanyName != "")
            {
                sqlSearch += " and [CompanyName] = '" + item.CompanyName + "'";
            }
            if (item.IsParentCompany != null)
            {
                int isparentcompany = (item.IsParentCompany == true) ? 1 : 0;
                sqlSearch += " and [IsParentCompany] = " + isparentcompany + " ";
            }

            sqlSearch += " order by CompanyID ";
       

            return sqlSearch;
        }



        public QueryParam GenerateSearchTerm(CompanyInfo item)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = "FM2E_Company";
            qp.ReturnFields = "*";
            qp.OrderBy = " order by CompanyID desc ";

            string sqlSearch = "where [IsDeleted]=0 ";
            if (item.CompanyID != null && item.CompanyID.Trim() != string.Empty)
            {
                sqlSearch += " and CompanyID like '%" + item.CompanyID.Trim() + "%'";
            }
            if (item.CompanyName != null && item.CompanyName.Trim() != string.Empty)
            {
                sqlSearch += " and CompanyName like '%" + item.CompanyName.Trim() + "%'";
            }
            qp.Where = sqlSearch;
            return qp;
        }
        public IList GetList(QueryParam term, out int recordCount)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = "FM2E_Company";
                    term.ReturnFields = "*";
                    term.OrderBy = " order by CompanyID desc ";
                    term.Where = "where [IsDeleted]=0 ";
                }
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取用户分页失败", e);
            }
        }

    }
}
