using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FM2E.Model.Utils;
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;

using WebUtility;
using WebUtility.Components;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;



public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_OutWarehouse_OutWarehouseApply_QueryStorage : System.Web.UI.Page
{
    /// <summary>
    /// 设备管理逻辑处理类
    /// </summary>
    Equipment equipmentBll = new Equipment();

    /// <summary>
    /// 消耗品管理逻辑处理类
    /// </summary>
    Expendable expendableBll = new Expendable();

   
    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Warehouse wbll = new Warehouse();
            IList list = wbll.GetAllWarehouseByCompany(UserData.CurrentUserData.CompanyID);

            DDLWarehouse.Items.Clear();
            DDLWarehouse.Items.Add(new ListItem("请选择仓库", ""));
            foreach (WarehouseInfo item in list)
            {
                DDLWarehouse.Items.Add(new ListItem(item.Name, item.WareHouseID.ToString()));
            }
        }
    }

    /// <summary>
    /// 根据输入条件查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Query_Click(object sender, EventArgs e)
    {
        FillData1();
        FillData2();
        TabContainer1.ActiveTabIndex= 1;//查询结果的Tab
    }

    /// <summary>
    /// 往数据表中填充
    /// </summary>
    private void FillData1()
    {
        string productName = Common.inSQL( TextBox_ProductName.Text.Trim());
        string productModel =  Common.inSQL(TextBox_Model.Text.Trim());
        string companyid = UserData.CurrentUserData.CompanyID;
        string warehouseid = DDLWarehouse.SelectedValue;

        //查询库存信息
        //PriceDetailSearchInfo queryInfo = new PriceDetailSearchInfo();

        //queryInfo.CompanyID = companyid;
        //queryInfo.ProductName = productName;
        //queryInfo.Model = productModel;

        //QueryParam qp = priceBll.GeneratePriceDetailSearchTerm(queryInfo);

        //qp.PageIndex = AspNetPager1.CurrentPageIndex;
        //qp.PageSize = AspNetPager1.PageSize;

        int recordCount = 0;
        IList Result = equipmentBll.QueryStorage(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize,
            out recordCount, companyid, productName, productModel,warehouseid);
        GridView_ResultDevice.DataSource = Result;
        GridView_ResultDevice.DataBind();

        AspNetPager1.RecordCount = recordCount;
        TabPanel_ResultDevice.HeaderText = "查询结果--设备(" + recordCount + ")";

    }

    /// <summary>
    /// 往数据表中填充
    /// </summary>
    private void FillData2()
    {
        string productName = Common.inSQL(TextBox_ProductName.Text.Trim());
        string productModel = Common.inSQL(TextBox_Model.Text.Trim());
        string companyid = UserData.CurrentUserData.CompanyID;
        string warehouseid = DDLWarehouse.SelectedValue;
       
        int recordCountExpendable = 0;
        IList ResultExpendable = expendableBll.QueryStorage(AspNetPager2.CurrentPageIndex, AspNetPager2.PageSize,
            out recordCountExpendable, companyid, productName, productModel,warehouseid);
        GridView_ResultExpendable.DataSource = ResultExpendable;
        GridView_ResultExpendable.DataBind();

        AspNetPager2.RecordCount = recordCountExpendable;
        TabPanel_ResultExpendable.HeaderText = "查询结果--易耗品(" + recordCountExpendable + ")";

    }


    /// <summary>
    /// 把选中的结果添加到父页面中
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_AddItem_Click(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 换页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        //if (ScriptManager1.IsInAsyncPostBack && !ScriptManager1.IsNavigating)
        //{
        //    ScriptManager1.AddHistoryPoint("Index", AspNetPager1.CurrentPageIndex.ToString());
        //}
        FillData1();
        TabContainer1.ActiveTabIndex = 1;
    }

    /// <summary>
    /// 换页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        //if (ScriptManager1.IsInAsyncPostBack && !ScriptManager1.IsNavigating)
        //{
        //    ScriptManager1.AddHistoryPoint("Index", AspNetPager1.CurrentPageIndex.ToString());
        //}
        FillData2();
        TabContainer1.ActiveTabIndex = 2;
    }
}
