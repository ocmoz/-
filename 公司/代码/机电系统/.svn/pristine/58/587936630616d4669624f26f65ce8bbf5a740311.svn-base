using System;
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
    public class ReturnAcceptance:IReturnAcceptance
    {
        /// <summary>
        /// 从DataReader中提取数据
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private ReturnAcceptanceInfo GetData(IDataReader dr)
        {
            ReturnAcceptanceInfo item = new ReturnAcceptanceInfo();

            if (!Convert.IsDBNull(dr["ReturnID"]))
                item.ReturnID = Convert.ToInt64(dr["ReturnID"]);

            if (!Convert.IsDBNull(dr["EquipmentNO"]))
                item.EquipmentNO = Convert.ToString(dr["EquipmentNO"]);

            if (!Convert.IsDBNull(dr["EquipmentName"]))
                item.EquipmentName = Convert.ToString(dr["EquipmentName"]);

            item.Model = "";
            if (!Convert.IsDBNull(dr["Model"]))
                item.Model = Convert.ToString(dr["Model"]);

            if (!Convert.IsDBNull(dr["Specification"]))
            {
                item.Specification = Convert.ToString(dr["Specification"]);
                item.Model += "/" + item.Specification;
            }

            if (!Convert.IsDBNull(dr["BorrowApplyID"]))
                item.BorrowApplyID = Convert.ToInt64(dr["BorrowApplyID"]);

            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);

            if (!Convert.IsDBNull(dr["CompanyName"]))
                item.CompanyName = Convert.ToString(dr["CompanyName"]);

            if (!Convert.IsDBNull(dr["Result"]))
                item.Result = Convert.ToBoolean(dr["Result"]);

            if (!Convert.IsDBNull(dr["FeeBack"]))
                item.FeeBack = Convert.ToString(dr["FeeBack"]);

            if (!Convert.IsDBNull(dr["Checker"]))
                item.Checker = Convert.ToString(dr["Checker"]);

            if (!Convert.IsDBNull(dr["CheckerName"]))
                item.CheckerName = Convert.ToString(dr["CheckerName"]);

            if (!Convert.IsDBNull(dr["ReturnDate"]))
                item.ReturnDate = Convert.ToDateTime(dr["ReturnDate"]);

            if (!Convert.IsDBNull(dr["ReturnCompany"]))
                item.ReturnCompany = Convert.ToString(dr["ReturnCompany"]);

            if (!Convert.IsDBNull(dr["ReturnCompanyName"]))
                item.ReturnCompanyName = Convert.ToString(dr["ReturnCompanyName"]);

            if (!Convert.IsDBNull(dr["Returner"]))
                item.Returner = Convert.ToString(dr["Returner"]);

            if (!Convert.IsDBNull(dr["ReturnerName"]))
                item.ReturnerName = Convert.ToString(dr["ReturnerName"]);

            return item;
        }
        #region IReturnAcceptance 成员
        /// <summary>
        /// 获取某一项的设备归还验收记录
        /// </summary>
        /// <param name="returnID"></param>
        /// <returns></returns>
        ReturnAcceptanceInfo IReturnAcceptance.GetAcceptanceInfo(long returnID)
        {
            ReturnAcceptanceInfo item = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" from ReturnAcceptanceView");
                strSql.Append(" where ReturnID=@ReturnID");
                SqlParameter[] parameters = {
					new SqlParameter("@ReturnID", SqlDbType.BigInt)};
                parameters[0].Value = returnID;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        item = GetData(rd);
                    }
                }
            }
            catch (Exception ex)
            {
                item = null;
                throw new DALException("获取设备归还验收记录失败", ex);
            }
            return item;
        }
        /// <summary>
        /// 获取某张借调申请单的验收记录
        /// </summary>
        /// <param name="borrowApplyID"></param>
        /// <returns></returns>
        IList IReturnAcceptance.GetAcceptanceList(long borrowApplyID)
        {
            ArrayList list = new ArrayList();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM ReturnAcceptanceView");
                strSql.Append(" where BorrowApplyID=@BorrowApplyID order by ReturnDate desc");
                SqlParameter[] parameters = {
					new SqlParameter("@BorrowApplyID", SqlDbType.BigInt)};
                parameters[0].Value = borrowApplyID;

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
                throw new DALException("获取验收记录失败", e);
            }

            return list;
        }
        /// <summary>
        /// 获取验收记录（支持分页）
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList IReturnAcceptance.GetAcceptanceList(QueryParam term, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取设备验收记录失败", e);
            }
        }
        /// <summary>
        /// 获取某个设备借调验收的历史明细
        /// </summary>
        /// <param name="equipmentNO"></param>
        /// <returns></returns>
        IList IReturnAcceptance.GetBorrowAcceptanceHistory(string equipmentNO)
        {
            ArrayList list = new ArrayList();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM ReturnAcceptanceView");
                strSql.Append(" where EquipmentNO=@EquipmentNO");
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
                throw new DALException("设备归还验收历史记录失败", e);
            }

            return list;
        }

        /// <summary>
        /// 设备归还验收
        /// </summary>
        /// <param name="acceptanceRecords"></param>
        void IReturnAcceptance.AddAcceptanceRecord(IList acceptanceRecords)
        {
            if (acceptanceRecords == null || acceptanceRecords.Count == 0)
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
                strSql.Append("insert into FM2E_ReturnAcceptance(");
                strSql.Append("EquipmentNO,BorrowApplyID,CompanyID,Result,FeeBack,Checker,ReturnDate,ReturnCompany,Returner)");
                strSql.Append(" values (");
                strSql.Append("@EquipmentNO,@BorrowApplyID,@CompanyID,@Result,@FeeBack,@Checker,@ReturnDate,@ReturnCompany,@Returner)");
                SqlParameter[] parameters = {
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
					new SqlParameter("@BorrowApplyID", SqlDbType.BigInt,8),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Result", SqlDbType.Bit,1),
                    new SqlParameter("@FeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@Checker", SqlDbType.VarChar,20),
					new SqlParameter("@ReturnDate", SqlDbType.DateTime),
					new SqlParameter("@ReturnCompany", SqlDbType.VarChar,2),
					new SqlParameter("@Returner", SqlDbType.VarChar,20)};

                StringBuilder strReturnSql = new StringBuilder();
                strReturnSql.Append("update FM2E_BorrowRecord ");
                strReturnSql.Append(" set IsReturned=1");
                strReturnSql.Append(" where BorrowApplyID=@BorrowApplyID and EquipmentNO=@EquipmentNO");
                SqlParameter[] paraReturn ={
                   	new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
					new SqlParameter("@BorrowApplyID", SqlDbType.BigInt,8)
                                          };
                Equipment equipment = new Equipment();
                foreach (ReturnAcceptanceInfo item in acceptanceRecords)
                {
                    parameters[0].Value = item.EquipmentNO;
                    parameters[1].Value = item.BorrowApplyID;
                    parameters[2].Value = item.CompanyID;
                    parameters[3].Value = item.Result;
                    parameters[4].Value = item.FeeBack;
                    parameters[5].Value = item.Checker;
                    parameters[6].Value = item.ReturnDate;
                    parameters[7].Value = item.ReturnCompany;
                    parameters[8].Value = item.Returner;

                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                    if (item.Result)
                    {
                        //验收通过后，需要把借出设备的状态设置为已归还
                        paraReturn[0].Value = item.EquipmentNO;
                        paraReturn[1].Value = item.BorrowApplyID;
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strReturnSql.ToString(), paraReturn);

                        //equipment.UpdateEquipment(trans, item.EquipmentNO, item.SectionID, item.SystemID, item.LocationID, item.LocationTag);
                        equipment.UpdateEquipmentAddress(trans, item.EquipmentNO, item.AddressID, item.DetailLocation);
                    }
                }

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new DALException("添加设备归还验收记录失败", e);
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
        /// 删除某张借调申请单的某个设备的验收记录
        /// </summary>
        /// <param name="borrowApplyID"></param>
        /// <param name="equipmentNO"></param>
        void IReturnAcceptance.DeleteAcceptanceRecord(long borrowApplyID, string equipmentNO)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_ReturnAcceptance ");
                strSql.Append(" where ReturnApplyID=@ReturnApplyID and EquipmentNO=@EquipmentNO ");
                SqlParameter[] parameters = {
					new SqlParameter("@ReturnApplyID", SqlDbType.BigInt),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50)};
                parameters[0].Value = borrowApplyID;
                parameters[1].Value = equipmentNO;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除设备验收记录失败", e);
            }
        }

        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam IReturnAcceptance.GenerateSearchTerm(ReturnAcceptanceSearchInfo term)
        {
            QueryParam searchTerm = new QueryParam();
            try
            {
                #region 生成where条件
                string sqlSearch = "where 1=1";
                if (!string.IsNullOrEmpty(term.EquipmentNO))
                    sqlSearch += " and a.EquipmentNO like '%" + term.EquipmentNO + "%'";

                if (!string.IsNullOrEmpty(term.EquipmentName))
                    sqlSearch += " and a.EquipmentName like '%" + term.EquipmentName + "%'";

                if (!string.IsNullOrEmpty(term.SheetNO))
                    sqlSearch += " and b.SheetName like '%" + term.SheetNO + "%'";

                if (!string.IsNullOrEmpty(term.CompanyID) && term.CompanyID != "0")
                    sqlSearch += " and a.CompanyID='" + term.CompanyID + "'";

                if (!string.IsNullOrEmpty(term.ReturnCompany) && term.ReturnCompany != "0")
                    sqlSearch += " and a.ReturnCompany='" + term.ReturnCompany + "'";

                if (term.Result != 3)
                    sqlSearch += " and a.result=" + term.Result;

                if (DateTime.Compare(term.CheckDateFrom, DateTime.MinValue) != 0)
                {
                    DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                    if (DateTime.Compare(term.CheckDateFrom, sqlMinDate) < 0)
                        term.CheckDateFrom = sqlMinDate;

                    sqlSearch += " and a.ReturnDate>='" + term.CheckDateFrom.ToShortDateString() + " 00:00:00'";
                }
                if (DateTime.Compare(term.CheckDateTo, DateTime.MinValue) != 0)
                {
                    DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                    if (DateTime.Compare(term.CheckDateTo, sqlMaxDate) > 0)
                        term.CheckDateTo = sqlMaxDate;

                    sqlSearch += " and a.ReturnDate<='" + term.CheckDateTo.ToShortDateString() + " 23:59:59'";
                }

                #endregion

                if (string.IsNullOrEmpty(term.SheetNO))
                {
                    searchTerm.TableName = "ReturnAcceptanceView a";
                    searchTerm.ReturnFields = "a.*";
                }
                else
                {
                    searchTerm.TableName = "ReturnAcceptanceView a left join FM2E_BorrowApply b on a.BorrowApplyID=b.BorrowApplyID";
                    searchTerm.ReturnFields = "a.*";
                }
                searchTerm.OrderBy = "order by ReturnDate desc";
                searchTerm.Where = sqlSearch;
                return searchTerm;
            }
            catch (Exception ex)
            {
                searchTerm = null;
                throw new DALException("生成查询条件出错", ex);
            }
        }
        #endregion
    }
}
