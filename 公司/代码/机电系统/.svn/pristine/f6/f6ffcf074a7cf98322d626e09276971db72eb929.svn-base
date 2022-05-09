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

public partial class Module_FM2E_SpecialProject_ProjectApproval_ModifyApproval_ModifyApproval : System.Web.UI.Page
{
    SpecialProject specialProjectBll = new SpecialProject();

    /// <summary>
    /// 上传文件路径，相对于~/public文件夹
    /// </summary>
    protected const string UPLOADFOLDER = "SpecialProject/";

    /// <summary>
    /// 命令，包括cmd=new新建、cmd=edit编辑
    /// </summary>
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 0, 0, DataType.Str);
    /// <summary>
    /// 编辑专项工程的ID
    /// </summary>
    private long id = (long)Common.sink("projectid", MethodType.Get, 0, 0, DataType.Long);
    /// <summary>
    /// 变更申请单ID，修改的时候用
    /// </summary>
    private long modifyid = (long)Common.sink("modifyid", MethodType.Get, 0, 0, DataType.Long);

    private const string sessionName = "Module_FM2E_SpecialProject_ProjectApproval_ModifyApproval_ModifyApproval";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
        }
        HeadMenuWebControls1.ButtonList[0].ButtonUrl = "../../ProjectManagement/Working/EditModify.aspx?cmd=edit&projectid=" + id;
    }


    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitPage()
    {

        SpecialProjectInfo project = specialProjectBll.GetSpecialProjectBasicInfo(id);
        Label_ProjectName.Text = project.ProjectName;
        Label_ProjectName2.Text = project.ProjectName;
        SpecialProjectModifyInfo modify = null;

        if (modifyid != 0)
        {
            modify = specialProjectBll.GetModify(modifyid);
            Label_ApplyTime.Text = modify.ApplyTime.ToString("yyyy-MM-dd");
            Label_BudgetChange.Text = modify.BudgetChange.ToString("0.##");
            Label_BudgetIncDesc.Text = modify.BudgetIncDesc.ToString("0.##");
            Label_ChangeContent.Text = modify.ChangeContent;
            if (modify.ContentAttechment.Length > 0)
            {
                HyperLink_File.Text = modify.ContentAttechment;
                HyperLink_File.NavigateUrl = SystemConfig.Instance.UploadPath + UPLOADFOLDER + modify.ContentAttechment;
                HyperLink_File.Visible = true;

            }
            else
                HyperLink_File.Visible = false;
            Label_DelayDays.Text = modify.DelayDays.ToString();
            Label_Remark2.Text = modify.Remark;

        }
        else
        {
            EventMessage.MessageBox(Msg_Type.Error, "进入审批失败", "无权限或者获取数据错误", Icon_Type.Error, false, "history.go(-1);", UrlType.JavaScript, "");
            return;
        }


        Label_Status.Text = modify.StatusString;

        Session[sessionName] = modify;

        //下拉列表初始化
        Array array = Enum.GetValues(typeof(SpecialProjectModifyApprovalResult));
        ListItem li = null;
        foreach (SpecialProjectModifyApprovalResult item in array)
        {
            switch (item)
            {
                case SpecialProjectModifyApprovalResult.FAILED:
                    li = new ListItem("不通过", ((int)item).ToString());
                    DropDownList_Owner.Items.Add(li);
                    DropDownList_Contract.Items.Add(li);
                    DropDownList_Leader.Items.Add(li);
                    break;
                case SpecialProjectModifyApprovalResult.SUCCESS:
                    li = new ListItem("通过", ((int)item).ToString());
                    DropDownList_Owner.Items.Add(li);
                    DropDownList_Contract.Items.Add(li);
                    DropDownList_Leader.Items.Add(li);
                    break;
                default:
                    break;
            }
        }
        //业主
        if (((int)modify.OwnerResult) == 0)
        {
            div_ownerapproval.Visible = true;
            div_ownerapprovalinfo.Visible = false;
        }
        else
        {
            div_ownerapproval.Visible = false;
            div_ownerapprovalinfo.Visible = true;
            Label_Owner.Text = modify.OwnerApprovaler;
            Label_OwnerApproval.Text = modify.OwnerResultString;
            Label_OwnerFeeBack.Text = modify.OwnerFeeBack;
            Label_OwnerTime.Text = modify.OwnerApprovalDate.ToString("yyyy-MM-dd");
        }
        //合约部
        if (((int)modify.ContractResult) == 0&& modify.OwnerResult== SpecialProjectModifyApprovalResult.SUCCESS)//业主审批过才需要，未审批不需要
        {
            div_contractapproval.Visible = true;
            div_contractapprovalinfo.Visible = false;
        }
        else
        {
            div_contractapproval.Visible = false;
            div_contractapprovalinfo.Visible = true;
            if ((int)modify.ContractResult != 0)//审批过才显示
            {
                Label_Contract.Text = modify.ContractApprovaler;
                Label_ContractApproval.Text = modify.ContractResultString;
                Label_ContractFeeBack.Text = modify.ContractFeeBack;
                Label_ContractTime.Text = modify.ContractApprovalDate.ToString("yyyy-MM-dd");
            }
        }
        //领导
        if (((int)modify.LeaderResult) == 0 && modify.ContractResult == SpecialProjectModifyApprovalResult.SUCCESS)
        {
            div_leaderapproval.Visible = true;
            div_leaderapprovalinfo.Visible = false;
        }
        else
        {
            div_leaderapproval.Visible = false;
            div_leaderapprovalinfo.Visible = true;
            if ((int)modify.LeaderResult != 0)//审批过才显示
            {
                Label_Leader.Text = modify.LeaderApprovaler;
                Label_LeaderApproval.Text = modify.LeaderResultString;
                Label_LeaderFeeBack.Text = modify.LeaderFeeBack;
                Label_LeaderTime.Text = modify.LeaderApprovalDate.ToString("yyyy-MM-dd");
            }
        }
        FillData();
    }

    /// <summary>
    /// 数据填充
    /// </summary>
    private void FillData()
    {
        SpecialProjectModifyInfo modify = GetSessionModify();
        decimal total = modify.TotalAmountFromDetail;
        Label_TotalAmount.Text = (total > 0 ? "+" : "-") + total.ToString("#,0.##");
        if (total > 0)
        {
            Label_TotalAmount.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            Label_TotalAmount.ForeColor = System.Drawing.Color.Green;
        }

        Repeater_Detail.DataSource = modify.DetailList;
        Repeater_Detail.DataBind();

    }


    /// <summary>
    /// 从SESSION中获取临时数据
    /// </summary>
    /// <returns></returns>
    private SpecialProjectModifyInfo GetSessionModify()
    {
        SpecialProjectModifyInfo modify = Session[sessionName] as SpecialProjectModifyInfo;
        if (modify == null)
        {

            modify = specialProjectBll.GetModify(modifyid);
            Session[sessionName] = modify;
        }
        return modify;
    }

    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_SaveModify_Click(object sender, EventArgs e)
    {
        SpecialProjectModifyInfo modify = GetSessionModify();



        if (((int)modify.OwnerResult) == 0)//原先是未审批的
        {
            string owner = TextBox_Owner.Text.Trim();
            if (owner.Length == 0)
            {
                EventMessage.MessageBox(Msg_Type.Error, "审批变更失败", "审批变更失败，未输入业主签名", Icon_Type.Error, false, "history.go(-1);", UrlType.JavaScript, "history.go(-1);");
                return;
            }

            string feeback = TextBox_OwnerFeeBack.Text.Trim();
            if (feeback.Length == 0)
            {
                EventMessage.MessageBox(Msg_Type.Error, "审批变更失败", "审批变更失败，未输入反馈意见", Icon_Type.Error, false, "history.go(-1);", UrlType.JavaScript, "history.go(-1);");
                return;
            }

            modify.OwnerApprovalDate = DateTime.Now;
            modify.OwnerApprovaler = owner;
            modify.OwnerFeeBack = feeback;
            modify.OwnerResult = (SpecialProjectModifyApprovalResult)Enum.Parse(typeof(
                SpecialProjectModifyApprovalResult), DropDownList_Owner.SelectedValue);

        }
        else
        {

            if (((int)modify.ContractResult) == 0)//原先是未审批的
            {
                string contract = TextBox_Contract.Text.Trim();
                if (contract.Length == 0)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "审批变更失败", "审批变更失败，未输入合约部签名", Icon_Type.Error, false, "history.go(-1);", UrlType.JavaScript, "history.go(-1);");
                    return;
                }

                string feeback = TextBox_ContractFeeBack.Text.Trim();
                if (feeback.Length == 0)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "审批变更失败", "审批变更失败，未输入合约部反馈意见", Icon_Type.Error, false, "history.go(-1);", UrlType.JavaScript, "history.go(-1);");
                    return;
                }

                modify.ContractApprovalDate = DateTime.Now;
                modify.ContractApprovaler = contract;
                modify.ContractFeeBack = feeback;
                modify.ContractResult = (SpecialProjectModifyApprovalResult)Enum.Parse(typeof(
                    SpecialProjectModifyApprovalResult), DropDownList_Contract.SelectedValue);

            }
            else
            {
                if (((int)modify.LeaderResult) == 0)//原先是未审批的
                {
                    string leader = TextBox_Leader.Text.Trim();
                    if (leader.Length == 0)
                    {
                        EventMessage.MessageBox(Msg_Type.Error, "审批变更失败", "审批变更失败，未输入领导签名", Icon_Type.Error, false, "history.go(-1);", UrlType.JavaScript, "history.go(-1);");
                        return;
                    }

                    string feeback = TextBox_LeaderFeeBack.Text.Trim();
                    if (feeback.Length == 0)
                    {
                        EventMessage.MessageBox(Msg_Type.Error, "审批变更失败", "审批变更失败，未输入领导意见", Icon_Type.Error, false, "history.go(-1);", UrlType.JavaScript, "history.go(-1);");
                        return;
                    }

                    modify.LeaderApprovalDate = DateTime.Now;
                    modify.LeaderApprovaler = leader;
                    modify.LeaderFeeBack = feeback;
                    modify.LeaderResult = (SpecialProjectModifyApprovalResult)Enum.Parse(typeof(
                        SpecialProjectModifyApprovalResult), DropDownList_Leader.SelectedValue);
                }
            }
        }


       

        if (modify.OwnerApprovaler.Length > 0 && modify.ContractApprovaler.Length > 0 && modify.LeaderApprovaler.Length > 0
            && modify.OwnerResult == SpecialProjectModifyApprovalResult.SUCCESS && modify.LeaderResult == SpecialProjectModifyApprovalResult.SUCCESS &&
            modify.ContractResult == SpecialProjectModifyApprovalResult.SUCCESS)
        {
            modify.Status = SpecialProjectModifyStatus.OK;//全部审核通过，即OK
        }
        else
        {
            if (modify.OwnerResult == SpecialProjectModifyApprovalResult.FAILED || modify.LeaderResult == SpecialProjectModifyApprovalResult.FAILED ||
            modify.ContractResult == SpecialProjectModifyApprovalResult.FAILED)
            {
                modify.Status = SpecialProjectModifyStatus.CANCEL;//审核不通过，取消
            }
            //其他情况，均在审批中
            modify.Status = SpecialProjectModifyStatus.APPROVALING;
        }

        try
        {
            specialProjectBll.SaveModify(modify);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "审批变更失败", "审批变更失败，请重试", ex, Icon_Type.Error, true, "", UrlType.JavaScript, "history.go(-1);");
            return;
        }
        EventMessage.MessageBox(Msg_Type.Info, "审批提交成功", "审批提交成功，点击转到变更列表", Icon_Type.OK, false, "~/Module/FM2E/SpecialProject/ProjectManagement/Working/ModifyList.aspx?cmd=edit&projectid=" + id, UrlType.Href, "");

    }




}