using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FM2E.BLL.Insurance;
using FM2E.BLL.System;
using FM2E.Model.Insurance;
using FM2E.Model.System;
using WebUtility;
using WebUtility.Components;

public partial class Module_FM2E_InsureManager_InsureInfoManager_Insurancelist : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
            PermissionControl();
        }
    }
    private void PermissionControl()
    {
        if (SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[7].Visible = true ;
        else GridView1.Columns[7].Visible = false ;

    }
    private void FillData()
    {
        try
        { 
            Module module = new Module();
           // int recordCount = 0;
          //  IList list = module.GetSubModuleByPage(Guid.Empty.ToString("N"), AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize,out recordCount);
            Insurance insuranceBll = new Insurance();
            //IList list = insuranceBll.GetAllInsurance();

            InsuranceSearchInfo term = GetSearchTerm();
            int recordCount = 0;
            IList list = insuranceBll.GetInsurance(term, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out recordCount);
            
            
            GridView1.DataSource = list;
            GridView1.DataBind();
            AspNetPager1.RecordCount = recordCount;

            ddl_search_type.Items.Clear();
            ddl_search_type.Items.Add(new ListItem("不限",""));
            ddl_search_type.Items.AddRange(EnumHelper.GetListItems(typeof(InsuranceType)));
            
            
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取保单信息失败" ,ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        string id = gvRow.Attributes["InsuranceId"];

        if (e.CommandName == "view")
        {
            Response.Redirect("InsuranceManager.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "del")
        {
            

            bool bSuccess = false;
            try
            {
                Insurance insuranceBll = new Insurance();
                insuranceBll.DelInsurance(id);
                bSuccess = true;
               // GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Visible = false;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除保单失败",ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除保单成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Insurancelist.aspx?"), UrlType.Href, "");
            }
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

            InsuranceInfo insuranceInfo = (InsuranceInfo)e.Row.DataItem;
            e.Row.Attributes["InsuranceId"] = Convert.ToString(insuranceInfo.InsuranceId);
        }  
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSearch_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        FillData();
    }

    /// <summary>
    /// 获取查询条件
    /// </summary>
    /// <returns></returns>
    private InsuranceSearchInfo GetSearchTerm()
    {
        InsuranceSearchInfo item = new InsuranceSearchInfo();
        item.InsuranceNo = tb_search_insuranceNo.Text.Trim();
        item.InsureTarget = tb_search_insureTarget.Text.Trim();
        if (!string.IsNullOrEmpty(tb_search_startDate.Text.Trim()))
        {
            item.StartDate = Convert.ToDateTime(tb_search_startDate.Text.Trim());
        }
        if (!string.IsNullOrEmpty(tb_search_endDate.Text.Trim()))
        {
            item.EndDate = Convert.ToDateTime(tb_search_endDate.Text.Trim());
        }
        string temp = ddl_search_type.SelectedValue;
        if (!string.IsNullOrEmpty(temp.Trim()))
        {
            item.InsuranceType = (InsuranceType) Convert.ToInt32(ddl_search_type.SelectedValue);
        }

        return item;
    }
}
