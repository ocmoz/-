﻿using System;
using System.Collections.Generic;
using System.Text;

using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;
using FM2E.IDAL.Equipment;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;
using System.Data.SqlTypes;

namespace FM2E.SQLServerDAL.Equipment
{
    public class BorrowRecord : IBorrowRecord
    {
        private BorrowRecordInfo GetData(IDataReader dr)
        {
            BorrowRecordInfo item = new BorrowRecordInfo();


            if (!Convert.IsDBNull(dr["BorrowApplyID"]))
                item.BorrowApplyID = Convert.ToInt64(dr["BorrowApplyID"]);

            if(!Convert.IsDBNull(dr["SheetNO"]))
                item.SheetNO=Convert.ToString(dr["SheetNO"]);

            if (!Convert.IsDBNull(dr["ItemID"]))
                item.ItemID = Convert.ToInt64(dr["ItemID"]);

            if (!Convert.IsDBNull(dr["EquipmentNO"]))
                item.EquipmentNO = Convert.ToString(dr["EquipmentNO"]);

            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);

            if (!Convert.IsDBNull(dr["ReturnDate"]))
                item.ReturnDate = Convert.ToDateTime(dr["ReturnDate"]);

            if (!Convert.IsDBNull(dr["Reason"]))
                item.Reason = Convert.ToString(dr["Reason"]);

            if (!Convert.IsDBNull(dr["BorrowCompany"]))
                item.BorrowCompany = Convert.ToString(dr["BorrowCompany"]);

            if (!Convert.IsDBNull(dr["BorrowCompanyName"]))
                item.BorrowCompanyName = Convert.ToString(dr["BorrowCompanyName"]);

            if (!Convert.IsDBNull(dr["BorrowTime"]))
                item.BorrowTime = Convert.ToDateTime(dr["BorrowTime"]);

            if (!Convert.IsDBNull(dr["Borrower"]))
                item.Borrower = Convert.ToString(dr["Borrower"]);

            if (!Convert.IsDBNull(dr["BorrowerName"]))
                item.BorrowerName = Convert.ToString(dr["BorrowerName"]);

            if (!Convert.IsDBNull(dr["Recorder"]))
                item.Recorder = Convert.ToString(dr["Recorder"]);

            if (!Convert.IsDBNull(dr["IsReturned"]))
                item.IsReturned = Convert.ToBoolean(dr["IsReturned"]);

