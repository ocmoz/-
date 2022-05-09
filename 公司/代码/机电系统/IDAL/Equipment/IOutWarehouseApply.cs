using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.IDAL.Equipment
{
    public interface IOutWarehouseApply
    {
        QueryParam GenerateSearchTerm(OutWarehouseApplySearchInfo info, int pageindex, int pagesize);
        QueryParam GenerateSearchTerm(OutWarehouseApplySearchForApprovalerInfo info, int pageindex, int pagesize);
        IList SearchOutWarehouseApply(QueryParam qp, out int recordCount);
     

        long InsertOutWarehouseApply(OutWarehouseApplyInfo model);
        long InsertApprovalRecord(OutWarehouseApprovalInfo model);

        void UpdateOutWarehouseApplyWithDetail(OutWarehouseApplyInfo model);

        OutWarehouseApplyInfo GetOutWarehouseApplyInfo(long id);

        void DeleteApplyInfo(long id);

        /// <summary>
        /// 更新主表信息，插入出库记录，并且更新设备位置和易耗品数量
        /// </summary>
        /// <param name="model"></param>
        void UpdateApplyInfoWithEquipmentInsertUpdate(OutWarehouseApplyInfo model);

        //**********Modified by Xue    For V 3.1.2     2011-10-20****************************************************************************************************
        /// <summary>
        /// 检验是否在出库设备表中存在
        /// </summary>
        /// <param name="eqNo">设备条形码</param>
        /// <returns></returns>
        bool ExistsOutEquipmentInfoByEquipmentNO(string eqNo);
        //**********Modification Finished 2011-6-27**********************************************************************************************
    }
}
