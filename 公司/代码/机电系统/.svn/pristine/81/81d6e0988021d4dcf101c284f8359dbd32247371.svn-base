using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using System.Collections;
using System.Data;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Exceptions;
using System.Data.SqlClient;
using FM2E.IDAL.Equipment;
using System.Data.SqlTypes;

namespace FM2E.SQLServerDAL.Equipment
{
    public class PriceManager : IPriceManager
    {
        /// <summary>
        /// PriceApply
        /// </summary>
        private const string PriceApplyTableName = " FM2E_PriceApply pr left join FM2E_User b on pr.applicant=b.UserName left join FM2E_User c on pr.Approvaler=c.UserName";

        private const string PriceApplyReturnFields = " pr.*,b.PersonName as ApplicantName,c.PersonName as ApprovalerName ";

        private const string PriceApplyOrderBy = " order by ApplyFormID desc ";

        private const string PriceApplyWhere = " where 1=1 ";


        public IList<PriceApplyInfo> GetAllPriceApply()
        {
            throw new NotImplementedException();
        }


        public IList<PriceApplyInfo> GetRecentPriceApply(int num)
        {
           throw new NotImplementedException();
        }

        public PriceApplyInfo GetPriceApply(string id)
        {
            throw new NotImplementedException();
        }

        public long InsertPriceApply(PriceApplyInfo item)
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
                id = InsertPriceApplyBase(trans, item);

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FM2E_PriceApplyDetail");
                strSql.Append("(ApplyFormID,CompanyID,ProductName,Model,StartTime,OldUpperPrice,OldLowerPrice,NewUpperPrice,NewLowerPrice,Reason,Result,FeeBack,DeleteOld,Status,instanceId,Unit)");
                strSql.Append(" values ");
                strSql.Append("(@ApplyFormID,@CompanyID,@ProductName,@Model,@StartTime,@OldUpperPrice,@OldLowerPrice,@NewUpperPrice,@NewLowerPrice,@Reason,@Result,@FeeBack,@DeleteOld,@Status,@instanceId,@Unit)");
                SqlParameter[] parameters = 
                {
                    new SqlParameter("@ApplyFormID",SqlDbType.BigInt,8),
                    new SqlParameter("@CompanyID",SqlDbType.VarChar,2),
                    new SqlParameter("@ProductName",SqlDbType.NVarChar,20),
                    new SqlParameter("@Model",SqlDbType.NVarChar,20),
                    new SqlParameter("@StartTime",SqlDbType.DateTime),
                    new SqlParameter("@OldUpperPrice",SqlDbType.Decimal,10),
                    new SqlParameter("@OldLowerPrice",SqlDbType.Decimal,10),
                    new SqlParameter("@NewUpperPrice",SqlDbType.Decimal,10),
                    new SqlParameter("@NewLowerPrice",SqlDbType.Decimal,10),
                    new SqlParameter("@Reason",SqlDbType.NVarChar,100),
                    new SqlParameter("@Result",SqlDbType.TinyInt,1),
                    new SqlParameter("@FeeBack",SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteOld",SqlDbType.TinyInt,1),
                    new SqlParameter("@Status",SqlDbType.NVarChar,20),
                    new SqlParameter("@instanceId",SqlDbType.UniqueIdentifier),
                    new SqlParameter("@Unit",SqlDbType.NVarChar,20)
                };
                foreach (PriceApplyDetailInfo model in item.DetailList)
                {
                    parameters[0].Value = id;
                    parameters[1].Value = model.CompanyID;
                    parameters[2].Value = model.ProductName;
                    parameters[3].Value = model.Model;
                    parameters[4].Value = DateTime.Compare(model.StartTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : model.StartTime;
                    parameters[5].Value = model.OldUpperPrice;
                    parameters[6].Value = model.OldLowerPrice;
                    parameters[7].Value = model.NewUpperPrice;
                    parameters[8].Value = model.NewLowerPrice;
                    parameters[9].Value = model.Reason;
                    parameters[10].Value = model.Result;
                    parameters[11].Value = model.FeeBack;
                    parameters[12].Value = model.DeleteOld;
                    parameters[13].Value = model.Status;
                    parameters[14].Value = model.instanceId;
                    parameters[15].Value = model.Unit;
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                }
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new DALException("添加指导价格修改申请失败", e);
            }
            return id;
        }
        public void UpdatePriceApply(PriceApplyInfo item)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();
                //先更新基本信息
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_PriceApply set ");
                strSql.Append("Approvaler=@Approvaler,ApprovalDate=@ApprovalDate,Status=@Status ");
                strSql.Append("where ApplyFormID=@ApplyFormID");
                SqlParameter[] parameters2 = 
                {
                    new SqlParameter("@Approvaler",SqlDbType.VarChar,20),
                    new SqlParameter("@ApprovalDate",SqlDbType.DateTime),
                    new SqlParameter("@ApplyFormID",SqlDbType.BigInt,8),
                    new SqlParameter("@Status", SqlDbType.TinyInt,1)
                };
                parameters2[0].Value = item.Approvaler;
                parameters2[1].Value = DateTime.Compare(item.ApprovalDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.ApprovalDate;
                parameters2[2].Value = item.ApplyFormID;
                parameters2[3].Value = (int)item.Status;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters2);
                strSql.Remove(0, strSql.Length);

