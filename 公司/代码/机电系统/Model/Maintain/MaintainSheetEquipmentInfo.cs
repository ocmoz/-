using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;

namespace FM2E.Model.Maintain
{
    /// <summary>
    /// 维护表设备实体类
    /// </summary>
    public class MaintainSheetEquipmentInfo
    {
        #region Model
        private long _id;
        private long _sheetid;
        private long _equipmentid;
        private string _equipmentno;
        private string _equipmentname;
        private string _equipmentmodel;
        private long _addressid;
        private string _addressname = "";
        private string _detaillocation;
        private EquipmentStatus _newstatus;
        private bool _isnormal;
        private bool _isextra;
        private string _remark;
        /// <summary>
        /// 主键
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 维护表ID
        /// </summary>
        public long SheetID
        {
            set { _sheetid = value; }
            get { return _sheetid; }
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
        /// 设备状态
        /// </summary>
        public EquipmentStatus NewStatus
        {
            set { _newstatus = value; }
            get { return _newstatus; }
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
        /// 是否正常
        /// </summary>
        public bool IsNormal
        {
            get { return _isnormal; }
            set { _isnormal = value; }
        }
        /// <summary>
        /// 是否额外设备
        /// </summary>
        public bool IsExtra
        {
            get { return _isextra; }
            set { _isextra = value; }
        }
        #endregion Model

        #region from view
        /// <summary>
        /// 维护时间（FROM VIEW)
        /// </summary>
        public DateTime MaintainTime { get; set; }
        /// <summary>
        /// 维护类型（from VIEW)
        /// </summary>
        public MaintainType MaintainType { get; set; }
        /// <summary>
        /// 维护人ID
        /// </summary>
        public string MaintainerID { get; set; }
        /// <summary>
        /// 维护人姓名
        /// </summary>
        public string MaintainerName { get; set; }
        #endregion
    }

    /// <summary>
    /// 维护表设备搜索实体
    /// </summary>
    public class MaintainSheetEquipmentSearchInfo
    {
        #region Model
        private long _id;
        private long _sheetid;
        private string _equipmentno;
        private string _equipmentname;
        private string _equipmentmodel;
        private string _detaillocation;
        private EquipmentStatus _newstatus;
        private bool _isnormal;
        private bool _isextra;
        private string _remark;
        /// <summary>
        /// 主键
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 维护表ID
        /// </summary>
        public long SheetID
        {
            set { _sheetid = value; }
            get { return _sheetid; }
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
        /// 设备安装位置
        /// </summary>
        public string DetailLocation
        {
            set { _detaillocation = value; }
            get { return _detaillocation; }
        }
        /// <summary>
        /// 设备状态
        /// </summary>
        public EquipmentStatus NewStatus
        {
            set { _newstatus = value; }
            get { return _newstatus; }
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
        /// 是否正常
        /// </summary>
        public bool IsNormal
        {
            get { return _isnormal; }
            set { _isnormal = value; }
        }
        /// <summary>
        /// 是否额外设备
        /// </summary>
        public bool IsExtra
        {
            get { return _isextra; }
            set { _isextra = value; }
        }
        #endregion Model

        #region from view
        /// <summary>
        /// 维护时间（FROM VIEW)
        /// </summary>
        public DateTime MaintainTime { get; set; }
        /// <summary>
        /// 维护类型（from VIEW)
        /// </summary>
        public MaintainType MaintainType { get; set; }
        /// <summary>
        /// 维护人ID
        /// </summary>
        public string MaintainerID { get; set; }
        /// <summary>
        /// 维护人姓名
        /// </summary>
        public string MaintainerName { get; set; }
        #endregion
    }
}
