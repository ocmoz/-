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
using FM2E.BLL.Maintain;
using FM2E.Model.Maintain;

public partial class Module_FM2E_MaintainManager_DailyPatrolManager_DailyPatrolConfig_SelectDevice : System.Web.UI.Page
{

    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    string companyid = UserData.CurrentUserData.CompanyID;
    protected string IsShow = "block";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonBind();
            InitialPage();
            FillData();
            FillData2();
        }
    }
    /// <summary>
    /// 获取或设置选中项的集合
    /// </summary>
    protected ArrayList SelectedItems
    {
        get
        {
            return (ViewState["mySelectedItems"] != null) ? (ArrayList)ViewState["mySelectedItems"] : null;
        }
        set
        {
            ViewState["mySelectedItems"] = value;
        }
    }
    private void PermissionControl()
    {

    }

    private void ButtonBind()
    {
        HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[1];
        button.ButtonUrlType = UrlType.Href;
        button.ButtonUrl = string.Format("ViewDailyPatrolConfig.aspx?cmd=view&id={0}", id);
    }

    private void InitialPage()
    {
        try
        {
            //设备类型
            string[] eqtype = FM2E.BLL.System.ConfigItems.EqType;
            foreach (string s in eqtype)
            {
                ddlEqType.Items.Add(new ListItem(s, s));
            }


            MaintainPlanConfig configbll = new MaintainPlanConfig();
            this.SelectedItems = (ArrayList)configbll.GetAllEquipmentByItemID(id);

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

            EquipmentSystem systemBll = new EquipmentSystem();
            DropDownList_System.Items.AddRange(systemBll.GenerateListItemCollectionWithBlank());

            //设备状态
            Status.Items.Clear();
            Status.Items.AddRange(EnumHelper.GetListItems(typeof(EquipmentStatus)));

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
            //MaintainPlanConfig configbll = new MaintainPlanConfig();
            //this.SelectedItems = (ArrayList)configbll.GetAllEquipmentByItemID(id);
            Equipment bll = new Equipment();
            QueryParam qp = (QueryParam)ViewState["SearchTerm"];
            if (qp == null)
            {
                qp = new QueryParam();


            }
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;
            Warehouse bllwh = new Warehouse();
            WarehouseInfo warehouse = bllwh.GetWarehouseByUserName(UserData.CurrentUserData.UserName);
            int recordCount = 0;
            IList list = bll.GetList(qp, out recordCount, null);

            GridView1.DataSource = list;
            GridView1.DataBind();

            AspNetPager1.RecordCount = recordCount;
            //SetSelectedAll();
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
        CollectSelected();
        FillData();
    }
    /// <summary>
    /// 初始化数据列表
    /// </summary>
    private void FillData2()
    {
        try
        {
            MaintainPlanConfig bll = new MaintainPlanConfig();
            MaintainPlanConfigInfo item = new MaintainPlanConfigInfo();
            item.ItemID = id;

            QueryParam qp = bll.GenerateSearchTermForEquipmentList(item);
            qp.PageIndex = AspNetPager2.CurrentPageIndex;
            qp.PageSize = AspNetPager2.PageSize;

            int recordCount = 0;
            IList list = bll.GetListForEquipmentList(qp,out recordCount);
            
            GridView2.DataSource = list;
            GridView2.DataBind();

            AspNetPager2.RecordCount = recordCount;
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
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        FillData2();
    }
    /// <summary>
    /// 判断当页是否全选
    /// </summary>
    protected void SetSelectedAll()
    {
        ArrayList selectedItems = null;
        if (this.SelectedItems == null)
            selectedItems = new ArrayList();
        else
            selectedItems = this.SelectedItems;

        HtmlInputCheckBox cbAll = this.GridView1.HeaderRow.FindControl("CheckAll") as HtmlInputCheckBox;
        ArrayList list = new ArrayList();
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            string id = this.GridView1.Rows[i].Attributes["eNO"].ToString();
            CheckBox cb = this.GridView1.Rows[i].FindControl("checkBox1") as CheckBox;
            if (selectedItems.Contains(id))
                list.Add(id);
        }
        if (list.Count.Equals(this.GridView1.Rows.Count))
            cbAll.Checked = true;
        else
            cbAll.Checked = false;
    }
    /// <summary>
    /// 获取已选记录
    /// </summary>
    protected void CollectSelected()
    {
        ArrayList selectedItems = null;
        if (this.SelectedItems == null)
            selectedItems = new ArrayList();
        else
            selectedItems = this.SelectedItems;

        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            string id = this.GridView1.Rows[i].Attributes["eNO"].ToString();
            CheckBox cb = this.GridView1.Rows[i].FindControl("checkBox1") as CheckBox;
            if (selectedItems.Contains(id) && !cb.Checked)
                selectedItems.Remove(id);
            if (!selectedItems.Contains(id) && cb.Checked)
                selectedItems.Add(id);
        }
        this.SelectedItems = selectedItems;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;
        try
        {
            CollectSelected();
            MaintainPlanConfig bll = new MaintainPlanConfig();
            MaintainPlanConfigInfo info = bll.GetMaintainPlanConfig(id);
            info.EquipmentList = this.SelectedItems;
            bll.UpdateEquipments(info);
            bSuccess = true;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "配置设备失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        if (bSuccess)
        {
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "配置设备成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("SelectDevice.aspx?cmd=view&id=" + id), UrlType.Href, "");
        }
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView2.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "del")
        {
            try
            {
                string equipment = Convert.ToString(gvRow.Attributes["eNO"]);
                MaintainPlanConfig bll = new MaintainPlanConfig();
                bll.DelMaintainPlanEquipment(equipment, id);
                FillData();
                FillData2();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败：" + ex.Message, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
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
            e.Row.Attributes["eNO"] = item.EquipmentNO.ToString();

            if (e.Row.RowIndex > -1 && this.SelectedItems != null)
            {
                CheckBox cb = e.Row.FindControl("checkBox1") as CheckBox;
                if (this.SelectedItems.Contains(e.Row.Attributes["eNO"]))
                    cb.Checked = true;
                else
                    cb.Checked = false;
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

            EquipmentInfoFacade item = (EquipmentInfoFacade)e.Row.DataItem;

            e.Row.Attributes["eNO"] = item.EquipmentNO.ToString();
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
            }
            //if (SystemName.Text.Trim() != string.Empty)
            //{
            //    item.SystemID = SystemID.Text;
            //    item.SystemName = Common.inSQL(SystemName.Text.Trim());
            //}
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
            QueryParam qp = bll.GenerateSearchTerm(item);
            AspNetPager1.CurrentPageIndex = 1;
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;
            ViewState["SearchTerm"] = qp;
            FillData();
            TabContainer1.ActiveTabIndex = 1;


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





}
