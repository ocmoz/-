using System;
using System.Collections.Generic;
using System.Text;

using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;
using FM2E.IDAL.Equipment;
using System.Collections;
using System.Data;
using FM2E.SQLServerDAL.Utils;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace FM2E.SQLServerDAL.Equipment
{
    public class BorrowApply : IBorrowApply
    {
        private const string TABLENAME_BORROWAPPLY = "FM2E_BorrowApply";
        private const string TABLENAME_BORROWAPPLYDETAIL = "FM2E_BorrowApplyDetail";
        private const string TABLENAME_BORROWAPPROVAL = "FM2E_BorrowApproval";

        private const string VIEW_BORROWAPPLY_DETAIL = "FM2E_BorrowApplyDetailView";
        private const string VIEW_BORROW_APPLY = "FM2E_BorrowApplyView";
        private const string VIEW_BORROW_APPROVAL = "FM2E_BorrowApprovalView";
        /// <summary>
        /// 从DataReader中读取数据
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private BorrowApplyInfo GetData(IDataReader dr)
        {
            BorrowApplyInfo item = new BorrowApplyInfo();

            if (!Convert.IsDBNull(dr["BorrowApplyID"]))
                item.BorrowApplyID = Convert.ToInt64(dr["BorrowApplyID"]);

            if (!Convert.IsDBNull(dr["SheetName"]))
                item.SheetName = Convert.ToString(dr["SheetName"]);

            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);

            if (!Convert.IsDBNull(dr["CompanyName"]))
                item.CompanyName = Convert.ToString(dr["CompanyName"]);

            if (!Convert.IsDBNull(dr["BorrowCompanyID"]))
                item.BorrowCompanyID = Convert.ToString(dr["BorrowCompanyID"]);

            if (!Convert.IsDBNull(dr["BorrowCompanyName"]))
                item.BorrowCompanyName = Convert.ToString(dr["BorrowCompanyName"]);

            if (!Convert.IsDBNull(dr["Applicant"]))
                item.Applicant = Convert.ToString(dr["Applicant"]);


            if (!Convert.IsDBNull(dr["ApplicantName"]))
                item.ApplicantName = Convert.ToString(dr["ApplicantName"]);


            if (!Convert.IsDBNull(dr["Status"]))
                item.Status = (BorrowApplyStatus)Convert.ToInt32(dr["Status"]);

            if (!Convert.IsDBNull(dr["SubmitTime"]))
                item.SubmitTime = Convert.ToDateTime(dr["SubmitTime"]);

            return item;
        }

        /// <summary>
        /// 读取明细数据
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private BorrowApplyDetailInfo GetDetailData(IDataReader dr)
        {
            BorrowApplyDetailInfo item = new BorrowApplyDetailInfo();

            if (!Convert.IsDBNull(dr["BorrowApplyID"]))
                item.BorrowApplyID = Convert.ToInt64(dr["BorrowApplyID"]);

            if (!Convert.IsDBNull(dr["ItemID"]))
                item.ItemID = Convert.ToInt64(dr["ItemID"]);

            if (!Convert.IsDBNull(dr["EquipmentName"]))
                item.EquipmentName = Convert.ToString(dr["EquipmentName"]);

            if (!Convert.IsDBNull(dr["Model"]))
                item.Model = Convert.ToString(dr["Model"]);

            if (!Convert.IsDBNull(dr["Count"]))
                item.Count = Convert.ToInt32(dr["Count"]);

            if (!Convert.IsDBNull(dr["Unit"]))
                item.Unit = Convert.ToString(dr["Unit"]);

            if (!Convert.IsDBNull(dr["ReturnDate"]))
                item.ReturnDate = Convert.ToDateTime(dr["ReturnDate"]);

            if (!Convert.IsDBNull(dr["Unit"]))
                item.Unit = Convert.ToString(dr["Unit"]);

            if (!Convert.IsDBNull(dr["AddressID"]))
                item.AddressID = Convert.ToInt64(dr["AddressID"]);

            if (!Convert.IsDBNull(dr["AddressName"]))
                item.AddressName = Convert.ToString(dr["AddressName"]);

            if (!Convert.IsDBNull(dr["AddressCode"]))
                item.AddressCode = Convert.ToString(dr["AddressCode"]);

            if (!Convert.IsDBNull(dr["DetailLocation"]))
                item.DetailLocation = Convert.ToString(dr["DetailLocation"]);
            return item;
        }
        /// <summary>
        /// 读取审批历史数据
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private BorrowApprovaFacadelInfo GetApprovalHistoryData(IDataReader dr)
        {
            BorrowApprovaFacadelInfo item = new BorrowApprovaFacadelInfo();

            if (!Convert.IsDBNull(dr["BorrowApplyID"]))
                item.BorrowApplyID = Convert.ToInt64(dr["BorrowApplyID"]);

            if (!Convert.IsDBNull(dr["SheetName"]))
                item.SheetNO = Convert.ToString(dr["SheetName"]);

            if (!Convert.IsDBNull(dr["BorrowCompanyID"]))
                item.BorrowCompanyID = Convert.ToString(dr["BorrowCompanyID"]);

            if (!Convert.IsDBNull(dr["BorrowCompanyName"]))
                item.BorrowCompanyName = Convert.ToString(dr["BorrowCompanyName"]);

            if (!Convert.IsDBNull(dr["Applicant"]))
                item.Applicant = Convert.ToString(dr["Applicant"]);

            if (!Convert.IsDBNull(dr["ApplicantName"]))
                item.ApplicantName = Convert.ToString(dr["ApplicantName"]);

            if (!Convert.IsDBNull(dr["SubmitTime"]))
                item.ApplyDate = Convert.ToDateTime(dr["SubmitTime"]);

            if (!Convert.IsDBNull(dr["Status"]))
                item.Status = (BorrowApplyStatus)Convert.ToInt32(dr["Status"]);

            if (!Convert.IsDBNull(dr["Approvaler"]))
                item.Approvaler = Convert.ToString(dr["Approvaler"]);

            if (!Convert.IsDBNull(dr["ApprovalerName"]))
                item.ApprovalerName = Convert.ToString(dr["ApprovalerName"]);

            if (!Convert.IsDBNull(dr["ApprovalResult"]))
                item.ApprovalResult = Convert.ToInt32(dr["ApprovalResult"]);

            if (!Convert.IsDBNull(dr["ApprovalDate"]))
                item.ApprovalDate = Convert.ToDateTime(dr["ApprovalDate"]);

            return item;
        }


        #region IBorrowApply 成员

        /// <summary>
        /// 获取借调申请列表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList IBorrowApply.GetBorrowApplyList(QueryParam term, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取设备借调申请列表失败"+e.Message, e);
            }
        }
        /// <summary>
        /// 获取审批历史记录
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList IBorrowApply.GetBorrowApprovalHistory(QueryParam term, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetApprovalHistoryData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取审批历史记录", e);
            }
        }
        /// <summary>
        /// 获取特定借调申请的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BorrowApplyInfo IBorrowApply.GetBorrowApply(long id)
        {
            BorrowApplyInfo model = null;
            SqlConnection conn = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  * ");
                strSql.Append(" from "+VIEW_BORROW_APPLY);
                strSql.Append(" where BorrowApplyID=@BorrowApplyID ");
                SqlParameter[] parameters = {
					new SqlParameter("@BorrowApplyID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                using (SqlDataReader rd = SQLHelper.ExecuteReader(conn, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        model = GetData(rd);
                    }
                }

                if (model == null) return null;

                //获取借调设备列表
                StringBuilder strDetailSql = new StringBuilder();
                strDetailSql.Append("select * from "+VIEW_BORROWAPPLY_DETAIL+" ");
                strDetailSql.Append(" where BorrowApplyID=@BorrowApplyID order by ItemID asc");

                ArrayList list = new ArrayList();
                using (SqlDataReader rd = SQLHelper.ExecuteReader(conn, CommandType.Text, strDetailSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        BorrowApplyDetailInfo item = GetDetailData(rd);
                        list.Add(item);
                    }
                }
                model.DetailList = list;

                //获取审批历史
                StringBuilder strApprovalSql = new StringBuilder();
                strApprovalSql.Append("select * from " + VIEW_BORROW_APPROVAL);
                strApprovalSql.Append(" where BorrowApplyID=@BorrowApplyID order by ApprovalDate asc");

                ArrayList approvalList = new ArrayList();
                using (SqlDataReader rd = SQLHelper.ExecuteReader(conn, CommandType.Text, strApprovalSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        BorrowApprovalInfo item = new BorrowApprovalInfo();

                        if (!Convert.IsDBNull(rd["BorrowApplyID"]))
                            item.BorrowApplyID = Convert.ToInt64(rd["BorrowApplyID"]);


                        if (!Convert.IsDBNull(rd["Approvaler"]))
                            item.Approvaler = Convert.ToString(rd["Approvaler"]);

                        if (!Convert.IsDBNull(rd["ApprovalerName"]))
                            item.ApprovalerName = Convert.ToString(rd["ApprovalerName"]);

                        if (!Convert.IsDBNull(rd["Result"]))
                            item.Result = Convert.ToBoolean(rd["Result"]);

                        if (!Convert.IsDBNull(rd["FeeBack"]))
                            item.FeeBack = Convert.ToString(rd["FeeBack"]);

                        if (!Convert.IsDBNull(rd["ApprovalDate"]))
                            item.ApprovalDate = Convert.ToDateTime(rd["ApprovalDate"]);

                        approvalList.Add(item);
                    }
                }
                model.ApprovalList = approvalList;
            }
            catch (Exception ex)
            {
                model = null;
                throw new DALException("获取设备借调申请单失败"+ex.Message, ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn = null;
                }
            }
            return model;
        }
        /// <summary>
        /// 添加借调申请
        /// </summary>
        /// <param name="model"></param>
        long IBorrowApply.AddBorrowApply(BorrowApplyInfo model)
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

                //先插入申请单基本信息
                id = InsertBorrowApply(trans, model);

               

                int i = 0;
                foreach (BorrowApplyDetailInfo item in model.DetailList)
                {
                    item.ItemID = ++i;
                    item.BorrowApplyID = id;
                    InsertDetailItem(trans, item);

                }
                //再插入明细信息
                trans.Commit();
               
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new DALException("添加设备借调申请失败", e);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn = null;
                }
            } 
            return id;
        }

        private void InsertDetailItem(SqlTransaction trans, BorrowApplyDetailInfo item)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("insert into {0}(", TABLENAME_BORROWAPPLYDETAIL);
            strSql.Append("BorrowApplyID,ItemID,EquipmentName,Model,Count,Unit,ReturnDate,Reason,AddressID,DetailLocation)");
            strSql.Append(" values (");
            strSql.Append("@BorrowApplyID,@ItemID,@EquipmentName,@Model,@Count,@Unit,@ReturnDate,@Reason,@AddressID,@DetailLocation)");
            SqlParameter[] parameters = {
					new SqlParameter("@BorrowApplyID", SqlDbType.BigInt,8),
                    new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@EquipmentName", SqlDbType.NVarChar,20),
					new SqlParameter("@Model", SqlDbType.VarChar,20),
					new SqlParameter("@Count", SqlDbType.Int,4),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@ReturnDate", SqlDbType.DateTime),
					new SqlParameter("@Reason", SqlDbType.NVarChar,50),
                    new SqlParameter("@AddressID",SqlDbType.BigInt),
                    new SqlParameter("@DetailLocation",SqlDbType.NVarChar,20)};
            parameters[0].Value = item.BorrowApplyID;
            parameters[1].Value = item.ItemID;
            parameters[2].Value = string.IsNullOrEmpty(item.EquipmentName) ? SqlString.Null : item.EquipmentName;
            parameters[3].Value = string.IsNullOrEmpty(item.Model) ? SqlString.Null : item.EquipmentName;
            parameters[4].Value = item.Count;
            parameters[5].Value = string.IsNullOrEmpty(item.Unit) ? SqlString.Null : item.Unit;
            parameters[6].Value = item.ReturnDate == DateTime.MinValue ? SqlDateTime.Null : item.ReturnDate;
            parameters[7].Value = string.IsNullOrEmpty(item.Reason) ? SqlString.Null : item.Reason;
            parameters[8].Value = item.AddressID == 0 ? SqlInt64.Null : item.AddressID;
            parameters[9].Value = string.IsNullOrEmpty(item.DetailLocation) ? SqlString.Null : item.DetailLocation;
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 插入申请的基本信息
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="model"></param>
        /// <returns>返回申请单的id</returns>
        private long InsertBorrowApply(SqlTransaction trans, BorrowApplyInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("insert into {0}(", TABLENAME_BORROWAPPLY);
            strSql.Append("SheetName,CompanyID,BorrowCompanyID,Applicant,Status,SubmitTime)");
            strSql.Append(" values (");
            strSql.Append("@SheetName,@CompanyID,@BorrowCompanyID,@Applicant,@Status,@SubmitTime)");
            strSql.Append(";select cast(@@IDENTITY as BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@SheetName", SqlDbType.NVarChar,20),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@BorrowCompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Applicant", SqlDbType.VarChar,20),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@SubmitTime", SqlDbType.DateTime)};
            parameters[0].Value = model.SheetName;
            parameters[1].Value = model.CompanyID;
            parameters[2].Value = model.BorrowCompanyID;
            parameters[3].Value = model.Applicant;
            parameters[4].Value = model.Status;
            parameters[5].Value = model.SubmitTime;

            long id = 1;
            id = (long)SQLHelper.ExecuteScalar(trans, CommandType.Text, strSql.ToString(), parameters);

            return id;
        }
        /// <summary>
        /// 更新借调申请
        /// </summary>
        /// <param name="model"></param>
        void IBorrowApply.UpdateBorrowApply(BorrowApplyInfo model)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                UpdateBorrowApply(trans, model);
                UpdateBorrowApplyDetail(trans, model);

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new DALException("更新设备借调申请失败", e);
            }
        }
        /// <summary>
        /// 更新借调申请的基本信息
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="model"></param>
        private void UpdateBorrowApply(SqlTransaction trans, BorrowApplyInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("update {0} set ", TABLENAME_BORROWAPPLY);
            strSql.Append("SheetName=@SheetName,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("BorrowCompanyID=@BorrowCompanyID,");
            strSql.Append("Applicant=@Applicant,");
            strSql.Append("Status=@Status,");
            strSql.Append("SubmitTime=@SubmitTime");
            strSql.Append(" where BorrowApplyID=@BorrowApplyID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BorrowApplyID", SqlDbType.BigInt,8),
					new SqlParameter("@SheetName", SqlDbType.NVarChar,20),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@BorrowCompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Applicant", SqlDbType.VarChar,20),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@SubmitTime", SqlDbType.DateTime)};
            parameters[0].Value = model.BorrowApplyID;
            parameters[1].Value = model.SheetName;
            parameters[2].Value = model.CompanyID;
            parameters[3].Value = model.BorrowCompanyID;
            parameters[4].Value = model.Applicant;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.SubmitTime;

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新借调申请的明细信息
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="model"></param>
        private void UpdateBorrowApplyDetail(SqlTransaction trans, BorrowApplyInfo model)
        {
            //先删除原来的明细，后添加新的明细
            StringBuilder delSql = new StringBuilder();
            delSql.AppendFormat("delete {0} ", TABLENAME_BORROWAPPLYDETAIL);
            delSql.Append(" where BorrowApplyID=@BorrowApplyID ");
            SqlParameter[] delPara = {
					new SqlParameter("@BorrowApplyID", SqlDbType.BigInt)};
            delPara[0].Value = model.BorrowApplyID;
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, delSql.ToString(), delPara);

            //添加新的明细
            

            int i = 0;
            foreach (BorrowApplyDetailInfo item in model.DetailList)
            {
                item.BorrowApplyID = model.BorrowApplyID;
                item.ItemID = ++i;
                InsertDetailItem(trans, item);
            }
        }
        /// <summary>
        /// 删除相应的借调申请
        /// </summary>
        /// <param name="id"></param>
        void IBorrowApply.DeleteBorrowApply(long id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("delete {0} ", TABLENAME_BORROWAPPLY);
                strSql.Append(" where BorrowApplyID=@BorrowApplyID ");
                SqlParameter[] parameters = {
					new SqlParameter("@BorrowApplyID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除设备借调申请失败", e);
            }
        }
        /// <summary>
        /// 审批借调申请
        /// </summary>
        /// <param name="model"></param>
        void IBorrowApply.ApprovalBorrowApply(BorrowApprovalInfo model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("insert into {0}(", TABLENAME_BORROWAPPROVAL);
                strSql.Append("BorrowApplyID,Approvaler,Result,FeeBack,ApprovalDate)");
                strSql.Append(" values (");
                strSql.Append("@BorrowApplyID,@Approvaler,@Result,@FeeBack,@ApprovalDate)");
                SqlParameter[] parameters = {
					new SqlParameter("@BorrowApplyID", SqlDbType.BigInt,8),
					new SqlParameter("@Approvaler", SqlDbType.VarChar,20),
					new SqlParameter("@Result", SqlDbType.Bit,1),
					new SqlParameter("@FeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@ApprovalDate", SqlDbType.DateTime)};
                parameters[0].Value = model.BorrowApplyID;
                parameters[1].Value = model.Approvaler;
                parameters[2].Value = model.Result;
                parameters[3].Value = model.FeeBack;
                parameters[4].Value = model.ApprovalDate;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("添加审批信息失败", e);
            }
        }
        /// <summary>
        /// 改变申请单状态值
        /// </summary>
        /// <param name="borrowApplyID">申请单号</param>
        /// <param name="status">状态值</param>
        void IBorrowApply.ChangeStatus(long borrowApplyID, BorrowApplyStatus status)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_BorrowApply set Status=@Status ");
                strSql.Append("where BorrowApplyID=@BorrowApplyID");
                SqlParameter[] parameters = {
					new SqlParameter("@BorrowApplyID", SqlDbType.BigInt,8),
					new SqlParameter("@Status", SqlDbType.TinyInt,1)};
                parameters[0].Value = borrowApplyID;
                parameters[1].Value = status;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("改变申请单状态值失败", e);
            }
        }
        /// <summary>
        /// 生成借调申请单查询条件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam IBorrowApply.GenerateSearchTerm(BorrowApplySearchInfo item)
        {
            string sqlSearch = "where 1=1";
            if (!string.IsNullOrEmpty(item.CompanyID)&&item.CompanyID != "0")
                sqlSearch += " and CompanyID ='" + item.CompanyID + "'";

            if (item.SheetName != null && item.SheetName.Trim() != string.Empty)
                sqlSearch += " and SheetName = '" + item.SheetName + "'";

            if (!string.IsNullOrEmpty(item.BorrowCompanyID)&&item.BorrowCompanyID != "0")
            {
                sqlSearch += " and BorrowCompanyID='" + item.BorrowCompanyID + "'";
            }
            if (item.Status != 0)
                sqlSearch += " and Status =" + (int)item.Status;

            if (!string.IsNullOrEmpty(item.Applicant))
            {
                sqlSearch += " and Applicant='" + item.Applicant + "'";
            }

            if (!string.IsNullOrEmpty(item.ApplicantName))
            {
                sqlSearch += " and ApplicantName like '%" + item.ApplicantName + "%'";
            }

            if (DateTime.Compare(item.SubmitTimeFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(item.SubmitTimeFrom, sqlMinDate) < 0)
                    item.SubmitTimeFrom = sqlMinDate;

                sqlSearch += " and SubmitTime>='" + item.SubmitTimeFrom.ToString("yyyy-MM-dd") + " 00:00:00'";
            }

            if (DateTime.Compare(item.SubmitTimeTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(item.SubmitTimeTo, sqlMaxDate) > 0)
                    item.SubmitTimeTo = sqlMaxDate;

                sqlSearch += " and SubmitTime<='" + item.SubmitTimeTo.ToString("yyyy-MM-dd") + " 23:59:59'";
            }

            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = VIEW_BORROW_APPLY;
            searchTerm.ReturnFields = "*";
            searchTerm.OrderBy = "order by SubmitTime desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        /// <summary>
        /// 生成借调申请单查询条件(用于工作流)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam IBorrowApply.GenerateSearchTerm(BorrowApplySearchInfo item, string[] WFStates)
        {
            string sqlSearch = "where 1=1";
            if (!string.IsNullOrEmpty(item.CompanyID) && item.CompanyID != "0")
                sqlSearch += " and CompanyID ='" + item.CompanyID + "'";

            if (item.SheetName != null && item.SheetName.Trim() != string.Empty)
                sqlSearch += " and SheetName = '" + item.SheetName + "'";

            if (!string.IsNullOrEmpty(item.BorrowCompanyID) && item.BorrowCompanyID != "0")
            {
                sqlSearch += " and BorrowCompanyID='" + item.BorrowCompanyID + "'";
            }
            if (item.Status != 0)
                sqlSearch += " and Status =" + (int)item.Status;

            if (!string.IsNullOrEmpty(item.Applicant))
            {
                sqlSearch += " and Applicant='" + item.Applicant + "'";
            }

            if (!string.IsNullOrEmpty(item.ApplicantName))
            {
                sqlSearch += " and ApplicantName like '%" + item.ApplicantName + "%'";
            }

            if (DateTime.Compare(item.SubmitTimeFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(item.SubmitTimeFrom, sqlMinDate) < 0)
                    item.SubmitTimeFrom = sqlMinDate;

                sqlSearch += " and SubmitTime>='" + item.SubmitTimeFrom.ToShortDateString() + " 00:00:00'";
            }

            if (DateTime.Compare(item.SubmitTimeTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(item.SubmitTimeTo, sqlMaxDate) > 0)
                    item.SubmitTimeTo = sqlMaxDate;

                sqlSearch += " and SubmitTime<='" + item.SubmitTimeTo.ToShortDateString() + " 23:59:59'";
            }

            if (WFStates != null && WFStates.Length > 0)
            {
                sqlSearch += " and (";
                bool first = true;
                foreach (string wfstate in WFStates)
                {
                    if (first)
                    {
                        sqlSearch += " CurrentStateName='" + wfstate + "'";
                        first = false;
                    }
                    else
                        sqlSearch += "or CurrentStateName='" + wfstate + "'";
                }
                sqlSearch += ")";
            }
            

            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = VIEW_BORROW_APPLY;
            searchTerm.ReturnFields = "*";
            searchTerm.OrderBy = "order by SubmitTime desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        /// <summary>
        /// 生成审批历史记录列表的查询条件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam IBorrowApply.GenerateSearchTerm(BorrowApprovalSearchInfo item)
        {
            #region 生成where条件
            string sqlSearch = "where 1=1";
            if (!string.IsNullOrEmpty(item.SheetNO))
            {
                sqlSearch += " and a.SheetName = '" + item.SheetNO + "'";
            }
            if (!string.IsNullOrEmpty(item.BorrowCompanyID)&&item.BorrowCompanyID!="0")
            {
                sqlSearch += " and a.BorrowCompanyID='" + item.BorrowCompanyID + "'";
            }
            if (!string.IsNullOrEmpty(item.ApplicantName))
            {
                sqlSearch += " and a.ApplicantName like '%" + item.ApplicantName + "%'";
            }
            if (DateTime.Compare(item.ApplyDateFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(item.ApplyDateFrom, sqlMinDate) < 0)
                    item.ApplyDateFrom = sqlMinDate;

                sqlSearch += " and a.SubmitTime>='" + item.ApplyDateFrom.ToString("yyyy-MM-dd") + " 00:00:00'";
            }
            if (DateTime.Compare(item.ApplyDateTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(item.ApplyDateTo, sqlMaxDate) > 0)
                    item.ApplyDateTo = sqlMaxDate;

                sqlSearch += " and a.SubmitTime<='" + item.ApplyDateTo.ToString("yyyy-MM-dd") + " 23:59:59'";
            }

            if (DateTime.Compare(item.ApprovalDateFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(item.ApprovalDateFrom, sqlMinDate) < 0)
                    item.ApprovalDateFrom = sqlMinDate;

                sqlSearch += " and b.ApprovalDate>='" + item.ApprovalDateFrom.ToString("yyyy-MM-dd") + " 00:00:00'";
            }
            if (DateTime.Compare(item.ApprovalDateTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(item.ApprovalDateTo, sqlMaxDate) > 0)
                    item.ApprovalDateTo = sqlMaxDate;

                sqlSearch += " and b.ApprovalDate<='" + item.ApprovalDateTo.ToString("yyyy-MM-dd") + " 23:59:59'";
            }
            if (!string.IsNullOrEmpty(item.Approvaler))
            {
                sqlSearch += " and b.Approvaler='" + item.Approvaler + "'";
            }
            if (item.ApprovalResult != 3)
            {
                sqlSearch += " and b.Result=" + item.ApprovalResult;
            }
            #endregion
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = VIEW_BORROW_APPLY + " a inner JOIN "+VIEW_BORROW_APPROVAL+" b ON a.BorrowApplyID = b.BorrowApplyID";
            searchTerm.ReturnFields = "a.*";
            searchTerm.ReturnFields += ",b.Approvaler,b.ApprovalerName,b.Result as ApprovalResult,b.ApprovalDate as ApprovalDate";
            searchTerm.OrderBy = "order by ApprovalDate desc";
            searchTerm.Where = sqlSearch;

            return searchTerm;
        }
        #endregion

    }
}
