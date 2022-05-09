using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using FM2E.IDAL.EmployeesInfo;
using FM2E.Model.Equipment;
using FM2E.SQLServerDAL.Utils;
using FM2E.Model.Utils;
using FM2E.Model.Exceptions;
using System.Data.SqlTypes;
using FM2E.Model.Basic;
using FM2E.SQLServerDAL.Basic;
using System.Data.Common;
using FM2E.Model.Maintain;
namespace FM2E.SQLServerDAL.EmployeesInfo
{
    public class EmployeesInfo : IEmployeesInfo

    {
       public IList getxxx()
        {
            IList list = new List<FM2E.Model.Equipment.EmployeesInfomodel>();
            return list;
        }

        private const string INSERT_EMPLOYEESINFO = "insert Into t_EmployeesDetail([id],[stationName],[infYear],[infMonth],[company],[department],[orgGrade],[orgCode],[name],[empCode],[positionName],[sex],[birth],[idNum],[age],[nation],[nativePlace],[status],[education],[school],[major],[empStatus],[empType],[retireType],[resignType],[dismissType],[companyDate],[groupAge],[companyAge],[deptAge],[regularDate],[hireDate],[retireDate],[postType],[post],[postCode],[rankName],[gradeName],[titleType],[titleName],[titleGrade],[titleDate],[insurNum],[bankName],[barnkNum],[workDate],[workYear],[nowMajor],[formerName],[height],[blood],[marriage],[health],[family],[personal],[idAddress],[birthPlace],[address],[postalcode],[telephone],[mobilePhone],[email],[isParttime],[startParttime],[endParttime],[isFormation],[isUnit],[isTrial],[startTrial],[endTrial],[residence],[submitTime],[submitName]) "
                                                    + " values (@id,@stationName,@infYear,@infMonth,@company,@department,@orgGrade,@orgCode,@name,@empCode,@positionName,@sex,@birth,@idNum,@age,@nation,@nativePlace,@status,@education,@school,@major,@empStatus,@empType,@retireType,@resignType,@dismissType,@companyDate,@groupAge,@companyAge,@deptAge,@regularDate,@hireDate,@retireDate,@postType,@post,@postCode,@rankName,@gradeName,@titleType,@titleName,@titleGrade,@titleDate,@insurNum,@bankName,@barnkNum,@workDate,@workYear,@nowMajor,@formerName,@height,@blood,@marriage,@health,@family,@personal,@idAddress,@birthPlace,@address,@postalcode,@telephone,@mobilePhone,@email,@isParttime,@startParttime,@endParttime,@isFormation,@isUnit,@isTrial,@startTrial,@endTrial,@residence,@submitTime,@submitName) ";

