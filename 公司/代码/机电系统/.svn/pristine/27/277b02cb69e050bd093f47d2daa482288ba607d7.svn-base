using System;
using System.Collections.Generic;
using System.Text;
using FM2E.IDAL.Maintain;
using System.Collections;
using FM2E.Model.Maintain;
using FM2E.Model.Exceptions;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;
using System.Data;
using FM2E.Model.Utils;
using System.Data.SqlTypes;
using System.Data.Common;
using FM2E.SQLServerDAL.Basic;
using FM2E.Model.Basic;

namespace FM2E.SQLServerDAL.Maintain
{
    public class EquipmentCost : IEquipmentCost
    {
        private const string TABLEEquipmentCost = "FM2E_MalfunctionEquipmentCost";
        private const string TABLEEquipmentCostItems = "FM2E_MalfunctionEquipmentCostItems";

        private EquipmentCostInfor GetCostData(IDataReader dr)
        {
            EquipmentCostInfor item =new EquipmentCostInfor ();

            if (!Convert.IsDBNull(dr["ID"]))
                item.ID = Convert.ToInt32(dr["ID"]);
            if (!Convert.IsDBNull(dr["SheetID"]))
                item.SheetID = Convert.ToInt32(dr["SheetID"]);
            if (!Convert.IsDBNull(dr["EqSumPrice"]))
                item.EqSumPrice = Convert.ToDecimal(dr["EqSumPrice"]);
            if (!Convert.IsDBNull(dr["EqSumApprovalPrice"]))
                item.EqSumApprovalPrice = Convert.ToDecimal(dr["EqSumApprovalPrice"]);
            if (!Convert.IsDBNull(dr["MeasureCost"]))
                item.MeasureCost = Convert.ToDecimal(dr["MeasureCost"]); 
            if (!Convert.IsDBNull(dr["GuiCost"]))
                item.GuiCost = Convert.ToDecimal(dr["GuiCost"]);
            if (!Convert.IsDBNull(dr["TaxCost"]))
                item.TaxCost = Convert.ToDecimal(dr["TaxCost"]);
            if (!Convert.IsDBNull(dr["TrafficCost"]))
                item.TrafficCost = Convert.ToDecimal(dr["TrafficCost"]);
            if (!Convert.IsDBNull(dr["MeasureApprovalCost"]))
                item.MeasureApprovalCost = Convert.ToDecimal(dr["MeasureApprovalCost"]);
            if (!Convert.IsDBNull(dr["GuiApprovalCost"]))
                item.GuiApprovalCost = Convert.ToDecimal(dr["GuiApprovalCost"]);
            if (!Convert.IsDBNull(dr["TaxApprovalCost"]))
                item.TaxApprovalCost = Convert.ToDecimal(dr["TaxApprovalCost"]);
            if (!Convert.IsDBNull(dr["TrafficApprovalCost"]))
                item.TrafficApprovalCost = Convert.ToDecimal(dr["TrafficApprovalCost"]);
            if (!Convert.IsDBNull(dr["SumOtherCost"]))
                item.SumOtherCost = Convert.ToDecimal(dr["SumOtherCost"]);
            if (!Convert.IsDBNull(dr["SumApprovalOtherCost"]))
                item.SumApprovalOtherCost = Convert.ToDecimal(dr["SumApprovalOtherCost"]);
            if (!Convert.IsDBNull(dr["TotalSumCost"]))
                item.TotalSumCost = Convert.ToDecimal(dr["TotalSumCost"]);
            if (!Convert.IsDBNull(dr["TotalSumApprovalCost"]))
                item.TotalSumApprovalCost = Convert.ToDecimal(dr["TotalSumApprovalCost"]);
            if (!Convert.IsDBNull(dr["IsMeasure"]))
                item.IsMeasure = Convert.ToString(dr["IsMeasure"]);
            if (!Convert.IsDBNull(dr["OtherCost"]))
                item.OtherCost = Convert.ToDecimal(dr["OtherCost"]);
            if (!Convert.IsDBNull(dr["MarkOne"]))
                item.MarkOne = Convert.ToString(dr["MarkOne"]);
            if (!Convert.IsDBNull(dr["MarkTwo"]))
                item.MarkTwo = Convert.ToString(dr["MarkTwo"]);
            if (!Convert.IsDBNull(dr["MarkThree"]))
                item.MarkThree = Convert.ToString(dr["MarkThree"]);
            if (!Convert.IsDBNull(dr["MarkFour"]))
                item.MarkFour = Convert.ToString(dr["MarkFour"]);
            if (!Convert.IsDBNull(dr["MarkFive"]))
                item.MarkFive = Convert.ToString(dr["MarkFive"]);
            //if (!Convert.IsDBNull(dr["IsMeasure"]))
            //    item.IsMeasure = Convert.ToString(dr["IsMeasure"]);
            if (!Convert.IsDBNull(dr["OtherApprovalCost"]))
                item.OtherApprovalCost = Convert.ToDecimal(dr["OtherApprovalCost"]);
            //if (!Convert.IsDBNull(dr["MeasureCostMark"]))B
                //item.MeasureCostMark = Convert.ToString(dr["MeasureCostMark"]);//  [3/30/2012 Administrator]
            if (!Convert.IsDBNull(dr["IsApplyMeasure"]))
                item.IsApplyMeasure = Convert.ToString(dr["IsApplyMeasure"]);
            if (!Convert.IsDBNull(dr["IsProvider"]))
                item.IsProvider = Convert.ToString(dr["IsProvider"]);
            return item;
        }

