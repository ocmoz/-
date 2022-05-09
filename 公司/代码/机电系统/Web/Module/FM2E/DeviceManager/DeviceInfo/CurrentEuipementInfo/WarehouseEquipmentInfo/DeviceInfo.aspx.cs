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
using FM2E.Model.Exceptions;

public partial class Module_FM2E_DeviceManager_DeviceInfo_CurrentEuipementInfo_WarehouseEquipmentInfo_DeviceInfo : System.Web.UI.Page
{

    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    string companyid = UserData.CurrentUserData.CompanyID;
    protected string IsShow = "block";

    protected string WarehouseName = "";
    //********************************* Modified by Xue 2011-7-26 *******************
    private List<AddressInfo> AddressInforList
    {
        set { ViewState["addressinforList"] = value; }
        get 
        {
            if (ViewState["addressinforList"] == null)
            {
                return new List<AddressInfo>();
            }
            else
            {
                return (List<AddressInfo>)ViewState["addressinforList"]; 
            }
        }
    }
    //********************************* Modification Finished *************************
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //********************************* Modified by Xue 2011-7-26 *******************
            //---Warehouse bllwh = new Warehouse();
            //---WarehouseInfo warehouse = bllwh.GetWarehouseByUserName(UserData.CurrentUserData.UserName);
            //---if (warehouse == null || warehouse.WareHouseID == null || warehouse.WareHouseID == string.Empty)
            //---{
            //---    EventMessage.MessageBox(Msg_Type.Error, "操作警告", "本页面只允许仓管员进入", new WebException("本页面只允许仓管员进入"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            //---    return;
            //---}
            //---Hidden_WarehouseAddressCode.Value = warehouse.AddressCode;
            //---WarehouseName = warehouse.Name;
            
