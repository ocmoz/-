using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Utils;
using System.Data.SqlClient;
using System.Data;
using FM2E.Model.Exceptions;

namespace FM2E.SQLServerDAL.Utils
{
    public class EquipmentAssign : IEquipmentIDAssign
    {
        #region IEquipmentIDAssign 成员
        /// <summary>
        /// 获取生成条形码所需的设备ID
        /// </summary>
        /// <param name="companyID">公司编号</param>
        /// <param name="purchaseDate">购买年月</param>
        /// <returns></returns>
        int IEquipmentIDAssign.GetEquipmentID(string companyID, DateTime purchaseDate)
        {
            int equipmentID = -1;
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                StringBuilder strSql = new StringBuilder();
                strSql.Append("select PurchaseDate,[Count] from FM2E_EquipmentIDAssign ");
                strSql.Append("where CompanyID=@CompanyID and PurchaseDate=@PurchaseDate");

                SqlParameter[] parameters = {
                    new SqlParameter("@CompanyID",SqlDbType.VarChar,2),
                    new SqlParameter("@PurchaseDate", SqlDbType.VarChar,10),
                    new SqlParameter("@Count",SqlDbType.Int)};

                parameters[0].Value = companyID;
                parameters[1].Value = purchaseDate.ToString("yyyyMM");
                parameters[2].Value = 0;

                int count = 0;
                string nowDate = purchaseDate.ToString("yyyyMM");
                bool havaRecord = false;
                using (SqlDataReader rd = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.HasRows)
                        havaRecord = true;
                    if (rd.Read())
                    {
                        if (!Convert.IsDBNull(rd["Count"]))
                            count = Convert.ToInt32(rd["Count"]);
                    }
                }

                if (!havaRecord)
                {
                    //不存在相关的表单项，则插入一条记录
                    strSql.Remove(0, strSql.Length);
                    strSql.Append("insert into FM2E_EquipmentIDAssign(CompanyID,PurchaseDate,[Count]) ");
                    strSql.Append(" values(@CompanyID,@PurchaseDate,@Count)");

                    parameters[0].Value = companyID;
                    parameters[1].Value = nowDate;
                    parameters[2].Value = 1;

                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                    equipmentID = 1;

                }
                else
                {
                    //存在相关的项

                    strSql.Remove(0, strSql.Length);
                    strSql.Append("update FM2E_EquipmentIDAssign set PurchaseDate=@PurchaseDate,[Count]=@Count ");
                    strSql.Append(" where CompanyID=@CompanyID and PurchaseDate=@PurchaseDate");
                    parameters[0].Value = companyID;
                    parameters[1].Value = nowDate;
                    parameters[2].Value = ++count;

                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                    //string tmp = count.ToString("0000");
                    //equipmentID = tmp.Substring(tmp.Length - 4);
                    equipmentID = count;
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                equipmentID = -1;
                trans.Rollback();
                throw new DALException("生成设备ID失败"+ex.Message, ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn = null;
                }
            }
            return equipmentID;
        }

        /// <summary>
        /// 获取生成条形码所需的设备ID
        /// </summary>
        /// <param name="companyID">公司编号</param>
        /// <param name="purchaseDate">购买年月</param>
        /// <param name="num">需要生成的设备ID数量</param>
        /// <returns></returns>
        int IEquipmentIDAssign.GetEquipmentID(string companyID, DateTime purchaseDate, int num)
        {
            int equipmentID = -1;
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                StringBuilder strSql = new StringBuilder();
                strSql.Append("select PurchaseDate,[Count] from FM2E_EquipmentIDAssign ");
                strSql.Append("where CompanyID=@CompanyID and PurchaseDate=@PurchaseDate");

                SqlParameter[] parameters = {
                    new SqlParameter("@CompanyID",SqlDbType.VarChar,2),
                    new SqlParameter("@PurchaseDate", SqlDbType.VarChar,10),
                    new SqlParameter("@Count",SqlDbType.Int)};

                parameters[0].Value = companyID;
                parameters[1].Value = purchaseDate.ToString("yyyyMM");
                parameters[2].Value = 0;

                int count = 0;
                string nowDate = purchaseDate.ToString("yyyyMM");
                bool havaRecord = false;
                using (SqlDataReader rd = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.HasRows)
                        havaRecord = true;
                    if (rd.Read())
                    {
                        if (!Convert.IsDBNull(rd["Count"]))
                            count = Convert.ToInt32(rd["Count"]);
                    }
                }

                if (!havaRecord)
                {
                    //不存在相关的表单项，则插入一条记录
                    strSql.Remove(0, strSql.Length);
                    strSql.Append("insert into FM2E_EquipmentIDAssign(CompanyID,PurchaseDate,[Count]) ");
                    strSql.Append(" values(@CompanyID,@PurchaseDate,@Count)");

                    parameters[0].Value = companyID;
                    parameters[1].Value = nowDate;
                    parameters[2].Value = num;

                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                    equipmentID = 1;
                }
                else
                {
                    //存在相关的项

                    strSql.Remove(0, strSql.Length);
                    strSql.Append("update FM2E_EquipmentIDAssign set PurchaseDate=@PurchaseDate,[Count]=@Count ");
                    strSql.Append(" where CompanyID=@CompanyID and PurchaseDate=@PurchaseDate");
                    parameters[0].Value = companyID;
                    parameters[1].Value = nowDate;
                    parameters[2].Value = count + num;

                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                    equipmentID = count + 1;
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                equipmentID = -1;
                trans.Rollback();
                throw new DALException("生成设备ID失败", ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn = null;
                }
            }
            return equipmentID;
        }
        #endregion
    }
}
