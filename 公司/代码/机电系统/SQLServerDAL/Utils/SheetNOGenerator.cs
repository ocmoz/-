﻿using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Utils;
using System.Data.SqlClient;
using FM2E.Model.Exceptions;
using System.Data;

namespace FM2E.SQLServerDAL.Utils
{
    /// <summary>
    /// 用于分配表单编号,格式为 公司编号YYYYMMDD-本天第几张单
    /// </summary>
    public class SheetNOGenerator:ISheetNO
    {
        #region ISheetNO 成员
        /// <summary>
        /// 分配表单编号
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="sheetType"></param>
        /// <returns></returns>
        string ISheetNO.GetSheetNO(string companyID, FM2E.Model.Utils.SheetType sheetType)
        {
            string sheetNO = "";
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                StringBuilder strSql = new StringBuilder();
                strSql.Append("select Day,[Count] from FM2E_SheetNOAssign ");
                strSql.Append("where SheetType=@SheetType and CompanyID=@CompanyID");

                SqlParameter[] parameters = {
					new SqlParameter("@SheetType", SqlDbType.VarChar,100),
                    new SqlParameter("@CompanyID",SqlDbType.VarChar,2),
                    new SqlParameter("@Day", SqlDbType.VarChar,10),
                    new SqlParameter("@Count",SqlDbType.Int),                        
                                            };
                parameters[0].Value = sheetType.ToString();
                parameters[1].Value = companyID;
                parameters[2].Value = "";
                parameters[3].Value = 0;

                int count = 0;
                string date = "";
                string nowDate = DateTime.Now.ToString("yyMMddHH");
                bool havaRecord = false;
                using (SqlDataReader rd = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.HasRows)
                        havaRecord = true;
                    if (rd.Read())
                    {
                        if (!Convert.IsDBNull(rd["Day"]))
                        {
                            date = Convert.ToString(rd["Day"]);
                        }

                        if (!Convert.IsDBNull(rd["Count"]))
                            count = Convert.ToInt32(rd["Count"]);
                    }
                }

                if (!havaRecord)
                {
                    //不存在相关的表单项，则插入一条记录
                    strSql.Remove(0, strSql.Length);
                    strSql.Append("insert into FM2E_SheetNOAssign(SheetType,CompanyID,Day,[Count]) ");
                    strSql.Append(" values(@SheetType,@CompanyID,@Day,@Count)");

                    parameters[0].Value = sheetType.ToString();
                    parameters[1].Value = companyID;
                    parameters[2].Value = nowDate;
                    parameters[3].Value = 2;

                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                    //2012-12 L 深高速定制
                    //sheetNO = companyID + nowDate + "-01";
                    sheetNO = companyID + nowDate + "01";
                }
                else
                {
                    //存在相关的项
                    if (date != nowDate)
                    {
                        //不是当天的记录，则需要更新
                        count = 1;
                    }

                    strSql.Remove(0, strSql.Length);
                    strSql.Append("update FM2E_SheetNOAssign set Day=@Day,Count=@Count ");
                    strSql.Append(" where SheetType=@SheetType and CompanyID=@CompanyID ");
                    parameters[0].Value = sheetType.ToString();
                    parameters[1].Value = companyID;
                    parameters[2].Value = nowDate;
                    parameters[3].Value = count + 1;

                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                    string tmp = count.ToString();
                    if (0 <= count && count < 10)
                    {
                        tmp = count.ToString("00");
                    }
                    sheetNO = companyID + nowDate + tmp;
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                sheetNO = "";
                trans.Rollback();
                throw new DALException("生成表单编号失败", ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn = null;
                }
            }
            return sheetNO;
        }

        /// <summary>
        /// 把数字转化为36进制的字符串表示
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertTo36String(int value)
        {
            string result = "";
            int remainder = value % 36;
            int commerce = value / 36;
            result += IntToLetter(remainder);
            while (commerce != 0)
            {
                remainder = commerce % 36;
                result = IntToLetter(remainder) + result;
                commerce /= 36;
            }
            if (result.Length % 2 != 0)
                result = "0" + result;
            return result;
        }
        /// <summary>
        /// 数字转换成字母
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static string IntToLetter(int num)
        {
            string result = "";
            if (num < 10)
                result = num.ToString();
            else
            {
                string startLetter = "A";
                int tmp = Encoding.ASCII.GetBytes(startLetter)[0];
                tmp = tmp + (num - 10);
                result += (char)tmp;
            }

            return result;
        }

        #endregion
    }
}
