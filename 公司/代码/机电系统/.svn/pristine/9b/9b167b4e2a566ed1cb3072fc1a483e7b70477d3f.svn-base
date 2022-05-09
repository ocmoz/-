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

using WebUtility;
using FM2E.Model.Utils;
using FM2E.BLL.Basic;
using WebUtility.Components;
using FM2E.Model.Basic;
using System.Collections.Generic;

public partial class Module_FM2E_BasicData_DeviceTypeManage_DeviceType : System.Web.UI.Page
{
    string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    string showheader = (string)Common.sink("showheader", MethodType.Get, 0, 0, DataType.Str);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
            //Process();
            AddTree(0, (TreeNode)null);
            TreeView1.ShowLines = true;

            PermissionControl();
        }
        if (showheader == "false")
        {
            HeadMenuWebControls1.ShowHeader = false;
        }
    }

    private void PermissionControl()
    {
        if (SystemPermission.CheckPermission(PopedomType.Delete))
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
        else GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillData();
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
            Category bll = new Category();
            int recordCount = 0;
            IList list = bll.GetList(qp, out recordCount);
            GridView1.DataSource = list;
            GridView1.DataBind();


            AspNetPager1.RecordCount = recordCount;

        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }



    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "view")
        {
            string deviceNum = gvRow.Attributes["CategoryID"];
            Response.Redirect("ViewDeviceType.aspx?cmd=view&id=" + deviceNum + "&showheader=" + showheader);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                int row = Convert.ToInt32(e.CommandArgument);
                long id = Convert.ToInt64(gvRow.Attributes["CategoryID"]);
                Category bll = new Category();
                CategoryInfo categoryinfo = bll.GetCategory(id);
                deltecategory(id);
                CategoryInfo oldparentdept = bll.GetCategory(categoryinfo.ParentID);
                if (oldparentdept != null)
                {
                    oldparentdept.ChildrenCount -= 1;
                    bll.UpdateCategory(oldparentdept);
                }
                TreeView1.Nodes.Clear();
                AddTree(0, (TreeNode)null);
                FillData();
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        CategorysearchInfo item = new CategorysearchInfo();
        if (zhongleibianma.Value != string.Empty)
            item.CategoryID = Convert.ToInt64(Common.inSQL(zhongleibianma.Value.Trim()));
        item.CategoryName = Common.inSQL(zhongleiming.Value.Trim());
        if (shebeidanwei.Value != string.Empty)
            item.Unit = Common.inSQL(shebeidanwei.Value.Trim());
        item.ParentName = Common.inSQL(shangjizhonglei.Value.Trim());
        if (Convert.ToInt32(zhejiufangfa.SelectedValue) != 0)
            item.DepreciationMethod = Convert.ToInt32(zhejiufangfa.SelectedValue.Trim());
        if (zhejiunianxian1.Value != string.Empty && zhejiunianxian2.Value != string.Empty)
        {
            item.DepreciableLife1 = Convert.ToInt32(Common.inSQL(zhejiunianxian1.Value.Trim()));
            item.DepreciableLife2 = Convert.ToInt32(Common.inSQL(zhejiunianxian2.Value.Trim()));
        }
        if (jinchangzhilv1.Value != string.Empty && jinchangzhilv2.Value != string.Empty)
        {
            item.ResidualRate1 = Convert.ToDecimal(Common.inSQL(jinchangzhilv1.Value.Trim()));
            item.ResidualRate2 = Convert.ToDecimal(Common.inSQL(jinchangzhilv2.Value.Trim()));
        }
        Category bll = new Category();
        QueryParam qp = bll.GenerateSearchTerm(item);
        ViewState["SearchTerm"] = qp;
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
            Node.NavigateUrl = "ViewDeviceType.aspx?cmd=view&id=" + node.CategoryID + "&showheader=" + showheader;
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
}
