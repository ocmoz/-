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
using System.Collections.Generic;
using System.Data.SqlClient;
using FM2E.BLL.Utils;
using FM2E.Model.Exceptions;

public partial class Module_FM2E_BasicData_DeptManage_EditDept : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get,50, 0, DataType.Long);
    //private string companyid = (string)Common.sink("companyid", MethodType.Get, 50, 0, DataType.Str);
    //private long parentcompanyid;

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);

        if (!IsPostBack)
        {
            InitPage();
            FillData();
            ButtonBind();
            AddTree(0, (TreeNode)null);
            if (TreeView1.Nodes.Count == 0)
            {
                TreeNode node = new TreeNode();
                node.Value = "0";
                node.Text = "还没有部门";
                TreeView1.Nodes.Add(node);
            }
            TreeView1.ShowLines = true;
            //FillDataForSelectStaff();
        }
        if (cmd == "edit")
        {
            //HeadMenuWebControls1.ButtonList[0].ButtonUrl = string.Format("ViewDept.aspx?cmd=view&companyid={0}&id={1}", companyid, id);
            HeadMenuWebControls1.ButtonList[0].ButtonUrl = string.Format("ViewDept.aspx?cmd=view&id={0}",  id);
            HeadMenuWebControls1.ButtonList[0].ButtonName = "取消修改";
        }
        else
        {
            //HeadMenuWebControls1.ButtonList[0].ButtonUrl = string.Format("Dept.aspx?cmd=view&companyid={0}", companyid);
            HeadMenuWebControls1.ButtonList[0].ButtonUrl = string.Format("Dept.aspx?cmd=view");
            HeadMenuWebControls1.ButtonList[0].ButtonName = "返回部门列表";
        }
    }


    private void InitPage()
    {
        Company company = new FM2E.BLL.Basic.Company();
        IList<CompanyInfo> list = company.GetAllCompany();
        ddlCompany.Items.Clear();
        foreach (CompanyInfo item in list)
        {
            ListItem li = new ListItem(item.CompanyName, item.CompanyID);
            if (item.IsParentCompany.HasValue&&item.IsParentCompany.Value)
                li.Selected = true;
            ddlCompany.Items.Add(li);
        }

        //部门类型
        DropDownList_DepartmentType.Items.Clear();
        DropDownList_DepartmentType.Items.AddRange(EnumHelper.GetListItems(typeof(DepartmentType)));
    }




    private void FillData()
    {
        TextBox5.ReadOnly = true;
        if (cmd == "edit")
        {
            try
            {
                DepartmentInfo item;
                if (Session["DepartmentInfo"+id] != null)
                {
                    item = (DepartmentInfo)Session["DepartmentInfo"+id];
                }
                else
                {
                    Department bll = new Department();
                    item = bll.GetDepartment(id);
                }
                
                //TextBox1.Text = item.DepartmentID.ToString();
                //TextBox2.Text = item.CompanyName;
                try { ddlCompany.SelectedValue = item.CompanyID; }
                catch { }
                try
                {
                    DropDownList_DepartmentType.SelectedValue = ((int)item.DepartmentType).ToString();
                }
                catch { }

                ViewState["Name"] = item.Name;
                TextBox3.Text = item.Phone;
                if(item.LeaderID!="")
                {
                    TextBox4.Value = item.StaffName;
                    principalID.Value = item.LeaderID.ToString();
                }
                if (item.ParentID != 0)
                {
                    TextBox5.Text = item.ParentName;
                    ViewState["olderparentcompanyid"] = item.ParentID.ToString();
                }
                ViewState["level"] = item.Level.ToString();
                TextBox7.Text = item.ChildrenCount.ToString();
                TextBox8.Text = item.Name;
                TextBox9.Text = item.Remark;
                //olderparentcompanyid.Value = item.ParentID.ToString();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败","获取数据失败", ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        else if (cmd == "add")
        {
            try
            {

                Company bll = new Company();
                Department bllcompany = new Department();
                //TextBox2.Text = bll.GetCompany(companyid).CompanyName;
                
                if (id > 0)
                {
                    
                    DepartmentInfo dept = bllcompany.GetDepartment(id);
                    TextBox5.Text = dept.Name;
                    ViewState["parentcompanyidtemp"] = dept.DepartmentID;
                    ViewState["level"] = Convert.ToString(dept.Level + 1);

                }
                TextBox7.Text = "0";

                //TextBox1.Text = Convert.ToString(bllcompany.GenerateID());
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败","获取数据失败", ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }

    private void ButtonBind()
    {
        HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[0];
        //button.ButtonUrl += "?companyid=" + companyid;
        //button.ButtonUrl += "?companyid=" + companyid;
        if (cmd == "add")
        {

            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：部门添加";
            

            //TabPanel1.HeaderText = "添加部门";
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：部门修改";

           // TabPanel1.HeaderText = "修改部门信息";
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            DataRowView dv = (DataRowView)e.Row.DataItem;
            string staffID = (string)dv.Row.ItemArray.GetValue(1);

            CheckBox cb = (CheckBox)e.Row.FindControl("checkBox1");
            if (cb != null)
                cb.Attributes.Add("onclick", "onClientClick('" + cb.ClientID + "','" + e.Row.Cells[2].Text + "','" + staffID + "')");
        }
    }
    /// <summary>
    /// 提交添加或修改的部门事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {

        bool bSuccess = false;

        if (cmd == "add" || cmd == "edit")
        {
            DepartmentInfo item = new DepartmentInfo();

            try
            {
                //item.DepartmentID = Convert.ToInt64(TextBox1.Text.Trim());
                item.DepartmentID = id;
                item.DepartmentType = (DepartmentType)Convert.ToInt32(DropDownList_DepartmentType.SelectedValue);
                //item.CompanyName = TextBox2.Text.Trim();
                //item.CompanyID = companyid;
                item.CompanyID = ddlCompany.SelectedValue;
                item.CompanyName = ddlCompany.SelectedItem.Text;
                item.Phone = TextBox3.Text.Trim();
                if ((TextBox4.Value != null) && (TextBox4.Value != ""))
                {
                    item.StaffName = TextBox4.Value;
                    item.LeaderID = principalID.Value;
                }
                else
                {
                    item.StaffName = "";
                    item.LeaderID = "";
                }

                if (TextBox5.Text.Trim() != "")
                {
                    if (ViewState["parentcompanyidtemp"]!=null)
                        item.ParentID = Convert.ToInt64(ViewState["parentcompanyidtemp"].ToString());
                    else
                        item.ParentID = Convert.ToInt64(ViewState["olderparentcompanyid"].ToString());
                    item.ParentName = TextBox5.Text.Trim();
                }
                if ((ViewState["level"] != null) && (ViewState["level"].ToString() != ""))
                {
                    item.Level = Convert.ToByte(ViewState["level"].ToString());
                }
                else
                    item.Level = 1;
                item.ChildrenCount = Convert.ToInt64(TextBox7.Text.ToString());
                item.Name = TextBox8.Text.ToString();
                item.Remark = TextBox9.Text.ToString();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入数据的格式不正确", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }

            Department bll = new Department();
            if (cmd == "add")
            {
                DepartmentInfo departmentinfo = new DepartmentInfo();
                departmentinfo.Name = item.Name;
                //departmentinfo.CompanyID = companyid;
                departmentinfo.CompanyID = ddlCompany.SelectedValue;
                IList<DepartmentInfo> list = bll.Search(departmentinfo);
                if(list.Count!=0)
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "重复插入名称相同的部门", new WebException("重复插入名称相同的部门"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }
            else if (cmd == "edit")
            {
                bool overwrite = false;
                DepartmentInfo departmentinfo = new DepartmentInfo();
                departmentinfo.Name = item.Name;
                //departmentinfo.CompanyID = companyid;
                //departmentinfo.CompanyID = ddlCompany.SelectedValue;
                IList<DepartmentInfo> list = bll.Search(departmentinfo);
                if (list.Count != 0 && list[0].Name != ViewState["Name"].ToString())
                    overwrite = true;
                if (list.Count > 1 && list[0].Name != ViewState["Name"].ToString())
                    overwrite = true;
                if(overwrite)
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "已存在名称相同的部门", new WebException("已存在名称相同的部门"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }

            if (cmd == "add")
            {
                //Department bll = new Department();
                item.DepartmentID = bll.InsertDepartment(item);
                if ((ViewState["parentcompanyidtemp"]!=null) && (ViewState["parentcompanyidtemp"].ToString() != ""))
                {
                    DepartmentInfo parentdept = bll.GetDepartment(Convert.ToInt64(ViewState["parentcompanyidtemp"].ToString()));
                    if (parentdept != null)
                    {
                        parentdept.ChildrenCount += 1;
                        bll.UpdateDepartment(parentdept);
                    }
                }
                bSuccess = true;
                if (bSuccess)
                {
                    //EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加部门成功！", Icon_Type.OK, true , Common.GetHomeBaseUrl("Dept.aspx?companyid=" + companyid), UrlType.Href, "");
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加部门成功！", Icon_Type.OK, true , Common.GetHomeBaseUrl("Dept.aspx"), UrlType.Href, "");

                }

            }
            else if (cmd == "edit")
            {

                //Department bll = new Department();
                bll.UpdateDepartment(item);
                if ((ViewState["olderparentcompanyid"] != null) && (ViewState["olderparentcompanyid"].ToString() != ""))
                {
                    DepartmentInfo oldparentdept = bll.GetDepartment(Convert.ToInt64(ViewState["olderparentcompanyid"].ToString()));
                    if (oldparentdept != null)
                    {
                        if(oldparentdept.ChildrenCount>0)
                        oldparentdept.ChildrenCount -= 1;
                        bll.UpdateDepartment(oldparentdept);
                    }
                }
                if ((ViewState["parentcompanyidtemp"] != null) && (ViewState["parentcompanyidtemp"].ToString() != ""))
                {
                    DepartmentInfo parentdept = bll.GetDepartment(Convert.ToInt64(ViewState["parentcompanyidtemp"].ToString()));
                    if (parentdept != null)
                    {
                        parentdept.ChildrenCount += 1;
                        bll.UpdateDepartment(parentdept);
                    }
                }
                bSuccess = true;

                if (bSuccess)
                {
                    //EventMessage.MessageBox(Msg_Type.Error, "操作成功", "修改部门信息成功！", Icon_Type.OK, true , Common.GetHomeBaseUrl("Dept.aspx?companyid=" + companyid), UrlType.Href, "");
                    EventMessage.MessageBox(Msg_Type.Error, "操作成功", "修改部门信息成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Dept.aspx"), UrlType.Href, "");
                }
            }
        }
    }

    public void AddTree(long ParentID, TreeNode pNode)
    {

        DepartmentInfo departmentinfo = new DepartmentInfo();
        //departmentinfo.CompanyID = companyid;
        departmentinfo.ParentID = ParentID;
        IList<DepartmentInfo> nodelist = new Department().Search(departmentinfo);
        List<DepartmentInfo> subnodes = new List<DepartmentInfo>();
        foreach (DepartmentInfo node in nodelist)
        {
            if (cmd == "edit" && id == node.DepartmentID)
                continue;
            if ((node.ParentID == ParentID))
                subnodes.Add(node);
        }

        //循环递归
        foreach (DepartmentInfo node in subnodes)
        {
            if (cmd == "edit" && id == node.DepartmentID)
                continue;
            //声明节点
            TreeNode Node = new TreeNode();
            //绑定超级链接
            //开始递归
            if (pNode == null)
            {
                //添加根节点
                Node.Text = node.Name;
                Node.Value = node.DepartmentID.ToString();
                TreeView1.Nodes.Add(Node);
                Node.Expanded = true; //节点状态展开
                AddTree(node.DepartmentID, Node);    //再次递归
            }
            else
            {
                //添加当前节点的子节点
                Node.Text = node.Name;
                Node.Value = node.DepartmentID.ToString();
                //TreeView1.Nodes.Add(Node);
                pNode.ChildNodes.Add(Node);
                Node.Expanded = true; //节点状态展开
                AddTree(node.DepartmentID, Node);     //再次递归
            }
        }
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (cmd == "edit")//编辑的时候无法选择自己作为父节点
        {
            if (id == long.Parse(TreeView1.SelectedNode.Value))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "alertselect", "alert('不能选择自身作为父节点');", true);
                return;
            }

        }
        TextBox5.Text = this.TreeView1.SelectedNode.Text;
        ViewState["parentcompanyidtemp"] = this.TreeView1.SelectedNode.Value;
        Department bll = new Department();
        ViewState["level"] = Convert.ToString(bll.GetDepartment(Convert.ToInt64(ViewState["parentcompanyidtemp"].ToString())).Level + 1);

        PopupControlExtender1.Commit(TextBox5.Text);
    }


}
