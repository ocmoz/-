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

public partial class Module_FM2E_BasicData_PositionManage_EditPosition : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
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
                PositionInfo item;
                if (Session["PositionInfo"] == null)
                {
                    item = (PositionInfo)Session["PositionInfo"];
                }
                else
                {
                    Position bll = new Position();
                    item = bll.GetPosition(id);
                }

                TextBox2.Text = item.PositionName;
                TextBox3.Text = item.Remark;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败","获取数据失败", ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }

    private void ButtonBind()
    {
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：职位信息添加";

            TabPanel1.HeaderText = "添加职位";
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：职位信息修改";

            TabPanel1.HeaderText = "修改职位信息";
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        bool bSuccess = false;

        Position bll = new Position();
       
        if (cmd == "add" || cmd == "edit")
        {
            PositionInfo item = new PositionInfo();
            item.PositionID = id;
            item.PositionName = TextBox2.Text.Trim();
            item.Remark = TextBox3.Text.Trim();
            item.IsDeleted = false;

            if (bll.IfExists(Common.inSQL(item.PositionName)))
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "职位"+item.PositionName+"已经存在", Icon_Type.Error, false,"history.go(-1);", UrlType.JavaScript, "");
                return;

            }

            if (cmd == "add")
            {
                   
                   bll.InsertPosition(item);
                   bSuccess = true;
               if(bSuccess)
               {
                   EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加职位成功！", Icon_Type.OK, true , Common.GetHomeBaseUrl("Position.aspx"), UrlType.Href, "");
               }
           }
           else if (cmd == "edit")
           {

                 
                   bll.UpdatePosition(item);
                   bSuccess = true;
 
               if(bSuccess)
               {
                   EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改职位信息成功！", Icon_Type.OK, true , Common.GetHomeBaseUrl("Position.aspx"), UrlType.Href, "");
               }
           }
        }
    }

}
