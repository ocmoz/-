using System;
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
using FM2E.SQLServerDAL.Equipment;
namespace FM2E.SQLServerDAL.Equipment
{
    public class ExpendableInOut:IExpendableInOut
    {
        private const string TABLE_EXPENDALBEINOUTRECORD = "FM2E_ExpendableInOutRecord";

        private const string TABLE_EXPENDALBEINOUT = "FM2E_ExpendableInOut";

        private const string VIEW_EXPENDABLEINOUT = "FM2E_ExpendableInOutView";

        private const string TABLE_EXPENDABLESHEET = "FM2E_ExpendableInOutsheet";

        private const string TABLE_EXPENDABLEMODIFY = "FM2E_ExpendableInOutModify";

        public long insertsheet(ExpendableSheet model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+TABLE_EXPENDABLESHEET+"(");
            strSql.Append("name,time,xinzhengyewu,zongheshiwu,jihuacaiwu,fenguanlingdao,zongjinli)");
            strSql.Append(" values (");
            strSql.Append("@name,@time,@xinzhengyewu,@zongheshiwu,@jihuacaiwu,@fenguanlingdao,@zongjinli)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@time", SqlDbType.DateTime),
					new SqlParameter("@xinzhengyewu", SqlDbType.NChar,10),
					new SqlParameter("@zongheshiwu", SqlDbType.NChar,10),
					new SqlParameter("@jihuacaiwu", SqlDbType.NChar,10),
					new SqlParameter("@fenguanlingdao", SqlDbType.NChar,10),
					new SqlParameter("@zongjinli", SqlDbType.NChar,10)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.time;
            parameters[2].Value = model.xinzhengyewu;
            parameters[3].Value = model.zongheshiwu;
            parameters[4].Value = model.jihuacaiwu;
            parameters[5].Value = model.fenguanlingdao;
            parameters[6].Value = model.zongjinli;

