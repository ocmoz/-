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
using System.Collections.Generic;

using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using FM2E.Model.System;
using FM2E.BLL.System;

public partial class Module_FM2E_SystemManager_UserManager_SelectUser : System.Web.UI.Page
{

    int number =(int) Common.sink("number", MethodType.Get, 0, 0, DataType.Int);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (number == 0)
            number = 1;//默认是1
        if (!IsPostBack)
        {
            InitialPage();
            FillData();
        }

        
    }
    private void InitialPage()
    {
        ////try
        ////{
            //Position position = new Position();
            //IList<PositionInfo> positionList = position.GetAllPosition();
            //DropDownList_Position.Items.Clear();
            //DropDownList_Position.Items.Add(new ListItem("请选择职位", Guid.Empty.ToString("N")));
            //foreach (PositionInfo item in positionList)
            //{
            //    DropDownList_Position.Items.Add(new ListItem(item.PositionName, item.PositionID.ToString()));
            //}
  
            DropDownList_Sex.Items.Clear();  
            DropDownList_Sex.Items.AddRange(EnumHelper.GetListItemsEx(typeof(Sex), (int)Sex.Unknown));
        //}
        //catch (Exception ex)
        //{
        //    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        //}
    }
    private void FillData()
    {
        User userBll = new User();
        
        int listCount = 0;
        IList list = userBll.GetList(GetSearchTerm(), AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out listCount);
        AspNetPager1.RecordCount = listCount;

        GridView_UserList.DataSource = list;
        GridView_UserList.DataBind();
        TabContainer1.ActiveTabIndex = 1;

        AspNetPager1.RecordCount = listCount;
    }

    private UserSearchInfo GetSearchTerm()
    {
        //生成查询条件
        UserSearchInfo item = new UserSearchInfo();

        if (TextBox_UserName.Text.Trim() != string.Empty)
        {
            item.UserName = Common.inSQL(TextBox_UserName.Text.Trim());
        }
        if (TextBox_Name.Text.Trim() != string.Empty)
        {
            item.PersonName = Common.inSQL(TextBox_Name.Text.Trim());
        }

        item.CompanyID = DropDownList_Company.SelectedValue;

        item.Sex = (Sex)Convert.ToInt32(DropDownList_Sex.SelectedValue);

        if (!string.IsNullOrEmpty(DropDownList_Department.SelectedValue.Trim())) 
        {
            item.DepartmentID = Convert.ToInt64(DropDownList_Department.SelectedValue);
        }

        return item;
    }

    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        FillData();
        TabContainer1.ActiveTabIndex = 1;
    }



    protected void GridView_UserList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            UserInfo dv = (UserInfo)e.Row.DataItem;
            string username = dv.UserName;
            string personname = dv.PersonName;

            CheckBox cb = (CheckBox)e.Row.FindControl("checkBox1");
            if (cb != null)
                cb.Attributes.Add("onclick", "onClientClick(this,'" + username + "','" + personname + "')");
            //检查已经选择的用户名是否已经选中
            if (SelectedValue.Value.Length > 0)
            {
                if (SelectedValue.Value.Split(',')[0].Equals(username))
                    cb.Checked = true;
                else
                    cb.Checked = false;
            }
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
        TabContainer1.ActiveTabIndex = 1;
    }
}

