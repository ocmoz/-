using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using FM2E.IDAL.Equipment;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;

namespace FM2E.BLL.Equipment
{
    /// <summary>
    /// 设备借调管理业务逻辑类
    /// </summary>
    public class Secondment
    {
        #region 借调申请与审批
        /// <summary>
        /// 获取借调申请列表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetBorrowApplyList(QueryParam term, out int recordCount)
        {
            IBorrowApply dal = (IBorrowApply)FM2E.DALFactory.EquipmentAccess.CreateBorrowApply();
            return dal.GetBorrowApplyList(term,out recordCount);
        }

        /// <summary>
        /// 获取借调申请列表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList SearchApplyList(BorrowApplySearchInfo info,int pagesize,int pageindex, out int recordCount)
        {
            IBorrowApply dal = (IBorrowApply)FM2E.DALFactory.EquipmentAccess.CreateBorrowApply();

            QueryParam qp = dal.GenerateSearchTerm(info);
            qp.PageIndex = pageindex;
            qp.PageSize = pagesize;

            return dal.GetBorrowApplyList(qp, out recordCount);
        }


        public IList SearchApprovalHistory(BorrowApprovalSearchInfo info, int pagesize, int pageindex, out int recordCount)
        {
            IBorrowApply dal = (IBorrowApply)FM2E.DALFactory.EquipmentAccess.CreateBorrowApply();
            QueryParam qp = dal.GenerateSearchTerm(info);
            qp.PageIndex = pageindex;
            qp.PageSize = pagesize;

            return dal.GetBorrowApprovalHistory(qp, out recordCount);
        }

        /// <summary>
        /// 获取审批历史记录
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetBorrowApprovalHistory(QueryParam term, out int recordCount)
        {
            IBorrowApply dal = (IBorrowApply)FM2E.DALFactory.EquipmentAccess.CreateBorrowApply();
            return dal.GetBorrowApprovalHistory(term, out recordCount);
        }
        /// <summary>
        /// 获取特定借调申请的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BorrowApplyInfo GetBorrowApply(long id)
        {
            IBorrowApply dal = (IBorrowApply)FM2E.DALFactory.EquipmentAccess.CreateBorrowApply();
            return dal.GetBorrowApply(id);
        }
        /// <summary>
        /// 添加借调申请
        /// </summary>
        /// <param name="model"></param>
        public long AddBorrowApply(BorrowApplyInfo model)
        {
            IBorrowApply dal = (IBorrowApply)FM2E.DALFactory.EquipmentAccess.CreateBorrowApply();
            return dal.AddBorrowApply(model);
        }
        /// <summary>
        /// 更新借调申请
        /// </summary>
        /// <param name="model"></param>
        public void UpdateBorrowApply(BorrowApplyInfo model)
        {
            IBorrowApply dal = (IBorrowApply)FM2E.DALFactory.EquipmentAccess.CreateBorrowApply();
            dal.UpdateBorrowApply(model);
        }
        /// <summary>
        /// 删除相应的借调申请
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBorrowApply(long id)
        {
            IBorrowApply dal = (IBorrowApply)FM2E.DALFactory.EquipmentAccess.CreateBorrowApply();
            dal.DeleteBorrowApply(id);
        }
        /// <summary>
        /// 审批借调申请
        /// </summary>
        /// <param name="model"></param>
        public void ApprovalBorrowApply(BorrowApprovalInfo model)
        {
            IBorrowApply dal = (IBorrowApply)FM2E.DALFactory.EquipmentAccess.CreateBorrowApply();
            dal.ApprovalBorrowApply(model);
        }
        /// <summary>
        /// 改变申请单状态值
        /// </summary>
        /// <param name="borrowApplyID">申请单号</param>
        /// <param name="status">状态值</param>
        public void ChangeStatus(long borrowApplyID, BorrowApplyStatus status)
        {
            IBorrowApply dal = (IBorrowApply)FM2E.DALFactory.EquipmentAccess.CreateBorrowApply();
            dal.ChangeStatus(borrowApplyID, status);
        }
        #endregion

