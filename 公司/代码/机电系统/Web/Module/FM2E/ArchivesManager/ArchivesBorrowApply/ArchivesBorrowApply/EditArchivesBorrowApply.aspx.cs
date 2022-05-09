using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

using WebUtility;
using WebUtility.Components;
using WebUtility.WebControls;
using FM2E.BLL.Basic;
using FM2E.Model.Archives;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.WorkflowLayer;
using FM2E.BLL.Archives;
using FM2E.BLL.Utils;



public partial class Module_FM2E_ArchivesManager_ArchivesBorrowApply_ArchivesBorrowApply_EditArchivesBorrowApply : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    private const string ARCHIVESBORROWAPPLYDETAILLIST = "ArchivesBorrowApplyDetailList";
    private const string ARCHIVESBORROWAPPLYINFO = "ArchivesBorrowApplyInfo";
    private string XMLPATH = HttpContext.Current.Server.MapPath("~") + "/Module/FM2E/ArchivesManager/ArchivesConfig.xml";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            ButtonBind();
        }
    }
    /// <summary>
    /// 初始化页面
    /// </summary>

    private void InitialPage()
    {
        try
        {
            lbErrorMessage.Text = "";
            TBBorrowTime.Text = "1";
            if (cmd == "edit")
            {
                ArchivesBorrowApply bll = new ArchivesBorrowApply();
                ArchivesBorrowApplyInfo item = bll.GetArchivesBorrowApply(id);
                Label1.Text = item.SheetNo;
                TBBorrowTime.Text = item.BorrowTimeString.Replace("天", "");
                TextArea1.Value = item.BorrowReason;
                TextArea2.Value = item.Remark;
                if (item.ApplyStatus != ArchivesBorrowApplyStatus.Draft)
                {
                    btnDraft.Visible = false;
                    btSubmit.Visible = false;
                }
                Session[ARCHIVESBORROWAPPLYDETAILLIST] = item.ApplyDetailList;
            }
            if (cmd == "add")
            {
                Label1.Text = "_________________";
            }
            if (Session[ARCHIVESBORROWAPPLYDETAILLIST] == null || (((ArrayList)Session[ARCHIVESBORROWAPPLYDETAILLIST]).Count == 0))
            {
                btnAdd.Text = "添加明细";
            }
            else
            {
                btnAdd.Text = "继续添加";
            }
            if (Session[ARCHIVESBORROWAPPLYINFO] != null)
            {
                ArchivesBorrowApplyInfo item = (ArchivesBorrowApplyInfo)Session[ARCHIVESBORROWAPPLYINFO];
                Label1.Text = item.SheetNo;
                TBBorrowTime.Text = item.BorrowTimeString.Replace("天", "");
                TextArea1.Value = item.BorrowReason;
                TextArea2.Value = item.Remark;
            }
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
            if (Session[ARCHIVESBORROWAPPLYDETAILLIST] != null)
            {
                ArrayList list = (ArrayList)Session[ARCHIVESBORROWAPPLYDETAILLIST];
                int min = (AspNetPager1.CurrentPageIndex - 1) * 10;
                int max = (AspNetPager1.CurrentPageIndex * 10) > list.Count ? list.Count : AspNetPager1.CurrentPageIndex * 10;
                max = max - 1;
                ArrayList thisList = list.GetRange(min, max - min + 1);
                AspNetPager1.RecordCount = list.Count;
                GridView1.DataSource = thisList;
                GridView1.DataBind();
                lbErrorMessage.Text = "";
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

    }

    private void ButtonBind()
    {
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：档案借阅申请添加";

            TabPanel1.HeaderText = "添加申请";
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：档案借阅申请修改";

            TabPanel1.HeaderText = "修改申请";
        }
    }
    /// <summary>
    /// 添加按钮的响应
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lbErrorMessage.Text = "";
        ArchivesBorrowApplyInfo item = new ArchivesBorrowApplyInfo();
        item.SheetNo = Label1.Text;
        item.ApplyDate = DateTime.Now;
        item.BorrowTime = item.ApplyDate.AddDays(Convert.ToDouble(TBBorrowTime.Text.Trim()));//借阅时间是申请时间加上借阅期限
        item.BorrowReason = TextArea1.Value.Trim();
        item.Remark = TextArea2.Value.Trim();
        Session[ARCHIVESBORROWAPPLYINFO] = item;
        Response.Redirect("../../ArchivesManager.aspx?cmd=BorrowAdd", false);
    }
    /// <summary>
    /// 按钮提交申请的响应
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSubmit_Click(object sender, EventArgs e)
    {
        ArchivesBorrowApplyInfo item = new ArchivesBorrowApplyInfo();
        try
        {
            if (TBBorrowTime.Text.Trim() == "")
            {
                lbErrorMessage.Text = "请输入借阅期限";
                return;
            }
            item.Applicant = Common.Get_UserName;
            item.ApplyDate = DateTime.Now;
            item.BorrowTime = item.ApplyDate.AddDays(Convert.ToDouble(TBBorrowTime.Text.Trim()));//借阅时间是申请时间加上借阅期限
            item.ApprovalDate = item.ApplyDate;//假如审批时间等于申请时间，即该申请未审批,在model层实现
            item.Approvaler = "";
            item.ApprovalOpinion = "";
            item.BorrowReason = TextArea1.Value.Trim();
            item.ApplicantDept = UserData.CurrentUserData.DepartmentID;
            item.Remark = TextArea2.Value.Trim();
            item.ApplyStatus = ArchivesBorrowApplyStatus.Waiting4ApprovalResult;
            item.ApplyDetailList = (ArrayList)Session[ARCHIVESBORROWAPPLYDETAILLIST];
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交申请失败：获取参数失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        bool bSuccess = false;
        if (cmd == "add")
        {
            try
            {
                item.SheetNo = SheetNOGenerator.GetSheetNO(UserData.CurrentUserData.CompanyID, SheetType.ARCHIVES_BORROWAPPLY);
                ArchivesBorrowApply bll = new ArchivesBorrowApply();
                long thisID = bll.InsertArchivesBorrowApply(item);
                string title = "档案借阅申请" + item.SheetNo + "待审批";
                string URL = "../ArchivesManager/ArchivesBorrowApply/ArchivesBorrowApproval/ArchivesBorrowApplyApproval.aspx?cmd=approval&id=" + thisID;
                WorkflowApplication.CreateWorkflowAndSendingPendingOrder<ArchivesBorrowEventService>(thisID, title, ArchivesBorrowWorkflow.WorkflowName, ArchivesBorrowWorkflow.AppSubmitedEvent, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, UserData.CurrentUserData.CompanyID);
                bSuccess = true;
                Session.Remove(ARCHIVESBORROWAPPLYDETAILLIST);
                Session.Remove(ARCHIVESBORROWAPPLYINFO);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "提交申请成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ArchivesBorrowApply.aspx"), UrlType.Href, "");
            }
        }
        else if (cmd == "edit")
        {
            try
            {
                item.SheetNo = Label1.Text;
                ArchivesBorrowApply bll = new ArchivesBorrowApply();
                ArchivesBorrowApplyInfo info = bll.GetArchivesBorrowApply(id);
                item.ID = info.ID;
                bll.UpdateArchivesBorrowApply(item);
                string title = "档案借阅申请" + item.SheetNo + "待审批";
                string URL = "../ArchivesManager/ArchivesBorrowApply/ArchivesBorrowApproval/ArchivesBorrowApplyApproval.aspx?cmd=approval&id=" + item.ID;
                WorkflowApplication.CreateWorkflowAndSendingPendingOrder<ArchivesBorrowEventService>(item.ID, title, ArchivesBorrowWorkflow.WorkflowName, ArchivesBorrowWorkflow.AppSubmitedEvent, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, UserData.CurrentUserData.CompanyID);
                bSuccess = true;
                Session.Remove(ARCHIVESBORROWAPPLYDETAILLIST);
                Session.Remove(ARCHIVESBORROWAPPLYINFO);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "提交申请成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ArchivesBorrowApply.aspx"), UrlType.Href, "");
            }
        }
    }
    /// <summary>
    /// “保存草稿”操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDraft_Click(object sender, EventArgs e)
    {
        ArchivesBorrowApplyInfo item = new ArchivesBorrowApplyInfo();
        try
        {
            if (TBBorrowTime.Text.Trim() == "")
            {
                lbErrorMessage.Text = "请输入借阅期限";
                return;
            }
            item.Applicant = Common.Get_UserName;
            item.ApplyDate = DateTime.Now;
            item.BorrowTime = item.ApplyDate.AddDays(Convert.ToDouble(TBBorrowTime.Text.Trim()));//借阅时间是申请时间加上借阅期限
            item.ApprovalDate = item.ApplyDate;//假如审批时间等于申请时间，即该申请未审批,在model层实现
            item.Approvaler = "";
            item.ApprovalOpinion = "";
            item.BorrowReason = TextArea1.Value.Trim();
            item.ApplicantDept = UserData.CurrentUserData.DepartmentID;
            item.Remark = TextArea2.Value.Trim();
            item.ApplyStatus = ArchivesBorrowApplyStatus.Draft;
            item.ApplyDetailList = (ArrayList)Session[ARCHIVESBORROWAPPLYDETAILLIST];
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存草稿失败：获取参数失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        bool bSuccess = false;
        if (cmd == "add")
        {
            try
            {
                item.SheetNo = SheetNOGenerator.GetSheetNO(UserData.CurrentUserData.CompanyID, SheetType.ARCHIVES_BORROWAPPLY);
                ArchivesBorrowApply bll = new ArchivesBorrowApply();
                bll.InsertArchivesBorrowApply(item);
                bSuccess = true;
                Session.Remove(ARCHIVESBORROWAPPLYDETAILLIST);
                Session.Remove(ARCHIVESBORROWAPPLYINFO);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存草稿失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "保存草稿成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ArchivesBorrowApply.aspx"), UrlType.Href, "");
            }
        }
        else if (cmd == "edit")
        {
            try
            {
                item.SheetNo = Label1.Text;
                ArchivesBorrowApply bll = new ArchivesBorrowApply();
                ArchivesBorrowApplyInfo info = bll.GetArchivesBorrowApply(id);
                item.ID = info.ID;
                bll.UpdateArchivesBorrowApply(item);
                bSuccess = true;
                Session.Remove(ARCHIVESBORROWAPPLYDETAILLIST);
                Session.Remove(ARCHIVESBORROWAPPLYINFO);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存草稿失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "保存草稿成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ArchivesBorrowApply.aspx"), UrlType.Href, "");
            }
        }
    }

    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowNum = Convert.ToInt32(e.CommandArgument);
        ArrayList list = (ArrayList)Session[ARCHIVESBORROWAPPLYDETAILLIST];

        if (e.CommandName == "del")
        {
            //删除
            if (list == null) return;

            list.RemoveAt((AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize + rowNum);
            Session[ARCHIVESBORROWAPPLYDETAILLIST] = list;
            FillData();
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
        }
    }
}
