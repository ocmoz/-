using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Equipment;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using FM2E.BLL.Utils;

namespace FM2E.BLL.Equipment
{
    /// <summary>
    /// 采购管理逻辑处理类
    /// </summary>
    public class Purchase
    {

        private IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
        /// <summary>
        /// 获取下一个采购单的采购单号
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns>下一个能够分配的采购单号</returns>
        public string GenerateNextPurchaseOrderID(string companyid)
        {
            return SheetNOGenerator.GetSheetNO(companyid, SheetType.EQUIPMENT_PURCHASEAPPLY);
        }

        /// <summary>
        /// 获取订单的下一子订单序号
        /// </summary>
        /// <param name="orderid">订单</param>
        /// <returns>下一序号</returns>
        public short GenerateNextPurchaseOrderSubID(string orderid)
        {
            //找到suborderindex=1,orderid=orderid的记录，获取nextorderindex，然后nextorderindex++
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetNextSubOrderIndex(orderid, 1);
        }

        /// <summary>
        /// 添加采购申请
        /// </summary>
        /// <param name="order">申请单实体</param>
        public long AddPurchaseApply(PurchaseOrderInfo order)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            decimal amount = 0;
            if (order.DetailList != null)
            {
                foreach (PurchaseOrderDetailInfo detail in order.DetailList)
                {
                    amount += detail.PlanAmount;
                }
            }
            order.PlanTotalAmount = amount;
            return dal.InsertPurchaseApply(order);
        }


        /// <summary>
        /// 根据当前的采购单，生成一条快照记录
        /// </summary>
        /// <param name="order">采购单</param>
        /// <param name="type">修改类型</param>
        /// <returns>快照记录</returns>
        public PurchaseOrderModifyInfo GenerateModifyRecord(PurchaseOrderInfo order, PurchaseOrderModifyType type,string modifier)
        {
            PurchaseOrderModifyInfo record = new PurchaseOrderModifyInfo();
            record.CompanyID = order.CompanyID;
            record.ModifyTime = DateTime.Now;
            record.ModifyType = type;
            record.OrderSn = order.ID;
            record.PurchaseOrderID = order.PurchaseOrderID;
            record.SubOrderIndex = order.SubOrderIndex;
            record.Modifier = modifier;
            string content = "";
            int index = 1;
            if (order.DetailList != null && order.DetailList.Count > 0)
            {
                foreach (PurchaseOrderDetailInfo item in order.DetailList)
                {
                    if (index != 1)
                        content += "<br/>";
                    content += string.Format("[{0}]--[{1} {2}]--[{3}*{4}]--[{5}]", item.ItemID, item.ProductName, item.Model, item.PlanCount.ToString("#,0.##"), item.Price.ToString("#,0.##"), item.Remark);
                    //每一行内容分别是 序号-名称[型号]-数量*单价-备注
                    index++;
                }
            }
            record.Content = content;
            return record;
        }

