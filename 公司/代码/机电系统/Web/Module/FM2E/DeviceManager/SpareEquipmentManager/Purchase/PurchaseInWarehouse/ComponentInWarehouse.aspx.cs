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



public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseInWarehouse_ComponentInWarehouse : System.Web.UI.Page
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
    /// <summary>
    /// 设备
    /// </summary>
    Equipment equipmentBll = new Equipment();

    CheckAcceptance checkacceptanceBll = new CheckAcceptance();

    string sessionName = "Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaseInWarehouse_ComponentInWarehouse";
    /// <summary>
    /// 入库单类型，ca不经过申请采购的，p经过申请采购的
    /// </summary>
    string type = (string)Common.sink("type", MethodType.Get, 0, 0, DataType.Str);


    /// <summary>
    /// 明细条目ID参数
    /// </summary>
    long id = (long)Common.sink("id", MethodType.Get, 0, 0, DataType.Long);
    /// <summary>
    /// 仓库ID
    /// </summary>
    protected string warehouseid = (string)Common.sink("warehouse", MethodType.Get, 0, 0, DataType.Str);
    /// <summary>
    /// 明细条目
    /// </summary>
    short itemid = Convert.ToInt16(Common.sink("itemid", MethodType.Get, 0, 0, DataType.Int));
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
            

            //仓库名称 
            WarehouseInfo warehouse = warehouseBll.GetWarehouse(warehouseid);
            Hidden_WarehouseName.Value = warehouse.Name;
            //Label_LocationName.Text = warehouse.Name;

           

            //系统
            EquipmentSystem systemBll = new EquipmentSystem();
            DropDownList_System.Items.Clear();
            DropDownList_System.Items.AddRange(systemBll.GenerateListItemCollectionWithBlank());

            //默认是仓库
            TextBox_Address.Value = warehouse.AddressName;
            Hidden_AddressID.Value = warehouse.AddressID.ToString();

            //种类树
            AddTree(0, null);
            //单位
            DropDownList_Unit.DataSource = Constants.GetUnits();
            DropDownList_Unit.DataBind();

            DropDownList_Unit.Items.Insert(0, new ListItem("--请选择单位--", ""));

            FillData();
        }
    }



    /// <summary>
    /// 往数据表中填充
    /// </summary>
    private void FillData()
    {
        switch (type)
        {
            case "p":
                PurchaseRecordInfo item = purchaseBll.GetPurchaseRecordInfo(recordid);
                Session[sessionName] = item;
                Label_CheckRemark.Text = item.AcceptanceRemark;
                Label_Count.Text = item.AcceptanceCount.ToString("#,0.##");

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
                //Label_Price.Text = item.ActualPrice.ToString("#,0.##");
                //默认系统
                try
                {
                    DropDownList_System.SelectedValue = item.SystemID;
                }
                catch { }
                break;
            case "ca":
                CheckAcceptanceDetailInfo caitem = checkacceptanceBll.GetCheckAcceptanceDetailInfo(id, itemid);
                Session[sessionName] = caitem;
                Label_CheckRemark.Text = caitem.AcceptanceRemark;
                Label_Count.Text = caitem.AcceptanceCount.ToString("#,0.##");

                Label_Producer.Text = caitem.Producer;
                Label_Supplier.Text = caitem.Supplier;

                Label_ResponsiblityName.Text = "";
                Hidden_Responsiblity.Value = "";
                Label_Purchaser.Text = caitem.PurchaserName;
                Hidden_Purchaser.Value = caitem.Purchaser;
                Label_Technician.Text = caitem.TechnicianName;
                Hidden_Techincian.Value = caitem.Checker_Technician;
                Label_WarehouserKeeper.Text = caitem.WarehouseKeeperName;
                Hidden_WarehouseKeeper.Value = caitem.Checker_Warehouse;

                Label_PurchaseDate.Text = caitem.PurchaseTime.ToString("yyyy-MM-dd");
                Label_AcceptanceDate.Text = caitem.AcceptedTime.ToString("yyyy-MM-dd");
                Label_PurchaseOrderID.Text = caitem.SheetID;
                //Label_Price.Text = item.ActualPrice.ToString("#,0.##");
                //默认系统
                try
                {
                    DropDownList_System.SelectedValue = caitem.SystemID;
                }
                catch { }
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// 入库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_DeviceInWarehouse_Click(object sender, EventArgs e)
    {

        string name = TextBox_ProductName.Text.Trim();
        string model = TextBox_Model.Text.Trim();
        string unit = DropDownList_Unit.SelectedValue;

        if (name.Length == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "数据格式错误", "部件名称不能为空",
               Icon_Type.Error, "javascript:window.history.go(-1);", UrlType.JavaScript);
            return;
        }
        if (model.Length == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "数据格式错误", "部件型号规格不能为空",
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


        EquipmentInfo equipmentitem = new EquipmentInfo();
        equipmentitem.CategoryID = catagoryid;
        equipmentitem.CategoryName = TextBox_CategoryName.Text;
        equipmentitem.Checker = Label_Technician.Text;
        equipmentitem.CheckerName = Label_Technician.Text;
        equipmentitem.DepreciableLife = long.Parse(Hidden_DepreciableLife.Value);
        equipmentitem.DepreciationMethod = int.Parse(Hidden_DepreciationMethod.Value);

        
        equipmentitem.DetailLocation = TextBox_DetailLocation.Text.Trim();
        equipmentitem.SystemID = DropDownList_System.SelectedValue;

        long addressid = 0;
        if (!string.IsNullOrEmpty(TextBox_Address.Value.Trim()))
        {
            long.TryParse(Hidden_AddressID.Value, out addressid);
        }
        equipmentitem.AddressID = addressid;
        equipmentitem.AssertNumber = TextBox_AssertNumber.Text.Trim();

        equipmentitem.Purchaser = Hidden_Purchaser.Value;
        equipmentitem.PurchaserName = Label_Purchaser.Text;

        equipmentitem.Checker = Hidden_Techincian.Value;
        equipmentitem.CheckerName = Label_Technician.Text;

        equipmentitem.EquipmentNO = "";
        equipmentitem.FileDate = DateTime.Now;
        equipmentitem.IsCancel = false;
        equipmentitem.MaintenanceTimes = 0;
        equipmentitem.Model = model;
        equipmentitem.Name = name;
        equipmentitem.OpeningDate = DateTime.Now;
        equipmentitem.PhotoUrl = "";
     
        equipmentitem.Remark = TextBox_Remark.Text.Trim();
        equipmentitem.ResidualRate = decimal.Parse(Hidden_ResidualRate.Value);
        equipmentitem.Responsibility = Hidden_Responsiblity.Value;
        equipmentitem.ResponsibilityName = Label_ResponsiblityName.Text;
        equipmentitem.SectionID = "";
        equipmentitem.SerialNum = TextBox_SerialNum.Text.Trim();
        equipmentitem.ServiceLife = servicelife;
        equipmentitem.Specification = "";
        equipmentitem.Status = EquipmentStatus.Normal;

        equipmentitem.Price = 0;

        equipmentitem.UpdateTime = DateTime.Now;
        equipmentitem.WarrantyDate = DateTime.Now.AddMonths(warranty);
        switch (type)
        {
            case "p":

                PurchaseRecordInfo item = (PurchaseRecordInfo)Session[sessionName];
                if (item == null)
                {
                    item = purchaseBll.GetPurchaseRecordInfo(recordid);
                    Session[sessionName] = item;
                }

                equipmentitem.CompanyID = item.CompanyID;
                equipmentitem.ExamDate = item.AcceptedTime;
                //equipmentitem.LocationID = item.WarehouseID.ToString();
                //equipmentitem.Price = item.PurchaseUnitPrice;
                equipmentitem.PurchaseDate = item.PurchaseTime;
                equipmentitem.PurchaseOrderID = item.PurchaseOrderID + "-" + item.SubOrderIndex;
                equipmentitem.Purchaser = item.Purchaser;
                equipmentitem.PurchaserName = item.Purchaser;
                equipmentitem.SupplierName = item.Supplier;
                equipmentitem.ProducerName = item.Producer;

                IList baseBarcode = item.BaseBarcodeList;
                List<string> barcodeList = new List<string>();
                for (int i = 0; i < baseBarcode.Count; i++)
                {
                    string barcode = BarCode.GenerateBarCode((baseBarcode[i] as PurchaseBarcodeInfo).Barcode);
                    equipmentitem.EquipmentNO = barcode;
                    equipmentBll.InsertEquipment(equipmentitem);
                    barcodeList.Add(barcode);
                    purchaseBll.InsertBarcodeRecord(item.ID, item.OrderID, item.PlanDetailItemID, barcode, equipmentitem.Name, equipmentitem.Model);
                }
                string barcodeListStr = "";
                foreach (string str in barcodeList)
                {
                    barcodeListStr += "<br/>" + str;
                }
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

                equipmentitem.CompanyID = caitem.CompanyID;
                equipmentitem.ExamDate = caitem.AcceptedTime;
                //equipmentitem.LocationID = caitem.WarehouseID.ToString();
                //equipmentitem.Price = caitem.PurchaseUnitPrice;
                equipmentitem.PurchaseDate = caitem.PurchaseTime;
                equipmentitem.PurchaseOrderID = caitem.SheetID;
                equipmentitem.Purchaser = caitem.Purchaser;
                equipmentitem.PurchaserName = caitem.Purchaser;
                equipmentitem.SupplierName = caitem.Supplier;
                equipmentitem.ProducerName = caitem.Producer;

                IList cabaseBarcode = caitem.BaseBarcodeList;
                List<string> cabarcodeList = new List<string>();
                for (int i = 0; i < cabaseBarcode.Count; i++)
                {
                    string barcode = BarCode.GenerateBarCode((cabaseBarcode[i] as CheckAcceptanceDetailBarcodeInfo).Barcode);
                    equipmentitem.EquipmentNO = barcode;
                    equipmentBll.InsertEquipment(equipmentitem);
                    cabarcodeList.Add(barcode);
                    CheckAcceptanceDetailBarcodeInfo barcodeinfo = new CheckAcceptanceDetailBarcodeInfo();
                    barcodeinfo.Barcode = barcode;
                    barcodeinfo.CompanyID = caitem.CompanyID;
                    barcodeinfo.FormID = caitem.ID;
                    barcodeinfo.ID = 0;
                    barcodeinfo.ItemID = caitem.ItemID;
                    barcodeinfo.Model = caitem.Model;
                    barcodeinfo.ProductName = caitem.ProductName;
                    checkacceptanceBll.InsertBarcode(barcodeinfo);
                }
                string cabarcodeListStr = "";
                foreach (string str in cabarcodeList)
                {
                    cabarcodeListStr += "<br/>" + str;
                }
                EventMessage.MessageBox(Msg_Type.Info, "设备入库成功", "条形码分别为 " + cabarcodeListStr,
                          Icon_Type.OK, "javascript:window.parent.hidePopWin(true);", UrlType.JavaScript);

                break;
            default:
                break;
        }
        return;


    }
    #region 设备种类选择函数
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
}
