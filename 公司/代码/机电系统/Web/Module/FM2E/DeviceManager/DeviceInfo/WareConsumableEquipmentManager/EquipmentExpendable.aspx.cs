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
using FM2E.Model.System;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;

public partial class Module_FM2E_DeviceManager_DeviceInfo_WareConsumableEquipmentManager_EquipmentExpendable : System.Web.UI.Page
{
    private readonly ConsumableEquipment bll = new ConsumableEquipment();
    protected string WarehouseName = "";
    //加载页面
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Warehouse whbll = new Warehouse();
            List<WarehouseInfo> warehouseList = whbll.GetWarehouseListByUserName(UserData.CurrentUserData.UserName);
            if (warehouseList.Count == 0 || warehouseList[0].WareHouseID == null || warehouseList[0].WareHouseID == string.Empty)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作警告", "本页面只允许仓管员进入", new WebException("本页面只允许仓管员进入"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                return;
            }

            //获取仓库WareHouseID
            string wareHouseID = ",";
            foreach (WarehouseInfo wh in warehouseList)
            {
                wareHouseID = wareHouseID + "," + wh.WareHouseID;
            }
            //仓库下拉菜单
            DropDownList_FilterWareHouse.DataSource = warehouseList;
            DropDownList_FilterWareHouse.DataTextField = "Name";
            DropDownList_FilterWareHouse.DataValueField = "AddressCode";
            DropDownList_FilterWareHouse.DataBind();
            DropDownList_FilterWareHouse.Items.Insert(0, new ListItem("--ALL--", "--All--"));

            InitialPage();
            FillDataByWarehouseID(wareHouseID);
            PermissionControl();
        }
        Hidden_WarehouseAddressCode.Value = DropDownList_FilterWareHouse.Items[DropDownList_FilterWareHouse.SelectedIndex].Value;
        WarehouseName = DropDownList_FilterWareHouse.Items[DropDownList_FilterWareHouse.SelectedIndex].Text;
    }
    //初始化页面
    private void InitialPage()
    {
        try
        {
            LoginUserInfo loginUser = UserData.CurrentUserData;

            //维修单位
            //ddlMaintainTeam.Items.Clear();
            //ddlMaintainTeam.Items.Add(new ListItem("不限", "0"));
            //ddlMaintainTeam.Items.AddRange(ListItemHelper.GetAllMaintainTeams(loginUser.CompanyID));

            
            Company bllcompany = new Company();
            IList list = (List<CompanyInfo>)bllcompany.GetAllCompany();

            foreach (CompanyInfo item in list)
            {
                DDLCompany.Items.Add(new ListItem(item.CompanyName,item.CompanyID));
            }

            
            
        }
        catch (Exception ex)
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "初始化页面失败：" + ex.Message);
        }
    }
    //权限控制
    private void PermissionControl()
    {
        //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
        if (SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
        else
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;

        if (SystemPermission.CheckPermission(PopedomType.New))
        {
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = true;
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = true;
        }
        else
        {
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
        }
        //********** Modification Finished 2011-09-09 **********************************************************************************************
    }
    //填充数据
    private void FillData()
    {
        try
        {
            int listCount = 0;
            QueryParam searchTerm = (QueryParam)ViewState["SearchTerm"];
            if (searchTerm == null)
            {
                searchTerm = new QueryParam(1, 10);
                searchTerm.Where = "";
            }
            searchTerm.PageSize = AspNetPager1.PageSize;
            searchTerm.PageIndex = AspNetPager1.CurrentPageIndex;
            
            IList list = bll.GetExpendasList(searchTerm, out listCount);
            AspNetPager1.RecordCount = listCount;
            GridView1.DataSource = list;
            GridView1.DataBind();

            searchTerm.PageSize = Int32.MaxValue;
            //searchTerm.TableName = "resetcompany";
            lbCurrentDeviceCount.Text = bll.GetCurrentExpendasDeviceCount(searchTerm, DDLCompany.SelectedValue).ToString();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败"+ex.Message, ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    //根据仓库ID填充数据
    private void FillDataByWarehouseID(string wareHouseID)
    {
        try
        {
            int listCount = 0;
            QueryParam searchTerm = (QueryParam)ViewState["SearchTerm"];
            if (searchTerm == null)
            {
                searchTerm = new QueryParam(1, 10);
                searchTerm.Where = "";
            }
            searchTerm.PageSize = AspNetPager1.PageSize;
            searchTerm.PageIndex = AspNetPager1.CurrentPageIndex;

            IList list = bll.GetExpendasListByWarehouseID(searchTerm, out listCount,wareHouseID);
            AspNetPager1.RecordCount = listCount;
            GridView1.DataSource = list;
            GridView1.DataBind();

            searchTerm.PageSize = Int32.MaxValue;
            //searchTerm.TableName = "resetcompany";
            lbCurrentDeviceCount.Text = bll.GetCurrentExpendasDeviceCount(searchTerm, DDLCompany.SelectedValue).ToString();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败" + ex.Message, ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    protected void Button_FillData_Click(Object sender, EventArgs e)
    {
        FillData();
    }

    //行命令
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        string t = gvRow.Cells[3].ToString();
        long id = Convert.ToInt64(gvRow.Attributes["ConsumableEquipmentID"]);
        if (e.CommandName == "view")
        {
            Response.Redirect("ViewEquipmentExpendable.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "del")
        {
            bool bSuccess = false;
            try
            {
                bll.DeleteExpendasExpendable(id);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("EquipmentExpendable.aspx"), UrlType.Href, "");
            }
        }
    }
    //行数据绑定
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //根据库存和保险库存设置库存的背景色
            WareHouseConsumableEquipmentInfo item = (WareHouseConsumableEquipmentInfo)e.Row.DataItem;
            if (item.Count < item.ProducerID)
                //    e.Row.Cells[4].BackColor = System.Drawing.Color.Green;
                //e.Row.ControlStyle.ForeColor = System.Drawing.Color.Red;
                //e.Row.ForeColor = System.Drawing.Color.Red;
                e.Row.BackColor = System.Drawing.Color.Red;
            else
            {
                //鼠标移动到每项时颜色交替效果 
                e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
                e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");
                //ExpendableInfo item = (ExpendableInfo)e.Row.DataItem;
                //e.Row.Attributes["ExpendableID"] = item.ExpendableID.ToString();
            }

            //设置悬浮鼠标指针形状为"小手"
            e.Row.Attributes["style"] = "Cursor:hand";
            item = (WareHouseConsumableEquipmentInfo)e.Row.DataItem;
            e.Row.Attributes["ConsumableEquipmentID"] = item.ConsumableEquipmentID.ToString();
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        WareHouseConsumableEquipmentInfo item = new WareHouseConsumableEquipmentInfo();
        item.ConsumableEquipmentNO = Common.inSQL(tbConsumableEquipmentNO.Text.Trim());
        item.Name = Common.inSQL(tbName.Text.Trim());
        item.SystemID = Common.inSQL(tbSystemID.Text.Trim());
        item.SerialNum = Common.inSQL(tbSerialNum.Text.Trim());
        item.Model = Common.inSQL(tbModel.Text.Trim());
        item.Specification = Common.inSQL(tbSpecification.Text.Trim());
        item.AssertNumber = Common.inSQL(tbAssertNumber.Text.Trim());
        item.Unit = Common.inSQL(tbUnit.Text.Trim());
        item.WareHouseName = Common.inSQL(tbWareHouseName.Text.Trim());
        item.AddressName = Common.inSQL(tbAddressName.Text.Trim());
        
        if (tbCount.Text.Trim() != "")
        {
            item.Count = Convert.ToInt32(tbCount.Text.Trim());
        }
        else
        {
            item.Count = 0;
        }
        if (tbPrice.Text.Trim() != "")
        {
            item.Price = Convert.ToDecimal(tbPrice.Text.Trim());
        }
        else
        {
            item.Price = 0;
        }
        item.Remark = Common.inSQL(tbRemark.Text.Trim());
        if (item.CompanyID != "")
            item.CompanyID = DDLCompany.SelectedValue;

        QueryParam searchTerm = bll.GenerateExpendasSearchTerm(item);
        TabContainer1.ActiveTabIndex = 0;
        ViewState["SearchTerm"] = searchTerm;
        FillData();
    }


    private void Filter()
    {
        if (DropDownList_FilterWareHouse.SelectedIndex != 0)
        {
            WareHouseConsumableEquipmentInfo item = new WareHouseConsumableEquipmentInfo();
            item.AddressCode = DropDownList_FilterWareHouse.Items[DropDownList_FilterWareHouse.SelectedIndex].Value;
            QueryParam searchTerm = bll.GenerateExpendasSearchTerm(item);
            TabContainer1.ActiveTabIndex = 0;
            ViewState["SearchTerm"] = searchTerm;
            FillData();
        }
        else
        {
            ViewState["SearchTerm"] = null;
            Warehouse whbll = new Warehouse();
            List<WarehouseInfo> warehouseList = whbll.GetWarehouseListByUserName(UserData.CurrentUserData.UserName);
            //获取仓库WareHouseID
            string wareHouseID = ",";
            foreach (WarehouseInfo wh in warehouseList)
            {
                wareHouseID = wareHouseID + "," + wh.WareHouseID;
            }
            FillDataByWarehouseID(wareHouseID);

        }
    }

    protected void OnFilter(object sender, EventArgs e)
    {
        Filter();
    }

    private QueryParam CurrentQueryParam
    {
        get
        {
            QueryParam qp = (QueryParam)ViewState["SearchTerm"];
            if (qp == null)
            {
                if (qp == null)
                {
                    qp = new QueryParam();
                }
                qp.PageIndex = 1;
                qp.PageSize = AspNetPager1.PageSize;
            }
            return qp;
        }
        set { ViewState["SearchTerm"] = value; }
    }
    //导出
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ConsumableEquipment eqbll = new ConsumableEquipment();
        QueryParam searchTerm = CurrentQueryParam;
        IList list = eqbll.GetExportList(searchTerm);
        string timeFile = "";
        try
        {
            int beginRow = 2;//开始复制的行

            //实例化一个Excel助手工具类
            timeFile = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            string fileName = "设备台账导出" + timeFile;
            ExcelHelper ex = new ExcelHelper(Request.PhysicalApplicationPath + "\\public\\Excel\\WarehouseBook.xls", Server.MapPath("~/public/temp") + "/" + timeFile);

            ex.DeviceExpansListToExcel(list, beginRow, 1);

            ex.SetRangeBordStyle1(1, 1, 1 + list.Count, 15);

            ex.OutputExcelFile();
            GC.Collect();
            //发送到客户端
            Response.ClearContent();

            Response.ClearHeaders();

            Response.ContentType = "application/vnd.ms-excel";

            Response.AddHeader("Content-Disposition", "inline;filename=" + HttpUtility.UrlEncode(fileName) + "");

            Response.WriteFile(Server.MapPath("~/public/temp") + "/" + timeFile);//FileName为Excel文件所在地址

            Response.Flush();

            Response.Close();

            System.IO.File.Delete(Server.MapPath("~/public/temp") + "/" + timeFile);

            Response.End();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        EventMessage.MessageBox(Msg_Type.Info, "操作成功", "导入成功：" + list.Count, Icon_Type.OK, false, Common.GetHomeBaseUrl("EquipmentExpendable.aspx"), UrlType.Href, "");
    }
}
