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



public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseApproval_QueryPrice : System.Web.UI.Page
{
    /// <summary>
    /// 价格管理逻辑处理类
    /// </summary>
    PriceManager priceBll = new PriceManager();

    /// <summary>
    /// 默认查询的产品名称
    /// </summary>
    string name = Microsoft.JScript.GlobalObject.unescape((string)Common.sink("name", MethodType.Get, 0, 0, DataType.Str));

    /// <summary>
    /// 默认查询的产品规格型号
    /// </summary>
    string model = Microsoft.JScript.GlobalObject.unescape((string)Common.sink("model", MethodType.Get, 0, 0, DataType.Str));

    /// <summary>
    /// 原来的申请价格
    /// </summary>
    decimal price = (decimal)Common.sink("price", MethodType.Get, 0, 0, DataType.Decimal);

    /// <summary>
    /// 原来申请的数量
    /// </summary>
    decimal count = (decimal)Common.sink("count", MethodType.Get, 0, 0, DataType.Decimal);
    
    /// <summary>
    /// 原来的项序号
    /// </summary>
    int itemid = (int)Common.sink("itemid", MethodType.Get, 0, 0, DataType.Int);

    /// <summary>
    /// 原来申请的单位
    /// </summary>
    string unit = Microsoft.JScript.GlobalObject.unescape((string)Common.sink("unit", MethodType.Get, 0, 0, DataType.Str));

    /// <summary>
    /// 是否可以调整
    /// </summary>
    string canadjust = (string)Common.sink("adjust", MethodType.Get, 0, 0, DataType.Str);

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

            //产品的名称、规格、单位、备注不能被修改
            TextBox_SelectedProductModel.Attributes.Add("readonly", "readonly");
            TextBox_SelectedProductName.Attributes.Add("readonly", "readonly");
            TextBox_Unit.Attributes.Add("readonly", "readonly");


            //默认查询出要求的结果
            TextBox_ProductName.Text = name;
            TextBox_Model.Text = model;

            //原来申请的信息
            
            TextBox_SelectedProductName.Text = name;
            TextBox_SelectedProductModel.Text = model;

            TextBox_Unit.Text = unit;
            TextBox_UnitPrice.Text = price.ToString("0.##");
            TextBox_Count.Text = count.ToString("0.##");
            TextBox_Amount.Text = (price * count).ToString("0.##");

            Hidden_ItemID.Value = itemid.ToString();

            FillData();
            TabContainer1.ActiveTabIndex = 1;//查询结果的Tab

            //根据是否可以被调整
            if (canadjust.ToLower() == "false")
            {
                Button_Adjust.Attributes.Add("disabled", "disable");
            }
            else
            {
                Button_Adjust.Attributes.Remove("disabled");
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
        FillData();
        TabContainer1.ActiveTabIndex= 1;//查询结果的Tab
    }

    /// <summary>
    /// 往数据表中填充
    /// </summary>
    private void FillData()
    {
        string productName =  Common.inSQL(TextBox_ProductName.Text.Trim());
        string productModel =  Common.inSQL(TextBox_Model.Text.Trim());
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

        AspNetPager1.RecordCount = recordCount;
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
    }
}
