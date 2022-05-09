using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;
using FM2E.IDAL.Basic;
using FM2E.Model.Basic;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.SQLServerDAL.Basic
{
    public class Supplier : ISupplier
    {
        private const string SELECT_ALLSupplier = "select * from FM2E_Supplier where [IsDeleted]=0 order by SupplierID desc";

        private const string SELECT_Supplier = "select * from FM2E_Supplier where [SupplierID]='{0}'";

        private const string INSERT_Supplier = "insert into FM2E_Supplier([Name],[Product],[Address],[Phone],[Fax],[Email],[HomePage],[ResName],[ResPhone],[Remark],[IsDeleted],[Credit]) "
                                            + "values(@Name,@Product,@Address,@Phone,@Fax,@Email,@HomePage,@ResName,@ResPhone,@Remark,@IsDeleted,@Credit)";

        private const string UPDATE_Supplier = "update FM2E_Supplier "
                                            + "set [Name]=@Name,[Product]=@Product,[Address]=@Address,[Phone]=@Phone,[Fax]=@Fax,[Email]=@Email,[HomePage]=@HomePage,[ResName]=@ResName,[ResPhone]=@ResPhone,[Remark]=@Remark,[IsDeleted]=@IsDeleted,[Credit]=@Credit where [SupplierID]=@SupplierID";

        private const string DEL_Supplier = "update FM2E_Supplier set [IsDeleted]=1 where [SupplierID]='{0}'";

        private const string PARAM_SupplierID = "@SupplierID";
        private const string PARAM_NAME = "@Name";
        private const string PARAM_PRODUCT = "@Product";
        private const string PARAM_ADDRESS = "@Address";
        private const string PARAM_PHONE = "@Phone";
        private const string PARAM_FAX = "@Fax";
        private const string PARAM_EMAIL = "@Email";
        private const string PARAM_HOMEPAGE = "@HomePage";
        private const string PARAM_RESNAME = "@ResName";
        private const string PARAM_RESPHONE = "@ResPhone";
        private const string PARAM_REMARK = "@Remark";
        private const string PARAM_ISDELETED = "@IsDeleted";
        private const string PARAM_CREDIT = "@Credit";

        private const string TABLE_NAME = "FM2E_Supplier";

        public IList<SupplierInfo> GetAllSupplier()
        {
            List<SupplierInfo> list = new List<SupplierInfo>();

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, SELECT_ALLSupplier, null))
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

        public SupplierInfo GetSupplier(long id)
        {
            string cmd = string.Format(SELECT_Supplier, id);
            SupplierInfo item = null;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                    {
                        item = this.GetData(rd);
                        break;
                    }
                }

            }
            catch
            {
                throw;
            }
            return item;
        }

        public void InsertSupplier(SupplierInfo item)
        {
            SqlParameter[] param = GetInsertUpdateParam();
            param[0].Value = 0;
            param[1].Value = item.Name;
            param[2].Value = item.Product;
            param[3].Value = item.Address;
            param[4].Value = item.Phone;
            param[5].Value = item.Fax;
            param[6].Value = item.Email;
            param[7].Value = item.HomePage;
            param[8].Value = item.ResName;
            param[9].Value = item.ResPhone;
            param[10].Value = item.Remark;
            param[11].Value = item.IsDeleted;
            param[12].Value = item.Credit;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, INSERT_Supplier, param);
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
        public void UpdateSupplier(SupplierInfo item)
        {
            SqlParameter[] param = GetInsertUpdateParam();
            param[0].Value = item.SupplierID;
            param[1].Value = item.Name;
            param[2].Value = item.Product;
            param[3].Value = item.Address;
            param[4].Value = item.Phone;
            param[5].Value = item.Fax;
            param[6].Value = item.Email;
            param[7].Value = item.HomePage;
            param[8].Value = item.ResName;
            param[9].Value = item.ResPhone;
            param[10].Value = item.Remark;
            param[11].Value = item.IsDeleted;
            param[12].Value = item.Credit;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, UPDATE_Supplier, param);
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

        private static SqlParameter[] GetInsertUpdateParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(INSERT_Supplier);
            if (param == null)
            {
                param = new SqlParameter[]{
                    new SqlParameter(PARAM_SupplierID,SqlDbType.BigInt,8),
                    new SqlParameter(PARAM_NAME,SqlDbType.NVarChar,30),
                    new SqlParameter(PARAM_PRODUCT,SqlDbType.NVarChar,500),
                    new SqlParameter(PARAM_ADDRESS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PHONE,SqlDbType.VarChar,20),
                    new SqlParameter(PARAM_FAX,SqlDbType.VarChar,20),
                    new SqlParameter(PARAM_EMAIL,SqlDbType.VarChar,30),
                    new SqlParameter(PARAM_HOMEPAGE,SqlDbType.VarChar,100),
                    new SqlParameter(PARAM_RESNAME,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_RESPHONE,SqlDbType.VarChar,20),
                    new SqlParameter(PARAM_REMARK,SqlDbType.NVarChar,100),
                    new SqlParameter(PARAM_ISDELETED,SqlDbType.Bit,1),
                    new SqlParameter(PARAM_CREDIT,SqlDbType.Int,4)
                };
                SQLHelper.CacheParameters(INSERT_Supplier, param);
            }
            return param;
        }

        public void DelSupplier(long id)
        {
            string cmd = string.Format(DEL_Supplier, id);
            try
            {
                int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, cmd, null);
                if (rows == 0)
                    throw new Exception("没有删除任何数据项！");
            }
            catch
            {
                throw;
            }
        }

        public IList<SupplierInfo> Search(SupplierInfo item)
        {
            string cmd = GenerateSearchSQL(item);

            List<SupplierInfo> list = new List<SupplierInfo>();

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


        private string GenerateSearchSQL(SupplierInfo item)
        {
            string sqlSearch = "select * from FM2E_Supplier where [IsDeleted]=0 ";

            //if (item.PositionID >= 1)
            //{
            //    sqlSearch += " and [PositionID] =" + item.PositionID;
            //}

            //if (item.PositionName != "")
            //{
            //    sqlSearch += " and [PositionName] like '%" + item.PositionName + "%'";
            //}
            if (item.Name != "")
            {
                sqlSearch += " and [Name] like '%" + item.Name + "%'";
            }
            if (item.Credit != 0)
            {
                sqlSearch += " and [Credit] like '%" + item.Credit + "%'";
            }
            if (item.Product != "")
            {
                sqlSearch += " and [Product] like '%" + item.Product + "%'";
            }
            if (item.Address != "")
            {
                sqlSearch += " and [Address] like '%" + item.Address + "%'";
            }
            if (item.ResName != "")
            {
                sqlSearch += " and [ResName] like '%" + item.ResName + "%'";
            }
            sqlSearch += " order by SupplierID desc";
            return sqlSearch;
        }
        public QueryParam GenerateSearchTerm(SupplierInfo item)
        {
            string sqlSearch = "where 1=1";
            if (item.Name != "")
            {
                sqlSearch += " and [Name] like '%" + item.Name + "%'";
            }
            if (item.Credit != 0)
            {
                sqlSearch += " and [Credit] like '%" + item.Credit + "%'";
            }
            if (item.Product != "")
            {
                sqlSearch += " and [Product] like '%" + item.Product + "%'";
            }
            if (item.Address != "")
            {
                sqlSearch += " and [Address] like '%" + item.Address + "%'";
            }
            if (item.ResName != "")
            {
                sqlSearch += " and [ResName] like '%" + item.ResName + "%'";
            }
            sqlSearch += " and IsDeleted=0";
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_Supplier";
            searchTerm.ReturnFields = "*";
            //searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by SupplierID desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            if (searchTerm.Where == "")
            {
                searchTerm.TableName = "FM2E_Supplier";
                searchTerm.ReturnFields = "*";
                //searchTerm.PageSize = 10;
                searchTerm.OrderBy = "order by SupplierID desc";
                searchTerm.Where = "where [IsDeleted]=0";
            }
            return SQLHelper.GetObjectList(this.GetData, searchTerm, out recordCount);
        }
        private SupplierInfo GetData(IDataReader rd)
        {
            SupplierInfo it = new SupplierInfo();
            if (!Convert.IsDBNull(rd["SupplierID"]))
                it.SupplierID = Convert.ToInt64(rd["SupplierID"]);

            if (!Convert.IsDBNull(rd["Name"]))
                it.Name = Convert.ToString(rd["Name"]);

            if (!Convert.IsDBNull(rd["Product"]))
                it.Product = Convert.ToString(rd["Product"]);

            if (!Convert.IsDBNull(rd["Address"]))
                it.Address = Convert.ToString(rd["Address"]);

            if (!Convert.IsDBNull(rd["Phone"]))
                it.Phone = Convert.ToString(rd["Phone"]);

            if (!Convert.IsDBNull(rd["Fax"]))
                it.Fax = Convert.ToString(rd["Fax"]);

            if (!Convert.IsDBNull(rd["Email"]))
                it.Email = Convert.ToString(rd["Email"]);

            if (!Convert.IsDBNull(rd["HomePage"]))
                it.HomePage = Convert.ToString(rd["HomePage"]);

            if (!Convert.IsDBNull(rd["ResName"]))
                it.ResName = Convert.ToString(rd["ResName"]);

            if (!Convert.IsDBNull(rd["ResPhone"]))
                it.ResPhone = Convert.ToString(rd["ResPhone"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                it.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["IsDeleted"]))
                it.IsDeleted = Convert.ToBoolean(rd["IsDeleted"]);

            if (!Convert.IsDBNull(rd["Credit"]))
                it.Credit = Convert.ToInt32(rd["Credit"]);
            return it;
        }
    }
}
