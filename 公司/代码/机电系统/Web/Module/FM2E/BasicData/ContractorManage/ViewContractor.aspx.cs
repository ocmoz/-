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

using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;

using FM2E.BLL.Basic;
using FM2E.Model.Basic;

public partial class Module_FM2E_BasicData_ContractorManage_ViewContractor : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            ButtonBind();
            BindData();
        }
    }

    private void ButtonBind()
    {
        if (cmd == "view")
        {
            //删除
            HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[1];
            button.ButtonUrlType = UrlType.JavaScript;
            button.ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');", id);
            //修改
            button = HeadMenuWebControls1.ButtonList[0];
            button.ButtonUrlType = UrlType.Href;
            button.ButtonUrl = string.Format("EditContractor.aspx?cmd=edit&id={0}", id);
        }
        else if (cmd == "delete")
        {
            //执行删除操作
            bool bSuccess = false;
            try
            {
                Contractor bll = new Contractor();
                bll.DelContractor(id);

                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除承包商失败",ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess == true)
            {
                //EventMessage.MessageBox(1, "操作成功", "删除承包商ID:(" + id + ")成功！", Icon_Type.OK, Common.GetHomeBaseUrl("DeviceInfo.aspx"));
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除承包商(ID:" + id + ")成功！", Icon_Type.OK, true , Common.GetHomeBaseUrl("Contractor.aspx"), UrlType.Href, "");
            }
        }
    }


    private void BindData()
    {
        try
        {
            Contractor bll = new Contractor();
            ContractorInfo item = bll.GetContractor(id);

            Session["ContractorInfo"] = item;     //暂时记录下信息，以备编辑时使用

            Label1.Text = item.ContractorID.ToString();
            Label2.Text = item.Name;
            Label3.Text = item.Address;
            Label4.Text = item.Phone;
            Label5.Text = item.Fax;
            Label6.Text = item.Email;
            Label7.Text = item.HomePage;
            Label8.Text = item.ResName;
            Label9.Text = item.ResPhone;
            Label10.Text = item.Service;
            Label11.Text = item.Remark;
            Label12.Text = item.Credit.ToString();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败","获取数据失败", ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
        }

    }
}
