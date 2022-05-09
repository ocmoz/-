using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using FM2E.Model.Basic;

namespace FM2E.IDAL.Basic
{
    public interface IEquipmentSystem
    {
        IList GetAllSystem();
        EquipmentSystemInfo GetSystem(string id);        
        void InsertSystem(EquipmentSystemInfo item);
        void UpdateSystem(EquipmentSystemInfo item);
        void DelSystem(string id);

        IList GetAllSubSystem(string id);
        SubEquipmentSystemInfo GetSubSystem(string id);
        void InsertSubSystem(SubEquipmentSystemInfo item);
        void UpdateSubSystem(SubEquipmentSystemInfo item);
        void DelSubSystem(string id);
    }
}