            long id = 0;
            using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rdr.Read())
                {
                    id = rdr.GetInt64(0);
                }
            }
            return id;
        }

        public long insertmodify(ExpendableModify model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_ExpendableInOutModify(");
            strSql.Append("SheetID,RecordID,equipmentname,modifytime,oldnum,newnum,userid,username,type)");
            strSql.Append(" values (");
            strSql.Append("@SheetID,@RecordID,@equipmentname,@modifytime,@oldnum,@newnum,@userid,@username,@type)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.BigInt,8),
					new SqlParameter("@RecordID", SqlDbType.BigInt,8),
					new SqlParameter("@equipmentname", SqlDbType.NVarChar,50),
					new SqlParameter("@modifytime", SqlDbType.DateTime),
					new SqlParameter("@oldnum", SqlDbType.Int,4),
					new SqlParameter("@newnum", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.NVarChar,50),
					new SqlParameter("@username", SqlDbType.NVarChar,50),
                    new SqlParameter("@type", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.SheetID;
            parameters[1].Value = model.RecordID;
            parameters[2].Value = model.equipmentname;
            parameters[3].Value = model.modifytime;
            parameters[4].Value = model.oldnum;
            parameters[5].Value = model.newnum;
            parameters[6].Value = model.userid;
            parameters[7].Value = model.username;
            parameters[8].Value = model.type;

            long id = 0;
            using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rdr.Read())
                {
                    id = rdr.GetInt64(0);
                }
            }
            return id;
        }

        public Boolean updaterecordamount(ExpendableInOutRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_ExpendableInOutRecord set ");
            strSql.Append("Amount=@Amount");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@Amount", SqlDbType.Decimal,9)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Amount;

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
                    throw new DALException("更新出入库记录信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }
            return true;
        }

        public Boolean updaterecord(ExpendableInOutRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_ExpendableInOutRecord set ");
            strSql.Append("Model=@Model,");
            strSql.Append("Amount=@Amount,");
            strSql.Append("Unit=@Unit,");
            strSql.Append("Price=@Price,");
            strSql.Append("CategoryID=@CategoryID,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("Type=@Type,");
            strSql.Append("SheetID=@SheetID,");
            strSql.Append("WarehouseID=@WarehouseID,");
            strSql.Append("WarehouseKeeper=@WarehouseKeeper,");
            strSql.Append("WarehouseKeeperName=@WarehouseKeeperName,");
            strSql.Append("Receiver=@Receiver,");
            strSql.Append("ReceiverName=@ReceiverName,");
            strSql.Append("InOutTime=@InOutTime,");
            strSql.Append("Name=@Name");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@Model", SqlDbType.NVarChar,50),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@CategoryID", SqlDbType.BigInt,8),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Type", SqlDbType.TinyInt,1),
					new SqlParameter("@SheetID", SqlDbType.BigInt,8),
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@WarehouseKeeper", SqlDbType.VarChar,20),
					new SqlParameter("@WarehouseKeeperName", SqlDbType.NVarChar,50),
					new SqlParameter("@Receiver", SqlDbType.VarChar,20),
					new SqlParameter("@ReceiverName", SqlDbType.NVarChar,50),
					new SqlParameter("@InOutTime", SqlDbType.DateTime),
					new SqlParameter("@Name", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Model;
            parameters[2].Value = model.Amount;
            parameters[3].Value = model.Unit;
            parameters[4].Value = model.Price;
            parameters[5].Value = model.CategoryID;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.CompanyID;
            parameters[8].Value = model.Type;
            parameters[9].Value = model.SheetID;
            parameters[10].Value = model.WarehouseID;
            parameters[11].Value = model.WarehouseKeeper;
            parameters[12].Value = model.WarehouseKeeperName;
            parameters[13].Value = model.Receiver;
            parameters[14].Value = model.ReceiverName;
            parameters[15].Value = model.InOutTime;
            parameters[16].Value = model.Name;

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
                    throw new DALException("更新出入库记录信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }
            return true;
        }

        public Boolean updatesheet(ExpendableSheet model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_ExpendableInOutsheet set ");
            strSql.Append("name=@name,");
            strSql.Append("time=@time,");
            strSql.Append("xinzhengyewu=@xinzhengyewu,");
            strSql.Append("zongheshiwu=@zongheshiwu,");
            strSql.Append("jihuacaiwu=@jihuacaiwu,");
            strSql.Append("fenguanlingdao=@fenguanlingdao,");
            strSql.Append("zongjinli=@zongjinli");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.BigInt,8),
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@time", SqlDbType.DateTime),
					new SqlParameter("@xinzhengyewu", SqlDbType.NChar,10),
					new SqlParameter("@zongheshiwu", SqlDbType.NChar,10),
					new SqlParameter("@jihuacaiwu", SqlDbType.NChar,10),
					new SqlParameter("@fenguanlingdao", SqlDbType.NChar,10),
					new SqlParameter("@zongjinli", SqlDbType.NChar,10)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.name;
            parameters[2].Value = model.time;
            parameters[3].Value = model.xinzhengyewu;
            parameters[4].Value = model.zongheshiwu;
            parameters[5].Value = model.jihuacaiwu;
            parameters[6].Value = model.fenguanlingdao;
            parameters[7].Value = model.zongjinli;

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
                    throw new DALException("更新出入库单号信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }
            return true;
        }

        public Boolean deleteAllRecord()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from FM2E_ExpendableInOutRecord ;");
            try
            {

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null);
            }
            catch (Exception e)
            {
                throw new DALException("删除导入前记录失败", e);
            }
            return true;
        }

        public Boolean updateCurrentApprovalStatus(InOutApproval item)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update InOutApproval ");
            strSql.Append(" set xingzhenyewu=@xingzhenyewu,zongheshiwu=@zongheshiwu,jihuacaiwu=@jihuacaiwu,fenguanlingdao=@fenguanlingdao,zongjingli=@zongjingli ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] param = new SqlParameter[]{
                   new SqlParameter("@ID",SqlDbType.BigInt,8),
                      new SqlParameter("@xingzhenyewu",SqlDbType.NChar,10),
                    new SqlParameter("@zongheshiwu",SqlDbType.NChar,10),
                    new SqlParameter("@jihuacaiwu",SqlDbType.NChar,10),
                    new SqlParameter("@fenguanlingdao",SqlDbType.NChar,10),
                    new SqlParameter("@zongjingli",SqlDbType.NChar,10)
             };
            param[0].Value = item.ID;
            param[1].Value = item.xingzhenyewu;
            param[2].Value = item.zongheshiwu;
            param[3].Value = item.jihuacaiwu;
            param[4].Value = item.fenguanlingdao;
            param[5].Value = item.zongjingli;
            try
            {

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), param);
            }
            catch (Exception e)
            {
                throw new DALException("更新审批信息失败", e);
            }
            return true;

        }


        public InOutApproval GetCurrentApprovalStatus()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1  * from InOutApproval ; ");
            InOutApproval item = new InOutApproval();
            Boolean hasitem = false;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    if (rd.Read())
                    {
                        item = this.GetApprovalData(rd);
                        hasitem = true;
                    }
                }
                if (!hasitem)
                {
                    StringBuilder insertstr = new StringBuilder();
                    insertstr.Append("insert into InOutApproval(xingzhenyewu,zongheshiwu,jihuacaiwu,fenguanlingdao,zongjingli) values ('','','','','');");
                    using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, insertstr.ToString(), null))
                    {
                    }
                    using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                    {
                        if (rd.Read())
                        {
                            item = this.GetApprovalData(rd);
                            hasitem = true;
                        }
                    }

                }
            }
            catch (Exception e)
            {
                throw new DALException("获取审批信息失败", e);
            }
            return item;
        }

        /// <summary>
        /// 读取所有导入审批前纪录
        /// </summary>
        public IList GetallInOutRecord(ExpendableInOutRecordType type)
        {
            ArrayList list = new ArrayList();
            StringBuilder strSql = new StringBuilder();
            //switch (type)
            //{
            //    case ExpendableInOutRecordType.In:
            //        strSql.Append("select  * from FM2E_ExpendableInOut where Type = 1 ;");
            //        break;
            //    case ExpendableInOutRecordType.Out:
            //        strSql.Append("select  * from FM2E_ExpendableInOut where Type = 2 ;");
            //        break;
            //}
            strSql.Append("select  * from FM2E_ExpendableInOutRecord where Type = " + Convert.ToInt32(type) + " ;");
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        ExpendableInOutRecordInfo item = this.GetData(rd);
                        list.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取所有消耗品失败", e);
            }
            return list;
        }

        /// <summary>
        /// 增加易耗品出库信息(一个设备)
        /// </summary>
        /// <param name="model"></param>
        public long InsertOutWarehouseExpendable(OutWarehouseInfo model, InEquipmentsInfo item)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            Equipment eqDal = new Equipment();
            Expendable expDal = new Expendable();
            long thisID = 0;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先插入出库信息;
                thisID = InsertOutWarehouseItem(model, trans);

                //插入出库明细信息，并更新对应的地址信息、易耗品信息;
                item.ID = thisID;
                item.ItemID = AddOutEquipmentsForExpendable(item, trans);

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

            return thisID;
        }


        /// <summary>
        /// 增加一条易耗品出库申请;
        /// </summary>
        public long InsertOutWarehouseItem(OutWarehouseInfo model, DbTransaction trans)
        {
            long id = 1;
            SqlDataReader rdr = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FM2E_OutWarehouse(");
                strSql.Append("IsDeleted,SheetName,WarehouseID,CompanyID,DepartmentID,SubmitTime,ApplicantID,OperatorID,Remark,Attachment)");
                strSql.Append(" values (");
                strSql.Append("@IsDeleted,@SheetName,@WarehouseID,@CompanyID,@DepartmentID,@SubmitTime,@ApplicantID,@OperatorID,@Remark,@Attachment)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
					new SqlParameter("@SheetName", SqlDbType.NVarChar,20),
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@DepartmentID", SqlDbType.BigInt,8),
					new SqlParameter("@SubmitTime", SqlDbType.DateTime),
					new SqlParameter("@ApplicantID", SqlDbType.VarChar,20),
					new SqlParameter("@OperatorID", SqlDbType.VarChar,20),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),                    
                    new SqlParameter("@Attachment", SqlDbType.NVarChar,100)};

                parameters[0].Value = model.IsDeleted;
                parameters[1].Value = model.SheetName;
                parameters[2].Value = model.WarehouseID;
                parameters[3].Value = model.CompanyID;
                parameters[4].Value = model.DepartmentID;
                parameters[5].Value = model.SubmitTime;
                parameters[6].Value = model.ApplicantID;
                parameters[7].Value = model.OperatorID;
                parameters[8].Value = model.Remark;
                parameters[9].Value = string.IsNullOrEmpty(model.Attachment) ? SqlString.Null : model.Attachment;
                //读取ID
                using (rdr = SQLHelper.ExecuteReader((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        if (!Convert.IsDBNull(rdr[0]))
                            id = Convert.ToInt64(rdr[0]);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("添加出库申请失败", e);
            }
            finally
            {
                rdr.Close();
            }
            return id;
        }

        public long AddOutEquipmentsForExpendable(InEquipmentsInfo model, DbTransaction trans)
        {
            try
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FM2E_OutEquipmentsForExpendable(");
                strSql.Append("ID,WarehouseID,IsAsset,EquipmentNO,ExpendableID,Count,[Unit],OutTime,[Name],Model,ExpendableTypeID,ExpendablePrice,ExpendableType)");
                strSql.Append(" values (");
                strSql.Append("@ID,@WarehouseID,@IsAsset,@EquipmentNO,@ExpendableID,@Count,@Unit,@OutTime,@Name,@Model,@ExpendableTypeID,@ExpendablePrice,@ExpendableType)");
                strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@IsAsset", SqlDbType.Bit,1),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
					new SqlParameter("@ExpendableID", SqlDbType.BigInt,8),
					new SqlParameter("@Count", SqlDbType.Decimal,9),
                    new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@OutTime", SqlDbType.DateTime),
                    new SqlParameter("@Name",SqlDbType.NVarChar,20),
                    new SqlParameter("@Model",SqlDbType.NVarChar,20),
                    new SqlParameter("@ExpendableTypeID",SqlDbType.BigInt),
                    new SqlParameter("@ExpendablePrice",SqlDbType.Decimal,10),
                    
                    new SqlParameter("@ExpendableType",SqlDbType.NVarChar,50)};
                parameters[0].Value = model.ID;
                parameters[1].Value = model.WarehouseID;
                parameters[2].Value = model.IsAsset;
                parameters[3].Value = string.IsNullOrEmpty(model.EquipmentNO) ? SqlString.Null : model.EquipmentNO;
                parameters[4].Value = model.ExpendableID == 0 ? SqlInt64.Null : model.ExpendableID;
                parameters[5].Value = model.Count;
                parameters[6].Value = string.IsNullOrEmpty(model.Unit) ? SqlString.Null : model.Unit;
                parameters[7].Value = model.InTime == DateTime.MinValue ? SqlDateTime.Null : model.InTime;
                parameters[8].Value = string.IsNullOrEmpty(model.Name) ? SqlString.Null : model.Name;
                parameters[9].Value = string.IsNullOrEmpty(model.Model) ? SqlString.Null : model.Model;
                parameters[10].Value = model.ExpendableTypeID;
                parameters[11].Value = model.ExpendablePrice;
                parameters[12].Value = model.ExpendableType;

                long id = 0;
                using (SqlDataReader rdr = SQLHelper.ExecuteReader((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
                model.ItemID = id;
                return id;
            }
            catch (Exception e)
            {
                throw new DALException("添加一条出库明细信息失败" + e.Message, e);
            }
        }

        /// <summary>
        /// 增加一条易耗品出入库明细（旧的，无申请单）
        /// </summary>
        public long Add(ExpendableInOutRecordInfo model, DbTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+TABLE_EXPENDALBEINOUT+"(");
            strSql.Append("Model,Amount,Unit,Price,CategoryID,Remark,Type,WarehouseID,WarehouseKeeper,WarehouseKeeperName,Receiver,ReceiverName,InOutTime,Name,CompanyID)");
            strSql.Append(" values (");
            strSql.Append("@Model,@Amount,@Unit,@Price,@CategoryID,@Remark,@Type,@WarehouseID,@WarehouseKeeper,@WarehouseKeeperName,@Receiver,@ReceiverName,@InOutTime,@Name,@CompanyID)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@Model", SqlDbType.NVarChar,50),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@CategoryID", SqlDbType.BigInt,8),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
					new SqlParameter("@Type", SqlDbType.TinyInt,1),
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@WarehouseKeeper", SqlDbType.VarChar,20),
					new SqlParameter("@WarehouseKeeperName", SqlDbType.NVarChar,50),
					new SqlParameter("@Receiver", SqlDbType.VarChar,20),
					new SqlParameter("@ReceiverName", SqlDbType.NVarChar,50),
					new SqlParameter("@InOutTime", SqlDbType.DateTime),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@CompanyID",SqlDbType.VarChar,2)};
            parameters[0].Value = string.IsNullOrEmpty(model.Model) ? SqlString.Null : model.Model;
            parameters[1].Value = model.Amount;
            parameters[2].Value = string.IsNullOrEmpty(model.Unit) ? SqlString.Null : model.Unit;
            parameters[3].Value = model.Price;
            parameters[4].Value = model.CategoryID;
            parameters[5].Value = string.IsNullOrEmpty(model.Remark) ? SqlString.Null : model.Remark;
            parameters[6].Value = model.Type;
            parameters[7].Value = string.IsNullOrEmpty(model.WarehouseID) ? SqlString.Null : model.WarehouseID;
            parameters[8].Value = string.IsNullOrEmpty(model.WarehouseKeeper) ? SqlString.Null : model.WarehouseKeeper;
            parameters[9].Value = string.IsNullOrEmpty(model.WarehouseKeeperName) ? SqlString.Null : model.WarehouseKeeperName;
            parameters[10].Value = string.IsNullOrEmpty(model.Receiver) ? SqlString.Null : model.Receiver;
            parameters[11].Value = string.IsNullOrEmpty(model.ReceiverName) ? SqlString.Null : model.ReceiverName;
            parameters[12].Value = model.InOutTime == DateTime.MinValue ? SqlDateTime.Null : model.InOutTime;
            parameters[13].Value = string.IsNullOrEmpty(model.Name) ? SqlString.Null : model.Name;
            parameters[14].Value = string.IsNullOrEmpty(model.CompanyID) ? SqlString.Null : model.CompanyID;

            //读取ID
            long id = 1;
            if (trans != null)
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            else
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            return id;
        }

        /// <summary>
        /// 增加一条入库前数据
        /// </summary>
        public long AddRecord(ExpendableInOutRecordInfo model, DbTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + TABLE_EXPENDALBEINOUTRECORD + "(");
            strSql.Append("Model,Amount,Unit,Price,CategoryID,Remark,Type,WarehouseID,WarehouseKeeper,WarehouseKeeperName,Receiver,ReceiverName,InOutTime,Name,CompanyID,SheetID)");
            strSql.Append(" values (");
            strSql.Append("@Model,@Amount,@Unit,@Price,@CategoryID,@Remark,@Type,@WarehouseID,@WarehouseKeeper,@WarehouseKeeperName,@Receiver,@ReceiverName,@InOutTime,@Name,@CompanyID,@SheetID)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@Model", SqlDbType.NVarChar,50),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@CategoryID", SqlDbType.BigInt,8),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
					new SqlParameter("@Type", SqlDbType.TinyInt,1),
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@WarehouseKeeper", SqlDbType.VarChar,20),
					new SqlParameter("@WarehouseKeeperName", SqlDbType.NVarChar,50),
					new SqlParameter("@Receiver", SqlDbType.VarChar,20),
					new SqlParameter("@ReceiverName", SqlDbType.NVarChar,50),
					new SqlParameter("@InOutTime", SqlDbType.DateTime),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@CompanyID",SqlDbType.VarChar,2),
                    new SqlParameter("@SheetID",SqlDbType.BigInt,8)};
            parameters[0].Value = string.IsNullOrEmpty(model.Model) ? SqlString.Null : model.Model;
            parameters[1].Value = model.Amount;
            parameters[2].Value = string.IsNullOrEmpty(model.Unit) ? SqlString.Null : model.Unit;
            parameters[3].Value = model.Price;
            parameters[4].Value = model.CategoryID;
            parameters[5].Value = string.IsNullOrEmpty(model.Remark) ? SqlString.Null : model.Remark;
            parameters[6].Value = model.Type;
            parameters[7].Value = string.IsNullOrEmpty(model.WarehouseID) ? SqlString.Null : model.WarehouseID;
            parameters[8].Value = string.IsNullOrEmpty(model.WarehouseKeeper) ? SqlString.Null : model.WarehouseKeeper;
            parameters[9].Value = string.IsNullOrEmpty(model.WarehouseKeeperName) ? SqlString.Null : model.WarehouseKeeperName;
            parameters[10].Value = string.IsNullOrEmpty(model.Receiver) ? SqlString.Null : model.Receiver;
            parameters[11].Value = string.IsNullOrEmpty(model.ReceiverName) ? SqlString.Null : model.ReceiverName;
            parameters[12].Value = model.InOutTime == DateTime.MinValue ? SqlDateTime.Null : model.InOutTime;
            parameters[13].Value = string.IsNullOrEmpty(model.Name) ? SqlString.Null : model.Name;
            parameters[14].Value = string.IsNullOrEmpty(model.CompanyID) ? SqlString.Null : model.CompanyID;
            parameters[15].Value = model.SheetID;

            //读取ID
            long id = 1;
            if (trans != null)
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            else
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            return id;
        }


        /// <summary>
        /// 获取查询对象
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public QueryParam GenerateSearchTerm(ExpendableInOutRecordSearchInfo item)
        {
            string sqlSearch = "where 1=1";
            
            if (item.Name != null && item.Name != "")
                sqlSearch += " and Name = '" + item.Name + "'";

            if (!string.IsNullOrEmpty(item.Model))
            {
                sqlSearch += " and Model = '" + item.Model + "'";
            }
            if (item.WarehouseID != null && item.WarehouseID != "")
                sqlSearch += " and WarehouseID ='" + item.WarehouseID + "'";

            if (!string.IsNullOrEmpty(item.CompanyID))
            {
                sqlSearch += " and CompanyID='" + item.CompanyID + "'";
            }

            if (item.InOutTimeLower != DateTime.MinValue)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(item.InOutTimeLower, sqlMinDate) < 0)
                    item.InOutTimeLower = sqlMinDate;

                sqlSearch += " and SubmitTime>='" + item.InOutTimeLower.ToString("yyyy-MM-dd") + " 00:00:00'";
            }

            if (item.InOutTimeUpper != DateTime.MinValue)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(item.InOutTimeUpper, sqlMaxDate) > 0)
                    item.InOutTimeUpper = sqlMaxDate;

                sqlSearch += " and SubmitTime<='" + item.InOutTimeUpper.ToString("yyyy-MM-dd") + " 23:59:59'";
            }

            if (item.CategoryID != 0)
                sqlSearch += string.Format(" and (CategoryID in ( select CategoryID from FM2E_GetSubCategories({0})))", item.CategoryID);

            QueryParam searchTerm = new QueryParam();
            //searchTerm.TableName = VIEW_EXPENDABLEINOUT;
            searchTerm.TableName = "dbo.FM2E_InExpendableEquipmentView";
            searchTerm.ReturnFields = "*";
            searchTerm.OrderBy = "order by ID DESC";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }

        /// <summary>
        /// 获取查询对象(易耗品出库)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public QueryParam GenerateSearchTermOut(ExpendableInOutRecordSearchInfo item)
        {
            string sqlSearch = "where 1=1";

            if (item.Name != null && item.Name != "")
                sqlSearch += " and Name = '" + item.Name + "'";

            if (!string.IsNullOrEmpty(item.Model))
            {
                sqlSearch += " and Model = '" + item.Model + "'";
            }
            if (item.WarehouseID != null && item.WarehouseID != "")
                sqlSearch += " and WarehouseID ='" + item.WarehouseID + "'";

            if (!string.IsNullOrEmpty(item.CompanyID))
            {
                sqlSearch += " and CompanyID='" + item.CompanyID + "'";
            }

            if (item.InOutTimeLower != DateTime.MinValue)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(item.InOutTimeLower, sqlMinDate) < 0)
                    item.InOutTimeLower = sqlMinDate;

                sqlSearch += " and SubmitTime>='" + item.InOutTimeLower.ToString("yyyy-MM-dd") + " 00:00:00'";
            }

            if (item.InOutTimeUpper != DateTime.MinValue)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(item.InOutTimeUpper, sqlMaxDate) > 0)
                    item.InOutTimeUpper = sqlMaxDate;

                sqlSearch += " and SubmitTime<='" + item.InOutTimeUpper.ToString("yyyy-MM-dd") + " 23:59:59'";
            }

            if (item.CategoryID != 0)
                sqlSearch += string.Format(" and (CategoryID in ( select CategoryID from FM2E_GetSubCategories({0})))", item.CategoryID);

            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "dbo.FM2E_OutEquipmentsForExpendableView";
            searchTerm.ReturnFields = "*";
            searchTerm.OrderBy = "order by ID DESC";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }

        public QueryParam GenerateSearchRecordTerm(ExpendableInOutRecordInfo item)
        {
            string sqlSearch = "where 1=1";
            if ( item.SheetID != 0)
                sqlSearch += " and SheetID = " + item.SheetID;
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = TABLE_EXPENDALBEINOUTRECORD;
            searchTerm.ReturnFields = "*";
            searchTerm.OrderBy = "order by ID ";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }

        public QueryParam GenerateSearchSheetTerm(ExpendableSheet item)
        {
            string sqlSearch = "where 1=1";
            if (item.id != 0)
                sqlSearch += " and id = " + item.id;
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = TABLE_EXPENDABLESHEET;
            searchTerm.ReturnFields = "*";
            searchTerm.OrderBy = "order by id desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }

        public QueryParam GenerateSearchModifyTerm(ExpendableModify item)
        {
            string sqlSearch = "where 1=1";
            if (item.SheetID != null && item.SheetID != 0)
                sqlSearch += " and SheetID = " + item.SheetID;
            if(item.RecordID!=null&&item.RecordID!=0)
                sqlSearch += " and RecordID = " + item.RecordID;
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = TABLE_EXPENDABLEMODIFY;
            searchTerm.ReturnFields = "*";
            searchTerm.OrderBy = "order by id";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            return SQLHelper.GetObjectList(this.GetData, searchTerm, out recordCount);
        }

        public IList GetModifyList(QueryParam searchTerm, out int recordCount)
        {
            return SQLHelper.GetObjectList(this.GetModifyData, searchTerm, out recordCount);
        }

        public IList GetSheetList(QueryParam term, out int recordCount)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = TABLE_EXPENDABLESHEET;
                    term.ReturnFields = "*";
                    term.OrderBy = " order by id desc ";
                    term.Where = "where 1=1 ";
                }
                return SQLHelper.GetObjectList(this.GetSheetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取分页失败", e);
            }
        }

        /// <summary>
        /// 获取审批信息
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        /// 
        private InOutApproval GetApprovalData(IDataReader rd)
        {
            InOutApproval item = new InOutApproval();

            if (!Convert.IsDBNull(rd["xingzhenyewu"]))
                item.xingzhenyewu = Convert.ToString(rd["xingzhenyewu"]);
            if (!Convert.IsDBNull(rd["zongheshiwu"]))
                item.zongheshiwu = Convert.ToString(rd["zongheshiwu"]);
            if (!Convert.IsDBNull(rd["jihuacaiwu"]))
                item.jihuacaiwu = Convert.ToString(rd["jihuacaiwu"]);
            if (!Convert.IsDBNull(rd["fenguanlingdao"]))
                item.fenguanlingdao = Convert.ToString(rd["fenguanlingdao"]);
            if (!Convert.IsDBNull(rd["zongjingli"]))
                item.zongjingli = Convert.ToString(rd["zongjingli"]);
            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            return item;
        }


        private ExpendableSheet GetSheetData(IDataReader rd)
        {
            ExpendableSheet item = new ExpendableSheet();

            if (!Convert.IsDBNull(rd["id"]))
                item.id = Convert.ToInt32(rd["id"]);
            if (!Convert.IsDBNull(rd["name"]))
                item.name = Convert.ToString(rd["name"]);
            if (!Convert.IsDBNull(rd["time"]))
                item.time = Convert.ToDateTime(rd["time"]);
            if (!Convert.IsDBNull(rd["xinzhengyewu"]))
                item.xinzhengyewu = Convert.ToString(rd["xinzhengyewu"]).Trim();
            if (!Convert.IsDBNull(rd["zongheshiwu"]))
                item.zongheshiwu = Convert.ToString(rd["zongheshiwu"]).Trim();
            if (!Convert.IsDBNull(rd["jihuacaiwu"]))
                item.jihuacaiwu = Convert.ToString(rd["jihuacaiwu"]).Trim();
            if (!Convert.IsDBNull(rd["fenguanlingdao"]))
                item.fenguanlingdao = Convert.ToString(rd["fenguanlingdao"]).Trim();
            if (!Convert.IsDBNull(rd["zongjinli"]))
                item.zongjinli = Convert.ToString(rd["zongjinli"]).Trim();

            return item;
        }

        private ExpendableModify GetModifyData(IDataReader rd)
        {
            ExpendableModify item = new ExpendableModify();

            if (!Convert.IsDBNull(rd["id"]))
                item.id = Convert.ToInt32(rd["id"]);

            if (!Convert.IsDBNull(rd["SheetID"]))
                item.SheetID = Convert.ToInt32(rd["SheetID"]);

            if (!Convert.IsDBNull(rd["equipmentname"]))
                item.equipmentname = Convert.ToString(rd["equipmentname"]);

            if (!Convert.IsDBNull(rd["RecordID"]))
                item.RecordID = Convert.ToInt32(rd["RecordID"]);

            if (!Convert.IsDBNull(rd["modifytime"]))
                item.modifytime = Convert.ToDateTime(rd["modifytime"]);

            if (!Convert.IsDBNull(rd["oldnum"]))
                item.oldnum = Convert.ToInt32(rd["oldnum"]);

            if (!Convert.IsDBNull(rd["newnum"]))
                item.newnum = Convert.ToInt32(rd["newnum"]);

            if (!Convert.IsDBNull(rd["userid"]))
                item.userid = Convert.ToString(rd["userid"]);

            if (!Convert.IsDBNull(rd["username"]))
                item.username = Convert.ToString(rd["username"]);

            if (!Convert.IsDBNull(rd["type"]))
                item.type = Convert.ToString(rd["type"]);

            return item;
        }

        /// <summary>
        /// 获取一个对象
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private ExpendableInOutRecordInfo GetData(IDataReader rd)
        {
            ExpendableInOutRecordInfo item = new ExpendableInOutRecordInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["Type"]))
                item.Type =(ExpendableInOutRecordType) Convert.ToInt16(rd["Type"]);

            if (!Convert.IsDBNull(rd["WarehouseID"]))
                item.WarehouseID = Convert.ToString(rd["WarehouseID"]);

            if (!Convert.IsDBNull(rd["WarehouseKeeper"]))
                item.WarehouseKeeper = Convert.ToString(rd["WarehouseKeeper"]);

            if (!Convert.IsDBNull(rd["WarehouseKeeperName"]))
                item.WarehouseKeeperName = Convert.ToString(rd["WarehouseKeeperName"]);

            if (!Convert.IsDBNull(rd["Receiver"]))
                item.Receiver = Convert.ToString(rd["Receiver"]);

            if (!Convert.IsDBNull(rd["ReceiverName"]))
                item.ReceiverName = Convert.ToString(rd["ReceiverName"]);

            if (!Convert.IsDBNull(rd["InOutTime"]))
                item.InOutTime = Convert.ToDateTime(rd["InOutTime"]);

            if (!Convert.IsDBNull(rd["Model"]))
                item.Model = Convert.ToString(rd["Model"]);

            if (!Convert.IsDBNull(rd["Name"]))
                item.Name = Convert.ToString(rd["Name"]);

            if (!Convert.IsDBNull(rd["Amount"]))
                item.Amount = Convert.ToDecimal(rd["Amount"]);

            if (!Convert.IsDBNull(rd["Unit"]))
                item.Unit = Convert.ToString(rd["Unit"]);

            if (!Convert.IsDBNull(rd["Price"]))
                item.Price = Convert.ToDecimal(rd["Price"]);

            if (!Convert.IsDBNull(rd["CategoryID"]))
                item.CategoryID = Convert.ToInt64(rd["CategoryID"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
            {
                item.CompanyID = Convert.ToString(rd["CompanyID"]);
            }

            return item;

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DelExpendableInOut(long ExpendableID,long ID)
        {
            try
            {
                ExpendableInOutRecordInfo ExpendableInOutItem = GetExpendableInOut(ExpendableID);
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_ExpendableInOut ");
                strSql.Append(" where ID=@ExpendableID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ExpendableID", SqlDbType.BigInt)};
                parameters[0].Value = ExpendableID;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                Expendable bll = new Expendable();
                ExpendableInfo ExpedableItem=bll.GetExpendable(ID);
                if (ExpendableInOutItem.Type == ExpendableInOutRecordType.In)
                {
                    ExpedableItem.Count = ExpedableItem.Count - ExpendableInOutItem.Amount;
                    if (ExpedableItem.Count < 0)
                    {
                        ExpedableItem.Count = 0;  //归零
                    }
                }
                if (ExpendableInOutItem.Type == ExpendableInOutRecordType.Out)
                {
                    ExpedableItem.Count = ExpedableItem.Count + ExpendableInOutItem.Amount;
                    if (ExpedableItem.Count < 0)
                    {
                        ExpedableItem.Count = 0;  //归零
                    }
                }
                bll.UpdateExpendable(ExpedableItem);  //计数更新

            }
            catch (Exception e)
            {
                throw new DALException("删除消耗品信息失败", e);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ExpendableInOutRecordInfo GetExpendableInOut(long ExpendableID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1  * from FM2E_ExpendableInOut ");
            strSql.Append(" where ID=@ExpendableID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ExpendableID", SqlDbType.BigInt)};
            parameters[0].Value = ExpendableID;
            ExpendableInOutRecordInfo item = new ExpendableInOutRecordInfo();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        item = this.GetData1(rd);

                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取消耗品信息失败", e);
            }
            return item;
        }

        public IList GetExInOut(String companyid, String warehouseid, DateTime datefrom, DateTime dateto, long CategoryID)
        {
            ArrayList list = new ArrayList();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from FM2E_ExpendableInOut ");
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
            if (CategoryID != 0)
            {
                strSql.Append(" and CategoryID=@CategoryID ");
            }
            if (DateTime.Compare(datefrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(datefrom, sqlMinDate) < 0)
                    datefrom = sqlMinDate;

                strSql.Append(" and InOutTime>='" + datefrom.ToString("yyyy-MM-dd") + " 00:00:00'");
            }

            if (DateTime.Compare(dateto, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(dateto, sqlMaxDate) > 0)
                    dateto = sqlMaxDate;

                strSql.Append(" and InOutTime<='" + dateto.ToString("yyyy-MM-dd") + " 23:59:59'");
            }
            strSql.Append(" ;");
            SqlParameter[] parameters = {
			    new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
                new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
                new SqlParameter("@CategoryID", SqlDbType.BigInt)
                                    };
            parameters[0].Value = companyid;
            parameters[1].Value = warehouseid;
            parameters[2].Value = CategoryID;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        ExpendableInOutRecordInfo item = this.GetData(rd);
                        list.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取所有消耗品失败", e);
            }
            return list;
        }

        public IList GetExInOutYear(String companyid, String warehouseid, DateTime datefrom, DateTime dateto, long CategoryID)
        {
            ArrayList list = new ArrayList();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from FM2E_ExpendableInOut ");
            strSql.Append(" where 1=1");
            if (companyid != "0")
            {
                strSql.Append(" and   CompanyID=@CompanyID ");
                strSql.Append(" and   WarehouseID=@WarehouseID ");
            }
            else
            {
                //donothing
            }
            if (CategoryID != 0)
            {
                strSql.Append(" and CategoryID=@CategoryID ");
            }
            if (DateTime.Compare(datefrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(datefrom, sqlMinDate) < 0)
                    datefrom = sqlMinDate;

                strSql.Append(" and InOutTime>='" + datefrom.ToString("yyyy-MM-dd") + " 00:00:00'");
            }

            if (DateTime.Compare(dateto, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(dateto, sqlMaxDate) > 0)
                    dateto = sqlMaxDate;

                strSql.Append(" and InOutTime<='" + dateto.ToString("yyyy-MM-dd") + " 23:59:59'");
            }
            strSql.Append(" order by Name");
            strSql.Append(" ;");
            SqlParameter[] parameters = {
			    new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
                new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
                new SqlParameter("@CategoryID", SqlDbType.BigInt)
                                    };
            parameters[0].Value = companyid;
            parameters[1].Value = warehouseid;
            parameters[2].Value = CategoryID;
            string nametemp = "";
            decimal counttemp = 0;
            ExpendableInOutRecordInfo tempitem = null;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        ExpendableInOutRecordInfo item = this.GetData(rd);
                        if (nametemp == "")
                        {
                            nametemp = item.Name.Trim();  //第一个的情况
                        }
                        if (nametemp == item.Name.Trim())
                        {
                            if (item.Type == ExpendableInOutRecordType.In)
                            {
                                counttemp += item.Amount;
                            }
                            else if (item.Type == ExpendableInOutRecordType.Out)
                            {
                                counttemp -= 0;
                            }
                            tempitem = item;
                        }
                        else
                        {
                            tempitem.Amount = counttemp;
                            tempitem.InOutTime = datefrom;
                            list.Add(tempitem);
                            if (item.Type == ExpendableInOutRecordType.In)
                                counttemp = item.Amount;
                            else if (item.Type == ExpendableInOutRecordType.Out)
                                counttemp = 0;
                            nametemp = item.Name.Trim();
                        }
                        
                    }
                    if (tempitem != null)
                    {
                        tempitem.Amount = counttemp;
                        tempitem.InOutTime = datefrom;
                        list.Add(tempitem);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取所有消耗品失败", e);
            }
            return list;
        }

        private ExpendableInOutRecordInfo GetData1(IDataReader rd)
        {
            ExpendableInOutRecordInfo item = new ExpendableInOutRecordInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["Type"]))
                item.Type = (ExpendableInOutRecordType)Convert.ToInt16(rd["Type"]);

            if (!Convert.IsDBNull(rd["Amount"]))
                item.Amount = Convert.ToDecimal(rd["Amount"]);
            return item;
        }

        private OutWarehouseInfo GetDataExpendable(IDataReader rd)
        {
            OutWarehouseInfo item = new OutWarehouseInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["WarehouseID"]))
                item.WarehouseID = Convert.ToString(rd["WarehouseID"]);

            if (!Convert.IsDBNull(rd["DepartmentID"]))
                item.DepartmentID = Convert.ToInt64(rd["DepartmentID"]);

            if (!Convert.IsDBNull(rd["SubmitTime"]))
                item.SubmitTime = Convert.ToDateTime(rd["SubmitTime"]);

            if (!Convert.IsDBNull(rd["ApplicantID"]))
                item.ApplicantID = Convert.ToString(rd["ApplicantID"]);

            if (!Convert.IsDBNull(rd["OperatorID"]))
                item.OperatorID = Convert.ToString(rd["OperatorID"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["IsDeleted"]))
                item.IsDeleted = Convert.ToBoolean(rd["IsDeleted"]);

            if (!Convert.IsDBNull(rd["WarehouseName"]))
                item.WarehouseName = Convert.ToString(rd["WarehouseName"]);

            if (!Convert.IsDBNull(rd["CompanyName"]))
                item.CompanyName = Convert.ToString(rd["CompanyName"]);

            if (!Convert.IsDBNull(rd["DepartmentName"]))
                item.DepartmentName = Convert.ToString(rd["DepartmentName"]);

            if (!Convert.IsDBNull(rd["ApplicantName"]))
                item.ApplicantName = Convert.ToString(rd["ApplicantName"]);

            if (!Convert.IsDBNull(rd["OperatorName"]))
                item.OperatorName = Convert.ToString(rd["OperatorName"]);

            if (!Convert.IsDBNull(rd["SheetName"]))
                item.SheetName = Convert.ToString(rd["SheetName"]);

            if (!Convert.IsDBNull(rd["CurrentStateName"]))
                item.CurrentStateName = Convert.ToString(rd["CurrentStateName"]);

            if (!Convert.IsDBNull(rd["Attachment"]))
                item.Attachment = Convert.ToString(rd["Attachment"]);
            if (!Convert.IsDBNull(rd["Editreason"]))
                item.Editreason = Convert.ToString(rd["Editreason"]);
            if (!Convert.IsDBNull(rd["InstanceID"]))
                item.WorkFlowInstanceID = Convert.ToString(rd["InstanceID"]);
            if (!Convert.IsDBNull(rd["NextUserName"]))
                item.NextUserName = Convert.ToString(rd["NextUserName"]);
            if (!Convert.IsDBNull(rd["StatusDescription"]))
                item.StatusDescription = Convert.ToString(rd["StatusDescription"]);
            return item;

        }

