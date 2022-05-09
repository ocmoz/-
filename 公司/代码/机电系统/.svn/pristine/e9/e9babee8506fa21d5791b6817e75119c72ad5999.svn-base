using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 出库申请单查询信息封装类
    /// </summary>
    public class OutWarehouseApplySearchInfo
    {
        /// <summary>
        /// 出库申请单ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 出库申请单名称
        /// </summary>
        public string SheetName { get; set; }
        /// <summary>
        /// 出库目标仓库ID
        /// </summary>
        public string WarehouseID { get; set; }
        /// <summary>
        /// 申请时间下限
        /// </summary>
        public DateTime ApplyTimeLower { get; set; }
        /// <summary>
        /// 申请时间上限
        /// </summary>
        public DateTime ApplyTimeUpper { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// 申请人账号
        /// </summary>
        public string Applicant { get; set; }
        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplicantName { get; set; }
        /// <summary>
        /// 出库时间下限
        /// </summary>
        public DateTime OutTimeLower { get; set; }
        /// <summary>
        /// 出库时间上限
        /// </summary>
        public DateTime OutTimeUpper { get; set; }
        /// <summary>
        /// 接收人账号
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// 接收人姓名
        /// </summary>
        public string ReceiverName { get; set; }
        /// <summary>
        /// 经办人账号
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 经办人姓名
        /// </summary>
        public string OperatorName { get; set; }
        /// <summary>
        /// 含有代理和非代理，用or连接
        /// </summary>
        public string NextUserName { get; set;}

        private List<string> _workflowstatus = new List<string>();
        /// <summary>
        /// 工作流状态列表
        /// </summary>
        public List<string> WorkFlowStatusList
        {
            get { return _workflowstatus; }
            set { _workflowstatus = value; }
        }
        
    }
}
