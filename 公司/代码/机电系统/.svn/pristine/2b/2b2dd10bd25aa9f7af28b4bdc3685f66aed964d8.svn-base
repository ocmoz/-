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
using System.IO;
using System.Collections.Generic;

using WebUtility;
using WebUtility.WebControls;
using WebUtility.Components;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Utils;

public partial class Module_FM2E_BasicData_DeptManage_Dept : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    //string companyid = (string)Common.sink("companyid", MethodType.Get, 50, 0, DataType.Str);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonBind();
            FillData();
           // Process();

            //调用递归函数，完成树形结构的生成
            AddTree(0, (TreeNode)null);
            TreeView1.ShowLines = true;

            PermissionControl();
        }

        HeadMenuWebControls1.ButtonList[1].ButtonUrl = "../CompanyManage/Company.aspx?cmd=view";
    }

    private void PermissionControl()
    {
        if (SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
        else GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
    }

    private void ButtonBind()
    {
        HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[0];
    }
    private void FillData()
    {
        try
        {
            QueryParam qp = (QueryParam)ViewState["SearchTerm"];
            if (qp == null)
            {
                qp = new QueryParam();
            }
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;
            Department bll = new Department();
            int recordCount = 0;
            IList list = bll.GetList(qp, out recordCount,1);//获取第一层节点
            GridView1.DataSource = list;
            GridView1.DataBind();

            
            AspNetPager1.RecordCount = recordCount;

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败","获取数据失败", ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "view")
        {
            string id = gvRow.Attributes["DepartmentID"];
            //Response.Redirect("ViewDept.aspx?cmd=view&id=" + id+"&companyid="+companyid);
            Response.Redirect("ViewDept.aspx?cmd=view&id=" + id );
        }
        else if (e.CommandName == "del")
        {
            try
            {
                int row = Convert.ToInt32(e.CommandArgument);
                long id = Convert.ToInt64(gvRow.Attributes["DepartmentID"]);
                Department bll = new Department();
                DepartmentInfo departmentinfo = bll.GetDepartment(id);
                deltedepartment(id);
                DepartmentInfo oldparentdept = bll.GetDepartment(departmentinfo.ParentID);
                if (oldparentdept != null)
                {
                    oldparentdept.ChildrenCount -= 1;
                    bll.UpdateDepartment(oldparentdept);
                }
                TreeView1.Nodes.Clear();
                AddTree(0, (TreeNode)null);
                FillData();
                //DepartmentInfo departmentinfo2 = new DepartmentInfo();
                //departmentinfo2.CompanyID = companyid;
                //IList<DepartmentInfo> list = new Department().Search(departmentinfo2);
                //AspNetPager1.RecordCount = list.Count;
                //GridView1.DataSource = list;
                //GridView1.DataBind();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败",ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
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
            childdept.ParentID = departmentinfo.DepartmentID;
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果    
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='White';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#f7f7f7';");

            //设置悬浮鼠标指针形状为"小手"    
            e.Row.Attributes["style"] = "Cursor:hand";

            DepartmentInfo dv = (DepartmentInfo)e.Row.DataItem;
            //long id = dv.DepartmentID;
            e.Row.Attributes["DepartmentID"] = dv.DepartmentID.ToString();
        }    

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        DepartmentInfo item = new DepartmentInfo();
        try
        {
            if (TextBox1.Text.Trim() != string.Empty)
                item.DepartmentID = Convert.ToInt64(Common.inSQL(TextBox1.Text.Trim()));
            item.CompanyName = Common.inSQL(TextBox2.Text.Trim());
            item.StaffName = Common.inSQL(TextBox3.Text.Trim());
            item.Name = Common.inSQL(TextBox4.Text.Trim());
            if (TextBox5.Text.Trim() != string.Empty)
                item.Level = Convert.ToByte(Common.inSQL(TextBox5.Text.Trim()));
            item.ParentName = Common.inSQL(TextBox6.Text.Trim());
            //item.CompanyID = companyid;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入数据格式不正确", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }

        Department bll = new Department();
        QueryParam qp = bll.GenerateSearchTerm(item);
        ViewState["SearchTerm"] = qp;
        AspNetPager1.CurrentPageIndex = 1;
        FillData();
        TabContainer1.ActiveTabIndex = 0;
    }

    private void Process()
    {
        if (cmd == "export")
        {
            //导出
            string file = Server.MapPath("~/public/2.xls");
            FileStream stream = File.Open(file, FileMode.Open);

            byte[] Buffer = null;
            long size;
            size = stream.Length;
            Buffer = new byte[size];
            stream.Read(Buffer, 0, int.Parse(stream.Length.ToString()));
            stream.Close();
            stream = null;

            HttpContext.Current.Response.ContentType = "application/xls";
            string header = "attachment; filename=" + file;
            HttpContext.Current.Response.AddHeader("content-disposition", header);
            HttpContext.Current.Response.BinaryWrite(Buffer);
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();
          
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
            if (node.ParentID == ParentID)
                subnodes.Add(node);
        }

        //循环递归
        foreach (DepartmentInfo node in subnodes)
        {
            //声明节点
            TreeNode Node = new TreeNode();
            //绑定超级链接
            Node.NavigateUrl = "ViewDept.aspx?cmd=view&id=" + node.DepartmentID;// +"&companyid=" + companyid;
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
    }
}
