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
using FM2E.Model.System;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;

public partial class Module_FM2E_DeviceManager_DeviceInfo_ConsumableEquipmentManager_ConsumableEquipment : System.Web.UI.Page
{
    private readonly ConsumableEquipment bll = new ConsumableEquipment();
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
            LoginUserInfo loginUser = UserData.CurrentUserData;

            //维修单位
            ddlMaintainTeam.Items.Clear();
            ddlMaintainTeam.Items.Add(new ListItem("不限", "0"));
            ddlMaintainTeam.Items.AddRange(ListItemHelper.GetAllMaintainTeams(loginUser.CompanyID));

            
            Company bllcompany = new Company();
            IList list = (List<CompanyInfo>)bllcompany.GetAllCompany();

            foreach (CompanyInfo item in list)
            {
                DDLCompany.Items.Add(new ListItem(item.CompanyName,item.CompanyID));
            }

            
            
        }
        catch (Exception ex)
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "初始化页面失败：" + ex.Message);
        }
    }
    //权限控制
    private void PermissionControl()
    {
        //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
        if (SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
        else
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;

        if (SystemPermission.CheckPermission(PopedomType.New))
        {
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = true;
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = true;
        }
        else
        {
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
        }
        //********** Modification Finished 2011-09-09 **********************************************************************************************
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

            searchTerm.PageSize = Int32.MaxValue;
            //searchTerm.TableName = "resetcompany";
            lbCurrentDeviceCount.Text = bll.GetCurrentDeviceCount(searchTerm, DDLCompany.SelectedValue).ToString();
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
        long id = Convert.ToInt64(gvRow.Attributes["ConsumableEquipmentID"]);
        if (e.CommandName == "view")
        {
            Response.Redirect("ViewConsumableEquipment.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                bll.DeleteConsumableEquipment(id);
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
            ConsumableEquipmentInfo item = (ConsumableEquipmentInfo)e.Row.DataItem;
            e.Row.Attributes["ConsumableEquipmentID"] = item.ConsumableEquipmentID.ToString();
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        ConsumableEquipmentInfo item = new ConsumableEquipmentInfo();
        item.ConsumableEquipmentNO = Common.inSQL(tbConsumableEquipmentNO.Text.Trim());
        item.Name = Common.inSQL(tbName.Text.Trim());
        item.SystemID = Common.inSQL(tbSystemID.Text.Trim());
        item.SerialNum = Common.inSQL(tbSerialNum.Text.Trim());
        item.Model = Common.inSQL(tbModel.Text.Trim());
        item.Specification = Common.inSQL(tbSpecification.Text.Trim());
        item.AssertNumber = Common.inSQL(tbAssertNumber.Text.Trim());
        item.Unit = Common.inSQL(tbUnit.Text.Trim());
        if (tbCount.Text.Trim() != "")
        {
            item.Count = Convert.ToInt32(tbCount.Text.Trim());
        }
        else
        {
            item.Count = 0;
        }
        if (tbPrice.Text.Trim() != "")
        {
            item.Price = Convert.ToDecimal(tbPrice.Text.Trim());
        }
        else
        {
            item.Price = 0;
        }
        if (tbMaintenanceTimes.Text.Trim() != "")
        {
            item.MaintenanceTimes = Convert.ToInt32(tbMaintenanceTimes.Text.Trim());
        }
        else
        {
            item.MaintenanceTimes = 0;
        }
        item.Remark = Common.inSQL(tbRemark.Text.Trim());
        if(item.CompanyID!="")
            item.CompanyID = DDLCompany.SelectedValue;

        if(ddlMaintainTeam.SelectedValue!="0")
            item.MaintainDept = Convert.ToInt64(ddlMaintainTeam.SelectedValue);

        QueryParam searchTerm = bll.GenerateSearchTerm(item);
        TabContainer1.ActiveTabIndex = 0;
        ViewState["SearchTerm"] = searchTerm;
        FillData();
    }
}