        private EquipmentCostItems GetCostItemsData(IDataReader dr)
        {
            EquipmentCostItems item = new EquipmentCostItems ();

            if (!Convert.IsDBNull(dr["ID"]))
                item.ID = Convert.ToInt32(dr["ID"]);
            if (!Convert.IsDBNull(dr["CostID"]))
                item.CostID = Convert.ToInt32(dr["CostID"]);
            if (!Convert.IsDBNull(dr["EqName"]))
                item.EqName = Convert.ToString(dr["EqName"]);
            if (!Convert.IsDBNull(dr["EqModel"]))
                item.EqModel = Convert.ToString(dr["EqModel"]);
            if (!Convert.IsDBNull(dr["EqUnit"]))
                item.EqUnit = Convert.ToString(dr["EqUnit"]);
            if (!Convert.IsDBNull(dr["EqNum"]))
                item.EqNum = Convert.ToInt32(dr["EqNum"]);
            if (!Convert.IsDBNull(dr["EqUnitPrice"]))
                item.EqUnitPrice = Convert.ToDecimal(dr["EqUnitPrice"]);
            if (!Convert.IsDBNull(dr["EqTotalPrice"]))
                item.EqTotalPrice = Convert.ToDecimal(dr["EqTotalPrice"]);
            if (!Convert.IsDBNull(dr["EqApprovalUnitPrice"]))
                item.EqApprovalUnitPrice = Convert.ToDecimal(dr["EqApprovalUnitPrice"]);
            if (!Convert.IsDBNull(dr["EqApprovalTotalPrice"]))
                item.EqApprovalTotalPrice = Convert.ToDecimal(dr["EqApprovalTotalPrice"]);
            if (!Convert.IsDBNull(dr["EqRemark"]))
                item.EqRemark = Convert.ToString(dr["EqRemark"]);
            
            return item;
        }


        #region IEquipmentCost 成员

