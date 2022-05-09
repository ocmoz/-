using System;
using System.Text;
using System.Collections;
using FM2E.Model.Utils;
namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 实体类ConsumableEquipmentInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class ConsumableEquipmentInfo
    {
        public ConsumableEquipmentInfo()
        { }
        #region Model
        private long _consumableequipmentid;
        private string _consumableequipmentno;
        private string _name;
        private string _systemid;
        private string _serialnum;
        private string _model;
        private string _specification;
        private string _assertnumber;
        private string _unit;
        private int _count;
        private decimal _price;
        private long _supplierid;
        private long _producerid;
        private string _suppliername;
        private string _producername;
        private DateTime _filedate;
        private int _maintenancetimes;
        private string _remark;
        private DateTime _updatetime;
        private IList _consumableequipmentdetaillist;



        public string CompanyID { get; set; }

        public long MaintainDept { get; set; }
        /// <summary>
        /// key
        /// </summary>
        public long ConsumableEquipmentID
        {
            set { _consumableequipmentid = value; }
            get { return _consumableequipmentid; }
        }
        /// <summary>
        /// 设备易耗品编号
        /// </summary>
        public string ConsumableEquipmentNO
        {
            set { _consumableequipmentno = value; }
            get { return _consumableequipmentno; }
        }
        /// <summary>
        /// 设备易耗品名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// FK，所属系统
        /// </summary>
        public string SystemID
        {
            set { _systemid = value; }
            get { return _systemid; }
        }
        /// <summary>
        /// 序列号，可空
        /// </summary>
        public string SerialNum
        {
            set { _serialnum = value; }
            get { return _serialnum; }
        }
        /// <summary>
        /// 型号
        /// </summary>
        public string Model
        {
            set { _model = value; }
            get { return _model; }
        }
        /// <summary>
        /// 规格
        /// </summary>
        public string Specification
        {
            set { _specification = value; }
            get { return _specification; }
        }
        /// <summary>
        /// 资产编码
        /// </summary>
        public string AssertNumber
        {
            set { _assertnumber = value; }
            get { return _assertnumber; }
        }
        /// <summary>
        /// 设备单位
        /// </summary>
        public string Unit
        {
            set { _unit = value; }
            get { return _unit; }
        }
        /// <summary>
        /// 设备数量
        /// </summary>
        public int Count
        {
            set { _count = value; }
            get { return _count; }
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
        /// FK，供应商ID
        /// </summary>
        public long SupplierID
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// FK，生产商ID
        /// </summary>
        public long ProducerID
        {
            set { _producerid = value; }
            get { return _producerid; }
        }
        /// <summary>
        /// 生产商名称
        /// </summary>
        public string SupplierName
        {
            set { _suppliername = value; }
            get { return _suppliername; }
        }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string ProducerName
        {
            set { _producername = value; }
            get { return _producername; }
        }
        /// <summary>
        /// 建档日期
        /// </summary>
        public DateTime FileDate
        {
            set { _filedate = value; }
            get { return _filedate; }
        }
        /// <summary>
        /// 维修次数
        /// </summary>
        public int MaintenanceTimes
        {
            set { _maintenancetimes = value; }
            get { return _maintenancetimes; }
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
        /// 最近更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 易耗品分布情况 列表
        /// </summary>
        public IList ConsumableEquipmentDetailList
        {
            set { _consumableequipmentdetaillist = value; }
            get { return _consumableequipmentdetaillist; }
        }
        #endregion Model

    }
}

