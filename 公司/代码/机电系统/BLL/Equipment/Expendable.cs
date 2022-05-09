using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using FM2E.IDAL.Equipment;
using FM2E.Model.Utils;
using System.Collections;
using FM2E.Model.Exceptions;
using FM2E.IDAL.Utils;
using System.Data.Common;

namespace FM2E.BLL.Equipment
{
    public class Expendable
    {
        /// <summary>
        /// 获取易耗品信息
        /// </summary>
        /// <param name="ExpendableID"></param>
        /// <returns></returns>
        public ExpendableInfo GetExpendable(long ExpendableID)
        {
            IExpendable dal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
            return dal.GetExpendable(ExpendableID);
        }
        public bool InsertExpendable(ExpendableInfo model)
        {
            IExpendable dal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
            return dal.InsertExpendable(model);
        }
        public void UpdateExpendable(ExpendableInfo model)
        {
            IExpendable dal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
            dal.UpdateExpendable(model);
        }
        public void DelExpendable(long ExpendableID)
        {
            IExpendable dal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
            dal.DelExpendable(ExpendableID);
        }
        public QueryParam GenerateSearchTerm(ExpendableInfo item)
        {
            IExpendable dal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IExpendable dal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
            return dal.GetList(searchTerm, out recordCount);
        }
        public IList GetAllExpendableName()
        {
            IExpendable dal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
            return dal.GetAllExpendableName();
        }
        public IList GetAllExpendableModelbyName(string name)
        {
            IExpendable dal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
            return dal.GetAllExpendableModelbyName(name);
        }
        public IList GetAllExpendableName(string warehouseID)
        {
            IExpendable dal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
            return dal.GetAllExpendableName(warehouseID);
        }
        public IList GetAllExpendableModelbyName(string warehouseID, string name)
        {
            IExpendable dal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
            return dal.GetAllExpendableModelbyName(warehouseID, name);
        }
        public decimal GetCountOfExpendable(string WarehouseID, long ExpendableID)
        {
            IExpendable dal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
            return dal.GetCountOfExpendable(WarehouseID,ExpendableID);
        }

        /// <summary>
        /// 查询设备库存数量,By zjf
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="productName">产品名称</param>
        /// <param name="productModel">产品型号</param>
        /// <returns>库存信息</returns>
        public IList QueryStorage(int pageIndex, int pageSize, out int recordCount, string companyid, string productName, string productModel)
        {
            IExpendable dal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
            return dal.QueryStorage(pageIndex, pageSize, out recordCount, companyid, productName, productModel);
        }
        /// <summary>
        /// 查询设备库存数量,By zjf
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="productName">产品名称</param>
        /// <param name="productModel">产品型号</param>
        /// <param name="warehouseid">仓库ID</param>
        /// <returns>库存信息</returns>
        public IList QueryStorage(int pageIndex, int pageSize, out int recordCount, string companyid, string productName, string productModel, string warehouseid)
        {
            IExpendable dal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
            return dal.QueryStorage(pageIndex, pageSize, out recordCount, companyid, productName, productModel,warehouseid);
        }
        /// <summary>
        /// 采购回来，消耗品入库,by zjf 2009-1-20
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="warehouseid">仓库ID</param>
        /// <param name="productname">产品名称</param>
        /// <param name="model">产品型号</param>
        /// <param name="unit">单位</param>
        /// <param name="count">入库数量</param>
        /// <returns>返回库存量</returns>
        public decimal ExpendableInWarehouseFromPurchase(string companyid, string warehouseid, string productname, string model, string unit, decimal count,decimal price,long typeid)
        {
            IExpendable dal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
            return dal.AddExpendable(companyid, warehouseid, productname, model, unit, count, price,typeid);
        }

        /// <summary>
        /// 获取产品的名称匹配
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IList<string> GetProductNames(string name,int count)
        {
             IExpendable dal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
             return dal.GetProductNames(name,count); 
        }

