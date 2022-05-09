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
using WebUtility.WebControls;
using FM2E.Model.Utils;
using FM2E.BLL.Equipment;
using WebUtility.Components;
using FM2E.Model.Equipment;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_PriceManager_PriceMaintenance_PriceDetail : System.Web.UI.Page
{
    string companyid = UserData.CurrentUserData.CompanyID;
    int tabindex = (int)Common.sink("tabindex", MethodType.Get, 50, 0, DataType.Int);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonBind();
            FillData1();
            FillData2();
            FillData3();
            PermissionControl();
        }
        if (tabindex == 2)
            TabContainer1.ActiveTabIndex = tabindex - 1;

    }

    private void PermissionControl()
    {
        if (SystemPermission.CheckPermission(PopedomType.Edit) || SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
        else GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;



    }

    protected string ShowEndTime
    {
        get
        {
            if (historyorcurrent.SelectedIndex == 0)
                return "none";
            else
                return "";
        }
    }
    /// <summary>
    ///  当前指导价列表绑定数据源
    /// </summary>
    /// 
    private void FillData1()
    {
        try
        {
            QueryParam qp = (QueryParam)ViewState["SearchTerm1"];
            if (qp == null)
            {
                qp = new QueryParam();
            }
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;
            PriceManager bll = new PriceManager();
            int recordCount = 0;
            IList list = bll.GetPriceDetailList(qp, out recordCount, companyid);
            GridView1.DataSource = list;
            GridView1.DataBind();


            AspNetPager1.RecordCount = recordCount;

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "当前指导价格初始化失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 历史指导价列表绑定数据源
    /// </summary>
    private void FillData2()
    {
        try
        {
            QueryParam qp = (QueryParam)ViewState["SearchTerm2"];
            if (qp == null)
            {
                qp = new QueryParam();
            }
            qp.PageIndex = AspNetPager2.CurrentPageIndex;
            qp.PageSize = AspNetPager2.PageSize;
            PriceManager bll = new PriceManager();
            int recordCount = 0;
            IList list = bll.GetPriceHistoryList(qp, out recordCount, companyid);
            GridView2.DataSource = list;
            GridView2.DataBind();


            AspNetPager2.RecordCount = recordCount;

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "历史指导价格初始化失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }

    }
    /// <summary>
    /// 申请审批列表绑定指导价
    /// </summary>
    private void FillData3()
    {
        try
        {
           

            PriceManager bll = new PriceManager();

            PriceApplySearchInfo info = new PriceApplySearchInfo();
            info.CompanyID = UserData.CurrentUserData.CompanyID;
            info.Approvaler = TextBox_Approvaler.Text.Trim();
            info.Applicant = UserData.CurrentUserData.PersonName;
            try { info.ApplyTimeLower = DateTime.Parse(TextBox_ApplyTimeLower.Text.Trim()); }
            catch { }
            try { info.ApplyTimeUpper = DateTime.Parse(TextBox_ApplyTimeUpper.Text.Trim()); }
            catch { }
            try { info.ApprovalTimeLower = DateTime.Parse(TextBox_ApprovalTimeLower.Text.Trim()); }
            catch { }
            try { info.ApprovalTimeUpper = DateTime.Parse(TextBox_ApprovalTimeUpper.Text.Trim()); }
            catch { }
            try { info.Model = TextBox_Model.Text.Trim(); }
            catch { }
            try { info.ProductName = TextBox_ProductName.Text.Trim(); }
            catch { }
           
            int recordCount;
            IList list = bll.SearchPriceApplyForm(info, AspNetPager3.CurrentPageIndex, AspNetPager3.PageSize, out recordCount);

            GridView3.DataSource = list;
            GridView3.DataBind();
            

            AspNetPager3.RecordCount = recordCount;

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "申请审批信息初始化失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 按钮绑定
    /// </summary>
    private void ButtonBind()
    {
        HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[0];
        button.ButtonUrl += "?companyid=" + companyid;
    }
    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData1();
        TabContainer1.ActiveTabIndex = 0;
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        FillData2();
        TabContainer1.ActiveTabIndex = 1;
    }
    protected void AspNetPager3_PageChanged(object sender, EventArgs e)
    {
        FillData3();
        TabContainer1.ActiveTabIndex = 3;
    }
    /// <summary>
    /// 列表行事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "view")
        {
            string productname = gvRow.Attributes["ProductName"];
            string model = gvRow.Attributes["Model"];
            Response.Redirect("EditPrice.aspx?productname=" + Microsoft.JScript.GlobalObject.escape(productname) + "&model=" + model);
        }
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView3.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "view")
        {
            string applyformid = gvRow.Attributes["ApplyFormID"];
            Response.Redirect("PriceApply.aspx?cmd=view&id=" + applyformid);
        }
        
    }
    /// <summary>
    /// 列表绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";
            PriceDetailInfo dv = (PriceDetailInfo)e.Row.DataItem;
            e.Row.Attributes["ProductName"] = dv.ProductName;
            e.Row.Attributes["Model"] = dv.Model;
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";
        }
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            PriceApplyInfo dv = (PriceApplyInfo)e.Row.DataItem;
            e.Row.Attributes["ApplyFormID"] = dv.ApplyFormID.ToString();

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        if (historyorcurrent.SelectedValue == "1")
        {
            PriceDetailSearchInfo pricedetail = new PriceDetailSearchInfo();
            try
            {

                pricedetail.CompanyID = companyid;
                //if (ProductName.Text != string.Empty)
                pricedetail.ProductName = Common.inSQL(ProductName.Text.Trim());
                //if(Model.Text!=string.Empty)
                pricedetail.Model = Common.inSQL(Model.Text.Trim());
                if (StartTime1.Text != string.Empty)
                    pricedetail.StartTime1 = Convert.ToDateTime(StartTime1.Text.Trim());
                if (StartTime2.Text != string.Empty)
                    pricedetail.StartTime2 = Convert.ToDateTime(StartTime2.Text.Trim());
                //if (EndTime1.Text != string.Empty)
                //pricedetail.EndTime1 = Convert.ToDateTime(EndTime1.Text.Trim());
                //if (EndTime2.Text != string.Empty)
                //EndTime2.StartTime2 = Convert.ToDateTime(EndTime2.Text.Trim());
                if (UpperPrice.Text != string.Empty)
                    pricedetail.UpperPrice = Convert.ToDecimal(UpperPrice.Text.Trim());
                if (LowerPrice.Text != string.Empty)
                    pricedetail.LowerPrice = Convert.ToDecimal(LowerPrice.Text.Trim());
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入数据的格式不正确", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                return;
            }
            PriceManager bll = new PriceManager();
            QueryParam qp = bll.GeneratePriceDetailSearchTerm(pricedetail);
            ViewState["SearchTerm1"] = qp;
            FillData1();
            TabContainer1.ActiveTabIndex = 0;


        }
        else
        {
            PriceHistorySearchInfo pricehistory = new PriceHistorySearchInfo();
            try
            {
                pricehistory.CompanyID = companyid;
                pricehistory.ProductName = Common.inSQL(ProductName.Text.Trim());
                pricehistory.Model = Common.inSQL(Model.Text.Trim());
                if (StartTime1.Text != string.Empty)
                    pricehistory.StartTime1 = Convert.ToDateTime(StartTime1.Text.Trim());
                if (StartTime2.Text != string.Empty)
                    pricehistory.StartTime2 = Convert.ToDateTime(StartTime2.Text.Trim());
                if (EndTime1.Text != string.Empty)
                    pricehistory.EndTime1 = Convert.ToDateTime(EndTime1.Text.Trim());
                if (EndTime2.Text != string.Empty)
                    pricehistory.StartTime2 = Convert.ToDateTime(EndTime2.Text.Trim());
                if (UpperPrice.Text != string.Empty)
                    pricehistory.UpperPrice = Convert.ToDecimal(UpperPrice.Text.Trim());
                if (LowerPrice.Text != string.Empty)
                    pricehistory.LowerPrice = Convert.ToDecimal(LowerPrice.Text.Trim());

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入数据的格式不正确", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");

            }
            PriceManager bll = new PriceManager();
            QueryParam qp = bll.GeneratePriceHistorySearchTerm(pricehistory);
            ViewState["SearchTerm2"] = qp;
            FillData2();
            TabContainer1.ActiveTabIndex = 1;

        }
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "endtimedisplaychange", "selectchange();", true);

    }


    /// <summary>
    /// 查询申请审批
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_QueryApply_Click(object sender, EventArgs e)
    {
        FillData3();
        TabContainer1.ActiveTabIndex = 3;
    }

}
