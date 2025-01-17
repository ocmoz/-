﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using FM2E.SQLServerDAL.Utils;
using FM2E.IDAL.Equipment;
using FM2E.Model.Equipment;
using FM2E.Model.Exceptions;
using FM2E.Model.Utils;

using System.Data.SqlTypes;

namespace FM2E.SQLServerDAL.Equipment
{
    public class ScrapRecord:IScrapRecord
    {
        private ScrapRecordInfo GetData(IDataReader dr)
        {
            ScrapRecordInfo item = new ScrapRecordInfo();

            if (!Convert.IsDBNull(dr["ScrapID"]))
                item.ScrapID = Convert.ToInt64(dr["ScrapID"]);

            if (!Convert.IsDBNull(dr["EquipmentNO"]))
                item.EquipmentNO = Convert.ToString(dr["EquipmentNO"]);

            if (!Convert.IsDBNull(dr["EquipmentName"]))
                item.EquipmentName = Convert.ToString(dr["EquipmentName"]);

            if (!Convert.IsDBNull(dr["DepID"]))
                item.DepID = Convert.ToInt64(dr["DepID"]);

            if (!Convert.IsDBNull(dr["DepName"]))
                item.DepName= Convert.ToString(dr["DepName"]);

            if (!Convert.IsDBNull(dr["ScrapReason"]))
                item.ScrapReason = Convert.ToString(dr["ScrapReason"]);

            if (!Convert.IsDBNull(dr["ScrapTime"]))
                item.ScrapTime = Convert.ToDateTime(dr["ScrapTime"]);

            if (!Convert.IsDBNull(dr["SheetNO"]))
                item.SheetNO = Convert.ToString(dr["SheetNO"]);

            return item;
        }

        #region IScrapRecord 成员

        IList IScrapRecord.GetScrapRecordList(long scrapID)
        {
            ArrayList list = new ArrayList();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select ScrapID,DepID,ScrapReason,ScrapTime,EquipmentNO,EquipmentName ");
                strSql.Append(" FROM FM2E_ScrapRecord ");
                strSql.Append(" where ScrapID=@ScrapID");



                SqlParameter[] parameters = {
					new SqlParameter("@ScrapID", SqlDbType.BigInt)};
                parameters[0].Value = scrapID;

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
                throw new DALException("获取设备报废信息失败", e);
            }
            return list;
        }

        IList IScrapRecord.GetScrapRecordList(QueryParam term, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取已审批的设备报废列表失败", e);
            }
        }

        void IScrapRecord.AddscrapRecord(ScrapRecordInfo item)
        {

            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("insert into {0}(", "FM2E_ScrapRecord");
                strSql.Append("ScrapID,EquipmentNO,EquipmentName,DepID,ScrapTime,ScrapReason)");
                strSql.Append(" values (");
                strSql.Append("@ScrapID,@EquipmentNO,@EquipmentName,@DepID,@ScrapTime,@ScrapReason)");
                SqlParameter[] parameters = {
					new SqlParameter("@ScrapID", SqlDbType.BigInt),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
                    new SqlParameter("@EquipmentName", SqlDbType.NVarChar,20),
					new SqlParameter("@DepID", SqlDbType.BigInt),
					new SqlParameter("@ScrapTime", SqlDbType.DateTime),
                    new SqlParameter("@ScrapReason",SqlDbType.NVarChar,50)};

                
                parameters[0].Value = item.ScrapID;
                parameters[1].Value = item.EquipmentNO;
                parameters[2].Value = item.EquipmentName;
                parameters[3].Value = item.DepID;
                parameters[4].Value = DateTime.Now;
                parameters[5].Value = item.ScrapReason;

                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                //同进更新设备表
                StringBuilder strUpdateEquipment = new StringBuilder();
                strUpdateEquipment.Append("update FM2E_Equipment ");
                strUpdateEquipment.Append(" set Status=3,IsCancel=1,UpdateTime=@UpdateTime");
                strUpdateEquipment.Append(" where EquipmentNO=@EquipmentNO");

                SqlParameter[] paramForEquipment ={
                    new SqlParameter("@UpdateTime", SqlDbType.DateTime),
                    new SqlParameter("@EquipmentNO",SqlDbType.VarChar,20)
                                                 };
                paramForEquipment[0].Value = item.ScrapTime;
                paramForEquipment[1].Value = item.EquipmentNO;

                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strUpdateEquipment.ToString(), paramForEquipment);
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new DALException("添加设备报废登记信息失败", e);
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

        QueryParam IScrapRecord.GenerateSearchTerm(ScrapRecordSearchInfo item)
        {
            #region 生成where条件
            string sqlSearch = "where 1=1";
            if (!string.IsNullOrEmpty(item.SheetNO))
            {
                sqlSearch += " and b.SheetName like '%" + item.SheetNO + "%'";
            }

            if (DateTime.Compare(item.ScrapTimeFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(item.ScrapTimeFrom, sqlMinDate) < 0)
                    item.ScrapTimeFrom = sqlMinDate;

                sqlSearch += " and a.ScrapTime>='" + item.ScrapTimeFrom.ToShortDateString() + " 00:00:00'";
            }
            if (DateTime.Compare(item.ScrapTimeTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(item.ScrapTimeTo, sqlMaxDate) > 0)
                    item.ScrapTimeTo = sqlMaxDate;

                sqlSearch += " and a.ScrapTime<='" + item.ScrapTimeTo.ToShortDateString() + " 23:59:59'";
            }

            if (!string.IsNullOrEmpty(item.EquipmentNO))
            {
                sqlSearch += " and a.EquipmentNO like '%" + item.EquipmentNO + "%'";
            }
            
            #endregion
            QueryParam searchTerm = new QueryParam();
            
            searchTerm.TableName = "FM2E_ScrapRecord a LEFT JOIN FM2E_Scrap b ON a.ScrapID=b.ScrapID left join FM2E_Department c on a.DepID=c.DepartmentID";
            searchTerm.ReturnFields="a.EquipmentNO,a.ScrapTime,a.DepID,c.Name as DepName,b.SheetName AS SheetNO,a.EquipmentName,a.ScrapID,a.ScrapReason";
            searchTerm.OrderBy="order by ScrapTime desc";
            searchTerm.Where=sqlSearch;

            return searchTerm;
        }

        #endregion
    }
}
