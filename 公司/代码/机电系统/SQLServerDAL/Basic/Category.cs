using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;
using FM2E.IDAL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using System.Collections;
using FM2E.Model.Exceptions;

namespace FM2E.SQLServerDAL.Basic
{
    public class Category : ICategory
    {
        private const string SELECT_ALLCATEGORY = "select * from FM2E_Category where [IsDeleted]=0";

        private const string SELECT_CATEGORY = "select a.*,a.[Level] as lv,b.CategoryName as ParentName from FM2E_Category a left join FM2E_Category b on a.ParentID = b.CategoryID where a.IsDeleted = 0 and a.CategoryID = '{0}' order by lv";

        private const string UPDATE_CATEGORY = "declare @tmp nvarchar(200) select @tmp=CategoryCode from FM2E_Category where [CategoryID]=@CategoryID ; update FM2E_Category set CategoryCode=@CategoryCode+RIGHT(CategoryCode,LEN(CategoryCode)-LEN(@tmp)) where CategoryCode like @tmp+'%'; update FM2E_Category set [CategoryName]=@CategoryName,[Unit]=@Unit,[ParentID]=@ParentID,[Level]=@Level,[ChildrenCount]=@ChildrenCount,[DepreciationMethod]=@DepreciationMethod,[DepreciableLife]=@DepreciableLife,[ResidualRate]=@ResidualRate,[CategoryCode]=@CategoryCode where [CategoryID]=@CategoryID";

        private const string DEL_CATEGORY = "update FM2E_Category set [IsDeleted]=1 where [CategoryID]='{0}'";

        private const string INSERT_CATEGORY = "insert into FM2E_Category(CategoryName,Unit,ParentID,Level,ChildrenCount,DepreciationMethod,DepreciableLife,ResidualRate,IsDeleted,CategoryCode,NextCategoryCode) " +
                                                                          " values(@CategoryName,@Unit,@ParentID,@Level,@ChildrenCount,@DepreciationMethod,@DepreciableLife,@ResidualRate,0,@CategoryCode,@NextCategoryCode)";  

        private const string TableName = "FM2E_Category a left join FM2E_Category b on a.ParentID = b.CategoryID";
        private const string ReturnFields = "a.*,a.[Level] as lv,b.CategoryName as ParentName";
        private const string OrderBy = "order by lv asc";
        private const string Where = " where a.IsDeleted=0 ";

        private CategoryInfo GetData(IDataReader dr)
        {
            CategoryInfo item = new CategoryInfo();
            if (!Convert.IsDBNull(dr["CategoryID"]))
                item.CategoryID = Convert.ToInt64(dr["CategoryID"]);

            if (!Convert.IsDBNull(dr["CategoryName"]))
                item.CategoryName = Convert.ToString(dr["CategoryName"]);

            if (!Convert.IsDBNull(dr["Unit"]))
                item.Unit = Convert.ToString(dr["Unit"]);

            if (!Convert.IsDBNull(dr["ParentID"]))
                item.ParentID = Convert.ToInt64(dr["ParentID"]);

            if (!Convert.IsDBNull(dr["ParentName"]))
                item.ParentName = Convert.ToString(dr["ParentName"]);

            if (!Convert.IsDBNull(dr["Level"]))
                item.Level = Convert.ToInt32(dr["Level"]);

            if (!Convert.IsDBNull(dr["ChildrenCount"]))
                item.ChildrenCount = Convert.ToInt32(dr["ChildrenCount"]);

            if (!Convert.IsDBNull(dr["DepreciationMethod"]))
                item.DepreciationMethod = Convert.ToInt32(dr["DepreciationMethod"]);

            if (!Convert.IsDBNull(dr["DepreciableLife"]))
                item.DepreciableLife = Convert.ToInt32(dr["DepreciableLife"]);

            if (!Convert.IsDBNull(dr["ResidualRate"]))
                item.ResidualRate = Convert.ToDecimal(dr["ResidualRate"])*100;

            if (!Convert.IsDBNull(dr["CategoryCode"]))
                item.CategoryCode = Convert.ToString(dr["CategoryCode"]);

            if (!Convert.IsDBNull(dr["NextCategoryCode"]))
                item.NextCategoryCode = Convert.ToInt32(dr["NextCategoryCode"]);

            return item;
        }

