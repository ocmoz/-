using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using FM2E.Model.Exceptions;
using FM2E.WorkflowLayer;
using WebUtility;
using WebUtility.Components;
using FM2E.BLL.Utils;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_PriceManager_PriceMaintenance_EditPrice : System.Web.UI.Page
{
    string companyid = UserData.CurrentUserData.CompanyID;
    string productname = Microsoft.JScript.GlobalObject.unescape(HttpContext.Current.Request.QueryString["productname"]);
    string model = (string)Common.sink("model", MethodType.Get, 50, 0, DataType.Str);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitiaPage();
            FillData();
            PermissionControl();
        }
    }

    private void InitiaPage()
    {
        DropDownList_Unit.Items.Clear();
        DropDownList_Unit.Items.Add(new ListItem("请选择单位", ""));
        IList unitlist = Constants.GetUnits();
        foreach (string unit in unitlist)
            DropDownList_Unit.Items.Add(new ListItem(unit, unit));
    }

    private void PermissionControl()
    {
        if (SystemPermission.CheckPermission(PopedomType.New))
            newitem.Visible = true;
        else
            newitem.Visible = false;
        if (SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[GridView1.Columns.Count - 2].Visible = true;
        else GridView1.Columns[GridView1.Columns.Count - 2].Visible = false;
        if (SystemPermission.CheckPermission(PopedomType.Edit))
        {
            GridView1.Columns[GridView1.Columns.Count - 3].Visible = true;
            GridView1.Columns[GridView1.Columns.Count - 4].Visible = true;
            GridView1.Columns[GridView1.Columns.Count - 5].Visible = true;
        }
        else
        {
            GridView1.Columns[GridView1.Columns.Count - 3].Visible = false;
            GridView1.Columns[GridView1.Columns.Count - 4].Visible = false;
            GridView1.Columns[GridView1.Columns.Count - 5].Visible = false;
        }

    }

    private void FillData()
    {
        try
        {
            Session.Remove("GridViewList");
            Session.Remove("GridViewList2");
            PriceDetailSearchInfo pricedetail = new PriceDetailSearchInfo();
            pricedetail.CompanyID = companyid;
            pricedetail.ProductName = productname;
            pricedetail.Model = model;
            PriceManager detailbll = new PriceManager();
            IList<PriceDetailInfo> list = detailbll.SearchPriceDetail(pricedetail);
            GridView1.DataSource = list;
            GridView1.DataBind();
            Session["GridViewList"] = list;
            GridView2.DataSource = null;
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "申请审批页面初始化失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }

    }
    /// <summary>
    /// 列表行事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            SaveNewContent();
            int row = Convert.ToInt32(e.CommandArgument);
            //GridView1.Rows[row].Visible = false;
            IList list = (IList)Session["GridViewList"];
            list.RemoveAt(row);
            Session["GridViewList"] = list;
            GridView1.DataSource = list;
            GridView1.DataBind();
        }

    }


    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            int row = Convert.ToInt32(e.CommandArgument);
            IList list = (IList)Session["GridViewList2"];
            list.RemoveAt(row);
            Session["GridViewList2"] = list;
            GridView2.DataSource = list;
            GridView2.DataBind();
        }
    }
    /// <summary>
    /// 列表绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void AddItem_Click(object sender, EventArgs e)
    {
        errormessage.Text = "";
        SaveNewContent();
        string[] itemkeygroup = addstring.Value.Split('|');
        string[] itemkey;
        IList<PriceDetailInfo> list = (IList<PriceDetailInfo>)Session["GridViewList"];
        PriceDetailSearchInfo pricedetail = new PriceDetailSearchInfo();
        PriceManager detailbll = new PriceManager();
        errormessage.Text = "";
        foreach (string item in itemkeygroup)
        {
            if (item != string.Empty)
            {
                itemkey = item.Split(',');

                if (itemkey.Length == 3)
                {
                    try
                    {
                        pricedetail.CompanyID = itemkey[0];
                        pricedetail.ProductName = itemkey[1];
                        pricedetail.Model = itemkey[2];
                        bool same = false;
                        foreach (PriceDetailInfo addedmodle in list)
                        {
                            if (addedmodle.CompanyID == pricedetail.CompanyID && addedmodle.ProductName == pricedetail.ProductName && addedmodle.Model == pricedetail.Model)
                            {
                                errormessage.Text += "要添加的一些修改项已存在:" + addedmodle.ProductName + "<br/>";
                                same = true;
                            }
                        }
                        if (same)
                            continue;
                        PriceDetailInfo itemtemp = (PriceDetailInfo)detailbll.SearchPriceDetail(pricedetail)[0];
                        if (itemtemp != null)
                            list.Add(itemtemp);
                    }
                    catch (Exception ex)
                    {
                        EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加修改项失败", ex, Icon_Type.Error, true, "history.go(0)", UrlType.JavaScript, "");
                    }
                }
            }
        }

        GridView1.DataSource = list;
        GridView1.DataBind();
        Session["GridViewList"] = list;
    }
    /// <summary>
    /// 保存输入信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SaveNewContent()
    {
        IList<PriceDetailInfo> list = (IList<PriceDetailInfo>)Session["GridViewList"];
        try
        {
            for (int r = 0; r < GridView1.Rows.Count; r++)
            {
                if (((TextBox)GridView1.Rows[r].FindControl("NewUpperPrice")).Text != string.Empty)
                    list[r].NewUpperPrice = Convert.ToDecimal(((TextBox)GridView1.Rows[r].FindControl("NewUpperPrice")).Text);
                if (((TextBox)GridView1.Rows[r].FindControl("NewLowerPrice")).Text != string.Empty)
                    list[r].NewLowerPrice = Convert.ToDecimal(((TextBox)GridView1.Rows[r].FindControl("NewLowerPrice")).Text);
                list[r].Reason = ((TextBox)GridView1.Rows[r].FindControl("Reason")).Text;
                list[r].DeleteOrNot = ((CheckBox)GridView1.Rows[r].FindControl("deleteornot")).Checked;
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入数据格式不正确,请重新输入", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        Session["GridViewList"] = list;
    }
    /// <summary>
    /// 添加指导价事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddPriceDetail(object sender, EventArgs e)
    {
        errormessage2.Text = "";
        PriceManager bll = new PriceManager();
        PriceDetailSearchInfo searchitem = new PriceDetailSearchInfo();
        searchitem.CompanyID = companyid;
        searchitem.ProductName = TextBox_ProductName.Text;
        searchitem.Model = TextBox_Model.Text;

        IList<PriceDetailInfo> nowlist = bll.SearchPriceDetail(searchitem);

        if (nowlist.Count != 0)
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "你要添加的指导价格已存在或无意义", new WebException("你要添加的指导价格已存在或无意义"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");

        PriceDetailInfo model = new PriceDetailInfo();

        model.CompanyID = companyid;
        model.ProductName = TextBox_ProductName.Text;
        model.Model = TextBox_Model.Text;
        if (TextBox_UpperPrice.Text != string.Empty)
            model.UpperPrice = Convert.ToDecimal(TextBox_UpperPrice.Text);
        if (TextBox_LowerPrice.Text != string.Empty)
            model.LowerPrice = Convert.ToDecimal(TextBox_LowerPrice.Text);
        if (DropDownList_Unit.SelectedValue != string.Empty)
            model.Unit = DropDownList_Unit.SelectedValue;
        IList list;
        if (Session["GridViewList2"] != null)
            list = (IList)Session["GridViewList2"];
        else
            list = new List<PriceDetailInfo>();
        foreach (PriceDetailInfo addeditem in list)
        {
            if (addeditem.CompanyID == model.CompanyID && addeditem.ProductName == model.ProductName && addeditem.Model == model.Model)
            {
                errormessage2.Text = "要添加的新指导价已存在";
                return;
            }
        }
        list.Add(model);

        GridView2.DataSource = list;
        GridView2.DataBind();
        Session["GridViewList2"] = list;
    }
    /// <summary>
    /// 提交到审批事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void submittoapproval(object sender, EventArgs e)
    {
        PriceApplyInfo item = new PriceApplyInfo();
        PriceManager bll = new PriceManager();
        item.CompanyID = companyid;
        item.Applicant = UserData.CurrentUserData.UserName;
        item.Approvaler = "";
        item.ApplyDate = DateTime.Now;
        item.DetailList = new List<PriceApplyDetailInfo>();
        if(GridView1.Rows.Count == 0 && GridView2.Rows.Count == 0)
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交修改项不能为空", new WebException("提交修改项不能为空"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        for (int r = 0; r < GridView1.Rows.Count; r++)
        {
            PriceApplyDetailInfo item2 = new PriceApplyDetailInfo();
            item2.CompanyID = companyid;
            item2.ProductName = ((Label)GridView1.Rows[r].FindControl("ProductName")).Text;
            item2.Model = ((Label)GridView1.Rows[r].FindControl("Model")).Text;
            item2.Unit = ((Label)GridView1.Rows[r].FindControl("Unit")).Text;
            if (((Label)GridView1.Rows[r].FindControl("StartTime")).Text != string.Empty)
                item2.StartTime = Convert.ToDateTime(((Label)GridView1.Rows[r].FindControl("StartTime")).Text);
            if (((Label)GridView1.Rows[r].FindControl("UpperPrice")).Text != string.Empty)
                item2.OldUpperPrice = Convert.ToDecimal(((Label)GridView1.Rows[r].FindControl("UpperPrice")).Text);
            if (((Label)GridView1.Rows[r].FindControl("LowerPrice")).Text != string.Empty)
                item2.OldLowerPrice = Convert.ToDecimal(((Label)GridView1.Rows[r].FindControl("LowerPrice")).Text);
            if (((TextBox)GridView1.Rows[r].FindControl("NewUpperPrice")).Text != string.Empty)
                item2.NewUpperPrice = Convert.ToDecimal(((TextBox)GridView1.Rows[r].FindControl("NewUpperPrice")).Text);
            if (((TextBox)GridView1.Rows[r].FindControl("NewLowerPrice")).Text != string.Empty)
                item2.NewLowerPrice = Convert.ToDecimal(((TextBox)GridView1.Rows[r].FindControl("NewLowerPrice")).Text);

            item2.Reason = ((TextBox)GridView1.Rows[r].FindControl("Reason")).Text;
            item2.DeleteOld = (((CheckBox)GridView1.Rows[r].FindControl("deleteornot")).Checked == true) ? Convert.ToInt16(1) : Convert.ToInt16(0);
            item2.Result = 0;
            item2.FeeBack = "";
            item2.Status = "未审批";
            item2.instanceId = Guid.Empty;
            item.DetailList.Add(item2);
        }
        for (int r = 0; r < GridView2.Rows.Count; r++)
        {
            PriceApplyDetailInfo item2 = new PriceApplyDetailInfo();
            item2.CompanyID = companyid;
            item2.ProductName = ((Label)GridView2.Rows[r].FindControl("ProductName")).Text;
            item2.Model = ((Label)GridView2.Rows[r].FindControl("Model")).Text;
            item2.Unit = ((Label)GridView2.Rows[r].FindControl("Unit")).Text;
            item2.StartTime = DateTime.Now;
            item2.OldUpperPrice = decimal.Zero;
            item2.OldLowerPrice = decimal.Zero;
            if (((Label)GridView2.Rows[r].FindControl("UpperPrice")).Text != string.Empty)
                item2.NewUpperPrice = Convert.ToDecimal(((Label)GridView2.Rows[r].FindControl("UpperPrice")).Text);
            if (((Label)GridView2.Rows[r].FindControl("LowerPrice")).Text != string.Empty)
                item2.NewLowerPrice = Convert.ToDecimal(((Label)GridView2.Rows[r].FindControl("LowerPrice")).Text);
            item2.Reason = string.Empty;
            item2.DeleteOld = Convert.ToInt16(2);
            item2.Result = 0;
            item2.FeeBack = "";
            item2.Status = "未审批";
            item2.instanceId = Guid.Empty;
            item.DetailList.Add(item2);
        }
        long newapplyid = 0;
        try
        {
            item.Status = PriceApplyStatus.Waiting4ApprovalResult;
            newapplyid = bll.InsertPriceApply(item);
            string title = item.ApplicantName + "的价格管理待审批";
            string URL = "../DeviceManager/SpareEquipmentManager/PriceManager/PriceApproval/PriceApproval.aspx?cmd=approval&id=" + newapplyid;
            WorkflowApplication.CreateWorkflowAndSendingPendingOrder<PriceEventService>(newapplyid, title, PriceWorkflow.WorkflowName, 
                PriceWorkflow.AppSubmitedEvent, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, item.CompanyID);
                
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "提交修改项失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        Session.Remove("GridViewList");
        if (newapplyid != 0)
            Response.Redirect("PriceApply.aspx?cmd=view&id=" + newapplyid);

    }




}
