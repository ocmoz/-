using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Message;
using FM2E.Model.Message;
using System.Collections;
using FM2E.BLL.System;
using FM2E.Model.System;
namespace FM2E.BLL.Message
{
    public class Message
    {
        /// <summary>
        /// 发送一条新消息
        /// </summary>
        /// <param name="item">消息实体，包含消息接收者列表</param>
        public void SendMessage( MessageInfo item )
        {
            IMessage dal = FM2E.DALFactory.MessageAccess.CreateMessage();
            dal.InsertMessage(item);
        }

        /// <summary>
        /// 获取指定的消息
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <returns>需要获取的消息</returns>
        public MessageInfo GetMessage(long id)
        {
            IMessage dal = FM2E.DALFactory.MessageAccess.CreateMessage();
            MessageInfo m= dal.GetMessage(id);
            User userBll = new User();
            UserInfo u = userBll.GetUser(m.SendFrom);
            if (u != null)
            {
                m.SenderPersonName = u.PersonName;
            }
            else
                m.SenderPersonName = "";
            return m;
        }

        /// <summary>
        /// 标注指定消息已读
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="username">已读的用户名</param>
        public void MarkRead(long id, string username)
        {
            IMessage dal = FM2E.DALFactory.MessageAccess.CreateMessage();
            dal.MarkRead(id, username);
        }

        /// <summary>
        /// 获取指定的消息，并且标记为已读
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <returns>需要获取的消息</returns>
        public MessageInfo GetMessageMarkRead(long id, string username)
        {
            IMessage dal = FM2E.DALFactory.MessageAccess.CreateMessage();
            MessageInfo msg = dal.GetMessage(id);
            dal.MarkRead(id, username);

            User userBll = new User();
            UserInfo u = userBll.GetUser(msg.SendFrom);
            if (u != null)
            {
                msg.SenderPersonName = u.PersonName;
            }
            else
                msg.SenderPersonName = "";

            return msg;
        }

        /// <summary>
        /// 当前用户删除发送给自身的消息
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="username">接收用户名</param>
        public void DeleteMessage(long id, string username)
        {
            IMessage dal = FM2E.DALFactory.MessageAccess.CreateMessage();
            dal.DeleteMessage(id, username);
        }

        /// <summary>
        /// 管理员删除消息，级联删除接收者列表信息
        /// </summary>
        /// <param name="id">消息ID</param>
        public void DeleteMessage(long id)
        {
            IMessage dal = FM2E.DALFactory.MessageAccess.CreateMessage();
            dal.DeleteMessage(id);
        }

        /// <summary>
        /// 获取新消息的数目
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int GetNewMessageNumber(string username)
        {
            IMessage dal = FM2E.DALFactory.MessageAccess.CreateMessage();
            return dal.GetNewMessageNumber(username);
        }

        /// <summary>
        /// 获取指定用户所拥有的消息列表，按照未读先排，已读后排，再按时间倒序排序
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>先未读，后已读，再按时间倒序排序的消息列表</returns>
        public IList GetMessageListByReceiver(int pageIndex, int pageSize, out int recordCount, String username)
        {
            IMessage dal = FM2E.DALFactory.MessageAccess.CreateMessage();
            IList list = dal.GetMessageListByReceiver(pageIndex, pageSize, out recordCount, username);

            User userBll = new User();
            foreach (MessageInfo m in list)
            {
                UserInfo u = userBll.GetUser(m.SendFrom);
                if (u != null)
                {
                    m.SenderPersonName = u.PersonName;
                }
                else
                    m.SenderPersonName = "";
            }
            return list;
        }

        /// <summary>
        /// 获取指定用户从指定时间开始到现时所拥有的消息列表，按照未读先排，已读后排，再按时间倒序排序
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>先未读，后已读，再按时间倒序排序的消息列表</returns>
        public IList GetMessageListByReceiver(int pageIndex, int pageSize, out int recordCount, String username, DateTime start)
        {
            IMessage dal = FM2E.DALFactory.MessageAccess.CreateMessage();
            IList list =  dal.GetMessageListByReceiver(pageIndex, pageSize, out recordCount, username,start);
            User userBll = new User();
            foreach (MessageInfo m in list)
            {
                UserInfo u = userBll.GetUser(m.SendFrom);
                if (u != null)
                {
                    m.SenderPersonName = u.PersonName;
                }
                else
                    m.SenderPersonName = "";
            }
            return list;
        }

        /// <summary>
        /// 获取指定用户从指定时间段中所拥有的消息列表，按照未读先排，已读后排，再按时间倒序排序
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>先未读，后已读，再按时间倒序排序的消息列表</returns>
        public IList GetMessageListByReceiver(int pageIndex, int pageSize, out int recordCount, String username, DateTime start, DateTime end)
        {
            IMessage dal = FM2E.DALFactory.MessageAccess.CreateMessage();
            IList list = dal.GetMessageListByReceiver(pageIndex, pageSize, out recordCount, username, start, end);

            User userBll = new User();
            foreach (MessageInfo m in list)
            {
                UserInfo u = userBll.GetUser(m.SendFrom);
                if (u != null)
                {
                    m.SenderPersonName = u.PersonName;
                }
                else
                    m.SenderPersonName = "";
            }
            return list;
        }

