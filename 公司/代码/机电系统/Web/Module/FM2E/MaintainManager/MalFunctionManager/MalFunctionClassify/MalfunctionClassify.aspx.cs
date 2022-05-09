using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM2E.BLL.Maintain;
using FM2E.Model.Maintain;
using WebUtility;
using WebUtility.Components;
using FM2E.Model.Utils;
using System.Collections;
using FM2E.Model.Exceptions;

public partial class Module_FM2E_MaintainManager_MalFunctionManager_MalFunctionClassify_MalfunctionClassify : System.Web.UI.Page
{
    private readonly MalfunctionClassify classifyBll = new MalfunctionClassify();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            PermissionControl();
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
    /// 权限控制
    /// </summary>
    private void PermissionControl()
    {
        //校验是否有删除权限
        if (SystemPermission.CheckPermission(PopedomType.Delete))
        {
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
        }
        else
        {
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
        }
        //校验是否有编辑权限
        if (SystemPermission.CheckPermission(PopedomType.Edit))
        {
            GridView1.Columns[GridView1.Columns.Count - 2].Visible = true;
        }
        else
        {
            GridView1.Columns[GridView1.Columns.Count - 2].Visible = false;
        }

        //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = SystemPermission.CheckButtonPermission(PopedomType.New);
        //********** Modification Finished 2011-09-09 **********************************************************************************************
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
            TabContainer1.ActiveTabIndex = 0;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
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
    /// GridView  行命令
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long classifyID = Convert.ToInt64(gvRow.Attributes["ClassifyID"]);

        if (e.CommandName == "view")
        {
            //查看
            Response.Redirect("ViewMalfunctionClassify.aspx?cmd=view&id=" + classifyID);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                classifyBll.DeleteClassify(classifyID);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除故障分类信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除故障分类信息成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("MalfunctionClassify.aspx"), UrlType.Href, "");
        }
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
            e.Row.Attributes["ClassifyID"] = item.ID.ToString();
        }
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        FillData();
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

}
