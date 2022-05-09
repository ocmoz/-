using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Maintain;
using System.Collections;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Maintain
{
    public interface IMaintainPlanConfig
    {
        void InsertMaintainPlanConfig(MaintainPlanConfigInfo model);  //添加计划
        void UpdateMaintainPlanConfig(MaintainPlanConfigInfo model);  //更新计划
        void DelMaintainPlanConfig(long PlanID);  //删除计划

        IList GetList(QueryParam searchTerm, out int recordCount);
        IList GetAllList(MaintainPlanConfigInfo model);  //根据model获取相关设备
        IList GetAllEquipmentByItemID(long itemID);  //根据项目获取所有项目相关的设备
        IList GetAllEquipmentByItemIDandAddessCode(long itemID, string AddressCode);  //根据项目及地址信息获取所有相关的设备
        IList GetMaintainPlanConfigByEquipmentNO(string EquipmentNO, MaintainPlanType PlanType);  //根据设备号码获取其计划
        IList GetListForEquipmentList(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(MaintainPlanConfigInfo item);
        QueryParam GenerateSearchTermForEquipmentList(MaintainPlanConfigInfo item);  //设备列表
        QueryParam GenerateSearchTermForEquipmentAddressList(MaintainPlanConfigInfo item, string addresscode);  //地址信息列表

        MaintainPlanConfigInfo GetMaintainPlanConfig(long PlanID);  //根据PlanID返回对象实体

        void UpdateEquipments(MaintainPlanConfigInfo model);
        void DelMaintainPlanEquipment(string EquipmentNO, long ItemID);
        void InsertMaintainPlanEquipment(string EquipmentNO, long ItemID);
    }
}
