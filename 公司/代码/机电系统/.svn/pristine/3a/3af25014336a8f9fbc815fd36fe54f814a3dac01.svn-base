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

public partial class Module_FM2E_SpecialProject_ProjectManagement_Working_EditModify : System.Web.UI.Page
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

    private const string sessionName = "Module_FM2E_SpecialProject_ProjectManagement_EditModify";

    private const string sessionNameJob = "Module_FM2E_SpecialProject_ProjectManagement_EditModify2";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
        }
        HeadMenuWebControls1.ButtonList[0].ButtonUrl = "ModifyList.aspx?cmd=edit&projectid=" + id;
    }


    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitPage()
    {

        SpecialProjectInfo project = specialProjectBll.GetSpecialProject(id);
        Label_ProjectName.Text = project.ProjectName;
        Label_ProjectName2.Text = project.ProjectName;
        SpecialProjectModifyInfo modify = null;
        if (cmd == "new")
        {
            modify = new SpecialProjectModifyInfo();
            modify.ProjectID = id;
            modify.ModifyID = 0;
        }

        else
        {
            if (modifyid != 0)
            {
                modify = specialProjectBll.GetModify(modifyid);
                Label_ApplyTime.Text = modify.ApplyTime.ToString("yyyy-MM-dd");
                TextBox_BudgetChange.Text = modify.BudgetChange.ToString("0.##");
                TextBox_BudgetIncDesc.Text = modify.BudgetIncDesc.ToString("0.##");
                TextBox_ChangeContent.Text = modify.ChangeContent;
                if (modify.ContentAttechment.Length > 0)
                {
                    HyperLink_File.Text = modify.ContentAttechment;
                    HyperLink_File.NavigateUrl = SystemConfig.Instance.UploadPath + UPLOADFOLDER + modify.ContentAttechment;
                    HyperLink_File.Visible = true;

                }
                else
                    HyperLink_File.Visible = false;
                TextBox_DelayDays.Text = modify.DelayDays.ToString();
                TextBox_Remark2.Text = modify.Remark;

            }
            else
            {
                modify = new SpecialProjectModifyInfo();
                modify.ProjectID = id;
                modify.ModifyID = 0;
            }
        }

        Label_Status.Text = modify.StatusString;
        DropDownList_JobItems.Items.Add(new ListItem("----------", "0"));
        foreach (SpecialProjectJobItemInfo job in project.JobItems)
        {
            DropDownList_JobItems.Items.Add(new ListItem(job.Equipment + " " + job.Model, job.ItemID.ToString()));
        }

        Session[sessionNameJob] = project.JobItems;
        Session[sessionName] = modify;

        TextBox_Count.Attributes.Add("onblur", "javascript:onCountChange();");


        Label_Owner.Text = modify.OwnerApprovaler;
        Label_OwnerApproval.Text = modify.OwnerResultString;
        Label_OwnerFeeBack.Text = modify.OwnerFeeBack;
        Label_OwnerTime.Text = ((int)modify.OwnerResult == 0) ? "" : modify.OwnerApprovalDate.ToString("yyyy-MM-dd");

        Label_Contract.Text = modify.ContractApprovaler;
        Label_ContractApproval.Text = modify.ContractResultString;
        Label_ContractFeeBack.Text = modify.ContractFeeBack;
        Label_ContractTime.Text = ((int)modify.ContractResult == 0) ? "" : modify.ContractApprovalDate.ToString("yyyy-MM-dd");

        Label_Leader.Text = modify.LeaderApprovaler;
        Label_LeaderApproval.Text = modify.LeaderResultString;
        Label_LeaderFeeBack.Text = modify.LeaderFeeBack;
        Label_LeaderTime.Text = ((int)modify.LeaderResult == 0) ? "" : modify.LeaderApprovalDate.ToString("yyyy-MM-dd");


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
    /// 主要处理删除事件
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void Repeater_Detail_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            SpecialProjectModifyInfo modify = GetSessionModify();
            modify.DetailList.RemoveAt(e.Item.ItemIndex);
            Session[sessionName] = modify;
        }
        FillData();
    }

    /// <summary>
    /// 从SESSION中获取临时数据
    /// </summary>
    /// <returns></returns>
    private SpecialProjectModifyInfo GetSessionModify()
    {
        SpecialProjectModifyInfo modify =Session[sessionName]  as SpecialProjectModifyInfo;
        



        if (modify == null)
        {
            if (cmd == "new")
            {
                modify = new SpecialProjectModifyInfo();
                modify.ProjectID = id;
                modify.ModifyID = 0;
            }

            else
            {
                if (modifyid != 0)
                {
                    modify = specialProjectBll.GetModify(modifyid);
                }
            }
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

        modify.ApplyTime = DateTime.Now;
        try
        {
            modify.BudgetChange = decimal.Parse(TextBox_BudgetChange.Text.Trim());
        }
        catch
        {
            EventMessage.MessageBox(Msg_Type.Error, "保存变更失败", "变更后金额输入格式不正确", Icon_Type.Error, false, "", UrlType.JavaScript, "history.go(-1);");
            return;
        }
        try
        {
            modify.BudgetIncDesc = decimal.Parse(TextBox_BudgetIncDesc.Text.Trim());
        }
        catch
        {
            EventMessage.MessageBox(Msg_Type.Error, "保存变更失败", "增减金额输入格式不正确", Icon_Type.Error, false, "", UrlType.JavaScript, "history.go(-1);");
            return;
        }
        
        modify.ChangeContent = TextBox_ChangeContent.Text.Trim();
        if (modify.ChangeContent.Length <= 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "保存变更失败", "未输入变更内容", Icon_Type.Error, false, "", UrlType.JavaScript, "history.go(-1);");
            return;
        }

        string file = "";
        //现状描述附件处理
        FileUpLoadCommon fileUtility = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + UPLOADFOLDER, false);
        if (FileUpload_File.HasFile)
        {
            if (fileUtility.SaveFile(FileUpload_File, false))
            {
                file = fileUtility.NewFileName;
            }
            else
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "附件上传失败", new WebException(fileUtility.ErrorMsg), Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                return;
            }
        }
        modify.ContentAttechment = file;
        try
        {
            modify.DelayDays = int.Parse(TextBox_DelayDays.Text.Trim());
        }
        catch
        {
            EventMessage.MessageBox(Msg_Type.Error, "保存变更失败", "延长工期输入格式不正确", Icon_Type.Error, false, "", UrlType.JavaScript, "history.go(-1);");
            return;
        }
        modify.Remark = TextBox_Remark2.Text.Trim();

        if (modify.DetailList.Count <= 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "保存变更失败", "未添加变更列表", Icon_Type.Error, false, "", UrlType.JavaScript, "history.go(-1);");
            return;
        }
        modify.Status = SpecialProjectModifyStatus.DRAFT;
        try
        {
            specialProjectBll.SaveModify(modify);
        }
        catch(Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "保存变更失败", "保存变更失败，请重试", ex, Icon_Type.Error, true, "", UrlType.JavaScript, "history.go(-1);");
            return;
        }
        EventMessage.MessageBox(Msg_Type.Info, "变更提交成功", "变更提交成功，点击转到变更列表", Icon_Type.OK, false, "~/Module/FM2E/SpecialProject/ProjectManagement/Working/ModifyList.aspx?cmd=edit&projectid=" + id, UrlType.Href, "");
        
    }


    protected void DropDownList_JobItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_JobItems.SelectedValue != "0")
        {
            IList joblist = Session[sessionNameJob] as IList;
            SpecialProjectJobItemInfo job = null;
            foreach (SpecialProjectJobItemInfo item in joblist)
            {
                if (item.ItemID == long.Parse(DropDownList_JobItems.SelectedValue)) 
                {
                    job = item; break;
                }
            }
            if (job != null)
            {
                TextBox_Equipment.Text = job.Equipment;
                TextBox_Model.Text = job.Model;
                TextBox_Unit.Text = job.Unit;
                TextBox_UnitPrice.Text = job.UnitPrice.ToString("0.##");
            }
        }
    }

    /// <summary>
    /// 编辑一行的时候，进行保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        int index = int.Parse(Hidden_EditItemID.Value);

        SpecialProjectModifyInfo modify = GetSessionModify();

        SpecialProjectModifyDeviceInfo device = null;
        bool add = true;
        if (index == -1)//添加
        {
            device = new SpecialProjectModifyDeviceInfo();
            device.ItemID = 0;
            device.ModifyApplyID = modifyid;
            device.ProjectID = id;

            add = true;
        }
        else//编辑
        {
           
            if (modify.DetailList.Count > index)
            {
                device = modify.DetailList[index] as SpecialProjectModifyDeviceInfo;
                add = false;
            }
            else
            {
                device= new SpecialProjectModifyDeviceInfo();
                device.ItemID = 0;
                device.ModifyApplyID = modifyid;
                device.ProjectID = id;
                add = true;
            }
            
        }

        device.Model = TextBox_Model.Text.Trim();
        if (device.Model.Length <= 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "保存变更项失败", "未输入设备型号", Icon_Type.Error, false, "", UrlType.JavaScript, "history.go(-1);");
            return;
        }
        try
        {
            device.Count = decimal.Parse(TextBox_Count.Text.Trim());
        }
        catch
        {
            EventMessage.MessageBox(Msg_Type.Error, "保存变更项失败", "变更数量输入格式不正确", Icon_Type.Error, false, "", UrlType.JavaScript, "history.go(-1);");
            return;
        }
        device.DeviceName = TextBox_Equipment.Text.Trim();
        if (device.DeviceName.Length <= 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "保存变更项失败", "未输入设备名称", Icon_Type.Error, false, "", UrlType.JavaScript, "history.go(-1);");
            return;
        }
        device.IsAdd = RadioButton_Add.Checked;
        device.Remark = TextBox_Remark.Text.Trim();
        device.Unit = TextBox_Unit.Text.Trim();
        if (device.Unit.Length <= 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "保存变更项失败", "未输入单位", Icon_Type.Error, false, "", UrlType.JavaScript, "history.go(-1);");
            return;
        }
        try
        {
            device.Amount = decimal.Parse(TextBox_Amount.Text.Trim());
        }
        catch
        {
            EventMessage.MessageBox(Msg_Type.Error, "保存变更项失败", "变更金额输入格式不正确", Icon_Type.Error, false, "", UrlType.JavaScript, "history.go(-1);");
            return;
        }
        try
        {
            device.UnitPrice = decimal.Parse(TextBox_UnitPrice.Text.Trim());
        }
        catch
        {
            EventMessage.MessageBox(Msg_Type.Error, "保存变更项失败", "单价输入格式不正确", Icon_Type.Error, false, "", UrlType.JavaScript, "history.go(-1);");
            return;
        }



        if(add)
            modify.DetailList.Add(device);

        Session[sessionName] = modify;
        FillData();
    }
}