        #region 借出登记
        /// <summary>
        /// 获取某个借调申请的借出设备登记明细
        /// </summary>
        /// <param name="borrowApplyID"></param>
        /// <returns></returns>
        public IList GetBorrowRecordList(long borrowApplyID)
        {
            IBorrowRecord dal = (IBorrowRecord)FM2E.DALFactory.EquipmentAccess.CreateBorrowRecord();
            return dal.GetBorrowRecordList(borrowApplyID);
        }

        /// <summary>
        /// 搜索借出设备
        /// </summary>
        /// <param name="info"></param>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList SearchBorrowRecord(BorrowRecordSearchInfo info, int pagesize, int pageindex, out int recordCount)
        {
            IBorrowRecord dal = (IBorrowRecord)FM2E.DALFactory.EquipmentAccess.CreateBorrowRecord();
            QueryParam qp = dal.GenerateSearchTerm(info);
            qp.PageSize = pagesize;
            qp.PageIndex = pageindex;
            return dal.GetBorrowRecordList(qp, out recordCount);
        }

        /// <summary>
        /// 获取借出设备登记明细(支持分页)
        /// </summary>
        /// <param name="borrowApplyID"></param>
        /// <returns></returns>
        public IList GetBorrowRecordList(QueryParam term, out int recordCount)
        {
            IBorrowRecord dal = (IBorrowRecord)FM2E.DALFactory.EquipmentAccess.CreateBorrowRecord();
            return dal.GetBorrowRecordList(term,out recordCount);
        }
        /// <summary>
        /// 获取某个设备的借调历史明细
        /// </summary>
        /// <param name="equipmentNO"></param>
        /// <returns></returns>
        public IList GetBorrowRecordHistory(string equipmentNO)
        {
            IBorrowRecord dal = (IBorrowRecord)FM2E.DALFactory.EquipmentAccess.CreateBorrowRecord();
            return dal.GetBorrowRecordHistory(equipmentNO);
        }
         /// <summary>
        /// 获取某个未归还设备的借出信息
        /// </summary>
        /// <param name="equipmentNO"></param>
        /// <returns></returns>
        public BorrowRecordInfo GetEquipmentNotReturned(string equipmentNO)
        {
            IBorrowRecord dal = (IBorrowRecord)FM2E.DALFactory.EquipmentAccess.CreateBorrowRecord();
            return dal.GetEquipmentNotReturned(equipmentNO);
        }
             /// <summary>
        /// 获取某个设备的借出记录
        /// </summary>
        /// <param name="borrowApplyID"></param>
        /// <param name="equipmentNO"></param>
        /// <returns></returns>
        public BorrowRecordInfo GetBorrowRecord(long borrowApplyID, string equipmentNO)
        {
            IBorrowRecord dal = (IBorrowRecord)FM2E.DALFactory.EquipmentAccess.CreateBorrowRecord();
            return dal.GetBorrowRecord(borrowApplyID, equipmentNO);
        }
        /// <summary>
        /// 设备借出登记
        /// </summary>
        /// <param name="borrowRecords"></param>
        public void AddBorrowRecord(IList borrowRecords)
        {
            IBorrowRecord dal = (IBorrowRecord)FM2E.DALFactory.EquipmentAccess.CreateBorrowRecord();
            dal.AddBorrowRecord(borrowRecords);
        }
        /// <summary>
        /// 删除某张借调申请单的设备借出记录
        /// </summary>
        /// <param name="borrowApplyID"></param>
        public void DeleteBorrowRecord(long borrowApplyID)
        {
            IBorrowRecord dal = (IBorrowRecord)FM2E.DALFactory.EquipmentAccess.CreateBorrowRecord();
            dal.DeleteBorrowRecord(borrowApplyID);
        }
        /// <summary>
        /// 删除某张借调申请单的某个设备的借出记录
        /// </summary>
        /// <param name="borrowApplyID"></param>
        /// <param name="equipmentNO"></param>
        public void DeleteBorrowRecord(long borrowApplyID, string equipmentNO)
        {
            IBorrowRecord dal = (IBorrowRecord)FM2E.DALFactory.EquipmentAccess.CreateBorrowRecord();
            dal.DeleteBorrowRecord(borrowApplyID, equipmentNO);
        }
        #endregion

