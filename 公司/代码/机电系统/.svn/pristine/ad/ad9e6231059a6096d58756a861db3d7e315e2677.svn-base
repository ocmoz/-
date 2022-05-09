using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.IDAL.Equipment
{
    /// <summary>
    /// 采购管理模块数据库访问接口
    /// </summary>
    public interface IPurchase
    {
        /// <summary>
        /// 获取下一子订单的序号
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="suborderindex"></param>
        /// <returns></returns>
        short GetNextSubOrderIndex(string orderid, short suborderindex);

        /// <summary>
        /// 插入采购申请单
        /// </summary>
        /// <param name="order">申请单，含详情</param>
        long InsertPurchaseApply(PurchaseOrderInfo order);

        /// <summary>
        /// 查询申请单，含详情
        /// </summary>
        /// <param name="orderid">申请单数据库ID</param>
        /// <returns>申请单，含详情</returns>
        PurchaseOrderInfo GetPurchaseOrderByID(long orderid);

        /// <summary>
        /// 查询同一个逻辑序列号的申请单
        /// </summary>
        /// <param name="ordersn">申请单序列号</param>
        /// <returns>申请单列表</returns>
        IList GetPurchaseOrdersBySn(string ordersn);

        /// <summary>
        /// 获取申请人所有的申请单--不包含已完成
        /// </summary>
        /// <param name="userid">申请人ID</param>
        /// <returns>申请单列表</returns>
        IList GetPurchaseOrdersByApplier(int pageIndex, int pageSize, out int recordCount, string userid);

        /// <summary>
        /// 获取申请人所有的申请单--已完成
        /// </summary>
        /// <param name="userid">申请人ID</param>
        /// <returns>申请单列表</returns>
        IList GetPurchaseOrdersByApplierFinish(int pageIndex, int pageSize, out int recordCount, string userid);

        /// <summary>
        /// 获取采购员所有的采购单，未完成的
        /// </summary>
        /// <param name="userid">采购员ID</param>
        /// <returns>申请单列表</returns>
        IList GetPurchaseOrdersByPurchaser(int pageIndex, int pageSize, out int recordCount, string userid);
        /// <summary>
        /// 获取采购员所有的采购单，已经完成的
        /// </summary>
        /// <param name="userid">采购员ID</param>
        /// <returns>申请单列表</returns>
        IList GetPurchaseOrdersByPurchaserHistory(int pageIndex, int pageSize, out int recordCount, string userid);

        /// <summary>
        /// 获取某个状态下的所有申请单
        /// </summary>
        /// <param name="status">申请单状态</param>
        /// <returns>申请单列表</returns>
        IList GetPurchaseOrdersByStatus(int pageIndex, int pageSize, out int recordCount,string companyid, PurchaseOrderStatus status);

        /// <summary>
        /// 根据公司，获取所有的申请单
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns>申请单列表</returns>
        IList GetPurchaseOrdersByCompany(int pageIndex, int pageSize, out int recordCount, string companyid);

        /// <summary>
        /// 根据公司，获取所有可以被username审批的申请单
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="username">正在审批的人</param>
        /// <returns>申请单列表</returns>
        IList GetPurchaseOrders2Approval(int pageIndex, int pageSize, out int recordCount, string companyid, string username);
        

        /// <summary>
        /// 根据公司，获取所有被username审批过的申请单
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="username">审批的人</param>
        /// <returns>申请单列表</returns>
        IList GetPurchaseOrdersApprovalHistory(int pageIndex, int pageSize, out int recordCount, string companyid, string username);
        /// <summary>
        /// 删除申请单，只有草稿状态的才能删除
        /// </summary>
        /// <param name="orderid">申请单ID</param>
        void DeletePurchaseOrder(long orderid);

        /// <summary>
        /// 更新采购单信息
        /// </summary>
        /// <param name="order">采购单</param>
        void UpdatePurchaseOrder(PurchaseOrderInfo order);


        /// <summary>
        /// 更新采购单信息，不更新详情
        /// </summary>
        /// <param name="order">采购单</param>
        void UpdatePurchaseOrderNoDetail(PurchaseOrderInfo order);

        /// <summary>
        /// 更新采购单状态
        /// </summary>
        /// <param name="newStatus">采购单的新状态</param>
        void UpdatePurchaseOrderStatus(long id,PurchaseOrderStatus newStatus);

        /// <summary>
        /// 插入新的审批记录
        /// </summary>
        /// <param name="record"></param>
        void InsertApprovalRecord(PurchaseOrderApprovalInfo record);

        /// <summary>
        /// 根据公司，获取所有待采购以及正在采购的申请单
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns>进入采购流程的采购单</returns>
        IList GetPurchaseOrders2Purchase(int pageIndex, int pageSize, out int recordCount, string companyid);

         /// <summary>
        /// 根据公司，获取所有分派完毕的采购单
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns>进入采购流程的采购单</returns>
        IList GetPurchaseOrdersPurchaseFinish(int pageIndex, int pageSize, out int recordCount, string companyid);

        /// <summary>
        /// 根据仓库ID，获取需要进行验收的采购单
        /// </summary>
        /// <param name="companyid">仓库ID</param>
        /// <returns>进入验收流程的采购单</returns>
        IList GetPurchaseOrders2Check(int pageIndex, int pageSize, out int recordCount, string warehouseid);


        /// <summary>
        /// 根据仓库ID获取所完成验收的采购单
        /// </summary>
        /// <param name="companyid">仓库ID</param>
        /// <returns>完成验收流程的采购单</returns>
        IList GetPurchaseOrdersCheckHistory(int pageIndex, int pageSize, out int recordCount, string warehouseid);

        /// <summary>
        /// 根据仓库ID，获取需要进行入库的采购单
        /// </summary>
        /// <param name="companyid">仓库ID</param>
        /// <returns>进入入库流程的采购单</returns>
        IList GetPurchaseOrders2InWarehouse(int pageIndex, int pageSize, out int recordCount, string warehouseid);
        
             /// <summary>
        /// 根据仓库ID，获取入库完毕的采购单
        /// </summary>
        /// <param name="companyid">仓库ID</param>
        /// <returns>入库流程结束的采购单</returns>
        IList GetPurchaseOrdersInWarehouseHistroy(int pageIndex, int pageSize, out int recordCount, string warehouseid);

        /// <summary>
        /// 更新采购项信息，主要用于采购中心分发、采购员采购、验收等操作。
        /// </summary>
        /// <param name="item">采购项</param>
        void UpdatePurchaseOrderDetail(PurchaseOrderDetailInfo item);
        /// <summary>
        /// 获取一个详情实体
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>采购项</returns>
        PurchaseOrderDetailInfo GetPurchaseOrderDetailItem(long orderid,short itemid);
        /// <summary>
        /// 入库的时候插入条形码记录
        /// </summary>
        /// <param name="id">采购单ID</param>
        /// <param name="itemid">明细项ID</param>
        /// <param name="barcode">条码</param>
        /// <param name="name">产品名称</param>
        void InsertBarcodeRecord(long purchaserecordid, long orderid, short itemid, string barcode, string name, string model);


        /// <summary>
        /// 入库的时候插入条形码记录
        /// </summary>
        /// <param name="item">插入到数据库的对象</param>
        void InsertBarcodeRecord(PurchaseBarcodeInfo item);

        /// <summary>
        /// 获取基本的条码记录
        /// </summary>
        /// <param name="id">采购单ID</param>
        /// <param name="itemid">明细项ID</param>
        /// <returns>以整体入库的时候获取的条码列表</returns>
        IList GetBaseBarcodeRecords(long purchaserecordid);

        /// <summary>
        /// 获取所有条码记录
        /// </summary>
        /// <param name="id">采购单ID</param>
        /// <param name="itemid">明细项ID</param>
        /// <returns>以入库的条码列表</returns>
        IList GetBarcodeRecords(long purchaserecordid);

        /// <summary>
        /// 获取父条码下的子条码列表，前面0~N-3位相同的（即最后两位不需要比较）
        /// </summary>
        /// <param name="id">采购单ID</param>
        /// <param name="itemid">明细项ID</param>
        /// <param name="baseBarcode">父条码</param>
        /// <returns>子条码列表</returns>
        IList GetChildrenBarcodeRecords(long purchaserecordid, string baseBarcode);


        /// <summary>
        /// 生成查询信息参数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam GenerateSearchInfo(PurchaseOrderSearchInfo item);

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        IList SearchPurchaseOrder(QueryParam qp, out int recordCount);

        /// <summary>
        /// 插入采购记录
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        long InsertPurchaseRecord(PurchaseRecordInfo record);
        /// <summary>
        /// 更新采购记录 
        /// </summary>
        /// <param name="record"></param>
        void UpdatePurchaseRecord(PurchaseRecordInfo record);

        /// <summary>
        /// 获取一条采购记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PurchaseRecordInfo GetPurchaseRecordInfo(long id);

         /// <summary>
        /// 生成查询信息参数，验收专用
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam GenerateCheckSearchInfo(PurchaseOrderCheckSearchInfo item);
        #region 采购员管理
        /// <summary>
        /// 添加采购员
        /// </summary>
        /// <param name="p">采购员MODEL对象</param>
        void InsertPurchaser(PurchaserInfo p);
        /// <summary>
        /// 更新采购员信息
        /// </summary>
        /// <param name="p">需要更新的采购员对象</param>
        void UpdatePurchaser(PurchaserInfo p);
        /// <summary>
        /// 删除采购员
        /// </summary>
        /// <param name="id">采购员对象ID</param>
        void DeletePurchaser(long id);
        /// <summary>
        /// 删除采购员
        /// </summary>
        /// <param name="userid">采购员用户名</param>
        void DeletePurchaser(string userid);
        /// <summary>
        /// 获取采购员信息
        /// </summary>
        /// <param name="userid">采购员用户名</param>
        /// <returns>采购员信息</returns>
        PurchaserInfo GetPurchaser(string userid);
        /// <summary>
        /// 获取采购员信息
        /// </summary>
        /// <param name="id">记录ID</param>
        /// <returns>采购员信息</returns>
        PurchaserInfo GetPurchaser(long id);
        /// <summary>
        /// 获取公司下面所有采购员信息
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns>采购员列表</returns>
        IList GetPurchaserList(string companyid);
        /// <summary>
        /// 获取所有采购员信息
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns>采购员列表</returns>
        IList GetAllPurchaserList(string companyid);
        #endregion
    }
}
