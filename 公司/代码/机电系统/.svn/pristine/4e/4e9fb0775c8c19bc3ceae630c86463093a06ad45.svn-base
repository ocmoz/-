using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM2E.Model.Equipment;


using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WebUtility;
using WebUtility.Components;
//using WebUtility.WebControls;
//using FM2E.BLL.Basic;
using FM2E.BLL.Equipment;
//using FM2E.Model.Equipment;
//using FM2E.Model.Basic;
//using FM2E.Model.Utils;
//using FM2E.BLL.System;
//using FM2E.Model.Exceptions;
//using FM2E.WorkflowLayer;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_OutWarehouse_OutWarehouseRecord_ViewOutWarehouseRecordPrint : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    private const string RECORDLIST_SESSION = "OutEquipmentRecordList";
    private const string NOEQUIPMENT = "找不到相应设备";
    OutWarehouse bll = new OutWarehouse();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
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

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        try
        {
            OutWarehouseApplyInfo item = bll.GetOutWarehouseApplyInfo(id);
            CurrentOutWarehouseApplyInfo = item;
            Label_SheetName.Text = item.SheetName;
            Label_WarehouseName.Text = item.WarehouseName;
            Label_ApplicantName.Text = item.ApplicantName;

            Label_ApplyTime.Text = item.ApplyTime == DateTime.MinValue ? "" : item.ApplyTime.ToString("yyyy-MM-dd HH:mm");
            Label_ApplyRemark.Text = item.ApplyRemark;
          
            Label_Status.Text = item.WorkFlowStateDescription;
            Label_OperatorName.Text = item.OperatorName;
            Label_ReceiverName.Text = item.ReceiverName;
            Label_OutTime.Text = item.OutTime == DateTime.MinValue ? "" : item.OutTime.ToString("yyyy-MM-dd HH:mm");

            Label_WarehouseRemark.Text = item.OutWarehouseRemark; 
            LabelArea1.Text = item.ReceiverName;
           
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
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
}
