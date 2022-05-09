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
using WebUtility;
using WebUtility.Components;
using FM2E.BLL.Contract;
using FM2E.BLL.System;
using FM2E.Model.Contract;

public partial class Module_FM2E_Contract_ContractInformation_ContractInformation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
            PermissionControl();
        }
    }

    private void PermissionControl()
    {
        if (SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[8].Visible = true;
        else GridView1.Columns[8].Visible = false;
    }

    private void FillData()
    {
        try
        {
            Module module = new Module();
            // int recordCount = 0;
            //  IList list = module.GetSubModuleByPage(Guid.Empty.ToString("N"), AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize,out recordCount);
            ContractInformation contractInformationBll = new ContractInformation();
            //IList list = insuranceBll.GetAllInsurance();

            ContractInformationInfo term = GetSearchTerm();
            int recordCount = 0;
            IList list = contractInformationBll.GetContractInformation(term, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out recordCount);

            GridView1.DataSource = list;
            GridView1.DataBind();
            AspNetPager1.RecordCount = recordCount;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取合同基本信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        string id = gvRow.Attributes["Id"];

        if (e.CommandName == "view")
        {
            Response.Redirect("EditContractInformation.aspx?cmd=view&id=" + id);
        }
        else if (e.CommandName == "del")
        {


            bool bSuccess = false;
            try
            {
                ContractInformation contractInformationBll = new ContractInformation();
                contractInformationBll.DelContractInformation(id);
                bSuccess = true;
                // GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Visible = false;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除合同基本信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除合同基本信息成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ContractInformation.aspx?"), UrlType.Href, "");
            }
        }
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

            ContractInformationInfo contractInformationInfo = (ContractInformationInfo)e.Row.DataItem;
            e.Row.Attributes["Id"] = Convert.ToString(contractInformationInfo.Id);
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSearch_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        FillData();
    }

    /// <summary>
    /// 获取查询条件
    /// </summary>
    /// <returns></returns>
    private ContractInformationInfo GetSearchTerm()
    {
        ContractInformationInfo item = new ContractInformationInfo();

        item.ContractNo = tb_search_ContractNo.Text.Trim();
        item.ContractName = tb_search_ContractName.Text.Trim();
        if (!string.IsNullOrEmpty(tb_search_Period.Text.Trim()))
        {
            item.Period = Convert.ToInt32(tb_search_Period.Text.Trim());
        }

        return item;
    }
}
