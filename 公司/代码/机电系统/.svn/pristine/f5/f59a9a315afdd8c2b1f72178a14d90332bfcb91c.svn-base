using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using FM2E.Model.Basic;
using FM2E.BLL.Basic;
using System.Collections;
using System.IO;
using FM2E.Model.Exceptions;
using WebUtility;
/// <summary>
///用于进行导入操作，直接返回相关结果
/// </summary>
public class ImportHelper
{
    public ImportHelper()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 返回设备的列表
    /// </summary>
    /// <param name="csvfile"></param>
    /// <param name="companyid"></param>
    /// <param name="systemid"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public static IList ImportEquipment(string csvfile,string companyid,string systemid,EquipmentStatus state)
    {
        //所有地址
        Address addressBll = new Address();
        IList addressList = addressBll.GetAllAddress();
        //HashTable
        Hashtable addressHt = new Hashtable(addressList.Count);
        foreach (AddressInfo add in addressList)
        {
            addressHt.Add(add.AddressFullName, add);
        }

        //所有种类
        Category categoryBll = new Category();
        IList<CategoryInfo> categoryList = categoryBll.GetAllCategory();
        Hashtable categoryHt = new Hashtable(categoryList.Count);
        foreach (CategoryInfo ca in categoryList)
        {
            categoryHt.Add(ca.CategoryName, ca);
        }

        //打开文件
        //FileStream fs = File.OpenRead(csvfile);
        string[] lines = File.ReadAllLines(csvfile, System.Text.Encoding.Unicode);


        IList list = new List<EquipmentInfo>();

        for (int l = 1; l < lines.Length;l++ )
        {
            //参数分割
            string[] args = lines[l].Split(',');


            //EventMessage.EventWriteLog(WebUtility.Components.Msg_Type.Debug, "行:" + lines[l]);
            if (args.Length < 13)
                continue;

            int i = 0;
            int index = int.Parse(args[i++].Trim());//0序号
            string name = args[i++].Trim();//1名称
            string model = args[i++].Trim();//2型号
            string unit = args[i++].Trim();//3单位

            string categroyname = args[i++].Trim();
            CategoryInfo cainfo = categoryHt[categroyname] as CategoryInfo;//种类详细信息

            if (cainfo == null)
                throw new WebException("未能找到种类：" + categroyname);

            decimal unitprice = decimal.Parse(args[i++].Trim());//单价

            int count = int.Parse(args[i++].Trim());//数量

            string addressfullname = args[i++].Trim();//地址

            AddressInfo addinfo = addressHt[addressfullname] as AddressInfo;//地址详细信息
            if (addinfo == null)
                throw new WebException("未能找到地址：" + addressfullname);

            long addressid = addinfo.ID;
            string addcode = addinfo.AddressCode;

            string detaillocation = args[i++].Trim();

            string assertnumber = args[i++].Trim();//资产编号

            DateTime puchasetime = DateTime.Parse(args[i++].Trim());//采购时间；

            string sn = args[i++].Trim();//序列号

            string remark = args[i++].Trim();//备注

            //标注是否测试数据


            for (int j = 0; j < count; j++)
            {

                EquipmentInfo eq = new EquipmentInfo();

                eq.AddressCode = addcode;
                eq.AddressID = addressid;
                eq.AddressName = addinfo.AddressFullName;
                eq.AssertNumber = assertnumber;
                eq.CategoryID = cainfo.CategoryID;
                eq.CategoryName = cainfo.CategoryName;
                eq.CompanyID = companyid;
                eq.DepreciableLife = cainfo.DepreciableLife;
                eq.DepreciationMethod = cainfo.DepreciationMethod;
                eq.FileDate = DateTime.Now;
                eq.Model = model;
                eq.Name = name;
                eq.OpeningDate = puchasetime;
                eq.PurchaseDate = puchasetime;
                eq.ResidualRate = cainfo.ResidualRate;
                eq.Remark = remark;
                eq.Status = state;
                eq.SystemID = systemid;
                eq.UpdateTime = DateTime.Now;
                eq.DetailLocation = detaillocation;
                //以下为默认参数
                eq.PurchaseOrderID = "";


                list.Add(eq);
            }
        }


      
        return list;
    }

