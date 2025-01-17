﻿using System;
using System. Collections;
using System. Collections. Generic;
using System. Text;
using System. Data. SqlClient;
using System. Data;

using FM2E. SQLServerDAL. Utils;
using FM2E. Model. Exceptions;
using FM2E. Model. Workflow;
using FM2E. IDAL. Workflow;
using System.Data.SqlTypes;
using FM2E.Model.System;// 暂时使用 [4/17/2012 L]
//using FM2E.BLL.Basic;// 暂时使用 [4/17/2012 L]
using System.Configuration;









namespace FM2E. SQLServerDAL. Workflow
{



    public class WorkflowHelperDAL
    {
        
       // private readonly Department deptBll = new Department();


       // private readonly MalfunctionHandle malfunctionBll = new MalfunctionHandle();
        //private readonly Company companyBll = new Company();




        /// <summary>
        /// 将一个工作流实例与一个数据项绑定
        /// </summary>
        /// <param name="instanceID"></param>
        /// <param name="tableName"></param>
        /// <param name="dataID"></param>
        public void BindInstanceToData( Guid instanceID , String workflowName , String tableName , long dataID )
        {
            String strSql = "INSERT INTO FM2E_WorkflowInstance (InstanceID,TableName,DataID,WorkflowName) VALUES " +
                                    "(@InstanceID, @TableName, @DataID, @WorkflowName)";
            SqlParameter[ ] parameters = {
                    new SqlParameter ("@InstanceID", SqlDbType .UniqueIdentifier),
					new SqlParameter("@TableName", SqlDbType.VarChar,50),
					new SqlParameter("@DataID", SqlDbType.BigInt),
                    new SqlParameter("@WorkflowName", SqlDbType.VarChar,50)
                    };
            parameters[ 0 ]. Value = instanceID;
            parameters[ 1 ]. Value = tableName;
            parameters[ 2 ]. Value = dataID;
            parameters[ 3 ]. Value = workflowName;

            using ( SqlConnection conn = new SqlConnection( SQLHelper. ConnectionString ) )
            {
                try
                {
                    SQLHelper. ExecuteNonQuery( conn , CommandType. Text , strSql , parameters );
                }
                catch ( Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 更新一个实例的当前状态
        /// </summary>
        /// <param name="instanceID"></param>
        /// <param name="stateName"></param>
        public void UpdateInstanceStatus( Guid instanceID , String stateName , String stateDescription )
        {
            String cmd = "UPDATE FM2E_WorkflowInstance SET " +
                        "CurrentStateName=@StateName, " +
                        "StatusDescription=@StateDescription, " +
                        "NextUserName = @NextUserName, " +                           //每次将专人专项的设置清空，有需要再额外调用UpdateNextUser来填充
                        "DelegateUserName = @DelegateUserName " +
                        "WHERE InstanceID=@instanceID ";
            SqlParameter[ ] parameters = {
					new SqlParameter("@StateName", SqlDbType.VarChar,50),
                    new SqlParameter("@StateDescription", SqlDbType.VarChar, 50),
                    new SqlParameter ("@instanceID", SqlDbType .UniqueIdentifier),
                    new SqlParameter("@NextUserName",SqlDbType.VarChar,20),
                    new SqlParameter("@DelegateUserName",SqlDbType.VarChar,20)};
            parameters[ 0 ]. Value = stateName;
            parameters[ 1 ]. Value = stateDescription;
            parameters[ 2 ]. Value = instanceID;
            parameters[3].Value = SqlString.Null;
            parameters[4].Value = SqlString.Null;

            using ( SqlConnection conn = new SqlConnection( SQLHelper. ConnectionString ) )
            {
                try
                {
                    SQLHelper. ExecuteNonQuery( conn , CommandType. Text , cmd , parameters );
                }
                catch
                {
                    throw;
                }
            }
        }


        //********** Modified by Xue    For ShenGaoSu    2011-11-28 ********************************************************************************
        /// <summary>
        /// 更新一个实例的当前状态
        /// </summary>
        /// <param name="instanceID"></param>
        /// <param name="stateName"></param>
        public void UpdateInstanceStatusAndNextUser(string workflowName, Guid instanceID, String stateName, String stateDescription,out string nextUserName)
        {
            List<string> nextUser = this.GetAllApprover(workflowName, stateName, Guid.Empty);

            String cmd = "UPDATE FM2E_WorkflowInstance SET " +
                        "CurrentStateName=@StateName, " +
                        "StatusDescription=@StateDescription, " +
                        "NextUserName = @NextUserName, " +                           
                        "DelegateUserName = @DelegateUserName " +
                        "WHERE InstanceID=@instanceID ";
            SqlParameter[] parameters = {
					new SqlParameter("@StateName", SqlDbType.VarChar,50),
                    new SqlParameter("@StateDescription", SqlDbType.VarChar, 50),
                    new SqlParameter ("@instanceID", SqlDbType .UniqueIdentifier),
                    new SqlParameter("@NextUserName",SqlDbType.VarChar,20),
                    new SqlParameter("@DelegateUserName",SqlDbType.VarChar,20)};
            parameters[0].Value = stateName;
            parameters[1].Value = stateDescription;
            parameters[2].Value = instanceID;
            if (nextUser.Count == 0)
            {
                nextUserName = "WorkflowDefaultUser"; 
                parameters[3].Value = "WorkflowDefaultUser";                //没有设置工作流用户的，所有单都发到"WorkflowDefaultUser"账号
            }
            else
            {
                
                
                nextUserName = nextUser[0];  
                parameters[3].Value = nextUser[0];                           //目前是专人专项，只能取单个
            }
            parameters[4].Value = SqlString.Null;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, cmd, parameters);
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// 更新一个实例的当前状态
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="instanceID"></param>
        /// <param name="stateName"></param>
        /// <param name="nextUserName"></param>
        public void UpdateInstanceStatusAndNextUser(string workflowName, Guid instanceID, String stateName, out string nextUserName)
        {
            List<string> nextUser = this.GetAllApprover(workflowName, stateName, Guid.Empty);

            String cmd = "UPDATE FM2E_WorkflowInstance SET " +
                        "CurrentStateName=@StateName, " +
                        "NextUserName = @NextUserName, " +
                        "DelegateUserName = @DelegateUserName " +
                        "WHERE InstanceID=@instanceID ";
            SqlParameter[] parameters = {
					new SqlParameter("@StateName", SqlDbType.VarChar,50),
                    new SqlParameter ("@instanceID", SqlDbType .UniqueIdentifier),
                    new SqlParameter("@NextUserName",SqlDbType.VarChar,20),
                    new SqlParameter("@DelegateUserName",SqlDbType.VarChar,20)};
            parameters[0].Value = stateName;
            parameters[1].Value = instanceID;
            if (nextUser.Count == 0)
            {
                nextUserName = "WorkflowDefaultUser";
                parameters[2].Value = "WorkflowDefaultUser";                //没有设置工作流用户的，所有单都发到"WorkflowDefaultUser"账号
            }
            else
            {
                nextUserName = nextUser[0];
                parameters[2].Value = nextUser[0];                           //目前是专人专项，只能取单个
            }
            parameters[3].Value = SqlString.Null;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, cmd, parameters);
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// 更新一个实例的当前状态，根据维护单位
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="instanceID"></param>
        /// <param name="stateName"></param>
        /// <param name="company"></param>
        /// <param name="nextUserName"></param>
        public void UpdateInstanceStatusAndNextUser(string workflowName, Guid instanceID, String stateName, String stateDescription, String company,out string nextUserName)
        {
            List<string> nextUser = this.GetAllApprover(workflowName, stateName, Guid.Empty);

            String cmd = "UPDATE FM2E_WorkflowInstance SET " +
                        "CurrentStateName=@StateName, " +
                        "StatusDescription=@StateDescription, " +
                        "NextUserName = @NextUserName, " +
                        "DelegateUserName = @DelegateUserName " +
                        "WHERE InstanceID=@instanceID ";
            SqlParameter[] parameters = {
					new SqlParameter("@StateName", SqlDbType.VarChar,50),
                    new SqlParameter("@StateDescription", SqlDbType.VarChar, 50),
                    new SqlParameter ("@instanceID", SqlDbType .UniqueIdentifier),
                    new SqlParameter("@NextUserName",SqlDbType.VarChar,20),
                    new SqlParameter("@DelegateUserName",SqlDbType.VarChar,20)};
            parameters[0].Value = stateName;
            parameters[1].Value = stateDescription;
            parameters[2].Value = instanceID;
            if (nextUser.Count == 0)
            {
                nextUserName = "WorkflowDefaultUser";
                parameters[3].Value = "WorkflowDefaultUser";                //没有设置工作流用户的，所有单都发到"WorkflowDefaultUser"账号
            }
            else
            {
                parameters[3].Value = nextUser[0]; 

                nextUserName = "WorkflowDefaultUser"; 
                string CompanyName = Convert.ToString(ExecuteScalar("select CompanyName  from DepartmentView where name = '" + company+"'"));
                for (int i = 0; i < nextUser.Count;i++ )
                {
                    string nextusername = nextUser[i];
                    string nextUserCompanyName = Convert.ToString(ExecuteScalar("select CompanyName  from FM2E_UserView where UserName = '" + nextusername+"'"));
                    if (CompanyName == nextUserCompanyName)
                    {
                        nextUserName = nextUser[i];
                        parameters[3].Value = nextUser[i];
                    }
                 
                }

                // 选择流程节点审批的人员，留意数据库对应的数据
                
                //目前是专人专项，只能取单个
            }
            parameters[4].Value = SqlString.Null;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, cmd, parameters);
                }
                catch
                {
                    throw;
                }
            }


        }

