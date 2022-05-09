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

public partial class Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_BorrowApply_BorrowApply : System.Web.UI.Page
{
    private readonly Secondment secondmentBll = new Secondment();

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
            //绑定公司到下拉列表
            Company companyBll = new Company();
            IList<CompanyInfo> companyList = companyBll.GetAllCompany();

            DropDownList1.Items.Clear();
            DropDownList1.Items.Add(new ListItem("不限", "0"));
            foreach (CompanyInfo item in companyList)
            {
                DropDownList1.Items.Add(new ListItem(item.CompanyName, item.CompanyID));
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "页面初始化失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    private void PermissionControl()
    {
        if (SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
        else GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
    }

    private void FillData()
    {
        try
        {
            //获取查询条件
            QueryParam qp = (QueryParam)ViewState["SearchTerm"];
            if (qp == null)
            {
                BorrowApplySearchInfo searchTerm = new BorrowApplySearchInfo();
                searchTerm.Applicant = Common.Get_UserName;
                qp = secondmentBll.GenerateSearchTerm(searchTerm);
            }
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;

            //查询
            int recordCount = 0;
            IList list = secondmentBll.GetBorrowApplyList(qp, out recordCount);
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
        long borrowApplyID = Convert.ToInt64(gvRow.Attributes["BorrowApplyID"]);

        if (e.CommandName == "view")
        {
            //查看
            Response.Redirect("ViewBorrowApply.aspx?cmd=view&id=" + borrowApplyID);
        }
        else if (e.CommandName == "del")
        {
            bool bSuccess = false;
            try
            {
                secondmentBll.DeleteBorrowApply(borrowApplyID);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除设备借调申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除设备借调申请成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("BorrowApply.aspx"), UrlType.Href, "");
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

            BorrowApplyInfo item = (BorrowApplyInfo)e.Row.DataItem;
            e.Row.Attributes["BorrowApplyID"] = item.BorrowApplyID.ToString();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        BorrowApplySearchInfo item = new BorrowApplySearchInfo();

        try
        {
            if (TextBox1.Text.Trim() != string.Empty)
                item.SheetName = Common.inSQL(TextBox1.Text.Trim());

            if (TextBox2.Text.Trim() != string.Empty)
                item.SubmitTimeFrom = Convert.ToDateTime(TextBox2.Text.Trim());

            if (TextBox3.Text.Trim() != string.Empty)
                item.SubmitTimeTo = Convert.ToDateTime(TextBox3.Text.Trim());

            item.CompanyID = DropDownList1.SelectedValue;
            item.Status = (BorrowApplyStatus)Convert.ToInt32(DropDownList2.SelectedValue);
            item.Applicant = Common.Get_UserName;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "参数不合法", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        try
        {
            QueryParam qp = secondmentBll.GenerateSearchTerm(item);
            ViewState["SearchTerm"] = qp;
            AspNetPager1.CurrentPageIndex = 1;
            FillData();
            TabContainer1.ActiveTabIndex = 0;
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
