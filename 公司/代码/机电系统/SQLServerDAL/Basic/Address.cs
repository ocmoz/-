using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Basic;
using System.Collections;
using FM2E.Model.Basic;
using System.Data;
using FM2E.Model.Exceptions;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;

namespace FM2E.SQLServerDAL.Basic
{
    public class Address:IAddress
    {
        private AddressInfo GetData(IDataReader rd)
        {
            AddressInfo item = new AddressInfo();

            if (!Convert.IsDBNull(rd["AddressCode"]))
                item.AddressCode = Convert.ToString(rd["AddressCode"]);

            if (!Convert.IsDBNull(rd["AddressName"]))
                item.AddressFullName = Convert.ToString(rd["AddressName"]);

            if (!Convert.IsDBNull(rd["AddressType"]))
                item.AddressType = (AddressType)Convert.ToInt32(rd["AddressType"]);

            if (!Convert.IsDBNull(rd["ChildCount"]))
                item.ChildCount = Convert.ToInt32(rd["ChildCount"]);

            if (!Convert.IsDBNull(rd["Description"]))
                item.Description = Convert.ToString(rd["Description"]);

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["Modifier"]))
                item.Modifier = Convert.ToString(rd["Modifier"]);

            if (!Convert.IsDBNull(rd["ParentAddress"]))
                item.ParentAddress = Convert.ToInt64(rd["ParentAddress"]);

            if (!Convert.IsDBNull(rd["NextAddressCode"]))
                item.NextAddressCode = Convert.ToInt32(rd["NextAddressCode"]);

            if (!Convert.IsDBNull(rd["DepartmentID"]))
                item.MainTeamID = Convert.ToInt64(rd["DepartmentID"]);

            if (!Convert.IsDBNull(rd["DepartmentName"]))
                item.MainTeamName = Convert.ToString(rd["DepartmentName"]);

