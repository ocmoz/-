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
using FM2E.WorkflowLayer;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_OutWarehouse_OutWarehouseRecord_OutWarehouseRecord : System.Web.UI.Page
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


    private string CurrentAction
    {
        get
        {
            string action = (string)ViewState["action"];
            if (string.IsNullOrEmpty(action))
                action = ADD_EQUIPMENT;
            return action;
        }
        set { ViewState["action"] = value; }
    }

    private const string ADD_EQUIPMENT = "addDetail";
    private const string UPDATE_EQUIPMENT= "updateDetail";


    /// <summary>
    /// 初始化页面
    /// </summary>
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

            CascadingDropDown1.SelectedValue = item.WarehouseID;
            DDLProduct.Visible = false;
            DDLModel.Visible = false;
            DDLUnit.Items.Clear();
            IList list1 = Constants.GetUnits();
            foreach (string unit in list1)
                DDLUnit.Items.Add(new ListItem(unit, unit));
            DDLUnit.Items.Insert(0, new ListItem("请选择单位", ""));
            TextBox4.Text = "1";
            TextBox4.Enabled = false;
            Label_Status.Text = item.WorkFlowStateDescription;
            LblErrorMessage.Text = "";

            DDLSystem.Items.Clear();
            DDLSystem.Items.AddRange(ListItemHelper.GetSystemListItemsWithBlank());

            //Section sbll = new Section();
            //SectionInfo sectioninfo = new SectionInfo();
            //sectioninfo.CompanyID = item.CompanyID;
            //QueryParam sectionqp = sbll.GenerateSearchTerm(sectioninfo);
            //sectionqp.PageSize = 500;
            //int sectionrc = 0;
            //IList sectionlist = sbll.GetList(sectionqp, out sectionrc);
            //foreach (SectionInfo item1 in sectionlist)
            //{
            //    DDLSection.Items.Add(new ListItem(item1.SectionName, item1.SectionID));
            //}
            //DDLSection.Items.Insert(0, new ListItem("请选择路段", ""));
            //EquipmentSystem ebll = new EquipmentSystem();
            //IList elist = ebll.GetAllSystem();
            //foreach (EquipmentSystemInfo item2 in elist)
            //{
            //    DDLSystem.Items.Add(new ListItem(item2.SystemName, item2.SystemID));
            //}
            //DDLSystem.Items.Insert(0, new ListItem("请选择系统", ""));
            //CascadingDropDown6.Category = item.CompanyID;
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
    /// <summary>
    /// 初始化GridView2等数据
    /// </summary>
    private void FillDataOfEquipment(OutWarehouseDetailInfo detail)
    {
        try
        {
           
            ArrayList list = new ArrayList();
            list.Add(detail);
            GridView3.DataSource = list;
            GridView3.DataBind();

            DDLUnit.SelectedValue = detail.Unit;

            Hidden_AddressID.Value = detail.AddressID.ToString();
            TextBox_Address.Value = detail.AddressName;
            TextBox_DetailLocation.Value = detail.DetailLocation;

            DDLSystem.SelectedValue = detail.SystemID;
                        
            GridView2.DataSource = detail.OutEquipmentList;
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /////// <summary>
    /////// 获取已出库数量
    /////// </summary>
    /////// <param name="itemid"></param>
    /////// <returns></returns>
    ////public decimal getCountOfOutWarehouse(long itemid)
    ////{
    ////    ArrayList OutEquipmentList = (ArrayList)Session[RECORDLIST_SESSION];
    ////    if (OutEquipmentList == null)
    ////        return decimal.Zero;
    ////    decimal count = decimal.Zero;
    ////    foreach (OutEquipmentsInfo info in OutEquipmentList)
    ////    {
    ////        if (info.ApplyItemID == itemid)
    ////        {
    ////            count += info.Count;
    ////        }
    ////    }
    ////    return count;
    ////}
    ///// <summary>
    ///// 获取已出库数量
    ///// </summary>
    ///// <param name="itemid"></param>
    ///// <returns></returns>
    //public decimal getCountOfOutExpendable(long ExpendableID)
    //{
    //    ArrayList OutEquipmentList = (ArrayList)Session[RECORDLIST_SESSION];
    //    if (OutEquipmentList == null)
    //        return decimal.Zero;
    //    decimal count = decimal.Zero;
    //    foreach (OutEquipmentsInfo info in OutEquipmentList)
    //    {
    //        if (info.ExpendableID == ExpendableID)
    //        {
    //            count += info.Count;
    //        }
    //    }
    //    return count;
    //}

    ///// <summary>
    ///// 添加出库操作
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    TabContainer1.Tabs[1].Visible = false;
    //    TabContainer1.Tabs[2].Visible = true;
    //    TBEquipment.Focus();
    //}
    ///// <summary>
    ///// 返回出库明细操作
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    TabContainer1.Tabs[0].Visible = true;
    //    TabContainer1.Tabs[1].Visible = false;
    //    FillData();
    //}
    /// <summary>
    /// 确认出库操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSubmit_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;

        OutWarehouseApplyInfo item = CurrentOutWarehouseApplyInfo;
        bool hasEquipment = false;
        foreach (OutWarehouseDetailInfo detail in item.ApplyDetailList)
        {
            if (detail.OutEquipmentList.Count > 0)
            {
                hasEquipment = true;
            }
        }
        if (!hasEquipment)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "未选择出库设备", Icon_Type.Error, false, "history.go(-1)", UrlType.JavaScript, "");

        }

        //先检查借用人的用户名与密码是否相符
        string errorMsg = "";
        if (tbReceiver.Text.Trim() == "")
            errorMsg = "请输入领用人用户名";
        else if (tbPassword.Text.Trim() == "")
            errorMsg = "请输入领用人密码";

        if (errorMsg != "")
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入有误：" + errorMsg, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        bool bValidate = false;
        try
        {
            User bll = new User();
            bValidate = bll.ValidatePassword(tbReceiver.Text.Trim(), Common.md5(tbPassword.Text.Trim(), 32));
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "校验领用人用户名密码时发生错误", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        if (!bValidate)
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "校验领用人用户名密码不相符", Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");

        try
        {
            item.OutTime = DateTime.Now;
            item.Receiver = tbReceiver.Text.Trim();
            item.Operator = Common.Get_UserName;
            item.OutWarehouseRemark = TextArea1.Value.Trim();

            bll.DoOutWarehosue(item);
            try
            {
                Guid guid = new Guid(item.WorkFlowInstanceID);
                WorkflowHelper.SetStateMachine<OutWarehouseEventService>(guid, OutWarehouseWorkflow.FinishOutEvent);
            }
            catch (Exception wfe)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "出库失败："+wfe.Message, wfe, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            bSuccess = true;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "出库失败，请检查是否有登记出库信息", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        if (bSuccess)
        {
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "出库成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("OutWarehouseApply.aspx"), UrlType.Href, "");
        }

    }
    /// <summary>
    /// 添加明细和更新明细响应的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button3_Click(object sender, EventArgs e)
    {
        LblErrorMessage.Text = "";
        if (CheckBox1.Checked)
        {
            if (TBEquipment.Text.Trim().Equals(string.Empty))
                LblErrorMessage.Text = "请输入设备条形码";
            else if (TBProduct.Text.Trim().Equals(NOEQUIPMENT))
                LblErrorMessage.Text = "没有对应该条形码的设备";
            if (!LblErrorMessage.Text.Trim().Equals(string.Empty))
                return;
        }
        else
        {
            if (DDLModel.SelectedValue.Equals(string.Empty))
            {
                LblErrorMessage.Text = "请选择消产品名称和型号";
                return;
            }
        }
        if (DDLUnit.SelectedValue.Equals(string.Empty))
        {
            LblErrorMessage.Text = "请选择单位";
        }

        if (Hidden_AddressID.Value==""||Hidden_AddressID.Value=="0")
        {
            LblErrorMessage.Text = "请选择地址";
        }
        
        if (TextBox4.Text.Trim() == "")
            LblErrorMessage.Text = "数量不能为空";

        if (LblErrorMessage.Text != "")
        {
            return;
        }

        decimal count = decimal.Parse(TextBox4.Text.Trim());

        OutWarehouseApplyInfo apply = CurrentOutWarehouseApplyInfo;
        long detailid = long.Parse(Hidden_EditItemID.Value);
        OutWarehouseDetailInfo detail = null;
        foreach (OutWarehouseDetailInfo d in apply.ApplyDetailList)
        {
            if (detailid == d.ItemID)
            {
                detail = d;
                break;
            }
        }
        if (detail == null) return;

        OutEquipmentsInfo eqItem = null;
        if (CurrentAction == UPDATE_EQUIPMENT)
        {
            int index = Convert.ToInt32(Hidden_EditEquipmentItemIndex.Value);
            if (index >= detail.OutEquipmentList.Count) return;
            eqItem = detail.OutEquipmentList[index] as OutEquipmentsInfo;
            try
            {

                if (detail.OutCount + count - eqItem.Count > detail.Count)
                {
                    LblErrorMessage.Text = "出库数量超过申请数量";
                    return;
                }
                if (!CheckBox1.Checked)
                {
                    Expendable expendableBll = new Expendable();
                    decimal countOfWarehouse = expendableBll.GetCountOfExpendable(eqItem.WarehouseID, eqItem.ExpendableID);
                    if (countOfWarehouse < 0)
                    {
                        LblErrorMessage.Text = "当前仓库没有该易耗品";
                        return;
                    }
                    if (countOfWarehouse < count + detail.GetExpandableCount(eqItem.ExpendableID) - eqItem.Count)
                    {
                        LblErrorMessage.Text = "当前库存量只有" + (countOfWarehouse - detail.GetExpandableCount(eqItem.ExpendableID) + eqItem.Count).ToString("#,0.#####");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "更新明细失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");

            }
        }
        else
        {
            try
            {
                decimal countNow = detail.OutCount;
                if (countNow + count > detail.Count)
                {
                    LblErrorMessage.Text = "出库数量超过申请数量";
                    return;
                }
                if (!CheckBox1.Checked)
                {
                    Expendable expendableBll = new Expendable();
                    decimal countOfWarehouse = expendableBll.GetCountOfExpendable(apply.WarehouseID, DDLModel.SelectedValue == "" ? 0L : long.Parse(DDLModel.SelectedValue));
                    if (countOfWarehouse < 0)
                    {
                        LblErrorMessage.Text = "当前仓库没有该易耗品";
                        return;
                    }
                    if (countOfWarehouse - detail.GetExpandableCount(DDLModel.SelectedValue == "" ? 0L : long.Parse(DDLModel.SelectedValue)) < count)
                    {
                        LblErrorMessage.Text = "当前库存量只有" + (countOfWarehouse - detail.GetExpandableCount(DDLModel.SelectedValue == "" ? 0L : long.Parse(DDLModel.SelectedValue))).ToString("#,0.#####");
                        return;
                    }
                }
                else
                {
                    foreach (OutWarehouseDetailInfo d in apply.ApplyDetailList)
                    {
                        foreach (OutEquipmentsInfo oeq in d.OutEquipmentList)
                        {
                            if (oeq.EquipmentNO == TBEquipment.Text.Trim())
                            {
                                LblErrorMessage.Text = "该设备条形码已存在";
                                TBEquipment.Focus();
                                TBEquipment.Attributes.Add("onfocus", "f(this)");
                                return;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加明细失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }

            eqItem = new OutEquipmentsInfo();
            eqItem.ApplyItemID = detail.ItemID;
            eqItem.WarehouseID = apply.WarehouseID;
            eqItem.WarehouseName = apply.WarehouseName;
            detail.OutEquipmentList.Add(eqItem);

        }
        if (CheckBox1.Checked)
        {
            eqItem.IsAsset = true;
            eqItem.EquipmentNO = TBEquipment.Text.Trim();
            eqItem.ExpendableID = 0;
            eqItem.SystemName = DDLSystem.SelectedItem.Text;
            eqItem.Name = TBProduct.Text.Trim();
            eqItem.Model = TBModel.Text.Trim();
            TextBox4.Text = "1";
            TextBox4.Enabled = false;
            
        }
        else
        {
            eqItem.IsAsset = false;
            eqItem.EquipmentNO = "";
            eqItem.ExpendableID = Convert.ToInt64(DDLModel.SelectedValue);
            eqItem.Name = DDLProduct.SelectedItem.Text;
            eqItem.Model = DDLModel.SelectedItem.Text;
            //TextBox4.Text = "";
        }

        eqItem.Count = 1;
        eqItem.SystemID = DDLSystem.SelectedValue;
        eqItem.DetailLocation = TextBox_DetailLocation.Value;
        eqItem.AddressID = long.Parse(Hidden_AddressID.Value);
        eqItem.AddressName = TextBox_Address.Value;

        eqItem.Unit = DDLUnit.SelectedValue;
        eqItem.Remark = TextArea3.Value.Trim();
        eqItem.OutTime = DateTime.Now;

        CurrentOutWarehouseApplyInfo = apply;
        if (CheckBox1.Checked)
        {
            TBEquipment.Focus();
        }

        TBEquipment.Text = "";
        TBProduct.Text = "";
        TBModel.Text = "";
        CascadingDropDown3.SelectedValue = "";
        CascadingDropDown4.SelectedValue = "";

        CurrentAction = ADD_EQUIPMENT;
        LblErrorMessage.Text = "";

        FillData();
        FillDataOfEquipment(detail);
        //if (bSuccess)
        //{
        //    EventMessage.MessageBox(1, "操作成功", "添加明细成功！", Icon_Type.OK, false, Common.GetHomeBaseUrl("Employee.aspx"), UrlType.Href, "");
        //}
    }
    ///// <summary>
    ///// 分页事件
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    //{
    //    FillDataOfEquipment();
    //}
    ///// <summary>
    ///// 分页事件
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    //{
    //    FillData();
    //}
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
            if (equipment != null)
            {
                TBProduct.Text = equipment.Name;
                TBModel.Text = equipment.Model;
                DDLUnit.SelectedValue = equipment.Unit;
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
    /// CheckBox1变化时的响应
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {
            DDLProduct.Visible = false;
            DDLModel.Visible = false;
            TBProduct.Visible = true;
            TBModel.Visible = true;
            TBEquipment.Visible = true;
            TextBox4.Text = "1";
            TextBox4.Enabled = false;
            TBEquipment.Focus();
        }
        else
        {
            CascadingDropDown1.SelectedValue = CurrentOutWarehouseApplyInfo.WarehouseID;
            TBEquipment.Visible = false;
            DDLProduct.Visible = true;
            DDLModel.Visible = true;
            TBProduct.Visible = false;
            TBModel.Visible = false;
            TBEquipment.Visible = false;
            TextBox4.Text = "";
            TextBox4.Enabled = true;
            //this.TableLocaton.Style.Keys["display"] = "none";
        }
        TBEquipment.Text = "";
        TBProduct.Text = "";
        TBModel.Text = "";
        LblErrorMessage.Text = "";
    }


    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        long itemid = long.Parse(Hidden_EditItemID.Value);
        OutWarehouseDetailInfo detail = null;
        OutWarehouseApplyInfo applyitem = CurrentOutWarehouseApplyInfo;
        foreach (OutWarehouseDetailInfo d in applyitem.ApplyDetailList)
        {
            if (itemid == d.ItemID)
            {
                detail = d;
                break;
            }
        }
        if (detail == null) return;

        int index = Convert.ToInt32(e.CommandArgument.ToString());

        if (index >= detail.OutEquipmentList.Count) return;

        OutEquipmentsInfo eqItem = detail.OutEquipmentList[index] as OutEquipmentsInfo;

        LblErrorMessage.Text = "";
        if (e.CommandName == "editItem")
        {
            //TabContainer1.Tabs[1].HeaderText = "更新出库信息";
            try
            {

                CheckBox1.Checked = eqItem.IsAsset;
                if (eqItem.IsAsset)
                {
                    TBEquipment.Text = eqItem.EquipmentNO;
                    TBProduct.Text = eqItem.Name;
                    TBModel.Text = eqItem.Model;
                    DDLProduct.Visible = false;
                    DDLModel.Visible = false;
                    TBProduct.Visible = true;
                    TBModel.Visible = true;
                    TBEquipment.Visible = true;
                }
                else
                {
                    TBEquipment.Text = "";
                    CascadingDropDown1.SelectedValue = eqItem.WarehouseID;
                    CascadingDropDown3.SelectedValue = eqItem.Name;
                    CascadingDropDown4.SelectedValue = eqItem.ExpendableID.ToString();
                    TBEquipment.Visible = false;
                    DDLProduct.Visible = true;
                    DDLModel.Visible = true;
                    TBProduct.Visible = false;
                    TBModel.Visible = false;
                    TBEquipment.Visible = false;
                }
                TextBox_Address.Value = eqItem.AddressName;
                Hidden_EditEquipmentItemIndex.Value = index.ToString();
                Hidden_AddressID.Value = eqItem.AddressID.ToString();
                TextBox_DetailLocation.Value = eqItem.DetailLocation;
                DDLSystem.SelectedValue = eqItem.SystemID;
                TextBox4.Text = eqItem.Count.ToString("0.#####");
                DDLUnit.SelectedValue = eqItem.Unit;
                TextArea3.Value = eqItem.Remark;
                Button3.Text = "更新明细";
                CurrentAction = UPDATE_EQUIPMENT;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "编辑数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        else if (e.CommandName == "del")
        {
            try
            {
                detail.OutEquipmentList.Remove(eqItem);
                GridView3.DataSource = detail.OutEquipmentList;
                GridView3.DataBind();

                CurrentOutWarehouseApplyInfo = applyitem;
                FillData();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";
            OutEquipmentsInfo item = (OutEquipmentsInfo)e.Row.DataItem;
            e.Row.Attributes["itemID"] = item.ItemID.ToString();
        }
    }
    protected void Repeater_Detail_RowCommand(object sender, RepeaterCommandEventArgs e)
    {
        long itemid = Convert.ToInt64(e.CommandArgument.ToString());
        OutWarehouseApplyInfo applyitem = CurrentOutWarehouseApplyInfo;
        OutWarehouseDetailInfo detail = null;
        foreach (OutWarehouseDetailInfo d in applyitem.ApplyDetailList)
        {
            if (d.ItemID == itemid)
            {
                detail = d;
                break;
            }
        }
        if (detail == null) return;
        decimal itemcount = detail.Count;
        if (e.CommandName == "outEquipments")
        {
            try
            {
                //TabContainer1.Tabs[1].Visible = true;
                //TabContainer1.Tabs[0].Visible = false;
                TabContainer1.Tabs[2].Visible = true;
                TabContainer1.ActiveTabIndex = 2;
                Hidden_EditItemID.Value = itemid.ToString();

                FillDataOfEquipment(detail);
                TBEquipment.Focus();
                //ArrayList OutEquipmentList = new ArrayList();
                //Session[RECORDLIST_SESSION] = OutEquipmentList;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "编辑数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }


    //********************************************************************

   
    /// <summary>
    /// 添加明细和更新明细响应中 完成 按钮响应事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button4_Click(object sender, EventArgs e)
    {
        TabContainer1.Tabs[2].Visible = false;
        TabContainer1.ActiveTabIndex = 0;

    }

    //**********************************************************************

    
    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        //鼠标移动到每项时颜色交替效果    
    //        e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
    //        e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

    //        //设置悬浮鼠标指针形状为"小手"    
    //        e.Row.Attributes["style"] = "Cursor:hand";
    //        OutWarehouseDetailInfo item = (OutWarehouseDetailInfo)e.Row.DataItem;
    //        e.Row.Attributes["itemID"] = item.ItemID.ToString();
    //        e.Row.Attributes["itemCount"] = item.Count.ToString();
    //        //string click = "javascript:showPopWin('出库操作','OutWarehouseRecordDetail.aspx?orderID=" + Label1.Text + "&itemID=" + item.ItemID.ToString() + "&count=" + item.Count.ToString() + "', 900, 450, addtolist,true,true);return false;";
    //        //ImageButton cb = (ImageButton)e.Row.FindControl("ImageButton1");
    //        //if (cb != null)
    //        //    cb.Attributes.Add("onclick", click);
    //    }
    //}
}