        /// <summary>
        /// 易耗品出库
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public decimal ExpendableOutWarehouse(string companyid,ExpendableInOutRecordInfo record)
        {
            IExpendable exdal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
            IExpendableInOut recorddal = FM2E.DALFactory.EquipmentAccess.CreateExpendableInOut();

            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;
            decimal count = 0;
            try
            {
                trans = transDAL.GetTransaction();
                //先出库
                if (record.Model == null)
                    record.Model = "";
                count = exdal.AddExpendable(trans, companyid, record.WarehouseID, record.Name, record.Model, record.Unit, record.Price, record.Amount * -1,record.CategoryID);
                //添加出库记录
                //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
                if (count != -1)
                {
                    recorddal.Add(record, trans);
                    trans.Commit();
                }
                else
                {
                    throw new BLLException();
                }
                //********** Modification Finished 2011-09-09 **********************************************************************************************
                
                
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("出库失败"+ex.Message, ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
                trans.Dispose();
                trans = null;
            }
            return count;
        }

        #region 易耗品出库流程
       
        /// <summary>
        /// 添加易耗品出库申请;
        /// </summary>
        public long ExpendableOutRequest(OutWarehouseInfo item, InEquipmentsInfo record)
        {
            IExpendableInOut dal = FM2E.DALFactory.EquipmentAccess.CreateExpendableInOut();
            return dal.InsertOutWarehouseExpendable(item, record);
        }

        /// <summary>
        /// 查询易耗品出库单列表;
        /// </summary>
        public IList SearchOutWarehouseApply(OutWarehouseInfo info, int pageindex, int pagesize, out int recordCount)
        {
            IExpendableInOut recorddal = FM2E.DALFactory.EquipmentAccess.CreateExpendableInOut();
            QueryParam qp = recorddal.GenerateSearchTerm(info, pageindex, pagesize);
            return recorddal.SearchOutWarehouseExpendable(qp, out recordCount);
        }

        /// <summary>
        /// 获取出库单信息，含明细
        /// </summary>
        /// <param name="ID">入库单流水号</param>
        /// <returns>入库单信息（含明细）</returns>
        public OutWarehouseInfo GetOutWarehouse(long ID)
        {
            IExpendableInOut dal = FM2E.DALFactory.EquipmentAccess.CreateExpendableInOut();
            return dal.GetOutWarehouse(ID);
        }

        /// <summary>
        /// 查询易耗品出库单列表;（审批专用）
        /// </summary>
        public IList SearchOutWarehouseApply(OutWarehouseInfo info, int pageindex, int pagesize, string userName, out int recordCount)
        {
            IExpendableInOut recorddal = FM2E.DALFactory.EquipmentAccess.CreateExpendableInOut();
            QueryParam qp = recorddal.GetGenerateSearchTerm(info, pageindex, pagesize, userName);
            return recorddal.SearchOutWarehouseExpendable(qp, out recordCount);
        }

        /// <summary>
        /// 更新出库申请单
        /// </summary>
        /// <param name="model">需要更新的入库单对象</param>
        public void UpdateOutWarehouse(OutWarehouseInfo model)
        {
            IExpendableInOut recorddal = FM2E.DALFactory.EquipmentAccess.CreateExpendableInOut();
            recorddal.UpdateOutWarehouse(model);
        }

        #endregion

        /// <summary>
        /// 易耗品出库前记录
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public decimal ExpendableOutWarehouseRecord(string companyid, ExpendableInOutRecordInfo record)
        {
            IExpendableInOut recorddal = FM2E.DALFactory.EquipmentAccess.CreateExpendableInOut();

            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;
            decimal count = 0;
            try
            {
                trans = transDAL.GetTransaction();
                //添加出库记录
                recorddal.AddRecord(record, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("出库失败" + ex.Message, ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
                trans.Dispose();
                trans = null;              
            }
            return count;
        }
         
        /// <summary>
        /// 易耗品入库
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public decimal ExpendableInWarehouse(string companyid, ExpendableInOutRecordInfo record)
        {
            IExpendable exdal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
            IExpendableInOut recorddal = FM2E.DALFactory.EquipmentAccess.CreateExpendableInOut();

            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;
            decimal count = 0;
            try
            {
                trans = transDAL.GetTransaction();
                //先入库
                if (record.Model == null)
                    record.Model = "";
                count = exdal.AddExpendable(trans, companyid, record.WarehouseID, record.Name, record.Model, record.Unit, record.Price, record.Amount * 1,record.CategoryID);
                //添加入库记录
                //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
                if (count != -1)
                {
                    recorddal.Add(record, trans);
                    trans.Commit();
                }
                else
                {
                    throw new BLLException();
                }
                //********** Modification Finished 2011-09-09 **********************************************************************************************
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("入库失败" + ex.Message, ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
                trans.Dispose();
                trans = null;
            }
            return count;
        }
        /// <summary>
        /// 易耗品入库前记录
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public decimal ExpendableInWarehouseRecord(string companyid, ExpendableInOutRecordInfo record)
        {
            IExpendableInOut recorddal = FM2E.DALFactory.EquipmentAccess.CreateExpendableInOut();

            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;
            decimal count = 0;
            try
            {
                trans = transDAL.GetTransaction();
                //添加入库前记录
                recorddal.AddRecord(record, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("入库失败" + ex.Message, ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
                trans.Dispose();
                trans = null;
            }
            return count;
        }


        /// <summary>
        /// 获取产品的型号匹配
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IList<string> GetProductModels(string model, int count)
        {
            IExpendable dal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
            return dal.GetProductModels(model,count); 
        }

        public ExpendableInfo GetTopItem(string companyid,string WarehouseID, string Name, string Model, string unit)
        {
            IExpendable dal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
            return dal.GetTopExpendableItem(companyid,WarehouseID, Name, Model, unit);
        }

        public ExpendableStatisticsInfo GetExpendableStaticticsData(string companyid, string warehouseid, string categoryid, DateTime datefrom, DateTime dateto)
        {
            IExpendable dal = FM2E.DALFactory.EquipmentAccess.CreateExpendable();
            return dal.GetExpendableStaticticsData(companyid,warehouseid,categoryid,datefrom,dateto);
        }


        public IList GetallInOutRecord(ExpendableInOutRecordType type)
        {
            IExpendableInOut recorddal = FM2E.DALFactory.EquipmentAccess.CreateExpendableInOut();
            return recorddal.GetallInOutRecord(type);
        }

        public InOutApproval GetCurrentApprovalStatus()
        {
            IExpendableInOut recorddal = FM2E.DALFactory.EquipmentAccess.CreateExpendableInOut();
            return recorddal.GetCurrentApprovalStatus();
        }

        public Boolean updateCurrentApprovalStatus(InOutApproval item)
        {
            IExpendableInOut recorddal = FM2E.DALFactory.EquipmentAccess.CreateExpendableInOut();
            return recorddal.updateCurrentApprovalStatus(item);
        }

        public Boolean deleteAllRecord()
        {
            IExpendableInOut recorddal = FM2E.DALFactory.EquipmentAccess.CreateExpendableInOut();
            return recorddal.deleteAllRecord();
        }
    }
}
