﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using FM2E.IDAL.Equipment;
using FM2E.Model.Equipment;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Utils;
using System.Collections;
using FM2E.Model.Exceptions;

namespace FM2E.SQLServerDAL.Equipment
{
    public class WarehouseInventory : IWarehouseInventory
    {

        /// <summary>
        /// 根据仓库ID，盘点时间段返回盘点信息
        /// </summary>
        public IList GetWarehouseInventory(int pageindex, int pagesize, string WarehouseID, string WarehouseName, DateTime MinInventoryTime, DateTime MaxInventoryTime, out int listCount)
        {
            SqlConnection conn = null;
            ArrayList InventoryList = new ArrayList();
            int alllistindex = 0;
            listCount = 0;
            try
            {
                int startindex = (pageindex - 1) * pagesize + 1;
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                ArrayList list = selectProductOfEquipment(WarehouseID);
                listCount += list.Count;
                foreach (EquipmentInfo item in list)
                {
                    alllistindex += 1;
                    if (alllistindex >= startindex && InventoryList.Count < pagesize)
                    {
                        WarehouseInventoryInfo info = new WarehouseInventoryInfo();
                        info.ProductName = item.Name;
                        info.WarehouseID = item.LocationID;
                        info.WarehouseName = WarehouseName;
                        info.Model = item.Model + "/" + item.Specification;
                        info.OutCount = GetOutCountOfEquipment(item, WarehouseID, MinInventoryTime, MaxInventoryTime);
                        info.InCount = GetInCountOfEquipment(item, WarehouseID, MinInventoryTime, MaxInventoryTime);
                        info.WarehouseCount = GetWarehouseCountOfEquipment(item, WarehouseID);
                        info.InventoryTime = DateTime.Now;
                        info.PricePerUnit = decimal.Zero;
                        info.TotalValue = decimal.Zero;
                        info.Unit = GetUnitOfEquipment(item, WarehouseID);
                        info.Remark = string.Empty;
                        InventoryList.Add(info);
                    }
                    if (InventoryList.Count == pagesize)
                        break;
                }
                if (InventoryList.Count < pagesize)
                {
                    ArrayList list1 = selectProductOfExpendable(WarehouseID);
                    listCount += list1.Count;
                    foreach (ExpendableInfo item in list1)
                    {
                        alllistindex += 1;
                        if (alllistindex >= startindex && InventoryList.Count < pagesize)
                        {
                            WarehouseInventoryInfo info = new WarehouseInventoryInfo();
                            info.ProductName = item.Name;
                            info.WarehouseID = item.WarehouseID;
                            info.WarehouseName = WarehouseName;
                            info.Model = item.Model + "/" + item.Specification;
                            info.OutCount = GetOutCountOfExpendable(item, WarehouseID, MinInventoryTime, MaxInventoryTime);
                            info.InCount = GetInCountOfExpendable(item, WarehouseID, MinInventoryTime, MaxInventoryTime);
                            info.WarehouseCount = GetWarehouseCountOfExpendable(item, WarehouseID);
                            info.InventoryTime = DateTime.Now;
                            info.PricePerUnit = decimal.Zero;
                            info.TotalValue = decimal.Zero;
                            info.Unit = GetUnitOfExpendable(item, WarehouseID);
                            info.Remark = string.Empty;
                            InventoryList.Add(info);
                        }
                        if (InventoryList.Count == pagesize)
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭连接
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
            return InventoryList;
        }
        /// <summary>
        /// 设备
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="WarehouseID"></param>
        /// <returns></returns>
        private ArrayList selectProductOfEquipment(string WarehouseID)
        {
            ArrayList list = new ArrayList();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT distinct [Name],[LocationID],[Model],[Specification]");
            strSql.Append(" FROM FM2E_Equipment");
            strSql.Append(" where [LocationTag] = 4 and LocationID=@WarehouseID and IsCancel=0");
            SqlParameter[] parameters = {
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2)};
            parameters[0].Value = WarehouseID;
            EquipmentInfo item = new EquipmentInfo();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        item = this.GetEquipmentInfoData(rd);
                        list.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取设备信息失败", e);
            }
            return list;
        }

        private string GetUnitOfEquipment(EquipmentInfo info, string warehouseID)
        {
            string sql = string.Format("select b.Unit from FM2E_Equipment a left join FM2E_Category b on a.CategoryID = b.CategoryID where a.LocationTag = 4 and LocationID='{0}' and IsCancel=0 and Name='{1}' and Model='{2}' and Specification='{3}'", warehouseID, info.Name, info.Model, info.Specification);
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
                {
                    if (rd.Read())
                    {
                        if (!Convert.IsDBNull(rd[0]))
                        {
                            return Convert.ToString(rd[0]);
                        }
                        else
                            return "";
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取设备单位信息失败", e);
            }
            return "";
        }

        private EquipmentInfo GetEquipmentInfoData(IDataReader rd)
        {
            EquipmentInfo item = new EquipmentInfo();

            if (!Convert.IsDBNull(rd["Name"]))
                item.Name = Convert.ToString(rd["Name"]);

            if (!Convert.IsDBNull(rd["LocationID"]))
                item.LocationID = Convert.ToString(rd["LocationID"]);

            if (!Convert.IsDBNull(rd["Model"]))
                item.Model = Convert.ToString(rd["Model"]);

            if (!Convert.IsDBNull(rd["Specification"]))
                item.Specification = Convert.ToString(rd["Specification"]);

            return item;
        }
        private decimal GetOutCountOfEquipment(EquipmentInfo info, string WarehouseID, DateTime MinInventoryTime, DateTime MaxInventoryTime)
        {
            decimal OutCount = decimal.Zero;
            ArrayList list = new ArrayList();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [EquipmentNO]");
            strSql.Append(" FROM FM2E_Equipment");
            strSql.Append(" where [LocationTag] = 4 and LocationID=@WarehouseID and IsCancel=0 and Name=@Name and Model=@Model and Specification=@Specification");
            SqlParameter[] parameters = {
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
                    new SqlParameter("@Name",SqlDbType.NVarChar,20),
                    new SqlParameter("@Model",SqlDbType.VarChar,20),
                    new SqlParameter("@Specification",SqlDbType.NVarChar,60)};
            parameters[0].Value = WarehouseID;
            parameters[1].Value = info.Name;
            parameters[2].Value = info.Model;
            parameters[3].Value = info.Specification;
            string EquipmentNO = string.Empty;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        if (!Convert.IsDBNull(rd["EquipmentNO"]))
                        {
                            EquipmentNO = Convert.ToString(rd["EquipmentNO"]);
                            string sql = "select sum(Count) from [FM2E_OutEquipments] where EquipmentNO=@EquipmentNO and OutTime>=@MinInventoryTime and OutTime<=@MaxInventoryTime";
                            SqlParameter[] parameters1 = {
					            new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
                                new SqlParameter("@MinInventoryTime",SqlDbType.DateTime),
                                new SqlParameter("@MaxInventoryTime",SqlDbType.DateTime)};
                            parameters1[0].Value = EquipmentNO;
                            parameters1[1].Value = MinInventoryTime;
                            parameters1[2].Value = MaxInventoryTime;
                            using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, parameters1))
                            {
                                if (rdr.Read())
                                {
                                    if (!rdr.IsDBNull(0))
                                        OutCount += rdr.GetDecimal(0);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取出库数量失败", e);
            }
            return OutCount;
        }
        private decimal GetInCountOfEquipment(EquipmentInfo info, string WarehouseID, DateTime MinInventoryTime, DateTime MaxInventoryTime)
        {
            decimal InCount = decimal.Zero;
            ArrayList list = new ArrayList();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [EquipmentNO]");
            strSql.Append(" FROM FM2E_Equipment");
            strSql.Append(" where [LocationTag] = 4 and LocationID=@WarehouseID and IsCancel=0 and Name=@Name and Model=@Model and Specification=@Specification");
            SqlParameter[] parameters = {
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
                    new SqlParameter("@Name",SqlDbType.NVarChar,20),
                    new SqlParameter("@Model",SqlDbType.VarChar,20),
                    new SqlParameter("@Specification",SqlDbType.NVarChar,60)};
            parameters[0].Value = WarehouseID;
            parameters[1].Value = info.Name;
            parameters[2].Value = info.Model;
            parameters[3].Value = info.Specification;
            string EquipmentNO = string.Empty;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        if (!Convert.IsDBNull(rd["EquipmentNO"]))
                        {
                            EquipmentNO = Convert.ToString(rd["EquipmentNO"]);
                            string sql = "select sum(Count) from [FM2E_InEquipments] where EquipmentNO=@EquipmentNO and InTime>=@MinInventoryTime and InTime<=@MaxInventoryTime";
                            SqlParameter[] parameters1 = {
					            new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
                                new SqlParameter("@MinInventoryTime",SqlDbType.DateTime),
                                new SqlParameter("@MaxInventoryTime",SqlDbType.DateTime)};
                            parameters1[0].Value = EquipmentNO;
                            parameters1[1].Value = MinInventoryTime;
                            parameters1[2].Value = MaxInventoryTime;
                            using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, parameters1))
                            {
                                if (rdr.Read())
                                {
                                    if (!rdr.IsDBNull(0))
                                        InCount += rdr.GetDecimal(0);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取入库数量失败", e);
            }
            return InCount;
        }
        private decimal GetWarehouseCountOfEquipment(EquipmentInfo info, string WarehouseID)
        {
            decimal WarehouseCount = decimal.Zero;
            ArrayList list = new ArrayList();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT count(*)");
            strSql.Append(" FROM FM2E_Equipment");
            strSql.Append(" where [LocationTag] = 4 and LocationID=@WarehouseID and IsCancel=0 and Name=@Name and Model=@Model and Specification=@Specification");
            SqlParameter[] parameters = {
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
                    new SqlParameter("@Name",SqlDbType.NVarChar,20),
                    new SqlParameter("@Model",SqlDbType.VarChar,20),
                    new SqlParameter("@Specification",SqlDbType.NVarChar,60)};
            parameters[0].Value = WarehouseID;
            parameters[1].Value = info.Name;
            parameters[2].Value = info.Model;
            parameters[3].Value = info.Specification;
            string EquipmentNO = string.Empty;
            try
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rdr.Read())
                    {
                        if (!rdr.IsDBNull(0))
                            WarehouseCount = Convert.ToDecimal(rdr.GetInt32(0));
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取库存量失败", e);
            }
            return WarehouseCount;
        }
        /// <summary>
        /// 消耗品
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="WarehouseID"></param>
        /// <returns></returns>
        private ArrayList selectProductOfExpendable(string WarehouseID)
        {
            ArrayList list = new ArrayList();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT distinct [Name],[Model],[Specification]");
            strSql.Append(" FROM FM2E_Expendable");
            strSql.Append(" where WarehouseID=@WarehouseID");
            SqlParameter[] parameters = {
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2)};
            parameters[0].Value = WarehouseID;
            ExpendableInfo item = new ExpendableInfo();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        item = this.GetExpendableInfoData(rd);
                        list.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取消耗品信息失败", e);
            }
            return list;
        }

