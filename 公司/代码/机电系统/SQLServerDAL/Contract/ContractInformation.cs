using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Contract;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;
using FM2E.SQLServerDAL.Utils;
using System.Data;
using FM2E.Model.Contract;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace FM2E.SQLServerDAL.Contract
{
    public class ContractInformation : IContractInformation
    {
        private const string DELETE_CONTRACTINFORMATION_INFO = "delete from FM2E_Contract where [ID]='{0}'";

        private const string QUERY_CONTRACTINFORMATION_INFO_BY_ID = "select * from FM2E_Contract where id = ";

        private const string INSERT_CONTRACTINFORMATION_INFO =
            "Insert into FM2E_Contract ([ContractNo],[ContractName],[ContractAmount],[SettlementAmount],[ContractedUnits],[Period],[Retentions],[ContractPeople],[ContractTheWay],Attachment) values(@ContractNo,@ContractName,@ContractAmount,@SettlementAmount,@ContractedUnits,@Period,@Retentions,@ContractPeople,@ContractTheWay,@Attachment) ";

        private const string UPDATE_CONTRACTINFORMATION_INFO =
            "Update FM2E_Contract set [ContractNo]=@ContractNo,[ContractName]=@ContractName,[ContractAmount]=@ContractAmount,[SettlementAmount]=@SettlementAmount,[ContractedUnits]=@ContractedUnits,[Period]=@Period,[Retentions]=@Retentions,[ContractPeople]=@ContractPeople,[ContractTheWay]=@ContractTheWay,Attachment=@Attachment where [Id] = @Id";



        private const string SELECT_CONTRACTINTERIMPAYMENT =
              "SELECT * FROM FM2E_ContractInterimPayment where ContractId='{0}'";

        private const string DEL_CONTRACTINTERIMPAYMENT = "delete from FM2E_ContractInterimPayment where [Id]='{0}'";

        private const string SELECT_CONTRACTINTERIMPAYMENTINFO = "select * from FM2E_ContractInterimPayment where [Id]='{0}'";


        private const string INSERT_CONTRACTINTERIMPAYMENT = "insert into FM2E_ContractInterimPayment([ContractId],[PaymentAmount],[PaymentTime]) "
                                            + "values(@ContractId,@PaymentAmount,@PaymentTime)";

        private const string UPDATE_CONTRACTINTERIMPAYMENT = "update FM2E_ContractInterimPayment "
                                            + "set [PaymentAmount]=@PaymentAmount,PaymentTime=@PaymentTime where [Id]=@Id";

        public void InsertContractInformation(ContractInformationInfo item)
        {
            SqlParameter[] param = GetInsertParam();
            param[0].Value = item.ContractNo;
            param[1].Value = item.ContractName;
            param[2].Value = item.ContractAmount;
            param[3].Value = item.SettlementAmount;
            param[4].Value = item.ContractedUnits;
            param[5].Value = item.Period;
            param[6].Value = item.Retentions;
            param[7].Value = item.ContractPeople;
            param[8].Value = item.ContractTheWay;
            param[9].Value = item.Attachment;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, INSERT_CONTRACTINFORMATION_INFO, param);
                    if (result == 0)
                        throw new Exception("没有添加任何数据项");
                }
                catch (Exception e)
                {
                    throw e;// DALException("插入设备信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void UpdateContractInformationInfo(ContractInformationInfo item)
        {
            SqlParameter[] param = GetUpdateParam();

            param[0].Value = item.ContractNo;
            param[1].Value = item.ContractName;
            param[2].Value = item.ContractAmount;
            param[3].Value = item.SettlementAmount;
            param[4].Value = item.ContractedUnits;
            param[5].Value = item.Period;
            param[6].Value = item.Retentions;
            param[7].Value = item.ContractPeople;
            param[8].Value = item.ContractTheWay;
            param[9].Value = item.Id;
            param[10].Value = item.Attachment;
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, UPDATE_CONTRACTINFORMATION_INFO, param);
                    if (result == 0)
                        throw new Exception("没有更新任何数据项");
                }
                catch (Exception e)
                {
                    throw e;// DALException("插入设备信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 获取ContractInformationInfo得SQLPararment
        /// </summary>
        /// <returns></returns>
        private static SqlParameter[] GetInsertParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(INSERT_CONTRACTINFORMATION_INFO);
            if (param == null)
            {
                param = new SqlParameter[]{
                    new SqlParameter("@ContractNo",SqlDbType.VarChar,30),
                    new SqlParameter("@ContractName",SqlDbType.NVarChar,50),
                    new SqlParameter("@ContractAmount",SqlDbType.Decimal),
                    new SqlParameter("@SettlementAmount",SqlDbType.Decimal),
                    new SqlParameter("@ContractedUnits",SqlDbType.NVarChar,50),
                    new SqlParameter("@Period",SqlDbType.Int,2), 
                    new SqlParameter("@Retentions",SqlDbType.Decimal),
                    new SqlParameter("@ContractPeople",SqlDbType.NVarChar,50),
                    new SqlParameter("@ContractTheWay",SqlDbType.NVarChar,50),
                    new SqlParameter("@Attachment",SqlDbType.NVarChar,1000)
                };
                SQLHelper.CacheParameters(INSERT_CONTRACTINFORMATION_INFO, param);
            }
            return param;
        }

        private static SqlParameter[] GetUpdateParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(UPDATE_CONTRACTINFORMATION_INFO);
            if (param == null)
            {
                param = new SqlParameter[]{
                     new SqlParameter("@ContractNo",SqlDbType.VarChar,30),
                    new SqlParameter("@ContractName",SqlDbType.NVarChar,50),
                    new SqlParameter("@ContractAmount",SqlDbType.Decimal),
                    new SqlParameter("@SettlementAmount",SqlDbType.Decimal),
                    new SqlParameter("@ContractedUnits",SqlDbType.NVarChar,50),
                    new SqlParameter("@Period",SqlDbType.Int,2), 
                    new SqlParameter("@Retentions",SqlDbType.Decimal),
                    new SqlParameter("@ContractPeople",SqlDbType.NVarChar,50),
                    new SqlParameter("@ContractTheWay",SqlDbType.NVarChar,50),
                    new SqlParameter("@Id",SqlDbType.Int,32),
                    new SqlParameter("@Attachment",SqlDbType.NVarChar,1000)
                };
                SQLHelper.CacheParameters(UPDATE_CONTRACTINFORMATION_INFO, param);
            }
            return param;
        }

        public ContractInformationInfo GetContractInformationInfo(long id)
        {
            ContractInformationInfo contractInformationInfo = null;
            try
            {
                string queryStr = QUERY_CONTRACTINFORMATION_INFO_BY_ID + id;
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, queryStr, null))
                {
                    if (rd.Read())
                        contractInformationInfo = GetContractInformationInfo(rd);
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取合同基本信息失败", e);
            }
            return contractInformationInfo;
        }

        public void DelContractInformation(string id)
        {
            string cmd = string.Format(DELETE_CONTRACTINFORMATION_INFO, id);
            try
            {
                int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, cmd, null);
                if (rows == 0)
                    throw new Exception("没有删除任何数据项！");
            }
            catch (Exception e)
            {
                throw new DALException("删除合同基本信息失败", e);
            }
        }

        /// <summary>
        /// 查询合同
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetContractInformation(QueryParam term, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetContractInformationInfo, term, out recordCount);
            }
            catch (Exception ex)
            {
                throw new DALException("获取合同基本信息表分页失败", ex);
            }
        }

        public QueryParam GetSearchTerm(ContractInformationInfo term)
        {
            QueryParam qp = new QueryParam();
            qp.ReturnFields = "*";
            qp.TableName = "FM2E_Contract";
            qp.Where = GetSqlWhere(term);
            qp.OrderBy = "order by Id desc";

            return qp;
        }

        private string GetSqlWhere(ContractInformationInfo term)
        {
            string sqlSearch = "where 1=1 ";

            if (!string.IsNullOrEmpty(term.ContractNo))
                sqlSearch += " and ContractNo like '%" + term.ContractNo + "%'";

            if (!string.IsNullOrEmpty(term.ContractName))
                sqlSearch += " and ContractName like '%" + term.ContractName + "%'";

            if (term.Period != 0)
                sqlSearch += " and Period=" + (int)term.Period;

            return sqlSearch;
        }

        private ContractInformationInfo GetContractInformationInfo(IDataReader dr)
        {
            ContractInformationInfo item = new ContractInformationInfo();
            if (!Convert.IsDBNull(dr["Id"]))
            {
                item.Id = Convert.ToInt64(dr["Id"]);
            }
            if (!Convert.IsDBNull(dr["ContractNo"]))
            {
                item.ContractNo = Convert.ToString(dr["ContractNo"]);
            }
            if (!Convert.IsDBNull(dr["ContractName"]))
            {
                item.ContractName = Convert.ToString(dr["ContractName"]);
            }
            if (!Convert.IsDBNull(dr["ContractAmount"]))
            {
                item.ContractAmount = Convert.ToDecimal(dr["ContractAmount"]);
            }
            if (!Convert.IsDBNull(dr["SettlementAmount"]))
            {
                item.SettlementAmount = Convert.ToDecimal(dr["SettlementAmount"]);
            }
            if (!Convert.IsDBNull(dr["ContractedUnits"]))
            {
                item.ContractedUnits = Convert.ToString(dr["ContractedUnits"]);
            }
            if (!Convert.IsDBNull(dr["Period"]))
            {
                item.Period = Convert.ToInt32(dr["Period"]);
            }
            if (!Convert.IsDBNull(dr["Retentions"]))
            {
                item.Retentions = Convert.ToDecimal(dr["Retentions"]);
            }
            if (!Convert.IsDBNull(dr["ContractPeople"]))
            {
                item.ContractPeople = Convert.ToString(dr["ContractPeople"]);
            }
            if (!Convert.IsDBNull(dr["ContractTheWay"]))
            {
                item.ContractTheWay = Convert.ToString(dr["ContractTheWay"]);
            }         
            if (!Convert.IsDBNull(dr["Prepaid"]))
            {
                item.Prepaid = Convert.ToDecimal(dr["Prepaid"]);
            }
            if (!Convert.IsDBNull(dr["InterimPaymentId"]))
            {
                item.InterimPaymentId = Convert.ToInt32(dr["InterimPaymentId"]);
            }
            if (!Convert.IsDBNull(dr["CompletedPayment"]))
            {
                item.CompletedPayment = Convert.ToDecimal(dr["CompletedPayment"]);
            }
            if (!Convert.IsDBNull(dr["HandOverpay"]))
            {
                item.HandOverpay = Convert.ToDecimal(dr["HandOverpay"]);
            }
            if (!Convert.IsDBNull(dr["Attachment"]))
            {
                item.Attachment = Convert.ToString(dr["Attachment"]);
            }
            
            return item;
        }

        public void UpdatePrepaid(ContractInformationInfo item)
        {
            string UPDATE_PREPAID_INFO = "Update FM2E_Contract set SettlementAmount=@SettlementAmount, Prepaid=@Prepaid,CompletedPayment=@CompletedPayment,HandOverpay=@HandOverpay where [Id] = @Id";
           
            SqlParameter[] param = new SqlParameter[]{
                        new SqlParameter("@Prepaid",SqlDbType.Decimal),
                        new SqlParameter("@CompletedPayment",SqlDbType.Decimal),
                        new SqlParameter("@HandOverpay",SqlDbType.Decimal),
                         new SqlParameter("@SettlementAmount",SqlDbType.Decimal),
                        new SqlParameter("@Id",SqlDbType.Int,32)
                         };
            param[0].Value = item.Prepaid;
            param[1].Value = item.CompletedPayment;
            param[2].Value = item.HandOverpay;
            param[3].Value = item.SettlementAmount;   
            param[4].Value = item.Id;     
               
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, UPDATE_PREPAID_INFO, param);
                    if (result == 0)
                        throw new Exception("没有更新任何数据项");
                }
                catch (Exception e)
                {
                    throw e;// DALException("插入设备信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        public IList GetInterimPayment(int id)
        {
            string cmd = string.Format(SELECT_CONTRACTINTERIMPAYMENT, id);
            IList list = new List<ContractInterimPaymentInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                    {
                        list.Add(GetInterimPaymentData(rd));
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取合同期中支付信息时失败", e);
            }

            return list;
        }

        private ContractInterimPaymentInfo GetInterimPaymentData(IDataReader dr)
        {
            ContractInterimPaymentInfo item = new ContractInterimPaymentInfo();
            if (!Convert.IsDBNull(dr["Id"]))
                item.Id = Convert.ToInt32(dr["Id"]);
            if (!Convert.IsDBNull(dr["ContractId"]))
                item.ContractId = Convert.ToInt32(dr["ContractId"]);
            if (!Convert.IsDBNull(dr["PaymentAmount"]))
                item.PaymentAmount = Convert.ToDecimal(dr["PaymentAmount"]);
            if (!Convert.IsDBNull(dr["PaymentTime"]))        
                item.PaymentTime = Convert.ToDateTime(dr["PaymentTime"]);
            return item;
        }

        public void DelInterimPayment(long id)
        {
            string cmd = string.Format(DEL_CONTRACTINTERIMPAYMENT, id);
            try
            {
                int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, cmd, null);
                if (rows == 0)
                    throw new Exception("没有删除任何数据项！");
            }
            catch
            {
                throw;
            }
        }

        public ContractInterimPaymentInfo GetContractInterimPaymentInfo(long id)
        {
            string cmd = string.Format(SELECT_CONTRACTINTERIMPAYMENTINFO, id);
            ContractInterimPaymentInfo item = null;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                    {
                        item = GetData(rd);
                    }
                }

            }
            catch
            {
                throw;
            }
            return item;
        }

        private ContractInterimPaymentInfo GetData(IDataReader rd)
        {
            ContractInterimPaymentInfo item = new ContractInterimPaymentInfo();
            if (!Convert.IsDBNull(rd["Id"]))
                item.Id = Convert.ToInt32(rd["Id"]);

            if (!Convert.IsDBNull(rd["ContractId"]))
                item.ContractId = Convert.ToInt32(rd["ContractId"]);

            if (!Convert.IsDBNull(rd["PaymentAmount"]))
                item.PaymentAmount = Convert.ToDecimal(rd["PaymentAmount"]);
            if (!Convert.IsDBNull(rd["PaymentTime"]))
                item.PaymentTime = Convert.ToDateTime(rd["PaymentTime"]);
            return item;
        }

        public void InsertContractInterimPayment(ContractInterimPaymentInfo item)
        {
            SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@ContractId",SqlDbType.Int,20),
                    new SqlParameter("@PaymentAmount",SqlDbType.Decimal),
            new SqlParameter("@PaymentTime",SqlDbType.DateTime)};
            param[0].Value = item.ContractId;
            param[1].Value = item.PaymentAmount;
            param[2].Value = item.PaymentTime;
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, INSERT_CONTRACTINTERIMPAYMENT, param);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void UpdateContractInterimPayment(ContractInterimPaymentInfo item)
        {
            SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@PaymentAmount",SqlDbType.Int,20),
                      new SqlParameter("@PaymentTime",SqlDbType.DateTime),
                    new SqlParameter("@Id",SqlDbType.Decimal)};
            param[0].Value = item.PaymentAmount;
            param[1].Value = item.PaymentTime;
            param[2].Value = item.Id;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, UPDATE_CONTRACTINTERIMPAYMENT, param);
                    if (result == 0)
                        throw new Exception("没有更新任何数据项");
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }

        }
    }
}
