using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using WebUtility.Components;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using FM2E.Model.Utils;
using System.Collections;
using System;

public partial class Module_FM2E_DeviceManager_AssetManager_ScrapManager_ScrapRecord_ScrapRecord : System.Web.UI.Page
{
    private readonly Scrap scrapBll = new Scrap();
    public bool bShow = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            PermissionControl();
            //********** Modification Finished 2011-09-09 **********************************************************************************************
        }

        for (int i = 0; i < RadioButtonList1.Items.Count; i++)
        {
            RadioButtonList1.Items[i].Attributes.Add("onclick", "SetVisible(this)");
        }
        if (RadioButtonList1.SelectedIndex == 0)
            bShow = false;
        else bShow = true;
    }

    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
    private void PermissionControl()
    {
        GridView1.Columns[GridView1.Columns.Count - 1].Visible = SystemPermission.CheckButtonPermission(PopedomType.Edit);
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************

    private void FillData()
    {
        try
        {
            //获取查询条件
            QueryParam qp = (QueryParam)ViewState["SearchTerm"];
            if (qp == null)
            {
                
                ScrapApprovalSearchInfo term1 = new ScrapApprovalSearchInfo();
                term1.ApprovalResult = 1;
                qp = scrapBll.GenerateSearchTerm(term1);

                //获取已审批但未登记报废的申请列表
                qp.PageIndex = AspNetPager1.CurrentPageIndex;
                qp.PageSize = AspNetPager1.PageSize;

                int recordCount1 = 0;
                IList list1 = scrapBll.GetScrapApprovalHistory(qp, out recordCount1);
                GridView1.DataSource = list1;
                GridView1.DataBind();
                AspNetPager1.RecordCount = recordCount1;

                ScrapRecordSearchInfo term2 = new ScrapRecordSearchInfo();
                qp = scrapBll.GenerateSearchTerm(term2);
                //获取已报废设备的历史记录
                qp.PageIndex = AspNetPager2.CurrentPageIndex;
                qp.PageSize = AspNetPager2.PageSize;

                int recordCount2 = 0;
                IList list2 = scrapBll.GetScrapRecordList(qp, out recordCount2);
                GridView2.DataSource = list2;
                GridView2.DataBind();
                AspNetPager2.RecordCount = recordCount2;
                TabContainer1.ActiveTabIndex = 0;

            }
            else
            {
                if (RadioButtonList1.SelectedIndex == 0)
                {
                    //需要获取未审批的申请列表
                    qp.PageIndex = AspNetPager1.CurrentPageIndex;
                    qp.PageSize = AspNetPager1.PageSize;

                    int recordCount = 0;
                    IList list = scrapBll.GetScrapApprovalHistory(qp, out recordCount);
                    GridView1.DataSource = list;
                    GridView1.DataBind();
                    AspNetPager1.RecordCount = recordCount;
                    TabContainer1.ActiveTabIndex = 0;
                }
                else
                {

                    //需要获取审批历史记录
                    qp.PageIndex = AspNetPager2.CurrentPageIndex;
                    qp.PageSize = AspNetPager2.PageSize;

                    int recordCount = 0;
                    IList list = scrapBll.GetScrapRecordList(qp, out recordCount);
                    GridView2.DataSource = list;
                    GridView2.DataBind();
                    AspNetPager2.RecordCount = recordCount;
                    TabContainer1.ActiveTabIndex = 1;

                }
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long ApplyID = Convert.ToInt64(gvRow.Attributes["ScrapID"]);

        if (e.CommandName == "record")
        {
            //查看
            Response.Redirect("Record.aspx?cmd=add&id=" + Convert.ToString(ApplyID));
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            ScrapApprovaFacadelInfo item = (ScrapApprovaFacadelInfo)e.Row.DataItem;
            e.Row.Attributes["ScrapID"] = Convert.ToString(item.ScrapID);
        }
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView2.Rows[Convert.ToInt32(e.CommandArgument)];
        long ApplyID = Convert.ToInt64(gvRow.Attributes["ScrapID"]);
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

            ScrapRecordInfo item = (ScrapRecordInfo)e.Row.DataItem;
            e.Row.Attributes["ScrapID"] = Convert.ToString(item.ScrapID);
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ScrapApprovalSearchInfo item1 = new ScrapApprovalSearchInfo();
        ScrapRecordSearchInfo item2 = new ScrapRecordSearchInfo();
        int searchObject = RadioButtonList1.SelectedIndex;
        item1.ApprovalResult = 1;
        try
        {
            item2.SheetNO = item1.SheetName = Common.inSQL(tbSheetNO.Text.Trim());
             if (tbScrapTimeFrom.Text.Trim() != string.Empty)
            {
                item2.ScrapTimeFrom = Convert.ToDateTime(tbScrapTimeFrom.Text.Trim());
            }
            if (tbScrapTimeTo.Text.Trim() != string.Empty)
            {
                item2.ScrapTimeTo= Convert.ToDateTime(tbScrapTimeTo.Text.Trim());
            }
            
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "参数不合法：" + ex.Message, ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        try
        {
            QueryParam qp = null;
            if (searchObject == 0)
                qp = scrapBll.GenerateSearchTerm(item1);
            else qp = scrapBll.GenerateSearchTerm(item2);

            ViewState["SearchTerm"] = qp;
            FillData();
            AspNetPager1.CurrentPageIndex = 1;
            AspNetPager2.CurrentPageIndex = 1;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
}
