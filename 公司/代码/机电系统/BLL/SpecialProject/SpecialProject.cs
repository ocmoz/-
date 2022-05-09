using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.SpecialProject;
using FM2E.Model.SpecialProject;
using FM2E.Model.Utils;
using FM2E.BLL.Utils;
using FM2E.DALFactory;
using FM2E.Model.Exceptions;
using FM2E.WorkflowLayer;


namespace FM2E.BLL.SpecialProject
{
    /// <summary>
    /// 专项工程业务逻辑处理类
    /// </summary>
    public class SpecialProject
    {
        /// <summary>
        /// 数据库访问层接口
        /// </summary>
        private readonly ISpecialProject dal = SpecialProjectAccess.CreateSpecialProject();

        #region 逻辑处理函数
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="project"></param>
        public long SaveProjectBasicInfoDraft(SpecialProjectInfo project)
        {
            if (project.ProjectID == 0)//新的
            {
                return dal.InsertSpecialProjectInfo(project);
            }
            else//更新
            {
                dal.UpdateSpecialProjectInfo(project);
                return project.ProjectID;
            }
        }


        public void DeleteSpecialProject(long id)
        {
            dal.DeleteSpecialProjectInfo(id);
        }

        /// <summary>
        /// 获取专项工程基本信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SpecialProjectInfo GetSpecialProjectBasicInfo(long id)
        {
            return dal.GetSpecialProjectInfo(id);
        }

        /// <summary>
        /// 获取专项工程所有信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SpecialProjectInfo GetSpecialProject(long id)
        {
            SpecialProjectInfo project = dal.GetSpecialProjectInfo(id);
            IList joblist = dal.GetSpecialProjectJobList(id);
            project.JobItems = joblist;
            IList budgetlist = dal.GetSpecialProjectBudgetList(id);
            project.BudgetItems = budgetlist;
            project.Design = dal.GetDesign(id);
            project.Bid = dal.GetBidding(id);
            IList planList = dal.GetPlanItemList(id);
            project.PlanItems = planList;

            IList deviceList = dal.GetSpecialProjectDeviceList(id);
            project.DeviceList = deviceList;

            IList prepayList = dal.GetSpecialProjectPrePayList(id);
            project.PrePayList = prepayList;
            IList contractpayList = dal.GetSpecialProjectContractPayList(id);
            project.ContractPayList = contractpayList;

            IList monthlypayList = dal.GetSpecialProjectMonthlyPayRecordList(id);
            project.MonthlyPayRecordList = monthlypayList;

            IList modifyList = dal.GetSpecialProjectModifyInfoList(id);
            project.ModifyList = modifyList;

            project.CheckAcceptInfo = dal.GetCheckAccept(id);

            IList approval = dal.GetSpecialProjectApprovalRecordList(id);
            project.ApprovalRecords = approval;
            return project;
        }

        /// <summary>
        /// 获取一个公司的所有专项工程
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        public IList GetSpecialProjectList(int pageIndex, int pageSize, out int recordCount, string companyid, bool isFinish)
        {
            return dal.GetSpecialProjectByCompany(pageIndex, pageSize, out recordCount, companyid, isFinish);
        }

        /// <summary>
        /// 根据状态列表获取一个公司的所有专项工程
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        public IList GetSpecialProjectList(int pageIndex, int pageSize, out int recordCount, string companyid,  params SpecialProjectStatus[] status)
        {
            return dal.GetSpecialProjectByCompanyStatus(pageIndex, pageSize, out recordCount, companyid, status);
        }

        /// <summary>
        /// 保存一个工作项，如果工作项的ITEMID=0，则添加，不为0则更新
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public long SaveJobItem(SpecialProjectJobItemInfo job)
        {
            if (job.ItemID == 0)//新的
            {
                return dal.InsertJobItem(job);
            }
            else//更新
            {
                dal.UpdateJobItem(job);
                return job.ItemID;
            }
        }

