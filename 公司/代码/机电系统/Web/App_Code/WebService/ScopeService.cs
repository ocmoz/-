using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using AjaxControlToolkit;
using System.Collections.Generic;
using System.Collections.Specialized;

/// <summary>
///ScopeService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
[System.Web.Script.Services.ScriptService]
public class ScopeService : System.Web.Services.WebService
{

    public ScopeService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    [WebMethod]
    public CascadingDropDownNameValue[] getscope(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> scopelist = new List<CascadingDropDownNameValue>();
        scopelist.Add(new CascadingDropDownNameValue("按系统汇总", "1"));
        scopelist.Add(new CascadingDropDownNameValue("按种类汇总", "2"));
        return scopelist.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] getscope2(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> scopelist2 = new List<CascadingDropDownNameValue>();
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        string scopeitem = kv["scopetype"];
        switch (scopeitem)
        {
            case "1":
                {
                    scopelist2.Add(new CascadingDropDownNameValue("按种类汇总", "2"));
                    break;
                }
            case "2":
                {
                    scopelist2.Add(new CascadingDropDownNameValue("按系统汇总", "1"));
                    break;
                }
        }
        return scopelist2.ToArray();
    }
}

