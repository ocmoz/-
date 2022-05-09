using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;
using FM2E.IDAL.PendingOrder;
using FM2E.Model.PendingOrder;
using System.Collections;
using FM2E.Model.Exceptions;
using FM2E.Model.Utils;

namespace FM2E.SQLServerDAL.PendingOrder
{
    public class PendingOrder : IPendingOrder
    {
        private const string TABLE_NAME_PENDINGORDER = "FM2E_PendingOrder";
        private const string TABLE_NAME_RECEIVER = "FM2E_PendingOrderReceiver";

        #region IPendingOrder 成员



        /// <summary>
        /// 添加新的待办事务
        /// </summary>
        /// <param name="item"></param>
        void IPendingOrder.InsertPendingOrder(PendingOrderInfo item)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先插入待办事务
                long id = InsertPendingOrder(trans, item);

                //插入待办事务对象列表
                InsertReceivers(trans, item, id);

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
        /// 插入待办事务主体
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="msg">待办事务</param>
        /// <returns>自增标识</returns>
        private long InsertPendingOrder(SqlTransaction trans, PendingOrderInfo msg)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + TABLE_NAME_PENDINGORDER + "(");
            strSql.Append("Type,SendFrom,SendTime,Title,URL,Address)");
            strSql.Append(" values (");
            strSql.Append("@Type,@SendFrom,@SendTime,@Title,@URL,@Address)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@Type", SqlDbType.TinyInt,1),
					new SqlParameter("@SendFrom", SqlDbType.VarChar,10),
					new SqlParameter("@SendTime", SqlDbType.DateTime),
                    new SqlParameter("@Title", SqlDbType.NVarChar,50),
					new SqlParameter("@URL", SqlDbType.NVarChar,200),
                    new SqlParameter("@Address", SqlDbType.NVarChar,200)};
            parameters[0].Value = msg.Type;
            parameters[1].Value = msg.SendFrom;
            parameters[2].Value = msg.SendTime;
            parameters[3].Value = msg.Title;
            parameters[4].Value = msg.URL;
            parameters[5].Value = msg.ReceiverAddress;

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
        /// 插入待办事务接收对象
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="msg">待办事务</param>
        /// <param name="id">待办事务ID</param>
        private void InsertReceivers(SqlTransaction trans, PendingOrderInfo msg, long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + TABLE_NAME_RECEIVER + "(");
            strSql.Append("ID,UserName,HasRead)");
            strSql.Append(" values (");
            strSql.Append("@ID,@UserName,@HasRead)");
            if (msg.Receivers != null)
            {
                //针对每一个进行
                foreach (PendingOrderReceiverInfo mri in msg.Receivers)
                {
                    SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
					new SqlParameter("@HasRead", SqlDbType.Bit,1)};
                    parameters[0].Value = id;
                    parameters[1].Value = mri.UserName;
                    parameters[2].Value = mri.HasRead;

                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                }
            }


        }

        /// <summary>
        /// 获取待办事务实体（不包含用户列表）
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private PendingOrderInfo GetData(IDataReader rd)
        {
            PendingOrderInfo item = new PendingOrderInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["Type"]))
                item.Type = Convert.ToInt16(rd["Type"]);

            if (!Convert.IsDBNull(rd["SendFrom"]))
                item.SendFrom = Convert.ToString(rd["SendFrom"]);

            if (!Convert.IsDBNull(rd["SendTime"]))
                item.SendTime = Convert.ToDateTime(rd["SendTime"]);

            if (!Convert.IsDBNull(rd["Title"]))
                item.Title = Convert.ToString(rd["Title"]);

            if (!Convert.IsDBNull(rd["URL"]))
                item.URL = Convert.ToString(rd["URL"]);

            if (!Convert.IsDBNull(rd["Address"]))
                item.ReceiverAddress = Convert.ToString(rd["Address"]);

            return item;
        }

        /// <summary>
        /// 获取一个接收者信息实体
        /// </summary>
        /// <returns>接收者信息</returns>
        private PendingOrderReceiverInfo GetDataReceiver(IDataReader rd)
        {
            PendingOrderReceiverInfo item = new PendingOrderReceiverInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["UserName"]))
                item.UserName = Convert.ToString(rd["UserName"]);

            if (!Convert.IsDBNull(rd["HasRead"]))
                item.HasRead = Convert.ToBoolean(rd["HasRead"]);

            return item;
        }

        private PendingOrderReceiverCombineInfo GetDataPendingOrderReceiver(IDataReader rd)
        {
            PendingOrderReceiverCombineInfo item = new PendingOrderReceiverCombineInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["Type"]))
                item.Type = Convert.ToInt16(rd["Type"]);

            if (!Convert.IsDBNull(rd["SendFrom"]))
                item.SendFrom = Convert.ToString(rd["SendFrom"]);

            if (!Convert.IsDBNull(rd["SendTime"]))
                item.SendTime = Convert.ToDateTime(rd["SendTime"]);

            if (!Convert.IsDBNull(rd["Title"]))
                item.Title = Convert.ToString(rd["Title"]);

            if (!Convert.IsDBNull(rd["URL"]))
                item.URL = Convert.ToString(rd["URL"]);

            if (!Convert.IsDBNull(rd["Address"]))
                item.ReceiverAddress = Convert.ToString(rd["Address"]);

            if (!Convert.IsDBNull(rd["UserName"]))
                item.UserName = Convert.ToString(rd["UserName"]);

            if (!Convert.IsDBNull(rd["HasRead"]))
                item.HasRead = Convert.ToBoolean(rd["HasRead"]);

            return item;
        }

        /// <summary>
        /// 根据用户名获取待办事务列表，不包含用户列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>待办事务列表</returns>
        IList IPendingOrder.GetPendingOrderListByReceiver(int pageIndex, int pageSize, out int recordCount, string username)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME_PENDINGORDER + " INNER JOIN " +
                                 TABLE_NAME_RECEIVER + " ON " +
                                 TABLE_NAME_PENDINGORDER + ".ID = " + TABLE_NAME_RECEIVER + ".ID ";
                qp.ReturnFields = string.Format(
                    TABLE_NAME_PENDINGORDER + ".ID"
                    + "," + TABLE_NAME_PENDINGORDER + ".Type"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendFrom"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendTime"
                    + "," + TABLE_NAME_PENDINGORDER + ".Title"
                    + "," + TABLE_NAME_PENDINGORDER + ".URL"
                    + "," + TABLE_NAME_PENDINGORDER + ".Address"
                    + "," + TABLE_NAME_RECEIVER + ".UserName"
                    + "," + TABLE_NAME_RECEIVER + ".HasRead"
                    + ",{0} as HasReadSort,{1} as SendTimeSort", TABLE_NAME_RECEIVER + ".HasRead", TABLE_NAME_PENDINGORDER + ".SendTime");
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_NAME_RECEIVER + ".[UserName]='" + username + "' and " + TABLE_NAME_RECEIVER + ".[HasRead]=0";
                qp.OrderBy = string.Format("order by {0} ASC,{1} DESC", "HasReadSort", "SendTimeSort");
                return SQLHelper.GetObjectList(this.GetDataPendingOrderReceiver, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取待办事务分页失败", e);
            }
        }
        /// <summary>
        /// 根据用户名获取待办事务列表，不包含用户列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>待办事务列表</returns>
        IList IPendingOrder.GetDoneOrderListByReceiver(int pageIndex, int pageSize, out int recordCount, string username)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME_PENDINGORDER + " INNER JOIN " +
                                 TABLE_NAME_RECEIVER + " ON " +
                                 TABLE_NAME_PENDINGORDER + ".ID = " + TABLE_NAME_RECEIVER + ".ID ";
                qp.ReturnFields = string.Format(
                    TABLE_NAME_PENDINGORDER + ".ID"
                    + "," + TABLE_NAME_PENDINGORDER + ".Type"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendFrom"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendTime"
                    + "," + TABLE_NAME_PENDINGORDER + ".Title"
                    + "," + TABLE_NAME_PENDINGORDER + ".URL"
                    + "," + TABLE_NAME_PENDINGORDER + ".Address"
                    + "," + TABLE_NAME_RECEIVER + ".UserName"
                    + "," + TABLE_NAME_RECEIVER + ".HasRead"
                    + ",{0} as HasReadSort,{1} as SendTimeSort", TABLE_NAME_RECEIVER + ".HasRead", TABLE_NAME_PENDINGORDER + ".SendTime");
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_NAME_RECEIVER + ".[UserName]='" + username + "' and " + TABLE_NAME_RECEIVER + ".[HasRead]=1";
                qp.OrderBy = string.Format("order by {0} ASC,{1} DESC", "HasReadSort", "SendTimeSort");
                return SQLHelper.GetObjectList(this.GetDataPendingOrderReceiver, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取待办事务分页失败", e);
            }
        }

        /// <summary>
        /// 根据接收用户名和指定时间区段查询
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>待办事务列表</returns>
        IList IPendingOrder.GetPendingOrderListByReceiver(int pageIndex, int pageSize, out int recordCount, string username, DateTime start)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME_PENDINGORDER + " INNER JOIN " +
                                 TABLE_NAME_RECEIVER + " ON " +
                                 TABLE_NAME_PENDINGORDER + ".ID = " + TABLE_NAME_RECEIVER + ".ID ";
                qp.ReturnFields = string.Format(
                    TABLE_NAME_PENDINGORDER + ".ID"
                    + "," + TABLE_NAME_PENDINGORDER + ".Type"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendFrom"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendTime"
                    + "," + TABLE_NAME_PENDINGORDER + ".Title"
                    + "," + TABLE_NAME_PENDINGORDER + ".URL"
                    + "," + TABLE_NAME_PENDINGORDER + ".Address"
                    + "," + TABLE_NAME_RECEIVER + ".UserName"
                    + "," + TABLE_NAME_RECEIVER + ".HasRead"
                    + ",{0} as HasReadSort,{1} as SendTimeSort", TABLE_NAME_RECEIVER + ".HasRead", TABLE_NAME_PENDINGORDER + ".SendTime");
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_NAME_RECEIVER + ".[UserName]='" + username + "'" +
                    " and " + TABLE_NAME_PENDINGORDER + ".SendTime>='" + start.ToShortDateString() + "'";
                qp.OrderBy = string.Format("order by {0} ASC,{1} DESC", "HasReadSort", "SendTimeSort");
                return SQLHelper.GetObjectList(this.GetDataPendingOrderReceiver, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取待办事务分页失败", e);
            }
        }


        /// <summary>
        /// 根据接收用户名和指定时间区段查询
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>待办事务列表</returns>
        IList IPendingOrder.GetPendingOrderListByReceiver(int pageIndex, int pageSize, out int recordCount, string username, DateTime start, DateTime end)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME_PENDINGORDER + " INNER JOIN " +
                                 TABLE_NAME_RECEIVER + " ON " +
                                 TABLE_NAME_PENDINGORDER + ".ID = " + TABLE_NAME_RECEIVER + ".ID ";
                qp.ReturnFields = string.Format(
                    TABLE_NAME_PENDINGORDER + ".ID"
                    + "," + TABLE_NAME_PENDINGORDER + ".Type"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendFrom"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendTime"
                    + "," + TABLE_NAME_PENDINGORDER + ".Title"
                    + "," + TABLE_NAME_PENDINGORDER + ".URL"
                    + "," + TABLE_NAME_PENDINGORDER + ".Address"
                    + "," + TABLE_NAME_RECEIVER + ".UserName"
                    + "," + TABLE_NAME_RECEIVER + ".HasRead"
                    + ",{0} as HasReadSort,{1} as SendTimeSort", TABLE_NAME_RECEIVER + ".HasRead", TABLE_NAME_PENDINGORDER + ".SendTime");
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_NAME_RECEIVER + ".[UserName]='" + username + "'" +
                    " and " + TABLE_NAME_PENDINGORDER + ".SendTime>='" + start.ToShortDateString() + "'" +
                    " and " + TABLE_NAME_PENDINGORDER + ".SendTime<='" + start.ToShortDateString() + "'";
                qp.OrderBy = string.Format("order by {0} ASC,{1} DESC", "HasReadSort", "SendTimeSort");
                return SQLHelper.GetObjectList(this.GetDataPendingOrderReceiver, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取待办事务分页失败", e);
            }
        }

        /// <summary>
        /// 获取所有待办事务
        /// </summary>
        /// <returns>待办事务列表</returns>
        IList IPendingOrder.GetAllPendingOrder(int pageIndex, int pageSize, out int recordCount)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME_PENDINGORDER;
                qp.ReturnFields = string.Format(
                    TABLE_NAME_PENDINGORDER + ".ID"
                    + "," + TABLE_NAME_PENDINGORDER + ".Type"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendFrom"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendTime"
                    + "," + TABLE_NAME_PENDINGORDER + ".Title"
                    + "," + TABLE_NAME_PENDINGORDER + ".URL"
                    + "," + TABLE_NAME_PENDINGORDER + ".Address"
                    + ",{0} as SendTimeSort", TABLE_NAME_PENDINGORDER + ".SendTime");
                qp.PageIndex = pageIndex;
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.OrderBy = string.Format("order by {0} DESC", "SendTimeSort");
                return SQLHelper.GetObjectList(this.GetData, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取待办事务分页失败", e);
            }
        }

        /// <summary>
        /// 获取指定时间到现在内待办事务
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <returns>待办事务列表</returns>
        IList IPendingOrder.GetAllPendingOrder(int pageIndex, int pageSize, out int recordCount, DateTime start)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME_PENDINGORDER;
                qp.ReturnFields = string.Format(
                    TABLE_NAME_PENDINGORDER + ".ID"
                    + "," + TABLE_NAME_PENDINGORDER + ".Type"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendFrom"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendTime"
                    + "," + TABLE_NAME_PENDINGORDER + ".Title"
                    + "," + TABLE_NAME_PENDINGORDER + ".URL"
                    + "," + TABLE_NAME_PENDINGORDER + ".Address"
                    + ",{0} as SendTimeSort", TABLE_NAME_PENDINGORDER + ".SendTime");
                qp.PageIndex = pageIndex;
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_NAME_PENDINGORDER + ".SendTime>='" + start.ToShortDateString() + "'";
                qp.OrderBy = string.Format("order by {0} DESC", "SendTimeSort");
                return SQLHelper.GetObjectList(this.GetData, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取待办事务分页失败", e);
            }
        }

        /// <summary>
        /// 获取指定时间段内待办事务
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>待办事务列表</returns>
        IList IPendingOrder.GetAllPendingOrder(int pageIndex, int pageSize, out int recordCount, DateTime start, DateTime end)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME_PENDINGORDER;
                qp.ReturnFields = string.Format(
                    TABLE_NAME_PENDINGORDER + ".ID"
                    + "," + TABLE_NAME_PENDINGORDER + ".Type"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendFrom"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendTime"
                    + "," + TABLE_NAME_PENDINGORDER + ".Title"
                    + "," + TABLE_NAME_PENDINGORDER + ".URL"
                    + "," + TABLE_NAME_PENDINGORDER + ".Address"
                    + ",{0} as SendTimeSort", TABLE_NAME_PENDINGORDER + ".SendTime");
                qp.PageIndex = pageIndex;
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_NAME_PENDINGORDER + ".SendTime>='" + start.ToShortDateString() + "'" +
                    " and " + TABLE_NAME_PENDINGORDER + ".SendTime<='" + start.ToShortDateString() + "'";
                qp.OrderBy = string.Format("order by {0} DESC", "SendTimeSort");
                return SQLHelper.GetObjectList(this.GetData, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取待办事务分页失败", e);
            }
        }

        /// <summary>
        /// 根据指定的发送人获取待办事务
        /// </summary>
        /// <param name="sender">发送人ID</param>
        /// <returns>待办事务列表</returns>
        IList IPendingOrder.GetPendingOrderListBySender(int pageIndex, int pageSize, out int recordCount, string sender)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME_PENDINGORDER;
                qp.ReturnFields = string.Format(
                    TABLE_NAME_PENDINGORDER + ".ID"
                    + "," + TABLE_NAME_PENDINGORDER + ".Type"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendFrom"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendTime"
                    + "," + TABLE_NAME_PENDINGORDER + ".Title"
                    + "," + TABLE_NAME_PENDINGORDER + ".URL"
                    + "," + TABLE_NAME_PENDINGORDER + ".Address"
                    + ",{0} as SendTimeSort", TABLE_NAME_PENDINGORDER + ".SendTime");
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_NAME_PENDINGORDER + ".[SendFrom]='" + sender + "'";
                qp.OrderBy = string.Format("order by {0} DESC", "SendTimeSort");
                return SQLHelper.GetObjectList(this.GetData, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取待办事务分页失败", e);
            }
        }


        /// <summary>
        /// 根据指定发送人，获取指定时间开始到现在
        /// </summary>
        /// <param name="sender">发送人</param>
        /// <param name="start">开始时间</param>
        /// <returns>待办事务列表</returns>
        IList IPendingOrder.GetPendingOrderListBySender(int pageIndex, int pageSize, out int recordCount, string sender, DateTime start)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME_PENDINGORDER;
                qp.ReturnFields = string.Format(

                    TABLE_NAME_PENDINGORDER + ".ID"
                    + "," + TABLE_NAME_PENDINGORDER + ".Type"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendFrom"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendTime"
                    + "," + TABLE_NAME_PENDINGORDER + ".Title"
                    + "," + TABLE_NAME_PENDINGORDER + ".URL"
                    + "," + TABLE_NAME_PENDINGORDER + ".Address"
                    + ",{0} as SendTimeSort", TABLE_NAME_PENDINGORDER + ".SendTime");
                qp.PageIndex = pageIndex;
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_NAME_PENDINGORDER + ".[SendFrom]='" + sender + "'" +
                    " and " + TABLE_NAME_PENDINGORDER + ".SendTime>='" + start.ToShortDateString() + "'";
                qp.OrderBy = string.Format("order by {0} DESC", "SendTimeSort");
                return SQLHelper.GetObjectList(this.GetData, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取待办事务分页失败", e);
            }
        }

        /// <summary>
        /// 根据指定发送人，获取指定时间段内待办事务
        /// </summary>
        /// <param name="sender">发送人</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>待办事务列表</returns>
        IList IPendingOrder.GetPendingOrderListBySender(int pageIndex, int pageSize, out int recordCount, string sender, DateTime start, DateTime end)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME_PENDINGORDER;
                qp.ReturnFields = string.Format(
                    TABLE_NAME_PENDINGORDER + ".ID"
                    + "," + TABLE_NAME_PENDINGORDER + ".Type"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendFrom"
                    + "," + TABLE_NAME_PENDINGORDER + ".SendTime"
                    + "," + TABLE_NAME_PENDINGORDER + ".Title"
                    + "," + TABLE_NAME_PENDINGORDER + ".URL"
                    + "," + TABLE_NAME_PENDINGORDER + ".Address"
                    + ",{0} as SendTimeSort", TABLE_NAME_PENDINGORDER + ".SendTime");
                qp.PageIndex = pageIndex;
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_NAME_PENDINGORDER + ".[SendFrom]='" + sender + "'" +
                    " and " + TABLE_NAME_PENDINGORDER + ".SendTime>='" + start.ToShortDateString() + "'" +
                    " and " + TABLE_NAME_PENDINGORDER + ".SendTime<='" + start.ToShortDateString() + "'";
                qp.OrderBy = string.Format("order by {0} DESC", "SendTimeSort");
                return SQLHelper.GetObjectList(this.GetData, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取待办事务分页失败", e);
            }
        }

        /// <summary>
        /// 获取待办事务实体，包含所有接收人
        /// </summary>
        /// <param name="id">待办事务ID</param>
        /// <returns>待办事务实体</returns>
        PendingOrderInfo IPendingOrder.GetPendingOrder(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Type,SendFrom,SendTime,Title,URL,Address from " + TABLE_NAME_PENDINGORDER + " ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = id;

            PendingOrderInfo msg = new PendingOrderInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    msg = GetData(rd);
                    break;
                }
            }
            //获取待办事务的接收人
            List<PendingOrderReceiverInfo> receivers = new List<PendingOrderReceiverInfo>();

            strSql = new StringBuilder();
            strSql.Append("select ID,UserName,HasRead from " + TABLE_NAME_RECEIVER + " ");
            strSql.Append(" where ID=@ID  ");

            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    PendingOrderReceiverInfo mri = GetDataReceiver(rd);
                    receivers.Add(mri);
                }
            }
            msg.Receivers = receivers;
            return msg;
        }

        /// <summary>
        /// 标记已读
        /// </summary>
        /// <param name="id">待办事务ID</param>
        /// <param name="username">用户名</param>
        void IPendingOrder.MarkRead(long id, string username)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + TABLE_NAME_RECEIVER + " set ");
            strSql.Append("HasRead=@HasRead");
            strSql.Append(" where ID=@ID and UserName=@UserName ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
					new SqlParameter("@HasRead", SqlDbType.Bit,1)};
            parameters[0].Value = id;
            parameters[1].Value = username;
            parameters[2].Value = true;

            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }


        //**********Modified by Xue 2011-6-27****************************************************************************************************
        /// <summary>
        /// 根据URL标记为已处理
        /// </summary>
        /// <param name="url">处理URL地址</param>
        /// <param name="b">是否阅读</param>
        void IPendingOrder.MarkReadByURL(string url)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + TABLE_NAME_RECEIVER + " set ");
            strSql.Append("HasRead=@HasRead");
            strSql.Append(" where ID=(select top 1 ID from " + TABLE_NAME_PENDINGORDER + " where URL=@URL order by SendTime DESC)");
            SqlParameter[] parameters = {
                    new SqlParameter("@URL", SqlDbType.NVarChar,200),
					new SqlParameter("@HasRead", SqlDbType.Bit,1)};
            parameters[0].Value = url;
            parameters[1].Value = true;

            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        //**********Modification Finished 2011-6-27**********************************************************************************************


        /// <summary>
        /// 删除整条待办事务
        /// </summary>
        /// <param name="id">待办事务ID</param>
        void IPendingOrder.DeletePendingOrder(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete " + TABLE_NAME_PENDINGORDER + " ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = id;

            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除待办事务的接收用户
        /// </summary>
        /// <param name="id">待办事务ID</param>
        /// <param name="username">接收用户</param>
        void IPendingOrder.DeletePendingOrder(long id, string username)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete " + TABLE_NAME_RECEIVER + " ");
            strSql.Append(" where ID=@ID and UserName=@UserName ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt),
					new SqlParameter("@UserName", SqlDbType.VarChar,50)};
            parameters[0].Value = id;
            parameters[1].Value = username;

            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 获取新待办事务数目
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        int IPendingOrder.GetNewPendingOrderNumber(string username)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select cast(count(*) as int) from " + TABLE_NAME_RECEIVER + " ");
            strSql.Append(" where UserName=@UserName and HasRead=@HasRead ");

            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
                    new SqlParameter("@HasRead", SqlDbType.Bit)};
            parameters[0].Value = username;
            parameters[1].Value = false;
            int count = 0;
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    count = rd.GetInt32(0);
                }
            }
            return count;
        }

        /// <summary>
        /// 获取当前用户的id号在lastTime后的新消息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="lastTime"></param>
        /// <returns></returns>
        PendingOrderInfo IPendingOrder.GetNewPendingOrder(string username, DateTime lastTime, out int count)
        {
            count = 0;
            PendingOrderInfo item = null;
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                StringBuilder strSql1 = new StringBuilder();
                strSql1.AppendFormat("select  top 1 {0}.* from {0} inner join {1} on {0}.ID={1}.ID ", TABLE_NAME_PENDINGORDER, TABLE_NAME_RECEIVER);
                strSql1.AppendFormat(" where {0}.UserName=@UserName and {0}.HasRead=@HasRead", TABLE_NAME_RECEIVER);
                strSql1.AppendFormat(" where  {0}.UserName=@UserName and {0}.HasRead=@HasRead and {1}.SendTime>'{2}' ",TABLE_NAME_RECEIVER, TABLE_NAME_PENDINGORDER, lastTime.ToString("yyyy-MM-dd HH:mm:ss"));

                SqlParameter[] parameters1 = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
                    new SqlParameter("@HasRead", SqlDbType.Bit)};
                parameters1[0].Value = username;
                parameters1[1].Value = false;


                using (SqlDataReader rd = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql1.ToString(), parameters1))
                {
                    if (rd.Read())
                    {
                        item = GetData(rd);
                    }
                }

                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("select cast(count(*) as int) from " + TABLE_NAME_RECEIVER + " ");
                strSql2.Append(" where UserName=@UserName and HasRead=@HasRead ");

                SqlParameter[] parameters2 = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
                    new SqlParameter("@HasRead", SqlDbType.Bit)};
                parameters2[0].Value = username;
                parameters2[1].Value = false;
                using (SqlDataReader rd = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql2.ToString(), parameters2))
                {
                    if (rd.Read())
                    {
                        count = rd.GetInt32(0);
                    }
                }
                //事务提交
                trans.Commit();
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

            return item;
        }

        /// <summary>
        /// 获取当前用户的最新消息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="lastTime"></param>
        /// <returns></returns>
        PendingOrderInfo IPendingOrder.GetNewPendingOrder(string username, out int count)
        {
            count = 0;
            PendingOrderInfo item = null;
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                StringBuilder strSql1 = new StringBuilder();
                strSql1.AppendFormat("select  top 1 {0}.* from {0} inner join {1} on {0}.ID={1}.ID ", TABLE_NAME_PENDINGORDER,TABLE_NAME_RECEIVER);
                strSql1.AppendFormat(" where {0}.UserName=@UserName and {0}.HasRead=@HasRead",TABLE_NAME_RECEIVER);
                strSql1.Append(" order by SendTime desc");

                SqlParameter[] parameters1 = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
                    new SqlParameter("@HasRead", SqlDbType.Bit)};
                parameters1[0].Value = username;
                parameters1[1].Value = false;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql1.ToString(), parameters1))
                {
                    if (rd.Read())
                    {
                        item = GetData(rd);
                    }
                }

                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("select cast(count(*) as int) from " + TABLE_NAME_RECEIVER + " ");
                strSql2.Append(" where UserName=@UserName and HasRead=@HasRead ");

                SqlParameter[] parameters2 = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
                    new SqlParameter("@HasRead", SqlDbType.Bit)};
                parameters2[0].Value = username;
                parameters2[1].Value = false;
                using (SqlDataReader rd = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql2.ToString(), parameters2))
                {
                    if (rd.Read())
                    {
                        count = rd.GetInt32(0);
                    }
                }
                //事务提交
                trans.Commit();
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
            return item;
        }


        //**********Modified by Xue 2011-6-27****************************************************************************************************
        /// <summary>
        /// 根据URL更新接收者
        /// </summary>
        /// <param name="url">处理URL地址</param>
        /// <param name="item">集合</param>
        public void UpdateReceiversByURL(string url, PendingOrderInfo item)
        {
            long id = GetPendingOrderIDByURL(url);

            //Update Address
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + TABLE_NAME_PENDINGORDER + " set ");
            strSql.Append("Address=@Address");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@Address", SqlDbType.NVarChar,200)};
            parameters[0].Value = id;
            parameters[1].Value = item.ReceiverAddress;

            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

            //Delete Receivers
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete " + TABLE_NAME_RECEIVER + " ");
            strSql2.Append(" where ID=@ID ");
            SqlParameter[] parameters2 = {
                new SqlParameter("@ID", SqlDbType.BigInt,8)};
            parameters2[0].Value = id;

            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql2.ToString(), parameters2);


            //Add New Receivers
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("insert into " + TABLE_NAME_RECEIVER + "(");
            strSql3.Append("ID,UserName,HasRead)");
            strSql3.Append(" values (");
            strSql3.Append("@ID,@UserName,@HasRead)");
            if (item.Receivers.Count != 0)
            {
                //针对每一个进行
                foreach (PendingOrderReceiverInfo mri in item.Receivers)
                {
                    SqlParameter[] parameters3 = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
					new SqlParameter("@HasRead", SqlDbType.Bit,1)};
                    parameters3[0].Value = id;
                    parameters3[1].Value = mri.UserName;
                    parameters3[2].Value = false;

                    SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql3.ToString(), parameters3);
                }
            }



        }
        //**********Modification Finished 2011-6-27**********************************************************************************************



        //**********Modified by Xue 2011-6-27****************************************************************************************************
        /// <summary>
        /// 根据URL获取ID值
        /// </summary>
        /// <param name="url">处理URL地址</param>
        public long GetPendingOrderIDByURL(string url)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID from " + TABLE_NAME_PENDINGORDER + " ");
            strSql.Append(" where url=@URL ");
            SqlParameter[] parameters = {
					new SqlParameter("@URL", SqlDbType.NVarChar,200)};
            parameters[0].Value = url;

            return (long)SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        //**********Modification Finished 2011-6-27**********************************************************************************************



        #endregion
    }
}
