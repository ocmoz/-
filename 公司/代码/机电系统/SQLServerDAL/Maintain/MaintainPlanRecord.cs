﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using FM2E.IDAL.Maintain;
using FM2E.Model.Maintain;
using FM2E.Model.Basic;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Utils;
using System.Collections;
using FM2E.Model.Exceptions;

namespace FM2E.SQLServerDAL.Maintain
{
    public class MaintainPlanRecord:IMaintainPlanRecord
    {
        public QueryParam GenerateSearchTerm(MaintainPlanRecordInfo item)
        {
            string sqlSearch = "where 1=1";

            if (item.ItemID != 0)
                sqlSearch += " and a.ItemID ='" + item.ItemID + "'";

            if (item.RecordmanName != null && item.RecordmanName != "")
                sqlSearch += " and a.RecordmanName like '%" + item.RecordmanName + "%'";

            if (item.CompanyID != null && item.CompanyID != "")
                sqlSearch += " and d.CompanyID='" + item.CompanyID + "'";

            if (item.PlanType == MaintainPlanType.DailyPatrol || item.PlanType == MaintainPlanType.RoutineInspect || item.PlanType == MaintainPlanType.RoutineMaintain)
                sqlSearch += " and a.PlanType =" + (int)item.PlanType;

            if (item.VerifiedResult == MaintainPlanVerifiedResult.NotVerified)
            {
                sqlSearch += " and a.VerifiedResult=" + (int)item.VerifiedResult;
            }
            else if (item.VerifiedResult == MaintainPlanVerifiedResult.Verified)
            {
                sqlSearch += " and (a.VerifiedResult=" + (int)MaintainPlanVerifiedResult.NotCompleted + " or a.VerifiedResult=" + (int)MaintainPlanVerifiedResult.CompletedAsPlanned + ")";
            }

            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_MaintainPlanRecord a left join FM2E_User b on a.RecordmanID=b.UserName left join FM2E_User c on a.VerifyBy=c.UserName left join FM2E_MaintainPlanConfig d on a.ItemID=d.ItemID";
            searchTerm.ReturnFields = "a.*,b.PersonName as RecordmanName,c.PersonName as VerifyByName";
            searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by RecordDate desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        public QueryParam GenerateSearchTerm1(string system, long subsystem, string EquipmentNO, MaintainPlanType PlanType)
        {
            QueryParam searchTerm = new QueryParam();
            string sqlSearch = "where 1=1";
            if (subsystem != 0)
                sqlSearch += " and d.Subsystem ='" + subsystem.ToString() + "'";

            if (system != "")
                sqlSearch += " and d.System ='" + system + "'";

            if (EquipmentNO != "")
            {
                sqlSearch += " and e.EquipmentNO ='" + EquipmentNO + "'";
                sqlSearch += " and e.TableName='FM2E_MaintainPlanRecord'";
                searchTerm.TableName = "FM2E_MaintainRecordEquipment e left join FM2E_MaintainPlanRecord a on e.RecordID = a.RecordID left join FM2E_User b on a.RecordmanID=b.UserName left join FM2E_User c on a.VerifyBy=c.UserName";
                searchTerm.ReturnFields = "a.*,b.PersonName as RecordmanName,c.PersonName as VerifyByName";
            }
            else
            {
                searchTerm.TableName = "FM2E_MaintainPlanRecord a left join FM2E_User b on a.RecordmanID=b.UserName left join FM2E_User c on a.VerifyBy=c.UserName left join FM2E_MaintainPlanConfig d on a.ItemID = d.ItemID";
                searchTerm.ReturnFields = "a.*,b.PersonName as RecordmanName,c.PersonName as VerifyByName";
            }

            if (PlanType == MaintainPlanType.DailyPatrol || PlanType == MaintainPlanType.RoutineInspect || PlanType == MaintainPlanType.RoutineMaintain)
            {
                sqlSearch += "and a.PlanType=" + (int)PlanType;
            }

            searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by RecordDate desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            return SQLHelper.GetObjectList(this.GetData, searchTerm, out recordCount);
        }
        public IList GetList1(QueryParam searchTerm, out int recordCount)
        {
            return SQLHelper.GetObjectList(this.GetData1, searchTerm, out recordCount);
        }
        /// <summary>
        /// 获取当前记录之前的记录的巡查时间
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GetTheLastRecord(MaintainPlanRecordInfo info)
        {
            string thisRecordID = info.RecordID.ToString();
            string thisdate = info.RecordDate.ToString();
            string itemid = info.ItemID.ToString();
            string lastDate = null;
            string sql = string.Format("select max(RecordDate) from FM2E_MaintainPlanRecord where ItemID='{0}' and RecordDate <='{1}' and RecordID!='{2}'", itemid, thisdate, thisRecordID);
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
                {
                    if (rd.Read())
                    {
                        if (!Convert.IsDBNull(rd[0]))
                        {
                            lastDate = Convert.ToDateTime(rd[0]).ToString();
                        }
                        else
                        {
                            lastDate = string.Empty;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取日常巡查记录信息失败", e);
            }
            return lastDate;
        }
        public IList GetAllRecord(long itemID)
        {
            ArrayList list = new ArrayList();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,b.PersonName as RecordmanName,c.PersonName as VerifyByName from FM2E_MaintainPlanRecord a left join FM2E_User b on a.RecordmanID=b.UserName left join FM2E_User c on a.VerifyBy=c.UserName ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = itemID;
            MaintainPlanRecordInfo item = new MaintainPlanRecordInfo();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        item = this.GetData(rd);
                        list.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取日常巡查记录信息失败", e);
            }
            return list;
        }
        public IList GetAllEquipmentByRecordID(long RecordID)
        {
            string sql = string.Format("select * from FM2E_MaintainRecordEquipment where TableName='FM2E_MaintainPlanConfig' and RecordID='{0}'", RecordID);
            ArrayList list = new ArrayList();
            MaintainRecordEquipmentInfo item = new MaintainRecordEquipmentInfo();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
                {
                    while (rd.Read())
                    {
                        item = this.GetEquipmentData(rd);
                        list.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取日常巡查计划明细信息失败", e);
            }
            return list;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void InsertMaintainPlanRecord(MaintainPlanRecordInfo model)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            long thisID = 0;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先插入申请信息
                thisID = InsertRecord(trans, model);

                //插入地址信息
                model.RecordID = thisID;
                InsertAddress(trans, model);
                //插入申请明细信息
                if (model.EquipmentList != null)
                {
                    foreach (string i in model.EquipmentList)
                    {
                        MaintainRecordEquipmentInfo item = new MaintainRecordEquipmentInfo();
                        item.EquipmentNO = i;
                        item.RecordID = thisID;
                        item.TableName = "FM2E_MaintainPlanRecord";
                        InsertEquipments(trans, item);
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
        private long InsertRecord(SqlTransaction trans, MaintainPlanRecordInfo model)
        {
            long thisid = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_MaintainPlanRecord(");
            strSql.Append("VerifyBy,VerifyRemark,RecordDate,ItemID,RecordObject,RecordContent,RecordResult,RecordRemark,RecordmanID,VerifiedResult,PlanType)");
            strSql.Append(" values (");
            strSql.Append("@VerifyBy,@VerifyRemark,@RecordDate,@ItemID,@RecordObject,@RecordContent,@RecordResult,@RecordRemark,@RecordmanID,@VerifiedResult,@PlanType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@VerifyBy", SqlDbType.VarChar,20),
					new SqlParameter("@VerifyRemark", SqlDbType.NVarChar,100),
					new SqlParameter("@RecordDate", SqlDbType.DateTime),
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@RecordObject", SqlDbType.NVarChar,50),
					new SqlParameter("@RecordContent", SqlDbType.NVarChar,200),
					new SqlParameter("@RecordResult", SqlDbType.NVarChar,200),
					new SqlParameter("@RecordRemark", SqlDbType.NVarChar,200),
					new SqlParameter("@RecordmanID", SqlDbType.VarChar,20),
					new SqlParameter("@VerifiedResult", SqlDbType.TinyInt,1),
                    new SqlParameter("@PlanType",SqlDbType.TinyInt,1)};
            parameters[0].Value = model.VerifyBy;
            parameters[1].Value = model.VerifyRemark;
            parameters[2].Value = model.RecordDate;
            parameters[3].Value = model.ItemID;
            parameters[4].Value = model.RecordObject;
            parameters[5].Value = model.RecordContent;
            parameters[6].Value = model.RecordResult;
            parameters[7].Value = model.RecordRemark;
            parameters[8].Value = model.RecordmanID;
            parameters[9].Value = (int)model.VerifiedResult;
            parameters[10].Value = model.PlanType;
            SqlDataReader rdr = null;
            try
            {
                using (rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        if (!Convert.IsDBNull(rdr[0]))
                            thisid = Convert.ToInt64(rdr[0]);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("添加日常巡查记录信息失败", e);
            }
            return thisid;
        }
        private void InsertEquipments(SqlTransaction trans, MaintainRecordEquipmentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_MaintainRecordEquipment(");
            strSql.Append("TableName,RecordID,EquipmentNO,Name,Model)");
            strSql.Append(" values (");
            strSql.Append("@TableName,@RecordID,@EquipmentNO,@Name,@Model)");
            SqlParameter[] parameters = {
					new SqlParameter("@TableName", SqlDbType.VarChar,50),
					new SqlParameter("@RecordID", SqlDbType.BigInt,8),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@Model", SqlDbType.VarChar,20)};
            parameters[0].Value = model.TableName;
            parameters[1].Value = model.RecordID;
            parameters[2].Value = model.EquipmentNO;
            parameters[3].Value = "";
            parameters[4].Value = "";
            try
            {
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("添加日常巡查记录信息失败", e);
            }
        }
        private void InsertAddress(SqlTransaction trans, MaintainPlanRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_MaintainRecordAddress(");
            strSql.Append("TableName,RecordID,AddressID)");
            strSql.Append(" values(");
            strSql.Append("@TableName,@RecordID,@AddressID)");
            SqlParameter[] parameters = {
                                            new SqlParameter("@TableName",SqlDbType.VarChar,50),
                                            new SqlParameter("@RecordID",SqlDbType.BigInt,8),
                                            new SqlParameter("@AddressID",SqlDbType.BigInt,8)
                                        };
            parameters[0].Value = "FM2E_MaintainPlanRecord";
            parameters[1].Value = model.RecordID;
            parameters[2].Value = model.AddressID;
            try
            {
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("添加日常巡查地址信息失败", ex);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateMaintainPlanRecord(MaintainPlanRecordInfo model)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先更新申请信息
                updateRecord(trans, model);

                //先删除原来的明细，后添加新的明细
                StringBuilder delSql = new StringBuilder();
                delSql.AppendFormat("delete FM2E_MaintainRecordEquipment");
                delSql.Append(" where RecordID=@RecordID and TableName='FM2E_MaintainPlanRecord'");
                SqlParameter[] delPara = {
					new SqlParameter("@RecordID", SqlDbType.BigInt)};
                delPara[0].Value = model.RecordID;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, delSql.ToString(), delPara);

                //StringBuilder delAddressSql = new StringBuilder();
                //delAddressSql.AppendFormat("delete FM2E_MaintainRecordAddress");
                //delAddressSql.Append(" where RecordID=@RecordID and TableName='FM2E_MaintainPlanRecord'");
                //SQLHelper.ExecuteNonQuery(trans, CommandType.Text, delAddressSql.ToString(), delPara);

                //InsertAddress(trans, model);

                //插入申请明细信息
                if (model.EquipmentList != null)
                {
                    foreach (string i in model.EquipmentList)
                    {
                        MaintainRecordEquipmentInfo item = new MaintainRecordEquipmentInfo();
                        item.EquipmentNO = i;
                        item.RecordID = model.RecordID;
                        item.TableName = "FM2E_MaintainPlanRecord";
                        InsertEquipments(trans, item);
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
        private void updateRecord(SqlTransaction trans, MaintainPlanRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_MaintainPlanRecord set ");
            strSql.Append("VerifyBy=@VerifyBy,");
            strSql.Append("VerifyRemark=@VerifyRemark,");
            strSql.Append("RecordDate=@RecordDate,");
            strSql.Append("ItemID=@ItemID,");
            strSql.Append("RecordObject=@RecordObject,");
            strSql.Append("RecordContent=@RecordContent,");
            strSql.Append("RecordResult=@RecordResult,");
            strSql.Append("RecordRemark=@RecordRemark,");
            strSql.Append("RecordmanID=@RecordmanID,");
            strSql.Append("VerifiedResult=@VerifiedResult,");
            strSql.Append("PlanType=@PlanType");
            strSql.Append(" where RecordID=@RecordID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.BigInt,8),
					new SqlParameter("@VerifyBy", SqlDbType.VarChar,20),
					new SqlParameter("@VerifyRemark", SqlDbType.NVarChar,100),
					new SqlParameter("@RecordDate", SqlDbType.DateTime),
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@RecordObject", SqlDbType.NVarChar,50),
					new SqlParameter("@RecordContent", SqlDbType.NVarChar,200),
					new SqlParameter("@RecordResult", SqlDbType.NVarChar,200),
					new SqlParameter("@RecordRemark", SqlDbType.NVarChar,200),
					new SqlParameter("@RecordmanID", SqlDbType.VarChar,20),
					new SqlParameter("@VerifiedResult", SqlDbType.TinyInt,1),
                    new SqlParameter("@PlanType",SqlDbType.TinyInt,1)};
            parameters[0].Value = model.RecordID;
            parameters[1].Value = model.VerifyBy;
            parameters[2].Value = model.VerifyRemark;
            parameters[3].Value = model.RecordDate;
            parameters[4].Value = model.ItemID;
            parameters[5].Value = model.RecordObject;
            parameters[6].Value = model.RecordContent;
            parameters[7].Value = model.RecordResult;
            parameters[8].Value = model.RecordRemark;
            parameters[9].Value = model.RecordmanID;
            parameters[10].Value = (int)model.VerifiedResult;
            parameters[11].Value = model.PlanType;
            try
            {
                int result = SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("更新日常巡查记录信息失败", e);
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DelMaintainPlanRecord(long RecordID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_MaintainPlanRecord ");
                strSql.Append(" where RecordID=@RecordID ");
                SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.BigInt)};
                parameters[0].Value = RecordID;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
                StringBuilder delSql = new StringBuilder();
                delSql.AppendFormat("delete FM2E_MaintainRecordEquipment");
                delSql.Append(" where RecordID=@RecordID and TableName='FM2E_MaintainPlanRecord'");
                SqlParameter[] delPara = {
					new SqlParameter("@RecordID", SqlDbType.BigInt)};
                delPara[0].Value = RecordID;
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, delSql.ToString(), delPara);

                StringBuilder delAddressSql = new StringBuilder();
                delAddressSql.Append("delete FM2E_MaintainRecordAddress");
                delAddressSql.Append(" where RecordID=@RecordID and TableName='FM2E_MaintainPlanRecord' ");
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, delAddressSql.ToString(), delPara);
            }
            catch (Exception e)
            {
                throw new DALException("删除日常巡查记录信息失败", e);
            }
        }

        public long GetAddressIDByRecordID(long RecordID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 a.* from FM2E_MaintainRecordAddress a left join FM2E_MaintainPlanRecord b on a.RecordID=b.RecordID ");
            strSql.Append(" where a.RecordID=@RecordID ");
            SqlParameter[] parameters = {
                      new SqlParameter("@RecordID", SqlDbType.BigInt)};
            parameters[0].Value = RecordID;
            MaintainRecordAddressInfo item = new MaintainRecordAddressInfo();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        item = this.GetAddressData(rd);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取日常巡查地址信息失败" + e, e);
            }
            return item.AddressID;
        }

        private MaintainRecordAddressInfo GetAddressData(IDataReader rd)
        {
            MaintainRecordAddressInfo item = new MaintainRecordAddressInfo();
            if (!Convert.IsDBNull(rd["RecordID"]))
                item.RecordID = Convert.ToInt64(rd["RecordID"]);
            if (!Convert.IsDBNull(rd["TableName"]))
                item.TableName = Convert.ToString(rd["TableName"]);
            if (!Convert.IsDBNull(rd["AddressID"]))
                item.AddressID = Convert.ToInt64(rd["AddressID"]);
            return item;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MaintainPlanRecordInfo GetMaintainPlanRecord(long RecordID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 a.*,b.PersonName as RecordmanName,c.PersonName as VerifyByName from FM2E_MaintainPlanRecord a left join FM2E_User b on a.RecordmanID=b.UserName left join FM2E_User c on a.VerifyBy=c.UserName ");
            strSql.Append(" where RecordID=@RecordID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.BigInt)};
            parameters[0].Value = RecordID;
            MaintainPlanRecordInfo item = new MaintainPlanRecordInfo();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        item = this.GetData(rd);
                    }
                }
                if (item == null) return null;
                //获取计划明细列表
                StringBuilder strDetailSql = new StringBuilder();
                strDetailSql.Append("select * from FM2E_MaintainRecordEquipment");
                strDetailSql.Append(" where RecordID=@RecordID and TableName='FM2E_MaintainPlanRecord'");
                ArrayList list = new ArrayList();
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strDetailSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        MaintainRecordEquipmentInfo i = GetEquipmentData(rd);
                        list.Add(i.EquipmentNO);
                    }
                }
                item.EquipmentList = list;
            }
            catch (Exception e)
            {
                throw new DALException("获取日常巡查记录信息失败", e);
            }
            return item;
        }

        private MaintainRecordEquipmentInfo GetEquipmentData(IDataReader rd)
        {
            MaintainRecordEquipmentInfo item = new MaintainRecordEquipmentInfo();
            if (!Convert.IsDBNull(rd["RecordID"]))
                item.RecordID = Convert.ToInt64(rd["RecordID"]);
            if (!Convert.IsDBNull(rd["TableName"]))
                item.TableName = Convert.ToString(rd["TableName"]);
            if (!Convert.IsDBNull(rd["EquipmentNO"]))
                item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);
            if (!Convert.IsDBNull(rd["Name"]))
                item.Name = Convert.ToString(rd["Name"]);
            if (!Convert.IsDBNull(rd["Model"]))
                item.Model = Convert.ToString(rd["Model"]);
            return item;
        }

        private MaintainPlanRecordInfo GetData(IDataReader rd)
        {
            MaintainPlanRecordInfo item = new MaintainPlanRecordInfo();

            if (!Convert.IsDBNull(rd["RecordID"]))
                item.RecordID = Convert.ToInt64(rd["RecordID"]);

            if (!Convert.IsDBNull(rd["VerifiedResult"]))
                item.VerifiedResult = (MaintainPlanVerifiedResult)Convert.ToInt32(rd["VerifiedResult"]);

            if (!Convert.IsDBNull(rd["VerifyBy"]))
                item.VerifyBy = Convert.ToString(rd["VerifyBy"]);

            if (!Convert.IsDBNull(rd["VerifyRemark"]))
                item.VerifyRemark = Convert.ToString(rd["VerifyRemark"]);

            if (!Convert.IsDBNull(rd["RecordDate"]))
                item.RecordDate = Convert.ToDateTime(rd["RecordDate"]);

            if (!Convert.IsDBNull(rd["RecordRemark"]))
                item.RecordRemark = Convert.ToString(rd["RecordRemark"]);

            if (!Convert.IsDBNull(rd["ItemID"]))
                item.ItemID = Convert.ToInt64(rd["ItemID"]);

            if (!Convert.IsDBNull(rd["RecordObject"]))
                item.RecordObject = Convert.ToString(rd["RecordObject"]);

            if (!Convert.IsDBNull(rd["RecordContent"]))
                item.RecordContent = Convert.ToString(rd["RecordContent"]);

            if (!Convert.IsDBNull(rd["RecordResult"]))
                item.RecordResult = Convert.ToString(rd["RecordResult"]);

            if (!Convert.IsDBNull(rd["RecordmanID"]))
                item.RecordmanID = Convert.ToString(rd["RecordmanID"]);

            if (!Convert.IsDBNull(rd["RecordmanName"]))
                item.RecordmanName = Convert.ToString(rd["RecordmanName"]);

            if (!Convert.IsDBNull(rd["VerifyByName"]))
                item.VerifyName = Convert.ToString(rd["VerifyByName"]);

            if (!Convert.IsDBNull(rd["PlanType"]))
                item.PlanType = (MaintainPlanType)Convert.ToInt32(rd["PlanType"]);

            return item;
        }
        private MaintainPlanRecordInfo GetData1(IDataReader rd)
        {
            MaintainPlanRecordInfo item = new MaintainPlanRecordInfo();

            if (!Convert.IsDBNull(rd["RecordID"]))
                item.RecordID = Convert.ToInt64(rd["RecordID"]);

            if (!Convert.IsDBNull(rd["VerifiedResult"]))
                item.VerifiedResult = (MaintainPlanVerifiedResult)Convert.ToInt32(rd["VerifiedResult"]);

            if (!Convert.IsDBNull(rd["VerifyBy"]))
                item.VerifyBy = Convert.ToString(rd["VerifyBy"]);

            if (!Convert.IsDBNull(rd["VerifyRemark"]))
                item.VerifyRemark = Convert.ToString(rd["VerifyRemark"]);

            if (!Convert.IsDBNull(rd["RecordDate"]))
                item.RecordDate = Convert.ToDateTime(rd["RecordDate"]);

            if (!Convert.IsDBNull(rd["RecordRemark"]))
                item.RecordRemark = Convert.ToString(rd["RecordRemark"]);

            if (!Convert.IsDBNull(rd["ItemID"]))
                item.ItemID = Convert.ToInt64(rd["ItemID"]);

            if (!Convert.IsDBNull(rd["RecordObject"]))
                item.RecordObject = Convert.ToString(rd["RecordObject"]);

            if (!Convert.IsDBNull(rd["RecordContent"]))
                item.RecordContent = Convert.ToString(rd["RecordContent"]);

            if (!Convert.IsDBNull(rd["RecordResult"]))
                item.RecordResult = Convert.ToString(rd["RecordResult"]);

            if (!Convert.IsDBNull(rd["RecordmanID"]))
                item.RecordmanID = Convert.ToString(rd["RecordmanID"]);

            if (!Convert.IsDBNull(rd["RecordmanName"]))
                item.RecordmanName = Convert.ToString(rd["RecordmanName"]);

            if (!Convert.IsDBNull(rd["VerifyByName"]))
                item.VerifyName = Convert.ToString(rd["VerifyByName"]);

            if (!Convert.IsDBNull(rd["PlanType"]))
                item.PlanType = (MaintainPlanType)Convert.ToInt32(rd["PlanType"]);

            return item;
        }

    }
}
