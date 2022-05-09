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
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_InWarehouse_ViewInWarehouse : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonBind();
            FillData();
            BindDate();
        }
    }
    private void FillData()
    {
        try
        {
            InWarehouse bll = new InWarehouse();
            InWarehouseInfo item = bll.GetInWarehouse(id);

            Label1.Text = item.SheetName;
            Label2.Text = item.WarehouseName;
            Label3.Text = item.CompanyName;
            Label4.Text = item.DepartmentName;
            Label5.Text = item.SubmitTime.ToString();
            Label6.Text = item.ApplicantName;
            Label7.Text = item.OperatorName;
            Label8.Text = item.Remark;
            GridView1.DataSource = item.InWarehouseList;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

    }
    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }
    /// <summary>
    /// 删除和修改操作
    /// </summary>
    private void ButtonBind()
    {
        if (cmd == "viewArchives")
        {
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
        }
        if (cmd == "view")
        {
            //删除
            HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[1];
            button.ButtonUrlType = UrlType.JavaScript;
            button.ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');", id);
            //修改
            button = HeadMenuWebControls1.ButtonList[0];
            button.ButtonUrlType = UrlType.Href;
            button.ButtonUrl = string.Format("EditInWarehouse.aspx?cmd=edit&id={0}", id);
        }
        else if (cmd == "delete")
        {
            //执行删除操作
            bool bSuccess = false;
            try
            {
                InWarehouse bll = new InWarehouse();
                bll.DelInWarehouse(id);

                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess == true)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除申请成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("InWarehouse.aspx"), UrlType.Href, "");
            }
        }
    }
    /// <summary>
    /// 初始化化数据
    /// </summary>
    private void BindDate()
    {
       
    }
}