        /// <summary>
        /// 删除工程项
        /// </summary>
        /// <param name="itemid"></param>
        public void DeleteJobItem(long itemid)
        {
            dal.DeleteJobItem(itemid);
        }

        /// <summary>
        /// 保存一个预算项，如果预算项的ITEMID=0，则添加，不为0则更新
        /// </summary>
        /// <param name="budget"></param>
        /// <returns></returns>
        public long SaveBudgetItem(SpecialProjectBudgetItemInfo budget)
        {
            if (budget.ItemID == 0)//新的
            {
                return dal.InsertBudgetItem(budget);
            }
            else//更新
            {
                dal.UpdateBudgetItem(budget);
                return budget.ItemID;
            }
        }

        /// <summary>
        /// 删除预算项
        /// </summary>
        /// <param name="itemid"></param>
        public void DeleteBudgetItem(long itemid)
        {
            dal.DeleteBudgetItem(itemid);
        }

        /// <summary>
        ///  保存设计
        /// </summary>
        /// <param name="design"></param>
        public void SaveDesign(SpecialProjectDesignInfo design)
        {
            if (!dal.ExistsDesign(design.ProjectID))//新的
            {
                dal.InsertDesign(design);
            }
            else//更新
            {
                dal.UpdateDesign(design);
            }
        }

        /// <summary>
        ///  保存验收记录
        /// </summary>
        /// <param name="design"></param>
        public void SaveCheckAccept(SpecialProjectCheckAcceptInfo check)
        {
            if (!dal.ExistsCheckAccept(check.ProjectID))//新的
            {
                dal.InserCheckAccept(check);
            }
            else//更新
            {
                dal.UpdateCheckAccept(check);
            }
        }


        /// <summary>
        ///  保存招标信息
        /// </summary>
        /// <param name="bid"></param>
        public void SaveBidding(SpecialProjectBidInfo bid)
        {
            if (!dal.ExistsBidding(bid.ProjectID))//新的
            {
                dal.InsertBidding(bid);
            }
            else//更新
            {
                dal.UpdateBidding(bid);
            }
        }

        /// <summary>
        /// 保存一个施工计划项
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public long SavePlanItem(SpecialProjectPlanInfo item)
        {
            if (item.ItemID == 0)//新的
            {
                return dal.InsertPlanItem(item);
            }
            else//更新
            {
                dal.UpdatePlanItem(item);
                return item.ItemID;
            }
        }

        /// <summary>
        /// 删除一个施工计划项，如果有其他是这个工作计划项的后继的话，则把它的后续的前件变为它自身前件
        /// </summary>
        /// <param name="itemid"></param>
        public void DeletePlanItem(long itemid)
        {
            dal.DeletePlanItem(itemid);
        }

        /// <summary>
        /// 获取一个施工计划项，包括获取其前件的名称
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public SpecialProjectPlanInfo GetPlanItem(long itemid)
        {
            return dal.GetPlanItem(itemid);
        }
        #endregion

