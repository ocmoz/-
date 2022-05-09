using System;
using System.Collections;
using System.Text;
using FM2E.Model.Basic;
using System.Collections.Generic;

namespace FM2E.IDAL.Basic
{
    public interface IPosition
    {
        IList<PositionInfo> GetAllPosition();
        IList GetPositionByPage(int pageIndex, int pageSize, out int recordCount);
        PositionInfo GetPosition(long id);

        void InsertPosition(PositionInfo item);
        void UpdatePosition(PositionInfo item);
        void DelPosition(long id);
        /// <summary>
        /// 判断职位是否已经存在
        /// </summary>
        /// <param name="positionname"></param>
        /// <returns></returns>
        bool IfExists(string positionname);
    }
}
