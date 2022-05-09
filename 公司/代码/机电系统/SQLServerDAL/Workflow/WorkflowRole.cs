using System;
using System. Collections;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using FM2E. SQLServerDAL. Utils;
using FM2E. Model. Utils;
using FM2E. Model. Exceptions;
using FM2E .Model .Workflow ;
using FM2E .IDAL .Workflow;

namespace FM2E. SQLServerDAL. Workflow
{
    public class WorkflowRole : IWorkflowRole
    {
        const String GET_LIST_BY_WORKFLOWNAME = "SELECT * FROM FM2E_WorkflowRole WHERE WorkflowName=@WorkflowName " +
    "ORDER BY WorkflowRoleID ASC";
        const String DELETE_BY_USERNAME_WORKFLOWNAME = "DELETE FROM b FROM FM2E_UserWorkflowRole b INNER JOIN FM2E_WorkflowRole a ON a.WorkflowRoleID = b.WorkflowRoleID WHERE b.UserName=@UserName AND a.WorkflowName=@WorkflowName";
        const String INSERT_USERWORKFLOWROLE = "INSERT INTO FM2E_UserWorkflowRole (UserName, WorkflowRoleID) VALUES (@UserName,@WorkflowRoleID)";

        const String GET_BINDING_STATES = "SELECT WorkflowStateName FROM FM2E_WorkflowRoleWorkflowState WHERE WorkflowRoleID = @ID";
        const String INSERT_BINDING_STATE = "INSERT INTO FM2E_WorkflowRoleWorkflowState (WorkflowRoleID, WorkflowStateName) VALUES (@ID, @WorkflowStateName)";
        const String DELETE_BINDING_STATES = "DELETE FROM FM2E_WorkflowRoleWorkflowState WHERE WorkflowRoleID = @ID";

        /// <summary>
        /// 获得指定工作流的工作流角色表（分页显示用）
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="pageindex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList GetWorkflowRoleList( String workflowName , QueryParam searchTerm , out int recordCount )
        {
            try
            {
                searchTerm. TableName = "FM2E_WorkflowRole";
                searchTerm. ReturnFields = "*";
                searchTerm. Where = "WHERE WorkflowName = '" + workflowName + "'";
                searchTerm. OrderBy = "ORDER BY WorkflowRoleID ASC";
                searchTerm. PageSize = 10;

                IList retList = SQLHelper. GetObjectList( this. GetData , searchTerm , out recordCount );
                foreach ( WorkflowRoleInfo info in retList )
                {
                    info. BindingStates = GetBindingStateList( info. WorkflowRoleID );
                }
                return retList;
            }
            catch ( Exception e )
            {
                throw new DALException( "获取工作流角色分页失败" , e );
            }
        }
        /// <summary>
        /// 获得指定工作流的工作流角色表
        /// </summary>
        /// <param name="workflowName"></param>
        /// <returns></returns>
        public IList GetWorkflowRoleList( String workflowName )
        {
            SqlParameter[ ] parameters = {
                    new SqlParameter ("@WorkflowName", SqlDbType .VarChar , 50)};
            parameters[ 0 ]. Value = workflowName;
            List<WorkflowRoleInfo> retList = new List<WorkflowRoleInfo>( );
            try
            {
                using ( SqlDataReader rd = SQLHelper. ExecuteReader( SQLHelper. ConnectionString , CommandType. Text , GET_LIST_BY_WORKFLOWNAME , parameters ) )
                {
                    while ( rd. Read( ) )
                    {
                        WorkflowRoleInfo info = GetData( rd );
                        info. BindingStates = GetBindingStateList( info. WorkflowRoleID );
                        retList. Add( info );
                    }
                }
            }
            catch ( Exception e )
            {
                throw new DALException( "获取工作流角色列表失败" , e );
            }
            return retList;
        }
        /// <summary>
        /// 获得指定用户在指定工作流中的角色列表（当workflowName为null时，返回该用户的所有工作流角色）
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="workflowName">为null时返回该用户的所有工作流角色</param>
        /// <returns></returns>
        public IList GetWorkflowRoleList( String userName , String workflowName )
        {
            String cmd = String. Empty;
            if ( workflowName != null )
                cmd = "SELECT a.* FROM FM2E_WorkflowRole a INNER JOIN FM2E_UserWorkflowRole b ON a.WorkflowRoleID = b.WorkflowRoleID WHERE b.UserName=@UserName AND a.WorkflowName=@WorkflowName ORDER BY b.WorkflowRoleID ASC";
            else
                cmd = "SELECT a.* FROM FM2E_WorkflowRole a INNER JOIN FM2E_UserWorkflowRole b ON a.WorkflowRoleID = b.WorkflowRoleID WHERE b.UserName=@UserName ORDER BY b.WorkflowRoleID ASC";
            SqlParameter[ ] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
                    new SqlParameter ("@WorkflowName", SqlDbType .VarChar , 50)};
            parameters[ 0 ]. Value = userName;
            parameters[ 1 ]. Value = workflowName;
            List<WorkflowRoleInfo> retList = new List<WorkflowRoleInfo>( );
            try
            {
                using ( SqlDataReader rd = SQLHelper. ExecuteReader( SQLHelper. ConnectionString , CommandType. Text , cmd , parameters ) )
                {
                    while ( rd. Read( ) )
                    {
                        WorkflowRoleInfo info = GetData( rd );
                        info. BindingStates = GetBindingStateList( info. WorkflowRoleID );
                        retList. Add( info );
                    }
                }
            }
            catch ( Exception e )
            {
                throw new DALException( "获取工作流角色分页失败" , e );
            }
            return retList;
        }

