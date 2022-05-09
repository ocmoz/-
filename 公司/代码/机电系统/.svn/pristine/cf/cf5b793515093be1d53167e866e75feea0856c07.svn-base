using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using Microsoft.JScript;

using WebUtility;
using FM2E.BLL.BarCode;
public partial class Module_FM2E_DeviceManager_BarCode_BarCodeImage : System.Web.UI.Page
{
    string data = (string)Common.sink("data", MethodType.Get, 0, 0, DataType.Str);
    string type_value = (string)Common.sink("type", MethodType.Get, 0, 0, DataType.Str);
    string copyright = (string)Common.sink("cp", MethodType.Get, 0, 0, DataType.Str);
    protected void Page_Load(object sender, EventArgs e)
    {
        copyright = GlobalObject.unescape(copyright);
        Cobainsoft.Windows.Forms.BarcodeType t = (Cobainsoft.Windows.Forms.BarcodeType)Enum.Parse(typeof(Cobainsoft.Windows.Forms.BarcodeType), type_value);
        Image img = BarCode.GenerateBarCodeImage(data, t, copyright,1,100);
        img.Save(Response.OutputStream, ImageFormat.Jpeg);
    }
}
