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
using System.Collections.Generic;

using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Utils;

public partial class Module_FM2E_BasicData_DepotManage_ViewDepot : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    string id = (string)Common.sink("id", MethodType.Get, 50, 0, DataType.Str);

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            FillDate();
            ButtonBind();
        }
    }

    private void FillDate()
    {
        Warehouse warehouse = new Warehouse();
        WarehouseInfo wh = warehouse.GetWarehouse(id);
        Label1.Text = wh.WareHouseID.ToString();
        Label2.Text = wh.Name;
        Label3.Text = wh.AddressName;
        Label4.Text = wh.CompanyName;
        Label5.Text = wh.ResName;
        Label6.Text = wh.Contactor;
        Label7.Text = wh.Phone;
        Label8.Text = wh.Remark;

        //仓管员
        
        int listCount = 0;
        QueryParam searchTerm = new QueryParam();
        searchTerm.PageSize = AspNetPager1.PageSize;
        searchTerm.PageIndex = AspNetPager1.CurrentPageIndex;
        IList list = warehouse.GetWarehouseUserList(searchTerm, id, out listCount);
        AspNetPager1.RecordCount = listCount;
        GridView_WareHouseKeeper.DataSource = list;
        GridView_WareHouseKeeper.DataBind();
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
            button.ButtonUrl = string.Format("EditDepot.aspx?cmd=edit&id={0}", id);
        }
        else if (cmd == "delete")
        {
            //执行删除操作
            bool bSuccess = false;
            try
            {
                Warehouse warehouse = new Warehouse();
                warehouse.DelWarehouse(id);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除仓库失败", ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除仓库(ID:" + id + ")成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Depot.aspx"), UrlType.Href, "");
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        //仓管员
        Warehouse bll = new Warehouse();
        int listCount = 0;
        QueryParam searchTerm = new QueryParam();
        searchTerm.PageSize = AspNetPager1.PageSize;
        searchTerm.PageIndex = AspNetPager1.CurrentPageIndex;
        IList list = bll.GetWarehouseUserList(searchTerm, id, out listCount);
        AspNetPager1.RecordCount = listCount;
        GridView_WareHouseKeeper.DataSource = list;
        GridView_WareHouseKeeper.DataBind();
    }
}
