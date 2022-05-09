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
    public class RoutineMaintainRecord : IRoutineMaintainRecord
    {
        public QueryParam GenerateSearchTerm(RoutineMaintainRecordInfo item)
        {
            string sqlSearch = "where 1=1";
            if (item.PlanID != 0)
                sqlSearch += " and a.PlanID ='" + item.PlanID + "'";

            if (item.ItemID != 0)
                sqlSearch += " and a.ItemID ='" + item.ItemID + "'";

            if (item.MaintainmanName != "")
                sqlSearch += " and a.MaintainmanName like '%" + item.MaintainmanName + "%'";

            if (item.CompanyID != null && item.CompanyID != "")
            {
                sqlSearch += " and d.CompanyID='" + item.CompanyID + "'";
            }

            QueryParam searchTerm = new QueryParam();
            searchTerm.TableName = "FM2E_RoutineMaintainRecord a left join FM2E_User b on a.MaintainmanID=b.UserName left join FM2E_User c on a.VerifyBy=c.UserName left join FM2E_RoutineMaintainPlan d on a.PlanID=d.PlanID";
            searchTerm.ReturnFields = "a.*,b.PersonName as MaintainmanName,c.PersonName as VerifyByName";
            searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by MaintainDate desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        public QueryParam GenerateSearchTerm1(string system, long subsystem, string EquipmentNO)
        {
            QueryParam searchTerm = new QueryParam();
            string sqlSearch = "where 1=1";
            if (subsystem != 0)
                sqlSearch += " and d.Subsystem ='" + subsystem.ToString() + "'";

            if (system != "")
                sqlSearch += " and d.System ='" + system + "'";

            if (EquipmentNO != "")
            {
                sqlSearch += " and e.EquipmentNO ='" + EquipmentNO + "'";
                sqlSearch += " and e.TableName='FM2E_RoutineMaintainRecord'";
                searchTerm.TableName = "FM2E_MaintainRecordEquipment e left join FM2E_RoutineMaintainRecord a on e.RecordID = a.RecordID left join FM2E_User b on a.MaintainmanID=b.UserName left join FM2E_User c on a.VerifyBy=c.UserName left join FM2E_RoutineMaintainDetail d on a.ItemID = d.ItemID ";
                searchTerm.ReturnFields = "a.*,b.PersonName as MaintainmanName,c.PersonName as VerifyByName,e.EquipmentNO as RealEquipment,e.Name,e.Model";
            }
            else
            {
                searchTerm.TableName = "FM2E_RoutineMaintainRecord a left join FM2E_User b on a.MaintainmanID=b.UserName left join FM2E_User c on a.VerifyBy=c.UserName left join FM2E_RoutineMaintainDetail d on a.ItemID = d.ItemID left join FM2E_MaintainRecordEquipment e on e.Model = a.EquipmentNO ";
                searchTerm.ReturnFields = "a.*,b.PersonName as MaintainmanName,c.PersonName as VerifyByName,e.EquipmentNO as RealEquipment,e.Name,e.Model";
            }
            searchTerm.PageSize = 10;
            searchTerm.OrderBy = "order by MaintainDate desc";
            searchTerm.Where = sqlSearch;
            return searchTerm;
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            if (searchTerm.Where == "")
            {
                searchTerm.TableName = "FM2E_RoutineMaintainRecord a left join FM2E_User b on a.MaintainmanID=b.UserName left join FM2E_User c on a.VerifyBy=c.UserName";
                searchTerm.ReturnFields = "a.*,b.PersonName as MaintainmanName,c.PersonName as VerifyByName";
                searchTerm.PageSize = 10;
                searchTerm.OrderBy = "order by MaintainDate desc";
                searchTerm.Where = "";
            }
            return SQLHelper.GetObjectList(this.GetData, searchTerm, out recordCount);
        }
        public IList GetList1(QueryParam searchTerm, out int recordCount)
        {
            return SQLHelper.GetObjectList(this.GetData1, searchTerm, out recordCount);
        }
        /// <summary>
        /// 获取当前记录之前的记录的保养时间
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GetTheLastRecord(RoutineMaintainRecordInfo info)
        {
            string thisRecordID = info.RecordID.ToString();
            string thisdate = info.MaintainDate.ToString();
            string planid = info.PlanID.ToString();
            string itemid = info.ItemID.ToString();
            string lastDate = null;
            string sql = string.Format("select max(MaintainDate) from FM2E_RoutineMaintainRecord where PlanID='{0}' and ItemID='{1}' and MaintainDate <='{2}' and RecordID!='{3}'", planid, itemid, thisdate, thisRecordID);
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
                {
                    if (rd.Read())
                    {
                        if (!Convert.IsDBNull(rd[0]))
                        {
                            lastDate = Convert.ToDateTime(rd[0]).ToString();
                        }
                        else
                        {
                            lastDate = string.Empty;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取例行保养记录信息失败", e);
            }
            return lastDate;
        }
        public IList GetAllRecord(long itemID)
        {
            ArrayList list = new ArrayList();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,b.PersonName as MaintainmanName,c.PersonName as VerifyByName from FM2E_RoutineMaintainRecord a left join FM2E_User b on a.MaintainmanID=b.UserName left join FM2E_User c on a.VerifyBy=c.UserName ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = itemID;
            RoutineMaintainRecordInfo item = new RoutineMaintainRecordInfo();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        item = this.GetData(rd);
                        list.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取例行保养记录信息失败", e);
            }
            return list;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void InsertRoutineMaintainRecord(RoutineMaintainRecordInfo model)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            long thisID = 0;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先插入申请信息
                thisID = InsertRecord(trans, model);

                //插入申请明细信息
                if (model.EquipmentList != null)
                {
                    foreach (MaintainRecordEquipmentInfo item in model.EquipmentList)
                    {
                        item.RecordID = thisID;
                        item.TableName = "FM2E_RoutineMaintainRecord";
                        InsertEquipments(trans, item);
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
        private long InsertRecord(SqlTransaction trans, RoutineMaintainRecordInfo model)
        {
            long thisid = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_RoutineMaintainRecord(");
            strSql.Append("VerifiedResult,VerifyBy,VerifyRemark,MaintainDate,EquipmentNO,PlanID,ItemID,MaintainObject,MaintainContent,MaintainResult,MaintainmanID)");
            strSql.Append(" values (");
            strSql.Append("@VerifiedResult,@VerifyBy,@VerifyRemark,@MaintainDate,@EquipmentNO,@PlanID,@ItemID,@MaintainObject,@MaintainContent,@MaintainResult,@MaintainmanID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@VerifiedResult", SqlDbType.TinyInt,1),
					new SqlParameter("@VerifyBy", SqlDbType.VarChar,20),
					new SqlParameter("@VerifyRemark", SqlDbType.NVarChar,100),
					new SqlParameter("@MaintainDate", SqlDbType.DateTime),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
					new SqlParameter("@PlanID", SqlDbType.BigInt,8),
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@MaintainObject", SqlDbType.NVarChar,50),
					new SqlParameter("@MaintainContent", SqlDbType.NVarChar,200),
					new SqlParameter("@MaintainResult", SqlDbType.NVarChar,200),
					new SqlParameter("@MaintainmanID", SqlDbType.VarChar,20)};
            parameters[0].Value = (int)model.VerifiedResult;
            parameters[1].Value = model.VerifyBy;
            parameters[2].Value = model.VerifyRemark;
            parameters[3].Value = model.MaintainDate;
            parameters[4].Value = model.EquipmentNO;
            parameters[5].Value = model.PlanID;
            parameters[6].Value = model.ItemID;
            parameters[7].Value = model.MaintainObject;
            parameters[8].Value = model.MaintainContent;
            parameters[9].Value = model.MaintainResult;
            parameters[10].Value = model.MaintainmanID;
            SqlDataReader rdr = null;
            try
            {
                using (rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        if (!Convert.IsDBNull(rdr[0]))
                            thisid = Convert.ToInt64(rdr[0]);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("添加例行保养记录信息失败", e);
            }
            return thisid;
        }
        private void InsertEquipments(SqlTransaction trans, MaintainRecordEquipmentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_MaintainRecordEquipment(");
            strSql.Append("TableName,RecordID,EquipmentNO,Name,Model)");
            strSql.Append(" values (");
            strSql.Append("@TableName,@RecordID,@EquipmentNO,@Name,@Model)");
            SqlParameter[] parameters = {
					new SqlParameter("@TableName", SqlDbType.VarChar,50),
					new SqlParameter("@RecordID", SqlDbType.BigInt,8),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@Model", SqlDbType.VarChar,20)};
            parameters[0].Value = model.TableName;
            parameters[1].Value = model.RecordID;
            parameters[2].Value = model.EquipmentNO;
            parameters[3].Value = model.Name;
            parameters[4].Value = model.Model;
            try
            {
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("添加例行保养记录信息失败", e);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateRoutineMaintainRecord(RoutineMaintainRecordInfo model)
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
                updateRecord(trans, model);

                //先删除原来的明细，后添加新的明细
                StringBuilder delSql = new StringBuilder();
                delSql.AppendFormat("delete FM2E_MaintainRecordEquipment");
                delSql.Append(" where RecordID=@RecordID and TableName='FM2E_RoutineMaintainRecord'");
                SqlParameter[] delPara = {
					new SqlParameter("@RecordID", SqlDbType.BigInt)};
                delPara[0].Value = model.RecordID;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, delSql.ToString(), delPara);

                //插入申请明细信息
                if (model.EquipmentList != null)
                {
                    foreach (MaintainRecordEquipmentInfo item in model.EquipmentList)
                    {
                        item.RecordID = model.RecordID;
                        item.TableName = "FM2E_RoutineMaintainRecord";
                        InsertEquipments(trans, item);
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
        private void updateRecord(SqlTransaction trans, RoutineMaintainRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_RoutineMaintainRecord set ");
            strSql.Append("VerifiedResult=@VerifiedResult,");
            strSql.Append("VerifyBy=@VerifyBy,");
            strSql.Append("VerifyRemark=@VerifyRemark,");
            strSql.Append("MaintainDate=@MaintainDate,");
            strSql.Append("EquipmentNO=@EquipmentNO,");
            strSql.Append("PlanID=@PlanID,");
            strSql.Append("ItemID=@ItemID,");
            strSql.Append("MaintainObject=@MaintainObject,");
            strSql.Append("MaintainContent=@MaintainContent,");
            strSql.Append("MaintainResult=@MaintainResult,");
            strSql.Append("MaintainmanID=@MaintainmanID");
            strSql.Append(" where RecordID=@RecordID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.BigInt,8),
					new SqlParameter("@VerifiedResult", SqlDbType.TinyInt,1),
					new SqlParameter("@VerifyBy", SqlDbType.VarChar,20),
					new SqlParameter("@VerifyRemark", SqlDbType.NVarChar,100),
					new SqlParameter("@MaintainDate", SqlDbType.DateTime),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
					new SqlParameter("@PlanID", SqlDbType.BigInt,8),
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@MaintainObject", SqlDbType.NVarChar,50),
					new SqlParameter("@MaintainContent", SqlDbType.NVarChar,200),
					new SqlParameter("@MaintainResult", SqlDbType.NVarChar,200),
					new SqlParameter("@MaintainmanID", SqlDbType.VarChar,20)};
            parameters[0].Value = model.RecordID;
            parameters[1].Value = (int)model.VerifiedResult;
            parameters[2].Value = model.VerifyBy;
            parameters[3].Value = model.VerifyRemark;
            parameters[4].Value = model.MaintainDate;
            parameters[5].Value = model.EquipmentNO;
            parameters[6].Value = model.PlanID;
            parameters[7].Value = model.ItemID;
            parameters[8].Value = model.MaintainObject;
            parameters[9].Value = model.MaintainContent;
            parameters[10].Value = model.MaintainResult;
            parameters[11].Value = model.MaintainmanID;
            try
            {
                int result = SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("更新例行保养记录信息失败", e);
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DelRoutineMaintainRecord(long RecordID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_RoutineMaintainRecord ");
                strSql.Append(" where RecordID=@RecordID ");
                SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.BigInt)};
                parameters[0].Value = RecordID;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
                StringBuilder delSql = new StringBuilder();
                delSql.AppendFormat("delete FM2E_MaintainRecordEquipment");
                delSql.Append(" where RecordID=@RecordID and TableName='FM2E_RoutineMaintainRecord'");
                SqlParameter[] delPara = {
					new SqlParameter("@RecordID", SqlDbType.BigInt)};
                delPara[0].Value = RecordID;
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, delSql.ToString(), delPara);
            }
            catch (Exception e)
            {
                throw new DALException("删除例行保养记录信息失败", e);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RoutineMaintainRecordInfo GetRoutineMaintainRecord(long RecordID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 a.*,b.PersonName as MaintainmanName,c.PersonName as VerifyByName from FM2E_RoutineMaintainRecord a left join FM2E_User b on a.MaintainmanID=b.UserName left join FM2E_User c on a.VerifyBy=c.UserName ");
            strSql.Append(" where RecordID=@RecordID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.BigInt)};
            parameters[0].Value = RecordID;
            RoutineMaintainRecordInfo item = new RoutineMaintainRecordInfo();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        item = this.GetData(rd);
                    }
                }
                if (item == null) return null;
                //获取计划明细列表
                StringBuilder strDetailSql = new StringBuilder();
                strDetailSql.Append("select * from FM2E_MaintainRecordEquipment");
                strDetailSql.Append(" where RecordID=@RecordID and TableName='FM2E_RoutineMaintainRecord'");
                ArrayList list = new ArrayList();
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strDetailSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        MaintainRecordEquipmentInfo i = GetEquipmentData(rd);
                        list.Add(i);
                    }
                }
                item.EquipmentList = list;
            }
            catch (Exception e)
            {
                throw new DALException("获取例行保养记录信息失败", e);
            }
            return item;
        }

        private MaintainRecordEquipmentInfo GetEquipmentData(IDataReader rd)
        {
            MaintainRecordEquipmentInfo item = new MaintainRecordEquipmentInfo();
            if (!Convert.IsDBNull(rd["RecordID"]))
                item.RecordID = Convert.ToInt64(rd["RecordID"]);
            if (!Convert.IsDBNull(rd["TableName"]))
                item.TableName = Convert.ToString(rd["TableName"]);
            if (!Convert.IsDBNull(rd["EquipmentNO"]))
                item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);
            if (!Convert.IsDBNull(rd["Name"]))
                item.Name = Convert.ToString(rd["Name"]);
            if (!Convert.IsDBNull(rd["Model"]))
                item.Model = Convert.ToString(rd["Model"]);
            return item;
        }

        private RoutineMaintainRecordInfo GetData(IDataReader rd)
        {
            RoutineMaintainRecordInfo item = new RoutineMaintainRecordInfo();

            if (!Convert.IsDBNull(rd["RecordID"]))
                item.RecordID = Convert.ToInt64(rd["RecordID"]);

            if (!Convert.IsDBNull(rd["VerifiedResult"]))
                item.VerifiedResult = (RoutineMaintainVerifiedResult)Convert.ToInt32(rd["VerifiedResult"]);

            if (!Convert.IsDBNull(rd["VerifyBy"]))
                item.VerifyBy = Convert.ToString(rd["VerifyBy"]);

            if (!Convert.IsDBNull(rd["VerifyRemark"]))
                item.VerifyRemark = Convert.ToString(rd["VerifyRemark"]);

            if (!Convert.IsDBNull(rd["MaintainDate"]))
                item.MaintainDate = Convert.ToDateTime(rd["MaintainDate"]);

            if (!Convert.IsDBNull(rd["EquipmentNO"]))
                item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

            if (!Convert.IsDBNull(rd["PlanID"]))
                item.PlanID = Convert.ToInt64(rd["PlanID"]);

            if (!Convert.IsDBNull(rd["ItemID"]))
                item.ItemID = Convert.ToInt64(rd["ItemID"]);

            if (!Convert.IsDBNull(rd["MaintainObject"]))
                item.MaintainObject = Convert.ToString(rd["MaintainObject"]);

            if (!Convert.IsDBNull(rd["MaintainContent"]))
                item.MaintainContent = Convert.ToString(rd["MaintainContent"]);

            if (!Convert.IsDBNull(rd["MaintainResult"]))
                item.MaintainResult = Convert.ToString(rd["MaintainResult"]);

            if (!Convert.IsDBNull(rd["MaintainmanID"]))
                item.MaintainmanID = Convert.ToString(rd["MaintainmanID"]);

            if (!Convert.IsDBNull(rd["MaintainmanName"]))
                item.MaintainmanName = Convert.ToString(rd["MaintainmanName"]);

            if (!Convert.IsDBNull(rd["VerifyByName"]))
                item.VerifyName = Convert.ToString(rd["VerifyByName"]);

            return item;
        }
        private RoutineMaintainRecordInfo GetData1(IDataReader rd)
        {
            RoutineMaintainRecordInfo item = new RoutineMaintainRecordInfo();

            if (!Convert.IsDBNull(rd["RecordID"]))
                item.RecordID = Convert.ToInt64(rd["RecordID"]);

            if (!Convert.IsDBNull(rd["VerifiedResult"]))
                item.VerifiedResult = (RoutineMaintainVerifiedResult)Convert.ToInt32(rd["VerifiedResult"]);

            if (!Convert.IsDBNull(rd["VerifyBy"]))
                item.VerifyBy = Convert.ToString(rd["VerifyBy"]);

            if (!Convert.IsDBNull(rd["VerifyRemark"]))
                item.VerifyRemark = Convert.ToString(rd["VerifyRemark"]);

            if (!Convert.IsDBNull(rd["MaintainDate"]))
                item.MaintainDate = Convert.ToDateTime(rd["MaintainDate"]);

            if (!Convert.IsDBNull(rd["RealEquipment"]))
                item.EquipmentNO = Convert.ToString(rd["RealEquipment"]);

            if (!Convert.IsDBNull(rd["PlanID"]))
                item.PlanID = Convert.ToInt64(rd["PlanID"]);

            if (!Convert.IsDBNull(rd["ItemID"]))
                item.ItemID = Convert.ToInt64(rd["ItemID"]);

            if (!Convert.IsDBNull(rd["MaintainObject"]))
                item.MaintainObject = Convert.ToString(rd["MaintainObject"]);

            if (!Convert.IsDBNull(rd["MaintainContent"]))
                item.MaintainContent = Convert.ToString(rd["MaintainContent"]);

            if (!Convert.IsDBNull(rd["MaintainResult"]))
                item.MaintainResult = Convert.ToString(rd["MaintainResult"]);

            if (!Convert.IsDBNull(rd["MaintainmanID"]))
                item.MaintainmanID = Convert.ToString(rd["MaintainmanID"]);

            if (!Convert.IsDBNull(rd["MaintainmanName"]))
                item.MaintainmanName = Convert.ToString(rd["MaintainmanName"]);

            if (!Convert.IsDBNull(rd["VerifyByName"]))
                item.VerifyName = Convert.ToString(rd["VerifyByName"]);

            if (!Convert.IsDBNull(rd["Name"]))
                item.EquipmentNameModel = Convert.ToString(rd["Name"]);
            if (item.EquipmentNameModel != null)
            {
                if (!Convert.IsDBNull(rd["Model"]))
                    item.EquipmentNameModel += "/" + Convert.ToString(rd["Model"]);
            }

            return item;
        }
    }
}
