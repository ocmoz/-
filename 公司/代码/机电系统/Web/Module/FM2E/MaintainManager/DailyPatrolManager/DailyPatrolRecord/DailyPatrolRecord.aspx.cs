using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WebUtility;
using WebUtility.Components;
using WebUtility.WebControls;

using FM2E.Model.Maintain;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.Model.Equipment;

using FM2E.WorkflowLayer;

using FM2E.BLL.Basic;
using FM2E.BLL.Maintain;
using FM2E.BLL.Equipment;


public partial class Module_FM2E_MaintainManager_DailyPatrolManager_DailyPatrolRecord_DailyPatrolRecord : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);  //ItemID
    string addresscode = (string)Common.sink("addresscode", MethodType.Get, 50, 0, DataType.Str);
    int allselectlabel = 0;
    private const string EQUIPMENTLIST = "MaintainRecordRecordEquimentList";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
        }
    }

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        try
        {
            Session.Remove(EQUIPMENTLIST);
            //巡查人
            Label_Patroler.Text = UserData.CurrentUserData.PersonName;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 初始化GridView等数据,已执行日常巡查信息
    /// </summary>
    private void FillData()
    {
        try
        {
            MaintainPlanRecord rbll = new MaintainPlanRecord();
            MaintainPlanRecordInfo info = new MaintainPlanRecordInfo();
            info.PlanType = MaintainPlanType.DailyPatrol;
            int listCount = 0;
            QueryParam searchTerm = rbll.GenerateSearchTerm(info);
            searchTerm.PageIndex = AspNetPager2.CurrentPageIndex;
            IList list = rbll.GetList(searchTerm, out listCount);
            AspNetPager2.RecordCount = listCount;
            GridView2.DataSource = list;
            GridView2.DataBind();

            LblErrorMessage.Text = "";
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    /// <summary>
    /// 初始化GridView等数据,已执行日常巡查任务设备信息
    /// </summary>
    private void FillDataEquipment(int page)
    {
        try
        {
            MaintainPlanConfig bll = new MaintainPlanConfig();
            MaintainPlanConfigInfo item = new MaintainPlanConfigInfo();
            if (id != 0)
            {
                item.ItemID = id;
                QueryParam qp = null;
                if (Hidden_AddressID.Value.ToString() == "")
                {
                    qp = bll.GenerateSearchTermForEquipmentList(item);
                }
                else
                {
                    long addressid = Convert.ToInt64(Hidden_AddressID.Value.ToString());
                    Address abll = new Address();
                    AddressInfo aitem = abll.GetAddress(addressid);
                    string address = aitem.AddressCode;
                    qp = bll.GenerateSearchTermForEquipmentAddressList(item, address);
                    addresscode = address;
                }
                if (page != 0)
                {
                    AspNetPager1.CurrentPageIndex = 1;
                }
                qp.PageIndex = AspNetPager1.CurrentPageIndex;
                qp.PageSize = AspNetPager1.PageSize;
                int recordCount = 0;
                IList elist = bll.GetListForEquipmentList(qp, out recordCount);
                AspNetPager1.RecordCount = recordCount;
                GridView1.DataSource = elist;
                GridView1.DataBind();

                //SetSelectedAll();  //全选
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化设备数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 添加记录或更新记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!ValidateDetailInput())
            return;
        bool bSuccess = false;
        MaintainPlanConfig configbll = new MaintainPlanConfig();
        if (Button1.Text == "更新记录")
        {
            try
            {
                CollectSelected();
                MaintainPlanRecord recordbll = new MaintainPlanRecord();
                MaintainPlanRecordInfo info = recordbll.GetMaintainPlanRecord(Convert.ToInt64(ViewState["EditRecordID"]));
                info.RecordDate = Convert.ToDateTime(TBRecordDate.Text.Trim());
                info.RecordmanID = UserData.CurrentUserData.UserName;// TBRecordmanID.Text.Trim();
                info.RecordResult = taRecordResult.Value.Trim();
                info.RecordRemark = taRemark.Value.Trim();
                info.AddressID = Convert.ToInt64(Hidden_AddressID.Value.Trim());
                info.EquipmentList = this.SelectedItems;
                info.PlanType = MaintainPlanType.DailyPatrol;
                recordbll.UpdateMaintainPlanRecord(info);
                bSuccess = true;
                Button1.Text = "添加记录";
                taRecordResult.Value = taRemark.Value = "";
                TBRecordDate.Text = "";// TBRecordmanID.Text = "";
                CascadingDropDown1.SelectedValue = "";
                CascadingDropDown2.SelectedValue = "";
                CascadingDropDown3.SelectedValue = "";
                Hidden_AddressID.Value = "";
                TextBox_Address.Value = "";
                id = 0;
                FillDataEquipment(1);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "更新记录失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        else
        {
            try
            {
                CollectSelected();
                MaintainPlanRecordInfo info = new MaintainPlanRecordInfo();
                info.ItemID = Convert.ToInt64(DDLRecordObject.SelectedValue);
                MaintainPlanConfigInfo detail = configbll.GetMaintainPlanConfig(info.ItemID);
                info.RecordContent = detail.PlanContent;
                info.RecordObject = detail.PlanObject;
                info.RecordDate = Convert.ToDateTime(TBRecordDate.Text.Trim());
                info.RecordmanID = UserData.CurrentUserData.UserName;// TBRecordmanID.Text.Trim();
                info.RecordResult = taRecordResult.Value.Trim();
                info.RecordRemark = taRemark.Value.Trim();
                info.AddressID = Convert.ToInt64(Hidden_AddressID.Value.Trim());
                info.EquipmentList = this.SelectedItems;
                info.VerifiedResult = MaintainPlanVerifiedResult.NotVerified;
                info.VerifyBy = "";
                info.VerifyName = "";
                info.VerifyRemark = "";
                info.PlanType = MaintainPlanType.DailyPatrol;
                MaintainPlanRecord recordbll = new MaintainPlanRecord();
                recordbll.InsertMaintainPlanRecord(info);
                taRecordResult.Value = taRemark.Value = "";
                TBRecordDate.Text = "";// TBRecordmanID.Text = "";
                CascadingDropDown1.SelectedValue = "";
                CascadingDropDown2.SelectedValue = "";
                CascadingDropDown3.SelectedValue = "";
                Hidden_AddressID.Value = "";
                TextBox_Address.Value = "";
                id = 0;
                FillDataEquipment(1);
                this.SelectedItems = null;
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加记录失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        if (bSuccess)
        {
            FillData();
            id = 0;
        }
    }

    /// <summary>
    /// 校验添加明细时的输入是否合法
    /// </summary>
    private bool ValidateDetailInput()
    {
        string errorMsg = "";

        if (DDLRecordObject.SelectedValue.Equals(string.Empty))
        {
            errorMsg = "请选择系统-子系统-巡查项目";
        }

        else if (TBRecordDate.Text.Trim() == string.Empty)
        {
            errorMsg = "巡查日期不能为空";
        }
        //else if (TBRecordmanID.Text.Trim() == string.Empty)
        //{
        //    errorMsg = "巡查人不能为空";
        //}
        else if (taRecordResult.Value.Trim() == string.Empty)
        {
            errorMsg = "实际巡查结果不能为空";
        }

        else if (TextBox_Address.Value.Trim()==string.Empty || Hidden_AddressID.Value.ToString() == "")
        {
            errorMsg="地址信息不能为空";
        }

        if (TBRecordDate.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime recorddate = Convert.ToDateTime(TBRecordDate.Text.Trim());
                if (recorddate > DateTime.Now)
                {
                    errorMsg = "巡查日期不能大于当前时间";
                }

            }
            catch
            {
                errorMsg = "巡查日期只能为日期格式，请检查输入";
            }
        }

        if (errorMsg != "")
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "输入有误：" + errorMsg);
            LblErrorMessage.Text = "输入有误：" + errorMsg;
            return false;
        }
        return true;
    }

    /// <summary>
    /// 确定按钮
    /// </summary>
    protected void Button2_Click(object sender, EventArgs e)
    {
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", "执行日常巡查成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("DailyPatrolRecord.aspx"), UrlType.Href, "");
    }

    /// <summary>
    /// 
    /// </summary>
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView2.Rows[Convert.ToInt32(e.CommandArgument)];
        long recordid = Convert.ToInt64(gvRow.Attributes["ID"]);
        if (e.CommandName == "view")  //编辑
        {
            this.SelectedItems = null;  //清空记录
            GridView1.DataSource = null;
            GridView1.DataBind();  //清空GridView内容
            MaintainPlanRecord bll = new MaintainPlanRecord();
            MaintainPlanRecordInfo info = bll.GetMaintainPlanRecord(recordid);
            TBRecordDate.Text = info.RecordDate.ToString("yyyy-MM-dd");
            Label_Patroler.Text = info.RecordmanName;
            //TBRecordmanID.Text = 
            taRecordResult.Value = info.RecordResult;
            taRemark.Value = info.RecordRemark;
            //获取系统和子系统信息
            MaintainPlanConfig conbll = new MaintainPlanConfig();
            MaintainPlanConfigInfo coninfo = conbll.GetMaintainPlanConfig(info.ItemID);
            CascadingDropDown1.SelectedValue = coninfo.System;
            CascadingDropDown2.SelectedValue = coninfo.Subsystem.ToString();
            CascadingDropDown3.SelectedValue = coninfo.ItemID.ToString();
            //获取地址信息
            long addressid = bll.GetAddressIDByRecordID(recordid);
            Hidden_AddressID.Value = Convert.ToString(addressid);
            Address addbll = new Address();
            AddressInfo addinfo = addbll.GetAddress(addressid);
            TextBox_Address.Value = addinfo.AddressName;
            id = info.ItemID;
            //伪全选
            this.SelectedItems = (ArrayList)info.EquipmentList;
            Button1.Text = "更新记录";
            ViewState["EditRecordID"] = recordid;
            FillDataEquipment(1);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                MaintainPlanRecord bll = new MaintainPlanRecord();
                bll.DelMaintainPlanRecord(recordid);
                FillData();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            MaintainPlanRecordInfo item = (MaintainPlanRecordInfo)e.Row.DataItem;
            e.Row.Attributes["ID"] = item.RecordID.ToString();
        }

    }

    /// <summary>
    ///
    /// </summary>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "del")
        {
            try
            {
                string equipment = Convert.ToString(gvRow.Attributes["eNO"]);
                MaintainPlanConfig bll = new MaintainPlanConfig();
                bll.DelMaintainPlanEquipment(equipment, id);
                FillData();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败：" + ex.Message, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            EquipmentInfoFacade item = (EquipmentInfoFacade)e.Row.DataItem;

            e.Row.Attributes["PhotoUrl"] = item.PhotoUrl;

            e.Row.Attributes["ID"] = item.EquipmentID.ToString();
            e.Row.Attributes["eNO"] = item.EquipmentNO.ToString();

            if (e.Row.RowIndex > -1 && this.SelectedItems != null )
            {
                CheckBox cb = e.Row.FindControl("checkBox1") as CheckBox;
                if (this.SelectedItems.Contains(e.Row.Attributes["eNO"]))
                    cb.Checked = true;
                else
                    cb.Checked = false;
            }

        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void GridView1_OnDataBinding(object sender, EventArgs e)
    {
        //在每一次重新绑定之前，需要调用CollectSelected方法从当前页收集选中项的情况
        CollectSelected();
    }

    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }

    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        if (DDLRecordObject.SelectedItem.Value != "")
        {
            id = Convert.ToInt64(DDLRecordObject.SelectedValue);
        }
        //CollectSelected();
        FillDataEquipment(0);
    }

    /// <summary>
    /// 巡查项目改变事件
    /// </summary>
    protected void DDLRecordObjectOnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLRecordObject.SelectedItem.Value != "")
        {
            id = Convert.ToInt64(DDLRecordObject.SelectedValue);
        }
        FillDataEquipment(1);
    }

    /// <summary>
    /// 地址信息改变事件
    /// </summary>
    protected void ButtonHidden_Click(object sender, EventArgs e)
    {
        if (DDLRecordObject.SelectedItem.Value != "")
        {
            id = Convert.ToInt64(DDLRecordObject.SelectedValue);
        }
        FillDataEquipment(1);
    }

    /// <summary>
    /// 获取已选记录
    /// </summary>
    protected void CollectSelected()
    {
        ArrayList selectedItems = null;
        if (this.SelectedItems == null)
            selectedItems = new ArrayList();
        else
            selectedItems = this.SelectedItems;

        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            string id = this.GridView1.Rows[i].Attributes["eNO"].ToString();
            CheckBox cb = this.GridView1.Rows[i].FindControl("checkBox1") as CheckBox;
            if (allselectlabel == 0)
            {
                if (selectedItems.Contains(id) && !cb.Checked)
                    selectedItems.Remove(id);
                if (!selectedItems.Contains(id) && cb.Checked)
                    selectedItems.Add(id);
            }
            else if(allselectlabel == 1)
            {
                if (!selectedItems.Contains(id))
                { 
                    selectedItems.Add(id); 
                }
                cb.Checked = true;
            }
        }
        this.SelectedItems = selectedItems;
    }

    /// <summary>
    /// 获取或设置选中项的集合
    /// </summary>
    protected ArrayList SelectedItems
    {
        get
        {
            return (ViewState["mySelectedItems"] != null) ? (ArrayList)ViewState["mySelectedItems"] : null;
        }
        set
        {
            ViewState["mySelectedItems"] = value;
        }
    }

    /// <summary>
    /// 判断是否全选
    /// </summary>
    protected void SetSelectedAll()
    {
        allselectlabel = 1;
        ArrayList selectedItems = null;
        MaintainPlanConfig bll = new MaintainPlanConfig();
        IList selectallE = bll.GetAllEquipmentByItemIDandAddessCode(id, addresscode);
        if (selectallE == null)
            selectedItems = new ArrayList();
        else
            selectedItems = (ArrayList)selectallE;

        this.SelectedItems = selectedItems;
    }

    /// <summary>
    /// Button3_onclick
    /// </summary>
    protected void SelectAllE(object sender, EventArgs e)
    {
        if (DDLRecordObject.SelectedItem.Value != "")
        {
            id = Convert.ToInt64(DDLRecordObject.SelectedValue);
        }
        SetSelectedAll();
        FillDataEquipment(1);
    }

    /// <summary>
    /// 判断是否取消全选
    /// </summary>
    protected void SetSelectNone()
    {
        allselectlabel = 0;
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            CheckBox cb = this.GridView1.Rows[i].FindControl("checkBox1") as CheckBox;
            cb.Checked = false;
        }
        this.SelectedItems = null;
    }

    /// <summary>
    /// Button4_onclick
    /// </summary>
    protected void SelectNoneE(object sender, EventArgs e)
    {
        if (DDLRecordObject.SelectedItem.Value != "")
        {
            id = Convert.ToInt64(DDLRecordObject.SelectedValue);
        }
        FillDataEquipment(1);
    }

}
