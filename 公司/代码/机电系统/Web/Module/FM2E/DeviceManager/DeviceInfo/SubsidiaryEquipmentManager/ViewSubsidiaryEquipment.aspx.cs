using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using WebUtility.Components;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using FM2E.Model.Maintain;
using FM2E.BLL.Maintain;

public partial class Module_FM2E_DeviceManager_DeviceInfo_SubsidiaryEquipmentManager_ViewSubsidiaryEquipment : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 20, 0, DataType.Long);
    private readonly SubsidiaryEquipment bll = new SubsidiaryEquipment();
    private readonly MalfunctionHandle malfunctionBll = new MalfunctionHandle();
    //加载页面
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
            FillMaintainRecord();
            ButtonBind();
        }
    }
    //填充数据
    private void FillData()
    {
        if (cmd == "view")
        {
            try
            {
                SubsidiaryEquipmentInfo item = bll.GetSubsidiaryEquipment(id);
                if (item == null)
                {
                    return;
                }
                lbSubsidiaryEquipmentID.Text = Convert.ToString(item.SubsidiaryEquipmentID);
                lbSubsidiaryEquipmentNO.Text = item.SubsidiaryEquipmentNO;
                ViewState["EquipmentNO"] = item.SubsidiaryEquipmentNO;
                lbName.Text = item.Name;
                lbCompanyName.Text = item.CompanyName;
                lbSystemName.Text = item.SystemName;
                lbModel.Text = item.Model;
                lbSpecification.Text = item.Specification;
                lbDetailLocation.Text = item.DetailLocation;
                lbAddressName.Text = item.AddressName;
                lbAssertNumber.Text = item.AssertNumber;
                lbPrice.Text = Convert.ToString(item.Price);
                lbStatus.Text = EnumHelper.GetDescription(item.Status);
                if (DateTime.Compare(item.FileDate, DateTime.MinValue) != 0)
                    lbFileDate.Text = item.FileDate.ToString("yyyy-MM-dd");
                lbMaintenanceTimes.Text = Convert.ToString(item.MaintenanceTimes);
                lbRemark.Text = item.Remark;
                if (DateTime.Compare(item.UpdateTime, DateTime.MinValue) != 0)
                    lbUpdateTime.Text = item.UpdateTime.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        if (cmd == "delete")
        {
            bool bSuccess = false;
            try
            {
                bll.DeleteSubsidiaryEquipment(id);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除" + id + "成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("SubsidiaryEquipment.aspx"), UrlType.Href, "");
            }
        }
    }
    //绑定按钮
    private void ButtonBind()
    {
        HeadMenuWebControls1.ButtonList[0].ButtonUrl = "EditSubsidiaryEquipment.aspx?cmd=edit&id=" + id;
        HeadMenuWebControls1.ButtonList[0].ButtonUrlType = UrlType.Href;
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = true;
        HeadMenuWebControls1.ButtonList[1].ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');", id);
        HeadMenuWebControls1.ButtonList[1].ButtonUrlType = UrlType.JavaScript;
        HeadMenuWebControls1.ButtonList[1].ButtonVisible = true;
    }

    /// <summary>
    /// 维修记录数据绑定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private decimal totalFee = 0;//每次postback都会自动初始化
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            EquipmentMaintainRecordInfo item = e.Row.DataItem as EquipmentMaintainRecordInfo;

            totalFee += item.MaintainFee;
            Literal lt = (Literal)e.Row.FindControl("ltSheetNOTxt");
            if (lt != null)
            {
                lt.Text = string.Format("<a style=\"color: Blue\" href=\"{0}\">{1}</a>", string.Format("javascript:showPopWin('查看故障单','{0}Module/FM2E/MaintainManager/MalFunctionManager/MalfunctionReport/ViewMalfunctionSheet.aspx?id={1}&viewOnly=1',800, 430, null,true,true);", Page.ResolveUrl("~/"), item.SheetID), item.SheetNO);
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.BackColor = System.Drawing.Color.YellowGreen;
            e.Row.Font.Bold = true;
            e.Row.Cells[0].Text = "合计";
            e.Row.Cells[0].ColumnSpan = 2;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;

            e.Row.Cells[1].Visible = false;
            Label lbTotal = e.Row.FindControl("lbTotalFee") as Label;
            if (lbTotal != null)
            {
                lbTotal.Text = totalFee.ToString("#,0.##") + "元";
            }

        }
    }

    /// <summary>
    /// 分页控件换页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        TabContainer1.ActiveTabIndex = 2;
        FillMaintainRecord();
    }
    /// <summary>
    /// 查找维修信息
    /// </summary>
    private void FillMaintainRecord()
    {
        try
        {
            string equipmentNO = (string)ViewState["EquipmentNO"];
            EquipmentMaintainRecordSearchInfo term = new EquipmentMaintainRecordSearchInfo();
            term.EquipmentNO = equipmentNO;

            //查询
            int recordCount = 0;
            IList list = malfunctionBll.GetEquipmentMaintainRecords(term, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out recordCount);
            GridView1.DataSource = list;
            GridView1.DataBind();
            AspNetPager1.RecordCount = recordCount;
            Label_MalFunctionRecordError.Visible = false;
        }
        catch (Exception ex)
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "获取维修记录失败：" + ex.Message);
            Label_MalFunctionRecordError.Visible = true;
        }
    }
}
