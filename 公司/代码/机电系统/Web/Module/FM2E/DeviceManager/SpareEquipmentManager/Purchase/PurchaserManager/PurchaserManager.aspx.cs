using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;

using FM2E.Model.Basic;
using FM2E.Model.System;
using FM2E.Model.Equipment;
using FM2E.Model.Exceptions;

using FM2E.BLL.Basic;
using FM2E.BLL.System;
using FM2E.BLL.Equipment;


public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_Purchase_PurchaserManager_PurchaserManager : System.Web.UI.Page
{ 
    /// <summary>
    /// 采购管理业务逻辑处理类对象
    /// </summary>
    Purchase purchaseBll = new Purchase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            PermissionControl();
            //********** Modification Finished 2011-09-09 **********************************************************************************************
        }
    }

    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
    private void PermissionControl()
    {
        gridview_PurchaserList.Columns[gridview_PurchaserList.Columns.Count - 1].Visible = SystemPermission.CheckPermission(PopedomType.Delete);
        gridview_PurchaserList.Columns[gridview_PurchaserList.Columns.Count - 2].Visible = SystemPermission.CheckPermission(PopedomType.Edit);
        Button_Select.Visible = SystemPermission.CheckPermission(PopedomType.New);
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        TextBox_UserID.Attributes.Add("disabled", "disabled");
        TextBox_PersonName.Attributes.Add("disabled", "disabled");
        FillData();
    }



    /// <summary>
    /// 填充列表
    /// </summary>
    private void FillData()
    {
        try
        {
            IList list = purchaseBll.GetPurchaserList(UserData.CurrentUserData.CompanyID);
            gridview_PurchaserList.DataSource = list;
            gridview_PurchaserList.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "读取信息列表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 点击添加的时候
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        string str = Hidden_SelectedUser.Value;
        string[] array = str.Split('|');
        if (array.Length == 3)
        {
            string id = array[0];
            string name = array[1];
            string remark = array[2].Trim();
            PurchaserInfo p = new PurchaserInfo();
            p.CompanyID = UserData.CurrentUserData.CompanyID;
            p.PurchaserName = name;
            p.Remark = remark;
            p.UserID = id;
            purchaseBll.InsertPurchaser(p);
        }
        FillData();
    }

    /// <summary>
    /// 删除采购员
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridview_PurchaserList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        long id = (long)(gridview_PurchaserList.DataKeys[e.RowIndex]["ID"]);
        purchaseBll.DeletePurchaser(id);
        FillData();
    }


        /// <summary>
    /// 点击编辑保存的时候
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_SaveItem_Click(object sender, EventArgs e)
    {
        long id = long.Parse( Hidden_EditItemID.Value);
        PurchaserInfo p = purchaseBll.GetPurchaser(id);
        if (p != null)
        {
            p.Remark = TextBox_Remark.Text.Trim();
            purchaseBll.UpdatePurchaser(p);
        }
        FillData();
    }
}