            return item;
        }
        #region IBorrowRecord 成员
        /// <summary>
        /// 获取某个借调申请的借出设备登记明细
        /// </summary>
        /// <param name="borrowApplyID"></param>
        /// <returns></returns>
        IList IBorrowRecord.GetBorrowRecordList(long borrowApplyID)
        {
            ArrayList list = new ArrayList();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select a.*,b.PersonName as BorrowerName,c.PersonName as RecorderName,d.Name as EquipmentName,d.Model,d.Specification ");
                strSql.Append(" FROM FM2E_BorrowRecord a left join FM2E_User b on a.Borrower=b.UserName left join FM2E_User c on a.Recorder=c.UserName ");
                strSql.Append(" left join FM2E_Equipment d on a.EquipmentNO=d.EquipmentNO");
                strSql.Append(" where a.BorrowApplyID=@BorrowApplyID");

                SqlParameter[] parameters = {
					new SqlParameter("@BorrowApplyID", SqlDbType.BigInt)};
                parameters[0].Value = borrowApplyID;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        //list.Add(GetData(rd));
                        BorrowRecordInfo item = new BorrowRecordInfo();

                        if (!Convert.IsDBNull(rd["BorrowApplyID"]))
                            item.BorrowApplyID = Convert.ToInt64(rd["BorrowApplyID"]);

                        if (!Convert.IsDBNull(rd["ItemID"]))
                            item.ItemID = Convert.ToInt64(rd["ItemID"]);

                        if (!Convert.IsDBNull(rd["EquipmentNO"]))
                            item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

                        if (!Convert.IsDBNull(rd["CompanyID"]))
                            item.CompanyID = Convert.ToString(rd["CompanyID"]);

                        if (!Convert.IsDBNull(rd["ReturnDate"]))
                            item.ReturnDate = Convert.ToDateTime(rd["ReturnDate"]);

                        if (!Convert.IsDBNull(rd["Reason"]))
                            item.Reason = Convert.ToString(rd["Reason"]);

                        if (!Convert.IsDBNull(rd["BorrowCompany"]))
                            item.BorrowCompany = Convert.ToString(rd["BorrowCompany"]);

                        if (!Convert.IsDBNull(rd["BorrowTime"]))
                            item.BorrowTime = Convert.ToDateTime(rd["BorrowTime"]);

                        if (!Convert.IsDBNull(rd["BorrowTime"]))
                            item.BorrowTime = Convert.ToDateTime(rd["BorrowTime"]);

                        if (!Convert.IsDBNull(rd["Borrower"]))
                            item.Borrower = Convert.ToString(rd["Borrower"]);

                        if (!Convert.IsDBNull(rd["BorrowerName"]))
                            item.BorrowerName = Convert.ToString(rd["BorrowerName"]);

                        if (!Convert.IsDBNull(rd["Recorder"]))
                            item.Recorder = Convert.ToString(rd["Recorder"]);

                        if (!Convert.IsDBNull(rd["RecorderName"]))
                            item.RecorderName = Convert.ToString(rd["RecorderName"]);

                        if (!Convert.IsDBNull(rd["EquipmentName"]))
                            item.EquipmentName = Convert.ToString(rd["EquipmentName"]);

                        item.Model="";
                        if (!Convert.IsDBNull(rd["Model"]))
                            item.Model += Convert.ToString(rd["Model"]);

                        if (!Convert.IsDBNull(rd["Specification"]))
                            item.Model += "/" + Convert.ToString(rd["Specification"]);

                        if (!Convert.IsDBNull(rd["IsReturned"]))
                            item.IsReturned = Convert.ToBoolean(rd["IsReturned"]);

                        list.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                list.Clear();
                throw new DALException("获取设备借出信息失败", e);
            }
            return list;
        }
        /// <summary>
        /// 获取借出设备登记明细(支持分页)
        /// </summary>
        /// <param name="borrowApplyID"></param>
        /// <returns></returns>
        IList IBorrowRecord.GetBorrowRecordList(QueryParam term, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取借出设备登记明细失败", e);
            }
        }
        /// <summary>
        /// 获取某个设备的借调历史明细
        /// </summary>
        /// <param name="equipmentNO"></param>
        /// <returns></returns>
        IList IBorrowRecord.GetBorrowRecordHistory(string equipmentNO)
        {
            ArrayList list = new ArrayList();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select a.*,b.PersonName as BorrowerName,c.PersonName as RecorderName ");
                strSql.Append(" FROM FM2E_BorrowRecord a left join FM2E_User b on a.Borrower=b.UserName left join FM2E_User c on a.RecorderName=c.UserName ");
                strSql.Append(" where a.EquipmentNO=@EquipmentNO");

                SqlParameter[] parameters = {
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50)};
                parameters[0].Value = equipmentNO;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
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
                throw new DALException("获取设备借调历史明细失败", e);
            }
            return list;
        }
        /// <summary>
        /// 获取某个未归还设备的借出信息
        /// </summary>
        /// <param name="equipmentNO"></param>
        /// <returns></returns>
        BorrowRecordInfo IBorrowRecord.GetEquipmentNotReturned(string equipmentNO)
        {
            BorrowRecordInfo item = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from FM2E_BorrowRecord ");
                strSql.Append(" where EquipmentNO=@EquipmentNO and IsReturned=0");
                SqlParameter[] parameters = {
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50)};
                parameters[0].Value = equipmentNO;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        item = new BorrowRecordInfo();

                        if (!Convert.IsDBNull(rd["BorrowApplyID"]))
                            item.BorrowApplyID = Convert.ToInt64(rd["BorrowApplyID"]);

                        if (!Convert.IsDBNull(rd["ItemID"]))
                            item.ItemID = Convert.ToInt64(rd["ItemID"]);

                        if (!Convert.IsDBNull(rd["EquipmentNO"]))
                            item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

                        if (!Convert.IsDBNull(rd["CompanyID"]))
                            item.CompanyID = Convert.ToString(rd["CompanyID"]);

                        if (!Convert.IsDBNull(rd["ReturnDate"]))
                            item.ReturnDate = Convert.ToDateTime(rd["ReturnDate"]);

                        if (!Convert.IsDBNull(rd["Reason"]))
                            item.Reason = Convert.ToString(rd["Reason"]);

                        if (!Convert.IsDBNull(rd["BorrowCompany"]))
                            item.BorrowCompany = Convert.ToString(rd["BorrowCompany"]);

                        if (!Convert.IsDBNull(rd["BorrowTime"]))
                            item.BorrowTime = Convert.ToDateTime(rd["BorrowTime"]);

                        if (!Convert.IsDBNull(rd["Borrower"]))
                            item.Borrower = Convert.ToString(rd["Borrower"]);

                        if (!Convert.IsDBNull(rd["Recorder"]))
                            item.Recorder = Convert.ToString(rd["Recorder"]);

                        if (!Convert.IsDBNull(rd["IsReturned"]))
                            item.IsReturned = Convert.ToBoolean(rd["IsReturned"]);
                    }
                }
            }
            catch (Exception ex)
            {
                item = null;
                throw new DALException("获取设备的借出历史失败", ex);
            }
            return item;
        }
        /// <summary>
        /// 获取某个设备的借出记录
        /// </summary>
        /// <param name="borrowApplyID"></param>
        /// <param name="equipmentNO"></param>
        /// <returns></returns>
        BorrowRecordInfo IBorrowRecord.GetBorrowRecord(long borrowApplyID, string equipmentNO)
        {
            BorrowRecordInfo item = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  top 1 BorrowApplyID,ItemID,Recorder,EquipmentNO,CompanyID,ReturnDate,Reason,BorrowTime,BorrowCompany,Borrower,IsReturned from FM2E_BorrowRecord ");
                strSql.Append(" where BorrowApplyID=@BorrowApplyID and EquipmentNO=@EquipmentNO ");
                SqlParameter[] parameters = {
					new SqlParameter("@BorrowApplyID", SqlDbType.BigInt),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50)};
                parameters[0].Value = borrowApplyID;
                parameters[1].Value = equipmentNO;
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        item = new BorrowRecordInfo();

                        if (!Convert.IsDBNull(rd["BorrowApplyID"]))
                            item.BorrowApplyID = Convert.ToInt64(rd["BorrowApplyID"]);

                        if (!Convert.IsDBNull(rd["ItemID"]))
                            item.ItemID = Convert.ToInt64(rd["ItemID"]);

                        if (!Convert.IsDBNull(rd["EquipmentNO"]))
                            item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

                        if (!Convert.IsDBNull(rd["CompanyID"]))
                            item.CompanyID = Convert.ToString(rd["CompanyID"]);

                        if (!Convert.IsDBNull(rd["ReturnDate"]))
                            item.ReturnDate = Convert.ToDateTime(rd["ReturnDate"]);

                        if (!Convert.IsDBNull(rd["Reason"]))
                            item.Reason = Convert.ToString(rd["Reason"]);

                        if (!Convert.IsDBNull(rd["BorrowCompany"]))
                            item.BorrowCompany = Convert.ToString(rd["BorrowCompany"]);

                        if (!Convert.IsDBNull(rd["BorrowTime"]))
                            item.BorrowTime = Convert.ToDateTime(rd["BorrowTime"]);

                        if (!Convert.IsDBNull(rd["Borrower"]))
                            item.Borrower = Convert.ToString(rd["Borrower"]);

                        if (!Convert.IsDBNull(rd["Recorder"]))
                            item.Recorder = Convert.ToString(rd["Recorder"]);

                        if (!Convert.IsDBNull(rd["IsReturned"]))
                            item.IsReturned = Convert.ToBoolean(rd["IsReturned"]);
                    }
                }
            }
            catch (Exception ex)
            {
                item = null;
                throw new DALException("获取设备借出记录失败",ex);
            }
            return item;
        }
        /// <summary>
        /// 设备借出登记
        /// </summary>
        /// <param name="borrowRecords"></param>
        void IBorrowRecord.AddBorrowRecord(IList borrowRecords)
        {
            if (borrowRecords == null || borrowRecords.Count == 0)
                return;

            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FM2E_BorrowRecord(");
                strSql.Append("BorrowApplyID,ItemID,EquipmentNO,CompanyID,ReturnDate,Reason,BorrowTime,BorrowCompany,Borrower,Recorder,IsReturned)");
                strSql.Append(" values (");
                strSql.Append("@BorrowApplyID,@ItemID,@EquipmentNO,@CompanyID,@ReturnDate,@Reason,@BorrowTime,@BorrowCompany,@Borrower,@Recorder,@IsReturned)");
                SqlParameter[] parameters = {
					new SqlParameter("@BorrowApplyID", SqlDbType.BigInt,8),
                    new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@ReturnDate", SqlDbType.DateTime),
					new SqlParameter("@Reason", SqlDbType.NVarChar,50),
					new SqlParameter("@BorrowTime", SqlDbType.DateTime),
					new SqlParameter("@BorrowCompany", SqlDbType.VarChar,2),
					new SqlParameter("@Borrower", SqlDbType.VarChar,20),
                    new SqlParameter("@Recorder", SqlDbType.VarChar,20),
                    new SqlParameter("@IsReturned",SqlDbType.Bit)};

                Equipment equipment = new Equipment();
                foreach (BorrowRecordInfo item in borrowRecords)
                {
                    parameters[0].Value = item.BorrowApplyID;
                    parameters[1].Value = item.ItemID;
                    parameters[2].Value = item.EquipmentNO;
                    parameters[3].Value = item.CompanyID;
                    parameters[4].Value = item.ReturnDate;
                    parameters[5].Value = item.Reason;
                    parameters[6].Value = item.BorrowTime;
                    parameters[7].Value = item.BorrowCompany;
                    parameters[8].Value = item.Borrower;
                    parameters[9].Value = item.Recorder;
                    parameters[10].Value = item.IsReturned;

                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);


                    //更新地址信息
                    //equipment.UpdateEquipment(trans, item.EquipmentNO, item.SectionID, item.SystemID, item.LocationID, item.LocationTag);
                    equipment.UpdateEquipmentAddress(trans, item.EquipmentNO, item.AddressID, item.DetailLocation);
                }

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new DALException("添加设备借出登记信息失败", e);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn = null;
                }
            }
        }
        /// <summary>
        /// 删除某张借调申请单的设备借出记录
        /// </summary>
        /// <param name="borrowApplyID"></param>
        void IBorrowRecord.DeleteBorrowRecord(long borrowApplyID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_BorrowRecord ");
                strSql.Append(" where BorrowApplyID=@BorrowApplyID");
                SqlParameter[] parameters = {
					new SqlParameter("@BorrowApplyID", SqlDbType.BigInt)};
                parameters[0].Value = borrowApplyID;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除设备借出记录失败", e);
            }
        }
        /// <summary>
        /// 删除某张借调申请单的某个设备的借出记录
        /// </summary>
        /// <param name="borrowApplyID"></param>
        /// <param name="equipmentNO"></param>
        void IBorrowRecord.DeleteBorrowRecord(long borrowApplyID, string equipmentNO)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_BorrowRecord ");
                strSql.Append(" where BorrowApplyID=@BorrowApplyID and EquipmentNO=@EquipmentNO ");
                SqlParameter[] parameters = {
					new SqlParameter("@BorrowApplyID", SqlDbType.BigInt),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50)};
                parameters[0].Value = borrowApplyID;
                parameters[1].Value = equipmentNO;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除设备借出记录失败", e);
            }
        }
        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam IBorrowRecord.GenerateSearchTerm(BorrowRecordSearchInfo term)
        {
            #region 生成where条件
            string sqlSearch = "where 1=1";
            if (!string.IsNullOrEmpty(term.SheetNO))
            {
                sqlSearch += " and d.SheetName like '%" + term.SheetNO + "%'";
            }
            if (!string.IsNullOrEmpty(term.BorrowCompanyID) && term.BorrowCompanyID != "0")
            {
                sqlSearch += " and a.BorrowCompany='" + term.BorrowCompanyID + "'";
            }
            if (!string.IsNullOrEmpty(term.BorrowerName))
            {
                sqlSearch += " and c.PersonName like '%" + term.BorrowerName + "%'";
            }
            if (!string.IsNullOrEmpty(term.Recorder))
            {
                sqlSearch += " and a.Recorder='" + term.Recorder + "'";
            }
            if (DateTime.Compare(term.BorrowTimeFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(term.BorrowTimeFrom, sqlMinDate) < 0)
                    term.BorrowTimeFrom = sqlMinDate;

                sqlSearch += " and a.BorrowTime>='" + term.BorrowTimeFrom.ToShortDateString() + " 00:00:00'";
            }
            if (DateTime.Compare(term.BorrowTimeTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(term.BorrowTimeTo, sqlMaxDate) > 0)
                    term.BorrowTimeTo = sqlMaxDate;

                sqlSearch += " and a.BorrowTime<='" + term.BorrowTimeTo.ToShortDateString() + " 23:59:59'";
            }
            if (DateTime.Compare(term.ReturnDateFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(term.ReturnDateFrom, sqlMinDate) < 0)
                    term.ReturnDateFrom = sqlMinDate;

                sqlSearch += " and a.ReturnDate>='" + term.ReturnDateFrom.ToShortDateString() + " 00:00:00'";
            }
            if (DateTime.Compare(term.ReturnDateTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(term.ReturnDateTo, sqlMaxDate) > 0)
                    term.ReturnDateTo = sqlMaxDate;

                sqlSearch += " and a.ReturnDate<='" + term.ReturnDateTo.ToShortDateString() + " 23:59:59'";
            }
            #endregion
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_BorrowRecord a left join FM2E_Company b on a.BorrowCompany=b.CompanyID left join FM2E_User c on a.Borrower=c.UserName left join FM2E_BorrowApply d on a.BorrowApplyID=d.BorrowApplyID ";
            searchTerm.ReturnFields = "a.*,b.CompanyName as BorrowCompanyName,c.PersonName as BorrowerName,d.SheetName as SheetNO ";
            searchTerm.OrderBy = "order by BorrowTime desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }

        #endregion
    }
}
