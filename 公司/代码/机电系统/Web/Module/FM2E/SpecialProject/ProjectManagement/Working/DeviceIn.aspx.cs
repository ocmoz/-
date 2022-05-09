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

public partial class Module_FM2E_SpecialProject_ProjectManagement_Working_DeviceIn : System.Web.UI.Page
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

    private const string sessionName = "Module_FM2E_SpecialProject_ProjectManagement_DeviceIn";

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
        Repeater_ItemList.DataSource = project.DeviceList;
        Repeater_ItemList.DataBind();
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
        SpecialProjectDeviceInRecord record = new SpecialProjectDeviceInRecord();
        record.Count = decimal.Parse(TextBox_Count.Text.Trim());
        record.ItemID = itemID;
        record.ProjectID = project.ProjectID;
        record.RecordID = 0;
        record.Time = DateTime.Parse(TextBox_Time.Text.Trim());

 
        record.RecordID = specialProjectBll.SaveDeviceInRecord(record);
        device.ActualCount = specialProjectBll.UpdateDeviceItem(record.ItemID, record.Count, record.Time);
        device.LastInCount = record.Count;
        device.Time = record.Time;
        device.DeviceInRecordList.Add(record);

        Session[sessionName] = project;
        FillData();
    }


}
