﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;
using FM2E.IDAL.Equipment;
using FM2E.SQLServerDAL.Utils;

namespace FM2E.SQLServerDAL.Equipment
{
    /// <summary>
    /// 报验数据库访问
    /// </summary>
    public class CheckAcceptance:ICheckAcceptance
    {
        #region 表名
        const string TABLE_CHECK_ACCEPTANCE = "FM2E_CheckAcceptance";
        const string TABLE_CHECK_ACCEPTANCE_DETAIL = "FM2E_CheckAcceptanceDetail";
        const string TABLE_CHECK_ACCEPTANCE_DETAIL_BARCODE = "FM2E_CheckAcceptanceDetailBarcode";
        const string TABLE_WAREHOUSE = "FM2E_WareHouse";
        const string TABLE_USERS = "FM2E_User";
        const string VIEW_CHECK_ACCEPTANCE_SEARCH = "FM2E_CheckAcceptanceSearchView";
        const string VIEW_CHECK_ACCEPTANCE = "FM2E_CheckAcceptanceView";
        const string VIEW_CHECK_ACCEPTANCE_DETAIL = "FM2E_CheckAcceptanceDetailView";
        #endregion

        #region GetData
        private CheckAcceptanceInfo GetDataCheckAcceptanceInfo(IDataReader rd)
        {
            CheckAcceptanceInfo item = new CheckAcceptanceInfo();
            if (!Convert.IsDBNull(rd["ID"]))
            {
                item.ID = Convert.ToInt64(rd["ID"]);
            }
            if (!Convert.IsDBNull(rd["Remark"]))
            {
                item.Remark = Convert.ToString(rd["Remark"]);
            }
            if (!Convert.IsDBNull(rd["SheetID"]))
            {
                item.SheetID = Convert.ToString(rd["SheetID"]);
            }
            if (!Convert.IsDBNull(rd["SheetName"]))
            {
                item.SheetName = Convert.ToString(rd["SheetName"]);
            }
            if (!Convert.IsDBNull(rd["CompanyID"]))
            {
                item.CompanyID = Convert.ToString(rd["CompanyID"]);
            }
            try
            {
                if (!Convert.IsDBNull(rd["CompanyName"]))
                {
                    item.CompanyName = Convert.ToString(rd["CompanyName"]);
                }
            }
            catch { }
            if (!Convert.IsDBNull(rd["Status"]))
            {
                item.Status = (CheckAcceptanceStatus)Enum.Parse(typeof(CheckAcceptanceStatus), Convert.ToInt16(rd["Status"]).ToString());
            }
            if (!Convert.IsDBNull(rd["Applicant"]))
            {
                item.Applicant = Convert.ToString(rd["Applicant"]);
            }
            if (!Convert.IsDBNull(rd["ApplicantName"]))
            {
                item.ApplicantName = Convert.ToString(rd["ApplicantName"]);
            }
                
            if (!Convert.IsDBNull(rd["SubmitTime"]))
            {
                item.SubmitTime = Convert.ToDateTime(rd["SubmitTime"]);
            }
            if (!Convert.IsDBNull(rd["UpdateTime"]))
            {
                item.UpdateTime = Convert.ToDateTime(rd["UpdateTime"]);
            }
            return item;
        }

