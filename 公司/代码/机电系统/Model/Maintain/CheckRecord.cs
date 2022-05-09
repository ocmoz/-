using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Maintain
{
    public class CheckRecord
    {

        public CheckRecord(string userName, string userDeptName, string userPsnName, string checkDate, string remark)
        {
            this.UserName = userName;
            this.UserDeptName = userDeptName;
            this.UserPsnName = userPsnName;
            this.CheckDate = checkDate;
            this.Remark = remark;
        }

        private string _userName;
        private string _userDeptName;
        private string _userPsnName;
        private string _checkDate;
        private string _evenName;
        private string _remark;

        public string UserName
        {
            set { this._userName = value; }
            get { return this._userName; }
        }

        public string UserDeptName
        {
            set { this._userDeptName = value; }
            get { return this._userDeptName; }
        }

        public string UserPsnName
        {
            set { this._userPsnName = value; }
            get { return this._userPsnName; }
        }

        public string CheckDate
        {
            set { this._checkDate = value; }
            get { return this._checkDate; }
        }


        public string Remark
        {
            set { this._remark = value; }
            get { return this._remark; }
        }


    }
}
