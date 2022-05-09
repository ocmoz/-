using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FM2E.IDAL.Basic;
using FM2E.Model.Basic;
using FM2E.DALFactory;

namespace FM2E.BLL.Basic
{
    public class EquipmentSystem
    {
        public IList GetAllSystem()
        {
            IEquipmentSystem dal = BasicAccess.CreateSystem();
            return dal.GetAllSystem();
        }
        /// <summary>
        /// 获取下拉选择框的ListItemCollection，第一项为空项
        /// </summary>
        /// <returns></returns>
        public ListItem[] GenerateListItemCollectionWithBlank()
        {
            
            IEquipmentSystem dal = BasicAccess.CreateSystem();
            IList list = dal.GetAllSystem();
            ListItem[] collection = new ListItem[list.Count + 1];
            collection[0] = new ListItem("--请选择系统--", "");
            for (int i = 0; i < list.Count;i++ )
            {
                EquipmentSystemInfo s = list[i] as EquipmentSystemInfo;
                collection[i+1] = new ListItem(s.SystemName, s.SystemID);
            }
            return collection;
        }

        /// <summary>
        /// 获取下拉选择框的ListItemCollection，不含有空项
        /// </summary>
        /// <returns></returns>
        public ListItem[] GenerateListItemCollection()
        {

            IEquipmentSystem dal = BasicAccess.CreateSystem();
            IList list = dal.GetAllSystem();
            ListItem[] collection = new ListItem[list.Count];
           
            for (int i = 0; i < list.Count; i++)
            {
                EquipmentSystemInfo s = list[i] as EquipmentSystemInfo;
                collection[i] = new ListItem(s.SystemName, s.SystemID);
            }
            return collection;
        }

        public EquipmentSystemInfo GetSystem(string id)
        {
            IEquipmentSystem dal=BasicAccess.CreateSystem();
            return dal.GetSystem(id);
        }
        public void InsertSystem(EquipmentSystemInfo item)
        {
            IEquipmentSystem dal = BasicAccess.CreateSystem();
            dal.InsertSystem(item);
        }
        public void UpdateSystem(EquipmentSystemInfo item)
        {
            IEquipmentSystem dal = BasicAccess.CreateSystem();
            dal.UpdateSystem(item);
        }
        public void DelSystem(string id)
        {
            IEquipmentSystem dal = BasicAccess.CreateSystem();
            dal.DelSystem(id);
        }
        public IList GetAllSubSystem(string id)
        {
            IEquipmentSystem dal = BasicAccess.CreateSystem();
            return dal.GetAllSubSystem(id);
        }
        public SubEquipmentSystemInfo GetSubSystem(string id)
        {
            IEquipmentSystem dal = BasicAccess.CreateSystem();
            return dal.GetSubSystem(id);
        }
        public void InsertSubSystem(SubEquipmentSystemInfo item)
        {
            IEquipmentSystem dal = BasicAccess.CreateSystem();
            dal.InsertSubSystem(item);
        }
        public void UpdateSubSystem(SubEquipmentSystemInfo item)
        {
            IEquipmentSystem dal = BasicAccess.CreateSystem();
            dal.UpdateSubSystem(item);
        }
        public void DelSubSystem(string id)
        {
            IEquipmentSystem dal = BasicAccess.CreateSystem();
            dal.DelSubSystem(id);
        }
    }
}
