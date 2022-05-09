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
using FM2E.Model.Basic;
using FM2E.BLL.Basic;
using System.Data.SqlClient;
using FM2E.Model.Utils;
using System.Collections.Generic;
using FM2E.Model.Exceptions;

public partial class Module_FM2E_BasicData_DeviceTypeManage_EditDeviceType : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 255, 0, DataType.Long);
    string showheader = (string)Common.sink("showheader", MethodType.Get, 0, 0, DataType.Str);
    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);

        if (!IsPostBack)
        {
            FillData();
            ButtonBind();
        }
        if (showheader == "false")
        {
            HeadMenuWebControls1.ShowHeader = false;
        }
    }

    private void FillData()
    {
        if (cmd == "edit")
        {
            try
            {
                CategoryInfo item;
                if (Session["CategoryInfo" + id] != null)
                {
                    item = (CategoryInfo)Session["CategoryInfo" + id];
                }
                else
                {
                    Category bll = new Category();
                    item = bll.GetCategory(id);
                }

                categoryname.Text = item.CategoryName;
                ViewState["CategoryName"] = item.CategoryName;
                Unit.Items.Clear();
                Unit.Items.Add(new ListItem("请选择单位", ""));
                IList unitlist = Constants.GetUnits();
                foreach (string unit in unitlist)
                    Unit.Items.Add(new ListItem(unit, unit));
                Unit.SelectedValue = item.Unit;
                if (item.ParentID != 0)
                {
                    ViewState["olderparentcategory"] = item.ParentID.ToString();
                    parentcategoryname.Text = item.ParentName;
                }
                else
                {
                    ViewState.Remove("olderparentcategory");
                    parentcategoryname.Text = string.Empty;
                }
                ViewState["level"] = item.Level.ToString();
                ViewState["ChildrenCount"] = item.ChildrenCount.ToString();
                depreciationmethod.SelectedIndex = item.DepreciationMethod;
                if (item.DepreciableLife != 0)
                    depreciablelife.Text = item.DepreciableLife.ToString();
                else depreciablelife.Text = string.Empty;
                if (item.ResidualRate != decimal.Zero)
                    residualrate.Text = item.ResidualRate.ToString();
                else residualrate.Text = string.Empty;

                AddTree(0, (TreeNode)null);
                TreeView1.ShowLines = true;


                
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败","获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        else if (cmd == "add")
        {
            try
            {
                Unit.Items.Clear();
                Unit.Items.Add(new ListItem("请选择单位", ""));
                IList unitlist = Constants.GetUnits();
                foreach (string unit in unitlist)
                    Unit.Items.Add(new ListItem(unit, unit));
                //Category bll = new Category();
                //TextBox2.Text = bll.Getcategory(categoryid).categoryName;
                if (id > 0)
                {
                    Category bllcategory = new Category();
                    CategoryInfo catg = bllcategory.GetCategory(id);
                    parentcategoryname.Text = catg.CategoryName;
                    //zhong.Text = catg.Name;
                    ViewState["parentcategoryidtemp"] = catg.CategoryID.ToString();
                    ViewState["level"] = Convert.ToString(catg.Level + 1);
                    
                }
                ViewState["ChildrenCount"] = "0";
                AddTree(0, (TreeNode)null);
                TreeView1.ShowLines = true;
                
                
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

            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：设备种类信息添加";

            TabPanel1.HeaderText = "添加设备种类";
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：设备种类信息修改";

            TabPanel1.HeaderText = "修改设备种类信息";
        }
    }
    /// <summary>
    /// 保存添加或修改的内容
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;

        if (cmd == "add" || cmd == "edit")
        {
            CategoryInfo item = new CategoryInfo();
            try
            {
                item.CategoryName = categoryname.Text;
                item.Unit = Unit.SelectedValue;
                if (parentcategoryname.Text != string.Empty)
                {
                    if (ViewState["parentcategoryidtemp"] != null)
                        item.ParentID = Convert.ToInt64(ViewState["parentcategoryidtemp"].ToString());
                    else item.ParentID = Convert.ToInt64(ViewState["olderparentcategory"].ToString());
                    item.ParentName = parentcategoryname.Text;
                }
                if ((ViewState["level"] != null) && (ViewState["level"].ToString() != ""))
                {
                    item.Level = Convert.ToByte(ViewState["level"].ToString());
                }
                else
                    item.Level = 1;
                if (ViewState["ChildrenCount"] != null)
                    item.ChildrenCount = Convert.ToInt32(ViewState["ChildrenCount"]);
                item.DepreciationMethod = depreciationmethod.SelectedIndex;
                if (depreciablelife.Text != string.Empty)
                    item.DepreciableLife = Convert.ToInt32(depreciablelife.Text);
                if (residualrate.Text != string.Empty)
                    item.ResidualRate = Convert.ToDecimal(residualrate.Text);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入数据的格式不正确", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");

            }

            Category bll = new Category();
            if ("add"==cmd)
            {
                CategorysearchInfo categoryinfo = new CategorysearchInfo();
                categoryinfo.CategoryName = item.CategoryName;
                IList<CategoryInfo> list = bll.Search(categoryinfo);
                if(list.Count!=0)
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "重复插入名称相同的设备种类", new WebException("重复插入名称相同的设备种类"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");

            }
            else if ("edit" == cmd)
            {
                bool overwrite = false;
                CategorysearchInfo categoryinfo = new CategorysearchInfo();
                categoryinfo.CategoryName = item.CategoryName;
                IList<CategoryInfo> list = bll.Search(categoryinfo);
                if (list.Count != 0 && list[0].CategoryName != ViewState["CategoryName"].ToString())
                    overwrite = true;
                if (list.Count > 1 && list[1].CategoryName != ViewState["CategoryName"].ToString())
                    overwrite = true;
                if(overwrite)
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "重复插入名称相同的设备种类", new WebException("重复插入名称相同的设备种类"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");

            }


            if (cmd == "add")
            {
                CategoryInfo parentcatg = null;
                if ((ViewState["parentcategoryidtemp"] != null) && (ViewState["parentcategoryidtemp"].ToString() != ""))
                {
                    parentcatg = bll.GetCategory(Convert.ToInt64(ViewState["parentcategoryidtemp"].ToString()));
                    item.CategoryCode = bll.GetNextCategoryCode(parentcatg.CategoryCode);
                    item.NextCategoryCode = 1;
                }
                else
                {
                    item.CategoryCode = bll.GetNextCategoryCode("");
                    item.NextCategoryCode = 1;
                }
                
                bll.InsertCategory(item);

                if (parentcatg != null)
                {
                    parentcatg.ChildrenCount += 1;
                    bll.UpdateCategory(parentcatg);
                }
                bSuccess = true;
                if (bSuccess)
                {
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加设备种类成功！", Icon_Type.OK, true , Common.GetHomeBaseUrl("DeviceType.aspx"), UrlType.Href, "");
                }
            }
            else if (cmd == "edit")
            {
                item.CategoryID = id;
                CategoryInfo parentcatg = null;
                if ((ViewState["parentcategoryidtemp"] != null) && (ViewState["parentcategoryidtemp"].ToString() != ""))
                {
                    parentcatg = bll.GetCategory(Convert.ToInt64(ViewState["parentcategoryidtemp"].ToString()));
                    item.CategoryCode = bll.GetNextCategoryCode(parentcatg.CategoryCode);
                }
                else
                    item.CategoryCode = bll.GetNextCategoryCode("");
                
                bll.UpdateCategory(item);
                if ((ViewState["olderparentcategoryid"] != null) && (ViewState["olderparentcategoryid"].ToString() != ""))
                {
                    CategoryInfo oldparentcatg = bll.GetCategory(Convert.ToInt64(ViewState["olderparentcategoryid"].ToString()));
                    if (oldparentcatg != null)
                    {
                        oldparentcatg.ChildrenCount -= 1;
                        bll.UpdateCategory(oldparentcatg);
                    }
                }
                if (parentcatg != null)
                {
                    parentcatg.ChildrenCount += 1;
                    bll.UpdateCategory(parentcatg);
                }
                
                bSuccess = true;

                if (bSuccess)
                {
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改设备种类信息成功！", Icon_Type.OK, true , Common.GetHomeBaseUrl("DeviceType.aspx"), UrlType.Href, "");
                }
            }
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
            if (cmd == "edit")
            {
                if (node.CategoryID == id)
                    continue;
            }
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

            if (cmd == "edit")
            {
                if (node.CategoryID == id)
                    continue;
            }

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

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        parentcategoryname.Text = this.TreeView1.SelectedNode.Text;
        ViewState["parentcategoryidtemp"] = this.TreeView1.SelectedNode.Value;
        Category bll = new Category();
        ViewState["level"] = Convert.ToString(bll.GetCategory(Convert.ToInt64(ViewState["parentcategoryidtemp"].ToString())).Level+1);
        PopupControlExtender1.Commit(parentcategoryname.Text);

       

    }




}
