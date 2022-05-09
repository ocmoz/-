using System;
using System. Collections. Generic;
using System. Text;

namespace FM2E.Model.PendingOrder
{
    /// <summary>
    /// 实体类FM2E_PendingOrderReceiver 。接收待办事务用户
    /// </summary>
    public class PendingOrderReceiverCombineInfo:PendingOrderInfo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PendingOrderReceiverCombineInfo():base()
        { }

        /// <summary>
        /// 新建待办事务使用的构造函数
        /// </summary>
        /// <param name="id">待办事务ID</param>
        /// <param name="username">接收用户名</param>
        public PendingOrderReceiverCombineInfo(string username)
            : base()
        {
            _username = username;
            _hasread = false;
        }

        /// <summary>
        /// 读取待办事务使用的构造函数
        /// </summary>
        /// <param name="id">待办事务ID</param>
        /// <param name="username">接收用户名</param>
        /// <param name="hasread">是否已读</param>
        public PendingOrderReceiverCombineInfo(string username, bool hasread)
            : base()
        {
            _username = username;
            _hasread = hasread;
        }

        #region Properties

        private string _username;
        private bool _hasread;

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
        /// 是否已读字符串
        /// </summary>
        public string HasReadString
        {
            get
            {
                if (_hasread)
                    return "";
                else
                    return "New";
            }
        }
        #endregion Model

    }
}
