﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using System.Data.SqlTypes;

using System.Data;
using FM2E.IDAL.Equipment;
using FM2E.Model.Equipment;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Utils;
using System.Collections;
using FM2E.Model.Exceptions;
using System.Data.Common;
namespace FM2E.SQLServerDAL.Equipment
{
    public class Expendable : IExpendable
    {
        /// <summary>
        /// 消耗品表名
        /// </summary>
        private const string TABLE_EXPENDABLE = "FM2E_Expendable";
        /// <summary>
        /// 仓库表名
        /// </summary>
        private const string TABLE_WAREHOUSE = "FM2E_WareHouse";

        public QueryParam GenerateSearchTerm(ExpendableInfo item)
        {
            string sqlSearch = "where 1=1";
            if (item.CategoryCode != null && item.CategoryCode != "")
            {
                sqlSearch += " and d.CategoryCode like '"+item.CategoryCode+"%'";
            }
            if (item.Name != null && item.Name != "")
                sqlSearch += " and a.Name like '%" + item.Name + "%'";
            if (item.CompanyID != null && item.CompanyID != "")
                sqlSearch += " and a.CompanyID ='" + item.CompanyID + "'";
            if (item.WarehouseID != null && item.WarehouseID != "")
                sqlSearch += " and a.WarehouseID ='" + item.WarehouseID + "'";
            if (!string.IsNullOrEmpty(item.Model))
            {
                sqlSearch += " and a.Model like '%" + item.Model + "%'";
            }
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_Expendable a left join FM2E_Company b on a.CompanyID = b.CompanyID left join FM2E_Warehouse c on a.WarehouseID=c.WarehouseID left join FM2E_Category d on a.CategoryID=d.CategoryID ";
            searchTerm.ReturnFields = "a.*,b.CompanyName,c.Name as WarehouseName,d.CategoryName";
            //searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by ExpendableID desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            if (searchTerm.Where == "")
            {
                searchTerm.TableName = "FM2E_Expendable a left join FM2E_Company b on a.CompanyID = b.CompanyID left join FM2E_Warehouse c on a.WarehouseID=c.WarehouseID left join FM2E_Category d on a.CategoryID=d.CategoryID ";
                searchTerm.ReturnFields = "a.*,b.CompanyName,c.Name as WarehouseName,d.CategoryName";
                //searchTerm.PageSize = 10;
                searchTerm.OrderBy = "order by ExpendableID desc";
                searchTerm.Where = "";
            }
            return SQLHelper.GetObjectList(this.GetData, searchTerm, out recordCount);
        }
        public decimal GetCountOfExpendable(string WarehouseID, long ExpendableID)
        {
            string sql = string.Format("select Count from FM2E_Expendable where WarehouseID='{0}' and ExpendableID='{1}'", WarehouseID, ExpendableID);
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
                {
                    if (dr.Read())
                    {
                        if (!Convert.IsDBNull(dr[0]))
                            return Convert.ToDecimal(dr[0]);
                        else
                            return -1;
                    }
                }
            }
            catch
            {
                throw;
            }
            return -1;
        }
        public ExpendableInfo GetCountOfExpendable(string WarehouseID, string Name, string Model, string Specification)
        {
            ExpendableInfo item = null;
            string sql = string.Format("select top 1 a.*,b.CompanyName,c.Name as WarehouseName,d.CategoryName from FM2E_Expendable a left join FM2E_Company b on a.CompanyID = b.CompanyID left join FM2E_Warehouse c on a.WarehouseID=c.WarehouseID  left join FM2E_Category d on a.CategoryID=d.CategoryID where a.WarehouseID='{0}' and a.Name='{1}' and a.Model='{2}' and a.Specification='{3}' ", WarehouseID, Name, Model, Specification);
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
                {
                    if (dr.Read())
                    {
                        item = GetData(dr);
                    }
                }
            }
            catch
            {
                throw;
            }
            return item;
        }
        private bool IsExistExpendable(ExpendableInfo model)
        {
            string sql = string.Format("select Count(*) from FM2E_Expendable where WarehouseID ='{4}' and Name='{0}' and Model= '{1}'", model.Name, model.Model, model.Specification, model.Unit,model.WarehouseID);
            //string sql = string.Format("select Count(*) from FM2E_Expendable where WarehouseID ='{1}' and Name='{0}' ", model.Name, model.WarehouseID);
            SqlDataReader rdr = null;
            try
            {
                using (rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
                {
                    int id = 0;
                    while (rdr.Read())
                    {
                        if (!Convert.IsDBNull(rdr[0]))
                            id = Convert.ToInt32(rdr[0]);
                        if (id == 0)
                            return false;
                        else
                            return true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("读取消耗品信息失败", e);
            }
            return true;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool InsertExpendable(ExpendableInfo model)
        {
            if (IsExistExpendable(model))
            {
                throw new DuplicateException("插入重复的消耗品");
            }
            try
            {
                ExpendableInfo newinfo = this.GetCountOfExpendable(model.WarehouseID, model.Name, model.Model, model.Specification);
                if (newinfo != null)
                {
                    newinfo.Count += model.Count;
                    this.UpdateExpendable(newinfo);
                    return true;
                }
                else
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into FM2E_Expendable(");
                    strSql.Append("UpdateTime,Name,CompanyID,PhotoUrl,Model,Specification,Count,Unit,WarehouseID,CategoryID,Price,Remark)");
                    strSql.Append(" values (");
                    strSql.Append("@UpdateTime,@Name,@CompanyID,@PhotoUrl,@Model,@Specification,@Count,@Unit,@WarehouseID,@CategoryID,@Price,@Remark)");
                    strSql.Append(";");
                    SqlParameter[] parameters = {
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar,60),
					new SqlParameter("@Model", SqlDbType.NVarChar,20),
					new SqlParameter("@Specification", SqlDbType.NVarChar,60),
					new SqlParameter("@Count", SqlDbType.Decimal,9),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
                    new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
                    new SqlParameter("@Price",SqlDbType.Decimal,18),
                    new SqlParameter("@CategoryID",SqlDbType.BigInt,8),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100)};
                    parameters[0].Value = model.UpdateTime == DateTime.MinValue ? SqlDateTime.Null : model.UpdateTime;
                    parameters[1].Value = model.Name == null ? SqlString.Null : model.Name;
                    parameters[2].Value = model.CompanyID == null ? SqlString.Null : model.CompanyID;
                    parameters[3].Value = model.PhotoUrl == null ? SqlString.Null : model.PhotoUrl;
                    parameters[4].Value = model.Model == null ? SqlString.Null : model.Model;
                    parameters[5].Value = model.Specification == null ? SqlString.Null : model.Specification;
                    parameters[6].Value = model.Count;
                    parameters[7].Value = model.Unit == null ? SqlString.Null : model.Unit;
                    parameters[8].Value = model.WarehouseID == null ? SqlString.Null : model.WarehouseID;
                    parameters[9].Value = model.Price ;
                    parameters[10].Value = model.CategoryID ;
                    parameters[11].Value = model.Remark == null ? SqlString.Null : model.Remark;
                    using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
                    {
                        conn.Open();

                        SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("添加消耗品信息失败"+e.Message, e);
            }
            //return false;
        }
        public void InsertExpendable(SqlTransaction trans, ExpendableInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_Expendable(");
            strSql.Append("UpdateTime,Name,CompanyID,PhotoUrl,Model,Specification,Count,Unit,WarehouseID,CategoryID,Price,Remark)");
            strSql.Append(" values (");
            strSql.Append("@UpdateTime,@Name,@CompanyID,@PhotoUrl,@Model,@Specification,@Count,@Unit,@WarehouseID,@CategoryID,@Price,@Remark)");
            strSql.Append(";");
            SqlParameter[] parameters = {
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar,60),
					new SqlParameter("@Model", SqlDbType.NVarChar,20),
					new SqlParameter("@Specification", SqlDbType.NVarChar,60),
					new SqlParameter("@Count", SqlDbType.Decimal,9),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
                    new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
                    new SqlParameter("@Price",SqlDbType.Decimal,18),
                    new SqlParameter("@CategoryID",SqlDbType.BigInt,8),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.UpdateTime == DateTime.MinValue ? SqlDateTime.Null : model.UpdateTime;
            parameters[1].Value = model.Name == null ? SqlString.Null : model.Name;
            parameters[2].Value = model.CompanyID == null ? SqlString.Null : model.CompanyID;
            parameters[3].Value = model.PhotoUrl == null ? SqlString.Null : model.PhotoUrl;
            parameters[4].Value = model.Model == null ? SqlString.Null : model.Model;
            parameters[5].Value = model.Specification == null ? SqlString.Null : model.Specification;
            parameters[6].Value = model.Count;
            parameters[7].Value = model.Unit == null ? SqlString.Null : model.Unit;
            parameters[8].Value = model.WarehouseID == null ? SqlString.Null : model.WarehouseID;
            parameters[9].Value = model.Price;
            parameters[10].Value = model.CategoryID;
            parameters[11].Value = model.Remark == null ? SqlString.Null : model.Remark;
            try
            {
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("添加消耗品信息失败", e);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateExpendable(ExpendableInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_Expendable set ");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("Name=@Name,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("PhotoUrl=@PhotoUrl,");
            strSql.Append("Model=@Model,");
            strSql.Append("Specification=@Specification,");
            strSql.Append("Count=@Count,");
            strSql.Append("Unit=@Unit,");
            strSql.Append("WarehouseID=@WarehouseID,");
            strSql.Append("Price=@Price,");
            strSql.Append("CategoryID=@CategoryID,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ExpendableID=@ExpendableID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ExpendableID", SqlDbType.BigInt,8),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar,60),
					new SqlParameter("@Model", SqlDbType.NVarChar,20),
					new SqlParameter("@Specification", SqlDbType.NVarChar,60),
					new SqlParameter("@Count", SqlDbType.Decimal,9),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
                    new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
                    new SqlParameter("@Price",SqlDbType.Decimal,18),
                    new SqlParameter("@CategoryID",SqlDbType.BigInt,8),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.ExpendableID;
            parameters[1].Value = model.UpdateTime == DateTime.MinValue ? SqlDateTime.Null : model.UpdateTime;
            parameters[2].Value = model.Name == null ? SqlString.Null : model.Name;
            parameters[3].Value = model.CompanyID == null ? SqlString.Null : model.CompanyID;
            parameters[4].Value = model.PhotoUrl == null ? SqlString.Null : model.PhotoUrl;
            parameters[5].Value = model.Model == null ? SqlString.Null : model.Model;
            parameters[6].Value = model.Specification == null ? SqlString.Null : model.Specification;
            parameters[7].Value = model.Count;
            parameters[8].Value = model.Unit == null ? SqlString.Null : model.Unit;
            parameters[9].Value = model.WarehouseID == null ? SqlString.Null : model.WarehouseID;
            parameters[10].Value = model.Price;
            parameters[11].Value = model.CategoryID;
            parameters[12].Value = model.Remark == null ? SqlString.Null : model.Remark;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                    //if (result == 0)
                    //    throw new Exception("没有更新任何数据项");
                }
                catch (Exception e)
                {
                    throw new DALException("更新消耗品信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void UpdateExpendable(SqlTransaction trans, ExpendableInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_Expendable set ");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("Name=@Name,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("PhotoUrl=@PhotoUrl,");
            strSql.Append("Model=@Model,");
            strSql.Append("Specification=@Specification,");
            strSql.Append("Count=@Count,");
            strSql.Append("Unit=@Unit,");
            strSql.Append("WarehouseID=@WarehouseID,");
            strSql.Append("Price=@Price,");
            strSql.Append("CategoryID=@CategoryID,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ExpendableID=@ExpendableID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ExpendableID", SqlDbType.BigInt,8),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar,60),
					new SqlParameter("@Model", SqlDbType.NVarChar,20),
					new SqlParameter("@Specification", SqlDbType.NVarChar,60),
					new SqlParameter("@Count", SqlDbType.Decimal,9),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
                    new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
                    new SqlParameter("@Price",SqlDbType.Decimal,18),
                    new SqlParameter("@CategoryID",SqlDbType.BigInt,8),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.ExpendableID;
            parameters[1].Value = model.UpdateTime == DateTime.MinValue ? SqlDateTime.Null : model.UpdateTime;
            parameters[2].Value = model.Name == null ? SqlString.Null : model.Name;
            parameters[3].Value = model.CompanyID == null ? SqlString.Null : model.CompanyID;
            parameters[4].Value = model.PhotoUrl == null ? SqlString.Null : model.PhotoUrl;
            parameters[5].Value = model.Model == null ? SqlString.Null : model.Model;
            parameters[6].Value = model.Specification == null ? SqlString.Null : model.Specification;
            parameters[7].Value = model.Count;
            parameters[8].Value = model.Unit == null ? SqlString.Null : model.Unit;
            parameters[9].Value = model.WarehouseID == null ? SqlString.Null : model.WarehouseID;
            parameters[10].Value = model.Price;
            parameters[11].Value = model.CategoryID;
            parameters[12].Value = model.Remark == null ? SqlString.Null : model.Remark;
            try
            {
                int result = SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                //if (result == 0)
                //    throw new Exception("没有更新任何数据项");
            }
            catch (Exception e)
            {
                throw new DALException("更新消耗品信息失败", e);
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DelExpendable(long ExpendableID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_Expendable ");
                strSql.Append(" where ExpendableID=@ExpendableID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ExpendableID", SqlDbType.BigInt)};
                parameters[0].Value = ExpendableID;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除消耗品信息失败", e);
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ExpendableInfo GetExpendable(long ExpendableID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 a.*,b.CompanyName,c.Name as WarehouseName,d.CategoryName from FM2E_Expendable a left join FM2E_Company b on a.CompanyID = b.CompanyID left join FM2E_Warehouse c on a.WarehouseID=c.WarehouseID left join FM2E_Category d on a.CategoryID=d.CategoryID ");
            strSql.Append(" where ExpendableID=@ExpendableID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ExpendableID", SqlDbType.BigInt)};
            parameters[0].Value = ExpendableID;
            ExpendableInfo item = new ExpendableInfo();
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
            catch (Exception e)
            {
                throw new DALException("获取消耗品信息失败", e);
            }
            return item;
        }
        private ExpendableInfo GetData(IDataReader rd)
        {
            ExpendableInfo item = new ExpendableInfo();

            if (!Convert.IsDBNull(rd["ExpendableID"]))
                item.ExpendableID = Convert.ToInt64(rd["ExpendableID"]);

            if (!Convert.IsDBNull(rd["UpdateTime"]))
                item.UpdateTime = Convert.ToDateTime(rd["UpdateTime"]);

            if (!Convert.IsDBNull(rd["Name"]))
                item.Name = Convert.ToString(rd["Name"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["CompanyName"]))
                item.CompanyName = Convert.ToString(rd["CompanyName"]);

            if (!Convert.IsDBNull(rd["WarehouseID"]))
                item.WarehouseID = Convert.ToString(rd["WarehouseID"]);

            if (!Convert.IsDBNull(rd["WarehouseName"]))
                item.WarehouseName = Convert.ToString(rd["WarehouseName"]);

            if (!Convert.IsDBNull(rd["PhotoUrl"]))
                item.PhotoUrl = Convert.ToString(rd["PhotoUrl"]);

            if (!Convert.IsDBNull(rd["Model"]))
                item.Model = Convert.ToString(rd["Model"]);

            if (!Convert.IsDBNull(rd["Specification"]))
                item.Specification = Convert.ToString(rd["Specification"]);

            if (!Convert.IsDBNull(rd["Count"]))
                item.Count = Convert.ToDecimal(rd["Count"]);

            if (!Convert.IsDBNull(rd["Unit"]))
                item.Unit = Convert.ToString(rd["Unit"]);

            if (!Convert.IsDBNull(rd["Price"]))
                item.Price = Convert.ToDecimal(rd["Price"]);

            if (!Convert.IsDBNull(rd["CategoryID"]))
                item.CategoryID = Convert.ToInt64(rd["CategoryID"]);

            if (!Convert.IsDBNull(rd["CategoryName"]))
                item.CategoryName = Convert.ToString(rd["CategoryName"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);
            return item;

        }
        /// <summary>
        /// 读取实体，不做表连接
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private ExpendableInfo GetDataExpendable(IDataReader rd)
        {
            ExpendableInfo item = new ExpendableInfo();

            if (!Convert.IsDBNull(rd["ExpendableID"]))
                item.ExpendableID = Convert.ToInt64(rd["ExpendableID"]);

            if (!Convert.IsDBNull(rd["UpdateTime"]))
                item.UpdateTime = Convert.ToDateTime(rd["UpdateTime"]);

            if (!Convert.IsDBNull(rd["Name"]))
                item.Name = Convert.ToString(rd["Name"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);



            if (!Convert.IsDBNull(rd["WarehouseID"]))
                item.WarehouseID = Convert.ToString(rd["WarehouseID"]);


            if (!Convert.IsDBNull(rd["PhotoUrl"]))
                item.PhotoUrl = Convert.ToString(rd["PhotoUrl"]);

            if (!Convert.IsDBNull(rd["Model"]))
                item.Model = Convert.ToString(rd["Model"]);

            if (!Convert.IsDBNull(rd["Specification"]))
                item.Specification = Convert.ToString(rd["Specification"]);

            if (!Convert.IsDBNull(rd["Count"]))
                item.Count = Convert.ToDecimal(rd["Count"]);

            if (!Convert.IsDBNull(rd["Unit"]))
                item.Unit = Convert.ToString(rd["Unit"]);

            if (!Convert.IsDBNull(rd["Price"]))
                item.Price = Convert.ToDecimal(rd["Price"]);

            if (!Convert.IsDBNull(rd["CategoryID"]))
                item.CategoryID = Convert.ToInt64(rd["CategoryID"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);
            return item;

        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList GetAllExpendableName()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct Name from FM2E_Expendable");
            SqlParameter[] parameters = { };
            ArrayList list = new ArrayList();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        ExpendableInfo item = new ExpendableInfo();
                        if (!Convert.IsDBNull(rd["Name"]))
                            item.Name = Convert.ToString(rd["Name"]);
                        list.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取所有消耗品名称失败", e);
            }
            return list;
        }
        public IList GetAllExpendableModelbyName(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from FM2E_Expendable");
            strSql.Append(" where Name=@Name ");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,20)};
            parameters[0].Value = name;
            ArrayList list = new ArrayList();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        ExpendableInfo item = new ExpendableInfo();
                        if (!Convert.IsDBNull(rd["Name"]))
                            item.Name = Convert.ToString(rd["Name"]);
                        if (!Convert.IsDBNull(rd["Model"]))
                            item.Model = Convert.ToString(rd["Model"]);
                        if (!Convert.IsDBNull(rd["ExpendableID"]))
                            item.ExpendableID = Convert.ToInt64(rd["ExpendableID"]);
                        list.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取消耗品型号信息失败", e);
            }
            return list;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList GetAllExpendableName(string warehouseID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct Name from FM2E_Expendable");
            strSql.Append(" where WarehouseID='" + warehouseID + "'");
            SqlParameter[] parameters = { };
            ArrayList list = new ArrayList();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        ExpendableInfo item = new ExpendableInfo();
                        if (!Convert.IsDBNull(rd["Name"]))
                            item.Name = Convert.ToString(rd["Name"]);
                        list.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取所有消耗品名称失败", e);
            }
            return list;
        }
        public IList GetAllExpendableModelbyName(string warehouseID, string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from FM2E_Expendable");
            strSql.Append(" where Name=@Name and WarehouseID = @WarehouseID");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
                    new SqlParameter("@WarehouseID", SqlDbType.VarChar,2)};
            parameters[0].Value = name;
            parameters[1].Value = warehouseID;
            ArrayList list = new ArrayList();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        ExpendableInfo item = new ExpendableInfo();
                        if (!Convert.IsDBNull(rd["Name"]))
                            item.Name = Convert.ToString(rd["Name"]);
                        if (!Convert.IsDBNull(rd["Model"]))
                            item.Model = Convert.ToString(rd["Model"]);
                        if (!Convert.IsDBNull(rd["ExpendableID"]))
                            item.ExpendableID = Convert.ToInt64(rd["ExpendableID"]);
                        list.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取消耗品型号信息失败", e);
            }
            return list;
        }



        /// <summary>
        /// 查询库存信息，模糊匹配产品名称、产品型号，by zjf 2009-1-11
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyid">公司ID</param>
        /// <param name="productName">产品名称</param>
        /// <param name="productModel">规格型号</param>
        /// <returns>查询的库存结果</returns>
        IList IExpendable.QueryStorage(int pageIndex, int pageSize, out int recordCount, string companyid, string productName, string productModel)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_EXPENDABLE + " INNER JOIN " +
                                 TABLE_WAREHOUSE + " ON "
                                 + TABLE_EXPENDABLE + ".CompanyID = " + TABLE_WAREHOUSE + ".CompanyID "
                                 + " and " + TABLE_EXPENDABLE + ".WareHouseID = " + TABLE_WAREHOUSE + ".WareHouseID ";
                qp.ReturnFields =
                    TABLE_EXPENDABLE + ".CompanyID"
                    + "," + TABLE_EXPENDABLE + ".WarehouseID"//仓库ID
                    + "," + TABLE_EXPENDABLE + ".WarehouseID as WareHouseIDSort"//仓库ID
                    + "," + TABLE_WAREHOUSE + ".Name AS WareHouseName"//仓库名称
                    + "," + TABLE_EXPENDABLE + ".Name AS EquipmentName"
                    + "," + TABLE_EXPENDABLE + ".Model"
                    + ",SUM(" + TABLE_EXPENDABLE + ".Count) AS Storage"
                    + "," + TABLE_EXPENDABLE + ".Unit";
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " +
                    TABLE_EXPENDABLE + ".CompanyID='" + companyid + "'" +
                    " and " + TABLE_EXPENDABLE + ".CompanyID=" + TABLE_WAREHOUSE + ".CompanyID" +
                    " and " + TABLE_EXPENDABLE + ".Name like '%" + productName + "%'" +
                    " and " + TABLE_EXPENDABLE + ".Model like '%" + productModel + "%'" +
                    " group by " + TABLE_EXPENDABLE + ".CompanyID" +
                    "," + TABLE_EXPENDABLE + ".WarehouseID" +
                    "," + TABLE_EXPENDABLE + ".ExpendableID" +
                    "," + TABLE_WAREHOUSE + ".Name" +
                    "," + TABLE_EXPENDABLE + ".Name" +
                    "," + TABLE_EXPENDABLE + ".Model" +
                    "," + TABLE_EXPENDABLE + ".Unit";
                qp.GroupKey = "SUM(" + TABLE_EXPENDABLE + ".Count)";
                qp.OrderBy = " order by WareHouseIDSort ASC,EquipmentName ASC,Model ASC,Unit ASC";
                return SQLHelper.GetObjectListWithGroupBy(this.GetDataQueryStorage
                    , qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取库存信息查询分页失败", e);
            }
        }
        IList IExpendable.QueryStorage(int pageIndex, int pageSize, out int recordCount, string companyid, string productName, string productModel, string warehouseid)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_EXPENDABLE + " INNER JOIN " +
                                 TABLE_WAREHOUSE + " ON "
                                 + TABLE_EXPENDABLE + ".CompanyID = " + TABLE_WAREHOUSE + ".CompanyID "
                                 + " and " + TABLE_EXPENDABLE + ".WareHouseID = " + TABLE_WAREHOUSE + ".WareHouseID ";
                qp.ReturnFields =
                    TABLE_EXPENDABLE + ".CompanyID"
                    + "," + TABLE_EXPENDABLE + ".WarehouseID"//仓库ID
                    + "," + TABLE_EXPENDABLE + ".WarehouseID as WareHouseIDSort"//仓库ID
                    + "," + TABLE_WAREHOUSE + ".Name AS WareHouseName"//仓库名称
                    + "," + TABLE_EXPENDABLE + ".Name AS EquipmentName"
                    + "," + TABLE_EXPENDABLE + ".Model"
                    + ",SUM(" + TABLE_EXPENDABLE + ".Count) AS Storage"
                    + "," + TABLE_EXPENDABLE + ".Unit";
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                if (warehouseid == "")
                {
                    qp.Where = "where " +
                    TABLE_EXPENDABLE + ".CompanyID='" + companyid + "'" +
                    " and " + TABLE_EXPENDABLE + ".CompanyID=" + TABLE_WAREHOUSE + ".CompanyID" +
                    " and " + TABLE_EXPENDABLE + ".Name like '%" + productName + "%'" +
                    " and " + TABLE_EXPENDABLE + ".Model like '%" + productModel + "%'" +
                    " group by " + TABLE_EXPENDABLE + ".CompanyID" +
                    "," + TABLE_EXPENDABLE + ".WarehouseID" +
                    "," + TABLE_EXPENDABLE + ".ExpendableID" +
                    "," + TABLE_WAREHOUSE + ".Name" +
                    "," + TABLE_EXPENDABLE + ".Name" +
                    "," + TABLE_EXPENDABLE + ".Model" +
                    "," + TABLE_EXPENDABLE + ".Unit";
                }
                else
                {
                    qp.Where = "where " +
                        TABLE_EXPENDABLE + ".CompanyID='" + companyid + "'" +
                        " and " + TABLE_EXPENDABLE + ".CompanyID=" + TABLE_WAREHOUSE + ".CompanyID" +
                        " and " + TABLE_EXPENDABLE + ".Name like '%" + productName + "%'" +
                        " and " + TABLE_EXPENDABLE + ".Model like '%" + productModel + "%'" +
                        " and " + TABLE_WAREHOUSE + ".WarehouseID='" + warehouseid + "'" +
                        " group by " + TABLE_EXPENDABLE + ".CompanyID" +
                        "," + TABLE_EXPENDABLE + ".WarehouseID" +
                        "," + TABLE_EXPENDABLE + ".ExpendableID" +
                        "," + TABLE_WAREHOUSE + ".Name" +
                        "," + TABLE_EXPENDABLE + ".Name" +
                        "," + TABLE_EXPENDABLE + ".Model" +
                        "," + TABLE_EXPENDABLE + ".Unit";
                }
                qp.GroupKey = "SUM(" + TABLE_EXPENDABLE + ".Count)";
                qp.OrderBy = " order by WareHouseIDSort ASC,EquipmentName ASC,Model ASC,Unit ASC";
                return SQLHelper.GetObjectListWithGroupBy(this.GetDataQueryStorage
                    , qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取库存信息查询分页失败", e);
            }
        }
        /// <summary>
        /// 获取一个库存信息实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private EquipmentStorageInfo GetDataQueryStorage(IDataReader dr)
        {
            EquipmentStorageInfo item = new EquipmentStorageInfo();
            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);
            if (!Convert.IsDBNull(dr["WarehouseID"]))
                item.WareHouseID = Convert.ToString(dr["WarehouseID"]);
            if (!Convert.IsDBNull(dr["WareHouseName"]))
                item.WareHouseName = Convert.ToString(dr["WareHouseName"]);
            if (!Convert.IsDBNull(dr["EquipmentName"]))
                item.EquipmentName = Convert.ToString(dr["EquipmentName"]);
            if (!Convert.IsDBNull(dr["Model"]))
                item.EquipmentModel = Convert.ToString(dr["Model"]);
            if (!Convert.IsDBNull(dr["Storage"]))
                item.Storage = Convert.ToDecimal(dr["Storage"]);
            if (!Convert.IsDBNull(dr["Unit"]))
                item.Unit = Convert.ToString(dr["Unit"]);
            return item;
        }

        /// <summary>
        /// 获取第一个符合条件的对象
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="WarehouseID">仓库ID</param>
        /// <param name="Name">产品名称</param>
        /// <param name="Model">型号</param>
        /// <param name="unit">单位</param>
        /// <returns>如果不存在，返回null</returns>
        public ExpendableInfo GetTopExpendableItem(SqlTransaction trans, string companyid, string WarehouseID, string Name, string Model, string unit)
        {
            ExpendableInfo item = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from " + TABLE_EXPENDABLE + " ");
            strSql.Append(" where CompanyID=@CompanyID ");
            strSql.Append(" and   WarehouseID=@WarehouseID ");
            strSql.Append(" and   Name=@Name ");
            strSql.Append(" and   Model=@Model ");
            //strSql.Append(" and   Unit=@Unit ;");
            strSql.Append(" ;");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
                    new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
                    new SqlParameter("@Name", SqlDbType.NVarChar,20),
                    new SqlParameter("@Model", SqlDbType.NVarChar,20)
                    ,
                    new SqlParameter("@Unit", SqlDbType.NVarChar,5)
                                        };
            parameters[0].Value = companyid;
            parameters[1].Value = WarehouseID;
            parameters[2].Value = Name;
            parameters[3].Value = Model;
            parameters[4].Value = unit;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (dr.Read())
                    {
                        item = GetDataExpendable(dr);
                    }
                }
            }
            catch
            {
                throw;
            }
            return item;
        }

        public ExpendableInfo GetTopExpendableItem( string companyid, string WarehouseID, string Name, string Model, string unit)
        {
            ExpendableInfo item = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from " + TABLE_EXPENDABLE + " ");
            strSql.Append(" where 1=1");
            //strSql.Append(" and CompanyID=@CompanyID ");
            strSql.Append(" and   WarehouseID=@WarehouseID ");
            strSql.Append(" and   Name=@Name ");
            strSql.Append(" and   Model=@Model ");
            //strSql.Append(" and   Unit=@Unit ;");
            strSql.Append(" ;");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
                    new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
                    new SqlParameter("@Name", SqlDbType.NVarChar,20),
                    new SqlParameter("@Model", SqlDbType.NVarChar,20)
                    ,
                    new SqlParameter("@Unit", SqlDbType.NVarChar,5)
                                        };
            parameters[0].Value = companyid;
            parameters[1].Value = WarehouseID;
            parameters[2].Value = Name;
            parameters[3].Value = Model;
            parameters[4].Value = unit;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (dr.Read())
                    {
                        item = GetDataExpendable(dr);
                    }
                }
            }
            catch
            {
                throw;
            }
            return item;
        }



        /// <summary>
        /// 增加消耗品库存，完全匹配，by zjf 2009-1-20，需要先检查是否有符合条件（公司、仓库、产品名称、产品型号、单位完全匹配）的消耗品存在，如果没有，则插入一条新的记录，如果有，则增加数量
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="warehouseid">仓库ID</param>
        /// <param name="productname">产品名称</param>
        /// <param name="model">型号</param>
        /// <param name="unit">单位</param>
        /// <param name="count">增加数量</param>
        /// <returns>增加后的数量</returns>
        decimal IExpendable.AddExpendable(string companyid, string warehouseid, string productname, string model, string unit, decimal count,decimal price,long typeid)
        {
            decimal storage = count;
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先查询是否存在
                ExpendableInfo item = null;
                item = GetTopExpendableItem(trans, companyid, warehouseid, productname, model, unit);

                if (item != null)//修改
                {
                    item.Count += count;
                    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
                    if (item.Count < 0)
                        return -1;
                    //********** Modification Finished 2011-09-09 **********************************************************************************************
                    storage = item.Count;
                    item.Price = price;
                    UpdateExpendable(trans, item);
                }
                else//插入
                {
                    item = new ExpendableInfo();
                    item.CompanyID = companyid;
                    item.Count = count;
                    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
                    if (item.Count < 0)
                        return -1;
                    //********** Modification Finished 2011-09-09 **********************************************************************************************
                    item.Model = model;
                    item.Name = productname;
                    item.Unit = unit;
                    item.UpdateTime = DateTime.Now;
                    item.WarehouseID = warehouseid;
                    item.Price = price;
                    item.CategoryID = typeid;
                    InsertExpendable(trans, item);
                }
                //事务提交
                trans.Commit();
            }
            catch (SqlException sqlex)
            {
                //回滚
                trans.Rollback();
                throw sqlex;
            }
            catch (Exception ex)
            {
                //回滚
                trans.Rollback();
                throw ex;
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
            return storage;
        }

        /// <summary>
        /// 增加消耗品库存，完全匹配，by zjf 2009-1-20，需要先检查是否有符合条件（公司、仓库、产品名称、产品型号、单位完全匹配）的消耗品存在，如果没有，则插入一条新的记录，如果有，则增加数量
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="warehouseid">仓库ID</param>
        /// <param name="productname">产品名称</param>
        /// <param name="model">型号</param>
        /// <param name="unit">单位</param>
        /// <param name="count">增加数量</param>
        /// <returns>增加后的数量</returns>
        public decimal AddExpendable(SqlTransaction trans, string companyid, string warehouseid, string productname, string model, string unit, decimal count, decimal price, long typeid)
        {
            decimal storage = count;


            //先查询是否存在
            ExpendableInfo item = null;
            item = GetTopExpendableItem(trans, companyid, warehouseid, productname, model, unit);

            if (item != null)//修改
            {
                item.Count += count;
                //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
                if (item.Count < 0)
                    return -1;
                //********** Modification Finished 2011-09-09 **********************************************************************************************
                storage = item.Count;
                UpdateExpendable(trans, item);
            }
            else//插入
            {
                item = new ExpendableInfo();
                item.CompanyID = companyid;
                item.Count = count;
                //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
                if (item.Count < 0)
                    return -1;
                //********** Modification Finished 2011-09-09 **********************************************************************************************
                item.Model = model;
                item.Name = productname;
                item.Unit = unit;
                item.UpdateTime = DateTime.Now;
                item.WarehouseID = warehouseid;
                item.Price = price;
                item.CategoryID = typeid;
                InsertExpendable(trans, item);
            }

            return storage;
        }

        /// <summary>
        /// 增加消耗品库存，完全匹配（用于出库），by zjf 2009-1-20，需要先检查是否有符合条件（公司、仓库、产品名称、产品型号、单位完全匹配）的消耗品存在，如果没有，则插入一条新的记录，如果有，则增加数量
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="warehouseid">仓库ID</param>
        /// <param name="productname">产品名称</param>
        /// <param name="model">型号</param>
        /// <param name="unit">单位</param>
        /// <param name="count">增加数量</param>
        /// <returns>增加后的数量</returns>
        public decimal AddExpendable(DbTransaction trans, string companyid, string warehouseid, string productname, string model, string unit, decimal price, decimal count,long categoryid)
        {
            decimal storage = count;


            //先查询是否存在
            ExpendableInfo item = null;
            item = GetTopExpendableItem((SqlTransaction)trans, companyid, warehouseid, productname, model, unit);

            if (item != null)//修改
            {
                item.Count += count;
                //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
                if (item.Count < 0)
                    return -1;
                //********** Modification Finished 2011-09-09 **********************************************************************************************
                if(price!=0)
                    item.Price = price;
                storage = item.Count;
                UpdateExpendable((SqlTransaction)trans, item);
            }
            else//插入
            {
                item = new ExpendableInfo();
                item.CompanyID = companyid;
                item.Count = count;
                //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
                if (item.Count < 0)
                    return -1;
                //********** Modification Finished 2011-09-09 **********************************************************************************************
                item.Model = model;
                item.Name = productname;
                item.Unit = unit;
                if (price != 0)
                    item.Price = price;
                item.UpdateTime = DateTime.Now;
                item.WarehouseID = warehouseid;
                item.CategoryID = categoryid;
                InsertExpendable((SqlTransaction)trans, item);
            }

            return storage;
        }


        /// <summary>
        /// 获取产品的名称匹配
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IList<string> GetProductNames(string name, int count)
        {
            IList<string> list = new List<string>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct top " + count + " Name from FM2E_Expendable");
            strSql.Append(" where Name like '%'+@Name+'%'");
            SqlParameter[] parameters = {
                new SqlParameter("@Name",SqlDbType.NVarChar,20),
                new SqlParameter("@count",SqlDbType.Int)       
                                        };
            parameters[0].Value = name;
            parameters[1].Value = count;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        string n = "";
                        if (!Convert.IsDBNull(rd["Name"]))
                            n = Convert.ToString(rd["Name"]);
                        if(!string.IsNullOrEmpty(n))
                            list.Add(n);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取所有消耗品名称失败"+e.Message, e);
            }
            return list;
        }


        /// <summary>
        /// 获取产品的型号匹配
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IList<string> GetProductModels(string model, int count)
        {
            IList<string> list = new List<string>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct top  " + count + "   Model from FM2E_Expendable");
            strSql.Append(" where Model like '%'+@Model+'%'");
            SqlParameter[] parameters = {
                new SqlParameter("@Model",SqlDbType.NVarChar,20) ,
                 new SqlParameter("@count",SqlDbType.Int)       
                                        };
            parameters[0].Value = model;
            parameters[1].Value = count;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        string n = "";
                        if (!Convert.IsDBNull(rd["Model"]))
                            n = Convert.ToString(rd["Model"]);
                        if (!string.IsNullOrEmpty(n))
                            list.Add(n);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取所有消耗品型号失败" + e.Message, e);
            }
            return list;
        }

        /// <summary>
        /// 获取易耗品统计信息
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public ExpendableStatisticsInfo GetExpendableStaticticsData(string companyid, string warehouseid, string categoryid, DateTime datefrom, DateTime dateto)
        {
            StringBuilder dataConditionstr = new StringBuilder();
            if (DateTime.Compare(datefrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(datefrom, sqlMinDate) < 0)
                    datefrom = sqlMinDate;

                dataConditionstr.Append(" and InOutTime>='" + datefrom.ToString("yyyy-MM-dd") + " 00:00:00'");
            }

            if (DateTime.Compare(dateto, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(dateto, sqlMaxDate) > 0)
                    dateto = sqlMaxDate;

                dataConditionstr.Append(" and InOutTime<='" + dateto.ToString("yyyy-MM-dd") + " 23:59:59'");
            }

            ExpendableStatisticsInfo item = new ExpendableStatisticsInfo();
            ExpendableStatisticsInfo temp = new ExpendableStatisticsInfo();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  sum(Amount) as OutCount,sum(Amount*Price) as OutFee from FM2E_ExpendableInOut ");
            strSql.Append(" where 1=1");
            if (companyid != "0")
            {
                strSql.Append(" and   WarehouseID=@WarehouseID ");
                strSql.Append(" and   CompanyID=@CompanyID ");
            }
            else
            {
                //donothing
            }
            strSql.Append(" and   Type=@Type ");
            strSql.Append(dataConditionstr);

            strSql.Append(" and   CategoryID=@CategoryID ");
            strSql.Append(" ;");
            SqlParameter[] parameters = {
			    new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
                new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
                new SqlParameter("@Type", SqlDbType.TinyInt),
                new SqlParameter("@CategoryID", SqlDbType.BigInt)
                                    };
            parameters[0].Value = companyid;
            parameters[1].Value = warehouseid;
            parameters[2].Value = 2;
            parameters[3].Value = Convert.ToInt64(categoryid);
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (dr.Read())
                    {
                        temp = GetDataExpendableOutStatistics(dr);
                    }
                    item.OutCount = temp.OutCount;
                    item.OutFee = temp.OutFee;
                }
            }
            catch
            {
                throw;
            }
            StringBuilder instrSql = new StringBuilder();
            instrSql.Append("select  sum(Amount) as InCount,sum(Amount*Price) as InFee from FM2E_ExpendableInOut ");
            instrSql.Append(" where 1=1");
            if (companyid != "0")
            {
                strSql.Append(" and   WarehouseID=@WarehouseID ");
                strSql.Append(" and   CompanyID=@CompanyID ");
            }
            instrSql.Append(" and   Type=@Type ");
            instrSql.Append(dataConditionstr);
            instrSql.Append(" and   CategoryID=@CategoryID ");
            instrSql.Append(" ;");
            SqlParameter[] inparameters = {
			    new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
                new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
                new SqlParameter("@Type", SqlDbType.TinyInt),
                new SqlParameter("@CategoryID", SqlDbType.BigInt)
                                    };
            inparameters[0].Value = companyid;
            inparameters[1].Value = warehouseid;
            inparameters[2].Value = 1;
            inparameters[3].Value = Convert.ToInt64(categoryid);
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, instrSql.ToString(), inparameters))
                {
                    if (dr.Read())
                    {
                        temp = GetDataExpendableInStatistics(dr);
                    }
                    item.InCount = temp.InCount;
                    item.InFee = temp.InFee;
                }
            }
            catch
            {
                throw;
            }

            return item;
        }

        /// <summary>
        /// 读取实体
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private ExpendableStatisticsInfo GetDataExpendableInStatistics(IDataReader rd)
        {
            ExpendableStatisticsInfo item = new ExpendableStatisticsInfo();

            if (!Convert.IsDBNull(rd["InCount"]))
                item.InCount = Convert.ToInt32(rd["InCount"]);

            if (!Convert.IsDBNull(rd["InFee"]))
                item.InFee = Convert.ToDecimal(rd["InFee"]);

            return item;
        }

        /// <summary>
        /// 读取实体
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private ExpendableStatisticsInfo GetDataExpendableOutStatistics(IDataReader rd)
        {
            ExpendableStatisticsInfo item = new ExpendableStatisticsInfo();

            if (!Convert.IsDBNull(rd["OutCount"]))
                item.OutCount = Convert.ToInt32(rd["OutCount"]);

            if (!Convert.IsDBNull(rd["OutFee"]))
                item.OutFee = Convert.ToDecimal(rd["OutFee"]);

            return item;
        }
    }
}
