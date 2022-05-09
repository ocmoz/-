using System;
using System.Collections;
using System.Text;
using FM2E.IDAL.System;
using FM2E.Model.System;

namespace FM2E.BLL.System
{
    public class Module
    {
        /// <summary>
        /// 获取子模块
        /// </summary>
        /// <returns></returns>
        public IList GetSubModules(string id, bool includeClosed)
        {
            IModule dal = FM2E.DALFactory.SystemAccess.CreateModule();
            return dal.GetSubModules(id,includeClosed);
        }
        public void OrderModules(string[] ids)
        {
            IModule dal = FM2E.DALFactory.SystemAccess.CreateModule();
            dal.OrderModules(ids);
        }
        public ModuleInfo GetModule(string moduleID)
        {
            IModule dal = FM2E.DALFactory.SystemAccess.CreateModule();
            return dal.GetModule(moduleID);
        }
        public void AddModule(ModuleInfo model)
        {
            IModule dal = FM2E.DALFactory.SystemAccess.CreateModule();
            dal.AddModule(model);
        }
        public void UpdateModule(ModuleInfo model)
        {
            IModule dal = FM2E.DALFactory.SystemAccess.CreateModule();
            dal.UpdateModule(model);
        }
        public void DeleteModule(string moduleID)
        {
            IModule dal = FM2E.DALFactory.SystemAccess.CreateModule();
            dal.DeleteModule(moduleID);
        }

        public IList GetUserModules(string userName, string id)
        {
            IModule dal = FM2E.DALFactory.SystemAccess.CreateModule();
            return dal.GetUserModules(userName, id);
        }
    }
}