        public EquipmentCostInfor GetEquipmentCostInforBySheetID(long sheetID)
        {
            EquipmentCostInfor eci = null;
            try
            {
                string sql = "select * from "+TABLEEquipmentCost+" where SheetID=@SheetID";
                SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.BigInt)};
                parameters[0].Value = sheetID;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        eci = GetCostData(rd);
                    }
                }
                if(eci!=null)
                    eci.EqCostItems = GetEquipmentCostItemsByCostID(eci.ID);
            }
            catch (Exception ex)
            {
                throw new DALException("获取故障单费用表失败", ex);
            }
            return eci;
        }

        public EquipmentCostInfor GetEquipmentCostInforByID(long ID)
        {
            EquipmentCostInfor eci = null;
            try
            {
                string sql = "select * from " + TABLEEquipmentCost + " where ID=@ID";
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                parameters[0].Value = ID;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        eci = GetCostData(rd);
                    }
                }
                if (eci != null)
                    eci.EqCostItems = GetEquipmentCostItemsByCostID(eci.ID);
            }
            catch (Exception ex)
            {
                throw new DALException("获取故障单费用表失败", ex);
            }
            return eci;
        }

        public List<EquipmentCostItems> GetEquipmentCostItemsByCostID(long costID)
        {
            List<EquipmentCostItems> eciList = new List<EquipmentCostItems>();
            try
            {
                string sql = "select * from "+TABLEEquipmentCostItems+" where CostID=@CostID";
                SqlParameter[] parameters = {
					new SqlParameter("@CostID", SqlDbType.BigInt)};
                parameters[0].Value = costID;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        EquipmentCostItems item = GetCostItemsData(rd);
                        eciList.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                eciList.Clear();
                throw new DALException("获取故障单设备费用失败", ex);
            }
            return eciList;
        }

        public void AddEquipmentCostInfor(EquipmentCostInfor item)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+TABLEEquipmentCost+"(");
            strSql.Append("SheetID,EqSumPrice,EqSumApprovalPrice,MeasureCost,GuiCost,TaxCost,TrafficCost,MeasureApprovalCost,GuiApprovalCost,TaxApprovalCost,TrafficApprovalCost,SumOtherCost,SumApprovalOtherCost,TotalSumCost,TotalSumApprovalCost,IsMeasure,OtherCost,MarkOne,MarkTwo,MarkThree,MarkFour,MarkFive,OtherApprovalCost,IsApplyMeasure,IsProvider)");
            strSql.Append(" values (");
            strSql.Append("@SheetID,@EqSumPrice,@EqSumApprovalPrice,@MeasureCost,@GuiCost,@TaxCost,@TrafficCost,@MeasureApprovalCost,@GuiApprovalCost,@TaxApprovalCost,@TrafficApprovalCost,@SumOtherCost,@SumApprovalOtherCost,@TotalSumCost,@TotalSumApprovalCost,@IsMeasure,@OtherCost,@MarkOne,@MarkTwo,@MarkThree,@MarkFour,@MarkFive,@OtherApprovalCost,@IsApplyMeasure,@IsProvider)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.BigInt,8),
                    new SqlParameter("@EqSumPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@EqSumApprovalPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@MeasureCost", SqlDbType.Decimal,9),
                    new SqlParameter("@GuiCost", SqlDbType.Decimal,9),
                    new SqlParameter("@TaxCost", SqlDbType.Decimal,9),
                    new SqlParameter("@TrafficCost", SqlDbType.Decimal,9),
                    new SqlParameter("@MeasureApprovalCost", SqlDbType.Decimal,9),
                    new SqlParameter("@GuiApprovalCost", SqlDbType.Decimal,9),
                    new SqlParameter("@TaxApprovalCost", SqlDbType.Decimal,9),
                    new SqlParameter("@TrafficApprovalCost", SqlDbType.Decimal,9),
                    new SqlParameter("@SumOtherCost", SqlDbType.Decimal,9),
                    new SqlParameter("@SumApprovalOtherCost", SqlDbType.Decimal,9),
                    new SqlParameter("@TotalSumCost", SqlDbType.Decimal,9),
                    new SqlParameter("@TotalSumApprovalCost", SqlDbType.Decimal,9),
					new SqlParameter("@IsMeasure", SqlDbType.NVarChar,50),
					new SqlParameter("@OtherCost", SqlDbType.Decimal,9),
					new SqlParameter("@MarkOne", SqlDbType.NVarChar,50),
					new SqlParameter("@MarkTwo", SqlDbType.NVarChar,50),
					new SqlParameter("@MarkThree", SqlDbType.NVarChar,50),
					new SqlParameter("@MarkFour", SqlDbType.NVarChar,50),
					new SqlParameter("@MarkFive", SqlDbType.NVarChar,50),
					new SqlParameter("@OtherApprovalCost", SqlDbType.Decimal,9),
                    new SqlParameter("@IsApplyMeasure", SqlDbType.NVarChar,50),
                    new SqlParameter("@IsProvider", SqlDbType.NVarChar,50),
                   // new SqlParameter("@MeasureCostMark", SqlDbType.NVarChar,50)}; //3/30/2012 Administrator]
                                        };
            parameters[0].Value = item.SheetID;
            parameters[1].Value = item.EqSumPrice;
            parameters[2].Value = item.EqSumApprovalPrice;
            parameters[3].Value = item.MeasureCost;
            parameters[4].Value = item.GuiCost;
            parameters[5].Value = item.TaxCost;
            parameters[6].Value = item.TrafficCost;
            parameters[7].Value = item.MeasureApprovalCost;
            parameters[8].Value = item.GuiApprovalCost;
            parameters[9].Value = item.TaxApprovalCost;
            parameters[10].Value = item.TrafficApprovalCost;
            parameters[11].Value = item.SumOtherCost;
            parameters[12].Value = item.SumApprovalOtherCost;
            parameters[13].Value = item.TotalSumCost;
            parameters[14].Value = item.TotalSumApprovalCost;
            parameters[15].Value = string.IsNullOrEmpty(item.IsMeasure) ? "" : item.IsMeasure;
            parameters[16].Value = item.OtherCost;
            parameters[17].Value = string.IsNullOrEmpty(item.MarkOne) ? SqlString.Null : item.MarkOne;
            parameters[18].Value = string.IsNullOrEmpty(item.MarkTwo) ? SqlString.Null : item.MarkTwo;
            parameters[19].Value = string.IsNullOrEmpty(item.MarkThree) ? SqlString.Null : item.MarkThree;
            parameters[20].Value = string.IsNullOrEmpty(item.MarkFour) ? SqlString.Null : item.MarkFour;
            parameters[21].Value = string.IsNullOrEmpty(item.MarkFive) ? SqlString.Null : item.MarkFive;
            parameters[22].Value = item.OtherApprovalCost;
            parameters[23].Value = string.IsNullOrEmpty(item.IsApplyMeasure) ? SqlString.Null : item.IsApplyMeasure; 
            parameters[24].Value = string.IsNullOrEmpty(item.IsProvider) ? SqlString.Null : item.IsProvider; 


            //parameters[15].Value = item.MeasureCostMark;
            long costID = (long)SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            foreach (EquipmentCostItems i in item.EqCostItems)
            {
                if (i != null)
                {
                    i.CostID = costID;
                    AddEquipmentCostItems(i);
                }
            }
        }

        public void UpdateEquipmentCostInfor(EquipmentCostInfor item)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update "+TABLEEquipmentCost+" set ");
            strSql.Append("SheetID=@SheetID,");
            strSql.Append("EqSumPrice=@EqSumPrice,");
            strSql.Append("EqSumApprovalPrice=@EqSumApprovalPrice,");
            strSql.Append("MeasureCost=@MeasureCost,");
            strSql.Append("GuiCost=@GuiCost,");
            strSql.Append("TaxCost=@TaxCost,");
            strSql.Append("TrafficCost=@TrafficCost,");
            strSql.Append("MeasureApprovalCost=@MeasureApprovalCost,");
            strSql.Append("GuiApprovalCost=@GuiApprovalCost,");
            strSql.Append("TaxApprovalCost=@TaxApprovalCost,");
            strSql.Append("TrafficApprovalCost=@TrafficApprovalCost,");
            strSql.Append("SumOtherCost=@SumOtherCost,");
            strSql.Append("SumApprovalOtherCost=@SumApprovalOtherCost,");
            strSql.Append("TotalSumCost=@TotalSumCost,");
            strSql.Append("TotalSumApprovalCost=@TotalSumApprovalCost,");
            strSql.Append("IsMeasure=@IsMeasure,");
            strSql.Append("OtherCost=@OtherCost,");
            strSql.Append("MarkOne=@MarkOne,");
            strSql.Append("MarkTwo=@MarkTwo,");
            strSql.Append("MarkThree=@MarkThree,");
            strSql.Append("MarkFour=@MarkFour,");
            strSql.Append("MarkFive=@MarkFive,");
            strSql.Append("OtherApprovalCost=@OtherApprovalCost,");
            strSql.Append("IsApplyMeasure=@IsApplyMeasure,");
            strSql.Append("IsProvider=@IsProvider");

            //strSql.Append("MeasureCostMark=@MeasureCostMark");//  [3/30/2012 Administrator]

            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.BigInt,8),
                    new SqlParameter("@EqSumPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@EqSumApprovalPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@MeasureCost", SqlDbType.Decimal,9),                    
                    new SqlParameter("@GuiCost", SqlDbType.Decimal,9),
                    new SqlParameter("@TaxCost", SqlDbType.Decimal,9),
                    new SqlParameter("@TrafficCost", SqlDbType.Decimal,9),
                    new SqlParameter("@MeasureApprovalCost", SqlDbType.Decimal,9),
                    new SqlParameter("@GuiApprovalCost", SqlDbType.Decimal,9),
                    new SqlParameter("@TaxApprovalCost", SqlDbType.Decimal,9),
                    new SqlParameter("@TrafficApprovalCost", SqlDbType.Decimal,9),
                    new SqlParameter("@SumOtherCost", SqlDbType.Decimal,9),
                    new SqlParameter("@SumApprovalOtherCost", SqlDbType.Decimal,9),
                    new SqlParameter("@TotalSumCost", SqlDbType.Decimal,9),
                    new SqlParameter("@TotalSumApprovalCost", SqlDbType.Decimal,9),
                    new SqlParameter("@ID", SqlDbType.BigInt,8),
                    new SqlParameter("@IsMeasure", SqlDbType.NVarChar,50),
                    new SqlParameter("@OtherCost", SqlDbType.Decimal,9),
                    new SqlParameter("@MarkOne", SqlDbType.NVarChar,50),
                    new SqlParameter("@MarkTwo", SqlDbType.NVarChar,50),
                    new SqlParameter("@MarkThree", SqlDbType.NVarChar,50),
                    new SqlParameter("@MarkFour", SqlDbType.NVarChar,50),
                    new SqlParameter("@MarkFive", SqlDbType.NVarChar,50),
                    new SqlParameter("@OtherApprovalCost", SqlDbType.Decimal,9),
                    new SqlParameter("@IsApplyMeasure", SqlDbType.NVarChar,50),
                    new SqlParameter("@IsProvider", SqlDbType.NVarChar,50),


                   // new SqlParameter("@MeasureCostMark", SqlDbType.NVarChar, 50)};//  [3/30/2012 L]
                                        };
            parameters[0].Value = item.SheetID;
            parameters[1].Value = item.EqSumPrice;
            parameters[2].Value = item.EqSumApprovalPrice;
            parameters[3].Value = item.MeasureCost;
            parameters[4].Value = item.GuiCost;
            parameters[5].Value = item.TaxCost;
            parameters[6].Value = item.TrafficCost;
            parameters[7].Value = item.MeasureApprovalCost;
            parameters[8].Value = item.GuiApprovalCost;
            parameters[9].Value = item.TaxApprovalCost;
            parameters[10].Value = item.TrafficApprovalCost;
            parameters[11].Value = item.SumOtherCost;
            parameters[12].Value = item.SumApprovalOtherCost;
            parameters[13].Value = item.TotalSumCost;
            parameters[14].Value = item.TotalSumApprovalCost;
            parameters[15].Value = item.ID;
            parameters[16].Value = string.IsNullOrEmpty(item.IsMeasure)?SqlString.Null:item.IsMeasure;
            parameters[17].Value = item.OtherCost;
            parameters[18].Value = string.IsNullOrEmpty(item.MarkOne) ? SqlString.Null :item.MarkOne; 
            parameters[19].Value = string.IsNullOrEmpty(item.MarkTwo) ? SqlString.Null :item.MarkTwo;
            parameters[20].Value = string.IsNullOrEmpty(item.MarkThree) ? SqlString.Null :item.MarkThree;
            parameters[21].Value = string.IsNullOrEmpty(item.MarkFour) ? SqlString.Null :item.MarkFour ;
            parameters[22].Value = string.IsNullOrEmpty(item.MarkFive) ? SqlString.Null : item.MarkFive;
            parameters[23].Value = item.OtherApprovalCost;
            parameters[24].Value = string.IsNullOrEmpty(item.IsApplyMeasure) ? SqlString.Null : item.IsApplyMeasure;
            parameters[25].Value = string.IsNullOrEmpty(item.IsProvider) ? SqlString.Null : item.IsProvider;



            //parameters[16].Value = item.MeasureCostMark;
            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

            DelEquipmentCostItemsByCostID(item.ID);
            if (item.EqCostItems!=null)
            {
                foreach (EquipmentCostItems i in item.EqCostItems)
                {
                    if (i != null)
                    {
                        i.CostID = item.ID;
                        AddEquipmentCostItems(i);
                    }
                }
            }
            

        }

        public void DelEquipmentCostItemsByCostID(long costID)
        {
            string strSql = "delete from "+TABLEEquipmentCostItems+" where CostID=@CostID";
            SqlParameter[] parameters = {
				new SqlParameter("@CostID", SqlDbType.BigInt)};
            parameters[0].Value = costID;

            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        public void AddEquipmentCostItems(EquipmentCostItems item)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+TABLEEquipmentCostItems+"(");
            strSql.Append("CostID,EqName,EqModel,EqUnit,EqNum,EqUnitPrice,EqTotalPrice,EqApprovalUnitPrice,EqApprovalTotalPrice,EqRemark)");
            strSql.Append(" values (");
            strSql.Append("@CostID,@EqName,@EqModel,@EqUnit,@EqNum,@EqUnitPrice,@EqTotalPrice,@EqApprovalUnitPrice,@EqApprovalTotalPrice,@EqRemark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@EqName", SqlDbType.NVarChar,50),
					new SqlParameter("@EqModel", SqlDbType.NVarChar,50),
                    new SqlParameter("@EqUnit", SqlDbType.NVarChar,4),
                    new SqlParameter("@EqNum", SqlDbType.Int,4),
                    new SqlParameter("@EqUnitPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@EqTotalPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@EqApprovalUnitPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@EqApprovalTotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@EqRemark", SqlDbType.NVarChar,200),
                   // new SqlParameter("@IsMeasure", SqlDbType.Int,4)
                    };
            parameters[0].Value = item.CostID;
            parameters[1].Value = item.EqName;
            parameters[2].Value = item.EqModel;
            parameters[3].Value = item.EqUnit;
            parameters[4].Value = item.EqNum;
            parameters[5].Value = item.EqUnitPrice;
            parameters[6].Value = item.EqTotalPrice;
            parameters[7].Value = item.EqApprovalUnitPrice;
            parameters[8].Value = item.EqApprovalTotalPrice;
            parameters[9].Value = item.EqRemark;
          //  parameters[10].Value = item.IsMeasure;

            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        #endregion
    }
}
