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
using FM2E.BLL.System;
using FM2E.Model.Equipment;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.WorkflowLayer;
using FM2E.BLL.Utils;
using FM2E.Model.Exceptions;


public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_InWarehouse_EditInWarehouse : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    private const string NOEQUIPMENT = "找不到相应设备";
    private readonly Warehouse bllwh = new Warehouse();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //判断是否仓管员

            WarehouseInfo warehouse = bllwh.GetWarehouseByUserName(UserData.CurrentUserData.UserName);
            if (warehouse == null || warehouse.WareHouseID == null || warehouse.WareHouseID == string.Empty)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作警告", "本页面只允许仓管员进入", new WebException("本页面只允许仓管员进入"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                return;
            }
            CurrentWarehouse = warehouse;
            InitialPage();
            FillData();
            ButtonBind();


        }
    }

    /// <summary>
    /// 当前的仓库信息
    /// </summary>
    private WarehouseInfo CurrentWarehouse
    {
        get
        {
            WarehouseInfo warehouse = (WarehouseInfo)ViewState["CurrentWarehouse"];
            if (warehouse == null)
            {
                warehouse = new Warehouse().GetWarehouseByUserName(UserData.CurrentUserData.UserName);
            }
            return warehouse;
        }
        set
        {
            ViewState["CurrentWarehouse"] = value;
        }
    }

    /// <summary>
    /// 当前工作的列表
    /// </summary>
    private IList InWarehouseList
    {
        get
        {
            IList list = (IList)Session[this.ToString()];
            if (list == null)
                list = new List<InEquipmentsInfo>();
            return list;
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
            //Warehouse bll = new Warehouse();
            //IList list = bll.GetAllWarehouseByCompany(UserData.CurrentUserData.CompanyID);
            //DropDownList1.Items.Clear();
            //DropDownList1.Items.Add(new ListItem("请选择仓库", ""));
            //foreach (WarehouseInfo item in list)
            //{
            //    DropDownList1.Items.Add(new ListItem(item.Name, item.WareHouseID.ToString()));
            //}

            InWarehouseList.Clear();

            Department departmentBll = new Department();
            DropDownList_Department.Items.AddRange(ListItemHelper.GetDepartmentListItemsWithBlank(UserData.CurrentUserData.CompanyID));


            DropDownList_ExpendableType.Items.Clear();
            DropDownList_ExpendableType.Items.AddRange(ListItemHelper.GetCategoryListItems());

            //DDLProduct.Visible = false;
            //DDLModel.Visible = false;
            TBCount.Text = "1";
            TBCount.Enabled = false;
            DDLUnit.Items.Clear();
            IList list1 = Constants.GetUnits();
            foreach (string unit in list1)
                DDLUnit.Items.Add(new ListItem(unit, unit));
            DDLUnit.Items.Insert(0, new ListItem("请选择单位", ""));
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 初始化GridView等数据
    /// </summary>

    private void FillData()
    {
        try
        {
            //if (UserData.CurrentUserData.CompanyID != null && UserData.CurrentUserData.CompanyID != string.Empty)
            //{
            //    CascadingDropDown1.SelectedValue = UserData.CurrentUserData.CompanyID;
            //}
            InEquipmentsInfo detail = new InEquipmentsInfo();
            #region 编辑
            if (cmd == "edit")
            {
                InWarehouse bll = new InWarehouse();
                InWarehouseInfo item = bll.GetInWarehouse(id);
                ViewState["editItem"] = item;
                //DropDownList1.SelectedValue = item.WarehouseID;
                //CascadingDropDown1.SelectedValue = item.CompanyID;
                //CascadingDropDown2.SelectedValue = item.DepartmentID.ToString();
                TextArea1.Value = item.Remark;
                detail.ID = id;
                int listCount = 0;
                InEquipments bll1 = new InEquipments();
                QueryParam searchTerm = bll1.GenerateSearchTerm(detail);
                //searchTerm.PageIndex = AspNetPager1.CurrentPageIndex;
                IList list = bll1.GetList(searchTerm, out listCount);
                //AspNetPager1.RecordCount = listCount;
                GridView1.DataSource = list;
                GridView1.DataBind();
            }
            #endregion
            else if (cmd == "add")
            {
                GridView1.DataSource = InWarehouseList;
                GridView1.DataBind();
            }
            LblErrorMessage.Text = "";
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

    }

    private void ButtonBind()
    {
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：入库信息添加";

            TabPanel1.HeaderText = "添加入库";
        }
        #region 编辑
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：入库信息修改";

            TabPanel1.HeaderText = "修改入库";
        }
        #endregion
    }
    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSubmit_Click(object sender, EventArgs e)
    {
        //先检查借用人的用户名与密码是否相符
        string errorMsg = "";
        if (tbApplicant.Text.Trim() == "")
            errorMsg = "请输入领用人用户名";
        else if (tbPassword.Text.Trim() == "")
            errorMsg = "请输入领用人密码";

        if (InWarehouseList.Count == 0)
        {
            EventMessage.MessageBox(Msg_Type.Warn, "操作失败", "没有入库信息" , Icon_Type.Alert, true, "history.go(-1)", UrlType.JavaScript, "");
            return;

        }

        if (string.IsNullOrEmpty(DropDownList_Department.SelectedValue))
        {
            EventMessage.MessageBox(Msg_Type.Warn, "操作失败", "没有选择部门", Icon_Type.Alert, true, "history.go(-1)", UrlType.JavaScript, "");
            return;

        }

        if (errorMsg != "")
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入有误：" + errorMsg, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        bool bValidate = false;
        try
        {
            User bll = new User();
            bValidate = bll.ValidatePassword(tbApplicant.Text.Trim(), Common.md5(tbPassword.Text.Trim(), 32));
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "校验领用人用户名密码时发生错误", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        if (!bValidate)
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "校验领用人用户名密码不相符", Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");

        bool bSuccess = false;
        if (cmd == "add")
        {
            try
            {
                InWarehouseInfo item = new InWarehouseInfo();

                item.CompanyID = UserData.CurrentUserData.CompanyID;
                item.WarehouseID = CurrentWarehouse.WareHouseID;
                item.WarehouseAddressID = CurrentWarehouse.AddressID;
                item.WarehouseDetailLocation = "";
                item.SubmitTime = DateTime.Now;
                item.ApplicantID = tbApplicant.Text.Trim();
                item.OperatorID = Common.Get_UserName;
                item.Remark = TextArea1.Value.Trim();
                item.IsDeleted = false;
                item.InWarehouseList = InWarehouseList;
                item.SheetName = SheetNOGenerator.GetSheetNO(UserData.CurrentUserData.CompanyID, SheetType.INWAREHOUSESHEET);
                InWarehouse bll = new InWarehouse();
                item.DepartmentID = Convert.ToInt64(DropDownList_Department.SelectedValue);
                bll.InsertInWarehouseWithDetail(item);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", ex.Message, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加入库成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("InWarehouse.aspx"), UrlType.Href, "");
            }
        }
        #region 编辑
        else if (cmd == "edit")
        {
            try
            {
                InWarehouseInfo item = (InWarehouseInfo)ViewState["editItem"];
                item.ID = id;
                //item.WarehouseID = DropDownList1.SelectedValue;
                //item.CompanyID = DropDownList2.SelectedValue;
                //if (CascadingDropDown2.SelectedValue != "")
                //    item.DepartmentID = Convert.ToInt64(DropDownList3.SelectedValue);
                //else
                //    item.DepartmentID = 0;
                item.Remark = TextArea1.Value.Trim();
                item.ApplicantID = tbApplicant.Text.Trim();
                item.OperatorID = Common.Get_UserName;
                InWarehouse bll = new InWarehouse();
                bll.UpdateInWarehouse(item);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改入库失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改入库成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("InWarehouse.aspx"), UrlType.Href, "");
            }
        }
        #endregion

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
            InEquipmentsInfo item = (InEquipmentsInfo)e.Row.DataItem;
            e.Row.Attributes["itemID"] = item.ItemID.ToString();
        }

    }


    /// <summary>
    /// CheckBox1变化时的响应
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {
            //DDLProduct.Visible = false;
            //DDLModel.Visible = false;
            TBProduct.Visible = true;
            TBModel.Visible = true;
            TBEquipment.Visible = true;
            TBCount.Text = "1";
            TBCount.Enabled = false;
            tr_button_add.Visible = false;
            tr_expandable.Visible = false;
            Button_UpdateExpendable.Visible = false;
            TBEquipment.Focus();
        }
        else
        {
            TBEquipment.Visible = false;
            //DDLProduct.Visible = true;
            //DDLModel.Visible = true;
            TBProduct.Visible = true;
            TBModel.Visible = true;
            TBCount.Enabled = true;
            tr_button_add.Visible = true;
            tr_expandable.Visible = true;
            Button_UpdateExpendable.Visible = true;
        }
        TBProduct.Text = "";
        TBModel.Text = "";
        DropDownList_ExpendableType.SelectedIndex = 0;
        TextBox_UnitPrice.Text = "";
        LblErrorMessage.Text = "";
    }
    /// <summary>
    /// TBEquipment的文本变化时，即输入设备条形码后响应的事件，主要是完成对设备名和型号的自动完成
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TBEquipment_TextChanged(object sender, EventArgs e)
    {
        try
        {
            LblErrorMessage.Text = "";
            Equipment bll = new Equipment();
            EquipmentInfoFacade equipment = bll.GetEquipmentBYNO(TBEquipment.Text.Trim());
            if (equipment != null && equipment.EquipmentID!=0)
            {
                TBProduct.Text = equipment.Name;
                TBModel.Text = equipment.Model;
                if (DDLUnit.Items.FindByValue(equipment.Unit) != null)
                {
                    DDLUnit.SelectedValue = equipment.Unit;
                }
                else
                {
                    DDLUnit.Items.Add(new ListItem(equipment.Unit, equipment.Unit));
                    DDLUnit.SelectedValue = equipment.Unit;
                }
                Button3_Click(sender, e);
            }
            else
            {
                LblErrorMessage.Text = NOEQUIPMENT;
                TBProduct.Text = "";
                TBModel.Text = "";
                TBEquipment.Focus();
                TBEquipment.Attributes.Add("onfocus", "f(this)");
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 添加明细和更新明细响应的事件
    /// 分两种情况处理
    /// 1.添加申请时，明细保存在Session["AddInWarehouseList"]中，提交申请时使用事务一次性提交数据库
    /// 2.编辑申请时，编辑明细直接操作数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button3_Click(object sender, EventArgs e)
    {
        InEquipmentsInfo item = new InEquipmentsInfo();
        //item.WarehouseID = DropDownList1.SelectedValue;
        if (TBCount.Text.Trim() != "")
            item.Count = Convert.ToDecimal(TBCount.Text.Trim());
        else
            item.Count = 1;//默认数量为1
        if (CheckBox1.Checked)
        {
            item.IsAsset = true;
            item.EquipmentNO = TBEquipment.Text.Trim();
            item.ExpendableID = 0;
            if (TBEquipment.Text.Trim().Equals(string.Empty))
                LblErrorMessage.Text = "请输入设备条形码";
            else if (TBProduct.Text.Trim().Equals(NOEQUIPMENT))
                LblErrorMessage.Text = "没有对应该条形码的设备";
            if (!LblErrorMessage.Text.Trim().Equals(string.Empty))
                return;
            TBCount.Text = "1";
        }
        else
        {
            item.IsAsset = false;
            item.EquipmentNO = "";
            if (string.IsNullOrEmpty(TBProduct.Text.Trim())||string.IsNullOrEmpty(TBModel.Text.Trim()))
            {
                LblErrorMessage.Text = "请选择易耗品名称和型号";
                return;
            }
            else if (TBCount.Text.Trim().Equals(string.Empty))
            {
                LblErrorMessage.Text = "请输入易耗品数量";
                return;
            }
            item.ExpendableID = 0;// Convert.ToInt64(DDLModel.SelectedValue);
            if (DropDownList_ExpendableType.SelectedIndex == 0)
            {
                LblErrorMessage.Text = "请选择易耗品种类";
                return;
            }


            item.ExpendableTypeID = Convert.ToInt64(DropDownList_ExpendableType.SelectedValue);
            decimal price = 0;
            if ((!decimal.TryParse(TextBox_UnitPrice.Text.Trim(), out price)) || (!(price >= 0)))
            {
                LblErrorMessage.Text = "请输入正确的易耗品单价";
                return;
            }
            item.ExpendablePrice = Convert.ToDecimal(TextBox_UnitPrice.Text.Trim());
            TBCount.Text = "";
        }
        if (DDLUnit.SelectedValue.Equals(string.Empty))
        {
            LblErrorMessage.Text = "请选择单位";
            return;
        }
        LblErrorMessage.Text = "";
        item.Unit = DDLUnit.SelectedValue;
        item.InTime = DateTime.Now;


        IList list = InWarehouseList;

        if (item.EquipmentNO != "")
        {
            foreach (InEquipmentsInfo i in list)
            {
                if (i.EquipmentNO == item.EquipmentNO)
                {
                    LblErrorMessage.Text = "该设备条形码已存在";
                    TBEquipment.Focus();
                    TBEquipment.Attributes.Add("onfocus", "f(this)");
                    return;
                }
            }
        }
        InEquipmentsInfo info = new InEquipmentsInfo();

        item.WarehouseID = CurrentWarehouse.WareHouseID;
        
        //item.EquipmentModel = TBModel.Text.Trim();
        //item.EquipmentName = TBProduct.Text.Trim();
        //item.ExpendableModel = DDLModel.SelectedItem.Text;
        //item.ExpendableName = DDLProduct.SelectedItem.Text;
        item.Name = TBProduct.Text.Trim();
        item.Model = TBModel.Text.Trim();

        list.Add(item);
        InWarehouseList = list;

        TBEquipment.Text = TBProduct.Text = TBModel.Text = "";
        //CascadingDropDown3.SelectedValue = "";
        //CascadingDropDown4.SelectedValue = "";
        DDLUnit.SelectedIndex = -1;
        FillData();

        if (CheckBox1.Checked)
        {
            TBEquipment.Focus();
        }

    }
    ///// <summary>
    ///// “完成”操作
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void Button4_Click(object sender, EventArgs e)
    //{
    //    TabContainer1.ActiveTabIndex = 0;
    //    LblErrorMessage.Text = "";
    //}
    /// <summary>
    /// “编辑明细”操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //    protected void Button2_Click(object sender, EventArgs e)
//    {
//        if (cmd == "add")
//        {
//            ViewState["NewInWarehouseID"] = 1;
//            //ArrayList list = new ArrayList();
//            //Session["AddInWarehouseList"] = list;
//        }
//        TabContainer1.ActiveTabIndex = 1;
////        TabPanel2.Visible = true;
//        FillData();
//        TBEquipment.Focus();
//    }


    /// <summary>
    /// 删除明细项
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = int.Parse(e.CommandArgument.ToString());
        LblErrorMessage.Text = "";
        if (e.CommandName == "del")
        {
            try
            {
                InWarehouseList.RemoveAt(index);
                FillData();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }

    /// <summary>
    /// 产品选择
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DDLProduct_SelectedChanged(object sender, EventArgs e)
    {
        //if (!string.IsNullOrEmpty(DDLProduct.SelectedValue))
        //{
        //    TBProduct.Text = DDLProduct.SelectedItem.Text;
        //}
        
    }

    /// <summary>
    /// 型号选择
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DDLModel_SelectedChanged(object sender, EventArgs e)
    {
    //    if (!string.IsNullOrEmpty(DDLModel.SelectedValue))
    //    {
    //        TBModel.Text = DDLModel.SelectedItem.Text;
    //    }
    }


    /// <summary>
    /// 读取是否有一个名称一样的易耗品，自动填写种类
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_UpdateExpendable_Click(object sender, EventArgs e)
    {
        Expendable bll = new Expendable();
        ExpendableInfo item = bll.GetTopItem(
            UserData.CurrentUserData.CompanyID,
            CurrentWarehouse.WareHouseID,
            TBProduct.Text.Trim(), TBModel.Text.Trim(), DDLUnit.SelectedValue);
        if (item != null)
        {
            try
            {
                DropDownList_ExpendableType.SelectedValue = item.CategoryID.ToString();
                DDLUnit.SelectedValue = item.Unit;
                TextBox_UnitPrice.Text = item.Price.ToString("0.##");
                Label_NoItem.Text = "";
            }
            catch {
                Label_NoItem.Text = "易耗品信息不全";
                
            }
        }
        else
        {
            Label_NoItem.Text = "找不到对应的易耗品";
        }
    }
}
