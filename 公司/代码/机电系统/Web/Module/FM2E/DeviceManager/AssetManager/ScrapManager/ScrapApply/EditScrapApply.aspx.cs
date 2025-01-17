﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using System.Collections;
using WebUtility.Components;
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using FM2E.Model.Maintain;

using FM2E.Model.Utils;
using FM2E.BLL.Utils;
using FM2E.Model.System;
using FM2E.WorkflowLayer;


public partial class Module_FM2E_DeviceManager_AssetManager_ScrapManager_ScrapApply_EditScrapApply: System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 10, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 20, 0, DataType.Long);
    private readonly Scrap scrapBll = new Scrap();

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        CascadingDropDown1.SelectedValue = UserData.CurrentUserData.CompanyID;
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            ButtonBind();
        }
        
    }

    private void InitialPage()
    {
        //绑定部门到下拉列表
        Department depBll=new Department();
        DepartmentInfo term = new DepartmentInfo();
        LoginUserInfo loginInfo=UserData.CurrentUserData;
        term.CompanyID = loginInfo.CompanyID;
        ViewState["CompanyID"] = term.CompanyID;
        //Company companyBll = new Company();
        //CompanyInfo companyInfo = companyBll.GetCompany(term.CompanyID);
        lbCompany.Text = UserData.CurrentUserData.CompanyName;
        //报废原因
        ListItem[] malReason = EnumHelper.GetListItems(typeof(ScrapReason), (int)ScrapReason.Unknown);
        tbReason.Items.Clear();
        tbReason.Items.Add(new ListItem("未知", ((int)ScrapReason.Unknown).ToString()));
        tbReason.Items.AddRange(malReason);

        IList<DepartmentInfo> DepList = depBll.Search(term);

        ddlDep.Items.Clear();
        ddlDep.Items.Add(new ListItem("请选择部门", "0"));
        foreach (DepartmentInfo item in DepList)
        {
            ddlDep.Items.Add(new ListItem(item.Name, Convert.ToString(item.DepartmentID)));
        }
        ddlDep.SelectedValue = loginInfo.DepartmentID.ToString();
    }

    private void FillData()
    {
        try
        {
            if (cmd == "add")
            {
                //表单编号
                lbSheetNO.Text = SheetNOGenerator.GetSheetNO(UserData.CurrentUserData.CompanyID, SheetType.SCRAP_SCRAPAPPLY);
                ddlDep.SelectedValue = Convert.ToString(UserData.CurrentUserData.DepartmentID);
                //申请人
                lbApplicant.Text = UserData.CurrentUserData.PersonName;
                lbStatus.Text = "草稿";

            }
            else if (cmd == "edit")
            {
                ScrapApplyInfo model = scrapBll.GetScrapApply(id);
                lbSheetNO.Text = model.SheetName;
                ddlDep.SelectedValue = Convert.ToString(model.DepID);
                lbApplicant.Text = model.Applicant;
                lbStatus.Text = model.StatusString;
                tbRemark.Text = model.Remark;

                if (model.Equipments != null && model.Equipments.Count != 0)
                {
                    ScrapApplyDetailInfo equipment = (ScrapApplyDetailInfo)model.Equipments[0];
                    tbEquipmentNO.Text = equipment.EquipmentNo;
                    tbEquipmentName.Text = equipment.EquipmentName;
                    tbReason.Text = equipment.ScrapReason;
                }

                if (model.Status != ScrapStatus.Draft)
                {
                    Button2.Visible = false;
                    Button3.Visible = false;
                }
                #region 附件绑定
                string separatorStr = "@First@";
                string[] split = { separatorStr };
                if (model.Attachment != null)
                {
                    if (!model.Attachment.Contains(separatorStr))
                    {
                        model.Attachment += " " + separatorStr + " ";  //附件名称+附件地址
                    }
                }
                else
                {
                    model.Attachment += " " + separatorStr + " ";  //附件名称+附件地址
                }
                string[] editreason1 = model.Attachment.Split(split, StringSplitOptions.None);
                if (model.Attachment.Length > 0)
                {
                    HyperLink_File.NavigateUrl = editreason1[1];
                    HyperLink_File.Text = editreason1[0];
                    HyperLink_File.Visible = true;
                }
                #endregion
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "加载页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    private void ButtonBind()
    {
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：添加设备报废申请";
            TabContainer1.Tabs[0].HeaderText = "添加报废申请";
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：修改设备借调申请";
            TabContainer1.Tabs[0].HeaderText = "修改报废申请";
        }
    }
    /// <summary>
    /// 保存草稿
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        ValidateInput();

        ScrapApplyInfo item = new ScrapApplyInfo();
        try
        {
            item.SheetName = lbSheetNO.Text.Trim();
            item.CompanyID = (string)ViewState["CompanyID"];
            item.DepID = Convert.ToInt64(ddlDep.SelectedValue);
            item.Applicant = UserData.CurrentUserData.UserName;
            item.Status = ScrapStatus.Draft;  //草稿
            item.ApplyDate = DateTime.Now;
            item.Remark = tbRemark.Text;
            item.Attachment = HyperLink_File.Text + "@First@" + HyperLink_File.NavigateUrl;
            FileUpLoadCommon fileUtility_ArchivesAttachment = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + "ScrapManager/", false);
            if (FileUpload_ArchivesAttachmentFile.HasFile)
            {
                if (fileUtility_ArchivesAttachment.SaveFile(FileUpload_ArchivesAttachmentFile, false))
                {                    
                    item.Attachment = FileUpload_ArchivesAttachmentFile.FileName + "@First@" + SystemConfig.Instance.UploadPath + "ScrapManager/" + fileUtility_ArchivesAttachment.NewFileName;
                }
                else
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "附件上传失败：" + fileUtility_ArchivesAttachment.ErrorMsg, new FM2E.Model.Exceptions.WebException(fileUtility_ArchivesAttachment.ErrorMsg), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                }
            }
            ArrayList list = new ArrayList();
            ScrapApplyDetailInfo detail = new ScrapApplyDetailInfo();
            detail.EquipmentNo = tbEquipmentNO.Text.Trim();
            detail.EquipmentName = tbEquipmentName.Text.Trim();
            detail.ScrapReason = Convert.ToString(tbReason.SelectedItem);
            list.Add(detail);
            item.Equipments = list;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存草稿失败：获取参数失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        if (cmd == "add")
        {
            try
            {
                scrapBll.AddScrapApply(item);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存草稿失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "保存草稿成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ScrapApply.aspx"), UrlType.Href, "");
        }
        else if (cmd == "edit")
        {
            try
            {
                item.ScrapID = id;
                scrapBll.UpdateScrapApply(item);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存草稿失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "保存草稿成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ScrapApply.aspx"), UrlType.Href, "");
        }
    }
    /// <summary>
    /// 提交申请
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button3_Click(object sender, EventArgs e)
    {
        ValidateInput();

        ScrapApplyInfo item = new ScrapApplyInfo();
        try
        {
            item.SheetName = lbSheetNO.Text.Trim();
            item.CompanyID = (string)ViewState["CompanyID"];
            item.DepID = Convert.ToInt64(ddlDep.SelectedValue);
            item.Applicant = UserData.CurrentUserData.UserName;
            item.Status = ScrapStatus.Wait4ApprovalResult;  //草稿
            item.ApplyDate = DateTime.Now;
            item.Remark = tbRemark.Text.Trim();

            item.Attachment = HyperLink_File.Text + "@First@" + HyperLink_File.NavigateUrl;
            ArrayList list = new ArrayList();
            ScrapApplyDetailInfo detail = new ScrapApplyDetailInfo();
            detail.EquipmentNo = tbEquipmentNO.Text.Trim();
            detail.EquipmentName = tbEquipmentName.Text.Trim();
            detail.ScrapReason = Convert.ToString(tbReason.SelectedItem);
            list.Add(detail);
            item.Equipments = list;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交申请失败：获取参数失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        if (cmd == "add")
        {
            try
            {
                long thisID = scrapBll.AddScrapApply(item);
                string title = "报废申请" + item.SheetName + "待审批";
                string URL = "../DeviceManager/AssetManager/ScrapManager/ScrapApproval/Approval.aspx?cmd=approval&id=" + thisID;
                //WorkflowApplication.CreateWorkflowAndSendingPendingOrder<ScrapEventService>(thisID, title, ScrapWorkflow.WorkflowName, ScrapWorkflow.AppSubmitedEvent, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, item.CompanyID);
                WorkflowApplication.CreateWorkflowAndSendingPendingOrder1<ScrapEventService>(thisID, title, ScrapWorkflow.WorkflowName, ScrapWorkflow.AppSubmitedEvent, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, item.CompanyID);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "提交申请成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ScrapApply.aspx"), UrlType.Href, "");
        }
        else if (cmd == "edit")
        {
            try
            {
                item.ScrapID = id;
                scrapBll.UpdateScrapApply(item);
                string title = "报废申请" + item.SheetName + "待审批";
                string URL = "../DeviceManager/AssetManager/ScrapManager/ScrapApproval/Approval.aspx?cmd=approval&id=" + id;
                WorkflowApplication.CreateWorkflowAndSendingPendingOrder1<ScrapEventService>(item.ScrapID, title, ScrapWorkflow.WorkflowName, ScrapWorkflow.AppSubmitedEvent, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, item.CompanyID);               
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "提交申请成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ScrapApply.aspx"), UrlType.Href, "");
        }
    }
    /// <summary>
    /// 检查申请单输入是否合法
    /// </summary>
    private void ValidateInput()
    {
        string errorMsg = "";
        if (lbSheetNO.Text.Trim() == string.Empty)
        {
            errorMsg = "表单编号不能为空";
        }
        else if (CascadingDropDown2.SelectedValue == "")
        {
            errorMsg = "请选择所属部门";
        }
        else if (lbApplicant.Text.Trim() == string.Empty)
        {
            errorMsg = "申请人不能为空";
        }
        else if (lbStatus.Text.Trim() == string.Empty)
        {
            errorMsg = "表单状态不能为空";
        }
        else if (tbEquipmentNO.Text.Trim() == "")
        {
            errorMsg = "报废设备的条形码不能为空";
        }

        if (errorMsg != "")
        {
            // EventMessage.EventWriteLog(Msg_Type.Error, errorMsg);
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入有误：" + errorMsg, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 输入设备条形码后触发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tbEquipmentNO_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string equipmentNO = tbEquipmentNO.Text.Trim();
            if (equipmentNO == "")
                return;

            Equipment bll = new Equipment();
            EquipmentInfoFacade item = bll.GetEquipmentBYNO(equipmentNO);
            if (item != null)
            {
                tbEquipmentName.Text = item.Name;
                Button2.Enabled = true;
                Button3.Enabled = true;
            }
            else
            {
                tbEquipmentName.Text = "找不到相应的设备";
                Button2.Enabled = false;
                Button3.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取设备数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
}
