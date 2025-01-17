﻿using System;
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
using FM2E.BLL.BarCode;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using System.Runtime.Serialization.Formatters.Binary;

public partial class Module_FM2E_DeviceManager_DeviceInfo_CurrentEuipementInfo_EquipmentExport_EquipmentExportList : System.Web.UI.Page
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
            Session.Remove(Constants.BARCODE_SESSION_STRING);
            Session.Remove("DevicePreviewList");
            InitialPage();
            FillData();
            //Process();
            TypeTreeSelect();
        }
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
            IList list = bll.GetList(qp, out recordCount, null);
            Department dpbll = new Department();
            DepartmentInfo dpitem = dpbll.GetDepartment(UserData.CurrentUserData.DepartmentID);
            //判断仓管员、普通用户、管理员之间的
            //仓管员只能修改自己的产品、普通用户根据权限配置，管理员不受权限控制
            foreach (EquipmentInfoFacade item in list)
            {

                if ((UserData.CurrentUserData.IsAdministrator && UserData.CurrentUserData.IsParentCompany)
                        || (UserData.CurrentUserData.IsAdministrator && (!UserData.CurrentUserData.IsParentCompany) && (UserData.CurrentUserData.CompanyID == item.CompanyID))
                        || (SystemPermission.CheckPermission(PopedomType.Delete) && (item.CompanyID == UserData.CurrentUserData.CompanyID))
                        || (SystemPermission.CheckPermission(PopedomType.Delete) && (dpitem.DepartmentType == DepartmentType.MaintainTeam)))
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

            Session["DevicePreviewList"] = list;

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
            ScriptManager1.AddHistoryPoint("currentaddress", ViewState["currentaddress"] != null ? ViewState["currentaddress"].ToString() : "");
        }
        FillData();

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
        }

    }

    private void TypeTreeSelect()
    {
        if (typetree != String.Empty)
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
            EquipmentSearchInfo item = null;
            item = setSearchItem();

            Equipment bll = new Equipment();
            QueryParam qp = bll.GenerateSearchTerm(item);
            AspNetPager1.CurrentPageIndex = 1;
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;
            CurrentQueryParam = qp;
            FillData();

            TabContainer1.ActiveTabIndex = 0;
            changeAddressView();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询设备失败：" + ex.Message, ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
    }

    private EquipmentSearchInfo setSearchItem()
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




        return item;


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
                Node.Value = item.CategoryID.ToString();
                e.Node.ChildNodes.Add(Node);
                Node.PopulateOnDemand = true;
                Node.Expanded = false;
            }
        }
    }

    /// <summary>
    /// 选择节点事情
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    #region 地址树

    private readonly Address addressBll = new Address();


    /// <summary>
    /// 建立地址树
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

        //EquipmentSearchInfo item = new EquipmentSearchInfo();
        //item.CompanyID = companyid;
        //item.AddressCode = address.AddressCode;
        ViewState["currentaddress"] = address.ID;
        //当是根结点时，把地址置为"",避免查找时对地址like查询出错
        if (address.ID == 1)
        {
            TextBox_Address.Value = "";
        }
        else
        {
            TextBox_Address.Value = address.AddressFullName;
        }


        Hidden_AddressCode.Value = address.AddressCode;

        EquipmentSearchInfo item = setSearchItem();
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
    /************************************************************************/
    /* 根据高级搜索中选择的项，重置addressView                                             */
    /************************************************************************/
    protected void changeAddressView()
    {
        string addressId = Hidden_AddressCode.Value;
        AddressInfo addressInfo = addressBll.GetAddressByAddressCode(addressId);
        if (addressInfo != null)
        {
            OperNodeByID(addressInfo.ID.ToString(), addressTree.Nodes, ref addressTree);
            expandSeletectAddressTree();
        }

    }
    /// <summary>
    /// 将node到root结点中所有结点展开
    /// </summary>
    protected void expandSeletectAddressTree()
    {
        TreeNode node = addressTree.SelectedNode;

        while (node.Parent != null)
        {
            node.Expand();
            node = node.Parent;
        }
    }


    //导出
    protected void btnExport_Click(object sender, EventArgs e)
    {

        Equipment eqbll = new Equipment();
        EquipmentSearchInfo item = setSearchItem();

        Equipment bll = new Equipment();
        QueryParam searchTerm = bll.GenerateSearchTerm(item);
        IList list = eqbll.GetExportList(searchTerm);
        try
        {
            string timeFile = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".csv";
            string fileName = "设备台账导出" + timeFile;
            string filepath = Server.MapPath("~/public") + "/" + timeFile;
            FileStream fs = File.Create(filepath);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);

            if (list != null)
            {
                sw.Write("序号,");
                sw.Write("设备名称,");
                sw.Write("型号,");
                sw.Write("单位,");
                sw.Write("设备种类,");
                sw.Write("价格,");
                sw.Write("数量,");
                sw.Write("资产编号,");
                sw.Write("序列号,");
                sw.Write("设备地址信息,");
                sw.Write("安装位置,");
                sw.Write("所属系统,");
                sw.Write("公司,");
                sw.Write("采购日期,");
                sw.Write("备注");
                sw.Write("\r\n");
                int i = 1;
                foreach (EquipmentExportInfo info in list)
                {
                    sw.Write(i + ",");
                    sw.Write(info.Name + ",");
                    sw.Write(info.Model + ",");
                    sw.Write(info.Unit + ",");
                    sw.Write(info.CategoryName + ",");
                    sw.Write(info.Price + ",");
                    sw.Write(info.Count + ",");
                    sw.Write(info.AssertNumber + ",");
                    sw.Write(info.SerialNum + ",");
                    sw.Write(info.AddressName + ",");
                    sw.Write(info.DetailLocation + ",");
                    sw.Write(info.SystemName + ",");
                    sw.Write(info.CompanyName + ",");
                    sw.Write(info.PurchaseDate + ",");
                    sw.Write(info.Remark + ",");
                    sw.Write("\r\n");
                }
                sw.Flush();
                sw.Close();
                fs.Close();
                Response.ClearContent();
                Response.ClearHeaders();

                Response.ContentType = "application/vnd.ms-excel";

                Response.AddHeader("Content-Disposition", "inline;filename=" + HttpUtility.UrlEncode(fileName));
                Response.WriteFile(filepath);//FileName为Excel文件所在地址

                Response.Flush();
                Response.Close();
                File.Delete(filepath);
                Response.End();
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取申请信息失败" + ex.ToString(), ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", "导入成功：" + list.Count, Icon_Type.OK, false, Common.GetHomeBaseUrl("EquipmentExportList.aspx"), UrlType.Href, "");
    }
}