        /// <summary>
        /// 生成甘特图的HTML表示
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public string MakeGant(IList items)
        {
            DateTime min = DateTime.MaxValue;
            DateTime max = DateTime.MinValue;
            foreach (SpecialProjectPlanInfo item in items)
            {
                if (min.CompareTo(item.StartTime) > 0)
                {
                    min = item.StartTime;
                }
                if (max.CompareTo(item.EndTime) < 0)
                {
                    max = item.EndTime;
                }
            }
            int days = (int)((max - min).TotalDays + 1);
            StringBuilder strTable = new StringBuilder();
            strTable.Append("<style>td.g{border:1pt black solid;background-color:white} " +
                "td.finish{border:1pt black solid;background-color:green} " +
                "td.notfinish{border:1pt black solid;background-color:yellow} " +
                "td.first{border:1pt black solid;background-color:white;border-left:2pt red;} " +
                "td.last{border:1pt black solid;background-color:white;border-right:2pt red;} " +
                "</style>");
            strTable.Append("<table style=' border-collapse: collapse;'>");

            //第一行，年月
            strTable.Append("<tr>");
            strTable.Append("<th rowspan='2' class='g locked' style='width:200px'><span style='display:inline-block;width:200px'>工程项目</span></th>");
            for (DateTime current = min;
                current.Year * 12 + current.Month <= max.Year * 12 + max.Month;
                current = DateTime.Parse(current.AddMonths(1).ToString("yyyy-MM") + "-1"))
            {
                int colspan = 0;
                colspan = (int)(DateTime.Parse(current.AddMonths(1).ToString("yyyy-MM") + "-1").AddDays(-1) - current).TotalDays + 1;
                strTable.Append(string.Format("<th colspan='{0}' class='g locked3'>{1}</th>", colspan, current.ToString("yyyy-MM")));
            }
            strTable.Append("</tr>");

            //第2行，日
            //最后一个格
            DateTime lastDate = DateTime.Parse(max.AddMonths(1).ToString("yyyy-MM") + "-1").AddDays(-1);
            strTable.Append("<tr>");
            for (DateTime current = min; current.CompareTo(lastDate) <= 0; current = current.AddDays(1))
            {
                strTable.Append(string.Format("<th class='g locked3'>{0}</th>", current.Day.ToString("00")));
            }
            strTable.Append("</tr>");

            //每条工作项
            int itemIndex = 1;
            foreach (SpecialProjectPlanInfo item in items)
            {
                strTable.Append("<tr>");
                //名称
                strTable.Append(string.Format("<td class='g locked' style='width:200px;text-align:left'><span style='display:inline-block;width:200px'>{0}</span></td>", itemIndex.ToString() + ": " + item.ItemName));

                //前面空白区
                for (DateTime current = min; current.CompareTo(item.StartTime) < 0; current = current.AddDays(1))
                {
                    if (current.Month == current.AddDays(1).Month && current.Month == current.AddDays(-1).Month
                        || current.CompareTo(lastDate) == 0 || current.CompareTo(min) == 0)
                    {
                        strTable.Append("<td class='g'></td>");//同一个月内
                    }
                    if (current.Year * 12 + current.Month < current.AddDays(1).Year * 12 + current.AddDays(1).Month
                        && current.CompareTo(lastDate) != 0)
                    {
                        strTable.Append("<td class='last'></td>");//一个月的最后一天
                    }
                    if (current.Year * 12 + current.Month > current.AddDays(-1).Year * 12 + current.AddDays(-1).Month
                        && current.CompareTo(min) != 0)
                    {
                        strTable.Append("<td class='first'></td>");//一个月的第一天
                    }
                }
                int finish = (int)(item.Days * item.Progress);
                //完成区
                if (finish >= 1)
                {
                    strTable.Append(string.Format("<td class='finish'  colspan='{0}'></td>", finish));
                }

                int notfinish = item.Days - finish;
                //未完成区
                if (notfinish >= 1)
                {
                    strTable.Append(string.Format("<td class='notfinish'  colspan='{0}'></td>", notfinish));
                }
                //后面空白区
                for (DateTime current = item.EndTime.AddDays(1); current.CompareTo(lastDate) <= 0; current = current.AddDays(1))
                {
                    if (current.Month == current.AddDays(1).Month && current.Month == current.AddDays(-1).Month
                        || current.CompareTo(lastDate) == 0 || current.CompareTo(min) == 0)
                    {
                        strTable.Append("<td class='g'></td>");//同一个月内
                    }
                    if (current.Year * 12 + current.Month < current.AddDays(1).Year * 12 + current.AddDays(1).Month
                        && current.CompareTo(lastDate) != 0)
                    {
                        strTable.Append("<td class='last'></td>");//一个月的最后一天
                    }
                    if (current.Year * 12 + current.Month > current.AddDays(-1).Year * 12 + current.AddDays(-1).Month
                        && current.CompareTo(min) != 0)
                    {
                        strTable.Append("<td class='first'></td>");//一个月的第一天
                    }
                }


                strTable.Append("</tr>");
                itemIndex++;
            }

            strTable.Append("</table>");
            return strTable.ToString();
        }

