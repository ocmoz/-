using System;
using System. Collections. Generic;
using System. Text;
using System. Data;
using System. Data. SqlClient;
using FM2E. SQLServerDAL. Utils;
using FM2E.IDAL.Message;
using FM2E.Model.Message;
using System.Collections;
using FM2E.Model.Exceptions;
using FM2E.Model.Utils;

namespace FM2E. SQLServerDAL. Message
{
    public class Message : IMessage
    {
        private const string TABLE_NAME_MESSAGE = "FM2E_Message";
        private const string TABLE_NAME_RECEIVER = "FM2E_MessageReceiver";

        #region IMessage 成员



        /// <summary>
        /// 添加新的消息
        /// </summary>
        /// <param name="item"></param>
        void IMessage.InsertMessage(MessageInfo item)
        {

            

            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先插入消息
                long id = InsertMessage(trans, item);

                //插入消息对象列表
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
        /// 插入消息主体
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="msg">消息</param>
        /// <returns>自增标识</returns>
        private long InsertMessage(SqlTransaction trans, MessageInfo msg)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + TABLE_NAME_MESSAGE + "(");
            strSql.Append("Type,SendWay,SendFrom,MessageTime,Title,Message,Attachment,Address)");
            strSql.Append(" values (");
            strSql.Append("@Type,@SendWay,@SendFrom,@MessageTime,@Title,@Message,@Attachment,@Address)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@Type", SqlDbType.TinyInt,1),
                    new SqlParameter("@SendWay",SqlDbType.TinyInt,1),
					new SqlParameter("@SendFrom", SqlDbType.VarChar,10),
					new SqlParameter("@MessageTime", SqlDbType.DateTime),
                    new SqlParameter("@Title", SqlDbType.NVarChar,50),
					new SqlParameter("@Message", SqlDbType.NVarChar,200),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,80),
                    new SqlParameter("@Address", SqlDbType.NVarChar,200)};
            parameters[0].Value = msg.Type;
            parameters[1].Value = msg.SendWay;
            parameters[2].Value = msg.SendFrom;
            parameters[3].Value = msg.MessageTime;
            parameters[4].Value = msg.Title;
            parameters[5].Value = msg.Message;
            parameters[6].Value = msg.Attachment;
            parameters[7].Value = msg.ReceiverAddress;

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
        /// 插入消息接收对象
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="msg">消息</param>
        /// <param name="id">消息ID</param>
        private void InsertReceivers(SqlTransaction trans, MessageInfo msg, long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + TABLE_NAME_RECEIVER + "(");
            strSql.Append("ID,UserName,HasRead)");
            strSql.Append(" values (");
            strSql.Append("@ID,@UserName,@HasRead)");
            if (msg.Receivers != null)
            {
                //针对每一个进行
                foreach (MessageReceiverInfo mri in msg.Receivers)
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
        /// 获取消息实体（不包含用户列表）
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private MessageInfo GetData(IDataReader rd)
        {
            MessageInfo item = new MessageInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["Type"]))
                item.Type = Convert.ToInt16(rd["Type"]);

            if (!Convert.IsDBNull(rd["SendWay"]))
                item.SendWay = Convert.ToInt16(rd["SendWay"]);

            if (!Convert.IsDBNull(rd["SendFrom"]))
                item.SendFrom = Convert.ToString(rd["SendFrom"]);

            if (!Convert.IsDBNull(rd["MessageTime"]))
                item.MessageTime = Convert.ToDateTime(rd["MessageTime"]);

            if (!Convert.IsDBNull(rd["Title"]))
                item.Title = Convert.ToString(rd["Title"]);

            if (!Convert.IsDBNull(rd["Message"]))
                item.Message = Convert.ToString(rd["Message"]);

            if (!Convert.IsDBNull(rd["Attachment"]))
                item.Attachment = Convert.ToString(rd["Attachment"]);

            if (!Convert.IsDBNull(rd["Address"]))
                item.ReceiverAddress = Convert.ToString(rd["Address"]);

            return item;
        }

        /// <summary>
        /// 获取一个接收者信息实体
        /// </summary>
        /// <returns>接收者信息</returns>
        private MessageReceiverInfo GetDataReceiver(IDataReader rd)
        {
            MessageReceiverInfo item = new MessageReceiverInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["UserName"]))
                item.UserName = Convert.ToString(rd["UserName"]);

            if (!Convert.IsDBNull(rd["HasRead"]))
                item.HasRead = Convert.ToBoolean(rd["HasRead"]);

            return item;
        }

        private MessageReceiverCombineInfo GetDataMessageReceiver(IDataReader rd)
        {
            MessageReceiverCombineInfo item = new MessageReceiverCombineInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["Type"]))
                item.Type = Convert.ToInt16(rd["Type"]);

            if (!Convert.IsDBNull(rd["SendWay"]))
                item.SendWay = Convert.ToInt16(rd["SendWay"]);

            if (!Convert.IsDBNull(rd["SendFrom"]))
                item.SendFrom = Convert.ToString(rd["SendFrom"]);

            if (!Convert.IsDBNull(rd["MessageTime"]))
                item.MessageTime = Convert.ToDateTime(rd["MessageTime"]);

            if (!Convert.IsDBNull(rd["Title"]))
                item.Title = Convert.ToString(rd["Title"]);

            if (!Convert.IsDBNull(rd["Message"]))
                item.Message = Convert.ToString(rd["Message"]);

            if (!Convert.IsDBNull(rd["Attachment"]))
                item.Attachment = Convert.ToString(rd["Attachment"]);

            if (!Convert.IsDBNull(rd["Address"]))
                item.ReceiverAddress = Convert.ToString(rd["Address"]);

            if (!Convert.IsDBNull(rd["UserName"]))
                item.UserName = Convert.ToString(rd["UserName"]);

            if (!Convert.IsDBNull(rd["HasRead"]))
                item.HasRead = Convert.ToBoolean(rd["HasRead"]);

            return item;
        }

        /// <summary>
        /// 根据用户名获取消息列表，不包含用户列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>消息列表</returns>
        IList IMessage.GetMessageListByReceiver(int pageIndex, int pageSize, out int recordCount,string username)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME_MESSAGE + " INNER JOIN " +
                                 TABLE_NAME_RECEIVER + " ON " +
                                 TABLE_NAME_MESSAGE + ".ID = " + TABLE_NAME_RECEIVER + ".ID ";
                qp.ReturnFields = string.Format(
                    TABLE_NAME_MESSAGE+".ID"
                    + "," + TABLE_NAME_MESSAGE +".Type"
                    + "," + TABLE_NAME_MESSAGE + ".SendWay"
                    + "," + TABLE_NAME_MESSAGE + ".SendFrom"
                    + "," + TABLE_NAME_MESSAGE + ".MessageTime"
                    + "," + TABLE_NAME_MESSAGE + ".Title"
                    + "," + TABLE_NAME_MESSAGE + ".Message"
                    + "," + TABLE_NAME_MESSAGE + ".Attachment"
                    + "," + TABLE_NAME_MESSAGE + ".Address"
                    + "," + TABLE_NAME_RECEIVER + ".UserName"
                    + "," + TABLE_NAME_RECEIVER + ".HasRead"
                    + ",{0} as HasReadSort,{1} as MessageTimeSort", TABLE_NAME_RECEIVER + ".HasRead", TABLE_NAME_MESSAGE + ".MessageTime");
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_NAME_RECEIVER + ".[UserName]='" + username + "'" +
                    " and " + TABLE_NAME_MESSAGE + ".[SendWay]=0";
                qp.OrderBy = string.Format("order by {0} ASC,{1} DESC", "HasReadSort", "MessageTimeSort");
                return SQLHelper.GetObjectList(this.GetDataMessageReceiver, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }

        /// <summary>
        /// 根据接收用户名和指定时间区段查询
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>消息列表</returns>
        IList IMessage.GetMessageListByReceiver(int pageIndex, int pageSize, out int recordCount, string username, DateTime start)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME_MESSAGE + " INNER JOIN " +
                                 TABLE_NAME_RECEIVER + " ON " +
                                 TABLE_NAME_MESSAGE + ".ID = " + TABLE_NAME_RECEIVER + ".ID ";
                qp.ReturnFields = string.Format(
                    TABLE_NAME_MESSAGE + ".ID"
                    + "," + TABLE_NAME_MESSAGE + ".Type"
                    + "," + TABLE_NAME_MESSAGE + ".SendWay"
                    + "," + TABLE_NAME_MESSAGE + ".SendFrom"
                    + "," + TABLE_NAME_MESSAGE + ".MessageTime"
                    + "," + TABLE_NAME_MESSAGE + ".Title"
                    + "," + TABLE_NAME_MESSAGE + ".Message"
                    + "," + TABLE_NAME_MESSAGE + ".Attachment"
                    + "," + TABLE_NAME_MESSAGE + ".Address"
                    + "," + TABLE_NAME_RECEIVER + ".UserName"
                    + "," + TABLE_NAME_RECEIVER + ".HasRead"
                    +",{0} as HasReadSort,{1} as MessageTimeSort", TABLE_NAME_RECEIVER + ".HasRead", TABLE_NAME_MESSAGE + ".MessageTime");
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_NAME_RECEIVER + ".[UserName]='" + username + "'" +
                    " and " + TABLE_NAME_MESSAGE + ".MessageTime>='" + start.ToShortDateString() + "'" +
                    " and " + TABLE_NAME_MESSAGE + ".[SendWay]=0";
                qp.OrderBy = string.Format("order by {0} ASC,{1} DESC", "HasReadSort", "MessageTimeSort");
                return SQLHelper.GetObjectList(this.GetDataMessageReceiver, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }


        /// <summary>
        /// 根据接收用户名和指定时间区段查询
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>消息列表</returns>
        IList IMessage.GetMessageListByReceiver(int pageIndex, int pageSize, out int recordCount, string username, DateTime start, DateTime end)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME_MESSAGE + " INNER JOIN " +
                                 TABLE_NAME_RECEIVER + " ON " +
                                 TABLE_NAME_MESSAGE + ".ID = " + TABLE_NAME_RECEIVER + ".ID ";
                qp.ReturnFields = string.Format(
                    TABLE_NAME_MESSAGE + ".ID"
                    + "," + TABLE_NAME_MESSAGE + ".Type"
                    + "," + TABLE_NAME_MESSAGE + ".SendWay"
                    + "," + TABLE_NAME_MESSAGE + ".SendFrom"
                    + "," + TABLE_NAME_MESSAGE + ".MessageTime"
                    + "," + TABLE_NAME_MESSAGE + ".Title"
                    + "," + TABLE_NAME_MESSAGE + ".Message"
                    + "," + TABLE_NAME_MESSAGE + ".Attachment"
                    + "," + TABLE_NAME_MESSAGE + ".Address"
                    + "," + TABLE_NAME_RECEIVER + ".UserName"
                    + "," + TABLE_NAME_RECEIVER + ".HasRead"
                    + ",{0} as HasReadSort,{1} as MessageTimeSort", TABLE_NAME_RECEIVER + ".HasRead", TABLE_NAME_MESSAGE + ".MessageTime");
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_NAME_RECEIVER + ".[UserName]='" + username + "'" +
                    " and " + TABLE_NAME_MESSAGE + ".MessageTime>='" + start.ToShortDateString() + "'" +
                    " and " + TABLE_NAME_MESSAGE + ".MessageTime<='" + start.ToShortDateString() + "'" +
                    " and " + TABLE_NAME_MESSAGE + ".[SendWay]=0";
                qp.OrderBy = string.Format("order by {0} ASC,{1} DESC", "HasReadSort", "MessageTimeSort");
                return SQLHelper.GetObjectList(this.GetDataMessageReceiver, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }

        /// <summary>
        /// 获取所有消息
        /// </summary>
        /// <returns>消息列表</returns>
        IList IMessage.GetAllMessage(int pageIndex, int pageSize, out int recordCount)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME_MESSAGE ;
                qp.ReturnFields = string.Format(
                    TABLE_NAME_MESSAGE + ".ID"
                    + "," + TABLE_NAME_MESSAGE + ".Type"
                    + "," + TABLE_NAME_MESSAGE + ".SendWay"
                    + "," + TABLE_NAME_MESSAGE + ".SendFrom"
                    + "," + TABLE_NAME_MESSAGE + ".MessageTime"
                    + "," + TABLE_NAME_MESSAGE + ".Title"
                    + "," + TABLE_NAME_MESSAGE + ".Message"
                    + "," + TABLE_NAME_MESSAGE + ".Attachment"
                    + "," + TABLE_NAME_MESSAGE + ".Address"
                    + ",{0} as MessageTimeSort", TABLE_NAME_MESSAGE + ".MessageTime");
                qp.PageIndex = pageIndex;
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.OrderBy = string.Format("order by {0} DESC", "MessageTimeSort");
                return SQLHelper.GetObjectList(this.GetData, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }

        /// <summary>
        /// 获取指定时间到现在内消息
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <returns>消息列表</returns>
        IList IMessage.GetAllMessage(int pageIndex, int pageSize, out int recordCount, DateTime start)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME_MESSAGE;
                qp.ReturnFields = string.Format(
                    TABLE_NAME_MESSAGE + ".ID"
                    + "," + TABLE_NAME_MESSAGE + ".Type"
                    + "," + TABLE_NAME_MESSAGE + ".SendWay"
                    + "," + TABLE_NAME_MESSAGE + ".SendFrom"
                    + "," + TABLE_NAME_MESSAGE + ".MessageTime"
                    + "," + TABLE_NAME_MESSAGE + ".Title"
                    + "," + TABLE_NAME_MESSAGE + ".Message"
                    + "," + TABLE_NAME_MESSAGE + ".Attachment"
                    + "," + TABLE_NAME_MESSAGE + ".Address"
                    + ",{0} as MessageTimeSort", TABLE_NAME_MESSAGE + ".MessageTime");
                qp.PageIndex = pageIndex;
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_NAME_MESSAGE + ".MessageTime>='" + start.ToShortDateString() + "'";
                qp.OrderBy = string.Format("order by {0} DESC", "MessageTimeSort");
                return SQLHelper.GetObjectList(this.GetData, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }

        /// <summary>
        /// 获取指定时间段内消息
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>消息列表</returns>
        IList IMessage.GetAllMessage(int pageIndex, int pageSize, out int recordCount,DateTime start, DateTime end)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME_MESSAGE;
                qp.ReturnFields = string.Format(
                    TABLE_NAME_MESSAGE + ".ID"
                    + "," + TABLE_NAME_MESSAGE + ".Type"
                    + "," + TABLE_NAME_MESSAGE + ".SendWay"
                    + "," + TABLE_NAME_MESSAGE + ".SendFrom"
                    + "," + TABLE_NAME_MESSAGE + ".MessageTime"
                    + "," + TABLE_NAME_MESSAGE + ".Title"
                    + "," + TABLE_NAME_MESSAGE + ".Message"
                    + "," + TABLE_NAME_MESSAGE + ".Attachment"
                    + "," + TABLE_NAME_MESSAGE + ".Address"
                    + ",{0} as MessageTimeSort", TABLE_NAME_MESSAGE + ".MessageTime");
                qp.PageIndex = pageIndex;
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_NAME_MESSAGE + ".MessageTime>='" + start.ToShortDateString() + "'" +
                    " and " + TABLE_NAME_MESSAGE + ".MessageTime<='" + start.ToShortDateString() + "'";
                qp.OrderBy = string.Format("order by {0} DESC", "MessageTimeSort");
                return SQLHelper.GetObjectList(this.GetData, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }

        /// <summary>
        /// 根据指定的发送人获取消息
        /// </summary>
        /// <param name="sender">发送人ID</param>
        /// <returns>消息列表</returns>
        IList IMessage.GetMessageListBySender(int pageIndex, int pageSize, out int recordCount, string sender)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME_MESSAGE;
                qp.ReturnFields = string.Format(
                    TABLE_NAME_MESSAGE + ".ID"
                    + "," + TABLE_NAME_MESSAGE + ".Type"
                    + "," + TABLE_NAME_MESSAGE + ".SendWay"
                    + "," + TABLE_NAME_MESSAGE + ".SendFrom"
                    + "," + TABLE_NAME_MESSAGE + ".MessageTime"
                    + "," + TABLE_NAME_MESSAGE + ".Title"
                    + "," + TABLE_NAME_MESSAGE + ".Message"
                    + "," + TABLE_NAME_MESSAGE + ".Attachment"
                    + "," + TABLE_NAME_MESSAGE + ".Address"
                    + ",{0} as MessageTimeSort", TABLE_NAME_MESSAGE + ".MessageTime");
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_NAME_MESSAGE + ".[SendFrom]='" + sender + "'";
                qp.OrderBy = string.Format("order by {0} DESC", "MessageTimeSort");
                return SQLHelper.GetObjectList(this.GetData, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }


        /// <summary>
        /// 根据指定发送人，获取指定时间开始到现在
        /// </summary>
        /// <param name="sender">发送人</param>
        /// <param name="start">开始时间</param>
        /// <returns>消息列表</returns>
        IList IMessage.GetMessageListBySender(int pageIndex, int pageSize, out int recordCount, string sender, DateTime start)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME_MESSAGE;
                qp.ReturnFields = string.Format(

                    TABLE_NAME_MESSAGE + ".ID"
                    + "," + TABLE_NAME_MESSAGE + ".Type"
                    + "," + TABLE_NAME_MESSAGE + ".SendWay"
                    + "," + TABLE_NAME_MESSAGE + ".SendFrom"
                    + "," + TABLE_NAME_MESSAGE + ".MessageTime"
                    + "," + TABLE_NAME_MESSAGE + ".Title"
                    + "," + TABLE_NAME_MESSAGE + ".Message"
                    + "," + TABLE_NAME_MESSAGE + ".Attachment"
                    + "," + TABLE_NAME_MESSAGE + ".Address"
                    + ",{0} as MessageTimeSort", TABLE_NAME_MESSAGE + ".MessageTime");
                qp.PageIndex = pageIndex;
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_NAME_MESSAGE + ".[SendFrom]='" + sender + "'" +
                    " and " + TABLE_NAME_MESSAGE + ".MessageTime>='" + start.ToShortDateString() + "'" ;
                qp.OrderBy = string.Format("order by {0} DESC", "MessageTimeSort");
                return SQLHelper.GetObjectList(this.GetData, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }

        /// <summary>
        /// 根据指定发送人，获取指定时间段内消息
        /// </summary>
        /// <param name="sender">发送人</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>消息列表</returns>
        IList IMessage.GetMessageListBySender(int pageIndex, int pageSize, out int recordCount,string sender, DateTime start, DateTime end)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = TABLE_NAME_MESSAGE;
                qp.ReturnFields = string.Format(
                    TABLE_NAME_MESSAGE + ".ID"
                    + "," + TABLE_NAME_MESSAGE + ".Type"
                    + "," + TABLE_NAME_MESSAGE + ".SendWay"
                    + "," + TABLE_NAME_MESSAGE + ".SendFrom"
                    + "," + TABLE_NAME_MESSAGE + ".MessageTime"
                    + "," + TABLE_NAME_MESSAGE + ".Title"
                    + "," + TABLE_NAME_MESSAGE + ".Message"
                    + "," + TABLE_NAME_MESSAGE + ".Attachment"
                    + "," + TABLE_NAME_MESSAGE + ".Address"
                    + ",{0} as MessageTimeSort", TABLE_NAME_MESSAGE + ".MessageTime");
                qp.PageIndex = pageIndex;
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_NAME_MESSAGE + ".[SendFrom]='" + sender + "'" +
                    " and "+ TABLE_NAME_MESSAGE + ".MessageTime>='" + start.ToShortDateString() + "'" +
                    " and " + TABLE_NAME_MESSAGE + ".MessageTime<='" + start.ToShortDateString() + "'";
                qp.OrderBy = string.Format("order by {0} DESC", "MessageTimeSort");
                return SQLHelper.GetObjectList(this.GetData, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }

        /// <summary>
        /// 获取消息实体，包含所有接收人
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <returns>消息实体</returns>
        MessageInfo IMessage.GetMessage(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Type,SendWay,SendFrom,MessageTime,Title,Message,Attachment,Address from " + TABLE_NAME_MESSAGE + " ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = id;

            MessageInfo msg = new MessageInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    msg = GetData(rd);
                    break;
                }
            }
            //获取消息的接收人
            List<MessageReceiverInfo> receivers = new List<MessageReceiverInfo>();

            strSql = new StringBuilder();
            strSql.Append("select ID,UserName,HasRead from "+ TABLE_NAME_RECEIVER +" ");
            strSql.Append(" where ID=@ID  ");

            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    MessageReceiverInfo mri = GetDataReceiver(rd);
                    receivers.Add(mri);
                }
            }
            msg.Receivers = receivers;
            return msg;
        }

        /// <summary>
        /// 标记已读
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="username">用户名</param>
        void IMessage.MarkRead(long id, string username)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update "+ TABLE_NAME_RECEIVER +" set ");
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

        /// <summary>
        /// 删除整条消息
        /// </summary>
        /// <param name="id">消息ID</param>
        void IMessage.DeleteMessage(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete " + TABLE_NAME_MESSAGE + " ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = id;

            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除消息的接收用户
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="username">接收用户</param>
        void IMessage.DeleteMessage(long id, string username)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete "+ TABLE_NAME_RECEIVER +" ");
            strSql.Append(" where ID=@ID and UserName=@UserName ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt),
					new SqlParameter("@UserName", SqlDbType.VarChar,50)};
            parameters[0].Value = id;
            parameters[1].Value = username;

            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 获取新消息数目
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        int IMessage.GetNewMessageNumber(string username)
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

        #endregion
    }
}
