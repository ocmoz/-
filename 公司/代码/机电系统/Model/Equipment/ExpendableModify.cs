using System;
using System.Collections.Generic;
using System.Text;

namespace FM2E.Model.Equipment
{
    public class ExpendableModify
    {
        #region Model
        private int? _id;
        private int? _sheetid;
        private int? _recordid;
        private string _equipmentname;
        private DateTime? _modifytime;
        private int? _oldnum;
        private int? _newnum;
        private string _userid;
        private string _username;

        private string _type;

        public string type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SheetID
        {
            set { _sheetid = value; }
            get { return _sheetid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RecordID
        {
            set { _recordid = value; }
            get { return _recordid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string equipmentname
        {
            set { _equipmentname = value; }
            get { return _equipmentname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? modifytime
        {
            set { _modifytime = value; }
            get { return _modifytime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? oldnum
        {
            set { _oldnum = value; }
            get { return _oldnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? newnum
        {
            set { _newnum = value; }
            get { return _newnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string userid
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string username
        {
            set { _username = value; }
            get { return _username; }
        }
        #endregion Model

    }
}
