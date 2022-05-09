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
using System.Data.SqlClient;
using System.Drawing;
public partial class Module_FM2E_Report_Import_EmployeeImport : System.Web.UI.Page
{

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
        }
        
        //ExecuteNonQuery("Insert into t_Mark(jingying) values(@jingying)",new SqlParameter("jingying",1));
        //ExecuteNonQuery("select count(*) from t_Mark where year = (year) values(@year) and month = (month) value(@month)",new SqlParameter("year",thisyear), new SqlParameter("month",thismonth));
        int Yingxiao =Convert.ToInt32(ExecuteScalar("select count(yingxiao) from t_Mark where year =" + lyear + " and month =" + lmonth));
        int Houqin = Convert.ToInt32(ExecuteScalar("select count(houqin) from t_Mark where year =" + lyear + " and month =" + lmonth));
        int Jingying = Convert.ToInt32(ExecuteScalar("select count(jingying) from t_Mark where year =" + lyear + " and month =" + lmonth));
        int Yuangong = Convert.ToInt32(ExecuteScalar("select count(yuangong) from t_Mark where year =" + lyear + " and month =" + lmonth));
        int Jidian = Convert.ToInt32(ExecuteScalar("select count(jidian) from t_Mark where year =" + lyear + " and month =" + lmonth));
        int Ludeng = Convert.ToInt32(ExecuteScalar("select count(ludeng) from t_Mark where year =" + lyear + " and month =" + lmonth));
        int Kefu = Convert.ToInt32(ExecuteScalar("select count(kefu) from t_Mark where year =" + lyear + " and month =" + lmonth));
        int Dudao = Convert.ToInt32(ExecuteScalar("select count(dudao) from t_Mark where year =" + lyear + " and month =" + lmonth));
        int Teshu = Convert.ToInt32(ExecuteScalar("select count(teshu) from t_Mark where year =" + lyear + " and month =" + lmonth));

        int Huizong = Convert.ToInt32(ExecuteScalar("select count(huizong) from t_Mark where year =" + lyear + " and month =" + lmonth));
        yingxiao.Enabled = false;
        houqin.Enabled = false;
        jingying.Enabled = false;
        yuangong.Enabled = false;
        jidian.Enabled = false;
        ludeng.Enabled = false;
        kefu.Enabled = false;
        dudao.Enabled = false;
        teshu.Enabled = false;
        huizong.Enabled = false;

        Button10.Enabled = true;
        Button1.Enabled = true;
        Button2.Enabled = true;
        Button3.Enabled = true;
        Button4.Enabled = true;
        Button5.Enabled = true;
        Button6.Enabled = true;
        Button7.Enabled = true;
        Button8.Enabled = true;
        Button9.Enabled = true;


        if (Yingxiao < 1 && Huizong < 1)//营销
        {
            Button10.Enabled = false;
            yingxiao.Enabled = true;
        };
        if (Houqin < 1 && Huizong < 1)//后勤
        {
            Button3.Enabled = false;
            houqin.Enabled = true;
        };
        if (Jingying < 1 && Huizong < 1)//经营环境
        {
            Button4.Enabled = false;

            jingying.Enabled = true;
        };
        if (Yuangong < 1 && Huizong < 1)//员工
        {
            Button1.Enabled = false;

            yuangong.Enabled = true;
        };
        if (Jidian < 1 && Huizong < 1)//机电
        {
            Button5.Enabled = false;
            jidian.Enabled = true;
        };
        if (Ludeng < 1 && Huizong < 1)//路灯隧道
        {
            Button6.Enabled = false;
            ludeng.Enabled = true;
        };
        if (Kefu < 1 && Huizong < 1)//客服中心
        {
            Button7.Enabled = false;
            kefu.Enabled = true;
        };
        if (Dudao < 1 && Huizong < 1)//督导信息
        {
            Button2.Enabled = false;
            dudao.Enabled = true;
        };
        if (Teshu < 1 && Huizong < 1)//特殊信息
        {
            Button8.Enabled = false;
            teshu.Enabled = true;
        }; 
        if (Huizong < 1)//汇总信息
        {
            Button9.Enabled = false;
            huizong.Enabled = true;
        };
        if (Huizong>=1)
        {
            Button10.Enabled = false;
            Button1.Enabled = false;
            Button2.Enabled = false;
            Button3.Enabled = false;
            Button4.Enabled = false;
            Button5.Enabled = false;
            Button6.Enabled = false;
            Button7.Enabled = false;
            Button8.Enabled = false;
        }
    }

    protected void jingying_Click(object sender, EventArgs e)
    {
        ExecuteNonQuery("Insert into t_Mark(jingying,year,month) values(@jingying,@year,@month)", new SqlParameter("jingying", 1), new SqlParameter("year", lyear), new SqlParameter("month", lmonth));

        //ExecuteNonQuery("Insert into t_Mark(jingying) values(@jingying)",new SqlParameter("jingying",1));
        //ExecuteNonQuery("select count(*) from t_Mark where year = (year) values(@year) and month = (month) value(@month)",new SqlParameter("year",thisyear), new SqlParameter("month",thismonth));
        object i = ExecuteScalar("select count(jingying) from t_Mark where year =" + lyear + " and month =" + lmonth);
        int j = Convert.ToInt32(i);
        if (j >= 1)
        {
            //jingying.ForeColor = System.Drawing.Color.Gray;
            jingying.Enabled = false;

        };
    
        
    }


    protected void yingxiao_Click(object sender, EventArgs e)
    {
        ExecuteNonQuery("Insert into t_Mark(yingxiao,year,month) values(@yingxiao,@year,@month)", new SqlParameter("yingxiao", 1), new SqlParameter("year", lyear), new SqlParameter("month", lmonth));
        object i = ExecuteScalar("select count(yingxiao) from t_Mark where year =" + lyear + " and month =" + lmonth);
        int j = Convert.ToInt32(i);
        if (j >= 1)
        {
            yingxiao.Enabled = false;
        };
    }
    protected void yuangong_Click(object sender, EventArgs e)
    {
        ExecuteNonQuery("Insert into t_Mark(yuangong,year,month) values(@yuangong,@year,@month)", new SqlParameter("yuangong", 1), new SqlParameter("year", lyear), new SqlParameter("month", lmonth));
        object i = ExecuteScalar("select count(yuangong) from t_Mark where year =" + lyear + " and month =" + lmonth);
        int j = Convert.ToInt32(i);
        if (j >= 1)
        {
            yuangong.Enabled = false;
        };
    }
    protected void houqin_Click(object sender, EventArgs e)
    {
        ExecuteNonQuery("Insert into t_Mark(houqin,year,month) values(@houqin,@year,@month)", new SqlParameter("houqin", 1), new SqlParameter("year", lyear), new SqlParameter("month", lmonth));
        object i = ExecuteScalar("select count(houqin) from t_Mark where year =" + lyear + " and month =" + lmonth);
        int j = Convert.ToInt32(i);
        if (j >= 1)
        {
            houqin.Enabled = false;
        };
    }
    protected void jidian_Click(object sender, EventArgs e)
    {
        ExecuteNonQuery("Insert into t_Mark(jidian,year,month) values(@jidian,@year,@month)", new SqlParameter("jidian", 1), new SqlParameter("year", lyear), new SqlParameter("month", lmonth));
        object i = ExecuteScalar("select count(jidian) from t_Mark where year =" + lyear + " and month =" + lmonth);
        int j = Convert.ToInt32(i);
        if (j >= 1)
        {
            jidian.Enabled = false;
        };
    }
    protected void ludeng_Click(object sender, EventArgs e)
    {
        ExecuteNonQuery("Insert into t_Mark(ludeng,year,month) values(@ludeng,@year,@month)", new SqlParameter("ludeng", 1), new SqlParameter("year", lyear), new SqlParameter("month", lmonth));
        object i = ExecuteScalar("select count(ludeng) from t_Mark where year =" + lyear + " and month =" + lmonth);
        int j = Convert.ToInt32(i);
        if (j >= 1)
        {
            ludeng.Enabled = false;
        };
    }
    protected void kefu_Click(object sender, EventArgs e)
    {
        ExecuteNonQuery("Insert into t_Mark(kefu,year,month) values(@kefu,@year,@month)", new SqlParameter("kefu", 1), new SqlParameter("year", lyear), new SqlParameter("month", lmonth));
        object i = ExecuteScalar("select count(kefu) from t_Mark where year =" + lyear + " and month =" + lmonth);
        int j = Convert.ToInt32(i);
        if (j >= 1)
        {
            kefu.Enabled = false;
        };
    }
    protected void dudao_Click(object sender, EventArgs e)
    {
        ExecuteNonQuery("Insert into t_Mark(dudao,year,month) values(@dudao,@year,@month)", new SqlParameter("dudao", 1), new SqlParameter("year", lyear), new SqlParameter("month", lmonth));
        object i = ExecuteScalar("select count(dudao) from t_Mark where year =" + lyear + " and month =" + lmonth);
        int j = Convert.ToInt32(i);
        if (j >= 1)
        {
            dudao.Enabled = false;
        };
    }
    protected void teshu_Click(object sender, EventArgs e)
    {
        ExecuteNonQuery("Insert into t_Mark(teshu,year,month) values(@teshu,@year,@month)", new SqlParameter("teshu", 1), new SqlParameter("year", lyear), new SqlParameter("month", lmonth));
        object i = ExecuteScalar("select count(teshu) from t_Mark where year =" + lyear + " and month =" + lmonth);
        int j = Convert.ToInt32(i);
        if (j >= 1)
        {
            teshu.Enabled = false;
        };
    }
    protected void huizong_Click(object sender, EventArgs e)
    {
        int Yingxiao = Convert.ToInt32(ExecuteScalar("select count(yingxiao) from t_Mark where year =" + lyear + " and month =" + lmonth));
        int Houqin = Convert.ToInt32(ExecuteScalar("select count(houqin) from t_Mark where year =" + lyear + " and month =" + lmonth));
        int Jingying = Convert.ToInt32(ExecuteScalar("select count(jingying) from t_Mark where year =" + lyear + " and month =" + lmonth));
        int Yuangong = Convert.ToInt32(ExecuteScalar("select count(yuangong) from t_Mark where year =" + lyear + " and month =" + lmonth));
        int Jidian = Convert.ToInt32(ExecuteScalar("select count(jidian) from t_Mark where year =" + lyear + " and month =" + lmonth));
        int Ludeng = Convert.ToInt32(ExecuteScalar("select count(ludeng) from t_Mark where year =" + lyear + " and month =" + lmonth));
        int Kefu = Convert.ToInt32(ExecuteScalar("select count(kefu) from t_Mark where year =" + lyear + " and month =" + lmonth));
        int Dudao = Convert.ToInt32(ExecuteScalar("select count(dudao) from t_Mark where year =" + lyear + " and month =" + lmonth));
        int Teshu = Convert.ToInt32(ExecuteScalar("select count(teshu) from t_Mark where year =" + lyear + " and month =" + lmonth));
        int var1 = Yingxiao + Houqin + Jingying + Yuangong + Jidian + Ludeng + Kefu + Dudao + Teshu;
        if (var1 >=9)
        {
            ExecuteNonQuery("Insert into t_Mark(huizong,year,month) values(@huizong,@year,@month)", new SqlParameter("huizong", 1), new SqlParameter("year", lyear), new SqlParameter("month", lmonth));
        };
        object i = ExecuteScalar("select count(huizong) from t_Mark where year =" + lyear + " and month =" + lmonth);
        int j = Convert.ToInt32(i);
        if (j >= 1)
        {
            huizong.Enabled = false;
        }
        else
        {
            EventMessage.MessageBox(Msg_Type.Info, "提示：", "九大业务线尚未完全提交完毕，暂不能确认汇总!", Icon_Type.Error, Common.GetHomeBaseUrl("../submit/submit.aspx"));
        }
    }

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


    protected void Button10_Click(object sender, EventArgs e)
    {

        ExecuteScalar("delete  from t_Mark where  yingxiao = '1' and year =" + lyear + " and month =" + lmonth);

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ExecuteScalar("delete  from t_Mark where  yuangong = '1' and year =" + lyear + " and month =" + lmonth);

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ExecuteScalar("delete  from t_Mark where  dudao = '1' and year =" + lyear + " and month =" + lmonth);

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        ExecuteScalar("delete  from t_Mark where  houqin = '1' and year =" + lyear + " and month =" + lmonth);

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        ExecuteScalar("delete  from t_Mark where  jingying = '1' and year =" + lyear + " and month =" + lmonth);

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        ExecuteScalar("delete  from t_Mark where  jidian = '1' and year =" + lyear + " and month =" + lmonth);

    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        ExecuteScalar("delete  from t_Mark where  ludeng = '1' and year =" + lyear + " and month =" + lmonth);

    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        ExecuteScalar("delete  from t_Mark where  kefu = '1' and year =" + lyear + " and month =" + lmonth);

    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        ExecuteScalar("delete  from t_Mark where  teshu = '1' and year =" + lyear + " and month =" + lmonth);

    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        ExecuteScalar("delete  from t_Mark where  huizong = '1' and year =" + lyear + " and month =" + lmonth);

    }
}
