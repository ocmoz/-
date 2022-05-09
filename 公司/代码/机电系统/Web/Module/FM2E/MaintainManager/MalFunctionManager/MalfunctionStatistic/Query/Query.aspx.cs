using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility.Components;
using WebUtility;
using FM2E.Model.System;
using FM2E.Model.Maintain;
using FM2E.BLL.Maintain;
using System.Collections;
using System.IO;

using FM2E.Model.Basic;
using FM2E.BLL.System;

using FM2E.BLL.Utils;

using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using FM2E.BLL.Basic;
public partial class Module_FM2E_MaintainManager_MalFunctionManager_MalfunctionStatistic_Query_Query : System.Web.UI.Page
{
    private readonly MalfunctionHandle malfunctionBll = new MalfunctionHandle();
        private readonly Department deptBll = new Department();
    private readonly User userBll = new User();
    private readonly Company companyBll = new Company();
    private readonly Address addressBll = new Address();
    private readonly EquipmentSystem systemBll = new EquipmentSystem();
    private readonly EquipmentCost ecBLL = new EquipmentCost();
    //private MalfunctionHandleStatus status = MalfunctionHandleStatus.Unknown;
    private readonly Equipment EqBll = new Equipment();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
        }
    }
    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        try
        {
            LoginUserInfo loginUser = UserData.CurrentUserData;

            //故障部门
            ddlDepartment.Items.Clear();
            ddlDepartment.Items.Add(new ListItem("不限", "0"));
            ddlDepartment.Items.AddRange(ListItemHelper.GetDepartmentListItems(loginUser.CompanyID));

            //故障等级
            ListItem[] rankItems = EnumHelper.GetListItems(typeof(MalfunctionRank), (int)MalfunctionRank.Unknown);
            ddlRank.Items.Clear();
            ddlRank.Items.Add(new ListItem("不限", ((int)MalfunctionHandleStatus.Unknown).ToString()));
            ddlRank.Items.AddRange(rankItems);

            //维修单位
            ddlMaintainTeam.Items.Clear();
            ddlMaintainTeam.Items.Add(new ListItem("不限", "0"));
            ddlMaintainTeam.Items.AddRange(ListItemHelper.GetAllMaintainTeams(loginUser.CompanyID));
            //5-3 By L ****************************************************************************************************************

            //故障原因
            ListItem[] malReason = EnumHelper.GetListItems(typeof(MalfunctionReason),  (int)MalfunctionReason.Unknown);
            ddlMalReason.Items.Clear();
            ddlMalReason.Items.Add(new ListItem("不限", ((int)MalfunctionHandleStatus.Unknown).ToString()));
            ddlMalReason.Items.AddRange(malReason);

            //5-3 By L ****************************************************************************************************************
            //5-3 By L ****************************************************************************************************************

            //计量与否
            ListItem[] malMeasure = EnumHelper.GetListItems(typeof(MalfunctionMeasure), (int)MalfunctionMeasure.Unknown);
            ddlMalMeasure.Items.Clear();
            ddlMalMeasure.Items.Add(new ListItem("不限", ((int)MalfunctionMeasure.Unknown).ToString()));
            ddlMalMeasure.Items.AddRange(malMeasure);

            //5-3 By L ****************************************************************************************************************

            //系统
            ddlSystem.Items.Clear();
            ddlSystem.Items.Add(new ListItem("不限", ""));
            ddlSystem.Items.AddRange(ListItemHelper.GetSystemListItems());

            tbDateFrom.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + "01";
            tbDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");

            //表单状态
            ListItem[] statusItems1 = EnumHelper.GetListItems(typeof(MalfunctionHandleStatus), (int)MalfunctionHandleStatus.Unknown);
            ddlFilterStatus.Items.Clear();
            ddlFilterStatus.Items.Add(new ListItem("不限", ((int)MalfunctionHandleStatus.Unknown).ToString()));
            ddlFilterStatus.Items.AddRange(statusItems1);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 填充页面数据
    /// </summary>
    private void FillData()
    {
        try
        {
            //EquipmentMaintainRecordSearchInfo term = GetSearchTerm();
            //int recordCount = 0;
            //IList list = malfunctionBll.GetEquipmentMaintainRecords(term, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out recordCount);

            MalfunctionSearchInfo term = GetSearchTerm();

            //查询
            int recordCount = 0;
            IList list = malfunctionBll.GetMalfunctionList(term, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out recordCount);

            List<MalfunctionHandleInfo> mList = new List<MalfunctionHandleInfo>();
            foreach (MalfunctionHandleInfo item in list)
            {
                mList.Add(item);
            }

            list = mList.OrderBy(o => o.ReportDate).ToList();

            GridView1.DataSource = list;
            GridView1.DataBind();
            AspNetPager1.RecordCount = recordCount;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询时发生错误", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
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
        Editreason = Editreason.Replace(',', '，');
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

    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        FillData();
    }
    /// <summary>
    /// 获取查询条件
    /// </summary>
    /// <returns></returns>
    //private EquipmentMaintainRecordSearchInfo GetSearchTerm()
    private MalfunctionSearchInfo GetSearchTerm()
    {
        //EquipmentMaintainRecordSearchInfo term=new EquipmentMaintainRecordSearchInfo();
        MalfunctionSearchInfo term = new MalfunctionSearchInfo();
        if (!string.IsNullOrEmpty(tbSheetNO.Text.Trim()))
            term.SheetNO = Common.inSQL(tbSheetNO.Text.Trim());

        if (ddlDepartment.SelectedValue != "0")
            term.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
        //By L 5-2 屏蔽设备条形码查询，改为设备故障原因查询***********************************
        //if (!string.IsNullOrEmpty(tbEquipmentNO.Text.Trim()))
        //    term.EquipmentNO = Common.inSQL(tbEquipmentNO.Text.Trim());

        if (ddlMalReason.SelectedValue != "0")
            term.MalReason = Convert.ToInt32(ddlMalReason.SelectedValue);
        if (ddlMalMeasure.SelectedValue != "0")
            term.MalMeasure = Convert.ToInt32(ddlMalMeasure.SelectedValue);
        if (jiagongOrNot.SelectedValue != "0")//甲供查询
        {
            term.JiagongOrNot = jiagongOrNot.SelectedValue;
        }
        if (jiliangOrNot.SelectedValue!="0")//计量查询
        {
            term.JiliangOrNot = jiliangOrNot.SelectedValue;
        }
        //**************************************************************************
        if (!string.IsNullOrEmpty(tbEquipmentName.Text.Trim()))
            term.EquipmentNO = Common.inSQL(tbEquipmentName.Text.Trim());

        if (!string.IsNullOrEmpty(tbName.Text.Trim()))
            term.EquipmentName = Common.inSQL(tbName.Text.Trim());

        if (!string.IsNullOrEmpty(tbSpecification.Text.Trim()))
            term.EquipmentSpecification = Common.inSQL(tbSpecification.Text.Trim());

        if (!string.IsNullOrEmpty(tbModel.Text.Trim()))
            term.EquipmentModel = Common.inSQL(tbModel.Text.Trim());


        if (!string.IsNullOrEmpty(ddlSystem.SelectedValue))
            term.SystemID = ddlSystem.SelectedValue;

        if (ddlRank.SelectedValue != "0")
            term.MalfunctionRank = Convert.ToInt32(ddlRank.SelectedValue);

        if (ddlMaintainTeam.SelectedValue != "0")
            //term.MaintainTeam = Convert.ToInt32(ddlMaintainTeam.SelectedValue);
            term.MaintainDept = Convert.ToInt32(ddlMaintainTeam.SelectedValue);

        if (!string.IsNullOrEmpty(tbDateFrom.Text.Trim()))
            term.ReportDateFrom = Convert.ToDateTime(tbDateFrom.Text.Trim());
            //term.MaintainDateFrom = Convert.ToDateTime(tbDateFrom.Text.Trim());

        if (!string.IsNullOrEmpty(tbDateTo.Text.Trim()))
            term.ReportDateTo = Convert.ToDateTime(tbDateTo.Text.Trim());
            //term.MaintainDateTo = Convert.ToDateTime(tbDateTo.Text.Trim());

        //if (!string.IsNullOrEmpty(tbConfirmTimeFrom.Text.Trim()))
        //    term.ReportDateFrom2 = Convert.ToDateTime(tbConfirmTimeFrom.Text.Trim());

        //if (!string.IsNullOrEmpty(tbConfirmTimeTo.Text.Trim()))
        //    term.ReportDateTo2 = Convert.ToDateTime(tbConfirmTimeTo.Text.Trim());

        term.Status = Convert.ToInt32(ddlFilterStatus.SelectedValue);

        return term;
    }

    protected void btExport_Click(object sender, EventArgs e)
    {
        MalfunctionSearchInfo term = GetSearchTerm();
        int recordCount = 0;
        IList list = malfunctionBll.GetMalfunctionList(term, 1, int.MaxValue, out recordCount);

        //List<MalfunctionHandleInfo> mList = new List<MalfunctionHandleInfo>();
        //foreach (MalfunctionHandleInfo item in list)
        //{
        //    mList.Add(item);
        //}

        //list = mList.OrderBy(o => o.ReportDate).ToList();


        string filepath = Server.MapPath("~/public") + "/" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".csv";
        FileStream fs = File.Create(filepath);
        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);

        if (list != null)
        {
            sw.Write("故障处理单,");
            sw.Write("报障部门,");
            sw.Write("上报人,");
            sw.Write("故障记录部门,");
            sw.Write("路段,");
            sw.Write("小站点,");
            sw.Write("站名,");
            sw.Write("故障描述,");
            sw.Write("报障时间,");
            sw.Write("维修单位,"); 
            sw.Write("处理状态,");
            sw.Write("最后审批人,");
            sw.Write("所属系统,");
            sw.Write("故障设备,");
            sw.Write("故障等级,");
            sw.Write("修复时间,");
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
            //sw.Write("配件名称,");
            //sw.Write("配件型号,");
            //sw.Write("配件数量,");
            //sw.Write("配件单价,");
            //sw.Write("配件审核单价,");
            //sw.Write("配件备注,");
            sw.Write("处理状态,");           
            sw.Write("用时,");
            sw.Write("维修方法,");
           
            sw.Write("\r\n");
        
            foreach (MalfunctionHandleInfo info in list)
            {
                sw.Write(info.SheetNO + ",");
                sw.Write(info.DepartmentName + ",");
                sw.Write(info.Reporter + ",");

                sw.Write(info.RecordDeptName + ",");

                int addressLength = info.AddressName.Split(' ').Length;
                int i = 1;
                for (; i <= addressLength-1; i++)
                {
                    if (i <= 3)
                    {
                        sw.Write(info.AddressName.Split(' ')[i] + ",");
                    }
                }
                do
                {
                    sw.Write(" ,");
                    i++;
                }
                while (i < 3);

                //if (info.MalfunctionDescription.Contains(","))
                //{
                //    info.MalfunctionDescription = info.MalfunctionDescription.Replace(",", "，");
                //}

                if (info.MalfunctionDescription.Contains("\r\n"))
                {
                    info.MalfunctionDescription = info.MalfunctionDescription.Replace("\r\n", "|");
                }
                sw.Write(info.MalfunctionDescription + ",");
                sw.Write(info.ReportDate.ToString("yyyy-MM-dd hh:mm") + ",");
                sw.Write(info.MaintainDeptName + ",");

                sw.Write(EnumHelper.GetDescription(info.Status) + ",");

                sw.Write(lostname(info.Editreason) + ",");


                string eqmno = info.AddressDetail.Split('@')[0];
                if (eqmno != null && eqmno != "")
                {
                    Equipment equipmentBll = new Equipment();
                    EquipmentInfoFacade equipmentitem = equipmentBll.GetEquipmentBYNO(eqmno);
                    if (equipmentitem!=null)
                    {
                        sw.Write(equipmentitem.SystemName + ",");
                        sw.Write(equipmentitem.Name + ",");
                    }
                    else
                    {
                        sw.Write(eqmno + ",");
                        sw.Write(" ,"); 
                    }
                }
                else
                {
                    sw.Write(" " + ",");
                }
                sw.Write(EnumHelper.GetDescription(info.MalfunctionRank) + ",");
                if(info.ActualRepairTime>0)
                {
                sw.Write(info.ReportDate2.AddMinutes(info.ActualRepairTime).ToString("yyyy-MM-dd HH:mm") + ",");                    
                }
                else
                {
                sw.Write( "未确认修复,");
                }
                sw.Write(EnumHelper.GetDescription(info.SystemID) + ",");
                if(info.IsDelayApply==true)
                {
                    sw.Write( "是,");                   
                }
                else{
                    sw.Write( "否,");  
                }
                sw.Write( info.DelayForTime+EnumHelper.GetDescription(info.DelayForUnit)+","); 
                EquipmentCostInfor ecInfor = ecBLL.GetEquipmentCostInforBySheetID(info.SheetID);
                if (ecInfor!=null)
                {
                    sw.Write(ecInfor.IsApplyMeasure + ",");
                    sw.Write(ecInfor.IsMeasure  + ",");                   
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

                //if (ecInfor != null && ecInfor.EqCostItems != null)
                //{
                //    foreach(EquipmentCostItems eqitem in ecInfor.EqCostItems)
                //    {
                //         sw.Write( eqitem.EqName+",");  
                //         sw.Write( eqitem.EqModel+",");  
                //         sw.Write( eqitem.EqNum+",");  
                //         sw.Write( eqitem.EqUnitPrice+",");  
                //         sw.Write( eqitem.EqApprovalUnitPrice+",");  
                //         sw.Write( eqitem.EqRemark+",");  
                        
                //    }
                //}
                if (info.ResponseTimeInMinutes >= info.ActualRepairTime)
                {
                    sw.Write( "按时,");  
                }
                else
                {
                    sw.Write("超时,");
                }
              
                sw.Write(info.ActualRepairTimeString + ",");

                IList maintainHistory = malfunctionBll.GetMaintainHistory(info.SheetID);
                if (maintainHistory != null && maintainHistory.Count != 0)
                {
                    MalfuncitonMaintainInfo item = (MalfuncitonMaintainInfo)maintainHistory[maintainHistory.Count - 1];
                    if (item.MaintenanceMethod.Contains("\r\n"))
                    {
                        item.MaintenanceMethod = item.MaintenanceMethod.Replace("\r\n", " ");
                    }
                    if (item.MaintenanceMethod.Contains("\r"))
                    {
                        item.MaintenanceMethod = item.MaintenanceMethod.Replace("\r", " ");
                    }
                    if (item.MaintenanceMethod.Contains("\n"))
                    {
                        item.MaintenanceMethod = item.MaintenanceMethod.Replace("\n", " ");
                    }
                    sw.Write(item.MaintenanceMethod + ",");
                }
                else
                {
                    sw.Write(" ,");
                }
                sw.Write("\r\n");
            }

            sw.Flush();
            sw.Close();
            fs.Close();
            Response.ClearContent();
            Response.ClearHeaders();

            Response.ContentType = "application/vnd.ms-excel";

            Response.AddHeader("Content-Disposition", "inline;filename=" + HttpUtility.UrlEncode("故障记录查询结果导出.csv"));
            Response.WriteFile(filepath);//FileName为Excel文件所在地址

            Response.Flush();

            Response.Close();
            File.Delete(filepath);

            Response.End();
        }
        else
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "无数据导出", Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
}
