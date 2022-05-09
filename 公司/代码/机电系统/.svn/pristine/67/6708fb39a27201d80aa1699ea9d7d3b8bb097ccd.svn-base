using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Maintain
{
    /// <summary>
    /// 根据故障维修的信息来更新设备表所用的实体类
    /// </summary>
    public class MaintainEquipmentsUpdateInfo
    {
        private string equipmentNO;
        private int status;
        private int maintainTimesIncrease;
        private DateTime updateTime;

        /// <summary>
        /// 设备条形码
        /// </summary>
        public string EquipmentNO
        {
            get { return equipmentNO; }
            set { equipmentNO = value; }
        }

        /// <summary>
        /// 设备的状态,如果status=0，则不更新状态
        /// </summary>
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// 增加的维修次数，如果maintainTimesIncrease=0，则不更新维修次数
        /// </summary>
        public int MaintainTimesIncrease
        {
            get { return maintainTimesIncrease; }
            set { maintainTimesIncrease = value; }
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }
    }
}
