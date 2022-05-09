using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using FM2E.BLL.Basic;
using FM2E.BLL.Equipment;
using FM2E.BLL.Maintain;
using FM2E.BLL.System;
using FM2E.BLL.Utils;
using FM2E.Model.Basic;
using FM2E.Model.Equipment;
using FM2E.Model.Maintain;
using FM2E.Model.System;
using WebUtility;
using WebUtility.Components;


public partial class Module_FM2E_Report_Input_Marketing : System.Web.UI.Page
{
    private readonly User userBll = new User();
    private readonly Company companyBll = new Company();
    
    protected String station;
    protected String quieeIP;
    protected String quieeIP1;
    protected String quieeIP2;
    int lyear = DateTime.Now.Year;
    int lmonth = DateTime.Now.Month;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (lmonth == 1)
        {
            lyear = lyear - 1;
            lmonth = 12;
        }
        else
        {
            lmonth = lmonth - 1;
        };


        UserInfo userinf = userBll.GetUser(Common.Get_UserName);

        quieeIP = ConfigurationManager.AppSettings["QuieeIP"];
        quieeIP1 = ConfigurationManager.AppSettings["QuieeIP1"];
        quieeIP2 = ConfigurationManager.AppSettings["QuieeIP2"];
        if (userinf.DepartmentName.EndsWith("站") || userinf.DepartmentName.EndsWith("隧道所") || userinf.DepartmentName.EndsWith("客服中心"))
        {
            station = userinf.DepartmentName;
            string stationName = station;
            
            meilin.Visible = false;
            kuichong.Visible = false;
            xiaomeisha.Visible = false;
            dameisha.Visible = false;
            yantian.Visible = false;
            heao.Visible = false;
            bainikeng.Visible = false;
            fumin.Visible = false;
            shiyan.Visible = false;
            banhua.Visible = false;
            guanlan.Visible = false;
            tangming.Visible = false;
            xili.Visible = false;
            huanghe.Visible = false;
            shuilang.Visible = false;

            Hashtable stationToInt = new Hashtable();
            switch (stationName) { 
                case "梅林站":
                    meilin.Visible=true;
                    stationToInt.Add(stationName, 1);
                    break;
                case "葵涌站":
                    kuichong.Visible=true;
                    stationToInt.Add(stationName, 2);
                    break;
                case "小梅沙站":
                    xiaomeisha.Visible = true;
                    stationToInt.Add(stationName, 3);
                    break;
                case "大梅沙站":
                    dameisha.Visible = true;
                    stationToInt.Add(stationName, 4);
                    break;
                case "盐田站":
                    yantian.Visible = true;
                    stationToInt.Add(stationName, 5);
                    break;
                case "荷坳站":
                    heao.Visible = true;
                    stationToInt.Add(stationName, 6);
                    break;
                case "白泥坑站":
                    bainikeng.Visible = true;
                    stationToInt.Add(stationName, 7);
                    break;
                case "福民站":
                    fumin.Visible = true;
                    stationToInt.Add(stationName, 8);
                    break;
                case "石岩站":
                    shiyan.Visible = true;
                    stationToInt.Add(stationName, 9);
                    break;
                case "坂华站":
                    banhua.Visible = true;
                    stationToInt.Add(stationName, 10);
                    break;
                case "观澜站":
                    guanlan.Visible = true;
                    stationToInt.Add(stationName, 11);
                    break;
                case "塘明站":
                    tangming.Visible = true;
                    stationToInt.Add(stationName, 12);
                    break;
                case "西丽站":
                    xili.Visible = true;
                    stationToInt.Add(stationName, 13);
                    break; 
                case "黄鹤站":
                    huanghe.Visible = true;
                    stationToInt.Add(stationName, 14);
                    break;  
                case "水朗站":
                    shuilang.Visible = true;
                    stationToInt.Add(stationName, 15);
                    break;
                case "隧道所":
                    shuilang.Visible = true;
                    stationToInt.Add(stationName, 16);
                    break;
                case "客服中心":
                    shuilang.Visible = true;
                    stationToInt.Add(stationName, 17);
                    break;            
                default:
                    stationToInt.Add(stationName, 18);
                    break;                               

            }

            if(stationToInt.Contains(stationName))
                station = stationToInt[stationName] + "&year=" + lyear + "&month=" + lmonth;
            else
                station = "" + "&year=" + lyear + "&month=" + lmonth;

        }
        else if (userinf.DepartmentName == "营运部")
        {
            DepartmentDIV.Attributes.Add("style", "display:");
            meilin.Visible = false;
            kuichong.Visible = false;
            xiaomeisha.Visible = false;
            dameisha.Visible = false;
            yantian.Visible = false;
            heao.Visible = false;
            bainikeng.Visible = false;
            fumin.Visible = false;
            shiyan.Visible = false;
            banhua.Visible = false;
            guanlan.Visible = false;
            tangming.Visible = false;
            xili.Visible = false;
            huanghe.Visible = false;
            shuilang.Visible = false;
            switch (DropDownList1.SelectedValue)
            {
                case "1":
                    meilin.Visible = true;
                    break;
                case "2":
                    kuichong.Visible = true;
                    break;
                case "3":
                    xiaomeisha.Visible = true;
                    break;
                case "4":
                    dameisha.Visible = true;
                    break;
                case "5":
                    yantian.Visible = true;
                    break;
                case "6":
                    heao.Visible = true;
                    break;
                case "7":
                    bainikeng.Visible = true;
                    break;
                case "8":
                    fumin.Visible = true;
                    break;
                case "9":
                    shiyan.Visible = true;
                    break;
                case "10":
                    banhua.Visible = true;
                    break;
                case "11":
                    guanlan.Visible = true;
                    break;
                case "12":
                    tangming.Visible = true;
                    break;
                case "13":
                    xili.Visible = true;
                    break;
                case "14":
                    huanghe.Visible = true;
                    break;
                case "15":
                    shuilang.Visible = true;
                    break;
                case "16":
                    shuilang.Visible = true;
                    break;
                case "17":
                    shuilang.Visible = true;
                    break;             
            }
            station = DropDownList1.SelectedValue + "&year=" + lyear + "&month=" + lmonth;
        }
        else
        {
            station = "" + "&year=" + lyear + "&month=" + lmonth;
        }
        int Yingxiao = Convert.ToInt32(ExecuteScalar("select count(yingxiao) from t_Mark where year =" + lyear + " and month =" + lmonth));
        if (Yingxiao >= 1)
        {
            b1.Disabled = true;
            b2.Disabled = true;
            b3.Disabled = true;
            meilin.Disabled = true;
            kuichong.Disabled = true;
            xiaomeisha.Disabled = true;
            dameisha.Disabled = true;
            yantian.Disabled = true;
            heao.Disabled = true;
            bainikeng.Disabled = true;
            fumin.Disabled = true;
            shiyan.Disabled = true;
            banhua.Disabled = true;
            guanlan.Disabled = true;
            tangming.Disabled = true;
            xili.Disabled = true;
            huanghe.Disabled = true;
            shuilang.Disabled = true;
        }
    }
    public static object ExecuteScalar(string Sql, params SqlParameter[] parameters)
    {
        String connStr = ConfigurationManager.ConnectionStrings["Mark"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = Sql; //清除就数据
                foreach (SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
                return cmd.ExecuteScalar();
            }
        }
    }
}
