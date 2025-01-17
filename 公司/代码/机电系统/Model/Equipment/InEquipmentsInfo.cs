﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 易耗品出入库明细信息实体类
    /// </summary>
    public class InEquipmentsInfo
    {
        #region Model
        private long _id;
        private long _itemid;
        private string _warehouseid;
        private string _warehousename;
        private bool _isasset;
        private string _equipmentno;
        private long _expendableid;
        private decimal _count;
        private DateTime _longime;
        private string _unit;
        private string _equipmentname;
        private string _equipmentmodel;
        private string _expendablename;
        private string _expendablemodel;
        private DateTime _inouttimelower;
        private DateTime _inouttimeupper;
        private DateTime _outtime;
        private string _equipmenttype;
        private string _remark;
        private string _expendableType;
        /// <summary>
        /// 入库备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 设备类型;新件、二手件
        /// </summary>
        public string EquipmentType
        {
            set { _equipmenttype = value; }
            get { return _equipmenttype; }
        }
        public DateTime OutTime
        {
            set { _outtime = value; }
            get { return _outtime; }
        }

        public DateTime InOutTimeLower
        {
            set { _inouttimelower = value; }
            get { return _inouttimelower; }
        }

        public DateTime InOutTimeUpper
        {
            set { _inouttimeupper = value; }
            get { return _inouttimeupper; }
        }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 产品型号
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 易耗品种类ID，只有易耗品才使用
        /// </summary>
        public long ExpendableTypeID { get; set; }

        /// <summary>
        /// 易耗品单价，只有易耗品才使用
        /// </summary>
        public decimal ExpendablePrice { get; set; }

        /// <summary>
        /// 入库单流水号
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 入库明细流水号
        /// </summary>
        public long ItemID
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// 入库仓库ID
        /// </summary>
        public string WarehouseID
        {
            set { _warehouseid = value; }
            get { return _warehouseid; }
        }
        /// <summary>
        /// 入库仓库名称
        /// </summary>
        public string WarehouseName
        {
            set { _warehousename = value; }
            get { return _warehousename; }
        }
        /// <summary>
        /// 是否固定资产入库
        /// </summary>
        public bool IsAsset
        {
            set { _isasset = value; }
            get { return _isasset; }
        }
        /// <summary>
        /// 设备条形码
        /// </summary>
        public string EquipmentNO
        {
            set { _equipmentno = value; }
            get { return _equipmentno; }
        }
        /// <summary>
        /// 易耗品ID
        /// </summary>
        public long ExpendableID
        {
            set { _expendableid = value; }
            get { return _expendableid; }
        }
        /// <summary>
        /// 入库数量
        /// </summary>
        public decimal Count
        {
            set { _count = value; }
            get { return _count; }
        }
        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime InTime
        {
            set { _longime = value; }
            get { return _longime; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit
        {
            set { _unit = value; }
            get { return _unit; }
        }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentName
        {
            set { _equipmentname = value; }
            get { return _equipmentname; }
        }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string EquipmentModel
        {
            set { _equipmentmodel = value; }
            get { return _equipmentmodel; }
        }
        /// <summary>
        /// 易耗品名称
        /// </summary>
        public string ExpendableName
        {
            set { _expendablename = value; }
            get { return _expendablename; }
        }
        /// <summary>
        /// 易耗品型号
        /// </summary>
        public string ExpendableModel
        {
            set { _expendablemodel = value; }
            get { return _expendablemodel; }
        }
        #endregion Model
        public string GetProductName
        {
            get
            {
                if (_equipmentno == string.Empty)
                    return _expendablename;
                else
                    return _equipmentname;
            }
        }
        public string GetProductModel
        {
            get
            {
                if (_equipmentno == string.Empty)
                    return _expendablemodel;
                else
                    return _equipmentmodel;
            }
        }

        public string ExpendableType
        {
            get { return _expendableType; }
            set { _expendableType = value; }
        }
    }
}
