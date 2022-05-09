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
using FM2E.BLL.Contract;
using FM2E.Model.Contract;
using WebUtility.WebControls;
using WebUtility.Components;
using FM2E.Model.Exceptions;

using FM2E.BLL.System;
using System.Collections.Generic;
using System.IO;
using FM2E.Model.Insurance;

public partial class Module_FM2E_Contract_ContractInformation_EditContractInformation : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private int id = 0;
    private const string UPLOADFOLDER = "~/public/contract/";

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
            FillInterimPaymentData();
        }
        Session["Contract"] = id;
    }

    private void FillData()
    {
        ContractInformation contractInformationBll = new ContractInformation();
        ContractInformationInfo contractInformationInfo = contractInformationBll.GetContractInformationInfo(id);
        Session["contract"] = contractInformationInfo;
        if (cmd == "add")
        {
            isView(false);
        }
        else if (cmd == "view")
        {  
            
            #region 附件绑定

            string separatorStr = "@First@";
            string[] split = { separatorStr };
            if (contractInformationInfo.Attachment != null)
            {
                if (!contractInformationInfo.Attachment.Contains(separatorStr))
                {
                    contractInformationInfo.Attachment += " " + separatorStr + " ";  //附件名称+附件地址
                }
            }
            else
            {
                contractInformationInfo.Attachment += " " + separatorStr + " ";  //附件名称+附件地址
            }
            string[] editreason1 = contractInformationInfo.Attachment.Split(split, StringSplitOptions.None);
            List<CurrentStatusFile> arList = new List<CurrentStatusFile>();

            int editreason1id = 1;
            for (int i = 0; i < editreason1.Length; i++)
            {
                string[] bb = { "#" };
                string[] arsplitsplit = editreason1[i].Split(bb, StringSplitOptions.None);
                if (arsplitsplit.Length == 2)
                {
                    CurrentStatusFile ar = new CurrentStatusFile(editreason1id, arsplitsplit[0], arsplitsplit[1]);
                    arList.Add(ar);
                    editreason1id += 1;
                }
            }
            gridviewFile.DataSource = arList;
            gridviewFile.DataBind();

            #endregion



            isView(true);
            //修改和查看时，需要先查询出模块的信息
            //修改和查看时，传进来的参数id为本模块id
            if (contractInformationInfo != null)
            {
                lb_ContractNo.Text = contractInformationInfo.ContractNo;
                lb_ContractName.Text = contractInformationInfo.ContractName;
                lb_ContractAmount.Text = contractInformationInfo.ContractAmount.ToString();              
                lb_ContractedUnits.Text = contractInformationInfo.ContractedUnits;
                lb_Period.Text = contractInformationInfo.Period.ToString();
                lb_Retentions.Text = contractInformationInfo.Retentions.ToString();
                lb_ContractPeople.Text = contractInformationInfo.ContractPeople;
                lb_ContractTheWay.Text = contractInformationInfo.ContractTheWay;
                tb_Prepaid.Text = contractInformationInfo.Prepaid.ToString();
                tb_CompletedPayment.Text = contractInformationInfo.CompletedPayment.ToString();
                tb_HandOverpay.Text = contractInformationInfo.HandOverpay.ToString();
                lb_SettlementAmount.Text = contractInformationInfo.SettlementAmount.ToString();
            }
        }
        else if (cmd == "edit")
        {
            #region 附件绑定

            string separatorStr = "@First@";
            string[] split = { separatorStr };
            if (contractInformationInfo.Attachment != null)
            {
                if (!contractInformationInfo.Attachment.Contains(separatorStr))
                {
                    contractInformationInfo.Attachment += " " + separatorStr + " ";  //附件名称+附件地址
                }
            }
            else
            {
                contractInformationInfo.Attachment += " " + separatorStr + " ";  //附件名称+附件地址
            }
            string[] editreason1 = contractInformationInfo.Attachment.Split(split, StringSplitOptions.None);
            List<CurrentStatusFile> arList = new List<CurrentStatusFile>();

            int editreason1id = 1;
            for (int i = 0; i < editreason1.Length; i++)
            {
                string[] bb = { "#" };
                string[] arsplitsplit = editreason1[i].Split(bb, StringSplitOptions.None);
                if (arsplitsplit.Length == 2)
                {
                    CurrentStatusFile ar = new CurrentStatusFile(editreason1id, arsplitsplit[0], arsplitsplit[1]);
                    arList.Add(ar);
                    editreason1id += 1;
                }
            }
            gridviewFile.DataSource = arList;
            gridviewFile.DataBind();

            #endregion

            isView(false);
            if (contractInformationInfo != null)
            {
                lb_SettlementAmount.Text = contractInformationInfo.SettlementAmount.ToString();
                tb_ContractNo.Text = contractInformationInfo.ContractNo;
                tb_ContractName.Text = contractInformationInfo.ContractName;
                tb_ContractAmount.Text = contractInformationInfo.ContractAmount.ToString();             
                tb_ContractedUnits.Text = contractInformationInfo.ContractedUnits;
                tb_Period.Text = contractInformationInfo.Period.ToString();
                tb_Retentions.Text = contractInformationInfo.Retentions.ToString();
                tb_ContractPeople.Text = contractInformationInfo.ContractPeople;
                tb_ContractTheWay.Text = contractInformationInfo.ContractTheWay;
            }
        }
    }

    private void isView(bool isbool)
    {
        tb_ContractNo.Visible = !isbool;
        tb_ContractName.Visible = !isbool;
        tb_ContractAmount.Visible = !isbool;
        tb_ContractedUnits.Visible = !isbool;
        tb_Period.Visible = !isbool;
        tb_Retentions.Visible = !isbool;
        tb_ContractPeople.Visible = !isbool;
        tb_ContractTheWay.Visible = !isbool;

        lb_ContractNo.Visible = isbool;
        lb_ContractName.Visible = isbool;
        lb_ContractAmount.Visible = isbool;
        lb_ContractedUnits.Visible = isbool;
        lb_Period.Visible = isbool;
        lb_Retentions.Visible = isbool;
        lb_ContractPeople.Visible = isbool;
        lb_ContractTheWay.Visible = isbool;

        PostButton.Visible = !isbool;

        TabPanel2.Visible = isbool;
        FileUpload_div.Visible = !isbool;
    }

    private void ButtonBind()
    {
        if (cmd == "edit")
        {
            //添加新增与删除按钮
            //string moduleName = Label2.Text.Trim();
            HeadMenuButtonItem itemAdd = new HeadMenuButtonItem();
            itemAdd.ButtonName = "添加合同";
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
            itemAdd.ButtonName = "添加合同";
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
            try
            {
                ContractInformation contractInformationBll = new ContractInformation();
                contractInformationBll.DelContractInformation(id.ToString());
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除保单失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除合同基本信息(ID:" + id + ")成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ContractInformation.aspx"), UrlType.Href, "");
        }
    }

    /// <summary>
    /// 添加/修改模块
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        ContractInformationInfo contractInformationInfo = new ContractInformationInfo();
        //获取用户输入
        try
        {
            contractInformationInfo.ContractNo = tb_ContractNo.Text;
            contractInformationInfo.ContractName = tb_ContractName.Text;
            contractInformationInfo.ContractAmount = tb_ContractAmount.Text.Trim() == "" ? 0 : Convert.ToDecimal(tb_ContractAmount.Text.Trim());            
            contractInformationInfo.ContractedUnits = tb_ContractedUnits.Text;
            contractInformationInfo.Period = tb_Period.Text.Trim() == "" ? 0 : Convert.ToInt32(tb_Period.Text.Trim());
            contractInformationInfo.Retentions = tb_Retentions.Text.Trim() == "" ? 0 :Convert.ToDecimal(tb_Retentions.Text.Trim());
            contractInformationInfo.ContractPeople = tb_ContractPeople.Text;
            contractInformationInfo.ContractTheWay = tb_ContractTheWay.Text;            
        }
        catch (WebException ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加合同基本信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)",
                UrlType.JavaScript, "");
        }

        #region 附件处理

        string currentstatus_file = "";

        HttpFileCollection uploadedFiles = Request.Files;
        for (int i = 0; i < uploadedFiles.Count; i++)
        {
            HttpPostedFile userPostedFile = uploadedFiles[i];
            if (userPostedFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(userPostedFile.FileName);//定义文件名
                int index = fileName.IndexOf(".");
                string FileType = fileName.Substring(index).ToLower();//截取文件后缀名
                //FileTypeImg = "../FileTypeimg/" + hz + ".gif";
                Guid fileGuid = Guid.NewGuid();//生成新的文件名称 以GUID命名防止文件名相同
                string NewFileName = fileGuid.ToString();//新的文件名
                NewFileName = NewFileName + FileType;//新的文件名+后缀名
                if (userPostedFile.ContentLength > 2097151 * 1024)//判断是否大于配置文件中的上传文件大小
                {
                    Page.RegisterStartupScript("提示", "<script language='javascript'>alert('对不起您的上传资源过大!');return;</script>");
                    return;
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
                            if (currentstatus_file == "")
                            {
                                currentstatus_file += fileName + "#" + NewFileName;
                            }
                            else
                                currentstatus_file += "@First@" + fileName + "#" + NewFileName;

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
                        return;
                    }
                }
            }
        }        
        #endregion

        ContractInformation contractInformationBll = new ContractInformation();
        if (cmd == "add")
        {
            try
            {
                contractInformationInfo.SettlementAmount = 0;
                contractInformationInfo.Attachment = currentstatus_file;
                contractInformationBll.InsertContractInformation(contractInformationInfo);

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加合同基本信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加合同基本信息成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ContractInformation.aspx"), UrlType.Href, "");
        }
        if (cmd == "edit")
        {
            try
            {
                contractInformationInfo.Id = id;
                if (!string.IsNullOrEmpty(contractInformationInfo.Attachment))
                    contractInformationInfo.Attachment += "@First@" + currentstatus_file;
                else
                    contractInformationInfo.Attachment = currentstatus_file;
                contractInformationBll.UpdateContractInformationInfo(contractInformationInfo);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改保单信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改保单信息成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("ContractInformation.aspx"), UrlType.Href, "");
        }
    }

    /// <summary>
    /// 添加支付情况
    /// </summary>
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            ContractInformation contractInformationBll = new ContractInformation();
            ContractInformationInfo contractInformationInfo = new ContractInformationInfo();
            contractInformationInfo.Id = id;
            contractInformationInfo.Prepaid = Convert.ToDecimal(tb_Prepaid.Text);
            contractInformationInfo.CompletedPayment = Convert.ToDecimal(tb_CompletedPayment.Text);
            contractInformationInfo.HandOverpay = Convert.ToDecimal(tb_CompletedPayment.Text);
            
            contractInformationInfo.SettlementAmount = Convert.ToDecimal(tb_Prepaid.Text) + Convert.ToDecimal(tb_CompletedPayment.Text) + Convert.ToDecimal(tb_CompletedPayment.Text);
            IList list = contractInformationBll.GetInterimPayment(id);
            foreach (ContractInterimPaymentInfo info in list)
            {
                contractInformationInfo.SettlementAmount += info.PaymentAmount;
            }
            
            contractInformationBll.UpdatePrepaid(contractInformationInfo);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "保存失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
        
        EventMessage.MessageBox(Msg_Type.Info, "操作成功", "保存成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("EditContractInformation.aspx?cmd=view&id=" + id), UrlType.Href, "");
        
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string url = "EditInterimPayment.aspx?cmd=add&ContractId="+id;
        Response.Redirect(url);
    }

    private void FillInterimPaymentData()
    {
        try
        {
            ContractInformation contractInformationBll = new ContractInformation();
            IList list = contractInformationBll.GetInterimPayment(id);
            GridView1.DataSource = list;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName == "edit")
        {
            string interimPaymentId = gvRow.Attributes["Id"];
            Response.Redirect("EditInterimPayment.aspx?cmd=edit&id=" + interimPaymentId + "&ContractId=" + id);
        }
        else if (e.CommandName == "del")
        {
            try
            {
                int row = Convert.ToInt32(e.CommandArgument);
                long interimPaymentId = Convert.ToInt64(gvRow.Attributes["Id"]);
                ContractInformation contractInformationBll = new ContractInformation();
                contractInformationBll.DelInterimPayment(interimPaymentId);

                FillInterimPaymentData();
                // GridView1.Rows[row].Visible = false;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
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

            ContractInterimPaymentInfo item = (ContractInterimPaymentInfo)e.Row.DataItem;
            e.Row.Attributes["Id"] = item.Id.ToString();
        }

    }

    protected void GridView_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != null && e.CommandName != "")
        {
            ContractInformation contractInformationBll = new ContractInformation();
            int itemid = int.Parse(e.CommandArgument.ToString());
            try
            {
                ContractInformationInfo project = Session["contract"] as ContractInformationInfo;
                string separatorStr = "@First@";
                string[] split = { separatorStr };
                string[] editreason1 = project.Attachment.Split(split, StringSplitOptions.None);
                string files = "";
                for (int i = 0; i < editreason1.Count(); i++)
                {
                    if (i != (itemid - 1))
                    {
                        if (files != "")
                        {
                            files += "@First@" + editreason1[i].ToString();
                        }
                        else
                        {
                            files += editreason1[i].ToString();
                        }
                    }
                    else
                    {
                        string[] bb = { "#" };
                        string[] arsplitsplit = editreason1[i].Split(bb, StringSplitOptions.None);
                        if (arsplitsplit.Length == 2)
                        {
                            string delFile = arsplitsplit[1];
                            File.Delete(Server.MapPath(UPLOADFOLDER + delFile));

                        }
                    }
                }
                project.Attachment = files;
                contractInformationBll.UpdateContractInformationInfo(project);

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "删除失败", "删除失败，请重试", ex, Icon_Type.Error, true, "history.go(-1);", UrlType.JavaScript, "");

                return;
            }

            FillData();
        }
    }
    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
}
