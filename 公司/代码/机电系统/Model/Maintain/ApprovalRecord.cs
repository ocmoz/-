using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Maintain
{
    public class ApprovalRecord
    {

        public ApprovalRecord(string _userName, string _userDeptName, string _userPsnName, string _approvalDate, string _evenName, string _remark)
        {
            this.UserName = _userName;
            this.UserDeptName = _userDeptName;
            this.UserPsnName = _userPsnName;
            this.ApprovalDate = _approvalDate;
            this.EvenName = _evenName;
            this.Remark = _remark;
        }

        private string _userName;
        private string _userDeptName;
        private string _userPsnName;
        private string _approvalDate;
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

        public string ApprovalDate
        {
            set { this._approvalDate = value; }
            get { return this._approvalDate; }
        }

        public string EvenName
        {
            set { this._evenName = value; }
            get { return this._evenName; }
        }

        public string Remark
        {
            set { this._remark = value; }
            get { return this._remark; }
        }
       

    }
}