        public void UpdateInstanceStatusAndNextUser1(string workflowName, Guid instanceID, String stateName, String stateDescription, String company, out string nextUserName)
        {
            List<string> nextUser = this.GetAllApprover(workflowName, stateName, Guid.Empty);

            String cmd = "UPDATE FM2E_WorkflowInstance SET " +
                        "CurrentStateName=@StateName, " +
                        "StatusDescription=@StateDescription, " +
                        "NextUserName = @NextUserName, " +
                        "DelegateUserName = @DelegateUserName " +
                        "WHERE InstanceID=@instanceID ";
            SqlParameter[] parameters = {
					new SqlParameter("@StateName", SqlDbType.VarChar,50),
                    new SqlParameter("@StateDescription", SqlDbType.VarChar, 50),
                    new SqlParameter ("@instanceID", SqlDbType .UniqueIdentifier),
                    new SqlParameter("@NextUserName",SqlDbType.VarChar,20),
                    new SqlParameter("@DelegateUserName",SqlDbType.VarChar,20)};
            parameters[0].Value = stateName;
            parameters[1].Value = stateDescription;
            parameters[2].Value = instanceID;
            if (nextUser.Count == 0)
            {
                nextUserName = "WorkflowDefaultUser";
                parameters[3].Value = "WorkflowDefaultUser";                //没有设置工作流用户的，所有单都发到"WorkflowDefaultUser"账号
            }
            else
            {
                parameters[3].Value = nextUser[0];

                nextUserName = "WorkflowDefaultUser";
                string CompanyName = Convert.ToString(ExecuteScalar("select CompanyName  from DepartmentView where name = '" + company + "'"));
                for (int i = 0; i < nextUser.Count; i++)
                {
                    string nextusername = nextUser[i];
                    string nextUserCompanyName = Convert.ToString(ExecuteScalar("select CompanyName  from FM2E_UserView where UserName = '" + nextusername + "'"));
                    if (CompanyName == nextUserCompanyName)
                    {
                        nextUserName = nextUser[i];
                        parameters[3].Value = nextUser[i];
                    }

                }

                // 选择流程节点审批的人员，留意数据库对应的数据

                //目前是专人专项，只能取单个
            }
            parameters[4].Value = SqlString.Null;

            string next = "";
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {

                string sql = "select DelegateUserName from FM2E_WorkflowInstance where InstanceID = @ID";
                SqlParameter[] p = {
                                  new SqlParameter("@ID", SqlDbType.UniqueIdentifier),
                                   };
                p[0].Value = instanceID;
                try
                {
                    SqlDataReader rd = SQLHelper.ExecuteReader(conn, CommandType.Text, sql, p);
                    if (rd.Read())
                    {
                        next = rd[0].ToString();
                    }
                }
                catch
                {
                    throw;
                }

                if (!string.IsNullOrEmpty(next))
                {
                    parameters[3].Value = next;
                }
            }

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, cmd, parameters);
                }
                catch
                {
                    throw;
                }
            }


        }

        public void UpdateInstanceStatusAndNextUser2(string workflowName, Guid instanceID, String stateName, String stateDescription, String company, out string nextUserName)
        {
            List<string> nextUser = this.GetAllApprover(workflowName, stateName, Guid.Empty);

            String cmd = "UPDATE FM2E_WorkflowInstance SET " +
                        "CurrentStateName=@StateName, " +
                        "StatusDescription=@StateDescription, " +
                        "NextUserName = @NextUserName, " +
                        "DelegateUserName = @DelegateUserName " +
                        "WHERE InstanceID=@instanceID ";
            SqlParameter[] parameters = {
					new SqlParameter("@StateName", SqlDbType.VarChar,50),
                    new SqlParameter("@StateDescription", SqlDbType.VarChar, 50),
                    new SqlParameter ("@instanceID", SqlDbType .UniqueIdentifier),
                    new SqlParameter("@NextUserName",SqlDbType.VarChar,20),
                    new SqlParameter("@DelegateUserName",SqlDbType.VarChar,20)};
            parameters[0].Value = stateName;
            parameters[1].Value = stateDescription;
            parameters[2].Value = instanceID;
            if (nextUser.Count == 0)
            {
                nextUserName = "WorkflowDefaultUser";
                parameters[3].Value = "WorkflowDefaultUser";                //没有设置工作流用户的，所有单都发到"WorkflowDefaultUser"账号
            }
            else
            {
                parameters[3].Value = nextUser[0];

                nextUserName = "WorkflowDefaultUser";
                string CompanyName = Convert.ToString(ExecuteScalar("select CompanyName  from DepartmentView where name = '" + company + "'"));
                for (int i = 0; i < nextUser.Count; i++)
                {
                    string nextusername = nextUser[i];
                    string nextUserCompanyName = Convert.ToString(ExecuteScalar("select CompanyName  from FM2E_UserView where UserName = '" + nextusername + "'"));
                    if (CompanyName == nextUserCompanyName)
                    {
                        nextUserName = nextUser[i];
                        parameters[3].Value = nextUser[i];
                    }

                }

                // 选择流程节点审批的人员，留意数据库对应的数据

                //目前是专人专项，只能取单个
            }
            parameters[4].Value = SqlString.Null;
            string next = "";
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {

                string sql = "select DelegateUserName from FM2E_WorkflowInstance where InstanceID = @ID";
                SqlParameter[] p = {
                                  new SqlParameter("@ID", SqlDbType.UniqueIdentifier),
                                   };
                p[0].Value = instanceID;
                try
                {
                    SqlDataReader rd = SQLHelper.ExecuteReader(conn, CommandType.Text, sql, p);
                    if (rd.Read())
                    {
                        next = rd[0].ToString();
                    }
                }
                catch
                {
                    throw;
                }
            }

            if (!string.IsNullOrEmpty(next))
            {
                parameters[4].Value = next;
            }

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, cmd, parameters);
                }
                catch
                {
                    throw;
                }
            }
        }

        public void UpdateInstanceStatusAndNextUser3(string workflowName, Guid instanceID, String stateName, String stateDescription, String company,string delegateUserName, out string nextUserName)
        {
            List<string> nextUser = this.GetAllApprover(workflowName, stateName, Guid.Empty);

            String cmd = "UPDATE FM2E_WorkflowInstance SET " +
                        "CurrentStateName=@StateName, " +
                        "StatusDescription=@StateDescription, " +
                        "NextUserName = @NextUserName, " +
                        "DelegateUserName = @DelegateUserName " +
                        "WHERE InstanceID=@instanceID ";
            SqlParameter[] parameters = {
					new SqlParameter("@StateName", SqlDbType.VarChar,50),
                    new SqlParameter("@StateDescription", SqlDbType.VarChar, 50),
                    new SqlParameter ("@instanceID", SqlDbType .UniqueIdentifier),
                    new SqlParameter("@NextUserName",SqlDbType.VarChar,20),
                    new SqlParameter("@DelegateUserName",SqlDbType.VarChar,20)};
            parameters[0].Value = stateName;
            parameters[1].Value = stateDescription;
            parameters[2].Value = instanceID;
            if (nextUser.Count == 0)
            {
                nextUserName = "WorkflowDefaultUser";
                parameters[3].Value = "WorkflowDefaultUser";                //没有设置工作流用户的，所有单都发到"WorkflowDefaultUser"账号
            }
            else
            {
                parameters[3].Value = delegateUserName;

                nextUserName = "WorkflowDefaultUser";
                //string CompanyName = Convert.ToString(ExecuteScalar("select CompanyName  from DepartmentView where name = '" + company + "'"));
                //for (int i = 0; i < nextUser.Count; i++)
                //{
                //    string nextusername = nextUser[i];
                //    string nextUserCompanyName = Convert.ToString(ExecuteScalar("select CompanyName  from FM2E_UserView where UserName = '" + nextusername + "'"));
                //    if (CompanyName == nextUserCompanyName)
                //    {
                //        nextUserName = nextUser[i];
                //        parameters[3].Value = nextUser[i];
                //    }

                //}

                // 选择流程节点审批的人员，留意数据库对应的数据

                //目前是专人专项，只能取单个
            }
            parameters[4].Value = SqlString.Null;
            string next = "";
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {

                string sql = "select DelegateUserName from FM2E_WorkflowInstance where InstanceID = @ID";
                SqlParameter[] p = {
                                  new SqlParameter("@ID", SqlDbType.UniqueIdentifier),
                                   };
                p[0].Value = instanceID;
                try
                {
                    SqlDataReader rd = SQLHelper.ExecuteReader(conn, CommandType.Text, sql, p);
                    if (rd.Read())
                    {
                        next = rd[0].ToString();
                    }
                }
                catch
                {
                    throw;
                }
            }

            if (!string.IsNullOrEmpty(next))
            {
                parameters[4].Value = next;
            }

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, cmd, parameters);
                }
                catch
                {
                    throw;
                }
            }
        }

        public void UpdateInstanceStatusAndNextUser(string workflowName, Guid instanceID, String stateName, String stateDescription, bool stationCheck, string systemID,String next, out string nextUserName)
        {
            List<string> nextUser = this.GetAllApprover(workflowName, stateName, Guid.Empty);

            String cmd = "UPDATE FM2E_WorkflowInstance SET " +
                        "CurrentStateName=@StateName, " +
                        "StatusDescription=@StateDescription, " +
                        "NextUserName = @NextUserName, " +
                        "DelegateUserName = @DelegateUserName " +
                        "WHERE InstanceID=@instanceID ";
            SqlParameter[] parameters = {
					new SqlParameter("@StateName", SqlDbType.VarChar,50),
                    new SqlParameter("@StateDescription", SqlDbType.VarChar, 50),
                    new SqlParameter ("@instanceID", SqlDbType .UniqueIdentifier),
                    new SqlParameter("@NextUserName",SqlDbType.VarChar,20),
                    new SqlParameter("@DelegateUserName",SqlDbType.VarChar,20)};
            parameters[0].Value = stateName;
            parameters[1].Value = stateDescription;
            parameters[2].Value = instanceID;
            if (nextUser.Count == 0)
            {
                nextUserName = "WorkflowDefaultUser";
                parameters[3].Value = "WorkflowDefaultUser";                //没有设置工作流用户的，所有单都发到"WorkflowDefaultUser"账号
            }
            else if (stationCheck)
            {
                parameters[3].Value = nextUser[0];
                nextUserName = "WorkflowDefaultUser";
                string systemEngineerID = ConfigurationManager.AppSettings["SoftwareSystemID"];
                for (int i = 0; i < nextUser.Count; i++)
                {
                    string nextusername = nextUser[i];
                    string nextUserSystemEngineerID = Convert.ToString(ExecuteScalar("select IM  from FM2E_UserView where UserName = '" + nextusername + "'"));
                    if (nextUserSystemEngineerID.Contains(systemEngineerID))
                    {
                        nextUserName = nextUser[i];
                        parameters[3].Value = nextUser[i];
                    }
                }
            }
            else
            {
                parameters[3].Value = nextUser[0];
                nextUserName = "WorkflowDefaultUser";
                for (int i = 0; i < nextUser.Count; i++)
                {
                    string nextusername = nextUser[i];
                    string nextUserSystemEngineerID = Convert.ToString(ExecuteScalar("select IM  from FM2E_UserView where UserName = '" + nextusername + "'"));
                    if (nextUserSystemEngineerID.Contains(systemID))
                    {
                        nextUserName = nextUser[i];
                        parameters[3].Value = nextUser[i];
                    }
                }
            }

            parameters[4].Value = SqlString.Null;


            //************
            //if (next == null)
            //{
            //    using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            //    {

            //        string sql = "select DelegateUserName from FM2E_WorkflowInstance where InstanceID = @ID";
            //        SqlParameter[] p = {
            //                      new SqlParameter("@ID", SqlDbType.UniqueIdentifier),
            //                       };
            //        p[0].Value = instanceID;
            //        try
            //        {
            //            SqlDataReader rd = SQLHelper.ExecuteReader(conn, CommandType.Text, sql, p);
            //            if (rd.Read())
            //            {
            //                next = rd[0].ToString();
            //            }
            //        }
            //        catch
            //        {
            //            throw;
            //        }
            //    }

            //    if (!string.IsNullOrEmpty(next))
            //    {
            //        parameters[3].Value = next;
            //    }
            //}
            //else
            //{
            //    parameters[4].Value = next;
            //}
            if (next != null)
            {
                parameters[3].Value = next;
            }
            //************

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, cmd, parameters);
                }
                catch
                {
                    throw;
                }
            }


        }

        //  [6/13/2013 Genland]
        /// <summary>
        /// 更新一个实例的当前状态，根据故障类型    
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="instanceID"></param>
        /// <param name="stateName"></param>
        /// <param name="stationCheck"></param>
        /// <param name="nextUserName"></param>
        public void UpdateInstanceStatusAndNextUser(string workflowName, Guid instanceID, String stateName, String stateDescription, bool stationCheck, string systemID, out string nextUserName)
        {
            List<string> nextUser = this.GetAllApprover(workflowName, stateName, Guid.Empty);

            String cmd = "UPDATE FM2E_WorkflowInstance SET " +
                        "CurrentStateName=@StateName, " +
                        "StatusDescription=@StateDescription, " +
                        "NextUserName = @NextUserName, " +
                        "DelegateUserName = @DelegateUserName " +
                        "WHERE InstanceID=@instanceID ";
            SqlParameter[] parameters = {
					new SqlParameter("@StateName", SqlDbType.VarChar,50),
                    new SqlParameter("@StateDescription", SqlDbType.VarChar, 50),
                    new SqlParameter ("@instanceID", SqlDbType .UniqueIdentifier),
                    new SqlParameter("@NextUserName",SqlDbType.VarChar,20),
                    new SqlParameter("@DelegateUserName",SqlDbType.VarChar,20)};
            parameters[0].Value = stateName;
            parameters[1].Value = stateDescription;
            parameters[2].Value = instanceID;
            if (nextUser.Count == 0)
            {
                nextUserName = "WorkflowDefaultUser";
                parameters[3].Value = "WorkflowDefaultUser";                //没有设置工作流用户的，所有单都发到"WorkflowDefaultUser"账号
            }
            else if (stationCheck)
            {                
                parameters[3].Value = nextUser[0];
                nextUserName = "WorkflowDefaultUser";
                string systemEngineerID = ConfigurationManager.AppSettings["SoftwareSystemID"];
                for (int i = 0; i < nextUser.Count; i++)
                {
                    string nextusername = nextUser[i];
                    string nextUserSystemEngineerID = Convert.ToString(ExecuteScalar("select IM  from FM2E_UserView where UserName = '" + nextusername + "'"));
                    if (nextUserSystemEngineerID.Contains(systemEngineerID))
                    {
                        nextUserName = nextUser[i];
                        parameters[3].Value = nextUser[i];
                    }
                }                
             }
            else
            {
                parameters[3].Value = nextUser[0];
                nextUserName = "WorkflowDefaultUser";
                for (int i = 0; i < nextUser.Count; i++)
                {
                    string nextusername = nextUser[i];
                    string nextUserSystemEngineerID = Convert.ToString(ExecuteScalar("select IM  from FM2E_UserView where UserName = '" + nextusername + "'"));
                    if (nextUserSystemEngineerID.Contains(systemID))
                    {
                        nextUserName = nextUser[i];
                        parameters[3].Value = nextUser[i];
                    }
                }   
            }
            
            parameters[4].Value = SqlString.Null;

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, cmd, parameters);
                }
                catch
                {
                    throw;
                }
            }


        }
        //  [6/13/2013 Genland]
        //******************************2012-10 By L*********************************************************
        public static object ExecuteScalar(string Sql, params SqlParameter[] parameters)
        {
            String connStr = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = Sql; //清除就数据
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    return cmd.ExecuteScalar().ToString();
                }

            }
        }
        //******************************2012-10 By L*********************************************************

        //********** Modification Finished 2011-11-28 **********************************************************************************************




        /// <summary>
        /// 获得一个角色列表所绑定的所有工作流状态
        /// </summary>
        /// <param name="roleInfoList"></param>
        /// <returns></returns>
        public List<String> GetBindingStates( IList roleInfoList )
        {
            const String SELECT_BINDINGSTATES_BY_ROLE = "SELECT a.WorkflowStateName FROM FM2E_WorkflowRoleWorkflowState a INNER JOIN FM2E_WorkflowRole b ON a.WorkflowRoleID = b.WorkflowRoleID WHERE b.WorkflowName = @WorkflowName AND b.WorkflowRoleID = @WorkflowRoleID ";
            if ( roleInfoList. Count > 0 )
            {
                SqlParameter[ ] parameters = {
                    new SqlParameter ("@WorkflowName", SqlDbType .VarChar , 50),
                    new SqlParameter("@WorkflowRoleID", SqlDbType.BigInt) };
                parameters[ 0 ]. Value = ( roleInfoList[ 0 ] as WorkflowRoleInfo ). WorkflowName;
                try
                {
                    List<String> bindingStateList = new List<string>( );
                    foreach ( WorkflowRoleInfo info in roleInfoList )
                    {
                        parameters[ 1 ]. Value = info. WorkflowRoleID;
                        using ( SqlDataReader reader = SQLHelper. ExecuteReader( SQLHelper. ConnectionString , CommandType. Text , SELECT_BINDINGSTATES_BY_ROLE , parameters ) )
                        {
                            while ( reader. Read( ) )
                                if ( !Convert. IsDBNull( reader[ "WorkflowStateName" ] ) )
                                {
                                    String s = reader[ "WorkflowStateName" ] as String;
                                    if ( !bindingStateList. Contains( s ) )
                                        bindingStateList. Add( s );
                                }
                        }
                    }
                    return bindingStateList;
                }
                catch
                {
                    throw;
                }
            }
            else
                return new List<String>( 0 );
        }

        /// <summary>
        /// 根据一个状态名称数组获得相关的所有数据项
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="stateNames"></param>
        /// <returns></returns>
        public List<long> GetDataIDListByStateNames( String tableName , String[ ] stateNames )
        {
            const String SELECT_DATAID_BY_STATES = "SELECT DataID FROM FM2E_WorkflowInstance WHERE  TableName = @TableName AND CurrentStateName = @CurrentStateName AND ChiefUserName = null";          //专人专项的数据不包括在内
            SqlParameter[ ] parameters = {
                    new SqlParameter ("@TableName", SqlDbType .VarChar , 50),
                    new SqlParameter("@CurrentStateName", SqlDbType.VarChar , 50) };
            parameters[ 0 ]. Value = tableName;
            try
            {
                List<long> dataIDList = new List<long>( );
                foreach ( String name in stateNames )
                {
                    parameters[ 1 ]. Value = name;
                    using ( SqlDataReader reader = SQLHelper. ExecuteReader( SQLHelper. ConnectionString , CommandType. Text , SELECT_DATAID_BY_STATES , parameters ) )
                    {
                        while ( reader. Read( ) )
                            if ( !Convert. IsDBNull( reader[ "DataID" ] ) )
                                dataIDList. Add( ( long )reader[ "DataID" ] );
                    }
                }
                return dataIDList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获得工作流实例的信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataID"></param>
        /// <returns></returns>
        public WorkflowInstanceInfo GetWorkflowInstanceInfo( String tableName , long dataID )
        {
            String cmd = "SELECT InstanceID,CurrentStateName,NextUserName,DelegateUserName FROM FM2E_WorkflowInstance WHERE TableName='" + tableName + "' and DataID = " + dataID. ToString( );
            try
            {
                WorkflowInstanceInfo ret = null;
                using (SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, null))
                {
                    while (reader.Read())
                        if (!(Convert.IsDBNull(reader["InstanceID"]) || Convert.IsDBNull(reader["CurrentStateName"]) || Convert.IsDBNull(reader["NextUserName"])))
                            ret = new WorkflowInstanceInfo((Guid)reader["InstanceID"], (String)reader["CurrentStateName"], (String)reader["NextUserName"], Convert.IsDBNull(reader["DelegateUserName"]) ? null : (String)reader["DelegateUserName"]);
                }
                return ret;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 清空InstanceState表中所有工作流实例的Owner信息
        /// （上次强行关闭Runtime而残留的，若不清理会导致这些实例被持续锁定，从而下次无法正常读取）
        /// </summary>
        public void ClearInstanceStateOwner( )
        {
            String cmd = "UPDATE InstanceState SET ownerID = NULL, ownedUntil = NULL";
            using ( SqlConnection conn = new SqlConnection( SQLHelper. ConnectionString ) )
            {
                try
                {
                    SQLHelper. ExecuteNonQuery( conn , CommandType. Text , cmd , null );
                }
                catch ( Exception e )
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// 删除一个工作流实例记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataID"></param>
        public void DeleteWorkflowInstance( String tableName , long dataID )
        {
            String cmd = "DELETE FROM FM2E_WorkflowInstance WHERE TableName = '" + tableName + "' AND DataID=" + dataID. ToString( );
            using ( SqlConnection conn = new SqlConnection( SQLHelper. ConnectionString ) )
            {
                try
                {
                    SQLHelper. ExecuteNonQuery( conn , CommandType. Text , cmd , null );
                }
                catch ( Exception e )
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// 当instanceId为Guid.Empty时，获得一个工作流中某状态的所有审批者用户名; 
        /// 当instanceId不为Guid.Empty时返回专人专项的用户名
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="workflowStateName"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public List<String> GetAllApprover( String workflowName , String workflowStateName, Guid instanceId )
        {
            if ( instanceId == Guid.Empty )
            {
                String cmd = "SELECT DISTINCT a.UserName FROM " +
                    "FM2E_UserWorkflowRole a INNER JOIN FM2E_WorkflowRole b ON a.WorkflowRoleID = b.WorkflowRoleID " +
                    "INNER JOIN FM2E_WorkflowRoleWorkflowState c ON b.WorkflowRoleID = c.WorkflowRoleID " +
                    "WHERE b.IsApprover = 1 AND b.WorkflowName = '" + workflowName + "' AND c.WorkflowStateName = '" + workflowStateName + "'";
                List<String> retUserNameList = new List<string>( );
                try
                {
                    using ( SqlDataReader reader = SQLHelper. ExecuteReader( SQLHelper. ConnectionString , CommandType. Text , cmd , null ) )
                    {
                        while ( reader. Read( ) )
                            if ( !Convert. IsDBNull( reader[ "UserName" ] ) )
                                retUserNameList. Add( reader[ "UserName" ] as String );
                    }
                    return retUserNameList;
                }
                catch
                {
                    throw;
                }
            }
            else      //专人专项情况的处理
            {
                String cmd = "SELECT NextUserName, DelegateUserName FROM FM2E_WorkflowInstance WHERE InstanceID = '" + instanceId. ToString( ) + "'";
                try
                {
                    using ( SqlDataReader reader = SQLHelper. ExecuteReader( SQLHelper. ConnectionString , CommandType. Text , cmd , null ) )
                    {
                        List<String> retList = new List<string> ();
                        if ( reader. Read( ) )
                        {
                            if ( !Convert. IsDBNull( reader[ "DelegateUserName" ] ) )
                                retList. Add( reader[ "DelegateUserName" ] as String );
                            if ( !Convert. IsDBNull( reader[ "NextUserName" ] ) )
                                retList. Add( reader[ "NextUserName" ] as String );
                            return retList;
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }
            return null;
        }


        /// <summary>
        /// 更改一个工作流的一个状态的描述
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="oldDescription"></param>
        /// <param name="newDescription"></param>
        public void UpdateStateDescription( String workflowName , String oldDescription , String newDescription )
        {
            String cmd = "UPDATE FM2E_WorkflowInstance SET " +
            "StatusDescription=@NewDescription " +
            "WHERE WorkflowName=@WorkflowName AND StatusDescription = @OldDescription ";
            SqlParameter[ ] parameters = {
					new SqlParameter("@NewDescription", SqlDbType.VarChar,50),
                    new SqlParameter("@WorkflowName", SqlDbType.VarChar, 50),
                    new SqlParameter ("@OldDescription", SqlDbType.VarChar, 50)};
            parameters[ 0 ]. Value = newDescription;
            parameters[ 1 ]. Value = workflowName;
            parameters[ 2 ]. Value = oldDescription;

            using ( SqlConnection conn = new SqlConnection( SQLHelper. ConnectionString ) )
            {
                try
                {
                    SQLHelper. ExecuteNonQuery( conn , CommandType. Text , cmd , parameters );
                }
                catch
                {
                    throw;
                }
            }
        }

        #region Extension Functions ( 按分部门工作流的需求 ）
        /// <summary>
        /// 给工作流实例增加专人专项，如果有代理用户会自动添加
        /// </summary>
        /// <param name="instanceID"></param>
        /// <param name="nextUserName"></param>
        public void UpdateNextUser( Guid instanceID , String nextUserName )
        {
            //先将与该用户有关的过时代理删除
            String cmd = "DELETE FROM FM2E_UserDelegate WHERE ChiefUsername = @NextUserName AND DelegateEndTime < GetDate() " +
                //然后将下一用户名和代理（如果存在）写进FM2E_WorkflowInstance表
                        "UPDATE FM2E_WorkflowInstance SET " +
                        "NextUserName=@NextUserName, " +
                        "DelegateUserName = " +

                        //如果符合权限要求的代理存在，则写入
                        "( SELECT TOP 1 DelegateUserName FROM FM2E_UserDelegate a WHERE ChiefUserName = @NextUserName AND " +
                        "DelegateStartTime < GetDate() AND "+          //代理开始时间要小于当前
                        "EXISTS (SELECT WorkflowStateName FROM FM2E_WorkflowRoleWorkflowState " +
                        "WHERE WorkflowStateName = " +                //代理的工作流角色权限要包括当前工作流状态
                        "(SELECT CurrentStateName FROM FM2E_WorkflowInstance WHERE InstanceID = @InstanceID) " + 
                        "AND WorkflowRoleID = a.WorkflowRoleID) ) " +

                        "WHERE InstanceID = @InstanceID";

            using ( SqlConnection conn = new SqlConnection( SQLHelper. ConnectionString ) )
            {
                SqlParameter[ ] parameters = new SqlParameter[ 2 ]{
                   	new SqlParameter("@NextUserName", SqlDbType.VarChar,20),
                    new SqlParameter ("@InstanceID", SqlDbType .UniqueIdentifier)};
                parameters[ 0 ]. Value = nextUserName;
                parameters[ 1 ]. Value = instanceID;
                try
                {
                    SQLHelper. ExecuteNonQuery( conn , CommandType. Text , cmd , null );
                }
                catch ( Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 给工作流实例增加专人专项，如果有代理用户会自动添加
        /// </summary>
        /// <param name="instanceID"></param>
        /// <param name="nextUserName"></param>
        public void UpdateNextUser(Guid instanceID, String nextUserName,string delegateUserName)
        {
            
            String cmd = "UPDATE FM2E_WorkflowInstance SET " +
                        "NextUserName=@NextUserName, " +
                        "DelegateUserName = @DelegateUserName " +
                        "WHERE InstanceID = @InstanceID";

            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                SqlParameter[] parameters = {
                   	new SqlParameter("@NextUserName", SqlDbType.VarChar,20),
                    new SqlParameter("@DelegateUserName",SqlDbType.VarChar,20),
                    new SqlParameter ("@InstanceID", SqlDbType .UniqueIdentifier)};
                parameters[0].Value = string.IsNullOrEmpty(nextUserName) ? SqlString.Null : nextUserName;
                parameters[1].Value = string.IsNullOrEmpty(delegateUserName) ? SqlString.Null : delegateUserName;
                parameters[2].Value = instanceID;
                try
                {
                    SQLHelper.ExecuteNonQuery(conn, CommandType.Text, cmd, parameters);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 仅根据用户名（专人专项，包括代理用户）获取相关的数据ID列表
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<long> GetDataIDJustByUserName(String workflowName, String userName)
        {
            String cmd = "SELECT DataID FROM FM2E_WorkflowInstance WHERE WorkflowName = @WorkflowName AND (ChiefUserName = @UserName OR DelegateUserName = @UserName)";

            SqlParameter[ ] parameters = {
                    new SqlParameter ("@WorkflowName", SqlDbType .VarChar , 50),
                    new SqlParameter("@UserName", SqlDbType.VarChar , 20) };
            parameters[ 0 ]. Value = workflowName;
            parameters[ 1 ]. Value = userName;
            try
            {
                using ( SqlDataReader reader = SQLHelper. ExecuteReader( SQLHelper. ConnectionString , CommandType. Text , cmd , null ) )
                {
                    List<long> dataIDList = new List<long>( );
                    while ( reader. Read( ) )
                    {
                        if ( !Convert. IsDBNull( reader[ "DataID" ] ) )
                            dataIDList. Add( ( long )reader[ "DataID" ] );
                    }
                    return dataIDList;
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
