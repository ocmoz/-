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
using System.Text;
public partial class Module_FM2E_SpecialProject_ProjectManagement_Working_CheckProgress : System.Web.UI.Page
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

    private const string sessionName = "Module_FM2E_SpecialProject_ProjectManagement_EditPlan";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
        }

        HeadMenuWebControls1.ButtonList[0].ButtonUrl = "ViewProject.aspx?cmd=edit&projectid=" + id;
    }


    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitPage()
    {
        SpecialProjectInfo project = specialProjectBll.GetSpecialProject(id);
        Session[sessionName] = project;
        Label_ProjectName.Text = project.ProjectName;
        Label_ProjectName2.Text = project.ProjectName;
        FillData();
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
        Repeater_ProgressCheckItemList.DataSource = project.PlanItems;
        Repeater_ProgressCheckItemList.DataBind();

        //更新横道图

        div_gant.InnerHtml = specialProjectBll.MakeGant(project.PlanItems);
    }

    /// <summary>
    /// 编辑一行的时候，进行保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        long itemID = long.Parse(Hidden_EditItemID.Value);
        if (itemID == 0)
            return;
        SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
        if (project == null)
        {
            project = specialProjectBll.GetSpecialProject(id);
        }
        IList planList = project.PlanItems;
        SpecialProjectPlanInfo plan = null;
        foreach (SpecialProjectPlanInfo item in planList)
        {
            if (item.ItemID == itemID)
            {
                plan = item;
                break;
            }
        }
        if (plan == null)
        {
            return;
        }


        plan.Progress = decimal.Parse(TextBox_Progress.Text.Trim()) / 100;
        specialProjectBll.SavePlanItem(plan);

        SpecialProjectCheckRecordInfo record = new SpecialProjectCheckRecordInfo();
        record.Checker = UserData.CurrentUserData.UserName;
        record.CheckTime = DateTime.Parse(TextBox_Time.Text.Trim());
        record.ItemID = 0;
        record.PlanItemID = itemID;
        record.Progress = plan.Progress;
        record.ProjectID = project.ProjectID;
        record.HR = TextBox_HR.Text.Trim();
        record.Quality = TextBox_Quality.Text.Trim();
        record.Remark = TextBox_Remark.Text.Trim();

        specialProjectBll.SaveProgressCheckRecord(record);

        plan.ProgressCheckRecord.Add(record);
        //project = specialProjectBll.GetSpecialProject(id);//从数据库重新读取
        Session[sessionName] = project;
        FillData();
    }



}
