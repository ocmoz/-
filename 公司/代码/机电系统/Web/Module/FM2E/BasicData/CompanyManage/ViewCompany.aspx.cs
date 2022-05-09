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
using WebUtility.WebControls;
using WebUtility.Components;
using WebUtility;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;

public partial class Module_FM2E_BasicData_CompanyManage_ViewCompany : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    string id = (string)Common.sink("id", MethodType.Get, 50, 0, DataType.Str);

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            ButtonBind();
            BindDate();
        }
    }

    private void ButtonBind()
    {
        HeadMenuButtonItem button2 = HeadMenuWebControls1.ButtonList[2];
        button2.ButtonUrl += id;
        if (cmd == "view")
        {
            //删除
            HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[1];
            button.ButtonUrlType = UrlType.JavaScript;
            button.ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');", id);
            //修改
            button = HeadMenuWebControls1.ButtonList[0];
            button.ButtonUrlType = UrlType.Href;
            button.ButtonUrl = string.Format("EditCompany.aspx?cmd=edit&id={0}", id);
        }
        else if (cmd == "delete")
        {
            //执行删除操作
            bool bSuccess = false;
            try
            {
                

                Company bll = new Company();
                CompanyInfo item = bll.GetCompany(id);
                if (item.PictureUrl != "" && item.PictureUrl != null)
                {
                    FileUpLoadCommon.DeleteFile(item.PictureUrl);
                }
                
                bll.DelCompany(id);

                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除公司失败",ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess == true)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除公司(名字:" + (Session["CompanyInfo" + id] as CompanyInfo).CompanyName + ")成功！", Icon_Type.OK, true , Common.GetHomeBaseUrl("Company.aspx"), UrlType.Href, "");
            }
        }
    }

    private void BindDate()
    {

        Company Company = new Company();
        CompanyInfo item = Company.GetCompany(id);

        Session["CompanyInfo" + id] = item;     //暂时记录下设备信息，以备编辑时使用

        Label1.Text = item.CompanyID;
        Label2.Text = item.CompanyName;
        Label3.Text = item.Address;
        Label4.Text = item.Contact;
        Label5.Text = item.Phone;
        Label6.Text = item.Website;
        Label7.Text = item.Email;
        Label8.Text = item.Fax;
        Label9.Text = item.Remark;
        if(item.IsParentCompany != null)
        Label10.Text = (item.IsParentCompany==true)?"是":"否";

        if (item.PictureUrl != "" && item.PictureUrl != null)
        {
            Image1.ImageUrl = item.PictureUrl;
            Image1.ToolTip = item.CompanyName + "的照片";
        }
        else Image1.ImageUrl = "~/images/nopicture.gif";

    }
}