        private CheckAcceptanceDetailInfo GetDataCheckAcceptanceDetailInfo(IDataReader rd)
        {
            CheckAcceptanceDetailInfo item = new CheckAcceptanceDetailInfo();
            if (!Convert.IsDBNull(rd["ID"]))
            {
                item.ID = Convert.ToInt64(rd["ID"]);
            }
            if (!Convert.IsDBNull(rd["SheetID"]))
            {
                item.SheetID = Convert.ToString(rd["SheetID"]);
            }
            if (!Convert.IsDBNull(rd["ItemID"]))
            {
                item.ItemID = Convert.ToInt16(rd["ItemID"]);
            }
            if (!Convert.IsDBNull(rd["CompanyID"]))
            {
                item.CompanyID = Convert.ToString(rd["CompanyID"]);
            }
            if (!Convert.IsDBNull(rd["ProductName"]))
            {
                item.ProductName = Convert.ToString(rd["ProductName"]);
            }
            if (!Convert.IsDBNull(rd["Model"]))
            {
                item.Model = Convert.ToString(rd["Model"]);
            }

            if (!Convert.IsDBNull(rd["Type"]))
            {
                item.Type = (ItemType)Enum.Parse(typeof(ItemType), Convert.ToInt16(rd["Type"]).ToString());
            }

            if (!Convert.IsDBNull(rd["PurchaseCount"]))
            {
                item.PurchaseCount = Convert.ToDecimal(rd["PurchaseCount"]);
            }
            if (!Convert.IsDBNull(rd["PurchaseUnitPrice"]))
            {
                item.PurchaseUnitPrice = Convert.ToDecimal(rd["PurchaseUnitPrice"]);
            }
            if (!Convert.IsDBNull(rd["Unit"]))
            {
                item.Unit = Convert.ToString(rd["Unit"]);
            }

            if (!Convert.IsDBNull(rd["Producer"]))
            {
                item.Producer = Convert.ToString(rd["Producer"]);
            }
            if (!Convert.IsDBNull(rd["Supplier"]))
            {
                item.Supplier = Convert.ToString(rd["Supplier"]);
            }
            if (!Convert.IsDBNull(rd["PurchaseRemark"]))
            {
                item.PurchaseRemark = Convert.ToString(rd["PurchaseRemark"]);
            }

            if (!Convert.IsDBNull(rd["PurchaseTime"]))
            {
                item.PurchaseTime = Convert.ToDateTime(rd["PurchaseTime"]);
            }
            if (!Convert.IsDBNull(rd["Purchaser"]))
            {
                item.Purchaser = Convert.ToString(rd["Purchaser"]);
            }
            if (!Convert.IsDBNull(rd["PurchaserName"]))
            {
                item.PurchaserName = Convert.ToString(rd["PurchaserName"]);
            }

            if (!Convert.IsDBNull(rd["PurchaserConfirm"]))
            {
                item.PurchaserConfirm = Convert.ToBoolean(rd["PurchaserConfirm"]);
            }

            if (!Convert.IsDBNull(rd["PurchaserConfirmTime"]))
            {
                item.PurchaserConfirmTime = Convert.ToDateTime(rd["PurchaserConfirmTime"]);
            }

            if (!Convert.IsDBNull(rd["WarehouseID"]))
            {
                item.WarehouseID = Convert.ToString(rd["WarehouseID"]);
            }

            if (!Convert.IsDBNull(rd["WarehouseName"]))
            {
                item.WarehouseName = Convert.ToString(rd["WarehouseName"]);
            }

            if (!Convert.IsDBNull(rd["Checker_Warehouse"]))
            {
                item.Checker_Warehouse = Convert.ToString(rd["Checker_Warehouse"]);
            }

            if (!Convert.IsDBNull(rd["WarehouseKeeperName"]))
            {
                item.WarehouseKeeperName = Convert.ToString(rd["WarehouseKeeperName"]);
            }

            if (!Convert.IsDBNull(rd["Checker_Technician"]))
            {
                item.Checker_Technician = Convert.ToString(rd["Checker_Technician"]);
            }
            if (!Convert.IsDBNull(rd["TechnicianName"]))
            {
                item.TechnicianName = Convert.ToString(rd["TechnicianName"]);
            }
            if (!Convert.IsDBNull(rd["AcceptanceCount"]))
            {
                item.AcceptanceCount = Convert.ToDecimal(rd["AcceptanceCount"]);
            }
            if (!Convert.IsDBNull(rd["AcceptanceResult"]))
            {
                item.AcceptanceResult = (PurchaseOrderDetailAcceptanceResult)Enum.Parse(typeof(PurchaseOrderDetailAcceptanceResult), Convert.ToInt16(rd["AcceptanceResult"]).ToString());
            }
            if (!Convert.IsDBNull(rd["AcceptanceRemark"]))
            {
                item.AcceptanceRemark = Convert.ToString(rd["AcceptanceRemark"]);
            }

            if (!Convert.IsDBNull(rd["AcceptedTime"]))
            {
                item.AcceptedTime = Convert.ToDateTime(rd["AcceptedTime"]);
            }
            if (!Convert.IsDBNull(rd["Status"]))
            {
                item.Status = (PurchaseRecordStatus)Enum.Parse(typeof(PurchaseRecordStatus), Convert.ToInt16(rd["Status"]).ToString());
            }
            if (!Convert.IsDBNull(rd["IsDividable"]))
            {
                item.IsDividable = Convert.ToBoolean(rd["IsDividable"]);
            }
            if (!Convert.IsDBNull(rd["SystemName"]))
            {
                item.SystemName = Convert.ToString(rd["SystemName"]);
            }
            if (!Convert.IsDBNull(rd["SystemID"]))
            {
                item.SystemID = Convert.ToString(rd["SystemID"]);
            }
            return item;
        }

