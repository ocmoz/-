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
using FM2E.Model.Utils;

public partial class Module_FM2E_BasicData_DeviceTypeManage_ViewDeviceType : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    long id = (long)Common.sink("id", MethodType.Get, 255, 0, DataType.Long);
    string showheader = (string)Common.sink("showheader", MethodType.Get, 0, 0, DataType.Str);
    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            ButtonBind();
            BindData();
            AddTree(0, (TreeNode)null);
            OperNodeByID(id.ToString(), TreeView1.Nodes, ref TreeView1);
            TreeView1.ShowLines = true;
        }
        if (showheader == "false")
        {
            HeadMenuWebControls1.ShowHeader = false;
        }
    }

    private void deltecategory(long id)
    {
        Category bll = new Category();
        CategoryInfo categoryinfo = bll.GetCategory(id);
        CategorysearchInfo childcatg = new CategorysearchInfo();
        if (categoryinfo.ChildrenCount > 0)
        {
            childcatg.ParentName = categoryinfo.CategoryName;
            childcatg.ParentID = categoryinfo.CategoryID;

            IList<CategoryInfo> deptlist = bll.Search(childcatg);
            foreach (CategoryInfo depttemp in deptlist)
            {
                deltecategory(depttemp.CategoryID);
            }
            bll.DelCategory(categoryinfo.CategoryID);
        }
        else
            bll.DelCategory(categoryinfo.CategoryID);

    }

    private void ButtonBind()
    {
        HeadMenuButtonItem button1 = HeadMenuWebControls1.ButtonList[1];
        button1.ButtonUrl += "&id=" + id;
        if (cmd == "view")
        {
            //删除
            HeadMenuButtonItem button = HeadMenuWebControls1.ButtonList[3];
            button.ButtonUrlType = UrlType.JavaScript;
            button.ButtonUrl = string.Format("DelData('?cmd=delete&id={0}');", id);
            //修改
            button = HeadMenuWebControls1.ButtonList[2];
            button.ButtonUrlType = UrlType.Href;
            button.ButtonUrl = string.Format("EditDeviceType.aspx?cmd=edit&id={0}", id);
        }
        else if (cmd == "delete")
        {
            //执行删除操作
            bool bSuccess = false;
            try
            {
                //int row = Convert.ToInt32(e.CommandArgument);
                //long id = Convert.ToInt64(gvRow.Attributes["DepartmentID"]);
                Category bll = new Category();
                CategoryInfo dcategoryinfo = bll.GetCategory(id);
                deltecategory(id);
                CategoryInfo oldparentcatg = bll.GetCategory(dcategoryinfo.ParentID);
                if (oldparentcatg != null)
                {
                    oldparentcatg.ChildrenCount -= 1;
                    bll.UpdateCategory(oldparentcatg);
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
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除记录失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            if (bSuccess == true)
            {
                //EventMessage.MessageBox(1, "操作成功", "删除记录ID:(" + id + ")成功！", Icon_Type.OK, Common.GetHomeBaseUrl("DeviceInfo.aspx"));
                EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除记录:成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("DeviceType.aspx"), UrlType.Href, "");
            }
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "view")
        {
            string deviceNum = gvRow.Attributes["CategoryID"];
            Response.Redirect("ViewDeviceType.aspx?cmd=view&id=" + deviceNum);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                int row = Convert.ToInt32(e.CommandArgument);
                long idtemp = Convert.ToInt64(gvRow.Attributes["CategoryID"]);
                Category bll = new Category();
                CategoryInfo categoryinfo = bll.GetCategory(idtemp);
                deltecategory(idtemp);
                CategoryInfo oldparentdept = bll.GetCategory(categoryinfo.ParentID);
                if (oldparentdept != null)
                {
                    oldparentdept.ChildrenCount -= 1;
                    bll.UpdateCategory(oldparentdept);
                }
                //Response.Redirect("ViewDeviceType.aspx?cmd=view&id=" + id);
                TreeView1.Nodes.Clear();
                AddTree(0, (TreeNode)null);
                CategorysearchInfo item2 = new CategorysearchInfo();
                item2.ParentID = id;
                IList<CategoryInfo> list = bll.Search(item2);
                GridView1.DataSource = list;
                GridView1.DataBind();
                //BindData();
                //CategoryInfo Categoryinfo2 = new CategoryInfo();
                //IList<CategoryInfo> list = new Category().Search(Categoryinfo2);
                //AspNetPager1.RecordCount = list.Count;
                //GridView1.DataSource = list;
                //GridView1.DataBind();
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }



    private void BindData()
    {
        try
        {
            Category category = new Category();
            CategoryInfo item = category.GetCategory(id);

            Session["CategoryInfo" + id] = item;     //暂时记录下设备信息，以备编辑时使用
            if (item.CategoryID != 0)
                CategoryID.Text = item.CategoryID.ToString();
            else CategoryID.Text = string.Empty;
            CategoryName.Text = item.CategoryName;
            Unit.Text = item.Unit;
            ParentCategoryName.Text = item.ParentName;
            if (item.ParentName != string.Empty)
                ParentCategoryName.NavigateUrl = "ViewDeviceType.aspx?cmd=view&id=" + item.ParentID;
            if (item.Level != 0)
                Level.Text = item.Level.ToString();
            else Level.Text = string.Empty;
            if (item.ChildrenCount != 0)
                Childrencount.Text = item.ChildrenCount.ToString();
            else Childrencount.Text = string.Empty;
            if (item.DepreciationMethod != 0)
            {
                switch (item.DepreciationMethod)
                {
                    case 1: { DepreciationMethod.Text = "无折旧"; break; }
                    case 2: { DepreciationMethod.Text = "直线折旧"; break; }
                    case 3: { DepreciationMethod.Text = "双倍余额"; break; }
                }
            }
            else DepreciationMethod.Text = string.Empty;
            if (item.DepreciableLife != 0)
                DepreciationLife.Text = item.DepreciableLife.ToString();
            else DepreciationLife.Text = string.Empty;
            if (item.ResidualRate != decimal.Zero)
                ResidualRate.Text = item.ResidualRate.ToString("#,0.##");
            else ResidualRate.Text = string.Empty;

            CategorysearchInfo item2 = new CategorysearchInfo();
            item2.ParentID = item.CategoryID;
            IList<CategoryInfo> list = category.Search(item2);
            GridView1.DataSource = list;
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
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
            CategoryInfo dv = (CategoryInfo)e.Row.DataItem;
            e.Row.Attributes["CategoryID"] = dv.CategoryID.ToString();
        }

    }

    public void AddTree(long ParentID, TreeNode pNode)
    {
        CategorysearchInfo categoryinfo = new CategorysearchInfo();
        categoryinfo.ParentID = ParentID;
        Category bll = new Category();
        QueryParam qp = bll.GenerateSearchTerm(categoryinfo);
        int recordcount = 0;
        IList nodelist = bll.GetList(qp, out recordcount);
        List<CategoryInfo> subnodes = new List<CategoryInfo>();
        foreach (CategoryInfo node in nodelist)
        {
            if (node.ParentID == ParentID)
                subnodes.Add(node);
        }

        //循环递归
        foreach (CategoryInfo node in subnodes)
        {
            //声明节点
            TreeNode Node = new TreeNode();
            //绑定超级链接
            //Node.NavigateUrl = "ViewDeviceType.aspx?cmd=view&id=" + node.CategoryID;
            //开始递归
            if (pNode == null)
            {
                //添加根节点
                Node.Text = node.CategoryName;
                Node.Value = node.CategoryID.ToString();
                TreeView1.Nodes.Add(Node);
                Node.Expanded = false; //节点状态展开
                AddTree(node.CategoryID, Node);    //再次递归

            }
            else
            {
                //添加当前节点的子节点
                Node.Text = node.CategoryName;
                Node.Value = node.CategoryID.ToString();
                //TreeView1.Nodes.Add(Node);
                pNode.ChildNodes.Add(Node);
                Node.Expanded = false; //节点状态展开
                AddTree(node.CategoryID, Node);     //再次递归

            }
        }
    }

    public void OperNodeByID(string nodeID, TreeNodeCollection tnc, ref   TreeView tv)
    {
        foreach (TreeNode node in tnc)
        {
            if (node.Value == nodeID)
            {
                tv.FindNode(node.ValuePath).Selected = true;
                ExpandAllParentNode(tv.FindNode(node.ValuePath));
            }
            if (node.ChildNodes.Count != 0)
                OperNodeByID(nodeID, node.ChildNodes, ref   tv);
        }
    }

    private void ExpandAllParentNode(TreeNode node)
    {
        if (node.Parent != null)
        {
            node.Parent.Expanded = true;
            ExpandAllParentNode(node.Parent);
        }
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        id = Convert.ToInt64(TreeView1.SelectedNode.Value);
        BindData();

    }



}
