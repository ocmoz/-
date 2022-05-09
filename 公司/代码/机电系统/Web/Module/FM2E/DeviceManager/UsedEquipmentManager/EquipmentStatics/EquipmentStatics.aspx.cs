using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections.Generic;
using FM2E.Model.Exceptions;
using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;
using FM2E.BLL.Basic;
using FM2E.BLL.Maintain;
using FM2E.Model.Maintain;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using FM2E.Model.System;

public partial class Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentStatics_EquipmentStatics : System.Web.UI.Page
{
    private readonly EquipmentStatistic statisticBll = new EquipmentStatistic();
    private readonly MalfunctionHandle handleBll = new MalfunctionHandle();
    /// <summary>
    /// 设备总数
    /// </summary>
    private int equipmentCount = 0;
    /// <summary>
    /// 故障设备数量
    /// </summary>
    private int faultyEquipmentCount = 0;
    /// <summary>
    /// 设备完好率
    /// </summary>
    private float rate = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
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
            //系统
            ddlSystem.Items.Clear();
            ddlSystem.Items.Add(new ListItem("不限", ""));
            ddlSystem.Items.AddRange(ListItemHelper.GetSystemListItems());

            //维修单位
            ddlMaintainTeam.Items.Clear();
            ddlMaintainTeam.Items.Add(new ListItem("不限", "0"));
            ddlMaintainTeam.Items.AddRange(ListItemHelper.GetAllMaintainTeams(loginUser.CompanyID));

            tbAddress.Attributes.Add("onclick", string.Format("javascript:showPopWin('地址选择','{0}Module/FM2E/BasicData/AddressManage/Address.aspx?operator=select',700, 400, RecordAddress,false,true);", Page.ResolveUrl("~")));

