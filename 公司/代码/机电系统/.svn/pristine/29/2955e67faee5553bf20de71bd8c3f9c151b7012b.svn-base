using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

using FM2E.Model.SpecialProject;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;
using FM2E.IDAL.SpecialProject;
using FM2E.SQLServerDAL.Utils;

namespace FM2E.SQLServerDAL.SpecialProject
{
    /// <summary>
    /// 专项工程模块数据库访问类
    /// </summary>
    public class SpecialProject:ISpecialProject
    {

        #region 表名
        /// <summary>
        /// 专项工程表
        /// </summary>
        const string TABLE_SPECIAL_PROJECT = "FM2E_SpecialProject";
        /// <summary>
        /// 专项工程审查审批表
        /// </summary>
        const string TABLE_SPECIAL_PROJECT_APPROVAL = "FM2E_SpecialProjectApproval";
        /// <summary>
        /// 专项工程招标情况
        /// </summary>
        const string TABLE_SPECIAL_PROJECT_BID = "FM2E_SpecialProjectBid";
        /// <summary>
        /// 专项工程预算清单
        /// </summary>
        const string TABLE_SPECIAL_PROJECT_BUDGET_ITEM = "FM2E_SpecialProjectBudgetItem";
        /// <summary>
        /// 专项工程验收
        /// </summary>
        const string TABLE_SPECIAL_PROJECT_CHECK_ACCEPT = "FM2E_SpecialProjectCheckAccept";
        /// <summary>
        /// 专项工程施工检查表
        /// </summary>
        const string TABLE_SPECIAL_PROJECT_CHECK_RECORD = "FM2E_SpecialProjectCheckRecord";
        /// <summary>
        /// 专项工程支付计划（合同支付）
        /// </summary>
        const string TABLE_SPECIAL_PROJECT_CONTRACT_PAY = "FM2E_SpecialProjectContractPay";
        /// <summary>
        /// 专项工程设计方案
        /// </summary>
        const string TABLE_SPECIAL_PROJECT_DESIGN = "FM2E_SpecialProjectDesign";
        /// <summary>
        /// 专项工程进场机器设备
        /// </summary>
        const string TABLE_SPECIAL_PROJECT_DEVICE = "FM2E_SpecialProjectDevice";
        /// <summary>
        /// 专项工程工程量清单
        /// </summary>
        const string TABLE_SPECIAL_PROJECT_JOB_ITEM = "FM2E_SpecialProjectJobItem";
        /// <summary>
        /// 专项工程变更
        /// </summary>
        const string TABLE_SPECIAL_PROJECT_MODIFY = "FM2E_SpecialProjectModify";
        /// <summary>
        /// 专项工程变更详情
        /// </summary>
        const string TABLE_SPECIAL_PROJECT_MODIFY_DEVICE_LIST = "FM2E_SpecialProjectModifyDeviceList";
        /// <summary>
        /// 专项工程支付记录（进度支付）
        /// </summary>
        const string TABLE_SPECIAL_PROJECT_PAY_RECORD = "FM2E_SpecialProjectPayRecord";
        /// <summary>
        /// 专项工程施工计划
        /// </summary>
        const string TABLE_SPECIAL_PROJECT_PLAN = "FM2E_SpecialProjectPlan";
        /// <summary>
        /// 专项工程支付计划（预支付）
        /// </summary>
        const string TABLE_SPECIAL_PROJECT_PRE_PAY = "FM2E_SpecialProjectPrePay";

        /// <summary>
        /// 专项工程设备进场记录
        /// </summary>
        const string TABLE_SPECIAL_PROJECT_DEVICE_IN_RECORD = "FM2E_SpecialProjectDeviceInRecord";

        /// <summary>
        /// 公司表
        /// </summary>
        const string TABLE_COMPANY = "FM2E_Company";

        #endregion

        #region GetDateObject函数
        /// <summary>
        /// 获取一个专项工程记录实体对象
        /// </summary>
        private SpecialProjectInfo GetDataSpecialProjectInfo(IDataReader rd)
        {
            SpecialProjectInfo item = new SpecialProjectInfo();
            if (!Convert.IsDBNull(rd["Budget"]))
            {
                item.Budget = Convert.ToDecimal(rd["Budget"]);
            }
            if (!Convert.IsDBNull(rd["BudgetName"]))
            {
                item.BudgetName = Convert.ToString(rd["BudgetName"]);
            }
            if (!Convert.IsDBNull(rd["CompanyID"]))
            {
                item.CompanyID = Convert.ToString(rd["CompanyID"]);
            }
            if (!Convert.IsDBNull(rd["CurrentStatus"]))
            {
                item.CurrentStatus = Convert.ToString(rd["CurrentStatus"]);
            }
            if (!Convert.IsDBNull(rd["CurrentStatusFile"]))
            {
                item.CurrentStatusFile = Convert.ToString(rd["CurrentStatusFile"]);
            }
            if (!Convert.IsDBNull(rd["Problem"]))
            {
                item.Problem = Convert.ToString(rd["Problem"]);
            }
            if (!Convert.IsDBNull(rd["ProblemFile"]))
            {
                item.ProblemFile = Convert.ToString(rd["ProblemFile"]);
            }
            if (!Convert.IsDBNull(rd["ProjectID"]))
            {
                item.ProjectID = Convert.ToInt64(rd["ProjectID"]);
            }
            if (!Convert.IsDBNull(rd["ProjectName"]))
            {
                item.ProjectName = Convert.ToString(rd["ProjectName"]);
            }
            if (!Convert.IsDBNull(rd["Solution"]))
            {
                item.Solution = Convert.ToString(rd["Solution"]);
            }
            if (!Convert.IsDBNull(rd["SolutionFile"]))
            {
                item.SolutionFile = Convert.ToString(rd["SolutionFile"]);
            }
            if (!Convert.IsDBNull(rd["Status"]))
            {
                item.Status = (SpecialProjectStatus)Enum.Parse(typeof(SpecialProjectStatus), Convert.ToInt32(rd["Status"]).ToString());
            }
            if (!Convert.IsDBNull(rd["Submitter"]))
            {
                item.Submitter = Convert.ToString(rd["Submitter"]);
            }
            if (!Convert.IsDBNull(rd["SubmitTime"]))
            {
                item.SubmitTime = Convert.ToDateTime(rd["SubmitTime"]);
            }
            if (!Convert.IsDBNull(rd["UpdateTime"]))
            {
                item.UpdateTime = Convert.ToDateTime(rd["UpdateTime"]);
            }
            if (!Convert.IsDBNull(rd["CompanyName"]))
            {
                item.CompanyName = Convert.ToString(rd["CompanyName"]);
            }
            return item;
        }

        /// <summary>
        /// 获取一个专项工程审批记录实体对象
        /// </summary>
        private SpecialProjectApprovalInfo GetDataSpecialProjectApprovalInfo(IDataReader rd)
        {
            SpecialProjectApprovalInfo item = new SpecialProjectApprovalInfo();
            if (!Convert.IsDBNull(rd["ApprovalDate"]))
            {
                item.ApprovalDate = Convert.ToDateTime(rd["ApprovalDate"]);
            }
            if (!Convert.IsDBNull(rd["Approvaler"]))
            {
                item.Approvaler = Convert.ToString(rd["Approvaler"]);
            }
            if (!Convert.IsDBNull(rd["ApprovalName"]))
            {
                item.ApprovalName = Convert.ToString(rd["ApprovalName"]);
            }
            if (!Convert.IsDBNull(rd["FeeBack"]))
            {
                item.FeeBack = Convert.ToString(rd["FeeBack"]);
            }
            if (!Convert.IsDBNull(rd["ApprovalFile"]))
            {
                item.ApprovalFile = Convert.ToString(rd["ApprovalFile"]);
            }
            if (!Convert.IsDBNull(rd["ItemID"]))
            {
                item.ItemID = Convert.ToInt64(rd["ItemID"]);
            }
            if (!Convert.IsDBNull(rd["ProjectID"]))
            {
                item.ProjectID = Convert.ToInt64(rd["ProjectID"]);
            }
            if (!Convert.IsDBNull(rd["Result"]))
            {
                item.Result = (SpecialProjectApprovalResult)Enum.Parse(typeof(SpecialProjectApprovalResult), Convert.ToInt16(rd["Result"]).ToString());
            }
            return item;
        }

        /// <summary>
        /// 获取一个专项工程招标记录实体对象
        /// </summary>
        private SpecialProjectBidInfo GetDataSpecialProjectBidInfo(IDataReader rd)
        {
            SpecialProjectBidInfo item = new SpecialProjectBidInfo();
            if (!Convert.IsDBNull(rd["ApprovalDate"]))
            {
                item.ApprovalDate = Convert.ToDateTime(rd["ApprovalDate"]);
            }
            if (!Convert.IsDBNull(rd["Approvaler"]))
            {
                item.Approvaler = Convert.ToString(rd["Approvaler"]);
            }
            if (!Convert.IsDBNull(rd["Attechment"]))
            {
                item.Attechment = Convert.ToString(rd["Attechment"]);
            }
            if (!Convert.IsDBNull(rd["BiddenCompany"]))
            {
                item.BiddenCompany = Convert.ToString(rd["BiddenCompany"]);
            }
            if (!Convert.IsDBNull(rd["BiddenCompanyInfo"]))
            {
                item.BiddenCompanyInfo = Convert.ToString(rd["BiddenCompanyInfo"]);
            }
            if (!Convert.IsDBNull(rd["FeeBack"]))
            {
                item.FeeBack = Convert.ToString(rd["FeeBack"]);
            }
            if (!Convert.IsDBNull(rd["ProjectID"]))
            {
                item.ProjectID = Convert.ToInt64(rd["ProjectID"]);
            }
            if (!Convert.IsDBNull(rd["Result"]))
            {
                item.Result = Convert.ToInt32(rd["Result"]);
            }
            return item;
        }

        /// <summary>
        /// 获取一个专项工程预算项实体对象
        /// </summary>
        private SpecialProjectBudgetItemInfo GetDataSpecialProjectBudgetItemInfo(IDataReader rd)
        {
            SpecialProjectBudgetItemInfo item = new SpecialProjectBudgetItemInfo();
            if (!Convert.IsDBNull(rd["Amount"]))
            {
                item.Amount = Convert.ToDecimal(rd["Amount"]);
            }
            if (!Convert.IsDBNull(rd["Amultiple"]))
            {
                item.Amultiple = Convert.ToDecimal(rd["Amultiple"]);
            }
            if (!Convert.IsDBNull(rd["BudgetItemName"]))
            {
                item.BudgetItemName = Convert.ToString(rd["BudgetItemName"]);
            }
            if (!Convert.IsDBNull(rd["ItemID"]))
            {
                item.ItemID = Convert.ToInt64(rd["ItemID"]);
            }
            if (!Convert.IsDBNull(rd["ProjectID"]))
            {
                item.ProjectID = Convert.ToInt64(rd["ProjectID"]);
            }
            if (!Convert.IsDBNull(rd["Remark"]))
            {
                item.Remark = Convert.ToString(rd["Remark"]);
            }
            if (!Convert.IsDBNull(rd["IsRelated2Direct"]))
            {
                item.IsRelated2Direct = Convert.ToBoolean(rd["IsRelated2Direct"]);
            }
            return item;
        }

        /// <summary>
        /// 获取一个专项工程验收信息记录实体对象
        /// </summary>
        private SpecialProjectCheckAcceptInfo GetDataSpecialProjectCheckAcceptInfo(IDataReader rd)
        {
            SpecialProjectCheckAcceptInfo item = new SpecialProjectCheckAcceptInfo();
            if (!Convert.IsDBNull(rd["Complete"]))
            {
                item.Complete = Convert.ToBoolean(rd["Complete"]);
            }
            if (!Convert.IsDBNull(rd["CompleteApprovaler"]))
            {
                item.CompleteApprovaler = Convert.ToString(rd["CompleteApprovaler"]);
            }
            if (!Convert.IsDBNull(rd["CompleteDate"]))
            {
                item.CompleteDate = Convert.ToDateTime(rd["CompleteDate"]);
            }
            if (!Convert.IsDBNull(rd["CompleteRemark"]))
            {
                item.CompleteRemark = Convert.ToString(rd["CompleteRemark"]);
            }
            if (!Convert.IsDBNull(rd["Finish"]))
            {
                item.Finish = Convert.ToBoolean(rd["Finish"]);
            }
            if (!Convert.IsDBNull(rd["FinishApprovaler"]))
            {
                item.FinishApprovaler = Convert.ToString(rd["FinishApprovaler"]);
            }
            if (!Convert.IsDBNull(rd["FinishDate"]))
            {
                item.FinishDate = Convert.ToDateTime(rd["FinishDate"]);
            }
            if (!Convert.IsDBNull(rd["FinishRemark"]))
            {
                item.FinishRemark = Convert.ToString(rd["FinishRemark"]);
            }
            if (!Convert.IsDBNull(rd["Pass"]))
            {
                item.Pass = Convert.ToBoolean(rd["Pass"]);
            }
            if (!Convert.IsDBNull(rd["PassApprovaler"]))
            {
                item.PassApprovaler = Convert.ToString(rd["PassApprovaler"]);
            }
            if (!Convert.IsDBNull(rd["PassDate"]))
            {
                item.PassDate = Convert.ToDateTime(rd["PassDate"]);
            }
            if (!Convert.IsDBNull(rd["PassRemark"]))
            {
                item.PassRemark = Convert.ToString(rd["PassRemark"]);
            }
            if (!Convert.IsDBNull(rd["ProjectID"]))
            {
                item.ProjectID = Convert.ToInt64(rd["ProjectID"]);
            }
            return item;
        }

        /// <summary>
        /// 获取一个专项工程进度检查记录实体对象
        /// </summary>
        private SpecialProjectCheckRecordInfo GetDataSpecialProjectCheckRecordInfo(IDataReader rd)
        {
            SpecialProjectCheckRecordInfo item = new SpecialProjectCheckRecordInfo();
            if (!Convert.IsDBNull(rd["Checker"]))
            {
                item.Checker = Convert.ToString(rd["Checker"]);
            }
            if (!Convert.IsDBNull(rd["CheckTime"]))
            {
                item.CheckTime = Convert.ToDateTime(rd["CheckTime"]);
            }
            if (!Convert.IsDBNull(rd["HR"]))
            {
                item.HR = Convert.ToString(rd["HR"]);
            }
            if (!Convert.IsDBNull(rd["ItemID"]))
            {
                item.ItemID = Convert.ToInt64(rd["ItemID"]);
            }
            if (!Convert.IsDBNull(rd["PlanItemID"]))
            {
                item.PlanItemID = Convert.ToInt64(rd["PlanItemID"]);
            }
            if (!Convert.IsDBNull(rd["Progress"]))
            {
                item.Progress = Convert.ToDecimal(rd["Progress"]);
            }
            if (!Convert.IsDBNull(rd["ProjectID"]))
            {
                item.ProjectID = Convert.ToInt64(rd["ProjectID"]);
            }
            if (!Convert.IsDBNull(rd["Quality"]))
            {
                item.Quality = Convert.ToString(rd["Quality"]);
            }
            if (!Convert.IsDBNull(rd["Remark"]))
            {
                item.Remark = Convert.ToString(rd["Remark"]);
            }
            
            return item;
        }

        /// <summary>
        /// 获取一个专项工程合同支付项实体对象
        /// </summary>
        private SpecialProjectContractPayInfo GetDataSpecialProjectContractPayInfo(IDataReader rd)
        {
            SpecialProjectContractPayInfo item = new SpecialProjectContractPayInfo();
            if (!Convert.IsDBNull(rd["Amount"]))
            {
                item.Amount = Convert.ToDecimal(rd["Amount"]);
            }
            if (!Convert.IsDBNull(rd["DaysAfter"]))
            {
                item.DaysAfter = Convert.ToInt32(rd["DaysAfter"]);
            }
            if (!Convert.IsDBNull(rd["ItemID"]))
            {
                item.ItemID = Convert.ToInt64(rd["ItemID"]);
            }
            if (!Convert.IsDBNull(rd["Method"]))
            {
                item.Method = Convert.ToString(rd["Method"]);
            }
            if (!Convert.IsDBNull(rd["PlanItemID"]))
            {
                item.PlanItemID = Convert.ToInt64(rd["PlanItemID"]);
            }
            if (!Convert.IsDBNull(rd["Paid"]))
            {
                item.Paid = Convert.ToDecimal(rd["Paid"]);
            }
            if (!Convert.IsDBNull(rd["ProjectID"]))
            {
                item.ProjectID = Convert.ToInt64(rd["ProjectID"]);
            }
            if (!Convert.IsDBNull(rd["Payee"]))
            {
                item.Payee = Convert.ToString(rd["Payee"]);
            }
            if (!Convert.IsDBNull(rd["Remark"]))
            {
                item.Remark = Convert.ToString(rd["Remark"]);
            }
            if (!Convert.IsDBNull(rd["ItemName"]))
            {
                item.ItemName = Convert.ToString(rd["ItemName"]);
            }
            if (!Convert.IsDBNull(rd["PlanItemName"]))
            {
                item.PlanItemName = Convert.ToString(rd["PlanItemName"]);
            }
            return item;
        }

