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
using AjaxControlToolkit;
using WebUtility;
using WebUtility.Components;
using WebUtility.WebControls;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.Exceptions;
using System.Collections.Generic;

public partial class Module_FM2E_BasicData_ChannalManage_EditChannal : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private string id = (string)Common.sink("id", MethodType.Get, 50, 0, DataType.Str);

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);

        if (!IsPostBack)
        {
            ButtonBind();
            FillData();
        }
    }

    private void ButtonBind()
    {
        if (cmd == "add")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：隧道信息添加";
            TabPanel1.HeaderText = "添加隧道";
            //TabOptionWebControls1.TaboptionItems[0].Tab_Name = "添加隧道";
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：隧道信息修改";
            TabPanel1.HeaderText = "修改隧道信息";
            //TabOptionWebControls1.TaboptionItems[0].Tab_Name = "修改隧道信息";
        }
    }

    private void FillData()
    {
        if (cmd == "edit")
        {
            try
            {
                ChannalInfo item;
                Channal bll = new Channal();
                item = bll.GetChannal(id);

                TextBox1.Text = item.ChannalID;
                TextBox2.Text = item.ChannalName;

                CascadingDropDown1.SelectedValue = item.CompanyID;
                CascadingDropDown2.SelectedValue = item.SectionID;
                TextBox4.Text = item.Length.ToString();
                TextBox5.Text = item.OpenTime.ToShortDateString();

                TextArea1.Value = item.Remark;

                if (item.PictureUrl == "")
                    ImageButton1.Visible = false;
                ViewState["PictureUrl"] = item.PictureUrl;
                FileUpload1.Visible = true;
                if (item.PictureUrl != null && item.PictureUrl != "")
                {
                    if (System.IO.File.Exists(Server.MapPath(item.PictureUrl)))
                    {
                        ImageButton1.ImageUrl = item.PictureUrl;
                        Image1.ImageUrl = item.PictureUrl;
                    }

                    else
                    {
                        ImageButton1.ImageUrl = "~/images/deletedpicture.gif";
                        Image1.ImageUrl = "~/images/deletedpicture.gif";
                    }
                }
                else
                {
                    ImageButton1.ImageUrl = "~/images/nopicture.gif";
                    Image1.ImageUrl = "~/images/nopicture.gif";
                }
               
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取数据失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        else if (cmd == "add")
        {

            if (UserData.CurrentUserData.CompanyID != null && UserData.CurrentUserData.CompanyID != string.Empty)
            {
                CascadingDropDown1.SelectedValue = UserData.CurrentUserData.CompanyID;
            }
            //ImageButton1.Visible = false;
            ViewState["PictureUrl"] = "";
            ImageButton1.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;
        if (cmd == "add" || cmd == "edit")
        {
            ChannalInfo item = new ChannalInfo();
            item.ChannalID = TextBox1.Text.Trim();
            item.ChannalName = TextBox2.Text.Trim();
            item.CompanyID = DropDownList1.SelectedValue;
            item.SectionID = DropDownList2.SelectedValue;
            if (TextBox4.Text.Trim() != "")
            {
                item.Length = Convert.ToDecimal(TextBox4.Text.Trim());
            }
            else
                item.Length = 0;
            if (TextBox5.Text.Trim() != "")
                item.OpenTime = Convert.ToDateTime(TextBox5.Text.Trim());
            else
                item.OpenTime = DateTime.Now;
            item.Remark = TextArea1.Value.Trim();
            item.IsDeleted = false;

            item.PictureUrl = (string)ViewState["PictureUrl"];

            string errorMsg = "";
            bool isSuccess = false;

            //对图片是否上传更新的选择处理
            if (FileUpload1.HasFile)
            {
                string photoUrl = UploadPhoto(ref isSuccess, ref errorMsg);
                if (cmd == "add")
                {
                    if (photoUrl != "")
                    {
                        item.PictureUrl = SystemConfig.Instance.UploadPath + "ChannalPic/" + photoUrl;
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
                        item.PictureUrl = SystemConfig.Instance.UploadPath + "ChannalPic/" + photoUrl;
                    }
                }
            }
            else
            {
                isSuccess = true;
                item.PictureUrl = (string)ViewState["PictureUrl"];
            }
            if (!isSuccess)
            {
                EventMessage.MessageBox(Msg_Type.Error, "上传图片失败", errorMsg, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }

            else if (cmd == "add")
            {
                try
                {
                    bSuccess = false;
                    Channal Channal = new Channal();
                    Channal.InsertChannal(item);
                    bSuccess = true;
                }
                catch (DuplicateException ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加隧道失败：" + ex.Message, ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "添加隧道失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                }
                if (bSuccess == true)
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加隧道成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Channal.aspx"), UrlType.Href, "");
            }
            else if (cmd == "edit")
            {
                try
                {
                    Channal Channal = new Channal();
                    Channal.UpdateChannal(item);
                    bSuccess = true;
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败", "修改隧道信息失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
                }
                if (bSuccess == true)
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改隧道信息成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Channal.aspx"), UrlType.Href, "");
            }
        }
    }
    private string UploadPhoto(ref bool isSuccess, ref string errorMsg)
    {
        FileUpLoadCommon fc = new FileUpLoadCommon(SystemConfig.Instance.UploadPath + "ChannalPic/", false);
        isSuccess = fc.SaveFile(FileUpload1.PostedFile, true, false);
        if (!isSuccess)
            errorMsg = fc.ErrorMsg;
        return fc.NewFileName;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

    }
}
