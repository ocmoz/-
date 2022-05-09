using System;
using System. Collections. Generic;
using System. Text;

namespace FM2E.Model.PendingOrder
{
    /// <summary>
    /// 短信息的记录
    /// </summary>
    public class PendingOrderInfo
    {
        #region Properties
        private long id;

        /// <summary>
        /// 待办事务的唯一ID
        /// </summary>
        public long ID
        {
            get { return id; }
            set { id = value; }
        }

        private short type;

        /// <summary>
        /// 待办事务类型代码
        /// </summary>
        public short Type
        {
            get { return type; }
            set { type = value; }
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

        private DateTime sendTime;

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime
        {
            get { return sendTime; }
            set { sendTime = value; }
        }

        private string title;

        /// <summary>
        /// 待办事务标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string url;

        /// <summary>
        /// 附件地址
        /// </summary>
        public string URL
        {
            get { return url; }
            set { url = value; }
        }

        private IList<PendingOrderReceiverInfo> receivers = new List<PendingOrderReceiverInfo>();

        /// <summary>
        /// 接收用户列表
        /// </summary>
        public IList<PendingOrderReceiverInfo> Receivers
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
        /// 系统待办事务字符串
        /// </summary>
        public const string NORMAL_PENDINGORDER = "普通待办事务";

        /// <summary>
        /// 预警待办事务字符串
        /// </summary>
        public const string URGENT_PENDINGORDER = "紧急待办事务";

        /// <summary>
        /// 获取待办事务类型名称
        /// </summary>
        public string TypeString
        {
            get
            {
                switch (type)
                {
                    case 0:
                        return NORMAL_PENDINGORDER;
                    case 1:
                        return URGENT_PENDINGORDER;
                    default:
                        return string.Empty;
                }
            }
        }

        /// <summary>
        /// 根据待办事务类型名称设置待办事务类型代码
        /// </summary>
        /// <param name="typeName">待办事务类型名称</param>
        public void SetTypeCode(String typeName)
        {
            switch (typeName)
            {
                case NORMAL_PENDINGORDER:
                    type = 0;
                    break;
                case URGENT_PENDINGORDER:
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
                    foreach (PendingOrderReceiverInfo mri in receivers)
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
