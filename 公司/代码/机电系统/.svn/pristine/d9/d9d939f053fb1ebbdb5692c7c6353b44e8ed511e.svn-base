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
using FM2E.Model.Basic;
using FM2E.BLL.Basic;
using FM2E.Model.Utils;
using WebUtility;
using WebUtility.Components;
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using FM2E.BLL.Utils;
using CrystalDecisions.CrystalReports.Engine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public partial class Module_FM2E_DeviceManager_AssetManager_AssetAndDepreciation_AssetAndDepreciation : System.Web.UI.Page
{
    ReportDocument reportdocument = new ReportDocument();
    string companyid = UserData.CurrentUserData.CompanyID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
        }

    }

    private void Page_Unload(object sender, EventArgs e)
    {
        reportdocument.Dispose();
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


            Session.Remove("printdateset");
            Session.Remove("printloadpath");
            //this.CascadingDropDown2.Category += companyid;
            AddTree1(0, (TreeNode)null);
            TreeView1.ShowLines = true;
            //FM2E.BLL.Basic.Section bll = new FM2E.BLL.Basic.Section();
            //SectionInfo sectioninfo = new SectionInfo();
            //sectioninfo.CompanyID = companyid;
            //QueryParam sectionqp = bll.GenerateSearchTerm(sectioninfo);
            //sectionqp.PageSize = 500;
            //int sectionrc = 0;
            //IList sectionlist = bll.GetList(sectionqp, out sectionrc);
            //foreach (SectionInfo item in sectionlist)
            //{
            //    SectionName.Items.Add(new ListItem(item.SectionName, item.SectionID));
            //}
            AddTree2(0, (TreeNode)null);
            TreeView2.ShowLines = true;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败：" + ex.Message, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 初始化数据列表
    /// </summary>
    //private void FillData()
    //{
    //    try
    //    {
    //        QueryParam qp = (QueryParam)ViewState["SearchTerm"];
    //        if (qp == null)
    //        {
    //            qp = new QueryParam();
    //        }

    //        //GridView1.DataSource = list;
    //        //GridView1.DataBind();


    //    }
    //    catch (Exception ex)
    //    {
    //        EventMessage.MessageBox(Msg_Type.Error, "操作失败", "设备列表初始化失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
    //    }
    //}
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
    /// <summary>
    /// 选择种类事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        CategoryID.Text = this.TreeView1.SelectedNode.Value;
        CategoryName.Text = this.TreeView1.SelectedNode.Text;
        PopupControlExtender1.Commit(CategoryName.Text);
        PopupControlExtender2.Commit(CategoryID.Text);
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

            if (scope.SelectedValue == string.Empty)
            {
                GridView1.Columns[0].Visible = true;
            }
            else GridView1.Columns[0].Visible = false;

            if (SystemName.Text != string.Empty || scope.SelectedValue == "1" || scope2.SelectedValue == "1")
            {
                GridView1.Columns[1].Visible = true;

            }
            else GridView1.Columns[1].Visible = false;

            if (CategoryName.Text != string.Empty || scope.SelectedValue == "2" || scope2.SelectedValue == "2")
            {
                GridView1.Columns[2].Visible = true;

            }
            else GridView1.Columns[2].Visible = false;

            //if (SectionName.SelectedIndex == 0)
            //    GridView1.Columns[3].Visible = false;
            //else GridView1.Columns[3].Visible = true;

            //if (LocationID.SelectedValue == string.Empty)
            //    GridView1.Columns[4].Visible = false;
            //else GridView1.Columns[4].Visible = true;

            if (!string.IsNullOrEmpty(TextBox_Address.Value.Trim()))
                GridView1.Columns[3].Visible = true;
            else
                GridView1.Columns[3].Visible = false;

            if (ddlEqType.SelectedValue == string.Empty)
                GridView1.Columns[4].Visible = false;
            else GridView1.Columns[4].Visible = true;
            if (Name.Text == string.Empty)
                GridView1.Columns[5].Visible = false;
            else GridView1.Columns[5].Visible = true;
            if (EquipmentNO.Text == string.Empty)
                GridView1.Columns[6].Visible = false;
            else GridView1.Columns[6].Visible = true;
            if (Model.Text == string.Empty)
                GridView1.Columns[7].Visible = false;
            else GridView1.Columns[7].Visible = true;

            EquipmentSearchInfo item = new EquipmentSearchInfo();
            item.CompanyID = companyid;
            if (CategoryID.Text != string.Empty && CategoryName.Text != string.Empty)
            {
                item.CategoryID = Convert.ToInt64(Common.inSQL(CategoryID.Text));
                Category categorybll = new Category();
                item.CategoryCode = ((CategoryInfo)categorybll.GetCategory(item.CategoryID)).CategoryCode;
            }
            item.CategoryName = CategoryName.Text;
            item.GroupBy = scope.SelectedValue;
            item.GroupBy2 = scope2.SelectedValue;
            item.SystemName = SystemName.Text;
            //if (SectionName.SelectedIndex != 0)
            //    item.SectionName = Common.inSQL(SectionName.SelectedItem.Text).Trim();
            item.EquipmentNO = Common.inSQL(EquipmentNO.Text).Trim();
            item.Name = Common.inSQL(Name.Text).Trim();
            item.SerialNum = Common.inSQL(ddlEqType.SelectedValue).Trim();
            item.Model = Common.inSQL(Model.Text).Trim();
            if (!string.IsNullOrEmpty(TextBox_Address.Value.Trim()))
            {
                item.AddressName = Common.inSQL(TextBox_Address.Value.Trim());
                item.AddressID = Convert.ToInt64(Hidden_AddressID.Value);
                Address addressbll = new Address();
                item.AddressCode = ((AddressInfo)addressbll.GetAddress(item.AddressID)).AddressCode;
            }
            item.IsCancel = 1;
            //item.LocationTag = LocationTag.SelectedValue;
            //item.LocationID = LocationID.SelectedValue;
            Equipment bll = new Equipment();
            IList typelist = bll.Gettypelist(item);
            IList list = (List<EquipmentInfoFacade>)bll.Search(item);
            DataSet dataset = new DataSet();
            DataTable datatable = new DataTable("ReportView");
            dataset.Tables.Add(datatable);
            DataColumn column;

            if (scope.SelectedValue == string.Empty)//按公司汇总
            {
                column = new DataColumn();
                column.ColumnName = "Price";
                column.DataType = Type.GetType("System.String");
                column.Unique = false;
                datatable.Columns.Add(column);
                column = new DataColumn();
                column.ColumnName = "DepreciationPrice";
                column.DataType = Type.GetType("System.Decimal");
                column.Unique = false;
                datatable.Columns.Add(column);
                foreach (EquipmentInfoFacade model in typelist)
                {
                    model.SerialNum = ddlEqType.SelectedValue;
                    model.Name = Name.Text;
                    model.EquipmentNO = EquipmentNO.Text;
                    model.AddressName = TextBox_Address.Value;
                    model.Model = Model.Text;
                    foreach (EquipmentInfoFacade model2 in list)
                    {
                        model.Price += model2.Price;
                        DateTime begintime = model2.PurchaseDate.AddMonths(1);
                        switch (model2.DepreciationMethod)
                        {
                            case 1:
                                {
                                    model.DepreciationPrice += model2.Price;
                                    break;
                                }
                            case 2:
                                {
                                    model.DepreciationPrice += DepreciationMethod.GetResidualAssetByLinearReduce(begintime.Year,
                                begintime.Month, DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(model2.DepreciableLife),
                                model2.Price, Convert.ToDouble(model2.ResidualRate));
                                    break;
                                }
                            case 3:
                                {
                                    model.DepreciationPrice += DepreciationMethod.GetResidualAssetByDoubleReduce(begintime.Year,
                                begintime.Month, DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(model2.DepreciableLife),
                                model2.Price, Convert.ToDouble(model2.ResidualRate));
                                    break;
                                }
                        }

                    }
                    DataRow datarow;
                    datarow = datatable.NewRow();
                    datarow["Price"] = "净值";
                    datarow["DepreciationPrice"] = model.DepreciationPrice;
                    datatable.Rows.Add(datarow);
                    datarow = datatable.NewRow();
                    datarow["Price"] = "折旧值";
                    datarow["DepreciationPrice"] = model.Price - model.DepreciationPrice;
                    datatable.Rows.Add(datarow);


                    reportdocument.Load(Server.MapPath("~") + "/report/reportbycompany.rpt");
                    reportdocument.SetDataSource(dataset);
                    //Session["printloadpath"] = Server.MapPath("~") + "/report/reportbycompany.rpt";
                    //Session["printdateset"] = dataset;
                    CrystalReportViewer1.ReportSource = reportdocument;
                    CrystalReportViewer1.DataBind();

                }

            }
            else if (scope2.SelectedValue == string.Empty)
            {
                if (scope.SelectedValue == "1")//按系统汇总
                {
                    column = new DataColumn();
                    column.ColumnName = "SystemName";
                    column.DataType = Type.GetType("System.String");
                    column.Unique = false;
                    datatable.Columns.Add(column);
                    column = new DataColumn();
                    column.ColumnName = "Price";
                    column.DataType = Type.GetType("System.String");
                    column.Unique = false;
                    datatable.Columns.Add(column);
                    column = new DataColumn();
                    column.ColumnName = "DepreciationPrice";
                    column.DataType = Type.GetType("System.Decimal");
                    column.Unique = false;
                    datatable.Columns.Add(column);
                    foreach (EquipmentInfoFacade model in typelist)
                    {
                        model.AddressName = TextBox_Address.Value;
                        model.SerialNum = ddlEqType.SelectedValue;
                        model.Name = Name.Text;
                        model.EquipmentNO = EquipmentNO.Text;
                        model.Model = Model.Text;
                        foreach (EquipmentInfoFacade model2 in list)
                        {
                            if (model2.SystemName == model.SystemName)
                            {
                                model.Price += model2.Price;
                                DateTime begintime = model2.PurchaseDate.AddMonths(1);
                                switch (model2.DepreciationMethod)
                                {
                                    case 1:
                                        {
                                            model.DepreciationPrice += model2.Price;
                                            break;
                                        }
                                    case 2:
                                        {
                                            model.DepreciationPrice += DepreciationMethod.GetResidualAssetByLinearReduce(begintime.Year,
                                        begintime.Month, DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(model2.DepreciableLife),
                                        model2.Price, Convert.ToDouble(model2.ResidualRate));
                                            break;
                                        }
                                    case 3:
                                        {
                                            model.DepreciationPrice += DepreciationMethod.GetResidualAssetByDoubleReduce(begintime.Year,
                                        begintime.Month, DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(model2.DepreciableLife),
                                        model2.Price, Convert.ToDouble(model2.ResidualRate));
                                            break;
                                        }
                                }
                            }
                        }
                        DataRow datarow;
                        datarow = datatable.NewRow();
                        datarow["SystemName"] = model.SystemName;
                        datarow["Price"] = "净值";
                        datarow["DepreciationPrice"] = model.DepreciationPrice;
                        datatable.Rows.Add(datarow);
                        datarow = datatable.NewRow();
                        datarow["SystemName"] = model.SystemName;
                        datarow["Price"] = "折旧值";
                        datarow["DepreciationPrice"] = model.Price - model.DepreciationPrice;
                        datatable.Rows.Add(datarow);

                    }

                    reportdocument.Load(Server.MapPath("~") + "/report/reportbysystem.rpt");
                    reportdocument.SetDataSource(dataset);
                    //Session["printloadpath"] = Server.MapPath("~") + "/report/reportbysystem.rpt";
                    //Session["printdateset"] = dataset;
                    CrystalReportViewer1.ReportSource = reportdocument;
                    CrystalReportViewer1.DataBind();
                }
                else
                {
                    column = new DataColumn();
                    column.ColumnName = "SystemName";
                    column.DataType = Type.GetType("System.String");
                    column.Unique = false;
                    datatable.Columns.Add(column);
                    column = new DataColumn();
                    column.ColumnName = "Price";
                    column.DataType = Type.GetType("System.String");
                    column.Unique = false;
                    datatable.Columns.Add(column);
                    column = new DataColumn();
                    column.ColumnName = "DepreciationPrice";
                    column.DataType = Type.GetType("System.Decimal");
                    column.Unique = false;
                    datatable.Columns.Add(column);
                    foreach (EquipmentInfoFacade model in typelist)//按种类汇总
                    {
                        model.AddressName = TextBox_Address.Value;
                        model.SerialNum = ddlEqType.SelectedValue;
                        model.Name = Name.Text;
                        model.EquipmentNO = EquipmentNO.Text;
                        model.Model = Model.Text;
                        foreach (EquipmentInfoFacade model2 in list)
                        {
                            if (String.CompareOrdinal(model.CategoryCode, 0, model2.CategoryCode, 0, model.CategoryCode.Length) == 0)
                            {
                                model.Price += model2.Price;
                                DateTime begintime = model2.PurchaseDate.AddMonths(1);
                                switch (model2.DepreciationMethod)
                                {
                                    case 1:
                                        {
                                            model.DepreciationPrice += model2.Price;
                                            break;
                                        }
                                    case 2:
                                        {
                                            model.DepreciationPrice += DepreciationMethod.GetResidualAssetByLinearReduce(begintime.Year,
                                        begintime.Month, DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(model2.DepreciableLife),
                                        model2.Price, Convert.ToDouble(model2.ResidualRate));
                                            break;
                                        }
                                    case 3:
                                        {
                                            model.DepreciationPrice += DepreciationMethod.GetResidualAssetByDoubleReduce(begintime.Year,
                                        begintime.Month, DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(model2.DepreciableLife),
                                        model2.Price, Convert.ToDouble(model2.ResidualRate));
                                            break;
                                        }
                                }
                            }
                        }
                        DataRow datarow;
                        datarow = datatable.NewRow();
                        datarow["SystemName"] = model.CategoryName.Replace("-", "");
                        datarow["Price"] = "净值";
                        datarow["DepreciationPrice"] = model.DepreciationPrice;
                        datatable.Rows.Add(datarow);
                        datarow = datatable.NewRow();
                        datarow["SystemName"] = model.CategoryName.Replace("-", "");
                        datarow["Price"] = "折旧值";
                        datarow["DepreciationPrice"] = model.Price - model.DepreciationPrice;
                        datatable.Rows.Add(datarow);

                    }

                    reportdocument.Load(Server.MapPath("~") + "/report/reportbycategory.rpt");
                    reportdocument.SetDataSource(dataset);
                    //Session["printloadpath"] = Server.MapPath("~") + "/report/reportbycategory.rpt";
                    //Session["printdateset"] = dataset;
                    CrystalReportViewer1.ReportSource = reportdocument;
                    CrystalReportViewer1.DataBind();
                }

            }
            else//按系统和种类两级汇总
            {
                column = new DataColumn();
                column.ColumnName = "SystemName";
                column.DataType = Type.GetType("System.String");
                column.Unique = false;
                datatable.Columns.Add(column);
                column = new DataColumn();
                column.ColumnName = "CategoryName";
                column.DataType = Type.GetType("System.String");
                column.Unique = false;
                datatable.Columns.Add(column);
                column = new DataColumn();
                column.ColumnName = "Price";
                column.DataType = Type.GetType("System.String");
                column.Unique = false;
                datatable.Columns.Add(column);
                column = new DataColumn();
                column.ColumnName = "DepreciationPrice";
                column.DataType = Type.GetType("System.Decimal");
                column.Unique = false;
                datatable.Columns.Add(column);
                foreach (EquipmentInfoFacade model in typelist)
                {
                    model.AddressName = TextBox_Address.Value;
                    model.SerialNum = ddlEqType.SelectedValue;
                    model.Name = Name.Text;
                    model.EquipmentNO = EquipmentNO.Text;
                    model.Model = Model.Text;
                    foreach (EquipmentInfoFacade model2 in list)
                    {
                        if (model2.SystemName == model.SystemName && String.CompareOrdinal(model.CategoryCode, 0, model2.CategoryCode, 0, model.CategoryCode.Length) == 0)
                        {
                            model.Price += model2.Price;
                            DateTime begintime = model2.PurchaseDate.AddMonths(1);
                            switch (model2.DepreciationMethod)
                            {
                                case 1:
                                    {
                                        model.DepreciationPrice += model2.Price;
                                        break;
                                    }
                                case 2:
                                    {
                                        model.DepreciationPrice += DepreciationMethod.GetResidualAssetByLinearReduce(begintime.Year,
                                    begintime.Month, DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(model2.DepreciableLife),
                                    model2.Price, Convert.ToDouble(model2.ResidualRate));
                                        break;
                                    }
                                case 3:
                                    {
                                        model.DepreciationPrice += DepreciationMethod.GetResidualAssetByDoubleReduce(begintime.Year,
                                    begintime.Month, DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(model2.DepreciableLife),
                                    model2.Price, Convert.ToDouble(model2.ResidualRate));
                                        break;
                                    }
                            }
                        }
                    }
                    DataRow datarow;
                    datarow = datatable.NewRow();
                    datarow["SystemName"] = model.SystemName;
                    datarow["CategoryName"] = model.CategoryName.Replace("-", "");
                    datarow["Price"] = "净值";
                    datarow["DepreciationPrice"] = model.DepreciationPrice;
                    datatable.Rows.Add(datarow);
                    datarow = datatable.NewRow();
                    datarow["SystemName"] = model.SystemName;
                    datarow["CategoryName"] = model.CategoryName.Replace("-", "");
                    datarow["Price"] = "折旧值";
                    datarow["DepreciationPrice"] = model.Price - model.DepreciationPrice;
                    datatable.Rows.Add(datarow);
                }
                reportdocument.Load(Server.MapPath("~") + "/report/reportbysystemandcategory.rpt");
                reportdocument.SetDataSource(dataset);
                //Session["printloadpath"] = Server.MapPath("~") + "/report/reportbysystemandcategory.rpt";
                //Session["printdateset"] = dataset;
                CrystalReportViewer1.ReportSource = reportdocument;
                CrystalReportViewer1.DataBind();
            }


            //DataSet dataset = ConvertIListToDataSet.ConvertToDataSet(typelist);
            GridView1.DataSource = typelist;
            GridView1.DataBind();

            TabContainer1.ActiveTabIndex = 1;

            Session["typelist"] = typelist;

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "统计折旧失败：" + ex.Message, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
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
                Node.Value = item.CategoryID.ToString();
                e.Node.ChildNodes.Add(Node);
                Node.PopulateOnDemand = true;
                Node.Expanded = false;
            }
        }
    }
    /// <summary>
    /// 选择所属系统事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
    {
        SystemName.Text = this.TreeView2.SelectedNode.Text;
        SystemID.Text = this.TreeView2.SelectedValue;
        PopupControlExtender3.Commit(SystemName.Text);
        PopupControlExtender4.Commit(SystemID.Text);
    }
    /// <summary>
    /// 初始化系统弹出树
    /// </summary>
    /// <param name="ParentID"></param>
    /// <param name="pNode"></param>
    public void AddTree2(long ParentID, TreeNode pNode)
    {
        EquipmentSystem bll = new EquipmentSystem();
        IList rootsystem = bll.GetAllSystem();
        foreach (EquipmentSystemInfo item in rootsystem)
        {
            TreeNode Node = new TreeNode();
            Node.Text = item.SystemName;
            Node.Value = item.SystemID;
            TreeView2.Nodes.Add(Node);

            Node.Expanded = true;
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            //EquipmentInfoFacade olditem = ((List<EquipmentInfoFacade>)ViewState["typelist"])[Convert.ToInt32(e.CommandArgument)];
            //MemoryStream stream = new MemoryStream();
            //BinaryFormatter formatter = new BinaryFormatter();
            //stream.Position = 0;
            //formatter.Serialize(stream, olditem);
            //stream.Position = 0;
            //Session["newitem"]  = (EquipmentSearchInfo)formatter.Deserialize(stream);
            //ScriptManager.RegisterClientScriptBlock(gridviewdiv, this.GetType(), "click ", "alert( '该角色为系统默认角色不可以删除   ! ');", true); 


        }
    }

}
