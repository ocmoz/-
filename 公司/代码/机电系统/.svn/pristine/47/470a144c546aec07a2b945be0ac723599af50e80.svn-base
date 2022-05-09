using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FM2E.BLL.Maintain;
using FM2E.Model.Maintain;
using FM2E.BLL.Utils;
using FM2E.Model.Basic;
using FM2E.BLL.Basic;

namespace FM2E.BLL.Equipment
{
    /// <summary>
    /// 设备信息统计逻辑类
    /// </summary>
    public class EquipmentStatistic
    {
        /// <summary>
        /// 计算设备完好率，并返回故障设备列表
        /// </summary>
        /// <param name="addressCode">地址字符编码</param>
        /// <param name="systemID">系统ID</param>
        /// <param name="equipmentCount">设备总数量</param>
        /// <param name="rate">设备完好率</param>
        /// <returns>故障设备列表</returns>
        public IList ComputeServiceabilityRate1(string addressCode, string systemID, DateTime ReportDateFrom, DateTime ReportDateTo, out int equipmentCount, out float rate)
        {
            IList faultyEquipments1 = new Equipment().GetEquipmentCount(addressCode, systemID, out equipmentCount);  //得到符合查询条件的设备总数,以及故障设备列表

            MalfunctionSearchInfo term = new MalfunctionSearchInfo();
            term.AddressCode = addressCode;
            term.SystemID = systemID;
            term.ReportDateFrom = ReportDateFrom;
            term.ReportDateTo = ReportDateTo;

            IList faultyEquipments2 = new MalfunctionHandle().GetFaultyEquipments(term);

            foreach (FaultyEquipmentInfo item in faultyEquipments2)
            {
                if (string.IsNullOrEmpty(item.EquipmentNO))
                {
                    faultyEquipments1.Add(item);
                }
            }
            int faultyCount = faultyEquipments1.Count;

            if (equipmentCount != 0)
                rate = (float)(equipmentCount - faultyEquipments1.Count) / equipmentCount;
            else rate = 0;

            return faultyEquipments1;
        }

        /// <summary>
        /// 计算设备完好率，并返回故障设备列表
        /// </summary>
        /// <param name="addressCode">地址字符编码</param>
        /// <param name="systemID">系统ID</param>
        /// <param name="equipmentCount">设备总数量</param>
        /// <param name="rate">设备完好率</param>
        /// <returns>故障设备列表</returns>
        public IList ComputeServiceabilityRate2(string companyid,long mainteamid,string addressCode, string systemID, DateTime ReportDateFrom, DateTime ReportDateTo, out int equipmentCount, out float rate)
        {
            IList faultyEquipments1 = new Equipment().GetEquipmentCount(companyid,mainteamid,addressCode, systemID, out equipmentCount);  //得到符合查询条件的设备总数,以及故障设备列表

            MalfunctionSearchInfo term = new MalfunctionSearchInfo();
            term.AddressCode = addressCode;
            term.SystemID = systemID;
            term.ReportDateFrom = ReportDateFrom;
            term.ReportDateTo = ReportDateTo;
            term.CompanyID = companyid;
            term.DepartmentID = mainteamid;
            

            IList maintainedEquipments1 = new MalfunctionHandle().GetMaintainedEquipments(term);
            IList maintainedEquipments12 = new ArrayList();

            int maintainedcount = 0;

            NoSortHashTable ht = new NoSortHashTable();

            //NoSortHashTable addressht = new NoSortHashTable();

            //if(mainteamid!=0)
            //{
            //    IList addresslist = new Address().GetAddressByMaintainDept(term.DepartmentID);
            //    foreach (AddressInfo addressinfo in addresslist)
            //    {
            //        addressht.Add(addressinfo.AddressCode, addressinfo.AddressCode);
            //    }

            //}

            foreach (MaintainedEquipmentInfo temp in maintainedEquipments1)
            {



                //if (mainteamid != 0 && temp.AddressCode)
                //    continue;


                if (ht.ContainsKey(temp.SheetID + "," + temp.EquipmentNO))
                {
                    if (temp.MaintainResult == MaintainedEquipmentStatus.Fixed || temp.MaintainResult == MaintainedEquipmentStatus.Replace || temp.MaintainResult == MaintainedEquipmentStatus.Scrapped)
                    {
                        ht.Remove(temp.SheetID + "," + temp.EquipmentNO);
                        maintainedcount--;


                    }
                    else
                    {
                        ht.Remove(temp.SheetID + "," + temp.EquipmentNO);
                        ht.Add(temp.MainteamID + "," + temp.EquipmentNO, temp);
                    }
                }
                else
                {
                    if (temp.MaintainResult != MaintainedEquipmentStatus.Fixed && temp.MaintainResult != MaintainedEquipmentStatus.Replace && temp.MaintainResult != MaintainedEquipmentStatus.Scrapped)
                    {
                        maintainedcount++;
                        ht.Add(temp.SheetID + "," + temp.EquipmentNO, temp);
                    }
                }
            }

            foreach (String key in ht.Keys)
            {
                maintainedEquipments12.Add(ht[key]);
            }



            //foreach (MaintainedEquipmentInfo temp in maintainedEquipments1)
            //{
            //    if (temp.MaintainResult == MaintainedEquipmentStatus.Wait4Repair)
            //    {
            //        maintainedcount++;
            //    }
            //}

            if (equipmentCount != 0)
                rate = (float)(equipmentCount - maintainedcount) / equipmentCount;
            else rate = 0;

            return maintainedEquipments12;
        }

