using System;
using System.Collections;
using System.Text;
using FM2E.Model.System;

namespace FM2E.IDAL.System
{
    public interface IModule
    {
        IList GetSubModules(string id,bool includeClosed);
        IList GetUserModules(string userName, string id);
        ModuleInfo GetModule(string moduleID);
        void AddModule(ModuleInfo model);
        void OrderModules(string[] ids);
        void UpdateModule(ModuleInfo model);
        void DeleteModule(string moduleID);
    }
}