            Warehouse whbll = new Warehouse();
            List<WarehouseInfo> warehouseList = whbll.GetWarehouseListByUserName(UserData.CurrentUserData.UserName);
            if (warehouseList.Count == 0 || warehouseList[0].WareHouseID == null || warehouseList[0].WareHouseID == string.Empty)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作警告", "本页面只允许仓管员进入", new WebException("本页面只允许仓管员进入"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                return;
            }
            DropDownList_FilterWareHouse.DataSource = warehouseList;
            DropDownList_FilterWareHouse.DataTextField = "Name";
            DropDownList_FilterWareHouse.DataValueField = "AddressCode";
            DropDownList_FilterWareHouse.DataBind();

            Address addressBLL = new Address();
            List<AddressInfo> aiList = new List<AddressInfo>();
            foreach (WarehouseInfo item in warehouseList)
            {
                aiList.Add(addressBLL.GetAddressByAddressCode(item.AddressCode));
            }
            AddressInforList = aiList;
            //********************************* Modification Finished *************************

            //LocationTag.Text = "仓库";
            //LocationID.Text = warehouse.WareHouseID;
            //LocationName.Text = warehouse.Name;
            //DDLCompany.Text = UserData.CurrentUserData.CompanyName;
            ButtonBind();
            InitialPage();
            FillData();
            //Process();
            PermissionControl();
            //---HeadMenuWebControls1.HeadTitleTxt += "    当前仓库：<font color='red'>" + WarehouseName + "</font>";
        }
        //********************************* Modified by Xue 2011-7-26 *******************
        Hidden_WarehouseAddressCode.Value = DropDownList_FilterWareHouse.Items[DropDownList_FilterWareHouse.SelectedIndex].Value;
        WarehouseName = DropDownList_FilterWareHouse.Items[DropDownList_FilterWareHouse.SelectedIndex].Text;
        //HeadMenuWebControls1.HeadTitleTxt = "仓库设备管理    当前仓库：<font color='red'>" + WarehouseName + "</font>";
        //********************************* Modification Finished *************************
    }

    private void PermissionControl()
    {

        if (SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
        else GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;

        //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = SystemPermission.CheckPermission(PopedomType.New);
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
            //TreeView2.ShowLines = true;

            EquipmentSystem systemBll = new EquipmentSystem();
            DropDownList_System.Items.AddRange(systemBll.GenerateListItemCollectionWithBlank());
            DropDownList_FilterSystem.Items.AddRange(systemBll.GenerateListItemCollectionWithBlank());
            //DropDownList1.Items.AddRange(systemBll.GenerateListItemCollectionWithBlank());

            //DropDownList2.Items.AddRange(EnumHelper.GetListItems(typeof(EquipmentStatus)));

            //设备状态
            Status.Items.Clear();
            Status.Items.AddRange(EnumHelper.GetListItems(typeof(EquipmentStatus)));
            DropDownList_FilterStatus.Items.Clear();
            DropDownList_FilterStatus.Items.AddRange(EnumHelper.GetListItems(typeof(EquipmentStatus)));

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
            //Warehouse bllwh = new Warehouse();
            //WarehouseInfo warehouse = bllwh.GetWarehouseByUserName(UserData.CurrentUserData.UserName);
            Equipment bll = new Equipment();
            QueryParam qp = CurrentQueryParam;
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;


            //qp.PageIndex = AspNetPager2.CurrentPageIndex;
            //qp.PageSize = AspNetPager2.PageSize;


            int recordCount = 0;
            IList list = bll.GetList(qp, out recordCount, null);
            //foreach(EquipmentInfoFacade item in list)
            //{
            //    if (UserData.CurrentUserData.IsParentCompany || ((!UserData.CurrentUserData.IsParentCompany) && (item.CompanyID== UserData.CurrentUserData.CompanyID)) || (warehouse != null && warehouse.WareHouseID != null && warehouse.WareHouseID != string.Empty && item.LocationTag == "4" && item.LocationID == warehouse.WareHouseID))
            //    {
            //        item.Visible = true;
                    
            //    }
            //    else
            //        item.Visible = false;

            //}
            GridView1.DataSource = list;
            GridView1.DataBind();

            //GridView2.DataSource = list;
            //GridView2.DataBind();

            AspNetPager1.RecordCount = recordCount;
            //AspNetPager2.RecordCount = recordCount;

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "设备列表初始化失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();

    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        FillData();

    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "view")
        {
            string id = gvRow.Attributes["ID"];
            Response.Redirect("ViewDeviceInfo.aspx?cmd=view&id=" + id + "&companyid=" + companyid);
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
        if (e.CommandName == "WarmingCheck")
        {
            try
            {
                string id = Convert.ToString(gvRow.Attributes["ID"]);
                Equipment bll = new Equipment();
                EquipmentInfoFacade updateEq = bll.GetEquipment(id);
                updateEq.Warming = 1;
                bll.UpdateEquipment(updateEq);
                FillData();
                
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "确认入库失败：" + ex.Message, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
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
            //item.CompanyID = DDLCompany.SelectedValue;
            //item.DetailLocation = Common.inSQL(TextBox_DetailLocation.Text.Trim());
            item.EquipmentNO = Common.inSQL(EquipmentNO.Text.Trim());
            item.Name = Common.inSQL(Name.Text.Trim());
            if (CategoryID.Text != string.Empty)
                item.CategoryID = Convert.ToInt64(CategoryID.Text);
            item.CategoryName = Common.inSQL(CategoryName.Text.Trim());

            //item.AddressCode = Hidden_AddressCode.Value;

            //if (SectionName.SelectedValue != string.Empty)
            //    item.SectionID = Common.inSQL(SectionName.SelectedValue);
            //item.LocationTag = "4";
            //item.LocationID = Common.inSQL(LocationID.Text);
            //if (SystemName.Text.Trim() != string.Empty)
            //{
            //    item.SystemID = SystemID.Text;
            //    item.SystemName = Common.inSQL(SystemName.Text.Trim());
            //}

            item.SystemID = DropDownList_System.SelectedValue;

            //if (!string.IsNullOrEmpty(TextBox_Address.Value.Trim()))
            //{
            //    item.AddressName = Common.inSQL(TextBox_Address.Value.Trim());
            //    item.AddressCode = Hidden_AddressCode.Value;
            //}

            //item.AddressCode = Hidden_WarehouseAddressCode.Value;

            if (PurchaseOrderID.Text != string.Empty)
                item.PurchaseOrderID = PurchaseOrderID.Text.Trim();
            item.SerialNum = Common.inSQL(ddlEqType.SelectedValue);
            item.Model = Common.inSQL(Model.Text.Trim());
            if (Status.SelectedValue != "0")
                item.Status = (EquipmentStatus)Convert.ToInt64(Status.SelectedValue);
            item.Specification = Specification.Text.Trim();
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
            //item.CompanyID = companyid;

            Equipment bll = new Equipment();
            //********************************* Modified by Xue 2011-7-26 *******************
            QueryParam qp = bll.GenerateSearchTermForWarehouse(item, AddressInforList);
            //---QueryParam qp = bll.GenerateSearchTermForWarehouse(item);
            //********************************* Modification Finished *************************
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

    private void Process()
    {
        if (cmd == "export")
        {
            //导出
            string file = Server.MapPath("~/public/2.xls");
            FileStream stream = File.Open(file, FileMode.Open);

            byte[] Buffer = null;
            long size;
            size = stream.Length;
            Buffer = new byte[size];
            stream.Read(Buffer, 0, int.Parse(stream.Length.ToString()));
            stream.Close();
            stream = null;

            HttpContext.Current.Response.ContentType = "application/xls";
            string header = "attachment; filename=" + file;
            HttpContext.Current.Response.AddHeader("content-disposition", header);
            HttpContext.Current.Response.BinaryWrite(Buffer);
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();

        }
    }

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
                Node.Text = item.CategoryID.ToString();
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



    private QueryParam CurrentQueryParam
    {
        get
        {
            QueryParam qp = (QueryParam)ViewState["SearchTerm"];
            if (qp == null)
            {
                if (qp == null)
                {
                    Equipment bll = new Equipment();
                    EquipmentSearchInfo item = new EquipmentSearchInfo();
                    //item.LocationTag = "4";
                    //item.LocationID = warehouse.WareHouseID;
                    //item.AddressCode = Hidden_WarehouseAddressCode.Value;

                    //********************************* Modified by Xue 2011-7-26 *******************
                    item.AddressCode = DropDownList_FilterWareHouse.Items[DropDownList_FilterWareHouse.SelectedIndex].Value;                  
                    //---qp = bll.GenerateSearchTermForWarehouse(item);
                    qp = bll.GenerateSearchTerm(item);
                    //********************************* Modification Finished *************************
                }
                qp.PageIndex = 1;
                qp.PageSize = AspNetPager1.PageSize;
            }
            return qp;
        }
        set { ViewState["SearchTerm"] = value; }
    }


    private void Filter()
    {


        string name = Common.inSQL(TextBox_FilterName.Text.Trim());
        string model = Common.inSQL(TextBox_FilterModel.Text.Trim());
        string systemid = DropDownList_FilterSystem.SelectedValue;
        EquipmentStatus status = (EquipmentStatus)Convert.ToInt32(DropDownList_FilterStatus.SelectedValue);
        int warming = Convert.ToInt32(DropDownList1.SelectedValue);
        EquipmentSearchInfo item = new EquipmentSearchInfo();

        item.Name = name;
        item.Model = model;
        item.SystemID = systemid;
        item.Status = status;
        item.AddressCode = Hidden_WarehouseAddressCode.Value;
        if (warming!=0)
        {
            item.Warming = warming;
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
}
