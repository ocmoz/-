using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace FM2E.Model.Equipment
{
    public class ScrapApprovalInfo
    {
        private long _scrapid;
        private string _approvalerid;
        private string _approvalerName;
        private bool _result;
        private string _equipmentNO;
        private string _equipmentName;
        private string _feeback;
        private DateTime _approvaldate;
        private string _sheetno;
        private IList _equipmentlist;

        public IList EquipmentList
        {
            set { _equipmentlist = value; }
            get { return _equipmentlist; }
        }

        public string SheetNO
        {
            set { _sheetno = value; }
            get { return _sheetno; }
        }
        public string EquipmentNO
        {
            set { _equipmentNO = value; }
            get { return _equipmentNO; }
        }
        public string EquipmentName
        {
            set { _equipmentName = value; }
            get { return _equipmentName; }
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
        public string ApprovalerID
        {
            set { _approvalerid = value; }
            get { return _approvalerid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ApprovalerName
        {
            set { _approvalerName = value; }
            get { return _approvalerName; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Result
        {
            set { _result = value; }
            get { return _result; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FeeBack
        {
            set { _feeback = value; }
            get { return _feeback; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ApprovalDate
        {
            set { _approvaldate = value; }
            get { return _approvaldate; }
        }
    }
}
