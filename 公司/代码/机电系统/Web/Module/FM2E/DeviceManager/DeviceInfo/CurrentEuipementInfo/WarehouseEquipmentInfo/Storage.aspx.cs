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

public partial class Module_FM2E_DeviceManager_DeviceInfo_CurrentEuipementInfo_WarehouseEquipmentInfo_Storage : System.Web.UI.Page
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
            hidden01.Visible = false;

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
        
        TabContainer1.ActiveTabIndex = 1;//查询结果的Tab
    }

    /// <summary>
    /// 往数据表中填充
    /// </summary>
    private void FillData1()
    {
        string productName = Common.inSQL(TextBox_ProductName.Text.Trim());
        string productModel = Common.inSQL(TextBox_Model.Text.Trim());
        string companyid = UserData.CurrentUserData.CompanyID;
        string warehouseid = DDLWarehouse.SelectedValue;
        int warming = 0;
        hidden01.Visible = false;
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
            out recordCount, companyid, productName, productModel, warehouseid);
        GridView_ResultDevice.DataSource = Result;
        GridView_ResultDevice.DataBind();

        AspNetPager1.RecordCount = recordCount;
        TabPanel_ResultDevice.HeaderText = "查询结果--设备(" + recordCount + ")";

    }

    /// <summary>
    /// 往数据表中填充
    /// </summary>
    


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
    
    /// <summary>
    /// 绑定列数据（设备，非易耗品）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_StorageList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            EquipmentStorageInfo dv = (EquipmentStorageInfo)e.Row.DataItem;
            string equipmentname = dv.EquipmentName;
            string equipmentmodel = dv.EquipmentModel;
            string equipmentnum = dv.Storage.ToString();
            string equipmentunit = dv.Unit;
            int equipmentwarming = dv.Warming;
            CheckBox cb = (CheckBox)e.Row.FindControl("checkBox1");
            if (cb != null)
                cb.Attributes.Add("onclick", "onClientClick(this,'" + equipmentname + "','" + equipmentmodel + "','" + equipmentnum + "','" + equipmentunit + "')");
            //检查已经选择的设备是否已经选中
            if (SelectedValue.Value.Length > 0)
            {
                if (SelectedValue.Value.Split(',')[0].Equals(equipmentname))
                    cb.Checked = true;
                else
                    cb.Checked = false;
            }
        }
    }
}
