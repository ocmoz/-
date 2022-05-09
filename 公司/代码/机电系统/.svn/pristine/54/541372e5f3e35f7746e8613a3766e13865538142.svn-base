using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using FM2E.IDAL.Equipment;
using FM2E.Model.Equipment;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Utils;
using System.Collections;
using FM2E.Model.Exceptions;
using System.Data.SqlTypes;

namespace FM2E.SQLServerDAL.Equipment
{
    public class OutWarehouseApply : IOutWarehouseApply
    {
        #region 表、视图名称
        private const string TABLE_OUTWAREHOUSE_APPLY = "FM2E_OutWarehouseApply";
        private const string VIEW_OUTWAREHOUSE_APPLY = "FM2E_OutWarehouseApplyView";
        private const string TABLE_OUTWAREHOUSE_DETAIL = "FM2E_OutWarehouseDetail";
        private const string VIEW_OUTWAREHOUSE_DETAIL = "FM2E_OutWarehouseDetailView";
        private const string TABLE_OUTWAREHOUSE_APPROVAL = "FM2E_OutWarehouseApproval";
        private const string VIEW_OUTWAREHOUSE_APPROVAL = "FM2E_OutWarehouseApprovalView";
        private const string TABLE_OUT_EQUIPMENT = "FM2E_OutEquipments";
        private const string VIEW_OUT_EQUIPMENT = "FM2E_OutEquipmentView";
        #endregion

        #region 原来的代码，暂时注释
        //public QueryParam GenerateSearchTerm(OutWarehouseApplyInfo item)
        //{
        //    string sqlSearch = "where 1=1";
        //    if (item.SheetName != "")
        //        sqlSearch += " and a.SheetName like '%" + item.SheetName + "%'";
        //    if (item.WarehouseID != "")
        //        sqlSearch += " and a.WarehouseID ='" + item.WarehouseID + "'";
        //    if (item.CompanyID != "")
        //        sqlSearch += " and a.CompanyID ='" + item.CompanyID + "'";
        //    if (item.Applicant != "")
        //        sqlSearch += " and a.Applicant = '" + item.Applicant + "'";
        //    if (item.ApplicantName != "")
        //        sqlSearch += " and b.PersonName like '%" + item.ApplicantName + "%'";
        //    if (item.ApprovalerName != "")
        //        sqlSearch += " and c.PersonName like '%" + item.ApprovalerName + "%'";
        //    if (item.Status != 0)
        //        sqlSearch += " and a.Status ='" + (int)item.Status + "'";
        //    if (item.StatusArray != null && item.StatusArray.Count > 0)
        //    {
        //        for (int i = 0; i < item.StatusArray.Count; i++)
        //        {
        //            if (i == 0)
        //            {
        //                sqlSearch += " and ( ";
        //                sqlSearch += " " + " a.Status=" + (int)item.StatusArray[i] + " ";
        //            }
        //            else
        //            {
        //                sqlSearch += " or " + " a.Status=" + (int)item.StatusArray[i] + " ";
        //            }
        //            if (i == item.StatusArray.Count - 1)
        //            {
        //                sqlSearch += " ) ";
        //            }
        //        }
        //    }
        //    sqlSearch += " and a.IsDeleted=0";
        //    QueryParam searchTerm = new QueryParam();
        //    searchTerm.TableName = "FM2E_OutWarehouseApply a left join FM2E_User b on a.applicant=b.UserName left join FM2E_User c on a.Approvaler=c.UserName left join FM2E_User d on a.Receiver=d.UserName left join FM2E_User e on a.Operator=e.UserName left join FM2E_Warehouse f on a.warehouseID = f.warehouseID left join FM2E_Company g on a.CompanyID = g.CompanyID";
        //    searchTerm.ReturnFields = "a.*,b.PersonName as ApplicantName,c.PersonName as ApprovalerName,d.PersonName as ReceiverName,e.PersonName as OperatorName,f.[name] as WarehouseName,g.CompanyName as CompanyName";
        //    searchTerm.PageSize = 10;
        //    searchTerm.OrderBy = "order by ApplyTime desc";
        //    searchTerm.Where = sqlSearch;
        //    return searchTerm;
        //}
        //public QueryParam GenerateSearchTerm(OutWarehouseApplyInfo item,string []WFStates)
        //{
        //    string sqlSearch = "where 1=1";
        //    if (item.SheetName != "")
        //        sqlSearch += " and a.SheetName like '%" + item.SheetName + "%'";
        //    if (item.WarehouseID != "")
        //        sqlSearch += " and a.WarehouseID ='" + item.WarehouseID + "'";
        //    if (item.CompanyID != "")
        //        sqlSearch += " and a.CompanyID ='" + item.CompanyID + "'";
        //    if (item.Applicant != "")
        //        sqlSearch += " and a.Applicant = '" + item.Applicant + "'";
        //    if (item.ApplicantName != "")
        //        sqlSearch += " and b.PersonName like '%" + item.ApplicantName + "%'";
        //    if (item.ApprovalerName != "")
        //        sqlSearch += " and c.PersonName like '%" + item.ApprovalerName + "%'";
        //    if (item.Status != 0)
        //        sqlSearch += " and a.Status ='" + (int)item.Status + "'";
        //    sqlSearch += " and a.IsDeleted=0";
        //    if (WFStates != null && WFStates.Length > 0)
        //    {
        //        sqlSearch += "and h.TableName='FM2E_OutWarehouseApply' and (";
        //        bool first = true;
        //        foreach (string wfstate in WFStates)
        //        {
        //            if (first)
        //            {
        //                sqlSearch += "CurrentStateName='" + wfstate + "'";
        //                first = false;
        //            }
        //            else
        //                sqlSearch += "or CurrentStateName='" + wfstate + "'";
        //        }
        //        sqlSearch += ")";
        //    }
        //    else
        //    {
        //        sqlSearch = "where 1=0";
        //    }
        //    QueryParam searchTerm = new QueryParam();
        //    searchTerm.TableName = "FM2E_OutWarehouseApply a left join FM2E_User b on a.applicant=b.UserName left join FM2E_User c on a.Approvaler=c.UserName left join FM2E_User d on a.Receiver=d.UserName left join FM2E_User e on a.Operator=e.UserName left join FM2E_Warehouse f on a.warehouseID = f.warehouseID left join FM2E_Company g on a.CompanyID = g.CompanyID left join FM2E_WorkflowInstance h on a.ID=h.DataID";
        //    searchTerm.ReturnFields = "a.*,b.PersonName as ApplicantName,c.PersonName as ApprovalerName,d.PersonName as ReceiverName,e.PersonName as OperatorName,f.[name] as WarehouseName,g.CompanyName as CompanyName";
        //    searchTerm.PageSize = 10;
        //    searchTerm.OrderBy = "order by ApplyTime desc";
        //    searchTerm.Where = sqlSearch;
        //    return searchTerm;
        //}
        //public IList GetList(QueryParam searchTerm, out int recordCount)
        //{
        //    if (searchTerm.Where == "")
        //    {
        //        searchTerm.TableName = "FM2E_OutWarehouseApply a left join FM2E_User b on a.applicant=b.UserName left join FM2E_User c on a.Approvaler=c.UserName left join FM2E_User d on a.Receiver=d.UserName left join FM2E_User e on a.Operator=e.UserName left join FM2E_Warehouse f on a.warehouseID = f.warehouseID left join FM2E_Company g on a.CompanyID = g.CompanyID";
        //        searchTerm.ReturnFields = "a.*,b.PersonName as ApplicantName,c.PersonName as ApprovalerName,d.PersonName as ReceiverName,e.PersonName as OperatorName,f.[name] as WarehouseName,g.CompanyName as CompanyName";
        //        searchTerm.PageSize = 10;
        //        searchTerm.OrderBy = "order by ApplyTime desc";
        //        searchTerm.Where = "where a.IsDeleted=0";
        //    }
        //    return SQLHelper.GetObjectList(this.GetData, searchTerm, out recordCount);
        //}
        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public long InsertOutWarehouseApply(OutWarehouseApplyInfo model)
        //{
        //    SqlConnection conn = null;
        //    SqlTransaction trans = null;
        //    long id = 0;
        //    try
        //    {
        //        //建立连接，开始事务
        //        conn = new SqlConnection(SQLHelper.ConnectionString);
        //        conn.Open();
        //        trans = conn.BeginTransaction();

        //        //先插入申请信息
        //        id = insertApply(trans, model);

