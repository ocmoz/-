using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;

namespace FM2E.BLL.Utils
{
    public class ConvertIListToDataSet
    {
        
        /// <summary>
        /// Ilist 转换成 DataSet
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataSet ConvertToDataSet(IList i_objlist)
        {
            if (i_objlist == null || i_objlist.Count <= 0)
            {
                return null;
            }
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataRow row;

            PropertyInfo[] myPropertyInfo = i_objlist[0].GetType().GetProperties();

            foreach (PropertyInfo pi in myPropertyInfo)
            {
                if (pi == null)
                {
                    continue;
                }
                dt.Columns.Add(pi.Name, Type.GetType(pi.PropertyType.ToString()));
            }
            for (int j = 0; j < i_objlist.Count; j++)
            {
                row = dt.NewRow();
                for (int i = 0; i < myPropertyInfo.Length; i++)
                {
                    PropertyInfo pi = myPropertyInfo[i];
                    row[pi.Name] = pi.GetValue(i_objlist[j], null);
                }
                dt.Rows.Add(row);
            }
            ds.Tables.Add(dt);
            return ds;
        }



    }
}