        private CheckAcceptanceDetailBarcodeInfo GetDataCheckAcceptanceDetailBarcodeInfo(IDataReader rd)
        {
            CheckAcceptanceDetailBarcodeInfo item = new CheckAcceptanceDetailBarcodeInfo();
            if (!Convert.IsDBNull(rd["ID"]))
            {
                item.ID = Convert.ToInt64(rd["ID"]);
            }

            if (!Convert.IsDBNull(rd["CompanyID"]))
            {
                item.CompanyID = Convert.ToString(rd["CompanyID"]);
            }

            if (!Convert.IsDBNull(rd["FormID"]))
            {
                item.FormID = Convert.ToInt64(rd["FormID"]);
            }
            if (!Convert.IsDBNull(rd["ItemID"]))
            {
                item.ItemID = Convert.ToInt16(rd["ItemID"]);
            }
            if (!Convert.IsDBNull(rd["Barcode"]))
            {
                item.Barcode = Convert.ToString(rd["Barcode"]);
            }
            if (!Convert.IsDBNull(rd["ProductName"]))
            {
                item.ProductName = Convert.ToString(rd["ProductName"]);
            }

            if (!Convert.IsDBNull(rd["Model"]))
            {
                item.Model = Convert.ToString(rd["Model"]);
            }
           
            return item;
        }
        #endregion

        #region Insert
        private long InsertCheckAcceptanceInfo(SqlTransaction trans, CheckAcceptanceInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+TABLE_CHECK_ACCEPTANCE+"(");
            strSql.Append("SheetID,SheetName,CompanyID,Applicant,SubmitTime,Status,Remark,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@SheetID,@SheetName,@CompanyID,@Applicant,@SubmitTime,@Status,@Remark,@UpdateTime)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.VarChar,20),
					new SqlParameter("@SheetName", SqlDbType.NVarChar,20),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Applicant", SqlDbType.VarChar,20),
					new SqlParameter("@SubmitTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.SheetID;
            parameters[1].Value = model.SheetName;
            parameters[2].Value = model.CompanyID;
            parameters[3].Value = model.Applicant;
            parameters[4].Value = model.SubmitTime == DateTime.MinValue ? SqlDateTime.Null : model.SubmitTime;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.UpdateTime == DateTime.MinValue ? SqlDateTime.Null : model.UpdateTime;

            //读取ID
            long id = 1;
            if (trans != null)
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
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

        private void InsertCheckAcceptanceDetailInfo(SqlTransaction trans, CheckAcceptanceDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+TABLE_CHECK_ACCEPTANCE_DETAIL+"(");
            strSql.Append("ID,Unit,Producer,Supplier,PurchaseRemark,PurchaseTime,Purchaser,PurchaserConfirm,PurchaserConfirmTime,WarehouseID,Checker_Warehouse,ItemID,Checker_Technician,AcceptanceCount,AcceptanceResult,AcceptanceRemark,AcceptedTime,Status,IsDividable,SheetID,CompanyID,ProductName,Model,Type,PurchaseCount,PurchaseUnitPrice,SystemID)");
            strSql.Append(" values (");
            strSql.Append("@ID,@Unit,@Producer,@Supplier,@PurchaseRemark,@PurchaseTime,@Purchaser,@PurchaserConfirm,@PurchaserConfirmTime,@WarehouseID,@Checker_Warehouse,@ItemID,@Checker_Technician,@AcceptanceCount,@AcceptanceResult,@AcceptanceRemark,@AcceptedTime,@Status,@IsDividable,@SheetID,@CompanyID,@ProductName,@Model,@Type,@PurchaseCount,@PurchaseUnitPrice,@SystemID)");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@Producer", SqlDbType.NVarChar,50),
					new SqlParameter("@Supplier", SqlDbType.NVarChar,50),
					new SqlParameter("@PurchaseRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@PurchaseTime", SqlDbType.DateTime),
					new SqlParameter("@Purchaser", SqlDbType.VarChar,20),
					new SqlParameter("@PurchaserConfirm", SqlDbType.Bit,1),
					new SqlParameter("@PurchaserConfirmTime", SqlDbType.DateTime),
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@Checker_Warehouse", SqlDbType.VarChar,20),
					new SqlParameter("@ItemID", SqlDbType.TinyInt,1),
					new SqlParameter("@Checker_Technician", SqlDbType.VarChar,20),
					new SqlParameter("@AcceptanceCount", SqlDbType.Decimal,9),
					new SqlParameter("@AcceptanceResult", SqlDbType.TinyInt,1),
					new SqlParameter("@AcceptanceRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@AcceptedTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@IsDividable", SqlDbType.Bit,1),
					new SqlParameter("@SheetID", SqlDbType.VarChar,20),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@Model", SqlDbType.NVarChar,20),
					new SqlParameter("@Type", SqlDbType.TinyInt,1),
					new SqlParameter("@PurchaseCount", SqlDbType.Decimal,9),
					new SqlParameter("@PurchaseUnitPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@SystemID",SqlDbType.VarChar,2)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Unit;
            parameters[2].Value = model.Producer;
            parameters[3].Value = model.Supplier;
            parameters[4].Value = model.PurchaseRemark;
            parameters[5].Value = model.PurchaseTime == DateTime.MinValue ? SqlDateTime.Null : model.PurchaseTime;
            parameters[6].Value = model.Purchaser;
            parameters[7].Value = model.PurchaserConfirm;
            parameters[8].Value = model.PurchaserConfirmTime == DateTime.MinValue ? SqlDateTime.Null : model.PurchaserConfirmTime;
            parameters[9].Value = model.WarehouseID;
            parameters[10].Value = model.Checker_Warehouse;
            parameters[11].Value = model.ItemID;
            parameters[12].Value = model.Checker_Technician;
            parameters[13].Value = model.AcceptanceCount;
            parameters[14].Value = model.AcceptanceResult;
            parameters[15].Value = model.AcceptanceRemark;
            parameters[16].Value = model.AcceptedTime == DateTime.MinValue ? SqlDateTime.Null : model.AcceptedTime;
            parameters[17].Value = model.Status;
            parameters[18].Value = model.IsDividable;
            parameters[19].Value = model.SheetID;
            parameters[20].Value = model.CompanyID;
            parameters[21].Value = model.ProductName;
            parameters[22].Value = model.Model;
            parameters[23].Value = model.Type;
            parameters[24].Value = model.PurchaseCount;
            parameters[25].Value = model.PurchaseUnitPrice;
            parameters[26].Value = (model.SystemID == null || model.SystemID == "") ? SqlString.Null : model.SystemID;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        private long InsertCheckAcceptanceDetailBarcodeInfo(SqlTransaction trans, CheckAcceptanceDetailBarcodeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+TABLE_CHECK_ACCEPTANCE_DETAIL_BARCODE+"(");
            strSql.Append("CompanyID,FormID,ItemID,Barcode,ProductName,Model)");
            strSql.Append(" values (");
            strSql.Append("@CompanyID,@FormID,@ItemID,@Barcode,@ProductName,@Model)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@FormID", SqlDbType.BigInt,8),
					new SqlParameter("@ItemID", SqlDbType.TinyInt,1),
					new SqlParameter("@Barcode", SqlDbType.VarChar,50),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,50),
					new SqlParameter("@Model", SqlDbType.NVarChar,20)};
            parameters[0].Value = model.CompanyID;
            parameters[1].Value = model.FormID;
            parameters[2].Value = model.ItemID;
            parameters[3].Value = model.Barcode;
            parameters[4].Value = model.ProductName;
            parameters[5].Value = model.Model;

