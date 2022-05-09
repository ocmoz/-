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
using FM2E.Model.Utils;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.BLL.System;
using FM2E.Model.System;
using System.Collections.Generic;
using FM2E.Model.Exceptions;
using System.Data.OleDb;
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;

public partial class Module_FM2E_DeviceManager_DeviceInfo_WareConsumableEquipmentManager_ImportConsumableEquipment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button_Import_Click(object sender, EventArgs e)
    {
        string UPLOADFOLDER = "ImportDevice/";

        if (FileUpload_ImportDevice.HasFile)
        {

            //附件处理
            FileUpLoadCommon fuc = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + UPLOADFOLDER, false);
            string file = "";
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

            file = Server.MapPath(file);


            //EventMessage.MessageBox(Msg_Type.Error, "操作失败", "上传失败"+systemid, Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");

            IList list = null;
            try
            {
                DataSet ds = ImportXlsToData(file);
                list = AddDataToObject(ds);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "导入失败" + ex.Message, Icon_Type.Error, false, "window.history.go(-1)", UrlType.JavaScript, "");
                return;
            }

            ConsumableEquipment bll = new ConsumableEquipment();

            foreach (EquipmentInfo eq in list)
            {
                //查找是否存在
                ConsumableEquipmentInfo searchitem = new ConsumableEquipmentInfo();
                searchitem.Name = eq.Name;
                searchitem.Model = eq.Model;
                IList<ConsumableEquipmentInfo> searchlist = bll.Search(searchitem);

                ConsumableEquipmentDetailInfo detail = new ConsumableEquipmentDetailInfo();
                detail.CompanyID = eq.CompanyID;
                detail.AddressID = eq.AddressID;
                detail.AddressName = eq.AddressName;
                detail.Count = eq.Count;
                detail.DetailLocation = eq.DetailLocation;
                detail.Remark = eq.Remark;

                ArrayList detaillist = new ArrayList();
                detaillist.Add(detail);

                if (searchlist == null || searchlist.Count == 0)
                {
                    //生成设备配套设施的编号，与设备编号对应。
                    DateTime date = Convert.ToDateTime("2011-1-1");
                    searchitem.ConsumableEquipmentNO = FM2E.BLL.BarCode.BarCode.GenerateBarCode("YH", date, false);
                    searchitem.SystemID = eq.SystemID;
                    searchitem.SerialNum = eq.SerialNum;
                    searchitem.Model = eq.Model;
                    searchitem.Specification = eq.Specification;
                    searchitem.AssertNumber = eq.AssertNumber;
                    searchitem.Unit = eq.Unit;
                    searchitem.Count = eq.Count;
                    searchitem.Price = eq.Price;
                    //item.SupplierID = Convert.ToInt64(tbSupplierID.Text.Trim());
                    //item.ProducerID = Convert.ToInt64(tbProducerID.Text.Trim());
                    //item.SupplierName = tbSupplierName.Text.Trim();
                    //item.ProducerName = tbProducerName.Text.Trim();
                    searchitem.FileDate = DateTime.Now;
                    searchitem.MaintenanceTimes = Convert.ToInt32(eq.MaintenanceTimes);
                    searchitem.Remark = eq.Remark;
                    searchitem.UpdateTime = DateTime.Now;
                    searchitem.ConsumableEquipmentDetailList = detaillist;

                    //不存在，增加
                    bll.InsertConsumableEquipment(searchitem);
                }
                else
                {
                    ConsumableEquipmentInfo conitem = bll.GetConsumableEquipment(searchlist[0].ConsumableEquipmentID);
                    conitem.Count += eq.Count;
                    int markcount = 0;
                    foreach (ConsumableEquipmentDetailInfo detailtempitem in conitem.ConsumableEquipmentDetailList)
                    {
                        if (detailtempitem.AddressID == eq.AddressID && ((detailtempitem.DetailLocation == null && eq.DetailLocation == "") || detailtempitem.DetailLocation == eq.DetailLocation))
                        {
                            break;
                        }
                        else
                        {
                            markcount++;
                        }
                    }
                    if (markcount == conitem.ConsumableEquipmentDetailList.Count)
                    {
                        conitem.ConsumableEquipmentDetailList.Add(detail);
                    }
                    else
                    {
                        ((ConsumableEquipmentDetailInfo)conitem.ConsumableEquipmentDetailList[markcount]).Count += eq.Count;
                    }
                    //存在，更新
                    bll.UpdateConsumableEquipment(conitem);
                }
            }

            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "导入成功：" + list.Count, Icon_Type.OK, false, Common.GetHomeBaseUrl("ConsumableEquipment.aspx"), UrlType.Href, "");
        }

    }

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

        //系统
        EquipmentSystem systemBll = new EquipmentSystem();
        IList sysList = systemBll.GetAllSystem();
        Hashtable sysHt = new Hashtable(sysList.Count);
        foreach (EquipmentSystemInfo sys in sysList)
        {
            sysHt.Add(sys.SystemName, sys);
        }

        //公司
        Company bllcompany = new Company();
        IList comlist = (List<CompanyInfo>)bllcompany.GetAllCompany();
        Hashtable comHt = new Hashtable(comlist.Count);
        foreach (CompanyInfo com in comlist)
        {
            comHt.Add(com.CompanyName, com);
        }


        //设备
        Equipment eqbll = new Equipment();
        try
        {
            ArrayList list = new ArrayList();
            int ic, ir;
            ic = ds.Tables[0].Columns.Count;
            if (ds.Tables[0].Columns.Count < 10)
            {
                throw new Exception("导入Excel格式错误！Excel只有" + ic.ToString() + "列");
            }
            ir = ds.Tables[0].Rows.Count;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    EquipmentInfo record = new EquipmentInfo();


                    int index = 0;  //0序号
                    string name = "";  //1名称
                    string model = "";  //2型号
                    string unit = "";  //3单位
                    string categroyname = "";   //种类详细信息
                    CategoryInfo cainfo = new CategoryInfo();
                    decimal unitprice = 0;  //单价
                    int count = 1;  //数量
                    string addressfullname = "";//地址
                    AddressInfo addinfo = new AddressInfo();
                    long addressid = 0;
                    string addcode = "";
                    string detaillocation = "";  //详细地址
                    string assertnumber = "";//资产编号
                    DateTime puchasetime = DateTime.Now;//采购时间；
                    string sn = "";//序列号
                    string remark = "";//备注
                    string companyname = "";
                    string companyid = "";  //公司id
                    CompanyInfo cominfo = new CompanyInfo();
                    string systemname = "";
                    string systemid = "";  //系统
                    EquipmentSystemInfo eqsysinfo = new EquipmentSystemInfo();


                    if (ds.Tables[0].Rows[i][0].ToString().Trim() != "")  //序号(无用)
                    {
                        index = Convert.ToInt32(ds.Tables[0].Rows[i][0].ToString().Trim());
                    }

                    if (ds.Tables[0].Rows[i][1].ToString().Trim() != "")  //设备名称
                    {
                        name = ds.Tables[0].Rows[i][1].ToString().Trim();
                    }
                    else
                    {
                        break;  //结束条件
                    }

                    if (ds.Tables[0].Rows[i][2].ToString() != "")  //设备型号
                    {
                        model = ds.Tables[0].Rows[i][2].ToString().Trim();
                    }
                    else
                    {
                    }

                    if (ds.Tables[0].Rows[i][3].ToString() != "")  //单位
                    {
                        unit = ds.Tables[0].Rows[i][3].ToString().Trim();
                    }
                    else
                    {
                    }

                    if (ds.Tables[0].Rows[i][4].ToString() != "")  //种类详细信息
                    {
                        categroyname = ds.Tables[0].Rows[i][4].ToString().Trim();
                        cainfo = categoryHt[categroyname] as CategoryInfo;//种类详细信息
                        if (cainfo == null)
                            throw new WebException("未能找到种类：" + categroyname);
                    }
                    else
                    {
                        throw new WebException("未能找到种类：" + categroyname);
                    }

                    if (ds.Tables[0].Rows[i][5].ToString() != "")  //单价
                    {
                        unitprice = decimal.Parse(ds.Tables[0].Rows[i][5].ToString().Trim());
                    }
                    else
                    {
                    }

                    if (ds.Tables[0].Rows[i][6].ToString() != "")  //数量
                    {
                        count = int.Parse(ds.Tables[0].Rows[i][6].ToString().Trim());
                    }
                    else
                    {
                    }

                    if (ds.Tables[0].Rows[i][7].ToString() != "")  //资产编号
                    {
                        assertnumber = ds.Tables[0].Rows[i][7].ToString().Trim();
                    }
                    else
                    {
                    }

                    if (ds.Tables[0].Rows[i][8].ToString() != "")  //序列号
                    {
                        sn = ds.Tables[0].Rows[i][8].ToString().Trim();
                    }
                    else
                    {
                    }

                    if (ds.Tables[0].Rows[i][9].ToString() != "")  //地址
                    {
                        addressfullname = ds.Tables[0].Rows[i][9].ToString().Trim();
                        addinfo = addressHt[addressfullname] as AddressInfo;//地址详细信息
                        if (addinfo == null)
                            throw new WebException("未能找到地址：" + addressfullname);
                        addressid = addinfo.ID;
                        addcode = addinfo.AddressCode;
                    }
                    else
                    {
                        throw new WebException("未能找到地址：" + addressfullname);
                    }

                    if (ds.Tables[0].Rows[i][10].ToString() != "")  //安装详细地址
                    {
                        detaillocation = ds.Tables[0].Rows[i][10].ToString().Trim();
                    }
                    else
                    {
                    }

                    if (ds.Tables[0].Rows[i][11].ToString() != "")  //系统
                    {
                        systemname = ds.Tables[0].Rows[i][11].ToString().Trim();  //系统名称
                        eqsysinfo = sysHt[systemname] as EquipmentSystemInfo;
                        if (eqsysinfo == null)
                            throw new WebException("未能找到系统：" + systemname);
                        systemid = eqsysinfo.SystemID;
                    }
                    else
                    {
                    }

                    if (ds.Tables[0].Rows[i][12].ToString() != "")  //公司
                    {
                        companyname = ds.Tables[0].Rows[i][12].ToString().Trim();  //公司名称
                        cominfo = comHt[companyname] as CompanyInfo;
                        if (cominfo == null)
                            throw new WebException("未能找到公司：" + companyname);
                        companyid = cominfo.CompanyID;
                    }
                    else
                    {
                    }

                    if (ds.Tables[0].Rows[i][13].ToString() != "")  //采购时间
                    {
                        puchasetime = DateTime.Parse(ds.Tables[0].Rows[i][13].ToString().Trim());
                    }
                    else
                    {
                    }

                    if (ds.Tables[0].Rows[i][14].ToString() != "")  //备注
                    {
                        remark = ds.Tables[0].Rows[i][14].ToString().Trim();
                    }
                    else
                    {
                    }

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
                        eq.Price = unitprice;
                        eq.Remark = remark;
                        eq.Status = EquipmentStatus.Normal;  //正常
                        eq.SystemID = systemid;
                        eq.UpdateTime = DateTime.Now;
                        eq.DetailLocation = detaillocation;
                        eq.Unit = unit;
                        eq.Count = 1;
                        //以下为默认参数
                        eq.PurchaseOrderID = "";

                        //验证该设备是否已经存在，易耗品类不考虑
                        if (eq.CategoryName == "易耗品")
                        {
                            list.Add(eq);
                        }
                        else  //非易耗品类
                        {
                            EquipmentSearchInfo eqsearch = new EquipmentSearchInfo();
                            eqsearch.Name = name;  //设备名称
                            eqsearch.Model = model; //设备型号
                            eqsearch.AddressCode = addcode;  //设备地址信息
                            eqsearch.DetailLocation = detaillocation;  //设备安装位置
                            eqsearch.Remark = remark;  //备注说明

                            IList<EquipmentInfoFacade> searchlist = eqbll.Search(eqsearch);
                            if (searchlist == null || searchlist.Count > 0)
                            {
                                list.Clear();
                                throw new Exception("。导入设备数据重复，请检查设备数据信息！<br>设备重复项--名称：" + name + "，型号：" + model + "，地址：" + addinfo.AddressFullName + "，安装位置：" + detaillocation);
                            }
                            else
                            {
                                list.Add(eq);
                            }
                        }
                    }
                }
                return list;
            }
            else
            {
                list.Clear();
                throw new Exception("导入数据为空或数据格式不符合！");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
