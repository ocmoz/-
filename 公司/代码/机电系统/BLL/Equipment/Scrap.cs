using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using FM2E.IDAL.Equipment;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;

namespace FM2E.BLL.Equipment
{
    public class Scrap
    {
        #region 报废申请与审批
        /// <summary>
        /// 获取报废申请列表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetScrapApplyList(QueryParam term, out int recordCount)
        {
            IScrapApply dal = (IScrapApply)FM2E.DALFactory.EquipmentAccess.CreateScrapApply();
            return dal.GetScrapApplyList(term, out recordCount);
        }
        /// <summary>
        /// 获取审批历史记录
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetScrapApprovalHistory(QueryParam term, out int recordCount)
        {
            IScrapApply dal = (IScrapApply)FM2E.DALFactory.EquipmentAccess.CreateScrapApply();
            return dal.GetScrapApprovalHistory(term, out recordCount);
        }
        /// <summary>
        /// 获取特定报废申请的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ScrapApplyInfo GetScrapApply(long id)
        {
            IScrapApply dal = (IScrapApply)FM2E.DALFactory.EquipmentAccess.CreateScrapApply();
            return dal.GetScrapApply(id);
        }
        /// <summary>
        /// 添加报废申请
        /// </summary>
        /// <param name="model"></param>
        public long AddScrapApply(ScrapApplyInfo model)
        {
            IScrapApply dal = (IScrapApply)FM2E.DALFactory.EquipmentAccess.CreateScrapApply();
            return dal.AddScrapApply(model);
        }
        /// <summary>
        /// 更新报废申请
        /// </summary>
        /// <param name="model"></param>
        public void UpdateScrapApply(ScrapApplyInfo model)
        {
            IScrapApply dal = (IScrapApply)FM2E.DALFactory.EquipmentAccess.CreateScrapApply();
            dal.UpdateScrapApply(model);
        }
        /// <summary>
        /// 删除相应的报废申请
        /// </summary>
        /// <param name="id"></param
        /// >
        public void DeleteScrapApply(long id)
        {
            IScrapApply dal = (IScrapApply)FM2E.DALFactory.EquipmentAccess.CreateScrapApply();
            dal.DeleteScrapApply(id);
        }
        /// <summary>
        /// 审批报废申请
        /// </summary>
        /// <param name="model"></param>
        public void ApprovalScrapApply(ScrapApprovalInfo model)
        {
            IScrapApply dal = (IScrapApply)FM2E.DALFactory.EquipmentAccess.CreateScrapApply();
            dal.ApprovalScrapApply(model);
        }
        /// <summary>
        /// 改变申请单状态值
        /// </summary>
        /// <param name="borrowApplyID">申请单号</param>
        /// <param name="status">状态值</param>
        public void ChangeStatus(long scrapID, int status)
        {
            IScrapApply dal = (IScrapApply)FM2E.DALFactory.EquipmentAccess.CreateScrapApply();
            dal.ChangeStatus(scrapID, status);
        }
        #endregion

        #region 借出登记
        /// <summary>
        /// 获取某个报废申请的报废设备登记明细
        /// </summary>
        /// <param name="scrapID"></param>
        /// <returns></returns>
        public IList GetScrapRecordList(long scrapID)
        {
            IScrapRecord dal = (IScrapRecord)FM2E.DALFactory.EquipmentAccess.CreateScrapRecord();
            return dal.GetScrapRecordList(scrapID);
        }
        /// <summary>
        /// 获取某个报废申请的报废设备登记明细(支持分页)
        /// </summary>
        /// <param name="scrapID"></param>
        /// <returns></returns>
        public IList GetScrapRecordList(QueryParam term, out int recordCount)
        {
            IScrapRecord dal = (IScrapRecord)FM2E.DALFactory.EquipmentAccess.CreateScrapRecord();
            return dal.GetScrapRecordList(term, out recordCount);
        }
        /// <summary>
        /// 设备报废登记
        /// </summary>
        /// <param name="borrowRecords"></param>
        public void AddScrapRecord(ScrapRecordInfo item)
        {
            IScrapRecord dal = (IScrapRecord)FM2E.DALFactory.EquipmentAccess.CreateScrapRecord();
            dal.AddscrapRecord(item);
        }
        #endregion

        #region 查询条件生成
        public QueryParam GenerateSearchTerm(ScrapApplySearchInfo item)
        {
            IScrapApply dal = (IScrapApply)FM2E.DALFactory.EquipmentAccess.CreateScrapApply();
            return dal.GenerateSearchTerm(item);
        }
        public QueryParam GenerateSearchTerm(ScrapApplySearchInfo item, string[] WFStates)
        {
            IScrapApply dal = (IScrapApply)FM2E.DALFactory.EquipmentAccess.CreateScrapApply();
            return dal.GenerateSearchTerm(item, WFStates);
        }
        /// <summary>
        /// 生成审批查询条件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public QueryParam GenerateSearchTerm(ScrapApprovalSearchInfo item)
        {
            IScrapApply dal = (IScrapApply)FM2E.DALFactory.EquipmentAccess.CreateScrapApply();
            return dal.GenerateSearchTerm(item);
        }
        public QueryParam GenerateSearchTerm(ScrapRecordSearchInfo item)
        {
            IScrapRecord dal = (IScrapRecord)FM2E.DALFactory.EquipmentAccess.CreateScrapRecord();
            return dal.GenerateSearchTerm(item);
        }
        #endregion
    }
}
