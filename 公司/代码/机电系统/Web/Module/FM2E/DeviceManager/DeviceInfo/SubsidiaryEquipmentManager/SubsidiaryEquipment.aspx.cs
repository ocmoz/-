using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using WebUtility.Components;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using FM2E.Model.Basic;
using FM2E.BLL.Basic;

public partial class Module_FM2E_DeviceManager_DeviceInfo_SubsidiaryEquipmentManager_SubsidiaryEquipment : System.Web.UI.Page
{
    private readonly SubsidiaryEquipment bll = new SubsidiaryEquipment();
    //加载页面
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            PermissionControl();
        }
    }
    //初始化页面
    private void InitialPage()
    {
        try
        {
            //使用状态
            ListItem[] StatusTypeItems = EnumHelper.GetListItems(typeof(EquipmentStatus), (int)EquipmentStatus.Unknown);
            ddlStatusType.Items.Clear();
            ddlStatusType.Items.Add(new ListItem("不限", ((int)EquipmentStatus.Unknown).ToString()));
            ddlStatusType.Items.AddRange(StatusTypeItems);
            //所属系统
            EquipmentSystem systemBll = new EquipmentSystem();
            DDL_System.Items.AddRange(systemBll.GenerateListItemCollectionWithBlank());
            //公司
            DDL_Company.Items.Clear();
            DDL_Company.Items.AddRange(ListItemHelper.GetCompanyListItemsWithBlank());

        }
        catch (Exception ex)
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "初始化页面失败：" + ex.Message);
        }
    }
    //权限控制
    private void PermissionControl()
    {
    }
    //填充数据
    private void FillData()
    {
        try
        {
            int listCount = 0;
            QueryParam searchTerm = (QueryParam)ViewState["SearchTerm"];
            if (searchTerm == null)
            {
                searchTerm = new QueryParam(1, 10);
                searchTerm.Where = "";
            }
            searchTerm.PageSize = AspNetPager1.PageSize;
            searchTerm.PageIndex = AspNetPager1.CurrentPageIndex;
            IList list = bll.GetList(searchTerm, out listCount);
            AspNetPager1.RecordCount = listCount;
            GridView1.DataSource = list;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    //行命令
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long id = Convert.ToInt64(gvRow.Attributes["SubsidiaryEquipmentID"]);
        if (e.CommandName == "view")
        {
            Response.Redirect("ViewSubsidiaryEquipment.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                bll.DeleteSubsidiaryEquipment(id);
                FillData();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }
    //行数据绑定
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果 
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");
            //设置悬浮鼠标指针形状为"小手"
            e.Row.Attributes["style"] = "Cursor:hand";
            SubsidiaryEquipmentInfo item = (SubsidiaryEquipmentInfo)e.Row.DataItem;
            e.Row.Attributes["SubsidiaryEquipmentID"] = item.SubsidiaryEquipmentID.ToString();
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        SubsidiaryEquipmentInfo item = new SubsidiaryEquipmentInfo();
        item.SubsidiaryEquipmentNO = Common.inSQL(tbSubsidiaryEquipmentNO.Text.Trim());
        item.Name = Common.inSQL(tbName.Text.Trim());
        item.CompanyID = DDL_Company.SelectedValue;
        item.SystemID = DDL_System.SelectedValue;
        item.Model = Common.inSQL(tbModel.Text.Trim());
        item.Specification = Common.inSQL(tbSpecification.Text.Trim());

        if (!string.IsNullOrEmpty(TextBox_Address.Value.Trim()))
        {
            item.AddressName = Common.inSQL(TextBox_Address.Value.Trim());
        }
        item.DetailLocation = Common.inSQL(TextBox_DetailLocation.Text.Trim());

        item.AssertNumber = Common.inSQL(tbAssertNumber.Text.Trim());
        if (tbPrice.Text.Trim() != "")
        {
            item.Price = Convert.ToDecimal(tbPrice.Text.Trim());
        }
        else
        {
            item.Price = 0;
        }
        item.Status = (EquipmentStatus)Convert.ToInt32(ddlStatusType.SelectedValue);
        if (tbMaintenanceTimes.Text.Trim() != "")
        {
            item.MaintenanceTimes = Convert.ToInt32(tbMaintenanceTimes.Text.Trim());
        }
        else
        {
            item.MaintenanceTimes = 0;
        }
        item.Remark = Common.inSQL(tbRemark.Text.Trim());
        QueryParam searchTerm = bll.GenerateSearchTerm(item);
        TabContainer1.ActiveTabIndex = 0;
        ViewState["SearchTerm"] = searchTerm;
        FillData();
    }
}