            //读取ID
            long id = 1;
            if (trans != null)
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
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
        #endregion

        #region Get
        private CheckAcceptanceInfo GetCheckAcceptanceInfo(SqlConnection conn,long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from " + VIEW_CHECK_ACCEPTANCE + " ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = id;

            CheckAcceptanceInfo item = new CheckAcceptanceInfo();
            if (conn == null)
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        item = GetDataCheckAcceptanceInfo(rd);
                        break;
                    }
                }
            }
            else
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(conn, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        item = GetDataCheckAcceptanceInfo(rd);
                        break;
                    }
                }
            }
            //封装其他相关的信息
            //
            return item;
        }

        private CheckAcceptanceDetailInfo GetCheckAcceptanceDetailInfo(SqlConnection conn,long id,short itemid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from " + VIEW_CHECK_ACCEPTANCE_DETAIL + " ");
            strSql.Append(" where ID=@ID and ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt),
					new SqlParameter("@ItemID", SqlDbType.TinyInt)};
            parameters[0].Value = id;
            parameters[1].Value = itemid;


            CheckAcceptanceDetailInfo item = new CheckAcceptanceDetailInfo();
            if (conn == null)
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        item = GetDataCheckAcceptanceDetailInfo(rd);
                        break;
                    }
                }
            }
            else
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(conn, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        item = GetDataCheckAcceptanceDetailInfo(rd);
                        break;
                    }
                }
            }
            //封装其他相关的信息
            //
            return item;
        }

        private CheckAcceptanceDetailBarcodeInfo GetCheckAcceptanceDetailBarcodeInfo(SqlConnection conn,long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from "+TABLE_CHECK_ACCEPTANCE_DETAIL_BARCODE+" ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = id;


            CheckAcceptanceDetailBarcodeInfo item = new CheckAcceptanceDetailBarcodeInfo();
            if (conn == null)
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        item = GetDataCheckAcceptanceDetailBarcodeInfo(rd);
                        break;
                    }
                }
            }
            else
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(conn, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        item = GetDataCheckAcceptanceDetailBarcodeInfo(rd);
                        break;
                    }
                }
            }
            //封装其他相关的信息
            //
            return item;
        }
        #endregion

        #region List
        private IList GetCheckAcceptanceInfoList(SqlConnection conn)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + VIEW_CHECK_ACCEPTANCE + " ");
            strSql.Append(" order by ID ASC;");//排序

            List<CheckAcceptanceInfo> list = new List<CheckAcceptanceInfo>();
            if (conn == null)
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        CheckAcceptanceInfo item = GetDataCheckAcceptanceInfo(rd);
                        list.Add(item);
                    }
                }
            }
            else
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(conn, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        CheckAcceptanceInfo item = GetDataCheckAcceptanceInfo(rd);
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        private IList GetCheckAcceptanceDetailInfoList(SqlConnection conn,long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + VIEW_CHECK_ACCEPTANCE_DETAIL + " ");
            //strSql.Append(" where ID=@ID ");
            //if (conn == null)
            // 更正 [8/15/2013 Genland]
            if (id >0)
            {
            strSql.Append(" where ID=@ID ");
            }
            else
            {
                strSql.Append(" where 1=1 ");                
            }

            strSql.Append(" order by ID ASC;");//排序
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = id;

            List<CheckAcceptanceDetailInfo> list = new List<CheckAcceptanceDetailInfo>();
            if (conn == null)
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        CheckAcceptanceDetailInfo item = GetDataCheckAcceptanceDetailInfo(rd);
                        list.Add(item);
                    }
                }
            }
            else
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(conn, CommandType.Text, strSql.ToString(), parameters))
                //using (SqlDataReader rd = SQLHelper.ExecuteReader(conn, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        CheckAcceptanceDetailInfo item = GetDataCheckAcceptanceDetailInfo(rd);
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        private IList GetCheckAcceptanceDetailBarcodeInfoList(SqlConnection conn,long formid,short itemid)
        {
            
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + TABLE_CHECK_ACCEPTANCE_DETAIL_BARCODE + " ");
            strSql.Append(" where FormID=@FormID ");
            strSql.Append(" order by ID ASC;");//排序
            SqlParameter[] parameters = {
					new SqlParameter("@FormID", SqlDbType.BigInt),
                    new SqlParameter("@ItemID",SqlDbType.TinyInt)};
            parameters[0].Value = formid;
            parameters[1].Value = itemid;

            List<CheckAcceptanceDetailBarcodeInfo> list = new List<CheckAcceptanceDetailBarcodeInfo>();
            if (conn == null)
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        CheckAcceptanceDetailBarcodeInfo item = GetDataCheckAcceptanceDetailBarcodeInfo(rd);
                        list.Add(item);
                    }
                }
            }
            else
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(conn, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        CheckAcceptanceDetailBarcodeInfo item = GetDataCheckAcceptanceDetailBarcodeInfo(rd);
                        list.Add(item);
                    }
                }
            }
            return list;
        }
        #endregion

        #region Update
        private void UpdateCheckAcceptanceInfo(SqlTransaction trans, CheckAcceptanceInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update "+TABLE_CHECK_ACCEPTANCE+" set ");
            strSql.Append("SheetID=@SheetID,");
            strSql.Append("SheetName=@SheetName,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("Applicant=@Applicant,");
            strSql.Append("SubmitTime=@SubmitTime,");
            strSql.Append("Status=@Status,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@SheetID", SqlDbType.VarChar,20),
					new SqlParameter("@SheetName", SqlDbType.NVarChar,20),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Applicant", SqlDbType.VarChar,20),
					new SqlParameter("@SubmitTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.SheetID;
            parameters[2].Value = model.SheetName;
            parameters[3].Value = model.CompanyID;
            parameters[4].Value = model.Applicant;
            parameters[5].Value = model.SubmitTime == DateTime.MinValue ? SqlDateTime.Null : model.SubmitTime;
            parameters[6].Value = model.Status;
            parameters[7].Value = model.Remark;
            parameters[8].Value = model.UpdateTime == DateTime.MinValue ? SqlDateTime.Null : model.UpdateTime;
            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        private void UpdateCheckAcceptanceDetailInfo(SqlTransaction trans, CheckAcceptanceDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update "+TABLE_CHECK_ACCEPTANCE_DETAIL+" set ");
            strSql.Append("Unit=@Unit,");
            strSql.Append("Producer=@Producer,");
            strSql.Append("Supplier=@Supplier,");
            strSql.Append("PurchaseRemark=@PurchaseRemark,");
            strSql.Append("PurchaseTime=@PurchaseTime,");
            strSql.Append("Purchaser=@Purchaser,");
            strSql.Append("PurchaserConfirm=@PurchaserConfirm,");
            strSql.Append("PurchaserConfirmTime=@PurchaserConfirmTime,");
            strSql.Append("WarehouseID=@WarehouseID,");
            strSql.Append("Checker_Warehouse=@Checker_Warehouse,");
            strSql.Append("Checker_Technician=@Checker_Technician,");
            strSql.Append("AcceptanceCount=@AcceptanceCount,");
            strSql.Append("AcceptanceResult=@AcceptanceResult,");
            strSql.Append("AcceptanceRemark=@AcceptanceRemark,");
            strSql.Append("AcceptedTime=@AcceptedTime,");
            strSql.Append("Status=@Status,");
            strSql.Append("IsDividable=@IsDividable,");
            strSql.Append("SheetID=@SheetID,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("ProductName=@ProductName,");
            strSql.Append("Model=@Model,");
            strSql.Append("Type=@Type,");
            strSql.Append("PurchaseCount=@PurchaseCount,");
            strSql.Append("PurchaseUnitPrice=@PurchaseUnitPrice,");
            strSql.Append("SystemID=@SystemID");
            strSql.Append(" where ID=@ID and ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@Producer", SqlDbType.NVarChar,50),
					new SqlParameter("@Supplier", SqlDbType.NVarChar,50),
					new SqlParameter("@PurchaseRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@PurchaseTime", SqlDbType.DateTime),
					new SqlParameter("@Purchaser", SqlDbType.VarChar,20),
					new SqlParameter("@PurchaserConfirm", SqlDbType.Bit,1),
					new SqlParameter("@PurchaserConfirmTime", SqlDbType.DateTime),
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@Checker_Warehouse", SqlDbType.VarChar,20),
					new SqlParameter("@ItemID", SqlDbType.TinyInt,1),
					new SqlParameter("@Checker_Technician", SqlDbType.VarChar,20),
					new SqlParameter("@AcceptanceCount", SqlDbType.Decimal,9),
					new SqlParameter("@AcceptanceResult", SqlDbType.TinyInt,1),
					new SqlParameter("@AcceptanceRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@AcceptedTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@IsDividable", SqlDbType.Bit,1),
					new SqlParameter("@SheetID", SqlDbType.VarChar,20),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@Model", SqlDbType.NVarChar,20),
					new SqlParameter("@Type", SqlDbType.TinyInt,1),
					new SqlParameter("@PurchaseCount", SqlDbType.Decimal,9),
					new SqlParameter("@PurchaseUnitPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@SystemID",SqlDbType.VarChar,2)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Unit;
            parameters[2].Value = model.Producer;
            parameters[3].Value = model.Supplier;
            parameters[4].Value = model.PurchaseRemark;
            parameters[5].Value = model.PurchaseTime == DateTime.MinValue ? SqlDateTime.Null : model.PurchaseTime;
            parameters[6].Value = model.Purchaser;
            parameters[7].Value = model.PurchaserConfirm;
            parameters[8].Value = model.PurchaserConfirmTime == DateTime.MinValue ? SqlDateTime.Null : model.PurchaserConfirmTime;
            parameters[9].Value = model.WarehouseID;
            parameters[10].Value = model.Checker_Warehouse;
            parameters[11].Value = model.ItemID;
            parameters[12].Value = model.Checker_Technician;
            parameters[13].Value = model.AcceptanceCount;
            parameters[14].Value = model.AcceptanceResult;
            parameters[15].Value = model.AcceptanceRemark;
            parameters[16].Value = model.AcceptedTime == DateTime.MinValue ? SqlDateTime.Null : model.AcceptedTime;
            parameters[17].Value = model.Status;
            parameters[18].Value = model.IsDividable;
            parameters[19].Value = model.SheetID;
            parameters[20].Value = model.CompanyID;
            parameters[21].Value = model.ProductName;
            parameters[22].Value = model.Model;
            parameters[23].Value = model.Type;
            parameters[24].Value = model.PurchaseCount;
            parameters[25].Value = model.PurchaseUnitPrice;
            parameters[26].Value = string.IsNullOrEmpty(model.SystemID) ? SqlString.Null : model.SystemID;
            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        private void UpdateCheckAcceptanceDetailBarcodeInfo(SqlTransaction trans, CheckAcceptanceDetailBarcodeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update "+TABLE_CHECK_ACCEPTANCE_DETAIL_BARCODE+" set ");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("FormID=@FormID,");
            strSql.Append("ItemID=@ItemID,");
            strSql.Append("Barcode=@Barcode,");
            strSql.Append("ProductName=@ProductName,");
            strSql.Append("Model=@Model");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@FormID", SqlDbType.BigInt,8),
					new SqlParameter("@ItemID", SqlDbType.TinyInt,1),
					new SqlParameter("@Barcode", SqlDbType.VarChar,50),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,50),
					new SqlParameter("@Model", SqlDbType.NVarChar,20)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.CompanyID;
            parameters[2].Value = model.FormID;
            parameters[3].Value = model.ItemID;
            parameters[4].Value = model.Barcode;
            parameters[5].Value = model.ProductName;
            parameters[6].Value = model.Model;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        #endregion

        #region Delete
        private void DeleteCheckAcceptanceInfo(SqlTransaction trans, long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete "+TABLE_CHECK_ACCEPTANCE+" ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = id;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        private void DeleteCheckAcceptanceDetailInfo(SqlTransaction trans,long id,short itemid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete "+TABLE_CHECK_ACCEPTANCE_DETAIL+" ");
            strSql.Append(" where ID=@ID and ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt),
					new SqlParameter("@ItemID", SqlDbType.TinyInt)};
            parameters[0].Value = id;
            parameters[1].Value = itemid;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        private void DeleteCheckAcceptanceDetailBarcodeInfo(SqlTransaction trans, long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete "+TABLE_CHECK_ACCEPTANCE_DETAIL_BARCODE+" ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = id;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        #endregion

        #region Search
        private QueryParam GenerateSearchInfo(CheckAcceptanceSearchInfo item)
        {

            QueryParam q = new QueryParam();

            q.TableName = VIEW_CHECK_ACCEPTANCE_SEARCH;

            string sqlSearch = "where 1=1";
            string empty = Guid.Empty.ToString("N");

       
            if (item.CompanyID != null && item.CompanyID != "")
            {
                sqlSearch += " and CompanyID = '" + item.CompanyID + "'";
            }

            if (item.ApplicantID != null && item.ApplicantID != "")
            {
                sqlSearch += " and Applicant = '" + item.ApplicantID + "'";
            }

            if (item.SheetID != null && item.SheetID != "")
            {
                sqlSearch += " and SheetID = '" + item.SheetID + "'";
            }

            if (item.ApplicantName != null && item.ApplicantName != "")
            {
                sqlSearch += " and ApplicantName like '%" + item.ApplicantName + "%'";
            }

            if (item.ProductName != null && item.ProductName != "")
            {
                sqlSearch += " and ProductName like '%" + item.ProductName + "%'";
            }

            if (item.Model != null && item.Model != "")
            {
                sqlSearch += " and Model like '%" + item.Model + "%'";
            }

            if (item.SheetName != null && item.SheetName != "")
            {
                sqlSearch += " and SheetName like '%" + item.SheetName + "%'";
            }

            if (item.PurchaserID != null && item.PurchaserID != "")
            {
                sqlSearch += " and Purchaser = '" + item.PurchaserID + "'";
            }

            if (item.PurchaserName != null && item.PurchaserName != "")
            {
                sqlSearch += " and PurchaserName like '%" + item.PurchaserName + "%'";
            }

            if (item.WareHouseID != null && item.WareHouseID != "")
            {
                sqlSearch += " and WarehouseID = '" + item.WareHouseID + "'";
            }
            if (item.PurchaserConfirm != null)
            {
                sqlSearch += " and PurchaserConfirm = " + ((bool)item.PurchaserConfirm ? 1 : 0) + "";
            }
            if (item.StatusList != null && item.StatusList.Count > 0)
            {
                for (int i = 0; i < item.StatusList.Count; i++)
                {
                    if (i == 0)
                    {
                        sqlSearch += " and ( ";
                        sqlSearch += " " + " Status=" + (int)item.StatusList[i] + " ";
                    }
                    else
                    {
                        sqlSearch += " or " + " Status=" + (int)item.StatusList[i] + " ";
                    }
                    if (i == item.StatusList.Count - 1)
                    {
                        sqlSearch += " ) ";
                    }
                }
            }
            


            if (item.DetailStatusList != null && item.DetailStatusList.Count > 0)
            {
                for (int i = 0; i < item.DetailStatusList.Count; i++)
                {
                    if (i == 0)
                    {
                        sqlSearch += " and ( ";
                        sqlSearch += " " + " DetailStatus=" + (int)item.DetailStatusList[i] + " ";
                    }
                    else
                    {
                        sqlSearch += " or " + " DetailStatus=" + (int)item.DetailStatusList[i] + " ";
                    }
                    if (i == item.DetailStatusList.Count - 1)
                    {
                        sqlSearch += " ) ";
                    }
                }
            }

            q.ReturnFields = "*";

            q.OrderBy = "order by UpdateTime desc";

            q.Where = sqlSearch;

            return q;
        }

        private IList SearchCheckAcceptanceInfo(QueryParam qp, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectListWithDistinct(this.GetDataCheckAcceptanceInfo, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取报验单分页查询失败", e);
            }
        }
        #endregion


        #region ICheckAcceptance 成员

        long ICheckAcceptance.InsertCheckAcceptanceWithDetail(CheckAcceptanceInfo item)
        {

            SqlConnection conn = null;
            SqlTransaction trans = null;
            long id = 0;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先插入消息
                id= InsertCheckAcceptanceInfo(trans, item);

                if (item.DetailList != null)
                {
                    foreach (CheckAcceptanceDetailInfo detail in item.DetailList)
                    {
                        detail.ID = id;
                        InsertCheckAcceptanceDetailInfo(trans, detail);
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
            return id;

        }

        void ICheckAcceptance.DeleteCheckAcceptanceInfo(long id)
        {
            DeleteCheckAcceptanceInfo(null, id);
        }

        CheckAcceptanceInfo ICheckAcceptance.GetCheckAcceptanceInfoWithAllDetail(long id)
        {
            SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString);
            CheckAcceptanceInfo form = new CheckAcceptanceInfo();
            try
            {
                form = GetCheckAcceptanceInfo(conn, id);
                form.DetailList = GetCheckAcceptanceDetailInfoList(conn, id);
                foreach (CheckAcceptanceDetailInfo detail in form.DetailList)
                {
                    detail.BarcodeList = GetCheckAcceptanceDetailBarcodeInfoList(conn, detail.ID, detail.ItemID);
                }
            }
            catch
            {
                if (conn != null)
                    conn.Close();
                throw;
            }
            return form;
        }

        void ICheckAcceptance.UpdateCheckAcceptanceNoDetail(CheckAcceptanceInfo item)
        {
            UpdateCheckAcceptanceInfo(null, item);
        }

        void ICheckAcceptance.UpdateCheckAcceptanceWithDetail(CheckAcceptanceInfo item)
        {
            throw new NotImplementedException();
        }

        void ICheckAcceptance.UpdateCheckAcceptanceDetail(CheckAcceptanceDetailInfo item)
        {
            UpdateCheckAcceptanceDetailInfo(null,item);
        }

        void ICheckAcceptance.InsertBarcodeRecord(CheckAcceptanceDetailBarcodeInfo item)
        {
            InsertCheckAcceptanceDetailBarcodeInfo(null, item);
        }

        IList ICheckAcceptance.SearchCheckAcceptanceForm(QueryParam p,out int recordCount)
        {
            return SearchCheckAcceptanceInfo(p, out recordCount);
        }

        QueryParam ICheckAcceptance.GenerateQueryItem(CheckAcceptanceSearchInfo info)
        {
            return GenerateSearchInfo(info);
        }

        /// <summary>
        /// 不含有详情
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        long ICheckAcceptance.InsertCheckAcceptanceWithoutDetail(CheckAcceptanceInfo item)
        {
            return InsertCheckAcceptanceInfo(null, item);
        }


        CheckAcceptanceDetailInfo ICheckAcceptance.GetCheckAcceptanceDetailInfo(long id, short itemid)
        {
            CheckAcceptanceDetailInfo detail = null;
            
            SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString);
            try
            {
                detail = GetCheckAcceptanceDetailInfo(conn, id, itemid);
                detail.BarcodeList = GetCheckAcceptanceDetailBarcodeInfoList(conn, id, itemid);
            }
            catch
            {
                if (conn != null)
                    conn.Close();
                throw;
            }
            return detail;
        }
        #endregion
    }
}
