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
using WebUtility.WebControls;
using WebUtility.Components;

using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using System.Collections.Generic;

public partial class Module_FM2E_BasicData_DeptManage_ViewDept : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 255, 0, DataType.Long);
    //string companyid = (string)Common.sink("companyid", MethodType.Get, 50, 0, DataType.Str);

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            ButtonBind();
            BindData();
            AddTree(0, (TreeNode)null);
            OperNodeByID(id.ToString(),TreeView1.Nodes,ref TreeView1);
            TreeView1.ShowLines = true;
        }
        HeadMenuButtonItem button1 = HeadMenuWebControls1.ButtonList[0];
        //button1.ButtonUrl += "&companyid=" + companyid + "&id=" + id;
        button1.ButtonUrl += "&id=" + id;

        //删除
        HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[2];
        button.ButtonUrlType = UrlType.JavaScript;
        //button.ButtonUrl = string.Format("DelData('?cmd=delete&id={0}&companyid={1}');", id, companyid);
        button.ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');",id);

        //修改
        button = HeadMenuWebControls1.ButtonList[1];
        button.ButtonUrlType = UrlType.Href;
        //button.ButtonUrl = string.Format("EditDept.aspx?cmd=edit&id={0}&companyid={1}", id, companyid);
        button.ButtonUrl = string.Format("EditDept.aspx?cmd=edit&id={0}", id);

        //HeadMenuWebControls1.ButtonList[3].ButtonUrl = "../CompanyManage/ViewCompany.aspx?cmd=view&id=" + companyid;
       

    }

    private void ButtonBind()
    {
        //HeadMenuButtonItem button0 = HeadMenuWebControls1.ButtonList[0];
        //button0.ButtonUrl += "?companyid=" + companyid;
        
        if (cmd == "view")
        {
            
        }
        else if (cmd == "delete")
        {
            //执行删除操作
            bool bSuccess = false;
            try
            {
                //int row = Convert.ToInt32(e.CommandArgument);
                //long id = Convert.ToInt64(gvRow.Attributes["DepartmentID"]);
                Department bll = new Department();
                DepartmentInfo departmentinfo = bll.GetDepartment(id);
                deltedepartment(id);
                DepartmentInfo oldparentdept = bll.GetDepartment(departmentinfo.ParentID);
                if (oldparentdept != null)
                {
                    oldparentdept.ChildrenCount -= 1;
                    bll.UpdateDepartment(oldparentdept);
                }
                /*TreeView1.Nodes.Clear();
                AddTree(0, (TreeNode)null);
                DepartmentInfo departmentinfo2 = new DepartmentInfo();
                departmentinfo2.CompanyID = companyid;
                IList<DepartmentInfo> list = new Department().Search(departmentinfo2);
                AspNetPager1.RecordCount = list.Count;
                GridView1.DataSource = list;
                GridView1.DataBind();*/

                bSuccess = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除记录失败",ex , Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess == true)
            {
                //EventMessage.MessageBox(1, "操作成功", "删除记录ID:(" + id + ")成功！", Icon_Type.OK, Common.GetHomeBaseUrl("DeviceInfo.aspx"));
//                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除记录:成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Dept.aspx?companyid=" + companyid), UrlType.Href, "");
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除记录:成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Dept.aspx"), UrlType.Href, "");

            }

        }
    }

    private void deltedepartment(long id)
    {
        Department bll = new Department();
        DepartmentInfo departmentinfo = bll.GetDepartment(id);
        DepartmentInfo childdept = new DepartmentInfo();
        if (departmentinfo.ChildrenCount > 0)
        {
            childdept.ParentName = departmentinfo.Name;
            //childdept.CompanyID = companyid;
            IList<DepartmentInfo> deptlist = bll.Search(childdept);
            foreach (DepartmentInfo depttemp in deptlist)
            {
                deltedepartment(depttemp.DepartmentID);
            }
            bll.DelDepartment(departmentinfo.DepartmentID);
        }
        else
            bll.DelDepartment(departmentinfo.DepartmentID);

    }

    private void BindData()
    {
        try
        {
            Department department = new Department();
            DepartmentInfo item = department.GetDepartment(id);

            Session["DepartmentInfo"+id] = item;     //暂时记录下设备信息，以备编辑时使用

            Label1.Text = item.DepartmentID.ToString();
            Label2.Text = item.CompanyName;
            Label3.Text = item.Name;
            Label4.Text = item.Phone;
            if (item.LeaderID != "")
                Label5.Text = item.StaffName;
            else Label5.Text = string.Empty;
            if (item.ParentID != 0)
                Label6.Text = item.ParentName;
            else Label6.Text = string.Empty;
            Label7.Text = item.Level.ToString();
            Label8.Text = item.ChildrenCount.ToString();
            Label9.Text = item.Remark;

            Label_DepartmentType.Text = EnumHelper.GetDescription(item.DepartmentType);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败","获取数据失败", ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
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
            if ((node.ParentID == ParentID))
                subnodes.Add(node);
        }

        //循环递归
        foreach (DepartmentInfo node in subnodes)
        {
            //声明节点
            TreeNode Node = new TreeNode();
            //绑定超级链接
            //Node.NavigateUrl = "ViewDept.aspx?cmd=view&id=" + node.DepartmentID+"&companyid="+companyid;
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
        id = Convert.ToInt64(TreeView1.SelectedNode.Value);
        BindData();
        
    }

    public void OperNodeByID(string nodeID, TreeNodeCollection tnc, ref   TreeView tv)
    {
        foreach (TreeNode node in tnc)
        {
            if (node.Value == nodeID)
            {
                tv.FindNode(node.ValuePath).Selected = true;
            }
            if (node.ChildNodes.Count!=0)
                OperNodeByID(nodeID, node.ChildNodes, ref   tv);
        }
    } 
}
