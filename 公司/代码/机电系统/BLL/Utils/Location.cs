using System;
using System.Collections.Generic;
using System.Text;
using FM2E.BLL.Basic;
using FM2E.Model.Basic;

namespace FM2E.BLL.Utils
{
    /// <summary>
    /// 位置转换类
    /// </summary>
    public class Location
    {
        /// <summary>
        /// 获取位置类型的文字表示
        /// </summary>
        /// <param name="locationTag"></param>
        /// <returns></returns>
        public static string GetLocationTagName(string locationTag)
        {
            string locationType = "未知位置类型";
            switch (locationTag)
            {
                case "1":
                    locationType = "隧道";
                    break;
                case "2":
                    locationType = "收费站";
                    break;
                case "3":
                    locationType = "桩号";
                    break;
                case "4":
                    locationType = "仓库";
                    break;
                case "5":
                    locationType = "公司";
                    break;
                default:
                    break;
            }
            return locationType;
        }

        /// <summary>
        /// 根据位置类型(locationTag)获取具体的位置名称
        /// </summary>
        /// <param name="locationTag"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public static string GetLocationName(string locationTag, string locationID)
        {
            string locationName = "";
            try
            {
                switch (locationTag)
                {
                    case "1": //隧道
                        Channal channel = new Channal();
                        ChannalInfo channalInfo = channel.GetChannal(locationID);
                        locationName = channalInfo.ChannalName;
                        break;
                    case "2": //收费站
                        TollGate tollGate = new TollGate();
                        TollGateInfo tollGateInfo = tollGate.GetTollGate(locationID);
                        locationName = tollGateInfo.TollGateName;
                        break;
                    case "3":
                        //桩号
                        locationName = locationID;
                        break;
                    case "4":
                        Warehouse warehouse = new Warehouse();
                        WarehouseInfo warehouseInfo = warehouse.GetWarehouse(locationID);
                        locationName = warehouseInfo.Name;
                        break;
                    case "5":
                        //公司
                        locationName = "暂未处理该种类型";
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                locationName = "";
            }
            return locationName;
        }
    }
}
