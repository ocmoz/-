using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    public class ScrapRecordInfo
    {
        private long _scrapid;
        private string _equipmentno;
        private string _equipmentname;
        private DateTime _scraptime;
        private string _scrapreason;
        private long _depid;
        private string _depname;
        private string _sheetno;

       /// <summary>
        /// 
        /// </summary>
        public DateTime ScrapTime
        {
            set { _scraptime = value; }
            get { return _scraptime; }
        }
        public string SheetNO
        {
            set { _sheetno = value; }
            get { return _sheetno; }
        }
        
        public string DepName
        {
            set { _depname = value; }
            get { return _depname; }
        }

        public string EquipmentName
        {
            set { _equipmentname = value; }
            get { return _equipmentname; }
        }

        /// <summary>
        /// 
        /// </summary>
        public long ScrapID
        {
            set { _scrapid = value; }
            get { return _scrapid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long DepID
        {
            set { _depid = value; }
            get { return _depid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string EquipmentNO
        {
            set { _equipmentno = value; }
            get { return _equipmentno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ScrapReason
        {
            set { _scrapreason = value; }
            get { return _scrapreason; }
        }
 
    }
}