        //        //插入申请明细信息
        //        if (model.ApplyDetailList != null)
        //        {
        //            foreach (OutWarehouseDetailInfo item in model.ApplyDetailList)
        //            {
        //                item.ID = id;
        //                InsertOutWarehouseDetail(trans, item);
        //            }
        //        }
        //        //事务提交
        //        trans.Commit();
        //        return id;
        //    }
        //    catch (SqlException sqlex)
        //    {
        //        //回滚
        //        trans.Rollback();
        //        throw sqlex;
        //    }
        //    catch (Exception ex)
        //    {
        //        //回滚
        //        trans.Rollback();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        //关闭连接
        //        if (trans != null)
        //        {
        //            trans.Dispose();
        //            trans = null;
        //        }
        //        if (conn != null)
        //        {
        //            conn.Close();
        //            conn.Dispose();
        //            conn = null;
        //        }
        //    }
        //    return id;
        //}
        //private long insertApply(SqlTransaction trans, OutWarehouseApplyInfo model)
        //{
        //    long id = 1;
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("insert into FM2E_OutWarehouseApply(");
        //    strSql.Append("ApprovalTime,Status,FeedBack,OutTime,Receiver,Operator,OutWarehouseRemark,IsDeleted,SheetName,WarehouseID,ApplyTime,ApplyRemark,CompanyID,Applicant,Approvaler)");
        //    strSql.Append(" values (");
        //    strSql.Append("@ApprovalTime,@Status,@FeedBack,@OutTime,@Receiver,@Operator,@OutWarehouseRemark,@IsDeleted,@SheetName,@WarehouseID,@ApplyTime,@ApplyRemark,@CompanyID,@Applicant,@Approvaler)");
        //    strSql.Append(";select @@IDENTITY");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ApprovalTime", SqlDbType.DateTime),
        //            new SqlParameter("@Status", SqlDbType.TinyInt,1),
        //            new SqlParameter("@FeedBack", SqlDbType.NVarChar,50),
        //            new SqlParameter("@OutTime", SqlDbType.DateTime),
        //            new SqlParameter("@Receiver", SqlDbType.VarChar,20),
        //            new SqlParameter("@Operator", SqlDbType.VarChar,20),
        //            new SqlParameter("@OutWarehouseRemark", SqlDbType.NVarChar,50),
        //            new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
        //            new SqlParameter("@SheetName", SqlDbType.NVarChar,20),
        //            new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
        //            new SqlParameter("@ApplyTime", SqlDbType.DateTime),
        //            new SqlParameter("@ApplyRemark", SqlDbType.NVarChar,50),
        //            new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
        //            new SqlParameter("@Applicant", SqlDbType.VarChar,20),
        //            new SqlParameter("@Approvaler", SqlDbType.VarChar,20)};
        //    parameters[0].Value = model.ApprovalTime;
        //    parameters[1].Value = (int)model.Status;
        //    parameters[2].Value = model.FeedBack;
        //    parameters[3].Value = model.OutTime;
        //    parameters[4].Value = model.Receiver;
        //    parameters[5].Value = model.Operator;
        //    parameters[6].Value = model.OutWarehouseRemark;
        //    parameters[7].Value = model.IsDeleted;
        //    parameters[8].Value = model.SheetName;
        //    parameters[9].Value = model.WarehouseID;
        //    parameters[10].Value = model.ApplyTime;
        //    parameters[11].Value = model.ApplyRemark;
        //    parameters[12].Value = model.CompanyID;
        //    parameters[13].Value = model.Applicant;
        //    parameters[14].Value = model.Approvaler;

        //    SqlDataReader rdr = null;
        //    try
        //    {
        //        using (rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
        //        {
        //            while (rdr.Read())
        //            {
        //                if (!Convert.IsDBNull(rdr[0]))
        //                    id = Convert.ToInt64(rdr[0]);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new DALException("添加设备出库申请信息失败", e);
        //    }
        //    finally
        //    {
        //        rdr.Close();
        //    }
        //    return id;
        //}
        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        //private void InsertOutWarehouseDetail(SqlTransaction trans, OutWarehouseDetailInfo model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("insert into FM2E_OutWarehouseDetail(");
        //    strSql.Append("ID,ProductName,Model,Count,Unit,Usage,Remark,SystemID,SectionID,LocationID,LocationTag,DetailLocation)");
        //    strSql.Append(" values (");
        //    strSql.Append("@ID,@ProductName,@Model,@Count,@Unit,@Usage,@Remark,@SystemID,@SectionID,@LocationID,@LocationTag,@DetailLocation)");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID", SqlDbType.BigInt,8),
        //            new SqlParameter("@ItemID", SqlDbType.BigInt,8),
        //            new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
        //            new SqlParameter("@Model", SqlDbType.NVarChar,20),
        //            new SqlParameter("@Count", SqlDbType.Decimal,9),
        //            new SqlParameter("@Unit", SqlDbType.NVarChar,5),
        //            new SqlParameter("@Usage", SqlDbType.NVarChar,50),
        //            new SqlParameter("@Remark", SqlDbType.NVarChar,50),
        //            new SqlParameter("@SystemID", SqlDbType.VarChar,1),                    
        //            new SqlParameter("@SectionID", SqlDbType.VarChar,2),                    
        //            new SqlParameter("@LocationID", SqlDbType.VarChar,6),                    
        //            new SqlParameter("@LocationTag", SqlDbType.VarChar,1),
        //            new SqlParameter("@DetailLocation", SqlDbType.NVarChar,100)};
        //    parameters[0].Value = model.ID;
        //    parameters[1].Value = model.ItemID;
        //    parameters[2].Value = model.ProductName;
        //    parameters[3].Value = model.Model;
        //    parameters[4].Value = model.Count;
        //    parameters[5].Value = model.Unit;
        //    parameters[6].Value = model.Usage;
        //    parameters[7].Value = model.Remark;
        //    parameters[8].Value = model.SystemID;
        //    parameters[9].Value = model.SectionID;
        //    parameters[10].Value = model.LocationID;
        //    parameters[11].Value = model.LocationTag;
        //    parameters[12].Value = model.DetailLocation;

        //    using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
        //    {
        //        conn.Open();
        //        try
        //        {
        //            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception e)
        //        {
        //            throw new DALException("更新出库申请明细信息失败", e);
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
        //    }
        //}
        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public void UpdateOutWarehouseApply(OutWarehouseApplyInfo model)
        //{
        //    SqlConnection conn = null;
        //    SqlTransaction trans = null;
        //    try
        //    {
        //        //建立连接，开始事务
        //        conn = new SqlConnection(SQLHelper.ConnectionString);
        //        conn.Open();
        //        trans = conn.BeginTransaction();

        //        //先更新申请信息
        //        UpdateOutWarehouseApply(trans, model);

        //        //先删除原来的明细，后添加新的明细
        //        StringBuilder delSql = new StringBuilder();
        //        delSql.AppendFormat("delete FM2E_OutWarehouseDetail");
        //        delSql.Append(" where ID=@ID ");
        //        SqlParameter[] delPara = {
        //            new SqlParameter("@ID", SqlDbType.BigInt)};
        //        delPara[0].Value = model.ID;
        //        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, delSql.ToString(), delPara);

