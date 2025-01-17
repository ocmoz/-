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
using FM2E.SQLServerDAL.Equipment;
using FM2E.SQLServerDAL.Basic;
using System.Data.SqlTypes;

namespace FM2E.SQLServerDAL.Equipment
{
    public class InWarehouse : IInWarehouse
    {
        public QueryParam GenerateSearchTerm(InWarehouseInfo item)
        {
            string sqlSearch = "where 1=1";

            if (!string.IsNullOrEmpty(item.WarehouseID))
                sqlSearch += " and a.WarehouseID ='" + item.WarehouseID + "'";

            if (!string.IsNullOrEmpty(item.CompanyID))
                sqlSearch += " and a.CompanyID = '" + item.CompanyID + "'";

            if (item.DepartmentID != 0)
                sqlSearch += " and a.DepartmentID =" + item.DepartmentID + "";

            if (!string.IsNullOrEmpty(item.SheetName))
                sqlSearch += " and a.SheetName = '" + item.SheetName + "'";

            if (!string.IsNullOrEmpty(item.ApplicantName))
                sqlSearch += " and e.PersonName like '%" + item.ApplicantName + "%'";

            if (!string.IsNullOrEmpty(item.OperatorName))
                sqlSearch += " and f.PersonName like '%" + item.OperatorName + "%'";

            if (item.TimeLower!=DateTime.MinValue)
                sqlSearch += " and a.SubmitTime >= '" + item.TimeLower.ToString("yyyy-MM-dd HH:mm") + "' ";

            if (item.TimeUpper != DateTime.MinValue)
                sqlSearch += " and a.SubmitTime< '" + item.TimeUpper.AddDays(1).ToString("yyyy-MM-dd HH:mm") + "'";


            sqlSearch += " and a.isdeleted = 0";
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_InWarehouse a left join FM2E_WareHouse b on a.WarehouseID = b.WareHouseID left join FM2E_Company c on a.CompanyID = c. CompanyID left join FM2E_Department d on a.DepartmentID = d.DepartmentID left join FM2E_UserView e on a.ApplicantID=e.UserName left join FM2E_UserView f on a.OperatorID=f.UserName";
            searchTerm.ReturnFields = "a.*,b.Name as WarehouseName,c.CompanyName,d.Name as DepartmentName,e.PersonName as ApplicantName,f.PersonName as OperatorName";
            //searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by ID desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            if (searchTerm.Where == "")
            {
                searchTerm.TableName = "FM2E_InWarehouse a left join FM2E_WareHouse b on a.WarehouseID = b.WareHouseID left join FM2E_Company c on a.CompanyID = c. CompanyID left join FM2E_Department d on a.DepartmentID = d.DepartmentID left join FM2E_User e on a.ApplicantID=e.UserName left join FM2E_User f on a.OperatorID=f.UserName";
                searchTerm.ReturnFields = "a.*,b.Name as WarehouseName,c.CompanyName,d.Name as DepartmentName,e.PersonName as ApplicantName,f.PersonName as OperatorName";
                //searchTerm.PageSize = 10;
                searchTerm.OrderBy = "order by ID desc";
                searchTerm.Where = "where a.isdeleted = 0";
            }
            return SQLHelper.GetObjectList(this.GetData, searchTerm, out recordCount);
        }

        #region 查询
        /// <summary>
        /// 生成易耗品入库申请单查询对象
        /// </summary>
        /// <param name="info">查询信息对象</param>
        /// <param name="pageindex">页码</param>
        /// <param name="pagesize">页大小</param>
        /// <returns>出库申请单查询对象</returns>
        public QueryParam GenerateSearchTerm(InWarehouseInfo info, int pageindex, int pagesize)
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
            searchTerm.TableName = "FM2E_InExpendableEquipmentView s1";
            searchTerm.ReturnFields = "*";
            searchTerm.PageSize = pagesize;
            searchTerm.PageIndex = pageindex;
            searchTerm.OrderBy = " order by SubmitTime desc";
            searchTerm.Where = sqlSearch;

            return searchTerm;
        }

        /// <summary>
        /// 生成易耗品入库申请单审批查询对象（审批专用）
        /// </summary>
        /// <param name="info">查询信息对象</param>
        /// <param name="pageindex">页码</param>
        /// <param name="pagesize">页大小</param>
        /// <returns>出库申请单查询对象</returns>
        public QueryParam GetGenerateSearchTerm(InWarehouseInfo info, int pageindex, int pagesize, string userName)
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
            searchTerm.TableName = " FM2E_InExpendableEquipmentView s1";
            searchTerm.ReturnFields = " *";
            searchTerm.PageSize = pagesize;
            searchTerm.PageIndex = pageindex;
            searchTerm.OrderBy = " order by SubmitTime desc";
            searchTerm.Where = sqlSearch;

