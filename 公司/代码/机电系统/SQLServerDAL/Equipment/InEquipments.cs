using System;
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
using System.Data.SqlTypes;


namespace FM2E.SQLServerDAL.Equipment
{
    public class InEquipments:IInEquipments
    {
        public QueryParam GenerateSearchTermForOut(InEquipmentsInfo item)
        {
            string sqlSearch = "where 1=1";
            if (item.ID > 0)
                sqlSearch += " and a.ItemID ='" + item.ID + "'";
            if (item.WarehouseID != null && item.WarehouseID != "")
                sqlSearch += " and a.WarehouseID ='" + item.WarehouseID + "'";
            if (item.InOutTimeLower != DateTime.MinValue)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(item.InOutTimeLower, sqlMinDate) < 0)
                    item.InOutTimeLower = sqlMinDate;

                sqlSearch += " and OutTime>='" + item.InOutTimeLower.ToString("yyyy-MM-dd") + " 00:00:00'";
            }

            if (item.InOutTimeUpper != DateTime.MinValue)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(item.InOutTimeUpper, sqlMaxDate) > 0)
                    item.InOutTimeUpper = sqlMaxDate;

                sqlSearch += " and OutTime<='" + item.InOutTimeUpper.ToString("yyyy-MM-dd") + " 23:59:59'";
            }
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_OutEquipments a left join FM2E_WareHouse b on a.WarehouseID = b.WareHouseID left join FM2E_Equipment c on a.EquipmentNO = c.EquipmentNO left join FM2E_Expendable d on a.ExpendableID=d.ExpendableID";
            searchTerm.ReturnFields = "a.*,b.Name as WarehouseName,c.Name as EquipmentName,c.Model as EquipmentModel,d.Name as ExpendableName,d.Model as ExpendableModel";
            //searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by ItemID asc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }

        public QueryParam GenerateSearchTerm(InEquipmentsInfo item)
        {
            string sqlSearch = "where 1=1";
            if (item.ID > 0)
                sqlSearch += " and a.ID ='" + item.ID + "'";
            if (item.WarehouseID != null && item.WarehouseID != "")
                sqlSearch += " and a.WarehouseID ='" + item.WarehouseID + "'";
            if (item.InOutTimeLower != DateTime.MinValue)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(item.InOutTimeLower, sqlMinDate) < 0)
                    item.InOutTimeLower = sqlMinDate;

                sqlSearch += " and InTime>='" + item.InOutTimeLower.ToString("yyyy-MM-dd") + " 00:00:00'";
            }

            if (item.InOutTimeUpper != DateTime.MinValue)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(item.InOutTimeUpper, sqlMaxDate) > 0)
                    item.InOutTimeUpper = sqlMaxDate;

                sqlSearch += " and InTime<='" + item.InOutTimeUpper.ToString("yyyy-MM-dd") + " 23:59:59'";
            }
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_InEquipments a left join FM2E_WareHouse b on a.WarehouseID = b.WareHouseID left join FM2E_Equipment c on a.EquipmentNO = c.EquipmentNO left join FM2E_Expendable d on a.ExpendableID=d.ExpendableID";
            searchTerm.ReturnFields = "a.*,b.Name as WarehouseName,c.Name as EquipmentName,c.Model as EquipmentModel,d.Name as ExpendableName,d.Model as ExpendableModel";
            //searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by ID asc,ItemID asc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            if (searchTerm.Where == "")
            {
                searchTerm.TableName = "FM2E_InEquipments a left join FM2E_WareHouse b on a.WarehouseID = b.WareHouseID left join FM2E_Equipment c on a.EquipmentNO = c.EquipmentNO left join FM2E_Expendable d on a.ExpendableID=d.ExpendableID";
                searchTerm.ReturnFields = "a.*,b.Name as WarehouseName,c.Name as EquipmentName,c.Model as EquipmentModel,d.Name as ExpendableName,d.Model as ExpendableModel";
                //searchTerm.PageSize = 10;
                searchTerm.OrderBy = "order by ID asc,ItemID asc";
                searchTerm.Where = "";
            }
            return SQLHelper.GetObjectList(this.GetData, searchTerm, out recordCount);
        }

        public IList GetListForOut(QueryParam searchTerm, out int recordCount)
        {
            if (searchTerm.Where == "")
            {
                searchTerm.TableName = "FM2E_InEquipments a left join FM2E_WareHouse b on a.WarehouseID = b.WareHouseID left join FM2E_Equipment c on a.EquipmentNO = c.EquipmentNO left join FM2E_Expendable d on a.ExpendableID=d.ExpendableID";
                searchTerm.ReturnFields = "a.*,b.Name as WarehouseName,c.Name as EquipmentName,c.Model as EquipmentModel,d.Name as ExpendableName,d.Model as ExpendableModel";
                //searchTerm.PageSize = 10;
                searchTerm.OrderBy = "order by ID asc,ItemID asc";
                searchTerm.Where = "";
            }
            return SQLHelper.GetObjectList(this.GetDataForOut, searchTerm, out recordCount);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void InsertInEquipments(InEquipmentsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_InEquipments(");
            strSql.Append("ID,WarehouseID,IsAsset,EquipmentNO,ExpendableID,Count,Unit,Name,Model,InTime,EquipmentType,Remark)");
            strSql.Append(" values (");
            strSql.Append("@ID,@WarehouseID,@IsAsset,@EquipmentNO,@ExpendableID,@Count,@Unit,@Name,@Model,@InTime,@EquipmentType,@Remark)");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@IsAsset", SqlDbType.Bit,1),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
					new SqlParameter("@ExpendableID", SqlDbType.BigInt,8),
					new SqlParameter("@Count", SqlDbType.Decimal,9),
                    new SqlParameter("@Unit", SqlDbType.NVarChar,5),
                    new SqlParameter("@Name", SqlDbType.NVarChar,20),
                    new SqlParameter("@Model", SqlDbType.NVarChar,50),
					new SqlParameter("@InTime", SqlDbType.DateTime),
                    new SqlParameter("@EquipmentType",SqlDbType.VarChar,50),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,200)

                                        };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.ItemID;
            parameters[2].Value = model.WarehouseID;
            parameters[3].Value = model.IsAsset;
            parameters[4].Value = model.EquipmentNO;
            parameters[5].Value = model.ExpendableID;
            parameters[6].Value = model.Count;
            parameters[7].Value = string.IsNullOrEmpty(model.Unit) ? SqlString.Null : model.Unit; 
            parameters[8].Value = model.EquipmentName;
            parameters[9].Value = string.IsNullOrEmpty(model.EquipmentModel) ? SqlString.Null : model.EquipmentModel;
            parameters[10].Value = model.InTime;
            parameters[11].Value = string.IsNullOrEmpty(model.EquipmentType) ? SqlString.Null : model.EquipmentType;
            parameters[12].Value = string.IsNullOrEmpty(model.Remark) ? SqlString.Null : model.Remark;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception e)
                {
                    throw new DALException("添加入库设备信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateInEquipments(InEquipmentsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_InEquipments set ");
            strSql.Append("WarehouseID=@WarehouseID,");
            strSql.Append("IsAsset=@IsAsset,");
            strSql.Append("EquipmentNO=@EquipmentNO,");
            strSql.Append("ExpendableID=@ExpendableID,");
            strSql.Append("Count=@Count,");
            strSql.Append("Unit=@Unit,");
            strSql.Append("InTime=@InTime");
            strSql.Append(" where ID=@ID and ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@IsAsset", SqlDbType.Bit,1),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
					new SqlParameter("@ExpendableID", SqlDbType.BigInt,8),
					new SqlParameter("@Count", SqlDbType.Decimal,9),
                    new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@InTime", SqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.ItemID;
            parameters[2].Value = model.WarehouseID;
            parameters[3].Value = model.IsAsset;
            parameters[4].Value = model.EquipmentNO;
            parameters[5].Value = model.ExpendableID;
            parameters[6].Value = model.Count;
            parameters[7].Value = model.Unit;
            parameters[8].Value = model.InTime;

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
                    throw new DALException("更新入库设备信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DelInEquipments(long ID, long ItemID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_InEquipments ");
                strSql.Append(" where ID=@ID and ItemID=@ItemID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt),
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
                parameters[0].Value = ID;
                parameters[1].Value = ItemID;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除入库设备信息失败", e);
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public InEquipmentsInfo GetInEquipments(long ID, long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 a.*,b.Name as WarehouseName,c.Name as EquipmentName,c.Model as EquipmentModel,d.Name as ExpendableName,d.Model as ExpendableModel from FM2E_InEquipments a left join FM2E_WareHouse b on a.WarehouseID = b.WareHouseID left join FM2E_Equipment c on a.EquipmentNO = c.EquipmentNO left join FM2E_Expendable d on a.ExpendableID=d.ExpendableID");
            strSql.Append(" where ID=@ID and ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt),
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ID;
            parameters[1].Value = ItemID;
            InEquipmentsInfo item = new InEquipmentsInfo();
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
                throw new DALException("获取入库设备信息失败", e);
            }
            return item;
        }
        private InEquipmentsInfo GetData(IDataReader rd)
        {
            InEquipmentsInfo item = new InEquipmentsInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["ItemID"]))
                item.ItemID = Convert.ToInt64(rd["ItemID"]);

            if (!Convert.IsDBNull(rd["WarehouseID"]))
                item.WarehouseID = Convert.ToString(rd["WarehouseID"]);

            if (!Convert.IsDBNull(rd["IsAsset"]))
                item.IsAsset = Convert.ToBoolean(rd["IsAsset"]);

            if (!Convert.IsDBNull(rd["EquipmentNO"]))
                item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

            if (!Convert.IsDBNull(rd["ExpendableID"]))
                item.ExpendableID = Convert.ToInt64(rd["ExpendableID"]);

            if (!Convert.IsDBNull(rd["Count"]))
                item.Count = Convert.ToDecimal(rd["Count"]);

            if (!Convert.IsDBNull(rd["InTime"]))
                item.InTime = Convert.ToDateTime(rd["InTime"]);

            if (!Convert.IsDBNull(rd["WarehouseName"]))
                item.WarehouseName = Convert.ToString(rd["WarehouseName"]);

            if (!Convert.IsDBNull(rd["Unit"]))
                item.Unit = Convert.ToString(rd["Unit"]);

            if (!Convert.IsDBNull(rd["EquipmentName"]))
                item.EquipmentName = Convert.ToString(rd["EquipmentName"]);

            if (!Convert.IsDBNull(rd["EquipmentModel"]))
                item.EquipmentModel = Convert.ToString(rd["EquipmentModel"]);

            if (!Convert.IsDBNull(rd["ExpendableName"]))
                item.ExpendableName = Convert.ToString(rd["ExpendableName"]);

            if (!Convert.IsDBNull(rd["ExpendableModel"]))
                item.ExpendableModel = Convert.ToString(rd["ExpendableModel"]);

            if (!Convert.IsDBNull(rd["Name"]))
                item.Name = Convert.ToString(rd["Name"]);

            if (!Convert.IsDBNull(rd["Model"]))
                item.Model = Convert.ToString(rd["Model"]);

            return item;

        }

        private InEquipmentsInfo GetDataForOut(IDataReader rd)
        {
            InEquipmentsInfo item = new InEquipmentsInfo();
            // 数据库不存在相关字段 [8/20/2013 Genland]
            //if (!Convert.IsDBNull(rd["ID"]))
            //    item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["ItemID"]))
                item.ItemID = Convert.ToInt64(rd["ItemID"]);

            if (!Convert.IsDBNull(rd["WarehouseID"]))
                item.WarehouseID = Convert.ToString(rd["WarehouseID"]);

            if (!Convert.IsDBNull(rd["IsAsset"]))
                item.IsAsset = Convert.ToBoolean(rd["IsAsset"]);

            if (!Convert.IsDBNull(rd["EquipmentNO"]))
                item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

            if (!Convert.IsDBNull(rd["ExpendableID"]))
                item.ExpendableID = Convert.ToInt64(rd["ExpendableID"]);

            if (!Convert.IsDBNull(rd["Count"]))
                item.Count = Convert.ToDecimal(rd["Count"]);


            if (!Convert.IsDBNull(rd["WarehouseName"]))
                item.WarehouseName = Convert.ToString(rd["WarehouseName"]);

            if (!Convert.IsDBNull(rd["Unit"]))
                item.Unit = Convert.ToString(rd["Unit"]);

            if (!Convert.IsDBNull(rd["EquipmentName"]))
                item.EquipmentName = Convert.ToString(rd["EquipmentName"]);

            if (!Convert.IsDBNull(rd["EquipmentModel"]))
                item.EquipmentModel = Convert.ToString(rd["EquipmentModel"]);

            if (!Convert.IsDBNull(rd["ExpendableName"]))
                item.ExpendableName = Convert.ToString(rd["ExpendableName"]);

            if (!Convert.IsDBNull(rd["ExpendableModel"]))
                item.ExpendableModel = Convert.ToString(rd["ExpendableModel"]);

            if (!Convert.IsDBNull(rd["Name"]))
                item.Name = Convert.ToString(rd["Name"]);

            if (!Convert.IsDBNull(rd["Model"]))
                item.Model = Convert.ToString(rd["Model"]);

            if (!Convert.IsDBNull(rd["OutTime"]))
                item.OutTime = Convert.ToDateTime(rd["OutTime"]);

            return item;

        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(string strWhere)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select ID,Result,FeedBack,InTime,Receiver,Operator,InWarehouseRemark,IsDeleted,OrderID,WarehouseID,ApplyTime,ApplyRemark,CompanyID,Applicant,Approvaler,Status ");
        //    strSql.Append(" FROM FM2E_InEquipments ");
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where " + strWhere);
        //    }
        //    return DbHelperSQL.Query(strSql.ToString());
        //}
    }
}
