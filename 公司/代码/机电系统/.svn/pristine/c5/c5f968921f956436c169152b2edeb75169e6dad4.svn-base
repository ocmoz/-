using System;
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
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using FM2E.Model.Utils;
using WebUtility.Components;
using FM2E.BLL.System;
using FM2E.WorkflowLayer;
using FM2E.Model.Exceptions;
using System.Collections.Generic;
using FM2E.Model.Workflow;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_PriceManager_PriceApproval_PriceApproval : System.Web.UI.Page
{
    string companyid = UserData.CurrentUserData.CompanyID;
    long ApplyFormID = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
        }
    }

    private void InitialPage()
    {
        PriceApplyInfo item = new PriceApplyInfo();
        item.ApplyFormID = ApplyFormID;
        PriceManager bll = new PriceManager();
        QueryParam qp = bll.GeneratePriceApplySearchTerm(item);
        qp.PageSize = 500;
        int recordCount = 0;
        PriceApplyInfo priceapply = (PriceApplyInfo)bll.GetPriceApplyList(qp, out recordCount, companyid)[0];
        User userbll = new User();
        Applicant.Text = userbll.GetUser(priceapply.Applicant).PersonName;
        //Approvaler.Text = priceapply.Approvaler;
        //ApprovalDate.Text = DateTime.Compare(priceapply.ApprovalDate, DateTime.MinValue) == 0 ? "" : priceapply.ApprovalDate.ToString();
        ApplyDate.Text = DateTime.Compare(priceapply.ApplyDate, DateTime.MinValue) == 0 ? "" : priceapply.ApplyDate.ToString();

        DDLWFEvent.Items.Clear();
        List<WorkflowEventInfo> list = WorkflowHelper.GetEventInfoList(PriceWorkflow.WorkflowName, PriceWorkflow.WaitManagerApproveState);
        DDLWFEvent.DataTextField = "Description";
        DDLWFEvent.DataValueField = "Name";
        DDLWFEvent.DataSource = list;
        DDLWFEvent.DataBind();
    }
    /// <summary>
    /// 列表绑定数据源
    /// </summary>
    private void FillData()
    {
        try
        {
            PriceApplyDetailInfo item = new PriceApplyDetailInfo();
            item.ApplyFormID = ApplyFormID;
            PriceManager bll = new PriceManager();
            QueryParam qp = bll.GeneratePriceApplyDetailSearchTerm(item);



            int recordCount = 0;
            IList list = bll.GetPriceApplyDetailList(qp, out recordCount, companyid);
            GridView1.DataSource = list;
            GridView1.DataBind();



        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "审批的指导价格初始化失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 列表绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";
            DropDownList DropDownList1 = e.Row.FindControl("Result") as DropDownList;
            DropDownList1.Items.Clear();
            DropDownList1.Items.Add(new ListItem("请审批", "未审批"));
            DropDownList1.Items.Add(new ListItem("通过", "通过"));
            DropDownList1.Items.Add(new ListItem("不通过", "不通过"));
            //e.Row.Cells[12].Style["DISPLAY"] = "none";

        }
    }
    /// <summary>
    /// 列表行事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void SubmitItem_click(object sender, EventArgs e)
    {
        int approvalcount = 0;
        for (int r = 0; r < GridView1.Rows.Count; r++)
        {
            if (((DropDownList)GridView1.Rows[r].FindControl("Result")).SelectedValue != "未审批")
            {
                approvalcount++;
            }
        }

        if (approvalcount != GridView1.Rows.Count)
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交前请审批完所有的申请", new WebException("提交前请审批完所有的申请"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");

        PriceApplyInfo item = new PriceApplyInfo();
        PriceManager bll = new PriceManager();
        item.ApplyFormID = ApplyFormID;
        item.Approvaler = UserData.CurrentUserData.PersonName;
        item.ApprovalDate = DateTime.Now;
        item.DetailList = new List<PriceApplyDetailInfo>();

        int PassCount = 0;
        for (int r = 0; r < GridView1.Rows.Count; r++)
        {
            PriceApplyDetailInfo item2 = new PriceApplyDetailInfo();
            item2.ApplyFormID = item.ApplyFormID;
            item2.CompanyID = companyid;
            item2.ProductName = GridView1.Rows[r].Cells[1].Text;

            item2.Model = GridView1.Rows[r].Cells[2].Text;
            item2.Unit = GridView1.Rows[r].Cells[3].Text;
            item2.Status = ((DropDownList)GridView1.Rows[r].FindControl("Result")).SelectedValue;
            if (item2.Status == "通过")
                PassCount++;

            if (GridView1.Rows[r].Cells[4].Text != string.Empty)
                item2.StartTime = Convert.ToDateTime(GridView1.Rows[r].Cells[4].Text);

            if (((Label)GridView1.Rows[r].FindControl("OldUpperPrice")).Text != string.Empty)
                item2.OldUpperPrice = Convert.ToDecimal(((Label)GridView1.Rows[r].FindControl("OldUpperPrice")).Text);

            if (((Label)GridView1.Rows[r].FindControl("NewUpperPrice")).Text != string.Empty)
                item2.NewUpperPrice = Convert.ToDecimal(((Label)GridView1.Rows[r].FindControl("NewUpperPrice")).Text);

            if (((Label)GridView1.Rows[r].FindControl("OldLowerPrice")).Text != string.Empty)
                item2.OldLowerPrice = Convert.ToDecimal(((Label)GridView1.Rows[r].FindControl("OldLowerPrice")).Text);

            if (((Label)GridView1.Rows[r].FindControl("NewLowerPrice")).Text != string.Empty)
                item2.NewLowerPrice = Convert.ToDecimal(((Label)GridView1.Rows[r].FindControl("NewLowerPrice")).Text);

            //item2.Reason = ((Label)GridView1.Rows[r].Cells[8].FindControl("Reason")).Text;


            item2.FeeBack = ((TextBox)GridView1.Rows[r].Cells[9].FindControl("FeeBack")).Text;

            switch (GridView1.Rows[r].Cells[0].Text.Trim())
            {
                case "修改":
                    {
                        item2.DeleteOld = Convert.ToInt16(0);
                        break;
                    }
                case "删除":
                    {
                        item2.DeleteOld = Convert.ToInt16(1);
                        break;
                    }
                case "添加":
                    {
                        item2.DeleteOld = Convert.ToInt16(2);
                        break;
                    }
            }
            try
            {

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "审批失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }

            item.DetailList.Add(item2);

        }
        try
        {
            if (PassCount == GridView1.Rows.Count)
            {
                item.Status = PriceApplyStatus.ApprovalPassed;
            }
            else if (PassCount == 0)
            {
                item.Status = PriceApplyStatus.ApprovalFailed;
            }
            else
            {
                item.Status = PriceApplyStatus.PartApprovalPassed;
            }
            item.Approvaler = Common.Get_UserName;
            bll.UpdatePriceApply(item);

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

            WorkflowInstanceInfo info = WorkflowHelper.GetWorkflowInstanceInfo(PriceWorkflow.TableName, item.ApplyFormID);
            WorkflowHelper.SetStateMachine<PriceEventService>(info.InstanceID, DDLWFEvent.SelectedValue);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "审批失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        EventMessage.MessageBox(Msg_Type.Info, "操作成功", "审批成功", new WebException("审批成功"), Icon_Type.OK, true, "~/Module/FM2E/DeviceManager/SpareEquipmentManager/PriceManager/PriceApproval/PriceDetail.aspx", UrlType.Href, "");

    }




}
