using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.IDAL.Basic
{
    public interface IWarehouse
    {
        IList<WarehouseInfo> GetAllWarehouse();
        WarehouseInfo GetWarehouse(string warehouseid);
        void DelWarehouse(string warehouseid);
        IList<WarehouseInfo> Search(WarehouseInfo item);
        void InsertWarehouse(WarehouseInfo item);
        void UpdateWarehouse(WarehouseInfo item);
        QueryParam GenerateSearchTerm(WarehouseInfo item);
        IList GetList(QueryParam searchTerm, out int recordCount);
        IList GetAllWarehouseByCompany(string CompanyID);

        void InsertWarehouseUser(WarehouseUserInfo model);
        void DelWarehouseUser(WarehouseUserInfo model);
        WarehouseInfo GetWarehouseByUserName(string UserName);

        //********************************* Modified by Xue 2011-7-26 *******************
        List<WarehouseInfo> GetWarehouseListByUserName(string UserName);
        //********************************* Modification Finished *************************

        IList GetWarehouseUserList(QueryParam searchTerm, string WarehouseID, out int recordCount);
        IList GetAllWarehouseUserByWarehouseID(string WarehouseID);
    } 
}
