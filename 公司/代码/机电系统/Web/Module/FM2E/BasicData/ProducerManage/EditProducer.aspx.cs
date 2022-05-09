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

using FM2E.BLL.Basic;
using FM2E.Model.Basic;

public partial class Module_FM2E_BasicData_ProducerManage_EditProducer : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private long id = (long)Common.sink("id", MethodType.Get, 50, 0, DataType.Long);

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
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：生产商信息添加";
            TabPanel1.HeaderText = "添加生产商";
//            TabOptionWebControls1.TaboptionItems[0].Tab_Name = "";
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：生产商信息修改";
            TabPanel1.HeaderText = "修改生产商信息";
//            TabOptionWebControls1.TaboptionItems[0].Tab_Name = "";
        }
    }
    private void FillData()
    {
        if (cmd == "edit")
        {
            try
            {
                ProducerInfo item;
                if (Session["ProducerInfo"] == null)
                {
                    item = (ProducerInfo)Session["ProducerInfo"];
                }
                else
                {
                    Producer bll = new Producer();
                    item = bll.GetProducer(id);
                }

                TextBox1.Text = item.Name;
                if(item.Credit!=0)
                LikeRating.CurrentRating = item.Credit;
                TextBox2.Text = item.Address;
                TextBox3.Text = item.Phone;
                TextBox4.Text = item.Fax;
                TextBox5.Text = item.Email;
                TextBox6.Text = item.HomePage;
                TextBox7.Text = item.ResName;
                TextBox8.Text = item.ResPhone;
                TextBox9.Text = item.Product;
                TextBox10.Text = item.Remark;

            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败","获取数据失败", ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bool bSuccess = false;
        if (cmd == "add" || cmd == "edit")
        {
            ProducerInfo item = new ProducerInfo();
            item.Name = TextBox1.Text.Trim();
            item.Address = TextBox2.Text.Trim();
            item.Phone = TextBox3.Text.Trim();
            item.Fax = TextBox4.Text.Trim();
            item.Email = TextBox5.Text.Trim();
            item.HomePage = TextBox6.Text.Trim();
            item.ResName = TextBox7.Text.Trim();
            item.ResPhone = TextBox8.Text.Trim();
            item.Product = TextBox9.Text.Trim();
            item.Remark = TextBox10.Text.Trim();
            item.IsDeleted = false;
            item.Credit = LikeRating.CurrentRating;

            if (cmd == "add")
            {
                try
                {
                    Producer bll = new Producer();
                    bll.InsertProducer(item);
                    bSuccess = true;
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败","添加生产商失败", ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
                }

                if (bSuccess)
                {
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加生产商成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Producer.aspx"), UrlType.Href, "");
                }
                //   EventMessage.MessageBox(1, "操作成功", "添加供应商成功！", Icon_Type.OK, false, Common.GetHomeBaseUrl("Provider.aspx"), UrlType.Href, "");
            }
            else if (cmd == "edit")
            {
                try
                {
                    item.ProducerID = id;
                    Producer bll = new Producer();
                    bll.UpdateProducer(item);
                    bSuccess = true;
                }
                catch (Exception ex)
                {
                    EventMessage.MessageBox(Msg_Type.Error, "操作失败","修改生产商信息失败", ex, Icon_Type.Error, true , "window.history.go(-1)", UrlType.JavaScript, "");
                }

                if (bSuccess)
                {
                    EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改生产商成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("Producer.aspx"), UrlType.Href, "");
                }
            }
        }
    }
    protected void LikeRating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        e.CallbackResult = "Upate done. Value = " + e.Value + " Tag = " + e.Tag;
    }
}
