using System;
using System. Collections. Generic;
using System. Text;

namespace FM2E.Model.Message
{
    /// <summary>
    /// 实体类FM2E_MessageReceiver 。接收消息用户
    /// </summary>
    public class MessageReceiverCombineInfo:MessageInfo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MessageReceiverCombineInfo():base()
        { }

        /// <summary>
        /// 新建消息使用的构造函数
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="username">接收用户名</param>
        public MessageReceiverCombineInfo(string username)
            : base()
        {
            _username = username;
            _hasread = false;
        }

        /// <summary>
        /// 读取消息使用的构造函数
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="username">接收用户名</param>
        /// <param name="hasread">是否已读</param>
        public MessageReceiverCombineInfo(string username, bool hasread)
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
