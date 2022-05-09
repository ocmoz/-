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
public partial class Module_FM2E_SpecialProject_ProjectManagement_Working_EditDevice : System.Web.UI.Page
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

    private const string sessionName = "Module_FM2E_SpecialProject_ProjectManagement_EditDevice";

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
        FillData();
        //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
        gridview_DeviceItemList.Columns[gridview_DeviceItemList.Columns.Count - 1].Visible = SystemPermission.CheckButtonPermission(PopedomType.Delete);
        gridview_DeviceItemList.Columns[gridview_DeviceItemList.Columns.Count - 2].Visible = SystemPermission.CheckButtonPermission(PopedomType.Edit);
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
        gridview_DeviceItemList.DataSource = project.DeviceList;
        gridview_DeviceItemList.DataBind();
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
            IList deviceList = project.DeviceList;
            SpecialProjectDeviceInfo device = new SpecialProjectDeviceInfo();
           
            device.ItemID = 0;
            device.ActualCount = 0;
            device.DeviceName = TextBox_Equipment.Text.Trim();
            device.LastInCount = 0;
            device.Model = TextBox_Model.Text.Trim();
            device.PlanCount = decimal.Parse(TextBox_PlanCount.Text.Trim());
            device.ProjectID = project.ProjectID;
            device.Size = TextBox_Size.Text.Trim();
            device.Status = TextBox_Status.Text.Trim();
            device.Time = DateTime.MinValue;
            device.Usage = TextBox_Usage.Text.Trim();
            //插入数据库
            device.ItemID = specialProjectBll.SaveDeviceItem(device);
            project.DeviceList.Add(device);
        }
        else//编辑
        {
            IList deviceList = project.DeviceList;
            SpecialProjectDeviceInfo device = null;
            foreach (SpecialProjectDeviceInfo item in deviceList)
            {
                if (item.ItemID == itemID)
                {
                    device = item;
                    break;
                }
            }
            if (device == null)
            {
                device = new SpecialProjectDeviceInfo();
                device.ItemID = 0;
                device.ProjectID = project.ProjectID;
                project.DeviceList.Add(device);
            }
          
            device.DeviceName = TextBox_Equipment.Text.Trim();
            
            device.Model = TextBox_Model.Text.Trim();
            device.PlanCount = decimal.Parse(TextBox_PlanCount.Text.Trim());

            device.Size = TextBox_Size.Text.Trim();
            device.Status = TextBox_Status.Text.Trim();
      
            device.Usage = TextBox_Usage.Text.Trim();


            device.ItemID = specialProjectBll.SaveDeviceItem(device);
        }
        Session[sessionName] = project;
        FillData();
    }



    /// <summary>
    /// 删除行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridview_ItemList_RowDeleted(object sender, GridViewDeleteEventArgs e)
    {
        long itemID = (long)gridview_DeviceItemList.DataKeys[e.RowIndex].Values["ItemID"];

        IList list = null;
        SpecialProjectInfo project = Session[sessionName] as SpecialProjectInfo;
        if (project == null)
        {
            project = specialProjectBll.GetSpecialProject(id);
        }
        list = project.DeviceList;

        if (list == null || list.Count == 0)
        {
            EventMessage.MessageBox(Msg_Type.Error, "删除失败", "设备项:" + itemID + " 已经不存在，请刷新",
               Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "history.go(-1)");
            return;
        }

        specialProjectBll.DeleteDeviceItem(itemID);

        foreach (SpecialProjectDeviceInfo item in list)
        {
            if (item.ItemID == itemID)
            {
                list.Remove(item);
                break;
            }
        }
        Session[sessionName] = project;

        FillData();
    }
}