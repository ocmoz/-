using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.BudgetManagement;
using FM2E.Model.Utils;
using FM2E.Model.BudgetManagement;
using System.Data;
using FM2E.Model.Exceptions;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;
using System.Collections;

namespace FM2E.SQLServerDAL.BudgetManagement
{
    public class SubjectRelation : ISubjectRelation
    {
        private const string DEL_SUBJECTRELATION = "delete from FM2E_BudgetAccounts where SubID = '{0}'";

        private const string UPDATE_SUBJECTRELATION = " update FM2E_BudgetAccounts set [ParentID]=@ParentID,[Name]=@Name,[CompanyID]=@CompanyID,[IsLeaf]=@IsLeaf where [SubID]=@SubID";

        private const string INSERT_SUBJECTRELATION = " insert into FM2E_BudgetAccounts(ParentID,Name,CompanyID,IsLeaf) values(@ParentID,@Name,@CompanyID,@IsLeaf)";

        private const string TableName = " FM2E_BudgetAccounts AS su1 LEFT OUTER JOIN FM2E_BudgetAccounts AS su2 ON su1.ParentID = su2.SubID ";

        private const string ReturnFields = " su1.SubID as subid, su1.ParentID, su1.Name, su1.IsLeaf, su1.CompanyID, su2.Name AS ParentName ";

        private const string TableName2 = " FM2E_BudgetAccountsPerYear AS su1 LEFT OUTER JOIN FM2E_BudgetAccountsPerYear AS su2 ON su1.ParentID = su2.SubID and su1.Year = su2.Year ";

        private const string ReturnFields2 = " su1.Year as budgetyearid, su1.SubID as subid, su1.ParentID, su1.Name, su1.IsLeaf, su1.CompanyID, su2.Name AS ParentName ";

        private const string OrderBy = " order by subid ";

        private const string Where = " where 1=1 ";

        private const string SELECT_SUBJECTRELATION = " select " + ReturnFields + " from " + TableName + Where + " and su1.SubID = '{0}' " + OrderBy;

        public QueryParam GenerateSearchTerm(SubjectRelationInfos item)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = TableName;
            qp.ReturnFields = ReturnFields;
            qp.OrderBy = OrderBy;
            qp.Where = Where;
            if (item.ParentID != 0)
                qp.Where += " and su1.ParentID = " + item.ParentID + " ";
            if (item.Name != null && item.Name != string.Empty)
                qp.Where += " and su1.Name like '%" + item.Name + "%' ";
            if (item.ParentName != null && item.ParentName != string.Empty)
                qp.Where += " and su2.Name like '%" + item.ParentName + "%' ";
            if (item.IsLeaf != 0)
            {
                if (item.IsLeaf == 1)
                    qp.Where += " and su1.IsLeaf = 1";
                else if (item.IsLeaf == 2)
                    qp.Where += " and su1.IsLeaf = 0";
            }
            if (item.CompanyID != null && item.CompanyID != string.Empty)
                qp.Where += " and su1.CompanyID = " + item.CompanyID + " ";
            return qp;
        }

