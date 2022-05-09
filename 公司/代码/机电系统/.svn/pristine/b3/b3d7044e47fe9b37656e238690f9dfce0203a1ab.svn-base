using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using FM2E.IDAL.Equipment;
using FM2E.Model.Equipment;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Utils;
using System.Collections;
using FM2E.Model.Exceptions;
using System.Data.SqlTypes;
using FM2E.Model.Basic;
using FM2E.SQLServerDAL.Basic;
using System.Data.Common;
//using System.Web;
using FM2E.Model.Maintain;

namespace FM2E.SQLServerDAL.Equipment
{
    public class Equipment : IEquipment
    {

        /// <summary>
        /// 设备表名
        /// </summary>
        private const string TABLE_EQUIPMENT = "FM2E_Equipment";
        /// <summary>
        /// 仓库表名
        /// </summary>
        private const string TABLE_WAREHOUSE = "FM2E_WareHouse";

        private const string TableName = " EquipmentView ";
        private const string ReturnFields = " * ";
        private const string OrderBy = " order by UpdateTime DESC,EquipmentNO DESC ";

        private const string Where = " where 1=1 ";

        private const string SELECT_GETALLEQUIPMENT = "select " + ReturnFields + " from " + TableName + " " + OrderBy;

        private const string SELECT_GETEQUIPMENTBYID = "select " + ReturnFields + " from " + TableName + " where EquipmentID = '{0}' " + OrderBy;

        private const string SELECT_GETEQUIPMENTBYNO = "select " + ReturnFields + " from " + TableName + "left join FM2E_Category on EquipmentView.CategoryID=FM2E_Category.CategoryID where EquipmentNO = '{0}' " + OrderBy;

        private const string SELECT_GETRELATED = "select " + ReturnFields + " from " + TableName + "left join FM2E_Category on EquipmentView.CategoryID=FM2E_Category.CategoryID where EquipmentNO LIKE LEFT('{0}', LEN(EquipmentNO) - 2) + '%' " + OrderBy;

        private const string INSERT_EQUIPMENT = "insert into FM2E_Equipment([EquipmentNO],[Name],[CompanyID],[SectionID],[LocationTag],[LocationID],[SystemID],[PurchaseOrderID],[SerialNum],[PhotoUrl],[Model],[Specification],[Status],[SupplierID],[ProducerID],[Purchaser],[Responsibility],[ResponsibilityName],[Checker],[PurchaseDate],[ExamDate],[OpeningDate],[FileDate],[WarrantyDate],[ServiceLife],[Price],[CategoryID],[CategoryName],[DepreciationMethod],[DepreciableLife],[ResidualRate],[MaintenanceTimes],[Remark],[IsCancel],[UpdateTime],[ProducerName],[SupplierName],[DetailLocation],[AddressID],[AssertNumber],[Count],[Unit]) "
                                                + " values(@EquipmentNO,@Name,@CompanyID,@SectionID,@LocationTag,@LocationID,@SystemID,@PurchaseOrderID,@SerialNum,@PhotoUrl,@Model,@Specification,@Status,@SupplierID,@ProducerID,@Purchaser,@Responsibility,@ResponsibilityName,@Checker,@PurchaseDate,@ExamDate,@OpeningDate,@FileDate,@WarrantyDate,@ServiceLife,@Price,@CategoryID,@CategoryName,@DepreciationMethod,@DepreciableLife,@ResidualRate,@MaintenanceTimes,@Remark,@IsCancel,@UpdateTime,@ProducerName,@SupplierName,@DetailLocation,@AddressID,@AssertNumber,@Count,@Unit) ";

        private const string UPDATE_EQUIPMENT = "update FM2E_Equipment "
                                            + " set [EquipmentNO]=@EquipmentNO,[Name]=@Name,[CompanyID]=@CompanyID,"
                                            +"[SectionID]=@SectionID,[LocationTag]=@LocationTag,[LocationID]=@LocationID,"
                                            +"[SystemID]=@SystemID,[PurchaseOrderID]=@PurchaseOrderID,[SerialNum]=@SerialNum,"
                                            +"[PhotoUrl]=@PhotoUrl,[Model]=@Model,[Specification]=@Specification,[Status]=@Status,"
                                            +"[SupplierID]=@SupplierID,[ProducerID]=@ProducerID,[Purchaser]=@Purchaser,"
                                            +"[Responsibility]=@Responsibility,[ResponsibilityName]=@ResponsibilityName,"
                                            +"[Checker]=@Checker,[PurchaseDate]=@PurchaseDate,[ExamDate]=@ExamDate,"
                                            +"[OpeningDate]=@OpeningDate,[FileDate]=@FileDate,[WarrantyDate]=@WarrantyDate,"
                                            +"[ServiceLife]=@ServiceLife,[Price]=@Price,[CategoryID]=@CategoryID,"
                                            +"[CategoryName]=@CategoryName,[DepreciationMethod]=@DepreciationMethod,"
                                            +"[DepreciableLife]=@DepreciableLife,[ResidualRate]=@ResidualRate,"
                                            +"[MaintenanceTimes]=@MaintenanceTimes,[Remark]=@Remark,[IsCancel]=@IsCancel,"
                                            +"[UpdateTime]=@UpdateTime,[DetailLocation]=@DetailLocation,"
                                            +"[AddressID]=@AddressID,[AssertNumber]=@AssertNumber,[Count]=@Count,[Unit]=@Unit"
                                            + " where [EquipmentID]=@EquipmentID";
        private const string UPDATE_EQUIPMENT2 = "update FM2E_Equipment "
                                            + " set [EquipmentNO]=@EquipmentNO,[Name]=@Name,[CompanyID]=@CompanyID,"
                                            + "[SectionID]=@SectionID,[LocationTag]=@LocationTag,[LocationID]=@LocationID,"
                                            + "[SystemID]=@SystemID,[PurchaseOrderID]=@PurchaseOrderID,[SerialNum]=@SerialNum,"
                                            + "[PhotoUrl]=@PhotoUrl,[Model]=@Model,[Specification]=@Specification,[Status]=@Status,"
                                            + "[SupplierID]=@SupplierID,[ProducerID]=@ProducerID,[Purchaser]=@Purchaser,"
                                            + "[Responsibility]=@Responsibility,[ResponsibilityName]=@ResponsibilityName,"
                                            + "[Checker]=@Checker,[PurchaseDate]=@PurchaseDate,[ExamDate]=@ExamDate,"
                                            + "[OpeningDate]=@OpeningDate,[FileDate]=@FileDate,[WarrantyDate]=@WarrantyDate,"
                                            + "[ServiceLife]=@ServiceLife,[Price]=@Price,[CategoryID]=@CategoryID,"
                                            + "[CategoryName]=@CategoryName,[DepreciationMethod]=@DepreciationMethod,"
                                            + "[DepreciableLife]=@DepreciableLife,[ResidualRate]=@ResidualRate,"
                                            + "[MaintenanceTimes]=@MaintenanceTimes,[Remark]=@Remark,[IsCancel]=@IsCancel,"
                                            + "[UpdateTime]=@UpdateTime,[DetailLocation]=@DetailLocation,"
                                            + "[AddressID]=@AddressID,[AssertNumber]=@AssertNumber,[Count]=@Count,[Unit]=@Unit,[Warming]=@Warming"
                                            + " where [EquipmentID]=@EquipmentID";
        private const string DEL_EQUIPMENT = "delete from FM2E_Equipment where [EquipmentID]='{0}'";

