using System;
using System.Collections.Generic;
using System.Text;

using FM2E.Model.Schedule;
using FM2E.IDAL.Plan;
using FM2E.Model.Utils;
using System.Collections;
using FM2E.Model.Plan;
using System.Data.SqlClient;


namespace FM2E.BLL.Schedule
{
    public class Schedule
    {
        IPlanInformation dal = FM2E.DALFactory.PlanAccess.CreateSchedule();
        public void AddPlan(PlanInfo item)
        {
            dal.AddPlan(item);
        }
        public IList GetPlanList(string times, int pageIndex, int pageSize, out int recordCount)
        {            
            QueryParam qp = GetPlanTerm(times);
            qp.PageIndex = pageIndex;
            qp.PageSize = pageSize;
            return dal.GetPlanList(qp, out recordCount);
        }
        public QueryParam GetPlanTerm(string times)
        {
            QueryParam qp = new QueryParam();
            qp.ReturnFields = "*";
            qp.TableName = "FM2E_PlanView";
            qp.Where = "where 1=1";
            if (times.Trim() != "")
                qp.Where += " and [Year]=" + Convert.ToDateTime(times).Year + " and [month]=" + Convert.ToDateTime(times).Month;

            qp.OrderBy = "order by year desc,month desc";
            return qp;
        }

        public PlanInfo GetPlan(PlanInfo item)
        {
            return dal.GetPlan(item);
        }
        public void DelPlan(int id)
        {
            dal.DelPlan(id);           
        }

        public PlanInfo GetLastPlan(PlanInfo item)
        {
            return dal.GetLastPlan(item);
        }

        public void UpdatePlan(PlanInfo item)
        {
             dal.UpdatePlan(item);
        }
        /// <summary>
        /// 添加明细
        /// </summary>
        public void AddAndUpdateSchedule(long id,ScheduleInfo item)
        {
            //先删除
            dal.DelSchedule(id);
            //再添加
            foreach (ScheduleInfo itemlist in item.Schedulelist)
            {
                itemlist.PlanId = id;
                dal.AddSchedule(itemlist);
            }           
        }
        public void AddAndUpdateScheduleActual(long id, ScheduleInfo item)
        {
            //先删除
            dal.DelScheduleActual(id);
            //再添加
            foreach (ScheduleInfo itemlist in item.Schedulelist)
            {
                itemlist.PlanId = id;
                dal.AddScheduleActual(itemlist);
            }
        }
        public void AddAndUpdateScheduleIncome(long id, ScheduleInfo item)
        {
            //先删除
            dal.DelScheduleIncome(id);
            //再添加
            foreach (ScheduleInfo itemlist in item.Schedulelist)
            {
                itemlist.PlanId = id;
                dal.AddScheduleIncome(itemlist);
            }
        }
        public void AddAndUpdateScheduleIncomeActual(long id, ScheduleInfo item)
        {
            //先删除
            dal.DelScheduleIncomeActual(id);
            //再添加
            foreach (ScheduleInfo itemlist in item.Schedulelist)
            {
                itemlist.PlanId = id;
                dal.AddScheduleIncomeActual(itemlist);
            }
        }
     
        public List<ScheduleInfo> GetSchedule(int id)
        {
            return dal.GetSchedule(id);
        }
        public List<ScheduleInfo> GetScheduleActual(int id)
        {
            return dal.GetScheduleActual(id);
        }
        public List<ScheduleInfo> GetScheduleGroupBy(int id)
        {
            return dal.GetScheduleGroupBy(id);
        }

        public List<ScheduleInfo> GetScheduleIncome(int id)
        {
            return dal.GetScheduleIncome(id);
        }
        public List<ScheduleInfo> GetScheduleIncomeActual(int id)
        {
            return dal.GetScheduleIncomeActual(id);
        }
        public List<ScheduleInfo> GetScheduleIncomeGroupBy(int id)
        {
            return dal.GetScheduleIncomeGroupBy(id);
        }

        public List<PlanInfo> GetStatistics(DateTime dt1, DateTime dt2)
        {
            return dal.GetStatistics(dt1, dt2);
        }  
    }
}