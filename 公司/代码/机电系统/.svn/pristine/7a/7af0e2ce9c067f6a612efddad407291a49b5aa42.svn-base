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

public partial class Module_FM2E_DeviceManager_AssetManager_ScrapManager_ScrapApply_ScrapApply : System.Web.UI.Page
{
    private readonly Scrap scrapBll = new Scrap();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            PermissionControl();
        }
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

    private void PermissionControl()
    {
        //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
        //---if (SystemPermission.CheckPermission(PopedomType.Delete))
        //---    GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
        //---else GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = SystemPermission.CheckButtonPermission(PopedomType.New);
        //********** Modification Finished 2011-09-09 **********************************************************************************************
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
                searchTerm.Applicant = Common.Get_UserName;
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
        long scrapID = Convert.ToInt64(gvRow.Attributes["scrapID"]);

        if (e.CommandName == "view")
        {
            //查看
            Response.Redirect("ViewScrapApply.aspx?cmd=view&id=" + scrapID);
        }
        else if (e.CommandName == "del")
        {
            bool bSuccess = false;
            try
            {
                scrapBll.DeleteScrapApply(scrapID);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除设备报废申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除设备报废申请成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ScrapApply.aspx"), UrlType.Href, "");
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

            ScrapApplyInfo item = (ScrapApplyInfo)e.Row.DataItem;
            e.Row.Attributes["ScrapID"] = item.ScrapID.ToString();
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
            item.Applicant = Common.Get_UserName;
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
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询出错" ,ex,Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询参数有错误", ex , Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
}

