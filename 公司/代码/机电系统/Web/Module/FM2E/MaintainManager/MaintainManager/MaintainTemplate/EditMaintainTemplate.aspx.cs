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

public partial class Module_FM2E_MaintainManager_MaintainManager_MaintainTemplate_EditMaintainTemplate : System.Web.UI.Page
{
    protected int CountPerRow = 5;
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 0, 0, DataType.Long);
    private readonly Maintain maintainBll = new Maintain();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
            ScriptManager1.RegisterAsyncPostBackControl(Button_AddEquipment);
        }
    }

    private void InitPage()
    {
        switch (cmd)
        {
            case "add":
                {
                    CurrentTempateSheet = null;
                    CurrentEquipmentHashTable = null;
                    break;
                }
            case "edit":
                {
                    CurrentTempateSheet = maintainBll.GetTemplateMaintainSheet(id);
                    break;
                }
            default:
                break;
        }
        //初始化界面
        DropDownList_FilterStatus.Items.Clear();
        DropDownList_FilterStatus.Items.AddRange(EnumHelper.GetListItems(typeof(EquipmentStatus)));

        //公司
        DropDownList_FilterCompany.Items.Clear();
        DropDownList_FilterCompany.Items.AddRange(ListItemHelper.GetCompanyListItemsWithBlank());

        //系统
        DDLSystem.Items.Clear();
        DDLSystem.Items.AddRange(ListItemHelper.GetSystemListItemsWithBlank());

        //类型
        DropDownList_Type.Items.Clear();
        DropDownList_Type.Items.AddRange(EnumHelper.GetListItems(typeof(MaintainType)));

        //周期
        DropDownList_PeriodUnit.Items.Clear();
        DropDownList_PeriodUnit.Items.AddRange(EnumHelper.GetListItems(typeof(MaintainIntervalUnit)));
        FillData();
    }

    /// <summary>
    /// 当前工作中的信息 
    /// </summary>
    private TemplateMaintainSheetInfo CurrentTempateSheet
    {
        get
        {
            TemplateMaintainSheetInfo item = (TemplateMaintainSheetInfo)Session[this.ToString()];
            if (item == null)
            {
                item = new TemplateMaintainSheetInfo();
                item.DepartmentID = UserData.CurrentUserData.DepartmentID;
                item.Modifier = UserData.CurrentUserData.UserName;
                item.AddressID = long.Parse(Hidden_AddressID.Value);
                item.IsNotUsed = false;
                item.IsTemp = CheckBox_IsTemp.Checked;
                item.MaintainType = (MaintainType)Convert.ToInt32(DropDownList_Type.SelectedValue);
                item.LastExecuteTime = DateTime.MinValue;
                item.Period = int.Parse(TextBox_Period.Text.Trim());
                item.PeriodUnit = (MaintainIntervalUnit)Convert.ToInt32(DropDownList_PeriodUnit.SelectedValue);
                item.Remark = taRemark.Value.Trim();
                item.SystemID = DDLSystem.SelectedValue;
                item.TemplateSheetID = 0;
                item.TemplateSheetName = TextBox_Name.Text.Trim();
                item.Equipments = new List<TemplateSheetEquipmentInfo>();
            }
            return item;
        }
        set
        {
            Session[this.ToString()] = value;
        }
    }

    protected void Button_Submit_Click(object sender, EventArgs e)
    {
        TemplateMaintainSheetInfo sheet = CurrentTempateSheet;
        sheet.SaveTime = DateTime.Now;
        sheet.Modifier = Common.Get_UserName;
        sheet.TemplateSheetName = TextBox_Name.Text.Trim();
        sheet.SystemID = DDLSystem.SelectedValue;
        sheet.IsTemp = CheckBox_IsTemp.Checked;
        sheet.Period = int.Parse(TextBox_Period.Text.Trim());
        sheet.PeriodUnit = (MaintainIntervalUnit)Convert.ToInt32(DropDownList_PeriodUnit.SelectedValue);
        sheet.Remark = taRemark.Value.Trim();
        sheet.MaintainType = (MaintainType)Convert.ToInt32(DropDownList_Type.SelectedValue);
        sheet.AddressID = long.Parse(Hidden_AddressID.Value);

        sheet.TemplateSheetID = maintainBll.SaveTemplateMaintainSheet(sheet);
        

        CurrentTempateSheet = sheet;
        Response.Redirect("MaintainTemplateList.aspx");
    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
        FillData();
        UpdatePanel_Edit.Update();
    }

    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("MaintainTemplateList.aspx");
    }



    protected void Repeater_EquipmentList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int index = int.Parse(e.CommandArgument.ToString());
        switch (e.CommandName)
        {
            //删除设备
            case "DeleteCMD":
                {
                    TemplateMaintainSheetInfo item = CurrentTempateSheet;
                    item.Equipments.RemoveAt(index);
                    FillData();
                    break;
                }
            default: break;
        }
    }

    private void FillData()
    {
        TextBox_Name.Text = CurrentTempateSheet.TemplateSheetName;
        TextBox_Address.Value = CurrentTempateSheet.AddressName;
        Hidden_AddressID.Value = CurrentTempateSheet.AddressID.ToString();
        Hidden_AddressCode.Value = CurrentTempateSheet.AddressCode;
        TextBox_Period.Text = CurrentTempateSheet.Period.ToString();
        taRemark.Value = CurrentTempateSheet.Remark;
        DDLSystem.SelectedValue = CurrentTempateSheet.SystemID;
        DropDownList_PeriodUnit.SelectedValue = ((int)CurrentTempateSheet.PeriodUnit).ToString();
        DropDownList_Type.SelectedValue = ((int)CurrentTempateSheet.MaintainType).ToString();
        CheckBox_IsTemp.Checked = CurrentTempateSheet.IsTemp;

        Repeater_EquipmentList.DataSource = CurrentTempateSheet.Equipments;
        Repeater_EquipmentList.DataBind();
    }

    protected void Repeater_EquipmentList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        HtmlTableCell tr = (HtmlTableCell)e.Item.FindControl("td_item");
        if (tr != null)
        {
            //鼠标移动到每项时颜色交替效果    
            tr.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            tr.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");
            //设置悬浮鼠标指针形状为"小手"    
            tr.Attributes["style"] = "Cursor:hand;text-align:left";
        }
    }

    /// <summary>
    /// 筛选设备
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_AddEquipment_Click(object sender, EventArgs e)
    {
        //判断是否有修改查询提交，即系统和地址
        //if (CurrentSearchInfo.AddressID!=long.Parse(Hidden_AddressID.Value)||
        //    CurrentSearchInfo.SystemID!=DDLSystem.SelectedValue
        //    )
        //{
        Label_FilterSystem.Text = DDLSystem.SelectedItem.Text;
        Label_FilterAddress.Text = TextBox_Address.Value.Trim();
            TextBox_FilterModel.Text = "";
            TextBox_FilterName.Text = "";
            DropDownList_FilterCompany.SelectedIndex = 0;
            DropDownList_FilterStatus.SelectedIndex = 0;

            AspNetPager1.CurrentPageIndex = 1;
            Filter();
        //}
        
        //筛选设备 
        ModalPopupExtender_AddItem.Show();
    }

    /// <summary>
    /// 根据条件筛选设备
    /// </summary>
    private void FillDataEquipment()
    {
        Equipment bll = new Equipment();
        EquipmentSearchInfo item = CurrentSearchInfo;

        QueryParam qp = bll.GenerateSearchTerm(item);
        qp.PageIndex = AspNetPager1.CurrentPageIndex;
        qp.PageSize = AspNetPager1.PageSize;
        
        
        int recordCount = 0;
        IList list = bll.GetList(qp, out recordCount, null);

        GridView1.DataSource = list;
        GridView1.DataBind();
        Hashtable hs = CurrentEquipmentHashTable;
        bool allselected = true;
        //已经选择的变成绿色
        for (int i = 0; i < list.Count; i++)
        {
            string equipmentNO = (list[i] as EquipmentInfoFacade).EquipmentNO;
             CheckBox cb = (CheckBox)GridView1.Rows[i].FindControl("CheckBox_Select");
             if (hs.Contains(equipmentNO))
            {
                GridView1.Rows[i].BackColor = System.Drawing.Color.LightSteelBlue;

                if (cb != null)
                    cb.Checked = true;
            }
            else
            {
                GridView1.Rows[i].BackColor = System.Drawing.Color.Transparent;
                if (cb != null)
                    cb.Checked = false;
                allselected = false;
            }
        }

        if (GridView1.HeaderRow != null)
        {
            CheckBox cball = (GridView1.HeaderRow.FindControl("CheckBox_SelectAll") as CheckBox);
            if (cball != null && allselected)
            {

                cball.Checked = true;
            }
            else
            {

                cball.Checked = false;
            }
        }


        AspNetPager1.RecordCount = recordCount;
    }

    /// <summary>
    /// HashTable
    /// </summary>
    private Hashtable CurrentEquipmentHashTable
    {
        get
        {
            Hashtable hs = (Hashtable)Session[this.ToString() + "Hashtable"];
            if (hs == null)
            {
                hs = new Hashtable(CurrentTempateSheet.Equipments.Count);
                foreach (TemplateSheetEquipmentInfo e in CurrentTempateSheet.Equipments)
                {
                    if(!string.IsNullOrEmpty(e.EquipmentID.ToString()))
                        hs.Add(e.EquipmentID.ToString(), e);
                }
            }
            return hs;
        }
        set { Session[this.ToString() + "Hashtable"] = value; }
    }

    /// <summary>
    /// 本页全选
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckBox_SelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = sender as CheckBox;
        TemplateMaintainSheetInfo sheet = CurrentTempateSheet;
        Hashtable hs = CurrentEquipmentHashTable;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            GridViewRow gvr = GridView1.Rows[i];
            //EquipmentInfoFacade eq = (EquipmentInfoFacade)gvr.DataItem;
            string equipmentNO = (gvr.FindControl("Label_EquipmentNO") as Label).Text;
            string name = (gvr.FindControl("Label_Name") as Label).Text;
            string model = (gvr.FindControl("Label_Model") as Label).Text;
            string detaillocation = (gvr.FindControl("Label_DetailLocation") as Label).Text;
            long equipmentid = Convert.ToInt64((gvr.FindControl("Label_EquipmentID") as Label).Text);
            long addressid = Convert.ToInt64((gvr.FindControl("Label_AddressID") as Label).Text);
            string addressname = (gvr.FindControl("Label_AddressName") as Label).Text;
            CheckBox cbs = (gvr.FindControl("CheckBox_Select") as CheckBox);
            if (cb.Checked)// 本页增加
            {
                if (!hs.Contains(equipmentid.ToString()))//如果已经有了，则不需要增加
                {
                    TemplateSheetEquipmentInfo tse = new TemplateSheetEquipmentInfo();
                    tse.DetailLocation = detaillocation;
                    tse.EquipmentModel = model;
                    tse.EquipmentName = name;
                    tse.EquipmentNO = equipmentNO;
                    tse.EquipmentID = equipmentid;
                    tse.AddressID = addressid;
                    tse.AddressName = addressname;
                    hs.Add(equipmentid.ToString(), tse);
                    sheet.Equipments.Add(tse);
                    cbs.Checked = true;
                    gvr.BackColor = System.Drawing.Color.LightSteelBlue;
                }
            }
            else
            {
                if (hs.Contains(equipmentid.ToString()))//如果原来没有的，则不需要再减少
                {
                    hs.Remove(equipmentid.ToString());
                    cbs.Checked = false;
                    foreach (TemplateSheetEquipmentInfo tse in sheet.Equipments)
                    {
                        if (tse.EquipmentID == equipmentid)
                        {
                            sheet.Equipments.Remove(tse);
                            break;
                        }
                    }
                    gvr.BackColor = System.Drawing.Color.Transparent;
                }
            }
        }
        CurrentEquipmentHashTable = hs;
        CurrentTempateSheet = sheet;
        FillData();
    }
    /// <summary>
    ///  选择一个
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckBox_Select_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = sender as CheckBox;
        GridViewRow gvr = (GridViewRow)cb.Parent.Parent;
        //EquipmentInfoFacade eq = (EquipmentInfoFacade)gvr.DataItem;
        string equipmentNO = (gvr.FindControl("Label_EquipmentNO") as Label).Text;
        string name = (gvr.FindControl("Label_Name") as Label).Text;
        string model = (gvr.FindControl("Label_Model") as Label).Text;
        string detaillocation = (gvr.FindControl("Label_DetailLocation") as Label).Text;
        long equipmentid = Convert.ToInt64((gvr.FindControl("Label_EquipmentID") as Label).Text);
        long addressid = Convert.ToInt64((gvr.FindControl("Label_AddressID") as Label).Text);
        string addressname = (gvr.FindControl("Label_AddressName") as Label).Text;
        TemplateMaintainSheetInfo sheet = CurrentTempateSheet;
        if (cb.Checked)//增加
        {
            TemplateSheetEquipmentInfo tse = new TemplateSheetEquipmentInfo();
            tse.DetailLocation = detaillocation;
            tse.EquipmentModel = model;
            tse.EquipmentName = name;
            tse.EquipmentNO = equipmentNO;
            tse.EquipmentID = equipmentid;
            tse.AddressID = addressid;
            tse.AddressName = addressname;
            Hashtable hs = CurrentEquipmentHashTable;
            hs.Add(equipmentid.ToString(), tse);
            CurrentEquipmentHashTable = hs;
            sheet.Equipments.Add(tse);
            gvr.BackColor = System.Drawing.Color.LightSteelBlue;
        }
        else//删除

        {
            Hashtable hs = CurrentEquipmentHashTable;
            hs.Remove(equipmentid.ToString());
            CurrentEquipmentHashTable = hs;
            foreach (TemplateSheetEquipmentInfo tse in sheet.Equipments)
            {
                if (tse.EquipmentID == equipmentid)
                {
                    sheet.Equipments.Remove(tse);
                    break;
                }
            }
            gvr.BackColor = System.Drawing.Color.Transparent;
        }
         CurrentTempateSheet = sheet;
         FillData();
    }
    

    private EquipmentSearchInfo CurrentSearchInfo
    {
        get
        {
            EquipmentSearchInfo item = (EquipmentSearchInfo)ViewState["SearchTerm"];
            if (item == null)
            {
                if (item == null)
                {
                    item = new EquipmentSearchInfo();
                }
                long addressid = Convert.ToInt64(Hidden_AddressID.Value);
                string addresscode = Hidden_AddressCode.Value;
                string systemid = DDLSystem.SelectedValue;
                //item.AddressID = addressid;
                item.AddressCode = addresscode;
          
                item.SystemID = systemid;   
            }
            return item;
        }
        set { ViewState["SearchTerm"] = value; }
    }


    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillDataEquipment();
    }

    protected void OnFilter(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        Filter();
    }

    private void Filter()
    {
        //long addressid = Convert.ToInt64(Hidden_AddressID.Value);
        string addresscode = Hidden_AddressCode.Value;
        string systemid = DDLSystem.SelectedValue;

        string name = Common.inSQL(TextBox_FilterName.Text.Trim());
        string model = Common.inSQL(TextBox_FilterModel.Text.Trim());
        
        EquipmentStatus status = (EquipmentStatus)Convert.ToInt32(DropDownList_FilterStatus.SelectedValue);
        string companyid = DropDownList_FilterCompany.SelectedValue;

        EquipmentSearchInfo item = new EquipmentSearchInfo();

        item.CompanyID = companyid;
        item.AddressCode = addresscode;
        //item.AddressID = addressid;
        item.Name = name;
        item.Model = model;
        item.SystemID = systemid;
        item.Status = status;

      
        AspNetPager1.CurrentPageIndex = 1;
       

        CurrentSearchInfo = item;

        FillDataEquipment();
    }

    /// <summary>
    /// 添加其他维护对象
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_AddOther_Click(object sender, EventArgs e)
    {
        TemplateSheetEquipmentInfo eq = new TemplateSheetEquipmentInfo();
        eq.EquipmentName = TextBox_OtherName.Text.Trim();
        eq.EquipmentModel = TextBox_OtherModel.Text.Trim();
        eq.DetailLocation = TextBox_OtherLocation.Text.Trim();

        TemplateMaintainSheetInfo sheet = CurrentTempateSheet;
        sheet.Equipments.Add(eq);
        CurrentTempateSheet = sheet;
        //CurrentTempateSheet.Equipments.Add(eq);
        FillData();

        TextBox_OtherLocation.Text = "";
        TextBox_OtherModel.Text = "";
        TextBox_OtherName.Text = "";


    }
}
