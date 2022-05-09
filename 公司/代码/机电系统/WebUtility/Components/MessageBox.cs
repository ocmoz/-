
using System;
using System.Collections.Generic;
using System.Text;

namespace WebUtility.Components
{
    /// <summary>
    /// 信息提示实体类
    /// </summary>
    [Serializable]
    public class MessageBox
    {

        #region "Private Variables"
        private string _M_Title = "信息提示标题";
        private string _M_Body = "信息提示内容";
        [NonSerialized]
        private Exception _M_Exception = null;
        private Icon_Type _M_IconType = Icon_Type.OK;
        private List<sys_NavigationUrl> _M_ButtonList = new List<sys_NavigationUrl>();
        private Msg_Type _M_Type = Msg_Type.Info;
        private bool _M_WriteToLog = true;
        private string _M_ReturnScript ;
        #endregion

        #region "Public Variables"
        /// <summary>
        /// 信息提示标题
        /// </summary>
        public string M_Title
        {
            get {
                return _M_Title;
            }
            set {
                _M_Title = value;
            }
        }

        /// <summary>
        /// 信息提示内容
        /// </summary>
        public string M_Body
        {
            get {
                return _M_Body;
            }
            set {
                _M_Body = value;
            }
        }

        public Exception M_Exception
        {
            get
            {
                return _M_Exception;
            }
            set
            {
                _M_Exception = value;
            }
        }

        /// <summary>
        /// 信息提示图标类型
        /// </summary>
        public Icon_Type M_IconType
        {
            get {
                return _M_IconType;
            }
            set {
                _M_IconType = value;
            }
        }

        /// <summary>
        /// 按钮列表
        /// </summary>
        public List<sys_NavigationUrl> M_ButtonList
        {
            get {
                return _M_ButtonList;
            }
            set {
                _M_ButtonList = value;
            }
        }
        /// <summary>
        /// 信息类型
        /// </summary>
        public Msg_Type M_Type
        {
            get
            {
                return _M_Type;
            }
            set {
                _M_Type = value;
            }
        }

        /// <summary>
        /// 是否需要写入当前日志,默认为true
        /// </summary>
        public bool M_WriteToLog
        {
            get {
                return _M_WriteToLog;
            }
            set {
                _M_WriteToLog = value;
            }
        }

        /// <summary>
        /// 执行Script脚本字符串(需加<script></script>)
        /// </summary>
        public string M_ReturnScript
        {
            get {
                return _M_ReturnScript;
            }
            set {
                _M_ReturnScript = value;
            }
        }

        #endregion
    }

    #region "提示框图标类型"

    /// <summary>
    /// 提示Icon类型
    /// </summary>
    public enum Icon_Type : byte
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        OK,         
        /// <summary>
        /// 操作标示
        /// </summary>
        Alert,       
        /// <summary>
        /// 操作失败
        /// </summary>
        Error
    }

    public enum Msg_Type : byte
    {
        /// <summary>
        /// 普通信息
        /// </summary>
        Info=1,
        /// <summary>
        /// 错误信息
        /// </summary>
        Error,
        /// <summary>
        /// 致命错误
        /// </summary>
        Fatal,
        /// <summary>
        /// 调试信息
        /// </summary>
        Debug,
        /// <summary>
        /// 警告信息
        /// </summary>
        Warn
    }
    #endregion



}
