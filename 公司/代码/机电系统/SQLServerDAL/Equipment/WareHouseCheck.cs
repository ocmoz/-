using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using FM2E.IDAL.Equipment;
using FM2E.Model.Equipment;
using System.Data;
using FM2E.Model.Exceptions;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Utils;
using System.Data.SqlTypes;

namespace FM2E.SQLServerDAL.Equipment
{
    public class WareHouseCheck:IWareHouseCheck
    {
        private WareHouseCheckInfo GetData(IDataReader rd)
        {
            WareHouseCheckInfo item = new WareHouseCheckInfo();

            if (!Convert.IsDBNull(rd["FormNO"]))
                item.FormNO = Convert.ToString(rd["FormNO"]);

            if (!Convert.IsDBNull(rd["Status"]))
                item.Status = (WareHouseFormStatus)Convert.ToInt32(rd["Status"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["WareHouseID"]))
                item.WareHouseID = Convert.ToString(rd["WareHouseID"]);

            if (!Convert.IsDBNull(rd["WareHouseName"]))
                item.WareHouseName = Convert.ToString(rd["WareHouseName"]);

            if (!Convert.IsDBNull(rd["CheckDate"]))
                item.CheckDate = Convert.ToDateTime(rd["CheckDate"]);

            if (!Convert.IsDBNull(rd["MaterialType"]))
                item.MaterialType = (MaterialType)Convert.ToInt32(rd["MaterialType"]);

            if (!Convert.IsDBNull(rd["Checker"]))
                item.Checker = Convert.ToString(rd["Checker"]);

            if (!Convert.IsDBNull(rd["CheckerName"]))
                item.CheckerName = Convert.ToString(rd["CheckerName"]);

            if (!Convert.IsDBNull(rd["SpotCheck"]))
                item.SpotCheck = Convert.ToString(rd["SpotCheck"]);

            if (!Convert.IsDBNull(rd["StockCount"]))
                item.StockCount = Convert.ToString(rd["StockCount"]);

            if (!Convert.IsDBNull(rd["QuantitySituation"]))
                item.QuantitySituation = (QuantitySituation)Convert.ToInt32(rd["QuantitySituation"]);

            if (!Convert.IsDBNull(rd["QualitySituation"]))
                item.QualitySituation = (QualitySituation)Convert.ToInt32(rd["QualitySituation"]);

            if (!Convert.IsDBNull(rd["RegSituation"]))
                item.RegSituation = (RegSituation)Convert.ToInt32(rd["RegSituation"]);

            if (!Convert.IsDBNull(rd["ExceptionSituation"]))
                item.ExceptionSituation = Convert.ToString(rd["ExceptionSituation"]);

            if (!Convert.IsDBNull(rd["QuantityFeeBack"]))
                item.QuantityFeeBack = Convert.ToString(rd["QuantityFeeBack"]);

            if (!Convert.IsDBNull(rd["QuantityConfirmer"]))
                item.QuantityConfirmer = Convert.ToString(rd["QuantityConfirmer"]);

            if (!Convert.IsDBNull(rd["QualityFeeBack"]))
                item.QualityFeeBack = Convert.ToString(rd["QualityFeeBack"]);

            if (!Convert.IsDBNull(rd["QualityConfirmer"]))
                item.QualityConfirmer = Convert.ToString(rd["QualityConfirmer"]);

            if (!Convert.IsDBNull(rd["RegFeeBack"]))
                item.RegFeeBack = Convert.ToString(rd["RegFeeBack"]);

            if (!Convert.IsDBNull(rd["RegConfirmer"]))
                item.RegConfirmer = Convert.ToString(rd["RegConfirmer"]);

            if (!Convert.IsDBNull(rd["OtherFeeBack"]))
                item.OtherFeeBack = Convert.ToString(rd["OtherFeeBack"]);

            if (!Convert.IsDBNull(rd["OtherConfirmer"]))
                item.OtherConfirmer = Convert.ToString(rd["OtherConfirmer"]);

            if (!Convert.IsDBNull(rd["OtherConfirmTime"]))
                item.OtherConfirmTime = Convert.ToDateTime(rd["OtherConfirmTime"]);

            if (!Convert.IsDBNull(rd["ResultConfirmer"]))
                item.ResultConfirmer = Convert.ToString(rd["ResultConfirmer"]);

            if (!Convert.IsDBNull(rd["UpdateTime"]))
                item.UpdateTime = Convert.ToDateTime(rd["UpdateTime"]);

            return item;
        }
        #region IWareHouseCheck 成员
        /// <summary>
        /// 获取所有的仓库检查单
        /// </summary>
        /// <returns></returns>
        IList IWareHouseCheck.GetAllWareHouseCheck()
        {
            ArrayList list = new ArrayList();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.*,b.Name as WareHouseName,c.PersonName as CheckerName ");
                strSql.Append(" from FM2E_WareHouseCheck a left join FM2E_WareHouse b on a.WareHouseID=b.WareHouseID ");
                strSql.Append(" left join FM2E_User c on a.Checker=c.UserName ");

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while(rd.Read())
                    {
                       list.Add(GetData(rd));
                    }
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取仓库检查单列表失败", ex);
            }
            return list;
        }
        /// <summary>
        /// 获取符合条件的仓库检查单列表（支持分页）
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList IWareHouseCheck.GetWareHouseCheckList(QueryParam term, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取仓库检查单列表失败", e);
            }
        }
        /// <summary>
        /// 根据表单编号获取仓库检查单
        /// </summary>
        /// <param name="formNO"></param>
        /// <returns></returns>
        WareHouseCheckInfo IWareHouseCheck.GetWareHouseCheck(string formNO)
        {
            WareHouseCheckInfo item = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from FM2E_WareHouseCheck ");
                strSql.Append(" where FormNO=@FormNO");

                SqlParameter[] parameters = {
					new SqlParameter("@FormNO", SqlDbType.VarChar,50)};
                parameters[0].Value = formNO;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        item = new WareHouseCheckInfo();

                        if (!Convert.IsDBNull(rd["FormNO"]))
                            item.FormNO = Convert.ToString(rd["FormNO"]);

                        if (!Convert.IsDBNull(rd["Status"]))
                            item.Status = (WareHouseFormStatus)Convert.ToInt32(rd["Status"]);

                        if (!Convert.IsDBNull(rd["CompanyID"]))
                            item.CompanyID = Convert.ToString(rd["CompanyID"]);

                        if (!Convert.IsDBNull(rd["WareHouseID"]))
                            item.WareHouseID = Convert.ToString(rd["WareHouseID"]);

                        if (!Convert.IsDBNull(rd["CheckDate"]))
                            item.CheckDate = Convert.ToDateTime(rd["CheckDate"]);

                        if (!Convert.IsDBNull(rd["MaterialType"]))
                            item.MaterialType = (MaterialType)Convert.ToInt32(rd["MaterialType"]);

                        if (!Convert.IsDBNull(rd["Checker"]))
                            item.Checker = Convert.ToString(rd["Checker"]);

                        if (!Convert.IsDBNull(rd["SpotCheck"]))
                            item.SpotCheck = Convert.ToString(rd["SpotCheck"]);

                        if (!Convert.IsDBNull(rd["StockCount"]))
                            item.StockCount = Convert.ToString(rd["StockCount"]);

                        if (!Convert.IsDBNull(rd["QuantitySituation"]))
                            item.QuantitySituation = (QuantitySituation)Convert.ToInt32(rd["QuantitySituation"]);

                        if (!Convert.IsDBNull(rd["QualitySituation"]))
                            item.QualitySituation = (QualitySituation)Convert.ToInt32(rd["QualitySituation"]);

                        if (!Convert.IsDBNull(rd["RegSituation"]))
                            item.RegSituation = (RegSituation)Convert.ToInt32(rd["RegSituation"]);

                        if (!Convert.IsDBNull(rd["ExceptionSituation"]))
                            item.ExceptionSituation = Convert.ToString(rd["ExceptionSituation"]);

                        if (!Convert.IsDBNull(rd["QuantityFeeBack"]))
                            item.QuantityFeeBack = Convert.ToString(rd["QuantityFeeBack"]);

                        if (!Convert.IsDBNull(rd["QuantityConfirmer"]))
                            item.QuantityConfirmer = Convert.ToString(rd["QuantityConfirmer"]);

                        if (!Convert.IsDBNull(rd["QualityFeeBack"]))
                            item.QualityFeeBack = Convert.ToString(rd["QualityFeeBack"]);

                        if (!Convert.IsDBNull(rd["QualityConfirmer"]))
                            item.QualityConfirmer = Convert.ToString(rd["QualityConfirmer"]);

                        if (!Convert.IsDBNull(rd["RegFeeBack"]))
                            item.RegFeeBack = Convert.ToString(rd["RegFeeBack"]);

                        if (!Convert.IsDBNull(rd["RegConfirmer"]))
                            item.RegConfirmer = Convert.ToString(rd["RegConfirmer"]);

                        if (!Convert.IsDBNull(rd["OtherFeeBack"]))
                            item.OtherFeeBack = Convert.ToString(rd["OtherFeeBack"]);

                        if (!Convert.IsDBNull(rd["OtherConfirmer"]))
                            item.OtherConfirmer = Convert.ToString(rd["OtherConfirmer"]);

                        if (!Convert.IsDBNull(rd["OtherConfirmTime"]))
                            item.OtherConfirmTime = Convert.ToDateTime(rd["OtherConfirmTime"]);

                        if (!Convert.IsDBNull(rd["ResultConfirmer"]))
                            item.ResultConfirmer = Convert.ToString(rd["ResultConfirmer"]);

                        if (!Convert.IsDBNull(rd["UpdateTime"]))
                            item.UpdateTime = Convert.ToDateTime(rd["UpdateTime"]);

                    }
                }
            }
            catch (Exception ex)
            {
                item = null;
                throw new DALException("获取仓库检查单信息失败", ex);
            }

            return item;
        }
        /// <summary>
        /// 添加仓库检查单
        /// </summary>
        /// <param name="item"></param>
        void IWareHouseCheck.AddWareHouseCheck(WareHouseCheckInfo item)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FM2E_WareHouseCheck(");
                strSql.Append("FormNO,Status,QualitySituation,RegSituation,ExceptionSituation,QuantityFeeBack,QuantityConfirmer,QualityFeeBack,QualityConfirmer,RegFeeBack,RegConfirmer,OtherFeeBack,CompanyID,OtherConfirmer,OtherConfirmTime,ResultConfirmer,UpdateTime,WareHouseID,CheckDate,MaterialType,Checker,SpotCheck,StockCount,QuantitySituation)");
                strSql.Append(" values (");
                strSql.Append("@FormNO,@Status,@QualitySituation,@RegSituation,@ExceptionSituation,@QuantityFeeBack,@QuantityConfirmer,@QualityFeeBack,@QualityConfirmer,@RegFeeBack,@RegConfirmer,@OtherFeeBack,@CompanyID,@OtherConfirmer,@OtherConfirmTime,@ResultConfirmer,@UpdateTime,@WareHouseID,@CheckDate,@MaterialType,@Checker,@SpotCheck,@StockCount,@QuantitySituation)");
                SqlParameter[] parameters = {
					new SqlParameter("@FormNO", SqlDbType.VarChar,20),
					new SqlParameter("@QualitySituation", SqlDbType.TinyInt,1),
					new SqlParameter("@RegSituation", SqlDbType.TinyInt,1),
					new SqlParameter("@ExceptionSituation", SqlDbType.NVarChar,50),
					new SqlParameter("@QuantityFeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@QuantityConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@QualityFeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@QualityConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@RegFeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@RegConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@OtherFeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@OtherConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@OtherConfirmTime", SqlDbType.DateTime),
					new SqlParameter("@ResultConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@WareHouseID", SqlDbType.VarChar,2),
					new SqlParameter("@CheckDate", SqlDbType.DateTime),
					new SqlParameter("@MaterialType", SqlDbType.TinyInt,1),
					new SqlParameter("@Checker", SqlDbType.VarChar,20),
					new SqlParameter("@SpotCheck", SqlDbType.NVarChar,50),
					new SqlParameter("@StockCount", SqlDbType.NVarChar,50),
					new SqlParameter("@QuantitySituation", SqlDbType.TinyInt,1),
                    new SqlParameter("@Status",SqlDbType.TinyInt,1)};
                parameters[0].Value = item.FormNO;
                parameters[1].Value = item.QualitySituation;
                parameters[2].Value = item.RegSituation;
                parameters[3].Value = item.ExceptionSituation;
                parameters[4].Value = item.QuantityFeeBack;
                parameters[5].Value = item.QuantityConfirmer;
                parameters[6].Value = item.QualityFeeBack;
                parameters[7].Value = item.QualityConfirmer;
                parameters[8].Value = item.RegFeeBack;
                parameters[9].Value = item.RegConfirmer;
                parameters[10].Value = item.OtherFeeBack;
                parameters[11].Value = item.CompanyID;
                parameters[12].Value = item.OtherConfirmer;
                parameters[13].Value = DateTime.Compare(item.OtherConfirmTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.OtherConfirmTime;
                parameters[14].Value = item.ResultConfirmer;
                parameters[15].Value = DateTime.Compare(item.UpdateTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.UpdateTime;
                parameters[16].Value = item.WareHouseID;
                parameters[17].Value = DateTime.Compare(item.CheckDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.CheckDate;
                parameters[18].Value = item.MaterialType;
                parameters[19].Value = item.Checker;
                parameters[20].Value = item.SpotCheck;
                parameters[21].Value = item.StockCount;
                parameters[22].Value = item.QuantitySituation;
                parameters[23].Value = item.Status;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("添加仓库检查单失败", ex);
            }
        }
        /// <summary>
        /// 更新仓库检查单
        /// </summary>
        /// <param name="item"></param>
        void IWareHouseCheck.UpdateWareHouseCheck(WareHouseCheckInfo item)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_WareHouseCheck set ");
                strSql.Append("QualitySituation=@QualitySituation,");
                strSql.Append("Status=@Status,");
                strSql.Append("RegSituation=@RegSituation,");
                strSql.Append("ExceptionSituation=@ExceptionSituation,");
                strSql.Append("QuantityFeeBack=@QuantityFeeBack,");
                strSql.Append("QuantityConfirmer=@QuantityConfirmer,");
                strSql.Append("QualityFeeBack=@QualityFeeBack,");
                strSql.Append("QualityConfirmer=@QualityConfirmer,");
                strSql.Append("RegFeeBack=@RegFeeBack,");
                strSql.Append("RegConfirmer=@RegConfirmer,");
                strSql.Append("OtherFeeBack=@OtherFeeBack,");
                strSql.Append("CompanyID=@CompanyID,");
                strSql.Append("OtherConfirmer=@OtherConfirmer,");
                strSql.Append("OtherConfirmTime=@OtherConfirmTime,");
                strSql.Append("ResultConfirmer=@ResultConfirmer,");
                strSql.Append("UpdateTime=@UpdateTime,");
                strSql.Append("WareHouseID=@WareHouseID,");
                strSql.Append("CheckDate=@CheckDate,");
                strSql.Append("MaterialType=@MaterialType,");
                strSql.Append("Checker=@Checker,");
                strSql.Append("SpotCheck=@SpotCheck,");
                strSql.Append("StockCount=@StockCount,");
                strSql.Append("QuantitySituation=@QuantitySituation");
                strSql.Append(" where FormNO=@FormNO ");
                SqlParameter[] parameters = {
					new SqlParameter("@FormNO", SqlDbType.VarChar,20),
					new SqlParameter("@QualitySituation", SqlDbType.TinyInt,1),
					new SqlParameter("@RegSituation", SqlDbType.TinyInt,1),
					new SqlParameter("@ExceptionSituation", SqlDbType.NVarChar,50),
					new SqlParameter("@QuantityFeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@QuantityConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@QualityFeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@QualityConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@RegFeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@RegConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@OtherFeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@OtherConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@OtherConfirmTime", SqlDbType.DateTime),
					new SqlParameter("@ResultConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@WareHouseID", SqlDbType.VarChar,2),
					new SqlParameter("@CheckDate", SqlDbType.DateTime),
					new SqlParameter("@MaterialType", SqlDbType.TinyInt,1),
					new SqlParameter("@Checker", SqlDbType.VarChar,20),
					new SqlParameter("@SpotCheck", SqlDbType.NVarChar,50),
					new SqlParameter("@StockCount", SqlDbType.NVarChar,50),
					new SqlParameter("@QuantitySituation", SqlDbType.TinyInt,1),
                    new SqlParameter("@Status",SqlDbType.TinyInt,1)};

                parameters[0].Value = item.FormNO;
                parameters[1].Value = item.QualitySituation;
                parameters[2].Value = item.RegSituation;
                parameters[3].Value = item.ExceptionSituation;
                parameters[4].Value = item.QuantityFeeBack;
                parameters[5].Value = item.QuantityConfirmer;
                parameters[6].Value = item.QualityFeeBack;
                parameters[7].Value = item.QualityConfirmer;
                parameters[8].Value = item.RegFeeBack;
                parameters[9].Value = item.RegConfirmer;
                parameters[10].Value = item.OtherFeeBack;
                parameters[11].Value = item.CompanyID;
                parameters[12].Value = item.OtherConfirmer;
                parameters[13].Value = DateTime.Compare(item.OtherConfirmTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.OtherConfirmTime;
                parameters[14].Value = item.ResultConfirmer;
                parameters[15].Value = DateTime.Compare(item.UpdateTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.UpdateTime;
                parameters[16].Value = item.WareHouseID;
                parameters[17].Value = DateTime.Compare(item.CheckDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.CheckDate;
                parameters[18].Value = item.MaterialType;
                parameters[19].Value = item.Checker;
                parameters[20].Value = item.SpotCheck;
                parameters[21].Value = item.StockCount;
                parameters[22].Value = item.QuantitySituation;
                parameters[23].Value = item.Status;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("更新仓库检查单失败", ex);
            }
        }
        /// <summary>
        /// 删除仓库检查单
        /// </summary>
        /// <param name="formNO"></param>
        void IWareHouseCheck.DeleteWareHouseCheck(string formNO)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_WareHouseCheck ");
                strSql.Append(" where FormNO=@FormNO ");
                SqlParameter[] parameters = {
					new SqlParameter("@FormNO", SqlDbType.VarChar,50)};
                parameters[0].Value = formNO;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("删除仓库检查单失败", ex);
            }
        }
        /// <summary>
        /// 生成检查单查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam IWareHouseCheck.GenerateSearchTerm(WareHouseCheckSearchInfo term)
        {
            #region 生成where条件
            string sqlSearch = "where 1=1";
            if (!string.IsNullOrEmpty(term.FormNO))
                sqlSearch += " and a.FormNO like '%" + term.FormNO + "%'";

            if (!string.IsNullOrEmpty(term.WareHouseID) && term.WareHouseID != "0")
                sqlSearch += " and a.WareHouseID ='" + term.WareHouseID + "'";

            if (!string.IsNullOrEmpty(term.CompanyID) && term.CompanyID != "0")
                sqlSearch += " and a.CompanyID='" + term.CompanyID + "'";

            if (!string.IsNullOrEmpty(term.Checker))
                sqlSearch += " and a.Checker ='" + term.Checker + "'";

            if (term.Status != 0)
                sqlSearch += " and a.Status=" + (int)term.Status;

            if (DateTime.Compare(term.CheckDateFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(term.CheckDateFrom, sqlMinDate) < 0)
                    term.CheckDateFrom = sqlMinDate;

                sqlSearch += " and a.CheckDate>='" + term.CheckDateFrom.ToShortDateString() + " 00:00:00'";
            }

            if (DateTime.Compare(term.CheckDateTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(term.CheckDateTo, sqlMaxDate) > 0)
                    term.CheckDateTo = sqlMaxDate;

                sqlSearch += " and a.CheckDate<='" + term.CheckDateTo.ToShortDateString() + " 23:59:59'";
            }

            #endregion
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_WareHouseCheck a left join FM2E_WareHouse b on a.WareHouseID=b.WareHouseID";
            searchTerm.TableName += "  left join FM2E_User c on a.Checker=c.UserName ";
            searchTerm.ReturnFields = "a.*,b.Name as WareHouseName,c.PersonName as CheckerName";
            searchTerm.Where = sqlSearch;
            searchTerm.OrderBy = "order by CheckDate desc";

            return searchTerm;
            
        }
        /// <summary>
        /// 数量情况确认人签名
        /// </summary>
        /// <param name="formNO"></param>
        /// <param name="confirmer"></param>
        /// <param name="opinion"></param>
        void IWareHouseCheck.QuantitySign(string formNO,string confirmer, string opinion)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_WareHouseCheck ");
                strSql.Append(" set QuantityFeeBack=@QuantityFeeBack,QuantityConfirmer=@QuantityConfirmer,UpdateTime=@UpdateTime ");
                strSql.Append(" where FormNO=@FormNO");
                SqlParameter[] parameters = {
					new SqlParameter("@FormNO", SqlDbType.VarChar,20),
					new SqlParameter("@QuantityConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@QuantityFeeBack", SqlDbType.NVarChar,50),
                    new SqlParameter("@UpdateTime",SqlDbType.DateTime)};
                parameters[0].Value = formNO;
                parameters[1].Value = confirmer;
                parameters[2].Value = opinion;
                parameters[3].Value = DateTime.Now;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("签名失败", ex);
            }
        }
        /// <summary>
        /// 质量情况确认人签名
        /// </summary>
        /// <param name="formNO"></param>
        /// <param name="confirmer"></param>
        /// <param name="opinion"></param>
        void IWareHouseCheck.QualitySign(string formNO,string confirmer, string opinion)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_WareHouseCheck ");
                strSql.Append(" set QualityFeeBack=@QualityFeeBack,QualityConfirmer=@QualityConfirmer,UpdateTime=@UpdateTime  ");
                strSql.Append(" where FormNO=@FormNO");
                SqlParameter[] parameters = {
					new SqlParameter("@FormNO", SqlDbType.VarChar,20),
					new SqlParameter("@QualityConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@QualityFeeBack", SqlDbType.NVarChar,50),
                    new SqlParameter("@UpdateTime",SqlDbType.DateTime)};
                parameters[0].Value = formNO;
                parameters[1].Value = confirmer;
                parameters[2].Value = opinion;
                parameters[3].Value = DateTime.Now;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("签名失败", ex);
            }
        }
        /// <summary>
        /// 表单登记情况确认人签名
        /// </summary>
        /// <param name="formNO"></param>
        /// <param name="confirmer"></param>
        /// <param name="opinion"></param>
        void IWareHouseCheck.RegistrationSign(string formNO, string confirmer, string opinion)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_WareHouseCheck ");
                strSql.Append(" set RegFeeBack=@RegFeeBack,RegConfirmer=@RegConfirmer,UpdateTime=@UpdateTime ");
                strSql.Append(" where FormNO=@FormNO");
                SqlParameter[] parameters = {
					new SqlParameter("@FormNO", SqlDbType.VarChar,20),
					new SqlParameter("@RegConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@RegFeeBack", SqlDbType.NVarChar,50),
                    new SqlParameter("@UpdateTime",SqlDbType.DateTime)};
                parameters[0].Value = formNO;
                parameters[1].Value = confirmer;
                parameters[2].Value = opinion;
                parameters[3].Value = DateTime.Now;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("签名失败", ex);
            }
        }
        /// <summary>
        /// 其它意见确认人签名
        /// </summary>
        /// <param name="formNO"></param>
        /// <param name="confirmer"></param>
        /// <param name="opinion"></param>
        void IWareHouseCheck.OtherOpinionSign(string formNO, string confirmer, string opinion)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_WareHouseCheck ");
                strSql.Append(" set OtherFeeBack=@OtherFeeBack,OtherConfirmer=@OtherConfirmer,OtherConfirmTime=@OtherConfirmTime,UpdateTime=@UpdateTime ");
                strSql.Append(" where FormNO=@FormNO");
                SqlParameter[] parameters = {
					new SqlParameter("@FormNO", SqlDbType.VarChar,20),
					new SqlParameter("@OtherConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@OtherFeeBack", SqlDbType.NVarChar,50),
                    new SqlParameter("@OtherConfirmTime",SqlDbType.DateTime),
                    new SqlParameter("@UpdateTime",SqlDbType.DateTime)};
                parameters[0].Value = formNO;
                parameters[1].Value = confirmer;
                parameters[2].Value = opinion;
                parameters[3].Value = DateTime.Now;
                parameters[4].Value = DateTime.Now;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("签名失败", ex);
            }
        }
        /// <summary>
        /// 结果确认人签名
        /// </summary>
        /// <param name="formNO"></param>
        /// <param name="confirmer"></param>
        /// <param name="opinion"></param>
        /// <param name="status"></param>
        void IWareHouseCheck.ResultConfirmSign(string formNO, string confirmer, WareHouseFormStatus status)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_WareHouseCheck ");
                strSql.Append(" set ResultConfirmer=@ResultConfirmer,UpdateTime=@UpdateTime,Status=@Status ");
                strSql.Append(" where FormNO=@FormNO");
                SqlParameter[] parameters = {
					new SqlParameter("@FormNO", SqlDbType.VarChar,20),
					new SqlParameter("@ResultConfirmer", SqlDbType.VarChar,20),
                    new SqlParameter("@UpdateTime",SqlDbType.DateTime),
                    new SqlParameter("@Status",SqlDbType.TinyInt,1)};
                parameters[0].Value = formNO;
                parameters[1].Value = confirmer;
                parameters[2].Value = DateTime.Now;
                parameters[3].Value = status;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("签名失败", ex);
            }
        }
        #endregion
    }
}
