using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Maintain
{
    /// <summary>
    /// 维护模板设备实体类
    /// </summary>
    public class TemplateSheetEquipmentInfo
    {
        #region Model
        private long _id;
        private long _templatesheetid;
        private long _equipmentid;
        private string _equipmentno = "";
        private string _equipmentname = "";
        private string _equipmentmodel = "";
        private long _addressid;
        private string _addressname = "";
        private string _detaillocation = "";
        private string _remark = "";
        /// <summary>
        /// 主键
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 维护模板表ID
        /// </summary>
        public long TemplateSheetID
        {
            set { _templatesheetid = value; }
            get { return _templatesheetid; }
        }
        /// <summary>
        /// 设备ID
        /// </summary>
        public long EquipmentID
        {
            set { _equipmentid = value; }
            get { return _equipmentid; }
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
        /// 地址信息ID
        /// </summary>
        public long AddressID
        {
            set { _addressid = value; }
            get { return _addressid; }
        }
        /// <summary>
        /// 地址信息名称
        /// </summary>
        public string AddressName
        {
            set { _addressname = value; }
            get { return _addressname; }
        }
        /// <summary>
        /// 设备安装位置
        /// </summary>
        public string DetailLocation
        {
            set { _detaillocation = value; }
            get { return _detaillocation; }
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
