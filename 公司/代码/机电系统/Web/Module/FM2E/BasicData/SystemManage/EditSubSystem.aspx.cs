using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WebUtility;
using WebUtility.Components;
using WebUtility.WebControls;

using FM2E.BLL.Basic;
using FM2E.Model.Basic;

public partial class Module_FM2E_BasicData_SystemManage_EditSubSystem : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    string id = (string)Common.sink("id", MethodType.Get, 2, 0, DataType.Str);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
            ButtonBind();    
        }
    }

    private void FillData()
    {
        if (cmd == "edit")
        {
            try
            {
                SubEquipmentSystemInfo item = null;
                if (Session["SubEquipmentSystemInfo"] != null)
                {
                    item = (SubEquipmentSystemInfo)Session["SubEquipmentSystemInfo"];
                }
                else
                {
                    EquipmentSystem bll = new EquipmentSystem();
                    item = bll.GetSubSystem(id);
                }
                Label1.Text = Convert.ToString(item.SubSystemID);
                TextBox2.Text = item.SubSystemName;
                Label3.Text = item.ParentSystemID;
                TextBox4.Text = item.Remark;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        else if (cmd == "add")
        {
            SystemCode.Visible = false;
            Label3.Text = id;
        }
    }

    private void ButtonBind()
    {
        HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[0];
        button.ButtonUrlType = UrlType.Href;

        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：系统信息添加";
            TabPanel1.HeaderText = "添加系统";
            button.ButtonUrl = string.Format("ViewSystem.aspx?cmd=view&id={0}", id);
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：系统信息修改";
            TabPanel1.HeaderText = "修改系统信息";
            button.ButtonUrl = string.Format("ViewSubSystem.aspx?cmd=view&id={0}", id);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        bool success = false;

        if (cmd == "add" || cmd == "edit")
        {
            SubEquipmentSystemInfo item = new SubEquipmentSystemInfo();     
            item.SubSystemName = TextBox2.Text.Trim();
            item.ParentSystemID = Label3.Text.Trim();
            item.Remark = TextBox4.Text.Trim();
            item.IsDeleted = false;

            if (cmd == "add")
            {
                try
                {
                    EquipmentSystem bll = new EquipmentSystem();
                    bll.InsertSubSystem(item);
                    success = true;
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加系统失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                }
                if (success)
                {
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加子系统成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ViewSystem.aspx?cmd=view&id=" + item.ParentSystemID), UrlType.Href, "");
                }
            }
            else if (cmd == "edit")
            {
                try
                {
                    item.SubSystemID = Convert.ToInt32(Label1.Text.Trim());
                    EquipmentSystem bll = new EquipmentSystem();
                    bll.UpdateSubSystem(item);
                    success = true;
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "编辑系统信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                }
                if (success)
                {
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改系统信息成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ViewSystem.aspx?cmd=view&id="+item.ParentSystemID), UrlType.Href, "");
                }
            }
        }
    }

}
