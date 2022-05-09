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
using System.IO;
using System.Collections.Generic;

using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using System.Runtime.Serialization.Formatters.Binary;

public partial class Module_FM2E_DeviceManager_DeviceInfo_CurrentEuipementInfo_AllEquipmentInfo_DeviceInfo : System.Web.UI.Page
{

    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    string companyid = UserData.CurrentUserData.CompanyID;
    string index = (string)Common.sink("index", MethodType.Get, 50, 0, DataType.Str);
    string typetree = (string)Common.sink("type", MethodType.Get, 50, 0, DataType.Str);
    protected string IsShow = "block";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonBind();
            InitialPage();
            FillData();
            //Process();
            TypeTreeSelect();
        }
        PermissionControl();
    }

    private void PermissionControl()
    {

        if (SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
        else GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;

        //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
        if (SystemPermission.CheckPermission(PopedomType.New))
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = true;
        else
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
        //********** Modification Finished 2011-09-09 **********************************************************************************************
    }

    private void ButtonBind()
    {
        HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[0];
        button.ButtonUrl += "&companyid=" + companyid;
    }

    private void InitialPage()
    {
        try
        {
            //设备类型
            ddlEqType.Items.Add(new ListItem("不限", ""));
            string[] eqtype = FM2E.BLL.System.ConfigItems.EqType;
            foreach (string s in eqtype)
            {
                ddlEqType.Items.Add(new ListItem(s, s));
            }


            //this.CascadingDropDown2.Category += companyid;
            AddTree1(0, (TreeNode)null);
            TreeView1.ShowLines = true;
            //Section bll = new Section();
            //SectionInfo sectioninfo = new SectionInfo();
            ////sectioninfo.CompanyID = companyid;
            //QueryParam sectionqp = bll.GenerateSearchTerm(sectioninfo);
            //sectionqp.PageSize = 500;
            //int sectionrc = 0;
            //IList sectionlist = bll.GetList(sectionqp, out sectionrc);
            //foreach (SectionInfo item in sectionlist)
            //{
            //    SectionName.Items.Add(new ListItem(item.SectionName, item.SectionID));
            //}

            //AddTree2(0, (TreeNode)null);

            FM2E.Model.System.LoginUserInfo loginUser = UserData.CurrentUserData;

            //维修单位
            ddlMaintainTeam.Items.Clear();
            ddlMaintainTeam.Items.Add(new ListItem("不限", "0"));
            ddlMaintainTeam.Items.AddRange(ListItemHelper.GetAllMaintainTeams(loginUser.CompanyID));

            EquipmentSystem systemBll = new EquipmentSystem();
            DropDownList_System.Items.AddRange(systemBll.GenerateListItemCollectionWithBlank());
            DropDownList_FilterSystem.Items.AddRange(systemBll.GenerateListItemCollectionWithBlank());



            //设备状态
            Status.Items.Clear();
            Status.Items.AddRange(EnumHelper.GetListItems(typeof(EquipmentStatus)));

            DropDownList_FilterStatus.Items.Clear();
            DropDownList_FilterStatus.Items.AddRange(EnumHelper.GetListItems(typeof(EquipmentStatus)));


            //公司
            DropDownList_FilterCompany.Items.Clear();
            DropDownList_FilterCompany.Items.AddRange(ListItemHelper.GetCompanyListItemsWithBlank());

            //TreeView2.ShowLines = true;
            //Warehouse bllwh = new Warehouse();
            //WarehouseInfo warehouse = bllwh.GetWarehouseByUserName(UserData.CurrentUserData.UserName);
            //if (warehouse == null || warehouse.WareHouseID == null || warehouse.WareHouseID == "")
            //{

            //}
            //else
            //{
            //    CascadingDropDown1.SelectedValue = "4";
            //    CascadingDropDown2.SelectedValue = warehouse.WareHouseID;
            //    IsShow = "none";
            //}
            BuildTree();

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败：" + ex.Message, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 初始化数据列表
    /// </summary>
    private void FillData()
    {
        try
        {
            GetStaticsViewState(index);//如果是统计时浏览，则获取要查阅的统计项信息
            Equipment bll = new Equipment();
            QueryParam qp = CurrentQueryParam;
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;
            Warehouse bllwh = new Warehouse();
            WarehouseInfo warehouse = bllwh.GetWarehouseByUserName(UserData.CurrentUserData.UserName);
            int recordCount = 0;
            IList list = bll.GetListWithoutWarehouse(qp, out recordCount, null);
            lbCurrentDeviceCount.Text = bll.GetCurrentDeviceCount(qp,null).ToString();
            Department dpbll = new Department();
            DepartmentInfo dpitem = dpbll.GetDepartment(UserData.CurrentUserData.DepartmentID);
            //判断仓管员、普通用户、管理员之间的
            //仓管员只能修改自己的产品、普通用户根据权限配置，管理员不受权限控制
            foreach(EquipmentInfoFacade item in list)
            {

                if ((UserData.CurrentUserData.IsAdministrator&&UserData.CurrentUserData.IsParentCompany)
                        ||(UserData.CurrentUserData.IsAdministrator&& (!UserData.CurrentUserData.IsParentCompany) && (UserData.CurrentUserData.CompanyID == item.CompanyID))
                        ||(SystemPermission.CheckPermission(PopedomType.Delete) && (item.CompanyID == UserData.CurrentUserData.CompanyID))
                        ||(SystemPermission.CheckPermission(PopedomType.Delete) && (dpitem.DepartmentType == DepartmentType.MaintainTeam)))
                    {
                        item.Visible = true;
                    }
                    else
                    {
                        item.Visible = false;
                    }


            }
            GridView1.DataSource = list;
            GridView1.DataBind();


            AspNetPager1.RecordCount = recordCount;

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "设备列表初始化失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
    }

    private void GetStaticsViewState(string index)
    {
        if (index != null && index != string.Empty && Session["typelist"] != null && ((List<EquipmentInfoFacade>)Session["typelist"]).Count >= Convert.ToInt32(index))
        {
            EquipmentInfoFacade item = ((List<EquipmentInfoFacade>)Session["typelist"])[Convert.ToInt32(index)];
            item.CategoryName = "";
            item.CategoryID = 0;
            //EquipmentSearchInfo item = new EquipmentSearchInfo();

            Equipment bll = new Equipment();
            QueryParam qp = bll.GenerateSearchTerm(item);

            CurrentQueryParam = qp;
        }
    }

    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        if (ScriptManager1.IsInAsyncPostBack && !ScriptManager1.IsNavigating)
        {
            ScriptManager1.AddHistoryPoint("Index", AspNetPager1.CurrentPageIndex.ToString());
        }
        if (ScriptManager1.IsInAsyncPostBack && !ScriptManager1.IsNavigating)
        {
            ScriptManager1.AddHistoryPoint("currentaddress", ViewState["currentaddress"]!=null?ViewState["currentaddress"].ToString():"");
        } 
        FillData();

    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "view")
        {
            string id = gvRow.Attributes["ID"];
            //Response.Redirect("<javascript>window.open("http://new.qzone.qq.com/" + Server.UrlEncode(qq)+")'</javascript>");

             Response.Redirect("ViewDeviceInfo.aspx?cmd=view&id=" + id + "&companyid=" + companyid + "&index=" + index);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                int row = Convert.ToInt32(e.CommandArgument);
                string id = Convert.ToString(gvRow.Attributes["ID"]);
                Equipment bll = new Equipment();
                bll.DelEquipment(id);
                FileUpLoadCommon.DeleteFile(gvRow.Attributes["PhotoUrl"]);

                //GridView1.Rows[row].Visible = false;
                //ButtonBind();
                FillData();


            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败：" + ex.Message, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }
    /// <summary>
    /// 列表显示初始化
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

            EquipmentInfoFacade item = (EquipmentInfoFacade)e.Row.DataItem;

            e.Row.Attributes["PhotoUrl"] = item.PhotoUrl;

            e.Row.Attributes["ID"] = item.EquipmentID.ToString();
        }

    }

    private void TypeTreeSelect()
    {
        if (typetree!=String.Empty)
        {
            EquipmentSearchInfo item = new EquipmentSearchInfo();
            item.CompanyID = DDLCompany.SelectedValue;
            item.AddressCode = typetree;
            Equipment bll = new Equipment();
            QueryParam qp = bll.GenerateSearchTerm(item);
            AspNetPager1.CurrentPageIndex = 1;
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;
            CurrentQueryParam = qp;
            FillData();
            TabContainer1.ActiveTabIndex = 0;
        }
    }

    /// <summary>
    /// 单击确定按钮触发的查询事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            EquipmentSearchInfo item = new EquipmentSearchInfo();
            item.CompanyID = DDLCompany.SelectedValue;
            //item.DetailLocation = Common.inSQL(DetailLocation.Text.Trim());
            item.EquipmentNO = Common.inSQL(EquipmentNO.Text.Trim());
            item.Name = Common.inSQL(Name.Text.Trim());
            if (CategoryID.Text != string.Empty)
                item.CategoryID = Convert.ToInt64(CategoryID.Text);
            item.CategoryName = Common.inSQL(CategoryName.Text.Trim());
            //if (SectionName.SelectedValue != string.Empty)
            //    item.SectionID = Common.inSQL(SectionName.SelectedValue);
            //item.LocationTag = Common.inSQL(LocationTag.SelectedValue);
            //item.LocationID = Common.inSQL(LocationID.SelectedValue);

            item.SystemID = DropDownList_System.SelectedValue;

            if (!string.IsNullOrEmpty(TextBox_Address.Value.Trim()))
            {
                item.AddressName = Common.inSQL(TextBox_Address.Value.Trim());
                item.AddressCode = Hidden_AddressCode.Value;
                typetree = item.AddressCode;
            }
            //if (SystemName.Text.Trim() != string.Empty)
            //{
            //    item.SystemID = SystemID.Text;
            //    item.SystemName = Common.inSQL(SystemName.Text.Trim());
            //}
            if (PurchaseOrderID.Text != string.Empty)
                item.PurchaseOrderID = PurchaseOrderID.Text.Trim();
            item.SerialNum = Common.inSQL(ddlEqType.SelectedValue);
            item.Model = Common.inSQL(Model.Text.Trim());//型号 
            if (Status.SelectedValue != "0")
                item.Status = (EquipmentStatus)Convert.ToInt64(Status.SelectedValue);
            item.Specification = Specification.Text.Trim();//品牌
            item.SupplierName = SupplierName.Text.Trim();
            item.ProducerName = ProducerName.Text.Trim();
            item.PurchaserName = PurchaserName.Text.Trim();
            item.ResponsibilityName = ResponsibilityName.Text.Trim();
            item.CheckerName = CheckerName.Text.Trim();
            if (PurchaseDate1.Text != string.Empty)
                item.PurchaseDate1 = Convert.ToDateTime(PurchaseDate1.Text.Trim());
            if (PurchaseDate2.Text != string.Empty)
                item.PurchaseDate2 = Convert.ToDateTime(PurchaseDate2.Text.Trim());
            if (ExamDate1.Text != string.Empty)
                item.ExamDate1 = Convert.ToDateTime(ExamDate1.Text.Trim());
            if (ExamDate2.Text != string.Empty)
                item.ExamDate2 = Convert.ToDateTime(ExamDate2.Text.Trim());

            if (OpeningDate1.Text != string.Empty)
                item.OpeningDate1 = Convert.ToDateTime(OpeningDate1.Text.Trim());
            if (OpeningDate2.Text != string.Empty)
                item.OpeningDate2 = Convert.ToDateTime(OpeningDate2.Text.Trim());
            if (FileDate1.Text != string.Empty)
                item.FileDate1 = Convert.ToDateTime(FileDate1.Text.Trim());
            if (FileDate2.Text != string.Empty)
                item.FileDate2 = Convert.ToDateTime(FileDate2.Text.Trim());
            if (UpdateTime1.Text != string.Empty)
                item.UpdateTime1 = Convert.ToDateTime(UpdateTime1.Text.Trim());
            if (UpdateTime2.Text != string.Empty)
                item.UpdateTime2 = Convert.ToDateTime(UpdateTime2.Text.Trim());
            if (IsCancel.SelectedValue != "0")
                item.IsCancel = Convert.ToInt16(IsCancel.SelectedValue);
            item.AddressType = AddressType.Warehouse;  //筛选非仓库类设备

            if (ddlMaintainTeam.SelectedValue != "0")
                item.MaintainDept = Convert.ToInt64(ddlMaintainTeam.SelectedValue);
            //By L　2012-9 增加资产编号编号设备查询*********************************************
            if (TextBox_AssertNumber.Text!=string.Empty)
            {
                item.AssertNumber = TextBox_AssertNumber.Text;
            }
            if (minRepairedTimes.Text!=string.Empty)
            {
                item.MinRepairTimes = Convert.ToInt32(minRepairedTimes.Text.Trim());
            }
            if (maxRepairedTimes.Text!=string.Empty)
            {
                item.MaxRepairTimes = Convert.ToInt32(maxRepairedTimes.Text.Trim());
            }
            else
            {
                item.MaxRepairTimes = 9999;
            }
            //**********************************************************************************
            Equipment bll = new Equipment();
            QueryParam qp = bll.GenerateSearchTerm(item);
            AspNetPager1.CurrentPageIndex = 1;
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;
            CurrentQueryParam = qp;
            FillData();
            TabContainer1.ActiveTabIndex = 0;


        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询设备失败：" + ex.Message, ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
    }

    //private void Process()
    //{
    //    if (cmd == "export")
    //    {
    //        //导出
    //        string file = Server.MapPath("~/public/2.xls");
    //        FileStream stream = File.Open(file, FileMode.Open);

    //        byte[] Buffer = null;
    //        long size;
    //        size = stream.Length;
    //        Buffer = new byte[size];
    //        stream.Read(Buffer, 0, int.Parse(stream.Length.ToString()));
    //        stream.Close();
    //        stream = null;

    //        HttpContext.Current.Response.ContentType = "application/xls";
    //        string header = "attachment; filename=" + file;
    //        HttpContext.Current.Response.AddHeader("content-disposition", header);
    //        HttpContext.Current.Response.BinaryWrite(Buffer);
    //        HttpContext.Current.Response.End();
    //        HttpContext.Current.Response.Flush();

    //    }
    //}

    public void AddTree1(long ParentID, TreeNode pNode)
    {
        CategorysearchInfo categoryinfo = new CategorysearchInfo();
        categoryinfo.Level = 1;
        Category bll = new Category();
        QueryParam qp = bll.GenerateSearchTerm(categoryinfo);
        qp.PageSize = 500;
        int recordcount = 0;
        IList nodelist = bll.GetList(qp, out recordcount);
        foreach (CategoryInfo item in nodelist)
        {
            TreeNode Node = new TreeNode();
            Node.Text = item.CategoryName;
            Node.Value = item.CategoryID.ToString();
            TreeView1.Nodes.Add(Node);
            Node.PopulateOnDemand = true;
            Node.Expanded = false;
        }

    }
    /// <summary>
    /// 树的展开事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void TreeView1_OnTreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {
        if (e.Node.ChildNodes.Count == 0)
        {
            CategorysearchInfo categoryinfo = new CategorysearchInfo();
            categoryinfo.ParentID = Convert.ToInt64(e.Node.Value);
            Category bll = new Category();
            QueryParam qp = bll.GenerateSearchTerm(categoryinfo);
            qp.PageSize = 500;
            int recordcount = 0;
            IList nodelist = bll.GetList(qp, out recordcount);
            foreach (CategoryInfo item in nodelist)
            {
                TreeNode Node = new TreeNode();
                Node.Text = item.CategoryName;
                Node.Value = item.CategoryID.ToString();
                e.Node.ChildNodes.Add(Node);
                Node.PopulateOnDemand = true;
                Node.Expanded = false;
            }
        }
    }


    //public void AddTree2(long ParentID, TreeNode pNode)
    //{
    //    EquipmentSystem bll = new EquipmentSystem();
    //    IList rootsystem = bll.GetAllSystem();
    //    foreach (EquipmentSystemInfo item in rootsystem)
    //    {
    //        TreeNode Node = new TreeNode();
    //        Node.Text = item.SystemName;
    //        Node.Value = item.SystemID;
    //        TreeView2.Nodes.Add(Node);

    //        Node.Expanded = true;
    //    }
    //}



    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        CategoryName.Text = this.TreeView1.SelectedNode.Text;
        CategoryID.Text = this.TreeView1.SelectedValue;
        ViewState["parentcategoryidtemp"] = this.TreeView1.SelectedNode.Value;
        Category bll = new Category();
        ViewState["level"] = Convert.ToString(bll.GetCategory(Convert.ToInt64(ViewState["parentcategoryidtemp"].ToString())).Level + 1);
        PopupControlExtender1.Commit(CategoryName.Text);
        PopupControlExtender2.Commit(CategoryID.Text);


    }

    //protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
    //{
    //    SystemName.Text = this.TreeView2.SelectedNode.Text;
    //    SystemID.Text = this.TreeView2.SelectedValue;
    //    PopupControlExtender3.Commit(SystemName.Text);
    //    PopupControlExtender4.Commit(SystemID.Text);


    //}

    ///// <summary>
    ///// 打印设备事件
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void PrintPreviewAll(object sender, EventArgs e)
    //{
    //    Equipment bll = new Equipment();
    //    IList list = bll.GetAllEquipment();
    //    GridView1.DataSource = list;
    //    GridView1.DataBind();
    //    GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
    //    GridView1.Columns[GridView1.Columns.Count - 2].Visible = false;
    //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "printpreviewall", "printdiv('PrintDiv','null');", true);
    //}


    #region 地址树

    private readonly Address addressBll = new Address();


    /// <summary>
    /// 建立地址树，不加仓库类型的地址
    /// </summary>
    private void BuildTree()
    {
        try
        {
            TreeNode root = null;

            IList addressList = addressBll.GetChildAddress(1);
            root = new TreeNode("地址列表", "1");

            addressTree.Nodes.Add(root);

            foreach (AddressInfo item in addressList)
            {
                if (item.AddressType != AddressType.Warehouse)
                {
                    TreeNode node = new TreeNode(item.AddressName, item.ID.ToString());
                    node.Expanded = false;
                    if (item.ChildCount > 0)
                    {
                        //非叶子结点
                        node.Expanded = false;
                        node.PopulateOnDemand = true;
                        node.SelectAction = TreeNodeSelectAction.SelectExpand;

                    }
                    root.ChildNodes.Add(node);
                }
            }

            root.Select();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "加载地址树失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 展开树结点
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void addressTree_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        try
        {
            long id = Convert.ToInt64(e.Node.Value);
            IList addressList = addressBll.GetChildAddress(id);
            foreach (AddressInfo item in addressList)
            {
                if (item.AddressType != AddressType.Warehouse)
                {
                    TreeNode node = new TreeNode(item.AddressName, item.ID.ToString());
                    node.Expanded = false;
                    if (item.ChildCount > 0)
                    {
                        //非叶子结点
                        node.PopulateOnDemand = true;
                        node.SelectAction = TreeNodeSelectAction.SelectExpand;
                    }
                    e.Node.ChildNodes.Add(node);
                }
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取下一级地址结点失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 选择树结点
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void addressTree_SelectedNodeChanged(object sender, EventArgs e)
    {
        Filter();
    }

    private void Filter()
    {
        long addressid = Convert.ToInt64(addressTree.SelectedValue);
        AddressInfo address = addressBll.GetAddress(addressid);

        string name = Common.inSQL(TextBox_FilterName.Text.Trim());
        string model = Common.inSQL(TextBox_FilterModel.Text.Trim());
        string systemid = DropDownList_FilterSystem.SelectedValue;
        EquipmentStatus status = (EquipmentStatus)Convert.ToInt32(DropDownList_FilterStatus.SelectedValue);
        string companyid = DropDownList_FilterCompany.SelectedValue;

        EquipmentSearchInfo item = new EquipmentSearchInfo();
        item.CompanyID = companyid;
        item.Name = name;
        item.Model = model;
        item.SystemID = systemid;
        item.Status = status;
        item.AddressType = AddressType.Warehouse;  //筛选非仓库类型的设备
        if (address != null)
        {
            item.AddressCode = address.AddressCode;
            ViewState["currentaddress"] = address.ID;
        }
        if (ScriptManager1.IsInAsyncPostBack && !ScriptManager1.IsNavigating)
        {
            ScriptManager1.AddHistoryPoint("Index", Convert.ToString(1));
        }
        if (ScriptManager1.IsInAsyncPostBack && !ScriptManager1.IsNavigating)
        {
            ScriptManager1.AddHistoryPoint("currentaddress", ViewState["currentaddress"] != null ? ViewState["currentaddress"].ToString() : "");
        } 
        Equipment bll = new Equipment();
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

    #endregion


    private QueryParam CurrentQueryParam
    {
        get
        {
            QueryParam qp = (QueryParam)ViewState["SearchTerm"];
            if (qp == null)
            {
                if (qp == null)
                {
                    qp = new QueryParam();
                }
                qp.PageIndex = 1;
                qp.PageSize = AspNetPager1.PageSize;
            }
            return qp;
        }
        set { ViewState["SearchTerm"] = value; }
    }

    /// <summary>
    /// 浏览器返回的时候
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ScriptManager1_Navigate(object sender, HistoryEventArgs e)
    {
        string indexString = e.State["Index"];
        string currentaddress = e.State["currentaddress"];
        if (string.IsNullOrEmpty(indexString))
        {
            AspNetPager1.CurrentPageIndex = 1;

        }
        else
        {
            int Index = Convert.ToInt32(indexString);
            AspNetPager1.CurrentPageIndex = Index;
        }
        if (!string.IsNullOrEmpty(currentaddress))
        {
            AddressInfo address = addressBll.GetAddress(Convert.ToInt64(currentaddress));
            EquipmentSearchInfo item = new EquipmentSearchInfo();
            item.AddressCode = address.AddressCode;
            item.MaxRepairTimes = 999;
            Equipment bll = new Equipment();
            QueryParam qp = bll.GenerateSearchTerm(item);
            CurrentQueryParam = qp;
            OperNodeByID(currentaddress, addressTree.Nodes, ref addressTree);
        }
        else
        {
            QueryParam qp = new QueryParam();
            CurrentQueryParam = qp;
            addressTree.FindNode(addressTree.Nodes[0].ValuePath).Selected = true;
        }

        FillData();
    }


    public void OperNodeByID(string nodeID, TreeNodeCollection tnc, ref   TreeView tv)
    {
        foreach (TreeNode node in tnc)
        {
            if (node.Value == nodeID)
            {
                tv.FindNode(node.ValuePath).Selected = true;
            }
            if (node.ChildNodes.Count != 0)
                OperNodeByID(nodeID, node.ChildNodes, ref   tv);
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
