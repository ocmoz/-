using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Equipment;

namespace FM2E.IDAL.Equipment
{
    public interface IScrapRecord
    {
        /// <summary>
        /// 获取某个报废申请的报废设备登记明细
        /// </summary>
        /// <param name="scrapID"></param>
        /// <returns></returns>
        IList GetScrapRecordList(long scrapID);
        /// <summary>
        /// 获取某个报废申请的报废设备登记明细(支持分页)
        /// </summary>
        /// <param name="scrapID"></param>
        /// <returns></returns>
        IList GetScrapRecordList(QueryParam term, out int recordCount);
        /// <summary>
        /// 设备报废登记
        /// </summary>
        /// <param name="scrapRecords"></param>
        void AddscrapRecord(ScrapRecordInfo item);

        QueryParam GenerateSearchTerm(ScrapRecordSearchInfo term);
    }
}
