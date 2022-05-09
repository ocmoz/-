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
using System.Data.Common;
using System.Collections.Generic;
using FM2E.SQLServerDAL.Basic;
using FM2E.Model.Basic;

namespace FM2E.SQLServerDAL.Equipment
{
    /// <summary>
    /// 数据访问类ConsumableEquipment。
    /// </summary>
    public class ConsumableEquipment : IConsumableEquipment
    {
        public ConsumableEquipment()
        { }
        #region  成员方法

        /// <summary>
        /// 获取一个实体
        /// </summary>
        private ConsumableEquipmentInfo GetData(IDataReader rd)
        {
            ConsumableEquipmentInfo item = new ConsumableEquipmentInfo();
            if (!Convert.IsDBNull(rd["ConsumableEquipmentID"]))
                item.ConsumableEquipmentID = Convert.ToInt64(rd["ConsumableEquipmentID"]);

            if (!Convert.IsDBNull(rd["ConsumableEquipmentNO"]))
                item.ConsumableEquipmentNO = Convert.ToString(rd["ConsumableEquipmentNO"]);

            if (!Convert.IsDBNull(rd["Name"]))
                item.Name = Convert.ToString(rd["Name"]);

            if (!Convert.IsDBNull(rd["SystemID"]))
                item.SystemID = Convert.ToString(rd["SystemID"]);

            if (!Convert.IsDBNull(rd["SerialNum"]))
                item.SerialNum = Convert.ToString(rd["SerialNum"]);

            if (!Convert.IsDBNull(rd["Model"]))
                item.Model = Convert.ToString(rd["Model"]);

            if (!Convert.IsDBNull(rd["Specification"]))
                item.Specification = Convert.ToString(rd["Specification"]);

            if (!Convert.IsDBNull(rd["AssertNumber"]))
                item.AssertNumber = Convert.ToString(rd["AssertNumber"]);

            if (!Convert.IsDBNull(rd["Unit"]))
                item.Unit = Convert.ToString(rd["Unit"]);

            if (!Convert.IsDBNull(rd["Count"]))
                item.Count = Convert.ToInt32(rd["Count"]);

            if (!Convert.IsDBNull(rd["Price"]))
                item.Price = Convert.ToDecimal(rd["Price"]);

            if (!Convert.IsDBNull(rd["SupplierID"]))
                item.SupplierID = Convert.ToInt64(rd["SupplierID"]);

            if (!Convert.IsDBNull(rd["ProducerID"]))
                item.ProducerID = Convert.ToInt64(rd["ProducerID"]);

            if (!Convert.IsDBNull(rd["SupplierName"]))
                item.SupplierName = Convert.ToString(rd["SupplierName"]);

            if (!Convert.IsDBNull(rd["ProducerName"]))
                item.ProducerName = Convert.ToString(rd["ProducerName"]);

            if (!Convert.IsDBNull(rd["FileDate"]))
                item.FileDate = Convert.ToDateTime(rd["FileDate"]);

            if (!Convert.IsDBNull(rd["MaintenanceTimes"]))
                item.MaintenanceTimes = Convert.ToInt32(rd["MaintenanceTimes"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["UpdateTime"]))
                item.UpdateTime = Convert.ToDateTime(rd["UpdateTime"]);

            return item;
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        private ConsumableEquipmentDetailInfo GetConsumableEquipmentDetailData(IDataReader rd)
        {
            ConsumableEquipmentDetailInfo item = new ConsumableEquipmentDetailInfo();
            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["ConsumableEquipmentID"]))
                item.ConsumableEquipmentID = Convert.ToInt64(rd["ConsumableEquipmentID"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["DetailLocation"]))
                item.DetailLocation = Convert.ToString(rd["DetailLocation"]);

            if (!Convert.IsDBNull(rd["AddressName"]))
                item.AddressName = Convert.ToString(rd["AddressName"]);

            if (!Convert.IsDBNull(rd["AddressID"]))
                item.AddressID = Convert.ToInt64(rd["AddressID"]);

