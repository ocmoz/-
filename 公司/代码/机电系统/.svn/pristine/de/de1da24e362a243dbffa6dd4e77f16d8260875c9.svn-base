using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;

using FM2E.Model.Basic;
using FM2E.Model.System;
using FM2E.Model.Equipment;
using FM2E.Model.Exceptions;

using FM2E.BLL.Basic;
using FM2E.BLL.System;
using FM2E.BLL.Equipment;


public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_ExecutePurchasing_ViewCAForm : System.Web.UI.Page
{
    CheckAcceptance checkaccptanceBll = new CheckAcceptance();
    /// <summary>
    /// 仓库业务逻辑处理类对象
    /// </summary>
    Warehouse warehouseBll = new Warehouse();
    /// <summary>
    /// 校验用户名
    /// </summary>
    User userBll = new User();
    /// <summary>
    /// 员工业务逻辑处理类对象，用于获取仓库管理员
    /// </summary>
    Warehouse staffBll = new Warehouse();


    string sessionName = "Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_ExecutePurchasing_ViewCAForm";


    /// <summary>
    /// 需要查看的报验单ID
    /// </summary>
    protected long id = (long)Common.sink("id", MethodType.Get, 0, 0, DataType.Long);

    /// <summary>
    /// 加载页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
        }

    }

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        Session[sessionName] = null;

        FillData();
    }



    /// <summary>
    /// 往对应的控件填入数据
    /// </summary>
    private void FillData()
    {
        CheckAcceptanceInfo form = (CheckAcceptanceInfo)Session[sessionName];
        if (form == null)
        {
            form = checkaccptanceBll.GetCheckAcceptanceInfoAllDetail(id);
            Session[sessionName] = form;
        }
        //公司ID
        CompanyInfo company = new Company().GetCompany(form.CompanyID);
        if (company != null)
            Label_CompanyName.Text = company.CompanyName;

        Label_SheetID.Text = form.SheetID;
        Label_SheetName.Text = form.SheetName;

        Label_Status.Text = form.StatusString;


        Label_UpdateTime.Text = form.UpdateTime == DateTime.MinValue ? "" : form.UpdateTime.ToString("yyyy-MM-dd HH:mm");

        Label_SubmitTime.Text = form.UpdateTime == DateTime.MinValue ? "" : form.SubmitTime.ToString("yyyy-MM-dd HH:mm");

        Label_ApplicantName.Text = form.ApplicantName;

        Repeater_PurchaseRecordList.DataSource = form.DetailList;
        Repeater_PurchaseRecordList.DataBind();

    }


    /// <summary>
    /// 数据绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Repeater_ItemList_RowDataBound(object sender, RepeaterItemEventArgs e)
    {

        CheckAcceptanceDetailInfo item = e.Item.DataItem as CheckAcceptanceDetailInfo;
        HtmlTableRow tr = e.Item.FindControl("tr_item") as HtmlTableRow;
        if ( item.Purchaser== Common.Get_UserName)
        {
            tr.BgColor = "LightYellow";

        }
        else
        {
            tr.BgColor = "Transparent";
        }

    }


    protected void Repeater_ItemList_RowCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName != null && e.CommandName != "")
        {
            CheckAcceptanceInfo form = (CheckAcceptanceInfo)Session[sessionName];
            if (form == null)
            {
                form = checkaccptanceBll.GetCheckAcceptanceInfoAllDetail(id);
                Session[sessionName] = form;
            }

            short itemid = short.Parse(e.CommandArgument.ToString());
            CheckAcceptanceDetailInfo target = null;
            foreach (CheckAcceptanceDetailInfo item in form.DetailList)
            {
                if (item.ItemID == itemid)
                {
                    target = item;
                    break;
                }
            }

            switch (e.CommandName)
            {
                case "CONFIRM":
                    target.PurchaserConfirm = true;
                    target.PurchaserConfirmTime = DateTime.Now;
                    break;
                default:
                    break;
            }
            try
            {
                checkaccptanceBll.UpdateCheckAcceptanceDetail(target);
            }
            catch(Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "核实失败", "核实失败，请重试", ex, Icon_Type.Error, true, "history.go(-1);", UrlType.JavaScript, "");
                form = checkaccptanceBll.GetCheckAcceptanceInfoAllDetail(id);
                return;
            }
           
 
            FillData();
        }
    }
}
