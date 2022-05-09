using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Basic;
using FM2E.IDAL.Basic;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Basic
{
    public class Warehouse
    {
        IWarehouse dal = FM2E.DALFactory.BasicAccess.CreateWarehouse();
        public IList<WarehouseInfo> GetAllWarehouse()
        {
            
            return dal.GetAllWarehouse();
        }
        public WarehouseInfo GetWarehouse(string warehouseid)
        {
            return dal.GetWarehouse(warehouseid);
        }
        public void DelWarehouse(string warehouseid)
        {
            dal.DelWarehouse(warehouseid);
        }
        public IList<WarehouseInfo> SearchWarehouse(WarehouseInfo item)
        {
            return dal.Search(item);
        }
        public void InsertWarehouse(WarehouseInfo item)
        {
            dal.InsertWarehouse(item);
        }
        public void UpdateWarehouse(WarehouseInfo item)
        {
            dal.UpdateWarehouse(item);
        }
        public QueryParam GenerateSearchTerm(WarehouseInfo item)
        {
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            return dal.GetList(searchTerm, out recordCount);
        }
        public IList GetAllWarehouseByCompany(string CompanyID)
        {
            
            return dal.GetAllWarehouseByCompany(CompanyID);
        }

        public void InsertWarehouseUser(WarehouseUserInfo model)
        {
            dal.InsertWarehouseUser(model);
        }
        public void DelWarehouseUser(WarehouseUserInfo model)
        {
            dal.DelWarehouseUser(model);
        }
        public WarehouseInfo GetWarehouseByUserName(string UserName)
        {
            return dal.GetWarehouseByUserName(UserName);
        }

        //********************************* Modified by Xue 2011-7-26 *******************
        public List<WarehouseInfo> GetWarehouseListByUserName(string UserName)
        {
            return dal.GetWarehouseListByUserName(UserName);
        }
        //********************************* Modification Finished *************************

        public IList GetWarehouseUserList(QueryParam searchTerm, string WarehouseID, out int recordCount)
        {
            return dal.GetWarehouseUserList(searchTerm, WarehouseID, out recordCount);
        }
        public IList GetAllWarehouseUserByWarehouseID(string WarehouseID)
        {
            return dal.GetAllWarehouseUserByWarehouseID(WarehouseID);
        }
    }
}
