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
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.BLL.Maintain;
using FM2E.Model.Maintain;
using FM2E.BLL.System;
using FM2E.WorkflowLayer;
using FM2E.Model.Archives;
using FM2E.BLL.Archives;
using System.Xml;

public partial class Module_FM2E_ArchivesManager_SearchPages_RoutineMaintainPlan : System.Web.UI.Page
{
    string archivesType = (string)Common.sink("archivesType", MethodType.Get, 50, 0, DataType.Str);
    string archivesTypeName = (string)Common.sink("archivesTypeName", MethodType.Get, 50, 0, DataType.Str);
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private string XMLPATH = HttpContext.Current.Server.MapPath("~") + "/Module/FM2E/ArchivesManager/ArchivesConfig.xml";
    private const string ARCHIVESBORROWAPPLYDETAILLIST = "ArchivesBorrowApplyDetailList";
    private const string ARCHIVESDESTROYAPPLYDETAILLIST = "ArchivesDestroyApplyDetailList";
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

    private void BondButton()
    {
        if (cmd == "BorrowAdd")
        {
            HeadMenuWebControls1.ButtonList[0].ButtonName = "返回借阅申请单";
            HeadMenuWebControls1.ButtonList[0].ButtonUrl = "../ArchivesBorrowApply/ArchivesBorrowApply/EditArchivesBorrowApply.aspx?cmd=add";
            BtnBorrow.Text = "添加借阅";
            BtnDestroy.Text = "添加销毁";
            BtnBorrow.Visible = true;
            BtnDestroy.Visible = false;
        }
        else if (cmd == "DestroyAdd")
        {
            HeadMenuWebControls1.ButtonList[0].ButtonName = "返回销毁申请单";
            HeadMenuWebControls1.ButtonList[0].ButtonUrl = "../ArchivesDestroyApply/ArchivesDestroyApply/EditArchivesDestroyApply.aspx?cmd=add";
            BtnBorrow.Text = "添加借阅";
            BtnDestroy.Text = "添加销毁";
            BtnBorrow.Visible = false;
            BtnDestroy.Visible = true;
        }
        else
        {
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
            BtnBorrow.Text = "申请借阅";
            BtnDestroy.Text = "申请销毁";
            BtnBorrow.Visible = true;
            BtnDestroy.Visible = true;
        }
        lbErrorMessage.Text = string.Empty;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
        }
        BondButton();
    }
    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        try
        {
            ViewState["isSearch"] = false;
            Company companybll = new Company();
            IList<CompanyInfo> companylist = companybll.GetAllCompany();
            DDLCompany.Items.Clear();
            DDLCompany.Items.Add(new ListItem("请选择公司", ""));
            foreach (CompanyInfo item in companylist)
            {
                DDLCompany.Items.Add(new ListItem(item.CompanyName, item.CompanyID.ToString()));
            }
            DDLStatus.Items.Clear();
            DDLStatus.Items.Add(new ListItem("请选择申请状态", "0"));
            //DDLStatus.Items.Add(new ListItem("草稿", "1"));
            DDLStatus.Items.Add(new ListItem("等待审批", "2"));
            DDLStatus.Items.Add(new ListItem("审批通过", "3"));
            DDLStatus.Items.Add(new ListItem("审批不通过", "4"));
            DDLStatus.Items.Add(new ListItem("执行完毕", "5"));
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 初始化GridView等数据
    /// </summary>
    private void FillData()
    {
        try
        {
            bool search = Convert.ToBoolean(ViewState["isSearch"]);
            RoutineMaintainPlan bll = new RoutineMaintainPlan();
            int listCount = 0;
            ArrayList list = new ArrayList();

            QueryParam searchTerm = new QueryParam();
            if (!search)
            {
                RoutineMaintainPlanInfo item = new RoutineMaintainPlanInfo();
                item.PlanID = 0;
                item.CompanyID = "";
                item.PlanName = "";
                item.Planner = "";
                item.PlannerName = "";
                item.Status = 0;
                Array array = Enum.GetValues(typeof(RoutineMaintainPlanStatus));
                ArrayList al = new ArrayList(array);
                al.Remove(RoutineMaintainPlanStatus.UnKnownStatus);
                al.Remove(RoutineMaintainPlanStatus.Draft);
                item.StatusArray = al;
                searchTerm = bll.GenerateSearchTerm(item);
            }
            else
            {
                RoutineMaintainPlanInfo item = new RoutineMaintainPlanInfo();
                item.PlanID = 0;
                item.CompanyID = DDLCompany.SelectedValue;
                item.PlanName = Common.inSQL(TextBox1.Text.Trim());
                item.Planner = "";
                item.PlannerName = Common.inSQL(TextBox2.Text.Trim());
                item.Status = (RoutineMaintainPlanStatus)Convert.ToInt32(DDLStatus.SelectedValue);
                Array array = Enum.GetValues(typeof(RoutineMaintainPlanStatus));
                ArrayList al = new ArrayList(array);
                al.Remove(RoutineMaintainPlanStatus.UnKnownStatus);
                al.Remove(RoutineMaintainPlanStatus.Draft);
                item.StatusArray = al;
                searchTerm = bll.GenerateSearchTerm(item);
            }
            searchTerm.PageIndex = AspNetPager1.CurrentPageIndex;
            int thiscount = 0;
            IList l = bll.GetList(searchTerm, out thiscount);
            list.AddRange(l);
            listCount += thiscount;
            AspNetPager1.RecordCount = listCount;
            GridView1.DataSource = list;
            GridView1.DataBind();

            lbErrorMessage.Text = string.Empty;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
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
        SetSelectedAll();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long id = Convert.ToInt64(gvRow.Attributes["ID"]);
        if (e.CommandName == "view")
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(XMLPATH);
            XmlNode node = xmldoc.GetElementsByTagName(archivesType).Item(0);
            Response.Redirect("../../" + node.Attributes["ViewUrl"].Value + "?cmd=viewArchives&id=" + id, false);
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

            RoutineMaintainPlanInfo item = (RoutineMaintainPlanInfo)e.Row.DataItem;
            e.Row.Attributes["ID"] = item.PlanID.ToString();
            e.Row.Attributes["Name"] = item.PlanName;
            if (e.Row.RowIndex > -1 && this.SelectedItems != null)
            {
                CheckBox cb = e.Row.FindControl("checkBox1") as CheckBox;
                if (this.SelectedItems.Contains(e.Row.Attributes["ID"] + "|" + e.Row.Attributes["Name"]))
                    cb.Checked = true;
                else
                    cb.Checked = false;
            }
        }
    }
    /// <summary>
    /// 判断当页是否全选
    /// </summary>
    protected void SetSelectedAll()
    {
        HtmlInputCheckBox cbAll = this.GridView1.HeaderRow.FindControl("CheckAll") as HtmlInputCheckBox;
        ArrayList list = new ArrayList();
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            string id = this.GridView1.Rows[i].Attributes["ID"].ToString() + "|" + this.GridView1.Rows[i].Attributes["Name"].ToString();
            CheckBox cb = this.GridView1.Rows[i].FindControl("checkBox1") as CheckBox;
            if (!list.Contains(id) && cb.Checked)
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
            string id = this.GridView1.Rows[i].Attributes["ID"].ToString() + "|" + this.GridView1.Rows[i].Attributes["Name"].ToString();
            CheckBox cb = this.GridView1.Rows[i].FindControl("checkBox1") as CheckBox;
            if (selectedItems.Contains(id) && !cb.Checked)
                selectedItems.Remove(id);
            if (!selectedItems.Contains(id) && cb.Checked)
                selectedItems.Add(id);
        }
        this.SelectedItems = selectedItems;
    }
    /// <summary>
    /// 查询事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            TabContainer1.ActiveTabIndex = 0;
            ViewState["isSearch"] = true;
            FillData();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询计划失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 借阅按钮的响应
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnBorrow_Click(object sender, EventArgs e)
    {
        try
        {
            CollectSelected();

            lbErrorMessage.Text = string.Empty;
            ArrayList list = new ArrayList();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(HttpContext.Current.Server.MapPath("~") + "/Module/FM2E/ArchivesManager/ArchivesConfig.xml");
            XmlNode node = xmldoc.GetElementsByTagName(archivesType).Item(0);
            string thisModule = node.ParentNode.Attributes["ModuleName"].Value;
            string thisArchivesType = node.Attributes["ModuleName"].Value;
            if (SelectedItems.Count==0)
            {
                lbErrorMessage.Text = "您没有选择任何项，请选择！";
                return;
            }
            foreach (string item in SelectedItems)
            {
                ArchivesBorrowApplyDetailInfo info = new ArchivesBorrowApplyDetailInfo();
                info.Module = thisModule;
                info.ArchivesType = thisArchivesType;
                string[] s = item.Split('|');
                info.ArchivesID = Convert.ToInt64(s[0]);
                info.ArchivesName = s[1];
                list.Add(info);
            }
            if (Session[ARCHIVESBORROWAPPLYDETAILLIST] == null)
            {
                Session[ARCHIVESBORROWAPPLYDETAILLIST] = list;
            }
            else
            {
                ArrayList _list = (ArrayList)Session[ARCHIVESBORROWAPPLYDETAILLIST];
                foreach (ArchivesBorrowApplyDetailInfo item in list)
                {
                    if (!isContainsBorrow(_list, item))
                        _list.Add(item);
                }
                Session[ARCHIVESBORROWAPPLYDETAILLIST] = _list;
            }
            Response.Redirect("../ArchivesBorrowApply/ArchivesBorrowApply/EditArchivesBorrowApply.aspx?&cmd=add", false);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询计划失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    private bool isContainsBorrow(ArrayList list, ArchivesBorrowApplyDetailInfo info)
    {
        foreach (ArchivesBorrowApplyDetailInfo item in list)
        {
            if (item.ArchivesID == info.ArchivesID && item.ArchivesType == info.ArchivesType)
            {
                return true;
            }
        }
        return false;
    }
    private bool isContainsDestroy(ArrayList list, ArchivesDestroyApplyDetailInfo info)
    {
        foreach (ArchivesDestroyApplyDetailInfo item in list)
        {
            if (item.ArchivesID == info.ArchivesID && item.ArchivesType == info.ArchivesType)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 销毁按钮的响应
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnDestroy_Click(object sender, EventArgs e)
    {
        try
        {
            CollectSelected();

            lbErrorMessage.Text = string.Empty;
            ArrayList list = new ArrayList();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(HttpContext.Current.Server.MapPath("~") + "/Module/FM2E/ArchivesManager/ArchivesConfig.xml");
            XmlNode node = xmldoc.GetElementsByTagName(archivesType).Item(0);
            string thisModule = node.ParentNode.Attributes["ModuleName"].Value;
            string thisArchivesType = node.Attributes["ModuleName"].Value;
            if (SelectedItems.Count == 0)
            {
                lbErrorMessage.Text = "您没有选择任何项，请选择！";
                return;
            }
            foreach (string item in SelectedItems)
            {
                ArchivesDestroyApplyDetailInfo info = new ArchivesDestroyApplyDetailInfo();
                info.Module = thisModule;
                info.ArchivesType = thisArchivesType;
                string[] s = item.Split('|');
                info.ArchivesID = Convert.ToInt64(s[0]);
                info.ArchivesName = s[1];
                info.IsDestroyed = false;
                list.Add(info);
            }
            if (Session[ARCHIVESDESTROYAPPLYDETAILLIST] == null)
            {
                Session[ARCHIVESDESTROYAPPLYDETAILLIST] = list;
            }
            else
            {
                ArrayList _list = (ArrayList)Session[ARCHIVESDESTROYAPPLYDETAILLIST];
                foreach (ArchivesDestroyApplyDetailInfo item in list)
                {
                    if (!isContainsDestroy(_list, item))
                        _list.Add(item);
                }
                Session[ARCHIVESDESTROYAPPLYDETAILLIST] = _list;
            }
            Response.Redirect("../ArchivesDestroyApply/ArchivesDestroyApply/EditArchivesDestroyApply.aspx?&cmd=add", false);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询计划失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    public bool IsBorrowed(long id)
    {
        try
        {
            ArchivesBorrowApply bll = new ArchivesBorrowApply();
            return bll.isBorrowedDetail(archivesTypeName, id, Common.Get_UserName);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        return false;
    }

}
