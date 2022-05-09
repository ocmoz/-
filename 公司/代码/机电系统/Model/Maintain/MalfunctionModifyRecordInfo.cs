using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Maintain
{
    /// <summary>
    /// 故障处理修改历史实体类
    /// </summary>
    public class MalfunctionModifyRecordInfo
    {
        private long _id;
        private long _sheetid;
        private string _modifier;
        private string _modifierName;
        private string _modifydescription;
        private DateTime _modifydate;
        /// <summary>
        /// 主键ID
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 故障处理单ID
        /// </summary>
        public long SheetID
        {
            set { _sheetid = value; }
            get { return _sheetid; }
        }
        /// <summary>
        /// 修改人用户名
        /// </summary>
        public string Modifier
        {
            set { _modifier = value; }
            get { return _modifier; }
        }
        /// <summary>
        /// 修改人姓名
        /// </summary>
        public string ModifierName
        {
            set { _modifierName = value; }
            get { return _modifierName; }
        }
        /// <summary>
        /// 修改描述
        /// </summary>
        public string ModifyDescription
        {
            set { _modifydescription = value; }
            get { return _modifydescription; }
        }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime ModifyDate
        {
            set { _modifydate = value; }
            get { return _modifydate; }
        }
    }
}
