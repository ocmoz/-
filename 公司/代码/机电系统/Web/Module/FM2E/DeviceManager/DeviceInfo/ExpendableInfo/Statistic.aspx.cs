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
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using System.Collections;
using System.Text;
using FM2E.Model.Basic;
using FM2E.BLL.Basic;
using FM2E.Model.System;

public partial class Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_Statistic : System.Web.UI.Page
{
    private readonly MalfunctionHandle malfunctionBll = new MalfunctionHandle();
    private readonly Department departmentBll = new Department();
    private int intotalCount = 0;  //入库总量
    private int outtotalCount = 0;  //出库总量
    private decimal intotalFee = 0;  //入库总金额
    private decimal outtotalFee = 0;  //出库总金额
    private int sheetCount = 0; //列表总数
    private int yearsheetCount = 0; //年份统计数量

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AddTree(0, (TreeNode)null);
            TreeView1.ShowLines = true;
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

            //故障部门
            ddlDepartment.Items.Clear();
            ddlDepartment.Items.Add(new ListItem("不限", "0"));
            ddlDepartment.Items.AddRange(ListItemHelper.GetDepartmentListItems(loginUser.CompanyID));

            tbDateFrom.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + "01";
            tbDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");

            //年份列表
            DDLYear.Items.Clear();
            DDLYear.Items.Add(new ListItem("请选择年份", "0"));
            for (int i = 0; i < 14; i++)
            {
                int year = DateTime.Now.Year - 7 + i;
                DDLYear.Items.Add(new ListItem(year.ToString() + "年", year.ToString()));
            }
            DDLYear.SelectedValue = DateTime.Now.Year.ToString();

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
        intotalCount = 0;
        intotalFee = 0;
        outtotalCount = 0;
        outtotalFee = 0;
        try
        {
            Warehouse bllstaff = new Warehouse();
            WarehouseInfo warehouse = bllstaff.GetWarehouseByUserName(UserData.CurrentUserData.UserName);
            DateTime dateTo = Convert.ToDateTime(tbDateTo.Text.Trim());
            DateTime dateFrom = Convert.ToDateTime(tbDateFrom.Text.Trim());
            long categoryid = 0;

            if (CategoryName.Text != string.Empty)
            {
                categoryid = Convert.ToInt64(CategoryID.Text);
            }
            string companyid = UserData.CurrentUserData.CompanyID;
            if (UserData.CurrentUserData.UserName == "gongni")
            {
                companyid = "0";  //如果这样，不用加仓库的约束条件
            }

            //生成数据表输出


            //数据表的总列数
            StringBuilder table = new StringBuilder();

            table.Append("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"1\" bordercolor=\"#cccccc\" style=\"border-collapse: collapse;\">");
            //生成表头
            table.Append("<tr><td class=\"Table_searchtitle\">易耗品种类</td>");
            table.Append("<td class=\"Table_searchtitle\" >入库数量</td>");
            table.Append("<td class=\"Table_searchtitle\" >入库金额</td>");
            table.Append("<td class=\"Table_searchtitle\" >出库数量</td>");
            table.Append("<td class=\"Table_searchtitle\" >出库金额</td>");
            table.Append("<td class=\"Table_searchtitle\" style=\"width:100px;\">合计</td></tr>");

            //生成计划内物品部分
            table.Append("<tr><td align=\"center\" >计划内物品</td>");
            table.Append(GetOneRowHtml(companyid, warehouse.WareHouseID, "30", dateFrom, dateTo));
            table.Append("</tr>");

            //生成新员工生活用品部分
            table.Append("<tr><td align=\"center\">新员工生活用品</td>");
            table.Append(GetOneRowHtml(companyid, warehouse.WareHouseID, "29", dateFrom, dateTo));
            table.Append("</tr>");

            //生成额外办公用品部分
            table.Append("<tr><td align=\"center\">额外办公用品</td>");
            table.Append(GetOneRowHtml(companyid, warehouse.WareHouseID, "28", dateFrom, dateTo));
            table.Append("</tr>");

            //生成五金电料部分
            table.Append("<tr><td align=\"center\" >五金电料</td>");
            table.Append(GetOneRowHtml(companyid, warehouse.WareHouseID, "27", dateFrom, dateTo));
            table.Append("</tr>");

            //生成日常用品部分
            table.Append("<tr><td align=\"center\">日常用品</td>");
            table.Append(GetOneRowHtml(companyid, warehouse.WareHouseID, "26", dateFrom, dateTo));
            table.Append("</tr>");

            //生成办公用品部分
            table.Append("<tr><td align=\"center\">办公用品</td>");
            table.Append(GetOneRowHtml(companyid, warehouse.WareHouseID, "25", dateFrom, dateTo));
            table.Append("</tr>");

            //生成定额办公用品部分
            table.Append("<tr><td align=\"center\">定额办公用品</td>");
            table.Append(GetOneRowHtml(companyid, warehouse.WareHouseID, "24", dateFrom, dateTo));
            table.Append("</tr>");

            //生成合计部分
            table.Append("<tr><td align=\"center\">合计</td>");
            table.AppendFormat("<td align=\"center\">{0}</td>", intotalCount);
            table.AppendFormat("<td align=\"center\">{0}</td>", intotalFee);
            table.AppendFormat("<td align=\"center\">{0}</td>", outtotalCount);
            table.AppendFormat("<td align=\"center\">{0}</td>", outtotalFee);
            table.AppendFormat("<td align=\"center\">{0}</td>", intotalFee - outtotalFee);
            table.Append("</tr>");

            table.Append("</table>");

            ltStatisticResult.Text = table.ToString();

            GetMalfunctionSheetList(dateFrom, dateTo, companyid, warehouse.WareHouseID, categoryid);

            TabContainer1.ActiveTabIndex = 1;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "统计时发生错误", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 获取统计结果表一行的html代码
    /// </summary>
    /// <param name="rank"></param>
    /// <param name="type"></param>
    /// <param name="maintainTeam"></param>
    /// <returns></returns>
    private string GetOneRowHtml(string companyid, string warehouseid, string categoryid, DateTime datefrom, DateTime dateto)
    {
        StringBuilder rowHtml = new StringBuilder();
        Expendable epdbll=new Expendable();
        ExpendableStatisticsInfo item = epdbll.GetExpendableStaticticsData(companyid,warehouseid,categoryid,datefrom,dateto);
        rowHtml.AppendFormat("<td align=\"center\">{0}</td>", item.InCount);
        rowHtml.AppendFormat("<td align=\"center\">{0}</td>", item.InFee);
        rowHtml.AppendFormat("<td align=\"center\">{0}</td>", item.OutCount);
        rowHtml.AppendFormat("<td align=\"center\">{0}</td>", item.OutFee);
        rowHtml.AppendFormat("<td align=\"center\">{0}</td>", item.InFee-item.OutFee);
        intotalCount += item.InCount;
        intotalFee += item.InFee;
        outtotalCount += item.OutCount;
        outtotalFee += item.OutFee;
        return rowHtml.ToString();
    }

    /// <summary>
    /// 获取故障处理单列表
    /// </summary>
    private void GetMalfunctionSheetList(DateTime datefrom,DateTime dateto,String componyid ,String warehouseid,long categoryid)
    {
        try
        {
            lbErrorMsg.Text = "";

            ExpendableInOut exinoutBLL=new ExpendableInOut();

            IList list = exinoutBLL.GetExInOut(componyid,warehouseid,datefrom,dateto,categoryid);
            rptRxpendableSheets.DataSource = list;
            rptRxpendableSheets.DataBind();

            if (list == null || list.Count == 0)
                divSheets.Visible = false;
            else divSheets.Visible = true;
        }
        catch (Exception ex)
        {
            lbErrorMsg.Text = "获取易耗品列表失败";
            divSheets.Visible = false;
            EventMessage.EventWriteLog(Msg_Type.Error, "获取易耗品列表失败，原因：" + ex.Message + ",\r\nstack:" + ex.StackTrace);
        }
    }
    /// <summary>
    /// 统计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillData();
    }

