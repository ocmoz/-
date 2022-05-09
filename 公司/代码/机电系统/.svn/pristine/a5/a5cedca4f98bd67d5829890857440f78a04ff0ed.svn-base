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
using FM2E.Model.System;

namespace FM2E.SQLServerDAL.Basic
{
    public class Warehouse:IWarehouse
    {

        private const string VIEW_WAREHOUSE = "FM2E_WarehouseView";

        private const string SELECT_ALLWAREHOUSE = "select * from "+VIEW_WAREHOUSE+" where IsDeleted=0";
        private const string SELECT_WAREHOUSE = "select * from "+VIEW_WAREHOUSE+" where WareHouseID='{0}'";
        private const string DEL_WAREHOUSE = "update FM2E_WareHouse set IsDeleted=1 where WareHouseID='{0}'";

       

        public QueryParam GenerateSearchTerm(WarehouseInfo item)
        {
            string sqlSearch = "where 1=1";
            if (!string.IsNullOrEmpty(item.WareHouseID))
                sqlSearch += " and WareHouseID = '" + item.WareHouseID + "'";
            if (!string.IsNullOrEmpty(item.Name))
                sqlSearch += " and Name like '%" + item.Name + "%'";

            if (!string.IsNullOrEmpty(item.AddressCode))
                sqlSearch += " and AddressCode = '" + item.AddressName + "'";

            if (item.AddressID!=0)
                sqlSearch += " and AddressID =" + item.AddressID + "";

            if (!string.IsNullOrEmpty(item.AddressName))
                sqlSearch += " and AddressName like '%" + item.AddressName + "%'";
            if (!string.IsNullOrEmpty(item.CompanyID))
                sqlSearch += " and CompanyID = '" + item.CompanyID + "'";
            if (!string.IsNullOrEmpty(item.ResName))
                sqlSearch += " and ResName like '%" + item.ResName + "%'";
            if (!string.IsNullOrEmpty(item.Contactor))
                sqlSearch += " and Contactor like '%" + item.Contactor + "%'";
            if (!string.IsNullOrEmpty(item.Phone))
                sqlSearch += " and Phone like '%" + item.Phone + "%'";

            sqlSearch += " and IsDeleted = 0";
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = VIEW_WAREHOUSE;
            searchTerm.ReturnFields = "*";
            //searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by CompanyID asc,WareHouseID asc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            if (searchTerm.Where == "")
            {
                searchTerm.TableName = VIEW_WAREHOUSE;
                searchTerm.ReturnFields = "*";
                //searchTerm.PageSize = 10;
                searchTerm.OrderBy = "order by CompanyID asc,WareHouseID asc";
                searchTerm.Where = "where [IsDeleted]=0";
            }
            return SQLHelper.GetObjectList(this.GetData, searchTerm, out recordCount);
        }
        private WarehouseInfo GetData(IDataReader rd)
        {
            WarehouseInfo item = new WarehouseInfo();

            if (!Convert.IsDBNull(rd["WareHouseID"]))
                item.WareHouseID = Convert.ToString(rd["WareHouseID"]);

            if (!Convert.IsDBNull(rd["Name"]))
                item.Name = Convert.ToString(rd["Name"]);

            if (!Convert.IsDBNull(rd["AddressName"]))
                item.AddressName = Convert.ToString(rd["AddressName"]);

            if (!Convert.IsDBNull(rd["AddressID"]))
                item.AddressID = Convert.ToInt64(rd["AddressID"]);

            if (!Convert.IsDBNull(rd["AddressCode"]))
                item.AddressCode = Convert.ToString(rd["AddressCode"]);

            if (!Convert.IsDBNull(rd["Phone"]))
                item.Phone = Convert.ToString(rd["Phone"]);

            if (!Convert.IsDBNull(rd["ResID"]))
                item.ResID = Convert.ToString(rd["ResID"]);

            if (!Convert.IsDBNull(rd["ResName"]))
                item.ResName = Convert.ToString(rd["ResName"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["CompanyName"]))
                item.CompanyName = Convert.ToString(rd["CompanyName"]);

            if (!Convert.IsDBNull(rd["ContactorID"]))
                item.ContactorID = Convert.ToString(rd["ContactorID"]);

            if (!Convert.IsDBNull(rd["Contactor"]))
                item.Contactor = Convert.ToString(rd["Contactor"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["IsDeleted"]))
                item.IsDeleted = Convert.ToBoolean(rd["IsDeleted"]);
            return item;
        }
        public IList<WarehouseInfo> GetAllWarehouse()
        {
            List<WarehouseInfo> list = new List<WarehouseInfo>();

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, SELECT_ALLWAREHOUSE, null))
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

