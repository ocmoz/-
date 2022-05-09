using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.PendingOrder;
using FM2E.Model.PendingOrder;
using System.Collections;
using FM2E.BLL.System;
using FM2E.Model.System;
namespace FM2E.BLL.PendingOrder
{
    public class PendingOrder
    {
        private readonly User userBll = new User();
        /// <summary>
        /// 发送一条新待办事务
        /// </summary>
        /// <param name="item">待办事务实体，包含待办事务接收者列表</param>
        public void SendPendingOrder( PendingOrderInfo item )
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            dal.InsertPendingOrder(item);
        }

        /// <summary>
        /// 获取指定的待办事务
        /// </summary>
        /// <param name="id">待办事务ID</param>
        /// <returns>需要获取的待办事务</returns>
        public PendingOrderInfo GetPendingOrder(long id)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            PendingOrderInfo m= dal.GetPendingOrder(id);
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
        /// 获取指定的待办事务
        /// </summary>
        /// <param name="id">待办事务ID</param>
        /// <param name="username">已读的用户名</param>
        public void MarkRead(long id, string username)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            dal.MarkRead(id, username);
        }


        //**********Modified by Xue 2011-6-27****************************************************************************************************
        /// <summary>
        /// 根据URL标记为已处理
        /// </summary>
        /// <param name="url">处理URL地址</param>
        public void MarkReadByURL(string url)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            dal.MarkReadByURL(url);
        }
        //**********Modification Finished 2011-6-27**********************************************************************************************


        /// <summary>
        /// 获取指定的待办事务，并且标记为已读
        /// </summary>
        /// <param name="id">待办事务ID</param>
        /// <returns>需要获取的待办事务</returns>
        public PendingOrderInfo GetPendingOrderMarkRead(long id, string username)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            PendingOrderInfo msg = dal.GetPendingOrder(id);
            dal.MarkRead(id, username);

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
        /// 当前用户删除发送给自身的待办事务
        /// </summary>
        /// <param name="id">待办事务ID</param>
        /// <param name="username">接收用户名</param>
        public void DeletePendingOrder(long id, string username)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            dal.DeletePendingOrder(id, username);
        }

        /// <summary>
        /// 管理员删除待办事务，级联删除接收者列表信息
        /// </summary>
        /// <param name="id">待办事务ID</param>
        public void DeletePendingOrder(long id)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            dal.DeletePendingOrder(id);
        }

        /// <summary>
        /// 获取新待办事务的数目
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int GetNewPendingOrderNumber(string username)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            return dal.GetNewPendingOrderNumber(username);
        }

        /// <summary>
        /// 获取指定用户所拥有的待办事务列表，按照未读先排，已读后排，再按时间倒序排序
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>先未读，后已读，再按时间倒序排序的待办事务列表</returns>
        public IList GetPendingOrderListByReceiver(int pageIndex, int pageSize, out int recordCount, String username)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            IList list = dal.GetPendingOrderListByReceiver(pageIndex, pageSize, out recordCount, username);

            foreach (PendingOrderInfo m in list)
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
        /// 获取指定用户所拥有的已办事务列表，按照未读先排，已读后排，再按时间倒序排序
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>先未读，后已读，再按时间倒序排序的已办事务列表</returns>
        public IList GetDoneOrderListByReceiver(int pageIndex, int pageSize, out int recordCount, String username)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            IList list = dal.GetDoneOrderListByReceiver(pageIndex, pageSize, out recordCount, username);

            foreach (PendingOrderInfo m in list)
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
        /// 获取指定用户从指定时间开始到现时所拥有的待办事务列表，按照未读先排，已读后排，再按时间倒序排序
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>先未读，后已读，再按时间倒序排序的待办事务列表</returns>
        public IList GetPendingOrderListByReceiver(int pageIndex, int pageSize, out int recordCount, String username, DateTime start)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            IList list =  dal.GetPendingOrderListByReceiver(pageIndex, pageSize, out recordCount, username,start);
            foreach (PendingOrderInfo m in list)
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
        /// 获取指定用户从指定时间段中所拥有的待办事务列表，按照未读先排，已读后排，再按时间倒序排序
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>先未读，后已读，再按时间倒序排序的待办事务列表</returns>
        public IList GetPendingOrderListByReceiver(int pageIndex, int pageSize, out int recordCount, String username, DateTime start, DateTime end)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            IList list = dal.GetPendingOrderListByReceiver(pageIndex, pageSize, out recordCount, username, start, end);

            foreach (PendingOrderInfo m in list)
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
        /// 获取系统中所有的待办事务
        /// </summary>
        /// <returns>按照时间倒序排序的待办事务列表</returns>
        public IList GetAllPendingOrder(int pageIndex, int pageSize, out int recordCount)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            IList list =  dal.GetAllPendingOrder(pageIndex, pageSize, out recordCount);
            foreach (PendingOrderInfo m in list)
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
        /// 获取系统中指定开始时间到现在的所有的待办事务
        /// </summary>
        /// <returns>按照时间倒序排序的待办事务列表</returns>
        public IList GetAllPendingOrder(int pageIndex, int pageSize, out int recordCount,DateTime start)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            IList list = dal.GetAllPendingOrder(pageIndex, pageSize, out recordCount, start);
            foreach (PendingOrderInfo m in list)
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
        /// 获取系统中指定时间段内的所有的待办事务
        /// </summary>
        /// <returns>按照时间倒序排序的待办事务列表</returns>
        public IList GetAllPendingOrder(int pageIndex, int pageSize, out int recordCount, DateTime start, DateTime end)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            IList list =  dal.GetAllPendingOrder(pageIndex, pageSize, out recordCount, start, end);
            foreach (PendingOrderInfo m in list)
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
        /// 获取指定用户发送过的所有待办事务
        /// </summary>
        /// <returns>按照时间倒序排序的待办事务列表</returns>
        public IList GetPendingOrderListBySender(int pageIndex, int pageSize, out int recordCount, string sender)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            IList list = dal.GetPendingOrderListBySender(pageIndex, pageSize, out recordCount, sender);
            foreach (PendingOrderInfo m in list)
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
        /// 获取指定用户从指定时间开始到当前时间发送过的所有待办事务
        /// </summary>
        /// <returns>按照时间倒序排序的待办事务列表</returns>
        public IList GetPendingOrderListBySender(int pageIndex, int pageSize, out int recordCount, string sender, DateTime start)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            IList list = dal.GetPendingOrderListBySender(pageIndex, pageSize, out recordCount, sender, start);
            foreach (PendingOrderInfo m in list)
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
        /// 获取指定用户从指定时间段内发送过的所有待办事务
        /// </summary>
        /// <returns>按照时间倒序排序的待办事务列表</returns>
        public IList GetPendingOrderListBySender(int pageIndex, int pageSize, out int recordCount,string sender, DateTime start,DateTime end)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            IList list = dal.GetPendingOrderListBySender(pageIndex, pageSize, out recordCount, sender, start, end);
            foreach (PendingOrderInfo m in list)
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
        /// 获取当前用户的id号在lastTime后的新消息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="lastTime"></param>
        /// <returns></returns>
        public PendingOrderInfo GetNewPendingOrder(string username, DateTime lastTime, out int count)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            PendingOrderInfo item=dal.GetNewPendingOrder(username, lastTime,out count);

            if (item != null)
            {
                UserInfo u = userBll.GetUser(item.SendFrom);
                if (u != null)
                {
                    item.SenderPersonName = u.PersonName;
                }
                else
                    item.SenderPersonName = "";
            }
            return item;
        }
        /// <summary>
        /// 获取当前用户的最新消息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="lastTime"></param>
        /// <returns></returns>
        public PendingOrderInfo GetNewPendingOrder(string username, out int count)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            PendingOrderInfo item = dal.GetNewPendingOrder(username,out count);

            if (item != null)
            {
                UserInfo u = userBll.GetUser(item.SendFrom);
                if (u != null)
                {
                    item.SenderPersonName = u.PersonName;
                }
                else
                    item.SenderPersonName = "";
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
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            dal.UpdateReceiversByURL(url, item);
        }
        //**********Modification Finished 2011-6-27**********************************************************************************************

        //**********Modified by Xue 2011-6-27****************************************************************************************************
        /// <summary>
        /// 根据URL获取ID值
        /// </summary>
        /// <param name="url">处理URL地址</param>
        public long GetPendingOrderIDByURL(string url)
        {
            IPendingOrder dal = FM2E.DALFactory.PendingOrderAccess.CreatePendingOrder();
            return dal.GetPendingOrderIDByURL(url);
        }
        //**********Modification Finished 2011-6-27**********************************************************************************************


    }
}