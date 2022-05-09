using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Maintain
{
    [Serializable]
    public class EquipmentCostInfor
    {
        private long id;
        private long sheetId;
        private List<EquipmentCostItems> eqCostItems;
        private decimal eqSumPrice;
        private decimal eqSumApprovalPrice;
        private decimal measureCost;
        private decimal guiCost;
        private decimal taxCost;
        private decimal trafficCost;
        private decimal measureApprovalCost;
        private decimal guiApprovalCost;
        private decimal taxApprovalCost;
        private decimal trafficApprovalCost;
        private decimal sumOtherCost;
        private decimal sumApprovalOtherCost;
        private decimal totalSumCost;
        private decimal totalSumApprovalCost;
        private string isMeasure;
     //   private string measureCostMark;
        private decimal otherCost;
        private string markOne;
        private string markTwo;
        private string markThree;
        private string markFour;
        private string markFive;
        private decimal otherApprovalCost;
        private string isApplyMeasure;
        private string isProvider;
        /// <summary>
        /// ID
        /// </summary>
        public long ID
        {
            set { this.id = value; }
            get { return this.id; }
        }
        /// <summary>
        /// 故障表SheetID
        /// </summary>
        public long SheetID
        {
            set { this.sheetId = value; }
            get { return this.sheetId; }
        }
        /// <summary>
        /// 设备集
        /// </summary>
        public List<EquipmentCostItems> EqCostItems
        {
            set { this.eqCostItems = value; }
            get { return this.eqCostItems; }
        }
        /// <summary>
        /// 小计设备总价
        /// </summary>
        public decimal EqSumPrice
        {
            //set
            //{
            //    if (this.eqCostItems.Count != 0)
            //    {
            //        foreach (EquipmentCostItems item in eqCostItems)
            //        {
            //            this.eqSumPrice += item.EqTotalPrice;
            //        }
            //    }
            //    else
            //    {
            //        this.eqSumPrice = 0;
            //    }
            //}
            set { this.eqSumPrice = value; }
            get { return this.eqSumPrice; }
        }
        /// <summary>
        /// 小计设备审核后总价
        /// </summary>
        public decimal EqSumApprovalPrice
        {
            //set
            //{
            //    if (this.eqCostItems.Count != 0)
            //    {
            //        foreach (EquipmentCostItems item in eqCostItems)
            //        {
            //            this.eqSumApprovalPrice += item.EqApprovalTotalPrice;
            //        }
            //    }
            //    else
            //    {
            //        this.eqSumApprovalPrice = 0;
            //    }
            //}
            set { this.eqSumApprovalPrice = value; }
            get { return this.eqSumApprovalPrice; }
        }
        /// <summary>
        /// 措施费
        /// </summary>
        public decimal MeasureCost
        {
            set { this.measureCost = value; }
            get { return this.measureCost; }
        }

        /// <summary>
        /// 规费
        /// </summary>
        public decimal GuiCost
        {
            set { this.guiCost = value; }
            get { return this.guiCost; }
        }
        /// <summary>
        /// 税费
        /// </summary>
        public decimal TaxCost
        {
            set { this.taxCost = value; }
            get { return this.taxCost; }
        }
        /// <summary>
        /// 交通费
        /// </summary>
        public decimal TrafficCost
        {
            set { this.trafficCost = value; }
            get { return this.trafficCost; }
        }
        /// <summary>
        /// 审核后措施费
        /// </summary>
        public decimal MeasureApprovalCost
        {
            set { this.measureApprovalCost = value; }
            get { return this.measureApprovalCost; }
        }
        /// <summary>
        /// 审核后规费
        /// </summary>
        public decimal GuiApprovalCost
        {
            set { this.guiApprovalCost = value; }
            get { return this.guiApprovalCost; }
        }
        /// <summary>
        /// 审核后税费
        /// </summary>
        public decimal TaxApprovalCost
        {
            set { this.taxApprovalCost = value; }
            get { return this.taxApprovalCost; }
        }
        /// <summary>
        /// 审核后交通费
        /// </summary>
        public decimal TrafficApprovalCost
        {
            set { this.trafficApprovalCost = value; }
            get { return this.trafficApprovalCost; }
        }

        /// <summary>
        /// 小计其他费用总价
        /// </summary>
        public decimal SumOtherCost
        {
            set { this.sumOtherCost = value; }
            get { return this.sumOtherCost; }
        }
        /// <summary>
        /// 小计其他费用审核后总价
        /// </summary>
        public decimal SumApprovalOtherCost
        {
            set { this.sumApprovalOtherCost = value; }
            get { return this.sumApprovalOtherCost; }
        }

        /// <summary>
        /// 合计总价
        /// </summary>
        public decimal TotalSumCost
        {
            set { this.totalSumCost = value; }
            get { return this.totalSumCost; }
        }
        /// <summary>
        /// 合计审核后总价
        /// </summary>
        public decimal TotalSumApprovalCost
        {
            set { this.totalSumApprovalCost = value; }
            get { return this.totalSumApprovalCost; }
        }
        /// 措施费备注
        /// </summary>
       // public string MeasureCostMark
       // {
        //    set { this.measureCostMark = value; }
       //     get { return this.measureCostMark; }
        //}
        /// <summary>
        /// 是否计量
        /// </summary>
        public string IsMeasure
        {
            set { this.isMeasure = value; }
            get { return this.isMeasure; }
        }
        
        public decimal OtherCost
        {
            set { this.otherCost = value; }
            get { return this.otherCost; }
        }
        public decimal OtherApprovalCost
        {
            set { this.otherApprovalCost = value; }
            get { return this.otherApprovalCost; }
        }
        public string MarkOne
        {
            set { this.markOne = value; }
            get { return this.markOne; }
        }
        public string MarkTwo
        {
            set { this.markTwo = value; }
            get { return this.markTwo; }
        }

        public string MarkThree
        {
            set { this.markThree = value; }
            get { return this.markThree; }
        }
        public string MarkFour
        {
            set { this.markFour = value; }
            get { return this.markFour; }
        }
        public string MarkFive
        {
            set { this.markFive = value; }
            get { return this.markFive; }
        }
        /// <summary>
        /// 是否申请计量
        /// </summary>
        public string IsApplyMeasure
        {
            set { this.isApplyMeasure = value; }
            get { return this.isApplyMeasure; }
        }
        /// <summary>
        /// 是否甲供
        /// </summary>
        public string IsProvider
        {
            set { this.isProvider = value; }
            get { return this.isProvider; }
        }
    }

    [Serializable]
    public class EquipmentCostItems
    {
        private long id;
        private long costId;
        private string eqName;
        private string eqModel;
        private string eqUnit;
        private int eqNum;
        private decimal eqUnitPrice;
        private decimal eqTotalPrice;
        private decimal eqApprovalUnitPrice;
        private decimal eqApprovalTotalPrice;
        private string eqRemark;


        /// <summary>
        /// ID
        /// </summary>
        public long ID
        {
            set { this.id = value; }
            get { return this.id; }
        }
        /// <summary>
        /// Cost表ID
        /// </summary>
        public long CostID
        {
            set { this.costId = value; }
            get { return this.costId; }
        }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string EqName
        {
            set { this.eqName = value; }
            get { return this.eqName; }
        }
        /// <summary>
        /// 型号
        /// </summary>
        public string EqModel
        {
            set { this.eqModel = value; }
            get { return this.eqModel; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string EqUnit
        {
            set { this.eqUnit = value; }
            get { return this.eqUnit; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int EqNum
        {
            set { this.eqNum = value; }
            get { return this.eqNum; }
        }
        /// <summary>
        /// 综合单价
        /// </summary>
        public decimal EqUnitPrice
        {
            set { this.eqUnitPrice = value; }
            get { return this.eqUnitPrice; }
        }
        /// <summary>
        /// 合价
        /// </summary>
        public decimal EqTotalPrice
        {
            set { this.eqTotalPrice = value; }
            get { return this.eqTotalPrice; }
        }
        /// <summary>
        /// 审核综合单价
        /// </summary>
        public decimal EqApprovalUnitPrice
        {
            set { this.eqApprovalUnitPrice = value; }
            get { return this.eqApprovalUnitPrice; }
        }
        /// <summary>
        /// 审核合价
        /// </summary>
        public decimal EqApprovalTotalPrice
        {
            set { this.eqApprovalTotalPrice = value; }
            get { return this.eqApprovalTotalPrice; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string EqRemark
        {
            set { this.eqRemark = value; }
            get { return this.eqRemark; }
        }

    }
}