        private ExpendableInfo GetExpendableInfoData(IDataReader rd)
        {
            ExpendableInfo item = new ExpendableInfo();

            if (!Convert.IsDBNull(rd["Name"]))
                item.Name = Convert.ToString(rd["Name"]);

            if (!Convert.IsDBNull(rd["Model"]))
                item.Model = Convert.ToString(rd["Model"]);

            if (!Convert.IsDBNull(rd["Specification"]))
                item.Specification = Convert.ToString(rd["Specification"]);

            return item;
        }

        private string GetUnitOfExpendable(ExpendableInfo info, string WarehouseID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Unit");
            strSql.Append(" FROM FM2E_Expendable");
            strSql.Append(" where WarehouseID=@WarehouseID and Name=@Name and Model=@Model and Specification=@Specification");
            SqlParameter[] parameters = {
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
                    new SqlParameter("@Name",SqlDbType.NVarChar,20),
                    new SqlParameter("@Model",SqlDbType.VarChar,20),
                    new SqlParameter("@Specification",SqlDbType.NVarChar,60)};
            parameters[0].Value = WarehouseID;
            parameters[1].Value = info.Name;
            parameters[2].Value = info.Model;
            parameters[3].Value = info.Specification;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        if (!Convert.IsDBNull(rd[0]))
                        {
                            return Convert.ToString(rd[0]);
                        }
                        else
                            return "";
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取消耗品单位信息失败", e);
            }
            return "";
        }

        private decimal GetOutCountOfExpendable(ExpendableInfo info, string WarehouseID, DateTime MinInventoryTime, DateTime MaxInventoryTime)
        {
            decimal OutCount = decimal.Zero;
            ArrayList list = new ArrayList();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [ExpendableID]");
            strSql.Append(" FROM FM2E_Expendable");
            strSql.Append(" where WarehouseID=@WarehouseID and Name=@Name and Model=@Model and Specification=@Specification");
            SqlParameter[] parameters = {
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
                    new SqlParameter("@Name",SqlDbType.NVarChar,20),
                    new SqlParameter("@Model",SqlDbType.VarChar,20),
                    new SqlParameter("@Specification",SqlDbType.NVarChar,60)};
            parameters[0].Value = WarehouseID;
            parameters[1].Value = info.Name;
            parameters[2].Value = info.Model;
            parameters[3].Value = info.Specification;
            string ExpendableID = string.Empty;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        if (!Convert.IsDBNull(rd["ExpendableID"]))
                        {
                            ExpendableID = Convert.ToString(rd["ExpendableID"]);
                            string sql = "select sum(Count) from [FM2E_OutEquipments] where ExpendableID=@ExpendableID and OutTime>=@MinInventoryTime and OutTime<=@MaxInventoryTime";
                            SqlParameter[] parameters1 = {
					            new SqlParameter("@ExpendableID", SqlDbType.VarChar,20),
                                new SqlParameter("@MinInventoryTime",SqlDbType.DateTime),
                                new SqlParameter("@MaxInventoryTime",SqlDbType.DateTime)};
                            parameters1[0].Value = ExpendableID;
                            parameters1[1].Value = MinInventoryTime;
                            parameters1[2].Value = MaxInventoryTime;
                            using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, parameters1))
                            {
                                if (rdr.Read())
                                {
                                    if (!rdr.IsDBNull(0))
                                        OutCount += rdr.GetDecimal(0);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取出库数量失败", e);
            }
            return OutCount;
        }
        private decimal GetInCountOfExpendable(ExpendableInfo info, string WarehouseID, DateTime MinInventoryTime, DateTime MaxInventoryTime)
        {
            decimal InCount = decimal.Zero;
            ArrayList list = new ArrayList();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [ExpendableID]");
            strSql.Append(" FROM FM2E_Expendable");
            strSql.Append(" where WarehouseID=@WarehouseID and Name=@Name and Model=@Model and Specification=@Specification");
            SqlParameter[] parameters = {
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
                    new SqlParameter("@Name",SqlDbType.NVarChar,20),
                    new SqlParameter("@Model",SqlDbType.VarChar,20),
                    new SqlParameter("@Specification",SqlDbType.NVarChar,60)};
            parameters[0].Value = WarehouseID;
            parameters[1].Value = info.Name;
            parameters[2].Value = info.Model;
            parameters[3].Value = info.Specification;
            string ExpendableID = string.Empty;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        if (!Convert.IsDBNull(rd["ExpendableID"]))
                        {
                            ExpendableID = Convert.ToString(rd["ExpendableID"]);
                            string sql = "select sum(Count) from [FM2E_InEquipments] where ExpendableID=@ExpendableID and InTime>=@MinInventoryTime and InTime<=@MaxInventoryTime";
                            SqlParameter[] parameters1 = {
					            new SqlParameter("@ExpendableID", SqlDbType.VarChar,20),
                                new SqlParameter("@MinInventoryTime",SqlDbType.DateTime),
                                new SqlParameter("@MaxInventoryTime",SqlDbType.DateTime)};
                            parameters1[0].Value = ExpendableID;
                            parameters1[1].Value = MinInventoryTime;
                            parameters1[2].Value = MaxInventoryTime;
                            using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, parameters1))
                            {
                                if (rdr.Read())
                                {
                                    if (!rdr.IsDBNull(0))
                                        InCount += rdr.GetDecimal(0);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取入库数量失败", e);
            }
            return InCount;
        }
        private decimal GetWarehouseCountOfExpendable(ExpendableInfo info, string WarehouseID)
        {
            decimal WarehouseCount = decimal.Zero;
            ArrayList list = new ArrayList();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT sum(Count)");
            strSql.Append(" FROM FM2E_Expendable");
            strSql.Append(" where WarehouseID=@WarehouseID and Name=@Name and Model=@Model and Specification=@Specification");
            SqlParameter[] parameters = {
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
                    new SqlParameter("@Name",SqlDbType.NVarChar,20),
                    new SqlParameter("@Model",SqlDbType.VarChar,20),
                    new SqlParameter("@Specification",SqlDbType.NVarChar,60)};
            parameters[0].Value = WarehouseID;
            parameters[1].Value = info.Name;
            parameters[2].Value = info.Model;
            parameters[3].Value = info.Specification;
            string ExpendableID = string.Empty;
            try
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rdr.Read())
                    {
                        if (!rdr.IsDBNull(0))
                            WarehouseCount = rdr.GetDecimal(0);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取库存量失败", e);
            }
            return WarehouseCount;
        }
    }
}
