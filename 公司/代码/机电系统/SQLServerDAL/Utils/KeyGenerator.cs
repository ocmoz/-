using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.Data.Common;


namespace FM2E.SQLServerDAL.Utils
{
    public class KeyGenerator
    {
        private IDbConnection connection = null;
        private string tableName;
        private int incrementBy;
        private long nextID = 0;
        private long maxID = 0;

        public KeyGenerator(IDbConnection connection, string tableName, int incrementBy)
        {
            this.connection = connection;
            this.tableName = tableName;
            this.incrementBy = incrementBy;
        }

        public long NextKey
        {
            get
            {
                if (nextID == maxID)
                {
                    reserveIds();
                }
                return nextID++;
            }
        }

        private void reserveIds()
        {
            IDbTransaction tx = connection.BeginTransaction();
            IDbCommand cmd = connection.CreateCommand();
            cmd.Transaction = tx;
            try
            {
                cmd.CommandText = string.Format(
                    "update FM2E_Keys set [NextID] = [NextID] + {0} where [TableName] = \'{1}\'",
                    incrementBy, tableName);
                int result = cmd.ExecuteNonQuery();
                if (result == 0)  //不存在此项
                {
                    cmd.CommandText = string.Format("insert into FM2E_Keys([TableName],[NextID]) values(\'{0}\',2)", tableName);
                    cmd.ExecuteNonQuery();
                    maxID = 2;
                }
                else
                {
                    cmd.CommandText = string.Format("select NextID from FM2E_Keys where [TableName] = \'{0}\'",
                        tableName);
                    maxID = Convert.ToInt64(cmd.ExecuteScalar());
                }
                nextID = maxID - incrementBy;
                tx.Commit();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw ex;
            }
            finally
            {
                cmd.Dispose();
            }

        }

    }
}