            if (!Convert.IsDBNull(rd["Count"]))
                item.Count = Convert.ToInt32(rd["Count"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            return item;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void InsertConsumableEquipment(ConsumableEquipmentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_ConsumableEquipment(");
            strSql.Append("ConsumableEquipmentNO,Name,SystemID,SerialNum,Model,Specification,AssertNumber,Unit,Count,Price,SupplierID,ProducerID,SupplierName,ProducerName,FileDate,MaintenanceTimes,Remark,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@ConsumableEquipmentNO,@Name,@SystemID,@SerialNum,@Model,@Specification,@AssertNumber,@Unit,@Count,@Price,@SupplierID,@ProducerID,@SupplierName,@ProducerName,@FileDate,@MaintenanceTimes,@Remark,@UpdateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ConsumableEquipmentNO", SqlDbType.VarChar,20),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@SystemID", SqlDbType.VarChar,2),
					new SqlParameter("@SerialNum", SqlDbType.VarChar,30),
					new SqlParameter("@Model", SqlDbType.VarChar,40),
					new SqlParameter("@Specification", SqlDbType.NVarChar,60),
					new SqlParameter("@AssertNumber", SqlDbType.VarChar,50),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@Count", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@SupplierID", SqlDbType.BigInt,8),
					new SqlParameter("@ProducerID", SqlDbType.BigInt,8),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProducerName", SqlDbType.NVarChar,50),
					new SqlParameter("@FileDate", SqlDbType.DateTime),
					new SqlParameter("@MaintenanceTimes", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};
            parameters[0].Value = string.IsNullOrEmpty(model.ConsumableEquipmentNO) ? SqlString.Null : model.ConsumableEquipmentNO;
            parameters[1].Value = string.IsNullOrEmpty(model.Name) ? SqlString.Null : model.Name;
            parameters[2].Value = string.IsNullOrEmpty(model.SystemID) ? SqlString.Null : model.SystemID;
            parameters[3].Value = string.IsNullOrEmpty(model.SerialNum) ? SqlString.Null : model.SerialNum;
            parameters[4].Value = string.IsNullOrEmpty(model.Model) ? SqlString.Null : model.Model;
            parameters[5].Value = string.IsNullOrEmpty(model.Specification) ? SqlString.Null : model.Specification;
            parameters[6].Value = string.IsNullOrEmpty(model.AssertNumber) ? SqlString.Null : model.AssertNumber;
            parameters[7].Value = string.IsNullOrEmpty(model.Unit) ? SqlString.Null : model.Unit;
            parameters[8].Value = model.Count == 0 ? SqlInt32.Null : model.Count;
            parameters[9].Value = model.Price;
            parameters[10].Value = model.SupplierID == 0 ? SqlInt64.Null : model.SupplierID;
            parameters[11].Value = model.ProducerID == 0 ? SqlInt64.Null : model.ProducerID;
            parameters[12].Value = string.IsNullOrEmpty(model.SupplierName) ? SqlString.Null : model.SupplierName;
            parameters[13].Value = string.IsNullOrEmpty(model.ProducerName) ? SqlString.Null : model.ProducerName;
            parameters[14].Value = DateTime.Compare(model.FileDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : model.FileDate;
            parameters[15].Value = model.MaintenanceTimes;
            parameters[16].Value = string.IsNullOrEmpty(model.Remark) ? SqlString.Null : model.Remark;
            parameters[17].Value = DateTime.Compare(model.UpdateTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : model.UpdateTime;


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
        public void UpdateConsumableEquipment(FM2E.Model.Equipment.ConsumableEquipmentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_ConsumableEquipment set ");
            strSql.Append("ConsumableEquipmentNO=@ConsumableEquipmentNO,");
            strSql.Append("Name=@Name,");
            strSql.Append("SystemID=@SystemID,");
            strSql.Append("SerialNum=@SerialNum,");
            strSql.Append("Model=@Model,");
            strSql.Append("Specification=@Specification,");
            strSql.Append("AssertNumber=@AssertNumber,");
            strSql.Append("Unit=@Unit,");
            strSql.Append("Count=@Count,");
            strSql.Append("Price=@Price,");
            strSql.Append("SupplierID=@SupplierID,");
            strSql.Append("ProducerID=@ProducerID,");
            strSql.Append("SupplierName=@SupplierName,");
            strSql.Append("ProducerName=@ProducerName,");
            strSql.Append("FileDate=@FileDate,");
            strSql.Append("MaintenanceTimes=@MaintenanceTimes,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where ConsumableEquipmentID=@ConsumableEquipmentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ConsumableEquipmentID", SqlDbType.BigInt,8),
					new SqlParameter("@ConsumableEquipmentNO", SqlDbType.VarChar,20),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@SystemID", SqlDbType.VarChar,2),
					new SqlParameter("@SerialNum", SqlDbType.VarChar,30),
					new SqlParameter("@Model", SqlDbType.VarChar,40),
					new SqlParameter("@Specification", SqlDbType.NVarChar,60),
					new SqlParameter("@AssertNumber", SqlDbType.VarChar,50),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@Count", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@SupplierID", SqlDbType.BigInt,8),
					new SqlParameter("@ProducerID", SqlDbType.BigInt,8),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProducerName", SqlDbType.NVarChar,50),
					new SqlParameter("@FileDate", SqlDbType.DateTime),
					new SqlParameter("@MaintenanceTimes", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.ConsumableEquipmentID;
            parameters[1].Value = model.ConsumableEquipmentNO;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.SystemID;
            parameters[4].Value = model.SerialNum;
            parameters[5].Value = model.Model;
            parameters[6].Value = model.Specification;
            parameters[7].Value = model.AssertNumber;
            parameters[8].Value = model.Unit;
            parameters[9].Value = model.Count;
            parameters[10].Value = model.Price;
            parameters[11].Value = model.SupplierID;
            parameters[12].Value = model.ProducerID;
            parameters[13].Value = model.SupplierName;
            parameters[14].Value = model.ProducerName;
            parameters[15].Value = model.FileDate;
            parameters[16].Value = model.MaintenanceTimes;
            parameters[17].Value = model.Remark;
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
        public void DeleteConsumableEquipment(long ConsumableEquipmentID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from FM2E_ConsumableEquipment ");
            strSql.Append(" where ConsumableEquipmentID=@ConsumableEquipmentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ConsumableEquipmentID", SqlDbType.BigInt)};
            parameters[0].Value = ConsumableEquipmentID;

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
        public ConsumableEquipmentInfo GetConsumableEquipment(long ConsumableEquipmentID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ConsumableEquipmentID,ConsumableEquipmentNO,Name,SystemID,SerialNum,Model,Specification,AssertNumber,Unit,Count,Price,SupplierID,ProducerID,SupplierName,ProducerName,FileDate,MaintenanceTimes,Remark,UpdateTime ");
            strSql.Append(" FROM FM2E_ConsumableEquipment ");
            strSql.Append(" where ConsumableEquipmentID=@ConsumableEquipmentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ConsumableEquipmentID", SqlDbType.BigInt)};
            parameters[0].Value = ConsumableEquipmentID;

            ConsumableEquipmentInfo item = new ConsumableEquipmentInfo();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        item = this.GetData(rd);
                        item.ConsumableEquipmentDetailList = GetConsumableEquipmentDetailList(item.ConsumableEquipmentID);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取ConsumableEquipmentInfo实体信息失败", e);
            }
            return item;
        }


        /// <summary>
        /// 根据条形码得到一个对象实体
        /// </summary>
        public ConsumableEquipmentInfo GetConsumableEquipmentByNO(string ConsumableEquipmentNO)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ConsumableEquipmentID,ConsumableEquipmentNO,Name,SystemID,SerialNum,Model,Specification,AssertNumber,Unit,Count,Price,SupplierID,ProducerID,SupplierName,ProducerName,FileDate,MaintenanceTimes,Remark,UpdateTime ");
            strSql.Append(" FROM FM2E_ConsumableEquipment ");
            strSql.Append(" where ConsumableEquipmentNO=@ConsumableEquipmentNO ");
            SqlParameter[] parameters = {
					new SqlParameter("@ConsumableEquipmentNO", SqlDbType.VarChar,20)};
            parameters[0].Value = ConsumableEquipmentNO;

            ConsumableEquipmentInfo item = new ConsumableEquipmentInfo();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        item = this.GetData(rd);
                        item.ConsumableEquipmentDetailList = GetConsumableEquipmentDetailList(item.ConsumableEquipmentID);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取ConsumableEquipmentInfo实体信息失败", e);
            }
            return item;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="MaintainedEquipmentID"></param>
        /// <returns></returns>
        private IList GetConsumableEquipmentDetailList(long ConsumableEquipmentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ConsumableEquipmentID,CompanyID,DetailLocation,AddressID,AddressName,Count,Remark ");
            strSql.Append(" FROM FM2E_ConsumableEquipmentDetailView ");
            strSql.Append(" where ConsumableEquipmentID=@ConsumableEquipmentID");

            SqlParameter[] parameters = {
					new SqlParameter("@ConsumableEquipmentID", SqlDbType.BigInt)};
            parameters[0].Value = ConsumableEquipmentID;
            ArrayList list = new ArrayList();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        list.Add(GetConsumableEquipmentDetailData(rd));
                    }
                }
            }
            catch (Exception e)
            {
                list.Clear();
                throw new DALException("获取FM2E_ConsumableEquipmentDetail列表信息失败", e);
            }
            return list;
        }

        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        public IList GetAllConsumableEquipment()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ConsumableEquipmentID,ConsumableEquipmentNO,Name,SystemID,SerialNum,Model,Specification,AssertNumber,Unit,Count,Price,SupplierID,ProducerID,SupplierName,ProducerName,FileDate,MaintenanceTimes,Remark,UpdateTime ");
            strSql.Append(" FROM FM2E_ConsumableEquipment ");
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
                throw new DALException("获取ConsumableEquipmentInfo列表信息失败", e);
            }
            return list;
        }

        /// <summary>
        /// 获取查询实体
        /// </summary>
        public QueryParam GenerateSearchTerm(ConsumableEquipmentInfo model)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = "FM2E_ConsumableEquipment";
            qp.ReturnFields = "ConsumableEquipmentID,ConsumableEquipmentNO,Name,SystemID,SerialNum,Model,Specification,AssertNumber,Unit,Count,Price,SupplierID,ProducerID,SupplierName,ProducerName,FileDate,MaintenanceTimes,Remark,UpdateTime";
            qp.OrderBy = "";
            string sqlSearch = "where 1=1 ";

            if (!string.IsNullOrEmpty(model.CompanyID))
                sqlSearch += " and ConsumableEquipmentID in (select DISTINCT ConsumableEquipmentID from FM2E_ConsumableEquipmentDetail where CompanyID = '"+model.CompanyID+"') ";

            if (model.MaintainDept != 0)
            {
                IList addresslist = new Address().GetAddressByMaintainDept(model.MaintainDept);
                if (addresslist != null && addresslist.Count > 0)
                {
                    sqlSearch += " and ConsumableEquipmentID in (select DISTINCT a.ConsumableEquipmentID from FM2E_ConsumableEquipmentDetail a,FM2E_Address b where a.AddressID = b.ID ";
                    int i = 0;
                    foreach (AddressInfo addressinfo in addresslist)
                    {
                        if (i == 0)
                            sqlSearch += " and ( b.AddressCode like '" + addressinfo.AddressCode + "%' ";
                        else
                            sqlSearch += " or b.AddressCode like '" + addressinfo.AddressCode + "%' ";
                        i++;
                    }
                    sqlSearch += " ) ) ";
                }
                else
                {
                    sqlSearch += " and 1=2 ";
                }
            }

            //if (model.MaintainDept != 0)
            //    sqlSearch += " and ConsumableEquipmentID in (select DISTINCT a.ConsumableEquipmentID from FM2E_ConsumableEquipmentDetail a,FM2E_Address b where a.AddressID = b.ID and b.DepartmentID = "+model.MaintainDept+") ";

            if (model.ConsumableEquipmentID != 0)
            {
                sqlSearch += " and ConsumableEquipmentID =" + model.ConsumableEquipmentID;
            }

            if (model.ConsumableEquipmentNO != null && model.ConsumableEquipmentNO.Trim() != string.Empty)
            {
                sqlSearch += " and ConsumableEquipmentNO like '%" + model.ConsumableEquipmentNO.Trim() + "%'";
            }

            if (model.Name != null && model.Name.Trim() != string.Empty)
            {
                sqlSearch += " and Name like '%" + model.Name.Trim() + "%'";
            }

            if (model.SystemID != null && model.SystemID.Trim() != string.Empty)
            {
                sqlSearch += " and SystemID like '%" + model.SystemID.Trim() + "%'";
            }

            if (model.SerialNum != null && model.SerialNum.Trim() != string.Empty)
            {
                sqlSearch += " and SerialNum like '%" + model.SerialNum.Trim() + "%'";
            }

            if (model.Model != null && model.Model.Trim() != string.Empty)
            {
                sqlSearch += " and Model like '%" + model.Model.Trim() + "%'";
            }

            if (model.Specification != null && model.Specification.Trim() != string.Empty)
            {
                sqlSearch += " and Specification like '%" + model.Specification.Trim() + "%'";
            }

            if (model.AssertNumber != null && model.AssertNumber.Trim() != string.Empty)
            {
                sqlSearch += " and AssertNumber like '%" + model.AssertNumber.Trim() + "%'";
            }

            if (model.Unit != null && model.Unit.Trim() != string.Empty)
            {
                sqlSearch += " and Unit like '%" + model.Unit.Trim() + "%'";
            }

            if (model.Count != 0)
            {
                sqlSearch += " and Count =" + model.Count;
            }

            if (model.Price != 0)
            {
                sqlSearch += " and Price =" + model.Price;
            }

            if (model.SupplierID != 0)
            {
                sqlSearch += " and SupplierID =" + model.SupplierID;
            }

            if (model.ProducerID != 0)
            {
                sqlSearch += " and ProducerID =" + model.ProducerID;
            }

            if (model.SupplierName != null && model.SupplierName.Trim() != string.Empty)
            {
                sqlSearch += " and SupplierName like '%" + model.SupplierName.Trim() + "%'";
            }

            if (model.ProducerName != null && model.ProducerName.Trim() != string.Empty)
            {
                sqlSearch += " and ProducerName like '%" + model.ProducerName.Trim() + "%'";
            }


            if (model.MaintenanceTimes != 0)
            {
                sqlSearch += " and MaintenanceTimes =" + model.MaintenanceTimes;
            }

            if (model.Remark != null && model.Remark.Trim() != string.Empty)
            {
                sqlSearch += " and Remark like '%" + model.Remark.Trim() + "%'";
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
                term.TableName = "FM2E_ConsumableEquipment";
                term.ReturnFields = "ConsumableEquipmentID,ConsumableEquipmentNO,Name,SystemID,SerialNum,Model,Specification,AssertNumber,Unit,Count,Price,SupplierID,ProducerID,SupplierName,ProducerName,FileDate,MaintenanceTimes,Remark,UpdateTime";
                term.OrderBy = "";
                term.Where = "where 1=1";
            }
            try
            {
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取ConsumableEquipmentInfo列表分页失败", e);
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ConsumableEquipmentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from FM2E_ConsumableEquipment");
            strSql.Append(" where ConsumableEquipmentID=@ConsumableEquipmentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ConsumableEquipmentID", SqlDbType.BigInt)};
            parameters[0].Value = ConsumableEquipmentID;

            return SQLHelper.Exists(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 添加故障处理单
        /// </summary>
        /// <param name="model"></param>
        public long InsertConsumableEquipmentTrans(ConsumableEquipmentInfo model, DbTransaction trans)
        {
            //先插入消息
            long id = InsertConsumableEquipment((SqlTransaction)trans, model);

            //插入消息对象列表
            UpdateEquipmentDetail((SqlTransaction)trans, model.ConsumableEquipmentDetailList, id);

            return id;
        }
        /// <summary>
        /// 插入消息
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        private long InsertConsumableEquipment(SqlTransaction trans, ConsumableEquipmentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_ConsumableEquipment(");
            strSql.Append("ConsumableEquipmentNO,Name,SystemID,SerialNum,Model,Specification,AssertNumber,Unit,Count,Price,SupplierID,ProducerID,SupplierName,ProducerName,FileDate,MaintenanceTimes,Remark,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@ConsumableEquipmentNO,@Name,@SystemID,@SerialNum,@Model,@Specification,@AssertNumber,@Unit,@Count,@Price,@SupplierID,@ProducerID,@SupplierName,@ProducerName,@FileDate,@MaintenanceTimes,@Remark,@UpdateTime)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@ConsumableEquipmentNO", SqlDbType.VarChar,20),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@SystemID", SqlDbType.VarChar,2),
					new SqlParameter("@SerialNum", SqlDbType.VarChar,30),
					new SqlParameter("@Model", SqlDbType.VarChar,20),
					new SqlParameter("@Specification", SqlDbType.NVarChar,60),
					new SqlParameter("@AssertNumber", SqlDbType.VarChar,50),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@Count", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@SupplierID", SqlDbType.BigInt,8),
					new SqlParameter("@ProducerID", SqlDbType.BigInt,8),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProducerName", SqlDbType.NVarChar,50),
					new SqlParameter("@FileDate", SqlDbType.DateTime),
					new SqlParameter("@MaintenanceTimes", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};
            parameters[0].Value = string.IsNullOrEmpty(model.ConsumableEquipmentNO) ? SqlString.Null : model.ConsumableEquipmentNO;
            parameters[1].Value = string.IsNullOrEmpty(model.Name) ? SqlString.Null : model.Name;
            parameters[2].Value = string.IsNullOrEmpty(model.SystemID) ? SqlString.Null : model.SystemID;
            parameters[3].Value = string.IsNullOrEmpty(model.SerialNum) ? SqlString.Null : model.SerialNum;
            parameters[4].Value = string.IsNullOrEmpty(model.Model) ? SqlString.Null : model.Model;
            parameters[5].Value = string.IsNullOrEmpty(model.Specification) ? SqlString.Null : model.Specification;
            parameters[6].Value = string.IsNullOrEmpty(model.AssertNumber) ? SqlString.Null : model.AssertNumber;
            parameters[7].Value = string.IsNullOrEmpty(model.Unit) ? SqlString.Null : model.Unit;
            parameters[8].Value = model.Count == 0 ? SqlInt32.Null : model.Count;
            parameters[9].Value = model.Price;
            parameters[10].Value = model.SupplierID == 0 ? SqlInt64.Null : model.SupplierID;
            parameters[11].Value = model.ProducerID == 0 ? SqlInt64.Null : model.ProducerID;
            parameters[12].Value = string.IsNullOrEmpty(model.SupplierName) ? SqlString.Null : model.SupplierName;
            parameters[13].Value = string.IsNullOrEmpty(model.ProducerName) ? SqlString.Null : model.ProducerName;
            parameters[14].Value = DateTime.Compare(model.FileDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : model.FileDate;
            parameters[15].Value = model.MaintenanceTimes;
            parameters[16].Value = string.IsNullOrEmpty(model.Remark) ? SqlString.Null : model.Remark;
            parameters[17].Value = DateTime.Compare(model.UpdateTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : model.UpdateTime;

            long id = (long)SQLHelper.ExecuteScalar(trans, CommandType.Text, strSql.ToString(), parameters);
            return id;
        }
        /// <summary>
        /// 插入设备分布列表
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="equipments"></param>
        /// <param name="id"></param>
        private void UpdateEquipmentDetail(SqlTransaction trans, IList consumableequipmentdetaillist, long id)
        {
            //先删除
            StringBuilder strDel = new StringBuilder();
            strDel.Append("delete from FM2E_ConsumableEquipmentDetail ");
            strDel.Append(" where ConsumableEquipmentID=@ConsumableEquipmentID ");
            SqlParameter[] paramDel = {
					new SqlParameter("@ConsumableEquipmentID", SqlDbType.BigInt)};
            paramDel[0].Value = id;
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strDel.ToString(), paramDel);

            if (consumableequipmentdetaillist == null || consumableequipmentdetaillist.Count == 0)
                return;

            //后插入
            StringBuilder strInsert = new StringBuilder();
            strInsert.Append("insert into FM2E_ConsumableEquipmentDetail(");
            strInsert.Append("ConsumableEquipmentID,CompanyID,DetailLocation,AddressID,Count,Remark)");
            strInsert.Append(" values (");
            strInsert.Append("@ConsumableEquipmentID,@CompanyID,@DetailLocation,@AddressID,@Count,@Remark)");
            SqlParameter[] parameters = {
					new SqlParameter("@ConsumableEquipmentID", SqlDbType.BigInt,8),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@DetailLocation", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressID", SqlDbType.BigInt,8),
					new SqlParameter("@Count", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100)};

            foreach (ConsumableEquipmentDetailInfo model in consumableequipmentdetaillist)
            {
                parameters[0].Value = id;
                parameters[1].Value = string.IsNullOrEmpty(model.CompanyID) ? SqlString.Null : model.CompanyID;
                parameters[2].Value = string.IsNullOrEmpty(model.DetailLocation) ? SqlString.Null : model.DetailLocation;
                parameters[3].Value = model.AddressID == 0 ? SqlInt64.Null : model.AddressID;
                parameters[4].Value = model.Count == 0 ? SqlInt32.Null : model.Count;
                parameters[5].Value = string.IsNullOrEmpty(model.Remark) ? SqlString.Null : model.Remark;

                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strInsert.ToString(), parameters);
            }
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public void UpdateConsumableEquipmentTrans(ConsumableEquipmentInfo model, DbTransaction trans)
        {

            //先插入消息
            UpdateConsumableEquipment((SqlTransaction)trans, model);

            UpdateEquipmentDetail((SqlTransaction)trans, model.ConsumableEquipmentDetailList, model.ConsumableEquipmentID);
        }

        /// <summary>
        /// 更新消息
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="model"></param>
        private void UpdateConsumableEquipment(SqlTransaction trans, ConsumableEquipmentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_ConsumableEquipment set ");
            strSql.Append("ConsumableEquipmentNO=@ConsumableEquipmentNO,");
            strSql.Append("Name=@Name,");
            strSql.Append("SystemID=@SystemID,");
            strSql.Append("SerialNum=@SerialNum,");
            strSql.Append("Model=@Model,");
            strSql.Append("Specification=@Specification,");
            strSql.Append("AssertNumber=@AssertNumber,");
            strSql.Append("Unit=@Unit,");
            strSql.Append("Count=@Count,");
            strSql.Append("Price=@Price,");
            strSql.Append("SupplierID=@SupplierID,");
            strSql.Append("ProducerID=@ProducerID,");
            strSql.Append("SupplierName=@SupplierName,");
            strSql.Append("ProducerName=@ProducerName,");
            strSql.Append("FileDate=@FileDate,");
            strSql.Append("MaintenanceTimes=@MaintenanceTimes,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where ConsumableEquipmentID=@ConsumableEquipmentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ConsumableEquipmentID", SqlDbType.BigInt,8),
					new SqlParameter("@ConsumableEquipmentNO", SqlDbType.VarChar,20),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@SystemID", SqlDbType.VarChar,2),
					new SqlParameter("@SerialNum", SqlDbType.VarChar,30),
					new SqlParameter("@Model", SqlDbType.VarChar,20),
					new SqlParameter("@Specification", SqlDbType.NVarChar,60),
					new SqlParameter("@AssertNumber", SqlDbType.VarChar,50),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@Count", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@SupplierID", SqlDbType.BigInt,8),
					new SqlParameter("@ProducerID", SqlDbType.BigInt,8),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProducerName", SqlDbType.NVarChar,50),
					new SqlParameter("@FileDate", SqlDbType.DateTime),
					new SqlParameter("@MaintenanceTimes", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};

            parameters[0].Value = model.ConsumableEquipmentID;
            parameters[1].Value = string.IsNullOrEmpty(model.ConsumableEquipmentNO) ? SqlString.Null : model.ConsumableEquipmentNO;
            parameters[2].Value = string.IsNullOrEmpty(model.Name) ? SqlString.Null : model.Name;
            parameters[3].Value = string.IsNullOrEmpty(model.SystemID) ? SqlString.Null : model.SystemID;
            parameters[4].Value = string.IsNullOrEmpty(model.SerialNum) ? SqlString.Null : model.SerialNum;
            parameters[5].Value = string.IsNullOrEmpty(model.Model) ? SqlString.Null : model.Model;
            parameters[6].Value = string.IsNullOrEmpty(model.Specification) ? SqlString.Null : model.Specification;
            parameters[7].Value = string.IsNullOrEmpty(model.AssertNumber) ? SqlString.Null : model.AssertNumber;
            parameters[8].Value = string.IsNullOrEmpty(model.Unit) ? SqlString.Null : model.Unit;
            parameters[9].Value = model.Count == 0 ? SqlInt32.Null : model.Count;
            parameters[10].Value = model.Price;
            parameters[11].Value = model.SupplierID == 0 ? SqlInt64.Null : model.SupplierID;
            parameters[12].Value = model.ProducerID == 0 ? SqlInt64.Null : model.ProducerID;
            parameters[13].Value = string.IsNullOrEmpty(model.SupplierName) ? SqlString.Null : model.SupplierName;
            parameters[14].Value = string.IsNullOrEmpty(model.ProducerName) ? SqlString.Null : model.ProducerName;
            parameters[15].Value = DateTime.Compare(model.FileDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : model.FileDate;
            parameters[16].Value = model.MaintenanceTimes;
            parameters[17].Value = string.IsNullOrEmpty(model.Remark) ? SqlString.Null : model.Remark;
            parameters[18].Value = DateTime.Compare(model.UpdateTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : model.UpdateTime;


            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        }


        public IList<ConsumableEquipmentInfo> Search(ConsumableEquipmentInfo item)
        {
            QueryParam qp = GenerateSearchTerm(item);
            string cmd = "select " + qp.ReturnFields + " from " + qp.TableName + " " + qp.Where + qp.OrderBy;
            List<ConsumableEquipmentInfo> list = new List<ConsumableEquipmentInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                    {
                        list.Add(GetData(rd));
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("搜索设备信息失败", e);
            }


            return list;
        }

        /// <summary>
        /// 获取当前查询条件下的设备总量
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        public int GetCurrentDeviceCount(QueryParam term, string companyid)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = "FM2E_ConsumableEquipmentDetail";
                    term.ReturnFields = "ConsumableEquipmentID,ConsumableEquipmentNO,Name,SystemID,SerialNum,Model,Specification,AssertNumber,Unit,Count,Price,SupplierID,ProducerID,SupplierName,ProducerName,FileDate,MaintenanceTimes,Remark,UpdateTime";
                    term.OrderBy = "";
                    string Where = "where 1=1";
                    if (companyid != null && companyid != string.Empty)
                        term.Where = Where + " and CompanyID = '" + companyid + "' ";
                    else
                        term.Where = Where;
                }
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select sum([Count]) as CurrentCount from ");
                strSql.Append(term.TableName + " ");
                strSql.Append(term.Where);
                int CurrentCount = 0;
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    if (rd.Read())
                    {
                        if (!Convert.IsDBNull(rd["CurrentCount"]))
                            CurrentCount = Convert.ToInt32(rd["CurrentCount"]);
                    }
                }
                return CurrentCount;
            }
            catch (Exception e)
            {
                throw new DALException(" 获取设备分页失败", e);
            }
        }



        #endregion  成员方法

        #region 修改 By Tianmu
        private const string TABLE_UNION = "FM2E_ConsumableEquipment a INNER JOIN FM2E_ConsumableEquipmentDetail b on a.ConsumableEquipmentID = b.ConsumableEquipmentID INNER JOIN FM2E_WarehouseView c on b.AddressID = c.AddressID ";
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
        public decimal AddExpendasExpendable(DbTransaction trans, string companyid, string warehouseid, string productname, string model, string unit, decimal price, decimal count)
        {
            decimal storage = count;


            //先查询是否存在
            WareHouseConsumableEquipmentInfo item = null;
            item = GetTopExpendableItem((SqlTransaction)trans, companyid, warehouseid, productname, model, unit);

            if (item != null)//修改
            {
                item.Count += Convert.ToInt32(count);
                //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
                if (item.Count < 0)
                    return -1;
                //********** Modification Finished 2011-09-09 **********************************************************************************************
                if (price != 0)
                    item.Price = price;
                storage = item.Count;
                UpdateExpendasExpendable((SqlTransaction)trans, item);
            }
            else//插入
            {
                item = new WareHouseConsumableEquipmentInfo();
                item.CompanyID = companyid;
                item.Count = Convert.ToInt32(count);
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
                item.WareHouseID = warehouseid;
                InsertExpendasExpendable((SqlTransaction)trans, item);
            }

            return storage;
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
        public WareHouseConsumableEquipmentInfo GetTopExpendableItem(SqlTransaction trans, string companyid, string WarehouseID, string Name, string Model, string unit)
        {
            WareHouseConsumableEquipmentInfo item = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from " + TABLE_UNION + " ");
            strSql.Append(" where b.CompanyID=@CompanyID ");
            strSql.Append(" and   WarehouseID=@WarehouseID ");
            strSql.Append(" and   c.Name=@Name ");
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
                        item = GetExpendasData(dr);
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
        /// 删除一条数据
        /// </summary>
        public void DeleteExpendasExpendable(long ConsumableEquipmentID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from FM2E_ConsumableEquipment ");
            strSql.Append(" where ConsumableEquipmentID=@ConsumableEquipmentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ConsumableEquipmentID", SqlDbType.BigInt)};
            parameters[0].Value = ConsumableEquipmentID;

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
        public WareHouseConsumableEquipmentInfo GetExpendasExpendable(long ConsumableEquipmentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,c.AddressID,c.AddressCode,c.AddressName,c.Name WareHouseName,WareHouseID ");
            strSql.Append(" from FM2E_ConsumableEquipment a INNER JOIN FM2E_ConsumableEquipmentDetail b on a.ConsumableEquipmentID = b.ConsumableEquipmentID INNER JOIN FM2E_WarehouseView c on b.AddressID = c.AddressID ");
            strSql.Append(" where a.ConsumableEquipmentID=@ConsumableEquipmentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ConsumableEquipmentID", SqlDbType.BigInt)};
            parameters[0].Value = ConsumableEquipmentID;
            WareHouseConsumableEquipmentInfo item = new WareHouseConsumableEquipmentInfo();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        item = this.GetExpendasData(rd);

                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取消耗品信息失败", e);
            }
            return item;
        }

        public void UpdateExpendasExpendable(WareHouseConsumableEquipmentInfo model, DbTransaction trans)
        {

            //先插入消息
            UpdateExpendasExpendable((SqlTransaction)trans, model);

            UpdateExpendasDetail((SqlTransaction)trans, model.WareHouseConsumableEquipmentDetailList, model.ConsumableEquipmentID);
        }
        /// <summary>
        /// 更新消息
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="model"></param>
        private void UpdateExpendasExpendable(SqlTransaction trans, WareHouseConsumableEquipmentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_ConsumableEquipment set ");
            strSql.Append("ConsumableEquipmentNO=@ConsumableEquipmentNO,");
            strSql.Append("Name=@Name,");
            strSql.Append("SystemID=@SystemID,");
            strSql.Append("SerialNum=@SerialNum,");
            strSql.Append("Model=@Model,");
            strSql.Append("Specification=@Specification,");
            strSql.Append("AssertNumber=@AssertNumber,");
            strSql.Append("Unit=@Unit,");
            strSql.Append("Count=@Count,");
            strSql.Append("Price=@Price,");
            strSql.Append("SupplierID=@SupplierID,");
            strSql.Append("ProducerID=@ProducerID,");
            strSql.Append("SupplierName=@SupplierName,");
            strSql.Append("ProducerName=@ProducerName,");
            strSql.Append("FileDate=@FileDate,");
            strSql.Append("MaintenanceTimes=@MaintenanceTimes,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where ConsumableEquipmentID=@ConsumableEquipmentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ConsumableEquipmentID", SqlDbType.BigInt,8),
					new SqlParameter("@ConsumableEquipmentNO", SqlDbType.VarChar,20),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@SystemID", SqlDbType.VarChar,2),
					new SqlParameter("@SerialNum", SqlDbType.VarChar,30),
					new SqlParameter("@Model", SqlDbType.VarChar,20),
					new SqlParameter("@Specification", SqlDbType.NVarChar,60),
					new SqlParameter("@AssertNumber", SqlDbType.VarChar,50),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@Count", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@SupplierID", SqlDbType.BigInt,8),
					new SqlParameter("@ProducerID", SqlDbType.BigInt,8),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProducerName", SqlDbType.NVarChar,50),
					new SqlParameter("@FileDate", SqlDbType.DateTime),
					new SqlParameter("@MaintenanceTimes", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};

            parameters[0].Value = model.ConsumableEquipmentID;
            parameters[1].Value = string.IsNullOrEmpty(model.ConsumableEquipmentNO) ? SqlString.Null : model.ConsumableEquipmentNO;
            parameters[2].Value = string.IsNullOrEmpty(model.Name) ? SqlString.Null : model.Name;
            parameters[3].Value = string.IsNullOrEmpty(model.SystemID) ? SqlString.Null : model.SystemID;
            parameters[4].Value = string.IsNullOrEmpty(model.SerialNum) ? SqlString.Null : model.SerialNum;
            parameters[5].Value = string.IsNullOrEmpty(model.Model) ? SqlString.Null : model.Model;
            parameters[6].Value = string.IsNullOrEmpty(model.Specification) ? SqlString.Null : model.Specification;
            parameters[7].Value = string.IsNullOrEmpty(model.AssertNumber) ? SqlString.Null : model.AssertNumber;
            parameters[8].Value = string.IsNullOrEmpty(model.Unit) ? SqlString.Null : model.Unit;
            parameters[9].Value = model.Count == 0 ? SqlInt32.Null : model.Count;
            parameters[10].Value = model.Price;
            parameters[11].Value = model.SupplierID == 0 ? SqlInt64.Null : model.SupplierID;
            parameters[12].Value = model.ProducerID == 0 ? SqlInt64.Null : model.ProducerID;
            parameters[13].Value = string.IsNullOrEmpty(model.SupplierName) ? SqlString.Null : model.SupplierName;
            parameters[14].Value = string.IsNullOrEmpty(model.ProducerName) ? SqlString.Null : model.ProducerName;
            parameters[15].Value = DateTime.Compare(model.FileDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : model.FileDate;
            parameters[16].Value = model.MaintenanceTimes;
            parameters[17].Value = string.IsNullOrEmpty(model.Remark) ? SqlString.Null : model.Remark;
            parameters[18].Value = DateTime.Compare(model.UpdateTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : model.UpdateTime;


            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        }
        public long InsertExpendasExpendable(WareHouseConsumableEquipmentInfo model, DbTransaction trans)
        {

            //先插入消息
            long id = InsertExpendasExpendable((SqlTransaction)trans, model);

            //插入消息对象列表
            UpdateExpendasDetail((SqlTransaction)trans, model.WareHouseConsumableEquipmentDetailList, id);

            return id;

        }
        private long InsertExpendasExpendable(SqlTransaction trans, WareHouseConsumableEquipmentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_ConsumableEquipment(");
            strSql.Append("ConsumableEquipmentNO,Name,SystemID,SerialNum,Model,Specification,AssertNumber,Unit,Count,Price,SupplierID,ProducerID,SupplierName,ProducerName,FileDate,MaintenanceTimes,Remark,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@ConsumableEquipmentNO,@Name,@SystemID,@SerialNum,@Model,@Specification,@AssertNumber,@Unit,@Count,@Price,@SupplierID,@ProducerID,@SupplierName,@ProducerName,@FileDate,@MaintenanceTimes,@Remark,@UpdateTime)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@ConsumableEquipmentNO", SqlDbType.VarChar,20),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@SystemID", SqlDbType.VarChar,2),
					new SqlParameter("@SerialNum", SqlDbType.VarChar,30),
					new SqlParameter("@Model", SqlDbType.VarChar,20),
					new SqlParameter("@Specification", SqlDbType.NVarChar,60),
					new SqlParameter("@AssertNumber", SqlDbType.VarChar,50),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@Count", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@SupplierID", SqlDbType.BigInt,8),
					new SqlParameter("@ProducerID", SqlDbType.BigInt,8),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProducerName", SqlDbType.NVarChar,50),
					new SqlParameter("@FileDate", SqlDbType.DateTime),
					new SqlParameter("@MaintenanceTimes", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};
            parameters[0].Value = string.IsNullOrEmpty(model.ConsumableEquipmentNO) ? SqlString.Null : model.ConsumableEquipmentNO;
            parameters[1].Value = string.IsNullOrEmpty(model.Name) ? SqlString.Null : model.Name;
            parameters[2].Value = string.IsNullOrEmpty(model.SystemID) ? SqlString.Null : model.SystemID;
            parameters[3].Value = string.IsNullOrEmpty(model.SerialNum) ? SqlString.Null : model.SerialNum;
            parameters[4].Value = string.IsNullOrEmpty(model.Model) ? SqlString.Null : model.Model;
            parameters[5].Value = string.IsNullOrEmpty(model.Specification) ? SqlString.Null : model.Specification;
            parameters[6].Value = string.IsNullOrEmpty(model.AssertNumber) ? SqlString.Null : model.AssertNumber;
            parameters[7].Value = string.IsNullOrEmpty(model.Unit) ? SqlString.Null : model.Unit;
            parameters[8].Value = model.Count == 0 ? SqlInt32.Null : model.Count;
            parameters[9].Value = model.Price;
            parameters[10].Value = model.SupplierID == 0 ? SqlInt64.Null : model.SupplierID;
            parameters[11].Value = model.ProducerID == 0 ? SqlInt64.Null : model.ProducerID;
            parameters[12].Value = string.IsNullOrEmpty(model.SupplierName) ? SqlString.Null : model.SupplierName;
            parameters[13].Value = string.IsNullOrEmpty(model.ProducerName) ? SqlString.Null : model.ProducerName;
            parameters[14].Value = DateTime.Compare(model.FileDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : model.FileDate;
            parameters[15].Value = model.MaintenanceTimes;
            parameters[16].Value = string.IsNullOrEmpty(model.Remark) ? SqlString.Null : model.Remark;
            parameters[17].Value = DateTime.Compare(model.UpdateTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : model.UpdateTime;

            long id = (long)SQLHelper.ExecuteScalar(trans, CommandType.Text, strSql.ToString(), parameters);
            return id;
        }
        private void UpdateExpendasDetail(SqlTransaction trans, IList warehouseConsumableEquipmentDetailList, long id)
        {
            //先删除
            StringBuilder strDel = new StringBuilder();
            strDel.Append("delete from FM2E_ConsumableEquipmentDetail ");
            strDel.Append(" where ConsumableEquipmentID=@ConsumableEquipmentID ");
            SqlParameter[] paramDel = {
					new SqlParameter("@ConsumableEquipmentID", SqlDbType.BigInt)};
            paramDel[0].Value = id;
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strDel.ToString(), paramDel);

            if (warehouseConsumableEquipmentDetailList == null || warehouseConsumableEquipmentDetailList.Count == 0)
                return;

            //后插入
            StringBuilder strInsert = new StringBuilder();
            strInsert.Append("insert into FM2E_ConsumableEquipmentDetail(");
            strInsert.Append("ConsumableEquipmentID,CompanyID,DetailLocation,AddressID,Count,Remark)");
            strInsert.Append(" values (");
            strInsert.Append("@ConsumableEquipmentID,@CompanyID,@DetailLocation,@AddressID,@Count,@Remark)");
            SqlParameter[] parameters = {
					new SqlParameter("@ConsumableEquipmentID", SqlDbType.BigInt,8),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@DetailLocation", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressID", SqlDbType.BigInt,8),
					new SqlParameter("@Count", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100)};
            try
            {
                foreach (WareHouseConsumableEquipmentInfo model in warehouseConsumableEquipmentDetailList)
                {
                    parameters[0].Value = id;
                    parameters[1].Value = string.IsNullOrEmpty(model.CompanyID) ? SqlString.Null : model.CompanyID;
                    parameters[2].Value = string.IsNullOrEmpty(model.DetailLocation) ? SqlString.Null : model.DetailLocation;
                    parameters[3].Value = model.AddressID == 0 ? SqlInt64.Null : model.AddressID;
                    parameters[4].Value = model.Count == 0 ? SqlInt32.Null : model.Count;
                    parameters[5].Value = string.IsNullOrEmpty(model.Remark) ? SqlString.Null : model.Remark;

                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strInsert.ToString(), parameters);
                }
            }
            catch (Exception ex)
            { }
        }
        public ConsumableEquipmentDetailInfo GetEquipmentDetailByWarehouseID(string warehouseID, DbTransaction trans)
        {
            long id = GetDetailAddressIDByWarehouseID((SqlTransaction)trans, warehouseID);
            ConsumableEquipmentDetailInfo ced = new ConsumableEquipmentDetailInfo();

            ced = GetEquipmentDetailByAddressID((SqlTransaction)trans, id);

            return ced;

        }
        private long GetDetailAddressIDByWarehouseID(SqlTransaction trans, string id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AddressID ");
            strSql.Append("from FM2E_WarehouseView ");
            strSql.Append("where WareHouseID = @WareHouseID");
            SqlParameter[] parameters = {
                                            new SqlParameter("@WareHouseID",SqlDbType.VarChar,2)
                                        };
            parameters[0].Value = id;

            long Addressid = (long)SQLHelper.ExecuteScalar(trans, CommandType.Text, strSql.ToString(), parameters);
            return Addressid;
        }
        private ConsumableEquipmentDetailInfo GetEquipmentDetailByAddressID(SqlTransaction trans, long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ID,ConsumableEquipmentID,CompanyID,DetailLocation,AddressID,Count,Remark ");
            strSql.Append(" FROM FM2E_ConsumableEquipmentDetail ");
            strSql.Append(" where AddressID=@AddressID ");
            SqlParameter[] parameters = {
					new SqlParameter("@AddressID", SqlDbType.BigInt)};
            parameters[0].Value = id;
            DataTable dt = new DataTable();
            dt = SQLHelper.GetObjectList(trans, CommandType.Text, strSql.ToString(), parameters);
            ConsumableEquipmentDetailInfo ced = new ConsumableEquipmentDetailInfo();
            ced.ID = Convert.ToInt64(dt.Rows[0][0]);
            ced.ConsumableEquipmentID = Convert.ToInt64(dt.Rows[0][1]);
            ced.CompanyID = Convert.ToString(dt.Rows[0][2]);
            ced.DetailLocation = Convert.ToString(dt.Rows[0][3]);
            ced.AddressID = Convert.ToInt64(dt.Rows[0][4]);
            ced.Count = Convert.ToInt32(dt.Rows[0][5]);
            ced.Remark = Convert.ToString(dt.Rows[0][6]);


            return ced;
        }
        public QueryParam GenerateExpendasSearchTerm(WareHouseConsumableEquipmentInfo item)
        {
            string sqlSearch = "where 1=1";
            if (!string.IsNullOrEmpty(item.CompanyID))
                sqlSearch += " b.CompanyID = '" + item.CompanyID + "') ";

            if (item.ConsumableEquipmentID != 0)
            {
                sqlSearch += " and a.ConsumableEquipmentID =" + item.ConsumableEquipmentID;
            }

            if (item.ConsumableEquipmentNO != null && item.ConsumableEquipmentNO.Trim() != string.Empty)
            {
                sqlSearch += " and a.ConsumableEquipmentNO like '%" + item.ConsumableEquipmentNO.Trim() + "%'";
            }

            if (item.Name != null && item.Name.Trim() != string.Empty)
            {
                sqlSearch += " and a.Name like '%" + item.Name.Trim() + "%'";
            }

            if (item.SystemID != null && item.SystemID.Trim() != string.Empty)
            {
                sqlSearch += " and SystemID like '%" + item.SystemID.Trim() + "%'";
            }

            if (item.SerialNum != null && item.SerialNum.Trim() != string.Empty)
            {
                sqlSearch += " and SerialNum like '%" + item.SerialNum.Trim() + "%'";
            }

            if (item.Model != null && item.Model.Trim() != string.Empty)
            {
                sqlSearch += " and Model like '%" + item.Model.Trim() + "%'";
            }

            if (item.Specification != null && item.Specification.Trim() != string.Empty)
            {
                sqlSearch += " and Specification like '%" + item.Specification.Trim() + "%'";
            }

            if (item.AssertNumber != null && item.AssertNumber.Trim() != string.Empty)
            {
                sqlSearch += " and AssertNumber like '%" + item.AssertNumber.Trim() + "%'";
            }

            if (item.Unit != null && item.Unit.Trim() != string.Empty)
            {
                sqlSearch += " and Unit like '%" + item.Unit.Trim() + "%'";
            }

            if (item.Count != 0)
            {
                sqlSearch += " and a.Count =" + item.Count;
            }

            if (item.Price != 0)
            {
                sqlSearch += " and Price =" + item.Price;
            }

            if (item.SupplierID != 0)
            {
                sqlSearch += " and SupplierID =" + item.SupplierID;
            }

            if (item.ProducerID != 0)
            {
                sqlSearch += " and ProducerID =" + item.ProducerID;
            }

            if (item.SupplierName != null && item.SupplierName.Trim() != string.Empty)
            {
                sqlSearch += " and SupplierName like '%" + item.SupplierName.Trim() + "%'";
            }

            if (item.ProducerName != null && item.ProducerName.Trim() != string.Empty)
            {
                sqlSearch += " and ProducerName like '%" + item.ProducerName.Trim() + "%'";
            }


            if (item.MaintenanceTimes != 0)
            {
                sqlSearch += " and MaintenanceTimes =" + item.MaintenanceTimes;
            }

            if (item.Remark != null && item.Remark.Trim() != string.Empty)
            {
                sqlSearch += " and a.Remark like '%" + item.Remark.Trim() + "%'";
            }
            if (item.AddressID != 0)
            {
                sqlSearch += " and c.AddressID = " + item.AddressID;
            }
            if (item.AddressCode != null && item.AddressCode.Trim() != string.Empty)
            {
                sqlSearch += " and AddressCode = '" + item.AddressCode + "'";
            }
            if (item.AddressName != null && item.AddressName.Trim() != string.Empty)
            {
                sqlSearch += " and AddressName like '% " + item.AddressName + "%'";
            }
            if (item.WareHouseID != null && item.WareHouseID.Trim() != string.Empty)
            {
                sqlSearch += " and WareHouseID = '" + item.WareHouseID + "'";
            }
            if (item.WareHouseName != null && item.WareHouseName.Trim() != string.Empty)
            {
                sqlSearch += " and c.Name like '% " + item.WareHouseName + "%'";
            }
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_ConsumableEquipment a INNER JOIN FM2E_ConsumableEquipmentDetail b on a.ConsumableEquipmentID = b.ConsumableEquipmentID INNER JOIN FM2E_WarehouseView c on b.AddressID = c.AddressID ";
            searchTerm.ReturnFields = "a.*,c.AddressID,c.AddressCode,c.AddressName,c.Name WareHouseName,WareHouseID ";
            //searchTerm.PageSize = 10;
            searchTerm.OrderBy = " ";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        /// <summary>
        /// 仓库设备易耗品信息实体
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private WareHouseConsumableEquipmentInfo GetExpendasData(IDataReader rd)
        {
            WareHouseConsumableEquipmentInfo item = new WareHouseConsumableEquipmentInfo();

            if (!Convert.IsDBNull(rd["ConsumableEquipmentID"]))
                item.ConsumableEquipmentID = Convert.ToInt64(rd["ConsumableEquipmentID"]);

            if (!Convert.IsDBNull(rd["ConsumableEquipmentNO"]))
                item.ConsumableEquipmentNO = Convert.ToString(rd["ConsumableEquipmentNO"]);

            if (!Convert.IsDBNull(rd["Name"]))
                item.Name = Convert.ToString(rd["Name"]);

            if (!Convert.IsDBNull(rd["SystemID"]))
                item.SystemID = Convert.ToString(rd["SystemID"]);

            if (!Convert.IsDBNull(rd["SerialNum"]))
                item.SerialNum = Convert.ToString(rd["SerialNum"]);

            if (!Convert.IsDBNull(rd["Model"]))
                item.Model = Convert.ToString(rd["Model"]);

            if (!Convert.IsDBNull(rd["Specification"]))
                item.Specification = Convert.ToString(rd["Specification"]);

            if (!Convert.IsDBNull(rd["AssertNumber"]))
                item.AssertNumber = Convert.ToString(rd["AssertNumber"]);

            if (!Convert.IsDBNull(rd["Unit"]))
                item.Unit = Convert.ToString(rd["Unit"]);

            if (!Convert.IsDBNull(rd["Count"]))
                item.Count = Convert.ToInt32(rd["Count"]);

            if (!Convert.IsDBNull(rd["Price"]))
                item.Price = Convert.ToDecimal(rd["Price"]);

            if (!Convert.IsDBNull(rd["SupplierID"]))
                item.SupplierID = Convert.ToInt64(rd["SupplierID"]);

            if (!Convert.IsDBNull(rd["ProducerID"]))
                item.ProducerID = Convert.ToInt64(rd["ProducerID"]);

            if (!Convert.IsDBNull(rd["SupplierName"]))
                item.SupplierName = Convert.ToString(rd["SupplierName"]);

            if (!Convert.IsDBNull(rd["ProducerName"]))
                item.ProducerName = Convert.ToString(rd["ProducerName"]);

            if (!Convert.IsDBNull(rd["FileDate"]))
                item.FileDate = Convert.ToDateTime(rd["FileDate"]);

            if (!Convert.IsDBNull(rd["MaintenanceTimes"]))
                item.MaintenanceTimes = Convert.ToInt32(rd["MaintenanceTimes"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["UpdateTime"]))
                item.UpdateTime = Convert.ToDateTime(rd["UpdateTime"]);

            if (!Convert.IsDBNull(rd["AddressID"]))
                item.AddressID = Convert.ToInt64(rd["AddressID"]);
            if (!Convert.IsDBNull(rd["AddressName"]))
                item.AddressName = Convert.ToString(rd["AddressName"]);
            //if (!Convert.IsDBNull(rd["DetailLocation"]))
            //    item.DetailLocation = Convert.ToString(rd["DetailLocation"]);
            if (!Convert.IsDBNull(rd["WareHouseID"]))
                item.WareHouseID = Convert.ToString(rd["WareHouseID"]);
            if (!Convert.IsDBNull(rd["AddressCode"]))
                item.AddressCode = Convert.ToString(rd["AddressCode"]);
            if (!Convert.IsDBNull(rd["WareHouseName"]))
                item.WareHouseName = Convert.ToString(rd["WareHouseName"]);


            return item;

        }

        public IList GetExpendasListByWarehouseID(QueryParam searchTerm, out int recordCount, string wareHouseID)
        {
            if (searchTerm.Where == "")
            {
                string[] wareHouseIDList = wareHouseID.Split(',');
                searchTerm.TableName = TABLE_UNION;
                searchTerm.ReturnFields = "a.*,c.AddressID,c.AddressCode,c.AddressName,c.Name WareHouseName,WareHouseID ";
                searchTerm.OrderBy = "";
                searchTerm.Where = "where WareHouseID in ( ''";
                foreach (string whID in wareHouseIDList)
                {
                    if (whID != "")
                    {
                        searchTerm.Where = searchTerm.Where + ",'" + whID + "'";
                    }
                }
                searchTerm.Where = searchTerm.Where + ")";

            }
            return SQLHelper.GetObjectList(this.GetExpendasData, searchTerm, out recordCount);
        }
        public IList GetExpendasList(QueryParam searchTerm, out int recordCount)
        {
            if (searchTerm.Where == "")
            {
                searchTerm.TableName = "FM2E_ConsumableEquipment a INNER JOIN FM2E_ConsumableEquipmentDetail b on a.ConsumableEquipmentID = b.ConsumableEquipmentID INNER JOIN FM2E_WarehouseView c on b.AddressID = c.AddressID ";
                searchTerm.ReturnFields = "a.*,c.AddressID,c.AddressCode,c.AddressName,c.Name WareHouseName,WareHouseID ";
                searchTerm.OrderBy = "";
                searchTerm.Where = " ";
            }
            return SQLHelper.GetObjectList(this.GetExpendasData, searchTerm, out recordCount);
        }

        /// <summary>
        /// 获取当前查询条件下的设备总量
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        public int GetCurrentExpendasDeviceCount(QueryParam term, string companyid)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = "FM2E_ConsumableEquipment a INNER JOIN FM2E_ConsumableEquipmentDetail b on a.ConsumableEquipmentID = b.ConsumableEquipmentID INNER JOIN FM2E_WarehouseView c on b.AddressID = c.AddressID ";

                    term.ReturnFields = "a.*,b.CompanyID,c.Name WarehouseName";
                    term.OrderBy = "";
                    string Where = "where 1=1";
                    if (companyid != null && companyid != string.Empty)
                        term.Where = Where + " and CompanyID = '" + companyid + "' ";
                    else
                        term.Where = Where;
                }
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select sum(a.Count) as CurrentCount from ");
                strSql.Append(term.TableName + " ");
                strSql.Append(term.Where);
                int CurrentCount = 0;
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    if (rd.Read())
                    {
                        if (!Convert.IsDBNull(rd["CurrentCount"]))
                            CurrentCount = Convert.ToInt32(rd["CurrentCount"]);
                    }
                }
                return CurrentCount;
            }
            catch (Exception e)
            {
                throw new DALException(" 获取设备分页失败", e);
            }
        }


        /// <summary>
        /// 获取导出仓库设备易耗品信息列表
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public IList GetExportList(QueryParam searchTerm)
        {
            List<WareHouseConsumableEquipmentInfo> list = new List<WareHouseConsumableEquipmentInfo>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("Select a.*,b.*,c.AddressID,c.AddressCode,c.AddressName,c.Name WareHouseName,WareHouseID ");
                strSql.Append(" from FM2E_ConsumableEquipment a INNER JOIN FM2E_ConsumableEquipmentDetail b on a.ConsumableEquipmentID = b.ConsumableEquipmentID INNER JOIN FM2E_WarehouseView c on b.AddressID = c.AddressID  ");
                strSql.Append(searchTerm.Where);
                strSql.Append(" order by AddressName asc");

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        list.Add(this.GetExpendasData(rd));
                    }
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取卡片列表失败", ex);
            }

            return list;
        }
        #endregion
    }
}

