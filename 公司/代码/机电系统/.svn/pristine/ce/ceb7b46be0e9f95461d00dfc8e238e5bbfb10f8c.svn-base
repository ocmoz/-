using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;
using FM2E.IDAL.Basic;
using FM2E.Model.Basic;
using System.Collections;
using FM2E.Model.Exceptions;
using FM2E.Model.Utils;

namespace FM2E.SQLServerDAL.Basic
{
    public class Department : IDepartment
    {
        private const string SELECT_ALLDEPARTMENT = "SELECT a.DepartmentID, a.CompanyID, b.CompanyName, a.Name, " +
                                                    "a.Remark, a.Phone, a.LeaderID, c.PersonName AS StaffName, a.ParentID, " +
                                                    "d.Name AS ParentName, a.[Level], a.ChildrenCount,a.DepartmentType " +
                                                    "FROM dbo.FM2E_Department a LEFT OUTER JOIN " +
                                                    "dbo.FM2E_Company b ON a.CompanyID = b.CompanyID LEFT OUTER JOIN " +
                                                    "dbo.FM2E_User c ON a.LeaderID = c.UserName LEFT OUTER JOIN " +
                                                    "dbo.FM2E_Department d ON a.ParentID = d.DepartmentID where a.[IsDeleted]=0 " +
                                                    "ORDER BY a.[Level]";
        private const string SEARCH = "SELECT a.DepartmentID, a.CompanyID, b.CompanyName, a.Name, " +
                                                    "a.Remark, a.Phone, a.LeaderID, c.PersonName AS StaffName, a.ParentID, " +
                                                    "d.Name AS ParentName, a.[Level], a.ChildrenCount,a.DepartmentType " +
                                                    "FROM dbo.FM2E_Department a LEFT OUTER JOIN " +
                                                    "dbo.FM2E_Company b ON a.CompanyID = b.CompanyID LEFT OUTER JOIN " +
                                                    "dbo.FM2E_User c ON a.LeaderID = c.UserName LEFT OUTER JOIN " +
                                                    "dbo.FM2E_Department d ON a.ParentID = d.DepartmentID ";

        private const string SELECT_GETDEPARTMENT = "SELECT a.DepartmentID, a.CompanyID, b.CompanyName, a.Name, " +
                                                    "a.Remark, a.Phone, a.LeaderID, c.PersonName AS StaffName, a.ParentID, " +
                                                    "d.Name AS ParentName, a.[Level], a.ChildrenCount,a.DepartmentType " +
                                                    "FROM dbo.FM2E_Department a LEFT OUTER JOIN " +
                                                    "dbo.FM2E_Company b ON a.CompanyID = b.CompanyID LEFT OUTER JOIN " +
                                                    "dbo.FM2E_User c ON a.LeaderID = c.UserName LEFT OUTER JOIN " +
                                                    "dbo.FM2E_Department d ON a.ParentID = d.DepartmentID where a.[IsDeleted]=0 and a.[DepartmentID]='{0}'";

        private const string SELECT_BYPARENT = "SELECT a.DepartmentID, a.CompanyID, b.CompanyName, a.Name, " +
                                                    "a.Remark, a.Phone, a.LeaderID, c.PersonName AS StaffName, a.ParentID, " +
                                                    "d.Name AS ParentName, a.[Level], a.ChildrenCount,a.DepartmentType " +
                                                    "FROM dbo.FM2E_Department a LEFT OUTER JOIN " +
                                                    "dbo.FM2E_Company b ON a.CompanyID = b.CompanyID LEFT OUTER JOIN " +
                                                    "dbo.FM2E_User c ON a.LeaderID = c.UserName LEFT OUTER JOIN " +
                                                    "dbo.FM2E_Department d ON a.ParentID = d.DepartmentID where a.[IsDeleted]=0 and a.[ParentID]='{0}'";



        private const string INSERT_DEPARTMENT = "insert into FM2E_Department([CompanyID],[Name],[Remark],[Phone],[LeaderID],[ParentID],[Level],[ChildrenCount],[IsDeleted],DepartmentType) "
                                               + "values(@CompanyID,@Name,@Remark,@Phone,@LeaderID,@ParentID,@Level,@ChildrenCount,0,@DepartmentType);select cast(@@IDENTITY AS BIGINT);";

        private const string UPDATE_DEPARTMENT = "update FM2E_Department "
                                               + "set [CompanyID]=@CompanyID,[Remark]=@Remark,[Phone]=@Phone,[Name]=@Name,[LeaderID]=@LeaderID,[ParentID]=@ParentID,[Level]=@Level,[ChildrenCount]=@ChildrenCount,DepartmentType=@DepartmentType where [DepartmentID]=@DepartmentID";

