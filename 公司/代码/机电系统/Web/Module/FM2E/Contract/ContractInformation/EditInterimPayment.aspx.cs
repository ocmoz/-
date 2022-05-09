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
using FM2E.Model.Contract;
using FM2E.BLL.Contract;
using WebUtility.Components;

public partial class Module_FM2E_Contract_ContractInformation_EditInterimPayment : System.Web.UI.Page
{
    private string cmd = (string)Common.sink("cmd", MethodType.Get, 50, 0, DataType.Str);
    private int id = (int)Common.sink("id", MethodType.Get, 50, 0, DataType.Int);
    int ContractId = (int)Common.sink("ContractId", MethodType.Get, 32, 0, DataType.Int);

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
                ContractInterimPaymentInfo item;

                ContractInformation contractInformationBll = new ContractInformation();
                item = contractInformationBll.GetContractInterimPaymentInfo(id);
                
                tb_PaymentAmount.Text = item.PaymentAmount.ToString();
                tb_PaymentTime.Text = item.PaymentTime.ToString("yyyy-MM-dd");
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
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：期中支付添加";

            TabPanel1.HeaderText = "添加期中支付";
        }
        else if (cmd == "edit")
        {
            HeadMenuWebControls1.HeadOPTxt = "目前操作功能：期中支付修改";

            TabPanel1.HeaderText = "修改期中支付";
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ContractInformation contractInformationBll = new ContractInformation();
        ContractInterimPaymentInfo item = new ContractInterimPaymentInfo();
        item.Id = id;
        item.ContractId = ContractId;
        if (tb_PaymentAmount.Text.Trim() != "")
            item.PaymentAmount = Convert.ToDecimal(tb_PaymentAmount.Text.Trim());
        else
            item.PaymentAmount = 0;
        if (tb_PaymentTime.Text.Trim() != "")
            item.PaymentTime = Convert.ToDateTime(tb_PaymentTime.Text.Trim());
        else
            item.PaymentTime = DateTime.Now;
        if (cmd == "add")
        {
            ContractId = Convert.ToInt32(Session["Contract"].ToString());
            contractInformationBll.InsertContractInterimPayment(item);
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "添加期中支付金额成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("EditContractInformation.aspx?cmd=view&id=" + ContractId), UrlType.Href, "");
        }
        else if (cmd == "edit")
        {
            contractInformationBll.UpdateContractInterimPayment(item);
            EventMessage.MessageBox(Msg_Type.Info, "操作成功", "修改期中支付金额成功！", Icon_Type.OK, true, Common.GetHomeBaseUrl("EditContractInformation.aspx?cmd=view&id=" + ContractId), UrlType.Href, "");
        }
    }
}
