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
    public class DailyPatrolRecord : IDailyPatrolRecord
    {
        public QueryParam GenerateSearchTerm(DailyPatrolRecordInfo item)
        {
            string sqlSearch = "where 1=1";

            if (item.ItemID != 0)
                sqlSearch += " and a.ItemID ='" + item.ItemID + "'";

            if (item.PatrolmanName != null && item.PatrolmanName != "")
                sqlSearch += " and a.PatrolmanName like '%" + item.PatrolmanName + "%'";

            if (item.CompanyID != null && item.CompanyID != "")
                sqlSearch += " and d.CompanyID='" + item.CompanyID + "'";

            if (item.VerifiedResult == DailyPatrolVerifiedResult.NotVerified)
            {
                sqlSearch += " and a.VerifiedResult=" + (int)item.VerifiedResult;
            }
            else if (item.VerifiedResult == DailyPatrolVerifiedResult.Verified)
            {
                sqlSearch += " and (a.VerifiedResult=" + (int)DailyPatrolVerifiedResult.NotCompleted + " or a.VerifiedResult=" + (int)DailyPatrolVerifiedResult.CompletedAsPlanned + ")";
            }

            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_DailyPatrolRecord a left join FM2E_User b on a.PatrolmanID=b.UserName left join FM2E_User c on a.VerifyBy=c.UserName left join FM2E_DailyPatrolConfig d on a.ItemID=d.ItemID";
            searchTerm.ReturnFields = "a.*,b.PersonName as PatrolmanName,c.PersonName as VerifyByName";
            searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by PatrolDate desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        public QueryParam GenerateSearchTerm1(string system, long subsystem, string EquipmentNO)
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
                sqlSearch += " and e.TableName='FM2E_DailyPatrolRecord'";
                searchTerm.TableName = "FM2E_MaintainRecordEquipment e left join FM2E_DailyPatrolRecord a on e.RecordID = a.RecordID left join FM2E_User b on a.PatrolmanID=b.UserName left join FM2E_User c on a.VerifyBy=c.UserName";
                searchTerm.ReturnFields = "a.*,b.PersonName as PatrolmanName,c.PersonName as VerifyByName";
            }
            else
            {
                searchTerm.TableName = "FM2E_DailyPatrolRecord a left join FM2E_User b on a.PatrolmanID=b.UserName left join FM2E_User c on a.VerifyBy=c.UserName left join FM2E_DailyPatrolConfig d on a.ItemID = d.ItemID";
                searchTerm.ReturnFields = "a.*,b.PersonName as PatrolmanName,c.PersonName as VerifyByName";
            }
            searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by PatrolDate desc";
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
        public string GetTheLastRecord(DailyPatrolRecordInfo info)
        {
            string thisRecordID = info.RecordID.ToString();
            string thisdate = info.PatrolDate.ToString();
            string itemid = info.ItemID.ToString();
            string lastDate = null;
            string sql = string.Format("select max(PatrolDate) from FM2E_DailyPatrolRecord where ItemID='{0}' and PatrolDate <='{1}' and RecordID!='{2}'", itemid, thisdate, thisRecordID);
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
            strSql.Append("select a.*,b.PersonName as PatrolmanName,c.PersonName as VerifyByName from FM2E_DailyPatrolRecord a left join FM2E_User b on a.PatrolmanID=b.UserName left join FM2E_User c on a.VerifyBy=c.UserName ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = itemID;
            DailyPatrolRecordInfo item = new DailyPatrolRecordInfo();
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
            string sql = string.Format("select * from FM2E_MaintainRecordEquipment where TableName='FM2E_DailyPatrolConfig' and RecordID='{0}'", RecordID);
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
        public void InsertDailyPatrolRecord(DailyPatrolRecordInfo model)
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
                        item.TableName = "FM2E_DailyPatrolRecord";
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
        private long InsertRecord(SqlTransaction trans, DailyPatrolRecordInfo model)
        {
            long thisid = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_DailyPatrolRecord(");
            strSql.Append("VerifyBy,VerifyRemark,PatrolDate,ItemID,PatrolObject,PatrolContent,PatrolResult,PatrolRemark,PatrolmanID,VerifiedResult)");
            strSql.Append(" values (");
            strSql.Append("@VerifyBy,@VerifyRemark,@PatrolDate,@ItemID,@PatrolObject,@PatrolContent,@PatrolResult,@PatrolRemark,@PatrolmanID,@VerifiedResult)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@VerifyBy", SqlDbType.VarChar,20),
					new SqlParameter("@VerifyRemark", SqlDbType.NVarChar,100),
					new SqlParameter("@PatrolDate", SqlDbType.DateTime),
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@PatrolObject", SqlDbType.NVarChar,50),
					new SqlParameter("@PatrolContent", SqlDbType.NVarChar,200),
					new SqlParameter("@PatrolResult", SqlDbType.NVarChar,200),
					new SqlParameter("@PatrolRemark", SqlDbType.NVarChar,200),
					new SqlParameter("@PatrolmanID", SqlDbType.VarChar,20),
					new SqlParameter("@VerifiedResult", SqlDbType.TinyInt,1)};
            parameters[0].Value = model.VerifyBy;
            parameters[1].Value = model.VerifyRemark;
            parameters[2].Value = model.PatrolDate;
            parameters[3].Value = model.ItemID;
            parameters[4].Value = model.PatrolObject;
            parameters[5].Value = model.PatrolContent;
            parameters[6].Value = model.PatrolResult;
            parameters[7].Value = model.PatrolRemark;
            parameters[8].Value = model.PatrolmanID;
            parameters[9].Value = (int)model.VerifiedResult;
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
        private void InsertAddress(SqlTransaction trans, DailyPatrolRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_MaintainPatrolAddress(");
            strSql.Append("TableName,RecordID,AddressID)");
            strSql.Append(" values(");
            strSql.Append("@TableName,@RecordID,@AddressID)");
            SqlParameter[] parameters = {
                                            new SqlParameter("@TableName",SqlDbType.VarChar,50),
                                            new SqlParameter("@RecordID",SqlDbType.BigInt,8),
                                            new SqlParameter("@AddressID",SqlDbType.BigInt,8)
                                        };
            parameters[0].Value = "FM2E_DailyPatrolRecord";
            parameters[1].Value = model.RecordID;
            parameters[2].Value = model.AddressID;
            try 
            {
                SQLHelper.ExecuteNonQuery(trans,CommandType.Text,strSql.ToString(),parameters);
            }
            catch (Exception ex)
            { 
                throw new DALException("添加日常巡查地址信息失败",ex); 
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateDailyPatrolRecord(DailyPatrolRecordInfo model)
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
                delSql.Append(" where RecordID=@RecordID and TableName='FM2E_DailyPatrolRecord'");
                SqlParameter[] delPara = {
					new SqlParameter("@RecordID", SqlDbType.BigInt)};
                delPara[0].Value = model.RecordID;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, delSql.ToString(), delPara);

                //StringBuilder delAddressSql = new StringBuilder();
                //delAddressSql.AppendFormat("delete FM2E_MaintainPatrolAddress");
                //delAddressSql.Append(" where RecordID=@RecordID and TableName='FM2E_DailyPatrolRecord'");
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
                        item.TableName = "FM2E_DailyPatrolRecord";
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
        private void updateRecord(SqlTransaction trans, DailyPatrolRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_DailyPatrolRecord set ");
            strSql.Append("VerifyBy=@VerifyBy,");
            strSql.Append("VerifyRemark=@VerifyRemark,");
            strSql.Append("PatrolDate=@PatrolDate,");
            strSql.Append("ItemID=@ItemID,");
            strSql.Append("PatrolObject=@PatrolObject,");
            strSql.Append("PatrolContent=@PatrolContent,");
            strSql.Append("PatrolResult=@PatrolResult,");
            strSql.Append("PatrolRemark=@PatrolRemark,");
            strSql.Append("PatrolmanID=@PatrolmanID,");
            strSql.Append("VerifiedResult=@VerifiedResult");
            strSql.Append(" where RecordID=@RecordID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.BigInt,8),
					new SqlParameter("@VerifyBy", SqlDbType.VarChar,20),
					new SqlParameter("@VerifyRemark", SqlDbType.NVarChar,100),
					new SqlParameter("@PatrolDate", SqlDbType.DateTime),
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@PatrolObject", SqlDbType.NVarChar,50),
					new SqlParameter("@PatrolContent", SqlDbType.NVarChar,200),
					new SqlParameter("@PatrolResult", SqlDbType.NVarChar,200),
					new SqlParameter("@PatrolRemark", SqlDbType.NVarChar,200),
					new SqlParameter("@PatrolmanID", SqlDbType.VarChar,20),
					new SqlParameter("@VerifiedResult", SqlDbType.TinyInt,1)};
            parameters[0].Value = model.RecordID;
            parameters[1].Value = model.VerifyBy;
            parameters[2].Value = model.VerifyRemark;
            parameters[3].Value = model.PatrolDate;
            parameters[4].Value = model.ItemID;
            parameters[5].Value = model.PatrolObject;
            parameters[6].Value = model.PatrolContent;
            parameters[7].Value = model.PatrolResult;
            parameters[8].Value = model.PatrolRemark;
            parameters[9].Value = model.PatrolmanID;
            parameters[10].Value = (int)model.VerifiedResult;
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
        public void DelDailyPatrolRecord(long RecordID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_DailyPatrolRecord ");
                strSql.Append(" where RecordID=@RecordID ");
                SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.BigInt)};
                parameters[0].Value = RecordID;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
                StringBuilder delSql = new StringBuilder();
                delSql.AppendFormat("delete FM2E_MaintainRecordEquipment");
                delSql.Append(" where RecordID=@RecordID and TableName='FM2E_DailyPatrolRecord'");
                SqlParameter[] delPara = {
					new SqlParameter("@RecordID", SqlDbType.BigInt)};
                delPara[0].Value = RecordID;
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, delSql.ToString(), delPara);

                StringBuilder delAddressSql = new StringBuilder();
                delAddressSql.Append("delete FM2E_MaintainPatrolAddress");
                delAddressSql.Append(" where RecordID=@RecordID and TableName='FM2E_DailyPatrolRecord' ");
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text,delAddressSql.ToString(),delPara);
            }
            catch (Exception e)
            {
                throw new DALException("删除日常巡查记录信息失败", e);
            }
        }

        public long GetAddressIDByRecordID(long RecordID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 a.* from FM2E_MaintainPatrolAddress a left join FM2E_DailyPatrolRecord b on a.RecordID=b.RecordID ");
            strSql.Append(" where a.RecordID=@RecordID ");
            SqlParameter[] parameters = {
                      new SqlParameter("@RecordID", SqlDbType.BigInt)};
            parameters[0].Value = RecordID;
            MaintainPatrolAddressInfo item = new MaintainPatrolAddressInfo();
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
                throw new DALException("获取日常巡查地址信息失败"+e, e);
            }
            return item.AddressID;
        }

        private MaintainPatrolAddressInfo GetAddressData(IDataReader rd)
        {
            MaintainPatrolAddressInfo item = new MaintainPatrolAddressInfo();
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
        public DailyPatrolRecordInfo GetDailyPatrolRecord(long RecordID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 a.*,b.PersonName as PatrolmanName,c.PersonName as VerifyByName from FM2E_DailyPatrolRecord a left join FM2E_User b on a.PatrolmanID=b.UserName left join FM2E_User c on a.VerifyBy=c.UserName ");
            strSql.Append(" where RecordID=@RecordID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.BigInt)};
            parameters[0].Value = RecordID;
            DailyPatrolRecordInfo item = new DailyPatrolRecordInfo();
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
                strDetailSql.Append(" where RecordID=@RecordID and TableName='FM2E_DailyPatrolRecord'");
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

        private DailyPatrolRecordInfo GetData(IDataReader rd)
        {
            DailyPatrolRecordInfo item = new DailyPatrolRecordInfo();

            if (!Convert.IsDBNull(rd["RecordID"]))
                item.RecordID = Convert.ToInt64(rd["RecordID"]);

            if (!Convert.IsDBNull(rd["VerifiedResult"]))
                item.VerifiedResult = (DailyPatrolVerifiedResult)Convert.ToInt32(rd["VerifiedResult"]);

            if (!Convert.IsDBNull(rd["VerifyBy"]))
                item.VerifyBy = Convert.ToString(rd["VerifyBy"]);

            if (!Convert.IsDBNull(rd["VerifyRemark"]))
                item.VerifyRemark = Convert.ToString(rd["VerifyRemark"]);

            if (!Convert.IsDBNull(rd["PatrolDate"]))
                item.PatrolDate = Convert.ToDateTime(rd["PatrolDate"]);

            if (!Convert.IsDBNull(rd["PatrolRemark"]))
                item.PatrolRemark = Convert.ToString(rd["PatrolRemark"]);

            if (!Convert.IsDBNull(rd["ItemID"]))
                item.ItemID = Convert.ToInt64(rd["ItemID"]);

            if (!Convert.IsDBNull(rd["PatrolObject"]))
                item.PatrolObject = Convert.ToString(rd["PatrolObject"]);

            if (!Convert.IsDBNull(rd["PatrolContent"]))
                item.PatrolContent = Convert.ToString(rd["PatrolContent"]);

            if (!Convert.IsDBNull(rd["PatrolResult"]))
                item.PatrolResult = Convert.ToString(rd["PatrolResult"]);

            if (!Convert.IsDBNull(rd["PatrolmanID"]))
                item.PatrolmanID = Convert.ToString(rd["PatrolmanID"]);

            if (!Convert.IsDBNull(rd["PatrolmanName"]))
                item.PatrolmanName = Convert.ToString(rd["PatrolmanName"]);

            if (!Convert.IsDBNull(rd["VerifyByName"]))
                item.VerifyName = Convert.ToString(rd["VerifyByName"]);

            return item;
        }
        private DailyPatrolRecordInfo GetData1(IDataReader rd)
        {
            DailyPatrolRecordInfo item = new DailyPatrolRecordInfo();

            if (!Convert.IsDBNull(rd["RecordID"]))
                item.RecordID = Convert.ToInt64(rd["RecordID"]);

            if (!Convert.IsDBNull(rd["VerifiedResult"]))
                item.VerifiedResult = (DailyPatrolVerifiedResult)Convert.ToInt32(rd["VerifiedResult"]);

            if (!Convert.IsDBNull(rd["VerifyBy"]))
                item.VerifyBy = Convert.ToString(rd["VerifyBy"]);

            if (!Convert.IsDBNull(rd["VerifyRemark"]))
                item.VerifyRemark = Convert.ToString(rd["VerifyRemark"]);

            if (!Convert.IsDBNull(rd["PatrolDate"]))
                item.PatrolDate = Convert.ToDateTime(rd["PatrolDate"]);

            if (!Convert.IsDBNull(rd["PatrolRemark"]))
                item.PatrolRemark = Convert.ToString(rd["PatrolRemark"]);

            if (!Convert.IsDBNull(rd["ItemID"]))
                item.ItemID = Convert.ToInt64(rd["ItemID"]);

            if (!Convert.IsDBNull(rd["PatrolObject"]))
                item.PatrolObject = Convert.ToString(rd["PatrolObject"]);

            if (!Convert.IsDBNull(rd["PatrolContent"]))
                item.PatrolContent = Convert.ToString(rd["PatrolContent"]);

            if (!Convert.IsDBNull(rd["PatrolResult"]))
                item.PatrolResult = Convert.ToString(rd["PatrolResult"]);

            if (!Convert.IsDBNull(rd["PatrolmanID"]))
                item.PatrolmanID = Convert.ToString(rd["PatrolmanID"]);

            if (!Convert.IsDBNull(rd["PatrolmanName"]))
                item.PatrolmanName = Convert.ToString(rd["PatrolmanName"]);

            if (!Convert.IsDBNull(rd["VerifyByName"]))
                item.VerifyName = Convert.ToString(rd["VerifyByName"]);

            return item;
        }
    }
}
