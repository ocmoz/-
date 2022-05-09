using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Examine;
using FM2E.Model.Examine;
using System.Collections;
using FM2E.Model.Utils;
using System.Data;
using FM2E.Model.Exceptions;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;
using System.Data.Common;
using System.Data.SqlTypes;

namespace FM2E.SQLServerDAL.Examine
{
    /// <summary>
    /// 考核数据访问类
    /// </summary>
    public class Examine : IExamine
    {
        #region GetData
        /// <summary>
        /// 获取考核项实体数据
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private ExamineItemInfo GetExamineItemData(IDataReader rd)
        {
            ExamineItemInfo item = new ExamineItemInfo();

            if (!Convert.IsDBNull(rd["Content"]))
                item.Content = Convert.ToString(rd["Content"]);

            if (!Convert.IsDBNull(rd["ExamineType"]))
                item.ExamineType = (ExamineType)Convert.ToInt32(rd["ExamineType"]);

            if (!Convert.IsDBNull(rd["ExamItemID"]))
                item.ExamItemID = Convert.ToInt64(rd["ExamItemID"]);

            if (!Convert.IsDBNull(rd["ItemName"]))
                item.ItemName = Convert.ToString(rd["ItemName"]);

            if (!Convert.IsDBNull(rd["ParentItem"]))
                item.ParentItem = Convert.ToInt64(rd["ParentItem"]);

            if (!Convert.IsDBNull(rd["Score"]))
                item.Score = Convert.ToSingle(rd["Score"]);

            if (!Convert.IsDBNull(rd["Standard"]))
                item.Standard = Convert.ToString(rd["Standard"]);

            if (!Convert.IsDBNull(rd["Threshold"]))
                item.Threshold = Convert.ToSingle(rd["Threshold"]);

            if (!Convert.IsDBNull(rd["ChildCount"]))
                item.ChildCount = Convert.ToInt32(rd["ChildCount"]);

            if (!Convert.IsDBNull(rd["Level"]))
                item.Level = Convert.ToInt32(rd["Level"]);

            return item;
        }

        /// <summary>
        /// 获取考核结果数据实体
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private ExamineResultInfo GetExamineResultData(IDataReader rd)
        {
            ExamineResultInfo item = new ExamineResultInfo();

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["CompanyName"]))
                item.CompanyName = Convert.ToString(rd["CompanyName"]);

            if (!Convert.IsDBNull(rd["DailyExamineRatio"]))
                item.DailyExamineRatio = Convert.ToSingle(rd["DailyExamineRatio"]);

            if (!Convert.IsDBNull(rd["DailyExamineResult"]))
                item.DailyExamineResult = Convert.ToSingle(rd["DailyExamineResult"]);

            if (!Convert.IsDBNull(rd["ExamineConfirmer"]))
                item.ExamineConfirmer = Convert.ToString(rd["ExamineConfirmer"]);

            if (!Convert.IsDBNull(rd["ExamineConfirmRemark"]))
                item.ExamineConfirmRemark = Convert.ToString(rd["ExamineConfirmRemark"]);

            if (!Convert.IsDBNull(rd["ExamineConfirmResult"]))
                item.ExamineConfirmResult = (ExamineConfirmResult)Convert.ToInt32(rd["ExamineConfirmResult"]);

            if (!Convert.IsDBNull(rd["ExamineConfirmTime"]))
                item.ExamineConfirmTime = Convert.ToDateTime(rd["ExamineConfirmTime"]);

            if (!Convert.IsDBNull(rd["Examiner"]))
                item.Examiner = Convert.ToString(rd["Examiner"]);

            if (!Convert.IsDBNull(rd["ExaminerConfirmerName"]))
                item.ExaminerConfirmerName = Convert.ToString(rd["ExaminerConfirmerName"]);

            if (!Convert.IsDBNull(rd["ExaminerName"]))
                item.ExaminerName = Convert.ToString(rd["ExaminerName"]);

            if (!Convert.IsDBNull(rd["ExamineTarget"]))
                item.ExamineTarget = Convert.ToInt64(rd["ExamineTarget"]);

