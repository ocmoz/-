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



public partial class Module_FM2E_ArchivesManager_ArchivesDestroyApply_ArchivesDestroyApply_EditArchivesDestroyApply : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    private const string ARCHIVESDESTROYAPPLYDETAILLIST = "ArchivesDestroyApplyDetailList";
    private const string ARCHIVESDESTROYAPPLYINFO = "ArchivesDestroyApplyInfo";
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
            if (cmd == "edit")
            {
                ArchivesDestroyApply bll = new ArchivesDestroyApply();
                ArchivesDestroyApplyInfo item = bll.GetArchivesDestroyApply(id);
                Label1.Text = item.SheetNo;
                TextArea1.Value = item.DestroyReason;
                TextArea2.Value = item.Remark;
                if (item.ApplyStatus != ArchivesDestroyApplyStatus.Draft)
                {
                    btnDraft.Visible = false;
                    btSubmit.Visible = false;
                }
                Session[ARCHIVESDESTROYAPPLYDETAILLIST] = item.ApplyDetailList;
            }
            if (cmd == "add")
            {
                Label1.Text = "_________________";
            }


            if (Session[ARCHIVESDESTROYAPPLYDETAILLIST] == null || (((ArrayList)Session[ARCHIVESDESTROYAPPLYDETAILLIST]).Count == 0))
            {
                btnAdd.Text = "添加明细";
            }
            else
            {
                btnAdd.Text = "继续添加";
            }
            if (Session[ARCHIVESDESTROYAPPLYINFO] != null)
            {
                ArchivesDestroyApplyInfo item = (ArchivesDestroyApplyInfo)Session[ARCHIVESDESTROYAPPLYINFO];
                Label1.Text = item.SheetNo;
                TextArea1.Value = item.DestroyReason;
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
            if (Session[ARCHIVESDESTROYAPPLYDETAILLIST] != null)
            {
                ArrayList list = (ArrayList)Session[ARCHIVESDESTROYAPPLYDETAILLIST];
                int min = (AspNetPager1.CurrentPageIndex - 1) * 10;
                int max = (AspNetPager1.CurrentPageIndex * 10) > list.Count ? list.Count : AspNetPager1.CurrentPageIndex * 10;
                max = max - 1;
                ArrayList thisList = list.GetRange(min, max - min + 1);
                AspNetPager1.RecordCount = list.Count;
                GridView1.DataSource = thisList;
                GridView1.DataBind();
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
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：档案销毁申请添加";

            TabPanel1.HeaderText = "添加申请";
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：档案销毁申请修改";

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
        ArchivesDestroyApplyInfo item = new ArchivesDestroyApplyInfo();
        item.SheetNo = Label1.Text;
        item.DestroyReason = TextArea1.Value.Trim();
        item.Remark = TextArea2.Value.Trim();
        Session[ARCHIVESDESTROYAPPLYINFO] = item;
        Response.Redirect("../../ArchivesManager.aspx?cmd=DestroyAdd", false);
    }
    /// <summary>
    /// 按钮提交申请的响应
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSubmit_Click(object sender, EventArgs e)
    {
        ArchivesDestroyApplyInfo item = new ArchivesDestroyApplyInfo();
        try
        {
            item.Applicant = Common.Get_UserName;
            item.ApplyDate = DateTime.Now;
            item.ApprovalDate = item.ApplyDate;//假如审批时间等于申请时间，即该申请未审批,在model层实现
            item.Approvaler = "";
            item.ApprovalOpinion = "";
            item.DestroyReason = TextArea1.Value.Trim();
            item.ApplicantDept = UserData.CurrentUserData.DepartmentID;
            item.Remark = TextArea2.Value.Trim();
            item.ApplyStatus = ArchivesDestroyApplyStatus.Waiting4ApprovalResult;
            item.ApplyDetailList = (ArrayList)Session[ARCHIVESDESTROYAPPLYDETAILLIST];
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
                item.SheetNo = SheetNOGenerator.GetSheetNO(UserData.CurrentUserData.CompanyID, SheetType.ARCHIVES_DESTROYAPPLY);
                ArchivesDestroyApply bll = new ArchivesDestroyApply();
                long thisID = bll.InsertArchivesDestroyApply(item);
                string title = "档案销毁申请" + item.SheetNo + "待审批";
                string URL = "../ArchivesManager/ArchivesDestroyApply/ArchivesDestroyApproval/ArchivesDestroyApplyApproval.aspx?cmd=approval&id=" + thisID;
                WorkflowApplication. CreateWorkflowAndSendingPendingOrder<ArchivesDestroyEventService>( thisID , title , ArchivesDestroyWorkflow. WorkflowName , ArchivesDestroyWorkflow. AppSubmitedEvent , Common. Get_UserName , UserData. CurrentUserData. PersonName , URL , 0 , UserData. CurrentUserData. CompanyID );
                bSuccess = true;
                Session.Remove(ARCHIVESDESTROYAPPLYDETAILLIST);
                Session.Remove(ARCHIVESDESTROYAPPLYINFO);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "提交申请成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ArchivesDestroyApply.aspx"), UrlType.Href, "");
            }
        }
        else if (cmd == "edit")
        {
            try
            {
                item.SheetNo = Label1.Text;
                ArchivesDestroyApply bll = new ArchivesDestroyApply();
                ArchivesDestroyApplyInfo info = bll.GetArchivesDestroyApply(id);
                item.ID = info.ID;
                bll.UpdateArchivesDestroyApply(item);
                string title = "档案销毁申请" + item.SheetNo + "待审批";
                string URL = "../ArchivesManager/ArchivesDestroyApply/ArchivesDestroyApproval/ArchivesDestroyApplyApproval.aspx?cmd=approval&id=" + item.ID;
                WorkflowApplication.CreateWorkflowAndSendingPendingOrder<ArchivesDestroyEventService>(item.ID, title, ArchivesDestroyWorkflow.WorkflowName, ArchivesDestroyWorkflow.AppSubmitedEvent, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, UserData.CurrentUserData.CompanyID);
                bSuccess = true;
                bSuccess = true;
                Session.Remove(ARCHIVESDESTROYAPPLYDETAILLIST);
                Session.Remove(ARCHIVESDESTROYAPPLYINFO);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "提交申请成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ArchivesDestroyApply.aspx"), UrlType.Href, "");
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
        ArchivesDestroyApplyInfo item = new ArchivesDestroyApplyInfo();
        try
        {

            item.Applicant = Common.Get_UserName;
            item.ApplyDate = DateTime.Now;
            item.ApprovalDate = item.ApplyDate;//假如审批时间等于申请时间，即该申请未审批,在model层实现
            item.Approvaler = "";
            item.ApprovalOpinion = "";
            item.DestroyReason = TextArea1.Value.Trim();
            item.ApplicantDept = UserData.CurrentUserData.DepartmentID;
            item.Remark = TextArea2.Value.Trim();
            item.ApplyStatus = ArchivesDestroyApplyStatus.Draft;
            item.ApplyDetailList = (ArrayList)Session[ARCHIVESDESTROYAPPLYDETAILLIST];
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
                item.SheetNo = SheetNOGenerator.GetSheetNO(UserData.CurrentUserData.CompanyID, SheetType.ARCHIVES_DESTROYAPPLY);
                ArchivesDestroyApply bll = new ArchivesDestroyApply();
                bll.InsertArchivesDestroyApply(item);
                bSuccess = true;
                Session.Remove(ARCHIVESDESTROYAPPLYDETAILLIST);
                Session.Remove(ARCHIVESDESTROYAPPLYINFO);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存草稿失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "保存草稿成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ArchivesDestroyApply.aspx"), UrlType.Href, "");
            }
        }
        else if (cmd == "edit")
        {
            try
            {
                item.SheetNo = Label1.Text;
                ArchivesDestroyApply bll = new ArchivesDestroyApply();
                ArchivesDestroyApplyInfo info = bll.GetArchivesDestroyApply(id);
                item.ID = info.ID;
                bll.UpdateArchivesDestroyApply(item);
                bSuccess = true;
                Session.Remove(ARCHIVESDESTROYAPPLYDETAILLIST);
                Session.Remove(ARCHIVESDESTROYAPPLYINFO);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存草稿失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "保存草稿成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ArchivesDestroyApply.aspx"), UrlType.Href, "");
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
        ArrayList list = (ArrayList)Session[ARCHIVESDESTROYAPPLYDETAILLIST];

        if (e.CommandName == "del")
        {
            //删除
            if (list == null) return;

            list.RemoveAt((AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize + rowNum);
            Session[ARCHIVESDESTROYAPPLYDETAILLIST] = list;
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
