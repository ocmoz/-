using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Equipment;
using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Equipment
{
    public class CheckAcceptance
    {
        private ICheckAcceptance dal = FM2E.DALFactory.EquipmentAccess.CreateCheckAcceptance();

        public void DeleteForm(long id)
        {
            dal.DeleteCheckAcceptanceInfo(id);
        }

        public long SaveCheckAcceptanceFormWithDetail(CheckAcceptanceInfo item)
        {
            item.UpdateTime = DateTime.Now;
            if (item.ID == 0)//新的
            {
                return dal.InsertCheckAcceptanceWithDetail(item);
            }
            else//更新
            {
                dal.UpdateCheckAcceptanceWithDetail(item);
                return item.ID;
            }
        }

        public long SaveCheckAcceptanceFormWithoutDetail(CheckAcceptanceInfo item)
        {
            item.UpdateTime = DateTime.Now;
            if (item.ID == 0)//新的
            {
                return dal.InsertCheckAcceptanceWithoutDetail(item);
            }
            else//更新
            {
                dal.UpdateCheckAcceptanceNoDetail(item);
                return item.ID;
            }
        }

        public CheckAcceptanceInfo GetCheckAcceptanceInfoAllDetail(long id)
        {
            return dal.GetCheckAcceptanceInfoWithAllDetail(id);
        }

        public IList SearchCheckAcceptanceForm(CheckAcceptanceSearchInfo info, int currentPageIndex, int pageSize, out int recordCount)
        {
            QueryParam p = dal.GenerateQueryItem(info);
            p.PageIndex = currentPageIndex;
            p.PageSize = pageSize;
            return dal.SearchCheckAcceptanceForm(p, out recordCount);
        }
        public IList SearchCheckAcceptanceForm(QueryParam p, out int recordCount)
        {            
            return dal.SearchCheckAcceptanceForm(p, out recordCount);
        }
        public void UpdateCheckAcceptanceDetail(CheckAcceptanceDetailInfo detail)
        {
            dal.UpdateCheckAcceptanceDetail(detail);
        }

        public void InsertBarcode(CheckAcceptanceDetailBarcodeInfo bc)
        {
            dal.InsertBarcodeRecord(bc);
        }

        public CheckAcceptanceDetailInfo GetCheckAcceptanceDetailInfo(long id,short itemid)
        {
            return dal.GetCheckAcceptanceDetailInfo(id, itemid);
        }
    }
}