        /// <summary>
        /// 保存一个进场设备项，如果工作项的ITEMID=0，则添加，不为0则更新
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public long SaveDeviceItem(SpecialProjectDeviceInfo device)
        {
            if (device.ItemID == 0)//新的
            {
                return dal.InsertDeviceItem(device);
            }
            else//更新
            {
                dal.UpdateDeviceItem(device);
                return device.ItemID;
            }
        }

        /// <summary>
        /// 更新设备信息数量
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="increaseamount"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public decimal UpdateDeviceItem(long itemid, decimal increaseamount, DateTime time)
        {
            return dal.UpdateDeviceItem(itemid, increaseamount, time);
        }

        /// <summary>
        /// 删除设备项
        /// </summary>
        /// <param name="itemid"></param>
        public void DeleteDeviceItem(long itemid)
        {
            dal.DeleteDeviceItem(itemid);
        }

        /// <summary>
        /// 保存一个预支付项，如果工作项的ITEMID=0，则添加，不为0则更新
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public long SavePrePayItem(SpecialProjectPrePayInfo item)
        {
            if (item.ItemID == 0)//新的
            {
                return dal.InsertPrePayItem(item);
            }
            else//更新
            {
                dal.UpdatePrePayItem(item);
                return item.ItemID;
            }
        }

        /// <summary>
        /// 删除预支付项
        /// </summary>
        /// <param name="itemid"></param>
        public void DeletePrePayItem(long itemid)
        {
            dal.DeletePrePayItem(itemid);
        }

        /// <summary>
        /// 保存一个合同支付项，如果工作项的ITEMID=0，则添加，不为0则更新
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public long SaveContractPayItem(SpecialProjectContractPayInfo item)
        {
            if (item.ItemID == 0)//新的
            {
                return dal.InsertContractPayItem(item);
            }
            else//更新
            {
                dal.UpdateContractPayItem(item);
                return item.ItemID;
            }
        }

        /// <summary>
        /// 删除合同支付项
        /// </summary>
        /// <param name="itemid"></param>
        public void DeleteContractPayItem(long itemid)
        {
            dal.DeleteContractPayItem(itemid);
        }

        /// <summary>
        /// 保存一个设备进场记录，如果工作项的ITEMID=0，则添加，不为0则更新
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public long SaveDeviceInRecord(SpecialProjectDeviceInRecord record)
        {
            if (record.RecordID == 0)//新的
            {
                
                return dal.InsertDeviceInRecord(record);
            }
            else//更新
            {
                dal.UpdateDeviceInRecord(record);
                return record.RecordID;
            }
        }

        /// <summary>
        /// 删除设备进场记录
        /// </summary>
        /// <param name="itemid"></param>
        public void DeleteDeviceInRecord(long recordid)
        {
            dal.DeleteDeviceInRecord(recordid);
        }


        /// <summary>
        /// 保存一个进度检查项记录，如果工作项的ITEMID=0，则添加，不为0则更新
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public long SaveProgressCheckRecord(SpecialProjectCheckRecordInfo record)
        {
            if (record.ItemID == 0)//新的
            {

                return dal.InsertProgressCheckRecord(record);
            }
            else//更新
            {
                dal.UpdateProgressCheckRecord(record);
                return record.ItemID;
            }
        }

        /// <summary>
        /// 删除进度检查项
        /// </summary>
        /// <param name="itemid"></param>
        public void DeleteProgressCheckRecord(long recordid)
        {
            dal.DeleteProgressCheckRecord(recordid);
        }

        /// <summary>
        /// 保存一个月进度支付记录，如果工作项的ITEMID=0，则添加，不为0则更新
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public void SaveMonthlyPayRecord(SpecialProjectPayRecordInfo record)
        {
            if (!dal.ExistsMonthlyPayRecord(record.ProjectID,record.Year,record.Month))//新的
            {
                dal.InsertMonthlyPayRecord(record);
            }
            else//更新
            {
                dal.UpdateMonthlyPayRecord(record);
               
            }
        }

