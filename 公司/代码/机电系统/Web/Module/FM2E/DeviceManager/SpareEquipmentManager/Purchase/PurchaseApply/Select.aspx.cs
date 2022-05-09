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
using FM2E.BLL.Basic;

using WebUtility;
using WebUtility.Components;



public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseApply_Select : System.Web.UI.Page
{
    /// <summary>
    /// 价格管理逻辑处理类
    /// </summary>
    PriceManager priceBll = new PriceManager();


    EquipmentSystem eqmtsysBll = new EquipmentSystem();
    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //控件的事件
            TextBox_Amount.Attributes.Add("readonly", "readonly");
            TextBox_Count.Attributes.Add("onchange", "javascript:onCountChange()");
            TextBox_UnitPrice.Attributes.Add("onchange", "javascript:onCountChange()");
            Button_Query.Attributes.Add("onclick", "javascript:causeValidate = false;");
            Button_Query.Attributes.Add("onmouseover", "javascript:causeValidate = false;");
            Button_Query.Attributes.Add("onmouseout", "javascript:causeValidate = true;");

            //单位
            DropDownList_Unit.DataSource = Constants.GetUnits();
            DropDownList_Unit.DataBind();

            DropDownList_Unit.Items.Insert(0, new ListItem("--请选择单位--", ""));

            DropDownList_System.Items.AddRange(eqmtsysBll.GenerateListItemCollectionWithBlank());


            //默认查询出所有结果
            FillData();
        }
    }

    /// <summary>
    /// 根据输入条件查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Query_Click(object sender, EventArgs e)
    {
        FillData();
        TabContainer1.ActiveTabIndex= 1;//查询结果的Tab
    }

    /// <summary>
    /// 往数据表中填充
    /// </summary>
    private void FillData()
    {
        string productName = TextBox_ProductName.Text.Trim();
        string productModel = TextBox_Model.Text.Trim();
        string companyid = UserData.CurrentUserData.CompanyID;

        //查询指导价格信息
        PriceDetailSearchInfo queryInfo = new PriceDetailSearchInfo();

        queryInfo.CompanyID = companyid;
        queryInfo.ProductName = productName;
        queryInfo.Model = productModel;

        QueryParam qp = priceBll.GeneratePriceDetailSearchTerm(queryInfo);

        qp.PageIndex = AspNetPager1.CurrentPageIndex;
        qp.PageSize = AspNetPager1.PageSize;

        int recordCount = 0;
        IList Result = priceBll.GetPriceDetailList(qp, out recordCount, companyid);

        GridView_Result.DataSource = Result;
        GridView_Result.DataBind();

        //如果查询出来的记录数为0，则需要自动按照查询条件填充，否则清空原来的box
        ClearInputBox();
        if (recordCount == 0)
        {
            TextBox_SelectedProductName.Text = productName;
            TextBox_SelectedProductModel.Text = productModel;
        }

        AspNetPager1.RecordCount = recordCount;
    }

    /// <summary>
    /// 清空输入框
    /// </summary>
    private void ClearInputBox()
    {
        TextBox_Amount.Text = "";
        TextBox_Count.Text = "";
        TextBox_SelectedProductModel.Text = "";
        TextBox_SelectedProductName.Text = "";
        DropDownList_Unit.SelectedIndex = 0;
        TextBox_UnitPrice.Text = "";
        TextBox_Remark.Text = "";
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

        FillData();
        TabContainer1.ActiveTabIndex = 1;//查询结果的Tab
    }
}