        /// <summary>
        /// 获取一个专项工程设计方案记录实体对象
        /// </summary>
        private SpecialProjectDesignInfo GetDataSpecialProjectDesignInfo(IDataReader rd)
        {
            SpecialProjectDesignInfo item = new SpecialProjectDesignInfo();
            if (!Convert.IsDBNull(rd["ApprovalDate"]))
            {
                item.ApprovalDate = Convert.ToDateTime(rd["ApprovalDate"]);
            }
            if (!Convert.IsDBNull(rd["Approvaler"]))
            {
                item.Approvaler = Convert.ToString(rd["Approvaler"]);
            }
            if (!Convert.IsDBNull(rd["Attechment"]))
            {
                item.Attechment = Convert.ToString(rd["Attechment"]);
            }
            if (!Convert.IsDBNull(rd["Design"]))
            {
                item.Design = Convert.ToString(rd["Design"]);
            }
            if (!Convert.IsDBNull(rd["Designer"]))
            {
                item.Designer = Convert.ToString(rd["Designer"]);
            }
            if (!Convert.IsDBNull(rd["FeeBack"]))
            {
                item.FeeBack = Convert.ToString(rd["FeeBack"]);
            }
            if (!Convert.IsDBNull(rd["ProjectID"]))
            {
                item.ProjectID = Convert.ToInt64(rd["ProjectID"]);
            }
            if (!Convert.IsDBNull(rd["Result"]))
            {
                item.Result = Convert.ToInt32(rd["Result"]);
            }
            if (!Convert.IsDBNull(rd["DesignerInfo"]))
            {
                item.DesignerInfo = Convert.ToString(rd["DesignerInfo"]);
            }
            if (!Convert.IsDBNull(rd["DesignName"]))
            {
                item.DesignName = Convert.ToString(rd["DesignName"]);
            }
            if (!Convert.IsDBNull(rd["DesignCost"]))
            {
                item.DesignCost = Convert.ToDecimal(rd["DesignCost"]);
            }
            if (!Convert.IsDBNull(rd["ProjectCost"]))
            {
                item.ProjectCost = Convert.ToDecimal(rd["ProjectCost"]);
            }
            return item;
        }

        /// <summary>
        /// 获取一个专项工程进场设备项实体对象
        /// </summary>
        private SpecialProjectDeviceInfo GetDataSpecialProjectDeviceInfo(IDataReader rd)
        {
            SpecialProjectDeviceInfo item = new SpecialProjectDeviceInfo();
            if (!Convert.IsDBNull(rd["ActualCount"]))
            {
                item.ActualCount = Convert.ToDecimal(rd["ActualCount"]);
            }
            if (!Convert.IsDBNull(rd["DeviceName"]))
            {
                item.DeviceName = Convert.ToString(rd["DeviceName"]);
            }
            if (!Convert.IsDBNull(rd["ItemID"]))
            {
                item.ItemID = Convert.ToInt64(rd["ItemID"]);
            }
            if (!Convert.IsDBNull(rd["LastInCount"]))
            {
                item.LastInCount = Convert.ToDecimal(rd["LastInCount"]);
            }
            if (!Convert.IsDBNull(rd["ProjectID"]))
            {
                item.ProjectID = Convert.ToInt64(rd["ProjectID"]);
            }
            if (!Convert.IsDBNull(rd["Model"]))
            {
                item.Model = Convert.ToString(rd["Model"]);
            }
            if (!Convert.IsDBNull(rd["PlanCount"]))
            {
                item.PlanCount = Convert.ToDecimal(rd["PlanCount"]);
            }
            if (!Convert.IsDBNull(rd["Size"]))
            {
                item.Size = Convert.ToString(rd["Size"]);
            }
            if (!Convert.IsDBNull(rd["Status"]))
            {
                item.Status = Convert.ToString(rd["Status"]);
            }
            if (!Convert.IsDBNull(rd["Time"]))
            {
                item.Time = Convert.ToDateTime(rd["Time"]);
            }
            if (!Convert.IsDBNull(rd["Usage"]))
            {
                item.Usage = Convert.ToString(rd["Usage"]);
            }
            return item;
        }

        /// <summary>
        /// 获取一个专项工程工作量项实体对象
        /// </summary>
        private SpecialProjectJobItemInfo GetDataSpecialProjectJobItemInfo(IDataReader rd)
        {
            SpecialProjectJobItemInfo item = new SpecialProjectJobItemInfo();
            if (!Convert.IsDBNull(rd["Count"]))
            {
                item.Count = Convert.ToDecimal(rd["Count"]);
            }
            if (!Convert.IsDBNull(rd["Equipment"]))
            {
                item.Equipment = Convert.ToString(rd["Equipment"]);
            }
            if (!Convert.IsDBNull(rd["Model"]))
            {
                item.Model = Convert.ToString(rd["Model"]);
            }
            if (!Convert.IsDBNull(rd["ItemID"]))
            {
                item.ItemID = Convert.ToInt64(rd["ItemID"]);
            }
            if (!Convert.IsDBNull(rd["ProjectID"]))
            {
                item.ProjectID = Convert.ToInt64(rd["ProjectID"]);
            }
            if (!Convert.IsDBNull(rd["Remark"]))
            {
                item.Remark = Convert.ToString(rd["Remark"]);
            }
            if (!Convert.IsDBNull(rd["Unit"]))
            {
                item.Unit = Convert.ToString(rd["Unit"]);
            }
            if (!Convert.IsDBNull(rd["UnitPrice"]))
            {
                item.UnitPrice = Convert.ToDecimal(rd["UnitPrice"]);
            }

            

            return item;
        }

        /// <summary>
        /// 获取一个专项工程变更详情设备项实体对象
        /// </summary>
        private SpecialProjectModifyDeviceInfo GetDataSpecialProjectModifyDeviceInfo(IDataReader rd)
        {
            SpecialProjectModifyDeviceInfo item = new SpecialProjectModifyDeviceInfo();
            if (!Convert.IsDBNull(rd["Amount"]))
            {
                item.Amount = Convert.ToDecimal(rd["Amount"]);
            }
            if (!Convert.IsDBNull(rd["DeviceName"]))
            {
                item.DeviceName = Convert.ToString(rd["DeviceName"]);
            }
            if (!Convert.IsDBNull(rd["ItemID"]))
            {
                item.ItemID = Convert.ToInt64(rd["ItemID"]);
            }
            if (!Convert.IsDBNull(rd["Count"]))
            {
                item.Count = Convert.ToDecimal(rd["Count"]);
            }
            if (!Convert.IsDBNull(rd["ProjectID"]))
            {
                item.ProjectID = Convert.ToInt64(rd["ProjectID"]);
            }
            if (!Convert.IsDBNull(rd["Model"]))
            {
                item.Model = Convert.ToString(rd["Model"]);
            }
            if (!Convert.IsDBNull(rd["IsAdd"]))
            {
                item.IsAdd = Convert.ToBoolean(rd["IsAdd"]);
            }
            if (!Convert.IsDBNull(rd["ModifyApplyID"]))
            {
                item.ModifyApplyID = Convert.ToInt32(rd["ModifyApplyID"]);
            }
            if (!Convert.IsDBNull(rd["Unit"]))
            {
                item.Unit = Convert.ToString(rd["Unit"]);
            }
            if (!Convert.IsDBNull(rd["Remark"]))
            {
                item.Remark = Convert.ToString(rd["Remark"]);
            }
            if (!Convert.IsDBNull(rd["UnitPrice"]))
            {
                item.UnitPrice = Convert.ToDecimal(rd["UnitPrice"]);
            } 
            
            return item;
        }

        /// <summary>
        /// 获取一个专项工程变更记录实体对象
        /// </summary>
        private SpecialProjectModifyInfo GetDataSpecialProjectModifyInfo(IDataReader rd)
        {
            SpecialProjectModifyInfo item = new SpecialProjectModifyInfo();
            if (!Convert.IsDBNull(rd["ApplyTime"]))
            {
                item.ApplyTime = Convert.ToDateTime(rd["ApplyTime"]);
            }
            if (!Convert.IsDBNull(rd["BudgetChange"]))
            {
                item.BudgetChange = Convert.ToDecimal(rd["BudgetChange"]);
            }
            if (!Convert.IsDBNull(rd["BudgetIncDesc"]))
            {
                item.BudgetIncDesc = Convert.ToDecimal(rd["BudgetIncDesc"]);
            }
            if (!Convert.IsDBNull(rd["ChangeContent"]))
            {
                item.ChangeContent = Convert.ToString(rd["ChangeContent"]);
            }
            if (!Convert.IsDBNull(rd["ContentAttechment"]))
            {
                item.ContentAttechment = Convert.ToString(rd["ContentAttechment"]);
            }
            if (!Convert.IsDBNull(rd["ContractApprovalDate"]))
            {
                item.ContractApprovalDate = Convert.ToDateTime(rd["ContractApprovalDate"]);
            }
            if (!Convert.IsDBNull(rd["ContractApprovaler"]))
            {
                item.ContractApprovaler = Convert.ToString(rd["ContractApprovaler"]);
            }
            if (!Convert.IsDBNull(rd["ContractFeeBack"]))
            {
                item.ContractFeeBack = Convert.ToString(rd["ContractFeeBack"]);
            }
            if (!Convert.IsDBNull(rd["ContractResult"]))
            {
                item.ContractResult = (SpecialProjectModifyApprovalResult)Enum.Parse(typeof(SpecialProjectModifyApprovalResult),
                    Convert.ToInt16(rd["ContractResult"]).ToString());
               
            }
            if (!Convert.IsDBNull(rd["DelayDays"]))
            {
                item.DelayDays = Convert.ToInt32(rd["DelayDays"]);
            }

            if (!Convert.IsDBNull(rd["ModifyID"]))
            {
                item.ModifyID = Convert.ToInt64(rd["ModifyID"]);
            }
            if (!Convert.IsDBNull(rd["ProjectID"]))
            {
                item.ProjectID = Convert.ToInt64(rd["ProjectID"]);
            }

            if (!Convert.IsDBNull(rd["LeaderApprovalDate"]))
            {
                item.LeaderApprovalDate = Convert.ToDateTime(rd["LeaderApprovalDate"]);
            }
            if (!Convert.IsDBNull(rd["LeaderApprovaler"]))
            {
                item.LeaderApprovaler = Convert.ToString(rd["LeaderApprovaler"]);
            }
            if (!Convert.IsDBNull(rd["LeaderFeeBack"]))
            {
                item.LeaderFeeBack = Convert.ToString(rd["LeaderFeeBack"]);
            }
            if (!Convert.IsDBNull(rd["LeaderResult"]))
            {
                
                item.LeaderResult = (SpecialProjectModifyApprovalResult)Enum.Parse(typeof(SpecialProjectModifyApprovalResult),
                    Convert.ToInt16(rd["LeaderResult"]).ToString());
            }

            if (!Convert.IsDBNull(rd["OwnerApprovalDate"]))
            {
                item.OwnerApprovalDate = Convert.ToDateTime(rd["OwnerApprovalDate"]);
            }
            if (!Convert.IsDBNull(rd["OwnerApprovaler"]))
            {
                item.OwnerApprovaler = Convert.ToString(rd["OwnerApprovaler"]);
            }
            if (!Convert.IsDBNull(rd["OwnerFeeBack"]))
            {
                item.OwnerFeeBack = Convert.ToString(rd["OwnerFeeBack"]);
            }
            if (!Convert.IsDBNull(rd["OwnerResult"]))
            {

                item.OwnerResult = (SpecialProjectModifyApprovalResult)Enum.Parse(typeof(SpecialProjectModifyApprovalResult),
                    Convert.ToInt16(rd["OwnerResult"]).ToString());
            }

            if (!Convert.IsDBNull(rd["Remark"]))
            {
                item.Remark = Convert.ToString(rd["Remark"]);
            }

            if (!Convert.IsDBNull(rd["Status"]))
            {
                item.Status = (SpecialProjectModifyStatus)Enum.Parse(typeof(SpecialProjectModifyStatus),
                    Convert.ToInt16(rd["Status"]).ToString());
            }
           
            return item;
        }

        /// <summary>
        /// 获取一个专项工程月支付记录项实体对象
        /// </summary>
        private SpecialProjectPayRecordInfo GetDataSpecialProjectPayRecordInfo(IDataReader rd)
        {
            SpecialProjectPayRecordInfo item = new SpecialProjectPayRecordInfo();
            if (!Convert.IsDBNull(rd["Amount"]))
            {
                item.Amount = Convert.ToDecimal(rd["Amount"]);
            }
            if (!Convert.IsDBNull(rd["Method"]))
            {
                item.Method = Convert.ToString(rd["Method"]);
            }
            if (!Convert.IsDBNull(rd["Month"]))
            {
                item.Month = Convert.ToInt32(rd["Month"]);
            }
            if (!Convert.IsDBNull(rd["Year"]))
            {
                item.Year = Convert.ToInt32(rd["Year"]);
            }
            if (!Convert.IsDBNull(rd["ProjectID"]))
            {
                item.ProjectID = Convert.ToInt64(rd["ProjectID"]);
            }
            if (!Convert.IsDBNull(rd["Remark"]))
            {
                item.Remark = Convert.ToString(rd["Remark"]);
            }
            if (!Convert.IsDBNull(rd["Payee"]))
            {
                item.Payee = Convert.ToString(rd["Payee"]);
            }
            if (!Convert.IsDBNull(rd["Paid"]))
            {
                item.Paid = Convert.ToDecimal(rd["Paid"]);
            }
            if (!Convert.IsDBNull(rd["PayTime"]))
            {
                item.PayTime = Convert.ToDateTime(rd["PayTime"]);
            }
            if (!Convert.IsDBNull(rd["Progress"]))
            {
                item.Progress = Convert.ToDecimal(rd["Progress"]);
            }
            return item;
        }

        /// <summary>
        /// 获取一个专项工程进度计划项记录项实体对象
        /// </summary>
        private SpecialProjectPlanInfo GetDataSpecialProjectPlanInfo(IDataReader rd)
        {
            SpecialProjectPlanInfo item = new SpecialProjectPlanInfo();
            if (!Convert.IsDBNull(rd["Days"]))
            {
                item.Days = Convert.ToInt32(rd["Days"]);
            }
            if (!Convert.IsDBNull(rd["DevicePlan"]))
            {
                item.DevicePlan = Convert.ToString(rd["DevicePlan"]);
            }
            if (!Convert.IsDBNull(rd["EndTime"]))
            {
                item.EndTime = Convert.ToDateTime(rd["EndTime"]);
            }
            if (!Convert.IsDBNull(rd["HRPlan"]))
            {
                item.HRPlan = Convert.ToString(rd["HRPlan"]);
            }
            if (!Convert.IsDBNull(rd["ProjectID"]))
            {
                item.ProjectID = Convert.ToInt64(rd["ProjectID"]);
            }
            if (!Convert.IsDBNull(rd["ItemID"]))
            {
                item.ItemID = Convert.ToInt64(rd["ItemID"]);
            }
            if (!Convert.IsDBNull(rd["ItemName"]))
            {
                item.ItemName = Convert.ToString(rd["ItemName"]);
            }
            if (!Convert.IsDBNull(rd["PrefixItemID"]))
            {
                item.PrefixItemID = Convert.ToInt64(rd["PrefixItemID"]);
            }
            if (!Convert.IsDBNull(rd["StartTime"]))
            {
                item.StartTime = Convert.ToDateTime(rd["StartTime"]);
            }
            if (!Convert.IsDBNull(rd["Progress"]))
            {
                item.Progress = Convert.ToDecimal(rd["Progress"]);
            }
            if (!Convert.IsDBNull(rd["Status"]))
            {
                item.Status = (SpecialProjectPlanStatus)Enum.Parse(typeof(SpecialProjectPlanStatus), Convert.ToInt32(rd["Status"]).ToString());
            }
            if (!Convert.IsDBNull(rd["PrefixItemName"]))
            {
                item.PrefixItemName = Convert.ToString(rd["PrefixItemName"]);
            }
            if (!Convert.IsDBNull(rd["DaysAfter"]))
            {
                item.DaysAfter = Convert.ToInt32(rd["DaysAfter"]);
            }
            return item;
        }

        /// <summary>
        /// 获取一个专项工程预支付项实体对象
        /// </summary>
        private SpecialProjectPrePayInfo GetDataSpecialProjectPrePayInfo(IDataReader rd)
        {
            SpecialProjectPrePayInfo item = new SpecialProjectPrePayInfo();
            if (!Convert.IsDBNull(rd["Amount"]))
            {
                item.Amount = Convert.ToDecimal(rd["Amount"]);
            }
            if (!Convert.IsDBNull(rd["ItemName"]))
            {
                item.ItemName = Convert.ToString(rd["ItemName"]);
            }
            if (!Convert.IsDBNull(rd["ItemID"]))
            {
                item.ItemID = Convert.ToInt64(rd["ItemID"]);
            }
            if (!Convert.IsDBNull(rd["Method"]))
            {
                item.Method = Convert.ToString(rd["Method"]);
            }
            if (!Convert.IsDBNull(rd["Time"]))
            {
                item.Time = Convert.ToDateTime(rd["Time"]);
            }
            if (!Convert.IsDBNull(rd["Paid"]))
            {
                item.Paid = Convert.ToDecimal(rd["Paid"]);
            }
            if (!Convert.IsDBNull(rd["ProjectID"]))
            {
                item.ProjectID = Convert.ToInt64(rd["ProjectID"]);
            }
            if (!Convert.IsDBNull(rd["Payee"]))
            {
                item.Payee = Convert.ToString(rd["Payee"]);
            }
            if (!Convert.IsDBNull(rd["Remark"]))
            {
                item.Remark = Convert.ToString(rd["Remark"]);
            }
            return item;
        }


        /// <summary>
        /// 获取一个专项工程设备进场记录实体
        /// </summary>
        private SpecialProjectDeviceInRecord GetDataSpecialProjectDeviceInRecord(IDataReader rd)
        {
            SpecialProjectDeviceInRecord item = new SpecialProjectDeviceInRecord();
            if (!Convert.IsDBNull(rd["Count"]))
            {
                item.Count = Convert.ToDecimal(rd["Count"]);
            }
            
            if (!Convert.IsDBNull(rd["ItemID"]))
            {
                item.ItemID = Convert.ToInt64(rd["ItemID"]);
            }
            if (!Convert.IsDBNull(rd["RecordID"]))
            {
                item.RecordID = Convert.ToInt64(rd["RecordID"]);
            }
            if (!Convert.IsDBNull(rd["Time"]))
            {
                item.Time = Convert.ToDateTime(rd["Time"]);
            }
           
            if (!Convert.IsDBNull(rd["ProjectID"]))
            {
                item.ProjectID = Convert.ToInt64(rd["ProjectID"]);
            }
            
            return item;
        }


        #endregion

