using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FM2E.Model.Maintain;
using FM2E.IDAL.Maintain;
using FM2E.Model.Utils;
using System.Collections;
using FM2E.Model.Exceptions;
using FM2E.IDAL.Utils;
using System.Data.Common;

namespace FM2E.BLL.Maintain
{
    /// <summary>
    /// 维护逻辑处理
    /// </summary>
    public class Maintain
    {
        private IMaintain dal = FM2E.DALFactory.MaintainAccess.CreateMaintain();
        #region 维护项
        /// <summary>
        /// 添加维护项
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public long SaveMaintainItem(MaintainItemInfo p)
        {
            if (p.ID == 0)
            {
                //添加
                return dal.AddMaintainItem(p);
            }
            else
            {
                //修改
                return dal.UpdateMaintainItem(p);
            }
        }

        /// <summary>
        /// 删除维护项
        /// </summary>
        /// <param name="id"></param>
        public void DeleteMaintainItem(long id)
        {
            dal.DeleteMaintainItem(id);
        }

        /// <summary>
        /// 获取一个维护项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MaintainItemInfo GetMaintainItem(long id)
        {
            return dal.GetMaintainItem(id);
        }

        /// <summary>
        /// 搜索维护项，分页
        /// </summary>
        /// <param name="info"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList SearchMaintainItem(MaintainItemSearchInfo info, int pageIndex, int pageSize, out int recordCount)
        {
            QueryParam qp = dal.GetSearchTerm(info);
            qp.PageIndex = pageIndex;
            qp.PageSize = pageSize;

            return dal.SearchMaintainItem(qp, out recordCount);
        }
        /// <summary>
        /// 搜索维护项，不分页
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public IList SearchMaintainItem(MaintainItemSearchInfo info)
        {
            return dal.SearchMaintainItem(info);
        }
        #endregion


        #region 维护模板表
        /// <summary>
        /// 保存一个维护模板表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long SaveTemplateMaintainSheet(TemplateMaintainSheetInfo model)
        {
            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;
            long id = 0;
            try
            {
                trans = transDAL.GetTransaction();
                if (model.TemplateSheetID == 0)
                {
                    //添加
                    id = dal.AddTemplateMaintainSheet(model, trans);
                }
                else
                {
                    //修改
                    id = dal.UpdateTemplateMaintainSheet(model, trans);
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("保存维护模板表失败"+ex.Message, ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
            }
            return id;
        }
        /// <summary>
        /// 删除一个维护模板表
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTemplateMaintainSheet(long id)
        {
            dal.DeleteTemplateMaintainSheet(id);
        }
        /// <summary>
        /// 获取一个维护模板表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TemplateMaintainSheetInfo GetTemplateMaintainSheet(long id)
        {
            return dal.GetTemplateMaintainSheet(id);
        }
        /// <summary>
        /// 核实
        /// </summary>
        /// <param name="id"></param>
        /// <param name="result"></param>
        /// <param name="confirmer"></param>
        /// <param name="confirmTime"></param>
        /// <param name="remark"></param>
        public void DoConfirm(long id, MaintainConfirmResult result, string confirmer, DateTime confirmTime, string remark)
        {
            dal.DoConfirm(id, result, confirmer, confirmTime, remark);
        }
        /// <summary>
        /// 查询维护模板表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList SearchTemplateMaintainSheet(TemplateMaintainSheetSearchInfo term, int pageIndex, int pageSize, out int recordCount)
        {
            QueryParam qp = dal.GetSearchTerm(term);
            qp.PageIndex = pageIndex;
            qp.PageSize = pageSize;

            return dal.SearchTemplateMaintainSheet(qp, out recordCount);
        }
        #endregion


        #region 维护表
        /// <summary>
        /// 保存一个维护表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long SaveMaintainSheet(MaintainSheetInfo model)
        {
            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;
            long id = 0;
            try
            {
                trans = transDAL.GetTransaction();
                if (model.SheetID == 0)
                {
                    //添加
                    id = dal.AddMaintainSheet(model, trans);
                }
                else
                {
                    //修改
                    id = dal.UpdateMaintainSheet(model, trans);
                }
                //修改相关设备的状态，后补




                /////
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("保存维护记录失败"+ex.Message, ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
            }
            return id;
        }
        /// <summary>
        /// 删除一个维护表
        /// </summary>
        /// <param name="id"></param>
        public void DeleteMaintainSheet(long id)
        {
            dal.DeleteMaintainSheet(id);
        }
        /// <summary>
        /// 获取一个维护表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MaintainSheetInfo GetMaintainSheet(long id)
        {
            return dal.GetMaintainSheet(id);
        }
        /// <summary>
        /// 获取一个维护表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MaintainSheetInfo GetMaintainSheetByEquipmentName(long id)
        {
            return dal.GetMaintainSheetByEquipmentName(id);
        }
        /// <summary>
        /// 查询维护表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList SearchMaintainSheet(MaintainSheetSearchInfo term, int pageIndex, int pageSize, out int recordCount)
        {
            QueryParam qp = dal.GetSearchTerm(term);
            qp.PageIndex = pageIndex;
            qp.PageSize = pageSize;

            return dal.SearchMaintainSheet(qp, out recordCount);
        }
        #endregion

        #region 设备维护记录
        /// <summary>
        /// 获取设备维护记录
        /// </summary>
        /// <param name="equipmentNO">设备条形码</param>
        /// <param name="type">维护类型</param>
        /// <returns></returns>
        public IList GetDeviceMaintainRecord(string equipmentNO, MaintainType type)
        {
            return dal.SearchDeviceMaintainRecord(equipmentNO, type);
        }
        /// <summary>
        /// 获取设备维护记录，分页
        /// </summary>
        /// <param name="equipmentNO">设备条形码</param>
        /// <param name="type">维护类型</param>
        /// <returns></returns>
        public IList GetDeviceMaintainRecord(MaintainSheetEquipmentSearchInfo info,  int pageIndex, int pageSize,out int recordCount)
        {
            QueryParam qp = dal.GetSearchTerm(info);
            qp.PageIndex = pageIndex;
            qp.PageSize = pageSize;
            return dal.SearchDeviceMaintainRecord(qp, out recordCount);
        }
        #endregion
    }
}
