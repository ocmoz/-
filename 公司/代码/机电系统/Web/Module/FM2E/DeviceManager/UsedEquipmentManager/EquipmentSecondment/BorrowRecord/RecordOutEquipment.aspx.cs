using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using WebUtility.Components;
using System.Collections;
using FM2E.BLL.System;
using FM2E.Model.Exceptions;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using System.Web.UI.HtmlControls;

public partial class Module_FM2E_DeviceManager_UsedEquipmentManager_EquipmentSecondment_BorrowRecord_RecordOutEquipment : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 20, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 20, 0, DataType.Long);
    private readonly Secondment secondmentBll = new Secondment();
    private const string RECORDLIST_SESSION = "RecordList";
    private const string APPLYLIST_SESSION = "ApplyList";


    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            FillData();
            InitialPage();
            BindReocrdDetail();
        }
    }
    private void InitialPage()
    {
        string companyID = (string)ViewState["BorrowCompanyID"];
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
    /// 加载页面数据
    /// </summary>
    private void FillData()
    {
        try
        {
            if (cmd == "add")
            {
                BorrowApplyInfo item = secondmentBll.GetBorrowApply(id);
                if (item == null)
                    return;

                lbSheetName.Text = item.SheetName;
                lbLendCompany.Text = item.CompanyName;
                ViewState["CompanyID"] = item.CompanyID;
                lbBorrowCompany.Text = item.BorrowCompanyName;
                ViewState["BorrowCompanyID"] = item.BorrowCompanyID;    //保存借用方公司ID
                lbApplicant.Text = item.ApplicantName;
                lbStatus.Text = item.StatusString;
                lbSubmitTime.Text = item.SubmitTime.ToString("yyyy-MM-dd HH:mm:ss");

                //保存申请项到Session中
                Session[APPLYLIST_SESSION] = item.DetailList;
                Session.Remove(RECORDLIST_SESSION);
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "加载页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 为每一项的申请绑定借出登记信息
    /// </summary>
    private void BindReocrdDetail()
    {
        try
        {
            IList list = (IList)Session[APPLYLIST_SESSION];
            if (list == null)
            {
                //Session丢失,从数据库中重新加载
                BorrowApplyInfo item = secondmentBll.GetBorrowApply(id);
                if (item == null)
                    return;

                //保存申请项到Session中
                Session[APPLYLIST_SESSION] = item.DetailList;
                list = item.DetailList;
            }
            Repeater1.DataSource = list;
            Repeater1.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "绑定借出登记信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        BorrowApplyDetailInfo recordItem = (BorrowApplyDetailInfo)e.Item.DataItem;
        ArrayList list = (ArrayList)Session[RECORDLIST_SESSION];
        if (list == null || list.Count == 0)
            return;

        ArrayList subList = new ArrayList();
        foreach (BorrowRecordInfo item in list)
        {
            if (item.BorrowApplyID == recordItem.BorrowApplyID && item.ItemID == recordItem.ItemID)
                subList.Add(item);
        }
        if (subList.Count > 0)
        {
            Repeater Repeater2 = (Repeater)e.Item.FindControl("Repeater2");
            Repeater2.DataSource = subList;
            Repeater2.DataBind();
        }
    }
    /// <summary>
    /// 借出登记命令
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Record")
        {
            ClearInput();
            TabContainer1.Tabs[0].Visible = false;
            TabContainer1.Tabs[1].Visible = true;
            tbEquipmentNO.Focus();

            Literal ltItemID = ((Literal)Repeater1.Items[index].FindControl("ltItemID"));
            long itemID=0;
            if (ltItemID != null)
            {
                ViewState["EditingItemID"] =itemID= Convert.ToInt64(ltItemID.Text.Trim());
            }
            else ViewState["EditingItemID"] =itemID= index + 1;

            Literal ltEquipmentName = ((Literal)Repeater1.Items[index].FindControl("ltEquipmentName"));
            string equipmentName = "";
            if (ltEquipmentName != null)
            {
                equipmentName = ltEquipmentName.Text.Trim();
            }

            Literal ltModel = ((Literal)Repeater1.Items[index].FindControl("ltModel"));
            string model = "";
            if (ltModel != null)
            {
                model = ltModel.Text.Trim();
            }

            Literal ltCount = ((Literal)Repeater1.Items[index].FindControl("ltCount"));
            if (ltCount != null)
            {
                int count = Convert.ToInt32(ltCount.Text.Trim());
                if (FindRelatedRecord(itemID) >= count)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", string.Format("型号为 {0} 的设备 {1}只能借出{2}个",model,equipmentName,count), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                }

            }

            //填充借用原因和归还日期
            Literal ltReturnDate = ((Literal)Repeater1.Items[index].FindControl("ltReturnDate"));
            if (ltReturnDate != null)
            {
                tbReturnDate.Text = ltReturnDate.Text;
            }

            Literal ltReason = ((Literal)Repeater1.Items[index].FindControl("ltReason"));
            if (ltReason != null)
            {
                tbReason.Text = ltReason.Text;
            }


            //使用地点
            HtmlInputHidden hdAddressID = (HtmlInputHidden)Repeater1.Items[index].FindControl("Hidden_AddressID");
            Hidden_AddressID.Value = hdAddressID.Value;

            Literal ltAddressName = ((Literal)Repeater1.Items[index].FindControl("ltAddressName"));
            if (ltAddressName != null)
            {
                TextBox_Address.Value = ltAddressName.Text;
            }

            Literal ltDetailLocation = ((Literal)Repeater1.Items[index].FindControl("DetailLocation"));
            if (ltDetailLocation != null)
            {
                TextBox_DetailLocation.Value = ltDetailLocation.Text;
            }


            ViewState["action"] = "add";
        }

    }
    /// <summary>
    /// 找出ItemID相应的借出记录项
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns></returns>
    private int FindRelatedRecord(long itemID)
    {
        int count = 0;
        ArrayList list = (ArrayList)Session[RECORDLIST_SESSION];
        if (list == null || list.Count == 0)
            return 0;
        foreach (BorrowRecordInfo item in list)
        {
            if (item.ItemID == itemID)
                count++;
        }

        return count;
    }
    /// <summary>
    /// 添加借出记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btRecord_Click(object sender, EventArgs e)
    {
        ValidateInput();

        ArrayList list = (ArrayList)Session[RECORDLIST_SESSION];
        if (list == null)
            list = new ArrayList();

        BorrowRecordInfo item = new BorrowRecordInfo();
        item.BorrowApplyID = id;
        item.ItemID = (long)ViewState["EditingItemID"];
        item.EquipmentNO = tbEquipmentNO.Text.Trim();
        item.EquipmentName = tbEquipmentName.Text.Trim();
        item.Model = tbModel.Text.Trim();
        item.ReturnDate = Convert.ToDateTime(tbReturnDate.Text.Trim());
        item.Reason = tbReason.Text.Trim();

        //获取位置的信息
        //item.SectionID = DDLSection.SelectedValue;
        //item.SystemID = DDLSystem.SelectedValue;
        //item.LocationID = tbLocationName.Text.Trim();
        //item.LocationTag = LocationTag.SelectedValue;
        item.AddressID = long.Parse(Hidden_AddressID.Value);
        item.AddressName = TextBox_Address.Value.Trim();
        item.DetailLocation = TextBox_DetailLocation.Value.Trim();

        if (ViewState["action"].ToString() == "add")
        {
            list.Insert(0, item);
        }
        else if (ViewState["action"].ToString() == "edit")
        {
            int index = (int)ViewState["index"];
            list.RemoveAt(index);
            list.Insert(0, item);
        }

        ClearInput();

        Session[RECORDLIST_SESSION] = list;
        BindReocrdDetail();
        TabContainer1.Tabs[1].Visible = false;
        TabContainer1.Tabs[0].Visible = true;
    }
    /// <summary>
    /// 输入校验
    /// </summary>
    private void ValidateInput()
    {
        string errorMsg = "";
        if (tbEquipmentNO.Text.Trim() == "")
        {
            errorMsg = "设备条形码不能为空";
        }
        else if (tbEquipmentName.Text.Trim() == "")
        {
            errorMsg = "设备名称不能为空";
        }
        else if (tbModel.Text.Trim() == "")
        {
            errorMsg = "规格型号不能为空";
        }
        else if (tbReturnDate.Text.Trim() == "")
        {
            errorMsg = "归还日期不能为空";
        }
        else if (tbReason.Text.Trim().Length > 50)
        {
            errorMsg = "借用原因过长";
        }
        else if (Hidden_AddressID.Value.Trim() == "")
        {
            errorMsg = "请选择使用地址";
        }

        try
        {
            Convert.ToDateTime(tbReturnDate.Text.Trim());
        }
        catch
        {
            errorMsg = "归还日期格式不正确";
        }

        ArrayList list = (ArrayList)Session[RECORDLIST_SESSION];
        if (list != null && list.Count > 0&&ViewState["action"].ToString()=="add")
        {
            string equipmentNO = tbEquipmentNO.Text.Trim();
            foreach (BorrowRecordInfo item in list)
            {
                if (item.EquipmentNO == equipmentNO)
                {
                    errorMsg = "条形码为" + equipmentNO + "的设备不能重复登记";
                    break;
                }
            }
        }

        if (errorMsg != "")
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入有误：" + errorMsg, Icon_Type.Error, false, "history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 取出借出登记
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btCancel_Click(object sender, EventArgs e)
    {
        ClearInput();
        TabContainer1.Tabs[1].Visible = false;
        TabContainer1.Tabs[0].Visible = true;
    }
    /// <summary>
    /// 保存借出登记信息到数据库中
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSubmit_Click(object sender, EventArgs e)
    {
        //先检查借用人的用户名与密码是否相符
        string errorMsg = "";
        if (tbBorrower.Text.Trim() == "")
            errorMsg = "请输入领用人用户名";
        else if (tbPassword.Text.Trim() == "")
            errorMsg = "请输入领用人密码";

        if (errorMsg != "")
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入有误："+errorMsg, Icon_Type.Error, false, "history.go(-1)", UrlType.JavaScript, "");
        }
        bool bValidate=false;
        try
        {
            User bll = new User();
            bValidate = bll.ValidatePassword(tbBorrower.Text.Trim(), Common.md5(tbPassword.Text.Trim(),32));
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "校验领用人用户名密码时发生错误", ex, Icon_Type.Error, false, "history.go(-1)", UrlType.JavaScript, "");
        }
        if(!bValidate)
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "校验领用人用户名密码不相符", Icon_Type.Error, false, "history.go(-1)", UrlType.JavaScript, "");

        try
        {
            ArrayList list = (ArrayList)Session[RECORDLIST_SESSION];
            if (list == null)
                throw new WebException("没有任何的借出登记信息");

            string companyID = (string)ViewState["CompanyID"];
            string borrowCompanyID = (string)ViewState["BorrowCompanyID"];
            string borrower = tbBorrower.Text.Trim();
            string recorder = Common.Get_UserName;
            foreach (BorrowRecordInfo item in list)
            {
                item.BorrowApplyID = id;
                item.CompanyID = companyID;
                item.BorrowTime = DateTime.Now;
                item.BorrowCompany = borrowCompanyID;
                item.Borrower = borrower;
                item.Recorder = recorder;
                item.IsReturned = false;
            }
            secondmentBll.AddBorrowRecord(list);
            secondmentBll.ChangeStatus(id, BorrowApplyStatus.HasLent);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存设备借出登记信息失败，请检查是否有输入借出登记信息",ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        //清除Session
        Session.Remove(RECORDLIST_SESSION);
        Session.Remove(APPLYLIST_SESSION);
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", "保存设备借出登记信息成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("BorrowRecord.aspx"), UrlType.Href, "");
    }

    /// <summary>
    /// 清空用户输入
    /// </summary>
    private void ClearInput()
    {
        tbEquipmentNO.Text = "";
        tbEquipmentName.Text = "";
        tbModel.Text = "";
        tbReturnDate.Text = "";
        tbReason.Text = "";
        TextBox_Address.Value = "";
        TextBox_DetailLocation.Value = "";
        Hidden_AddressID.Value = "";
        //DDLSystem.SelectedIndex = 0;
        //DDLSection.SelectedIndex = 0;
        //tbLocationName.Text = "";
        //CascadingDropDown5.SelectedValue = "";
        //CascadingDropDown6.SelectedValue = "";
        //LocationID.Attributes["style"] = "display:inline";
        //tbLocationName.Attributes["style"] = "display:none";
    }
    /// <summary>
    /// 删除或修改借出登记信息的命令处理方法
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string equipmentNO = (string)e.CommandArgument;
        BorrowRecordInfo recordInfo = null;
        int index = 0;

        ArrayList list = (ArrayList)Session[RECORDLIST_SESSION];
        if (list == null) return;
        //在list中找出相应的项
        foreach (BorrowRecordInfo item in list)
        {
            if (item.EquipmentNO == equipmentNO)
            {
                recordInfo = item;
                break;
            }
            index++;
        }

        if (e.CommandName == "Edit")
        {
            if (recordInfo == null) return;
            tbEquipmentNO.Text = recordInfo.EquipmentNO;
            tbEquipmentName.Text = recordInfo.EquipmentName;
            tbModel.Text = recordInfo.Model;
            tbReturnDate.Text = recordInfo.ReturnDate.ToString("yyyy-MM-dd");
            tbReason.Text = recordInfo.Reason;

            //填充位置信息
            //DDLSystem.SelectedValue = recordInfo.SystemID;
            //DDLSection.SelectedValue = recordInfo.SectionID;
            //CascadingDropDown5.SelectedValue = recordInfo.LocationTag;
            //CascadingDropDown6.SelectedValue = recordInfo.LocationID;
            //tbLocationName.Text = recordInfo.LocationID;

            //if (recordInfo.LocationTag == "3")
            //{
            //    LocationID.Attributes["style"] = "display:none";
            //    tbLocationName.Attributes["style"] = "display:inline";
            //}
            //else
            //{
            //    LocationID.Attributes["style"] = "display:inline";
            //    tbLocationName.Attributes["style"] = "display:none";
            //}
            Hidden_AddressID.Value = recordInfo.AddressID.ToString();
            TextBox_Address.Value = recordInfo.AddressName;
            TextBox_DetailLocation.Value = recordInfo.DetailLocation;

            ViewState["action"] = "edit";
            ViewState["index"] = index;
            TabContainer1.Tabs[0].Visible = false;
            TabContainer1.Tabs[1].Visible = true;
        }
        else if (e.CommandName == "Delete")
        {
            list.RemoveAt(index);
            BindReocrdDetail();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TBEquipmentNO_TextChanged(object sender, EventArgs e)
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

            tbEquipmentName.Text = "";
            tbModel.Text = "";

            BorrowRecordInfo recordInfo = secondmentBll.GetEquipmentNotReturned(equipmentNO);
            if (recordInfo != null)
            {
                //此设备已借出
                btRecord.Enabled = false;
                tbEquipmentName.Text = "此设备已借出";
                tbModel.Text = "此设备已借出";
                return;
            }

            Equipment bll = new Equipment();
            EquipmentInfoFacade item = bll.GetEquipmentBYNO(equipmentNO);
            if (item != null)
            {
                tbEquipmentName.Text = item.Name;
                tbModel.Text = item.Model;
                btRecord.Enabled = true;
            }
            else
            {
                tbEquipmentName.Text = "找不到相应的设备";
                tbModel.Text = "找不到相应的设备";
                btRecord.Enabled = false;
            }
            
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
}
