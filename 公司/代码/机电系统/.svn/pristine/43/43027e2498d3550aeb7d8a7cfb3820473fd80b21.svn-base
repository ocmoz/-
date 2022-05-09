using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using AjaxControlToolkit;
using FM2E.BLL.Equipment;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using WebUtility;
using System.Collections.Specialized;

/// <summary>
///EquipmentInfoService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
[System.Web.Script.Services.ScriptService]
public class EquipmentInfoService : System.Web.Services.WebService
{

    public EquipmentInfoService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string[] GetEquipmentNameList(string prefixText, int count)
    {
        //EventMessage.EventWriteLog(WebUtility.Components.Msg_Type.Info, "Test WebService");
        Equipment bll = new Equipment();
        IList<string> list = bll.GetEquipmentNameList(prefixText.Trim(), count);
        return list.ToArray();
    }

}

