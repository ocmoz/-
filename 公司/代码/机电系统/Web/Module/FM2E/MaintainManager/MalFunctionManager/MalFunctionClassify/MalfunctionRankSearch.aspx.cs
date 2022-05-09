using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM2E.BLL.Maintain;
using FM2E.Model.Utils;
using FM2E.Model.Maintain;
using WebUtility;
using WebUtility.Components;
using System.Collections;

public partial class Module_FM2E_MaintainManager_MalFunctionManager_MalFunctionClassify_MalfunctionRankSearch : System.Web.UI.Page
{
    private readonly MalfunctionClassify classifyBll = new MalfunctionClassify();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
        }
    }
    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        ListItem[] rankTypes = EnumHelper.GetListItems(typeof(MalfunctionRank), (int)MalfunctionRank.Unknown);

        ddlRank.Items.Clear();
        ddlRank.Items.Add(new ListItem("不限", ((int)MalfunctionRank.Unknown).ToString()));
        ddlRank.Items.AddRange(rankTypes);
    }
    /// <summary>
    /// 填充页面数据
    /// </summary>
    private void FillData()
    {
        try
        {
            MalfunctionClassifySearchInfo term = GetSearchTerm();
            //查询
            int recordCount = 0;
            IList list = classifyBll.GetClassifyList(term, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out recordCount);
            GridView1.DataSource = list;
            GridView1.DataBind();
            AspNetPager1.RecordCount = recordCount;
            TabContainer1.ActiveTabIndex = 1;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    private MalfunctionClassifySearchInfo GetSearchTerm()
    {
        MalfunctionClassifySearchInfo item = new MalfunctionClassifySearchInfo();

        if (ddlSystem.SelectedValue != "")
            item.System = ddlSystem.SelectedValue;

        if (ddlSubsystem.SelectedValue != "")
            item.SubSystem = Convert.ToInt64(ddlSubsystem.SelectedValue);

        if (tbMalfunctionObject.Text.Trim() != string.Empty)
            item.MalfunctionObject = Common.inSQL(tbMalfunctionObject.Text.Trim());

        if (ddlRank.SelectedValue != "0")
            item.Rank = Convert.ToInt32(ddlRank.SelectedValue);

        return item;
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
    /// GridView数据绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            MalfunctionClassifyInfo item = (MalfunctionClassifyInfo)e.Row.DataItem;
            string rank = ((int)item.Rank).ToString();

            CheckBox cb = (CheckBox)e.Row.FindControl("cbSelect");
            if (cb != null)
                cb.Attributes.Add("onclick",
                    string.Format("onClientClick('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", cb.ClientID, rank, item.ResponseTime, (int)item.ResponseUnit, item.FunRestoreTime, (int)item.FunRestoreUnit, item.RepairTime, (int)item.RepairUnit));
        }
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        FillData();
        AspNetPager1.CurrentPageIndex = 1;
    }
}
