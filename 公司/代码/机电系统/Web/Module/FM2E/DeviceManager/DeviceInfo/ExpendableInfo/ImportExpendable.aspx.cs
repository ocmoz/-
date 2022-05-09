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
using FM2E.Model.Exceptions;
using System.IO;

public partial class Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_ImportExpendable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Button_Import_Click(object sender, EventArgs e)
    {
        string UPLOADFOLDER = "ImportExpendable/";

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

            IList list = null;
            try
            {
                list = ImportExpendable(file);
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
                Expendable bll = new Expendable();
                foreach (ExpendableInOutRecordInfo record in list)
                {
                    if (record.Type == ExpendableInOutRecordType.In)
                    {
                        bll.ExpendableInWarehouse(UserData.CurrentUserData.CompanyID, record);
                    }
                    else if (record.Type == ExpendableInOutRecordType.Out)
                    {
                        bll.ExpendableOutWarehouse(UserData.CurrentUserData.CompanyID, record);
                    }
                    count++;
                }
                success = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "装载数据失败" + ex.Message, Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
                return;
            }
            if (success)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "导入成功：" + count, Icon_Type.OK, false, Common.GetHomeBaseUrl("Expendable.aspx"), UrlType.Href, "");
            }
        }

    }

    /// <summary>
    /// 返回易耗品列表
    /// </summary>
    /// <param name="csvfile"></param>
    /// <param name="companyid"></param>
    /// <param name="warehouseid"></param>
    /// <returns></returns>
    public IList ImportExpendable(string csvfile)
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

        IList list = new List<ExpendableInOutRecordInfo>();
        int l = 1;
        try
        {

            for (; l < lines.Length; l++)
            {
                //参数分割
                string[] args = lines[l].Split(',');


                //EventMessage.EventWriteLog(WebUtility.Components.Msg_Type.Debug, "行:" + lines[l]);
                if (args.Length < 16)
                    continue;

                int i = 0;

                if (args[i++].Trim() == string.Empty)
                {
                    //break;
                }

                string warehouseid = args[i++].Trim();  //仓库ID
                if (warehouseid == string.Empty)
                {
                    break;
                }
                WarehouseInfo whinfo = warehouseHt[warehouseid] as WarehouseInfo;//仓库详细信息
                if (whinfo == null)
                    throw new WebException("未能找到仓库：" + whinfo.Name + "，编号：" + whinfo.WareHouseID);

                string warehousekeeper = args[i++].Trim(); //warehousekeeper

                string warehousekeepername = args[i++].Trim(); //warehousekeepername

                string receiver = args[i++].Trim();  //receiver

                string receivername = args[i++].Trim();  //receivername

                DateTime inouttime = Convert.ToDateTime(args[i++].Trim());  //inouttime

                string name = args[i++].Trim();//名称

                string model = args[i++].Trim();//模型

                decimal amountin = 0;
                if (args[i].Trim() == string.Empty)
                {
                    amountin = 0;
                    i++;
                }
                else
                {
                    amountin = decimal.Parse(args[i++].Trim());//入库数量
                }

                decimal amountout = 0;//出库数量
                if (args[i].Trim() == string.Empty)
                {
                    amountout = 0;
                    i++;
                }
                else
                {
                    amountout = decimal.Parse(args[i++].Trim());//出库数量
                }

                string unit = args[i++].Trim();//单位

                decimal price = 0;
                try
                {
                    price = decimal.Parse(args[i++].Trim());  //价格
                }
                catch (Exception pe)
                {

                }

                string categoryidstr = args[i++].Trim();

                long categroyid = long.Parse(categoryidstr);  //种类
                CategoryInfo cainfo = categoryHt[categroyid] as CategoryInfo;//种类详细信息
                if (cainfo == null)
                    throw new WebException("未能找到种类：" + cainfo.CategoryName + "，编号：" + cainfo.CategoryID);

                string remark = args[i++].Trim();

                string companyid = args[i++].Trim();  //公司
                CompanyInfo cpinfo = companyHt[companyid] as CompanyInfo;//公司信息
                if (cpinfo == null)
                    throw new WebException("未能找到公司：" + cpinfo.CompanyName + "，编号：" + cpinfo.CompanyID);

                if (amountin > 0)
                {
                    ExpendableInOutRecordInfo recordin = new ExpendableInOutRecordInfo();

                    recordin.Type = ExpendableInOutRecordType.In;
                    recordin.WarehouseID = warehouseid;
                    recordin.WarehouseKeeper = warehousekeeper;
                    recordin.WarehouseKeeperName = warehousekeepername;
                    recordin.Receiver = receiver;
                    recordin.ReceiverName = receivername;
                    recordin.InOutTime = inouttime;
                    recordin.Name = name;
                    recordin.Model = model;
                    recordin.Amount = amountin;
                    recordin.Unit = unit;
                    recordin.Price = price;
                    recordin.CategoryID = categroyid;
                    recordin.Remark = remark;
                    recordin.CompanyID = companyid;

                    list.Add(recordin);
                }
                if (amountout > 0)
                {
                    ExpendableInOutRecordInfo recordout = new ExpendableInOutRecordInfo();

                    recordout.Type = ExpendableInOutRecordType.Out;
                    recordout.WarehouseID = warehouseid;
                    recordout.WarehouseKeeper = warehousekeeper;
                    recordout.WarehouseKeeperName = warehousekeepername;
                    recordout.Receiver = receiver;
                    recordout.ReceiverName = receivername;
                    recordout.InOutTime = inouttime;
                    recordout.Name = name;
                    recordout.Model = model;
                    recordout.Amount = amountout;
                    recordout.Unit = unit;
                    recordout.Price = price;
                    recordout.CategoryID = categroyid;
                    recordout.Remark = remark;
                    recordout.CompanyID = companyid;

                    list.Add(recordout);
                }

            }
        }
        catch (Exception e)
        {
            System.Console.Write("error line:" + l);
        }
        return list;
    }

}
