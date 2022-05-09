using System;

using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

using System.IO;
using System.Windows.Forms.VisualStyles;
using FM2E.BLL.Insurance;
using FM2E.Model.Insurance;
using WebUtility;
using WebUtility.Components;
using WebUtility.WebControls;

using FM2E.BLL.System;
using FM2E.Model.Exceptions;

using FM2E.WorkflowLayer;
using FM2E.BLL.Utils;
using FM2E.Model.Workflow;
using System.Configuration;
using System.Collections;
using FM2E.Model.System;
using System.Text.RegularExpressions;

public partial class Module_FM2E_InsureManager_InsureInfoManager_InsuranceManager : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private int id = 0;
    InsuranceReport insuranceReportBll = new InsuranceReport();

    private const string UPLOADFOLDER = "~/public/InsuranceReport/";

    protected void Page_Load(object sender, EventArgs e)
    {
        id = (int)Common.sink("id", MethodType.Get, 50, 0, DataType.Int);

        SystemPermission.CheckCommandPermission(cmd);

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

    private void IsToView1(bool isbool)
    {
        tb_insuranceNo.Visible = !isbool;
        tb_reportNo.Visible = !isbool;
        tb_riskDate.Visible = !isbool;
        tb_reportDate.Visible = !isbool;

        tb_riskTypeName.Visible = !isbool;
        tb_riskContent.Visible = !isbool;

        tb_ReceiptNo.Visible = !isbool;
        tb_Estimate.Visible = !isbool;
        tb_Claim.Visible = !isbool;
        tb_Address.Visible = !isbool;

        lb_insuranceNo.Visible = isbool;
        lb_reportNo.Visible = isbool;
        lb_riskDate.Visible = isbool;
        lb_reportDate.Visible = isbool;

        lb_riskType.Visible = isbool;
        lb_riskContent.Visible = isbool;

        lb_ReceiptNo.Visible = isbool;
        lb_Estimate.Visible = isbool;
        lb_Claim.Visible = isbool;
        lb_Address.Visible = isbool;

        add1.Visible = !isbool;
    }
    private void IsToView2(bool isbool) 
    {
        tb_repairContent.Visible = !isbool;
        lb_repairContent.Visible = isbool;
        add2.Visible = !isbool;
    }
    private void IsToView3(bool isbool)
    {
        tb_reviewContent.Visible = !isbool;
        lb_reviewContent.Visible = isbool;
    } 
    private void FillData()
    {        
        InsuranceReportInfo insuranceReportInfo = null;
      
        if (id != 0)
        {
            insuranceReportInfo = insuranceReportBll.GetInsuranceReportInfo(id);
        }

        ViewState["InsuranceReportInfo"] = insuranceReportInfo;

        string separatorStr = "@First@";
        string[] split = { separatorStr };      

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        
        if (cmd == "add")
        {
            IsToView1(false);
            table_repair.Visible = false;
            tableRow_review.Visible = false;
        }
        else if (cmd == "view")
        {
            IsToView1(true);

            PostButton.Visible = false;

            lb_insuranceNo.Text = insuranceReportInfo.InsuranceNo;
            lb_reportNo.Text = insuranceReportInfo.ReportNo;
            string riskTypeTemp;
            if (insuranceReportInfo.RiskType == RiskType.Other)
            {
                riskTypeTemp = "其他(" + insuranceReportInfo.RiskTypeName + ")";
            }
            else
            {
                riskTypeTemp = insuranceReportInfo.RiskTypeName; 
             
            }
            rb_riskType.SelectedValue = ((int)insuranceReportInfo.RiskType).ToString();
            lb_riskType.Text = riskTypeTemp;
           
            lb_riskDate.Text = insuranceReportInfo.RiskDate.ToString("yyyy-M-d");
            lb_reportDate.Text = insuranceReportInfo.ReportDate.ToString("yyyy-M-d");
            lb_riskContent.Text = insuranceReportInfo.RiskContent;
            lb_ReceiptNo.Text = insuranceReportInfo.ReceiptNo;
            lb_Estimate.Text = insuranceReportInfo.Estimate;
            lb_Claim.Text = insuranceReportInfo.Claim;
            lb_Address.Text = insuranceReportInfo.Address;

            #region 附件绑定
            if (insuranceReportInfo.RiskAttachment != null)
            {
                string[] editreason1 = insuranceReportInfo.RiskAttachment.Split(split, StringSplitOptions.None);
                List<CurrentStatusFile> arList1 = new List<CurrentStatusFile>();

                int editreason1id = 1;
                for (int i = 0; i < editreason1.Length; i++)
                {
                    string[] bb = { "#" };
                    string[] arsplitsplit = editreason1[i].Split(bb, StringSplitOptions.None);
                    if (arsplitsplit.Length == 2)
                    {
                        CurrentStatusFile ar = new CurrentStatusFile(editreason1id, arsplitsplit[0], arsplitsplit[1]);
                        arList1.Add(ar);
                        editreason1id += 1;
                    }
                }
                gridviewFile.DataSource = arList1;
                gridviewFile.DataBind();
            }
            #endregion           

            if (insuranceReportInfo.State == State.New)
            {
                table_repair.Visible = false;
                tableRow_review.Visible = false;
            }
            else if (insuranceReportInfo.State == State.Repaired)
            {
                IsToView2(true);
                tableRow_review.Visible = false;                
                lb_repairContent.Text = insuranceReportInfo.RepairContent;

                #region 附件绑定

                if (insuranceReportInfo.RepairAttachment != null)
                {
                    string[] editreason2 = insuranceReportInfo.RepairAttachment.Split(split, StringSplitOptions.None);
                    List<CurrentStatusFile> arList2 = new List<CurrentStatusFile>();

                    int editreason2id = 1;
                    for (int i = 0; i < editreason2.Length; i++)
                    {
                        string[] bb = { "#" };
                        string[] arsplitsplit = editreason2[i].Split(bb, StringSplitOptions.None);
                        if (arsplitsplit.Length == 2)
                        {
                            CurrentStatusFile ar = new CurrentStatusFile(editreason2id, arsplitsplit[0], arsplitsplit[1]);
                            arList2.Add(ar);
                            editreason2id += 1;
                        }
                    }
                    gridview_repairAttachment.DataSource = arList2;
                    gridview_repairAttachment.DataBind();
                }
                #endregion
                lb_StationManager.InnerText = "站级负责人：" + insuranceReportInfo.StationManager;
            }
            else
            {
                IsToView2(true);
                IsToView3(true);
                lb_repairContent.Text = insuranceReportInfo.RepairContent;
                #region 附件绑定

                if (insuranceReportInfo.RepairAttachment != null)
                {
                    string[] editreason2 = insuranceReportInfo.RepairAttachment.Split(split, StringSplitOptions.None);
                    List<CurrentStatusFile> arList2 = new List<CurrentStatusFile>();

                    int editreason2id = 1;
                    for (int i = 0; i < editreason2.Length; i++)
                    {
                        string[] bb = { "#" };
                        string[] arsplitsplit = editreason2[i].Split(bb, StringSplitOptions.None);
                        if (arsplitsplit.Length == 2)
                        {
                            CurrentStatusFile ar = new CurrentStatusFile(editreason2id, arsplitsplit[0], arsplitsplit[1]);
                            arList2.Add(ar);
                            editreason2id += 1;
                        }
                    }
                    gridview_repairAttachment.DataSource = arList2;
                    gridview_repairAttachment.DataBind();
                }
                #endregion
                lb_reviewContent.Text = insuranceReportInfo.ReviewContent;
                lb_StationManager.InnerText = "站级负责人：" + insuranceReportInfo.StationManager;
                lb_insutanceManager.InnerText = "保险负责人：" + insuranceReportInfo.InsuranceManager;
            }
        }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        else if (cmd == "edit")
        {
            IsToView1(false);
            tb_insuranceNo.Text = insuranceReportInfo.InsuranceNo;
            tb_reportNo.Text = insuranceReportInfo.ReportNo;
            tb_riskDate.Text = insuranceReportInfo.RiskDate.ToString("yyyy-M-d");
            tb_reportDate.Text = insuranceReportInfo.ReportDate.ToString("yyyy-M-d");
            tb_ReceiptNo.Text = insuranceReportInfo.ReceiptNo;
            tb_Estimate.Text = insuranceReportInfo.Estimate;
            tb_Claim.Text = insuranceReportInfo.Claim;
            tb_Address.Text = insuranceReportInfo.Address;
            string riskTypeTemp;
            if (insuranceReportInfo.RiskType == RiskType.Other)
            {
                riskTypeTemp = "其他(" + insuranceReportInfo.RiskTypeName + ")";
            }
            else
            {
                riskTypeTemp = insuranceReportInfo.RiskTypeName;

            }
            rb_riskType.SelectedValue =((int)insuranceReportInfo.RiskType).ToString();
            lb_riskType.Text = riskTypeTemp;
            #region 附件绑定
            tb_riskContent.Text = insuranceReportInfo.RiskContent;
            if (insuranceReportInfo.RiskAttachment != null)
            {
                string[] editreason1 = insuranceReportInfo.RiskAttachment.Split(split, StringSplitOptions.None);
                List<CurrentStatusFile> arList1 = new List<CurrentStatusFile>();

                int editreason1id = 1;
                for (int i = 0; i < editreason1.Length; i++)
                {
                    string[] bb = { "#" };
                    string[] arsplitsplit = editreason1[i].Split(bb, StringSplitOptions.None);
                    if (arsplitsplit.Length == 2)
                    {
                        CurrentStatusFile ar = new CurrentStatusFile(editreason1id, arsplitsplit[0], arsplitsplit[1]);
                        arList1.Add(ar);
                        editreason1id += 1;
                    }
                }
                gridviewFile.DataSource = arList1;
                gridviewFile.DataBind();
            }
            #endregion

            if (insuranceReportInfo.State == State.New)
            {
                table_repair.Visible = false;
                tableRow_review.Visible = false;           
            }

            else if (insuranceReportInfo.State == State.Repaired)
            {
                IsToView2(false);
                lb_repairContent.Text = insuranceReportInfo.RepairContent;
                tableRow_review.Visible = false;   
                #region 附件绑定

                tb_repairContent.Text = insuranceReportInfo.RepairContent;
                if (insuranceReportInfo.RepairAttachment != null)
                {
                    string[] editreason2 = insuranceReportInfo.RepairAttachment.Split(split, StringSplitOptions.None);
                    List<CurrentStatusFile> arList2 = new List<CurrentStatusFile>();

                    int editreason2id = 1;
                    for (int i = 0; i < editreason2.Length; i++)
                    {
                        string[] bb = { "#" };
                        string[] arsplitsplit = editreason2[i].Split(bb, StringSplitOptions.None);
                        if (arsplitsplit.Length == 2)
                        {
                            CurrentStatusFile ar = new CurrentStatusFile(editreason2id, arsplitsplit[0], arsplitsplit[1]);
                            arList2.Add(ar);
                            editreason2id += 1;
                        }
                    }
                    gridview_repairAttachment.DataSource = arList2;
                    gridview_repairAttachment.DataBind();
                }
                #endregion
            }
            else
            {
                IsToView3(false);
                #region 附件绑定

                tb_repairContent.Text = insuranceReportInfo.RepairContent;
                if (insuranceReportInfo.RepairAttachment != null)
                {
                    string[] editreason2 = insuranceReportInfo.RepairAttachment.Split(split, StringSplitOptions.None);
                    List<CurrentStatusFile> arList2 = new List<CurrentStatusFile>();

                    int editreason2id = 1;
                    for (int i = 0; i < editreason2.Length; i++)
                    {
                        string[] bb = { "#" };
                        string[] arsplitsplit = editreason2[i].Split(bb, StringSplitOptions.None);
                        if (arsplitsplit.Length == 2)
                        {
                            CurrentStatusFile ar = new CurrentStatusFile(editreason2id, arsplitsplit[0], arsplitsplit[1]);
                            arList2.Add(ar);
                            editreason2id += 1;
                        }
                    }
                    gridview_repairAttachment.DataSource = arList2;
                    gridview_repairAttachment.DataBind();
                }
                #endregion
                lb_repairContent.Text = insuranceReportInfo.RepairContent;
                tb_reviewContent.Text = insuranceReportInfo.ReviewContent;
            }
        }
        else if (cmd == "repair")//修复
        {
            IsToView1(true);
            IsToView2(false);
            lb_insuranceNo.Text = insuranceReportInfo.InsuranceNo;
            lb_reportNo.Text = insuranceReportInfo.ReportNo;
            string riskTypeTemp;
            if (insuranceReportInfo.RiskType == RiskType.Other)
            {
                riskTypeTemp = "其他(" + insuranceReportInfo.RiskTypeName + ")";
            }
            else
            {
                riskTypeTemp = insuranceReportInfo.RiskTypeName;
            }

            lb_riskType.Text = riskTypeTemp;
            lb_riskDate.Text = insuranceReportInfo.RiskDate.ToString("yyyy-M-d");
            lb_reportDate.Text = insuranceReportInfo.ReportDate.ToString("yyyy-M-d");
            lb_riskContent.Text = insuranceReportInfo.RiskContent;

            #region 附件绑定
            if (insuranceReportInfo.RiskAttachment != null)
            {
                string[] editreason1 = insuranceReportInfo.RiskAttachment.Split(split, StringSplitOptions.None);
                List<CurrentStatusFile> arList1 = new List<CurrentStatusFile>();

                int editreason1id = 1;
                for (int i = 0; i < editreason1.Length; i++)
                {
                    string[] bb = { "#" };
                    string[] arsplitsplit = editreason1[i].Split(bb, StringSplitOptions.None);
                    if (arsplitsplit.Length == 2)
                    {
                        CurrentStatusFile ar = new CurrentStatusFile(editreason1id, arsplitsplit[0], arsplitsplit[1]);
                        arList1.Add(ar);
                        editreason1id += 1;
                    }
                }
                gridviewFile.DataSource = arList1;
                gridviewFile.DataBind();
            }
            #endregion

            lb_ReceiptNo.Text = insuranceReportInfo.ReceiptNo;
            lb_Estimate.Text = insuranceReportInfo.Estimate;
            lb_Claim.Text = insuranceReportInfo.Claim;
            lb_Address.Text = insuranceReportInfo.Address;

        }
        else if (cmd == "review")//复核
        {
            IsToView1(true);
            IsToView2(true);
            IsToView3(false);
            lb_insuranceNo.Text = insuranceReportInfo.InsuranceNo;
            lb_reportNo.Text = insuranceReportInfo.ReportNo;
            string riskTypeTemp;
            if (insuranceReportInfo.RiskType == RiskType.Other)
            {
                riskTypeTemp = "其他(" + insuranceReportInfo.RiskTypeName + ")";
            }
            else
            {
                riskTypeTemp = insuranceReportInfo.RiskTypeName;
            }

            lb_riskType.Text = riskTypeTemp;
            lb_riskDate.Text = insuranceReportInfo.RiskDate.ToString("yyyy-M-d");
            lb_reportDate.Text = insuranceReportInfo.ReportDate.ToString("yyyy-M-d");
            lb_riskContent.Text = insuranceReportInfo.RiskContent;


            lb_repairContent.Text = insuranceReportInfo.RepairContent;

            #region 附件绑定

            if (insuranceReportInfo.RiskAttachment != null)
            {
                string[] editreason1 = insuranceReportInfo.RiskAttachment.Split(split, StringSplitOptions.None);
                List<CurrentStatusFile> arList1 = new List<CurrentStatusFile>();

                int editreason1id = 1;
                for (int i = 0; i < editreason1.Length; i++)
                {
                    string[] bb = { "#" };
                    string[] arsplitsplit = editreason1[i].Split(bb, StringSplitOptions.None);
                    if (arsplitsplit.Length == 2)
                    {
                        CurrentStatusFile ar = new CurrentStatusFile(editreason1id, arsplitsplit[0], arsplitsplit[1]);
                        arList1.Add(ar);
                        editreason1id += 1;
                    }
                }
                gridviewFile.DataSource = arList1;
                gridviewFile.DataBind();
            }
            #endregion

            #region 附件绑定

            if (insuranceReportInfo.RepairAttachment != null)
            {
                string[] editreason2 = insuranceReportInfo.RepairAttachment.Split(split, StringSplitOptions.None);
                List<CurrentStatusFile> arList2 = new List<CurrentStatusFile>();

                int editreason2id = 1;
                for (int i = 0; i < editreason2.Length; i++)
                {
                    string[] bb = { "#" };
                    string[] arsplitsplit = editreason2[i].Split(bb, StringSplitOptions.None);
                    if (arsplitsplit.Length == 2)
                    {
                        CurrentStatusFile ar = new CurrentStatusFile(editreason2id, arsplitsplit[0], arsplitsplit[1]);
                        arList2.Add(ar);
                        editreason2id += 1;
                    }
                }
                gridview_repairAttachment.DataSource = arList2;
                gridview_repairAttachment.DataBind();
            }
            #endregion

            lb_ReceiptNo.Text = insuranceReportInfo.ReceiptNo;
            lb_Estimate.Text = insuranceReportInfo.Estimate;
            lb_Claim.Text = insuranceReportInfo.Claim;
            lb_Address.Text = insuranceReportInfo.Address;
        }
    }
    private void ButtonBind()
    {
        if (cmd == "edit")
        {
            //添加新增与删除按钮
            //string moduleName = Label2.Text.Trim();
            HeadMenuButtonItem itemAdd = new HeadMenuButtonItem();
            itemAdd.ButtonName = "新增报险";
            itemAdd.ButtonIcon = "new.gif";
            itemAdd.ButtonPopedom = PopedomType.New;
            itemAdd.ButtonUrl = "?cmd=add&id=" + id;
            HeadMenuWebControls1.ButtonList.Add(itemAdd);
        }
        else if (cmd == "view")
        {
            //添加新增与修改按钮
            //view 有两种情况，一是查看最上层的模块，二是查看第二层以下的模块
            //对于查看最上层模块的情况，传进来的参数id为本模块id，parentName为"无"
            //string moduleName = Label2.Text.Trim();
            HeadMenuButtonItem itemAdd = new HeadMenuButtonItem();
            itemAdd.ButtonName = "新增报险";
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

            HeadMenuButtonItem itemDel = new HeadMenuButtonItem();
            itemDel.ButtonName = "删除";
            itemDel.ButtonIcon = "delete.gif";
            itemDel.ButtonPopedom = PopedomType.Delete;
            itemDel.ButtonUrl = string.Format("DelData('?cmd=delete&id={0}')", id);
            itemDel.ButtonUrlType = UrlType.JavaScript;
            HeadMenuWebControls1.ButtonList.Add(itemDel);
        }
        else if (cmd == "delete")
        {
            try
            {
                insuranceReportBll.DelInsuranceReport(id.ToString());
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除保险失败", ex, Icon_Type.Error, true,
                    "window.history.go(-1)", UrlType.JavaScript, "");
            }

            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除保险(ID:" + id + ")成功！", Icon_Type.OK, true,
                Common.GetHomeBaseUrl("InsuranceReportlist.aspx"), UrlType.Href, "");
        }
    }
    protected void GridView1_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "del1")
        {
            int itemid = int.Parse(e.CommandArgument.ToString());
            try
            {
                InsuranceReportInfo project = ViewState["InsuranceReportInfo"] as InsuranceReportInfo;
                InsuranceReport insuranceReportBll = new InsuranceReport();
                string separatorStr = "@First@";
                string[] split = { separatorStr };
                string[] editreason = project.RiskAttachment.Split(split, StringSplitOptions.None);
                string files = "";
                for (int i = 0; i < editreason.Length; i++)
                {
                    if (i != (itemid - 1))
                    {
                        if (files != "")
                        {
                            files += "@First@" + editreason[i].ToString();
                        }
                        else
                        {
                            files += editreason[i].ToString();
                        }
                    }
                    else
                    {
                        string[] bb = { "#" };
                        string[] arsplitsplit = editreason[i].Split(bb, StringSplitOptions.None);
                        if (arsplitsplit.Length == 2)
                        {
                            string delFile = arsplitsplit[1];
                            File.Delete(Server.MapPath(UPLOADFOLDER + delFile));

                        }
                    }
                }
                project.RiskAttachment = files;

                insuranceReportBll.UpdateInsuranceReport(project);

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "删除失败", "删除失败，请重试", ex, Icon_Type.Error, true, "history.go(-1);", UrlType.JavaScript, "");

                return;
            }

            FillData();
        }
    }
    protected void GridView2_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "del2")
        {
            int itemid = int.Parse(e.CommandArgument.ToString());
            try
            {
                InsuranceReportInfo project = ViewState["InsuranceReportInfo"] as InsuranceReportInfo;
                InsuranceReport insuranceReportBll = new InsuranceReport();
                string separatorStr = "@First@";
                string[] split = { separatorStr };
                string[] editreason = project.RepairAttachment.Split(split, StringSplitOptions.None);
                string files = "";
                for (int i = 0; i < editreason.Length; i++)
                {
                    if (i != (itemid - 1))
                    {
                        if (files != "")
                        {
                            files += "@First@" + editreason[i].ToString();
                        }
                        else
                        {
                            files += editreason[i].ToString();
                        }
                    }
                    else
                    {
                        string[] bb = { "#" };
                        string[] arsplitsplit = editreason[i].Split(bb, StringSplitOptions.None);
                        if (arsplitsplit.Length == 2)
                        {
                            string delFile = arsplitsplit[1];
                            File.Delete(Server.MapPath(UPLOADFOLDER + delFile));

                        }
                    }
                }
                project.RepairAttachment = files;

                insuranceReportBll.RepairRegister(project);

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "删除失败", "删除失败，请重试", ex, Icon_Type.Error, true, "history.go(-1);", UrlType.JavaScript, "");

                return;
            }

            FillData();
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
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
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
    /// 添加/修改报险
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {     
        InsuranceReportInfo insuranceReportInfo = new InsuranceReportInfo();
        insuranceReportInfo = getAddInsuranceReportInfo();
        if (cmd == "add")
        {
            try
            {
                insuranceReportInfo.State = State.New;
                insuranceReportInfo.Operator = UserData.CurrentUserData.UserName;
                long thisID = insuranceReportBll.InsertInsuranceReport(insuranceReportInfo);

                //string title = "报险跟踪表" + insuranceReportInfo.ReportNo + "待修复";
                //string URL = "../InsureManager/InsureReport/InsuranceReportlist";
                // User userBll = new User();
                // int tempRoleID = Convert.ToInt32(ConfigurationManager.AppSettings["xfsp"]);
                // IList roleUsers = userBll.GetUsers(tempRoleID);
                // for (int k = 0; k < roleUsers.Count; k++)
                // {
                //     string[] receiver = { ((UserRoleInfo)roleUsers[k]).UserName };
                //     WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, receiver);
                // }

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加保险信息失败", ex, Icon_Type.Error, true,
                    "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加报险信息成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("InsuranceReportlist.aspx"),
                UrlType.Href, "");

        }
        if (cmd == "edit")
        {
            try
            {  
                insuranceReportInfo.Id = id;
                insuranceReportBll.UpdateInsuranceReport(insuranceReportInfo);            
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改保单信息失败", ex, Icon_Type.Error, true,
                    "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改保单信息成功！", Icon_Type.OK, true,
                Common.GetHomeBaseUrl("InsuranceReportlist.aspx"), UrlType.Href, "");
        }
        if (cmd == "repair")
        {
            try
            {
                insuranceReportInfo.Id =id;
                insuranceReportInfo.State = State.Repaired;
                insuranceReportInfo.StationManager = UserData.CurrentUserData.UserName;
                insuranceReportBll.RepairRegister(insuranceReportInfo);

                //string URL = "../DeviceManager/DeviceInfo/ExpendableInfo/InWarehouseApply.aspx";
                //string title = "您有新的报险单" + item.SheetName + "待复核";

                // User userBll = new User();
                // int tempRoleID = Convert.ToInt32(ConfigurationManager.AppSettings["fhsp"]);
                // IList roleUsers = userBll.GetUsers(tempRoleID);
                // for (int k = 0; k < roleUsers.Count; k++)
                // {
                //     string[] receiver = { ((UserRoleInfo)roleUsers[k]).UserName };
                //     WorkflowApplication.SendingPendingOrderToUsers(title, Common.Get_UserName, UserData.CurrentUserData.PersonName, URL, 0, null, receiver);
                // }
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改保单信息失败", ex, Icon_Type.Error, true,
                    "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改保单信息成功！", Icon_Type.OK, true,
                Common.GetHomeBaseUrl("InsuranceReportList.aspx"), UrlType.Href, "");

        }
        if (cmd == "review")
        {
            try
            {
                insuranceReportInfo.Id = id; 
                insuranceReportInfo.State = State.Reviewed;
                insuranceReportInfo.InsuranceManager = UserData.CurrentUserData.UserName;
                insuranceReportBll.ReviewRegister(insuranceReportInfo);

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改保单信息失败", ex, Icon_Type.Error, true,
                    "window.history.go(-1)", UrlType.JavaScript, "");
            }

            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改保单信息成功！", Icon_Type.OK, true,
                Common.GetHomeBaseUrl("InsuranceReportList.aspx"), UrlType.Href, "");
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

    private InsuranceReportInfo getAddInsuranceReportInfo()
    { 
        InsuranceReportInfo insuranceReportInfo = new InsuranceReportInfo();     
        if (id != 0)
        {
            insuranceReportInfo = insuranceReportBll.GetInsuranceReportInfo(id);
        }
         
        insuranceReportInfo.InsuranceNo = tb_insuranceNo.Text.Trim();
        insuranceReportInfo.ReportNo = tb_reportNo.Text.Trim();
        insuranceReportInfo.RiskType = (RiskType)Convert.ToInt32(rb_riskType.SelectedValue);

        if (insuranceReportInfo.RiskType == RiskType.Other)
        {
            insuranceReportInfo.RiskTypeName = tb_riskTypeName.Value.Trim();
        }
        else
        {
            insuranceReportInfo.RiskTypeName = EnumHelper.GetDescription(insuranceReportInfo.RiskType);
        }
        insuranceReportInfo.RiskDate = Convert.ToDateTime(tb_riskDate.Text.Trim());
        insuranceReportInfo.ReportDate = Convert.ToDateTime(tb_reportDate.Text.Trim());
        insuranceReportInfo.RiskContent = tb_riskContent.Text.Trim();

        insuranceReportInfo.ReceiptNo = tb_ReceiptNo.Text.Trim();
        insuranceReportInfo.Estimate = tb_Estimate.Text.Trim();
        insuranceReportInfo.Claim = tb_Claim.Text.Trim();
        insuranceReportInfo.Address = tb_Address.Text.Trim();
       
        //附件处理
        string filepath = Server.MapPath("./") + "UploadFiles";
      
        HttpFileCollection uploadedFiles = Request.Files;
        for (int i = 0; i < uploadedFiles.Count; i++)
        {  
            HttpPostedFile userPostedFile = uploadedFiles[i];
            if (userPostedFile.ContentLength > 0)
            {
                string fileName = "";//定义文件名
                fileName = Path.GetFileName(userPostedFile.FileName);
                int index = fileName.IndexOf(".");
                string FileType = fileName.Substring(index).ToLower();//截取文件后缀名
                //FileTypeImg = "../FileTypeimg/" + hz + ".gif";
                Guid fileGuid = Guid.NewGuid();//生成新的文件名称 以GUID命名防止文件名相同
                string NewFileName = fileGuid.ToString();//新的文件名
                NewFileName = NewFileName + FileType;//新的文件名+后缀名
                if (userPostedFile.ContentLength > 2097151 * 1024)//判断是否大于配置文件中的上传文件大小
                {
                    Page.RegisterStartupScript("提示", "<script language='javascript'>alert('对不起您的上传资源过大!');return;</script>");
                    return null;
                }
                else
                {
                    if (fileName != "")//如果文件名不为空
                    {
                        try
                        {
                            //文件虚拟路径
                            string strpath = Server.MapPath(UPLOADFOLDER + NewFileName);
                            if (!Directory.Exists(strpath))//判断目录是否存在
                            {
                                Directory.CreateDirectory(Server.MapPath(UPLOADFOLDER));
                            }
                            userPostedFile.SaveAs(strpath);

                            string[] bb = { "$" };
                            string[] upfilelist = uploadedFiles.AllKeys[i].Split(bb, StringSplitOptions.None);

                            //正则表达式   
                            string strExp = @"^file";
                            //创建正则表达式对象   
                            Regex myRegex = new Regex(strExp);

                            if (myRegex.IsMatch(upfilelist[2].ToString()))
                            {
                                if (string.IsNullOrEmpty(insuranceReportInfo.RiskAttachment))
                                {
                                    insuranceReportInfo.RiskAttachment += fileName + "#" + NewFileName;
                                }
                                else
                                    insuranceReportInfo.RiskAttachment += "@First@" + fileName + "#" + NewFileName;
                            }
                            else 
                            {
                                if (string.IsNullOrEmpty(insuranceReportInfo.RepairAttachment))
                                {
                                    insuranceReportInfo.RepairAttachment += fileName + "#" + NewFileName;
                                }
                                else
                                    insuranceReportInfo.RepairAttachment += "@First@" + fileName + "#" + NewFileName;
                            }                           

                            Response.Write("上传成功!");

                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.ToString());
                        }
                    }
                    else
                    {
                        Response.Write("上传文件不能为空!");
                        return null;
                    }
                }
            }
        }

        insuranceReportInfo.ReceiptNo = tb_ReceiptNo.Text;
         insuranceReportInfo.Estimate = tb_Estimate.Text;
         insuranceReportInfo.Claim = tb_Claim.Text;
         insuranceReportInfo.Address = tb_Address.Text;
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         insuranceReportInfo.RepairContent = tb_repairContent.Text.Trim();
                 
         insuranceReportInfo.ReviewContent = tb_reviewContent.Text.Trim();
         InsuranceReportInfo project = ViewState["InsuranceReportInfo"] as InsuranceReportInfo;
         if (project != null)
         {
             insuranceReportInfo.Operator = project.Operator == null ? "" : project.Operator;
             insuranceReportInfo.StationManager = project.StationManager == null ? "" : project.StationManager;
             insuranceReportInfo.InsuranceManager = project.InsuranceManager == null ? "" : project.InsuranceManager;            
        }
        return insuranceReportInfo;

    }
}
