using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.SpecialProject;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.IDAL.SpecialProject
{
    /// <summary>
    /// 专项工程数据库访问接口类
    /// </summary>
    public interface ISpecialProject
    {
        /// <summary>
        /// 插入专项工程基本信息
        /// </summary>
        /// <param name="project"></param>
        long InsertSpecialProjectInfo(SpecialProjectInfo project);
        /// <summary>
        /// 更新专项工程基本信息
        /// </summary>
        /// <param name="project"></param>
        void UpdateSpecialProjectInfo(SpecialProjectInfo project);
        /// <summary>
        /// 获取专项工程基本信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SpecialProjectInfo GetSpecialProjectInfo(long id);

        /// <summary>
        /// 删除专项工程
        /// </summary>
        /// <param name="id"></param>
        void DeleteSpecialProjectInfo(long id);

        /// <summary>
        /// 获取专项工程工作量列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList GetSpecialProjectJobList(long id);


        /// <summary>
        /// 获取专项工程预算列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList GetSpecialProjectBudgetList(long id);

        /// <summary>
        /// 根据公司获取专项工程
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <param name="companyid"></param>
        /// <param name="isFinish"></param>
        /// <returns></returns>
        IList GetSpecialProjectByCompany(int pageIndex, int pageSize, out int recordCount, string companyid, bool isFinish);

        /// <summary>
        /// 插入一个工程量项
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        long InsertJobItem(SpecialProjectJobItemInfo job);

        /// <summary>
        /// 更新一个工程量项
        /// </summary>
        /// <param name="job"></param>
        void UpdateJobItem(SpecialProjectJobItemInfo job);

        /// <summary>
        /// 删除工程项
        /// </summary>
        /// <param name="itemid"></param>
        void DeleteJobItem(long itemid);


        /// <summary>
        /// 插入一个预算项
        /// </summary>
        /// <param name="budget"></param>
        /// <returns></returns>
        long InsertBudgetItem(SpecialProjectBudgetItemInfo budget);

        /// <summary>
        /// 更新一个预算项
        /// </summary>
        /// <param name="budget"></param>
        /// <returns></returns>
        void UpdateBudgetItem(SpecialProjectBudgetItemInfo budget);

        /// <summary>
        /// 删除预算项
        /// </summary>
        /// <param name="itemid"></param>
        void DeleteBudgetItem(long itemid);

        /// <summary>
        /// 判断是否有设计存在
        /// </summary>
        /// <param name="projectid">项目ID</param>
        /// <returns></returns>
        bool ExistsDesign(long projectid);

        /// <summary>
        /// 插入设计记录
        /// </summary>
        /// <param name="design"></param>
        void InsertDesign(SpecialProjectDesignInfo design);

        /// <summary>
        /// 更新设计记录
        /// </summary>
        /// <param name="design"></param>
        void UpdateDesign(SpecialProjectDesignInfo design);

        /// <summary>
        /// 获取设计记录
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        SpecialProjectDesignInfo GetDesign(long projectid);

        /// <summary>
        /// 判断是否有招标信息存在
        /// </summary>
        /// <param name="projectid">项目ID</param>
        /// <returns></returns>
        bool ExistsBidding(long projectid);

        /// <summary>
        /// 插入招标记录
        /// </summary>
        /// <param name="design"></param>
        void InsertBidding(SpecialProjectBidInfo bid);

        /// <summary>
        /// 更新招标记录
        /// </summary>
        /// <param name="design"></param>
        void UpdateBidding(SpecialProjectBidInfo bid);

        /// <summary>
        /// 获取招标记录
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        SpecialProjectBidInfo GetBidding(long projectid);

        /// <summary>
        /// 插入一个施工项
        /// </summary>
        /// <param name="item"></param>
        long InsertPlanItem(SpecialProjectPlanInfo item);

        /// <summary>
        /// 更新一个施工项
        /// </summary>
        /// <param name="item"></param>
        void UpdatePlanItem(SpecialProjectPlanInfo item);

        /// <summary>
        /// 删除一个施工项，并且把其后继续项的前置项修改为其前置项
        /// </summary>
        /// <param name="itemid"></param>
        void DeletePlanItem(long itemid);

        /// <summary>
        /// 获取一个施工项
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        SpecialProjectPlanInfo GetPlanItem(long itemid);

        /// <summary>
        /// 获取施工项列表
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        IList GetPlanItemList(long projectid);

        /// <summary>
        /// 插入一个进场设备项
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        long InsertDeviceItem(SpecialProjectDeviceInfo device);

        /// <summary>
        /// 更新一个进场设备项
        /// </summary>
        /// <param name="job"></param>
        void UpdateDeviceItem(SpecialProjectDeviceInfo device);

        /// <summary>
        /// 删除进场设备项
        /// </summary>
        /// <param name="itemid"></param>
        void DeleteDeviceItem(long itemid);

        /// <summary>
        /// 获取进场设备清单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList GetSpecialProjectDeviceList(long projectid);


        /// <summary>
        /// 插入一个预支付项
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        long InsertPrePayItem(SpecialProjectPrePayInfo item);

        /// <summary>
        /// 更新一个预支付项
        /// </summary>
        /// <param name="job"></param>
        void UpdatePrePayItem(SpecialProjectPrePayInfo item);

        /// <summary>
        /// 删除预支付项
        /// </summary>
        /// <param name="itemid"></param>
        void DeletePrePayItem(long itemid);
        /// <summary>
        /// 获取预支付项清单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList GetSpecialProjectPrePayList(long id);

        /// <summary>
        /// 插入一个合同支付项
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        long InsertContractPayItem(SpecialProjectContractPayInfo item);

        /// <summary>
        /// 更新一个合同支付项
        /// </summary>
        /// <param name="job"></param>
        void UpdateContractPayItem(SpecialProjectContractPayInfo item);

        /// <summary>
        /// 删除合同支付项
        /// </summary>
        /// <param name="itemid"></param>
        void DeleteContractPayItem(long itemid);

        /// <summary>
        /// 获取合同支付项清单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList GetSpecialProjectContractPayList(long id);

        /// <summary>
        /// 插入一个进场设备记录
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        long InsertDeviceInRecord(SpecialProjectDeviceInRecord record);
        /// <summary>
        /// 更新一个进场设备记录
        /// </summary>
        /// <param name="job"></param>
        void UpdateDeviceInRecord(SpecialProjectDeviceInRecord record);

        /// <summary>
        /// 删除进场设备记录
        /// </summary>
        /// <param name="itemid"></param>
        void DeleteDeviceInRecord(long recordid);

        /// <summary>
        /// 获取进场设备记录清单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList GetSpecialProjectDeviceInRecordList(long deviceItemID);

        /// <summary>
        /// 更新一个进场设备项，增加进场数量
        /// </summary>
        /// <param name="job"></param>
        decimal UpdateDeviceItem(long itemid, decimal increaseamount, DateTime time);

        /// <summary>
        /// 插入一个进度检查记录
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        long InsertProgressCheckRecord(SpecialProjectCheckRecordInfo record);

        /// <summary>
        /// 更新一个进度检查记录
        /// </summary>
        /// <param name="job"></param>
        void UpdateProgressCheckRecord(SpecialProjectCheckRecordInfo record);

        /// <summary>
        /// 删除进度检查记录
        /// </summary>
        /// <param name="itemid"></param>
        void DeleteProgressCheckRecord(long recordid);

        /// <summary>
        /// 获取进度检查记录清单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList GetSpecialProjectProgressCheckRecordList(long planItemID);

        /// <summary>
        /// 插入一个月度支付项记录
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        void InsertMonthlyPayRecord(SpecialProjectPayRecordInfo record);
        /// <summary>
        /// 更新一个月度支付项记录
        /// </summary>
        /// <param name="job"></param>
        void UpdateMonthlyPayRecord(SpecialProjectPayRecordInfo record);

        /// <summary>
        /// 删除月度支付项记录
        /// </summary>
        /// <param name="itemid"></param>
        void DeleteMonthlyPayRecord(long projectid, int year, int month);

        /// <summary>
        /// 获取月度支付项记录清单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList GetSpecialProjectMonthlyPayRecordList(long projectid);

        /// <summary>
        /// 判断该月支付是否已经存在
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        bool ExistsMonthlyPayRecord(long projectid, int year, int month);


        /// <summary>
        /// 插入一个变更记录，包含变更详情
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        long InsertModifyInfo(SpecialProjectModifyInfo modify);

        /// <summary>
        /// 更新一个变更记录，含变更详情
        /// </summary>
        /// <param name="job"></param>
        void UpdateModifyInfo(SpecialProjectModifyInfo modify);

        /// <summary>
        /// 删除变更记录，删除变更详情
        /// </summary>
        /// <param name="itemid"></param>
        void DeleteModifyInfo(long modifyid);

        /// <summary>
        /// 获取变更列表，含变更详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList GetSpecialProjectModifyInfoList(long projectid);

        /// <summary>
        /// 获取变更修改单
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="modifyid"></param>
        /// <returns></returns>
        SpecialProjectModifyInfo GetSpecialProjectModifyInfo(long modifyid);

        /// <summary>
        /// 判断是否有验收记录存在
        /// </summary>
        /// <param name="projectid">项目ID</param>
        /// <returns></returns>
        bool ExistsCheckAccept(long projectid);

        /// <summary>
        /// 插入验收记录
        /// </summary>
        /// <param name="design"></param>
        void InserCheckAccept(SpecialProjectCheckAcceptInfo check);
        /// <summary>
        /// 更新验收记录
        /// </summary>
        /// <param name="design"></param>
        void UpdateCheckAccept(SpecialProjectCheckAcceptInfo check);

        /// <summary>
        /// 获取验收记录
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        SpecialProjectCheckAcceptInfo GetCheckAccept(long projectid);

        /// <summary>
        /// 获取一个公司的专项工程，根据状态
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        IList GetSpecialProjectByCompanyStatus(int pageIndex, int pageSize, out int recordCount, string companyid, params SpecialProjectStatus[] status);

        /// <summary>
        /// 插入审批项
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        void InsertApprovalRecord(SpecialProjectApprovalInfo record);
        /// <summary>
        /// 更新审批项
        /// </summary>
        /// <param name="job"></param>
        void UpdateApprovalyRecord(SpecialProjectApprovalInfo record);

        /// <summary>
        /// 删除审批项
        /// </summary>
        /// <param name="itemid"></param>
        void DeleteApprovalRecord(long itemid);

        /// <summary>
        /// 获取审批项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList GetSpecialProjectApprovalRecordList(long projectid);

        /// <summary>
        /// 生成查询信息参数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam GenerateSearchInfo(SpecialProjectSearchInfo item);

         /// <summary>
        /// 查找
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        IList SearchSpecialProject(QueryParam qp, out int recordCount);
    }
}
