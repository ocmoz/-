using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using FM2E.Model.System;
using FM2E.Model.Exceptions;
using FM2E.IDAL.System;
using System.Data;
using System.Data.SqlClient;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Utils;
using System.Data.SqlTypes;

namespace FM2E.SQLServerDAL.System
{
    public class User : IUser
    {
        /// <summary>
        /// 获取用户数据实体
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private UserInfo GetData(IDataReader rd)
        {
            UserInfo item = new UserInfo();

            if (!Convert.IsDBNull(rd["Address"]))
                item.Address = Convert.ToString(rd["Address"]);

            if (!Convert.IsDBNull(rd["Birthday"]))
                item.Birthday = Convert.ToDateTime(rd["Birthday"]);

            if (!Convert.IsDBNull(rd["CompanyID"]))
                item.CompanyID = Convert.ToString(rd["CompanyID"]);

            if (!Convert.IsDBNull(rd["CompanyName"]))
                item.CompanyName = Convert.ToString(rd["CompanyName"]);

            if (!Convert.IsDBNull(rd["DepartmentID"]))
                item.DepartmentID = Convert.ToInt64(rd["DepartmentID"]);

            if (!Convert.IsDBNull(rd["DepartmentName"]))
                item.DepartmentName = Convert.ToString(rd["DepartmentName"]);

            if (!Convert.IsDBNull(rd["Email"]))
                item.Email = Convert.ToString(rd["Email"]);

            if (!Convert.IsDBNull(rd["Fax"]))
                item.Fax = Convert.ToString(rd["Fax"]);

            if (!Convert.IsDBNull(rd["HomePhone"]))
                item.HomePhone = Convert.ToString(rd["HomePhone"]);

            if (!Convert.IsDBNull(rd["IDCard"]))
                item.IDCard = Convert.ToString(rd["IDCard"]);

            if (!Convert.IsDBNull(rd["IM"]))
                item.IM = Convert.ToString(rd["IM"]);

            if (!Convert.IsDBNull(rd["LastLoginTime"]))
                item.LastLoginTime = Convert.ToDateTime(rd["LastLoginTime"]);

            if (!Convert.IsDBNull(rd["MobilePhone"]))
                item.MobilePhone = Convert.ToString(rd["MobilePhone"]);

            if (!Convert.IsDBNull(rd["OfficePhone"]))
                item.OfficePhone = Convert.ToString(rd["OfficePhone"]);

            if (!Convert.IsDBNull(rd["Password"]))
                item.Password = Convert.ToString(rd["Password"]);

            if (!Convert.IsDBNull(rd["PersonName"]))
                item.PersonName = Convert.ToString(rd["PersonName"]);

            if (!Convert.IsDBNull(rd["PhotoUrl"]))
                item.PhotoUrl = Convert.ToString(rd["PhotoUrl"]);

            if (!Convert.IsDBNull(rd["PositionID"]))
                item.PositionID = Convert.ToInt64(rd["PositionID"]);

            if (!Convert.IsDBNull(rd["PositionName"]))
                item.PositionName = Convert.ToString(rd["PositionName"]);

            if (!Convert.IsDBNull(rd["Responsibility"]))
                item.Responsibility = Convert.ToString(rd["Responsibility"]);

            if (!Convert.IsDBNull(rd["Sex"]))
                item.Sex = (Sex)Convert.ToInt32(rd["Sex"]);

            if (!Convert.IsDBNull(rd["StaffNO"]))
                item.StaffNO = Convert.ToString(rd["StaffNO"]);

            if (!Convert.IsDBNull(rd["Status"]))
                item.Status = (UserStatus)Convert.ToInt32(rd["Status"]);

            if (!Convert.IsDBNull(rd["UpdateTime"]))
                item.UpdateTime = Convert.ToDateTime(rd["UpdateTime"]);

            if (!Convert.IsDBNull(rd["UserName"]))
                item.UserName = Convert.ToString(rd["UserName"]);

            if (!Convert.IsDBNull(rd["UserType"]))
                item.UserType = (UserType)Convert.ToInt32(rd["UserType"]);

            if (!Convert.IsDBNull(rd["IsParentCompany"]))
                item.IsParentCompany = Convert.ToBoolean(rd["IsParentCompany"]);
            return item;
        }
        #region IUser 成员
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        IList IUser.GetAllUser()
        {
            ArrayList list = new ArrayList();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select UserName,DepartmentID,PositionID,Sex,Birthday,PhotoUrl,OfficePhone,MobilePhone,HomePhone,Fax,Address,Password,Email,IM,Responsibility,UpdateTime,PositionName,DepartmentName,CompanyName,UserType,Status,LastLoginTime,StaffNO,CompanyID,PersonName,IDCard,IsParentCompany ");
                strSql.Append(" FROM FM2E_UserView ");
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                        list.Add(GetData(rd));
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取用户列表失败", ex);
            }
            return list;
        }

