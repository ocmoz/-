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

public partial class Module_FM2E_MaintainManager_ViewDeviceInfo_ViewDeviceInfo : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 32, 0, DataType.Long);
    string _equipmentno = (string)Common.sink("EquipmentNO", MethodType.Get, 50, 0, DataType.Str);

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);

        if (!IsPostBack)
        {
            Session.Remove(Constants.BARCODE_SESSION_STRING);
            BindData();
        }
    }

    private void BindData()
    {
        if (cmd == "view")
        {
            try
            {
                Equipment equipment = new Equipment();
                EquipmentInfoFacade item = equipment.GetEquipmentBYNO(_equipmentno);

                Session["EquipmentInfo" + id] = item;   //暂时记录下设备信息，以备编辑时使用

                equipmentno.Text = item.EquipmentNO;
                equipmentname.Text = item.Name;
                companyname.Text = item.CompanyName;
                sectionname.Text = item.SectionName;
                switch (item.LocationTag)
                {
                    case "1":
                        {
                            Channal channalbll = new Channal();
                            locationname.Text = channalbll.GetChannal(item.LocationID).ChannalName;
                            break;
                        }
                    case "2":
                        {
                            TollGate tollgate = new TollGate();
                            locationname.Text = tollgate.GetTollGate(item.LocationID).TollGateName;
                            break;
                        }
                    case "3":
                        {
                            break;
                        }
                    case "4":
                        {
                            Warehouse warehouse = new Warehouse();
                            locationname.Text = warehouse.GetWarehouse(item.LocationID).Name;
                            break;
                        }
                    default: break;

                }
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
                purchaseorderid.Text = item.PurchaseOrderID; ;
                producername.Text = item.SupplierName;
                shengchangshang.Text = item.ProducerName;
                purchasername.Text = item.PurchaserName;
                responsibilityname.Text = item.ResponsibilityName;
                checkername.Text = item.CheckerName;
                if (DateTime.Compare(item.PurchaseDate, DateTime.MinValue) != 0)
                    purchasedate.Text = item.PurchaseDate.ToLongDateString().ToString();
                if (DateTime.Compare(item.ExamDate, DateTime.MinValue) != 0)
                    examdate.Text = item.ExamDate.ToLongDateString().ToString();
                if (DateTime.Compare(item.OpeningDate, DateTime.MinValue) != 0)
                    openingdate.Text = item.OpeningDate.ToLongDateString().ToString();
                if (DateTime.Compare(item.FileDate, DateTime.MinValue) != 0)
                    filedate.Text = item.FileDate.ToLongDateString().ToString();
                if (DateTime.Compare(item.WarrantyDate, DateTime.MinValue) != 0)
                    warrantydate.Text = item.WarrantyDate.ToLongDateString().ToString();
                servicelife.Text = item.ServiceLife.ToString();
                if (DateTime.Compare(item.UpdateTime, DateTime.MinValue) != 0)
                    updatetime.Text = item.UpdateTime.ToLongDateString().ToString();
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
                    residualrate.Text = item.ResidualRate.ToString();
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
                BarCodeInfo[] barCodes = new BarCodeInfo[1];
                barCodes[0] = new BarCodeInfo();
                barCodes[0].BarCode = item.EquipmentNO;
                barCodes[0].CompanyName = item.CompanyName;//"路达高速公路";
                barCodes[0].EquipmentName = item.Name;
                Session[Constants.BARCODE_SESSION_STRING] = barCodes;    //打印条形码时所需要的信息
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", ex.Message, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }
}
