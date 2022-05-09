using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using WebUtility.Components;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using FM2E.Model.Utils;
using System.Collections;
using FM2E.WorkflowLayer;

public partial class Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_BorrowApproval_BorrowApproval : System.Web.UI.Page
{
    private readonly Secondment secondmentBll = new Secondment();
    public bool bShow = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData1();
            FillData2();
            PermissionControl();
        }

        //for (int i = 0; i < RadioButtonList1.Items.Count; i++)
        //{
        //    RadioButtonList1.Items[i].Attributes.Add("onclick", "SetVisible(this)");
        //}
        //if (RadioButtonList1.SelectedIndex == 0)
        //    bShow = false;
        //else bShow = true;
    }

    private void InitialPage()
    {
        try
        {
            //加载公司列表
            ddlBorrowCompany.Items.Clear();
            Company companyBll = new Company();
            IList<CompanyInfo> companyList = companyBll.GetAllCompany();

            ddlBorrowCompany.Items.Add(new ListItem("不限", "0"));
            foreach (CompanyInfo item in companyList)
            {
                ddlBorrowCompany.Items.Add(new ListItem(item.CompanyName, item.CompanyID));
            }

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "页面初始化失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    private void PermissionControl()
    {
        if (!SystemPermission.CheckPermission(PopedomType.Approval))
        {
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
        }
        else
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
    }

    /// <summary>
    /// 待审批列表
    /// </summary>
    private void FillData1()
    {
        //List<string> states = WorkflowHelper.GetCorrelativeStateNameList(EquipmentBorrowWorkflow.WorkflowName, Common.Get_UserName);

        BorrowApplySearchInfo term1 = new BorrowApplySearchInfo();
        term1.CompanyID = UserData.CurrentUserData.CompanyID;
        term1.Status = BorrowApplyStatus.Waiting4ApprovalResult;


        int recordCount1 = 0;
        IList list1 = secondmentBll.SearchApplyList(term1, AspNetPager1.PageSize, AspNetPager2.CurrentPageIndex, out recordCount1);
        GridView1.DataSource = list1;
        GridView1.DataBind();
        AspNetPager1.RecordCount = recordCount1;
    }


    private void FillData2()
    {
        BorrowApprovalSearchInfo info = new BorrowApprovalSearchInfo();

        info.SheetNO = Common.inSQL(tbSheetNO.Text.Trim());
        info.CompanyID = UserData.CurrentUserData.CompanyID;
        info.BorrowCompanyID = ddlBorrowCompany.SelectedValue;
        info.ApplicantName = Common.inSQL(tbApplicant.Text.Trim());

        if (tbApplyDateFrom.Text.Trim() != string.Empty)
        {
            info.ApplyDateFrom = Convert.ToDateTime(tbApplyDateFrom.Text.Trim());
        }
        if (tbApplyDateTo.Text.Trim() != string.Empty)
        {
            info.ApplyDateTo = Convert.ToDateTime(tbApplyDateTo.Text.Trim());
        }
        if (tbApprovalDateFrom.Text.Trim() != string.Empty)
        {
            info.ApprovalDateFrom = Convert.ToDateTime(tbApprovalDateFrom.Text.Trim());
        }
        if (tbApprovalDateTo.Text.Trim() != string.Empty)
        {
            info.ApprovalDateTo = Convert.ToDateTime(tbApprovalDateTo.Text.Trim());
        }
        info.ApprovalResult = Convert.ToInt32(ddlResult.SelectedValue);
        info.Approvaler = Common.Get_UserName;

        int recordCount = 0;
        IList list = secondmentBll.SearchApprovalHistory(info, AspNetPager2.PageSize, AspNetPager2.CurrentPageIndex, out recordCount);
        GridView2.DataSource = list;
        GridView2.DataBind();
        AspNetPager2.RecordCount = recordCount;
    }

    //private void FillData()
    //{
    //    try
    //    {
    //        //获取查询条件
    //        QueryParam qp = (QueryParam)ViewState["SearchTerm"];
    //        if (qp == null)
    //        {

               

    //            BorrowApprovalSearchInfo term2 = new BorrowApprovalSearchInfo();
    //            term2.Approvaler = Common.Get_UserName;
    //            term2.ApprovalResult = 3;
    //            qp = secondmentBll.GenerateSearchTerm(term2);
    //            //需要获取审批历史记录
    //            qp.PageIndex = AspNetPager2.CurrentPageIndex;
    //            qp.PageSize = AspNetPager2.PageSize;

    //            int recordCount2 = 0;
    //            IList list2 = secondmentBll.GetBorrowApprovalHistory(qp, out recordCount2);
    //            GridView2.DataSource = list2;
    //            GridView2.DataBind();
    //            AspNetPager2.RecordCount = recordCount2;
    //            TabContainer1.ActiveTabIndex = 0;
    //        }
    //        else
    //        {
    //            if (RadioButtonList1.SelectedIndex == 0)
    //            {
    //                //需要获取未审批的申请列表
    //                qp.PageIndex = AspNetPager1.CurrentPageIndex;
    //                qp.PageSize = AspNetPager1.PageSize;

    //                int recordCount = 0;
    //                IList list = secondmentBll.GetBorrowApplyList(qp, out recordCount);
    //                GridView1.DataSource = list;
    //                GridView1.DataBind();
    //                AspNetPager1.RecordCount = recordCount;
    //                TabContainer1.ActiveTabIndex = 0;
    //            }
    //            else
    //            {
    //                //需要获取审批历史记录
    //                qp.PageIndex = AspNetPager2.CurrentPageIndex;
    //                qp.PageSize = AspNetPager2.PageSize;

    //                int recordCount = 0;
    //                IList list = secondmentBll.GetBorrowApprovalHistory(qp, out recordCount);
    //                GridView2.DataSource = list;
    //                GridView2.DataBind();
    //                AspNetPager2.RecordCount = recordCount;
    //                TabContainer1.ActiveTabIndex = 1;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
    //    }
    //}

    //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
    //    long borrowApplyID = Convert.ToInt64(gvRow.Attributes["BorrowApplyID"]);

    //    if (e.CommandName == "approval")
    //    {
    //        //查看
    //        Response.Redirect("Approval.aspx?cmd=approval&id=" + borrowApplyID);
    //    }
    //}
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

    //protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    GridViewRow gvRow = GridView2.Rows[Convert.ToInt32(e.CommandArgument)];
    //    long borrowApplyID = Convert.ToInt64(gvRow.Attributes["BorrowApplyID"]);

    //    if (e.CommandName == "view")
    //    {
    //        //查看
    //        Response.Redirect("Approval.aspx?cmd=view&id=" + borrowApplyID);
    //    }
    //}
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData1();
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        FillData2();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //BorrowApprovalSearchInfo item1 = new BorrowApprovalSearchInfo();
        //BorrowApplySearchInfo item2 = new BorrowApplySearchInfo();
        //int searchObject = RadioButtonList1.SelectedIndex;
        //try
        //{

        //    item2.SheetName = item1.SheetNO = Common.inSQL(tbSheetNO.Text.Trim());
        //    item2.CompanyID = item1.CompanyID = UserData.CurrentUserData.CompanyID;
        //    item2.BorrowCompanyID = item1.BorrowCompanyID = ddlBorrowCompany.SelectedValue;
        //    item2.ApplicantName = item1.ApplicantName = Common.inSQL(tbApplicant.Text.Trim());
        //    item2.Status = BorrowApplyStatus.Waiting4ApprovalResult;
        //    if (tbApplyDateFrom.Text.Trim() != string.Empty)
        //    {
        //        item2.SubmitTimeFrom = item1.ApplyDateFrom = Convert.ToDateTime(tbApplyDateFrom.Text.Trim());
        //    }
        //    if (tbApplyDateTo.Text.Trim() != string.Empty)
        //    {
        //        item2.SubmitTimeTo = item1.ApplyDateTo = Convert.ToDateTime(tbApplyDateTo.Text.Trim());
        //    }
        //    if (tbApprovalDateFrom.Text.Trim() != string.Empty)
        //    {
        //        item1.ApprovalDateFrom = Convert.ToDateTime(tbApprovalDateFrom.Text.Trim());
        //    }
        //    if (tbApprovalDateTo.Text.Trim() != string.Empty)
        //    {
        //        item1.ApprovalDateTo = Convert.ToDateTime(tbApprovalDateTo.Text.Trim());
        //    }
        //    item1.ApprovalResult = Convert.ToInt32(ddlResult.SelectedValue);
        //    item1.Approvaler = Common.Get_UserName;
        //}
        //catch (Exception ex)
        //{
        //    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "参数不合法：" + ex.Message, ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        //}
        try
        {
            //QueryParam qp = null;
            //List<string> states = WorkflowHelper.GetCorrelativeStateNameList(EquipmentBorrowWorkflow.WorkflowName, Common.Get_UserName);
            //if (searchObject == 0)
            //    qp = secondmentBll.GenerateSearchTerm(item2, states.ToArray());
            //else qp = secondmentBll.GenerateSearchTerm(item1);

            //ViewState["SearchTerm"] = qp;

            AspNetPager2.CurrentPageIndex = 1;
            FillData2();
            TabContainer1.ActiveTabIndex = 1;
            //AspNetPager1.CurrentPageIndex = 1;
            
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
}
