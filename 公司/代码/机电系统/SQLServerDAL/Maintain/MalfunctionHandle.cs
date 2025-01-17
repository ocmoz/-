﻿using System;
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
using FM2E.Model.System;
using FM2E.Model.Equipment;

namespace FM2E.SQLServerDAL.Maintain
{
    public class MalfunctionHandle : IMalfunctionHandle
    {


        /// <summary>
        /// 获取故障处理单实体信息
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private MalfunctionHandleInfo GetData(IDataReader rd)
        {
            MalfunctionHandleInfo item = new MalfunctionHandleInfo();

            if (!Convert.IsDBNull(rd["ActualFunRestoreTime"]))
                item.ActualFunRestoreTime = Convert.ToInt32(rd["ActualFunRestoreTime"]);

            if (!Convert.IsDBNull(rd["ActualRepairTime"]))
                item.ActualRepairTime = Convert.ToInt32(rd["ActualRepairTime"]);

            if (!Convert.IsDBNull(rd["ActualResponseTime"]))
                item.ActualResponseTime = Convert.ToInt32(rd["ActualResponseTime"]);

            if (!Convert.IsDBNull(rd["AddressDetail"]))
                item.AddressDetail = Convert.ToString(rd["AddressDetail"]);

            if (!Convert.IsDBNull(rd["AddressID"]))
                item.AddressID = Convert.ToInt64(rd["AddressID"]);

            if (!Convert.IsDBNull(rd["AddressName"]))
                item.AddressName = Convert.ToString(rd["AddressName"]);

            if (!Convert.IsDBNull(rd["Attitude"]))
                item.Attitude = (Grade)Convert.ToInt32(rd["Attitude"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["DepartmentID"]))
                item.DepartmentID = Convert.ToInt64(rd["DepartmentID"]);

            if (!Convert.IsDBNull(rd["Effect"]))
                item.Effect = (Grade)Convert.ToInt32(rd["Effect"]);

            if (!Convert.IsDBNull(rd["Feeback"]))
                item.Feeback = Convert.ToString(rd["Feeback"]);

            if (!Convert.IsDBNull(rd["FunRestoreTime"]))
                item.FunRestoreTime = Convert.ToInt32(rd["FunRestoreTime"]);

            if (!Convert.IsDBNull(rd["MaintainDept"]))
                item.MaintainDept = Convert.ToInt64(rd["MaintainDept"]);

            if (!Convert.IsDBNull(rd["MalfunctionDescription"]))
                item.MalfunctionDescription = Convert.ToString(rd["MalfunctionDescription"]);

            if (!Convert.IsDBNull(rd["MalfunctionRank"]))
                item.MalfunctionRank = (MalfunctionRank)Convert.ToInt32(rd["MalfunctionRank"]);

            if (!Convert.IsDBNull(rd["Rationality"]))
                item.Rationality = (Grade)Convert.ToInt32(rd["Rationality"]);

            if (!Convert.IsDBNull(rd["ReceiveDate"]))
                item.ReceiveDate = Convert.ToDateTime(rd["ReceiveDate"]);

            if (!Convert.IsDBNull(rd["Receiver"]))
                item.Receiver = Convert.ToString(rd["Receiver"]);

            if (!Convert.IsDBNull(rd["Recorder"]))
                item.Recorder = Convert.ToString(rd["Recorder"]);

            if (!Convert.IsDBNull(rd["RepairTime"]))
                item.RepairTime = Convert.ToInt32(rd["RepairTime"]);

            if (!Convert.IsDBNull(rd["ReportDate"]))
                item.ReportDate = Convert.ToDateTime(rd["ReportDate"]);

            if (!Convert.IsDBNull(rd["Reporter"]))
                item.Reporter = Convert.ToString(rd["Reporter"]);

            if (!Convert.IsDBNull(rd["ResponseTime"]))
                item.ResponseTime = Convert.ToInt32(rd["ResponseTime"]);

            if (!Convert.IsDBNull(rd["SheetID"]))
                item.SheetID = Convert.ToInt64(rd["SheetID"]);

            if (!Convert.IsDBNull(rd["SheetNO"]))
                item.SheetNO = Convert.ToString(rd["SheetNO"]);

            if (!Convert.IsDBNull(rd["TechnicEvaluate"]))
                item.TechnicEvaluate = (Grade)Convert.ToInt32(rd["TechnicEvaluate"]);

            if (!Convert.IsDBNull(rd["UpdateTime"]))
                item.UpdateTime = Convert.ToDateTime(rd["UpdateTime"]);

            if (!Convert.IsDBNull(rd["RecordDept"]))
                item.RecordDept = Convert.ToInt64(rd["RecordDept"]);

            if (!Convert.IsDBNull(rd["RecordDeptName"]))
                item.RecordDeptName = Convert.ToString(rd["RecordDeptName"]);

            if (!Convert.IsDBNull(rd["SystemID"]))
                item.SystemID = (MalfunctionReason)Convert.ToInt32(rd["SystemID"]);

            if (!Convert.IsDBNull(rd["ResponseUnit"]))
                item.ResponseUnit = (TimeUnits)Convert.ToInt32(rd["ResponseUnit"]);

            if (!Convert.IsDBNull(rd["FunRestoreUnit"]))
                item.FunRestoreUnit = (TimeUnits)Convert.ToInt32(rd["FunRestoreUnit"]);

            if (!Convert.IsDBNull(rd["RepairUnit"]))
                item.RepairUnit = (TimeUnits)Convert.ToInt32(rd["RepairUnit"]);

            if (!Convert.IsDBNull(rd["DepartmentName"]))
                item.DepartmentName = Convert.ToString(rd["DepartmentName"]);

            if (!Convert.IsDBNull(rd["RecorderName"]))
                item.RecorderName = Convert.ToString(rd["RecorderName"]);

            if (!Convert.IsDBNull(rd["Status"]))
                item.Status = (MalfunctionHandleStatus)Convert.ToInt32(rd["Status"]);

            if (!Convert.IsDBNull(rd["MaintainDeptName"]))
                item.MaintainDeptName = Convert.ToString(rd["MaintainDeptName"]);

            if (!Convert.IsDBNull(rd["CancelReason"]))
                item.CancelReason = Convert.ToString(rd["CancelReason"]);

            if (!Convert.IsDBNull(rd["Canceler"]))
                item.Canceler = Convert.ToString(rd["Canceler"]);

            if (!Convert.IsDBNull(rd["Investigator"]))
                item.Investigator = Convert.ToString(rd["Investigator"]);

            if (!Convert.IsDBNull(rd["IsResponseInTime"]))
                item.IsResponseInTime = Convert.ToBoolean(rd["IsResponseInTime"]);

            if (!Convert.IsDBNull(rd["IsFunRestoreInTime"]))
                item.IsFunRestoreInTime = Convert.ToBoolean(rd["IsFunRestoreInTime"]);

            if (!Convert.IsDBNull(rd["IsRepairInTime"]))
                item.IsRepairInTime = Convert.ToBoolean(rd["IsRepairInTime"]);

            if (!Convert.IsDBNull(rd["IsPrinted"]))
                item.IsPrinted = Convert.ToBoolean(rd["IsPrinted"]);

            if (!Convert.IsDBNull(rd["IsDelivered"]))
                item.IsDelivered = Convert.ToBoolean(rd["IsDelivered"]);

            if (!Convert.IsDBNull(rd["editor"]))
                item.Editor = Convert.ToString(rd["editor"]);

            if (!Convert.IsDBNull(rd["editreason"]))
                item.Editreason = Convert.ToString(rd["editreason"]);

            if (!Convert.IsDBNull(rd["ReportDate2"]))
                item.ReportDate2 = Convert.ToDateTime(rd["ReportDate2"]);

            if (!Convert.IsDBNull(rd["Stationcheck"]))
                item.Stationcheck = Convert.ToBoolean(rd["Stationcheck"]);

            if (!Convert.IsDBNull(rd["IsDelayApply"]))
                item.IsDelayApply = Convert.ToBoolean(rd["IsDelayApply"]);
            //if (!Convert.IsDBNull(rd["IsDelayCheck1"]))
            //    item.IsDelayCheck1 = Convert.ToInt32(rd["IsDelayCheck1"]);
            //if (!Convert.IsDBNull(rd["IsDelayCheck2"]))
            //    item.IsDelayCheck2 = Convert.ToInt32(rd["IsDelayCheck2"]);
            if (!Convert.IsDBNull(rd["CancelApplyTime"]))
                item.CancelApplyTime = Convert.ToDateTime(rd["CancelApplyTime"]);

            if (!Convert.IsDBNull(rd["CancelApproveName"]))
                item.CancelApproveName = Convert.ToString(rd["CancelApproveName"]);

            if (!Convert.IsDBNull(rd["CancelApproveRemark"]))
                item.CancelApproveRemark = Convert.ToString(rd["CancelApproveRemark"]);

            if (!Convert.IsDBNull(rd["CancelApproveTime"]))
                item.CancelApproveTime = Convert.ToDateTime(rd["CancelApproveTime"]);

            if (!Convert.IsDBNull(rd["CancelApproveResult"]))
                item.CancelApproveResult = Convert.ToString(rd["CancelApproveResult"]);

            if (!Convert.IsDBNull(rd["DelayApplyTime"]))
                item.DelayApplyTime = Convert.ToDateTime(rd["DelayApplyTime"]);

            if (!Convert.IsDBNull(rd["FirstConsultTime"]))
                item.FirstConsultTime = Convert.ToInt32(rd["FirstConsultTime"]);

            if (!Convert.IsDBNull(rd["FirstConsultUnit"]))
                item.FirstConsultUnit = (TimeUnits)Convert.ToInt32(rd["FirstConsultUnit"]);

            if (!Convert.IsDBNull(rd["FirstApproveTime"]))
                item.FirstApproveTime = Convert.ToDateTime(rd["FirstApproveTime"]);

            if (!Convert.IsDBNull(rd["FirstDelayRemark"]))
                item.FirstDelayRemark = Convert.ToString(rd["FirstDelayRemark"]);

            if (!Convert.IsDBNull(rd["FirstDelayApprove"]))
                item.FirstDelayApprove = Convert.ToInt32(rd["FirstDelayApprove"]);

            if (!Convert.IsDBNull(rd["FirstApproveName"]))
                item.FirstApproveName = Convert.ToString(rd["FirstApproveName"]);

            if (!Convert.IsDBNull(rd["FinalConsultTime"]))
                item.FinalConsultTime = Convert.ToInt32(rd["FinalConsultTime"]);

            if (!Convert.IsDBNull(rd["FinalConsultUnit"]))
                item.FinalConsultUnit = (TimeUnits)Convert.ToInt32(rd["FinalConsultUnit"]);

            if (!Convert.IsDBNull(rd["FinalDelayRemark"]))
                item.FinalDelayRemark = Convert.ToString(rd["FinalDelayRemark"]);

            if (!Convert.IsDBNull(rd["FinalAprroveTime"]))
                item.FinalAprroveTime = Convert.ToDateTime(rd["FinalAprroveTime"]);

            if (!Convert.IsDBNull(rd["FinalDelayApprove"]))
                item.FinalDelayApprove = Convert.ToInt32(rd["FinalDelayApprove"]);

            if (!Convert.IsDBNull(rd["FinalApproveName"]))
                item.FinalApproveName = Convert.ToString(rd["FinalApproveName"]);

            if (!Convert.IsDBNull(rd["TimeConfirmer"]))
                item.TimeConfirmer = Convert.ToString(rd["TimeConfirmer"]);

            if (!Convert.IsDBNull(rd["DelayForTime"]))
                item.DelayForTime = Convert.ToInt32(rd["DelayForTime"]);

            if (!Convert.IsDBNull(rd["DelayForUnit"]))
                item.DelayForUnit = (TimeUnits)Convert.ToInt32(rd["DelayForUnit"]);

            if (!Convert.IsDBNull(rd["NextApproverPersonName"]))
                item.NextApproverPersonName = Convert.ToString(rd["NextApproverPersonName"]);

            if (!Convert.IsDBNull(rd["attachment"]))
                item.attachment = Convert.ToString(rd["attachment"]);

            return item;
        }
        /// <summary>
        /// 获取故障设备实体信息
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private FaultyEquipmentInfo GetFaultyEquipmentData(IDataReader rd)
        {
            FaultyEquipmentInfo item = new FaultyEquipmentInfo();

            if (!Convert.IsDBNull(rd["EquipmentName"]))
                item.EquipmentName = Convert.ToString(rd["EquipmentName"]);

            if (!Convert.IsDBNull(rd["EquipmentNO"]))
                item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["SheetID"]))
                item.SheetID = Convert.ToInt64(rd["SheetID"]);