#region 
        /// <summary>
        /// 查询易耗品出库申请单
        /// </summary>
        /// <param name="qp">出库申请单查询对象</param>
        /// <param name="recordCount">查询结果总数</param>
        /// <returns>出库申请单查询结果列表</returns>
        public IList SearchOutWarehouseExpendable(QueryParam qp, out int recordCount)
        {
            return SQLHelper.GetObjectListWithDistinct(this.GetDataExpendable, qp, out recordCount);
        }

        /// <summary>
        /// 生成出库申请单查询对象
        /// </summary>
        /// <param name="info">查询信息对象</param>
        /// <param name="pageindex">页码</param>
        /// <param name="pagesize">页大小</param>
        /// <returns>出库申请单查询对象</returns>
        public QueryParam GenerateSearchTerm(OutWarehouseInfo info, int pageindex, int pagesize)
        {
            string sqlSearch = " where 1=1";

            if (!string.IsNullOrEmpty(info.ApplicantID))
            {
                sqlSearch += " and s1.ApplicantID='" + info.ApplicantID + "'";
            }
            if (!string.IsNullOrEmpty(info.ApplicantName))
            {
                sqlSearch += " and s1.ApplicantName like '%" + info.ApplicantName + "%'";
            }
            if (!string.IsNullOrEmpty(info.CompanyID))
            {
                sqlSearch += " and s1.CompanyID='" + info.CompanyID + "'";
            }
            if (info.ID != 0)
            {
                sqlSearch += " and s1.ID=" + info.ID + "";

            }
            if (!string.IsNullOrEmpty(info.SheetName))
            {
                sqlSearch += " and s1.SheetName='" + info.SheetName + "'";
            }

            if (!string.IsNullOrEmpty(info.WarehouseID))
            {
                sqlSearch += " and s1.WarehouseID='" + info.WarehouseID + "'";
            }
            if (!string.IsNullOrEmpty(info.CurrentStateName))
            {
                sqlSearch += " and s1.CurrentStateName='" + info.CurrentStateName + "'";
            }

            if (info.TimeLower != DateTime.MinValue)
            {
                sqlSearch += " and s1.SubmitTime >= '" + info.TimeLower.ToString("yyyy-MM-dd HH:mm") + "'";
            }

            if (info.TimeUpper != DateTime.MinValue)
            {
                sqlSearch += " and s1.SubmitTime< '" + info.TimeUpper.AddDays(1).ToString("yyyy-MM-dd HH:mm") + "'";
            }

            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_OutEquipmentsForExpendableView s1";
            searchTerm.ReturnFields = "*";
            searchTerm.PageSize = pagesize;
            searchTerm.PageIndex = pageindex;
            searchTerm.OrderBy = " order by SubmitTime desc";
            searchTerm.Where = sqlSearch;

            return searchTerm;
        }

        /// <summary>
        /// 生成易耗品出库申请单审批查询对象（审批专用）
        /// </summary>
        /// <param name="info">查询信息对象</param>
        /// <param name="pageindex">页码</param>
        /// <param name="pagesize">页大小</param>
        /// <returns>出库申请单查询对象</returns>
        public QueryParam GetGenerateSearchTerm(OutWarehouseInfo info, int pageindex, int pagesize, string userName)
        {
            string sqlSearch = " where s1.NextUserName='" + userName + "'";

            if (!string.IsNullOrEmpty(info.SheetName))
            {
                sqlSearch += " and s1.SheetName='" + info.SheetName + "'";
            }

            if (!string.IsNullOrEmpty(info.WarehouseID))
            {
                sqlSearch += " and s1.WarehouseID='" + info.WarehouseID + "'";
            }

            //if (info.WorkFlowStatusList != null && info.WorkFlowStatusList.Count > 0)
            //{
            //    for (int i = 0; i < info.WorkFlowStatusList.Count; i++)
            //    {
            //        if (i == 0)
            //        {
            //            sqlSearch += " and ( ";
            //            sqlSearch += " " + "s1.CurrentStateName='" + info.WorkFlowStatusList[i] + "'";
            //        }
            //        else
            //        {
            //            sqlSearch += " or " + "s1.CurrentStateName='" + info.WorkFlowStatusList[i] + "'";
            //        }
            //        if (i == info.WorkFlowStatusList.Count - 1)
            //        {
            //            sqlSearch += " ) ";
            //        }
            //    }
            //}

            if (info.ApplyTimeLower != DateTime.MinValue)
            {
                sqlSearch += " and s1.SubmitTime >= '" + info.ApplyTimeLower.ToString("yyyy-MM-dd HH:mm") + "'";
            }

            if (info.ApplyTimeUpper != DateTime.MinValue)
            {
                sqlSearch += " and s1.SubmitTime< '" + info.ApplyTimeUpper.AddDays(1).ToString("yyyy-MM-dd HH:mm") + "'";
            }

            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = " FM2E_OutEquipmentsForExpendableView s1";
            searchTerm.ReturnFields = " *";
            searchTerm.PageSize = pagesize;
            searchTerm.PageIndex = pageindex;
            searchTerm.OrderBy = " order by SubmitTime desc";
            searchTerm.Where = sqlSearch;

            return searchTerm;
        }

        /// <summary>
        /// 得到易耗品出口（含明细）
        /// </summary>
        public OutWarehouseInfo GetOutWarehouse(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from FM2E_OutEquipmentsForExpendableView ");
            strSql.Append(" where ID=@ID and IsDeleted = 0");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = ID;
            OutWarehouseInfo item = new OutWarehouseInfo();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        item = this.GetDataExpendable(rd);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取入库信息失败", e);
            }
            item.OutWarehouseList = GetEquipmentList(ID);
            return item;
        }

        /// <summary>
        /// 获取出库详情
        /// </summary>
        private IList GetEquipmentList(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from FM2E_OutEquipmentsForExpendable ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = id;
            List<InEquipmentsInfo> list = new List<InEquipmentsInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    InEquipmentsInfo item = GetDataInEquipment(rd);
                    list.Add(item);
                }
            }
            return list;
        }

        private InEquipmentsInfo GetDataInEquipment(IDataReader rd)
        {
            InEquipmentsInfo item = new InEquipmentsInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt32(rd["ID"]);

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

            if (!Convert.IsDBNull(rd["OutTime"]))
                item.OutTime = Convert.ToDateTime(rd["OutTime"]);

            if (!Convert.IsDBNull(rd["Unit"]))
                item.Unit = Convert.ToString(rd["Unit"]);

            if (!Convert.IsDBNull(rd["Name"]))
            {
                item.Name = Convert.ToString(rd["Name"]);
            }

            if (!Convert.IsDBNull(rd["Model"]))
            {
                item.Model = Convert.ToString(rd["Model"]);
            }

            if (!Convert.IsDBNull(rd["ExpendableTypeID"]))
                item.ExpendableTypeID = Convert.ToInt64(rd["ExpendableTypeID"]);

            if (!Convert.IsDBNull(rd["ExpendablePrice"]))
                item.ExpendablePrice = Convert.ToDecimal(rd["ExpendablePrice"]);


            if (!Convert.IsDBNull(rd["ExpendableType"]))
                item.ExpendableType = Convert.ToString(rd["ExpendableType"]);
            return item;

        }

        /// <summary>
        /// 更新易耗品出库申请单
        /// </summary>
        public void UpdateOutWarehouse(OutWarehouseInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_OutWarehouse set ");
            strSql.Append("IsDeleted=@IsDeleted,");
            strSql.Append("SheetName=@SheetName,");
            strSql.Append("WarehouseID=@WarehouseID,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("DepartmentID=@DepartmentID,");
            strSql.Append("SubmitTime=@SubmitTime,");
            strSql.Append("ApplicantID=@ApplicantID,");
            strSql.Append("OperatorID=@OperatorID,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("Editreason=@Editreason");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
					new SqlParameter("@SheetName", SqlDbType.NVarChar,20),
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@DepartmentID", SqlDbType.BigInt,8),
					new SqlParameter("@SubmitTime", SqlDbType.DateTime),
					new SqlParameter("@ApplicantID", SqlDbType.VarChar,20),
					new SqlParameter("@OperatorID", SqlDbType.VarChar,20),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
                    new SqlParameter("@Attachment", SqlDbType.VarChar,50),
                    new SqlParameter("@Editreason", SqlDbType.VarChar,2000)
                                        };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.IsDeleted;
            parameters[2].Value = model.SheetName;
            parameters[3].Value = model.WarehouseID;
            parameters[4].Value = model.CompanyID;
            parameters[5].Value = model.DepartmentID;
            parameters[6].Value = model.SubmitTime;
            parameters[7].Value = model.ApplicantID;
            parameters[8].Value = model.OperatorID;
            parameters[9].Value = model.Remark;
            parameters[10].Value = string.IsNullOrEmpty(model.Attachment) ? SqlString.Null : model.Attachment;
            parameters[11].Value = string.IsNullOrEmpty(model.Editreason) ? SqlString.Null : model.Editreason;

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
                    throw new DALException("更新入库信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        
#endregion
    }
}