    /// <summary>
    /// 返回易耗品列表
    /// </summary>
    /// <param name="csvfile"></param>
    /// <param name="companyid"></param>
    /// <param name="warehouseid"></param>
    /// <returns></returns>
    public static IList ImportExpendable(string csvfile, string companyid, string warehouseid)
    {
        //打开文件
        //FileStream fs = File.OpenRead(csvfile);
        string[] lines = File.ReadAllLines(csvfile, System.Text.Encoding.Unicode);


        //所有种类
        Category categoryBll = new Category();
        IList<CategoryInfo> categoryList = categoryBll.GetAllCategory();
        Hashtable categoryHt = new Hashtable(categoryList.Count);
        foreach (CategoryInfo ca in categoryList)
        {
            categoryHt.Add(ca.CategoryName, ca);
        }

        IList list = new List<ExpendableInfo>();

        for (int l = 1; l < lines.Length; l++)
        {
            //参数分割
            string[] args = lines[l].Split(',');


            //EventMessage.EventWriteLog(WebUtility.Components.Msg_Type.Debug, "行:" + lines[l]);
            if (args.Length < 5)
                continue;

            int i = 0;
            int index = int.Parse(args[i++].Trim());//0序号
            string name = args[i++].Trim();//1名称
            string model = "";// args[i++].Trim();//2型号
            string unit = args[i++].Trim();//3单位

            decimal price = decimal.Parse(args[i++].Trim());

            string remark = "(导入)";

            decimal storage = decimal.Parse(args[i++].Trim());//数量


            string categroyname = args[i++].Trim();
            CategoryInfo cainfo = categoryHt[categroyname] as CategoryInfo;//种类详细信息

            if (cainfo == null)
                throw new WebException("未能找到种类：" + categroyname);

            ExpendableInfo exp = new ExpendableInfo();

            exp.CompanyID = companyid;
            exp.Count = storage;
            exp.ExpendableID = 0;
            exp.Model = model;
            exp.Name = name;
            exp.Remark = remark;
            exp.Unit = unit;
            exp.UpdateTime = DateTime.Now;
            exp.WarehouseID = warehouseid;
            exp.Price = price;
            exp.CategoryID = cainfo.CategoryID;

            list.Add(exp);

        }



        return list;
    }



    //********** Modified by Xue    For V 3.1.1    2011-10-17 **********************************************************************************
    /// <summary>
    /// 批量导入仓库易耗品 并返回易耗品列表
    /// </summary>
    /// <param name="csvfile"></param>
    /// <returns></returns>
    public static IList ImportExpendable(string csvfile)
    {
        //打开文件
        //FileStream fs = File.OpenRead(csvfile);
        string[] lines = File.ReadAllLines(csvfile, System.Text.Encoding.Unicode);


        //所有种类
        Category categoryBll = new Category();
        IList<CategoryInfo> categoryList = categoryBll.GetAllCategory();
        Hashtable categoryHt = new Hashtable(categoryList.Count);
        foreach (CategoryInfo ca in categoryList)
        {
            categoryHt.Add(ca.CategoryName, ca);
        }

        IList list = new List<ExpendableInfo>();

        for (int l = 1; l < lines.Length; l++)
        {
            //参数分割
            string[] args = lines[l].Split(',');


            //EventMessage.EventWriteLog(WebUtility.Components.Msg_Type.Debug, "行:" + lines[l]);
            if (args.Length < 5)
                continue;

            int i = 0;
            //***************************************************************************************************************************************************
            //模板元素：序号，公司ID，仓库ID，名称，型号，规格，单位，价格，数量，种类，备注           （元素共11个，分别以“，”隔开，并保存为Unicode文本格式）
            //***************************************************************************************************************************************************
            int index = int.Parse(args[i++].Trim());//0序号
            string cId = args[i++].Trim();//1公司ID
            string wId = args[i++].Trim();//2仓库ID
            string name = args[i++].Trim();//3名称
            string model = args[i++].Trim();//4型号
            string specification = args[i++].Trim();//5规格
            string unit = args[i++].Trim();//6单位

            decimal price = decimal.Parse(args[i++].Trim());//7价格
            decimal storage = decimal.Parse(args[i++].Trim());//87数量

            string categroyname = args[i++].Trim();//9种类
            CategoryInfo cainfo = categoryHt[categroyname] as CategoryInfo;//种类详细信息

            if (cainfo == null)
                throw new WebException("未能找到种类：" + categroyname);

            string remark = args[i++].Trim();//10备注

            ExpendableInfo exp = new ExpendableInfo();

            exp.CompanyID = cId;
            exp.Count = storage;
            exp.ExpendableID = 0;
            exp.Model = model;
            exp.Name = name;
            exp.Remark = remark;
            exp.Unit = unit;
            exp.UpdateTime = DateTime.Now;
            exp.WarehouseID = wId;
            exp.Price = price;
            exp.CategoryID = cainfo.CategoryID;
            exp.Specification = specification;
            list.Add(exp);
            
        }



        return list;
    }
    //********** Modification Finished 2011-10-17 **********************************************************************************************

}
