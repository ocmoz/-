using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FM2E.Model.Utils;
using FM2E.Model.Equipment;
using FM2E.IDAL.Equipment;

namespace FM2E.BLL.Equipment
{
    public class WareHouseCheck
    {
        /// <summary>
        /// 获取所有的仓库检查单
        /// </summary>
        /// <returns></returns>
        public IList GetAllWareHouseCheck()
        {
            IWareHouseCheck dal = FM2E.DALFactory.EquipmentAccess.CreateWareHouseCheck();
            return dal.GetAllWareHouseCheck();
        }
        /// <summary>
        /// 获取符合条件的仓库检查单列表（支持分页）
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetWareHouseCheckList(QueryParam term, out int recordCount)
        {
            IWareHouseCheck dal = FM2E.DALFactory.EquipmentAccess.CreateWareHouseCheck();
            return dal.GetWareHouseCheckList(term, out recordCount);
        }
        /// <summary>
        /// 根据表单编号获取仓库检查单
        /// </summary>
        /// <param name="formNO"></param>
        /// <returns></returns>
        public WareHouseCheckInfo GetWareHouseCheck(string formNO)
        {
            IWareHouseCheck dal = FM2E.DALFactory.EquipmentAccess.CreateWareHouseCheck();
            return dal.GetWareHouseCheck(formNO);
        }
        /// <summary>
        /// 添加仓库检查单
        /// </summary>
        /// <param name="item"></param>
        public void AddWareHouseCheck(WareHouseCheckInfo item)
        {
            IWareHouseCheck dal = FM2E.DALFactory.EquipmentAccess.CreateWareHouseCheck();
            dal.AddWareHouseCheck(item);
        }
        /// <summary>
        /// 更新仓库检查单
        /// </summary>
        /// <param name="item"></param>
        public void UpdateWareHouseCheck(WareHouseCheckInfo item)
        {
            IWareHouseCheck dal = FM2E.DALFactory.EquipmentAccess.CreateWareHouseCheck();
            dal.UpdateWareHouseCheck(item);
        }
        /// <summary>
        /// 删除仓库检查单
        /// </summary>
        /// <param name="formNO"></param>
        public void DeleteWareHouseCheck(string formNO)
        {
            IWareHouseCheck dal = FM2E.DALFactory.EquipmentAccess.CreateWareHouseCheck();
            dal.DeleteWareHouseCheck(formNO);
        }
        /// <summary>
        /// 生成检查单查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public QueryParam GenerateSearchTerm(WareHouseCheckSearchInfo term)
        {
            IWareHouseCheck dal = FM2E.DALFactory.EquipmentAccess.CreateWareHouseCheck();
            return dal.GenerateSearchTerm(term);
        }
        /// <summary>
        /// 数量情况确认人签名
        /// </summary>
        /// <param name="formNO"></param>
        /// <param name="confirmer"></param>
        /// <param name="opinion"></param>
        public void QuantitySign(string formNO, string confirmer, string opinion)
        {
            IWareHouseCheck dal = FM2E.DALFactory.EquipmentAccess.CreateWareHouseCheck();
            dal.QuantitySign(formNO, confirmer, opinion);
        }
        /// <summary>
        /// 质量情况确认人签名
        /// </summary>
        /// <param name="formNO"></param>
        /// <param name="confirmer"></param>
        /// <param name="opinion"></param>
        public void QualitySign(string formNO, string confirmer, string opinion)
        {
            IWareHouseCheck dal = FM2E.DALFactory.EquipmentAccess.CreateWareHouseCheck();
            dal.QualitySign(formNO, confirmer, opinion);
        }
        /// <summary>
        /// 表单登记情况确认人签名
        /// </summary>
        /// <param name="formNO"></param>
        /// <param name="confirmer"></param>
        /// <param name="opinion"></param>
        public void RegistrationSign(string formNO, string confirmer, string opinion)
        {
            IWareHouseCheck dal = FM2E.DALFactory.EquipmentAccess.CreateWareHouseCheck();
            dal.RegistrationSign(formNO, confirmer, opinion);
        }
        /// <summary>
        /// 其它意见确认人签名
        /// </summary>
        /// <param name="formNO"></param>
        /// <param name="confirmer"></param>
        /// <param name="opinion"></param>
        public void OtherOpinionSign(string formNO, string confirmer, string opinion)
        {
            IWareHouseCheck dal = FM2E.DALFactory.EquipmentAccess.CreateWareHouseCheck();
            dal.OtherOpinionSign(formNO, confirmer, opinion);
        }
        /// <summary>
        /// 结果确认人签名
        /// </summary>
        /// <param name="formNO"></param>
        /// <param name="confirmer"></param>
        /// <param name="opinion"></param>
        /// <param name="status"></param>
        public void ResultConfirmSign(string formNO, string confirmer, WareHouseFormStatus status)
        {
            IWareHouseCheck dal = FM2E.DALFactory.EquipmentAccess.CreateWareHouseCheck();
            dal.ResultConfirmSign(formNO, confirmer,status);
        }
    }
}
