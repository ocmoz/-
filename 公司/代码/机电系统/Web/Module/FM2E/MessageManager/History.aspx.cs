using System;
using System. Collections. Generic;
using System. Web;
using System. Web. UI;
using System. Web. UI. WebControls;

using WebUtility;
using WebUtility. WebControls;
using WebUtility. Components;
using FM2E.BLL.Message;
using FM2E.Model.Message;
using System.Collections;

public partial class Module_FM2E_MessageManager_History : System.Web.UI.Page
{

    /// <summary>
    /// 消息发送处理业务逻辑处理类
    /// </summary>
    Message messageBll = new Message();

    protected void Page_Load( object sender , EventArgs e )
    {
        if ( !IsPostBack )
        {
            FillData();
        }
    }

    /// <summary>
    /// 设置列表鼠标悬停样式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridview_MessageList_RowDataBound( object sender , GridViewRowEventArgs e )
    {
        if ( e. Row. RowType == DataControlRowType. DataRow )
        {
            //鼠标移动到每项时颜色交替效果    
            e. Row. Attributes. Add( "OnMouseOut" , "this.style.backgroundColor='White';" );
            e. Row. Attributes. Add( "OnMouseOver" , "this.style.backgroundColor='#f7f7f7';" );

            //设置悬浮鼠标指针形状为"小手"    
            e. Row. Attributes[ "style" ] = "Cursor:hand";

           // //点击显示信息内容 
           // MessageInfo info = e. Row. DataItem as MessageInfo;
           //e. Row. Attributes. Add( "OnClick" , String. Format( "javascript:showPopWin('信息内容','ViewMessageContent.aspx?id={0}&type={1}&sendfrom={2}&time={3}&attachment={4}', 600, 350, null,true,true);" , info. ID , info. Type , info. SendFrom , info. MessageTime. ToString( ) , info. Attachment ) );
        }
    }

    /// <summary>
    /// 填充列表
    /// </summary>
    private void FillData()
    {
        try
        {
            int pageIndex = AspNetPager1.CurrentPageIndex;
            int listCount = 0;
            IList messageList = messageBll.GetMessageListBySender(pageIndex, AspNetPager1.PageSize, out listCount, Common.Get_UserName);
            AspNetPager1.RecordCount = listCount;
            gridview_MessageList.DataSource = messageList;
            gridview_MessageList.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "读取信息列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
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

    ///// <summary>
    ///// 浏览器返回的时候
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void ScriptManager1_Navigate(object sender, HistoryEventArgs e)
    //{
    //    string indexString = e.State["Index"];
    //    if (string.IsNullOrEmpty(indexString))
    //    {
    //        AspNetPager1.CurrentPageIndex = 0;

    //    }
    //    else
    //    {
    //        int Index = Convert.ToInt32(indexString);
    //        AspNetPager1.CurrentPageIndex = Index;
    //    }
    //    FillData();
    //}
}
