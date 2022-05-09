using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace FM2E.IDAL.Utils
{
    public interface ITransaction
    {
        /// <summary>
        /// 获取一个事务对象
        /// </summary>
        /// <returns></returns>
        DbTransaction GetTransaction();
        /// <summary>
        /// 关闭一个事务对象
        /// </summary>
        /// <param name="trans"></param>
        void CloseTransaction(DbTransaction _trans);
    }
}
