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
using System.Windows.Forms;
using System.Xml.Linq;
using FM2E.SQLServerDAL.Utils;
using WebUtility;
using WebUtility.Components;
using FM2E.BLL.System;
using FM2E.BLL.Basic;
using FM2E.Model.System;
public partial class Module_FM2E_Report_Output_Marketing : System.Web.UI.Page
{
    private readonly User userBll = new User();
    private readonly Company companyBll = new Company();
    public String station="";

    protected String quieeIP;
    protected String quieeIP1;
    protected String quieeIP2;
    int lyear = DateTime.Now.Year;
    int lmonth = DateTime.Now.Month;
    protected void Page_Load(object sender, EventArgs e)
    {
        //yingxiao.Visible = false;
        String connStr = ConfigurationManager.ConnectionStrings["Mark"].ConnectionString;
        if (lmonth == 1)
        {
            lyear = lyear - 1;
            lmonth = 12;
        }
        else
        {
            lmonth = lmonth - 1;
        };

        quieeIP = ConfigurationManager.AppSettings["QuieeIP"];
        quieeIP1 = ConfigurationManager.AppSettings["QuieeIP1"];
        quieeIP2 = ConfigurationManager.AppSettings["QuieeIP2"];

        UserInfo userinf = userBll.GetUser(Common.Get_UserName);
        //ExecuteNonQuery("Insert into t_Mark(jingying) values(@jingying)",new SqlParameter("jingying",1));
        //ExecuteNonQuery("select count(*) from t_Mark where year = (year) values(@year) and month = (month) value(@month)",new SqlParameter("year",thisyear), new SqlParameter("month",thismonth));
       object i = ExecuteScalar("select count(huizong) from t_Mark where year =" + lyear + " and month =" + lmonth);
       int j = Convert.ToInt32(i);
       if (j >= 1)
       {
           main.Visible = true;
           station += "&year=" + lyear + "&month=" + lmonth;

       }
        else {
            main.Visible = true;
            station += "&year=" + lyear + "&month=" + lmonth;

            //BEventMessage.MessageBox(Msg_Type.Info, "提示：", "上月份的报表尚未汇总！", WebUtility.Components.Icon_Type.Error, Common.GetHomeBaseUrl("../Output/Business/Business.aspx"));
       }
      
            /*
        SqlDataReader reader = ExecuteReader("select huizong from t_Mark ",new SqlParameter("year",2012),new SqlParameter("month",6));
        while (reader.Read()) {
            int huizong = Convert.ToInt32(reader.GetSqlValue(reader.GetOrdinal("huizong")));
            if (huizong >= 1)
            {
                main.Visible = true;
            }
            main.Visible = false;
        }
            */
    }

    //插入函数
    public int ExecuteNonQuery(string Sql, params SqlParameter[] parameters)
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
                return cmd.ExecuteNonQuery();
            }
        }
    }
    //查询
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
    //
    public static SqlDataReader ExecuteReader(string Sql, params SqlParameter[] parameters)
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
                return cmd.ExecuteReader();
            }
        }
    }
}
