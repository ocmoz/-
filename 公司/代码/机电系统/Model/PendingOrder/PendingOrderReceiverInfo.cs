using System;
using System. Collections. Generic;
using System. Text;

namespace FM2E.Model.PendingOrder
{
    /// <summary>
    /// 实体类FM2E_PendingOrderReceiver 。接收待办事务用户
    /// </summary>
    public class PendingOrderReceiverInfo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PendingOrderReceiverInfo()
        { }

        /// <summary>
        /// 新建待办事务使用的构造函数
        /// </summary>
        /// <param name="id">待办事务ID</param>
        /// <param name="username">接收用户名</param>
        public PendingOrderReceiverInfo(long id, string username)
        {
            _id = id;
            _username = username;
            _hasread = false;
        }

        /// <summary>
        /// 读取待办事务使用的构造函数
        /// </summary>
        /// <param name="id">待办事务ID</param>
        /// <param name="username">接收用户名</param>
        /// <param name="hasread">是否已读</param>
        public PendingOrderReceiverInfo(long id, string username, bool hasread)
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
        /// 待办事务ID
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
