﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Utils
{
    public class SheetType
    {
        #region 表单类型
        /// <summary>
        /// 借调申请单
        /// </summary>
        public readonly static SheetType SECONDMENT_BORROWAPPLY = new SheetType("FM2E_BorrowApply");

        /// <summary>
        /// 采购申请单
        /// </summary>
        public readonly static SheetType EQUIPMENT_PURCHASEAPPLY = new SheetType("FM2E_PurchasePlan");

        /// <summary>
        /// 报验单（不经过采购申请即拿回来验收的物品）
        /// </summary>
        public readonly static SheetType EQUIPMENT_CHECKACCEPTANCEFORM = new SheetType("FM2E_CheckAcceptance");

        /// <summary>
        /// 报废申请单
        /// </summary>
        public readonly static SheetType SCRAP_SCRAPAPPLY = new SheetType("FM2E_Scrap");

        /// <summary>
        /// 仓库检查单
        /// </summary>
        public readonly static SheetType WAREHOUSE_CHECKFORM = new SheetType("FM2E_WareHouseCheck");

        /// <summary>
        /// 故障处理表
        /// </summary>
        public readonly static SheetType MALFUNCTION_HANDLEFORM = new SheetType("FM2E_MalfunctionHandle");

        /// <summary>
        /// 档案借阅申请表
        /// </summary>
        public readonly static SheetType ARCHIVES_BORROWAPPLY = new SheetType("FM2E_ArchivesBorrowApply");

        /// <summary>
        /// 档案销毁申请表
        /// </summary>
        public readonly static SheetType ARCHIVES_DESTROYAPPLY = new SheetType("FM2E_ArchivesDestroyApply");

        /// <summary>
        /// 易耗品入库表
        /// </summary>
        public readonly static SheetType INWAREHOUSESHEET = new SheetType("FM2E_InWarehouse");

        /// <summary>
        /// 易耗品出库表
        /// </summary>
        public readonly static SheetType OUTWAREHOUSESHEET = new SheetType("FM2E_OutWarehouse");

        /// <summary>
        /// 设备出库申请表
        /// </summary>
        public readonly static SheetType OUTWAREHOUSEAPPLY = new SheetType("FM2E_OutWarehouseApply");

        /// <summary>
        /// 维护记录单
        /// </summary>
        public readonly static SheetType MAINTAINSHEET = new SheetType("FM2E_MaintainSheet");
     
        /// <summary>
        /// 考核表
        /// </summary>
        public readonly static SheetType EXAMINESHEET = new SheetType("FM2E_Examine");
        /// <summary>
        /// 考核结果
        /// </summary>
        public readonly static SheetType EXAMINERESULT = new SheetType("FM2E_ExamineResult");
        #endregion

        public override string ToString()
        {
            return sheetType;
        }
        private string sheetType;
        private SheetType(string type)
        {
            sheetType = type;
        }
    }
}