        /// <summary>
        /// 获取工作流状态下指定部门的用户列表
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="workflowStateName"></param>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        IList IUser.GetNextWorkflowUser(String workflowName, String workflowStateName,long departmentID)
        {
            ArrayList list = new ArrayList();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select DISTINCT FM2E_UserView.* ");
                strSql.Append(" FROM FM2E_UserView INNER JOIN FM2E_UserWorkflowRole a");
                strSql.Append(" ON FM2E_UserView.UserName = a.UserName ");
                strSql.Append(" INNER JOIN FM2E_WorkflowRole b ON a.WorkflowRoleID = b.WorkflowRoleID ");
                strSql.Append(" INNER JOIN FM2E_WorkflowRoleWorkflowState c ON b.WorkflowRoleID = c.WorkflowRoleID ");
                strSql.Append(" WHERE FM2E_UserView.DepartmentID = "+departmentID);
                strSql.Append(" and b.IsApprover = 1 AND b.WorkflowName = '" + workflowName + "' AND c.WorkflowStateName = '" + workflowStateName + "'");
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                        list.Add(GetData(rd));
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取工作流用户列表失败", ex);
            }
            return list;

        }

        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        UserInfo IUser.GetUser(string userName)
        {
            UserInfo user = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select UserName,DepartmentID,PositionID,Sex,Birthday,PhotoUrl,OfficePhone,MobilePhone,HomePhone,Fax,Address,Password,Email,IM,Responsibility,UpdateTime,PositionName,DepartmentName,CompanyName,UserType,Status,LastLoginTime,StaffNO,CompanyID,PersonName,IDCard,IsParentCompany ");
                strSql.Append(" FROM FM2E_UserView ");
                strSql.Append(" where UserName=@UserName");

                SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20)};
                parameters[0].Value = userName;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    if (rd.Read())
                        user = GetData(rd);
                }

                if (user == null)
                    return null;

                //取出用户角色列表
                StringBuilder roleSql = new StringBuilder();
                roleSql.Append("select a.UserName as [UserName],b.RoleID as [RoleID],b.RoleName as [RoleName]");
                roleSql.Append(" FROM FM2E_UserRole a left join FM2E_Role b on a.RoleID=b.RoleID where a.UserName=@UserName");

                ArrayList roles = new ArrayList();
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, roleSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        UserRoleInfo item = new UserRoleInfo();
                        if (!Convert.IsDBNull(rd["UserName"]))
                            item.UserName = Convert.ToString(rd["UserName"]);

                        if (!Convert.IsDBNull(rd["RoleID"]))
                            item.RoleID = Convert.ToInt64(rd["RoleID"]);

                        if (!Convert.IsDBNull(rd["RoleName"]))
                            item.RoleName = Convert.ToString(rd["RoleName"]);
                        roles.Add(item);
                    }
                }

                user.Roles = roles;
            }
            catch (Exception ex)
            {
                user = null;
                throw new DALException("获取用户列表失败", ex);
            }
            return user;
        }
        /// <summary>
        /// 获取属于某种角色的所有用户
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        IList IUser.GetUsers(long roleID)
        {
            ArrayList list = new ArrayList();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM FM2E_UserRole where RoleID=" + roleID);

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null))
                {
                    while (rd.Read())
                    {
                        UserRoleInfo item = new UserRoleInfo();

                        if (!Convert.IsDBNull(rd["RoleID"]))
                            item.RoleID = Convert.ToInt64(rd["RoleID"]);

                        if (!Convert.IsDBNull(rd["UserName"]))
                            item.UserName = Convert.ToString(rd["UserName"]);

                        list.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException("获取属于RoleID：" + roleID + "的用户列表失败", ex);
            }

            return list;
        }
        /// <summary>
        /// 获取属于某一间公司的所有用户
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        IList IUser.GetUsersByCompanyID(string companyID)
        {
            ArrayList list = new ArrayList();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select UserName,DepartmentID,PositionID,Sex,Birthday,PhotoUrl,OfficePhone,MobilePhone,HomePhone,Fax,Address,Password,Email,IM,Responsibility,UpdateTime,PositionName,DepartmentName,CompanyName,UserType,Status,LastLoginTime,StaffNO,CompanyID,PersonName,IDCard,IsParentCompany ");
                strSql.Append(" FROM FM2E_UserView ");
                strSql.Append(" where CompanyID=@Company");

                SqlParameter[] parameters = {
					new SqlParameter("@Company", SqlDbType.VarChar,2)};
                parameters[0].Value = companyID;


                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                        list.Add(GetData(rd));
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException(string.Format("公司ID为{0}的用户列表失败", companyID), ex);
            }
            return list;
        }

        public IList GetUsersByDepartmentID(long DepartmentID)
        {
            ArrayList list = new ArrayList();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select UserName,DepartmentID,PositionID,Sex,Birthday,PhotoUrl,OfficePhone,MobilePhone,HomePhone,Fax,Address,Password,Email,IM,Responsibility,UpdateTime,PositionName,DepartmentName,CompanyName,UserType,Status,LastLoginTime,StaffNO,CompanyID,PersonName,IDCard,IsParentCompany ");
                strSql.Append(" FROM FM2E_UserView ");
                strSql.Append(" where DepartmentID=@DepartmentID");

                SqlParameter[] parameters = {
					new SqlParameter("@DepartmentID", SqlDbType.BigInt,8)};
                parameters[0].Value = DepartmentID;


                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                        list.Add(GetData(rd));
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException(string.Format("公司ID为{0}的用户列表失败", DepartmentID), ex);
            }
            return list;
        }

        /// <summary>
        /// 更新用户名与密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        void IUser.UpdatePassword(string userName, string password)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("UPDATE FM2E_User ");
                strSql.Append(" set Password=@Password ");
                strSql.Append(" Where UserName=@UserName");

                SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.VarChar,20),
                    new SqlParameter("@Password",SqlDbType.VarChar,32)};

                parameters[0].Value = userName;
                parameters[1].Value = password;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("更新密码失败", e);
            }
        }
        /// <summary>
        /// 校验密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>正确-true，否则-false</returns>
        bool IUser.ValidatePassword(string userName, string password)
        {
            bool bSuccess = false;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT Password ");
                strSql.Append(" FROM FM2E_User ");
                strSql.Append(" Where UserName=@UserName");
                SqlParameter[] parameters = {
                        new SqlParameter("@UserName", SqlDbType.VarChar,20)};
                parameters[0].Value = userName;

                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    string tmp = "";
                    while (rd.Read())
                    {
                        if (!Convert.IsDBNull(rd["Password"]))
                            tmp = Convert.ToString(rd["Password"]);
                    }

                    if (tmp != "" && tmp == password)
                    {
                        bSuccess = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new DALException("密码不符", e);
            }

            return bSuccess;
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        void IUser.AddUser(UserInfo user)
        {
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into FM2E_User(");
                    strSql.Append("UserName,DepartmentID,PositionID,Sex,Birthday,PhotoUrl,OfficePhone,MobilePhone,HomePhone,Fax,Address,Password,Email,IM,Responsibility,UpdateTime,UserType,Status,LastLoginTime,StaffNO,CompanyID,PersonName,IDCard)");
                    strSql.Append(" values (");
                    strSql.Append("@UserName,@DepartmentID,@PositionID,@Sex,@Birthday,@PhotoUrl,@OfficePhone,@MobilePhone,@HomePhone,@Fax,@Address,@Password,@Email,@IM,@Responsibility,@UpdateTime,@UserType,@Status,@LastLoginTime,@StaffNO,@CompanyID,@PersonName,@IDCard)");
                    SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
					new SqlParameter("@DepartmentID", SqlDbType.BigInt,8),
					new SqlParameter("@PositionID", SqlDbType.BigInt,8),
					new SqlParameter("@Sex", SqlDbType.TinyInt,1),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar,80),
					new SqlParameter("@OfficePhone", SqlDbType.VarChar,20),
					new SqlParameter("@MobilePhone", SqlDbType.VarChar,20),
					new SqlParameter("@HomePhone", SqlDbType.VarChar,20),
					new SqlParameter("@Fax", SqlDbType.VarChar,20),
					new SqlParameter("@Address", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.VarChar,32),
					new SqlParameter("@Email", SqlDbType.VarChar,30),
					new SqlParameter("@IM", SqlDbType.VarChar,30),
					new SqlParameter("@Responsibility", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UserType", SqlDbType.TinyInt,1),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
					new SqlParameter("@StaffNO", SqlDbType.VarChar,20),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@PersonName", SqlDbType.NVarChar,20),
					new SqlParameter("@IDCard", SqlDbType.VarChar,20)};
                    parameters[0].Value = user.UserName;
                    parameters[1].Value = user.DepartmentID == 0 ? SqlInt64.Null : user.DepartmentID;
                    parameters[2].Value = user.PositionID == 0 ? SqlInt64.Null : user.PositionID;
                    parameters[3].Value = user.Sex;
                    parameters[4].Value = DateTime.Compare(user.Birthday, DateTime.MinValue) == 0 ? SqlDateTime.Null : user.Birthday;
                    parameters[5].Value = user.PhotoUrl;
                    parameters[6].Value = user.OfficePhone;
                    parameters[7].Value = user.MobilePhone;
                    parameters[8].Value = user.HomePhone;
                    parameters[9].Value = user.Fax;
                    parameters[10].Value = user.Address;
                    parameters[11].Value = user.Password;
                    parameters[12].Value = user.Email;
                    parameters[13].Value = user.IM;
                    parameters[14].Value = user.Responsibility;
                    parameters[15].Value = DateTime.Compare(user.UpdateTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : user.UpdateTime;
                    parameters[16].Value = user.UserType;
                    parameters[17].Value = user.Status;
                    parameters[18].Value = DateTime.Compare(user.LastLoginTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : user.LastLoginTime;
                    parameters[19].Value = user.StaffNO;
                    parameters[20].Value = user.CompanyID;
                    parameters[21].Value = user.PersonName;
                    parameters[22].Value = user.IDCard;

                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                    if (user.Roles != null)
                    {
                        //添加角色资料
                        StringBuilder roleSql = new StringBuilder();
                        roleSql.Append("insert into FM2E_UserRole(");
                        roleSql.Append("UserName,RoleID)");
                        roleSql.Append(" values (");
                        roleSql.Append("@UserName,@RoleID)");
                        SqlParameter[] rolePara = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
					new SqlParameter("@RoleID", SqlDbType.BigInt)};

                        foreach (object item in user.Roles)
                        {
                            UserRoleInfo role = (UserRoleInfo)item;
                            rolePara[0].Value = user.UserName;
                            rolePara[1].Value = role.RoleID;

                            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, roleSql.ToString(), rolePara);
                        }
                    }
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    throw new DALException("添加用户失败", e);
                }
            }
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        void IUser.UpdateUser(UserInfo user)
        {
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update FM2E_User set ");
                    strSql.Append("DepartmentID=@DepartmentID,");
                    strSql.Append("PositionID=@PositionID,");
                    strSql.Append("Sex=@Sex,");
                    strSql.Append("Birthday=@Birthday,");
                    strSql.Append("PhotoUrl=@PhotoUrl,");
                    strSql.Append("OfficePhone=@OfficePhone,");
                    strSql.Append("MobilePhone=@MobilePhone,");
                    strSql.Append("HomePhone=@HomePhone,");
                    strSql.Append("Fax=@Fax,");
                    strSql.Append("Address=@Address,");
                    strSql.Append("Password=@Password,");
                    strSql.Append("Email=@Email,");
                    strSql.Append("IM=@IM,");
                    strSql.Append("Responsibility=@Responsibility,");
                    strSql.Append("UpdateTime=@UpdateTime,");
                    strSql.Append("UserType=@UserType,");
                    strSql.Append("Status=@Status,");
                    strSql.Append("LastLoginTime=@LastLoginTime,");
                    strSql.Append("StaffNO=@StaffNO,");
                    strSql.Append("CompanyID=@CompanyID,");
                    strSql.Append("PersonName=@PersonName,");
                    strSql.Append("IDCard=@IDCard");
                    strSql.Append(" where UserName=@UserName ");
                    SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
					new SqlParameter("@DepartmentID", SqlDbType.BigInt,8),
					new SqlParameter("@PositionID", SqlDbType.BigInt,8),
					new SqlParameter("@Sex", SqlDbType.TinyInt,1),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar,80),
					new SqlParameter("@OfficePhone", SqlDbType.VarChar,20),
					new SqlParameter("@MobilePhone", SqlDbType.VarChar,20),
					new SqlParameter("@HomePhone", SqlDbType.VarChar,20),
					new SqlParameter("@Fax", SqlDbType.VarChar,20),
					new SqlParameter("@Address", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.VarChar,32),
					new SqlParameter("@Email", SqlDbType.VarChar,30),
					new SqlParameter("@IM", SqlDbType.VarChar,30),
					new SqlParameter("@Responsibility", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UserType", SqlDbType.TinyInt,1),
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
					new SqlParameter("@StaffNO", SqlDbType.VarChar,20),
					new SqlParameter("@CompanyID", SqlDbType.VarChar,2),
					new SqlParameter("@PersonName", SqlDbType.NVarChar,20),
					new SqlParameter("@IDCard", SqlDbType.VarChar,20)};
                    parameters[0].Value = user.UserName;
                    parameters[1].Value = user.DepartmentID == 0 ? SqlInt64.Null : user.DepartmentID;
                    parameters[2].Value = user.PositionID == 0 ? SqlInt64.Null : user.PositionID;
                    parameters[3].Value = user.Sex;
                    parameters[4].Value = DateTime.Compare(user.Birthday, DateTime.MinValue) == 0 ? SqlDateTime.Null : user.Birthday;
                    parameters[5].Value = user.PhotoUrl;
                    parameters[6].Value = user.OfficePhone;
                    parameters[7].Value = user.MobilePhone;
                    parameters[8].Value = user.HomePhone;
                    parameters[9].Value = user.Fax;
                    parameters[10].Value = user.Address;
                    parameters[11].Value = user.Password;
                    parameters[12].Value = user.Email;
                    parameters[13].Value = user.IM;
                    parameters[14].Value = user.Responsibility;
                    parameters[15].Value = DateTime.Compare(user.UpdateTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : user.UpdateTime;
                    parameters[16].Value = user.UserType;
                    parameters[17].Value = user.Status;
                    parameters[18].Value = DateTime.Compare(user.LastLoginTime, DateTime.MinValue) == 0 ? SqlDateTime.Null : user.LastLoginTime;
                    parameters[19].Value = user.StaffNO;
                    parameters[20].Value = user.CompanyID;
                    parameters[21].Value = user.PersonName;
                    parameters[22].Value = user.IDCard;

                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                    //更新用户角色
                    if (user.Roles != null)
                    {
                        //先删除原有的用户角色信息，再添加
                        StringBuilder delRole = new StringBuilder();
                        delRole.Append("delete FM2E_UserRole ");
                        delRole.Append(" where UserName=@UserName ");
                        SqlParameter[] delRolePara = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20)};

                        delRolePara[0].Value = user.UserName;
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, delRole.ToString(), delRolePara);

                        StringBuilder insertSql = new StringBuilder();
                        insertSql.Append("insert into FM2E_UserRole(");
                        insertSql.Append("UserName,RoleID)");
                        insertSql.Append(" values (");
                        insertSql.Append("@UserName,@RoleID)");
                        SqlParameter[] insertSqlPara = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
					new SqlParameter("@RoleID", SqlDbType.BigInt,8)};

                        foreach (object item in user.Roles)
                        {
                            UserRoleInfo role = (UserRoleInfo)item;
                            insertSqlPara[0].Value = user.UserName;
                            insertSqlPara[1].Value = role.RoleID;
                            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, insertSql.ToString(), insertSqlPara);
                        }
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new DALException("修改用户信息失败", ex);
                }
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userName"></param>
        void IUser.DeleteUser(string userName)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete FM2E_User ");
                strSql.Append(" where UserName=@UserName ");
                SqlParameter[] parameters = {
                        new SqlParameter("@UserName", SqlDbType.VarChar,20)};
                parameters[0].Value = userName;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                throw new DALException("删除用户信息失败", e);
            }
        }
        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueryParam IUser.GenerateSearchTerm(UserSearchInfo item)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = "FM2E_UserView";
            qp.ReturnFields = "*";
            qp.OrderBy = "order by UpdateTime desc";

            string sqlSearch = "where 1=1 ";
            if (item.UserName != null && item.UserName.Trim() != string.Empty)
            {
                sqlSearch += " and UserName like '%" + item.UserName.Trim() + "%'";
            }
            if (item.PersonName != null && item.PersonName.Trim() != string.Empty)
            {
                sqlSearch += " and PersonName like '%" + item.PersonName.Trim() + "%'";
            }
            if (item.UserType != UserType.Unknown)
            {
                sqlSearch += " and UserType=" + (int)item.UserType;
            }
            if (item.Status != UserStatus.Unknown)
            {
                sqlSearch += " and Status=" + (int)item.Status;
            }
            if (!string.IsNullOrEmpty(item.CompanyID) && item.CompanyID.Trim() != "0")
            {
                sqlSearch += " and CompanyID='" + item.CompanyID + "'";
            }
            if (item.DepartmentID != 0)
            {
                sqlSearch += " and DepartmentID=" + item.DepartmentID;
            }
            if (item.Sex != Sex.Unknown)
            {
                sqlSearch += " and Sex=" + (int)item.Sex;
            }
            qp.Where = sqlSearch;
            return qp;
        }

        

        /// <summary>
        /// 根据查询条件获取用户列表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList IUser.GetList(QueryParam term, out int recordCount)
        {
            try
            {
                return SQLHelper.GetObjectList(this.GetData, term, out recordCount);
            }
            catch (Exception e)
            {
                throw new DALException("获取用户列表分页失败", e);
            }
        }
        /// <summary>
        /// 用户登录校验
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>如果用户与密码均正确，则返回用户记录，否则返回null</returns>
        LoginUserInfo IUser.UserLogin(string userName, string password)
        {
            LoginUserInfo item = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT CompanyID, CompanyName,DepartmentID,DepartmentName,PersonName, UserName, Password,UserType,Status,IM,IsParentCompany ");
                strSql.Append(" FROM FM2E_UserView");
                strSql.Append(" Where UserName=@UserName");
                SqlParameter[] parameters = {
                        new SqlParameter("@UserName", SqlDbType.VarChar,20)};
                parameters[0].Value = userName;

                string tmp = null;
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        item = new LoginUserInfo();

                        if (!Convert.IsDBNull(rd["UserName"]))
                            item.UserName = Convert.ToString(rd["UserName"]);

                        if (!Convert.IsDBNull(rd["Password"]))
                            tmp = Convert.ToString(rd["Password"]);

                        if (!Convert.IsDBNull(rd["CompanyID"]))
                            item.CompanyID = Convert.ToString(rd["CompanyID"]);

                        if (!Convert.IsDBNull(rd["CompanyName"]))
                            item.CompanyName = Convert.ToString(rd["CompanyName"]);

                        if (!Convert.IsDBNull(rd["DepartmentID"]))
                            item.DepartmentID = Convert.ToInt64(rd["DepartmentID"]);

                        if (!Convert.IsDBNull(rd["DepartmentName"]))
                            item.DepartmentName = Convert.ToString(rd["DepartmentName"]);

                        if (!Convert.IsDBNull(rd["PersonName"]))
                            item.PersonName = Convert.ToString(rd["PersonName"]);

                        if (!Convert.IsDBNull(rd["UserType"]))
                            item.UserType = (UserType)Convert.ToInt32(rd["UserType"]);

                        if (!Convert.IsDBNull(rd["Status"]))
                            item.Status = (UserStatus)Convert.ToInt32(rd["Status"]);

                        if (!Convert.IsDBNull(rd["IM"]))
                            item.IM = Convert.ToString(rd["IM"]);

                        if (!Convert.IsDBNull(rd["IsParentCompany"]))
                            item.IsParentCompany = Convert.ToBoolean(rd["IsParentCompany"]);
                    }
                }

                if (item == null)
                    return null;

                if (tmp.Trim() != password)
                    return null;

                //更新登录时间
                StringBuilder strUpdate = new StringBuilder();
                strUpdate.Append("Update FM2E_User set LastLoginTime=@LastLoginTime where UserName=@UserName");

                SqlParameter[] updateParam = {
                        new SqlParameter("@UserName", SqlDbType.VarChar,20),
                        new SqlParameter("@LastLoginTime", SqlDbType.DateTime),                         
                                                 };
                updateParam[0].Value = userName;
                updateParam[1].Value = DateTime.Now;

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strUpdate.ToString(), updateParam);
            }
            catch (Exception e)
            {
                item = null;
                throw new DALException("用户登录失败" + e.Message, e);
            }

            return item;
        }
        /// <summary>
        /// 获取用户的登陆信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        LoginUserInfo IUser.GetLoginUser(string userName)
        {
            LoginUserInfo item = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT CompanyID, CompanyName,DepartmentID,DepartmentName,PositionID,PositionName,PersonName, UserName, Password,UserType,Status,IM,IsParentCompany ");
                strSql.Append(" FROM FM2E_UserView");
                strSql.Append(" Where UserName=@UserName");
                SqlParameter[] parameters = {
                        new SqlParameter("@UserName", SqlDbType.VarChar,20)};
                parameters[0].Value = userName;

                string tmp = null;
                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                    {
                        item = new LoginUserInfo();

                        if (!Convert.IsDBNull(rd["UserName"]))
                            item.UserName = Convert.ToString(rd["UserName"]);

                        if (!Convert.IsDBNull(rd["Password"]))
                            tmp = Convert.ToString(rd["Password"]);

                        if (!Convert.IsDBNull(rd["CompanyID"]))
                            item.CompanyID = Convert.ToString(rd["CompanyID"]);

                        if (!Convert.IsDBNull(rd["CompanyName"]))
                            item.CompanyName = Convert.ToString(rd["CompanyName"]);

                        if (!Convert.IsDBNull(rd["DepartmentID"]))
                            item.DepartmentID = Convert.ToInt64(rd["DepartmentID"]);

                        if (!Convert.IsDBNull(rd["DepartmentName"]))
                            item.DepartmentName = Convert.ToString(rd["DepartmentName"]);

                        if (!Convert.IsDBNull(rd["PositionID"]))
                            item.PositionID = Convert.ToInt64(rd["PositionID"]);

                        if (!Convert.IsDBNull(rd["PositionName"]))
                            item.PositionName = Convert.ToString(rd["PositionName"]);

                        if (!Convert.IsDBNull(rd["PersonName"]))
                            item.PersonName = Convert.ToString(rd["PersonName"]);

                        if (!Convert.IsDBNull(rd["UserType"]))
                            item.UserType = (UserType)Convert.ToInt32(rd["UserType"]);

                        if (!Convert.IsDBNull(rd["Status"]))
                            item.Status = (UserStatus)Convert.ToInt32(rd["Status"]);
                        
                        if (!Convert.IsDBNull(rd["IM"]))
                            item.IM = Convert.ToString(rd["IM"]);

                        if (!Convert.IsDBNull(rd["IsParentCompany"]))
                            item.IsParentCompany = Convert.ToBoolean(rd["IsParentCompany"]);
                    }
                }
            }
            catch (Exception e)
            {
                item = null;
                throw new DALException("获取登录用户信息失败", e);
            }

            return item;
        }


        //**********Modified by Xue    For V 3.1.2     2011-10-20****************************************************************************************************
        /// <summary>
        /// 通过部门ID获取用户
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public IList GetUsersByDepartmentId(long deptId)
        {
            ArrayList list = new ArrayList();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select UserName,DepartmentID,PositionID,Sex,Birthday,PhotoUrl,OfficePhone,MobilePhone,HomePhone,Fax,Address,Password,Email,IM,Responsibility,UpdateTime,PositionName,DepartmentName,CompanyName,UserType,Status,LastLoginTime,StaffNO,CompanyID,PersonName,IDCard,IsParentCompany ");
                strSql.Append(" FROM FM2E_UserView ");
                strSql.Append(" where DepartmentID=@DepartmentID");

                SqlParameter[] parameters = {
					new SqlParameter("@DepartmentID", SqlDbType.BigInt,8)};
                parameters[0].Value = deptId;


                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                        list.Add(GetData(rd));
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException(string.Format("部门ID为{0}的用户列表失败", deptId), ex);
            }
            return list;
        }
        //**********Modification Finished 2011-6-27**********************************************************************************************



        /// <summary>
        /// 获取所属工程师的所有用户
        /// </summary>
        /// <param name="im"></param>
        /// <returns></returns>
        public IList GetUsersByIM(string im)
        {
            ArrayList list = new ArrayList();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select UserName,DepartmentID,PositionID,Sex,Birthday,PhotoUrl,OfficePhone,MobilePhone,HomePhone,Fax,Address,Password,Email,IM,Responsibility,UpdateTime,PositionName,DepartmentName,CompanyName,UserType,Status,LastLoginTime,StaffNO,CompanyID,PersonName,IDCard,IsParentCompany ");
                strSql.Append(" FROM FM2E_UserView ");
                strSql.Append(" where IM like '%@IM%'");

                SqlParameter[] parameters = {
					new SqlParameter("@IM", SqlDbType.VarChar,30)};
                parameters[0].Value = im;


                using (SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
                {
                    while (rd.Read())
                        list.Add(GetData(rd));
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                throw new DALException(string.Format("系统ID为{0}的用户列表失败", im), ex);
            }
            return list;
        }


        #endregion
    }
}