        public IList<CategoryInfo> GetAllCategory()
        {
            List<CategoryInfo> list = new List<CategoryInfo>();

            ///StringBuilder strSql = new StringBuilder();
            string sql = "select a.*,a.[Level] as lv,b.CategoryName as ParentName from FM2E_Category a left join FM2E_Category b on a.ParentID = b.CategoryID where a.IsDeleted = 0  order by lv";

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        CategoryInfo item = GetData(rd);
                        list.Add(item);
                    }
                }

            }
            catch (Exception e)
            {
                throw new DALException("获取设备种类信息失败"+e.Message, e);
            }

            return list;
        }
        public CategoryInfo GetCategory(long id)
        {
            CategoryInfo item = null;
            string cmd = string.Format(SELECT_CATEGORY, id);

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        item = GetData(rd);
                }

            }
            catch(Exception e)
            {
                throw new DALException("获取设备种类信息失败",e);
            }
            return item;

        }
        public IList<CategoryInfo> GetChilds(long id)
        {
            List<CategoryInfo> list = new List<CategoryInfo>();

            return list;
        }
        public void InsertCategory(CategoryInfo item)
        {
            SqlParameter[] param = GetInsertParam();

            param[0].Value = item.CategoryName;
            param[1].Value = item.Unit;
            param[2].Value = item.ParentID;
            param[3].Value = item.Level;
            param[4].Value = item.ChildrenCount;
            param[5].Value = item.DepreciationMethod;
            param[6].Value = item.DepreciableLife;
       
            param[7].Value = item.ResidualRate/100;
            param[8].Value = item.CategoryCode;
            param[9].Value = item.NextCategoryCode;


            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, INSERT_CATEGORY, param);
                    
                }
                catch (Exception e)
                {
                    throw new DALException("插入设备种类信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        public void UpdateCategory(CategoryInfo item)
        {
            SqlParameter[] param = GetInsertUpdateParam();
            param[0].Value = item.CategoryID;
            param[1].Value = item.CategoryName;
            param[2].Value = item.Unit;
            param[3].Value = item.ParentID;
            param[4].Value = item.Level;
            param[5].Value = item.ChildrenCount;
            param[6].Value = item.DepreciationMethod;
            param[7].Value = item.DepreciableLife;
            param[8].Value = item.ResidualRate/100;
            param[9].Value = item.CategoryCode;


            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, UPDATE_CATEGORY, param);
                    if (result == 0)
                        throw new Exception("没有更新任何数据项");
                }
                catch (Exception e)
                {
                    throw new DALException("更新设备种类信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        private static SqlParameter[] GetInsertParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(INSERT_CATEGORY);
            if (param == null)
            {
                param = new SqlParameter[]{
    
                    new SqlParameter("@CategoryName",SqlDbType.NVarChar,20),
                    new SqlParameter("@Unit",SqlDbType.NVarChar,5),
                    new SqlParameter("@ParentID",SqlDbType.BigInt,8),
                    new SqlParameter("@Level",SqlDbType.TinyInt,1),
                    new SqlParameter("@ChildrenCount",SqlDbType.TinyInt,1),
                    new SqlParameter("@DepreciationMethod",SqlDbType.TinyInt,1),
                    new SqlParameter("@DepreciableLife",SqlDbType.Int,4),
                    new SqlParameter("@ResidualRate",SqlDbType.Float,8),
                    new SqlParameter("@CategoryCode",SqlDbType.VarChar,60),
                    new SqlParameter("@NextCategoryCode",SqlDbType.Int,4)
                };
                SQLHelper.CacheParameters(INSERT_CATEGORY, param);
            }
            return param;
        }


        private static SqlParameter[] GetInsertUpdateParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(UPDATE_CATEGORY);
            if (param == null)
            {
                param = new SqlParameter[]{
                    new SqlParameter("@CategoryID",SqlDbType.BigInt,8),
                    new SqlParameter("@CategoryName",SqlDbType.NVarChar,20),
                    new SqlParameter("@Unit",SqlDbType.NVarChar,5),
                    new SqlParameter("@ParentID",SqlDbType.BigInt,8),
                    new SqlParameter("@Level",SqlDbType.TinyInt,1),
                    new SqlParameter("@ChildrenCount",SqlDbType.TinyInt,1),
                    new SqlParameter("@DepreciationMethod",SqlDbType.TinyInt,1),
                    new SqlParameter("@DepreciableLife",SqlDbType.Int,4),
                    new SqlParameter("@ResidualRate",SqlDbType.Float,8),
                    new SqlParameter("@CategoryCode",SqlDbType.VarChar,60)
                };
                SQLHelper.CacheParameters(UPDATE_CATEGORY, param);
            }
            return param;
        }

        public void DelCategory(long id)
        {
            string cmd = string.Format(DEL_CATEGORY, id);
            try
            {
                int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, cmd, null);
                if (rows == 0)
                    throw new Exception("没有删除任何数据项！");
            }
            catch (Exception e)
            {
                throw new DALException("删除设备种类信息失败", e);
            }
        }
        public void DelCategory(IList<CategoryInfo> categoryinfos)
        {
        }
        public IList<CategoryInfo> Search(CategorysearchInfo item1)
        {
            string cmd = "select "+ReturnFields+" from "+TableName+Where;
            if (item1.CategoryID != 0)
                cmd += " and a.CategoryID = " + item1.CategoryID + " "; 
            if (item1.CategoryName != null && item1.CategoryName.Trim() != string.Empty)
            {
                cmd += " and a.CategoryName = '" + item1.CategoryName.Trim() + "' ";
            }
            if (item1.Unit != null && item1.Unit.Trim() != string.Empty)
            {
                cmd += " and a.Unit = '" + item1.Unit.Trim() + "' ";
            }
            if (item1.ParentID != 0)
            {
                cmd += " and a.ParentID = " + item1.ParentID + " ";
            }
            if (item1.ParentName != null && item1.ParentName.Trim() != string.Empty)
            {
                cmd += " and b.CategoryName = '" + item1.ParentName.Trim() + "' ";
            }
            if (item1.Level != 0)
            {
                cmd += " and a.Level = " + item1.Level + " ";
            }
            if (item1.ChildrenCount != 0)
            {
                cmd += " and a.ChildrenCount = " + item1.ChildrenCount + " ";
            }
            if (item1.DepreciableLife1 != 0 && item1.DepreciableLife2 != 0)
            {
                cmd += " and a.DepreciableLife >= " + item1.DepreciableLife1 + " and a.DepreciableLife <= " + item1.DepreciableLife2;
            }
            if (item1.ResidualRate1 > decimal.Zero && item1.ResidualRate2 > decimal.Zero)
            {
                cmd += " and a.ResidualRate >= " + item1.ResidualRate1 + " and a.ResidualRate <= " + item1.ResidualRate2;
            }
            if (item1.DepreciationMethod != 0)
                cmd += " and a.DepreciationMethod = " + item1.DepreciationMethod;

            List<CategoryInfo> list = new List<CategoryInfo>();
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
                throw new DALException("查询设备种类信息失败", e);
            }

            return list;
        }
        public QueryParam GenerateSearchTerm(CategorysearchInfo item1)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = TableName;
            qp.ReturnFields = ReturnFields;
            qp.OrderBy = OrderBy;
            qp.Where = Where;
            if(item1.CategoryID!=0)
                qp.Where+=" and a.CategoryID like '%"+ item1.CategoryID+"%' ";
            
            if(item1.CategoryName!=null&&item1.CategoryName.Trim()!=string.Empty)
            {
                qp.Where += " and a.CategoryName like '%"+item1.CategoryName.Trim()+"%'";
            }
            if (item1.Unit != null && item1.Unit.Trim() != string.Empty)
            {
                qp.Where += " and a.Unit like '%" + item1.Unit.Trim() + "%'";
            }
            if (item1.ParentID != 0)
            {
                qp.Where += " and a.ParentID like '%" + item1.ParentID + "%'";
            }
            if(item1.ParentName!=null&&item1.ParentName.Trim()!=string.Empty)
            {
                qp.Where += " and b.CategoryName like '%" + item1.ParentName.Trim() + "%'";
            }
            if (item1.Level != 0)
            {
                qp.Where += " and a.Level = " + item1.Level + " ";
            }
            if (item1.ChildrenCount != 0)
            {
                qp.Where += " and a.ChildrenCount like '%" + item1.ChildrenCount + "%'";
            }
            if (item1.DepreciableLife1 != 0&&item1.DepreciableLife2!=0)
            {
                qp.Where += " and a.DepreciableLife >= " + item1.DepreciableLife1 + " and a.DepreciableLife <= "+item1.DepreciableLife2;
            }
            if (item1.ResidualRate1 > decimal.Zero && item1.ResidualRate2 > decimal.Zero)
            {
                qp.Where += " and a.ResidualRate >= " + item1.ResidualRate1 + " and a.ResidualRate <= " + item1.ResidualRate2;
            }
            if (item1.DepreciationMethod != 0)
                qp.Where += " and a.DepreciationMethod like '%" + item1.DepreciationMethod + "%' ";

            return qp;
        }
        public IList GetList(QueryParam term, out int recordCount)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = TableName;
                    term.ReturnFields = ReturnFields;
                    term.OrderBy = OrderBy;
                    term.Where = Where;
                }
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException(" 获取设备种类分页失败",e);
            }
        }

        int ICategory.GetNextCategoryCode(string CategoryCode)
        {
            int nextCategoryCode = 0;

            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                StringBuilder strSql = new StringBuilder();
                if (CategoryCode != null && CategoryCode != string.Empty)
                {
                    strSql.Append(" select top 1 NextCategoryCode ");
                    strSql.Append(" from FM2E_Category ");
                    strSql.AppendFormat(" where CategoryCode like '{0}'", CategoryCode);
                    strSql.AppendFormat(" update FM2E_Category set NextCategoryCode=NextCategoryCode+1 where CategoryCode like '{0}';", CategoryCode);

                    nextCategoryCode = (int)SQLHelper.ExecuteScalar(trans, CommandType.Text, strSql.ToString(), null);
                }
                else
                {
                    strSql.Append(" SELECT TOP 1 LEFT(MAX(CategoryCode), 3) AS MaxCategoryCode FROM FM2E_Category");

                    string MaxCategoryCode = SQLHelper.ExecuteScalar(trans, CommandType.Text, strSql.ToString(), null) as string;
                    if (MaxCategoryCode != null)
                        nextCategoryCode = Convert.ToInt32(MaxCategoryCode) + 1;
                    else
                        nextCategoryCode = 0;
                }
                
                trans.Commit();
            }
            catch (Exception ex)
            {
                nextCategoryCode = 0;
                trans.Rollback();
                throw new DALException("获取下一级种类编码失败", ex);
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
            return nextCategoryCode;
        }
    }
}