        /// <summary>
        /// 删除月进度支付记录项
        /// </summary>
        /// <param name="itemid"></param>
        public void DeleteMonthlyPayRecord(long projectid,int year,int month)
        {
            dal.DeleteMonthlyPayRecord(projectid, year, month);
        }

        /// <summary>
        /// 保存一个变更记录，如果工作项的ITEMID=0，则添加，不为0则更新
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public void SaveModify(SpecialProjectModifyInfo modify)
        {
            if (modify.ModifyID==0)//新的
            {
                dal.InsertModifyInfo(modify);
            }
            else//更新
            {
                dal.UpdateModifyInfo(modify);

            }
        }

        /// <summary>
        /// 删除变更记录项
        /// </summary>
        /// <param name="itemid"></param>
        public void DeleteModify(long modifyid)
        {
            dal.DeleteModifyInfo(modifyid);
        }

        /// <summary>
        /// 获取变更记录项
        /// </summary>
        /// <param name="itemid"></param>
        public SpecialProjectModifyInfo GetModify(long modifyid)
        {
            return dal.GetSpecialProjectModifyInfo(modifyid);
        }



        /// <summary>
        /// 保存一个审批项，如果审批项的ITEMID=0，则添加，不为0则更新
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public void SaveApproval(SpecialProjectApprovalInfo record)
        {
            if (record.ItemID == 0)//新的
            {
                dal.InsertApprovalRecord(record);
            }
            else//更新
            {
                dal.UpdateApprovalyRecord(record);

            }
        }

        /// <summary>
        /// 删除审批项
        /// </summary>
        /// <param name="itemid"></param>
        public void DeleteApprovalRecord(long itemid)
        {
            dal.DeleteApprovalRecord(itemid);
        }

        /// <summary>
        /// 查找专项工程
        /// </summary>
        /// <param name="info">相关信息</param>
        /// <returns></returns>
        public IList SearchSpecialProject(SpecialProjectSearchInfo info, int currentPageIndex, int pageSize, out int recordCount)
        {
            QueryParam p = dal.GenerateSearchInfo(info);
            p.PageIndex = currentPageIndex;
            p.PageSize = pageSize;
            return dal.SearchSpecialProject(p, out recordCount);
        }

