using System;
using System. Collections. Generic;
using System. Text;

namespace FM2E.Model.Message
{
    /// <summary>
    /// 短信息的记录
    /// </summary>
    public class MessageInfo
    {
        #region Properties
        private long id;

        /// <summary>
        /// 消息的唯一ID
        /// </summary>
        public long ID
        {
            get { return id; }
            set { id = value; }
        }

        private short type;

        /// <summary>
        /// 消息类型代码
        /// </summary>
        public short Type
        {
            get { return type; }
            set { type = value; }
        }

        private short sendway;
        /// <summary>
        /// 消息发送方式代码，如果是EMAIL发送，则默认为已读的
        /// </summary>
        public short SendWay
        {
            get { return sendway; }
            set { sendway = value; }
        }

        private string sendFrom;

        /// <summary>
        /// 发送者ID 
        /// </summary>
        public string SendFrom
        {
            get { return sendFrom; }
            set { sendFrom = value; }
        }

        private string _senderpersonname;

        /// <summary>
        /// 发送者的真实姓名
        /// </summary>
        public string SenderPersonName
        {
            get { return _senderpersonname; }
            set { _senderpersonname = value; }
        }

        private DateTime messageTime;

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime MessageTime
        {
            get { return messageTime; }
            set { messageTime = value; }
        }

        private string title;

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }


        private string message;

        /// <summary>
        /// 消息主体
        /// </summary>
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        private string attachment;


        /// <summary>
        /// 附件地址
        /// </summary>
        public string Attachment
        {
            get { return attachment; }
            set { attachment = value; }
        }

        private IList<MessageReceiverInfo> receivers = new List<MessageReceiverInfo>();

        /// <summary>
        /// 接收用户列表
        /// </summary>
        public IList<MessageReceiverInfo> Receivers
        {
            get { return receivers; }
            set { receivers = value; }
        }


        private string receiverAddress;

        /// <summary>
        /// 原始输入用户地址表
        /// </summary>
        public string ReceiverAddress
        {
            get
            {
                return receiverAddress;
            }
            set
            {
                receiverAddress = value;
            }
        }

        #endregion

        /// <summary>
        /// 系统消息字符串
        /// </summary>
        public const string SYSTEM_MESSAGE = "系统消息";

        /// <summary>
        /// 预警消息字符串
        /// </summary>
        public const string WARNING_MESSAGE = "预警消息";

        /// <summary>
        /// 获取消息类型名称
        /// </summary>
        public string TypeString
        {
            get
            {
                switch (type)
                {
                    case 0:
                        return SYSTEM_MESSAGE;
                    case 1:
                        return WARNING_MESSAGE;
                    default:
                        return string.Empty;
                }
            }
        }

        /// <summary>
        /// 根据消息类型名称设置消息类型代码
        /// </summary>
        /// <param name="typeName">消息类型名称</param>
        public void SetTypeCode(String typeName)
        {
            switch (typeName)
            {
                case SYSTEM_MESSAGE:
                    type = 0;
                    break;
                case WARNING_MESSAGE:
                    type = 1;
                    break;
                default:
                    type = 0;
                    break;
            }
        }

        /// <summary>
        /// 站内消息字符串
        /// </summary>
        public const string INNER_MESSAGE = "站内消息";

        /// <summary>
        /// EMAIL消息字符串
        /// </summary>
        public const string EMAIL_MESSAGE = "EMAIL";

        /// <summary>
        /// 获取消息发送方式名称
        /// </summary>
        public string SendWayString
        {
            get
            {
                switch (sendway)
                {
                    case 0:
                        return INNER_MESSAGE;
                    case 1:
                        return EMAIL_MESSAGE;
                    default:
                        return string.Empty;
                }
            }
        }

        /// <summary>
        /// 根据消息发送方式名称设置消息发送方式代码
        /// </summary>
        /// <param name="typeName">消息发送方式名称</param>
        public void SetSendWayCode(String sendwayName)
        {
            switch (sendwayName)
            {
                case INNER_MESSAGE:
                    type = 0;
                    break;
                case EMAIL_MESSAGE:
                    type = 1;
                    break;
                default:
                    type = 0;
                    break;
            }
        }

        /// <summary>
        /// 接收者字符串
        /// </summary>
        public string RecevierString
        {
            get
            {
                string str = "";
                if (receivers != null)
                {
                    foreach (MessageReceiverInfo mri in receivers)
                    {
                        str += mri.UserName + ",";
                    }
                }
                if (str.Length > 0)
                {
                    //去掉最后一个逗号
                    str = str.Substring(0, str.Length - 1);
                }
                return str;
            }
        }
    }
}
