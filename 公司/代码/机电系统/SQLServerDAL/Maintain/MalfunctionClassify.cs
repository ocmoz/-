using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Maintain;
using System.Collections;
using FM2E.Model.Maintain;
using FM2E.Model.Exceptions;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;
using System.Data;
using FM2E.Model.Utils;

namespace FM2E.SQLServerDAL.Maintain
{
    public class MalfunctionClassify : IMalfunctionClassify
    {
        private MalfunctionClassifyInfo GetData(IDataReader rd)
        {
            MalfunctionClassifyInfo item = new MalfunctionClassifyInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["System"]))
                item.System = Convert.ToString(rd["System"]);

            if (!Convert.IsDBNull(rd["SystemName"]))
                item.SystemName = Convert.ToString(rd["SystemName"]);

            if (!Convert.IsDBNull(rd["SubSystem"]))
                item.SubSystem = Convert.ToInt64(rd["SubSystem"]);

            if (!Convert.IsDBNull(rd["SubSystemName"]))
                item.SubSystemName = Convert.ToString(rd["SubSystemName"]);

            if (!Convert.IsDBNull(rd["MalfunctionObject"]))
                item.MalfunctionObject = Convert.ToString(rd["MalfunctionObject"]);

            if (!Convert.IsDBNull(rd["MalfunctionDescription"]))
                item.MalfunctionDescription = Convert.ToString(rd["MalfunctionDescription"]);

            if (!Convert.IsDBNull(rd["Rank"]))
                item.Rank = (MalfunctionRank)Convert.ToInt32(rd["Rank"]);

            if (!Convert.IsDBNull(rd["ResponseTime"]))
                item.ResponseTime = Convert.ToInt32(rd["ResponseTime"]);

            if (!Convert.IsDBNull(rd["ResponseUnit"]))
                item.ResponseUnit =(TimeUnits)Convert.ToInt32(rd["ResponseUnit"]);

            if (!Convert.IsDBNull(rd["FunRestoreTime"]))
                item.FunRestoreTime = Convert.ToInt32(rd["FunRestoreTime"]);

            if (!Convert.IsDBNull(rd["FunRestoreUnit"]))
                item.FunRestoreUnit = (TimeUnits)Convert.ToInt32(rd["FunRestoreUnit"]);

            if (!Convert.IsDBNull(rd["RepairTime"]))
                item.RepairTime = Convert.ToInt32(rd["RepairTime"]);

            if (!Convert.IsDBNull(rd["RepairUnit"]))
                item.RepairUnit = (TimeUnits)Convert.ToInt32(rd["RepairUnit"]);

