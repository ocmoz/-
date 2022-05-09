using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WebUtility;
using WebUtility.Components;
using FM2E.BLL.BudgetManagement;
using FM2E.Model.BudgetManagement;
using System.Collections.Generic;
using FM2E.Model.Exceptions;
using FM2E.Model.Utils;

public partial class Module_FM2E_BudgetManagement_BudgetAccounts_EditSubjectRelation : System.Web.UI.Page
{
    string companyid = UserData.CurrentUserData.CompanyID;
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 255, 0, DataType.Long);

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
                SubjectRelationInfos item;
                if (Session["SubjectRelationInfos" + id] != null)
                    item = (SubjectRelationInfos)Session["SubjectRelationInfos" + id];
                else
                {
                    SubjectRelation bll = new SubjectRelation();
                    item = bll.GetSubjectRelation(id);
                }

                Name.Text = item.Name;
                ViewState["Name"] = item.Name;
                if (item.ParentID != 0)
                {
                    ViewState["OldParentSubID"] = item.ParentID.ToString();
                    ParentName.Text = item.ParentName;
                }
                else
                {
                    ViewState.Remove("OldParentSubID");
                    ParentName.Text = string.Empty;
                }

                ViewState["IsLeaf"] = item.IsLeaf;

                AddTree(0, (TreeNode)null);
                TreeView1.ShowLines = true;

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        else if (cmd == "add")
        {
            try
            {
                if (id > 0)
                {
                    SubjectRelation bllsubjectrelation = new SubjectRelation();
                    SubjectRelationInfos subject = bllsubjectrelation.GetSubjectRelation(id);
                    ParentName.Text = subject.Name;
                    ViewState["ParentSubIDtemp"] = subject.SubID.ToString();
                }

                AddTree(0, (TreeNode)null);
                TreeView1.ShowLines = true;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }

    private void ButtonBind()
    {
        if (cmd == "add")
        {

            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：预算科目信息添加";

            TabPanel1.HeaderText = "添加预算科目";
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：预算科目信息修改";

            TabPanel1.HeaderText = "修改预算科目信息";
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
            SubjectRelationInfos item = new SubjectRelationInfos();
            try
            {
                item.Name = Common.inSQL(Name.Text);
                if (ParentName.Text != string.Empty)
                {
                    if (ViewState["ParentSubIDtemp"] != null)
                        item.ParentID = Convert.ToInt64(ViewState["ParentSubIDtemp"].ToString());
                    else
                        item.ParentID = Convert.ToInt64(ViewState["OldParentSubID"].ToString());
                    item.ParentName = ParentName.Text;

                    if (ViewState["IsLeaf"] != null)
                        item.IsLeaf = Convert.ToInt16(ViewState["IsLeaf"]);
                    else
                        item.IsLeaf = Convert.ToInt16(1);
                    item.CompanyID = companyid;
                }
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入数据的格式不正确", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");

            }

            SubjectRelation bll = new SubjectRelation();
            if ("add" == cmd)
            {
                SubjectRelationInfos subjectinfo = new SubjectRelationInfos();
                subjectinfo.Name = item.Name;
                subjectinfo.ParentID = item.ParentID;
                IList<SubjectRelationInfos> list = bll.Search(subjectinfo);
                if(list.Count!=0)
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "同级科目重复插入名称相同的预算科目", new WebException("同级科目重复插入名称相同的预算科目"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }
            else if ("edit" == cmd)
            {
              //  bool overwrite = false;
                SubjectRelationInfos subjectinfo = new SubjectRelationInfos();
                subjectinfo.Name = item.Name;
                subjectinfo.ParentID = item.ParentID;
                IList<SubjectRelationInfos> list = bll.Search(subjectinfo);
                if (list.Count > 1)
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "已存在名称相同的预算科目", new WebException("已存在名称相同的预算科目"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");

            }

            if (cmd == "add")
            {
                item.CompanyID = companyid;
                item.IsLeaf = 1;
                bll.InsertSubjectRelation(item);
                if ((ViewState["ParentSubIDtemp"] != null) && (ViewState["ParentSubIDtemp"].ToString() != ""))
                {
                    SubjectRelationInfos subjectinfo = bll.GetSubjectRelation(Convert.ToInt64(ViewState["ParentSubIDtemp"].ToString()));
                    if (subjectinfo != null&&subjectinfo.IsLeaf != Convert.ToInt16(2))
                    {
                        subjectinfo.IsLeaf = Convert.ToInt16(2);
                        bll.UpdateSubjectRelation(subjectinfo);

                    }
                }
                bSuccess = true;
                if (bSuccess)
                {
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加预算科目成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("SubjectRelation.aspx"), UrlType.Href, "");
                }
                    
            }
            else if (cmd == "edit")
            {
                item.SubID = id;
                item.CompanyID = companyid;
                bll.UpdateSubjectRelation(item);
                if ((ViewState["OldParentSubID"] != null) && (ViewState["OldParentSubID"].ToString() != string.Empty))
                {
                    SubjectRelationInfos subjectinfo = bll.GetSubjectRelation(Convert.ToInt64(ViewState["OldParentSubID"].ToString()));
                    if (subjectinfo != null)
                    {
                        SubjectRelationInfos subjectchildrens = new SubjectRelationInfos();
                        subjectchildrens.ParentID = subjectinfo.SubID;
                        if (bll.Search(subjectchildrens).Count == 0)
                        {
                            subjectinfo.IsLeaf = 1;
                            bll.UpdateSubjectRelation(subjectinfo);
                        }
                    }
                }
                if (ViewState["ParentSubIDtemp"] != null && ViewState["ParentSubIDtemp"].ToString() != string.Empty)
                {
                    SubjectRelationInfos subjectinfo = bll.GetSubjectRelation(Convert.ToInt64(ViewState["ParentSubIDtemp"].ToString()));
                    if (subjectinfo != null && subjectinfo.IsLeaf != Convert.ToInt16(2))
                    {
                        subjectinfo.IsLeaf = Convert.ToInt16(2);
                        bll.UpdateSubjectRelation(subjectinfo);

                    }
                }

                bSuccess = true;

                if (bSuccess)
                {
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改预算科目信息成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("SubjectRelation.aspx"), UrlType.Href, "");
                }

            }

        }
    }

    public void AddTree(long ParentID, TreeNode pNode)
    {
        SubjectRelationInfos subjectrelationinfo = new SubjectRelationInfos();
        subjectrelationinfo.ParentID = ParentID;
        SubjectRelation bll = new SubjectRelation();
        QueryParam qp = bll.GenerateSearchTerm(subjectrelationinfo);
        int recordcount = 0;
        IList nodelist = bll.GetList(qp, out recordcount, companyid);
        List<SubjectRelationInfos> subnodes = new List<SubjectRelationInfos>();
        foreach (SubjectRelationInfos node in nodelist)
        {
            if (node.ParentID == ParentID)
                subnodes.Add(node);
        }

        //循环递归
        foreach (SubjectRelationInfos node in subnodes)
        {
            //声明节点
            TreeNode Node = new TreeNode();
            //绑定超级链接
            //Node.NavigateUrl = "ViewSubjectRelation.aspx?cmd=view&id=" + node.SubID;
            if (pNode == null)
            {
                Node.Text = node.Name;
                Node.Value = node.SubID.ToString();
                TreeView1.Nodes.Add(Node);
                Node.Expanded = false;
                AddTree(node.SubID, Node);
            }
            else
            {
                Node.Text = node.Name;
                Node.Value = node.SubID.ToString();
                pNode.ChildNodes.Add(Node);
                Node.Expanded = false;
                
                AddTree(node.SubID, Node);
            }
        }
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        ParentName.Text = this.TreeView1.SelectedNode.Text;
        ViewState["ParentSubIDtemp"] = this.TreeView1.SelectedNode.Value;
        //SubjectRelation bll = new SubjectRelation();
        //ViewState["level"] = Convert.ToString(bll.GetCategory(Convert.ToInt64(ViewState["parentcategoryidtemp"].ToString())).Level + 1);
        PopupControlExtender1.Commit(ParentName.Text);



    }





}