                //后更新详细申请内容
                strSql.Append("update FM2E_PriceApplyDetail set ");
                strSql.Append("FeeBack=@FeeBack,Status=@Status ");
                strSql.Append("where ApplyFormID=@ApplyFormID and CompanyID=@CompanyID and ProductName=@ProductName and Model=@Model");
                SqlParameter[] parameters =
                {
                    new SqlParameter("@FeeBack",SqlDbType.NVarChar,50),
                    new SqlParameter("@Status",SqlDbType.NVarChar,20),
                    new SqlParameter("@ApplyFormID",SqlDbType.BigInt,8),
                    new SqlParameter("@CompanyID",SqlDbType.VarChar,2),
                    new SqlParameter("@ProductName",SqlDbType.NVarChar,20),
                    new SqlParameter("@Model",SqlDbType.NVarChar,20)
                };
                foreach (PriceApplyDetailInfo model in item.DetailList)
                {
                    parameters[0].Value = model.FeeBack;
                    parameters[1].Value = model.Status;
                    parameters[2].Value = model.ApplyFormID;
                    parameters[3].Value = model.CompanyID;
                    parameters[4].Value = model.ProductName;
                    parameters[5].Value = model.Model;
                    if (model.Status == "通过")
                    {
                        switch (model.DeleteOld)
                        {
                            case 0:
                                {
                                    InsertPriceHistory(trans, model);
                                    UpdatePriceDeitail(trans, model);
                                    break;
                                }
                            case 1:
                                {
                                    InsertPriceHistory(trans, model);
                                    DeletePriceDetail(trans, model);
                                    break;
                                }
                            case 2:
                                {
                                    InsertPriceDetail(trans, model);
                                    break;
                                }
                        }
                    }
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                }
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new DALException("更新指导价格申请表失败", e);
            }



        }

        private void InsertPriceDetail(SqlTransaction trans, PriceApplyDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_RecommendPriceDetail(");
            strSql.Append("CompanyID,ProductName,Model,StartTime,UpperPrice,LowerPrice,Unit)");
            strSql.Append(" values (");
            strSql.Append("@CompanyID,@ProductName,@Model,@StartTime,@UpperPrice,@LowerPrice,@Unit)");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@Model", SqlDbType.NVarChar,20),
					new SqlParameter("@StartTime", SqlDbType.DateTime),
					new SqlParameter("@UpperPrice", SqlDbType.Decimal,9),
					new SqlParameter("@LowerPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@Unit",SqlDbType.NVarChar,20)
                                        };
            parameters[0].Value = model.CompanyID;
            parameters[1].Value = model.ProductName;
            parameters[2].Value = model.Model;
            parameters[3].Value = DateTime.Now;
            parameters[4].Value = model.NewUpperPrice;
            parameters[5].Value = model.NewLowerPrice;
            parameters[6].Value = model.Unit;
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        }

        private void UpdatePriceDeitail(SqlTransaction trans, PriceApplyDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_RecommendPriceDetail set ");
            strSql.Append("StartTime=@StartTime,UpperPrice=@UpperPrice,LowerPrice=@LowerPrice ");
            strSql.Append("where CompanyID=@CompanyID and ProductName=@ProductName and Model=@Model");
            SqlParameter[] parameters = 
            {
                new SqlParameter("@StartTime",SqlDbType.DateTime),
                new SqlParameter("@UpperPrice",SqlDbType.Decimal,18),
                new SqlParameter("@LowerPrice",SqlDbType.Decimal,18),
                new SqlParameter("@CompanyID",SqlDbType.VarChar,2),
                new SqlParameter("@ProductName",SqlDbType.NVarChar,20),
                new SqlParameter("@Model",SqlDbType.NVarChar,20)

            };
            parameters[0].Value = DateTime.Now;
            parameters[1].Value = model.NewUpperPrice;
            parameters[2].Value = model.NewLowerPrice;
            parameters[3].Value = model.CompanyID;
            parameters[4].Value = model.ProductName;
            parameters[5].Value = model.Model;
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        }
        private void InsertPriceHistory(SqlTransaction trans, PriceApplyDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_RecommendPriceHistory");
            strSql.Append("(CompanyID,ProductName,Model,StartTime,EndTime,UpperPrice,LowerPrice)");
            strSql.Append(" values ");
            strSql.Append("(@CompanyID,@ProductName,@Model,@StartTime,@EndTime,@UpperPrice,@LowerPrice);");
            SqlParameter[] parameters =
            {
                new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@Model", SqlDbType.NVarChar,20),
					new SqlParameter("@StartTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@UpperPrice", SqlDbType.Decimal,9),
					new SqlParameter("@LowerPrice", SqlDbType.Decimal,9)

            };
            parameters[0].Value = model.CompanyID;
            parameters[1].Value = model.ProductName;
            parameters[2].Value = model.Model;
            parameters[3].Value = model.StartTime;
            parameters[4].Value = DateTime.Now;
            parameters[5].Value = model.OldUpperPrice;
            parameters[6].Value = model.OldLowerPrice;
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        }
        private void DeletePriceDetail(SqlTransaction trans, PriceApplyDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_RecommendPriceDetail ");
            strSql.Append(" where CompanyID=@CompanyID and ProductName=@ProductName and Model=@Model ");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyID", SqlDbType.VarChar,50),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,50),
					new SqlParameter("@Model", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.CompanyID;
            parameters[1].Value = model.ProductName;
            parameters[2].Value = model.Model;
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        }

        private long InsertPriceApplyBase(SqlTransaction trans, PriceApplyInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_PriceApply(");
            strSql.Append("CompanyID,Applicant,Approvaler,ApprovalDate,ApplyDate,Status)");
            strSql.Append(" values (");
            strSql.Append("@CompanyID,@Applicant,@Approvaler,@ApprovalDate,@ApplyDate,@Status)");
            strSql.Append("select cast(@@IDENTITY as BIGINT);");
            SqlParameter[] parameters =
            {
               new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Applicant", SqlDbType.VarChar,20),
					new SqlParameter("@Approvaler", SqlDbType.VarChar,20),
					new SqlParameter("@ApprovalDate", SqlDbType.DateTime),
					new SqlParameter("@ApplyDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.TinyInt,1)};
            parameters[0].Value = model.CompanyID;
            parameters[1].Value = model.Applicant;
            parameters[2].Value = model.Approvaler;
            parameters[3].Value = DateTime.Compare(model.ApprovalDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : model.ApprovalDate;
            parameters[4].Value = DateTime.Compare(model.ApplyDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : model.ApplyDate;
            parameters[5].Value = (int)model.Status;
            long id = 1;
            id = (long)SQLHelper.ExecuteScalar(trans, CommandType.Text, strSql.ToString(), parameters);

            return id;
        }




        public void DelPriceApply(PriceApplyInfo item)
        {
            throw new NotImplementedException();
        }
        public IList<PriceApplyInfo> SearchPriceApply(PriceApplyInfo item)
        {
            throw new NotImplementedException();
        }

        public QueryParam GeneratePriceApplySearchTerm(PriceApplyInfo item)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = PriceApplyTableName;
            qp.ReturnFields = PriceApplyReturnFields;
            qp.OrderBy = PriceApplyOrderBy;
            qp.Where = PriceApplyWhere;
            if (item.ApplyFormID != Decimal.Zero)
                qp.Where += " and pr.ApplyFormID = " + item.ApplyFormID;
            if (item.Applicant != null && item.Applicant != string.Empty)
                qp.Where += " and pr.Applicant = '" + item.Applicant + "' ";
            if (item.Status != 0 && item.Status != PriceApplyStatus.Approvaled)
                qp.Where += " and pr.Status = " + (int)item.Status + " ";
            if (item.Status != 0 && item.Status == PriceApplyStatus.Approvaled)
                qp.Where += " and pr.Status >= " + 3 + " ";
            return qp;
      

        }
        public QueryParam GeneratePriceApplySearchTerm(PriceApplyInfo item, string[] WFStates)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = " FM2E_PriceApply pr left join FM2E_WorkflowInstance h on pr.ApplyFormID = h.DataID left join FM2E_User b on pr.applicant=b.UserName left join FM2E_User c on pr.Approvaler=c.UserName";
            qp.ReturnFields = PriceApplyReturnFields;
            qp.OrderBy = PriceApplyOrderBy;
            qp.Where = PriceApplyWhere;
            if (item.ApplyFormID != 0)
                qp.Where += " and pr.ApplyFormID = " + item.ApplyFormID;
            if (item.Applicant != null && item.Applicant != string.Empty)
                qp.Where += " and pr.Applicant = '" + item.Applicant + "' ";
            if (item.Status != 0 && item.Status != PriceApplyStatus.Approvaled)
                qp.Where += " and pr.Status = " + (int)item.Status + " ";
            if (item.Status != 0 && item.Status == PriceApplyStatus.Approvaled)
                qp.Where += " and pr.Status >= " + 3 + " ";
            
            if (WFStates != null && WFStates.Length > 0)
            {
                qp.Where += "and h.TableName='FM2E_PriceApply' and (";
                bool first = true;
                foreach (string wfstate in WFStates)
                {
                    if (first)
                    {
                        qp.Where += "CurrentStateName='" + wfstate + "'";
                        first = false;
                    }
                    else
                        qp.Where += "or CurrentStateName='" + wfstate + "'";
                }
                qp.Where += ")";
            }
            else
            {
                qp.Where = "where 1=0";
            }
            return qp;
        }

        public IList GetPriceApplyList(QueryParam term, out int recordCount, string companyid)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = PriceApplyTableName;
                    term.ReturnFields = PriceApplyReturnFields;
                    term.OrderBy = PriceApplyOrderBy;
                    term.Where = PriceApplyWhere;
                }
                if (companyid != null && companyid != string.Empty)
                    term.Where += " and pr.CompanyID = '" + companyid + "' ";
                return SQLHelper.GetObjectList(this.GetPriceApplyData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException(" 获取申请审批分页失败", e);
            }
        }



        private PriceApplyInfo GetPriceApplyData(IDataReader dr)
        {
            PriceApplyInfo item = new PriceApplyInfo();
            if (!Convert.IsDBNull(dr["ApplyFormID"]))
                item.ApplyFormID = Convert.ToInt64(dr["ApplyFormID"]);
            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);
            if (!Convert.IsDBNull(dr["Applicant"]))
                item.Applicant = Convert.ToString(dr["Applicant"]);
            if (!Convert.IsDBNull(dr["Approvaler"]))
                item.Approvaler = Convert.ToString(dr["Approvaler"]);
            if (!Convert.IsDBNull(dr["ApplicantName"]))
                item.ApplicantName = Convert.ToString(dr["ApplicantName"]);
            if (!Convert.IsDBNull(dr["ApprovalerName"]))
                item.ApprovalerName = Convert.ToString(dr["ApprovalerName"]);
            if (!Convert.IsDBNull(dr["ApprovalDate"]))
                item.ApprovalDate = Convert.ToDateTime(dr["ApprovalDate"]);
            if (!Convert.IsDBNull(dr["ApplyDate"]))
                item.ApplyDate = Convert.ToDateTime(dr["ApplyDate"]);
            if (!Convert.IsDBNull(dr["Status"]))
                item.Status = (PriceApplyStatus)Convert.ToInt32(dr["Status"]);

            return item;

        }

        /// <summary>
        /// PriceApplyDetail
        /// </summary>

        private const string PriceApplyDetailTableName = " FM2E_PriceApplyDetail pr ";

        private const string PriceApplyDetailReturnFields = " pr.* ";

        private const string PriceApplyDetailOrderBy = " ";

        private const string PriceApplyDetailWhere = " where 1=1 ";

        //private const string INSERT_PRICEAPPLYDETAIL = "insert into FM2E_PriceApplyDetail()"



        public IList<PriceApplyDetailInfo> GetAllPriceApplyDetail()
        {
            throw new NotImplementedException();
        }


        public IList<PriceApplyDetailInfo> GetRecentPriceApplyDetail(int num)
        {
           throw new NotImplementedException();
        }

        public PriceApplyDetailInfo GetPriceApplyDetail(string id)
        {
           throw new NotImplementedException();
        }

        public void InsertPriceApplyDetail(PriceApplyDetailInfo item)
        {
            throw new NotImplementedException();
        }


        public void UpdatePriceApplyDetail(PriceApplyDetailInfo item)
        {
            throw new NotImplementedException();
        }
        public void DelPriceApplyDetail(PriceApplyDetailInfo item)
        {
            throw new NotImplementedException();
        }
        public IList<PriceApplyDetailInfo> SearchPriceApplyDetail(PriceApplyDetailInfo item)
        {
           throw new NotImplementedException();
        }

        public QueryParam GeneratePriceApplyDetailSearchTerm(PriceApplyDetailInfo item)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = PriceApplyDetailTableName;
            qp.ReturnFields = PriceApplyDetailReturnFields;
            qp.OrderBy = PriceApplyDetailOrderBy;
            qp.Where = PriceApplyDetailWhere;
            if (item.ApplyFormID != Decimal.Zero)
                qp.Where += " and pr.ApplyFormID = " + item.ApplyFormID;
            return qp;
        }

        public IList GetPriceApplyDetailList(QueryParam term, out int recordCount, string companyid)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = PriceApplyDetailTableName;
                    term.ReturnFields = PriceApplyDetailReturnFields;
                    term.OrderBy = PriceApplyDetailOrderBy;
                    term.Where = PriceApplyDetailWhere;
                }
                if (companyid != null && companyid != string.Empty)
                    term.Where += " and pr.CompanyID = '" + companyid + "' ";

                return SQLHelper.GetObjectList(this.GetPriceApplyDetailData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException(" 获取详细申请审批信息分页失败", e);
            }
        }

        private PriceApplyDetailInfo GetPriceApplyDetailData(IDataReader dr)
        {
            PriceApplyDetailInfo item = new PriceApplyDetailInfo();
            if (!Convert.IsDBNull(dr["ApplyFormID"]))
                item.ApplyFormID = Convert.ToInt64(dr["ApplyFormID"]);
            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);
            if (!Convert.IsDBNull(dr["ProductName"]))
                item.ProductName = Convert.ToString(dr["ProductName"]);
            if (!Convert.IsDBNull(dr["Model"]))
                item.Model = Convert.ToString(dr["Model"]);
            if (!Convert.IsDBNull(dr["StartTime"]))
                item.StartTime = Convert.ToDateTime(dr["StartTime"]);
            if (!Convert.IsDBNull(dr["OldUpperPrice"]))
                item.OldUpperPrice = Convert.ToDecimal(dr["OldUpperPrice"]);
            if (!Convert.IsDBNull(dr["OldLowerPrice"]))
                item.OldLowerPrice = Convert.ToDecimal(dr["OldLowerPrice"]);
            if (!Convert.IsDBNull(dr["NewUpperPrice"]))
                item.NewUpperPrice = Convert.ToDecimal(dr["NewUpperPrice"]);
            if (!Convert.IsDBNull(dr["NewLowerPrice"]))
                item.NewLowerPrice = Convert.ToDecimal(dr["NewLowerPrice"]);
            if (!Convert.IsDBNull(dr["Reason"]))
                item.Reason = Convert.ToString(dr["Reason"]);
            if (!Convert.IsDBNull(dr["Result"]))
                item.Result = Convert.ToInt32(dr["Result"]);
            if (!Convert.IsDBNull(dr["FeeBack"]))
                item.FeeBack = Convert.ToString(dr["FeeBack"]);
            if (!Convert.IsDBNull(dr["DeleteOld"]))
                item.DeleteOld = Convert.ToInt16(dr["DeleteOld"]);
            if (!Convert.IsDBNull(dr["Status"]))
                item.Status = Convert.ToString(dr["Status"]);
            if (!Convert.IsDBNull(dr["instanceId"]))
                item.instanceId = (Guid)(dr["instanceId"]);
            if (!Convert.IsDBNull(dr["Unit"]))
                item.Unit = Convert.ToString(dr["Unit"]);

            return item;

        }


        /// <summary>
        /// PriceDetail
        /// </summary>

        private const string PriceDetailTableName = " FM2E_RecommendPriceDetail re ";

        private const string PriceDetailReturnFields = " re.* ";

        private const string PriceDetailOrderBy = " order by StartTime desc ";

        private const string PriceDetailWhere = " where 1=1 ";


        //private const string GET_PRICEDETAIL = "select " + PriceDetailReturnFields + " from " + PriceDetailTableName + PriceDetailWhere; 

        public IList<PriceDetailInfo> GetAllPriceDetail()
        {
         throw new NotImplementedException();
        }


        public IList<PriceDetailInfo> GetRecentPriceDetail(int num)
        {
           throw new NotImplementedException();
        }

        public PriceDetailInfo GetPriceDetail(string id)
        {
            throw new NotImplementedException();
        }

        public void InsertPriceDetail(PriceDetailInfo item)
        {
            throw new NotImplementedException();
        }

        public void UpdatePriceDetail(PriceDetailInfo item)
        {

            throw new NotImplementedException();
        }
        public void DelPriceDetail(PriceDetailInfo item)
        {
            throw new NotImplementedException();
        }
        public IList<PriceDetailInfo> SearchPriceDetail(PriceDetailSearchInfo item)
        {
            string cmd = "select " + PriceDetailReturnFields + " from " + PriceDetailTableName + PriceDetailWhere;

            if (item.CompanyID != null && item.CompanyID != string.Empty)
                cmd += " and re.CompanyID = '" + item.CompanyID + "' ";
            if (item.ProductName != null && item.ProductName != string.Empty)
                cmd += " and re.ProductName = '" + item.ProductName + "' ";
            if (item.Model != null && item.Model != string.Empty)
                cmd += " and re.Model = '" + item.Model + "' ";
            if (DateTime.Compare(item.StartTime1, DateTime.MinValue) != 0)
                cmd += " and re.StartTime >= '" + item.StartTime1.ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.StartTime2, DateTime.MinValue) != 0)
                cmd += " and re.StartTime <= '" + item.StartTime2.AddDays(1).ToShortDateString() + " 00:00" + "' ";
            if (item.UpperPrice != Decimal.Zero)
                cmd += " and re.UpperPrice <= " + item.UpperPrice;
            if (item.LowerPrice != Decimal.Zero)
                cmd += " and re.LowerPrice >= " + item.LowerPrice;
            cmd += PriceDetailOrderBy;
            List<PriceDetailInfo> list = new List<PriceDetailInfo>();

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        list.Add(GetPriceDetailData(rd));
                }

            }
            catch (Exception e)
            {
                throw new DALException(" 搜索当前指导价格信息失败", e);
            }
            return list;
        }

        public QueryParam GeneratePriceDetailSearchTerm(PriceDetailSearchInfo item)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = PriceDetailTableName;
            qp.ReturnFields = PriceDetailReturnFields;
            qp.OrderBy = PriceDetailOrderBy;
            qp.Where = PriceDetailWhere;
            if (item.CompanyID != null && item.CompanyID != string.Empty)
                qp.Where += " and re.CompanyID = '" + item.CompanyID + "' ";
            if (item.ProductName != null && item.ProductName != string.Empty)
                qp.Where += " and re.ProductName like '%" + item.ProductName + "%' ";
            if (item.Model != null && item.Model != string.Empty)
                qp.Where += " and re.Model like '%" + item.Model + "%' ";
            if (DateTime.Compare(item.StartTime1, DateTime.MinValue) != 0)
                qp.Where += " and re.StartTime >= '" + item.StartTime1.ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.StartTime2, DateTime.MinValue) != 0)
                qp.Where += " and re.StartTime < '" + item.StartTime2.AddDays(1).ToShortDateString() + " 00:00" + "' ";

            if (item.LowerPrice != Decimal.MinValue)
                qp.Where += " and re.UpperPrice >= " + item.LowerPrice;

            if (item.UpperPrice != Decimal.MaxValue)
                qp.Where += " and re.LowerPrice <= " + item.UpperPrice;  
            

            return qp;

        }

        public IList GetPriceDetailList(QueryParam term, out int recordCount, string companyid)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = PriceDetailTableName;
                    term.ReturnFields = PriceDetailReturnFields;
                    term.OrderBy = PriceDetailOrderBy;
                    term.Where = PriceDetailWhere;
                }
                if (companyid != null && companyid != string.Empty)
                    term.Where += " and re.CompanyID = '" + companyid + "' ";
                return SQLHelper.GetObjectList(this.GetPriceDetailData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException(" 获取当前指导价格分页失败", e);
            }
        }
        private PriceDetailInfo GetPriceDetailData(IDataReader dr)
        {
            PriceDetailInfo item = new PriceDetailInfo();
            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);
            if (!Convert.IsDBNull(dr["ProductName"]))
                item.ProductName = Convert.ToString(dr["ProductName"]);
            if (!Convert.IsDBNull(dr["Model"]))
                item.Model = Convert.ToString(dr["Model"]);
            if (!Convert.IsDBNull(dr["StartTime"]))
                item.StartTime = Convert.ToDateTime(dr["StartTime"]);
            if (!Convert.IsDBNull(dr["UpperPrice"]))
                item.UpperPrice = Convert.ToDecimal(dr["UpperPrice"]);
            if (!Convert.IsDBNull(dr["LowerPrice"]))
                item.LowerPrice = Convert.ToDecimal(dr["LowerPrice"]);
            if (!Convert.IsDBNull(dr["Unit"]))
                item.Unit = Convert.ToString(dr["Unit"]);
            return item;

        }

        /// <summary>
        /// PriceHistory
        /// </summary>


        private const string PriceHistoryTableName = " FM2E_RecommendPriceHistory re ";

        private const string PriceHistoryReturnFields = " re.* ";

        private const string PriceHistoryOrderBy = " order by EndTime desc ";

        private const string PriceHistoryWhere = " where 1=1 ";



        public IList<PriceHistoryInfo> GetAllPriceHistory()
        {
            throw new NotImplementedException("历史价格获取所有功能未实现");
        }


        public IList<PriceHistoryInfo> GetRecentPriceHistory(int num)
        {
            throw new NotImplementedException("历史价格获取最近功能未实现");
        }

        public PriceHistoryInfo GetPriceHistory(string id)
        {
            throw new NotImplementedException("历史价格获取功能未实现");
        }

        public void InsertPriceHistory(PriceHistoryInfo item)
        {
            throw new NotImplementedException("历史价格添加功能未实现");
        }



        public void UpdatePriceHistory(PriceHistoryInfo item)
        {
            throw new NotImplementedException("历史价格更新功能未实现");
        }
        public void DelPriceHistory(PriceHistoryInfo item)
        {
            throw new NotImplementedException("历史价格删除功能未实现");
        }
        public IList<PriceHistoryInfo> SearchPriceHistory(PriceHistorySearchInfo item)
        {
            throw new NotImplementedException("历史价格查询功能未实现");
        }

        public QueryParam GeneratePriceHistorySearchTerm(PriceHistorySearchInfo item)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = PriceHistoryTableName;
            qp.ReturnFields = PriceHistoryReturnFields;
            qp.OrderBy = PriceHistoryOrderBy;
            qp.Where = PriceHistoryWhere;
            if (item.CompanyID != null && item.CompanyID != string.Empty)
                qp.Where += " and re.CompanyID = '" + item.CompanyID + "' ";
            if (item.ProductName != null && item.ProductName != string.Empty)
                qp.Where += " and re.ProductName like '%" + item.ProductName + "%' ";
            if (item.Model != null && item.Model != string.Empty)
                qp.Where += " and re.Model = '" + item.Model + "' ";
            if (DateTime.Compare(item.StartTime1, DateTime.MinValue) != 0)
                qp.Where += " and re.StartTime >= '" + item.StartTime1.ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.StartTime2, DateTime.MinValue) != 0)
                qp.Where += " and re.StartTime <= '" + item.StartTime2.ToShortDateString() + " 23:59" + "' ";
            if (DateTime.Compare(item.EndTime1, DateTime.MinValue) != 0)
                qp.Where += " and re.EndTime >= '" + item.EndTime1.ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.EndTime2, DateTime.MinValue) != 0)
                qp.Where += " and re.EndTime <= '" + item.EndTime2.ToShortDateString() + " 23:59" + "' ";
            if (item.UpperPrice != Decimal.Zero)
                qp.Where += " and re.UpperPrice <= " + item.UpperPrice;
            if (item.LowerPrice != Decimal.Zero)
                qp.Where += " and re.LowerPrice >= " + item.LowerPrice;
            return qp;
        }

        public IList GetPriceHistoryList(QueryParam term, out int recordCount, string companyid)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = PriceHistoryTableName;
                    term.ReturnFields = PriceHistoryReturnFields;
                    term.OrderBy = PriceHistoryOrderBy;
                    term.Where = PriceHistoryWhere;
                }
                if (companyid != null && companyid != string.Empty)
                    term.Where += " and re.CompanyID = '" + companyid + "' ";
                return SQLHelper.GetObjectList(this.GetPriceHistoryData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException(" 获取历史指导价格分页失败", e);
            }
        }

        private PriceHistoryInfo GetPriceHistoryData(IDataReader dr)
        {
            PriceHistoryInfo item = new PriceHistoryInfo();
            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);
            if (!Convert.IsDBNull(dr["ProductName"]))
                item.ProductName = Convert.ToString(dr["ProductName"]);
            if (!Convert.IsDBNull(dr["Model"]))
                item.Model = Convert.ToString(dr["Model"]);
            if (!Convert.IsDBNull(dr["StartTime"]))
                item.StartTime = Convert.ToDateTime(dr["StartTime"]);
            if (!Convert.IsDBNull(dr["EndTime"]))
                item.EndTime = Convert.ToDateTime(dr["EndTime"]);
            if (!Convert.IsDBNull(dr["UpperPrice"]))
                item.UpperPrice = Convert.ToDecimal(dr["UpperPrice"]);
            if (!Convert.IsDBNull(dr["LowerPrice"]))
                item.LowerPrice = Convert.ToDecimal(dr["LowerPrice"]);
            if (!Convert.IsDBNull(dr["Unit"]))
                item.Unit = Convert.ToString(dr["Unit"]);
            return item;

        }


        private const string PurchasePriceHistoryTableName = "FM2E_PriceHistory ";
        private const string PurchasePriceHistoryReturnFields = " * ";
        private const string PurchasePriceHistoryOrderBy = " order by PurchaseTime desc ";
        private const string PurchasePriceHistoryWhere = " where 1=1 ";


        /// <summary>
        /// 历史购买价格记录添加，By zjf 2009-02-05 
        /// </summary>
        /// <param name="item"></param>
        public void InsertPricePurchaseHistory(PricePurchaseHistoryInfo item)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_PriceHistory(");
            strSql.Append("CompanyID,ProductName,Model,PurchaseTime,ActualPrice,Unit,Supplier)");
            strSql.Append(" values (");
            strSql.Append("@CompanyID,@ProductName,@Model,@PurchaseTime,@ActualPrice,@Unit,@Supplier)");
            strSql.Append(";");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@Model", SqlDbType.NVarChar,20),
					new SqlParameter("@PurchaseTime", SqlDbType.DateTime),
					new SqlParameter("@ActualPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@Unit",SqlDbType.NVarChar,5),
					new SqlParameter("@Supplier", SqlDbType.NVarChar,50)};
            parameters[0].Value = item.CompanyID;
            parameters[1].Value = item.ProductName == null ? SqlString.Null : item.ProductName;
            parameters[2].Value = item.Model == null ? SqlString.Null : item.Model;
            parameters[3].Value = item.PurchaseTime == DateTime.MinValue ? SqlDateTime.Null : item.PurchaseTime;
            parameters[4].Value = item.ActualPrice;
            parameters[5].Value = item.Unit == null ? SqlString.Null : item.Unit;
            parameters[6].Value = item.Supplier == null ? SqlString.Null : item.Supplier;

            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>shi
        /// 生成查询历史购买价格条件By hmz 2009-02-10
        /// </summary>
        /// <param name="item">购买历史实体</param>
        public QueryParam GeneratePurchasePriceHistorySearchTerm(PricePurchaseHistoryInfo item)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = PurchasePriceHistoryTableName;
            qp.ReturnFields = PurchasePriceHistoryReturnFields;
            qp.OrderBy = PurchasePriceHistoryOrderBy;
            qp.Where = PurchasePriceHistoryWhere;
            if (item.CompanyID != string.Empty)
                qp.Where += " and CompanyID = '" + item.CompanyID + "' ";
            if (item.ProductName != string.Empty)
                qp.Where += " and ProductName = '" + item.ProductName + "' ";
            if (item.Model != string.Empty)
                qp.Where += " and Model = '" + item.Model + "' ";
            return qp;

        }
        /// <summary>
        /// 获取历史购买价格
        /// </summary>
        /// <param name="term">查询条件</param>
        /// <param name="recordCount">记录数</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public IList GetPurchasePriceHistoryList(QueryParam term, out int recordCount, string companyid)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = PurchasePriceHistoryTableName;
                    term.ReturnFields = PurchasePriceHistoryReturnFields;
                    term.OrderBy = PurchasePriceHistoryOrderBy;
                    if (companyid != null && companyid != string.Empty)
                        term.Where = PurchasePriceHistoryWhere + " and CompanyID = '" + companyid + "' ";
                    else
                        term.Where = PurchasePriceHistoryWhere;
                }
                return SQLHelper.GetObjectList(this.GetPurchasePriceHistoryData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException(" 获取历史购买价格分页失败", e);
            }
        }

        private PricePurchaseHistoryInfo GetPurchasePriceHistoryData(IDataReader dr)
        {
            PricePurchaseHistoryInfo item = new PricePurchaseHistoryInfo();
            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);
            if (!Convert.IsDBNull(dr["ProductName"]))
                item.ProductName = Convert.ToString(dr["ProductName"]);
            if (!Convert.IsDBNull(dr["Model"]))
                item.Model = Convert.ToString(dr["Model"]);
            if (!Convert.IsDBNull(dr["PurchaseTime"]))
                item.PurchaseTime = Convert.ToDateTime(dr["PurchaseTime"]);
            if (!Convert.IsDBNull(dr["ActualPrice"]))
                item.ActualPrice = Convert.ToDecimal(dr["ActualPrice"]);
            if (!Convert.IsDBNull(dr["Unit"]))
                item.Unit = Convert.ToString(dr["Unit"]);
            if (!Convert.IsDBNull(dr["Supplier"]))
                item.Supplier = Convert.ToString(dr["Supplier"]);

            return item;
        }

        /// <summary>
        /// 生成表单查询参数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam IPriceManager.GeneratePriceApplySearchTermEx(PriceApplySearchInfo item)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = " FM2E_PriceApply pr left join FM2E_WorkflowInstance h on pr.ApplyFormID = h.DataID left join FM2E_User b on pr.applicant=b.UserName left join FM2E_User c on pr.Approvaler=c.UserName " +
                " left join FM2E_PriceApplyDetail d on pr.ApplyFormID = d.ApplyFormID ";
            qp.ReturnFields = PriceApplyReturnFields;
            qp.OrderBy = PriceApplyOrderBy;
            qp.Where = PriceApplyWhere;

            
            if (item.ApplyFormID != 0)
                qp.Where += " and pr.ApplyFormID = " + item.ApplyFormID;

            if (item.Applicant != null && item.Applicant != string.Empty)
                qp.Where += " and b.PersonName like '%" + item.Applicant + "%' ";

            if (item.ApplyTimeLower != DateTime.MinValue)
            {
                qp.Where += " and pr.ApplyDate>='" + item.ApplyTimeLower.ToShortDateString() + " 00:00' ";
            }

            if (item.ApplyTimeUpper != DateTime.MaxValue)
            {
                qp.Where += " and pr.ApplyDate<'" + item.ApplyTimeUpper.AddDays(1).ToShortDateString() + " 00:00' ";
            }

            if (item.ApprovalTimeLower != DateTime.MinValue)
            {
                qp.Where += " and pr.ApprovalDate>='" + item.ApprovalTimeLower.ToShortDateString() + " 00:00' ";
            }

            if (item.ApprovalTimeUpper != DateTime.MaxValue)
            {
                qp.Where += " and pr.ApprovalDate<'" + item.ApprovalTimeUpper.AddDays(1).ToShortDateString() + " 00:00' ";
            }
            if (item.CompanyID != null && item.CompanyID != "")
            {
                qp.Where += " and pr.[CompanyID] = '" + item.CompanyID + "'";
            }

            if (item.Approvaler != null && item.Approvaler != string.Empty)
                qp.Where += " and c.PersonName like '%" + item.Approvaler + "%' ";

            if (item.ProductName != null && item.ProductName != string.Empty)
                qp.Where += " and d.ProductName like '%" + item.ProductName + "%' ";

            if (item.Model != null && item.Model != string.Empty)
                qp.Where += " and d.Model like '%" + item.Model + "%' ";

            if (item.StatusList != null && item.StatusList.Count > 0)
            {
                for (int i = 0; i < item.StatusList.Count; i++)
                {
                    if (i == 0)
                    {
                        qp.Where += " and ( ";
                        qp.Where += " " + " pr.Status=" + (int)item.StatusList[i] + " ";
                    }
                    else
                    {
                        qp.Where += " or " + " pr.Status=" + (int)item.StatusList[i] + " ";
                    }
                    if (i == item.StatusList.Count - 1)
                    {
                        qp.Where += " ) ";
                    }
                }
            }


            if (item.WFStatusList != null && item.WFStatusList.Count > 0)
            {
                qp.Where += "and h.TableName='FM2E_PriceApply' and (";
                bool first = true;
                foreach (string wfstate in item.WFStatusList)
                {
                    if (first)
                    {
                        qp.Where += "CurrentStateName='" + wfstate + "'";
                        first = false;
                    }
                    else
                        qp.Where += "or CurrentStateName='" + wfstate + "'";
                }
                qp.Where += ")";
            }
            return qp;
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        IList IPriceManager.SearchPriceApplyForm(QueryParam qp, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectListWithDistinct(this.GetPriceApplyData, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取价格管理申请单分页失败", e);
            }
        }
    }
}
