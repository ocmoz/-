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
using FM2E.BLL.EmployeesInfo;
public partial class Module_FM2E_Report_Import_EmployeeImport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button_Import_Click(object sender, EventArgs e)
    {
        string UPLOADFOLDER = "ImportEmployee/";

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

            EmployeesInfo bll = new EmployeesInfo();

            foreach (EmployeesInfomodel eq in list)
            {
                bll.InsertEmployeesInfo(eq); //插入员工数据
                
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "导入成功：" + list.Count, Icon_Type.OK, false, Common.GetHomeBaseUrl("../Import/EmployeeImport.aspx"), UrlType.Href, "");
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
        /*
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
        */

        //员工
        EmployeesInfomodel eqbll = new EmployeesInfomodel();
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
                    EmployeesInfomodel record = new EmployeesInfomodel();
                    int index = 0;            //序号
                    string stationname = "";
                    int infyear = DateTime.Now.Year;          //
                    int infmonth = DateTime.Now.Month;          //
                    string company = "";                //公司
                    string department = "";             //部门
                    string orggrade = "";               //组织级别
                    string orgcode = "";                //组织代码
                    string name = "";                   //姓名
                    string empcode = "";                //职员代码
                    string positionname = "";          //职位
                    int sex = 0;                        //性别
                    DateTime birth = DateTime.Now;          //出生日期
                    string idnum = "";                //身份证号
                    int age = 0;                 //年龄
                    string nation = "";          //民族
                    string nativeplace = "";          //籍贯
                    int status = 0;          //政治面貌
                    int education = 0;          //最高学历
                    string school = "";          //毕业院校
                    string major = "";          //专业
                    string empstatus = "";          //员工状态
                    string emptype = "";              //员工类别
                    string retiretype = "";          //离退休类别
                    string resigntype = "";          //辞职类别
                    string dismisstype = "";          //辞退类别
                    DateTime companydate = DateTime.Now;          //入司日期
                    int groupage = 0;          //集团服务年限
                    int companyage = 0;          //公司服务年限
                    int deptage = 0;          //部门服务年限
                    DateTime regulardate = DateTime.Now;          //入职日期
                    DateTime hiredate = DateTime.Now;          //转正日期
                    DateTime retiredate = DateTime.Now;          //离退日期
                    string posttype = "";          //职务类型
                    string post = "";          //职务
                    string postcode = "";          //职位代码
                    string rankname = "";          //职级名称
                    string gradename = "";          //职等名称
                    string tittletype = "";          //职称类别
                    string tittlename = "";          //职称名称
                    string tittlegrade = "";          //职称级别
                    DateTime tittledate = DateTime.Now;          //职称授予时间
                    string insurnum = "";          //社会保险账号
                    string bankname = "";          //银行名称
                    string barnknum = "";          //银行账号
                    DateTime workdate = DateTime.Now;          //参加工作时间
                    int workyear = 0;          //入司前连续工作时间（年）
                    string nowmajor = "";          //现从事专业
                    string formername = "";          //曾用名
                    float height = 0;          //身高
                    string blood = "";          //血型
                    int marriage = 0;          //婚姻状况
                    string health = "";          //健康状况
                    string family = "";          //家庭出身
                    string personal = "";          //个人成份
                    string idaddress = "";          //身份证地址
                    string birthplace = "";          //出生地
                    string address = "";          //地址
                    string postalcode = "";          //邮政编码
                    string telephone = "";          //常用电话
                    string mobilephone = "";          //移动电话
                    string email = "";          //EMail地址
                    string isparttime = "";          //是否兼职
                    DateTime startparttime = DateTime.Now;          //兼职开始日期
                    DateTime endparttime = DateTime.Now;          //兼职结束日期
                    string isformation = "";          //是否占兼职职位编制
                    string isunit = "";          //是否占兼职组织单元人数
                    string istrial = "";          //是否处于试用考察期
                    DateTime starttrial = DateTime.Now;          //试用考察期开始日期
                    DateTime endtrial = DateTime.Now;          //试用考察期转正日期
                    string residence = "";          //户口所在地
                    DateTime submittime = DateTime.Now;         //
                    DateTime submitname = DateTime.Now;


                                //序号
                    if (ds.Tables[0].Rows[i][0].ToString().Trim() != "")  
                    {
                        index = Convert.ToInt32(ds.Tables[0].Rows[i][0].ToString().Trim());
                    }            
                                 //公司
                    if (ds.Tables[0].Rows[i][1].ToString().Trim() != "")
                    {
                        company = ds.Tables[0].Rows[i][1].ToString().Trim();
                    }
                                                    //部门
                    if (ds.Tables[0].Rows[i][2].ToString().Trim() != "")
                    {

                        department = ds.Tables[0].Rows[i][2].ToString().Trim();
                        string str = department.Substring(0,department.IndexOf("&"));
                        stationname = str;       //收费站
                    }

                                  //组织级别                    
                    if (ds.Tables[0].Rows[i][3].ToString().Trim() != "")
                    {
                        orggrade = ds.Tables[0].Rows[i][3].ToString().Trim();
                    }
                                    //组织代码
                    if (ds.Tables[0].Rows[i][4].ToString().Trim() != "")
                    {
                        orgcode = ds.Tables[0].Rows[i][4].ToString().Trim();
                    }
                                       //姓名
                    if (ds.Tables[0].Rows[i][5].ToString().Trim() != "")
                    {
                        name = ds.Tables[0].Rows[i][5].ToString().Trim();
                    }
                                    //职员代码
                    if (ds.Tables[0].Rows[i][6].ToString().Trim() != "")
                    {
                        empcode = ds.Tables[0].Rows[i][6].ToString().Trim();
                    }
                              //职位
                    if (ds.Tables[0].Rows[i][7].ToString().Trim() != "")
                    {
                        positionname = ds.Tables[0].Rows[i][7].ToString().Trim();
                    }
                                        //性别
                    if (ds.Tables[0].Rows[i][8].ToString().Trim() != "")
                    {
                        string sexinfo = ds.Tables[0].Rows[i][8].ToString().Trim();
                        switch (sexinfo)
                        {
                            case "男性":
                                sex = 1;
                                break;
                            case "女性":
                                sex = 2;
                                break;
                            default:
                                sex = 0;
                                break;
                        }
                        //sex = 0;
                    }
                              //出生日期
                    if (ds.Tables[0].Rows[i][9].ToString().Trim() != "")
                    {
                        birth = DateTime.Parse(ds.Tables[0].Rows[i][9].ToString().Trim());
                    }
                                    //身份证号
                    if (ds.Tables[0].Rows[i][10].ToString().Trim() != "")
                    {
                        idnum = ds.Tables[0].Rows[i][10].ToString().Trim();
                    }
                                   //年龄
                    if (ds.Tables[0].Rows[i][11].ToString().Trim() != "")
                    {
                        age = Convert.ToInt16(ds.Tables[0].Rows[i][11].ToString().Trim());
                    }
                             //民族
                    if (ds.Tables[0].Rows[i][12].ToString().Trim() != "")
                    {
                        nation = ds.Tables[0].Rows[i][12].ToString().Trim();
                    }
                           //籍贯
                    if (ds.Tables[0].Rows[i][13].ToString().Trim() != "")
                    {
                        nativeplace = ds.Tables[0].Rows[i][13].ToString().Trim();
                    }
                           //政治面貌
                    if (ds.Tables[0].Rows[i][14].ToString().Trim() != "")
                    {
                        //status = Convert.ToInt16(ds.Tables[0].Rows[i][14].ToString().Trim());
                        //status = 0;
                        switch (ds.Tables[0].Rows[i][14].ToString().Trim())
                        {
                            case "共产党员":
                                status = 1;
                                break;
                            case "预备党员":
                                status = 1;
                                break;
                            case "中国共产党员":
                                status = 1;
                                break;
                            case "中国共产党预备党员":
                                status = 1;
                                break;
                            case "中国共产主义青年团团员":
                                status = 2;
                                break;
                            case "共青团员":
                                status = 2;
                                break;
                            case "民主党派":
                                status = 3;
                                break;
                            case "群众":
                                status = 4;
                                break;
                            default:
                                status = 0;
                                break;
                        
                        }
                    }
                             //最高学历
                    if (ds.Tables[0].Rows[i][15].ToString().Trim() != "")
                    {
                        switch (ds.Tables[0].Rows[i][15].ToString().Trim()) {
                            case "初中及以下":
                                education = Convert.ToInt16("1");
                                break;
                            case "高中":
                                education = Convert.ToInt16("3");
                                break;
                            case "中专":
                                education = Convert.ToInt16("3");
                                break;
                            case "大学专科":
                                education = Convert.ToInt16("4");
                                break;
                            case "大学本科":
                                education = Convert.ToInt16("5");
                                break;
                            case "硕士研究生":
                                education = Convert.ToInt16("8");
                                break;
                            case "博士研究生":
                                education = Convert.ToInt16("9");
                                break;
                            default:
                                education = Convert.ToInt16("0");
                                break;
                        }
                        //education = Convert.ToInt16(ds.Tables[0].Rows[i][15].ToString().Trim());
                       // education = Convert.ToInt16("13");

                    }
                              //毕业院校
                    if (ds.Tables[0].Rows[i][16].ToString().Trim() != "")
                    {
                        school = ds.Tables[0].Rows[i][16].ToString().Trim();
                    }
                            //专业
                    if (ds.Tables[0].Rows[i][17].ToString().Trim() != "")
                    {
                        major = ds.Tables[0].Rows[i][17].ToString().Trim();
                    }
                              //员工状态
                    if (ds.Tables[0].Rows[i][18].ToString().Trim() != "")
                    {
                        empstatus = ds.Tables[0].Rows[i][18].ToString().Trim();
                    }
                                //员工类别
                    if (ds.Tables[0].Rows[i][19].ToString().Trim() != "")
                    {
                        emptype = ds.Tables[0].Rows[i][19].ToString().Trim();
                    }
                              //离退休类别
                    if (ds.Tables[0].Rows[i][20].ToString().Trim() != "")
                    {
                        retiretype = ds.Tables[0].Rows[i][20].ToString().Trim();
                    }
                            //辞职类别
                    if (ds.Tables[0].Rows[i][21].ToString().Trim() != "")
                    {
                        resigntype = ds.Tables[0].Rows[i][21].ToString().Trim();
                    }
                             //辞退类别
                    if (ds.Tables[0].Rows[i][22].ToString().Trim() != "")
                    {
                        dismisstype = ds.Tables[0].Rows[i][22].ToString().Trim();
                    }
                              //入司日期
                    if (ds.Tables[0].Rows[i][23].ToString().Trim() != "")
                    {
                        companydate = DateTime.Parse(ds.Tables[0].Rows[i][23].ToString().Trim());
                    }
                           //集团服务年限
                    if (ds.Tables[0].Rows[i][24].ToString().Trim() != "")
                    {
                        groupage = Convert.ToInt16(ds.Tables[0].Rows[i][24].ToString().Trim());
                    }
                              //公司服务年限
                    if (ds.Tables[0].Rows[i][25].ToString().Trim() != "")
                    {
                        companyage = Convert.ToInt16(ds.Tables[0].Rows[i][25].ToString().Trim());
                    }
                          //部门服务年限
                    if (ds.Tables[0].Rows[i][26].ToString().Trim() != "")
                    {
                        deptage = Convert.ToInt16(ds.Tables[0].Rows[i][26].ToString().Trim());
                    }
                             //入职日期
                    if (ds.Tables[0].Rows[i][27].ToString().Trim() != "")
                    {
                        regulardate = DateTime.Parse(ds.Tables[0].Rows[i][27].ToString().Trim());
                    }
                           //转正日期
                    if (ds.Tables[0].Rows[i][28].ToString().Trim() != "")
                    {
                        hiredate = DateTime.Parse(ds.Tables[0].Rows[i][28].ToString().Trim());
                    }
                            //离退日期
                    if (ds.Tables[0].Rows[i][29].ToString().Trim() != "")
                    {
                        retiredate = DateTime.Parse(ds.Tables[0].Rows[i][29].ToString().Trim());
                    }
                         //职务类型
                    if (ds.Tables[0].Rows[i][30].ToString().Trim() != "")
                    {
                        posttype = ds.Tables[0].Rows[i][30].ToString().Trim();
                    }
                            //职务
                    if (ds.Tables[0].Rows[i][31].ToString().Trim() != "")
                    {
                        post = ds.Tables[0].Rows[i][31].ToString().Trim();
                    }
                             //职位代码
                    if (ds.Tables[0].Rows[i][32].ToString().Trim() != "")
                    {
                        postcode = ds.Tables[0].Rows[i][32].ToString().Trim();
                    }
                            //职级名称
                    if (ds.Tables[0].Rows[i][33].ToString().Trim() != "")
                    {
                        rankname = ds.Tables[0].Rows[i][33].ToString().Trim();
                    }
                             //职等名称
                    if (ds.Tables[0].Rows[i][34].ToString().Trim() != "")
                    {
                        gradename = ds.Tables[0].Rows[i][34].ToString().Trim();
                    }
                            //职称类别
                    if (ds.Tables[0].Rows[i][35].ToString().Trim() != "")
                    {
                        tittletype = ds.Tables[0].Rows[i][35].ToString().Trim();
                    }
                          //职称名称
                    if (ds.Tables[0].Rows[i][36].ToString().Trim() != "")
                    {
                        tittlename = ds.Tables[0].Rows[i][36].ToString().Trim();
                    }
                              //职称级别
                    if (ds.Tables[0].Rows[i][37].ToString().Trim() != "")
                    {
                        tittlegrade = ds.Tables[0].Rows[i][37].ToString().Trim();
                    }
                              //职称授予时间
                    if (ds.Tables[0].Rows[i][38].ToString().Trim() != "")
                    {
                        tittledate = DateTime.Parse(ds.Tables[0].Rows[i][38].ToString().Trim());
                    }
                             //社会保险账号
                    if (ds.Tables[0].Rows[i][39].ToString().Trim() != "")
                    {
                        insurnum = ds.Tables[0].Rows[i][39].ToString().Trim();
                    }
                              //银行名称
                    if (ds.Tables[0].Rows[i][40].ToString().Trim() != "")
                    {
                        bankname = ds.Tables[0].Rows[i][40].ToString().Trim();
                    }
                              //银行账号
                    if (ds.Tables[0].Rows[i][41].ToString().Trim() != "")
                    {
                        barnknum = ds.Tables[0].Rows[i][41].ToString().Trim();
                    }
                              //参加工作时间
                    if (ds.Tables[0].Rows[i][42].ToString().Trim() != "")
                    {
                        workdate = DateTime.Parse(ds.Tables[0].Rows[i][42].ToString().Trim());
                    }
                             //入司前连续工作时间（年）
                    if (ds.Tables[0].Rows[i][43].ToString().Trim() != "")
                    {
                        workyear = Convert.ToInt32(ds.Tables[0].Rows[i][43].ToString().Trim());
                    }
                              //现从事专业
                    if (ds.Tables[0].Rows[i][44].ToString().Trim() != "")
                    {
                        nowmajor = ds.Tables[0].Rows[i][44].ToString().Trim();
                    }
                              //曾用名
                    if (ds.Tables[0].Rows[i][45].ToString().Trim() != "")
                    {
                        formername = ds.Tables[0].Rows[i][45].ToString().Trim();
                    }
                              //身高
                    if (ds.Tables[0].Rows[i][46].ToString().Trim() != "")
                    {
                        height = float.Parse(ds.Tables[0].Rows[i][46].ToString().Trim());
                    }
                             //血型
                    if (ds.Tables[0].Rows[i][47].ToString().Trim() != "")
                    {
                        blood = ds.Tables[0].Rows[i][47].ToString().Trim();
                    }
                              //婚姻状况
                    if (ds.Tables[0].Rows[i][48].ToString().Trim() != "")
                    {
                        //marriage = Convert.ToInt16(ds.Tables[0].Rows[i][48].ToString().Trim());
                        marriage = 0;
                    }
                           //健康状况
                    if (ds.Tables[0].Rows[i][49].ToString().Trim() != "")
                    {
                        health = ds.Tables[0].Rows[i][49].ToString().Trim();
                    }
                             //家庭出身
                    if (ds.Tables[0].Rows[i][50].ToString().Trim() != "")
                    {
                        family = ds.Tables[0].Rows[i][50].ToString().Trim();
                    }
                            //个人成份
                    if (ds.Tables[0].Rows[i][51].ToString().Trim() != "")
                    {
                        personal = ds.Tables[0].Rows[i][51].ToString().Trim();
                    }
                            //身份证地址
                    if (ds.Tables[0].Rows[i][52].ToString().Trim() != "")
                    {
                        idaddress = ds.Tables[0].Rows[i][52].ToString().Trim();
                    }
                           //出生地
                    if (ds.Tables[0].Rows[i][53].ToString().Trim() != "")
                    {
                        birthplace = ds.Tables[0].Rows[i][53].ToString().Trim();
                    }
                            //地址
                    if (ds.Tables[0].Rows[i][54].ToString().Trim() != "")
                    {
                        address = ds.Tables[0].Rows[i][54].ToString().Trim();
                    }
                             //邮政编码
                    if (ds.Tables[0].Rows[i][55].ToString().Trim() != "")
                    {
                        postalcode = ds.Tables[0].Rows[i][55].ToString().Trim();
                    }
                             //常用电话
                    if (ds.Tables[0].Rows[i][56].ToString().Trim() != "")
                    {
                        telephone = ds.Tables[0].Rows[i][56].ToString().Trim();
                    }
                             //移动电话
                    if (ds.Tables[0].Rows[i][57].ToString().Trim() != "")
                    {
                        mobilephone = ds.Tables[0].Rows[i][57].ToString().Trim();
                    }
                            //EMail地址
                    if (ds.Tables[0].Rows[i][58].ToString().Trim() != "")
                    {
                        email = ds.Tables[0].Rows[i][58].ToString().Trim();
                    }
                           //是否兼职
                    if (ds.Tables[0].Rows[i][59].ToString().Trim() != "")
                    {
                        isparttime = ds.Tables[0].Rows[i][59].ToString().Trim();
                    }
                          //兼职开始日期
                    if (ds.Tables[0].Rows[i][60].ToString().Trim() != "")
                    {
                        startparttime = DateTime.Parse(ds.Tables[0].Rows[i][60].ToString().Trim());
                    }
                             //兼职结束日期
                    if (ds.Tables[0].Rows[i][61].ToString().Trim() != "")
                    {
                        endparttime = DateTime.Parse(ds.Tables[0].Rows[i][61].ToString().Trim());
                    }
                           //是否占兼职职位编制
                    if (ds.Tables[0].Rows[i][62].ToString().Trim() != "")
                    {
                        isformation = ds.Tables[0].Rows[i][62].ToString().Trim();
                    }
                            //是否占兼职组织单元人数
                    if (ds.Tables[0].Rows[i][63].ToString().Trim() != "")
                    {
                        isunit = ds.Tables[0].Rows[i][63].ToString().Trim();
                    }
                             //是否处于试用考察期
                    if (ds.Tables[0].Rows[i][64].ToString().Trim() != "")
                    {
                        istrial = ds.Tables[0].Rows[i][64].ToString().Trim();
                    }
                            //试用考察期开始日期
                    if (ds.Tables[0].Rows[i][65].ToString().Trim() != "")
                    {
                        starttrial = DateTime.Parse(ds.Tables[0].Rows[i][65].ToString().Trim());
                    }
                            //试用考察期转正日期
                    if (ds.Tables[0].Rows[i][66].ToString().Trim() != "")
                    {
                        endtrial = DateTime.Parse(ds.Tables[0].Rows[i][66].ToString().Trim());
                    }
                             //户口所在地
                    if (ds.Tables[0].Rows[i][67].ToString().Trim() != "")
                    {
                        residence = ds.Tables[0].Rows[i][67].ToString().Trim();
                    }
                   // ************************************************88888888888888888************************************************
                    for (int j = 0; j < 1; j++)
                    {
                        EmployeesInfomodel eq = new EmployeesInfomodel();

                        eq.id = index    ;       //序号
                        eq.stationname = stationname;
                        eq.infyear =infyear ;          //
                        eq.infmonth =infmonth;          //
                        eq.company =company;                //公司
                        eq.department =department;             //部门
                        eq.orggrade =orggrade;               //组织级别
                        eq.orgcode =orgcode;                //组织代码
                        eq.name =name;                   //姓名
                        eq.empcode =empcode;                //职员代码
                        eq.positionname =positionname;          //职位
                        eq.sex =sex;                        //性别
                        eq.birth =birth ;          //出生日期
                        eq.idnum =idnum;                //身份证号
                        eq.age =age;                 //年龄
                        eq.nation =nation;          //民族
                        eq.nativeplace =nativeplace;          //籍贯
                        eq.status =status;          //政治面貌
                        eq.education =education;          //最高学历
                        eq.school =school;          //毕业院校
                        eq.major =major;          //专业
                        eq.empstatus =empstatus;          //员工状态
                        eq.emptype = emptype;              //员工类别
                        eq.retiretype =retiretype;          //离退休类别
                        eq.resigntype =resigntype;          //辞职类别
                        eq.dismisstype =dismisstype;          //辞退类别
                        eq.companydate =companydate;          //入司日期
                        eq.groupage =groupage;          //集团服务年限
                        eq.companyage =companyage;          //公司服务年限
                        eq.deptage =deptage;          //部门服务年限
                        eq.regulardate =regulardate;          //入职日期
                        eq.hiredate =hiredate;          //转正日期
                        eq.retiredate =retiredate;          //离退日期
                        eq.posttype =posttype;          //职务类型
                        eq.post =post;          //职务
                        eq.postcode =postcode;          //职位代码
                        eq.rankname =rankname;          //职级名称
                        eq.gradename =gradename;          //职等名称
                        eq.tittletype =tittletype;          //职称类别
                        eq.tittlename =tittlename;          //职称名称
                        eq.tittlegrade =tittlegrade;          //职称级别
                        eq.tittledate =tittledate ;          //职称授予时间
                        eq.insurnum =insurnum;          //社会保险账号
                        eq.bankname =bankname;          //银行名称
                        eq.barnknum =barnknum;          //银行账号
                        eq.workdate =workdate ;          //参加工作时间
                        eq.workyear =workyear;          //入司前连续工作时间（年）
                        eq.nowmajor =nowmajor;          //现从事专业
                        eq.formername =formername;          //曾用名
                        eq.height =height;          //身高
                        eq.blood =blood;          //血型
                        eq.marriage =marriage;          //婚姻状况
                        eq.health =health;          //健康状况
                        eq.family =family;          //家庭出身
                        eq.personal =personal;          //个人成份
                        eq.idaddress =idaddress;          //身份证地址
                        eq.birthplace =birthplace;          //出生地
                        eq.address =address;          //地址
                        eq.postalcode =postalcode;          //邮政编码
                        eq.telephone =telephone;          //常用电话
                        eq.mobilephone =mobilephone;          //移动电话
                        eq.email =email;          //EMail地址
                        eq.isparttime =isparttime;          //是否兼职
                        eq.startparttime =startparttime;          //兼职开始日期
                        eq.endparttime =endparttime ;          //兼职结束日期
                        eq.isformation =isformation;          //是否占兼职职位编制
                        eq.isunit =isunit;          //是否占兼职组织单元人数
                        eq.istrial =istrial;          //是否处于试用考察期
                        eq.starttrial =starttrial ;          //试用考察期开始日期
                        eq.endtrial =endtrial ;          //试用考察期转正日期
                        eq.residence =residence;          //户口所在地
                        eq.submittime =submittime ;         //
                        eq.submitname =submitname ;
                        list.Add(eq);
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
