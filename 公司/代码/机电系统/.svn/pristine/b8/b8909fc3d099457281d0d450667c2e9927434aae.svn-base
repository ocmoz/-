using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Utils;
using System.Data.Common;
using System.Data.SqlClient;

namespace FM2E.SQLServerDAL.Utils
{
    public class Transaction:ITransaction
    {
        #region ITransaction 成员

        DbTransaction ITransaction.GetTransaction()
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();
            }
            catch
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
                throw;
            }

            return trans;
        }

        /// <summary>
        /// 关闭一个事务对象
        /// </summary>
        /// <param name="trans"></param>
        void ITransaction.CloseTransaction(DbTransaction _trans)
        {
            SqlTransaction trans = _trans as SqlTransaction;
            if (trans == null)
                return;
            
            //关闭连接
            SqlConnection conn = trans.Connection;
            if (trans != null)
            {
                trans.Dispose();
                trans = null;
            }
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        #endregion
    }
}
