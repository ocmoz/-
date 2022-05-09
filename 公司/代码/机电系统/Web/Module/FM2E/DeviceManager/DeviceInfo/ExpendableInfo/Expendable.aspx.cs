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
using WebUtility.Components;
using FM2E.BLL.Basic;
using FM2E.BLL.System;
using FM2E.BLL.Utils;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using System.Collections.Generic;

public partial class Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_Expendable : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 255, 0, DataType.Long);
    string showheader = (string)Common.sink("showheader", MethodType.Get, 0, 0, DataType.Str);
    protected string IsShow = "block";
    protected string IsShow2 = "block";
    protected string WarehouseName = "";
    protected string WarehouseID = "";

    private string searchquery = "Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_Expendable";



    protected void Page_Load(object sender, EventArgs e)
    {
        BindControl();   
        if (!IsPostBack)
        {
            InitPage();     
            AddTypeTree(0, (TreeNode)null);
            TreeTypeView.ShowLines = true;
            FillData();
            if (!string.IsNullOrEmpty(WarehouseName))
                HeadMenuWebControls1.HeadTitleTxt += "    当前仓库：<font color='red'>" + WarehouseName + "</font>";

            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            PermissionControl();
            //********** Modification Finished 2011-09-09 **********************************************************************************************
        }
        
    }

    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
    private void PermissionControl()
    {
        bool bNew, bDelete, bEdit, bOut, bIn;
        bNew = SystemPermission.CheckPermission(PopedomType.New);
        bDelete = SystemPermission.CheckPermission(PopedomType.Delete);
        bEdit = SystemPermission.CheckPermission(PopedomType.Edit);
        //bOut = SystemPermission.CheckPermission(PopedomType.PermissionA);
        //bIn = SystemPermission.CheckPermission(PopedomType.PermissionB);

        HeadMenuWebControls1.ButtonList[0].ButtonVisible = bNew;
        HeadMenuWebControls1.ButtonList[1].ButtonVisible = bNew;
        HeadMenuWebControls1.ButtonList[4].ButtonVisible = bNew;
        HeadMenuWebControls1.ButtonList[5].ButtonVisible = bNew;
        GridView1.Columns[GridView1.Columns.Count - 2].Visible = bDelete;
        GridView1.Columns[GridView1.Columns.Count - 3].Visible = bEdit;
        GridView1.Columns[GridView1.Columns.Count - 4].Visible = bEdit;
        GridView1.Columns[GridView1.Columns.Count - 5].Visible = bEdit;
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************


    private void InitPage()
    {
        //Warehouse bllstaff = new Warehouse();
        //ExpendableInfo item = new ExpendableInfo();
        //WarehouseInfo warehouse = bllstaff.GetWarehouseByUserName(UserData.CurrentUserData.UserName);
        //if (warehouse == null || warehouse.WareHouseID == null || warehouse.WareHouseID == "")
        //{
        //    if (!UserData.CurrentUserData.IsParentCompany)
        //    {
        //        CascadingDropDown1.SelectedValue = UserData.CurrentUserData.CompanyID;
        //        item.CompanyID = UserData.CurrentUserData.CompanyID;
        //        IsShow = "none";
        //    }
        //    else
        //    {
        //        IsShow = "block";
        //    }
        //}
        //else
        //{
        //    CascadingDropDown1.SelectedValue = UserData.CurrentUserData.CompanyID;
        //    CascadingDropDown2.SelectedValue = warehouse.WareHouseID;
        //    IsShow = "none";
        //    IsShow2 = "none";
        //    item.CompanyID = UserData.CurrentUserData.CompanyID;
        //    item.WarehouseID = warehouse.WareHouseID;
        //    WarehouseName = warehouse.Name;
        //    WarehouseID = warehouse.WareHouseID;
        //}

        Expendable bll = new Expendable();
        QueryParam qp = bll.GenerateSearchTerm(new ExpendableInfo());
        CurrentQueryParam = qp;
    }

    private void BindControl()
    {
        Warehouse bllstaff = new Warehouse();
        ExpendableInfo item = new ExpendableInfo();
        //WarehouseInfo warehouse = bllstaff.GetWarehouseByUserName(UserData.CurrentUserData.UserName);
        //if (warehouse == null || warehouse.WareHouseID == null || warehouse.WareHouseID == "")
        //{
            //if (!UserData.CurrentUserData.IsParentCompany)
            //{
            //    CascadingDropDown1.SelectedValue = UserData.CurrentUserData.CompanyID;
            //    item.CompanyID = UserData.CurrentUserData.CompanyID;
            //    IsShow = "none";
            //}
            //else
            //{
            //    IsShow = "block";
            //}
        //}
        //else
        //{
            //CascadingDropDown1.SelectedValue = UserData.CurrentUserData.CompanyID;
            //CascadingDropDown2.SelectedValue = warehouse.WareHouseID;
            IsShow = "block";
            IsShow2 = "block";
            //item.CompanyID = UserData.CurrentUserData.CompanyID;
            //item.WarehouseID = warehouse.WareHouseID;
            //WarehouseName = warehouse.Name;
            //WarehouseID = warehouse.WareHouseID;
        //}
    }
    private void FillData()
    {
        Expendable bll = new Expendable();
        int listCount = 0;

        
        //ExpendableInfo item = new ExpendableInfo();
        //item.Name = Common.inSQL(TbName.Text.Trim());
        //item.Model = Common.inSQL(TextBox_Model.Text.Trim());
        //item.CompanyID = DDLCompany.SelectedValue;
        //item.WarehouseID = DDLWarehouse.SelectedValue;

        QueryParam searchTerm = CurrentQueryParam;// bll.GenerateSearchTerm(item);

        //ViewState["SearchTerm"] = searchTerm;

        searchTerm.PageIndex = AspNetPager1.CurrentPageIndex;
        searchTerm.PageSize = AspNetPager1.PageSize;
        IList list = bll.GetList(searchTerm, out listCount);
        AspNetPager1.RecordCount = listCount;
        GridView1.DataSource = list;
        GridView1.DataBind();

        searchTerm.PageIndex = 1;
        searchTerm.PageSize = Int32.MaxValue;
        Session[searchquery] = searchTerm;
    }


    private QueryParam CurrentQueryParam
    {
        get
        {
            QueryParam qp = (QueryParam)ViewState["SearchTerm"];
            Expendable bll = new Expendable();
            ExpendableInfo item = new ExpendableInfo();
            if (qp == null)
            {
                item.Name = Common.inSQL(TbName.Text.Trim());
                item.Model = Common.inSQL(TextBox_Model.Text.Trim());
                item.CompanyID = DDLCompany.SelectedValue;
                //item.WarehouseID = DDLWarehouse.SelectedValue;
                //if (cmd == "view")
                //{
                   // item.CategoryID = id;
                //}
                //item.WarehouseID = WarehouseID;
                qp = bll.GenerateSearchTerm(item);
            }

            qp.PageIndex = 1;
            qp.PageSize = AspNetPager1.PageSize;
            return qp;
        }
        set { ViewState["SearchTerm"] = value; }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        if (ScriptManager1.IsInAsyncPostBack && !ScriptManager1.IsNavigating)
        {
            ScriptManager1.AddHistoryPoint("Index", AspNetPager1.CurrentPageIndex.ToString());
        } 
        FillData();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            try
            {
                GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
                long id = Convert.ToInt64(gvRow.Attributes["ExpendableID"]);
                Expendable Expendable = new Expendable();
                Expendable.DelExpendable(id);
                FillData();
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
            ExpendableInfo item = (ExpendableInfo)e.Row.DataItem;
            e.Row.Attributes["ExpendableID"] = item.ExpendableID.ToString();
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {



            Expendable bll = new Expendable();

            ExpendableInfo item = new ExpendableInfo();
            item.Name = Common.inSQL(TbName.Text.Trim());
            item.Model = Common.inSQL(TextBox_Model.Text.Trim());
            item.CompanyID = DDLCompany.SelectedValue;
            item.WarehouseID = DDLWarehouse.SelectedValue;

            QueryParam searchTerm = bll.GenerateSearchTerm(item);
            searchTerm.PageSize = AspNetPager1.PageSize;
            ViewState["SearchTerm"] = searchTerm;
            AspNetPager1.CurrentPageIndex = 1;
            TabContainer1.ActiveTabIndex = 0;
            FillData();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询消耗品失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        TbName.Text = "";
        TextBox_Model.Text = "";
        TabContainer1.ActiveTabIndex = 1;
        //TabOptionWebControls1.SelectIndex = 1;
    }


    private void Filter()
    {


        string name = Common.inSQL(TextBox_FilterName.Text.Trim());
        string model = Common.inSQL(TextBox_FilterModel.Text.Trim());
        string categorycode = Convert.ToString(TreeTypeView.SelectedValue);

        Expendable bll = new Expendable();

        ExpendableInfo item = new ExpendableInfo();
        item.Name = name;
        item.Model = model;
        //item.CompanyID = DDLCompany.SelectedValue;
        //item.WarehouseID = DDLWarehouse.SelectedValue;
        item.CategoryCode = categorycode;

        //QueryParam searchTerm = bll.GenerateSearchTerm(item);


        QueryParam qp = bll.GenerateSearchTerm(item);
        AspNetPager1.CurrentPageIndex = 1;
        qp.PageIndex = AspNetPager1.CurrentPageIndex;
        qp.PageSize = AspNetPager1.PageSize;

        CurrentQueryParam = qp;

        FillData();
    }

    protected void OnFilter(object sender, EventArgs e)
    {
        Filter();
    }


    public void AddTypeTree(long ParentID, TreeNode pNode)
    {
        CategorysearchInfo categoryinfo = new CategorysearchInfo();
        categoryinfo.ParentID = ParentID;
        Category bll = new Category();
        QueryParam qp = bll.GenerateSearchTerm(categoryinfo);
        int recordcount = 0;
        IList nodelist = bll.GetList(qp, out recordcount);
        List<CategoryInfo> subnodes = new List<CategoryInfo>();
        //插入一个不限的
        if (ParentID == 0)
        {
            TreeNode Node = new TreeNode();
            Node.Text = "不限";
            Node.Value = "0";
            Node.Selected = true;
            Node.SelectAction = TreeNodeSelectAction.SelectExpand;
            TreeTypeView.Nodes.Add(Node);
            Node.Expanded = false;
        }
        foreach (CategoryInfo node in nodelist)
        {
            if (node.ParentID == ParentID)
                subnodes.Add(node);
        }

        //循环递归
        foreach (CategoryInfo node in subnodes)
        {
            TreeNode Node = new TreeNode();
            Node.SelectAction = TreeNodeSelectAction.SelectExpand;
            //Node.NavigateUrl = "Expendable.aspx?cmd=view&id=" + node.CategoryID + "&showheader=" + showheader;
            if (pNode == null)
            {
                Node.Text = node.CategoryName;
                Node.Value = node.CategoryCode;
                TreeTypeView.Nodes.Add(Node);
                Node.Expanded = false;
                AddTypeTree(node.CategoryID, Node);
            }
            else
            {
                Node.Text = node.CategoryName;
                Node.Value = node.CategoryCode;
                pNode.ChildNodes.Add(Node);
                Node.Expanded = false;
                AddTypeTree(node.CategoryID, Node);
            }
        }
    }

    /// <summary>
    /// 选择树结点
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TreeTypeView_SelectedNodeChanged(object sender, EventArgs e)
    {
        Filter();
    }

    protected void Button_Filter_Click(object sender, EventArgs e)
    {
        Filter();
    }

    private WarehouseInfo CurrentWarehouse
    {
        get
        {
            WarehouseInfo warehouse = (WarehouseInfo)ViewState["CurrentWarehouse"];
            if (warehouse == null)
            {
                warehouse = new Warehouse().GetWarehouseByUserName(UserData.CurrentUserData.UserName);
            }
            return warehouse;
        }
        set
        {
            ViewState["CurrentWarehouse"] = value;
        }
    }

    protected void Button_FillData_Click(Object sender, EventArgs e)
    {
        FillData();
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