            //tbDateFrom.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + "01";
            tbDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 初始化GridView等数据
    /// </summary>
    private void FillData()
    {
        try
        {
            string companyid = DDLCompany.SelectedValue;
            Company item =new  Company();
            CompanyInfo company = item.GetCompany(companyid);
            if (company != null)
            {
                bool? i = company.IsParentCompany;
                if (i.HasValue == true)
                {
                    companyid = "";
                }
            
            }

            long mainteamid = Convert.ToInt64(ddlMaintainTeam.SelectedValue);

            string addressCode = hdAddressID.Value.Trim();
            if (addressCode == "00")
                addressCode = "";
            DateTime ReportDateFrom = DateTime.MinValue;
            DateTime ReportDateTo = DateTime.MaxValue;

            string systemID = ddlSystem.SelectedValue;
            //if (!string.IsNullOrEmpty(tbDateFrom.Text.Trim()))
            //    ReportDateFrom = Convert.ToDateTime(tbDateFrom.Text.Trim());
            ReportDateFrom = DateTime.MinValue;

            if (!string.IsNullOrEmpty(tbDateTo.Text.Trim()))
                ReportDateTo = Convert.ToDateTime(tbDateTo.Text.Trim());
            IList list = statisticBll.ComputeServiceabilityRate(companyid, mainteamid,addressCode, systemID, ReportDateFrom, ReportDateTo, out equipmentCount, out rate);
            faultyEquipmentCount = list.Count;
            GridView1.DataSource = list;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 查询事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillData();
    }
    /// <summary>
    /// 进行数据绑定时触发
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.BackColor = System.Drawing.Color.LightBlue;
            e.Row.Font.Bold = true;
            Label lbTotal = e.Row.FindControl("lbTotalCount") as Label;
            Label lbactualrepairedtimestr = e.Row.FindControl("lbactualrepairedtimestr") as Label;
            MaintainedEquipmentInfo dv = (MaintainedEquipmentInfo)e.Row.DataItem;

            if(dv!=null)
                lbactualrepairedtimestr.Text = dv.ActualRepairedTimestr;

            if (lbTotal != null)
            {
                lbTotal.Text = "设备总数：" + equipmentCount;
            }
            Label lbFaultyCount = e.Row.FindControl("lbFaultyCount") as Label;
            if (lbFaultyCount != null)
            {
                lbFaultyCount.Text = "故障设备总数：" + faultyEquipmentCount;
            }
            Label lbRate = e.Row.FindControl("lbRate") as Label;
            if (lbRate != null)
            {
                lbRate.Text = "设备完好率：" + (rate * 100).ToString("0.##") + "%";
            }

        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        string addressCode = hdAddressID.Value.Trim();
        if (addressCode == "00")
            addressCode = "";
        DateTime ReportDateFrom = DateTime.MinValue;
        DateTime ReportDateTo = DateTime.MaxValue;

        string systemID = ddlSystem.SelectedValue;

        string companyid = DDLCompany.SelectedValue;

        long mainteamid = Convert.ToInt64(ddlMaintainTeam.SelectedValue);
        //if (!string.IsNullOrEmpty(tbDateFrom.Text.Trim()))
        //    ReportDateFrom = Convert.ToDateTime(tbDateFrom.Text.Trim());

        ReportDateFrom = DateTime.MinValue;

        if (!string.IsNullOrEmpty(tbDateTo.Text.Trim()))
            ReportDateTo = Convert.ToDateTime(tbDateTo.Text.Trim());

        MalfunctionSearchInfo term = new MalfunctionSearchInfo();
        term.AddressCode = addressCode;
        term.SystemID = systemID;
        term.ReportDateFrom = ReportDateFrom;
        term.ReportDateTo = ReportDateTo;

        term.CompanyID = companyid;
        term.DepartmentID = mainteamid;

        IList maintainedequipmentlist = handleBll.GetMaintainedEquipmentCount(term);  //维修设备总列表

        IList repairedequipmentlist = handleBll.GetRepairedEquipmentCount(term); //故障待修设备总列表

        IList wait4repairequipmentlist = handleBll.GetWait4RepairedEquipmentCount(term);  //故障仍未修复设备列表

        IList allequipmentlist = handleBll.GetAllEquipmentCount(term);  //总设备列表

        //HashTable
        Hashtable allequipmentHt = new Hashtable(allequipmentlist.Count);
        foreach (ComputeMaintainedEquipmentRateInfo add in allequipmentlist)
        {
            //if (add.Model != null)
            //{
            //    allequipmentHt.Add(add.Name+"("+add.Model+")", add);
            //}
            //else
            //{
            if(!allequipmentHt.Contains(add.Name))
                allequipmentHt.Add(add.Name , add);
            //}
        }

        Hashtable repairedequipmentHt = new Hashtable(repairedequipmentlist.Count);
        foreach (ComputeMaintainedEquipmentRateInfo add in repairedequipmentlist)
        {
            //if (add.Model != null)
            //{
            //    repairedequipmentHt.Add(add.Name + "(" + add.Model + ")", add);
            //}
            //else
            //{
            if (!repairedequipmentHt.Contains(add.Name))
                repairedequipmentHt.Add(add.Name, add);
            //}
        }

        Hashtable wait4repairequipmentHt = new Hashtable(wait4repairequipmentlist.Count);
        foreach (ComputeMaintainedEquipmentRateInfo add in wait4repairequipmentlist)
        {
            //if (add.Model != null)
            //{
            //    wait4repairequipmentHt.Add(add.Name + "(" + add.Model + ")", add);
            //}
            //else
            //{
            if (!wait4repairequipmentHt.Contains(add.Name))
                wait4repairequipmentHt.Add(add.Name, add);
            //}
        }

        ArrayList ratelist = new ArrayList();

        int equipmentrateno = 0;

        foreach (ComputeMaintainedEquipmentRateInfo tempequipment in maintainedequipmentlist)
        {
            ComputeMaintainedEquipmentRateInfo tempallequipment = new ComputeMaintainedEquipmentRateInfo();
            ComputeMaintainedEquipmentRateInfo temprepairedequipment = new ComputeMaintainedEquipmentRateInfo();
            ComputeMaintainedEquipmentRateInfo tempwait4repairequipment = new ComputeMaintainedEquipmentRateInfo();

            MaintainedEquipmentRateInfo equipmentrateitem = new MaintainedEquipmentRateInfo();

            equipmentrateitem.MaintainedEquipmentCount = tempequipment.Count;
            equipmentrateitem.EquipmentNO = equipmentrateno;
            equipmentrateno++;

            if (tempequipment.Model != null && tempequipment.Model != "")
            {
                equipmentrateitem.EquipmentName = tempequipment.Name + "(" + tempequipment.Model + ")";

                tempallequipment = allequipmentHt[tempequipment.Name + "(" + tempequipment.Model + ")"] as ComputeMaintainedEquipmentRateInfo;
                if (tempallequipment == null)
                {
                    equipmentrateitem.AllEquipmentCount = 0;
                }
                else
                {
                    equipmentrateitem.AllEquipmentCount = tempallequipment.Count;
                }
                temprepairedequipment = repairedequipmentHt[tempequipment.Name + "(" + tempequipment.Model + ")"] as ComputeMaintainedEquipmentRateInfo;
                if (temprepairedequipment == null)
                {
                    equipmentrateitem.UnRepairedEquipmentCount = 0;
                }
                else
                {
                    equipmentrateitem.UnRepairedEquipmentCount = temprepairedequipment.Count;
                }
                tempwait4repairequipment = wait4repairequipmentHt[tempequipment.Name + "(" + tempequipment.Model + ")"] as ComputeMaintainedEquipmentRateInfo;
                if (tempwait4repairequipment == null)
                {
                    equipmentrateitem.Wait4RepairedEquipmentCount = 0;
                }
                else
                {
                    equipmentrateitem.Wait4RepairedEquipmentCount = tempwait4repairequipment.Count;
                }
            }
            else
            {
                equipmentrateitem.EquipmentName = tempequipment.Name;

                tempallequipment = allequipmentHt[tempequipment.Name] as ComputeMaintainedEquipmentRateInfo;
                if (tempallequipment == null)
                {
                    equipmentrateitem.AllEquipmentCount = 0;
                }
                else
                {
                    equipmentrateitem.AllEquipmentCount = tempallequipment.Count;
                }
                temprepairedequipment = repairedequipmentHt[tempequipment.Name] as ComputeMaintainedEquipmentRateInfo;
                if (temprepairedequipment == null)
                {
                    equipmentrateitem.UnRepairedEquipmentCount = 0;
                }
                else
                {
                    equipmentrateitem.UnRepairedEquipmentCount = temprepairedequipment.Count;
                }
                tempwait4repairequipment = wait4repairequipmentHt[tempequipment.Name] as ComputeMaintainedEquipmentRateInfo;
                if (tempwait4repairequipment == null)
                {
                    equipmentrateitem.Wait4RepairedEquipmentCount = 0;
                }
                else
                {
                    equipmentrateitem.Wait4RepairedEquipmentCount = tempwait4repairequipment.Count;
                }
            }

            equipmentrateitem.RepairedEquipmentCount = equipmentrateitem.MaintainedEquipmentCount - equipmentrateitem.UnRepairedEquipmentCount;
            equipmentrateitem.NormalEquipmentCount = equipmentrateitem.AllEquipmentCount - equipmentrateitem.Wait4RepairedEquipmentCount - equipmentrateitem.UnRepairedEquipmentCount;

            if (equipmentrateitem.AllEquipmentCount != 0)
            {
                equipmentrateitem.NormalEquipmentRate = equipmentrateitem.NormalEquipmentCount / Convert.ToDecimal(equipmentrateitem.AllEquipmentCount);
            }
            else
            {
                equipmentrateitem.NormalEquipmentRate = 1;
            }
            equipmentrateitem.Remark = "";

            ratelist.Add(equipmentrateitem);
        }

        string timeFile = "";
        try
        {
            int beginRow = 3;//开始复制的行

            //实例化一个Excel助手工具类
            timeFile = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            string fileName = "设备完好率导出" + timeFile;
            ExcelHelper ex = new ExcelHelper(Request.PhysicalApplicationPath + "\\public\\ComputeServiceabilityRate\\ComputeServiceabilityRate.xls", Server.MapPath("~/public/temp") + "/" + timeFile);

            ex.DeviceRateListToExcel(ratelist, beginRow, 1);

            ex.SetRangeBordStyle1(1, 1, 1 + ratelist.Count, 15);

            ex.OutputExcelFile();
            GC.Collect();
            //发送到客户端
            Response.ClearContent();

            Response.ClearHeaders();

            Response.ContentType = "application/vnd.ms-excel";

            Response.AddHeader("Content-Disposition", "inline;filename=" + HttpUtility.UrlEncode(fileName) + "");

            Response.WriteFile(Server.MapPath("~/public/temp") + "/" + timeFile);//FileName为Excel文件所在地址

            Response.Flush();

            Response.Close();

            System.IO.File.Delete(Server.MapPath("~/public/temp") + "/" + timeFile);

            Response.End();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    

}
