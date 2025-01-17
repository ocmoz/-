﻿using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;

namespace FM2E.Model.Maintain
{
    /// <summary>
    /// 设备维修记录实体类
    /// </summary>
    public class EquipmentMaintainRecordInfo
    {
        private string _equipmentno;
        private long _maintaindept;
        private string _maintaindeptname;
        private MalfunctionRank _malfunctionrank;
        private string _systemid;
        private DateTime _reportdate;
        private DateTime _maintaindate;
        private long _id;
        private string _remark;
        private string _equipmentname;
        private string _model;
        private MaintainedEquipmentStatus _maintainresult;
        private decimal _maintainfee;
        private long _sheetid;
        private string _sheetno;
        private string _companyid;
        private long _departmentid;
        private string _departmentname;

        /// <summary>
        /// 故障设备条形码
        /// </summary>
        public string EquipmentNO
        {
            set { _equipmentno = value; }
            get { return _equipmentno; }
        }
        /// <summary>
        /// 维修队ID
        /// </summary>
        public long MaintainDept
        {
            set { _maintaindept = value; }
            get { return _maintaindept; }
        }
        /// <summary>
        /// 维修队名称
        /// </summary>
        public string MaintainDeptName
        {
            set { _maintaindeptname = value; }
            get { return _maintaindeptname; }
        }
        /// <summary>
        /// 故障等级
        /// </summary>
        public MalfunctionRank MalfunctionRank
        {
            set { _malfunctionrank = value; }
            get { return _malfunctionrank; }
        }
        /// <summary>
        /// 所属系统ID
        /// </summary>
        public string SystemID
        {
            set { _systemid = value; }
            get { return _systemid; }
        }
        /// <summary>
        /// 故障上报时间
        /// </summary>
        public DateTime ReportDate
        {
            set { _reportdate = value; }
            get { return _reportdate; }
        }
        /// <summary>
        /// 设备维修时间
        /// </summary>
        public DateTime MaintainDate
        {
            set { _maintaindate = value; }
            get { return _maintaindate; }
        }
        /// <summary>
        /// 设备维修记录主键
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 设备维修具体情况
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 故障设备名称
        /// </summary>
        public string EquipmentName
        {
            set { _equipmentname = value; }
            get { return _equipmentname; }
        }
        /// <summary>
        /// 故障设备型号
        /// </summary>
        public string Model
        {
            set { _model = value; }
            get { return _model; }
        }
        /// <summary>
        /// 故障设备维修结果
        /// </summary>
        public MaintainedEquipmentStatus MaintainResult
        {
            set { _maintainresult = value; }
            get { return _maintainresult; }
        }
        /// <summary>
        /// 故障设备维修费用
        /// </summary>
        public decimal MaintainFee
        {
            set { _maintainfee = value; }
            get { return _maintainfee; }
        }
        /// <summary>
        /// 故障处理单ID号
        /// </summary>
        public long SheetID
        {
            set { _sheetid = value; }
            get { return _sheetid; }
        }
        /// <summary>
        /// 故障处理单编号
        /// </summary>
        public string SheetNO
        {
            set { _sheetno = value; }
            get { return _sheetno; }
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
        /// 故障部门ID
        /// </summary>
        public long DepartmentID
        {
            set { _departmentid = value; }
            get { return _departmentid; }
        }
        /// <summary>
        /// 故障部门名称
        /// </summary>
        public string DepartmentName
        {
            set { _departmentname = value; }
            get { return _departmentname; }
        }
    }

    /// <summary>
    /// 流转记录
    /// </summary>
    public class TransferRecord
    {
        private string _url;
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        private string _sheetNO;
        public string SheetNO
        {
            set { _sheetNO = value; }
            get { return _sheetNO; }
        }
        private DateTime _reportDate;
        public DateTime ReportDate
        {
            set { _reportDate = value; }
            get { return _reportDate; }
        }
        private string _name;
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        private string _type;
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        private EquipmentStatus _status;
        public EquipmentStatus Status
        {
            set { _status = value; }
            get { return _status; }
        }
    }
}
