using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebUtility;
using WebUtility.Components;
using FM2E.BLL.Basic;
using System.Collections;
using FM2E.Model.Basic;
using FM2E.Model.Exceptions;
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using FM2E.Model.Utils;
using FM2E.Model.System;
using System.Web.UI.HtmlControls;
using System.Xml;
using FM2E.Model.Archives;
using FM2E.BLL.Archives;

public partial class Module_FM2E_ArchivesManager_SearchPages_ScrapApply : System.Web.UI.Page
{
    private readonly Scrap scrapBll = new Scrap();

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

    private void InitialPage()
    {
        try
        {
            LoginUserInfo loginInfo = UserData.CurrentUserData;

            //绑定部门到下拉列表
            Department departmentbll = new Department();
            DepartmentInfo term = new DepartmentInfo();
            term.CompanyID = loginInfo.CompanyID;
            IList<DepartmentInfo> departmentList = departmentbll.Search(term);

            DropDownList3.Items.Clear();
            DropDownList3.Items.Add(new ListItem("不限", "0"));
            foreach (DepartmentInfo item in departmentList)
            {
                DropDownList3.Items.Add(new ListItem(item.Name, Convert.ToString(item.DepartmentID)));
            }

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "页面初始化失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    private void FillData()
    {
        try
        {
            //获取查询条件
            QueryParam qp = (QueryParam)ViewState["SearchTerm"];
            if (qp == null)
            {
                ScrapApplySearchInfo searchTerm = new ScrapApplySearchInfo();
                searchTerm.CompanyID = UserData.CurrentUserData.CompanyID;
                qp = scrapBll.GenerateSearchTerm(searchTerm);
            }
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;

            //查询
            int recordCount = 0;
            IList list = scrapBll.GetScrapApplyList(qp, out recordCount);
            GridView1.DataSource = list;
            GridView1.DataBind();
            AspNetPager1.RecordCount = recordCount;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long scrapID = Convert.ToInt64(gvRow.Attributes["ID"]);


        if (e.CommandName == "view")
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(XMLPATH);
            XmlNode node = xmldoc.GetElementsByTagName(archivesType).Item(0);
            Response.Redirect("../../" + node.Attributes["ViewUrl"].Value + "?cmd=viewArchives&id=" + scrapID, false);
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

            ScrapApplyInfo item = (ScrapApplyInfo)e.Row.DataItem;
            e.Row.Attributes["ID"] = item.ScrapID.ToString();
            e.Row.Attributes["Name"] = item.SheetName;
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        ScrapApplySearchInfo item = new ScrapApplySearchInfo();

        try
        {
            if (TextBox1.Text.Trim() != string.Empty)
                item.SheetName = Common.inSQL(TextBox1.Text.Trim());

            if (TextBox2.Text.Trim() != string.Empty)
                item.SubmitTimeFrom = Convert.ToDateTime(TextBox2.Text.Trim());

            if (TextBox3.Text.Trim() != string.Empty)
                item.SubmitTimeTo = Convert.ToDateTime(TextBox3.Text.Trim());

            item.DepID = Convert.ToInt64(DropDownList3.SelectedValue);
            item.Status = (ScrapStatus)Convert.ToInt32(DropDownList2.SelectedValue);
            item.CompanyID = UserData.CurrentUserData.CompanyID;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "参数不合法", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        try
        {
            QueryParam qp = scrapBll.GenerateSearchTerm(item);
            ViewState["SearchTerm"] = qp;
            FillData();
            TabContainer1.ActiveTabIndex = 0;
            AspNetPager1.CurrentPageIndex = 1;
        }
        catch (DALException ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询出错", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询参数有错误", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
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
            if (SelectedItems.Count == 0)
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