            public void InsertEmployeesInfo(EmployeesInfomodel item)
            {
                SqlParameter[] param = GetInsertParam();
                param[0].Value = item.id;
                param[1].Value = item.stationname;
                param[2].Value = item.infyear;
                param[3].Value = item.infmonth;
                param[4].Value = item.company;
                param[5].Value = item.department;
                param[6].Value = item.orggrade;
                param[7].Value = item.orgcode;
                param[8].Value = item.name;
                param[9].Value = item.empcode;
                param[10].Value = item.positionname;
                param[11].Value = item.sex;
                param[12].Value = item.birth;
                param[13].Value = item.idnum;
                param[14].Value = item.age;
                param[15].Value = item.nation;
                param[16].Value = item.nativeplace;
                param[17].Value = item.status;
                param[18].Value = item.education;
                param[19].Value = item.school;
                param[20].Value = item.major;
                param[21].Value = item.empstatus;
                param[22].Value = item.emptype;
                param[23].Value = item.retiretype;
                param[24].Value = item.resigntype;
                param[25].Value = item.dismisstype;
                param[26].Value = item.companydate;
                param[27].Value = item.groupage;
                param[28].Value = item.companyage;
                param[29].Value = item.deptage;
                param[30].Value = item.regulardate;
                param[31].Value = item.hiredate;
                param[32].Value = item.retiredate;
                param[33].Value = item.posttype;
                param[34].Value = item.post;
                param[35].Value = item.postcode;
                param[36].Value = item.rankname;
                param[37].Value = item.gradename;
                param[38].Value = item.tittletype;
                param[39].Value = item.tittlename;
                param[40].Value = item.tittlegrade;
                param[41].Value = item.tittledate;
                param[42].Value = item.insurnum;
                param[43].Value = item.bankname;
                param[44].Value = item.barnknum;
                param[45].Value = item.workdate;
                param[46].Value = item.workyear;
                param[47].Value = item.nowmajor;
                param[48].Value = item.formername;
                param[49].Value = item.height;
                param[50].Value = item.blood;
                param[51].Value = item.marriage;
                param[52].Value = item.health;
                param[53].Value = item.family;
                param[54].Value = item.personal;
                param[55].Value = item.idaddress;
                param[56].Value = item.birthplace;
                param[57].Value = item.address;
                param[58].Value = item.postalcode;
                param[59].Value = item.telephone;
                param[60].Value = item.mobilephone;
                param[61].Value = item.email;
                param[62].Value = item.isparttime;
                param[63].Value = item.startparttime;
                param[64].Value = item.endparttime;
                param[65].Value = item.isformation;
                param[66].Value = item.isunit;
                param[67].Value = item.istrial;
                param[68].Value = item.starttrial;
                param[69].Value = item.endtrial;
                param[70].Value = item.residence;
                param[71].Value = item.submittime;
                param[72].Value = item.submitname;

                using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionStringRSMS))
                {
                    conn.Open();
                    try
                    {
                        int result = SQLHelper.ExecuteNonQuery(conn, CommandType.Text, INSERT_EMPLOYEESINFO, param);
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

            private static SqlParameter[] GetInsertParam()
            {
                SqlParameter[] param = SQLHelper.GetCachedParameters(INSERT_EMPLOYEESINFO);
                if (param == null)
                {
                    param = new SqlParameter[]{


                                    new SqlParameter("@id",SqlDbType.BigInt,8),
	                                new SqlParameter("@stationName",SqlDbType.VarChar,100),
	                                new SqlParameter("@infYear",SqlDbType.Int,4),
	                                new SqlParameter("@infMonth",SqlDbType.Int,4),
	                                new SqlParameter("@company",SqlDbType.VarChar,50),
	                                new SqlParameter("@department",SqlDbType.VarChar,50),
	                                new SqlParameter("@orgGrade",SqlDbType.VarChar,50),
	                                new SqlParameter("@orgCode",SqlDbType.VarChar,50),
	                                new SqlParameter("@name",SqlDbType.VarChar,100),
	                                new SqlParameter("@empCode",SqlDbType.VarChar,50),
	                                new SqlParameter("@positionName",SqlDbType.VarChar,50),
	                                new SqlParameter("@sex",SqlDbType.SmallInt,2),
	                                new SqlParameter("@birth",SqlDbType.DateTime,8),
	                                new SqlParameter("@idNum",SqlDbType.VarChar,20),
	                                new SqlParameter("@age",SqlDbType.SmallInt,2),
	                                new SqlParameter("@nation",SqlDbType.VarChar,50),
	                                new SqlParameter("@nativePlace",SqlDbType.NText,16),
	                                new SqlParameter("@status",SqlDbType.SmallInt,2),
	                                new SqlParameter("@education",SqlDbType.SmallInt,2),
	                                new SqlParameter("@school",SqlDbType.VarChar,50),
	                                new SqlParameter("@major",SqlDbType.VarChar,50),
	                                new SqlParameter("@empStatus",SqlDbType.VarChar,50),
	                                new SqlParameter("@empType",SqlDbType.VarChar,50),
	                                new SqlParameter("@retireType",SqlDbType.VarChar,50),
	                                new SqlParameter("@resignType",SqlDbType.VarChar,50),
	                                new SqlParameter("@dismissType",SqlDbType.VarChar,50),
	                                new SqlParameter("@companyDate",SqlDbType.DateTime,8),
	                                new SqlParameter("@groupAge",SqlDbType.SmallInt,2),
	                                new SqlParameter("@companyAge",SqlDbType.SmallInt,2),
	                                new SqlParameter("@deptAge",SqlDbType.SmallInt,2),
	                                new SqlParameter("@regularDate",SqlDbType.DateTime,8),
	                                new SqlParameter("@hireDate",SqlDbType.DateTime,8),
	                                new SqlParameter("@retireDate",SqlDbType.DateTime,8),
	                                new SqlParameter("@postType",SqlDbType.VarChar,50),
	                                new SqlParameter("@post",SqlDbType.VarChar,50),
	                                new SqlParameter("@postCode",SqlDbType.VarChar,50),
	                                new SqlParameter("@rankName",SqlDbType.VarChar,50),
	                                new SqlParameter("@gradeName",SqlDbType.VarChar,50),
	                                new SqlParameter("@titleType",SqlDbType.VarChar,50),
	                                new SqlParameter("@titleName",SqlDbType.VarChar,50),
	                                new SqlParameter("@titleGrade",SqlDbType.VarChar,50),
	                                new SqlParameter("@titleDate",SqlDbType.DateTime,8),
	                                new SqlParameter("@insurNum",SqlDbType.VarChar,50),
	                                new SqlParameter("@bankName",SqlDbType.VarChar,50),
	                                new SqlParameter("@barnkNum",SqlDbType.VarChar,50),
	                                new SqlParameter("@workDate",SqlDbType.DateTime,8),
	                                new SqlParameter("@workYear",SqlDbType.Int,4),
	                                new SqlParameter("@nowMajor",SqlDbType.VarChar,50),
	                                new SqlParameter("@formerName",SqlDbType.VarChar,50),
	                                new SqlParameter("@height",SqlDbType.Float,8),
	                                new SqlParameter("@blood",SqlDbType.VarChar,50),
	                                new SqlParameter("@marriage",SqlDbType.SmallInt,2),
	                                new SqlParameter("@health",SqlDbType.VarChar,50),
	                                new SqlParameter("@family",SqlDbType.VarChar,50),
	                                new SqlParameter("@personal",SqlDbType.VarChar,50),
	                                new SqlParameter("@idAddress",SqlDbType.VarChar,200),
	                                new SqlParameter("@birthPlace",SqlDbType.VarChar,100),
	                                new SqlParameter("@address",SqlDbType.VarChar,200),
	                                new SqlParameter("@postalcode",SqlDbType.VarChar,50),
	                                new SqlParameter("@telephone",SqlDbType.VarChar,50),
	                                new SqlParameter("@mobilePhone",SqlDbType.VarChar,50),
	                                new SqlParameter("@email",SqlDbType.VarChar,50),
	                                new SqlParameter("@isParttime",SqlDbType.VarChar,10),
	                                new SqlParameter("@startParttime",SqlDbType.DateTime,8),
	                                new SqlParameter("@endParttime",SqlDbType.DateTime,8),
	                                new SqlParameter("@isFormation",SqlDbType.VarChar,50),
	                                new SqlParameter("@isUnit",SqlDbType.VarChar,50),
	                                new SqlParameter("@isTrial",SqlDbType.VarChar,50),
	                                new SqlParameter("@startTrial",SqlDbType.DateTime,8),
	                                new SqlParameter("@endTrial",SqlDbType.DateTime,8),
	                                new SqlParameter("@residence",SqlDbType.VarChar,200),
	                                new SqlParameter("@submitTime",SqlDbType.DateTime,8),
	                                new SqlParameter("@submitName",SqlDbType.DateTime,8)
                    

                };
                    SQLHelper.CacheParameters(INSERT_EMPLOYEESINFO, param);
                }
                return param;
            }


        }

    
}
