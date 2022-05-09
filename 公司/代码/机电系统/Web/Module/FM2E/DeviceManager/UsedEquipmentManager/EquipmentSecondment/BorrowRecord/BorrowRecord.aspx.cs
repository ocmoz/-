using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using WebUtility;
using WebUtility.Components;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using FM2E.BLL.Equipment;
using System.Collections;

public partial class Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_BorrowRecord_BorrowRecord : System.Web.UI.Page
{
    private readonly Secondment secondmentBll = new Secondment();
    public bool bShowRecordSearch = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            //FillData();
            PermissionControl();
            FillData1();
            FillData2();
        }


        for (int i = 0; i < rblSearchObject.Items.Count; i++)
        {
            rblSearchObject.Items[i].Attributes.Add("onclick", "SetVisible(this)");
        }
        if (rblSearchObject.SelectedIndex == 0)
            bShowRecordSearch = false;
        else bShowRecordSearch = true;

    }
    private void PermissionControl()
    {
        if (!SystemPermission.CheckPermission(PopedomType.New))
        {
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
        }
        else GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
    }
    private void InitialPage()
    {
        try
        {
            //绑定公司到下拉列表
            Company companyBll = new Company();
            IList<CompanyInfo> companyList = companyBll.GetAllCompany();

            ddlBorrowCompany.Items.Clear();
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

    private void FillData1()
    {
        BorrowApplySearchInfo term1 = new BorrowApplySearchInfo();
        term1.CompanyID = UserData.CurrentUserData.CompanyID;
        term1.Status = BorrowApplyStatus.ApprovalPassed; //借用审批通过
        term1.SheetName = Common.inSQL(tbSheetNO.Text.Trim());
        term1.BorrowCompanyID = ddlBorrowCompany.SelectedValue;
        term1.ApplicantName = Common.inSQL(tbApplicant.Text.Trim());
        if (tbApplyDateFrom.Text.Trim() != string.Empty)
        {
            term1.SubmitTimeFrom = Convert.ToDateTime(tbApplyDateFrom.Text.Trim());
        }
        if (tbApplyDateTo.Text.Trim() != string.Empty)
        {
            term1.SubmitTimeTo = Convert.ToDateTime(tbApplyDateTo.Text.Trim());
        }
        int recordCount1 = 0;
        IList list1 = secondmentBll.SearchApplyList(term1,AspNetPager1.PageSize,AspNetPager1.CurrentPageIndex, out recordCount1);
        GridView1.DataSource = list1;
        GridView1.DataBind();
        AspNetPager1.RecordCount = recordCount1;
    }


    private void FillData2()
    {
        BorrowRecordSearchInfo term2 = new BorrowRecordSearchInfo();
        term2.Recorder = Common.Get_UserName;
        term2.SheetNO = Common.inSQL(tbSheetNO.Text.Trim());
        term2.BorrowCompanyID = ddlBorrowCompany.SelectedValue;
        term2.BorrowerName = Common.inSQL(tbBorrower.Text.Trim());
        term2.Recorder = Common.Get_UserName;
        if (tbBorrowTimeFrom.Text.Trim() != string.Empty)
        {
            term2.BorrowTimeFrom = Convert.ToDateTime(tbBorrowTimeFrom.Text.Trim());
        }
        if (tbBorrowTimeTo.Text.Trim() != string.Empty)
        {
            term2.BorrowTimeTo = Convert.ToDateTime(tbBorrowTimeTo.Text.Trim());
        }
        if (tbReturnDateFrom.Text.Trim() != string.Empty)
        {
            term2.ReturnDateFrom = Convert.ToDateTime(tbReturnDateFrom.Text.Trim());
        }
        if (tbReturnDateTo.Text.Trim() != string.Empty)
        {
            term2.ReturnDateTo = Convert.ToDateTime(tbReturnDateTo.Text.Trim());
        }
        int recordCount2 = 0;
        IList list2 = secondmentBll.SearchBorrowRecord(term2, AspNetPager2.PageSize, AspNetPager2.CurrentPageIndex,out recordCount2);
        GridView2.DataSource = list2;
        GridView2.DataBind();
        AspNetPager2.RecordCount = recordCount2;
    }

    private void FillData()
    {
        try
        {
            //获取查询条件
            QueryParam qp = (QueryParam)ViewState["SearchTerm"];
            if (qp == null)
            {
                BorrowApplySearchInfo term1 = new BorrowApplySearchInfo();
                term1.BorrowCompanyID = UserData.CurrentUserData.CompanyID;
                term1.Status = BorrowApplyStatus.ApprovalPassed; //借用审批通过
                qp = secondmentBll.GenerateSearchTerm(term1);
                //获取审批通过的申请列表
                qp.PageIndex = AspNetPager1.CurrentPageIndex;
                qp.PageSize = AspNetPager1.PageSize;
                int recordCount1 = 0;
                IList list1 = secondmentBll.GetBorrowApplyList(qp, out recordCount1);
                GridView1.DataSource = list1;
                GridView1.DataBind();
                AspNetPager1.RecordCount = recordCount1;

                BorrowRecordSearchInfo term2 = new BorrowRecordSearchInfo();
                term2.Recorder = Common.Get_UserName;
                qp = secondmentBll.GenerateSearchTerm(term2);
                //获取借出登记信息
                qp.PageIndex = AspNetPager2.CurrentPageIndex;
                qp.PageSize = AspNetPager2.PageSize;
                int recordCount2 = 0;
                IList list2 = secondmentBll.GetBorrowRecordList(qp, out recordCount2);
                GridView2.DataSource = list2;
                GridView2.DataBind();
                AspNetPager2.RecordCount = recordCount2;
            }
            else
            {
                if (rblSearchObject.SelectedIndex == 0)
                {
                    //查询审批通过的申请
                    qp.PageIndex = AspNetPager1.CurrentPageIndex;
                    qp.PageSize = AspNetPager1.PageSize;
                    int recordCount = 0;
                    IList list = secondmentBll.GetBorrowApplyList(qp, out recordCount);
                    GridView1.DataSource = list;
                    GridView1.DataBind();
                    AspNetPager1.RecordCount = recordCount;
                    TabContainer1.ActiveTabIndex = 0;
                }
                else
                {
                    //查询借出登记信息
                    qp.PageIndex = AspNetPager2.CurrentPageIndex;
                    qp.PageSize = AspNetPager2.PageSize;
                    int recordCount = 0;
                    IList list = secondmentBll.GetBorrowRecordList(qp, out recordCount);
                    GridView2.DataSource = list;
                    GridView2.DataBind();
                    AspNetPager2.RecordCount = recordCount;
                    TabContainer1.ActiveTabIndex = 1;
                }
            }

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
    //    long borrowApplyID = Convert.ToInt64(gvRow.Attributes["BorrowApplyID"]);

    //    if (e.CommandName == "view")
    //    {
    //        Response.Redirect("RecordOutEquipment.aspx?cmd=add&id=" + borrowApplyID);
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

            //BorrowApplyInfo item = (BorrowApplyInfo)e.Row.DataItem;
            //e.Row.Attributes["BorrowApplyID"] = item.BorrowApplyID.ToString();
        }
    }
    //protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    GridViewRow gvRow = GridView2.Rows[Convert.ToInt32(e.CommandArgument)];
    //    long borrowApplyID = Convert.ToInt64(gvRow.Attributes["BorrowApplyID"]);
    //    string equipmentNO = Convert.ToString(gvRow.Attributes["EquipmentNO"]);

    //    if (e.CommandName == "view")
    //    {
    //        Response.Redirect("ViewBorrowRecord.aspx?cmd=view&id=" + borrowApplyID + "&equipmentNO=" + equipmentNO);
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

            //BorrowRecordInfo item = (BorrowRecordInfo)e.Row.DataItem;
            //e.Row.Attributes["BorrowApplyID"] = item.BorrowApplyID.ToString();
            //e.Row.Attributes["EquipmentNO"] = item.EquipmentNO;
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
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        int index = rblSearchObject.SelectedIndex;
        if (index == 0)
        {
            //查询申请单
            AspNetPager1.CurrentPageIndex = 1;
            //SearchApply();
            FillData1();
            TabContainer1.ActiveTabIndex = 0;
        }
        else if (index == 1)
        {
            //查询借出记录
            AspNetPager2.CurrentPageIndex = 1;
            //SearchRecord();
            FillData2();
            TabContainer1.ActiveTabIndex = 1;
        }
        
        
    }
    ///// <summary>
    ///// 查询申请单
    ///// </summary>
    //private void SearchApply()
    //{
    //    BorrowApplySearchInfo term = new BorrowApplySearchInfo();
    //    try
    //    {
    //        term.SheetName = Common.inSQL(tbSheetNO.Text.Trim());
    //        term.BorrowCompanyID = ddlBorrowCompany.SelectedValue;
    //        term.ApplicantName = Common.inSQL(tbApplicant.Text.Trim());
    //        if (tbApplyDateFrom.Text.Trim() != string.Empty)
    //        {
    //            term.SubmitTimeFrom = Convert.ToDateTime(tbApplyDateFrom.Text.Trim());
    //        }
    //        if (tbApplyDateTo.Text.Trim() != string.Empty)
    //        {
    //            term.SubmitTimeTo = Convert.ToDateTime(tbApplyDateTo.Text.Trim());
    //        }
    //        term.CompanyID = UserData.CurrentUserData.CompanyID;
    //        term.Status = BorrowApplyStatus.ApprovalPassed;  //借用审批通过
    //    }
    //    catch (Exception ex)
    //    {
    //        EventMessage.MessageBox(Msg_Type.Error, "操作失败", "参数不合法：" + ex.Message, ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
    //    }
    //    try
    //    {
    //        QueryParam qp = secondmentBll.GenerateSearchTerm(term);
    //        ViewState["SearchTerm"] = qp;
    //        FillData1();
    //    }
    //    catch (Exception ex)
    //    {
    //        EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
    //    }
    //}
    ///// <summary>
    ///// 查询借出记录
    ///// </summary>
    //private void SearchRecord()
    //{
    //    BorrowRecordSearchInfo term = new BorrowRecordSearchInfo();
    //    try
    //    {
    //        term.SheetNO = Common.inSQL(tbSheetNO.Text.Trim());
    //        term.BorrowCompanyID = ddlBorrowCompany.SelectedValue;
    //        term.BorrowerName = Common.inSQL(tbBorrower.Text.Trim());
    //        term.Recorder = Common.Get_UserName;
    //        if (tbBorrowTimeFrom.Text.Trim() != string.Empty)
    //        {
    //            term.BorrowTimeFrom = Convert.ToDateTime(tbBorrowTimeFrom.Text.Trim());
    //        }
    //        if (tbBorrowTimeTo.Text.Trim() != string.Empty)
    //        {
    //            term.BorrowTimeTo = Convert.ToDateTime(tbBorrowTimeTo.Text.Trim());
    //        }
    //        if (tbReturnDateFrom.Text.Trim() != string.Empty)
    //        {
    //            term.ReturnDateFrom = Convert.ToDateTime(tbReturnDateFrom.Text.Trim());
    //        }
    //        if (tbReturnDateTo.Text.Trim() != string.Empty)
    //        {
    //            term.ReturnDateTo = Convert.ToDateTime(tbReturnDateTo.Text.Trim());
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        EventMessage.MessageBox(Msg_Type.Error, "操作失败", "参数不合法：" + ex.Message, ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
    //    }

    //    try
    //    {
    //        QueryParam qp = secondmentBll.GenerateSearchTerm(term);
    //        ViewState["SearchTerm"] = qp;
    //        FillData2();
    //    }
    //    catch (Exception ex)
    //    {
    //        EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
    //    }
    //}
}
