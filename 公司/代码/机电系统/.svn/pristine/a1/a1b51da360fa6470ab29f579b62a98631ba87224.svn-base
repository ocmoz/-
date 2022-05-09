using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 封装了采购单对应的条码信息，包括条码、产品名称，用于条码打印时候信息的传递
    /// </summary>
    public class PurchaseBarcodeInfo
    {
        private long _id;
        private long _purchaserecordid;
        private long _orderid;
        private short _itemid;
        private string _barcode;
        private string _productname;
        private string _model;

        /// <summary>
        /// 数据库记录自增ID
        /// </summary>
        public long ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 对应的采购记录自增ID
        /// </summary>
        public long PurchaseRecordID
        {
            get { return _purchaserecordid; }
            set { _purchaserecordid = value; }
        }

        /// <summary>
        /// 采购单ID，自增
        /// </summary>
        public long OrderID
        {
            get { return _orderid; }
            set { _orderid = value; }
        }
        /// <summary>
        /// 采购明细项ID，在orderid内的itemid，非自增
        /// </summary>
        public short ItemID
        {
            get { return _itemid; }
            set { _itemid = value; }
        }
        /// <summary>
        /// 条码
        /// </summary>
        public string Barcode
        {
            get { return _barcode; }
            set { _barcode = value; }
        }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName
        {
            get { return _productname; }
            set { _productname = value; }
        }

        /// <summary>
        /// 型号
        /// </summary>
        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

    }
}
