using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FM2E.IDAL.Maintain;
using FM2E.Model.Maintain;
using System.Data;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;
using System.Data.Common;
using System.Data.SqlTypes;


namespace FM2E.SQLServerDAL.Maintain
{
    /// <summary>
    /// 常规维护数据访问类
    /// </summary>
    public class Maintain:IMaintain
    {
        /// <summary>
        /// 获取维护项实体
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private MaintainItemInfo GetMaintainItemData(IDataReader rd)
        {
            MaintainItemInfo item = new MaintainItemInfo();

            if (!Convert.IsDBNull(rd["Content"]))
                item.Content = Convert.ToString(rd["Content"]);

            if (!Convert.IsDBNull(rd["MaintainType"]))
                item.MaintainType = (MaintainType)Convert.ToInt32(rd["MaintainType"]);

            if (!Convert.IsDBNull(rd["Object"]))
                item.Object = Convert.ToString(rd["Object"]);


            if (!Convert.IsDBNull(rd["Period"]))
                item.Period = Convert.ToInt32(rd["Period"]);

            if (!Convert.IsDBNull(rd["PeriodUnit"]))
                item.PeriodUnit = (MaintainIntervalUnit)Convert.ToInt32(rd["PeriodUnit"]);

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["Standard"]))
                item.Standard = Convert.ToString(rd["Standard"]);

            if (!Convert.IsDBNull(rd["SubSystemID"]))
                item.SubSystemID = Convert.ToInt64(rd["SubSystemID"]);

            if (!Convert.IsDBNull(rd["SubSystemName"]))
                item.SubSystemName = Convert.ToString(rd["SubSystemName"]);

            if (!Convert.IsDBNull(rd["SystemID"]))
                item.SystemID = Convert.ToString(rd["SystemID"]);

            if (!Convert.IsDBNull(rd["SystemName"]))
                item.SystemName = Convert.ToString(rd["SystemName"]);

