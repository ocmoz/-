using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM2E.BLL.Equipment;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using WebUtility;
using WebUtility.Components;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using System.Collections;

public partial class Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_ReturnEquipment_ReturnEquipment : System.Web.UI.Page
{
    private readonly Secondment secondmentBll = new Secondment();
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 20, 0, DataType.Str);

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
        }
    }
    private void InitialPage()
    {
        try
        {
            //绑定公司到下拉列表
            Company companyBll = new Company();
            IList<CompanyInfo> companyList = companyBll.GetAllCompany();

            ddlReturnCompany.Items.Clear();
            ddlReturnCompany.Items.Add(new ListItem("不限", "0"));
            foreach (CompanyInfo item in companyList)
            {
                ddlReturnCompany.Items.Add(new ListItem(item.CompanyName, item.CompanyID));
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "页面初始化失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    private void FillData()
    {
        try
        {
            QueryParam qp = (QueryParam)ViewState["SearchTerm"];
            if (qp == null)
            {
                ReturnAcceptanceSearchInfo searchTerm = new ReturnAcceptanceSearchInfo();
                searchTerm.CompanyID = UserData.CurrentUserData.CompanyID;
                searchTerm.Result = 3;
                qp = secondmentBll.GenerateSearchTerm(searchTerm);
            }
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;

            //查询
            int recordCount = 0;
            IList list = secondmentBll.GetAcceptanceList(qp, out recordCount);
            GridView1.DataSource = list;
            GridView1.DataBind();
            AspNetPager1.RecordCount = recordCount;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long returnID = Convert.ToInt64(gvRow.Attributes["ReturnID"]);

        if (e.CommandName == "view")
        {
            Response.Redirect("ViewAcceptanceReocrd.aspx?cmd=view&id=" + returnID);
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

            ReturnAcceptanceInfo item = (ReturnAcceptanceInfo)e.Row.DataItem;
            e.Row.Attributes["ReturnID"] = item.ReturnID.ToString();
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        ReturnAcceptanceSearchInfo term = new ReturnAcceptanceSearchInfo();

        try
        {
            if (tbEquipmentNO.Text.Trim() != string.Empty)
                term.EquipmentNO = Common.inSQL(tbEquipmentNO.Text.Trim());

            if (tbEquipmentName.Text.Trim() != string.Empty)
                term.EquipmentName = Common.inSQL(tbEquipmentName.Text.Trim());

            if (tbSheetNO.Text.Trim() != string.Empty)
                term.SheetNO = Common.inSQL(tbSheetNO.Text.Trim());

            term.CompanyID = UserData.CurrentUserData.CompanyID;
            term.ReturnCompany = ddlReturnCompany.SelectedValue;
            term.Result = Convert.ToInt32(ddlResult.SelectedValue);

            if (tbCheckDateFrom.Text.Trim() != "")
                term.CheckDateFrom = Convert.ToDateTime(tbCheckDateFrom.Text.Trim());

            if (tbCheckDateTo.Text.Trim() != "")
                term.CheckDateTo = Convert.ToDateTime(tbCheckDateTo.Text.Trim());
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "参数不合法", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        try
        {
            QueryParam qp = secondmentBll.GenerateSearchTerm(term);
            ViewState["SearchTerm"] = qp;
            FillData();
            TabContainer1.ActiveTabIndex = 0;
            AspNetPager1.CurrentPageIndex = 1;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询出错", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

    }
}
