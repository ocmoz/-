using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 出库申请明细实体类
    /// </summary>
    [Serializable]
    public class OutWarehouseDetailInfo
    {
        #region Model
        private long _id;
        private long _itemid;
        private string _productname;
        private string _model;
        private decimal _count;
        private string _unit;
        private string _usage;
        private string _remark;
        /// <summary>
        /// 出库申请单ID
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 明细项序号
        /// </summary>
        public long ItemID
        {
            set { _itemid = value; }
            get { return _itemid; }
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
        /// 申请数量
        /// </summary>
        public decimal Count
        {
            set { _count = value; }
            get { return _count; }
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
        /// 用途
        /// </summary>
        public string Usage
        {
            set { _usage = value; }
            get { return _usage; }
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
        //private string _sectionid;
        //private string sectionname;
        //private string _locationtag;
        //private string _locationid;
        private string _systemid;
        private string systemname;
        private long _addressid;
        private string _addressname;
        private string _detaillocation;
        ///// <summary>
        ///// 
        ///// </summary>
        //public string SectionID
        //{
        //    set { _sectionid = value; }
        //    get { return _sectionid; }
        //}

        //public string SectionName
        //{
        //    set { sectionname = value; }
        //    get { return sectionname; }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //public string LocationTag
        //{
        //    set { _locationtag = value; }
        //    get { return _locationtag; }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //public string LocationID
        //{
        //    set { _locationid = value; }
        //    get { return _locationid; }
        //}
        /// <summary>
        /// 系统划分ID
        /// </summary>
        public string SystemID
        {
            set { _systemid = value; }
            get { return _systemid; }
        }

        /// <summary>
        /// 系统划分名称
        /// </summary>
        public string SystemName
        {
            set { systemname = value; }
            get { return systemname; }
        }

        /// <summary>
        /// 使用地址代码
        /// </summary>
        public long AddressID
        {
            get { return _addressid; }
            set { _addressid = value; }
        }

        /// <summary>
        /// 使用地址的全称
        /// </summary>
        public string AddressName
        {
            get { return _addressname; }
            set { _addressname = value; }
        }

        /// <summary>
        /// 详细地址补充
        /// </summary>
        public string DetailLocation
        {
            set { _detaillocation = value; }
            get { return _detaillocation; }
        }

        private IList _outequipmentlist = new List<OutEquipmentsInfo>();

        /// <summary>
        /// 出库设备列表
        /// </summary>
        public IList OutEquipmentList
        {
            get { return _outequipmentlist; }
            set { _outequipmentlist = value; }
        }

        /// <summary>
        /// 已出库数量
        /// </summary>
        public decimal OutCount
        {
            get
            {
                decimal count = 0;
                foreach (OutEquipmentsInfo eq in _outequipmentlist)
                {
                    count += eq.Count;
                }
                return count;
            }
        }

        public decimal GetExpandableCount(long expandableid)
        {

            decimal count = 0;
            foreach (OutEquipmentsInfo eq in _outequipmentlist)
            {
                if(eq.ExpendableID==expandableid)
                    count += eq.Count;
            }
            return count;

        }
    }
}
