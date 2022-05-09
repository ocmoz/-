using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using FM2E.DALFactory;
using FM2E.IDAL.Equipment;
using System.Data.Common;
using FM2E.IDAL.Utils;
using FM2E.Model.Exceptions;
namespace FM2E.BLL.Equipment
{
    /// <summary>
    /// 业务逻辑类ConsumableEquipment 的摘要说明。
    /// </summary>
    public class ConsumableEquipment
    {
        private readonly IConsumableEquipment dal = EquipmentAccess.CreateConsumableEquipment();
        public ConsumableEquipment()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long InsertConsumableEquipment(ConsumableEquipmentInfo model)
        {
            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;
            long id = 0;
            try
            {
                trans = transDAL.GetTransaction();
                //增加一条数据
                id = dal.InsertConsumableEquipmentTrans(model, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("添加失败", ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
            }
            return id;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateConsumableEquipment(ConsumableEquipmentInfo model)
        {
            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;
            try
            {
                trans = transDAL.GetTransaction();
                //更新一条数据
                dal.UpdateConsumableEquipmentTrans(model, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("更新失败", ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteConsumableEquipment(long ConsumableEquipmentID)
        {

            dal.DeleteConsumableEquipment(ConsumableEquipmentID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ConsumableEquipmentInfo GetConsumableEquipment(long ConsumableEquipmentID)
        {

            return dal.GetConsumableEquipment(ConsumableEquipmentID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ConsumableEquipmentInfo GetConsumableEquipmentByNO(string ConsumableEquipmentNO)
        {

            return dal.GetConsumableEquipmentByNO(ConsumableEquipmentNO);
        }

        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        public IList GetAllConsumableEquipment()
        {
            return dal.GetAllConsumableEquipment();
        }

        /// <summary>
        /// 获取查询实体
        /// </summary>
        public QueryParam GenerateSearchTerm(ConsumableEquipmentInfo model)
        {
            return dal.GenerateSearchTerm(model);
        }

        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        public IList GetList(QueryParam term, out int recordCount)
        {
            return dal.GetList(term, out recordCount);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ConsumableEquipmentID)
        {
            return dal.Exists(ConsumableEquipmentID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public long InsertConsumableEquipment(ConsumableEquipmentInfo model, DbTransaction trans)
        {
            return dal.InsertConsumableEquipmentTrans(model, trans);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public void UpdateConsumableEquipment(ConsumableEquipmentInfo model, DbTransaction trans)
        {
            dal.UpdateConsumableEquipmentTrans(model, trans);
        }
        /// <summary>
        /// 查询易耗品列表
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IList<ConsumableEquipmentInfo> Search(ConsumableEquipmentInfo item)
        {
            return dal.Search(item);
        }

        /// <summary>
        /// 当前的设备总数
        /// </summary>
        /// <param name="term"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        public int GetCurrentDeviceCount(QueryParam term, string companyid)
        {
            return dal.GetCurrentDeviceCount(term, companyid);
        }

        #endregion  成员方法
        #region 修改 By Tianmu
        /// <summary>
        /// 易耗品入库
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public decimal ExpendableInWarehouse(string companyid, ExpendableInOutRecordInfo record)
        {
            IConsumableEquipment dal = EquipmentAccess.CreateConsumableEquipment();
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
                count = dal.AddExpendasExpendable(trans, companyid, record.WarehouseID, record.Name, record.Model, record.Unit, record.Price, record.Amount * 1);
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
        /// 易耗品出库
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public decimal ExpendableOutWarehouse(string companyid, ExpendableInOutRecordInfo record)
        {
            IConsumableEquipment dal = EquipmentAccess.CreateConsumableEquipment();
            IExpendableInOut recorddal = FM2E.DALFactory.EquipmentAccess.CreateExpendableInOut();

            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;
            decimal count = 0;
            try
            {
                trans = transDAL.GetTransaction();
                //先出库
                count = dal.AddExpendasExpendable(trans, companyid, record.WarehouseID, record.Name, record.Model, record.Unit, record.Price, record.Amount * -1);
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
        /// 删除一条数据
        /// </summary>
        public void DeleteExpendasExpendable(long ConsumableEquipmentID)
        {

            dal.DeleteExpendasExpendable(ConsumableEquipmentID);
        }
        public ConsumableEquipmentDetailInfo GetEquipmentDetailByWarehouseID(string warehouseid)
        {
            IConsumableEquipment dal = EquipmentAccess.CreateConsumableEquipment();
            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;
            ConsumableEquipmentDetailInfo model = new ConsumableEquipmentDetailInfo();
            try
            {
                trans = transDAL.GetTransaction();
                model = dal.GetEquipmentDetailByWarehouseID(warehouseid, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("更新失败", ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
            }
            return model;
        }
        public void UpdateExpendasExpendable(WareHouseConsumableEquipmentInfo model)
        {
            IConsumableEquipment dal = EquipmentAccess.CreateConsumableEquipment();
            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;
            try
            {
                trans = transDAL.GetTransaction();
                //更新一条数据
                dal.UpdateExpendasExpendable(model, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("更新失败", ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
            }
        }
        // <summary>
        public WareHouseConsumableEquipmentInfo GetExpendasExpendable(long ConsumableEquipmentID)
        {
            IConsumableEquipment dal = EquipmentAccess.CreateConsumableEquipment();
            return dal.GetExpendasExpendable(ConsumableEquipmentID);
        }
        /// 增加一条数据
        /// </summary>
        public long InsertExpendasExpendable(WareHouseConsumableEquipmentInfo model)
        {
            IConsumableEquipment dal = EquipmentAccess.CreateConsumableEquipment();
            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;
            long id = 0;
            try
            {
                trans = transDAL.GetTransaction();
                //增加一条数据
                id = dal.InsertExpendasExpendable(model, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("添加失败", ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
            }
            return id;
        }
        /// <summary>
        /// 获取查询实体
        /// </summary>
        public QueryParam GenerateExpendasSearchTerm(WareHouseConsumableEquipmentInfo model)
        {
            IConsumableEquipment dal = EquipmentAccess.CreateConsumableEquipment();
            return dal.GenerateExpendasSearchTerm(model);
        }

        public IList GetExpendasListByWarehouseID(QueryParam searchTerm, out int recordCount, string wareHouseID)
        {
            IConsumableEquipment dal = EquipmentAccess.CreateConsumableEquipment();
            return dal.GetExpendasListByWarehouseID(searchTerm, out recordCount, wareHouseID);
        }
        public IList GetExpendasList(QueryParam searchTerm, out int recordCount)
        {
            IConsumableEquipment dal = EquipmentAccess.CreateConsumableEquipment();
            return dal.GetExpendasList(searchTerm, out recordCount);
        }
        /// <summary>
        /// 当前的设备易耗品总数
        /// </summary>
        /// <param name="term"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        public int GetCurrentExpendasDeviceCount(QueryParam term, string companyid)
        {
            IConsumableEquipment dal = EquipmentAccess.CreateConsumableEquipment();
            return dal.GetCurrentExpendasDeviceCount(term, companyid);
        }

        /// <summary>
        /// 获取导出仓库设备易耗品信息列表
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public IList GetExportList(QueryParam searchTerm)
        {
            IConsumableEquipment dal = EquipmentAccess.CreateConsumableEquipment();
            return dal.GetExportList(searchTerm);
        }
        #endregion
    }
}

