using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Equipment;
using FM2E.IDAL.Equipment;
using FM2E.Model.Utils;
using System.Collections;

namespace FM2E.BLL.Equipment
{
    public class InEquipments
    {
        public InEquipmentsInfo GetInEquipments(long ID, long ItemID)
        {
            IInEquipments dal = FM2E.DALFactory.EquipmentAccess.CreateInEquipments();
            return dal.GetInEquipments(ID, ItemID);
        }
        public void InsertInEquipments(InEquipmentsInfo model)
        {
            IInEquipments dal = FM2E.DALFactory.EquipmentAccess.CreateInEquipments();
            dal.InsertInEquipments(model);
        }
        public void UpdateInEquipments(InEquipmentsInfo model)
        {
            IInEquipments dal = FM2E.DALFactory.EquipmentAccess.CreateInEquipments();
            dal.UpdateInEquipments(model);
        }
        public void DelInEquipments(long ID, long ItemID)
        {
            IInEquipments dal = FM2E.DALFactory.EquipmentAccess.CreateInEquipments();
            dal.DelInEquipments(ID, ItemID);
        }
        public QueryParam GenerateSearchTerm(InEquipmentsInfo item)
        {
            IInEquipments dal = FM2E.DALFactory.EquipmentAccess.CreateInEquipments();
            return dal.GenerateSearchTerm(item);
        }
        public IList GetList(QueryParam searchTerm, out int recordCount)
        {
            IInEquipments dal = FM2E.DALFactory.EquipmentAccess.CreateInEquipments();
            return dal.GetList(searchTerm, out recordCount);
        }

        public IList SearchRecord(InEquipmentsInfo info, int pageIndex, int pageSize, out int recordCount)
        {
            IInEquipments dal = FM2E.DALFactory.EquipmentAccess.CreateInEquipments();
            QueryParam qp = dal.GenerateSearchTerm(info);
            qp.PageIndex = pageIndex;
            qp.PageSize = pageSize;
            return dal.GetList(qp, out recordCount);
        }

        public IList SearchRecordForOut(InEquipmentsInfo info, int pageIndex, int pageSize, out int recordCount)
        {
            IInEquipments dal = FM2E.DALFactory.EquipmentAccess.CreateInEquipments();
            QueryParam qp = dal.GenerateSearchTermForOut(info);
            qp.PageIndex = pageIndex;
            qp.PageSize = pageSize;
            return dal.GetListForOut(qp, out recordCount);
        }
    }
}