            return searchTerm;
        }

        /// <summary>
        /// 查询易耗品入库申请单
        /// </summary>
        /// <param name="qp">易耗品入库申请单查询对象</param>
        /// <param name="recordCount">查询结果总数</param>
        /// <returns>易耗品入库申请单查询结果列表</returns>
        public IList SearchInWarehouseApply(QueryParam qp, out int recordCount)
        {
            return SQLHelper.GetObjectListWithDistinct(this.GetData, qp, out recordCount);
        }
      
        #endregion

        /// <summary>
        /// 增加入库信息
        /// </summary>
        /// <param name="model"></param>
        public void InsertInWarehouse(InWarehouseInfo model, ArrayList list)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先插入入库信息
                long thisID = InsertInWarehouseItem(trans, model);

                //插入入库明细信息
                if (list != null)
                {
                    foreach (InEquipmentsInfo item in list)
                    {
                        item.ID = thisID;
                        InsertInEquipments(trans, item);
                    }
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
        }


        /// <summary>
        /// 增加入库信息
        /// </summary>
        /// <param name="model"></param>
        public void InsertInWarehouseWithDetail(InWarehouseInfo model)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            Equipment eqDal = new Equipment();
            Expendable expDal = new Expendable();
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先插入入库信息
                long thisID = InsertInWarehouseItem(trans, model);
                model.ID = thisID;
                //插入入库明细信息，并更新对应的地址信息、易耗品信息
                if (model.InWarehouseList != null)
                {
                    foreach (InEquipmentsInfo item in model.InWarehouseList)
                    {
                        item.ID = thisID;
                        item.ItemID = InsertInEquipments(trans, item);
                        if (item.IsAsset)
                        {
                            eqDal.UpdateEquipmentAddress(trans, item.EquipmentNO, model.WarehouseAddressID, model.WarehouseDetailLocation);
                        }
                        else
                        {
                            //throw new DALException("未能实现更新价格和新增易耗品的时候获取类型信息");
                            expDal.AddExpendable(trans, model.CompanyID, model.WarehouseID, item.Name, item.Model, item.Unit, item.Count,item.ExpendablePrice,item.ExpendableTypeID);
                        }
                    }
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
        }


        /// <summary>
        /// 增加易耗品入库信息(一个设备)
        /// </summary>
        /// <param name="model"></param>
        public long InsertInWarehouseExpendable(InWarehouseInfo model,InEquipmentsInfo item)
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

                //先插入入库信息
                thisID = InsertInWarehouseItem(trans, model);
                model.ID = thisID;
                //插入入库明细信息，并更新对应的地址信息、易耗品信息
                
                item.ID = thisID;
                item.ItemID = InsertInEquipments(trans, item);
                
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
        /// 增加一条入库信息
        /// </summary>
        private long InsertInWarehouseItem(SqlTransaction trans, InWarehouseInfo model)
        {
            long id = 1;
            SqlDataReader rdr =null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FM2E_InWarehouseForExpendable(");
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
                parameters[9].Value = string.IsNullOrEmpty(model.Attachment) ? SqlString.Null :model.Attachment;
                //读取ID
                using (rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
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
                throw new DALException("添加入库信息失败", e);
            }
            finally
            {
                rdr.Close();
            }
            return id;
        }
        /// <summary>
        /// 增加一条入库明细
        /// </summary>
        private long InsertInEquipments(SqlTransaction trans, InEquipmentsInfo model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FM2E_InEquipmentsForExpendable(");
                strSql.Append("ID,WarehouseID,IsAsset,EquipmentNO,ExpendableID,Count,[Unit],InTime,[Name],Model,ExpendableTypeID,ExpendablePrice,ExpendableType)");
                strSql.Append(" values (");
                strSql.Append("@ID,@WarehouseID,@IsAsset,@EquipmentNO,@ExpendableID,@Count,@Unit,@InTime,@Name,@Model,@ExpendableTypeID,@ExpendablePrice,@ExpendableType)");
                strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@IsAsset", SqlDbType.Bit,1),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
					new SqlParameter("@ExpendableID", SqlDbType.BigInt,8),
					new SqlParameter("@Count", SqlDbType.Decimal,9),
                    new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@InTime", SqlDbType.DateTime),
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
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
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
                throw new DALException("添加一条入库明细信息失败"+e.Message, e);
            }
            //if (model.EquipmentNO == "")
            //{
            //    Expendable expendable = new Expendable();
            //    decimal count = decimal.Zero;
            //    count = expendable.GetCountOfExpendable(model.WarehouseID, model.ExpendableID);
            //    if (count < 0)
            //    {
            //        ExpendableInfo info = new ExpendableInfo();
            //        info.WarehouseID = model.WarehouseID;
            //        info.UpdateTime = DateTime.Now;
            //        info.Unit = model.Unit;
            //        info.Specification = "";
            //        info.Name = model.ExpendableName;
            //        info.Model = model.ExpendableModel;
            //        info.ExpendableID = model.ExpendableID;
            //        info.Count = model.Count;
            //        info.PhotoUrl = "";
            //        info.Remark = "";
            //        Warehouse warehouse = new Warehouse();
            //        info.CompanyID = warehouse.GetWarehouse(info.WarehouseID).CompanyID;
            //        expendable.InsertExpendable(trans, info);
            //    }
            //    else
            //    {
            //        ExpendableInfo info = expendable.GetExpendable(model.ExpendableID);
            //        info.Count = model.Count + count;
            //        expendable.UpdateExpendable(trans,info);
            //    }
            //}
            //else
            //{
            //    Equipment equipment = new Equipment();
            //    EquipmentInfoFacade info = equipment.GetEquipmentBYNO(model.EquipmentNO);
            //    if (info != null)
            //    {
            //        info.LocationTag = "4";
            //        info.LocationID = model.WarehouseID;
            //        info.UpdateTime = DateTime.Now;
            //        equipment.UpdateEquipment(info);
            //    }
            //}

        }

        /// <summary>
        /// 更新易耗品入库信息(一个设备)
        /// </summary>
        /// <param name="model"></param>
        public long UpdateInWarehouseExpendable(InWarehouseInfo model, InEquipmentsInfo item)
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

                //先更新入库信息
                UpdateInWarehouse(model);
                thisID = model.ID;
                //更新入库明细信息，并更新对应的地址信息、易耗品信息

                item.ID = thisID;
                UpdateInEquipments(item);

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
        /// 更新一条数据
        /// </summary>
        public void UpdateInWarehouse(InWarehouseInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_InWarehouseForExpendable set ");
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

        /// <summary>
        /// 更新一条数据明细
        /// </summary>
        public void UpdateInEquipments(InEquipmentsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_InEquipmentsForExpendable set ");
            strSql.Append("WarehouseID=@WarehouseID,IsAsset=@IsAsset,EquipmentNO=@EquipmentNO,ExpendableID=@ExpendableID,");
            strSql.Append("Count=@Count,[Unit]=@Unit,InTime=@InTime,[Name]=@Name,Model=@Model,ExpendableTypeID=@ExpendableTypeID,ExpendablePrice=@ExpendablePrice,ExpendableType=@ExpendableType ");
            strSql.Append("where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@IsAsset", SqlDbType.Bit,1),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
					new SqlParameter("@ExpendableID", SqlDbType.BigInt,8),
					new SqlParameter("@Count", SqlDbType.Decimal,9),
                    new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@InTime", SqlDbType.DateTime),
                    new SqlParameter("@Name",SqlDbType.NVarChar,20),
                    new SqlParameter("@Model",SqlDbType.NVarChar,20),
                    new SqlParameter("@ExpendableTypeID",SqlDbType.BigInt),
                    new SqlParameter("@ExpendablePrice",SqlDbType.Decimal,10),
                    new SqlParameter("@ExpendableType",SqlDbType.NVarChar,50)
                                        };
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
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DelInWarehouse(long ID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_InWarehouse set IsDeleted = 1");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                parameters[0].Value = ID;
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                //同时删除掉入库表中设备信息
                StringBuilder strSql1 = new StringBuilder();
                strSql1.Append("Delete from FM2E_InEquipments");
                strSql1.Append(" where ID=@ID");
                SqlParameter[] parameters1 = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                parameters1[0].Value = ID;
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql1.ToString(), parameters1);

            }
            catch (Exception e)
            {
                throw new DALException("删除入库信息失败", e);
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public InWarehouseInfo GetInWarehouse(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from FM2E_InExpendableEquipmentView ");
            strSql.Append(" where ID=@ID and IsDeleted = 0");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = ID;
            InWarehouseInfo item = new InWarehouseInfo();
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
                throw new DALException("获取入库信息失败", e);
            }
            item.InWarehouseList = GetEquipmentList(ID);
            return item;
        }
        private InWarehouseInfo GetData(IDataReader rd)
        {
            InWarehouseInfo item = new InWarehouseInfo();

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
        /// <summary>
        /// 获取入库详情
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

            if (!Convert.IsDBNull(rd["InTime"]))
                item.InTime = Convert.ToDateTime(rd["InTime"]);

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
    }
}