            return item;
        }

        public IList GetAddressByMaintainDept(long maintaindept)
        {
            ArrayList list = new ArrayList();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("Select * from FM2E_Address where DepartmentID = "+maintaindept);

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        list.Add(GetData(rd));
                    }
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取地址列表失败", ex);
            }

            return list;
        }

        public IList GetWarehouseAddress()
        {
            ArrayList list = new ArrayList();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("Select * from FM2E_Address where AddressType = 11 " );

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        list.Add(GetData(rd));
                    }
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取地址列表失败", ex);
            }

            return list;
        }

        #region IAddress 成员
        /// <summary>
        /// 获取所有地址信息
        /// </summary>
        /// <returns></returns>
        IList IAddress.GetAllAddress()
        {
            ArrayList list = new ArrayList();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("Select * from FM2E_Address order by ID asc");

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        list.Add(GetData(rd));
                    }
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取地址列表失败", ex);
            }

            return list;
        }
        /// <summary>
        /// 通过位置id来获取具体的地址信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AddressInfo IAddress.GetAddress(long id)
        {
            AddressInfo item = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  top 1 ID,AddressCode,AddressName,AddressType,ParentAddress,ChildCount,Description,Modifier,NextAddressCode,DepartmentID,DepartmentName from FM2E_Address ");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                        item = GetData(rd);
                }
            }
            catch (Exception ex)
            {
                item = null;
                throw new DALException("获取地址信息失败", ex);
            }
            return item;
        }
        /// <summary>
        /// 通过addressCode来获取具体的地址信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AddressInfo IAddress.GetAddressByAddressCode(string addressCode)
        {
            AddressInfo item = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  top 1 ID,AddressCode,AddressName,AddressType,ParentAddress,ChildCount,Description,Modifier,NextAddressCode,DepartmentID,DepartmentName  from FM2E_Address ");
                strSql.Append(" where AddressCode=@AddressCode ");
                SqlParameter[] parameters = {
					new SqlParameter("@AddressCode", SqlDbType.VarChar,40)};
                parameters[0].Value = addressCode;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                        item = GetData(rd);
                }
            }
            catch (Exception ex)
            {
                item = null;
                throw new DALException("获取地址信息失败", ex);
            }
            return item;
        }
        /// <summary>
        /// 获取某个地址下的所有子地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList IAddress.GetChildAddress(long id)
        {
            ArrayList list = new ArrayList();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  ID,AddressCode,AddressName,AddressType,ParentAddress,ChildCount,Description,Modifier,NextAddressCode,DepartmentID,DepartmentName  from FM2E_Address ");
                strSql.Append(" where ParentAddress=@ParentAddress ");
                SqlParameter[] parameters = {
					new SqlParameter("@ParentAddress", SqlDbType.BigInt)};
                parameters[0].Value = id;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while(rd.Read())
                        list.Add(GetData(rd));
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取子地址失败", ex);
            }

            return list;
        }
        /// <summary>
        /// 添加地址
        /// </summary>
        /// <param name="model"></param>
        long IAddress.AddAddress(FM2E.Model.Basic.AddressInfo model)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FM2E_Address(");
                strSql.Append("AddressCode,AddressName,AddressType,ParentAddress,ChildCount,Description,Modifier,NextAddressCode,DepartmentID,DepartmentName)");
                strSql.Append(" values (");
                strSql.Append("@AddressCode,@AddressName,@AddressType,@ParentAddress,@ChildCount,@Description,@Modifier,@NextAddressCode,@DepartmentID,@DepartmentName)");
                strSql.Append(";update FM2E_Address set ChildCount=ChildCount+1 where ID=@ParentAddress");
                strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
                SqlParameter[] parameters = {
					new SqlParameter("@AddressCode", SqlDbType.VarChar,40),
					new SqlParameter("@AddressName", SqlDbType.NVarChar,200),
					new SqlParameter("@AddressType", SqlDbType.TinyInt,1),
					new SqlParameter("@ParentAddress", SqlDbType.BigInt,8),
					new SqlParameter("@ChildCount", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,50),
					new SqlParameter("@Modifier", SqlDbType.VarChar,20),
                    new SqlParameter("@NextAddressCode", SqlDbType.Int,4),
                    new SqlParameter("@DepartmentID", SqlDbType.BigInt,8),
                    new SqlParameter("@DepartmentName", SqlDbType.NVarChar,50)};
                parameters[0].Value = model.AddressCode;
                parameters[1].Value = model.AddressFullName;
                parameters[2].Value = model.AddressType;
                parameters[3].Value = model.ParentAddress;
                parameters[4].Value = model.ChildCount;
                parameters[5].Value = model.Description;
                parameters[6].Value = model.Modifier;
                parameters[7].Value = model.NextAddressCode;
                parameters[8].Value = model.MainTeamID;
                if(model.MainTeamName!=null)
                    parameters[9].Value = model.MainTeamName;
                else
                    parameters[9].Value = "";

                long id=(long)SQLHelper.ExecuteScalar(trans, CommandType.Text, strSql.ToString(), parameters);
                trans.Commit();
                return id;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new DALException("添加地址失败", ex);
            }
            finally
            {
                //关闭连接
                if (trans != null)
                {
                    trans.Dispose();
                    trans = null;
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
        }
        /// <summary>
        /// 修改地址
        /// </summary>
        /// <param name="model"></param>
        void IAddress.UpdateAddress(FM2E.Model.Basic.AddressInfo model)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("declare @tmp nvarchar(200) ");
                strSql.Append("select @tmp=AddressName ");
                strSql.Append("from FM2E_Address ");
                strSql.Append("where ID=@ID ");
                strSql.Append(";update FM2E_Address set ");
                strSql.Append("AddressName=@AddressName+RIGHT(AddressName,LEN(AddressName)-LEN(@tmp)) ");
                strSql.Append("where AddressCode like @AddressCode+'%'");
                strSql.Append(";update FM2E_Address set ");
                strSql.Append("AddressCode=@AddressCode,");
                strSql.Append("AddressType=@AddressType,");
                strSql.Append("ParentAddress=@ParentAddress,");
                strSql.Append("ChildCount=@ChildCount,");
                strSql.Append("Description=@Description,");
                strSql.Append("Modifier=@Modifier,");

                strSql.Append("DepartmentID=@DepartmentID,");
                strSql.Append("DepartmentName=@DepartmentName");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@AddressCode", SqlDbType.VarChar,40),
					new SqlParameter("@AddressName", SqlDbType.NVarChar,200),
					new SqlParameter("@AddressType", SqlDbType.TinyInt,1),
					new SqlParameter("@ParentAddress", SqlDbType.BigInt,8),
					new SqlParameter("@ChildCount", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,50),
					new SqlParameter("@Modifier", SqlDbType.VarChar,20),
                    new SqlParameter("@DepartmentID", SqlDbType.BigInt,8),
                    new SqlParameter("@DepartmentName", SqlDbType.NVarChar,50)}; 
                parameters[0].Value = model.ID;
                parameters[1].Value = model.AddressCode;
                parameters[2].Value = model.AddressFullName;
                parameters[3].Value = model.AddressType;
                parameters[4].Value = model.ParentAddress;
                parameters[5].Value = model.ChildCount;
                parameters[6].Value = model.Description;
                parameters[7].Value = model.Modifier;
                parameters[8].Value = model.MainTeamID;
                if (model.MainTeamName != null)
                    parameters[9].Value = model.MainTeamName;
                else
                    parameters[9].Value = "";

                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new DALException("修改地址失败", ex);
            }
                    finally
            {
                //关闭连接
                if (trans != null)
                {
                    trans.Dispose();
                    trans = null;
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
        }
        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="id"></param>
        void IAddress.DelAddress(long id)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_Address set ChildCount=ChildCount-1 where ID in (select ParentAddress from FM2E_Address where ID=@ID)");
                strSql.Append(";delete FM2E_WareHouse where AddressID=@ID; delete FM2E_Address ");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new DALException("删除地址失败", ex);
            }
            finally
            {
                //关闭连接
                if (trans != null)
                {
                    trans.Dispose();
                    trans = null;
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
        }

        /// <summary>
        /// 获取当前结点的下一个子结点的编码
        /// </summary>
        int IAddress.GetNextAddressCode(string addressCode)
        {
            int nextAddressCode = 0;

            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select top 1 NextAddressCode ");
                strSql.Append(" from FM2E_Address ");
                strSql.AppendFormat(" where AddressCode like '{0}'", addressCode);
                strSql.AppendFormat(" update FM2E_Address set NextAddressCode=NextAddressCode+1 where AddressCode like '{0}';", addressCode);

                nextAddressCode = (int)SQLHelper.ExecuteScalar(trans, CommandType.Text, strSql.ToString(), null);
                trans.Commit();
            }
            catch (Exception ex)
            {
                nextAddressCode = 0;
                trans.Rollback();
                throw new DALException("获取下一级地址编码失败", ex);
            }
            finally
            {
                //关闭连接
                if (trans != null)
                {
                    trans.Dispose();
                    trans = null;
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
            return nextAddressCode;
        }
        #endregion
    }
}
