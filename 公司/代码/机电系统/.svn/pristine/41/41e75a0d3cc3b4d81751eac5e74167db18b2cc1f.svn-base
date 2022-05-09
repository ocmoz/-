using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FM2E.BLL.BarCode;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using WebUtility;
using WebUtility.Components;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using FM2E.Model.System;

public partial class Module_FM2E_DeviceManager_BarCode_BarCode : System.Web.UI.Page
{
    //private Font printFont;
    //private StreamReader streamToPrint;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove(Constants.BARCODE_SESSION_STRING);
            InitialPage();
        }
    }

    private void InitialPage()
    {
        try
        {
            //绑定公司到下拉列表
            Company companyBll = new Company();
            IList<CompanyInfo> companyList = companyBll.GetAllCompany();

            ddlCompany.Items.Clear();
            ddlCompany.Items.Add(new ListItem("请选择公司", "0"));
            foreach (CompanyInfo item in companyList)
            {
                ddlCompany.Items.Add(new ListItem(item.CompanyName, item.CompanyID));
            }

            LoginUserInfo userInfo = UserData.CurrentUserData;
            if (!string.IsNullOrEmpty(userInfo.CompanyID))
            {
                ddlCompany.SelectedValue = userInfo.CompanyID;
            }

            tbPurchaseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            tbCount.Text = "1";
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "页面初始化失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 生成条形码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            lbErrMsg.Text = "";
            Session.Remove(Constants.BARCODE_SESSION_STRING);

            string company = ddlCompany.SelectedValue;
            DateTime purchaseDate = Convert.ToDateTime(tbPurchaseDate.Text.Trim());
            bool isComponent = cbIsComponent.Checked;
            int count = Convert.ToInt32(tbCount.Text.Trim());
            if (count > 100)
            {
                lbErrMsg.Text = "错误：一次最多只能生成100个条形码";
                lbErrMsg.ForeColor = System.Drawing.Color.Red;
                barCodeTable.Visible = false;
                ltBarCodes.Text = "";
                return;
            }
            string[] barCodes = BarCode.GenerateBarCode(company, purchaseDate, isComponent, count);
            if (barCodes == null)
            {
                barCodeTable.Visible = false;
                lbErrMsg.Text = "错误：没有生成任何条形码";
                lbErrMsg.ForeColor = System.Drawing.Color.Red;
                ltBarCodes.Text = "";
                return;
            }
            ltBarCodes.Text = "";
            barCodeTable.Visible = true;

            BarCodeInfo[] barCodeArray = new BarCodeInfo[barCodes.Length];
            for (int i = 0; i < barCodes.Length; i++)
            {
                barCodeArray[i] = new BarCodeInfo();
                barCodeArray[i].BarCode = barCodes[i];
                barCodeArray[i].CompanyName = ddlCompany.SelectedItem.Text;
                barCodeArray[i].EquipmentName = "";
                if ((i + 1) % 10 == 0)
                    ltBarCodes.Text += barCodes[i] + "<br/>";
                else ltBarCodes.Text += barCodes[i] + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            }

            Session[Constants.BARCODE_SESSION_STRING] = barCodeArray;
        }
        catch (Exception ex)
        {
            EventMessage.MessageBox(Msg_Type.Error, "操作失败", "批量生成条形码失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
}