        /// <summary>
        /// 获取统计时汇总的条件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IList Gettypelist(EquipmentSearchInfo item)
        {
            StringBuilder strselect = new StringBuilder();
            strselect.Append(" SELECT distinct co.CompanyName ");
            StringBuilder strwhere = new StringBuilder();
            strwhere.AppendFormat(" FROM FM2E_Company AS co CROSS JOIN FM2E_System AS sy CROSS JOIN FM2E_Category AS ca CROSS JOIN FM2E_Company WHERE co.CompanyID = '{0}' ", item.CompanyID);
            
            StringBuilder strorderby = new StringBuilder();
            strorderby.Append("");
            if (item.GroupBy != string.Empty)
            {
                if (item.GroupBy == "System")
                {
                    strselect.Append(", sy.SystemName");
                    strwhere.Append(" and sy.IsDeleted = 0");
                    strorderby.Append(" order by sy.SystemName");
                }
                if (item.GroupBy == "Category")
                {
                    strselect.Append(", ca.CategoryName,ca.CategoryCode,ca.Level");
                    strwhere.Append(" and ca.IsDeleted = 0");
                    strorderby.Append(" order by ca.CategoryCode");
                }
            }
            if (item.GroupBy2 != string.Empty)
            {
                if (item.GroupBy2 == "System")
                {
                    strselect.Append(", sy.SystemName");
                    strwhere.Append(" and sy.IsDeleted = 0");
                    strorderby.Append(",sy.SystemName");
                }
                if (item.GroupBy2 == "Category")
                {
                    strselect.Append(", ca.CategoryName,ca.CategoryCode,ca.Level");
                    strwhere.Append(" and ca.IsDeleted = 0");
                    strorderby.Append(",ca.CategoryCode");
                }
            }
            if (item.CategoryName != null && item.CategoryName != string.Empty)
            {
                strselect.Append(", ca.CategoryName,ca.Level,ca.CategoryCode");
                strwhere.Append(" and ca.CategoryName = '" + item.CategoryName + "' ");
                    
            }
            if (item.SystemName != null && item.SystemName != string.Empty)
            {
                strselect.Append(", sy.SystemName");
                strwhere.Append(" and sy.SystemName = '" + item.SystemName + "' ");
          
            }
            strselect.Append(strwhere);
            strselect.Append(strorderby);

            IList list = new List<EquipmentInfoFacade>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strselect.ToString(), null))
                {
                    while (rd.Read())
                    {
                        EquipmentInfoFacade model = new EquipmentInfoFacade();

                        if (item.SystemName != null && item.SystemName != string.Empty && !Convert.IsDBNull(rd["SystemName"]))
                            model.SystemName = Convert.ToString(rd["SystemName"]);

                        if (item.CategoryName != null && item.CategoryName != string.Empty && !Convert.IsDBNull(rd["CategoryName"]))
                        {
                            
                            model.CategoryName =  Convert.ToString(rd["CategoryName"]);
                        }

                        if (!Convert.IsDBNull(rd["CompanyName"]))
                            model.CompanyName = Convert.ToString(rd["CompanyName"]);

                        if (item.GroupBy == "System" || item.GroupBy2 == "System")
                            model.SystemName = Convert.ToString(rd["SystemName"]);

                        if (item.GroupBy == "Category" || item.GroupBy2 == "Category")
                        {
                            int level = Convert.ToInt32(rd["Level"]) - 1;
                            string tagcount = "";
                            for (int i = 0; i < level; i++)
                            {
                                tagcount += "---";
                            }
                            model.CategoryName = tagcount + Convert.ToString(rd["CategoryName"]);
                            model.CategoryCode = Convert.ToString(rd["CategoryCode"]);
                        }

                        model.Price = decimal.Zero;


                        list.Add(model);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取统计汇总分类时失败", e);
            }

            return list;
        }
        /// <summary>
        /// 获取要汇总的设备
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IList AssetAndDepreciation(AssetAndDepreciationInfo item)
        {
            StringBuilder strselect = new StringBuilder();
            strselect.Append(" SELECT co.CompanyName , eq.CategoryName, sy.SystemName, se.SectionName, eq.EquipmentNO, eq.Name, eq.SerialNum, eq.Model");
            StringBuilder strwhere = new StringBuilder();
            strwhere.Append(" FROM FM2E_Equipment eq inner JOIN FM2E_Company co ON eq.CompanyID = co.CompanyID inner join FM2E_System sy on eq.SystemID = sy.SystemID  inner JOIN FM2E_Category ca ON eq.CategoryID  = ca.CategoryID inner join FM2E_Section se on eq.SectionID = se.SectionID  where 1=1 and sy.IsDeleted = 0 and ca.IsDeleted = 0 and eq.IsCancel = 0");



            if (item.CategoryName != null && item.CategoryName != string.Empty)
            {
                strwhere.Append(" and eq.CategoryName = '" + item.CategoryName + "' ");

            }
            if (item.SystemName != null && item.SystemName != string.Empty)
            {
                strwhere.Append(" and sy.SystemName = '" + item.SystemName + "' ");

            }
            if (item.CompanyID != null && item.CompanyID != string.Empty)
            {
                strwhere.Append(" and eq.CompanyID = '" + item.CompanyID + "' ");
            }
            if (item.SectionName != null && item.SectionName != string.Empty)
            {
                strwhere.Append(" and se.SectionName = '" + item.SectionName + "' ");

            }
            if (item.LocationID != null && item.LocationID != string.Empty)
            {
                strwhere.Append(" and eq.LocationID = '" + item.LocationID + "' ");

            }
            if (item.LocationTag != null && item.LocationTag != string.Empty)
            {
                strwhere.Append(" and eq.LocationTag = '" + item.LocationTag + "' ");

            }
            if (item.EquipmentNO != null && item.EquipmentNO != string.Empty)
            {
                strwhere.Append(" and eq.EquipmentNO = '" + item.EquipmentNO + "' ");

            }
            if (item.Name != null && item.Name != string.Empty)
            {
                strwhere.Append(" and eq.Name = '" + item.Name + "' ");

            }
            if (item.SerialNum != null && item.SerialNum != string.Empty)
            {
                strwhere.Append(" and eq.SerialNum = '" + item.SerialNum + "' ");

            }
            if (item.Model != null && item.Model != string.Empty)
            {
                strwhere.Append(" and eq.Model = '" + item.Model + "' ");

            }
            strselect.Append(", eq.Price AS BuyPrice,eq.PurchaseDate,eq.DepreciableLife,eq.ResidualRate,eq.DepreciationMethod");
            strselect.Append(strwhere);
            IList list = new List<AssetAndDepreciationInfo>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strselect.ToString(), null))
                {
                    while (rd.Read())
                    {
                        AssetAndDepreciationInfo model = new AssetAndDepreciationInfo();

                        if (item.GroupBy == "sy.SystemName" || item.GroupBy2 == "sy.SystemName")
                            model.SystemName = Convert.ToString(rd["SystemName"]);

                        if (item.GroupBy == "ca.CategoryName" || item.GroupBy2 == "ca.CategoryName")
                            model.CategoryName = Convert.ToString(rd["CategoryName"]);

                        if (item.CategoryName != null && item.CategoryName != string.Empty && !Convert.IsDBNull(rd["CategoryName"]))
                            model.CategoryName = Convert.ToString(rd["CategoryName"]);

                        if (item.SectionName != null && item.SectionName != string.Empty && !Convert.IsDBNull(rd["SectionName"]))
                            model.SectionName = Convert.ToString(rd["SectionName"]);

                        if (item.SystemName != null && item.SystemName != string.Empty && !Convert.IsDBNull(rd["SystemName"]))
                            model.SystemName = Convert.ToString(rd["SystemName"]);

                        //if (item.LocationID != null && item.LocationID != string.Empty&&!Convert.IsDBNull(rd["LocationID"]))
                        //    model.LocationID = Convert.ToString(rd["LocationID"]);

                        //if (item.LocationTag != null && item.LocationTag != string.Empty&&!Convert.IsDBNull(rd["LocationTag"]))
                        //    model.LocationTag = Convert.ToString(rd["LocationTag"]);

                        if (item.EquipmentNO != null && item.EquipmentNO != string.Empty && !Convert.IsDBNull(rd["EquipmentNO"]))
                            model.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

                        if (item.Name != null && item.Name != string.Empty && !Convert.IsDBNull(rd["Name"]))
                            model.Name = Convert.ToString(rd["Name"]);

                        if (item.SerialNum != null && item.SerialNum != string.Empty && !Convert.IsDBNull(rd["SerialNum"]))
                            model.SerialNum = Convert.ToString(rd["SerialNum"]);

                        if (item.Model != null && item.Model != string.Empty && !Convert.IsDBNull(rd["Model"]))
                            model.Model = Convert.ToString(rd["Model"]);

                        if (item.LocationID != null && item.LocationID != string.Empty)
                            switch (item.LocationTag)
                            {
                                case "1":
                                    {
                                        Channal channal = new Channal();
                                        ChannalInfo channalinfo = new ChannalInfo();
                                        model.LocationName = channal.GetChannal(item.LocationID).ChannalName;
                                        break;
                                    }
                                case "2":
                                    {
                                        TollGate tollgate = new TollGate();
                                        TollGateInfo tollgateinfo = new TollGateInfo();
                                        model.LocationName = tollgate.GetTollGate(item.LocationID).TollGateName;
                                        break;
                                    }
                                case "3":
                                    {

                                        break;
                                    }
                                case "4":
                                    {
                                        Warehouse warehouse = new Warehouse();
                                        WarehouseInfo warehouseinfo = new WarehouseInfo();
                                        model.LocationName = warehouse.GetWarehouse(item.LocationID).Name;
                                        break;
                                    }
                            }


                        if (!Convert.IsDBNull(rd["BuyPrice"]))
                            model.Price = Convert.ToDecimal(rd["BuyPrice"]);
                        if (!Convert.IsDBNull(rd["PurchaseDate"]))
                            model.PurchaseDate = Convert.ToDateTime(rd["PurchaseDate"]);
                        if (!Convert.IsDBNull(rd["DepreciableLife"]))
                            model.DepreciableLife = Convert.ToInt32(rd["DepreciableLife"]);
                        if (!Convert.IsDBNull(rd["ResidualRate"]))
                            model.ResidualRate = Convert.ToDecimal(rd["ResidualRate"]);
                        if (!Convert.IsDBNull(rd["DepreciationMethod"]))
                            model.DepreciationMethod = Convert.ToInt64(rd["DepreciationMethod"]);

                        list.Add(model);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("统计折旧时失败", e);
            }

            return list;
        }


        public IList GetAllEquipment()
        {
            IList list = new List<EquipmentInfoFacade>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, SELECT_GETALLEQUIPMENT, null))
                {
                    while (rd.Read())
                        list.Add(GetData(rd));
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取所有设备信息时失败", e);
            }
            return list;

        }

        public IList<EquipmentInfoFacade> GetRecentEquipment(int num)
        {
            //    string cmd = string.Format(SELECT_RECENTEQUIPMENT, num);

            List<EquipmentInfoFacade> list = new List<EquipmentInfoFacade>();
            //    try
            //    {
            //        using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
            //        {
            //            while (rd.Read())
            //            {
            //                //EquipmentInfoFacade item = new EquipmentInfoFacade();
            //                //if (!Convert.IsDBNull(rd["EquipmentID"]))
            //                //    item.EquipmentID = Convert.ToInt64(rd["EquipmentID"]);

            //                //if (!Convert.IsDBNull(rd["EquipmentNum"]))
            //                //    item.EquipmentNO = Convert.ToString(rd["EquipmentNum"]);

            //                //if (!Convert.IsDBNull(rd["EquipmentName"]))
            //                //    item.Name = Convert.ToString(rd["EquipmentName"]);

            //                //if (!Convert.IsDBNull(rd["Category"]))
            //                //    item.Category = Convert.ToString(rd["Category"]);

            //                //if (!Convert.IsDBNull(rd["Department"]))
            //                //    item.Department = Convert.ToString(rd["Department"]);

            //                //if (!Convert.IsDBNull(rd["Price"]))
            //                //    item.Price = Convert.ToDecimal(rd["Price"]);

            //                //if (!Convert.IsDBNull(rd["PhotoUrl"]))
            //                //    item.PhotoUrl = Convert.ToString(rd["PhotoUrl"]);

            //                //if (!Convert.IsDBNull(rd["UStatus"]))
            //                //    item.UsingStatus = Convert.ToString(rd["UStatus"]);

            //                //if (!Convert.IsDBNull(rd["RStatus"]))
            //                //    item.RunningStatus = Convert.ToString(rd["RStatus"]);

            //                //if (!Convert.IsDBNull(rd["Purchaser"]))
            //                //    item.Purchaser = Convert.ToString(rd["Purchaser"]);

            //                //if (!Convert.IsDBNull(rd["PurchaseDate"]))
            //                //    item.PurchaseDate = Convert.ToDateTime(rd["PurchaseDate"]);

            //                //list.Add(item);
            //            }
            //        }
            //    }
            //    catch
            //    {
            //        throw;
            //    }

            return list;
        }

        public EquipmentInfoFacade GetEquipment(string id)
        {
            string cmd = string.Format(SELECT_GETEQUIPMENTBYID, id);
            EquipmentInfoFacade item = null;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        item = GetData(rd);
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取设备信息失败", e);
            }
            return item;
        }

        public EquipmentInfoFacade GetEquipmentBYNO(string id)
        {
            string cmd = string.Format(SELECT_GETEQUIPMENTBYNO, id);
            EquipmentInfoFacade item = null;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        item = GetDataForEquipmentNO(rd);
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取设备信息失败", e);
            }
            return item;
        }
        
        //*************************************************6-25增加插入员工信息*******************************************

        //****************************************************************************************************************

        public void InsertEquipment(EquipmentInfo item)
        {
            SqlParameter[] param = GetInsertParam();
            //param[0].Value = item.EquipmentID;
            param[0].Value = item.EquipmentNO;
            param[1].Value = item.Name;
            param[2].Value = item.CompanyID;
            param[3].Value = string.IsNullOrEmpty(item.SectionID) ? SqlString.Null : item.SectionID;
            param[4].Value = string.IsNullOrEmpty(item.LocationTag) ? SqlString.Null : item.LocationTag;
            param[5].Value = string.IsNullOrEmpty(item.LocationID) ? SqlString.Null : item.LocationID;
            param[6].Value = string.IsNullOrEmpty(item.SystemID) ? SqlString.Null : item.SystemID;
            param[7].Value = string.IsNullOrEmpty(item.PurchaseOrderID) ? SqlString.Null : item.PurchaseOrderID;
            param[8].Value = string.IsNullOrEmpty(item.SerialNum) ? SqlString.Null : item.SerialNum;
            param[9].Value = string.IsNullOrEmpty(item.PhotoUrl) ? SqlString.Null : item.PhotoUrl;
            param[10].Value = string.IsNullOrEmpty(item.Model) ? SqlString.Null : item.Model;
            param[11].Value = string.IsNullOrEmpty(item.Specification) ? SqlString.Null : item.Specification;
            param[12].Value = item.Status;
            param[13].Value = item.SupplierID;
            param[14].Value = item.ProducerID;
            param[15].Value = string.IsNullOrEmpty(item.Purchaser) ? SqlString.Null : item.Purchaser;
            param[16].Value = string.IsNullOrEmpty(item.Responsibility) ? SqlString.Null : item.Responsibility;
            param[17].Value = string.IsNullOrEmpty(item.ResponsibilityName) ? SqlString.Null : item.ResponsibilityName;
            param[18].Value = string.IsNullOrEmpty(item.Checker) ? SqlString.Null : item.Checker;
            param[19].Value = DateTime.Compare(item.PurchaseDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.PurchaseDate;
            param[20].Value = DateTime.Compare(item.ExamDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.ExamDate;
            param[21].Value = DateTime.Compare(item.OpeningDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.OpeningDate;
            param[22].Value = DateTime.Compare(item.FileDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.FileDate;
            param[23].Value = DateTime.Compare(item.WarrantyDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.WarrantyDate;
            param[24].Value = item.ServiceLife;
            param[25].Value = item.Price;
            param[26].Value = item.CategoryID;
            param[27].Value = string.IsNullOrEmpty(item.CategoryName) ? SqlString.Null : item.CategoryName;
            param[28].Value = item.DepreciationMethod;
            param[29].Value = item.DepreciableLife;
            param[30].Value = item.ResidualRate;
            param[31].Value = item.MaintenanceTimes;
            param[32].Value = string.IsNullOrEmpty(item.Remark) ? SqlString.Null : item.Remark;
            param[33].Value = item.IsCancel;
            param[34].Value = DateTime.Compare(item.UpdateTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.UpdateTime;
            param[35].Value = item.ProducerName == null ? SqlString.Null : item.ProducerName;
            param[36].Value = item.SupplierName == null ? SqlString.Null : item.SupplierName;
            param[37].Value = string.IsNullOrEmpty(item.DetailLocation) ? SqlString.Null : item.DetailLocation;
            param[38].Value = item.AddressID == 0 ? SqlInt64.Null : item.AddressID;
            param[39].Value = string.IsNullOrEmpty(item.AssertNumber) ? SqlString.Null : item.AssertNumber;
            param[40].Value = item.Count == 0 ? SqlInt32.Null : item.Count;
            param[41].Value = item.Unit == null ? SqlString.Null : item.Unit;
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, INSERT_EQUIPMENT, param);
                    if (result == 0)
                        throw new Exception("没有更新任何数据项");
                }
                catch (Exception e)
                {
                    throw e;// DALException("插入设备信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void UpdateEquipment(EquipmentInfo item)
        {
            SqlParameter[] param = GetInsertUpdateParam();
            param[0].Value = item.EquipmentID;
            param[1].Value = item.EquipmentNO;
            param[2].Value = item.Name;
            param[3].Value = item.CompanyID;
            param[4].Value = string.IsNullOrEmpty(item.SectionID) ? SqlString.Null : item.SectionID;
            param[5].Value = string.IsNullOrEmpty(item.LocationTag) ? SqlString.Null : item.LocationTag;
            param[6].Value = string.IsNullOrEmpty(item.LocationID) ? SqlString.Null : item.LocationID;
            param[7].Value = string.IsNullOrEmpty(item.SystemID) ? SqlString.Null : item.SystemID;
            param[8].Value = string.IsNullOrEmpty(item.PurchaseOrderID) ? SqlString.Null : item.PurchaseOrderID;
            param[9].Value = string.IsNullOrEmpty(item.SerialNum) ? SqlString.Null : item.SerialNum;
            param[10].Value = string.IsNullOrEmpty(item.PhotoUrl) ? SqlString.Null : item.PhotoUrl;
            param[11].Value = string.IsNullOrEmpty(item.Model) ? SqlString.Null : item.Model;
            param[12].Value = string.IsNullOrEmpty(item.Specification) ? SqlString.Null : item.Specification;
            param[13].Value = item.Status;
            param[14].Value = item.SupplierID;
            param[15].Value = item.ProducerID;
            param[16].Value = string.IsNullOrEmpty(item.Purchaser) ? SqlString.Null : item.Purchaser;
            param[17].Value = string.IsNullOrEmpty(item.Responsibility) ? SqlString.Null : item.Responsibility;
            param[18].Value = string.IsNullOrEmpty(item.ResponsibilityName) ? SqlString.Null : item.ResponsibilityName;
            param[19].Value = string.IsNullOrEmpty(item.Checker) ? SqlString.Null : item.Checker;
            param[20].Value = DateTime.Compare(item.PurchaseDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.PurchaseDate;
            param[21].Value = DateTime.Compare(item.ExamDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.ExamDate;
            param[22].Value = DateTime.Compare(item.OpeningDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.OpeningDate;
            param[23].Value = DateTime.Compare(item.FileDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.FileDate;
            param[24].Value = DateTime.Compare(item.WarrantyDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.WarrantyDate;
            param[25].Value = item.ServiceLife;
            param[26].Value = item.Price;
            param[27].Value = item.CategoryID;
            param[28].Value = string.IsNullOrEmpty(item.CategoryName) ? SqlString.Null : item.CategoryName;
            param[29].Value = item.DepreciationMethod;
            param[30].Value = item.DepreciableLife;
            param[31].Value = item.ResidualRate;
            param[32].Value = item.MaintenanceTimes;
            param[33].Value = string.IsNullOrEmpty(item.Remark) ? SqlString.Null : item.Remark;
            param[34].Value = item.IsCancel;
            param[35].Value = DateTime.Compare(item.UpdateTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.UpdateTime;
            param[36].Value = string.IsNullOrEmpty(item.DetailLocation) ? SqlString.Null : item.DetailLocation;
            param[37].Value = item.AddressID == 0 ? SqlInt64.Null : item.AddressID;
            param[38].Value = string.IsNullOrEmpty(item.AssertNumber) ? SqlString.Null : item.AssertNumber;
            param[39].Value = item.Count == 0 ? SqlInt32.Null : item.Count;
            param[40].Value = item.Unit == null ? SqlString.Null : item.Unit;
            param[41].Value = item.Warming;
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, UPDATE_EQUIPMENT2, param);
                    if (result == 0)
                        throw new Exception("没有更新任何数据项");
                }
                catch (Exception e)
                {
                    throw new DALException("更新设备信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void UpdateEquipment(EquipmentInfoFacade item)
        {
            SqlParameter[] param = GetInsertUpdateParam();
            param[0].Value = item.EquipmentID;
            param[1].Value = item.EquipmentNO;
            param[2].Value = item.Name;
            param[3].Value = item.CompanyID;
            param[4].Value = string.IsNullOrEmpty(item.SectionID) ? SqlString.Null : item.SectionID;
            param[5].Value = string.IsNullOrEmpty(item.LocationTag) ? SqlString.Null : item.LocationTag;
            param[6].Value = string.IsNullOrEmpty(item.LocationID) ? SqlString.Null : item.LocationID;
            param[7].Value = string.IsNullOrEmpty(item.SystemID) ? SqlString.Null : item.SystemID;
            param[8].Value = string.IsNullOrEmpty(item.PurchaseOrderID) ? SqlString.Null : item.PurchaseOrderID;
            param[9].Value = string.IsNullOrEmpty(item.SerialNum) ? SqlString.Null : item.SerialNum;
            param[10].Value = string.IsNullOrEmpty(item.PhotoUrl) ? SqlString.Null : item.PhotoUrl;
            param[11].Value = string.IsNullOrEmpty(item.Model) ? SqlString.Null : item.Model;
            param[12].Value = string.IsNullOrEmpty(item.Specification) ? SqlString.Null : item.Specification;
            param[13].Value = item.Status;
            param[14].Value = item.SupplierID;
            param[15].Value = item.ProducerID;
            param[16].Value = string.IsNullOrEmpty(item.Purchaser) ? SqlString.Null : item.Purchaser;
            param[17].Value = string.IsNullOrEmpty(item.Responsibility) ? SqlString.Null : item.Responsibility;
            param[18].Value = string.IsNullOrEmpty(item.ResponsibilityName) ? SqlString.Null : item.ResponsibilityName;
            param[19].Value = string.IsNullOrEmpty(item.Checker) ? SqlString.Null : item.Checker;
            param[20].Value = DateTime.Compare(item.PurchaseDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.PurchaseDate;
            param[21].Value = DateTime.Compare(item.ExamDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.ExamDate;
            param[22].Value = DateTime.Compare(item.OpeningDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.OpeningDate;
            param[23].Value = DateTime.Compare(item.FileDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.FileDate;
            param[24].Value = DateTime.Compare(item.WarrantyDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.WarrantyDate;
            param[25].Value = item.ServiceLife;
            param[26].Value = item.Price;
            param[27].Value = item.CategoryID;
            param[28].Value = string.IsNullOrEmpty(item.CategoryName) ? SqlString.Null : item.CategoryName;
            param[29].Value = item.DepreciationMethod;
            param[30].Value = item.DepreciableLife;
            param[31].Value = item.ResidualRate;
            param[32].Value = item.MaintenanceTimes;
            param[33].Value = string.IsNullOrEmpty(item.Remark) ? SqlString.Null : item.Remark;
            param[34].Value = item.IsCancel;
            param[35].Value = DateTime.Compare(item.UpdateTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.UpdateTime;
            param[36].Value = string.IsNullOrEmpty(item.DetailLocation) ? SqlString.Null : item.DetailLocation;
            param[37].Value = item.AddressID == 0 ? SqlInt64.Null : item.AddressID;
            param[38].Value = string.IsNullOrEmpty(item.AssertNumber) ? SqlString.Null : item.AssertNumber;
            param[39].Value = item.Count == 0 ? SqlInt32.Null : item.Count;
            param[40].Value = item.Unit == null ? SqlString.Null : item.Unit;
            param[41].Value = item.Warming;
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, UPDATE_EQUIPMENT2, param);
                    if (result == 0)
                        throw new Exception("没有更新任何数据项");
                }
                catch (Exception e)
                {
                    throw new DALException("更新设备信息失败", e);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void UpdateEquipment(SqlTransaction trans, EquipmentInfoFacade item)
        {
            SqlParameter[] param = GetInsertUpdateParam();
            param[0].Value = item.EquipmentID;
            param[1].Value = item.EquipmentNO;
            param[2].Value = item.Name;
            param[3].Value = item.CompanyID;
            param[4].Value = string.IsNullOrEmpty(item.SectionID) ? SqlString.Null : item.SectionID;
            param[5].Value = string.IsNullOrEmpty(item.LocationTag) ? SqlString.Null : item.LocationTag;
            param[6].Value = string.IsNullOrEmpty(item.LocationID) ? SqlString.Null : item.LocationID;
            param[7].Value = string.IsNullOrEmpty(item.SystemID) ? SqlString.Null : item.SystemID;
            param[8].Value = item.PurchaseOrderID;
            param[9].Value = item.SerialNum;
            param[10].Value = item.PhotoUrl;
            param[11].Value = item.Model;
            param[12].Value = item.Specification;
            param[13].Value = item.Status;
            param[14].Value = item.SupplierID;
            param[15].Value = item.ProducerID;
            param[16].Value = item.Purchaser;
            param[17].Value = item.Responsibility;
            param[18].Value = item.ResponsibilityName;
            param[19].Value = item.Checker;
            param[20].Value = DateTime.Compare(item.PurchaseDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.PurchaseDate;
            param[21].Value = DateTime.Compare(item.ExamDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.ExamDate;
            param[22].Value = DateTime.Compare(item.OpeningDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.OpeningDate;
            param[23].Value = DateTime.Compare(item.FileDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.FileDate;
            param[24].Value = DateTime.Compare(item.WarrantyDate, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.WarrantyDate;
            param[25].Value = item.ServiceLife;
            param[26].Value = item.Price;
            param[27].Value = item.CategoryID;
            param[28].Value = item.CategoryName;
            param[29].Value = item.DepreciationMethod;
            param[30].Value = item.DepreciableLife;
            param[31].Value = item.ResidualRate;
            param[32].Value = item.MaintenanceTimes;
            param[33].Value = item.Remark;
            param[34].Value = item.IsCancel;
            param[35].Value = DateTime.Compare(item.UpdateTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : item.UpdateTime;
            param[36].Value = string.IsNullOrEmpty(item.DetailLocation) ? SqlString.Null : item.DetailLocation;
            param[37].Value = item.AddressID == 0 ? SqlInt64.Null : item.AddressID;
            param[38].Value = string.IsNullOrEmpty(item.AssertNumber) ? SqlString.Null : item.AssertNumber;
            param[39].Value = item.Count == 0 ? SqlInt32.Null : item.Count;
            param[40].Value = item.Unit == null ? SqlString.Null : item.Unit;
            try
            {
                int result = SQLHelper.ExecuteNonQuery(trans, CommandType.Text, UPDATE_EQUIPMENT, param);
                if (result == 0)
                    throw new Exception("没有更新任何数据项");
            }
            catch (Exception e)
            {
                throw e;// new DALException("更新设备信息失败", e);
            }
        }

        public void UpdateEquipment(SqlTransaction trans, string equipmentNO, string sectionID, string systemID, string locationID, string locationTag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_Equipment ");
            strSql.Append(" set SectionID=@SectionID,SystemID=@SystemID,LocationTag=@LocationTag,LocationID=@LocationID ");
            strSql.Append(" where EquipmentNO=@EquipmentNO");
            SqlParameter[] param = new SqlParameter[]{
                   new SqlParameter("@EquipmentNO",SqlDbType.VarChar,20),
                      new SqlParameter("@SectionID",SqlDbType.VarChar,2),
                    new SqlParameter("@LocationTag",SqlDbType.VarChar,1),
                    new SqlParameter("@LocationID",SqlDbType.VarChar,6),
                    new SqlParameter("@SystemID",SqlDbType.VarChar,1)
             };
            param[0].Value = equipmentNO;
            param[1].Value = string.IsNullOrEmpty(sectionID) ? SqlString.Null : sectionID;
            param[2].Value = string.IsNullOrEmpty(locationTag) ? SqlString.Null : locationTag;
            param[3].Value = string.IsNullOrEmpty(locationID) ? SqlString.Null : locationID;
            param[4].Value = string.IsNullOrEmpty(systemID) ? SqlString.Null : systemID;

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), param);
            return;
        }



        private static SqlParameter[] GetInsertParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(INSERT_EQUIPMENT);
            if (param == null)
            {
                param = new SqlParameter[]{
                    
                    new SqlParameter("@EquipmentNO",SqlDbType.VarChar,20),
                    new SqlParameter("@Name",SqlDbType.NVarChar,20),
                    new SqlParameter("@CompanyID",SqlDbType.VarChar,2),
                    new SqlParameter("@SectionID",SqlDbType.VarChar,2),
                    new SqlParameter("@LocationTag",SqlDbType.VarChar,1),
                    new SqlParameter("@LocationID",SqlDbType.VarChar,6),
                    new SqlParameter("@SystemID",SqlDbType.VarChar,2),
                    new SqlParameter("@PurchaseOrderID",SqlDbType.VarChar,20),
                    new SqlParameter("@SerialNum",SqlDbType.VarChar,30),
                    new SqlParameter("@PhotoUrl",SqlDbType.NVarChar,60),
                    new SqlParameter("@Model",SqlDbType.NVarChar,20),
                    new SqlParameter("@Specification",SqlDbType.NVarChar,60),
                    new SqlParameter("@Status",SqlDbType.TinyInt,1),
                    new SqlParameter("@SupplierID",SqlDbType.BigInt,8),
                    new SqlParameter("@ProducerID",SqlDbType.BigInt,8),
                    new SqlParameter("@Purchaser",SqlDbType.VarChar,20),
                    new SqlParameter("@Responsibility",SqlDbType.VarChar,20),
                    new SqlParameter("@ResponsibilityName",SqlDbType.VarChar,20),
                    new SqlParameter("@Checker",SqlDbType.VarChar,20),
                    new SqlParameter("@PurchaseDate",SqlDbType.DateTime,8),
                    new SqlParameter("@ExamDate",SqlDbType.DateTime,8),
                    new SqlParameter("@OpeningDate",SqlDbType.DateTime,8),
                    new SqlParameter("@FileDate",SqlDbType.DateTime,8),
                    new SqlParameter("@WarrantyDate",SqlDbType.DateTime,8),
                    new SqlParameter("@ServiceLife",SqlDbType.Int,4),
                    new SqlParameter("@Price",SqlDbType.Decimal,18),
                    new SqlParameter("@CategoryID",SqlDbType.BigInt,8),
                    new SqlParameter("@CategoryName",SqlDbType.NVarChar,20),
                    new SqlParameter("@DepreciationMethod",SqlDbType.TinyInt,1),
                    new SqlParameter("@DepreciableLife",SqlDbType.Int,4),
                    new SqlParameter("@ResidualRate",SqlDbType.Decimal,10),
                    new SqlParameter("@MaintenanceTimes",SqlDbType.Int,4),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                    new SqlParameter("@IsCancel",SqlDbType.Bit,1),
                    new SqlParameter("@UpdateTime",SqlDbType.DateTime,8),
                    new SqlParameter("@ProducerName",SqlDbType.NVarChar,50),
                    new SqlParameter("@SupplierName",SqlDbType.NVarChar,50),
                    new SqlParameter("@DetailLocation",SqlDbType.NVarChar,20),
                    new SqlParameter("@AddressID",SqlDbType.BigInt),
                    new SqlParameter("@AssertNumber",SqlDbType.VarChar,20),
                    new SqlParameter("@Count",SqlDbType.Int,4),
                    new SqlParameter("@Unit",SqlDbType.NVarChar,5)
                };
                SQLHelper.CacheParameters(INSERT_EQUIPMENT, param);
            }
            return param;
        }

        private static SqlParameter[] GetInsertUpdateParam()
        {
            SqlParameter[] param = SQLHelper.GetCachedParameters(UPDATE_EQUIPMENT);
            if (param == null)
            {
                param = new SqlParameter[]{
                    new SqlParameter("@EquipmentID",SqlDbType.BigInt,8),
                    new SqlParameter("@EquipmentNO",SqlDbType.VarChar,20),
                    new SqlParameter("@Name",SqlDbType.NVarChar,20),
                    new SqlParameter("@CompanyID",SqlDbType.VarChar,2),
                    new SqlParameter("@SectionID",SqlDbType.VarChar,2),
                    new SqlParameter("@LocationTag",SqlDbType.VarChar,1),
                    new SqlParameter("@LocationID",SqlDbType.VarChar,6),
                    new SqlParameter("@SystemID",SqlDbType.VarChar,2),
                    new SqlParameter("@PurchaseOrderID",SqlDbType.VarChar,20),
                    new SqlParameter("@SerialNum",SqlDbType.VarChar,30),
                    new SqlParameter("@PhotoUrl",SqlDbType.NVarChar,60),
                    new SqlParameter("@Model",SqlDbType.NVarChar,20),
                    new SqlParameter("@Specification",SqlDbType.NVarChar,60),
                    new SqlParameter("@Status",SqlDbType.TinyInt,1),
                    new SqlParameter("@SupplierID",SqlDbType.BigInt,8),
                    new SqlParameter("@ProducerID",SqlDbType.BigInt,8),
                    new SqlParameter("@Purchaser",SqlDbType.VarChar,20),
                    new SqlParameter("@Responsibility",SqlDbType.VarChar,20),
                    new SqlParameter("@ResponsibilityName",SqlDbType.VarChar,20),
                    new SqlParameter("@Checker",SqlDbType.VarChar,20),
                    new SqlParameter("@PurchaseDate",SqlDbType.DateTime,8),
                    new SqlParameter("@ExamDate",SqlDbType.DateTime,8),
                    new SqlParameter("@OpeningDate",SqlDbType.DateTime,8),
                    new SqlParameter("@FileDate",SqlDbType.DateTime,8),
                    new SqlParameter("@WarrantyDate",SqlDbType.DateTime,8),
                    new SqlParameter("@ServiceLife",SqlDbType.Int,4),
                    new SqlParameter("@Price",SqlDbType.Decimal,18),
                    new SqlParameter("@CategoryID",SqlDbType.BigInt,8),
                    new SqlParameter("@CategoryName",SqlDbType.NVarChar,20),
                    new SqlParameter("@DepreciationMethod",SqlDbType.TinyInt,1),
                    new SqlParameter("@DepreciableLife",SqlDbType.Int,4),
                    new SqlParameter("@ResidualRate",SqlDbType.Decimal,10),
                    new SqlParameter("@MaintenanceTimes",SqlDbType.Int,4),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                    new SqlParameter("@IsCancel",SqlDbType.Bit,1),
                    new SqlParameter("@UpdateTime",SqlDbType.DateTime,8),
                    new SqlParameter("@DetailLocation",SqlDbType.NVarChar,20),
                    new SqlParameter("@AddressID",SqlDbType.BigInt),
                    new SqlParameter("@AssertNumber",SqlDbType.VarChar,20),
                    new SqlParameter("@Count",SqlDbType.Int,4),
                    new SqlParameter("@Unit",SqlDbType.NVarChar,5),
                    new SqlParameter("@Warming",SqlDbType.Int,4)

                };
                SQLHelper.CacheParameters(UPDATE_EQUIPMENT, param);
            }
            return param;
        }

        public void DelEquipment(string id)
        {
            string cmd = string.Format(DEL_EQUIPMENT, id);
            try
            {
                int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, cmd, null);
                if (rows == 0)
                    throw new Exception("没有删除任何数据项！");
            }
            catch (Exception e)
            {
                throw new DALException("删除设备信息失败", e);
            }
        }

        public IList<EquipmentInfoFacade> Search(EquipmentSearchInfo item)
        {
            QueryParam qp = GenerateSearchTerm(item);
            string cmd ="select " + qp.ReturnFields +" from " + qp.TableName + qp.Where + qp.OrderBy;
            List<EquipmentInfoFacade> list = new List<EquipmentInfoFacade>();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                    {
                        list.Add(GetData(rd));
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("搜索设备信息失败", e);
            }


            return list;
        }

        public QueryParam GenerateSearchTerm(EquipmentSearchInfo item)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = TableName;
            qp.ReturnFields = ReturnFields;
            qp.OrderBy = OrderBy;
            qp.Where = Where;
            if (item.EquipmentID != 0)
                qp.Where += " and EquipmentID = " + item.EquipmentID + " ";
            if (item.EquipmentNO != null && item.EquipmentNO != string.Empty)
                qp.Where += " and EquipmentNO = '" + item.EquipmentNO + "' ";
            if (item.Name != null && item.Name != string.Empty)
                qp.Where += " and Name like '%" + item.Name + "%' ";
            if (item.CompanyID != null && item.CompanyID != string.Empty)
                qp.Where += " and CompanyID = '" + item.CompanyID + "' ";
            if (item.CompanyName != null && item.CompanyName != string.Empty)
                qp.Where += " and CompanyName = '" + item.CompanyName + "' ";
            if (item.SectionID != null && item.SectionID != string.Empty)
                qp.Where += " and SectionID = '" + item.SectionID + "' ";
            if (item.LocationTag != null && item.LocationTag != string.Empty)
                qp.Where += " and LocationTag = '" + item.LocationTag + "' ";
            if (item.LocationID != null && item.LocationID != string.Empty)
                qp.Where += " and LocationID = '" + item.LocationID + "' ";
            if (item.SystemID != null && item.SystemID != string.Empty)
                qp.Where += " and SystemID = '" + item.SystemID + "' ";
            if (item.SystemName != null && item.SystemName != string.Empty)
                qp.Where += " and SystemName = '" + item.SystemName + "' ";
            if (item.PurchaseOrderID != null && item.PurchaseOrderID != string.Empty)
                qp.Where += " and PurchaseOrderID = '" + item.PurchaseOrderID + "' ";
            if (item.SerialNum != null && item.SerialNum != string.Empty)
                qp.Where += " and SerialNum = '" + item.SerialNum + "' ";
            if (item.Model != null && item.Model != string.Empty)
                qp.Where += " and Model  like '%" + item.Model + "%' ";
            if (item.Specification != null && item.Specification != string.Empty)
                qp.Where += " and Specification = '" + item.Specification + "' ";
            if (item.Status != 0)
                qp.Where += " and Status = " + (int)item.Status + " ";
            if (item.SupplierID != 0)
                qp.Where += " and SupplierID = " + item.SupplierID + " ";
            if (item.SupplierName != null && item.SupplierName != string.Empty)
                qp.Where += " and SupplierName like '%" + item.SupplierName + "%' ";
            if (item.ProducerID != 0)
                qp.Where += " and ProducerID = " + item.ProducerID + " ";
            if (item.ProducerName != null && item.ProducerName != string.Empty)
                qp.Where += " and ProducerName like '%" + item.ProducerName + "%' ";
            if (item.Purchaser != null && item.Purchaser != string.Empty)
                qp.Where += " and Purchaser = '" + item.Purchaser + "' ";
            if (item.Responsibility != null && item.Responsibility != string.Empty)
                qp.Where += " and Responsibility = '" + item.Responsibility + "' ";
            if (item.ResponsibilityName != null && item.ResponsibilityName != string.Empty)
                qp.Where += " and ResponsibilityName = '" + item.ResponsibilityName + "' ";
            if (item.Checker != null && item.Checker != string.Empty)
                qp.Where += " and Checker = '" + item.Checker + "' ";
            if (item.CategoryID != 0)
                qp.Where += " and CategoryID = " + item.CategoryID + " ";
            if (item.CategoryName != null && item.CategoryName != string.Empty)
                qp.Where += " and CategoryName = '" + item.CategoryName + "' ";
            if (DateTime.Compare(item.PurchaseDate1, DateTime.MinValue) != 0)
                qp.Where += " and PurchaseDate >= '" + item.PurchaseDate1.ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.PurchaseDate2, DateTime.MinValue) != 0)
                qp.Where += " and PurchaseDate <= '" + item.PurchaseDate2.AddDays(1).ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.ExamDate1, DateTime.MinValue) != 0)
                qp.Where += " and ExamDate >= '" + item.ExamDate1.ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.ExamDate2, DateTime.MinValue) != 0)
                qp.Where += " and ExamDate <= '" + item.ExamDate2.AddDays(1).ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.OpeningDate1, DateTime.MinValue) != 0)
                qp.Where += " and OpeningDate >= '" + item.OpeningDate1.ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.OpeningDate2, DateTime.MinValue) != 0)
                qp.Where += " and OpeningDate <= '" + item.OpeningDate2.AddDays(1).ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.FileDate1, DateTime.MinValue) != 0)
                qp.Where += " and FileDate >= '" + item.FileDate1.ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.FileDate2, DateTime.MinValue) != 0)
                qp.Where += " and FileDate <= '" + item.FileDate2.AddDays(1).ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.UpdateTime1, DateTime.MinValue) != 0)
                qp.Where += " and UpdateTime >= '" + item.UpdateTime1.ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.UpdateTime2, DateTime.MinValue) != 0)
                qp.Where += " and UpdateTime <= '" + item.UpdateTime2.AddDays(1).ToShortDateString() + " 00:00" + "' ";
            if (item.DetailLocation != null && item.DetailLocation != string.Empty)
                qp.Where += " and DetailLocation like '%" + item.DetailLocation + "%' ";
            if (item.IsCancel != 0)
            {
                if (item.IsCancel == 1)
                    qp.Where += " and IsCancel = 0 ";
                else if (item.IsCancel == 2)
                    qp.Where += " and IsCancel = 1";
            }

            if (!string.IsNullOrEmpty(item.AssertNumber))
            {
                qp.Where += " and AssertNumber like '%" + item.AssertNumber + "%'";
            }
            //if (item.AddressID != 0)
            //{
            //    qp.Where += " and AddressID=" + item.AddressID;
            //}
            if (!string.IsNullOrEmpty(item.AddressCode))
            {
                qp.Where += " and AddressCode like '" + item.AddressCode + "%' ";
            }

            if (item.MaintainDept != 0)
            {
                IList addresslist = new Address().GetAddressByMaintainDept(item.MaintainDept);
                if (addresslist != null && addresslist.Count > 0)
                {
                    int i = 0;
                    foreach (AddressInfo addressinfo in addresslist)
                    {
                        if (i == 0)
                            qp.Where += " and ( AddressCode like '" + addressinfo.AddressCode + "%' ";
                        else
                            qp.Where += " or AddressCode like '" + addressinfo.AddressCode + "%' ";
                        i++;
                    }
                    qp.Where += " ) ";
                }
                else
                {
                    qp.Where += " and 1=2 ";
                }

                
            }

            if (!string.IsNullOrEmpty(item.AddressName))
            {
                qp.Where += " and AddressName like '%" + item.AddressName + "%'";
            }
            if (!string.IsNullOrEmpty(item.CategoryCode))
            {
                qp.Where += " and CategoryCode like '" + item.CategoryCode + "%'";
            }
            if (item.Count != 0)
            {
                qp.Where += " and Count = " + item.Count + " ";
            }
            if (item.Unit != null && item.Unit != string.Empty)
            {
                qp.Where += " and Unit = '" + item.Unit + "' ";
            }
            if (item.AddressType != 0)  //相反
                qp.Where += " and AddressType != " + (int)item.AddressType + " ";

            if (Decimal.Compare(item.Price1, 0) != 0)
                qp.Where += " and Price >= " + item.Price1 ;
            if (Decimal.Compare(item.Price2, 0) != 0)
                qp.Where += " and Price <= " + item.Price2;
            if (item.MinRepairTimes != null)
            {
                qp.Where += " and MaintenanceTimes >= " + item.MinRepairTimes;
            }
            if (item.MaxRepairTimes >0)
            {
                qp.Where += " and MaintenanceTimes <= " + (item.MaxRepairTimes-1);
            }
            if (item.Warming==9999)
            {
                qp.Where += " and Warming = 9999 "  ;
            }
            if (item.Warming==1)
            {
                qp.Where += " and ( Warming != 9999 or Warming is null )";                
            }
            return qp;
        }

        //********************************* Modified by Xue 2011-7-26 *******************
        public QueryParam GenerateSearchTermForWarehouse(EquipmentSearchInfo item,List<AddressInfo> addressinfor)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = TableName;
            qp.ReturnFields = ReturnFields;
            qp.OrderBy = OrderBy;
            qp.Where = Where;
            if (item.EquipmentID != 0)
                qp.Where += " and EquipmentID = " + item.EquipmentID + " ";
            if (item.EquipmentNO != null && item.EquipmentNO != string.Empty)
                qp.Where += " and EquipmentNO = '" + item.EquipmentNO + "' ";
            if (item.Name != null && item.Name != string.Empty)
                qp.Where += " and Name like '%" + item.Name + "%' ";
            if (item.CompanyID != null && item.CompanyID != string.Empty)
                qp.Where += " and CompanyID = '" + item.CompanyID + "' ";
            if (item.CompanyName != null && item.CompanyName != string.Empty)
                qp.Where += " and CompanyName = '" + item.CompanyName + "' ";
            if (item.SectionID != null && item.SectionID != string.Empty)
                qp.Where += " and SectionID = '" + item.SectionID + "' ";
            if (item.LocationTag != null && item.LocationTag != string.Empty)
                qp.Where += " and LocationTag = '" + item.LocationTag + "' ";
            if (item.LocationID != null && item.LocationID != string.Empty)
                qp.Where += " and LocationID = '" + item.LocationID + "' ";
            if (item.SystemID != null && item.SystemID != string.Empty)
                qp.Where += " and SystemID = '" + item.SystemID + "' ";
            if (item.SystemName != null && item.SystemName != string.Empty)
                qp.Where += " and SystemName = '" + item.SystemName + "' ";
            if (item.PurchaseOrderID != null && item.PurchaseOrderID != string.Empty)
                qp.Where += " and PurchaseOrderID = '" + item.PurchaseOrderID + "' ";
            if (item.SerialNum != null && item.SerialNum != string.Empty)
                qp.Where += " and SerialNum = '" + item.SerialNum + "' ";
            if (item.Model != null && item.Model != string.Empty)
                qp.Where += " and Model  like '%" + item.Model + "%' ";
            if (item.Specification != null && item.Specification != string.Empty)
                qp.Where += " and Specification = '" + item.Specification + "' ";
            if (item.Status != 0)
                qp.Where += " and Status = " + (int)item.Status + " ";
            if (item.SupplierID != 0)
                qp.Where += " and SupplierID = " + item.SupplierID + " ";
            if (item.SupplierName != null && item.SupplierName != string.Empty)
                qp.Where += " and SupplierName like '%" + item.SupplierName + "%' ";
            if (item.ProducerID != 0)
                qp.Where += " and ProducerID = " + item.ProducerID + " ";
            if (item.ProducerName != null && item.ProducerName != string.Empty)
                qp.Where += " and ProducerName like '%" + item.ProducerName + "%' ";
            if (item.Purchaser != null && item.Purchaser != string.Empty)
                qp.Where += " and Purchaser = '" + item.Purchaser + "' ";
            if (item.Responsibility != null && item.Responsibility != string.Empty)
                qp.Where += " and Responsibility = '" + item.Responsibility + "' ";
            if (item.ResponsibilityName != null && item.ResponsibilityName != string.Empty)
                qp.Where += " and ResponsibilityName = '" + item.ResponsibilityName + "' ";
            if (item.Checker != null && item.Checker != string.Empty)
                qp.Where += " and Checker = '" + item.Checker + "' ";
            if (item.CategoryID != 0)
                qp.Where += " and CategoryID = " + item.CategoryID + " ";
            if (item.CategoryName != null && item.CategoryName != string.Empty)
                qp.Where += " and CategoryName = '" + item.CategoryName + "' ";
            if (DateTime.Compare(item.PurchaseDate1, DateTime.MinValue) != 0)
                qp.Where += " and PurchaseDate >= '" + item.PurchaseDate1.ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.PurchaseDate2, DateTime.MinValue) != 0)
                qp.Where += " and PurchaseDate <= '" + item.PurchaseDate2.AddDays(1).ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.ExamDate1, DateTime.MinValue) != 0)
                qp.Where += " and ExamDate >= '" + item.ExamDate1.ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.ExamDate2, DateTime.MinValue) != 0)
                qp.Where += " and ExamDate <= '" + item.ExamDate2.AddDays(1).ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.OpeningDate1, DateTime.MinValue) != 0)
                qp.Where += " and OpeningDate >= '" + item.OpeningDate1.ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.OpeningDate2, DateTime.MinValue) != 0)
                qp.Where += " and OpeningDate <= '" + item.OpeningDate2.AddDays(1).ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.FileDate1, DateTime.MinValue) != 0)
                qp.Where += " and FileDate >= '" + item.FileDate1.ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.FileDate2, DateTime.MinValue) != 0)
                qp.Where += " and FileDate <= '" + item.FileDate2.AddDays(1).ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.UpdateTime1, DateTime.MinValue) != 0)
                qp.Where += " and UpdateTime >= '" + item.UpdateTime1.ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.UpdateTime2, DateTime.MinValue) != 0)
                qp.Where += " and UpdateTime <= '" + item.UpdateTime2.AddDays(1).ToShortDateString() + " 00:00" + "' ";
            if (item.DetailLocation != null && item.DetailLocation != string.Empty)
                qp.Where += " and DetailLocation like '%" + item.DetailLocation + "%' ";
            if (item.IsCancel != 0)
            {
                if (item.IsCancel == 1)
                    qp.Where += " and IsCancel = 0 ";
                else if (item.IsCancel == 2)
                    qp.Where += " and IsCancel = 1";
            }

            if (!string.IsNullOrEmpty(item.AssertNumber))
            {
                qp.Where += " and AssertNumber like '%" + item.AssertNumber + "%' ";
            }
            //if (item.AddressID != 0)
            //{
            //    qp.Where += " and AddressID=" + item.AddressID;
            //}
            //if (!string.IsNullOrEmpty(item.AddressCode))
            //{
            //    qp.Where += " and AddressCode like '" + item.AddressCode + "%' ";
            //}

            //IList warehouseaddresslist = new Address().GetWarehouseAddress();

            if (addressinfor != null && addressinfor.Count > 0)
            {
                int i = 0;
                foreach (AddressInfo addressinfo in addressinfor)
                {
                    if (i == 0)
                        qp.Where += " and ( AddressCode like '" + addressinfo.AddressCode + "%' ";
                    else
                        qp.Where += " or AddressCode like '" + addressinfo.AddressCode + "%' ";
                    i++;
                }
                qp.Where += " ) ";
            }
            else
            {
                qp.Where += " and 1=2 ";
            }

            if (item.MaintainDept != 0)
            {
                IList addresslist = new Address().GetAddressByMaintainDept(item.MaintainDept);
                if (addresslist != null && addresslist.Count > 0)
                {
                    int i = 0;
                    foreach (AddressInfo addressinfo in addresslist)
                    {
                        if (i == 0)
                            qp.Where += " and ( AddressCode like '" + addressinfo.AddressCode + "%' ";
                        else
                            qp.Where += " or AddressCode like '" + addressinfo.AddressCode + "%' ";
                        i++;
                    }
                    qp.Where += " ) ";
                }
                else
                {
                    qp.Where += " and 1=2 ";
                }


            }

            if (!string.IsNullOrEmpty(item.AddressName))
            {
                qp.Where += " and AddressName like '%" + item.AddressName + "%'";
            }
            if (!string.IsNullOrEmpty(item.CategoryCode))
            {
                qp.Where += " and CategoryCode like '" + item.CategoryCode + "%'";
            }
            if (item.Count != 0)
            {
                qp.Where += " and Count = " + item.Count + " ";
            }
            if (item.Unit != null && item.Unit != string.Empty)
            {
                qp.Where += " and Unit = '" + item.Unit + "' ";
            }
            if (item.AddressType != 0)  //相反
                qp.Where += " and AddressType != " + (int)item.AddressType + " ";

            if (Decimal.Compare(item.Price1, 0) != 0)
                qp.Where += " and Price >= " + item.Price1;
            if (Decimal.Compare(item.Price2, 0) != 0)
                qp.Where += " and Price <= '" + item.Price2;

            return qp;
        }
        //********************************* Modification Finished *************************


        public QueryParam GenerateSearchTermForWarehouse(EquipmentSearchInfo item)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = TableName;
            qp.ReturnFields = ReturnFields;
            qp.OrderBy = OrderBy;
            qp.Where = Where;
            if (item.EquipmentID != 0)
                qp.Where += " and EquipmentID = " + item.EquipmentID + " ";
            if (item.EquipmentNO != null && item.EquipmentNO != string.Empty)
                qp.Where += " and EquipmentNO = '" + item.EquipmentNO + "' ";
            if (item.Name != null && item.Name != string.Empty)
                qp.Where += " and Name like '%" + item.Name + "%' ";
            if (item.CompanyID != null && item.CompanyID != string.Empty)
                qp.Where += " and CompanyID = '" + item.CompanyID + "' ";
            if (item.CompanyName != null && item.CompanyName != string.Empty)
                qp.Where += " and CompanyName = '" + item.CompanyName + "' ";
            if (item.SectionID != null && item.SectionID != string.Empty)
                qp.Where += " and SectionID = '" + item.SectionID + "' ";
            if (item.LocationTag != null && item.LocationTag != string.Empty)
                qp.Where += " and LocationTag = '" + item.LocationTag + "' ";
            if (item.LocationID != null && item.LocationID != string.Empty)
                qp.Where += " and LocationID = '" + item.LocationID + "' ";
            if (item.SystemID != null && item.SystemID != string.Empty)
                qp.Where += " and SystemID = '" + item.SystemID + "' ";
            if (item.SystemName != null && item.SystemName != string.Empty)
                qp.Where += " and SystemName = '" + item.SystemName + "' ";
            if (item.PurchaseOrderID != null && item.PurchaseOrderID != string.Empty)
                qp.Where += " and PurchaseOrderID = '" + item.PurchaseOrderID + "' ";
            if (item.SerialNum != null && item.SerialNum != string.Empty)
                qp.Where += " and SerialNum = '" + item.SerialNum + "' ";
            if (item.Model != null && item.Model != string.Empty)
                qp.Where += " and Model  like '%" + item.Model + "%' ";
            if (item.Specification != null && item.Specification != string.Empty)
                qp.Where += " and Specification = '" + item.Specification + "' ";
            if (item.Status != 0)
                qp.Where += " and Status = " + (int)item.Status + " ";
            if (item.SupplierID != 0)
                qp.Where += " and SupplierID = " + item.SupplierID + " ";
            if (item.SupplierName != null && item.SupplierName != string.Empty)
                qp.Where += " and SupplierName like '%" + item.SupplierName + "%' ";
            if (item.ProducerID != 0)
                qp.Where += " and ProducerID = " + item.ProducerID + " ";
            if (item.ProducerName != null && item.ProducerName != string.Empty)
                qp.Where += " and ProducerName like '%" + item.ProducerName + "%' ";
            if (item.Purchaser != null && item.Purchaser != string.Empty)
                qp.Where += " and Purchaser = '" + item.Purchaser + "' ";
            if (item.Responsibility != null && item.Responsibility != string.Empty)
                qp.Where += " and Responsibility = '" + item.Responsibility + "' ";
            if (item.ResponsibilityName != null && item.ResponsibilityName != string.Empty)
                qp.Where += " and ResponsibilityName = '" + item.ResponsibilityName + "' ";
            if (item.Checker != null && item.Checker != string.Empty)
                qp.Where += " and Checker = '" + item.Checker + "' ";
            if (item.CategoryID != 0)
                qp.Where += " and CategoryID = " + item.CategoryID + " ";
            if (item.CategoryName != null && item.CategoryName != string.Empty)
                qp.Where += " and CategoryName = '" + item.CategoryName + "' ";
            if (DateTime.Compare(item.PurchaseDate1, DateTime.MinValue) != 0)
                qp.Where += " and PurchaseDate >= '" + item.PurchaseDate1.ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.PurchaseDate2, DateTime.MinValue) != 0)
                qp.Where += " and PurchaseDate <= '" + item.PurchaseDate2.AddDays(1).ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.ExamDate1, DateTime.MinValue) != 0)
                qp.Where += " and ExamDate >= '" + item.ExamDate1.ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.ExamDate2, DateTime.MinValue) != 0)
                qp.Where += " and ExamDate <= '" + item.ExamDate2.AddDays(1).ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.OpeningDate1, DateTime.MinValue) != 0)
                qp.Where += " and OpeningDate >= '" + item.OpeningDate1.ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.OpeningDate2, DateTime.MinValue) != 0)
                qp.Where += " and OpeningDate <= '" + item.OpeningDate2.AddDays(1).ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.FileDate1, DateTime.MinValue) != 0)
                qp.Where += " and FileDate >= '" + item.FileDate1.ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.FileDate2, DateTime.MinValue) != 0)
                qp.Where += " and FileDate <= '" + item.FileDate2.AddDays(1).ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.UpdateTime1, DateTime.MinValue) != 0)
                qp.Where += " and UpdateTime >= '" + item.UpdateTime1.ToShortDateString() + " 00:00" + "' ";
            if (DateTime.Compare(item.UpdateTime2, DateTime.MinValue) != 0)
                qp.Where += " and UpdateTime <= '" + item.UpdateTime2.AddDays(1).ToShortDateString() + " 00:00" + "' ";
            if (item.DetailLocation != null && item.DetailLocation != string.Empty)
                qp.Where += " and DetailLocation like '%" + item.DetailLocation + "%' ";
            if (item.IsCancel != 0)
            {
                if (item.IsCancel == 1)
                    qp.Where += " and IsCancel = 0 ";
                else if (item.IsCancel == 2)
                    qp.Where += " and IsCancel = 1";
            }

            if (!string.IsNullOrEmpty(item.AssertNumber))
            {
                qp.Where += " and AssertNumber like '%" + item.AssertNumber + "%' ";
            }
            //if (item.AddressID != 0)
            //{
            //    qp.Where += " and AddressID=" + item.AddressID;
            //}
            //if (!string.IsNullOrEmpty(item.AddressCode))
            //{
            //    qp.Where += " and AddressCode like '" + item.AddressCode + "%' ";
            //}

            IList warehouseaddresslist = new Address().GetWarehouseAddress();

            if (warehouseaddresslist != null && warehouseaddresslist.Count > 0)
            {
                int i = 0;
                foreach (AddressInfo addressinfo in warehouseaddresslist)
                {
                    if (i == 0)
                        qp.Where += " and ( AddressCode like '" + addressinfo.AddressCode + "%' ";
                    else
                        qp.Where += " or AddressCode like '" + addressinfo.AddressCode + "%' ";
                    i++;
                }
                qp.Where += " ) ";
            }
            else
            {
                qp.Where += " and 1=2 ";
            }

            if (item.MaintainDept != 0)
            {
                IList addresslist = new Address().GetAddressByMaintainDept(item.MaintainDept);
                if (addresslist != null && addresslist.Count > 0)
                {
                    int i = 0;
                    foreach (AddressInfo addressinfo in addresslist)
                    {
                        if (i == 0)
                            qp.Where += " and ( AddressCode like '" + addressinfo.AddressCode + "%' ";
                        else
                            qp.Where += " or AddressCode like '" + addressinfo.AddressCode + "%' ";
                        i++;
                    }
                    qp.Where += " ) ";
                }
                else
                {
                    qp.Where += " and 1=2 ";
                }


            }

            if (!string.IsNullOrEmpty(item.AddressName))
            {
                qp.Where += " and AddressName like '%" + item.AddressName + "%'";
            }
            if (!string.IsNullOrEmpty(item.CategoryCode))
            {
                qp.Where += " and CategoryCode like '" + item.CategoryCode + "%'";
            }
            if (item.Count != 0)
            {
                qp.Where += " and Count = " + item.Count + " ";
            }
            if (item.Unit != null && item.Unit != string.Empty)
            {
                qp.Where += " and Unit = '" + item.Unit + "' ";
            }
            if (item.AddressType != 0)  //相反
                qp.Where += " and AddressType != " + (int)item.AddressType + " ";

            if (Decimal.Compare(item.Price1, 0) != 0)
                qp.Where += " and Price >= " + item.Price1;
            if (Decimal.Compare(item.Price2, 0) != 0)
                qp.Where += " and Price <= '" + item.Price2;

            return qp;
        }

        public QueryParam GenerateSearchTerm(EquipmentInfoFacade item)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = TableName;
            qp.ReturnFields = ReturnFields;
            qp.OrderBy = OrderBy;
            qp.Where = Where;
            if (item.EquipmentID != 0)
                qp.Where += " and EquipmentID = " + item.EquipmentID + " ";
            if (item.EquipmentNO != null && item.EquipmentNO != string.Empty)
                qp.Where += " and EquipmentNO = '" + item.EquipmentNO + "' ";
            if (item.Name != null && item.Name != string.Empty)
                qp.Where += " and Name like '%" + item.Name + "%' ";
            if (item.CompanyID != null && item.CompanyID != string.Empty)
                qp.Where += " and CompanyID = '" + item.CompanyID + "' ";
            if (item.CompanyName != null && item.CompanyName != string.Empty)
                qp.Where += " and CompanyName = '" + item.CompanyName + "' ";
            if (item.SectionID != null && item.SectionID != string.Empty)
                qp.Where += " and SectionID = '" + item.SectionID + "' ";
            if (item.LocationTag != null && item.LocationTag != string.Empty)
                qp.Where += " and LocationTag = '" + item.LocationTag + "' ";
            if (item.LocationID != null && item.LocationID != string.Empty)
                qp.Where += " and LocationID = '" + item.LocationID + "' ";
            if (item.SystemID != null && item.SystemID != string.Empty)
                qp.Where += " and SystemID = '" + item.SystemID + "' ";
            if (item.SystemName != null && item.SystemName != string.Empty)
                qp.Where += " and SystemName = '" + item.SystemName + "' ";
            if (item.PurchaseOrderID != null && item.PurchaseOrderID != string.Empty)
                qp.Where += " and PurchaseOrderID = '" + item.PurchaseOrderID + "' ";
            if (item.SerialNum != null && item.SerialNum != string.Empty)
                qp.Where += " and SerialNum = '" + item.SerialNum + "' ";
            if (item.Model != null && item.Model != string.Empty)
                qp.Where += " and Model  like '%" + item.Model + "%' ";
            if (item.Specification != null && item.Specification != string.Empty)
                qp.Where += " and Specification = '" + item.Specification + "' ";
            if (item.Status != 0)
                qp.Where += " and Status = " + (int)item.Status + " ";
            if (item.SupplierID != 0)
                qp.Where += " and SupplierID = " + item.SupplierID + " ";
            if (item.SupplierName != null && item.SupplierName != string.Empty)
                qp.Where += " and SupplierName like '%" + item.SupplierName + "%' ";
            if (item.ProducerID != 0)
                qp.Where += " and ProducerID = " + item.ProducerID + " ";
            if (item.ProducerName != null && item.ProducerName != string.Empty)
                qp.Where += " and ProducerName like '%" + item.ProducerName + "%' ";
            if (item.Purchaser != null && item.Purchaser != string.Empty)
                qp.Where += " and Purchaser = '" + item.Purchaser + "' ";
            if (item.Responsibility != null && item.Responsibility != string.Empty)
                qp.Where += " and Responsibility = '" + item.Responsibility + "' ";
            if (item.ResponsibilityName != null && item.ResponsibilityName != string.Empty)
                qp.Where += " and ResponsibilityName = '" + item.ResponsibilityName + "' ";
            if (item.Checker != null && item.Checker != string.Empty)
                qp.Where += " and Checker = '" + item.Checker + "' ";
            if (item.CategoryID != 0)
                qp.Where += " and CategoryID = " + item.CategoryID + " ";
            if (item.CategoryName != null && item.CategoryName != string.Empty)
                qp.Where += " and CategoryName = '" + item.CategoryName + "' ";
            
            if (item.DetailLocation != null && item.DetailLocation != string.Empty)
                qp.Where += " and DetailLocation like '%" + item.DetailLocation + "%' ";

            if (!string.IsNullOrEmpty(item.AssertNumber))
            {
                qp.Where += " and AssertNumber like '%" + item.AssertNumber + "%' ";
            }
            if (item.AddressID != 0)
            {
                qp.Where += " and AddressID=" + item.AddressID;
            }
            if (!string.IsNullOrEmpty(item.AddressCode))
            {
                qp.Where += " and AddressCode like '" + item.AddressCode + "%'";
            }
            if (!string.IsNullOrEmpty(item.AddressName))
            {
                qp.Where += " and AddressName like '%" + item.AddressName + "%'";
            }
            if (!string.IsNullOrEmpty(item.CategoryCode))
            {
                qp.Where += " and CategoryCode like '" + item.CategoryCode + "%'";
            }
            if (item.IsCancel != null)
            {
                qp.Where += " and IsCancel = '" + item.IsCancel + "' ";
            }
            if (item.Count != 0)
            {
                qp.Where += " and Count = " + item.Count + " ";
            }
            if (item.Unit != null && item.Unit != string.Empty)
            {
                qp.Where += " and Unit = '" + item.Unit + "' ";
            }
            if (item.AddressType != 0)  //相反
                qp.Where += " and AddressType != " + (int)item.AddressType + " ";
            
            return qp;
        }

        public IList GetList(QueryParam term, out int recordCount, string companyid)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = TableName;
                    term.ReturnFields = ReturnFields;
                    term.OrderBy = OrderBy;
                    if (companyid != null && companyid != string.Empty)
                        term.Where = Where + " and CompanyID = '" + companyid + "' ";
                    else
                        term.Where = Where;
                }
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException(" 获取设备分页失败", e);
            }
        }
        /// <summary>
        /// 获取设备列表，不包括仓库内的设备信息
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        public IList GetListWithoutWarehouse(QueryParam term, out int recordCount, string companyid)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = TableName;
                    term.ReturnFields = ReturnFields;
                    term.OrderBy = OrderBy;
                    if (companyid != null && companyid != string.Empty)
                        term.Where = Where + " and CompanyID = '" + companyid + "' and AddressType!=" + (int)AddressType.Warehouse + " ";
                    else
                        term.Where = Where + "and AddressType!=" + (int)AddressType.Warehouse + " ";
                }
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException(" 获取设备分页失败", e);
            }
        }
        /// <summary>
        /// 获取当前查询条件下的设备总量
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        public int GetCurrentDeviceCount(QueryParam term, string companyid)
        {
            try
            {
                if (term.TableName == null || term.TableName.Trim() == string.Empty)
                {
                    term.TableName = TableName;
                    term.ReturnFields = ReturnFields;
                    term.OrderBy = OrderBy;
                    if (companyid != null && companyid != string.Empty)
                        term.Where = Where + " and CompanyID = '" + companyid + "' ";
                    else
                        term.Where = Where;
                }
                StringBuilder strSql = new StringBuilder();
                strSql.Append ("select sum([Count]) as CurrentCount from ");
                strSql.Append (term.TableName);
                strSql.Append(term.Where);
                int CurrentCount = 0;
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    if (rd.Read())
                    {
                        if (!Convert.IsDBNull(rd["CurrentCount"]))
                        CurrentCount = Convert.ToInt32(rd["CurrentCount"]);
                    }
                }
                return CurrentCount;
            }
            catch (Exception e)
            {
                throw new DALException(" 获取设备分页失败", e);
            }
        }


        private EquipmentInfoFacade GetDataForEquipmentNO(IDataReader dr)
        {
            EquipmentInfoFacade item = new EquipmentInfoFacade();
            if (!Convert.IsDBNull(dr["CategoryID"]))
                item.CategoryID = Convert.ToInt64(dr["CategoryID"]);
            if (!Convert.IsDBNull(dr["CategoryName"]))
                item.CategoryName = Convert.ToString(dr["CategoryName"]);
            if (!Convert.IsDBNull(dr["PurchaserName"]))
                item.PurchaserName = Convert.ToString(dr["PurchaserName"]);
            if (!Convert.IsDBNull(dr["CheckerName"]))
                item.CheckerName = Convert.ToString(dr["CheckerName"]);
            if (!Convert.IsDBNull(dr["EquipmentID"]))
                item.EquipmentID = Convert.ToInt64(dr["EquipmentID"]);
            if (!Convert.IsDBNull(dr["SerialNum"]))
                item.SerialNum = Convert.ToString(dr["SerialNum"]);
            if (!Convert.IsDBNull(dr["PhotoUrl"]))
                item.PhotoUrl = Convert.ToString(dr["PhotoUrl"]);
            if (!Convert.IsDBNull(dr["Model"]))
                item.Model = Convert.ToString(dr["Model"]);
            if (!Convert.IsDBNull(dr["Specification"]))
                item.Specification = Convert.ToString(dr["Specification"]);
            if (!Convert.IsDBNull(dr["Status"]))
                item.Status = (EquipmentStatus)Convert.ToInt64(dr["Status"]);
            if (!Convert.IsDBNull(dr["SupplierID"]))
                item.SupplierID = Convert.ToInt64(dr["SupplierID"]);
            if (!Convert.IsDBNull(dr["ProducerID"]))
                item.ProducerID = Convert.ToInt64(dr["ProducerID"]);
            if (!Convert.IsDBNull(dr["SupplierName"]))
                item.SupplierName = Convert.ToString(dr["SupplierName"]);
            if (!Convert.IsDBNull(dr["ProducerName"]))
                item.ProducerName = Convert.ToString(dr["ProducerName"]);
            if (!Convert.IsDBNull(dr["Purchaser"]))
                item.Purchaser = Convert.ToString(dr["Purchaser"]);
            if(!Convert.IsDBNull(dr["PurchaseOrderID"]))
            {
                item.PurchaseOrderID = Convert.ToString(dr["PurchaseOrderID"]);
            }

            if (!Convert.IsDBNull(dr["Responsibility"]))
                item.Responsibility = Convert.ToString(dr["Responsibility"]);
            if (!Convert.IsDBNull(dr["Checker"]))
                item.Checker = Convert.ToString(dr["Checker"]);
            if (!Convert.IsDBNull(dr["EquipmentNO"]))
                item.EquipmentNO = Convert.ToString(dr["EquipmentNO"]);
            if (!Convert.IsDBNull(dr["PurchaseDate"]))
                item.PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"]);
            if (!Convert.IsDBNull(dr["ExamDate"]))
                item.ExamDate = Convert.ToDateTime(dr["ExamDate"]);
            if (!Convert.IsDBNull(dr["OpeningDate"]))
                item.OpeningDate = Convert.ToDateTime(dr["OpeningDate"]);
            if (!Convert.IsDBNull(dr["FileDate"]))
                item.FileDate = Convert.ToDateTime(dr["FileDate"]);
            if (!Convert.IsDBNull(dr["WarrantyDate"]))
                item.WarrantyDate = Convert.ToDateTime(dr["WarrantyDate"]);
            if (!Convert.IsDBNull(dr["ServiceLife"]))
                item.ServiceLife = Convert.ToInt64(dr["ServiceLife"]);
            if (!Convert.IsDBNull(dr["Price"]))
                item.Price = Convert.ToDecimal(dr["Price"]);
            if (!Convert.IsDBNull(dr["CategoryID"]))
                item.CategoryID = Convert.ToInt64(dr["CategoryID"]);
            if (!Convert.IsDBNull(dr["DepreciationMethod"]))
                item.DepreciationMethod = Convert.ToInt64(dr["DepreciationMethod"]);
            if (!Convert.IsDBNull(dr["DepreciableLife"]))
                item.DepreciableLife = Convert.ToInt64(dr["DepreciableLife"]);
            if (!Convert.IsDBNull(dr["Name"]))
                item.Name = Convert.ToString(dr["Name"]);
            if (!Convert.IsDBNull(dr["ResidualRate"]))
                item.ResidualRate = Convert.ToDecimal(dr["ResidualRate"]);
            if (!Convert.IsDBNull(dr["MaintenanceTimes"]))
                item.MaintenanceTimes = Convert.ToInt64(dr["MaintenanceTimes"]);
            if (!Convert.IsDBNull(dr["Remark"]))
                item.Remark = Convert.ToString(dr["Remark"]);
            if (!Convert.IsDBNull(dr["IsCancel"]))
                item.IsCancel = Convert.ToBoolean(dr["IsCancel"]);
            if (!Convert.IsDBNull(dr["UpdateTime"]))
                item.UpdateTime = Convert.ToDateTime(dr["UpdateTime"]);
            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);
            if (!Convert.IsDBNull(dr["CompanyName"]))
                item.CompanyName = Convert.ToString(dr["CompanyName"]);
            if (!Convert.IsDBNull(dr["SectionID"]))
                item.SectionID = Convert.ToString(dr["SectionID"]);
            if (!Convert.IsDBNull(dr["LocationTag"]))
                item.LocationTag = Convert.ToString(dr["LocationTag"]);
            if (!Convert.IsDBNull(dr["LocationID"]))
                item.LocationID = Convert.ToString(dr["LocationID"]);
            if (!Convert.IsDBNull(dr["SystemID"]))
                item.SystemID = Convert.ToString(dr["SystemID"]);
            if (!Convert.IsDBNull(dr["SystemName"]))
                item.SystemName = Convert.ToString(dr["SystemName"]);
            if (!Convert.IsDBNull(dr["PurchaseOrderID"]))
                item.PurchaseOrderID = Convert.ToString(dr["PurchaseOrderID"]);
            if (!Convert.IsDBNull(dr["ResponsibilityName"]))
                item.ResponsibilityName = Convert.ToString(dr["ResponsibilityName"]);
            if (!Convert.IsDBNull(dr["NextSplitNO"]))
                item.NextSplitNO = Convert.ToInt32(dr["NextSplitNO"]);
            if (!Convert.IsDBNull(dr["Unit"]))
                item.Unit = Convert.ToString(dr["Unit"]);
            if (!Convert.IsDBNull(dr["Count"]))
                item.Count = Convert.ToInt32(dr["Count"]);
            if (!Convert.IsDBNull(dr["DetailLocation"]))
                item.DetailLocation = Convert.ToString(dr["DetailLocation"]);

            if (!Convert.IsDBNull(dr["AddressID"]))
            {
                item.AddressID = Convert.ToInt64(dr["AddressID"]);
            }
            if (!Convert.IsDBNull(dr["AddressCode"]))
            {
                item.AddressCode = Convert.ToString(dr["AddressCode"]);
            }
            if (!Convert.IsDBNull(dr["AddressName"]))
            {
                item.AddressName = Convert.ToString(dr["AddressName"]);
            }
            if (!Convert.IsDBNull(dr["AssertNumber"]))
            {
                item.AssertNumber = Convert.ToString(dr["AssertNumber"]);
            }
            if (!Convert.IsDBNull(dr["AddressType"]))
            {
                item.AddressType = (AddressType)Convert.ToInt32(dr["AddressType"]);
            }

            return item;
        }
        private EquipmentInfoFacade GetData(IDataReader dr)
        {
            EquipmentInfoFacade item = new EquipmentInfoFacade();
            if (!Convert.IsDBNull(dr["CategoryID"]))
                item.CategoryID = Convert.ToInt64(dr["CategoryID"]);
            if (!Convert.IsDBNull(dr["CategoryName"]))
                item.CategoryName = Convert.ToString(dr["CategoryName"]);
            if (!Convert.IsDBNull(dr["PurchaserName"]))
                item.PurchaserName = Convert.ToString(dr["PurchaserName"]);
            if (!Convert.IsDBNull(dr["CheckerName"]))
                item.CheckerName = Convert.ToString(dr["CheckerName"]);
            if (!Convert.IsDBNull(dr["EquipmentID"]))
                item.EquipmentID = Convert.ToInt64(dr["EquipmentID"]);
            if (!Convert.IsDBNull(dr["SerialNum"]))
                item.SerialNum = Convert.ToString(dr["SerialNum"]);
            if (!Convert.IsDBNull(dr["PhotoUrl"]))
                item.PhotoUrl = Convert.ToString(dr["PhotoUrl"]);
            if (!Convert.IsDBNull(dr["Model"]))
                item.Model = Convert.ToString(dr["Model"]);
            if (!Convert.IsDBNull(dr["Specification"]))
                item.Specification = Convert.ToString(dr["Specification"]);
            if (!Convert.IsDBNull(dr["Status"]))
                item.Status = (EquipmentStatus)Convert.ToInt64(dr["Status"]);
            if (!Convert.IsDBNull(dr["SupplierID"]))
                item.SupplierID = Convert.ToInt64(dr["SupplierID"]);
            if (!Convert.IsDBNull(dr["ProducerID"]))
                item.ProducerID = Convert.ToInt64(dr["ProducerID"]);
            if (!Convert.IsDBNull(dr["SupplierName"]))
                item.SupplierName = Convert.ToString(dr["SupplierName"]);
            if (!Convert.IsDBNull(dr["ProducerName"]))
                item.ProducerName = Convert.ToString(dr["ProducerName"]);
            if (!Convert.IsDBNull(dr["Purchaser"]))
                item.Purchaser = Convert.ToString(dr["Purchaser"]);

            if (!Convert.IsDBNull(dr["Responsibility"]))
                item.Responsibility = Convert.ToString(dr["Responsibility"]);
            if (!Convert.IsDBNull(dr["Checker"]))
                item.Checker = Convert.ToString(dr["Checker"]);
            if (!Convert.IsDBNull(dr["EquipmentNO"]))
                item.EquipmentNO = Convert.ToString(dr["EquipmentNO"]);
            if (!Convert.IsDBNull(dr["PurchaseDate"]))
                item.PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"]);
            if (!Convert.IsDBNull(dr["ExamDate"]))
                item.ExamDate = Convert.ToDateTime(dr["ExamDate"]);
            if (!Convert.IsDBNull(dr["OpeningDate"]))
                item.OpeningDate = Convert.ToDateTime(dr["OpeningDate"]);
            if (!Convert.IsDBNull(dr["FileDate"]))
                item.FileDate = Convert.ToDateTime(dr["FileDate"]);
            if (!Convert.IsDBNull(dr["WarrantyDate"]))
                item.WarrantyDate = Convert.ToDateTime(dr["WarrantyDate"]);
            if (!Convert.IsDBNull(dr["ServiceLife"]))
                item.ServiceLife = Convert.ToInt64(dr["ServiceLife"]);
            if (!Convert.IsDBNull(dr["Price"]))
                item.Price = Convert.ToDecimal(dr["Price"]);
            if (!Convert.IsDBNull(dr["CategoryID"]))
                item.CategoryID = Convert.ToInt64(dr["CategoryID"]);
            if (!Convert.IsDBNull(dr["DepreciationMethod"]))
                item.DepreciationMethod = Convert.ToInt64(dr["DepreciationMethod"]);
            if (!Convert.IsDBNull(dr["DepreciableLife"]))
                item.DepreciableLife = Convert.ToInt64(dr["DepreciableLife"]);
            if (!Convert.IsDBNull(dr["Name"]))
                item.Name = Convert.ToString(dr["Name"]);
            if (!Convert.IsDBNull(dr["ResidualRate"]))
                item.ResidualRate = Convert.ToDecimal(dr["ResidualRate"]);
            if (!Convert.IsDBNull(dr["MaintenanceTimes"]))
                item.MaintenanceTimes = Convert.ToInt64(dr["MaintenanceTimes"]);
            if (!Convert.IsDBNull(dr["Remark"]))
                item.Remark = Convert.ToString(dr["Remark"]);
            if (!Convert.IsDBNull(dr["IsCancel"]))
                item.IsCancel = Convert.ToBoolean(dr["IsCancel"]);
            if (!Convert.IsDBNull(dr["UpdateTime"]))
                item.UpdateTime = Convert.ToDateTime(dr["UpdateTime"]);
            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);
            if (!Convert.IsDBNull(dr["CompanyName"]))
                item.CompanyName = Convert.ToString(dr["CompanyName"]);
            if (!Convert.IsDBNull(dr["SectionID"]))
                item.SectionID = Convert.ToString(dr["SectionID"]);
            
            if (!Convert.IsDBNull(dr["LocationTag"]))
                item.LocationTag = Convert.ToString(dr["LocationTag"]);
            if (!Convert.IsDBNull(dr["LocationID"]))
                item.LocationID = Convert.ToString(dr["LocationID"]);
            if (!Convert.IsDBNull(dr["SystemID"]))
                item.SystemID = Convert.ToString(dr["SystemID"]);
            if (!Convert.IsDBNull(dr["SystemName"]))
                item.SystemName = Convert.ToString(dr["SystemName"]);
            if (!Convert.IsDBNull(dr["PurchaseOrderID"]))
                item.PurchaseOrderID = Convert.ToString(dr["PurchaseOrderID"]);
            
            if (!Convert.IsDBNull(dr["ResponsibilityName"]))
                item.ResponsibilityName = Convert.ToString(dr["ResponsibilityName"]);

            if (!Convert.IsDBNull(dr["NextSplitNO"]))
                item.NextSplitNO = Convert.ToInt32(dr["NextSplitNO"]);
            if (!Convert.IsDBNull(dr["DetailLocation"]))
                item.DetailLocation = Convert.ToString(dr["DetailLocation"]);

            if(!Convert.IsDBNull(dr["AddressID"]))
            {
                item.AddressID = Convert.ToInt64(dr["AddressID"]);
            }
            if (!Convert.IsDBNull(dr["AddressCode"]))
            {
                item.AddressCode = Convert.ToString(dr["AddressCode"]);
            }
            if (!Convert.IsDBNull(dr["AddressName"]))
            {
                item.AddressName = Convert.ToString(dr["AddressName"]);
            }
            if (!Convert.IsDBNull(dr["AssertNumber"]))
            {
                item.AssertNumber = Convert.ToString(dr["AssertNumber"]);
            }
            if (!Convert.IsDBNull(dr["CategoryCode"]))
            {
                item.CategoryCode = Convert.ToString(dr["CategoryCode"]);
            }

            if (!Convert.IsDBNull(dr["Count"]))
            {
                item.Count = Convert.ToInt32(dr["Count"]);
            }

            if (!Convert.IsDBNull(dr["Unit"]))
            {
                item.Unit = Convert.ToString(dr["Unit"]);
            }
            if (!Convert.IsDBNull(dr["AddressType"]))
            {
                item.AddressType = (AddressType)Convert.ToInt32(dr["AddressType"]);
            }

            if (!Convert.IsDBNull(dr["Warming"]))
            {
                item.Warming = Convert.ToInt32(dr["Warming"]);
            }
            return item;
        }

        /// <summary>
        /// 查询库存信息，模糊匹配产品名称、产品型号，by zjf 2009-1-11
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyid">公司ID</param>
        /// <param name="productName">产品名称</param>
        /// <param name="productModel">规格型号</param>
        /// <returns>查询的库存结果</returns>
        public IList QueryStorage(int pageIndex, int pageSize, out int recordCount, string companyid, string productName, string productModel)
        {
            return QueryStorage(pageIndex, pageSize, out recordCount, companyid, productName, productModel, null);
        }

        public IList QueryStorage(int pageIndex, int pageSize, out int recordCount, string companyid, string productName, string productModel, string warehouseid)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = "EquipmentView eq inner join FM2E_WAREHOUSEView wh on  eq.addresscode like wh.addresscode+'%'";
                qp.ReturnFields = "eq.CompanyID,eq.Unit,eq.[Name] as EquipmentName,wh.[Name] as WarehouseName,WarehouseID, eq.Model,count(eq.EquipmentID) as Storage,eq.Warming";
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                if (string.IsNullOrEmpty(warehouseid))
                {
                    qp.Where = " where " +
                    " eq.Name like '%" + productName + "%'";

                    if (!string.IsNullOrEmpty(productModel))
                    {
                        qp.Where += " and eq.Model like '%" + productModel + "%'";
                    }
                    qp.Where += " group by eq.CompanyID,eq.Unit,wh.WarehouseID,wh.[Name],eq.[Name],eq.Model,eq.Warming";
                }
                else
                {
                    qp.Where = " where " +
                        "eq.Name like '%" + productName + "%'";
                    if (!string.IsNullOrEmpty(productModel))
                    {
                        qp.Where += " and eq.Model like '%" + productModel + "%'";
                    }
                     qp.Where += " and wh.WarehouseID ='" + warehouseid + "'" +
                        " group by eq.CompanyID,eq.Unit,wh.WarehouseID,wh.[Name],eq.[Name],eq.Model,eq.Warming";
                }

                qp.GroupKey = "COUNT(eq.EquipmentID)";
                qp.OrderBy = " order by WarehouseID ASC,EquipmentName ASC,Model ASC";


                //File.WriteAllText(HttpContext.Current.Server.MapPath("debug.txt"), qp.ToString());

                return SQLHelper.GetObjectListWithGroupBy(GetDataQueryStorage
                    , qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取库存信息查询分页失败"+e.Message, e);
            }
        }

        /// <summary>
        /// 获取一个库存信息实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private EquipmentStorageInfo GetDataQueryStorage(IDataReader dr)
        {
            EquipmentStorageInfo item = new EquipmentStorageInfo();
            if (!Convert.IsDBNull(dr["CompanyID"]))
                item.CompanyID = Convert.ToString(dr["CompanyID"]);
            if (!Convert.IsDBNull(dr["WarehouseID"]))
                item.WareHouseID = Convert.ToString(dr["WarehouseID"]);
            if (!Convert.IsDBNull(dr["WareHouseName"]))
                item.WareHouseName = Convert.ToString(dr["WareHouseName"]);
            if (!Convert.IsDBNull(dr["EquipmentName"]))
                item.EquipmentName = Convert.ToString(dr["EquipmentName"]);
            if (!Convert.IsDBNull(dr["Model"]))
                item.EquipmentModel = Convert.ToString(dr["Model"]);
            if (!Convert.IsDBNull(dr["Storage"]))
                item.Storage = Convert.ToDecimal(dr["Storage"]);
            if (!Convert.IsDBNull(dr["Unit"]))
                item.Unit = Convert.ToString(dr["Unit"]);
            if (!Convert.IsDBNull(dr["Warming"]))
                item.Warming = Convert.ToInt32(dr["Warming"]);
            //item.Unit = "套/件";
            return item;
        }

        /// <summary>
        /// 获取设备的下一个拆分编号
        /// </summary>
        /// <param name="equipmentNO"></param>
        /// <returns></returns>
        int IEquipment.GetNextSplitNO(string equipmentNO)
        {
            int splitNO = 0;

            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select top 1 NextSplitNO ");
                strSql.Append(" from FM2E_Equipment ");
                strSql.AppendFormat(" where EquipmentNO like '{0}'", equipmentNO);
                strSql.AppendFormat(" update FM2E_Equipment set NextSplitNO=NextSplitNO+1 where EquipmentNO like '{0}';", equipmentNO);


                splitNO = (int)SQLHelper.ExecuteScalar(trans, CommandType.Text, strSql.ToString(), null);
                trans.Commit();
            }
            catch (Exception ex)
            {
                splitNO = 0;
                trans.Rollback();
                throw new DALException("获取设备拆分编号失败", ex);
            }
            finally
            {
                //关闭连接
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
            return splitNO;
        }

        ///// <summary>
        ///// 用于更新设备的维修相关的状态
        ///// </summary>
        ///// <param name="equipmentNO">设备条形码</param>
        ///// <param name="status">设备状态,如果status=0，则不更新状态</param>
        ///// <param name="maintainTimesIncrease">维修次数的增加值，如果maintainTimesIncrease=0，则不更新维修次数</param>
        ///// <param name="updateTime">最近更新时间</param>
        //void IEquipment.UpdateEquipmentMaintainInfo(string equipmentNO, int status, int maintainTimesIncrease, DateTime updateTime)
        //{
        //    try
        //    {
        //        SqlParameter[] param = new SqlParameter[]{
                    
        //            new SqlParameter("@EquipmentNO",SqlDbType.VarChar,20),
        //            new SqlParameter("@Status",SqlDbType.TinyInt,1),
        //            new SqlParameter("@UpdateTime",SqlDbType.DateTime,8)};
        //        param[0].Value = equipmentNO;
        //        param[1].Value = status;
        //        param[2].Value = updateTime;

        //        StringBuilder strSql = new StringBuilder();
        //        if (status > 0)
        //        {
        //            strSql.Append("update FM2E_Equipment ");
        //            strSql.AppendFormat(" set Status=@Status,MaintenanceTimes=MaintenanceTimes+{0},UpdateTime=@UpdateTime ", maintainTimesIncrease);
        //            strSql.Append(" where EquipmentNO=@EquipmentNO");
        //        }
        //        else
        //        {
        //            strSql.Append("update FM2E_Equipment ");
        //            strSql.AppendFormat(" set MaintenanceTimes=MaintenanceTimes+{0},UpdateTime=@UpdateTime ", maintainTimesIncrease);
        //            strSql.Append(" where EquipmentNO=@EquipmentNO");
        //        }

        //        SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), param);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new DALException("更新设备维修状态失败", ex);
        //    }
        //}
        /// <summary>
        /// 更新设备地址信息
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="equipmentno"></param>
        /// <param name="addressid"></param>
        /// <param name="detaillocation"></param>
        public void UpdateEquipmentAddress(SqlTransaction trans, string equipmentno, long addressid, string detaillocation)
        {
            SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@EquipmentNO",SqlDbType.VarChar,20),
                    new SqlParameter("@AddressID",SqlDbType.BigInt),
                    new SqlParameter("@DetailLocation",SqlDbType.NVarChar,50),
                    new SqlParameter("@UpdateTime",SqlDbType.DateTime)};
            param[0].Value = equipmentno;
            param[1].Value = (addressid == 0) ? SqlInt64.Null : addressid;
            param[2].Value = string.IsNullOrEmpty(detaillocation) ? SqlString.Null : detaillocation;
            param[3].Value = DateTime.Now;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_Equipment set");
            strSql.Append(" AddressID=@AddressID,");
            strSql.Append(" DetailLocation=@DetailLocation,");
            strSql.Append(" UpdateTime=@UpdateTime ");
            strSql.Append(" where EquipmentNO=@EquipmentNO");
            if (trans == null)
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), param);
            }
            else
            {
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), param);
            }
        }

        /// <summary>
        /// 用于更新设备的维修相关的状态,具体的参数说明请参考本方法的另一重载方法
        /// </summary>
        /// <param name="updateEquipmentInfo"></param>
        void IEquipment.UpdateEquipmentMaintainInfo(IList updateEquipmentInfo, DbTransaction trans)
        {
            if (updateEquipmentInfo == null)
                return;

            SqlParameter[] param = new SqlParameter[]{
                
                new SqlParameter("@EquipmentNO",SqlDbType.VarChar,20),
                new SqlParameter("@Status",SqlDbType.TinyInt,1),
                new SqlParameter("@UpdateTime",SqlDbType.DateTime,8)};

            foreach (FM2E.Model.Maintain.MaintainEquipmentsUpdateInfo item in updateEquipmentInfo)
            {
                StringBuilder strSql = new StringBuilder();

                if (item.Status > 0)
                {
                    strSql.Append("update FM2E_Equipment ");
                    strSql.AppendFormat(" set Status=@Status,MaintenanceTimes=MaintenanceTimes+{0},UpdateTime=@UpdateTime ", item.MaintainTimesIncrease);
                    strSql.Append(" where EquipmentNO=@EquipmentNO");
                }
                else
                {
                    strSql.Append("update FM2E_Equipment ");
                    strSql.AppendFormat(" set MaintenanceTimes=MaintenanceTimes+{0},UpdateTime=@UpdateTime ", item.MaintainTimesIncrease);
                    strSql.Append(" where EquipmentNO=@EquipmentNO");
                }

                param[0].Value = item.EquipmentNO;
                param[1].Value = item.Status;
                param[2].Value = item.UpdateTime;
                SQLHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, strSql.ToString(), param);
            }
        }

        /// <summary>
        /// 获取相关的设备，即同一拆分设备
        /// </summary>
        /// <param name="equipmentno"></param>
        /// <returns></returns>
        IList IEquipment.GetRelatedDevice(string equipmentno)
        {
            string cmd = string.Format(SELECT_GETRELATED, equipmentno);
            IList list = new List<EquipmentInfoFacade>();
            //try
            //{
            //    using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
            //    {
            //        while (rd.Read())
            //        {
            //            EquipmentInfoFacade item = GetDataForEquipmentNO(rd);
            //            list.Add(item);
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw new DALException("获取相关设备信息失败", e);
            //}
            return list;
        }
        /// <summary>
        /// 根据地址以及系统获取相关的设备总数、故障设备总数
        /// </summary>
        /// <param name="addressCode"></param>
        /// <param name="systemID"></param>
        /// <param name="count">设备总数</param>
        /// <returns>故障设备列表</returns>
        //public IList GetEquipmentCount1(string addressCode, string systemID,out int count)
        //{
        //    count = 0;
        //    ArrayList list = new ArrayList();
        //    try
        //    {
        //        string cmd = "";
        //        cmd += "select count(*) from EquipmentView";
 
        //        string sqlWhere = "";
        //        sqlWhere += " where 1=1 ";
        //        if (!string.IsNullOrEmpty(addressCode.Trim()) && addressCode.Trim() != "00")
        //            sqlWhere += string.Format(" and AddressCode like '{0}%'", addressCode);
           
        //        if (!string.IsNullOrEmpty(systemID))
        //            sqlWhere += string.Format("  and SystemID='{0}'", systemID);

        //        count = (int)SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, cmd + sqlWhere, null);

        //        cmd = "select Name,EquipmentNO,SystemID,SystemName,AddressID,AddressName from EquipmentView ";
        //        sqlWhere += string.Format(" and (Status={0} or Status={1} or Status={2})", (int)EquipmentStatus.BeyondRepair, (int)EquipmentStatus.Failure, (int)EquipmentStatus.FunctionalityRestore);

        //        using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd + sqlWhere, null))
        //        {
        //            while (rd.Read())
        //            {
        //                FaultyEquipmentInfo item = new FaultyEquipmentInfo();

        //                if (!Convert.IsDBNull(rd["Name"]))
        //                    item.EquipmentName = Convert.ToString(rd["Name"]);

        //                if (!Convert.IsDBNull(rd["EquipmentNO"]))
        //                    item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

        //                if (!Convert.IsDBNull(rd["SystemID"]))
        //                    item.SystemID = Convert.ToString(rd["SystemID"]);

        //                if (!Convert.IsDBNull(rd["SystemName"]))
        //                    item.SystemName = Convert.ToString(rd["SystemName"]);

        //                if (!Convert.IsDBNull(rd["AddressID"]))
        //                    item.AddressID = Convert.ToInt64(rd["AddressID"]);

        //                if (!Convert.IsDBNull(rd["AddressName"]))
        //                    item.AddressName = Convert.ToString(rd["AddressName"]);

        //                list.Add(item);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        list.Clear();
        //        throw new DALException("获取设备数量失败", ex);
        //    }
        //    return list;
        //}


        IList IEquipment.GetEquipmentCount(string companyid, long mainteamid, string addressCode, string systemID, out int count)
        {
            count = 0;
            ArrayList list = new ArrayList();
            try
            {
                string cmd = "";
                cmd += "select count(*) from EquipmentView2";

                string sqlWhere = "";
                sqlWhere += " where 1=1 ";

                sqlWhere += "and AddressType!=" + (int)AddressType.Warehouse + " ";

                if (!string.IsNullOrEmpty(addressCode.Trim()) && addressCode.Trim() != "00")
                    sqlWhere += string.Format(" and AddressCode like '{0}%'", addressCode);

                if (!string.IsNullOrEmpty(systemID))
                    sqlWhere += string.Format("  and SystemID='{0}'", systemID);

                if (!string.IsNullOrEmpty(companyid))
                    sqlWhere += string.Format(" and CompanyID='{0}'", companyid);

                //if (mainteamid != 0)
                //    sqlWhere += string.Format(" and mainteamid={0}", mainteamid);

                if (mainteamid != 0)
                {
                    IList addresslist = new Address().GetAddressByMaintainDept(mainteamid);
                    if (addresslist != null && addresslist.Count > 0)
                    {
                        int i = 0;
                        foreach (AddressInfo addressinfo in addresslist)
                        {
                            if (i == 0)
                                sqlWhere += " and ( AddressCode like '" + addressinfo.AddressCode + "%' ";
                            else
                                sqlWhere += " or AddressCode like '" + addressinfo.AddressCode + "%' ";
                            i++;
                        }
                        sqlWhere += " ) ";
                    }
                    else
                    {
                        sqlWhere += " and 1=2 ";
                    }


                }

                //sqlWhere += string.Format(" and Status<>{0} ", (int)MaintainedEquipmentStatus.Scrapped);


                count = (int)SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, cmd + sqlWhere, null);

                cmd = "select Name,EquipmentNO,SystemID,SystemName,AddressID,AddressName,Status from EquipmentView2 ";
                //sqlWhere += string.Format(" and (Status={0} or Status={1} or Status={2})", (int)EquipmentStatus.BeyondRepair, (int)EquipmentStatus.Failure, (int)EquipmentStatus.FunctionalityRestore);

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd + sqlWhere, null))
                {
                    while (rd.Read())
                    {
                        MaintainedEquipmentInfo item = new MaintainedEquipmentInfo();

                        if (!Convert.IsDBNull(rd["Name"]))
                            item.EquipmentName = Convert.ToString(rd["Name"]);

                        if (!Convert.IsDBNull(rd["EquipmentNO"]))
                            item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

                        if (!Convert.IsDBNull(rd["Status"]))
                        {
                            EquipmentStatus status = (EquipmentStatus)(Convert.ToInt32(rd["Status"]));
                            if (status == EquipmentStatus.BeyondRepair)
                            {
                                item.MaintainResult = MaintainedEquipmentStatus.UnFixed;
                            }
                            else if (status == EquipmentStatus.Failure)
                            {
                                item.MaintainResult = MaintainedEquipmentStatus.Wait4Repair;
                            }
                            else if (status == EquipmentStatus.FunctionalityRestore)
                            {
                                item.MaintainResult = MaintainedEquipmentStatus.FunctionalityRestore;
                            }
                        }

                        list.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取设备数量失败", ex);
            }
            return list;
        }

        /// <summary>
        /// 根据地址以及系统获取相关的设备总数、故障设备总数
        /// </summary>
        /// <param name="addressCode"></param>
        /// <param name="systemID"></param>
        /// <param name="count">设备总数</param>
        /// <returns>故障设备列表</returns>
        IList IEquipment.GetEquipmentCount(string addressCode, string systemID, out int count)
        {
            count = 0;
            ArrayList list = new ArrayList();
            try
            {
                string cmd = "";
                cmd += "select count(*) from EquipmentView";

                string sqlWhere = "";
                sqlWhere += " where 1=1 ";
                if (!string.IsNullOrEmpty(addressCode.Trim()) && addressCode.Trim() != "00")
                    sqlWhere += string.Format(" and AddressCode like '{0}%'", addressCode);

                if (!string.IsNullOrEmpty(systemID))
                    sqlWhere += string.Format("  and SystemID='{0}'", systemID);

                count = (int)SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, cmd + sqlWhere, null);

                cmd = "select Name,EquipmentNO,SystemID,SystemName,AddressID,AddressName,Status from EquipmentView ";
                sqlWhere += string.Format(" and (Status={0} or Status={1} or Status={2})", (int)EquipmentStatus.BeyondRepair, (int)EquipmentStatus.Failure, (int)EquipmentStatus.FunctionalityRestore);

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd + sqlWhere, null))
                {
                    while (rd.Read())
                    {
                        MaintainedEquipmentInfo item = new MaintainedEquipmentInfo();

                        if (!Convert.IsDBNull(rd["Name"]))
                            item.EquipmentName = Convert.ToString(rd["Name"]);

                        if (!Convert.IsDBNull(rd["EquipmentNO"]))
                            item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

                        if (!Convert.IsDBNull(rd["Status"]))
                        {
                            EquipmentStatus status = (EquipmentStatus)(Convert.ToInt32(rd["Status"]));
                            if(status == EquipmentStatus.BeyondRepair)
                            {
                                item.MaintainResult = MaintainedEquipmentStatus.UnFixed;
                            }
                            else if (status == EquipmentStatus.Failure)
                            {
                                item.MaintainResult = MaintainedEquipmentStatus.Wait4Repair;
                            }
                            else if (status == EquipmentStatus.FunctionalityRestore)
                            {
                                item.MaintainResult = MaintainedEquipmentStatus.FunctionalityRestore;
                            }
                        }

                        list.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取设备数量失败", ex);
            }
            return list;
        }

        public IList<string> GetEquipmentName(string prefixText, int count)
        {
            IList<string> list = new List<string>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct top  " + count + "   Name from FM2E_Equipment");
            strSql.Append(" where Name like '%'+@Name+'%'");
            SqlParameter[] parameters = {
                new SqlParameter("@Name",SqlDbType.NVarChar,20) ,
                 new SqlParameter("@count",SqlDbType.Int)       
                                        };
            parameters[0].Value = prefixText;
            parameters[1].Value = count;
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        string n = "";
                        if (!Convert.IsDBNull(rd["Name"]))
                            n = Convert.ToString(rd["Name"]);
                        if (!string.IsNullOrEmpty(n))
                            list.Add(n);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("获取设备名称提示失败" + e.Message, e);
            }
            return list;
        }


        /// <summary>
        /// 读取导出设备信息实体
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private EquipmentExportInfo GetExportDate(IDataReader rd)
        {
            EquipmentExportInfo item = new EquipmentExportInfo();

            if (!Convert.IsDBNull(rd["EquipmentNO"]))
                item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);
            if (!Convert.IsDBNull(rd["Name"]))
                item.Name = Convert.ToString(rd["Name"]);
            if (!Convert.IsDBNull(rd["Model"]))
                item.Model = Convert.ToString(rd["Model"]);
            if (!Convert.IsDBNull(rd["Unit"]))
                item.Unit = Convert.ToString(rd["Unit"]);
            if (!Convert.IsDBNull(rd["Count"]))
                item.Count = Convert.ToInt32(rd["Count"]);
            if (!Convert.IsDBNull(rd["CategoryName"]))
                item.CategoryName = Convert.ToString(rd["CategoryName"]);
            if (!Convert.IsDBNull(rd["Price"]))
                item.Price = Convert.ToDecimal(rd["Price"]);
            if (!Convert.IsDBNull(rd["AddressName"]))
                item.AddressName = Convert.ToString(rd["AddressName"]);
            if (!Convert.IsDBNull(rd["DetailLocation"]))
                item.DetailLocation = Convert.ToString(rd["DetailLocation"]);
            if (!Convert.IsDBNull(rd["AssertNumber"]))
                item.AssertNumber = Convert.ToString(rd["AssertNumber"]);
            if (!Convert.IsDBNull(rd["PurchaseDate"]))
                item.PurchaseDate = Convert.ToDateTime(rd["PurchaseDate"]);
            if (!Convert.IsDBNull(rd["SerialNum"]))
                item.SerialNum = Convert.ToString(rd["SerialNum"]);
            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);
            if (!Convert.IsDBNull(rd["CompanyName"]))
                item.CompanyName = Convert.ToString(rd["CompanyName"]);
            if (!Convert.IsDBNull(rd["SystemName"]))
                item.SystemName = Convert.ToString(rd["SystemName"]);

            return item;
        }


        /// <summary>
        /// 获取导出设备信息列表
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public IList GetExportList(QueryParam searchTerm)
        {
            List<EquipmentExportInfo> list = new List<EquipmentExportInfo>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("Select * from EquipmentView ");
                strSql.Append(searchTerm.Where);
                strSql.Append(" order by AddressName asc");

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        list.Add(this.GetExportDate(rd));
                    }
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取卡片列表失败", ex);
            }

            return list;
        }

    }
}
