using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WebUtility;
using WebUtility.Components;
using WebUtility.WebControls;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using System.Collections.Generic;
using FM2E.Model.Exceptions;
using FM2E.Model.Utils;
using FM2E.BLL.System;
using FM2E.Model.System;

public partial class Module_FM2E_BasicData_DepotManage_EditDepot : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private string id = (string)Common.sink("id", MethodType.Get, 50, 0, DataType.Str);

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            ButtonBind();
            DropDownList_Sex.Items.Clear();
            DropDownList_Sex.Items.AddRange(EnumHelper.GetListItemsEx(typeof(Sex), (int)Sex.Unknown));
            FillData();
            TabContainer1.Tabs[1].Visible = false;
        }
    }

    private void ButtonBind()
    {
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：仓库信息添加";
            TabPanel1.HeaderText = "添加仓库";
            //TabOptionWebControls1.TaboptionItems[0].Tab_Name = "添加仓库";
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：仓库信息修改";
            TabPanel1.HeaderText = "修改仓库信息";
            //TabOptionWebControls1.TaboptionItems[0].Tab_Name = "修改仓库信息";
        }
    }
    private void FillData()
    {
        DropDownList1.Items.Clear();
        DropDownList1.Items.Add(new ListItem("非公司所属", ""));
        Company company = new Company();
        IList<CompanyInfo> list = company.GetAllCompany();
        foreach (CompanyInfo item in list)
        {
            DropDownList1.Items.Add(new ListItem(item.CompanyName, item.CompanyID));
        }
        //默认选中公司
        string companyid = UserData.CurrentUserData.CompanyID;
        
        if (companyid != null && companyid != "")
        {
            try
            {
                DropDownList1.SelectedValue = companyid;
            }
            catch { }
        }

        if (cmd == "edit")
        {
            try
            {
                WarehouseInfo item;
                Warehouse bll = new Warehouse();
                item = bll.GetWarehouse(id);
                TextBox1.Text = item.WareHouseID;
                TextBox2.Text = item.Name;
                TextBox_Address.Value = item.AddressName;
                Hidden_AddressID.Value = item.AddressID.ToString();
                if (item.CompanyID != "")
                    DropDownList1.SelectedValue = item.CompanyID;
                this.principal.Value = item.ResName;
                this.principalID.Value = item.ResID.ToString();
                this.contactor.Value = item.Contactor;
                this.contactorID.Value = item.ContactorID.ToString();
                TextBox6.Text = item.Phone;
                TextArea1.Value = item.Remark;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;
        if (cmd == "add" || cmd == "edit")
        {
            WarehouseInfo item = new WarehouseInfo();
            item.WareHouseID = TextBox1.Text.Trim();
            item.Name = TextBox2.Text.Trim();
            long addressid = 0;
            if(!long.TryParse(Hidden_AddressID.Value,out addressid))
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "必须选择仓库地址", Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }

            item.AddressID = addressid;
            item.CompanyName = DropDownList1.SelectedItem.Text.ToString();
            item.CompanyID = DropDownList1.SelectedValue.ToString();

            item.Phone = TextBox6.Text.Trim();
            if (principalID.Value.Trim() != "")
                item.ResID = principalID.Value.Trim();
            else
                item.ResID = "";
            item.ResName = principal.Value.Trim();
            item.Contactor = contactor.Value.Trim();
            if (contactorID.Value.Trim() != "")
                item.ContactorID = contactorID.Value.Trim();
            else
                item.ContactorID = "";
            //if (!yes.Checked)//负责人不属于本公司
            //{
            //    item.ResID = 0;
            //    item.ResName = TextBox4.Text.Trim();
            //    item.Type = (byte)0;
            //}
            //else//负责人属于本公司
            //{
            //    item.ResID = Convert.ToInt64(principalID.Value.Trim());
            //    item.ResName = principal.Value.Trim();
            //    item.Type = (byte)1;
            //}
            item.Remark = TextArea1.Value.Trim();
            item.IsDeleted = false;
            if (cmd == "add")
            {
                try
                {
                    Warehouse warehouse = new Warehouse();
                    warehouse.InsertWarehouse(item);
                    bSuccess = true;
                }
                catch (DuplicateException ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加仓库失败：" + ex.Message, ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加仓库失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                }
                if (bSuccess == true)
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加仓库成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Depot.aspx"), UrlType.Href, "");
            }
            else if (cmd == "edit")
            {
                try
                {
                    Warehouse warehouse = new Warehouse();
                    warehouse.UpdateWarehouse(item);
                    bSuccess = true;
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改仓库信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                }
                if (bSuccess == true)
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改仓库信息成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Depot.aspx"), UrlType.Href, "");
            }
        }
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text.Trim() != string.Empty)
        {
            TabContainer1.Tabs[1].Visible = true;
            TabContainer1.ActiveTabIndex = 1;
            try
            {
               CascadingDropDown1.SelectedValue = UserData.CurrentUserData.CompanyID;
                //DropDownList_Company.SelectedValue = UserData.CurrentUserData.CompanyID;
            }
            catch { }
            FillWarehouseUser();
        }
        
    }

    private UserSearchInfo GetSearchTerm()
    {
        //生成查询条件
        UserSearchInfo item = new UserSearchInfo();

        if (TextBox_UserName.Text.Trim() != string.Empty)
        {
            item.UserName = Common.inSQL(TextBox_UserName.Text.Trim());
        }
        if (TextBox_Name.Text.Trim() != string.Empty)
        {
            item.PersonName = Common.inSQL(TextBox_Name.Text.Trim());
        }

        item.CompanyID = DropDownList_Company.SelectedValue;

        item.Sex = (Sex)Convert.ToInt32(DropDownList_Sex.SelectedValue);

        if (!string.IsNullOrEmpty(DropDownList_Department.SelectedValue.Trim()))
        {
            item.DepartmentID = Convert.ToInt64(DropDownList_Department.SelectedValue);
        }

        return item;
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        AspNetPager2.CurrentPageIndex = 1;
        FillUser(); 
        
    }
    private void FillWarehouseUser()
    {
        try
        {
            Warehouse bll = new Warehouse();
            int listCount = 0;
            QueryParam searchTerm = new QueryParam();
            searchTerm.PageSize = AspNetPager1.PageSize;
            searchTerm.PageIndex = AspNetPager1.CurrentPageIndex;
            IList list = bll.GetWarehouseUserList(searchTerm, TextBox1.Text.Trim(), out listCount);
            AspNetPager1.RecordCount = listCount;
            GridView1.DataSource = list;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    private void FillUser()
    {
        try
        {
            User userBll = new User();
            int listCount = 0;
            IList list = userBll.GetList(GetSearchTerm(), AspNetPager2.CurrentPageIndex, AspNetPager2.PageSize, out listCount);
            AspNetPager2.RecordCount = listCount;

            GridView2.DataSource = list;
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询用户失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        string username = Convert.ToString(gvRow.Attributes["UserName"]);
        if (e.CommandName == "del")
        {
            try
            {
                Warehouse bll = new Warehouse();
                WarehouseUserInfo info = new WarehouseUserInfo();
                info.WarehouseID = TextBox1.Text.Trim();
                info.UserName = username;
                bll.DelWarehouseUser(info);
                FillWarehouseUser();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
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
            UserInfo item = (UserInfo)e.Row.DataItem;
            e.Row.Attributes["UserName"] = item.UserName.ToString();
        }

    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string username = Convert.ToString(e.CommandArgument);
        if (e.CommandName == "select")
        {
            try
            {
                WarehouseUserInfo info = new WarehouseUserInfo();
                info.WarehouseID = TextBox1.Text.Trim();
                info.UserName = username;
                Warehouse bll = new Warehouse();
                bll.InsertWarehouseUser(info);
                FillWarehouseUser();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "选择数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
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
            UserInfo item = (UserInfo)e.Row.DataItem;
            e.Row.Attributes["UserName"] = item.UserName.ToString();
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillWarehouseUser();
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        FillUser();
    }
}
