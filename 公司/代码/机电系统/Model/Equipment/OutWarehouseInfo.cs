﻿using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;

using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 出库故障处理单状态
    /// </summary>
    public enum OutWarehouseInfoStatus
    {
        [EnumDescription("等待工程师审批")]
        WaitEngineer = 1,
        [EnumDescription("等待领导审批")]
        WaitManagerApprove = 2,

        [EnumDescription("返回修改")]
        ReturnModify = 3,

        [EnumDescription("等待仓管出库")]
        WaitWarehouseKeeper = 4,
        [EnumDescription("出库完成")]
        End = 5
    }
      /// <summary>
    /// 易耗品出库单信息实体类
    /// </summary>
    [Serializable]
    public class OutWarehouseInfo
    {
        #region Model
        private long _id;
        private string _warehouseid;
        private string _warehousename;
        private long _warehouseaddressid;
        private string _companyid;
        private string _companyname;
        private long _departmentid;
        private string _departmentname;
        private DateTime _submittime;
        private string _applicantid;
        private string _operatorid;
        private string _remark;
        private bool _isdeleted;
        private string _applicantname;
        private string _operatorname;
        private string _warehousedetaillocation;
        private string _sheetname;

        private string _attachment;
        private string _nextUserName;
        private string _statusDescription;
        private string _currentStateName;
        private string _workFlowInstanceID;
        private string _editreason;
        /// <summary>
        /// 仓库的详细地址
        /// </summary>
        public string WarehouseDetailLocation
        {
            get { return _warehousedetaillocation; }
            set { _warehousedetaillocation = value; }
        }

        /// <summary>
        /// 仓库地址ID
        /// </summary>
        public long WarehouseAddressID
        {
            get { return _warehouseaddressid; }
            set { _warehouseaddressid = value; }
        }

        /// <summary>
        /// 入库单流水号
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 所入仓库ID
        /// </summary>
        public string WarehouseID
        {
            set { _warehouseid = value; }
            get { return _warehouseid; }
        }
        /// <summary>
        /// 所入仓库名称
        /// </summary>
        public string WarehouseName
        {
            set { _warehousename = value; }
            get { return _warehousename; }
        }
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 入库部门ID
        /// </summary>
        public long DepartmentID
        {
            set { _departmentid = value; }
            get { return _departmentid; }
        }
        /// <summary>
        /// 入库部门名称
        /// </summary>
        public string DepartmentName
        {
            set { _departmentname = value; }
            get { return _departmentname; }
        }
        /// <summary>
        /// 入库单提交时间
        /// </summary>
        public DateTime SubmitTime
        {
            set { _submittime = value; }
            get { return _submittime; }
        }
   
        /// <summary>
        /// 入库申请人ID
        /// </summary>
        public string ApplicantID
        {
            set { _applicantid = value; }
            get { return _applicantid; }
        }
        /// <summary>
        /// 经办人ID
        /// </summary>
        public string OperatorID
        {
            set { _operatorid = value; }
            get { return _operatorid; }
        }
        /// <summary>
        /// 入库备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 是否已经被删除
        /// </summary>
        public bool IsDeleted
        {
            set { _isdeleted = value; }
            get { return _isdeleted; }
        }
        #endregion Model
        /// <summary>
        /// 出库申请人姓名
        /// </summary>
        public string ApplicantName
        {
            set { _applicantname = value; }
            get { return _applicantname; }
        }
        /// <summary>
        /// 经办人姓名
        /// </summary>
        public string OperatorName
        {
            set { _operatorname = value; }
            get { return _operatorname; }
        }
        /// <summary>
        /// 出库单表名
        /// </summary>
        public string SheetName
        {
            set { _sheetname = value; }
            get { return _sheetname; }
        }

        /// <summary>
        /// 查找的时候使用的出库时间下限
        /// </summary>
        public DateTime TimeLower { get; set; }
        /// <summary>
        /// 查找的时候使用的出库时间上限
        /// </summary>
        public DateTime TimeUpper { get; set; }

        private IList _list = new List<OutEquipmentsInfo>();

        /// <summary>
        /// 出库明细列表
        /// </summary>
        public IList OutWarehouseList
        {
            get { return _list; }
            set { _list = value; }
        }

        /// <summary>
        /// 附件
        /// </summary>
        public string Attachment
        {
            get { return _attachment; }
            set { _attachment = value; }
        }

        /// <summary>
        /// 工作流id
        /// </summary>
        public string WorkFlowInstanceID
        {
            get { return _workFlowInstanceID; }
            set { _workFlowInstanceID = value; }
        }

        /// <summary>
        ///下一个审批者
        /// </summary>
        public string NextUserName
        {
            set { _nextUserName = value; }
            get { return _nextUserName; }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public string StatusDescription
        {
            get { return _statusDescription; }
            set { _statusDescription = value; }
        }

        /// <summary>
        /// 工作流节点
        /// </summary>
        public string CurrentStateName
        {
            set { _currentStateName = value; }
            get { return _currentStateName; }
        }
       
        /// <summary>
        /// 申请时间下限
        /// </summary>
        public DateTime ApplyTimeLower { get; set; }
        /// <summary>
        /// 申请时间上限
        /// </summary>
        public DateTime ApplyTimeUpper { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string Editreason
        {
            get { return _editreason; }
            set { _editreason = value; }
        }
    }
}
