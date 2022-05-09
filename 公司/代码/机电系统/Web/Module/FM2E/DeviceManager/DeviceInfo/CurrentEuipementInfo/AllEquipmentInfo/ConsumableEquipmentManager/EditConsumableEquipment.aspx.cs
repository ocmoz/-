using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using WebUtility.Components;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;
using FM2E.Model.Equipment;
using FM2E.BLL.Equipment;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;

public partial class Module_FM2E_DeviceManager_DeviceInfo_ConsumableEquipmentManager_EditConsumableEquipment : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 20, 0, DataType.Long);
    private readonly ConsumableEquipment bll = new ConsumableEquipment();
    //加载页面
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
            ButtonBind();
        }
    }
    // 页面初始化
    private void InitialPage()
    {
        try
        {
            //系统
            EquipmentSystem systemBll = new EquipmentSystem();
            IList sysList = systemBll.GetAllSystem();

            foreach (EquipmentSystemInfo sys in sysList)
            {
                DDL_System.Items.Add(new ListItem(sys.SystemName, sys.SystemID));
            }
            //单位
            DDL_Unit.Items.Clear();
            IList list = Constants.GetUnits();
            foreach (string unit in list)
                DDL_Unit.Items.Add(new ListItem(unit, unit));
            DDL_Unit.Items.Insert(0, new ListItem("请选择单位", ""));

            EquipmentDetailList = null;  //清空
        }
        catch (Exception ex)
        {
            EventMessage.EventWriteLog(Msg_Type.Error, "初始化页面失败：" + ex.Message);
        }
    }
    //填充数据
    private void FillData()
    {
        if (cmd == "edit")
        {
            try
            {
                ConsumableEquipmentInfo item = bll.GetConsumableEquipment(id);
                //填充页面
                //tbConsumableEquipmentNO.Text = item.ConsumableEquipmentNO;
                tbName.Text = item.Name;
                DDL_System.SelectedValue = item.SystemID;
                tbSerialNum.Text = item.SerialNum;
                tbModel.Text = item.Model;
                tbSpecification.Text = item.Specification;
                tbAssertNumber.Text = item.AssertNumber;
                DDL_Unit.SelectedValue = item.Unit;
                tbCount.Text = (item.Count).ToString();
                tbPrice.Text = (item.Price).ToString();
                //tbSupplierID.Text = (item.SupplierID).ToString();
                //tbProducerID.Text = (item.ProducerID).ToString();
                //tbSupplierName.Text = item.SupplierName;
                //tbProducerName.Text = item.ProducerName;
                //if (DateTime.Compare(item.FileDate, DateTime.MinValue) != 0)
                //{
                //    tbFileDate.Text = item.FileDate.ToShortDateString();
                //}
                EquipmentDetailList = item.ConsumableEquipmentDetailList;
                FillEquipmentDetail();
                tbMaintenanceTimes.Text = (item.MaintenanceTimes).ToString();
                tbRemark.Text = item.Remark;
                //if (DateTime.Compare(item.UpdateTime, DateTime.MinValue) != 0)
                //{
                //    tbUpdateTime.Text = item.UpdateTime.ToShortDateString();
                //}

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "加载信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }
    //绑定按钮
    private void ButtonBind()
    {
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：信息添加";
        }
        if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：信息修改";
        }
    }
    //确定按钮事件
    protected void btSave_Click(object sender, EventArgs e)
    {
        ConsumableEquipmentInfo item = new ConsumableEquipmentInfo();
        //获取填入数据
        item.ConsumableEquipmentID = id;
        //生成设备配套设施的编号，与设备编号对应。
        DateTime date = Convert.ToDateTime("2011-1-1");
        item.ConsumableEquipmentNO = FM2E.BLL.BarCode.BarCode.GenerateBarCode("YH", date, false);
        item.Name = tbName.Text.Trim();
        item.SystemID = DDL_System.SelectedValue;
        item.SerialNum = tbSerialNum.Text.Trim();
        item.Model = tbModel.Text.Trim();
        item.Specification = tbSpecification.Text.Trim();
        item.AssertNumber = tbAssertNumber.Text.Trim();
        item.Unit = DDL_Unit.SelectedValue;
        item.Count = Convert.ToInt32(tbCount.Text.Trim());
        item.Price = Convert.ToDecimal(tbPrice.Text.Trim());
        //item.SupplierID = Convert.ToInt64(tbSupplierID.Text.Trim());
        //item.ProducerID = Convert.ToInt64(tbProducerID.Text.Trim());
        //item.SupplierName = tbSupplierName.Text.Trim();
        //item.ProducerName = tbProducerName.Text.Trim();
        item.FileDate = DateTime.Now;
        item.MaintenanceTimes = Convert.ToInt32(tbMaintenanceTimes.Text.Trim());
        item.Remark = tbRemark.Text.Trim();
        item.UpdateTime = DateTime.Now;
        item.ConsumableEquipmentDetailList = EquipmentDetailList;
        //增加
        if (cmd == "add")
        {
            bool bSuccess = false;
            try
            {
                bll.InsertConsumableEquipment(item);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ConsumableEquipment.aspx"), UrlType.Href, "");
            }
        }
        //修改
        else if (cmd == "edit")
        {
            bool bSuccess = false;
            try
            {
                bll.UpdateConsumableEquipment(item);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ConsumableEquipment.aspx"), UrlType.Href, "");
            }
        }
    }


    /// <summary>
    /// GridView  行命令
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMaintainedEquipmentDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridViewEquipmentDetail.Rows[Convert.ToInt32(e.CommandArgument)];
        int rowNum = Convert.ToInt32(e.CommandArgument);
        
        if (e.CommandName == "del")
        {
            //string count = gvRow.Attributes["Count"];
            //tbCount.Text = (totalCount - Convert.ToInt32(count.Trim())).ToString();
            //删除
            IList list = EquipmentDetailList;
            if (list == null || list.Count == 0)
                return;
            list.RemoveAt(rowNum);
            FillEquipmentDetail();
        }
    }
    /// <summary>
    /// 设备详细信息列表
    /// </summary>
    private IList EquipmentDetailList
    {
        get
        {
            if (Session["EquipmentDetailList"] == null)
                return new ArrayList();
            return (ArrayList)Session["EquipmentDetailList"];
        }
        set
        {
            Session["EquipmentDetailList"] = value;
        }
    }
    /// <summary>
    /// 填充列表
    /// </summary>
    private void FillEquipmentDetail()
    {
        GridViewEquipmentDetail.DataSource = EquipmentDetailList;
        GridViewEquipmentDetail.DataBind();
    }
    
         /// <summary>
    /// GridView数据绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private int totalCount = 0;//每次postback都会自动初始化
    protected void gvMaintainedEquipmentDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            ConsumableEquipmentDetailInfo item = e.Row.DataItem as ConsumableEquipmentDetailInfo;
            totalCount += item.Count;
            e.Row.Attributes["Count"] = item.Count.ToString();
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.BackColor = System.Drawing.Color.YellowGreen;
            e.Row.Font.Bold = true;
            e.Row.Cells[0].Text = "合计";
            e.Row.Cells[0].ColumnSpan = 3;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            for (int i = 1; i <= 2; i++)
            {
                e.Row.Cells[i].Visible = false;
            }
            Label lbTotal = e.Row.FindControl("lbTotalCount") as Label;
            if (lbTotal != null)
            {
                TotalEquipmentCount = totalCount;
                lbTotal.Text = TotalEquipmentCount.ToString();
                tbCount.Text = TotalEquipmentCount.ToString();
            }
        }
    }
    /// <summary>
    /// 设备详细个数
    /// </summary>
    private int TotalEquipmentCount
    {
        get
        {
            if (ViewState["TotalEquipmentCount"] == null)
                return 0;
            return (int)ViewState["TotalEquipmentCount"];
        }
        set
        {
            ViewState["TotalEquipmentCount"] = value;
        }
    }

    /// <summary>
    /// 添加设备详细信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_AddEquipmentDetail_Click(object sender, EventArgs e)
    {
        ConsumableEquipmentDetailInfo ep = new ConsumableEquipmentDetailInfo();
        ep.AddressName = TextBox_Address.Value;
        ep.AddressID = Convert.ToInt64(Hidden_AddressID.Value);
        ep.DetailLocation = TextBox_DetailLocation.Text.Trim();
        ep.Count = Convert.ToInt32(tbEquipmentDetailCount.Text.Trim());
        ep.Remark = tbEquipmentDetailMark.Text.Trim();
        IList equipmentparts = EquipmentDetailList;
        equipmentparts.Add(ep);
        EquipmentDetailList = equipmentparts;
        FillEquipmentDetail();
        tbEquipmentDetailCount.Text = "0";
        tbEquipmentDetailMark.Text = TextBox_DetailLocation.Text = TextBox_Address.Value = "";
    }
}
