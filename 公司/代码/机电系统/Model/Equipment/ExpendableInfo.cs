using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    public class ExpendableInfo
    {
        #region Model
        private long _expendableid;
        private DateTime _updatetime;
        private string _name = "";
        private string _companyid;
        private string _companyname;
        private string _warehouseid;
        private string _warehousename;
        private string _photourl;
        private string _model = "";
        private string _specification;
        private decimal _count;
        private string _unit;
        private string _remark;
        private long _categoryid;
        private string _categorycode;
        private string _categoryname;
        private decimal _price;
        /// <summary>
        /// 
        /// </summary>
        public long ExpendableID
        {
            set { _expendableid = value; }
            get { return _expendableid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// /// <summary>
        /// 
        /// </summary>
        public string WarehouseID
        {
            set { _warehouseid = value; }
            get { return _warehouseid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WarehouseName
        {
            set { _warehousename = value; }
            get { return _warehousename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PhotoUrl
        {
            set { _photourl = value; }
            get { return _photourl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Model
        {
            set { _model = value; }
            get { return _model; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Specification
        {
            set { _specification = value; }
            get { return _specification; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Count
        {
            set { _count = value; }
            get { return _count; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Unit
        {
            set { _unit = value; }
            get { return _unit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long CategoryID
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CategoryCode
        {
            set { _categorycode = value; }
            get { return _categorycode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CategoryName
        {
            set { _categoryname = value; }
            get { return _categoryname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RemarkString
        {
            get
            {
                if (_remark != null)
                {
                    if (_remark.Length > 10)
                        return _remark.Substring(0, 10) + "...";
                    else
                        return _remark;
                }
                else return null;
            }
        }

        #endregion Model
    }
}
