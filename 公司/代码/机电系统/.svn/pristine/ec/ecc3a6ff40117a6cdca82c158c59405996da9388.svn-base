using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;
using FM2E.BLL.PendingOrder;
using FM2E.Model.PendingOrder;
using System.Collections;

public partial class Module_FM2E_PendingOrderMessage_History : System.Web.UI.Page
{
    /// <summary>
    /// 消息发送处理业务逻辑处理类
    /// </summary>
    PendingOrder poBll = new PendingOrder();


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            FillData();
        }
    }

    private void FillData()
    {
        try
        {
            int pageIndex = AspNetPager1.CurrentPageIndex;
            int listCount = 0;
            IList poList = poBll.GetDoneOrderListByReceiver(pageIndex, AspNetPager1.PageSize, out listCount, Common.Get_UserName);
            AspNetPager1.RecordCount = listCount;
            gridview_PendingOrderList.DataSource = poList;
            gridview_PendingOrderList.DataBind();
        }
        catch (Exception ex)
        {

            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "读取已办事务失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        if (ScriptManager1.IsInAsyncPostBack && !ScriptManager1.IsNavigating)
        {
            ScriptManager1.AddHistoryPoint("Index", AspNetPager1.CurrentPageIndex.ToString());
        }
        FillData();
    }
    protected void gridview_PendingOrderList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = gridview_PendingOrderList.Rows[Convert.ToInt32(e.CommandArgument)];
        long id = Convert.ToInt64(gvRow.Attributes["ID"]);
        string url = Convert.ToString(gvRow.Attributes["URL"]);
        try
        {
            if (e.CommandName == "link")
            {
                poBll.MarkRead(id, Common.Get_UserName);
                Response.Redirect(url, false);
            }
            else if (e.CommandName == "del")
            {
                poBll.DeletePendingOrder(id, Common.Get_UserName);
                FillData();
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "设置已办事务失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    protected void gridview_PendingOrderList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            //点击显示已办事务内容 
            PendingOrderReceiverCombineInfo info = e.Row.DataItem as PendingOrderReceiverCombineInfo;
            e.Row.Attributes["ID"] = info.ID.ToString();
            e.Row.Attributes["URL"] = info.URL.ToString();
            //e.Row.Attributes.Add( "OnClick" , String. Format( "javascript:showPopWin('已办事务内容','ViewPendingOrderContent.aspx?id={0}&type={1}&sendfrom={2}&time={3}&attachment={4}', 600, 350, null,true,true);" , info. ID , info. Type , info. SendFrom , info. SendTime. ToString( ) , info. URL ) );
            //           e. Row. Attributes. Add( "OnClick" , String. Format( "javascript:showPopWin('已办事务内容','ViewPendingOrderContent.aspx?id={0}&type={1}&sendfrom={2}&time={3}&attachment={4}', 600, 350, null,true,true);" , info. ID , info. Type , info. SendFrom , info. SendTime. ToString( ) , info. URL ) );
        }
    }

    /// <summary>
    /// 浏览器返回的时候
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ScriptManager1_Navigate(object sender, HistoryEventArgs e)
    {
        string indexString = e.State["Index"];
        if (string.IsNullOrEmpty(indexString))
        {
            AspNetPager1.CurrentPageIndex = 0;

        }
        else
        {
            int Index = Convert.ToInt32(indexString);
            AspNetPager1.CurrentPageIndex = Index;
        }
        FillData();
    }
}
