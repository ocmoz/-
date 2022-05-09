using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using WebUtility.Components;
using FM2E.WorkflowLayer;
using FM2E.Model.Workflow;

public partial class Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_BorrowApproval_Approval : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 10, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 20, 0, DataType.Long);
    private readonly Secondment secondment = new Secondment();

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
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
            List<WorkflowEventInfo> list = WorkflowHelper.GetEventInfoList(EquipmentBorrowWorkflow.WorkflowName, EquipmentBorrowWorkflow.WaitManagerApproveState);
            DDLApproval.DataTextField = "Description";
            DDLApproval.DataValueField = "Name";
            DDLApproval.DataSource = list;
            DDLApproval.DataBind();

            if (cmd == "view" || cmd == "approval")
            {
                BorrowApplyInfo item = secondment.GetBorrowApply(id);
                if (item != null)
                {
                    lbSheetName.Text = item.SheetName;
                    lbLendCompany.Text = item.CompanyName;
                    lbBorrowCompany.Text = item.BorrowCompanyName;
                    lbApplicant.Text = item.ApplicantName;
                    lbStatus.Text = item.StatusString;
                    lbSubmitTime.Text = item.SubmitTime.ToShortDateString();

                    if (item.DetailList != null)
                    {
                        GridView1.DataSource = item.DetailList;
                        GridView1.DataBind();
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
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：查看设备借调申请";
            TabContainer1.Tabs[0].HeaderText = "借调申请详细信息";
            ApprovalPanel.Visible = false;
        }
        else if (cmd == "approval")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：借调申请审批";
            TabContainer1.Tabs[0].HeaderText = "借调申请审批";
            ApprovalPanel.Visible = true;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            BorrowApprovalInfo item = new BorrowApprovalInfo();
            item.BorrowApplyID = id;
            item.Approvaler = Common.Get_UserName;
            item.Result = DDLApproval.SelectedValue == EquipmentBorrowWorkflow.ManagerApprovedEvent ? true : false;
            item.FeeBack = tbFeeBack.Text.Trim();
            item.ApprovalDate = DateTime.Now;

            secondment.ApprovalBorrowApply(item);
            secondment.ChangeStatus(id, item.Result ? BorrowApplyStatus.ApprovalPassed : BorrowApplyStatus.ApprovalFailed);

            //**********Modified by Xue 2011-6-27****************************************************************************************************
            FM2E.BLL.PendingOrder.PendingOrder pobll = new FM2E.BLL.PendingOrder.PendingOrder();
            string lastURL = Request.Url.AbsolutePath + "?" + Request.QueryString.ToString();
            if (lastURL.Contains("/Web/Module/FM2E"))
            {
                lastURL = lastURL.Replace("/Web/Module/FM2E", "..");
            }
            if (lastURL.Contains("/Module/FM2E"))
            {
                lastURL = lastURL.Replace("/Module/FM2E", "..");
            }
            pobll.MarkReadByURL(lastURL);
            //**********Modification Finished 2011-6-27**********************************************************************************************

            //此处应用工作流
            WorkflowInstanceInfo info = WorkflowHelper.GetWorkflowInstanceInfo(EquipmentBorrowWorkflow.TableName, item.BorrowApplyID);
            WorkflowHelper.SetStateMachine<EquipmentBorrowEventService>(info.InstanceID, DDLApproval.SelectedValue);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交审批结果失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", "提交审批结果成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("BorrowApproval.aspx"), UrlType.Href, "");
    }


}
