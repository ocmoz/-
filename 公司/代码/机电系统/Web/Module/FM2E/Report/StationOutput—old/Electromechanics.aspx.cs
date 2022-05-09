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
public partial class Module_FM2E_Report_Output_Electromechanics : System.Web.UI.Page
{
    private readonly User userBll = new User();
    private readonly Company companyBll = new Company();
    public String station;

    protected String quieeIP;
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

        quieeIP = ConfigurationManager.AppSettings["QuieeIP"];

        UserInfo userinf = userBll.GetUser(Common.Get_UserName);


        if (userinf.DepartmentName.EndsWith("站") || userinf.DepartmentName.EndsWith("营运部") || userinf.DepartmentName.EndsWith("隧道所") || userinf.DepartmentName.EndsWith("中心"))
        {
            station = userinf.DepartmentName + "&year=" + lyear + "&month=" + lmonth;
        }
        else
        {
            station = "";
        }
    }
}
