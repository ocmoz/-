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
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using WebUtility;
using WebUtility.Components;
using WebUtility.WebControls;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;
using System.Collections.Generic;
using FM2E.BLL.System;
using FM2E.BLL.BarCode;

public partial class Module_FM2E_DeviceManager_DeviceInfo_CurrentEuipementInfo_WarehouseEquipmentInfo_EditDevice : System.Web.UI.Page
{
    public string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 32, 0, DataType.Long);
    string companyid = (string)Common.sink("companyid", MethodType.Get, 50, 0, DataType.Str);
    public string action = (string)Common.sink("action", MethodType.Get, 20, 0, DataType.Str);
    protected string IsLocationShow = "block";
    protected string IsCompanyShow
    {
        get { return (string)ViewState["IsCompanyShow"]; }
        set { ViewState["IsCompanyShow"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);

        if (!IsPostBack)
        {

            Warehouse bllwh = new Warehouse();
            WarehouseInfo warehouse = bllwh.GetWarehouseByUserName(UserData.CurrentUserData.UserName);
            if (warehouse == null || warehouse.WareHouseID == null || warehouse.WareHouseID == string.Empty)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作警告", "本页面只允许仓管员进入", new WebException("本页面只允许仓管员进入"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                return;
            }
            
            CurrentWarehouse = warehouse;

            ViewState["CompanyID"] = UserData.CurrentUserData.CompanyID;
            InitialPage();
            FillData();
            ButtonBind();

        }
        else
        {
            if (ViewState["changetoadd"] != null && ViewState["changetoadd"].ToString() == "add")
                cmd = "add";
        }

    }


    private WarehouseInfo CurrentWarehouse
    {
        get { return (WarehouseInfo)ViewState["CurrentWarehouse"]; }
        set { ViewState["CurrentWarehouse"] = value; }
    }

    private void InitialPage()
    {
        try
        {
            //设备类型
            string[] eqtype = FM2E.BLL.System.ConfigItems.EqType;
            foreach (string s in eqtype)
            {
                ddlEqType.Items.Add(new ListItem(s, s));
            }

            this.CascadingDropDown2.Category += companyid;

            Section bll = new Section();
            IList<SectionInfo> sectionlist = bll.GetAllSection();
            foreach (SectionInfo item in sectionlist)
            {
                SectionName.Items.Add(new ListItem(item.SectionName, item.SectionID));
            }
            //系统
            EquipmentSystem systemBll = new EquipmentSystem();
            IList sysList = systemBll.GetAllSystem();
            foreach (EquipmentSystemInfo sys in sysList)
            {
                DropDownList_System.Items.Add(new ListItem(sys.SystemName, sys.SystemID));
            }

            //设备状态
            Status.Items.Clear();
            Status.Items.AddRange(EnumHelper.GetListItemsEx(typeof(EquipmentStatus), (int)EquipmentStatus.Normal, (int)EquipmentStatus.Unknown));

            AddTree(0, (TreeNode)null);
            TreeView1.ShowLines = true;


            if (UserData.CurrentUserData.IsParentCompany)
            {
                Warehouse bllwh = new Warehouse();
                WarehouseInfo warehouse = bllwh.GetWarehouseByUserName(UserData.CurrentUserData.UserName);
                if (warehouse == null || warehouse.WareHouseID == null || warehouse.WareHouseID == "")
                {

                }
                else
                {
                    CascadingDropDown3.SelectedValue = companyid;
                    IsCompanyShow = "none";
                    div_company.InnerText = UserData.CurrentUserData.CompanyName;
                    CascadingDropDown1.SelectedValue = "4";
                    CascadingDropDown2.SelectedValue = warehouse.WareHouseID;
                    IsLocationShow = "none";
                    div_LocationTag.InnerText = "仓库";
                    div_LocationName.InnerText = warehouse.Name;
                }
            }
            else
            {
                CascadingDropDown3.SelectedValue = companyid;
                IsCompanyShow = "none";
                div_company.InnerText = UserData.CurrentUserData.CompanyName;
                Warehouse bllwh = new Warehouse();
                WarehouseInfo warehouse = bllwh.GetWarehouseByUserName(UserData.CurrentUserData.UserName);
                if (warehouse == null || warehouse.WareHouseID == null || warehouse.WareHouseID == "")
                {

                }
                else
                {
                    CascadingDropDown1.SelectedValue = "4";
                    CascadingDropDown2.SelectedValue = warehouse.WareHouseID;
                    IsLocationShow = "none";
                    div_LocationTag.InnerText = "仓库";
                    div_LocationName.InnerText = warehouse.Name;
                }
            }


            //地址
            TextBox_Address.Value = CurrentWarehouse.AddressName;
            Hidden_AddressID.Value = CurrentWarehouse.AddressID.ToString();

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", ex.Message, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }


    }

    private void FillData()
    {
        //编辑或拆分设备
        //EquipmentNO.Text = "保存后自动生成条形码";
        if (cmd == "edit" || (cmd == "add" && action == "split"))
        {
            try
            {
                EquipmentInfoFacade item;
                if (Session["EquipmentInfo" + id] != null)
                {
                    item = (EquipmentInfoFacade)Session["EquipmentInfo" + id];
                }
                else
                {
                    Equipment bll = new Equipment();
                    item = bll.GetEquipment(id.ToString());
                }

                ViewState["EquipmentNO"] = item.EquipmentNO;
                try
                {
                    CascadingDropDown3.SelectedValue = item.CompanyID;
                }
                catch { }
                if (action == "split")
                {
                    //正在进行的操作是拆分设备
                    Price.ReadOnly = true;
                    Price.Text = "0";
                }
                else
                {
                    //正在进行的操作是修改设备信息
                    EquipmentNO.Text = item.EquipmentNO;
                    Name.Text = item.Name;
                    //SerialNum.Text = item.SerialNum;
                    ddlEqType.SelectedValue = item.SerialNum;
                    Model.Text = item.Model;
                    if (item.Status != 0)
                        Status.SelectedValue = item.Status.ToString();
                    Specification.Text = item.Specification;
                    if (item.CategoryID != 0)
                    {
                        CategoryName.Text = item.CategoryName;
                        CategoryID.Text = item.CategoryID.ToString();
                    }

                    Price.Text = item.Price.ToString("0.##");
                    Price.ReadOnly = false;
                    if (item.MaintenanceTimes != 0)
                        MaintenanceTimes.Text = item.MaintenanceTimes.ToString();
                    Remark.Text = item.Remark;

                    ViewState["PictureUrl"] = item.PhotoUrl;
                    FileUpload1.Visible = false;
                    if (item.PhotoUrl != "" && item.PhotoUrl != null)
                    {
                        ImageButton1.ImageUrl = item.PhotoUrl;
                        ImageButton2.ImageUrl = item.PhotoUrl;
                    }
                    else
                    {
                        ImageButton1.ImageUrl = "~/images/nopicture.gif";
                        ImageButton2.ImageUrl = "~/images/nopicture.gif";
                    }
                    Session["NeedUpdatePhoto"] = false;
                    ViewState["DepreciationMethod"] = item.DepreciationMethod.ToString();
                    ViewState["DepreciableLife"] = item.DepreciableLife.ToString();
                    ViewState["ResidualRate"] = item.ResidualRate.ToString();
                }
                try
                {
                    //if (item.SectionID != string.Empty)
                    SectionName.SelectedValue = item.SectionID;
                }
                catch { }
                try
                {
                    //if (item.LocationTag != string.Empty)
                    CascadingDropDown1.SelectedValue = item.LocationTag;
                }
                catch { }
                try
                {
                    //if (item.LocationID != string.Empty)
                    CascadingDropDown2.SelectedValue = item.LocationID;
                }
                catch { }

                try
                {
                    DropDownList_System.SelectedValue = item.SystemID;
                }
                catch
                {
                    EventMessage.EventWriteLog(Msg_Type.Error, item.SystemID + item.SystemName + "无法在下拉列表中找到");
                }

                PurchaseOrderID.Text = item.PurchaseOrderID;
                TextBox_DetailLocation.Text = item.DetailLocation;


                SupplierName.Value = item.SupplierName;
                if (item.SupplierID != 0)
                    SupplierID.Value = item.SupplierID.ToString();

                ProducerName.Value = item.ProducerName;
                if (item.ProducerID != 0)
                    ProducerID.Value = item.ProducerID.ToString();

                PurchaserName.Value = item.PurchaserName;

                Purchaser.Value = item.Purchaser;

                ResponsibilityName.Value = item.ResponsibilityName;

                Responsibility.Value = item.Responsibility;

                CheckerName.Value = item.CheckerName;

                Checker.Value = item.Checker;
                if (DateTime.Compare(item.PurchaseDate, DateTime.MinValue) != 0)
                    PurchaseDate.Text = item.PurchaseDate.ToString("yyyy-MM-dd");
                if (DateTime.Compare(item.ExamDate, DateTime.MinValue) != 0)
                    ExamDate.Text = item.ExamDate.ToString("yyyy-MM-dd");
                if (DateTime.Compare(item.OpeningDate, DateTime.MinValue) != 0)
                    OpeningDate.Text = item.OpeningDate.ToString("yyyy-MM-dd");
                if (DateTime.Compare(item.FileDate, DateTime.MinValue) != 0)
                    FileDate.Text = item.FileDate.ToString("yyyy-MM-dd");
                //if (DateTime.Compare(item.UpdateTime, DateTime.MinValue) != 0)
                //    UpdateTime.Text = item.UpdateTime.ToString("yyyy-MM-dd");

                Hidden_AddressID.Value = item.AddressID.ToString();
                TextBox_Address.Value = item.AddressName;

                switch (item.IsCancel)
                {
                    case false:
                        {
                            IsCancel.SelectedValue = "1";
                            break;
                        }
                    case true:
                        {
                            IsCancel.SelectedValue = "2";
                            break;
                        }
                    default: break;

                }

                if (DateTime.Compare(item.PurchaseDate, DateTime.MinValue) != 0 && DateTime.Compare(item.WarrantyDate, DateTime.MinValue) != 0)
                {
                    baoxiuqi.Text = Convert.ToString((item.WarrantyDate.Year - item.PurchaseDate.Year) * 12 + (item.WarrantyDate.Month - item.PurchaseDate.Month));
                }
                ServiceLife.Text = item.ServiceLife.ToString();
                if (action == "split")
                {
                    ImageButton1.Visible = false;
                    shoebig.Visible = false;
                }
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", ex.Message, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }
        }
        else if (cmd == "add")
        {
            try

            {
                try
                {
                    CascadingDropDown3.SelectedValue = UserData.CurrentUserData.CompanyID;
                }
                catch { }
                ImageButton1.Visible = false;
                shoebig.Visible = false;
                FileDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", ex.Message, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }
    /// <summary>
    /// 保存并添加同类设备
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddThisType(object sender, EventArgs e)
    {
        //先检查输入
        if (PurchaseDate.Text.Trim() == "")
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "生成条形码失败：采购日期不能为空", Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }

        try
        {
            Convert.ToDateTime(PurchaseDate.Text.Trim());
        }
        catch
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "生成条形码失败：采购日期格式不正确", Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }

        if (cmd == "add" || cmd == "edit")
        {
            bool bSuccess = false;
            EquipmentInfo item = new EquipmentInfo();
            if ("edit" == cmd)
                item.EquipmentID = id;

            //item.EquipmentNO = EquipmentNO.Text;
            //生成设备条形码失败
            DateTime purchaseDate = Convert.ToDateTime(PurchaseDate.Text.Trim());
            //string barCode = BarCode.GenerateBarCode((string)ViewState["CompanyID"], purchaseDate, cbComponent.Checked ? true : false);
            //item.EquipmentNO = barCode;

            item.EquipmentNO = EquipmentNO.Text.Trim();

            item.DetailLocation = TextBox_DetailLocation.Text;
            item.Name = Name.Text;
            item.CompanyID = DDLCompany.SelectedValue;
            item.SectionID = SectionName.SelectedValue;
            item.LocationTag = LocationTag.SelectedValue;
            item.LocationID = LocationID.SelectedValue;
            item.SystemID = DropDownList_System.SelectedValue;
            item.PurchaseOrderID = PurchaseOrderID.Text;
            item.SerialNum = ddlEqType.SelectedValue;
            item.Model = Model.Text;
            item.Specification = Specification.Text;
            if (Status.SelectedValue != "0")
                item.Status = (EquipmentStatus)Convert.ToInt64(Status.SelectedValue);
            if (SupplierID.Value != string.Empty)
                item.SupplierID = Convert.ToInt64(SupplierID.Value);
            if (ProducerID.Value != string.Empty)
                item.ProducerID = Convert.ToInt64(ProducerID.Value);
            //if (Purchaser.Value != string.Empty)
            item.Purchaser = Purchaser.Value;
            if (Responsibility.Value != string.Empty)
            {
                item.Responsibility = Responsibility.Value;

                User userbll = new User();
                item.ResponsibilityName = userbll.GetUser(Responsibility.Value).PersonName;
            }
            else
            {
                item.Responsibility = "";
                item.ResponsibilityName = "";
            }
            //if (Checker.Value != string.Empty)
            item.Checker = Checker.Value;
            if (PurchaseDate.Text != string.Empty)
                item.PurchaseDate = Convert.ToDateTime(PurchaseDate.Text);
            if (ExamDate.Text != string.Empty)
                item.ExamDate = Convert.ToDateTime(ExamDate.Text);
            if (OpeningDate.Text != string.Empty)
                item.OpeningDate = Convert.ToDateTime(OpeningDate.Text);
            if (FileDate.Text != string.Empty)
                item.FileDate = Convert.ToDateTime(FileDate.Text);
            item.UpdateTime = DateTime.Now;
            switch (IsCancel.SelectedValue)
            {
                case "1":
                    {
                        item.IsCancel = false;
                        break;
                    }
                case "2":
                    {
                        item.IsCancel = true;
                        break;
                    }
                default: break;

            }
            if (baoxiuqi.Text != string.Empty)
                item.WarrantyDate = item.PurchaseDate.AddMonths(Convert.ToInt32(baoxiuqi.Text));
            if (Price.Text != string.Empty)
                item.Price = Convert.ToDecimal(Price.Text);
            if (MaintenanceTimes.Text != string.Empty)
                item.MaintenanceTimes = Convert.ToInt64(MaintenanceTimes.Text);
            item.Remark = Remark.Text;
            if (ServiceLife.Text != string.Empty)
                item.ServiceLife = Convert.ToInt64(ServiceLife.Text);
            //item.PhotoUrl = (string)ViewState["PictureUrl"];
            if (CategoryName.Text != string.Empty)
            {
                item.CategoryID = Convert.ToInt64(CategoryID.Text);
                item.CategoryName = CategoryName.Text;
                if (ViewState["DepreciationMethod"] != null && ViewState["DepreciationMethod"].ToString() != string.Empty)
                    item.DepreciationMethod = Convert.ToInt64(ViewState["DepreciationMethod"]);
                if (ViewState["DepreciableLife"] != null && ViewState["DepreciableLife"].ToString() != string.Empty)
                    item.DepreciableLife = Convert.ToInt64(ViewState["DepreciableLife"]);
                if (cmd == "add")
                {
                    if (ViewState["ResidualRate"] != null && ViewState["ResidualRate"].ToString() != string.Empty)
                        item.ResidualRate = Convert.ToDecimal(ViewState["ResidualRate"]) / 100;
                }
                else
                    if (ViewState["ResidualRate"] != null && ViewState["ResidualRate"].ToString() != string.Empty)
                        item.ResidualRate = Convert.ToDecimal(ViewState["ResidualRate"]);
            }
            else
                item.CategoryName = "";

            long addressid = 0;
            long.TryParse(Hidden_AddressID.Value, out addressid);

            item.AddressID = addressid;
            item.AssertNumber = TextBox_AssertNumber.Text.Trim();

            string errorMsg = "";
            bool isSuccess = false;

            //对图片是否上传更新的选择处理
            if ((FileUpload1.FileName != null) && (FileUpload1.FileName != ""))
            {
                string photoUrl = UploadPhoto(ref isSuccess, ref errorMsg);
                if (photoUrl != "")
                {
                    if ("edit" == cmd)
                        FileUpLoadCommon.DeleteFile(string.Format("{0}", item.PhotoUrl));
                    item.PhotoUrl = SystemConfig.Instance.UploadPath + "EquipmentPic/" + photoUrl;
                }
                else
                    item.PhotoUrl = photoUrl;
            }
            else
            {
                if (ViewState["PictureUrl"] != null && ViewState["PictureUrl"].ToString() != string.Empty)
                    item.PhotoUrl = ViewState["PictureUrl"].ToString();
                else
                    item.PhotoUrl = "";
                isSuccess = true;
            }
            if (!isSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "上传图片失败："+errorMsg, new WebException(errorMsg), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }
            if ("edit" == cmd)
            {
                try
                {

                    Equipment bll = new Equipment();
                    bll.UpdateEquipment(item);

                    bSuccess = true;
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改设备信息失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                }

                if (bSuccess)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "addthistype", string.Format("alert('修改设备信息成功，条形码为{0}');", item.EquipmentNO), true);
                }
            }
            else if ("add" == cmd)
            {
                try
                {

                    Equipment bll = new Equipment();
                    bll.InsertEquipment(item);

                    bSuccess = true;
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加设备失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                }

                if (bSuccess)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "addthistype", string.Format("alert('添加设备信息成功，条形码为{0}');", item.EquipmentNO), true);
                }

            }
            if (cmd == "edit")
            {
                EquipmentNO.Text = "";
                //SerialNum.Text = "";
                ViewState.Remove("PictureUrl");
                ViewState["changetoadd"] = "add";
            }
            else if (cmd == "add")
            {

                EquipmentNO.Text = "";
                //SerialNum.Text = "";
                ViewState.Remove("PictureUrl");
            }
        }
    }

    private void ButtonBind()
    {
        HeadMenuButtonItem button0 = HeadMenuWebControls1.ButtonList[1];
        button0.ButtonUrl += "?companyid=" + companyid;
        if (cmd == "add")
        {

            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：设备信息添加";
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = true;
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = true;
            if (action == "split")
            {
                HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
                HeadMenuWebControls1.ButtonList[2].ButtonVisible = true;
                addthistype.Visible = false;
            }
            else
            {
                HeadMenuWebControls1.ButtonList[2].ButtonVisible = false;
                addthistype.Visible = true;
            }
            //TabPanel1.HeaderText = "添加设备";
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：设备信息修改";
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = true;
            HeadMenuWebControls1.ButtonList[2].ButtonVisible = true;
            addthistype.Visible = false;
            //TabPanel1.HeaderText = "修改设备信息";
        }
    }
    /// <summary>
    /// 点击确定按钮触发添加或修改的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void Button1_Click(object sender, EventArgs e)
    {
        //先检查输入
        if (PurchaseDate.Text.Trim() == "")
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "生成条形码失败：采购日期不能为空", Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }

        try
        {
            Convert.ToDateTime(PurchaseDate.Text.Trim());
        }
        catch
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "生成条形码失败：采购日期格式不正确", Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }

        bool bSuccess = false;
        EquipmentInfo item = new EquipmentInfo();
        if ("edit" == cmd || "add" == cmd)
        {
            if ("edit" == cmd)
                item.EquipmentID = id;

            //生成设备条形码
            DateTime purchaseDate = Convert.ToDateTime(PurchaseDate.Text.Trim());
            
            item.DetailLocation = TextBox_DetailLocation.Text;
            item.Name = Name.Text;
            item.CompanyID = DDLCompany.SelectedValue;
            item.SectionID = SectionName.SelectedValue;
            item.LocationTag = LocationTag.SelectedValue;
            item.LocationID = LocationID.SelectedValue;
            item.SystemID = DropDownList_System.SelectedValue;
            item.PurchaseOrderID = PurchaseOrderID.Text;
            item.SerialNum = ddlEqType.SelectedValue;
            item.Model = Model.Text;
            item.Specification = Specification.Text;
            if (Status.SelectedValue != "0")
                item.Status = (EquipmentStatus)Convert.ToInt64(Status.SelectedValue);
            if (SupplierID.Value != string.Empty)
                item.SupplierID = Convert.ToInt64(SupplierID.Value);
            if (ProducerID.Value != string.Empty)
                item.ProducerID = Convert.ToInt64(ProducerID.Value);
            //if (Purchaser.Value != string.Empty)
            item.Purchaser = Purchaser.Value;
            if (Responsibility.Value != string.Empty)
            {
                item.Responsibility = Responsibility.Value;

                User userbll = new User();
                item.ResponsibilityName = userbll.GetUser(Responsibility.Value).PersonName;
            }
            else
            {
                item.Responsibility = "";
                item.ResponsibilityName = "";
            }
            //if (Checker.Value != string.Empty)
            item.Checker = Checker.Value;
            if (PurchaseDate.Text != string.Empty)
                item.PurchaseDate = Convert.ToDateTime(PurchaseDate.Text);
            if (ExamDate.Text != string.Empty)
                item.ExamDate = Convert.ToDateTime(ExamDate.Text);
            if (OpeningDate.Text != string.Empty)
                item.OpeningDate = Convert.ToDateTime(OpeningDate.Text);
            if (FileDate.Text != string.Empty)
                item.FileDate = Convert.ToDateTime(FileDate.Text);
            item.UpdateTime = DateTime.Now;
            switch (IsCancel.SelectedValue)
            {
                case "1":
                    {
                        item.IsCancel = false;
                        break;
                    }
                case "2":
                    {
                        item.IsCancel = true;
                        break;
                    }
                default: break;

            }
            if (baoxiuqi.Text != string.Empty)
                item.WarrantyDate = item.PurchaseDate.AddMonths(Convert.ToInt32(baoxiuqi.Text));
            if (Price.Text != string.Empty)
                item.Price = Convert.ToDecimal(Price.Text);
            if (MaintenanceTimes.Text != string.Empty)
                item.MaintenanceTimes = Convert.ToInt64(MaintenanceTimes.Text);
            item.Remark = Remark.Text;
            if (ServiceLife.Text != string.Empty)
                item.ServiceLife = Convert.ToInt64(ServiceLife.Text);
            //item.PhotoUrl = (string)ViewState["PictureUrl"];
            if (CategoryName.Text != string.Empty)
            {
                item.CategoryID = Convert.ToInt64(CategoryID.Text);

                item.CategoryName = CategoryName.Text;
                if (ViewState["DepreciationMethod"] != null && ViewState["DepreciationMethod"].ToString() != string.Empty)
                    item.DepreciationMethod = Convert.ToInt64(ViewState["DepreciationMethod"]);
                if (ViewState["DepreciableLife"] != null && ViewState["DepreciableLife"].ToString() != string.Empty)
                    item.DepreciableLife = Convert.ToInt64(ViewState["DepreciableLife"]);
                if (cmd == "add")
                {
                    if (ViewState["ResidualRate"] != null && ViewState["ResidualRate"].ToString() != string.Empty)
                        item.ResidualRate = Convert.ToDecimal(ViewState["ResidualRate"]) / 100;
                }
                else
                    if (ViewState["ResidualRate"] != null && ViewState["ResidualRate"].ToString() != string.Empty)
                        item.ResidualRate = Convert.ToDecimal(ViewState["ResidualRate"]);
            }
            else
                item.CategoryName = "";

            long addressid = 0;
            long.TryParse(Hidden_AddressID.Value, out addressid);

            item.AddressID = addressid;
            item.AddressName = TextBox_Address.Value.Trim();
            item.AssertNumber = TextBox_AssertNumber.Text.Trim();
          

            string errorMsg = "";
            bool isSuccess = false;

            //对图片是否上传更新的选择处理
            if ((FileUpload1.FileName != null) && (FileUpload1.FileName != ""))
            {
                string photoUrl = UploadPhoto(ref isSuccess, ref errorMsg);
                if (photoUrl != "")
                {
                    if ("edit" == cmd)
                        FileUpLoadCommon.DeleteFile(string.Format("{0}", item.PhotoUrl));
                    item.PhotoUrl = SystemConfig.Instance.UploadPath + "EquipmentPic/" + photoUrl;
                }
                else
                    item.PhotoUrl = photoUrl;
            }
            else
            {
                if (ViewState["PictureUrl"] != null && ViewState["PictureUrl"].ToString() != string.Empty)
                    item.PhotoUrl = ViewState["PictureUrl"].ToString();
                else
                    item.PhotoUrl = "";
                isSuccess = true;
            }
            if (!isSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "上传图片失败："+errorMsg, new WebException(errorMsg), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }
            if ("edit" == cmd)
            {
                try
                {
                    item.EquipmentNO = EquipmentNO.Text;
                    Equipment bll = new Equipment();
                    bll.UpdateEquipment(item);

                    bSuccess = true;
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改设备信息失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                }

                if (bSuccess)
                {
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", string.Format("修改设备信息成功，条形码为{0}！", item.EquipmentNO), Icon_Type.OK, true, Common.GetHomeBaseUrl("DeviceInfo.aspx"), UrlType.Href, "");
                }
            }
            else if ("add" == cmd)
            {
                try
                {

                    Equipment bll = new Equipment();
                    if (action == "split")
                        item.EquipmentNO = BarCode.GenerateBarCode((string)ViewState["EquipmentNO"]);  //对于拆分操作，条形码自动生成
                    else
                    {
                        //string barCode = BarCode.GenerateBarCode((string)ViewState["CompanyID"], purchaseDate, cbComponent.Checked ? true : false);
                        //item.EquipmentNO = barCode;
                        item.EquipmentNO = EquipmentNO.Text.Trim();
                    }
                    bll.InsertEquipment(item);

                    bSuccess = true;
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加设备失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                }

                if (bSuccess)
                {
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", string.Format("添加设备信息成功，条形码为{0}！", item.EquipmentNO), Icon_Type.OK, true, Common.GetHomeBaseUrl("DeviceInfo.aspx"), UrlType.Href, "");
                }

            }
        }
    }
    /// <summary>
    /// 选择种类事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        CategoryID.Text = this.TreeView1.SelectedNode.Value;
        CategoryName.Text = this.TreeView1.SelectedNode.Text;
        Category bll = new Category();
        CategoryInfo categoryinfo = bll.GetCategory(Convert.ToInt32(TreeView1.SelectedNode.Value));
        ViewState["DepreciationMethod"] = categoryinfo.DepreciationMethod;
        ViewState["DepreciableLife"] = categoryinfo.DepreciableLife;
        ViewState["ResidualRate"] = categoryinfo.ResidualRate;
        PopupControlExtender1.Commit(CategoryName.Text);
        PopupControlExtender2.Commit(CategoryID.Text);
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "causeValidatescript", "causeValidate=true;", true);
    }

    ///// <summary>
    ///// 选择所属系统事件
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
    //{
    //    SystemName.Text = this.TreeView2.SelectedNode.Text;
    //    SystemID.Text = this.TreeView2.SelectedValue;
    //    PopupControlExtender3.Commit(SystemName.Text);
    //    PopupControlExtender5.Commit(SystemID.Text);
    //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "causeValidatescript", "causeValidate=true;", true); 
    //}

    private string UploadPhoto(ref bool isSuccess, ref string errorMsg)
    {

        FileUpLoadCommon fc = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + "EquipmentPic/", false);
        isSuccess = fc.SaveFile(FileUpload1.PostedFile, true, false);
        if (!isSuccess)
            errorMsg = fc.ErrorMsg;
        return fc.NewFileName;

    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {

        ImageButton1.Visible = false;
        FileUpload1.Visible = true;
        ButtonCancel.Visible = true;
        shoebig.Visible = false;
        //Session["NeedUpdatePhoto"] = true;

    }

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {

        ImageButton1.Visible = true;
        FileUpload1.Visible = false;
        ButtonCancel.Visible = false;
        shoebig.Visible = true;

        //Session["NeedUpdatePhoto"] = false;
    }
    ///// <summary>
    ///// 初始化系统弹出树
    ///// </summary>
    ///// <param name="ParentID"></param>
    ///// <param name="pNode"></param>
    //public void AddTree2(long ParentID, TreeNode pNode)
    //{
    //    EquipmentSystem bll = new EquipmentSystem();
    //    IList rootsystem = bll.GetAllSystem();
    //    foreach (EquipmentSystemInfo item in rootsystem)
    //    {
    //        TreeNode Node = new TreeNode();
    //        Node.Text = item.SystemName;
    //        Node.Value = item.SystemID;
    //        TreeView2.Nodes.Add(Node);

    //        Node.Expanded = true;
    //    }
    //}
    /// <summary>
    /// 初始化种类弹出树
    /// </summary>
    /// <param name="ParentID"></param>
    /// <param name="pNode"></param>
    public void AddTree(long ParentID, TreeNode pNode)
    {
        CategorysearchInfo categoryinfo = new CategorysearchInfo();
        categoryinfo.Level = 1;
        Category bll = new Category();
        //QueryParam qp = bll.GenerateSearchTerm(categoryinfo);
        //qp.PageSize = 500;
        //int recordcount = 0;
        IList nodelist = (List<CategoryInfo>)bll.Search(categoryinfo);
        foreach (CategoryInfo item in nodelist)
        {
            TreeNode Node = new TreeNode();
            Node.Text = item.CategoryName;
            Node.Value = item.CategoryID.ToString();
            TreeView1.Nodes.Add(Node);
            Node.PopulateOnDemand = true;
            Node.Expanded = false;
        }

    }
    /// <summary>
    /// 树的展开事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void TreeView1_OnTreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {
        if (e.Node.ChildNodes.Count == 0)
        {
            CategorysearchInfo categoryinfo = new CategorysearchInfo();
            categoryinfo.ParentID = Convert.ToInt64(e.Node.Value);
            Category bll = new Category();
            //QueryParam qp = bll.GenerateSearchTerm(categoryinfo);
            //qp.PageSize = 500;
            //int recordcount = 0;
            IList nodelist = (List<CategoryInfo>)bll.Search(categoryinfo);
            foreach (CategoryInfo item in nodelist)
            {
                TreeNode Node = new TreeNode();
                Node.Text = item.CategoryName;
                Node.Value = item.CategoryID.ToString();
                e.Node.ChildNodes.Add(Node);
                Node.PopulateOnDemand = true;
                Node.Expanded = false;
            }
        }
    }
    ///// <summary>
    ///// 生成设备条形码
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void btGenerateBarCode_Click(object sender, EventArgs e)
    //{
    //    //先检查输入
    //    if (PurchaseDate.Text.Trim() == "")
    //    {
    //        EventMessage.MessageBox(Msg_Type.Error, "操作失败", "生成条形码失败：采购日期不能为空", Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
    //    }

    //    try
    //    {
    //        Convert.ToDateTime(PurchaseDate.Text.Trim());
    //    }
    //    catch
    //    {
    //        EventMessage.MessageBox(Msg_Type.Error, "操作失败", "生成条形码失败：采购日期格式不正确", Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
    //    }
    //    try
    //    {
    //        DateTime purchaseDate = Convert.ToDateTime(PurchaseDate.Text.Trim());
    //        string barCode = BarCode.GenerateBarCode((string)ViewState["CompanyID"], purchaseDate, cbComponent.Checked ? true : false);
    //        EquipmentNO.Text = barCode;
    //    }
    //    catch (Exception ex)
    //    {
    //        EventMessage.MessageBox(Msg_Type.Error, "操作失败", "生成条形码失败：" + ex.Message, ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
    //    }
    //}
}
