using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class MasterPage_MasterPageNoCheck : System.Web.UI.MasterPage
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
    }
}
