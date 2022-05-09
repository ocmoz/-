using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUtility.Components;
using WebUtility;
using System.Collections;
using FM2E.BLL.BarCode;
using System.Text;

public partial class Module_FM2E_DeviceManager_BarCode_BarCodePrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialPage();
        }
    }

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitialPage()
    {
        try
        {
            lbErrMsg.Text = "";

            IList printers = Constants.GetBarCodePrinters();
            ddlPrinters.Items.Clear();

            if (printers.Count == 1 && printers[0].ToString() == "获取打印机列表出错")
            {
                ddlPrinters.Items.Add(new ListItem("获取打印机列表出错", ""));
                ddlPrinters.Enabled = false;
                return;
            }

            //ddlPrinters.Items.Add(new ListItem("请选择打印机", ""));
            foreach (string printer in printers)
            {
                ddlPrinters.Items.Add(new ListItem(printer, printer));
            }

            //ddlPrinters.SelectedValue = "1";
            SelectParam();

            string cookiesKey = string.Format("{0}-BarCodePrinter", Common.Get_CookiesName);
            if (Response.Cookies[cookiesKey] != null && !string.IsNullOrEmpty(Response.Cookies[cookiesKey].Value))
            {
                //从cookie中读取默认打印机
                string printer = Response.Cookies[cookiesKey].Value.ToString();
                if (printer != "")
                    ddlPrinters.SelectedValue = System.Text.Encoding.Default.GetString(Convert.FromBase64String(printer));
            }

            if (Session[Constants.BARCODE_SESSION_STRING] == null)
            {
                lbErrMsg.Text = "错误：缺少需要打印的条形码信息";
            }
            else
            {
                BarCodeInfo[] barCodeArray = (BarCodeInfo[])Session[Constants.BARCODE_SESSION_STRING];
                barCodeCount.InnerText = barCodeArray.Length.ToString();
                sumSpan.InnerText = (Convert.ToInt32(tbCount.Text.Trim()) * barCodeArray.Length).ToString();
            }
        }
        catch (Exception ex)
        {
            lbErrMsg.Text = "错误：加载页面数据失败，原因："+ex.Message;
            EventMessage.EventWriteLog(Msg_Type.Error, lbErrMsg.Text);
            //EventMessage.MessageBox(Msg_Type.Error, "操作失败", "页面初始化失败", ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
    /// <summary>
    /// 选择打印机事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlPrinters_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectParam();
        btPrint.Enabled = true;
    }
    /// <summary>
    /// 选择端口事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlPort_SelectedIndexChanged(object sender, EventArgs e)
    {
        btPrint.Enabled = true;
    }
    private void SelectParam()
    {
        if (ddlPrinters.SelectedValue != "0")
        {
            PrintParameter param = new PrintParameter(ddlPrinters.SelectedValue, Constants.PRINTER_CONFIG_FILE);
            lbIPAddress.Text = string.Format("{0}：{1}", param.IPAddress, param.Port);
            lbLabelWidth.Text = param.LabelWidth.ToString() + " dots";
            lbLeftMargin.Text = param.LeftMargin.ToString() + " dots";
            lbLabelGap.Text = param.LabelGap.ToString() + " dots";
            lbBarCodeType.Text = param.BarCodeType;
            lbBarCodeOrg.Text = string.Format("X={0} dots,Y={1} dots", param.BCPrintPointX, param.BCPrintPointY);
            lbTitleOrg.Text = string.Format("X={0} dots,Y={1} dots", param.TitlePointX, param.TitlePointY);
            lbRemarkOrg.Text = string.Format("X={0} dots,Y={1} dots", param.RemarkPointX, param.RemarkPointY);
            lbBarCodeHeight.Text = param.BarCodeHeight.ToString() + " dots";
            lbBarCodeRatio.Text = param.BarCodeRatio.Replace(",", "：");
            lbBarMag.Text = param.BarMag.ToString();
            lbFont.Text = param.Font;
            lbFontSize.Text = param.FontSize.ToString() + " point";
            //   UpdatePanel1.Update();
        }
    }
    /// <summary>
    /// 打印
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btPrint_Click(object sender, EventArgs e)
    {
        int count = Convert.ToInt32(tbCount.Text);
        if (count == 0)
        {
            lbErrMsg.Text = "错误：打印失败（打印数量不能为0）";
            EventMessage.EventWriteLog(Msg_Type.Error, lbErrMsg.Text);
            return;
        }
        try
        {
            PrintParameter param = new PrintParameter(ddlPrinters.SelectedValue, Constants.PRINTER_CONFIG_FILE);

            BarCodeInfo[] barCodeArray = (BarCodeInfo[])Session[Constants.BARCODE_SESSION_STRING];
            //BarCodeInfo[] barCodes = new BarCodeInfo[barCodeArray.Length * count];

            //for (int i = 0; i < barCodes.Length; i++)
            //{
            //    barCodes[i] = new BarCodeInfo();
            //    barCodes[i].BarCode = barCodeArray[i / count].BarCode;
            //    barCodes[i].CompanyName = barCodeArray[i / count].CompanyName;
            //    barCodes[i].EquipmentName = barCodeArray[i / count].EquipmentName;
            //}
            if (barCodeArray.Length * count != 0)
            {
                StringBuilder printCmd = new StringBuilder();
                printCmd.Append("var printControl=document.getElementById(\"PrintActiveX\");");
                printCmd.AppendFormat("printControl.LabelWidth={0};", param.LabelWidth);
                printCmd.AppendFormat("printControl.PageWidth={0};", param.PageWidth);
                printCmd.AppendFormat("printControl.LeftMargin={0};", param.LeftMargin);
                printCmd.AppendFormat("printControl.LabelGap={0};", param.LabelGap);
                printCmd.AppendFormat("printControl.BarCodeType=\"{0}\";", param.BarCodeType);
                printCmd.AppendFormat("printControl.BarCodeX={0};", param.BCPrintPointX);
                printCmd.AppendFormat("printControl.BarCodeY={0};", param.BCPrintPointY);
                printCmd.AppendFormat("printControl.TitleX={0};", param.TitlePointX);
                printCmd.AppendFormat("printControl.TitleY={0};", param.TitlePointY);
                printCmd.AppendFormat("printControl.RemarkX={0};", param.RemarkPointX);
                printCmd.AppendFormat("printControl.RemarkY={0};", param.RemarkPointY);
                printCmd.AppendFormat("printControl.BarcodeHeight={0};", param.BarCodeHeight);
                printCmd.AppendFormat("printControl.BarCodeRatio=\"{0}\";", param.BarCodeRatio);
                printCmd.AppendFormat("printControl.BarMag={0};", param.BarMag);
                printCmd.AppendFormat("printControl.VerMajor={0};", param.MajorVersion);
                printCmd.AppendFormat("printControl.VerMinor={0};", param.MinorVersion);
                printCmd.AppendFormat("printControl.PortNum={0};", ddlPort.SelectedValue);

                for (int i = 0; i < barCodeArray.Length * count; i++)
                {
                    if (i == 0)
                    {
                        printCmd.AppendFormat("printControl.Barcodes=\"{0},{1},{2}", barCodeArray[i / count].BarCode, barCodeArray[i / count].EquipmentName, barCodeArray[i / count].CompanyName);

                    }
                    else
                    {
                        printCmd.AppendFormat("|{0},{1},{2}", barCodeArray[i / count].BarCode, barCodeArray[i / count].EquipmentName, barCodeArray[i / count].CompanyName);
                    }
                }
                if (barCodeArray.Length * count != 0)
                    printCmd.Append("\";printControl.PrintBarcode();");

                btPrint.Enabled = false;

                //********** Modified by Xue    For V 3.1.0    2011-09-09 **********************************************************************************
                if (FM2E.BLL.System.ConfigItems.PrintSwitch == "NetWork")
                {
                    BarCode barCodeBll = new BarCode();
                    barCodeBll.PrintBarCode(barCodeArray, param);

                    Session.Remove(Constants.BARCODE_SESSION_STRING);
                }
                if (FM2E.BLL.System.ConfigItems.PrintSwitch == "BarCodeControl")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PrintCommand", printCmd.ToString(), true);
                }
                //********** Modification Finished 2011-09-09 **********************************************************************************************
            }

            string cookiesKey = string.Format("{0}-BarCodePrinter", Common.Get_CookiesName);
            Response.Cookies[cookiesKey].Value = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(ddlPrinters.SelectedValue));
        }
        catch (Exception ex)
        {
            lbErrMsg.Text = "错误：打印失败（" + ex.Message + "）";
            btPrint.Enabled = true;
            EventMessage.EventWriteLog(Msg_Type.Error, lbErrMsg.Text);
            //EventMessage.MessageBox(Msg_Type.Error, "操作失败", "打印失败："+ex.Message, ex, Icon_Type.Error, true, "window.history.go(-1)", UrlType.JavaScript, "");
        }
    }
}
