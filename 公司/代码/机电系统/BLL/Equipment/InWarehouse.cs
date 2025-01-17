﻿using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using FM2E.IDAL.Equipment;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Equipment
{
    public class InWarehouse
    {
        /// <summary>
        /// 获取入库单信息，含明细
        /// </summary>
        /// <param name="ID">入库单流水号</param>
        /// <returns>入库单信息（含明细）</returns>
        public InWarehouseInfo GetInWarehouse(long ID)
        {
            IInWarehouse dal = FM2E.DALFactory.EquipmentAccess.CreateInWarehouse();
            return dal.GetInWarehouse(ID);
        }
        #region 易耗品
        /// <summary>
        /// 查询易耗品入库申请单
        /// </summary>
        /// <param name="info">查询申请单参数</param>
        /// <param name="pageindex">页码</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="recordCount">查询结果总记录数</param>
        /// <returns>申请单列表</returns>
        public IList SearchInWarehouseApply(InWarehouseInfo info, int pageindex, int pagesize, out int recordCount)
        {
            IInWarehouse dal = FM2E.DALFactory.EquipmentAccess.CreateInWarehouse();
            QueryParam qp = dal.GenerateSearchTerm(info, pageindex, pagesize);
            return dal.SearchInWarehouseApply(qp, out recordCount);
        }


        /// <summary>
        /// 查询易耗品入库申请单(审批专用）
        /// </summary>
        /// <param name="info">查询申请单参数</param>
        /// <param name="pageindex">页码</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="recordCount">查询结果总记录数</param>
        /// <returns>申请单列表</returns>
        public IList GetSearchInWarehouseApply(InWarehouseInfo info, int pageindex, int pagesize, string userName, out int recordCount)
        {
            IInWarehouse dal = FM2E.DALFactory.EquipmentAccess.CreateInWarehouse();
            QueryParam qp = dal.GetGenerateSearchTerm(info, pageindex, pagesize, userName);
            return dal.SearchInWarehouseApply(qp, out recordCount);
        }
        #endregion

        #region 添加
        /// <summary>
        /// 添加入库单信息
        /// </summary>
        /// <param name="model">入库单主信息</param>
        /// <param name="list">入库单明细列表</param>
        public void InsertInWarehouse(InWarehouseInfo model,ArrayList list)
        {
            IInWarehouse dal = FM2E.DALFactory.EquipmentAccess.CreateInWarehouse();
            dal.InsertInWarehouse(model,list);
        }

        /// <summary>
        /// MODEL中带有入库详情
        /// </summary>
        /// <param name="model">新增入库的对象</param>
        public void InsertInWarehouseWithDetail(InWarehouseInfo model)
        {
            IInWarehouse dal = FM2E.DALFactory.EquipmentAccess.CreateInWarehouse();

            ExpendableInOut expendableInOutBll = new ExpendableInOut();
            dal.InsertInWarehouseWithDetail(model);
            //把易耗品的入库记录记下来
            foreach (InEquipmentsInfo eq in model.InWarehouseList)
            {
                if (!eq.IsAsset)
                {
                    ExpendableInOutRecordInfo record = new ExpendableInOutRecordInfo();
                    record.CompanyID = model.CompanyID;
                    record.Amount = eq.Count;
                    record.CategoryID = eq.ExpendableTypeID;
                    record.InOutTime = eq.InTime;
                    record.Model = eq.Model;
                    record.Name = eq.Name;
                    record.Price = eq.ExpendablePrice;
                    record.Receiver = model.ApplicantID;
                    record.ReceiverName = model.ApplicantName;
                    record.Remark = model.Remark;
                    record.Type = ExpendableInOutRecordType.In;
                    record.Unit = eq.Unit;
                    record.WarehouseID = eq.WarehouseID;
                    record.WarehouseKeeper = model.OperatorID;
                    record.WarehouseKeeperName = model.OperatorName;
                    expendableInOutBll.InsertRecord(record, null);
                }
            }
        }
        /// <summary>
        /// 添加易耗品入库
        /// </summary>
        /// <param name="model">入库单主信息</param>
        /// <param name="item">易耗品入库信息</param>
        public long InsertInWarehouseExpendable(InWarehouseInfo model, InEquipmentsInfo item)
        {
            IInWarehouse dal = FM2E.DALFactory.EquipmentAccess.CreateInWarehouse();
            return dal.InsertInWarehouseExpendable(model, item);
        }
        #endregion

        #region 修改
        /// <summary>
        /// 更新易耗品入库信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="item"></param>
        public long SavaOutWarehouseApply(InWarehouseInfo model, InEquipmentsInfo item)
        {
            IInWarehouse dal = FM2E.DALFactory.EquipmentAccess.CreateInWarehouse();
            return dal.UpdateInWarehouseExpendable(model, item);
        }


        /// <summary>
        /// 更新入库单明细
        /// </summary>
        /// <param name="item">需要更新的入库单对象</param>
        public void UpdateInEquipments(InEquipmentsInfo item)
        {
            IInWarehouse dal = FM2E.DALFactory.EquipmentAccess.CreateInWarehouse();
            dal.UpdateInEquipments(item);
        }

        /// <summary>
        /// 更新入库单信息
        /// </summary>
        /// <param name="model">需要更新的入库单对象</param>
        public void UpdateInWarehouse(InWarehouseInfo model)
        {
            IInWarehouse dal = FM2E.DALFactory.EquipmentAccess.CreateInWarehouse();
            dal.UpdateInWarehouse(model);
        }
        #endregion

        /// <summary>
        /// 删除入库单
        /// </summary>
        /// <param name="ID"></param>
        public void DelInWarehouse(long ID)
        {
            IInWarehouse dal = FM2E.DALFactory.EquipmentAccess.CreateInWarehouse();
            dal.DelInWarehouse(ID);
        }
        /// <summary>
        /// 生成查询对象
        /// </summary>
        /// <param name="item">查询入库单信息</param>
        /// <returns>查询使用的查询对象</returns>
        public QueryParam GenerateSearchTerm(InWarehouseInfo item)
        {
            IInWarehouse dal = FM2E.DALFactory.EquipmentAccess.CreateInWarehouse();
            return dal.GenerateSearchTerm(item);
        }
        /// <summary>
        /// 根据查询条件查询入库单
        /// </summary>
        /// <param name="searchTerm">查询对象</param>
        /// <param name="recordCount">查询结果记录数</param>
        /// <returns>符合条件的入库单列表</returns>
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IInWarehouse dal = FM2E.DALFactory.EquipmentAccess.CreateInWarehouse();
            return dal.GetList(searchTerm, out recordCount);
        }
    }
}
