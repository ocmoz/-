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
using FM2E.Model.SpecialProject;
using FM2E.Model.Exceptions;

using FM2E.BLL.Basic;
using FM2E.BLL.System;
using FM2E.BLL.SpecialProject;
public partial class Module_FM2E_SpecialProject_ProjectApply_EditJobItems : System.Web.UI.Page
{
    SpecialProject specialProjectBll = new SpecialProject();

    /// <summary>
    /// 命令，包括cmd=new新建、cmd=edit编辑
    /// </summary>
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 0, 0, DataType.Str);
    /// <summary>
    /// 编辑专项工程的ID
    /// </summary>
    private long id = (long)Common.sink("projectid", MethodType.Get, 0, 0, DataType.Long);

    private const string sessionName = "Module_FM2E_SpecialProject_ProjectApply_EditJobItems";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            PermissionControl();
            //********** Modification Finished 2011-09-09 **********************************************************************************************
        }
        HeadMenuWebControls1.ButtonList[0].ButtonUrl = "Apply.aspx?cmd=edit&projectid=" + id;
        HeadMenuWebControls1.ButtonList[1].ButtonUrl = "EditBudgetItems.aspx?cmd=edit&projectid=" + id;

    }

    //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
    private void PermissionControl()
    {
        gridview_JobItemList.Columns[gridview_JobItemList.Columns.Count - 1].Visible = SystemPermission.CheckButtonPermission(PopedomType.Delete);
        gridview_JobItemList.Columns[gridview_JobItemList.Columns.Count - 2].Visible = SystemPermission.CheckButtonPermission(PopedomType.Edit);
    }
    //********** Modification Finished 2011-09-09 **********************************************************************************************

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitPage()
    {
        SpecialProjectInfo project = specialProjectBll.GetSpecialProject(id);
        Session[sessionName] = project;
        Label_ProjectName.Text = project.ProjectName;
        FillData();

        HeadMenuWebControls1.ButtonList[0].ButtonUrl = "Apply.aspx?cmd=edit&projectid=" + id;
        HeadMenuWebControls1.ButtonList[1].ButtonUrl = "EditBudgetItems.aspx?cmd=edit&projectid=" + id;

        TextBox_Amount.Attributes.Add("readonly", "readonly");
        TextBox_Count.Attributes.Add("onchange", "javascript:onCountChange()");
        TextBox_UnitPrice.Attributes.Add("onchange", "javascript:onCountChange()");
    }

    /// <summary>
    /// 数据填充
    /// </summary>
    private void FillData()
    {
        SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
        if (project == null)
        {
            project = specialProjectBll.GetSpecialProject(id);
        }
        gridview_JobItemList.DataSource = project.JobItems;
        gridview_JobItemList.DataBind();
    }


    /// <summary>
    /// 编辑一行的时候，进行保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        long itemID = long.Parse(Hidden_EditItemID.Value);
        SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
        if (project == null)
        {
            project = specialProjectBll.GetSpecialProject(id);
        }
        if (itemID == 0)//添加
        {
            IList jobList = project.JobItems;
            SpecialProjectJobItemInfo job = new SpecialProjectJobItemInfo();
            job.Count = decimal.Parse(TextBox_Count.Text.Trim());
            job.Equipment = TextBox_Equipment.Text.Trim();
            job.ItemID = 0;
            job.Model = TextBox_Model.Text.Trim();
            job.ProjectID = project.ProjectID;
            job.Remark = TextBox_Remark.Text.Trim();
            job.Unit = TextBox_Unit.Text.Trim();
            job.UnitPrice = decimal.Parse(TextBox_UnitPrice.Text.Trim());
            //插入数据库
            job.ItemID = specialProjectBll.SaveJobItem(job);
            project.JobItems.Add(job);
        }
        else//编辑
        {
            IList jobList = project.JobItems;
            SpecialProjectJobItemInfo job = null;
            foreach (SpecialProjectJobItemInfo item in jobList)
            {
                if (item.ItemID == itemID)
                {
                    job = item;
                    break;
                }
            }
            if (job == null)
            {
                job = new SpecialProjectJobItemInfo();
                job.ItemID = 0;
                job.ProjectID = project.ProjectID;
                project.JobItems.Add(job);
            }
            job.Count = decimal.Parse(TextBox_Count.Text.Trim());
            job.Equipment = TextBox_Equipment.Text.Trim();
            job.Model = TextBox_Model.Text.Trim();
            job.ProjectID = project.ProjectID;
            job.Remark = TextBox_Remark.Text.Trim();
            job.Unit = TextBox_Unit.Text.Trim();
            job.UnitPrice = decimal.Parse(TextBox_UnitPrice.Text.Trim());
            job.ItemID = specialProjectBll.SaveJobItem(job);
        }
        Session[sessionName] = project;
        FillData();
    }


    /// <summary>
    /// 数据绑定事件，主要处理footer合计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private decimal totalAmount = 0;//每次postback都会自动初始化
    protected void gridview_ItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SpecialProjectJobItemInfo item = e.Row.DataItem as SpecialProjectJobItemInfo;
            totalAmount += item.Amount;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "合计";
            e.Row.Cells[0].ColumnSpan = 6;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            for (int i = 1; i <= 5; i++)
            {
                e.Row.Cells[i].Visible = false;
            }
            Label LabelTotal = e.Row.FindControl("Label_TotalAmount") as Label;

            if (LabelTotal != null)
            {
                LabelTotal.Text += totalAmount.ToString("#,0.##");//"计算的总数，或者也可以单独计算";//
            }


        }

    }

    /// <summary>
    /// 删除行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridview_ItemList_RowDeleted(object sender, GridViewDeleteEventArgs e)
    {
        long itemID = (long)gridview_JobItemList.DataKeys[e.RowIndex].Values["ItemID"];

        IList list = null;
        SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
        if (project == null)
        {
            project = specialProjectBll.GetSpecialProject(id);
        }
        list = project.JobItems;

        if (list == null || list.Count == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "删除失败", "工程项:" + itemID + " 已经不存在，请刷新",
               Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }

        specialProjectBll.DeleteJobItem(itemID);

        foreach (SpecialProjectJobItemInfo item in list)
        {
            if (item.ItemID == itemID)
            {
                list.Remove(item);
                break;
            }
        }
        Session[sessionName] = project;
        
        FillData();
    }
}
