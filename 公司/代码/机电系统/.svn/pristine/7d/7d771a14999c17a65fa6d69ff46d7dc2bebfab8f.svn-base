using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility;
using FM2E.BLL.Equipment;
using FM2E.BLL.Basic;
using FM2E.BLL.System;
using FM2E.Model.Equipment;
using WebUtility.Components;

public partial class Module_FM2E_DeviceManager_SpareEquipmentManager_WareHouseCheck_FeeBackFill_SignForm : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 10, 0, DataType.Str);
    private string id = (string)Common.sink("id", MethodType.Get, 20, 0, DataType.Str);
    private readonly WareHouseCheck wareHouseCheck = new WareHouseCheck();
    private readonly Company company = new Company();
    private readonly User user = new User();
    private readonly Warehouse warehouse = new Warehouse();
    private WareHouseFormStatus status = WareHouseFormStatus.Unknown;

    protected void Page_Load(object sender, EventArgs e)
    {
        SystemPermission.CheckCommandPermission(cmd);
        if (!IsPostBack)
        {
            FillData();
        }
    }
    /// <summary>
    /// 填充页面数据
    /// </summary>
    private void FillData()
    {
        if (cmd == "edit")
        {
            try
            {
                WareHouseCheckInfo item = wareHouseCheck.GetWareHouseCheck(id);
                if (item == null) return;

                lbCompanyName.Text = company.GetCompany(item.CompanyID).CompanyName;
                lbWareHouse.Text = warehouse.GetWarehouse(item.WareHouseID).Name;
                lbCheckDate.Text = item.CheckDate.ToString("yyyy-MM-dd");
                lbFormNO.Text = item.FormNO;
                cblMaterialType.SelectedValue = ((int)item.MaterialType).ToString();
                cblMaterialType.Enabled = false;
                lbChecker.Text = string.Format("{0}（{1}）", user.GetUser(item.Checker).PersonName, item.Checker);
                lbSpotCheck.Text = item.SpotCheck;
                lbStockCount.Text = item.StockCount;
                cblQuantitySituation.SelectedValue = ((int)item.QuantitySituation).ToString();
                cblQuantitySituation.Enabled = false;
                cblQuality.SelectedValue = ((int)item.QualitySituation).ToString();
                cblQuality.Enabled = false;
                cblRegSituation.SelectedValue = ((int)item.RegSituation).ToString();
                cblRegSituation.Enabled = false;
                lbExceptionSituation.Text = item.ExceptionSituation;

                //表单状态
                status = item.Status;

                //数量情况
                if (!string.IsNullOrEmpty(item.QuantityFeeBack))
                {
                    lbQuantityFeeBack.Text = item.QuantityFeeBack;
                }
                if (!string.IsNullOrEmpty(item.QuantityConfirmer))
                    lbQuantityConfirmer.Text = string.Format("签名：{0}", user.GetUser(item.QuantityConfirmer).PersonName);
                else
                {   
                    lbQuantityFeeBack.Visible = false;
                    tbQuantityFeeBack.Visible = true;
                    lbQuantityConfirmer.Visible=false;
                    divQuantitySign.Visible = true;
                }

                //质量情况
                if (!string.IsNullOrEmpty(item.QualityFeeBack))
                {
                    lbQualityFeeBack.Text = item.QualityFeeBack;
                }
                if (!string.IsNullOrEmpty(item.QualityConfirmer))
                    lbQualityConfirmer.Text = string.Format("签名：{0}", user.GetUser(item.QualityConfirmer).PersonName);
                else
                {   
                    lbQualityFeeBack.Visible = false;
                    tbQualityFeeBack.Visible = true;
                    lbQualityConfirmer.Visible = false;
                    divQualitySign.Visible = true;
                }

                //表单登记情况
                if (!string.IsNullOrEmpty(item.RegFeeBack))
                {
                    lbRegFeeBack.Text = item.RegFeeBack;
                }
                if (!string.IsNullOrEmpty(item.RegConfirmer))
                    lbRegConfirmer.Text = string.Format("签名：{0}", user.GetUser(item.RegConfirmer).PersonName);
                else
                {   
                    lbRegFeeBack.Visible = false;
                    tbRegFeeBack.Visible = true;
                    lbRegConfirmer.Visible = false;
                    divRegSign.Visible = true;
                }

                //其它处理意见
                if (!string.IsNullOrEmpty(item.OtherFeeBack))
                {
                    lbOtherFeeBack.Text = item.OtherFeeBack;
                }
                if (!string.IsNullOrEmpty(item.OtherConfirmer))
                    lbOtherConfirmer.Text = string.Format("签名：{0}", user.GetUser(item.OtherConfirmer).PersonName);
                else
                {    
                    lbOtherFeeBack.Visible = false;
                    tbOtherFeeBack.Visible = true;
                    lbOtherConfirmer.Visible = false;
                    lbOtherConfirmTime.Visible = false;
                    divOtherSign.Visible = true;
                }

                if (DateTime.Compare(item.OtherConfirmTime, DateTime.MinValue) != 0)
                    lbOtherConfirmTime.Text = string.Format("时间：{0}",item.OtherConfirmTime.ToString("yyyy年MM月dd日"));

                if (!string.IsNullOrEmpty(item.ResultConfirmer))
                    lbResultConfirmer.Text = user.GetUser(item.ResultConfirmer).PersonName;
                else
                {
                    lbResultConfirmer.Visible = false;
                    divResultSign.Visible = true;
                }
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "获取仓库检查表内容失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
        }
        else if (cmd == "delete")
        {
            try
            {
                wareHouseCheck.DeleteWareHouseCheck(id);
            }
            catch (Exception ex)
            {
                EventMessage.MessageBox(Msg_Type.Error, "操作失败", "删除仓库检查表失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
            }
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "删除仓库检查表成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("WareHouseCheck.aspx"), UrlType.Href, "");
        }
    }
    /// <summary>
    /// 数量情况签名
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSignQuantity_Click(object sender, EventArgs e)
    {
        //先检查用户名与密码是否相符
        string errorMsg = "";
        if (tbQuantityUser.Text.Trim() == "")
            errorMsg = "请输入用户名（数量情况）";
        else if (tbQuantityPassword.Text.Trim() == "")
            errorMsg = "请输入密码（数量情况）";

        if (errorMsg != "")
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入有误：" + errorMsg, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        bool bValidate = false;
        try
        {
            bValidate = user.ValidatePassword(tbQuantityUser.Text.Trim(), Common.md5(tbQuantityPassword.Text.Trim(), 32));
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "校验（数量情况）用户名密码时发生错误", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        if (!bValidate)
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "校验（数量情况）用户名密码不相符", Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");

        try
        {
            wareHouseCheck.QuantitySign(id, tbQuantityUser.Text.Trim(), tbQuantityFeeBack.Text.Trim());
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "数量情况确认签名失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        Response.Redirect("SignForm.aspx?id=" + id + "&cmd=edit");
    }
    /// <summary>
    /// 质量情况签名
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btQualitySign_Click(object sender, EventArgs e)
    {
        //先检查用户名与密码是否相符
        string errorMsg = "";
        if (tbQualityUser.Text.Trim() == "")
            errorMsg = "请输入用户名（质量情况）";
        else if (tbQualityPassword.Text.Trim() == "")
            errorMsg = "请输入密码（质量情况）";

        if (errorMsg != "")
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入有误：" + errorMsg, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        bool bValidate = false;
        try
        {
            bValidate = user.ValidatePassword(tbQualityUser.Text.Trim(), Common.md5(tbQualityPassword.Text.Trim(), 32));
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "校验（质量情况）用户名密码时发生错误", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        if (!bValidate)
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "校验（质量情况）用户名密码不相符", Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");

        try
        {
            wareHouseCheck.QualitySign(id, tbQualityUser.Text.Trim(), tbQualityFeeBack.Text.Trim());
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "质量情况确认签名失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        Response.Redirect("SignForm.aspx?id=" + id + "&cmd=edit");
    }
    /// <summary>
    /// 表单登记情况签名
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btRegSign_Click(object sender, EventArgs e)
    {
        //先检查用户名与密码是否相符
        string errorMsg = "";
        if (tbRegUser.Text.Trim() == "")
            errorMsg = "请输入用户名（表单登记情况）";
        else if (tbRegPassword.Text.Trim() == "")
            errorMsg = "请输入密码（表单登记情况）";

        if (errorMsg != "")
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入有误：" + errorMsg, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        bool bValidate = false;
        try
        {
            bValidate = user.ValidatePassword(tbRegUser.Text.Trim(), Common.md5(tbRegPassword.Text.Trim(), 32));
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "校验（表单登记情况）用户名密码时发生错误", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        if (!bValidate)
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "校验（表单登记情况）用户名密码不相符", Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");

        try
        {
            wareHouseCheck.RegistrationSign(id, tbRegUser.Text.Trim(), tbRegFeeBack.Text.Trim());
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "表单登记情况确认签名失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        Response.Redirect("SignForm.aspx?id=" + id + "&cmd=edit");
    }
    /// <summary>
    /// 其它意见签名
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btOtherSign_Click(object sender, EventArgs e)
    {
        //先检查用户名与密码是否相符
        string errorMsg = "";
        if (tbOtherUser.Text.Trim() == "")
            errorMsg = "请输入用户名（其它意见）";
        else if (tbOtherPassword.Text.Trim() == "")
            errorMsg = "请输入密码（其它意见）";

        if (errorMsg != "")
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入有误：" + errorMsg, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        bool bValidate = false;
        try
        {
            bValidate = user.ValidatePassword(tbOtherUser.Text.Trim(), Common.md5(tbOtherPassword.Text.Trim(), 32));
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "校验（其它意见）用户名密码时发生错误", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        if (!bValidate)
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "校验（其它意见）用户名密码不相符", Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");

        try
        {
            wareHouseCheck.OtherOpinionSign(id, tbOtherUser.Text.Trim(), tbOtherFeeBack.Text.Trim());
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "其它意见确认签名失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        Response.Redirect("SignForm.aspx?id=" + id + "&cmd=edit");
    }
    /// <summary>
    /// 结果确认签名
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btResultSign_Click(object sender, EventArgs e)
    {
        //先检查用户名与密码是否相符
        string errorMsg = "";
        if (tbResultUser.Text.Trim() == "")
            errorMsg = "请输入用户名（结果确认人）";
        else if (tbResultPassword.Text.Trim() == "")
            errorMsg = "请输入密码（结果确认人）";

        if (errorMsg != "")
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "输入有误：" + errorMsg, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        bool bValidate = false;
        try
        {
            bValidate = user.ValidatePassword(tbResultUser.Text.Trim(), Common.md5(tbResultPassword.Text.Trim(), 32));
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "校验（结果确认人）用户名密码时发生错误", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        if (!bValidate)
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "校验（结果确认人）用户名密码不相符", Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        try
        {
            wareHouseCheck.ResultConfirmSign(id, tbResultUser.Text.Trim(),WareHouseFormStatus.Finished);
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "结果确认签名失败", ex, Icon_Type.Error, true, "history.go(-1)", UrlType.JavaScript, "");
        }
        Response.Redirect("SignForm.aspx?id=" + id + "&cmd=edit");
    }
}
