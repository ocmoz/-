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

using WebUtility;
using WebUtility.Components;
using WebUtility.WebControls;
using FM2E.BLL.Basic;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.BLL.System;
using FM2E.Model.Exceptions;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_OutWarehouse_OutWarehouseRecord_ViewOutWarehouseRecord : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    OutWarehouse bll = new OutWarehouse();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
        }
        HeadMenuWebControls1.ButtonList[2].ButtonUrl = string.Format("window.open('ViewOutWarehouseRecordPrint.aspx?id={0}','故障单打印','width=1000,top=0, left=0, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');", id);
        HeadMenuWebControls1.ButtonList[2].ButtonUrlType = UrlType.JavaScript;
        BindButton();
    }
    private void InitialPage()
    {
        try
        {
            //Session.Remove(RECORDLIST_SESSION);

            OutWarehouseApplyInfo item = bll.GetOutWarehouseApplyInfo(id);
            CurrentOutWarehouseApplyInfo = item;
            Label_SheetName.Text = item.SheetName;
            Label_WarehouseName.Text = item.WarehouseName;
            Label_ApplicantName.Text = item.ApplicantName;

            Label_ApplyTime.Text = item.ApplyTime == DateTime.MinValue ? "" : item.ApplyTime.ToString("yyyy-MM-dd HH:mm");
            Label_ApplyRemark.Text = item.ApplyRemark;
            Label_OutTime.Text = item.OutTime == DateTime.MinValue ? "" : item.OutTime.ToString("yyyy-MM-dd HH:mm");
            Label_WarehouseRemark.Text = item.OutWarehouseRemark;

            Label_Status.Text = item.WorkFlowStateDescription;
            Label_OperatorName.Text = item.OperatorName;

            Label_ReceiverName.Text = item.ReceiverName;


           
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }


    private OutWarehouseApplyInfo CurrentOutWarehouseApplyInfo
    {
        get
        {
            OutWarehouseApplyInfo item = (OutWarehouseApplyInfo)Session[this.ToString()];
            if (item == null)
            {
                item = bll.GetOutWarehouseApplyInfo(id);
                Session[this.ToString()] = item;

            }
            return item;
        }
        set
        {
            Session[this.ToString()] = value;
        }
    }

    private void BindButton()
    {
        if (cmd == "viewArchives")
        {
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
           
        }
        else if (cmd == "view")
        {
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = true;
           
        }
    }
    /// <summary>
    /// 初始化GridView1等数据
    /// </summary>
    private void FillData()
    {
        try
        {
            OutWarehouseApplyInfo item = CurrentOutWarehouseApplyInfo;
            Repeater_Detail.DataSource = item.ApplyDetailList;
            Repeater_Detail.DataBind();

            gridview_ApprovalRecord.DataSource = item.ApprovalList;
            gridview_ApprovalRecord.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
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
