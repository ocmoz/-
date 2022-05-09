using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

using FM2E.Model.Equipment;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;
using FM2E.IDAL.Equipment;
using FM2E.SQLServerDAL.Utils;


namespace FM2E.SQLServerDAL.Equipment
{
    /// <summary>
    /// 采购管理模块数据库访问类
    /// </summary>
    public class Purchase:IPurchase
    {
        const string TABLE_PURCHASE_ORDER = "FM2E_PurchasePlan";
        const string TABLE_PURCHASE_ORDER_DETAIL = "FM2E_PurchasePlanDetail";

        const string TABLE_PURCHASE_APPROVAL = "FM2E_PurchaseApproval";
        const string TABLE_PURCHASE_ORDER_MODIFTY = "FM2E_PurchaseModifyRecord";

        const string TABLE_PURCHASE_RECORD = "FM2E_PurchaseRecord";

        const string TABLE_WAREHOUSE = "FM2E_WareHouse";
        const string TABLE_COMPANY = "FM2E_Company";

        const string VIEW_PURCHASE_ORDER = "FM2E_PurchasePlanView";
        const string VIEW_PURCHASE_ORDER_DETAIL = "FM2E_PurchasePlanDetailView";
        const string VIEW_PURCHASE_RECORD_VIEW = "FM2E_PurchaseRecordView";
        const string VIEW_PURCHASE_MODIFY_VIEW = "FM2E_PurchaseModifyRecordView";
        const string VIEW_PURCHASE_APPROVAL_VIEW = "FM2E_PurchaseApprovalRecordView";
        #region IPurchase 成员


