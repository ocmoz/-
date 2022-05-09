using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using FM2E.BLL.System;
using WebUtility.Components;

public partial class UserSet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = Common.Get_UserName;
        Label2.Text = "";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string dispMsg = "";

        if (TextBox1.Text.Trim() == string.Empty)
        {
            dispMsg = "请输入旧密码";
        }
        else if (TextBox2.Text.Trim() == string.Empty)
        {
            dispMsg = "请输入新密码";
        }
        else if (TextBox3.Text.Trim() == string.Empty)
        {
            dispMsg = "请再一次输入新密码";
        }
        else if (TextBox3.Text.Trim() != TextBox2.Text.Trim())
        {
            dispMsg = "两次输入的新密码不相同，请重新输入";
        }
        else
        {
            User bll = new User();

            string oldPassword = Common.md5(TextBox1.Text.Trim(), 32);

            try
            {
                if (bll.ValidatePassword(Common.Get_UserName, oldPassword))
                {
                    //密码正确,修改密码
                    string newPassword = Common.md5(TextBox2.Text.Trim(),32);
                    bll.UpdatePassword(Common.Get_UserName, newPassword);
                }
                else
                    dispMsg = "原始密码不正确";
            }
            catch (Exception ex)
            {
                dispMsg = "发生异常：" + ex.Message;
            }
        }
        if (dispMsg != "")
        {
            EventMessage.EventWriteLog(Msg_Type.Error, dispMsg);
            Label2.Text = "错误提示：" + dispMsg;
        }
        else
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "修改密码成功");

            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(string), "closeModalPopup", "<script language='javascript'>window.returnVal='修改密码成功';window.parent.hidePopWin(true);</script>");
        }
    }
}
