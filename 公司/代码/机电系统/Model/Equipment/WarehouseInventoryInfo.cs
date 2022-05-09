using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 仓库盘点
    /// </summary>
    public class WarehouseInventoryInfo
    {
        private string _orderid;
        private long _itemid;
        private string _warehouseid;
        private string _warehousename;
        private string _productname;
        private string _model;
        private decimal _outcount;
        private decimal _incount;
        private decimal _warehousecount;
        private decimal _priceperunit;
        private string _unit;
        private decimal _totalvalue;
        private string _remark;
        private DateTime _inventorytime;
        /// <summary>
        /// 盘点单号码
        /// </summary>
        public string OrderID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 盘点单流水号
        /// </summary>
        public long ItemID
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// 仓库ID
        /// </summary>
        public string WarehouseID
        {
            set { _warehouseid = value; }
            get { return _warehouseid; }
        }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WarehouseName
        {
            set { _warehousename = value; }
            get { return _warehousename; }
        }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        /// <summary>
        /// 产品型号
        /// </summary>
        public string Model
        {
            set { _model = value; }
            get { return _model; }
        }
        /// <summary>
        /// 出库数量
        /// </summary>
        public decimal OutCount
        {
            set { _outcount = value; }
            get { return _outcount; }
        }
        /// <summary>
        /// 入库数量
        /// </summary>
        public decimal InCount
        {
            set { _incount = value; }
            get { return _incount; }
        }
        /// <summary>
        /// 库存数量
        /// </summary>
        public decimal WarehouseCount
        {
            set { _warehousecount = value; }
            get { return _warehousecount; }
        }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal PricePerUnit
        {
            set { _priceperunit = value; }
            get { return _priceperunit; }
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
        /// 库存价值
        /// </summary>
        public decimal TotalValue
        {
            set { _totalvalue = value; }
            get { return _totalvalue; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 盘点时间
        /// </summary>
        public DateTime InventoryTime
        {
            set { _inventorytime = value; }
            get { return _inventorytime; }
        }

    }
}
