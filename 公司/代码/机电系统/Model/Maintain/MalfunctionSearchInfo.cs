using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Maintain
{
    /// <summary>
    /// 故障处理单查询实体类
    /// </summary>
    [Serializable]
    public class MalfunctionSearchInfo
    {
        private string sheetNO;
        private string companyID;
        private long departmentID;
        private string reporter;
        private DateTime reportDateFrom;
        private DateTime reportDateTo;
        //private DateTime reportDateFrom2;
        //private DateTime reportDateTo2;
        private string recorder;
        private string recorderName;
        private long recordDept;
        private long maintainDept;
        private int status;
        private string receiver;
        private string equipmentNO;
        private string equipmentModel;
        private string equipmentSpecification;
        private string equipmentName;
        private string systemID;
        private string addressCode;
        private int rank;
        private int malReason;
        private int isDelayApply;
        private int malMeasure;
        private int stationcheck;
        public int EStatus { get; set; }

        public string ECompanyID { get; set; }

        public string MaintainSataff { get; set; }
        private int firstDelayApprove;
        private int finalDelayApprove;
        private string jiliangOrNot;
        private string jiagongOrNot;

        /// <summary>
        /// 故障处理单编号
        /// </summary>
        public string SheetNO
        {
            get { return sheetNO; }
            set { sheetNO = value; }
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyID
        {
            get { return companyID; }
            set { companyID = value; }
        }
        /// <summary>
        /// 部门编号
        /// </summary>
        public long DepartmentID
        {
            get { return departmentID; }
            set { departmentID = value; }
        }
        /// <summary>
        /// 报修人
        /// </summary>
        public string Reporter
        {
            get { return reporter; }
            set { reporter = value; }
        }
        /// <summary>
        /// 报修日期
        /// </summary>
        public DateTime ReportDateFrom
        {
            get { return reportDateFrom; }
            set { reportDateFrom = value; }
        }
        /// <summary>
        /// 报修日期
        /// </summary>
        public DateTime ReportDateTo
        {
            get { return reportDateTo; }
            set { reportDateTo = value; }
        }

        /// <summary>
        /// 确认修复日期
        /// </summary>
        //public DateTime ReportDateFrom2
        //{
        //    get { return reportDateFrom2; }
        //    set { reportDateFrom2 = value; }
        //}

        /// <summary>
        /// 确认修复日期
        /// </summary>
        //public DateTime ReportDateTo2
        //{
        //    get { return reportDateTo2; }
        //    set { reportDateTo2 = value; }
        //}

        /// <summary>
        /// 故障记录人用户名
        /// </summary>
        public string Recorder
        {
            get { return recorder; }
            set { recorder = value; }
        }
        /// <summary>
        /// 故障记录人姓名
        /// </summary>
        public string RecorderName
        {
            get { return recorderName; }
            set { recorderName = value; }
        }
        /// <summary>
        /// 记录故障部门的ID
        /// </summary>
        public long RecordDept
        {
            get { return recordDept; }
            set { recordDept = value; }
        }
        /// <summary>
        /// 维修队
        /// </summary>
        public long MaintainDept
        {
            get { return maintainDept; }
            set { maintainDept = value; }
        }
        /// <summary>
        /// 表单状态
        /// </summary>
        public int Status
        {
            set { status = value; }
            get { return status; }
        }
        /// <summary>
        /// 故障受理人用户名
        /// </summary>
        public string Receiver
        {
            set { receiver = value; }
            get { return receiver; }
        }
        /// <summary>
        /// 设备条形码
        /// </summary>
        public string EquipmentNO
        {
            set { equipmentNO = value; }
            get { return equipmentNO; }
        }        
        /// <summary>
        /// 设备型号
        /// </summary>
        public string EquipmentModel
        {
            set { equipmentModel = value; }
            get { return equipmentModel; }
        }        
        /// <summary>
        /// 设备品牌
        /// </summary>
        public string EquipmentSpecification
        {
            set { equipmentSpecification = value; }
            get { return equipmentSpecification; }
        }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentName
        {
            set { equipmentName = value; }
            get { return equipmentName; }
        }
        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemID
        {
            set { systemID = value; }
            get { return systemID; }
        }
        /// <summary>
        /// 故障发生地址的字符编码
        /// </summary>
        public string AddressCode
        {
            get { return addressCode; }
            set { addressCode = value; }
        }
        /// <summary>
        /// 故障等级
        /// </summary>
        public int MalfunctionRank
        {
            get { return rank; }
            set { rank = value; }
        }
        /// <summary>
        /// 故障原因
        /// </summary>
        public int MalReason
        {
            get { return malReason; }
            set { malReason = value; }
        }
        /// <summary>
        /// 故障延时申请与否
        /// </summary>
        public int IsDelayApply
        {
            get { return isDelayApply; }
            set { isDelayApply = value; }
        }
        /// <summary>
        /// 申请计量与否
        /// </summary>
        public int MalMeasure
        {
            get { return malMeasure; }
            set { malMeasure = value; }
        }
        /// <summary>
        /// 自维工程师确认（确认后电工才能处理故障单）
        /// </summary>
        public int Stationcheck
        {
            set { stationcheck = value; }
            get { return stationcheck; }
        }
        /// <summary>
        /// 故障延时初次审批
        /// </summary>
        public int FirstDelayApprove 
        {
            get { return firstDelayApprove; }
            set { firstDelayApprove = value; }
        }        /// <summary>
        /// 故障延时二次审批
        /// </summary>
        public int FinalDelayApprove
        {
            get { return finalDelayApprove; }
            set { finalDelayApprove = value; }
        }

        /// <summary>
        /// 计量与否
        /// </summary>
        public string JiliangOrNot
        {
            get { return jiliangOrNot; }
            set { jiliangOrNot = value; }
        }        
        /// <summary>
        /// 甲供与否
        /// </summary>
        public string JiagongOrNot
        {
            get { return jiagongOrNot; }
            set { jiagongOrNot = value; }
        }
    }
}
