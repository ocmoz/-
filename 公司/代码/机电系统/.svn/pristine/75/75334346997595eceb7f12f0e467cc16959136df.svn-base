using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class savedoc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        SOAOfficeX.SaveDocObj SOAObj = new SOAOfficeX.SaveDocObj();


        SOAObj.SaveToFile(Server.MapPath("excel\\") + SOAObj.FileName);

        //向客户端控件返回以上代码执行成功的消息。如果您需要客户端显示调试信息，请注释掉此句。
        SOAObj.ReturnOK();
    }
}
