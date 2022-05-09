using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class MasterPage_MasterPage2 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        HtmlGenericControl CheckLinkJs = new HtmlGenericControl("script");

        CheckLinkJs.Attributes.Add("type", "text/javascript");
        CheckLinkJs.Attributes.Add("src", Page.ResolveUrl("~/") + "js/checkform.js");
        CheckLinkJs.Attributes.Add("charset", "utf-8");
        Page.Header.Controls.Add(CheckLinkJs);



        HtmlGenericControl dateLinkJs = new HtmlGenericControl("script");
        dateLinkJs.Attributes.Add("src", Page.ResolveUrl("~/") + "js/date/date.js");
        dateLinkJs.Attributes.Add("type", "text/javascript");
        dateLinkJs.Attributes.Add("charset", "utf-8");
        Page.Header.Controls.Add(dateLinkJs);

        Page.Header.Controls[2].Visible = false;

    }
}
