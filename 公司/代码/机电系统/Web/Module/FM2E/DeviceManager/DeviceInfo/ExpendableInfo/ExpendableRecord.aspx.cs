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
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using System.Collections.Generic;
using FM2E.Model.Exceptions;

public partial class Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_ExpendableRecord : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    protected string IsShow = "block";
    protected string IsShow2 = "block";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AddTree(0, (TreeNode)null);
            TreeView1.ShowLines = true;
            FillData();
        }
    }

    private void FillData()
    {
        //公司DROPDOWNLIST
        Company company = new FM2E.BLL.Basic.Company();
        IList<CompanyInfo> list = company.GetAllCompany();
        DDLCompany.Items.Clear();
        DDLCompany.Items.Add(new ListItem("请选择公司", "0"));
        foreach (CompanyInfo item in list)
        {
            DDLCompany.Items.Add(new ListItem(item.CompanyName, item.CompanyID));
        }
        //仓库DROPDOWNLIST
        Warehouse bll1 = new Warehouse();
        IList<WarehouseInfo> list1 = bll1.GetAllWarehouse();

        DDLWarehouse.Items.Clear();
        DDLWarehouse.Items.Add(new ListItem("请选择仓库", ""));
        foreach (WarehouseInfo item in list1)
        {
            DDLWarehouse.Items.Add(new ListItem(item.Name, item.WareHouseID.ToString()));
        }

        Warehouse bllstaff = new Warehouse();
        //if (UserData.CurrentUserData.IsParentCompany == false)
        //{
        WarehouseInfo warehouse = bllstaff.GetWarehouseByUserName(UserData.CurrentUserData.UserName);
        if (warehouse == null || warehouse.WareHouseID == null || warehouse.WareHouseID == "")
        {
            if (!UserData.CurrentUserData.IsParentCompany)
            {
                CascadingDropDown1.SelectedValue = UserData.CurrentUserData.CompanyID;
                IsShow = "none";
                divcompany.InnerText = UserData.CurrentUserData.CompanyName;
            }
        }
        else
        {
            CascadingDropDown1.SelectedValue = UserData.CurrentUserData.CompanyID;
            CascadingDropDown2.SelectedValue = warehouse.WareHouseID;
            IsShow = "none";
            IsShow2 = "none";
            divcompany.InnerText = UserData.CurrentUserData.CompanyName;
            divwarehouse.InnerText = warehouse.Name;
        }
        //}
        //单位DROPDOWNLIST
        DDLUnit.Items.Clear();
        IList list2 = Constants.GetUnits();
        foreach (string unit in list2)
            DDLUnit.Items.Add(new ListItem(unit, unit));
        DDLUnit.Items.Insert(0, new ListItem("请选择单位", ""));
        if (UserData.CurrentUserData.CompanyID != null && UserData.CurrentUserData.CompanyID != string.Empty)
        {
            CascadingDropDown1.SelectedValue = UserData.CurrentUserData.CompanyID;
        }
        if (cmd == "edit")
        {
            try
            {
                ExpendableInfo item;
                Expendable bll = new Expendable();
                item = bll.GetExpendable(id);
                TBName.Text = item.Name;
                CascadingDropDown1.SelectedValue = item.CompanyID;
                CascadingDropDown2.SelectedValue = item.WarehouseID;
                TBModel.Text = item.Model.ToString();
                TBSpecification.Text = item.Specification;
                TBCount.Text = string.Format("{0:#,0.#####}", item.Count);
                DDLUnit.SelectedValue = item.Unit;
                TBUpdateDate.Text = item.UpdateTime.ToString("yyyy-MM-dd");
                TARemark.Value = item.Remark;
                if (item.PhotoUrl == "")
                    ImageButton1.Visible = false;
                ViewState["PhotoUrl"] = item.PhotoUrl;
                FileUpload1.Visible = true;
                if (item.CategoryID != 0)
                {
                    CategoryName.Text = item.CategoryName;
                    CategoryID.Text = item.CategoryID.ToString();
                }
                TBPrice.Text = item.Price.ToString("0.##");
                if (item.PhotoUrl != null && item.PhotoUrl != "")
                {
                    if (System.IO.File.Exists(Server.MapPath(item.PhotoUrl)))
                    {
                        ImageButton1.ImageUrl = item.PhotoUrl;
                        Image1.ImageUrl = item.PhotoUrl;
                    }

                    else
                    {
                        ImageButton1.ImageUrl = "~/images/deletedpicture.gif";
                        Image1.ImageUrl = "~/images/deletedpicture.gif";
                    }
                }
                else
                {
                    ImageButton1.ImageUrl = "~/images/nopicture.gif";
                    Image1.ImageUrl = "~/images/nopicture.gif";
                }
                IsShow = "none";
                IsShow2 = "none";
                divcompany.InnerText = item.CompanyName;
                divwarehouse.InnerText = item.WarehouseName;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;
        if (cmd == "edit")
        {
            ExpendableInfo item = new ExpendableInfo();
            item.Name = TBName.Text.Trim();
            item.CompanyID = DDLCompany.SelectedValue;
            item.WarehouseID = DDLWarehouse.SelectedValue;
            if (TBCount.Text.Trim() != "")
                item.Count = Convert.ToDecimal(TBCount.Text.Trim());
            else
                item.Count = 0;
            item.Model = TBModel.Text.Trim();
            item.Specification = TBSpecification.Text.Trim();
            item.Unit = DDLUnit.SelectedValue;
            item.Remark = TARemark.Value.Trim();
            item.UpdateTime = Convert.ToDateTime(TBUpdateDate.Text.Trim());
            item.PhotoUrl = (string)ViewState["PhotoUrl"];

            if (TBPrice.Text.Trim() != "")
                item.Price = Convert.ToDecimal(TBPrice.Text.Trim());
            else
                item.Price = 0;

            if (CategoryName.Text != string.Empty)
            {
                item.CategoryID = Convert.ToInt64(CategoryID.Text);
                item.CategoryName = CategoryName.Text;
            }
            else
                item.CategoryName = "";

            string errorMsg = "";
            bool isSuccess = false;

            //对图片是否上传更新的选择处理
            if (FileUpload1.HasFile)
            {
                string photoUrl = UploadPhoto(ref isSuccess, ref errorMsg);
                if (cmd == "edit")
                {
                    if (photoUrl != "")
                    {
                        FileUpLoadCommon.DeleteFile(string.Format("{0}", item.PhotoUrl));
                        item.PhotoUrl = SystemConfig.Instance.UploadPath + "ExpendablePic/" + photoUrl;
                    }
                }
            }
            else
            {
                isSuccess = true;
                item.PhotoUrl = (string)ViewState["PhotoUrl"];
            }
            if (!isSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "上传图片失败", Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }

            if (cmd == "edit")
            {
                try
                {
                    item.ExpendableID = id;
                    Expendable Expendable = new Expendable();
                    Expendable.UpdateExpendable(item);
                    bSuccess = true;
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "编辑数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                }
            }
            if (bSuccess == true)
                EventMessage.MessageBox(Msg_Type.Info, "修改易耗品成功", "修改易耗品成功", Icon_Type.OK, "javascript:window.parent.hidePopWin(true);", UrlType.JavaScript);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    private string UploadPhoto(ref bool isSuccess, ref string errorMsg)
    {
        FileUpLoadCommon fc = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + "ExpendablePic/", false);
        isSuccess = fc.SaveFile(FileUpload1.PostedFile, true, false);
        if (!isSuccess)
            errorMsg = fc.ErrorMsg;
        return fc.NewFileName;
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
        Category bll = new Category();
        CategoryInfo categoryinfo = bll.GetCategory(Convert.ToInt32(TreeView1.SelectedNode.Value));
        PopupControlExtender2.Commit(CategoryName.Text);
        PopupControlExtender3.Commit(CategoryID.Text);
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "causeValidatescript", "causeValidate=true;", true);
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
            //QueryParam qp = bll.GenerateSearchTerm(categoryinfo);
            //qp.PageSize = 500;
            //int recordcount = 0;
            IList nodelist = (List<CategoryInfo>)bll.Search(categoryinfo);
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
    /// 初始化种类弹出树
    /// </summary>
    /// <param name="ParentID"></param>
    /// <param name="pNode"></param>
    public void AddTree(long ParentID, TreeNode pNode)
    {
        CategorysearchInfo categoryinfo = new CategorysearchInfo();
        categoryinfo.Level = 1;
        Category bll = new Category();
        //QueryParam qp = bll.GenerateSearchTerm(categoryinfo);
        //qp.PageSize = 500;
        //int recordcount = 0;
        IList nodelist = (List<CategoryInfo>)bll.Search(categoryinfo);
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
}
