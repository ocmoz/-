using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FM2E.Model.Maintain
{
    public class MaintainRecordAddressInfo
    {
        #region Model
        private string _tablename;
        private long _recordid;
        private long _addressid;
        /// <summary>
        /// 
        /// </summary>
        public string TableName
        {
            set { _tablename = value; }
            get { return _tablename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long RecordID
        {
            set { _recordid = value; }
            get { return _recordid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long AddressID
        {
            set { _addressid = value; }
            get { return _addressid; }
        }
        #endregion Model
    }
}