            return item;
        }
        #region IMalfunctionClassify 成员
        /// <summary>
        /// 获取所有的故障分类信息
        /// </summary>
        /// <returns></returns>
        IList IMalfunctionClassify.GetClassifyList()
        {
            ArrayList list = new ArrayList();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select a.ID,a.System,a.SubSystem,a.MalfunctionObject,a.MalfunctionDescription,a.Rank,a.ResponseTime,a.ResponseUnit,a.FunRestoreTime,a.FunRestoreUnit,a.RepairTime,a.RepairUnit,b.SystemName,c.SubSystemName ");
                strSql.Append(" FROM FM2E_MalfunctionClassify a");
                strSql.Append(" left join FM2E_System b on a.System=b.SystemID ");
                strSql.Append(" left join FM2E_SubSystem c on a.SubSystem=c.SubSystemID");
                strSql.Append(" order by System asc,SubSystem asc,ID desc");

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                        list.Add(GetData(rd));
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取故障分类列表失败", ex);
            }
            return list;
        }
        /// <summary>
        /// 获取故障分类列表，支持分页
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList IMalfunctionClassify.GetClassifyList(QueryParam term, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取故障分类分页列表失败", e);
            }
        }
        /// <summary>
        /// 获取某项的故障分类信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MalfunctionClassifyInfo IMalfunctionClassify.GetClassify(long id)
        {
            MalfunctionClassifyInfo item = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  top 1 ID,System,SubSystem,MalfunctionObject,MalfunctionDescription,Rank,ResponseTime,ResponseUnit,FunRestoreTime,FunRestoreUnit,RepairTime,RepairUnit from FM2E_MalfunctionClassify ");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        item = new MalfunctionClassifyInfo();

                        if (!Convert.IsDBNull(rd["ID"]))
                            item.ID = Convert.ToInt64(rd["ID"]);

                        if (!Convert.IsDBNull(rd["System"]))
                            item.System = Convert.ToString(rd["System"]);

                        if (!Convert.IsDBNull(rd["SubSystem"]))
                            item.SubSystem = Convert.ToInt64(rd["SubSystem"]);

                        if (!Convert.IsDBNull(rd["MalfunctionObject"]))
                            item.MalfunctionObject = Convert.ToString(rd["MalfunctionObject"]);

                        if (!Convert.IsDBNull(rd["MalfunctionDescription"]))
                            item.MalfunctionDescription = Convert.ToString(rd["MalfunctionDescription"]);

                        if (!Convert.IsDBNull(rd["Rank"]))
                            item.Rank = (MalfunctionRank)Convert.ToInt32(rd["Rank"]);

                        if (!Convert.IsDBNull(rd["ResponseTime"]))
                            item.ResponseTime = Convert.ToInt32(rd["ResponseTime"]);

                        if (!Convert.IsDBNull(rd["ResponseUnit"]))
                            item.ResponseUnit = (TimeUnits)Convert.ToInt32(rd["ResponseUnit"]);

                        if (!Convert.IsDBNull(rd["FunRestoreTime"]))
                            item.FunRestoreTime = Convert.ToInt32(rd["FunRestoreTime"]);

                        if (!Convert.IsDBNull(rd["FunRestoreUnit"]))
                            item.FunRestoreUnit = (TimeUnits)Convert.ToInt32(rd["FunRestoreUnit"]);

                        if (!Convert.IsDBNull(rd["RepairTime"]))
                            item.RepairTime = Convert.ToInt32(rd["RepairTime"]);

                        if (!Convert.IsDBNull(rd["RepairUnit"]))
                            item.RepairUnit = (TimeUnits)Convert.ToInt32(rd["RepairUnit"]);
                    }
                }
            }
            catch (Exception ex)
            {
                item = null;
                throw new DALException("获取故障分类信息失败", ex);
            }
            return item;
        }
        /// <summary>
        /// 添加故障分类信息
        /// </summary>
        /// <param name="model"></param>
        void IMalfunctionClassify.AddClassify(MalfunctionClassifyInfo model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FM2E_MalfunctionClassify(");
                strSql.Append("System,SubSystem,MalfunctionObject,MalfunctionDescription,Rank,ResponseTime,ResponseUnit,FunRestoreTime,FunRestoreUnit,RepairTime,RepairUnit)");
                strSql.Append(" values (");
                strSql.Append("@System,@SubSystem,@MalfunctionObject,@MalfunctionDescription,@Rank,@ResponseTime,@ResponseUnit,@FunRestoreTime,@FunRestoreUnit,@RepairTime,@RepairUnit)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@System", SqlDbType.VarChar,2),
					new SqlParameter("@SubSystem", SqlDbType.BigInt,8),
					new SqlParameter("@MalfunctionObject", SqlDbType.NVarChar,30),
					new SqlParameter("@MalfunctionDescription", SqlDbType.NVarChar,100),
					new SqlParameter("@Rank", SqlDbType.TinyInt,1),
					new SqlParameter("@ResponseTime", SqlDbType.Int,4),
                    new SqlParameter("@FunRestoreTime", SqlDbType.Int,4),
					new SqlParameter("@RepairTime", SqlDbType.Int,4),
                    new SqlParameter("@ResponseUnit", SqlDbType.TinyInt,1),
                    new SqlParameter("@FunRestoreUnit", SqlDbType.TinyInt,1),
					new SqlParameter("@RepairUnit", SqlDbType.TinyInt,1)};
                parameters[0].Value = model.System;
                parameters[1].Value = model.SubSystem;
                parameters[2].Value = model.MalfunctionObject;
                parameters[3].Value = model.MalfunctionDescription;
                parameters[4].Value = model.Rank;
                parameters[5].Value = model.ResponseTime;
                parameters[6].Value = model.FunRestoreTime;
                parameters[7].Value = model.RepairTime;
                parameters[8].Value = model.ResponseUnit;
                parameters[9].Value = model.FunRestoreUnit;
                parameters[10].Value = model.RepairUnit;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("添加故障分类信息失败", ex);
            }
        }
        /// <summary>
        /// 修改故障分类信息
        /// </summary>
        /// <param name="model"></param>
        void IMalfunctionClassify.UpdateClassify(MalfunctionClassifyInfo model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_MalfunctionClassify set ");
                strSql.Append("System=@System,");
                strSql.Append("SubSystem=@SubSystem,");
                strSql.Append("MalfunctionObject=@MalfunctionObject,");
                strSql.Append("MalfunctionDescription=@MalfunctionDescription,");
                strSql.Append("Rank=@Rank,");
                strSql.Append("ResponseTime=@ResponseTime,");
                strSql.Append("FunRestoreTime=@FunRestoreTime,");
                strSql.Append("RepairTime=@RepairTime,");
                strSql.Append("ResponseUnit=@ResponseUnit,");
                strSql.Append("FunRestoreUnit=@FunRestoreUnit,");
                strSql.Append("RepairUnit=@RepairUnit");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@System", SqlDbType.VarChar,2),
					new SqlParameter("@SubSystem", SqlDbType.BigInt,8),
					new SqlParameter("@MalfunctionObject", SqlDbType.NVarChar,30),
					new SqlParameter("@MalfunctionDescription", SqlDbType.NVarChar,100),
					new SqlParameter("@Rank", SqlDbType.TinyInt,1),
					new SqlParameter("@ResponseTime", SqlDbType.Int,4),
                    new SqlParameter("@FunRestoreTime", SqlDbType.Int,4),
					new SqlParameter("@RepairTime", SqlDbType.Int,4),
                    new SqlParameter("@ResponseUnit", SqlDbType.TinyInt,1),
                    new SqlParameter("@FunRestoreUnit", SqlDbType.TinyInt,1),
					new SqlParameter("@RepairUnit", SqlDbType.TinyInt,1)};
                parameters[0].Value = model.ID;
                parameters[1].Value = model.System;
                parameters[2].Value = model.SubSystem;
                parameters[3].Value = model.MalfunctionObject;
                parameters[4].Value = model.MalfunctionDescription;
                parameters[5].Value = model.Rank;
                parameters[6].Value = model.ResponseTime;
                parameters[7].Value = model.FunRestoreTime;
                parameters[8].Value = model.RepairTime;
                parameters[9].Value = model.ResponseUnit;
                parameters[10].Value = model.FunRestoreUnit;
                parameters[11].Value = model.RepairUnit;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("修改故障分类信息失败", ex);
            }
        }
        /// <summary>
        /// 删除故障分类信息
        /// </summary>
        /// <param name="id"></param>
        void IMalfunctionClassify.DeleteClassify(long id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_MalfunctionClassify ");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("删除故障分类信息失败", ex);
            }
        }
  
        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam IMalfunctionClassify.GenerateSearchTerm(MalfunctionClassifySearchInfo term)
        {
            #region 生成where条件
            string sqlSearch = " where 1=1";
            if (term.System != "0" && !string.IsNullOrEmpty(term.System))
                sqlSearch += " and a.System='" + term.System.Trim() + "'";

            if (term.SubSystem != 0)
                sqlSearch += " and a.SubSystem=" + term.SubSystem;

            if (!string.IsNullOrEmpty(term.MalfunctionObject))
                sqlSearch += " and a.MalfunctionObject like '%" + term.MalfunctionObject.Trim() + "%'";

            if (term.Rank != 0)
                sqlSearch += " and a.Rank=" + term.Rank;

            #endregion

            QueryParam qp = new QueryParam();

            qp.TableName = "FM2E_MalfunctionClassify a ";
            qp.TableName += " left join FM2E_System b on a.System=b.SystemID ";
            qp.TableName += " left join FM2E_SubSystem c on a.SubSystem=c.SubSystemID";

            qp.ReturnFields = "a.ID as ID,a.System as System,a.SubSystem as SubSystem,a.MalfunctionObject,a.MalfunctionDescription,a.Rank,a.ResponseTime,a.ResponseUnit,a.FunRestoreTime,a.FunRestoreUnit,a.RepairTime,a.RepairUnit,b.SystemName,c.SubSystemName ";
            qp.OrderBy = "order by System asc,SubSystem asc,ID desc";
            qp.Where = sqlSearch;

            return qp;
        }
        #endregion

    }
}
