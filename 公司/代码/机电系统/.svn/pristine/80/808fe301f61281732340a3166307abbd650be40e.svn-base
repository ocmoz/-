using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using System.Collections;
namespace FM2E.IDAL.Equipment
{
    /// <summary>
    /// 报验数据库接口
    /// </summary>
    public interface ICheckAcceptance
    {
        /// <summary>
        /// 含有详情
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        long InsertCheckAcceptanceWithDetail(CheckAcceptanceInfo item);


        void DeleteCheckAcceptanceInfo(long id);

        /// <summary>
        /// 不含有详情
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        long InsertCheckAcceptanceWithoutDetail(CheckAcceptanceInfo item);

        /// <summary>
        /// 获取一个报验单，包括所有详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CheckAcceptanceInfo GetCheckAcceptanceInfoWithAllDetail(long id);

        /// <summary>
        /// 只更新表，不更新详情
        /// </summary>
        /// <param name="item"></param>
        void UpdateCheckAcceptanceNoDetail(CheckAcceptanceInfo item);

        /// <summary>
        /// 更新表，而且跟新详情
        /// </summary>
        /// <param name="item"></param>
        void UpdateCheckAcceptanceWithDetail(CheckAcceptanceInfo item);

        /// <summary>
        /// 更新一个详情
        /// </summary>
        /// <param name="item"></param>
        void UpdateCheckAcceptanceDetail(CheckAcceptanceDetailInfo item);

        /// <summary>
        /// 入库的时候插入条形码记录
        /// </summary>
        /// <param name="item">插入到数据库的对象</param>
        void InsertBarcodeRecord(CheckAcceptanceDetailBarcodeInfo item);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        IList SearchCheckAcceptanceForm(QueryParam p, out int recordCount);

        /// <summary>
        /// 获取查询实体
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        QueryParam GenerateQueryItem(CheckAcceptanceSearchInfo info);

        CheckAcceptanceDetailInfo GetCheckAcceptanceDetailInfo(long id, short itemid);
    }
}
