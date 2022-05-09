using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using FM2E.IDAL.Maintain;
using FM2E.Model.Maintain;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Utils;
using System.Collections;
using FM2E.Model.Exceptions;
using FM2E.Model.Equipment;

namespace FM2E.SQLServerDAL.Maintain
{
    public class DailyPatrolConfig : IDailyPatrolConfig
    {
        public QueryParam GenerateSearchTerm(DailyPatrolConfigInfo item)
        {
            string sqlSearch = "where 1=1";
            if (item.Subsystem != 0)
                sqlSearch += " and Subsystem ='" + item.Subsystem + "'";

            if (item.System != null && item.System != "")
                sqlSearch += " and System ='" + item.System + "'";

            if (item.PatrolObject != null && item.PatrolObject != "")
                sqlSearch += " and PatrolObject like '%" + item.PatrolObject + "%'";
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_DailyPatrolConfig a left join FM2E_System b on a.System = b.SystemID left join FM2E_SubSystem c on a.Subsystem = c.SubSystemID left join FM2E_Company g on a.CompanyID = g.CompanyID";
            searchTerm.ReturnFields = "a.*,b.SystemName as SystemName,c.SubSystemName as SubSystemName,g.CompanyName";
            searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by ItemID desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            return SQLHelper.GetObjectList(this.GetData, searchTerm, out recordCount);
        }
        public QueryParam GenerateSearchTermForEquipmentList(DailyPatrolConfigInfo item)
        {
            string sqlSearch = "where 1=1";
            if (item.ItemID != 0)
                sqlSearch += " and a.ItemID ='" + item.ItemID + "'";
           
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_Equipment b left join FM2E_MaintainPlanEquipment a on a.EquipmentNO = b.EquipmentNO ";
            searchTerm.ReturnFields = "b.*";
            searchTerm.PageSize = 10;
            searchTerm.OrderBy = " order by UpdateTime DESC";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        public QueryParam GenerateSearchTermForEquipmentAddressList(DailyPatrolConfigInfo item,string addresscode)
        {
            int len = addresscode.Length;
            string sqlSearch = "where 1=1";
            if (item.ItemID != 0)
                sqlSearch += " and a.ItemID ='" + item.ItemID + "'";
            if (addresscode != "")
                sqlSearch += "and left(c.AddressCode,"+ len + ")"+ " like '%" + addresscode + "%'";

            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_Equipment b left join FM2E_MaintainPlanEquipment a on a.EquipmentNO = b.EquipmentNO left join FM2E_Address c on b.AddressID = c.ID ";
            searchTerm.ReturnFields = "b.*";
            searchTerm.PageSize = 10;
            searchTerm.OrderBy = " order by UpdateTime DESC";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        public IList GetListForEquipmentList(QueryParam searchTerm, out int recordCount)
        {
            return SQLHelper.GetObjectList(this.GetDataForEquipmentList, searchTerm, out recordCount);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void InsertDailyPatrolConfig(DailyPatrolConfigInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_DailyPatrolConfig(");
            strSql.Append("PatrolPeriod,PeriodUnit,System,Subsystem,PatrolObject,PatrolContent,CheckStandard,CompanyID)");
            strSql.Append(" values (");
            strSql.Append("@PatrolPeriod,@PeriodUnit,@System,@Subsystem,@PatrolObject,@PatrolContent,@CheckStandard,@CompanyID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PatrolPeriod", SqlDbType.Int,4),
					new SqlParameter("@PeriodUnit", SqlDbType.TinyInt,1),
					new SqlParameter("@System", SqlDbType.VarChar,2),
					new SqlParameter("@Subsystem", SqlDbType.BigInt,8),
					new SqlParameter("@PatrolObject", SqlDbType.NVarChar,50),
					new SqlParameter("@PatrolContent", SqlDbType.NVarChar,200),
					new SqlParameter("@CheckStandard", SqlDbType.NVarChar,200),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2)};
            parameters[0].Value = model.PatrolPeriod;
            parameters[1].Value = model.PeriodUnit;
            parameters[2].Value = model.System;
            parameters[3].Value = model.Subsystem;
            parameters[4].Value = model.PatrolObject;
            parameters[5].Value = model.PatrolContent;
            parameters[6].Value = model.CheckStandard;
            parameters[7].Value = model.CompanyID;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception e)
                {
                    throw new DALException("添加日常巡查计划明细信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateDailyPatrolConfig(DailyPatrolConfigInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_DailyPatrolConfig set ");
            strSql.Append("PatrolPeriod=@PatrolPeriod,");
            strSql.Append("PeriodUnit=@PeriodUnit,");
            strSql.Append("System=@System,");
            strSql.Append("Subsystem=@Subsystem,");
            strSql.Append("PatrolObject=@PatrolObject,");
            strSql.Append("PatrolContent=@PatrolContent,");
            strSql.Append("CheckStandard=@CheckStandard,");
            strSql.Append("CompanyID=@CompanyID");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@PatrolPeriod", SqlDbType.Int,4),
					new SqlParameter("@PeriodUnit", SqlDbType.TinyInt,1),
					new SqlParameter("@System", SqlDbType.VarChar,2),
					new SqlParameter("@Subsystem", SqlDbType.BigInt,8),
					new SqlParameter("@PatrolObject", SqlDbType.NVarChar,50),
					new SqlParameter("@PatrolContent", SqlDbType.NVarChar,200),
					new SqlParameter("@CheckStandard", SqlDbType.NVarChar,200),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2)};
            parameters[0].Value = model.ItemID;
            parameters[1].Value = model.PatrolPeriod;
            parameters[2].Value = model.PeriodUnit;
            parameters[3].Value = model.System;
            parameters[4].Value = model.Subsystem;
            parameters[5].Value = model.PatrolObject;
            parameters[6].Value = model.PatrolContent;
            parameters[7].Value = model.CheckStandard;
            parameters[8].Value = model.CompanyID;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                    //if (result == 0)
                    //    throw new Exception("没有更新任何数据项");
                }
                catch (Exception e)
                {
                    throw new DALException("更新日常巡查计划明细信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DelDailyPatrolConfig(long ItemID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_DailyPatrolConfig ");
                strSql.Append(" where ItemID=@ItemID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
                parameters[0].Value = ItemID;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除日常巡查计划明细信息失败", e);
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DailyPatrolConfigInfo GetDailyPatrolConfig(long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 a.*,b.SystemName as SystemName,c.SubSystemName as SubSystemName,g.CompanyName from FM2E_DailyPatrolConfig  a left join FM2E_System b on a.System = b.SystemID left join FM2E_SubSystem c on a.Subsystem = c.SubSystemID left join FM2E_Company g on a.CompanyID = g.CompanyID");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            DailyPatrolConfigInfo item = new DailyPatrolConfigInfo();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        item = this.GetData(rd);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取日常巡查计划明细信息失败", e);
            }
            return item;
        }
        private DailyPatrolConfigInfo GetData(IDataReader rd)
        {
            DailyPatrolConfigInfo item = new DailyPatrolConfigInfo();

            if (!Convert.IsDBNull(rd["ItemID"]))
                item.ItemID = Convert.ToInt64(rd["ItemID"]);

            if (!Convert.IsDBNull(rd["PatrolPeriod"]))
                item.PatrolPeriod = Convert.ToInt32(rd["PatrolPeriod"]);

            if (!Convert.IsDBNull(rd["PeriodUnit"]))
                item.PeriodUnit = (DailyPatrolPeriodUnit)Convert.ToInt32(rd["PeriodUnit"]);

            if (!Convert.IsDBNull(rd["System"]))
                item.System = Convert.ToString(rd["System"]);

            if (!Convert.IsDBNull(rd["Subsystem"]))
                item.Subsystem = Convert.ToInt64(rd["Subsystem"]);

            if (!Convert.IsDBNull(rd["SystemName"]))
                item.SystemName = Convert.ToString(rd["SystemName"]);

            if (!Convert.IsDBNull(rd["SubsystemName"]))
                item.SubsystemName = Convert.ToString(rd["SubsystemName"]);

            if (!Convert.IsDBNull(rd["PatrolObject"]))
                item.PatrolObject = Convert.ToString(rd["PatrolObject"]);

            if (!Convert.IsDBNull(rd["PatrolContent"]))
                item.PatrolContent = Convert.ToString(rd["PatrolContent"]);

            if (!Convert.IsDBNull(rd["CheckStandard"]))
                item.CheckStandard = Convert.ToString(rd["CheckStandard"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["CompanyName"]))
                item.CompanyName = Convert.ToString(rd["CompanyName"]);

            return item;

        }
        private EquipmentInfoFacade GetDataForEquipmentList(IDataReader dr)
        {
             EquipmentInfoFacade item = new EquipmentInfoFacade();
            
            if (!Convert.IsDBNull(dr["Model"]))
                item.Model = Convert.ToString(dr["Model"]);

            if (!Convert.IsDBNull(dr["Status"]))
                item.Status = (EquipmentStatus)Convert.ToInt64(dr["Status"]);
            
            if (!Convert.IsDBNull(dr["EquipmentNO"]))
                item.EquipmentNO = Convert.ToString(dr["EquipmentNO"]);
            
            if (!Convert.IsDBNull(dr["Name"]))
                item.Name = Convert.ToString(dr["Name"]);
            return item;
        }
        public IList GetAllList(DailyPatrolConfigInfo item)
        {
            string sqlSearch = "where 1=1";
            if (item.Subsystem != 0)
                sqlSearch += " and Subsystem ='" + item.Subsystem + "'";

            if (item.System != null && item.System != "")
                sqlSearch += " and System ='" + item.System + "'";

            if (item.PatrolObject != null && item.PatrolObject != "")
                sqlSearch += " and PatrolObject like '%" + item.PatrolObject + "%'";

            string sql = string.Format("select a.*,b.SystemName as SystemName,c.SubSystemName as SubSystemName,g.CompanyName from FM2E_DailyPatrolConfig a left join FM2E_System b on a.System = b.SystemID left join FM2E_SubSystem c on a.Subsystem = c.SubSystemID left join FM2E_Company g on a.CompanyID = g.CompanyID {0}", sqlSearch);

            ArrayList list = new ArrayList();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
                {
                    while (rd.Read())
                    {
                        DailyPatrolConfigInfo i = this.GetData(rd);
                        list.Add(i);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取日常巡查计划明细信息失败", e);
            }
            return list;
        }

        public IList GetAllEquipmentByItemIDandAddessCode(long itemID, string AddressCode)
        {
            if(AddressCode == "" || AddressCode == null)
            {
                return GetAllEquipmentByItemID(itemID);
            }
            else
            {
                int len = AddressCode.Length;
                string sql = string.Format("select b.EquipmentNO from FM2E_Equipment b left join FM2E_MaintainPlanEquipment a on a.EquipmentNO = b.EquipmentNO left join FM2E_Address c on b.AddressID = c.ID where a.ItemID='{0}' and left(c.AddressCode,'{1}') like '%{2}%' " , itemID , len , AddressCode);
                ArrayList list = new ArrayList();
                string EquipmentNo = "";
                try
                {
                    using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
                    {
                        while (rd.Read())
                        {
                            if (!Convert.IsDBNull(rd["EquipmentNO"]))
                                EquipmentNo = Convert.ToString(rd["EquipmentNO"]);
                            list.Add(EquipmentNo);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new DALException("获取日常巡查计划明细信息失败", e);
                }
                return list;
            }
        }

        public IList GetAllEquipmentByItemID(long itemID)
        {
            string sql = string.Format("select EquipmentNO from FM2E_MaintainPlanEquipment where TableName='FM2E_DailyPatrolConfig' and ItemID='{0}'", itemID);
            ArrayList list = new ArrayList();
            string item = "";
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
                {
                    while (rd.Read())
                    {
                        if (!Convert.IsDBNull(rd["EquipmentNO"]))
                            item = Convert.ToString(rd["EquipmentNO"]);
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
        public void UpdateEquipments(DailyPatrolConfigInfo model)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();
                //先删除原来的明细，后添加新的明细
                StringBuilder delSql = new StringBuilder();
                delSql.AppendFormat("delete FM2E_MaintainPlanEquipment");
                delSql.Append(" where ItemID=@ItemID and TableName='FM2E_DailyPatrolConfig'");
                SqlParameter[] delPara = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
                delPara[0].Value = model.ItemID;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, delSql.ToString(), delPara);

                //插入数据
                if (model.EquipmentList != null)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into FM2E_MaintainPlanEquipment(");
                    strSql.Append("TableName,ItemID,EquipmentNO)");
                    strSql.Append(" values (");
                    strSql.Append("@TableName,@ItemID,@EquipmentNO)");
                    SqlParameter[] parameters = {
					new SqlParameter("@TableName", SqlDbType.VarChar,50),
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50)};
                    parameters[0].Value = "FM2E_DailyPatrolConfig";
                    parameters[1].Value = model.ItemID;
                    foreach (string equipment in model.EquipmentList)
                    {
                        parameters[2].Value = equipment;
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
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
        public IList GetDailyPatrolConfigByEquipmentNO(string EquipmentNO)
        {
            string sql = string.Format("select a.*,b.SystemName as SystemName,c.SubSystemName as SubSystemName,g.CompanyName from FM2E_MaintainPlanEquipment s1 left join FM2E_DailyPatrolConfig a on a.ItemID = s1.ItemID left join FM2E_System b on a.System = b.SystemID left join FM2E_SubSystem c on a.Subsystem = c.SubSystemID left join FM2E_Company g on a.CompanyID = g.CompanyID where s1.tablename='FM2E_DailyPatrolConfig' and s1.EquipmentNO ='{0}'", EquipmentNO);
            ArrayList list = new ArrayList();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
                {
                    while (rd.Read())
                    {
                        list.Add(GetData(rd));
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取日常巡查计划明细信息失败", e);
            }
            return list;
        }
        public void DelDailyPatrolPlanEquipment(string EquipmentNO, long ItemID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_MaintainPlanEquipment ");
                strSql.Append(" where TableName=@TableName and ItemID=@ItemID and EquipmentNO=@EquipmentNO ");
                SqlParameter[] parameters = {
					new SqlParameter("@TableName", SqlDbType.VarChar,50),
					new SqlParameter("@ItemID", SqlDbType.BigInt),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50)};
                parameters[0].Value = "FM2E_DailyPatrolConfig";
                parameters[1].Value = ItemID;
                parameters[2].Value = EquipmentNO;
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除日常巡查计划明细信息失败", e);
            }
        }
        public void InsertDailyPatrolPlanEquipment(string EquipmentNO, long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from FM2E_MaintainPlanEquipment ");
            strSql.Append(" where TableName=@TableName and ItemID=@ItemID and EquipmentNO=@EquipmentNO ");
            SqlParameter[] parameters = {
					new SqlParameter("@TableName", SqlDbType.VarChar,50),
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50)};
            parameters[0].Value = "FM2E_DailyPatrolConfig";
            parameters[1].Value = ItemID;
            parameters[2].Value = EquipmentNO;
            SqlDataReader rdr = null;
            int id = 0;
            try
            {
                using (rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rdr.Read())
                    {
                        if (!Convert.IsDBNull(rdr[0]))
                            id = Convert.ToInt32(rdr[0]);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("查询日常巡查计划信息失败", e);
            }
            if (id == 0)
            {
                strSql = new StringBuilder();
                strSql.Append("insert into FM2E_MaintainPlanEquipment(");
                strSql.Append("TableName,ItemID,EquipmentNO)");
                strSql.Append(" values (");
                strSql.Append("@TableName,@ItemID,@EquipmentNO)");
                using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
                {
                    conn.Open();
                    try
                    {
                        int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                    }
                    catch (Exception e)
                    {
                        throw new DALException("插入日常巡查计划明细信息失败", e);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }
    }
}
