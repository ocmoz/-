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
using System.Collections.Generic;

namespace FM2E.SQLServerDAL.Archives
{
    /// <summary>
    /// 数据访问类ArchivesType。
    /// </summary>
    public class ArchivesType : IArchivesType
    {
        public ArchivesType()
        { }
        #region  成员方法

        private const string SEARCH = "select ArchivesTypeID,ArchivesTypeName,Description,ParentID,ParentName,Level,ChildCount,Remark FROM FM2E_ArchivesTypeView ";

        /// <summary>
        /// 获取一个实体
        /// </summary>
        private ArchivesTypeInfo GetData(IDataReader rd)
        {
            ArchivesTypeInfo item = new ArchivesTypeInfo();
            if (!Convert.IsDBNull(rd["ArchivesTypeID"]))
                item.ArchivesTypeID = Convert.ToInt64(rd["ArchivesTypeID"]);

            if (!Convert.IsDBNull(rd["ArchivesTypeName"]))
                item.ArchivesTypeName = Convert.ToString(rd["ArchivesTypeName"]);

            if (!Convert.IsDBNull(rd["Description"]))
                item.Description = Convert.ToString(rd["Description"]);

            if (!Convert.IsDBNull(rd["ParentID"]))
                item.ParentID = Convert.ToInt64(rd["ParentID"]);

            if (!Convert.IsDBNull(rd["ParentName"]))
                item.ParentName = Convert.ToString(rd["ParentName"]);

            if (!Convert.IsDBNull(rd["Level"]))
                item.Level = Convert.ToInt32(rd["Level"]);

            if (!Convert.IsDBNull(rd["ChildCount"]))
                item.ChildCount = Convert.ToInt32(rd["ChildCount"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            return item;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void InsertArchivesType(ArchivesTypeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_ArchivesType(");
            strSql.Append("ArchivesTypeName,Description,ParentID,Level,ChildCount,Remark)");
            strSql.Append(" values (");
            strSql.Append("@ArchivesTypeName,@Description,@ParentID,@Level,@ChildCount,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesTypeName", SqlDbType.NVarChar,20),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@ParentID", SqlDbType.BigInt,8),
					new SqlParameter("@Level", SqlDbType.TinyInt,1),
					new SqlParameter("@ChildCount", SqlDbType.TinyInt,1),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ArchivesTypeName;
            parameters[1].Value = model.Description;
            parameters[2].Value = model.ParentID;
            parameters[3].Value = model.Level;
            parameters[4].Value = model.ChildCount;
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
        public void UpdateArchivesType(FM2E.Model.Archives.ArchivesTypeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_ArchivesType set ");
            strSql.Append("ArchivesTypeName=@ArchivesTypeName,");
            strSql.Append("Description=@Description,");
            strSql.Append("ParentID=@ParentID,");
            strSql.Append("Level=@Level,");
            strSql.Append("ChildCount=@ChildCount,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ArchivesTypeID=@ArchivesTypeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@ArchivesTypeName", SqlDbType.NVarChar,20),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@ParentID", SqlDbType.BigInt,8),
					new SqlParameter("@Level", SqlDbType.TinyInt,1),
					new SqlParameter("@ChildCount", SqlDbType.TinyInt,1),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ArchivesTypeID;
            parameters[1].Value = model.ArchivesTypeName;
            parameters[2].Value = model.Description;
            parameters[3].Value = model.ParentID;
            parameters[4].Value = model.Level;
            parameters[5].Value = model.ChildCount;
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
        public void DeleteArchivesType(long ArchivesTypeID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_ArchivesType set ChildCount=ChildCount-1 where ArchivesTypeID in (select ParentID from FM2E_ArchivesType where ArchivesTypeID=@ArchivesTypeID);");
            strSql.Append("delete from FM2E_ArchivesType ");
            strSql.Append(" where ArchivesTypeID=@ArchivesTypeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesTypeID", SqlDbType.BigInt)};
            parameters[0].Value = ArchivesTypeID;

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
        public ArchivesTypeInfo GetArchivesType(long ArchivesTypeID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ArchivesTypeID,ArchivesTypeName,Description,ParentID,ParentName,Level,ChildCount,Remark ");
            strSql.Append(" FROM FM2E_ArchivesTypeView ");
            strSql.Append(" where ArchivesTypeID=@ArchivesTypeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesTypeID", SqlDbType.BigInt)};
            parameters[0].Value = ArchivesTypeID;

            ArchivesTypeInfo item = new ArchivesTypeInfo();
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
                throw new DALException("获取ArchivesTypeInfo实体信息失败", e);
            }
            return item;
        }

        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        public IList GetAllArchivesType()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ArchivesTypeID,ArchivesTypeName,Description,ParentID,ParentName,Level,ChildCount,Remark ");
            strSql.Append(" FROM FM2E_ArchivesTypeView ");
            strSql.Append(" Order by ArchivesTypeID asc");
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
                throw new DALException("获取ArchivesTypeInfo列表信息失败", e);
            }
            return list;
        }

        /// <summary>
        /// 获取查询实体
        /// </summary>
        public QueryParam GenerateSearchTerm(ArchivesTypeInfo model)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = "FM2E_ArchivesTypeView";
            qp.ReturnFields = "ArchivesTypeID,ArchivesTypeName,Description,ParentID,ParentName,Level,ChildCount,Remark";
            qp.OrderBy = " Order by [Level] asc";
            string sqlSearch = "where 1=1 ";

            if (model.ArchivesTypeID != 0)
            {
                sqlSearch += " and ArchivesTypeID =" + model.ArchivesTypeID;
            }

            if (model.ArchivesTypeName != null && model.ArchivesTypeName.Trim() != string.Empty)
            {
                sqlSearch += " and ArchivesTypeName like '%" + model.ArchivesTypeName.Trim() + "%'";
            }

            if (model.Description != null && model.Description.Trim() != string.Empty)
            {
                sqlSearch += " and Description like '%" + model.Description.Trim() + "%'";
            }

            if (model.ParentID != 0)
            {
                sqlSearch += " and ParentID =" + model.ParentID;
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
                term.TableName = "FM2E_ArchivesTypeView";
                term.ReturnFields = "ArchivesTypeID,ArchivesTypeName,Description,ParentID,ParentName,Level,ChildCount,Remark";
                term.OrderBy = "order by [Level] asc";
                term.Where = "where 1=1";
            }
            try
            {
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取ArchivesTypeInfo列表分页失败", e);
            }
        }

        /// <summary>
        /// 获取查找实体
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IList<ArchivesTypeInfo> Search(ArchivesTypeInfo item)
        {
            string cmd = SEARCH + " where 1=1 ";
            if (item.ArchivesTypeName != null && item.ArchivesTypeName != string.Empty)
                cmd += " and ArchivesTypeName like '%" + item.ArchivesTypeName + "%' ";
            if (item.ArchivesTypeID != 0)
                cmd += " and ArchivesTypeID = " + item.ArchivesTypeID;
            if (item.Level != 0)
                cmd += " and Level = " + item.Level;
            if (item.ParentID != 0)
                cmd += " and ParentID = " + item.ParentID;
            if (item.Remark != null && item.Remark != string.Empty)
                cmd += " and Remark = like '%" + item.Remark + "%' ";
            if (item.Description != null && item.Description != string.Empty)
                cmd += " and Description = like '%" + item.Description + "%' ";

            cmd += "order by [Level] asc";


            List<ArchivesTypeInfo> list = new List<ArchivesTypeInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        list.Add(GetData(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("查询档案类型信息失败", e);
            }
            return list;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ArchivesTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from FM2E_ArchivesType");
            strSql.Append(" where ArchivesTypeID=@ArchivesTypeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ArchivesTypeID", SqlDbType.BigInt)};
            parameters[0].Value = ArchivesTypeID;

            return SQLHelper.Exists(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 获取某个类型下的所有子地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList GetChildArchivesType(long id)
        {
            ArrayList list = new ArrayList();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  ArchivesTypeID,ArchivesTypeName,Description,ParentID,ParentName,Level,ChildCount,Remark from FM2E_ArchivesTypeView ");
                strSql.Append(" where ParentID=@ParentID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ParentID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                        list.Add(GetData(rd));
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取子地址失败", ex);
            }

            return list;
        }

        #endregion  成员方法
    }
}