using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using WebUtility;
using FM2E.BLL.Maintain;
using FM2E.Model.Maintain;
using FM2E.Model.Utils;
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using System.Collections;

public partial class Module_FM2E_MaintainManager_MaintainManager_MaintainSheet_EditMaintainSheet : System.Web.UI.Page
{
    protected int CountPerRow = 5;
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 0, 0, DataType.Long);
    long templateid = (long)Common.sink("templateid", MethodType.Get, 0, 0, DataType.Long);
    private readonly Maintain maintainBll = new Maintain();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
        }
    }

    private void InitPage()
    {
        CurrentSheet = null;

        //其他设备状态
        DropDownList_OtherStatus.Items.Clear();
        DropDownList_OtherStatus.Items.AddRange(EnumHelper.GetListItemsEx(typeof(EquipmentStatus),(int)EquipmentStatus.Failure));

        FillData();
    }


    private void FillData()
    {
        MaintainSheetInfo item = CurrentSheet;
        Label_AddressName.Text = item.AddressName;
        Label_DeparmentName.Text = item.DepartmentName;
        Label_Period.Text = item.Period + " " + EnumHelper.GetDescription(item.PeriodUnit);
        Label_Remark.Text = item.Remark;
        Label_SystemName.Text = item.SystemName;
        Label_TemplateSheetName.Text = (string.IsNullOrEmpty(item.SheetNO) ? "" : item.SheetNO) + item.SheetName;
        Label_TypeName.Text = EnumHelper.GetDescription(item.MaintainType);
        Label_Maintainer.Text = item.MaintainerName;

        TextBox_MaintainTime.Text = item.MaintainTime.ToString("yyyy-MM-dd");

        Image_IsTemp.Visible = item.IsTemp;

        //正常的列表
        Repeater_EquipmentList.DataSource = item.Equipments;
        Repeater_EquipmentList.DataBind();

        //异常的
        GridView1.DataSource = item.AbnormalEquipments;
        GridView1.DataBind();

        for (int i = 0; i < item.AbnormalEquipments.Count; i++)
        {
            MaintainSheetEquipmentInfo eq = item.AbnormalEquipments[i] as MaintainSheetEquipmentInfo;
            GridViewRow gvr = GridView1.Rows[i];
            DropDownList status = gvr.FindControl("DropDownList_NewStatus") as DropDownList;
            status.Items.Clear();
            status.Items.AddRange(EnumHelper.GetListItemsEx(typeof(EquipmentStatus), (int)EquipmentStatus.Failure, (int)EquipmentStatus.Unknown));
            try { status.SelectedValue = ((int)eq.NewStatus).ToString(); }
            catch { }

            TextBox tbRemark = gvr.FindControl("TextBox_EquipmentRemark") as TextBox;
            tbRemark.Text = eq.Remark;
        }
    }

    /// <summary>
    /// 当前工作中的信息 
    /// </summary>
    private MaintainSheetInfo CurrentSheet
    {
        get
        {
            MaintainSheetInfo item = (MaintainSheetInfo)Session[this.ToString()];
            if (item == null)
            {
                item = new MaintainSheetInfo();
                switch (cmd)
                {
                    //填写的时候需要加载模板
                    case "add":
                        {
                            TemplateMaintainSheetInfo templatesheet = maintainBll.GetTemplateMaintainSheet(templateid);

                            item.AddressCode = templatesheet.AddressCode;
                            item.AddressID = templatesheet.AddressID;
                            item.AddressName = templatesheet.AddressName;
                            item.DepartmentID = UserData.CurrentUserData.DepartmentID;
                            item.DepartmentName = UserData.CurrentUserData.DepartmentName;
                            item.HasAbnormal = false;
                            item.Maintainer = UserData.CurrentUserData.UserName;
                            item.MaintainerName = UserData.CurrentUserData.PersonName;
                            item.MaintainType = templatesheet.MaintainType;
                            item.Period = templatesheet.Period;
                            item.PeriodUnit = templatesheet.PeriodUnit;
                            item.SheetName = templatesheet.TemplateSheetName;
                            item.MaintainTime = DateTime.Now;
                            item.LastExecuteTime = templatesheet.LastExecuteTime;
                            item.SystemID = templatesheet.SystemID;
                            item.SystemName = templatesheet.SystemName;
                            item.IsTemp = templatesheet.IsTemp;
                            item.SaveTime = templatesheet.SaveTime;
                            item.TemplateSheetID = templatesheet.TemplateSheetID;
                            item.Equipments = new List<MaintainSheetEquipmentInfo>();
                            item.ConfirmResult = MaintainConfirmResult.NotConfirm;
                            //维护设备
                            foreach (TemplateSheetEquipmentInfo tse in templatesheet.Equipments)
                            {
                                MaintainSheetEquipmentInfo eq = new MaintainSheetEquipmentInfo();
                                eq.DetailLocation = tse.DetailLocation;
                                eq.EquipmentModel = tse.EquipmentModel;
                                eq.EquipmentName = tse.EquipmentName;
                                eq.EquipmentNO = tse.EquipmentNO;
                                eq.EquipmentID = tse.EquipmentID;
                                eq.AddressID = tse.AddressID;
                                eq.AddressName = tse.AddressName;
                                eq.IsNormal = true;
                                eq.NewStatus = EquipmentStatus.Normal;
                                eq.Remark = "";
                                item.Equipments.Add(eq);
                            }
                            

                            break;
                        }
                    case "edit":
                        {
                            item = maintainBll.GetMaintainSheet(id);
                            break;
                        }
                    default:
                        break;
                }
            }
            return item;
        }
        set
        {
            Session[this.ToString()] = value;
        }
    }

    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Submit_Click(object sender, EventArgs e)
    {
        MaintainSheetInfo sheet = CurrentSheet;
        CollectAbnormal();
        CollectAbnormalItems();
        if (sheet.AbnormalEquipments.Count > 0)
            sheet.HasAbnormal = true;//含有异常设备

        sheet.MaintainTime = DateTime.Parse(TextBox_MaintainTime.Text.Trim());
        sheet.Remark = TextBox_MaintainRemark.Text.Trim();
        sheet.SheetNO = FM2E.BLL.Utils.SheetNOGenerator.GetSheetNO(UserData.CurrentUserData.CompanyID, SheetType.MAINTAINSHEET);
        sheet.SheetID = maintainBll.SaveMaintainSheet(sheet);
        CurrentSheet = sheet;
        Response.Redirect("MaintainSheetList.aspx");
    }
    /// <summary>
    /// 收集异常设备信息
    /// </summary>
    private void CollectAbnormal()
    {
        //MaintainSheetInfo sheet = CurrentSheet;
        //for (int i = 0; i < sheet.Equipments.Count; i++) 
        //{
        //    MaintainSheetEquipmentInfo mse = sheet.Equipments[i] as MaintainSheetEquipmentInfo;
        //    if (!mse.IsNormal)
        //    {
        //        RepeaterItem ri = Repeater_EquipmentList.Items[i];
        //        //新状态
        //        DropDownList ddl = ri.FindControl("DropDownList_NewStatus") as DropDownList;
        //        EquipmentStatus newstatus = (EquipmentStatus)Convert.ToInt32(ddl.SelectedValue);

        //        //描述
        //        string remark = (ri.FindControl("TextBox_EquipmentRemark") as TextBox).Text.Trim();
        //        mse.NewStatus = newstatus;
        //        mse.Remark = remark;
        //    }
        //   //只需要收集
        //}
        //CurrentSheet = sheet;
    }

    /// <summary>
    /// 删除设备
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void Repeater_EquipmentList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        CollectAbnormalItems();
        int index = int.Parse(e.CommandArgument.ToString());
        switch (e.CommandName)
        {
            //删除设备
            case "DeleteCMD":
                {
                    MaintainSheetInfo item = CurrentSheet;
                    item.Equipments.RemoveAt(index);
                    
                    CurrentSheet = item;
                    
                    FillData();
                    break;
                }
            default: break;
        }
        //UpdatePanel_Edit.Update();
    }

    

    protected void Repeater_EquipmentList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("td_item");
        if (tr != null)
        {
            //鼠标移动到每项时颜色交替效果    
            tr.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            tr.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");
            //设置悬浮鼠标指针形状为"小手"    
            tr.Attributes["style"] = "Cursor:hand";
        }
        //选中

        RadioButtonList rbl = e.Item.FindControl("RadioButtonList_Normal") as RadioButtonList;
        if (rbl != null)
        {
            rbl.SelectedValue = (e.Item.DataItem as MaintainSheetEquipmentInfo).IsNormal ? "1" : "0";

            
        }

        //HtmlGenericControl span = (HtmlGenericControl)e.Item.FindControl("span_input");
        //span.Visible = !(e.Item.DataItem as MaintainSheetEquipmentInfo).IsNormal;
    }

    protected void GridView1_OnRowDataBound(object sender,GridViewRowEventArgs e)
    {
        
    }

    protected void GridView1_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        
    }

    private void CollectAbnormalItems()
    {
        MaintainSheetInfo sheet = CurrentSheet;
        for (int i = 0; i < sheet.AbnormalEquipments.Count; i++)
        {
            MaintainSheetEquipmentInfo eq = sheet.AbnormalEquipments[i] as MaintainSheetEquipmentInfo;
            GridViewRow gvr = GridView1.Rows[i];
            DropDownList status = gvr.FindControl("DropDownList_NewStatus") as DropDownList;
            TextBox tbRemark = gvr.FindControl("TextBox_EquipmentRemark") as TextBox;

            eq.NewStatus = (EquipmentStatus)Convert.ToInt32(status.SelectedValue);
            eq.Remark = tbRemark.Text.Trim();
        }
        CurrentSheet = sheet;
    }

    /// <summary>
    /// 添加其他维护对象
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_AddOther_Click(object sender, EventArgs e)
    {
        CollectAbnormalItems();
        MaintainSheetEquipmentInfo eq = new MaintainSheetEquipmentInfo();
        eq.EquipmentNO = TextBox_OtherEquipmentNO.Text.Trim();
        eq.EquipmentName = TextBox_OtherName.Text.Trim();
        eq.EquipmentModel = TextBox_OtherModel.Text.Trim();
        eq.DetailLocation = TextBox_OtherLocation.Text.Trim();
        eq.IsNormal = false;
        eq.Remark = TextBox_OtherRemark.Text.Trim();
        eq.IsExtra = true;
        eq.NewStatus = (EquipmentStatus)Convert.ToInt32(DropDownList_OtherStatus.SelectedValue);
        //判断是否已经存在
        bool exist = false;
        MaintainSheetInfo sheet = CurrentSheet;
        foreach (MaintainSheetEquipmentInfo mse in sheet.Equipments)
        {
            if (!string.IsNullOrEmpty(eq.EquipmentNO)&&mse.EquipmentNO == eq.EquipmentNO)
            {
                exist = true;
                break;
            }
        }


        if (!exist)
        {
            sheet.Equipments.Add(eq);

            FillData();

            TextBox_OtherEquipmentNO.Text = "";
            TextBox_OtherLocation.Text = "";
            TextBox_OtherModel.Text = "";
            TextBox_OtherName.Text = "";
            TextBox_OtherRemark.Text = "";
            Label_ErrMsg.Text = "";

            // UpdatePanel_Edit.Update();
        }
        else
        {
            Label_ErrMsg.Text = eq.EquipmentNO + "已经存在，无需再添加";
        }
        CurrentSheet = sheet;
        CollectAbnormalItems();
        FillData();
        //UpdatePanel_Edit.Update();
    }

    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../MaintainTemplate/MaintainTemplateList.aspx");
    }

    /// <summary>
    /// 选择异常以及正常
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioButtonList_Normal_Changed(object sender, EventArgs e)
    {

        CollectAbnormalItems();

        RadioButtonList rbl = (RadioButtonList)sender;
        RepeaterItem ri = rbl.Parent as RepeaterItem;
        HtmlGenericControl span = (HtmlGenericControl)ri.FindControl("span_input");
        MaintainSheetInfo sheet = CurrentSheet;
        MaintainSheetEquipmentInfo eq = (sheet.Equipments[ri.ItemIndex] as MaintainSheetEquipmentInfo);
        if(rbl.SelectedValue=="1")//正常
        {
            eq.IsNormal = true;
            //span.Visible = false;
        }
        else//异常
        {
            //span.Visible = true;
            eq.IsNormal = false;
        }
        CurrentSheet = sheet;
        
        FillData();
        
        //UpdatePanel_Edit.Update();
    }

    protected void ordertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        MaintainSheetInfo item = CurrentSheet;
        if (item.Equipments == null || item.AbnormalEquipments == null)
            return;
        if (ordertype.SelectedValue.Equals("address"))
        {
            Repeater_EquipmentList.DataSource = item.Equipments;
            Repeater_EquipmentList.DataBind();
        }
        else if (ordertype.SelectedValue.Equals("name"))
        {

            Repeater_EquipmentList.DataSource = new IListSort<MaintainSheetEquipmentInfo>((IList<MaintainSheetEquipmentInfo>)item.Equipments, "EquipmentName").Sort();
            Repeater_EquipmentList.DataBind();

        }
    }
}
