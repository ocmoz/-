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
using FM2E.Model.Equipment;
using WebUtility.Components;
using FM2E.BLL.Equipment;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_PriceManager_PriceMaintenance_SelectPrice : System.Web.UI.Page
{
    string companyid = UserData.CurrentUserData.CompanyID;
    protected void Page_Load(object sender, EventArgs e)
    {
        FillData1();
    }
    /// <summary>
    ///  列表绑定数据源
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
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData1();

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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";
            PriceDetailInfo item = (PriceDetailInfo)e.Row.DataItem;
            CheckBox cb = (CheckBox)e.Row.FindControl("checkBox1");
            if (cb != null)
                cb.Attributes.Add("onclick", "onClientClick('" + cb.ClientID +"','"+ companyid + "','" + item.ProductName + "','" + item.Model + "')");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
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

        }
        PriceManager bll = new PriceManager();
        QueryParam qp = bll.GeneratePriceDetailSearchTerm(pricedetail);
        ViewState["SearchTerm1"] = qp;
        FillData1();
        TabContainer1.ActiveTabIndex = 0;
    }


    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    //ClientScriptManager cs = Page.ClientScript;
    //    //cs.RegisterStartupScript(typeof(string), "returnaddstring", "<script language='javascript'>window.returnVal='" + addstring.Value + "';window.parent.hidePopWin(true);alert('a');(</script>");
    //   // ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "returnaddstring", "window.returnVal='" + addstring.Value + "';window.parent.hidePopWin(true);", true); 
    //}


}
