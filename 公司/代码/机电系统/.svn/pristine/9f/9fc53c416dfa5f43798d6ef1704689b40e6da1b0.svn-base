using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;

using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;

using FM2E.WorkflowLayer;
using FM2E.Model.Workflow;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_OutWarehouse_OutWarehouseApply_ViewOutWarehouseApply : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonBind();
            BindData();
        }
    }
    /// <summary>
    /// 删除和修改操作
    /// </summary>
    private void ButtonBind()
    {
        if (cmd == "viewArchives")
        {
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
        }
        if (cmd == "view")
        {
            //删除
            HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[1];
            button.ButtonUrlType = UrlType.JavaScript;
            button.ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');", id);
            //修改
            button = HeadMenuWebControls1.ButtonList[0];
            button.ButtonUrlType = UrlType.Href;
            button.ButtonUrl = string.Format("EditOutWarehouseApply.aspx?cmd=edit&id={0}", id);
        }
        else if (cmd == "delete")
        {
            //执行删除操作
            bool bSuccess = false;
            try
            {
                OutWarehouse bll = new OutWarehouse();
                bll.DeleteApplyInfo(id);
                //工作流删除
                WorkflowInstanceInfo wif = WorkflowHelper.GetWorkflowInstanceInfo(OutWarehouseWorkflow.TableName, id);
                WorkflowHelper.SetStateMachine<OutWarehouseEventService>
                    (wif.InstanceID, OutWarehouseWorkflow.DeleteDraftEvent);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess == true)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除申请成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("OutWarehouseApply.aspx"), UrlType.Href, "");
            }
        }
    }
    /// <summary>
    /// 初始化数据
    /// </summary>
    private void BindData()
    {
        OutWarehouse bll = new OutWarehouse();
        OutWarehouseApplyInfo item = bll.GetOutWarehouseApplyInfo(id);

        Label_ApplicantName.Text = item.ApplicantName;
        Label_ApplyRemark.Text = item.ApplyRemark;
        Label_ApplyTime.Text = item.OutTime == DateTime.MinValue ? "" : item.ApplyTime.ToString("yyyy-MM-dd HH:mm");
        Label_OperatorName.Text = item.OperatorName;
        Label_OutTime.Text = item.OutTime == DateTime.MinValue ? "" : item.OutTime.ToString("yyyy-MM-dd HH:mm");
        Label_ReceiverName.Text = item.ReceiverName;
        Label_SheetName.Text = item.SheetName;
        Label_Status.Text = item.WorkFlowStateDescription;
        Label_WarehouseName.Text = item.WarehouseName;
        Label_WarehouseRemark.Text = item.OutWarehouseRemark;

        GridView_ApplyDetail.DataSource = item.ApplyDetailList;
        GridView_ApplyDetail.DataBind();

        gridview_ApprovalRecord.DataSource = item.ApprovalList;
        gridview_ApprovalRecord.DataBind();
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
        HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;

        //**********Modified by Xue 2011-6-27****************************************************************************************************
        //---if (item.WorkFlowStateName == OutWarehouseWorkflow.DraftState)
        //---{
        //---    HeadMenuWebControls1.ButtonList[0].ButtonVisible = true;
        //---    HeadMenuWebControls1.ButtonList[1].ButtonVisible = true;
        //---}
        //---if (item.WorkFlowStateName == OutWarehouseWorkflow.ReturnModifyState)
        //---{
        //---    HeadMenuWebControls1.ButtonList[0].ButtonVisible = true;

        //---}

        if (item.WorkFlowStateName == OutWarehouseWorkflow.DraftState && cmd == "view") 
        {
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = true;
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = true;
        }
        if (item.WorkFlowStateName == OutWarehouseWorkflow.ReturnModifyState && cmd == "view")
        {
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = true;
        }
        //**********Modification Finished 2011-6-27***********************************************************************************************
    }
   

}