            if (!Convert.IsDBNull(rd["ExamineTargetName"]))
                item.ExamineTargetName = Convert.ToString(rd["ExamineTargetName"]);

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["SaveTime"]))
                item.SaveTime = Convert.ToDateTime(rd["SaveTime"]);

            if (!Convert.IsDBNull(rd["Season"]))
                item.Season = (ExamineSeason)Convert.ToInt32(rd["Season"]);

            if (!Convert.IsDBNull(rd["SeasonExamineRatio"]))
                item.SeasonExamineRatio = Convert.ToSingle(rd["SeasonExamineRatio"]);

            if (!Convert.IsDBNull(rd["SeasonExamineResult"]))
                item.SeasonExamineResult = Convert.ToSingle(rd["SeasonExamineResult"]);

            if (!Convert.IsDBNull(rd["TargetConfirmer"]))
                item.TargetConfirmer = Convert.ToString(rd["TargetConfirmer"]);

            if (!Convert.IsDBNull(rd["TargetConfirmerName"]))
                item.TargetConfirmerName = Convert.ToString(rd["TargetConfirmerName"]);

            if (!Convert.IsDBNull(rd["TargetConfirmRemark"]))
                item.TargetConfirmRemark = Convert.ToString(rd["TargetConfirmRemark"]);

            if (!Convert.IsDBNull(rd["TargetConfirmResult"]))
                item.TargetConfirmResult = (ExamineConfirmResult)Convert.ToInt32(rd["TargetConfirmResult"]);

            if (!Convert.IsDBNull(rd["TargetConfirmTime"]))
                item.TargetConfirmTime = Convert.ToDateTime(rd["TargetConfirmTime"]);

            if (!Convert.IsDBNull(rd["UpdateTime"]))
                item.UpdateTime = Convert.ToDateTime(rd["UpdateTime"]);

            if (!Convert.IsDBNull(rd["SheetName"]))
                item.SheetName = Convert.ToString(rd["SheetName"]);

            if (!Convert.IsDBNull(rd["SheetNO"]))
                item.SheetNO = Convert.ToString(rd["SheetNO"]);

            if (!Convert.IsDBNull(rd["Year"]))
                item.Year = Convert.ToInt32(rd["Year"]);

            return item;
        }

        /// <summary>
        /// 获取考核表实体数据
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private ExamineInfo GetExamineData(IDataReader rd)
        {
            ExamineInfo item = new ExamineInfo();

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["CompanyName"]))
                item.CompanyName = Convert.ToString(rd["CompanyName"]);

            if (!Convert.IsDBNull(rd["ExamineConfirmer"]))
                item.ExamineConfirmer = Convert.ToString(rd["ExamineConfirmer"]);

            if (!Convert.IsDBNull(rd["ExamineConfirmerName"]))
                item.ExamineConfirmerName = Convert.ToString(rd["ExamineConfirmerName"]);

            if (!Convert.IsDBNull(rd["ExamineConfirmRemark"]))
                item.ExamineConfirmRemark = Convert.ToString(rd["ExamineConfirmRemark"]);

            if (!Convert.IsDBNull(rd["ExamineConfirmResult"]))
                item.ExamineConfirmResult = (ExamineConfirmResult)Convert.ToInt32(rd["ExamineConfirmResult"]);

            if (!Convert.IsDBNull(rd["Examiner"]))
                item.Examiner = Convert.ToString(rd["Examiner"]);

            if (!Convert.IsDBNull(rd["ExaminerConfirmDate"]))
                item.ExaminerConfirmDate = Convert.ToDateTime(rd["ExaminerConfirmDate"]);

            if (!Convert.IsDBNull(rd["ExaminerName"]))
                item.ExaminerName = Convert.ToString(rd["ExaminerName"]);
           
            if (!Convert.IsDBNull(rd["ExamineTarget"]))
                item.ExamineTarget = Convert.ToInt64(rd["ExamineTarget"]);

            if (!Convert.IsDBNull(rd["ExamineType"]))
                item.ExamineType = (ExamineType)Convert.ToInt32(rd["ExamineType"]);

            if (!Convert.IsDBNull(rd["ExamSheetID"]))
                item.ExamSheetID = Convert.ToInt64(rd["ExamSheetID"]);

            if (!Convert.IsDBNull(rd["ExamSheetName"]))
                item.ExamSheetName = Convert.ToString(rd["ExamSheetName"]);

            if (!Convert.IsDBNull(rd["ExamSheetNO"]))
                item.ExamSheetNO = Convert.ToString(rd["ExamSheetNO"]);

            if (!Convert.IsDBNull(rd["SaveTime"]))
                item.SaveTime = Convert.ToDateTime(rd["SaveTime"]);

            if (!Convert.IsDBNull(rd["Score"]))
                item.Score = Convert.ToSingle(rd["Score"]);

            if (!Convert.IsDBNull(rd["TargetConfirmDate"]))
                item.TargetConfirmDate = Convert.ToDateTime(rd["TargetConfirmDate"]);

            if (!Convert.IsDBNull(rd["TargetConfirmer"]))
                item.TargetConfirmer = Convert.ToString(rd["TargetConfirmer"]);

            if (!Convert.IsDBNull(rd["TargetConfirmerName"]))
                item.TargetConfirmerName = Convert.ToString(rd["TargetConfirmerName"]);

            if (!Convert.IsDBNull(rd["TargetConfirmRemark"]))
                item.TargetConfirmRemark = Convert.ToString(rd["TargetConfirmRemark"]);

            if (!Convert.IsDBNull(rd["TargetConfirmResult"]))
                item.TargetConfirmResult = (ExamineConfirmResult)Convert.ToInt32(rd["TargetConfirmResult"]);

            if (!Convert.IsDBNull(rd["UpdateTime"]))
                item.UpdateTime = Convert.ToDateTime(rd["UpdateTime"]);


            if (!Convert.IsDBNull(rd["Status"]))
                item.Status = (ExamineSheetStatus)Convert.ToInt32(rd["Status"]);

            if (!Convert.IsDBNull(rd["ExamineTargetName"]))
                item.ExamineTargetName = Convert.ToString(rd["ExamineTargetName"]);
        
            return item;
        }

        /// <summary>
        /// 获取考核表明细实体数据
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private ExamineDetailInfo  GetExamineDetailData(IDataReader rd)
        {
            ExamineDetailInfo item = new ExamineDetailInfo();

            if (!Convert.IsDBNull(rd["Confirmer"]))
                item.Confirmer = Convert.ToString(rd["Confirmer"]);

            if (!Convert.IsDBNull(rd["ConfirmerName"]))
                item.ConfirmerName = Convert.ToString(rd["ConfirmerName"]);

            if (!Convert.IsDBNull(rd["ConfirmRemark"]))
                item.ConfirmRemark = Convert.ToString(rd["ConfirmRemark"]);

            if (!Convert.IsDBNull(rd["ConfirmResult"]))
                item.ConfirmResult = (ExamineConfirmResult)Convert.ToInt32(rd["ConfirmResult"]);

            if (!Convert.IsDBNull(rd["ConfirmTime"]))
                item.ConfirmTime = Convert.ToDateTime(rd["ConfirmTime"]);

            if (!Convert.IsDBNull(rd["Content"]))
                item.Content = Convert.ToString(rd["Content"]);

            if (!Convert.IsDBNull(rd["Deduct"]))
                item.Deduct = Convert.ToSingle(rd["Deduct"]);

            if (!Convert.IsDBNull(rd["DeductReason"]))
                item.DeductReason = Convert.ToString(rd["DeductReason"]);

            if (!Convert.IsDBNull(rd["ExamineDate"]))
                item.ExamineDate = Convert.ToDateTime(rd["ExamineDate"]);

            if (!Convert.IsDBNull(rd["Examiner"]))
                item.Examiner = Convert.ToString(rd["Examiner"]);

            if (!Convert.IsDBNull(rd["ExaminerName"]))
                item.ExaminerName = Convert.ToString(rd["ExaminerName"]);

            if (!Convert.IsDBNull(rd["ExamSheetID"]))
                item.ExamSheetID = Convert.ToInt64(rd["ExamSheetID"]);

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["ItemName"]))
                item.ItemName = Convert.ToString(rd["ItemName"]);

            if (!Convert.IsDBNull(rd["ParentItem"]))
                item.ParentItem = Convert.ToInt64(rd["ParentItem"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["Score"]))
                item.Score = Convert.ToSingle(rd["Score"]);

            if (!Convert.IsDBNull(rd["Standard"]))
                item.Standard = Convert.ToString(rd["Standard"]);

            if (!Convert.IsDBNull(rd["Threshold"]))
                item.Threshold = Convert.ToSingle(rd["Threshold"]);

            if (!Convert.IsDBNull(rd["UpdateTime"]))
                item.UpdateTime = Convert.ToDateTime(rd["UpdateTime"]);

            if (!Convert.IsDBNull(rd["ExamItemID"]))
                item.ExamItemID = Convert.ToInt64(rd["ExamItemID"]);

            if (!Convert.IsDBNull(rd["ChildCount"]))
                item.ChildCount = Convert.ToInt32(rd["ChildCount"]);

            if (!Convert.IsDBNull(rd["Level"]))
                item.Level = Convert.ToInt32(rd["Level"]);

            if (!Convert.IsDBNull(rd["CanAddChild"]))
                item.CanAddChild = Convert.ToBoolean(rd["CanAddChild"]);

            return item;
        }
        #endregion
        #region IExamine 成员
        #region 考核项
        /// <summary>
        /// 添加考核项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long IExamine.AddExamineItem(ExamineItemInfo model,DbTransaction trans)
        {
            long id = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_ExamineItem(");
            strSql.Append("ExamineType,ItemName,ParentItem,Score,Threshold,Content,Standard,ChildCount,Level)");
            strSql.Append(" values (");
            strSql.Append("@ExamineType,@ItemName,@ParentItem,@Score,@Threshold,@Content,@Standard,@ChildCount,@Level)");
            strSql.Append(";select cast(@@IDENTITY as bigint);");
            SqlParameter[] parameters = {
				new SqlParameter("@ExamineType", SqlDbType.TinyInt,1),
				new SqlParameter("@ItemName", SqlDbType.NVarChar,50),
				new SqlParameter("@ParentItem", SqlDbType.BigInt,8),
				new SqlParameter("@Score", SqlDbType.Float,8),
				new SqlParameter("@Threshold", SqlDbType.Float,8),
				new SqlParameter("@Content", SqlDbType.NVarChar,50),
				new SqlParameter("@Standard", SqlDbType.NVarChar,400),
                new SqlParameter("@ChildCount", SqlDbType.Int,4),
                new SqlParameter("@Level", SqlDbType.Int,4)};
            parameters[0].Value = model.ExamineType;
            parameters[1].Value = model.ItemName;
            parameters[2].Value = model.ParentItem;
            parameters[3].Value = model.Score;
            parameters[4].Value = model.Threshold;
            parameters[5].Value = model.Content;
            parameters[6].Value = model.Standard;
            parameters[7].Value = model.ChildCount;
            parameters[8].Value = model.Level;

            id = (long)SQLHelper.ExecuteScalar((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters);

            StringBuilder strUpdate = new StringBuilder();
            strUpdate.Append("update FM2E_ExamineItem ");
            strUpdate.Append(" set ChildCount=ChildCount+1 ");
            strUpdate.AppendFormat(" where ExamItemID={0}", model.ParentItem);

            SQLHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, strUpdate.ToString(), null);

            return id;
        }
        /// <summary>
        /// 修改考核项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long IExamine.UpdateExamineItem(ExamineItemInfo model,DbTransaction trans)
        {
            long id = model.ExamItemID;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_ExamineItem set ");
            strSql.Append("ExamineType=@ExamineType,");
            strSql.Append("ItemName=@ItemName,");
            strSql.Append("ParentItem=@ParentItem,");
            strSql.Append("Score=@Score,");
            strSql.Append("Threshold=@Threshold,");
            strSql.Append("Content=@Content,");
            strSql.Append("Standard=@Standard,");
            strSql.Append("ChildCount=@ChildCount,");
            strSql.Append("Level=@Level");
            strSql.Append(" where ExamItemID=@ExamItemID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ExamItemID", SqlDbType.BigInt,8),
				new SqlParameter("@ExamineType", SqlDbType.TinyInt,1),
				new SqlParameter("@ItemName", SqlDbType.NVarChar,50),
				new SqlParameter("@ParentItem", SqlDbType.BigInt,8),
				new SqlParameter("@Score", SqlDbType.Float,8),
				new SqlParameter("@Threshold", SqlDbType.Float,8),
				new SqlParameter("@Content", SqlDbType.NVarChar,50),
				new SqlParameter("@Standard", SqlDbType.NVarChar,400),
                new SqlParameter("@ChildCount", SqlDbType.Int,4),
                new SqlParameter("@Level", SqlDbType.Int,4) };
            parameters[0].Value = model.ExamItemID;
            parameters[1].Value = model.ExamineType;
            parameters[2].Value = model.ItemName;
            parameters[3].Value = model.ParentItem;
            parameters[4].Value = model.Score;
            parameters[5].Value = model.Threshold;
            parameters[6].Value = model.Content;
            parameters[7].Value = model.Standard;
            parameters[8].Value = model.ChildCount;
            parameters[9].Value = model.Level;

            SQLHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters);
        
            return id;
        }
        /// <summary>
        /// 删除考核项
        /// </summary>
        /// <param name="id"></param>
        void IExamine.DeleteExamineItem(long id,DbTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_ExamineItem ");
            strSql.Append(" where ExamItemID=@ExamItemID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ExamItemID", SqlDbType.BigInt)};

            StringBuilder strSelect = new StringBuilder();
            strSelect.Append("select * from FM2E_ExamineItem ");
            strSelect.Append(" where ParentItem=@ParentItem ");
            SqlParameter[] paramSelect = {
				new SqlParameter("@ParentItem", SqlDbType.BigInt)};

            StringBuilder strChildCount = new StringBuilder();
            strChildCount.Append("Update FM2E_ExamineItem ");
            strChildCount.Append(" set ChildCount=ChildCount-1 ");
            strChildCount.Append(" where ExamItemID=@ExamItemID ");
            SqlParameter[] paramChildCount = {
				new SqlParameter("@ExamItemID", SqlDbType.BigInt)};

            ExamineItemInfo item = GetExamineItem(id);

            if (item != null)
            {
                //更新父结点的ChildCount
                paramChildCount[0].Value = item.ParentItem;
                SQLHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, strChildCount.ToString(), paramChildCount);
            }

            Queue<long> queue = new Queue<long>();
            queue.Enqueue(id);

            while (queue.Count > 0)
            {
                long itemID = queue.Dequeue();
                parameters[0].Value = itemID;
                SQLHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters);

                paramSelect[0].Value = itemID;
                using (SqlDataReader rd = SQLHelper.ExecuteReader((SqlTransaction)trans, CommandType.Text, strSelect.ToString(), paramSelect))
                {
                    while (rd.Read())
                    {
                        long tmp = 0;
                        if (!Convert.IsDBNull(rd["ExamItemID"]))
                        {
                            tmp = Convert.ToInt64(rd["ExamItemID"]);
                            queue.Enqueue(tmp);
                        }
                    }
                }
            }
            
        }
        /// <summary>
        /// 获取某考核项的所有子考核项（树结构）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList IExamine.GetSubExamineItems(long id)
        {
            ArrayList list = new ArrayList();
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                IList tmpList = GetChildExamineItems(id,trans);
                Stack<ExamineItemInfo> stack = new Stack<ExamineItemInfo>();

                if (tmpList != null && tmpList.Count > 0)
                {
                    foreach (ExamineItemInfo item in tmpList)
                    {
                        stack.Push(item);

                        while (stack.Count > 0)
                        {
                            ExamineItemInfo it = stack.Pop();
                            list.Add(it);

                            float scoreOfChild = 0;
                            IList childList = GetChildExamineItems(it.ExamItemID,trans);
                            foreach (ExamineItemInfo child in childList)
                            {
                                scoreOfChild += child.Score;
                                stack.Push(child);
                            }
                            it.ScoreOfChild = scoreOfChild;
                        }
                    }
                }

                //事务提交
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                list.Clear();
                throw new DALException("获取子考核项失败", ex);
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
            return list;
        }
        /// <summary>
        /// 获取某个考核项的直接子结点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList GetChildExamineItems(long id,DbTransaction trans)
        {
            ArrayList list = new ArrayList();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from FM2E_ExamineItem ");
            strSql.Append(" where ParentItem=@ParentItem ");
            strSql.Append(" order by ExamItemID desc");
            SqlParameter[] parameters = {
					new SqlParameter("@ParentItem", SqlDbType.BigInt)};
            parameters[0].Value = id;

            using (SqlDataReader rd = SQLHelper.ExecuteReader((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                    list.Add(GetExamineItemData(rd));
            }

            return list;
        }
        private IList GetChildExamineItems(long id)
        {
            ArrayList list = new ArrayList();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from FM2E_ExamineItem ");
            strSql.Append(" where ParentItem=@ParentItem ");
            strSql.Append(" order by ExamItemID desc");
            SqlParameter[] parameters = {
					new SqlParameter("@ParentItem", SqlDbType.BigInt)};
            parameters[0].Value = id;

            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                    list.Add(GetExamineItemData(rd));
            }

            return list;
        }
        /// <summary>
        /// 获取某个特定的考核项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExamineItemInfo GetExamineItem(long id)
        {
            ExamineItemInfo item = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from FM2E_ExamineItem");
                strSql.Append(" where ExamItemID=@ExamItemID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ExamItemID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                    {
                        item = GetExamineItemData(rd);
                    }
                }

                IList childList = GetChildExamineItems(id);

                foreach (ExamineItemInfo it in childList)
                {
                    item.ScoreOfChild += it.Score;
                }
            }
            catch (Exception ex)
            {
                item = null;
                throw new DALException("获取考核项失败:"+ex.Message, ex);
            }
            return item;
        }
        #endregion

        #region 考核表
        /// <summary>
        /// 添加考核表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        long IExamine.AddExamine(ExamineInfo model, global::System.Data.Common.DbTransaction trans)
        {
            long id = AddExamine(model, trans);
            UpdateExamineDetail(id, model, trans);
            return id;
        }
        /// <summary>
        /// 添加考核表主表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        private long AddExamine(ExamineInfo model, DbTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_Examine(");
            strSql.Append("ExamineConfirmer,ExamineConfirmResult,ExamineConfirmRemark,ExaminerConfirmDate,TargetConfirmer,TargetConfirmResult,TargetConfirmRemark,TargetConfirmDate,Status,UpdateTime,ExamSheetNO,ExamSheetName,CompanyID,Examiner,ExamineTarget,Score,SaveTime,ExamineType)");
            strSql.Append(" values (");
            strSql.Append("@ExamineConfirmer,@ExamineConfirmResult,@ExamineConfirmRemark,@ExaminerConfirmDate,@TargetConfirmer,@TargetConfirmResult,@TargetConfirmRemark,@TargetConfirmDate,@Status,@UpdateTime,@ExamSheetNO,@ExamSheetName,@CompanyID,@Examiner,@ExamineTarget,@Score,@SaveTime,@ExamineType)");
            strSql.Append(";select cast(@@IDENTITY as bigint);");
            SqlParameter[] parameters = {
					new SqlParameter("@ExamineConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@ExamineConfirmResult", SqlDbType.TinyInt,1),
					new SqlParameter("@ExamineConfirmRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@ExaminerConfirmDate", SqlDbType.DateTime),
					new SqlParameter("@TargetConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@TargetConfirmResult", SqlDbType.TinyInt,1),
					new SqlParameter("@TargetConfirmRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@TargetConfirmDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ExamSheetNO", SqlDbType.VarChar,20),
					new SqlParameter("@ExamSheetName", SqlDbType.NVarChar,50),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Examiner", SqlDbType.VarChar,20),
					new SqlParameter("@ExamineTarget", SqlDbType.BigInt,8),
					new SqlParameter("@Score", SqlDbType.Float,8),
					new SqlParameter("@SaveTime", SqlDbType.DateTime),
					new SqlParameter("@ExamineType", SqlDbType.TinyInt,1)};
            parameters[0].Value = model.ExamineConfirmer == null ? SqlString.Null : model.ExamineConfirmer;
            parameters[1].Value = model.ExamineConfirmResult;
            parameters[2].Value = model.ExamineConfirmRemark == null ? SqlString.Null : model.ExamineConfirmRemark;
            parameters[3].Value = model.ExaminerConfirmDate == DateTime.MinValue ? SqlDateTime.Null : model.ExaminerConfirmDate;
            parameters[4].Value = model.TargetConfirmer == null ? SqlString.Null : model.TargetConfirmer;
            parameters[5].Value = model.TargetConfirmResult;
            parameters[6].Value = model.TargetConfirmRemark == null ? SqlString.Null : model.TargetConfirmRemark;
            parameters[7].Value = model.TargetConfirmDate == DateTime.MinValue ? SqlDateTime.Null : model.TargetConfirmDate;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.UpdateTime == DateTime.MinValue ? SqlDateTime.Null : model.UpdateTime;
            parameters[10].Value = model.ExamSheetNO == null ? SqlString.Null : model.ExamSheetNO;
            parameters[11].Value = model.ExamSheetName == null ? SqlString.Null : model.ExamSheetName;
            parameters[12].Value = model.CompanyID == null ? SqlString.Null : model.CompanyID;
            parameters[13].Value = model.Examiner == null ? SqlString.Null : model.Examiner;
            parameters[14].Value = model.ExamineTarget;
            parameters[15].Value = model.Score;
            parameters[16].Value = model.SaveTime == DateTime.MinValue ? SqlDateTime.Null : model.SaveTime;
            parameters[17].Value = model.ExamineType;

            long id = (long)SQLHelper.ExecuteScalar((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters);
            return id;
        }
        /// <summary>
        /// 更新考核明细项,还需要进行完善
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        private void UpdateExamineDetail(long id, ExamineInfo model, DbTransaction trans)
        {
            if (model.ExamineItems == null || model.ExamineItems.Count <= 0)
                return;

            //先删除，后插入
            StringBuilder strDel = new StringBuilder();
            strDel.Append("delete FM2E_ExamineDetail ");
            strDel.Append(" where ExamSheetID=@ExamSheetID ");
            SqlParameter[] paramDel = {
					new SqlParameter("@ExamSheetID", SqlDbType.BigInt)};
            paramDel[0].Value = id;

            SQLHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, strDel.ToString(), paramDel);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_ExamineDetail(");
            strSql.Append("Remark,DeductReason,Deduct,ExamineDate,Confirmer,ConfirmResult,ConfirmRemark,ConfirmTime,UpdateTime,ExamSheetID,Examiner,ItemName,ParentItem,Score,Threshold,Content,Standard,ExamItemID,ChildCount,Level,CanAddChild)");
            strSql.Append(" values (");
            strSql.Append("@Remark,@DeductReason,@Deduct,@ExamineDate,@Confirmer,@ConfirmResult,@ConfirmRemark,@ConfirmTime,@UpdateTime,@ExamSheetID,@Examiner,@ItemName,@ParentItem,@Score,@Threshold,@Content,@Standard,@ExamItemID,@ChildCount,@Level,@CanAddChild);");
            SqlParameter[] parameters = {
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
					new SqlParameter("@DeductReason", SqlDbType.NVarChar,30),
					new SqlParameter("@Deduct", SqlDbType.Float,8),
					new SqlParameter("@ExamineDate", SqlDbType.DateTime),
					new SqlParameter("@Confirmer", SqlDbType.VarChar,20),
					new SqlParameter("@ConfirmResult", SqlDbType.TinyInt,1),
					new SqlParameter("@ConfirmRemark", SqlDbType.NVarChar,5),
					new SqlParameter("@ConfirmTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ExamSheetID", SqlDbType.BigInt,8),
					new SqlParameter("@Examiner", SqlDbType.VarChar,20),
					new SqlParameter("@ItemName", SqlDbType.NVarChar,50),
					new SqlParameter("@ParentItem", SqlDbType.BigInt,8),
					new SqlParameter("@Score", SqlDbType.Float,8),
					new SqlParameter("@Threshold", SqlDbType.Float,8),
					new SqlParameter("@Content", SqlDbType.NVarChar,200),
					new SqlParameter("@Standard", SqlDbType.NVarChar,400),
                    new SqlParameter("@ExamItemID", SqlDbType.BigInt,8),
					new SqlParameter("@ChildCount", SqlDbType.Int,4),
					new SqlParameter("@Level", SqlDbType.Int,4),
					new SqlParameter("@CanAddChild", SqlDbType.Bit,1)};

            foreach (ExamineDetailInfo item in model.ExamineItems)
            {
                parameters[0].Value = item.Remark == null ? SqlString.Null : item.Remark;
                parameters[1].Value = item.DeductReason == null ? SqlString.Null : item.DeductReason;
                parameters[2].Value = item.Deduct;
                parameters[3].Value = item.ExamineDate == DateTime.MinValue ? SqlDateTime.Null : item.ExamineDate;
                parameters[4].Value = item.Confirmer == null ? SqlString.Null : item.Confirmer;
                parameters[5].Value = item.ConfirmResult;
                parameters[6].Value = item.ConfirmRemark == null ? SqlString.Null : item.ConfirmRemark;
                parameters[7].Value = item.ConfirmTime == DateTime.MinValue ? SqlDateTime.Null : item.ConfirmTime;
                parameters[8].Value = item.UpdateTime == DateTime.MinValue ? SqlDateTime.Null : item.UpdateTime;
                parameters[9].Value = id;
                parameters[10].Value = item.Examiner == null ? SqlString.Null : item.Examiner;
                parameters[11].Value = item.ItemName == null ? SqlString.Null : item.ItemName;
                parameters[12].Value = item.ParentItem;
                parameters[13].Value = item.Score;
                parameters[14].Value = item.Threshold;
                parameters[15].Value = item.Content == null ? SqlString.Null : item.Content;
                parameters[16].Value = item.Standard == null ? SqlString.Null : item.Standard;
                parameters[17].Value = item.ExamItemID;
                parameters[18].Value = item.ChildCount;
                parameters[19].Value = item.Level;
                parameters[20].Value = item.CanAddChild;

                SQLHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters);
            }

        }
        /// <summary>
        ///更新考核表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        long IExamine.UpdateExamine(ExamineInfo model, global::System.Data.Common.DbTransaction trans)
        {
            UpdateExamine(model, trans);
            UpdateExamineDetail(model.ExamSheetID,model, trans);

            return model.ExamSheetID;
        }
        /// <summary>
        /// 更新考核表主表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        private void UpdateExamine(ExamineInfo model, DbTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_Examine set ");
            strSql.Append("ExamineConfirmer=@ExamineConfirmer,");
            strSql.Append("ExamineConfirmResult=@ExamineConfirmResult,");
            strSql.Append("ExamineConfirmRemark=@ExamineConfirmRemark,");
            strSql.Append("ExaminerConfirmDate=@ExaminerConfirmDate,");
            strSql.Append("TargetConfirmer=@TargetConfirmer,");
            strSql.Append("TargetConfirmResult=@TargetConfirmResult,");
            strSql.Append("TargetConfirmRemark=@TargetConfirmRemark,");
            strSql.Append("TargetConfirmDate=@TargetConfirmDate,");
            strSql.Append("Status=@Status,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("ExamSheetNO=@ExamSheetNO,");
            strSql.Append("ExamSheetName=@ExamSheetName,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("Examiner=@Examiner,");
            strSql.Append("ExamineTarget=@ExamineTarget,");
            strSql.Append("Score=@Score,");
            strSql.Append("SaveTime=@SaveTime,");
            strSql.Append("ExamineType=@ExamineType");
            strSql.Append(" where ExamSheetID=@ExamSheetID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ExamSheetID", SqlDbType.BigInt,8),
					new SqlParameter("@ExamineConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@ExamineConfirmResult", SqlDbType.TinyInt,1),
					new SqlParameter("@ExamineConfirmRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@ExaminerConfirmDate", SqlDbType.DateTime),
					new SqlParameter("@TargetConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@TargetConfirmResult", SqlDbType.TinyInt,1),
					new SqlParameter("@TargetConfirmRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@TargetConfirmDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ExamSheetNO", SqlDbType.VarChar,20),
					new SqlParameter("@ExamSheetName", SqlDbType.NVarChar,50),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Examiner", SqlDbType.VarChar,20),
					new SqlParameter("@ExamineTarget", SqlDbType.BigInt,8),
					new SqlParameter("@Score", SqlDbType.Float,8),
					new SqlParameter("@SaveTime", SqlDbType.DateTime),
					new SqlParameter("@ExamineType", SqlDbType.TinyInt,1)};
            parameters[0].Value = model.ExamSheetID;
            parameters[1].Value = model.ExamineConfirmer == null ? SqlString.Null : model.ExamineConfirmer;
            parameters[2].Value = model.ExamineConfirmResult;
            parameters[3].Value = model.ExamineConfirmRemark == null ? SqlString.Null : model.ExamineConfirmRemark;
            parameters[4].Value = model.ExaminerConfirmDate == DateTime.MinValue ? SqlDateTime.Null : model.ExaminerConfirmDate;
            parameters[5].Value = model.TargetConfirmer == null ? SqlString.Null : model.TargetConfirmer;
            parameters[6].Value = model.TargetConfirmResult;
            parameters[7].Value = model.TargetConfirmRemark == null ? SqlString.Null : model.TargetConfirmRemark;
            parameters[8].Value = model.TargetConfirmDate == DateTime.MinValue ? SqlDateTime.Null : model.TargetConfirmDate;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.UpdateTime == DateTime.MinValue ? SqlDateTime.Null : model.UpdateTime;
            parameters[11].Value = model.ExamSheetNO == null ? SqlString.Null : model.ExamSheetNO;
            parameters[12].Value = model.ExamSheetName == null ? SqlString.Null : model.ExamSheetName;
            parameters[13].Value = model.CompanyID == null ? SqlString.Null : model.CompanyID;
            parameters[14].Value = model.Examiner == null ? SqlString.Null : model.Examiner;
            parameters[15].Value = model.ExamineTarget;
            parameters[16].Value = model.Score;
            parameters[17].Value = model.SaveTime == DateTime.MinValue ? SqlDateTime.Null : model.SaveTime;
            parameters[18].Value = model.ExamineType;

            SQLHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 删除考核表
        /// </summary>
        /// <param name="id"></param>
        void IExamine.DeleteExamine(long id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_Examine ");
                strSql.Append(" where ExamSheetID=@ExamSheetID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ExamSheetID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text,strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("删除考核表失败", ex);
            }
        }
        /// <summary>
        /// 获取考核表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ExamineInfo IExamine.GetExamine(long id)
        {
            ExamineInfo item = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from FM2E_ExamineView ");
                strSql.Append(" where ExamSheetID=@ExamSheetID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ExamSheetID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                        item = GetExamineData(rd);
                }

                if (item != null)
                {
                    //StringBuilder strDetail= new StringBuilder();
                    //strDetail.Append("select * from FM2E_ExamineDetailView ");
                    //strDetail.Append(" where ExamSheetID=@ExamSheetID ");

                    //ArrayList list = new ArrayList();
                    //using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strDetail.ToString(), parameters))
                    //{
                    //    while (rd.Read())
                    //    {
                    //        list.Add(GetExamineDetailData(rd));
                    //    }
                    //}
                    item.ExamineItems = GetSubExamineDetail(id,0);
                }
            }
            catch (Exception ex)
            {
                item = null;
                throw new DALException("获取考核表失败，原因："+ex.Message, ex);
            }
            return item;
        }
        /// <summary>
        /// 获取考核明细中某考核项的所有子考核项（树结构）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private IList GetSubExamineDetail(long id,long examItemID)
        {
            ArrayList list = new ArrayList();
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                IList tmpList = GetChildExamineDetail(id,examItemID, trans);
                Stack<ExamineDetailInfo> stack = new Stack<ExamineDetailInfo>();

                if (tmpList != null && tmpList.Count > 0)
                {
                    foreach (ExamineDetailInfo item in tmpList)
                    {
                        stack.Push(item);

                        while (stack.Count > 0)
                        {
                            ExamineDetailInfo it = stack.Pop();
                            list.Add(it);

                            IList childList = GetChildExamineDetail(id,it.ExamItemID, trans);
                            foreach (ExamineDetailInfo child in childList)
                            {
                                stack.Push(child);
                            }
                        }
                    }
                }

                //事务提交
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                list.Clear();
                throw new DALException("获取考核明细的子考核项失败，原因：" + ex.Message, ex);
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
            return list;
        }
        /// <summary>
        /// 获取考核明细中某个考核项的直接子结点
        /// </summary>
        /// <param name="id">考核表ID</param>
        /// <param name="examItemID">考核项ID</param>
        /// <returns></returns>
        public IList GetChildExamineDetail(long id,long examItemID, DbTransaction trans)
        {
            ArrayList list = new ArrayList();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from FM2E_ExamineDetailView ");
            strSql.Append(" where ParentItem=@ParentItem and ExamSheetID=@ExamSheetID");
            strSql.Append(" order by ExamItemID desc");
            SqlParameter[] parameters = {
					new SqlParameter("@ParentItem", SqlDbType.BigInt),
                    new SqlParameter("@ExamSheetID", SqlDbType.BigInt)};
            parameters[0].Value = examItemID;
            parameters[1].Value = id;

            using (SqlDataReader rd = SQLHelper.ExecuteReader((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                    list.Add(GetExamineDetailData(rd));
            }

            return list;
        }
        /// <summary>
        /// 获取考核表查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam IExamine.GetSearchTerm(ExamineSearchInfo term)
        {
            QueryParam qp = new QueryParam();
            qp.ReturnFields = "*";
            qp.TableName = "FM2E_ExamineView";
            qp.Where = GetSqlWhere(term) ;
            qp.OrderBy = "order by UpdateTime desc";

            return qp;
        }
        /// <summary>
        /// 查询考核表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList IExamine.GetExamines(QueryParam term, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetExamineData, term, out recordCount);
            }
            catch (Exception ex)
            {
                throw new DALException("获取考核表分页失败", ex);
            }
        }

        private string GetSqlWhere(ExamineSearchInfo term)
        {
            string sqlSearch = "where 1=1 ";

            if (!string.IsNullOrEmpty(term.ExamSheetNO))
                sqlSearch += " and ExamSheetNO='%" + term.ExamSheetNO + "%'";

            if (!string.IsNullOrEmpty(term.CompanyID))
                sqlSearch += " and CompanyID='" + term.CompanyID + "'";

            if (term.ExamineTarget != 0)
                sqlSearch += " and ExamineTarget=" + term.ExamineTarget;

            if (!string.IsNullOrEmpty(term.Examiner))
                sqlSearch += " and Examiner='" + term.Examiner + "'";

            if (!string.IsNullOrEmpty(term.ExaminerName))
                sqlSearch += " and ExaminerName like '%" + term.ExaminerName + "%'";

            if (term.ExamineType != (int)ExamineType.Unknown)
                sqlSearch += " and ExamineType=" + term.ExamineType;

            if (term.Status != ExamineSheetStatus.Unknown)
                sqlSearch += " and Status=" + (int)term.Status;

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
            return sqlSearch;
        }
        /// <summary>
        /// 获取符合条件的所有考核表
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList IExamine.GetExamines(ExamineSearchInfo term)
        {
            ArrayList list=new ArrayList();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from FM2E_ExamineView ");
                strSql.Append(GetSqlWhere(term));

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                        list.Add(GetExamineData(rd));
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取考核表失败", ex);
            }
            return list;
        }
        /// <summary>
        /// 考核表确认
        /// </summary>
        /// <param name="id"></param>
        /// <param name="confirmer"></param>
        /// <param name="result"></param>
        /// <param name="remark"></param>
        /// <param name="confirmDate"></param>
        void IExamine.ExamineConfirm(long id, string confirmer, ExamineConfirmResult result, string remark, DateTime confirmDate, ExamineSheetStatus status)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_Examine set ");
                strSql.Append("ExamineConfirmer=@ExamineConfirmer,");
                strSql.Append("ExamineConfirmResult=@ExamineConfirmResult,");
                strSql.Append("ExamineConfirmRemark=@ExamineConfirmRemark,");
                strSql.Append("ExaminerConfirmDate=@ExaminerConfirmDate,");
                strSql.Append("Status=@Status");
                strSql.Append(" where ExamSheetID=@ExamSheetID ");

                SqlParameter[] parameters = {	
                    new SqlParameter("@ExamSheetID", SqlDbType.BigInt,8),
					new SqlParameter("@ExamineConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@ExamineConfirmResult", SqlDbType.TinyInt,1),
					new SqlParameter("@ExamineConfirmRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@ExaminerConfirmDate", SqlDbType.DateTime),
                    new SqlParameter("@Status",SqlDbType.TinyInt)};

                parameters[0].Value = id;
                parameters[1].Value = confirmer;
                parameters[2].Value = (int)result;
                parameters[3].Value = remark;
                parameters[4].Value = confirmDate;
                parameters[5].Value = (int)status;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("确认考核表失败,原因："+ex.Message, ex);
            }
        }
        #endregion

        #region 考核结果
        /// <summary>
        /// 添加考核结果
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long IExamine.AddExamineResult(ExamineResultInfo model)
        {
            long id = 0;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FM2E_ExamineResult(");
                strSql.Append("SaveTime,ExamineConfirmer,ExamineConfirmResult,ExamineConfirmRemark,ExamineConfirmTime,TargetConfirmer,TargetConfirmResult,TargetConfirmRemark,TargetConfirmTime,UpdateTime,CompanyID,Season,Examiner,ExamineTarget,DailyExamineResult,DailyExamineRatio,SeasonExamineResult,SeasonExamineRatio,SheetNO,SheetName,Year)");
                strSql.Append(" values (");
                strSql.Append("@SaveTime,@ExamineConfirmer,@ExamineConfirmResult,@ExamineConfirmRemark,@ExamineConfirmTime,@TargetConfirmer,@TargetConfirmResult,@TargetConfirmRemark,@TargetConfirmTime,@UpdateTime,@CompanyID,@Season,@Examiner,@ExamineTarget,@DailyExamineResult,@DailyExamineRatio,@SeasonExamineResult,@SeasonExamineRatio,@SheetNO,@SheetName,@Year)");
                strSql.Append(";select cast(@@IDENTITY as bigint);");
                SqlParameter[] parameters = {
					new SqlParameter("@SaveTime", SqlDbType.DateTime),
					new SqlParameter("@ExamineConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@ExamineConfirmResult", SqlDbType.TinyInt,1),
					new SqlParameter("@ExamineConfirmRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@ExamineConfirmTime", SqlDbType.DateTime),
					new SqlParameter("@TargetConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@TargetConfirmResult", SqlDbType.TinyInt,1),
					new SqlParameter("@TargetConfirmRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@TargetConfirmTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Season", SqlDbType.TinyInt,1),
					new SqlParameter("@Examiner", SqlDbType.VarChar,20),
					new SqlParameter("@ExamineTarget", SqlDbType.BigInt,8),
					new SqlParameter("@DailyExamineResult", SqlDbType.Float,8),
					new SqlParameter("@DailyExamineRatio", SqlDbType.Float,8),
					new SqlParameter("@SeasonExamineResult", SqlDbType.Float,8),
					new SqlParameter("@SeasonExamineRatio", SqlDbType.Float,8),
                    new SqlParameter("@SheetNO",SqlDbType.VarChar,20),
                    new SqlParameter("@SheetName",SqlDbType.NVarChar,50),
                    new SqlParameter("@Year",SqlDbType.Int,4)};
                parameters[0].Value = model.SaveTime == DateTime.MinValue ? SqlDateTime.Null : model.SaveTime;
                parameters[1].Value = model.ExamineConfirmer == null ? SqlString.Null : model.ExamineConfirmer;
                parameters[2].Value = model.ExamineConfirmResult;
                parameters[3].Value = model.ExamineConfirmRemark == null ? SqlString.Null : model.ExamineConfirmRemark;
                parameters[4].Value = model.ExamineConfirmTime == DateTime.MinValue ? SqlDateTime.Null : model.ExamineConfirmTime;
                parameters[5].Value = model.TargetConfirmer == null ? SqlString.Null : model.TargetConfirmer;
                parameters[6].Value = model.TargetConfirmResult;
                parameters[7].Value = model.TargetConfirmRemark == null ? SqlString.Null : model.TargetConfirmRemark;
                parameters[8].Value = model.TargetConfirmTime == DateTime.MinValue ? SqlDateTime.Null : model.TargetConfirmTime;
                parameters[9].Value = model.UpdateTime == DateTime.MinValue ? SqlDateTime.Null : model.UpdateTime;
                parameters[10].Value = model.CompanyID == null ? SqlString.Null : model.CompanyID;
                parameters[11].Value = model.Season;
                parameters[12].Value = model.Examiner == null ? SqlString.Null : model.Examiner;
                parameters[13].Value = model.ExamineTarget;
                parameters[14].Value = model.DailyExamineResult;
                parameters[15].Value = model.DailyExamineRatio;
                parameters[16].Value = model.SeasonExamineResult;
                parameters[17].Value = model.SeasonExamineRatio;
                parameters[18].Value = model.SheetNO == null ? SqlString.Null : model.SheetNO;
                parameters[19].Value = model.SheetName == null ? SqlString.Null : model.SheetName;
                parameters[20].Value = model.Year;

                id = (long)SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("添加考核结果失败", ex);
            }
            return id;
        }
        /// <summary>
        /// 修改考核结果
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long IExamine.UpdateExamineResult(ExamineResultInfo model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update FM2E_ExamineResult set ");
                strSql.Append("SaveTime=@SaveTime,");
                strSql.Append("ExamineConfirmer=@ExamineConfirmer,");
                strSql.Append("ExamineConfirmResult=@ExamineConfirmResult,");
                strSql.Append("ExamineConfirmRemark=@ExamineConfirmRemark,");
                strSql.Append("ExamineConfirmTime=@ExamineConfirmTime,");
                strSql.Append("TargetConfirmer=@TargetConfirmer,");
                strSql.Append("TargetConfirmResult=@TargetConfirmResult,");
                strSql.Append("TargetConfirmRemark=@TargetConfirmRemark,");
                strSql.Append("TargetConfirmTime=@TargetConfirmTime,");
                strSql.Append("UpdateTime=@UpdateTime,");
                strSql.Append("CompanyID=@CompanyID,");
                strSql.Append("Season=@Season,");
                strSql.Append("Examiner=@Examiner,");
                strSql.Append("ExamineTarget=@ExamineTarget,");
                strSql.Append("DailyExamineResult=@DailyExamineResult,");
                strSql.Append("DailyExamineRatio=@DailyExamineRatio,");
                strSql.Append("SeasonExamineResult=@SeasonExamineResult,");
                strSql.Append("SeasonExamineRatio=@SeasonExamineRatio,");
                strSql.Append("SheetNO=@SheetNO,");
                strSql.Append("SheetName=@SheetName,");
                strSql.Append("Year=@Year");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@SaveTime", SqlDbType.DateTime),
					new SqlParameter("@ExamineConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@ExamineConfirmResult", SqlDbType.TinyInt,1),
					new SqlParameter("@ExamineConfirmRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@ExamineConfirmTime", SqlDbType.DateTime),
					new SqlParameter("@TargetConfirmer", SqlDbType.VarChar,20),
					new SqlParameter("@TargetConfirmResult", SqlDbType.TinyInt,1),
					new SqlParameter("@TargetConfirmRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@TargetConfirmTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Season", SqlDbType.TinyInt,1),
					new SqlParameter("@Examiner", SqlDbType.VarChar,20),
					new SqlParameter("@ExamineTarget", SqlDbType.BigInt,8),
					new SqlParameter("@DailyExamineResult", SqlDbType.Float,8),
					new SqlParameter("@DailyExamineRatio", SqlDbType.Float,8),
					new SqlParameter("@SeasonExamineResult", SqlDbType.Float,8),
					new SqlParameter("@SeasonExamineRatio", SqlDbType.Float,8),
                    new SqlParameter("@SheetNO",SqlDbType.VarChar,20),
                    new SqlParameter("@SheetName",SqlDbType.NVarChar,50),
                    new SqlParameter("@Year",SqlDbType.Int,4)};
                parameters[0].Value = model.ID;
                parameters[1].Value = model.SaveTime == DateTime.MinValue ? SqlDateTime.Null : model.SaveTime;
                parameters[2].Value = model.ExamineConfirmer == null ? SqlString.Null : model.ExamineConfirmer;
                parameters[3].Value = model.ExamineConfirmResult;
                parameters[4].Value = model.ExamineConfirmRemark == null ? SqlString.Null : model.ExamineConfirmRemark;
                parameters[5].Value = model.ExamineConfirmTime == DateTime.MinValue ? SqlDateTime.Null : model.ExamineConfirmTime;
                parameters[6].Value = model.TargetConfirmer == null ? SqlString.Null : model.TargetConfirmer;
                parameters[7].Value = model.TargetConfirmResult;
                parameters[8].Value = model.TargetConfirmRemark == null ? SqlString.Null : model.TargetConfirmRemark;
                parameters[9].Value = model.TargetConfirmTime == DateTime.MinValue ? SqlDateTime.Null : model.TargetConfirmTime;
                parameters[10].Value = model.UpdateTime == DateTime.MinValue ? SqlDateTime.Null : model.UpdateTime;
                parameters[11].Value = model.CompanyID == null ? SqlString.Null : model.CompanyID;
                parameters[12].Value = model.Season;
                parameters[13].Value = model.Examiner == null ? SqlString.Null : model.Examiner;
                parameters[14].Value = model.ExamineTarget;
                parameters[15].Value = model.DailyExamineResult;
                parameters[16].Value = model.DailyExamineRatio;
                parameters[17].Value = model.SeasonExamineResult;
                parameters[18].Value = model.SeasonExamineRatio;
                parameters[19].Value = model.SheetNO == null ? SqlString.Null : model.SheetNO;
                parameters[20].Value = model.SheetName == null ? SqlString.Null : model.SheetName;
                parameters[21].Value = model.Year;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("修改考核结果失败", ex);
            }
            return model.ID;
        }
        /// <summary>
        /// 获取已保存的考核结果
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ExamineResultInfo IExamine.GetExamineResult(long id)
        {
            ExamineResultInfo item = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from FM2E_ExamineResultView ");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                        item = GetExamineResultData(rd);
                }
            }
            catch (Exception ex)
            {
                item = null;
                throw new DALException("获取已保存的考核结果失败", ex);
            }
            return item;
        }
        /// <summary>
        /// 删除考核结果
        /// </summary>
        /// <param name="id"></param>
        void IExamine.DeleteExamineResult(long id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_ExamineResult ");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw new DALException("删除考核结果失败", ex);
            }
        }
        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam IExamine.GetSearchTerm(ExamineResultSearchInfo term)
        {
            string sqlSearch = "where 1=1 ";

            if (!string.IsNullOrEmpty(term.SheetNO))
                sqlSearch += " and SheetNO like '%" + term.SheetNO + "%'";

            if (!string.IsNullOrEmpty(term.SheetName))
                sqlSearch += " and SheetName like '%" + term.SheetName + "%'";

            if (term.Year != 0)
            {
                sqlSearch += " and Year=" + term.Year;
            }

            if (!string.IsNullOrEmpty(term.CompanyID))
                sqlSearch += " and CompanyID='" + term.CompanyID + "'";

            if (term.ExamineTarget != 0)
                sqlSearch += " and ExamineTarget=" + term.ExamineTarget;

            if (!string.IsNullOrEmpty(term.Examiner))
                sqlSearch += " and Examiner='" + term.Examiner + "'";

            if (!string.IsNullOrEmpty(term.ExaminerName))
                sqlSearch += " and ExaminerName like '%" + term.ExaminerName + "%'";

            if (term.Season != ExamineSeason.Unknown)
                sqlSearch += " and Season=" + (int)term.Season;

            if (!string.IsNullOrEmpty(term.ExamineConfirmer))
                sqlSearch += " and ExamineConfirmer='" + term.ExamineConfirmer + "'";

            if (term.ExamineConfirmeResult != ExamineConfirmResult.Unknown)
                sqlSearch += " and ExamineConfirmeResult=" + (int)term.ExamineConfirmeResult;

            if (!string.IsNullOrEmpty(term.TargetConfirmer))
                sqlSearch += " and TargetConfirmer='" + term.TargetConfirmer + "'";

            if (term.TargetConfirmeResult != ExamineConfirmResult.Unknown)
                sqlSearch += " and TargetConfirmeResult=" + (int)term.TargetConfirmeResult;

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
            qp.ReturnFields = "*";
            qp.TableName = "FM2E_ExamineResultView";
            qp.Where = sqlSearch;
            qp.OrderBy = "order by UpdateTime desc";

            return qp;
        }
        /// <summary>
        /// 根据查询条件获取考核结果表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList IExamine.GetExamineResults(QueryParam term, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetExamineResultData, term, out recordCount);
            }
            catch (Exception ex)
            {
                throw new DALException("获取考核结果表分页失败", ex);
            }
        }
        #endregion
        #endregion
    }
}