        /// <summary>
        /// 获取下一子订单的序号
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="suborderindex"></param>
        /// <returns></returns>
        private short GetNextSubOrderIndex( SqlTransaction trans, string orderid, short suborderindex)
        {
            StringBuilder strSqlRead = new StringBuilder();
            strSqlRead.Append("select top 1 NextOrderIndex from " + TABLE_PURCHASE_ORDER + " ");
            strSqlRead.Append(" where PurchaseOrderID=@PurchaseOrderID and SubOrderIndex=@SubOrderIndex ;");
           

            SqlParameter[] parameters = {
					
					new SqlParameter("@PurchaseOrderID", SqlDbType.VarChar,20),

					new SqlParameter("@SubOrderIndex", SqlDbType.TinyInt,1)};

            parameters[0].Value = orderid;
            parameters[1].Value = suborderindex;

            //读取ID
            short id = 1;
            using (SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSqlRead.ToString(), parameters))
            {
                while (rdr.Read())
                {
                    id = Convert.ToInt16(rdr["NextOrderIndex"]);
                }
            }
            return id;
        }
        /// <summary>
        /// 更新下一ID
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="orderid"></param>
        /// <param name="suborderindex"></param>
        private void UpdateNextIndex(SqlTransaction trans, string orderid, short suborderindex)
        {
            StringBuilder strUpdate = new StringBuilder();
            strUpdate.Append(" update " + TABLE_PURCHASE_ORDER + " ");
            strUpdate.Append(" set NextOrderIndex = NextOrderIndex+1 ");
            strUpdate.Append(" where PurchaseOrderID=@PurchaseOrderID and SubOrderIndex=@SubOrderIndex ");
            SqlParameter[] parameters = {
					
					new SqlParameter("@PurchaseOrderID", SqlDbType.VarChar,20),

					new SqlParameter("@SubOrderIndex", SqlDbType.TinyInt,1)};

            parameters[0].Value = orderid;
            parameters[1].Value = suborderindex;

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strUpdate.ToString(), parameters);
        }
        /// <summary>
        /// 获取下一子订单的序号
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="suborderindex"></param>
        /// <returns></returns>
        short IPurchase.GetNextSubOrderIndex(string orderid, short suborderindex)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            short id = 1;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先获取ID
                id = GetNextSubOrderIndex(trans, orderid, suborderindex);

                //更新下一ID
                UpdateNextIndex(trans, orderid, suborderindex);
                //事务提交
                trans.Commit();
            }
            catch (SqlException sqlex)
            {
                //回滚
                trans.Rollback();
                throw sqlex;
            }
            catch (Exception ex)
            {
                //回滚
                trans.Rollback();
                throw ex;
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
            return id;
           
        }

        /// <summary>
        /// 插入一张新的申请单
        /// </summary>
        /// <param name="order">申请单</param>
        long IPurchase.InsertPurchaseApply(PurchaseOrderInfo order)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            long id = 0;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先插入消息
                id = InsertPurchaseOrder(trans, order);

                order.ID = id;
                //插入消息对象列表
                InsertPurchaseOrderDetail(trans, order, id);

                if (order.ModifyInfo != null)
                {
                    InsertModifyRecord(trans, order, order.ModifyInfo);
                }
                //事务提交
                trans.Commit();
            }
            catch (SqlException sqlex)
            {
                //回滚
                trans.Rollback();
                throw sqlex;
            }
            catch (Exception ex)
            {
                //回滚
                trans.Rollback();
                throw ex;
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
            return id;
        }

        /// <summary>
        /// 根据ID获取一张申请单，包含详情
        /// </summary>
        /// <param name="orderid">采购单的数据库ID</param>
        /// <returns>含详情的采购单</returns>
        PurchaseOrderInfo IPurchase.GetPurchaseOrderByID(long orderid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from " + VIEW_PURCHASE_ORDER + " ");
            strSql.Append(" where ID=@ID ;");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = orderid;
            PurchaseOrderInfo order = new PurchaseOrderInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    order = GetDataOrder(rd);
                    break;
                }
            }

            order.DetailList = GetOrderDetailList(orderid);
            order.ApprovalList = GetOrderApprovalList(orderid);
            order.ModifyRecordList = GetOrderModifyList(orderid);
            order.RelatedOrders = GetRelatedOrders(order.PurchaseOrderID, order.SubOrderIndex);
            return order;
        }

        /// <summary>
        /// 根据申请单的逻辑单号，获取所有的相关订单，按照子序号排序
        /// </summary>
        /// <param name="ordersn">采购单逻辑单号</param>
        /// <returns>采购单列表，含详情</returns>
        IList IPurchase.GetPurchaseOrdersBySn(string ordersn)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + VIEW_PURCHASE_ORDER + " ");
            strSql.Append(" where PurchaseOrderID=@PurchaseOrderID ");
            strSql.Append(" order by SubOrderIndex;");
            SqlParameter[] parameters = {
					new SqlParameter("@PurchaseOrderID", SqlDbType.VarChar,20)};
            parameters[0].Value = ordersn;
            List<PurchaseOrderInfo> orderList = new List<PurchaseOrderInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    PurchaseOrderInfo order = GetDataOrder(rd);
                    orderList.Add(order);
                }
            }

            //为每个采购单装配详情
            foreach (PurchaseOrderInfo order in orderList)
            {
                //order.DetailList = GetOrderDetailList(order.ID);
                //order.ApprovalList = GetOrderApprovalList(order.ID);
                //order.ModifyRecordList = GetOrderModifyList(order.ID);
                //order.RelatedOrders = GetRelatedOrders(order.PurchaseOrderID, order.SubOrderIndex);
            }

            return orderList;
        }

        /// <summary>
        /// 分页获取申请者自己的所有申请单(不包括已经完成的)，按照更新时间倒序，草稿在前，列表中的采购单不含详情
        /// </summary>
        /// <param name="userid">申请者ID</param>
        /// <returns>申请单列表</returns>
        IList IPurchase.GetPurchaseOrdersByApplier(int pageIndex, int pageSize, out int recordCount, string userid)
        {
           try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = VIEW_PURCHASE_ORDER;
                qp.ReturnFields = string.Format("*"
                    + ",CAST(" + VIEW_PURCHASE_ORDER + ".Status - " + (int)PurchaseOrderStatus.DRAFT + " AS bit) as StatusSort" //草稿排在前面
                    + ",{0} as SubmitTimeSort", VIEW_PURCHASE_ORDER + ".UpdateTime");
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + VIEW_PURCHASE_ORDER + ".[Applicant]='" + userid + "'"
                    + " and " + VIEW_PURCHASE_ORDER + ".[Status]<>" + (int)PurchaseOrderStatus.INWAREHOUSEFINISH;
                qp.OrderBy = string.Format("order by {0} asc, {1} desc","StatusSort", "SubmitTimeSort");
                return SQLHelper.GetObjectList(this.GetDataOrder, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }

        /// <summary>
        /// 分页获取申请者自己的所有申请单(已经完成的)，按照更新时间倒序，草稿在前，列表中的采购单不含详情
        /// </summary>
        /// <param name="userid">申请者ID</param>
        /// <returns>申请单列表</returns>
        IList IPurchase.GetPurchaseOrdersByApplierFinish(int pageIndex, int pageSize, out int recordCount, string userid)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = VIEW_PURCHASE_ORDER;
                qp.ReturnFields = string.Format("*"
                    + ",CAST(" + VIEW_PURCHASE_ORDER + ".Status - " + (int)PurchaseOrderStatus.DRAFT + " AS bit) as StatusSort" //草稿排在前面
                    + ",{0} as SubmitTimeSort", VIEW_PURCHASE_ORDER + ".UpdateTime");
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + VIEW_PURCHASE_ORDER + ".[Applicant]='" + userid + "'"
                    + " and " + VIEW_PURCHASE_ORDER + ".[Status]=" + (int)PurchaseOrderStatus.INWAREHOUSEFINISH;
                qp.OrderBy = string.Format("order by {0} asc, {1} desc", "StatusSort", "SubmitTimeSort");
                return SQLHelper.GetObjectList(this.GetDataOrder, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }

        /// <summary>
        /// 获取采购人所有的申请单
        /// </summary>
        /// <param name="userid">采购人ID</param>
        /// <returns>申请单列表</returns>
        IList IPurchase.GetPurchaseOrdersByPurchaser(int pageIndex, int pageSize, out int recordCount, string userid)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = VIEW_PURCHASE_ORDER + " INNER JOIN " + TABLE_PURCHASE_ORDER_DETAIL + " " +
                               "ON " + VIEW_PURCHASE_ORDER + ".ID = " + TABLE_PURCHASE_ORDER_DETAIL + ".ID";
                qp.ReturnFields = string.Format(
                    VIEW_PURCHASE_ORDER + ".*"
                    + ",{0} as SubmitTimeSort", VIEW_PURCHASE_ORDER + ".UpdateTime"
                    );
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_PURCHASE_ORDER_DETAIL + ".[Purchaser]='" + userid + "'" +
                    " and (" + TABLE_PURCHASE_ORDER_DETAIL + ".[Status]=" + (int)PurchaseOrderDetailStatus.WAITING4PURCHASE + ""
                    + " or " + TABLE_PURCHASE_ORDER_DETAIL + ".[Status]=" + (int)PurchaseOrderDetailStatus.PURCHASING + ")";

                qp.OrderBy = string.Format("order by {0} desc", "SubmitTimeSort");
                return SQLHelper.GetObjectListWithDistinct(this.GetDataOrder, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }


        /// <summary>
        /// 获取采购人所有的申请单，完成的
        /// </summary>
        /// <param name="userid">采购人ID</param>
        /// <returns>申请单列表</returns>
        IList IPurchase.GetPurchaseOrdersByPurchaserHistory(int pageIndex, int pageSize, out int recordCount, string userid)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = VIEW_PURCHASE_ORDER + " INNER JOIN " + TABLE_PURCHASE_ORDER_DETAIL + " " +
                               "ON " + VIEW_PURCHASE_ORDER + ".ID = " + TABLE_PURCHASE_ORDER_DETAIL + ".ID";
                qp.ReturnFields = string.Format(
                    VIEW_PURCHASE_ORDER + ".*"
                    + ",{0} as SubmitTimeSort", VIEW_PURCHASE_ORDER + ".UpdateTime"
                    );
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_PURCHASE_ORDER_DETAIL + ".[Purchaser]='" + userid + "'" +
                    " and (" + TABLE_PURCHASE_ORDER_DETAIL + ".[Status]=" + (int)PurchaseOrderDetailStatus.PURCHASINGFINISH +
                    " or " + TABLE_PURCHASE_ORDER_DETAIL + ".[Status]=" + (int)PurchaseOrderDetailStatus.CHECKFINISH +
                     " or " + TABLE_PURCHASE_ORDER_DETAIL + ".[Status]=" + (int)PurchaseOrderDetailStatus.INWAREHOUSEFINISH +
                    ")";

                qp.OrderBy = string.Format("order by {0} desc", "SubmitTimeSort");
                return SQLHelper.GetObjectListWithDistinct(this.GetDataOrder, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }


        /// <summary>
        /// 获取仓库对应的所有有待验收的采购单
        /// </summary>
        /// <param name="userid">仓库ID</param>
        /// <returns>申请单列表</returns>
        IList IPurchase.GetPurchaseOrders2Check(int pageIndex, int pageSize, out int recordCount, string warehouseid)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = VIEW_PURCHASE_ORDER + " INNER JOIN " + TABLE_PURCHASE_ORDER_DETAIL + " " +
                               "ON " + VIEW_PURCHASE_ORDER + ".ID = " + TABLE_PURCHASE_ORDER_DETAIL + ".ID";
                qp.ReturnFields = string.Format(
                    VIEW_PURCHASE_ORDER + ".*"
                    + ",{0} as SubmitTimeSort", VIEW_PURCHASE_ORDER + ".UpdateTime"
                    );
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_PURCHASE_ORDER_DETAIL + ".[WarehouseID]='" + warehouseid + "'" +
                    " and " + TABLE_PURCHASE_ORDER_DETAIL + ".[Status]=" + (int)PurchaseOrderDetailStatus.PURCHASINGFINISH + "";

                qp.OrderBy = string.Format("order by {0} desc", "SubmitTimeSort");
                return SQLHelper.GetObjectListWithDistinct(this.GetDataOrder, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }

        /// <summary>
        /// 获取仓库对应的所有完成验收的采购单
        /// </summary>
        /// <param name="userid">仓库ID</param>
        /// <returns>申请单列表</returns>
        IList IPurchase.GetPurchaseOrdersCheckHistory(int pageIndex, int pageSize, out int recordCount, string warehouseid)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = VIEW_PURCHASE_ORDER + " INNER JOIN " + TABLE_PURCHASE_ORDER_DETAIL + " " +
                               "ON " + VIEW_PURCHASE_ORDER + ".ID = " + TABLE_PURCHASE_ORDER_DETAIL + ".ID";
                qp.ReturnFields = string.Format(
                    VIEW_PURCHASE_ORDER + ".*"
                    + ",{0} as SubmitTimeSort", VIEW_PURCHASE_ORDER + ".UpdateTime"
                    );
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_PURCHASE_ORDER_DETAIL + ".[WarehouseID]='" + warehouseid + "'" +
                    " and (" + TABLE_PURCHASE_ORDER_DETAIL + ".[Status]=" + (int)PurchaseOrderDetailStatus.CHECKFINISH + ""+
                "      or " + TABLE_PURCHASE_ORDER_DETAIL + ".[Status]=" + (int)PurchaseOrderDetailStatus.INWAREHOUSEFINISH + ")";

                qp.OrderBy = string.Format("order by {0} desc", "SubmitTimeSort");
                return SQLHelper.GetObjectListWithDistinct(this.GetDataOrder, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }

        /// <summary>
        /// 根据仓库ID，获取需要进行入库的采购单
        /// </summary>
        /// <param name="companyid">仓库ID</param>
        /// <returns>进入入库流程的采购单</returns>
        IList IPurchase.GetPurchaseOrders2InWarehouse(int pageIndex, int pageSize, out int recordCount, string warehouseid)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = VIEW_PURCHASE_ORDER + " INNER JOIN " + TABLE_PURCHASE_ORDER_DETAIL + " " +
                               "ON " + VIEW_PURCHASE_ORDER + ".ID = " + TABLE_PURCHASE_ORDER_DETAIL + ".ID";
                qp.ReturnFields = string.Format(
                    VIEW_PURCHASE_ORDER + ".*"
                    + ",{0} as SubmitTimeSort", TABLE_PURCHASE_ORDER + ".UpdateTime"
                    );
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_PURCHASE_ORDER_DETAIL + ".[WarehouseID]='" + warehouseid + "'" +
                    " and " + TABLE_PURCHASE_ORDER_DETAIL + ".[Status]=" + (int)PurchaseOrderDetailStatus.CHECKFINISH + "";

                qp.OrderBy = string.Format("order by {0} desc", "SubmitTimeSort");
                return SQLHelper.GetObjectListWithDistinct(this.GetDataOrder, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }

        /// <summary>
        /// 根据仓库ID，获取入库完毕的采购单
        /// </summary>
        /// <param name="companyid">仓库ID</param>
        /// <returns>入库流程结束的采购单</returns>
        IList IPurchase.GetPurchaseOrdersInWarehouseHistroy(int pageIndex, int pageSize, out int recordCount, string warehouseid)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = VIEW_PURCHASE_ORDER + " INNER JOIN " + TABLE_PURCHASE_ORDER_DETAIL + " " +
                               "ON " + VIEW_PURCHASE_ORDER + ".ID = " + TABLE_PURCHASE_ORDER_DETAIL + ".ID";
                qp.ReturnFields = string.Format(
                    VIEW_PURCHASE_ORDER + ".*"

                    + ",{0} as SubmitTimeSort", VIEW_PURCHASE_ORDER + ".UpdateTime"
                    );
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_PURCHASE_ORDER_DETAIL + ".[WarehouseID]='" + warehouseid + "'" +
                    " and " + TABLE_PURCHASE_ORDER_DETAIL + ".[Status]=" + (int)PurchaseOrderDetailStatus.INWAREHOUSEFINISH + "";

                qp.OrderBy = string.Format("order by {0} desc", "SubmitTimeSort");
                return SQLHelper.GetObjectListWithDistinct(this.GetDataOrder, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }

        /// <summary>
        /// 获取一个公司中所有的申请单
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        IList IPurchase.GetPurchaseOrdersByCompany(int pageIndex, int pageSize, out int recordCount, string companyid)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = VIEW_PURCHASE_ORDER;
                qp.ReturnFields = string.Format(
                    VIEW_PURCHASE_ORDER + ".*"

                    + ",{0} as SubmitTimeSort", VIEW_PURCHASE_ORDER + ".UpdateTime");
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + VIEW_PURCHASE_ORDER + ".[CompanyID]='" + companyid + "'";
                qp.OrderBy = string.Format("order by {0} desc", "SubmitTimeSort");
                return SQLHelper.GetObjectList(this.GetDataOrder, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }

        /// <summary>
        /// 获取需要审批的所有的申请单
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="username">审批人ID</param>
        /// <returns>可以被username审批的申请单</returns>
        IList IPurchase.GetPurchaseOrders2Approval(int pageIndex, int pageSize, out int recordCount, string companyid, string username)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = VIEW_PURCHASE_ORDER;
                qp.ReturnFields = string.Format(
                    VIEW_PURCHASE_ORDER + ".*"

                    + ",CAST(" + VIEW_PURCHASE_ORDER + ".Status - " + (int)PurchaseOrderStatus.APPROVALING + " AS bit) as StatusSort" //审批中的排在前面
                    + ",{0} as SubmitTimeSort", VIEW_PURCHASE_ORDER + ".UpdateTime");
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + VIEW_PURCHASE_ORDER + ".[CompanyID]='" + companyid + "'" //选择公司
                           + " and (" + VIEW_PURCHASE_ORDER + ".[Status]=" + (int)PurchaseOrderStatus.WAITING4APPROVAL//等待审批的
                           + " or " + VIEW_PURCHASE_ORDER + ".[Approvaling]='" + username + "')";
                qp.OrderBy = string.Format("order by {0} asc, {1} desc", "StatusSort", "SubmitTimeSort");
                return SQLHelper.GetObjectList(this.GetDataOrder, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }


        /// <summary>
        /// 获取审批历史
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="username">审批人ID</param>
        /// <returns>已经被username审批的申请单</returns>
        IList IPurchase.GetPurchaseOrdersApprovalHistory(int pageIndex, int pageSize, out int recordCount, string companyid, string username)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = VIEW_PURCHASE_ORDER + " INNER JOIN " + TABLE_PURCHASE_APPROVAL + " " +
                               "ON " + VIEW_PURCHASE_ORDER + ".ID = " + TABLE_PURCHASE_APPROVAL + ".OrderSn";
                qp.ReturnFields = string.Format(
                    VIEW_PURCHASE_ORDER + ".*"

                    + ",{0} as SubmitTimeSort", VIEW_PURCHASE_ORDER + ".UpdateTime"
                    );
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + TABLE_PURCHASE_APPROVAL + ".[Approvaler]='" + username + "' and " + TABLE_PURCHASE_APPROVAL + ".[CompanyID]='" + companyid + "'";

                qp.OrderBy = string.Format("order by {0} desc", "SubmitTimeSort");
                return SQLHelper.GetObjectListWithDistinct(this.GetDataOrder, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }

        /// <summary>
        /// 删除申请单
        /// </summary>
        /// <param name="orderid">申请单ID</param>
        void IPurchase.DeletePurchaseOrder(long orderid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete "+ TABLE_PURCHASE_ORDER +" ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = orderid;

            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新采购单的信息，一般是编辑草稿，然后进行保存草稿或者提交审批
        /// </summary>
        /// <param name="order">新的采购单MODEL</param>
        void IPurchase.UpdatePurchaseOrder(PurchaseOrderInfo order)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                //建立连接，开始事务
                conn = new SqlConnection(SQLHelper.ConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                //先更新总表
                UpdatePurchaseOrder(trans, order);

                //详情表更新，使用先删除，后插入的方法
                UpdatePurchaseOrderDetail(trans, order);

                if(order.ModifyInfo!=null)
                    InsertModifyRecord(trans, order, order.ModifyInfo);
                //事务提交
                trans.Commit();
            }
            catch (SqlException sqlex)
            {
                //回滚
                trans.Rollback();
                throw sqlex;
            }
            catch (Exception ex)
            {
                //回滚
                trans.Rollback();
                throw ex;
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
        }

         /// <summary>
        /// 更新采购单的信息，不更新采购单详情
        /// </summary>
        /// <param name="order">新的采购单MODEL</param>
        void IPurchase.UpdatePurchaseOrderNoDetail(PurchaseOrderInfo order)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + TABLE_PURCHASE_ORDER + " set ");
            strSql.Append("NextOrderIndex=@NextOrderIndex,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("PurchaseOrderID=@PurchaseOrderID,");
            strSql.Append("PurchaseOrderName=@PurchaseOrderName,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("Applicant=@Applicant,");
            strSql.Append("SubmitTime=@SubmitTime,");
            strSql.Append("Approvaling=@Approvaling,");
            strSql.Append("Approvalers=@Approvalers,");
            strSql.Append("Status=@Status,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("SubOrderIndex=@SubOrderIndex,");
            strSql.Append("DeliveryStatus=@DeliveryStatus,");
            strSql.Append("PurchasingStatus=@PurchasingStatus,");
            strSql.Append("PlanTotalAmount=@PlanTotalAmount");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@NextOrderIndex", SqlDbType.TinyInt,1),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@PurchaseOrderID", SqlDbType.VarChar,20),
					new SqlParameter("@PurchaseOrderName", SqlDbType.NVarChar,30),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Applicant", SqlDbType.VarChar,20),
					new SqlParameter("@SubmitTime", SqlDbType.DateTime),
                    new SqlParameter("@Approvaling", SqlDbType.VarChar,20),
                    new SqlParameter("@Approvalers", SqlDbType.VarChar,200),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
					new SqlParameter("@SubOrderIndex", SqlDbType.TinyInt,1),
                               new SqlParameter("@DeliveryStatus", SqlDbType.TinyInt,1),
                               new SqlParameter("@PurchasingStatus", SqlDbType.TinyInt,1),
                    new SqlParameter("@PlanTotalAmount",SqlDbType.Decimal,9)
                                        };
            parameters[0].Value = order.ID;
            parameters[1].Value = order.NextOrderIndex;
            parameters[2].Value = DateTime.Now;
            parameters[3].Value = order.PurchaseOrderID;
            parameters[4].Value = order.PurchaseOrderName == null ? SqlString.Null : order.PurchaseOrderName;
            parameters[5].Value = order.CompanyID;
            parameters[6].Value = order.Applicant == null ? SqlString.Null : order.Applicant;
            parameters[7].Value = order.SubmitTime == DateTime.MinValue ? SqlDateTime.Null : order.SubmitTime;
            parameters[8].Value = order.Approvaling == null ? SqlString.Null : order.Approvaling;
            parameters[9].Value = order.Approvalers == null ? SqlString.Null : order.Approvalers;
            parameters[10].Value = order.Status;
            parameters[11].Value = order.Remark == null ? SqlString.Null : order.Remark;
            parameters[12].Value = order.SubOrderIndex;
            parameters[13].Value = order.DeliveryStatus;
            parameters[14].Value = order.PurchasingStatus;
            parameters[15].Value = order.PlanTotalAmount;

            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新采购单状态，不更新提交时间
        /// </summary>
        /// <param name="newStatus">新状态</param>
        void IPurchase.UpdatePurchaseOrderStatus(long id,PurchaseOrderStatus newStatus)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + TABLE_PURCHASE_ORDER + " set ");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("Status=@Status,");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.TinyInt,1)};
            parameters[0].Value = id;
            parameters[1].Value = DateTime.Now;
            parameters[2].Value = newStatus;
            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 获取指定公司某个状态下的所有申请单
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="status">申请单状态</param>
        /// <returns>申请单列表</returns>
        IList IPurchase.GetPurchaseOrdersByStatus(int pageIndex, int pageSize, out int recordCount, string companyid, PurchaseOrderStatus status)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = VIEW_PURCHASE_ORDER;
                qp.ReturnFields = string.Format(
                    VIEW_PURCHASE_ORDER + ".*"

                    + ",{0} as SubmitTimeSort", VIEW_PURCHASE_ORDER + ".UpdateTime");
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + VIEW_PURCHASE_ORDER + ".[CompanyID]='" + companyid + "' " +
                           " and " + VIEW_PURCHASE_ORDER + ".[Status]=" + status;
                qp.OrderBy = string.Format("order by {0} desc", "SubmitTimeSort");
                return SQLHelper.GetObjectList(this.GetDataOrder, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }

        /// <summary>
        /// 插入一条新的审批记录
        /// </summary>
        /// <param name="record">审批记录</param>
        void IPurchase.InsertApprovalRecord(PurchaseOrderApprovalInfo record)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + TABLE_PURCHASE_APPROVAL + "(");
            strSql.Append("OrderSn,CompanyID,PurchaseOrderID,SubOrderIndex,Approvaler,Result,FeeBack,ApprovalDate,ApprovalLog)");
            strSql.Append(" values (");
            strSql.Append("@OrderSn,@CompanyID,@PurchaseOrderID,@SubOrderIndex,@Approvaler,@Result,@FeeBack,@ApprovalDate,@ApprovalLog)");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderSn", SqlDbType.BigInt,8),
                    new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@PurchaseOrderID", SqlDbType.VarChar,20),
					new SqlParameter("@SubOrderIndex", SqlDbType.TinyInt,1),
					new SqlParameter("@Approvaler", SqlDbType.VarChar,20),
					new SqlParameter("@Result", SqlDbType.TinyInt),
					new SqlParameter("@FeeBack", SqlDbType.NVarChar,50),
					new SqlParameter("@ApprovalDate", SqlDbType.DateTime),
                                        new SqlParameter("@ApprovalLog", SqlDbType.NVarChar,500)};

            parameters[0].Value = record.OrderSn;
            parameters[1].Value = record.CompanyID;
            parameters[2].Value = record.PurchaseOrderID == null ? SqlString.Null : record.PurchaseOrderID;
            parameters[3].Value = record.SubOrderIndex;
            parameters[4].Value = record.Approvaler == null ? SqlString.Null : record.Approvaler;
            parameters[5].Value = record.Result;
            parameters[6].Value = record.FeeBack == null ? SqlString.Null : record.FeeBack;
            parameters[7].Value = record.ApprovalDate == DateTime.MinValue ? SqlDateTime.Null : record.ApprovalDate;
            parameters[8].Value = record.ApprovalLog == null ? SqlString.Null : record.ApprovalLog;

            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据公司，获取所有待采购以及正在采购的申请单
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns>进入采购流程的采购单</returns>
        IList IPurchase.GetPurchaseOrders2Purchase(int pageIndex, int pageSize, out int recordCount, string companyid)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = VIEW_PURCHASE_ORDER;
                qp.ReturnFields = string.Format(
                    VIEW_PURCHASE_ORDER + ".*"

                    + ",CAST(" + VIEW_PURCHASE_ORDER + ".Status - " + (int)PurchaseOrderStatus.WAITING4PURCHASE + " AS bit) as StatusSort1" //等待指派的排在前面
                    + ",CAST(" + VIEW_PURCHASE_ORDER + ".Status - " + (int)PurchaseOrderStatus.PURCHASING + " AS bit) as StatusSort2" //采购中的排在前面
                    + ",{0} as SubmitTimeSort", VIEW_PURCHASE_ORDER + ".UpdateTime");
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + VIEW_PURCHASE_ORDER + ".[CompanyID]='" + companyid + "'" //选择公司
                           + " and (" + VIEW_PURCHASE_ORDER + ".[Status]=" + (int)PurchaseOrderStatus.WAITING4PURCHASE//等待采购的
                           + " or " + VIEW_PURCHASE_ORDER + ".[Status]=" + (int)PurchaseOrderStatus.PURCHASING + ")";//正在采购的
                qp.OrderBy = string.Format("order by {0} asc, {1} asc, {2} desc", "StatusSort1", "StatusSort2", "SubmitTimeSort");
                return SQLHelper.GetObjectList(this.GetDataOrder, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }

        /// <summary>
        /// 根据公司，获取所有分派完毕的采购单
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns>完成采购流程的采购单</returns>
        IList IPurchase.GetPurchaseOrdersPurchaseFinish(int pageIndex, int pageSize, out int recordCount, string companyid)
        {
            try
            {
                QueryParam qp = new QueryParam();
                qp.TableName = VIEW_PURCHASE_ORDER;
                qp.ReturnFields = string.Format(
                    VIEW_PURCHASE_ORDER + ".*"

                    + ",CAST(" + VIEW_PURCHASE_ORDER + ".Status - " + (int)PurchaseOrderStatus.WAITING4PURCHASE + " AS bit) as StatusSort1" //等待指派的排在前面
                    + ",CAST(" + VIEW_PURCHASE_ORDER + ".Status - " + (int)PurchaseOrderStatus.PURCHASING + " AS bit) as StatusSort2" //采购中的排在前面
                    + ",{0} as SubmitTimeSort", VIEW_PURCHASE_ORDER + ".UpdateTime");
                qp.PageIndex = pageIndex;
                qp.PageSize = pageSize;
                qp.Where = "where " + VIEW_PURCHASE_ORDER + ".[CompanyID]='" + companyid + "'" //选择公司
                           + " and (" + VIEW_PURCHASE_ORDER + ".[Status]=" + (int)PurchaseOrderStatus.PURCHASINGFINISH//采购完成的，或者其他更高状态的
                           + " or " + VIEW_PURCHASE_ORDER + ".[Status]=" + (int)PurchaseOrderStatus.ACCEPTING           //验收中
                           + " or " + VIEW_PURCHASE_ORDER + ".[Status]=" + (int)PurchaseOrderStatus.ACCEPTINGFINISH       //验收完成
                           + " or " + VIEW_PURCHASE_ORDER + ".[Status]=" + (int)PurchaseOrderStatus.INWAREHOUSEING        //入库中
                           + " or " + VIEW_PURCHASE_ORDER + ".[Status]=" + (int)PurchaseOrderStatus.INWAREHOUSEFINISH + ")";//入库完毕

                qp.OrderBy = string.Format("order by {0} asc, {1} asc, {2} desc", "StatusSort1", "StatusSort2", "SubmitTimeSort");
                return SQLHelper.GetObjectList(this.GetDataOrder, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取消息分页失败", e);
            }
        }

        /// <summary>
        /// 更新采购项信息，主要用于采购中心分发、采购员采购、验收等操作。
        /// </summary>
        /// <param name="item">采购项</param>
        void IPurchase.UpdatePurchaseOrderDetail(PurchaseOrderDetailInfo item)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update "+ TABLE_PURCHASE_ORDER_DETAIL +" set ");
            strSql.Append("AdjustCount=@AdjustCount,");
            strSql.Append("FinalCount=@FinalCount,");
            strSql.Append("Unit=@Unit,");
            strSql.Append("Price=@Price,");
            strSql.Append("AdjustPrice=@AdjustPrice,");
            strSql.Append("FinalPrice=@FinalPrice,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("ActualCount=@ActualCount,");
            strSql.Append("ActualPrice=@ActualPrice,");
            strSql.Append("Purchaser=@Purchaser,");
            strSql.Append("PurchaseOrderID=@PurchaseOrderID,");
            strSql.Append("Checker_WK=@Checker_WK,");
            strSql.Append("Checker_Technician=@Checker_Technician,");
            strSql.Append("AcceptanceResult=@AcceptanceResult,");
            strSql.Append("AcceptedCount=@AcceptedCount,");
            strSql.Append("AcceptanceRemark=@AcceptanceRemark,");
            strSql.Append("SubOrderIndex=@SubOrderIndex,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("ProductName=@ProductName,");
            strSql.Append("Model=@Model,");
            strSql.Append("IsAsset=@IsAsset,");
            strSql.Append("PlanCount=@PlanCount,");
            strSql.Append("Status=@Status,");
            strSql.Append("PurchaseRemark=@PurchaseRemark,");
            strSql.Append("PurchaseTime=@PurchaseTime,");
            strSql.Append("WarehouseID=@WarehouseID,");
            strSql.Append("AcceptedTime=@AcceptedTime,");
            strSql.Append("Type=@Type,");
            strSql.Append("Producer=@Producer,");
            strSql.Append("Supplier=@Supplier,");
            strSql.Append("Divide=@Divide,");
            strSql.Append("SystemID=@SystemID");
            strSql.Append(" where ID=@ID and ItemID=@ItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@AdjustCount", SqlDbType.Decimal,9),
					new SqlParameter("@FinalCount", SqlDbType.Decimal,9),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@AdjustPrice", SqlDbType.Decimal,9),
					new SqlParameter("@FinalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
					new SqlParameter("@ActualCount", SqlDbType.Decimal,9),
					new SqlParameter("@ActualPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Purchaser", SqlDbType.VarChar,20),
					new SqlParameter("@PurchaseOrderID", SqlDbType.VarChar,20),
					new SqlParameter("@Checker_WK", SqlDbType.VarChar,20),
					new SqlParameter("@Checker_Technician", SqlDbType.VarChar,20),
					new SqlParameter("@AcceptanceResult", SqlDbType.TinyInt,1),
					new SqlParameter("@AcceptedCount", SqlDbType.Decimal,9),
					new SqlParameter("@AcceptanceRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@SubOrderIndex", SqlDbType.TinyInt,1),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@ItemID", SqlDbType.TinyInt,1),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@Model", SqlDbType.NVarChar,20),
					new SqlParameter("@IsAsset", SqlDbType.Bit,1),
					new SqlParameter("@PlanCount", SqlDbType.Decimal,9),
                    new SqlParameter("@Status", SqlDbType.TinyInt,1),
                    new SqlParameter("@PurchaseRemark", SqlDbType.NVarChar,50),
                    new SqlParameter("@PurchaseTime", SqlDbType.DateTime),
                    new SqlParameter("@WarehouseID",SqlDbType.VarChar,2),
                    new SqlParameter("@AcceptedTime",SqlDbType.DateTime),
                    new SqlParameter("@Type",SqlDbType.TinyInt),
                    new SqlParameter("@Producer",SqlDbType.NVarChar,50),
                    new SqlParameter("@Supplier",SqlDbType.NVarChar,50),
                    new SqlParameter("@Divide",SqlDbType.Bit),
                    new SqlParameter("@SystemID",SqlDbType.VarChar,2)};
            parameters[0].Value = item.ID;
            parameters[1].Value = item.AdjustCount;
            parameters[2].Value = item.FinalCount;
            parameters[3].Value = item.Unit == null ? SqlString.Null : item.Unit;
            parameters[4].Value = item.Price;
            parameters[5].Value = item.AdjustPrice;
            parameters[6].Value = item.FinalPrice;
            parameters[7].Value = item.Remark == null ? SqlString.Null : item.Remark;
            parameters[8].Value = item.ActualCount;
            parameters[9].Value = item.ActualPrice;
            parameters[10].Value = item.Purchaser == null ? SqlString.Null : item.Purchaser;
            parameters[11].Value = item.PurchaseOrderID == null ? SqlString.Null : item.PurchaseOrderID;
            parameters[12].Value = item.Checker_WK == null ? SqlString.Null : item.Checker_WK;
            parameters[13].Value = item.Checker_Technician == null ? SqlString.Null : item.Checker_Technician;
            parameters[14].Value = item.AcceptanceResult;
            parameters[15].Value = item.AcceptedCount;
            parameters[16].Value = item.AcceptanceRemark == null ? SqlString.Null : item.AcceptanceRemark;
            parameters[17].Value = item.SubOrderIndex;
            parameters[18].Value = item.CompanyID;
            parameters[19].Value = item.ItemID;
            parameters[20].Value = item.ProductName == null ? SqlString.Null : item.ProductName;
            parameters[21].Value = item.Model == null ? SqlString.Null : item.Model;
            parameters[22].Value = item.IsAsset;
            parameters[23].Value = item.PlanCount;
            parameters[24].Value = item.Status;
            parameters[25].Value = item.PurchaseRemark == null ? SqlString.Null : item.PurchaseRemark;
            parameters[26].Value = item.PurchaseTime==DateTime.MinValue ? SqlDateTime.Null : item.PurchaseTime;
            parameters[27].Value = item.WareHouseID == null ? SqlString.Null : item.WareHouseID;
            parameters[28].Value = item.AcceptedTime == DateTime.MinValue ? SqlDateTime.Null : item.AcceptedTime;
            parameters[29].Value = item.Type;
            parameters[30].Value = item.Producer == null ? SqlString.Null : item.Producer;
            parameters[31].Value = item.Supplier == null ? SqlString.Null : item.Supplier;
            parameters[32].Value = item.Divide;
            parameters[33].Value = string.IsNullOrEmpty(item.SystemID) ? SqlString.Null : item.SystemID;
            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获取一个详情实体
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>采购项</returns>
        PurchaseOrderDetailInfo IPurchase.GetPurchaseOrderDetailItem(long orderid,short itemid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from "+ VIEW_PURCHASE_ORDER_DETAIL+" ");
            strSql.Append(" where ID=@ID and ItemID=@ItemID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt),
                    new SqlParameter("@ItemID",SqlDbType.TinyInt)};
            parameters[0].Value = orderid;
            parameters[1].Value = itemid;

            PurchaseOrderDetailInfo item = new PurchaseOrderDetailInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    item = GetDataOrderDetail(rd);
                    break;
                }
            }
            
            return item;
        }


        #endregion

        #region 表的插入读取函数
        /// <summary>
        /// 插入采购单，第一次生成的采购单
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="order">申请单</param>
        /// <returns>生成的申请单ID</returns>
        private long InsertPurchaseOrder(SqlTransaction trans, PurchaseOrderInfo order)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + TABLE_PURCHASE_ORDER + "(");
            strSql.Append("NextOrderIndex,UpdateTime,PurchaseOrderID,PurchaseOrderName,CompanyID,Applicant,SubmitTime,Approvaling,Approvalers,Status,Remark,SubOrderIndex,DeliveryStatus,PurchasingStatus,PlanTotalAmount)");
            strSql.Append(" values (");
            strSql.Append("@NextOrderIndex,@UpdateTime,@PurchaseOrderID,@PurchaseOrderName,@CompanyID,@Applicant,@SubmitTime,@Approvaling,@Approvalers,@Status,@Remark,@SubOrderIndex,@DeliveryStatus,@PurchasingStatus,@PlanTotalAmount)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@NextOrderIndex", SqlDbType.TinyInt,1),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@PurchaseOrderID", SqlDbType.VarChar,20),
					new SqlParameter("@PurchaseOrderName", SqlDbType.NVarChar,30),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Applicant", SqlDbType.VarChar,20),
					new SqlParameter("@SubmitTime", SqlDbType.DateTime),
                    new SqlParameter("@Approvaling", SqlDbType.VarChar,20),
                    new SqlParameter("@Approvalers", SqlDbType.VarChar,200),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
					new SqlParameter("@SubOrderIndex", SqlDbType.TinyInt,1),
                               new SqlParameter("@DeliveryStatus", SqlDbType.TinyInt,1),
                               new SqlParameter("@PurchasingStatus", SqlDbType.TinyInt,1),
                               new SqlParameter("@PlanTotalAmount",SqlDbType.Decimal,9)
                                        };
            parameters[0].Value = 2;//新订单默认开始是2
            parameters[1].Value = order.UpdateTime == DateTime.MinValue ? SqlDateTime.Null : order.UpdateTime;
            parameters[2].Value = order.PurchaseOrderID;
            parameters[3].Value = order.PurchaseOrderName;
            parameters[4].Value = order.CompanyID;
            parameters[5].Value = order.Applicant;
            parameters[6].Value = order.SubmitTime == DateTime.MinValue ? SqlDateTime.Null : order.SubmitTime;
            parameters[7].Value = order.Approvaling == null ? SqlString.Null : order.Approvaling;
            parameters[8].Value = order.Approvalers == null ? SqlString.Null : order.Approvalers;
            parameters[9].Value = order.Status;
            parameters[10].Value = order.Remark == null ? SqlString.Null : order.Remark;
            parameters[11].Value = order.SubOrderIndex;//新订单默认开始是1
            parameters[12].Value = order.DeliveryStatus;
            parameters[13].Value = order.PurchasingStatus;
            parameters[14].Value = order.PlanTotalAmount;

            //读取ID
            long id = 1;
            using (SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rdr.Read())
                {
                    id = rdr.GetInt64(0);
                }
            }
            return id;
        }

        /// <summary>
        /// 插入采购单详情
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="order">申请单</param>
        /// <param name="id">申请单ID</param>
        private void InsertPurchaseOrderDetail(SqlTransaction trans, PurchaseOrderInfo order, long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + TABLE_PURCHASE_ORDER_DETAIL + "(");
            strSql.Append("ID,AdjustCount,FinalCount,Unit,Price,AdjustPrice,FinalPrice,Remark,ActualCount,ActualPrice,Purchaser,Checker_WK,PurchaseOrderID,Checker_Technician,AcceptanceResult,AcceptedCount,AcceptanceRemark,SubOrderIndex,CompanyID,ItemID,ProductName,Model,IsAsset,PlanCount,Status,PurchaseRemark,PurchaseTime,WarehouseID,AcceptedTime,Type,Producer,Supplier,Divide,SystemID)");
            strSql.Append(" values (");
            strSql.Append("@ID,@AdjustCount,@FinalCount,@Unit,@Price,@AdjustPrice,@FinalPrice,@Remark,@ActualCount,@ActualPrice,@Purchaser,@Checker_WK,@PurchaseOrderID,@Checker_Technician,@AcceptanceResult,@AcceptedCount,@AcceptanceRemark,@SubOrderIndex,@CompanyID,@ItemID,@ProductName,@Model,@IsAsset,@PlanCount,@Status,@PurchaseRemark,@PurchaseTime,@WarehouseID,@AcceptedTime,@Type,@Producer,@Supplier,@Divide,@SystemID)");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@AdjustCount", SqlDbType.Decimal,9),
					new SqlParameter("@FinalCount", SqlDbType.Decimal,9),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@AdjustPrice", SqlDbType.Decimal,9),
					new SqlParameter("@FinalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
					new SqlParameter("@ActualCount", SqlDbType.Decimal,9),
					new SqlParameter("@ActualPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@Purchaser", SqlDbType.VarChar,20),
					new SqlParameter("@Checker_WK", SqlDbType.VarChar,20),
					new SqlParameter("@PurchaseOrderID", SqlDbType.VarChar,20),
					new SqlParameter("@Checker_Technician", SqlDbType.VarChar,20),
					new SqlParameter("@AcceptanceResult", SqlDbType.TinyInt,1),
					new SqlParameter("@AcceptedCount", SqlDbType.Decimal,9),
					new SqlParameter("@AcceptanceRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@SubOrderIndex", SqlDbType.TinyInt,1),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@ItemID", SqlDbType.TinyInt,1),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@Model", SqlDbType.NVarChar,20),
					new SqlParameter("@IsAsset", SqlDbType.Bit,1),
					new SqlParameter("@PlanCount", SqlDbType.Decimal,9),
                    new SqlParameter("@Status", SqlDbType.TinyInt,1),
                    new SqlParameter("@PurchaseRemark",SqlDbType.NVarChar,50),
                    new SqlParameter("@PurchaseTime",SqlDbType.DateTime),
                    new SqlParameter("@WarehouseID",SqlDbType.VarChar,2),
                    new SqlParameter("@AcceptedTime",SqlDbType.DateTime),
                    new SqlParameter("@Type", SqlDbType.TinyInt,1),
                    new SqlParameter("@Producer",SqlDbType.NVarChar,50),
                    new SqlParameter("@Supplier",SqlDbType.NVarChar,50),
                    new SqlParameter("@Divide",SqlDbType.Bit),
                    new SqlParameter("@SystemID",SqlDbType.VarChar,2)};
            if (order.DetailList != null)
            {
                //针对每一个进行
                foreach (PurchaseOrderDetailInfo item in order.DetailList)
                {
                    parameters[0].Value = id;
                    parameters[1].Value = item.AdjustCount ;
                    parameters[2].Value = item.FinalCount ;
                    parameters[3].Value = item.Unit == null ? SqlString.Null : item.Unit;
                    parameters[4].Value = item.Price ;
                    parameters[5].Value = item.AdjustPrice;
                    parameters[6].Value = item.FinalPrice;
                    parameters[7].Value = item.Remark == null ? SqlString.Null : item.Remark;
                    parameters[8].Value = item.ActualCount;
                    parameters[9].Value = item.ActualPrice ;
                    parameters[10].Value = item.Purchaser == null ? SqlString.Null : item.Purchaser;

                    parameters[11].Value = item.Checker_WK == null ? SqlString.Null : item.Checker_WK;
                    parameters[12].Value = order.PurchaseOrderID;
                    parameters[13].Value = item.Checker_Technician == null ? SqlString.Null : item.Checker_Technician;
                    parameters[14].Value = item.AcceptanceResult;
                    parameters[15].Value = item.AcceptedCount;
                    parameters[16].Value = item.AcceptanceRemark == null ? SqlString.Null : item.AcceptanceRemark;
                    parameters[17].Value = order.SubOrderIndex;
                    parameters[18].Value = order.CompanyID;
                    parameters[19].Value = item.ItemID;
                    parameters[20].Value = item.ProductName == null ? SqlString.Null : item.ProductName;
                    parameters[21].Value = item.Model == null ? SqlString.Null : item.Model;
                    parameters[22].Value = item.IsAsset;
                    parameters[23].Value = item.PlanCount ;
                    parameters[24].Value = item.Status;
                    parameters[25].Value = item.PurchaseRemark == null ? SqlString.Null : item.PurchaseRemark;
                    parameters[26].Value = item.PurchaseTime==DateTime.MinValue ? SqlDateTime.Null : item.PurchaseTime;
                    parameters[27].Value = item.WareHouseID == null ? SqlString.Null : item.WareHouseID;
                    parameters[28].Value = item.AcceptedTime == DateTime.MinValue ? SqlDateTime.Null : item.AcceptedTime;
                    parameters[29].Value = item.Type;
                    parameters[30].Value = item.Producer == null ? SqlString.Null : item.Producer;
                    parameters[31].Value = item.Supplier == null ? SqlString.Null : item.Supplier;
                    parameters[32].Value = item.Divide;
                    parameters[33].Value = string.IsNullOrEmpty(item.SystemID)? SqlString.Null : item.SystemID;
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                }
            }

        }



        /// <summary>
        /// 获取采购单实体，不含详情
        /// </summary>
        /// <param name="rd">reader</param>
        /// <returns>采购单实体</returns>
        private PurchaseOrderInfo GetDataOrder(IDataReader rd)
        {
            PurchaseOrderInfo order = new PurchaseOrderInfo();
            if (!Convert.IsDBNull(rd["ID"]))
            {
                order.ID = Convert.ToInt64(rd["ID"]);
            }

            try
            {
                if (!Convert.IsDBNull(rd["CompanyName"]))
                {
                    order.CompanyName = Convert.ToString(rd["CompanyName"]);
                }
            }
            catch
            {
            }

            if (!Convert.IsDBNull(rd["NextOrderIndex"]))
            {
                order.NextOrderIndex = Convert.ToInt16(rd["NextOrderIndex"]);
            }
            if (!Convert.IsDBNull(rd["UpdateTime"]))
            {
                order.UpdateTime = Convert.ToDateTime(rd["UpdateTime"]);
            }
            if (!Convert.IsDBNull(rd["PurchaseOrderID"]))
            {
                order.PurchaseOrderID = Convert.ToString(rd["PurchaseOrderID"]);
            }
            if (!Convert.IsDBNull(rd["PurchaseOrderName"]))
            {
                order.PurchaseOrderName = Convert.ToString(rd["PurchaseOrderName"]);
            }
            if (!Convert.IsDBNull(rd["CompanyID"]))
            {
                order.CompanyID = Convert.ToString(rd["CompanyID"]);
            }
            if (!Convert.IsDBNull(rd["Applicant"]))
            {
                order.Applicant = Convert.ToString(rd["Applicant"]);
            }
            if (!Convert.IsDBNull(rd["SubmitTime"]))
            {
                order.SubmitTime = Convert.ToDateTime(rd["SubmitTime"]);
            }
            if (!Convert.IsDBNull(rd["Approvaling"]))
            {
                order.Approvaling = Convert.ToString(rd["Approvaling"]);
            }

            if (!Convert.IsDBNull(rd["Approvalers"]))
            {
                order.Approvalers = Convert.ToString(rd["Approvalers"]);
            }

            if (!Convert.IsDBNull(rd["Status"]))
            {
                order.Status = (PurchaseOrderStatus)Enum.Parse(
                        typeof(PurchaseOrderStatus),
                        Convert.ToString(Convert.ToInt16(rd["Status"])),
                        true
                        );
            }

            if (!Convert.IsDBNull(rd["DeliveryStatus"]))
            {
                order.DeliveryStatus = (PurchaseOrderDeliveryStatus)Enum.Parse(
                        typeof(PurchaseOrderDeliveryStatus),
                        Convert.ToString(Convert.ToInt16(rd["DeliveryStatus"])),
                        true
                        );
            }

            if (!Convert.IsDBNull(rd["PurchasingStatus"]))
            {
                order.PurchasingStatus = (PurchasingStatus)Enum.Parse(
                        typeof(PurchasingStatus),
                        Convert.ToString(Convert.ToInt16(rd["PurchasingStatus"])),
                        true
                        );
            }

            if (!Convert.IsDBNull(rd["Remark"]))
            {
                order.Remark = Convert.ToString(rd["Remark"]);
            }

            if (!Convert.IsDBNull(rd["SubOrderIndex"]))
            {
                order.SubOrderIndex = Convert.ToInt16(rd["SubOrderIndex"]);
            }
            if (!Convert.IsDBNull(rd["PlanTotalAmount"]))
            {
                order.PlanTotalAmount = Convert.ToDecimal(rd["PlanTotalAmount"]);
            }

            if (!Convert.IsDBNull(rd["Applicant"]))
            {
                order.Applicant = Convert.ToString(rd["Applicant"]);
            }
            if (!Convert.IsDBNull(rd["ApplicantName"]))
            {
                order.ApplicantName = Convert.ToString(rd["ApplicantName"]);
            }
            if (!Convert.IsDBNull(rd["ApplicantPositionName"]))
            {
                order.ApplicantPositionName = Convert.ToString(rd["ApplicantPositionName"]);
            }
            if (!Convert.IsDBNull(rd["ApplicantDepartmentID"]))
            {
                order.ApplicantDepartmentID = Convert.ToInt64(rd["ApplicantDepartmentID"]);
            }
            if (!Convert.IsDBNull(rd["ApplicantDepartmentName"]))
            {
                order.ApplicantDepartmentName = Convert.ToString(rd["ApplicantDepartmentName"]);
            }


            if (!Convert.IsDBNull(rd["NextUserName"]))
            {
                order.NextUserName = Convert.ToString(rd["NextUserName"]);
            }
            if (!Convert.IsDBNull(rd["NextUserPersonName"]))
            {
                order.NextUserPersonName = Convert.ToString(rd["NextUserPersonName"]);
            }
            if (!Convert.IsDBNull(rd["NextUserPositionName"]))
            {
                order.NextUserPositionName = Convert.ToString(rd["NextUserPositionName"]);
            }
            if (!Convert.IsDBNull(rd["NextUserDepartmentID"]))
            {
                order.NextUserDepartmentID = Convert.ToInt64(rd["NextUserDepartmentID"]);
            }
            if (!Convert.IsDBNull(rd["NextUserDepartmentName"]))
            {
                order.NextUserDepartmentName = Convert.ToString(rd["NextUserDepartmentName"]);
            }

            if (!Convert.IsDBNull(rd["DelegateUserName"]))
            {
                order.DelegateUserName = Convert.ToString(rd["DelegateUserName"]);
            }
            if (!Convert.IsDBNull(rd["DelegateUserPersonName"]))
            {
                order.DelegateUserPersonName = Convert.ToString(rd["DelegateUserPersonName"]);
            }
            if (!Convert.IsDBNull(rd["DelegateUserPositionName"]))
            {
                order.DelegateUserPositionName = Convert.ToString(rd["DelegateUserPositionName"]);
            }
            if (!Convert.IsDBNull(rd["DelegateUserDepartmentID"]))
            {
                order.DelegateUserDepartmentID = Convert.ToInt64(rd["DelegateUserDepartmentID"]);
            }
            if (!Convert.IsDBNull(rd["DelegateUserDepartmentName"]))
            {
                order.DelegateUserDepartmentName = Convert.ToString(rd["DelegateUserDepartmentName"]);
            }

            if (!Convert.IsDBNull(rd["InstanceID"]))
            {
                order.WorkFlowInstanceID = Convert.ToString(rd["InstanceID"]);
            }
            if (!Convert.IsDBNull(rd["StatusDescription"]))
            {
                order.WorkFlowStateDescription = Convert.ToString(rd["StatusDescription"]);
            }
            if (!Convert.IsDBNull(rd["CurrentStateName"]))
            {
                order.WorkFlowStateName = Convert.ToString(rd["CurrentStateName"]);
            }

            return order;
        }

        /// <summary>
        /// 获取采购单详情实体
        /// </summary>
        /// <param name="rd">reader</param>
        /// <returns>详情实体</returns>
        private PurchaseOrderDetailInfo GetDataOrderDetail(IDataReader rd)
        {
            PurchaseOrderDetailInfo item = new PurchaseOrderDetailInfo();
            if (!Convert.IsDBNull(rd["ID"]))
            {
                item.ID = Convert.ToInt64(rd["ID"]);
            }

            if (!Convert.IsDBNull(rd["AdjustCount"]))
            {
                item.AdjustCount = Convert.ToDecimal(rd["AdjustCount"]);
            }

            if (!Convert.IsDBNull(rd["FinalCount"]))
            {
                item.FinalCount = Convert.ToDecimal(rd["FinalCount"]);
            }
            if (!Convert.IsDBNull(rd["Unit"]))
            {
                item.Unit = Convert.ToString(rd["Unit"]);
            }

            if (!Convert.IsDBNull(rd["Price"]))
            {
                item.Price = Convert.ToDecimal(rd["Price"]);
            }

            if (!Convert.IsDBNull(rd["AdjustPrice"]))
            {
                item.AdjustPrice = Convert.ToDecimal(rd["AdjustPrice"]);
            }

            if (!Convert.IsDBNull(rd["FinalPrice"]))
            {
                item.FinalPrice = Convert.ToDecimal(rd["FinalPrice"]);
            }

            if (!Convert.IsDBNull(rd["Remark"]))
            {
                item.Remark = Convert.ToString(rd["Remark"]);
            }

            if (!Convert.IsDBNull(rd["PurchaseRemark"]))
            {
                item.PurchaseRemark = Convert.ToString(rd["PurchaseRemark"]);
            }

            if (!Convert.IsDBNull(rd["ActualCount"]))
            {
                //item.ActualCount = Convert.ToDecimal(rd["ActualCount"]);
            }

            if (!Convert.IsDBNull(rd["ActualPrice"]))
            {
                //item.ActualPrice = Convert.ToDecimal(rd["ActualPrice"]);
            }

            if (!Convert.IsDBNull(rd["Purchaser"]))
            {
                item.Purchaser = Convert.ToString(rd["Purchaser"]);
            }

            if (!Convert.IsDBNull(rd["Checker_WK"]))
            {
                item.Checker_WK = Convert.ToString(rd["Checker_WK"]);
            }
            if (!Convert.IsDBNull(rd["PurchaseOrderID"]))
            {
                item.PurchaseOrderID = Convert.ToString(rd["PurchaseOrderID"]);
            }
            if (!Convert.IsDBNull(rd["Checker_Technician"]))
            {
                item.Checker_Technician = Convert.ToString(rd["Checker_Technician"]);
            }

            if (!Convert.IsDBNull(rd["AcceptanceResult"]))
            {
                item.AcceptanceResult =
                    (PurchaseOrderDetailAcceptanceResult)Enum.Parse(
                        typeof(PurchaseOrderDetailAcceptanceResult),
                        Convert.ToString(Convert.ToInt16(rd["AcceptanceResult"])),
                        true
                        );
            }

            if (!Convert.IsDBNull(rd["AcceptedCount"]))
            {
                //item.AcceptedCount = Convert.ToDecimal(rd["AcceptedCount"]);
            }
            if (!Convert.IsDBNull(rd["AcceptanceRemark"]))
            {
                item.AcceptanceRemark = Convert.ToString(rd["AcceptanceRemark"]);
            }

            if (!Convert.IsDBNull(rd["SubOrderIndex"]))
            {
                item.SubOrderIndex = Convert.ToInt16(rd["SubOrderIndex"]);
            }

            if (!Convert.IsDBNull(rd["CompanyID"]))
            {
                item.CompanyID = Convert.ToString(rd["CompanyID"]);
            }

            if (!Convert.IsDBNull(rd["ItemID"]))
            {
                item.ItemID = Convert.ToInt16(rd["ItemID"]);
            }

            if (!Convert.IsDBNull(rd["ProductName"]))
            {
                item.ProductName = Convert.ToString(rd["ProductName"]);
            }
            if (!Convert.IsDBNull(rd["Model"]))
            {
                item.Model = Convert.ToString(rd["Model"]);
            }

            if (!Convert.IsDBNull(rd["IsAsset"]))
            {

                item.IsAsset = Convert.ToBoolean(rd["IsAsset"]);

            }

            if (!Convert.IsDBNull(rd["PlanCount"]))
            {
                item.PlanCount = Convert.ToDecimal(rd["PlanCount"]);
            }


            if (!Convert.IsDBNull(rd["PurchaseTime"]))
            {
                item.PurchaseTime = Convert.ToDateTime(rd["PurchaseTime"]);
            }


            if (!Convert.IsDBNull(rd["AcceptedTime"]))
            {
                item.AcceptedTime = Convert.ToDateTime(rd["AcceptedTime"]);
            }


            if (!Convert.IsDBNull(rd["Status"]))
            {
                item.Status =
                    (PurchaseOrderDetailStatus)Enum.Parse(
                        typeof(PurchaseOrderDetailStatus),
                        Convert.ToString(Convert.ToInt16(rd["Status"])),
                        true
                        );
            }
            if (!Convert.IsDBNull(rd["WarehouseID"]))
            {
                item.WareHouseID = Convert.ToString(rd["WarehouseID"]);
            }
            if (!Convert.IsDBNull(rd["Type"]))
            {
                item.Type =
                    (ItemType)Enum.Parse(
                        typeof(ItemType),
                        Convert.ToString(Convert.ToInt16(rd["Type"])),
                        true
                        );
            }
            if (!Convert.IsDBNull(rd["Producer"]))
            {
                item.Producer = Convert.ToString(rd["Producer"]);
            }

            if (!Convert.IsDBNull(rd["Supplier"]))
            {
                item.Supplier = Convert.ToString(rd["Supplier"]);
            }

            if (!Convert.IsDBNull(rd["Divide"]))
            {
                item.Divide = Convert.ToBoolean(rd["Divide"]);
            }

            if (!Convert.IsDBNull(rd["SystemID"]))
            {
                item.SystemID = Convert.ToString(rd["SystemID"]);
            }
            if (!Convert.IsDBNull(rd["SystemName"]))
            {
                item.SystemName = Convert.ToString(rd["SystemName"]);
            }
            return item;
        }

        /// <summary>
        /// 获取审批记录
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private PurchaseOrderApprovalInfo GetDataApproval(IDataReader rd)
        {
            PurchaseOrderApprovalInfo record = new PurchaseOrderApprovalInfo();
            if (!Convert.IsDBNull(rd["ID"]))
            {
                record.ID = Convert.ToInt64(rd["ID"]);
            }

            if (!Convert.IsDBNull(rd["OrderSn"]))
            {
                record.OrderSn = Convert.ToInt64(rd["OrderSn"]);
            }

            if (!Convert.IsDBNull(rd["CompanyID"]))
            {
                record.CompanyID = Convert.ToString(rd["CompanyID"]);
            }

            if (!Convert.IsDBNull(rd["PurchaseOrderID"]))
            {
                record.PurchaseOrderID = Convert.ToString(rd["PurchaseOrderID"]);
            }

            if (!Convert.IsDBNull(rd["Approvaler"]))
            {
                record.Approvaler = Convert.ToString(rd["Approvaler"]);
            }

            if (!Convert.IsDBNull(rd["FeeBack"]))
            {
                record.FeeBack = Convert.ToString(rd["FeeBack"]);
            }

            if (!Convert.IsDBNull(rd["SubOrderIndex"]))
            {
                record.SubOrderIndex = Convert.ToInt16(rd["SubOrderIndex"]);
            }

            if (!Convert.IsDBNull(rd["Result"]))
            {
                record.Result = (PurchaseOrderApprovalResult)Enum.Parse(typeof(PurchaseOrderApprovalResult),
                    Convert.ToString(rd["Result"]), true);
            }

            if (!Convert.IsDBNull(rd["ApprovalDate"]))
            {
                record.ApprovalDate = Convert.ToDateTime(rd["ApprovalDate"]);
            }

            if (!Convert.IsDBNull(rd["ApprovalLog"]))
            {
                record.ApprovalLog = Convert.ToString(rd["ApprovalLog"]);
            }
            if (!Convert.IsDBNull(rd["ApprovalerName"]))
            {
                record.ApprovalerName = Convert.ToString(rd["ApprovalerName"]);
            }

            return record;
        }


        /// <summary>
        /// 获取修改记录记录
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private PurchaseOrderModifyInfo GetDataModifyRecord(IDataReader rd)
        {
            PurchaseOrderModifyInfo record = new PurchaseOrderModifyInfo();
            if (!Convert.IsDBNull(rd["ID"]))
            {
                record.ID = Convert.ToInt64(rd["ID"]);
            }

            if (!Convert.IsDBNull(rd["OrderSn"]))
            {
                record.OrderSn = Convert.ToInt64(rd["OrderSn"]);
            }

            if (!Convert.IsDBNull(rd["CompanyID"]))
            {
                record.CompanyID = Convert.ToString(rd["CompanyID"]);
            }

            if (!Convert.IsDBNull(rd["PurchaseOrderID"]))
            {
                record.PurchaseOrderID = Convert.ToString(rd["PurchaseOrderID"]);
            }

            if (!Convert.IsDBNull(rd["Modifier"]))
            {
                record.Modifier = Convert.ToString(rd["Modifier"]);
            }

            if(!Convert.IsDBNull(rd["ModifierName"]))
            {
                record.ModifierName = Convert.ToString(rd["ModifierName"]);
            }

            if (!Convert.IsDBNull(rd["SaveContent"]))
            {
                record.Content = Convert.ToString(rd["SaveContent"]);
            }

            if (!Convert.IsDBNull(rd["SubOrderIndex"]))
            {
                record.SubOrderIndex = Convert.ToInt16(rd["SubOrderIndex"]);
            }

            if (!Convert.IsDBNull(rd["ModifyType"]))
            {
                record.ModifyType = (PurchaseOrderModifyType)Enum.Parse(typeof(PurchaseOrderModifyType),
                    Convert.ToString(rd["ModifyType"]), true);
            }

            if (!Convert.IsDBNull(rd["ModifyTime"]))
            {
                record.ModifyTime = Convert.ToDateTime(rd["ModifyTime"]);
            }



            return record;
        }


        /// <summary>
        /// 获取采购单详情列表
        /// </summary>
        /// <param name="orderid">采购单ID</param>
        /// <returns>采购单详情列表</returns>
        private List<PurchaseOrderDetailInfo> GetOrderDetailList(long orderid)
        {
            //获取采购单详情
            List<PurchaseOrderDetailInfo> list = new List<PurchaseOrderDetailInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + VIEW_PURCHASE_ORDER_DETAIL + " ");
            strSql.Append(" where ID=@ID ");
            strSql.Append(" order by ItemID ;");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = orderid;
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    PurchaseOrderDetailInfo item = GetDataOrderDetail(rd);
                    list.Add(item);
                }
            }
            foreach (PurchaseOrderDetailInfo item in list)
            {
                item.PurchaseRecordList = GetPurchaseRecordInfoList(item.ID, item.ItemID);
            }
            return list;
        }


        /// <summary>
        /// 获取采购单审批记录列表
        /// </summary>
        /// <param name="orderid">采购单ID</param>
        /// <returns>采购单审批记录</returns>
        private List<PurchaseOrderApprovalInfo> GetOrderApprovalList(long orderid)
        {
            //获取采购单详情
            List<PurchaseOrderApprovalInfo> list = new List<PurchaseOrderApprovalInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + VIEW_PURCHASE_APPROVAL_VIEW + " ");
            strSql.Append(" where OrderSn=@OrderSn ");
            strSql.Append(" order by ApprovalDate DESC;");//倒序排序
            SqlParameter[] parameters = {
					new SqlParameter("@OrderSn", SqlDbType.BigInt)};
            parameters[0].Value = orderid;
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    PurchaseOrderApprovalInfo item = GetDataApproval(rd);
                    list.Add(item);
                }
            }
            return list;
        }


        /// <summary>
        /// 获取采购单修改记录列表
        /// </summary>
        /// <param name="orderid">采购单ID</param>
        /// <returns>采购单审批记录</returns>
        private List<PurchaseOrderModifyInfo> GetOrderModifyList(long orderid)
        {
            //获取采购单详情
            List<PurchaseOrderModifyInfo> list = new List<PurchaseOrderModifyInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + VIEW_PURCHASE_MODIFY_VIEW + " ");
            strSql.Append(" where OrderSn=@OrderSn ");
            strSql.Append(" order by ModifyTime DESC;");//倒序排序
            SqlParameter[] parameters = {
					new SqlParameter("@OrderSn", SqlDbType.BigInt)};
            parameters[0].Value = orderid;
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    PurchaseOrderModifyInfo item = GetDataModifyRecord(rd);
                    list.Add(item);
                }
            }
            if (list.Count > 0)
            {
                list.RemoveAt(0);//最近的一条记录可以去除，因为order的详情里面跟这条记录是一致的
            }
            return list;
        }

        /// <summary>
        /// 更新采购单的信息，一般是编辑草稿，采购单总表
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="order">新的采购单MODEL</param>
        void UpdatePurchaseOrder(SqlTransaction trans, PurchaseOrderInfo order)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + TABLE_PURCHASE_ORDER + " set ");
            strSql.Append("NextOrderIndex=@NextOrderIndex,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("PurchaseOrderID=@PurchaseOrderID,");
            strSql.Append("PurchaseOrderName=@PurchaseOrderName,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("Applicant=@Applicant,");
            strSql.Append("SubmitTime=@SubmitTime,");
            strSql.Append("Approvaling=@Approvaling,");
            strSql.Append("Approvalers=@Approvalers,");
            strSql.Append("Status=@Status,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("SubOrderIndex=@SubOrderIndex,");
            strSql.Append("DeliveryStatus=@DeliveryStatus,");
            strSql.Append("PurchasingStatus=@PurchasingStatus,");
            strSql.Append("PlanTotalAmount=@PlanTotalAmount");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@NextOrderIndex", SqlDbType.TinyInt,1),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@PurchaseOrderID", SqlDbType.VarChar,20),
					new SqlParameter("@PurchaseOrderName", SqlDbType.NVarChar,30),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@Applicant", SqlDbType.VarChar,20),
					new SqlParameter("@SubmitTime", SqlDbType.DateTime),
                    new SqlParameter("@Approvaling", SqlDbType.VarChar,20),
                    new SqlParameter("@Approvalers", SqlDbType.VarChar,200),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
					new SqlParameter("@SubOrderIndex", SqlDbType.TinyInt,1),
                    new SqlParameter("@DeliveryStatus", SqlDbType.TinyInt,1),
                    new SqlParameter("@PurchasingStatus", SqlDbType.TinyInt,1),
                    new SqlParameter("@PlanTotalAmount",SqlDbType.Decimal,9)};
            parameters[0].Value = order.ID;
            parameters[1].Value = order.NextOrderIndex;
            parameters[2].Value = DateTime.Now;
            parameters[3].Value = order.PurchaseOrderID;
            parameters[4].Value = order.PurchaseOrderName == null ? SqlString.Null : order.PurchaseOrderName;
            parameters[5].Value = order.CompanyID;
            parameters[6].Value = order.Applicant == null ? SqlString.Null : order.Applicant;
            parameters[7].Value = order.SubmitTime == DateTime.MinValue ? SqlDateTime.Null : order.SubmitTime;
            parameters[8].Value = order.Approvaling == null ? SqlString.Null : order.Approvaling;
            parameters[9].Value = order.Approvalers == null ? SqlString.Null : order.Approvalers;
            parameters[10].Value = order.Status;
            parameters[11].Value = order.Remark == null ? SqlString.Null : order.Remark;
            parameters[12].Value = order.SubOrderIndex;
            parameters[13].Value = order.DeliveryStatus;
            parameters[14].Value = order.PurchasingStatus;
            parameters[15].Value = order.PlanTotalAmount;
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新详情
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="order">新的采购单MODEL,含有详情LIST</param>
        void UpdatePurchaseOrderDetail(SqlTransaction trans, PurchaseOrderInfo order)
        {
            //先删除
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete "+ TABLE_PURCHASE_ORDER_DETAIL +" ");
            strSql.Append(" where ID=@ID ;");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt),};
            parameters[0].Value = order.ID;

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

            //然后再插入
            InsertPurchaseOrderDetail(trans, order, order.ID);
        }

        /// <summary>
        /// 插入一条新的修改记录
        /// </summary>
        /// <param name="record">修改记录</param>
        private void InsertModifyRecord(SqlTransaction trans, PurchaseOrderInfo order, PurchaseOrderModifyInfo record)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + TABLE_PURCHASE_ORDER_MODIFTY + "(");
            strSql.Append("OrderSn,CompanyID,PurchaseOrderID,SubOrderIndex,Modifier,ModifyType,SaveContent,ModifyTime)");
            strSql.Append(" values (");
            strSql.Append("@OrderSn,@CompanyID,@PurchaseOrderID,@SubOrderIndex,@Modifier,@ModifyType,@SaveContent,@ModifyTime)");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderSn", SqlDbType.BigInt,8),
                    new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@PurchaseOrderID", SqlDbType.VarChar,20),
					new SqlParameter("@SubOrderIndex", SqlDbType.TinyInt,1),
					new SqlParameter("@Modifier", SqlDbType.VarChar,20),
					new SqlParameter("@ModifyType", SqlDbType.TinyInt),
					new SqlParameter("@SaveContent", SqlDbType.NVarChar,500),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime)};

            parameters[0].Value = order.ID;
            parameters[1].Value = order.CompanyID;
            parameters[2].Value = order.PurchaseOrderID == null ? SqlString.Null : order.PurchaseOrderID;
            parameters[3].Value = order.SubOrderIndex;
            parameters[4].Value = record.Modifier == null ? SqlString.Null : record.Modifier;
            parameters[5].Value = record.ModifyType;
            parameters[6].Value = record.Content == null ? SqlString.Null : record.Content;
            parameters[7].Value = record.ModifyTime == DateTime.MinValue ? SqlDateTime.Null : record.ModifyTime;

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取相关的采购单，orderid=orderid，但不包括自己，即suborderindex!=suborderindex
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="suborderindex"></param>
        /// <returns></returns>
        private IList GetRelatedOrders(string orderid, short suborderindex)
        {
            //获取采购单详情
            List<PurchaseOrderInfo> list = new List<PurchaseOrderInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + VIEW_PURCHASE_ORDER + " ");
            strSql.Append(" where PurchaseOrderID=@PurchaseOrderID ");
            strSql.Append(" and SubOrderIndex<>@SubOrderIndex ");
            strSql.Append(" and Status<>" + (int)PurchaseOrderStatus.DRAFT);//不是草稿
            strSql.Append(" order by ID DESC;");//倒序排序
            SqlParameter[] parameters = {
					new SqlParameter("@PurchaseOrderID", SqlDbType.VarChar,20),
                    new SqlParameter("@SubOrderIndex",SqlDbType.TinyInt)};
            parameters[0].Value = orderid;
            parameters[1].Value = suborderindex;
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    PurchaseOrderInfo item = GetDataOrder(rd);
                    list.Add(item);
                }
            }
            return list;
        }


        /// <summary>
        /// 获取一个采购记录实体对象
        /// </summary>
        private PurchaseRecordInfo GetDataPurchaseRecordInfo(IDataReader rd)
        {
            PurchaseRecordInfo item = new PurchaseRecordInfo();
            if (!Convert.IsDBNull(rd["ID"]))
            {
                item.ID = Convert.ToInt64(rd["ID"]);
            }
            if (!Convert.IsDBNull(rd["OrderID"]))
            {
                item.OrderID = Convert.ToInt64(rd["OrderID"]);
            }
            if (!Convert.IsDBNull(rd["PlanDetailItemID"]))
            {
                item.PlanDetailItemID = Convert.ToInt16(rd["PlanDetailItemID"]);
            }
            if (!Convert.IsDBNull(rd["CompanyID"]))
            {
                item.CompanyID = Convert.ToString(rd["CompanyID"]);
            }
            if (!Convert.IsDBNull(rd["PurchaseOrderID"]))
            {
                item.PurchaseOrderID = Convert.ToString(rd["PurchaseOrderID"]);
            }
            if (!Convert.IsDBNull(rd["SubOrderIndex"]))
            {
                item.SubOrderIndex = Convert.ToInt16(rd["SubOrderIndex"]);
            }
            if (!Convert.IsDBNull(rd["ProductName"]))
            {
                item.ProductName = Convert.ToString(rd["ProductName"]);
            }
            if (!Convert.IsDBNull(rd["Model"]))
            {
                item.Model = Convert.ToString(rd["Model"]);
            }

            if (!Convert.IsDBNull(rd["Type"]))
            {
                item.Type = (ItemType)Enum.Parse(typeof(ItemType), Convert.ToInt16(rd["Type"]).ToString());
            }

            if (!Convert.IsDBNull(rd["PurchaseCount"]))
            {
                item.PurchaseCount = Convert.ToDecimal(rd["PurchaseCount"]);
            }
            if (!Convert.IsDBNull(rd["PurchaseUnitPrice"]))
            {
                item.PurchaseUnitPrice = Convert.ToDecimal(rd["PurchaseUnitPrice"]);
            }
            if (!Convert.IsDBNull(rd["Unit"]))
            {
                item.Unit = Convert.ToString(rd["Unit"]);
            }

            if (!Convert.IsDBNull(rd["Producer"]))
            {
                item.Producer = Convert.ToString(rd["Producer"]);
            }
            if (!Convert.IsDBNull(rd["Supplier"]))
            {
                item.Supplier = Convert.ToString(rd["Supplier"]);
            }
            if (!Convert.IsDBNull(rd["PurchaseRemark"]))
            {
                item.PurchaseRemark = Convert.ToString(rd["PurchaseRemark"]);
            }

            if (!Convert.IsDBNull(rd["PurchaseTime"]))
            {
                item.PurchaseTime = Convert.ToDateTime(rd["PurchaseTime"]);
            }
            if (!Convert.IsDBNull(rd["Purchaser"]))
            {
                item.Purchaser = Convert.ToString(rd["Purchaser"]);
            }
            if (!Convert.IsDBNull(rd["PurchaserConfirm"]))
            {
                item.PurchaserConfirm = Convert.ToBoolean(rd["PurchaserConfirm"]);
            }

            if (!Convert.IsDBNull(rd["PurchaserConfirmTime"]))
            {
                item.PurchaserConfirmTime =  Convert.ToDateTime(rd["PurchaserConfirmTime"]);
            }

            if (!Convert.IsDBNull(rd["WarehouseID"]))
            {
                item.WarehouseID = Convert.ToString(rd["WarehouseID"]);
            }

            if (!Convert.IsDBNull(rd["WarehouseName"]))
            {
                item.WarehouseName = Convert.ToString(rd["WarehouseName"]);
            }

            if (!Convert.IsDBNull(rd["Checker_Warehouse"]))
            {
                item.Checker_Warehouse = Convert.ToString(rd["Checker_Warehouse"]);
            }

            if (!Convert.IsDBNull(rd["WarehouseKeeperName"]))
            {
                item.WarehouseKeeperName = Convert.ToString(rd["WarehouseKeeperName"]);
            }

            if (!Convert.IsDBNull(rd["Checker_Technician"]))
            {
                item.Checker_Technician = Convert.ToString(rd["Checker_Technician"]);
            }
            if (!Convert.IsDBNull(rd["PurchaserName"]))
            {
                item.PurchaserName = Convert.ToString(rd["PurchaserName"]);
            }
            if (!Convert.IsDBNull(rd["TechnicianName"]))
            {
                item.TechnicianName = Convert.ToString(rd["TechnicianName"]);
            }

            if (!Convert.IsDBNull(rd["AcceptanceCount"]))
            {
                item.AcceptanceCount = Convert.ToDecimal(rd["AcceptanceCount"]);
            }
            if (!Convert.IsDBNull(rd["AcceptanceResult"]))
            {
                item.AcceptanceResult = (PurchaseOrderDetailAcceptanceResult)Enum.Parse(typeof(PurchaseOrderDetailAcceptanceResult), Convert.ToInt16(rd["AcceptanceResult"]).ToString());
            }
            if (!Convert.IsDBNull(rd["AcceptanceRemark"]))
            {
                item.AcceptanceRemark = Convert.ToString(rd["AcceptanceRemark"]);
            }

            if (!Convert.IsDBNull(rd["AcceptedTime"]))
            {
                item.AcceptedTime = Convert.ToDateTime(rd["AcceptedTime"]);
            }
            if (!Convert.IsDBNull(rd["Status"]))
            {
                item.Status = (PurchaseRecordStatus)Enum.Parse(typeof(PurchaseRecordStatus), Convert.ToInt16(rd["Status"]).ToString());
            }
            if (!Convert.IsDBNull(rd["IsDividable"]))
            {
                item.IsDividable = Convert.ToBoolean(rd["IsDividable"]);
            }
            if (!Convert.IsDBNull(rd["SystemID"]))
            {
                item.SystemID = Convert.ToString(rd["SystemID"]);
            }

            return item;
        }

        /// <summary>
        /// 插入表TABLE_PURCHASE_RECORD记录
        /// </summary>
        private long InsertPurchaseRecordInfo(SqlTransaction trans, PurchaseRecordInfo model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into "+TABLE_PURCHASE_RECORD+"(");
            strSql.Append("PurchaseCount,PurchaseUnitPrice,Unit,Producer,Supplier,PurchaseRemark,PurchaseTime,Purchaser,PurchaserConfirm,PurchaserConfirmTime,OrderID,WarehouseID,Checker_Warehouse,Checker_Technician,AcceptanceCount,AcceptanceResult,AcceptanceRemark,AcceptedTime,Status,IsDividable,PlanDetailItemID,CompanyID,PurchaseOrderID,SubOrderIndex,ProductName,Model,Type,SystemID)");
            strSql.Append(" values (");
            strSql.Append("@PurchaseCount,@PurchaseUnitPrice,@Unit,@Producer,@Supplier,@PurchaseRemark,@PurchaseTime,@Purchaser,@PurchaserConfirm,@PurchaserConfirmTime,@OrderID,@WarehouseID,@Checker_Warehouse,@Checker_Technician,@AcceptanceCount,@AcceptanceResult,@AcceptanceRemark,@AcceptedTime,@Status,@IsDividable,@PlanDetailItemID,@CompanyID,@PurchaseOrderID,@SubOrderIndex,@ProductName,@Model,@Type,@SystemID)");
            strSql.Append(";select cast(@@IDENTITY AS BIGINT);");
            SqlParameter[] parameters = {
					new SqlParameter("@PurchaseCount", SqlDbType.Decimal,9),
					new SqlParameter("@PurchaseUnitPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@Producer", SqlDbType.NVarChar,50),
					new SqlParameter("@Supplier", SqlDbType.NVarChar,50),
					new SqlParameter("@PurchaseRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@PurchaseTime", SqlDbType.DateTime),
					new SqlParameter("@Purchaser", SqlDbType.VarChar,20),
					new SqlParameter("@PurchaserConfirm", SqlDbType.Bit,1),
					new SqlParameter("@PurchaserConfirmTime", SqlDbType.DateTime),
					new SqlParameter("@OrderID", SqlDbType.BigInt,8),
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@Checker_Warehouse", SqlDbType.VarChar,20),
					new SqlParameter("@Checker_Technician", SqlDbType.VarChar,20),
					new SqlParameter("@AcceptanceCount", SqlDbType.Decimal,9),
					new SqlParameter("@AcceptanceResult", SqlDbType.TinyInt,1),
					new SqlParameter("@AcceptanceRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@AcceptedTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@IsDividable", SqlDbType.Bit,1),
					new SqlParameter("@PlanDetailItemID", SqlDbType.TinyInt,1),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@PurchaseOrderID", SqlDbType.VarChar,20),
					new SqlParameter("@SubOrderIndex", SqlDbType.TinyInt,1),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@Model", SqlDbType.NVarChar,20),
					new SqlParameter("@Type", SqlDbType.TinyInt,1),
                    new SqlParameter("@SystemID",SqlDbType.VarChar,2)};
            parameters[0].Value = model.PurchaseCount;
            parameters[1].Value = model.PurchaseUnitPrice;
            parameters[2].Value = model.Unit;
            parameters[3].Value = model.Producer;
            parameters[4].Value = model.Supplier;
            parameters[5].Value = model.PurchaseRemark;
            parameters[6].Value = model.PurchaseTime == DateTime.MinValue ? SqlDateTime.Null : model.PurchaseTime;
            parameters[7].Value = model.Purchaser;
            parameters[8].Value = model.PurchaserConfirm;
            parameters[9].Value = model.PurchaserConfirmTime == DateTime.MinValue ? SqlDateTime.Null : model.PurchaserConfirmTime;
            parameters[10].Value = model.OrderID;
            parameters[11].Value = model.WarehouseID;
            parameters[12].Value = model.Checker_Warehouse;
            parameters[13].Value = model.Checker_Technician;
            parameters[14].Value = model.AcceptanceCount;
            parameters[15].Value = model.AcceptanceResult;
            parameters[16].Value = model.AcceptanceRemark;
            parameters[17].Value = model.AcceptedTime == DateTime.MinValue ? SqlDateTime.Null : model.AcceptedTime;
            parameters[18].Value = model.Status;
            parameters[19].Value = model.IsDividable;
            parameters[20].Value = model.PlanDetailItemID;
            parameters[21].Value = model.CompanyID;
            parameters[22].Value = model.PurchaseOrderID;
            parameters[23].Value = model.SubOrderIndex;
            parameters[24].Value = model.ProductName;
            parameters[25].Value = model.Model;
            parameters[26].Value = model.Type;
            parameters[27].Value = string.IsNullOrEmpty(model.SystemID) ? SqlString.Null : model.SystemID;
            
            //读取ID
            long id = 1;
            if (trans != null)
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            else
            {
                using (SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rdr.Read())
                    {
                        id = rdr.GetInt64(0);
                    }
                }
            }
            return id;
        }

        /// <summary>
        /// 读取一条采购记录实体
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        private PurchaseRecordInfo GetPurchaseRecordInfo(long RecordID)
        {
           
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from " + VIEW_PURCHASE_RECORD_VIEW + " ");
            strSql.Append(" where ID=@RecordID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.BigInt)};
            parameters[0].Value = RecordID;

            PurchaseRecordInfo item = new PurchaseRecordInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    item = GetDataPurchaseRecordInfo(rd);
                    break;
                }
            }
            //封装其他相关的信息
            item.BarcodeList = GetBarcodeRecords(item.ID);
            return item;
        }


        /// <summary>
        /// 获取某个申购项中的采购记录列表
        /// </summary>
        private IList GetPurchaseRecordInfoList(long OrderID, short PlanDetailItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + VIEW_PURCHASE_RECORD_VIEW + " ");
            strSql.Append(" where OrderID=@OrderID ");
            strSql.Append(" and PlanDetailItemID=@PlanDetailItemID ");
            strSql.Append(" order by ID ASC;");//排序
            SqlParameter[] parameters = {
					new SqlParameter("@OrderID", SqlDbType.BigInt),
                    new SqlParameter("@PlanDetailItemID",SqlDbType.TinyInt)
                                        };
            parameters[0].Value = OrderID;
            parameters[1].Value = PlanDetailItemID;

            List<PurchaseRecordInfo> list = new List<PurchaseRecordInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    PurchaseRecordInfo item = GetDataPurchaseRecordInfo(rd);
                    item.BarcodeList = GetBarcodeRecords(item.ID);
                    list.Add(item);
                }
            }
            return list;
        }


        /// <summary>
        /// 获取某张申购单中的采购记录列表
        /// </summary>
        private IList GetPurchaseRecordInfoList(long OrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + VIEW_PURCHASE_RECORD_VIEW + " ");
            strSql.Append(" where OrderID=@OrderID ");
            strSql.Append(" order by ID ASC;");//排序
            SqlParameter[] parameters = {
					new SqlParameter("@OrderID", SqlDbType.BigInt)
                                        };
            parameters[0].Value = OrderID;

            List<PurchaseRecordInfo> list = new List<PurchaseRecordInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    PurchaseRecordInfo item = GetDataPurchaseRecordInfo(rd);
                    item.BarcodeList = GetBarcodeRecords(item.ID);
                    list.Add(item);
                }
            }
            return list;
        }



        /// <summary>
        /// 更新采购记录表
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void UpdatePurchaseRecordInfo(SqlTransaction trans, PurchaseRecordInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update "+TABLE_PURCHASE_RECORD+" set ");
            strSql.Append("PurchaseCount=@PurchaseCount,");
            strSql.Append("PurchaseUnitPrice=@PurchaseUnitPrice,");
            strSql.Append("Unit=@Unit,");
            strSql.Append("Producer=@Producer,");
            strSql.Append("Supplier=@Supplier,");
            strSql.Append("PurchaseRemark=@PurchaseRemark,");
            strSql.Append("PurchaseTime=@PurchaseTime,");
            strSql.Append("Purchaser=@Purchaser,");
            strSql.Append("PurchaserConfirm=@PurchaserConfirm,");
            strSql.Append("PurchaserConfirmTime=@PurchaserConfirmTime,");
            strSql.Append("OrderID=@OrderID,");
            strSql.Append("WarehouseID=@WarehouseID,");
            strSql.Append("Checker_Warehouse=@Checker_Warehouse,");
            strSql.Append("Checker_Technician=@Checker_Technician,");
            strSql.Append("AcceptanceCount=@AcceptanceCount,");
            strSql.Append("AcceptanceResult=@AcceptanceResult,");
            strSql.Append("AcceptanceRemark=@AcceptanceRemark,");
            strSql.Append("AcceptedTime=@AcceptedTime,");
            strSql.Append("Status=@Status,");
            strSql.Append("IsDividable=@IsDividable,");
            strSql.Append("PlanDetailItemID=@PlanDetailItemID,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("PurchaseOrderID=@PurchaseOrderID,");
            strSql.Append("SubOrderIndex=@SubOrderIndex,");
            strSql.Append("ProductName=@ProductName,");
            strSql.Append("Model=@Model,");
            strSql.Append("Type=@Type,");
            strSql.Append("SystemID=@SystemID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@PurchaseCount", SqlDbType.Decimal,9),
					new SqlParameter("@PurchaseUnitPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Unit", SqlDbType.NVarChar,5),
					new SqlParameter("@Producer", SqlDbType.NVarChar,50),
					new SqlParameter("@Supplier", SqlDbType.NVarChar,50),
					new SqlParameter("@PurchaseRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@PurchaseTime", SqlDbType.DateTime),
					new SqlParameter("@Purchaser", SqlDbType.VarChar,20),
					new SqlParameter("@PurchaserConfirm", SqlDbType.Bit,1),
					new SqlParameter("@PurchaserConfirmTime", SqlDbType.DateTime),
					new SqlParameter("@OrderID", SqlDbType.BigInt,8),
					new SqlParameter("@WarehouseID", SqlDbType.VarChar,2),
					new SqlParameter("@Checker_Warehouse", SqlDbType.VarChar,20),
					new SqlParameter("@Checker_Technician", SqlDbType.VarChar,20),
					new SqlParameter("@AcceptanceCount", SqlDbType.Decimal,9),
					new SqlParameter("@AcceptanceResult", SqlDbType.TinyInt,1),
					new SqlParameter("@AcceptanceRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@AcceptedTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@IsDividable", SqlDbType.Bit,1),
					new SqlParameter("@PlanDetailItemID", SqlDbType.TinyInt,1),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@PurchaseOrderID", SqlDbType.VarChar,20),
					new SqlParameter("@SubOrderIndex", SqlDbType.TinyInt,1),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@Model", SqlDbType.NVarChar,20),
					new SqlParameter("@Type", SqlDbType.TinyInt,1),
                    new SqlParameter("@SystemID",SqlDbType.VarChar,2)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.PurchaseCount;
            parameters[2].Value = model.PurchaseUnitPrice;
            parameters[3].Value = model.Unit;
            parameters[4].Value = model.Producer;
            parameters[5].Value = model.Supplier;
            parameters[6].Value = model.PurchaseRemark;
            parameters[7].Value = model.PurchaseTime == DateTime.MinValue ? SqlDateTime.Null : model.PurchaseTime;
            parameters[8].Value = model.Purchaser;
            parameters[9].Value = model.PurchaserConfirm;
            parameters[10].Value = model.PurchaserConfirmTime == DateTime.MinValue ? SqlDateTime.Null : model.PurchaserConfirmTime;
            parameters[11].Value = model.OrderID;
            parameters[12].Value = model.WarehouseID;
            parameters[13].Value = model.Checker_Warehouse;
            parameters[14].Value = model.Checker_Technician;
            parameters[15].Value = model.AcceptanceCount;
            parameters[16].Value = model.AcceptanceResult;
            parameters[17].Value = model.AcceptanceRemark;
            parameters[18].Value = model.AcceptedTime == DateTime.MinValue ? SqlDateTime.Null : model.AcceptedTime;
            parameters[19].Value = model.Status;
            parameters[20].Value = model.IsDividable;
            parameters[21].Value = model.PlanDetailItemID;
            parameters[22].Value = model.CompanyID;
            parameters[23].Value = model.PurchaseOrderID;
            parameters[24].Value = model.SubOrderIndex;
            parameters[25].Value = model.ProductName;
            parameters[26].Value = model.Model;
            parameters[27].Value = model.Type;
            parameters[28].Value = string.IsNullOrEmpty(model.SystemID) ? SqlString.Null : model.SystemID;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 删除采购记录
        /// </summary>
        /// <param name="trans">事务，如果为null，则不使用事务</param>
        /// <param name="ItemID">主键</param>
        private void DeletePurchaseRecordInfo(SqlTransaction trans, long ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete " + TABLE_PURCHASE_RECORD + " ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = ItemID;

            if (trans != null)
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            else
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 插入采购记录 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long IPurchase.InsertPurchaseRecord(PurchaseRecordInfo model)
        {
            return InsertPurchaseRecordInfo(null, model);
        }

        /// <summary>
        /// 更新采购记录
        /// </summary>
        /// <param name="model"></param>
        void IPurchase.UpdatePurchaseRecord(PurchaseRecordInfo model)
        {
            UpdatePurchaseRecordInfo(null,model);
        }

        /// <summary>
        /// 获取一条采购记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PurchaseRecordInfo IPurchase.GetPurchaseRecordInfo(long id)
        {
            return GetPurchaseRecordInfo(id);
        }

        #endregion

        #region 与采购员相关的数据库访问函数

        const string TABLE_PURCHASER = "FM2E_Purchaser";
        const string VIEW_PURCHASER = "FM2E_PurchaserView";
        /// <summary>
        /// 添加采购员
        /// </summary>
        /// <param name="p">采购员MODEL对象</param>
        void IPurchase.InsertPurchaser(PurchaserInfo p)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + TABLE_PURCHASER + "(");
            strSql.Append("CompanyID,UserID,Remark)");
            strSql.Append(" values (");
            strSql.Append("@CompanyID,@UserID,@Remark)");

            SqlParameter[] parameters = {
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@UserID", SqlDbType.VarChar,20),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50)};
            parameters[0].Value = p.CompanyID;
            parameters[1].Value = p.UserID;
            parameters[2].Value = p.Remark;

            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新采购员信息
        /// </summary>
        /// <param name="p">需要更新的采购员对象</param>
        void IPurchase.UpdatePurchaser(PurchaserInfo p)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + TABLE_PURCHASER + " set ");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@UserID", SqlDbType.VarChar,20),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50)};
            parameters[0].Value = p.ID;
            parameters[1].Value = p.CompanyID;
            parameters[2].Value = p.UserID;
            parameters[3].Value = p.Remark;
            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 删除采购员
        /// </summary>
        /// <param name="id">采购员对象ID</param>
        void IPurchase.DeletePurchaser(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete " + TABLE_PURCHASER + " ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = id;
            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 删除采购员
        /// </summary>
        /// <param name="userid">采购员用户名</param>
        void IPurchase.DeletePurchaser(string userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete " + TABLE_PURCHASER + " ");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.VarChar,20)};
            parameters[0].Value = userid;
            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获取采购员信息
        /// </summary>
        /// <param name="userid">采购员用户名</param>
        /// <returns>采购员信息</returns>
        PurchaserInfo IPurchase.GetPurchaser(string userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from " + VIEW_PURCHASER + " ");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.VarChar,20)};
            parameters[0].Value = userid;

            PurchaserInfo p = new PurchaserInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    p = GetDataPurchaser(rd);
                    break;
                }
            }
            return p;
        }
        /// <summary>
        /// 获取采购员信息
        /// </summary>
        /// <param name="id">采购员用户名</param>
        /// <returns>采购员信息</returns>
        PurchaserInfo IPurchase.GetPurchaser(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from " + VIEW_PURCHASER + " ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = id;

            PurchaserInfo p = new PurchaserInfo();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    p = GetDataPurchaser(rd);
                    break;
                }
            }
            return p;
        }

        /// <summary>
        /// 获取公司下面所有采购员信息
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns>采购员列表</returns>
        IList IPurchase.GetPurchaserList(string companyid)
        {
            List<PurchaserInfo> list = new List<PurchaserInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + VIEW_PURCHASER + " ");
            strSql.Append(" where CompanyID=@CompanyID ");
            strSql.Append(" order by UserID ASC;");//倒序排序
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2)};
            parameters[0].Value = companyid;
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    PurchaserInfo item = GetDataPurchaser(rd);
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取所有采购员信息
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns>采购员列表</returns>
        IList IPurchase.GetAllPurchaserList(string companyid)
        {
            List<PurchaserInfo> list = new List<PurchaserInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + VIEW_PURCHASER + " ");
            //strSql.Append(" where CompanyID=@CompanyID ");
            strSql.Append(" order by UserID ASC;");//倒序排序
            //SqlParameter[] parameters = {
            //        new SqlParameter("@CompanyID", SqlDbType.VarChar,2)};
            //parameters[0].Value = companyid;
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
            {
                while (rd.Read())
                {
                    PurchaserInfo item = GetDataPurchaser(rd);
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取一个采购员实体对象
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private PurchaserInfo GetDataPurchaser(IDataReader rd)
        {
            PurchaserInfo p = new PurchaserInfo();
            if (!Convert.IsDBNull(rd["ID"]))
            {
                p.ID = Convert.ToInt64(rd["ID"]);
            }
            if (!Convert.IsDBNull(rd["CompanyID"]))
            {
                p.CompanyID = Convert.ToString(rd["CompanyID"]);
            }
            if (!Convert.IsDBNull(rd["UserID"]))
            {
                p.UserID = Convert.ToString(rd["UserID"]);
            }
            if (!Convert.IsDBNull(rd["Remark"]))
            {
                p.Remark = Convert.ToString(rd["Remark"]);
            }

            if (!Convert.IsDBNull(rd["PersonName"]))
            {
                p.PurchaserName = Convert.ToString(rd["PersonName"]);
            }

            return p;
        }
        #endregion

        #region 条形码相关的项

        const string TABLE_PUCHASEDETAILBARCODE = "FM2E_PurchaseDetailBarcode";
        /// <summary>
        /// 入库的时候插入条形码记录
        /// </summary>
        /// <param name="id">采购单ID</param>
        /// <param name="itemid">明细项ID</param>
        /// <param name="barcode">条码</param>
        void IPurchase.InsertBarcodeRecord(long purchaserecordid,long orderid, short itemid, string barcode,string name,string model)
        {
            InsertBarcode(purchaserecordid, orderid, itemid, barcode, name, model);
        }

        /// <summary>
        /// 插入条码记录
        /// </summary>
        /// <param name="purchaserecordid"></param>
        /// <param name="orderid"></param>
        /// <param name="itemid"></param>
        /// <param name="barcode"></param>
        /// <param name="name"></param>
        /// <param name="model"></param>
        private void InsertBarcode(long purchaserecordid, long orderid, short itemid, string barcode, string name, string model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + TABLE_PUCHASEDETAILBARCODE + "(");
            strSql.Append("PurchaseRecordID,OrderID,ItemID,Barcode,ProductName,Model)");
            strSql.Append(" values (");
            strSql.Append("@PurchaseRecordID,@OrderID,@ItemID,@Barcode,@ProductName,@Model)");
            SqlParameter[] parameters = {
                    new SqlParameter("@PurchaseRecordID",SqlDbType.BigInt,8),
					new SqlParameter("@OrderID", SqlDbType.BigInt,8),
					new SqlParameter("@ItemID", SqlDbType.TinyInt,1),
					new SqlParameter("@Barcode", SqlDbType.VarChar,50),
                    new SqlParameter("@ProductName",SqlDbType.NVarChar,20),
                    new SqlParameter("@Model",SqlDbType.NVarChar,20)};
            parameters[0].Value = purchaserecordid;
            parameters[1].Value = orderid;
            parameters[2].Value = itemid;
            parameters[3].Value = barcode;
            parameters[4].Value = name == null ? SqlString.Null : name;
            parameters[5].Value = model == null ? SqlString.Null : model;
            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 入库的时候插入条形码记录
        /// </summary>
        /// <param name="item">插入到数据库的对象</param>
        void IPurchase.InsertBarcodeRecord(PurchaseBarcodeInfo item)
        {
            InsertBarcode(item.PurchaseRecordID, item.OrderID, item.ItemID, item.Barcode, item.ProductName, item.Model);
        }
        /// <summary>
        /// 获取基本的条码记录，即不是拆分出来的条码
        /// </summary>
        /// <param name="id">采购记录流水号ID</param>
        /// <returns>以整体入库的时候获取的条码列表</returns>
        IList IPurchase.GetBaseBarcodeRecords(long purchaserecordid)
        {
            return GetBaseBarcodeRecords(purchaserecordid);
        }

        private IList GetBaseBarcodeRecords(long purchaserecordid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + TABLE_PUCHASEDETAILBARCODE + " ");
            strSql.Append(" where PurchaseRecordID=@PurchaseRecordID and ItemID=@ItemID and SUBSTRING(Barcode, LEN(Barcode) - 1, 1) = '0'");
            SqlParameter[] parameters = {
					new SqlParameter("@PurchaseRecordID", SqlDbType.BigInt)};
            parameters[0].Value = purchaserecordid;

            List<PurchaseBarcodeInfo> list = new List<PurchaseBarcodeInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    list.Add(GetDataPurchaseBarcodeInfo(rd));
                }
            }
            return list;
        }

        /// <summary>
        /// 获取所有条码记录
        /// </summary>
        /// <param name="id">采购单ID</param>
        /// <param name="itemid">明细项ID</param>
        /// <returns>以入库的条码列表</returns>
        IList IPurchase.GetBarcodeRecords(long purchaserecordid)
        {
            return GetBarcodeRecords(purchaserecordid);
        }
        /// <summary>
        /// 获取所有条码记录
        /// </summary>
        /// <param name="id">采购单ID</param>
        /// <param name="itemid">明细项ID</param>
        /// <returns>以入库的条码列表</returns>
        private IList GetBarcodeRecords(long purchaserecordid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,SUBSTRING(Barcode, LEN(Barcode) - 1, 1) as SortExp1, SUBSTRING(Barcode,1,LEN(Barcode)-1) as SortExp2 from " + TABLE_PUCHASEDETAILBARCODE + " ");
            strSql.Append(" where PurchaseRecordID=@PurchaseRecordID");
            strSql.Append(" order by SortExp1 asc,SortExp2 asc ");
            SqlParameter[] parameters = {
					new SqlParameter("@PurchaseRecordID", SqlDbType.BigInt)};
            parameters[0].Value = purchaserecordid;

            List<PurchaseBarcodeInfo> list = new List<PurchaseBarcodeInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    list.Add(GetDataPurchaseBarcodeInfo(rd));
                }
            }
            return list;
        }

        /// <summary>
        /// 获取父条码下的子条码列表，前面0~N-3位相同的（即最后两位不需要比较）
        /// </summary>
        /// <param name="id">采购单ID</param>
        /// <param name="itemid">明细项ID</param>
        /// <param name="baseBarcode">父条码</param>
        /// <returns>子条码列表</returns>
        IList IPurchase.GetChildrenBarcodeRecords(long purchaserecordid, string baseBarcode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + TABLE_PUCHASEDETAILBARCODE + " ");
            strSql.Append(" where PurchaseRecordID=@PurchaseRecordID and (Barcode LIKE LEFT(@Barcode, LEN(Barcode) - 2) + '%')");
            SqlParameter[] parameters = {
					new SqlParameter("@PurchaseRecordID", SqlDbType.BigInt),
					new SqlParameter("@Barcode", SqlDbType.VarChar,50)};
            parameters[0].Value = purchaserecordid;
            parameters[1].Value = baseBarcode;
            List<PurchaseBarcodeInfo> list = new List<PurchaseBarcodeInfo>();
            using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                while (rd.Read())
                {
                    list.Add(GetDataPurchaseBarcodeInfo(rd));
                }
            }
            return list;
        }


        /// <summary>
        /// 获取一个条码记录实体对象
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private PurchaseBarcodeInfo GetDataPurchaseBarcodeInfo(IDataReader rd)
        {
            PurchaseBarcodeInfo item = new PurchaseBarcodeInfo();
            if (!Convert.IsDBNull(rd["ID"]))
            {
                item.ID = Convert.ToInt64(rd["ID"]);
            }

            if (!Convert.IsDBNull(rd["PurchaseRecordID"]))
            {
                item.PurchaseRecordID = Convert.ToInt64(rd["PurchaseRecordID"]);
            }

            if (!Convert.IsDBNull(rd["OrderID"]))
            {
                item.OrderID = Convert.ToInt64(rd["OrderID"]);
            }
            if (!Convert.IsDBNull(rd["ItemID"]))
            {
                item.ItemID = Convert.ToInt16(rd["ItemID"]);
            }

            if (!Convert.IsDBNull(rd["Barcode"]))
            {
                item.Barcode = Convert.ToString(rd["Barcode"]);
            }
            if (!Convert.IsDBNull(rd["ProductName"]))
            {
                item.ProductName = Convert.ToString(rd["ProductName"]);
            }

            if (!Convert.IsDBNull(rd["Model"]))
            {
                item.Model = Convert.ToString(rd["Model"]);
            }

            return item;
        }

        #endregion

        #region 查找相关

        /// <summary>
        /// 生成查询信息参数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam IPurchase.GenerateSearchInfo(PurchaseOrderSearchInfo item)
        {

            QueryParam q = new QueryParam();

            q.TableName = VIEW_PURCHASE_ORDER + " s1 left join " + VIEW_PURCHASE_ORDER_DETAIL + " s2 on s1.ID=s2.ID " +
                " left join " + VIEW_PURCHASE_RECORD_VIEW + " s3 on s2.ID = s3.OrderID and s2.ItemID = s3.PlanDetailItemID ";

            string sqlSearch = "where 1=1";
            string empty = Guid.Empty.ToString("N");

            if (item.CompanyID != null && item.CompanyID != "")
            {
                sqlSearch += " and s1.[CompanyID] = '" + item.CompanyID + "'";
            }

            if (item.OrderSn != null && item.OrderSn != "")
            {
                sqlSearch += " and s1.[PurchaseOrderID] + '-' + LTRIM(STR(s1.[SubOrderIndex])) = '" + item.OrderSn + "'";
            }
            if (item.OrderName != null && item.OrderName != "")
            {
                sqlSearch += " and s1.[PurchaseOrderName] like '%" + item.OrderName + "%'";
            }

            if (item.ProductName != null && item.ProductName != "")
            {
                sqlSearch += " and s2.[ProductName] like '%" + item.ProductName + "%'";
            }

            if (item.Model != null && item.Model != "")
            {
                sqlSearch += " and s2.[Model] like '%" + item.Model + "%'";
            }

            if (DateTime.Compare(item.TimeLower, DateTime.MinValue) != 0)
            {
                sqlSearch += " and s1.[SubmitTime]>='" + item.TimeLower.ToShortDateString() + " 00:00' ";
            }

            if (DateTime.Compare(item.TimeUpper, DateTime.MaxValue) != 0)
            {
                sqlSearch += " and s1.[UpdateTime]<'" + item.TimeUpper.AddDays(1).ToShortDateString() + " 00:00' ";
            }

            if (item.AmountLower != decimal.MinValue)
            {
                sqlSearch += " and s1.[PlanTotalAmount]>=" + item.AmountLower.ToString() + " ";
            }

            if (item.AmountUpper != decimal.MaxValue)
            {
                sqlSearch += " and s1.[PlanTotalAmount]<=" + item.AmountUpper.ToString() + " ";
            }

            if (item.Applicant != null && item.Applicant != "")
            {
                sqlSearch += " and s1.[Applicant] = '" + item.Applicant + "'";
            }



            if (item.StatusArray != null && item.StatusArray.Length > 0)
            {
                for (int i = 0; i < item.StatusArray.Length; i++)
                {
                    if (i == 0)
                    {
                        sqlSearch += " and ( ";
                        sqlSearch += " " + " s1.Status=" + (int)item.StatusArray[i] + " ";
                    }
                    else
                    {
                        sqlSearch += " or " + " s1.Status=" + (int)item.StatusArray[i] + " ";
                    }
                    if (i == item.StatusArray.Length - 1)
                    {
                        sqlSearch += " ) ";
                    }
                }
            }

            if (item.WorkFlowStatus != null && item.WorkFlowStatus.Count > 0)
            {
                for (int i = 0; i < item.WorkFlowStatus.Count; i++)
                {
                    if (i == 0)
                    {
                        sqlSearch += " and ( ";
                        sqlSearch += " " + " s1.CurrentStateName='" + item.WorkFlowStatus[i] + "' ";
                    }
                    else
                    {
                        sqlSearch += " or " + " s1.CurrentStateName='" + item.WorkFlowStatus[i] + "' ";
                    }
                    if (i == item.WorkFlowStatus.Count - 1)
                    {
                        sqlSearch += " ) ";
                    }
                }
            }

            if (!string.IsNullOrEmpty(item.WorkFlowUserName))
            {
                sqlSearch += " and s1.NextUserName='" + item.WorkFlowUserName + "' or s1.DelegateUserName='" + item.WorkFlowUserName + "'";
            }
            if (!string.IsNullOrEmpty(item.NextUserName))
            {
                sqlSearch += " and s1.NextUserName='" + item.NextUserName + "'";
            }
            if (!string.IsNullOrEmpty(item.DelegateUserName))
            {
                sqlSearch += " and s1.DelegateUserName='" + item.DelegateUserName + "'";
            }


            if (item.DetailStatusList != null && item.DetailStatusList.Count > 0)
            {
                for (int i = 0; i < item.DetailStatusList.Count; i++)
                {
                    if (i == 0)
                    {
                        sqlSearch += " and ( ";
                        sqlSearch += " " + " s2.Status=" + (int)item.DetailStatusList[i] + " ";
                    }
                    else
                    {
                        sqlSearch += " or " + " s2.Status=" + (int)item.DetailStatusList[i] + " ";
                    }
                    if (i == item.DetailStatusList.Count - 1)
                    {
                        sqlSearch += " ) ";
                    }
                }
            }

            if ((item.PurchaseRecordStatusList == null || item.PurchaseRecordStatusList.Count == 0)
                && (item.WareHouseRecordStatus == null || item.WareHouseRecordStatus.Count == 0))
            {
                if (item.WareHouseID != null && item.WareHouseID != "")
                {
                    sqlSearch += " and s3.[WarehouseID] = '" + item.WareHouseID + "'";//有仓库，但没有状态约束
                }
            }
            else
            {
                //有状态约束，但是没有仓库
                if (item.WareHouseID == null || item.WareHouseID == "")
                {
                    if (item.PurchaseRecordStatusList != null && item.PurchaseRecordStatusList.Count > 0)
                    {
                        for (int i = 0; i < item.PurchaseRecordStatusList.Count; i++)
                        {
                            if (i == 0)
                            {
                                sqlSearch += " and ( ";
                                sqlSearch += " " + " s3.Status=" + (int)item.PurchaseRecordStatusList[i] + " ";
                            }
                            else
                            {
                                sqlSearch += " or " + " s3.Status=" + (int)item.PurchaseRecordStatusList[i] + " ";
                            }
                            if (i == item.PurchaseRecordStatusList.Count - 1)
                            {
                                sqlSearch += " ) ";
                            }
                        }
                    }
                }
                else//有状态约束，又有仓库
                {
                    sqlSearch += " and ( ";
                    bool needor = false;
                    if (item.PurchaseRecordStatusList != null && item.PurchaseRecordStatusList.Count > 0)
                    {
                        needor = true;
                        for (int i = 0; i < item.PurchaseRecordStatusList.Count; i++)
                        {
                            if (i == 0)
                            {
                                sqlSearch += " " + " s3.Status=" + (int)item.PurchaseRecordStatusList[i] + " ";
                            }
                            else
                            {
                                sqlSearch += " or " + " s3.Status=" + (int)item.PurchaseRecordStatusList[i] + " ";
                            }
                        }
                    }
                    if (item.WareHouseRecordStatus != null && item.WareHouseRecordStatus.Count > 0)
                    {
                        for (int i = 0; i < item.WareHouseRecordStatus.Count; i++)
                        {
                            if (i == 0)
                            {
                                sqlSearch += (needor ? " or " : " ") + " s3.Status=" + (int)item.WareHouseRecordStatus[i] + " and s3.[WarehouseID] = '" + item.WareHouseID + "'";
                            }
                            else
                            {
                                sqlSearch += " or " + " s3.Status=" + (int)item.WareHouseRecordStatus[i] + " and s3.[WarehouseID] = '" + item.WareHouseID + "'";
                            }
                        }
                    }
                    sqlSearch += " ) ";
                }
            }
            

            q.ReturnFields = "s1.*";

            q.OrderBy = "order by UpdateTime desc";

            q.Where = sqlSearch;

            return q;
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        IList IPurchase.SearchPurchaseOrder(QueryParam qp, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectListWithDistinct(this.GetDataOrder, qp, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取采购单分页失败", e);
            }
        }



        /// <summary>
        /// 生成查询信息参数，验收专用
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam IPurchase.GenerateCheckSearchInfo(PurchaseOrderCheckSearchInfo item)
        {

            QueryParam q = new QueryParam();

            q.TableName = VIEW_PURCHASE_ORDER + " s1 left join " + VIEW_PURCHASE_ORDER_DETAIL + " s2 on s1.ID=s2.ID " +
                " left join " + VIEW_PURCHASE_RECORD_VIEW + " s3 on s2.ID = s3.OrderID and s2.ItemID = s3.PlanDetailItemID ";

            string sqlSearch = "where 1=1";
            string empty = Guid.Empty.ToString("N");

            if (item.CompanyID != null && item.CompanyID != "")
            {
                sqlSearch += " and s1.[CompanyID] = '" + item.CompanyID + "'";
            }

            if (item.OrderSn != null && item.OrderSn != "")
            {
                sqlSearch += " and s1.[PurchaseOrderID] + '-' + LTRIM(STR(s1.[SubOrderIndex])) = '" + item.OrderSn + "'";
            }
            if (item.OrderName != null && item.OrderName != "")
            {
                sqlSearch += " and s1.[PurchaseOrderName] like '%" + item.OrderName + "%'";
            }

            if (item.ProductName != null && item.ProductName != "")
            {
                sqlSearch += " and s2.[ProductName] like '%" + item.ProductName + "%'";
            }

            if (item.Model != null && item.Model != "")
            {
                sqlSearch += " and s2.[Model] like '%" + item.Model + "%'";
            }

            if (DateTime.Compare(item.TimeLower, DateTime.MinValue) != 0)
            {
                sqlSearch += " and s1.[SubmitTime]>='" + item.TimeLower.ToShortDateString() + " 00:00' ";
            }

            if (DateTime.Compare(item.TimeUpper, DateTime.MaxValue) != 0)
            {
                sqlSearch += " and s1.[UpdateTime]<'" + item.TimeUpper.AddDays(1).ToShortDateString() + " 00:00' ";
            }

            if (item.AmountLower != decimal.MinValue)
            {
                sqlSearch += " and s1.[PlanTotalAmount]>=" + item.AmountLower.ToString() + " ";
            }

            if (item.AmountUpper != decimal.MaxValue)
            {
                sqlSearch += " and s1.[PlanTotalAmount]<=" + item.AmountUpper.ToString() + " ";
            }

            if (item.Applicant != null && item.Applicant != "")
            {
                sqlSearch += " and s1.[Applicant] = '" + item.Applicant + "'";
            }



            if (item.StatusArray != null && item.StatusArray.Length > 0)
            {
                for (int i = 0; i < item.StatusArray.Length; i++)
                {
                    if (i == 0)
                    {
                        sqlSearch += " and ( ";
                        sqlSearch += " " + " s1.Status=" + (int)item.StatusArray[i] + " ";
                    }
                    else
                    {
                        sqlSearch += " or " + " s1.Status=" + (int)item.StatusArray[i] + " ";
                    }
                    if (i == item.StatusArray.Length - 1)
                    {
                        sqlSearch += " ) ";
                    }
                }
            }

            if (item.WorkFlowStatus != null && item.WorkFlowStatus.Count > 0)
            {
                for (int i = 0; i < item.WorkFlowStatus.Count; i++)
                {
                    if (i == 0)
                    {
                        sqlSearch += " and ( ";
                        sqlSearch += " " + " s1.CurrentStateName='" + item.WorkFlowStatus[i] + "' ";
                    }
                    else
                    {
                        sqlSearch += " or " + " s1.CurrentStateName='" + item.WorkFlowStatus[i] + "' ";
                    }
                    if (i == item.WorkFlowStatus.Count - 1)
                    {
                        sqlSearch += " ) ";
                    }
                }
            }
           

            if ((item.PurchaseRecordStatusList == null || item.PurchaseRecordStatusList.Count == 0)
                && (item.DetailStatusList == null || item.DetailStatusList.Count == 0))
            {
                if (item.WareHouseID != null && item.WareHouseID != "")
                {
                    sqlSearch += " and s3.[WarehouseID] = '" + item.WareHouseID + "'";//有仓库，但没有任何状态约束
                }
            }
            else
            {
                //有状态约束，但是没有仓库，可以叠加
                if (item.WareHouseID == null || item.WareHouseID == "")
                {
                    if (item.DetailStatusList != null && item.DetailStatusList.Count > 0)
                    {
                        for (int i = 0; i < item.DetailStatusList.Count; i++)
                        {
                            if (i == 0)
                            {
                                sqlSearch += " and ( ";
                                sqlSearch += " " + " s2.Status=" + (int)item.DetailStatusList[i] + " ";
                            }
                            else
                            {
                                sqlSearch += " or " + " s2.Status=" + (int)item.DetailStatusList[i] + " ";
                            }
                            if (i == item.DetailStatusList.Count - 1)
                            {
                                sqlSearch += " ) ";
                            }
                        }
                    }

                    if (item.PurchaseRecordStatusList != null && item.PurchaseRecordStatusList.Count > 0)
                    {
                        for (int i = 0; i < item.PurchaseRecordStatusList.Count; i++)
                        {
                            if (i == 0)
                            {
                                sqlSearch += " and ( ";
                                sqlSearch += " " + " s3.Status=" + (int)item.PurchaseRecordStatusList[i] + " ";
                            }
                            else
                            {
                                sqlSearch += " or " + " s3.Status=" + (int)item.PurchaseRecordStatusList[i] + " ";
                            }
                            if (i == item.PurchaseRecordStatusList.Count - 1)
                            {
                                sqlSearch += " ) ";
                            }
                        }
                    }
                }
                else//有状态约束，又有仓库
                {
                    sqlSearch += " and ( ";
                    bool needor = false;
                    if (item.DetailStatusList != null && item.DetailStatusList.Count > 0)
                    {
                        needor = true;
                        for (int i = 0; i < item.DetailStatusList.Count; i++)
                        {
                            if (i == 0)
                            {
                                sqlSearch += " " + " s2.Status=" + (int)item.DetailStatusList[i] + " ";
                            }
                            else
                            {
                                sqlSearch += " or " + " s2.Status=" + (int)item.DetailStatusList[i] + " ";
                            }
                        }
                    }
                    if (item.PurchaseRecordStatusList != null && item.PurchaseRecordStatusList.Count > 0)
                    {
                        for (int i = 0; i < item.PurchaseRecordStatusList.Count; i++)
                        {
                            if (i == 0)
                            {
                                sqlSearch += (needor ? " or " : " ") + " s3.Status=" + (int)item.PurchaseRecordStatusList[i] + " and s3.[WarehouseID] = '" + item.WareHouseID + "'";
                            }
                            else
                            {
                                sqlSearch += " or " + " s3.Status=" + (int)item.PurchaseRecordStatusList[i] + " and s3.[WarehouseID] = '" + item.WareHouseID + "'";
                            }
                        }
                    }
                    sqlSearch += " ) ";
                }
            }
            q.ReturnFields = "s1.*";

            q.OrderBy = "order by UpdateTime desc";

            q.Where = sqlSearch;

            return q;
        }

        #endregion
    }
}
