using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace FM2E.Model.Budget
{
    [Serializable]
    public class BudgetDetailInfo
    {
        #region Model
        private long _detailid;
        private string _system;
        private long _budgetpermonthid;
        private string _expenditurename;
        private decimal _expenditure;
        private string _review;
        private string _expendituredetail;
        private string _manager;
        private string _remarks;
        private DateTime _recorddate;
        private decimal _BudgetApprove;
        private int _year;
        private int _month;
        private long _SubID;
        private string _Approvaler;
        private string _Attachment;
        private long _TotalID;
        private string _SubName;
        private decimal _RealExpenditure;
        private string _expenditurestr;
        private string _BudgetApprovestr;
        private string _companyid;
        private string _companyname;
        private string _Supplier;
        private IList _BudgetDetailList;
        private IList _CompanyList;
        private int _startyear;
        private int _endyear;
        private int _startmonth;
        private int _endmonth;
        private string _title;
        private IList _totallist;
        private DateTime _paydate;
        private Hashtable _ht;


        public Hashtable Ht
        {
            set { _ht = value; }
            get { return _ht; }
        }

        public DateTime PayDate
        {
            set { _paydate = value; }
            get { return _paydate; }
        }
        /// <summary>
        /// 小计
        /// </summary>
        public IList Totallist
        {
            set { _totallist = value; }
            get { return _totallist; }
        }

        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }

        public int StartYear
        {
            set { _startyear = value; }
            get { return _startyear; }
        }
        public int EndYear
        {
            set { _endyear = value; }
            get { return _endyear; }
        }
        public int StartMonth
        {
            set { _startmonth = value; }
            get { return _startmonth; }
        }
        public int EndMonth
        {
            set { _endmonth = value; }
            get { return _endmonth; }
        }


        public IList CompanyList
        {
            set { _CompanyList = value; }
            get { return _CompanyList; }
        }
       

        /// <summary>
        /// 详细开支列表
        /// </summary>
        public IList BudgetDetailList
        {
            set { _BudgetDetailList = value; }
            get { return _BudgetDetailList; }
        }

        public string Supplier
        {
            set { _Supplier = value; }
            get { return _Supplier != null?_Supplier:string.Empty; }
        }

        public static BudgetDetailInfo CloneObject(BudgetDetailInfo obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                stream.Position = 0;
                formatter.Serialize(stream, obj);
                stream.Position = 0;
                return (BudgetDetailInfo)formatter.Deserialize(stream);
            }
        }   

        //public BudgetDetailInfo(BudgetDetailInfo item)
        //{
        //    _detailid = item.DetailID;
        //    _system = item.System;
        //    _budgetpermonthid = item.BudgetPermonthID;
        //    _expenditurename = item

        //}

        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }

        public string CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }

        public string BudgetApproveStr
        {
            set { _BudgetApprovestr = value; }
            get { return _BudgetApprovestr; }
        }

        public string ExpenditureStr
        {
            set { _expenditurestr = value; }
            get { return _expenditurestr; }
        }

        public decimal RealExpenditure
        {
            set { _RealExpenditure = value; }
            get { return _RealExpenditure; }
        }

        public string SubName
        {
            set { _SubName = value; }
            get { return _SubName; }
        }

        public long TotalID
        {
            set { _TotalID = value; }
            get { return _TotalID; }
        }

        public string Attachment
        {
            set { _Attachment = value; }
            get { return _Attachment; }
        }


        public string Approvaler
        {
            set { _Approvaler = value; }
            get { return _Approvaler; }
        }

        public long SubID
        {
            set { _SubID = value; }
            get { return _SubID; }
        }


        public int Month
        {
            set { _month = value; }
            get { return _month; }
        }


        public int Year
        {
            set { _year = value; }
            get { return _year; }
        }


        public decimal BudgetApprove
        {
            set { _BudgetApprove = value; }
            get { return _BudgetApprove; }
        }
        /// <summary>
        /// 预算明细编号
        /// </summary>
        public long DetailID
        {
            set { _detailid = value; }
            get { return _detailid; }
        }
        /// <summary>
        /// 所属系统
        /// </summary>
        public string System
        {
            set { _system = value; }
            get { return _system; }
        }
        /// <summary>
        /// 上级明细编号
        /// </summary>
        public long BudgetPermonthID
        {
            set { _budgetpermonthid = value; }
            get { return _budgetpermonthid; }
        }
        /// <summary>
        /// 开支项目
        /// </summary>
        public string ExpenditureName
        {
            set { _expenditurename = value; }
            get { return _expenditurename; }
        }
        /// <summary>
        /// 开支金额
        /// </summary>
        public decimal Expenditure
        {
            set { _expenditure = value; }
            get { return _expenditure; }
        }
        /// <summary>
        /// 审核状态
        /// </summary>
        public string Review
        {
            set { _review = value; }
            get { return _review; }
        }
        /// <summary>
        /// 开支内容及其依据
        /// </summary>
        public string ExpenditureDetail
        {
            set { _expendituredetail = value; }
            get { return _expendituredetail; }
        }
        /// <summary>
        /// 经办人
        /// </summary>
        public string Manager
        {
            set { _manager = value; }
            get { return _manager; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        /// <summary>
        /// 交付日期
        /// </summary>
        public DateTime RecordDate
        {
            set { _recorddate = value; }
            get { return _recorddate; }
        }
        #endregion Model
    }
}