        /// <summary>
        /// 查询申请单，含详情
        /// </summary>
        /// <param name="orderid">申请单数据库ID</param>
        /// <returns>申请单，含详情</returns>
        public PurchaseOrderInfo GetPurchaseOrderByID(long orderid)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            PurchaseOrderInfo order = dal.GetPurchaseOrderByID(orderid);
            if (order.DetailList != null)
                foreach (PurchaseOrderDetailInfo item in order.DetailList)
                {
                    item.BeforeAdjustCount = item.AdjustCount;
                    item.BeforeAdjustPrice = item.AdjustPrice;
                }
            return order;
        }

        /// <summary>
        /// 查询同一个逻辑序列号的申请单
        /// </summary>
        /// <param name="ordersn">申请单序列号</param>
        /// <returns>申请单列表</returns>
        public IList GetPurchaseOrdersBySn(string ordersn)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetPurchaseOrdersBySn(ordersn);
        }

        /// <summary>
        /// 获取申请人所有的申请单-- 不包含已完成
        /// </summary>
        /// <param name="userid">申请人ID</param>
        /// <returns>申请单列表</returns>
        public IList GetPurchaseOrdersByApplier(int pageIndex, int pageSize, out int recordCount, string userid)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetPurchaseOrdersByApplier(pageIndex, pageSize, out recordCount, userid);
        }

        /// <summary>
        /// 获取申请人所有的申请单--已完成
        /// </summary>
        /// <param name="userid">申请人ID</param>
        /// <returns>申请单列表</returns>
        public IList GetPurchaseOrdersByApplierFinish(int pageIndex, int pageSize, out int recordCount, string userid)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetPurchaseOrdersByApplierFinish(pageIndex, pageSize, out recordCount, userid);
        }

        /// <summary>
        /// 获取采购人所有的申请单
        /// </summary>
        /// <param name="userid">采购人ID</param>
        /// <returns>申请单列表</returns>
        public IList GetPurchaseOrdersByPurchaser(int pageIndex, int pageSize, out int recordCount, string userid)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetPurchaseOrdersByPurchaser(pageIndex, pageSize, out recordCount, userid);
        }
        /// <summary>
        /// 获取采购人所有的申请单GetPurchaseOrdersByPurchaserHistory
        /// </summary>
        /// <param name="userid">采购人ID</param>
        /// <returns>申请单列表</returns>
        public IList GetPurchaseOrdersByPurchaserHistory(int pageIndex, int pageSize, out int recordCount, string userid)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetPurchaseOrdersByPurchaserHistory(pageIndex, pageSize, out recordCount, userid);
        }

        /// <summary>
        /// 根据公司，获取所有的申请单
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns>申请单列表</returns>
        public IList GetPurchaseOrdersByCompany(int pageIndex, int pageSize, out int recordCount, string companyid)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetPurchaseOrdersByCompany(pageIndex, pageSize, out recordCount, companyid);
        }


        /// <summary>
        /// 根据公司，获取所有指定状态的申请单
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="status">申请单状态</param>
        /// <returns>申请单列表</returns>
        public IList GetPurchaseOrdersByCompany(int pageIndex, int pageSize, out int recordCount, string companyid, PurchaseOrderStatus status)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetPurchaseOrdersByStatus(pageIndex, pageSize, out recordCount, companyid, status);
        }

        /// <summary>
        /// 根据公司，获取所有待审批的申请单
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="username">审批人</param>
        /// <returns>可以被审批的申请单</returns>
        public IList GetPurchaseOrders2Approval(int pageIndex, int pageSize, out int recordCount, string companyid,string username)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetPurchaseOrders2Approval(pageIndex, pageSize, out recordCount, companyid, username);
        }
        /// <summary>
        /// 根据公司，获取所审批过的申请单
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="username">审批人</param>
        /// <returns>已经被审批的申请单</returns>
        public IList GetPurchaseOrdersApprovalHistory(int pageIndex, int pageSize, out int recordCount, string companyid, string username)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetPurchaseOrdersApprovalHistory(pageIndex, pageSize, out recordCount, companyid, username);
        }
        /// <summary>
        /// 根据公司，获取所有待采购以及正在采购的申请单
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns>进入采购流程的采购单</returns>
        public IList GetPurchaseOrders2Purchase(int pageIndex, int pageSize, out int recordCount, string companyid)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetPurchaseOrders2Purchase(pageIndex, pageSize, out recordCount, companyid);
        }

        /// <summary>
        /// 根据公司，获取所有分派完毕的采购单
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns>完成采购流程的采购单</returns>
        public IList GetPurchaseOrdersPurchaseFinish(int pageIndex, int pageSize, out int recordCount, string companyid)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetPurchaseOrdersPurchaseFinish(pageIndex, pageSize, out recordCount, companyid);
        }
        /// <summary>
        /// 根据仓库ID获取所有待验收的采购单
        /// </summary>
        /// <param name="companyid">仓库ID</param>
        /// <returns>进入验收流程的采购单</returns>
        public IList GetPurchaseOrders2Check(int pageIndex, int pageSize, out int recordCount, string warehousid)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetPurchaseOrders2Check(pageIndex, pageSize, out recordCount, warehousid);
        }

        /// <summary>
        /// 根据仓库ID获取所完成验收的采购单
        /// </summary>
        /// <param name="companyid">仓库ID</param>
        /// <returns>完成验收流程的采购单</returns>
        public IList GetPurchaseOrdersCheckHistory(int pageIndex, int pageSize, out int recordCount, string companyid)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetPurchaseOrdersCheckHistory(pageIndex, pageSize, out recordCount, companyid);
        }

        /// <summary>
        /// 根据仓库ID获取所有待入库的采购单
        /// </summary>
        /// <param name="companyid">仓库ID</param>
        /// <returns>进入入库流程的采购单</returns>
        public IList GetPurchaseOrders2InWarehouse(int pageIndex, int pageSize, out int recordCount, string warehousid)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetPurchaseOrders2InWarehouse(pageIndex, pageSize, out recordCount, warehousid);
        }

        /// <summary>
        /// 根据仓库ID获取所有待入库完毕的采购单
        /// </summary>
        /// <param name="companyid">仓库ID</param>
        /// <returns>入库流程完成的采购单</returns>
        public IList GetPurchaseOrdersInWarehouseHistroy(int pageIndex, int pageSize, out int recordCount, string warehousid)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetPurchaseOrdersInWarehouseHistroy(pageIndex, pageSize, out recordCount, warehousid);
        }


        /// <summary>
        /// 删除申请单
        /// </summary>
        /// <param name="id">申请单ID</param>
        public void DeletePurchase(long id)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            dal.DeletePurchaseOrder(id);
        }

        /// <summary>
        /// 保存申购单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public long SavePurchaseOrder(PurchaseOrderInfo order)
        {
            if (order.ID == 0)
            {
                return AddPurchaseApply(order);
            }
            else
            {
                UpdatePurchaseOrder(order);
                return order.ID;
            }
        }

        /// <summary>
        /// 更新申请单
        /// </summary>
        /// <param name="order">新的申请单信息，含详情</param>
        public void UpdatePurchaseOrder(PurchaseOrderInfo order)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            //首先要记录详情
            order.UpdateTime = DateTime.Now;
            decimal amount = 0;
            if (order.DetailList != null)
            {
                foreach (PurchaseOrderDetailInfo detail in order.DetailList)
                {
                    amount += detail.PlanAmount;
                }
            }
            order.PlanTotalAmount = amount;

            dal.UpdatePurchaseOrder(order);
        }

        /// <summary>
        /// 更新申请单，不含详情
        /// </summary>
        /// <param name="order">新的申请单信息，不含详情</param>
        public void UpdatePurchaseOrderNoDetail(PurchaseOrderInfo order)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            order.UpdateTime = DateTime.Now;
            dal.UpdatePurchaseOrderNoDetail(order);
        }

        /// <summary>
        /// 更新申请单状态
        /// </summary>
        /// <param name="id">申请单ID</param>
        /// <param name="newStatus">新的申请状态</param>
        public void UpdatePurchaseOrder(long id, PurchaseOrderStatus newStatus)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();

            dal.UpdatePurchaseOrderStatus(id, newStatus);
        }

        /// <summary>
        /// 执行审批操作，如果是直接通过的状态，则不用UPDATE原来的ORDER，否则需要UPDATEORDER
        /// </summary>
        /// <param name="record">要添加的审批记录</param>
        /// <param name="order">原来的order</param>
        public void DoApproval(PurchaseOrderInfo order, PurchaseOrderApprovalInfo record)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            string approvalers = order.Approvalers;
            string[] approvalerArray = null;
            if (approvalers != null && approvalers.Length > 0)
            {
                approvalerArray = approvalers.Split('|');
            }
            bool existApprovaler = false;
            if (approvalerArray != null)//把现在这个审批者加到审批列表中
            {
                for (int i = 0; i < approvalerArray.Length; i++)
                {
                    if (approvalerArray[i].ToLower() == record.Approvaler.ToLower())
                    {
                        existApprovaler = true;
                        break;
                    }
                }
            }
            if (!existApprovaler)
            {
                if (order.Approvalers == null || order.Approvalers == "")
                    order.Approvalers = record.Approvaler;
                else
                    order.Approvalers +="|"+ record.Approvaler;
                //加到审批者列表中
            }

            approvalerArray = order.Approvalers.Split('|');

            string nextApprovaler = "";
            switch(record.Result) 
            {
                case PurchaseOrderApprovalResult.PASS://通过 
                    
                    //需要判断是不是第一次审批的，如果不是，则要流到原来的顺序中去
                    for (int i = 0; i < approvalerArray.Length; i++)
                    {
                        if (approvalerArray[i].ToLower() == record.Approvaler.ToLower()&& i<approvalerArray.Length-1)
                        {
                            nextApprovaler = approvalerArray[i+1];
                            break;
                        }
                    }
                    order.Approvaling = nextApprovaler;
                    if (nextApprovaler != "")
                    {
                        order.Status = PurchaseOrderStatus.APPROVALING;//先假设不是最后一个审批者，流到下一个审批者中
                    }
                    else
                    {
                        order.Status = PurchaseOrderStatus.WAITING4PURCHASE;//暂时是一级审批
                        //把审批的单价、数量作为最终的单价数量
                        foreach (PurchaseOrderDetailInfo item in order.DetailList)
                        {
                            item.FinalCount = item.AdjustCount;
                            item.FinalPrice = item.AdjustPrice;
                        }
                    }
                    break;
                case PurchaseOrderApprovalResult.RETURNANDMODIFY://返回修改
                    order.Status = PurchaseOrderStatus.APPROVALANDRETURN;
                    order.Approvaling = "";//流回去申请者中
                    break;
                case PurchaseOrderApprovalResult.NOTPASS://不通过，只需要更新状态，而不需要调整信息的更新
                    order.Status = PurchaseOrderStatus.APPROVALFAILED;
                    order.Approvaling = "";//返回到申请者中，即可终止
                    break;
                default:
                    break;
            }
            order.UpdateTime = DateTime.Now;
            
            UpdatePurchaseOrder(order);

            dal.InsertApprovalRecord(record);  
        }

        /// <summary>
        /// 更新采购项信息，主要用于采购中心分发、采购员采购、验收等操作。
        /// </summary>
        /// <param name="item">采购项</param>
        public void UpdatePurchaseOrderDetail(PurchaseOrderDetailInfo item)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            dal.UpdatePurchaseOrderDetail(item);
        }


        /// <summary>
        /// 添加采购员
        /// </summary>
        /// <param name="p">采购员MODEL对象</param>
        public void InsertPurchaser(PurchaserInfo p)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            dal.InsertPurchaser(p);
        }

        /// <summary>
        /// 更新采购员信息
        /// </summary>
        /// <param name="p">需要更新的采购员对象</param>
        public void UpdatePurchaser(PurchaserInfo p)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            dal.UpdatePurchaser(p);
        }
        /// <summary>
        /// 删除采购员
        /// </summary>
        /// <param name="id">采购员对象ID</param>
        public void DeletePurchaser(long id)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            dal.DeletePurchaser(id);
        }
        /// <summary>
        /// 删除采购员
        /// </summary>
        /// <param name="userid">采购员用户名</param>
        public void DeletePurchaser(string userid)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            dal.DeletePurchaser(userid);
        }
        /// <summary>
        /// 获取采购员信息
        /// </summary>
        /// <param name="userid">采购员用户名</param>
        /// <returns>采购员信息</returns>
        public PurchaserInfo GetPurchaser(string userid)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetPurchaser(userid);
        }
        /// <summary>
        /// 获取采购员信息
        /// </summary>
        /// <param name="id">记录ID</param>
        /// <returns>采购员信息</returns>
        public PurchaserInfo GetPurchaser(long id)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetPurchaser(id);
        }
        /// <summary>
        /// 获取公司下面所有采购员信息
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns>采购员列表</returns>
        public IList GetPurchaserList(string companyid)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetPurchaserList(companyid);
        }
        /// <summary>
        /// 获取所有采购员信息
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns>采购员列表</returns>
        public IList GetAllPurchaserList(string companyid)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetAllPurchaserList(companyid);
        }
        /// <summary>
        /// 获取一个详情实体
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>采购项</returns>
        public PurchaseOrderDetailInfo GetPurchaseOrderDetailItem(long orderid,short itemid)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetPurchaseOrderDetailItem(orderid, itemid);
        }

        /// <summary>
        /// 入库的时候插入条形码记录
        /// </summary>
        /// <param name="id">采购单ID</param>
        /// <param name="itemid">明细项ID</param>
        /// <param name="barcode">条码</param>
        public void InsertBarcodeRecord(long purchaserecordid,long id, short itemid, string barcode,string name,string model)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            dal.InsertBarcodeRecord(purchaserecordid, id, itemid, barcode, name, model);
        }

        /// <summary>
        /// 入库的时候插入条形码记录
        /// </summary>
        public void InsertBarcodeRecord(PurchaseBarcodeInfo item)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            dal.InsertBarcodeRecord(item);
        }

        /// <summary>
        /// 获取基本的条码记录
        /// </summary>
        /// <param name="id">采购单ID</param>
        /// <param name="itemid">明细项ID</param>
        /// <returns>以整体入库的时候获取的条码列表</returns>
        public IList GetBaseBarcodeRecords(long purchaserecordid)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetBaseBarcodeRecords(purchaserecordid);
        }

        /// <summary>
        /// 获取父条码下的子条码列表，前面0~N-3位相同的（即最后两位不需要比较）
        /// </summary>
        /// <param name="id">采购单ID</param>
        /// <param name="itemid">明细项ID</param>
        /// <param name="baseBarcode">父条码</param>
        /// <returns>子条码列表</returns>
        public IList GetChildrenBarcodeRecords(long purchaserecordid, string baseBarcode)
        {
            IPurchase dal = FM2E.DALFactory.EquipmentAccess.CreatePurchase();
            return dal.GetChildrenBarcodeRecords(purchaserecordid, baseBarcode);
        }


        /// <summary>
        /// 查找采购单
        /// </summary>
        /// <param name="info">相关信息</param>
        /// <returns></returns>
        public IList SearchPurchaseOrder(PurchaseOrderSearchInfo info, int currentPageIndex, int pageSize, out int recordCount)
        {
           
            QueryParam p = dal.GenerateSearchInfo(info);
            p.PageIndex = currentPageIndex;
            p.PageSize = pageSize;
            return dal.SearchPurchaseOrder(p, out recordCount);
        }
        public IList SearchPurchaseOrder(QueryParam p,out int recordCount)
        {
            return dal.SearchPurchaseOrder(p, out recordCount);
        }

        /// <summary>
        /// 查找采购单，验收专用
        /// </summary>
        /// <param name="info">相关信息</param>
        /// <returns></returns>
        public IList SearchPurchaseOrder(PurchaseOrderCheckSearchInfo info, int currentPageIndex, int pageSize, out int recordCount)
        {

            QueryParam p = dal.GenerateCheckSearchInfo(info);
            p.PageIndex = currentPageIndex;
            p.PageSize = pageSize;
            return dal.SearchPurchaseOrder(p, out recordCount);
        }

        /// <summary>
        /// 保存采购记录
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public long SavePurchaseRecord(PurchaseRecordInfo record)
        {
            
            if (record.ID == 0)//新的
            {
                return dal.InsertPurchaseRecord(record);
            }
            else//更新
            {
                dal.UpdatePurchaseRecord(record);
                return record.ID;
            }
        }

        /// <summary>
        /// 获取一条采购记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PurchaseRecordInfo GetPurchaseRecordInfo(long id)
        {
            return dal.GetPurchaseRecordInfo(id);
        }
    }
}
