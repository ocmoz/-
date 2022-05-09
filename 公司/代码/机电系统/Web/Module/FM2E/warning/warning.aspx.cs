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
public partial class Module_FM2E_warning_warning : System.Web.UI.Page
{
    protected String warningIP;
    protected void Page_Load(object sender, EventArgs e)
    {
        warningIP = ConfigurationManager.AppSettings["warningIP"];
    }
}