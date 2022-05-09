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
    public class UStatus:IUStatus
    {
        private const string SELECT_ALLUSTATUS = "select * from FM2E_UsingStatus where [IsDeleted]=0";

        public IList<UStatusInfo> GetAllUStatus()
        {
            List<UStatusInfo> list = new List<UStatusInfo>();

            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, SELECT_ALLUSTATUS, null))
                {
                    while (rd.Read())
                    {
                        UStatusInfo item = new UStatusInfo();
                        if (!Convert.IsDBNull(rd["UStatusID"]))
                            item.UStatusID = Convert.ToString(rd["UStatusID"]);

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
