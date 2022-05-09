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

namespace FM2E.SQLServerDAL.Maintain
{
    public class RoutineMaintainDetail:IRoutineMaintainDetail
    {
        public QueryParam GenerateSearchTerm(RoutineMaintainDetailInfo item)
        {
            string sqlSearch = "where 1=1";
            if (item.PlanID != 0)
                sqlSearch += " and PlanID ='" + item.PlanID + "'";
            //if (item.ProductName != "")
            //    sqlSearch += " and ProductName like '%" + item.ProductName + "%'";
            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_RoutineMaintainDetail a left join FM2E_System b on a.System = b.SystemID left join FM2E_SubSystem c on a.Subsystem = c.SubSystemID";
            searchTerm.ReturnFields = "a.*,b.SystemName as SystemName,c.SubSystemName as SubSystemName";
            searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by PlanID asc,ItemID asc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            if (searchTerm.Where == "")
            {
                searchTerm.TableName = "FM2E_RoutineMaintainDetail a left join FM2E_System b on a.System = b.SystemID left join FM2E_SubSystem c on a.Subsystem = c.SubSystemID";
                searchTerm.ReturnFields = "a.*,b.SystemName as SystemName,c.SubSystemName as SubSystemName";
                searchTerm.PageSize = 10;
                searchTerm.OrderBy = "order by PlanID asc,ItemID asc";
                searchTerm.Where = "";
            }
            return SQLHelper.GetObjectList(this.GetData, searchTerm, out recordCount);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void InsertRoutineMaintainDetail(RoutineMaintainDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_RoutineMaintainDetail(");
            strSql.Append("PlanID,MaintainPeriod,PeriodUnit,System,Subsystem,MaintainObject,MaintainContent,CheckStandard)");
            strSql.Append(" values (");
            strSql.Append("@PlanID,@MaintainPeriod,@PeriodUnit,@System,@Subsystem,@MaintainObject,@MaintainContent,@CheckStandard)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PlanID", SqlDbType.BigInt,8),
					new SqlParameter("@MaintainPeriod", SqlDbType.Int,4),
					new SqlParameter("@PeriodUnit", SqlDbType.TinyInt,1),
					new SqlParameter("@System", SqlDbType.VarChar,1),
					new SqlParameter("@Subsystem", SqlDbType.BigInt,8),
					new SqlParameter("@MaintainObject", SqlDbType.NVarChar,50),
					new SqlParameter("@MaintainContent", SqlDbType.NVarChar,200),
					new SqlParameter("@CheckStandard", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.PlanID;
            parameters[1].Value = model.MaintainPeriod;
            parameters[2].Value = (int)model.PeriodUnit;
            parameters[3].Value = model.System;
            parameters[4].Value = model.Subsystem;
            parameters[5].Value = model.MaintainObject;
            parameters[6].Value = model.MaintainContent;
            parameters[7].Value = model.CheckStandard;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception e)
                {
                    throw new DALException("添加例行保养计划明细信息失败", e);
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
        public void UpdateRoutineMaintainDetail(RoutineMaintainDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_RoutineMaintainDetail set ");
            strSql.Append("PlanID=@PlanID,");
            strSql.Append("MaintainPeriod=@MaintainPeriod,");
            strSql.Append("PeriodUnit=@PeriodUnit,");
            strSql.Append("System=@System,");
            strSql.Append("Subsystem=@Subsystem,");
            strSql.Append("MaintainObject=@MaintainObject,");
            strSql.Append("MaintainContent=@MaintainContent,");
            strSql.Append("CheckStandard=@CheckStandard");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PlanID", SqlDbType.BigInt,8),
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@MaintainPeriod", SqlDbType.Int,4),
					new SqlParameter("@PeriodUnit", SqlDbType.TinyInt,1),
					new SqlParameter("@System", SqlDbType.VarChar,1),
					new SqlParameter("@Subsystem", SqlDbType.BigInt,8),
					new SqlParameter("@MaintainObject", SqlDbType.NVarChar,50),
					new SqlParameter("@MaintainContent", SqlDbType.NVarChar,200),
					new SqlParameter("@CheckStandard", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.PlanID;
            parameters[1].Value = model.ItemID;
            parameters[2].Value = model.MaintainPeriod;
            parameters[3].Value = (int)model.PeriodUnit;
            parameters[4].Value = model.System;
            parameters[5].Value = model.Subsystem;
            parameters[6].Value = model.MaintainObject;
            parameters[7].Value = model.MaintainContent;
            parameters[8].Value = model.CheckStandard;

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
                    throw new DALException("更新例行保养计划明细信息失败", e);
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
        public void DelRoutineMaintainDetail(long ItemID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_RoutineMaintainDetail ");
                strSql.Append(" where ItemID=@ItemID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
                parameters[0].Value = ItemID;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除例行保养计划明细信息失败", e);
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RoutineMaintainDetailInfo GetRoutineMaintainDetail(long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 a.*,b.SystemName as SystemName,c.SubSystemName as SubSystemName from FM2E_RoutineMaintainDetail a left join FM2E_System b on a.System = b.SystemID left join FM2E_SubSystem c on a.Subsystem = c.SubSystemID ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            RoutineMaintainDetailInfo item = new RoutineMaintainDetailInfo();
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
                throw new DALException("获取例行保养计划明细信息失败", e);
            }
            return item;
        }
        private RoutineMaintainDetailInfo GetData(IDataReader rd)
        {
            RoutineMaintainDetailInfo item = new RoutineMaintainDetailInfo();

            if (!Convert.IsDBNull(rd["PlanID"]))
                item.PlanID = Convert.ToInt64(rd["PlanID"]);

            if (!Convert.IsDBNull(rd["ItemID"]))
                item.ItemID = Convert.ToInt64(rd["ItemID"]);

            if (!Convert.IsDBNull(rd["MaintainPeriod"]))
                item.MaintainPeriod = Convert.ToInt32(rd["MaintainPeriod"]);

            if (!Convert.IsDBNull(rd["PeriodUnit"]))
                item.PeriodUnit = (RoutineMaintainPeriodUnit)Convert.ToInt32(rd["PeriodUnit"]);

            if (!Convert.IsDBNull(rd["System"]))
                item.System = Convert.ToString(rd["System"]);

            if (!Convert.IsDBNull(rd["Subsystem"]))
                item.Subsystem = Convert.ToInt64(rd["Subsystem"]);

            if (!Convert.IsDBNull(rd["SystemName"]))
                item.SystemName = Convert.ToString(rd["SystemName"]);

            if (!Convert.IsDBNull(rd["SubsystemName"]))
                item.SubsystemName = Convert.ToString(rd["SubsystemName"]);

            if (!Convert.IsDBNull(rd["MaintainObject"]))
                item.MaintainObject = Convert.ToString(rd["MaintainObject"]);

            if (!Convert.IsDBNull(rd["MaintainContent"]))
                item.MaintainContent = Convert.ToString(rd["MaintainContent"]);

            if (!Convert.IsDBNull(rd["CheckStandard"]))
                item.CheckStandard = Convert.ToString(rd["CheckStandard"]);   

            return item;

        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(string strWhere)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select ID,Result,FeedBack,OutTime,Receiver,Operator,RoutineMaintainRemark,IsDeleted,OrderID,WarehouseID,ApplyTime,ApplyRemark,CompanyID,Applicant,Approvaler,Status ");
        //    strSql.Append(" FROM FM2E_RoutineMaintainDetail ");
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where " + strWhere);
        //    }
        //    return DbHelperSQL.Query(strSql.ToString());
        //}
    }
}
