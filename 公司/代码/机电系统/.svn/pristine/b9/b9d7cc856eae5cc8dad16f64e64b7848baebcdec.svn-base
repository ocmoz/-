using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    public class AssetAndDepreciationInfo
    {
        private string _companyid;
        public string CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }

        private string _companyname;
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }


        private long _categoryid;
        public long CategoryID
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }

        private string _categoryname;
        public string CategoryName
        {
            set { _categoryname = value; }
            get { return _categoryname; }
        }

        private string _systemid;
        public string SystemID
        {
            set { _systemid = value; }
            get { return _systemid; }
        }

        private string _systemname;
        public string SystemName
        {
            set { _systemname = value; }
            get { return _systemname; }
        }

        private string _sectionid;
        public string SectionID
        {
            set { _sectionid = value; }
            get { return _sectionid; }
        }

        private string _sectionname;
        public string SectionName
        {
            set { _sectionname = value; }
            get { return _sectionname; }
        }

        private string _locationid;
        public string LocationID
        {
            set { _locationid = value; }
            get { return _locationid; }
        }

        private string _locationname;
        public string LocationName
        {
            set { _locationname = value; }
            get { return _locationname; }
        }

        private string _locationtag;
        public string LocationTag
        {
            set { _locationtag = value; }
            get { return _locationtag; }
        }

        private string serialnum;
        public string SerialNum
        {
            set { serialnum = value; }
            get { return serialnum; }
        }

        private string _Name;
        public string Name
        {
            set { _Name = value; }
            get { return _Name; }
        }

        private string _EquipmentNO;
        public string EquipmentNO
        {
            set { _EquipmentNO = value; }
            get { return _EquipmentNO; }
        }

        private string _Model;
        public string Model
        {
            set { _Model = value; }
            get { return _Model; }
        }

        private decimal _Price;
        public decimal Price
        {
            set { _Price = value; }
            get { return _Price; }
        }

        private decimal _DepreciationPrice;
        public decimal DepreciationPrice
        {
            set { _DepreciationPrice = value; }
            get { return _DepreciationPrice; }
        }

        private string _groupby;
        public string GroupBy
        {
            set {
                switch (value)
                {
                    case "1":
                        {
                            _groupby = "sy.SystemName";
                            break;
                        }
                    case "2":
                        {
                            _groupby = "ca.CategoryName";
                            break;
                        }
                    default:
                        {
                            _groupby = value;
                            break;
                        }
                }
            }
            get { return _groupby; }
        }

        private string _groupby2;
        public string GroupBy2
        {
            set {
                switch (value)
                {
                    case "1":
                        {
                            _groupby2 = "sy.SystemName";
                            break;
                        }
                    case "2":
                        {
                            _groupby2 = "ca.CategoryName";
                            break;
                        }
                    default:
                        {
                            _groupby2 = value;
                            break;
                        }
                }
            }
            get { return _groupby2; }
        }

        private DateTime _PurchaseDate;
        public DateTime PurchaseDate
        {
            set { _PurchaseDate = value; }
            get { return _PurchaseDate; }
        }

        private int _DepreciableLife;
        public int DepreciableLife
        {
            set { _DepreciableLife = value; }
            get { return _DepreciableLife; }
        }

        private decimal _ResidualRate;
        public decimal ResidualRate
        {
            set { _ResidualRate = value; }
            get { return _ResidualRate; }
        }

        private long _DepreciationMethod;
        public long DepreciationMethod
        {
            set { _DepreciationMethod = value; }
            get { return _DepreciationMethod; }
        }
    }
}