            return item;
        }
        /// <summary>
        /// 获取经过维修的设备实体信息
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private MaintainedEquipmentInfo GetMaintainedEquipmentData(IDataReader rd)
        {
            MaintainedEquipmentInfo item = new MaintainedEquipmentInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["MaintainID"]))
                item.MaintainID = Convert.ToInt64(rd["MaintainID"]);

            if (!Convert.IsDBNull(rd["SheetID"]))
                item.SheetID = Convert.ToInt64(rd["SheetID"]);

            if (!Convert.IsDBNull(rd["EquipmentNO"]))
                item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

            if (!Convert.IsDBNull(rd["EquipmentName"]))
                item.EquipmentName = Convert.ToString(rd["EquipmentName"]);

            if (!Convert.IsDBNull(rd["Model"]))
                item.Model = Convert.ToString(rd["Model"]);

            if (!Convert.IsDBNull(rd["SerialNum"]))
                item.SerialNum = Convert.ToString(rd["SerialNum"]);

            if (!Convert.IsDBNull(rd["LastAddress"]))
                item.LastAddress = Convert.ToString(rd["LastAddress"]);

            if (!Convert.IsDBNull(rd["MaintainResult"]))
                item.MaintainResult = (MaintainedEquipmentStatus)Convert.ToInt32(rd["MaintainResult"]);

            if (!Convert.IsDBNull(rd["MaintainFee"]))
                item.MaintainFee = Convert.ToDecimal(rd["MaintainFee"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["MaintainDate"]))
                item.MaintainDate = Convert.ToDateTime(rd["MaintainDate"]);

            return item;
        }
        /// <summary>
        /// 获取经过维修的维修人员实体信息
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private MalfunctionMaintainStaffInfo GetMalfunctionMaintainStaffData(IDataReader rd)
        {
            MalfunctionMaintainStaffInfo item = new MalfunctionMaintainStaffInfo();
            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["MaintainID"]))
                item.MaintainID = Convert.ToInt64(rd["MaintainID"]);

            if (!Convert.IsDBNull(rd["MaintenanceStaff"]))
                item.MaintenanceStaff = Convert.ToString(rd["MaintenanceStaff"]);

            return item;
        }
        /// <summary>
        /// 获取经过维修设备的设备零件
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private MaintainedEquipmentPartInfo GetMaintainedEquipmentPartData(IDataReader rd)
        {
            MaintainedEquipmentPartInfo item = new MaintainedEquipmentPartInfo();
            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["MaintainedEquipmentID"]))
                item.MaintainedEquipmentID = Convert.ToInt64(rd["MaintainedEquipmentID"]);

            if (!Convert.IsDBNull(rd["PartName"]))
                item.PartName = Convert.ToString(rd["PartName"]);

            if (!Convert.IsDBNull(rd["MaintainFee"]))
                item.MaintainFee = Convert.ToDecimal(rd["MaintainFee"]);

            return item;
        }
        /// <summary>
        /// 获取故障单维修实体信息
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private MalfuncitonMaintainInfo GetMaintainInfo(IDataReader rd)
        {
            MalfuncitonMaintainInfo item = new MalfuncitonMaintainInfo();

            if (!Convert.IsDBNull(rd["MaintainID"]))
                item.MaintainID = Convert.ToInt64(rd["MaintainID"]);

            if (!Convert.IsDBNull(rd["SheetID"]))
                item.SheetID = Convert.ToInt64(rd["SheetID"]);

            if (!Convert.IsDBNull(rd["MaintenanceStaff"]))
                item.MaintenanceStaff = Convert.ToString(rd["MaintenanceStaff"]);

            if (!Convert.IsDBNull(rd["MaintenanceStaffName"]))
                item.MaintenanceStaffName = Convert.ToString(rd["MaintenanceStaffName"]);

            if (!Convert.IsDBNull(rd["MaintenanceTeam"]))
                item.MaintenanceTeam = Convert.ToString(rd["MaintenanceTeam"]);

            if (!Convert.IsDBNull(rd["MaintenanceDetail"]))
                item.MaintenanceDetail = Convert.ToString(rd["MaintenanceDetail"]);

            if (!Convert.IsDBNull(rd["MaintenanceDescription"]))
                item.MaintenanceDescription = Convert.ToString(rd["MaintenanceDescription"]);

            if (!Convert.IsDBNull(rd["MaintenanceMethod"]))
                item.MaintenanceMethod = Convert.ToString(rd["MaintenanceMethod"]);

            if (!Convert.IsDBNull(rd["RepairSituation"]))
                item.RepairSituation = (RepairSituation)Convert.ToInt32(rd["RepairSituation"]);

            if (!Convert.IsDBNull(rd["TotalFee"]))
                item.TotalFee = Convert.ToDecimal(rd["TotalFee"]);

            if (!Convert.IsDBNull(rd["UpdateTime"]))
                item.UpdateTime = Convert.ToDateTime(rd["UpdateTime"]);

            if (!Convert.IsDBNull(rd["IsDelivered"]))
                item.IsDelivered = Convert.ToBoolean(rd["IsDelivered"]);

            if (!Convert.IsDBNull(rd["noequipment"]))
                item.NoEquipment = Convert.ToString(rd["noequipment"]);

            return item;
        }
        /// <summary>
        /// 获取设备维修记录实体信息
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private EquipmentMaintainRecordInfo GetEquipmemtMaintainRecordInfo(IDataReader rd)
        {
            EquipmentMaintainRecordInfo item = new EquipmentMaintainRecordInfo();

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);


            if (!Convert.IsDBNull(rd["DepartmentID"]))
                item.DepartmentID = Convert.ToInt64(rd["DepartmentID"]);

            if (!Convert.IsDBNull(rd["DepartmentName"]))
                item.DepartmentName = Convert.ToString(rd["DepartmentName"]);

            if (!Convert.IsDBNull(rd["EquipmentName"]))
                item.EquipmentName = Convert.ToString(rd["EquipmentName"]);

            if (!Convert.IsDBNull(rd["EquipmentNO"]))
                item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["MaintainDate"]))
                item.MaintainDate = Convert.ToDateTime(rd["MaintainDate"]);

            if (!Convert.IsDBNull(rd["MaintainDept"]))
                item.MaintainDept = Convert.ToInt64(rd["MaintainDept"]);

            if (!Convert.IsDBNull(rd["MaintainDeptName"]))
                item.MaintainDeptName = Convert.ToString(rd["MaintainDeptName"]);

            if (!Convert.IsDBNull(rd["MaintainFee"]))
                item.MaintainFee = Convert.ToDecimal(rd["MaintainFee"]);

            if (!Convert.IsDBNull(rd["MaintainResult"]))
                item.MaintainResult = (MaintainedEquipmentStatus)Convert.ToInt32(rd["MaintainResult"]);

            if (!Convert.IsDBNull(rd["MalfunctionRank"]))
                item.MalfunctionRank = (MalfunctionRank)Convert.ToInt32(rd["MalfunctionRank"]);

            if (!Convert.IsDBNull(rd["Model"]))
                item.Model = Convert.ToString(rd["Model"]);

            if (!Convert.IsDBNull(rd["Remark"]))
                item.Remark = Convert.ToString(rd["Remark"]);

            if (!Convert.IsDBNull(rd["ReportDate"]))
                item.ReportDate = Convert.ToDateTime(rd["ReportDate"]);

            if (!Convert.IsDBNull(rd["SheetID"]))
                item.SheetID = Convert.ToInt64(rd["SheetID"]);

            if (!Convert.IsDBNull(rd["SheetNO"]))
                item.SheetNO = Convert.ToString(rd["SheetNO"]);

            if (!Convert.IsDBNull(rd["SystemID"]))
                item.SystemID = Convert.ToString(rd["SystemID"]);

            return item;
        }
        /// <summary>
        /// 获取设备流转记录
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private TransferRecord GetTransferRecordInfo(IDataReader rd)
        {
            TransferRecord item = new TransferRecord();

            if (!Convert.IsDBNull(rd["SheetNO"]))
                item.SheetNO = Convert.ToString(rd["SheetNO"]);

            if (!Convert.IsDBNull(rd["Name"]))
                item.Name = Convert.ToString(rd["Name"]);

            if (!Convert.IsDBNull(rd["Url"]))
                item.Url = Convert.ToString(rd["Url"]);
            if (!Convert.IsDBNull(rd["Type"]))
                item.Type = Convert.ToString(rd["Type"]);

            if (!Convert.IsDBNull(rd["Status"]))
                item.Status = (EquipmentStatus)Convert.ToInt32(rd["Status"]);
            if (!Convert.IsDBNull(rd["ReportDate"]))
                item.ReportDate = Convert.ToDateTime(rd["ReportDate"]);

            return item;
        }
        /// <summary>
        /// 获取故障单修改记录实体信息
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private MalfunctionModifyRecordInfo GetModifyRecordData(IDataReader rd)
        {
            MalfunctionModifyRecordInfo item = new MalfunctionModifyRecordInfo();

            if (!Convert.IsDBNull(rd["ID"]))
                item.ID = Convert.ToInt64(rd["ID"]);

            if (!Convert.IsDBNull(rd["SheetID"]))
                item.SheetID = Convert.ToInt64(rd["SheetID"]);

            if (!Convert.IsDBNull(rd["Modifier"]))
                item.Modifier = Convert.ToString(rd["Modifier"]);

            if (!Convert.IsDBNull(rd["ModifierName"]))
                item.ModifierName = Convert.ToString(rd["ModifierName"]);

            if (!Convert.IsDBNull(rd["ModifyDescription"]))
                item.ModifyDescription = Convert.ToString(rd["ModifyDescription"]);

            if (!Convert.IsDBNull(rd["ModifyDate"]))
                item.ModifyDate = Convert.ToDateTime(rd["ModifyDate"]);

            return item;
        }
        /// <summary>
        /// 获取故障报修统计结果信息
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private MalfunctionStatisticInfo GetMalfunctionStatisticInfo(IDataReader rd)
        {
            MalfunctionStatisticInfo item = new MalfunctionStatisticInfo();

            if (!Convert.IsDBNull(rd["Count"]))
                item.Count = Convert.ToInt32(rd["Count"]);

            if (!Convert.IsDBNull(rd["MaintainDept"]))
                item.MaintainDept = Convert.ToInt64(rd["MaintainDept"]);

            if (!Convert.IsDBNull(rd["MalfunctionRank"]))
                item.MalfunctionRank = (MalfunctionRank)Convert.ToInt32(rd["MalfunctionRank"]);

            if (!Convert.IsDBNull(rd["Status"]))
                item.Status = (MalfunctionHandleStatus)Convert.ToInt32(rd["Status"]);

            return item;
        }

        private MalfunctionStatisticInfo GetMalfunctionStatisticInfo2(IDataReader rd)
        {
            MalfunctionStatisticInfo item = new MalfunctionStatisticInfo();

            if (!Convert.IsDBNull(rd["MaintainDept"]))
                item.MaintainDept = Convert.ToInt64(rd["MaintainDept"]);

            if (!Convert.IsDBNull(rd["MalfunctionRank"]))
                item.MalfunctionRank = (MalfunctionRank)Convert.ToInt32(rd["MalfunctionRank"]);

            if (!Convert.IsDBNull(rd["Status"]))
                item.Status = (MalfunctionHandleStatus)Convert.ToInt32(rd["Status"]);

            if (!Convert.IsDBNull(rd["ReportDate"]))
                item.ReportDate = Convert.ToDateTime(rd["ReportDate"]);

            if (!Convert.IsDBNull(rd["RepairTime"]))
                item.RepairTime = Convert.ToInt32(rd["RepairTime"]);

            if (!Convert.IsDBNull(rd["RepairUnit"]))
                item.RepairUnit = Convert.ToInt32(rd["RepairUnit"]);

            if (!Convert.IsDBNull(rd["ActualRepairTime"]))
                item.ActualRepairTime = Convert.ToInt32(rd["ActualRepairTime"]);



            return item;
        }
        #region IMalfunctionHandle 成员
        /// <summary>
        /// 获取所有的故障处理单
        /// </summary>
        /// <returns></returns>
        IList IMalfunctionHandle.GetAllMalfunctionSheets()
        {
            ArrayList list = new ArrayList();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM FM2E_MalfunctionSheetView ");

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                        list.Add(GetData(rd));
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取故障处理单列表失败", ex);
            }
            return list;
        }
        /// <summary>
        /// 获取某张故障处理单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MalfunctionHandleInfo IMalfunctionHandle.GetMalfunctionSheet(long id)
        {
            MalfunctionHandleInfo item = null;
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from FM2E_MalfunctionSheetView ");
                strSql.Append(" where SheetID=@SheetID ");
                strSql.Append(" order by UpdateTime desc ");
                SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.BigInt,8)};
                parameters[0].Value = id;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                        item = GetData(rd);
                }

                StringBuilder strSelectEquipments = new StringBuilder();
                strSelectEquipments.Append("Select * from FM2E_FaultyEquipments ");
                strSelectEquipments.Append(" where SheetID=@SheetID");
                ArrayList equipments = new ArrayList();
                using (SqlDataReader rd = SQLHelper.ExecuteReader(trans, CommandType.Text, strSelectEquipments.ToString(), parameters))
                {
                    while (rd.Read())
                        equipments.Add(GetFaultyEquipmentData(rd));

                }

                item.FaultyEquipments = equipments;
                trans.Commit();
            }
            catch (Exception ex)
            {
                item = null;
                trans.Rollback();
                throw new DALException("获取故障处理单失败", ex);
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
            return item;
        }

        MalfunctionHandleInfo IMalfunctionHandle.GetMalfunctionSheet(DbTransaction transin, long id)
        {
            MalfunctionHandleInfo item = null;
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务


                if (transin != null)
                    trans = (SqlTransaction)transin;
                else
                {
                    conn = new SqlConnection(SQLHelper.ConnectionString);
                    conn.Open();
                    trans = conn.BeginTransaction();
                }
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from FM2E_MalfunctionSheetView ");
                strSql.Append(" where SheetID=@SheetID ");
                strSql.Append(" order by UpdateTime desc ");
                SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.BigInt,8)};
                parameters[0].Value = id;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                        item = GetData(rd);
                }

                StringBuilder strSelectEquipments = new StringBuilder();
                strSelectEquipments.Append("Select * from FM2E_FaultyEquipments ");
                strSelectEquipments.Append(" where SheetID=@SheetID");
                ArrayList equipments = new ArrayList();
                using (SqlDataReader rd = SQLHelper.ExecuteReader(trans, CommandType.Text, strSelectEquipments.ToString(), parameters))
                {
                    while (rd.Read())
                        equipments.Add(GetFaultyEquipmentData(rd));

                }

                item.FaultyEquipments = equipments;
                //trans.Commit();
            }
            catch (Exception ex)
            {
                item = null;
                trans.Rollback();
                throw new DALException("获取故障处理单失败", ex);
            }
            finally
            {
                //关闭连接
                //if (trans != null)
                //{
                //    trans.Dispose();
                //    trans = null;
                //}
                if (conn != null)
                {
                    trans.Dispose();
                    trans = null;
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
            return item;
        }
        /// <summary>
        /// 根据查询条件获取故障处理单
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList IMalfunctionHandle.GetMalfunctionSheets(QueryParam term, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectListWithDistinct(this.GetData, term, out recordCount);
            }
            catch (Exception ex)
            {
                throw new DALException("获取故障处理单列表分页失败", ex);
            }
        }
        /// <summary>
        /// 根据查询条件获取故障处理单(不包括已撤消的故障单)，不支持分页
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList IMalfunctionHandle.GetMalfunctionSheets(MalfunctionSearchInfo term)
        {
            ArrayList list = new ArrayList();
            try
            {
                string strJoinTable = "";
                string strSqlWhere = CreateSqlTerm2(term, out strJoinTable);
                string cmd = "select a.* from FM2E_MalfunctionSheetView a " + strJoinTable + " ";
                cmd += strSqlWhere;
                cmd += " and a.Status<>" + (int)MalfunctionHandleStatus.Canceled;
                cmd += " order by a.UpdateTime desc";

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (rd.Read())
                        list.Add(GetData(rd));
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取故障处理单列表失败", ex);
            }
            return list;
        }

        /// <summary>
        /// 获取设备维修记录
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList IMalfunctionHandle.GetEquipmentMaintainRecords(QueryParam term, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetEquipmemtMaintainRecordInfo, term, out recordCount);
            }
            catch (Exception ex)
            {
                throw new DALException("获取维修记录失败", ex);
            }
        }
        /// <summary>
        /// 获取设备流转记录
        /// </summary>
        IList IMalfunctionHandle.GetTransferRecord(QueryParam term, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetTransferRecordInfo, term, out recordCount);
            }
            catch (Exception ex)
            {
                throw new DALException("获取维修记录失败", ex);
            }
        }
        /// <summary>
        /// 添加故障处理单
        /// </summary>
        /// <param name="model"></param>
        long IMalfunctionHandle.AddMalfunctionSheet(MalfunctionHandleInfo model, DbTransaction trans)
        {
            //先插入消息
            long id = AddMalfunctionSheet((SqlTransaction)trans, model);

            //插入消息对象列表
            UpdateFaultyEquipments((SqlTransaction)trans, model.FaultyEquipments, id);

            return id;
        }
        /// <summary>
        /// 插入故障处理单主体
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private long AddMalfunctionSheet(SqlTransaction trans, MalfunctionHandleInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_MalfunctionHandle(");
            strSql.Append("Recorder,RecordDept,MaintainDept,MalfunctionRank,ResponseTime,ResponseUnit,ActualResponseTime,FunRestoreTime,FunRestoreUnit,ActualFunRestoreTime,RepairTime,RepairUnit,ActualRepairTime,SheetNO,Receiver,ReceiveDate,Effect,TechnicEvaluate,Attitude,Rationality,Feeback,UpdateTime,CompanyID,DepartmentID,Reporter,ReportDate,AddressID,AddressDetail,MalfunctionDescription,SystemID,Status,CancelReason,Canceler,Investigator,IsResponseInTime,IsFunRestoreInTime,IsRepairInTime,IsPrinted,IsDelivered,ReportDate2,Stationcheck,IsDelayApply,CancelApplyTime,CancelApproveName,CancelApproveRemark,CancelApproveTime,CancelApproveResult,DelayApplyTime,FirstConsultTime,FirstConsultUnit,FirstApproveTime,FirstDelayRemark,FirstDelayApprove,FirstApproveName,FinalConsultTime,FinalConsultUnit,FinalDelayRemark,FinalAprroveTime,FinalDelayApprove,FinalApproveName,TimeConfirmer,DelayForTime,DelayForUnit)");
            strSql.Append(" values (");
            strSql.Append("@Recorder,@RecordDept,@MaintainDept,@MalfunctionRank,@ResponseTime,@ResponseUnit,@ActualResponseTime,@FunRestoreTime,@FunRestoreUnit,@ActualFunRestoreTime,@RepairTime,@RepairUnit,@ActualRepairTime,@SheetNO,@Receiver,@ReceiveDate,@Effect,@TechnicEvaluate,@Attitude,@Rationality,@Feeback,@UpdateTime,@CompanyID,@DepartmentID,@Reporter,@ReportDate,@AddressID,@AddressDetail,@MalfunctionDescription,@SystemID,@Status,@CancelReason,@Canceler,@Investigator,@IsResponseInTime,@IsFunRestoreInTime,@IsRepairInTime,@IsPrinted,@IsDelivered,@ReportDate2,@Stationcheck,@IsDelayApply,@CancelApplyTime,@CancelApproveName,@CancelApproveRemark,@CancelApproveTime,@CancelApproveResult,@DelayApplyTime,@FirstConsultTime,@FirstConsultUnit,@FirstApproveTime,@FirstDelayRemark,@FirstDelayApprove,@FirstApproveName,@FinalConsultTime,@FinalConsultUnit,@FinalDelayRemark,@FinalAprroveTime,@FinalDelayApprove,@FinalApproveName,@TimeConfirmer,@DelayForTime,@DelayForUnit)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@Recorder", SqlDbType.VarChar,20),
					new SqlParameter("@RecordDept", SqlDbType.BigInt,8),
					new SqlParameter("@MaintainDept", SqlDbType.BigInt,8),
					new SqlParameter("@MalfunctionRank", SqlDbType.TinyInt,1),
					new SqlParameter("@ResponseTime", SqlDbType.Int,4),
                    new SqlParameter("@ResponseUnit", SqlDbType.TinyInt,1),
					new SqlParameter("@ActualResponseTime", SqlDbType.Int,4),
					new SqlParameter("@FunRestoreTime", SqlDbType.Int,4),
                    new SqlParameter("@FunRestoreUnit", SqlDbType.TinyInt,1),
					new SqlParameter("@ActualFunRestoreTime", SqlDbType.Int,4),
					new SqlParameter("@RepairTime", SqlDbType.Int,4),
                    new SqlParameter("@RepairUnit", SqlDbType.TinyInt,1),
					new SqlParameter("@ActualRepairTime", SqlDbType.Int,4),
					new SqlParameter("@SheetNO", SqlDbType.VarChar,20),
					new SqlParameter("@Receiver", SqlDbType.VarChar,20),
					new SqlParameter("@ReceiveDate", SqlDbType.DateTime),
					new SqlParameter("@Effect", SqlDbType.TinyInt,1),
					new SqlParameter("@TechnicEvaluate", SqlDbType.TinyInt,1),
					new SqlParameter("@Attitude", SqlDbType.TinyInt,1),
					new SqlParameter("@Rationality", SqlDbType.TinyInt,1),
					new SqlParameter("@Feeback", SqlDbType.NVarChar,200),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@DepartmentID", SqlDbType.BigInt,8),
					new SqlParameter("@Reporter", SqlDbType.NVarChar,10),
					new SqlParameter("@ReportDate", SqlDbType.DateTime),
					new SqlParameter("@AddressID", SqlDbType.BigInt,8),
					new SqlParameter("@AddressDetail", SqlDbType.NVarChar,100),
					new SqlParameter("@MalfunctionDescription", SqlDbType.NVarChar,200),
                    new SqlParameter("@SystemID",SqlDbType.VarChar,2),
                    new SqlParameter("@Status",SqlDbType.Int,4),
                    new SqlParameter("@CancelReason",SqlDbType.NVarChar,200),
                    new SqlParameter("@Canceler",SqlDbType.VarChar,20),
                    new SqlParameter("@Investigator",SqlDbType.VarChar,20),
                    new SqlParameter("@IsResponseInTime",SqlDbType.Bit,1),
                    new SqlParameter("@IsFunRestoreInTime",SqlDbType.Bit,1),
                    new SqlParameter("@IsRepairInTime",SqlDbType.Bit,1),
                    new SqlParameter("@IsDelivered",SqlDbType.Bit,1),
                    new SqlParameter("@IsPrinted",SqlDbType.Bit,1),
                    new SqlParameter("@ReportDate2", SqlDbType.DateTime),
                    new SqlParameter("@Stationcheck",SqlDbType.Bit,1),
                    new SqlParameter("@IsDelayApply",SqlDbType.Bit,1),
                    new SqlParameter("@CancelApplyTime",SqlDbType.DateTime),
	                new SqlParameter("@CancelApproveName",SqlDbType.VarChar,20 ),
		            new SqlParameter("@CancelApproveRemark",SqlDbType.NVarChar,200) ,
		            new SqlParameter("@CancelApproveTime",SqlDbType.DateTime),
                    new SqlParameter("@CancelApproveResult",SqlDbType.NVarChar,50 ),
                	new SqlParameter("@DelayApplyTime",SqlDbType.DateTime ),
	                new SqlParameter("@FirstConsultTime",SqlDbType.Int,4 ),
	                new SqlParameter("@FirstConsultUnit",SqlDbType.TinyInt,1 ),
	                new SqlParameter("@FirstApproveTime",SqlDbType.DateTime ),
	                new SqlParameter("@FirstDelayRemark",SqlDbType.NVarChar,200 ),
	                new SqlParameter("@FirstDelayApprove",SqlDbType.Int,4 ),
	                new SqlParameter("@FirstApproveName",SqlDbType.VarChar,20 ),
	                new SqlParameter("@FinalConsultTime",SqlDbType.Int,4 ),
	                new SqlParameter("@FinalConsultUnit",SqlDbType.TinyInt,1 ),
	                new SqlParameter("@FinalDelayRemark",SqlDbType.NVarChar,200 ),
	                new SqlParameter("@FinalAprroveTime",SqlDbType.DateTime ),
	                new SqlParameter("@FinalDelayApprove",SqlDbType.Int,4 ),
	                new SqlParameter("@FinalApproveName",SqlDbType.VarChar,20 ),
	                new SqlParameter("@TimeConfirmer",SqlDbType.VarChar,20 ),
                    new SqlParameter("@DelayForTime",SqlDbType.Int,4 ),
	                new SqlParameter("@DelayForUnit",SqlDbType.TinyInt,1 ),
                                        };
            parameters[0].Value = model.Recorder;
            parameters[1].Value = model.RecordDept;
            parameters[2].Value = model.MaintainDept;
            parameters[3].Value = model.MalfunctionRank;
            parameters[4].Value = model.ResponseTime;
            parameters[5].Value = model.ResponseUnit;
            parameters[6].Value = model.ActualResponseTime;
            //   parameters[7].Value = model.ActualResponseUnit;
            parameters[7].Value = model.FunRestoreTime;
            parameters[8].Value = model.FunRestoreUnit;
            parameters[9].Value = model.ActualFunRestoreTime;
            //  parameters[11].Value = model.ActualFunRestoreUnit;
            parameters[10].Value = model.RepairTime;
            parameters[11].Value = model.RepairUnit;
            parameters[12].Value = model.ActualRepairTime;
            //   parameters[15].Value = model.ActualRepairUnit;
            parameters[13].Value = model.SheetNO;
            parameters[14].Value = model.Receiver;
            parameters[15].Value = DateTime.Compare(model.ReceiveDate, DateTime.MinValue) != 0 ? model.ReceiveDate : SqlDateTime.Null;
            parameters[16].Value = model.Effect;
            parameters[17].Value = model.TechnicEvaluate;
            parameters[18].Value = model.Attitude;
            parameters[19].Value = model.Rationality;
            parameters[20].Value = model.Feeback;
            parameters[21].Value = DateTime.Compare(model.UpdateTime, DateTime.MinValue) != 0 ? model.UpdateTime : SqlDateTime.Null;
            parameters[22].Value = model.CompanyID;
            parameters[23].Value = model.DepartmentID;
            parameters[24].Value = model.Reporter;
            parameters[25].Value = DateTime.Compare(model.ReportDate, DateTime.MinValue) != 0 ? model.ReportDate : SqlDateTime.Null;
            parameters[26].Value = model.AddressID;
            parameters[27].Value = model.AddressDetail;
            parameters[28].Value = model.MalfunctionDescription;
            parameters[29].Value = (int)model.SystemID;
            parameters[30].Value = model.Status;
            parameters[31].Value = model.CancelReason;
            parameters[32].Value = model.Canceler;
            parameters[33].Value = model.Investigator;
            parameters[34].Value = model.IsResponseInTime;
            parameters[35].Value = model.IsFunRestoreInTime;
            parameters[36].Value = model.IsRepairInTime;
            parameters[37].Value = model.IsDelivered;//== null ? false : model.IsDelivered;
            parameters[38].Value = model.IsPrinted;
            parameters[39].Value = DateTime.Compare(model.ReportDate2, DateTime.MinValue) != 0 ? model.ReportDate2 : SqlDateTime.Null;
            parameters[40].Value = model.Stationcheck;
            parameters[41].Value = model.IsDelayApply;
            parameters[42].Value = DateTime.Compare(model.CancelApplyTime, DateTime.MinValue) != 0 ? model.CancelApplyTime : SqlDateTime.Null;
            parameters[43].Value = model.CancelApproveName != null ? model.CancelApproveName : SqlString.Null;
            parameters[44].Value = model.CancelApproveRemark != null ? model.CancelApproveRemark : SqlString.Null;
            parameters[45].Value = DateTime.Compare(model.CancelApproveTime, DateTime.MinValue) != 0 ? model.CancelApproveTime : SqlDateTime.Null;
            parameters[46].Value = model.CancelApproveResult != null ? model.CancelApproveResult : SqlString.Null;
            parameters[47].Value = DateTime.Compare(model.DelayApplyTime, DateTime.MinValue) != 0 ? model.DelayApplyTime : SqlDateTime.Null;
            parameters[48].Value = model.FirstConsultTime != null ? model.FirstConsultTime : SqlInt32.Null;
            parameters[49].Value = model.FirstConsultUnit;
            parameters[50].Value = DateTime.Compare(model.FirstApproveTime, DateTime.MinValue) != 0 ? model.FirstApproveTime : SqlDateTime.Null;
            parameters[51].Value = model.FirstDelayRemark != null ? model.FirstDelayRemark : SqlString.Null;
            parameters[52].Value = model.FirstDelayApprove;
            parameters[53].Value = model.FirstApproveName != null ? model.FirstApproveName : SqlString.Null;
            parameters[54].Value = model.FinalConsultTime;
            parameters[55].Value = model.FinalConsultUnit;
            parameters[56].Value = model.FinalDelayRemark != null ? model.FinalDelayRemark : SqlString.Null;
            parameters[57].Value = DateTime.Compare(model.FinalAprroveTime, DateTime.MinValue) != 0 ? model.FinalAprroveTime : SqlDateTime.Null;
            parameters[58].Value = model.FinalDelayApprove;
            parameters[59].Value = model.FinalApproveName != null ? model.FinalApproveName : SqlString.Null;
            parameters[60].Value = model.TimeConfirmer != null ? model.TimeConfirmer : SqlString.Null;
            parameters[61].Value = model.DelayForTime;
            parameters[62].Value = model.DelayForUnit;
            long id = (long)SQLHelper.ExecuteScalar(trans, CommandType.Text, strSql.ToString(), parameters);
            return id;
        }
        /// <summary>
        /// 插入故障设备
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="equipments"></param>
        /// <param name="id"></param>
        private void UpdateFaultyEquipments(SqlTransaction trans, IList equipments, long id)
        {
            //先删除
            StringBuilder strDel = new StringBuilder();
            strDel.Append("delete FM2E_FaultyEquipments ");
            strDel.Append(" where SheetID=@SheetID ");
            SqlParameter[] paramDel = {
					new SqlParameter("@SheetID", SqlDbType.BigInt)};
            paramDel[0].Value = id;
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strDel.ToString(), paramDel);

            if (equipments == null || equipments.Count == 0)
                return;

            //后插入
            StringBuilder strInsert = new StringBuilder();
            strInsert.Append("insert into FM2E_FaultyEquipments(");
            strInsert.Append("SheetID,EquipmentNO,EquipmentName)");
            strInsert.Append(" values (");
            strInsert.Append("@SheetID,@EquipmentNO,@EquipmentName)");
            strInsert.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.BigInt,8),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
					new SqlParameter("@EquipmentName", SqlDbType.NVarChar,20)};

            foreach (FaultyEquipmentInfo model in equipments)
            {
                parameters[0].Value = id;
                parameters[1].Value = model.EquipmentNO;
                parameters[2].Value = model.EquipmentName;

                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strInsert.ToString(), parameters);
            }
        }
        /// <summary>
        /// 更新故障处理单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="updateSubTable">是否更新子表</param>
        void IMalfunctionHandle.UpdateMalfunctionSheet(MalfunctionHandleInfo model, bool updateSubTable, DbTransaction trans)
        {

            //先插入消息
            UpdateMalfunctionSheet((SqlTransaction)trans, model);

            if (updateSubTable)
            {
                //插入消息对象列表
                UpdateFaultyEquipments((SqlTransaction)trans, model.FaultyEquipments, model.SheetID);
            }
        }
        /// <summary>
        /// 更新故障处理单主体
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="model"></param>
        void UpdateMalfunctionSheet(SqlTransaction trans, MalfunctionHandleInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FM2E_MalfunctionHandle set ");
            strSql.Append("Recorder=@Recorder,");
            strSql.Append("RecordDept=@RecordDept,");
            strSql.Append("MaintainDept=@MaintainDept,");
            strSql.Append("MalfunctionRank=@MalfunctionRank,");
            strSql.Append("ResponseTime=@ResponseTime,");
            strSql.Append("ResponseUnit=@ResponseUnit,");
            strSql.Append("ActualResponseTime=@ActualResponseTime,");
            strSql.Append("FunRestoreTime=@FunRestoreTime,");
            strSql.Append("FunRestoreUnit=@FunRestoreUnit,");
            strSql.Append("ActualFunRestoreTime=@ActualFunRestoreTime,");
            strSql.Append("RepairTime=@RepairTime,");
            strSql.Append("RepairUnit=@RepairUnit,");
            strSql.Append("ActualRepairTime=@ActualRepairTime,");
            strSql.Append("SheetNO=@SheetNO,");
            strSql.Append("Receiver=@Receiver,");
            strSql.Append("ReceiveDate=@ReceiveDate,");
            strSql.Append("Effect=@Effect,");
            strSql.Append("TechnicEvaluate=@TechnicEvaluate,");
            strSql.Append("Attitude=@Attitude,");
            strSql.Append("Rationality=@Rationality,");
            strSql.Append("Feeback=@Feeback,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("DepartmentID=@DepartmentID,");
            strSql.Append("Reporter=@Reporter,");
            strSql.Append("ReportDate=@ReportDate,");
            strSql.Append("AddressID=@AddressID,");
            strSql.Append("AddressDetail=@AddressDetail,");
            strSql.Append("MalfunctionDescription=@MalfunctionDescription,");
            strSql.Append("SystemID=@SystemID,");
            strSql.Append("Status=@Status,");
            strSql.Append("CancelReason=@CancelReason,");
            strSql.Append("Canceler=@Canceler,");
            strSql.Append("Investigator=@Investigator,");
            strSql.Append("IsResponseInTime=@IsResponseInTime,");
            strSql.Append("IsFunRestoreInTime=@IsFunRestoreInTime,");
            strSql.Append("IsRepairInTime=@IsRepairInTime,");
            strSql.Append("IsPrinted=@IsPrinted,");
            strSql.Append("IsDelivered=@IsDelivered,");
            strSql.Append("editor=@editor,");
            strSql.Append("editreason=@editreason,");
            strSql.Append("Stationcheck=@Stationcheck,");
            strSql.Append("IsDelayApply=@IsDelayApply,");
            strSql.Append("CancelApplyTime=@CancelApplyTime,");
            strSql.Append("CancelApproveName=@CancelApproveName,");
            strSql.Append("CancelApproveRemark=@CancelApproveRemark,");
            strSql.Append("CancelApproveTime=@CancelApproveTime,");
            strSql.Append("CancelApproveResult=@CancelApproveResult,");
            strSql.Append("DelayApplyTime=@DelayApplyTime,");
            strSql.Append("FirstConsultTime=@FirstConsultTime,");
            strSql.Append("FirstConsultUnit=@FirstConsultUnit,");
            strSql.Append("FirstApproveTime=@FirstApproveTime,");
            strSql.Append("FirstDelayRemark=@FirstDelayRemark,");
            strSql.Append("FirstDelayApprove=@FirstDelayApprove,");
            strSql.Append("FirstApproveName=@FirstApproveName,");
            strSql.Append("FinalConsultTime=@FinalConsultTime,");
            strSql.Append("FinalConsultUnit=@FinalConsultUnit,");
            strSql.Append("FinalDelayRemark=@FinalDelayRemark,");
            strSql.Append("FinalAprroveTime=@FinalAprroveTime,");
            strSql.Append("FinalDelayApprove=@FinalDelayApprove,");
            strSql.Append("FinalApproveName=@FinalApproveName,");
            strSql.Append("TimeConfirmer=@TimeConfirmer,");
            strSql.Append("DelayForTime=@DelayForTime,");
            strSql.Append("DelayForUnit=@DelayForUnit,");
            strSql.Append("attachment=@attachment");

            strSql.Append(" where SheetID=@SheetID ");

            SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.BigInt,8),
					new SqlParameter("@Recorder", SqlDbType.VarChar,20),
					new SqlParameter("@RecordDept", SqlDbType.BigInt,8),
					new SqlParameter("@MaintainDept", SqlDbType.BigInt,8),
					new SqlParameter("@MalfunctionRank", SqlDbType.TinyInt,1),
				    new SqlParameter("@ResponseTime", SqlDbType.Int,4),
                    new SqlParameter("@ResponseUnit", SqlDbType.TinyInt,1),
					new SqlParameter("@ActualResponseTime", SqlDbType.Int,4),
					new SqlParameter("@FunRestoreTime", SqlDbType.Int,4),
                    new SqlParameter("@FunRestoreUnit", SqlDbType.TinyInt,1),
					new SqlParameter("@ActualFunRestoreTime", SqlDbType.Int,4),
					new SqlParameter("@RepairTime", SqlDbType.Int,4),
                    new SqlParameter("@RepairUnit", SqlDbType.TinyInt,1),
					new SqlParameter("@ActualRepairTime", SqlDbType.Int,4),
					new SqlParameter("@SheetNO", SqlDbType.VarChar,20),
					new SqlParameter("@Receiver", SqlDbType.VarChar,20),
					new SqlParameter("@ReceiveDate", SqlDbType.DateTime),
					new SqlParameter("@Effect", SqlDbType.TinyInt,1),
					new SqlParameter("@TechnicEvaluate", SqlDbType.TinyInt,1),
					new SqlParameter("@Attitude", SqlDbType.TinyInt,1),
					new SqlParameter("@Rationality", SqlDbType.TinyInt,1),
					new SqlParameter("@Feeback", SqlDbType.NVarChar,200),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@DepartmentID", SqlDbType.BigInt,8),
					new SqlParameter("@Reporter", SqlDbType.NVarChar,10),
					new SqlParameter("@ReportDate", SqlDbType.DateTime),
					new SqlParameter("@AddressID", SqlDbType.BigInt,8),
					new SqlParameter("@AddressDetail", SqlDbType.NVarChar,100),
					new SqlParameter("@MalfunctionDescription", SqlDbType.NVarChar,200),
                    new SqlParameter("@SystemID", SqlDbType.VarChar,2),
                    new SqlParameter("@Status",SqlDbType.Int,4),
                    new SqlParameter("@CancelReason",SqlDbType.NVarChar,200),
                    new SqlParameter("@Canceler",SqlDbType.VarChar,20),
                    new SqlParameter("@Investigator",SqlDbType.VarChar,20),
                    new SqlParameter("@IsResponseInTime",SqlDbType.Bit,1),
                    new SqlParameter("@IsFunRestoreInTime",SqlDbType.Bit,1),
                    new SqlParameter("@IsRepairInTime",SqlDbType.Bit,1),
                    new SqlParameter("@IsDelivered",SqlDbType.Bit,1),
                    new SqlParameter("@IsPrinted",SqlDbType.Bit,1),
                    new SqlParameter("@editor",SqlDbType.NVarChar,50),
                    new SqlParameter("@editreason",SqlDbType.NVarChar,2000),
                    new SqlParameter("@Stationcheck",SqlDbType.Bit,1),
                    new SqlParameter("@IsDelayApply",SqlDbType.Bit,1),
                    new SqlParameter("@CancelApplyTime",SqlDbType.DateTime),
	                new SqlParameter("@CancelApproveName",SqlDbType.VarChar,20 ),
		            new SqlParameter("@CancelApproveRemark",SqlDbType.NVarChar,200) ,
		            new SqlParameter("@CancelApproveTime",SqlDbType.DateTime),
                    new SqlParameter("@CancelApproveResult",SqlDbType.NVarChar,50 ),
                	new SqlParameter("@DelayApplyTime",SqlDbType.DateTime ),
	                new SqlParameter("@FirstConsultTime",SqlDbType.Int,4 ),
	                new SqlParameter("@FirstConsultUnit",SqlDbType.TinyInt,1 ),
	                new SqlParameter("@FirstApproveTime",SqlDbType.DateTime ),
	                new SqlParameter("@FirstDelayRemark",SqlDbType.NVarChar,200 ),
	                new SqlParameter("@FirstDelayApprove",SqlDbType.Int,4 ),
	                new SqlParameter("@FirstApproveName",SqlDbType.VarChar,20 ),
	                new SqlParameter("@FinalConsultTime",SqlDbType.Int,4 ),
	                new SqlParameter("@FinalConsultUnit",SqlDbType.TinyInt,1 ),
	                new SqlParameter("@FinalDelayRemark",SqlDbType.NVarChar,200 ),
	                new SqlParameter("@FinalAprroveTime",SqlDbType.DateTime ),
	                new SqlParameter("@FinalDelayApprove",SqlDbType.Int,4 ),
	                new SqlParameter("@FinalApproveName",SqlDbType.VarChar,20 ),
	                new SqlParameter("@TimeConfirmer",SqlDbType.VarChar,20 ),
                    new SqlParameter("@DelayForTime",SqlDbType.Int,4 ),
	                new SqlParameter("@DelayForUnit",SqlDbType.TinyInt,1 ),
                    new SqlParameter("@attachment",SqlDbType.NVarChar,200 )
                                        };
            parameters[0].Value = model.SheetID;
            parameters[1].Value = model.Recorder;
            parameters[2].Value = model.RecordDept;
            parameters[3].Value = model.MaintainDept;
            parameters[4].Value = model.MalfunctionRank;
            parameters[5].Value = model.ResponseTime;
            parameters[6].Value = model.ResponseUnit;
            parameters[7].Value = model.ActualResponseTime;
            // parameters[8].Value = model.ActualResponseUnit;
            parameters[8].Value = model.FunRestoreTime;
            parameters[9].Value = model.FunRestoreUnit;
            parameters[10].Value = model.ActualFunRestoreTime;
            // parameters[12].Value = model.ActualFunRestoreUnit;
            parameters[11].Value = model.RepairTime;
            parameters[12].Value = model.RepairUnit;
            parameters[13].Value = model.ActualRepairTime;
            // parameters[16].Value = model.ActualRepairUnit;
            parameters[14].Value = model.SheetNO;
            parameters[15].Value = model.Receiver;
            parameters[16].Value = model.ReceiveDate != DateTime.MinValue ? model.ReceiveDate : SqlDateTime.Null;
            parameters[17].Value = model.Effect;
            parameters[18].Value = model.TechnicEvaluate;
            parameters[19].Value = model.Attitude;
            parameters[20].Value = model.Rationality;
            parameters[21].Value = model.Feeback;
            parameters[22].Value = model.UpdateTime != DateTime.MinValue ? model.UpdateTime : SqlDateTime.Null;
            parameters[23].Value = model.CompanyID;
            parameters[24].Value = model.DepartmentID;
            parameters[25].Value = model.Reporter;
            parameters[26].Value = model.ReportDate != DateTime.MinValue ? model.ReportDate : SqlDateTime.Null;
            parameters[27].Value = model.AddressID;
            parameters[28].Value = model.AddressDetail;
            parameters[29].Value = model.MalfunctionDescription;
            parameters[30].Value = (int)model.SystemID;
            parameters[31].Value = model.Status;
            parameters[32].Value = model.CancelReason;
            parameters[33].Value = model.Canceler;
            parameters[34].Value = model.Investigator;
            parameters[35].Value = model.IsResponseInTime;
            parameters[36].Value = model.IsFunRestoreInTime;
            parameters[37].Value = model.IsRepairInTime;
            parameters[38].Value = model.IsDelivered;// == null ? false : model.IsDelivered;
            parameters[39].Value = model.IsPrinted;
            parameters[40].Value = model.Editor == null ? SqlString.Null : model.Editor;
            parameters[41].Value = model.Editreason == null ? SqlString.Null : model.Editreason;
            parameters[42].Value = model.Stationcheck;
            if (model.IsDelayApply == null)
            {
                parameters[43].Value = false;
            }
            parameters[43].Value = model.IsDelayApply;


            parameters[44].Value = model.CancelApplyTime != DateTime.MinValue ? model.CancelApplyTime : SqlDateTime.Null;
            parameters[45].Value = model.CancelApproveName != null ? model.CancelApproveName : SqlString.Null;
            parameters[46].Value = model.CancelApproveRemark != null ? model.CancelApproveRemark : SqlString.Null;
            parameters[47].Value = model.CancelApproveTime != DateTime.MinValue ? model.CancelApproveTime : SqlDateTime.Null;
            parameters[48].Value = model.CancelApproveResult != null ? model.CancelApproveResult : SqlString.Null;
            parameters[49].Value = model.DelayApplyTime != DateTime.MinValue ? model.DelayApplyTime : SqlDateTime.Null;
            parameters[50].Value = model.FirstConsultTime;
            parameters[51].Value = model.FirstConsultUnit;
            parameters[52].Value = model.FirstApproveTime != DateTime.MinValue ? model.FirstApproveTime : SqlDateTime.Null;
            parameters[53].Value = model.FirstDelayRemark != null ? model.FirstDelayRemark : SqlString.Null;
            parameters[54].Value = model.FirstDelayApprove;
            parameters[55].Value = model.FirstApproveName != null ? model.FirstApproveName : SqlString.Null;
            parameters[56].Value = model.FinalConsultTime;
            parameters[57].Value = model.FinalConsultUnit;
            parameters[58].Value = model.FinalDelayRemark != null ? model.FinalDelayRemark : SqlString.Null;
            parameters[59].Value = model.FinalAprroveTime != DateTime.MinValue ? model.FinalAprroveTime : SqlDateTime.Null;
            parameters[60].Value = model.FinalDelayApprove;
            parameters[61].Value = model.FinalApproveName != null ? model.FinalApproveName : SqlString.Null;
            parameters[62].Value = model.TimeConfirmer != null ? model.TimeConfirmer : SqlString.Null;
            parameters[63].Value = model.DelayForTime;
            parameters[64].Value = model.DelayForUnit;
            parameters[65].Value = model.attachment != null ? model.attachment : SqlString.Null; ;

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        }

        void IMalfunctionHandle.UpdateWorkflowInstanceNextUserName(long id)
        {

            using (SqlConnection con = new SqlConnection(SQLHelper.ConnectionString))
            {
                try
                {
                    con.Open();
                    string sql = string.Format("update FM2E_WorkflowInstance set NextUserName = DelegateUserName,DelegateUserName = null where DataID = {0}", id);
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 删除故障处理单
        /// </summary>
        /// <param name="id"></param>
        void IMalfunctionHandle.DelMalfunctionSheet(long id, DbTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FM2E_MalfunctionHandle ");
            strSql.Append(" where SheetID=@SheetID ");
            SqlParameter[] parameters = {
				new SqlParameter("@SheetID", SqlDbType.BigInt)};
            parameters[0].Value = id;

            SQLHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 添加维修信息并更新故障处理单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="item"></param>
        void IMalfunctionHandle.AddMaintainRecord(MalfunctionHandleInfo model, MalfuncitonMaintainInfo item, DbTransaction trans)
        {
            //先更新故障处理单
            this.UpdateMalfunctionSheet((SqlTransaction)trans, model);
            //添加维修信息
            long maintainID = this.AddMaintainRecord((SqlTransaction)trans, item);
            //更新已维修的设备列表
            this.UpdateMaintainedEquipments((SqlTransaction)trans, maintainID, item.MaintainedEquipments);
            //更新维修人员列表
            this.UpdateMaintainStaff((SqlTransaction)trans, maintainID, item.MaintainStaff);
        }
        /// <summary>
        /// 添加维修信息
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="item"></param>
        private long AddMaintainRecord(SqlTransaction trans, MalfuncitonMaintainInfo item)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_MalfunctionMaintainDetail(");
            strSql.Append("SheetID,MaintenanceStaff,MaintenanceDetail,MaintenanceDescription,MaintenanceMethod,RepairSituation,TotalFee,UpdateTime,IsDelivered,noequipment)");
            strSql.Append(" values (");
            strSql.Append("@SheetID,@MaintenanceStaff,@MaintenanceDetail,@MaintenanceDescription,@MaintenanceMethod,@RepairSituation,@TotalFee,@UpdateTime,@IsDelivered,@noequipment)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.BigInt,8),
					new SqlParameter("@MaintenanceStaff", SqlDbType.VarChar,20),
					new SqlParameter("@MaintenanceDetail", SqlDbType.NVarChar,200),
                    new SqlParameter("@MaintenanceDescription", SqlDbType.NVarChar,200),
                    new SqlParameter("@MaintenanceMethod", SqlDbType.NVarChar,200),
					new SqlParameter("@RepairSituation", SqlDbType.TinyInt,1),
					new SqlParameter("@TotalFee", SqlDbType.Decimal,9),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
                    new SqlParameter("@IsDelivered",SqlDbType.Bit,1),
                    new SqlParameter("@noequipment",SqlDbType.NVarChar,200)};
            parameters[0].Value = item.SheetID;
            parameters[1].Value = item.MaintenanceStaff;
            parameters[2].Value = item.MaintenanceDetail;
            parameters[3].Value = item.MaintenanceDescription;
            parameters[4].Value = item.MaintenanceMethod;
            parameters[5].Value = item.RepairSituation;
            parameters[6].Value = item.TotalFee;
            parameters[7].Value = item.UpdateTime;
            parameters[8].Value = item.IsDelivered;
            parameters[9].Value = item.NoEquipment;

            long maintainID = (long)SQLHelper.ExecuteScalar(trans, CommandType.Text, strSql.ToString(), parameters);

            return maintainID;
        }
        /// <summary>
        /// 更新已维修的设备列表
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="maintainID"></param>
        /// <param name="item"></param>
        private void UpdateMaintainedEquipments(SqlTransaction trans, long maintainID, IList equipments)
        {
            //先删除维修部分零件
            StringBuilder strPartSql = new StringBuilder();
            strPartSql.Append("delete from FM2E_MaintainedEquipmentPart ");
            strPartSql.Append(" where ID=@ID ");

            foreach (MaintainedEquipmentInfo item in equipments)
            {
                SqlParameter[] parametersPartDel = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
                parametersPartDel[0].Value = item.ID;
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strPartSql.ToString(), parametersPartDel);
            }


            //先删除
            StringBuilder strDel = new StringBuilder();
            strDel.Append("delete FM2E_MaintainedEquipment ");
            strDel.Append(" where MaintainID=@MaintainID ");
            SqlParameter[] paramDel = {
					new SqlParameter("@MaintainID", SqlDbType.BigInt)};
            paramDel[0].Value = maintainID;

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strDel.ToString(), paramDel);

            if (equipments == null || equipments.Count == 0)
                return;
            //后插入
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_MaintainedEquipment(");
            strSql.Append("MaintainID,SheetID,EquipmentNO,EquipmentName,Model,SerialNum,MaintainResult,MaintainFee,Remark,MaintainDate,LastAddress)");
            strSql.Append(" values (");
            strSql.Append("@MaintainID,@SheetID,@EquipmentNO,@EquipmentName,@Model,@SerialNum,@MaintainResult,@MaintainFee,@Remark,@MaintainDate,@LastAddress)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@MaintainID", SqlDbType.BigInt,8),
					new SqlParameter("@SheetID", SqlDbType.BigInt,8),
					new SqlParameter("@EquipmentNO", SqlDbType.VarChar,50),
					new SqlParameter("@EquipmentName", SqlDbType.NVarChar,20),
                    new SqlParameter("@Model",SqlDbType.VarChar,20),
                    new SqlParameter("@SerialNum",SqlDbType.VarChar,20),
					new SqlParameter("@MaintainResult", SqlDbType.TinyInt,1),
					new SqlParameter("@MaintainFee", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@MaintainDate", SqlDbType.DateTime),
                    new SqlParameter("@LastAddress",SqlDbType.NVarChar,50)
                                        
                                        };

            foreach (MaintainedEquipmentInfo item in equipments)
            {
                parameters[0].Value = maintainID;
                parameters[1].Value = item.SheetID;
                parameters[2].Value = item.EquipmentNO;
                parameters[3].Value = item.EquipmentName;
                parameters[4].Value = item.Model != null ? item.Model : SqlString.Null;
                parameters[5].Value = item.SerialNum != null ? item.SerialNum : SqlString.Null;
                parameters[6].Value = item.MaintainResult;
                parameters[7].Value = item.MaintainFee;
                parameters[8].Value = item.Remark;
                parameters[9].Value = item.MaintainDate;
                parameters[10].Value = item.LastAddress != null ? item.LastAddress : SqlString.Null;

                long equipmentid = (long)SQLHelper.ExecuteScalar(trans, CommandType.Text, strSql.ToString(), parameters);

                if (item.MaintainedEquipmentParts != null)
                {
                    //插入维修零件部分
                    foreach (MaintainedEquipmentPartInfo equipmentitem in item.MaintainedEquipmentParts)
                    {
                        StringBuilder strPartInsertSql = new StringBuilder();
                        strPartInsertSql.Append("insert into FM2E_MaintainedEquipmentPart(");
                        strPartInsertSql.Append("MaintainedEquipmentID,PartName,MaintainFee)");
                        strPartInsertSql.Append(" values (");
                        strPartInsertSql.Append("@MaintainedEquipmentID,@PartName,@MaintainFee)");
                        SqlParameter[] parametersPartInsert = {
					        new SqlParameter("@MaintainedEquipmentID", SqlDbType.BigInt,8),
					        new SqlParameter("@PartName", SqlDbType.VarChar,20),
					        new SqlParameter("@MaintainFee", SqlDbType.Decimal,9)};
                        parametersPartInsert[0].Value = equipmentid;  //新的维修设备ID
                        parametersPartInsert[1].Value = equipmentitem.PartName;
                        parametersPartInsert[2].Value = equipmentitem.MaintainFee;

                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strPartInsertSql.ToString(), parametersPartInsert);
                    }
                }
            }
        }
        /// <summary>
        /// 更新维修人员列表
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="maintainID"></param>
        /// <param name="item"></param>
        private void UpdateMaintainStaff(SqlTransaction trans, long maintainID, IList staff)
        {

            //先删除
            StringBuilder strDel = new StringBuilder();
            strDel.Append("delete from FM2E_MalfunctionMaintainStaff ");
            strDel.Append(" where MaintainID=@MaintainID ");
            SqlParameter[] paramDel = {
					new SqlParameter("@MaintainID", SqlDbType.BigInt)};
            paramDel[0].Value = maintainID;

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strDel.ToString(), paramDel);

            if (staff == null || staff.Count == 0)
                return;
            //后插入
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_MalfunctionMaintainStaff(");
            strSql.Append("MaintainID,MaintenanceStaff)");
            strSql.Append(" values (");
            strSql.Append("@MaintainID,@MaintenanceStaff)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@MaintainID", SqlDbType.BigInt,8),
					new SqlParameter("@MaintenanceStaff", SqlDbType.VarChar,20)};


            foreach (MalfunctionMaintainStaffInfo item in staff)
            {
                parameters[0].Value = maintainID;
                parameters[1].Value = item.MaintenanceStaff;

                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            }
        }
        /// <summary>
        /// 获取某张故障单的处理历史
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList IMalfunctionHandle.GetMaintainHistory(long id)
        {
            ArrayList maintainList = new ArrayList();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select a.*,b.DepartmentName as MaintenanceTeam,b.PersonName as MaintenanceStaffName ");
                strSql.Append(" FROM FM2E_MalfunctionMaintainDetail a ");
                strSql.Append(" left join FM2E_UserView b on a.MaintenanceStaff=b.UserName");
                strSql.Append(" where a.SheetID=@SheetID");
                strSql.Append(" order by a.UpdateTime desc");
                SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        MalfuncitonMaintainInfo maintainInfo = GetMaintainInfo(rd);
                        maintainInfo.MaintainedEquipments = GetMaintainedEquipmentList(maintainInfo.MaintainID);
                        maintainInfo.MaintainStaff = GetMalfunctionMaintainStaffList(maintainInfo.MaintainID);
                        maintainList.Add(maintainInfo);
                    }
                }

            }
            catch (Exception ex)
            {
                maintainList.Clear();
                throw new DALException("获取故障单处理历史失败", ex);
            }
            return maintainList;
        }
        /// <summary>
        /// 获取某张故障处理单的修改历史
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList IMalfunctionHandle.GetModifyHistory(long id)
        {
            ArrayList modifyList = new ArrayList();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select a.ID,a.SheetID,a.Modifier,b.PersonName as ModifierName,a.ModifyDescription,a.ModifyDate ");
                strSql.Append(" FROM FM2E_MalfunctionModifyRecord a left join FM2E_User b on a.Modifier=b.UserName");
                strSql.Append(" where a.SheetID=@SheetID");
                strSql.Append(" order by a.ModifyDate desc");
                SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.BigInt)};
                parameters[0].Value = id;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        modifyList.Add(GetModifyRecordData(rd));
                    }
                }
            }
            catch (Exception ex)
            {
                modifyList.Clear();
                throw new DALException("获取故障单修改历史失败", ex);
            }
            return modifyList;
        }
        /// <summary>
        /// 添加故障单修改记录
        /// </summary>
        /// <param name="model"></param>
        void IMalfunctionHandle.AddModifyRecord(MalfunctionModifyRecordInfo model, DbTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FM2E_MalfunctionModifyRecord(");
            strSql.Append("SheetID,Modifier,ModifyDescription,ModifyDate)");
            strSql.Append(" values (");
            strSql.Append("@SheetID,@Modifier,@ModifyDescription,@ModifyDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.BigInt,8),
					new SqlParameter("@Modifier", SqlDbType.VarChar,20),
					new SqlParameter("@ModifyDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime)};
            parameters[0].Value = model.SheetID;
            parameters[1].Value = model.Modifier;
            parameters[2].Value = model.ModifyDescription;
            parameters[3].Value = model.ModifyDate;

            SQLHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获取某次维修过程维修过的设备列表
        /// </summary>
        /// <param name="maintainID"></param>
        /// <returns></returns>
        private IList GetMaintainedEquipmentList(long maintainID)
        {
            ArrayList equipments = new ArrayList();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,MaintainDate,MaintainID,SheetID,EquipmentNO,EquipmentName,Model,SerialNum,LastAddress,MaintainResult,MaintainFee,Remark ");
            strSql.Append(" FROM FM2E_MaintainedEquipment ");
            strSql.Append(" where MaintainID=@MaintainID");

            SqlParameter[] parameters = {
					new SqlParameter("@MaintainID", SqlDbType.BigInt)};
            parameters[0].Value = maintainID;

            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    MaintainedEquipmentInfo item = GetMaintainedEquipmentData(rd);
                    item.MaintainedEquipmentParts = GetMaintainedEquipmentPartList(item.ID);
                    equipments.Add(item);
                }
            }
            return equipments;
        }


        /// <summary>
        /// 根据ID删除故障登记纪录
        /// </summary>
        /// <param name="maintainID"></param>
        /// <param name="trans"></param>
        public void DelMaintainedEquipmentByMaintainID(long maintainID, DbTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ");
            strSql.Append(" FROM FM2E_MalfunctionMaintainDetail ");
            strSql.Append(" where MaintainID=@MaintainID");

            SqlParameter[] parameters = {
					new SqlParameter("@MaintainID", SqlDbType.BigInt)};
            parameters[0].Value = maintainID;

            SQLHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters);
        }



        /// <summary>
        /// 获取某个维修设备的零件列表
        /// </summary>
        /// <param name="MaintainedEquipmentID"></param>
        /// <returns></returns>
        private IList GetMaintainedEquipmentPartList(long MaintainedEquipmentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,MaintainedEquipmentID,PartName,MaintainFee ");
            strSql.Append(" FROM FM2E_MaintainedEquipmentPart ");
            strSql.Append(" where MaintainedEquipmentID=@MaintainedEquipmentID");

            SqlParameter[] parameters = {
					new SqlParameter("@MaintainedEquipmentID", SqlDbType.BigInt)};
            parameters[0].Value = MaintainedEquipmentID;
            ArrayList list = new ArrayList();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        list.Add(GetMaintainedEquipmentPartData(rd));
                    }
                }
            }
            catch (Exception e)
            {
                list.Clear();
                throw new DALException("获取MalfunctionMaintainStaffInfo列表信息失败", e);
            }
            return list;
        }
        /// <summary>
        /// 获取某次维修过程维修人员列表
        /// </summary>
        /// <param name="maintainID"></param>
        /// <returns></returns>
        private IList GetMalfunctionMaintainStaffList(long maintainID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,MaintainID,MaintenanceStaff ");
            strSql.Append(" FROM FM2E_MalfunctionMaintainStaff ");
            strSql.Append(" where MaintainID=@MaintainID");

            SqlParameter[] parameters = {
					new SqlParameter("@MaintainID", SqlDbType.BigInt)};
            parameters[0].Value = maintainID;
            ArrayList list = new ArrayList();
            try
            {
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        list.Add(GetMalfunctionMaintainStaffData(rd));
                    }
                }
            }
            catch (Exception e)
            {
                list.Clear();
                throw new DALException("获取MalfunctionMaintainStaffInfo列表信息失败", e);
            }
            return list;
        }
        /// <summary>
        /// 生成故障处理查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam IMalfunctionHandle.GenerateSearchTerm(MalfunctionSearchInfo term)
        {
            QueryParam qp = new QueryParam();

            string strJoinTable = "";
            string strSqlWhere = CreateSqlTerm(term, out strJoinTable);

            qp.TableName = "FM2E_MalfunctionSheetView a" + strJoinTable;
            qp.ReturnFields = "a.*,a.UpdateTime as SortedUpdateTime";
            qp.OrderBy = "order by SortedUpdateTime desc";
            qp.Where = strSqlWhere;

            return qp;

        }
        /// <summary>
        /// 生成故障处理查询条件按上报时间排序
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam IMalfunctionHandle.GenerateSearchTermByReportTime(MalfunctionSearchInfo term)
        {
            QueryParam qp = new QueryParam();

            string strJoinTable = "";
            string strSqlWhere = CreateSqlTerm(term, out strJoinTable);

            qp.TableName = "FM2E_MalfunctionSheetView a" + strJoinTable;
            qp.ReturnFields = "a.*,a.UpdateTime as SortedUpdateTime";
            qp.OrderBy = "order by ReportDate desc";
            qp.Where = strSqlWhere;

            return qp;

        }
        //  [5/21/2013 Tvk]
        /// <summary>
        /// 生成延迟审批故障处理查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam IMalfunctionHandle.GenerateDelayApproveSearchTerm(MalfunctionSearchInfo term)
        {
            QueryParam qp = new QueryParam();

            string strJoinTable = "";
            string strSqlWhere = CreateDelayApproveSqlTerm(term, out strJoinTable);

            qp.TableName = "FM2E_MalfunctionSheetView a" + strJoinTable;
            qp.ReturnFields = "a.*,a.UpdateTime as SortedUpdateTime";
            qp.OrderBy = "order by SortedUpdateTime desc";
            qp.Where = strSqlWhere;

            return qp;

        }
        //  [5/21/2013 Tvk]
        //********** Modified by Xue    For ShenGaoSu    2011-11-28 ********************************************************************************
        /// <summary>
        /// 生成故障处理查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam IMalfunctionHandle.GenerateApprovalSearchTerm(MalfunctionSearchInfo term, string userName)
        {
            QueryParam qp = new QueryParam();

            string strJoinTable = "";
            string strSqlWhere = CreateSqlTerm(term, out strJoinTable);


            strSqlWhere += " and a.SheetID in (select DataID from FM2E_WorkflowInstance where FM2E_WorkflowInstance.WorkflowName='SGS_MalFunctionWorkflow' and FM2E_WorkflowInstance.NextUserName='" + userName + "' ) ";
            qp.TableName = "FM2E_MalfunctionSheetView a" + strJoinTable;
            qp.ReturnFields = "a.*,a.UpdateTime as SortedUpdateTime";
            qp.OrderBy = "order by SortedUpdateTime desc";
            qp.Where = strSqlWhere;

            return qp;

        }
        //********** Modification Finished 2011-11-28 **********************************************************************************************

        QueryParam IMalfunctionHandle.GenerateSearchTerm2(MalfunctionSearchInfo term)
        {
            QueryParam qp = new QueryParam();

            string strJoinTable = "";
            string strSqlWhere = CreateSqlTerm2(term, out strJoinTable);

            qp.TableName = "FM2E_MalfunctionSheetView a" + strJoinTable;
            qp.ReturnFields = "a.*,a.UpdateTime as SortedUpdateTime";
            qp.OrderBy = "order by SortedUpdateTime desc";
            qp.Where = strSqlWhere;

            return qp;

        }
        /// <summary>
        /// 根据查询参数，生成sql查询语句
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        private string CreateSqlTerm2(MalfunctionSearchInfo term, out string strJoinTable)
        {
            strJoinTable = "";

            string sqlSearch = "where 1=1 ";

            sqlSearch += string.Format(" and a.Status<>{0} ", (int)MalfunctionHandleStatus.Waiting4Accept);

            if (term.MaintainDept != 0)
                sqlSearch += string.Format(" and a.MaintainDept ={0} ", term.MaintainDept);

            if (term.EStatus == 1)
                sqlSearch += string.Format(" and a.Status ={0} ", (int)MalfunctionHandleStatus.FunctionalityRestore);

            if (term.EStatus == 2)
                sqlSearch += string.Format(" and (a.Status ={0} or a.Status ={1}) ", (int)MalfunctionHandleStatus.Fixed, (int)MalfunctionHandleStatus.Finished);

            if (term.EStatus == 3)
                sqlSearch += string.Format(" and a.Status <>{0} and a.Status <>{1} and a.Status <>{2} ", (int)MalfunctionHandleStatus.FunctionalityRestore, (int)MalfunctionHandleStatus.Fixed, (int)MalfunctionHandleStatus.Finished);

            if (!string.IsNullOrEmpty(term.MaintainSataff))
                sqlSearch += string.Format(" and a.SheetID IN (SELECT SheetID FROM FM2E_MalfunctionMaintainDetail, FM2E_MalfunctionMaintainStaff  WHERE FM2E_MalfunctionMaintainDetail.MaintainID = FM2E_MalfunctionMaintainStaff.MaintainID AND FM2E_MalfunctionMaintainStaff.MaintenanceStaff = '{0}')", term.MaintainSataff);

            if (!string.IsNullOrEmpty(term.ECompanyID))
                sqlSearch += string.Format(" and a.SheetID IN (SELECT FM2E_MaintainedEquipment.SheetID FROM dbo.FM2E_MaintainedEquipment, FM2E_Equipment WHERE FM2E_MaintainedEquipment.EquipmentNO = FM2E_Equipment.EquipmentNO AND FM2E_Equipment.CompanyID = '{0}')", term.ECompanyID);


            if (!string.IsNullOrEmpty(term.SheetNO))
                sqlSearch += " and a.SheetNO like '%" + term.SheetNO + "%'";

            if (!string.IsNullOrEmpty(term.CompanyID))
                sqlSearch += " and a.CompanyID = '" + term.CompanyID + "'";

            if (term.DepartmentID != 0)
                sqlSearch += " and a.DepartmentID=" + term.DepartmentID;

            if (!string.IsNullOrEmpty(term.Reporter))
                sqlSearch += " and a.Reporter like '%" + term.Reporter + "%'";

            if (DateTime.Compare(term.ReportDateFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(term.ReportDateFrom, sqlMinDate) < 0)
                    term.ReportDateFrom = sqlMinDate;

                sqlSearch += " and a.ReportDate>='" + term.ReportDateFrom.ToString("yyyy-MM-dd") + " 00:00:00'";
            }

            if (DateTime.Compare(term.ReportDateTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(term.ReportDateTo, sqlMaxDate) > 0)
                    term.ReportDateTo = sqlMaxDate;

                sqlSearch += " and a.ReportDate<='" + term.ReportDateTo.ToString("yyyy-MM-dd") + " 23:59:59'";
            }

            if (!string.IsNullOrEmpty(term.Recorder))
                sqlSearch += " and a.Recorder ='" + term.Recorder + "'";

            if (!string.IsNullOrEmpty(term.RecorderName))
                sqlSearch += " and a.RecorderName like '%" + term.RecorderName + "%'";

            if (term.RecordDept != 0)  //连子部门的也要查询出来
                sqlSearch += string.Format(" and (a.RecordDept in ( select DepartmentID from FM2E_GetSubDepartments({0})))", term.RecordDept);

            if (term.Status != (int)MalfunctionHandleStatus.Unknown)
            {
                //sqlSearch += " and Status=" + term.Status;
                sqlSearch += " and (1<0 ";
                int status = 1;
                int statusValue = term.Status;
                while (statusValue != 0)
                {
                    if ((statusValue & 1) != 0)
                        sqlSearch += string.Format(" or a.Status={0}", status);
                    status <<= 1;
                    statusValue >>= 1;
                }
                sqlSearch += ")";
            }

            if (term.MaintainDept != 0)
                //sqlSearch += string.Format(" and (a.MaintainDept in (select DepartmentID from FM2E_GetSubDepartments({0})))" , term.MaintainDept);  //查询该部门及下子部门
                sqlSearch += "and a.MaintainDept=" + term.MaintainDept;

            if (term.MalfunctionRank != 0)
                sqlSearch += " and a.MalfunctionRank=" + term.MalfunctionRank;

            if (!string.IsNullOrEmpty(term.Receiver))
                sqlSearch += " and a.Receiver='" + term.Receiver + "'";

            if (!string.IsNullOrEmpty(term.SystemID))
                sqlSearch += " and a.SystemID='" + term.SystemID + "'";

            if (!string.IsNullOrEmpty(term.EquipmentNO))
            {
                sqlSearch += " and b.EquipmentNO='" + term.EquipmentNO + "'";
                strJoinTable += " inner join FM2E_FaultyEquipments b on a.SheetID=b.SheetID";
            }

            if (!string.IsNullOrEmpty(term.EquipmentName))
            {
                sqlSearch += " and b.EquipmentName like '%" + term.EquipmentName + "%'";
                strJoinTable += " inner join FM2E_FaultyEquipments b on a.SheetID=b.SheetID";
            }

            if (!string.IsNullOrEmpty(term.AddressCode))
            {
                sqlSearch += " and c.AddressCode like '" + term.AddressCode + "%' ";
                strJoinTable += " left join FM2E_Address c on a.AddressID=c.ID ";
            }

            return sqlSearch;
        }

        private string CreateSqlTerm(MalfunctionSearchInfo term, out string strJoinTable)
        {
            strJoinTable = "";

            string sqlSearch = "where 1=1 ";


            if (!string.IsNullOrEmpty(term.SheetNO))
                sqlSearch += " and a.SheetNO like '%" + term.SheetNO + "%'";

            if (!string.IsNullOrEmpty(term.CompanyID))
                sqlSearch += " and a.CompanyID = '" + term.CompanyID + "'";

            if (term.DepartmentID != 0)
                sqlSearch += " and a.DepartmentID=" + term.DepartmentID;

            if (!string.IsNullOrEmpty(term.Reporter))
                sqlSearch += " and a.Reporter like '%" + term.Reporter + "%'";

            if (DateTime.Compare(term.ReportDateFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(term.ReportDateFrom, sqlMinDate) < 0)
                    term.ReportDateFrom = sqlMinDate;

                sqlSearch += " and a.ReportDate>='" + term.ReportDateFrom.ToString("yyyy-MM-dd") + " 00:00:00'";
            }

            if (DateTime.Compare(term.ReportDateTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(term.ReportDateTo, sqlMaxDate) > 0)
                    term.ReportDateTo = sqlMaxDate;

                sqlSearch += " and a.ReportDate<='" + term.ReportDateTo.ToString("yyyy-MM-dd") + " 23:59:59'";
            }

            //if (DateTime.Compare(term.ReportDateFrom2, DateTime.MinValue) != 0)
            //{
            //    DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
            //    if (DateTime.Compare(term.ReportDateFrom2, sqlMinDate) < 0)
            //        term.ReportDateFrom2 = sqlMinDate;

            //    sqlSearch += " and a.ReportDate2>='" + term.ReportDateFrom2.ToString("yyyy-MM-dd") + " 00:00:00'";
            //}

            //if (DateTime.Compare(term.ReportDateTo2, DateTime.MinValue) != 0)
            //{
            //    DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
            //    if (DateTime.Compare(term.ReportDateTo2, sqlMaxDate) > 0)
            //        term.ReportDateTo2 = sqlMaxDate;

            //    sqlSearch += " and a.ReportDate2<='" + term.ReportDateTo2.ToString("yyyy-MM-dd") + " 23:59:59'";
            //}

            if (!string.IsNullOrEmpty(term.Recorder))
                sqlSearch += " and a.Recorder ='" + term.Recorder + "'";

            if (!string.IsNullOrEmpty(term.RecorderName))
                sqlSearch += " and a.RecorderName like '%" + term.RecorderName + "%'";

            if (term.RecordDept != 0)  //连子部门的也要查询出来
                sqlSearch += string.Format(" and (a.RecordDept in ( select DepartmentID from FM2E_GetSubDepartments({0})))", term.RecordDept);

            if (term.Status != (int)MalfunctionHandleStatus.Unknown)
            {
                //sqlSearch += " and Status=" + term.Status;
                //sqlSearch += string.Format(" and (1<0 or a.Status={0} ", term.Status);
                sqlSearch += " and (1<0 ";
                //***
                int status = 1;
                int statusValue = term.Status;
                if (term.Status == 6)
                {
                    sqlSearch += " or a.Status=6 ";
                }
                else if (term.Status == 10)
                {
                    sqlSearch += " or a.Status=10 ";
                }
                else
                {
                    while (statusValue != 0)
                    {
                        if ((statusValue & 1) != 0)
                            sqlSearch += string.Format(" or a.Status={0}", status);
                        status <<= 1;
                        statusValue >>= 1;
                    }
                }
                //***
                sqlSearch += ")";
            }

            if (term.MaintainDept != 0)
                //sqlSearch += string.Format(" and (a.MaintainDept in (select DepartmentID from FM2E_GetSubDepartments({0})))" , term.MaintainDept);  //查询该部门及下子部门
                sqlSearch += "and a.MaintainDept=" + term.MaintainDept;
            //By L 5-3 根据故障原因查询分类****************************************************************************************************************
            if (term.MalReason != 0)

                sqlSearch += " and a.SystemID='" + term.MalReason + "'";
            if (term.MalMeasure == 1 || term.MalMeasure == 2)
            {
                string var = "";
                if (term.MalMeasure == 1)
                    var = "需要计量";
                if (term.MalMeasure == 2)
                    var = "不需要计量";

                sqlSearch += " and a.IsMeasure='" + var + "'";
            }

            if (!string.IsNullOrEmpty(term.JiliangOrNot))
            {
                sqlSearch += " and IsApplyMeasure = '" + term.JiliangOrNot + "'";
            }

            if (!string.IsNullOrEmpty(term.JiagongOrNot))
            {
                sqlSearch += " and IsProvider ='" + term.JiagongOrNot + "'";
            }

            if (term.Stationcheck == 1)
            {
                sqlSearch += " and a.Stationcheck = 1 ";
            }

            //************************************************************************************************************************************************
            if (term.MalfunctionRank != 0)
                sqlSearch += " and a.MalfunctionRank=" + term.MalfunctionRank;

            if (!string.IsNullOrEmpty(term.Receiver))
                sqlSearch += " and a.Receiver='" + term.Receiver + "'";


            //By L 5-2 修正查询系统项，根据表中addressdetail的记录进行查询不同系统的值******************************************************************************

            //if (!string.IsNullOrEmpty(term.SystemID))
            //    sqlSearch += " and a.SystemID='" + term.SystemID + "'";

            if (!string.IsNullOrEmpty(term.SystemID))
                sqlSearch += " and a.AddressDetail LIKE '%" + term.SystemID + "'";

            //By L 5-2 ******************************************************************************************************************************************

            if (!string.IsNullOrEmpty(term.EquipmentNO))
            {
                sqlSearch += " and b.EquipmentNO='" + term.EquipmentNO + "'";
                strJoinTable += " inner join FM2E_FaultyEquipments b on a.SheetID=b.SheetID";
            }

            //By L 5-2 修正查询部分,设备名称查询***************************************************************************************************************************

            //if (!string.IsNullOrEmpty(term.EquipmentName))
            //{
            //    sqlSearch += " and b.EquipmentName like '%" + term.EquipmentName + "%'";
            //    strJoinTable += " inner join FM2E_FaultyEquipments b on a.SheetID=b.SheetID";
            //}

            if (!string.IsNullOrEmpty(term.EquipmentName))
            {
                sqlSearch += " and a.Name LIKE'%" + term.EquipmentName + "%'";

                //strJoinTable += " inner join FM2E_FaultyEquipments b on a.SheetID=b.SheetID";
            }

            if (!string.IsNullOrEmpty(term.EquipmentSpecification))
            {
                sqlSearch += " and a.Specification LIKE'%" + term.EquipmentSpecification + "%'";

                //strJoinTable += " inner join FM2E_FaultyEquipments b on a.SheetID=b.SheetID";
            } if (!string.IsNullOrEmpty(term.EquipmentModel))
            {
                sqlSearch += " and a.Model LIKE'%" + term.EquipmentModel + "%'";

                //strJoinTable += " inner join FM2E_FaultyEquipments b on a.SheetID=b.SheetID";
            }

            //By L 5-2 修正查询部分,设备名称查询***************************************************************


            if (!string.IsNullOrEmpty(term.AddressCode))
            {
                sqlSearch += " and c.AddressCode like '" + term.AddressCode + "%' ";
                strJoinTable += " left join FM2E_Address c on a.AddressID=c.ID ";
            }
            //2012-8-13*****************************************************************************************************

            //if (term.IsDelayApply == 1)
            // {
            //     sqlSearch += " or ( a.IsDelayApply = 1 and a.IsDelayCheck1 >= 1 and a.IsDelayCheck2 < 1 ) ";
            //  }

            //2012-8-13修改部分实现审批传递*****************************************************************************************************
            return sqlSearch;
        }


        //  [5/21/2013 Tvk]

        private string CreateDelayApproveSqlTerm(MalfunctionSearchInfo term, out string strJoinTable)
        {
            strJoinTable = "";

            string sqlSearch = "where 1=1 ";


            if (!string.IsNullOrEmpty(term.SheetNO))
                sqlSearch += " and a.SheetNO like '%" + term.SheetNO + "%'";

            if (!string.IsNullOrEmpty(term.CompanyID))
                sqlSearch += " and a.CompanyID = '" + term.CompanyID + "'";

            if (term.DepartmentID != 0)
                sqlSearch += " and a.DepartmentID=" + term.DepartmentID;

            if (!string.IsNullOrEmpty(term.Reporter))
                sqlSearch += " and a.Reporter like '%" + term.Reporter + "%'";

            if (DateTime.Compare(term.ReportDateFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(term.ReportDateFrom, sqlMinDate) < 0)
                    term.ReportDateFrom = sqlMinDate;

                sqlSearch += " and a.ReportDate>='" + term.ReportDateFrom.ToString("yyyy-MM-dd") + " 00:00:00'";
            }

            if (DateTime.Compare(term.ReportDateTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(term.ReportDateTo, sqlMaxDate) > 0)
                    term.ReportDateTo = sqlMaxDate;

                sqlSearch += " and a.ReportDate<='" + term.ReportDateTo.ToString("yyyy-MM-dd") + " 23:59:59'";
            }

            if (!string.IsNullOrEmpty(term.Recorder))
                sqlSearch += " and a.Recorder ='" + term.Recorder + "'";

            if (!string.IsNullOrEmpty(term.RecorderName))
                sqlSearch += " and a.RecorderName like '%" + term.RecorderName + "%'";

            if (term.RecordDept != 0)  //连子部门的也要查询出来
                sqlSearch += string.Format(" and (a.RecordDept in ( select DepartmentID from FM2E_GetSubDepartments({0})))", term.RecordDept);

            if (term.Status != (int)MalfunctionHandleStatus.Unknown)
            {
                sqlSearch += " and Status=" + term.Status;
                //sqlSearch += " and (1<0 ";
                //int status = 1;
                //int statusValue = term.Status;
                //while (statusValue != 0)
                //{
                //    if ((statusValue & 1) != 0)
                //        sqlSearch += string.Format(" or a.Status={0}", status);
                //    status <<= 1;
                //    statusValue >>= 1;
                //}
                //sqlSearch += ")";
            }

            if (term.MaintainDept != 0)
                //sqlSearch += string.Format(" and (a.MaintainDept in (select DepartmentID from FM2E_GetSubDepartments({0})))" , term.MaintainDept);  //查询该部门及下子部门
                sqlSearch += "and a.MaintainDept=" + term.MaintainDept;
            //By L 5-3 根据故障原因查询分类****************************************************************************************************************
            if (term.MalReason != 0)

                sqlSearch += " and a.SystemID='" + term.MalReason + "'";
            if (term.MalMeasure == 1 || term.MalMeasure == 2)
            {
                string var = "";
                if (term.MalMeasure == 1)
                    var = "需要计量";
                if (term.MalMeasure == 2)
                    var = "不需要计量";

                sqlSearch += " and a.IsMeasure='" + var + "'";
            }
            if (term.Stationcheck == 1)
            {
                sqlSearch += " and a.Stationcheck = 1 ";
            }

            //************************************************************************************************************************************************
            if (term.MalfunctionRank != 0)
                sqlSearch += " and a.MalfunctionRank=" + term.MalfunctionRank;

            if (!string.IsNullOrEmpty(term.Receiver))
                sqlSearch += " and a.Receiver='" + term.Receiver + "'";


            //By L 5-2 修正查询系统项，根据表中addressdetail的记录进行查询不同系统的值******************************************************************************

            //if (!string.IsNullOrEmpty(term.SystemID))
            //    sqlSearch += " and a.SystemID='" + term.SystemID + "'";

            if (!string.IsNullOrEmpty(term.SystemID))
                sqlSearch += " and a.AddressDetail LIKE '%" + term.SystemID + "'";

            //By L 5-2 ******************************************************************************************************************************************

            if (!string.IsNullOrEmpty(term.EquipmentNO))
            {
                sqlSearch += " and b.EquipmentNO='" + term.EquipmentNO + "'";
                strJoinTable += " inner join FM2E_FaultyEquipments b on a.SheetID=b.SheetID";
            }

            //By L 5-2 修正查询部分,设备名称查询***************************************************************************************************************************

            //if (!string.IsNullOrEmpty(term.EquipmentName))
            //{
            //    sqlSearch += " and b.EquipmentName like '%" + term.EquipmentName + "%'";
            //    strJoinTable += " inner join FM2E_FaultyEquipments b on a.SheetID=b.SheetID";
            //}

            if (!string.IsNullOrEmpty(term.EquipmentName))
            {
                sqlSearch += " and a.Name LIKE'%" + term.EquipmentName + "%'";

                //strJoinTable += " inner join FM2E_FaultyEquipments b on a.SheetID=b.SheetID";
            }

            if (!string.IsNullOrEmpty(term.EquipmentSpecification))
            {
                sqlSearch += " and a.Specification LIKE'%" + term.EquipmentSpecification + "%'";

                //strJoinTable += " inner join FM2E_FaultyEquipments b on a.SheetID=b.SheetID";
            } if (!string.IsNullOrEmpty(term.EquipmentModel))
            {
                sqlSearch += " and a.Model LIKE'%" + term.EquipmentModel + "%'";

                //strJoinTable += " inner join FM2E_FaultyEquipments b on a.SheetID=b.SheetID";
            }

            //By L 5-2 修正查询部分,设备名称查询***************************************************************


            if (!string.IsNullOrEmpty(term.AddressCode))
            {
                sqlSearch += " and c.AddressCode like '" + term.AddressCode + "%' ";
                strJoinTable += " left join FM2E_Address c on a.AddressID=c.ID ";
            }


            if (term.IsDelayApply == 1)
            {
                if (term.FinalDelayApprove == 0)
                {
                    sqlSearch += " and a.IsDelayApply = 1 and ( a.FirstDelayApprove = " + term.FirstDelayApprove + " or a.FirstDelayApprove is null ) ";
                }
                else
                {
                    sqlSearch += " and a.IsDelayApply = 1 and a.FirstDelayApprove = " + term.FirstDelayApprove;
                }
            }
            if (term.FirstDelayApprove == 4)
            {
                sqlSearch += " and ( a.FinalDelayApprove = 0 or a.FinalDelayApprove is null ) ";
            }
            return sqlSearch;
        }

        //  [5/21/2013 Tvk]
        /// <summary>
        /// 生成设备维修记录查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam IMalfunctionHandle.GenerateSearchTerm(EquipmentMaintainRecordSearchInfo term)
        {
            QueryParam qp = new QueryParam();

            string sqlSearch = "where 1=1 ";
            if (!string.IsNullOrEmpty(term.SheetNO))
                sqlSearch += " and SheetNO like '%" + term.SheetNO + "%'";

            if (term.DepartmentID != 0)
                sqlSearch += " and DepartmentID=" + term.DepartmentID;

            if (!string.IsNullOrEmpty(term.EquipmentName))
                sqlSearch += " and EquipmentName like '%" + term.EquipmentName + "%'";

            if (!string.IsNullOrEmpty(term.EquipmentNO))
                sqlSearch += " and EquipmentNO ='" + term.EquipmentNO + "'";

            if (!string.IsNullOrEmpty(term.SystemID))
                sqlSearch += " and SystemID='" + term.SystemID + "'";

            if (term.MalfunctionRank != (int)MalfunctionRank.Unknown)
                sqlSearch += " and MalfunctionRank=" + term.MalfunctionRank;

            if (term.MaintainTeam != 0)
                sqlSearch += string.Format(" and (MaintainDept in (select DepartmentID from FM2E_GetSubDepartments({0})))", term.MaintainTeam);

            if (DateTime.Compare(term.MaintainDateFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(term.MaintainDateFrom, sqlMinDate) < 0)
                    term.MaintainDateFrom = sqlMinDate;

                sqlSearch += " and MaintainDate>='" + term.MaintainDateFrom.ToString("yyyy-MM-dd") + " 00:00:00'";
            }

            if (DateTime.Compare(term.MaintainDateTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(term.MaintainDateTo, sqlMaxDate) > 0)
                    term.MaintainDateTo = sqlMaxDate;

                sqlSearch += " and MaintainDate<='" + term.MaintainDateTo.ToString("yyyy-MM-dd") + " 23:59:59'";
            }

            qp.Where = sqlSearch;
            qp.TableName = "FM2E_EquipmentMaintainRecordView";
            qp.ReturnFields = "*";
            qp.OrderBy = "order by ID desc";
            return qp;
        }

        QueryParam IMalfunctionHandle.GenerateSearchTerm1(EquipmentMaintainRecordSearchInfo term)
        {
            QueryParam qp = new QueryParam();

            string sqlSearch = "where 1=1 ";            

            if (!string.IsNullOrEmpty(term.EquipmentNO))
                sqlSearch += " and EquipmentNO ='" + term.EquipmentNO + "'";
            
            qp.Where = sqlSearch;
            qp.TableName = "FM2E_TransferRecordView";
            qp.ReturnFields = "*";
            qp.OrderBy = "order by ReportDate";
            return qp;
        }

        //********** Modified by Xue    For ShenGaoSu    2011-11-28 ********************************************************************************
        /// <summary>
        /// 生成待审批的设备维修记录查询条件
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        QueryParam IMalfunctionHandle.GenerateApprovalSearchTerm(EquipmentMaintainRecordSearchInfo term, string userName)
        {
            QueryParam qp = new QueryParam();

            string sqlSearch = "where 1=1 ";
            if (!string.IsNullOrEmpty(term.SheetNO))
                sqlSearch += " and SheetNO like '%" + term.SheetNO + "%'";

            if (term.DepartmentID != 0)
                sqlSearch += " and DepartmentID=" + term.DepartmentID;

            if (!string.IsNullOrEmpty(term.EquipmentName))
                sqlSearch += " and EquipmentName like '%" + term.EquipmentName + "%'";

            if (!string.IsNullOrEmpty(term.EquipmentNO))
                sqlSearch += " and EquipmentNO ='" + term.EquipmentNO + "'";

            if (!string.IsNullOrEmpty(term.SystemID))
                sqlSearch += " and SystemID='" + term.SystemID + "'";

            if (term.MalfunctionRank != (int)MalfunctionRank.Unknown)
                sqlSearch += " and MalfunctionRank=" + term.MalfunctionRank;

            if (term.MaintainTeam != 0)
                sqlSearch += string.Format(" and (MaintainDept in (select DepartmentID from FM2E_GetSubDepartments({0})))", term.MaintainTeam);

            if (DateTime.Compare(term.MaintainDateFrom, DateTime.MinValue) != 0)
            {
                DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                if (DateTime.Compare(term.MaintainDateFrom, sqlMinDate) < 0)
                    term.MaintainDateFrom = sqlMinDate;

                sqlSearch += " and MaintainDate>='" + term.MaintainDateFrom.ToString("yyyy-MM-dd") + " 00:00:00'";
            }

            if (DateTime.Compare(term.MaintainDateTo, DateTime.MinValue) != 0)
            {
                DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                if (DateTime.Compare(term.MaintainDateTo, sqlMaxDate) > 0)
                    term.MaintainDateTo = sqlMaxDate;

                sqlSearch += " and MaintainDate<='" + term.MaintainDateTo.ToString("yyyy-MM-dd") + " 23:59:59'";
            }


            sqlSearch += " and FM2E_EquipmentMaintainRecordView.SheetID in (select DataID from FM2E_WorkflowInstance where FM2E_WorkflowInstance.WorkflowName='SGS_MalFunctionWorkflow' and FM2E_WorkflowInstance.NextUserName='" + userName + "' )";
            qp.Where = sqlSearch;
            qp.TableName = "FM2E_EquipmentMaintainRecordView,FM2E_WorkflowInstance";
            qp.ReturnFields = "*";
            qp.OrderBy = "order by ID desc";
            return qp;
        }

        //********** Modification Finished 2011-11-28 **********************************************************************************************

        public IList GetMalfunctionStatisticData2(MalfunctionStatisticTerm term)
        {
            ArrayList list = new ArrayList();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT a.MalfunctionRank, a.Status, a.MaintainDept,a.ReportDate,a.RepairTime,a.RepairUnit,ActualRepairTime");
                strSql.Append(" FROM FM2E_MalfunctionSheetView AS a ");
                //strSql.Append(" INNER JOIN FM2E_FaultyEquipments AS b ON a.SheetID = b.SheetID ");
                if (!string.IsNullOrEmpty(term.AddressCode) && term.AddressCode != "00")
                {
                    strSql.Append(" left join FM2E_Address c on a.AddressID=c.ID ");
                }

                strSql.Append("where 1=1 ");

                strSql.Append(string.Format(" and a.Status<>{0} ", (int)MalfunctionHandleStatus.Waiting4Accept));

                if (term.MaintainDept != 0)
                    strSql.Append(string.Format(" and a.MaintainDept ={0} ", term.MaintainDept));

                if (term.Status == 1)
                    strSql.Append(string.Format(" and a.Status ={0} ", (int)MalfunctionHandleStatus.FunctionalityRestore));

                if (term.Status == 2)
                    strSql.Append(string.Format(" and (a.Status ={0} or a.Status ={1}) ", (int)MalfunctionHandleStatus.Fixed, (int)MalfunctionHandleStatus.Finished));

                if (term.Status == 3)
                    strSql.Append(string.Format(" and a.Status <>{0} and a.Status <>{1} and a.Status <>{2} ", (int)MalfunctionHandleStatus.FunctionalityRestore, (int)MalfunctionHandleStatus.Fixed, (int)MalfunctionHandleStatus.Finished));

                if (!string.IsNullOrEmpty(term.MaintainSataff))
                    strSql.Append(string.Format(" and a.SheetID IN (SELECT SheetID FROM FM2E_MalfunctionMaintainDetail, FM2E_MalfunctionMaintainStaff  WHERE FM2E_MalfunctionMaintainDetail.MaintainID = FM2E_MalfunctionMaintainStaff.MaintainID AND FM2E_MalfunctionMaintainStaff.MaintenanceStaff = '{0}')", term.MaintainSataff));

                if (!string.IsNullOrEmpty(term.ECompanyID))
                    strSql.Append(string.Format(" and a.SheetID IN (SELECT FM2E_MaintainedEquipment.SheetID FROM dbo.FM2E_MaintainedEquipment, FM2E_Equipment WHERE FM2E_MaintainedEquipment.EquipmentNO = FM2E_Equipment.EquipmentNO AND FM2E_Equipment.CompanyID = '{0}')", term.ECompanyID));

                if (!string.IsNullOrEmpty(term.SystemID))
                    strSql.AppendFormat(" and a.SystemID='{0}'", term.SystemID);

                if (term.DepartmentID != 0)
                    strSql.AppendFormat(" and a.DepartmentID={0}", term.DepartmentID);

                if (!string.IsNullOrEmpty(term.AddressCode) && term.AddressCode != "00")
                {
                    strSql.AppendFormat(" and c.AddressCode like '{0}%'", term.AddressCode);
                }

                if (DateTime.Compare(term.ReportDateFrom, DateTime.MinValue) != 0)
                {
                    DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                    if (DateTime.Compare(term.ReportDateFrom, sqlMinDate) < 0)
                        term.ReportDateFrom = sqlMinDate;

                    strSql.Append(" and a.ReportDate>='" + term.ReportDateFrom.ToString("yyyy-MM-dd") + " 00:00:00'");
                }

                if (DateTime.Compare(term.ReportDateTo, DateTime.MinValue) != 0)
                {
                    DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                    if (DateTime.Compare(term.ReportDateTo, sqlMaxDate) > 0)
                        term.ReportDateTo = sqlMaxDate;

                    strSql.Append(" and a.ReportDate<='" + term.ReportDateTo.ToString("yyyy-MM-dd") + " 23:59:59'");
                }

                if (term.RecordDepartment != 0)
                    strSql.Append(string.Format(" and (a.RecordDept in (select DepartmentID from FM2E_GetSubDepartments({0})))", term.RecordDepartment));

                strSql.Append(" and a.Status<>" + (int)MalfunctionHandleStatus.Canceled);  //需要排除已撤消的故障单
                //strSql.Append(" GROUP BY a.MalfunctionRank, a.Status, a.MaintainDept");

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        list.Add(GetMalfunctionStatisticInfo2(rd));
                    }
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("统计时发生错误", ex);
            }
            return list;
        }
        /// <summary>
        /// 根据统计条件进行统计
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList IMalfunctionHandle.GetMalfunctionStatisticData(MalfunctionStatisticTerm term)
        {
            ArrayList list = new ArrayList();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT a.MalfunctionRank, a.Status, a.MaintainDept, COUNT(*) AS count ");
                strSql.Append(" FROM FM2E_MalfunctionSheetView AS a ");
                //strSql.Append(" INNER JOIN FM2E_FaultyEquipments AS b ON a.SheetID = b.SheetID ");
                if (!string.IsNullOrEmpty(term.AddressCode) && term.AddressCode != "00")
                {
                    strSql.Append(" left join FM2E_Address c on a.AddressID=c.ID ");
                }

                strSql.Append("where 1=1 ");

                strSql.Append(string.Format(" and a.Status<>{0} ", (int)MalfunctionHandleStatus.Waiting4Accept));

                if (term.MaintainDept != 0)
                    strSql.Append(string.Format(" and a.MaintainDept ={0} ", term.MaintainDept));

                if (term.Status == 1)
                    strSql.Append(string.Format(" and a.Status ={0} ", (int)MalfunctionHandleStatus.FunctionalityRestore));

                if (term.Status == 2)
                    strSql.Append(string.Format(" and (a.Status ={0} or a.Status ={1}) ", (int)MalfunctionHandleStatus.Fixed, (int)MalfunctionHandleStatus.Finished));

                if (term.Status == 3)
                    strSql.Append(string.Format(" and a.Status <>{0} and a.Status <>{1} and a.Status <>{2} ", (int)MalfunctionHandleStatus.FunctionalityRestore, (int)MalfunctionHandleStatus.Fixed, (int)MalfunctionHandleStatus.Finished));

                if (!string.IsNullOrEmpty(term.MaintainSataff))
                    strSql.Append(string.Format(" and a.SheetID IN (SELECT SheetID FROM FM2E_MalfunctionMaintainDetail, FM2E_MalfunctionMaintainStaff  WHERE FM2E_MalfunctionMaintainDetail.MaintainID = FM2E_MalfunctionMaintainStaff.MaintainID AND FM2E_MalfunctionMaintainStaff.MaintenanceStaff = '{0}')", term.MaintainSataff));

                if (!string.IsNullOrEmpty(term.ECompanyID))
                    strSql.Append(string.Format(" and a.SheetID IN (SELECT FM2E_MaintainedEquipment.SheetID FROM dbo.FM2E_MaintainedEquipment, FM2E_Equipment WHERE FM2E_MaintainedEquipment.EquipmentNO = FM2E_Equipment.EquipmentNO AND FM2E_Equipment.CompanyID = '{0}')", term.ECompanyID));



                if (!string.IsNullOrEmpty(term.SystemID))
                    strSql.AppendFormat(" and a.SystemID='{0}'", term.SystemID);

                if (term.DepartmentID != 0)
                    strSql.AppendFormat(" and a.DepartmentID={0}", term.DepartmentID);

                if (!string.IsNullOrEmpty(term.AddressCode) && term.AddressCode != "00")
                {
                    strSql.AppendFormat(" and c.AddressCode like '{0}%'", term.AddressCode);
                }

                if (DateTime.Compare(term.ReportDateFrom, DateTime.MinValue) != 0)
                {
                    DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                    if (DateTime.Compare(term.ReportDateFrom, sqlMinDate) < 0)
                        term.ReportDateFrom = sqlMinDate;

                    strSql.Append(" and a.ReportDate>='" + term.ReportDateFrom.ToString("yyyy-MM-dd") + " 00:00:00'");
                }

                if (DateTime.Compare(term.ReportDateTo, DateTime.MinValue) != 0)
                {
                    DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                    if (DateTime.Compare(term.ReportDateTo, sqlMaxDate) > 0)
                        term.ReportDateTo = sqlMaxDate;

                    strSql.Append(" and a.ReportDate<='" + term.ReportDateTo.ToString("yyyy-MM-dd") + " 23:59:59'");
                }

                if (term.RecordDepartment != 0)
                    strSql.Append(string.Format(" and (a.RecordDept in (select DepartmentID from FM2E_GetSubDepartments({0})))", term.RecordDepartment));

                strSql.Append(" and a.Status<>" + (int)MalfunctionHandleStatus.Canceled);  //需要排除已撤消的故障单
                strSql.Append(" GROUP BY a.MalfunctionRank, a.Status, a.MaintainDept");

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        list.Add(GetMalfunctionStatisticInfo(rd));
                    }
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("统计时发生错误", ex);
            }
            return list;
        }

        /// <summary>
        /// 获取所有故障设备
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList IMalfunctionHandle.GetFaultyEquipments(MalfunctionSearchInfo term)
        {
            ArrayList list = new ArrayList();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select a.SheetID,a.SheetNO,b.EquipmentNO,b.EquipmentName,a.SystemID,c.SystemName,a.AddressID,d.AddressName ");
                strSql.Append(" from FM2E_MalfunctionHandle a ");
                strSql.Append(" inner join FM2E_FaultyEquipments b on a.SheetID=b.SheetID ");
                strSql.Append(" left join FM2E_System c on a.SystemID=c.SystemID ");
                strSql.Append(" left join FM2E_Address d on a.AddressID=d.ID ");
                strSql.Append(" where 1=1 ");

                if (!string.IsNullOrEmpty(term.SystemID))
                    strSql.Append(" and a.SystemID='" + term.SystemID + "'");

                if (!string.IsNullOrEmpty(term.AddressCode) && term.AddressCode != "00")
                {
                    strSql.Append(" and d.AddressCode like '" + term.AddressCode + "%' ");
                }

                if (DateTime.Compare(term.ReportDateFrom, DateTime.MinValue) != 0)
                {
                    DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                    if (DateTime.Compare(term.ReportDateFrom, sqlMinDate) < 0)
                        term.ReportDateFrom = sqlMinDate;

                    strSql.Append(" and a.ReportDate>='" + term.ReportDateFrom.ToString("yyyy-MM-dd") + " 00:00:00'");
                }

                if (DateTime.Compare(term.ReportDateTo, DateTime.MinValue) != 0)
                {
                    DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                    if (DateTime.Compare(term.ReportDateTo, sqlMaxDate) > 0)
                        term.ReportDateTo = sqlMaxDate;

                    strSql.Append(" and a.ReportDate<='" + term.ReportDateTo.ToString("yyyy-MM-dd") + " 23:59:59'");
                }

                ////只选出未修好的设备
                //strSql.Append(" and a.Status<>" + (int)MalfunctionHandleStatus.Canceled);
                //strSql.Append(" and a.Status<>" + (int)MalfunctionHandleStatus.Finished);
                //strSql.Append(" and a.Status<>" + (int)MalfunctionHandleStatus.Fixed);

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        FaultyEquipmentInfo item = new FaultyEquipmentInfo();

                        if (!Convert.IsDBNull(rd["EquipmentName"]))
                            item.EquipmentName = Convert.ToString(rd["EquipmentName"]);

                        if (!Convert.IsDBNull(rd["EquipmentNO"]))
                            item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

                        if (!Convert.IsDBNull(rd["SystemID"]))
                            item.SystemID = Convert.ToString(rd["SystemID"]);

                        if (!Convert.IsDBNull(rd["SystemName"]))
                            item.SystemName = Convert.ToString(rd["SystemName"]);

                        if (!Convert.IsDBNull(rd["AddressID"]))
                            item.AddressID = Convert.ToInt64(rd["AddressID"]);

                        if (!Convert.IsDBNull(rd["AddressName"]))
                            item.AddressName = Convert.ToString(rd["AddressName"]);

                        list.Add(item);
                    }
                }

            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取故障设备数据时发生错误", ex);
            }

            return list;
        }

        /// <summary>
        /// 获取所有故障设备
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList IMalfunctionHandle.GetMaintainedEquipments(MalfunctionSearchInfo term)
        {
            ArrayList list = new ArrayList();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select a.SheetID,d.DepartmentID,a.SheetNO,b.MaintainDate,b.EquipmentNO,b.EquipmentName,b.Model,b.SerialNum,b.LastAddress,b.MaintainResult,a.SystemID,c.SystemName,a.AddressID,d.AddressCode,d.AddressName,a.RepairTime,a.RepairUnit,a.ActualRepairTime,a.ReportDate ");
                strSql.Append(" from FM2E_MalfunctionHandle a ");
                strSql.Append(" inner join FM2E_MaintainedEquipment b on a.SheetID=b.SheetID ");
                strSql.Append(" left join FM2E_System c on a.SystemID=c.SystemID ");
                strSql.Append(" left join FM2E_Address d on a.AddressID=d.ID ");
                strSql.Append(" left join FM2E_Equipment e on b.EquipmentNO = e.EquipmentNO ");
                strSql.Append(" where 1=1 ");

                if (!string.IsNullOrEmpty(term.CompanyID))
                    strSql.Append(" and e.CompanyID = '" + term.CompanyID + "' ");

                if (term.DepartmentID != 0)
                {
                    IList addresslist = new Address().GetAddressByMaintainDept(term.DepartmentID);
                    if (addresslist != null && addresslist.Count > 0)
                    {
                        int i = 0;
                        foreach (AddressInfo addressinfo in addresslist)
                        {
                            if (i == 0)
                                strSql.Append(" and ( d.AddressCode like '" + addressinfo.AddressCode + "%' ");
                            else
                                strSql.Append(" or d.AddressCode like '" + addressinfo.AddressCode + "%' ");
                            i++;
                        }
                        strSql.Append(" ) ");
                    }
                    else
                    {
                        strSql.Append(" and 1=2 ");
                    }
                }

                //if (term.DepartmentID != 0)
                //    strSql.Append(" and d.DepartmentID=" + term.DepartmentID + "");

                if (!string.IsNullOrEmpty(term.SystemID))
                    strSql.Append(" and a.SystemID='" + term.SystemID + "'");

                if (!string.IsNullOrEmpty(term.AddressCode) && term.AddressCode != "00")
                {
                    strSql.Append(" and d.AddressCode like '" + term.AddressCode + "%' ");
                }

                if (DateTime.Compare(term.ReportDateFrom, DateTime.MinValue) != 0)
                {
                    DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                    if (DateTime.Compare(term.ReportDateFrom, sqlMinDate) < 0)
                        term.ReportDateFrom = sqlMinDate;

                    strSql.Append(" and a.ReportDate>='" + term.ReportDateFrom.ToString("yyyy-MM-dd") + " 00:00:00'");
                }

                if (DateTime.Compare(term.ReportDateTo, DateTime.MinValue) != 0)
                {
                    DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                    if (DateTime.Compare(term.ReportDateTo, sqlMaxDate) > 0)
                        term.ReportDateTo = sqlMaxDate;

                    strSql.Append(" and a.ReportDate<='" + term.ReportDateTo.ToString("yyyy-MM-dd") + " 23:59:59'");

                    strSql.Append(" and b.MaintainDate <='" + term.ReportDateTo.ToString("yyyy-MM-dd") + " 23:59:59'");
                }

                strSql.Append(" order by b.MaintainDate ");

                //strSql.Append(string.Format(" and MaintainResult<>{0} and MaintainResult<>{1} ", (int)MaintainedEquipmentStatus.Replace, (int)MaintainedEquipmentStatus.Scrapped));

                ////只选出未修好的设备
                //strSql.Append(" and a.Status<>" + (int)MalfunctionHandleStatus.Canceled);
                //strSql.Append(" and a.Status<>" + (int)MalfunctionHandleStatus.Finished);
                //strSql.Append(" and a.Status<>" + (int)MalfunctionHandleStatus.Fixed);

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        MaintainedEquipmentInfo item = new MaintainedEquipmentInfo();

                        if (!Convert.IsDBNull(rd["AddressCode"]))
                            item.AddressCode = Convert.ToString(rd["AddressCode"]);

                        if (!Convert.IsDBNull(rd["SheetID"]))
                            item.SheetID = Convert.ToInt64(rd["SheetID"]);

                        if (!Convert.IsDBNull(rd["EquipmentName"]))
                            item.EquipmentName = Convert.ToString(rd["EquipmentName"]);

                        if (!Convert.IsDBNull(rd["EquipmentNO"]))
                            item.EquipmentNO = Convert.ToString(rd["EquipmentNO"]);

                        if (!Convert.IsDBNull(rd["Model"]))
                            item.Model = Convert.ToString(rd["Model"]);

                        if (!Convert.IsDBNull(rd["SerialNum"]))
                            item.SerialNum = Convert.ToString(rd["SerialNum"]);

                        if (!Convert.IsDBNull(rd["LastAddress"]))
                            item.LastAddress = Convert.ToString(rd["LastAddress"]);

                        if (!Convert.IsDBNull(rd["MaintainResult"]))
                            item.MaintainResult = (MaintainedEquipmentStatus)Convert.ToInt32(rd["MaintainResult"]);

                        if (!Convert.IsDBNull(rd["RepairTime"]))
                            item.RepairTime = Convert.ToInt32(rd["RepairTime"]);

                        if (!Convert.IsDBNull(rd["RepairUnit"]))
                            item.RepairUnit = Convert.ToInt32(rd["RepairUnit"]);

                        if (!Convert.IsDBNull(rd["ActualRepairTime"]))
                            item.ActualRepairTime = Convert.ToInt32(rd["ActualRepairTime"]);

                        if (!Convert.IsDBNull(rd["ReportDate"]))
                            item.ReportDate = Convert.ToDateTime(rd["ReportDate"]);

                        if (!Convert.IsDBNull(rd["DepartmentID"]))
                            item.MainteamID = Convert.ToInt64(rd["DepartmentID"]);

                        if (item.EquipmentName != "")
                        {
                            list.Add(item);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取故障设备数据时发生错误", ex);
            }

            return list;
        }


        /// <summary>
        /// 根据条件获取故障设备总数列表
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList IMalfunctionHandle.GetMaintainedEquipmentCount(MalfunctionSearchInfo term)
        {
            ArrayList list = new ArrayList();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select count(b.EquipmentName) as Count,b.EquipmentName,b.Model ");
                strSql.Append(" from FM2E_MalfunctionHandle a ");
                strSql.Append(" inner join FM2E_MaintainedEquipment b on a.SheetID=b.SheetID ");
                strSql.Append(" left join FM2E_System c on a.SystemID=c.SystemID ");
                strSql.Append(" left join FM2E_Address d on a.AddressID=d.ID ");
                strSql.Append(" left join FM2E_Equipment e on b.EquipmentNO = e.EquipmentNO ");
                strSql.Append(" where 1=1 ");

                if (!string.IsNullOrEmpty(term.CompanyID))
                    strSql.Append(" and e.CompanyID = '" + term.CompanyID + "' ");

                if (term.DepartmentID != 0)
                {
                    IList addresslist = new Address().GetAddressByMaintainDept(term.DepartmentID);
                    if (addresslist != null && addresslist.Count > 0)
                    {
                        int i = 0;
                        foreach (AddressInfo addressinfo in addresslist)
                        {
                            if (i == 0)
                                strSql.Append(" and ( d.AddressName like '" + addressinfo.AddressName + "%' ");
                            else
                                strSql.Append(" or d.AddressName like '" + addressinfo.AddressName + "%' ");
                            i++;
                        }
                        strSql.Append(" ) ");
                    }
                    else
                    {
                        strSql.Append(" and 1=2 ");
                    }
                }

                if (!string.IsNullOrEmpty(term.SystemID))
                    strSql.Append(" and a.SystemID='" + term.SystemID + "'");

                if (!string.IsNullOrEmpty(term.AddressCode) && term.AddressCode != "00")
                {
                    strSql.Append(" and d.AddressCode like '" + term.AddressCode + "%' ");
                }

                if (DateTime.Compare(term.ReportDateFrom, DateTime.MinValue) != 0)
                {
                    DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                    if (DateTime.Compare(term.ReportDateFrom, sqlMinDate) < 0)
                        term.ReportDateFrom = sqlMinDate;

                    strSql.Append(" and a.ReportDate>='" + term.ReportDateFrom.ToString("yyyy-MM-dd") + " 00:00:00'");
                }

                if (DateTime.Compare(term.ReportDateTo, DateTime.MinValue) != 0)
                {
                    DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                    if (DateTime.Compare(term.ReportDateTo, sqlMaxDate) > 0)
                        term.ReportDateTo = sqlMaxDate;

                    strSql.Append(" and a.ReportDate<='" + term.ReportDateTo.ToString("yyyy-MM-dd") + " 23:59:59'");
                }

                strSql.Append(" group by b.EquipmentName,b.Model");

                ////只选出未修好的设备
                //strSql.Append(" and a.Status<>" + (int)MalfunctionHandleStatus.Canceled);
                //strSql.Append(" and a.Status<>" + (int)MalfunctionHandleStatus.Finished);
                //strSql.Append(" and a.Status<>" + (int)MalfunctionHandleStatus.Fixed);

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        ComputeMaintainedEquipmentRateInfo item = new ComputeMaintainedEquipmentRateInfo();

                        if (!Convert.IsDBNull(rd["EquipmentName"]))
                            item.Name = Convert.ToString(rd["EquipmentName"]);

                        if (!Convert.IsDBNull(rd["Model"]))
                            item.Model = Convert.ToString(rd["Model"]);

                        if (!Convert.IsDBNull(rd["Count"]))
                            item.Count = Convert.ToInt32(rd["Count"]);

                        if (item.Name != "")
                        {
                            list.Add(item);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取故障设备数据时发生错误", ex);
            }

            return list;
        }

        /// <summary>
        /// 根据条件获取未修复故障设备总数列表
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList IMalfunctionHandle.GetRepairedEquipmentCount(MalfunctionSearchInfo term)
        {
            ArrayList list = new ArrayList();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select count(b.EquipmentName) as Count,b.EquipmentName,b.Model ");
                strSql.Append(" from FM2E_MalfunctionHandle a ");
                strSql.Append(" inner join FM2E_MaintainedEquipment b on a.SheetID=b.SheetID ");
                strSql.Append(" left join FM2E_System c on a.SystemID=c.SystemID ");
                strSql.Append(" left join FM2E_Address d on a.AddressID=d.ID ");
                strSql.Append(" left join FM2E_Equipment e on b.EquipmentNO = e.EquipmentNO ");
                strSql.Append(" where 1=1 ");

                if (!string.IsNullOrEmpty(term.CompanyID))
                    strSql.Append(" and e.CompanyID = '" + term.CompanyID + "' ");

                if (term.DepartmentID != 0)
                {
                    IList addresslist = new Address().GetAddressByMaintainDept(term.DepartmentID);
                    if (addresslist != null && addresslist.Count > 0)
                    {
                        int i = 0;
                        foreach (AddressInfo addressinfo in addresslist)
                        {
                            if (i == 0)
                                strSql.Append(" and ( d.AddressName like '" + addressinfo.AddressName + "%' ");
                            else
                                strSql.Append(" or d.AddressName like '" + addressinfo.AddressName + "%' ");
                            i++;
                        }
                        strSql.Append(" ) ");
                    }
                    else
                    {
                        strSql.Append(" and 1=2 ");
                    }
                }

                if (!string.IsNullOrEmpty(term.SystemID))
                    strSql.Append(" and a.SystemID='" + term.SystemID + "'");

                if (!string.IsNullOrEmpty(term.AddressCode) && term.AddressCode != "00")
                {
                    strSql.Append(" and d.AddressCode like '" + term.AddressCode + "%' ");
                }

                if (DateTime.Compare(term.ReportDateFrom, DateTime.MinValue) != 0)
                {
                    DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                    if (DateTime.Compare(term.ReportDateFrom, sqlMinDate) < 0)
                        term.ReportDateFrom = sqlMinDate;

                    strSql.Append(" and a.ReportDate>='" + term.ReportDateFrom.ToString("yyyy-MM-dd") + " 00:00:00'");
                }

                if (DateTime.Compare(term.ReportDateTo, DateTime.MinValue) != 0)
                {
                    DateTime sqlMaxDate = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
                    if (DateTime.Compare(term.ReportDateTo, sqlMaxDate) > 0)
                        term.ReportDateTo = sqlMaxDate;

                    strSql.Append(" and a.ReportDate<='" + term.ReportDateTo.ToString("yyyy-MM-dd") + " 23:59:59'");
                }

                ////只选出未修好的设备
                strSql.Append(" and b.MaintainResult=" + (int)MaintainedEquipmentStatus.Wait4Repair);

                strSql.Append(" group by b.EquipmentName,b.Model");

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        ComputeMaintainedEquipmentRateInfo item = new ComputeMaintainedEquipmentRateInfo();

                        if (!Convert.IsDBNull(rd["EquipmentName"]))
                            item.Name = Convert.ToString(rd["EquipmentName"]);

                        if (!Convert.IsDBNull(rd["Model"]))
                            item.Model = Convert.ToString(rd["Model"]);

                        if (!Convert.IsDBNull(rd["Count"]))
                            item.Count = Convert.ToInt32(rd["Count"]);

                        if (item.Name != "")
                        {
                            list.Add(item);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取故障设备数据时发生错误", ex);
            }

            return list;
        }


        /// <summary>
        /// 根据条件获取未修复故障设备总数列表
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList IMalfunctionHandle.GetWait4RepairedEquipmentCount(MalfunctionSearchInfo term)
        {
            ArrayList list = new ArrayList();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select count(b.EquipmentName) as Count,b.EquipmentName,b.Model ");
                strSql.Append(" from FM2E_MalfunctionHandle a ");
                strSql.Append(" inner join FM2E_MaintainedEquipment b on a.SheetID=b.SheetID ");
                strSql.Append(" left join FM2E_System c on a.SystemID=c.SystemID ");
                strSql.Append(" left join FM2E_Address d on a.AddressID=d.ID ");
                strSql.Append(" left join FM2E_Equipment e on b.EquipmentNO = e.EquipmentNO ");
                strSql.Append(" where 1=1 ");

                if (!string.IsNullOrEmpty(term.CompanyID))
                    strSql.Append(" and e.CompanyID = '" + term.CompanyID + "' ");

                if (term.DepartmentID != 0)
                {
                    IList addresslist = new Address().GetAddressByMaintainDept(term.DepartmentID);
                    if (addresslist != null && addresslist.Count > 0)
                    {
                        int i = 0;
                        foreach (AddressInfo addressinfo in addresslist)
                        {
                            if (i == 0)
                                strSql.Append(" and ( d.AddressName like '" + addressinfo.AddressName + "%' ");
                            else
                                strSql.Append(" or d.AddressName like '" + addressinfo.AddressName + "%' ");
                            i++;
                        }
                        strSql.Append(" ) ");
                    }
                    else
                    {
                        strSql.Append(" and 1=2 ");
                    }
                }

                if (!string.IsNullOrEmpty(term.SystemID))
                    strSql.Append(" and a.SystemID='" + term.SystemID + "'");

                if (!string.IsNullOrEmpty(term.AddressCode) && term.AddressCode != "00")
                {
                    strSql.Append(" and d.AddressCode like '" + term.AddressCode + "%' ");
                }

                if (DateTime.Compare(term.ReportDateFrom, DateTime.MinValue) != 0)
                {
                    DateTime sqlMinDate = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
                    if (DateTime.Compare(term.ReportDateFrom, sqlMinDate) < 0)
                        term.ReportDateFrom = sqlMinDate;

                    strSql.Append(" and a.ReportDate<='" + term.ReportDateFrom.ToString("yyyy-MM-dd") + " 00:00:00'");
                }

                ////只选出未修好的设备
                strSql.Append(" and b.MaintainResult=" + (int)MaintainedEquipmentStatus.Wait4Repair);

                strSql.Append(" group by b.EquipmentName,b.Model");

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        ComputeMaintainedEquipmentRateInfo item = new ComputeMaintainedEquipmentRateInfo();

                        if (!Convert.IsDBNull(rd["EquipmentName"]))
                            item.Name = Convert.ToString(rd["EquipmentName"]);

                        if (!Convert.IsDBNull(rd["Model"]))
                            item.Model = Convert.ToString(rd["Model"]);

                        if (!Convert.IsDBNull(rd["Count"]))
                            item.Count = Convert.ToInt32(rd["Count"]);

                        if (item.Name != "")
                        {
                            list.Add(item);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取故障设备数据时发生错误", ex);
            }

            return list;
        }



        //public void UpdateWorkInstance(long p)
        //{
        //    string sql = string.Format("update FM2E_WorkflowInstance set TableName = 'FM2E_MalfunctionHandle',CurrentStateName = 'Wait4MaintenanceConfirm',StatusDescription='等待维护员确定',WorkflowName='SGS_MalFunctionWorklow' where DataID = '{0}' ", p);
        //    SqlConnection con = new SqlConnection(SQLHelper.ConnectionString);
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}

        /// <summary>
        /// 根据条件获取设备总数列表
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IList IMalfunctionHandle.GetAllEquipmentCount(MalfunctionSearchInfo term)
        {
            ArrayList list = new ArrayList();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select count(Name) as Count,Name,Model from EquipmentView ");
                strSql.Append(" where 1=1 ");

                if (!string.IsNullOrEmpty(term.CompanyID))
                    strSql.Append(" and CompanyID = '" + term.CompanyID + "' ");

                if (term.DepartmentID != 0)
                {
                    IList addresslist = new Address().GetAddressByMaintainDept(term.DepartmentID);
                    if (addresslist != null && addresslist.Count > 0)
                    {
                        int i = 0;
                        foreach (AddressInfo addressinfo in addresslist)
                        {
                            if (i == 0)
                                strSql.Append(" and ( AddressName like '" + addressinfo.AddressName + "%' ");
                            else
                                strSql.Append(" or AddressName like '" + addressinfo.AddressName + "%' ");
                            i++;
                        }
                        strSql.Append(" ) ");
                    }
                    else
                    {
                        strSql.Append(" and 1=2 ");
                    }
                }

                if (!string.IsNullOrEmpty(term.SystemID))
                    strSql.Append(" and SystemID='" + term.SystemID + "'");

                if (!string.IsNullOrEmpty(term.AddressCode) && term.AddressCode != "00")
                {
                    strSql.Append(" and AddressCode like '" + term.AddressCode + "%' ");
                }
                strSql.Append(" group by Name,Model");


                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        ComputeMaintainedEquipmentRateInfo item = new ComputeMaintainedEquipmentRateInfo();

                        if (!Convert.IsDBNull(rd["Name"]))
                            item.Name = Convert.ToString(rd["Name"]);

                        if (!Convert.IsDBNull(rd["Model"]))
                            item.Model = Convert.ToString(rd["Model"]);

                        if (!Convert.IsDBNull(rd["Count"]))
                            item.Count = Convert.ToInt32(rd["Count"]);

                        if (item.Name != "")
                        {
                            list.Add(item);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取故障设备数据时发生错误", ex);
            }

            return list;
        }

        /// <summary>
        /// 获取某张故障处理单的故障设备
        /// </summary>
        /// <param name="sheetID"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        IList IMalfunctionHandle.GetFaultyEquipments(long sheetID, DbTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from FM2E_FaultyEquipments ");
            strSql.Append(" where SheetID=@SheetID");
            SqlParameter[] parameters = {
					new SqlParameter("@SheetID", SqlDbType.BigInt)};
            parameters[0].Value = sheetID;

            ArrayList list = new ArrayList();

            using (SqlDataReader rd = SQLHelper.ExecuteReader((SqlTransaction)trans, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    list.Add(GetFaultyEquipmentData(rd));
                }
            }

            return list;
        }

        public decimal CountPriceByNO(string equipmentNO)
        {
            decimal price = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT sum(MaintainFee) AS price FROM FM2E_EquipmentMaintainRecordView where EquipmentNO ='" + equipmentNO + "' ");

            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
            {
                while (rd.Read())
                {
                    if (!Convert.IsDBNull(rd["price"]))
                        price = Convert.ToDecimal(rd["price"]);
                }
            }

            return price;
        }
        #endregion
    }
}
