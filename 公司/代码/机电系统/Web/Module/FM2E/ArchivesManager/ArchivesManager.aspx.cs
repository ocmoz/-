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
using FM2E.BLL.Basic;
using FM2E.Model.Archives;
using FM2E.Model.Maintain;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.WorkflowLayer;
using FM2E.BLL.Maintain;
using WebUtility.Components;
using WebUtility;
using System.Xml;
using FM2E.BLL.Archives;

public partial class Module_FM2E_ArchivesManager_ArchivesManager : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);

    private static string XMLPATH = HttpContext.Current.Server.MapPath("~") + "/Module/FM2E/ArchivesManager/ArchivesConfig.xml";
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
        lbErrorMessage.Text = "";
        if (cmd == "BorrowAdd")
        {
            HeadMenuWebControls1.ButtonList[0].ButtonName = "返回借阅申请单";
            HeadMenuWebControls1.ButtonList[0].ButtonUrl = "ArchivesBorrowApply/ArchivesBorrowApply/EditArchivesBorrowApply.aspx?cmd=add";
        }
        else if (cmd == "DestroyAdd")
        {
            HeadMenuWebControls1.ButtonList[0].ButtonName = "返回销毁申请单";
            HeadMenuWebControls1.ButtonList[0].ButtonUrl = "ArchivesDestroyApply/ArchivesDestroyApply/EditArchivesDestroyApply.aspx?cmd=add";
        }
        else
        {
            HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
            
        }
    }
    /// <summary>
    /// 查询按钮的响应
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if(DropDownList2.SelectedValue.Equals(string.Empty))
            {
                lbErrorMessage.Text = "请选择系统模块和档案类型";
                return;
            }
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(XMLPATH);
            XmlNode node = xmldoc.GetElementsByTagName(DropDownList2.SelectedValue).Item(0);
            string searchUrl = node.Attributes["SearchUrl"].Value;
            string name = node.Attributes["ModuleName"].Value;
            if (string.IsNullOrEmpty(cmd))
            {
                cmd = "BorrowAdd";
            }
            Response.Redirect("SearchPages/" + searchUrl + "?archivesType=" + node.Name + "&archivesTypeName=" + name + "&cmd=" + cmd, false);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "查询数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
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
    /// 已有的申请单
    /// </summary>
    private void FillData()
    {
        ArchivesBorrowApply bll = new ArchivesBorrowApply();

        QueryParam searchTerm = new QueryParam();
        ArchivesBorrowApplyDetailInfo item = new ArchivesBorrowApplyDetailInfo();
        searchTerm = bll.GenerateDetailSearchTerm(item,UserData.CurrentUserData.UserName);
        int listCount = 0; 
        searchTerm.PageIndex = AspNetPager1.CurrentPageIndex;
        IList list = bll.GetDetailList(searchTerm, out listCount);
        //IList list = bll.GetisBorrowedDetail(UserData.CurrentUserData.UserName);  //获取用户尚可查看的历史档案
        AspNetPager1.RecordCount = listCount;
        GridView1.DataSource = list;
        GridView1.DataBind();
        
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            ArchivesBorrowApplyDetailInfo item = (ArchivesBorrowApplyDetailInfo)e.Row.DataItem;

            e.Row.Attributes["ArchivesID"] = item.ArchivesID.ToString();
            e.Row.Attributes["ArchivesType"]=item.ArchivesType;
            e.Row.Attributes["Module"] = item.Module;
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        long id = Convert.ToInt64(gvRow.Attributes["ArchivesID"]);
        string archivestype = gvRow.Attributes["ArchivesType"];  //档案类型
        string module = gvRow.Attributes["Module"];  //系统模块

        if (e.CommandName == "view")
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(XMLPATH);
            XmlNodeList nodelist = xmldoc.SelectSingleNode("ArchivesConfig").ChildNodes;  //系统模块
            foreach (XmlNode node in nodelist)
            {
                if (node.Attributes["ModuleName"].Value == module)
                {
                    XmlNode childnode = xmldoc.SelectSingleNode("ArchivesConfig/" + node.Name);  //子模块
                    foreach (XmlNode subnode in childnode.ChildNodes)
                    {
                        if (subnode.Attributes["ModuleName"].Value == archivestype)
                        {
                            Response.Redirect("../" + subnode.Attributes["ViewUrl"].Value + "?cmd=viewArchives&id=" + id, false);
                        }
                    }
                }
            }
        }
    }

}