        #region 对FM2E_WorkflowRole表的操作
        public WorkflowRoleInfo GetWorkflowRoleInfo( long id )
        {
            StringBuilder strSql = new StringBuilder( );
            strSql. Append( "SELECT  TOP 1 * FROM FM2E_WorkflowRole " );
            strSql. Append( " where WorkflowRoleID=@ID " );
            SqlParameter[ ] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[ 0 ]. Value = id;
            WorkflowRoleInfo item = null;
            try
            {
                using ( SqlDataReader rd = SQLHelper. ExecuteReader( SQLHelper. ConnectionString , CommandType. Text , strSql. ToString( ) , parameters ) )
                {
                    if ( rd. Read( ) )
                    {
                        item = this. GetData( rd );
                    }
                }
            }
            catch ( Exception e )
            {
                throw new DALException( "获取工作流角色失败" , e );
            }
            return item;
        }
        //非接口成员
        long GetWorkflowRoleID( String workflowName , String roleName )
        {
            StringBuilder strSql = new StringBuilder( );
            strSql. Append( "SELECT WorkflowRoleID FROM FM2E_WorkflowRole " );
            strSql. Append( " WHERE RoleName=@Name AND WorkflowName=@WorkflowName" );
            SqlParameter[ ] parameters = {
					new SqlParameter("@Name", SqlDbType.VarChar, 50),
                     new SqlParameter("@WorkflowName", SqlDbType .VarChar ,50)};
            parameters[ 0 ]. Value = roleName;
            parameters[ 1 ]. Value = workflowName;
            long ret = -1;
            try
            {
                using ( SqlDataReader rd = SQLHelper. ExecuteReader( SQLHelper. ConnectionString , CommandType. Text , strSql. ToString( ) , parameters ) )
                {
                    if ( rd. Read( ) )
                    {
                        if ( !Convert. IsDBNull( rd[ 0 ] ) )
                            ret = Convert. ToInt64( rd[ 0 ] );
                    }
                }
            }
            catch ( Exception e )
            {
                throw new DALException( "获取工作流角色失败" , e );
            }
            return ret;
        }
        public void InsertWorkflowRole( WorkflowRoleInfo item )
        {
            long existID = GetWorkflowRoleID( item. WorkflowName , item. RoleName );
            if ( existID > -1 && existID != item. WorkflowRoleID )
                throw new DALException( "当前工作流中同名的角色已存在" , null );

            StringBuilder strSql = new StringBuilder( );
            strSql. Append( "INSERT INTO FM2E_WorkflowRole " );
            strSql. Append( "(WorkflowName, RoleName, IsSingle, IsApprover) " );
            strSql. Append( "VALUES (" );
            strSql. Append( "@WorkflowName,@RoleName,@IsSingle, @IsApprover)" );
            strSql. Append( " SELECT @WorkflowRoleID = @@Identity" );
            using ( SqlConnection conn = new SqlConnection( SQLHelper. ConnectionString ) )
            {
                conn. Open( );
                SqlCommand cmd = conn. CreateCommand( );
                cmd. CommandText = strSql. ToString( );
                cmd. Parameters. Add( new SqlParameter( "@WorkflowName" , SqlDbType. VarChar , 50 ) );
                cmd. Parameters. Add( new SqlParameter( "@RoleName" , SqlDbType. VarChar , 50 ) );
                cmd. Parameters. Add( new SqlParameter( "@IsSingle" , SqlDbType. Bit ) );
                cmd. Parameters. Add( new SqlParameter( "@IsApprover" , SqlDbType. Bit ) );
                cmd. Parameters. Add( new SqlParameter( "@WorkflowRoleID" , SqlDbType. BigInt ) ). Direction = ParameterDirection. Output;
                cmd. Parameters[ 0 ]. Value = item. WorkflowName;
                cmd. Parameters[ 1 ]. Value = item. RoleName;
                cmd. Parameters[ 2 ]. Value = ( item. IsSingle == true ? 1 : 0 );
                cmd. Parameters[ 3 ]. Value = ( item. IsApprover == true ? 1 : 0 );
                try
                {
                    cmd. ExecuteNonQuery( );
                    long id = ( long )( cmd. Parameters[ "@WorkflowRoleID" ]. Value );
                    foreach ( String stateName in item. BindingStates )
                    {
                        InsertBindingState( id , stateName );
                    }
                }
                catch ( Exception e )
                {
                    throw new DALException( "添加工作流角色失败" , e );
                }
                finally
                {
                    conn. Close( );
                }
            }
        }
        public void UpdateWorkflowRoleInfo( WorkflowRoleInfo item )
        {
            long existID = GetWorkflowRoleID( item. WorkflowName , item. RoleName );
            if ( existID > -1 && existID != item. WorkflowRoleID )
                throw new DALException( "当前工作流中同名的角色已存在" , null );

            StringBuilder strSql = new StringBuilder( );
            strSql. Append( "UPDATE FM2E_WorkflowRole SET " );
            strSql. Append( "RoleName=@RoleName, " );
            strSql. Append( "IsSingle=@IsSingle, " );
            strSql. Append( "IsApprover=@IsApprover " );
            strSql. Append( "where WorkflowRoleID=@ID " );
            SqlParameter[ ] parameters = {
					new SqlParameter("@RoleName", SqlDbType.VarChar,50),
					new SqlParameter("@IsSingle", SqlDbType.Bit),
					new SqlParameter("@IsApprover", SqlDbType.Bit),
                    new SqlParameter ("@ID", SqlDbType .BigInt)};
            parameters[ 0 ]. Value = item. RoleName;
            parameters[ 1 ]. Value = ( item. IsSingle == true ? 1 : 0 );
            parameters[ 2 ]. Value = ( item. IsApprover == true ? 1 : 0 );
            parameters[ 3 ]. Value = item. WorkflowRoleID;

            using ( SqlConnection conn = new SqlConnection( SQLHelper. ConnectionString ) )
            {
                conn. Open( );
                try
                {
                    SQLHelper. ExecuteNonQuery( conn , CommandType. Text , strSql. ToString( ) , parameters );
                    UpdateBindingStateList( item );
                }
                catch ( Exception e )
                {
                    throw new DALException( "更新工作流角色失败" , e );
                }
                finally
                {
                    conn. Close( );
                }
            }
        }
        public void DeleteWorkflowRole( long id )
        {
            StringBuilder strSql = new StringBuilder( );
            strSql. Append( "DELETE FROM FM2E_WorkflowRole " );
            strSql. Append( " where WorkflowRoleID=@ID " );
            SqlParameter[ ] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[ 0 ]. Value = id;

            using ( SqlConnection conn = new SqlConnection( SQLHelper. ConnectionString ) )
            {
                conn. Open( );
                try
                {
                    SQLHelper. ExecuteNonQuery( conn , CommandType. Text , strSql. ToString( ) , parameters );
                    DeleteBindingStateList( id );
                }
                catch ( Exception e )
                {
                    throw new DALException( "删除工作流角色失败" , e );
                }
                finally
                {
                    conn. Close( );
                }
            }
        }
        #endregion

