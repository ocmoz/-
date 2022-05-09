using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;
using AjaxControlToolkit;
using System.Collections.Generic;
using System.Collections.Specialized;
/// <summary>
///SystemSubsystemService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
[System.Web.Script.Services.ScriptService]
public class SystemSubsystemService : System.Web.Services.WebService
{

    public SystemSubsystemService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetSystem(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> SystemList = new List<CascadingDropDownNameValue>();
        EquipmentSystem ebll = new EquipmentSystem();
        IList elist = ebll.GetAllSystem();
        foreach (EquipmentSystemInfo item in elist)
        {
            SystemList.Add(new CascadingDropDownNameValue(item.SystemName, item.SystemID));
        }
        return SystemList.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetSubsystem(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> SubsystemList = new List<CascadingDropDownNameValue>();
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        string Systemname = kv["System"];
        EquipmentSystem ebll = new EquipmentSystem();
        IList list = ebll.GetAllSubSystem(Systemname);
        foreach (SubEquipmentSystemInfo item in list)
        {
            SubsystemList.Add(new CascadingDropDownNameValue(item.SubSystemName, item.SubSystemID.ToString()));
        }
        return SubsystemList.ToArray();
    }
}

