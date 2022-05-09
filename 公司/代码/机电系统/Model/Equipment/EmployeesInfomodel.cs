using System;
using System.Collections.Generic;
using System.Text;
using FM2E.Model.Basic;
using FM2E.Model.Utils;
//************************ 6-25 报表系统部分员工信息导入用*********************************************************************************
namespace FM2E.Model.Equipment
{
    public class EmployeesInfomodel
    {
        #region Model
        private int   _id ;
	    private string _stationname;
	    private int _infyear;
        private int _infmonth;
	    private string _company;
        private string _department;
        private string _orggrade;
        private string _orgcode;
        private string _name;
        private string _empcode;
        private string _positionname;
        private int _sex;
        private DateTime _birth;
        private string _idnum;
        private int _age;
        private string _nation;
        private string _nativeplace;
        private int _status;
        private int _education;
        private string _school;
        private string _major;
        private string _empstatus;
        private string _emptype;
        private string _retiretype;
        private string _resigntype;
        private string _dismisstype;
        private DateTime _companydate;
        private int _groupage;
        private int _companyage;
        private int _deptage;
        private DateTime _regulardate;
        private DateTime _hiredate;
        private DateTime _retiredate;
        private string _posttype;
        private string _post;
        private string _postcode;
        private string _rankname;
        private string _gradename;
        private string _tittletype;
        private string _tittlename;
        private string _tittlegrade;
        private DateTime _tittledate;
        private string _insurnum;
        private string _bankname;
        private string _barnknum;
        private DateTime _workdate;
        private int _workyear;
        private string _nowmajor;
        private string _formername;
        private float _height;
        private string _blood;
        private int _marriage;
        private string _health;
        private string _family;
        private string _personal;
        private string _idaddress;
        private string _birthplace;
        private string _address;
        private string _postalcode;
        private string _telephone;
        private string _mobilephone;
        private string _email;
        private string _isparttime;
        private DateTime _startparttime;
        private DateTime _endparttime;
        private string _isformation;
        private string _isunit;
        private string _istrial;
        private DateTime _starttrial;
        private DateTime _endtrial;
        private string _residence;
        private DateTime _submittime;
        private DateTime _submitname;

