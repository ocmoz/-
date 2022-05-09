using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using WebUtility.Components;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;
using FM2E.Model.Archives;
using FM2E.BLL.Archives;

public partial class Module_FM2E_ArchivesManager_ArchivesManage_ViewArchives : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 20, 0, DataType.Long);
    private readonly Archives bll = new Archives();
    //加载页面
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
            ButtonBind();
            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            PermissionControl();
            //********** Modification Finished 2011-09-09 **********************************************************************************************
        }
    }

    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
    private void PermissionControl()
    {
        HeadMenuWebControls1.ButtonList[1].ButtonVisible = SystemPermission.CheckPermission(PopedomType.Delete);
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = SystemPermission.CheckPermission(PopedomType.Edit);
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************

    //填充数据
    private void FillData()
    {
        IList list = null;
        if (cmd == "view")
        {
            try
            {
                ArchivesInfo item = bll.GetArchives(id);
                if (item == null)
                {
                    return;
                }
                lbArchivesID.Text = Convert.ToString(item.ArchivesID);
                lbArchivesName.Text = item.ArchivesName;
                lbArchivesTypeName.Text = item.ArchivesTypeName.Trim();
                lbInvolvedSystem.Text = item.InvolvedSystem;
                lbInvolvedEquipment.Text = item.InvolvedEquipment;
                lbDescription.Text = item.Description;
                lbRemark.Text = item.Remark;
                list = item.AttachmentList;
                gridview_ItemList.DataSource = list;
                gridview_ItemList.DataBind();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        if (cmd == "delete")
        {
            bool bSuccess = false;
            try
            {
                bll.DeleteArchives(id);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除" + id + "成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Archives.aspx"), UrlType.Href, "");
            }
        }
    }
    //绑定按钮
    private void ButtonBind()
    {
        HeadMenuWebControls1.ButtonList[0].ButtonUrl = "EditArchives.aspx?cmd=edit&id=" + id;
        HeadMenuWebControls1.ButtonList[0].ButtonUrlType = UrlType.Href;
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = true;
        HeadMenuWebControls1.ButtonList[1].ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');", id);
        HeadMenuWebControls1.ButtonList[1].ButtonUrlType = UrlType.JavaScript;
        HeadMenuWebControls1.ButtonList[1].ButtonVisible = true;
    }

    /// <summary>
    /// 数据绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridview_ItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ArchivesAttachmentInfo item = (ArchivesAttachmentInfo)e.Row.DataItem;
            HyperLink hyperlink = (HyperLink)e.Row.FindControl("HyperLink_ArchivesAttachmentFile");
            hyperlink.NavigateUrl = item.SavePath;
            hyperlink.Text = item.ArchivesAttachmentName;
            hyperlink.Visible = true;
        }
    }
}
