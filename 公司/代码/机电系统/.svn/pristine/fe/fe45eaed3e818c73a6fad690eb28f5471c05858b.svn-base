using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using System.Collections;
using System.Data.Common;

namespace FM2E.IDAL.Equipment
{
    public interface IEquipment
    {
        IList GetAllEquipment();
        IList<EquipmentInfoFacade> GetRecentEquipment(int num);
        EquipmentInfoFacade GetEquipment(string id);
        EquipmentInfoFacade GetEquipmentBYNO(string id);
        void InsertEquipment(EquipmentInfo item);
        void UpdateEquipment(EquipmentInfo item);
        void UpdateEquipment(EquipmentInfoFacade item);
        void DelEquipment(string id);
        IList<EquipmentInfoFacade> Search(EquipmentSearchInfo item);
        QueryParam GenerateSearchTerm(EquipmentSearchInfo item);

        //********************************* Modified by Xue 2011-7-26 *******************
        QueryParam GenerateSearchTermForWarehouse(EquipmentSearchInfo item,List<FM2E.Model.Basic.AddressInfo> addressinfor);
        //********************************* Modification Finished *************************

        QueryParam GenerateSearchTermForWarehouse(EquipmentSearchInfo item);
        QueryParam GenerateSearchTerm(EquipmentInfoFacade item);
        IList GetList(QueryParam term, out int recordCount, string companyid);
        IList AssetAndDepreciation(AssetAndDepreciationInfo item);
        IList Gettypelist(EquipmentSearchInfo item);

        /// <summary>
        /// 查询库存量,by zjf 2009-1-11
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="recordCount">总记录条数</param>
        /// <param name="companyid">公司ID</param>
        /// <param name="productName">产品名称</param>
        /// <param name="productModel">产品型号</param>
        /// <returns>库存信息</returns>
        IList QueryStorage(int pageIndex, int pageSize, out int recordCount, string companyid, string productName, string productModel);
    

        /// <summary>
        /// 查询库存量,by zjf 2009-1-11
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="recordCount">总记录条数</param>
        /// <param name="companyid">公司ID</param>
        /// <param name="productName">产品名称</param>
        /// <param name="productModel">产品型号</param>
        /// <param name="warehouseid">仓库ID</param>
        /// <returns>库存信息</returns>
        IList QueryStorage(int pageIndex, int pageSize, out int recordCount, string companyid, string productName, string productModel, string warehouseid);
        
        /// <summary>
        /// 获取设备的下一个拆分编号
        /// </summary>
        /// <param name="equipmentNO"></param>
        /// <returns></returns>
        int GetNextSplitNO(string equipmentNO);
        ///// <summary>
        ///// 用于更新设备的维修相关的状态
        ///// </summary>
        ///// <param name="equipmentNO">设备条形码</param>
        ///// <param name="status">设备状态,如果status=0，则不更新状态</param>
        ///// <param name="maintainTimesIncrease">维修次数的增加值，如果maintainTimesIncrease=0，则不更新维修次数</param>
        ///// <param name="updateTime">最近更新时间</param>
        //void UpdateEquipmentMaintainInfo(string equipmentNO,int status, int maintainTimesIncrease, DateTime updateTime);

        /// <summary>
        /// 用于更新设备的维修相关的状态
        /// </summary>
        /// <param name="updateEquipmentInfo"></param>
        void UpdateEquipmentMaintainInfo(IList updateEquipmentInfo,DbTransaction trans);

        /// <summary>
        /// 获取相关的设备，即同一拆分设备，条形码除倒数第二位之外，其他相同
        /// </summary>
        /// <param name="equipmentno"></param>
        /// <returns></returns>
        IList GetRelatedDevice(string equipmentno);
        /// <summary>
        /// 根据地址以及系统获取相关的设备总数、故障设备总数
        /// </summary>
        /// <param name="addressCode"></param>
        /// <param name="systemID"></param>
        /// <param name="count">设备总数</param>
        /// <returns>故障设备列表</returns>
        IList GetEquipmentCount(string addressCode, string systemID, out int count);
        IList GetEquipmentCount(string companyid, long mainteamid, string addressCode, string systemID, out int count);

        IList<string> GetEquipmentName(string prefixText, int count);

        /// <summary>
        /// 获取导出设备信息列表
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        IList GetExportList(QueryParam searchTerm);
        /// <summary>
        /// 获取当前查询条件下的设备总量
        /// </summary>
        /// <param name="term"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        int GetCurrentDeviceCount(QueryParam term, string companyid);
        /// <summary>
        /// 获取设备列表，不包括仓库内的设备信息
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        IList GetListWithoutWarehouse(QueryParam term, out int recordCount, string companyid);
    }
}
