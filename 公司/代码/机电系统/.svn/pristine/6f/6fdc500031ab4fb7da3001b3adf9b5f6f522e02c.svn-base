using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    /// <summary>
    /// 材料类型
    /// </summary>
    public enum MaterialType
    {
        /// <summary>
        /// 未知材料类型
        /// </summary>
        Unknown=0,
        /// <summary>
        /// 机电材料
        /// </summary>
        ElectroMechanical=1,
        /// <summary>
        /// 监控材料
        /// </summary>
        Monitoring=2,
        /// <summary>
        /// 消防材料
        /// </summary>
        FireFlighting=3,
        /// <summary>
        /// 其它材料
        /// </summary>
        Other=4
    }
    /// <summary>
    /// 数量情况
    /// </summary>
    public enum QuantitySituation
    {
        /// <summary>
        /// 未知情况
        /// </summary>
        Unknown=0,
        /// <summary>
        /// 盘盈
        /// </summary>
        InventoryGain=1,
        /// <summary>
        /// 盘亏
        /// </summary>
        InventoryLoss=2,
        /// <summary>
        /// 正常
        /// </summary>
        Normal=3
    }
    /// <summary>
    /// 质量情况
    /// </summary>
    public enum QualitySituation
    {
        /// <summary>
        /// 未知情况
        /// </summary>
        Unknown=0,
        /// <summary>
        /// 正常
        /// </summary>
        Normal=1,
        /// <summary>
        /// 有损耗
        /// </summary>
        ExistedLossy=2,
        /// <summary>
        /// 已损耗
        /// </summary>
        HaveLossed=3,
        /// <summary>
        /// 其它
        /// </summary>
        Other=4
    }
    /// <summary>
    /// 表单登记情况
    /// </summary>
    public enum RegSituation
    {
        /// <summary>
        /// 未知情况
        /// </summary>
        Unknown=0,
        /// <summary>
        /// 规范
        /// </summary>
        Standard=1,
        /// <summary>
        /// 不规范
        /// </summary>
        NonStandard=2,
        /// <summary>
        /// 需改进
        /// </summary>
        Need2Improve=3
    }
    /// <summary>
    /// 仓库检查单状态
    /// </summary>
    public enum WareHouseFormStatus
    {
        /// <summary>
        /// 未知状态
        /// </summary>
        Unknown=0,
        /// <summary>
        /// 草稿
        /// </summary>
        Draft=1,
        /// <summary>
        /// 已提交
        /// </summary>
        Committed=2,
        /// <summary>
        /// 已结束
        /// </summary>
        Finished=3
    }
    public class WareHouseCheckInfo
    {
        #region Model
        private string _formno="";
        private WareHouseFormStatus _status;
        private QualitySituation _qualitysituation;
        private RegSituation _regsituation;
        private string _exceptionsituation = "";
        private string _quantityfeeback = "";
        private string _quantityconfirmer = "";
        private string _qualityfeeback = "";
        private string _qualityconfirmer = "";
        private string _regfeeback = "";
        private string _regconfirmer = "";
        private string _otherfeeback = "";
        private string _companyid = "";
        private string _otherconfirmer = "";
        private DateTime _otherconfirmtime;
        private string _resultconfirmer = "";
        private string _warehouseid = "";
        private string _warehousename = "";
        private DateTime _checkdate;
        private MaterialType _materialtype;
        private string _checker = "";
        private string _checkername = "";
        private string _spotcheck = "";
        private string _stockcount = "";
        private QuantitySituation _quantitysituation;
        private DateTime _updatetime;
        /// <summary>
        /// 表单编号
        /// </summary>
        public string FormNO
        {
            set { _formno = value; }
            get { return _formno; }
        }
        /// <summary>
        /// 仓库检查单状态
        /// </summary>
        public WareHouseFormStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// 质量情况
        /// </summary>
        public QualitySituation QualitySituation
        {
            set { _qualitysituation = value; }
            get { return _qualitysituation; }
        }
        /// <summary>
        /// 表单登记情况
        /// </summary>
        public RegSituation RegSituation
        {
            set { _regsituation = value; }
            get { return _regsituation; }
        }
        /// <summary>
        /// 有无异常情况
        /// </summary>
        public string ExceptionSituation
        {
            set { _exceptionsituation = value; }
            get { return _exceptionsituation; }
        }
        /// <summary>
        /// 数量情况处理意见
        /// </summary>
        public string QuantityFeeBack
        {
            set { _quantityfeeback = value; }
            get { return _quantityfeeback; }
        }
        /// <summary>
        /// 数量情况审核人
        /// </summary>
        public string QuantityConfirmer
        {
            set { _quantityconfirmer = value; }
            get { return _quantityconfirmer; }
        }
        /// <summary>
        /// 质量情况处理意见
        /// </summary>
        public string QualityFeeBack
        {
            set { _qualityfeeback = value; }
            get { return _qualityfeeback; }
        }
        /// <summary>
        /// 质量情况审核人
        /// </summary>
        public string QualityConfirmer
        {
            set { _qualityconfirmer = value; }
            get { return _qualityconfirmer; }
        }
        /// <summary>
        /// 表单登记情况处理意见
        /// </summary>
        public string RegFeeBack
        {
            set { _regfeeback = value; }
            get { return _regfeeback; }
        }
        /// <summary>
        /// 表单登记情况审核人
        /// </summary>
        public string RegConfirmer
        {
            set { _regconfirmer = value; }
            get { return _regconfirmer; }
        }
        /// <summary>
        /// 其它意见
        /// </summary>
        public string OtherFeeBack
        {
            set { _otherfeeback = value; }
            get { return _otherfeeback; }
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 其它意见提供人
        /// </summary>
        public string OtherConfirmer
        {
            set { _otherconfirmer = value; }
            get { return _otherconfirmer; }
        }
        /// <summary>
        /// 其它意见填写时间
        /// </summary>
        public DateTime OtherConfirmTime
        {
            set { _otherconfirmtime = value; }
            get { return _otherconfirmtime; }
        }
        /// <summary>
        /// 检查结果确认人
        /// </summary>
        public string ResultConfirmer
        {
            set { _resultconfirmer = value; }
            get { return _resultconfirmer; }
        }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string WareHouseID
        {
            set { _warehouseid = value; }
            get { return _warehouseid; }
        }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WareHouseName
        {
            set { _warehousename = value; }
            get { return _warehousename; }
        }
        /// <summary>
        /// 检查日期
        /// </summary>
        public DateTime CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        /// <summary>
        /// 材料类型
        /// </summary>
        public MaterialType MaterialType
        {
            set { _materialtype = value; }
            get { return _materialtype; }
        }
        /// <summary>
        /// 检查人员用户名
        /// </summary>
        public string Checker
        {
            set { _checker = value; }
            get { return _checker; }
        }
        /// <summary>
        /// 检查人员姓名
        /// </summary>
        public string CheckerName
        {
            set { _checkername = value; }
            get { return _checkername; }
        }
        /// <summary>
        /// 抽查情况
        /// </summary>
        public string SpotCheck
        {
            set { _spotcheck = value; }
            get { return _spotcheck; }
        }
        /// <summary>
        /// 盘点情况
        /// </summary>
        public string StockCount
        {
            set { _stockcount = value; }
            get { return _stockcount; }
        }
        /// <summary>
        /// 数量情况
        /// </summary>
        public QuantitySituation QuantitySituation
        {
            set { _quantitysituation = value; }
            get { return _quantitysituation; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            get { return _updatetime; }
            set { _updatetime = value; }
        }
        /// <summary>
        /// 仓库检查单状态描述字符串
        /// </summary>
        public string StatusString
        {
            get
            {
                string str="";
                switch (_status)
                {
                    case WareHouseFormStatus.Unknown:
                        str = "未知状态";
                        break;
                    case WareHouseFormStatus.Draft:
                        str = "草稿";
                        break;
                    case WareHouseFormStatus.Committed:
                        str = "已提交";
                        break;
                    case WareHouseFormStatus.Finished:
                        str = "已结束";
                        break;
                
                }
                return str;
            }
        }
        /// <summary>
        /// 材料类型描述字符串
        /// </summary>
        public string MaterialTypeString
        {
            get
            {
                string str = "";
                switch (_materialtype)
                {
                    case MaterialType.Unknown:
                        str = "未知材料类型";
                        break;
                    case MaterialType.ElectroMechanical:
                        str = "机电";
                        break;
                    case MaterialType.Monitoring:
                        str = "监控";
                        break;
                    case MaterialType.FireFlighting:
                        str = "消防";
                        break;
                    case MaterialType.Other:
                        str = "其它";
                        break;
                }
                return str;
            }
        }
        /// <summary>
        /// 数量情况描述字符串
        /// </summary>
        public string QuanlitySituationString
        {
            get
            {
                string str = "";
                switch (_quantitysituation)
                {
                    case QuantitySituation.Unknown:
                        str = "未知情况";
                        break;
                    case QuantitySituation.InventoryGain:
                        str = "盘盈";
                        break;
                    case QuantitySituation.InventoryLoss:
                        str = "盘亏";
                        break;
                    case QuantitySituation.Normal:
                        str = "正常";
                        break;
                }
                return str;
            }
        }
        /// <summary>
        /// 质量情况描述字符串
        /// </summary>
        public string QualitySituationString
        {
            get
            {
                string str = "";
                switch (_qualitysituation)
                {
                    case QualitySituation.Unknown:
                        str = "未知情况";
                        break;
                    case QualitySituation.Normal:
                        str = "正常";
                        break;
                    case QualitySituation.ExistedLossy:
                        str = "有损耗";
                        break;
                    case QualitySituation.HaveLossed:
                        str = "已损耗";
                        break;
                    case QualitySituation.Other:
                        str = "其它";
                        break;
                }
                return str;
            }
        }
        /// <summary>
        /// 表单登记情况描述字符串
        /// </summary>
        public string RegSituationString
        {
            get
            {
                string str = "";
                switch (_regsituation)
                {
                    case RegSituation.Unknown:
                        str = "未知情况";
                        break;
                    case RegSituation.Standard:
                        str = "规范";
                        break;
                    case RegSituation.NonStandard:
                        str = "不规范";
                        break;
                    case RegSituation.Need2Improve:
                        str = "需改进";
                        break;
                }
                return str;
            }
        }
        #endregion Model
    }
}