        //        //插入申请明细信息
        //        if (model.ApplyDetailList != null)
        //        {
        //            foreach (OutWarehouseDetailInfo item in model.ApplyDetailList)
        //            {
        //                item.ID = model.ID;
        //                InsertOutWarehouseDetail(trans, item);
        //            }
        //        }
        //        //事务提交
        //        trans.Commit();
        //    }
        //    catch (SqlException sqlex)
        //    {
        //        //回滚
        //        trans.Rollback();
        //        throw sqlex;
        //    }
        //    catch (Exception ex)
        //    {
        //        //回滚
        //        trans.Rollback();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        //关闭连接
        //        if (trans != null)
        //        {
        //            trans.Dispose();
        //            trans = null;
        //        }
        //        if (conn != null)
        //        {
        //            conn.Close();
        //            conn.Dispose();
        //            conn = null;
        //        }
        //    }
        //}
        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public void UpdateOutWarehouseApply(SqlTransaction trans, OutWarehouseApplyInfo model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("update FM2E_OutWarehouseApply set ");
        //    strSql.Append("ApprovalTime=@ApprovalTime,");
        //    strSql.Append("Status=@Status,");
        //    strSql.Append("FeedBack=@FeedBack,");
        //    strSql.Append("OutTime=@OutTime,");
        //    strSql.Append("Receiver=@Receiver,");
        //    strSql.Append("Operator=@Operator,");
        //    strSql.Append("OutWarehouseRemark=@OutWarehouseRemark,");
        //    strSql.Append("IsDeleted=@IsDeleted,");
        //    strSql.Append("SheetName=@SheetName,");
        //    strSql.Append("WarehouseID=@WarehouseID,");
        //    strSql.Append("ApplyTime=@ApplyTime,");
        //    strSql.Append("ApplyRemark=@ApplyRemark,");
        //    strSql.Append("CompanyID=@CompanyID,");
        //    strSql.Append("Applicant=@Applicant,");
        //    strSql.Append("Approvaler=@Approvaler");
        //    strSql.Append(" where ID=@ID ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID", SqlDbType.BigInt,8),
        //            new SqlParameter("@ApprovalTime", SqlDbType.DateTime),
        //            new SqlParameter("@Status", SqlDbType.TinyInt,1),
        //            new SqlParameter("@FeedBack", SqlDbType.NVarChar,50),
        //            new SqlParameter("@OutTime", SqlDbType.DateTime),
        //            new SqlParameter("@Receiver", SqlDbType.VarChar,20),
        //            new SqlParameter("@Operator", SqlDbType.VarChar,20),
        //            new SqlParameter("@OutWarehouseRemark", SqlDbType.NVarChar,50),
        //            new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
        //            new SqlParameter("@SheetName", SqlDbType.NVarChar,20),
        //            new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
        //            new SqlParameter("@ApplyTime", SqlDbType.DateTime),
        //            new SqlParameter("@ApplyRemark", SqlDbType.NVarChar,50),
        //            new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
        //            new SqlParameter("@Applicant", SqlDbType.VarChar,20),
        //            new SqlParameter("@Approvaler", SqlDbType.VarChar,20)};
        //    parameters[0].Value = model.ID;
        //    parameters[1].Value = model.ApprovalTime;
        //    parameters[2].Value = (int)model.Status;
        //    parameters[3].Value = model.FeedBack;
        //    parameters[4].Value = model.OutTime;
        //    parameters[5].Value = model.Receiver;
        //    parameters[6].Value = model.Operator;
        //    parameters[7].Value = model.OutWarehouseRemark;
        //    parameters[8].Value = model.IsDeleted;
        //    parameters[9].Value = model.SheetName;
        //    parameters[10].Value = model.WarehouseID;
        //    parameters[11].Value = model.ApplyTime;
        //    parameters[12].Value = model.ApplyRemark;
        //    parameters[13].Value = model.CompanyID;
        //    parameters[14].Value = model.Applicant;
        //    parameters[15].Value = model.Approvaler;
        //    try
        //    {
        //        int result = SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        //        //if (result == 0)
        //        //    throw new Exception("没有更新任何数据项");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new DALException("更新出库申请信息失败", e);
        //    }
        //}
        ///// <summary>
        ///// 删除一条数据
        ///// </summary>
        //public void DelOutWarehouseApply(long ID)
        //{
        //    SqlConnection conn = null;
        //    SqlTransaction trans = null;
        //    try
        //    {
        //        //建立连接，开始事务
        //        conn = new SqlConnection(SQLHelper.ConnectionString);
        //        conn.Open();
        //        trans = conn.BeginTransaction();

        //        OutWarehouseApplyInfo item = GetOutWarehouseApply(ID);

        //        //删除申请明细信息
        //        DelOutWarehouseDetail(trans, item.ID);

        //        //删除申请信息
        //        DelApply(trans, ID);

        //        //事务提交
        //        trans.Commit();
        //    }
        //    catch (SqlException sqlex)
        //    {
        //        //回滚
        //        trans.Rollback();
        //        throw sqlex;
        //    }
        //    catch (Exception ex)
        //    {
        //        //回滚
        //        trans.Rollback();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        //关闭连接
        //        if (trans != null)
        //        {
        //            trans.Dispose();
        //            trans = null;
        //        }
        //        if (conn != null)
        //        {
        //            conn.Close();
        //            conn.Dispose();
        //            conn = null;
        //        }
        //    }

        //}
        ///// <summary>
        ///// 删除申请信息
        ///// </summary>
        ///// <param name="trans"></param>
        ///// <param name="ID"></param>
        //private void DelApply(SqlTransaction trans, long ID)
        //{
        //    try
        //    {
        //        StringBuilder strSql = new StringBuilder();
        //        strSql.Append("update FM2E_OutWarehouseApply set IsDeleted=1");
        //        strSql.Append(" where ID=@ID ");
        //        SqlParameter[] parameters = {
        //            new SqlParameter("@ID", SqlDbType.BigInt)};
        //        parameters[0].Value = ID;
        //        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new DALException("删除出库申请信息失败", e);
        //    }
        //}
        ///// <summary>
        ///// 删除所有申请明细
        ///// </summary>
        //private void DelOutWarehouseDetail(SqlTransaction trans, long ID)
        //{
        //    try
        //    {
        //        StringBuilder strSql = new StringBuilder();
        //        strSql.Append("delete FM2E_OutWarehouseDetail ");
        //        strSql.Append(" where ID=@ID");
        //        SqlParameter[] parameters = {
        //            new SqlParameter("@ID", SqlDbType.BigInt)};
        //        parameters[0].Value = ID;
        //        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new DALException("删除出库申请明细信息失败", e);
        //    }
        //}
        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public OutWarehouseApplyInfo GetOutWarehouseApply(long ID)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT a.*,b.PersonName as ApplicantName,c.PersonName as ApprovalerName,d.PersonName as ReceiverName,e.PersonName as OperatorName,f.[name] as WarehouseName,g.CompanyName as CompanyName FROM FM2E_OutWarehouseApply a left join FM2E_User b on a.applicant=b.UserName left join FM2E_User c on a.Approvaler=c.UserName left join FM2E_User d on a.Receiver=d.UserName left join FM2E_User e on a.Operator=e.UserName left join FM2E_Warehouse f on a.warehouseID = f.warehouseID  left join FM2E_Company g on a.CompanyID = g.CompanyID");
        //    strSql.Append(" where a.ID=@ID ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID", SqlDbType.BigInt)};
        //    parameters[0].Value = ID;
        //    OutWarehouseApplyInfo item = new OutWarehouseApplyInfo();
        //    try
        //    {
        //        using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
        //        {
        //            if (rd.Read())
        //            {
        //                item = this.GetData(rd);
        //            }
        //            if (item == null) return null;

        //            //获取申请明细列表
        //            strSql = new StringBuilder();
        //            strSql.Append("select a.*,s1.SystemName,s2.SectionName from FM2E_OutWarehouseDetail a left join FM2E_System s1 on s1.SystemID=a.SystemID left join FM2E_Section s2 on s2.SectionID = a.SectionID");
        //            strSql.Append(" where a.ID='" + ID.ToString() + "'");
                   
        //            ArrayList list = new ArrayList();
        //            using (SqlDataReader rd1 = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
        //            {
        //                while (rd1.Read())
        //                {
        //                    OutWarehouseDetailInfo item1 = GetDetailData(rd1);
        //                    list.Add(item1);
        //                }
        //            }
        //            item.ApplyDetailList = list;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new DALException("获取出库申请信息失败", e);
        //    }
        //    return item;
        //}
        #endregion

        #region 各表INSERT函数
        private long InsertOutWarehouseApply(SqlTransaction trans, OutWarehouseApplyInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+TABLE_OUTWAREHOUSE_APPLY+"(");
            strSql.Append("OutTime,Receiver,Operator,OutWarehouseRemark,SheetName,WarehouseID,ApplyTime,ApplyRemark,CompanyID,Applicant)");
            strSql.Append(" values (");
            strSql.Append("@OutTime,@Receiver,@Operator,@OutWarehouseRemark,@SheetName,@WarehouseID,@ApplyTime,@ApplyRemark,@CompanyID,@Applicant)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@OutTime", SqlDbType.DateTime),
					new SqlParameter("@Receiver", SqlDbType.VarChar,20),
					new SqlParameter("@Operator", SqlDbType.VarChar,20),
					new SqlParameter("@OutWarehouseRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@SheetName", SqlDbType.NVarChar,20),
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@ApplyTime", SqlDbType.DateTime),
					new SqlParameter("@ApplyRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Applicant", SqlDbType.VarChar,20)};
            parameters[0].Value = (model.OutTime == DateTime.MinValue) ? SqlDateTime.Null : model.OutTime;
            parameters[1].Value = string.IsNullOrEmpty(model.Receiver)?SqlString.Null:model.Receiver;
            parameters[2].Value = string.IsNullOrEmpty(model.Operator) ? SqlString.Null : model.Operator;
            parameters[3].Value = string.IsNullOrEmpty(model.OutWarehouseRemark)?SqlString.Null:model.OutWarehouseRemark;
            parameters[4].Value = string.IsNullOrEmpty(model.SheetName)?SqlString.Null:model.SheetName;
            parameters[5].Value = string.IsNullOrEmpty(model.WarehouseID)?SqlString.Null:model.WarehouseID;
            parameters[6].Value = (model.ApplyTime==DateTime.MinValue)?SqlDateTime.Null:model.ApplyTime;
            parameters[7].Value = string.IsNullOrEmpty(model.ApplyRemark)?SqlString.Null:model.ApplyRemark;
            parameters[8].Value = string.IsNullOrEmpty(model.CompanyID)?SqlString.Null:model.CompanyID;
            parameters[9].Value = string.IsNullOrEmpty(model.Applicant)?SqlString.Null:model.Applicant;
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

