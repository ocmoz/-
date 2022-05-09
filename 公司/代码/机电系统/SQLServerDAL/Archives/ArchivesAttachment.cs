using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using FM2E.IDAL.Archives;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Exceptions;
using FM2E.Model.Utils;
using FM2E.Model.Archives;
namespace FM2E.SQLServerDAL.Archives
{
    /// <summary>
    /// 数据访问类ArchivesAttachment。
    /// </summary>
    public class ArchivesAttachment : IArchivesAttachment
    {
        public ArchivesAttachment()
        { }
        #region  成员方法

        /// <summary>
        /// 获取一个实体
        /// </summary>
        private ArchivesAttachmentInfo GetData(IDataReader rd)
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
        /// 增加一条数据
        /// </summary>
        public void InsertArchivesAttachment(ArchivesAttachmentInfo model)
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
            parameters[0].Value = model.ArchivesID;
            parameters[1].Value = model.ArchivesAttachmentName;
            parameters[2].Value = model.ItemID;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.SavePath;
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
        /// 更新一条数据
        /// </summary>
        public void UpdateArchivesAttachment(FM2E.Model.Archives.ArchivesAttachmentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_ArchivesAttachment set ");
            strSql.Append("ArchivesID=@ArchivesID,");
            strSql.Append("ArchivesAttachmentName=@ArchivesAttachmentName,");
            strSql.Append("ItemID=@ItemID,");
            strSql.Append("Description=@Description,");
            strSql.Append("SavePath=@SavePath,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ArchivesAttachmentID=@ArchivesAttachmentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesAttachmentID", SqlDbType.BigInt,8),
					new SqlParameter("@ArchivesID", SqlDbType.BigInt,8),
					new SqlParameter("@ArchivesAttachmentName", SqlDbType.NVarChar,50),
					new SqlParameter("@ItemID", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@SavePath", SqlDbType.VarChar,80),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ArchivesAttachmentID;
            parameters[1].Value = model.ArchivesID;
            parameters[2].Value = model.ArchivesAttachmentName;
            parameters[3].Value = model.ItemID;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.SavePath;
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
        /// 删除一条数据
        /// </summary>
        public void DeleteArchivesAttachment(long ArchivesAttachmentID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from FM2E_ArchivesAttachment ");
            strSql.Append(" where ArchivesAttachmentID=@ArchivesAttachmentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesAttachmentID", SqlDbType.BigInt)};
            parameters[0].Value = ArchivesAttachmentID;

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
        public ArchivesAttachmentInfo GetArchivesAttachment(long ArchivesAttachmentID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ArchivesAttachmentID,ArchivesID,ArchivesAttachmentName,ItemID,Description,SavePath,Remark ");
            strSql.Append(" FROM FM2E_ArchivesAttachment ");
            strSql.Append(" where ArchivesAttachmentID=@ArchivesAttachmentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesAttachmentID", SqlDbType.BigInt)};
            parameters[0].Value = ArchivesAttachmentID;

            ArchivesAttachmentInfo item = new ArchivesAttachmentInfo();
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
                throw new DALException("获取ArchivesAttachmentInfo实体信息失败", e);
            }
            return item;
        }

        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        public IList GetAllArchivesAttachment()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ArchivesAttachmentID,ArchivesID,ArchivesAttachmentName,ItemID,Description,SavePath,Remark ");
            strSql.Append(" FROM FM2E_ArchivesAttachment ");
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
                throw new DALException("获取ArchivesAttachmentInfo列表信息失败", e);
            }
            return list;
        }

        /// <summary>
        /// 获取查询实体
        /// </summary>
        public QueryParam GenerateSearchTerm(ArchivesAttachmentInfo model)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = "FM2E_ArchivesAttachment";
            qp.ReturnFields = "ArchivesAttachmentID,ArchivesID,ArchivesAttachmentName,ItemID,Description,SavePath,Remark";
            qp.OrderBy = "";
            string sqlSearch = "where 1=1 ";

            if (model.ArchivesAttachmentID != 0)
            {
                sqlSearch += " and ArchivesAttachmentID =" + model.ArchivesAttachmentID;
            }

            if (model.ArchivesID != 0)
            {
                sqlSearch += " and ArchivesID =" + model.ArchivesID;
            }

            if (model.ArchivesAttachmentName != null && model.ArchivesAttachmentName.Trim() != string.Empty)
            {
                sqlSearch += " and ArchivesAttachmentName like '%" + model.ArchivesAttachmentName.Trim() + "%'";
            }

            if (model.ItemID != 0)
            {
                sqlSearch += " and ItemID =" + model.ItemID;
            }

            if (model.Description != null && model.Description.Trim() != string.Empty)
            {
                sqlSearch += " and Description like '%" + model.Description.Trim() + "%'";
            }

            if (model.SavePath != null && model.SavePath.Trim() != string.Empty)
            {
                sqlSearch += " and SavePath like '%" + model.SavePath.Trim() + "%'";
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
                term.TableName = "FM2E_ArchivesAttachment";
                term.ReturnFields = "ArchivesAttachmentID,ArchivesID,ArchivesAttachmentName,ItemID,Description,SavePath,Remark";
                term.OrderBy = "";
                term.Where = "where 1=1";
            }
            try
            {
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取ArchivesAttachmentInfo列表分页失败", e);
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ArchivesAttachmentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from FM2E_ArchivesAttachment");
            strSql.Append(" where ArchivesAttachmentID=@ArchivesAttachmentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesAttachmentID", SqlDbType.BigInt)};
            parameters[0].Value = ArchivesAttachmentID;

            return SQLHelper.Exists(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        #endregion  成员方法
    }
}

