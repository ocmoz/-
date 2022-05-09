using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Utils;

namespace FM2E.Model.Equipment
{
    public class EquipmentExportInfo
    {
        #region Model

        private string _equipmentno;  //设备条形码编号
        private string _name;  //设备名称
        private string _model;  //设备型号
        private string _unit;  //设备单位
        private string _categoryname;  //设备种类
        private decimal _price;  //设备价格
        private int _count;  //设备数量
        private string _addressname;  //设备地址信息
        private string _detaillocation;  //安装位置
        private string _assertnumber;  //资产编号
        private DateTime _purchasedate;  //采购时间
        private string _serialnum;  //序列号
        private string _remark;  //备注
        private string _companyname;  //公司名称
        private string _systemname;  //系统名称
        private long _maintenancetimes;  //维修次数


        /// <summary>
        /// 
        /// </summary>
        public long MaintenanceTimes
        {
            set { _maintenancetimes = value; }
            get { return _maintenancetimes; }
        }
        /// <summary>
        /// 设备数量
        /// </summary>
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            get { return _companyname; }
            set { _companyname = value; }
        }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName
        {
            get { return _systemname; }
            set { _systemname = value; }
        }

        /// <summary>
        /// 资产编号
        /// </summary>
        public string AssertNumber
        {
            get { return _assertnumber; }
            set { _assertnumber = value; }
        }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNum
        {
            get { return _serialnum; }
            set { _serialnum = value; }
        }

        /// <summary>
        /// 地址信息名称
        /// </summary>
        public string AddressName
        {
            get { return _addressname; }
            set { _addressname = value; }
        }

        /// <summary>
        /// 位置详细信息
        /// </summary>
        public string DetailLocation
        {
            set { _detaillocation = value; }
            get { return _detaillocation; }
        }

        /// <summary>
        /// 设备类型名称
        /// </summary>
        /// 
        public string CategoryName
        {
            set { _categoryname = value; }
            get { return _categoryname; }
        }

        /// <summary>
        /// 设备型号
        /// </summary>
        public string Model
        {
            set { _model = value; }
            get { return _model; }
        }


        /// <summary>
        /// 设备条形码编号
        /// </summary>
        public string EquipmentNO
        {
            set { _equipmentno = value; }
            get { return _equipmentno; }
        }

        /// <summary>
        /// 采购时间
        /// </summary>
        public DateTime PurchaseDate
        {
            set { _purchasedate = value; }
            get { return _purchasedate; }
        }

        /// <summary>
        /// 设备价格
        /// </summary>
        public decimal Price
        {
            set { _price = value; }
            get { return _price; }
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }

        #endregion Model
    }
}
