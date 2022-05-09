using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Equipment;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using System.Configuration;
using System.Collections;

namespace FM2E.BLL.Equipment
{
    public class Equipment
    {
        public IList GetAllEquipment()
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            return dal.GetAllEquipment();
        }

        /// <summary>
        /// 获取最近添加的num个设备
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public IList<EquipmentInfoFacade> GetRecentEquipment(int num)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            return dal.GetRecentEquipment(num);
        }

        public EquipmentInfoFacade GetEquipment(string id)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            EquipmentInfoFacade device = dal.GetEquipment(id);
            device.RelatedDevice = dal.GetRelatedDevice(device.EquipmentNO);
            return device;
        }
        public EquipmentInfoFacade GetEquipmentBYNO(string id)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            EquipmentInfoFacade device = dal.GetEquipmentBYNO(id);
            if (device != null && device.EquipmentID != 0)
            {
                device.RelatedDevice = dal.GetRelatedDevice(device.EquipmentNO);
            }
            return device;
        }

        public void InsertEquipment(EquipmentInfo item)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            //判断是不是试用版本，如果是试用版本，则收到数量限制
            string numstr = ConfigurationManager.AppSettings["WebDefinition"];
            int number = int.Parse(numstr);
            if (number > 0)
            {
                //受限的
                QueryParam qp = dal.GenerateSearchTerm(new EquipmentSearchInfo());
                int currentNumber = 0;
                dal.GetList(qp, out currentNumber, item.CompanyID);
                if (currentNumber >= number)//超出数量限制
                    return;
            }

            
            dal.InsertEquipment(item);
        }

        
        public void UpdateEquipment(EquipmentInfo item)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            dal.UpdateEquipment(item);
        }
        public void UpdateEquipment(EquipmentInfoFacade item)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            dal.UpdateEquipment(item);
        }

        public void DelEquipment(string id)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            dal.DelEquipment(id);
        }
        public IList<EquipmentInfoFacade> Search(EquipmentSearchInfo item)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            return dal.Search(item);
        }

        public QueryParam GenerateSearchTerm(EquipmentSearchInfo item)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            return dal.GenerateSearchTerm(item);
        }

        //********************************* Modified by Xue 2011-7-26 *******************
        public QueryParam GenerateSearchTermForWarehouse(EquipmentSearchInfo item, List<FM2E.Model.Basic.AddressInfo> addressinfor)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            return dal.GenerateSearchTermForWarehouse(item, addressinfor);
        }
        //********************************* Modification Finished *************************

        public QueryParam GenerateSearchTermForWarehouse(EquipmentSearchInfo item)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            return dal.GenerateSearchTermForWarehouse(item);
        }

        public QueryParam GenerateSearchTerm(EquipmentInfoFacade item)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            return dal.GenerateSearchTerm(item);
        }

        public IList GetList(QueryParam term, out int recordCount, string companyid)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            return dal.GetList(term, out recordCount, companyid);
        }
        public IList AssetAndDepreciation(AssetAndDepreciationInfo item)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            return dal.AssetAndDepreciation(item);
        }
        public IList Gettypelist(EquipmentSearchInfo item)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            return dal.Gettypelist(item);
        }

        /// <summary>
        /// 查询设备库存数量,By zjf
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="productName">产品名称</param>
        /// <param name="productModel">产品型号</param>
        /// <returns>库存信息</returns>
        public IList QueryStorage(int pageIndex, int pageSize, out int recordCount, string companyid,string productName, string productModel )
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            return dal.QueryStorage(pageIndex, pageSize, out recordCount, companyid, productName, productModel);
        }
        /// <summary>
        /// 查询设备库存数量,By zjf
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="productName">产品名称</param>
        /// <param name="productModel">产品型号</param>
        /// <param name="warehouseid">仓库ID</param>
        /// <returns>库存信息</returns>
        public IList QueryStorage(int pageIndex, int pageSize, out int recordCount, string companyid, string productName, string productModel, string warehouseid )
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            return dal.QueryStorage(pageIndex, pageSize, out recordCount, companyid, productName, productModel, warehouseid );
        }
       
        /// <summary>
        /// 查询设备库存数量,By zjf
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="warehouseID">仓库ID</param>
        /// <param name="productName">产品名称</param>
        /// <param name="productModel">产品型号</param>
        /// <returns>库存信息</returns>
        public IList QueryStorage(int pageIndex, int pageSize, out int recordCount, string companyid,long warehouseID, string productName, string productModel)
        {
            recordCount = 0;
            throw new Exception("Not Implemented");
            //return null;
        }

        
        ///// <summary>
        ///// 用于更新设备的维修相关的状态
        ///// </summary>
        ///// <param name="equipmentNO">设备条形码</param>
        ///// <param name="status">设备状态,如果status=0，则不更新状态</param>
        ///// <param name="maintainTimesIncrease">维修次数的增加值，如果maintainTimesIncrease=0，则不更新维修次数</param>
        ///// <param name="updateTime">最近更新时间</param>
        //public void UpdateEquipmentMaintainInfo(string equipmentNO, int status, int maintainTimesIncrease, DateTime updateTime)
        //{
        //    IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
        //    dal.UpdateEquipmentMaintainInfo(equipmentNO, status, maintainTimesIncrease, updateTime);
        //}

        ///// <summary>
        ///// 用于更新设备的维修相关的状态
        ///// </summary>
        ///// <param name="updateEquipmentInfo"></param>
        //public void UpdateEquipmentMaintainInfo(IList updateEquipmentInfo)
        //{
        //    IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
        //    dal.UpdateEquipmentMaintainInfo(updateEquipmentInfo);
        //}

        /// <summary>
        /// 根据地址以及系统获取相关的设备总数、故障设备总数
        /// </summary>
        /// <param name="addressCode"></param>
        /// <param name="systemID"></param>
        /// <param name="count">设备总数</param>
        /// <returns>故障设备列表</returns>
        public IList GetEquipmentCount(string addressCode, string systemID, out int count)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            return dal.GetEquipmentCount(addressCode, systemID, out count);
        }

        public IList GetEquipmentCount(string companyid, long mainteamid, string addressCode, string systemID, out int count)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            return dal.GetEquipmentCount(companyid, mainteamid,addressCode, systemID, out count);
        }

        /// <summary>
        /// WEB SERVICE所使用的获取设备名称列表
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<string> GetEquipmentNameList(string prefixText, int count)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            return dal.GetEquipmentName(prefixText, count); 
        }


        /// <summary>
        /// 获取导出设备信息列表
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public IList GetExportList(QueryParam searchTerm)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            return dal.GetExportList(searchTerm); 
        }
        /// <summary>
        /// 当前的设备总数
        /// </summary>
        /// <param name="term"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        public int GetCurrentDeviceCount(QueryParam term, string companyid)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            return dal.GetCurrentDeviceCount(term,companyid);
        }
        /// <summary>
        /// 获取设备列表，不包括仓库内的设备信息
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        public IList GetListWithoutWarehouse(QueryParam term, out int recordCount, string companyid)
        {
            IEquipment dal = FM2E.DALFactory.EquipmentAccess.CreateEquipment();
            return dal.GetListWithoutWarehouse(term, out recordCount, companyid);
        }
    }
}