        private long InsertOutWarehouseDetail(SqlTransaction trans, OutWarehouseDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + TABLE_OUTWAREHOUSE_DETAIL + "(");
            strSql.Append("SystemID,DetailLocation,AddressID,ID,ProductName,Model,Count,Unit,Usage,Remark)");
            strSql.Append(" values (");
            strSql.Append("@SystemID,@DetailLocation,@AddressID,@ID,@ProductName,@Model,@Count,@Unit,@Usage,@Remark)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@SystemID", SqlDbType.VarChar,2),
					new SqlParameter("@DetailLocation", SqlDbType.NVarChar,20),
					new SqlParameter("@AddressID", SqlDbType.BigInt,8),
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@Model", SqlDbType.NVarChar,20),
					new SqlParameter("@Count", SqlDbType.Decimal,9),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@Usage", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50)};
            parameters[0].Value = string.IsNullOrEmpty(model.SystemID) ? SqlString.Null : model.SystemID;
            parameters[1].Value = string.IsNullOrEmpty(model.DetailLocation) ? SqlString.Null : model.DetailLocation;
            parameters[2].Value = model.AddressID == 0 ? SqlInt64.Null : model.AddressID;
            parameters[3].Value = model.ID == 0 ? SqlInt64.Null : model.ID;
            parameters[4].Value = string.IsNullOrEmpty(model.ProductName) ? SqlString.Null : model.ProductName;
            parameters[5].Value = string.IsNullOrEmpty(model.Model) ? SqlString.Null : model.Model;
            parameters[6].Value = model.Count == decimal.Zero ? SqlDecimal.Null : model.Count;
            parameters[7].Value = string.IsNullOrEmpty(model.Unit) ? SqlString.Null : model.Unit;
            parameters[8].Value = string.IsNullOrEmpty(model.Usage) ? SqlString.Null : model.Usage;
            parameters[9].Value = string.IsNullOrEmpty(model.Remark) ? SqlString.Null : model.Remark;
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

        private long InsertOutWarehouseApproval(SqlTransaction trans, OutWarehouseApprovalInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+TABLE_OUTWAREHOUSE_APPROVAL+"(");
            strSql.Append("OutWarehouseApplyID,CompanyID,Approvaler,Result,FeedBack,ApprovalTime)");
            strSql.Append(" values (");
            strSql.Append("@OutWarehouseApplyID,@CompanyID,@Approvaler,@Result,@FeedBack,@ApprovalTime)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@OutWarehouseApplyID", SqlDbType.BigInt,8),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Approvaler", SqlDbType.VarChar,20),
					new SqlParameter("@Result", SqlDbType.NVarChar,20),
					new SqlParameter("@FeedBack", SqlDbType.NVarChar,50),
					new SqlParameter("@ApprovalTime", SqlDbType.DateTime)};
            parameters[0].Value = model.OutWarehouseApplyID==0?SqlInt64.Null:model.OutWarehouseApplyID;
            parameters[1].Value = string.IsNullOrEmpty(model.CompanyID) ? SqlString.Null : model.CompanyID;
            parameters[2].Value =  string.IsNullOrEmpty(model.Approvaler) ? SqlString.Null : model.Approvaler;
            parameters[3].Value =  string.IsNullOrEmpty(model.Result) ? SqlString.Null : model.Result;
            parameters[4].Value =  string.IsNullOrEmpty(model.FeedBack) ? SqlString.Null : model.FeedBack;
            parameters[5].Value = model.ApprovalTime==DateTime.MinValue?SqlDateTime.Null:model.ApprovalTime;
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

        private long InsertOutEquipment(SqlTransaction trans, OutEquipmentsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+TABLE_OUT_EQUIPMENT+"(");
            strSql.Append("Remark,Name,Model,AddressID,DetailLocation,SystemID,ApplyItemID,WarehouseID,IsAsset,EquipmentNO,ExpendableID,Count,Unit,OutTime)");
            strSql.Append(" values (");
            strSql.Append("@Remark,@Name,@Model,@AddressID,@DetailLocation,@SystemID,@ApplyItemID,@WarehouseID,@IsAsset,@EquipmentNO,@ExpendableID,@Count,@Unit,@OutTime)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@Model", SqlDbType.NVarChar,20),
					new SqlParameter("@AddressID", SqlDbType.BigInt,8),
					new SqlParameter("@DetailLocation", SqlDbType.NVarChar,50),
					new SqlParameter("@SystemID", SqlDbType.VarChar,2),
					new SqlParameter("@ApplyItemID", SqlDbType.BigInt,8),
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@IsAsset", SqlDbType.Bit,1),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
					new SqlParameter("@ExpendableID", SqlDbType.BigInt,8),
					new SqlParameter("@Count", SqlDbType.Decimal,9),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@OutTime", SqlDbType.DateTime)};
            parameters[0].Value = string.IsNullOrEmpty(model.Remark) ? SqlString.Null :model.Remark;
            parameters[1].Value = string.IsNullOrEmpty(model.Name) ? SqlString.Null : model.Name;
            parameters[2].Value = string.IsNullOrEmpty(model.Model) ? SqlString.Null : model.Model;
            parameters[3].Value = model.AddressID == 0 ? SqlInt64.Null : model.AddressID;
            parameters[4].Value =string.IsNullOrEmpty( model.DetailLocation) ? SqlString.Null : model.DetailLocation;
            parameters[5].Value =string.IsNullOrEmpty( model.SystemID) ? SqlString.Null : model.SystemID;
            parameters[6].Value = model.ApplyItemID == 0 ? SqlInt64.Null : model.ApplyItemID;
            parameters[7].Value =string.IsNullOrEmpty( model.WarehouseID) ? SqlString.Null : model.WarehouseID;
            parameters[8].Value = model.IsAsset;
            parameters[9].Value =string.IsNullOrEmpty( model.EquipmentNO) ? SqlString.Null : model.EquipmentNO;
            parameters[10].Value = model.ExpendableID == 0 ? SqlInt64.Null : model.ExpendableID;
            parameters[11].Value = model.Count == decimal.Zero ? SqlDecimal.Null : model.Count;
            parameters[12].Value = string.IsNullOrEmpty(model.Unit) ? SqlString.Null : model.Unit;
            parameters[13].Value = model.OutTime == DateTime.MinValue ? SqlDateTime.Null : model.OutTime;
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

