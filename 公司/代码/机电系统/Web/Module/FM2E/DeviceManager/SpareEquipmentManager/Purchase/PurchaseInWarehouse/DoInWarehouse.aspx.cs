using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FM2E.Model.Utils;
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using FM2E.BLL.System;
using FM2E.Model.System;
using FM2E.Model.Basic;
using FM2E.BLL.Basic;
using FM2E.BLL.BarCode;

using WebUtility;
using WebUtility.Components;

using FM2E.WorkflowLayer;
using System.Data;
using FM2E.Model.Exceptions;
using System.Data.OleDb;


public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseInWarehouse_DoInWarehouse : System.Web.UI.Page
{
    /// <summary>
    /// 用户业务逻辑处理类
    /// </summary>
    User userBll = new User();

    /// <summary>
    /// 采购管理业务逻辑处理类对象
    /// </summary>
    Purchase purchaseBll = new Purchase();
    /// <summary>
    /// 消耗品业务逻辑处理类对象
    /// </summary>
    Expendable expendableBll = new Expendable();
    /// <summary>
    /// 仓库管理BLL
    /// </summary>
    Warehouse warehouseBll = new Warehouse();
    /// <summary>
    /// 条形码
    /// </summary>
    BarCode barcodeBll = new BarCode();
    InEquipments inEqBll = new InEquipments();

    CheckAcceptance checkacceptanceBll = new CheckAcceptance();
    ExpendableInOut exInOutBll = new ExpendableInOut();
    /// <summary>
    /// 设备
    /// </summary>
    Equipment equipmentBll = new Equipment();
    string sessionName = "Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseInWarehouse_DoInWarehouse";

    /// <summary>
    /// 入库单类型，ca不经过申请采购的，p经过申请采购的
    /// </summary>
    string type = (string)Common.sink("type", MethodType.Get, 0, 0, DataType.Str);

    /// <summary>
    /// 明细条目ID参数
    /// </summary>
    long id = (long)Common.sink("id", MethodType.Get, 0, 0, DataType.Long);

    short itemid = Convert.ToInt16(Common.sink("itemid", MethodType.Get, 0, 0, DataType.Int));

   
    /// <summary>
    /// 仓库ID
    /// </summary>
     protected string warehouseid = (string)Common.sink("warehouse", MethodType.Get, 0, 0, DataType.Str);
 long recordid = (long)Common.sink("recordid", MethodType.Get, 0, 0, DataType.Long);
    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RadioButton_Device.Attributes.Add("onclick", "javascript:ShowHideObject('tr_barcode',true);ShowHideObject('tr_component',true);ShowHideObject('tr_expandabletype',false);");
            RadioButton_Expendable.Attributes.Add("onclick", "javascript:ShowHideObject('tr_barcode',false);ShowHideObject('tr_component',false);ShowHideObject('tr_expandabletype',true);");

            //仓库名称 
            WarehouseInfo warehouse = warehouseBll.GetWarehouse(warehouseid);
            Hidden_WarehouseName.Value = warehouse.Name;
            //Label_LocationName.Text = warehouse.Name;
            
            //默认的地址是仓库
            TextBox_Address.Value = warehouse.AddressName;
            Hidden_AddressID.Value = warehouse.AddressID.ToString();

            //系统
            EquipmentSystem systemBll = new EquipmentSystem();
            DropDownList_System.Items.Clear();
            DropDownList_System.Items.AddRange(systemBll.GenerateListItemCollectionWithBlank());

           

            //种类树
            AddTree(0, null);

            AddTree2(0, null);

            InitPage();

            FillData();
        }
    }


    private void InitPage()
    { 
        
    }

    /// <summary>
    /// 根据输入条件查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Next_Click(object sender, EventArgs e)
    {
        switch (type)
        {
            case "p":
                PurchaseRecordInfo item = (PurchaseRecordInfo)Session[sessionName];
                if (item == null)
                {
                    item = purchaseBll.GetPurchaseRecordInfo(recordid);
                    Session[sessionName] = item;
                }
                if (RadioButton_Expendable.Checked)//消耗品
                {
                    decimal count = expendableBll.ExpendableInWarehouseFromPurchase(UserData.CurrentUserData.CompanyID, warehouseid, item.ProductName, item.Model, item.Unit, item.AcceptanceCount,item.PurchaseUnitPrice,long.Parse(Hidden_ExpendableTypeID.Value.Trim()));
                    item.Status = PurchaseRecordStatus.INWAREHOUSEFINISH;
                    purchaseBll.SavePurchaseRecord(item);
                    EventMessage.MessageBox(Msg_Type.Info, "消耗品入库成功", "数量为 " + item.AcceptanceCount.ToString("#,0.##") + "单位为 " + item.Unit + " 的消耗品 " + item.ProductName + "[" + item.Model + "] 入库成功<br/>" +
                        "仓库 " + Hidden_WarehouseName.Value + " 有该类产品库存为 " + count.ToString("#,0.##") + item.Unit,
                        Icon_Type.OK, "javascript:window.parent.hidePopWin(true);", UrlType.JavaScript);
                }
                break;
            case "ca":

                CheckAcceptanceDetailInfo caitem = (CheckAcceptanceDetailInfo)Session[sessionName];
                if (caitem == null)
                {
                    caitem = checkacceptanceBll.GetCheckAcceptanceDetailInfo(id, itemid);
                    Session[sessionName] = caitem;
                }
                if (RadioButton_Expendable.Checked)//消耗品
                {
                    decimal count = expendableBll.ExpendableInWarehouseFromPurchase(UserData.CurrentUserData.CompanyID, warehouseid, caitem.ProductName, caitem.Model, caitem.Unit, caitem.AcceptanceCount, caitem.PurchaseUnitPrice, long.Parse(Hidden_ExpendableTypeID.Value.Trim()));
                    caitem.Status = PurchaseRecordStatus.INWAREHOUSEFINISH;
                    checkacceptanceBll.UpdateCheckAcceptanceDetail(caitem);
                    //  [9/5/2013 Genland]
                    if (caitem.AcceptanceCount != 0)
                    {
                        ExpendableInOutRecordInfo exItem = new ExpendableInOutRecordInfo();
                        exItem.CompanyID = UserData.CurrentUserData.CompanyID;
                        exItem.Type = ExpendableInOutRecordType.In;
                        exItem.WarehouseID = warehouseid;
                        exItem.Name=caitem.ProductName;
                        exItem.Model=caitem.Model;
                        exItem.Price=caitem.PurchaseUnitPrice;
                        exItem.Amount=caitem.AcceptanceCount;
                        exItem.InOutTime=DateTime.Now;
                        exItem.Remark = caitem.SheetID;
                        exItem.Unit=caitem.Unit;
                        exItem.WarehouseKeeper=UserData.CurrentUserData.UserName;
                        exItem.WarehouseKeeperName=UserData.CurrentUserData.PersonName;
                        exItem.CategoryID=long.Parse(Hidden_ExpendableTypeID.Value.Trim());
                        exInOutBll.InsertRecord(exItem);
                    }
                    //  [9/5/2013 Genland]
                    EventMessage.MessageBox(Msg_Type.Info, "消耗品入库成功", "数量为 " + caitem.AcceptanceCount.ToString("#,0.##") + "单位为 " + caitem.Unit + " 的消耗品 " + caitem.ProductName + "[" + caitem.Model + "] 入库成功<br/>" +
                        "仓库 " + Hidden_WarehouseName.Value + " 有该类产品库存为 " + count.ToString("#,0.##") + caitem.Unit,
                        Icon_Type.OK, "javascript:window.parent.hidePopWin(true);", UrlType.JavaScript);
                }

                break;

            default:
                break;
        }
        if (RadioButton_Device.Checked)//设备
        {
            TabContainer1.ActiveTabIndex = 1;
            Button_PreStep.Enabled = true;
            Button_DeviceInWarehouse.Enabled = true;
        }
    }

    /// <summary>
    /// 往数据表中填充
    /// </summary>
    private void FillData()
    {
        //易耗品种类
        ExpendableInfo searchInfo = new ExpendableInfo();

        switch (type)
        {
            case "p":
                PurchaseRecordInfo item = purchaseBll.GetPurchaseRecordInfo(recordid);
                Session[sessionName] = item;
                Label_CheckRemark.Text = item.AcceptanceRemark;
                Label_Remark2.Text = item.AcceptanceRemark;
                Label_Count2.Text = Label_Count.Text = item.AcceptanceCount.ToString("#,0.##");

                Label_Model2.Text = Label_Model.Text = item.Model;
                Label_ProductName2.Text = Label_ProductName.Text = item.ProductName;
                Label_Unit2.Text = Label_Unit.Text = item.Unit;

                TextBox_BarCodeCount.Text = item.AcceptanceCount.ToString("0");
                Label_Producer.Text = item.Producer;
                Label_Supplier.Text = item.Supplier;
                Label_ResponsiblityName.Text = "";
                Label_Purchaser.Text = item.PurchaserName;
                Hidden_Responsiblity.Value = "";
                Hidden_Purchaser.Value = item.Purchaser;
                Label_Technician.Text = item.TechnicianName;
                Label_WarehouserKeeper.Text = item.WarehouseKeeperName;
                Hidden_WarehouseKeeper.Value = item.Checker_Warehouse;
                Hidden_Techincian.Value = item.Checker_Technician;
                Label_PurchaseDate.Text = item.PurchaseTime.ToString("yyyy-MM-dd");
                Label_AcceptanceDate.Text = item.AcceptedTime.ToString("yyyy-MM-dd");
                Label_PurchaseOrderID.Text = item.PurchaseOrderID + "-" + item.SubOrderIndex;
                Label_Price.Text = item.PurchaseUnitPrice.ToString("#,0.##");
                //默认系统
                try
                {
                    DropDownList_System.SelectedValue = item.SystemID;
                }
                catch { }

               
                searchInfo.Model = item.Model;
                searchInfo.Name = item.ProductName;
                searchInfo.Unit = item.Unit;
                
               

                break;
            case "ca":
                CheckAcceptanceDetailInfo itemca = checkacceptanceBll.GetCheckAcceptanceDetailInfo(id, itemid);
                Session[sessionName] = itemca;
                Label_Remark2.Text = Label_CheckRemark.Text = itemca.AcceptanceRemark;
                Label_Count2.Text = Label_Count.Text = itemca.AcceptanceCount.ToString("#,0.##");
                Label_Model2.Text = Label_Model.Text = itemca.Model;
                Label_ProductName2.Text = Label_ProductName.Text = itemca.ProductName;
                Label_Unit2.Text = Label_Unit.Text = itemca.Unit;
                TextBox_BarCodeCount.Text = itemca.AcceptanceCount.ToString("0");
                Label_Producer.Text = itemca.Producer;
                Label_Supplier.Text = itemca.Supplier;
                Label_ResponsiblityName.Text = "";
                Hidden_Responsiblity.Value = "";
                Label_Purchaser.Text = itemca.PurchaserName;
                Hidden_Purchaser.Value = itemca.Purchaser;
                Label_Technician.Text = itemca.TechnicianName;
                Hidden_Techincian.Value = itemca.Checker_Technician;
                Label_WarehouserKeeper.Text = itemca.WarehouseKeeperName;
                Hidden_WarehouseKeeper.Value = itemca.Checker_Warehouse;
                Label_PurchaseDate.Text = itemca.PurchaseTime.ToString("yyyy-MM-dd");
                Label_AcceptanceDate.Text = itemca.AcceptedTime.ToString("yyyy-MM-dd");
                //Label_PurchaseOrderID.Text = itemca.SheetID;
                Label_Price.Text = itemca.PurchaseUnitPrice.ToString("#,0.##");
                try
                {
                    DropDownList_System.SelectedValue = itemca.SystemID;
                }
                catch { }

                searchInfo.Model = itemca.Model;
                searchInfo.Name = itemca.ProductName;
                searchInfo.Unit = itemca.Unit;

                break;
            default:
                break;
        }

        //易耗品种类
        searchInfo.WarehouseID = warehouseid;
        QueryParam qp = expendableBll.GenerateSearchTerm(searchInfo);
        qp.PageIndex = 1;
        int count = 0;
        IList list = expendableBll.GetList(qp, out count);
        if (count > 0)
        {
            ExpendableInfo exinfo = list[0] as ExpendableInfo;
            Hidden_ExpendableTypeID.Value = exinfo.CategoryID.ToString();
            TextBox_ExpendableType.Text = exinfo.CategoryName;
        }
    }


    /// <summary>
    /// 上一步
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_PreStep_Click(object sender, EventArgs e)
    {
        TabContainer1.ActiveTabIndex = 0;
        Button_PreStep.Enabled = false;
        Button_DeviceInWarehouse.Enabled = false;
    }

    /// <summary>
    /// 入库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_DeviceInWarehouse_Click(object sender, EventArgs e)
    {
        

        int count = 0;
        if (!int.TryParse(TextBox_BarCodeCount.Text.Trim(), out count))
        {
            EventMessage.MessageBox(Msg_Type.Error, "数据格式错误", "条形码数量不为整数： " + TextBox_BarCodeCount.Text.Trim(),
                Icon_Type.Error, "javascript:window.history.go(-1);", UrlType.JavaScript);
            return;
        }

        long servicelife = 0;
        if (!long.TryParse(TextBox_ServiceLife.Text.Trim(), out servicelife))
        {
            //EventMessage.MessageBox(Msg_Type.Error, "数据格式错误", "使用年限不为整数： " + TextBox_ServiceLife.Text.Trim(),
            //    Icon_Type.Error, "javascript:window.history.go(-1);", UrlType.JavaScript);
            //return;
        }

        int warranty = 0;

        if (!int.TryParse(TextBox_Warranty.Text.Trim(), out warranty))
        {
            //EventMessage.MessageBox(Msg_Type.Error, "数据格式错误", "保修时长不为整数： " + TextBox_Warranty.Text.Trim(),
            //    Icon_Type.Error, "javascript:window.history.go(-1);", UrlType.JavaScript);
            //return;
        }

        long catagoryid = 0;
        if (!long.TryParse(Hidden_CategoryID.Value, out catagoryid))
        {
            EventMessage.MessageBox(Msg_Type.Error, "数据格式错误", "未选定设备种类",
                Icon_Type.Error, "javascript:window.history.go(-1);", UrlType.JavaScript);
            return;
        }
        string systemid = DropDownList_System.SelectedValue;
        //string sectionid = DropDownList_Section.SelectedValue;

        //**************************2013-1-23导入设备条形码 *************************************************
        string UPLOADFOLDER = "ImportEquipmentBarCode/";
        IList EqBarCodeList = null;
        if (ImportEqBar.HasFile)
        {

            //附件处理
            FileUpLoadCommon fuc = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + UPLOADFOLDER, false);
            string file = "";
            if (ImportEqBar.HasFile)
            {
                if (fuc.SaveFile(ImportEqBar, false))
                {
                    file = SystemConfig.Instance.UploadPath + UPLOADFOLDER + "/" + fuc.NewFileName;
                }
                else
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "上传失败", new WebException(fuc.ErrorMsg), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                    return;
                }
            }

            file = Server.MapPath(file);
            try
            {
                DataSet ds = ImportXlsToData(file);
                EqBarCodeList = AddDataToObject(ds);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "导入失败" + ex.Message, Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
                return;
            }

            
        }

    





        //**************************2013-1-23导入设备条形码 *************************************************




        EquipmentInfo equipmentitem = new EquipmentInfo();

        equipmentitem.CategoryID = catagoryid;
        equipmentitem.CategoryName = TextBox_CategoryName.Text;
        equipmentitem.Checker = Label_Technician.Text;
        equipmentitem.CheckerName = Label_Technician.Text;
        equipmentitem.Count = 1;//2013
        equipmentitem.DepreciableLife = long.Parse(Hidden_DepreciableLife.Value);
        equipmentitem.DepreciationMethod = int.Parse(Hidden_DepreciationMethod.Value);
        equipmentitem.EquipmentNO = "";

        //equipmentitem.LocationTag = DropDownList_LocationType.SelectedValue;
        //equipmentitem.LocationID = DropDownList_Location.SelectedValue;

        long addressid = 0;
        if (!string.IsNullOrEmpty(TextBox_Address.Value.Trim()))
        {
            long.TryParse(Hidden_AddressID.Value, out addressid);
        }
        equipmentitem.AddressID = addressid;
        equipmentitem.AssertNumber = TextBox_AssertNumber.Text.Trim();

        equipmentitem.DetailLocation = TextBox_DetailLocation.Text.Trim();

        equipmentitem.MaintenanceTimes = 0;
        equipmentitem.IsCancel = false;
        equipmentitem.OpeningDate = DateTime.Now;
        equipmentitem.PhotoUrl = "";

        equipmentitem.Remark = TextBox_Remark.Text.Trim();
        equipmentitem.ResidualRate = decimal.Parse(Hidden_ResidualRate.Value);
        equipmentitem.Responsibility = Hidden_Responsiblity.Value;
        equipmentitem.ResponsibilityName = Label_ResponsiblityName.Text;

        equipmentitem.Purchaser = Hidden_Purchaser.Value;
        equipmentitem.PurchaserName = Label_Purchaser.Text;

        equipmentitem.Checker = Hidden_Techincian.Value;
        equipmentitem.CheckerName = Label_Technician.Text;
        equipmentitem.Unit=

        
        //equipmentitem.SectionID = sectionid;
        equipmentitem.SerialNum = TextBox_SerialNum.Text.Trim();
        equipmentitem.ServiceLife = servicelife;
        equipmentitem.Specification = "";
        equipmentitem.Status = EquipmentStatus.Normal;
         
        equipmentitem.UpdateTime = DateTime.Now;
        equipmentitem.WarrantyDate = DateTime.Now.AddMonths(warranty);
        equipmentitem.FileDate = DateTime.Now;
        switch (type)
        {
            case "p":
                //数量
                PurchaseRecordInfo item = (PurchaseRecordInfo)Session[sessionName];
                if (item == null)
                {
                    item = purchaseBll.GetPurchaseRecordInfo(recordid);
                    Session[sessionName] = item;
                }
                
                equipmentitem.ExamDate = item.AcceptedTime;
                equipmentitem.CompanyID = item.CompanyID;
                equipmentitem.Model = item.Model;
                equipmentitem.Name = item.ProductName;
                equipmentitem.Price = item.PurchaseUnitPrice;
                equipmentitem.PurchaseDate = item.PurchaseTime;
                equipmentitem.PurchaseOrderID = item.PurchaseOrderID + "-" + item.SubOrderIndex;
                equipmentitem.Unit = item.Unit;
                equipmentitem.SupplierName = item.Supplier;
                equipmentitem.ProducerName = item.Producer;

                List<string> barcodeList = new List<string>();
                for (int i = 0; i < count; i++)
                {
                    string barcode = BarCode.GenerateBarCode(item.CompanyID, item.PurchaseTime, CheckBox_Component.Checked);
                    //equipmentitem.EquipmentNO = barcode;
                    if (EqBarCodeList.Count >= (i+1))
                    {
                        barcode=equipmentitem.EquipmentNO = EqBarCodeList[i].ToString().Trim();
                    } 
                    else
                    {
                    equipmentitem.EquipmentNO = barcode;
                    }
                    equipmentBll.InsertEquipment(equipmentitem);
                    purchaseBll.InsertBarcodeRecord(recordid, item.OrderID, item.PlanDetailItemID, equipmentitem.EquipmentNO, equipmentitem.Name, equipmentitem.Model);
                    barcodeList.Add(equipmentitem.EquipmentNO);
                }
                string barcodeListStr = "";
                foreach (string str in barcodeList)
                {
                    barcodeListStr += "<br/>" + str;
                }
                item.IsDividable = CheckBox_Divide.Checked;
                item.Type = ItemType.DEVICE;
                item.Status = PurchaseRecordStatus.INWAREHOUSEFINISH;
                purchaseBll.SavePurchaseRecord(item);
                //PurchaseOrderInfo order = purchaseBll.GetPurchaseOrderByID(id);
                EventMessage.MessageBox(Msg_Type.Info, "设备入库成功", "条形码分别为 " + barcodeListStr,
                         Icon_Type.OK, "javascript:window.parent.hidePopWin(true);", UrlType.JavaScript);
                
                break;
            case "ca":
                CheckAcceptanceDetailInfo caitem = (CheckAcceptanceDetailInfo)Session[sessionName];
                if (caitem == null)
                {
                    caitem = checkacceptanceBll.GetCheckAcceptanceDetailInfo(id, itemid);
                    Session[sessionName] = caitem;
                }

                equipmentitem.ExamDate = caitem.AcceptedTime;

                equipmentitem.CompanyID = caitem.CompanyID;
                equipmentitem.Model = caitem.Model;
                equipmentitem.Name = caitem.ProductName;
                equipmentitem.Price = caitem.PurchaseUnitPrice;
                equipmentitem.PurchaseDate = caitem.PurchaseTime;
                equipmentitem.PurchaseOrderID = caitem.SheetID;
                equipmentitem.Unit = caitem.Unit;
                equipmentitem.SupplierName = caitem.Supplier;
                equipmentitem.ProducerName = caitem.Producer;

                List<string> cabarcodeList = new List<string>();
                for (int i = 0; i < count; i++)
                {
                    string barcode = BarCode.GenerateBarCode(caitem.CompanyID, caitem.PurchaseTime, CheckBox_Component.Checked);
                    //equipmentitem.EquipmentNO = barcode;
                    if (EqBarCodeList!=null)
                    {
                        barcode=equipmentitem.EquipmentNO = EqBarCodeList[i].ToString().Trim();
                    }
                    else
                    {
                        equipmentitem.EquipmentNO = barcode;
                    }
                    equipmentBll.InsertEquipment(equipmentitem);
                    CheckAcceptanceDetailBarcodeInfo barcodeinfo = new CheckAcceptanceDetailBarcodeInfo();
                    //barcodeinfo.Barcode = equipmentitem.EquipmentNO;
                    barcodeinfo.Barcode = equipmentitem.EquipmentNO;
                    barcodeinfo.CompanyID = caitem.CompanyID;
                    barcodeinfo.FormID = caitem.ID;
                    barcodeinfo.ID = 0;
                    barcodeinfo.ItemID = caitem.ItemID;
                    barcodeinfo.Model = caitem.Model;
                    barcodeinfo.ProductName = caitem.ProductName;
                    checkacceptanceBll.InsertBarcode(barcodeinfo);
                    cabarcodeList.Add(equipmentitem.EquipmentNO);
                    //cabarcodeList.Add(barcode);

                    //-//******************************************************************
                    InEquipmentsInfo inEquipmentsRecord = new InEquipmentsInfo();
                    inEquipmentsRecord.ID = caitem.ID;
                    inEquipmentsRecord.ItemID = caitem.ItemID;
                    inEquipmentsRecord.IsAsset = true;
                    inEquipmentsRecord.EquipmentNO = barcode;
                    inEquipmentsRecord.EquipmentName=inEquipmentsRecord.Name = equipmentitem.Name;
                    inEquipmentsRecord.EquipmentModel= inEquipmentsRecord.Model = equipmentitem.Model;
                    inEquipmentsRecord.EquipmentType = equipmentitem.SerialNum;
                    inEquipmentsRecord.Count = 1;
                    inEquipmentsRecord.InTime = DateTime.Now;
                    inEquipmentsRecord.Unit = caitem.Unit;
                    WarehouseInfo warehouse = warehouseBll.GetWarehouse(warehouseid);
                    inEquipmentsRecord.WarehouseID = warehouseid;
                    inEquipmentsRecord.WarehouseName = warehouse.Name;
                    inEquipmentsRecord.Remark = equipmentitem.Remark;
                    inEqBll.InsertInEquipments(inEquipmentsRecord);
                    //**********************************************
                }
                string cabarcodeListStr = "";
                foreach (string str in cabarcodeList)
                {
                    cabarcodeListStr += "<br/>" + str;
                }
                caitem.IsDividable = CheckBox_Divide.Checked;
                caitem.Type = ItemType.DEVICE;
                caitem.Status = PurchaseRecordStatus.INWAREHOUSEFINISH;
                checkacceptanceBll.UpdateCheckAcceptanceDetail(caitem);
                EventMessage.MessageBox(Msg_Type.Info, "设备入库成功", "条形码分别为 " + cabarcodeListStr,
                         Icon_Type.OK, "javascript:window.parent.hidePopWin(true);", UrlType.JavaScript);
                break;
            default:
                break;
        }

        #region 全部入库完毕

        //需要更新采购单状态
        if (type == "p")
        {
            bool finish = true;
            PurchaseOrderInfo order = purchaseBll.GetPurchaseOrderByID(id);
            foreach (PurchaseOrderDetailInfo detail in order.DetailList)
            {
                if (detail.Status != PurchaseOrderDetailStatus.PURCHASINGFINISH)
                {
                    finish = false;
                }
                foreach (PurchaseRecordInfo record in detail.PurchaseRecordList)
                {
                    if (record.Status != PurchaseRecordStatus.INWAREHOUSEFINISH)
                    {
                        finish = false;
                        break;
                    }
                }
                if (!finish)
                    break;
            }
            if (finish)
            {
                order.Status = PurchaseOrderStatus.INWAREHOUSEFINISH;
                try
                {
                    purchaseBll.UpdatePurchaseOrderNoDetail(order);
                    Guid guid = new Guid(order.WorkFlowInstanceID);
                    WorkflowHelper.SetStateMachine<PurchaseEventService>(guid, PurchaseWorkflow.FinishEvent);
                }
                catch(Exception ex)
                {
                    EventMessage.EventWriteLog(Msg_Type.Error, "无法更新采购单" + order.ID + "状态" + ex.Message + "\nStack:" + ex.StackTrace);
                }
            }
        }
        #endregion

        return;

    }


    #region 有关设备种类选择的函数
    /// <summary>
    /// 选择种类事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TreeView_Catagory_SelectedNodeChanged(object sender, EventArgs e)
    {
        Hidden_CategoryID.Value = this.TreeView_Catagory.SelectedNode.Value;
        TextBox_CategoryName.Text = this.TreeView_Catagory.SelectedNode.Text;
        Category bll = new Category();
        CategoryInfo categoryinfo = bll.GetCategory(Convert.ToInt32(TreeView_Catagory.SelectedNode.Value));
        Hidden_DepreciationMethod.Value = categoryinfo.DepreciationMethod.ToString();
        Hidden_DepreciableLife.Value = categoryinfo.DepreciableLife.ToString();
        Hidden_ResidualRate.Value = categoryinfo.ResidualRate.ToString();
        PopupControlExtender_SelectCatagory.Commit(TextBox_CategoryName.Text);
    }


    /// <summary>
    /// 初始化种类弹出树
    /// </summary>
    /// <param name="ParentID"></param>
    /// <param name="pNode"></param>
    private void AddTree(long ParentID, TreeNode pNode)
    {
        CategorysearchInfo categoryinfo = new CategorysearchInfo();
        categoryinfo.Level = 1;
        Category bll = new Category();
        QueryParam qp = bll.GenerateSearchTerm(categoryinfo);
        qp.PageSize = 500;
        int recordcount = 0;
        IList nodelist = bll.GetList(qp, out recordcount);
        foreach (CategoryInfo item in nodelist)
        {
            TreeNode Node = new TreeNode();
            Node.Text = item.CategoryName;
            Node.Value = item.CategoryID.ToString();
            TreeView_Catagory.Nodes.Add(Node);
            Node.PopulateOnDemand = true;
            Node.Expanded = false;
        }
    }
    /// <summary>
    /// 树的展开事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TreeView_Catagory_OnTreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {
        if (e.Node.ChildNodes.Count == 0)
        {
            CategorysearchInfo categoryinfo = new CategorysearchInfo();
            categoryinfo.ParentID = Convert.ToInt64(e.Node.Value);
            Category bll = new Category();
            QueryParam qp = bll.GenerateSearchTerm(categoryinfo);
            qp.PageSize = 500;
            int recordcount = 0;
            IList nodelist = bll.GetList(qp, out recordcount);
            foreach (CategoryInfo item in nodelist)
            {
                TreeNode Node = new TreeNode();
                Node.Text = item.CategoryName;
                Node.Text = item.CategoryID.ToString();
                e.Node.ChildNodes.Add(Node);
                Node.PopulateOnDemand = true;
                Node.Expanded = false;
            }
        }
    }


    #endregion

    #region 有关易耗品种类选择的函数
    /// <summary>
    /// 选择种类事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TreeView_ExpendableType_SelectedNodeChanged(object sender, EventArgs e)
    {
        Hidden_ExpendableTypeID.Value = this.TreeView_ExpendableType.SelectedNode.Value;
        TextBox_ExpendableType.Text = this.TreeView_ExpendableType.SelectedNode.Text;
        Category bll = new Category();
        CategoryInfo categoryinfo = bll.GetCategory(Convert.ToInt32(TreeView_ExpendableType.SelectedNode.Value));
        PopupControlExtender1.Commit(TextBox_ExpendableType.Text);
    }


    /// <summary>
    /// 初始化种类弹出树
    /// </summary>
    /// <param name="ParentID"></param>
    /// <param name="pNode"></param>
    private void AddTree2(long ParentID, TreeNode pNode)
    {
        CategorysearchInfo categoryinfo = new CategorysearchInfo();
        categoryinfo.Level = 1;
        Category bll = new Category();
        QueryParam qp = bll.GenerateSearchTerm(categoryinfo);
        qp.PageSize = 500;
        int recordcount = 0;
        IList nodelist = bll.GetList(qp, out recordcount);
        foreach (CategoryInfo item in nodelist)
        {
            TreeNode Node = new TreeNode();
            Node.Text = item.CategoryName;
            Node.Value = item.CategoryID.ToString();
            TreeView_ExpendableType.Nodes.Add(Node);
            Node.PopulateOnDemand = true;
            Node.Expanded = false;
        }
    }
    /// <summary>
    /// 树的展开事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TreeView_ExpendableType_OnTreeNodeExpanded(object sender, TreeNodeEventArgs e)
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


    #endregion

    /// <summary>
    /// 从Excel提取数据--》Dataset
    /// </summary>
    /// <param name="filename">Excel文件路径名</param>
    private DataSet ImportXlsToData(string fileName)
    {
        try
        {
            if (fileName == string.Empty)
            {
                throw new ArgumentNullException("上传文件失败！");
            }
            //
            string oleDBConnString = String.Empty;
            oleDBConnString = "Provider=Microsoft.Jet.OLEDB.4.0;";
            oleDBConnString += "Data Source=";
            oleDBConnString += fileName;
            oleDBConnString += ";Extended Properties=Excel 8.0;";
            //
            OleDbConnection oleDBConn = null;
            OleDbDataAdapter oleAdMaster = null;
            DataTable m_tableName = new DataTable();
            DataSet ds = new DataSet();

            oleDBConn = new OleDbConnection(oleDBConnString);
            oleDBConn.Open();
            m_tableName = oleDBConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);  //表架构

            if (m_tableName != null && m_tableName.Rows.Count > 0)
            {

                m_tableName.TableName = m_tableName.Rows[0]["TABLE_NAME"].ToString();

            }
            string sqlMaster;
            sqlMaster = " SELECT *  FROM [" + m_tableName.TableName + "]";
            oleAdMaster = new OleDbDataAdapter(sqlMaster, oleDBConn);
            oleAdMaster.Fill(ds, "m_tableName");  //Fill DataSet
            oleAdMaster.Dispose();
            oleDBConn.Close();
            oleDBConn.Dispose();

            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 把DataSet->Object IList
    /// </summary>
    private IList AddDataToObject(DataSet ds)
    {
        try
        {
            ArrayList list = new ArrayList();

            int ic, ir;
            ir = ds.Tables[0].Rows.Count;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string tempBarCode = "";
                    tempBarCode = ds.Tables[0].Rows[i][0].ToString().Trim();
                    list.Add(tempBarCode);
                }
                return list;
            }
            else
            {
                list.Clear();
                throw new Exception("导入数据为空或数据格式不符合！");
            }
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
       
    }
}
