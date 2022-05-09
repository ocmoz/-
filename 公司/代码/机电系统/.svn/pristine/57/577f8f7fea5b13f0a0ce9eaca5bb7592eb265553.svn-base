using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WebUtility;
using FM2E.Model.Utils;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using WebUtility.Components;
using CrystalDecisions.CrystalReports.Engine;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_PriceManager_PriceMaintenance_PurchaseHistory : System.Web.UI.Page
{
    ReportDocument reportdocument = new ReportDocument();
    string companyid = UserData.CurrentUserData.CompanyID;
    string productname = Microsoft.JScript.GlobalObject.unescape(HttpContext.Current.Request.QueryString["ProductName"]);
    string model = (string)Common.sink("Model", MethodType.Get, 50, 0, DataType.Str);
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            FillData();
        }
    }
    private void Page_Unload(object sender, EventArgs e)
    {
        reportdocument.Dispose();
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
    /// 列表绑定事件
    /// </summary>
    private void FillData()
    {
        try
        {
            /**列表**/
            PriceManager bll = new PriceManager();
            PricePurchaseHistoryInfo item = new PricePurchaseHistoryInfo();
            item.CompanyID = companyid;
            item.ProductName = productname;
            item.Model = model;
            QueryParam qp = bll.GeneratePurchasePriceHistorySearchTerm(item);
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;
            int recordCount = 0;
            IList list = bll.GetPurchasePriceHistoryList(qp, out recordCount, null);
            GridView1.DataSource = list;
            GridView1.DataBind();
            /**曲线图**/
            DataSet dataset = new DataSet();
            DataTable datatable = new DataTable("FM2E_PriceHistory");
            dataset.Tables.Add(datatable);
            DataColumn column;
            column = new DataColumn();
            column.ColumnName = "PurchaseTime";
            column.DataType = Type.GetType("System.DateTime");
            column.Unique = false;
            datatable.Columns.Add(column);
            column = new DataColumn();
            column.ColumnName = "ActualPrice";
            column.DataType = Type.GetType("System.Decimal");
            column.Unique = false;
            datatable.Columns.Add(column);
            foreach(PricePurchaseHistoryInfo PurchasePricemodel in list)
            {
                DataRow datarow;
                datarow = datatable.NewRow();
                datarow["PurchaseTime"] = PurchasePricemodel.PurchaseTime;
                datarow["ActualPrice"] = PurchasePricemodel.ActualPrice;
                datatable.Rows.Add(datarow);
            }
            reportdocument.Load(Server.MapPath("~") + "/report/PurchasePriceHistory/PurchasePriceHistory.rpt");
            reportdocument.SetDataSource(dataset);
            CrystalReportViewer1.ReportSource = reportdocument;
            CrystalReportViewer1.DataBind();

            AspNetPager1.RecordCount = recordCount;

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "当前历史实际购买价格初始化失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 列表行事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    /// <summary>
    /// 列表绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    
}
