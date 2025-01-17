﻿using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using System.Collections;
using FM2E.Model.Utils;
using System.Data.Common;

namespace FM2E.IDAL.Equipment
{
    public interface IExpendable
    {
        ExpendableInfo GetExpendable(long ExpendableID);
        bool InsertExpendable(ExpendableInfo model);
        void UpdateExpendable(ExpendableInfo model);
        void DelExpendable(long ExpendableID);
        IList GetList(QueryParam searchTerm, out int recordCount);
        QueryParam GenerateSearchTerm(ExpendableInfo item);
        IList GetAllExpendableModelbyName(string name);
        IList GetAllExpendableName();
        decimal GetCountOfExpendable(string WarehouseID, long ExpendableID);
        IList GetAllExpendableModelbyName(string warehouseID, string name);
        IList GetAllExpendableName(string warehouseID);
        /// <summary>
        /// 查询库存信息，模糊匹配产品名称、产品型号，by zjf 2009-1-11
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyid">公司ID</param>
        /// <param name="productName">产品名称</param>
        /// <param name="productModel">规格型号</param>
        /// <returns>查询的库存结果</returns>
        IList QueryStorage(int pageIndex, int pageSize, out int recordCount, string companyid, string productName, string productModel);
        IList QueryStorage(int pageIndex, int pageSize, out int recordCount, string companyid, string productName, string productModel,string warehouseid);
        /// <summary>
        /// 增加消耗品库存，完全匹配，by zjf 2009-1-20，需要先检查是否有符合条件（公司、仓库、产品名称、产品型号、单位完全匹配）的消耗品存在，如果没有，则插入一条新的记录，如果有，则增加数量
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="warehouseid">仓库ID</param>
        /// <param name="productname">产品名称</param>
        /// <param name="model">型号</param>
        /// <param name="unit">单位</param>
        /// <param name="count">增加数量</param>
        /// <returns>增加后的数量</returns>
        decimal AddExpendable(string companyid, string warehouseid, string productname, string model, string unit, decimal count,decimal price,long typeid);

        /// <summary>
        /// 获取产品的名称匹配
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IList<string> GetProductNames(string name,int count);


        /// <summary>
        /// 获取产品的型号匹配
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IList<string> GetProductModels(string model, int count);

        ExpendableInfo GetTopExpendableItem(string companyid,string WarehouseID, string Name, string Model, string unit);

        /// <summary>
        /// 用于出库
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="companyid"></param>
        /// <param name="warehouseid"></param>
        /// <param name="productname"></param>
        /// <param name="model"></param>
        /// <param name="unit"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        decimal AddExpendable(DbTransaction trans, string companyid, string warehouseid, string productname, string model, string unit, decimal price, decimal count,long categoryid);

        ExpendableStatisticsInfo GetExpendableStaticticsData(string companyid, string warehouseid, string CategoryID, DateTime datefrom, DateTime dateto);

    }
}
