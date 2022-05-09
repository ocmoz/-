using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using FM2E.BLL.Basic;
using FM2E.Model.Maintain;
using FM2E.Model.Basic;
using FM2E.BLL.Maintain;
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
public class SystemSubsystemMaintainObjectService : System.Web.Services.WebService
{

    public SystemSubsystemMaintainObjectService()
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
    [WebMethod]
    public CascadingDropDownNameValue[] GetDailyPatrolObject(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> DailyPatrolObjectList = new List<CascadingDropDownNameValue>();
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        string subsystem = kv["Subsystem"];
        MaintainPlanConfig bll = new MaintainPlanConfig();
        MaintainPlanConfigInfo info = new MaintainPlanConfigInfo();
        info.Subsystem = Convert.ToInt64(subsystem);
        info.PlanType = MaintainPlanType.DailyPatrol;
        IList list = bll.GetAllList(info);
        foreach (MaintainPlanConfigInfo item in list)
        {
            DailyPatrolObjectList.Add(new CascadingDropDownNameValue(item.PlanObject, item.ItemID.ToString()));
        }
        return DailyPatrolObjectList.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetRoutineInspectObject(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> RoutineInspectObjectList = new List<CascadingDropDownNameValue>();
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        string subsystem = kv["Subsystem"];
        MaintainPlanConfig bll = new MaintainPlanConfig();
        MaintainPlanConfigInfo info = new MaintainPlanConfigInfo();
        info.Subsystem = Convert.ToInt64(subsystem);
        info.PlanType = MaintainPlanType.RoutineInspect;
        IList list = bll.GetAllList(info);
        foreach (MaintainPlanConfigInfo item in list)
        {
            RoutineInspectObjectList.Add(new CascadingDropDownNameValue(item.PlanObject, item.ItemID.ToString()));
        }
        return RoutineInspectObjectList.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetRoutineMaintainObject(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> RoutineMaintainObjectList = new List<CascadingDropDownNameValue>();
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        string subsystem = kv["Subsystem"];
        MaintainPlanConfig bll = new MaintainPlanConfig();
        MaintainPlanConfigInfo info = new MaintainPlanConfigInfo();
        info.Subsystem = Convert.ToInt64(subsystem);
        info.PlanType = MaintainPlanType.RoutineMaintain;
        IList list = bll.GetAllList(info);
        foreach (MaintainPlanConfigInfo item in list)
        {
            RoutineMaintainObjectList.Add(new CascadingDropDownNameValue(item.PlanObject, item.ItemID.ToString()));
        }
        return RoutineMaintainObjectList.ToArray();
    }
}

