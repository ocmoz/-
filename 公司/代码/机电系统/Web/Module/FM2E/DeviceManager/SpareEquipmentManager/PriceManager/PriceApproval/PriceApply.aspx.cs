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
using FM2E.Model.Utils;
using WebUtility;
using FM2E.BLL.Equipment;
using WebUtility.Components;
using FM2E.Model.Equipment;
using FM2E.BLL.System;
using FM2E.Model.System;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_PriceManager_PriceApproval_PriceApply : System.Web.UI.Page
{
    string companyid = UserData.CurrentUserData.CompanyID;
    long ApplyFormID = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
        }
    }

    private void InitialPage()
    {
        PriceApplyInfo item = new PriceApplyInfo();
        item.ApplyFormID = ApplyFormID;
        PriceManager bll = new PriceManager();
        QueryParam qp = bll.GeneratePriceApplySearchTerm(item);
        int recordCount = 0;
        PriceApplyInfo priceapply = (PriceApplyInfo)bll.GetPriceApplyList(qp, out recordCount, companyid)[0];
        User userbll = new User();
        UserInfo info = new UserInfo();
        info = userbll.GetUser(priceapply.Applicant);
        Applicant.Text = info == null ? "" : info.PersonName;
        info = userbll.GetUser(priceapply.Approvaler);
        Approvaler.Text = info == null ? "" : info.PersonName;
        LbStatus.Text = priceapply.StatusName;
        ApprovalDate.Text = DateTime.Compare(priceapply.ApprovalDate, DateTime.MinValue) == 0 ? "" : priceapply.ApprovalDate.ToString();
        ApplyDate.Text = DateTime.Compare(priceapply.ApplyDate, DateTime.MinValue) == 0 ? "" : priceapply.ApplyDate.ToString();
    }
    /// <summary>
    /// 列表绑定数据源
    /// </summary>
    private void FillData()
    {
        try
        {
            PriceApplyDetailInfo item = new PriceApplyDetailInfo();
            item.ApplyFormID = ApplyFormID;
            PriceManager bll = new PriceManager();
            QueryParam qp = bll.GeneratePriceApplyDetailSearchTerm(item);
            
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;
            
            int recordCount = 0;
            IList list = bll.GetPriceApplyDetailList(qp, out recordCount, companyid);
            GridView1.DataSource = list;
            GridView1.DataBind();


            AspNetPager1.RecordCount = recordCount;

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "审批的指导价格初始化失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
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
    /// 列表绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    /// <summary>
    /// 列表行事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
}
