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
using FM2E.Model.Basic;
using FM2E.BLL.Basic;
using WebUtility.Components;
using WebUtility;
using FM2E.Model.Exceptions;
using System.Collections.Generic;

public partial class Module_FM2E_BasicData_CompanyManage_EditCompany : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private string id = (string)Common.sink("id", MethodType.Get, 50, 0, DataType.Str);

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);

        if (!IsPostBack)
        {
            FillData();
            ButtonBind();
        }
        if (cmd == "edit")

            HeadMenuWebControls1.ButtonList[0].ButtonUrl = "ViewCompany.aspx?cmd=view&id=" + id;
        else
            HeadMenuWebControls1.ButtonList[0].ButtonUrl = "Company.aspx";
    }

    private void FillData()
    {
        if (cmd == "add")
        {
            string today = DateTime.Now.ToShortDateString();
            ImageButton1.Visible = false;
            ImageButton2.Visible = false;
            shoebig.Visible = false;
            //Session["NeedUpdatePhoto"] = true;
            //TextBox4.Text = today;
            //TextBox9.Text = today;
            //TextBox10.Text = today;
        }
        if (cmd == "edit")
        {
            try
            {
                TextBox1.Enabled = false;
                CompanyInfo item;
                if (Session["CompanyInfo" + id] != null)
                {
                    item = (CompanyInfo)Session["CompanyInfo" + id];
                }
                else
                {
                    Company bll = new Company();
                    item = bll.GetCompany(id);
                }

                TextBox1.Text = item.CompanyID;
                ViewState["CompanyName"] = item.CompanyName;
                TextBox2.Text = item.CompanyName;
                TextBox3.Text = item.Address;
                TextBox4.Text = item.Contact;
                TextBox5.Text = item.Phone;
                TextBox6.Text = item.Website;
                TextBox7.Text = item.Email;
                TextBox8.Text = item.Fax;
                if ((item.Remark != "") && (item.Remark != null))
                    TextBox9.Text = item.Remark;
                if (item.IsParentCompany == true)
                    DropDownList3.SelectedValue = "true";
                else
                    DropDownList3.SelectedValue = "false";


                ViewState["PictureUrl"] = item.PictureUrl;
                FileUpload1.Visible = false;
                if (item.PictureUrl != "" && item.PictureUrl != null)
                {
                    ImageButton1.ImageUrl = item.PictureUrl;
                    ImageButton2.ImageUrl = item.PictureUrl;
                }
                else
                {
                    ImageButton1.ImageUrl = "~/images/nopicture.gif";
                    ImageButton2.ImageUrl = "~/images/nopicture.gif";
                }
                Session["NeedUpdatePhoto"] = false;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }

    private void ButtonBind()
    {
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：公司信息添加";
            //TabPanel1.HeaderText = "添加公司信息";
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：公司信息修改";
            //TabPanel1.HeaderText = "修改公司信息";
        }
    }
    /// <summary>
    /// 添加或修改的确定按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;

        if (cmd == "add" || cmd == "edit")
        {
            CompanyInfo item = new CompanyInfo();
            string errorString = "";
            try
            {
                item.CompanyID = Common.inSQL(TextBox1.Text.Trim());

                item.CompanyName = Common.inSQL(TextBox2.Text.Trim());

                item.Address = Common.inSQL(TextBox3.Text.Trim());

                item.Contact = Common.inSQL(TextBox4.Text.Trim());

                item.Phone = Common.inSQL(TextBox5.Text.Trim());

                item.Website = Common.inSQL(TextBox6.Text.Trim());

                if (TextBox7.Text.Trim()=="")
                {
                    item.Email = "";
                }
                if (Common.IsValidEmail(TextBox7.Text.Trim()))
                    item.Email = Common.inSQL(TextBox7.Text.Trim());
                else
                    errorString = "电子邮件格式错误！";

                item.Fax = Common.inSQL(TextBox8.Text.Trim());

                item.Remark = Common.inSQL(TextBox9.Text.Trim());
                item.IsParentCompany = Convert.ToBoolean(DropDownList3.SelectedValue);
                item.IsDeleted = false;
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入数据的格式不正确"+errorString, ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");

            }

            //item.PictureUrl = (string)ViewState["PictureUrl"];

            string errorMsg = "";
            bool isSuccess = false;

            //对图片是否上传更新的选择处理
            if ((FileUpload1.FileName != null) && (FileUpload1.FileName != ""))
            {
                string photoUrl = UploadPhoto(ref isSuccess, ref errorMsg);
                if (cmd == "add")
                {
                    if (photoUrl != "")
                    {
                        item.PictureUrl = SystemConfig.Instance.UploadPath + "CompanyPic/" + photoUrl;
                    }
                    else
                    {
                        item.PictureUrl = photoUrl;
                    }
                }
                else if (cmd == "edit")
                {
                    if (photoUrl != "")
                    {
                        FileUpLoadCommon.DeleteFile(string.Format("{0}", item.PictureUrl));
                        item.PictureUrl = SystemConfig.Instance.UploadPath + "CompanyPic/" + photoUrl;
                    }
                    else
                        item.PictureUrl = photoUrl;
                }
            }
            else
            {
                if (ViewState["PictureUrl"] != null && ViewState["PictureUrl"].ToString() != string.Empty)
                    item.PictureUrl = ViewState["PictureUrl"].ToString();
                else item.PictureUrl = "";
                isSuccess = true;
            }


            if (!isSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "上传图片失败", new WebException(errorMsg), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
            }

            Company bll = new Company();
            if (cmd == "add")
            {
                CompanyInfo companyinfo = bll.GetCompany(item.CompanyID);
                if (companyinfo != null)
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "重复插入编号相同的公司", new WebException("重复插入编号相同的公司"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                else
                {
                    CompanyInfo companyinfo2 = new CompanyInfo();
                    companyinfo2.CompanyName = item.CompanyName;
                    List<CompanyInfo> list = bll.Search(companyinfo2);
                    if (list.Count != 0)
                        EventMessage.MessageBox(Msg_Type.Error, "操作失败", "重复插入名称相同的公司", new WebException("重复插入名称相同的公司"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                }
            }
            else if (cmd == "edit")
            {
                CompanyInfo companyinfo = bll.GetCompany(item.CompanyID);
                if (companyinfo != null&&companyinfo.CompanyID!=id)
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "已存在编号相同的公司", new WebException("已存在编号相同的公司"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                else
                {
                    bool overwrite = false;
                    CompanyInfo companyinfo2 = new CompanyInfo();
                    companyinfo2.CompanyName = item.CompanyName;
                    List<CompanyInfo> list = bll.Search(companyinfo2);
                    if (list.Count != 0 && list[0].CompanyName != ViewState["CompanyName"].ToString())
                        overwrite = true;
                    if (list.Count > 1 && list[0].CompanyName != ViewState["CompanyName"].ToString())
                        overwrite = true;
                    if(overwrite)
                        EventMessage.MessageBox(Msg_Type.Error, "操作失败", "已存在名称相同的公司", new WebException("已存在名称相同的公司"), Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                }
            }



            if (cmd == "add")
            {
                try
                {
                    bll.InsertCompany(item);
                    bSuccess = true;
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加公司失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                }
                if (bSuccess)
                {
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加公司成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Company.aspx"), UrlType.Href, "");
                }
            }
            else if (cmd == "edit")
            {
                try
                {
                    bll.UpdateCompany(item);
                    bSuccess = true;
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改公司信息失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
                }

                if (bSuccess)
                {
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改公司信息成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Company.aspx"), UrlType.Href, "");
                }
            }
        }
    }

    private string UploadPhoto(ref bool isSuccess, ref string errorMsg)
    {
        FileUpLoadCommon fc = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + "CompanyPic/", false);
        isSuccess = fc.SaveFile(FileUpload1.PostedFile, true, false);
        if (!isSuccess)
            errorMsg = fc.ErrorMsg;
        return fc.NewFileName;
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {

        ImageButton1.Visible = false;
        FileUpload1.Visible = true;
        ButtonCancel.Visible = true;
        shoebig.Visible = false;
        //Session["NeedUpdatePhoto"] = true;

    }

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {

        ImageButton1.Visible = true;
        FileUpload1.Visible = false;
        ButtonCancel.Visible = false;
        shoebig.Visible = true;
        //Session["NeedUpdatePhoto"] = false;
    }
}
