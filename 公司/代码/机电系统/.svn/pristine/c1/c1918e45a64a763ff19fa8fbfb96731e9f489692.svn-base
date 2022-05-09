using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using FM2E.IDAL.Archives;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Exceptions;
using FM2E.Model.Utils;
using FM2E.Model.Archives;

namespace FM2E.SQLServerDAL.Archives
{
    /// <summary>
    /// 数据访问类Archives。
    /// </summary>
    public class Archives : IArchives
    {
        public Archives()
        { }
        #region  成员方法

        /// <summary>
        /// 获取一个实体
        /// </summary>
        private ArchivesInfo GetData(IDataReader rd)
        {
            ArchivesInfo item = new ArchivesInfo();
            if (!Convert.IsDBNull(rd["ArchivesID"]))
                item.ArchivesID = Convert.ToInt64(rd["ArchivesID"]);

            if (!Convert.IsDBNull(rd["ArchivesName"]))
                item.ArchivesName = Convert.ToString(rd["ArchivesName"]);

            if (!Convert.IsDBNull(rd["ArchivesTypeID"]))
                item.ArchivesTypeID = Convert.ToInt64(rd["ArchivesTypeID"]);

            if (!Convert.IsDBNull(rd["ArchivesTypeName"]))
                item.ArchivesTypeName = Convert.ToString(rd["ArchivesTypeName"]);

            if (!Convert.IsDBNull(rd["InvolvedSystem"]))
                item.InvolvedSystem = Convert.ToString(rd["InvolvedSystem"]);

            if (!Convert.IsDBNull(rd["InvolvedEquipment"]))
                item.InvolvedEquipment = Convert.ToString(rd["InvolvedEquipment"]);

            if (!Convert.IsDBNull(rd["Description"]))
                item.Description = Convert.ToString(rd["Description"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            return item;
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        private ArchivesAttachmentInfo GetAttachmentData(IDataReader rd)
        {
            ArchivesAttachmentInfo item = new ArchivesAttachmentInfo();

            if (!Convert.IsDBNull(rd["ArchivesAttachmentID"]))
                item.ArchivesAttachmentID = Convert.ToInt64(rd["ArchivesAttachmentID"]);

            if (!Convert.IsDBNull(rd["ArchivesID"]))
                item.ArchivesID = Convert.ToInt64(rd["ArchivesID"]);

            if (!Convert.IsDBNull(rd["ArchivesAttachmentName"]))
                item.ArchivesAttachmentName = Convert.ToString(rd["ArchivesAttachmentName"]);

            if (!Convert.IsDBNull(rd["ItemID"]))
                item.ItemID = Convert.ToInt32(rd["ItemID"]);

            if (!Convert.IsDBNull(rd["Description"]))
                item.Description = Convert.ToString(rd["Description"]);

            if (!Convert.IsDBNull(rd["SavePath"]))
                item.SavePath = Convert.ToString(rd["SavePath"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            return item;
        }


        /// <summary>
        /// 得到一个档案附件实体
        /// </summary>
        public List<ArchivesAttachmentInfo> GetArchivesAttachmentByArchivesID(long ArchivesID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ArchivesAttachmentID,ArchivesID,ArchivesAttachmentName,ItemID,Description,SavePath,Remark ");
            strSql.Append(" FROM FM2E_ArchivesAttachment ");
            strSql.Append(" where ArchivesID=@ArchivesID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesID", SqlDbType.BigInt)};
            parameters[0].Value = ArchivesID;

            List<ArchivesAttachmentInfo> list = new List<ArchivesAttachmentInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        ArchivesAttachmentInfo item = this.GetAttachmentData(rd);
                        list.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取ArchivesAttachmentInfo实体信息失败", e);
            }
            return list;
        }


        /// <summary>
        /// 插入新的档案详细信息
        /// </summary>
        /// <param name="order">申请单</param>
        public long InsertArchivesDetails(ArchivesInfo model)
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
                id = InsertArchives(trans, model);

                model.ArchivesID = id;
                //插入消息对象列表
                InsertArchivesAttachment(trans, model, id);

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


        /// <summary>
        /// 插入记录，事务
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public long InsertArchives(SqlTransaction trans, ArchivesInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_Archives(");
            strSql.Append("ArchivesName,ArchivesTypeID,InvolvedSystem,InvolvedEquipment,Description,Remark)");
            strSql.Append(" values (");
            strSql.Append("@ArchivesName,@ArchivesTypeID,@InvolvedSystem,@InvolvedEquipment,@Description,@Remark)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesName", SqlDbType.NVarChar,20),
					new SqlParameter("@ArchivesTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@InvolvedSystem", SqlDbType.NVarChar,80),
					new SqlParameter("@InvolvedEquipment", SqlDbType.NVarChar,80),
					new SqlParameter("@Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ArchivesName;
            parameters[1].Value = model.ArchivesTypeID;
            parameters[2].Value = model.InvolvedSystem;
            parameters[3].Value = model.InvolvedEquipment;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.Remark;

            //读取ID
            long id = 1;
            using (SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rdr.Read())
                {
                    id = rdr.GetInt64(0);
                }
            }
            return id;
        }

        /// <summary>
        /// 档案附件列表增加
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="model"></param>
        /// <param name="id"></param>
        public void InsertArchivesAttachment(SqlTransaction trans, ArchivesInfo model, long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_ArchivesAttachment(");
            strSql.Append("ArchivesID,ArchivesAttachmentName,ItemID,Description,SavePath,Remark)");
            strSql.Append(" values (");
            strSql.Append("@ArchivesID,@ArchivesAttachmentName,@ItemID,@Description,@SavePath,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesID", SqlDbType.BigInt,8),
					new SqlParameter("@ArchivesAttachmentName", SqlDbType.NVarChar,50),
					new SqlParameter("@ItemID", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@SavePath", SqlDbType.VarChar,80),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50)};

            if (model.AttachmentList != null)
            {
                //针对每一个进行
                foreach (ArchivesAttachmentInfo item in model.AttachmentList)
                {
                    parameters[0].Value = id;
                    parameters[1].Value = item.ArchivesAttachmentName;
                    parameters[2].Value = item.ItemID;
                    parameters[3].Value = item.Description;
                    parameters[4].Value = item.SavePath;
                    parameters[5].Value = item.Remark;
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                }
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void InsertArchives(ArchivesInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_Archives(");
            strSql.Append("ArchivesName,ArchivesTypeID,InvolvedSystem,InvolvedEquipment,Description,Remark)");
            strSql.Append(" values (");
            strSql.Append("@ArchivesName,@ArchivesTypeID,@InvolvedSystem,@InvolvedEquipment,@Description,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesName", SqlDbType.NVarChar,20),
					new SqlParameter("@ArchivesTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@InvolvedSystem", SqlDbType.NVarChar,80),
					new SqlParameter("@InvolvedEquipment", SqlDbType.NVarChar,80),
					new SqlParameter("@Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ArchivesName;
            parameters[1].Value = model.ArchivesTypeID;
            parameters[2].Value = model.InvolvedSystem;
            parameters[3].Value = model.InvolvedEquipment;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.Remark;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                    if (result == 0)
                        throw new Exception("没有增加任何数据项");
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        /// <summary>
        /// 更新档案详细信息，事务
        /// </summary>
        /// <param name="model"></param>
        public void UpdateArchivesDetails(ArchivesInfo model)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先更新总表
                UpdateArchives(trans, model);

                //详情表更新，使用先删除，后插入的方法
                UpdateArchivesAttachment(trans, model);

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
        /// 更新一条数据
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="model"></param>
        public void UpdateArchives(SqlTransaction trans, ArchivesInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_Archives set ");
            strSql.Append("ArchivesName=@ArchivesName,");
            strSql.Append("ArchivesTypeID=@ArchivesTypeID,");
            strSql.Append("InvolvedSystem=@InvolvedSystem,");
            strSql.Append("InvolvedEquipment=@InvolvedEquipment,");
            strSql.Append("Description=@Description,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ArchivesID=@ArchivesID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesID", SqlDbType.BigInt,8),
					new SqlParameter("@ArchivesName", SqlDbType.NVarChar,20),
					new SqlParameter("@ArchivesTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@InvolvedSystem", SqlDbType.NVarChar,80),
					new SqlParameter("@InvolvedEquipment", SqlDbType.NVarChar,80),
					new SqlParameter("@Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ArchivesID;
            parameters[1].Value = model.ArchivesName;
            parameters[2].Value = model.ArchivesTypeID;
            parameters[3].Value = model.InvolvedSystem;
            parameters[4].Value = model.InvolvedEquipment;
            parameters[5].Value = model.Description;
            parameters[6].Value = model.Remark;

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);  //提交
        }


        /// <summary>
        /// 更新附件列表
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="model">档案信息实体</param>
        private void UpdateArchivesAttachment(SqlTransaction trans, ArchivesInfo model)
        {
            //先删除
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from FM2E_ArchivesAttachment ");
            strSql.Append(" where ArchivesID=@ArchivesID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesID", SqlDbType.BigInt)};
            parameters[0].Value = model.ArchivesID;

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

            //然后再插入
            InsertArchivesAttachment(trans, model, model.ArchivesID);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateArchives(ArchivesInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_Archives set ");
            strSql.Append("ArchivesName=@ArchivesName,");
            strSql.Append("ArchivesTypeID=@ArchivesTypeID,");
            strSql.Append("InvolvedSystem=@InvolvedSystem,");
            strSql.Append("InvolvedEquipment=@InvolvedEquipment,");
            strSql.Append("Description=@Description,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ArchivesID=@ArchivesID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesID", SqlDbType.BigInt,8),
					new SqlParameter("@ArchivesName", SqlDbType.NVarChar,20),
					new SqlParameter("@ArchivesTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@InvolvedSystem", SqlDbType.NVarChar,80),
					new SqlParameter("@InvolvedEquipment", SqlDbType.NVarChar,80),
					new SqlParameter("@Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ArchivesID;
            parameters[1].Value = model.ArchivesName;
            parameters[2].Value = model.ArchivesTypeID;
            parameters[3].Value = model.InvolvedSystem;
            parameters[4].Value = model.InvolvedEquipment;
            parameters[5].Value = model.Description;
            parameters[6].Value = model.Remark;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                    if (result == 0)
                        throw new Exception("没有更新任何数据项");
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        /// <summary>
        /// 删除档案详细信息，事务
        /// </summary>
        /// <param name="model"></param>
        public void DeleteArchivesDetails(long id)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先删除档案附件
                DeleteArchivesAttachment(trans, id);

                //再删除总表
                DeleteArchives(trans, id);

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
        /// 删除档案附件，事务
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="model"></param>
        private void DeleteArchivesAttachment(SqlTransaction trans, long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from FM2E_ArchivesAttachment ");
            strSql.Append(" where ArchivesID=@ArchivesID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesID", SqlDbType.BigInt)};
            parameters[0].Value = id;

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除档案主表
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="id"></param>
        public void DeleteArchives(SqlTransaction trans, long id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from FM2E_Archives ");
            strSql.Append(" where ArchivesID=@ArchivesID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesID", SqlDbType.BigInt)};
            parameters[0].Value = id;

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteArchives(long ArchivesID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from FM2E_Archives ");
            strSql.Append(" where ArchivesID=@ArchivesID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesID", SqlDbType.BigInt)};
            parameters[0].Value = ArchivesID;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                    if (result == 0)
                        throw new Exception("没有删除任何数据项");
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ArchivesInfo GetArchives(long ArchivesID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ArchivesID,ArchivesName,ArchivesTypeID,ArchivesTypeName,InvolvedSystem,InvolvedEquipment,Description,Remark ");
            strSql.Append(" FROM FM2E_ArchivesView ");
            strSql.Append(" where ArchivesID=@ArchivesID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesID", SqlDbType.BigInt)};
            parameters[0].Value = ArchivesID;

            ArchivesInfo item = new ArchivesInfo();
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
                throw new DALException("获取ArchivesInfo实体信息失败", e);
            }

            item.AttachmentList = GetArchivesAttachmentByArchivesID(item.ArchivesID);

            return item;
        }

        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        public IList GetAllArchives()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ArchivesID,ArchivesName,ArchivesTypeID,ArchivesTypeName,InvolvedSystem,InvolvedEquipment,Description,Remark ");
            strSql.Append(" FROM FM2E_ArchivesView ");
            ArrayList list = new ArrayList();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
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
                throw new DALException("获取ArchivesInfo列表信息失败", e);
            }
            return list;
        }

        /// <summary>
        /// 获取查询实体
        /// </summary>
        public QueryParam GenerateSearchTerm(ArchivesInfo model)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = "FM2E_ArchivesView";
            qp.ReturnFields = "ArchivesID,ArchivesName,ArchivesTypeID,ArchivesTypeName,InvolvedSystem,InvolvedEquipment,Description,Remark";
            qp.OrderBy = "";
            string sqlSearch = "where 1=1 ";

            if (model.ArchivesID != 0)
            {
                sqlSearch += " and ArchivesID =" + model.ArchivesID;
            }

            if (model.ArchivesName != null && model.ArchivesName.Trim() != string.Empty)
            {
                sqlSearch += " and ArchivesName like '%" + model.ArchivesName.Trim() + "%'";
            }

            if (model.ArchivesTypeID != 0)
            {
                sqlSearch += " and ArchivesTypeID =" + model.ArchivesTypeID;
            }

            if (model.ArchivesTypeName != null && model.ArchivesTypeName.Trim() != string.Empty)
            {
                sqlSearch += " and ArchivesTypeName like '%" + model.ArchivesTypeName.Trim() + "%'";
            }

            if (model.InvolvedSystem != null && model.InvolvedSystem.Trim() != string.Empty)
            {
                sqlSearch += " and InvolvedSystem like '%" + model.InvolvedSystem.Trim() + "%'";
            }

            if (model.InvolvedEquipment != null && model.InvolvedEquipment.Trim() != string.Empty)
            {
                sqlSearch += " and InvolvedEquipment like '%" + model.InvolvedEquipment.Trim() + "%'";
            }

            if (model.Description != null && model.Description.Trim() != string.Empty)
            {
                sqlSearch += " and Description like '%" + model.Description.Trim() + "%'";
            }

            if (model.Remark != null && model.Remark.Trim() != string.Empty)
            {
                sqlSearch += " and Remark like '%" + model.Remark.Trim() + "%'";
            }

            qp.Where = sqlSearch;
            return qp;
        }

        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        public IList GetList(QueryParam term, out int recordCount)
        {
            if (term.Where == "")
            {
                term.TableName = "FM2E_ArchivesView";
                term.ReturnFields = "ArchivesID,ArchivesName,ArchivesTypeID,ArchivesTypeName,InvolvedSystem,InvolvedEquipment,Description,Remark";
                term.OrderBy = "";
                term.Where = "where 1=1";
            }
            try
            {
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取ArchivesInfo列表分页失败", e);
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ArchivesID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from FM2E_Archives");
            strSql.Append(" where ArchivesID=@ArchivesID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesID", SqlDbType.BigInt)};
            parameters[0].Value = ArchivesID;

            return SQLHelper.Exists(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        #endregion  成员方法
    }
}