        public void SendPendingOrder(SpecialProjectInfo project,string username,string userpersonname,string companyid)
        {
            long thisID = project.ProjectID;
            string title = "";// "专项工程预算申请：" + project.ProjectName + " 待审批";
            string URL = "";// "../SpecialProject/ProjectApproval/MotherCompany/Budget/ViewProject.aspx?projectid=" + thisID + "&cmd=approval";
            string statestring = "";

            switch (project.Status)
            {
                case SpecialProjectStatus.BIDCHECK:
                    statestring = SpecialProjectWorkflow.BidCheckState;
                    title = "专项工程：" + project.ProjectName + " 待定标审查";
                    URL = "../SpecialProject/ProjectApproval/MotherCompany/Bid/ViewProject.aspx?projectid=" + thisID + "&cmd=approval";
                    break;
                case SpecialProjectStatus.BIDDING:
                    statestring = SpecialProjectWorkflow.BiddingState;
                    title = "专项工程：" + project.ProjectName + " 待招标";
                    URL = "../SpecialProject/ProjectManagement/Bid/ViewProject.aspx?projectid=" + thisID + "&cmd=edit";
                    break;
                case SpecialProjectStatus.BUDGETAPPORVAL:
                    statestring = SpecialProjectWorkflow.BudgetApprovalState;
                    title = "专项工程：" + project.ProjectName + " 待预算批复";
                    URL = "../SpecialProject/ProjectApproval/MotherCompany/Budget/ViewProject.aspx?projectid=" + thisID + "&cmd=approval";
                    break;
                case SpecialProjectStatus.BUDGETCHECK:
                    statestring = SpecialProjectWorkflow.BudgetCheckState;
                    title = "专项工程：" + project.ProjectName + " 待预算审查";
                    URL = "../SpecialProject/ProjectApproval/Company/Budget/ViewProject.aspx?projectid=" + thisID + "&cmd=approval";
                    break;
                case SpecialProjectStatus.COMPANYAPPOVAL:
                    statestring = SpecialProjectWorkflow.CompanyApprovalState;
                    title = "专项工程：" + project.ProjectName + " 待立项审查";
                    URL = "../SpecialProject/ProjectApproval/Company/Project/ViewProject.aspx?projectid=" + thisID + "&cmd=approval";
                    break;
                case SpecialProjectStatus.COSTCHECK:
                    statestring = SpecialProjectWorkflow.CostCheckState;
                    title = "专项工程：" + project.ProjectName + " 待成本审查";
                    URL = "../SpecialProject/ProjectApproval/CostOffice/ViewProject.aspx?projectid=" + thisID + "&cmd=approval";
                    break;
                case SpecialProjectStatus.DESIGNCHECK:
                    statestring = SpecialProjectWorkflow.DesignCheckState;
                    title = "专项工程：" + project.ProjectName + " 待设计审查";
                    URL = "../SpecialProject/ProjectApproval/Group/Design/ViewProject.aspx?projectid=" + thisID + "&cmd=approval";
                    break;
                case SpecialProjectStatus.DESIGNINPUT:
                    statestring = SpecialProjectWorkflow.DesignInputState;
                    title = "专项工程：" + project.ProjectName + " 待设计";
                    URL = "../SpecialProject/ProjectManagement/Design/ViewProject.aspx?projectid=" + thisID + "&cmd=edit";
                    break;
                case SpecialProjectStatus.GROUPAPPROVAL:
                    statestring = SpecialProjectWorkflow.GroupApprovalState;
                    title = "专项工程：" + project.ProjectName + " 待交通集团审批";
                    URL = "../SpecialProject/ProjectApproval/Group/Setup/ViewProject.aspx?projectid=" + thisID + "&cmd=approval";
                    break;
                case SpecialProjectStatus.MOTHERCOMPANYAPPROVAL:
                    statestring = SpecialProjectWorkflow.MotherCompanyApprovalState;
                    title = "专项工程：" + project.ProjectName + " 待路桥公司审批";
                    URL = "../SpecialProject/ProjectApproval/MotherCompany/Setup/ViewProject.aspx?projectid=" + thisID + "&cmd=approval";
                    break;
                case SpecialProjectStatus.PROJECTSETUP:
                    statestring = SpecialProjectWorkflow.ProjectSetupState;
                    title = "专项工程：" + project.ProjectName + " 可以启动立项";
                    URL = "../SpecialProject/ProjectApply/Apply.aspx?projectid=" + thisID + "&cmd=edit";
                    break;
                case SpecialProjectStatus.WAIT4START:
                    statestring = SpecialProjectWorkflow.Wait4StartState;
                    title = "专项工程：" + project.ProjectName + " 可以开工";
                    URL = "../SpecialProject/ProjectApply/Start.aspx?projectid=" + thisID + "&cmd=edit";

                    break;
                case SpecialProjectStatus.WORKING:
                    statestring = SpecialProjectWorkflow.WorkingState;
                    title = "专项工程：" + project.ProjectName + " 可以进行施工管理";
                    URL = "../SpecialProject/ProjectManagement/Working/ViewProject.aspx?projectid=" + thisID + "&cmd=edit";
                    break;
                default :
                    statestring = "";
                    break;

            }

            try
            {
                if (statestring != "")
                    WorkflowApplication.SendingPendingOrderToStateUsers(title, SpecialProjectWorkflow.WorkflowName, statestring, username, userpersonname, URL, 0, companyid);
            }
            catch (Exception ex)
            {
               throw new BLLException("工作流发送待办事务错误" + ex.Message,ex);
            }
        }
    }
}