        #region 归还验收
        /// <summary>
        /// 获取某一项的设备归还验收记录
        /// </summary>
        /// <param name="returnID"></param>
        /// <returns></returns>
        public ReturnAcceptanceInfo GetAcceptanceInfo(long returnID)
        {
            IReturnAcceptance dal = (IReturnAcceptance)FM2E.DALFactory.EquipmentAccess.CreateReturnAcceptance();
            return dal.GetAcceptanceInfo(returnID);
        }
        /// <summary>
        /// 获取某张借调申请单的验收记录
        /// </summary>
        /// <param name="borrowApplyID"></param>
        /// <returns></returns>
        public IList GetAcceptanceList(long borrowApplyID)
        {
            IReturnAcceptance dal = (IReturnAcceptance)FM2E.DALFactory.EquipmentAccess.CreateReturnAcceptance();
            return dal.GetAcceptanceList(borrowApplyID);
        }
        /// <summary>
        /// 获取验收记录（支持分页）
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetAcceptanceList(QueryParam term, out int recordCount)
        {
            IReturnAcceptance dal = (IReturnAcceptance)FM2E.DALFactory.EquipmentAccess.CreateReturnAcceptance();
            return dal.GetAcceptanceList(term,out recordCount);
        }

        /// <summary>
        /// 获取某个设备借调验收的历史明细
        /// </summary>
        /// <param name="equipmentNO"></param>
        /// <returns></returns>
        public IList GetBorrowAcceptanceHistory(string equipmentNO)
        {
            IReturnAcceptance dal = (IReturnAcceptance)FM2E.DALFactory.EquipmentAccess.CreateReturnAcceptance();
            return dal.GetBorrowAcceptanceHistory(equipmentNO);
        }

        /// <summary>
        /// 设备归还验收
        /// </summary>
        /// <param name="acceptanceRecords"></param>
        public void AddAcceptanceRecord(IList acceptanceRecords)
        {
            IReturnAcceptance dal = (IReturnAcceptance)FM2E.DALFactory.EquipmentAccess.CreateReturnAcceptance();
            dal.AddAcceptanceRecord(acceptanceRecords);
        }
        /// <summary>
        /// 删除某张归还申请单的某个设备的验收记录
        /// </summary>
        /// <param name="borrowApplyID"></param>
        /// <param name="equipmentNO"></param>
        public void DeleteAcceptanceRecord(long borrowApplyID, string equipmentNO)
        {
            IReturnAcceptance dal = (IReturnAcceptance)FM2E.DALFactory.EquipmentAccess.CreateReturnAcceptance();
            dal.DeleteAcceptanceRecord(borrowApplyID, equipmentNO);
        }
        #endregion

        #region 查询条件生成
        public QueryParam GenerateSearchTerm(BorrowApplySearchInfo item)
        {
            IBorrowApply dal = (IBorrowApply)FM2E.DALFactory.EquipmentAccess.CreateBorrowApply();
            return dal.GenerateSearchTerm(item);
        }
        public QueryParam GenerateSearchTerm(BorrowApplySearchInfo item, string[] WFStates)
        {
            IBorrowApply dal = (IBorrowApply)FM2E.DALFactory.EquipmentAccess.CreateBorrowApply();
            return dal.GenerateSearchTerm(item, WFStates);
        }
        /// <summary>
        /// 生成审批查询条件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public QueryParam GenerateSearchTerm(BorrowApprovalSearchInfo item)
        {
            IBorrowApply dal = (IBorrowApply)FM2E.DALFactory.EquipmentAccess.CreateBorrowApply();
            return dal.GenerateSearchTerm(item);
        }
        /// <summary>
        /// 生成借出登记信息查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public QueryParam GenerateSearchTerm(BorrowRecordSearchInfo term)
        {
            IBorrowRecord dal = (IBorrowRecord)FM2E.DALFactory.EquipmentAccess.CreateBorrowRecord();
            return dal.GenerateSearchTerm(term);
        }
        /// <summary>
        /// 生成归还验收记录查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public QueryParam GenerateSearchTerm(ReturnAcceptanceSearchInfo term)
        {
            IReturnAcceptance dal = (IReturnAcceptance)FM2E.DALFactory.EquipmentAccess.CreateReturnAcceptance();
            return dal.GenerateSearchTerm(term);
        }
        #endregion
    }
}
