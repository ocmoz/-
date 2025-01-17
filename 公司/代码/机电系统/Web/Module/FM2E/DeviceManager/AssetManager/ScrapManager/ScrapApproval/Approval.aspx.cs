﻿using System;
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
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using WebUtility.Components;
using FM2E.WorkflowLayer;
using System.Collections.Generic;
using FM2E.Model.Workflow;

public partial class Module_FM2E_DeviceManager_AssetManager_ScrapManager_ScrapApproval_Approval : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 10, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 20, 0, DataType.Long);
    private readonly Scrap scrapBll = new Scrap();
    private readonly Equipment eqBll = new Equipment();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
            BindButton();
        }
    }

    private void FillData()
    {
        try
        {
            DDLApproval.Items.Clear();
            List<WorkflowEventInfo> list = WorkflowHelper.GetEventInfoList(ScrapWorkflow.WorkflowName, ScrapWorkflow.WaitManagerApproveState);
            DDLApproval.DataTextField = "Description";
            DDLApproval.DataValueField = "Name";
            DDLApproval.DataSource = list;
            DDLApproval.DataBind();

            if (cmd == "view" || cmd == "approval")
            {
                ScrapApplyInfo item = scrapBll.GetScrapApply(id);
                if (item != null)
                {
                    lbSheetName.Text = item.SheetName;
                    lbApplicant.Text = item.ApplicantName;
                    lbCompany.Text = item.CompanyName;
                    lbDep.Text = item.DepName;
                    lbStatus.Text = item.StatusString;
                    lbApplyDate.Text = item.ApplyDate.ToShortDateString();
                    lbRemark.Text = item.Remark;

                    #region 附件绑定
                    string separatorStr = "@First@";
                    string[] split = { separatorStr };
                    if (item.Attachment != null)
                    {
                        if (!item.Attachment.Contains(separatorStr))
                        {
                            item.Attachment += " " + separatorStr + " ";  //附件名称+附件地址
                        }
                    }
                    else
                    {
                        item.Attachment += " " + separatorStr + " ";  //附件名称+附件地址
                    }
                    string[] editreason1 = item.Attachment.Split(split, StringSplitOptions.None);
                    if (item.Attachment.Length > 0)
                    {
                        HyperLink_File.NavigateUrl = editreason1[1];
                        HyperLink_File.Text = editreason1[0];
                        HyperLink_File.Visible = true;
                    }
                    #endregion
                      
                    if (item.Equipments != null&&item.Equipments.Count!=0)
                    {
                        ScrapApplyDetailInfo detail = (ScrapApplyDetailInfo)item.Equipments[0];
                        //lbEquipment.Text = detail.EquipmentNo + "（" + detail.EquipmentName + "）";
                        lbEquipment.Text = detail.EquipmentName;
                        EquipmentInfoFacade Eqitem = eqBll.GetEquipmentBYNO(detail.EquipmentNo);

                        lbReason.Text = detail.ScrapReason;
                        lbEquipmentNo.Text = string.Format("<a style=\"color: Blue\" href=\"{0}\">{1}</a>", string.Format("javascript:showPopWin('查看设备信息','{0}Module/FM2E/DeviceManager/DeviceInfo/CurrentEuipementInfo/AllEquipmentInfo/ViewDeviceInfo.aspx?cmd=view&id={1}&companyid=SG&index=',800, 430, null,true,true);", Page.ResolveUrl("~/"), Eqitem.EquipmentID), detail.EquipmentNo);

                    }

                    if (item.ApprovalList != null)
                    {
                        GridView2.DataSource = item.ApprovalList;
                        GridView2.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "加载页面数据失败" + ex.Message, ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    private void BindButton()
    {
        if (cmd == "view")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：查看设备报废申请";
            TabContainer1.Tabs[0].HeaderText = "报废申请详细信息";
            ApprovalPanel.Visible = false;
        }
        else if (cmd == "approval")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：报废申请审批";
            TabContainer1.Tabs[0].HeaderText = "报废申请审批";
            ApprovalPanel.Visible = true;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            ScrapApprovalInfo item = new ScrapApprovalInfo();
            item.ScrapID = id;
            item.ApprovalerID = Common.Get_UserName;
            item.Result = DDLApproval.SelectedValue == ScrapWorkflow.ManagerApprovedEvent ? true : false;
            item.FeeBack = tbFeeBack.Text.Trim();
            item.ApprovalDate = DateTime.Now;

            scrapBll.ApprovalScrapApply(item);
            scrapBll.ChangeStatus(id, item.Result ? (int)ScrapStatus.ApprovalPassed : (int)ScrapStatus.ApprovalNotPassed);

            //**********Modified by Xue 2011-6-27****************************************************************************************************
            //FM2E.BLL.PendingOrder.PendingOrder pobll = new FM2E.BLL.PendingOrder.PendingOrder();
            //string lastURL = Request.Url.AbsolutePath + "?" + Request.QueryString.ToString();
            //if (lastURL.Contains("/Web/Module/FM2E"))
            //{
            //    lastURL = lastURL.Replace("/Web/Module/FM2E", "..");
            //}
            //if (lastURL.Contains("/Module/FM2E"))
            //{
            //    lastURL = lastURL.Replace("/Module/FM2E", "..");
            //}
            //pobll.MarkReadByURL(lastURL);
            //**********Modification Finished 2011-6-27**********************************************************************************************

            //此处应用工作流
            WorkflowInstanceInfo info = WorkflowHelper.GetWorkflowInstanceInfo(ScrapWorkflow.TableName, item.ScrapID);
            WorkflowHelper.SetStateMachine<ScrapEventService>(info.InstanceID, DDLApproval.SelectedValue);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交审批结果失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", "提交审批结果成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ScrapApproval.aspx"), UrlType.Href, "");
    }

}
