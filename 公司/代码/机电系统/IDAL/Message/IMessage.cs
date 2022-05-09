using System;
using System. Collections. Generic;
using System. Text;
using FM2E. Model. Message;
using System.Collections;

namespace FM2E. IDAL. Message
{
    public interface IMessage
    {
        /// <summary>
        /// 发送一条新消息
        /// </summary>
        /// <param name="item">消息实体，包含消息接收者列表</param>
        void InsertMessage(MessageInfo item);

        /// <summary>
        /// 获取指定用户所拥有的消息列表，按照未读先排，已读后排，再按时间倒序排序
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>先未读，后已读，再按时间倒序排序的消息列表</returns>
        IList GetMessageListByReceiver(int pageIndex, int pageSize, out int recordCount,String username);

        /// <summary>
        /// 获取指定用户从指定时间开始到现时所拥有的消息列表，按照未读先排，已读后排，再按时间倒序排序
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>先未读，后已读，再按时间倒序排序的消息列表</returns>
        IList GetMessageListByReceiver(int pageIndex, int pageSize, out int recordCount, String username, DateTime start);

        /// <summary>
        /// 获取指定用户从指定时间段中所拥有的消息列表，按照未读先排，已读后排，再按时间倒序排序
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>先未读，后已读，再按时间倒序排序的消息列表</returns>
        IList GetMessageListByReceiver(int pageIndex, int pageSize, out int recordCount, String username, DateTime start, DateTime end);

        /// <summary>
        /// 获取系统中所有的消息
        /// </summary>
        /// <returns>按照时间倒序排序的消息列表</returns>
        IList GetAllMessage(int pageIndex, int pageSize, out int recordCount);

        /// <summary>
        /// 获取系统中指定时间开始到现时的所有的消息
        /// </summary>
        /// <returns>按照时间倒序排序的消息列表</returns>
        IList GetAllMessage(int pageIndex, int pageSize, out int recordCount, DateTime start);

        /// <summary>
        /// 获取系统中指定时间段内的所有的消息
        /// </summary>
        /// <returns>按照时间倒序排序的消息列表</returns>
        IList GetAllMessage(int pageIndex, int pageSize, out int recordCount, DateTime start, DateTime end);

        /// <summary>
        /// 获取指定用户发送过的所有消息
        /// </summary>
        /// <returns>按照时间倒序排序的消息列表</returns>
        IList GetMessageListBySender(int pageIndex, int pageSize, out int recordCount, string sender);

        /// <summary>
        /// 获取指定用户从指定时间开始到现时发送过的所有消息
        /// </summary>
        /// <returns>按照时间倒序排序的消息列表</returns>
        IList GetMessageListBySender(int pageIndex, int pageSize, out int recordCount, string sender, DateTime start);

        /// <summary>
        /// 获取指定用户从指定时间段内发送过的所有消息
        /// </summary>
        /// <returns>按照时间倒序排序的消息列表</returns>
        IList GetMessageListBySender(int pageIndex, int pageSize, out int recordCount, string sender, DateTime start, DateTime end);

        /// <summary>
        /// 获取指定的消息
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <returns>需要获取的消息</returns>
        MessageInfo GetMessage(long id);

        /// <summary>
        /// 获取指定的消息
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="username">已读的用户名</param>
        void MarkRead(long id, string username);


                /// <summary>
        /// 管理员删除消息，级联删除接收者列表信息
        /// </summary>
        /// <param name="id">消息ID</param>
        void DeleteMessage(long id);

        /// <summary>
        /// 当前用户删除发送给自身的消息
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="username">接收用户名</param>
        void DeleteMessage(long id, string username);


        /// <summary>
        /// 获取新消息数目
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        int GetNewMessageNumber(string username);
    }
}
