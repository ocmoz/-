using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.BugReportManager
{
    public class BugReportInfo
    {
        #region Model
        private long _bugreportid;
        private string _title;
        private string _message;
        private string _attachment;
        private string _senderid;
        private string _sendername;
        /// <summary>
        /// 报告时间
        /// </summary>
        public DateTime ReportTime { get; set; }

        public DateTime ReponseTime { get; set; }

        private int _status;


        private string _attachment2;
        private string _report;

        public string StatusShow
        {
            get
            {
                switch (Status)
                {
                    case 1:
                        {
                            return "等待反馈";
                            //break;
                        }
                    case 2:
                        {
                            return "反馈完毕";
                            //break;
                        }
                    default: { return ""; 
                        
                        //break; 
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public long BugreportID
        {
            set { _bugreportid = value; }
            get { return _bugreportid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Message
        {
            set { _message = value; }
            get { return _message; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SenderID
        {
            set { _senderid = value; }
            get { return _senderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SenderName
        {
            set { _sendername = value; }
            get { return _sendername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Attachment2
        {
            set { _attachment2 = value; }
            get { return _attachment2; }
        }
        /// <summary>
        /// 反馈
        /// </summary>
        public string Report
        {
            set { _report = value; }
            get { return _report; }
        }
        /// <summary>
        /// 反馈人
        /// </summary>
        public string ResponserID { get; set; }
        /// <summary>
        /// 反馈人姓名
        /// </summary>
        public string ResponserName { get; set; }
        #endregion Model
    }
}
