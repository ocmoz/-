using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 设备验收记录查询实体类
    /// </summary>
    public class ReturnAcceptanceSearchInfo
    {
        private string equipmentNO;
        private string equipmentName;
        private string sheetNO;
        private string companyID;
        private string returnCompany;
        private int result;
        private DateTime checkDateFrom;
        private DateTime checkDateTo;

        /// <summary>
        /// 设备条形码
        /// </summary>
        public string EquipmentNO
        {
            get { return equipmentNO; }
            set { equipmentNO = value; }
        }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentName
        {
            get { return equipmentName; }
            set { equipmentName = value; }
        }
        /// <summary>
        /// 表单编号
        /// </summary>
        public string SheetNO
        {
            get { return sheetNO; }
            set { sheetNO = value; }
        }
        /// <summary>
        /// 验收方公司
        /// </summary>
        public string CompanyID
        {
            get { return companyID; }
            set { companyID = value; }
        }
        /// <summary>
        /// 归还公司的ID
        /// </summary>
        public string ReturnCompany
        {
            get { return returnCompany; }
            set { returnCompany = value; }
        }
        /// <summary>
        /// 验收结果
        /// </summary>
        public int Result
        {
            get { return result; }
            set { result = value; }
        }
        /// <summary>
        /// 验收日期
        /// </summary>
        public DateTime CheckDateFrom
        {
            get { return checkDateFrom; }
            set { checkDateFrom = value; }
        }
        /// <summary>
        /// 验收日期
        /// </summary>
        public DateTime CheckDateTo
        {
            get { return checkDateTo; }
            set { checkDateTo = value; }
        }
    }
}