        #endregion Model


        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        public string stationname
        {
            set { _stationname = value; }
            get { return _stationname; }
        }
        public int infyear
        {
            set { _infyear = value; }
            get { return _infyear; }
        }
        public int infmonth
        {
            set { _infmonth = value; }
            get { return _infmonth; }
        }
        public string company
        {
            set { _company = value; }
            get { return _company; }
        }
        public string department
        {
            set { _department = value; }
            get { return _department; }
        }
        public string orggrade
        {
            set { _orggrade = value; }
            get { return _orggrade; }
        }
        public string orgcode
        {
            set { _orgcode = value; }
            get { return _orgcode; }
        }
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        public string empcode
        {
            set { _empcode = value; }
            get { return _empcode; }
        }
        public string positionname
        {
            set { _positionname = value; }
            get { return _positionname; }
        }
        public int sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        public DateTime birth
        {
            set { _birth = value; }
            get { return _birth; }
        }
        public string idnum
        {
            set { _idnum = value; }
            get { return _idnum; }
        }
        public int age
        {
            set { _age = value; }
            get { return _age; }
        }
        public string nation
        {
            set { _nation = value; }
            get { return _nation; }
        }
        public string nativeplace
        {
            set { _nativeplace = value; }
            get { return _nativeplace; }
        }
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }
        public int education
        {
            set { _education = value; }
            get { return _education; }
        }
        public string school
        {
            set { _school = value; }
            get { return _school; }
        }
        public string major
        {
            set { _major = value; }
            get { return _major; }
        }
        public string empstatus
        {
            set { _empstatus = value; }
            get { return _empstatus; }
        }
        public string emptype
        {
            set { _emptype = value; }
            get { return _emptype; }
        }
        public string retiretype
        {
            set { _retiretype = value; }
            get { return _retiretype; }
        }
        public string resigntype
        {
            set { _resigntype = value; }
            get { return _resigntype; }
        }
        public string dismisstype
        {
            set { _dismisstype = value; }
            get { return _dismisstype; }
        }
        public DateTime companydate
        {
            set { _companydate = value; }
            get { return _companydate; }
        }
        public int groupage
        {
            set { _groupage = value; }
            get { return _groupage; }
        }
        public int companyage
        {
            set { _companyage = value; }
            get { return _companyage; }
        }
        public int deptage
        {
            set { _deptage = value; }
            get { return _deptage; }
        }
        public DateTime regulardate
        {
            set { _regulardate = value; }
            get { return _regulardate; }
        }
        public DateTime hiredate
        {
            set { _hiredate = value; }
            get { return _hiredate; }
        }
        public DateTime retiredate
        {
            set { _retiredate = value; }
            get { return _retiredate; }
        }
        public string posttype
        {
            set { _posttype = value; }
            get { return _posttype; }
        }
        public string post
        {
            set { _post = value; }
            get { return _post; }
        }
        public string postcode
        {
            set { _postcode = value; }
            get { return _postcode; }
        }
        public string rankname
        {
            set { _rankname = value; }
            get { return _rankname; }
        }
        public string gradename
        {
            set { _gradename = value; }
            get { return _gradename; }
        }
        public string tittletype
        {
            set { _tittletype = value; }
            get { return _tittletype; }
        }
        public string tittlename
        {
            set { _tittlename = value; }
            get { return _tittlename; }
        }
        public string tittlegrade
        {
            set { _tittlegrade = value; }
            get { return _tittlegrade; }
        }
        public DateTime tittledate
        {
            set { _tittledate = value; }
            get { return _tittledate; }
        }
        public string insurnum
        {
            set { _insurnum = value; }
            get { return _insurnum; }
        }
        public string bankname
        {
            set { _bankname = value; }
            get { return _bankname; }
        }
        public string barnknum
        {
            set { _barnknum = value; }
            get { return _barnknum; }
        }
        public DateTime workdate
        {
            set { _workdate = value; }
            get { return _workdate; }
        }
        public int workyear
        {
            set { _workyear = value; }
            get { return _workyear; }
        }
        public string nowmajor
        {
            set { _nowmajor = value; }
            get { return _nowmajor; }
        }
        public string formername
        {
            set { _formername = value; }
            get { return _formername; }
        }
        public float height
        {
            set { _height = value; }
            get { return _height; }
        }
        public string blood
        {
            set { _blood = value; }
            get { return _blood; }
        }
        public int marriage
        {
            set { _marriage = value; }
            get { return _marriage; }
        }
        public string health
        {
            set { _health = value; }
            get { return _health; }
        }
        public string family
        {
            set { _family = value; }
            get { return _family; }
        }
        public string personal
        {
            set { _personal = value; }
            get { return _personal; }
        }
        public string idaddress
        {
            set { _idaddress = value; }
            get { return _idaddress; }
        }
        public string birthplace
        {
            set { _birthplace = value; }
            get { return _birthplace; }
        }
        public string address
        {
            set { _address = value; }
            get { return _address; }
        }
        public string postalcode
        {
            set { _postalcode = value; }
            get { return _postalcode; }
        }
        public string telephone
        {
            set { _telephone = value; }
            get { return _telephone; }
        }
        public string mobilephone
        {
            set { _mobilephone = value; }
            get { return _mobilephone; }
        }
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }
        public string isparttime
        {
            set { _isparttime = value; }
            get { return _isparttime; }
        }
        public DateTime startparttime
        {
            set { _startparttime = value; }
            get { return _startparttime; }
        }
        public DateTime endparttime
        {
            set { _endparttime = value; }
            get { return _endparttime; }
        }
        public string isformation
        {
            set { _isformation = value; }
            get { return _isformation; }
        }
        public string isunit
        {
            set { _isunit = value; }
            get { return _isunit; }
        }
        public string istrial
        {
            set { _istrial = value; }
            get { return _istrial; }
        }
        public DateTime starttrial
        {
            set { _starttrial = value; }
            get { return _starttrial; }
        }
        public DateTime endtrial
        {
            set { _endtrial = value; }
            get { return _endtrial; }
        }
        public string residence
        {
            set { _residence = value; }
            get { return _residence; }
        }
        public DateTime submittime
        {
            set { _submittime = value; }
            get { return _submittime; }
        }
        public DateTime submitname
        {
            set { _submitname = value; }
            get { return _submitname; }
        }


    }
}
