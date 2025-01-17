﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FM2E.Model.Maintain;
using FM2E.IDAL.Maintain;
using FM2E.Model.Utils;
using FM2E.IDAL.Utils;
using System.Data.Common;
using FM2E.Model.Exceptions;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using FM2E.IDAL.Equipment;
using FM2E.Model.Basic;
using FM2E.BLL.Basic;
using System.Data;

namespace FM2E.BLL.Maintain
{
    /// <summary>
    /// 故障处理业务逻辑类
    /// </summary>
    public class MalfunctionHandle
    {
        /// <summary>
        /// 获取所有的故障处理单
        /// </summary>
        /// <returns></returns>
        public IList GetAllMalfunctionSheets()
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            return dal.GetAllMalfunctionSheets();
        }
        /// <summary>
        /// 获取某张故障处理单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MalfunctionHandleInfo GetMalfunctionSheet(long id)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            return dal.GetMalfunctionSheet(id);
        }
        /// <summary>
        /// 获取所有故障设备
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public IList GetFaultyEquipments(MalfunctionSearchInfo term)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            return dal.GetFaultyEquipments(term);
        }
        /// <summary>
        /// 获取所有故障设备
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public IList GetMaintainedEquipments(MalfunctionSearchInfo term)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            return dal.GetMaintainedEquipments(term);
        }
        /// <summary>
        /// 根据查询条件获取故障处理单(不包括已撤消的故障单)，不支持分页
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public IList GetMalfunctionSheets(MalfunctionSearchInfo term)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            return dal.GetMalfunctionSheets(term);
        }
        /// <summary>
        /// 保存故障处理单，根据model.SheetID是否为0来决定是新增操作还是更新操作
        /// </summary>
        /// <param name="model"></param>
        public void SaveMalfunctionSheet(MalfunctionHandleInfo model, MalfunctionModifyRecordInfo modify)
        {
            if (model.SheetID == 0)
                AddMalfunctionSheet(model);
            else UpdateMalfunctionSheet(model, modify);
        }
        /// <summary>
        /// 添加故障处理单
        /// </summary>
        /// <param name="model"></param>
        public long AddMalfunctionSheet(MalfunctionHandleInfo model)
        {
            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;
            long id = 0;
            try
            {
                trans = transDAL.GetTransaction();
                IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
                //先添加故障处理单
                id = dal.AddMalfunctionSheet(model, trans);
                //再设置故障设备的状态为故障待修
                if (model.FaultyEquipments != null)
                {
                    ArrayList equiments = new ArrayList();
                    foreach (FaultyEquipmentInfo item in model.FaultyEquipments)
                    {
                        if (string.IsNullOrEmpty(item.EquipmentNO))
                            continue;
                        MaintainEquipmentsUpdateInfo oneEquipment = new MaintainEquipmentsUpdateInfo();
                        oneEquipment.EquipmentNO = item.EquipmentNO;
                        oneEquipment.MaintainTimesIncrease = 0;
                        oneEquipment.Status = (int)EquipmentStatus.Failure;
                        oneEquipment.UpdateTime = DateTime.Now;
                        equiments.Add(oneEquipment);
                    }
                    IEquipment equipmentDAL=FM2E.DALFactory.EquipmentAccess.CreateEquipment();
                    equipmentDAL.UpdateEquipmentMaintainInfo(equiments, trans);
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                if(trans!=null)
                    trans.Rollback();
                throw new BLLException("添加故障处理单失败", ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
            }
            return id;
        }
        /// <summary>
        /// 更新故障处理单
        /// </summary>
        /// <param name="model"></param>
        public void UpdateMalfunctionSheet(MalfunctionHandleInfo model,MalfunctionModifyRecordInfo modify)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            MalfunctionHandleInfo sheet = dal.GetMalfunctionSheet(model.SheetID);  //获取故障处理单旧的内容

            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;

            try
            {
                trans = transDAL.GetTransaction();
                dal.UpdateMalfunctionSheet(model, true, trans);
                if (modify != null)
                {
                    //添加修改记录
                    dal.AddModifyRecord(modify, trans);
                }
                //更新相关设备的状态,先把旧处理单中的故障设备置为正常，再把新处理单中的设备置为故障
                IEquipment equipmentDAL = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
                if (sheet.FaultyEquipments != null)
                {
                    ArrayList equiments = new ArrayList();
                    foreach (FaultyEquipmentInfo item in sheet.FaultyEquipments)
                    {
                        if (string.IsNullOrEmpty(item.EquipmentNO))
                            continue;
                        MaintainEquipmentsUpdateInfo oneEquipment = new MaintainEquipmentsUpdateInfo();
                        oneEquipment.EquipmentNO = item.EquipmentNO;
                        oneEquipment.MaintainTimesIncrease = 0;
                        oneEquipment.Status = (int)EquipmentStatus.Normal;
                        oneEquipment.UpdateTime = DateTime.Now;
                        equiments.Add(oneEquipment);
                    }
                    equipmentDAL.UpdateEquipmentMaintainInfo(equiments, trans);
                }
                if (model.FaultyEquipments != null)
                {
                    ArrayList equiments = new ArrayList();
                    foreach (FaultyEquipmentInfo item in model.FaultyEquipments)
                    {
                        if (string.IsNullOrEmpty(item.EquipmentNO))
                            continue;
                        MaintainEquipmentsUpdateInfo oneEquipment = new MaintainEquipmentsUpdateInfo();
                        oneEquipment.EquipmentNO = item.EquipmentNO;
                        oneEquipment.MaintainTimesIncrease = 0;
                        oneEquipment.Status = (int)EquipmentStatus.Failure;
                        oneEquipment.UpdateTime = DateTime.Now;
                        equiments.Add(oneEquipment);
                    }
                    equipmentDAL.UpdateEquipmentMaintainInfo(equiments, trans);
                }

                //撤单操作，需要把相关设备的状态置为正常状态
                if (model.Status == MalfunctionHandleStatus.Canceled)
                {
                    if (model.FaultyEquipments != null)
                    {
                        ArrayList equiments = new ArrayList();
                        foreach (FaultyEquipmentInfo item in model.FaultyEquipments)
                        {
                            if (string.IsNullOrEmpty(item.EquipmentNO))
                                continue;
                            MaintainEquipmentsUpdateInfo oneEquipment = new MaintainEquipmentsUpdateInfo();
                            oneEquipment.EquipmentNO = item.EquipmentNO;
                            oneEquipment.MaintainTimesIncrease = 0;
                            oneEquipment.Status = (int)EquipmentStatus.Normal;
                            oneEquipment.UpdateTime = DateTime.Now;
                            equiments.Add(oneEquipment);
                        }
                        equipmentDAL.UpdateEquipmentMaintainInfo(equiments, trans);
                    }
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("更新故障处理单失败", ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
            }
        }

        //public void UpdateWorkInstance(MalfunctionHandleInfo model)
        //{
        //    IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
        //    dal.UpdateWorkInstance(model.SheetID);
        //}

        /// <summary>
        /// 更新故障处理单
        /// </summary>
        /// <param name="model"></param>
        public void UpdateMalfunctionSheetBasicData(MalfunctionHandleInfo model)
        {
            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;

            try
            {
                trans = transDAL.GetTransaction();
                IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
                dal.UpdateMalfunctionSheet(model, false,trans);

                //撤单操作，需要把相关设备的状态置为正常状态
                if (model.Status == MalfunctionHandleStatus.Canceled)  
                {
                    IEquipment equipmentDAL = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
                    if (model.FaultyEquipments == null || model.FaultyEquipments.Count == 0)
                        model = dal.GetMalfunctionSheet(trans,model.SheetID);

                    if (model.FaultyEquipments != null)
                    {
                        ArrayList equiments = new ArrayList();
                        foreach (FaultyEquipmentInfo item in model.FaultyEquipments)
                        {
                            if (string.IsNullOrEmpty(item.EquipmentNO))
                                continue;
                            MaintainEquipmentsUpdateInfo oneEquipment = new MaintainEquipmentsUpdateInfo();
                            oneEquipment.EquipmentNO = item.EquipmentNO;
                            oneEquipment.MaintainTimesIncrease = 0;
                            oneEquipment.Status = (int)EquipmentStatus.Normal;
                            oneEquipment.UpdateTime = DateTime.Now;
                            equiments.Add(oneEquipment);
                        }
                        equipmentDAL.UpdateEquipmentMaintainInfo(equiments, trans);
                    }
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("更新故障处理单失败", ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
            }
        }

        /// <summary>
        /// 把DelegateUserName的值赋给NextUserName
        /// </summary>
        /// <param name="sheetid"></param>
        public void UpdateWorkflowInstanceNextUserName(long sheetid)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            dal.UpdateWorkflowInstanceNextUserName(sheetid);
        }

        /// <summary>
        /// 删除故障处理单
        /// </summary>
        /// <param name="id"></param>
        public void DelMalfunctionSheet(long id)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            MalfunctionHandleInfo sheet = dal.GetMalfunctionSheet(id);  //获取故障处理单内容

            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;

            try
            {
                trans = transDAL.GetTransaction();
                dal.DelMalfunctionSheet(id,trans);

                //如果故障处理单的处理状态为未结束且未撤单的，删除此单后，需要把相关设备的状态设为正常
                if (sheet.Status != MalfunctionHandleStatus.Finished&&sheet.Status!=MalfunctionHandleStatus.Canceled)
                {
                    if (sheet.FaultyEquipments != null)
                    {
                        ArrayList equiments = new ArrayList();
                        foreach (FaultyEquipmentInfo item in sheet.FaultyEquipments)
                        {
                            if (string.IsNullOrEmpty(item.EquipmentNO))
                                continue;

                            MaintainEquipmentsUpdateInfo oneEquipment = new MaintainEquipmentsUpdateInfo();
                            oneEquipment.EquipmentNO = item.EquipmentNO;
                            oneEquipment.MaintainTimesIncrease = 0;
                            oneEquipment.Status = (int)EquipmentStatus.Normal;
                            oneEquipment.UpdateTime = DateTime.Now;
                            equiments.Add(oneEquipment);
                        }
                        IEquipment equipmentDAL = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
                        equipmentDAL.UpdateEquipmentMaintainInfo(equiments, trans);
                    }
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("删除故障处理单失败", ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
            }
        }


        /// <summary>
        /// 根据ID删除故障登记纪录
        /// </summary>
        /// <param name="maintainID"></param>
        /// <param name="trans"></param>
        public void DelMaintainedEquipmentByMaintainID(long maintainID,long sheetID)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            MalfunctionHandleInfo sheet = dal.GetMalfunctionSheet(sheetID);  //获取故障处理单内容

            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;

            try
            {
                trans = transDAL.GetTransaction();
                dal.DelMaintainedEquipmentByMaintainID(maintainID, trans);

                if (sheet.Status != MalfunctionHandleStatus.Finished && sheet.Status != MalfunctionHandleStatus.Canceled)
                {
                    if (sheet.FaultyEquipments != null)
                    {
                        ArrayList equiments = new ArrayList();
                        foreach (FaultyEquipmentInfo item in sheet.FaultyEquipments)
                        {
                            if (string.IsNullOrEmpty(item.EquipmentNO))
                                continue;

                            MaintainEquipmentsUpdateInfo oneEquipment = new MaintainEquipmentsUpdateInfo();
                            oneEquipment.EquipmentNO = item.EquipmentNO;
                            oneEquipment.MaintainTimesIncrease = 0;
                            oneEquipment.Status = (int)EquipmentStatus.Normal;
                            oneEquipment.UpdateTime = DateTime.Now;
                            equiments.Add(oneEquipment);
                        }
                        IEquipment equipmentDAL = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
                        equipmentDAL.UpdateEquipmentMaintainInfo(equiments, trans);
                    }
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("删除故障处理单登记记录失败", ex);
            }
        }


        /// <summary>
        /// 添加维修信息并更新故障处理单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="item"></param>
        public void AddMaintainRecord(MalfunctionHandleInfo model, MalfuncitonMaintainInfo item)
        {
            ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
            DbTransaction trans = null;

            try
            {
                trans = transDAL.GetTransaction();
                IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
                dal.AddMaintainRecord(model, item,trans);

                //需要更新相关设备的状态为正常状态
                if (item.MaintainedEquipments != null)
                {
                    ArrayList equiments = new ArrayList();
                    foreach (MaintainedEquipmentInfo it in item.MaintainedEquipments)
                    {
                        if (string.IsNullOrEmpty(it.EquipmentNO))
                            continue;
                        MaintainEquipmentsUpdateInfo oneEquipment = new MaintainEquipmentsUpdateInfo();
                        oneEquipment.EquipmentNO = it.EquipmentNO;
                        if (it.MaintainResult == MaintainedEquipmentStatus.FunctionalityRestore)
                        {
                            oneEquipment.MaintainTimesIncrease = 0;
                            oneEquipment.Status = (int)EquipmentStatus.FunctionalityRestore;
                        }
                        else if (it.MaintainResult == MaintainedEquipmentStatus.Fixed || it.MaintainResult == MaintainedEquipmentStatus.UnFixed)
                        {
                            oneEquipment.MaintainTimesIncrease = 1;
                            oneEquipment.Status = (int)EquipmentStatus.Normal;
                        }
                        else if (it.MaintainResult == MaintainedEquipmentStatus.Unknown)
                        {
                            continue;
                        }
                        oneEquipment.UpdateTime = DateTime.Now;
                        equiments.Add(oneEquipment);
                    }
                    IEquipment equipmentDAL = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
                    equipmentDAL.UpdateEquipmentMaintainInfo(equiments, trans);
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new BLLException("添加维修信息失败", ex);
            }
            finally
            {
                transDAL.CloseTransaction(trans);
            }
        }
        /// <summary>
        /// 获取某张故障单的处理历史
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList GetMaintainHistory(long id)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            return dal.GetMaintainHistory(id);
        }

        /// <summary>
        /// 获取某张故障处理单的修改历史
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList GetModifyHistory(long id)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            return dal.GetModifyHistory(id);
        }
        /// <summary>
        /// 根据查询条件获取故障处理单列表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetMalfunctionList(MalfunctionSearchInfo term, int currentPageIndex, int pageSize, out int recordCount)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            QueryParam qp = dal.GenerateSearchTerm(term);
            qp.PageIndex = currentPageIndex;
            qp.PageSize = pageSize;
            //if (term.Status == 6)
            //{
            //    qp.Where = " where 1=1 and (1<0 or a.Status=6) ";
            //}
            //else if(term.Status == 10)
            //{
            //    qp.Where = " where 1=1 and (1<0 or a.Status=10) ";
            //}
            IList list = dal.GetMalfunctionSheets(qp, out recordCount);

            //查询故障单中的故障设备
            if (list != null && list.Count > 0)
            {
                ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
                DbTransaction trans = null;

                try
                {
                    trans = transDAL.GetTransaction();

                    foreach (MalfunctionHandleInfo item in list)
                    {
                        try
                        {
                            IList equipments = dal.GetFaultyEquipments(item.SheetID, trans);
                            item.FaultyEquipments = equipments;
                        }
                        catch
                        {
                        }
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
                finally
                {
                    transDAL.CloseTransaction(trans);
                }
            }

            return list;
        }

        //  [5/28/2013 Tvk]
        /// <summary>
        /// 根据查询条件获取故障处理单列表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetMalfunctionListByReportTime(MalfunctionSearchInfo term, int currentPageIndex, int pageSize, out int recordCount)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            QueryParam qp = dal.GenerateSearchTermByReportTime(term);
            qp.PageIndex = currentPageIndex;
            qp.PageSize = pageSize;
            IList list = dal.GetMalfunctionSheets(qp, out recordCount);

            //查询故障单中的故障设备
            if (list != null && list.Count > 0)
            {
                ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
                DbTransaction trans = null;

                try
                {
                    trans = transDAL.GetTransaction();

                    foreach (MalfunctionHandleInfo item in list)
                    {
                        try
                        {
                            IList equipments = dal.GetFaultyEquipments(item.SheetID, trans);
                            item.FaultyEquipments = equipments;
                        }
                        catch
                        {
                        }
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
                finally
                {
                    transDAL.CloseTransaction(trans);
                }
            }

            return list;
        }
        //  [5/28/2013 Tvk]
        //  [5/21/2013 Tvk]
        /// <summary>
        /// 根据查询条件获取延迟审批故障处理单列表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetDelayApproveMalfunctionList(MalfunctionSearchInfo term, int currentPageIndex, int pageSize, out int recordCount)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            QueryParam qp = dal.GenerateDelayApproveSearchTerm(term);
            qp.PageIndex = currentPageIndex;
            qp.PageSize = pageSize;
            IList list = dal.GetMalfunctionSheets(qp, out recordCount);

            //查询故障单中的故障设备
            if (list != null && list.Count > 0)
            {
                ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
                DbTransaction trans = null;

                try
                {
                    trans = transDAL.GetTransaction();

                    foreach (MalfunctionHandleInfo item in list)
                    {
                        try
                        {
                            IList equipments = dal.GetFaultyEquipments(item.SheetID, trans);
                            item.FaultyEquipments = equipments;
                        }
                        catch
                        {
                        }
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
                finally
                {
                    transDAL.CloseTransaction(trans);
                }
            }

            return list;
        }

        //  [5/21/2013 Tvk]
        //********** Modified by Xue    For ShenGaoSu    2011-11-28 ********************************************************************************
        /// <summary>
        /// 根据查询条件获取当前审批人的待审批故障处理单列表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="userName">审批人用户名</param>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetMalfunctionApprovalList(MalfunctionSearchInfo term,string userName, int currentPageIndex, int pageSize, out int recordCount)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            QueryParam qp = dal.GenerateApprovalSearchTerm(term, userName);
            qp.PageIndex = currentPageIndex;
            qp.PageSize = pageSize;
            IList list = dal.GetMalfunctionSheets(qp, out recordCount);

            //查询故障单中的故障设备
            if (list != null && list.Count > 0)
            {
                ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
                DbTransaction trans = null;

                try
                {
                    trans = transDAL.GetTransaction();

                    foreach (MalfunctionHandleInfo item in list)
                    {
                        try
                        {
                            IList equipments = dal.GetFaultyEquipments(item.SheetID, trans);
                            item.FaultyEquipments = equipments;
                        }
                        catch
                        {
                        }
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
                finally
                {
                    transDAL.CloseTransaction(trans);
                }
            }

            return list;
        }
        //********** Modification Finished 2011-11-28 **********************************************************************************************

        public IList GetMalfunctionList2(MalfunctionSearchInfo term, int currentPageIndex, int pageSize, out int recordCount)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            QueryParam qp = dal.GenerateSearchTerm2(term);
            qp.PageIndex = currentPageIndex;
            qp.PageSize = pageSize;
            IList list = dal.GetMalfunctionSheets(qp, out recordCount);

            //查询故障单中的故障设备
            if (list != null && list.Count > 0)
            {
                ITransaction transDAL = FM2E.DALFactory.UtilsAccess.CreateTransaction();
                DbTransaction trans = null;

                try
                {
                    trans = transDAL.GetTransaction();

                    foreach (MalfunctionHandleInfo item in list)
                    {
                        try
                        {
                            IList equipments = dal.GetFaultyEquipments(item.SheetID, trans);
                            item.FaultyEquipments = equipments;
                        }
                        catch
                        {
                        }
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
                finally
                {
                    transDAL.CloseTransaction(trans);
                }
            }

            return list;
        }
        /// <summary>
        /// 根据查询条件获取设备维修记录
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetEquipmentMaintainRecords(EquipmentMaintainRecordSearchInfo term, int currentPageIndex, int pageSize, out int recordCount)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            QueryParam qp = dal.GenerateSearchTerm(term);
            qp.PageIndex = currentPageIndex;
            qp.PageSize = pageSize;
            return dal.GetEquipmentMaintainRecords(qp, out recordCount);
        }
        /// <summary>
        /// 根据查询条件获取设备流转记录
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetTransferRecord(EquipmentMaintainRecordSearchInfo term, int currentPageIndex, int pageSize, out int recordCount)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            QueryParam qp = dal.GenerateSearchTerm1(term);
            qp.PageIndex = currentPageIndex;
            qp.PageSize = pageSize;
            return dal.GetTransferRecord(qp, out recordCount);
        }
        /// <summary>
        /// 根据统计条件进行统计
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public IList GetMalfunctionStatisticData(MalfunctionStatisticTerm term)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            IList list = dal.GetMalfunctionStatisticData(term);
            int totalCount = 0;
            foreach (MalfunctionStatisticInfo item in list)
            {
                totalCount += item.Count;
            }
            foreach (MalfunctionStatisticInfo item in list)
            {
                item.Percent = (float)item.Count / totalCount;
            }
            return list;
        }

        /// <summary>
        /// 根据条件获取故障设备总数列表
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public IList GetMaintainedEquipmentCount(MalfunctionSearchInfo term)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            return dal.GetMaintainedEquipmentCount(term);
        }

        /// <summary>
        /// 根据条件获取未修复故障设备总数列表
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public IList GetRepairedEquipmentCount(MalfunctionSearchInfo term)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            return dal.GetRepairedEquipmentCount(term);
        }

        /// <summary>
        /// 根据条件获取未修复故障设备总数列表
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public IList GetWait4RepairedEquipmentCount(MalfunctionSearchInfo term)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            return dal.GetWait4RepairedEquipmentCount(term);
        }

        /// <summary>
        /// 根据条件获取设备总数列表
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public IList GetAllEquipmentCount(MalfunctionSearchInfo term)
        {
            IMalfunctionHandle dal = FM2E.DALFactory.MaintainAccess.CreateMalfunctionHandle();
            return dal.GetAllEquipmentCount(term);
        }
    }
}