        #region 各表插入函数
        /// <summary>
        /// 插入表TABLE_SPECIAL_PROJECT_APPROVAL记录
        /// </summary>
        private void InsertSpecialProjectApprovalInfo(SqlTransaction trans, SpecialProjectApprovalInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + TABLE_SPECIAL_PROJECT_APPROVAL + "(");
            strSql.Append("ProjectID,ApprovalName,Approvaler,Result,FeeBack,ApprovalFile,ApprovalDate)");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@ApprovalName,@Approvaler,@Result,@FeeBack,@ApprovalFile,@ApprovalDate)");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@ApprovalName", SqlDbType.NVarChar,20),
					new SqlParameter("@Approvaler", SqlDbType.VarChar,20),
					new SqlParameter("@Result", SqlDbType.TinyInt,1),
					new SqlParameter("@FeeBack", SqlDbType.NVarChar,50),
                    new SqlParameter("@ApprovalFile", SqlDbType.VarChar,80),
					new SqlParameter("@ApprovalDate", SqlDbType.DateTime)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.ApprovalName;
            parameters[2].Value = model.Approvaler;
            parameters[3].Value = model.Result;
            parameters[4].Value = model.FeeBack;
            parameters[5].Value = model.ApprovalFile;
            parameters[6].Value = model.ApprovalDate;
            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 插入表TABLE_SPECIAL_PROJECT_BID记录
        /// </summary>
        private void InsertSpecialProjectBidInfo(SqlTransaction trans, SpecialProjectBidInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+ TABLE_SPECIAL_PROJECT_BID +"(");
            strSql.Append("ProjectID,BiddenCompany,BiddenCompanyInfo,Attechment,Approvaler,Result,FeeBack,ApprovalDate)");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@BiddenCompany,@BiddenCompanyInfo,@Attechment,@Approvaler,@Result,@FeeBack,@ApprovalDate)");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@BiddenCompany", SqlDbType.NVarChar,20),
					new SqlParameter("@BiddenCompanyInfo", SqlDbType.NVarChar,1000),
					new SqlParameter("@Attechment", SqlDbType.VarChar,80),
					new SqlParameter("@Approvaler", SqlDbType.VarChar,20),
					new SqlParameter("@Result", SqlDbType.TinyInt,1),
					new SqlParameter("@FeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@ApprovalDate", SqlDbType.DateTime)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.BiddenCompany;
            parameters[2].Value = model.BiddenCompanyInfo;
            parameters[3].Value = model.Attechment;
            parameters[4].Value = model.Approvaler;
            parameters[5].Value = model.Result;
            parameters[6].Value = model.FeeBack;
            parameters[7].Value = model.ApprovalDate == DateTime.MinValue ? SqlDateTime.Null : model.ApprovalDate;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 插入表TABLE_SPECIAL_PROJECT_BUDGET_ITEM记录
        /// </summary>
        private long InsertSpecialProjectBudgetItemInfo(SqlTransaction trans, SpecialProjectBudgetItemInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+ TABLE_SPECIAL_PROJECT_BUDGET_ITEM +"(");
            strSql.Append("ProjectID,BudgetItemName,Amultiple,Amount,Remark,IsRelated2Direct)");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@BudgetItemName,@Amultiple,@Amount,@Remark,@IsRelated2Direct)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@BudgetItemName", SqlDbType.NVarChar,40),
					new SqlParameter("@Amultiple", SqlDbType.Decimal,9),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
                    new SqlParameter("@IsRelated2Direct",SqlDbType.Bit)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.BudgetItemName;
            parameters[2].Value = model.Amultiple;
            parameters[3].Value = model.Amount;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.IsRelated2Direct;

            //读取ID
            long id = 1;
            if (trans != null)
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            else
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            return id;
        }

        /// <summary>
        /// 插入表TABLE_SPECIAL_PROJECT_CHECK_ACCEPT记录
        /// </summary>
        private void InsertSpecialProjectCheckAcceptInfo(SqlTransaction trans, SpecialProjectCheckAcceptInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+ TABLE_SPECIAL_PROJECT_CHECK_ACCEPT + "(");
            strSql.Append("ProjectID,Finish,FinishDate,FinishRemark,FinishApprovaler,Complete,CompleteDate,CompleteRemark,CompleteApprovaler,Pass,PassDate,PassRemark,PassApprovaler)");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@Finish,@FinishDate,@FinishRemark,@FinishApprovaler,@Complete,@CompleteDate,@CompleteRemark,@CompleteApprovaler,@Pass,@PassDate,@PassRemark,@PassApprovaler)");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@Finish", SqlDbType.Bit,1),
					new SqlParameter("@FinishDate", SqlDbType.DateTime),
					new SqlParameter("@FinishRemark", SqlDbType.NVarChar,100),
					new SqlParameter("@FinishApprovaler", SqlDbType.VarChar,20),
					new SqlParameter("@Complete", SqlDbType.Bit,1),
					new SqlParameter("@CompleteDate", SqlDbType.DateTime),
					new SqlParameter("@CompleteRemark", SqlDbType.NVarChar,100),
					new SqlParameter("@CompleteApprovaler", SqlDbType.VarChar,20),
					new SqlParameter("@Pass", SqlDbType.Bit,1),
					new SqlParameter("@PassDate", SqlDbType.DateTime),
					new SqlParameter("@PassRemark", SqlDbType.NVarChar,100),
					new SqlParameter("@PassApprovaler", SqlDbType.VarChar,20)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.Finish;
            parameters[2].Value = model.FinishDate == DateTime.MinValue ? SqlDateTime.Null : model.FinishDate;
            parameters[3].Value = model.FinishRemark;
            parameters[4].Value = model.FinishApprovaler;
            parameters[5].Value = model.Complete;
            parameters[6].Value = model.CompleteDate == DateTime.MinValue ? SqlDateTime.Null : model.CompleteDate;
            parameters[7].Value = model.CompleteRemark;
            parameters[8].Value = model.CompleteApprovaler;
            parameters[9].Value = model.Pass;
            parameters[10].Value = model.PassDate == DateTime.MinValue ? SqlDateTime.Null : model.PassDate;
            parameters[11].Value = model.PassRemark;
            parameters[12].Value = model.PassApprovaler;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 插入表TABLE_SPECIAL_PROJECT_CHECK_RECORD记录
        /// </summary>
        private long InsertSpecialProjectCheckRecordInfo(SqlTransaction trans, SpecialProjectCheckRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+ TABLE_SPECIAL_PROJECT_CHECK_RECORD+"(");
            strSql.Append("ProjectID,CheckTime,PlanItemID,Progress,Quality,HR,Checker,Remark)");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@CheckTime,@PlanItemID,@Progress,@Quality,@HR,@Checker,@Remark)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@CheckTime", SqlDbType.DateTime),
					new SqlParameter("@PlanItemID", SqlDbType.Int,4),
					new SqlParameter("@Progress", SqlDbType.Decimal,5),
					new SqlParameter("@Quality", SqlDbType.NVarChar,50),
					new SqlParameter("@HR", SqlDbType.NVarChar,50),
					new SqlParameter("@Checker", SqlDbType.VarChar,20),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.CheckTime == DateTime.MinValue ? SqlDateTime.Null : model.CheckTime;
            parameters[2].Value = model.PlanItemID;
            parameters[3].Value = model.Progress;
            parameters[4].Value = model.Quality;
            parameters[5].Value = model.HR;
            parameters[6].Value = model.Checker;
            parameters[7].Value = model.Remark;

            //读取ID
            long id = 1;
            if (trans != null)
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            else
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            return id;
        }

        /// <summary>
        /// 插入表TABLE_SPECIAL_PROJECT_CONTRACT_PAY记录
        /// </summary>
        private long InsertSpecialProjectContractPayInfo(SqlTransaction trans, SpecialProjectContractPayInfo model)
        {
           
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+ TABLE_SPECIAL_PROJECT_CONTRACT_PAY+"(");
            strSql.Append("ProjectID,ItemName,PlanItemID,DaysAfter,Amount,Method,Paid,Payee,Remark)");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@ItemName,@PlanItemID,@DaysAfter,@Amount,@Method,@Paid,@Payee,@Remark)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
                    new SqlParameter("@ItemName",SqlDbType.NVarChar,20),
					new SqlParameter("@PlanItemID", SqlDbType.Int,4),
					new SqlParameter("@DaysAfter", SqlDbType.Int,4),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
					new SqlParameter("@Method", SqlDbType.NVarChar,20),
					new SqlParameter("@Paid", SqlDbType.Decimal,9),
					new SqlParameter("@Payee", SqlDbType.VarChar,20),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.ItemName;
            parameters[2].Value = model.PlanItemID;
            parameters[3].Value = model.DaysAfter;
            parameters[4].Value = model.Amount;
            parameters[5].Value = model.Method;
            parameters[6].Value = model.Paid;
            parameters[7].Value = model.Payee;
            parameters[8].Value = model.Remark;

            //读取ID
            long id = 1;
            if (trans != null)
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            else
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            return id;
        }

        /// <summary>
        /// 插入表TABLE_SPECIAL_PROJECT_DESIGN记录
        /// </summary>
        private void InsertSpecialProjectDesignInfo(SqlTransaction trans, SpecialProjectDesignInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+TABLE_SPECIAL_PROJECT_DESIGN+"(");
            strSql.Append("ProjectID,ApprovalDate,DesignName,Designer,DesignerInfo,Design,Attechment,Approvaler,Result,FeeBack,DesignCost,ProjectCost )");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@ApprovalDate,@DesignName,@Designer,@DesignerInfo,@Design,@Attechment,@Approvaler,@Result,@FeeBack,@DesignCost,@ProjectCost )");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@ApprovalDate", SqlDbType.DateTime),
					new SqlParameter("@DesignName", SqlDbType.NVarChar,20),
					new SqlParameter("@Designer", SqlDbType.NVarChar,20),
					new SqlParameter("@DesignerInfo", SqlDbType.NVarChar,100),
					new SqlParameter("@Design", SqlDbType.NVarChar,2000),
					new SqlParameter("@Attechment", SqlDbType.VarChar,80),
					new SqlParameter("@Approvaler", SqlDbType.VarChar,20),
					new SqlParameter("@Result", SqlDbType.TinyInt,1),
					new SqlParameter("@FeeBack", SqlDbType.NVarChar,50),
                    new SqlParameter("@DesignCost",SqlDbType.Decimal),
                    new SqlParameter("@ProjectCost",SqlDbType.Decimal)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.ApprovalDate == DateTime.MinValue ? SqlDateTime.Null : model.ApprovalDate;
            parameters[2].Value = model.DesignName;
            parameters[3].Value = model.Designer;
            parameters[4].Value = model.DesignerInfo;
            parameters[5].Value = model.Design;
            parameters[6].Value = model.Attechment;
            parameters[7].Value = model.Approvaler;
            parameters[8].Value = model.Result;
            parameters[9].Value = model.FeeBack;
            parameters[10].Value = model.DesignCost;
            parameters[11].Value = model.ProjectCost;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 插入表TABLE_SPECIAL_PROJECT_DEVICE记录
        /// </summary>
        private long InsertSpecialProjectDeviceInfo(SqlTransaction trans, SpecialProjectDeviceInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+TABLE_SPECIAL_PROJECT_DEVICE+"(");
            strSql.Append("ProjectID,LastInCount,Time,DeviceName,Model,Size,Usage,Status,PlanCount,ActualCount)");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@LastInCount,@Time,@DeviceName,@Model,@Size,@Usage,@Status,@PlanCount,@ActualCount)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@LastInCount", SqlDbType.Decimal,9),
					new SqlParameter("@Time", SqlDbType.DateTime),
					new SqlParameter("@DeviceName", SqlDbType.NVarChar,20),
					new SqlParameter("@Model", SqlDbType.NVarChar,20),
					new SqlParameter("@Size", SqlDbType.NVarChar,20),
					new SqlParameter("@Usage", SqlDbType.NVarChar,100),
					new SqlParameter("@Status", SqlDbType.NVarChar,50),
					new SqlParameter("@PlanCount", SqlDbType.Decimal,9),
					new SqlParameter("@ActualCount", SqlDbType.Decimal,9)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.LastInCount;
            parameters[2].Value = model.Time == DateTime.MinValue ? SqlDateTime.Null : model.Time;
            parameters[3].Value = model.DeviceName;
            parameters[4].Value = model.Model;
            parameters[5].Value = model.Size;
            parameters[6].Value = model.Usage;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.PlanCount;
            parameters[9].Value = model.ActualCount;

            //读取ID
            long id = 1;
            if (trans != null)
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            else
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            return id;
        }

        /// <summary>
        /// 插入表TABLE_SPECIAL_PROJECT记录
        /// </summary>
        private long InsertSpecialProjectInfo(SqlTransaction trans, SpecialProjectInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+ TABLE_SPECIAL_PROJECT +"(");
            strSql.Append("Solution,SolutionFile,Status,CompanyID,ProjectName,BudgetName,Budget,CurrentStatus,CurrentStatusFile,Problem,ProblemFile,Submitter,SubmitTime,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@Solution,@SolutionFile,@Status,@CompanyID,@ProjectName,@BudgetName,@Budget,@CurrentStatus,@CurrentStatusFile,@Problem,@ProblemFile,@Submitter,@SubmitTime,@UpdateTime)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@Solution", SqlDbType.NVarChar,2000),
					new SqlParameter("@SolutionFile", SqlDbType.VarChar,80),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@ProjectName", SqlDbType.NVarChar,50),
					new SqlParameter("@BudgetName", SqlDbType.NVarChar,50),
					new SqlParameter("@Budget", SqlDbType.Decimal,9),
					new SqlParameter("@CurrentStatus", SqlDbType.NVarChar,1000),
					new SqlParameter("@CurrentStatusFile", SqlDbType.VarChar,80),
					new SqlParameter("@Problem", SqlDbType.NVarChar,2000),
					new SqlParameter("@ProblemFile", SqlDbType.VarChar,80),
                    new SqlParameter("@Submitter",SqlDbType.VarChar,20),
                    new SqlParameter("@SubmitTime",SqlDbType.DateTime),
                    new SqlParameter("@UpdateTime",SqlDbType.DateTime)};
            parameters[0].Value = model.Solution;
            parameters[1].Value = model.SolutionFile;
            parameters[2].Value = (int)model.Status;
            parameters[3].Value = model.CompanyID;
            parameters[4].Value = model.ProjectName;
            parameters[5].Value = model.BudgetName;
            parameters[6].Value = model.Budget;
            parameters[7].Value = model.CurrentStatus;
            parameters[8].Value = model.CurrentStatusFile;
            parameters[9].Value = model.Problem;
            parameters[10].Value = model.ProblemFile;
            parameters[11].Value = model.Submitter;
            parameters[12].Value = model.SubmitTime == DateTime.MinValue ? SqlDateTime.Null : model.SubmitTime;
            parameters[13].Value = model.UpdateTime == DateTime.MinValue ? SqlDateTime.Null : model.UpdateTime;
            //读取ID
            long id = 1;
            if (trans != null)
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            else
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            return id;
        }

        /// <summary>
        /// 插入表TABLE_SPECIAL_PROJECT_JOB_ITEM记录
        /// </summary>
        private long InsertSpecialProjectJobItemInfo(SqlTransaction trans, SpecialProjectJobItemInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+TABLE_SPECIAL_PROJECT_JOB_ITEM+"(");
            strSql.Append("ProjectID,Equipment,Model,Count,Unit,UnitPrice,Remark)");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@Equipment,@Model,@Count,@Unit,@UnitPrice,@Remark)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@Equipment", SqlDbType.NVarChar,40),
					new SqlParameter("@Model", SqlDbType.NVarChar,40),
					new SqlParameter("@Count", SqlDbType.Decimal,9),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@UnitPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.Equipment;
            parameters[2].Value = model.Model;
            parameters[3].Value = model.Count;
            parameters[4].Value = model.Unit;
            parameters[5].Value = model.UnitPrice;
            parameters[6].Value = model.Remark;

            //读取ID
            long id = 1;
            if (trans != null)
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            else
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            return id;
        }

        /// <summary>
        /// 插入表TABLE_SPECIAL_PROJECT_MODIFY_DEVICE_LIST记录
        /// </summary>
        private void InsertSpecialProjectModifyDeviceInfo(SqlTransaction trans, SpecialProjectModifyDeviceInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+TABLE_SPECIAL_PROJECT_MODIFY_DEVICE_LIST+"(");
            strSql.Append("ProjectID,Amount,Remark,ModifyApplyID,IsAdd,DeviceName,Model,Count,UnitPrice,Unit)");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@Amount,@Remark,@ModifyApplyID,@IsAdd,@DeviceName,@Model,@Count,@UnitPrice,@Unit)");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@ModifyApplyID", SqlDbType.BigInt,4),
					new SqlParameter("@IsAdd", SqlDbType.Bit,1),
					new SqlParameter("@DeviceName", SqlDbType.NVarChar,20),
					new SqlParameter("@Model", SqlDbType.NVarChar,20),
					new SqlParameter("@Count", SqlDbType.Decimal,9),
					new SqlParameter("@UnitPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.Amount;
            parameters[2].Value = model.Remark;
            parameters[3].Value = model.ModifyApplyID;
            parameters[4].Value = model.IsAdd;
            parameters[5].Value = model.DeviceName;
            parameters[6].Value = model.Model;
            parameters[7].Value = model.Count;
            parameters[8].Value = model.UnitPrice;
            parameters[9].Value = model.Unit;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 插入表TABLE_SPECIAL_PROJECT_MODIFY记录
        /// </summary>
        private long InsertSpecialProjectModifyInfo(SqlTransaction trans, SpecialProjectModifyInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+TABLE_SPECIAL_PROJECT_MODIFY+"(");
            strSql.Append("OwnerApprovaler,OwnerResult,OwnerFeeBack,OwnerApprovalDate,ContractApprovaler,ContractResult,ContractFeeBack,ContractApprovalDate,LeaderApprovaler,LeaderResult,ProjectID,LeaderFeeBack,LeaderApprovalDate,ApplyTime,BudgetChange,BudgetIncDesc,DelayDays,ChangeContent,ContentAttechment,Remark,Status)");
            strSql.Append(" values (");
            strSql.Append("@OwnerApprovaler,@OwnerResult,@OwnerFeeBack,@OwnerApprovalDate,@ContractApprovaler,@ContractResult,@ContractFeeBack,@ContractApprovalDate,@LeaderApprovaler,@LeaderResult,@ProjectID,@LeaderFeeBack,@LeaderApprovalDate,@ApplyTime,@BudgetChange,@BudgetIncDesc,@DelayDays,@ChangeContent,@ContentAttechment,@Remark,@Status)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@OwnerApprovaler", SqlDbType.VarChar,20),
					new SqlParameter("@OwnerResult", SqlDbType.TinyInt,1),
					new SqlParameter("@OwnerFeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@OwnerApprovalDate", SqlDbType.DateTime),
					new SqlParameter("@ContractApprovaler", SqlDbType.VarChar,20),
					new SqlParameter("@ContractResult", SqlDbType.TinyInt,1),
					new SqlParameter("@ContractFeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@ContractApprovalDate", SqlDbType.DateTime),
					new SqlParameter("@LeaderApprovaler", SqlDbType.VarChar,20),
					new SqlParameter("@LeaderResult", SqlDbType.TinyInt,1),
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@LeaderFeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@LeaderApprovalDate", SqlDbType.DateTime),
					new SqlParameter("@ApplyTime", SqlDbType.DateTime),
					new SqlParameter("@BudgetChange", SqlDbType.Decimal,9),
					new SqlParameter("@BudgetIncDesc", SqlDbType.Decimal,9),
					new SqlParameter("@DelayDays", SqlDbType.Int,4),
					new SqlParameter("@ChangeContent", SqlDbType.NVarChar,1000),
					new SqlParameter("@ContentAttechment", SqlDbType.VarChar,80),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
                    new SqlParameter("@Status",SqlDbType.TinyInt)};
            parameters[0].Value = model.OwnerApprovaler;
            parameters[1].Value = model.OwnerResult;
            parameters[2].Value = model.OwnerFeeBack;
            parameters[3].Value = model.OwnerApprovalDate == DateTime.MinValue ? SqlDateTime.Null : model.OwnerApprovalDate;
            parameters[4].Value = model.ContractApprovaler;
            parameters[5].Value = model.ContractResult;
            parameters[6].Value = model.ContractFeeBack;
            parameters[7].Value = model.ContractApprovalDate == DateTime.MinValue ? SqlDateTime.Null : model.ContractApprovalDate;
            parameters[8].Value = model.LeaderApprovaler;
            parameters[9].Value = model.LeaderResult;
            parameters[10].Value = model.ProjectID;
            parameters[11].Value = model.LeaderFeeBack;
            parameters[12].Value = model.LeaderApprovalDate == DateTime.MinValue ? SqlDateTime.Null : model.LeaderApprovalDate;
            parameters[13].Value = model.ApplyTime == DateTime.MinValue ? SqlDateTime.Null : model.ApplyTime;
            parameters[14].Value = model.BudgetChange;
            parameters[15].Value = model.BudgetIncDesc;
            parameters[16].Value = model.DelayDays;
            parameters[17].Value = model.ChangeContent;
            parameters[18].Value = model.ContentAttechment;
            parameters[19].Value = model.Remark;
            parameters[20].Value = model.Status;

            //读取ID
            long id = 1;
            if (trans != null)
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            else
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            return id;
        }

        /// <summary>
        /// 插入表TABLE_SPECIAL_PROJECT_PAY_RECORD记录
        /// </summary>
        private void InsertSpecialProjectPayRecordInfo(SqlTransaction trans, SpecialProjectPayRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+TABLE_SPECIAL_PROJECT_PAY_RECORD+"(");
            strSql.Append("ProjectID,Remark,Year,Month,PayTime,Progress,Amount,Method,Paid,Payee)");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@Remark,@Year,@Month,@PayTime,@Progress,@Amount,@Method,@Paid,@Payee)");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@Month", SqlDbType.Int,4),
					new SqlParameter("@PayTime", SqlDbType.DateTime),
					new SqlParameter("@Progress", SqlDbType.Decimal,5),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
					new SqlParameter("@Method", SqlDbType.NVarChar,20),
					new SqlParameter("@Paid", SqlDbType.Decimal,9),
					new SqlParameter("@Payee", SqlDbType.VarChar,20)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.Year;
            parameters[3].Value = model.Month;
            parameters[4].Value = model.PayTime;
            parameters[5].Value = model.Progress;
            parameters[6].Value = model.Amount;
            parameters[7].Value = model.Method;
            parameters[8].Value = model.Paid;
            parameters[9].Value = model.Payee;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 插入表TABLE_SPECIAL_PROJECT_PLAN记录
        /// </summary>
        private long InsertSpecialProjectPlanInfo(SqlTransaction trans, SpecialProjectPlanInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+TABLE_SPECIAL_PROJECT_PLAN+"(");
            strSql.Append("ProjectID,Progress,Status,ItemName,PrefixItemID,StartTime,EndTime,Days,HRPlan,DevicePlan,DaysAfter)");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@Progress,@Status,@ItemName,@PrefixItemID,@StartTime,@EndTime,@Days,@HRPlan,@DevicePlan,@DaysAfter)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@Progress", SqlDbType.Decimal,5),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@ItemName", SqlDbType.NVarChar,20),
					new SqlParameter("@PrefixItemID", SqlDbType.Int,4),
					new SqlParameter("@StartTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@Days", SqlDbType.Int,4),
					new SqlParameter("@HRPlan", SqlDbType.NVarChar,100),
					new SqlParameter("@DevicePlan", SqlDbType.NVarChar,100),
                    new SqlParameter("@DaysAfter",SqlDbType.Int)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.Progress;
            parameters[2].Value = model.Status;
            parameters[3].Value = model.ItemName;
            parameters[4].Value = model.PrefixItemID;
            parameters[5].Value = model.StartTime;
            parameters[6].Value = model.EndTime;
            parameters[7].Value = model.Days;
            parameters[8].Value = model.HRPlan;
            parameters[9].Value = model.DevicePlan;
            parameters[10].Value = model.DaysAfter;

            //读取ID
            long id = 1;
            if (trans != null)
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            else
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            return id;
        }

        /// <summary>
        /// 插入表TABLE_SPECIAL_PROJECT_PRE_PAY记录
        /// </summary>
        private long InsertSpecialProjectPrePayInfo(SqlTransaction trans, SpecialProjectPrePayInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+TABLE_SPECIAL_PROJECT_PRE_PAY+"(");
            strSql.Append("ProjectID,ItemName,Time,Amount,Method,Paid,Payee,Remark)");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@ItemName,@Time,@Amount,@Method,@Paid,@Payee,@Remark)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@ItemName", SqlDbType.NVarChar,20),
					new SqlParameter("@Time", SqlDbType.DateTime),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
					new SqlParameter("@Method", SqlDbType.NVarChar,20),
					new SqlParameter("@Paid", SqlDbType.Decimal,9),
					new SqlParameter("@Payee", SqlDbType.VarChar,20),
					new SqlParameter("@Remark", SqlDbType.VarChar,100)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.ItemName;
            parameters[2].Value = model.Time;
            parameters[3].Value = model.Amount;
            parameters[4].Value = model.Method;
            parameters[5].Value = model.Paid;
            parameters[6].Value = model.Payee;
            parameters[7].Value = model.Remark;
            //读取ID
            long id = 1;
            if (trans != null)
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            else
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            return id;
        }


        /// <summary>
        /// 插入表TABLE_SPECIAL_PROJECT_DEVICE_IN_RECORD记录
        /// </summary>
        private long InsertSpecialProjectDeviceInRecord(SqlTransaction trans, SpecialProjectDeviceInRecord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + TABLE_SPECIAL_PROJECT_DEVICE_IN_RECORD + "(");
            strSql.Append("ItemID,ProjectID,Count,Time)");
            strSql.Append(" values (");
            strSql.Append("@ItemID,@ProjectID,@Count,@Time)");
            strSql.Append(";select cast( @@IDENTITY as bigint);");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@Count", SqlDbType.Decimal,9),
					new SqlParameter("@Time", SqlDbType.DateTime)};
            parameters[0].Value = model.ItemID;
            parameters[1].Value = model.ProjectID;
            parameters[2].Value = model.Count;
            parameters[3].Value = model.Time == DateTime.MinValue ? SqlDateTime.Null : model.Time;

            //读取ID
            long id = 1;
            if (trans != null)
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            else
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            return id;
        }
        #endregion

        #region 各表读取列表函数
        /// <summary>
        /// 获取专项工程记录列表
        /// </summary>
        private IList GetSpecialProjectInfoList(long ProjectID)
        {
            string table = TABLE_SPECIAL_PROJECT + " s1 left join " + TABLE_COMPANY + " s4 on " +
                " s1.CompanyID = s4.CompanyID ";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select s1.*,s4.CompanyName from " + table + " ");
            strSql.Append(" where ProjectID=@ProjectID ");
            strSql.Append(" order by ProjectID ASC;");//排序
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;

            List<SpecialProjectInfo> list = new List<SpecialProjectInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    SpecialProjectInfo item = GetDataSpecialProjectInfo(rd);
                    list.Add(item);
                }
            }
            return list;
        }



        /// <summary>
        /// 获取专项工程审批记录列表
        /// </summary>
        private IList GetSpecialProjectApprovalInfoList(long ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ItemID,ProjectID,ApprovalName,Approvaler,Result,FeeBack,ApprovalFile,ApprovalDate from " + TABLE_SPECIAL_PROJECT_APPROVAL + " ");
            strSql.Append(" where ProjectID=@ProjectID ");
            strSql.Append(" order by ItemID desc;");//排序
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;


            List<SpecialProjectApprovalInfo> list = new List<SpecialProjectApprovalInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    SpecialProjectApprovalInfo item = GetDataSpecialProjectApprovalInfo(rd);
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取专项工程投标记录列表
        /// </summary>
        private IList GetSpecialProjectBidInfoList(long ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ProjectID,BiddenCompany,BiddenCompanyInfo,Attechment,Approvaler,Result,FeeBack,ApprovalDate from " + TABLE_SPECIAL_PROJECT_BID + " ");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;

            
            List<SpecialProjectBidInfo> list = new List<SpecialProjectBidInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    SpecialProjectBidInfo item = GetDataSpecialProjectBidInfo(rd);
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取专项工程预算记录列表
        /// </summary>
        private IList GetSpecialProjectBudgetItemInfoList(long ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ItemID,ProjectID,BudgetItemName,Amultiple,Amount,Remark,IsRelated2Direct from " + TABLE_SPECIAL_PROJECT_BUDGET_ITEM + " ");
            strSql.Append(" where ProjectID=@ProjectID ");
            strSql.Append(" order by ItemID ASC;");//排序
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;

          
            List<SpecialProjectBudgetItemInfo> list = new List<SpecialProjectBudgetItemInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    SpecialProjectBudgetItemInfo item = GetDataSpecialProjectBudgetItemInfo(rd);
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取专项工程验收记录列表
        /// </summary>
        private IList GetSpecialProjectCheckAcceptInfoList(long ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ProjectID,Finish,FinishDate,FinishRemark,FinishApprovaler,Complete,CompleteDate,CompleteRemark,CompleteApprovaler,Pass,PassDate,PassRemark,PassApprovaler from " + TABLE_SPECIAL_PROJECT_CHECK_ACCEPT + " ");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;

            List<SpecialProjectCheckAcceptInfo> list = new List<SpecialProjectCheckAcceptInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    SpecialProjectCheckAcceptInfo item = GetDataSpecialProjectCheckAcceptInfo(rd);
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取专项工程进度检查记录列表
        /// </summary>
        private IList GetSpecialProjectCheckRecordInfoList(long PlanItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ItemID,ProjectID,CheckTime,PlanItemID,Progress,Quality,HR,Checker,Remark from " + TABLE_SPECIAL_PROJECT_CHECK_RECORD + " ");
            strSql.Append(" where PlanItemID=@PlanItemID ");
            strSql.Append(" order by ItemID ASC;");//排序
            SqlParameter[] parameters = {
					new SqlParameter("@PlanItemID", SqlDbType.BigInt)};
            parameters[0].Value = PlanItemID;

      
            List<SpecialProjectCheckRecordInfo> list = new List<SpecialProjectCheckRecordInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    SpecialProjectCheckRecordInfo item = GetDataSpecialProjectCheckRecordInfo(rd);
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取专项工程合同支付项记录列表
        /// </summary>
        private IList GetSpecialProjectContractPayInfoList(long ProjectID)
        {
            string table = TABLE_SPECIAL_PROJECT_CONTRACT_PAY + " AS a LEFT OUTER JOIN " +
                          TABLE_SPECIAL_PROJECT_PLAN + " AS b ON " +
                          " a.PlanItemID = b.ItemID ";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 a.ItemID,a.ProjectID,a.ItemName,a.PlanItemID,b.ItemName as PlanItemName,a.DaysAfter,a.Amount,a.Method,a.Paid,a.Payee,a.Remark from " + table + " ");
            strSql.Append(" where a.ProjectID=@ProjectID ");
            strSql.Append(" order by a.ItemID ASC;");//排序
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;

            List<SpecialProjectContractPayInfo> list = new List<SpecialProjectContractPayInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    SpecialProjectContractPayInfo item = GetDataSpecialProjectContractPayInfo(rd);
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取专项工程设计方案记录列表
        /// </summary>
        private IList GetSpecialProjectDesignInfoList(long ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ProjectID,ApprovalDate,DesignName,Designer,DesignerInfo,Design,Attechment,Approvaler,Result,FeeBack,DesignCost,ProjectCost  from " + TABLE_SPECIAL_PROJECT_DESIGN + " ");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;

            List<SpecialProjectDesignInfo> list = new List<SpecialProjectDesignInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    SpecialProjectDesignInfo item = GetDataSpecialProjectDesignInfo(rd);
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取专项工程进场设备信息记录列表
        /// </summary>
        private IList GetSpecialProjectDeviceInfoList(long ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ItemID,LastInCount,Time,ProjectID,DeviceName,Model,Size,Usage,Status,PlanCount,ActualCount from " + TABLE_SPECIAL_PROJECT_DEVICE + " ");
            strSql.Append(" where ProjectID=@ProjectID ");
            strSql.Append(" order by ItemID ASC;");//排序
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;

           
            List<SpecialProjectDeviceInfo> list = new List<SpecialProjectDeviceInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    SpecialProjectDeviceInfo item = GetDataSpecialProjectDeviceInfo(rd);
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取专项工程工作项记录列表
        /// </summary>
        private IList GetSpecialProjectJobItemInfoList(long ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ItemID,ProjectID,Equipment,Model,Count,Unit,UnitPrice,Remark from " + TABLE_SPECIAL_PROJECT_JOB_ITEM + " ");
            strSql.Append(" where ProjectID=@ProjectID ");
            strSql.Append(" order by ItemID ASC;");//排序
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;


            List<SpecialProjectJobItemInfo> list = new List<SpecialProjectJobItemInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    SpecialProjectJobItemInfo item = GetDataSpecialProjectJobItemInfo(rd);
                    list.Add(item);
                }
            }
            return list;
        }


        /// <summary>
        /// 获取一条专项工程设备进场记录列表
        /// </summary>
        /// <param name="ItemID">设备项ID</param>
        private IList GetSpecialProjectDeviceInRecordList(long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select RecordID,ItemID,ProjectID,Count,Time from " + TABLE_SPECIAL_PROJECT_DEVICE_IN_RECORD + " ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            List<SpecialProjectDeviceInRecord> list = new List<SpecialProjectDeviceInRecord>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    SpecialProjectDeviceInRecord item = GetDataSpecialProjectDeviceInRecord(rd);
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取专项工程变更详情设备记录列表
        /// </summary>
        private IList GetSpecialProjectModifyDeviceInfoList(long ModifyApplyID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ItemID,Amount,Remark,ProjectID,ModifyApplyID,IsAdd,DeviceName,Model,Count,UnitPrice,Unit from " + TABLE_SPECIAL_PROJECT_MODIFY_DEVICE_LIST + " ");
            strSql.Append(" where ModifyApplyID=@ModifyApplyID ");
            strSql.Append(" order by ItemID ASC;");//排序
            SqlParameter[] parameters = {
					new SqlParameter("@ModifyApplyID", SqlDbType.BigInt)};
            parameters[0].Value = ModifyApplyID;

            
            List<SpecialProjectModifyDeviceInfo> list = new List<SpecialProjectModifyDeviceInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    SpecialProjectModifyDeviceInfo item = GetDataSpecialProjectModifyDeviceInfo(rd);
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取专项工程变更记录列表
        /// </summary>
        private IList GetSpecialProjectModifyInfoList(long ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ModifyID,OwnerApprovaler,OwnerResult,OwnerFeeBack,OwnerApprovalDate,ContractApprovaler,ContractResult,ContractFeeBack,ContractApprovalDate,LeaderApprovaler,LeaderResult,ProjectID,LeaderFeeBack,LeaderApprovalDate,ApplyTime,BudgetChange,BudgetIncDesc,DelayDays,ChangeContent,ContentAttechment,Remark,Status from " + TABLE_SPECIAL_PROJECT_MODIFY + " ");
            strSql.Append(" where ProjectID=@ProjectID ");
            strSql.Append(" order by ApplyTime DESC;");//排序
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;


            List<SpecialProjectModifyInfo> list = new List<SpecialProjectModifyInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    SpecialProjectModifyInfo item = GetDataSpecialProjectModifyInfo(rd);
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取专项工程进度支付项记录列表
        /// </summary>
        private IList GetSpecialProjectPayRecordInfoList(long ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ProjectID,Remark,Year,Month,PayTime,Progress,Amount,Method,Paid,Payee from " + TABLE_SPECIAL_PROJECT_PAY_RECORD + " ");
            strSql.Append(" where ProjectID=@ProjectID ");
            strSql.Append(" order by Year ASC, Month ASC;");//排序
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;

           
            List<SpecialProjectPayRecordInfo> list = new List<SpecialProjectPayRecordInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    SpecialProjectPayRecordInfo item = GetDataSpecialProjectPayRecordInfo(rd);
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取专项工程工作计划项记录列表
        /// </summary>
        private IList GetSpecialProjectPlanInfoList(long ProjectID)
        {
            string table = TABLE_SPECIAL_PROJECT_PLAN + " AS a LEFT OUTER JOIN " +
                           TABLE_SPECIAL_PROJECT_PLAN + " AS b ON " +
                           " a.PrefixItemID = b.ItemID ";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  a.ItemID,a.Progress,a.Status,a.ProjectID,a.ItemName,a.PrefixItemID,b.ItemName AS PrefixItemName, a.StartTime,a.EndTime,a.Days,a.HRPlan,a.DevicePlan,a.DaysAfter from "
                + table + " ");
            strSql.Append(" where a.ProjectID=@ProjectID ");
            strSql.Append(" order by a.ItemID ASC;");//排序
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;

            List<SpecialProjectPlanInfo> list = new List<SpecialProjectPlanInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    SpecialProjectPlanInfo item = GetDataSpecialProjectPlanInfo(rd);
                    list.Add(item);
                }
            }

            
            return list;
        }

        /// <summary>
        /// 获取专项工程工作计划项的后继列表记录列表
        /// </summary>
        /// <param name="itemID">本项ID</param>
        private IList GetSpecialProjectPlanInfoSucceedList( long itemID)
        {

            string table = TABLE_SPECIAL_PROJECT_PLAN + " AS a LEFT OUTER JOIN " +
                           TABLE_SPECIAL_PROJECT_PLAN + " AS b ON " +
                           " a.PrefixItemID = b.ItemID ";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ItemID,a.Progress,a.Status,a.ProjectID,a.ItemName,a.PrefixItemID,b.ItemName as PrefixItemName,a.StartTime,a.EndTime,a.Days,a.HRPlan,a.DevicePlan,a.DaysAfter from " + table + " ");
            //strSql.Append(" where ProjectID=@ProjectID ");
            strSql.Append(" where a.PrefixItemID=@PrefixItemID ");
            strSql.Append(" order by a.ItemID ASC;");//排序
            SqlParameter[] parameters = {
					//new SqlParameter("@ProjectID", SqlDbType.BigInt),
                    new SqlParameter("@PrefixItemID",SqlDbType.BigInt)};

            parameters[0].Value = itemID;

            List<SpecialProjectPlanInfo> list = new List<SpecialProjectPlanInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    SpecialProjectPlanInfo item = GetDataSpecialProjectPlanInfo(rd);
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取专项工程预支付项记录列表
        /// </summary>
        private IList GetSpecialProjectPrePayInfoList(long ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ItemID,ProjectID,ItemName,Time,Amount,Method,Paid,Payee,Remark from " + TABLE_SPECIAL_PROJECT_PRE_PAY + " ");
            strSql.Append(" where ProjectID=@ProjectID ");
            strSql.Append(" order by ItemID ASC;");//排序
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;

            List<SpecialProjectPrePayInfo> list = new List<SpecialProjectPrePayInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    SpecialProjectPrePayInfo item = GetDataSpecialProjectPrePayInfo(rd);
                    list.Add(item);
                }
            }
            return list;
        }
        #endregion

        #region 各表获取一条记录函数
        /// <summary>
        /// 获取一条专项工程记录
        /// </summary>
        private SpecialProjectInfo GetSpecialProjectInfo(long ProjectID)
        {
            string table = TABLE_SPECIAL_PROJECT + " s1 left join " + TABLE_COMPANY + " s4 on " +
                " s1.CompanyID = s4.CompanyID ";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select s1.*,s4.CompanyName from "+table+" ");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;

            SpecialProjectInfo item = new SpecialProjectInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    item = GetDataSpecialProjectInfo(rd);
                    break;
                }
            }
            //封装其他相关的信息
            //
            return item;
        }

        /// <summary>
        /// 获取一条专项工程审批记录
        /// </summary>
        private SpecialProjectApprovalInfo GetSpecialProjectApprovalInfo(long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ItemID,ProjectID,ApprovalName,Approvaler,Result,FeeBack,ApprovalDate from "+TABLE_SPECIAL_PROJECT_APPROVAL+" ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            SpecialProjectApprovalInfo item = new SpecialProjectApprovalInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    item = GetDataSpecialProjectApprovalInfo(rd);
                    break;
                }
            }
            //封装其他相关的信息
            //
            return item;
        }

        /// <summary>
        /// 获取一条专项工程投标记录
        /// </summary>
        private SpecialProjectBidInfo GetSpecialProjectBidInfo(long ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ProjectID,BiddenCompany,BiddenCompanyInfo,Attechment,Approvaler,Result,FeeBack,ApprovalDate from "+TABLE_SPECIAL_PROJECT_BID+" ");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;

            SpecialProjectBidInfo item = new SpecialProjectBidInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    item = GetDataSpecialProjectBidInfo(rd);
                    break;
                }
            }
            //封装其他相关的信息
            //
            return item;
        }

        /// <summary>
        /// 获取一条专项工程预算记录
        /// </summary>
        private SpecialProjectBudgetItemInfo GetSpecialProjectBudgetItemInfo(long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ItemID,ProjectID,BudgetItemName,Amultiple,Amount,Remark,IsRelated2Direct from " + TABLE_SPECIAL_PROJECT_BUDGET_ITEM + " ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            SpecialProjectBudgetItemInfo item = new SpecialProjectBudgetItemInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    item = GetDataSpecialProjectBudgetItemInfo(rd);
                    break;
                }
            }
            //封装其他相关的信息
            //
            return item;
        }

        /// <summary>
        /// 获取一条专项工程验收记录
        /// </summary>
        private SpecialProjectCheckAcceptInfo GetSpecialProjectCheckAcceptInfo(long ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ProjectID,Finish,FinishDate,FinishRemark,FinishApprovaler,Complete,CompleteDate,CompleteRemark,CompleteApprovaler,Pass,PassDate,PassRemark,PassApprovaler from "+TABLE_SPECIAL_PROJECT_CHECK_ACCEPT+" ");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;

            SpecialProjectCheckAcceptInfo item = new SpecialProjectCheckAcceptInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    item = GetDataSpecialProjectCheckAcceptInfo(rd);
                    break;
                }
            }
            //封装其他相关的信息
            //
            return item;
        }

        /// <summary>
        /// 获取一条专项工程进度检查记录
        /// </summary>
        private SpecialProjectCheckRecordInfo GetSpecialProjectCheckRecordInfo(long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ItemID,ProjectID,CheckTime,PlanItemID,Progress,Quality,HR,Checker,Remark from "+TABLE_SPECIAL_PROJECT_CHECK_RECORD+" ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            SpecialProjectCheckRecordInfo item = new SpecialProjectCheckRecordInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    item = GetDataSpecialProjectCheckRecordInfo(rd);
                    break;
                }
            }
            //封装其他相关的信息
            //
            return item;
        }

        /// <summary>
        /// 获取一条专项工程合同支付项记录
        /// </summary>
        private SpecialProjectContractPayInfo GetSpecialProjectContractPayInfo(long ItemID)
        {
            string table = TABLE_SPECIAL_PROJECT_CONTRACT_PAY + " AS a LEFT OUTER JOIN " +
                         TABLE_SPECIAL_PROJECT_PLAN + " AS b ON " +
                         " a.PlanItemID = b.ItemID ";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 a.ItemID,a.ProjectID,a.ItemName,a.PlanItemID,b.ItemName as PlanItemName,a.DaysAfter,a.Amount,a.Method,a.Paid,a.Payee,a.Remark from " + table + " ");
            strSql.Append(" where a.ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            SpecialProjectContractPayInfo item = new SpecialProjectContractPayInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    item = GetDataSpecialProjectContractPayInfo(rd);
                    break;
                }
            }
            //封装其他相关的信息
            //
            return item;
        }

        /// <summary>
        /// 获取一条专项工程设计方案记录
        /// </summary>
        private SpecialProjectDesignInfo GetSpecialProjectDesignInfo(long ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ProjectID,ApprovalDate,DesignName,Designer,DesignerInfo,Design,Attechment,Approvaler,Result,FeeBack,DesignCost,ProjectCost from "+TABLE_SPECIAL_PROJECT_DESIGN+" ");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;

            SpecialProjectDesignInfo item = new SpecialProjectDesignInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    item = GetDataSpecialProjectDesignInfo(rd);
                    break;
                }
            }
            //封装其他相关的信息
            //
            return item;
        }

        /// <summary>
        /// 获取一条专项工程进场设备信息记录
        /// </summary>
        private SpecialProjectDeviceInfo GetSpecialProjectDeviceInfo(long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ItemID,LastInCount,Time,ProjectID,DeviceName,Model,Size,Usage,Status,PlanCount,ActualCount from "+TABLE_SPECIAL_PROJECT_DEVICE+" ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            SpecialProjectDeviceInfo item = new SpecialProjectDeviceInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    item = GetDataSpecialProjectDeviceInfo(rd);
                    break;
                }
            }
            //封装其他相关的信息
            //
            return item;
        }

        /// <summary>
        /// 获取一条专项工程工作项记录
        /// </summary>
        private SpecialProjectJobItemInfo GetSpecialProjectJobItemInfo(long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ItemID,ProjectID,Equipment,Model,Count,Unit,UnitPrice,Remark from "+TABLE_SPECIAL_PROJECT_JOB_ITEM+" ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            SpecialProjectJobItemInfo item = new SpecialProjectJobItemInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    item = GetDataSpecialProjectJobItemInfo(rd);
                    break;
                }
            }
            //封装其他相关的信息
            //
            return item;
        }

        /// <summary>
        /// 获取一条专项工程变更详情设备记录
        /// </summary>
        private SpecialProjectModifyDeviceInfo GetSpecialProjectModifyDeviceInfo(long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ItemID,Amount,Remark,ProjectID,ModifyApplyID,IsAdd,DeviceName,Model,Count,UnitPrice,Unit from "+TABLE_SPECIAL_PROJECT_MODIFY_DEVICE_LIST+" ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            SpecialProjectModifyDeviceInfo item = new SpecialProjectModifyDeviceInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    item = GetDataSpecialProjectModifyDeviceInfo(rd);
                    break;
                }
            }
            //封装其他相关的信息
            //
            return item;
        }

        /// <summary>
        /// 获取一条专项工程变更记录
        /// </summary>
        private SpecialProjectModifyInfo GetSpecialProjectModifyInfo(long ModifyID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ModifyID,OwnerApprovaler,OwnerResult,OwnerFeeBack,OwnerApprovalDate,ContractApprovaler,ContractResult,ContractFeeBack,ContractApprovalDate,LeaderApprovaler,LeaderResult,ProjectID,LeaderFeeBack,LeaderApprovalDate,ApplyTime,BudgetChange,BudgetIncDesc,DelayDays,ChangeContent,ContentAttechment,Remark,Status from "+TABLE_SPECIAL_PROJECT_MODIFY+" ");
            strSql.Append(" where ModifyID=@ModifyID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ModifyID", SqlDbType.BigInt)};
            parameters[0].Value = ModifyID;

            SpecialProjectModifyInfo item = new SpecialProjectModifyInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    item = GetDataSpecialProjectModifyInfo(rd);
                    break;
                }
            }
            //封装其他相关的信息
            //
            return item;
        }

        /// <summary>
        /// 获取一条专项工程进度支付项记录
        /// </summary>
        private SpecialProjectPayRecordInfo GetSpecialProjectPayRecordInfo(long ProjectID, int Year, int Month)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ProjectID,Remark,Year,Month,PayTime,Progress,Amount,Method,Paid,Payee from "+TABLE_SPECIAL_PROJECT_PAY_RECORD+" ");
            strSql.Append(" where ProjectID=@ProjectID and Year=@Year and Month=@Month ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@Month", SqlDbType.Int,4)};
            parameters[0].Value = ProjectID;
            parameters[1].Value = Year;
            parameters[2].Value = Month;

            SpecialProjectPayRecordInfo item = new SpecialProjectPayRecordInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    item = GetDataSpecialProjectPayRecordInfo(rd);
                    break;
                }
            }
            //封装其他相关的信息
            //
            return item;
        }

        /// <summary>
        /// 获取一条专项工程工作计划项记录
        /// </summary>
        private SpecialProjectPlanInfo GetSpecialProjectPlanInfo(long ItemID)
        {
            string table = TABLE_SPECIAL_PROJECT_PLAN + " AS a LEFT OUTER JOIN " +
                           TABLE_SPECIAL_PROJECT_PLAN + " AS b ON " +
                           " a.PrefixItemID = b.ItemID ";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 a.ItemID,a.Progress,a.Status,a.ProjectID,a.ItemName,a.PrefixItemID,b.ItemName AS PrefixItemName, a.StartTime,a.EndTime,a.Days,a.HRPlan,a.DevicePlan,a.DaysAfter from "
                + table + " ");
            strSql.Append(" where a.ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            SpecialProjectPlanInfo item = new SpecialProjectPlanInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    item = GetDataSpecialProjectPlanInfo(rd);
                    break;
                }
            }
            
            return item;
        }

        /// <summary>
        /// 获取一条专项工程预支付项记录
        /// </summary>
        private SpecialProjectPrePayInfo GetSpecialProjectPrePayInfo(long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ItemID,ProjectID,ItemName,Time,Amount,Method,Paid,Payee,Remark from "+TABLE_SPECIAL_PROJECT_PRE_PAY+" ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            SpecialProjectPrePayInfo item = new SpecialProjectPrePayInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    item = GetDataSpecialProjectPrePayInfo(rd);
                    break;
                }
            }
            //封装其他相关的信息
            //
            return item;
        }


        /// <summary>
        /// 获取一条专项工程设备进场记录
        /// </summary>
        private SpecialProjectDeviceInRecord GetSpecialProjectDeviceInRecord(long RecordID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 RecordID,ItemID,ProjectID,Count,Time from " + TABLE_SPECIAL_PROJECT_DEVICE_IN_RECORD + " ");
            strSql.Append(" where RecordID=@RecordID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.BigInt)};
            parameters[0].Value = RecordID;

            SpecialProjectDeviceInRecord item = new SpecialProjectDeviceInRecord();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    item = GetDataSpecialProjectDeviceInRecord(rd);
                    break;
                }
            }
            //封装其他相关的信息
            //
            return item;
        }

        #endregion

        #region 各表更新函数
        /// <summary>
        /// 更新专项工程
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ProjectID">主键</param>
        private void UpdateSpecialProjectInfo(SqlTransaction trans, SpecialProjectInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_SpecialProject set ");
            strSql.Append("Solution=@Solution,");
            strSql.Append("SolutionFile=@SolutionFile,");
            strSql.Append("Status=@Status,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("ProjectName=@ProjectName,");
            strSql.Append("BudgetName=@BudgetName,");
            strSql.Append("Budget=@Budget,");
            strSql.Append("CurrentStatus=@CurrentStatus,");
            strSql.Append("CurrentStatusFile=@CurrentStatusFile,");
            strSql.Append("Problem=@Problem,");
            strSql.Append("ProblemFile=@ProblemFile,");
            strSql.Append("Submitter=@Submitter,");
            strSql.Append("SubmitTime=@SubmitTime,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@Solution", SqlDbType.NVarChar,2000),
					new SqlParameter("@SolutionFile", SqlDbType.VarChar,80),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@ProjectName", SqlDbType.NVarChar,50),
					new SqlParameter("@BudgetName", SqlDbType.NVarChar,50),
					new SqlParameter("@Budget", SqlDbType.Decimal,9),
					new SqlParameter("@CurrentStatus", SqlDbType.NVarChar,1000),
					new SqlParameter("@CurrentStatusFile", SqlDbType.VarChar,80),
					new SqlParameter("@Problem", SqlDbType.NVarChar,2000),
					new SqlParameter("@ProblemFile", SqlDbType.VarChar,80),
                    new SqlParameter("@Submitter",SqlDbType.VarChar,20),
                    new SqlParameter("@SubmitTime",SqlDbType.DateTime),
                    new SqlParameter("@UpdateTime",SqlDbType.DateTime)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.Solution;
            parameters[2].Value = model.SolutionFile;
            parameters[3].Value = (int)model.Status;
            parameters[4].Value = model.CompanyID;
            parameters[5].Value = model.ProjectName;
            parameters[6].Value = model.BudgetName;
            parameters[7].Value = model.Budget;
            parameters[8].Value = model.CurrentStatus;
            parameters[9].Value = model.CurrentStatusFile;
            parameters[10].Value = model.Problem;
            parameters[11].Value = model.ProblemFile;
            parameters[12].Value = model.Submitter;
            parameters[13].Value = model.SubmitTime == DateTime.MinValue ? SqlDateTime.Null : model.SubmitTime;
            parameters[14].Value = model.UpdateTime == DateTime.MinValue ? SqlDateTime.Null : model.UpdateTime;
            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新专项工程审批记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void UpdateSpecialProjectApprovalInfo(SqlTransaction trans, SpecialProjectApprovalInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_SpecialProjectApproval set ");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("ApprovalName=@ApprovalName,");
            strSql.Append("Approvaler=@Approvaler,");
            strSql.Append("Result=@Result,");
            strSql.Append("FeeBack=@FeeBack,");
            strSql.Append("ApprovalFile=@ApprovalFile,");
            strSql.Append("ApprovalDate=@ApprovalDate");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@ApprovalName", SqlDbType.NVarChar,20),
					new SqlParameter("@Approvaler", SqlDbType.VarChar,20),
					new SqlParameter("@Result", SqlDbType.TinyInt,1),
					new SqlParameter("@FeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@ApprovalDate", SqlDbType.DateTime)};
            parameters[0].Value = model.ItemID;
            parameters[1].Value = model.ProjectID;
            parameters[2].Value = model.ApprovalName;
            parameters[3].Value = model.Approvaler;
            parameters[4].Value = model.Result;
            parameters[5].Value = model.FeeBack;
            parameters[6].Value = model.ApprovalFile;
            parameters[7].Value = model.ApprovalDate;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新专项工程招标记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ProjectID">主键</param>
        private void UpdateSpecialProjectBidInfo(SqlTransaction trans, SpecialProjectBidInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_SpecialProjectBid set ");
            strSql.Append("BiddenCompany=@BiddenCompany,");
            strSql.Append("BiddenCompanyInfo=@BiddenCompanyInfo,");
            strSql.Append("Attechment=@Attechment,");
            strSql.Append("Approvaler=@Approvaler,");
            strSql.Append("Result=@Result,");
            strSql.Append("FeeBack=@FeeBack,");
            strSql.Append("ApprovalDate=@ApprovalDate");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@BiddenCompany", SqlDbType.NVarChar,20),
					new SqlParameter("@BiddenCompanyInfo", SqlDbType.NVarChar,1000),
					new SqlParameter("@Attechment", SqlDbType.VarChar,80),
					new SqlParameter("@Approvaler", SqlDbType.VarChar,20),
					new SqlParameter("@Result", SqlDbType.TinyInt,1),
					new SqlParameter("@FeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@ApprovalDate", SqlDbType.DateTime)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.BiddenCompany;
            parameters[2].Value = model.BiddenCompanyInfo;
            parameters[3].Value = model.Attechment;
            parameters[4].Value = model.Approvaler;
            parameters[5].Value = model.Result;
            parameters[6].Value = model.FeeBack;
            parameters[7].Value = model.ApprovalDate == DateTime.MinValue ? SqlDateTime.Null : model.ApprovalDate;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新专项工程预算项记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void UpdateSpecialProjectBudgetItemInfo(SqlTransaction trans, SpecialProjectBudgetItemInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_SpecialProjectBudgetItem set ");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("BudgetItemName=@BudgetItemName,");
            strSql.Append("Amultiple=@Amultiple,");
            strSql.Append("Amount=@Amount,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("IsRelated2Direct=@IsRelated2Direct");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@BudgetItemName", SqlDbType.NVarChar,40),
					new SqlParameter("@Amultiple", SqlDbType.Decimal,9),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
                    new SqlParameter("@IsRelated2Direct",SqlDbType.Bit)};
            parameters[0].Value = model.ItemID;
            parameters[1].Value = model.ProjectID;
            parameters[2].Value = model.BudgetItemName;
            parameters[3].Value = model.Amultiple;
            parameters[4].Value = model.Amount;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.IsRelated2Direct;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新专项工程验收信息记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ProjectID">主键</param>
        private void UpdateSpecialProjectCheckAcceptInfo(SqlTransaction trans, SpecialProjectCheckAcceptInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_SpecialProjectCheckAccept set ");
            strSql.Append("Finish=@Finish,");
            strSql.Append("FinishDate=@FinishDate,");
            strSql.Append("FinishRemark=@FinishRemark,");
            strSql.Append("FinishApprovaler=@FinishApprovaler,");
            strSql.Append("Complete=@Complete,");
            strSql.Append("CompleteDate=@CompleteDate,");
            strSql.Append("CompleteRemark=@CompleteRemark,");
            strSql.Append("CompleteApprovaler=@CompleteApprovaler,");
            strSql.Append("Pass=@Pass,");
            strSql.Append("PassDate=@PassDate,");
            strSql.Append("PassRemark=@PassRemark,");
            strSql.Append("PassApprovaler=@PassApprovaler");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@Finish", SqlDbType.Bit,1),
					new SqlParameter("@FinishDate", SqlDbType.DateTime),
					new SqlParameter("@FinishRemark", SqlDbType.NVarChar,100),
					new SqlParameter("@FinishApprovaler", SqlDbType.VarChar,20),
					new SqlParameter("@Complete", SqlDbType.Bit,1),
					new SqlParameter("@CompleteDate", SqlDbType.DateTime),
					new SqlParameter("@CompleteRemark", SqlDbType.NVarChar,100),
					new SqlParameter("@CompleteApprovaler", SqlDbType.VarChar,20),
					new SqlParameter("@Pass", SqlDbType.Bit,1),
					new SqlParameter("@PassDate", SqlDbType.DateTime),
					new SqlParameter("@PassRemark", SqlDbType.NVarChar,100),
					new SqlParameter("@PassApprovaler", SqlDbType.VarChar,20)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.Finish;
            parameters[2].Value = model.FinishDate == DateTime.MinValue ? SqlDateTime.Null : model.FinishDate;
            parameters[3].Value = model.FinishRemark;
            parameters[4].Value = model.FinishApprovaler;
            parameters[5].Value = model.Complete;
            parameters[6].Value = model.CompleteDate == DateTime.MinValue ? SqlDateTime.Null : model.CompleteDate;
            parameters[7].Value = model.CompleteRemark;
            parameters[8].Value = model.CompleteApprovaler;
            parameters[9].Value = model.Pass;
            parameters[10].Value = model.PassDate == DateTime.MinValue ? SqlDateTime.Null : model.PassDate;
            parameters[11].Value = model.PassRemark;
            parameters[12].Value = model.PassApprovaler;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新专项工程进度检查记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void UpdateSpecialProjectCheckRecordInfo(SqlTransaction trans, SpecialProjectCheckRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_SpecialProjectCheckRecord set ");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("CheckTime=@CheckTime,");
            strSql.Append("PlanItemID=@PlanItemID,");
            strSql.Append("Progress=@Progress,");
            strSql.Append("Quality=@Quality,");
            strSql.Append("HR=@HR,");
            strSql.Append("Checker=@Checker,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@CheckTime", SqlDbType.DateTime),
					new SqlParameter("@PlanItemID", SqlDbType.BigInt,8),
					new SqlParameter("@Progress", SqlDbType.Decimal,5),
					new SqlParameter("@Quality", SqlDbType.NVarChar,50),
					new SqlParameter("@HR", SqlDbType.NVarChar,50),
					new SqlParameter("@Checker", SqlDbType.VarChar,20),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.ItemID;
            parameters[1].Value = model.ProjectID;
            parameters[2].Value = model.CheckTime == DateTime.MinValue ? SqlDateTime.Null : model.CheckTime;
            parameters[3].Value = model.PlanItemID;
            parameters[4].Value = model.Progress;
            parameters[5].Value = model.Quality;
            parameters[6].Value = model.HR;
            parameters[7].Value = model.Checker;
            parameters[8].Value = model.Remark;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新专项工程合同支付记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void UpdateSpecialProjectContractPayInfo(SqlTransaction trans, SpecialProjectContractPayInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_SpecialProjectContractPay set ");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("ItemName=@ItemName,");
            strSql.Append("PlanItemID=@PlanItemID,");
            strSql.Append("DaysAfter=@DaysAfter,");
            strSql.Append("Amount=@Amount,");
            strSql.Append("Method=@Method,");
            strSql.Append("Paid=@Paid,");
            strSql.Append("Payee=@Payee,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
                    new SqlParameter("@ItemName",SqlDbType.NVarChar,20),
					new SqlParameter("@PlanItemID", SqlDbType.BigInt,8),
					new SqlParameter("@DaysAfter", SqlDbType.Int,4),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
					new SqlParameter("@Method", SqlDbType.NVarChar,20),
					new SqlParameter("@Paid", SqlDbType.Decimal,9),
					new SqlParameter("@Payee", SqlDbType.VarChar,20),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.ItemID;
            parameters[1].Value = model.ProjectID;
            parameters[2].Value = model.ItemName;
            parameters[3].Value = model.PlanItemID;
            parameters[4].Value = model.DaysAfter;
            parameters[5].Value = model.Amount;
            parameters[6].Value = model.Method;
            parameters[7].Value = model.Paid;
            parameters[8].Value = model.Payee;
            parameters[9].Value = model.Remark;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新专项工程设计方案记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ProjectID">主键</param>
        private void UpdateSpecialProjectDesignInfo(SqlTransaction trans, SpecialProjectDesignInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_SpecialProjectDesign set ");
            strSql.Append("ApprovalDate=@ApprovalDate,");
            strSql.Append("DesignName=@DesignName,");
            strSql.Append("Designer=@Designer,");
            strSql.Append("DesignerInfo=@DesignerInfo,");
            strSql.Append("Design=@Design,");
            strSql.Append("Attechment=@Attechment,");
            strSql.Append("Approvaler=@Approvaler,");
            strSql.Append("Result=@Result,");
            strSql.Append("FeeBack=@FeeBack,");
            strSql.Append("DesignCost=@DesignCost,");
            strSql.Append("ProjectCost=@ProjectCost");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@ApprovalDate", SqlDbType.DateTime),
					new SqlParameter("@DesignName", SqlDbType.NVarChar,20),
					new SqlParameter("@Designer", SqlDbType.NVarChar,20),
					new SqlParameter("@DesignerInfo", SqlDbType.NVarChar,100),
					new SqlParameter("@Design", SqlDbType.NVarChar,2000),
					new SqlParameter("@Attechment", SqlDbType.VarChar,80),
					new SqlParameter("@Approvaler", SqlDbType.VarChar,20),
					new SqlParameter("@Result", SqlDbType.TinyInt,1),
					new SqlParameter("@FeeBack", SqlDbType.NVarChar,50),
                    new SqlParameter("@DesignCost",SqlDbType.Decimal),
                    new SqlParameter("@ProjectCost",SqlDbType.Decimal)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.ApprovalDate == DateTime.MinValue ? SqlDateTime.Null : model.ApprovalDate;
            parameters[2].Value = model.DesignName;
            parameters[3].Value = model.Designer;
            parameters[4].Value = model.DesignerInfo;
            parameters[5].Value = model.Design;
            parameters[6].Value = model.Attechment;
            parameters[7].Value = model.Approvaler;
            parameters[8].Value = model.Result;
            parameters[9].Value = model.FeeBack;
            parameters[10].Value = model.DesignCost;
            parameters[11].Value = model.ProjectCost;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新专项工程进场设备记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void UpdateSpecialProjectDeviceInfo(SqlTransaction trans, SpecialProjectDeviceInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_SpecialProjectDevice set ");
            strSql.Append("LastInCount=@LastInCount,");
            strSql.Append("Time=@Time,");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("DeviceName=@DeviceName,");
            strSql.Append("Model=@Model,");
            strSql.Append("Size=@Size,");
            strSql.Append("Usage=@Usage,");
            strSql.Append("Status=@Status,");
            strSql.Append("PlanCount=@PlanCount,");
            strSql.Append("ActualCount=@ActualCount");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@LastInCount", SqlDbType.Decimal,9),
					new SqlParameter("@Time", SqlDbType.DateTime),
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@DeviceName", SqlDbType.NVarChar,20),
					new SqlParameter("@Model", SqlDbType.NVarChar,20),
					new SqlParameter("@Size", SqlDbType.NVarChar,20),
					new SqlParameter("@Usage", SqlDbType.NVarChar,100),
					new SqlParameter("@Status", SqlDbType.NVarChar,50),
					new SqlParameter("@PlanCount", SqlDbType.Decimal,9),
					new SqlParameter("@ActualCount", SqlDbType.Decimal,9)};
            parameters[0].Value = model.ItemID;
            parameters[1].Value = model.LastInCount;
            parameters[2].Value = model.Time == DateTime.MinValue ? SqlDateTime.Null : model.Time;
            parameters[3].Value = model.ProjectID;
            parameters[4].Value = model.DeviceName;
            parameters[5].Value = model.Model;
            parameters[6].Value = model.Size;
            parameters[7].Value = model.Usage;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.PlanCount;
            parameters[10].Value = model.ActualCount;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 更新专项工程进场设备记录，添加入场设备数量
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private decimal UpdateSpecialProjectDeviceInfo(SqlTransaction trans,long itemid,decimal increaseamount, DateTime time)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_SpecialProjectDevice set ");
            strSql.Append("LastInCount=@LastInCount,");
            strSql.Append("Time=@Time,");
            strSql.Append("ActualCount=ActualCount + @LastInCount");
            strSql.Append(" where ItemID=@ItemID;");
            strSql.Append(" select cast(ActualCount as decimal(15,5)) from " + TABLE_SPECIAL_PROJECT_DEVICE + " ");
            strSql.Append(" where ItemID=@ItemID;");

            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@LastInCount", SqlDbType.Decimal,9),
					new SqlParameter("@Time", SqlDbType.DateTime)};
            parameters[0].Value = itemid;
            parameters[1].Value = increaseamount;
            parameters[2].Value = time == DateTime.MinValue ? SqlDateTime.Null : time;

            //读取ID
            decimal amount = 0;
            if (trans != null)
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        amount = rdr.GetDecimal(0);
                    }
                }
            }
            else
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        amount = rdr.GetDecimal(0);
                    }
                }
            }
            return amount;
        }

        /// <summary>
        /// 更新专项工程工作量记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void UpdateSpecialProjectJobItemInfo(SqlTransaction trans, SpecialProjectJobItemInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_SpecialProjectJobItem set ");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("Equipment=@Equipment,");
            strSql.Append("Model=@Model,");
            strSql.Append("Count=@Count,");
            strSql.Append("Unit=@Unit,");
            strSql.Append("UnitPrice=@UnitPrice,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@Equipment", SqlDbType.NVarChar,40),
					new SqlParameter("@Model", SqlDbType.NVarChar,40),
					new SqlParameter("@Count", SqlDbType.Decimal,9),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@UnitPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.ItemID;
            parameters[1].Value = model.ProjectID;
            parameters[2].Value = model.Equipment;
            parameters[3].Value = model.Model;
            parameters[4].Value = model.Count;
            parameters[5].Value = model.Unit;
            parameters[6].Value = model.UnitPrice;
            parameters[7].Value = model.Remark;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新专项工程变更详情记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void UpdateSpecialProjectModifyDeviceInfo(SqlTransaction trans, SpecialProjectModifyDeviceInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_SpecialProjectModifyDeviceList set ");
            strSql.Append("Amount=@Amount,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("ModifyApplyID=@ModifyApplyID,");
            strSql.Append("IsAdd=@IsAdd,");
            strSql.Append("DeviceName=@DeviceName,");
            strSql.Append("Model=@Model,");
            strSql.Append("Count=@Count,");
            strSql.Append("UnitPrice=@UnitPrice,");
            strSql.Append("Unit=@Unit");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@ModifyApplyID", SqlDbType.BigInt,8),
					new SqlParameter("@IsAdd", SqlDbType.Bit,1),
					new SqlParameter("@DeviceName", SqlDbType.NVarChar,20),
					new SqlParameter("@Model", SqlDbType.NVarChar,20),
					new SqlParameter("@Count", SqlDbType.Decimal,9),
					new SqlParameter("@UnitPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5)};
            parameters[0].Value = model.ItemID;
            parameters[1].Value = model.Amount;
            parameters[2].Value = model.Remark;
            parameters[3].Value = model.ProjectID;
            parameters[4].Value = model.ModifyApplyID;
            parameters[5].Value = model.IsAdd;
            parameters[6].Value = model.DeviceName;
            parameters[7].Value = model.Model;
            parameters[8].Value = model.Count;
            parameters[9].Value = model.UnitPrice;
            parameters[10].Value = model.Unit;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新专项工程变更记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ModifyID">主键</param>
        private void UpdateSpecialProjectModifyInfo(SqlTransaction trans, SpecialProjectModifyInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_SpecialProjectModify set ");
            strSql.Append("OwnerApprovaler=@OwnerApprovaler,");
            strSql.Append("OwnerResult=@OwnerResult,");
            strSql.Append("OwnerFeeBack=@OwnerFeeBack,");
            strSql.Append("OwnerApprovalDate=@OwnerApprovalDate,");
            strSql.Append("ContractApprovaler=@ContractApprovaler,");
            strSql.Append("ContractResult=@ContractResult,");
            strSql.Append("ContractFeeBack=@ContractFeeBack,");
            strSql.Append("ContractApprovalDate=@ContractApprovalDate,");
            strSql.Append("LeaderApprovaler=@LeaderApprovaler,");
            strSql.Append("LeaderResult=@LeaderResult,");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("LeaderFeeBack=@LeaderFeeBack,");
            strSql.Append("LeaderApprovalDate=@LeaderApprovalDate,");
            strSql.Append("ApplyTime=@ApplyTime,");
            strSql.Append("BudgetChange=@BudgetChange,");
            strSql.Append("BudgetIncDesc=@BudgetIncDesc,");
            strSql.Append("DelayDays=@DelayDays,");
            strSql.Append("ChangeContent=@ChangeContent,");
            strSql.Append("ContentAttechment=@ContentAttechment,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Status=@Status");
            strSql.Append(" where ModifyID=@ModifyID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ModifyID", SqlDbType.BigInt,8),
					new SqlParameter("@OwnerApprovaler", SqlDbType.VarChar,20),
					new SqlParameter("@OwnerResult", SqlDbType.TinyInt,1),
					new SqlParameter("@OwnerFeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@OwnerApprovalDate", SqlDbType.DateTime),
					new SqlParameter("@ContractApprovaler", SqlDbType.VarChar,20),
					new SqlParameter("@ContractResult", SqlDbType.TinyInt,1),
					new SqlParameter("@ContractFeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@ContractApprovalDate", SqlDbType.DateTime),
					new SqlParameter("@LeaderApprovaler", SqlDbType.VarChar,20),
					new SqlParameter("@LeaderResult", SqlDbType.TinyInt,1),
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@LeaderFeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@LeaderApprovalDate", SqlDbType.DateTime),
					new SqlParameter("@ApplyTime", SqlDbType.DateTime),
					new SqlParameter("@BudgetChange", SqlDbType.Decimal,9),
					new SqlParameter("@BudgetIncDesc", SqlDbType.Decimal,9),
					new SqlParameter("@DelayDays", SqlDbType.Int,4),
					new SqlParameter("@ChangeContent", SqlDbType.NVarChar,1000),
					new SqlParameter("@ContentAttechment", SqlDbType.VarChar,80),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
                    new SqlParameter("@Status",SqlDbType.TinyInt)};
            parameters[0].Value = model.ModifyID;
            parameters[1].Value = model.OwnerApprovaler;
            parameters[2].Value = model.OwnerResult;
            parameters[3].Value = model.OwnerFeeBack;
            parameters[4].Value = model.OwnerApprovalDate == DateTime.MinValue ? SqlDateTime.Null : model.OwnerApprovalDate;
            parameters[5].Value = model.ContractApprovaler;
            parameters[6].Value = model.ContractResult;
            parameters[7].Value = model.ContractFeeBack;
            parameters[8].Value = model.ContractApprovalDate == DateTime.MinValue ? SqlDateTime.Null : model.ContractApprovalDate;
            parameters[9].Value = model.LeaderApprovaler;
            parameters[10].Value = model.LeaderResult;
            parameters[11].Value = model.ProjectID;
            parameters[12].Value = model.LeaderFeeBack;
            parameters[13].Value = model.LeaderApprovalDate == DateTime.MinValue ? SqlDateTime.Null : model.LeaderApprovalDate;
            parameters[14].Value = model.ApplyTime == DateTime.MinValue ? SqlDateTime.Null : model.ApplyTime;
            parameters[15].Value = model.BudgetChange;
            parameters[16].Value = model.BudgetIncDesc;
            parameters[17].Value = model.DelayDays;
            parameters[18].Value = model.ChangeContent;
            parameters[19].Value = model.ContentAttechment;
            parameters[20].Value = model.Remark;
            parameters[21].Value = model.Status;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新专项工程进度支付记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ProjectID">主键</param>
        /// <param name="Month">主键</param>
        /// <param name="Year">主键</param>
        private void UpdateSpecialProjectPayRecordInfo(SqlTransaction trans, SpecialProjectPayRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_SpecialProjectPayRecord set ");
            strSql.Append("Remark=@Remark,");
            strSql.Append("PayTime=@PayTime,");
            strSql.Append("Progress=@Progress,");
            strSql.Append("Amount=@Amount,");
            strSql.Append("Method=@Method,");
            strSql.Append("Paid=@Paid,");
            strSql.Append("Payee=@Payee");
            strSql.Append(" where ProjectID=@ProjectID and Year=@Year and Month=@Month ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@Month", SqlDbType.Int,4),
					new SqlParameter("@PayTime", SqlDbType.DateTime),
					new SqlParameter("@Progress", SqlDbType.Decimal,5),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
					new SqlParameter("@Method", SqlDbType.NVarChar,20),
					new SqlParameter("@Paid", SqlDbType.Decimal,9),
					new SqlParameter("@Payee", SqlDbType.VarChar,20)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.Year;
            parameters[3].Value = model.Month;
            parameters[4].Value = model.PayTime;
            parameters[5].Value = model.Progress;
            parameters[6].Value = model.Amount;
            parameters[7].Value = model.Method;
            parameters[8].Value = model.Paid;
            parameters[9].Value = model.Payee;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新专项工程工作安排记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void UpdateSpecialProjectPlanInfo(SqlTransaction trans, SpecialProjectPlanInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_SpecialProjectPlan set ");
            strSql.Append("Progress=@Progress,");
            strSql.Append("Status=@Status,");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("ItemName=@ItemName,");
            strSql.Append("PrefixItemID=@PrefixItemID,");
            strSql.Append("StartTime=@StartTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("Days=@Days,");
            strSql.Append("HRPlan=@HRPlan,");
            strSql.Append("DevicePlan=@DevicePlan,");
            strSql.Append("DaysAfter=@DaysAfter");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@Progress", SqlDbType.Decimal,5),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@ItemName", SqlDbType.NVarChar,20),
					new SqlParameter("@PrefixItemID", SqlDbType.BigInt,8),
					new SqlParameter("@StartTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@Days", SqlDbType.Int,4),
					new SqlParameter("@HRPlan", SqlDbType.NVarChar,100),
					new SqlParameter("@DevicePlan", SqlDbType.NVarChar,100),
                    new SqlParameter("@DaysAfter",SqlDbType.Int)};
            parameters[0].Value = model.ItemID;
            parameters[1].Value = model.Progress;
            parameters[2].Value = model.Status;
            parameters[3].Value = model.ProjectID;
            parameters[4].Value = model.ItemName;
            parameters[5].Value = model.PrefixItemID;
            parameters[6].Value = model.StartTime;
            parameters[7].Value = model.EndTime;
            parameters[8].Value = model.Days;
            parameters[9].Value = model.HRPlan;
            parameters[10].Value = model.DevicePlan;
            parameters[11].Value = model.DaysAfter;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新专项工程预支付项记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void UpdateSpecialProjectPrePayInfo(SqlTransaction trans, SpecialProjectPrePayInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_SpecialProjectPrePay set ");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("ItemName=@ItemName,");
            strSql.Append("Time=@Time,");
            strSql.Append("Amount=@Amount,");
            strSql.Append("Method=@Method,");
            strSql.Append("Paid=@Paid,");
            strSql.Append("Payee=@Payee,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@ItemName", SqlDbType.NVarChar,20),
					new SqlParameter("@Time", SqlDbType.DateTime),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
					new SqlParameter("@Method", SqlDbType.NVarChar,20),
					new SqlParameter("@Paid", SqlDbType.Decimal,9),
					new SqlParameter("@Payee", SqlDbType.VarChar,20),
					new SqlParameter("@Remark", SqlDbType.VarChar,100)};
            parameters[0].Value = model.ItemID;
            parameters[1].Value = model.ProjectID;
            parameters[2].Value = model.ItemName;
            parameters[3].Value = model.Time;
            parameters[4].Value = model.Amount;
            parameters[5].Value = model.Method;
            parameters[6].Value = model.Paid;
            parameters[7].Value = model.Payee;
            parameters[8].Value = model.Remark;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 更新专项工程设备进仓记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void UpdateSpecialProjectDeviceInRecord(SqlTransaction trans, SpecialProjectDeviceInRecord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update "+TABLE_SPECIAL_PROJECT_DEVICE_IN_RECORD+" set ");
            strSql.Append("ItemID=@ItemID,");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("Count=@Count,");
            strSql.Append("Time=@Time");
            strSql.Append(" where RecordID=@RecordID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.BigInt,8),
					new SqlParameter("@ItemID", SqlDbType.BigInt,8),
					new SqlParameter("@ProjectID", SqlDbType.BigInt,8),
					new SqlParameter("@Count", SqlDbType.Decimal,9),
					new SqlParameter("@Time", SqlDbType.DateTime)};
            parameters[0].Value = model.RecordID;
            parameters[1].Value = model.ItemID;
            parameters[2].Value = model.ProjectID;
            parameters[3].Value = model.Count;
            parameters[4].Value = model.Time == DateTime.MinValue ? SqlDateTime.Null : model.Time;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        #endregion

        #region 各表删除函数
        /// <summary>
        /// 删除专项工程
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ProjectID">主键</param>
        private void DeleteSpecialProjectInfo(SqlTransaction trans,long ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_SpecialProject ");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;
            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除专项工程审批记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void DeleteSpecialProjectApprovalInfo(SqlTransaction trans, long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_SpecialProjectApproval ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除专项工程招标记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ProjectID">主键</param>
        private void DeleteSpecialProjectBidInfo(SqlTransaction trans, long ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_SpecialProjectBid ");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除专项工程预算项记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void DeleteSpecialProjectBudgetItemInfo(SqlTransaction trans, long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_SpecialProjectBudgetItem ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除专项工程验收信息记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ProjectID">主键</param>
        private void DeleteSpecialProjectCheckAcceptInfo(SqlTransaction trans, long ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_SpecialProjectCheckAccept ");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除专项工程进度检查记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void DeleteSpecialProjectCheckRecordInfo(SqlTransaction trans, long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_SpecialProjectCheckRecord ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除专项工程合同支付记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void DeleteSpecialProjectContractPayInfo(SqlTransaction trans, long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_SpecialProjectContractPay ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除专项工程设计方案记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ProjectID">主键</param>
        private void DeleteSpecialProjectDesignInfo(SqlTransaction trans, long ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_SpecialProjectDesign ");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = ProjectID;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除专项工程进场设备记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void DeleteSpecialProjectDeviceInfo(SqlTransaction trans, long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_SpecialProjectDevice ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除专项工程工作量记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void DeleteSpecialProjectJobItemInfo(SqlTransaction trans, long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_SpecialProjectJobItem ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除专项工程变更详情记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void DeleteSpecialProjectModifyDeviceInfo(SqlTransaction trans, long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_SpecialProjectModifyDeviceList ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除专项工程变更记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ModifyID">主键</param>
        private void DeleteSpecialProjectModifyInfo(SqlTransaction trans, long ModifyID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_SpecialProjectModify ");
            strSql.Append(" where ModifyID=@ModifyID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ModifyID", SqlDbType.BigInt)};
            parameters[0].Value = ModifyID;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除专项工程进度支付记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ProjectID">主键</param>
        /// <param name="Month">主键</param>
        /// <param name="Year">主键</param>
        private void DeleteSpecialProjectPayRecordInfo(SqlTransaction trans, long ProjectID,int Year,int Month)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_SpecialProjectPayRecord ");
            strSql.Append(" where ProjectID=@ProjectID and Year=@Year and Month=@Month ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@Month", SqlDbType.Int,4)};
            parameters[0].Value = ProjectID;
            parameters[1].Value = Year;
            parameters[2].Value = Month;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除专项工程工作安排记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void DeleteSpecialProjectPlanInfo(SqlTransaction trans, long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_SpecialProjectPlan ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除专项工程预支付项记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void DeleteSpecialProjectPrePayInfo(SqlTransaction trans, long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_SpecialProjectPrePay ");
            strSql.Append(" where ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除专项工程设备进场记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void DeleteSpecialProjectDeviceInRecord(SqlTransaction trans, long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete "+ TABLE_SPECIAL_PROJECT_DEVICE_IN_RECORD+" ");
            strSql.Append(" where RecordID=@RecordID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        #endregion


        #region ISpecialProject 成员
        /// <summary>
        /// 插入专项工程基本信息
        /// </summary>
        /// <param name="project"></param>
        long ISpecialProject.InsertSpecialProjectInfo(SpecialProjectInfo project)
        {
            return InsertSpecialProjectInfo(null,project);
        }

        /// <summary>
        /// 删除专项工程
        /// </summary>
        /// <param name="id"></param>
        void ISpecialProject.DeleteSpecialProjectInfo(long id)
        {
            DeleteSpecialProjectInfo(null, id);
        }
        /// <summary>
        /// 更新专项工程基本信息
        /// </summary>
        /// <param name="project"></param>
        void ISpecialProject.UpdateSpecialProjectInfo(SpecialProjectInfo project)
        {
            UpdateSpecialProjectInfo(null, project);
        }
        /// <summary>
        /// 获取专项工程基本信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SpecialProjectInfo ISpecialProject.GetSpecialProjectInfo(long id)
        {
            return GetSpecialProjectInfo(id);
        }

        /// <summary>
        /// 获取专项工程工作量列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList ISpecialProject.GetSpecialProjectJobList(long id)
        {
            return GetSpecialProjectJobItemInfoList(id);
        }

        /// <summary>
        /// 获取一个公司的专项工程
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        IList ISpecialProject.GetSpecialProjectByCompany(int pageIndex, int pageSize, out int recordCount, string companyid, bool isFinish)
        {
            try
            {
                string table = TABLE_SPECIAL_PROJECT + " s1 left join " + TABLE_COMPANY + " s4 on " +
                " s1.CompanyID = s4.CompanyID ";
                QueryParam qp = new QueryParam();
                qp.TableName = table;
                qp.ReturnFields = string.Format(
                    "s1.*,s4.CompanyName"
                    + ",{0} as SubmitTimeSort", "s1.SubmitTime");
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " +  "s1.[CompanyID]='" + companyid + "'";
                if(isFinish)
                    qp.Where += " and " + "s1.[Status]=" + (int)SpecialProjectStatus.FINISH + "";
                else
                    qp.Where += " and " +  "s1.[Status]<>" + (int)SpecialProjectStatus.FINISH + "";
                qp.OrderBy = string.Format("order by {0} desc", "SubmitTimeSort");
                return SQLHelper.GetObjectList(this.GetDataSpecialProjectInfo, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取专项工程分页失败", e);
            }
        }


        /// <summary>
        /// 获取一个公司的专项工程，根据状态
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        IList ISpecialProject.GetSpecialProjectByCompanyStatus(int pageIndex, int pageSize, out int recordCount, string companyid, params SpecialProjectStatus[] status)
        {
            try
            {
                string table = TABLE_SPECIAL_PROJECT + " s1 left join " + TABLE_COMPANY + " s4 on " +
                 " s1.CompanyID = s4.CompanyID ";
                QueryParam qp = new QueryParam();
                qp.TableName = table;
                qp.ReturnFields = string.Format(
                   "s1.*,s4.CompanyName"
                    + ",{0} as SubmitTimeSort",  "s1.SubmitTime");
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " +  "s1.[CompanyID]='" + companyid + "'";
                for(int i=0;i<status.Length;i++)
                {
                    if (i == 0)
                    {
                        qp.Where += " and ( ";
                        qp.Where += " " + "s1.Status=" + (int)status[i] + " ";
                    }
                    else{
                        qp.Where += " or " + "s1.Status=" + (int)status[i] + " ";
                    }
                    if (i == status.Length - 1)
                    {
                        qp.Where += " ) ";
                    }
                }
                qp.OrderBy = string.Format("order by {0} desc", "SubmitTimeSort");
                return SQLHelper.GetObjectList(this.GetDataSpecialProjectInfo, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取专项工程分页失败", e);
            }
        }

        /// <summary>
        /// 插入一个工程量项
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        long ISpecialProject.InsertJobItem(SpecialProjectJobItemInfo job)
        {
            return InsertSpecialProjectJobItemInfo(null, job);
        }

        /// <summary>
        /// 更新一个工程量项
        /// </summary>
        /// <param name="job"></param>
        void ISpecialProject.UpdateJobItem(SpecialProjectJobItemInfo job)
        {
            UpdateSpecialProjectJobItemInfo(null, job);
        }

        /// <summary>
        /// 删除工程项
        /// </summary>
        /// <param name="itemid"></param>
        void ISpecialProject.DeleteJobItem(long itemid)
        {
            DeleteSpecialProjectJobItemInfo(null, itemid);
        }

        /// <summary>
        /// 插入一个预算项
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        long ISpecialProject.InsertBudgetItem(SpecialProjectBudgetItemInfo budget)
        {
            return InsertSpecialProjectBudgetItemInfo(null, budget);
        }

        /// <summary>
        /// 更新一个预算项
        /// </summary>
        /// <param name="job"></param>
        void ISpecialProject.UpdateBudgetItem(SpecialProjectBudgetItemInfo budget)
        {
            UpdateSpecialProjectBudgetItemInfo(null, budget);
        }

        /// <summary>
        /// 删除预算项
        /// </summary>
        /// <param name="itemid"></param>
        void ISpecialProject.DeleteBudgetItem(long itemid)
        {
            DeleteSpecialProjectBudgetItemInfo(null, itemid);
        }

        /// <summary>
        /// 获取预算清单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList ISpecialProject.GetSpecialProjectBudgetList(long id)
        {
            return GetSpecialProjectBudgetItemInfoList(id);
        }

        /// <summary>
        /// 判断是否有设计存在
        /// </summary>
        /// <param name="projectid">项目ID</param>
        /// <returns></returns>
        bool ISpecialProject.ExistsDesign(long projectid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from "+ TABLE_SPECIAL_PROJECT_DESIGN+"");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = projectid;
            return SQLHelper.Exists(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 插入设计记录
        /// </summary>
        /// <param name="design"></param>
        void ISpecialProject.InsertDesign(SpecialProjectDesignInfo design)
        {
            InsertSpecialProjectDesignInfo(null, design);
        }

        /// <summary>
        /// 更新设计记录
        /// </summary>
        /// <param name="design"></param>
        void ISpecialProject.UpdateDesign(SpecialProjectDesignInfo design)
        {
            UpdateSpecialProjectDesignInfo(null, design);
        }

        /// <summary>
        /// 获取设计记录
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        SpecialProjectDesignInfo ISpecialProject.GetDesign(long projectid)
        {
            return GetSpecialProjectDesignInfo(projectid);
        }


        /// <summary>
        /// 判断是否有招标信息存在
        /// </summary>
        /// <param name="projectid">项目ID</param>
        /// <returns></returns>
        bool ISpecialProject.ExistsBidding(long projectid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from " + TABLE_SPECIAL_PROJECT_BID + "");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = projectid;
            return SQLHelper.Exists(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 插入招标记录
        /// </summary>
        /// <param name="design"></param>
        void ISpecialProject.InsertBidding(SpecialProjectBidInfo bid)
        {
            InsertSpecialProjectBidInfo(null,bid);
        }

        /// <summary>
        /// 更新招标记录
        /// </summary>
        /// <param name="design"></param>
        void ISpecialProject.UpdateBidding(SpecialProjectBidInfo bid)
        {
            UpdateSpecialProjectBidInfo(null, bid);
        }

        /// <summary>
        /// 获取招标记录
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        SpecialProjectBidInfo ISpecialProject.GetBidding(long projectid)
        {
            return GetSpecialProjectBidInfo(projectid);
        }


        /// <summary>
        /// 插入一个施工项
        /// </summary>
        /// <param name="item"></param>
        long ISpecialProject.InsertPlanItem(SpecialProjectPlanInfo item)
        {
            return InsertSpecialProjectPlanInfo(null,item);
        }

        /// <summary>
        /// 更新一个施工项
        /// </summary>
        /// <param name="item"></param>
        void ISpecialProject.UpdatePlanItem(SpecialProjectPlanInfo item)
        {
            
            IList succeedList = GetSpecialProjectPlanInfoSucceedList(item.ItemID);
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //更新原来的项主要的
                UpdateSpecialProjectPlanInfo(trans, item);

                //再更新后续项的开始时间和结束时间
                foreach (SpecialProjectPlanInfo plan in succeedList)
                {
                    plan.StartTime = item.EndTime.AddDays(plan.DaysAfter);
                    plan.EndTime = plan.StartTime.AddDays(plan.Days - 1);
                    UpdateSpecialProjectPlanInfo(trans, plan);
                }
                //事务提交
                trans.Commit();
            }
            catch (SqlException sqlex)
            {
                //回滚
                trans.Rollback();
                throw sqlex;
            }
            catch (Exception ex)
            {
                //回滚
                trans.Rollback();
                throw ex;
            }
            finally
            {
                //关闭连接
                if (trans != null)
                {
                    trans.Dispose();
                    trans = null;
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
        }

        /// <summary>
        /// 删除一个施工项，并且把其后继续项的前置项修改为其前置项
        /// </summary>
        /// <param name="itemid"></param>
        void ISpecialProject.DeletePlanItem(long itemid)
        {
            //GETITEM,RECORD THE PREFIXITEMID,DELETE ITEM,AND MODIFY THE COMING ITEMS
            SpecialProjectPlanInfo item = GetSpecialProjectPlanInfo(itemid);
            if (item != null && item.ItemID > 0)
            {
                long prefixitemid = item.PrefixItemID;
                IList succeedList = GetSpecialProjectPlanInfoSucceedList(itemid);
                SpecialProjectPlanInfo prefixitem = new SpecialProjectPlanInfo();
                if (prefixitemid != 0)
                {
                    prefixitem = GetSpecialProjectPlanInfo(prefixitemid);
                }
                SqlConnection conn = null;
                SqlTransaction trans = null;
                try
                {
                    //建立连接，开始事务
                    conn = new SqlConnection(SQLHelper.ConnectionString);
                    conn.Open();
                    trans = conn.BeginTransaction();

                    //先获取ID
                    DeleteSpecialProjectPlanInfo(trans, itemid);

                    foreach (SpecialProjectPlanInfo plan in succeedList)
                    {
                        plan.PrefixItemID = prefixitemid;
                        if (prefixitemid != 0)
                        {
                            item.StartTime = prefixitem.EndTime.AddDays(item.DaysAfter);
                            item.EndTime = item.StartTime.AddDays(item.Days);
                        }
                        UpdateSpecialProjectPlanInfo(trans, plan);
                    }
                    //事务提交
                    trans.Commit();
                }
                catch (SqlException sqlex)
                {
                    //回滚
                    trans.Rollback();
                    throw sqlex;
                }
                catch (Exception ex)
                {
                    //回滚
                    trans.Rollback();
                    throw ex;
                }
                finally
                {
                    //关闭连接
                    if (trans != null)
                    {
                        trans.Dispose();
                        trans = null;
                    }
                    if (conn != null)
                    {
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                    }
                }
            }
        }

        /// <summary>
        /// 获取一个施工项
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        SpecialProjectPlanInfo ISpecialProject.GetPlanItem(long itemid)
        {
            SpecialProjectPlanInfo item = GetSpecialProjectPlanInfo(itemid);

            item.ProgressCheckRecord = GetSpecialProjectCheckRecordInfoList(item.ItemID);
            return item;

        }

        /// <summary>
        /// 获取施工项列表
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        IList ISpecialProject.GetPlanItemList(long projectid)
        {
            IList list =  GetSpecialProjectPlanInfoList(projectid);
            foreach (SpecialProjectPlanInfo item in list)
            {
                item.ProgressCheckRecord = GetSpecialProjectCheckRecordInfoList(item.ItemID);
            }
            return list;
        }


        /// <summary>
        /// 插入一个进场设备项
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        long ISpecialProject.InsertDeviceItem(SpecialProjectDeviceInfo device)
        {
            return InsertSpecialProjectDeviceInfo(null, device);
        }

        /// <summary>
        /// 更新一个进场设备项
        /// </summary>
        /// <param name="job"></param>
        void ISpecialProject.UpdateDeviceItem(SpecialProjectDeviceInfo device)
        {
            UpdateSpecialProjectDeviceInfo(null, device);
        }
        /// <summary>
        /// 更新一个进场设备项，增加进场数量
        /// </summary>
        /// <param name="job"></param>
        decimal ISpecialProject.UpdateDeviceItem(long itemid, decimal increaseamount, DateTime time)
        {
            return UpdateSpecialProjectDeviceInfo(null, itemid, increaseamount, time);
        }
        
        
        /// <summary>
        /// 删除进场设备项
        /// </summary>
        /// <param name="itemid"></param>
        void ISpecialProject.DeleteDeviceItem(long itemid)
        {
            DeleteSpecialProjectDeviceInfo(null, itemid);
        }

        /// <summary>
        /// 获取进场设备清单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList ISpecialProject.GetSpecialProjectDeviceList(long projectid)
        {
            IList list = GetSpecialProjectDeviceInfoList(projectid);
            foreach (SpecialProjectDeviceInfo device in list)
            {
                device.DeviceInRecordList = GetSpecialProjectDeviceInRecordList(device.ItemID);
            }
            return list;
        }


        /// <summary>
        /// 插入一个预支付项
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        long ISpecialProject.InsertPrePayItem(SpecialProjectPrePayInfo item)
        {
            return InsertSpecialProjectPrePayInfo(null, item);
            
        }

        /// <summary>
        /// 更新一个预支付项
        /// </summary>
        /// <param name="job"></param>
        void ISpecialProject.UpdatePrePayItem(SpecialProjectPrePayInfo item)
        {
            UpdateSpecialProjectPrePayInfo(null, item);
        }

        /// <summary>
        /// 删除预支付项
        /// </summary>
        /// <param name="itemid"></param>
        void ISpecialProject.DeletePrePayItem(long itemid)
        {
            DeleteSpecialProjectPrePayInfo(null, itemid);
        }

        /// <summary>
        /// 获取预支付项清单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList ISpecialProject.GetSpecialProjectPrePayList(long id)
        {
            return GetSpecialProjectPrePayInfoList(id);
        }


        /// <summary>
        /// 插入一个合同支付项
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        long ISpecialProject.InsertContractPayItem(SpecialProjectContractPayInfo item)
        {
            return InsertSpecialProjectContractPayInfo(null, item);

        }

        /// <summary>
        /// 更新一个合同支付项
        /// </summary>
        /// <param name="job"></param>
        void ISpecialProject.UpdateContractPayItem(SpecialProjectContractPayInfo item)
        {
            UpdateSpecialProjectContractPayInfo(null, item);
        }

        /// <summary>
        /// 删除合同支付项
        /// </summary>
        /// <param name="itemid"></param>
        void ISpecialProject.DeleteContractPayItem(long itemid)
        {
            DeleteSpecialProjectContractPayInfo(null, itemid);
        }

        /// <summary>
        /// 获取合同支付项清单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList ISpecialProject.GetSpecialProjectContractPayList(long id)
        {
            return GetSpecialProjectContractPayInfoList(id);
        }

        /// <summary>
        /// 插入一个进场设备记录
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        long ISpecialProject.InsertDeviceInRecord(SpecialProjectDeviceInRecord record)
        {
            return InsertSpecialProjectDeviceInRecord(null, record);
        }

        /// <summary>
        /// 更新一个进场设备记录
        /// </summary>
        /// <param name="job"></param>
        void ISpecialProject.UpdateDeviceInRecord(SpecialProjectDeviceInRecord record)
        {
            UpdateSpecialProjectDeviceInRecord(null, record);
        }

        /// <summary>
        /// 删除进场设备记录
        /// </summary>
        /// <param name="itemid"></param>
        void ISpecialProject.DeleteDeviceInRecord(long recordid)
        {
            DeleteSpecialProjectDeviceInRecord(null, recordid);
        }

        /// <summary>
        /// 获取进场设备记录清单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList ISpecialProject.GetSpecialProjectDeviceInRecordList(long deviceItemID)
        {
            return GetSpecialProjectDeviceInRecordList(deviceItemID);
        }

        /// <summary>
        /// 插入一个进度检查记录
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        long ISpecialProject.InsertProgressCheckRecord(SpecialProjectCheckRecordInfo record)
        {
            return InsertSpecialProjectCheckRecordInfo(null, record);
        }

        /// <summary>
        /// 更新一个进度检查记录
        /// </summary>
        /// <param name="job"></param>
        void ISpecialProject.UpdateProgressCheckRecord(SpecialProjectCheckRecordInfo record)
        {
            UpdateSpecialProjectCheckRecordInfo(null, record);
        }

        /// <summary>
        /// 删除进度检查记录
        /// </summary>
        /// <param name="itemid"></param>
        void ISpecialProject.DeleteProgressCheckRecord(long recordid)
        {
            DeleteSpecialProjectCheckRecordInfo(null, recordid);
        }

        /// <summary>
        /// 获取进度检查记录清单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList ISpecialProject.GetSpecialProjectProgressCheckRecordList(long planItemID)
        {
            return GetSpecialProjectCheckRecordInfoList(planItemID);
        }

        /// <summary>
        /// 插入一个月度支付项记录
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        void ISpecialProject.InsertMonthlyPayRecord(SpecialProjectPayRecordInfo record)
        {
            InsertSpecialProjectPayRecordInfo(null, record);
        }

        /// <summary>
        /// 更新一个月度支付项记录
        /// </summary>
        /// <param name="job"></param>
        void ISpecialProject.UpdateMonthlyPayRecord(SpecialProjectPayRecordInfo record)
        {
            UpdateSpecialProjectPayRecordInfo(null, record);
        }

        /// <summary>
        /// 删除月度支付项记录
        /// </summary>
        /// <param name="itemid"></param>
        void ISpecialProject.DeleteMonthlyPayRecord(long projectid,int year,int month)
        {
            DeleteSpecialProjectPayRecordInfo(null, projectid, year, month);
        }

        /// <summary>
        /// 获取月度支付项记录清单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList ISpecialProject.GetSpecialProjectMonthlyPayRecordList(long projectid)
        {
            return GetSpecialProjectPayRecordInfoList(projectid);
        }

        /// <summary>
        /// 判断该月支付是否已经存在
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        bool ISpecialProject.ExistsMonthlyPayRecord(long projectid, int year, int month)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from FM2E_SpecialProjectPayRecord");
            strSql.Append(" where ProjectID=@ProjectID and Year=@Year and Month=@Month ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@Month", SqlDbType.Int,4)};
            parameters[0].Value = projectid;
            parameters[1].Value = year;
            parameters[2].Value = month;
            return SQLHelper.Exists(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 插入一个变更记录，包含变更详情
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        long ISpecialProject.InsertModifyInfo(SpecialProjectModifyInfo modify)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            long id = 1;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先插入消息
                id = InsertSpecialProjectModifyInfo(trans, modify);

                modify.ModifyID = id;
                foreach (SpecialProjectModifyDeviceInfo item in modify.DetailList)
                {
                    item.ModifyApplyID = id;
                    InsertSpecialProjectModifyDeviceInfo(trans, item);
                }
                //事务提交
                trans.Commit();
            }
            catch (SqlException sqlex)
            {
                //回滚
                trans.Rollback();
                throw sqlex;
            }
            catch (Exception ex)
            {
                //回滚
                trans.Rollback();
                throw ex;
            }
            finally
            {
                //关闭连接
                if (trans != null)
                {
                    trans.Dispose();
                    trans = null;
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
            return id;
        }

        /// <summary>
        /// 更新一个变更记录，含变更详情
        /// </summary>
        /// <param name="job"></param>
        void ISpecialProject.UpdateModifyInfo(SpecialProjectModifyInfo modify)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
           
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先插入消息
                UpdateSpecialProjectModifyInfo(trans, modify);

                
                foreach (SpecialProjectModifyDeviceInfo item in modify.DetailList)
                {
                    if (item.ItemID == 0)
                    {
                        InsertSpecialProjectModifyDeviceInfo(trans, item);
                    }
                    else
                    {
                        UpdateSpecialProjectModifyDeviceInfo(trans, item);
                    }
                }
                //事务提交
                trans.Commit();
            }
            catch (SqlException sqlex)
            {
                //回滚
                trans.Rollback();
                throw sqlex;
            }
            catch (Exception ex)
            {
                //回滚
                trans.Rollback();
                throw ex;
            }
            finally
            {
                //关闭连接
                if (trans != null)
                {
                    trans.Dispose();
                    trans = null;
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
        }

        /// <summary>
        /// 删除变更记录，删除变更详情
        /// </summary>
        /// <param name="itemid"></param>
        void ISpecialProject.DeleteModifyInfo(long modifyid)
        {
            DeleteSpecialProjectModifyInfo(null, modifyid);
        }

        /// <summary>
        /// 获取变更列表，含变更详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList ISpecialProject.GetSpecialProjectModifyInfoList(long projectid)
        {
            IList list = GetSpecialProjectModifyInfoList(projectid);
            foreach (SpecialProjectModifyInfo item in list)
            {
                item.DetailList = GetSpecialProjectModifyDeviceInfoList(item.ModifyID);
            }
            return list;
        }

        /// <summary>
        /// 获取变更修改单
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="modifyid"></param>
        /// <returns></returns>
        SpecialProjectModifyInfo ISpecialProject.GetSpecialProjectModifyInfo( long modifyid)
        {
            SpecialProjectModifyInfo modify =  GetSpecialProjectModifyInfo(modifyid);
            modify.DetailList = GetSpecialProjectModifyDeviceInfoList(modifyid);
            return modify;
        }


        /// <summary>
        /// 判断是否有验收记录存在
        /// </summary>
        /// <param name="projectid">项目ID</param>
        /// <returns></returns>
        bool ISpecialProject.ExistsCheckAccept(long projectid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from " + TABLE_SPECIAL_PROJECT_CHECK_ACCEPT + "");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.BigInt)};
            parameters[0].Value = projectid;
            return SQLHelper.Exists(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 插入验收记录
        /// </summary>
        /// <param name="design"></param>
        void ISpecialProject.InserCheckAccept(SpecialProjectCheckAcceptInfo check)
        {
            InsertSpecialProjectCheckAcceptInfo(null, check);
        }

        /// <summary>
        /// 更新验收记录
        /// </summary>
        /// <param name="design"></param>
        void ISpecialProject.UpdateCheckAccept(SpecialProjectCheckAcceptInfo check)
        {
            UpdateSpecialProjectCheckAcceptInfo(null, check);
        }

        /// <summary>
        /// 获取验收记录
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        SpecialProjectCheckAcceptInfo ISpecialProject.GetCheckAccept(long projectid)
        {
            return GetSpecialProjectCheckAcceptInfo(projectid);
        }


        /// <summary>
        /// 插入审批项
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        void ISpecialProject.InsertApprovalRecord(SpecialProjectApprovalInfo record)
        {
            InsertSpecialProjectApprovalInfo(null, record);
        }

        /// <summary>
        /// 更新审批项
        /// </summary>
        /// <param name="job"></param>
        void ISpecialProject.UpdateApprovalyRecord(SpecialProjectApprovalInfo record)
        {
            UpdateSpecialProjectApprovalInfo(null, record);
        }

        /// <summary>
        /// 删除审批项
        /// </summary>
        /// <param name="itemid"></param>
        void ISpecialProject.DeleteApprovalRecord(long itemid)
        {
            DeleteSpecialProjectApprovalInfo(null, itemid);
        }

        /// <summary>
        /// 获取审批项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList ISpecialProject.GetSpecialProjectApprovalRecordList(long projectid)
        {
            return GetSpecialProjectApprovalInfoList(projectid);
        }


        /// <summary>
        /// 生成查询信息参数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam ISpecialProject.GenerateSearchInfo(SpecialProjectSearchInfo item)
        {
           
            string sqlSearch = "where 1=1";
            string empty = Guid.Empty.ToString("N");

            if (item.CompanyID != "")
            {
                sqlSearch += " and s1.[CompanyID] = '" + item.CompanyID + "'";
            }

            if (item.BidCompany != "")
            {
                sqlSearch += " and s3.[BiddenCompany] like '%" + item.BidCompany + "%'";
            }
            if (item.DesignCompany != "")
            {
                sqlSearch += " and s2.[Designer] like '%" + item.DesignCompany + "%'";
            }

            if (item.ProjectName != "")
            {
                sqlSearch += " and s1.[ProjectName] like '%" + item.ProjectName + "%'";
            }
            
            if (DateTime.Compare(item.TimeLower, DateTime.MinValue) != 0)
            {
                sqlSearch += " and s1.[SubmitTime]>='" + item.TimeLower.ToShortDateString() + " 00:00' ";// and s1.[Birthday]<='" + item.MaxBirthday.ToShortDateString() + " 23:59')";
            }

            if(DateTime.Compare(item.TimeUpper,DateTime.MaxValue)!=0)
            {
                sqlSearch += " and s1.[UpdateTime]<'" + item.TimeUpper.AddDays(1).ToShortDateString() + " 00:00' ";
            }

            if (item.BudgetLower != decimal.MinValue)
            {
                sqlSearch += " and s1.[Budget]>=" + item.BudgetLower.ToString() + " ";
            }

            if (item.BudgetUpper != decimal.MaxValue)
            {
                sqlSearch += " and s1.[Budget]<=" + item.BudgetUpper.ToString() + " ";
            }

            if (item.StatusArray != null && item.StatusArray.Length > 0)
            {
                for (int i = 0; i < item.StatusArray.Length; i++)
                {
                    if (i == 0)
                    {
                        sqlSearch += " and ( ";
                        sqlSearch += " " + " s1.Status=" + (int)item.StatusArray[i] + " ";
                    }
                    else
                    {
                        sqlSearch += " or " + " s1.Status=" + (int)item.StatusArray[i] + " ";
                    }
                    if (i == item.StatusArray.Length - 1)
                    {
                        sqlSearch += " ) ";
                    }
                }
            }

            QueryParam q = new QueryParam();

            q.TableName = TABLE_SPECIAL_PROJECT + " s1 left join " + TABLE_SPECIAL_PROJECT_DESIGN + " s2 on s1.ProjectID=s2.ProjectID " +
                " left join " + TABLE_SPECIAL_PROJECT_BID + " s3 on s1.ProjectID = s3.ProjectID left join " + TABLE_COMPANY +" s4 on s1.CompanyID = s4.CompanyID";

            q.ReturnFields = "s1.*,s1.CompanyID as CompanyIDSort,s4.CompanyName";

            q.OrderBy = "order by CompanyIDSort asc,UpdateTime desc";

            q.Where = sqlSearch;

            return q;
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        IList ISpecialProject.SearchSpecialProject(QueryParam qp, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetDataSpecialProjectInfo, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取专项工程分页失败", e);
            }
        }

        #endregion
    }
}
