using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using FM2E.IDAL.Equipment;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Exceptions;
using FM2E.Model.Utils;
using FM2E.Model.Equipment;
using System.Data.SqlTypes;

namespace FM2E.SQLServerDAL.Equipment
{
    /// <summary>
    /// 数据访问类SubsidiaryEquipment。
    /// </summary>
    public class SubsidiaryEquipment : ISubsidiaryEquipment
    {
        public SubsidiaryEquipment()
        { }
        #region  成员方法

        /// <summary>
        /// 获取一个实体
        /// </summary>
        private SubsidiaryEquipmentInfo GetData(IDataReader rd)
        {
            SubsidiaryEquipmentInfo item = new SubsidiaryEquipmentInfo();
            if (!Convert.IsDBNull(rd["SubsidiaryEquipmentID"]))
                item.SubsidiaryEquipmentID = Convert.ToInt64(rd["SubsidiaryEquipmentID"]);

            if (!Convert.IsDBNull(rd["SubsidiaryEquipmentNO"]))
                item.SubsidiaryEquipmentNO = Convert.ToString(rd["SubsidiaryEquipmentNO"]);

            if (!Convert.IsDBNull(rd["Name"]))
                item.Name = Convert.ToString(rd["Name"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["CompanyName"]))
                item.CompanyName = Convert.ToString(rd["CompanyName"]);

            if (!Convert.IsDBNull(rd["SystemID"]))
                item.SystemID = Convert.ToString(rd["SystemID"]);

            if (!Convert.IsDBNull(rd["SystemName"]))
                item.SystemName = Convert.ToString(rd["SystemName"]);

            if (!Convert.IsDBNull(rd["Model"]))
                item.Model = Convert.ToString(rd["Model"]);

            if (!Convert.IsDBNull(rd["Specification"]))
                item.Specification = Convert.ToString(rd["Specification"]);

            if (!Convert.IsDBNull(rd["DetailLocation"]))
                item.DetailLocation = Convert.ToString(rd["DetailLocation"]);

            if (!Convert.IsDBNull(rd["AddressID"]))
                item.AddressID = Convert.ToInt64(rd["AddressID"]);

            if (!Convert.IsDBNull(rd["AddressName"]))
                item.AddressName = Convert.ToString(rd["AddressName"]);

            if (!Convert.IsDBNull(rd["CatalogID"]))
                item.CatalogID = Convert.ToInt64(rd["CatalogID"]);

            if (!Convert.IsDBNull(rd["CatalogName"]))
                item.CatalogName = Convert.ToString(rd["CatalogName"]);

            if (!Convert.IsDBNull(rd["AssertNumber"]))
                item.AssertNumber = Convert.ToString(rd["AssertNumber"]);

            if (!Convert.IsDBNull(rd["Price"]))
                item.Price = Convert.ToDecimal(rd["Price"]);

            if (!Convert.IsDBNull(rd["Status"]))
                item.Status = (EquipmentStatus)Convert.ToInt32(rd["Status"]);

            if (!Convert.IsDBNull(rd["FileDate"]))
                item.FileDate = Convert.ToDateTime(rd["FileDate"]);

            if (!Convert.IsDBNull(rd["MaintenanceTimes"]))
                item.MaintenanceTimes = Convert.ToInt32(rd["MaintenanceTimes"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["IsCancel"]))
                item.IsCancel = Convert.ToBoolean(rd["IsCancel"]);

            if (!Convert.IsDBNull(rd["UpdateTime"]))
                item.UpdateTime = Convert.ToDateTime(rd["UpdateTime"]);

            return item;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void InsertSubsidiaryEquipment(SubsidiaryEquipmentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_SubsidiaryEquipment(");
            strSql.Append("SubsidiaryEquipmentNO,Name,CompanyID,SystemID,Model,Specification,DetailLocation,AddressID,CatalogID,CatalogName,AssertNumber,Price,Status,FileDate,MaintenanceTimes,Remark,IsCancel,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@SubsidiaryEquipmentNO,@Name,@CompanyID,@SystemID,@Model,@Specification,@DetailLocation,@AddressID,@CatalogID,@CatalogName,@AssertNumber,@Price,@Status,@FileDate,@MaintenanceTimes,@Remark,@IsCancel,@UpdateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SubsidiaryEquipmentNO", SqlDbType.VarChar,20),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@SystemID", SqlDbType.VarChar,2),
					new SqlParameter("@Model", SqlDbType.VarChar,20),
					new SqlParameter("@Specification", SqlDbType.NVarChar,60),
					new SqlParameter("@DetailLocation", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressID", SqlDbType.BigInt,8),
					new SqlParameter("@CatalogID", SqlDbType.BigInt,8),
					new SqlParameter("@CatalogName", SqlDbType.NVarChar,20),
					new SqlParameter("@AssertNumber", SqlDbType.VarChar,50),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@FileDate", SqlDbType.DateTime),
					new SqlParameter("@MaintenanceTimes", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@IsCancel", SqlDbType.Bit,1),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.SubsidiaryEquipmentNO;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.CompanyID;
            parameters[3].Value = model.SystemID;
            parameters[4].Value = model.Model;
            parameters[5].Value = model.Specification;
            parameters[6].Value = model.DetailLocation;
            parameters[7].Value = model.AddressID;
            parameters[8].Value = model.CatalogID == 0 ? SqlInt64.Null : model.CatalogID;
            parameters[9].Value = string.IsNullOrEmpty(model.CatalogName) ? SqlString.Null : model.CatalogName;
            parameters[10].Value = model.AssertNumber;
            parameters[11].Value = model.Price;
            parameters[12].Value = model.Status;
            parameters[13].Value = model.FileDate;
            parameters[14].Value = model.MaintenanceTimes;
            parameters[15].Value = model.Remark;
            parameters[16].Value = model.IsCancel;
            parameters[17].Value = model.UpdateTime;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                    if (result == 0)
                        throw new Exception("没有增加任何数据项");
                }
                catch (Exception e)
                {
                    throw e;
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
        public void UpdateSubsidiaryEquipment(FM2E.Model.Equipment.SubsidiaryEquipmentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_SubsidiaryEquipment set ");
            strSql.Append("SubsidiaryEquipmentNO=@SubsidiaryEquipmentNO,");
            strSql.Append("Name=@Name,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("SystemID=@SystemID,");
            strSql.Append("Model=@Model,");
            strSql.Append("Specification=@Specification,");
            strSql.Append("DetailLocation=@DetailLocation,");
            strSql.Append("AddressID=@AddressID,");
            strSql.Append("CatalogID=@CatalogID,");
            strSql.Append("CatalogName=@CatalogName,");
            strSql.Append("AssertNumber=@AssertNumber,");
            strSql.Append("Price=@Price,");
            strSql.Append("Status=@Status,");
            strSql.Append("FileDate=@FileDate,");
            strSql.Append("MaintenanceTimes=@MaintenanceTimes,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("IsCancel=@IsCancel,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where SubsidiaryEquipmentID=@SubsidiaryEquipmentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SubsidiaryEquipmentID", SqlDbType.BigInt,8),
					new SqlParameter("@SubsidiaryEquipmentNO", SqlDbType.VarChar,20),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@SystemID", SqlDbType.VarChar,2),
					new SqlParameter("@Model", SqlDbType.VarChar,20),
					new SqlParameter("@Specification", SqlDbType.NVarChar,60),
					new SqlParameter("@DetailLocation", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressID", SqlDbType.BigInt,8),
					new SqlParameter("@CatalogID", SqlDbType.BigInt,8),
					new SqlParameter("@CatalogName", SqlDbType.NVarChar,20),
					new SqlParameter("@AssertNumber", SqlDbType.VarChar,50),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@FileDate", SqlDbType.DateTime),
					new SqlParameter("@MaintenanceTimes", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@IsCancel", SqlDbType.Bit,1),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.SubsidiaryEquipmentID;
            parameters[1].Value = model.SubsidiaryEquipmentNO;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.CompanyID;
            parameters[4].Value = model.SystemID;
            parameters[5].Value = model.Model;
            parameters[6].Value = model.Specification;
            parameters[7].Value = model.DetailLocation;
            parameters[8].Value = model.AddressID;
            parameters[9].Value = model.CatalogID == 0 ? SqlInt64.Null : model.CatalogID;
            parameters[10].Value = string.IsNullOrEmpty(model.CatalogName) ? SqlString.Null : model.CatalogName;
            parameters[11].Value = model.AssertNumber;
            parameters[12].Value = model.Price;
            parameters[13].Value = model.Status;
            parameters[14].Value = model.FileDate;
            parameters[15].Value = model.MaintenanceTimes;
            parameters[16].Value = model.Remark;
            parameters[17].Value = model.IsCancel;
            parameters[18].Value = model.UpdateTime;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                    if (result == 0)
                        throw new Exception("没有更新任何数据项");
                }
                catch (Exception e)
                {
                    throw e;
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
        public void DeleteSubsidiaryEquipment(long SubsidiaryEquipmentID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from FM2E_SubsidiaryEquipment ");
            strSql.Append(" where SubsidiaryEquipmentID=@SubsidiaryEquipmentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SubsidiaryEquipmentID", SqlDbType.BigInt)};
            parameters[0].Value = SubsidiaryEquipmentID;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                    if (result == 0)
                        throw new Exception("没有删除任何数据项");
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SubsidiaryEquipmentInfo GetSubsidiaryEquipment(long SubsidiaryEquipmentID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SubsidiaryEquipmentID,SubsidiaryEquipmentNO,Name,CompanyID,CompanyName,SystemID,SystemName,Model,Specification,DetailLocation,AddressID,AddressName,CatalogID,CatalogName,AssertNumber,Price,Status,FileDate,MaintenanceTimes,Remark,IsCancel,UpdateTime ");
            strSql.Append(" FROM FM2E_SubsidiaryEquipmentView ");
            strSql.Append(" where SubsidiaryEquipmentID=@SubsidiaryEquipmentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SubsidiaryEquipmentID", SqlDbType.BigInt)};
            parameters[0].Value = SubsidiaryEquipmentID;

            SubsidiaryEquipmentInfo item = new SubsidiaryEquipmentInfo();
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
                throw new DALException("获取SubsidiaryEquipmentInfo实体信息失败", e);
            }
            return item;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SubsidiaryEquipmentInfo GetSubsidiaryEquipmentByNO(string SubsidiaryEquipmentNO)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 SubsidiaryEquipmentID,SubsidiaryEquipmentNO,Name,CompanyID,CompanyName,SystemID,SystemName,Model,Specification,DetailLocation,AddressID,AddressName,CatalogID,CatalogName,AssertNumber,Price,Status,FileDate,MaintenanceTimes,Remark,IsCancel,UpdateTime ");
            strSql.Append(" FROM FM2E_SubsidiaryEquipmentView ");
            strSql.Append(" where SubsidiaryEquipmentNO=@SubsidiaryEquipmentNO ");
            SqlParameter[] parameters = {
					new SqlParameter("@SubsidiaryEquipmentNO", SqlDbType.VarChar,20)};
            parameters[0].Value = SubsidiaryEquipmentNO;

            SubsidiaryEquipmentInfo item = new SubsidiaryEquipmentInfo();
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
                throw new DALException("获取SubsidiaryEquipmentInfo实体信息失败", e);
            }
            return item;
        }

        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        public IList GetAllSubsidiaryEquipment()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SubsidiaryEquipmentID,SubsidiaryEquipmentNO,Name,CompanyID,CompanyName,SystemID,SystemName,Model,Specification,DetailLocation,AddressID,AddressName,CatalogID,CatalogName,AssertNumber,Price,Status,FileDate,MaintenanceTimes,Remark,IsCancel,UpdateTime ");
            strSql.Append(" FROM FM2E_SubsidiaryEquipmentView ");
            ArrayList list = new ArrayList();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        list.Add(GetData(rd));
                    }
                }
            }
            catch (Exception e)
            {
                list.Clear();
                throw new DALException("获取SubsidiaryEquipmentInfo列表信息失败", e);
            }
            return list;
        }

        /// <summary>
        /// 获取查询实体
        /// </summary>
        public QueryParam GenerateSearchTerm(SubsidiaryEquipmentInfo model)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = "FM2E_SubsidiaryEquipmentView";
            qp.ReturnFields = "SubsidiaryEquipmentID,SubsidiaryEquipmentNO,Name,CompanyID,CompanyName,SystemID,SystemName,Model,Specification,DetailLocation,AddressID,AddressName,CatalogID,CatalogName,AssertNumber,Price,Status,FileDate,MaintenanceTimes,Remark,IsCancel,UpdateTime";
            qp.OrderBy = "";
            string sqlSearch = "where 1=1 ";

            if (model.SubsidiaryEquipmentID != 0)
            {
                sqlSearch += " and SubsidiaryEquipmentID =" + model.SubsidiaryEquipmentID;
            }

            if (model.SubsidiaryEquipmentNO != null && model.SubsidiaryEquipmentNO.Trim() != string.Empty)
            {
                sqlSearch += " and SubsidiaryEquipmentNO like '%" + model.SubsidiaryEquipmentNO.Trim() + "%'";
            }

            if (model.Name != null && model.Name.Trim() != string.Empty)
            {
                sqlSearch += " and Name like '%" + model.Name.Trim() + "%'";
            }

            if (model.CompanyID != null && model.CompanyID.Trim() != string.Empty)
            {
                sqlSearch += " and CompanyID like '%" + model.CompanyID.Trim() + "%'";
            }

            if (model.CompanyName != null && model.CompanyName.Trim() != string.Empty)
            {
                sqlSearch += " and CompanyName like '%" + model.CompanyName.Trim() + "%'";
            }

            if (model.SystemID != null && model.SystemID.Trim() != string.Empty)
            {
                sqlSearch += " and SystemID like '%" + model.SystemID.Trim() + "%'";
            }

            if (model.SystemName != null && model.SystemName.Trim() != string.Empty)
            {
                sqlSearch += " and SystemName like '%" + model.SystemName.Trim() + "%'";
            }

            if (model.Model != null && model.Model.Trim() != string.Empty)
            {
                sqlSearch += " and Model like '%" + model.Model.Trim() + "%'";
            }

            if (model.Specification != null && model.Specification.Trim() != string.Empty)
            {
                sqlSearch += " and Specification like '%" + model.Specification.Trim() + "%'";
            }

            if (model.DetailLocation != null && model.DetailLocation.Trim() != string.Empty)
            {
                sqlSearch += " and DetailLocation like '%" + model.DetailLocation.Trim() + "%'";
            }

            if (model.AddressID != 0)
            {
                sqlSearch += " and AddressID =" + model.AddressID;
            }
            if (model.AddressName != null && model.AddressName.Trim() != string.Empty)
            {
                sqlSearch += " and AddressName like '%" + model.AddressName.Trim() + "%'";
            }

            if (model.CatalogID != 0)
            {
                sqlSearch += " and CatalogID =" + model.CatalogID;
            }

            if (model.CatalogName != null && model.CatalogName.Trim() != string.Empty)
            {
                sqlSearch += " and CatalogName like '%" + model.CatalogName.Trim() + "%'";
            }

            if (model.AssertNumber != null && model.AssertNumber.Trim() != string.Empty)
            {
                sqlSearch += " and AssertNumber like '%" + model.AssertNumber.Trim() + "%'";
            }

            if (model.Price != 0)
            {
                sqlSearch += " and Price =" + model.Price;
            }

            if (model.Status != EquipmentStatus.Unknown)
            {
                sqlSearch += " and Status =" + (int)model.Status;
            }


            if (model.MaintenanceTimes != 0)
            {
                sqlSearch += " and MaintenanceTimes =" + model.MaintenanceTimes;
            }

            if (model.Remark != null && model.Remark.Trim() != string.Empty)
            {
                sqlSearch += " and Remark like '%" + model.Remark.Trim() + "%'";
            }

            if (model.IsCancel == true || model.IsCancel == false)
            {
                sqlSearch += " and IsCancel =" + Convert.ToInt32(model.IsCancel);
            }


            qp.Where = sqlSearch;
            return qp;
        }

        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        public IList GetList(QueryParam term, out int recordCount)
        {
            if (term.Where == "")
            {
                term.TableName = "FM2E_SubsidiaryEquipmentView";
                term.ReturnFields = "SubsidiaryEquipmentID,SubsidiaryEquipmentNO,Name,CompanyID,CompanyName,SystemID,SystemName,Model,Specification,DetailLocation,AddressID,AddressName,CatalogID,CatalogName,AssertNumber,Price,Status,FileDate,MaintenanceTimes,Remark,IsCancel,UpdateTime";
                term.OrderBy = "";
                term.Where = "where 1=1";
            }
            try
            {
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取SubsidiaryEquipmentInfo列表分页失败", e);
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long SubsidiaryEquipmentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from FM2E_SubsidiaryEquipment");
            strSql.Append(" where SubsidiaryEquipmentID=@SubsidiaryEquipmentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SubsidiaryEquipmentID", SqlDbType.BigInt)};
            parameters[0].Value = SubsidiaryEquipmentID;

            return SQLHelper.Exists(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        #endregion  成员方法
    }
}