        private const string DEL_DEPARTMENT = "update FM2E_Department "
                                             + "set [IsDeleted]=1 where [DepartmentID]='{0}'";



        private const string TABLE_NAME = "FM2E_DEPARTMENT";

        private DepartmentInfo GetData(IDataReader rd)
        {
            DepartmentInfo item = new DepartmentInfo();

            if (!Convert.IsDBNull(rd["DepartmentID"]))
                item.DepartmentID = Convert.ToInt64(rd["DepartmentID"]);

            if (!Convert.IsDBNull(rd["CompanyName"]))
                item.CompanyName = Convert.ToString(rd["CompanyName"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["Name"]))
                item.Name = Convert.ToString(rd["Name"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["Phone"]))
                item.Phone = Convert.ToString(rd["Phone"]);

            if (!Convert.IsDBNull(rd["LeaderID"]))
                item.LeaderID = Convert.ToString(rd["LeaderID"]);

            if (!Convert.IsDBNull(rd["StaffName"]))
                item.StaffName = Convert.ToString(rd["StaffName"]);

            if (!Convert.IsDBNull(rd["ParentID"]))
                item.ParentID = Convert.ToInt64(rd["ParentID"]);

            if (!Convert.IsDBNull(rd["ParentName"]))
                item.ParentName = Convert.ToString(rd["ParentName"]);

            if (!Convert.IsDBNull(rd["Level"]))
                item.Level = Convert.ToByte(rd["Level"]);

            if (!Convert.IsDBNull(rd["ChildrenCount"]))
                item.ChildrenCount = Convert.ToByte(rd["ChildrenCount"]);

            if (!Convert.IsDBNull(rd["DepartmentType"]))
                item.DepartmentType = (DepartmentType)Convert.ToByte(rd["DepartmentType"]);

            return item;
        }


