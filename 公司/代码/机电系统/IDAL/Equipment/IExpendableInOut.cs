using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using System.Collections;
using FM2E.Model.Utils;
using System.Data.Common;

namespace FM2E.IDAL.Equipment
{
    public interface IExpendableInOut
    {
        long InsertOutWarehouseExpendable(OutWarehouseInfo model, InEquipmentsInfo item);

#region 易耗品出库流程

        /// <summary>
        /// 增加一条易耗品出库详情，trans可为空;
        /// </summary>
        long Add(ExpendableInOutRecordInfo model, DbTransaction trans);
        /// <summary>
        /// 增加一条易耗品出库申请;
        /// </summary>
        long InsertOutWarehouseItem(OutWarehouseInfo model,DbTransaction trans );

        /// <summary>
        /// 查询易耗品出口申请单列表条件
        /// </summary>
        QueryParam GenerateSearchTerm(OutWarehouseInfo item, int pageindex, int pagesize);

        QueryParam GetGenerateSearchTerm(OutWarehouseInfo info, int pageindex, int pagesize, string userName);

        /// <summary>
        /// 查询易耗品出口申请单列表
        /// </summary>
        IList SearchOutWarehouseExpendable(QueryParam qp, out int recordCount);

        /// <summary>
        /// 获取易耗品申请单详细（含明细）
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        OutWarehouseInfo GetOutWarehouse(long ID);

        void UpdateOutWarehouse(OutWarehouseInfo model);
#endregion

       /// <summary>
        /// 增加一条入库前数据，trans可为空;
        /// </summary>
        long AddRecord(ExpendableInOutRecordInfo model, DbTransaction trans);

        /// <summary>
        /// 获取查询对象;
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam GenerateSearchTerm(ExpendableInOutRecordSearchInfo item);
        QueryParam GenerateSearchTermOut(ExpendableInOutRecordSearchInfo item);
        /// <summary>
        /// 获取列表;
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList GetList(QueryParam searchTerm, out int recordCount);

        void DelExpendableInOut(long ExpendableID,long ID);

        ExpendableInOutRecordInfo GetExpendableInOut(long ExpendableID);

        IList GetExInOut(String companyid, String warehouseid, DateTime datefrom, DateTime dateto,long categoryid);

        IList GetExInOutYear(String companyid, String warehouseid, DateTime datefrom, DateTime dateto, long categoryid);

        IList GetallInOutRecord(ExpendableInOutRecordType type);

        InOutApproval GetCurrentApprovalStatus();

        Boolean updateCurrentApprovalStatus(InOutApproval item);

        Boolean deleteAllRecord();
 
    }
}
