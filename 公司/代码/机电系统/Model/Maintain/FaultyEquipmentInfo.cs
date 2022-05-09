using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Maintain
{
    /// <summary>
    /// 故障设备
    /// </summary>
    [Serializable]
    public class FaultyEquipmentInfo
    {
        private long _id;
        private long _sheetid;
        private string _equipmentno;
        private string _equipmentname;
        private string _systemID;
        private string _systemName;
        private long _addressID;
        private string _addressName;
        /// <summary>
        /// 主键
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 所属故障处理单ID
        /// </summary>
        public long SheetID
        {
            set { _sheetid = value; }
            get { return _sheetid; }
        }
        /// <summary>
        /// 故障设备条形码
        /// </summary>
        public string EquipmentNO
        {
            set { _equipmentno = value; }
            get { return _equipmentno; }
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
        /// 所属系统
        /// </summary>
        public string SystemName
        {
            get { return _systemName; }
            set { _systemName = value; }
        }
        /// <summary>
        /// 所属系统ID
        /// </summary>
        public string SystemID
        {
            get { return _systemID; }
            set { _systemID = value; }
        }
        /// <summary>
        /// 地址名称
        /// </summary>
        public string AddressName
        {
            get { return _addressName; }
            set { _addressName = value; }
        }
        /// <summary>
        /// 地址ID
        /// </summary>
        public long AddressID
        {
            get { return _addressID; }
            set { _addressID = value; }
        }
    }
}
