using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using FM2E.BLL.Equipment;
using WebUtility;
using WebUtility.Components;
using FM2E.Model.Equipment;
using FM2E.BLL.System;
using FM2E.Model.Exceptions;
using FM2E.Model.System;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Utils;

public partial class Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_ReturnEquipment_ReturnAcceptance : System.Web.UI.Page
{
    private readonly Secondment secondmentBll = new Secondment();
    private const string DETAILSESSION = "DetailList";
    private string companyID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove(DETAILSESSION);
            ViewState["action"] = "add";
            InitialPage();
        }


        //if (ddlResult.SelectedIndex == 0)
        //{
        //    EquipmentLocation.Attributes["style"] = "display:inline";
        //}
        //else
        //{
        //    EquipmentLocation.Attributes["style"] = "display:none";
        //}
    }
    private void InitialPage()
    {
        companyID = UserData.CurrentUserData.CompanyID;
        //Section sbll = new Section();
        //SectionInfo sectioninfo = new SectionInfo();
        //sectioninfo.CompanyID = companyID;
        //QueryParam sectionqp = sbll.GenerateSearchTerm(sectioninfo);
        //sectionqp.PageSize = 500;
        //int sectionrc = 0;
        //IList sectionlist = sbll.GetList(sectionqp, out sectionrc);
        //foreach (SectionInfo item1 in sectionlist)
        //{
        //    DDLSection.Items.Add(new ListItem(item1.SectionName, item1.SectionID));
        //}
        //DDLSection.Items.Insert(0, new ListItem("请选择路段", ""));
        //EquipmentSystem ebll = new EquipmentSystem();
        //IList elist = ebll.GetAllSystem();
        //foreach (EquipmentSystemInfo item2 in elist)
        //{
        //    DDLSystem.Items.Add(new ListItem(item2.SystemName, item2.SystemID));
        //}
        //DDLSystem.Items.Insert(0, new ListItem("请选择系统", ""));
        //CascadingDropDown6.Category = companyID;
    }
    /// <summary>
    /// 填充Gridview的数据
    /// </summary>
    private void FillData()
    {
        ArrayList list = (ArrayList)Session[DETAILSESSION];
        if (list == null)
            list = new ArrayList();

        GridView1.DataSource = list;
        GridView1.DataBind();
    }
    /// <summary>
    /// 添加需要归还的设备明细
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btAddDetail_Click(object sender, EventArgs e)
    {
        if (!ValidateInput())
            return;

        ReturnAcceptanceInfo item = new ReturnAcceptanceInfo();
        item.BorrowApplyID = (long)ViewState["BorrowApplyID"];
        item.ReturnCompany = ViewState["ReturnCompany"].ToString();
        item.EquipmentNO = tbEquipmentNO.Text.Trim();
        item.EquipmentName = lbEquipmentName.Text.Trim();
        item.Model = lbModel.Text.Trim();
        if (lbReturnDate.Text.Trim() != "")
            item.ReturnDate = Convert.ToDateTime(lbReturnDate.Text.Trim());

        if (lbBorrowTime.Text.Trim() != "")
            item.BorrowTime = Convert.ToDateTime(lbBorrowTime.Text.Trim());
        
        item.Result = ddlResult.SelectedValue == "0" ? false : true;
        item.FeeBack = tbFeeBack.Text.Trim();

        //获取位置的信息
        //item.SectionID = DDLSection.SelectedValue;
        //item.SystemID = DDLSystem.SelectedValue;
        //item.LocationID = tbLocationName.Text.Trim();
        //item.LocationTag = LocationTag.SelectedValue;
        item.AddressID = long.Parse(Hidden_AddressID.Value.Trim());
        item.AddressName = TextBox_Address.Value.Trim();
        item.DetailLocation = TextBox_DetailLocation.Value.Trim();

        ArrayList list = (ArrayList)Session[DETAILSESSION];
        if (list == null)
            list = new ArrayList();

        if (ViewState["action"] == null || ViewState["action"].ToString() == "add")
        {
            //检测列表中是否已有此项
            foreach (ReturnAcceptanceInfo it in list)
            {
                if (it.EquipmentNO == item.EquipmentNO)
                {
                    errMsg.Text = string.Format("错误：条形码为{0}的物品已存在列表中，不可重复归还验收", it.EquipmentNO);
                    return;
                }
            }
            list.Insert(0, item);
            ViewState["action"] = "add";
            btAddDetail.Text = "添加明细";
        }
        else if (ViewState["action"].ToString().Contains("edit"))
        {
            string tmp = "edit";
            int index = Convert.ToInt32(ViewState["action"].ToString().Substring(tmp.Length));
            list.RemoveAt(index);
            list.Insert(0, item);
            ViewState["action"] = "add";
            btAddDetail.Text = "添加明细";
        }
        Session[DETAILSESSION] = list;
        ClearInput();
        FillData();
    }
    /// <summary>
    /// 输入校验
    /// </summary>
    /// <returns></returns>
    private bool ValidateInput()
    {
        string errorMsg = "";
        if (tbEquipmentNO.Text.Trim() == string.Empty)
        {
            errorMsg = "设备条形码不能为空";
        }
        else if (lbEquipmentName.Text.Trim() == "找不到相应的设备")
        {
            errorMsg = "条形码为" + tbEquipmentNO.Text.Trim() + "的设备不存在";
        }
        else if (lbModel.Text.Trim() == "找不到相应的设备")
        {
            errorMsg = "条形码为" + tbEquipmentNO.Text.Trim() + "的设备不存在";
        }

        else if (ddlResult.SelectedValue == "0" && tbFeeBack.Text.Trim() == string.Empty)
        {
            errorMsg = "对于验收不通过的设备，需要输入验收备注";
        }
        else if (Hidden_AddressID.Value.Trim() == "")
        {
            errorMsg = "请选择归还放置地点";
        }

        if (errorMsg != "")
        {
            errMsg.Text = "输入有误："+errorMsg;
            EventMessage.EventWriteLog(Msg_Type.Error, errorMsg);
            return false;
        }
        return true;
    }
    /// <summary>
    /// 清空所有用户输入
    /// </summary>
    private void ClearInput()
    {
        tbEquipmentNO.Text = "";
        lbEquipmentName.Text = "";
        lbModel.Text = "";
        tbFeeBack.Text = "";
        ddlResult.SelectedIndex = 0;
        errMsg.Text = "";
        lbBorrowTime.Text = "";
        lbReturnDate.Text = "";
        //DDLSystem.SelectedIndex = 0;
        //DDLSection.SelectedIndex = 0;
        //tbLocationName.Text = "";
        //CascadingDropDown5.SelectedValue = "";
        //CascadingDropDown6.SelectedValue = "";
        //LocationID.Attributes["style"] = "display:inline";
        //tbLocationName.Attributes["style"] = "display:none";
        //EquipmentLocation.Attributes["style"] = "display:inline";
        Hidden_AddressID.Value = "";
        TextBox_Address.Value = "";
        TextBox_DetailLocation.Value = "";
    }
    /// <summary>
    /// 归还设备
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSubmit_Click(object sender, EventArgs e)
    {
        //先检查借用人的用户名与密码是否相符
        string errorMsg = "";
        if (tbReturner.Text.Trim() == "")
            errorMsg = "请输入领用人用户名";
        else if (tbPassword.Text.Trim() == "")
            errorMsg = "请输入领用人密码";

        if (errorMsg != "")
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入有误：" + errorMsg, Icon_Type.Error, false, "history.go(-1)", UrlType.JavaScript, "");
        }
        bool bValidate = false;
        User bll = new User();
        try
        {
            bValidate = bll.ValidatePassword(tbReturner.Text.Trim(), Common.md5(tbPassword.Text.Trim(), 32));
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "校验领用人用户名密码时发生错误", ex, Icon_Type.Error, false, "history.go(-1)", UrlType.JavaScript, "");
        }
        if (!bValidate)
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "校验领用人用户名密码不相符", Icon_Type.Error, false, "history.go(-1)", UrlType.JavaScript, "");

        try
        {
            ArrayList list = (ArrayList)Session[DETAILSESSION];
            if (list == null)
                throw new WebException("没有任何的归还验收信息");

            companyID = UserData.CurrentUserData.CompanyID;
            string checker = Common.Get_UserName;
            DateTime returnDate = DateTime.Now;
            string returner=tbReturner.Text.Trim();
            
            foreach (ReturnAcceptanceInfo item in list)
            {
                item.CompanyID = companyID;
                item.Checker = checker;
                item.ReturnDate = returnDate;
                item.Returner = returner;
            }
            secondmentBll.AddAcceptanceRecord(list);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存设备归还验收信息失败，请检查是否有输入归还验收信息", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        //清除Session
        Session.Remove(DETAILSESSION);
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", "保存设备归还验收信息成功！", Icon_Type.OK, false, Common.GetHomeBaseUrl("ReturnEquipment.aspx"), UrlType.Href, "");
    }
    
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowNum = Convert.ToInt32(e.CommandArgument);
        ArrayList list = (ArrayList)Session[DETAILSESSION];

        if (e.CommandName == "del")
        {
            //删除
            if (list == null) return;

            list.RemoveAt(rowNum);
            Session[DETAILSESSION] = list;
            FillData();
        }
        else if (e.CommandName == "view")
        {
            if (list == null || list.Count == 0)
                return;

            ReturnAcceptanceInfo item = (ReturnAcceptanceInfo)list[rowNum];
            tbEquipmentNO.Text = item.EquipmentNO;
            lbEquipmentName.Text = item.EquipmentName;
            lbModel.Text = item.Model;
            lbBorrowTime.Text = item.BorrowTime.ToString("yyyy-MM-dd HH:mm:ss");
            lbReturnDate.Text = item.ReturnDate.ToString("yyyy-MM-dd");
            ddlResult.SelectedValue = item.Result ? "1" :"0";
            tbFeeBack.Text = item.FeeBack;

            //填充位置信息
            //填充位置信息
            Hidden_AddressID.Value = item.AddressID.ToString();
            TextBox_Address.Value = item.AddressName;
            TextBox_DetailLocation.Value = item.DetailLocation;

            //DDLSystem.SelectedValue = item.SystemID;
            //DDLSection.SelectedValue = item.SectionID;
            //CascadingDropDown5.SelectedValue = item.LocationTag;
            //CascadingDropDown6.SelectedValue = item.LocationID;
            //tbLocationName.Text = item.LocationID;

            //if (item.LocationTag == "3")
            //{
            //    LocationID.Attributes["style"] = "display:none";
            //    tbLocationName.Attributes["style"] = "display:inline";
            //}
            //else
            //{
            //    LocationID.Attributes["style"] = "display:inline";
            //    tbLocationName.Attributes["style"] = "display:none";
            //}

            //if (ddlResult.SelectedIndex == 0)
            //{
            //    EquipmentLocation.Attributes["style"] = "display:inline";
            //}
            //else
            //{
            //    EquipmentLocation.Attributes["style"] = "display:none";
            //}

            ViewState["action"] = "edit" + rowNum;
            btAddDetail.Text = "更新明细";
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
        }

    }
    protected void tbEquipmentNO_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //if (LocationTag.SelectedValue == "3")
            //{
            //    LocationID.Attributes["style"] = "display:none";
            //    tbLocationName.Attributes["style"] = "display:inline";
            //}
            //else
            //{
            //    LocationID.Attributes["style"] = "display:inline";
            //    tbLocationName.Attributes["style"] = "display:none";
            //}

            string equipmentNO = tbEquipmentNO.Text.Trim();
            if (equipmentNO == "")
                return;

            lbEquipmentName.Text = "";
            lbModel.Text = "";
            lbBorrowTime.Text = "";
            lbReturnDate.Text = "";

            BorrowRecordInfo recordInfo = secondmentBll.GetEquipmentNotReturned(equipmentNO);
            if (recordInfo == null)
            {
                //没有此设备的借出记录
                btAddDetail.Enabled = false;
                errMsg.Text = "错误：找不到条形码为" + equipmentNO + "的设备的借出记录";
                return;
            }
            else
            {
                ViewState["BorrowApplyID"] = recordInfo.BorrowApplyID;
                ViewState["ReturnCompany"] = recordInfo.BorrowCompany;
                lbBorrowTime.Text = recordInfo.BorrowTime.ToString("yyyy-MM-dd HH:mm:ss");
                lbReturnDate.Text = recordInfo.ReturnDate.ToString("yyyy-MM-dd");
                errMsg.Text = "";
            }

            Equipment bll = new Equipment();
            EquipmentInfoFacade item = bll.GetEquipmentBYNO(equipmentNO);
            if (item != null)
            {
                lbEquipmentName.Text = item.Name;
                lbModel.Text = item.Model;
                btAddDetail.Enabled = true;
            }
            else
            {
                lbEquipmentName.Text = "找不到相应的设备";
                lbModel.Text = "找不到相应的设备";
                btAddDetail.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
}