            return item;
        }
        /// <summary>
        /// 获取维护模板表实体信息
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private TemplateMaintainSheetInfo GetTemplateMaintainSheetInfo(IDataReader rd)
        {
            TemplateMaintainSheetInfo item = new TemplateMaintainSheetInfo();

            if (!Convert.IsDBNull(rd["AddressCode"]))
                item.AddressCode = Convert.ToString(rd["AddressCode"]);

            if (!Convert.IsDBNull(rd["AddressID"]))
                item.AddressID = Convert.ToInt64(rd["AddressID"]);

            if (!Convert.IsDBNull(rd["AddressName"]))
                item.AddressName = Convert.ToString(rd["AddressName"]);

            if (!Convert.IsDBNull(rd["DepartmentID"]))
                item.DepartmentID = Convert.ToInt64(rd["DepartmentID"]);

            if (!Convert.IsDBNull(rd["DepartmentName"]))
                item.DepartmentName = Convert.ToString(rd["DepartmentName"]);

            if (!Convert.IsDBNull(rd["IsNotUsed"]))
                item.IsNotUsed = Convert.ToBoolean(rd["IsNotUsed"]);

            if (!Convert.IsDBNull(rd["IsTemp"]))
                item.IsTemp = Convert.ToBoolean(rd["IsTemp"]);

            if (!Convert.IsDBNull(rd["LastExecuteTime"]))
                item.LastExecuteTime = Convert.ToDateTime(rd["LastExecuteTime"]);

            if (!Convert.IsDBNull(rd["MaintainType"]))
                item.MaintainType = (MaintainType)Convert.ToInt32(rd["MaintainType"]);

            if (!Convert.IsDBNull(rd["ModifierName"]))
                item.ModifierName = Convert.ToString(rd["ModifierName"]);

            if (!Convert.IsDBNull(rd["Modifier"]))
                item.Modifier = Convert.ToString(rd["Modifier"]);

            if (!Convert.IsDBNull(rd["Period"]))
                item.Period = Convert.ToInt32(rd["Period"]);

            if (!Convert.IsDBNull(rd["PeriodUnit"]))
                item.PeriodUnit = (MaintainIntervalUnit)Convert.ToInt32(rd["PeriodUnit"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["SaveTime"]))
                item.SaveTime = Convert.ToDateTime(rd["SaveTime"]);

            if (!Convert.IsDBNull(rd["SystemID"]))
                item.SystemID = Convert.ToString(rd["SystemID"]);

            if (!Convert.IsDBNull(rd["SystemName"]))
                item.SystemName = Convert.ToString(rd["SystemName"]);

            if (!Convert.IsDBNull(rd["TemplateSheetID"]))
                item.TemplateSheetID = Convert.ToInt64(rd["TemplateSheetID"]);

            if (!Convert.IsDBNull(rd["TemplateSheetName"]))
                item.TemplateSheetName = Convert.ToString(rd["TemplateSheetName"]);

            return item;
        }
        /// <summary>
        /// 获取维护表实体信息
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private MaintainSheetInfo GetMaintainSheetData(IDataReader rd)
        {
            MaintainSheetInfo item = new MaintainSheetInfo();

            if (!Convert.IsDBNull(rd["SheetID"]))
                item.SheetID = Convert.ToInt64(rd["SheetID"]);

            if (!Convert.IsDBNull(rd["TemplateSheetID"]))
                item.TemplateSheetID = Convert.ToInt64(rd["TemplateSheetID"]);

            if (!Convert.IsDBNull(rd["AddressCode"]))
                item.AddressCode = Convert.ToString(rd["AddressCode"]);

            if (!Convert.IsDBNull(rd["AddressID"]))
                item.AddressID = Convert.ToInt64(rd["AddressID"]);

            if (!Convert.IsDBNull(rd["AddressName"]))
                item.AddressName = Convert.ToString(rd["AddressName"]);

            if (!Convert.IsDBNull(rd["Confirmer"]))
                item.Confirmer = Convert.ToString(rd["Confirmer"]);

            if (!Convert.IsDBNull(rd["ConfirmerName"]))
                item.ConfirmerName = Convert.ToString(rd["ConfirmerName"]);

            if (!Convert.IsDBNull(rd["ConfirmRemark"]))
                item.ConfirmRemark = Convert.ToString(rd["ConfirmRemark"]);

            if (!Convert.IsDBNull(rd["ConfirmResult"]))
                item.ConfirmResult = (MaintainConfirmResult)Convert.ToInt32(rd["ConfirmResult"]);

            if (!Convert.IsDBNull(rd["ConfirmTime"]))
                item.ConfirmTime = Convert.ToDateTime(rd["ConfirmTime"]);

            if (!Convert.IsDBNull(rd["DepartmentID"]))
                item.DepartmentID = Convert.ToInt64(rd["DepartmentID"]);

            if (!Convert.IsDBNull(rd["DepartmentName"]))
                item.DepartmentName = Convert.ToString(rd["DepartmentName"]);

            if (!Convert.IsDBNull(rd["Maintainer"]))
                item.Maintainer = Convert.ToString(rd["Maintainer"]);

            if (!Convert.IsDBNull(rd["MaintainerName"]))
                item.MaintainerName = Convert.ToString(rd["MaintainerName"]);

            if (!Convert.IsDBNull(rd["MaintainTime"]))
                item.MaintainTime = Convert.ToDateTime(rd["MaintainTime"]);

            if (!Convert.IsDBNull(rd["MaintainType"]))
                item.MaintainType = (MaintainType)Convert.ToInt32(rd["MaintainType"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["Result"]))
                item.Result = Convert.ToString(rd["Result"]);

            if (!Convert.IsDBNull(rd["SystemID"]))
                item.SystemID = Convert.ToString(rd["SystemID"]);

            if (!Convert.IsDBNull(rd["SystemName"]))
                item.SystemName = Convert.ToString(rd["SystemName"]);

            if (!Convert.IsDBNull(rd["SheetNO"]))
                item.SheetNO = Convert.ToString(rd["SheetNO"]);

            if (!Convert.IsDBNull(rd["SheetName"]))
                item.SheetName = Convert.ToString(rd["SheetName"]);

            if (!Convert.IsDBNull(rd["Period"]))
                item.Period = Convert.ToInt32(rd["Period"]);

            if (!Convert.IsDBNull(rd["PeriodUnit"]))
                item.PeriodUnit = (MaintainIntervalUnit)Convert.ToInt32(rd["PeriodUnit"]);

            if (!Convert.IsDBNull(rd["LastExecuteTime"]))
                item.LastExecuteTime = Convert.ToDateTime(rd["LastExecuteTime"]);


            if (!Convert.IsDBNull(rd["HasAbnormal"]))
                item.HasAbnormal = Convert.ToBoolean(rd["HasAbnormal"]);

            if (!Convert.IsDBNull(rd["IsTemp"]))
                item.IsTemp = Convert.ToBoolean(rd["IsTemp"]);

            if (!Convert.IsDBNull(rd["SaveTime"]))
                item.SaveTime = Convert.ToDateTime(rd["SaveTime"]);

            return item;
        }
        /// <summary>
        /// 获取维护表设备的实体信息
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private MaintainSheetEquipmentInfo GetMaintainSheetEquipmentData(IDataReader rd)
        {
            MaintainSheetEquipmentInfo item = new MaintainSheetEquipmentInfo();

            if (!Convert.IsDBNull(rd["DetailLocation"]))
                item.DetailLocation = Convert.ToString(rd["DetailLocation"]);

            if (!Convert.IsDBNull(rd["AddressID"]))
                item.AddressID = Convert.ToInt64(rd["AddressID"]);

            if (!Convert.IsDBNull(rd["AddressName"]))
                item.AddressName = Convert.ToString(rd["AddressName"]);

            if (!Convert.IsDBNull(rd["EquipmentModel"]))
                item.EquipmentModel = Convert.ToString(rd["EquipmentModel"]);

            if (!Convert.IsDBNull(rd["EquipmentName"]))
                item.EquipmentName = Convert.ToString(rd["EquipmentName"]);

            if (!Convert.IsDBNull(rd["EquipmentNO"]))
                item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

            if (!Convert.IsDBNull(rd["EquipmentID"]))
                item.EquipmentID = Convert.ToInt64(rd["EquipmentID"]);

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["SheetID"]))
                item.SheetID = Convert.ToInt64(rd["SheetID"]);

            if (!Convert.IsDBNull(rd["NewStatus"]))
                item.NewStatus = (EquipmentStatus)Convert.ToInt32(rd["NewStatus"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["IsNormal"]))
                item.IsNormal = Convert.ToBoolean(rd["IsNormal"]);

            if (!Convert.IsDBNull(rd["IsExtra"]))
                item.IsExtra = Convert.ToBoolean(rd["IsExtra"]);

            if (!Convert.IsDBNull(rd["Maintainer"]))
            {
                item.MaintainerID = Convert.ToString(rd["Maintainer"]);
            }
            if (!Convert.IsDBNull(rd["MaintainerName"]))
            {
                item.MaintainerName = Convert.ToString(rd["MaintainerName"]);
            }
            if (!Convert.IsDBNull(rd["MaintainTime"]))
            {
                item.MaintainTime = Convert.ToDateTime(rd["MaintainTime"]);
            }
            if (!Convert.IsDBNull(rd["MaintainType"]))
            {
                item.MaintainType = (MaintainType) Convert.ToInt32(rd["MaintainType"]);
            }
            return item;
        }
        /// <summary>
        /// 获取维护模板表设备的实体信息
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private TemplateSheetEquipmentInfo GetTemplateSheetEquipmentData(IDataReader rd)
        {
            TemplateSheetEquipmentInfo item = new TemplateSheetEquipmentInfo();

            if (!Convert.IsDBNull(rd["DetailLocation"]))
                item.DetailLocation = Convert.ToString(rd["DetailLocation"]);

            if (!Convert.IsDBNull(rd["EquipmentModel"]))
                item.EquipmentModel = Convert.ToString(rd["EquipmentModel"]);

            if (!Convert.IsDBNull(rd["EquipmentName"]))
                item.EquipmentName = Convert.ToString(rd["EquipmentName"]);

            if (!Convert.IsDBNull(rd["EquipmentNO"]))
                item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

            if (!Convert.IsDBNull(rd["AddressID"]))
                item.AddressID = Convert.ToInt64(rd["AddressID"]);

            if (!Convert.IsDBNull(rd["AddressName"]))
                item.AddressName = Convert.ToString(rd["AddressName"]);

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["TemplateSheetID"]))
                item.TemplateSheetID = Convert.ToInt64(rd["TemplateSheetID"]);

            if (!Convert.IsDBNull(rd["EquipmentID"]))
                item.EquipmentID = Convert.ToInt64(rd["EquipmentID"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            return item;
        }

        #region IMaintain 成员
        /// <summary>
        /// 添加维护项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long IMaintain.AddMaintainItem(MaintainItemInfo model)
        {
            long id = 0;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FM2E_MaintainItems(");
                strSql.Append("MaintainType,SystemID,SubSystemID,Period,PeriodUnit,Object,Content,Standard,PeriodInDays)");
                strSql.Append(" values (");
                strSql.Append("@MaintainType,@SystemID,@SubSystemID,@Period,@PeriodUnit,@Object,@Content,@Standard,@PeriodInDays)");
                strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
                SqlParameter[] parameters = {
					new SqlParameter("@MaintainType", SqlDbType.TinyInt,1),
					new SqlParameter("@SystemID", SqlDbType.VarChar,2),
					new SqlParameter("@SubSystemID", SqlDbType.BigInt,8),
					new SqlParameter("@Period", SqlDbType.Int,4),
					new SqlParameter("@PeriodUnit", SqlDbType.TinyInt,1),
					new SqlParameter("@Object", SqlDbType.NVarChar,50),
					new SqlParameter("@Content", SqlDbType.NVarChar,200),
					new SqlParameter("@Standard", SqlDbType.NVarChar,200),
                    new SqlParameter("@PeriodInDays",SqlDbType.Int,4)};
                parameters[0].Value = model.MaintainType;
                parameters[1].Value = model.SystemID;
                parameters[2].Value = model.SubSystemID;
                parameters[3].Value = model.Period;
                parameters[4].Value = model.PeriodUnit;
                parameters[5].Value = model.Object;
                parameters[6].Value = model.Content;
                parameters[7].Value = model.Standard;
                parameters[8].Value = model.PeriodInDays;

                id =(long) SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("添加维护项失败", ex);
            }
            return id;
        }
        /// <summary>
        /// 修改维护项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long IMaintain.UpdateMaintainItem(MaintainItemInfo model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_MaintainItems set ");
                strSql.Append("MaintainType=@MaintainType,");
                strSql.Append("SystemID=@SystemID,");
                strSql.Append("SubSystemID=@SubSystemID,");
                strSql.Append("Period=@Period,");
                strSql.Append("PeriodUnit=@PeriodUnit,");
                strSql.Append("Object=@Object,");
                strSql.Append("Content=@Content,");
                strSql.Append("Standard=@Standard,");
                strSql.Append("PeriodInDays=@PeriodInDays");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@MaintainType", SqlDbType.TinyInt,1),
					new SqlParameter("@SystemID", SqlDbType.VarChar,2),
					new SqlParameter("@SubSystemID", SqlDbType.BigInt,8),
					new SqlParameter("@Period", SqlDbType.Int,4),
					new SqlParameter("@PeriodUnit", SqlDbType.TinyInt,1),
					new SqlParameter("@Object", SqlDbType.NVarChar,50),
					new SqlParameter("@Content", SqlDbType.NVarChar,200),
					new SqlParameter("@Standard", SqlDbType.NVarChar,200),
                    new SqlParameter("@PeriodInDays",SqlDbType.Int,4)};
                parameters[0].Value = model.ID;
                parameters[1].Value = model.MaintainType;
                parameters[2].Value = model.SystemID;
                parameters[3].Value = model.SubSystemID;
                parameters[4].Value = model.Period;
                parameters[5].Value = model.PeriodUnit;
                parameters[6].Value = model.Object;
                parameters[7].Value = model.Content;
                parameters[8].Value = model.Standard;
                parameters[9].Value = model.PeriodInDays;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("更新维护项失败", ex);
            }
            return model.ID;
        }
        /// <summary>
        /// 删除维护项
        /// </summary>
        /// <param name="id"></param>
        void IMaintain.DeleteMaintainItem(long id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_MaintainItems ");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("删除维护失败", ex);
            }
        }
        /// <summary>
        /// 获取一个维护项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MaintainItemInfo IMaintain.GetMaintainItem(long id)
        {
            MaintainItemInfo item=null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from FM2E_MaintainItemsView ");
                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                        item = GetMaintainItemData(rd);
                }
            }
            catch (Exception ex)
            {
                item = null;
                throw new DALException("获取维护项失败", ex);
            }
            return item;
        }
        /// <summary>
        /// 生成维护项查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>s
        QueryParam IMaintain.GetSearchTerm(MaintainItemSearchInfo term)
        {
            QueryParam qp = new QueryParam();

            string sqlSearch = GenerateSqlWhereString(term);

            qp.Where = sqlSearch;
            qp.TableName = "FM2E_MaintainItemsView";
            qp.ReturnFields = "*";
            qp.OrderBy = "Order by ID desc";

            return qp;
        }
        /// <summary>
        /// 生成维护项where条件 
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        private string GenerateSqlWhereString(MaintainItemSearchInfo term)
        {
            string sqlSearch = "where 1=1 ";

            if (term.MaintainType != MaintainType.Unknown)
            {
                sqlSearch += " and MaintainType=" + (int)term.MaintainType;
            }

            if (!string.IsNullOrEmpty(term.Object))
                sqlSearch += " and Object like '%" + term.Object + "%'";

            if (!string.IsNullOrEmpty(term.SystemID))
                sqlSearch += " and SystemID='" + term.SystemID + "'";

            if (term.SubSystemID != 0)
                sqlSearch += " and SubSystemID=" + term.SubSystemID;

            if (term.PeriodUnit != MaintainIntervalUnit.Unknown)
                sqlSearch += " and PeriodUnit=" + (int)term.PeriodUnit;
            return sqlSearch;
        }
        /// <summary>
        /// 搜索维护项（分页）
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList IMaintain.SearchMaintainItem(QueryParam qp, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetMaintainItemData, qp, out recordCount);
            }
            catch (Exception ex)
            {
                throw new DALException("获取维护项分页失败", ex);
            }
        }
        /// <summary>
        /// 搜索维护项（不分页）
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList IMaintain.SearchMaintainItem(MaintainItemSearchInfo term)
        {
            ArrayList list = new ArrayList();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from FM2E_MaintainItemsView ");
                strSql.Append(GenerateSqlWhereString(term));

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        list.Add(GetMaintainItemData(rd));
                    }
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("查询维护项失败", ex);
            }
            return list;
        }
        /// <summary>
        /// 添加一个维护模板表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        long IMaintain.AddTemplateMaintainSheet(TemplateMaintainSheetInfo model,DbTransaction trans)
        {
            long id = AddTemplateMaintainSheet(model, trans);
            //model.TemplateSheetID = id;
            UpdateTemplateSheetEquipments(id,model, trans);
            return id;
        }
        /// <summary>
        /// 添加维护模板表主表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        private long AddTemplateMaintainSheet(TemplateMaintainSheetInfo model, DbTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_TemplateMaintainSheet(");
            strSql.Append("Remark,Modifier,IsNotUsed,SaveTime,TemplateSheetName,DepartmentID,AddressID,SystemID,MaintainType,Period,PeriodUnit,IsTemp,PeriodInDays)");
            strSql.Append(" values (");
            strSql.Append("@Remark,@Modifier,@IsNotUsed,@SaveTime,@TemplateSheetName,@DepartmentID,@AddressID,@SystemID,@MaintainType,@Period,@PeriodUnit,@IsTemp,@PeriodInDays)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT)");
            SqlParameter[] parameters = {
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@Modifier", SqlDbType.VarChar,20),
					new SqlParameter("@IsNotUsed", SqlDbType.Bit,1),
					new SqlParameter("@SaveTime", SqlDbType.DateTime),
					new SqlParameter("@TemplateSheetName", SqlDbType.NVarChar,50),
					new SqlParameter("@DepartmentID", SqlDbType.BigInt,8),
					new SqlParameter("@AddressID", SqlDbType.BigInt,8),
					new SqlParameter("@SystemID", SqlDbType.VarChar,2),
					new SqlParameter("@MaintainType", SqlDbType.TinyInt,1),
					new SqlParameter("@Period", SqlDbType.Int,4),
					new SqlParameter("@PeriodUnit", SqlDbType.TinyInt,1),
					new SqlParameter("@IsTemp", SqlDbType.Bit,1),
                    new SqlParameter("@PeriodInDays",SqlDbType.Int,4)};
            parameters[0].Value = model.Remark;
            parameters[1].Value = model.Modifier;
            parameters[2].Value = model.IsNotUsed;
            parameters[3].Value = model.SaveTime;
            parameters[4].Value = model.TemplateSheetName;
            parameters[5].Value = model.DepartmentID;
            parameters[6].Value = model.AddressID;
            parameters[7].Value = model.SystemID;
            parameters[8].Value = model.MaintainType;
            parameters[9].Value = model.Period;
            parameters[10].Value = model.PeriodUnit;
            parameters[11].Value = model.IsTemp;
            parameters[12].Value = model.PeriodInDays;

            long id = (long)SQLHelper.ExecuteScalar((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters);
            return id;
        }
        /// <summary>
        /// 添加维护模板表设备
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        private void UpdateTemplateSheetEquipments(long sheetID,TemplateMaintainSheetInfo model, DbTransaction trans)
        {
            if (model.Equipments == null || model.Equipments.Count == 0)
                return;
            //先删除，后插入
            StringBuilder strDel = new StringBuilder();
            strDel.Append("delete FM2E_TemplateSheetEquipments ");
            strDel.Append(" where TemplateSheetID=@TemplateSheetID ");
            SqlParameter[] paramDel = {
					new SqlParameter("@TemplateSheetID", SqlDbType.BigInt)};
            paramDel[0].Value = sheetID;
            SQLHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, strDel.ToString(), paramDel);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_TemplateSheetEquipments(");
            strSql.Append("TemplateSheetID,EquipmentNO,EquipmentName,EquipmentModel,DetailLocation,Remark,AddressID,AddressName,EquipmentID)");
            strSql.Append(" values (");
            strSql.Append("@TemplateSheetID,@EquipmentNO,@EquipmentName,@EquipmentModel,@DetailLocation,@Remark,@AddressID,@AddressName,@EquipmentID);");
            SqlParameter[] parameters = {
					new SqlParameter("@TemplateSheetID", SqlDbType.BigInt,8),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
					new SqlParameter("@EquipmentName", SqlDbType.NVarChar,20),
					new SqlParameter("@EquipmentModel", SqlDbType.NVarChar,20),
					new SqlParameter("@DetailLocation", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
                    new SqlParameter("@AddressID", SqlDbType.BigInt,8),
                    new SqlParameter("@AddressName", SqlDbType.NVarChar,200),
                    new SqlParameter("@EquipmentID", SqlDbType.BigInt,8)
                                        };

            //循环插入
            foreach (TemplateSheetEquipmentInfo item in model.Equipments)
            {
                parameters[0].Value = sheetID;
                parameters[1].Value = item.EquipmentNO;
                parameters[2].Value = item.EquipmentName;
                parameters[3].Value = item.EquipmentModel;
                parameters[4].Value = item.DetailLocation;
                parameters[5].Value = item.Remark;
                parameters[6].Value = item.AddressID;
                parameters[7].Value = item.AddressName;
                parameters[8].Value = item.EquipmentID;

                SQLHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters);
            }
        }
        /// <summary>
        /// 修改维护模板表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        long IMaintain.UpdateTemplateMaintainSheet(TemplateMaintainSheetInfo model,DbTransaction trans)
        {
            long id = model.TemplateSheetID;

            UpdateTemplateMaintainSheet(model, trans);
            UpdateTemplateSheetEquipments(id, model, trans);

            return id;
        }
        /// <summary>
        /// 更新维护模板主表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        private void UpdateTemplateMaintainSheet(TemplateMaintainSheetInfo model, DbTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_TemplateMaintainSheet set ");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Modifier=@Modifier,");
            strSql.Append("IsNotUsed=@IsNotUsed,");
            strSql.Append("SaveTime=@SaveTime,");
            strSql.Append("TemplateSheetName=@TemplateSheetName,");
            strSql.Append("DepartmentID=@DepartmentID,");
            strSql.Append("AddressID=@AddressID,");
            strSql.Append("SystemID=@SystemID,");
            strSql.Append("MaintainType=@MaintainType,");
            strSql.Append("Period=@Period,");
            strSql.Append("PeriodUnit=@PeriodUnit,");
            strSql.Append("IsTemp=@IsTemp,");
            strSql.Append("PeriodInDays=@PeriodInDays");
            strSql.Append(" where TemplateSheetID=@TemplateSheetID ");
            SqlParameter[] parameters = {
					new SqlParameter("@TemplateSheetID", SqlDbType.BigInt,8),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@Modifier", SqlDbType.VarChar,20),
					new SqlParameter("@IsNotUsed", SqlDbType.Bit,1),
					new SqlParameter("@SaveTime", SqlDbType.DateTime),
					new SqlParameter("@TemplateSheetName", SqlDbType.NVarChar,50),
					new SqlParameter("@DepartmentID", SqlDbType.BigInt,8),
					new SqlParameter("@AddressID", SqlDbType.BigInt,8),
					new SqlParameter("@SystemID", SqlDbType.VarChar,2),
					new SqlParameter("@MaintainType", SqlDbType.TinyInt,1),
					new SqlParameter("@Period", SqlDbType.Int,4),
					new SqlParameter("@PeriodUnit", SqlDbType.TinyInt,1),
					new SqlParameter("@IsTemp", SqlDbType.Bit,1),
                    new SqlParameter("@PeriodInDays",SqlDbType.Int,4)};
            parameters[0].Value = model.TemplateSheetID;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.Modifier;
            parameters[3].Value = model.IsNotUsed;
            parameters[4].Value = model.SaveTime;
            parameters[5].Value = model.TemplateSheetName;
            parameters[6].Value = model.DepartmentID;
            parameters[7].Value = model.AddressID;
            parameters[8].Value = model.SystemID;
            parameters[9].Value = model.MaintainType;
            parameters[10].Value = model.Period;
            parameters[11].Value = model.PeriodUnit;
            parameters[12].Value = model.IsTemp;
            parameters[13].Value = model.PeriodInDays;

            SQLHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 删除维护模板表
        /// </summary>
        /// <param name="id"></param>
        void IMaintain.DeleteTemplateMaintainSheet(long id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_TemplateMaintainSheet ");
                strSql.Append(" where TemplateSheetID=@TemplateSheetID ");
                SqlParameter[] parameters = {
					new SqlParameter("@TemplateSheetID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("删除维护模板表失败", ex);
            }
        }
        /// <summary>
        /// 获取一个维护模板表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TemplateMaintainSheetInfo IMaintain.GetTemplateMaintainSheet(long id)
        {
            TemplateMaintainSheetInfo item = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select *,(select max(MaintainTime) as LastExecuteTime from FM2E_MaintainSheet where TemplateSheetID=@TemplateSheetID) as LastExecuteTime ");
                strSql.Append(" from FM2E_TemplateMaintainSheetView ");
                strSql.Append(" where TemplateSheetID=@TemplateSheetID");
                SqlParameter[] parameters = {
					new SqlParameter("@TemplateSheetID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                        item = GetTemplateMaintainSheetInfo(rd);
                }

                if (item != null)
                {
                    StringBuilder strSqlEquipment = new StringBuilder();
                    strSqlEquipment.Append("select * from FM2E_TemplateSheetEquipments ");
                    strSqlEquipment.Append(" where TemplateSheetID=@TemplateSheetID");
                    strSqlEquipment.Append(" order by AddressID asc");

                    parameters[0].Value = id;

                    ArrayList list=new ArrayList();
                    using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSqlEquipment.ToString(), parameters))
                    {
                        while (rd.Read())
                            list.Add(GetTemplateSheetEquipmentData(rd));
                    }

                    item.Equipments = list;
                }

            }
            catch (Exception ex)
            {
                throw new DALException("获取维护模板表失败", ex);
            }

            return item;
        }
        /// <summary>
        /// 生成维护模板表查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam IMaintain.GetSearchTerm(TemplateMaintainSheetSearchInfo term)
        {
            string sqlSearch = " where 1=1 ";

            if (!string.IsNullOrEmpty(term.ModifierName))
                sqlSearch += " and ModifierName like '%" + term.Modifier + "%'";

            if (!string.IsNullOrEmpty(term.Modifier))
                sqlSearch += " and Modifier='" + term.Modifier + "'";

            if (term.IsNotUsed.HasValue)
                sqlSearch += " and IsNotUser=" + (term.IsNotUsed.Value ? "1" : "0");

            if (!string.IsNullOrEmpty(term.TemplateSheetName))
                sqlSearch += " and TemplateSheetName like '%" + term.TemplateSheetName + "%'";

            if (term.Department != 0)
                sqlSearch += " and DepartmentID=" + term.Department;

            if (term.AddressID != 0)
                sqlSearch += " and AddressID=" + term.AddressID;

            if (!string.IsNullOrEmpty(term.AddressCode))
                sqlSearch += " and AddressCode like '" + term.AddressCode + "%'";

            if (!string.IsNullOrEmpty(term.AddressName))
                sqlSearch += " and AddressName like '%" + term.AddressName + "%'";

            if (!string.IsNullOrEmpty(term.SystemID))
                sqlSearch += " and SystemID='" + term.SystemID + "'";

            if (term.MaintainType != MaintainType.Unknown)
            {
                sqlSearch += " and MaintainType=" + (int)term.MaintainType;
            }

            if (term.IsTemp.HasValue)
                sqlSearch += " and IsTemp=" + (term.IsTemp.Value ? "1" : "0");

            if (term.PeriodUnit != MaintainIntervalUnit.Unknown)
            {
                sqlSearch += " and PeriodUnit=" + (int)term.PeriodUnit;
            }

            if (DateTime.Compare(term.SaveTimeFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(term.SaveTimeFrom, sqlMinDate) < 0)
                    term.SaveTimeFrom = sqlMinDate;

                sqlSearch += " and SaveTime>='" + term.SaveTimeFrom.ToString("yyyy-MM-dd") + " 00:00:00'";
            }

            if (DateTime.Compare(term.SaveTimeTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(term.SaveTimeTo, sqlMaxDate) > 0)
                    term.SaveTimeTo = sqlMaxDate;

                sqlSearch += " and SaveTime<='" + term.SaveTimeTo.ToString("yyyy-MM-dd") + " 23:59:59'";
            }

            QueryParam qp = new QueryParam();
            qp.ReturnFields = "*,(SELECT MAX(MaintainTime) AS LastExecuteTime FROM FM2E_MaintainSheet WHERE (TemplateSheetID = FM2E_TemplateMaintainSheetView.TemplateSheetID)) AS LastExecuteTime ";
            qp.TableName = "FM2E_TemplateMaintainSheetView";
            qp.Where = sqlSearch;
            qp.OrderBy = "order by LastExecuteTime desc";

            return qp;
        }
        /// <summary>
        /// 查询维护模板表
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList IMaintain.SearchTemplateMaintainSheet(QueryParam qp, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetTemplateMaintainSheetInfo,qp, out recordCount);
            }
            catch (Exception ex)
            {
                throw new DALException("查询维护模板表失败", ex);
            }
        }
        /// <summary>
        /// 添加一个维护表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        long IMaintain.AddMaintainSheet(MaintainSheetInfo model, DbTransaction trans)
        {
            long id = AddMaintainSheet(model, trans);
            UpdateMaintainSheetEquipments(id, model, trans);
            return id;
        }
        /// <summary>
        /// 添加维护表主表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        private long AddMaintainSheet(MaintainSheetInfo model, DbTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_MaintainSheet(");
            strSql.Append("Maintainer,MaintainTime,MaintainType,Result,Remark,LastExecuteTime,ConfirmResult,Confirmer,ConfirmRemark,ConfirmTime,TemplateSheetID,HasAbnormal,SheetNO,SheetName,AddressID,SystemID,DepartmentID,Period,PeriodUnit,IsTemp,PeriodInDays,SaveTime)");
            strSql.Append(" values (");
            strSql.Append("@Maintainer,@MaintainTime,@MaintainType,@Result,@Remark,@LastExecuteTime,@ConfirmResult,@Confirmer,@ConfirmRemark,@ConfirmTime,@TemplateSheetID,@HasAbnormal,@SheetNO,@SheetName,@AddressID,@SystemID,@DepartmentID,@Period,@PeriodUnit,@IsTemp,@PeriodInDays,@SaveTime)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@Maintainer", SqlDbType.VarChar,20),
					new SqlParameter("@MaintainTime", SqlDbType.DateTime),
					new SqlParameter("@MaintainType", SqlDbType.TinyInt,1),
					new SqlParameter("@Result", SqlDbType.NVarChar,200),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@LastExecuteTime", SqlDbType.DateTime),
					new SqlParameter("@ConfirmResult", SqlDbType.TinyInt,1),
					new SqlParameter("@Confirmer", SqlDbType.VarChar,20),
					new SqlParameter("@ConfirmRemark", SqlDbType.NVarChar,100),
					new SqlParameter("@ConfirmTime", SqlDbType.DateTime),
					new SqlParameter("@TemplateSheetID", SqlDbType.BigInt,8),
					new SqlParameter("@HasAbnormal", SqlDbType.Bit,1),
					new SqlParameter("@SheetNO", SqlDbType.VarChar,20),
					new SqlParameter("@SheetName", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressID", SqlDbType.BigInt,8),
					new SqlParameter("@SystemID", SqlDbType.VarChar,2),
					new SqlParameter("@DepartmentID", SqlDbType.BigInt,8),
					new SqlParameter("@Period", SqlDbType.Int,4),
					new SqlParameter("@PeriodUnit", SqlDbType.TinyInt,1),
                    new SqlParameter("@IsTemp",SqlDbType.Bit,1),
                    new SqlParameter("@PeriodInDays",SqlDbType.Int,4),
                    new SqlParameter("@SaveTime",SqlDbType.DateTime)};
            parameters[0].Value = model.Maintainer;
            parameters[1].Value = model.MaintainTime == DateTime.MinValue ? SqlDateTime.Null : model.MaintainTime;
            parameters[2].Value = model.MaintainType;
            parameters[3].Value = model.Result;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.LastExecuteTime == DateTime.MinValue ? SqlDateTime.Null : model.LastExecuteTime;
            parameters[6].Value = model.ConfirmResult;
            parameters[7].Value = model.Confirmer;
            parameters[8].Value = model.ConfirmRemark;
            parameters[9].Value = model.ConfirmTime == DateTime.MinValue ? SqlDateTime.Null : model.ConfirmTime;
            parameters[10].Value = model.TemplateSheetID;
            parameters[11].Value = model.HasAbnormal;
            parameters[12].Value = model.SheetNO;
            parameters[13].Value = model.SheetName;
            parameters[14].Value = model.AddressID;
            parameters[15].Value = model.SystemID;
            parameters[16].Value = model.DepartmentID;
            parameters[17].Value = model.Period;
            parameters[18].Value = model.PeriodUnit;
            parameters[19].Value = model.IsTemp;
            parameters[20].Value = model.PeriodInDays;
            parameters[21].Value = model.SaveTime == DateTime.MinValue ? SqlDateTime.Null : model.SaveTime;

            long id = (long)SQLHelper.ExecuteScalar((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters);
            return id;
        }
        /// <summary>
        /// 更新维护 表设备
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        private void UpdateMaintainSheetEquipments(long id, MaintainSheetInfo model, DbTransaction trans)
        {
            if (model.Equipments == null || model.Equipments.Count <= 0)
                return;

            //先删除，后插入
            StringBuilder strDel = new StringBuilder();
            strDel.Append("delete FM2E_MaintainSheetEquipment ");
            strDel.Append(" where SheetID=@SheetID ");
            SqlParameter[] paramDel = {
					new SqlParameter("@SheetID", SqlDbType.BigInt)};
            paramDel[0].Value = id;
            SQLHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, strDel.ToString(), paramDel);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_MaintainSheetEquipment(");
            strSql.Append("SheetID,EquipmentNO,EquipmentName,EquipmentModel,DetailLocation,NewStatus,IsNormal,IsExtra,Remark,EquipmentID,AddressID,AddressName)");
            strSql.Append(" values (");
            strSql.Append("@SheetID,@EquipmentNO,@EquipmentName,@EquipmentModel,@DetailLocation,@NewStatus,@IsNormal,@IsExtra,@Remark,@EquipmentID,@AddressID,@AddressName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.BigInt,8),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
					new SqlParameter("@EquipmentName", SqlDbType.NVarChar,20),
					new SqlParameter("@EquipmentModel", SqlDbType.NVarChar,20),
					new SqlParameter("@DetailLocation", SqlDbType.NVarChar,50),
					new SqlParameter("@NewStatus", SqlDbType.TinyInt,1),
					new SqlParameter("@IsNormal", SqlDbType.Bit,1),
                    new SqlParameter("@IsExtra",SqlDbType.Bit,1),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
                    new SqlParameter("@EquipmentID", SqlDbType.BigInt,8),
                    new SqlParameter("@AddressID", SqlDbType.BigInt,8),
                    new SqlParameter("@AddressName", SqlDbType.NVarChar,200)};
         
            foreach (MaintainSheetEquipmentInfo item in model.Equipments)
            {
                parameters[0].Value = id;
                parameters[1].Value = item.EquipmentNO;
                parameters[2].Value = item.EquipmentName;
                parameters[3].Value = item.EquipmentModel;
                parameters[4].Value = item.DetailLocation;
                parameters[5].Value = item.NewStatus;
                parameters[6].Value = item.IsNormal;
                parameters[7].Value = item.IsExtra;
                parameters[8].Value = item.Remark;
                parameters[9].Value = item.EquipmentID;
                parameters[10].Value = item.AddressID;
                parameters[11].Value = item.AddressName;
                SQLHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters);
            }

        }
        /// <summary>
        /// 修改维护表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        long IMaintain.UpdateMaintainSheet(MaintainSheetInfo model, global::System.Data.Common.DbTransaction trans)
        {
            long id = model.SheetID;

            UpdateMaintainSheet(model, trans);
            UpdateMaintainSheetEquipments(id, model, trans);

            return id;
        }
        /// <summary>
        /// 更新维护表主表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        private void UpdateMaintainSheet(MaintainSheetInfo model, DbTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_MaintainSheet set ");
            strSql.Append("Maintainer=@Maintainer,");
            strSql.Append("MaintainTime=@MaintainTime,");
            strSql.Append("MaintainType=@MaintainType,");
            strSql.Append("Result=@Result,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("LastExecuteTime=@LastExecuteTime,");
            strSql.Append("ConfirmResult=@ConfirmResult,");
            strSql.Append("Confirmer=@Confirmer,");
            strSql.Append("ConfirmRemark=@ConfirmRemark,");
            strSql.Append("ConfirmTime=@ConfirmTime,");
            strSql.Append("TemplateSheetID=@TemplateSheetID,");
            strSql.Append("HasAbnormal=@HasAbnormal,");
            strSql.Append("SheetNO=@SheetNO,");
            strSql.Append("SheetName=@SheetName,");
            strSql.Append("AddressID=@AddressID,");
            strSql.Append("SystemID=@SystemID,");
            strSql.Append("DepartmentID=@DepartmentID,");
            strSql.Append("Period=@Period,");
            strSql.Append("PeriodUnit=@PeriodUnit,");
            strSql.Append("IsTemp=@IsTemp,");
            strSql.Append("PeriodInDays=@PeriodInDays,");
            strSql.Append("SaveTime=@SaveTime");
            strSql.Append(" where SheetID=@SheetID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.BigInt,8),
					new SqlParameter("@Maintainer", SqlDbType.VarChar,20),
					new SqlParameter("@MaintainTime", SqlDbType.DateTime),
					new SqlParameter("@MaintainType", SqlDbType.TinyInt,1),
					new SqlParameter("@Result", SqlDbType.NVarChar,200),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@LastExecuteTime", SqlDbType.DateTime),
					new SqlParameter("@ConfirmResult", SqlDbType.TinyInt,1),
					new SqlParameter("@Confirmer", SqlDbType.VarChar,20),
					new SqlParameter("@ConfirmRemark", SqlDbType.NVarChar,100),
					new SqlParameter("@ConfirmTime", SqlDbType.DateTime),
					new SqlParameter("@TemplateSheetID", SqlDbType.BigInt,8),
					new SqlParameter("@HasAbnormal", SqlDbType.Bit,1),
					new SqlParameter("@SheetNO", SqlDbType.VarChar,20),
					new SqlParameter("@SheetName", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressID", SqlDbType.BigInt,8),
					new SqlParameter("@SystemID", SqlDbType.VarChar,2),
					new SqlParameter("@DepartmentID", SqlDbType.BigInt,8),
					new SqlParameter("@Period", SqlDbType.Int,4),
					new SqlParameter("@PeriodUnit", SqlDbType.TinyInt,1),
                    new SqlParameter("@IsTemp",SqlDbType.Bit,1),
                    new SqlParameter("@PeriodInDays",SqlDbType.Int,4),
                    new SqlParameter("@SaveTime",SqlDbType.DateTime)};
            parameters[0].Value = model.SheetID;
            parameters[1].Value = model.Maintainer;
            parameters[2].Value = model.MaintainTime == DateTime.MinValue ? SqlDateTime.Null : model.MaintainTime;
            parameters[3].Value = model.MaintainType;
            parameters[4].Value = model.Result;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.LastExecuteTime == DateTime.MinValue ? SqlDateTime.Null : model.LastExecuteTime;
            parameters[7].Value = model.ConfirmResult;
            parameters[8].Value = model.Confirmer;
            parameters[9].Value = model.ConfirmRemark;
            parameters[10].Value = model.ConfirmTime == DateTime.MinValue ? SqlDateTime.Null : model.ConfirmTime;
            parameters[11].Value = model.TemplateSheetID;
            parameters[12].Value = model.HasAbnormal;
            parameters[13].Value = model.SheetNO;
            parameters[14].Value = model.SheetName;
            parameters[15].Value = model.AddressID;
            parameters[16].Value = model.SystemID;
            parameters[17].Value = model.DepartmentID;
            parameters[18].Value = model.Period;
            parameters[19].Value = model.PeriodUnit;
            parameters[20].Value = model.IsTemp;
            parameters[21].Value = model.PeriodInDays;
            parameters[22].Value = model.SaveTime == DateTime.MinValue ? SqlDateTime.Null : model.SaveTime;

            SQLHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 核实
        /// </summary>
        /// <param name="id"></param>
        /// <param name="result"></param>
        /// <param name="confirmer"></param>
        /// <param name="confirmTime"></param>
        /// <param name="remark"></param>
        void IMaintain.DoConfirm(long id, MaintainConfirmResult result, string confirmer, DateTime confirmTime, string remark)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_MaintainSheet set ");
                strSql.Append("ConfirmResult=@ConfirmResult,");
                strSql.Append("Confirmer=@Confirmer,");
                strSql.Append("ConfirmRemark=@ConfirmRemark,");
                strSql.Append("ConfirmTime=@ConfirmTime");
                strSql.Append(" where SheetID=@SheetID ");
                SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.BigInt,8),
					new SqlParameter("@ConfirmResult", SqlDbType.TinyInt,1),
					new SqlParameter("@Confirmer", SqlDbType.VarChar,20),
					new SqlParameter("@ConfirmRemark", SqlDbType.NVarChar,100),
					new SqlParameter("@ConfirmTime", SqlDbType.DateTime)};
                parameters[0].Value = id;
                parameters[1].Value = (int)result;
                parameters[2].Value = confirmer;
                parameters[3].Value = remark;
                parameters[4].Value = confirmTime;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString,CommandType.Text,strSql.ToString(),parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("核实失败", ex);
            }
        }
        /// <summary>
        /// 删除维护表
        /// </summary>
        /// <param name="id"></param>
        void IMaintain.DeleteMaintainSheet(long id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_MaintainSheet ");
                strSql.Append(" where SheetID=@SheetID ");
                SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("删除维护表", ex);
            }
        }
        /// <summary>
        /// 获取维护表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MaintainSheetInfo IMaintain.GetMaintainSheet(long id)
        {
            MaintainSheetInfo item = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from FM2E_MaintainSheetView ");
                strSql.Append(" where SheetID=@SheetID ");
                SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                        item = GetMaintainSheetData(rd);
                }

                if (item != null)
                {
                    StringBuilder strSqlEquipment = new StringBuilder();
                    strSqlEquipment.Append("select * ");
                    strSqlEquipment.Append(" FROM FM2E_MaintainSheetEquipmentView ");
                    strSqlEquipment.Append(" where SheetID=@SheetID");
                    strSqlEquipment.Append(" order by AddressID asc");

                    parameters[0].Value = id;

                    ArrayList list=new ArrayList();
                    using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSqlEquipment.ToString(), parameters))
                    {
                        while (rd.Read())
                            list.Add(GetMaintainSheetEquipmentData(rd));
                    }
                    item.Equipments = list;
                    
                }

            }
            catch (Exception ex)
            {
                item = null;
                throw new DALException("获取维护表失败", ex);
            }

            return item;

        }
        /// <summary>
        /// 获取维护表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MaintainSheetInfo IMaintain.GetMaintainSheetByEquipmentName(long id)
        {
            MaintainSheetInfo item = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from FM2E_MaintainSheetView ");
                strSql.Append(" where SheetID=@SheetID ");
                SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                        item = GetMaintainSheetData(rd);
                }

                if (item != null)
                {
                    StringBuilder strSqlEquipment = new StringBuilder();
                    strSqlEquipment.Append("select * ");
                    strSqlEquipment.Append(" FROM FM2E_MaintainSheetEquipmentView ");
                    strSqlEquipment.Append(" where SheetID=@SheetID");
                    strSqlEquipment.Append(" order by EquipmentName asc");

                    parameters[0].Value = id;

                    ArrayList list = new ArrayList();
                    using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSqlEquipment.ToString(), parameters))
                    {
                        while (rd.Read())
                            list.Add(GetMaintainSheetEquipmentData(rd));
                    }
                    item.Equipments = list;

                }

            }
            catch (Exception ex)
            {
                item = null;
                throw new DALException("获取维护表失败", ex);
            }

            return item;

        }
        /// <summary>
        /// 生成维护表查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam IMaintain.GetSearchTerm(MaintainSheetSearchInfo term)
        {

            string sqlSearch = " where 1=1 ";

            if (!string.IsNullOrEmpty(term.SystemID))
                sqlSearch += " and SystemID='" + term.SystemID + "'";

            if (!string.IsNullOrEmpty(term.SystemName))
                sqlSearch += " and SystemName like '%" + term.SystemName + "%'";

            if (term.AddressID != 0)
                sqlSearch += " and AddressID=" + term.AddressID;

            if (!string.IsNullOrEmpty(term.AddressName))
                sqlSearch += " and AddressName like '%" + term.AddressName + "%'";

            if (!string.IsNullOrEmpty(term.AddressCode))
                sqlSearch += " and AddressCode like '" + term.AddressCode + "%'";

            if (term.ConfirmResult != MaintainConfirmResult.Unknown)
            {
                sqlSearch += " and ConfirmResult=" + (int)term.ConfirmResult;
            }

            if (!string.IsNullOrEmpty(term.Confirmer))
                sqlSearch += " and Confirmer='" + term.Confirmer + "'";

            if (!string.IsNullOrEmpty(term.ConfirmerName))
                sqlSearch += " and ConfirmerName like '%" + term.ConfirmerName + "%'";

            if (DateTime.Compare(term.ConfirmTimeFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(term.ConfirmTimeFrom, sqlMinDate) < 0)
                    term.ConfirmTimeFrom = sqlMinDate;

                sqlSearch += " and ConfirmTime>='" + term.ConfirmTimeFrom.ToString("yyyy-MM-dd") + " 00:00:00'";
            }

            if (DateTime.Compare(term.ConfirmTimeTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(term.ConfirmTimeTo, sqlMaxDate) > 0)
                    term.ConfirmTimeTo = sqlMaxDate;

                sqlSearch += " and ConfirmTime<='" + term.ConfirmTimeTo.ToString("yyyy-MM-dd") + " 23:59:59'";
            }

            if (!string.IsNullOrEmpty(term.SheetName))
                sqlSearch += " and SheetName like '%" + term.SheetName + "%'";

            if (!string.IsNullOrEmpty(term.SheetNO))
                sqlSearch += " and SheetNO like '%" + term.SheetNO + "%'";

            if (term.DepartmentID != 0)
            {
                sqlSearch += " and DepartmentID=" + term.DepartmentID;
            }

            if (!string.IsNullOrEmpty(term.Maintainer))
                sqlSearch += " and Maintainer='" + term.Maintainer + "'";

            if (!string.IsNullOrEmpty(term.MaintainerName))
                sqlSearch += " and MaintainerName like '%" + term.MaintainerName + "%'";

            if (DateTime.Compare(term.MaintainTimeFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(term.MaintainTimeFrom, sqlMinDate) < 0)
                    term.MaintainTimeFrom = sqlMinDate;

                sqlSearch += " and MaintainTime>='" + term.MaintainTimeFrom.ToString("yyyy-MM-dd") + " 00:00:00'";
            }

            if (DateTime.Compare(term.MaintainTimeTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(term.MaintainTimeTo, sqlMaxDate) > 0)
                    term.MaintainTimeTo = sqlMaxDate;

                sqlSearch += " and MaintainTime<='" + term.MaintainTimeTo.ToString("yyyy-MM-dd") + " 23:59:59'";
            }

            if (term.MaintainType != MaintainType.Unknown)
            {
                sqlSearch += " and MaintainType=" + (int)term.MaintainType;
            }

            if (term.HasAbnormal.HasValue)
            {
                sqlSearch += " and HasAbnormal=" + (term.HasAbnormal.Value ? "1" : "0");
            }

            if (term.IsTemp.HasValue)
            {
                sqlSearch += " and IsTemp=" + (term.IsTemp.Value ? "1" : "0");
            }

            if (term.PeriodUnit != MaintainIntervalUnit.Unknown)
            {
                sqlSearch += " and PeriodUnit=" + (int)term.PeriodUnit;
            }

            QueryParam qp = new QueryParam();
            qp.Where = sqlSearch;
            qp.TableName = "FM2E_MaintainSheetView";
            qp.ReturnFields = "*";
            qp.OrderBy = "order by MaintainTime desc";
            return qp;
        }
        /// <summary>
        /// 查询维护表
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList IMaintain.SearchMaintainSheet(QueryParam qp, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetMaintainSheetData, qp, out recordCount);
            }
            catch (Exception ex)
            {
                throw new DALException("查询维护表失败", ex);
            }
        }
        /// <summary>
        /// 获取设备维护记录，不分页
        /// </summary>
        /// <param name="equipmentNO">设备条形码</param>
        /// <param name="type">维护类型</param>
        /// <returns></returns>
        IList IMaintain.SearchDeviceMaintainRecord(string equipmentNO, MaintainType type)
        {
            StringBuilder strSqlEquipment = new StringBuilder();
            strSqlEquipment.Append("select * ");
            strSqlEquipment.Append(" FROM FM2E_MaintainSheetEquipmentView ");
            strSqlEquipment.Append(" where EquipmentNO=@EquipmentNO");
            strSqlEquipment.Append(" and MaintainType=@MaintainType");

            SqlParameter[] parameters = {
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
                    new SqlParameter("@MaintainType", SqlDbType.TinyInt)};
            parameters[0].Value = equipmentNO;
            parameters[1].Value = type;

            ArrayList list = new ArrayList();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSqlEquipment.ToString(), parameters))
            {
                while (rd.Read())
                    list.Add(GetMaintainSheetEquipmentData(rd));
            }
            return list;
        }

        /// <summary>
        /// 生成维护设备搜索条件
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        QueryParam IMaintain.GetSearchTerm(MaintainSheetEquipmentSearchInfo info)
        {
            QueryParam qp = new QueryParam();

            string sqlSearch = GenerateSqlWhereString(info);

            qp.Where = sqlSearch;
            qp.TableName = "FM2E_MaintainSheetEquipmentView";
            qp.ReturnFields = "*";
            qp.OrderBy = "Order by ID desc";

            return qp;
        }
        /// <summary>
        /// 生成维护设备where条件 
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        private string GenerateSqlWhereString(MaintainSheetEquipmentSearchInfo term)
        {
            string sqlSearch = "where 1=1 ";

            if (term.MaintainType != MaintainType.Unknown)
            {
                sqlSearch += " and MaintainType=" + (int)term.MaintainType;
            }

            if (!string.IsNullOrEmpty(term.EquipmentNO))
                sqlSearch += " and EquipmentNO = '" + term.EquipmentNO + "'";

            return sqlSearch;
        }
        /// <summary>
        /// 获取设备维护记录，分页
        /// </summary>
        /// <param name="equipmentNO">设备条形码</param>
        /// <param name="type">维护类型</param>
        /// <returns></returns>
        IList IMaintain.SearchDeviceMaintainRecord(QueryParam qp, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetMaintainSheetEquipmentData, qp, out recordCount);
            }
            catch (Exception ex)
            {
                throw new DALException("查询维护设备记录失败"+ex.Message, ex);
            }
        }
        #endregion
    }
}
