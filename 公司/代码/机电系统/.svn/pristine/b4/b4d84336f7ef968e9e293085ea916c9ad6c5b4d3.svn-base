using System;
using System. Collections. Generic;
using System. Text;
using FM2E. Model. PendingOrder;
using System.Collections;

namespace FM2E. IDAL. PendingOrder
{
    public interface IPendingOrder
    {
        /// <summary>
        /// 发送一条新待办事务
        /// </summary>
        /// <param name="item">待办事务实体，包含待办事务接收者列表</param>
        void InsertPendingOrder(PendingOrderInfo item);

        /// <summary>
        /// 获取指定用户所拥有的待办事务列表，按照未读先排，已读后排，再按时间倒序排序
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>先未读，后已读，再按时间倒序排序的待办事务列表</returns>
        IList GetPendingOrderListByReceiver(int pageIndex, int pageSize, out int recordCount,String username);

        /// <summary>
        /// 获取指定用户所拥有的已办事务列表，按照未读先排，已读后排，再按时间倒序排序
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>先未读，后已读，再按时间倒序排序的已办事务列表</returns>
        IList GetDoneOrderListByReceiver(int pageIndex, int pageSize, out int recordCount, String username);

        /// <summary>
        /// 获取指定用户从指定时间开始到现时所拥有的待办事务列表，按照未读先排，已读后排，再按时间倒序排序
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>先未读，后已读，再按时间倒序排序的待办事务列表</returns>
        IList GetPendingOrderListByReceiver(int pageIndex, int pageSize, out int recordCount, String username, DateTime start);

        /// <summary>
        /// 获取指定用户从指定时间段中所拥有的待办事务列表，按照未读先排，已读后排，再按时间倒序排序
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>先未读，后已读，再按时间倒序排序的待办事务列表</returns>
        IList GetPendingOrderListByReceiver(int pageIndex, int pageSize, out int recordCount, String username, DateTime start, DateTime end);

        /// <summary>
        /// 获取系统中所有的待办事务
        /// </summary>
        /// <returns>按照时间倒序排序的待办事务列表</returns>
        IList GetAllPendingOrder(int pageIndex, int pageSize, out int recordCount);

        /// <summary>
        /// 获取系统中指定时间开始到现时的所有的待办事务
        /// </summary>
        /// <returns>按照时间倒序排序的待办事务列表</returns>
        IList GetAllPendingOrder(int pageIndex, int pageSize, out int recordCount, DateTime start);

        /// <summary>
        /// 获取系统中指定时间段内的所有的待办事务
        /// </summary>
        /// <returns>按照时间倒序排序的待办事务列表</returns>
        IList GetAllPendingOrder(int pageIndex, int pageSize, out int recordCount, DateTime start, DateTime end);

        /// <summary>
        /// 获取指定用户发送过的所有待办事务
        /// </summary>
        /// <returns>按照时间倒序排序的待办事务列表</returns>
        IList GetPendingOrderListBySender(int pageIndex, int pageSize, out int recordCount, string sender);

        /// <summary>
        /// 获取指定用户从指定时间开始到现时发送过的所有待办事务
        /// </summary>
        /// <returns>按照时间倒序排序的待办事务列表</returns>
        IList GetPendingOrderListBySender(int pageIndex, int pageSize, out int recordCount, string sender, DateTime start);

        /// <summary>
        /// 获取指定用户从指定时间段内发送过的所有待办事务
        /// </summary>
        /// <returns>按照时间倒序排序的待办事务列表</returns>
        IList GetPendingOrderListBySender(int pageIndex, int pageSize, out int recordCount, string sender, DateTime start, DateTime end);

        /// <summary>
        /// 获取指定的待办事务
        /// </summary>
        /// <param name="id">待办事务ID</param>
        /// <returns>需要获取的待办事务</returns>
        PendingOrderInfo GetPendingOrder(long id);

        /// <summary>
        /// 获取指定的待办事务
        /// </summary>
        /// <param name="id">待办事务ID</param>
        /// <param name="username">已读的用户名</param>
        void MarkRead(long id, string username);



        //**********Modified by Xue 2011-6-27****************************************************************************************************
        /// <summary>
        /// 根据URL标记为已处理
        /// </summary>
        /// <param name="url">处理URL地址</param>
        void MarkReadByURL(string url);
        //**********Modification Finished 2011-6-27**********************************************************************************************


        /// <summary>
        /// 管理员删除待办事务，级联删除接收者列表信息
        /// </summary>
        /// <param name="id">待办事务ID</param>
        void DeletePendingOrder(long id);

        /// <summary>
        /// 当前用户删除发送给自身的待办事务
        /// </summary>
        /// <param name="id">待办事务ID</param>
        /// <param name="username">接收用户名</param>
        void DeletePendingOrder(long id, string username);


        /// <summary>
        /// 获取新待办事务数目
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        int GetNewPendingOrderNumber(string username);

        /// <summary>
        /// 获取当前用户的id号在lastTime后的新消息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="lastTime"></param>
        /// <returns></returns>
        PendingOrderInfo GetNewPendingOrder(string username, DateTime lastTime,out int count);

        /// <summary>
        /// 获取当前用户的最新消息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="lastTime"></param>
        /// <returns></returns>
        PendingOrderInfo GetNewPendingOrder(string username, out int count);


        //**********Modified by Xue 2011-6-27****************************************************************************************************
        /// <summary>
        /// 根据URL更新接收者
        /// </summary>
        /// <param name="url">处理URL地址</param>
        /// <param name="item">集合</param>
        void UpdateReceiversByURL(string url, PendingOrderInfo item);
        //**********Modification Finished 2011-6-27**********************************************************************************************

        //**********Modified by Xue 2011-6-27****************************************************************************************************
        /// <summary>
        /// 根据URL获取ID值
        /// </summary>
        /// <param name="url">处理URL地址</param>
        long GetPendingOrderIDByURL(string url);
        //**********Modification Finished 2011-6-27**********************************************************************************************
    }
}
