﻿using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using FM2E.IDAL.Equipment;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Equipment
{
    /// <summary>
    /// 备品备件/设备出库业务逻辑处理类
    /// </summary>
    public class OutWarehouse
    {
        /// <summary>
        /// 数据库访问对象;
        /// </summary>
        private readonly IOutWarehouseApply dal = FM2E.DALFactory.EquipmentAccess.CreateOutWarehouseApply();

        //public OutWarehouseApplyInfo GetOurWarehouseApply(long ID)
        //{
        //    return dal.GetOutWarehouseApply(ID);
        //}
        //public long InsertOurWarehouseApply(OutWarehouseApplyInfo model)
        //{
        //    IOutWarehouseApply dal = FM2E.DALFactory.EquipmentAccess.CreateOutWarehouseApply();
        //    return dal.InsertOutWarehouseApply(model);
        //}
        //public void UpdateOurWarehouseApply(OutWarehouseApplyInfo model)
        //{
        //    IOutWarehouseApply dal = FM2E.DALFactory.EquipmentAccess.CreateOutWarehouseApply();
        //    dal.UpdateOutWarehouseApply(model);
        //}
        //public void DelOurWarehouseApply(long ID)
        //{
        //    IOutWarehouseApply dal = FM2E.DALFactory.EquipmentAccess.CreateOutWarehouseApply();
        //    dal.DelOutWarehouseApply(ID);
        //}
        //public QueryParam GenerateSearchTerm(OutWarehouseApplyInfo item)
        //{
        //    IOutWarehouseApply dal = FM2E.DALFactory.EquipmentAccess.CreateOutWarehouseApply();
        //    return dal.GenerateSearchTerm(item);
        //}
        //public QueryParam GenerateSearchTerm(OutWarehouseApplyInfo item,string[] WFStates)
        //{
        //    IOutWarehouseApply dal = FM2E.DALFactory.EquipmentAccess.CreateOutWarehouseApply();
        //    return dal.GenerateSearchTerm(item, WFStates);
        //}

        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            return dal.SearchOutWarehouseApply(searchTerm, out recordCount);
        }

        /// <summary>
        /// 查询出库申请单
        /// </summary>
        /// <param name="info">查询申请单参数</param>
        /// <param name="pageindex">页码</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="recordCount">查询结果总记录数</param>
        /// <returns>申请单列表</returns>
        public IList SearchOutWarehouseApply(OutWarehouseApplySearchInfo info, int pageindex, int pagesize, out int recordCount)
        {
            QueryParam qp = dal.GenerateSearchTerm(info, pageindex, pagesize);
            return dal.SearchOutWarehouseApply(qp, out recordCount);
        }      

        /// <summary>
        /// 查询出库申请单（审批人专用）
        /// </summary>
        /// <param name="info">查询申请单参数</param>
        /// <param name="pageindex">页码</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="recordCount">查询结果总记录数</param>
        /// <returns>申请单列表</returns>
        public IList SearchOutWarehouseApply(OutWarehouseApplySearchForApprovalerInfo info, int pageindex, int pagesize, out int recordCount)
        {
            QueryParam qp = dal.GenerateSearchTerm(info, pageindex, pagesize);
            return dal.SearchOutWarehouseApply(qp, out recordCount);
        }

        /// <summary>
        /// 保存出库申请单，新增或者更新
        /// </summary>
        /// <param name="model">出库申请单信息</param>
        /// <returns>出库申请单流水号</returns>
        public long SavaOutWarehouseApply(OutWarehouseApplyInfo model)
        {
            if (model.ID == 0)
            {
                return dal.InsertOutWarehouseApply(model);
            }
            else
            {
                dal.UpdateOutWarehouseApplyWithDetail(model);
                return model.ID;
            }
        }

        /// <summary>
        /// 执行申请单审批
        /// </summary>
        /// <param name="model">申请单审批信息</param>
        /// <returns>审批记录流水号</returns>
        public long DoApproval(OutWarehouseApprovalInfo model)
        {
            return dal.InsertApprovalRecord(model);
        }

        /// <summary>
        /// 获取出库申请单信息
        /// </summary>
        /// <param name="id">出库申请单流水号</param>
        /// <returns>出库申请单所有信息</returns>
        public OutWarehouseApplyInfo GetOutWarehouseApplyInfo(long id)
        {
            return dal.GetOutWarehouseApplyInfo(id);
        }

        /// <summary>
        /// 删除出库申请单
        /// </summary>
        /// <param name="id">出库申请单流水号</param>
        public void DeleteApplyInfo(long id)
        {
            dal.DeleteApplyInfo(id);
        }

        /// <summary>
        /// 执行出库登记
        /// </summary>
        /// <param name="model">出库申请单出库所有信息</param>
        public void DoOutWarehosue(OutWarehouseApplyInfo model)
        {
            dal.UpdateApplyInfoWithEquipmentInsertUpdate(model);
            ExpendableInOut expendableInOutBll = new ExpendableInOut();
            //把易耗品的出库记录记下来
            foreach (OutWarehouseDetailInfo item in model.ApplyDetailList)
            {
                foreach (OutEquipmentsInfo eq in item.OutEquipmentList)
                {
                    if (!eq.IsAsset)
                    {
                        ExpendableInOutRecordInfo record = new ExpendableInOutRecordInfo();
                        record.CompanyID = model.CompanyID;
                        record.Amount = eq.Count;
                        record.CategoryID = 0;
                        record.InOutTime = model.OutTime;
                        record.Model = eq.Model;
                        record.Name = eq.Name;
                        record.Price = 0;
                        record.Receiver = model.Receiver;
                        record.ReceiverName = model.ReceiverName;
                        record.Remark = model.OutWarehouseRemark;
                        record.Type = ExpendableInOutRecordType.Out;
                        record.Unit = eq.Unit;
                        record.WarehouseID = eq.WarehouseID;
                        record.WarehouseKeeper = model.Operator;
                        record.WarehouseKeeperName = model.OperatorName;
                        expendableInOutBll.InsertRecord(record, null);
                    }
                }
            }

        }


        //**********Modified by Xue    For V 3.1.2     2011-10-20****************************************************************************************************
        /// <summary>
        /// 检验是否在出库设备表中存在
        /// </summary>
        /// <param name="eqNo">设备条形码</param>
        /// <returns></returns>
        public bool ExistsOutEquipmentInfoByEquipmentNO(string eqNo)
        {
            return dal.ExistsOutEquipmentInfoByEquipmentNO(eqNo);
        }
        //**********Modification Finished 2011-6-27**********************************************************************************************


      

    }
}
