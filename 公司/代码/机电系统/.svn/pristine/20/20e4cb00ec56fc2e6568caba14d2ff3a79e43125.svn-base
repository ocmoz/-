using System;
using FM2E.Model.Utils;

namespace FM2E.Model.Insurance
{
    /// <summary>
    /// 出险类型
    /// </summary>
    public enum RiskType
    {

        [EnumDescription("被盗")]
        Steal = 1,
        [EnumDescription("雷击")]
        Thunder = 2,
        [EnumDescription("风毁或雨毁")]
        Wind = 3,
        [EnumDescription("栏杆砸车")]
        Rail = 4,
        [EnumDescription("其他")]
        Other = 5
    }

    public enum State
    {
        [EnumDescription("新增")]
        New = 1,
        [EnumDescription("已修复")]
        Repaired = 2,
        [EnumDescription("已复核")]
        Reviewed = 3

    }

    [Serializable]
    public class InsuranceReportInfo
    {
        #region Model

        private long _id;
        private string _reportNo;
        private DateTime _reportDate;
        private string _insuranceNo;
        private RiskType _riskType;
        private string _riskTypeName;
        private DateTime _riskDate;
        private string _riskContent;

           private string   _receiptNo;
            private string   _estimate;
            private string   _claim;
            private string _address;

        private string _riskAttachment;
        private string _operator;
        private string _repairContent;
        private string _repairAttachment;
        private string _stationManager;
        private string _reviewContent;
        private string _insuranceManager;
        private State _state;

        public State State
        {
            get { return _state; }
            set { _state = value; }
        }

        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string InsuranceManager
        {
            get { return _insuranceManager; }
            set { _insuranceManager = value; }
        }

        public string ReviewContent
        {
            get { return _reviewContent; }
            set { _reviewContent = value; }
        }

        public string StationManager
        {
            get { return _stationManager; }
            set { _stationManager = value; }
        }

        public string RepairAttachment
        {
            get { return _repairAttachment; }
            set { _repairAttachment = value; }
        }

        public string RepairContent
        {
            get { return _repairContent; }
            set { _repairContent = value; }
        }

        public string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

        public string RiskAttachment
        {
            get { return _riskAttachment; }
            set { _riskAttachment = value; }
        }

        public string RiskContent
        {
            get { return _riskContent; }
            set { _riskContent = value; }
        }
     
        public string ReceiptNo
        {
            get { return _receiptNo; }
            set { _receiptNo = value; }
        }
        public string Estimate
        {
            get { return _estimate; }
            set { _estimate = value; }
        }
            public string Claim
        {
            get { return _claim; }
            set { _claim = value; }
        }
            public string Address
        {
            get { return _address; }
            set { _address = value; }
        }


        public DateTime RiskDate
        {
            get { return _riskDate; }
            set { _riskDate = value; }
        }

        public string RiskTypeName
        {
            get { return _riskTypeName; }
            set { _riskTypeName = value; }
        }

        public RiskType RiskType
        {
            get { return _riskType; }
            set { _riskType = value; }
        }

        public DateTime ReportDate
        {
            get { return _reportDate; }
            set { _reportDate = value; }
        }

        public string ReportNo
        {
            get { return _reportNo; }
            set { _reportNo = value; }
        }

        public string InsuranceNo
        {
            get { return _insuranceNo; }
            set { _insuranceNo = value; }
        }

        #endregion Model

    }
    /// <summary>
    /// 附件绑定实体类
    /// </summary>
    public class CurrentStatusFile
    {
        public CurrentStatusFile(int _editreason1id, string _name, string _address)
        {
            this.Editreason1id = _editreason1id;
            this.Address = _address;
            this.Name = _name;
        }
        private int _editreason1id;
        private string _address;
        private string _name;

        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public int Editreason1id
        {
            get
            {
                return _editreason1id;
            }
            set
            {
                _editreason1id = value;
            }
        }
    }
    /// <summary>
    /// Insurance搜索实体类
    /// </summary>
    public class InsuranceReportSearchInfo
    {

       
        private string _reportNo;
        private DateTime _startReportDate;
        private DateTime _endReportDate;
        private string _insuranceNo;
        private RiskType _riskType;
        private DateTime _startRiskDate;
        private DateTime _endRiskDate;
        private State _state;

        public string ReportNo
        {
            get { return _reportNo; }
            set { _reportNo = value; }
        }

        public DateTime StartReportDate
        {
            get { return _startReportDate; }
            set { _startReportDate = value; }
        }

        public DateTime EndReportDate
        {
            get { return _endReportDate; }
            set { _endReportDate = value; }
        }

        public string InsuranceNo
        {
            get { return _insuranceNo; }
            set { _insuranceNo = value; }
        }

        public RiskType RiskType
        {
            get { return _riskType; }
            set { _riskType = value; }
        }

        public DateTime StartRiskDate
        {
            get { return _startRiskDate; }
            set { _startRiskDate = value; }
        }

        public DateTime EndRiskDate
        {
            get { return _endRiskDate; }
            set { _endRiskDate = value; }
        }

        public State State
        {
            get { return _state; }
            set { _state = value; }
        }
    }
}