        #region 对FM2E_UserWorkflowRole表的操作
        public void UpdateUserWorkflowRole( String userName , String workflowName , List<long> roleIDList )
        {
            DeleteUserWorkflowRole( userName , workflowName );

            SqlParameter[ ] parameters = {
                 new SqlParameter ("@UserName", SqlDbType .VarChar , 20),
                 new SqlParameter ("@WorkflowRoleID", SqlDbType.BigInt)};
            parameters[ 0 ]. Value = userName;
            using ( SqlConnection conn = new SqlConnection( SQLHelper. ConnectionString ) )
            {
                conn. Open( );
                try
                {
                    foreach ( long id in roleIDList )
                    {
                        parameters[ 1 ]. Value = id;
                        SQLHelper. ExecuteNonQuery( conn , CommandType. Text , INSERT_USERWORKFLOWROLE , parameters );
                    }
                }
                catch ( Exception e )
                {
                    throw new DALException( "配置用户的工作流角色失败" , e );
                }
                finally
                {
                    conn. Close( );
                }
            }
        }
        public void DeleteUserWorkflowRole( String userName , String workflowName )
        {
            SqlParameter[ ] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
                    new SqlParameter ("@WorkflowName", SqlDbType .VarChar , 50)};
            parameters[ 0 ]. Value = userName;
            parameters[ 1 ]. Value = workflowName;
            using ( SqlConnection conn = new SqlConnection( SQLHelper. ConnectionString ) )
            {
                conn. Open( );
                try
                {
                    SQLHelper. ExecuteNonQuery( conn , CommandType. Text , DELETE_BY_USERNAME_WORKFLOWNAME , parameters );
                }
                catch ( Exception e )
                {
                    throw new DALException( "解除用户与工作流角色绑定失败" , e );
                }
                finally
                {
                    conn. Close( );
                }
            }
        }
        public void DeleteUserWorkflowRole( String userName )
        {
            String cmd = "DELETE FROM FM2E_UserWorkflowRole WHERE UserName='" + userName + "'";
            using ( SqlConnection conn = new SqlConnection( SQLHelper. ConnectionString ) )
            {
                conn. Open( );
                try
                {
                    SQLHelper. ExecuteNonQuery( conn , CommandType. Text , cmd );
                }
                catch ( Exception e )
                {
                    throw new DALException( "解除用户与工作流角色绑定失败" , e );
                }
                finally
                {
                    conn. Close( );
                }
            }
        }
        public void DeleteUserWorkflowRole( long workflowRoleID )
        {
            String cmd = "DELETE FROM FM2E_UserWorkflowRole WHERE WorkflowRoleID=" + workflowRoleID. ToString( );
            using ( SqlConnection conn = new SqlConnection( SQLHelper. ConnectionString ) )
            {
                conn. Open( );
                try
                {
                    SQLHelper. ExecuteNonQuery( conn , CommandType. Text , cmd );
                }
                catch ( Exception e )
                {
                    throw new DALException( "解除用户与工作流角色绑定失败" , e );
                }
                finally
                {
                    conn. Close( );
                }
            }
        }
        #endregion

        #region 对FM2E_UserDelegate表的操作
        public List<UserDelegateInfo> GetUserDelegateList( String chiefUserName )
        {
            String cmd = "SELECT * FROM FM2E_UserDelegate WHERE ChiefUserName='" + chiefUserName + "'";
            try
            {
                List<UserDelegateInfo> retList = new List<UserDelegateInfo>( );
                using ( SqlDataReader reader = SQLHelper. ExecuteReader( SQLHelper. ConnectionString , CommandType. Text , cmd , null ) )
                {
                    while ( reader. Read( ) )
                    {
                        UserDelegateInfo info = new UserDelegateInfo( );
                        if ( !Convert. IsDBNull( reader[ "ChiefUserName" ] ) )
                            info. ChiefUserName = reader[ "ChiefUserName" ] as String;
                        if ( !Convert. IsDBNull( reader[ "DelegateUserName" ] ) )
                            info. DelegateUserName = reader[ "DelegateUserName" ] as String;
                        if ( !Convert. IsDBNull( reader[ "WorkflowRoleID" ] ) )
                            info. WorkflowRoleID = ( long )reader[ "WorkflowRoleID" ];
                        if ( !Convert. IsDBNull( reader[ "DelegateStartTime" ] ) )
                            info. DelegateStartTime = ( DateTime )reader[ "DelegateStartTime" ];
                        if ( !Convert. IsDBNull( reader[ "DelegateEndTime" ] ) )
                            info. DelegateEndTime = ( DateTime )reader[ "DelegateEndTime" ];
                        retList. Add( info );
                    }
                }
                return retList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获得指定用户在某工作流角色上的有效代理
        /// </summary>
        /// <param name="chiefUserName"></param>
        /// <param name="workflowRoleID"></param>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        public UserDelegateInfo GetUserDelegate(String chiefUserName, long workflowRoleID, DateTime currentTime)
        {
            String cmd = "SELECT * FROM FM2E_UserDelegate WHERE ChiefUserName=@ChiefUserName AND WorkflowRoleID = @WorkflowRoleID AND DelegateStartTime < @CurrentTime AND DelegateEndTime > @CurrentTime";
            SqlParameter[ ] parameters = {
                    new SqlParameter ("@ChiefUserName", SqlDbType .VarChar , 20),
                    new SqlParameter ("@WorkflowRoleID", SqlDbType .BigInt),
                    new SqlParameter ("@CurrentTime", SqlDbType .DateTime)};
            parameters[ 0 ]. Value = chiefUserName;
            parameters[ 1 ]. Value = workflowRoleID;
            parameters[ 2 ]. Value = currentTime;
            try
            {
                UserDelegateInfo info = null;
                using ( SqlDataReader reader = SQLHelper. ExecuteReader( SQLHelper. ConnectionString , CommandType. Text , cmd , parameters ) )
                {
                    if( reader. Read( ) )
                    {
                        info = new UserDelegateInfo( );
                        if ( !Convert. IsDBNull( reader[ "ChiefUserName" ] ) )
                            info. ChiefUserName = reader[ "ChiefUserName" ] as String;
                        if ( !Convert. IsDBNull( reader[ "DelegateUserName" ] ) )
                            info. DelegateUserName = reader[ "DelegateUserName" ] as String;
                        if ( !Convert. IsDBNull( reader[ "WorkflowRoleID" ] ) )
                            info. WorkflowRoleID = ( long )reader[ "WorkflowRoleID" ];
                        if ( !Convert. IsDBNull( reader[ "DelegateStartTime" ] ) )
                            info. DelegateStartTime = ( DateTime )reader[ "DelegateStartTime" ];
                        if ( !Convert. IsDBNull( reader[ "DelegateEndTime" ] ) )
                            info. DelegateEndTime = ( DateTime )reader[ "DelegateEndTime" ];
                    }
                }
                return info;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 获得指定用户在某工作流角色上的有效代理
        /// </summary>
        /// <param name="chiefUserName"></param>
        /// <param name="workflowRoleID"></param>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        public UserDelegateInfo GetUserDelegate(String chiefUserName, DateTime currentTime,string workflowName,string workflowState)
        {
            String cmd = "SELECT s1.*,s4.PersonName FROM FM2E_UserDelegate s1 INNER JOIN" +
                " FM2E_WorkflowRole s2 ON s1.WorkflowRoleID = s2.WorkflowRoleID " +
                " INNER JOIN FM2E_WorkflowRoleWorkflowState s3 ON " +
                " s2.WorkflowRoleID = s3.WorkflowRoleID " +
                " LEFT JOIN FM2E_UserView s4 ON s1.DelegateUserName= s4.UserName "+
              "WHERE s1.ChiefUserName=@ChiefUserName AND s1.DelegateStartTime < @CurrentTime AND s1.DelegateEndTime > @CurrentTime" +
              " AND s2.WorkflowName = @WorkflowName AND s3.WorkflowStateName = @WorkflowStateName";
            SqlParameter[] parameters = {
                    new SqlParameter ("@ChiefUserName", SqlDbType .VarChar , 20),
                    new SqlParameter ("@CurrentTime", SqlDbType .DateTime),
                    new SqlParameter("@WorkflowName",SqlDbType.VarChar,50),
                    new SqlParameter("@WorkflowStateName",SqlDbType.VarChar,50)};
            parameters[0].Value = chiefUserName;
            parameters[1].Value = currentTime;
            parameters[2].Value = workflowName;
            parameters[3].Value = workflowState;
            try
            {
                UserDelegateInfo info = null;
                using (SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, cmd, parameters))
                {
                    if (reader.Read())
                    {
                        info = new UserDelegateInfo();
                        if (!Convert.IsDBNull(reader["ChiefUserName"]))
                            info.ChiefUserName = reader["ChiefUserName"] as String;
                        if (!Convert.IsDBNull(reader["DelegateUserName"]))
                            info.DelegateUserName = reader["DelegateUserName"] as String;
                        if (!Convert.IsDBNull(reader["WorkflowRoleID"]))
                            info.WorkflowRoleID = (long)reader["WorkflowRoleID"];
                        if (!Convert.IsDBNull(reader["DelegateStartTime"]))
                            info.DelegateStartTime = (DateTime)reader["DelegateStartTime"];
                        if (!Convert.IsDBNull(reader["DelegateEndTime"]))
                            info.DelegateEndTime = (DateTime)reader["DelegateEndTime"];
                        try
                        {
                            if (!Convert.IsDBNull(reader["PersonName"]))
                            {
                                info.DelegateUserPersonName = Convert.ToString(reader["PersonName"]);
                            }

                        }
                        catch { }
                    }
                }
                return info;
            }
            catch
            {
                throw;
            }
        }

        public void InsertUserDelegate( String chiefUserName , String delegateUserName , long workflowRoleID , DateTime delegateStartTime , DateTime delegateEndTime )
        {
            String cmd = "INSERT INTO FM2E_UserDelegate " +
            "(ChiefUserName, DelegateUserName, WorkflowRoleID, DelegateStartTime, DelegateEndTime) " +
            "VALUES ( @ChiefUserName,@DelegateUserName,@WorkflowRoleID, @DelegateStartTime, @DelegateEndTime ) ";
            SqlParameter[ ] parameters = {
                    new SqlParameter ("@ChiefUserName", SqlDbType.VarChar, 20),
                    new SqlParameter ("@DelegateUserName", SqlDbType.VarChar, 20),
					new SqlParameter("@WorkflowRoleID", SqlDbType.BigInt),
                    new SqlParameter("@DelegateStartTime", SqlDbType.DateTime),
                    new SqlParameter("@DelegateEndTime", SqlDbType.DateTime)
                    };
            parameters[ 0 ]. Value = chiefUserName;
            parameters[ 1 ]. Value = delegateUserName;
            parameters[ 2 ]. Value = workflowRoleID;
            parameters[ 3 ]. Value = delegateStartTime;
            parameters[ 4 ]. Value = delegateEndTime;

            using ( SqlConnection conn = new SqlConnection( SQLHelper. ConnectionString ) )
            {
                try
                {
                    SQLHelper. ExecuteNonQuery( conn , CommandType. Text , cmd , parameters );
                }
                catch ( Exception )
                {
                    throw;
                }
            }
        }

        //public void DeleteUserDelegate

        //public void UpdateUserDelegate
        #endregion

        #region 对FM2E_WorkflowRoleWorkflowState表的操作（非接口成员）
        List<String> GetBindingStateList( long workflowRoleID )
        {
            SqlParameter[ ] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[ 0 ]. Value = workflowRoleID;
            List<String> retList = new List<string>( );
            try
            {
                using ( SqlDataReader rd = SQLHelper. ExecuteReader( SQLHelper. ConnectionString , CommandType. Text , GET_BINDING_STATES , parameters ) )
                {
                    while ( rd. Read( ) )
                    {
                        if ( !Convert. IsDBNull( rd[ "WorkflowStateName" ] ) )
                            retList. Add( rd[ "WorkflowStateName" ] as String );
                    }
                }
            }
            catch ( Exception e )
            {
                throw new DALException( "获取工作流角色绑定状态失败" , e );
            }
            return retList;
        }

        void InsertBindingState( long workflowRoleID , String workflowStateName )
        {
            String cmd = "INSERT INTO FM2E_WorkflowRoleWorkflowState (WorkflowRoleID, WorkflowStateName) VALUES (" + workflowRoleID. ToString( ) + ", '" + workflowStateName + "')";
            using ( SqlConnection conn = new SqlConnection( SQLHelper. ConnectionString ) )
            {
                try
                {
                    SQLHelper. ExecuteNonQuery( conn , CommandType. Text , cmd , null );
                }
                catch ( Exception e )
                {
                    throw new DALException( "添加工作流角色绑定状态失败" , e );
                }
                finally
                {
                    conn. Close( );
                }
            }
        }

        void DeleteBindingStateList( long workflowRoleID )
        {
            String cmd = "DELETE FROM FM2E_WorkflowRoleWorkflowState WHERE WorkflowRoleID = " + workflowRoleID. ToString( );
            using ( SqlConnection conn = new SqlConnection( SQLHelper. ConnectionString ) )
            {
                try
                {
                    SQLHelper. ExecuteNonQuery( conn , CommandType. Text , cmd , null );
                }
                catch ( Exception e )
                {
                    throw new DALException( "删除工作流角色绑定状态失败" , e );
                }
                finally
                {
                    conn. Close( );
                }
            }
        }

        void UpdateBindingStateList( WorkflowRoleInfo updatedInfo )
        {
            try
            {
                DeleteBindingStateList( updatedInfo. WorkflowRoleID );
                foreach ( String s in updatedInfo. BindingStates )
                {
                    InsertBindingState( updatedInfo. WorkflowRoleID , s );
                }
            }
            catch ( Exception e )
            {
                throw new DALException( "更新工作流角色绑定状态失败" , e );
            }
        }
        #endregion

        private WorkflowRoleInfo GetData( IDataReader rd )
        {
            WorkflowRoleInfo item = new WorkflowRoleInfo( );

            if ( !Convert. IsDBNull( rd[ "WorkflowRoleID" ] ) )
                item. WorkflowRoleID = Convert. ToInt64( rd[ "WorkflowRoleID" ] );

            if ( !Convert. IsDBNull( rd[ "RoleName" ] ) )
                item. RoleName = Convert. ToString( rd[ "RoleName" ] );

            if ( !Convert. IsDBNull( rd[ "WorkflowName" ] ) )
                item. WorkflowName = Convert. ToString( rd[ "WorkflowName" ] );

            if ( !Convert. IsDBNull( rd[ "IsSingle" ] ) )
                item. IsSingle = Convert. ToBoolean( rd[ "IsSingle" ] );

            if ( !Convert. IsDBNull( rd[ "IsApprover" ] ) )
                item. IsApprover = Convert. ToBoolean( rd[ "IsApprover" ] );

            return item;
        }
    }
}