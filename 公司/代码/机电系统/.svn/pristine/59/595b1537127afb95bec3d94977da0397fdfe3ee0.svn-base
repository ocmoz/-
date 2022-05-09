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
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using System.Collections.Generic;

public partial class Module_FM2E_DeviceManager_DeviceInfo_ExpendableInfo_ViewExpendable : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
            ButtonBind();
        }
    }

    private void FillData()
    {
        Expendable Expendable = new Expendable();
        ExpendableInfo s = Expendable.GetExpendable(id);
        Label1.Text = s.Name;
        Label2.Text = s.CompanyName;
        Label3.Text = s.Model;
        Label4.Text = s.Specification;
        //Label5.Text = s.PhotoUrl;
        Label6.Text = s.Count.ToString("#,0.#####");
        Label7.Text = s.Unit;
        Label8.Text = s.UpdateTime.ToString();
        Label9.Text = s.Remark;
        Label10.Text = s.WarehouseName;
        if (s.PhotoUrl != null && s.PhotoUrl != "")
        {
            if (System.IO.File.Exists(Server.MapPath(s.PhotoUrl)))
                Image1.ImageUrl = s.PhotoUrl;
            else
                Image1.ImageUrl = "~/images/deletedpicture.gif";
        }
        else Image1.ImageUrl = "~/images/nopicture.gif";
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
            button.ButtonUrl = string.Format("EditExpendable.aspx?cmd=edit&id={0}", id);
        }
        else if (cmd == "delete")
        {
            //执行删除操作
            bool bSuccess = false;
            try
            {
                Expendable Expendable = new Expendable();
                Expendable.DelExpendable(id);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除消耗品失败",ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除消耗品(ID:" + id + ")成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Expendable.aspx"), UrlType.Href, "");
        }
    }
}