        /// <summary>
        /// 计算设备完好率，并返回故障设备列表
        /// </summary>
        /// <param name="addressCode">地址字符编码</param>
        /// <param name="systemID">系统ID</param>
        /// <param name="equipmentCount">设备总数量</param>
        /// <param name="rate">设备完好率</param>
        /// <returns>故障设备列表</returns>
        public IList ComputeServiceabilityRate(string companyid, long mainteamid, string addressCode, string systemID, DateTime ReportDateFrom, DateTime ReportDateTo, out int equipmentCount, out float rate)
        {
            IList faultyEquipments1 = new Equipment().GetEquipmentCount(companyid, mainteamid, addressCode, systemID, out equipmentCount);  //得到符合查询条件的设备总数,以及故障设备列表

            MalfunctionSearchInfo term = new MalfunctionSearchInfo();
            term.AddressCode = addressCode;
            term.SystemID = systemID;
            term.ReportDateFrom = ReportDateFrom;
            term.ReportDateTo = ReportDateTo;
            term.CompanyID = companyid;
            term.DepartmentID = mainteamid;


            IList maintainedEquipments1 = new MalfunctionHandle().GetMaintainedEquipments(term);
            IList maintainedEquipments12 = new ArrayList();

            int maintainedcount = 0;

            NoSortHashTable ht = new NoSortHashTable();

            //NoSortHashTable addressht = new NoSortHashTable();

            //if(mainteamid!=0)
            //{
            //    IList addresslist = new Address().GetAddressByMaintainDept(term.DepartmentID);
            //    foreach (AddressInfo addressinfo in addresslist)
            //    {
            //        addressht.Add(addressinfo.AddressCode, addressinfo.AddressCode);
            //    }

            //}

            foreach (MaintainedEquipmentInfo temp in maintainedEquipments1)
            {



                //if (mainteamid != 0 && temp.AddressCode)
                //    continue;


                if (ht.ContainsKey(temp.SheetID + "," + temp.EquipmentNO))
                {
                    if (temp.MaintainResult == MaintainedEquipmentStatus.Fixed || temp.MaintainResult == MaintainedEquipmentStatus.Replace || temp.MaintainResult == MaintainedEquipmentStatus.Scrapped)
                    {
                        ht.Remove(temp.SheetID + "," + temp.EquipmentNO);
                        maintainedcount--;


                    }
                    else
                    {
                        ht.Remove(temp.SheetID + "," + temp.EquipmentNO);
                        ht.Add(temp.MainteamID + "," + temp.EquipmentNO, temp);
                    }
                }
                else
                {
                    //修改，待修正 6.13
                    if (temp.MaintainResult == MaintainedEquipmentStatus.Fixed && temp.MaintainResult != MaintainedEquipmentStatus.Replace && temp.MaintainResult != MaintainedEquipmentStatus.Scrapped)
                    {
                        maintainedcount++;
                        ht.Add(temp.SheetID + "," + temp.EquipmentNO, temp);
                    }
                }
            }

            foreach (String key in ht.Keys)
            {
                maintainedEquipments12.Add(ht[key]);
            }



            //foreach (MaintainedEquipmentInfo temp in maintainedEquipments1)
            //{
            //    if (temp.MaintainResult == MaintainedEquipmentStatus.Wait4Repair)
            //    {
            //        maintainedcount++;
            //    }
            //}

            if (equipmentCount != 0)
                rate = (float)(equipmentCount - maintainedcount) / equipmentCount;
            else rate = 0;

            return maintainedEquipments12;
        }
    }
}
