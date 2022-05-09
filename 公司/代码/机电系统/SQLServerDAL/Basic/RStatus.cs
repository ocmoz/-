using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;
using FM2E.IDAL.Basic;
using FM2E.Model.Basic;

namespace FM2E.SQLServerDAL.Basic
{
    public class RStatus : IRStatus
    {
        private const string SELECT_ALLRSTATUS = "select * from FM2E_RunningStatus where [IsDeleted]=0";

        public IList<RStatusInfo> GetAllRStatus()
        {
            List<RStatusInfo> list = new List<RStatusInfo>();

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, SELECT_ALLRSTATUS, null))
                {
                    while (rd.Read())
                    {
                        RStatusInfo item = new RStatusInfo();
                        if (!Convert.IsDBNull(rd["RStatusID"]))
                            item.RStatusID = Convert.ToString(rd["RStatusID"]);

                        if (!Convert.IsDBNull(rd["StatusName"]))
                            item.StatusName = Convert.ToString(rd["StatusName"]);

                        if (!Convert.IsDBNull(rd["IsDeleted"]))
                            item.IsDeleted = Convert.ToBoolean(rd["IsDeleted"]);

                        list.Add(item);
                    }
                }
            }
            catch
            {
                throw;
            }
            return list;
        }
    }
}
