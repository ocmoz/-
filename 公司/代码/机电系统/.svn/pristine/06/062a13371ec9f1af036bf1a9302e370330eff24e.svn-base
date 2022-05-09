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
using System.Collections.Generic;
using FM2E.Model.Equipment;
using FM2E.Model.Basic;
using FM2E.Model.Exceptions;
using WebUtility;
using FM2E.Model.Utils;
using FM2E.SQLServerDAL.Equipment;
using System.IO;
using WebUtility.Components;
using FM2E.BLL.Basic;
using FM2E.BLL.Utils;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_InWarehouse_Impotwarehouse : System.Web.UI.Page
{

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

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button_Import_Click(object sender, EventArgs e)
    {
        string UPLOADFOLDER = "Importwarehouse/";

        if (FileUpload_ImportDevice.HasFile)
        {

            //附件处理
            FileUpLoadCommon fuc = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + UPLOADFOLDER, false);
            string file = "", oldfile = "";
            if (FileUpload_ImportDevice.HasFile)
            {
                if (fuc.SaveFile(FileUpload_ImportDevice, false))
                {
                    file = SystemConfig.Instance.UploadPath + UPLOADFOLDER + "/" + fuc.NewFileName;
                }
                else
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "上传失败", new WebException(fuc.ErrorMsg), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                    return;
                }
            }
            oldfile = file;
            file = Server.MapPath(file);


            //EventMessage.MessageBox(Msg_Type.Error, "操作失败", "上传失败"+systemid, Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");

            InWarehouseInfo item = null;
            try
            {
                item = ImportExpendable(file);
                FileUpLoadCommon.DeleteFile(oldfile);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "导入失败" + ex.Message, Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
                return;
            }

            bool success = false;
            int count = 0;
            try
            {
                InWarehouse bll = new InWarehouse();
                bll.InsertInWarehouseWithDetail(item);
                count = item.InWarehouseList.Count;
                success = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "装载数据失败" + ex.Message, Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
                return;
            }
            if (success)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "导入成功：" + count, Icon_Type.OK, false, Common.GetHomeBaseUrl("InWarehouse.aspx"), UrlType.Href, "");
            }
        }
    }

    /// <summary>
    /// 返回备品备件列表
    /// </summary>
    /// <param name="csvfile"></param>
    /// <param name="companyid"></param>
    /// <param name="warehouseid"></param>
    /// <returns></returns>
    public InWarehouseInfo ImportExpendable(string csvfile)
    {
        //打开文件
        //FileStream fs = File.OpenRead(csvfile);
        string[] lines = File.ReadAllLines(csvfile, System.Text.Encoding.Default);
        //所有种类
        Category categoryBll = new Category();
        IList<CategoryInfo> categoryList = categoryBll.GetAllCategory();
        Hashtable categoryHt = new Hashtable(categoryList.Count);
        foreach (CategoryInfo ca in categoryList)
        {
            categoryHt.Add(ca.CategoryID, ca);
        }
        //所有仓库
        Warehouse warehouseBll = new Warehouse();
        IList<WarehouseInfo> warehouseList = warehouseBll.GetAllWarehouse();
        Hashtable warehouseHt = new Hashtable(warehouseList.Count);
        foreach (WarehouseInfo wh in warehouseList)
        {
            warehouseHt.Add(wh.WareHouseID, wh);
        }
        //所有公司
        Company companyBll = new Company();
        IList<CompanyInfo> companyList = companyBll.GetAllCompany();
        Hashtable companyHt = new Hashtable(companyList.Count);
        foreach (CompanyInfo cp in companyList)
        {
            companyHt.Add(cp.CompanyID, cp);
        }

        InWarehouseInfo item = new InWarehouseInfo();

        IList list = new List<InEquipmentsInfo>();
        int l = 1;
        try
        {

            for (; l < lines.Length; l++)
            {
                //参数分割
                string[] args = lines[l].Split('\t');


                //EventMessage.EventWriteLog(WebUtility.Components.Msg_Type.Debug, "行:" + lines[l]);
                if (args.Length < 13)
                    continue;

                int i = 0;

                if (args[i].Trim() == string.Empty)
                {
                    break;
                }

                string warehouseid = args[i++].Trim();  //仓库ID
                WarehouseInfo whinfo = warehouseHt[warehouseid] as WarehouseInfo;//仓库详细信息
                if (whinfo == null)
                    throw new WebException("未能找到仓库：" + whinfo.Name + "，编号：" + whinfo.WareHouseID);

                string warehousekeeper = args[i++].Trim(); //warehousekeeper

                string warehousekeepername = args[i++].Trim(); //warehousekeepername

                string receiver = args[i++].Trim();  //receiver

                string receivername = args[i++].Trim();  //receivername

                DateTime inouttime = Convert.ToDateTime(args[i++].Trim());  //inouttime

                string name = args[i++].Trim();//名称

                decimal amountin = decimal.Parse(args[i++].Trim());//入库数量

                decimal amountout = decimal.Parse(args[i++].Trim());//出库数量

                string unit = args[i++].Trim();//单位

                string price = args[i++].Trim();  //价格

                string categroyid = args[i++].Trim();  //种类

                string companyid = args[i++].Trim();  //公司
                CompanyInfo cpinfo = companyHt[companyid] as CompanyInfo;//种类详细信息
                if (cpinfo == null)
                    throw new WebException("未能找到公司：" + cpinfo.CompanyName + "，编号：" + cpinfo.CompanyID);

                if (amountin > 0)
                {
                    InEquipmentsInfo equimentinfo = new InEquipmentsInfo();

                    equimentinfo.Count = amountin;
                    equimentinfo.IsAsset = true;
                    equimentinfo.EquipmentNO = categroyid;
                    equimentinfo.ExpendableID = 0;
                    equimentinfo.Unit = unit;
                    equimentinfo.InTime = inouttime;
                    equimentinfo.WarehouseID = warehouseid;
                    equimentinfo.Name = name;
                    equimentinfo.Model = price;

                    list.Add(equimentinfo);
                }

            }
            item.CompanyID = UserData.CurrentUserData.CompanyID;
            item.WarehouseID = CurrentWarehouse.WareHouseID;
            item.WarehouseAddressID = CurrentWarehouse.AddressID;
            item.WarehouseDetailLocation = "";
            item.SubmitTime = DateTime.Now;
            item.ApplicantID = UserData.CurrentUserData.UserName;
            item.OperatorID = Common.Get_UserName;
            item.Remark = "";
            item.IsDeleted = false;
            item.InWarehouseList = list;
            item.SheetName = SheetNOGenerator.GetSheetNO(UserData.CurrentUserData.CompanyID, SheetType.INWAREHOUSESHEET);
            item.DepartmentID = UserData.CurrentUserData.DepartmentID;
            
        }
        catch (Exception e)
        {
            System.Console.Write("error line:" + l);
        }
        return item;
    }
}
