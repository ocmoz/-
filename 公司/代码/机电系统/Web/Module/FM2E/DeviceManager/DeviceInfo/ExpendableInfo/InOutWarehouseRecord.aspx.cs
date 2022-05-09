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
using WebUtility.WebControls;
using WebUtility.Components;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using System.Collections.Generic;


public partial class Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_InOutWarehouseRecord : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    decimal inCount = 0;
    decimal outCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
            FillData();
            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            PermissionControl();
            //********** Modification Finished 2011-09-09 **********************************************************************************************
        }
    }

    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
    private void PermissionControl()
    {
        GridView1.Columns[GridView1.Columns.Count - 1].Visible = SystemPermission.CheckPermission(PopedomType.Delete);
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************


    /// <summary>
    /// 
    /// </summary>
    private void InitPage()
    {
        Expendable Expendable = new Expendable();
        ExpendableInfo s = Expendable.GetExpendable(id);
        Label_Model.Text = s.Model;
        Label_Name.Text = s.Name;
        Hidden_WarehouseID.Value = s.WarehouseID;
        //时间，默认本月
        TB_TimeLower.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
        TB_TimeUpper.Text = DateTime.Now.ToString("yyyy-MM-dd");

        //公司
        DDL_OutDepartment.Items.Clear();
        //DDL_OutDepartment.Items.AddRange(ListItemHelper.GetDepartmentListItemsWithBlank(UserData.CurrentUserData.CompanyID));
        DDL_OutDepartment.Items.AddRange(ListItemHelper.GetCompanyListItemsWithBlank());
    }

    private void FillData()
    {
        lbStatisticsMsg.Text = "";
        ExpendableInOut bll = new ExpendableInOut();

        int listCount = 0;


        ExpendableInOutRecordSearchInfo searchTerm = CurrentSearchInfo;

        IList list = bll.SearchRecord(searchTerm,AspNetPager1.CurrentPageIndex,AspNetPager1.PageSize, out listCount);
        AspNetPager1.RecordCount = listCount;
        GridView1.DataSource = list;
        GridView1.DataBind();
        if (list == null || list.Count == 0)
        {
        }
        else
        {
            lbStatisticsMsg.Text = "入库共量为：" + Convert.ToInt32(inCount) + "，出库共量为：" + Convert.ToInt32(outCount);
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }

    private ExpendableInOutRecordSearchInfo CurrentSearchInfo
    {
        get
        {
            ExpendableInOutRecordSearchInfo info = (ExpendableInOutRecordSearchInfo)ViewState["SearchTerm"];
            if (info == null)
            {
                info = new ExpendableInOutRecordSearchInfo();
                info.WarehouseID = Hidden_WarehouseID.Value;
                info.Name = Label_Name.Text;
                info.Model = Label_Model.Text;

                DateTime lower = DateTime.MinValue;
                DateTime.TryParse(TB_TimeLower.Text.Trim(), out lower);
                info.InOutTimeLower = lower;

                DateTime upper = DateTime.MinValue;
                DateTime.TryParse(TB_TimeUpper.Text.Trim(), out upper);
                info.InOutTimeUpper = upper;

                info.CompanyID = DDL_OutDepartment.SelectedValue;
            }

            return info;
        }
        set { ViewState["SearchTerm"] = value; }
    }
    protected void Button_Search_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        FillData();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long expendableid = Convert.ToInt64(gvRow.Attributes["ID"]);
        if (e.CommandName == "del")
        {
            try
            {
                ExpendableInOut bll = new ExpendableInOut();
                bll.DelExpendableInOut(expendableid,id);
                FillData();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
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

            ExpendableInOutRecordInfo item = (ExpendableInOutRecordInfo)e.Row.DataItem;
            e.Row.Attributes["ID"] = item.ID.ToString();
            if (item.Type == ExpendableInOutRecordType.In)
            {
                inCount += item.Amount;
            }
            else if (item.Type == ExpendableInOutRecordType.Out)
            {
                outCount += item.Amount;
            }

        }

    }

}
