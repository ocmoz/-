using System;

using System.Collections.Generic;

using System.Web.UI.WebControls;

using System.IO;
using FM2E.BLL.Insurance;
using FM2E.Model.Insurance;
using WebUtility;
using WebUtility.Components;
using WebUtility.WebControls;

using FM2E.BLL.System;
using FM2E.Model.Exceptions;


public partial class Module_FM2E_InsureManager_InsureInfoManager_InsuranceManager: System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private int id  =0;

    [Serializable]
    private class PageInfo
    {
        private string pageName;
        public string PageName
        {
            get { return pageName; }
            set { pageName = value; }
        }

        public PageInfo(string pageName)
        {
            this.pageName = pageName;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        id = (int)Common.sink("id", MethodType.Get, 50, 0, DataType.Int);
        
        SystemPermission.CheckCommandPermission(cmd);

       // TextBox4.Attributes.Add("ReadOnly", "ReadOnly");

        if (!IsPostBack)
        {
            //校验是否有权限执行此cmd
            SystemPermission.CheckCommandPermission(cmd);

            //BuildTree();
            FillData();
            ButtonBind();
            PermissionControl();
        }
    }
    private void PermissionControl()
    {
        /*if (SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[7].Visible = true;
        else GridView1.Columns[7].Visible = false;*/

        
    }

   

    protected void PopulateNode(Object sender, TreeNodeEventArgs e)
    {
        string path = e.Node.Value;
        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath(path));
        DirectoryInfo[] dirs = dirInfo.GetDirectories();
        foreach (DirectoryInfo dir in dirs)
        {
            if (dir.Name == ".svn") continue;
            TreeNode node = new TreeNode(dir.Name, path + "/" + dir.Name);
            node.PopulateOnDemand = true;
            node.Expanded = false;
            node.SelectAction = TreeNodeSelectAction.Expand;
            e.Node.ChildNodes.Add(node);
        }
        FileInfo[] files = dirInfo.GetFiles();
        foreach (FileInfo file in files)
        {
           if (file.Extension != ".aspx" && file.Extension != ".asp" && file.Extension != ".html" && file.Extension != ".htm" && file.Extension != ".chm")
                continue;
            TreeNode node = new TreeNode(file.Name, path + "/" + file.Name);
            node.Expanded = false;
            node.SelectAction = TreeNodeSelectAction.Select;
            e.Node.ChildNodes.Add(node);
        }
    }

  

    private void FillData()
    {
        Insurance insuranceBll = new Insurance();
        InsuranceInfo insuranceInfo = insuranceBll.GetInsuranceInfo(id);
        ViewState["InsuranceInfo"] = insuranceInfo;

        if (cmd == "add")
        {
            dd_insuranceType.Items.Clear();
            dd_insuranceType.Items.AddRange(EnumHelper.GetListItems(typeof(InsuranceType)));
        }
        else if (cmd == "view")
        {
            //修改和查看时，需要先查询出模块的信息
            //修改和查看时，传进来的参数id为本模块id
            if (insuranceInfo != null)
            {
                
                lb_insuranceNo.Text = insuranceInfo.InsuranceNo;
                lb_insuranceTarget.Text = insuranceInfo.InsureTarget;
                lb_insuranceType.Text = EnumHelper.GetDescription(insuranceInfo.InsuranceType);
                lb_startDate.Text = insuranceInfo.StartDate.ToString("yyyy-M-d");
                lb_endDate.Text = insuranceInfo.EndDate.ToString("yyyy-M-d");

            }
        }
        else if (cmd == "edit")
        {
            if (insuranceInfo != null)
            {
                dd_insuranceType.Items.Clear();
                dd_insuranceType.Items.AddRange(EnumHelper.GetListItems(typeof(InsuranceType)));
                dd_insuranceType.SelectedValue = Convert.ToString(Convert.ToInt32(insuranceInfo.InsuranceType));

                tb_insuranceNo.Text = insuranceInfo.InsuranceNo;
                tb_insuranceTarget.Text = insuranceInfo.InsureTarget;
                tb_startDate.Text = insuranceInfo.StartDate.ToString("yyyy-M-d");
                tb_endDate.Text = insuranceInfo.EndDate.ToString("yyyy-M-d");
            }
        }

       

        if (cmd == "add")
        {
            HideDisplay();
        }
        if (cmd == "edit")
        {
            HideDisplay();
        }
        if (cmd == "view")
        {
          
            HideEdit();
        }
    }

    private void HideEdit()
    {
        tb_insuranceNo.Visible = false;
        tb_insuranceTarget.Visible = false;
        tb_startDate.Visible = false;
        tb_endDate.Visible = false;
        dd_insuranceType.Visible = false;

        lb_insuranceNo.Visible = true;
        lb_insuranceTarget.Visible = true;
        lb_startDate.Visible = true;
        lb_endDate.Visible = true;
        lb_insuranceType.Visible = true;

        PostButton.Visible = false;
    }

    private void HideDisplay()
    {
        tb_insuranceNo.Visible = true;
        tb_insuranceTarget.Visible = true;
        tb_startDate.Visible = true;
        tb_endDate.Visible = true;
        dd_insuranceType.Visible = true;

        lb_insuranceNo.Visible = false;
        lb_insuranceTarget.Visible = false;
        lb_startDate.Visible = false;
        lb_endDate.Visible = false;
        lb_insuranceType.Visible = false;

        PostButton.Visible = true;
    }

    private void ButtonBind()
    {
        
        if (cmd == "add")
        {

        }
        else if (cmd == "edit")
        {
            //添加新增与删除按钮
            //string moduleName = Label2.Text.Trim();
            HeadMenuButtonItem itemAdd = new HeadMenuButtonItem();
            itemAdd.ButtonName = "新增模块";
            itemAdd.ButtonIcon = "new.gif";
            itemAdd.ButtonPopedom = PopedomType.New;
            itemAdd.ButtonUrl = "?cmd=add&id=" + id;
            HeadMenuWebControls1.ButtonList.Add(itemAdd);

            HeadMenuButtonItem itemDel = new HeadMenuButtonItem();
            itemDel.ButtonName = "删除";
            itemDel.ButtonIcon = "delete.gif";
            itemDel.ButtonPopedom = PopedomType.Delete;
            itemDel.ButtonUrl = string.Format("DelData('?cmd=delete&id={0}')", id);
            itemDel.ButtonUrlType = UrlType.JavaScript;
            HeadMenuWebControls1.ButtonList.Add(itemDel);
        }
        else if (cmd == "view")
        {
            //添加新增与修改按钮
            //view 有两种情况，一是查看最上层的模块，二是查看第二层以下的模块
            //对于查看最上层模块的情况，传进来的参数id为本模块id，parentName为"无"
            //string moduleName = Label2.Text.Trim();
            HeadMenuButtonItem itemAdd = new HeadMenuButtonItem();
            itemAdd.ButtonName = "新增模块";
            itemAdd.ButtonIcon = "new.gif";
            itemAdd.ButtonPopedom = PopedomType.New;
            itemAdd.ButtonUrl = "?cmd=add&id=" + id;
            HeadMenuWebControls1.ButtonList.Add(itemAdd);

            HeadMenuButtonItem itemEdit = new HeadMenuButtonItem();
            itemEdit.ButtonName = "修改";
            itemEdit.ButtonIcon = "edit.gif";
            itemEdit.ButtonPopedom = PopedomType.Edit;
            itemEdit.ButtonUrl = "?cmd=edit&id=" + id;
            HeadMenuWebControls1.ButtonList.Add(itemEdit);

           
        }
        else if (cmd == "delete")
        {
           

            bool bSuccess = false;
            try
            {
                
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除保单失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                string url= "Insurancelist.aspx";

                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除保单(ID:" + id + ")成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl(url), UrlType.Href, "");
            }
        }
    }
   /* /// <summary>
    /// 清空用户输入的URL
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btClearUrl_Click(object sender, EventArgs e)
    {
        TextBox4.Text = "";
    }*/
    /// <summary>
    /// 添加/修改模块
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        InsuranceInfo insuranceInfo = new InsuranceInfo();
        //获取用户输入
        try
        {
            insuranceInfo.InsuranceNo = tb_insuranceNo.Text;
            insuranceInfo.InsureTarget = tb_insuranceTarget.Text;
            insuranceInfo.StartDate = Convert.ToDateTime(tb_startDate.Text);
            insuranceInfo.EndDate = Convert.ToDateTime(tb_endDate.Text);
            insuranceInfo.InsuranceType =(InsuranceType) Convert.ToInt32(dd_insuranceType.SelectedValue);
        }
        catch (WebException ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加保单失败", ex, Icon_Type.Error, true, "window.history.go(-1)",
                UrlType.JavaScript, "");
        }

        bool bSuccess = false;
        Insurance insuranceBll = new Insurance();
        if (cmd == "add")
        {
            try
            {
               insuranceBll.InsertInsurance(insuranceInfo);
               
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加保单失败",ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                string url = "Insurancelist.aspx";

                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加保单成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl(url), UrlType.Href, "");
            }
        }
        if (cmd == "edit")
        {
            try
            {
                //module.ChildCount = GridView1.Rows.Count;
                InsuranceInfo oldInsuranceInfo = (InsuranceInfo) ViewState["InsuranceInfo"];
                insuranceInfo.InsuranceId = oldInsuranceInfo.InsuranceId;
                insuranceBll.UpdateInsurance(insuranceInfo);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改保单信息失败",ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改保单信息成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("InsuranceList.aspx"), UrlType.Href, "");
            }
        }
    }
    protected void ReorderList1_ItemReorder(object sender, AjaxControlToolkit.ReorderListItemReorderEventArgs e)
    {
        List<string> order = (List<string>)Session["order"];
        if (order == null)
            return;

        string tmp = order[e.OldIndex];
        order.RemoveAt(e.OldIndex);
        order.Insert(e.NewIndex, tmp);

        Session["order"] = order;
    }

    /// <summary>
    /// 重排模块顺序
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;
        try
        {
            List<string> order = (List<string>) Session["order"];
            Module module = new Module();
            module.OrderModules(order.ToArray());
            bSuccess = true;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "模块分类排序失败", ex, Icon_Type.Error, true,
                "window.history.go(-1)", UrlType.JavaScript, "");
        }
        if (bSuccess)
        {
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "模块分类排序成功！", Icon_Type.OK, true,
                Common.GetHomeBaseUrl("ModuleManager.aspx?cmd=" + cmd + "&id=" + id), UrlType.Href, "");
        }
    }

}
