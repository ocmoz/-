using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using FM2E.Model.Schedule;
using FM2E.Model.Utils;
using FM2E.Model.Plan;
using System.Data.SqlClient;

namespace FM2E.IDAL.Plan
{
    public interface IPlanInformation
    {
        void AddPlan(PlanInfo item); 
        IList GetPlanList(QueryParam term, out int recordCount);
        PlanInfo GetPlan(PlanInfo item);
        void DelPlan(int id);

        PlanInfo GetLastPlan(PlanInfo item);
        void UpdatePlan(PlanInfo item);
        /// <summary>
        /// 插入明细
        /// </summary>
        void AddSchedule(ScheduleInfo item);
        void AddScheduleActual(ScheduleInfo item);
        void DelSchedule(long id);
        void DelScheduleActual(long id);

        void AddScheduleIncome(ScheduleInfo item);
        void AddScheduleIncomeActual(ScheduleInfo item);
        void DelScheduleIncome(long id);
        void DelScheduleIncomeActual(long id);

        List<ScheduleInfo> GetSchedule(int id);
        List<ScheduleInfo> GetScheduleActual(int id);
        List<ScheduleInfo> GetScheduleGroupBy(int id);

        List<ScheduleInfo> GetScheduleIncome(int id);
        List<ScheduleInfo> GetScheduleIncomeActual(int id);
        List<ScheduleInfo> GetScheduleIncomeGroupBy(int id);

        List<PlanInfo> GetStatistics(DateTime dt1, DateTime dt2);
    }
}
