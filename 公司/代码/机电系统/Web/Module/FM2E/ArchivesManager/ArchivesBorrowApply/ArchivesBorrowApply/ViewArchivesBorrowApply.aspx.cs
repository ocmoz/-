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
using FM2E.Model.Utils;
using FM2E.BLL.Archives;
using FM2E.Model.Archives;

public partial class Module_FM2E_ArchivesManager_ArchivesBorrowApply_ArchivesBorrowApply_ViewArchivesBorrowApply : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    private IList detailList = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonBind();
            BindDate();
        }
    }
    /// <summary>
    /// 删除和修改操作
    /// </summary>
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
            button.ButtonUrl = string.Format("EditArchivesBorrowApply.aspx?cmd=edit&id={0}", id);
        }
        else if (cmd == "viewArchives")
        {
            HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
        }
        else if (cmd == "delete")
        {
            //执行删除操作
            bool bSuccess = false;
            try
            {
                ArchivesBorrowApply bll = new ArchivesBorrowApply();
                bll.DelArchivesBorrowApply(id);

                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除申请失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess == true)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除申请成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ArchivesBorrowApply.aspx"), UrlType.Href, "");
            }
        }
    }
    /// <summary>
    /// 初始化数据
    /// </summary>
    private void BindDate()
    {
        try
        {
            ArchivesBorrowApply bll = new ArchivesBorrowApply();
            ArchivesBorrowApplyInfo item = bll.GetArchivesBorrowApply(id);
            Label1.Text = item.SheetNo;
            Label2.Text = item.ApplyDate.ToString();
            Label3.Text = item.ApplicantName;
            Label4.Text = item.ApplicantDeptName;
            Label5.Text = item.BorrowReason;
            Label6.Text = item.BorrowTimeString;
            Label7.Text = item.Remark;
            Label8.Text = item.ApprovalerName;
            Label9.Text = item.ApprovalOpinion;
            Label10.Text = item.ApprovalTime1;
            Label11.Text = item.StatusString;
            detailList = item.ApplyDetailList;
            FillData();
            AspNetPager1.CurrentPageIndex = 1;
            if (item.ApplyStatus != ArchivesBorrowApplyStatus.Draft)
            {
                HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
                HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取申请信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 填充GridView
    /// </summary>
    private void FillData()
    {
        try
        {
            if (detailList != null)
            {
                ArrayList list = (ArrayList)detailList;
                int min = (AspNetPager1.CurrentPageIndex - 1) * 10;
                int max = (AspNetPager1.CurrentPageIndex * 10) > list.Count ? list.Count : AspNetPager1.CurrentPageIndex * 10;
                max = max - 1;
                ArrayList thisList = list.GetRange(min, max - min + 1);
                AspNetPager1.RecordCount = list.Count;
                GridView1.DataSource = thisList;
                GridView1.DataBind();
            }
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
}
