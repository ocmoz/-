using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM2E.BLL.Basic;
using System.Collections;
using FM2E.Model.Basic;
using WebUtility;
using WebUtility.Components;
using FM2E.Model.Exceptions;
using FM2E.Model.System;

public partial class Module_FM2E_BasicData_AddressManage_Address : System.Web.UI.Page
{
    private const string ISLEAF_VIEWSTATE = "isLeaf";
    //private string returnType = (string)Common.sink("returnType", MethodType.Get, 20, 0, DataType.Str);
    public string operatorType = (string)Common.sink("operator", MethodType.Get, 20, 0, DataType.Str);
    private readonly Address addressBll = new Address();
    protected AddressType selectType = (AddressType)(int)Common.sink("addresstype", MethodType.Get, 2, 0, DataType.Int);
    /// <summary>
    /// 页面模式
    /// </summary>
    public string Mode
    {
        get
        {
            if (ViewState["Mode"] == null)
                return "viewmode";
            
            return (string)ViewState["Mode"];
        }
        set
        {
            ViewState["Mode"] = value;
        }

    }
    /// <summary>
    /// 保存选中的树结点对应的地址信息
    /// </summary>
    public AddressInfo CurrentAddress
    {
        get
        {
            if (ViewState["addressInfo"] == null)
                return null;

            return (AddressInfo)ViewState["addressInfo"];
        }
        set
        {
            ViewState["addressInfo"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
            BuildTree();
            FillData();
        }
        ButtonBind();
    }
    /// <summary>
    /// 初始化页面数据
    /// </summary>
    private void InitialPage()
    {
        try
        {
            LoginUserInfo loginUser = UserData.CurrentUserData;

            //加载用户类型列表以及用户状态列表
            ListItem[] addressTypeItems = EnumHelper.GetListItems(typeof(AddressType));
            ddlAddressType.Items.Clear();
            ddlAddressType.Items.AddRange(addressTypeItems);

            //维修单位
            ddlMaintainTeam.Items.Clear();
            ddlMaintainTeam.Items.Add(new ListItem("不限", "0"));
            ddlMaintainTeam.Items.AddRange(ListItemHelper.GetAllMaintainTeams(loginUser.CompanyID));
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "初始化页面失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 建立地址树
    /// </summary>
    private void BuildTree()
    {
        try
        {
            TreeNode root = null;

            IList addressList = addressBll.GetChildAddress(1);
            root = new TreeNode("地址列表", "1");

            addressTree.Nodes.Add(root);

            foreach (AddressInfo item in addressList)
            {
                TreeNode node = new TreeNode(item.AddressName, item.ID.ToString());
                node.Expanded = false;
                if (item.ChildCount > 0)
                {
                    //非叶子结点
                    node.Expanded = false;
                    node.PopulateOnDemand = true;
                    node.SelectAction = TreeNodeSelectAction.SelectExpand;

                }
                root.ChildNodes.Add(node);
            }

            root.Select();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "加载地址树失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 填充页面数据
    /// </summary>
    private void FillData()
    {
        try
        {
            long id = Convert.ToInt64(addressTree.SelectedValue);
            AddressInfo address = addressBll.GetAddress(id);
            if (address == null)
            {
                //根结点
                address = new AddressInfo();
                address.AddressCode = "00";
                address.AddressFullName = "地址列表";
                address.AddressType = AddressType.Unknown;
                address.ChildCount = 0;
                address.Description = "地址列表";
                address.ID = 1;
                address.Modifier = "admin";
                address.NextAddressCode = 1;
                address.ParentAddress = 0;
                CurrentAddress = address;
                return;
            }

            //填充页面数据
            lbAddressName.Text = tbAddressName.Text = address.AddressName;
            lbAddressType.Text = EnumHelper.GetDescription(address.AddressType);
            ddlAddressType.SelectedValue = ((int)address.AddressType).ToString();
          
                ddlMaintainTeam.SelectedValue = address.MainTeamID.ToString();
            

            Hidden_AddressType.Value = ((int)address.AddressType).ToString();
            Label_FullName.Text = address.AddressFullName;
            CurrentAddress = address;
            if (address.ParentAddress == 0)
                lbParentAddress.Text = "无上级地址";
            else
            {
                AddressInfo parentAddress = addressBll.GetAddress(address.ParentAddress);
                if (parentAddress != null)
                {
                    lbParentAddress.Text = parentAddress.AddressName;
                }
                else lbParentAddress.Text = "找不到上级地址信息";
            }
            lbChildCount.Text = address.ChildCount.ToString();
            ViewState[ISLEAF_VIEWSTATE] = address.ChildCount > 0 ? false : true;
            lbModifier.Text = address.Modifier;
            lbDescription.Text = tbDescription.Text = address.Description;

            //if (string.IsNullOrEmpty(returnType))
            //{
            SelectedAddress.Value = string.Format("{0}|{1}|{2}", address.ID, address.AddressCode, address.AddressFullName);
            //}
            //else if (returnType.ToLower() == "id")
            //{
            //    SelectedAddress.Value = address.ID.ToString();
            //}
            //else if (returnType.ToLower() == "code")
            //{
            //    SelectedAddress.Value = address.AddressCode;
            //}

            //获取子地址列表
            IList subAddressList = addressBll.GetChildAddress(id);
            gvSubAddress.DataSource = subAddressList;
            gvSubAddress.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "填充页面数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 菜单项绑定
    /// </summary>
    private void ButtonBind()
    {
        try
        {
            HeadMenuWebControls1.ButtonList[0].ButtonUrl = string.Format("document.all.{0}.click();", btAddMode.ClientID);
            HeadMenuWebControls1.ButtonList[0].ButtonUrlType = UrlType.JavaScript;
            HeadMenuWebControls1.ButtonList[1].ButtonUrl = string.Format("document.all.{0}.click();", btEditMode.ClientID);
            HeadMenuWebControls1.ButtonList[1].ButtonUrlType = UrlType.JavaScript;
            HeadMenuWebControls1.ButtonList[2].ButtonUrl = string.Format("document.all.{0}.click();", btDelete.ClientID);
            HeadMenuWebControls1.ButtonList[2].ButtonUrlType = UrlType.JavaScript;

            //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
            //---HeadMenuWebControls1.ShowHeader = !(operatorType == "select");
            if (operatorType == "select")
            {
                HeadMenuWebControls1.ShowHeader = false;
                HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
                HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
                HeadMenuWebControls1.ButtonList[2].ButtonVisible = false;
            }
            else
            {
                HeadMenuWebControls1.ShowHeader = true;
                HeadMenuWebControls1.ButtonList[0].ButtonVisible = true;
                HeadMenuWebControls1.ButtonList[1].ButtonVisible = true;
                HeadMenuWebControls1.ButtonList[2].ButtonVisible = true;
            }

            //********** Modification Finished 2011-09-09 **********************************************************************************************

            //if (Mode == "viewmode")
            //    ViewMode();
            //else if (Mode == "editmode" || Mode == "addmode")
            //{
            //    HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
            //    HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
            //    HeadMenuWebControls1.ButtonList[2].ButtonVisible = false;
            //}
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "加载菜单项失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

    }
    /// <summary>
    /// 子地址列表行命令
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSubAddress_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    /// <summary>
    /// 子地址列表数据绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSubAddress_RowDataBound(object sender, GridViewRowEventArgs e)
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
    /// <summary>
    /// 选择树结点
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void addressTree_SelectedNodeChanged(object sender, EventArgs e)
    {
        FillData();
        //ViewMode();
    }
    /// <summary>
    /// 展开树结点
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void addressTree_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        try
        {
            long id = Convert.ToInt64(e.Node.Value);
            IList addressList = addressBll.GetChildAddress(id);
            foreach (AddressInfo item in addressList)
            {
                TreeNode node = new TreeNode(item.AddressName, item.ID.ToString());
                node.Expanded = false;
                if (item.ChildCount > 0)
                {
                    //非叶子结点
                    node.PopulateOnDemand = true;
                    node.SelectAction = TreeNodeSelectAction.SelectExpand;
                }
                e.Node.ChildNodes.Add(node);
            }
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取下一级地址结点失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 添加地址
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btAddMode_Click(object sender, EventArgs e)
    {
        AddMode();
        if (addressTree.SelectedValue == "1")
        {
            lbParentAddress.Text = "无";
        }
        else
        {
            lbParentAddress.Text = addressTree.SelectedNode.Text;
        }
        lbChildCount.Text = "0";
        lbModifier.Text = UserData.CurrentUserData.PersonName;

    }
    /// <summary>
    /// 修改地址
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btEditMode_Click(object sender, EventArgs e)
    {
        EditMode();
    }
    /// <summary>
    /// 查看模式
    /// </summary>
    private void ViewMode()
    {
        lbAddressName.Visible = true;
        tbAddressName.Visible = false;
        lbAddressType.Visible = true;
        ddlAddressType.Visible = false;
        lbParentAddress.Visible = true;
        lbChildCount.Visible = true;
        lbModifier.Visible = true;
        lbDescription.Visible = true;
        tbDescription.Visible = false;
        lbTitle.Text = "查看地址具体信息";
        Mode = "viewmode";

        HeadMenuWebControls1.ButtonList[0].ButtonVisible = true;

        //判断修改按钮是否显示
        if (addressTree.SelectedValue != "1" && SystemPermission.CheckPermission(PopedomType.Edit))
        {
            if (CurrentAddress.Modifier == Common.Get_UserName)
                HeadMenuWebControls1.ButtonList[1].ButtonVisible = true;
            else HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
        }
        else HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;

        if (addressTree.SelectedValue == "1")
        {
            HeadMenuWebControls1.ButtonList[2].ButtonVisible = false;
        }
        else
        {
            if ((ViewState[ISLEAF_VIEWSTATE] == null || ((bool)ViewState[ISLEAF_VIEWSTATE]) == true) && SystemPermission.CheckPermission(PopedomType.Edit))
            {
                if (CurrentAddress.Modifier == Common.Get_UserName)
                    HeadMenuWebControls1.ButtonList[2].ButtonVisible = true;
                else HeadMenuWebControls1.ButtonList[2].ButtonVisible = false;
            }
            else HeadMenuWebControls1.ButtonList[2].ButtonVisible = false;
        }
    }
    /// <summary>
    /// 添加模式
    /// </summary>
    private void AddMode()
    {
        lbAddressName.Visible = false;
        tbAddressName.Visible = true;
        lbAddressType.Visible = false;
        ddlAddressType.Visible = true;
        lbParentAddress.Visible = true;
        lbChildCount.Visible = true;
        lbChildCount.Text = "0";
        lbModifier.Visible = true;
        lbDescription.Visible = false;
        tbDescription.Visible = true;
        lbTitle.Text = "添加地址具体信息";
        Mode = "addmode";
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
        HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
        HeadMenuWebControls1.ButtonList[2].ButtonVisible = false;
        Clear();
    }
    /// <summary>
    /// 修改模式
    /// </summary>
    private void EditMode()
    {
        lbAddressName.Visible = false;
        tbAddressName.Visible = true;
        lbAddressType.Visible = false;
        ddlAddressType.Visible = true;
        lbParentAddress.Visible = true;
        lbChildCount.Visible = true;
        lbModifier.Visible = true;
        lbDescription.Visible = false;
        tbDescription.Visible = true;
        lbTitle.Text = "修改地址具体信息";
        Mode = "editmode";
        HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
        HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
        HeadMenuWebControls1.ButtonList[2].ButtonVisible = false;
    }
    /// <summary>
    /// 清空输入
    /// </summary>
    private void Clear()
    {
        lbAddressName.Text = "";
        tbAddressName.Text = "";
        lbAddressType.Text = "";
        ddlAddressType.SelectedValue = "0";
        ddlMaintainTeam.SelectedValue = "0";
        lbParentAddress.Text = "";
        lbChildCount.Text = "";
        lbModifier.Text = "";
        lbDescription.Text = "";
        tbDescription.Text = "";
    }
    /// <summary>
    /// 保存地址信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSave_Click(object sender, EventArgs e)
    {
        AddressInfo item = new AddressInfo();
        string addressName = tbAddressName.Text.Replace(" ", "");
        item.AddressType = (AddressType)Convert.ToInt32(ddlAddressType.SelectedValue);
        item.ChildCount = Convert.ToInt32(lbChildCount.Text.Trim());
        item.Modifier = Common.Get_UserName;
        item.Description = tbDescription.Text.Trim();

        if (ddlMaintainTeam.SelectedValue != "0")
        {
            item.MainTeamID = Convert.ToInt64(ddlMaintainTeam.SelectedValue);
            item.MainTeamName = ddlMaintainTeam.SelectedItem.Text;
        }

        if (Mode == "addmode")
        {
            if (!SystemPermission.CheckPermission(PopedomType.New))
                return;
            try
            {
                string parentCode = CurrentAddress.AddressCode;
                string addressCode = addressBll.GetNextAddressCode(parentCode);
                long parentID = CurrentAddress.ID;
                //地址名称
                if (CurrentAddress.AddressCode != "00")
                    item.AddressFullName = CurrentAddress.AddressFullName.Trim() + " " + addressName;
                else item.AddressFullName = addressName;
                if (item.AddressFullName.Length > 200)
                    throw new WebException("地址全名过长");

                item.ParentAddress = parentID;
                item.AddressCode = addressCode;
                item.NextAddressCode = 1;
                item.ID = addressBll.AddAddress(item);
                AddTreeNode(item);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加地址失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            //EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加地址成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Address.aspx"), UrlType.Href, "");
            //添加成功
        }
        else if (Mode == "editmode")
        {
            try
            {
                if (!SystemPermission.CheckPermission(PopedomType.Edit))
                    return;

                string addressCode = CurrentAddress.AddressCode;
                int nextAddressCode = CurrentAddress.NextAddressCode;
                item.ID = CurrentAddress.ID;
                item.ParentAddress = CurrentAddress.ParentAddress;

                // 生成地址全称
                int index = CurrentAddress.AddressFullName.LastIndexOf(" ");
                if (index > 0)
                {
                    item.AddressFullName = CurrentAddress.AddressFullName.Substring(0, index + 1) + addressName;
                }
                else
                {
                    item.AddressFullName = addressName;
                }
                if (item.AddressFullName.Length > 200)
                    throw new WebException("地址全名过长");
                item.AddressCode = addressCode;
                item.NextAddressCode = nextAddressCode;
                addressBll.UpdateAddress(item);
                
                addressTree.SelectedNode.Text = item.AddressName;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改地址失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            //EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改地址成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Address.aspx"), UrlType.Href, "");
        }
        FillData();
        ViewMode();
    }
    /// <summary>
    /// 取消编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btCancel_Click(object sender, EventArgs e)
    {
        ViewMode();
    }
    /// <summary>
    /// 删除地址
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btDelete_Click(object sender, EventArgs e)
    {
        if (!SystemPermission.CheckPermission(PopedomType.Delete))
            return;
        TreeNode tmp = addressTree.SelectedNode;
        try
        {
            long id = Convert.ToInt64(tmp.Value);
            addressBll.DelAddress(id);
            tmp.Parent.Select();
            addressTree.SelectedNode.ChildNodes.Remove(tmp);
            FillData();
            ViewMode();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除地址失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        //ButtonBind();
    }
    /// <summary>
    /// 添加地址结点到地址树中
    /// </summary>
    private void AddTreeNode(AddressInfo address)
    {
        TreeNode parentNode = addressTree.SelectedNode;
        TreeNode subNode = new TreeNode(address.AddressName,address.ID.ToString());
        subNode.SelectAction = TreeNodeSelectAction.Select;
        parentNode.Expand();
        parentNode.SelectAction = TreeNodeSelectAction.SelectExpand;
        parentNode.ChildNodes.Add(subNode);
    }
}
