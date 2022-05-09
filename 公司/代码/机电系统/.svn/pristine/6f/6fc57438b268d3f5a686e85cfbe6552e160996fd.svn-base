using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using WebUtility.Components;
using FM2E.Model.Maintain;
using FM2E.BLL.Maintain;
using System.Collections;
using System.Text;
using FM2E.Model.Basic;
using FM2E.BLL.Basic;
using FM2E.Model.System;
using System.IO;
using FM2E.Model.Utils;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;

public partial class Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionStatistic_Statistic_Statistic : System.Web.UI.Page
{
    private readonly MalfunctionHandle malfunctionBll = new MalfunctionHandle();
    private readonly Department departmentBll = new Department();
    private readonly EquipmentCost ecBLL = new EquipmentCost();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
        }
    }
    /// <summary>
    ///初始化页面
    /// </summary>
    private void InitialPage()
    {
        try
        {
            LoginUserInfo loginUser = UserData.CurrentUserData;

            DDLCompany.Items.Clear();
            DDLCompany.Items.Add(new ListItem("不限", ""));
            Company companybll = new Company();

            IList<CompanyInfo> companylist = companybll.GetAllCompany();

            foreach (CompanyInfo info in companylist)
            {
                DDLCompany.Items.Add(new ListItem(info.CompanyName, info.CompanyID));
            }


            //维修单位
            ddlMaintainTeam.Items.Clear();
            ddlMaintainTeam.Items.Add(new ListItem("不限", "0"));
            ddlMaintainTeam.Items.AddRange(ListItemHelper.GetAllMaintainTeams(loginUser.CompanyID));

            //故障部门
            ddlDepartment.Items.Clear();
            ddlDepartment.Items.Add(new ListItem("不限", "0"));
            ddlDepartment.Items.AddRange(ListItemHelper.GetDepartmentListItems(loginUser.CompanyID));

            //故障记录部门
            ddlRecordDepartment.Items.Clear();
            Department dpbll = new Department();
            DepartmentInfo dpitem = new DepartmentInfo();
            dpitem.DepartmentType = DepartmentType.MaintainTeam;
            IList<DepartmentInfo> dplist = dpbll.Search(dpitem);
            ddlRecordDepartment.Items.Add(new ListItem("全部维修部门", "0"));
            foreach (DepartmentInfo item in dplist)
            {
                ddlRecordDepartment.Items.Add(new ListItem(item.Name, item.DepartmentID.ToString()));
            }


            //ddlRecordDepartment.Items.Add(new ListItem("全部部门", "0"));
            //ddlRecordDepartment.Items.AddRange(ListItemHelper.GetDepartmentListItems(loginUser.CompanyID));
            //ddlRecordDepartment.SelectedValue = loginUser.DepartmentID.ToString();

            //系统
            ddlSystem.Items.Clear();
            ddlSystem.Items.Add(new ListItem("不限", ""));
            ddlSystem.Items.AddRange(ListItemHelper.GetSystemListItems());

            tbDateFrom.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + "01";
            tbDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    private Boolean IsWithInTime(DateTime actualendtime, DateTime value)
    {
        if (DateTime.Compare(value, actualendtime) >= 0)
            return true;
        else
            return false;
    }

    private IList listfilter(IList listwithintime)
    {
        IList listwithintimewithoutoverlap = new ArrayList();
        NoSortHashTable ht = new NoSortHashTable();
        foreach (MalfunctionStatisticInfo item in listwithintime)
        {
            if (ht.Contains(item.MaintainDept + "," + item.MalfunctionRank + "," + item.Status))
            {
                MalfunctionStatisticInfo info = (MalfunctionStatisticInfo)ht[item.MaintainDept + "," + item.MalfunctionRank + "," + item.Status];
                info.Count++;
            }
            else
            {
                listwithintimewithoutoverlap.Add(item);
                item.Count = 1;
                ht.Add(item.MaintainDept + "," + item.MalfunctionRank + "," + item.Status, item);
            }
        }
        return listwithintimewithoutoverlap;
    }

    /// <summary>
    /// 填充页面数据
    /// </summary>
    /// 

    private void FillData2()
    {
        try
        {
            FM2E.SQLServerDAL.Basic.Department departmentdal = new FM2E.SQLServerDAL.Basic.Department();

            IList<DepartmentInfo> deparmentlist = departmentdal.GetChilds(34);

            NoSortHashTable deparmentht = new NoSortHashTable();

            NoSortHashTable dataht = new NoSortHashTable();

            foreach (DepartmentInfo info in deparmentlist)
            {
                if (!deparmentht.Contains(info.Name))
                {
                    deparmentht.Add(info.Name, info);
                    dataht.Add(info.Name, new NoSortHashTable());
                }
            }

            FM2E.SQLServerDAL.System.User userdal = new FM2E.SQLServerDAL.System.User();

            UserSearchInfo item = new UserSearchInfo();

            foreach (String key in deparmentht.Keys)
            {
                DepartmentInfo info = deparmentht[key] as DepartmentInfo;
                IList userlist = userdal.GetUsersByDepartmentID(info.DepartmentID);

                NoSortHashTable userht = dataht[key] as NoSortHashTable;

                foreach (UserInfo userinfo in userlist)
                {

                    MalfunctionStatisticTerm term = new MalfunctionStatisticTerm();

                    if (!string.IsNullOrEmpty(tbDateFrom.Text.Trim()))
                        term.ReportDateFrom = Convert.ToDateTime(tbDateFrom.Text.Trim());

                    if (!string.IsNullOrEmpty(tbDateTo.Text.Trim()))
                        term.ReportDateTo = Convert.ToDateTime(tbDateTo.Text.Trim());

                    term.MaintainSataff = userinfo.PersonName;

                    IList list = malfunctionBll.GetMalfunctionStatisticData(term);

                    IList list2 = new FM2E.SQLServerDAL.Maintain.MalfunctionHandle().GetMalfunctionStatisticData2(term);

                    IList ListWithInTimeAndRepaired = new ArrayList();//未过期已修复

                    IList ListWithInTimeAndNoRepaired = new ArrayList();//未过期未修复

                    IList ListWithOutTimeAndRepaired = new ArrayList();//过期已修复

                    IList ListWithOutTimeAndNoRepaired = new ArrayList();//已过期未修复

                    IList ListWithInTimeAndRepairedwithoutoverlap = new ArrayList();

                    IList ListWithInTimeAndNoRepairedwithoutoverlap = new ArrayList();

                    IList ListWithOutTimeAndRepairedwithoutoverlap = new ArrayList();

                    IList ListWithOutTimeAndNoRepairedwithoutoverlap = new ArrayList();

                    term.ReportDateTo = term.ReportDateFrom.AddDays(-1);
                    term.ReportDateFrom = DateTime.MinValue;
                    term.Status = 3;

                    IList ListBeforeNoRepaired = malfunctionBll.GetMalfunctionStatisticData(term);//以前未过期未修复的

                    foreach (MalfunctionStatisticInfo detailitem in list2)
                    {
                        DateTime endtime = new DateTime();

                        DateTime actualendtime = detailitem.ReportDate.AddMinutes(detailitem.ActualRepairTime);
                        if (detailitem.ActualRepairTime == 0)
                            actualendtime = DateTime.Now;

                        switch (detailitem.RepairUnit)
                        {
                            case 1:
                                endtime = detailitem.ReportDate.AddMinutes(detailitem.RepairTime);
                                break;
                            case 2:
                                endtime = detailitem.ReportDate.AddHours(detailitem.RepairTime);
                                break;
                            case 3:
                                endtime = detailitem.ReportDate.AddDays(detailitem.RepairTime);
                                break;
                            case 4:
                                endtime = detailitem.ReportDate.AddMonths(detailitem.RepairTime);
                                break;
                            case 5:
                                endtime = detailitem.ReportDate.AddYears(detailitem.RepairTime);
                                break;

                        }

                        if (IsWithInTime(actualendtime, endtime))
                        {
                            if (detailitem.Status == MalfunctionHandleStatus.FunctionalityRestore || detailitem.Status == MalfunctionHandleStatus.Fixed || detailitem.Status == MalfunctionHandleStatus.Finished || detailitem.Status == MalfunctionHandleStatus.Waiting4MoneyApproval)
                                ListWithInTimeAndRepaired.Add(detailitem);
                            else
                                ListWithInTimeAndNoRepaired.Add(detailitem);
                        }
                        else
                        {
                            if (detailitem.Status == MalfunctionHandleStatus.FunctionalityRestore || detailitem.Status == MalfunctionHandleStatus.Fixed || detailitem.Status == MalfunctionHandleStatus.Finished || detailitem.Status == MalfunctionHandleStatus.Waiting4MoneyApproval)
                            {
                                ListWithOutTimeAndRepaired.Add(detailitem);
                            }
                            else
                                ListWithOutTimeAndNoRepaired.Add(detailitem);

                        }


                    }

                    ListWithInTimeAndRepairedwithoutoverlap = listfilter(ListWithInTimeAndRepaired);//过滤重复的

                    ListWithInTimeAndNoRepairedwithoutoverlap = listfilter(ListWithInTimeAndNoRepaired);//过滤重复的

                    ListWithOutTimeAndRepairedwithoutoverlap = listfilter(ListWithOutTimeAndRepaired);

                    ListWithOutTimeAndNoRepairedwithoutoverlap = listfilter(ListWithOutTimeAndNoRepaired);//过滤重复的

                    NoSortHashTable listdateht = new NoSortHashTable();

                    listdateht.Add("故障总数", list);

                    listdateht.Add("超期已修复数", ListWithOutTimeAndRepairedwithoutoverlap);

                    listdateht.Add("超期未修复数", ListWithOutTimeAndNoRepairedwithoutoverlap);

                    listdateht.Add("及时已修复数", ListWithInTimeAndRepairedwithoutoverlap);

                    listdateht.Add("未过期未修复数", ListWithInTimeAndNoRepairedwithoutoverlap);

                  //  listdateht.Add("前期未修复数", ListBeforeNoRepaired);

                    userht.Add(userinfo.PersonName, listdateht);


                }

            }

            StringBuilder table = new StringBuilder();
            table.Append("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"1\" bordercolor=\"#cccccc\" style=\"border-collapse: collapse;\">");

            table.Append("<tr><td align=\"center\">所属维护队</td><td align=\"center\">员工名字</td><td align=\"center\">故障总数</td><td align=\"center\">超期已修复数</td><td align=\"center\">超期未修复数</td><td align=\"center\">及时已修复数</td><td align=\"center\">未过期未修复数</td><td align=\"center\">前期未修复数</td><td align=\"center\">及时已修复数</td></tr>");

            foreach (string key in dataht.Keys)
            {
                NoSortHashTable userht = dataht[key] as NoSortHashTable;
                if (userht.Count <= 0)
                    continue;
                table.AppendFormat("<tr><td rowspan=\"{0}\" align=\"center\">{1}</td>", userht.Count, key);

                int usercount = 0;
                foreach (string userkey in userht.Keys)
                {
                    if (usercount != 0)
                        table.AppendFormat("<tr>");

                    table.AppendFormat("<td align=\"center\">{0}</td>", userkey);

                    NoSortHashTable listdateht = userht[userkey] as NoSortHashTable;


                    table.Append(Total(listdateht["故障总数"] as IList, userkey, "故障总数"));

                    table.Append(Total(listdateht["超期已修复数"] as IList, userkey, "超期已修复数"));

                    table.Append(Total(listdateht["超期未修复数"] as IList, userkey, "超期未修复数"));

                    table.Append(Total(listdateht["及时已修复数"] as IList, userkey, "及时已修复数"));

                    table.Append(Total(listdateht["未过期未修复数"] as IList, userkey, "未过期未修复数"));

                  //  table.Append(Total(listdateht["前期未修复数"] as IList, userkey, "前期未修复数"));

                    table.Append(TotalRatio(listdateht["及时已修复数"] as IList, userkey));

                    if (usercount != 0)
                        table.AppendFormat("</tr>");

                    usercount++;
                }

                table.Append("</tr>");


            }
            table.Append("</table>");

            Literal1.Text = table.ToString();


            TabContainer1.ActiveTabIndex = 2;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "统计时发生错误", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    private string TotalRatio(IList statisticData, string username)
    {
        StringBuilder rowHtml = new StringBuilder();
        int count = 0;
            foreach (MalfunctionStatisticInfo item in statisticData)
            {
                    count += item.Count;
            }

            float WithInNoRepaired = ViewState[username + "未过期未修复数"] == null ? 0 : Convert.ToSingle(ViewState[username + "未过期未修复数"]);
            float maintainnum = ViewState[username + "故障总数"] == null ? 0 : Convert.ToSingle(ViewState[username + "故障总数"]);
            float BeforeNoRepaired = ViewState[username + "前期未修复数"] == null ? 0 : Convert.ToSingle(ViewState[username + "前期未修复数"]);


            if (count != 0 && (maintainnum + BeforeNoRepaired - WithInNoRepaired) != 0)
            {
                float percent = (float)count / (maintainnum + BeforeNoRepaired - WithInNoRepaired);
                rowHtml.AppendFormat("<td align=\"center\">{0}%</td>", (percent * 100).ToString("0.##"));
            }
            else rowHtml.Append("<td align=\"center\">/</td>");


        return rowHtml.ToString();
    }

    private string Total(IList statisticData, string username,string type)
    {
        StringBuilder rowHtml = new StringBuilder();
        int count = 0;
        foreach (MalfunctionStatisticInfo item in statisticData)
        {
            count += item.Count;
        }

        ViewState[username + type] = count;
        if (count != 0)
            rowHtml.AppendFormat("<td align=\"center\">{0}</td>", count);
        else rowHtml.Append("<td align=\"center\">/</td>");

        return rowHtml.ToString();
    }



    private void FillData()
    {
        try
        {
            MalfunctionStatisticTerm term = this.GetStatisticTerm();
            IList list = malfunctionBll.GetMalfunctionStatisticData(term);

            IList list2 = new FM2E.SQLServerDAL.Maintain.MalfunctionHandle().GetMalfunctionStatisticData2(term);

            IList ListWithInTimeAndRepaired = new ArrayList();//未过期已修复

            IList ListWithInTimeAndNoRepaired = new ArrayList();//未过期未修复

            IList ListWithOutTimeAndRepaired = new ArrayList();//过期已修复

            IList ListWithOutTimeAndNoRepaired = new ArrayList();//已过期未修复

            IList ListWithInTimeAndRepairedwithoutoverlap = new ArrayList();

            IList ListWithInTimeAndNoRepairedwithoutoverlap = new ArrayList();

            IList ListWithOutTimeAndRepairedwithoutoverlap = new ArrayList();

            IList ListWithOutTimeAndNoRepairedwithoutoverlap = new ArrayList();

            term.ReportDateTo = term.ReportDateFrom.AddDays(-1);
            term.ReportDateFrom = DateTime.MinValue;
            term.Status = 3;

            IList ListBeforeNoRepaired = malfunctionBll.GetMalfunctionStatisticData(term);//以前未过期未修复的

            foreach (MalfunctionStatisticInfo item in list2)
            {
                DateTime endtime = new DateTime();

                DateTime actualendtime = item.ReportDate.AddMinutes(item.ActualRepairTime);
                if (item.ActualRepairTime == 0)
                    actualendtime = DateTime.Now;

                switch (item.RepairUnit)
                {
                    case 1:
                        endtime = item.ReportDate.AddMinutes(item.RepairTime);
                        break;
                    case 2:
                        endtime = item.ReportDate.AddHours(item.RepairTime);
                        break;
                    case 3:
                        endtime = item.ReportDate.AddDays(item.RepairTime);
                        break;
                    case 4:
                        endtime = item.ReportDate.AddMonths(item.RepairTime);
                        break;
                    case 5:
                        endtime = item.ReportDate.AddYears(item.RepairTime);
                        break;

                }

                if (IsWithInTime(actualendtime, endtime))
                {
                    if (item.Status == MalfunctionHandleStatus.FunctionalityRestore || item.Status == MalfunctionHandleStatus.Fixed || item.Status == MalfunctionHandleStatus.Finished || item.Status == MalfunctionHandleStatus.Waiting4MoneyApproval)
                        ListWithInTimeAndRepaired.Add(item);
                    else
                        ListWithInTimeAndNoRepaired.Add(item);
                }
                else
                {
                    if (item.Status == MalfunctionHandleStatus.FunctionalityRestore || item.Status == MalfunctionHandleStatus.Fixed || item.Status == MalfunctionHandleStatus.Finished || item.Status == MalfunctionHandleStatus.Waiting4MoneyApproval)
                    {
                        ListWithOutTimeAndRepaired.Add(item);
                    }
                    else
                        ListWithOutTimeAndNoRepaired.Add(item);

                }


            }

            ListWithInTimeAndRepairedwithoutoverlap = listfilter(ListWithInTimeAndRepaired);//过滤重复的

            ListWithInTimeAndNoRepairedwithoutoverlap = listfilter(ListWithInTimeAndNoRepaired);//过滤重复的

            ListWithOutTimeAndRepairedwithoutoverlap = listfilter(ListWithOutTimeAndRepaired);

            ListWithOutTimeAndNoRepairedwithoutoverlap = listfilter(ListWithOutTimeAndNoRepaired);//过滤重复的





            //生成数据表输出
            DepartmentInfo dinfo = new DepartmentInfo();
            //dinfo.CompanyID = UserData.CurrentUserData.CompanyID;
            dinfo.DepartmentType = DepartmentType.MaintainTeamOther;
            IList<DepartmentInfo> teams = departmentBll.Search(dinfo);

            //数据表的总列数,故障性质(占两列)+维修单位+合计
            int columns = teams.Count + 3;
            StringBuilder table = new StringBuilder();

            table.Append("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"1\" bordercolor=\"#cccccc\" style=\"border-collapse: collapse;\">");
            //生成表头
            table.AppendFormat("<tr><td colspan=\"{0}\" class=\"Table_searchtitle\">故障性质</td>", 2);
            foreach (DepartmentInfo dept in teams)
            {
                table.AppendFormat("<td class=\"Table_searchtitle\">{0}</td>", dept.Name);
            }
            table.Append("<td class=\"Table_searchtitle\" style=\"width:100px;\">合计</td></tr>");

            //生成一般故障部分
           // table.AppendFormat("<tr><td rowspan=\"{0}\" align=\"center\" style=\"width:100px;\">一级故障</td><td align=\"center\"  style=\"width:100px;\">功能性恢复</td>", 6);
           // table.Append(GetOneRowHtml(list, teams, MalfunctionRank.Common, MalfunctionRestoreType.FunctionalityRestore) + "</tr>");

            table.AppendFormat("<tr><td rowspan=\"{0}\" align=\"center\" style=\"width:100px;\">一级故障</td><td align=\"center\"  style=\"width:100px;\">已修复</td>", 4);
            table.Append(GetOneRowHtml(list, teams, MalfunctionRank.Common, MalfunctionRestoreType.Fixed) + "</tr>");

           // table.Append("<tr><td align=\"center\">占总故障比例</td>");
            //table.Append(GetOnePercentRowHtml(list, teams, MalfunctionRank.Common, MalfunctionRestoreType.FunctionalityRestore) + "</tr>");

            //table.Append("<tr><td align=\"center\">已恢复</td>");
            //table.Append(GetOneRowHtml(list, teams, MalfunctionRank.Common, MalfunctionRestoreType.Fixed) + "</tr>");

            table.Append("<tr><td align=\"center\">占总故障比例</td>");
            table.Append(GetOnePercentRowHtml(list, teams, MalfunctionRank.Common, MalfunctionRestoreType.Fixed) + "</tr>");

            table.Append("<tr><td align=\"center\">未修复</td>");
            table.Append(GetOneRowHtml(list, teams, MalfunctionRank.Common, MalfunctionRestoreType.UnFixed) + "</tr>");

            table.Append("<tr><td align=\"center\">占总故障比例</td>");
            table.Append(GetOnePercentRowHtml(list, teams, MalfunctionRank.Common, MalfunctionRestoreType.UnFixed) + "</tr>");


            //生成二级故障部分
            table.AppendFormat("<tr><td rowspan=\"{0}\" align=\"center\">二级故障</td><td align=\"center\">已修复</td>", 4);
            table.Append(GetOneRowHtml(list, teams, MalfunctionRank.Important, MalfunctionRestoreType.Fixed) + "</tr>");

            //table.Append("<tr><td align=\"center\">占总故障比例</td>");
            //table.Append(GetOnePercentRowHtml(list, teams, MalfunctionRank.Important, MalfunctionRestoreType.FunctionalityRestore) + "</tr>");

            //table.Append("<tr><td align=\"center\">已恢复</td>");
            //table.Append(GetOneRowHtml(list, teams, MalfunctionRank.Important, MalfunctionRestoreType.Fixed) + "</tr>");

            table.Append("<tr><td align=\"center\">占总故障比例</td>");
            table.Append(GetOnePercentRowHtml(list, teams, MalfunctionRank.Important, MalfunctionRestoreType.Fixed) + "</tr>");

            table.Append("<tr><td align=\"center\">未修复</td>");
            table.Append(GetOneRowHtml(list, teams, MalfunctionRank.Important, MalfunctionRestoreType.UnFixed) + "</tr>");

            table.Append("<tr><td align=\"center\">占总故障比例</td>");
            table.Append(GetOnePercentRowHtml(list, teams, MalfunctionRank.Important, MalfunctionRestoreType.UnFixed) + "</tr>");

            //生成三级故障部分
            table.AppendFormat("<tr><td rowspan=\"{0}\" align=\"center\">三级故障</td><td align=\"center\">已修复</td>", 4);
            table.Append(GetOneRowHtml(list, teams, MalfunctionRank.Urgent, MalfunctionRestoreType.Fixed) + "</tr>");

           // table.Append("<tr><td align=\"center\">占总故障比例</td>");
           // table.Append(GetOnePercentRowHtml(list, teams, MalfunctionRank.Urgent, MalfunctionRestoreType.FunctionalityRestore) + "</tr>");

           // table.Append("<tr><td align=\"center\">已恢复</td>");
           // table.Append(GetOneRowHtml(list, teams, MalfunctionRank.Urgent, MalfunctionRestoreType.Fixed) + "</tr>");

            table.Append("<tr><td align=\"center\">占总故障比例</td>");
            table.Append(GetOnePercentRowHtml(list, teams, MalfunctionRank.Urgent, MalfunctionRestoreType.Fixed) + "</tr>");

            table.Append("<tr><td align=\"center\">未修复</td>");
            table.Append(GetOneRowHtml(list, teams, MalfunctionRank.Urgent, MalfunctionRestoreType.UnFixed) + "</tr>");

            table.Append("<tr><td align=\"center\">占总故障比例</td>");
            table.Append(GetOnePercentRowHtml(list, teams, MalfunctionRank.Urgent, MalfunctionRestoreType.UnFixed) + "</tr>");

            //生成其他故障部分
            table.AppendFormat("<tr><td rowspan=\"{0}\" align=\"center\">其他故障</td><td align=\"center\">已修复</td>", 4);
            table.Append(GetOneRowHtml(list, teams, MalfunctionRank.Urgent, MalfunctionRestoreType.Fixed) + "</tr>");

           // table.Append("<tr><td align=\"center\">占总故障比例</td>");
           // table.Append(GetOnePercentRowHtml(list, teams, MalfunctionRank.Others, MalfunctionRestoreType.FunctionalityRestore) + "</tr>");

           // table.Append("<tr><td align=\"center\">已恢复</td>");
           // table.Append(GetOneRowHtml(list, teams, MalfunctionRank.Others, MalfunctionRestoreType.Fixed) + "</tr>");

            table.Append("<tr><td align=\"center\">占总故障比例</td>");
            table.Append(GetOnePercentRowHtml(list, teams, MalfunctionRank.Others, MalfunctionRestoreType.Fixed) + "</tr>");

            table.Append("<tr><td align=\"center\">未修复</td>");
            table.Append(GetOneRowHtml(list, teams, MalfunctionRank.Others, MalfunctionRestoreType.UnFixed) + "</tr>");

            table.Append("<tr><td align=\"center\">占总故障比例</td>");
            table.Append(GetOnePercentRowHtml(list, teams, MalfunctionRank.Others, MalfunctionRestoreType.UnFixed) + "</tr>");
            //生成总计部分
            table.Append("<tr><td colspan='2' align=\"center\">故障总数</td>");
            table.Append(GettotalHtml(list, teams) + "</tr>");

            table.Append("<tr><td colspan='2' align=\"center\">超期已修复数</td>");
            table.Append(GettotaldestroywithouttimeandreapairHtml(ListWithOutTimeAndRepairedwithoutoverlap, teams) + "</tr>");

            table.Append("<tr><td colspan='2' align=\"center\">超期未修复数</td>");
            table.Append(GettotaldestroyHtml(ListWithOutTimeAndNoRepairedwithoutoverlap, teams) + "</tr>");

            table.Append("<tr><td colspan='2' align=\"center\">及时已修复数</td>");
            table.Append(GettotalrepairedHtml(ListWithInTimeAndRepairedwithoutoverlap, teams) + "</tr>");

            table.Append("<tr><td colspan='2' align=\"center\">未过期未修复数</td>");
            table.Append(GettotalwithinnorepairedHtml(ListWithInTimeAndNoRepairedwithoutoverlap, teams) + "</tr>");

           // table.Append("<tr><td colspan='2' align=\"center\">前期未修复数</td>");
           // table.Append(GettotalBeforenorepairedHtml(ListBeforeNoRepaired, teams) + "</tr>");

           
            table.Append("<tr><td colspan='2' align=\"center\">及时修复率</td>");
            table.Append(GettotalrepairedratioHtml(ListWithInTimeAndRepairedwithoutoverlap, teams) + "</tr>");

          //  table.Append("<tr><td colspan='2' align=\"center\">平均超时天数</td>");
           // table.Append(GettotalrepairedratioHtml(ListWithOutTimeAndNoRepairedwithoutoverlap, teams) + "</tr>");

            table.Append("</table>");

            ltStatisticResult.Text = table.ToString();

            GetMalfunctionSheetList();

            TabContainer1.ActiveTabIndex = 1;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "统计时发生错误", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    private string GettotalHtml(IList statisticData, IList<DepartmentInfo> maintainTeams)
    {
        StringBuilder rowHtml = new StringBuilder();
        int total = 0;
        foreach (DepartmentInfo dept in maintainTeams)
        {
            int count = 0;
            foreach (MalfunctionStatisticInfo item in statisticData)
            {
                if (item.MaintainDept == dept.DepartmentID)
                    count += item.Count;
            }
            total += count;
            ViewState[dept.DepartmentID + "maintainnum"] = count;
            if (count != 0)
                rowHtml.AppendFormat("<td align=\"center\">{0}</td>", count);
            else rowHtml.Append("<td align=\"center\">/</td>");

        }
        ViewState["totalmaintainnum"] = total;
        if (total != 0)
            rowHtml.AppendFormat("<td align=\"center\">{0}</td>", total);
        else rowHtml.Append("<td align=\"center\">/</td>");
        return rowHtml.ToString();
    }

    private string GettotalrepairedratioHtml(IList statisticData, IList<DepartmentInfo> maintainTeams)
    {
        StringBuilder rowHtml = new StringBuilder();
        int total = 0;
        foreach (DepartmentInfo dept in maintainTeams)
        {
            int count = 0;
            foreach (MalfunctionStatisticInfo item in statisticData)
            {
                if (item.MaintainDept == dept.DepartmentID)
                    count += item.Count;
            }
            total += count;

            float WithInNoRepaired = ViewState[dept.DepartmentID + "WithInNoRepaired"] == null ? 0 : Convert.ToSingle(ViewState[dept.DepartmentID + "WithInNoRepaired"]);
            float maintainnum = ViewState[dept.DepartmentID + "maintainnum"] == null ? 0 : Convert.ToSingle(ViewState[dept.DepartmentID + "maintainnum"]);
            float BeforeNoRepaired = ViewState[dept.DepartmentID + "BeforeNoRepaired"] == null ? 0 : Convert.ToSingle(ViewState[dept.DepartmentID + "BeforeNoRepaired"]);


            //if (count != 0 && (maintainnum + BeforeNoRepaired - WithInNoRepaired) != 0)
            if (count != 0 && (maintainnum) != 0)

            {
                //float percent = (float)count / (maintainnum + BeforeNoRepaired - WithInNoRepaired);
                float percent = (float)count / (maintainnum);
                rowHtml.AppendFormat("<td align=\"center\">{0}%</td>", (percent * 100).ToString("0.##"));
            }
            else rowHtml.Append("<td align=\"center\">/</td>");

        }

        float totalWithInNoRepaired = ViewState["totalWithInNoRepaired"] == null ? 0 : Convert.ToSingle(ViewState["totalWithInNoRepaired"]);
        float totalmaintainnum = ViewState["totalmaintainnum"] == null ? 0 : Convert.ToSingle(ViewState["totalmaintainnum"]);
        float totalBeforeNoRepaired = ViewState["totalBeforeNoRepaired"] == null ? 0 : Convert.ToSingle(ViewState["totalBeforeNoRepaired"]);

        //if (total != 0 && (totalmaintainnum + totalBeforeNoRepaired - totalWithInNoRepaired) != 0)
        if (total != 0 && (totalmaintainnum) != 0)
        {
            //float percent = (float)total / (totalmaintainnum + totalBeforeNoRepaired - totalWithInNoRepaired);
            float percent = (float)total / (totalmaintainnum);
            rowHtml.AppendFormat("<td align=\"center\">{0}%</td>", (percent * 100).ToString("0.##"));
        }
        else rowHtml.Append("<td align=\"center\">/</td>");
        return rowHtml.ToString();
    }

    //平均超时天数6.16 By L



    private string GettotalrepairedHtml(IList statisticData, IList<DepartmentInfo> maintainTeams)
    {
        StringBuilder rowHtml = new StringBuilder();
        int total = 0;
        foreach (DepartmentInfo dept in maintainTeams)
        {
            int count = 0;
            foreach (MalfunctionStatisticInfo item in statisticData)
            {
                if (item.MaintainDept == dept.DepartmentID)
                    count += item.Count;
            }
            total += count;
            ViewState[dept.DepartmentID + "WithInRepaired"] = count;
            if (count != 0)
                rowHtml.AppendFormat("<td align=\"center\">{0}</td>", count);
            else rowHtml.Append("<td align=\"center\">/</td>");

        }

        ViewState["totalWithInRepaired"] = total;
        if (total != 0)
            rowHtml.AppendFormat("<td align=\"center\">{0}</td>", total);
        else rowHtml.Append("<td align=\"center\">/</td>");
        return rowHtml.ToString();
    }

    private string GettotalwithinnorepairedHtml(IList statisticData, IList<DepartmentInfo> maintainTeams)
    {
        StringBuilder rowHtml = new StringBuilder();
        int total = 0;
        foreach (DepartmentInfo dept in maintainTeams)
        {
            int count = 0;
            foreach (MalfunctionStatisticInfo item in statisticData)
            {
                if (item.MaintainDept == dept.DepartmentID)
                    count += item.Count;
            }
            total += count;
            ViewState[dept.DepartmentID + "WithInNoRepaired"] = count;
            if (count != 0)
                rowHtml.AppendFormat("<td align=\"center\">{0}</td>", count);
            else rowHtml.Append("<td align=\"center\">/</td>");

        }

        ViewState["totalWithInNoRepaired"] = total;
        if (total != 0)
            rowHtml.AppendFormat("<td align=\"center\">{0}</td>", total);
        else rowHtml.Append("<td align=\"center\">/</td>");
        return rowHtml.ToString();
    }

    private string GettotalBeforenorepairedHtml(IList statisticData, IList<DepartmentInfo> maintainTeams)
    {
        StringBuilder rowHtml = new StringBuilder();
        int total = 0;
        foreach (DepartmentInfo dept in maintainTeams)
        {
            int count = 0;
            foreach (MalfunctionStatisticInfo item in statisticData)
            {
                if (item.MaintainDept == dept.DepartmentID)
                    count += item.Count;
            }
            total += count;
            ViewState[dept.DepartmentID + "BeforeNoRepaired"] = count;
            if (count != 0)
                rowHtml.AppendFormat("<td align=\"center\">{0}</td>", count);
            else rowHtml.Append("<td align=\"center\">/</td>");

        }

        ViewState["totalBeforeNoRepaired"] = total;
        if (total != 0)
            rowHtml.AppendFormat("<td align=\"center\">{0}</td>", total);
        else rowHtml.Append("<td align=\"center\">/</td>");
        return rowHtml.ToString();
    }

    private string GettotaldestroyHtml(IList statisticData, IList<DepartmentInfo> maintainTeams)
    {
        StringBuilder rowHtml = new StringBuilder();
        int total = 0;
        foreach (DepartmentInfo dept in maintainTeams)
        {
            int count = 0;
            foreach (MalfunctionStatisticInfo item in statisticData)
            {
                if (item.MaintainDept == dept.DepartmentID)
                    count += item.Count;
            }
            total += count;
            ViewState[dept.DepartmentID + "WithOutNoRepaired"] = count;
            if (count != 0)
                rowHtml.AppendFormat("<td align=\"center\">{0}</td>", count);
            else rowHtml.Append("<td align=\"center\">/</td>");

        }

        ViewState["totalWithOutNoRepaired"] = total;
        if (total != 0)
            rowHtml.AppendFormat("<td align=\"center\">{0}</td>", total);
        else rowHtml.Append("<td align=\"center\">/</td>");
        return rowHtml.ToString();
    }

    private string GettotaldestroywithouttimeandreapairHtml(IList statisticData, IList<DepartmentInfo> maintainTeams)
    {
        StringBuilder rowHtml = new StringBuilder();
        int total = 0;
        foreach (DepartmentInfo dept in maintainTeams)
        {
            int count = 0;
            foreach (MalfunctionStatisticInfo item in statisticData)
            {
                if (item.MaintainDept == dept.DepartmentID)
                    count += item.Count;
            }
            total += count;

            if (count != 0)
                rowHtml.AppendFormat("<td align=\"center\">{0}</td>", count);
            else rowHtml.Append("<td align=\"center\">/</td>");

        }


        if (total != 0)
            rowHtml.AppendFormat("<td align=\"center\">{0}</td>", total);
        else rowHtml.Append("<td align=\"center\">/</td>");
        return rowHtml.ToString();
    }

    /// <summary>
    /// 获取统计结果表一行的html代码
    /// </summary>
    /// <param name="rank"></param>
    /// <param name="type"></param>
    /// <param name="maintainTeam"></param>
    /// <returns></returns>
    private string GetOneRowHtml(IList statisticData, IList<DepartmentInfo> maintainTeams, MalfunctionRank rank, MalfunctionRestoreType type)
    {
        StringBuilder rowHtml = new StringBuilder();
        int total = 0;
        foreach (DepartmentInfo dept in maintainTeams)
        {
            if (type != MalfunctionRestoreType.Unknown)
            {
                int count = (int)GetValueByTerm(statisticData, rank, type, dept.DepartmentID, false);
                total += count;
                if (count != 0)
                    rowHtml.AppendFormat("<td align=\"center\">{0}</td>", count);
                else rowHtml.Append("<td align=\"center\">/</td>");
            }
        }
        if (total != 0)
            rowHtml.AppendFormat("<td align=\"center\">{0}</td>", total);
        else rowHtml.Append("<td align=\"center\">/</td>");
        return rowHtml.ToString();
    }
    /// <summary>
    /// 获取统计结果表一行的html代码
    /// </summary>
    /// <param name="rank"></param>
    /// <param name="type"></param>
    /// <param name="maintainTeam"></param>
    /// <returns></returns>
    private string GetOnePercentRowHtml(IList statisticData, IList<DepartmentInfo> maintainTeams, MalfunctionRank rank, MalfunctionRestoreType type)
    {
        StringBuilder rowHtml = new StringBuilder();
        float total = 0;
        foreach (DepartmentInfo dept in maintainTeams)
        {
            if (type != MalfunctionRestoreType.Unknown)
            {
                float percent = GetValueByTerm(statisticData, rank, type, dept.DepartmentID, true);
                total += percent;
                if (percent != 0)
                    rowHtml.AppendFormat("<td align=\"center\">{0}%</td>", (percent * 100).ToString("0.##"));
                else rowHtml.Append("<td align=\"center\">/</td>");
            }
        }
        if (total != 0)
            rowHtml.AppendFormat("<td align=\"center\">{0}%</td>", (total * 100).ToString("0.##"));
        else rowHtml.Append("<td align=\"center\">/</td>");

        return rowHtml.ToString();
    }
    /// <summary>
    /// 根据条件获取相应的设备数量统计值或比例统计值
    /// </summary>
    /// <param name="statisticData"></param>
    /// <param name="rank"></param>
    /// <param name="type"></param>
    /// <param name="maintainTeam"></param>
    private float GetValueByTerm(IList statisticData, MalfunctionRank rank, MalfunctionRestoreType type, long maintainTeam, bool isPercent)
    {
        float value = 0;
        switch (type)
        {
            case MalfunctionRestoreType.Fixed:
                //已恢复
                foreach (MalfunctionStatisticInfo item in statisticData)
                {
                    if (rank == item.MalfunctionRank && maintainTeam == item.MaintainDept)
                        if (item.Status == MalfunctionHandleStatus.Fixed || item.Status == MalfunctionHandleStatus.Finished || item.Status==MalfunctionHandleStatus.Waiting4MoneyApproval)
                        {
                            if (!isPercent)
                                value += item.Count;
                            else value += item.Percent;
                            //break;
                        }
                }
                break;
            case MalfunctionRestoreType.FunctionalityRestore:
                //功能性恢复
                foreach (MalfunctionStatisticInfo item in statisticData)
                {
                    if (rank == item.MalfunctionRank && maintainTeam == item.MaintainDept)
                        if (item.Status == MalfunctionHandleStatus.FunctionalityRestore)
                        {
                            if (!isPercent)
                                value += item.Count;
                            else value += item.Percent;
                            //break;
                        }
                }
                break;
            case MalfunctionRestoreType.UnFixed:
                //未修复
                foreach (MalfunctionStatisticInfo item in statisticData)
                {
                    if (rank == item.MalfunctionRank && maintainTeam == item.MaintainDept)
                        if (item.Status != MalfunctionHandleStatus.FunctionalityRestore
                            && item.Status != MalfunctionHandleStatus.Fixed
                            && item.Status != MalfunctionHandleStatus.Finished
                            && item.Status != MalfunctionHandleStatus.Canceled
                            && item.Status != MalfunctionHandleStatus.Waiting4MoneyApproval)
                        {
                            if (!isPercent)
                                value += item.Count;
                            else value += item.Percent;
                            //break;
                        }
                }
                break;
        }
        return value;
    }
    /// <summary>
    /// 获取故障处理单列表
    /// </summary>
    private void GetMalfunctionSheetList()
    {
        try
        {
            lbErrorMsg.Text = "";

            MalfunctionSearchInfo term = new MalfunctionSearchInfo();

            term.DepartmentID = Convert.ToInt64(ddlDepartment.SelectedValue);
            term.SystemID = ddlSystem.SelectedValue;

            if (hdAddressID.Value.Trim() != "" && hdAddressID.Value.Trim() != "00")
                term.AddressCode = hdAddressID.Value.Trim();

            if (!string.IsNullOrEmpty(tbDateFrom.Text.Trim()))
                term.ReportDateFrom = Convert.ToDateTime(tbDateFrom.Text.Trim());

            if (!string.IsNullOrEmpty(tbDateTo.Text.Trim()))
                term.ReportDateTo = Convert.ToDateTime(tbDateTo.Text.Trim());

            term.RecordDept = Convert.ToInt64(ddlRecordDepartment.SelectedValue);

            term.MaintainDept = Convert.ToInt64(ddlMaintainTeam.SelectedValue);

            term.EStatus = Convert.ToInt32(ddlMaintainStatus.SelectedValue);

            term.MaintainSataff = MaintainStaff.Value;

            term.ECompanyID = DDLCompany.SelectedValue;

            IList list = malfunctionBll.GetMalfunctionSheets(term);

            foreach (MalfunctionHandleInfo item in list)
            {
                DateTime endtime = new DateTime();

                DateTime actualendtime = item.ReportDate.AddMinutes(item.ActualRepairTime);
                if (item.ActualRepairTime == 0)
                    actualendtime = DateTime.Now;

                switch ((int)item.RepairUnit)
                {
                    case 1:
                        endtime = item.ReportDate.AddMinutes(item.RepairTime);
                        break;
                    case 2:
                        endtime = item.ReportDate.AddHours(item.RepairTime);
                        break;
                    case 3:
                        endtime = item.ReportDate.AddDays(item.RepairTime);
                        break;
                    case 4:
                        endtime = item.ReportDate.AddMonths(item.RepairTime);
                        break;
                    case 5:
                        endtime = item.ReportDate.AddYears(item.RepairTime);
                        break;

                }

                if (IsWithInTime(actualendtime, endtime))
                {
                    item.IsInTime = true;
                }
                else
                {

                    item.IsInTime = false;
                }
            }

            ViewState["rptMalfunctionSheets"] = list;
            rptMalfunctionSheets.DataSource = list;
            rptMalfunctionSheets.DataBind();

            if (list == null || list.Count == 0)
                divSheets.Visible = false;
            else divSheets.Visible = true;
        }
        catch (Exception ex)
        {
            lbErrorMsg.Text = "获取故障处理单列表失败";
            divSheets.Visible = false;
            EventMessage.EventWriteLog(Msg_Type.Error, "获取故障处理单列表失败，原因：" + ex.Message + ",\r\nstack:" + ex.StackTrace);
        }
    }
    /// <summary>
    /// 统计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (DDLreporttype.SelectedValue.Equals("1"))
        {
            FillData();
        }
        else if (DDLreporttype.SelectedValue.Equals("2"))
        {
            FillData2();
        }

    }
    /// <summary>
    /// 获取统计条件
    /// </summary>
    /// <returns></returns>
    private MalfunctionStatisticTerm GetStatisticTerm()
    {
        MalfunctionStatisticTerm term = new MalfunctionStatisticTerm();

        term.DepartmentID = Convert.ToInt64(ddlDepartment.SelectedValue);
        term.SystemID = ddlSystem.SelectedValue;

        if (hdAddressID.Value.Trim() != "" && hdAddressID.Value.Trim() != "00")
            term.AddressCode = hdAddressID.Value.Trim();

        if (!string.IsNullOrEmpty(tbDateFrom.Text.Trim()))
            term.ReportDateFrom = Convert.ToDateTime(tbDateFrom.Text.Trim());

        if (!string.IsNullOrEmpty(tbDateTo.Text.Trim()))
            term.ReportDateTo = Convert.ToDateTime(tbDateTo.Text.Trim());

        //if (cbIsOneDept.Checked)
        //    term.RecordDepartment = UserData.CurrentUserData.DepartmentID;
        term.RecordDepartment = Convert.ToInt64(ddlRecordDepartment.SelectedValue);

        term.MaintainDept = Convert.ToInt64(ddlMaintainTeam.SelectedValue);

        term.Status = Convert.ToInt32(ddlMaintainStatus.SelectedValue);

        term.MaintainSataff = MaintainStaff.Value;

        term.ECompanyID = DDLCompany.SelectedValue;

        return term;
    }
    private int sheetCount = 0;
    /// <summary>
    /// 数据表数据绑定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptMalfunctionSheets_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            MalfunctionHandleInfo item = (MalfunctionHandleInfo)e.Item.DataItem;   //子模块

            Literal lt = (Literal)e.Item.FindControl("ltSheetNOTxt");
            Label lbStatus = (Label)e.Item.FindControl("lbStatus");
            Label lbisintime = (Label)e.Item.FindControl("lbisintime");

            if (lt != null)
            {
                lt.Text = string.Format("<a style=\"color: Blue\" href=\"{0}\">{1}</a>", string.Format("javascript:showPopWin('查看故障单','{0}Module/FM2E/MaintainManager/MalFunctionManager/MalfunctionReport/ViewMalfunctionSheet.aspx?id={1}&viewOnly=1',800, 430, null,true,true);", Page.ResolveUrl("~/"), item.SheetID), item.SheetNO);
            }
            if (lbStatus != null)
            {
                if (item.Status == MalfunctionHandleStatus.FunctionalityRestore)
                {
                    //功能性修复
                    lbStatus.ForeColor = System.Drawing.Color.SteelBlue;
                    lbStatus.Text = "功能性修复";
                }
                else if (item.Status == MalfunctionHandleStatus.Fixed || item.Status == MalfunctionHandleStatus.Finished || item.Status == MalfunctionHandleStatus.Waiting4MoneyApproval)
                {
                    //已修复
                    lbStatus.ForeColor = System.Drawing.Color.Green;
                    lbStatus.Text = "已修复";
                }
                else
                {
                    //未修复
                    lbStatus.ForeColor = System.Drawing.Color.Red;
                    lbStatus.Text = "未修复";
                }
            }
            if (item.IsInTime)
                lbisintime.Text = "未超期";
            else
                lbisintime.Text = "超期";
            sheetCount++;
        }
        else if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lbSheetCount = e.Item.FindControl("lbSheetCount") as Label;
            if (lbSheetCount != null)
            {
                lbSheetCount.Text = "故障单总数：" + sheetCount;
            }
        }
    }
    public string lostname(string Editreason)
    {
        string separatorStr = "@First@";
        if (Editreason != null)
        {
            if (!Editreason.Contains(separatorStr))
            {
                Editreason += " " + separatorStr + " " + separatorStr + " ";  //延迟+审批意见+验收意见
            }
        }
        else
        {
            Editreason += " " + separatorStr + " " + separatorStr + " ";  //延迟+审批意见+验收意见
        }
        string[] split = { separatorStr };
        string[] editreason = Editreason.Split(split, StringSplitOptions.None);
        string delayapply = editreason[0];
        string approvalrecord = editreason[1];
        string checkrecord = editreason[2];
        string[] aa = { "→" };
        string[] approvalrecordSplit = approvalrecord.Split(aa, StringSplitOptions.RemoveEmptyEntries);
        string[] checkrecordSplit = checkrecord.Split(aa, StringSplitOptions.RemoveEmptyEntries);
        if (approvalrecordSplit.Length > 1)
        {

            string[] bb = { "#" };
            string[] arsplitsplit = approvalrecordSplit[approvalrecordSplit.Length - 1].Split(bb, StringSplitOptions.None);
            return arsplitsplit[0];
        }
        else
            return "";
    }

    //导出
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string filepath = Server.MapPath("~/public") + "/" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".csv";
        FileStream fs = File.Create(filepath);
        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);

        if (ViewState["rptMalfunctionSheets"] != null)
        {
            sw.Write("故障处理单,");
            sw.Write("报障部门,");
            sw.Write("上报人,");
            sw.Write("故障记录部门,");
            sw.Write("故障描述,");
            sw.Write("报障时间,");
            sw.Write("维修单位,");
            sw.Write("处理状态");

            sw.Write("最后审批人,");
            sw.Write("所属系统,");
            sw.Write("故障等级,");
            sw.Write("修复时间,");
            sw.Write("故障设备,");
            sw.Write("故障原因,");
            sw.Write("是否申请延时,");
            sw.Write("申请延时时间,");
            sw.Write("是否申请计量,");
            sw.Write("是否计量,");
            sw.Write("是否甲供,");
            sw.Write("计量总价,");
            sw.Write("审核总价,");
            sw.Write("配件相关小计,");
            sw.Write("配件相关审核小计,");
            sw.Write("措施费小计,");
            sw.Write("措施费审核小计,");
            sw.Write("配件名称,");
            sw.Write("配件型号,");
            sw.Write("配件数量,");
            sw.Write("配件单价,");
            sw.Write("配件审核单价,");
            sw.Write("配件备注");

            sw.Write("\r\n");
        }

        IList list = (ArrayList)ViewState["rptMalfunctionSheets"];

        foreach (MalfunctionHandleInfo info in list)
        {
            sw.Write(info.SheetNO + ",");
            sw.Write(info.DepartmentName + ",");
            sw.Write(info.Reporter + ",");
            sw.Write(info.RecordDeptName + ",");
            sw.Write(info.MalfunctionDescription + ",");
            sw.Write(info.ReportDate.ToString("yyyy-MM-dd") + ",");
            sw.Write(info.MaintainDeptName + ",");
            if (info.Status == MalfunctionHandleStatus.FunctionalityRestore)
            {
                //功能性修复
                sw.Write("功能性修复,");
            }
            else if (info.Status == MalfunctionHandleStatus.Fixed || info.Status == MalfunctionHandleStatus.Finished || info.Status == MalfunctionHandleStatus.Waiting4MoneyApproval)
            {
                //已修复
                sw.Write("已修复,");
            }
            else
            {
                //未修复
                sw.Write("未修复,");
            }

            sw.Write(lostname(info.Editreason) + ",");


            string eqmno = info.AddressDetail.Split('@')[0];
            if (eqmno != null && eqmno != "")
            {
                Equipment equipmentBll = new Equipment();
                EquipmentInfoFacade equipmentitem = equipmentBll.GetEquipmentBYNO(eqmno);
                if (equipmentitem != null)
                {
                    sw.Write(equipmentitem.SystemName + ",");
                }
                else
                {
                    sw.Write(eqmno + ",");

                }
            }
            else
            {
                sw.Write(" " + ",");
            }
            sw.Write(EnumHelper.GetDescription(info.MalfunctionRank) + ",");
            if (info.ActualRepairTime > 0)
            {
                sw.Write(info.ReportDate2.AddMinutes(info.ActualRepairTime).ToString("yyyy-MM-dd HH:mm") + ",");
            }
            else
            {
                sw.Write("未确认修复,");
            }
            sw.Write(info.AddressDetail.Split('@')[1] + ",");
            sw.Write(EnumHelper.GetDescription(info.SystemID) + ",");
            if (info.IsDelayApply == true)
            {
                sw.Write("是,");
            }
            else
            {
                sw.Write("否,");
            }
            sw.Write(info.DelayForTime + EnumHelper.GetDescription(info.DelayForUnit) + ",");
            EquipmentCostInfor ecInfor = ecBLL.GetEquipmentCostInforBySheetID(info.SheetID);
            if (ecInfor != null)
            {
                sw.Write(ecInfor.IsMeasure + ",");
                sw.Write(ecInfor.IsApplyMeasure + ",");
                sw.Write(ecInfor.IsProvider + ",");
                sw.Write(ecInfor.TotalSumCost + ",");
                sw.Write(ecInfor.TotalSumApprovalCost + ",");
                sw.Write(ecInfor.EqSumPrice + ",");
                sw.Write(ecInfor.EqSumApprovalPrice + ",");
                sw.Write(ecInfor.SumOtherCost + ",");
                sw.Write(ecInfor.SumApprovalOtherCost + ",");
            }
            else
            {
                sw.Write("" + ",");
                sw.Write("" + ",");
                sw.Write("" + ",");
                sw.Write("" + ",");
                sw.Write("" + ",");
                sw.Write("" + ",");
                sw.Write("" + ",");
                sw.Write("" + ",");
                sw.Write("" + ",");

            }

            if (ecInfor != null && ecInfor.EqCostItems != null)
            {
                foreach (EquipmentCostItems eqitem in ecInfor.EqCostItems)
                {
                    sw.Write(eqitem.EqName + ",");
                    sw.Write(eqitem.EqModel + ",");
                    sw.Write(eqitem.EqNum + ",");
                    sw.Write(eqitem.EqUnitPrice + ",");
                    sw.Write(eqitem.EqApprovalUnitPrice + ",");
                    sw.Write(eqitem.EqRemark + ",");

                }
            }
            sw.Write("\r\n");
        }

        sw.Flush();
        sw.Close();
        fs.Close();
        Response.ClearContent();
        Response.ClearHeaders();

        Response.ContentType = "application/vnd.ms-excel";

        Response.AddHeader("Content-Disposition", "inline;filename=" + HttpUtility.UrlEncode("故障报修统计结果导出.csv"));
        Response.WriteFile(filepath);//FileName为Excel文件所在地址

        Response.Flush();

        Response.Close();
        File.Delete(filepath);

        Response.End();
    }
}
