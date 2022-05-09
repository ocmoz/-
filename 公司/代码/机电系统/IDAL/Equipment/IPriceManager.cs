using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.IDAL.Equipment
{
    public interface IPriceManager
    {
        IList<PriceApplyInfo> GetAllPriceApply();
        IList<PriceApplyInfo> GetRecentPriceApply(int num);
        PriceApplyInfo GetPriceApply(string id);

        long InsertPriceApply(PriceApplyInfo item);
        void UpdatePriceApply(PriceApplyInfo item);
        void DelPriceApply(PriceApplyInfo item);
        IList<PriceApplyInfo> SearchPriceApply(PriceApplyInfo item);
        QueryParam GeneratePriceApplySearchTerm(PriceApplyInfo item);
        IList GetPriceApplyList(QueryParam term, out int recordCount, string companyid);

        IList<PriceApplyDetailInfo> GetAllPriceApplyDetail();
        IList<PriceApplyDetailInfo> GetRecentPriceApplyDetail(int num);
        PriceApplyDetailInfo GetPriceApplyDetail(string id);

        void InsertPriceApplyDetail(PriceApplyDetailInfo item);
        void UpdatePriceApplyDetail(PriceApplyDetailInfo item);
        void DelPriceApplyDetail(PriceApplyDetailInfo item);
        IList<PriceApplyDetailInfo> SearchPriceApplyDetail(PriceApplyDetailInfo item);
        QueryParam GeneratePriceApplyDetailSearchTerm(PriceApplyDetailInfo item);
        IList GetPriceApplyDetailList(QueryParam term, out int recordCount, string companyid);

        IList<PriceHistoryInfo> GetAllPriceHistory();
        IList<PriceHistoryInfo> GetRecentPriceHistory(int num);
        PriceHistoryInfo GetPriceHistory(string id);

        void InsertPriceHistory(PriceHistoryInfo item);
        void UpdatePriceHistory(PriceHistoryInfo item);
        void DelPriceHistory(PriceHistoryInfo item);
        IList<PriceHistoryInfo> SearchPriceHistory(PriceHistorySearchInfo item);
        QueryParam GeneratePriceHistorySearchTerm(PriceHistorySearchInfo item);
        IList GetPriceHistoryList(QueryParam term, out int recordCount, string companyid);

        IList<PriceDetailInfo> GetAllPriceDetail();
        IList<PriceDetailInfo> GetRecentPriceDetail(int num);
        PriceDetailInfo GetPriceDetail(string id);

        void InsertPriceDetail(PriceDetailInfo item);
        void UpdatePriceDetail(PriceDetailInfo item);
        void DelPriceDetail(PriceDetailInfo item);
        IList<PriceDetailInfo> SearchPriceDetail(PriceDetailSearchInfo item);
        QueryParam GeneratePriceDetailSearchTerm(PriceDetailSearchInfo item);
        QueryParam GeneratePriceApplySearchTerm(PriceApplyInfo item, string[] WFStates);
        IList GetPriceDetailList(QueryParam term, out int recordCount, string companyid);

        /// <summary>
        /// 采购历史价格By zjf 2009-02-04
        /// </summary>
        /// <param name="item">购买历史实体</param>
        void InsertPricePurchaseHistory(PricePurchaseHistoryInfo item);
        /// <summary>shi
        /// 生成查询历史购买价格条件By hmz 2009-02-10
        /// </summary>
        /// <param name="item">购买历史实体</param>
        QueryParam GeneratePurchasePriceHistorySearchTerm(PricePurchaseHistoryInfo item);
        /// <summary>
        /// 获取历史购买价格
        /// </summary>
        /// <param name="term">查询条件</param>
        /// <param name="recordCount">记录数</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        IList GetPurchasePriceHistoryList(QueryParam term, out int recordCount, string companyid);

           /// <summary>
        /// 生成表单查询参数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam GeneratePriceApplySearchTermEx(PriceApplySearchInfo item);

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="qp">查询参数</param>
        /// <returns></returns>
        IList SearchPriceApplyForm(QueryParam qp, out int recordCount);
    }
}
