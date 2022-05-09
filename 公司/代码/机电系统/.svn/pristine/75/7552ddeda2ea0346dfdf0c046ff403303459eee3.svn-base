using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FM2E.BLL.Schedule;
using System.Collections;


public partial class Module_FM2E_Plan_Statistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
         Schedule bll = new Schedule();

        DateTime dt1=Convert.ToDateTime(BeginTime.Value);
        DateTime dt2 = Convert.ToDateTime(EndTime.Value);
        IList list = bll.GetStatistics(dt1, dt2);
        r_Statistics.DataSource = list;
        r_Statistics.DataBind();
    }
}