    /// <summary>
    /// 年份统计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void yearstatic_Click(object sender, EventArgs e)
    {
        try
        {
            lbYeartitle.Text = "";
            Warehouse bllstaff = new Warehouse();
            WarehouseInfo warehouse = bllstaff.GetWarehouseByUserName(UserData.CurrentUserData.UserName);
            DateTime dateTo = DateTime.MinValue;
            DateTime dateFrom = DateTime.MaxValue;
            long categoryid = 0;
            if (DDLYear.SelectedValue != "0")
            {
                dateFrom = Convert.ToDateTime(DDLYear.SelectedValue + "-01-01 00:00:00");
                dateTo = Convert.ToDateTime(DDLYear.SelectedValue.ToString() + "-12-31 23:59:59");
                lbYeartitle.Text += DDLYear.SelectedItem;
            }
            else
            {
                lbYeartitle.Text += "所有年份";
            }
            if (CategoryName.Text != string.Empty)
            {
                categoryid = Convert.ToInt64(CategoryID.Text);
                lbYeartitle.Text += CategoryName.Text;
            }
            else
            {
                lbYeartitle.Text += "所有类型的易耗品";
            }
            string companyid = UserData.CurrentUserData.CompanyID;
            if (UserData.CurrentUserData.UserName == "gongni")
            {
                companyid = "0";  //如果这样，不用加仓库的约束条件
            }

            ExpendableInOut exinoutBLL = new ExpendableInOut();

            IList list = exinoutBLL.GetExInOutYear(companyid, warehouse.WareHouseID, dateFrom, dateTo, categoryid);
            rpExpendableYear.DataSource = list;
            rpExpendableYear.DataBind();
            TabContainer1.ActiveTabIndex = 2;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 数据表数据绑定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptRxpendableSheets_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            sheetCount++;
        }
        else if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lbSheetCount = e.Item.FindControl("lbSheetCount") as Label;
            if (lbSheetCount != null)
            {
                lbSheetCount.Text = "易耗品记录总数：" + sheetCount;
            }
        }
    }

    /// <summary>
    /// 数据表数据绑定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rpExpendableYear_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            yearsheetCount++;
        }
        else if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lbSheetCount = e.Item.FindControl("lbYearSheetCount") as Label;
            if (lbSheetCount != null)
            {
                lbSheetCount.Text = "易耗品记录总数：" + yearsheetCount;
            }
        }
    }




    /// <summary>
    /// 选择种类事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        CategoryID.Text = this.TreeView1.SelectedNode.Value;
        CategoryName.Text = this.TreeView1.SelectedNode.Text;
        Category bll = new Category();
        CategoryInfo categoryinfo = bll.GetCategory(Convert.ToInt32(TreeView1.SelectedNode.Value));
        PopupControlExtender2.Commit(CategoryName.Text);
        PopupControlExtender3.Commit(CategoryID.Text);
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "causeValidatescript", "causeValidate=true;", true);
    }

    /// <summary>
    /// 树的展开事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void TreeView1_OnTreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {
        if (e.Node.ChildNodes.Count == 0)
        {
            CategorysearchInfo categoryinfo = new CategorysearchInfo();
            categoryinfo.ParentID = Convert.ToInt64(e.Node.Value);
            Category bll = new Category();
            //QueryParam qp = bll.GenerateSearchTerm(categoryinfo);
            //qp.PageSize = 500;
            //int recordcount = 0;
            IList nodelist = (List<CategoryInfo>)bll.Search(categoryinfo);
            foreach (CategoryInfo item in nodelist)
            {
                TreeNode Node = new TreeNode();
                Node.Text = item.CategoryName;
                Node.Value = item.CategoryID.ToString();
                e.Node.ChildNodes.Add(Node);
                Node.PopulateOnDemand = true;
                Node.Expanded = false;
            }
        }
    }

    /// <summary>
    /// 初始化种类弹出树
    /// </summary>
    /// <param name="ParentID"></param>
    /// <param name="pNode"></param>
    public void AddTree(long ParentID, TreeNode pNode)
    {
        CategorysearchInfo categoryinfo = new CategorysearchInfo();
        categoryinfo.Level = 1;
        Category bll = new Category();
        //QueryParam qp = bll.GenerateSearchTerm(categoryinfo);
        //qp.PageSize = 500;
        //int recordcount = 0;
        IList nodelist = (List<CategoryInfo>)bll.Search(categoryinfo);
        foreach (CategoryInfo item in nodelist)
        {
            TreeNode Node = new TreeNode();
            Node.Text = item.CategoryName;
            Node.Value = item.CategoryID.ToString();
            TreeView1.Nodes.Add(Node);
            Node.PopulateOnDemand = true;
            Node.Expanded = false;
        }

    }

}