        public QueryParam GenerateSearchTermByYear(SubjectPerYear item)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = TableName2;
            qp.ReturnFields = ReturnFields2;
            qp.OrderBy = OrderBy;
            qp.Where = Where;
            if (item.ParentID != 0)
                qp.Where += " and su1.ParentID = " + item.ParentID + " ";
            if (item.Name != null && item.Name != string.Empty)
                qp.Where += " and su1.Name like '%" + item.Name + "%' ";
            if (item.ParentName != null && item.ParentName != string.Empty)
                qp.Where += " and su2.Name like '%" + item.ParentName + "%' ";
            if (item.IsLeaf != 0)
            {
                if (item.IsLeaf == 1)
                    qp.Where += " and su1.IsLeaf = 1";
                else if (item.IsLeaf == 2)
                    qp.Where += " and su1.IsLeaf = 0";
            }
            if (item.CompanyID != null && item.CompanyID != string.Empty)
                qp.Where += " and su1.CompanyID = " + item.CompanyID + " ";
            if (item.Year != 0)
                qp.Where += " and su1.Year = " + item.Year + " ";
            return qp;
        }

        public IList GetList(QueryParam term, out int recordCount, string companyid)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = TableName;
                    term.ReturnFields = ReturnFields;
                    term.OrderBy = OrderBy;
                    if (companyid != null && companyid != string.Empty)
                        term.Where = Where + " and su1.CompanyID = '" + companyid + "' ";
                    else
                        term.Where = Where;
                }
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException(" 获取预算科目分页失败", e);
            }
        }

        public IList GetListByYear(QueryParam term, out int recordCount, string companyid)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = TableName2;
                    term.ReturnFields = ReturnFields2;
                    term.OrderBy = OrderBy;
                    if (companyid != null && companyid != string.Empty)
                        term.Where = Where + " and su1.CompanyID = '" + companyid + "' ";
                    else
                        term.Where = Where;
                }
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取预算用到的预算科目失败", e);
            }
        }

        private SubjectRelationInfos GetData(IDataReader dr)
        {
            SubjectRelationInfos item = new SubjectRelationInfos();
            if (!Convert.IsDBNull(dr["subid"]))
                item.SubID = Convert.ToInt64(dr["subid"]);
            if (!Convert.IsDBNull(dr["ParentID"]))
                item.ParentID = Convert.ToInt64(dr["ParentID"]);
            if (!Convert.IsDBNull(dr["Name"]))
                item.Name = Convert.ToString(dr["Name"]);
            if (!Convert.IsDBNull(dr["ParentName"]))
                item.ParentName = Convert.ToString(dr["ParentName"]);
            if (!Convert.IsDBNull(dr["IsLeaf"]))
                item.IsLeaf = (Convert.ToBoolean(dr["IsLeaf"]) == true) ? Convert.ToInt16(1) : Convert.ToInt16(2);
            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);

            return item;
        }

        public SubjectRelationInfos GetSubjectRelation(long id)
        {
            SubjectRelationInfos item = null;
            string cmd = string.Format(SELECT_SUBJECTRELATION, id);

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        item = GetData(rd);
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取预算科目信息失败", e);
            }
            return item;
        }


        private static SqlParameter[] GetInsertParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(INSERT_SUBJECTRELATION);
            if (param == null)
            {
                param = new SqlParameter[]{
                    new SqlParameter("@ParentID", SqlDbType.BigInt,8),
                    new SqlParameter("@Name", SqlDbType.VarChar,100),
                    new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
                    new SqlParameter("@IsLeaf", SqlDbType.Bit,1)
                };
                SQLHelper.CacheParameters(INSERT_SUBJECTRELATION, param);
            }
            return param;
        }

        public void InsertSubjectRelation(SubjectRelationInfos item)
        {
            SqlParameter[] param = GetInsertParam();
            param[0].Value = item.ParentID;
            param[1].Value = item.Name;
            param[2].Value = item.CompanyID;
            param[3].Value = (item.IsLeaf == 1) ? 1 : 0;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, INSERT_SUBJECTRELATION, param);

                }
                catch (Exception e)
                {
                    throw new DALException("插入预算科目信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        private static SqlParameter[] GetUpdateParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(UPDATE_SUBJECTRELATION);
            if (param == null)
            {
                param = new SqlParameter[]{
                    new SqlParameter("@ParentID", SqlDbType.BigInt,8),
                    new SqlParameter("@Name", SqlDbType.VarChar,100),
                    new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
                    new SqlParameter("@IsLeaf", SqlDbType.Bit,1),
                    new SqlParameter("@SubID",SqlDbType.BigInt,8)
                };
                SQLHelper.CacheParameters(UPDATE_SUBJECTRELATION, param);
            }
            return param;
        }

        public void UpdateSubjectRelation(SubjectRelationInfos item)
        {
            SqlParameter[] param = GetUpdateParam();
            param[0].Value = item.ParentID;
            param[1].Value = item.Name;
            param[2].Value = item.CompanyID;
            param[3].Value = (item.IsLeaf == 1) ? 1 : 0;
            param[4].Value = item.SubID;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, UPDATE_SUBJECTRELATION, param);
                    if (result == 0)
                        throw new Exception("没有更新任何数据项");
                }
                catch (Exception e)
                {
                    throw new DALException("更新预算科目信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void DelSubjectRelation(long id)
        {
            string cmd = string.Format(DEL_SUBJECTRELATION, id);
            try
            {
                int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, cmd, null);
                if (rows == 0)
                    throw new Exception("没有删除任何数据项！");
            }
            catch (Exception e)
            {
                throw new DALException("删除预算科目信息失败", e);
            }
        }
        public IList<SubjectRelationInfos> Search(SubjectRelationInfos item)
        {
            string cmd = " select " + ReturnFields + " from " + TableName + Where;
            if (item.SubID != 0)
                cmd += " and subid = " + item.SubID + " ";
            if (item.ParentID != 0)
                cmd += " and su1.ParentID = " + item.ParentID + " ";
            if (item.ParentName != null && item.ParentName != string.Empty)
                cmd += " and su2.Name = '" + item.ParentName + "' ";
            if (item.Name != null && item.Name != string.Empty)
                cmd += " and su1.Name = '" + item.Name + "' ";
            if (item.CompanyID != null && item.CompanyID != string.Empty)
                cmd += " and su1.CompanyID ='" + item.CompanyID + "' ";
            if (item.IsLeaf != 0)
            {
                if (item.IsLeaf == 1)
                    cmd += " and su1.IsLeaf = 1";
                else
                    cmd += " and su1.IsLeaf = 0";
            }

            List<SubjectRelationInfos> list = new List<SubjectRelationInfos>();
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
                throw new DALException("查询预算科目信息失败", e);
            }

            return list;

        }

        public IList<SubjectRelationInfos> SearchName(SubjectPerYear item)
        {
            string cmd = " select  distinct Name from FM2E_BudgetAccountsPerYear where 1=1 ";

            if (item.IsLeaf != 0)
            {
                if (item.IsLeaf == 1)
                    cmd += " and IsLeaf = 1";
                else
                    cmd += " and IsLeaf = 0";
            }

            List<SubjectRelationInfos> list = new List<SubjectRelationInfos>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                    {
                        SubjectRelationInfos result = new SubjectRelationInfos();
                        if (!Convert.IsDBNull(rd["Name"]))
                            result.Name = Convert.ToString(rd["Name"]);
                        list.Add(result);
                    }

                }

            }
            catch (Exception e)
            {
                throw new DALException("查询预算科目信息失败", e);
            }

            return list;

        }

        public IList<SubjectRelationInfos> Search(SubjectPerYear item)
        {
            string cmd = " select " + ReturnFields2 + " from " + TableName2 + Where;
            if (item.SubID != 0)
                cmd += " and su1.SubID = " + item.SubID + " ";
            if (item.ParentID != 0)
                cmd += " and su1.ParentID = " + item.ParentID + " ";
            if (item.ParentName != null && item.ParentName != string.Empty)
                cmd += " and su2.Name = '" + item.ParentName + "' ";
            if (item.Name != null && item.Name != string.Empty)
                cmd += " and su1.Name = '" + item.Name + "' ";
            if (item.CompanyID != null && item.CompanyID != string.Empty)
                cmd += " and su1.CompanyID ='" + item.CompanyID + "' ";
            if (item.IsLeaf != 0)
            {
                if (item.IsLeaf == 1)
                    cmd += " and su1.IsLeaf = 1";
                else
                    cmd += " and su1.IsLeaf = 0";
            }
            if (item.Year != 0)
                cmd += " and su1.Year = " + item.Year + " ";

            List<SubjectRelationInfos> list = new List<SubjectRelationInfos>();
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
                throw new DALException("查询预算科目信息失败", e);
            }

            return list;

        }

    }
}
