using System;
using System.Collections;
using System.Text;

using FM2E.IDAL.Basic;
using FM2E.Model.Basic;
using System.Collections.Generic;

namespace FM2E.BLL.Basic
{
    public class Position
    {
        public IList<PositionInfo> GetAllPosition()
        {
            IPosition dal = FM2E.DALFactory.BasicAccess.CreatePosition();
            return dal.GetAllPosition();
        }

        public IList GetPositionByPage(int pageIndex, int pageSize, out int recordCount)
        {
            IPosition dal = FM2E.DALFactory.BasicAccess.CreatePosition();
            return dal.GetPositionByPage(pageIndex, pageSize, out recordCount);
        }

        public PositionInfo GetPosition(long id)
        {
            IPosition dal = FM2E.DALFactory.BasicAccess.CreatePosition();
            return dal.GetPosition(id);
        }


        public void InsertPosition(PositionInfo item)
        {
            IPosition dal = FM2E.DALFactory.BasicAccess.CreatePosition();
            dal.InsertPosition(item);
        }

        public void UpdatePosition(PositionInfo item)
        {
            IPosition dal = FM2E.DALFactory.BasicAccess.CreatePosition();
            dal.UpdatePosition(item);
        }

        public void DelPosition(long id)
        {
            IPosition dal = FM2E.DALFactory.BasicAccess.CreatePosition();
            dal.DelPosition(id);
        }

        /// <summary>
        /// 判断职位是否已经存在
        /// </summary>
        /// <param name="positionname"></param>
        /// <returns></returns>
        public bool IfExists(string positionname)
        {
            IPosition dal = FM2E.DALFactory.BasicAccess.CreatePosition();
            return dal.IfExists(positionname);
        }

    }
}
