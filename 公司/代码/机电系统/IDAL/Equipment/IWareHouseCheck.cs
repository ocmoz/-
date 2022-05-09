using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;

namespace FM2E.IDAL.Equipment
{
    public interface IWareHouseCheck
    {
        /// <summary>
        /// 获取所有的仓库检查单
        /// </summary>
        /// <returns></returns>
        IList GetAllWareHouseCheck();
        /// <summary>
        /// 获取符合条件的仓库检查单列表（支持分页）
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList GetWareHouseCheckList(QueryParam term, out int recordCount);
        /// <summary>
        /// 根据表单编号获取仓库检查单
        /// </summary>
        /// <param name="formNO"></param>
        /// <returns></returns>
        WareHouseCheckInfo GetWareHouseCheck(string formNO);
        /// <summary>
        /// 添加仓库检查单
        /// </summary>
        /// <param name="item"></param>
        void AddWareHouseCheck(WareHouseCheckInfo item);
        /// <summary>
        /// 更新仓库检查单
        /// </summary>
        /// <param name="item"></param>
        void UpdateWareHouseCheck(WareHouseCheckInfo item);
        /// <summary>
        /// 删除仓库检查单
        /// </summary>
        /// <param name="formNO"></param>
        void DeleteWareHouseCheck(string formNO);
        /// <summary>
        /// 生成检查单查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam GenerateSearchTerm(WareHouseCheckSearchInfo term);
        /// <summary>
        /// 数量情况确认人签名
        /// </summary>
        /// <param name="formNO"></param>
        /// <param name="confirmer"></param>
        /// <param name="opinion"></param>
        void QuantitySign(string formNO, string confirmer, string opinion);
        /// <summary>
        /// 质量情况确认人签名
        /// </summary>
        /// <param name="formNO"></param>
        /// <param name="confirmer"></param>
        /// <param name="opinion"></param>
        void QualitySign(string formNO, string confirmer, string opinion);
        /// <summary>
        /// 表单登记情况确认人签名
        /// </summary>
        /// <param name="formNO"></param>
        /// <param name="confirmer"></param>
        /// <param name="opinion"></param>
        void RegistrationSign(string formNO, string confirmer, string opinion);
        /// <summary>
        /// 其它意见确认人签名
        /// </summary>
        /// <param name="formNO"></param>
        /// <param name="confirmer"></param>
        /// <param name="opinion"></param>
        void OtherOpinionSign(string formNO, string confirmer, string opinion);
        /// <summary>
        /// 结果确认人签名
        /// </summary>
        /// <param name="formNO"></param>
        /// <param name="confirmer"></param>
        /// <param name="opinion"></param>
        /// <param name="status"></param>
        void ResultConfirmSign(string formNO, string confirmer,WareHouseFormStatus status);
    }
}
