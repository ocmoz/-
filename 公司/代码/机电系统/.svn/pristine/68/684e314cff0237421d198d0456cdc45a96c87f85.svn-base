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
using FM2E.BLL.System;
using FM2E.BLL.Basic;
using WebUtility;
using FM2E.Model.System;
using System.Data.SqlClient;

public partial class Module_FM2E_Report_Input_Supervisor : System.Web.UI.Page
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

        int Dudao = Convert.ToInt32(ExecuteScalar("select count(dudao) from t_Mark where year =" + lyear + " and month =" + lmonth));
        if (Dudao >= 1)
        {
            b1.Disabled = true;
            b2.Disabled = true;
            b3.Disabled = true;
            b4.Disabled = true;
            b5.Disabled = true;
            b6.Disabled = true;
            b7.Disabled = true;

        }
        quieeIP = ConfigurationManager.AppSettings["QuieeIP"];
        quieeIP1 = ConfigurationManager.AppSettings["QuieeIP1"];
        quieeIP2 = ConfigurationManager.AppSettings["QuieeIP2"];

        UserInfo userinf = userBll.GetUser(Common.Get_UserName);


        if (userinf.DepartmentName.EndsWith("站") || userinf.DepartmentName.EndsWith("隧道所") || userinf.DepartmentName.EndsWith("客服中心"))
        {
            string stationName = userinf.DepartmentName;
            Hashtable stationToInt = new Hashtable();
            switch (stationName)
            {
                case "梅林站":
                    stationToInt.Add(stationName, 1);
                    break;
                case "葵涌站":
                    stationToInt.Add(stationName, 2);
                    break;
                case "小梅沙站":
                    stationToInt.Add(stationName, 3);
                    break;
                case "大梅沙站":
                    stationToInt.Add(stationName, 4);
                    break;
                case "盐田站":
                    stationToInt.Add(stationName, 5);
                    break;
                case "荷坳站":
                    stationToInt.Add(stationName, 6);
                    break;
                case "白泥坑站":
                    stationToInt.Add(stationName, 7);
                    break;
                case "福民站":
                    stationToInt.Add(stationName, 8);
                    break;
                case "石岩站":
                    stationToInt.Add(stationName, 9);
                    break;
                case "坂华站":
                    stationToInt.Add(stationName, 10);
                    break;
                case "观澜站":
                    stationToInt.Add(stationName, 11);
                    break;
                case "塘明站":
                    stationToInt.Add(stationName, 12);
                    break;
                case "西丽站":
                    stationToInt.Add(stationName, 13);
                    break;
                case "黄鹤站":
                    stationToInt.Add(stationName, 14);
                    break;
                case "水朗站":
                    stationToInt.Add(stationName, 15);
                    break;
                case "隧道所":

                    stationToInt.Add(stationName, 16);
                    break;
                case "客服中心":

                    stationToInt.Add(stationName, 17);
                    break;
                default:
                    stationToInt.Add(stationName, 18);
                    break;
            }

            if (stationToInt.Contains(stationName))
                station = stationToInt[stationName] + "&year=" + lyear + "&month=" + lmonth;
            else
                station = "" + "&year=" + lyear + "&month=" + lmonth;
        }
        else
        {
            station = "" + "&year=" + lyear + "&month=" + lmonth;
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
