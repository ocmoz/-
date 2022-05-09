using System;
using System. Collections. Generic;
using System. Text;

namespace FM2E.Model.Message
{
    /// <summary>
    /// 实体类FM2E_MessageReceiver 。接收消息用户
    /// </summary>
    public class MessageReceiverInfo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MessageReceiverInfo()
        { }

        /// <summary>
        /// 新建消息使用的构造函数
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="username">接收用户名</param>
        public MessageReceiverInfo(long id, string username)
        {
            _id = id;
            _username = username;
            _hasread = false;
        }

        /// <summary>
        /// 读取消息使用的构造函数
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="username">接收用户名</param>
        /// <param name="hasread">是否已读</param>
        public MessageReceiverInfo(long id, string username, bool hasread)
        {
            _id = id;
            _username = username;
            _hasread = hasread;
        }

        #region Properties
        private long _id;
        private string _username;
        private string _personname;
        private bool _hasread = false;
        /// <summary>
        /// 消息ID
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 接收用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 是否已读
        /// </summary>
        public bool HasRead
        {
            set { _hasread = value; }
            get { return _hasread; }
        }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string PersonName
        {
            get { return _personname; }
            set { _personname = value; }
        }
        #endregion Model

    }
}
