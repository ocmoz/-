using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Basic;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;

namespace FM2E.Model.Insurance
{
    /// <summary>
    /// 保险分类
    /// </summary>
    public enum InsuranceType
    {
        
        [EnumDescription("公共责任险")]
        Public = 1,
        [EnumDescription("财产一切险")]
        Property =2
    }

    [Serializable]
    public class InsuranceInfo
    {
        #region Model

        private long _insuranceId;
        private string _insuranceNo;
        private string _insureTarget;
        private DateTime _startDate;
        private DateTime _endDate;
        private InsuranceType _insuranceType;

        public InsuranceType InsuranceType
        {
            get { return _insuranceType; }
            set { _insuranceType = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public string InsureTarget
        {
            get { return _insureTarget; }
            set { _insureTarget = value; }
        }

        public string InsuranceNo
        {
            get { return _insuranceNo; }
            set { _insuranceNo = value; }
        }

        public long InsuranceId
        {
            get { return _insuranceId; }
            set { _insuranceId = value; }
        }

        #endregion Model
        
    }

    /// <summary>
    /// Insurance搜索实体类
    /// </summary>
    public class InsuranceSearchInfo
    {

        private string _insuranceNo;
        private string _insureTarget;
        private DateTime _startDate;
        private DateTime _endDate;
        private InsuranceType _insuranceType;

        public InsuranceType InsuranceType
        {
            get { return _insuranceType; }
            set { _insuranceType = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public string InsureTarget
        {
            get { return _insureTarget; }
            set { _insureTarget = value; }
        }

        public string InsuranceNo
        {
            get { return _insuranceNo; }
            set { _insuranceNo = value; }
        }

      


    }
}
