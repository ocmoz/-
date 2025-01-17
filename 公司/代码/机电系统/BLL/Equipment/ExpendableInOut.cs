﻿using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using FM2E.IDAL.Equipment;
using FM2E.Model.Utils;
using System.Collections;
using System.Data.Common;

namespace FM2E.BLL.Equipment
{
    public class ExpendableInOut
    {
        IExpendableInOut dal = FM2E.DALFactory.EquipmentAccess.CreateExpendableInOut();
        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public long InsertRecord(ExpendableInOutRecordInfo record)
        {
            return dal.Add(record, null);
        }
        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="record"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public long InsertRecord(ExpendableInOutRecordInfo record, DbTransaction trans)
        {
            return dal.Add(record, trans);
        }

        /// <summary>
        /// 搜索记录，精确匹配名称、型号
        /// </summary>
        /// <param name="info"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList SearchRecord(ExpendableInOutRecordSearchInfo info,int pageIndex,int pageSize,out int recordCount)
        {
            QueryParam qp = dal.GenerateSearchTerm(info);
            qp.PageIndex = pageIndex;
            qp.PageSize = pageSize;
            return dal.GetList(qp, out recordCount);
        }
        public IList SearchRecordOut(ExpendableInOutRecordSearchInfo info, int pageIndex, int pageSize, out int recordCount)
        {
            QueryParam qp = dal.GenerateSearchTermOut(info);
            qp.PageIndex = pageIndex;
            qp.PageSize = pageSize;
            return dal.GetList(qp, out recordCount);
        }

        public void DelExpendableInOut(long ExpendableID,long ID)
        {
            dal.DelExpendableInOut(ExpendableID,ID);
        }

        public ExpendableInOutRecordInfo GetExpendableInOut(long ExpendableID)
        {
            return dal.GetExpendableInOut(ExpendableID);
        }

        public IList GetExInOut(String companyid, String warehouseid, DateTime datefrom, DateTime dateto,long categoryid)
        {
            return dal.GetExInOut(companyid,warehouseid,datefrom,dateto,categoryid);
        }

        public IList GetExInOutYear(String companyid, String warehouseid, DateTime datefrom, DateTime dateto, long categoryid)
        {
            return dal.GetExInOutYear(companyid, warehouseid, datefrom, dateto, categoryid);
        }

    }
}