        public IList GetAllWarehouseByCompany(string CompanyID)
        {
            ArrayList list = new ArrayList();
            string sql = string.Format("select * from "+VIEW_WAREHOUSE+" where IsDeleted=0 and CompanyID='{0}'", CompanyID);

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
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

        public WarehouseInfo GetWarehouse(string warehouseid)
        {
            string cmd = string.Format(SELECT_WAREHOUSE, warehouseid);
            WarehouseInfo item = new WarehouseInfo();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
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

        public void DelWarehouse(string warehouseid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_WareHouse set IsDeleted=1");
            strSql.Append(" where WareHouseID=@WareHouseID ");
            SqlParameter[] parameters = {
					new SqlParameter("@WareHouseID", SqlDbType.VarChar,50)};
            parameters[0].Value = warehouseid;
            try
            {
                int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
                if (rows == 0)
                    throw new Exception("没有删除任何数据项！");
            }
            catch
            {
                throw;
            }
        }

        public IList<WarehouseInfo> Search(WarehouseInfo whi)
        {
            string cmd = GenerateSearchSQL(whi);
            List<WarehouseInfo> list = new List<WarehouseInfo>();
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

        private string GenerateSearchSQL(WarehouseInfo item)
        {
            string sqlSearch = "select * from "+VIEW_WAREHOUSE;

            QueryParam qp = GenerateSearchTerm(item);
            sqlSearch += " " + qp.Where;
            return sqlSearch;
        }

        public void InsertWarehouse(WarehouseInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_WareHouse(");
            strSql.Append("WareHouseID,Contactor,Remark,IsDeleted,Name,AddressID,Phone,ResID,ResName,CompanyID,CompanyName,ContactorID)");
            strSql.Append(" values (");
            strSql.Append("@WareHouseID,@Contactor,@Remark,@IsDeleted,@Name,@AddressID,@Phone,@ResID,@ResName,@CompanyID,@CompanyName,@ContactorID)");
            SqlParameter[] parameters = {
					new SqlParameter("@WareHouseID", SqlDbType.VarChar,2),
					new SqlParameter("@Contactor", SqlDbType.NVarChar,20),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
					new SqlParameter("@Name", SqlDbType.NVarChar,30),
					new SqlParameter("@AddressID", SqlDbType.BigInt),
					new SqlParameter("@Phone", SqlDbType.VarChar,20),
					new SqlParameter("@ResID", SqlDbType.VarChar,20),
					new SqlParameter("@ResName", SqlDbType.NVarChar,20),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@CompanyName", SqlDbType.NVarChar,30),
					new SqlParameter("@ContactorID", SqlDbType.VarChar,20)};
            parameters[0].Value = model.WareHouseID;
            parameters[1].Value = model.Contactor;
            parameters[2].Value = model.Remark;
            parameters[3].Value = model.IsDeleted;
            parameters[4].Value = model.Name;
            parameters[5].Value = model.AddressID;
            parameters[6].Value = model.Phone;
            parameters[7].Value = model.ResID;
            parameters[8].Value = model.ResName;
            parameters[9].Value = model.CompanyID;
            parameters[10].Value = model.CompanyName;
            parameters[11].Value = model.ContactorID;
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
                        throw new DuplicateException("插入重复的仓库编号", ex);
                    else
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void UpdateWarehouse(WarehouseInfo model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_WareHouse set ");
            strSql.Append("Contactor=@Contactor,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("IsDeleted=@IsDeleted,");
            strSql.Append("Name=@Name,");
            strSql.Append("AddressID=@AddressID,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("ResID=@ResID,");
            strSql.Append("ResName=@ResName,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("CompanyName=@CompanyName,");
            strSql.Append("ContactorID=@ContactorID");
            strSql.Append(" where WareHouseID=@WareHouseID ");
            SqlParameter[] parameters = {
					new SqlParameter("@WareHouseID", SqlDbType.VarChar,2),
					new SqlParameter("@Contactor", SqlDbType.NVarChar,20),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
					new SqlParameter("@Name", SqlDbType.NVarChar,30),
					new SqlParameter("@AddressID", SqlDbType.BigInt),
					new SqlParameter("@Phone", SqlDbType.VarChar,20),
					new SqlParameter("@ResID", SqlDbType.VarChar,20),
					new SqlParameter("@ResName", SqlDbType.NVarChar,20),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@CompanyName", SqlDbType.NVarChar,30),
					new SqlParameter("@ContactorID", SqlDbType.VarChar,20)};
            parameters[0].Value = model.WareHouseID;
            parameters[1].Value = model.Contactor;
            parameters[2].Value = model.Remark;
            parameters[3].Value = model.IsDeleted;
            parameters[4].Value = model.Name;
            parameters[5].Value = model.AddressID;
            parameters[6].Value = model.Phone;
            parameters[7].Value = model.ResID;
            parameters[8].Value = model.ResName;
            parameters[9].Value = model.CompanyID;
            parameters[10].Value = model.CompanyName;
            parameters[11].Value = model.ContactorID;
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

        public void InsertWarehouseUser(WarehouseUserInfo model)
        {
            if (ExistedWarehouseUser(model))
                return;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_WarehouseUser(");
            strSql.Append("WarehouseID,UserName)");
            strSql.Append(" values (");
            strSql.Append("@WarehouseID,@UserName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@UserName", SqlDbType.VarChar,20)};
            parameters[0].Value = model.WarehouseID;
            parameters[1].Value = model.UserName;
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
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
        private bool ExistedWarehouseUser(WarehouseUserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from FM2E_WarehouseUser ");
            strSql.Append(" where WarehouseID=@WarehouseID and UserName=@UserName");
            SqlParameter[] parameters = {
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@UserName", SqlDbType.VarChar,20)};
            parameters[0].Value = model.WarehouseID;
            parameters[1].Value = model.UserName; try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        if (Convert.ToInt32(rd[0]) != 0)
                            return true;
                        else
                            return false;
                    }
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }
        public void DelWarehouseUser(WarehouseUserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_WarehouseUser ");
            strSql.Append(" where WarehouseID=@WarehouseID and UserName=@UserName");
            SqlParameter[] parameters = {
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@UserName", SqlDbType.VarChar,20)};
            parameters[0].Value = model.WarehouseID;
            parameters[1].Value = model.UserName;
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
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
        public WarehouseInfo GetWarehouseByUserName(string UserName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.* from "+VIEW_WAREHOUSE+" a left join FM2E_WarehouseUser b on a.WarehouseID=b.WarehouseID");
            strSql.Append(" where b.UserName=@UserName ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20)};
            parameters[0].Value = UserName;
            WarehouseInfo item = new WarehouseInfo();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        item = this.GetWarehouseData(rd);
                    }
                }
            }
            catch
            {
                throw;
            }
            return item;

        }

        //********************************* Modified by Xue 2011-7-26 *******************
        public List<WarehouseInfo> GetWarehouseListByUserName(string UserName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.* from " + VIEW_WAREHOUSE + " a, FM2E_WarehouseUser b ");
            strSql.Append(" where a.WarehouseID=b.WarehouseID and b.UserName=@UserName ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20)};
            parameters[0].Value = UserName;
            List<WarehouseInfo> wareHouseInfoList = new List<WarehouseInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        wareHouseInfoList.Add(this.GetWarehouseData(rd));
                    }
                }
            }
            catch
            {
                throw;
            }
            return wareHouseInfoList;

        }
        //********************************* Modification Finished *************************
        
        private WarehouseInfo GetWarehouseData(IDataReader rd)
        {

            return GetData(rd);
        }
        public IList GetWarehouseUserList(QueryParam searchTerm, string WarehouseID, out int recordCount)
        {
            searchTerm.TableName = "FM2E_UserView s1 inner join FM2E_WarehouseUser s6 on s1.UserName = s6.UserName";
            searchTerm.ReturnFields = "s1.*,s1.UserName as UserNameSort";
            searchTerm.OrderBy = "order by UserNameSort asc";
            searchTerm.Where = "where s6.WarehouseID ='" + WarehouseID + "'";
            return SQLHelper.GetObjectList(this.GetDataUser, searchTerm, out recordCount);
        }

        /// <summary>
        /// 获取用户数据实体
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private UserInfo GetDataUser(IDataReader rd)
        {
            UserInfo item = new UserInfo();

            if (!Convert.IsDBNull(rd["Address"]))
                item.Address = Convert.ToString(rd["Address"]);

            if (!Convert.IsDBNull(rd["Birthday"]))
                item.Birthday = Convert.ToDateTime(rd["Birthday"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["CompanyName"]))
                item.CompanyName = Convert.ToString(rd["CompanyName"]);

            if (!Convert.IsDBNull(rd["DepartmentID"]))
                item.DepartmentID = Convert.ToInt64(rd["DepartmentID"]);

            if (!Convert.IsDBNull(rd["DepartmentName"]))
                item.DepartmentName = Convert.ToString(rd["DepartmentName"]);

            if (!Convert.IsDBNull(rd["Email"]))
                item.Email = Convert.ToString(rd["Email"]);

            if (!Convert.IsDBNull(rd["Fax"]))
                item.Fax = Convert.ToString(rd["Fax"]);

            if (!Convert.IsDBNull(rd["HomePhone"]))
                item.HomePhone = Convert.ToString(rd["HomePhone"]);

            if (!Convert.IsDBNull(rd["IDCard"]))
                item.IDCard = Convert.ToString(rd["IDCard"]);

            if (!Convert.IsDBNull(rd["IM"]))
                item.IM = Convert.ToString(rd["IM"]);

            if (!Convert.IsDBNull(rd["LastLoginTime"]))
                item.LastLoginTime = Convert.ToDateTime(rd["LastLoginTime"]);

            if (!Convert.IsDBNull(rd["MobilePhone"]))
                item.MobilePhone = Convert.ToString(rd["MobilePhone"]);

            if (!Convert.IsDBNull(rd["OfficePhone"]))
                item.OfficePhone = Convert.ToString(rd["OfficePhone"]);

            if (!Convert.IsDBNull(rd["Password"]))
                item.Password = Convert.ToString(rd["Password"]);

            if (!Convert.IsDBNull(rd["PersonName"]))
                item.PersonName = Convert.ToString(rd["PersonName"]);

            if (!Convert.IsDBNull(rd["PhotoUrl"]))
                item.PhotoUrl = Convert.ToString(rd["PhotoUrl"]);

            if (!Convert.IsDBNull(rd["PositionID"]))
                item.PositionID = Convert.ToInt64(rd["PositionID"]);

            if (!Convert.IsDBNull(rd["PositionName"]))
                item.PositionName = Convert.ToString(rd["PositionName"]);

            if (!Convert.IsDBNull(rd["Responsibility"]))
                item.Responsibility = Convert.ToString(rd["Responsibility"]);

            if (!Convert.IsDBNull(rd["Sex"]))
                item.Sex = (Sex)Convert.ToInt32(rd["Sex"]);

            if (!Convert.IsDBNull(rd["StaffNO"]))
                item.StaffNO = Convert.ToString(rd["StaffNO"]);

            if (!Convert.IsDBNull(rd["Status"]))
                item.Status = (UserStatus)Convert.ToInt32(rd["Status"]);

            if (!Convert.IsDBNull(rd["UpdateTime"]))
                item.UpdateTime = Convert.ToDateTime(rd["UpdateTime"]);

            if (!Convert.IsDBNull(rd["UserName"]))
                item.UserName = Convert.ToString(rd["UserName"]);

            if (!Convert.IsDBNull(rd["UserType"]))
                item.UserType = (UserType)Convert.ToInt32(rd["UserType"]);

            if (!Convert.IsDBNull(rd["IsParentCompany"]))
                item.IsParentCompany = Convert.ToBoolean(rd["IsParentCompany"]);
            return item;
        }

        public IList GetAllWarehouseUserByWarehouseID(string WarehouseID)
        {
            string sql = string.Format("select s1.*,s1.UserName as UserNameSort from FM2E_UserView s1 inner join FM2E_WarehouseUser s6 on " +
            "s1.UserName = s6.UserName where s6.WarehouseID ='{0}' order by UserNameSort asc", WarehouseID);
            ArrayList list = new ArrayList();

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
                {
                    while (rd.Read())
                    {
                        list.Add(this.GetDataUser(rd));
                    }
                }
            }
            catch
            {
                throw;
            }
            return list;
        }
    }
}