        /// <summary>
        /// 获取系统中所有的消息
        /// </summary>
        /// <returns>按照时间倒序排序的消息列表</returns>
        public IList GetAllMessage(int pageIndex, int pageSize, out int recordCount)
        {
            IMessage dal = FM2E.DALFactory.MessageAccess.CreateMessage();
            IList list =  dal.GetAllMessage(pageIndex, pageSize, out recordCount);
            User userBll = new User();
            foreach (MessageInfo m in list)
            {
                UserInfo u = userBll.GetUser(m.SendFrom);
                if (u != null)
                {
                    m.SenderPersonName = u.PersonName;
                }
                else
                    m.SenderPersonName = "";
            }
            return list;
        }


        /// <summary>
        /// 获取系统中指定开始时间到现在的所有的消息
        /// </summary>
        /// <returns>按照时间倒序排序的消息列表</returns>
        public IList GetAllMessage(int pageIndex, int pageSize, out int recordCount,DateTime start)
        {
            IMessage dal = FM2E.DALFactory.MessageAccess.CreateMessage();
            IList list = dal.GetAllMessage(pageIndex, pageSize, out recordCount, start);
            User userBll = new User();
            foreach (MessageInfo m in list)
            {
                UserInfo u = userBll.GetUser(m.SendFrom);
                if (u != null)
                {
                    m.SenderPersonName = u.PersonName;
                }
                else
                    m.SenderPersonName = "";
            }
            return list;
        }

        /// <summary>
        /// 获取系统中指定时间段内的所有的消息
        /// </summary>
        /// <returns>按照时间倒序排序的消息列表</returns>
        public IList GetAllMessage(int pageIndex, int pageSize, out int recordCount, DateTime start, DateTime end)
        {
            IMessage dal = FM2E.DALFactory.MessageAccess.CreateMessage();
            IList list =  dal.GetAllMessage(pageIndex, pageSize, out recordCount, start, end);
            User userBll = new User();
            foreach (MessageInfo m in list)
            {
                UserInfo u = userBll.GetUser(m.SendFrom);
                if (u != null)
                {
                    m.SenderPersonName = u.PersonName;
                }
                else
                    m.SenderPersonName = "";
            }
            return list;
        }

        /// <summary>
        /// 获取指定用户发送过的所有消息
        /// </summary>
        /// <returns>按照时间倒序排序的消息列表</returns>
        public IList GetMessageListBySender(int pageIndex, int pageSize, out int recordCount, string sender)
        {
            IMessage dal = FM2E.DALFactory.MessageAccess.CreateMessage();
            IList list = dal.GetMessageListBySender(pageIndex, pageSize, out recordCount, sender);
            User userBll = new User();
            foreach (MessageInfo m in list)
            {
                UserInfo u = userBll.GetUser(m.SendFrom);
                if (u != null)
                {
                    m.SenderPersonName = u.PersonName;
                }
                else
                    m.SenderPersonName = "";
            }
            return list;
        }

        /// <summary>
        /// 获取指定用户从指定时间开始到当前时间发送过的所有消息
        /// </summary>
        /// <returns>按照时间倒序排序的消息列表</returns>
        public IList GetMessageListBySender(int pageIndex, int pageSize, out int recordCount, string sender, DateTime start)
        {
            IMessage dal = FM2E.DALFactory.MessageAccess.CreateMessage();
            IList list = dal.GetMessageListBySender(pageIndex, pageSize, out recordCount, sender, start);
            User userBll = new User();
            foreach (MessageInfo m in list)
            {
                UserInfo u = userBll.GetUser(m.SendFrom);
                if (u != null)
                {
                    m.SenderPersonName = u.PersonName;
                }
                else
                    m.SenderPersonName = "";
            }
            return list;
        }

        /// <summary>
        /// 获取指定用户从指定时间段内发送过的所有消息
        /// </summary>
        /// <returns>按照时间倒序排序的消息列表</returns>
        public IList GetMessageListBySender(int pageIndex, int pageSize, out int recordCount,string sender, DateTime start,DateTime end)
        {
            IMessage dal = FM2E.DALFactory.MessageAccess.CreateMessage();
            IList list = dal.GetMessageListBySender(pageIndex, pageSize, out recordCount, sender, start, end);
            User userBll = new User();
            foreach (MessageInfo m in list)
            {
                UserInfo u = userBll.GetUser(m.SendFrom);
                if (u != null)
                {
                    m.SenderPersonName = u.PersonName;
                }
                else
                    m.SenderPersonName = "";
            }
            return list;
        }


    }
}