        public IList<DepartmentInfo> GetAllDepartment()
        {
            List<DepartmentInfo> list = new List<DepartmentInfo>();

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, SELECT_ALLDEPARTMENT, null))
                {
                    while (rd.Read())
                        list.Add(GetData(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取部门信息失败", e);
            }
            return list;
        }

        public DepartmentInfo GetDepartment(long id)
        {
            string cmd = string.Format(SELECT_GETDEPARTMENT, id);
            DepartmentInfo item = null;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        item = GetData(rd);
                }

            }
            catch (Exception e)
            {
                throw new DALException("获取某个部门信息失败", e);
            }
            return item;
        }

        public IList<DepartmentInfo> GetChilds(long id)
        {
            List<DepartmentInfo> list = new List<DepartmentInfo>();

            string sql = string.Format(SELECT_BYPARENT, id);

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
                {
                    while (rd.Read())
                        list.Add(GetData(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取部门信息失败", e);
            }
            return list;
        }

        public long InsertDepartment(DepartmentInfo item)
        {
            SqlParameter[] param = GetInsertParam();
            //param[0].Value = item.DepartmentID;
            param[0].Value = item.CompanyID;
            param[1].Value = item.Name;
            param[2].Value = item.Remark;
            param[3].Value = item.Phone;
            param[4].Value = item.LeaderID;
            param[5].Value = item.ParentID;
            param[6].Value = item.Level;
            param[7].Value = item.ChildrenCount;
            param[8].Value = item.DepartmentType;

            //读取ID
            long id = 1;
            using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, INSERT_DEPARTMENT, param))
            {
                while (rdr.Read())
                {
                    id = rdr.GetInt64(0);
                }
            }
            return id;

            //using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            //{
            //    conn.Open();
            //    try
            //    {
            //        SQLHelper.ExecuteNonQuery(conn, CommandType.Text, INSERT_DEPARTMENT, param);
            //    }
            //    catch (Exception e)
            //    {
            //        throw new DALException("插入部门信息失败", e);
            //    }
            //    finally
            //    {
            //        conn.Close();
            //    }
            //}
        }

        public void UpdateDepartment(DepartmentInfo item)
        {
            SqlParameter[] param = GetUpdateParam();
            param[0].Value = item.DepartmentID;
            param[1].Value = item.CompanyID;
            param[2].Value = item.Name;
            param[3].Value = item.Remark;
            param[4].Value = item.Phone;
            param[5].Value = item.LeaderID;
            param[6].Value = item.ParentID;
            param[7].Value = item.Level;
            param[8].Value = item.ChildrenCount;
            param[9].Value = item.DepartmentType;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, UPDATE_DEPARTMENT, param);
                    //if (result == 0)
                    //    throw new Exception("更新部门信息失败");
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

        private static SqlParameter[] GetUpdateParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(UPDATE_DEPARTMENT);
            if (param == null)
            {
                param = new SqlParameter[]{
                    new SqlParameter("@DepartmentID",SqlDbType.BigInt,8),
                    new SqlParameter("@CompanyID",SqlDbType.VarChar,2),
                    new SqlParameter("@Name",SqlDbType.NVarChar,20),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,1000),
                    new SqlParameter("@Phone",SqlDbType.VarChar,20),
                    new SqlParameter("@LeaderID",SqlDbType.VarChar,20),
                    new SqlParameter("@ParentID",SqlDbType.BigInt,8),
                    new SqlParameter("@Level",SqlDbType.TinyInt,1),
                    new SqlParameter("@ChildrenCount",SqlDbType.TinyInt,1),
                    new SqlParameter("@DepartmentType",SqlDbType.TinyInt)
                };
                SQLHelper.CacheParameters(UPDATE_DEPARTMENT, param);
            }
            return param;
        }

        private static SqlParameter[] GetInsertParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(INSERT_DEPARTMENT);
            if (param == null)
            {
                param = new SqlParameter[]{
                    //new SqlParameter("@DepartmentID",SqlDbType.BigInt,8),
                    new SqlParameter("@CompanyID",SqlDbType.VarChar,2),
                    new SqlParameter("@Name",SqlDbType.NVarChar,20),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,1000),
                    new SqlParameter("@Phone",SqlDbType.VarChar,20),
                    new SqlParameter("@LeaderID",SqlDbType.VarChar,20),
                    new SqlParameter("@ParentID",SqlDbType.BigInt,8),
                    new SqlParameter("@Level",SqlDbType.TinyInt,1),
                    new SqlParameter("@ChildrenCount",SqlDbType.TinyInt,1),
                    new SqlParameter("@DepartmentType",SqlDbType.TinyInt)
                };
                SQLHelper.CacheParameters(INSERT_DEPARTMENT, param);
            }
            return param;
        }

        public void DelDepartment(long id)
        {
            string cmd = string.Format(DEL_DEPARTMENT, id);
            try
            {
                int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, cmd, null);
                if (rows == 0)
                    throw new Exception("没有删除任何数据项！");
            }
            catch (Exception e)
            {
                throw new DALException("删除部门信息失败", e);
            }
        }

        public void DelDepartment(IList<DepartmentInfo> departments)
        {
            //此时nodeList中放的是全部要删除的结点
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (DepartmentInfo item in departments)
                        {
                            string cmd = string.Format(DEL_DEPARTMENT, item.DepartmentID);
                            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, cmd, null);
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public IList<DepartmentInfo> Search(DepartmentInfo item1)
        {
            string cmd = SEARCH + " where a.[IsDeleted]=0 ";
            //string DepartmentIDtemp = (item1.DepartmentID == 0) ? "" :Convert.ToString(item1.DepartmentID);
            //string Leveltemp = (item1.Level == 0) ? "" : Convert.ToString(item1.Level);
            if (item1.ParentName != null && item1.ParentName != string.Empty)
                cmd += " and d.Name = '" + item1.ParentName + "' ";
            if (item1.DepartmentID != 0)
                cmd += " and a.DepartmentID = " + item1.DepartmentID;
            if (item1.CompanyName != null && item1.CompanyName != string.Empty)
                cmd += " and b.CompanyName = '" + item1.CompanyName + "' ";
            if (item1.Name != null && item1.Name != string.Empty)
                cmd += " and a.Name = '" + item1.Name + "' ";
            if (item1.Level != 0)
                cmd += " and a.Level = " + item1.Level;
            if (item1.StaffName != null && item1.StaffName != string.Empty)
                cmd += " and c.PersonName like '%" + item1.StaffName + "%' ";
            if (item1.ParentID != 0)
                cmd += " and a.ParentID = " + item1.ParentID;
            if (item1.CompanyID != null && item1.CompanyID != string.Empty)
                cmd += " and a.CompanyID = '" + item1.CompanyID + "' ";
            if (item1.DepartmentType != DepartmentType.Unknown)
                cmd += " and a.DepartmentType=" + (int)item1.DepartmentType;

            cmd += "order by a.[Level] asc";


            List<DepartmentInfo> list = new List<DepartmentInfo>();
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
                throw new DALException("查询部门信息失败", e);
            }
            return list;

        }

        public QueryParam GenerateSearchTerm(DepartmentInfo item1)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = "dbo.FM2E_Department a LEFT OUTER JOIN " +
                                                    "dbo.FM2E_Company b ON a.CompanyID = b.CompanyID LEFT OUTER JOIN " +
                                                    "dbo.FM2E_User c ON a.LeaderID = c.UserName LEFT OUTER JOIN " +
                                                    "dbo.FM2E_Department d ON a.ParentID = d.DepartmentID";
            qp.ReturnFields = "a.DepartmentID as DepartmentID, a.CompanyID, b.CompanyName, a.Name, " +
                                                    "a.Remark, a.Phone, a.LeaderID, c.PersonName AS StaffName, a.ParentID, " +
                                                    "d.Name AS ParentName, a.[Level] as [Level], a.ChildrenCount,a.DepartmentType";
            qp.OrderBy = "order by [Level] asc , DepartmentID asc";

            //string sqlSearch = "where 1=1 ";
            string cmd = "where a.[IsDeleted]=0 ";
            string DepartmentIDtemp = (item1.DepartmentID == 0) ? "" : Convert.ToString(item1.DepartmentID);
            string Leveltemp = (item1.Level == 0) ? "" : Convert.ToString(item1.Level);
            if ((item1.ParentName != "") && (item1.ParentName != null))
                cmd += "and a.DepartmentID like '%" + DepartmentIDtemp + "%' and a.CompanyID like '%" + item1.CompanyID + "%' and b.CompanyName like '%" + item1.CompanyName + "%' and a.Name like '%" + item1.Name + "%' and a.Level like '%" + Leveltemp + "%' and d.Name like '%" + item1.ParentName + "%'";
            else
                cmd += "and a.DepartmentID like '%" + DepartmentIDtemp + "%' and a.CompanyID like '%" + item1.CompanyID + "%' and b.CompanyName like '%" + item1.CompanyName + "%' and a.Name like '%" + item1.Name + "%' and a.Level like '%" + Leveltemp + "%'";
            if ((item1.StaffName != "") && (item1.StaffName != null))
                cmd += " and c.PersonName like '%" + item1.StaffName + "%'";
            if (item1.Level != 0)
            {
                cmd += " and a.[Level]=" + item1.Level;
            }
            if (item1.ParentID != 0)
            {
                cmd += "and a.ParentID=" + item1.ParentID;
            }
            if (item1.DepartmentType != DepartmentType.Unknown)
                cmd += " and a.DepartmentType=" + (int)item1.DepartmentType;

            qp.Where = cmd;
            return qp;
        }
        public IList GetList(QueryParam term, out int recordCount, string companyid)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = "dbo.FM2E_Department a LEFT OUTER JOIN " +
                                                    "dbo.FM2E_Company b ON a.CompanyID = b.CompanyID LEFT OUTER JOIN " +
                                                    "dbo.FM2E_User c ON a.LeaderID = c.UserName LEFT OUTER JOIN " +
                                                    "dbo.FM2E_Department d ON a.ParentID = d.DepartmentID";
                    term.ReturnFields = "a.DepartmentID as DepartmentID, a.CompanyID, b.CompanyName, a.Name, " +
                                                    "a.Remark, a.Phone, a.LeaderID, c.PersonName AS StaffName, a.ParentID, " +
                                                    "d.Name AS ParentName, a.[Level] as [Level], a.ChildrenCount,a.DepartmentType";
                    term.OrderBy = "order by [Level] asc, DepartmentID asc";
                    term.Where = "where a.[IsDeleted]=0 and a.CompanyID = '" + companyid + "'";

                }
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取部门分页失败", e);
            }
        }

        public IList GetList(QueryParam term, out int recordCount, int level)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = "dbo.FM2E_Department a LEFT OUTER JOIN " +
                                                    "dbo.FM2E_Company b ON a.CompanyID = b.CompanyID LEFT OUTER JOIN " +
                                                    "dbo.FM2E_User c ON a.LeaderID = c.UserName LEFT OUTER JOIN " +
                                                    "dbo.FM2E_Department d ON a.ParentID = d.DepartmentID";
                    term.ReturnFields = "a.DepartmentID as DepartmentID, a.CompanyID, b.CompanyName, a.Name, " +
                                                    "a.Remark, a.Phone, a.LeaderID, c.PersonName AS StaffName, a.ParentID, " +
                                                    "d.Name AS ParentName, a.[Level] as [Level], a.ChildrenCount,a.DepartmentType";
                    term.OrderBy = "order by [Level] asc, DepartmentID asc";
                    term.Where = "where a.[IsDeleted]=0 and a.[Level] = " + level + "";

                }
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取部门分页失败", e);
            }
        }

        //public long GenerateID()
        //{
        //    SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString);
        //    conn.Open();
        //    return (long)(Convert.ToInt64(SQLHelper.ExecuteScalar(conn, CommandType.Text, "select max(DepartmentID) from FM2E_Department", null)) + Convert.ToInt64(1));
        //}

    }
}