        #region GetData函数
        private OutWarehouseApplyInfo GetData(IDataReader rd)
        {
            OutWarehouseApplyInfo item = new OutWarehouseApplyInfo();

            if (!Convert.IsDBNull(rd["Applicant"]))
                item.Applicant = Convert.ToString(rd["Applicant"]);

            if (!Convert.IsDBNull(rd["ApplicantName"]))
                item.ApplicantName = Convert.ToString(rd["ApplicantName"]);

            if (!Convert.IsDBNull(rd["ApplicantDepartmentID"]))
                item.ApplicantDepartmentID = Convert.ToInt64(rd["ApplicantDepartmentID"]);

            if (!Convert.IsDBNull(rd["ApplicantDepartmentName"]))
                item.ApplicantDepartmentName = Convert.ToString(rd["ApplicantDepartmentName"]);

            if (!Convert.IsDBNull(rd["ApplicantPositionName"]))
                item.ApplicantPositionName = Convert.ToString(rd["ApplicantPositionName"]);

            if (!Convert.IsDBNull(rd["ApplyRemark"]))
                item.ApplyRemark = Convert.ToString(rd["ApplyRemark"]);

            if (!Convert.IsDBNull(rd["ApplyTime"]))
                item.ApplyTime = Convert.ToDateTime(rd["ApplyTime"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["CompanyName"]))
                item.CompanyName = Convert.ToString(rd["CompanyName"]);

            if (!Convert.IsDBNull(rd["DelegateUserDepartmentID"]))
                item.DelegateUserDepartmentID = Convert.ToInt64(rd["DelegateUserDepartmentID"]);

            if (!Convert.IsDBNull(rd["DelegateUserDepartmentName"]))
                item.DelegateUserDepartmentName = Convert.ToString(rd["DelegateUserDepartmentName"]);

            if (!Convert.IsDBNull(rd["DelegateUserPersonName"]))
                item.DelegateUserName = Convert.ToString(rd["DelegateUserPersonName"]);

            if (!Convert.IsDBNull(rd["DelegateUserName"]))
                item.DelegateUserName = Convert.ToString(rd["DelegateUserName"]);

            if (!Convert.IsDBNull(rd["DelegateUserPositionName"]))
                item.DelegateUserPositionName = Convert.ToString(rd["DelegateUserPositionName"]);

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["NextUserDepartmentID"]))
                item.NextUserDepartmentID = Convert.ToInt64(rd["NextUserDepartmentID"]);

            if (!Convert.IsDBNull(rd["NextUserDepartmentName"]))
                item.NextUserDepartmentName = Convert.ToString(rd["NextUserDepartmentName"]);

            if (!Convert.IsDBNull(rd["NextUserName"]))
                item.NextUserName = Convert.ToString(rd["NextUserName"]);

            if (!Convert.IsDBNull(rd["NextUserPersonName"]))
                item.NextUserPersonName = Convert.ToString(rd["NextUserPersonName"]);

            if (!Convert.IsDBNull(rd["NextUserPositionName"]))
                item.NextUserPositionName = Convert.ToString(rd["NextUserPositionName"]);

            if (!Convert.IsDBNull(rd["OperatorDepartmentID"]))
                item.OperatorDepartmentID = Convert.ToInt64(rd["OperatorDepartmentID"]);

            if (!Convert.IsDBNull(rd["Operator"]))
                item.Operator = Convert.ToString(rd["Operator"]);

            if (!Convert.IsDBNull(rd["OperatorDepartmentName"]))
                item.OperatorDepartmentName = Convert.ToString(rd["OperatorDepartmentName"]);

            if (!Convert.IsDBNull(rd["OperatorName"]))
                item.OperatorName = Convert.ToString(rd["OperatorName"]);

            if (!Convert.IsDBNull(rd["OperatorPositionName"]))
                item.OperatorPositionName = Convert.ToString(rd["OperatorPositionName"]);

            if (!Convert.IsDBNull(rd["OutTime"]))
                item.OutTime = Convert.ToDateTime(rd["OutTime"]);

            if (!Convert.IsDBNull(rd["OutWarehouseRemark"]))
                item.OutWarehouseRemark = Convert.ToString(rd["OutWarehouseRemark"]);

            if (!Convert.IsDBNull(rd["Receiver"]))
                item.Receiver = Convert.ToString(rd["Receiver"]);

            if (!Convert.IsDBNull(rd["ReceiverDepartmentID"]))
                item.ReceiverDepartmentID = Convert.ToInt64(rd["ReceiverDepartmentID"]);

            if (!Convert.IsDBNull(rd["ReceiverDepartmentName"]))
                item.ReceiverDepartmentName = Convert.ToString(rd["ReceiverDepartmentName"]);

            if (!Convert.IsDBNull(rd["OperatorPositionName"]))
                item.OperatorPositionName = Convert.ToString(rd["OperatorPositionName"]);

            if (!Convert.IsDBNull(rd["ReceiverName"]))
                item.ReceiverName = Convert.ToString(rd["ReceiverName"]);

            if (!Convert.IsDBNull(rd["ReceiverPositionName"]))
                item.ReceiverPositionName = Convert.ToString(rd["ReceiverPositionName"]);

            if (!Convert.IsDBNull(rd["SheetName"]))
                item.SheetName = Convert.ToString(rd["SheetName"]);

            if (!Convert.IsDBNull(rd["WarehouseID"]))
                item.WarehouseID = Convert.ToString(rd["WarehouseID"]);

            if (!Convert.IsDBNull(rd["WarehouseName"]))
                item.WarehouseName = Convert.ToString(rd["WarehouseName"]);

            if (!Convert.IsDBNull(rd["InstanceID"]))
                item.WorkFlowInstanceID = Convert.ToString(rd["InstanceID"]);

            if (!Convert.IsDBNull(rd["CurrentStateName"]))
                item.WorkFlowStateName = Convert.ToString(rd["CurrentStateName"]);

            if (!Convert.IsDBNull(rd["StatusDescription"]))
                item.WorkFlowStateDescription = Convert.ToString(rd["StatusDescription"]);

            return item;

        }

      
      

        private OutWarehouseDetailInfo GetDetailData(IDataReader rd)
        {
            OutWarehouseDetailInfo item = new OutWarehouseDetailInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["ItemID"]))
                item.ItemID = Convert.ToInt64(rd["ItemID"]);

            if (!Convert.IsDBNull(rd["ProductName"]))
                item.ProductName = Convert.ToString(rd["ProductName"]);

            if (!Convert.IsDBNull(rd["Model"]))
                item.Model = Convert.ToString(rd["Model"]);

            if (!Convert.IsDBNull(rd["Count"]))
                item.Count = Convert.ToDecimal(rd["Count"]);

            if (!Convert.IsDBNull(rd["Unit"]))
                item.Unit = Convert.ToString(rd["Unit"]);

            if (!Convert.IsDBNull(rd["Usage"]))
                item.Usage = Convert.ToString(rd["Usage"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["SystemID"]))
                item.SystemID = Convert.ToString(rd["SystemID"]);

            if (!Convert.IsDBNull(rd["SystemName"]))
                item.SystemName = Convert.ToString(rd["SystemName"]);

            if (!Convert.IsDBNull(rd["AddressID"]))
                item.AddressID = Convert.ToInt64(rd["AddressID"]);

            if (!Convert.IsDBNull(rd["AddressName"]))
                item.AddressName = Convert.ToString(rd["AddressName"]);

            if (!Convert.IsDBNull(rd["DetailLocation"]))
                item.DetailLocation = Convert.ToString(rd["DetailLocation"]);
            return item;

        }
        private OutEquipmentsInfo GetEquipmentData(IDataReader rd)
        {
            OutEquipmentsInfo item = new OutEquipmentsInfo();

            if (!Convert.IsDBNull(rd["ItemID"]))
                item.ItemID = Convert.ToInt64(rd["ItemID"]);

            if (!Convert.IsDBNull(rd["ApplyItemID"]))
                item.ApplyItemID = Convert.ToInt64(rd["ApplyItemID"]);

            if (!Convert.IsDBNull(rd["WarehouseID"]))
                item.WarehouseID = Convert.ToString(rd["WarehouseID"]);

            if (!Convert.IsDBNull(rd["IsAsset"]))
                item.IsAsset = Convert.ToBoolean(rd["IsAsset"]);

            if (!Convert.IsDBNull(rd["EquipmentNO"]))
                item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

            if (!Convert.IsDBNull(rd["ExpendableID"]))
                item.ExpendableID = Convert.ToInt64(rd["ExpendableID"]);

            if (!Convert.IsDBNull(rd["Count"]))
                item.Count = Convert.ToDecimal(rd["Count"]);

            if (!Convert.IsDBNull(rd["Unit"]))
                item.Unit = Convert.ToString(rd["Unit"]);

            if (!Convert.IsDBNull(rd["OutTime"]))
                item.OutTime = Convert.ToDateTime(rd["OutTime"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["WarehouseName"]))
                item.WarehouseName = Convert.ToString(rd["WarehouseName"]);

            if (!Convert.IsDBNull(rd["Name"]))
                item.Name = Convert.ToString(rd["Name"]);

            if (!Convert.IsDBNull(rd["Model"]))
                item.Model = Convert.ToString(rd["Model"]);
            if (!Convert.IsDBNull(rd["SystemID"]))
                item.SystemID = Convert.ToString(rd["SystemID"]);

            if (!Convert.IsDBNull(rd["SystemName"]))
                item.SystemName = Convert.ToString(rd["SystemName"]);

            if (!Convert.IsDBNull(rd["AddressID"]))
                item.AddressID = Convert.ToInt64(rd["AddressID"]);

            if (!Convert.IsDBNull(rd["AddressName"]))
                item.AddressName = Convert.ToString(rd["AddressName"]);

            if (!Convert.IsDBNull(rd["DetailLocation"]))
                item.DetailLocation = Convert.ToString(rd["DetailLocation"]);

            return item;
        }
        private OutWarehouseApprovalInfo GetApprovalData(IDataReader rd)
        {
            OutWarehouseApprovalInfo item = new OutWarehouseApprovalInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["Approvaler"]))
                item.Approvaler = Convert.ToString(rd["Approvaler"]);

            if (!Convert.IsDBNull(rd["ApprovalerDepartmentID"]))
                item.ApprovalerDepartmentID = Convert.ToInt64(rd["ApprovalerDepartmentID"]);

            if (!Convert.IsDBNull(rd["ApprovalerDepartmentName"]))
                item.ApprovalerDepartmentName = Convert.ToString(rd["ApprovalerDepartmentName"]);

            if (!Convert.IsDBNull(rd["ApprovalerName"]))
                item.ApprovalerName = Convert.ToString(rd["ApprovalerName"]);

            if (!Convert.IsDBNull(rd["ApprovalerPositionName"]))
                item.ApprovalerPositionName = Convert.ToString(rd["ApprovalerPositionName"]);

            if (!Convert.IsDBNull(rd["ApprovalTime"]))
                item.ApprovalTime = Convert.ToDateTime(rd["ApprovalTime"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["FeedBack"]))
                item.FeedBack = Convert.ToString(rd["FeedBack"]);

            if (!Convert.IsDBNull(rd["OutWarehouseApplyID"]))
                item.OutWarehouseApplyID = Convert.ToInt64(rd["OutWarehouseApplyID"]);

            if (!Convert.IsDBNull(rd["Result"]))
                item.Result = Convert.ToString(rd["Result"]);
            return item;
        }
        #endregion

        #region 各视图获取一个对象的函数，当前只需要主表
        private OutWarehouseApplyInfo GetOutWarehouseApply(SqlConnection conn, long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from "+ VIEW_OUTWAREHOUSE_APPLY+" ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = id;

            OutWarehouseApplyInfo item = null;
            if (conn == null)
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        item = GetData(rd);
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
                        item = GetData(rd);
                        break;
                    }
                }
            }
            return item;
        }
        #endregion

        #region 各视图获取列表函数，不需要包括主表
        private IList GetOutWarehouseDetailInfoList(SqlConnection conn, long applyid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + VIEW_OUTWAREHOUSE_DETAIL + " ");
            strSql.Append(" where ID=@ID ");
            strSql.Append(" order by ID ASC;");//排序
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = applyid;

            List<OutWarehouseDetailInfo> list = new List<OutWarehouseDetailInfo>();
            if (conn == null)
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        OutWarehouseDetailInfo item = GetDetailData(rd);
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
                        OutWarehouseDetailInfo item = GetDetailData(rd);
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        private IList GetOutWarehouseApprovalInfoList(SqlConnection conn, long applyid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + VIEW_OUTWAREHOUSE_APPROVAL + " ");
            strSql.Append(" where OutWarehouseApplyID=@ID ");
            strSql.Append(" order by ID ASC;");//排序
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = applyid;

            List<OutWarehouseApprovalInfo> list = new List<OutWarehouseApprovalInfo>();
            if (conn == null)
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        OutWarehouseApprovalInfo item = GetApprovalData(rd);
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
                        OutWarehouseApprovalInfo item = GetApprovalData(rd);
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        private IList GetOutEquipmentInfoList(SqlConnection conn, long detailid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + VIEW_OUT_EQUIPMENT + " ");
            strSql.Append(" where ApplyItemID=@ID ");
            strSql.Append(" order by ItemID ASC;");//排序
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = detailid;

            List<OutEquipmentsInfo> list = new List<OutEquipmentsInfo>();
            if (conn == null)
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        OutEquipmentsInfo item = GetEquipmentData(rd);
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
                        OutEquipmentsInfo item = GetEquipmentData(rd);
                        list.Add(item);
                    }
                }
            }
            return list;
        }
        #endregion

        #region 各表UPDATE函数，当前只需要主表
        private void UpdateOutWarehouseApply(SqlTransaction trans, OutWarehouseApplyInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_OutWarehouseApply set ");
            strSql.Append("OutTime=@OutTime,");
            strSql.Append("Receiver=@Receiver,");
            strSql.Append("Operator=@Operator,");
            strSql.Append("OutWarehouseRemark=@OutWarehouseRemark,");
            strSql.Append("SheetName=@SheetName,");
            strSql.Append("WarehouseID=@WarehouseID,");
            strSql.Append("ApplyTime=@ApplyTime,");
            strSql.Append("ApplyRemark=@ApplyRemark,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("Applicant=@Applicant");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@OutTime", SqlDbType.DateTime),
					new SqlParameter("@Receiver", SqlDbType.VarChar,20),
					new SqlParameter("@Operator", SqlDbType.VarChar,20),
					new SqlParameter("@OutWarehouseRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@SheetName", SqlDbType.NVarChar,20),
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@ApplyTime", SqlDbType.DateTime),
					new SqlParameter("@ApplyRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Applicant", SqlDbType.VarChar,20)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.OutTime == DateTime.MinValue ? SqlDateTime.Null : model.OutTime;
            parameters[2].Value = string.IsNullOrEmpty(model.Receiver) ? SqlString.Null : model.Receiver;
            parameters[3].Value = string.IsNullOrEmpty(model.Operator) ? SqlString.Null : model.Operator;
            parameters[4].Value = string.IsNullOrEmpty(model.OutWarehouseRemark) ? SqlString.Null : model.OutWarehouseRemark;
            parameters[5].Value = string.IsNullOrEmpty(model.SheetName) ? SqlString.Null : model.SheetName;
            parameters[6].Value = string.IsNullOrEmpty(model.WarehouseID) ? SqlString.Null : model.WarehouseID;
            parameters[7].Value = model.ApplyTime == DateTime.MinValue ? SqlDateTime.Null : model.ApplyTime;
            parameters[8].Value = string.IsNullOrEmpty(model.ApplyRemark) ? SqlString.Null : model.ApplyRemark;
            parameters[9].Value = string.IsNullOrEmpty(model.CompanyID) ? SqlString.Null : model.CompanyID;
            parameters[10].Value = string.IsNullOrEmpty(model.Applicant) ? SqlString.Null : model.Applicant;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        #endregion

        #region 各表删除函数，当前只有主表和详情表
        private void DeleteOutWarehouseApplyInfo(SqlTransaction trans, long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete " + TABLE_OUTWAREHOUSE_APPLY + " ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = id;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        private void DeleteOutWarehouseDetailInfoList(SqlTransaction trans, long applyid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete " + TABLE_OUTWAREHOUSE_DETAIL + " ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = applyid;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        #endregion
               
        #region IOutWarehouseApply 成员

        /// <summary>
        /// 生成出库申请单查询对象
        /// </summary>
        /// <param name="info">查询信息对象</param>
        /// <param name="pageindex">页码</param>
        /// <param name="pagesize">页大小</param>
        /// <returns>出库申请单查询对象</returns>
        public QueryParam GenerateSearchTerm(OutWarehouseApplySearchInfo info,int pageindex, int pagesize)
        {
            string sqlSearch = "where 1=1";

            if (!string.IsNullOrEmpty(info.Applicant))
            {
                sqlSearch += " and Applicant='" + info.Applicant + "'";
            }
            if (!string.IsNullOrEmpty(info.ApplicantName))
            {
                sqlSearch += " and ApplicantName like '%" + info.ApplicantName + "%'";
            }
            if (!string.IsNullOrEmpty(info.CompanyID))
            {
                sqlSearch += " and CompanyID='" + info.CompanyID + "'";
            }
            if (info.ID!=0)
            {
                sqlSearch += " and ID=" + info.ID + "";

            }
            if (!string.IsNullOrEmpty(info.NextUserName))
            {
                sqlSearch += " and (NextUserName='" + info.NextUserName + "'" + " or DelegateUserName='" + info.NextUserName + "')";
            }
            if (!string.IsNullOrEmpty(info.Operator))
            {
                sqlSearch += " and Operator='" + info.Operator + "'";
            }

            if (!string.IsNullOrEmpty(info.OperatorName))
            {
                sqlSearch += " and OperatorName like '%" + info.OperatorName + "%'";
            }

            if (!string.IsNullOrEmpty(info.Receiver))
            {
                sqlSearch += " and Receiver='" + info.Receiver + "'";
            }

            if (!string.IsNullOrEmpty(info.ReceiverName))
            {
                sqlSearch += " and ReceiverName like '%" + info.ReceiverName + "%'";
            }

            if (!string.IsNullOrEmpty(info.SheetName))
            {
                sqlSearch += " and SheetName='" + info.SheetName + "'";
            }

            if (!string.IsNullOrEmpty(info.WarehouseID))
            {
                sqlSearch += " and WarehouseID='" + info.WarehouseID + "'";
            }

            if (info.WorkFlowStatusList != null && info.WorkFlowStatusList.Count > 0)
            {
                for (int i = 0; i < info.WorkFlowStatusList.Count; i++)
                {
                    if (i == 0)
                    {
                        sqlSearch += " and ( ";
                        sqlSearch += " " + "CurrentStateName='" + info.WorkFlowStatusList[i] + "'";
                    }
                    else
                    {
                        sqlSearch += " or " + "CurrentStateName='" + info.WorkFlowStatusList[i] + "'";
                    }
                    if (i == info.WorkFlowStatusList.Count - 1)
                    {
                        sqlSearch += " ) ";
                    }
                }
            }

            if (info.ApplyTimeLower != DateTime.MinValue)
            {
                sqlSearch += " and ApplyTime >= '" + info.ApplyTimeLower.ToString("yyyy-MM-dd HH:mm") + "'";
            }

            if (info.ApplyTimeUpper != DateTime.MinValue)
            {
                sqlSearch += " and ApplyTime< '" + info.ApplyTimeUpper.AddDays(1).ToString("yyyy-MM-dd HH:mm") + "'";
            }

            if (info.OutTimeLower != DateTime.MinValue)
            {
                sqlSearch += " and OutTime >= '" + info.OutTimeLower.ToString("yyyy-MM-dd HH:mm") + "'";
            }

            if (info.OutTimeUpper != DateTime.MinValue)
            {
                sqlSearch += " and OutTime< '" + info.OutTimeUpper.AddDays(1).ToString("yyyy-MM-dd HH:mm") + "'";
            }



            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = VIEW_OUTWAREHOUSE_APPLY;
            searchTerm.ReturnFields = "*";
            searchTerm.PageSize = pagesize;
            searchTerm.PageIndex = pageindex;
            searchTerm.OrderBy = "order by ApplyTime desc";
            searchTerm.Where = sqlSearch;
            
            return searchTerm;
        }

        /// <summary>
        /// 生成出库申请单查询对象（审批专用）
        /// </summary>
        /// <param name="info">查询信息对象</param>
        /// <param name="pageindex">页码</param>
        /// <param name="pagesize">页大小</param>
        /// <returns>出库申请单查询对象</returns>
        public QueryParam GenerateSearchTerm(OutWarehouseApplySearchForApprovalerInfo info, int pageindex, int pagesize)
        {
            string sqlSearch = "where 1=1";

            if (!string.IsNullOrEmpty(info.Applicant))
            {
                sqlSearch += " and s1.Applicant='" + info.Applicant + "'";
            }
            if (!string.IsNullOrEmpty(info.ApplicantName))
            {
                sqlSearch += " and s1.ApplicantName like '%" + info.ApplicantName + "%'";
            }
            if (!string.IsNullOrEmpty(info.CompanyID))
            {
                sqlSearch += " and s1.CompanyID='" + info.CompanyID + "'";
            }
            if (info.ID != 0)
            {
                sqlSearch += " and s1.ID=" + info.ID + "";

            }
            if (!string.IsNullOrEmpty(info.NextUserName))
            {
                sqlSearch += " and (s1.NextUserName='" + info.NextUserName + "'" + " or s1.DelegateUserName='" + info.NextUserName + "')";
            }
            if (!string.IsNullOrEmpty(info.Operator))
            {
                sqlSearch += " and s1.Operator='" + info.Operator + "'";
            }

            if (!string.IsNullOrEmpty(info.OperatorName))
            {
                sqlSearch += " and s1.OperatorName like '%" + info.OperatorName + "%'";
            }

            if (!string.IsNullOrEmpty(info.Receiver))
            {
                sqlSearch += " and s1.Receiver='" + info.Receiver + "'";
            }

            if (!string.IsNullOrEmpty(info.ReceiverName))
            {
                sqlSearch += " and s1.ReceiverName like '%" + info.ReceiverName + "%'";
            }

            if (!string.IsNullOrEmpty(info.SheetName))
            {
                sqlSearch += " and s1.SheetName='" + info.SheetName + "'";
            }

            if (!string.IsNullOrEmpty(info.WarehouseID))
            {
                sqlSearch += " and s1.WarehouseID='" + info.WarehouseID + "'";
            }

            if (info.WorkFlowStatusList != null && info.WorkFlowStatusList.Count > 0)
            {
                for (int i = 0; i < info.WorkFlowStatusList.Count; i++)
                {
                    if (i == 0)
                    {
                        sqlSearch += " and ( ";
                        sqlSearch += " " + "s1.CurrentStateName='" + info.WorkFlowStatusList[i] + "'";
                    }
                    else
                    {
                        sqlSearch += " or " + "s1.CurrentStateName='" + info.WorkFlowStatusList[i] + "'";
                    }
                    if (i == info.WorkFlowStatusList.Count - 1)
                    {
                        sqlSearch += " ) ";
                    }
                }
            }

            if (info.ApplyTimeLower != DateTime.MinValue)
            {
                sqlSearch += " and s1.ApplyTime >= '" + info.ApplyTimeLower.ToString("yyyy-MM-dd HH:mm") + "'";
            }

            if (info.ApplyTimeUpper != DateTime.MinValue)
            {
                sqlSearch += " and s1.ApplyTime< '" + info.ApplyTimeUpper.AddDays(1).ToString("yyyy-MM-dd HH:mm") + "'";
            }

            if (info.OutTimeLower != DateTime.MinValue)
            {
                sqlSearch += " and s1.OutTime >= '" + info.OutTimeLower.ToString("yyyy-MM-dd HH:mm") + "'";
            }

            if (info.OutTimeUpper != DateTime.MinValue)
            {
                sqlSearch += " and s1.OutTime< '" + info.OutTimeUpper.AddDays(1).ToString("yyyy-MM-dd HH:mm") + "'";
            }

            if (!string.IsNullOrEmpty(info.Approvaler))
            {
                sqlSearch += " and s2.Approvaler='" + info.Approvaler + "'";
            }

            if (info.ApprovalTimeLower != DateTime.MinValue)
            {
                sqlSearch += " and s2.ApprovalTimeUpper >= '" + info.ApprovalTimeLower.ToString("yyyy-MM-dd HH:mm") + "'";
            }

            if (info.ApprovalTimeUpper != DateTime.MinValue)
            {
                sqlSearch += " and s2.ApprovalTimeUpper< '" + info.ApprovalTimeUpper.AddDays(1).ToString("yyyy-MM-dd HH:mm") + "'";
            }


            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = VIEW_OUTWAREHOUSE_APPLY + " s1 INNER JOIN " + VIEW_OUTWAREHOUSE_APPROVAL + " s2 " +
                " ON s1.ID = s2.OutWarehouseApplyID";
            searchTerm.ReturnFields = "s1.*,s1.ApplyTime as ApplyTimeSort";
            searchTerm.PageSize = pagesize;
            searchTerm.PageIndex = pageindex;
            searchTerm.OrderBy = "order by ApplyTimeSort desc";
            searchTerm.Where = sqlSearch;

            return searchTerm;
        }

        /// <summary>
        /// 查询出库申请单
        /// </summary>
        /// <param name="qp">出库申请单查询对象</param>
        /// <param name="recordCount">查询结果总数</param>
        /// <returns>出库申请单查询结果列表</returns>
        public IList SearchOutWarehouseApply(QueryParam qp, out int recordCount)
        {
            return SQLHelper.GetObjectListWithDistinct(this.GetData, qp, out recordCount);
        }

        #region 易耗品

          private OutWarehouseInfo GetDataExpendable(IDataReader rd)
        {
            OutWarehouseInfo item = new OutWarehouseInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["WarehouseID"]))
                item.WarehouseID = Convert.ToString(rd["WarehouseID"]);

            if (!Convert.IsDBNull(rd["DepartmentID"]))
                item.DepartmentID = Convert.ToInt64(rd["DepartmentID"]);

            if (!Convert.IsDBNull(rd["SubmitTime"]))
                item.SubmitTime = Convert.ToDateTime(rd["SubmitTime"]);

            if (!Convert.IsDBNull(rd["ApplicantID"]))
                item.ApplicantID = Convert.ToString(rd["ApplicantID"]);

            if (!Convert.IsDBNull(rd["OperatorID"]))
                item.OperatorID = Convert.ToString(rd["OperatorID"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["IsDeleted"]))
                item.IsDeleted = Convert.ToBoolean(rd["IsDeleted"]);

            if (!Convert.IsDBNull(rd["WarehouseName"]))
                item.WarehouseName = Convert.ToString(rd["WarehouseName"]);

            if (!Convert.IsDBNull(rd["CompanyName"]))
                item.CompanyName = Convert.ToString(rd["CompanyName"]);

            if (!Convert.IsDBNull(rd["DepartmentName"]))
                item.DepartmentName = Convert.ToString(rd["DepartmentName"]);

            if (!Convert.IsDBNull(rd["ApplicantName"]))
                item.ApplicantName = Convert.ToString(rd["ApplicantName"]);

            if (!Convert.IsDBNull(rd["OperatorName"]))
                item.OperatorName = Convert.ToString(rd["OperatorName"]);

            if (!Convert.IsDBNull(rd["SheetName"]))
                item.SheetName = Convert.ToString(rd["SheetName"]);

            if (!Convert.IsDBNull(rd["CurrentStateName"]))
                item.CurrentStateName = Convert.ToString(rd["CurrentStateName"]);

            if (!Convert.IsDBNull(rd["Attachment"]))
                item.Attachment = Convert.ToString(rd["Attachment"]);
            if (!Convert.IsDBNull(rd["Editreason"]))
                item.Editreason = Convert.ToString(rd["Editreason"]);
            if (!Convert.IsDBNull(rd["InstanceID"]))
                item.WorkFlowInstanceID = Convert.ToString(rd["InstanceID"]);
            if (!Convert.IsDBNull(rd["NextUserName"]))
                item.NextUserName = Convert.ToString(rd["NextUserName"]);
            if (!Convert.IsDBNull(rd["StatusDescription"]))
                item.StatusDescription = Convert.ToString(rd["StatusDescription"]);
            return item;

        }


        /// <summary>
        /// 查询易耗品出库申请单
        /// </summary>
        /// <param name="qp">出库申请单查询对象</param>
        /// <param name="recordCount">查询结果总数</param>
        /// <returns>出库申请单查询结果列表</returns>
        public IList SearchOutWarehouseExpendable(QueryParam qp, out int recordCount)
        {
            return SQLHelper.GetObjectListWithDistinct(this.GetDataExpendable, qp, out recordCount);
        }

      
      
        /// <summary>
        /// 获取出库详情
        /// </summary>
        private IList GetEquipmentList(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from FM2E_OutEquipmentsForExpendable ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = id;
            List<InEquipmentsInfo> list = new List<InEquipmentsInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    InEquipmentsInfo item = GetDataInEquipment(rd);
                    list.Add(item);
                }
            }
            return list;
        }

        private InEquipmentsInfo GetDataInEquipment(IDataReader rd)
        {
            InEquipmentsInfo item = new InEquipmentsInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt32(rd["ID"]);

            if (!Convert.IsDBNull(rd["ItemID"]))
                item.ItemID = Convert.ToInt64(rd["ItemID"]);

            if (!Convert.IsDBNull(rd["WarehouseID"]))
                item.WarehouseID = Convert.ToString(rd["WarehouseID"]);

            if (!Convert.IsDBNull(rd["IsAsset"]))
                item.IsAsset = Convert.ToBoolean(rd["IsAsset"]);

            if (!Convert.IsDBNull(rd["EquipmentNO"]))
                item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

            if (!Convert.IsDBNull(rd["ExpendableID"]))
                item.ExpendableID = Convert.ToInt64(rd["ExpendableID"]);

            if (!Convert.IsDBNull(rd["Count"]))
                item.Count = Convert.ToDecimal(rd["Count"]);

            if (!Convert.IsDBNull(rd["InTime"]))
                item.InTime = Convert.ToDateTime(rd["InTime"]);

            if (!Convert.IsDBNull(rd["Unit"]))
                item.Unit = Convert.ToString(rd["Unit"]);

            if (!Convert.IsDBNull(rd["Name"]))
            {
                item.Name = Convert.ToString(rd["Name"]);
            }

            if (!Convert.IsDBNull(rd["Model"]))
            {
                item.Model = Convert.ToString(rd["Model"]);
            }

            if (!Convert.IsDBNull(rd["ExpendableTypeID"]))
                item.ExpendableTypeID = Convert.ToInt64(rd["ExpendableTypeID"]);

            if (!Convert.IsDBNull(rd["ExpendablePrice"]))
                item.ExpendablePrice = Convert.ToDecimal(rd["ExpendablePrice"]);


            if (!Convert.IsDBNull(rd["ExpendableType"]))
                item.ExpendableType = Convert.ToString(rd["ExpendableType"]);
            return item;

        }

        #endregion
      

        /// <summary>
        /// 添加出库申请单
        /// </summary>
        /// <param name="model">出库申请单信息</param>
        /// <returns>新增的出库申请单流水号</returns>
        public long InsertOutWarehouseApply(OutWarehouseApplyInfo model)
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

                //先插入申请信息
                id = InsertOutWarehouseApply(trans, model);

                //插入申请明细信息
                if (model.ApplyDetailList != null)
                {
                    foreach (OutWarehouseDetailInfo item in model.ApplyDetailList)
                    {
                        item.ID = id;
                        item.ItemID = InsertOutWarehouseDetail(trans, item);
                    }
                }
                //事务提交
                trans.Commit();
                return id;
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

        /// <summary>
        /// 添加审批记录
        /// </summary>
        /// <param name="model">出库申请审批信息</param>
        /// <returns>审批记录流水号</returns>
        public long InsertApprovalRecord(OutWarehouseApprovalInfo model)
        {
            return InsertOutWarehouseApproval(null, model);
        }

        /// <summary>
        /// 更新出库申请单
        /// </summary>
        /// <param name="model">新的出库申请单信息</param>
        public void UpdateOutWarehouseApplyWithDetail(OutWarehouseApplyInfo model)
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
                UpdateOutWarehouseApply(trans, model);

                //先删除原来的明细，后添加新的明细
                DeleteOutWarehouseDetailInfoList(trans, model.ID);

                //插入申请明细信息
                if (model.ApplyDetailList != null)
                {
                    foreach (OutWarehouseDetailInfo item in model.ApplyDetailList)
                    {
                        item.ID = model.ID;
                        InsertOutWarehouseDetail(trans, item);
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

        /// <summary>
        /// 获取出库申请单
        /// </summary>
        /// <param name="id">出库申请单流水号</param>
        /// <returns>目标出库申请单全部信息</returns>
        public OutWarehouseApplyInfo GetOutWarehouseApplyInfo(long id)
        {
            SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString);
            OutWarehouseApplyInfo form = new OutWarehouseApplyInfo();
            try
            {
                form = GetOutWarehouseApply(conn, id);
                form.ApplyDetailList = GetOutWarehouseDetailInfoList(conn, id);
                form.ApprovalList = GetOutWarehouseApprovalInfoList(conn, id);
                foreach (OutWarehouseDetailInfo detail in form.ApplyDetailList)
                {
                    detail.OutEquipmentList = GetOutEquipmentInfoList(conn, detail.ItemID);
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

        /// <summary>
        /// 删除申请单信息
        /// </summary>
        /// <param name="id">申请单流水号</param>
        public void DeleteApplyInfo(long id)
        {
            DeleteOutWarehouseApplyInfo(null, id);
        }

        /// <summary>
        /// 更新主表信息，插入出库记录，并且更新设备位置和易耗品数量
        /// </summary>
        /// <param name="model">新的出库申请单信息</param>
        public void UpdateApplyInfoWithEquipmentInsertUpdate(OutWarehouseApplyInfo model)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            Equipment eqDAL = new Equipment();
            Expendable expDAL = new Expendable();
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先更新申请信息
                UpdateOutWarehouseApply(trans, model);

                //插入出库设备信息
                if (model.ApplyDetailList != null)
                {
                    foreach (OutWarehouseDetailInfo item in model.ApplyDetailList)
                    {
                        foreach (OutEquipmentsInfo eq in item.OutEquipmentList)
                        {
                            
                            //更新设备位置
                            if (eq.IsAsset)
                            {
                                eqDAL.UpdateEquipmentAddress(trans, eq.EquipmentNO, eq.AddressID, eq.DetailLocation);
                                InsertOutEquipment(trans, eq);
                            }
                            else
                            {
                                //更新易耗品数量
                                expDAL.AddExpendable(trans, model.CompanyID, eq.WarehouseID, eq.Name, eq.Model, eq.Unit, eq.Count * -1, 0, 0);
                            }
                        }
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

        #endregion


        //**********Modified by Xue    For V 3.1.2     2011-10-20****************************************************************************************************
        /// <summary>
        /// 检验是否在出库设备表中存在
        /// </summary>
        /// <param name="eqNo">设备条形码</param>
        /// <returns></returns>
        public bool ExistsOutEquipmentInfoByEquipmentNO(string eqNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from " + TABLE_OUT_EQUIPMENT + " ");
            strSql.Append(" where EquipmentNO=@EquipmentNO ");
            SqlParameter[] parameters = {
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50)};
            parameters[0].Value = eqNo;

            int flag = 0;
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    flag = Convert.ToInt32(rd[0].ToString());
                }
            }

            //考虑出库再入库，再出库的情况。
            if (flag == 0)
                return false;
            else
                return true;
        }



        //**********Modification Finished 2011-6-27**********************************************************************************************
    }
}
