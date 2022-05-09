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
using FM2E.BLL.Equipment;
using FM2E.BLL.Maintain;
using FM2E.BLL.SpecialProject;

public partial class Module_FM2E_ArchivesManager_ArchivesDestroyApply_ArchivesDestroyRecord_ArchivesDestroyRecord : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);
    private IList detailList = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDate();
        } 
        ButtonBind();
    }
    /// <summary>
    /// 删除和修改操作
    /// </summary>
    private void ButtonBind()
    {
        if (cmd == "destroy")
        {
            btSubmit.Visible = true;
        }
        else if (cmd == "viewArchives")
        {
            btSubmit.Visible = false;
        }
    }
    /// <summary>
    /// 初始化数据
    /// </summary>
    private void BindDate()
    {
        try
        {
            ArchivesDestroyApply bll = new ArchivesDestroyApply();
            ArchivesDestroyApplyInfo item = bll.GetArchivesDestroyApply(id);
            Label1.Text = item.SheetNo;
            Label2.Text = item.ApplyDate.ToString();
            Label3.Text = item.ApplicantName;
            Label4.Text = item.ApplicantDeptName;
            Label5.Text = item.DestroyReason;
            Label7.Text = item.Remark;
            Label8.Text = item.ApprovalerName;
            Label9.Text = item.ApprovalOpinion;
            Label10.Text = item.ApprovalTime1;
            Label11.Text = item.StatusString;
            detailList = item.ApplyDetailList;
            FillData();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取申请信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    protected void btSubmit_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;
        try
        {
            ArchivesDestroyApply ArchivesDestroyApplybll = new ArchivesDestroyApply();
            ArchivesDestroyApplyInfo info = ArchivesDestroyApplybll.GetArchivesDestroyApply(id);

            foreach (ArchivesDestroyApplyDetailInfo item in info.ApplyDetailList)
            {
                if (item.IsDestroyed)
                    break;
                switch (item.ArchivesType)
                {
                    case "设备出库单":
                        {
                            OutWarehouse bll = new OutWarehouse();
                            bll.DeleteApplyInfo(item.ArchivesID);
                            ArchivesDestroyApplybll.SetDestroyApplyDetailDestroyed(item.ItemID);
                            break;
                        }
                    case "设备入库单":
                        {
                            InWarehouse bll = new InWarehouse();
                            bll.DelInWarehouse(item.ArchivesID);
                            ArchivesDestroyApplybll.SetDestroyApplyDetailDestroyed(item.ItemID);
                            break;
                        }
                    case "日常巡查计划单":
                        {
                            DailyPatrolPlan bll = new DailyPatrolPlan();
                            bll.DelDailyPatrolPlan(item.ArchivesID);
                            ArchivesDestroyApplybll.SetDestroyApplyDetailDestroyed(item.ItemID);
                            break;
                        }
                    case "例行检测计划单":
                        {
                            RoutineInspectPlan bll = new RoutineInspectPlan();
                            bll.DelRoutineInspectPlan(item.ArchivesID);
                            ArchivesDestroyApplybll.SetDestroyApplyDetailDestroyed(item.ItemID);
                            break;
                        }
                    case "例行保养计划单":
                        {
                            RoutineMaintainPlan bll = new RoutineMaintainPlan();
                            bll.DelRoutineMaintainPlan(item.ArchivesID);
                            ArchivesDestroyApplybll.SetDestroyApplyDetailDestroyed(item.ItemID);
                            break;
                        }
                    case "工程资料":
                        {
                            SpecialProject bll = new SpecialProject();
                            bll.DeleteSpecialProject(item.ArchivesID);
                            ArchivesDestroyApplybll.SetDestroyApplyDetailDestroyed(item.ItemID);
                            break;
                        }
                    case "采购单":
                        {
                            Purchase bll = new Purchase();
                            bll.DeletePurchase(item.ArchivesID);
                            ArchivesDestroyApplybll.SetDestroyApplyDetailDestroyed(item.ItemID);
                            break;
                        }
                    case "报验单":
                        {
                            CheckAcceptance bll = new CheckAcceptance();
                            bll.DeleteForm(id);
                            ArchivesDestroyApplybll.SetDestroyApplyDetailDestroyed(item.ItemID);
                            break;
                        }
                    case "设备借调单":
                        {
                            Secondment bll = new Secondment();
                            bll.DeleteBorrowApply(item.ArchivesID);
                            ArchivesDestroyApplybll.SetDestroyApplyDetailDestroyed(item.ItemID);
                            break;
                        }
                    case "设备报废单":
                        {
                            Scrap bll = new Scrap();
                            bll.DeleteScrapApply(item.ArchivesID);
                            ArchivesDestroyApplybll.SetDestroyApplyDetailDestroyed(item.ItemID);
                            break;
                        }
                    case "故障处理单":
                        {
                            MalfunctionHandle bll = new MalfunctionHandle();
                            bll.DelMalfunctionSheet(item.ArchivesID);
                            ArchivesDestroyApplybll.SetDestroyApplyDetailDestroyed(item.ItemID);
                            break;
                        }
                }
            }
            ArchivesDestroyApplybll.SetDestroyApplyDestroyed(id);
            bSuccess = true;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "销毁档案失败", ex, Icon_Type.Error, true, Common.GetHomeBaseUrl("ArchivesDestroyRecord.aspx"), UrlType.Href, "");
        }
        if (bSuccess)
        {
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "销毁档案成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ArchivesDestroyApply.aspx"), UrlType.Href, "");
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
