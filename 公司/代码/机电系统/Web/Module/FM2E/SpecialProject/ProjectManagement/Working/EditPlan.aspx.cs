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

public partial class Module_FM2E_SpecialProject_ProjectManagement_Working_EditPlan : System.Web.UI.Page
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
        //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
        gridview_PlanItemList.Columns[gridview_PlanItemList.Columns.Count - 1].Visible = SystemPermission.CheckButtonPermission(PopedomType.Delete);
        gridview_PlanItemList.Columns[gridview_PlanItemList.Columns.Count - 2].Visible = SystemPermission.CheckButtonPermission(PopedomType.Edit);
        //********** Modification Finished 2011-09-09 **********************************************************************************************
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
        gridview_PlanItemList.DataSource = project.PlanItems;
        gridview_PlanItemList.DataBind();

        //绑定DROPDOWNLIST
        DropDownList_PrefixItem.Items.Clear();
        DropDownList_PrefixItem.Items.Add(new ListItem("", ""));
        DropDownList_PrefixItem.Items.Add(new ListItem("无前置项", "0"));
        foreach (SpecialProjectPlanInfo item in project.PlanItems)
        {
            DropDownList_PrefixItem.Items.Add(new ListItem(item.ItemName, item.ItemID.ToString()));
        }

        //更新横道图

        div_gant.InnerHtml = specialProjectBll.MakeGant(project.PlanItems);
    }

    protected void DropDownList_PrefixItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        //需要检测是否有回路，编辑的时候需要用
        long itemid = 0;
        if(DropDownList_PrefixItem.SelectedValue == "")
        {
            return;
        }

       // ScriptManager.RegisterClientScriptBlock(table_edit, typeof(System.Web.UI.HtmlControls.HtmlTable), "alertcikkrcle", "alert('" + DropDownList_PrefixItem.SelectedValue + "');", true);

        if (DropDownList_PrefixItem.SelectedValue != "0")//有选择前置项的时候才需要检测
        {
            if (long.TryParse(Hidden_EditItemID.Value, out itemid) && itemid > 0)//编辑的时候才需要
            {

                if (CheckCircle(itemid, long.Parse(DropDownList_PrefixItem.SelectedValue)))
                {
                    ScriptManager.RegisterClientScriptBlock(table_edit, typeof(System.Web.UI.HtmlControls.HtmlTable), "alertcircle", "alert('前置项：" + DropDownList_PrefixItem.SelectedItem.Text + " 形成回路，不能设置');", true);
                    try
                    {
                        DropDownList_PrefixItem.SelectedValue = Hidden_SelectPrefixItemID.Value;
                        if (DropDownList_PrefixItem.SelectedValue == "0")
                        {
                            span_inputdays.Style.Value = "display:none";

                            Hidden_SelectPrefixItemID.Value = "0";

                            tr_startend.Style.Value = "display:block";

                        }
                    }
                    catch { }
                    return;
                }
            }
            span_inputdays.Style.Value = "display:block";
            Hidden_SelectPrefixItemID.Value = DropDownList_PrefixItem.SelectedValue;
            //找到前置的结束时间Hidden_SelectPrefixEndTime
            SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
            if (project == null)
            {
                project = specialProjectBll.GetSpecialProject(id);
            }
            IList list = project.PlanItems;
            foreach (SpecialProjectPlanInfo plan in list)
            {
                if (plan.ItemID == long.Parse(DropDownList_PrefixItem.SelectedValue))
                {
                    Hidden_SelectPrefixEndTime.Value = plan.EndTime.ToString("yyyy-MM-dd");
                }
            }


            //TextBox_EndTime.Text =  TextBox_StartTime.Text = Hidden_SelectPrefixEndTime.Value;
            //TextBox_DaysAfter.Text = "0";
            
            tr_startend.Style.Value = "display:none";
        }
        else
        {
            span_inputdays.Style.Value = "display:none";

            Hidden_SelectPrefixItemID.Value = "0";

            tr_startend.Style.Value = "display:block";
        }
    }

    /// <summary>
    /// 检测是否有回路存在
    /// </summary>
    /// <param name="currentid"></param>
    /// <param name="prefixid"></param>
    /// <returns></returns>
    private bool CheckCircle(long editid, long prefixid)
    {
        IList list = null;
        SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
        if (project == null)
        {
            project = specialProjectBll.GetSpecialProject(id);
        }
        list = project.PlanItems;

        Hashtable hash = new Hashtable();
        foreach (SpecialProjectPlanInfo plan in list)
        {
            hash.Add(plan.ItemID, plan);
        }
        //保存原来的施工项，以便恢复
        long originalprefixied = 0;
        SpecialProjectPlanInfo targetItem = hash[editid] as SpecialProjectPlanInfo;
        originalprefixied = targetItem.PrefixItemID;
        targetItem.PrefixItemID = prefixid;
        long currentid = editid;
        while (currentid != 0)
        {
            SpecialProjectPlanInfo item = hash[currentid] as SpecialProjectPlanInfo;

            if (item.PrefixItemID == editid)
            {
                targetItem.PrefixItemID = originalprefixied;
                return true;
            }
            else
            {
                currentid = item.PrefixItemID;
            }
        }
        targetItem.PrefixItemID = originalprefixied;
        return false;
    }

    /// <summary>
    /// 删除行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridview_ItemList_RowDeleted(object sender, GridViewDeleteEventArgs e)
    {
        long itemID = (long)gridview_PlanItemList.DataKeys[e.RowIndex].Values["ItemID"];

        IList list = null;
        SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
        if (project == null)
        {
            project = specialProjectBll.GetSpecialProject(id);
        }
        list = project.PlanItems;

        if (list == null || list.Count == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "删除失败", "施工项:" + itemID + " 已经不存在，请刷新",
               Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }
        //判断是否被合同支付项
        foreach (SpecialProjectContractPayInfo contractpay in project.ContractPayList)
        {
            if (contractpay.PlanItemID == itemID)
            {
                EventMessage.MessageBox(Msg_Type.Error, "删除失败", "施工项:" + itemID + " 已经被合同支付项:" + contractpay.ItemName + "引用，无法删除，请刷新",
               Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
                return;
            }
        }


        specialProjectBll.DeletePlanItem(itemID);
        
        //long prefixitemid = 0;

        //foreach (SpecialProjectPlanInfo item in list)
        //{
        //    if (item.ItemID == itemID)
        //    {
        //        prefixitemid = item.PrefixItemID;
        //        list.Remove(item);
        //        break;
        //    }
        //}
        //SpecialProjectPlanInfo newPrefixItem = new SpecialProjectPlanInfo();
        //foreach (SpecialProjectPlanInfo item in list)
        //{
        //    if (item.ItemID == prefixitemid)
        //    {
        //        newPrefixItem = item;
        //        break;
        //    }
        //}
        ////把后续工作项往前提，并且更新起始日期
        //foreach (SpecialProjectPlanInfo item in list)
        //{
        //    if (item.PrefixItemID == itemID)
        //    {
        //        item.PrefixItemID = prefixitemid;
        //        if (prefixitemid != 0)
        //        {
        //            item.StartTime = newPrefixItem.EndTime.AddDays(item.DaysAfter);
        //            item.EndTime = item.StartTime.AddDays(item.Days);
        //            item.PrefixItemName = newPrefixItem.ItemName;
        //        }
        //    }
        //}
        project = specialProjectBll.GetSpecialProject(id);//从数据库重新读取
        Session[sessionName] = project;

        FillData();
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
            IList planList = project.PlanItems;
            SpecialProjectPlanInfo plan = new SpecialProjectPlanInfo();
            
            plan.DevicePlan = TextBox_DevicePlan.Text.Trim();
            plan.ItemID = 0;
            
            plan.HRPlan = TextBox_HRPlan.Text.Trim();
            plan.ItemName = TextBox_ItemName.Text.Trim();
            plan.PrefixItemID = long.Parse( DropDownList_PrefixItem.SelectedValue);
            if (plan.PrefixItemID == 0)//没有前置项的时候
            {
                plan.DaysAfter = 0;
                plan.EndTime = DateTime.Parse(TextBox_EndTime.Text.Trim());
                plan.StartTime = DateTime.Parse(TextBox_StartTime.Text.Trim());
                TimeSpan span = new TimeSpan();
                span = plan.EndTime - plan.StartTime;
                plan.Days = (int)span.TotalDays + 1;
            }
            else//有前置项
            {
                plan.DaysAfter = int.Parse(TextBox_DaysAfter.Text.Trim());
                //先找到前置项，然后计算开始结束时间
                plan.Days = int.Parse(TextBox_DaysSpan.Text.Trim());
                SpecialProjectPlanInfo prefixitem = null;
                foreach (SpecialProjectPlanInfo item in planList)
                {
                    if (item.ItemID == plan.PrefixItemID)
                    {
                        prefixitem = item;
                    }
                }
                if (prefixitem != null)
                {
                    plan.StartTime = prefixitem.EndTime.AddDays(plan.DaysAfter);
                    plan.EndTime = plan.StartTime.AddDays(plan.Days - 1);
                }
            }
            plan.PrefixItemName = DropDownList_PrefixItem.SelectedItem.Text;
            plan.Progress = 0;
            plan.ProjectID = id;
            plan.Status = SpecialProjectPlanStatus.NORMAL;
            
            //插入数据库
            plan.ItemID = specialProjectBll.SavePlanItem(plan);
            project.PlanItems.Add(plan);
        }
        else//编辑
        {
            IList planList = project.PlanItems;
            SpecialProjectPlanInfo plan  = null;
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
                plan = new SpecialProjectPlanInfo();
                plan.ItemID = 0;
                plan.ProjectID = project.ProjectID;
                project.PlanItems.Add(plan);
            }
            
            plan.DevicePlan = TextBox_DevicePlan.Text.Trim();
            
            plan.HRPlan = TextBox_HRPlan.Text.Trim();
            plan.ItemName = TextBox_ItemName.Text.Trim();
            plan.PrefixItemID = long.Parse(DropDownList_PrefixItem.SelectedValue);
            if (plan.PrefixItemID == 0)//没有前置项的时候
            {
                plan.DaysAfter = 0;
                plan.EndTime = DateTime.Parse(TextBox_EndTime.Text.Trim());
                plan.StartTime = DateTime.Parse(TextBox_StartTime.Text.Trim());
                TimeSpan span = new TimeSpan();
                span = plan.EndTime - plan.StartTime;
                plan.Days = (int)span.TotalDays + 1;
            }
            else//有前置项
            {
                plan.DaysAfter = int.Parse(TextBox_DaysAfter.Text.Trim());
                //先找到前置项，然后计算开始结束时间
                plan.Days = int.Parse(TextBox_DaysSpan.Text.Trim());
                SpecialProjectPlanInfo prefixitem = null;
                foreach (SpecialProjectPlanInfo item in planList)
                {
                    if (item.ItemID == plan.PrefixItemID)
                    {
                        prefixitem = item;
                    }
                }
                if (prefixitem != null)
                {
                    plan.StartTime = prefixitem.EndTime.AddDays(plan.DaysAfter);
                    plan.EndTime = plan.StartTime.AddDays(plan.Days - 1);
                }
            }
            plan.PrefixItemName = DropDownList_PrefixItem.SelectedItem.Text;
            plan.Progress = 0;
            plan.ProjectID = id;
            plan.Status = SpecialProjectPlanStatus.NORMAL;
            plan.ItemID = specialProjectBll.SavePlanItem(plan);
        }

        project = specialProjectBll.GetSpecialProject(id);//从数据库重新读取
        Session[sessionName] = project;
        FillData();
    }

    //结束时间
    protected void TextBox_EndTime_TextChanged(object sender, EventArgs e)
    {
        int days = 0;
        TimeSpan span = DateTime.Parse(TextBox_EndTime.Text) - DateTime.Parse(TextBox_StartTime.Text);
        days = (int)span.TotalDays;
        if (days < 0)
        {
            ScriptManager.RegisterClientScriptBlock(table_edit, typeof(System.Web.UI.HtmlControls.HtmlTable), "alertcircle", "alert('结束时间不等早于开始时间');", true);
            TextBox_EndTime.Text = TextBox_StartTime.Text;
            TextBox_DaysSpan.Text = "1";
        }
        else
        {
            TextBox_DaysSpan.Text = (days + 1).ToString();
        }

    }
     //修改开始时间
    protected void TextBox_DaysAfter_TextChanged(object sender, EventArgs e)
    {
        DateTime start = DateTime.Parse(Hidden_SelectPrefixEndTime.Value);
        start = start.AddDays(int.Parse(TextBox_DaysAfter.Text));
        TextBox_EndTime.Text = TextBox_StartTime.Text = start.ToString("yyyy-MM-dd");
    }
    
}
