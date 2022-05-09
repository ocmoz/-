using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;

using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using FM2E.BLL.Basic;
using FM2E.BLL.Utils;
using FM2E.BLL.BarCode;
using FM2E.Model.Utils;
using FM2E.BLL.Maintain;
using FM2E.Model.Exceptions;
using FM2E.Model.Basic;
using FM2E.Model.Maintain;

public partial class Module_FM2E_DeviceManager_DeviceInfo_CurrentEuipementInfo_WarehouseEquipmentInfo_ViewDeviceInfo : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 32, 0, DataType.Long);
     protected string companyid = (string)Common.sink("companyid", MethodType.Get, 50, 0, DataType.Str);
    private readonly MalfunctionHandle malfunctionBll = new MalfunctionHandle();
    private readonly Maintain maintainBll = new Maintain();

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);

        if (!IsPostBack)
        {
            Session.Remove(Constants.BARCODE_SESSION_STRING);
            BindData();
            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            PermissionControl();
            //********** Modification Finished 2011-09-09 **********************************************************************************************
            ButtonBind();
        }
    }

    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
    private void PermissionControl()
    {
        HeadMenuWebControls1.ButtonList[4].ButtonVisible = SystemPermission.CheckPermission(PopedomType.Print);
        HeadMenuWebControls1.ButtonList[3].ButtonVisible = SystemPermission.CheckPermission(PopedomType.Delete);
        HeadMenuWebControls1.ButtonList[2].ButtonVisible = SystemPermission.CheckPermission(PopedomType.Edit);
        HeadMenuWebControls1.ButtonList[1].ButtonVisible = SystemPermission.CheckPermission(PopedomType.Edit);
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************

    private void ButtonBind()
    {
        HeadMenuButtonItem button0 = HeadMenuWebControls1.ButtonList[0];
        button0.ButtonUrl += "?companyid=" + companyid;
        if (cmd == "view")
        {
            //删除
            HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[3];
            button.ButtonUrlType = UrlType.JavaScript;
            button.ButtonUrl = string.Format("DelData('?cmd=delete&id={0}&companyid={1}');", id, companyid);
            //修改
            button = HeadMenuWebControls1.ButtonList[2];
            button.ButtonUrlType = UrlType.Href;
            button.ButtonUrl = string.Format("EditDevice.aspx?cmd=edit&id={0}&companyid={1}", id, companyid);
            //拆分设备
            button = HeadMenuWebControls1.ButtonList[1];
            button.ButtonUrlType = UrlType.Href;
            button.ButtonUrl = string.Format("EditDevice.aspx?cmd=add&id={0}&companyid={1}&action=split", id, companyid);

            HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;

            //检查是否需要显示拆分设备的按钮,最多只可以拆分出35件子设备
            if ((int)ViewState["NextSplitNO"] > 35)
            {
                button.ButtonVisible = false;
            }

            //打印条形码
            button = HeadMenuWebControls1.ButtonList[4];
            button.ButtonUrlType = UrlType.JavaScript;
            button.ButtonUrl = string.Format("javascript:showPopWin('打印条形码','{0}Module/FM2E/DeviceManager/BarCode/BarCodePrint.aspx',800, 330, null,true,true);", Page.ResolveUrl("~"));
        }
        else if (cmd == "delete")
        {
            //执行删除操作
            bool bSuccess = false;
            try
            {
                Equipment bll = new Equipment();
                EquipmentInfoFacade equipmentinfo = bll.GetEquipment(id.ToString());
                bll.DelEquipment(id.ToString());


                if (equipmentinfo.PhotoUrl != null && equipmentinfo.PhotoUrl != string.Empty)
                {
                    FileUpLoadCommon.DeleteFile(equipmentinfo.PhotoUrl);
                }
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除记录失败：" + ex.Message, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除记录ID:(" + id + ")成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("DeviceInfo.aspx?companyid=" + companyid), UrlType.Href, "");
        }
    }

    private void BindData()
    {
        if (cmd == "view")
        {
            try
            {
                Equipment equipment = new Equipment();
                EquipmentInfoFacade item = equipment.GetEquipment(id.ToString());
                /**判断是否具有编辑删除等权限**/
                Warehouse bllwh = new Warehouse();
                WarehouseInfo warehouse = bllwh.GetWarehouseByUserName(UserData.CurrentUserData.UserName);

                if (warehouse == null || warehouse.WareHouseID == null || warehouse.WareHouseID == "")
                {
                    if ((UserData.CurrentUserData.IsParentCompany) || ((!UserData.CurrentUserData.IsParentCompany) && (item.CompanyID == UserData.CurrentUserData.CompanyID)))
                    {
                        ((HeadMenuButtonItem)HeadMenuWebControls1.ButtonList[1]).ButtonVisible = true;
                        ((HeadMenuButtonItem)HeadMenuWebControls1.ButtonList[2]).ButtonVisible = true;
                        ((HeadMenuButtonItem)HeadMenuWebControls1.ButtonList[3]).ButtonVisible = true;
                    }
                    else
                    {
                        ((HeadMenuButtonItem)HeadMenuWebControls1.ButtonList[1]).ButtonVisible = false;
                        ((HeadMenuButtonItem)HeadMenuWebControls1.ButtonList[2]).ButtonVisible = false;
                        ((HeadMenuButtonItem)HeadMenuWebControls1.ButtonList[3]).ButtonVisible = false;
                    }

                }
                else
                {

                    //if (warehouse.WareHouseID == item.LocationID)
                    //{
                        ((HeadMenuButtonItem)HeadMenuWebControls1.ButtonList[1]).ButtonVisible = true;
                        ((HeadMenuButtonItem)HeadMenuWebControls1.ButtonList[2]).ButtonVisible = true;
                        ((HeadMenuButtonItem)HeadMenuWebControls1.ButtonList[3]).ButtonVisible = true;
                    //}
                    //else
                    //{
                    //    ((HeadMenuButtonItem)HeadMenuWebControls1.ButtonList[1]).ButtonVisible = false;
                    //    ((HeadMenuButtonItem)HeadMenuWebControls1.ButtonList[2]).ButtonVisible = false;
                    //    ((HeadMenuButtonItem)HeadMenuWebControls1.ButtonList[3]).ButtonVisible = false;
                    //}
                }

                

                Session["EquipmentInfo" + id] = item;   //暂时记录下设备信息，以备编辑时使用

                equipmentno.Text = item.EquipmentNO;
                equipmentname.Text = item.Name;
                Label_AssertNumber.Text = item.AssertNumber;
                companyname.Text = item.CompanyName;
                //sectionname.Text = item.SectionName;
                //switch (item.LocationTag)
                //{
                //    case "1":
                //        {
                //            Channal channalbll = new Channal();
                //            locationname.Text = "隧道："+channalbll.GetChannal(item.LocationID).ChannalName;
                //            break;
                //        }
                //    case "2":
                //        {
                //            TollGate tollgate = new TollGate();
                //            locationname.Text = "收费站：" + tollgate.GetTollGate(item.LocationID).TollGateName;
                //            break;
                //        }
                //    case "3":
                //        {
                //            locationname.Text = "桩号";
                //            break;
                //        }
                //    case "4":
                //        {
                //            //Warehouse warehouse = new Warehouse();
                //            locationname.Text = "仓库：" + bllwh.GetWarehouse(item.LocationID).Name;
                //            break;
                //        }
                //    default: break;

                //}
                Label_DetailLocation.Text = item.DetailLocation;
                systemname.Text = item.SystemName;
                serialnum.Text = item.SerialNum;
                model.Text = item.Model;
                specification.Text = item.Specification;
                //shebeizhuangtai.Text = item.Status;
                //switch (item.Status)
                //{
                //    case 1:
                //        {
                //            status.Text = "正常";
                //            break;
                //        }
                //    case 2:
                //        {
                //            status.Text = "故障待修";
                //            break;
                //        }
                //    case 3:
                //        {
                //            status.Text = "报废";
                //            break;
                //        }
                //    case 4:
                //        {
                //            status.Text = "遗失";
                //            break;
                //        }
                //    default: break;

                //}
                status.Text = EnumHelper.GetDescription(item.Status);

                purchaseorderid.Text = item.PurchaseOrderID;

                Label_AddressName.Text = item.AddressName;
                Label_Supplier.Text = item.SupplierName;
                Label_Producer.Text = item.ProducerName;
                purchasername.Text = item.PurchaserName;
                responsibilityname.Text = item.ResponsibilityName;
                checkername.Text = item.CheckerName;
                if (DateTime.Compare(item.PurchaseDate, DateTime.MinValue) != 0)
                    purchasedate.Text = item.PurchaseDate.ToString(Constants.DateFormatString);
                if (DateTime.Compare(item.ExamDate, DateTime.MinValue) != 0)
                    examdate.Text = item.ExamDate.ToString(Constants.DateFormatString);
                if (DateTime.Compare(item.OpeningDate, DateTime.MinValue) != 0)
                    openingdate.Text = item.OpeningDate.ToString(Constants.DateFormatString);
                if (DateTime.Compare(item.FileDate, DateTime.MinValue) != 0)
                    filedate.Text = item.FileDate.ToString(Constants.DateFormatString);
                if (DateTime.Compare(item.WarrantyDate, DateTime.MinValue) != 0)
                    warrantydate.Text = item.WarrantyDate.ToString(Constants.DateFormatString);
                servicelife.Text = item.ServiceLife.ToString();
                if (DateTime.Compare(item.UpdateTime, DateTime.MinValue) != 0)
                    updatetime.Text = item.UpdateTime.ToString(Constants.DateFormatString);
                categoryname.Text = item.CategoryName;
                switch (item.DepreciationMethod)
                {
                    case 1:
                        {
                            depreciationmethod.Text = "不折旧";
                            if (item.Price != decimal.Zero)
                            {
                                price.Text = item.Price.ToString("#,0.##");
                                net.Text = item.Price.ToString("#,0.##");
                            }
                            break;
                        }
                    case 2:
                        {
                            depreciationmethod.Text = "直线折旧";
                            if (item.Price != decimal.Zero)
                            {
                                price.Text = item.Price.ToString("#,0.##");
                                DateTime begintime = item.PurchaseDate.AddMonths(1);
                                net.Text = DepreciationMethod.GetResidualAssetByLinearReduce(begintime.Year,
                                    begintime.Month, DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(item.DepreciableLife),
                                    item.Price, Convert.ToDouble(item.ResidualRate)).ToString("#,0.##");
                            }
                            break;
                        }
                    case 3:
                        {
                            depreciationmethod.Text = "双倍余额";
                            if (item.Price != decimal.Zero)
                            {
                                price.Text = item.Price.ToString("#,0.##");
                                DateTime begintime = item.PurchaseDate.AddMonths(1);
                                net.Text = DepreciationMethod.GetResidualAssetByDoubleReduce(begintime.Year,
                                    begintime.Month, DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(item.DepreciableLife),
                                    item.Price, Convert.ToDouble(item.ResidualRate)).ToString("#,0.##");
                            }
                            break;
                        }
                    default: break;
                }
                if (item.DepreciableLife != 0)
                    depreciablelife.Text = item.DepreciableLife.ToString();
                if (item.ResidualRate != decimal.Zero)
                    residualrate.Text = item.ResidualRate.ToString("#,0.##")+"%";
                maintenancetimes.Text = item.MaintenanceTimes.ToString();
                remark.Text = item.Remark;
                switch (item.IsCancel)
                {
                    case false:
                        {
                            iscancel.Text = "否";
                            break;
                        }
                    case true:
                        {
                            iscancel.Text = "是";
                            break;
                        }
                    default: break;

                }

                if (item.EquipmentNO != null && item.EquipmentNO != string.Empty)
                {
                    equipmentpic.ImageUrl = string.Format("~/Module/FM2E/DeviceManager/BarCode/BarCodeImage.aspx?data={0}&type={1}&cp={2}", item.EquipmentNO, "CODE128B", item.Name);
                }
                else
                    equipmentpic.ImageUrl = "~/images/noequipment.jpg";

                if (item.PhotoUrl != null && item.PhotoUrl != "")
                {
                    if(System.IO.File.Exists(Server.MapPath( item.PhotoUrl)))
                        Image1.ImageUrl = item.PhotoUrl;
                    else
                        Image1.ImageUrl = "~/images/nopicture.gif";
                }
                else Image1.ImageUrl = "~/images/nopicture.gif";

                ViewState["NextSplitNO"] = item.NextSplitNO;
                ViewState["EquipmentNO"] = item.EquipmentNO;
                /**********************
                打印条形码所需的信息
                *********/
                BarCodeInfo[] barCodes = new BarCodeInfo[1];
                barCodes[0] = new BarCodeInfo();
                barCodes[0].BarCode = item.EquipmentNO;
                barCodes[0].CompanyName = item.CompanyName;//"路达高速公路";
                barCodes[0].EquipmentName = item.Name;
                Session[Constants.BARCODE_SESSION_STRING] = barCodes;    //打印条形码时所需要的信息

                //相关设备
                //By L 4-26 屏蔽仓库相关设备拆分选项*************************************************************
                /*
                Repeater_RelatedDevice.DataSource = item.ChildrenList;
                Repeater_RelatedDevice.DataBind();
                if (item.ChildrenList.Count == 0)
                {
                    Repeater_RelatedDevice.Visible = false;
                }

                if (item.BasicEquipment!=null && item.EquipmentNO != item.BasicEquipment.EquipmentNO)
                {
                    div_basicequipment.Visible = false;      //By L 4-26 屏蔽
                    HyperLink_BasicDevice.Text = item.BasicEquipment.EquipmentNO + "  " + item.BasicEquipment.Name + "  " + item.BasicEquipment.Model;
                    HyperLink_BasicDevice.NavigateUrl = "ViewDeviceInfo.aspx?cmd=view&id=" + item.BasicEquipment.EquipmentID + "&companyid=" + companyid;
                }
                else
                {
                    div_basicequipment.Visible = false;
                }
                 */
                //************************************************************************************88
                 
                //维修记录
                FillMaintainRecord();
                FillDailyPatrolRecord();
                FillRoutineInspectRecord();
                FillRoutineMaintainRecord();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", ex.Message, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }

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
    /// <summary>
    /// 日常巡查
    /// </summary>
    private void FillDailyPatrolRecord()
    {
        try
        {
            int recordCount = 0;
            string equipmentNO = (string)ViewState["EquipmentNO"];
            //QueryParam dailyPatrolRecordsearchTerm = maintainPlanRecordBll.GenerateSearchTerm1("", 0, equipmentNO,MaintainPlanType.DailyPatrol);
            //dailyPatrolRecordsearchTerm.PageIndex = AspNetPager2.CurrentPageIndex;
            //dailyPatrolRecordsearchTerm.PageSize=AspNetPager2.PageSize;
            //IList dailyPatrolRecordlist = maintainPlanRecordBll.GetList(dailyPatrolRecordsearchTerm, out recordCount);
            MaintainSheetEquipmentSearchInfo info = new MaintainSheetEquipmentSearchInfo();
            info.EquipmentNO = equipmentNO;
            info.MaintainType = MaintainType.DailyPatrol;
            IList list = maintainBll.GetDeviceMaintainRecord(info, AspNetPager2.CurrentPageIndex, AspNetPager2.PageSize, out recordCount);
            AspNetPager2.RecordCount = recordCount;
            GridView2.DataSource = list;
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "获取日常巡查记录失败：" + ex.Message);
            Label_DailyPatrolRecordError.Visible = true;
        }
    }
    /// <summary>
    /// 例行检测
    /// </summary>
    private void FillRoutineInspectRecord()
    {
        try
        {
            int recordCount = 0;
            string equipmentNO = (string)ViewState["EquipmentNO"];

            //QueryParam routineInspectRecordsearchTerm = maintainPlanRecordBll.GenerateSearchTerm1("", 0, equipmentNO,MaintainPlanType.RoutineInspect);
            //routineInspectRecordsearchTerm.PageIndex = AspNetPager4.CurrentPageIndex;
            //routineInspectRecordsearchTerm.PageSize = AspNetPager4.PageSize;
            //IList routineInspectRecordlist = maintainPlanRecordBll.GetList(routineInspectRecordsearchTerm, out recordCount);
            MaintainSheetEquipmentSearchInfo info = new MaintainSheetEquipmentSearchInfo();
            info.EquipmentNO = equipmentNO;
            info.MaintainType = MaintainType.RoutineInspect;
            IList list = maintainBll.GetDeviceMaintainRecord(info, AspNetPager4.CurrentPageIndex, AspNetPager4.PageSize, out recordCount);

            AspNetPager4.RecordCount = recordCount;
            GridView4.DataSource = list;
            GridView4.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "获取例行检测记录失败：" + ex.Message);
            Label_RoutineInspectRecordError.Visible = true;
        }
    }
    /// <summary>
    /// 例行保养
    /// </summary>
    private void FillRoutineMaintainRecord()
    {
        try
        {
            int recordCount = 0;
            string equipmentNO = (string)ViewState["EquipmentNO"];

            //QueryParam routineMaintainRecordsearchTerm = maintainPlanRecordBll.GenerateSearchTerm1("", 0, equipmentNO,MaintainPlanType.RoutineMaintain);
            //routineMaintainRecordsearchTerm.PageIndex = AspNetPager3.CurrentPageIndex;
            //routineMaintainRecordsearchTerm.PageSize = AspNetPager3.PageSize;
            //IList routineMaintainRecordlist = maintainPlanRecordBll.GetList(routineMaintainRecordsearchTerm, out recordCount);
            MaintainSheetEquipmentSearchInfo info = new MaintainSheetEquipmentSearchInfo();
            info.EquipmentNO = equipmentNO;
            info.MaintainType = MaintainType.RoutineMaintain;
            IList list = maintainBll.GetDeviceMaintainRecord(info, AspNetPager3.CurrentPageIndex, AspNetPager3.PageSize, out recordCount);

            AspNetPager3.RecordCount = recordCount;
            GridView3.DataSource = list;
            GridView3.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "获取例行保养记录失败：" + ex.Message);
            Label_RoutineMaintainRecordError.Visible = true;
        }
    }

    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";
        }
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
    /// 分页控件换页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        TabContainer1.ActiveTabIndex = 3;
        FillDailyPatrolRecord();
    }
    /// <summary>
    /// 分页控件换页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager3_PageChanged(object sender, EventArgs e)
    {
        TabContainer1.ActiveTabIndex = 4;
        FillRoutineMaintainRecord();
    }
    /// <summary>
    /// 分页控件换页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager4_PageChanged(object sender, EventArgs e)
    {
        TabContainer1.ActiveTabIndex = 5;
        FillRoutineInspectRecord();
    }
}
